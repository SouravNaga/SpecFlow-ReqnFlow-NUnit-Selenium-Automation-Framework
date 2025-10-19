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

        // Bengali: প্রতিটি স্টেপের পর লগ + স্ক্রিনশট (যদি fail করে)
        //[AfterStep]
        //public void LogStepResult()
        //{
        //    var stepText = _scenarioContext.StepContext.StepInfo.Text;
        //    var test = ExtentTestManager.GetTest();

        //    if (_scenarioContext.TestError == null)
        //    {

        //        test.Log(Status.Pass, $"✅ Step Passed: {stepText}");

        //    }
        //    else
        //    {
        //        string errorMsg = _scenarioContext.TestError.Message;
        //        var driver = _container.Resolve<IWebDriver>();
        //        string screenshotPath = ScreenshotHelper.CaptureScreenshot(driver, stepText);
        //        test.Log(Status.Fail, $"❌ Step Failed: {stepText}<br>{errorMsg}");
        //        test.AddScreenCaptureFromPath(screenshotPath);
        //    }
        //}

        [AfterStep]
        public void LogStepResult()
        {
            var stepText = _scenarioContext.StepContext.StepInfo.Text;
            var test = ExtentTestManager.GetTest();
            var driver = _container.Resolve<IWebDriver>();
            string screenshotPath = ScreenshotHelper.CaptureScreenshot(driver, stepText);

            if (_scenarioContext.TestError == null)
            {
                test.Log(Status.Pass, $"✅ Step Passed: {stepText}<br><img src='{screenshotPath}' width='400' />");
            }
            else
            {
                string errorMsg = _scenarioContext.TestError.Message;
                test.Log(Status.Fail, $"❌ Step Failed: {stepText}<br>{errorMsg}<br><img src='{screenshotPath}' width='400' />");
            }
        }

        // Bengali: WebDriver বন্ধ
        [AfterScenario]
        public void AfterScenario()
        {
            var driver = _container.Resolve<IWebDriver>();
            driver.Quit();
        }

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