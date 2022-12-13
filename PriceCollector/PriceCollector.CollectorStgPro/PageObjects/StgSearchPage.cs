using OpenQA.Selenium;
using PriceCollector.CollectorStgPro.PageObjects.Base;

namespace PriceCollector.CollectorStgPro.PageObjects
{
    public class StgSearchPage : PageObjectBase
    {
        public StgSearchPage(IWebDriver driver) : base(driver) { }

        public IWebElement SearchField => driver.FindElement(By.CssSelector("#searchterm"));
    }
}
