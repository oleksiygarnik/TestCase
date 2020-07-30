using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestCase.Domain;

namespace TestCase.Infrastructure.QueryHandlers.Transactions
{
    public static class QueryableFilterExtensions
    {
        public static IQueryable<Transaction> FilterByStatus(this IQueryable<Transaction> records, TransactionStatus? status)
        {
            return status.HasValue
                ? records.Where(r => r.Status == status)
                : records;
        }

        public static IQueryable<Transaction> FilterByType(this IQueryable<Transaction> records, TransactionType? type)
        {
            return type.HasValue
                ? records.Where(r => r.Type == type)
                : records;
        }
    }
}
