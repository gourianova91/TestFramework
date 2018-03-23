using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using System.Threading;


namespace TestFramework
{
    [TestFixture]
    class Test
    {
        [SetUp]
        public void startTest()
        {
            Driver.Instance.GetDriver();
        }

        [Test]
        public void testMethod()
        {
            //BBCPage bbc = new BBCPage();
        }

        [TearDown]
        public void endTest()
        {
            Driver.Instance.stopBrowser();
        }
    }
}
