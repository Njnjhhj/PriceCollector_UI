using OpenQA.Selenium;

namespace PriceCollector.Core.Extensions
{
    public static class WebElementExtensions
    {
        public static void HighlightElement(this IWebElement element, IWebDriver driver)
        {
            var jsDriver = (IJavaScriptExecutor)driver;
            string highlightJavascript = @"arguments[0].style.cssText = ""border-width: 2px; border-style: solid; border-color: red"";";
            jsDriver.ExecuteScript(highlightJavascript, new object[] { element });
        }
    }
}
