namespace TestCentric.Gui.Views.SettingsPages
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
            this.label3 = new System.Windows.Forms.Label();
            this.recentFilesCountTextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.initialDisplayComboBox = new System.Windows.Forms.ComboBox();
            this.saveVisualStateCheckBox = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(147, 32);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 13);
            this.label3.TabIndex = 41;
            this.label3.Text = "files in menu";
            // 
            // recentFilesCountTextBox
            // 
            this.recentFilesCountTextBox.Location = new System.Drawing.Point(91, 32);
            this.recentFilesCountTextBox.Name = "recentFilesCountTextBox";
            this.recentFilesCountTextBox.Size = new System.Drawing.Size(40, 20);
            this.recentFilesCountTextBox.TabIndex = 40;
            this.recentFilesCountTextBox.Validating += new System.ComponentModel.CancelEventHandler(this.recentFilesCountTextBox_Validating);
            this.recentFilesCountTextBox.Validated += new System.EventHandler(this.recentFilesCountTextBox_Validated);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(27, 32);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(23, 13);
            this.label4.TabIndex = 39;
            this.label4.Text = "List";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Location = new System.Drawing.Point(130, 8);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(295, 8);
            this.groupBox2.TabIndex = 37;
            this.groupBox2.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 13);
            this.label2.TabIndex = 38;
            this.label2.Text = "Recent Files";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(28, 123);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(104, 13);
            this.label1.TabIndex = 61;
            this.label1.Text = "Initial display on load";
            // 
            // initialDisplayComboBox
            // 
            this.initialDisplayComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.initialDisplayComboBox.ItemHeight = 13;
            this.initialDisplayComboBox.Items.AddRange(new object[] {
            "Auto",
            "Expand",
            "Collapse",
            "HideTests"});
            this.initialDisplayComboBox.Location = new System.Drawing.Point(232, 123);
            this.initialDisplayComboBox.Name = "initialDisplayComboBox";
            this.initialDisplayComboBox.Size = new System.Drawing.Size(168, 21);
            this.initialDisplayComboBox.TabIndex = 62;
            // 
            // saveVisualStateCheckBox
            // 
            this.saveVisualStateCheckBox.AutoSize = true;
            this.saveVisualStateCheckBox.Location = new System.Drawing.Point(28, 203);
            this.saveVisualStateCheckBox.Name = "saveVisualStateCheckBox";
            this.saveVisualStateCheckBox.Size = new System.Drawing.Size(234, 17);
            this.saveVisualStateCheckBox.TabIndex = 63;
            this.saveVisualStateCheckBox.Text = "Restore Visual State of each project on load";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Location = new System.Drawing.Point(137, 87);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(291, 8);
            this.groupBox1.TabIndex = 64;
            this.groupBox1.TabStop = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(1, 87);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(55, 13);
            this.label5.TabIndex = 65;
            this.label5.Text = "Tree View";
            // 
            // GuiSettingsPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.initialDisplayComboBox);
            this.Controls.Add(this.saveVisualStateCheckBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.recentFilesCountTextBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.label2);
            this.Name = "GuiSettingsPage";
            this.Size = new System.Drawing.Size(428, 293);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox recentFilesCountTextBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox initialDisplayComboBox;
        private System.Windows.Forms.CheckBox saveVisualStateCheckBox;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label5;
    }
}
