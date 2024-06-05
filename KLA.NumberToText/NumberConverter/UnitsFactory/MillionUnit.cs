using KLA.NumberToText.Helpers;

namespace KLA.NumberToText.NumberConverter.UnitsFactory
{
    // Class for representing the unit for millions
    public class MillionUnit : IUnitGetter
    {
        // Class for representing the unit for millions
        public string GetUnit(int indexOfSubset)
        =>
            indexOfSubset switch
            {
                0 => $"{Constants.MILLION} ", // First subset represents millions
                1 => $"{Constants.THOUSAND} ",// Second subset represents thousands
                2 => string.Empty, // Third subset represents no units, so empty string is returned
                _ => throw new
                ArgumentOutOfRangeException(nameof(indexOfSubset))  // Invalid index
            };

    }
}
