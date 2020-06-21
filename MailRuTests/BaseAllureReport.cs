using OpenQA.Selenium;
using Allure.Commons;
using OpenQA.Selenium.Remote;
using System;

namespace MailRuTests
{
    public abstract class BaseAllureReport : AllureReport
    {
        public BaseAllureReport()
        {
            Driver = new RemoteWebDriver(GetUriToRunTests(), GetDriverOptions().ToCapabilities());
        } 

        public IWebDriver Driver { get; }

        protected abstract DriverOptions GetDriverOptions();

        protected abstract Uri GetUriToRunTests();
    }
}
