using System;
using OpenQA.Selenium;
using PriceCollector.Collector.PageObjects.Base;
using PriceCollector.Collector.WebUtils;
using PriceCollector.Core.Utils;

namespace PriceCollector.Collector.PageObjects.MyElectro
{
    public class MyElectroSearchResultPage : PageObjectBase
    {
        public MyElectroSearchResultPage(IWebDriver driver) : base(driver) { }

        public IWebElement ProductItemBlock(string itemId) => driver.UsingDriverImplicitTimeout(() => ProductHrefElement(itemId).FindElement(By.XPath("./ancestor::div[@class='product-thumb']")), TimeSpan.FromMilliseconds(200));
        public IWebElement StockAvailabilityElement(string itemId) => driver.UsingDriverImplicitTimeout(() => ProductItemBlock(itemId).FindElement(By.CssSelector("span.product-stock-status")), TimeSpan.FromMilliseconds(200));
        public IWebElement PriceValueElement(string itemId) => driver.UsingDriverImplicitTimeout(() => ProductItemBlock(itemId).FindElement(By.CssSelector("span.price-normal")), TimeSpan.FromMilliseconds(200));


        public IWebElement ProductHrefElement(string itemId) => driver.UsingDriverImplicitTimeout(() => driver.FindElement(By.XPath(ProductHrefXpath(itemId))), TimeSpan.FromMilliseconds(500));

        public string ProductHrefXpath(string itemId) => $"//a[contains(@href, '{itemId.DeleteSymbolsAndTrim('-')}') or contains(@href, '{itemId}') and @title]";
    }
}
