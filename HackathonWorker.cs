using System.Threading.Tasks.Dataflow;
using Nsu.HackathonProblem.Contracts;

namespace Nsu.HackathonProblem {

    public class HackathonWorker : IHostedService
    {


            private static readonly int ITERATION_COUNT = 1000;
            private readonly ILogger<HackathonWorker> _logger;
            private readonly Hackathon _hackathon;
            private readonly HrManager _hrManager;
            private readonly HrDirector _hrDirector;

        public HackathonWorker(
            ILogger<HackathonWorker> logger,
            Hackathon hackathon,
            HrManager hrManager,
            HrDirector hrDirector
        ) {
            this._logger = logger;
            this._hackathon = hackathon;
            this._hrDirector = hrDirector;
            this._hrManager = hrManager;
            _logger.LogInformation("Hackathon worker create!");
        }

        public async Task StartAsync(CancellationToken cancellationToken) {
            _logger.LogInformation("Hackathon worker start");
            TableReader tableReader = new TableReader();
            Employee[] teamLeads = tableReader.ReadEmployee("TeamLeads20.csv");
            Employee[] juniors = tableReader.ReadEmployee("Juniors20.csv");
            await Task.Run(() => HoldHackatons(cancellationToken, teamLeads, juniors));
        }

        private async void HoldHackatons(CancellationToken cancellationToken, Employee[] teamLeads, Employee[] juniors) {
            double total = 0;
            for (int i = 0; i < ITERATION_COUNT && !cancellationToken.IsCancellationRequested; i++) {
                _logger.LogInformation("Start hackaton 1!");
                IEnumerable<Wishlist> teamLeadWishlists = _hackathon.CreateWishlist(teamLeads, juniors);
                IEnumerable<Wishlist> juniorWishlists = _hackathon.CreateWishlist(juniors, teamLeads);
                IEnumerable<Team> teams = _hrManager.BuildTeams(teamLeads, juniors, teamLeadWishlists, juniorWishlists);
                _logger.LogInformation("Build teams 1!");
                double quality = _hrDirector.CalculateQuality(teams, teamLeadWishlists, juniorWishlists);
                _logger.LogInformation("Calculate quality 1!");
                Console.WriteLine(quality);
                total += quality;
            }
            Console.WriteLine(total / ITERATION_COUNT);
        }
    
        public async Task StopAsync(CancellationToken cancellationToken) {
            await Task.CompletedTask;
        }
    }

}