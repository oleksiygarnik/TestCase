using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using TestCase.Application.Transactions.Commands.ImportTransactions.CsvParser.Converters;
using TestCase.Domain;

namespace TestCase.Application.Transactions.Commands.ImportTransactions.CsvParser
{
    public class TransactionMap : ClassMap<Transaction>
    {
        public TransactionMap()
        {
            Map(t => t.Id).Name("TransactionId");
            Map(t => t.Status).Name("Status");
            Map(t => t.Type).Name("Type");
            Map(t => t.ClientName).Name("ClientName");
            Map(t => t.Amount).Name("Amount").TypeConverter<AmountConverter>();
        }
    }
}
