using System;
using OpenQA.Selenium;
using PriceCollector.Collector.PageObjects.Base;
using PriceCollector.Collector.WebUtils;

namespace PriceCollector.Collector.PageObjects.Gigatek
{
    public class GigatekSearchResultPage : PageObjectBase
    {
        public GigatekSearchResultPage(IWebDriver driver) : base(driver) { }

        public IWebElement ProductItemBlock() => driver.FindElement(By.CssSelector(".product-thumbs-content"));
        public IWebElement StockAvailabilityElement() => driver.UsingDriverImplicitTimeout(() => driver.FindElement(By.CssSelector(".wrapper-image span.stock")), TimeSpan.FromMilliseconds(300));
        public IWebElement PriceValueElement() => driver.UsingDriverImplicitTimeout(() => ProductItemBlock().FindElement(By.CssSelector("span.product-thumbs-price-n")), TimeSpan.FromMilliseconds(300));
    }
}
