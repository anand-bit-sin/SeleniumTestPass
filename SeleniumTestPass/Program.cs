using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Requires reference to WebDriver.Support.dll
using OpenQA.Selenium.Support.UI;

namespace SeleniumTestPass
{
    class Program
    {
        static void Main(string[] args)
        {
            TestCases tcs = new TestCases();
            // tcs.TestCase1();
            tcs.TestCase2();

        }
    }
}
