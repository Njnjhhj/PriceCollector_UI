using System;
using OpenQA.Selenium;
using PriceCollector.Collector.PageObjects.Base;
using PriceCollector.Collector.WebUtils;

namespace PriceCollector.Collector.PageObjects.Semmatec
{
    public class SemmatecMainPage : PageObjectBase
    {
        public SemmatecMainPage(IWebDriver driver) : base (driver) { }

        public IWebElement CookiePopup => driver.UsingDriverImplicitTimeout(() => driver.FindElement(By.CssSelector("#lgcookieslaw_banner")), TimeSpan.FromMilliseconds(300));
        public IWebElement AcceptAllButton => CookiePopup.FindElement(By.CssSelector("#lgcookieslaw_accept"));
        public IWebElement SearchField => driver.FindElement(By.CssSelector("input#search_query_top"));
    }
}
