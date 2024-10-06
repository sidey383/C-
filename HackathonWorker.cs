using System.Threading.Tasks.Dataflow;
using Nsu.HackathonProblem.Contracts;

namespace Nsu.HackathonProblem {

    public class HackathonWorker : IHostedService {
            private static readonly int ITERATION_COUNT = 1000;
            private readonly IHostApplicationLifetime _appLifetime;
            private readonly IConfiguration _configuration;
            private readonly ILogger<HackathonWorker> _logger;
            private readonly Hackathon _hackathon;
            private readonly HrManager _hrManager;
            private readonly HrDirector _hrDirector;
            private readonly CancellationTokenSource cts = new CancellationTokenSource();
            private Task? mainTask;

        public HackathonWorker(
            IHostApplicationLifetime appLifetime,
            IConfiguration configuration,
            ILogger<HackathonWorker> logger,
            Hackathon hackathon,
            HrManager hrManager,
            HrDirector hrDirector
        ) {
            this._appLifetime = appLifetime;
            this._logger = logger;
            this._hackathon = hackathon;
            this._hrDirector = hrDirector;
            this._hrManager = hrManager;
            this._configuration = configuration;
        }

        public async Task StartAsync(CancellationToken cancellationToken) {
            _logger.LogInformation("Hackathon worker start async");
            TableReader tableReader = new TableReader();
            string? teamleadsCSV = _configuration["Teamleads"];
            string? juniorsCSV = _configuration["Juniors"];
            if (teamleadsCSV is null)
                throw new InvalidOperationException("Can't found 'TeamLeads' in configuration");
            if (juniorsCSV is null)
                throw new InvalidOperationException("Can't found 'Juniors' in configuration");
            Employee[] teamLeads = tableReader.ReadEmployee(teamleadsCSV);
            Employee[] juniors = tableReader.ReadEmployee(juniorsCSV);
            mainTask = Task.Run(() => HoldHackatons(cts.Token, teamLeads, juniors), cts.Token);
            await Task.CompletedTask;
        }

        private void HoldHackatons(CancellationToken cancellationToken, Employee[] teamLeads, Employee[] juniors) {
             _logger.LogInformation("Hackathon worker task start");
             double total = 0;
            for (int i = 0; i < ITERATION_COUNT && !cancellationToken.IsCancellationRequested; i++) {
                _logger.LogDebug("Start hackaton {}", i);
                IEnumerable<Wishlist> teamLeadWishlists = _hackathon.CreateWishlist(teamLeads, juniors);
                IEnumerable<Wishlist> juniorWishlists = _hackathon.CreateWishlist(juniors, teamLeads);
                IEnumerable<Team> teams = _hrManager.BuildTeams(teamLeads, juniors, teamLeadWishlists, juniorWishlists);
                _logger.LogDebug("Build teams {}", i);
                double quality = _hrDirector.CalculateQuality(teams, teamLeadWishlists, juniorWishlists);
                _logger.LogDebug("Calculate quality {}", i);
                Console.WriteLine(quality);
                total += quality;
            }
            if (!cancellationToken.IsCancellationRequested) {
                Console.WriteLine("Total average {0}", total / ITERATION_COUNT);
            }
            _appLifetime.StopApplication();
            _logger.LogInformation("Hackathon worker task complete");
        }
    
        public async Task StopAsync(CancellationToken cancellationToken) {
            _logger.LogInformation("Hackathon worker stoped");
            cts.Cancel();
            if (mainTask != null) 
                await mainTask;
        }
    }

}