using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace SeleniumTestPass.Helpers
{
    class XmlHelper: IXmlHelper
    {
        string testdataXmlFile = "TestData.xml";
        public string GetFormData(string xmlTag)
        {
            var doc = XmlDocument.Load(testdataXmlFile);
            var value = doc.Root.Element("C").Element("D").Value;
        }
        public string GetXPathData(string xmlTag)
        {

            return "";
        }
    }
}
