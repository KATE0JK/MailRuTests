using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace MailRuTests
{
    class SeleniumEasyAlertsPage
    {
        private IWebDriver _driver;
        private WebDriverWait _wait;

        private const string alertBoxButtonByCssSelector = ".btn.btn-default";

        IWebElement alertBoxButton;

        public SeleniumEasyAlertsPage(IWebDriver driver, WebDriverWait wait)
        {
            _driver = driver;
            _wait = wait;
        }
        public void ClickOnAlertBoxButton()
        {
            alertBoxButton = _driver.FindElements(By.CssSelector(alertBoxButtonByCssSelector))[0];
            alertBoxButton.Click();
        }

        public string GetAlertText()
        {
            string text = null;

            try
            {
                var alert = _driver.SwitchTo().Alert();
                text = alert.Text;
            }
            catch (NoAlertPresentException)
            {
                return null;
            }

            return text;
        }

        public void ConfirmAlert()
        {
            try
            {
                var alert = _driver.SwitchTo().Alert();
                alert.Accept();
            }
            catch (NoAlertPresentException)
            {
                //need to log this
            }
        }
    }
}
