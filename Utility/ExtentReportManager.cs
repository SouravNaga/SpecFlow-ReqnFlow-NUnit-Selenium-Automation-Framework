using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using System;
using System.IO;

namespace Utility
{
    public class ExtentReportManager
    {
        private static ExtentReports _extent;

        private static readonly string _projectRoot = Directory.GetParent(AppContext.BaseDirectory).Parent.Parent.Parent.FullName;

        private static readonly string _reportPath = Path.Combine(
            _projectRoot,
            "Reports",
            $"TestReport_{DateTime.Now:yyyyMMdd_HHmmss}.html"
        );

        public static ExtentReports GetExtent()
        {
            Console.WriteLine("📍 Report Path: " + _reportPath);

            if (_extent == null)
            {
                Directory.CreateDirectory(Path.GetDirectoryName(_reportPath));

                var sparkReporter = new ExtentHtmlReporter(_reportPath); // ✅ Use SparkReporter
                sparkReporter.Config.DocumentTitle = "ReqnFlow Automation Report";
                sparkReporter.Config.ReportName = "Test Execution Summary";
                sparkReporter.Config.Theme = AventStack.ExtentReports.Reporter.Configuration.Theme.Standard;

                _extent = new ExtentReports();
                _extent.AttachReporter(sparkReporter);

                _extent.AddSystemInfo("Environment", "QA");
                _extent.AddSystemInfo("User", Environment.UserName);
                _extent.AddSystemInfo("Machine", Environment.MachineName);
            }

            return _extent;
        }

        public static void FlushReport()
        {
            _extent?.Flush();
        }
    }
}