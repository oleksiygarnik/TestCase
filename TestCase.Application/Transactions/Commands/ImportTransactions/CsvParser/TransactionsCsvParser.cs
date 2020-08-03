using CsvHelper;
using Domain;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using TestCase.Domain;

namespace TestCase.Application.Transactions.Commands.ImportTransactions.CsvParser
{
    public class TransactionsCsvParser : CsvParserProvider<Transaction>
    {
        public TransactionsCsvParser(IFormFile fileInfo) : base(fileInfo)
        {
        }

        protected override List<Transaction> ReadCsvFile()
        {
            try
            {
                using (var reader = new StreamReader(_file.OpenReadStream()))
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    csv.Configuration.RegisterClassMap<TransactionMap>();
                    var records = csv.GetRecords<Transaction>().ToList();
                    return records;
                }
            }
            catch (UnauthorizedAccessException e)
            {
                throw new Exception(e.Message);
            }
            catch (FieldValidationException e)
            {
                throw new Exception(e.Message);
            }
            catch (CsvHelperException e)
            {
                throw new Exception(e.Message);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
