using OpenQA.Selenium;
using System.Linq;

namespace MailRuTests
{
    class NewEmailPage
    {
        private IWebDriver _driver;
        private const string NewEmailToField = ".container--H9L5q size_s--3_M-_";
        private const string NewEmailSubjectField = ".container--H9L5q size_s--3_M-_";
        private const string NewEmailBodyField = ".editable-ibst cke_editable cke_editable_inline cke_contents_true cke_show_borders";
        private const string NewEmailSendButton = ".button2 button2_base button2_primary button2_hover-support js-shortcut";

        public NewEmailPage(IWebDriver driver)
        {
            _driver = driver;
        }
        public bool NewEmailPopUpIsOpen()
        {
            return (_driver.FindElements(By.CssSelector(NewEmailToField)).Any());
        }

        public void SendNewEmail(string addressTo, string subject, string body)
        {
            IWebElement searchNewEmailToField = _driver.FindElements(By.CssSelector(NewEmailToField))[0];
            searchNewEmailToField.SendKeys(addressTo);
            
            IWebElement searchNewEmailSubjectField = _driver.FindElements(By.CssSelector(NewEmailSubjectField))[1];
            searchNewEmailSubjectField.SendKeys(subject);

            IWebElement searchNewEmailBodyField = _driver.FindElement(By.CssSelector(NewEmailBodyField));
            searchNewEmailBodyField.SendKeys(body);

            IWebElement searchNewEmailSendButton = _driver.FindElement(By.CssSelector(NewEmailSendButton));
            searchNewEmailSendButton.Click();
        }
    }
}
