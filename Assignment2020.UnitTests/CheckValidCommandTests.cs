using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FinalAssignment2020;
namespace Assignment2020.UnitTests
{
    [TestClass]
    public class checkValidCommandTests
    {

        CommandParser parserObject;
        [TestInitialize]
        public void Initialize()
        {
            parserObject = new CommandParser();
            string[] arr = { "circle", "rectangle", "triangle" };
            string[] arr2 = { "moveTo", "drawTo" };
            parserObject.shapes = arr;
            parserObject.otherCommands = arr2;
        }


        [TestMethod]

        public void checkValidParameter_circleCommandIsPassed_ReturnsTrue()

        {
            //Arrange



            string line = "circle(20)";
            //Act

            bool result = parserObject.checkValidShape(line);

            //Assert
            Assert.IsTrue(result);
        }


        [TestMethod]
        public void checkValidParameter_rectangleCommandIsPassed_ReturnsTrue()

        {
            //Arrange

            string line = "rectangle(20,30)";


            //Act

            bool result = parserObject.checkValidShape(line);

            //Assert
            Assert.IsTrue(result);
        }


        [TestMethod]
        public void checkValidParameter_triangleCommandIsPassed_ReturnsTrue()

        {
            //Arrange



            string line = "triangle(20)";
            //Act

            bool result = parserObject.checkValidShape(line);

            //Assert
            Assert.IsTrue(result);
        }


        [TestMethod]
        public void checkValidParameter_moveToCommandIsPassed_ReturnsTrue()

        {
            //Arrange



            string line = "moveTo(20,11)";
            //Act

            bool result = parserObject.checkValidOtherCommand(line);

            //Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void checkValidParameter_drawToCommandIsPassed_ReturnsTrue()

        {
            //Arrange



            string line = "drawTo(20,11)";
            //Act

            bool result = parserObject.checkValidOtherCommand(line);

            //Assert
            Assert.IsTrue(result);
        }

    }
}
