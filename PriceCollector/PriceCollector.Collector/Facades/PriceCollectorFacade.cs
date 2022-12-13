using System;
using System.Collections.Generic;
using OpenQA.Selenium;
using PriceCollector.Collector.Base;
using PriceCollector.Collector.DataModels;
using PriceCollector.Collector.Flows;
using PriceCollector.Collector.Flows.Interfaces;
using PriceCollector.Core.Enums;

namespace PriceCollector.Collector.Facades
{
    public class PriceCollectorFacade : BaseCollectorUi
    {
        public void SinglePageTestMethod1(string siteName, IFlow pageFlow, List<string> productIdList, Dictionary<string, List<ItemData>> itemDataDict, TimeSpan delayBetweenRequests = new())
        {
            pageFlow.GetProductItemsList(siteName, productIdList, itemDataDict, delayBetweenRequests);
        }

        public string FileNameCreator(string fileNamePrefix, DateTime fileDateTime, string specificName = default)
        {
            var fileName = string.Concat(fileNamePrefix, "_" + specificName, fileDateTime.ToString("_yyyyMMdd_HHmmss"));
            return fileName;
        }
    }
}
