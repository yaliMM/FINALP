namespace CSharpToCpp
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.TextBox inputTextBox;
        private System.Windows.Forms.Button compileButton;
        private System.Windows.Forms.TableLayoutPanel resultsLayout;
        private System.Windows.Forms.TextBox outputTextBox;
        private System.Windows.Forms.TextBox executionOutputTextBox;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.inputTextBox = new System.Windows.Forms.TextBox();
            this.compileButton = new System.Windows.Forms.Button();
            this.resultsLayout = new System.Windows.Forms.TableLayoutPanel();
            this.outputTextBox = new System.Windows.Forms.TextBox();
            this.executionOutputTextBox = new System.Windows.Forms.TextBox();

            // 
            // inputTextBox
            // 
            this.inputTextBox.Multiline = true;
            this.inputTextBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.inputTextBox.Height = 100;
            this.inputTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.inputTextBox.Font = new System.Drawing.Font("Consolas", 10F);
            // 
            // compileButton
            // 
            this.compileButton.Text = "Compile";
            this.compileButton.Dock = System.Windows.Forms.DockStyle.Top;
            this.compileButton.Height = 40;
            this.compileButton.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.compileButton.Click += new System.EventHandler(this.compileButton_Click);
            // 
            // resultsLayout
            // 
            this.resultsLayout.ColumnCount = 2;
            this.resultsLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.resultsLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.resultsLayout.RowCount = 1;
            this.resultsLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.resultsLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            // 
            // outputTextBox
            // 
            this.outputTextBox.Multiline = true;
            this.outputTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.outputTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.outputTextBox.Font = new System.Drawing.Font("Consolas", 10F);
            this.outputTextBox.ReadOnly = true;
            // 
            // executionOutputTextBox
            // 
            this.executionOutputTextBox.Multiline = true;
            this.executionOutputTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.executionOutputTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.executionOutputTextBox.Font = new System.Drawing.Font("Consolas", 10F);
            this.executionOutputTextBox.ReadOnly = true;
            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(1000, 600);
            this.Controls.Add(this.resultsLayout);
            this.Controls.Add(this.compileButton);
            this.Controls.Add(this.inputTextBox);
            this.Name = "Form1";
            this.Text = "CSharpToCpp Transpiler";

            // הוספת ה־TextBox-ים ל־TableLayoutPanel
            this.resultsLayout.Controls.Add(this.outputTextBox, 0, 0);
            this.resultsLayout.Controls.Add(this.executionOutputTextBox, 1, 0);

            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
