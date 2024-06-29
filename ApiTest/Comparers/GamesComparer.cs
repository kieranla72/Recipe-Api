using DB.Models;

namespace ApiTest.Comparers;

public class GamesComparer : IEqualityComparer<List<Game>>
{
    public bool Equals(List<Game>? list1, List<Game>? list2)
    {
        if (ReferenceEquals(list1, list2))
            return true;

        if (list1 == null || list2 == null)
            return false;

        if (list1.Count != list2.Count)
            return false;

        var orderedList1 = list1.OrderByDescending(g => g.Id).ToList();
        var orderedList2 = list2.OrderByDescending(g => g.Id).ToList();

        // Check each Game in both lists
        for (int i = 0; i < list1.Count; i++)
        {
            if (!GamesAreEqual(orderedList1[i], orderedList2[i]))
                return false;
        }

        return true;
    }

    public bool Equals(Game? game1, Game? game2)
    {
        if (ReferenceEquals(game1, game2))
            return true;

        if (game1 == null || game2 == null)
            return false;

        return GamesAreEqual(game1, game2);
    }

    public int GetHashCode(List<Game> list)
    {
        unchecked
        {
            int hash = 17;
            foreach (var game in list)
            {
                hash = hash * 23 + GetGameHashCode(game);
            }
            return hash;
        }
    }

    private bool GamesAreEqual(Game game1, Game game2)
    {
        // Compare Id, GameTime, HomeTeamId, HomeTeamScore, AwayTeamId, AwayTeamScore
        return game1.Id == game2.Id &&
               game1.GameTime == game2.GameTime &&
               game1.HomeTeamId == game2.HomeTeamId &&
               game1.HomeTeamScore == game2.HomeTeamScore &&
               game1.AwayTeamId == game2.AwayTeamId &&
               game1.AwayTeamScore == game2.AwayTeamScore;
    }

    private int GetGameHashCode(Game game)
    {
        unchecked
        {
            int hash = 17;
            hash = hash * 23 + game.Id.GetHashCode();
            hash = hash * 23 + (game.GameTime?.GetHashCode() ?? 0);
            hash = hash * 23 + game.HomeTeamId.GetHashCode();
            hash = hash * 23 + game.HomeTeamScore.GetHashCode();
            hash = hash * 23 + game.AwayTeamId.GetHashCode();
            hash = hash * 23 + game.AwayTeamScore.GetHashCode();
            return hash;
        }
    }
}