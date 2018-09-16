using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;

namespace SeleniumTestPass.Helpers
{
    internal class SeleniumHelper : ISeleniumHelper
    {
        XmlHelper xHelper = new XmlHelper();

        /// <summary>
        /// Check if element exists on the page
        /// </summary>
        /// <param name="driver">webdriver used</param>
        /// <param name="xpath">xpath of element to check</param>
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

        /// <summary>
        /// Check if element is displayed on page
        /// </summary>
        /// <param name="driver">webdriver used</param>
        /// <param name="xpath">xpath of element to check</param>
        public bool WaitTillDisplayed(IWebDriver driver, string xpath)
        {
            try
            {
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                wait.Until(ExpectedConditions.ElementExists(By.XPath(xpath)));
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        /// <summary>
        /// Navigare to website and login if required
        /// </summary>
        /// <param name="driver">webdriver used</param>
        public void NavigateUrlAndLogin(IWebDriver driver)
        {
            string websiteToNavigate = xHelper.GetNavigationData("SiteLink1");
            string userNavXPath = xHelper.GetXPathData("UserNav");
            string username = xHelper.GetFormData("UserName");
            string password = xHelper.GetFormData("Password");
            string loginButtonXPath = xHelper.GetXPathData("Login");

            driver.Navigate().GoToUrl(websiteToNavigate);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            driver.Manage().Window.Maximize();

            if (WaitTillDisplayed(driver, userNavXPath))
            {
                return;
            }

            // Find the login button and click
            IWebElement loginButton = driver.FindElement(By.XPath(loginButtonXPath));
            Actions actions = new Actions(driver);
            actions.MoveToElement(loginButton).Click().Perform();

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);

            DoLogin(driver, username, password);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        }

        /// <summary>
        /// Navigare to website and login if required
        /// </summary>
        /// <param name="driver">webdriver used</param>
        /// <param name="username">username for website</param>
        /// <param name="password">password for the username</param>
        public void DoLogin(IWebDriver driver, string username, string password)
        {
            string usernameXPath = xHelper.GetXPathData("UserName");
            string passwordXPath = xHelper.GetXPathData("Password");
            string submitXPath = xHelper.GetXPathData("Submit");

            if (WaitTillDisplayed(driver, usernameXPath))
            {
                IWebElement userNameTextBox = driver.FindElement(By.XPath(usernameXPath));
                userNameTextBox.SendKeys(username);
                IWebElement passwordTextbox = driver.FindElement(By.XPath(passwordXPath));
                passwordTextbox.SendKeys(password);
                IWebElement submitButton = driver.FindElement(By.XPath(submitXPath));
                submitButton.Click();
            }
            else
                Console.WriteLine("User name did not appear after login click");
        }

        /// <summary>
        /// Search for a text in searchtext
        /// </summary>
        /// <param name="driver">webdriver used</param>
        /// <param name="searchText">text to search in the searchbox</param>
        public void DoSearch(IWebDriver driver, string searchText)
        {
            string searchButtonXPath = xHelper.GetXPathData("Search");
            string searchTextBoxXPath = xHelper.GetXPathData("SearchTextBox");

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);

            // Find the search button and search
            if (WaitTillDisplayed(driver, searchButtonXPath))
            {
                IWebElement searchButton = driver.FindElement(By.XPath(searchButtonXPath));
                Actions actions = new Actions(driver);
                actions.MoveToElement(searchButton).Click().Perform();
            }
            else
                Console.WriteLine("Search button did not appear");

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);

            if (WaitTillDisplayed(driver, searchTextBoxXPath))
            {
                IWebElement searchTextBox = driver.FindElement(By.XPath(searchTextBoxXPath));
                searchTextBox.SendKeys(searchText);
                searchTextBox.SendKeys(Keys.Enter);
            }
            else
                Console.WriteLine("Search Text Box did not appear");
        }

        /// <summary>
        /// Do logout, click on user icon, scroll to logout link and click
        /// </summary>
        /// <param name="driver">webdriver used</param>
        public void DoLogout(IWebDriver driver)
        {
            string userNavXPath = xHelper.GetXPathData("UserNav");
            string logoutLinkXPath = xHelper.GetXPathData("Logout");

            //Logout of the site
            if (WaitTillDisplayed(driver, userNavXPath))
            {
                IWebElement userIconButton = driver.FindElement(By.XPath(userNavXPath));
                userIconButton.Click();
            }
            else
                Console.WriteLine("User Icon did not appear.");

            // Scroll down to reach the logout link
            IJavaScriptExecutor jse = ((IJavaScriptExecutor)driver);
            jse.ExecuteScript("window.scrollTo(0, 250)");

            // Click on Logout
            if (WaitTillDisplayed(driver, logoutLinkXPath))
            {
                IWebElement logoutLink = driver.FindElement(By.XPath(logoutLinkXPath));
                logoutLink.Click();
                Console.WriteLine("Done logout");
            }
            else
                Console.WriteLine("Logout link did not appear");
        }

        /// <summary>
        /// Check on hot news link and open first article to check facebook section
        /// </summary>
        /// <param name="driver">webdriver used</param>
        public void CheckFacebookCommentInArticle(IWebDriver driver)
        {
            string trendingNewsXPath = xHelper.GetXPathData("TrendingHotnews");
            string newsArticle1XPath = xHelper.GetXPathData("NewsArticle1");
            string facebookSectionXPath = xHelper.GetXPathData("Facebook");

            WebDriverWait wait2 = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait2.Until(ExpectedConditions.ElementToBeClickable(By.XPath(trendingNewsXPath)));

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            IWebElement bellIconButton = driver.FindElement(By.XPath(trendingNewsXPath));
            Actions actions = new Actions(driver);
            actions.MoveToElement(bellIconButton).Click().Perform();

            IWebElement articleButton = driver.FindElement(By.XPath(newsArticle1XPath));
            articleButton.Click();
            
            WaitTillDisplayed(driver, facebookSectionXPath);
        }        
    }
}


