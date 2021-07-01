using AventStack.ExtentReports;
using AzureSelenium.Utility;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureSelenium.PageObjects
{
    public class YahooPage
    {
        private IWebDriver driver;

        public YahooPage(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }

        [FindsBy(How = How.XPath, Using = "//div[@class='maincounter-number']/span")]
        public IWebElement CurrentPopulation;

        public string getCurrentPopulation()
        {
            string text = null;
            try
            {
                text = CurrentPopulation.Text;
                return text;
            }
            catch (Exception ex)
            {
                Reporter.LogReport(Status.Fail, "Exception occured - " + ex.Message);
                throw new Exception(Environment.NewLine + " Exception occured - " + ex.Message);
            }
        }
    }
}
