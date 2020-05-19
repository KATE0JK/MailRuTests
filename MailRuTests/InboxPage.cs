using System.Collections.Generic;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace MailRuTests
{
    class InboxPage
    {
        private IWebDriver _driver;
        private WebDriverWait _wait;

        private const string UserIsLogedInIdSelector = "PH_user-email";
        private const string NewEmailButtonSelector = "span.compose-button__wrapper";
        private const string LogOutButtonSelector = "PH_logoutLink";
        private const string senderSelector = ".ll-crpt";
        private const string subjectSelector = ".ll-sj__normal";
        private const string searchEmailListSelector = ".llc";

        IWebElement searchNewEmailButton;
        IWebElement searchLogOutButton;
        IReadOnlyCollection<IWebElement> searchEmailList;

        public InboxPage(IWebDriver driver, WebDriverWait wait)
        {
            _driver = driver;
            _wait = wait; ;
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
            var element = _wait.Until(condition =>
            {
                try
                {
                    searchNewEmailButton = _driver.FindElement(By.CssSelector(NewEmailButtonSelector));
                    return searchNewEmailButton.Displayed;
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
            searchNewEmailButton.Click();

            NewEmailPage newEmailPage = new NewEmailPage(_driver,_wait);

            return newEmailPage;
        }

        public void LogOut()
        {
            searchLogOutButton = _driver.FindElement(By.Id(LogOutButtonSelector));
            searchLogOutButton.Click();
        }

        public List<Email> GetEmailList()
        {
            var element=_wait.Until(condition=>
            {
                try
                {
                    searchEmailList = _driver.FindElements(By.CssSelector(searchEmailListSelector));
                    return true;
                }
                catch (NoSuchElementException)
                {
                    return false;
                }
            });

            List<Email> emails = new List<Email>();

            foreach (IWebElement e in searchEmailList)
            {
                string sender = e.FindElement(By.CssSelector(senderSelector)).GetAttribute("title");
                string subject = e.FindElement(By.CssSelector(subjectSelector)).Text;

                Email email = new Email(sender, subject);
                emails.Add(email);
            }

            return emails;
        }
    }
}
