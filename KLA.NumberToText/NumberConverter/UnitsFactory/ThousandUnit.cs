using KLA.NumberToText.Helpers;

namespace KLA.NumberToText.NumberConverter.UnitsFactory
{
    public class ThousandUnit : IUnitGetter
    {
        public string GetUnit(int indexOfSubset)
              =>
            indexOfSubset switch
            {
                0 => $"{Constants.THOUSAND} ",
                1 => string.Empty,
                _ => throw new
                ArgumentOutOfRangeException(nameof(indexOfSubset))
            };

    }
}
