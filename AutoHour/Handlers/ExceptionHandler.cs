using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using OpenQA.Selenium;

namespace AutoHour.Handlers
{
    public static class ExceptionHandler
    {

        public static void ShowException(string title, string content, MessageBoxButtons boxButtons = MessageBoxButtons.OK, MessageBoxIcon boxIcon = MessageBoxIcon.Error)
        {
            MessageBox.Show(content, title, boxButtons, boxIcon);
        }

        public static void ShowException(string title, string content, IWebDriver driver, MessageBoxButtons boxButtons = MessageBoxButtons.OK, MessageBoxIcon boxIcon = MessageBoxIcon.Error)
        {
            driver.Quit();
            driver.Dispose();

            MessageBox.Show(content, title, boxButtons, boxIcon);
        }
    }
}
