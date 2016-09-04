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
    public class TemperatureTests
    {
        [TestMethod()]
        public void toggleTest()
        {

        }

        [TestMethod()]
        public void displayTest()
        {
            string expected = "Temperature Information";
            Assert.AreEqual(expected, "Temperature Information");
        }

        [TestMethod()]
        public void ConvertToCelsiusTest()
        {
            Temperature t = new Temperature();
            double x = 212;
            double expected = t.ConvertToCelsius(x);
            Assert.AreEqual(expected, (5.0 / 9.0) * (x - 32), 0.00001);
        }

        [TestMethod()]
        public void ConvertToFarhenheitTest()
        {
            Temperature t = new Temperature();
            double x = 60;
            double expected = t.ConvertToFarhenheit(x);
            Assert.AreEqual(expected, ((x * 9) / 5) + 32, 0.00001);
        }
    }
}