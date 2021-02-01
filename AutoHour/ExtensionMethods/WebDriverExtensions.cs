using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace AutoHour.ExtensionMethods
{
    public static class WebDriverExtensions
    {

        /// <summary>
        /// Enter given data into given elements
        /// </summary>
        /// <param name="current">current web driver</param>
        /// <param name="elementTuples">List of tuples that contains the web driver elements and data</param>
        public static void EnterValue(this IWebDriver current, IEnumerable<Tuple<By, string>> elementTuples)
        {
            foreach (var (locator, data) in elementTuples)
            {
                try
                {
                    current.FindElement(locator).SendKeys(data);
                }
                catch (NoSuchElementException e)
                {
                    // TODO: Implement exception handler
                    throw e;
                }
                
            }
        }

        /// <summary>
        /// Enter given data into element
        /// </summary>
        /// <param name="current">current web driver</param>
        /// <param name="elementTuple">Tuple that contains the element and data</param>
        public static void EnterValue(this IWebDriver current, Tuple<By, string> elementTuple)
        {
            try
            {
                var (locator, data) = elementTuple;
                current.FindElement(locator).SendKeys(data);
            }
            catch (NoSuchElementException e)
            {
                // TODO: Implement exception handler
                throw e;
            }
        }

        /// <summary>
        /// Execute given javascript
        /// </summary>
        /// <param name="current">current web driver</param>
        /// <param name="script">Script to execute</param>
        public static void ExecuteJavascript(this IWebDriver current, string script)
        {
            var js = (IJavaScriptExecutor) current;
            js.ExecuteScript(script);
        }

        /// <summary>
        /// Set the value's of the given locators Id to the given data
        /// </summary>
        /// <param name="current">current web driver</param>
        /// <param name="elementTuples">List of tuples that contains the elements and data</param>
        public static void SetElementValueById(this IWebDriver current, IEnumerable<Tuple<string, string>> elementTuples)
        {
            var js = (IJavaScriptExecutor) current;
            foreach (var (id, data) in elementTuples)
            {
                js.ExecuteScript($"document.getElementById('{id}').value=\"{data}\"");
            }
        }

        /// <summary>
        /// Set the value of the given locator Id to the given data
        /// </summary>
        /// <param name="current">current web driver</param>
        /// <param name="elementTuple">Tuple that contains the element and data</param>
        public static void SetElementValueById(this IWebDriver current, Tuple<string, string> elementTuple)
        {
            var js = (IJavaScriptExecutor)current;
            var (id, data) = elementTuple;
            js.ExecuteScript($"document.getElementById('{id}').value=\"{data}\"");
        }

        /// <summary>
        /// Click the given element
        /// </summary>
        /// <param name="current">current web driver</param>
        /// <param name="locator">The element to click</param>
        public static void ClickElement(this IWebDriver current, By locator)
        {
            try
            {
                current.FindElement(locator).Click();
            }
            catch (NoSuchElementException e)
            {
                // TODO: Implement exception handler
                throw e;
            }
        }

        /// <summary>
        /// Move the cursor to the given element
        /// </summary>
        /// <param name="current">current web driver</param>
        /// <param name="locator">Element to move the cursor to</param>
        public static void MoveCursorToElement(this IWebDriver current, By locator)
        {
            try
            {
                var action = new Actions(current);
                action.MoveToElement(current.FindElement(locator)).Perform();
            }
            catch (NoSuchElementException e)
            {
                // TODO: Implement exception handler
                throw e;
            }
        }

        /// <summary>
        /// Check if given element exists
        /// </summary>
        /// <param name="current">current web driver</param>
        /// <param name="locator">The element to check</param>
        /// <param name="toWait">Total time to wait</param>
        /// <returns></returns>
        public static bool Exists(this IWebDriver current, By locator, long toWait = 10000)
        {
            if (WaitForElementDisplayed(current, locator, toWait))
            {
                return true;
            }

            try
            {
                current.FindElement(locator);
            }
            catch
            {
                return false;
            }

            return false;
        }

        /// <summary>
        /// Wait for x milliseconds while checking if element is visible
        /// </summary>
        /// <param name="current">current web driver</param>
        /// <param name="locator">Element to wait for</param>
        /// /// <param name="toWait">Total waiting time</param>
        /// <returns></returns>
        private static bool WaitForElementDisplayed(this IWebDriver current, By locator, long toWait)
        {
            try
            {
                var wait = new WebDriverWait(current, TimeSpan.FromMilliseconds(toWait));
                return wait.Until(ElementDisplayed(current, locator));
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Check if element is visible
        /// </summary>
        /// <param name="current">current web driver</param>
        /// <param name="locator">Element to check</param>
        /// <returns></returns>
        private static Func<IWebDriver, bool> ElementDisplayed(this ISearchContext current, By locator)
        {
            return (driver) =>
            {
                try
                {
                    var e = current.FindElement(locator);
                    return e.Displayed;
                }
                catch (StaleElementReferenceException)
                {
                    return false;
                }
                catch (NoSuchElementException)
                {
                    return false;
                }
            };
        }
    }
}
