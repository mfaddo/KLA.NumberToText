using KLA.NumberToText.Exceptions;
using KLA.NumberToText.Helpers;
using KLA.NumberToText.NumberConverter.UnitsFactory;
using System.Text;

namespace KLA.NumberToText.NumberConverter
{
    public class NumberConverterService : INumberConverterService
    {
        private readonly IStaticNumbersService _staticDataService;

        // Constructor to inject the dependency for static numbers service
        public NumberConverterService(IStaticNumbersService staticDataService)
        {
            _staticDataService = staticDataService;
        }

        // Convert method to convert a number into its textual representation
        public string Convert(string number)
        {
            // Validate the number format
            if (!number.IsValidForConvert())
                throw new NumberToTextException("Invalid number format");

            // Split the number into dollars and cents
            var numberParts = number.Split(new char[] { ',' });

            // Convert the dollars and cents parts into text representation
            string dollarsText = handleDollars(numberParts[0]);
            string centsText = handleCents(numberParts.Length > 1 ?
                numberParts[1] : "");

            // Combine dollars and cents texts with "and" if necessary
            return CombineDollarsAndCents(dollarsText, centsText);

            
        }

        #region Main Handler

        // Private method to handle the conversion of dollars part
        private string handleDollars(string dollars)
        {
            StringBuilder result = new StringBuilder();
            var subsets = dollars.SplitAndReverseString(3).ToList();
            int i = 0;
            foreach (var subset in subsets)
            {
                // Append the hundreds part
                result.Append(getHundreds(subset));
                // Append the tens part
                result.Append(getTens(subset.Substring(1, 2)));
                // Add space if necessary
                if (result.Length > 0 && result[result.Length - 1] != ' ')
                {
                    result.Append(" ");
                }
                // Append the unit part
                result.Append(getUnit(i, subsets.Count, subset));
                i++;
            }
            // Format and return the dollars text
            return FormatDollarsText(result.ToString());
        }

        // Private method to handle the conversion of cents part
        private string handleCents(string cents)
        {

            if (string.IsNullOrEmpty(cents))
                return string.Empty;

            var subset = cents;
            if (cents.Length == 1)
                subset += Constants.ZERO_NUMBER;

            StringBuilder builder = new StringBuilder();
            builder.Append(getTens(subset));
            
            // Format and return the cents text

            return FormatCentsText(builder.ToString());
        }
        #endregion


        #region Helper Methods For Number conversion

        // Private method to get the hundreds part
        private string getHundreds(string subset)
        {
            string number = _staticDataService.Numbers[subset.Substring(0, 1)];
            // Return empty string if no hundreds are found
            return string.IsNullOrEmpty(number) ? string.Empty
                : $"{number} {Constants.HUNDRED} ";

        }

        // Private method to get the tens part
        private string getTens(string number)
        {

            string tens = number.Substring(0, 1);
            string ones = number.Substring(1, 1);

            // Handle special cases for teens and zero
            if (tens == Constants.ONE_NUMBER)
                return _staticDataService.NumbersInTeenFormat[ones];
            if (tens == Constants.ZERO_NUMBER)
                return _staticDataService.Numbers[ones];

            StringBuilder builder = new StringBuilder();

            builder.Append(_staticDataService.NumbersInTyFormat[tens]);
            // Append dash if necessary
            if (ones != Constants.ZERO_NUMBER)
                builder.Append(Constants.DASH);
            builder.Append(_staticDataService.Numbers[ones]);

            return builder.ToString();


        }
        // Private method to get the unit part
        private string getUnit(int indexOfSubset, int numberOfSubsets, string subset)
        {
            IUnitGetter unitGetter = UnitFactory.FactorUnit(numberOfSubsets, subset);
            // Return the unit based on the index and number of subsets
            return unitGetter.GetUnit(indexOfSubset);
        }
        #endregion

        #region Text Combinators 

        // Private method to combine dollars and cents with "and"
        private string CombineDollarsAndCents(string dollarsText, string centsText)
        {
            if (string.IsNullOrEmpty(centsText))
                return dollarsText;

            return $"{dollarsText} {Constants.AND} {centsText}";
        }

        // Private method to format dollars text
        private string FormatDollarsText(string dollarsText)
        {
            if (string.IsNullOrWhiteSpace(dollarsText.Trim()))
                return $"{Constants.ZERO_TEXT} {Constants.DOLLAR}";
            if (dollarsText.Trim() == Constants.ONE_TEXT)
                return $"{dollarsText}{Constants.DOLLAR}";
            return $"{dollarsText}{Constants.DOLLARS}";
        }

        // Private method to format cents text
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
