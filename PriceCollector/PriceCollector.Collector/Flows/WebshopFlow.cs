using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using OpenQA.Selenium;
using PriceCollector.Collector.DataModels;
using PriceCollector.Collector.Flows.Interfaces;
using PriceCollector.Collector.PageObjects.Base;
using PriceCollector.Collector.PageObjects.Webshop;
using PriceCollector.Core.Utils;

namespace PriceCollector.Collector.Flows
{
    public class WebshopFlow : PageObjectBase, IFlow
    {
        private readonly WebshopMainPage _webshopMainPage;
        private readonly WebshopSearchResultPage _webshopSearchResultPage;

        public WebshopFlow(IWebDriver driver) : base(driver)
        {
            _webshopMainPage = new WebshopMainPage(driver);
            _webshopSearchResultPage = new WebshopSearchResultPage(driver);
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
            _webshopMainPage.SearchField.SendKeys(Keys.Control + "A");
            _webshopMainPage.SearchField.SendKeys(Keys.Delete);

            Thread.Sleep(50);

            _webshopMainPage.SearchField.Clear();
            _webshopMainPage.SearchField.SendKeys(itemId);
            _webshopMainPage.SearchField.SendKeys(Keys.Enter);

            Thread.Sleep(100);
        }

        public ItemData GetItemDataFromSearchResult(string itemId)
        {
            return new ItemData
            {
                Id = itemId,
                Availability = GetProductAvailability(),
                Price = GetPrice()
            };
        }

        protected string GetProductAvailability()
        {
            try
            {
                return _webshopSearchResultPage.StockAvailabilityElement().Text.Trim();
            }
            catch
            {
                return "not found";
            }
        }

        protected string GetPrice()
        {
            try
            {
                return _webshopSearchResultPage.PriceValueElement().Text.DeleteCurrencySymbolsAndTrim().ReplaceDotByComma();
            }
            catch
            {
                return "not found";
            }
        }
    }
}
