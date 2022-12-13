using System;
using System.Collections.Generic;
using OpenQA.Selenium;
using PriceCollector.Collector.Flows;
using PriceCollector.Collector.Flows.Interfaces;

namespace PriceCollector.Collector.Dictionaries
{
    public static class PageFlowDictionary
    {
        public static Dictionary<string, Func<IWebDriver, IFlow>> PageFlowCreator => new()
        {
            { "Dmlights", driver => new DmlightsFlow(driver) },
            { "Tecshop", driver => new TecshopFlow(driver) },
            { "Webshop", driver => new WebshopFlow(driver) },
            { "Solyd", driver => new SolydFlow(driver) },
            { "Gigatek", driver => new GigatekFlow(driver) },
            { "Semmatec", driver => new SemmatecFlow(driver) },
            { "Groothandel", driver => new GroothandelFlow(driver) },
            { "Omnielectric", driver => new OmnielectricFlow(driver) },
            { "Zelektro", driver => new ZelektroFlow(driver) }
        };
    }
}
