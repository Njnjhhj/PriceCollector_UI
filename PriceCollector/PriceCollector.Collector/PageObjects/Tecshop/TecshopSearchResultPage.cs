using System;
using OpenQA.Selenium;
using PriceCollector.Collector.PageObjects.Base;
using PriceCollector.Collector.WebUtils;

namespace PriceCollector.Collector.PageObjects.Tecshop
{
    public class TecshopSearchResultPage : PageObjectBase
    {
        public TecshopSearchResultPage(IWebDriver driver) : base(driver) { }

        public IWebElement ProductItemBlock(string itemId) => driver.FindElement(By.XPath($"//a[contains(@title, '{itemId}')]/ancestor::li"));

        public IWebElement PriceValueElement(string itemId) => driver.UsingDriverImplicitTimeout(() => ProductItemBlock(itemId).FindElement(By.XPath("./div[@class='price-box']//span[@class='price']")), TimeSpan.FromMilliseconds(100));
    }
}
