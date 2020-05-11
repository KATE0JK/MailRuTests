using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using NUnit.Framework.Interfaces;
using Assert = NUnit.Framework.Assert;

namespace MailRuTests
{
    [TestFixture]
    class AlertTest
    {
        IWebDriver driver;
        WebDriverWait wait;
        private const string Url = "https://www.seleniumeasy.com/test/javascript-alert-box-demo.html";

        [SetUp]
        public void TestInitialize()
        {
            driver = new FirefoxDriver();
            //driver.Manage().Timeouts().ImplicitWait = new TimeSpan(1000);
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            driver.Navigate().GoToUrl(Url);
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            if (TestContext.CurrentContext.Result.Outcome != ResultState.Success)
            {
                var testName = TestContext.CurrentContext.Test.FullName;
                var screenshot = ((ITakesScreenshot)driver).GetScreenshot();
                screenshot.SaveAsFile(@"c:\Screens" + testName + ".jpg", ScreenshotImageFormat.Jpeg);
            }
        }

        [TearDown]
        public void TestCleanUp()
        {
            driver.Close();
        }

        [Test]
        public void AlertTextTest()
        {
            SeleniumEasyAlertsPage seleniumEasyAlertsPage = new SeleniumEasyAlertsPage(driver,wait);
            seleniumEasyAlertsPage.ClickOnAlertBoxButton();
            Assert.AreEqual("I am an alert box!",seleniumEasyAlertsPage.GetAlertText());
            seleniumEasyAlertsPage.ConfirmAlert();
        }
    }
}
