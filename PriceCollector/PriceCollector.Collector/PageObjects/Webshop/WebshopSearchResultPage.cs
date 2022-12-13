using System;
using OpenQA.Selenium;
using PriceCollector.Collector.PageObjects.Base;
using PriceCollector.Collector.WebUtils;

namespace PriceCollector.Collector.PageObjects.Webshop
{
    public class WebshopSearchResultPage : PageObjectBase
    {
        public WebshopSearchResultPage(IWebDriver driver) : base(driver) { }

        public IWebElement ProductItemBlock() => driver.UsingDriverImplicitTimeout( () => driver.FindElement(By.CssSelector("#main .single-product-wrapper")), TimeSpan.FromMilliseconds(100));
        public IWebElement StockAvailabilityElement() => driver.UsingDriverImplicitTimeout(() => ProductItemBlock().FindElement(By.CssSelector(".availability p")), TimeSpan.FromMilliseconds(100));
        public IWebElement PriceValueElement() => driver.UsingDriverImplicitTimeout(() => ProductItemBlock().FindElement(By.CssSelector(".price ins .woocommerce-Price-amount")), TimeSpan.FromMilliseconds(100));
    }
}
