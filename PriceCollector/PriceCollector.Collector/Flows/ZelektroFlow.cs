using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using OpenQA.Selenium;
using PriceCollector.Collector.DataModels;
using PriceCollector.Collector.Flows.Interfaces;
using PriceCollector.Collector.PageObjects.Base;
using PriceCollector.Collector.PageObjects.Zelektro;
using PriceCollector.Core.Utils;

namespace PriceCollector.Collector.Flows
{
    public class ZelektroFlow : PageObjectBase, IFlow
    {
        private readonly ZelektroMainPage _zelectroMainPage;
        private readonly ZelektroSearchResultPage _zelectroSearchResultPage;
        private readonly ZelektroProductItemDetailsPage _zelektroProductItemDetailsPage;

        public ZelektroFlow(IWebDriver driver) : base(driver)
        {
            _zelectroMainPage = new ZelektroMainPage(driver);
            _zelectroSearchResultPage = new ZelektroSearchResultPage(driver);
            _zelektroProductItemDetailsPage = new ZelektroProductItemDetailsPage(driver);
        }

        public void GetProductItemsList(string siteName, List<string> itemIdList, Dictionary<string, List<ItemData>> itemDataDict, TimeSpan delayBetweenRequests)
        {
            var itemDataList = new List<ItemData>();

            try
            {
                foreach (var itemId in itemIdList)
                {
                    MethodUtils.Wait(TimeSpan.FromMilliseconds(750));

                    ItemData itemData;

                    SearchProductItem(itemId);

                    if (IsThereSingleResult())
                    {
                        OpenProductItemDetailsPage();
                        itemData = GetProductItemData(itemId);
                    }
                    else
                        itemData = GetEmptyData(itemId);

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
            _zelectroMainPage.SearchField.Clear();
            _zelectroMainPage.SearchField.SendKeys(itemId);
            _zelectroMainPage.SearchField.SendKeys(Keys.Enter);
        }

        public ItemData GetItemDataFromSearchResult(string itemId)
        {
            throw new NotImplementedException();
        }

        protected ItemData GetProductItemData(string itemId)
        {
            return new ItemData
            {
                Id = itemId,
                Availability = GetProductAvailability(),
                Price = GetPrice()
            };
        }

        protected ItemData GetEmptyData(string itemId)
        {
            return new ItemData
            {
                Id = itemId,
                Availability = "n/a",
                Price = "not found"
            };
        }

        protected string GetProductAvailability()
        {
            try
            {
                return _zelektroProductItemDetailsPage.StockAvailabilityElement.Text.Trim();
            }
            catch
            {
                return "not found(!)";
            }
        }

        protected string GetPrice()
        {
            try
            {
                return _zelektroProductItemDetailsPage.PriceValueElement.Text.DeleteCurrencySymbolsAndTrim().ReplaceDotByComma();
            }
            catch
            {
                return "not found(!)";
            }
        }

        private void OpenProductItemDetailsPage()
        {
            _zelectroSearchResultPage.ProductItemLinkElements.FirstOrDefault()?.Click();
        }

        private bool IsThereSingleResult()
        {
            var isSingle = _zelectroSearchResultPage.ProductItemLinkElements.Count == 1;
            return isSingle;
        }
    }
}
