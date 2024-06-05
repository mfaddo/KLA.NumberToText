namespace KLA.NumberToText.NumberConverter
{
    public interface IStaticNumbersService
    {
        IReadOnlyDictionary<string, string> Numbers { get; }
        IReadOnlyDictionary<string, string> NumbersInTyFormat { get; }
        IReadOnlyDictionary<string, string> NumbersInTeenFormat { get; }
    }
}
