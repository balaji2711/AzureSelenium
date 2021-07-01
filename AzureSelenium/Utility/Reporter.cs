using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports.Reporter.Configuration;
using AzureSelenium.CommonMethods;
using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureSelenium.Utility
{
    public class Reporter
    {
        public static ExtentReports ExtentReports;
        public static ExtentHtmlReporter ExtentHtmlReporter;
        public static ExtentTest TestCase;

        public static void SetupExtentReport(string reportName, string docTitle, dynamic path)
        {
            ExtentHtmlReporter = new ExtentHtmlReporter(path);
            ExtentHtmlReporter.Config.Theme = Theme.Dark;
            ExtentHtmlReporter.Config.DocumentTitle = docTitle;
            ExtentHtmlReporter.Config.ReportName = reportName;            

            ExtentReports = new ExtentReports();
            ExtentReports.AttachReporter(ExtentHtmlReporter);            
            ExtentReports.AddSystemInfo("Environment", "UAT");
            ExtentReports.AddSystemInfo("Machine", Environment.MachineName);
            ExtentReports.AddSystemInfo("OS", Environment.OSVersion.VersionString);
            ExtentReports.AddSystemInfo("Author", "Balaji G");
        }

        public static void CreateTest(string testName)
        {
            TestCase = ExtentReports.CreateTest(testName);
        }

        public static void LogReport(Status status, string message)
        {
            TestCase.Log(status, message);
        }

        public static void FlushReport()
        {
            ExtentReports.Flush();
        }

        public static void TestStatus(string status)
        {
            if (status.Equals("Passed"))
            {
                TestCase.Pass("Test Passed");
            }
            else
            {
                var stacktrace = TestContext.CurrentContext.Result.StackTrace;
                var errorMessage = "<pre>" + TestContext.CurrentContext.Result.Message + "</pre>";
                Reporter.SetTestStatusFail($"<br>{errorMessage}<br>Stack Trace: <br>{stacktrace}<br>");
                TestCase.AddScreenCaptureFromBase64String(UtilityMethods.ScreenCaptureAsBase64String(), "Screenshot on Error:");             
            }
        }

        public static void SetStepStatusPass(string stepDescription)
        {
            TestCase.Log(Status.Pass, stepDescription);
        }

        public static void SetTestStatusFail(string message = null)
        {
            var printMessage = "<p><b>Test FAILED!</b></p>";
            if (!string.IsNullOrEmpty(message))
            {
                printMessage += $"Message: <br>{message}<br>";
            }
            TestCase.Fail(printMessage);
        }

        public void SetTestStatusSkipped()
        {
            TestCase.Skip("Test skipped!");
        }
    }
}