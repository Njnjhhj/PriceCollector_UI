using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using PriceCollector.Collector.PageObjects.Base;
using PriceCollector.Collector.WebUtils;

namespace PriceCollector.Collector.PageObjects.Webshop
{
    public class WebshopSearchResultPage : PageObjectBase
    {
        public WebshopSearchResultPage(IWebDriver driver) : base(driver) { }

        // Single item element
        public IWebElement ProductSingleItemBlock() => driver.UsingDriverImplicitTimeout( () => driver.FindElement(By.CssSelector("#main .single-product-wrapper")), TimeSpan.FromMilliseconds(100));
        public IWebElement StockAvailabilityElement() => driver.UsingDriverImplicitTimeout(() => ProductSingleItemBlock().FindElement(By.CssSelector(".availability p")), TimeSpan.FromMilliseconds(100));
        public IWebElement PriceValueElement() => driver.UsingDriverImplicitTimeout(() => ProductSingleItemBlock().FindElement(By.CssSelector(".price ins .woocommerce-Price-amount")), TimeSpan.FromMilliseconds(100));

        // Multiple item elements
        public IWebElement PriceValueElementFromItemsList(string itemId) => ProductListItemDisplayedBlock(itemId).FindElement(By.CssSelector("span .woocommerce-Price-amount"));

        private IWebElement ProductListItemDisplayedBlock(string itemId) => ProductListItemBlockElements(itemId).First(e => e.Displayed);
        private List<IWebElement> ProductListItemBlockElements(string itemId) => driver.UsingDriverImplicitTimeout(() => driver.FindElements(By.XPath($"//h2[@class='woocommerce-loop-product__title' and contains(text(), '{itemId}')]/ancestor::div[@class='product-inner']")), TimeSpan.FromMilliseconds(100)).ToList();
    }
}
