using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FinalAssignment2020;
namespace Assignment2020.UnitTests
{
    [TestClass]
    public class VariableDeclarationTests
    {
        CommandParser parserObject;
        Form1 form;
        Dictionary<string, string> variableDictionary = new Dictionary<string, string>();

        [TestInitialize]
        public void Initialize()
        {
            parserObject = new CommandParser();
            form = new Form1();





        }


        [TestMethod]

        public void isVariable_formObjectandradius_equals_10_lineIsPassed_ReturnsTrue()

        {
            //Arrange



            string line = "radius=10";
            //Act

            bool result = parserObject.isVariable(line, form);

            //Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void checkVariable_formObjectandradius_equals_10_LineisPassed_ReturnsTrue()

        {
            //Arrange



            string line = "radius=10";
            //Act

            bool result = parserObject.checkVariable(line, form);

            //Assert
            Assert.IsTrue(result);
        }


        [TestMethod]
        public void runVariable_formObjectandradius_equals_10_LineisPassed_ReturnsTrue()

        {
            //Arrange



            string line = "radius=10";
            //Act

            bool result = parserObject.runVariable(form, line);

            //Assert
            Assert.IsTrue(result);
        }
    }
}
