using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.IE;

namespace SeleniumTestPass.Helpers
{
    class SeleniumHelper: ISeleniumHelper
    {
        IWebDriver driver;
        public bool ExistsElement(String xpath)
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
        public void DoLogin(string username, string password)
        {
            IWebElement userNameTextBox = driver.FindElement(By.Name("email"));
            userNameTextBox.SendKeys("anand.bit.sin@gmail.com");
            IWebElement passwordTextbox = driver.FindElement(By.Name("password"));
            passwordTextbox.SendKeys("Gyan@123");

            IWebElement submitButton = driver.FindElement(By.Name("sbmt"));
            submitButton.Click();
        }
        public void DoSearch(string searchText)
        {

        }

        public void DoLogout()
        {
            //Logout of the site
            IWebElement userIconButton = driver.FindElement(By.XPath("//*[@id='usernav']"));
            userIconButton.Click();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

            // Scroll down to reach the logout link
            IJavaScriptExecutor jse = ((IJavaScriptExecutor)driver);
            jse.ExecuteScript("window.scrollTo(0, document.body.scrollHeight)");

            // Click on Logout
            IWebElement logoutLink = driver.FindElement(By.XPath("//*[@id='auw_profile_logout']"));
            logoutLink.Click();
        }

        public bool WaitTillDisplayed(string xpath)
        {
            try
            {
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                wait.Until(driver => !driver.FindElement(By.XPath(xpath)).Displayed);
                return true;
            }
            catch(NoSuchElementException e)
            {
                return false;
            }
        }
}

   
}
