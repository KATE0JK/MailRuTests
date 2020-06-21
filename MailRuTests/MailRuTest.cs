using System;
using System.Collections.Generic;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System.Linq;
using NUnit.Framework.Interfaces;
using Assert = NUnit.Framework.Assert;
using MailRuTests2;
using Allure.NUnit.Attributes;
using Allure.Commons.Model;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Edge;

namespace MailRuTests
{
    [AllureSuite("TestSuite1")]
    [TestFixture]
    public class MailRuTest : BaseEmailTest
    {
        WebDriverWait wait;

        [SetUp]
        public void TestInitialize()
        {
            //driver.Manage().Timeouts().ImplicitWait = new TimeSpan(1000);
            wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(30));

        }

     /*   [OneTimeTearDown]
        public void TearDown()
        {
            if (TestContext.CurrentContext.Result.Outcome != ResultState.Success)
            {
                var testName = TestContext.CurrentContext.Test.MethodName;
                var screenshot = ((ITakesScreenshot)driver).GetScreenshot();
                screenshot.SaveAsFile(@"C:\Screens" + testName + ".jpg", ScreenshotImageFormat.Jpeg);
            }
        }*/

        [TearDown]
        public void TestCleanUp()
        {
            if (TestContext.CurrentContext.Result.Outcome != ResultState.Success)
            {
                var testName = TestContext.CurrentContext.Test.MethodName;
                var screenshot = ((ITakesScreenshot)Driver).GetScreenshot();
                screenshot.SaveAsFile(@"C:\Screens\" + testName + ".jpg", ScreenshotImageFormat.Jpeg);
            }
            Driver.Close();
        }

        [Test]
        [AllureSubSuite ("MailRuTest")]
        [AllureSeverity (SeverityLevel.Normal)]
        [AllureLink ("https://www.onliner.by/")]
        [AllureOwner("Katya Nikitina")]
        [AllureTest("Test 2")]
        [TestCaseSource(nameof(EmailParameters))]
        public void NewEmailTest(string addressTo, string subject, string body)
        {
            //string addressTo = "Kate_k@mail.ru";
            //string subject = "TestEmailSubject-" + DateTime.Now;
            //string body = "TestEmailBody-" + DateTime.Now;

            LoginPage loginPage = new LoginPage(UserConstantData.URL,Driver,wait);

            InboxPage inboxPage = loginPage.Login(UserConstantData.Login, UserConstantData.Password);

            Assert.IsTrue(inboxPage.UserIsLogedIn());

            NewEmailPage newEmail = inboxPage.ClickNewEmailButton();

            newEmail.SendNewEmail(addressTo, subject, body);

            List<Email> list = inboxPage.GetEmailList();

            Assert.IsTrue(list.Any(e => e.Sender.Contains(addressTo) && e.Subject.Contains(subject)));
        }

        protected override DriverOptions GetDriverOptions()
        {
            var edgeOptions = new EdgeOptions();

            edgeOptions.AddAdditionalCapability(CapabilityType.Version, "latest");
            edgeOptions.AddAdditionalCapability(CapabilityType.Platform, "WIN10");

            return edgeOptions;
        }

        //protected override DriverOptions GetDriverOptions()
        //{
        //    var chromeOptions = new ChromeOptions();

        //    chromeOptions.AddAdditionalCapability(CapabilityType.Version, "40");
        //    chromeOptions.AddAdditionalCapability(CapabilityType.Platform, "LINUX");

        //    return chromeOptions;
        //}
    }
}
