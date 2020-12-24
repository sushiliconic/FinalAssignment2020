using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FinalAssignment2020
{
    /// <summary>
    /// Class method executes the each line of the text from code TextBox
    /// </summary>
    public class CommandParser
    {
        //shapeFactory clas object
        ShapeFactory shapeFactory = new ShapeFactory();

        bool validShape = false;
        bool validOtherCommand = false;
        bool validLine = false;
        bool validParameterCheck = false;

        /// <summary>
        /// stores the name of the called shape
        /// </summary>
        public string finalShape = "";
        /// <summary>
        /// stores the name of the called command
        /// </summary>
        public string finalotherCommand = "";
        /// <summary>
        /// command that is being executed
        /// </summary>
        public string commandToExecute = "";
        /// <summary>
        /// lineNumber of each forEachLoop line
        /// </summary>
        public int lineCount = 1;
        /// <summary>
        /// counts the number of statements inside if endif
        /// </summary>
        public int ifStatementCounter = 0;
        /// <summary>
        /// stores color
        /// </summary>
        public Color c = Color.Black;
        /// <summary>
        /// number of elements inside loop
        /// </summary>
        public int loopCounter = 0;
        /// <summary>
        /// variable of if condition
        /// </summary>
        public string conditionKey = "";
        /// <summary>
        /// value of variable inside if condition
        /// </summary>
        public string conditionValue = "";
        /// <summary>
        /// value of dictionary variable
        /// </summary>
        public string variableValue = "";
        /// <summary>
        /// opertor of if condition
        /// </summary>
        public string conditionOperator = "";
        /// <summary>
        /// method name that is being called
        /// </summary>
        public string onCallMethodName = "";
        //initial coordinates
        /// <summary>
        /// the x coordinate
        /// </summary>
        public int xCoordinate = 0;
        /// <summary>
        /// the y coordinate
        /// </summary>
        public int yCoordinate = 0;
        /// <summary>
        /// counts  the number of statement inside loop
        /// </summary>
        public int lineNumberCount = 0;
        /// <summary>
        /// counts the number of elements inside method
        /// </summary>
        public int methodStatementCounter = 0;

        //shapes array
        /// <summary>
        /// stores the shape 
        /// </summary>
        public string[] shapes = { "circle", "rectangle", "triangle", "polygon" };
        //other commands array
        //stores other commands
        /// <summary>
        /// array of other commands
        /// </summary>
        public string[] otherCommands = { "moveto", "drawto", "pen" };

        /// <summary>
        /// stores  variable and its value
        /// </summary>
        public Dictionary<string, string> variableDictionary = new Dictionary<string, string>();
        /// <summary>
        /// stores method and its line number
        /// </summary>
        public Dictionary<string, string> methodVariable = new Dictionary<string, string>();
        /// <summary>
        /// stores method and it line number
        /// </summary>
        public Dictionary<string, string> methodDictionary = new Dictionary<string, string>();
        /// <summary>
        /// stores methoname and its parameters
        /// </summary>
        public Dictionary<string, string> parameterDictionary = new Dictionary<string, string>();
        /// <summary>
        /// temp dictionary for variableDictionary
        /// </summary>
        public Dictionary<string, string> tempDictionary = new Dictionary<string, string>();

        /// <summary>
        /// Graphics object
        /// </summary>
        public Graphics g;
        //constructor explicit
        /// <summary>
        /// constructure
        /// </summary>
        public CommandParser()
        {

        }
        /// <summary>
        /// Explicit overloaded constructor
        ///Takes form as object reference and passes it into executecommand method
        /// </summary>
        /// <param name="form"> Object Reference of the form class by runButton_Click Event</param>
        public CommandParser(Form1 form)
        {

            executeCommand(form);
        }


        /// <summary>
        /// Takes code textbox and executes them line by line
        /// </summary>
        /// <param name="form"> object raference of form thorwn by runButton_click Event</param>
        public void executeCommand(Form1 form)
        {

            //refresh the console and panel on every execution
            form.consoleTextBox.Clear();
            form.drawingBoard.Refresh();


            string txt = form.codeTextBox.Text.Trim();
            string[] lines = txt.Split(new Char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);


            foreach (string line in lines)
            {

                if (line.Contains("fill"))
                {
                    string option = line.Split('(', ')')[1];
                    if (option.Equals("on"))
                    {
                        form.fill.Checked = true;
                    }
                    else if (option.Equals("off"))
                    {
                        form.fill.Checked = false;
                    }
                    continue;
                }

                bool result = caseExecution(line, form);

                if (!result)
                    break;



                lineCount++;
            }
        }


        /// <summary>
        /// This method checks if the given string is either a shapeCommand, if endif, method ,loop, variable operation or a method call and shows error if it is none of the cases.
        /// </summary>
        /// <param name="line">Line is a single line in a forEach loop</param>
        /// <param name="form">This is the object reference of form passed by runButton_click Event</param>
        /// <returns>true if the line is succesfully executed</returns>
        public bool caseExecution(string line, Form1 form)
        {
            line = line.ToLower().Trim();

            //for if end statement block
            if (ifStatementCounter != 0)
            {
                ifStatementCounter--;

                return true;
            }

            // for method block
            if (methodStatementCounter != 0)
            {
                methodStatementCounter--;

                return true;
            }

            //for loop block
            if (lineNumberCount != 0)
            {
                Console.WriteLine("lineNumber bhitra gayo");
                lineNumberCount--;

                return true;
            }
            //checks for method declaration
            if (isMethodDeclaration(line))
            {
                if (!runMethodDeclaration(line, form))
                {
                    return false;
                }
                //checks for shape
            }
            else if (isShapeCommand(line))
            {
                if (!runShapeCommand(form, line))
                {
                    return false;

                }



            }
            //checks for valid variable declaration
            else if (isVariable(line, form))
            {
                if (!runVariable(form, line))
                {
                    form.compilationFailed();
                    return false;

                }


            }
            //checks for valid if endif
            else if (isIfClause(line, form))
            {
                if (!runIfClause(line, form))
                {
                    form.compilationFailed();
                    return false;
                }




            }

            else if (line.Contains("endif"))
            {
                Console.WriteLine("endif");
            }
            //for variable operation
            else if (isVariableOperation(line))
            {
                if (!runVariableOperation(line, form))
                {
                    form.compilationFailed();
                    return false;
                }
            }
            //check for loop
            else if (isLoop(line))
            {
                if (!runLoop(line, form))
                {

                    return false;
                }
            }
            else if (line.Contains("endloop"))
            {
                Console.WriteLine("endloop");
                //check for method call
            }
            else if (isMethodCall(line))
            {
                if (!runMethodCall(line, form))
                {

                    return false;
                }

            }


            else
            {
                form.compilationFailed();
                form.consoleTextBox.Text = "Sytnax error on line" + lineCount++;
                return false;

            }

            return true;
        }

        /// <summary>
        /// This method is called when a method is called. It checks for valid method and parameters and runs the method.
        /// </summary>
        /// <param name="line">Line is a single line in a forEach loop</param>
        /// <param name="form">This is the object reference of form passed by runButton_click Event</param>
        /// <returns>boolean result.True if the method and variable are valid</returns>
        public bool runMethodCall(string line, Form1 form)
        {
            try
            {

                string[] lineCheck = line.Split(new char[] { ')' }, StringSplitOptions.RemoveEmptyEntries);
                if (lineCheck.Length >= 2)
                {
                    throw new Exception();
                }

                string[] parameters = getParamter(line);
                bool comma = false;
                int methodCallParameterCount = 0;
                if (line.Contains(","))
                {
                    comma = true;
                }

                foreach (string parameter in parameters)
                {

                    if (String.IsNullOrEmpty(parameter))
                    {
                        if (comma)
                        {
                            form.compilationFailed();
                            form.consoleTextBox.Text = "Invalid Parameters on line " + lineCount;
                            return false;
                        }
                    }

                }
                methodCallParameterCount = parameterCount(line);
                string[] methodSignature = parameterDictionary[onCallMethodName].Split(new char[] { '-' }, 2, StringSplitOptions.RemoveEmptyEntries);
                int actualMethodParameterCount = Int32.Parse(methodSignature[1]);

                if (methodCallParameterCount != actualMethodParameterCount)
                {
                    form.compilationFailed();
                    form.consoleTextBox.Text = " Method with similar parameter list doesn't exist on line " + lineCount;
                    return false;
                }


                string[] methodParameter = getParamter(parameterDictionary[onCallMethodName]);

                int i = 0;
                foreach (string param in parameters)
                {
                    if (String.IsNullOrEmpty(param))
                        continue;



                    bool isNumeric = int.TryParse(param, out _);
                    if (!isNumeric)
                    {
                        if (!variableDictionary.ContainsKey(param))
                        {
                            form.compilationFailed();
                            form.consoleTextBox.Text = "Variable doesn't exist and cannot be used as parameter on line " + lineCount;
                            return false;
                        }
                        methodVariable[methodParameter[i]] = variableDictionary[param];
                    }
                    else
                    {
                        methodVariable[methodParameter[i]] = param;
                    }

                    i++;
                }

                /*     Console.WriteLine("Methodvariable dict start");
                     foreach(KeyValuePair<string,string>  kvp in methodVariable)
                     {
                         Console.WriteLine(kvp.Key + " = "+ kvp.Value);
                     }
                     Console.WriteLine("Methodvariable dict end");
                     tempDictionary = variableDictionary;

                     Console.WriteLine("temp dict start");
                     foreach (KeyValuePair<string, string> kvp in tempDictionary)
                     {
                         Console.WriteLine(kvp.Key + " = " + kvp.Value);
                     }
                     Console.WriteLine("temp dict end");*/

                foreach (KeyValuePair<string, string> kvp in methodVariable)
                {
                    if (variableDictionary.ContainsKey(kvp.Key))
                    {
                        /*tempDictionary.Add(kvp.Key, variableDictionary[kvp.Key]);*/
                        variableDictionary[kvp.Key] = kvp.Value;

                    }
                    else
                    {
                        variableDictionary.Add(kvp.Key, kvp.Value);
                    }
                }



                int lineNumber = Int32.Parse(methodDictionary[onCallMethodName]);
                Console.WriteLine("line numer is: " + lineNumber);
                int templineCount = lineCount;
                lineCount = lineNumber + 1;

                string txt = form.codeTextBox.Text.Trim();
                string[] lines = txt.Split(new Char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);

                for (int k = lineNumber; k < lines.Length; k++)
                {
                    Console.WriteLine("I am here");
                    lines[k] = lines[k].ToLower();
                    if (lines[k] == "endmethod")
                    {
                        break;
                    }
                    else
                    {

                        if (!caseExecution(lines[k], form))
                            return false;
                        lineCount++;
                    }
                }

                lineCount = templineCount;
                variableDictionary = tempDictionary;
                /*
                                    bool isNumeric = int.TryParse(parameter, out _);

                                    if (!isNumeric)
                                    {

                                    }

                                    if (isNumeric)
                                    {
                                        form.compilationFailed();
                                        form.consoleTextBox.Text = "Parameter cannot be numeric on line " + lineCount;
                                        return false;
                                    }
                                    if (methodVariable.ContainsKey(parameter))
                                    {
                                        methodVariable[parameter] = "0";
                                    }
                                    else
                                    {
                                        methodVariable.Add(parameter, "0");
                                    }
                                    Console.WriteLine(parameter);*/




                return true;
            }
            catch (Exception)
            {
                form.compilationFailed();
                form.consoleTextBox.Text = "Invalid method call on line " + lineCount;
                return false;
            }
        }


        /// <summary>
        /// checks if the line is valid and the call is valid method
        /// </summary>
        /// <param name="line">Line is a single line in a forEach loop</param>
        /// <returns> true if the method name in valid</returns>
        public bool isMethodCall(string line)
        {
            string[] method = line.Split(new char[] { '(' }, 2, StringSplitOptions.RemoveEmptyEntries);

            if (methodDictionary.ContainsKey(method[0]))
            {
                //stores the method name 
                onCallMethodName = method[0];
                return true;
            }
            return false;
        }

        /// <summary>
        /// checks if the line starts with the string "method".
        /// </summary>
        /// <param name="line">Line is a single line in a forEach loop</param>

        /// <returns>true if the line starts with method</returns>
        public bool isMethodDeclaration(string line)
        {
            line = line.ToLower().Trim();
            if (line.StartsWith("method"))
                return true;

            return false;
        }

        /// <summary>
        /// This method validates for method and store the parameters and method names in respective dictionary.
        /// </summary>
        /// <param name="line">Line is a single line in a forEach loop</param>
        /// <param name="form">This is the object reference of form passed by runButton_click Event</param>
        /// <returns>true if the method syntax is correct and the method name and paramters are successfully stored</returns>
        public bool runMethodDeclaration(string line, Form1 form)
        {
            string methodName = "";
            try
            {
                string[] methodSignature = line.Split(new char[] { ' ' }, 2, StringSplitOptions.RemoveEmptyEntries);
                if (methodSignature.Length != 2)
                {
                    throw new Exception();
                }

                string[] lineCheck = line.Split(new char[] { ')' }, StringSplitOptions.RemoveEmptyEntries);
                if (lineCheck.Length >= 2)
                {
                    throw new Exception();
                }


                string[] method = methodSignature[1].Split(new char[] { '(' }, 2, StringSplitOptions.RemoveEmptyEntries);
                methodName = method[0];


                string[] parameters = { };
                bool comma = false;
                /* bool paramEmpty = false;*/
                int parameterCount = 0;

                try
                {
                    parameters = getParamter(methodSignature[1]);
                }
                catch (Exception )
                {
                    form.consoleTextBox.Text = "empty param";

                    return false;
                }


                if (methodSignature[1].Contains(","))
                {
                    comma = true;
                }

                foreach (string parameter in parameters)
                {
                    parameterCount++;
                    if (String.IsNullOrEmpty(parameter))
                    {
                        if (comma)
                        {
                            form.compilationFailed();
                            form.consoleTextBox.Text = "Invalid Parameters on line " + lineCount;
                            return false;
                        }
                    }

                    bool isNumeric = int.TryParse(parameter, out _);
                    if (isNumeric)
                    {
                        form.compilationFailed();
                        form.consoleTextBox.Text = "Parameter cannot be numeric on line " + lineCount;
                        return false;
                    }
                    if (methodVariable.ContainsKey(parameter))
                    {
                        methodVariable[parameter] = "0";
                    }
                    else
                    {
                        methodVariable.Add(parameter, "0");
                    }
                    Console.WriteLine(parameter);



                }

                if (!checkEndMethod(line, form))
                    return false;

                try
                {
                    methodDictionary.Add(methodName, lineCount.ToString());

                }
                catch (Exception )
                {
                    form.compilationFailed();
                    form.consoleTextBox.Text = "Method already exits. Error on line" + lineCount;
                    return false;
                }

                parameterDictionary.Add(methodName, methodSignature[1] + "-" + parameterCount.ToString());



            }
            catch (Exception )
            {
                form.compilationFailed();
                form.consoleTextBox.Text = "Invalid Method Declaration on line " + lineCount;
                return false;
            }

            return true;
        }

        /*public void checkMethodStatementNumber(String line,Form1 form)
        {
            string txt = form.codeTextBox.Text.Trim();
            string[] lines = txt.Split(new Char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);

            for (int i = lineCount; i < lines.Length; i++)
            {
                if ((lines[i] == "endmethod"))
                {

                }


                }
        }*/

        /// <summary>
        /// Checks if the method is closed or not.
        /// </summary>

        /// <param name="line">Line is a single line in a forEach loop</param>
        /// <param name="form">This is the object reference of form passed by runButton_click Event</param>
        /// <returns>true if the end method is present</returns>
        public bool checkEndMethod(string line, Form1 form)
        {
            string txt = form.codeTextBox.Text.Trim();
            string[] lines = txt.Split(new Char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
            bool endMethodExistence = false;

            for (int i = lineCount; i < lines.Length; i++)
            {
                lines[i] = lines[i].ToLower().Trim();
                if ((lines[i] == "endmethod"))
                {
                    methodStatementCounter += 1;
                    endMethodExistence = true;
                    break;
                }
                else
                {
                    methodStatementCounter++;
                }

            }

            if (!endMethodExistence)
            {
                form.compilationFailed();
                form.consoleTextBox.Text = "Method not closed on line" + lineCount;
                return false;
            }



            for (int i = lineCount; i < lines.Length; i++)
            {
                lines[i] = lines[i].ToLower();
                if (lines[i].StartsWith("method"))
                {
                    form.compilationFailed();
                    form.consoleTextBox.Text = "Method not closed on line" + lineCount;
                    return false;
                }
                if (lines[i] == "endmethod")
                {
                    break;
                }
            }


            return true;
        }

        /// <summary>
        /// checks if the line start with loop
        /// </summary>
        /// <param name="line">Line is a single line in a forEach loop</param>

        /// <returns>true if the line starts with loop</returns>
        public bool isLoop(String line)
        {
            line = line.ToLower();
            if (line.StartsWith("loop"))
                return true;

            return false;
        }

        /// <summary>
        /// validates loop syntax, checks for condition and executes the statements according to the conditon.
        /// </summary>
        /// <param name="line">Line is a single line in a forEach loop</param>
        /// <param name="form">This is the object reference of form passed by runButton_click Event</param>
        /// <returns>returns true if there isn't any loop error in condition and validation.</returns>
        public bool runLoop(String line, Form1 form)
        {

            string[] loopCondition = { };
            try
            {
                string[] loopVariable = line.Split(new string[] { "for" }, StringSplitOptions.RemoveEmptyEntries);
                if (loopVariable.Length != 2)
                {
                    form.consoleTextBox.Text = "Invalid Loop Statement on line" + lineCount;
                    return false;
                }


                loopCondition = loopVariable[1].Split(new string[] { "<=", ">=" }, StringSplitOptions.RemoveEmptyEntries);

                if (loopCondition.Length == 1)
                {
                    form.compilationFailed();
                    form.consoleTextBox.Text = "Invalid Loop Statement on line" + lineCount + "\n operator should be <= or =>";
                    return false;
                }

                foreach (string loop in loopCondition)
                {
                    Console.WriteLine();
                }

                if (loopCondition.Length != 2)
                {
                    form.compilationFailed();
                    form.consoleTextBox.Text = "Invalid Loop Statement on line" + lineCount;
                    return false;


                }

                string loopKey = loopCondition[0].ToLower().Trim();
                string loopValueString = loopCondition[1].Trim();
                int loopValue = Int32.Parse(loopValueString);

                if (!variableDictionary.ContainsKey(loopKey))
                {
                    form.compilationFailed();
                    form.consoleTextBox.Text = "Invalid Loop Statement: Variable doesn't exist on line" + lineCount;
                    return false;
                }
                if (!checkEndLoop(line, form))
                {
                    return false;
                }

                string txt = form.codeTextBox.Text.Trim();
                string[] lines = txt.Split(new Char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
                bool counterCheck = false;
                int counterLine = 0;
                int lineNumberCount1 = 0;

                List<string> loopList = new List<string>();
                for (int i = lineCount; i < lines.Length; i++)
                {
                    if (lines[i].Contains(loopKey))
                    {
                        counterCheck = true;
                    }
                    if (lines[i] == "endloop")
                    {

                        break;
                    }
                    else
                    {
                        //added statment to the list
                        lineNumberCount1++;
                        loopList.Add(lines[i]);
                    }
                }

                if (!counterCheck)
                {
                    form.compilationFailed();
                    form.consoleTextBox.Text = "Counter increment is not handled on line " + lineCount;
                    return false;
                }

                int dictValue = 0;
                string loopOperator = "";
                counterLine = lineCount;

                if (line.Contains("<="))
                {
                    loopOperator = "<=";
                }
                else
                {
                    loopOperator = ">=";
                }


                if (loopOperator == "<=")
                {
                    while (dictValue <= loopValue)
                    {
                        lineCount = counterLine;
                        foreach (string list in loopList)
                        {
                            lineCount++;
                            if (!caseExecution(list, form))
                                return false;
                        }
                        dictValue = Int32.Parse(variableDictionary[loopKey]);
                    }
                }
                else
                {

                    while (dictValue >= loopValue)
                    {
                        lineCount = counterLine;
                        foreach (string list in loopList)
                        {
                            lineCount++;
                            if (!caseExecution(list, form))
                                return false;
                        }
                        dictValue = Int32.Parse(variableDictionary[loopKey]);
                    }
                }


                lineNumberCount = lineNumberCount1;
                lineCount = counterLine;
            }
            catch (Exception )
            {
                form.compilationFailed();
                form.consoleTextBox.Text = "Invalid Loop Statement on line" + lineCount;
                return false;
            }
            return true;
        }

        /// <summary>
        /// Checks if the line contains endloop or not
        /// </summary>
        /// <param name="line">Line is a single line in a forEach loop</param>
        /// <param name="form">This is the object reference of form passed by runButton_click Event</param>
        /// <returns>true if the line contains endloop</returns>
        public bool checkEndLoop(String line, Form1 form)
        {

            string txt = form.codeTextBox.Text.Trim();
            string[] lines = txt.Split(new Char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
            bool endloopExistence = false;

            for (int i = lineCount; i < lines.Length; i++)
            {
                lines[i] = lines[i].ToLower().Trim();
                if ((lines[i] == "endloop"))
                    endloopExistence = true;
            }

            if (!endloopExistence)
            {
                form.consoleTextBox.Text = " loop not closed on line " + lineCount;
                return false;
            }



            for (int i = lineCount; i < lines.Length; i++)
            {
                if (lines[i].StartsWith("loop"))
                {
                    form.compilationFailed();
                    form.consoleTextBox.Text = "loop not closed on line" + lineCount;
                    return false;
                }
                if (lines[i] == "endloop")
                {
                    break;
                }
            }

            return true;
        }

        /// <summary>
        /// Checks if the line is variable operation or not.
        /// </summary>
        /// <param name="line">Line is a single line in a forEach loop</param>
        /// <returns>true if the line contains + - or *</returns>
        public bool isVariableOperation(string line)
        {
            if (line.Contains("+") || line.Contains("-") || line.Contains("*"))
            {
                return true;
            }
            return false;
        }


        /// <summary>
        /// Checks for valid variable and updates the variable in the dictionary accordingly
        /// </summary>
        /// <param name="line">Line is a single line in a forEach loop</param>
        /// <param name="form">This is the object reference of form passed by runButton_click Event</param>
        /// <returns>true if the varaible is valid and stored succesfully</returns>
        public bool runVariableOperation(string line, Form1 form)
        {
            try
            {
                //splits by + = or -
                string[] variableEntity = line.Split(new Char[] { '+', '-', '*' }, StringSplitOptions.RemoveEmptyEntries);

                if (variableEntity.Length != 2)
                {


                    form.consoleTextBox.Text = "Invalid variable operation on line " + lineCount;
                    return false;
                }

                if (line.Contains("+") && line.Contains("-") || line.Contains("+") && line.Contains("*") || line.Contains("*") && line.Contains("-") || line.Contains("+") && line.Contains("-") && line.Contains("*"))
                {
                    form.consoleTextBox.Text = "Invalid vairable operation on line " + lineCount;
                    return false;
                }
                string vrKey = variableEntity[0].ToLower().Trim();
                string vrValue = variableEntity[1].Trim();
                int vrValuenum = Int32.Parse(vrValue);
                int dictValue = 0;
                bool matchStatus = false;

                foreach (KeyValuePair<string, string> kvp in variableDictionary)
                {
                    if (vrKey == kvp.Key)
                    {

                        dictValue = Int32.Parse(kvp.Value);
                        matchStatus = true;
                        if (line.Contains("+"))
                        {
                            variableDictionary[kvp.Key] = (dictValue + vrValuenum).ToString();
                        }
                        else if (line.Contains("-"))
                        {
                            variableDictionary[kvp.Key] = (dictValue - vrValuenum).ToString();
                        }
                        else if (line.Contains("*"))
                        {
                            variableDictionary[kvp.Key] = (dictValue * vrValuenum).ToString();
                        }

                        break;
                    }

                }

                if (matchStatus == false)
                {
                    form.consoleTextBox.Text = "Invalid Operation: variable doesn't exist on line" + lineCount;
                    return false;
                }

                return true;
            }
            catch (Exception )
            {

                form.consoleTextBox.Text = "Invalid variable operation on line " + lineCount;
                return false;
            }
        }

        /// <summary>
        /// Checks if the condition is correct and the variable is correct and if the condtion matches and runs the statements iniside it accordingly
        /// </summary>
        /// <param name="line">Line is a single line in a forEach loop</param>
        /// <param name="form">This is the object reference of form passed by runButton_click Event</param>
        /// <returns>true if there are no errors</returns>
        public bool runIfClause(string line, Form1 form)
        {



            try
            {

                bool thenCheck = false;
                if (!checkIfClause(line, form))
                    return false;

                if (!checkThenClause(form))
                {
                    if (!checkEndIfClause(form))
                        return false;


                }
                else
                {
                    thenCheck = true;
                }



                if (!checkValidLine(line))
                {

                    form.consoleTextBox.Text = "Invalid if endif statement on line " + lineCount;
                    return false;
                }





                bool conditionStatus = false;
                int value2 = Int32.Parse(conditionValue);
                int value1 = Int32.Parse(variableValue);



                if (conditionOperator == "<=")
                {
                    if (value1 <= value2)
                        conditionStatus = true;
                }

                else if (conditionOperator == ">=")
                {
                    if (value1 >= value2)
                        conditionStatus = true;
                }
                else if (conditionOperator == "=")
                {
                    if (value1 == value2)
                        conditionStatus = true;
                }
                else if (conditionOperator == ">")
                {
                    if (value1 > value2)
                        conditionStatus = true;
                }
                else if (conditionOperator == "<")
                {
                    if (value1 < value2)
                        conditionStatus = true;
                }
                else if (conditionOperator == "!=")
                {
                    if (value1 != value2)
                        conditionStatus = true;
                }

                string txt = form.codeTextBox.Text.Trim();
                string[] lines = txt.Split(new Char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);

                if (!thenCheck)
                {




                    for (int i = lineCount; i < lines.Length; i++)
                    {

                        if ((lines[i].ToLower().Trim() == "endif"))
                        {
                            ifStatementCounter += 1;
                            break;
                        }
                        else ifStatementCounter++;
                    }

                    if (ifStatementCounter == 0)
                    {
                        form.consoleTextBox.Text = "No statement inside if endif clause on line " + lineCount;
                        return false;
                    }
                    if (conditionStatus == true)
                    {
                        ifStatementCounter = 0;
                    }
                    return true;
                }
                else
                {
                    if (conditionStatus)
                    {
                        string eline = lines[lineCount + 1].ToLower().Trim();
                        if (!caseExecution(eline, form))
                            return false;
                        ifStatementCounter = 2;
                    }
                    else
                    {
                        ifStatementCounter = 2;
                        MessageBox.Show("Condition is false!!");
                    }
                    return true;
                }




            }
            catch (Exception )
            {
                form.consoleTextBox.Text = "Invalid if endif statement on line" + lineCount;
                return false;
            }

        }

        /// <summary>
        /// checks if the line start with "if".
        /// </summary>
        /// <param name="line">Line is a single line in a forEach loop</param>
        /// <param name="form">This is the object reference of form passed by runButton_click Event</param>
        /// <returns>ture if the lines start with if</returns>
        public bool isIfClause(String line, Form1 form)
        {



            if (line.StartsWith("if"))
            {
                return true;
            }
            return false;

        }

        /// <summary>
        /// Checks if the line contains "then" 
        /// /// </summary>
        /// <param name="form">This is the object reference of form passed by runButton_click Event</param>
        /// <returns></returns>
        public bool checkThenClause(Form1 form)
        {

            string txt = form.codeTextBox.Text.Trim();
            string[] lines = txt.Split(new Char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);

            string l = lines[lineCount];
            l = l.ToLower().Trim();
            Console.WriteLine(l);
            if (l == "then")
            {
                return true;
            }

            return false;
        }
        /// <summary>
        /// Check if the if statetment is closed with endif or not
        /// </summary>
        /// <param name="form">This is the object reference of form passed by runButton_click Event</param>
        /// <returns>true if the line contains endif</returns>
        public bool checkEndIfClause(Form1 form)
        {
            Console.WriteLine("GAYO");
            string txt = form.codeTextBox.Text.Trim();
            string[] lines = txt.Split(new Char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
            bool endIfExistence = false;

            for (int i = lineCount; i < lines.Length; i++)
            {
                lines[i] = lines[i].ToLower().Trim();
                if ((lines[i] == "endif"))
                    endIfExistence = true;
            }

            if (!endIfExistence)
            {
                Console.WriteLine("R1");
                form.consoleTextBox.Text = "1 if statement not closed on line " + lineCount;
                return false;
            }



            for (int i = lineCount; i < lines.Length; i++)
            {
                if (lines[i].StartsWith("if") && lines[i] != "endif")
                {
                    form.consoleTextBox.Text = "2 If Statement not closed on line" + lineCount;
                    Console.WriteLine("R2");
                    return false;
                }
                if (lines[i] == "endif")
                {
                    break;
                }
            }


            return true;
        }


        /// <summary>
        /// Check the condition operator, 
        /// </summary>
        /// <param name="line">Line is a single line in a forEach loop</param>
        /// <param name="form">This is the object reference of form passed by runButton_click Event</param>
        /// <returns>true if the condition is valid</returns>
        public bool checkIfClause(string line, Form1 form)
        {

            line = line.ToLower();
            if (!line.StartsWith("if"))
            {
                return false;
            }
            int start = line.IndexOf("(") + 1;
            int end = line.IndexOf(")", start);

            string condition = line.Substring(start, end - start);
            Console.WriteLine(condition);

            //check for operator
            if (condition.Contains("<=") && !condition.Contains(">"))
            {
                conditionOperator = "<=";
            }
            else if (condition.Contains(">=") && !condition.Contains("<"))
            {
                conditionOperator = ">=";
            }
            else if (condition.Contains("!="))
            {
                conditionOperator = "!=";
            }
            else if (condition.Contains("=") && !condition.Contains(">") && !condition.Contains("<"))
            {
                conditionOperator = "=";
            }
            else if (!condition.Contains("=") && condition.Contains(">") && !condition.Contains("<"))
            {
                conditionOperator = ">";

            }
            else if (!condition.Contains("=") && !condition.Contains(">") && condition.Contains("<"))
            {
                conditionOperator = "<";
            }
            else
            {
                form.consoleTextBox.Text = "Invalid if endif statement on line" + lineCount;
                return false;

            }


            string[] splitCondition = condition.Split(new string[] { conditionOperator }, StringSplitOptions.RemoveEmptyEntries);

            if (splitCondition.Length != 2)
            {
                form.consoleTextBox.Text = "Invalid if endif statement on line" + lineCount;
                return false;
            }

            conditionKey = splitCondition[0].Trim().ToLower();
            conditionValue = splitCondition[1].Trim().ToLower();
            bool variableCheck = false;

            foreach (KeyValuePair<string, string> kvp in variableDictionary)
            {
                if (conditionKey == kvp.Key)
                {
                    variableValue = kvp.Value;
                    variableCheck = true;
                }
            }
            if (variableCheck == false)
            {
                form.consoleTextBox.Text = "Invalid if else statement: variable doesn't exist on line " + lineCount;
                return false;
            }

            return true;

        }
        /// <summary>
        /// Checks if the line contains = and it doesn't begin with if loop or method
        /// </summary>
        /// <param name="line">Line is a single line in a forEach loop</param>
        /// <param name="form">This is the object reference of form passed by runButton_click Event</param>
        /// <returns>true if the line contains =</returns>
        public bool isVariable(String line, Form1 form)
        {

            line = line.ToLower();
            int equalsCount = line.Count(c => c == '=');
            if (equalsCount != 1)
            {
                return false;
            }

            if (line.StartsWith("if") || line.StartsWith("loop") || line.StartsWith("method"))
                return false;

            return true;
        }


        /// <summary>
        /// Invalid declration method
        /// </summary>
        /// <param name="form">This is the object reference of form passed by runButton_click Event</param>
        public void invalidVariable(Form1 form)
        {
            form.consoleTextBox.Text = "Invalid declaration of a variable on line" + lineCount;
        }
        /// <summary>
        /// stores the variable and its variable in dictionary
        /// </summary>
        /// <param name="line">Line is a single line in a forEach loop</param>
        /// <param name="form">This is the object reference of form passed by runButton_click Event</param>
        /// <returns>true if the store process is successful</returns>
        public bool runVariable(Form1 form, String line)
        {
            try
            {
                if (!checkVariable(line, form))
                    return false;
            }
            catch (Exception )
            {
                invalidVariable(form);
                return false;
            }

            string[] variableEntity = line.Split(new Char[] { '=' }, StringSplitOptions.RemoveEmptyEntries);
            string key = variableEntity[0].ToLower().Trim();
            string value = variableEntity[1].Trim();

            //if the variable is already stored it replaces the value
            if (variableDictionary.ContainsKey(key))
            {
                variableDictionary[key] = value;
            }
            else
            {
                variableDictionary.Add(key, value);
            }
            Console.WriteLine(variableDictionary);
            return true;
        }

        /// <summary>
        /// checks ithe the variable is of right syntax
        /// </summary>
        /// <param name="line">Line is a single line in a forEach loop</param>
        /// <param name="form">This is the object reference of form passed by runButton_click Event</param>
        /// <returns>true if the syntax is correct</returns>
        public bool checkVariable(String line, Form1 form)
        {
            line = line.ToLower();
            //counts the number of = in the line
            int equalsCount = line.Count(c => c == '=');
            if (equalsCount != 1)
            {
                return false;
            }

            if (line.StartsWith("if") || line.StartsWith("loop") || line.StartsWith("method"))
                return false;

            string[] variableEntity = line.Split(new Char[] { '=' }, StringSplitOptions.RemoveEmptyEntries);
            string[] variable = variableEntity[0].Split(new Char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            string[] rightSidePart = variableEntity[1].Split(new Char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            if (variable.Length != 1 || rightSidePart.Length != 1)
            {
                invalidVariable(form);
                return false;
            }
            Console.WriteLine("is variable");
            return true;
        }


        /// <summary>
        /// checks if the shape is valid or not
        /// </summary>
        /// <param name="line">Line is a single line in a forEach loop</param>
        /// <returns>true if the command is valid</returns>

        public bool isShapeCommand(String line)
        {
            //valid shape
            bool result = checkValidShape(line);
            //valid other command like drawto or moveto
            bool result2 = checkValidOtherCommand(line);

            if (result || result2)
                return true;

            return false;
        }

        /// <summary>
        /// checks for valid paramter and runs draws the shape
        /// </summary>
        /// <param name="line">Line is a single line in a forEach loop</param>
        /// <param name="form">This is the object reference of form passed by runButton_click Event</param>
        /// <returns>true if the shape is valid</returns>
        public bool runShapeCommand(Form1 form, String line)
        {

            bool variablePresent = false;
            string[] paramList = { };
            try
            {
                paramList = getParamter(line);

            }
            catch (Exception )
            {
                form.consoleTextBox.Text = $"Syntax Error: Line {lineCount} (Invalid Line)  ";
            }

            foreach (string param in paramList)
            {
                if (line.Contains("pen"))
                {
                    variablePresent = false;
                    break;
                }
                else
                {
                    //checks if the parameter is numeric or string
                    bool isNumeric = int.TryParse(param, out _);
                    if (!isNumeric)
                    {
                        variablePresent = true;
                        if (variableDictionary.ContainsKey(param))
                        {
                            string val = variableDictionary[param];
                            line = line.Replace(param, val);
                            Console.WriteLine(line);

                        }
                        else
                        {
                            form.compilationFailed();
                            form.consoleTextBox.Text = "variable doesn't exist and cannot be used as parameter on line " + lineCount;
                            return false;
                        }
                    }
                }
            }
            if (variablePresent)
            {
                if (!caseExecution(line, form))
                    return false;

                return true;
            }



            //loops every elemet of lines array

            //Validation Check
            validShape = checkValidShape(line);
            validOtherCommand = checkValidOtherCommand(line);

            try
            {
                //valdiates if the line is valid or not
                validLine = checkValidLine(line);

            }
            catch (Exception )
            {
                form.compilationFailed();
                form.consoleTextBox.Text = $"Syntax Error: Line {lineCount} (Invalid Line)  ";
                return false;

            }

            //If line is not valid the loop breaks and error message is shown
            if (!validLine)
            {
                form.compilationFailed();
                form.consoleTextBox.Text = $"Syntax Error:  Line {lineCount} (Invalid Line)  ";
                return false;

            }
            //not a valid line
            if ((!validShape && !validOtherCommand) || (validShape && validOtherCommand))
            {
                form.compilationFailed();
                form.consoleTextBox.Text = $"Syntax Error: Line {lineCount} (Invalid Command) B";
                return false;

            }

            if (validOtherCommand || validShape)
            {

                try
                {
                    //check for valid Paramter
                    validParameterCheck = checkValidParameter(line);
                }
                catch (Exception )
                {
                    form.compilationFailed();
                    form.consoleTextBox.Text = $"Syntax Error: Line {lineCount} (Invalid Parameter) ";
                    return false;


                }


            }





            if (!validParameterCheck)
            {
                form.compilationFailed();
                form.consoleTextBox.Text = $"Syntax Error : Line {lineCount} (Invalid Parameter)2";
                return false;

            }

            //After all validation success, following code will execute
            if (validShape && !validOtherCommand)
            {


                try
                {
                    //gets paramter from the line
                    string[] parameters = getParamter(line);
                    //converts it into integer array
                    int[] integerParameter = parameters.Select(int.Parse).ToArray();


                    //resize the array by two to further add x and y coordinate at the end of the array
                    Array.Resize(ref integerParameter, integerParameter.Length + 2);
                    integerParameter[integerParameter.Length - 2] = xCoordinate;
                    integerParameter[integerParameter.Length - 1] = yCoordinate;


                    //creates graphics on the drawing panel
                    g = form.drawingBoard.CreateGraphics();
                    //real time object of the required shape will be generated from factory
                    Shape shape1 = shapeFactory.getShape(finalShape);

                    //The corresponding shape methods are called and the parameters including coordinates are passed.
                    shape1.set(integerParameter);

                    shape1.draw(c, g, form);
                }
                catch (Exception )
                {
                    form.compilationFailed();

                    form.consoleTextBox.Text = $"Syntax Error: Line {lineCount} (Invalid Command) ";


                }
            }

            if (!validShape && validOtherCommand)
            {
                try
                {
                    string[] parameters = getParamter(line);
                    int[] integerParameter = new int[50];
                    if (finalotherCommand != "pen")
                    {
                        integerParameter = parameters.Select(int.Parse).ToArray();
                    }
                    g = form.drawingBoard.CreateGraphics();
                    //If moveTo command is to be executed
                    if (finalotherCommand == "moveto")
                    {
                        xCoordinate = integerParameter[0];
                        yCoordinate = integerParameter[1];
                    }

                    if (finalotherCommand == "pen")
                    {
                        if (parameters[0].ToLower().Equals("green"))
                        {

                            c = Color.Green;
                        }
                        else if (parameters[0].ToLower().Equals("red"))
                        {

                            c = Color.Red;
                        }
                        else if (parameters[0].ToLower().Equals("blue"))
                        {
                            c = Color.Blue;
                        }
                        else if (parameters[0].ToLower().Equals("yellow"))
                        {
                            c = Color.Yellow;
                        }
                        else if (parameters[0].ToLower().Equals("orange"))
                        {
                            c = Color.Orange;
                        }
                    }

                    //if draw to command is to be executed
                    if (finalotherCommand == "drawto")
                    {
                        Point p1 = new Point(xCoordinate, yCoordinate);
                        Point p2 = new Point(integerParameter[0], integerParameter[1]);
                        Pen pen1 = new Pen(c, 2);
                        g.DrawLine(pen1, p1, p2);

                    }
                }
                catch (Exception )
                {
                    form.compilationFailed();
                    form.consoleTextBox.Text = $"Syntax Error: Line {lineCount} (Invalid Command) ";
                }


            }





            return true;


        }
        /// <summary>
        /// splits the text by ')' and checks if there are more than one element in the splitted array
        /// if tehere are more text after ')' the line is invalid
        /// </summary>
        /// <param name="line"> This a line variable of string type which is each line of textbox</param>
        /// <returns> return true if the line is valid line</returns>
        public bool checkValidLine(string line)
        {

            line = line.Trim();
            string[] validText = line.Split(new Char[] { ')' }, StringSplitOptions.RemoveEmptyEntries);
            if (validText.Length >= 2)
            {
                return false;
            }

            return true;
        }


        /// <summary>
        /// checks for valid shapes like circle or rectangle
        /// </summary>
        /// <param name="line"> This a line variable of string type which is each line of textbox</param>
        /// <returns> return true if the shape is valid</returns>
        public bool checkValidShape(String line)
        {
            foreach (string shape in shapes)
            {
                if (line.StartsWith(shape))
                {

                    finalShape = shape;
                    commandToExecute = shape;

                    return true;

                }

            }
            return false;
        }


        /// <summary>
        /// checks for valid other commands like drawTo or moveTo
        /// </summary>
        /// <param name="line"> This a line variable of string type which is each line of textbox</param>
        /// <returns>returns true if the othercommands are valid</returns>
        public bool checkValidOtherCommand(String line)
        {
            foreach (string command in otherCommands)
            {
                if (line.StartsWith(command))
                {
                    finalotherCommand = command;
                    commandToExecute = command;

                    return true;

                }

            }
            return false;
        }

        /// <summary>
        /// check for valid Prameter
        /// </summary>
        /// <param name="line"> This is the line variable of string type which is each line of textbox</param>
        /// <returns> returns true if the parameters are valid</returns>
        public bool checkValidParameter(string line)
        {

            Dictionary<string, int> list =
            new Dictionary<string, int>();

            // Adding key/values pairs in myDict 
            list.Add("circle", 1);
            list.Add("triangle", 1);
            list.Add("rectangle", 2);
            list.Add("moveto", 2);
            list.Add("drawto", 2);
            list.Add("polygon", 6);
            list.Add("pen", 1);



            string[] parameters = getParamter(line);

            foreach (string param in parameters)
            {
                if (string.IsNullOrEmpty(param))
                {
                    Console.WriteLine("fasyo");
                    return false;
                }
                if (line.StartsWith("pen"))
                {

                    if (parameters.Length == 1 && (param.Equals("red") || param.Equals("yellow") || param.Equals("green") || param.Equals("blue") || param.Equals("orange")))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                }
            }

            int[] integerParameter = parameters.Select(int.Parse).ToArray();

            foreach (KeyValuePair<string, int> kvp in list)
            {



                if (kvp.Key == commandToExecute)
                {



                    if (integerParameter.Length != kvp.Value)
                    {

                        Console.WriteLine(finalotherCommand);
                        Console.WriteLine(kvp.Key + " = " + kvp.Value + " length=" + integerParameter.Length);

                        return false;
                    }

                }


            }


            return true;
        }



        /// <summary>
        /// takes string inside the parenthesis and split them by comma and returns the array
        /// </summary>
        /// <param name="line"> This a line variable of string type which is each line of textbox</param>
        /// <returns> returns parameter array by splitting the text with comma</returns>
        public string[] getParamter(string line)
        {

            int start = line.IndexOf("(") + 1;
            int end = line.IndexOf(")", start);

            string result = line.Substring(start, end - start);
            string[] parameterList = result.Split(new Char[] { ',' });

            return parameterList;


        }

        /// <summary>
        /// count the number of text between the commas
        /// </summary>
        /// <param name="line">This a line variable of string type which is each line of textbox</param>
        /// <returns></returns>
        public int parameterCount(string line)
        {
            int count = 0;
            int start = line.IndexOf("(") + 1;
            int end = line.IndexOf(")", start);

            string result = line.Substring(start, end - start);
            string[] parameterList = result.Split(new Char[] { ',' });
            foreach (string p in parameterList)
            {
                count++;
            }
            return count;
        }




    }
}
