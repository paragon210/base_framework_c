using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace AutomationFramework.BaseObjects
{
    [Binding]
    public class BaseSteps : Base
    {
        private IWebDriver currentDriver = null;   
    }
}
