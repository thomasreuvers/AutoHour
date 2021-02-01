using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using AutoHour.ExtensionMethods;
using AutoHour.Objects;
using AutoHour.Pages;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace AutoHour.Handlers
{
    public class WebHandler
    {
        private readonly IWebDriver _webDriver;

        public WebHandler()
        {
            // Hide windows
            var options = new ChromeOptions();
            // options.AddArgument("headless");

            // Hide debug cmd
            var chromeDriverService = ChromeDriverService.CreateDefaultService();
            chromeDriverService.HideCommandPromptWindow = true;

            _webDriver = new ChromeDriver(chromeDriverService, options);
            _webDriver.Manage().Window.Maximize();
        }

        public void Start(HourFillForm hourFillForm)
        {
            // Goto TrajectPlannerPage
            var trajectPlannerPage = new TrajectPlannerPage(_webDriver);

            // Login user by given username and password
            _webDriver.EnterValue(new List<Tuple<By, string>>
            {
                new Tuple<By, string>(By.Id(OnderwijsOnlinePage.UserNameInput), hourFillForm.Username),
                new Tuple<By, string>(By.Id(OnderwijsOnlinePage.PassWordInput), hourFillForm.Password),
            });

            // Click the login button
            _webDriver.ClickElement(By.Id(OnderwijsOnlinePage.LoginSubmitButton));

            if (_webDriver.Exists(By.Id(OnderwijsOnlinePage.LoginErrorText), 250))
            {
                ExceptionHandler.ShowException("Invalid input", _webDriver.FindElement(By.Id(OnderwijsOnlinePage.LoginErrorText)).Text, _webDriver);
                // throw new Exception(_webDriver.FindElement(By.Id(OnderwijsOnlinePage.LoginErrorText)).Text);
            }

            // Move cursor to bpv menu tab
            _webDriver.MoveCursorToElement(By.XPath(TrajectPlannerPage.BpvMenuTab));

            // Click on bpv anchor tag
            _webDriver.ClickElement(By.XPath(TrajectPlannerPage.BpvAnchor));

            // Check if the bpv page contains a internship
            if (!_webDriver.Exists(By.XPath(TrajectPlannerPage.FirstInternship)))
            {
                ExceptionHandler.ShowException("Non existing", "The requested element does not exist on the page.", _webDriver);
            }

            // Click first internship in table
            _webDriver.ClickElement(By.XPath(TrajectPlannerPage.FirstInternship));

            if (_webDriver.WindowHandles.Count <= 1)
            {
                ExceptionHandler.ShowException("Non existing", "The requested window does not exist.", _webDriver);
            }

            // Switch to the last open window
            _webDriver.SwitchTo().Window(_webDriver.WindowHandles.Last());

            // Open the hours tab
            _webDriver.ClickElement(By.XPath(TrajectPlannerPage.HoursTab));

            // Click the new hours button
            Thread.Sleep(250);
            _webDriver.ClickElement(By.XPath(TrajectPlannerPage.NewHoursBtn));
            Thread.Sleep(250);

            /*
             * Set the value's of the required input fields
             * Bug: Click the input fields for time because else not being able to save
             */

            _webDriver.SetElementValueById(new Tuple<string, string>(TrajectPlannerPage.DateInput, hourFillForm.Date.ToString("dd-MM-yyyy")));

            _webDriver.ClickElement(By.Id(TrajectPlannerPage.StartingTimeInput));
            _webDriver.EnterValue(new Tuple<By, string>(By.Id(TrajectPlannerPage.StartingTimeInput), hourFillForm.StartTime.ToShortTimeString()));

            _webDriver.ClickElement(By.Id(TrajectPlannerPage.EndingTimeInput));
            _webDriver.EnterValue(new Tuple<By, string>(By.Id(TrajectPlannerPage.EndingTimeInput), hourFillForm.EndTime.ToShortTimeString()));

            // Enter the description value
            _webDriver.EnterValue(new Tuple<By, string>(By.Id(TrajectPlannerPage.DescriptionInput), hourFillForm.Description));

            // Click the save button
            _webDriver.ClickElement(By.XPath(TrajectPlannerPage.SaveBtn));
            Thread.Sleep(2000);

            // Everything is done close windows and dispose web driver
            // Close();
        }

        // Close the web driver
        public void Close()
        {
            try
            {
                _webDriver.Quit();
                _webDriver.Dispose();
            }
            catch
            {
                // ignored
            }
        }
    }
}
