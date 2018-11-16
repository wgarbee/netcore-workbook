using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using ToDoApp.Areas.Tags.Models;

namespace ToDoApp.Areas.Tags.Data
{
    public class TagContext : DbContext
    {
        public TagContext(DbContextOptions<TagContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("Tags");

            modelBuilder
                .Entity<Tag>()
                .HasKey(x => x.Id)
                .ForSqlServerIsClustered();
            modelBuilder
                .Entity<Tag>()
                .Property(x => x.Id)
                .UseSqlServerIdentityColumn();
            modelBuilder
                .Entity<Tag>()
                .Property(x => x.Value)
                .IsRequired();
            modelBuilder
                .Entity<Tag>()
                .Property(x => x.TenantId)
                .IsRequired();
            modelBuilder
                .Entity<Tag>()
                .HasOne(x => x.Tenant)
                .WithMany(x => x.Tags)
                .HasForeignKey(x => x.TenantId);
            modelBuilder
                .Entity<Tag>()
                .HasIndex(x => new { x.TenantId, x.Value })
                .IsUnique()
                .HasName($"IX_{nameof(Tag)}_{nameof(Tag.TenantId)}_{nameof(Tag.Value)}");

            modelBuilder
                .Entity<Tenant>()
                .HasKey(x => x.Id)
                .ForSqlServerIsClustered();
            modelBuilder
                .Entity<Tenant>()
                .Property(x => x.Id)
                .UseSqlServerIdentityColumn();
            modelBuilder
                .Entity<Tenant>()
                .Property(x => x.Name)
                .IsRequired();
            modelBuilder
                .Entity<Tenant>()
                .HasIndex(x => x.Name)
                .IsUnique()
                .HasName($"IX_{nameof(Tenant)}_{nameof(Tenant.Name)}");
        }

        public DbSet<Tag> Tags { get; set; }

        public DbSet<Tenant> Tenants { get; set; }
    }
}
