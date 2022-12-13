using System;
using System.Collections.Generic;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using PriceCollector.CollectorStgPro.DataModels;
using PriceCollector.Core.Enums;

namespace PriceCollector.CollectorStgPro.Base
{
    [TestFixture]
    public class BaseCollectorStgProUi : BaseCollectorStgProConfigs
    {
        // ReSharper disable once InconsistentNaming
        protected IWebDriver driver;

        [OneTimeSetUp]
        public void BeforeAllUi()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromMilliseconds(30000);
        }

        [OneTimeTearDown]
        public void AfterAllUi()
        {
            driver.Quit();
        }

        public void OpenWebPage()
        {
            var webSite = Website;

            if (webSite != null) 
                OpenPage(webSite.Uri);
            else
                Console.WriteLine("ERROR! Website is null.");
        }

        public void PrintResultsToConsole(List<StgItemData> resultList, Separator separator)
        {
            foreach (var result in resultList)
            {
                Console.WriteLine(result.LogString(separator));
            }
        }

        private void OpenPage(Uri webAddress)
        {
            driver.Navigate().GoToUrl(webAddress);
        }
    }
}
