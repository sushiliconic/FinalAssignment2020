using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FinalAssignment2020;
namespace Assignment2020.UnitTests
{
    [TestClass]
    public class MethodTests
    {
        CommandParser parserObject;
        Form1 form;
        Dictionary<string, string> variableDictionary = new Dictionary<string, string>();
        public Dictionary<string, string> methodVariable = new Dictionary<string, string>();
        public Dictionary<string, string> methodDictionary = new Dictionary<string, string>();
        public Dictionary<string, string> parameterDictionary = new Dictionary<string, string>();
        public Dictionary<string, string> tempDictionary = new Dictionary<string, string>();
        [TestInitialize]
        public void Initialize()
        {
            parserObject = new CommandParser();
            form = new Form1();
            parserObject.lineNumberCount = 0;
            form.consoleTextBox.Text = "";

            parserObject.variableDictionary.Add("radius", "10");





        }


        [TestMethod]

        public void isMethodDeclaration_method_myMethod_radius_as_parameter_lineIsPassed_ReturnsTrue()

        {
            //Arrange



            string line = "method myMethod(radius)";
            //Act

            bool result = parserObject.isMethodDeclaration(line);

            //Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void runMethodDeclaration_method_myMethod_radius_as_parameter_lineIsPassed_ReturnsTrue()

        {
            //Arrange

            form.codeTextBox.Text = "method myMethod(radius)\ncircle(40)\nendmethod";
            string line = "method myMethod(radius)";

            parserObject.lineCount = 1;
            //Act

            bool result = parserObject.runMethodDeclaration(line, form);

            //Assert
            Assert.IsTrue(result);
        }
        [TestMethod]
        public void checkEndMethodDeclaration_method_myMethod_radius_as_parameter_lineIsPassed_ReturnsTrue()

        {
            //Arrange

            form.codeTextBox.Text = "method myMethod(radius)\ncircle(40)\nendmethod";
            string line = "method myMethod(radius)";

            parserObject.lineCount = 1;
            //Act

            bool result = parserObject.checkEndMethod(line, form);

            //Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void isMethodCall_myMethod_radius_as_parameter_lineIsPassed_ReturnsTrue()

        {
            //Arrange

            form.codeTextBox.Text = "method myMethod(radius)\ncircle(40)\nendmethod";
            string line = "myMethod(radius)";
            parserObject.methodDictionary.Add("myMethod", "1");
            parserObject.lineCount = 1;
            //Act

            bool result = parserObject.isMethodCall(line);

            //Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void runMethodCall_myMethod_radius_as_parameter_lineIsPassed_ReturnsTrue()

        {
            //Arrange

            form.codeTextBox.Text = "method mymethod(radius)\ncircle(40)\nendmethod myMethod(10)";
            string line = "mymethod(10)";
            parserObject.methodDictionary.Add("mymethod", "1");
            parserObject.methodVariable.Add("mymethod", "1");
            parserObject.parameterDictionary.Add("mymethod", "mymethod(radius)-2");
            parserObject.lineCount = 5;
            parserObject.onCallMethodName = "mymethod";
            //Act

            bool result = parserObject.runMethodCall(line, form);

            try
            {
                //Assert
                Assert.IsTrue(result);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(form.consoleTextBox.Text);
            }
        }





    }
}
