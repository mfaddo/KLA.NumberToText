using System.Text.RegularExpressions;

namespace KLA.NumberToText.Helpers
{
    public static class StringExtensions
    {
        public static IEnumerable<string> SplitAndReverseString(this string input, int subsetSize)
        {
            if (string.IsNullOrEmpty(input) || subsetSize <= 0)
            {
                throw new ArgumentException("Input string must not be null or empty, and subset size must be greater than zero.");
            }

            // Reverse the input string
            string reversedInput = new string(input.Reverse().ToArray());

            // Split the reversed string into subsets
            var subsets = Enumerable.Range(0, (reversedInput.Length + subsetSize - 1) / subsetSize)
                                .Select(i => new string(reversedInput.Skip(i * subsetSize).Take(subsetSize).Reverse().ToArray()))
                                .Select(subset => subset.Length < subsetSize ? subset.PadLeft(subsetSize, '0') : subset);

            // Reverse the result to match the required order
            return subsets.Reverse();
        }
        public static bool IsValidForConvert(this string input)
        {
            string pattern = @"^(?!0\d)\d{1,9}(\,\d{1,2})?$";
            Regex regex = new Regex(pattern);
            return regex.IsMatch(input);
        }
    }
}
