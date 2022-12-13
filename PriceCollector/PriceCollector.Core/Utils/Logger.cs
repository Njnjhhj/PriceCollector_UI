using log4net;
using log4net.Config;

namespace PriceCollector.Core.Utils
{
    public static class Logger
    {
        public static readonly ILog logger = LogManager.GetLogger(typeof(Logger));
    }
}
