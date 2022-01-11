using GeoWallE.GraphicRuntime;
using GeoWallE.GraphicRuntime.Core;
using GeoWallE.Parsing;
using GeoWallE.WFGraphicComponents;
using Syncfusion.Windows.Forms.Edit;
using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

using AST;

namespace GeoWallE
{
    public partial class MainApp : Form
    {
        public MainApp()
        {
            InitializeComponent();
            editcontrol = GetEditControl();
            editcontrol.Dock = DockStyle.Fill;
            codePanel.Controls.Add(editcontrol);

            canvas1.Clear(Color.LightSteelBlue);
            //canvas1.SetColor(Color.Blue);
            //canvas1.DrawLine(new Point(140, 320), new Point(200, 200), "");
            //canvas1.SetColor(Color.Black);
        //    canvas1.DrawPoint(new Point(140, 320), "");
           
        //    canvas1.DrawPoint(new Point(200, 200), "");

        //    canvas1.DrawPoint(new Point(250, 320), "A");
        }

        EditControl editcontrol;

        private EditControl GetEditControl()
        {
            var sceneCodeTextBox = new EditControl();

            sceneCodeTextBox.AcceptsReturn = true;
            sceneCodeTextBox.AcceptsTab = true;
            sceneCodeTextBox.AllowDrop = true;
            sceneCodeTextBox.Font = new System.Drawing.Font("Consolas", 10, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
            sceneCodeTextBox.Location = new System.Drawing.Point(8, 0);
            sceneCodeTextBox.Multiline = true;
            sceneCodeTextBox.Name = "tbCode";
            sceneCodeTextBox.Size = new System.Drawing.Size(568, 352);
            sceneCodeTextBox.TabIndex = 0;
            sceneCodeTextBox.Text = "";
            sceneCodeTextBox.WordWrap = false;

            sceneCodeTextBox.StatusBarVisible = false;
            sceneCodeTextBox.LineNumberMarginVisible = true;
            sceneCodeTextBox.IndicatorMarginVisible = false;
            sceneCodeTextBox.OutliningEnabled = true;
            sceneCodeTextBox.BraceMatchingEnabled = true;
            sceneCodeTextBox.WhiteSpaceVisible = false;
            sceneCodeTextBox.KeepTabs = false;
            sceneCodeTextBox.ContextPromptEnabled = false;
            sceneCodeTextBox.ContextChoiceEnabled = false;
            sceneCodeTextBox.SyntaxColoringEnabled = true;
            sceneCodeTextBox.GridLinesVisible = false;
            sceneCodeTextBox.IndentType = EditIndentType.Block;
            sceneCodeTextBox.ShowSplitterButton = false;

            sceneCodeTextBox.AutomaticOutliningEnabled = true;

            sceneCodeTextBox.StartAutomaticOutlining();

            sceneCodeTextBox.AddColorGroup("keywords", System.Drawing.Color.Blue, System.Drawing.Color.White, true, false, EditColorGroupType.RegularText);
            //string[] keywords = "if,then,else,undefined,let,in,intersect,and,or,not,draw,include,color,restore,clear".Split(',');
            foreach (string k in GSharpLexer.LexicalAnalyzer.Keywords)
                sceneCodeTextBox.AddKeyword(k, "keywords");

            sceneCodeTextBox.AddColorGroup("strings", System.Drawing.Color.FromArgb(255, 193, 21, 67), System.Drawing.Color.White, true, false, EditColorGroupType.RegularText);
            sceneCodeTextBox.AddTag("\"", "\"", "\\", false, "strings");

            sceneCodeTextBox.AddColorGroup("comments", System.Drawing.Color.DarkGreen, System.Drawing.Color.White, true, false, EditColorGroupType.RegularText);
            sceneCodeTextBox.AddTag("/*", "*/", "", true, "comments");
            sceneCodeTextBox.AddTag("//", "", "", false, "comments");

            sceneCodeTextBox.Dock = DockStyle.Fill;

            return sceneCodeTextBox;
        }

        private void SaveAll()
        {
            editcontrol.Save();
        }

        private void compileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //SaveAll();
           

            var outputInfo = new OutputInfo();
            var lexer = new GSharpLexer(outputInfo);
            //var parser = new GSharpParser(outputInfo);

            var code = editcontrol.Text;
            var tokenConsumer = lexer.GetTokenConsumer(editcontrol.CurrentFile, code);
            this.canvas1.Clear(Color.White);
            this.canvas1.Invalidate();
            this.canvas1.SetColor(Color.Black);
            program p = new program();
            program.errors = outputInfo;
            program.lexer = lexer;

            program.maxX = canvas1.Width;
            program.maxY = canvas1.Height;

            output.Text = "";               
            if (!outputInfo.Errors.Any())
            {
                if (viewTokensToolStripMenuItem.Checked)
                {
                    string o = "Done!\n";
                    foreach (var token in tokenConsumer)
                        o += token.ToString() + "\n";
                    output.Text = o;
                }
                try {

                    if (!p.build(tokenConsumer))
                        output.Text = "";
                }
                catch (Exception)
                {

                }
            }

            if (outputInfo.Errors.Any())
            {
                string errorList = "Error!\n";
                foreach (var er in outputInfo.Errors)
                {
                    errorList += string.Format("{0}: {1} at {2} line {3}\n", er.Code, er.Argument, Path.GetFileName(er.Location.File), er.Location.Line);
                }
                output.Text = errorList;
            }
            else
            {
                if (!viewTokensToolStripMenuItem.Checked)
                {
                    var drawer = new CanvasDrawer(this.canvas1);
                    var printer = new RichTextBoxPrinter(this.output);
                    var executionContext = new GSharpExecutionContext(drawer, printer);

                    program.drawer = drawer;
                    program.output = output;


                    Scope scope = new Scope(null);
                    try {
                        p.EvalProgram(scope);
                    }
                    catch (Exception)
                    {
                        output.Text += "Ocurrio un error evaluando";
                    }

                    //program.Run(executionContext);
                }
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveAll();
        }

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
           var a = new OpenFileDialog();
            a.Filter = "Archivos de text *.txt|*.txt";
            if (a.ShowDialog() == DialogResult.OK)
            {
                var file = a.FileName;
                editcontrol.LoadFile(file);
            }
            
        }

        private void canvas1_Load(object sender, EventArgs e)
        {

        }
    }
}
