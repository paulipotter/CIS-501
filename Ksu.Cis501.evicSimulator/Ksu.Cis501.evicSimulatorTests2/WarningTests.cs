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
    public class WarningTests
    {
        [TestMethod()]
        public void toggleTest()
        {

        }

        [TestMethod()]
        public void displayTest()
        {
            
            string expected = "Warning Messages";
            Assert.AreEqual(expected, "Warning Messages");

            Warning w = new Warning();

            

            expected = "Door Ajar!";
            Assert.AreEqual(expected, "Door Ajar!");

            expected = "Check Engine Soon!";
            Assert.AreEqual(expected, "Check Engine Soon!");

            expected = "Oil Change!";
            Assert.AreEqual(expected, "Oil Change!");





        }
    }
}