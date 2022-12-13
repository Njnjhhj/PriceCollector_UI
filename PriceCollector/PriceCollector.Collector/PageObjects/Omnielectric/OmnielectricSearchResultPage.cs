using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using OpenQA.Selenium;
using PriceCollector.Collector.PageObjects.Base;
using PriceCollector.Collector.WebUtils;

namespace PriceCollector.Collector.PageObjects.Omnielectric
{
    public class OmnielectricSearchResultPage : PageObjectBase
    {
        public OmnielectricSearchResultPage(IWebDriver driver) : base(driver) { }

        public IWebElement ProductItemBlock(string itemId) => driver.UsingDriverImplicitTimeout(() => GetProductIdElement(itemId).FindElement(By.XPath("./ancestor::div[@class='grid-product__wrap-inner']")), TimeSpan.FromMilliseconds(100));
        public IWebElement PriceValueElement(string itemId) => driver.UsingDriverImplicitTimeout(() => ProductItemBlock(itemId).FindElement(By.CssSelector("div.grid-product__price-amount")), TimeSpan.FromMilliseconds(100));

        private List<IWebElement> ProductIdElementsList() => driver.FindElements(By.XPath("//div[@class='grid-product__sku-inner']")).ToList();

        private IWebElement GetProductIdElement(string itemId)
        {
            Thread.Sleep(600);

            var element = ProductIdElementsList().FirstOrDefault(x => x.Text.Contains(itemId));
            driver.MoveToElement(element);

            return element;
        }
    }
}
