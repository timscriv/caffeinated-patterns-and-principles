using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

/// <summary>
/// This is contained in the Clorox Kernel, pulled out for reference, do not copy.
/// </summary>
namespace SqlQueueManager
{
    public interface IScheduledJob
    {
        IJobTrigger Trigger { get; }
        Task RunAsync(CancellationToken cancellationToken);
    }

    public class ScopedService<T> : BackgroundService where T : IScheduledJob
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<ScopedService<T>> _logger;

        public ScopedService(IServiceProvider serviceProvider, ILogger<ScopedService<T>> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await Task.Yield();

            while (!stoppingToken.IsCancellationRequested)
            {
                using var scope = _serviceProvider.CreateScope();
                var service = ActivatorUtilities.CreateInstance<T>(scope.ServiceProvider);
                try
                {
                    await service.RunAsync(stoppingToken);
                }
                catch (Exception ex)
                {
                    _logger.LogCritical(ex, "Uncaught exception in background service");
                }
                finally
                {
                    await service.Trigger.DelayUntil();
                }
            }
        }
    }

    public interface IJobTrigger
    {
        Task DelayUntil();
    }

    public class DelayJobTrigger : IJobTrigger
    {
        public DelayJobTrigger(TimeSpan timeSpan)
        {
            TimeSpan = timeSpan;
        }
        public TimeSpan TimeSpan { get; }

        public async Task DelayUntil()
        {
            await Task.Delay(TimeSpan);
        }

        public static DelayJobTrigger FromSeconds(double seconds) => new DelayJobTrigger(TimeSpan.FromSeconds(seconds));
        public static DelayJobTrigger FromMinutes(double minutes) => new DelayJobTrigger(TimeSpan.FromMinutes(minutes));
    }
}
