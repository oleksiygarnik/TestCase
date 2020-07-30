using Bogus;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TestCase.Domain;

namespace TestCase.Infrastructure
{
    public static class ModelBuilderExtensions
    {
        private const int ENTITY_COUNT = 5;

        public static void Seed(this ModelBuilder modelBuilder)
        {
            var transactions = GenerateRandomTransactions();

            modelBuilder.Entity<Transaction>().HasData(transactions);
        }

        public static ICollection<Transaction> GenerateRandomTransactions()
        {
            int transactionId = 1;

            var teamsFake = new Faker<Transaction>()
                .RuleFor(team => team.Id, f => transactionId++)
                .RuleFor(team => team.ClientName, f => f.Random.Word())
                .RuleFor(team => team.CreatedAt, f => DateTime.Now);

            return teamsFake.Generate(ENTITY_COUNT);
        }

    }
}