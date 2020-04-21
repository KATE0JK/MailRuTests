using System.Collections.Generic;
using OpenQA.Selenium;

namespace MailRuTests
{
    class InboxPage
    {
        private IWebDriver _driver;
        private const string UserIsLogedInIdSelector = "PH_user-email";

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
            IWebElement searchNewEmailButton = _driver.FindElement(By.CssSelector("span.compose-button__wrapper"));
            searchNewEmailButton.Click();

            NewEmailPage newEmailPage = new NewEmailPage(_driver);

            return newEmailPage;
        }

        public void LogOut()
        {
            IWebElement searchLogOutButton = _driver.FindElement(By.Id("PH_logoutLink"));
            searchLogOutButton.Click();
        }

        public List<Email> GetEmailList()
        {
            IReadOnlyCollection<IWebElement> searchEmailList = _driver.FindElements(By.CssSelector(".llc js-tooltip-direction_letter-bottom js-letter-list-item llc_normal"));
            List<Email> emails = new List<Email>();

            foreach (IWebElement e in searchEmailList)
            {
                string sender = e.FindElement(By.CssSelector(".ll-crpt")).Text;
                string subject = e.FindElement(By.CssSelector(".ll-sj__normal")).Text;

                Email email = new Email(sender, subject);
                emails.Add(email);
            }

            return emails;
        }
    }
}
