
using System.ComponentModel.DataAnnotations;

namespace DB.Models;

public class Game
{
    public int Id { get; set; }
    [Required]
    public DateTime? GameTime { get; set; }
    [Required]
    public string HomeTeam { get; set; }
    [Required]
    public int HomeTeamScore { get; set; }
    [Required]
    public string AwayTeam { get; set; }
    [Required]
    public int AwayTeamScore { get; set; }
}