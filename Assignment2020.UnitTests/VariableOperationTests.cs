using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FinalAssignment2020;
namespace Assignment2020.UnitTests
{
    [TestClass]
    public class VariableOperationTests
    {

        CommandParser parserObject;
        Form1 form;
        Dictionary<string, string> variableDictionary = new Dictionary<string, string>();

        [TestInitialize]
        public void Initialize()
        {
            parserObject = new CommandParser();
            form = new Form1();

            form.consoleTextBox.Text = "";
            Console.WriteLine(form.codeTextBox.Text);
            parserObject.variableDictionary.Add("radius", "10");




        }
        [TestMethod]

        public void isVariableOperationRadius_plus_1_lineIsPassed_ReturnsTrue()

        {
            //Arrange



            string line = "radius+1";
            //Act

            bool result = parserObject.isVariableOperation(line);

            //Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void runVariableOperation_Radius_plus_1_lineIsPassed_ReturnsTrue_ReturnsTruee()

        {
            //Arrange

            string line = "radius+1";



            //Act

            bool result = parserObject.runVariableOperation(line, form);

            //Assert
            Assert.IsTrue(result);
        }
    }
}
