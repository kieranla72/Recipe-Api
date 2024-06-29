using DB;
using DB.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;

namespace ApiTest;

public class TestsBase : IClassFixture<CustomWebApplicationFactory<Program>>, IDisposable
{
    protected readonly CustomWebApplicationFactory<Program> _factory;
    protected FootballDbContext _dbContext;

    protected List<FootballTeam> footballTeams = new()
    {
        new() { Name = "Manchester United", CoefficientRanking = 0 },
        new() { Name = "Arsenal", CoefficientRanking = 0 },
        new() { Name = "Chelsea", CoefficientRanking = 0 },
    };

    public TestsBase(CustomWebApplicationFactory<Program> factory)
    {
        _factory = factory;
        var connectionString = "Server=127.0.0.1;Database=SportsTest;User=root;Password=example";

        var options = new DbContextOptionsBuilder<FootballDbContext>()
            .UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
            .Options;
        _dbContext = new FootballDbContext(options);
        _dbContext.FootballTeams.AddRange(footballTeams);
        _dbContext.SaveChanges();
    }

    public async void Dispose()
    {
        await _dbContext.Games.ExecuteDeleteAsync();
        await _dbContext.FootballTeams.ExecuteDeleteAsync();
    }
}