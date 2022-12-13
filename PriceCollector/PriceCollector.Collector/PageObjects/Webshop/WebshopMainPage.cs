using OpenQA.Selenium;
using PriceCollector.Collector.PageObjects.Base;

namespace PriceCollector.Collector.PageObjects.Webshop
{
    public class WebshopMainPage : PageObjectBase
    {
        public WebshopMainPage(IWebDriver driver) : base(driver) { }

        public IWebElement SearchField => driver.FindElement(By.CssSelector(".input-search-field #search"));
    }
}
