using System;
using System.Collections.Generic;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System.Linq;
using Assert = NUnit.Framework.Assert;

namespace MailRuTests
{
    [TestFixture]
    public class MailRuTest
    {
        IWebDriver driver;
        WebDriverWait wait;

        [SetUp]
        public void TestInitialize()
        {
            driver = new ChromeDriver();
            //driver.Manage().Timeouts().ImplicitWait = new TimeSpan(1000);
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

        }

        [TearDown]
        public void TestCleanUp()
        {
            driver.Close();
        }

        [Test]
        public void NewEmailTest()
        {
            string addressTo = "Kate_k@mail.ru";
            string subject = "TestEmailSubject-" + DateTime.Now;
            string body = "TestEmailBody-" + DateTime.Now;

            LoginPage loginPage = new LoginPage(UserConstantData.URL,driver,wait);

            InboxPage inboxPage = loginPage.Login(UserConstantData.Login, UserConstantData.Password);

            Assert.IsTrue(inboxPage.UserIsLogedIn());

            NewEmailPage newEmail = inboxPage.ClickNewEmailButton();

            newEmail.SendNewEmail(addressTo, subject, body);

            List<Email> list = inboxPage.GetEmailList();

            Assert.IsTrue(list.Any(e => e.Sender == addressTo && e.Subject == subject));
        }
    }
}
