using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using DB.Models;

namespace ApiTest.Controllers;

[Collection("Sequential")]
public class FootballTeamsControllerTest : TestsBase
{
    public FootballTeamsControllerTest(CustomWebApplicationFactory<Program> factory) : base(factory)
    {
    }

    [Fact]
    public async Task CreateFootballTeams()
    {
        var client = _factory.CreateClient();
        var newFootballTeams = GetNewFootballTeams();

        var response = await client.PostAsJsonAsync("/FootballTeams", newFootballTeams);
        var footballTeams = await response.Content.ReadFromJsonAsync<List<Recipe>>();
        var sortedFootballTeams = footballTeams.OrderBy(ft => ft.Name).ToList();

        newFootballTeams[0].Id = sortedFootballTeams[0].Id;
        newFootballTeams[1].Id = sortedFootballTeams[1].Id;

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        Assert.Equal(2, newFootballTeams.Count);
        Assert.Equal(JsonSerializer.Serialize(newFootballTeams), JsonSerializer.Serialize(footballTeams));

    }

    private List<Recipe> GetNewFootballTeams()
    {
        return
        [
            new() { Name = "Sheffield United", CoefficientRanking = 0 },
            new() { Name = "Sheffield Wednesday", CoefficientRanking = 0 },
        ];
    }
}