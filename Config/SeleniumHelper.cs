using OpenQA.Selenium;
using OpenQA.Selenium.Support.Extensions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Policy;
using ExpectedConditions = SeleniumExtras.WaitHelpers.ExpectedConditions;

namespace TestJamaTestMgmt.Config
{
    /// <summary>
    /// Abstration of WEBDRIVER
    /// </summary>
    public class SeleniumHelper : IDisposable
    {
        public IWebDriver WebDriver;
        public readonly ConfigurationHelper Configuration;
        public WebDriverWait Wait;

        public SeleniumHelper(Browser browser, ConfigurationHelper configuration, bool headless = true)
        {
            Configuration = configuration;
            WebDriver = WebDriverFactory.CreateWebDriver(browser, configuration.WebDrivers, headless);
            WebDriver.Manage().Window.Maximize();
            Wait = new WebDriverWait(WebDriver, TimeSpan.FromSeconds(30));
        }

        public string GetUrl()
        {
            return  WebDriver.Url;
        }

        public void ClickLinkByText(string linkText)
        {
            Wait.Until(ExpectedConditions.ElementIsVisible(By.LinkText(linkText)))
                .Click();
        }

        public void GoToUrl(string url) 
        {
            WebDriver.Navigate().GoToUrl(url);
        }

        public bool ValidateUrlContent(string conteudo)
        {
            return Wait.Until(ExpectedConditions.UrlContains(conteudo));
        }

        public void ClickButtonById(string buttonId)
        {
            var button = Wait.Until(ExpectedConditions.ElementIsVisible(By.Id(buttonId)));
            button.Click();
        }

        public void ClickByXPath(string xpath)
        {
            var element = Wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(xpath)));
            element.Click();
        }

        public IWebElement GetElementByClass(string cssClass)
        { 
            return Wait.Until(ExpectedConditions.ElementIsVisible(By.ClassName(cssClass)));
        }

        public IWebElement GetElementById(string id)
        {
            return Wait.Until(ExpectedConditions.ElementIsVisible(By.Id(id)));
        }

        public IWebElement GetElementByXpath(string xpath)
        {
            return Wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(xpath)));
        }

        public IWebElement GetElementClickable(string xpath)
        {
            return Wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath(xpath)));
        }

        public void FillTextBoxById(string id, string content)
        {
            var element = Wait.Until(ExpectedConditions.ElementIsVisible(By.Id(id)));
            element.SendKeys(content);
        }

        public void FillDropDownById(string id, string fieldContent)
        {
            var element = Wait.Until(ExpectedConditions.ElementIsVisible(By.Id(id)));
            var selectElement = new SelectElement(element);
            selectElement.SelectByValue(fieldContent);
        }

        public string GetElementTextByClass(string CssClass)
        {
            return Wait.Until(ExpectedConditions.ElementIsVisible(By.ClassName(CssClass))).Text;
        }

        public string GetElementTextById(string id)
        {
            return Wait.Until(ExpectedConditions.ElementIsVisible(By.Id(id))).Text; ;
        }

        public string GetTextBoxValueById(string id)
        {
            return Wait.Until(ExpectedConditions.ElementIsVisible(By.Id(id))).GetAttribute("value"); ;
        }

        public IEnumerable<IWebElement> GetListByClass(string ClassName) 
        {
            return Wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(By.ClassName(ClassName)));
        }

        public IEnumerable<IWebElement> GetListByXpath(string xpath)
        {
            return Wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(By.XPath(xpath)));
        }

        public bool ValidateElementById(string id)
        {
            return ElementExists(By.Id(id));
        }

        public void NavigateBack(int times)
        {
            for (int i = 0; i < times;)
            {
                WebDriver.Navigate().Back(); 
            }
        }

        public void GetScreenshot(string name)
        {
            SaveScreenshot(WebDriver.TakeScreenshot(), string.Format("{0}_" + name + ".png", DateTime.Now.ToFileTime()));
        }

        private void SaveScreenshot(Screenshot screenshot, string fileName)
        {
            screenshot.SaveAsFile($"{Configuration.FolderPicture}{fileName}", ScreenshotImageFormat.Png );
        }

        private bool ElementExists(By by)
        {
            try
            {
                WebDriver.FindElement(by);
                return true;
            }
            catch
            {
                return false;
            }
        }
        
        public bool isTimeout(long originalTime, long timeoutInSeconds)
        {
            var time = DateTime.Now.Millisecond;

            long waitTime = timeoutInSeconds * 1000;
            long endTime = originalTime + waitTime;

            return time > endTime;
        }

        public bool isElementEnabled(IWebElement element)
        {
            if (element == null)
            {
                return false;
            }

            try
            {
                return element.Enabled;
            }
            catch 
            { 
                return false; 
            }
            
        }

        public bool waitForElementToVanish(IWebElement element, long timeout)
        {
            var startTime = DateTime.Now.Millisecond;
            bool wasFound = isElementEnabled(element);

            while (wasFound && !isTimeout(startTime, timeout))
            { 
                wasFound = isElementEnabled(element);
            }

            return !wasFound;
        }

        public void Dispose()
        {
            WebDriver.Quit();
            WebDriver.Dispose(); 
        }
    }
}
