using System;
using System.Collections.Generic;
using System.Text;
using OpenQA.Selenium;

namespace AutoHour.Pages
{
    public partial class TrajectPlannerPage
    {
        private readonly IWebDriver _webDriver;

        public TrajectPlannerPage(IWebDriver webDriver)
        {
            _webDriver = webDriver;
            _webDriver.Navigate().GoToUrl(MainPageUrl);
        }
    }
}
