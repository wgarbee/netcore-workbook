using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using ToDoApp.Models;

namespace ToDoApp.Data
{
    public class ToDoContext : DbContext, IReadOnlyToDoContext
    {
        public ToDoContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ToDo>().HasKey(x => x.Id).ForSqlServerIsClustered();
            modelBuilder.Entity<ToDo>().Property(x => x.Id).UseSqlServerIdentityColumn();
            modelBuilder.Entity<ToDo>().HasOne(x => x.Status).WithMany(x => x.ToDos).HasForeignKey(x => x.StatusId);
            modelBuilder.Entity<ToDo>().HasIndex(x => x.StatusId).HasName($"IX_{nameof(ToDo)}_{nameof(ToDo.Status)}");

            modelBuilder.Entity<Status>().HasMany(x => x.ToDos).WithOne(x => x.Status);
        }

        public DbSet<ToDo> ToDos { get; set; }

        public DbSet<Status> Statuses { get; set; }

        IQueryable<ToDo> IReadOnlyToDoContext.ToDos { get => ToDos.AsNoTracking(); }

        IQueryable<Status> IReadOnlyToDoContext.Statuses { get => Statuses.AsNoTracking(); }
    }
}
