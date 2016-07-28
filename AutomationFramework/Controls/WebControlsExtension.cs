using AutomationFramework.Utilities;
using Baseclass.Contrib.SpecFlow.Selenium.NUnit.Bindings;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace AutomationFramework.Controls
{
    public static class WebControlsExtension
    {

        /// <summary>
        /// Check if element is on a page
        /// </summary>
        /// <param name="element">Element to check if exists</param>
        /// <returns>Bool</returns>
        public static bool Exists(this IWebElement element)
        {
            if (element == null)
            { return false; }
            return true;
        }


        /// <summary>
        /// A control to click and select items from Angular Material menus that have animation.
        /// </summary>
        /// <param name="selectionId">The selection id of the item to select in the list</param>
        public static void ClickAndSelectFromDropDownAngularMaterial(string selectionId)
        {
            DriverWait.Wait.Until(ExpectedConditions.ElementIsVisible(By.Id(selectionId)));
            var element = Browser.Current.FindElement(By.Id(selectionId));
            element.Click();
            DriverWait.Wait.Until(ExpectedConditions.InvisibilityOfElementLocated(By.Id(selectionId)));
        }

        /// <summary>
        /// A control to click and select items from AngularJS menus using a XPath
        /// </summary>
        /// <param name="xPath">XPath of the element</param>
        public static void ClickAndSelectFromDropDownAngularXpath(string xPath)
        {
            DriverWait.Wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(xPath)));
            var element = Browser.Current.FindElement(By.XPath(xPath));
            element.Click();
        }

        /// <summary>
        /// Selects a value from drop down list by using text
        /// </summary>
        /// <param name="element">The element that is the DDL</param>
        /// <param name="value">The value to use to find the option</param>
        public static void SelectItemByTextFromDDL(IWebElement element, string value)
        {
            SelectElement ddlElement = new SelectElement(element);
            ddlElement.SelectByText(value);
        }

        /// <summary>
        /// Selects value from drop down list by using value
        /// </summary>
        /// <param name="element">The element that is the DDL</param>
        /// <param name="value">THe Value to use to find the option</param>
        public static void SelectItemByValueFromDDL(IWebElement element, string value)
        {
            SelectElement ddlElement = new SelectElement(element);
            ddlElement.SelectByValue(value);
        }

        /// <summary>
        /// Verifies the text from a selected drop down
        /// </summary>
        /// <param name="element"></param>
        /// <param name="expected"></param>
        public static void VerifySelectedTextFromDDL(IWebElement element, string expected)
        {
            SelectElement ddlElement = new SelectElement(element);
            string actual = ddlElement.SelectedOption.Text;
            Assert.IsTrue(expected.Equals(actual));
        }

        /// <summary>
        /// Switches windows and finds new one by CssSelector
        /// </summary>
        /// <param name="cssString">The CSS selector to use</param>
        public static void SwitchWindowToApplicationPage(string cssString)
        {
            PopupWindowFinder finder = new PopupWindowFinder(Browser.Current);
            string newHandle = finder.Click(Browser.Current.FindElement(By.CssSelector(cssString)));
            Assert.IsNotNullOrEmpty(newHandle);
            Browser.Current.SwitchTo().Window(newHandle);
        }

    }
}
