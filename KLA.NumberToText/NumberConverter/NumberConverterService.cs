using KLA.NumberToText.Exceptions;
using KLA.NumberToText.Helpers;
using KLA.NumberToText.NumberConverter.UnitsFactory;
using System.Text;

namespace KLA.NumberToText.NumberConverter
{
    public class NumberConverterService : INumberConverterService
    {
        private readonly IStaticNumbersService _staticDataService;

        public NumberConverterService(IStaticNumbersService staticDataService)
        {
            _staticDataService = staticDataService;
        }

        public string Convert(string number)
        {
            if (!number.IsValidForConvert())
                throw new NumberToTextException("Invalid number format");

            var numberParts = number.Split(new char[] { ',' });

            string dollarsText = handleDollars(numberParts[0]);
            string centsText = handleCents(numberParts.Length > 1 ?
                numberParts[1] : "");

            return CombineDollarsAndCents(dollarsText, centsText);

            
        }

        #region Main Handler
        private string handleDollars(string dollars)
        {
            StringBuilder result = new StringBuilder();
            var subsets = dollars.SplitAndReverseString(3).ToList();
            int i = 0;
            foreach (var subset in subsets)
            {
                result.Append(getHundreds(subset));
                result.Append(getTens(subset.Substring(1, 2)));
                if (result.Length > 0 && result[result.Length - 1] != ' ')
                {
                    result.Append(" ");
                }

                result.Append(getUnit(i, subsets.Count, subset));
                i++;
            }
            return FormatDollarsText(result.ToString());
        }

        private string handleCents(string cents)
        {

            if (string.IsNullOrEmpty(cents))
                return string.Empty;

            var subset = cents;
            if (cents.Length == 1)
                subset += Constants.ZERO_NUMBER;

            StringBuilder builder = new StringBuilder();
            builder.Append(getTens(subset));
            return FormatCentsText(builder.ToString());
        }
        #endregion

        #region Helper Methods For Number conversion
        private string getHundreds(string subset)
        {
            string number = _staticDataService.Numbers[subset.Substring(0, 1)];
            return string.IsNullOrEmpty(number) ? string.Empty
                : $"{number} {Constants.HUNDRED} ";

        }
        private string getTens(string number)
        {

            string tens = number.Substring(0, 1);
            string ones = number.Substring(1, 1);

            if (tens == Constants.ONE_NUMBER)
                return _staticDataService.NumbersInTeenFormat[ones];
            if (tens == Constants.ZERO_NUMBER)
                return _staticDataService.Numbers[ones];

            StringBuilder builder = new StringBuilder();

            builder.Append(_staticDataService.NumbersInTyFormat[tens]);
            if (ones != Constants.ZERO_NUMBER)
                builder.Append(Constants.DASH);
            builder.Append(_staticDataService.Numbers[ones]);

            return builder.ToString();


        }
        private string getUnit(int indexOfSubset, int numberOfSubsets, string subset)
        {
            IUnitGetter unitGetter = UnitFactory.FactorUnit(numberOfSubsets, subset);

            return unitGetter.GetUnit(indexOfSubset);
        }
        #endregion

        #region Text Combinators 
        private string CombineDollarsAndCents(string dollarsText, string centsText)
        {
            if (string.IsNullOrEmpty(centsText))
                return dollarsText;

            return $"{dollarsText} {Constants.AND} {centsText}";
        }

        private string FormatDollarsText(string dollarsText)
        {
            if (string.IsNullOrWhiteSpace(dollarsText.Trim()))
                return $"{Constants.ZERO_TEXT} {Constants.DOLLAR}";
            if (dollarsText.Trim() == Constants.ONE_TEXT)
                return $"{dollarsText} {Constants.DOLLAR}";
            return $"{dollarsText} {Constants.DOLLARS}";
        }
        private string FormatCentsText(string centsText)
        {
            if (string.IsNullOrWhiteSpace(centsText.Trim()))
                return string.Empty;
            if (centsText.Trim() == Constants.ONE_TEXT)
                return $"{centsText} {Constants.CENT}";
            return $"{centsText} {Constants.CENTS}";
        }
        #endregion


    }
}
