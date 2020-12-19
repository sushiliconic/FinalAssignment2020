using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FinalAssignment2020;
namespace Assignment2020.UnitTests
{
    [TestClass]
    public class CheckValidParamterTests
    {

        CommandParser parserObject;
        [TestInitialize]
        public void Initialize()
        {
            parserObject = new CommandParser();


        }


        [TestMethod]
        public void checkValidParameter_ValidCircleParameter_ReturnsTrue()

        {
            //Arrange

            parserObject.commandToExecute = "circle";

            string line = "circle(20)";
            //Act

            bool result = parserObject.checkValidParameter(line);

            //Assert
            Assert.IsTrue(result);
        }


        [TestMethod]
        public void checkValidParameter_ValidRectangleParameter_ReturnsTrue()

        {
            //Arrange


            parserObject.commandToExecute = "rectangle";
            string line = "rectangle(20,60)";
            //Act

            bool result = parserObject.checkValidParameter(line);

            //Assert
            Assert.IsTrue(result);
        }




        [TestMethod]
        public void checkValidParameter_ValidTriangleParameter_ReturnsTrue()

        {
            //Arrange


            parserObject.commandToExecute = "triangle";
            string line = "triangle(60)";
            //Act

            bool result = parserObject.checkValidParameter(line);

            //Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void checkValidParameter_ValidMoveToParameter_ReturnsTrue()

        {
            //Arrange


            parserObject.commandToExecute = "moveTo";
            string line = "moveTo(60,100)";
            //Act

            bool result = parserObject.checkValidParameter(line);

            //Assert
            Assert.IsTrue(result);
        }


        [TestMethod]
        public void checkValidParameter_ValidDrawToParameter_ReturnsTrue()

        {
            //Arrange


            parserObject.commandToExecute = "drawTo";
            string line = "drawTo(60,100)";
            //Act

            bool result = parserObject.checkValidParameter(line);

            //Assert
            Assert.IsTrue(result);
        }
    }
}
