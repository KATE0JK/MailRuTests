using OpenQA.Selenium;
using System.Linq;
using OpenQA.Selenium.Support.UI;

namespace MailRuTests
{
    class NewEmailPage
    {
        private IWebDriver _driver;
        private WebDriverWait _wait;
        private const string NewEmailToField = "div.contacts--1ofjA input.container--H9L5q.size_s--3_M-_";
        private const string NewEmailSubjectField = "div.subject__container--HWnat input.container--H9L5q.size_s--3_M-_";
        private const string NewEmailBodyField = "div.editable-container-n8y5 div.cke_editable>div";
        private const string NewEmailSendButton = ".button2 button2_base button2_primary button2_hover-support js-shortcut";

        IWebElement searchNewEmailToField;
        IWebElement searchNewEmailSubjectField;
        IWebElement searchNewEmailBodyField;
        IWebElement searchNewEmailSendButton;

        public NewEmailPage(IWebDriver driver,WebDriverWait wait)
        {
            _driver = driver;
            _wait = wait;
        }

        public void SendNewEmail(string addressTo, string subject, string body)
        {
            var element = _wait.Until(condition =>
            {
                try
                {
                    searchNewEmailToField = _driver.FindElement(By.CssSelector(NewEmailToField));
                    return searchNewEmailToField.Displayed;
                }

                catch (StaleElementReferenceException)
                {
                    return false;
                }

                catch (NoSuchElementException)
                {
                    return false;
                }
            });

            searchNewEmailToField.SendKeys(addressTo);
            
            searchNewEmailSubjectField = _driver.FindElements(By.CssSelector(NewEmailSubjectField))[1];
            searchNewEmailSubjectField.SendKeys(subject);

            searchNewEmailBodyField = _driver.FindElement(By.CssSelector(NewEmailBodyField));
            searchNewEmailBodyField.SendKeys(body);

            searchNewEmailSendButton = _driver.FindElement(By.CssSelector(NewEmailSendButton));
            searchNewEmailSendButton.Click();
        }
    }
}
