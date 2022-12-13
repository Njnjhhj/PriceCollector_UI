using System;
using OpenQA.Selenium;
using PriceCollector.Collector.PageObjects.Base;
using PriceCollector.Collector.WebUtils;

namespace PriceCollector.Collector.PageObjects.Zelektro
{
    public class ZelektroProductItemDetailsPage : PageObjectBase
    {
        public ZelektroProductItemDetailsPage(IWebDriver driver) : base(driver) { }
        
        public IWebElement PriceValueElement => driver.UsingDriverImplicitTimeout(() => driver.FindElement(By.CssSelector("span.oe_price span")), TimeSpan.FromMilliseconds(300));
        public IWebElement StockAvailabilityElement => driver.UsingDriverImplicitTimeout(() => driver.FindElement(By.CssSelector(".stock_info span")), TimeSpan.FromMilliseconds(300));
    }
}
