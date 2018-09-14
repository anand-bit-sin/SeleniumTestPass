using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumTestPass.Helpers
{
    interface IXmlHelper
    {
        string GetFormData(string xmlTag);
        string GetXPathData(string xmlTag);
    }
}
