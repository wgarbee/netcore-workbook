using BaseProject.Data.Models;
using BaseProject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BaseProject.Data
{
    public class IssueTrackerContext : DbContext
    {
        public IssueTrackerContext(DbContextOptions<IssueTrackerContext> options) : base(options)
        {
        }

        public DbSet<Issue> Issues { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<IssueTask> IssueTasks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Issue>().ToTable(nameof(Issue));
            modelBuilder.Entity<User>().ToTable(nameof(User));
            modelBuilder.Entity<IssueTask>().ToTable(nameof(IssueTask));
        }
    }
}
