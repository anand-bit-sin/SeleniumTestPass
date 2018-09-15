using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;

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

        public void NavigateUrlAndLogin(IWebDriver driver)
        {
            driver.Navigate().GoToUrl("https://www.amarujala.com/");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            driver.Manage().Window.Maximize();

            if (WaitTillDisplayed(driver, xHelper.GetXPathData("UserNav")))
            {
                DoLogout(driver);
            }

            // Find the login button by its name
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            IWebElement loginButton = driver.FindElement(By.XPath(xHelper.GetXPathData("Login")));
            Actions actions = new Actions(driver);
            actions.MoveToElement(loginButton).Click().Perform();

            //Click again if required
            IJavaScriptExecutor jse = ((IJavaScriptExecutor)driver);
            jse.ExecuteScript("window.scrollTo(0, 250)");
            //if(!ExistsElement(driver, xHelper.GetFormData("username")))
            //actions.MoveToElement(loginButton).Click().Perform();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);

            DoLogin(driver, xHelper.GetFormData("username"), xHelper.GetFormData("password"));
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        }

        public void DoLogin(IWebDriver driver, string username, string password)
        {
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            if (WaitTillDisplayed(driver, xHelper.GetXPathData("UserName")))
            {
                IWebElement userNameTextBox = driver.FindElement(By.XPath(xHelper.GetXPathData("UserName")));
                userNameTextBox.SendKeys(username);
                IWebElement passwordTextbox = driver.FindElement(By.XPath(xHelper.GetXPathData("Password")));
                passwordTextbox.SendKeys(password);
                IWebElement submitButton = driver.FindElement(By.XPath(xHelper.GetXPathData("Submit")));
                submitButton.Click();
            }
            else
                Console.WriteLine("User name did not appear..");
        }

        public void DoSearch(IWebDriver driver, string searchText)
        {
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            // Find the search button and search
            if (WaitTillDisplayed(driver, xHelper.GetXPathData("Search")))
            {
                IWebElement searchButton = driver.FindElement(By.XPath(xHelper.GetXPathData("Search")));
                Actions actions = new Actions(driver);
                actions.MoveToElement(searchButton).Click().Perform();
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
                Console.WriteLine("Done logout");
            }
            else
                Console.WriteLine("Logout link did not appear");
        }

        public void CheckFacebookCommentInArticle(IWebDriver driver)
        {
            WebDriverWait wait2 = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait2.Until(ExpectedConditions.ElementToBeClickable(By.XPath(xHelper.GetXPathData("TrendingHotnews"))));

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            IWebElement bellIconButton = driver.FindElement(By.XPath(xHelper.GetXPathData("TrendingHotnews")));
            Actions actions = new Actions(driver);
            actions.MoveToElement(bellIconButton).Click().Perform();

            IWebElement articleButton = driver.FindElement(By.XPath(xHelper.GetXPathData("NewsArticle1")));
            articleButton.Click();

            // Scroll down to reach the facebook section
            IWebElement facebookSection = driver.FindElement(By.XPath(xHelper.GetXPathData("Facebook")));
            IJavaScriptExecutor jse = ((IJavaScriptExecutor)driver);
            jse.ExecuteScript("arguments[0].scrollIntoView();", facebookSection);

            WaitTillDisplayed(driver, xHelper.GetXPathData("Facebook"));
        }        
    }
}


