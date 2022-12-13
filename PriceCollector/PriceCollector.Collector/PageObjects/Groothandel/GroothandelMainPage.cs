using OpenQA.Selenium;
using PriceCollector.Collector.PageObjects.Base;

namespace PriceCollector.Collector.PageObjects.Groothandel
{
    public class GroothandelMainPage : PageObjectBase
    {
        public GroothandelMainPage(IWebDriver driver) : base(driver) { }

        public IWebElement SearchField => driver.FindElement(By.CssSelector("#dgwt-wcas-search-input-1"));
    }
}
