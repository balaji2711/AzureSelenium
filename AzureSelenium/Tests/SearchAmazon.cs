using AzureSelenium.PageObjects;
using AzureSelenium.Utility;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureSelenium.Tests
{
    [TestFixture]
    public class SearchAmazon : BaseFixture
    {
        string currentWorldPopulation = null;

        [Test]
        public void PopulationCountAmazon()
        {
            YahooPage yahooPage = new YahooPage(driver);
            while (true)
            {
                currentWorldPopulation = yahooPage.getCurrentPopulation();
                Reporter.SetStepStatusPass("Successfully fetched the current world population - " + currentWorldPopulation);
                break;
            }
        }
    }
}
