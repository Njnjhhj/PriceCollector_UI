using OpenQA.Selenium;
using PriceCollector.Collector.PageObjects.Base;

namespace PriceCollector.Collector.PageObjects.Zelektro
{
    public class ZelektroMainPage : PageObjectBase
    {
        public ZelektroMainPage(IWebDriver driver) : base(driver) { }

        public IWebElement SearchField => TopMenu.FindElement(By.CssSelector("input#searchBox"));

        private IWebElement TopMenu => driver.FindElement(By.CssSelector("#top_menu_collapse"));
    }
}
