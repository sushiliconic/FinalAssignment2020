using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FinalAssignment2020;
namespace Assignment2020.UnitTests
{
    [TestClass]
    public class IfEndIfTests
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

        public void isIfClause_fomObject_and_If_radius_equals_10_lineIsPassed_ReturnsTrue()

        {
            //Arrange



            string line = "if(radius=10)";
            //Act

            bool result = parserObject.isIfClause(line, form);

            //Assert
            Assert.IsTrue(result);
        }
        [TestMethod]

        public void checkIfClause_fomObject_and_If_radius_equals_10_lineIsPassed_ReturnsTrue()

        {
            //Arrange



            string line = "if(radius=10)";
            //Act

            bool result = parserObject.checkIfClause(line, form);

            //Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void checkEndIfClause_fomObject_and_If_radius_equals_10_lineIsPassed_ReturnsTruee()

        {
            //Arrange

            form.codeTextBox.Text = "radius=10\nif(radius = 10)\ncircle(10)\nendif";


            parserObject.lineCount = 2;
            //Act

            bool result = parserObject.checkEndIfClause(form);

            //Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void checkThenClause_fomObject_and_If_radius_equals_10_lineIsPassed_ReturnsTruee()

        {
            //Arrange
            form.codeTextBox.Text = "radius=10\nif(radius = 10)\nthen\ncircle(10)";
            parserObject.lineCount = 2;


            //Act

            bool result = parserObject.checkThenClause(form);

            //Assert
            Assert.IsTrue(result);
        }


        [TestMethod]
        public void runIfClause_formObjectandradius_equals_10_LineisPassed_ReturnsTrue()

        {
            //Arrange

            form.codeTextBox.Text = "radius=10\nif(radius = 10)\ncircle(10)\nendif";
            parserObject.lineCount = 2;
            string line = "if(radius = 10)";
            parserObject.conditionOperator = "";
            parserObject.ifStatementCounter = 0;
            //Act

            bool result = parserObject.runIfClause(line, form);

            //Assert
            Assert.IsTrue(result);
        }
    }
}
