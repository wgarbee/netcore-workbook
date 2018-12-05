using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using BankWithUs.Models;
using System.Threading;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace BankWithUs.Data
{
    public class BankContext : DbContext, IBankContext
    {
        public BankContext(DbContextOptions<BankContext> options) : base(options)
        {
        }
        public override EntityEntry Update( object entity)
        {
            return base.Update(entity);
        }
        public override EntityEntry<TEntity> Update<TEntity>( TEntity entity)
        {
            return base.Update(entity);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=BankApp;Trusted_Connection=True;MultipleActiveResultSets=true");
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return base.SaveChangesAsync(cancellationToken);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
        {
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        public void AddEntity<TEntity>(TEntity entity) where TEntity: class
        {
            base.Add(entity);
        }
        
        public void Remove(Account account)
        {
            Accounts.Remove(account);
        }

        public override EntityEntry Add( object entity)
        {
            return base.Add(entity);
        }

        public DbSet<BankWithUs.Models.Account> Accounts { get; set; }
        IQueryable<BankWithUs.Models.Account> IBankContext.Accounts { get => this.Accounts; }
    }
}
