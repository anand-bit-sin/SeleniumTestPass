using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.IE;
using NUnit.Framework;
using SeleniumTestPass.Helpers;

namespace SeleniumTestPass
{
    [TestFixture]
    public class TestCases
    {
        SeleniumHelper automationHelper = new SeleniumHelper();
        XmlHelper xHelper = new XmlHelper();

        [TestCase]
        public void TestCase1()
        {
            using (IWebDriver driver = new InternetExplorerDriver())
            {
                // Navigate to the site
                driver.Navigate().GoToUrl("https://www.amarujala.com/");
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
                driver.Manage().Window.Maximize();
                
                if (automationHelper.WaitTillDisplayed(driver, xHelper.GetXPathData("UserNav")))
                {
                    automationHelper.DoLogout(driver);
                }

                // Find the login button by its name
                IWebElement loginButton = driver.FindElement(By.XPath(xHelper.GetXPathData("Login")));
                loginButton.Click();
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);

                automationHelper.DoLogin(driver, xHelper.GetFormData("username"), xHelper.GetFormData("password"));
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
                automationHelper.DoSearch(driver, "Pune");

                if (automationHelper.WaitTillDisplayed(driver, xHelper.GetXPathData("TopicSearchLabel")))
                {
                    IWebElement searchLabel = driver.FindElement(By.XPath(xHelper.GetXPathData("TopicSearchLabel")));
                    Assert.IsTrue(searchLabel.Text == "Pune");
                }
                else
                    Console.WriteLine("Topic search label did not appear.");

                automationHelper.DoLogout(driver);

                driver.Quit();
            }
        }

        [TestCase]
        public void TestCase2()
        {
            using (IWebDriver driver = new InternetExplorerDriver())
            {
                // Navigate to the site
                driver.Navigate().GoToUrl("https://www.amarujala.com/");
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
                driver.Manage().Window.Maximize();

                if (automationHelper.WaitTillDisplayed(driver, xHelper.GetXPathData("UserNav")))
                {
                    automationHelper.DoLogout(driver);
                }

                // Find the login button by its name
                IWebElement loginButton = driver.FindElement(By.XPath(xHelper.GetXPathData("Login")));
                loginButton.Click();
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);

                automationHelper.DoLogin(driver, xHelper.GetFormData("username"), xHelper.GetFormData("password"));
                automationHelper.CheckFacebookCommentInArticle(driver);

                IWebElement facebookSection = driver.FindElement(By.XPath(xHelper.GetXPathData("Facebook")));
                Assert.IsTrue(automationHelper.ExistsElement(driver, xHelper.GetXPathData("Facebook")));
                automationHelper.DoLogout(driver);

                driver.Quit();
            }
        }
    }
}