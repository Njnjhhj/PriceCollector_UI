using System;
using System.Collections.Generic;
using PriceCollector.Collector.DataModels;

namespace PriceCollector.Collector.Flows.Interfaces
{
    public interface IFlow
    {
        public void GetProductItemsList(string siteName, List<string> itemIdList, Dictionary<string, List<ItemData>> itemDataDict, TimeSpan delayBetweenRequests);

        public void SearchProductItem(string itemId);

        public ItemData GetItemDataFromSearchResult(string itemId);
    }
}
