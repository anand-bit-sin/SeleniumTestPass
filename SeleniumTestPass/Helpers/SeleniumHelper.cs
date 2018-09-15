using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Support.UI;

namespace SeleniumTestPass.Helpers
{
    internal class SeleniumHelper : ISeleniumHelper
    {
        XmlHelper xHelper = new XmlHelper();
        public bool ExistsElement(IWebDriver driver, string xpath)
        {
            try
            {
                driver.FindElement(By.XPath(xpath));
            }
            catch (NoSuchElementException e)
            {
                return false;
            }
            return true;
        }
        public void DoLogin(IWebDriver driver, string username, string password)
        {
            IWebElement userNameTextBox = driver.FindElement(By.XPath(xHelper.GetXPathData("UserName")));
            userNameTextBox.SendKeys(username);
            IWebElement passwordTextbox = driver.FindElement(By.XPath(xHelper.GetXPathData("Password")));
            passwordTextbox.SendKeys(password);
            IWebElement submitButton = driver.FindElement(By.XPath(xHelper.GetXPathData("Submit")));
            submitButton.Click();
        }

        public void DoSearch(IWebDriver driver, string searchText)
        {
            // Find the search button and search
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            IWebElement searchButton = driver.FindElement(By.XPath(xHelper.GetXPathData("Search")));
            searchButton.Click();

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            IWebElement searchTextBox = driver.FindElement(By.XPath(xHelper.GetXPathData("SearchTextBox")));
            searchTextBox.SendKeys(searchText);
        }

        public void DoLogout(IWebDriver driver)
        {
            //Logout of the site
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            IWebElement userIconButton = driver.FindElement(By.XPath(xHelper.GetXPathData("UserNav")));
            userIconButton.Click();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

            // Scroll down to reach the logout link
            IJavaScriptExecutor jse = ((IJavaScriptExecutor)driver);
            jse.ExecuteScript("window.scrollTo(0, document.body.scrollHeight)");

            // Click on Logout
            IWebElement logoutLink = driver.FindElement(By.XPath("//*[@id='auw_profile_logout']"));
            logoutLink.Click();
        }

        public bool WaitTillDisplayed(IWebDriver driver, string xpath)
        {
            try
            {
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                wait.IgnoreExceptionTypes(typeof(NoSuchElementException), typeof(ElementNotVisibleException));
                wait.Until(d => !d.FindElement(By.XPath(xpath)).Displayed);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}


