using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using OpenQA.Selenium;
using PriceCollector.Collector.DataModels;
using PriceCollector.Collector.Flows.Interfaces;
using PriceCollector.Collector.PageObjects.Base;
using PriceCollector.Collector.PageObjects.Omnielectric;
using PriceCollector.Core.Utils;

namespace PriceCollector.Collector.Flows
{
    public class OmnielectricFlow : PageObjectBase, IFlow
    {
        private readonly OmnielectricMainPage _omnielectricMainPage;
        private readonly OmnielectricSearchResultPage _omnielectricSearchResultPage;

        public OmnielectricFlow(IWebDriver driver) : base(driver)
        {
            _omnielectricMainPage = new OmnielectricMainPage(driver);
            _omnielectricSearchResultPage = new OmnielectricSearchResultPage(driver);
        }

        public void GetProductItemsList(string siteName, List<string> itemIdList, Dictionary<string, List<ItemData>> itemDataDict, TimeSpan delayBetweenRequests)
        {
            var itemDataList = new List<ItemData>();

            try
            {
                foreach (var itemId in itemIdList)
                {
                    MethodUtils.Wait(delayBetweenRequests);

                    SearchProductItem(itemId);

                    var itemData = GetItemDataFromSearchResult(itemId);

                    itemDataList.Add(itemData);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"ERROR occurred in method '{MethodBase.GetCurrentMethod()}': {e}");
                throw;
            }
            finally
            {
                itemDataDict.Add(siteName, itemDataList);
            }
        }

        public void SearchProductItem(string itemId)
        {
            _omnielectricMainPage.SearchField.Clear();
            _omnielectricMainPage.SearchField.SendKeys(itemId);
            _omnielectricMainPage.SearchField.SendKeys(Keys.Enter);

            Thread.Sleep(100);
        }

        public ItemData GetItemDataFromSearchResult(string itemId)
        {
            return new ItemData
            {
                Id = itemId,
                Availability = "n/a",
                Price = GetPrice(itemId)
            };
        }

        protected string GetPrice(string itemId)
        {
            try
            {
                return _omnielectricSearchResultPage.PriceValueElement(itemId).Text.DeleteCurrencySymbolsAndTrim().ReplaceDotByComma();
            }
            catch
            {
                return "not found";
            }
        }
    }
}
