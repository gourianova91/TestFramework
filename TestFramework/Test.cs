using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using System.Threading;


namespace TestFramework
{
    class BaseTest
    {
        public static string url = "http://www.bbc.com/";

        [TearDown]
        public void endTest()
        {
            Driver.Instance.stopBrowser();
        }
    }

    [TestFixture]
    class TestInChrome : BaseTest
    {
        [SetUp]
        public void startTest()
        {
            Driver.Instance.GetDriver(Driver.BrowserType.Chrome);
        }

        [Test]
        public void testInChrome()
        {
            BBCPage bbc = new BBCPage();
            bbc.navigateTo(url);
        }
    }

    [TestFixture]
    class TestInFirefox : BaseTest
    {
        [SetUp]
        public void startTest()
        {
            Driver.Instance.GetDriver(Driver.BrowserType.Firefox);
        }

        [Test]
        public void testInFirefox()
        {
            BBCPage bbc = new BBCPage();
            bbc.navigateTo(url);
        }
    }

    [TestFixture]
    class TestInIE : BaseTest
    {
        [SetUp]
        public void startTest()
        {
            Driver.Instance.GetDriver(Driver.BrowserType.IE);
        }

        [Test]
        public void testInIE()
        {
            BBCPage bbc = new BBCPage();
            bbc.navigateTo(url);
        }
    }
}
