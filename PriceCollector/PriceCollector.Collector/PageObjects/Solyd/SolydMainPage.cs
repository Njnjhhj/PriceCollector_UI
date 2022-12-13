using OpenQA.Selenium;
using PriceCollector.Collector.PageObjects.Base;

namespace PriceCollector.Collector.PageObjects.Solyd
{
    public class SolydMainPage : PageObjectBase
    {
        public SolydMainPage(IWebDriver driver) : base(driver) { }

        public IWebElement SearchField => driver.FindElement(By.CssSelector("#search"));
    }
}
