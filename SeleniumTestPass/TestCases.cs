using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Support.UI;

namespace SeleniumTestPass
{
    [TestFixture]
    public class TestCases
    {
        [Test]
        public void TestCase1()
        {
            Console.WriteLine("Starting test case.. ");
            try
            {
                Assert.IsTrue("ANAND".ToLower() == "anand");
                Console.WriteLine("TestCase1 passed..");
            }
            catch (Exception ex)
            {
                Console.WriteLine("TestCase1 failed with exception..");
            }            
        }

        [TestCase]
        public void TestCase2()
        {
            using (IWebDriver driver = new InternetExplorerDriver())
            {
                //Check if logged in, go with test steps

                // Navigate to the site
                driver.Navigate().GoToUrl("https://www.amarujala.com/");

                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);// ImplicitlyWait(TimeSpan.FromSeconds(10));
                driver.Manage().Window.Maximize();
                
                //driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(20);

                // Find the login button by its name
                IWebElement loginButton = driver.FindElement(By.Name("login"));
                loginButton.Click();
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);


                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

                IWebElement userDetail = driver.FindElement(By.XPath("//*[@id='usernav']/h3"));
                Console.WriteLine(userDetail.Text);
                Assert.IsTrue(userDetail.Text == "Anand Kumar");



                Console.WriteLine("User name verified and logged out");
                Console.ReadLine();
                driver.Quit();
            }
        }

        [TestCase]
        public void TestCase3()
        {
        
        }
    }
}
