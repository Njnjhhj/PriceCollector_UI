using System.Collections.Generic;
using System.Threading;
using OpenQA.Selenium;
using PriceCollector.CollectorStgPro.DataModels;
using PriceCollector.CollectorStgPro.PageObjects;
using PriceCollector.CollectorStgPro.PageObjects.Base;
using PriceCollector.Core.Utils;

namespace PriceCollector.CollectorStgPro.Flows
{
    public class ResultTableFlow : PageObjectBase
    {
        private readonly StgResultTablePage _stgResultTablePage;
        private readonly SearchFlow _searchFlow;

        public ResultTableFlow(IWebDriver driver) : base(driver)
        {
            _stgResultTablePage = new StgResultTablePage(driver);
            _searchFlow = new SearchFlow(driver);
        }

        public List<StgItemData> GetItemsDataList(List<string> productIdList)
        {
            var resultList = new List<StgItemData>();

            foreach (var productId in productIdList)
            {
                _searchFlow.SearchByProductId(productId)
                    ;
                Thread.Sleep(1000);

                var itemData = GetItemData(productId);
                resultList.Add(itemData);
            }

            return resultList;
        }

        private StgItemData GetItemData(string productId)
        {
            return new StgItemData()
            {
                ProductId = productId,
                Article = GetArticle(),
                Orders = GetOrders(),
                Stock = DefineStockType(GetStockAttribute()),
                NetPrice = GetNetPrice()
            };
        }

        private string GetArticle()
        {
            try
            {
                return _stgResultTablePage.ArticleElement.Text.Trim();
            }
            catch
            {
                return "art. not found";
            }
        }

        private string GetOrders()
        {
            try
            {
                var text = _stgResultTablePage.OrderedElement.Text.Trim();
                return string.IsNullOrEmpty(text) ? "not specified" : text;
            }
            catch
            {
                return "not found";
            }
        }

        private string GetStockAttribute()
        {
            try
            {
                return _stgResultTablePage.StockElement.GetAttribute("class");
            }
            catch
            {
                return "not found";
            }
        }

        private string GetNetPrice()
        {
            try
            {
                return _stgResultTablePage.NetPriceElement.Text.DeleteCurrencySymbolsAndTrim().ReplaceDotByComma();
            }
            catch
            {
                return "not found";
            }
        }

        private string DefineStockType(string str)
        {
            return str switch
            {
                { } a when a.Contains("light-green") => "GREEN",
                { } a when a.Contains("orange") => "ORANGE",
                { } a when a.Contains("warehouse-red") => "RED",
                { } a when a.Contains("not found") => "not found",
                _ => "color not specified"
            };
        }
        
        //private Color DefineStockColor(string str)
        //{
        //    return str switch
        //    {
        //        { } a when a.Contains("light-green") => Color.Green,
        //        { } a when a.Contains("orange") => Color.Orange,
        //        { } a when a.Contains("warehouse-red") => Color.Red,
        //        _ => Color.Gray
        //    };
        //}
    }
}
