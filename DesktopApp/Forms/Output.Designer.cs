using System.Windows.Forms;

namespace DesktopApp.Forms
{
    partial class Output
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent(int x, int y, int width, int height, int marginRight)
        {
            this.OutputTextBox = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // OutputTextBox
            // 
            this.OutputTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.OutputTextBox.Location = new System.Drawing.Point(0, 0);
            this.OutputTextBox.Name = "OutputTextBox";
            this.OutputTextBox.ReadOnly = true;
            this.OutputTextBox.Size = new System.Drawing.Size(width, height);
            this.OutputTextBox.TabIndex = 0;
            this.OutputTextBox.Text = "";
            this.OutputTextBox.RightMargin = width - marginRight;
            // 
            // Output
            // 
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new System.Drawing.Point(x, y);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(width, height);
            this.Width = width;
            this.Height = height;
            this.Controls.Add(this.OutputTextBox);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Output";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Output - 초맨의 한글 비주얼 노벨 프로젝트";
            this.TopMost = true;
            this.Move += new System.EventHandler(this.Output_Move);
            this.Resize += new System.EventHandler(this.Output_Resize);
            this.ResumeLayout(false);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Output_Closing);
        }

        #endregion

        private System.Windows.Forms.RichTextBox OutputTextBox;
    }
}