using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FinalAssignment2020;
namespace Assignment2020.UnitTests
{
    [TestClass]
    public class ShapeCommandTest
    {
        CommandParser parserObject;
        Form1 form;
        [TestInitialize]
        public void Initialize()
        {
            parserObject = new CommandParser();
            form = new Form1();


            string[] arr = { "circle", "rectangle", "triangle" };
            string[] arr2 = { "moveTo", "drawTo" };
            parserObject.shapes = arr;
            parserObject.otherCommands = arr2;
        }


        [TestMethod]

        public void runShapeCommand_formObjectandCircle_10_lineIsPassed_ReturnsTrue()

        {
            //Arrange



            string line = "circle(10)";
            //Act

            bool result = parserObject.runShapeCommand(form, line);

            //Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void isShapeCommand_circle_10_LineisPassed_ReturnsTrue()

        {
            //Arrange



            string line = "circle(10)";
            //Act

            bool result = parserObject.isShapeCommand(line);

            //Assert
            Assert.IsTrue(result);
        }
    }
}
