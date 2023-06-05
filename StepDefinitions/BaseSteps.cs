using SmartlySpecflow.Utilities;
using OpenQA.Selenium;

namespace SmartlySpecflow.Steps
{
    public abstract class BaseSteps
    {
        protected readonly IWebDriver _driver;
        protected readonly ScenarioContext _scenarioContext;
        protected readonly FeatureContext _featureContext;
        public WaitHelper _wait;

        protected BaseSteps(IWebDriver driver, ScenarioContext scenarioContext, FeatureContext featureContext)
        {
            _driver = driver;
            _featureContext = featureContext ?? throw new ArgumentNullException(nameof(featureContext));
            _scenarioContext = scenarioContext ?? throw new ArgumentNullException(nameof(scenarioContext));
            _wait = new WaitHelper(_driver);
        }
    }
}
