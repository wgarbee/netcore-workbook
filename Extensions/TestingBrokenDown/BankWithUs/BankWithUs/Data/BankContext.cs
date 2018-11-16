using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using BankWithUs.Models;

namespace BankWithUs.Data
{
    public class BankContext : DbContext
    {
        public BankContext() : base()
        {
        }

        public BankContext(DbContextOptions<BankContext> options) : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=BankApp;Trusted_Connection=True;MultipleActiveResultSets=true");
        }

        public DbSet<BankWithUs.Models.Account> Accounts { get; set; }
    }
}
