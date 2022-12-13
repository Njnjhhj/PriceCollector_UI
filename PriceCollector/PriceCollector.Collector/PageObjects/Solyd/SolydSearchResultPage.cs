using System;
using OpenQA.Selenium;
using PriceCollector.Collector.PageObjects.Base;
using PriceCollector.Collector.WebUtils;

namespace PriceCollector.Collector.PageObjects.Solyd
{
    public class SolydSearchResultPage : PageObjectBase
    {
        public SolydSearchResultPage(IWebDriver driver) : base(driver) { }

        public IWebElement ProductItemBlock() => driver.FindElement(By.CssSelector(".product-item-details .product-item-footer"));
        public IWebElement StockAvailabilityElement() => driver.UsingDriverImplicitTimeout(() => ProductItemBlock().FindElement(By.CssSelector(".available span")), TimeSpan.FromMilliseconds(300));
        public IWebElement PriceValueElement() => driver.UsingDriverImplicitTimeout(() => ProductItemBlock().FindElement(By.CssSelector("div.price-final_price .price-including-tax span.price")), TimeSpan.FromMilliseconds(300));
    }
}
