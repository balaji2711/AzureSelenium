using AventStack.ExtentReports;
using AzureSelenium.Utility;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.IO;
using System.Reflection;
using System.Configuration;
using OpenQA.Selenium.Firefox;
using AzureSelenium.CommonMethods;

namespace AzureSelenium
{
    [TestFixture]
    public class BaseFixture
    {
        public static IWebDriver driver;
        static int i = 0;
        static string baseURL = null;
        static string browser = null;                 

        [OneTimeSetUp]
        public void SetupReport()
        {
            var dir = TestContext.CurrentContext.TestDirectory;
            if (i==0)
            {
                Reporter.SetupExtentReport("Regression Testing", "Automation Testing Report", dir);
                ++i;
            }                        
        }

        [SetUp]
        public static void Setup()
        {
            baseURL = ConfigurationManager.AppSettings["url"].ToString();
            browser = ConfigurationManager.AppSettings["browser"].ToString();

            switch (browser)
            {
                case "Chrome":
                    var chromeOptions = new ChromeOptions();
                    chromeOptions.AddArguments("headless");
                    driver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), chromeOptions);                    
                    break;
                case "Firefox":
                    driver = new FirefoxDriver();
                    break;
                default:
                    driver = new ChromeDriver();
                    break;
            }
            Reporter.CreateTest(TestContext.CurrentContext.Test.Name);
            Reporter.SetStepStatusPass("Browser started.");
            driver.Navigate().GoToUrl(baseURL);
            Reporter.SetStepStatusPass($"Successfully navigated to the url [{baseURL}].");
            driver.Manage().Window.Maximize();
            Reporter.SetStepStatusPass("Browser maximized.");                                              
        }

        [TearDown]
        public static void Close()
        {
            var testStatus = TestContext.CurrentContext.Result.Outcome.Status;
            Reporter.TestStatus(testStatus.ToString());         
        }

        [OneTimeTearDown]
        public void CloseForReport()
        {
            Reporter.FlushReport();
            driver.Close();
            driver.Quit();
            Reporter.SetStepStatusPass($"Browser closed.");
            UtilityMethods.SendEmail();
        }
    }
}