using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FinalAssignment2020;
namespace Assignment2020.UnitTests
{
    [TestClass]
    public class CheckValidLineTests
    {
        [TestMethod]
        public void CheckValidLineTestMethod_ValidLineIsPassed_ReturnsTrue()
        {
            //arrange
            CommandParser parser = new CommandParser();
            string line = "circle(10)";
            //act
            bool result = parser.checkValidLine(line);
            //assert
            Assert.IsTrue(result);
        }
    }
}
