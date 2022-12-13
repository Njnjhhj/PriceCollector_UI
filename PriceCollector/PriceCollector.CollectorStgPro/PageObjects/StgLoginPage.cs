using OpenQA.Selenium;
using PriceCollector.CollectorStgPro.PageObjects.Base;

namespace PriceCollector.CollectorStgPro.PageObjects
{
    public class StgLoginPage : PageObjectBase
    {
        public StgLoginPage(IWebDriver driver) : base(driver) { }

        public IWebElement CustomerIdField => driver.FindElement(By.CssSelector("div.login-field #Customerid"));
        public IWebElement UsernameField => driver.FindElement(By.CssSelector("div.login-field #Username"));
        public IWebElement PasswordField => driver.FindElement(By.CssSelector("div.login-field #Password"));
        public IWebElement LoginButton => driver.FindElement(By.CssSelector("div.login-field input.login-button"));
    }
}
