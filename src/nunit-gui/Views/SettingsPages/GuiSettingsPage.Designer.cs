namespace NUnit.Gui.Views.SettingsPages
{
    partial class GuiSettingsPage
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.checkFilesExistCheckBox = new System.Windows.Forms.CheckBox();
            this.miniGuiRadioButton = new System.Windows.Forms.RadioButton();
            this.fullGuiRadioButton = new System.Windows.Forms.RadioButton();
            this.label3 = new System.Windows.Forms.Label();
            this.recentFilesCountTextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.loadLastProjectCheckBox = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // checkFilesExistCheckBox
            // 
            this.checkFilesExistCheckBox.AutoSize = true;
            this.checkFilesExistCheckBox.Location = new System.Drawing.Point(27, 159);
            this.checkFilesExistCheckBox.Name = "checkFilesExistCheckBox";
            this.checkFilesExistCheckBox.Size = new System.Drawing.Size(185, 17);
            this.checkFilesExistCheckBox.TabIndex = 45;
            this.checkFilesExistCheckBox.Text = "Check that files exist before listing";
            this.checkFilesExistCheckBox.UseVisualStyleBackColor = true;
            // 
            // miniGuiRadioButton
            // 
            this.miniGuiRadioButton.AutoSize = true;
            this.miniGuiRadioButton.Location = new System.Drawing.Point(27, 56);
            this.miniGuiRadioButton.Name = "miniGuiRadioButton";
            this.miniGuiRadioButton.Size = new System.Drawing.Size(148, 17);
            this.miniGuiRadioButton.TabIndex = 44;
            this.miniGuiRadioButton.Text = "Mini Gui showing tree only";
            // 
            // fullGuiRadioButton
            // 
            this.fullGuiRadioButton.AutoSize = true;
            this.fullGuiRadioButton.Location = new System.Drawing.Point(27, 24);
            this.fullGuiRadioButton.Name = "fullGuiRadioButton";
            this.fullGuiRadioButton.Size = new System.Drawing.Size(215, 17);
            this.fullGuiRadioButton.TabIndex = 43;
            this.fullGuiRadioButton.Text = "Full Gui with progress bar and result tabs";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(147, 120);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 13);
            this.label3.TabIndex = 41;
            this.label3.Text = "files in menu";
            // 
            // recentFilesCountTextBox
            // 
            this.recentFilesCountTextBox.Location = new System.Drawing.Point(91, 120);
            this.recentFilesCountTextBox.Name = "recentFilesCountTextBox";
            this.recentFilesCountTextBox.Size = new System.Drawing.Size(40, 20);
            this.recentFilesCountTextBox.TabIndex = 40;
            this.recentFilesCountTextBox.Validating += new System.ComponentModel.CancelEventHandler(this.recentFilesCountTextBox_Validating);
            this.recentFilesCountTextBox.Validated += new System.EventHandler(this.recentFilesCountTextBox_Validated);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(27, 120);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(23, 13);
            this.label4.TabIndex = 39;
            this.label4.Text = "List";
            // 
            // loadLastProjectCheckBox
            // 
            this.loadLastProjectCheckBox.AutoSize = true;
            this.loadLastProjectCheckBox.Location = new System.Drawing.Point(27, 198);
            this.loadLastProjectCheckBox.Name = "loadLastProjectCheckBox";
            this.loadLastProjectCheckBox.Size = new System.Drawing.Size(193, 17);
            this.loadLastProjectCheckBox.TabIndex = 42;
            this.loadLastProjectCheckBox.Text = "Load most recent project at startup.";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Location = new System.Drawing.Point(130, 96);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(295, 8);
            this.groupBox2.TabIndex = 37;
            this.groupBox2.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 96);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 13);
            this.label2.TabIndex = 38;
            this.label2.Text = "Recent Files";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Location = new System.Drawing.Point(130, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(295, 8);
            this.groupBox1.TabIndex = 35;
            this.groupBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 13);
            this.label1.TabIndex = 36;
            this.label1.Text = "Gui Display";
            // 
            // GuiSettingsPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.checkFilesExistCheckBox);
            this.Controls.Add(this.miniGuiRadioButton);
            this.Controls.Add(this.fullGuiRadioButton);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.recentFilesCountTextBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.loadLastProjectCheckBox);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.Name = "GuiSettingsPage";
            this.Size = new System.Drawing.Size(428, 293);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox checkFilesExistCheckBox;
        private System.Windows.Forms.RadioButton miniGuiRadioButton;
        private System.Windows.Forms.RadioButton fullGuiRadioButton;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox recentFilesCountTextBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox loadLastProjectCheckBox;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
    }
}
