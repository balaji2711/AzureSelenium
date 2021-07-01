using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace AzureSelenium.CommonMethods
{
    public class UtilityMethods : BaseFixture
    {
        public static string ScreenCaptureAsBase64String()
        {
            ITakesScreenshot ts = (ITakesScreenshot)driver;
            Screenshot screenshot = ts.GetScreenshot();
            return screenshot.AsBase64EncodedString;
        }

        public static void SendEmail()
        {
            try
            {
                List<MailAddress> to = new List<MailAddress>();
                string[] toList = ConfigurationManager.AppSettings["EmailAddressTo"].Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string emailId in toList)
                {
                    to.Add(new MailAddress(emailId));
                }

                MailMessage mail = new MailMessage();
                string from = ConfigurationManager.AppSettings["EmailAddressFrom"];
                mail.From = new MailAddress(from);
                mail.Body = "Body";
                mail.IsBodyHtml = true;
                to.ForEach(entry => mail.To.Add(entry));

                var file_1 = File.ReadAllText(@"D:\Learnings\AzureSelenium\AzureSelenium\bin\dashboard.html");
                var file_2 = File.ReadAllText(@"D:\Learnings\AzureSelenium\AzureSelenium\bin\dashboard.html");
                string content = file_1 + file_2;
                mail.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(content, System.Text.Encoding.Default, "text /html"));
                mail.BodyEncoding = System.Text.Encoding.UTF8;
                mail.Subject = "Automation Testing Report";
                mail.SubjectEncoding = System.Text.Encoding.UTF8;
                var smtp = new SmtpClient
                {
                    Host = ConfigurationManager.AppSettings["Host"]             
                };
                smtp.Send(mail);
                smtp.Dispose();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
