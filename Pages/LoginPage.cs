using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using SeleniumExtras.PageObjects;
using OpenQA.Selenium.Support.UI;
using NUnit.Framework;
using SeleniumExtras.WaitHelpers;
using System;

namespace OrangeHRM_Automation.Pages
{
    public class LoginPage
    {
        public IWebDriver driver = null;
        public CommonPageMethod common;
        // 🔧 Constructor: WebDriver initialize
        public LoginPage(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);

        }


        // Username input field
        [FindsBy(How = How.Id, Using = "username")]
        private IWebElement userTxt;

        // Password input field
        [FindsBy(How = How.Id, Using = "password")]
        private IWebElement passwordLocator;

        // Login button
        [FindsBy(How = How.XPath, Using = "//button[@type='submit']")]
        private IWebElement loginBtnLocator;

        // Search input field
        [FindsBy(How = How.XPath, Using = "//input[@placeholder='Search']")]
        private IWebElement submitBtnLocator;


        // 🧑‍💻 Enter Username
        public void EnterUsername(string username)
        {
            common = new CommonPageMethod(driver);
            common.WaitForVisible(userTxt);
            common.HighlightElement(driver, userTxt);
            common.enterText(userTxt, username);
        }

        // 🔐 Enter Password
        public void EnterPassword(string password)
        {
            common.WaitForVisible(passwordLocator);
            common.HighlightElement(driver, passwordLocator);
            common.enterText(passwordLocator, password);
        }

        // 🚪 Click Login Button + Assert Submit Button Visible
        public void ClickBtn()
        {

            common.WaitForClickable(loginBtnLocator);
            common.HighlightElement(driver, loginBtnLocator);
            common.clickBtn(loginBtnLocator);

            // ✅ Assertion: Submit button visible
            bool isSubmitVisible = common.IsElementPresent(submitBtnLocator);
            common.HighlightElement(driver, submitBtnLocator);
            Assert.IsTrue(isSubmitVisible, "Submit button is not present on the page.");
        }
    }
}