using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace SeleniumTestPass.Helpers
{
    class XmlHelper : IXmlHelper
    {
        string testdataXmlFile = "TestData.xml";
        public string GetFormData(string xmlTag)
        {
            XmlDocument testdata = new XmlDocument();
            testdata.Load(testdataXmlFile);
            XmlNode nodeFormData = testdata.SelectSingleNode("/TestData/FormData");
            foreach (XmlNode node in nodeFormData.ChildNodes)
            {
                if (string.Equals(node.Name, xmlTag, StringComparison.InvariantCultureIgnoreCase) == true)
                {
                    return node.InnerText;
                }
            }

            return "xml node not found";
        }

        public string GetXPathData(string xmlTag)
        {
            XmlDocument testdata = new XmlDocument();
            testdata.Load(testdataXmlFile);
            XmlNode nodeXPathData = testdata.SelectSingleNode("/TestData/XPathData");
            foreach (XmlNode node in nodeXPathData.ChildNodes)
            {
                if (string.Equals(node.Name, xmlTag, StringComparison.InvariantCultureIgnoreCase) == true)
                {
                    return node.InnerText;
                }
            }

            return "xml node not found";
        }
    }
}












