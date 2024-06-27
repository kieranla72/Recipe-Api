
namespace DB.Models;

public class SportsData
{
    public enum SportsTypes
    {
        Football,
        Rugby
    }
    
    public int Id { get; set; }
    public DateTime GameTime { get; set; }
    public string HomeTeam { get; set; }
    public string AwayTeam { get; set; }
}