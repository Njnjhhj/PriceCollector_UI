using OpenQA.Selenium;
using PriceCollector.CollectorStgPro.Configuration;
using PriceCollector.CollectorStgPro.PageObjects;
using PriceCollector.CollectorStgPro.PageObjects.Base;

namespace PriceCollector.CollectorStgPro.Flows
{
    public class LoginFlow : PageObjectBase
    {
        private StgLoginPage _loginPage;

        public LoginFlow(IWebDriver driver) : base(driver)
        {
            _loginPage = new StgLoginPage(driver);
        }

        public void Login(Credentials credentials)
        {
            InsertCustomerId(credentials.CustomerId);
            InsertUsername(credentials.Username);
            InsertPassword(credentials.Password);
            ClickLoginButton();
        }
        
        private void InsertCustomerId(string customerId)
        {
            _loginPage.CustomerIdField.SendKeys(customerId);
        }

        private void InsertUsername(string username)
        {
            _loginPage.UsernameField.SendKeys(username);
        }

        private void InsertPassword(string password)
        {
            _loginPage.PasswordField.SendKeys(password);
        }

        private void ClickLoginButton()
        {
            _loginPage.LoginButton.Click();
        }
    }
}
