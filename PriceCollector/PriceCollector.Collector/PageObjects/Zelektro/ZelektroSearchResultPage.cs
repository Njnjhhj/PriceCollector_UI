using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using PriceCollector.Collector.PageObjects.Base;
using PriceCollector.Collector.WebUtils;

namespace PriceCollector.Collector.PageObjects.Zelektro
{
    public class ZelektroSearchResultPage : PageObjectBase
    {
        public ZelektroSearchResultPage(IWebDriver driver) : base(driver) { }
        public List<IWebElement> ProductItemLinkElements => driver.UsingDriverImplicitTimeout( () =>driver.FindElements(By.CssSelector(".kd-pro-grid .kd-link-overlay")), TimeSpan.FromMilliseconds(1000)).ToList();
    }
}
