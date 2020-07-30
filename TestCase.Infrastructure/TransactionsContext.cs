using Microsoft.EntityFrameworkCore;
using System;
using TestCase.Domain;

namespace TestCase.Infrastructure
{
    public class TransactionsContext : DbContext
    {
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }

        public TransactionsContext(DbContextOptions<TransactionsContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Seed();

            base.OnModelCreating(modelBuilder);

        }
    }
}
