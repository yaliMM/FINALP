using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace CSharpToCpp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void compileButton_Click(object sender, EventArgs e)
        {
            try
            {
                // קלט C#
                string inputCode = inputTextBox.Text;

                // 1. Lexer
                var lexer = new Lexer.Lexer(inputCode);
                lexer.Start();

                // הצגת טוקנים
                outputTextBox.Clear();
                outputTextBox.AppendText("🔹 Tokens:\r\n");
                foreach (var token in lexer.SymbolTable)
                    outputTextBox.AppendText(token + "\r\n");

                // 2. Parser
                var parser = new Parser.Parser(lexer.SymbolTable);
                parser.Start();

                // 3. Code Generator
                var generator = new CodeGenerator.CodeGenerator(lexer.SymbolTable);
                string cppCode = generator.Generate();

                // הצגת קוד C++
                outputTextBox.AppendText("\r\n🔸 Generated C++ Code:\r\n");
                outputTextBox.AppendText(cppCode);

                // כתיבה לקובץ
                File.WriteAllText("temp.cpp", cppCode);

                // קומפילציה
                var compileInfo = new ProcessStartInfo("g++", "temp.cpp -o temp.exe")
                {
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };
                var compile = Process.Start(compileInfo);
                compile.WaitForExit();

                string compileErrors = compile.StandardError.ReadToEnd();
                if (!string.IsNullOrWhiteSpace(compileErrors))
                {
                    executionOutputTextBox.Text = "❌ Compilation error:\r\n" + compileErrors;
                    return;
                }
                executionOutputTextBox.Text = "✅ Compilation successful.\r\n";

                // הרצה
                var runInfo = new ProcessStartInfo("temp.exe")
                {
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };
                var run = Process.Start(runInfo);
                string result = run.StandardOutput.ReadToEnd();

                // הצגת פלט הריצה
                executionOutputTextBox.AppendText("▶ Program Output:\r\n" + result);
            }
            catch (Exception ex)
            {
                executionOutputTextBox.Text = "❌ Error:\r\n" + ex.Message;
            }
        }
    }
}
