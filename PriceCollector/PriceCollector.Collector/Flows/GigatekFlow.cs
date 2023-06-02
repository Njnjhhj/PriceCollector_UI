using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
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
        private readonly GigatekSearchPopup _gigatekSearchPopup;

        public GigatekFlow(IWebDriver driver) : base(driver)
        {
            _gigatekMainPage = new GigatekMainPage(driver);
            _gigatekSearchResultPage = new GigatekSearchResultPage(driver);
            _gigatekSearchPopup = new GigatekSearchPopup(driver);
        }

        public void GetProductItemsList(string siteName, List<string> itemIdList, Dictionary<string, List<ItemData>> itemDataDict, TimeSpan delayBetweenRequests)
        {
            var itemDataList = new List<ItemData>();

            InitiateSearchPopup();

            try
            {
                foreach (var itemId in itemIdList)
                {
                    MethodUtils.Wait(delayBetweenRequests);

                    SearchProductItem(itemId);

                    var itemData = GetItemDataFromSearchResult(itemId);

                    if (string.IsNullOrEmpty(itemData.Price))
                    {
                        MethodUtils.Wait(TimeSpan.FromMilliseconds(300));
                        itemData = GetItemDataFromSearchResult(itemId);
                    }

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
            _gigatekSearchPopup.SearchField.Clear();

            foreach(var charItem in itemId)
            {
                var s = new StringBuilder().Append(charItem).ToString();
                _gigatekSearchPopup.SearchField.SendKeys(s);
            }
            
            Thread.Sleep(200);
        }

        public ItemData GetItemDataFromSearchResult(string itemId)
        {
            return new ItemData
            {
                Id = itemId,
                Availability = "n/a",
                Price = GetPriceFromSearchPopup(itemId)
            };
        }

        protected void InitiateSearchPopup()
        {
            _gigatekMainPage.SearchField.Clear();
            _gigatekMainPage.SearchField.Click();
        }

        protected string GetPriceFromSearchPopup(string itemId)
        {
            try
            {
                try
                {
                    return _gigatekSearchPopup.PriceValueElement(itemId).Text.DeleteCurrencySymbolsAndTrim().ReplaceDotByComma();
                }
                catch
                {

                    return _gigatekSearchPopup.PriceValueElement(itemId).Text.DeleteCurrencySymbolsAndTrim().ReplaceDotByComma();
                } 
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
                return _gigatekSearchResultPage.PriceValueElement().Text.DeleteCurrencySymbolsAndTrim().ReplaceDotByComma();
            }
            catch
            {
                return "not found";
            }
        }
    }
}
