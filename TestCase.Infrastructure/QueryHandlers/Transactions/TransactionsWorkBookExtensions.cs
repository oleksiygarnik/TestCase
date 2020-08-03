using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestCase.Domain;

namespace TestCase.Infrastructure.QueryHandlers.Transactions
{
    public static class ListTransactionsXLSBuilderExtensions
    {
        private const string WORKSHEET_NAME = "Transactions"; 
        public static XLWorkbook BuildXLS(this List<Transaction> transactions)
        {
            var workbook = new XLWorkbook();

            IXLWorksheet worksheet = workbook.Worksheets.Add(WORKSHEET_NAME);

            worksheet.Cell(1, 1).Value = "Id";
            worksheet.Cell(1, 2).Value = "Status";
            worksheet.Cell(1, 3).Value = "Type";
            worksheet.Cell(1, 4).Value = "ClientName";
            worksheet.Cell(1, 5).Value = "Amount";

            for (int index = 1; index <= transactions.Count(); index++)
            {
                worksheet.Cell(index + 1, 1).Value = transactions.ToList()[index - 1].Id;
                worksheet.Cell(index + 1, 2).Value = transactions.ToList()[index - 1].Status;
                worksheet.Cell(index + 1, 3).Value = transactions.ToList()[index - 1].Type;
                worksheet.Cell(index + 1, 4).Value = transactions.ToList()[index - 1].ClientName;
                worksheet.Cell(index + 1, 5).Value = transactions.ToList()[index - 1].Amount;
            }

            return workbook;
        }
    }
}
