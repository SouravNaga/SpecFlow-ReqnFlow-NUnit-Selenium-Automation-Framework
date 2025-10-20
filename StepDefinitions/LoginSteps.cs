using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OrangeHRM_Automation.Pages;

namespace ReqnrollProject.StepDefinitions
{
    [Binding]
    public sealed class LoginSteps
    {
        public IWebDriver driver;
        public CommonPageMethod common;
        
        public LoginSteps(IWebDriver driver)
        {
            this.driver = driver;
        }

        [Given("Open the url")]
        public void GivenOpenTheUrl()
        {
            //driver =  new ChromeDriver();g
            //driver.Manage().Window.Maximize();
            driver.Url = "https://practicetestautomation.com/practice-test-login/";
            common = new CommonPageMethod(driver);

        }

        [When("User enter username")]
        public void WhenUserEnterUsername()
        {
            IWebElement username = driver.FindElement(By.Id("username"));
            username.SendKeys("student");
        }

        [When("User enter password")]
        public void WhenUserEnterPassword()
        {
            IWebElement password = driver.FindElement(By.Id("password"));
            password.SendKeys("Password123");
        }

        [When("User click submit button")]
        public void WhenUserClickSubmitButton()
        {
            IWebElement submitBtn = driver.FindElement(By.Id("submit"));
            submitBtn.Click();
        }

        [Then("Login should be successful")]
        public void ThenLoginShouldBeSuccessful()
        {
            IWebElement postMsg = driver.FindElement(By.XPath("//h1[@class='post-title']"));
            bool flag = common.IsElementPresent(postMsg);
            Assert.IsTrue(flag);
            Console.WriteLine(postMsg.Text);
            //driver.Quit();

        }
        

    }
}
