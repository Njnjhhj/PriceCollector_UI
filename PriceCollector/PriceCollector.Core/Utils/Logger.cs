using log4net;

namespace PriceCollector.Core.Utils
{
    public static class Logger
    {
        public static readonly ILog logger = LogManager.GetLogger(typeof(Logger));
    }
}
