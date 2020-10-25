namespace TranslationEditor
{
    partial class FindDialog
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
            this.findText = new System.Windows.Forms.TextBox();
            this.findNext = new System.Windows.Forms.Button();
            this.cancel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.wholeWords = new System.Windows.Forms.CheckBox();
            this.matchCase = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.down = new System.Windows.Forms.RadioButton();
            this.up = new System.Windows.Forms.RadioButton();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // findText
            // 
            this.findText.Location = new System.Drawing.Point(75, 12);
            this.findText.Name = "findText";
            this.findText.Size = new System.Drawing.Size(296, 20);
            this.findText.TabIndex = 0;
            // 
            // findNext
            // 
            this.findNext.Location = new System.Drawing.Point(377, 12);
            this.findNext.Name = "findNext";
            this.findNext.Size = new System.Drawing.Size(75, 23);
            this.findNext.TabIndex = 1;
            this.findNext.Text = "Find Next";
            this.findNext.UseVisualStyleBackColor = true;
            this.findNext.Click += new System.EventHandler(this.OnFindNext);
            // 
            // cancel
            // 
            this.cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancel.Location = new System.Drawing.Point(377, 41);
            this.cancel.Name = "cancel";
            this.cancel.Size = new System.Drawing.Size(75, 23);
            this.cancel.TabIndex = 2;
            this.cancel.Text = "Cancel";
            this.cancel.UseVisualStyleBackColor = true;
            this.cancel.Click += new System.EventHandler(this.OnCancel);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Find what:";
            // 
            // wholeWords
            // 
            this.wholeWords.AutoSize = true;
            this.wholeWords.Location = new System.Drawing.Point(13, 41);
            this.wholeWords.Name = "wholeWords";
            this.wholeWords.Size = new System.Drawing.Size(118, 17);
            this.wholeWords.TabIndex = 4;
            this.wholeWords.Text = "Match whole words";
            this.wholeWords.UseVisualStyleBackColor = true;
            // 
            // matchCase
            // 
            this.matchCase.AutoSize = true;
            this.matchCase.Location = new System.Drawing.Point(13, 65);
            this.matchCase.Name = "matchCase";
            this.matchCase.Size = new System.Drawing.Size(82, 17);
            this.matchCase.TabIndex = 5;
            this.matchCase.Text = "Match case";
            this.matchCase.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.down);
            this.groupBox1.Controls.Add(this.up);
            this.groupBox1.Location = new System.Drawing.Point(138, 39);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(233, 43);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Direction";
            // 
            // down
            // 
            this.down.AutoSize = true;
            this.down.Checked = true;
            this.down.Location = new System.Drawing.Point(94, 19);
            this.down.Name = "down";
            this.down.Size = new System.Drawing.Size(53, 17);
            this.down.TabIndex = 1;
            this.down.TabStop = true;
            this.down.Text = "Down";
            this.down.UseVisualStyleBackColor = true;
            // 
            // up
            // 
            this.up.AutoSize = true;
            this.up.Location = new System.Drawing.Point(17, 19);
            this.up.Name = "up";
            this.up.Size = new System.Drawing.Size(39, 17);
            this.up.TabIndex = 0;
            this.up.TabStop = true;
            this.up.Text = "Up";
            this.up.UseVisualStyleBackColor = true;
            // 
            // FindDialog
            // 
            this.AcceptButton = this.findNext;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancel;
            this.ClientSize = new System.Drawing.Size(464, 97);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.matchCase);
            this.Controls.Add(this.wholeWords);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cancel);
            this.Controls.Add(this.findNext);
            this.Controls.Add(this.findText);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FindDialog";
            this.ShowInTaskbar = false;
            this.Text = "Find";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.OnClosing);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox findText;
        private System.Windows.Forms.Button findNext;
        private System.Windows.Forms.Button cancel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox wholeWords;
        private System.Windows.Forms.CheckBox matchCase;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton down;
        private System.Windows.Forms.RadioButton up;
    }
}