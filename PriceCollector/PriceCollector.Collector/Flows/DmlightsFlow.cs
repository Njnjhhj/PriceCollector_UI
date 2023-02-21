using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using OpenQA.Selenium;
using PriceCollector.Collector.DataModels;
using PriceCollector.Collector.Flows.Interfaces;
using PriceCollector.Collector.PageObjects.Base;
using PriceCollector.Collector.PageObjects.Dmlights;
using PriceCollector.Collector.WebUtils;
using PriceCollector.Core.Utils;

namespace PriceCollector.Collector.Flows
{
    public class DmlightsFlow : PageObjectBase, IFlow
    {
        private readonly DmlightsMainPage _dmlightsMainPage;
        private readonly DmlightsSearchResultPage _dmlightsSearchResultPage;

        public DmlightsFlow(IWebDriver driver) : base(driver)
        {
            _dmlightsMainPage = new DmlightsMainPage(driver);
            _dmlightsSearchResultPage = new DmlightsSearchResultPage(driver);
        }

        public void GetProductItemsList(string siteName, List<string> itemIdList, Dictionary<string, List<ItemData>> itemDataDict, TimeSpan delayBetweenRequests)
        {
            var itemDataList = new List<ItemData>();

            try
            {
                foreach (var itemId in itemIdList)
                {
                    MethodUtils.Wait(delayBetweenRequests);

                    HandleCookiePopup();

                    HandleQuestionPopup();

                    SearchProductItem(itemId);

                    HandleRequestDataPopup();

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
            _dmlightsMainPage.SearchField.Clear();
            _dmlightsMainPage.SearchField.SendKeys( itemId);
            _dmlightsMainPage.SearchField.SendKeys(Keys.Enter);
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

        public void HandleCookiePopup()
        {
            if (_dmlightsMainPage.CookiePopup.Displayed)
                _dmlightsMainPage.AcceptAllButton.Click();
        }

        public void HandleQuestionPopup()
        {
            try
            {
                if (IsQuestionPopupDisplayed())
                    _dmlightsMainPage.QuestionPopupCloseButton.Click();
            }
            catch (Exception e)
            {
                Console.WriteLine($"WARNING! Error in '{MethodBase.GetCurrentMethod()}': {e.Message} ");
            }
            
        }

        protected string GetProductAvailability(string itemId)
        {
            try
            {
                return _dmlightsSearchResultPage.StockAvailabilityElement(itemId).Text.Trim();
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
                return _dmlightsSearchResultPage.PriceValueElement(itemId).Text.DeleteCurrencySymbolsAndTrim().ReplaceDotByComma();
            }
            catch
            {
                return "not found";
            }
        }

        private void HandleRequestDataPopup()
        {

            if (IsPopupDisplayed())
                _dmlightsSearchResultPage.RequestDataPopupCrossBtn.Click();

            driver.GoToDefaultFrameImplicitly();
        }

        private bool IsQuestionPopupDisplayed()
        {
            try
            {
                return _dmlightsMainPage.QuestionPopup.Displayed;
            }
            catch
            {
                return false;
            }
        }

        private bool IsPopupDisplayed()
        {
            try
            {
                var frame = driver.GetFrames("style", "display: inline !important").FirstOrDefault();

                if (frame == null) return false;

                driver.SwitchTo().Frame(frame);

                var isPopupDisplayed = _dmlightsSearchResultPage.RequestDataPopup.Displayed;

                return isPopupDisplayed;
            }
            catch
            {
                return false;
            }
        }
    }
}
