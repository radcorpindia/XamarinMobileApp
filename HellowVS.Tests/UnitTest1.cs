using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClassLibrary.Core;

namespace HellowVS.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            User u = new User { Name = "Test Shareed Class" };

            Assert.IsNotNull(u, "true");

        }

        [TestMethod]
        public void TestMethod2()
        {
            User u = new User { Name = "Test Shareed Class" };

            Assert.IsNull(u, "false");

        }
    }
}
