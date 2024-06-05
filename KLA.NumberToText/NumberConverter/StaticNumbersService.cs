using KLA.NumberToText.Helpers;
using System.Collections.ObjectModel;
using System.Xml.Linq;

namespace KLA.NumberToText.NumberConverter
{
    public class StaticNumbersService : IStaticNumbersService
    {
        private static readonly Dictionary<string, string> _numbers = new Dictionary<string, string>
            {
                { "0", "" },
                { "1", Constants.ONE_TEXT},
                { "2", Constants.TWO_TEXT},
                { "3",Constants.THREE_TEXT },
                { "4", Constants.FOUR_TEXT },
                { "5", Constants.FIVE_TEXT },
                { "6", Constants.SIX_TEXT },
                { "7", Constants.SEVEN_TEXT},
                { "8", Constants.EIGHT_TEXT },
                { "9", Constants.NINE_TEXT },
            };
        private static readonly Dictionary<string, string> _numbersInTyFormat = new Dictionary<string, string>
            {
                { "0", "" },
                { "1", Constants.TEN_TEXT},
                { "2", Constants.TWENTY_TEXT},
                { "3",Constants.THIRTY_TEXT },
                { "4", Constants.FORTY_TEXT },
                { "5", Constants.FIFTY_TEXT },
                { "6", Constants.SIXTY_TEXT },
                { "7", Constants.SEVENTY_TEXT},
                { "8", Constants.EIGHTY_TEXT },
                { "9", Constants.NINETY_TEXT },
            };

        private static readonly Dictionary<string, string> _numbersInTeenFormat = new Dictionary<string, string>
            {
                { "0", Constants.TEN_TEXT },
                { "1", Constants.ELEVEN_TEXT},
                { "2", Constants.TWELVE_TEXT},
                { "3",Constants.THIRTEEN_TEXT },
                { "4", Constants.FOURTEEN_TEXT },
                { "5", Constants.FIFTEEN_TEXT },
                { "6", Constants.SIXTEEN_TEXT },
                { "7", Constants.SEVENTEEN_TEXT},
                { "8", Constants.EIGHTEEN_TEXT },
            { "9", Constants.NINETEEN_TEXT },
        };

        public IReadOnlyDictionary<string, string> Numbers { get; } = new ReadOnlyDictionary<string, string>(_numbers);
        public IReadOnlyDictionary<string, string> NumbersInTyFormat { get; } = new ReadOnlyDictionary<string, string>(_numbersInTyFormat);
        public IReadOnlyDictionary<string, string> NumbersInTeenFormat { get; } = new ReadOnlyDictionary<string, string>(_numbersInTeenFormat);
    }
}
