namespace DB.Models;

public class Game
{
    public Game(GameCreateDto gameCreateDto)
    {
        Id = gameCreateDto.Id;
        GameTime = gameCreateDto.GameTime;
        HomeTeamId = gameCreateDto.HomeTeamId;
        HomeTeamScore = gameCreateDto.HomeTeamScore;
        HomeTeamTimeOnBall = gameCreateDto.HomeTeamTimeOnBall;
        HomeTeamShotsOnTarget = gameCreateDto.HomeTeamShotsOnTarget;
        HomeTeamTotalShots = gameCreateDto.HomeTeamTotalShots;
        HomeTeamAverageExpectedGoalsPerShot = gameCreateDto.HomeTeamAverageExpectedGoalsPerShot;        
        
        AwayTeamId = gameCreateDto.AwayTeamId;
        AwayTeamScore = gameCreateDto.AwayTeamScore;
        AwayTeamTimeOnBall = gameCreateDto.AwayTeamTimeOnBall;
        AwayTeamShotsOnTarget = gameCreateDto.AwayTeamShotsOnTarget;
        AwayTeamTotalShots = gameCreateDto.AwayTeamTotalShots;
        AwayTeamAverageExpectedGoalsPerShot = gameCreateDto.AwayTeamAverageExpectedGoalsPerShot;
    }
    public int Id { get; set; }
    public DateTime? GameTime { get; set; }
    public int HomeTeamId { get; set; }
    public Recipe? HomeTeam { get; set; } = null!;
    public int HomeTeamScore { get; set; }
    public Recipe? AwayTeam { get; set; } = null!;
    public int AwayTeamId { get; set; }
    public int AwayTeamScore { get; set; }
    public bool IsProcessed { get; set; }
    public bool? ProcessedDate { get; set; }
    public int HomeTeamTimeOnBall { get; set; }
    public int AwayTeamTimeOnBall { get; set; }
    public int HomeTeamShotsOnTarget { get; set; }
    public int HomeTeamTotalShots { get; set; }
    public int AwayTeamShotsOnTarget { get; set; }
    public int AwayTeamTotalShots { get; set; }
    public double HomeTeamAverageExpectedGoalsPerShot { get; set; }
    public double AwayTeamAverageExpectedGoalsPerShot { get; set; }
    
    public double HomeTeamPassingAccuracy { get; set; }
    public double AwayTeamPassingAccuracy { get; set; }
    public double HomeTeamShotAccuracy { get; set; }
    public double AwayTeamShotAccuracy { get; set; }
    public double HomeTeamExpectedGoals { get; set; }
    public double AwayTeamExpectedGoals { get; set; }
    public bool ResultIsOutOfSyncWithPerformance { get; set; }
}