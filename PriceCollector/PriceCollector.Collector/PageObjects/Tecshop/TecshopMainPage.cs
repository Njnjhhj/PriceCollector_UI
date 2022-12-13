using OpenQA.Selenium;
using PriceCollector.Collector.PageObjects.Base;

namespace PriceCollector.Collector.PageObjects.Tecshop
{
    public class TecshopMainPage : PageObjectBase
    {
        public TecshopMainPage(IWebDriver driver) : base(driver) { }

        public IWebElement SearchField => driver.FindElement(By.CssSelector("#search_mini_form input#search"));
    }
}
