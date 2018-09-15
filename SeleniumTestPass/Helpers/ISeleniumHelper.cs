using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace SeleniumTestPass.Helpers
{
    interface ISeleniumHelper
    {
        bool ExistsElement(IWebDriver driver, string xpath);
        void DoLogin(IWebDriver driver, string username, string password);
        void DoSearch(IWebDriver driver, string searchText);
        void DoLogout(IWebDriver driver);
        void CheckFacebookCommentInArticle(IWebDriver driver);
        bool WaitTillDisplayed(IWebDriver driver, string xpath);
    }

}