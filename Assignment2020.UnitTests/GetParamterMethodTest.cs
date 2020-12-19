using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FinalAssignment2020;
namespace Assignment2020.UnitTests
{
    [TestClass]
    public class GetParamterMethodTest
    {
        [TestMethod]
        public void CheckParametersReturnOutput_Rectanlge_20_30__Array_20_30()
        {

            //Arrange

            string[] expectedOutput = { "20", "30" };

            string line = "rectangle(20,30)";
            //Act

            CommandParser parser = new CommandParser();
            string[] actualOutput = parser.getParamter(line);

            //Assert
            CollectionAssert.AreEqual(expectedOutput, actualOutput);
        }



    }
}
