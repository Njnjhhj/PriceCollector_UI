using OpenQA.Selenium;
using PriceCollector.Collector.PageObjects.Base;

namespace PriceCollector.Collector.PageObjects.Gigatek
{
    public class GigatekMainPage : PageObjectBase
    {
        public GigatekMainPage(IWebDriver driver) : base (driver) { }

        public IWebElement SearchField => driver.FindElement(By.CssSelector("input#q"));
    }
}
