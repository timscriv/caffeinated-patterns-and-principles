using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SqlQueueManager
{
    public class QueueDbContext : DbContext
    {
        public QueueDbContext(DbContextOptions<QueueDbContext> options)
        : base(options)
            { }

        public DbSet<QueueItem> QueueItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //sqllite configure autoint
            modelBuilder.Entity<QueueItem>()
                .Property(e => e.Id)
                .ValueGeneratedOnAdd();
        }
    }

    public class QueueItem
    {
        //Autoint
        public int Id { get; set; }

        //This could be the data of the queue, or just a pointer id for something in the queue.
        public int DataId { get; set; }
        public DateTime LockedUntilUtc { get; set; }
        public int Attempts { get; set; }

    }
}
