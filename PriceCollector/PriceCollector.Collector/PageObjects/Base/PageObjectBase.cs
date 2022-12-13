﻿using OpenQA.Selenium;

namespace PriceCollector.Collector.PageObjects.Base
{
    public abstract class PageObjectBase
    {
        protected readonly IWebDriver driver;

        protected PageObjectBase(IWebDriver driver)
        {
            this.driver = driver;
        }
    }
}
