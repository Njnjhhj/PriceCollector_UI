using System.Collections.Generic;
using NUnit.Framework;
using PriceCollector.Collector.Configuration;
using PriceCollector.Core.Utils;

namespace PriceCollector.Collector.Base
{
    [TestFixture]
    public class BaseCollectorConfigs
    {
        private const string Environment = "Websites";
        protected readonly JsonReader JsonReader = new();
        protected List<Websites> Websites;


        [OneTimeSetUp]
        public void BeforeAll()
        {
            Websites = JsonReader.GetFileContentList<Websites>(Environment, "Websites");
        }
    }
}
