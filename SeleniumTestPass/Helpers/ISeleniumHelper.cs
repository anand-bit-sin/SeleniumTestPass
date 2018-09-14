using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumTestPass.Helpers
{
   interface ISeleniumHelper
    {
        bool ExistsElement(string xpath);
        void DoLogin(string username, string password);
        void DoSearch(string searchText);
        void DoLogout();
        bool WaitTillDisplayed(string xpath);
    }
}
