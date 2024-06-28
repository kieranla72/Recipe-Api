
using System.ComponentModel.DataAnnotations;

namespace DB.Models;

public class SportsData
{
    public enum SportsTypes
    {
        Football,
        Rugby
    }
    
    public int Id { get; set; }
    [Required]
    public DateTime? GameTime { get; set; }
    [Required]
    public string HomeTeam { get; set; }
    [Required]
    public string AwayTeam { get; set; }
}

internal class ValidDate : ValidationAttribute
{
    public override bool IsValid(object value)
    {
        if (value is DateTime dateTime)
        {
            return dateTime != default;
        }
        return false;
    }
}