using System.Collections;
using Microsoft.VisualBasic.FileIO;

namespace C_;


public class Program
{


    public static void Main(string[] args)
    {
        CreateHostBuilder(args).Build().RunAsync();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args).ConfigureServices((hostContext, services) => {
            services.AddHostedService<Hackathon.HackathonWorker>();
            services.AddTransient<Hackathon.Hackathon>(_ => new Hackathon.Hackathon());
            // services.AddTransient<ITeamBuildingStrategy, TeamLeadsHateTheirJuniorsTeamBuildingStrategy>();
            // services.AddTransient<HrManager>();
            // services.AddTransient<HrDirector>();
        });

    public static void OldMain(string[] args)
    {
        TableReader reader = new TableReader();
        reader.ReadJuniors("Juniors5.csv");
        reader.ReadTeamleads("Teamleads5.csv");
        IEnumerable<Junior> juniors = reader.GetJuniors();
        IEnumerable<Teamlead> teamleads = reader.GetTeamleads();
        TeamBuildingStrategy strategy = new TeamBuildingStrategy();
        QualityAssesment qualityAssesment = new QualityAssesment();
        List<double> values = new List<double>();
        System.Random random = new System.Random();
        for (int i = 0; i < 1000; i++)
        {
            List<Junior> juniorsLoc = new List<Junior>(juniors);
            List<Teamlead> teamleadsLoc = new List<Teamlead>(teamleads);
            List<Wishlist> jwl = new List<Wishlist>();
            List<Wishlist> tlwl = new List<Wishlist>();
            foreach (Junior j in juniorsLoc)
            {
                jwl.Add(Wishlist.CreateRandom<Teamlead>(j.GetId(), teamleads, random));
            }
            foreach (Teamlead t in teamleadsLoc)
            {
                tlwl.Add(Wishlist.CreateRandom<Junior>(t.GetId(), juniors, random));
            }
            IEnumerable<Team> teams = strategy.BuildTeams(teamleadsLoc, juniorsLoc, tlwl, jwl);
            double quality = qualityAssesment.GetQuality(teams, tlwl, jwl);
            values.Add(quality);
        }
        values.ForEach(Console.WriteLine);
        Console.WriteLine();
        Console.WriteLine();
        Console.WriteLine(values.Average());
    }

}
