using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Serilog;

namespace BeatXP.Utilities
{
    internal class CoreCodes
    {
        Dictionary<string, string> properties;
        public IWebDriver? driver;
        public ExtentReports extent;
        ExtentSparkReporter sparkReporter;
        public ExtentTest? test;

        public void ExtentReport()
        {
            ExtentReports report = new ExtentReports();

        }
        public bool CheckLinkStatus(string url)
        {
            try
            {
                var request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(url);
                request.Method = "HEAD";
                using (var response = request.GetResponse())
                {
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
        public void ReadConfigProperties()
        {
            string currDir = Directory.GetParent(@"../../../").FullName;
            properties = new Dictionary<string, string>();
            string fileName = currDir + "/ConfigSettings/config.properties";
            string[] lines = File.ReadAllLines(fileName);
            foreach (string line in lines)
            {
                if (!string.IsNullOrEmpty(line) && line.Contains("="))
                {
                    string[] parts = line.Split('=');
                    string key = parts[0].Trim();
                    string value = parts[1].Trim();
                    properties[key] = value;
                }
            }
        }
        public string TakeScreenShot()
        {
            ITakesScreenshot screenshot = (ITakesScreenshot)driver;
            Screenshot ss = screenshot.GetScreenshot();
            string currdir = Directory.GetParent(@"../../../").FullName;
            string filepath = currdir + "/Screenshot/ss_" +
                DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".png";
            ss.SaveAsFile(filepath);
            return filepath;
        }
        [OneTimeSetUp]
        public void InitializeBrowser()
        {
            string? currdir = Directory.GetParent(@"../../../")?.FullName;
            string? logfilepath = currdir + "/Logs/log_" +
                DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".txt";

            Log.Logger = new LoggerConfiguration()
            .WriteTo.File(logfilepath, rollingInterval: RollingInterval.Day)
            .CreateLogger();
            
            extent = new ExtentReports();
            sparkReporter = new ExtentSparkReporter(currdir + "/ExtentReport/extent-report"
                + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".html");
            extent.AttachReporter(sparkReporter);
            ReadConfigProperties();
            if (properties["browser"].ToLower() == "chrome")
            {
                driver = new ChromeDriver();
            }
            else if (properties["browser"].ToLower() == "edge")
            {
                driver = new EdgeDriver();
            }
            driver.Url = properties["baseUrl"];
            driver.Manage().Window.Maximize();
        }
        [OneTimeTearDown]
        public void Cleanup()
        {
            Log.CloseAndFlush();
            extent.Flush();
            driver.Quit();
        }
    }
}
