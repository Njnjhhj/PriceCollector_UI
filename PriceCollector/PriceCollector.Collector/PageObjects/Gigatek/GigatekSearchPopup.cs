using OpenQA.Selenium;
using PriceCollector.Collector.PageObjects.Base;
using PriceCollector.Collector.WebUtils;
using System;

namespace PriceCollector.Collector.PageObjects.Gigatek
{
    public class GigatekSearchPopup : PageObjectBase
    {
        public GigatekSearchPopup(IWebDriver driver) : base(driver) { }

        public IWebElement SearchField => driver.UsingDriverImplicitTimeout(() => driver.FindElement(By.CssSelector("#sfs-shbx-z-or")), TimeSpan.FromMilliseconds(1000));
        public IWebElement PriceValueElement(string itemId) => driver.UsingDriverImplicitTimeout(() => ProductItemBlock(itemId).FindElement(By.XPath(".//div[@class='sfs-vignet__price']")), TimeSpan.FromMilliseconds(1000));
        
        private IWebElement ProductItemBlock(string itemId) => SearchResultArea.FindElement(By.XPath($".//div[@class='sfs-vignet__ref' and contains(text(), '{itemId}')]/ancestor::div[@class='sfs-vignet__content']"));
        private IWebElement SearchResultArea => driver.FindElement(By.CssSelector(".sfs-r-c-t"));
    }
}
