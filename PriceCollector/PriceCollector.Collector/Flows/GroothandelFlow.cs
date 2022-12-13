using System;
using System.Collections.Generic;
using System.Reflection;
using OpenQA.Selenium;
using PriceCollector.Collector.DataModels;
using PriceCollector.Collector.Flows.Interfaces;
using PriceCollector.Collector.PageObjects.Base;
using PriceCollector.Collector.PageObjects.Groothandel;
using PriceCollector.Core.Utils;

namespace PriceCollector.Collector.Flows
{
    public class GroothandelFlow : PageObjectBase, IFlow
    {
        private readonly GroothandelMainPage _groothandelMainPage;
        private readonly GroothandelSearchResultPage _groothandelSearchResultPage;
        private readonly GroothandelProductDetailsPage _groothandelProductDetailsPage;

        public GroothandelFlow(IWebDriver driver) : base(driver)
        {
            _groothandelMainPage = new GroothandelMainPage(driver);
            _groothandelSearchResultPage = new GroothandelSearchResultPage(driver);
            _groothandelProductDetailsPage = new GroothandelProductDetailsPage(driver);
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

                    var itemData = IsProductItemBlockDisplayed(itemId) ?
                        GetItemDataFromSearchResult(itemId) :
                        GetItemDataFromProductDetails(itemId);

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
            _groothandelMainPage.SearchField.Clear();
            _groothandelMainPage.SearchField.SendKeys("*" + itemId);
            _groothandelMainPage.SearchField.SendKeys(Keys.Enter);
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

        protected ItemData GetItemDataFromProductDetails(string itemId)
        {
            return new ItemData
            {
                Id = itemId,
                Availability = GetProductAvailabilityFromDetails(itemId),
                Price = GetPriceFromDetails(itemId)
            };
        }

        protected string GetProductAvailability(string itemId)
        {
            try
            {
                return _groothandelSearchResultPage.StockAvailabilityElement(itemId).Text.Trim();
            }
            catch
            {
                return "not found";
            }
        }

        protected string GetProductAvailabilityFromDetails(string itemId)
        {
            try
            {
                return _groothandelProductDetailsPage.StockAvailabilityElement(itemId).Text.Trim();
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
                return _groothandelSearchResultPage.PriceValueElement(itemId).Text.DeleteCurrencySymbolsAndTrim().ReplaceDotByComma();
            }
            catch
            {
                return "not found";
            }
        }

        protected string GetPriceFromDetails(string itemId)
        {
            try
            {
                return _groothandelProductDetailsPage.PriceValueElement(itemId).Text.DeleteCurrencySymbolsAndTrim().ReplaceDotByComma();
            }
            catch
            {
                return "not found";
            }
        }

        private bool IsProductItemBlockDisplayed(string itemId)
        {
            try
            {
                return _groothandelSearchResultPage.ProductItemBlock(itemId).Displayed;
            }
            catch
            {
                return false;
            }
        }
    }
}
