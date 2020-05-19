using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using Allure.Commons;

namespace MailRuTests
{
    public abstract class BaseAllureReport: AllureReport
    {
        private IWebDriver driver;

        public BaseAllureReport(bool isChrome)
        {
            if (isChrome)
            {
                driver = new ChromeDriver();
            }
            else
            {
                driver = new FirefoxDriver();
            }
        }

        public IWebDriver Driver
        {
            get { return driver; }
        }
    }
}
