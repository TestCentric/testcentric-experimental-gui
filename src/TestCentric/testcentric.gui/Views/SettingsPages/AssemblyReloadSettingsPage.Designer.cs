namespace TestCentric.Gui.Views.SettingsPages
{
    partial class AssemblyReloadSettingsPage
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
            this.rerunOnChangeCheckBox = new System.Windows.Forms.CheckBox();
            this.reloadOnRunCheckBox = new System.Windows.Forms.CheckBox();
            this.reloadOnChangeCheckBox = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.SuspendLayout();
            // 
            // rerunOnChangeCheckBox
            // 
            this.rerunOnChangeCheckBox.AutoSize = true;
            this.rerunOnChangeCheckBox.Enabled = false;
            this.rerunOnChangeCheckBox.Location = new System.Drawing.Point(43, 92);
            this.rerunOnChangeCheckBox.Name = "rerunOnChangeCheckBox";
            this.rerunOnChangeCheckBox.Size = new System.Drawing.Size(120, 17);
            this.rerunOnChangeCheckBox.TabIndex = 18;
            this.rerunOnChangeCheckBox.Text = "Re-run last tests run";
            // 
            // reloadOnRunCheckBox
            // 
            this.reloadOnRunCheckBox.AutoSize = true;
            this.reloadOnRunCheckBox.Location = new System.Drawing.Point(19, 28);
            this.reloadOnRunCheckBox.Name = "reloadOnRunCheckBox";
            this.reloadOnRunCheckBox.Size = new System.Drawing.Size(158, 17);
            this.reloadOnRunCheckBox.TabIndex = 16;
            this.reloadOnRunCheckBox.Text = "Reload before each test run";
            // 
            // reloadOnChangeCheckBox
            // 
            this.reloadOnChangeCheckBox.AutoSize = true;
            this.reloadOnChangeCheckBox.Location = new System.Drawing.Point(19, 60);
            this.reloadOnChangeCheckBox.Name = "reloadOnChangeCheckBox";
            this.reloadOnChangeCheckBox.Size = new System.Drawing.Size(199, 17);
            this.reloadOnChangeCheckBox.TabIndex = 17;
            this.reloadOnChangeCheckBox.Text = "Reload when test assembly changes";
            this.reloadOnChangeCheckBox.CheckedChanged += new System.EventHandler(this.reloadOnChangeCheckBox_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 13);
            this.label1.TabIndex = 15;
            this.label1.Text = "Assembly Reload";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Location = new System.Drawing.Point(176, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(235, 8);
            this.groupBox1.TabIndex = 14;
            this.groupBox1.TabStop = false;
            // 
            // AssemblyReloadSettingsPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.rerunOnChangeCheckBox);
            this.Controls.Add(this.reloadOnRunCheckBox);
            this.Controls.Add(this.reloadOnChangeCheckBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox1);
            this.Name = "AssemblyReloadSettingsPage";
            this.Size = new System.Drawing.Size(414, 254);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox rerunOnChangeCheckBox;
        private System.Windows.Forms.CheckBox reloadOnRunCheckBox;
        private System.Windows.Forms.CheckBox reloadOnChangeCheckBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}
