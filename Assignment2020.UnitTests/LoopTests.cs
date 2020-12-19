using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FinalAssignment2020;
namespace Assignment2020.UnitTests
{
    [TestClass]
    public class LoopTests
    {
        CommandParser parserObject;
        Form1 form;
        Dictionary<string, string> variableDictionary = new Dictionary<string, string>();

        [TestInitialize]
        public void Initialize()
        {
            parserObject = new CommandParser();
            form = new Form1();
            parserObject.lineNumberCount = 0;
            form.consoleTextBox.Text = "";
            Console.WriteLine(form.codeTextBox.Text);
            parserObject.variableDictionary.Add("radius", "10");
            parserObject.variableDictionary.Add("counter", "1");




        }


        [TestMethod]

        public void isLoop_loop_for_counter_lessthanor_equalsto_10_lineIsPassed_ReturnsTrue()

        {
            //Arrange



            string line = "loop for counter<=10";
            //Act

            bool result = parserObject.isLoop(line);

            //Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void checkEndLoop_loop_for_counter_lessthanor_equalsto_10_lineIsPassed_ReturnsTrue()

        {
            //Arrange

            form.codeTextBox.Text = "counter=10\nloop for counter<=10\ncircle(10)\nendloop";
            string line = "loop for counter<=10";

            parserObject.lineCount = 2;
            //Act

            bool result = parserObject.checkEndLoop(line, form);

            //Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void runLoop_loop_for_counter_lessthanor_equalsto_10_lineIsPassed_ReturnsTrue()

        {
            //Arrange
            form.codeTextBox.Text = "counter=10\nloop for counter<=10\ncircle(10)\ncounter+1\nendloop";
            parserObject.lineCount = 2;

            string line = "loop for counter<=10";
            //Act

            bool result = parserObject.runLoop(line, form);

            //Assert

            Assert.IsTrue(result);
        }



    }
}
