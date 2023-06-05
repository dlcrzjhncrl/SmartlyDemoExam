using OpenQA.Selenium;
using OpenQA.Selenium.Support.Events;
using System;

namespace SmartlySpecflow.Drivers
{
    public abstract class AbstractDriverFactory : IDriverFactory
    {
        public IWebDriver GetDriver()
        {
            var driver = new EventFiringWebDriver(BuildDriver());

            //log to console -- registering events 
            driver.ElementClicked += new EventHandler<WebElementEventArgs>((object sender, WebElementEventArgs e) =>
            {
                Console.WriteLine("Clicked element " + e.Element.TagName);
            });
            driver.FindElementCompleted += new EventHandler<FindElementEventArgs>((object sender, FindElementEventArgs e) =>
            {
                Console.WriteLine("Found element " + e.FindMethod);
            });
            return driver;
        }

        public abstract DriverOptions GetOptions();
        public abstract IDriverFactory SetHeadless(bool isHeadless);
        protected abstract IWebDriver BuildDriver();
    }
}
