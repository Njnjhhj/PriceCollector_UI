using System;
using OpenQA.Selenium;
using PriceCollector.Collector.PageObjects.Base;
using PriceCollector.Collector.WebUtils;

namespace PriceCollector.Collector.PageObjects.Groothandel
{
    public class GroothandelProductDetailsPage : PageObjectBase
    {
        public GroothandelProductDetailsPage(IWebDriver driver) : base(driver) { }

        public IWebElement StockAvailabilityElement(string itemId) => driver.UsingDriverImplicitTimeout(() => driver.FindElement(By.CssSelector("p.stock")), TimeSpan.FromMilliseconds(100));
        public IWebElement PriceValueElement(string itemId) => driver.UsingDriverImplicitTimeout(() => driver.FindElement(By.CssSelector("p.price span.woocommerce-Price-amount")), TimeSpan.FromMilliseconds(100));
    }
}
