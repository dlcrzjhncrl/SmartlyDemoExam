using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System.Globalization;
using SELENIUM_EXTRAS = SeleniumExtras.WaitHelpers.ExpectedConditions;


namespace SmartlySpecflow.Utilities
{
    public class WaitHelper
    {
        private readonly WebDriverWait _waitHelper;
        private readonly IWebDriver _driver;

        private const int DefaultSleepTimeInMs = 30000;
        private readonly int SleepTimeInMs;

        public WaitHelper(IWebDriver driver)
        {
            SleepTimeInMs = Convert.ToInt32(Environment.GetEnvironmentVariable("SleepTimeInMs") ?? DefaultSleepTimeInMs.ToString(CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);
            _waitHelper = new WebDriverWait(driver, TimeSpan.FromMilliseconds(SleepTimeInMs));
            _driver = driver;
        }

        public WaitHelper(IWebDriver driver, double timeSpan)
        {
            _waitHelper = new WebDriverWait(driver, TimeSpan.FromSeconds(timeSpan));
            _driver = driver;
        }

        public void WaitForElementClickable(By locator)
        {
            _waitHelper.Until(SELENIUM_EXTRAS.ElementToBeClickable(locator));
        }

        public void WaitForElementClickable(IWebElement element)
        {
            _waitHelper.Until(SELENIUM_EXTRAS.ElementToBeClickable(element));
        }

        public void WaitForElementExists(By locator)
        {
            _waitHelper.Until(SELENIUM_EXTRAS.ElementExists(locator));
        }

        public void WaitForElementVisible(By locator)
        {
            _waitHelper.Until(SELENIUM_EXTRAS.ElementIsVisible(locator));
        }

        public void WaitForElementSelected(IWebElement element)
        {
            _waitHelper.Until(SELENIUM_EXTRAS.ElementToBeSelected(element));
        }

        public void WaitForElementSelected(By locator)
        {
            _waitHelper.Until(SELENIUM_EXTRAS.ElementToBeSelected(locator));
        }

        public void WaitUntilUrlContains(string fraction)
        {
            _waitHelper.Until(SELENIUM_EXTRAS.UrlContains(fraction));
        }

        /* Wait for background page task to finish -- JQuery running */
        public void WaitForJQueryToBeInactive()
        {

            var isJqueryUsed = (bool)((IJavaScriptExecutor)_driver).ExecuteScript("return (typeof(jQuery) != 'undefined')");

            if (isJqueryUsed)
            {
                while (true)
                {
                    var ajaxIsComplete = (bool)((IJavaScriptExecutor)_driver).ExecuteScript("return jQuery.active == 0");
                    if (ajaxIsComplete)
                        break;
                    try
                    {
                        Thread.Sleep(SleepTimeInMs);
                    }
                    catch (ThreadInterruptedException e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
            }
        }

        /* Wait for the Page to be loaded after a 'Processing' action - DOM */
        public void WaitForPageLoadComplete()
        {
            _waitHelper.Until(_driver => ((IJavaScriptExecutor)_driver).ExecuteScript("return document.readyState").Equals("complete"));
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);
            Thread.Sleep(1000);
        }
    }
}
