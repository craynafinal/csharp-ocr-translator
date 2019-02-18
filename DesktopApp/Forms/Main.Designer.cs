namespace DesktopApp
{
    partial class Main
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
        private void InitializeComponent()
        {
            this.ConfigButton = new System.Windows.Forms.Button();
            this.DictionaryButton = new System.Windows.Forms.Button();
            this.SetTargetButton = new System.Windows.Forms.Button();
            this.OutputButton = new System.Windows.Forms.Button();
            this.ConfigLabel = new System.Windows.Forms.Label();
            this.DictionaryLabel = new System.Windows.Forms.Label();
            this.SetTargetLabel = new System.Windows.Forms.Label();
            this.OutputLabel = new System.Windows.Forms.Label();
            this.StopLabel = new System.Windows.Forms.Label();
            this.RunLabel = new System.Windows.Forms.Label();
            this.StopButton = new System.Windows.Forms.Button();
            this.RunButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ConfigButton
            // 
            this.ConfigButton.Location = new System.Drawing.Point(13, 13);
            this.ConfigButton.Name = "ConfigButton";
            this.ConfigButton.Size = new System.Drawing.Size(75, 23);
            this.ConfigButton.TabIndex = 0;
            this.ConfigButton.Text = "Config";
            this.ConfigButton.UseVisualStyleBackColor = true;
            this.ConfigButton.Click += new System.EventHandler(this.ConfigButton_Click);
            // 
            // DictionaryButton
            // 
            this.DictionaryButton.Location = new System.Drawing.Point(13, 51);
            this.DictionaryButton.Name = "DictionaryButton";
            this.DictionaryButton.Size = new System.Drawing.Size(75, 23);
            this.DictionaryButton.TabIndex = 1;
            this.DictionaryButton.Text = "Dictionary";
            this.DictionaryButton.UseVisualStyleBackColor = true;
            this.DictionaryButton.Click += new System.EventHandler(this.DictionaryButton_Click);
            // 
            // SetTargetButton
            // 
            this.SetTargetButton.Location = new System.Drawing.Point(13, 89);
            this.SetTargetButton.Name = "SetTargetButton";
            this.SetTargetButton.Size = new System.Drawing.Size(75, 23);
            this.SetTargetButton.TabIndex = 3;
            this.SetTargetButton.Text = "Set Target";
            this.SetTargetButton.UseVisualStyleBackColor = true;
            this.SetTargetButton.Click += new System.EventHandler(this.SetTargetButton_Click);
            // 
            // OutputButton
            // 
            this.OutputButton.Location = new System.Drawing.Point(13, 127);
            this.OutputButton.Name = "OutputButton";
            this.OutputButton.Size = new System.Drawing.Size(75, 23);
            this.OutputButton.TabIndex = 5;
            this.OutputButton.Text = "Output";
            this.OutputButton.UseVisualStyleBackColor = true;
            this.OutputButton.Click += new System.EventHandler(this.OutputButton_Click);
            // 
            // ConfigLabel
            // 
            this.ConfigLabel.AutoSize = true;
            this.ConfigLabel.Location = new System.Drawing.Point(94, 18);
            this.ConfigLabel.Name = "ConfigLabel";
            this.ConfigLabel.Size = new System.Drawing.Size(131, 12);
            this.ConfigLabel.TabIndex = 6;
            this.ConfigLabel.Text = "Configure the program";
            // 
            // DictionaryLabel
            // 
            this.DictionaryLabel.AutoSize = true;
            this.DictionaryLabel.Location = new System.Drawing.Point(94, 56);
            this.DictionaryLabel.Name = "DictionaryLabel";
            this.DictionaryLabel.Size = new System.Drawing.Size(177, 12);
            this.DictionaryLabel.TabIndex = 7;
            this.DictionaryLabel.Text = "Force replace with other string";
            // 
            // SetTargetLabel
            // 
            this.SetTargetLabel.AutoSize = true;
            this.SetTargetLabel.Location = new System.Drawing.Point(94, 94);
            this.SetTargetLabel.Name = "SetTargetLabel";
            this.SetTargetLabel.Size = new System.Drawing.Size(179, 12);
            this.SetTargetLabel.TabIndex = 8;
            this.SetTargetLabel.Text = "Set target area (Ctrl + Shift + x)";
            // 
            // OutputLabel
            // 
            this.OutputLabel.AutoSize = true;
            this.OutputLabel.Location = new System.Drawing.Point(94, 132);
            this.OutputLabel.Name = "OutputLabel";
            this.OutputLabel.Size = new System.Drawing.Size(208, 12);
            this.OutputLabel.TabIndex = 9;
            this.OutputLabel.Text = "Open output window again if closed";
            // 
            // StopLabel
            // 
            this.StopLabel.AutoSize = true;
            this.StopLabel.Location = new System.Drawing.Point(94, 208);
            this.StopLabel.Name = "StopLabel";
            this.StopLabel.Size = new System.Drawing.Size(189, 12);
            this.StopLabel.TabIndex = 15;
            this.StopLabel.Text = "Stop Translation (Ctrl + Shift + c)";
            // 
            // RunLabel
            // 
            this.RunLabel.AutoSize = true;
            this.RunLabel.Location = new System.Drawing.Point(94, 170);
            this.RunLabel.Name = "RunLabel";
            this.RunLabel.Size = new System.Drawing.Size(186, 12);
            this.RunLabel.TabIndex = 14;
            this.RunLabel.Text = "Run Translation (Ctrl + Shift + z)";
            // 
            // StopButton
            // 
            this.StopButton.Location = new System.Drawing.Point(13, 203);
            this.StopButton.Name = "StopButton";
            this.StopButton.Size = new System.Drawing.Size(75, 23);
            this.StopButton.TabIndex = 12;
            this.StopButton.Text = "Stop";
            this.StopButton.UseVisualStyleBackColor = true;
            this.StopButton.Click += new System.EventHandler(this.StopButton_Click);
            // 
            // RunButton
            // 
            this.RunButton.Location = new System.Drawing.Point(13, 165);
            this.RunButton.Name = "RunButton";
            this.RunButton.Size = new System.Drawing.Size(75, 23);
            this.RunButton.TabIndex = 11;
            this.RunButton.Text = "Run";
            this.RunButton.UseVisualStyleBackColor = true;
            this.RunButton.Click += new System.EventHandler(this.RunButton_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(311, 241);
            this.Controls.Add(this.StopLabel);
            this.Controls.Add(this.RunLabel);
            this.Controls.Add(this.StopButton);
            this.Controls.Add(this.RunButton);
            this.Controls.Add(this.OutputLabel);
            this.Controls.Add(this.SetTargetLabel);
            this.Controls.Add(this.DictionaryLabel);
            this.Controls.Add(this.ConfigLabel);
            this.Controls.Add(this.OutputButton);
            this.Controls.Add(this.SetTargetButton);
            this.Controls.Add(this.DictionaryButton);
            this.Controls.Add(this.ConfigButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(1);
            this.Name = "Main";
            this.Text = "OCR Translator";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button ConfigButton;
        private System.Windows.Forms.Button DictionaryButton;
        private System.Windows.Forms.Button SetTargetButton;
        private System.Windows.Forms.Button OutputButton;
        private System.Windows.Forms.Label ConfigLabel;
        private System.Windows.Forms.Label DictionaryLabel;
        private System.Windows.Forms.Label SetTargetLabel;
        private System.Windows.Forms.Label OutputLabel;
        private System.Windows.Forms.Label StopLabel;
        private System.Windows.Forms.Label RunLabel;
        private System.Windows.Forms.Button StopButton;
        private System.Windows.Forms.Button RunButton;
    }
}

