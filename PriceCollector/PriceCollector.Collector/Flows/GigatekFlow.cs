using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using OpenQA.Selenium;
using PriceCollector.Collector.DataModels;
using PriceCollector.Collector.Flows.Interfaces;
using PriceCollector.Collector.PageObjects.Base;
using PriceCollector.Collector.PageObjects.Gigatek;
using PriceCollector.Core.Utils;

namespace PriceCollector.Collector.Flows
{
    public class GigatekFlow : PageObjectBase, IFlow
    {
        private readonly GigatekMainPage _gigatekMainPage;
        private readonly GigatekSearchResultPage _gigatekSearchResultPage;

        public GigatekFlow(IWebDriver driver) : base(driver)
        {
            _gigatekMainPage = new GigatekMainPage(driver);
            _gigatekSearchResultPage = new GigatekSearchResultPage(driver);
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
            _gigatekMainPage.SearchField.Clear();
            _gigatekMainPage.SearchField.SendKeys(itemId);
            _gigatekMainPage.SearchField.SendKeys(Keys.Enter);

            Thread.Sleep(100);
        }

        public ItemData GetItemDataFromSearchResult(string itemId)
        {
            return new ItemData
            {
                Id = itemId,
                Availability = "n/a",
                Price = GetPrice()
            };
        }

        protected string GetPrice()
        {
            try
            {
                return _gigatekSearchResultPage.PriceValueElement().Text.DeleteCurrencySymbolsAndTrim().ReplaceDotByComma();
            }
            catch
            {
                return "not found";
            }
        }
    }
}
