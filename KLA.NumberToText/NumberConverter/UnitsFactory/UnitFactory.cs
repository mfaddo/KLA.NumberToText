using KLA.NumberToText.Helpers;

namespace KLA.NumberToText.NumberConverter.UnitsFactory
{
    public static class UnitFactory
    {
        public static IUnitGetter FactorUnit(int numberOfSubsets, string subset)
        {
            if (subset == Constants.EMPTY_SUBSET)
                return new NoUnit();

            return numberOfSubsets switch
            {
                1 => new NoUnit(),
                2 => new ThousandUnit(),
                3 => new MillionUnit(),
                _ => throw new NotImplementedException("System can not calcuate for more than milion")
            };


        }
    }
}
