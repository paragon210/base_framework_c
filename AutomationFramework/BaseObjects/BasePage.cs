using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Baseclass.Contrib.SpecFlow.Selenium.NUnit.Bindings;
using OpenQA.Selenium.Support.PageObjects;
using TechTalk.SpecFlow;

namespace AutomationFramework.BaseObjects
{
    public class BasePage
    {
        /// <summary>
        /// CurrentPage object that stores the current page type so it can be accessed across steps.
        /// </summary>
        public BasePage _CurrentPage
        {
            get { return (BasePage)ScenarioContext.Current["currentPage"]; }
            set { ScenarioContext.Current["currentPage"] = value; }
        }

        /// <summary>
        /// Instance of page to draw methods from. 
        /// </summary>
        /// <typeparam name="TPage">The type of page to act as.</typeparam>
        /// <returns>BHG Page</returns>
        public TPage As<TPage>() where TPage : BasePage
        {
            return (TPage)this;
        }

        /// <summary>
        /// Set the acting page
        /// </summary>
        /// <typeparam name="TPage">The type of page to return an instance of.</typeparam>
        /// <returns>Returns a page instance.</returns>
        protected TPage GetInstance<TPage>() where TPage : BasePage, new()
        {
            TPage pageInstance = new TPage()
            {
                // _driver = DriverContext.Driver
                _driver = Browser.Current
            };
            //PageFactory.InitElements(DriverContext.Driver, pageInstance);
            PageFactory.InitElements(Browser.Current, pageInstance);
            return pageInstance;
        }
    }
}

