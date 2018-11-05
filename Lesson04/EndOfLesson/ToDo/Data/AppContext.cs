using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using ToDoApp.Models;

namespace ToDoApp.Data
{
    public class AppContext : DbContext
    {
        public AppContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ToDo>().HasKey(x => x.Id);
            modelBuilder.Entity<ToDo>().HasOne(x => x.Status).WithMany(x => x.ToDos).HasForeignKey(x => x.StatusId);

            modelBuilder.Entity<Status>().HasMany(x => x.ToDos).WithOne(x => x.Status);
        }

        public DbSet<ToDo> ToDos { get; set; }

        public DbSet<Status> Statuses { get; set; }
    }
}
