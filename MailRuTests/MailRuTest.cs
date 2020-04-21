using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Linq;

namespace MailRuTests
{
    [TestClass]
    public class MailRuTest
    {
        IWebDriver driver;

        [TestInitialize]
        public void TestInitialize()
        {
            driver = new ChromeDriver();
            driver.Manage().Timeouts().ImplicitWait = new TimeSpan(5000);
        }

        [TestCleanup]
        public void TestCleanUp()
        {
            driver.Close();
        }

        [TestMethod]
        public void NewEmailTest()
        {
            string addressTo = "Kate_k@mail.ru";
            string subject = "TestEmailSubject-" + DateTime.Now;
            string body = "TestEmailBody-" + DateTime.Now;

            LoginPage loginPage = new LoginPage(UserConstantData.URL,driver);

            InboxPage inboxPage = loginPage.Login(UserConstantData.Login, UserConstantData.Password);

            Assert.IsTrue(inboxPage.UserIsLogedIn());

            NewEmailPage newEmail = inboxPage.ClickNewEmailButton();

            Assert.IsTrue(newEmail.NewEmailPopUpIsOpen());

            newEmail.SendNewEmail(addressTo, subject, body);

            List<Email> list = inboxPage.GetEmailList();

            Assert.IsTrue(list.Any(e => e.Sender == addressTo && e.Subject == subject));
        }
    }
}
