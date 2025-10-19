using OpenQA.Selenium;

using System.IO;
using System;

namespace project
{
    public static class ScreenshotHelper
    {
        public static string CaptureScreenshot(IWebDriver driver, string stepName)
        {
            // Bengali: Screenshot folder path set করছি
            string folderPath = Path.Combine(Directory.GetParent(AppContext.BaseDirectory).Parent.Parent.Parent.FullName, "Screenshots");
            Directory.CreateDirectory(folderPath);

            // Bengali: Screenshot file name timestamp সহ
            string fileName = $"{stepName}_{DateTime.Now:yyyyMMdd_HHmmss}.png";
            string fullPath = Path.Combine(folderPath, fileName);

            // Bengali: Screenshot তোলা এবং PNG format এ সেভ
            Screenshot ss = ((ITakesScreenshot)driver).GetScreenshot();
            ss.SaveAsFile(fullPath);
            //ss.SaveAsFile("ScreenshotImageFormat.png");

            return fullPath;
        }
    }
}