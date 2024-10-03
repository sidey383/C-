namespace Hackathon;

public class HackathonWorker : IHostedService {

    public HackathonWorker() {
         Console.WriteLine("Hackathon worker create!");
    }

    public Task StartAsync(CancellationToken cancellationToken) {
        Console.WriteLine("Hackathon worker start async");
        DoSomeWorkEveryFiveSecondsAsync(cancellationToken);
        return Task.CompletedTask;
    }

        private async Task DoSomeWorkEveryFiveSecondsAsync(CancellationToken stoppingToken)
    {
        int i = 0;
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                Thread.Sleep(100);
                Console.WriteLine("Run " + i);
            }
            catch (Exception ex)
            {
                // обработка ошибки однократного неуспешного выполнения фоновой задачи
            }
 
            await Task.Delay(5000, stoppingToken);
        }
    }

    public Task StopAsync(CancellationToken cancellationToken) {
         return Task.CompletedTask;
    }

}