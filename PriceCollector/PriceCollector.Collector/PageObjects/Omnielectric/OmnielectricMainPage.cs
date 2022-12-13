using OpenQA.Selenium;
using PriceCollector.Collector.PageObjects.Base;

namespace PriceCollector.Collector.PageObjects.Omnielectric
{
    public class OmnielectricMainPage : PageObjectBase
    {
        public OmnielectricMainPage(IWebDriver driver) : base(driver) { }

        public IWebElement SearchField => driver.FindElement(By.CssSelector("div.ecwid-search-widget input"));
    }
}
