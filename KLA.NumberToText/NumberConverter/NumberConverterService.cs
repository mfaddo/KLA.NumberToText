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

            var splitedNumber = number.Split(new char[] { ',' });

            string dollars = handleDollars(splitedNumber[0]);
            string cents = handleCents(splitedNumber.Length > 1 ?
                splitedNumber[1] : "");

            if (!string.IsNullOrEmpty(cents))
            {
                return $"{dollars} {Constants.AND} {cents}";
            }

            return dollars;
        }

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

            string stringResult = result.ToString();
            if (string.IsNullOrWhiteSpace(stringResult.Trim()))
                return $"{Constants.ZERO_TEXT} {Constants.DOLLAR}";
            if (stringResult.Trim() == Constants.ONE_TEXT)
                return $"{result}{Constants.DOLLAR}";
            return $"{result}{Constants.DOLLARS}";


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
            string stringResult = builder.ToString();
            if (string.IsNullOrEmpty(stringResult.Trim()))
                return string.Empty;
            if (stringResult.Trim() == Constants.ONE_TEXT)
                return $"{builder} {Constants.CENT}";
            return $"{builder} {Constants.CENTS}";
        }
        private string getHundreds(string subset)
        {
            string number = _staticDataService.Numbers[subset.Substring(0, 1)];
            if (!string.IsNullOrEmpty(number))
            {
                return $"{number} {Constants.HUNDRED} ";
            }
            return string.Empty;
        }
        private string getTens(string number)
        {
            StringBuilder builder = new StringBuilder();
            string tens = number.Substring(0, 1);
            string ones = number.Substring(1, 1);

            if (tens == Constants.ONE_NUMBER)
                return _staticDataService.NumbersInTeenFormat[ones];
            if (tens == Constants.ZERO_NUMBER)
                return _staticDataService.Numbers[ones];

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
    }
}
