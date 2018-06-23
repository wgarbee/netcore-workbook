using BaseProject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BaseProject.Data
{
    public class BaseIssueTrackerContext : DbContext
    {
        public BaseIssueTrackerContext(DbContextOptions<IssueTrackerContext> options) : base(options)
        {
        }

        public DbSet<Issue> Issues { get; set; }
        public DbSet<AppUser> Users { get; set; }
        public DbSet<IssueTask> IssueTasks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Issue>().ToTable(nameof(Issue));
            modelBuilder.Entity<AppUser>().ToTable(nameof(AppUser));
            modelBuilder.Entity<IssueTask>().ToTable(nameof(IssueTask));
        }
    }

    public class ReadonlyIssueTrackerContext : BaseIssueTrackerContext
    {
        public ReadonlyIssueTrackerContext(DbContextOptions<IssueTrackerContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        }
    }

    public class IssueTrackerContext : BaseIssueTrackerContext
    {
        public IssueTrackerContext(DbContextOptions<IssueTrackerContext> options) : base(options)
        {
        }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);
        //    modelBuilder.Entity<Issue>().HasQueryFilter(f => f.Status != Status.Done);
        //}
    }
}
