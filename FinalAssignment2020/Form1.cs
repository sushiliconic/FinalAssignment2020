using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Linq;
using System.Collections;

namespace FinalAssignment2020
{
    public partial class Form1 : Form
    {
        /// <summary>
        /// 
        /// </summary>
        public Form1()
        {
            InitializeComponent();

            this.rotateValue.Text = "0";

        }
        /* public string ConsoleTextBoxMethod
         {
             get { return consoleTextBox.Text; }
             set { consoleTextBox.Text = value;  }
         }
         public string codeTextBoxMethod
         {
             get { return codeTextBox.Text; }
             set { codeTextBox.Text = value; }
         }*/

        
        


       

        /// <summary>
        /// Filters the text file to be allowed to save only on .txt fomart
        /// Saves the file.
        /// </summary>
        public void SaveFile()
        {
            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.Filter = "Text Files (.txt)| *.txt";
            saveFile.Title = "Save File...";
            if (saveFile.ShowDialog() == DialogResult.OK)
            {
                StreamWriter streamWriter = new StreamWriter(saveFile.FileName);
                streamWriter.Write(codeTextBox.Text);
                streamWriter.Close();
            }
        }

       /// <summary>
       /// 
       /// </summary>
        public void compilationFailed()
        {
            drawingBoard.Invalidate();
            drawingBoard.Refresh();
            MessageBox.Show("Compilation is Failed");
        }
      

       
        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) && (!char.IsControl(e.KeyChar)))
            {
                e.Handled = true;
            }
        }

        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void aboutDeveloperToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            MessageBox.Show("Graphics Compiler\n" +
              "© All Rights Reserved 2020\n" +
              "Developed by Sushil Ghimire", "About Developer");
        }

        private void commands_KeyDown_1(object sender, KeyEventArgs e)
        {
            string command = commands.Text.ToLower().Trim();
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                if (command.Equals("run"))
                {
                    CommandParser c = new CommandParser(this);
                }
                else if (command.Equals("clear"))
                {
                    drawingBoard.Invalidate();
                    rotateValue.Text = "0";
                    fill.Checked = false;
                }
                else if (command.Equals("reset"))
                {
                    if (String.IsNullOrEmpty(codeTextBox.Text))
                    {
                        codeTextBox.AppendText("moveTo(0,0)");
                    }
                    else
                    {
                        codeTextBox.AppendText("\nmoveTo(0,0)");
                    }

                    consoleTextBox.AppendText("\nThe pen position is set to (0,0)");
                }
            }
        }
        /// <summary>
        /// This method clears code textbox, consoleTextBox and drawing panel
        /// Promots user to savefile.
        /// </summary>
        /// <param name="sender">reference object</param>
        /// <param name="e">event data</param>
        private void newToolStripMenuItem_Click_1(object sender, EventArgs e)
        {

            if (!string.IsNullOrEmpty(codeTextBox.Text))
            {
                DialogResult dialogResult = MessageBox.Show("Do you want to save the file?", "Save File", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    SaveFile();
                }
            }
            //refreshing the windows
            consoleTextBox.Clear();
            codeTextBox.Clear();
            drawingBoard.Refresh();
        }

        private void openToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Filter = "Text Files (.txt)| *.txt";
            openFileDialog.Title = "Open file...";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                System.IO.StreamReader streamReader = new System.IO.StreamReader(openFileDialog.FileName);
                codeTextBox.Text = streamReader.ReadToEnd();
                streamReader.Close();
            }
        }

        private void saveToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            SaveFile();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void programmingManualToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            MessageBox.Show("1. Basic Shapes\n" +
               "  circle(radius)\n" +
               "  triangle(side)\n" +
               "  rectangle(width,height)\n" +
               "  drawTo(posX,posY)\n" +
               "  moveTo(posX,posY)\n" +
               "\n" +
               "2. Variable Declration\n" +
               " <variable name> = <variable value>\n" +
               "\n" +
               "3. Variable Operation\n" +
               "  <variable name> + or - or * <variable Value>\n" +
               "\n" +
               "4. Single line if then\n" +
               "  If(condtion)\n" +
               "  then\n" +
               "  Line1\n" +
               "\n" +
               "5.If with endif block\n" +
               "  If(condition)\n" +
               "  Line1\n" +
               "  Line2\n" +
               "  endif\n" +
               "\n" +
               "6. Loop\n" +
               "  Loop for condition\n" +
               "  Line1\n" +
               "  <counter increment>\n" +
               "endloop\n" +
               "\n" +
               "7. Method Declaration\n" +
               "  method <methodname>(<parameter>)\n" +
               "  Line1\n" +
               "  endMethod\n" +
               "\n" +
               "8. Method Call\n" +
               "  <method name>(<Parameter>)\n" +
               "\n" +
               "Clear button clears the drawing board.\n" +
               "Reset button moves the pointer to (0,0).\n" +
               "Fill Checkbox fills the color of the shapes.\n" +
               "Rotate rotates the shape with the given angle." +
               "", "Programming Manual"
               );
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        
    }
}
