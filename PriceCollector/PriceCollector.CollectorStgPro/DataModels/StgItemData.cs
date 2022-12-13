using PriceCollector.Core.Enums;

namespace PriceCollector.CollectorStgPro.DataModels
{
    public class StgItemData
    {
        public string ProductId { get; set; }
        public string Article { get; set; }
        public string Orders { get; set; }
        public string Stock { get; set; }
        public string NetPrice { get; set; }

        public string LogString(Separator separator)
        {
            string separationStr;

            switch (separator)
            {
                case Separator.Comma:
                    separationStr = ",";
                    break;
                case Separator.Dot:
                    separationStr = ".";
                    break;
                case Separator.Semicolon:
                    separationStr = ";";
                    break;
                case Separator.Colon:
                    separationStr = ":";
                    break;
                case Separator.Space:
                    separationStr = " ";
                    break;
                default:
                    separationStr = ";";
                    break;
            }

            return $"{ProductId}{separationStr} {Article}{separationStr} {Orders}{separationStr} {Stock}{separationStr} {NetPrice}";
        }
    }
}
