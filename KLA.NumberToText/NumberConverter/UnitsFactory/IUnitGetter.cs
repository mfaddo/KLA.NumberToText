namespace KLA.NumberToText.NumberConverter.UnitsFactory
{
    // Interface for getting the unit of a subset
    public interface IUnitGetter
    {
        // Method to get the unit based on the index of the subset
        string GetUnit(int indexOfSubset);
    }
}
