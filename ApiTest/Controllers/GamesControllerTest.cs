using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using ApiTest.Comparers;
using DB;
using DB.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiTest.Controllers;

[Collection("Sequential")]
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
        var client = _factory.CreateClient();
        var newGames = GetNewGamesList();
        
        var response = await client.PostAsJsonAsync("/Games", newGames);
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
        var response = await client.GetAsync("/Games");

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
        var response = await client.GetAsync($"/Games/{gamesToAdd[0].Id}");

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
        var response = await client.GetAsync("/Games/123123");

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

    private List<GameCreateDto> GetGameCreateDtos()
    {
        return new ()
        {
            new  ()
            {
                GameTime = new DateTime(2024, 1, 14),
                HomeTeamId = BaseFootballTeams[0].Id,
                HomeTeamScore = 0,
                AwayTeamId = BaseFootballTeams[1].Id,
                AwayTeamScore = 0,
                
                HomeTeamTimeOnBall = 15,
                HomeTeamShotsOnTarget = 10,
                HomeTeamTotalShots = 14,
                HomeTeamAverageExpectedGoalsPerShot = 0.2,
                AwayTeamTimeOnBall = 15,
                AwayTeamShotsOnTarget = 10,
                AwayTeamTotalShots = 14,
                AwayTeamAverageExpectedGoalsPerShot = 0.2, 
            },
            new()
            {
                GameTime = new DateTime(2024, 1, 12),
                HomeTeamId = BaseFootballTeams[1].Id,
                HomeTeamScore = 0,
                AwayTeamId = BaseFootballTeams[2].Id,
                AwayTeamScore = 0,
                
                HomeTeamTimeOnBall = 15,
                HomeTeamShotsOnTarget = 10,
                HomeTeamTotalShots = 14,
                HomeTeamAverageExpectedGoalsPerShot = 0.2,
                AwayTeamTimeOnBall = 15,
                AwayTeamShotsOnTarget = 10,
                AwayTeamTotalShots = 14,
                AwayTeamAverageExpectedGoalsPerShot = 0.2, 
            },
        };
    }

    private List<Game> GetNewGamesList()
    {
        var gameCreateDtos = GetGameCreateDtos();
        return new ()
        {
            new Game (gameCreateDtos[0])
            {
                GameTime = new DateTime(2024, 1, 14),
                HomeTeamId = BaseFootballTeams[0].Id,
                HomeTeamScore = 0,
                AwayTeamId = BaseFootballTeams[1].Id,
                AwayTeamScore = 0,
                
                HomeTeamTimeOnBall = 15,
                HomeTeamShotsOnTarget = 10,
                HomeTeamTotalShots = 14,
                HomeTeamAverageExpectedGoalsPerShot = 0.2,
                AwayTeamTimeOnBall = 15,
                AwayTeamShotsOnTarget = 10,
                AwayTeamTotalShots = 14,
                AwayTeamAverageExpectedGoalsPerShot = 0.2, 
                
                HomeTeamPassingAccuracy = 50,
                HomeTeamShotAccuracy = 50,
                HomeTeamExpectedGoals = 2,
                AwayTeamPassingAccuracy = 50,
                AwayTeamShotAccuracy = 50,
                AwayTeamExpectedGoals = 2,
                ResultIsOutOfSyncWithPerformance = true,
            },
            new(gameCreateDtos[1])
            {
                GameTime = new DateTime(2024, 1, 12),
                HomeTeamId = BaseFootballTeams[1].Id,
                HomeTeamScore = 0,
                AwayTeamId = BaseFootballTeams[2].Id,
                AwayTeamScore = 0,
                
                HomeTeamTimeOnBall = 15,
                HomeTeamShotsOnTarget = 10,
                HomeTeamTotalShots = 14,
                HomeTeamAverageExpectedGoalsPerShot = 0.2,
                AwayTeamTimeOnBall = 15,
                AwayTeamShotsOnTarget = 10,
                AwayTeamTotalShots = 14,
                AwayTeamAverageExpectedGoalsPerShot = 0.2, 
                
                HomeTeamPassingAccuracy = 50,
                HomeTeamShotAccuracy = 50,
                HomeTeamExpectedGoals = 2,
                AwayTeamPassingAccuracy = 50,
                AwayTeamShotAccuracy = 50,
                AwayTeamExpectedGoals = 2,
                ResultIsOutOfSyncWithPerformance = true,
            },
        };
    }
}