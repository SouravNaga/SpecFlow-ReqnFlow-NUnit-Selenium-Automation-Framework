using OpenQA.Selenium;
using OrangeHRM_Automation.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReqnrollProject.StepDefinitions
{
    [Binding]
    public sealed class DataProviderSteps
    {
        public IWebDriver driver;
        public CommonPageMethod common;
        public LoginPage login;
        public DataProviderSteps(IWebDriver driver) { 
            this.driver = driver;
            common = new CommonPageMethod(driver);
            login = new LoginPage(driver);
        }


        [When(@"Username enters (.*)")]
        public void WhenUserEntersStudent(string user)
        {
            login.EnterUsername(user);
            
        }

        [When(@"Password enters (.*)")]
        public void WhenUserEntersPassword(string pass)
        {
            login.EnterPassword(pass);
        }



    }
}
