using NUnit.Framework;
using OpenQA.Selenium;
using SmartlySpecflow.Models;
using SmartlySpecflow.Steps;
using TechTalk.SpecFlow.Assist;

namespace SmartlySpecflow.StepDefinitions
{
    [Binding]
    public class BNZDemoStepDefinitions : BaseSteps
    {
        private string AccountNumber = "";

        public BNZDemoStepDefinitions(IWebDriver driver, ScenarioContext scenarioContext, FeatureContext featureContext) : base(driver, scenarioContext, featureContext)
        {

        }

        [Given(@"I open the BNZ Demo website")]
        public void GivenIOpenTheBNZDemoWebsite()
        {
            var homePage = new HomePage(_driver);
            homePage.NavigateToBNZDemoDashboardPage();
            Assert.True(homePage.ValidateBNZDashboardPage());
        }

        [When(@"I click the main Menu")]
        public void WhenIClickTheMainMenu()
        {
            var homePage = new HomePage(_driver);
            homePage.ClickMainMenu();
        }

        [When(@"click on Payees option")]
        public void WhenClickOnPayeesOption()
        {
            var homePage = new HomePage(_driver);
            homePage.ClickPayeesOptionMenu();
        }

        [Then(@"Payees page should be loaded")]
        public void ThenPayeesPageShouldBeLoaded()
        {
            var payeesPage = new PayeesPage(_driver);
            Assert.True(payeesPage.ValidatePayeesPage());
        }

        [When(@"I navigate to Payees page")]
        public void WhenINavigateToPayeesPage()
        {
            var homePage = new HomePage(_driver);
            var payeesPage = new PayeesPage(_driver);
            homePage.ClickMainMenu();
            homePage.ClickPayeesOptionMenu();
            Assert.True(payeesPage.ValidatePayeesPage());
        }

        [When(@"I add new Payee")]
        public void WhenIAddNewPayee(Table table)
        {
            var payeesPage = new PayeesPage(_driver);
            payeesPage.ClickAddNewButton();

            var payeeDetails = table.CreateSet<PayeeData>();
            foreach (PayeeData data in payeeDetails)
            {
                payeesPage.EnterPayeeDetails(data.Name, data.Bank, data.Branch, data.Account, data.Suffix);
                payeesPage.ClickAddFormButton();
                _featureContext.Set(data.Bank + "-" + data.Branch + "-" + data.Account + "-" + data.Suffix, AccountNumber);
            }
        }

        [Then(@"create success confirmation message should be displayed")]
        public void ThenCreateSuccessConfirmationMessageShouldBeDisplayed()
        {
            var payeesPage = new PayeesPage(_driver);
            Assert.True(payeesPage.ValidateCreatePayeeMessage());
        }

        [Then(@"new Payee should be created successfully")]
        public void ThenNewPayeeShouldBeCreatedSuccessfully()
        {
            var payeesPage = new PayeesPage(_driver);
            var accountNumber = _featureContext.Get<string>(AccountNumber);
            Assert.True(payeesPage.ValidatePayeeExist(accountNumber));
        }

        [When(@"I add new Payee without entering Payee details")]
        public void WhenIAddNewPayeeWithoutEnteringPayeeDetails()
        {
            var payeesPage = new PayeesPage(_driver);
            payeesPage.ClickAddNewButton();
            payeesPage.ClickAddFormButton();
        }

        [Then(@"error message for mandatory field should be displayed")]
        public void ThenErrorMessageForMandatoryFieldShouldBeDisplayed()
        {
            var payeesPage = new PayeesPage(_driver);
            Assert.True(payeesPage.ValidateMandatoryErrMsg());
        }

        [When(@"I enter Payee Name")]
        public void WhenIEnterPayeeName(Table table)
        {
            var payeesPage = new PayeesPage(_driver);
            var payeeDetails = table.CreateSet<PayeeData>();

            foreach (PayeeData data in payeeDetails)
            {
                payeesPage.EnterPayeeName(data.Name);
            }
        }

        [Then(@"error message for mandatory field should not be displayed")]
        public void ThenErrorMessageForMandatoryFieldShouldNotBeDisplayed()
        {
            var payeesPage = new PayeesPage(_driver);
            Assert.False(payeesPage.ValidateMandatoryErrMsg());
        }

        [Then(@"Payee list is sorted in ascending order")]
        public void ThenPayeeListIsSortedInAscendingOrder()
        {
            var payeesPage = new PayeesPage(_driver);
            Assert.True(payeesPage.ValidateAscendingList());
        }

        [When(@"I click the Name header in Payee list")]
        public void WhenIClickTheNameHeaderInPayeeList()
        {
            var payeesPage = new PayeesPage(_driver);
            payeesPage.ClickNameColumn();
        }

        [Then(@"Payee list is sorted in descending order")]
        public void ThenPayeeListIsSortedInDescendingOrder()
        {
            var payeesPage = new PayeesPage(_driver);
            Assert.True(payeesPage.ValidateDescendingList());
        }

        [When(@"I navigate to Payments page")]
        public void WhenINavigateToPaymentsPage()
        {
            var homePage = new HomePage(_driver);
            var paymentsPage = new PaymentsPage(_driver);
            homePage.ClickMainMenu();
            homePage.ClickPayOrTransferOptionMenu();
            Assert.True(paymentsPage.ValidatePaymentsModal());
        }

        [When(@"I transfer (.*) from (.*) Account to (.*) Account")]
        public void WhenITransferAmountFromSenderAccountToReceiverAccount(string amount, string sender, string receiver)
        {
            var paymentsPage = new PaymentsPage(_driver);
            var senderBal = paymentsPage.SelectSender(sender);
            var receiverBal = paymentsPage.SelectReceiver(receiver);
            paymentsPage.TransferAmount(amount);

            _scenarioContext.Add("senderBal", senderBal);
            _scenarioContext.Add("receiverBal", receiverBal);
            _scenarioContext.Add("amount", amount);
        }

        [Then(@"transfer success message should be displayed")]
        public void ThenTransferSuccessMessageShouldBeDisplayed()
        {
            var paymentsPage = new PaymentsPage(_driver);
            Assert.True(paymentsPage.ValidateTransferConfirmationMsg());
        }

        [Then(@"balances after success transfer are correct for (.*) and (.*)")]
        public void ThenBalancesAfterSuccessTransferAreCorrectForSenderAndReceiver(string sender, string receiver)
        {
            var paymentsPage = new PaymentsPage(_driver);

            string strSenderBal = _scenarioContext.Get<string>("senderBal");
            string strReceiverBal = _scenarioContext.Get<string>("receiverBal");
            string strAmount = _scenarioContext.Get<string>("amount");

            var doubleSenderBal = double.Parse(strSenderBal);
            var doubleReceiverBal = double.Parse(strReceiverBal);
            var doubleTransferAmount = double.Parse($"{strAmount}");

            WhenINavigateToPaymentsPage();
            paymentsPage.ValidateCurrentBalances(doubleSenderBal, doubleReceiverBal, doubleTransferAmount, sender, receiver);
        }
    }
}