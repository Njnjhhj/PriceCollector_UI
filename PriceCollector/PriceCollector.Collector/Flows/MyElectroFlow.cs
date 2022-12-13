using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using OpenQA.Selenium;
using PriceCollector.Collector.DataModels;
using PriceCollector.Collector.Flows.Interfaces;
using PriceCollector.Collector.PageObjects.Base;
using PriceCollector.Collector.PageObjects.MyElectro;
using PriceCollector.Core.Utils;

namespace PriceCollector.Collector.Flows
{
    public class MyElectroFlow : PageObjectBase, IFlow
    {
        private readonly MyElectroMainPage _myElectroMainPage;
        private readonly MyElectroSearchResultPage _myElectroSearchResultPage;

        public MyElectroFlow(IWebDriver driver) : base(driver)
        {
            _myElectroMainPage = new MyElectroMainPage(driver);
            _myElectroSearchResultPage = new MyElectroSearchResultPage(driver);
        }

        public void GetProductItemsList(string siteName, List<string> itemIdList, Dictionary<string, List<ItemData>> itemDataDict, TimeSpan delayBetweenRequests)
        {
            var itemDataList = new List<ItemData>();

            try
            {
                foreach (var itemId in itemIdList)
                {
                    MethodUtils.Wait(delayBetweenRequests);

                    HandleCookiesPopup();

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
            _myElectroMainPage.SearchField.SendKeys(Keys.Control + "A");
            _myElectroMainPage.SearchField.SendKeys(Keys.Delete);
            _myElectroMainPage.SearchField.SendKeys(itemId);
            _myElectroMainPage.SearchField.SendKeys(Keys.Enter);

            Thread.Sleep(100);
        }

        public ItemData GetItemDataFromSearchResult(string itemId)
        {
            return new ItemData
            {
                Id = itemId,
                Availability = GetProductAvailability(itemId),
                Price = GetPrice(itemId)
            };
        }

        protected string GetProductAvailability(string itemId)
        {
            try
            {
                return _myElectroSearchResultPage.StockAvailabilityElement(itemId).Text.Trim();
            }
            catch
            {
                return "not found";
            }
        }

        protected string GetPrice(string itemId)
        {
            try
            {
                return _myElectroSearchResultPage.PriceValueElement(itemId).Text.DeleteCurrencySymbolsAndTrim().ReplaceDotByComma();
            }
            catch
            {
                return "not found";
            }
        }

        private void HandleCookiesPopup()
        {
            if (IsCookiesPopupDisplayed())
                _myElectroMainPage.AcceptСookieButton.Click();
        }

        private bool IsCookiesPopupDisplayed()
        {
            try
            {
                return _myElectroMainPage.СookiePopup.Displayed;
            }
            catch
            {
                return false;
            }
        }
    }
}
