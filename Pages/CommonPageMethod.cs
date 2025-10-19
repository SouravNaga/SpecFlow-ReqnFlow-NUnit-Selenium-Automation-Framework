using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Xml.Linq;

namespace OrangeHRM_Automation.Pages
{
    public class CommonPageMethod
    {
        public IWebDriver driver;

        // Constructor: Initializes the WebDriver instance for this class
        public CommonPageMethod(IWebDriver driver)
        {
            this.driver = driver;
        }

        // Waits until the specified element is visible on the page
        public void WaitForVisible(IWebElement locator, int timeout = 30)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeout));
            wait.Until(driver => locator.Displayed);
        }

        // Waits until the specified element is clickable
        public void WaitForClickable(IWebElement locator, int timeout = 30)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeout));
            wait.Until(ExpectedConditions.ElementToBeClickable(locator));
        }

        // Checks if the specified element is present on the page (Count > 0)
        public bool IsElementPresent(IWebElement element)
        {
            WaitForVisible(element, 30);
            try
            {
                return element.Displayed;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
            catch (StaleElementReferenceException)
            {
                return false;
            }
        }

        // Sends the given text to the specified input field
        public void enterText(IWebElement locator, string text)
        {
            locator.SendKeys(text);
        }

        // Clicks the specified button or clickable element
        public void clickBtn(IWebElement locator)
        {
            locator.Click();
        }
    }
}