# SQL Queue Manager

This is a simple queue using a single SQL table that allows graceful failure if there is a failure in processing in the middle, using a LockUntil column that will allow it to be reprocessed if the lock is not extended or the queue item is popped from the Queue.

# Structure

- Program.cs
  - Registers [HostedServices](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/host/hosted-services) to run background tasks.
    - **SqlQueueAdder** - Simulating adding items to the queue.
    - **SqlQueueManager** - A ScheduledJob that will run on a trigger delay for processing items off the queue. **This is core of the project.**
- ScopedService.cs
  - Wrapper for BackgroundServices to add in ServiceProvider scopes and givs a trigger when to re-fire the service/job again. **This is in the Clorox Kernel**
- QueueDbContext.cs
  - Basic EntityFramework context that contains a table that acts as the queue.
- queue.db
  - sqlite DB that holds the queue table.
