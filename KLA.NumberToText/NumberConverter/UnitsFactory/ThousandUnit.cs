using KLA.NumberToText.Helpers;

namespace KLA.NumberToText.NumberConverter.UnitsFactory
{
    // Class for representing the unit for thousands
    public class ThousandUnit : IUnitGetter
    {
        // Method implementation to get the unit for thousands
        public string GetUnit(int indexOfSubset)
              =>
            // Switch statement to determine the unit based on the index
            indexOfSubset switch
            {
                0 => $"{Constants.THOUSAND} ", // First subset represents thousands
                1 => string.Empty, // Second subset represents no units, so empty string is returned
                _ => throw new
                ArgumentOutOfRangeException(nameof(indexOfSubset))   // Invalid index
            };

    }
}
