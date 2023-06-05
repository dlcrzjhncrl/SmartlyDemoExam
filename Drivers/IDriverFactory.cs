using OpenQA.Selenium;

namespace SmartlySpecflow.Drivers
{
    public interface IDriverFactory
    {
        IWebDriver GetDriver();
        DriverOptions GetOptions();
        IDriverFactory SetHeadless(bool isHeadless);
    }
}
