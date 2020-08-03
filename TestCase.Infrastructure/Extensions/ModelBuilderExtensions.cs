using Bogus;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using TestCase.Domain;
using TestCase.Infrastructure.EntityTypes;

namespace TestCase.Infrastructure
{
    public static class ModelBuilderExtensions
    {
        private const int ENTITY_COUNT = 10;

        public static void Configure(this ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new TransactionConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new RefreshTokenConfiguration());
        }

        public static void Seed(this ModelBuilder modelBuilder)
        {
            var transactions = GenerateRandomTransactions();
            var users = GenerateRandomUsers();

            modelBuilder.Entity<Transaction>().HasData(transactions);
            modelBuilder.Entity<User>().HasData(users);
        }

        public static ICollection<Transaction> GenerateRandomTransactions()
        {
            int transactionId = 1;

            var teamsFake = new Faker<Transaction>()
                .RuleFor(t => t.Id, f => transactionId++)
                .RuleFor(t => t.ClientName, f => f.Random.Word())
                .RuleFor(t => t.Amount, f => f.Random.Decimal())
                .RuleFor(t => t.Type, f => f.PickRandom<TransactionType>())
                .RuleFor(t => t.Status, f => f.PickRandom<TransactionStatus>());

            return teamsFake.Generate(ENTITY_COUNT);
        }


        public static ICollection<User> GenerateRandomUsers()
        {
            int userId = 1;

            var usersFake = new Faker<User>()
                .RuleFor(u => u.Id, f => userId++)
                .RuleFor(u => u.UserName, f => f.Person.UserName)
                .RuleFor(u => u.Email, f => f.Person.Email)
                .RuleFor(u => u.Password, f => f.Internet.Password());

            return usersFake.Generate(ENTITY_COUNT);
        }

    }
}