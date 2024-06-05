using KLA.NumberToText.Helpers;

namespace KLA.NumberToText.NumberConverter.UnitsFactory
{
    public class MillionUnit : IUnitGetter
    {
        public string GetUnit(int indexOfSubset)
        =>
            indexOfSubset switch
            {
                0 => $"{Constants.MILLION} ",
                1 => $"{Constants.THOUSAND} ",
                2 => string.Empty,
                _ => throw new
                ArgumentOutOfRangeException(nameof(indexOfSubset))
            };

    }
}
