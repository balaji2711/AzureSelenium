using AventStack.ExtentReports;
using AzureSelenium.PageObjects;
using AzureSelenium.Utility;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AzureSelenium.Tests
{
    [TestFixture]
    public class SearchGoogle : BaseFixture
    {
        string currentWorldPopulation = null;        

        [Test]
        public void PopulationCountGoogle()
        {
            YahooPage yahooPage = new YahooPage(driver);            
            while (true)
            {
                currentWorldPopulation = yahooPage.getCurrentPopulation();
                Reporter.SetStepStatusPass("Successfully fetched the current world population - "+currentWorldPopulation);
                break;
            }            
        }
    }
}