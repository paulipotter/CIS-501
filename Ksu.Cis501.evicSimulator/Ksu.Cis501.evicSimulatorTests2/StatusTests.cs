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
    public class StatusTests
    {
        [TestMethod()]
        public void toggleTest()
        {

        }

        [TestMethod()]
        public void displayTest()
        {

        }

        [TestMethod()]
        public void ResetTest()
        {
            Status._isMetric = true;
            Assert.AreEqual(Status._isMetric, true);
            string expected = "Next oil change in 4828.032 km";
            Assert.AreEqual(expected, "Next oil change in 4828.032 km");

            expected = "Next oil change in 3000 mi";
            Assert.AreEqual(expected, "Next oil change in 3000 mi");

        }

        [TestMethod()]
        public void IncrementTest()
        {
            
        }
    }
}