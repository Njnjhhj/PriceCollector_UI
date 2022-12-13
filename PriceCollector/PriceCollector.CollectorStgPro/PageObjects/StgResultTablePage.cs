using System;
using OpenQA.Selenium;
using PriceCollector.Collector.WebUtils;
using PriceCollector.CollectorStgPro.PageObjects.Base;

namespace PriceCollector.CollectorStgPro.PageObjects
{
    public class StgResultTablePage : PageObjectBase
    {
        public StgResultTablePage(IWebDriver driver) : base(driver) { }

        public IWebElement ArticleElement => driver.UsingDriverImplicitTimeout(() =>driver.FindElement(By.XPath("//div[@class='table-column fullscreen-700']//div[@class='value']/a")), TimeSpan.FromMilliseconds(500));
        public IWebElement OrderedElement => driver.UsingDriverImplicitTimeout(() => driver.FindElement(By.XPath("//div[@class='table-column fullscreen-700']//div[@class='value']//div[@class='times-ordered']")), TimeSpan.FromMilliseconds(500));
        public IWebElement StockElement => driver.UsingDriverImplicitTimeout(() => GetTradeElement(_stockColumnName).FindElement(By.XPath(".//i[contains(@style, 'display')]")), TimeSpan.FromMilliseconds(500));
        public IWebElement NetPriceElement => driver.UsingDriverImplicitTimeout(() => GetTradeElement(_netPriceColumnName).FindElement(By.XPath(".//p")), TimeSpan.FromMilliseconds(500));

        private IWebElement GetTradeElement(string columnName) => driver.FindElement(By.XPath($"//div[@class='table-column']/label[text()='{columnName}']/ancestor::div[@class='table-column']"));
        private string _stockColumnName => "Voorraad";
        private string _netPriceColumnName => "Nettoprijs";
    }
}
