using System;
using OpenQA.Selenium;
using PriceCollector.Collector.PageObjects.Base;
using PriceCollector.Collector.WebUtils;

namespace PriceCollector.Collector.PageObjects.Semmatec
{
    public class SemmatecSearchResultPage : PageObjectBase
    {
        public SemmatecSearchResultPage(IWebDriver driver) : base(driver) { }

        public IWebElement ProductItemBlock() => driver.FindElement(By.CssSelector(".product-container .right-block"));
        public IWebElement PriceValueElement() => driver.UsingDriverImplicitTimeout(() => ProductItemBlock().FindElement(By.CssSelector("span.product-price")), TimeSpan.FromMilliseconds(300));
    }
}
