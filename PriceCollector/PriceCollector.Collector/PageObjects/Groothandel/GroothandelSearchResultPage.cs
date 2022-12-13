using System;
using OpenQA.Selenium;
using PriceCollector.Collector.PageObjects.Base;
using PriceCollector.Collector.WebUtils;

namespace PriceCollector.Collector.PageObjects.Groothandel
{
    public class GroothandelSearchResultPage : PageObjectBase
    {
        public GroothandelSearchResultPage(IWebDriver driver) : base(driver) { }

        public IWebElement ProductItemBlock(string itemId) => driver.UsingDriverImplicitTimeout(() => ProductHrefElement(itemId).FindElement(By.XPath("./ancestor::li")), TimeSpan.FromMilliseconds(100));
        public IWebElement StockAvailabilityElement(string itemId) => driver.UsingDriverImplicitTimeout(() => ProductItemBlock(itemId).FindElement(By.CssSelector("p.stock")), TimeSpan.FromMilliseconds(2000));
        public IWebElement PriceValueElement(string itemId) => driver.UsingDriverImplicitTimeout(() => ProductItemBlock(itemId).FindElement(By.CssSelector("span.price")), TimeSpan.FromMilliseconds(100));

        private IWebElement ProductHrefElement(string itemId) => driver.UsingDriverImplicitTimeout(() => driver.FindElement(By.XPath($"//a[contains(@href, '{itemId}')]")), TimeSpan.FromMilliseconds(200));
    }
}
