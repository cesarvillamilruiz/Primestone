using Data_Access.DataAccess;
using Logic_Layer.Bussiness;
using Logic_Layer.Helpers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Logic_Layer.Repositories
{
    public class TimedHostedService : IHostedService, IDisposable 
    {
        private Timer _timer;

        private readonly IServiceScopeFactory scopeFactory;

        public TimedHostedService(IServiceScopeFactory scopeFactory)
        {
            this.scopeFactory = scopeFactory;
        }

        public Task StartAsync(CancellationToken stoppingToken)
        {
            Log.Instance.Add(string.Format(ResponseStrings.Started));
            _timer = new Timer(DoWork, null, TimeSpan.Zero,
                TimeSpan.FromSeconds(10));

            return Task.CompletedTask;
        }

        private void DoWork(object state)
            {
            using (var scope = scopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<DataContext>();
                if (!HostedServiceExecution.Insert_New_DoWork(dbContext))
                {
                    
                    Log.Instance.Add("Host not Working");
                }
            }
        }

        public Task StopAsync(CancellationToken stoppingToken)
        {
            Log.Instance.Add(string.Format(ResponseStrings.Finished));
            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
