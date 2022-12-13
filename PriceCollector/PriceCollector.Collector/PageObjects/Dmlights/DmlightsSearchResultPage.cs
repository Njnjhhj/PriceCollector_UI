using System;
using OpenQA.Selenium;
using PriceCollector.Collector.PageObjects.Base;
using PriceCollector.Collector.WebUtils;

namespace PriceCollector.Collector.PageObjects.Dmlights
{
    public class DmlightsSearchResultPage : PageObjectBase
    {
        public DmlightsSearchResultPage(IWebDriver driver) : base(driver) { }

        public IWebElement ProductItemBlock(string itemId) => driver.UsingDriverImplicitTimeout(() => driver.FindElement(By.XPath($"//b[contains(text(), '{itemId}')]//ancestor::div[contains(@class, 'dmProductSideActionBlock__list ')]")), TimeSpan.FromMilliseconds(200));
        public IWebElement StockAvailabilityElement(string itemId) => driver.UsingDriverImplicitTimeout(() => ProductItemBlock(itemId).FindElement(By.XPath(".//div[@class='dmProduct--stock']")), TimeSpan.FromMilliseconds(100));
        public IWebElement PriceValueElement(string itemId) => driver.UsingDriverImplicitTimeout(() => ProductItemBlock(itemId).FindElement(By.XPath(".//div[@class='row productPrice']//div[contains(@class, 'text-right')]")), TimeSpan.FromMilliseconds(100));

        public IWebElement RequestDataPopup => driver.UsingDriverImplicitTimeout(() => driver.FindElement(By.XPath(".//div[@data-show='before']")), TimeSpan.FromMilliseconds(50));
        public IWebElement RequestDataPopupCrossBtn => RequestDataPopup.FindElement(By.XPath("./a"));
    }
}
