using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using PriceCollector.Collector.DataModels;
using PriceCollector.Core.Enums;

namespace PriceCollector.Collector.Base
{
    [TestFixture]
    public class BaseCollectorUi : BaseCollectorConfigs
    {
        // ReSharper disable once InconsistentNaming
        protected IWebDriver driver;

        [OneTimeSetUp]
        public void BeforeAllUi()
        {
            driver = new ChromeDriver();
            Thread.Sleep(5000); //TODO: set due to error on chrome v.103
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromMilliseconds(30000);
        }

        [OneTimeTearDown]
        public void AfterAllUi()
        {
            driver.Quit();
        }

        public void OpenWebPage(string siteName)
        {
            var webSite = Websites.FirstOrDefault(x => x.Name.Equals(siteName));

            if (webSite != null) 
                OpenPage(webSite.Uri);
            else
                Console.WriteLine("ERROR! Website is null.");
        }

        public void PrintResultsToConsole(List<ItemData> resultList, Separator separator)
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
