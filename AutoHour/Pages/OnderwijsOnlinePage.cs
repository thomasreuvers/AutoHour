using System;
using System.Collections.Generic;
using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace AutoHour.Pages
{
    public partial class OnderwijsOnlinePage
    {
        private readonly IWebDriver _webDriver;

        public OnderwijsOnlinePage(IWebDriver driver)
        {
            _webDriver = driver;
            _webDriver.Navigate().GoToUrl(MainPageUrl);
        }
    }
}
