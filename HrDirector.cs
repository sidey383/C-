namespace Nsu.HackathonProblem {
    using Contracts;
    public class HrDirector {
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
                return harmonicMean(grades);
            }

            private int findGrade(Wishlist wl, Employee employee) {
                return wl.DesiredEmployees.Length - findNumber(wl.DesiredEmployees, employee.Id);
            }

            private int findNumber(int[] order, int graded) {
                int number = 0;
                while (number < order.Length) {
                    if (order[number] == graded)
                        break;
                    number++;
                }
                return number;
            }

            private double harmonicMean(IEnumerable<int> values) {
                double sum = 0;
                foreach (int val in values) {
                    sum += 1/val;
                }
                return 1/sum;
            }

    }

}