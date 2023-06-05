using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using System;

namespace SmartlySpecflow.Drivers
{
    public class RemoteDriverFactory : AbstractDriverFactory
    {
        private readonly string _browser;
        private readonly string _gridUrl;
        private bool _headless = true;

        public RemoteDriverFactory(string browser, string gridUrl)
        {
            _browser = browser;
            _gridUrl = gridUrl;
        }

        public override DriverOptions GetOptions()
        {
            return FactoryBuilder.GetFactory(_browser).SetHeadless(_headless).GetOptions();
        }

        protected override IWebDriver BuildDriver()
        {
            return new RemoteWebDriver(new Uri(_gridUrl), GetOptions());
        }

        public override IDriverFactory SetHeadless(bool isHeadless)
        {
            _headless = isHeadless;
            return this;
        }
    }
}
