using System;
using System.Collections.Generic;
using OpenQA.Selenium;

namespace MailRuTests
{
    class LoginPage
    {
        private string _url;
        private IWebDriver _driver;

        private const string searchLoginIdSelectorById = "mailbox:login";
        private const string searchNextButtonSelectorByCss = "input.o-control";
        private const string searchPasswordIdSelectorById = "mailbox:password";

        IWebElement searchLoginId;
        IWebElement searchNextButton;
        IWebElement searchPasswordId;

        public LoginPage(string url,IWebDriver driver)
        {
            _url = url;
            _driver = driver;
        }

        public InboxPage Login(string login, string pw)
        {
            _driver.Navigate().GoToUrl(_url);

            searchLoginId = _driver.FindElement(By.Id(searchLoginIdSelectorById));
            searchLoginId.SendKeys(login);

            searchNextButton = _driver.FindElement(By.CssSelector(searchNextButtonSelectorByCss));
            searchNextButton.Click();

            _driver.Manage().Timeouts().ImplicitWait = new TimeSpan(5000);

            searchPasswordId = _driver.FindElement(By.Id(searchPasswordIdSelectorById));
            searchPasswordId.SendKeys(pw);

            searchNextButton.Click();

            _driver.Manage().Timeouts().ImplicitWait = new TimeSpan(5000);

            InboxPage inboxPage = new InboxPage(_driver);

            return inboxPage;
        }
    }
}
