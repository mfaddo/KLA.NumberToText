namespace KLA.NumberToText.NumberConverter.UnitsFactory
{
    public class NoUnit : IUnitGetter
    {
        public string GetUnit
            (int indexOfSubset)
            => string.Empty;

    }
}
