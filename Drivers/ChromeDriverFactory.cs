using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;

namespace SmartlySpecflow.Drivers
{
    public class ChromeDriverFactory : AbstractDriverFactory
    {
        private bool _headless = false;

        public override DriverOptions GetOptions()
        {
            var options = new ChromeOptions();
            options.AddArguments("--disable-gpu",
                      "--ignore-certificate-errors",
                      "no-sandbox",
                      "--window-size=1920,1080");
            /*Files download location via browser*/
            options.AddUserProfilePreference("download.default_directory", AppDomain.CurrentDomain.BaseDirectory);
            if (_headless)
                options.AddArguments("--headless");
            return options;
        }

        protected override IWebDriver BuildDriver()
        {
            return new ChromeDriver(ChromeDriverService.CreateDefaultService(), (ChromeOptions)GetOptions(), TimeSpan.FromMinutes(2));
        }

        public override IDriverFactory SetHeadless(bool isHeadless)
        {
            _headless = isHeadless;
            return this;
        }
    }
}
