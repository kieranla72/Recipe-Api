using System.ComponentModel.DataAnnotations;

namespace DB.Models;

public class FootballTeam
{
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public float CoefficientRanking { get; set; }
}