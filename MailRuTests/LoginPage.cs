using System;
//using Allure.NUnit.Attributes;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;


namespace MailRuTests
{
    class LoginPage
    {
        private string _url;
        private IWebDriver _driver;
        private WebDriverWait _wait;

        private const string searchLoginIdSelectorById = "mailbox:login";
        private const string searchNextButtonSelectorByCss = "input.o-control";
        private const string searchPasswordIdSelectorById = "mailbox:password";
        private const string headerToBeDisplayedSelectorById = "mailbox:mailHeaderSecondStepEmail";

        IWebElement searchLoginId;
        IWebElement searchNextButton;
        IWebElement searchPasswordId;
        IWebElement headerToBeDisplayed;

        public LoginPage(string url,IWebDriver driver, WebDriverWait wait)
        {
            _url = url;
            _driver = driver;
            _wait = wait;
            }

        //[AllureStep("Try login with credentials (&login&, &pw&)")]
        public InboxPage Login(string login, string pw)
        {
            _driver.Navigate().GoToUrl(_url);

            searchLoginId = _driver.FindElement(By.Id(searchLoginIdSelectorById));
            searchLoginId.SendKeys(login);

            searchNextButton = _driver.FindElement(By.CssSelector(searchNextButtonSelectorByCss));
            searchNextButton.Click();
            //_driver.Manage().Timeouts().ImplicitWait = new TimeSpan(5000);
            var element = _wait.Until(condition =>
            {
                try
                {
                    headerToBeDisplayed = _driver.FindElement(By.Id(headerToBeDisplayedSelectorById));
                    return headerToBeDisplayed.Displayed;
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

            searchPasswordId = _driver.FindElement(By.Id(searchPasswordIdSelectorById));
            searchPasswordId.SendKeys(pw);

            searchNextButton.Click();

            _driver.Manage().Timeouts().ImplicitWait = new TimeSpan(5000);

            InboxPage inboxPage = new InboxPage(_driver, _wait);

            return inboxPage;
        }
    }
}
