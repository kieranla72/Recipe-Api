using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using ApiTest.Comparers;
using DB;
using DB.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiTest.Controllers;

public class GamesControllerTest : TestsBase
{
    private GamesComparer _gamesComparer;

    public GamesControllerTest(CustomWebApplicationFactory<Program> factory) : base(factory)
    {
        _gamesComparer = new GamesComparer();
    }

    [Fact]
    public async Task CreateGames()
    {
        var newGames = GetNewGamesList();
        var client = _factory.CreateClient();
        
        var response = await client.PostAsJsonAsync("/", newGames);
        var games = await response.Content.ReadFromJsonAsync<List<Game>>();
        var gamesSortedByDate = games.OrderByDescending(g => g.GameTime).ToList();
        newGames[0].Id = gamesSortedByDate[0].Id;
        newGames[1].Id = gamesSortedByDate[1].Id;

        var insertedGames = await _dbContext.Games.ToListAsync();
        
        // Assert
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        Assert.Equal(2, games.Count);
        Assert.True(_gamesComparer.Equals(newGames, gamesSortedByDate));
        Assert.True(_gamesComparer.Equals(newGames, insertedGames));
    }
    
    [Fact]
    public async Task GetGames()
    {
        // Arrange
        var gamesToAdd = await SaveGamesToDb();
        var client = _factory.CreateClient();

        // Act
        var response = await client.GetAsync("/");

        // Assert
        response.EnsureSuccessStatusCode();
        var games = await response.Content.ReadFromJsonAsync<List<Game>>();
        Assert.True(_gamesComparer.Equals(gamesToAdd, games));
    }    
    
    [Fact]
    public async Task GetGameById()
    {
        // Arrange
        var gamesToAdd = await SaveGamesToDb();
        var client = _factory.CreateClient();

        // Act
        var response = await client.GetAsync($"/{gamesToAdd[0].Id}");

        // Assert
        response.EnsureSuccessStatusCode();
        var game = await response.Content.ReadFromJsonAsync<Game>();
        Assert.True(_gamesComparer.Equals(gamesToAdd[0], game));
    }    
    [Fact]
    public async Task GetGameById_NotFound()
    {
        // Arrange
        var gamesToAdd = await SaveGamesToDb();
        var client = _factory.CreateClient();

        // Act
        var response = await client.GetAsync("/123123");

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }
    
    private async Task<List<Game>> SaveGamesToDb()
    {
        var gamesToAdd = GetNewGamesList();
        _dbContext.Games.AddRange(gamesToAdd);
        await _dbContext.SaveChangesAsync();
        return gamesToAdd;
    }

    private List<Game> GetNewGamesList()
    {
        return new ()
        {
            new()
            {
                GameTime = new DateTime(2024, 1, 14),
                HomeTeamId = footballTeams[0].Id,
                HomeTeamScore = 0,
                AwayTeamId = footballTeams[1].Id,
                AwayTeamScore = 0
            },
            new()
            {
                GameTime = new DateTime(2024, 1, 12),
                HomeTeamId = footballTeams[1].Id,
                HomeTeamScore = 0,
                AwayTeamId = footballTeams[2].Id,
                AwayTeamScore = 0
            },
        };
    }
}