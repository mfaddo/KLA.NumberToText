using KLA.NumberToText.Helpers;

namespace KLA.NumberToText.NumberConverter.UnitsFactory
{
    // Factory class for creating unit getters based on the number of subsets
    public static class UnitFactory
    {
        // Method to create an instance of IUnitGetter based on the number of subsets and subset content
        public static IUnitGetter FactorUnit(int numberOfSubsets, string subset)
        {
            // Check if the subset is empty
            if (subset == Constants.EMPTY_SUBSET)
                return new NoUnit(); // If subset is empty, return NoUnit

            // Switch statement to determine the appropriate unit getter based on the number of subsets
            return numberOfSubsets switch
            {
                1 => new NoUnit(), // If there is only one subset, return NoUnit
                2 => new ThousandUnit(), // If there are two subsets, return ThousandUnit
                3 => new MillionUnit(), // If there are three subsets, return MillionUnit
                _ => throw new NotImplementedException("System can not calcuate for more than milion") // Unsupported number of subsets
            };


        }
    }
}
