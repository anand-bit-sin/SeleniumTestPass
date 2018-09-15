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
            if (WaitTillDisplayed(driver, xHelper.GetXPathData("Search")))
            {
                IWebElement searchButton = driver.FindElement(By.XPath(xHelper.GetXPathData("Search")));
                searchButton.Click();
            }
            else
                Console.WriteLine("Search button did not appear");

            if (WaitTillDisplayed(driver, xHelper.GetXPathData("SearchTextBox")))
            {
                IWebElement searchTextBox = driver.FindElement(By.XPath(xHelper.GetXPathData("SearchTextBox")));
                searchTextBox.SendKeys(searchText);
                searchTextBox.SendKeys(Keys.Enter);
            }
            else
                Console.WriteLine("Search Text Box did not appear");
        }

        public void DoLogout(IWebDriver driver)
        {
            //Logout of the site
            if (WaitTillDisplayed(driver, xHelper.GetXPathData("UserNav")))
            {
                IWebElement userIconButton = driver.FindElement(By.XPath(xHelper.GetXPathData("UserNav")));
                userIconButton.Click();
            }
            else
                Console.WriteLine("User Icon did not appear.");

            // Scroll down to reach the logout link
            IJavaScriptExecutor jse = ((IJavaScriptExecutor)driver);
            jse.ExecuteScript("window.scrollTo(0, 250)");

            // Click on Logout
            if (WaitTillDisplayed(driver, xHelper.GetXPathData("Logout")))
            {
                IWebElement logoutLink = driver.FindElement(By.XPath(xHelper.GetXPathData("Logout")));
                logoutLink.Click();
            }
            else
                Console.WriteLine("Logout link did not appear");
        }

        public void CheckFacebookCommentInArticle(IWebDriver driver)
        {
            IWebElement bellIconButton = driver.FindElement(By.XPath(xHelper.GetXPathData("TrendingHotnews")));
            bellIconButton.Click();

            IWebElement articleButton = driver.FindElement(By.XPath(xHelper.GetXPathData("NewsArticle1")));
            articleButton.Click();

            // Scroll down to reach the logout link
            IJavaScriptExecutor jse = ((IJavaScriptExecutor)driver);
            jse.ExecuteScript("window.scrollTo(0, 250)");

            WaitTillDisplayed(driver, xHelper.GetXPathData("Facebook"));
        }

        public bool WaitTillDisplayed(IWebDriver driver, string xpath)
        {
            try
            {
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                //wait.IgnoreExceptionTypes(typeof(NoSuchElementException), typeof(ElementNotVisibleException));
                wait.Until(ExpectedConditions.ElementExists(By.XPath(xpath)));
                //wait.Until(d => d.FindElement(By.XPath(xpath)).Text != null);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}


