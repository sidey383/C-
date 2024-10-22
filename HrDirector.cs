namespace Nsu.HackathonProblem {
    using Contracts;

    public class HrDirector {

            private readonly ILogger<HrDirector> _logger;


            public HrDirector(ILogger<HrDirector> logger) {
                this._logger = logger;
            }

            public double CalculateQuality(IEnumerable<Team> teams, IEnumerable<Wishlist> teamLeadsWishlists, IEnumerable<Wishlist> juniorsWishlists) {
                Dictionary<int, Wishlist> teamLeadsWishlistsDict = teamLeadsWishlists.ToDictionary(w => w.EmployeeId);
                Dictionary<int, Wishlist> juniorsWishlistsDict = juniorsWishlists.ToDictionary(w => w.EmployeeId);
                List<int> grades = new List<int>();
                foreach (Team t in teams) {
                    Wishlist tlw = teamLeadsWishlistsDict[t.TeamLead.Id];
                    Wishlist jw = teamLeadsWishlistsDict[t.Junior.Id];
                    grades.Add(findGrade(tlw, t.Junior));
                    grades.Add(findGrade(jw, t.TeamLead));
                }
                return MathUtils.harmonicMean(grades);
            }

            private int findGrade(Wishlist wl, Employee employee) {
                return wl.DesiredEmployees.Length - MathUtils.findPose(wl.DesiredEmployees, employee.Id);
            }


    }

}