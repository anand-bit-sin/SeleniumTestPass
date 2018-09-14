using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumTestPass
{
    class LoginHomePage
    {
        private IWebDriver driver;

        //Page URL
        private static String PAGE_URL = "https://www.linkedin.com";

        public LoginHomePage(IWebDriver browser)
        {
            this.driver = browser;
            PageFactory.InitElements(browser, this);
        }

        [FindsBy(How = How.Id, Using = "sb_form_q")]
        public IWebElement Login { get; set; }

        public void Navigate()
        {
            this.driver.Navigate().GoToUrl(PAGE_URL);
        }
        //public void Search(string textToType)
        //{
        //    this.SearchBox.Clear();
        //    this.SearchBox.SendKeys(textToType);
        //    this.GoButton.Click();
        //}

        //public void ValidateResultsCount(string expectedCount)
        //{
        //    Assert.IsTrue(this.ResultsCountDiv.Text.Contains(expectedCount),
        //"The results DIV doesn't contains the specified text.");
        //}

    }
}
