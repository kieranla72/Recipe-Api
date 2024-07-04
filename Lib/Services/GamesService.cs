using System.Collections.Concurrent;
using System.Diagnostics;
using DB.Daos;
using DB.Models;
using Lib.Exceptions;

namespace Lib.Services;

public class GamesService : IGamesService
{
    private readonly IGamesDao _gamesDao;
    private int iterations = 10000000;
    
    public GamesService(IGamesDao gamesDao)
    {
        _gamesDao = gamesDao;
    }

    public async Task<int> SaveSports(List<GameCreateDto> gameSearchRequests)
    {
        var r = new Random();
        var gameCreateDtos = Enumerable.Range(0, iterations).Select(i => new GameCreateDto
        {
            GameTime = new DateTime(2024, 1, 14),
            HomeTeamId = r.Next(0, 100),
            HomeTeamScore = r.Next(0, 5),
            AwayTeamId = r.Next(0, 100),
            AwayTeamScore = r.Next(0, 5),

            HomeTeamTimeOnBall = r.Next(0, 45),
            HomeTeamShotsOnTarget = r.Next(0, 15),
            HomeTeamTotalShots = r.Next(0, 25),
            HomeTeamAverageExpectedGoalsPerShot = r.NextDouble(),
            AwayTeamTimeOnBall = r.Next(0, 45),
            AwayTeamShotsOnTarget = r.Next(0, 15),
            AwayTeamTotalShots = r.Next(0, 25),
            AwayTeamAverageExpectedGoalsPerShot = r.NextDouble(),
        });

        var games = new ConcurrentBag<Game>();

        Parallel.ForEach(gameCreateDtos, gameCreateDto =>
        {
            games.Add(CalculateGameStatistics(gameCreateDto));
        });

        return 10;
    }
    public async Task<double> SaveSportsParallel(List<GameCreateDto> gameSearchRequests)
    {
        var r = new Random();
        // var gameCreateDtos = new ConcurrentBag<GameCreateDto>();
        var gameCreateDtos = Enumerable.Range(0, iterations).Select(i => new GameCreateDto
        {
            GameTime = new DateTime(2024, 1, 14),
            HomeTeamId = r.Next(0, 100),
            HomeTeamScore = r.Next(0, 5),
            AwayTeamId = r.Next(0, 100),
            AwayTeamScore = r.Next(0, 5),

            HomeTeamTimeOnBall = r.Next(0, 45),
            HomeTeamShotsOnTarget = r.Next(0, 15),
            HomeTeamTotalShots = r.Next(0, 25),
            HomeTeamAverageExpectedGoalsPerShot = r.NextDouble(),
            AwayTeamTimeOnBall = r.Next(0, 45),
            AwayTeamShotsOnTarget = r.Next(0, 15),
            AwayTeamTotalShots = r.Next(0, 25),
            AwayTeamAverageExpectedGoalsPerShot = r.NextDouble(),
        });
        
        var stopWatch = new Stopwatch();
        stopWatch.Start();

        var games = new ConcurrentBag<Game>();

        Parallel.ForEach(gameCreateDtos, gameCreateDto =>
        {
            games.Add(CalculateGameStatistics(gameCreateDto));
        });
        
        stopWatch.Stop();


        return stopWatch.ElapsedMilliseconds;
    }
    
