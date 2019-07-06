using CoreLibrary.Configuration;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using System;
using System.IO;
using System.Reflection;

namespace Selenium.Engine
{
    public partial class SeleniumDriver
    {
        public IWebDriver driver;
        private IJavaScriptExecutor executor;
        private WebDriverWait wait;
       
        public SeleniumDriver()
        {
            driver = GetChromeDriver();
            executor = ((IJavaScriptExecutor)driver);
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(50));
           

        }

        private IWebDriver GetChromeDriver()
        {
            try
            {
                IWebDriver driver;
                var options = SetChromeProperties();

                var config = new AppConfigReader();
                if (String.Compare(config.GetSeleniumHost(), "local", StringComparison.OrdinalIgnoreCase) == 0)
                {
                    driver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), options);
                    driver.Navigate().GoToUrl(config.GetWebsite() + config.GetLoginPageEndpoint() + "?marketcode=" + config.GetMarket());
                    driver.Manage().Window.Maximize();
                    driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(100);
                    driver.Manage().Timeouts().AsynchronousJavaScript = TimeSpan.FromSeconds(50);
                    return driver;
                }
                
                driver = new RemoteWebDriver(new Uri(config.GetSeleniumHost()),
                   options);
                driver.Navigate().GoToUrl(config.GetWebsite() + config.GetLoginPageEndpoint() + "?marketcode=" + config.GetMarket());
                driver.Manage().Window.Maximize();
                driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(50);
                driver.Manage().Timeouts().AsynchronousJavaScript = TimeSpan.FromSeconds(50);
                return driver;

            }
            catch (Exception e)
            {
                Console.WriteLine("Not able to create instance of webdriver in method - GetNgChromeDriver  --" + e.StackTrace + e.Message);
                throw e;
            }

        }


        private ChromeOptions SetChromeProperties()
        {
            var options = new ChromeOptions();
           
             options.AddArgument("no-sandbox");
            options.AddUserProfilePreference("download.prompt_for_download", false);
            options.AddUserProfilePreference("download.default_directory", "C:\\temp");
           // options.AddAdditionalCapability("browser", "Chrome",true);
           // options.AddAdditionalCapability("os", "Linux",true);

            return options;
        }
    }
}

