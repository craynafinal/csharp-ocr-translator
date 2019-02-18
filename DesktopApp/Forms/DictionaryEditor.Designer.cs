namespace DesktopApp.Forms
{
    partial class DictionaryEditor
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
            this.SaveButton = new System.Windows.Forms.Button();
            this.DictionaryTextBox = new System.Windows.Forms.RichTextBox();
            this.NotificationLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // SaveButton
            // 
            this.SaveButton.Location = new System.Drawing.Point(219, 152);
            this.SaveButton.Margin = new System.Windows.Forms.Padding(1, 1, 1, 1);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(75, 23);
            this.SaveButton.TabIndex = 0;
            this.SaveButton.Text = "Save";
            this.SaveButton.UseVisualStyleBackColor = true;
            this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // DictionaryTextBox
            // 
            this.DictionaryTextBox.Location = new System.Drawing.Point(15, 9);
            this.DictionaryTextBox.Margin = new System.Windows.Forms.Padding(1, 1, 1, 1);
            this.DictionaryTextBox.Name = "DictionaryTextBox";
            this.DictionaryTextBox.Size = new System.Drawing.Size(279, 132);
            this.DictionaryTextBox.TabIndex = 1;
            this.DictionaryTextBox.Text = "";
            this.DictionaryTextBox.TextChanged += new System.EventHandler(this.DictionaryTextBox_TextChanged);
            // 
            // NotificationLabel
            // 
            this.NotificationLabel.AutoSize = true;
            this.NotificationLabel.Location = new System.Drawing.Point(15, 152);
            this.NotificationLabel.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.NotificationLabel.Name = "NotificationLabel";
            this.NotificationLabel.Size = new System.Drawing.Size(0, 12);
            this.NotificationLabel.TabIndex = 2;
            // 
            // DictionaryEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(311, 189);
            this.Controls.Add(this.NotificationLabel);
            this.Controls.Add(this.DictionaryTextBox);
            this.Controls.Add(this.SaveButton);
            this.Margin = new System.Windows.Forms.Padding(1, 1, 1, 1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DictionaryEditor";
            this.Text = "DictionaryEditor";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button SaveButton;
        private System.Windows.Forms.RichTextBox DictionaryTextBox;
        private System.Windows.Forms.Label NotificationLabel;
    }
}