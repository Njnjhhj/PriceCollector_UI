using OpenQA.Selenium;
using PriceCollector.CollectorStgPro.PageObjects;
using PriceCollector.CollectorStgPro.PageObjects.Base;

namespace PriceCollector.CollectorStgPro.Flows
{
    public class SearchFlow : PageObjectBase
    {
        private readonly StgSearchPage _stgSearchPage;

        public SearchFlow(IWebDriver driver) : base(driver)
        {
            _stgSearchPage = new StgSearchPage(driver);
        }
        
        public void SearchByProductId(string productId)
        {
            _stgSearchPage.SearchField.Clear();
            _stgSearchPage.SearchField.SendKeys(productId);
            _stgSearchPage.SearchField.SendKeys(Keys.Enter);
        }
    }
}
