using System;
using System.Collections.Generic;
using System.Text;
using TestCase.Domain;

namespace TestCase.Application.Transactions.Queries
{
    public class TransactionDto
    {
        public int Id { get; set; }

        public TransactionStatus Status { get; set; }

        public TransactionType Type { get; set; }

        public string ClientName { get; set; }

        public decimal Amount { get; set; }
    }
}
