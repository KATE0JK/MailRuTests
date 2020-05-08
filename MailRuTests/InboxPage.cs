using System.Collections.Generic;
using OpenQA.Selenium;

namespace MailRuTests
{
    class InboxPage
    {
        private IWebDriver _driver;
        private const string UserIsLogedInIdSelector = "PH_user-email";
        private const string NewEmailButtonSelector = "span.compose-button__wrapper";
        private const string LogOutButtonSelector = "PH_logoutLink";
        private const string senderSelector = ".ll-crpt";
        private const string subjectSelector = ".ll-sj__normal";
        private const string searchEmailListSelector = ".llc js-tooltip-direction_letter-bottom js-letter-list-item llc_normal";

        IWebElement searchNewEmailButton;
        IWebElement searchLogOutButton;
        IReadOnlyCollection<IWebElement> searchEmailList;

        public InboxPage(IWebDriver driver)
        {
            _driver = driver;
        }

        public bool UserIsLogedIn()
        {
            //IWebElement searchEmail = _driver.FindElement(By.Id(UserIsLogedInIdSelector));
            /*if (searchEmail == null)
            {
                return false;
            }
            return true;*/
            return (_driver.FindElement(By.Id(UserIsLogedInIdSelector)) != null);
        }

        public NewEmailPage ClickNewEmailButton()
        {
            searchNewEmailButton = _driver.FindElement(By.CssSelector(NewEmailButtonSelector));
            searchNewEmailButton.Click();

            NewEmailPage newEmailPage = new NewEmailPage(_driver);

            return newEmailPage;
        }

        public void LogOut()
        {
            searchLogOutButton = _driver.FindElement(By.Id(LogOutButtonSelector));
            searchLogOutButton.Click();
        }

        public List<Email> GetEmailList()
        {
            searchEmailList = _driver.FindElements(By.CssSelector(searchEmailListSelector));
            List<Email> emails = new List<Email>();

            foreach (IWebElement e in searchEmailList)
            {
                string sender = e.FindElement(By.CssSelector(senderSelector)).Text;
                string subject = e.FindElement(By.CssSelector(subjectSelector)).Text;

                Email email = new Email(sender, subject);
                emails.Add(email);
            }

            return emails;
        }
    }
}
