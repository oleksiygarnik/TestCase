using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;

namespace Domain
{
    public abstract class CsvParserProvider<TRecord> 
    {
        protected readonly string _path;
        protected readonly IFormFile _file;

        protected List<TRecord> _records;

        public List<TRecord> Records => _records ?? (_records = ReadCsvFile());


        public CsvParserProvider(string path)
        {
            if (string.IsNullOrEmpty(path))
                throw new ArgumentException("message", nameof(path));

            _path = path;
        }

        public CsvParserProvider(IFormFile file)
        {
            if (file is null || Path.GetExtension(file.FileName) != ".csv")
                throw new ArgumentException(nameof(file));

            _file = file;
        }

        protected abstract List<TRecord> ReadCsvFile();
    }
}
