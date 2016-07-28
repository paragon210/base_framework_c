using System;
using System.Diagnostics;
using Baseclass.Contrib.SpecFlow.Selenium.NUnit.Bindings;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace AutomationFramework.Utilities
{
    public static class DriverWait
    {
        private static WebDriverWait _wait;

        /// <summary>
        /// Set the Wait for the test using Browser.Current
        /// </summary>
        public static WebDriverWait Wait
        {
            get { return _wait ?? (_wait = new WebDriverWait(Browser.Current, TimeSpan.FromSeconds(30))); }
            set { _wait = value; }
        }


        /// <summary>
        /// Waits for page loaded by checking document ready state
        /// </summary>
        public static void WaitForPageLoaded()
        {

            Browser.Current.WaitForCondition(dri =>
            {
                string state = dri.ExecuteJs("return document.readyState").ToString();
                return state == "complete";

            }, 90
            );
        }

        /// <summary>
        /// Waits for a condition. Used primiarly with WaitForPageLoaded
        /// </summary>
        /// <typeparam name="T">Object</typeparam>
        /// <param name="obj">Type T</param>
        /// <param name="condition">bool to wait for</param>
        /// <param name="timeOut">timeout in seconds</param>
        public static void WaitForCondition<T>(this T obj, Func<T, bool> condition, int timeOut)
        {
            Func<T, bool> execute =
                (arg) =>
                {
                    try
                    {
                        return condition(arg);
                    }
                    catch (Exception e)
                    {

                        return false;
                    }
                };
            var stopWatch = Stopwatch.StartNew();
            while (stopWatch.ElapsedMilliseconds < timeOut)
            {
                if (execute(obj))
                {
                    break;
                }
            }
        }

        /// <summary>
        /// Executes JavaScript.
        /// </summary>
        /// <param name="driver">Web Driver</param>
        /// <param name="script">Script to execute</param>
        /// <returns></returns>
        internal static object ExecuteJs(this IWebDriver driver, string script)
        {
            return ((IJavaScriptExecutor)Browser.Current).ExecuteScript(script);
        }
    }
}
}
