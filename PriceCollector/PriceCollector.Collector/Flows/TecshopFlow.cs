using System;
using System.Collections.Generic;
using System.Reflection;
using OpenQA.Selenium;
using PriceCollector.Collector.DataModels;
using PriceCollector.Collector.Flows.Interfaces;
using PriceCollector.Collector.PageObjects.Base;
using PriceCollector.Collector.PageObjects.Tecshop;
using PriceCollector.Core.Utils;

namespace PriceCollector.Collector.Flows
{
    public class TecshopFlow : PageObjectBase, IFlow
    {
        private readonly TecshopMainPage _tecshopMainPage;
        private readonly TecshopSearchResultPage _tecshopSearchResultPage;

        public TecshopFlow(IWebDriver driver) : base(driver)
        {
            _tecshopMainPage = new TecshopMainPage(driver);
            _tecshopSearchResultPage = new TecshopSearchResultPage(driver);
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
            _tecshopMainPage.SearchField.Clear();
            _tecshopMainPage.SearchField.SendKeys("*" + itemId);
            _tecshopMainPage.SearchField.SendKeys(Keys.Enter);
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
                return _tecshopSearchResultPage.PriceValueElement(itemId).Text.DeleteCurrencySymbolsAndTrim().ReplaceDotByComma();
            }
            catch
            {
                return "not found";
            }
        }
    }
}
