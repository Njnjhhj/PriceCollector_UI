using System;
using OpenQA.Selenium;
using PriceCollector.Collector.PageObjects.Base;
using PriceCollector.Collector.WebUtils;

namespace PriceCollector.Collector.PageObjects.MyElectro
{
    public class MyElectroMainPage : PageObjectBase
    {
        public MyElectroMainPage(IWebDriver driver) : base(driver) { }

        public IWebElement SearchField => driver.FindElement(By.CssSelector("input.search-input[name=search]"));
        public IWebElement AcceptСookieButton => СookiePopup.FindElement(By.XPath("//a[@aria-label='allow cookies']"));
        public IWebElement СookiePopup => driver.UsingDriverImplicitTimeout(() => driver.FindElement(By.CssSelector("div.cc-window")), TimeSpan.FromMilliseconds(200));
    }
}
