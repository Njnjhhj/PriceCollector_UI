using System;
using OpenQA.Selenium;
using PriceCollector.Collector.PageObjects.Base;
using PriceCollector.Collector.WebUtils;

namespace PriceCollector.Collector.PageObjects.Dmlights
{
    public class DmlightsMainPage : PageObjectBase
    {
        public DmlightsMainPage(IWebDriver driver) : base(driver) { }

        public IWebElement CookiePopup => driver.FindElement(By.CssSelector("#cmpbox"));
        public IWebElement AcceptAllButton => CookiePopup.FindElement(By.CssSelector("#cmpbntyestxt"));
        public IWebElement SearchField => driver.FindElement(By.CssSelector("#topSearchInput"));
        public IWebElement SearchButton => driver.FindElement(By.CssSelector(".dmSearchField .icon-search"));
        public IWebElement QuestionPopup => driver.UsingDriverImplicitTimeout(()=> driver.FindElement(By.CssSelector(".ins-preview-wrapper")), TimeSpan.FromMilliseconds(300));
        public IWebElement QuestionPopupCloseButton => driver.UsingDriverImplicitTimeout( () => QuestionPopup.FindElement(By.CssSelector(".ins-element-close-button")), TimeSpan.FromMilliseconds(300));
    }
}
