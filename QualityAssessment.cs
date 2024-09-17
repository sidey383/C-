namespace C_;

public class QualityAssesment {

    public double GetQuality(IEnumerable<Team> teams, IEnumerable<Wishlist> teamLeadsWishlists, IEnumerable<Wishlist> juniorsWishlists) {
        List<double> quality = new List<double>();
        foreach (Team t in teams) {
            foreach (Wishlist tlwl in teamLeadsWishlists) {
                if (tlwl.EmployeeId.Equals(t.TeamLead.GetId())) {
                    foreach (Wishlist jwl in teamLeadsWishlists) {
                        if (jwl.EmployeeId.Equals(t.Junior.GetId())) {
                            quality.Add(tlwl.DesiredEmployees.Length - tlwl.GetPose(t.Junior.GetId()));
                            quality.Add(jwl.DesiredEmployees.Length - jwl.GetPose(t.TeamLead.GetId()));
                        }
                    }
                }
            }
        }
        return this.HarmonicMean(quality.ToArray());
    }

    private double HarmonicMean(double[] values) {
        double reverseSum = 0;
        foreach (double v in values) {
            reverseSum += 1/v;
        }
        return values.Length / reverseSum;
    }

}