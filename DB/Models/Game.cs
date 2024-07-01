
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace DB.Models;

public class Game
{
    public int Id { get; set; }
    [Required]
    public DateTime? GameTime { get; set; }

    [Required] 
    public int HomeTeamId { get; set; }
    public FootballTeam? HomeTeam { get; set; } = null!;
    [Required]
    public int HomeTeamScore { get; set; }
    public FootballTeam? AwayTeam { get; set; } = null!;
    [Required]
    public int AwayTeamId { get; set; }
    [Required]
    public int AwayTeamScore { get; set; }

    public bool IsProcessed { get; set; }
    public bool? ProcessedDate { get; set; }
}