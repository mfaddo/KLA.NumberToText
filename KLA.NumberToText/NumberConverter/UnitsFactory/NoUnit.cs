namespace KLA.NumberToText.NumberConverter.UnitsFactory
{
    // Class for representing no unit
    public class NoUnit : IUnitGetter
    {
        // Method implementation to return empty string for no unit

        public string GetUnit
            (int indexOfSubset)
            => string.Empty; // No unit, so empty string is returned

    }
}
