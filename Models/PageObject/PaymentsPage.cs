using OpenQA.Selenium;

namespace SmartlySpecflow.Models
{
    public class PaymentsPage : BasePage
    {
        public readonly By mdlPayment = By.CssSelector("[data-testid='PaymentModal']");
        public readonly By btnFromAcct = By.CssSelector("[data-testid='from-account-chooser']");
        public readonly By btnToAcct = By.CssSelector("[data-testid='to-account-chooser']");
        public readonly By txtSearch = By.CssSelector("[placeholder='Search']");
        public readonly By btnSearchResult = By.CssSelector("[data-monitoring-label='Transfer Form Account Card']");
        public readonly By txtAmount = By.CssSelector("[name='amount']"); 
        public readonly By lblFromBal = By.XPath("/descendant::p[contains(@class,'balance')][1]");
        public readonly By lblToBal = By.XPath("/descendant::p[contains(@class,'balance')][2]");
        public readonly By btnTransfer = By.CssSelector("[data-monitoring-label='Transfer Form Submit']");
        public readonly By msgTransferSuccess = By.CssSelector("#notification .message");

        public PaymentsPage(IWebDriver driver) : base(driver)
        {

        }

        public bool ValidatePaymentsModal()
        {
            _wait.WaitForPageLoadComplete();
            return IsElementExists(mdlPayment);
        }

        public string SelectSender(string sender)
        {
            ValidatePaymentsModal();
            ClickElement(btnFromAcct);
            _wait.WaitForPageLoadComplete();
            ClickElement(txtSearch);
            _driver.FindElement(txtSearch).SendKeys(sender);
            ClickElement(btnSearchResult);

            var senderBalance = _driver.FindElement(lblFromBal).Text;
            senderBalance = senderBalance[1..^5];

            return senderBalance;
        }

        public string SelectReceiver(string receiver)
        {
            ValidatePaymentsModal();
            ClickElement(btnToAcct);
            _wait.WaitForPageLoadComplete();
            ClickElement(txtSearch);
            _driver.FindElement(txtSearch).SendKeys(receiver);
            ClickElement(btnSearchResult);

            var receiverBalance = _driver.FindElement(lblToBal).Text;
            receiverBalance = receiverBalance[1..^5];

            return receiverBalance;
        }

        public void TransferAmount(string amount)
        {
            ClickElement(txtAmount);
            _driver.FindElement(txtAmount).SendKeys(amount);
            ClickElement(btnTransfer);
        }

        public bool ValidateTransferConfirmationMsg()
        {
            _wait.WaitForPageLoadComplete();
            return IsElementExists(msgTransferSuccess);
        }

        public void ValidateCurrentBalances(double senderPrevBal, double receiverPrevVal, double transferAmout, string sender, string receiver)
        {
            double senderComputedBal = senderPrevBal - transferAmout;
            double receiverComputedBal = receiverPrevVal + transferAmout;

            var senderNewBalance = SelectSender(sender);
            var receiverNewBalance = SelectReceiver(receiver);

            double intNewSenderBal = double.Parse(senderNewBalance);
            double intNewReceiverBal = double.Parse(receiverNewBalance);

            Equals(senderComputedBal, intNewSenderBal).Should().BeTrue();
            Equals(receiverComputedBal, intNewReceiverBal).Should().BeTrue();
        }
    }
}
