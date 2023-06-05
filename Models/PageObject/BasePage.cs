using SmartlySpecflow.Utilities;
using OpenQA.Selenium;

namespace SmartlySpecflow.Models
{
    public abstract class BasePage
    {
        protected IWebDriver _driver;
        public WaitHelper _wait;

        protected BasePage(IWebDriver driver)
        {
            _driver = driver;
            _wait = new WaitHelper(_driver);
        }

        public void ClickElement(By element)
        {
            IWebElement newElement = _driver.FindElement(element);
            try
            {
                newElement.Click();
            }
            catch (StaleElementReferenceException)
            {

            }
        }

        public bool IsElementExists(By element)
        {
            ApplyImplicitWait();
            IList<IWebElement> elements = _driver.FindElements(element).ToList();
            if (elements.Count != 0)
                return true;
            else
                return false;
        }

        public void ApplyImplicitWait()
        {
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);
        }
    }
}
