using OpenQA.Selenium;
using Allure.Commons;
using OpenQA.Selenium.Remote;
using System;

namespace MailRuTests
{
    public abstract class BaseAllureReport : AllureReport
    {
        private readonly Uri _sauceLabsUri = new Uri("https://ondemand.eu-central-1.saucelabs.com:443");
        private readonly string _sauceLabsUserName = "katyanikitina";
        private readonly string _sauceLabsPassword = "realpasswordhere";

        public BaseAllureReport()
        {
            var driverOptions = GetDriverOptions();

            driverOptions.AddAdditionalCapability("username", _sauceLabsUserName);
            driverOptions.AddAdditionalCapability("accessKey", _sauceLabsPassword);

            Driver = new RemoteWebDriver(_sauceLabsUri, driverOptions.ToCapabilities());
        } 

        public IWebDriver Driver { get; }

        protected abstract DriverOptions GetDriverOptions();
    }
}
