using OpenQA.Selenium;

namespace SmartlySpecflow.Models
{
    public class HomePage : BasePage
    {
        public readonly By btnCheckItOut = By.CssSelector(".button");
        public readonly By pageBody = By.CssSelector("#left");
        public readonly By mainMenu = By.CssSelector(".js-main-menu-button-text");
        public readonly By optionMenuPayees = By.CssSelector("a[href^='/client/payees");
        public readonly By optionMenuPayOrTransfer = By.CssSelector(".js-main-menu-paytransfer");
        

        

        public HomePage(IWebDriver driver) : base(driver)
        {

        }

        public void NavigateToBNZDemoDashboardPage()
        {
            ClickElement(btnCheckItOut);
        }

        public bool ValidateBNZDashboardPage()
        {
            _wait.WaitForPageLoadComplete();
            return IsElementExists(pageBody);
        }

        public void ClickMainMenu()
        {
            ClickElement(mainMenu);
        }

        public void ClickPayeesOptionMenu()
        {
            ClickElement(optionMenuPayees);
        }

        public void ClickPayOrTransferOptionMenu()
        {
            ClickElement(optionMenuPayOrTransfer);
        }
    }
}
