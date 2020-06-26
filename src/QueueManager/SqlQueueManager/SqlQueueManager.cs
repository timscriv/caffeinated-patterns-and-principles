using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SqlQueueManager
{
    public class SqlQueueManager : IScheduledJob
    {
        private readonly ILogger<SqlQueueManager> _logger;
        private readonly QueueDbContext _queueDbContext;

        public IJobTrigger Trigger => DelayJobTrigger.FromSeconds(1); //Run the Manager 1 second after previous finish

        public SqlQueueManager(ILogger<SqlQueueManager> logger, QueueDbContext queueDbContext)
        {
            _logger = logger;
            _queueDbContext = queueDbContext;
        }

        public async Task RunAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("SqlQueueManager:RunAsync");
            var queueItems = await PeekLockQueueItemsAsync(take: 10, maxAttempts: 5, TimeSpan.FromSeconds(15));

            foreach (var item in queueItems)
            {
                var failed = false;

                _logger.LogInformation($"- Process QueueItem({item.Id})");

                // faking a failure in processing 
                if (item.Id % 5 == 0)
                {
                    failed = true;
                    _logger.LogInformation($"- QueueItem({item.Id}) Failed");
                }

                if (!failed)
                {
                    await PopQueueItemAsync(item); //only pop after done processing
                }
            }
        }

        private async Task PopQueueItemAsync(QueueItem queueItem)
        {
            _queueDbContext.Remove(queueItem);

            await _queueDbContext.SaveChangesAsync();

        }

        private async Task<List<QueueItem>> PeekLockQueueItemsAsync(int take, int maxAttempts, TimeSpan lockTime)
        {
            //A thread-safe way of doing this would be a stored proc
            var queueItems = await _queueDbContext.QueueItems
                .Where(c => c.LockedUntilUtc < DateTime.UtcNow && c.Attempts <= maxAttempts)
                .OrderBy(c => c.Id)
                .Take(take)
                .ToListAsync(); //peek


            foreach (var item in queueItems)
            {
                item.LockedUntilUtc = DateTime.UtcNow.Add(lockTime); // lock
                item.Attempts += 1;
            }

            await _queueDbContext.SaveChangesAsync();

            return queueItems;
        }
    }


    /// <summary>
    /// This is just here to simulate something adding things to the queue.
    /// </summary>
    public class SqlQueueAdder : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;

        public SqlQueueAdder(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var randomDataGen = new Random();

                var scopedDbContext = scope.ServiceProvider.GetRequiredService<QueueDbContext>();
                for (int i = 0; i < 5; i++)
                {
                    scopedDbContext.Add(new QueueItem
                    {
                        DataId = randomDataGen.Next(1, 100000),
                        LockedUntilUtc = DateTime.UtcNow,
                    });
                }

                await scopedDbContext.SaveChangesAsync();
            }
        }
    }
}
