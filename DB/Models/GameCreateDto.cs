using System.ComponentModel.DataAnnotations;

namespace DB.Models;

public class GameCreateDto
{
    public int Id { get; set; }
    [Required]
    public DateTime? GameTime { get; set; }
    [Required] 
    public int HomeTeamId { get; set; }
    [Required]
    public int HomeTeamScore { get; set; }
    [Required]
    public int AwayTeamId { get; set; }
    [Required]
    public int AwayTeamScore { get; set; }
    [Required]
    public int HomeTeamTimeOnBall { get; set; }
    [Required]
    public int AwayTeamTimeOnBall { get; set; }
    [Required]
    public int HomeTeamShotsOnTarget { get; set; }
    [Required]
    public int HomeTeamTotalShots { get; set; }
    [Required]
    public int AwayTeamShotsOnTarget { get; set; }
    [Required]
    public int AwayTeamTotalShots { get; set; }
    [Required]
    public double HomeTeamAverageExpectedGoalsPerShot { get; set; }
    [Required]
    public double AwayTeamAverageExpectedGoalsPerShot { get; set; }
}