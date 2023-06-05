using BoDi;
using OpenQA.Selenium;
using System.Globalization;
using System.IO;
using System;
using TechTalk.SpecFlow.Infrastructure;
using TechTalk.SpecFlow;
using SmartlySpecflow.Utilities;
using SmartlySpecflow.Drivers;

namespace SmartlySpecflow.Hooks
{
    [Binding]
    public sealed class UIHooks
    {
        // For additional details on SpecFlow hooks see http://go.specflow.org/doc-hooks
        private readonly FeatureContext _featureContext;
        private readonly ScenarioContext _scenarioContext;
        private readonly IObjectContainer _objectContainer;
        private readonly ISpecFlowOutputHelper _specflowOutputHelper;

        public UIHooks(IObjectContainer objectContainer, FeatureContext featureContext, ScenarioContext scenarioContext, ISpecFlowOutputHelper specFlowOutputHelper)
        {
            _featureContext = featureContext ?? throw new ArgumentNullException(nameof(featureContext));
            _scenarioContext = scenarioContext ?? throw new ArgumentNullException(nameof(scenarioContext));
            _objectContainer = objectContainer;
            _specflowOutputHelper = specFlowOutputHelper;
        }

        [BeforeScenario]
        public void BeforeScenario()
        {
            var browser = AppSettings.Browser;
            var headless = bool.Parse(AppSettings.Headless);
            var implicitWait = int.Parse(AppSettings.ImplicitWait, CultureInfo.InvariantCulture);
            var Url = new Uri(AppSettings.BNZUrl);


            var driver = FactoryBuilder.GetFactory(browser).SetHeadless(headless).GetDriver();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(implicitWait);
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl(Url);
            _featureContext.FeatureContainer.RegisterInstanceAs(driver);
        }

        [AfterScenario]
        public void AfterScenario()
        {
            IWebDriver driver = _objectContainer.Resolve<IWebDriver>();
            //var LoginPage = new LoginPage(driver);
            //LoginPage.Logout();
            driver.Quit();
        }

        public void AfterStep()
        {
            IWrapsDriver wrapperAccess = (IWrapsDriver)_objectContainer.Resolve<IWebDriver>();
            IWebDriver driver = wrapperAccess.WrappedDriver;

            if (_scenarioContext.TestError != null)
            {
                var filename = Path.ChangeExtension(Path.Combine(_scenarioContext.ScenarioInfo.Title.Replace(" ", "_", StringComparison.InvariantCultureIgnoreCase)), "png");
                var screenshot = ((ITakesScreenshot)driver).GetScreenshot();
                screenshot.SaveAsFile(filename);
                _specflowOutputHelper.AddAttachment(filename);
            }
        }
    }
}
