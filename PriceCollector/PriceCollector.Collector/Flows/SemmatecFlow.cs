using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using OpenQA.Selenium;
using PriceCollector.Collector.DataModels;
using PriceCollector.Collector.Flows.Interfaces;
using PriceCollector.Collector.PageObjects.Base;
using PriceCollector.Collector.PageObjects.Semmatec;
using PriceCollector.Core.Utils;

namespace PriceCollector.Collector.Flows
{
    public class SemmatecFlow : PageObjectBase, IFlow
    {
        private readonly SemmatecMainPage _semmatecMainPage;
        private readonly SemmatecSearchResultPage _semmatecSearchResultPage;

        public SemmatecFlow(IWebDriver driver) : base(driver)
        {
            _semmatecMainPage = new SemmatecMainPage(driver);
            _semmatecSearchResultPage = new SemmatecSearchResultPage(driver);
        }

        public void GetProductItemsList(string siteName, List<string> itemIdList, Dictionary<string, List<ItemData>> itemDataDict, TimeSpan delayBetweenRequests)
        {
            var itemDataList = new List<ItemData>();

            try
            {
                foreach (var itemId in itemIdList)
                {
                    HandleCookiePopup();

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
            _semmatecMainPage.SearchField.Clear();
            _semmatecMainPage.SearchField.SendKeys("*" + itemId);
            _semmatecMainPage.SearchField.SendKeys(Keys.Enter);
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

        public void HandleCookiePopup()
        {
            if (IsCookiePopupDisplayed())
                _semmatecMainPage.AcceptAllButton.Click();
        }

        protected string GetPrice(string itemId)
        {
            try
            {
                return _semmatecSearchResultPage.PriceValueElement().Text.DeleteCurrencySymbolsAndTrim().ReplaceDotByComma();
            }
            catch
            {
                return "not found";
            }
        }

        private bool IsCookiePopupDisplayed()
        {
            try
            {
                return _semmatecMainPage.CookiePopup.Displayed;
            }
            catch
            {
                return false;
            }
        }
    }
}