    public async Task<double> SaveSportsParallelListPartition(List<GameCreateDto> gameSearchRequests)
    {
        var r = new Random();
        // var gameCreateDtos = new ConcurrentBag<GameCreateDto>();
        var gameCreateDtos = Enumerable.Range(0, iterations).Select(i => new GameCreateDto
        {
            GameTime = new DateTime(2024, 1, 14),
            HomeTeamId = r.Next(0, 100),
            HomeTeamScore = r.Next(0, 5),
            AwayTeamId = r.Next(0, 100),
            AwayTeamScore = r.Next(0, 5),

            HomeTeamTimeOnBall = r.Next(0, 45),
            HomeTeamShotsOnTarget = r.Next(0, 15),
            HomeTeamTotalShots = r.Next(0, 25),
            HomeTeamAverageExpectedGoalsPerShot = r.NextDouble(),
            AwayTeamTimeOnBall = r.Next(0, 45),
            AwayTeamShotsOnTarget = r.Next(0, 15),
            AwayTeamTotalShots = r.Next(0, 25),
            AwayTeamAverageExpectedGoalsPerShot = r.NextDouble(),
        }).ToList();
        
        var stopWatch = new Stopwatch();
        stopWatch.Start();
        
        var rangePartitioner = Partitioner.Create(0, gameCreateDtos.Count);

        var games = new ConcurrentBag<Game>();
        
        Parallel.ForEach(rangePartitioner, (range, loop) =>
        {
            for (var i = range.Item1; i < range.Item2; i++)
            {
                games.Add(CalculateGameStatistics(gameCreateDtos[i]));
            }
        });
        
        stopWatch.Stop();
        
        return stopWatch.ElapsedMilliseconds;
    }    
    public async Task<double> SaveSportsNonParallelListPartition(List<GameCreateDto> gameSearchRequests)
    {
        var r = new Random();
        var gameCreateDtos = Enumerable.Range(0, iterations).Select(i => new GameCreateDto
        {
            GameTime = new DateTime(2024, 1, 14),
            HomeTeamId = r.Next(0, 100),
            HomeTeamScore = r.Next(0, 5),
            AwayTeamId = r.Next(0, 100),
            AwayTeamScore = r.Next(0, 5),

            HomeTeamTimeOnBall = r.Next(0, 45),
            HomeTeamShotsOnTarget = r.Next(0, 15),
            HomeTeamTotalShots = r.Next(0, 25),
            HomeTeamAverageExpectedGoalsPerShot = r.NextDouble(),
            AwayTeamTimeOnBall = r.Next(0, 45),
            AwayTeamShotsOnTarget = r.Next(0, 15),
            AwayTeamTotalShots = r.Next(0, 25),
            AwayTeamAverageExpectedGoalsPerShot = r.NextDouble(),
        }).ToList();

        var games = new List<Game>();
        
        var stopWatch = new Stopwatch();
        stopWatch.Start();

        foreach (var gameCreateDto in gameCreateDtos)
        {
            games.Add(CalculateGameStatistics(gameCreateDto));
        }
        
        stopWatch.Stop();


        return stopWatch.ElapsedMilliseconds;
    }
    
    public async Task<double> SaveSportsNonParallel(List<GameCreateDto> gameSearchRequests)
    {
        var r = new Random();
        var gameCreateDtos = Enumerable.Range(0, iterations).Select(i => new GameCreateDto
        {
            GameTime = new DateTime(2024, 1, 14),
            HomeTeamId = r.Next(0, 100),
            HomeTeamScore = r.Next(0, 5),
            AwayTeamId = r.Next(0, 100),
            AwayTeamScore = r.Next(0, 5),

            HomeTeamTimeOnBall = r.Next(0, 45),
            HomeTeamShotsOnTarget = r.Next(0, 15),
            HomeTeamTotalShots = r.Next(0, 25),
            HomeTeamAverageExpectedGoalsPerShot = r.NextDouble(),
            AwayTeamTimeOnBall = r.Next(0, 45),
            AwayTeamShotsOnTarget = r.Next(0, 15),
            AwayTeamTotalShots = r.Next(0, 25),
            AwayTeamAverageExpectedGoalsPerShot = r.NextDouble(),
        });

        var games = new List<Game>();
        
        var stopWatch = new Stopwatch();
        stopWatch.Start();

        foreach (var gameCreateDto in gameCreateDtos)
        {
            games.Add(CalculateGameStatistics(gameCreateDto));
        }
        
        stopWatch.Stop();


        return stopWatch.ElapsedMilliseconds;
    }

    public Game CalculateGameStatistics(GameCreateDto gameCreateDto)
    {
        var game = new Game(gameCreateDto);
        var totalTimeOnBall = game.HomeTeamTimeOnBall + game.AwayTeamTimeOnBall;
        
        game.HomeTeamPassingAccuracy = (double)game.HomeTeamTimeOnBall / totalTimeOnBall;
        game.HomeTeamShotAccuracy = (double)game.HomeTeamShotsOnTarget / game.HomeTeamTotalShots;
        game.HomeTeamExpectedGoals = game.HomeTeamAverageExpectedGoalsPerShot * game.HomeTeamTotalShots;   
        
        game.AwayTeamPassingAccuracy = (double)game.AwayTeamTimeOnBall / totalTimeOnBall;
        game.AwayTeamShotAccuracy = (double)game.AwayTeamShotsOnTarget / game.AwayTeamTotalShots;
        game.AwayTeamExpectedGoals = game.AwayTeamAverageExpectedGoalsPerShot * game.AwayTeamTotalShots;
        return game;
    }

    public async Task<List<Game>> GetSports()
    {
        var games = await _gamesDao.GetGames();
        return games.OrderByDescending(g => g.GameTime).ToList();
    }
    
    public async Task<Game> GetSportsById(int id)
    {
        var sportsData = await _gamesDao.GetGameById(id);
        if (sportsData == null)
        {
            throw new NotFoundException($"Could not find sports data with id {id}");
        }

        return sportsData;
    }
}

