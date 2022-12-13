using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using OpenQA.Selenium;
using PriceCollector.Collector.DataModels;
using PriceCollector.Collector.Flows.Interfaces;
using PriceCollector.Collector.PageObjects.Base;
using PriceCollector.Collector.PageObjects.Solyd;
using PriceCollector.Core.Utils;

namespace PriceCollector.Collector.Flows
{
    public class SolydFlow : PageObjectBase, IFlow
    {
        private readonly SolydMainPage _solydMainPage;
        private readonly SolydSearchResultPage _solydSearchResultPage;

        public SolydFlow(IWebDriver driver) : base(driver)
        {
            _solydMainPage = new SolydMainPage(driver);
            _solydSearchResultPage = new SolydSearchResultPage(driver);
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
            _solydMainPage.SearchField.Clear();
            _solydMainPage.SearchField.SendKeys(itemId);
            _solydMainPage.SearchField.SendKeys(Keys.Enter);

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
                return _solydSearchResultPage.StockAvailabilityElement().Text.Trim();
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
                return _solydSearchResultPage.PriceValueElement().Text.DeleteCurrencySymbolsAndTrim().ReplaceDotByComma();
            }
            catch
            {
                return "not found";
            }
        }
    }
}
