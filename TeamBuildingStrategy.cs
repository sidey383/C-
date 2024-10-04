using System;

namespace Nsu.HackathonProblem {

    using Contracts;

    public class TeamBuildingStrategy : ITeamBuildingStrategy
    {

        IEnumerable<Team> ITeamBuildingStrategy.BuildTeams(IEnumerable<Employee> teamLeads, IEnumerable<Employee> juniors, 
            IEnumerable<Wishlist> teamLeadsWishlists, IEnumerable<Wishlist> juniorsWishlists)
        {
                if (teamLeads.Count() != juniors.Count())
                    throw new Exception("Wrong employee count");
                IEnumerator<Employee> tlE = teamLeads.GetEnumerator();
                IEnumerator<Employee> juE = juniors.GetEnumerator();
                List<Team> teams = new List<Team>();
                while (tlE.MoveNext() && juE.MoveNext()) {
                    teams.Add(new Team(tlE.Current, juE.Current));
                }
                return teams;
        }
    }

}