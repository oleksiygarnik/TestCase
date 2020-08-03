using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace TestCase.Application.Transactions.Commands.ImportTransactions.CsvParser.Converters
{
    public class AmountConverter : TypeConverter
    {
        private readonly CultureInfo _culture;
        private readonly NumberStyles _styles;

        public AmountConverter()
        {
            _styles = NumberStyles.Number | NumberStyles.AllowCurrencySymbol;
            _culture = new CultureInfo("en-US");
        }

        public AmountConverter(CultureInfo culture, NumberStyles styles)
        {
            _styles = styles;
            _culture = culture;
        }
        public override object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
        {
            if (!decimal.TryParse(text, _styles, _culture, out var result))
                throw new InvalidCastException(nameof(text));

            return result;
        }
    }
}
