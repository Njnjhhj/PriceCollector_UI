using PriceCollector.Core.Enums;

namespace PriceCollector.Collector.DataModels
{
    public class ItemData
    {
        public string Id { get; set; }
        public string Availability { get; set; }
        public string Price { get; set; }

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

            return $"{Id}{separationStr} {Availability}{separationStr} {Price}";
        }
    }
}
