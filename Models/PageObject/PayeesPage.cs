using OpenQA.Selenium;

namespace SmartlySpecflow.Models
{
    public class PayeesPage : BasePage
    {
        public readonly By pageBody = By.CssSelector(".CustomPage-heading");
        public readonly By btnAdd = By.CssSelector(".Button.Button--sub.Button--translucid.js-add-payee");
        public readonly By ddlPayeeName = By.CssSelector("[data-cb-type='new-payee']");
        public readonly By btnAddModal = By.CssSelector(".js-submit.Button.Button--primary");
        public readonly By msgPayeeAdded = By.CssSelector("#notification .message");
        public readonly By msgErrMandatoryName = By.CssSelector(".error-header");
        public readonly By txtBank = By.CssSelector("#apm-bank");
        public readonly By txtPayeeName = By.CssSelector("#ComboboxInput-apm-name");
        public readonly By frmAddPayee = By.CssSelector(".js-modal-inner.Modal-content");
        public readonly By txtBranch = By.CssSelector("#apm-branch");
        public readonly By txtAccount = By.CssSelector("#apm-account");
        public readonly By txtSuffix = By.CssSelector("#apm-suffix");
        public readonly By srtName = By.CssSelector(".js-payee-name-column");
        public readonly By lstNames = By.CssSelector(".js-payee-name");

        public PayeesPage(IWebDriver driver) : base(driver)
        {

        }

        public bool ValidatePayeesPage()
        {
            _wait.WaitForPageLoadComplete();
            return IsElementExists(pageBody);
        }

        public void ClickAddNewButton()
        {
            ClickElement(btnAdd);
        }

        public void EnterPayeeDetails(string name, string bank, string branch, string account, string suffix)
        {
            _wait.WaitForPageLoadComplete();
            _driver.FindElement(frmAddPayee).Displayed.Should().BeTrue();
            _driver.FindElement(txtPayeeName).SendKeys(name);
            ClickElement(ddlPayeeName);
            _driver.FindElement(txtBank).SendKeys(bank);
            _driver.FindElement(txtBranch).SendKeys(branch);
            _driver.FindElement(txtAccount).SendKeys(account);
            _driver.FindElement(txtSuffix).SendKeys(suffix);
        }

        public void EnterPayeeName(string name)
        {
            _wait.WaitForPageLoadComplete();
            _driver.FindElement(frmAddPayee).Displayed.Should().BeTrue();
            _driver.FindElement(txtPayeeName).SendKeys(name);
            ClickElement(ddlPayeeName);
        }

        public void ClickAddFormButton()
        {
            ClickElement(btnAddModal);
        }

        public bool ValidateCreatePayeeMessage()
        {
            _wait.WaitForPageLoadComplete();
            _wait.WaitForElementVisible(By.CssSelector("#notification .message"));
            return IsElementExists(msgPayeeAdded);
        }

        public bool ValidatePayeeExist(string accountNumber)
        {
            _wait.WaitForPageLoadComplete();
            By lblAccountNumber = By.XPath($"//p[text()='{accountNumber}']");
            return IsElementExists(lblAccountNumber);
        }

        public bool ValidateMandatoryErrMsg()
        {
            return IsElementExists(msgErrMandatoryName);
        }


        public bool ValidateAscendingList()
        {
            _wait.WaitForPageLoadComplete();
            IList<IWebElement> rowCountAsc = _driver.FindElements(lstNames).ToList();

            List<string> uiPayeeName = new List<string>();

            for (int i = 1; i < rowCountAsc.Count + 1; i++)
            {
                uiPayeeName.Add(_driver.FindElement(By.XPath($"/descendant::span[@class='js-payee-name'][{i}]")).Text);
            }

            bool isOrdered = uiPayeeName.SequenceEqual(uiPayeeName.OrderBy(x => x));

            return isOrdered;
        }
        
        public void ClickNameColumn()
        {
            _driver.FindElement(srtName).Click();
        }

        public bool ValidateDescendingList()
        {
            _wait.WaitForPageLoadComplete();
            IList<IWebElement> rowCountDesc = _driver.FindElements(lstNames).ToList();

            List<string> uiPayeeName = new List<string>();

            for (int a = 1; a < rowCountDesc.Count + 1; a++)
            {
                uiPayeeName.Add(_driver.FindElement(By.XPath($"/descendant::span[@class='js-payee-name'][{a}]")).Text);
            }

            bool isOrdered = uiPayeeName.SequenceEqual(uiPayeeName.OrderByDescending(x => x));
            return isOrdered;
        }


    }
}
