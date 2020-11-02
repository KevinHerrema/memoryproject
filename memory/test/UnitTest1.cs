using System;
using memory;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Test_randomnr()
        {
            MainWindow mw = new MainWindow();

            int[] lijst1;
            int[] lijst2;

            lijst1 = mw.randomnr();
            lijst2 = mw.randomnr();

            Assert.AreNotEqual(lijst1, lijst2);
        }
    }
}
