using System;
using System.Collections.Generic;
using OpenQA.Selenium;

namespace MailRuTests
{
    class LoginPage
    {
        private string _url;
        private IWebDriver _driver;

        public LoginPage(string url,IWebDriver driver)
        {
            _url = url;
            _driver = driver;
        }

        public InboxPage Login(string login, string pw)
        {
            _driver.Navigate().GoToUrl(_url);

            IWebElement searchLoginId = _driver.FindElement(By.Id("mailbox:login"));
            searchLoginId.SendKeys(login);

            IWebElement searchNextButton = _driver.FindElement(By.CssSelector("input.o-control"));
            searchNextButton.Click();

            _driver.Manage().Timeouts().ImplicitWait = new TimeSpan(5000);

            IWebElement searchPasswordId = _driver.FindElement(By.Id("mailbox:password"));
            searchPasswordId.SendKeys(pw);

            searchNextButton.Click();

            _driver.Manage().Timeouts().ImplicitWait = new TimeSpan(5000);

            InboxPage inboxPage = new InboxPage(_driver);

            return inboxPage;
        }
    }
}
