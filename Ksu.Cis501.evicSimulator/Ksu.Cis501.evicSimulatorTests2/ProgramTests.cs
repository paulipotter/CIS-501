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
    public class ProgramTests
    {
        [TestMethod()]
        public void MainTest()
        {
            string expected = "Which mode would you like to enter?";
            Assert.AreEqual(expected, "Which mode would you like to enter?");

            expected = "1) Simulation";
            Assert.AreEqual(expected, "1) Simulation");

            expected = "2) Regular Run";
            Assert.AreEqual(expected, "2) Regular Run");

            //inside if 
            expected = "You chose the Regular Run. Use the <- -> arrows to navigate through the Main Menu";
            Assert.AreEqual(expected, "You chose the Regular Run. Use the <- -> arrows to navigate through the Main Menu");
        }

        [TestMethod()]
        public void MainMenuTest()
        {
            ConsoleKey k;
            k = ConsoleKey.LeftArrow;

            k = ConsoleKey.RightArrow;
        }

        [TestMethod()]
        public void BarSpaceTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void MoveUpDownTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void MoveLeftTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void MoveRightTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void SimulatorTest()
        {
            string expected = "1) System Status";
            Assert.AreEqual(expected, "1) System Status");

            expected = "2) Warning Messages";
            Assert.AreEqual(expected, "2) Warning Messages");

            expected = "3) Temperature";
            Assert.AreEqual(expected, "3) Temperature");
        }

        [TestMethod()]
        public void option1Test()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void option2Test()
        {
            string expected = "Toggle Warning Messages";
            Assert.AreEqual(expected, "Toggle Warning Messages");

            expected = "a) Door ajar";
            Assert.AreEqual(expected, ("a) Door ajar"));

            expected = "b) Change Engine Soon";
            Assert.AreEqual(expected, "Change Engine Soon");

            expected = "c) Oil Change";
            Assert.AreEqual(expected, "c) Oil Change");
        }

        [TestMethod()]
        public void option3Test()
        {
            string expected = "a) Outside Temperature";
            Assert.AreEqual(expected, "a) Outside Temperature");

            expected = "Please enter a value in Fahrenheit for the outside temperature:";
            Assert.AreEqual(expected, "Please enter a value in Fahrenheit for the outside temperature:");


            expected = "Please enter a value in Fahrenheit for the inside temperature:";
            Assert.AreEqual(expected, "Please enter a value in Fahrenheit for the Ouside temperature:");
        }
    }
}