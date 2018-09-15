using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.IE;
using NUnit.Framework;
using SeleniumTestPass.Helpers;
using OpenQA.Selenium.Interactions;

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
            string userNav = xHelper.GetXPathData("");
            using (IWebDriver driver = new InternetExplorerDriver())
            {
                // Navigate to the site and login
                automationHelper.NavigateUrlAndLogin(driver);

                automationHelper.DoSearch(driver, "Pune");

                if (automationHelper.WaitTillDisplayed(driver, xHelper.GetXPathData("TopicSearchLabel")))
                {
                    IWebElement searchLabel = driver.FindElement(By.XPath(xHelper.GetXPathData("TopicSearchLabel")));
                    Assert.IsTrue(searchLabel.Text == "Pune");
                    Console.WriteLine("Verified search label..");
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
                // Navigate to the site and login
                automationHelper.NavigateUrlAndLogin(driver);

                automationHelper.CheckFacebookCommentInArticle(driver);

                if (automationHelper.WaitTillDisplayed(driver, xHelper.GetXPathData("Facebook")))
                {
                    IWebElement facebookSection = driver.FindElement(By.XPath(xHelper.GetXPathData("Facebook")));
                    Assert.IsTrue(automationHelper.ExistsElement(driver, xHelper.GetXPathData("Facebook")));
                    Console.WriteLine("Verified facebook section present to provide comments..");
                }
                else
                    Console.WriteLine("Facebook section did not appear..");

                automationHelper.DoLogout(driver);

                driver.Quit();
            }
        }
    }
}