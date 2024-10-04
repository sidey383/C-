using System.Runtime.InteropServices.Marshalling;
using Nsu.HackathonProblem.Contracts;

namespace Nsu.HackathonProblem {
    public class HrManager : ITeamBuildingStrategy {

        private readonly ITeamBuildingStrategy _strategy;

        public HrManager(ITeamBuildingStrategy strategy) {
            this._strategy = strategy;
        }

        public IEnumerable<Team> BuildTeams(IEnumerable<Employee> teamLeads, IEnumerable<Employee> juniors, 
            IEnumerable<Wishlist> teamLeadsWishlists, IEnumerable<Wishlist> juniorsWishlists) {
            return _strategy.BuildTeams(teamLeads, juniors, teamLeadsWishlists, juniorsWishlists);
        }
    }

}