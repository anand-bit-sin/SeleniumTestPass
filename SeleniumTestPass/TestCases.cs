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
        
        /// <summary>
        /// Test Case 1 for searching news articles with search text of place
        /// </summary>
        [TestCase]
        public void TestCase1()
        {
            string searchTextData = xHelper.GetFormData("TestSearchText");
            string searchResultLabelXPath = xHelper.GetXPathData("TopicSearchLabel");

            using (IWebDriver driver = new InternetExplorerDriver())
            {
                // Navigate to the site and login
                automationHelper.NavigateUrlAndLogin(driver);

                automationHelper.DoSearch(driver, searchTextData);

                if (automationHelper.WaitTillDisplayed(driver, searchResultLabelXPath))
                {
                    IWebElement searchLabel = driver.FindElement(By.XPath(searchResultLabelXPath));
                    Assert.IsTrue(searchLabel.Text == searchTextData);
                }
                else
                {
                    Console.WriteLine("Topic search label did not appear.");
                    Assert.Fail("Searched text does not appear as expected.");
                }

                automationHelper.DoLogout(driver);
                driver.Quit();
            }
        }

        /// <summary>
        /// Test Case 2 for verifying facebookk section in articles
        /// </summary>
        [TestCase]
        public void TestCase2()
        {
            string facebookSectionXPath = xHelper.GetXPathData("Facebook");

            using (IWebDriver driver = new InternetExplorerDriver())
            {
                // Navigate to the site and login
                automationHelper.NavigateUrlAndLogin(driver);

                // Navigate to an article page
                automationHelper.CheckFacebookCommentInArticle(driver);

                // Validate the expected scenario
                if (automationHelper.WaitTillDisplayed(driver, facebookSectionXPath))
                {
                    IWebElement facebookSection = driver.FindElement(By.XPath(facebookSectionXPath));
                    Assert.IsTrue(automationHelper.ExistsElement(driver, facebookSectionXPath));
                    Console.WriteLine("Verified facebook section present to provide comments.");
                }
                else
                {
                    Console.WriteLine("Facebook section did not appear.");
                    Assert.Fail("Facebook section not found in article as expected.");
                }

                // Logout
                automationHelper.DoLogout(driver);
                driver.Quit();
            }
        }
    }
}