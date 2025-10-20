using AventStack.ExtentReports;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using project;
using ReqnFlowFramework.Reporting;
using Reqnroll;
using Reqnroll.BoDi;
using Utility;

namespace ReqnrollProject.Hooks
{
    [Binding]
    public sealed class Hooks
    {
        private readonly IObjectContainer _container;
        private readonly ScenarioContext _scenarioContext;

        public Hooks(IObjectContainer container, ScenarioContext scenarioContext)
        {
            _container = container;
            _scenarioContext = scenarioContext;
        }

        // Bengali: ট্যাগ ভিত্তিক হুক
        [BeforeScenario("@validLogin")]
        public void BeforeScenarioWithTag()
        {
            Console.WriteLine("This is tag-based hook");
        }

        // Bengali: WebDriver সেটআপ
        [BeforeScenario(Order = 1)]
        public void FirstBeforeScenario()
        {
            IWebDriver driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            _container.RegisterInstanceAs<IWebDriver>(driver);
        }

        // Bengali: Extent Test শুরু
        [BeforeScenario(Order = 2)]
        public void CreateExtentTest()
        {
            string scenarioName = _scenarioContext.ScenarioInfo.Title;
            ExtentTestManager.CreateTest(scenarioName);
        }

        // Bengali: প্রতিটি স্টেপের পর লগ + স্ক্রিনশট সহ হাইলাইট
        [AfterStep]
        public void LogStepResult()
        {
            var stepText = _scenarioContext.StepContext.StepInfo.Text;
            var test = ExtentTestManager.GetTest();
            var driver = _container.Resolve<IWebDriver>();
            string screenshotPath = ScreenshotHelper.CaptureScreenshot(driver, stepText);

            // Status-based styling
            string color = _scenarioContext.TestError == null ? "#d4edda" : "#f8d7da"; // Green or red background
            string border = _scenarioContext.TestError == null ? "#28a745" : "#dc3545"; // Left border
            string statusText = _scenarioContext.TestError == null ? "✅ Step Passed" : "❌ Step Failed";
            string errorMsg = _scenarioContext.TestError?.Message ?? "";

            // HTML block for report
            string htmlBlock = $@"
            <div style='background-color:{color}; border-left:5px solid {border}; padding:10px; margin-bottom:10px;'>
                <strong>{statusText}:</strong> {stepText}<br>
                {(string.IsNullOrEmpty(errorMsg) ? "" : $"<span style='color:#dc3545;'>{errorMsg}</span><br>")}
                <a href='{screenshotPath}' target='_blank'>
                    <img src='{screenshotPath}' width='400' style='border:1px solid #ccc; padding:4px;' />
                </a>
            </div>";

            test.Log(_scenarioContext.TestError == null ? Status.Pass : Status.Fail, htmlBlock);
        }

        // Bengali: WebDriver বন্ধ
        [AfterScenario]
        public void AfterScenario()
        {
            var driver = _container.Resolve<IWebDriver>();
            driver.Quit();
        }

        // Bengali: রিপোর্ট শুরু
        [BeforeTestRun(Order = 0)]
        public static void BeforeTestRun()
        {
            ExtentReportManager.GetExtent(); // ✅ Initializes _extent
        }

        // Bengali: রিপোর্ট শেষ
        [AfterTestRun]
        public static void AfterTestRun()
        {
            ExtentReportManager.FlushReport();
        }
    }
}