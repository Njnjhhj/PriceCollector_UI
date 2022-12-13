using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using PriceCollector.Collector.Enums;

namespace PriceCollector.Collector.WebUtils
{
    public static class WebElementUtils
    {
        public static void MoveToElement(this IWebDriver driver, IWebElement element)
        {
            Actions action = new Actions(driver);
            action.MoveToElement(element).Perform();
        }

        public static void GoToDefaultFrameImplicitly(this IWebDriver driver)
        {
            GoToFrameImplicit(driver.FrameSwitchToDefault, SwitchToFrameActions.SwitchToDefaultContent);
        }

        public static void DetectFrame(this IWebDriver driver)
        {
            driver.GoToDefaultFrameImplicitly();

            var iframes = driver.GetFrames();
            while (iframes != null && iframes.Count > 0)
            {
                GoToFrameImplicit(() => driver.FrameSwitchToLast(driver.GetFrames), SwitchToFrameActions.SwitchToSelectedFrame);
                iframes = driver.GetFrames();
            }
        }

        public static List<IWebElement> GetFrames(this IWebDriver driver, string attributeName, string attributeValue)
        {
            var frames = driver.GetFrames()
            .FindAll(x =>
            {
                var src = x.GetAttribute(attributeName); //"style"
                return (src.Contains(attributeValue)); //"display: inline !important"
            });

            return frames;
        }

        public static List<IWebElement> GetFrames(this IWebDriver driver)
        {
            return driver.GetFrames(By.XPath("//iframe"));
        }

        private static void FrameSwitchToDefault(this IWebDriver driver)
        {
            driver.SwitchTo().DefaultContent();
        }
        private static void FrameSwitchToLast(this IWebDriver driver, Func<List<IWebElement>> getFrames)
        {
            var frames = getFrames();
            if (frames == null || !frames.Any())
                return;

            driver.FrameSwitchTo(frames[frames.Count - 1]);
        }

        private static void FrameSwitchTo(this IWebDriver driver, IWebElement frame)
        {
            driver.SwitchTo().Frame(frame);
        }
        private static void GoToFrameImplicit(Action switchFrameAction, SwitchToFrameActions actionDescription)
        {
            var waitForSuccess = TimeSpan.FromSeconds(5);
            var waitIfAttemptFailed = TimeSpan.FromMilliseconds(500);

            var timer = new Stopwatch();
            timer.Start();

            while (timer.Elapsed <= waitForSuccess)
            {
                try
                {
                    switchFrameAction();
                    return;
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Exception ({e.GetType().UnderlyingSystemType}) at GoToFrameImplicit for {actionDescription} (Elapsed {timer.Elapsed}): {e}. Message: '{e.Message}'. Inner exception: '{e.InnerException}'");
                }
                System.Threading.Thread.Sleep(waitIfAttemptFailed);
            }
            throw new WebDriverTimeoutException($"Failed to {actionDescription} at GoToFrameImplicit. Timed out after {timer.Elapsed}");
        }

        public static T UsingDriverImplicitTimeout<T>(this IWebDriver driver, Func<T> theMethod, TimeSpan timeOut)
        {
            var initialImplicitWait = driver.Manage().Timeouts().ImplicitWait;

            try
            {
                driver.Manage().Timeouts().ImplicitWait = timeOut;
                return theMethod();
            }
            finally
            {
                driver.Manage().Timeouts().ImplicitWait = initialImplicitWait;
            }
        }

        private static List<IWebElement> GetFrames(this IWebDriver driver, By by)
        {
            try
            {
                var frames = driver.UsingDriverImplicitTimeout(() => driver.FindElements(by), TimeSpan.Zero).ToList();
                return frames;
            }
            catch (WebDriverException e)
            {
                Console.WriteLine(e.ToString());
                return null;
            }
        }
    }
}
