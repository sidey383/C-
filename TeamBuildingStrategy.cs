namespace C_;

public class TeamBuildingStrategy
{
    /// <summary>
    /// Распределяет тиилидов и джунов по командам
    /// </summary>
    /// <param name="teamLeads">Тимлиды</param>
    /// <param name="juniors">Джуны</param>
    /// <returns>Список команд</returns>
    public IEnumerable<Team> BuildTeams<T, J>(IEnumerable<T> teamLeads, IEnumerable<J> juniors,
        IEnumerable<Wishlist> teamLeadsWishlists, IEnumerable<Wishlist> juniorsWishlists) where J : Employee where T : Employee {
            if (teamLeads.Count() != juniors.Count())
                throw new Exception("Wrong employee count");
            IEnumerator<T> tlE = teamLeads.GetEnumerator();
            IEnumerator<J> juE = juniors.GetEnumerator();
            List<Team> teams = new List<Team>();
            while (tlE.MoveNext() && juE.MoveNext()) {
                teams.Add(new Team(tlE.Current, juE.Current));
            }
            return teams;
        }
}