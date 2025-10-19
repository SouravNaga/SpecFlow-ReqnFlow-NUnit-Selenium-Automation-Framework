using AventStack.ExtentReports;
using Utility;

namespace ReqnFlowFramework.Reporting
{
    public class ExtentTestManager
    {
        private static ExtentTest _test;

        public static ExtentTest CreateTest(string testName)
        {
            // ✅ Assume ExtentReportManager.GetExtent() already called in Hooks
            var extent = ExtentReportManager.GetExtent();
            _test = extent.CreateTest(testName);
            return _test;
        }

        public static ExtentTest GetTest()
        {
            return _test;
        }
    }
}