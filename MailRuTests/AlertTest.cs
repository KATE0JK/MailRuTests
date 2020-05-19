using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using NUnit.Framework.Interfaces;
using Assert = NUnit.Framework.Assert;
using Allure.NUnit.Attributes;
using Allure.Commons.Model;

namespace MailRuTests
{
    [AllureSuite("TestSuite1")]
    [TestFixture]
    public class AlertTest: BaseAllureReport
    {
        WebDriverWait wait;
        private const string Url = "https://www.seleniumeasy.com/test/javascript-alert-box-demo.html";

        public AlertTest():base(false)
        {

        }

        [SetUp]
        public void TestInitialize()
        {
            //driver.Manage().Timeouts().ImplicitWait = new TimeSpan(1000);
            wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(10));
            Driver.Navigate().GoToUrl(Url);
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            if (TestContext.CurrentContext.Result.Outcome != ResultState.Success)
            {
                var testName = TestContext.CurrentContext.Test.FullName;
                var screenshot = ((ITakesScreenshot)Driver).GetScreenshot();
                screenshot.SaveAsFile(@"c:\Screens" + testName + ".jpg", ScreenshotImageFormat.Jpeg);
            }
        }

        [TearDown]
        public void TestCleanUp()
        {
            Driver.Close();
        }

        [Test]
        [AllureSubSuite ("AlertTest")]
        [AllureSeverity(SeverityLevel.Critical)]
        [AllureLink("https://www.onliner.by/")]
        [AllureTest ("Test 1")]
        [AllureOwner ("Katya Nikitina")]
        public void AlertTextTest()
        {
            SeleniumEasyAlertsPage seleniumEasyAlertsPage = new SeleniumEasyAlertsPage(Driver,wait);
            seleniumEasyAlertsPage.ClickOnAlertBoxButton();
            Assert.AreEqual("I am an alert box!",seleniumEasyAlertsPage.GetAlertText());
            seleniumEasyAlertsPage.ConfirmAlert();
        }
    }
}
