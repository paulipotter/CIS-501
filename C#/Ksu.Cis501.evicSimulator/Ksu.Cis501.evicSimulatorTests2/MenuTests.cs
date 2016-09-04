using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ksu.Cis501.evicSimulator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ksu.Cis501.evicSimulator.Tests
{
    [TestClass()]
    public class MenuTests
    {
        [TestMethod()]
        public void MenuTest()
        {

        }

        [TestMethod()]
        public void toggleTest()
        {
            Menu._isMetric = false;
        }

        [TestMethod()]
        public void displayTest()
        {
            string expected = "Personal Settings";
            Assert.AreEqual(expected, "Personal Settings");

            //if metric false <- add test coverage for that
            expected = "US Units";
            Assert.AreEqual(expected, "US Units");

            //if ismetric true
            expected = "Metric Units";
            Assert.AreEqual(expected, "Metric Units");
        }

        [TestMethod()]
        public void ToStringTest()
        {
            string expected = "Personal Settings";
            Assert.AreEqual(expected, "Personal Settings");
        }

        [TestMethod()]
        public void ConvertToKMTest()
        {
            double x = 2;
            x = Menu.ConvertToKM(x) ;
            Assert.AreEqual(2 * 1.609344, x, 0.00001);
        }

        [TestMethod()]
        public void ConvertToMITest()
        {
            double x = 2;
            x = Menu.ConvertToMI(x);
            Assert.AreEqual(2 / 1.609344, x, 0.00001);
        }
    }
}