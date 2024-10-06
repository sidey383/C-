using Nsu.HackathonProblem;

using Nsu.HackathonProblem.Contracts;

var host = Host.CreateDefaultBuilder(args) 

        .ConfigureServices((hostContext, services) => 
	{ 
            services.AddHostedService<HackathonWorker>(); 
            services.AddTransient<Hackathon>(_ => new Hackathon(new System.Random(20))); 
            services.AddTransient<ITeamBuildingStrategy, SimpleTeamBuildingStrategy>(); 
            services.AddTransient<HrManager>();
            services.AddTransient<HrDirector>(); 

  	}).Build(); 

host.Run(); 
