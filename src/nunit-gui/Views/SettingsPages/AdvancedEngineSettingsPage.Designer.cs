namespace NUnit.Gui.Views.SettingsPages
{
    partial class AdvancedEngineSettingsPage
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
            this.label1 = new System.Windows.Forms.Label();
            this.principalPolicyListBox = new System.Windows.Forms.ListBox();
            this.principalPolicyCheckBox = new System.Windows.Forms.CheckBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.shadowCopyPathTextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.enableShadowCopyCheckBox = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(37, 93);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 13);
            this.label1.TabIndex = 24;
            this.label1.Text = "Warning:";
            // 
            // principalPolicyListBox
            // 
            this.principalPolicyListBox.FormattingEnabled = true;
            this.principalPolicyListBox.Items.AddRange(new object[] {
            "UnauthenticatedPrincipal",
            "NoPrincipal",
            "WindowsPrincipal"});
            this.principalPolicyListBox.Location = new System.Drawing.Point(134, 217);
            this.principalPolicyListBox.Name = "principalPolicyListBox";
            this.principalPolicyListBox.Size = new System.Drawing.Size(241, 69);
            this.principalPolicyListBox.TabIndex = 23;
            // 
            // principalPolicyCheckBox
            // 
            this.principalPolicyCheckBox.AutoSize = true;
            this.principalPolicyCheckBox.Location = new System.Drawing.Point(19, 191);
            this.principalPolicyCheckBox.Name = "principalPolicyCheckBox";
            this.principalPolicyCheckBox.Size = new System.Drawing.Size(214, 17);
            this.principalPolicyCheckBox.TabIndex = 21;
            this.principalPolicyCheckBox.Text = "Set Principal Policy for test AppDomains";
            this.principalPolicyCheckBox.UseVisualStyleBackColor = true;
            this.principalPolicyCheckBox.CheckedChanged += new System.EventHandler(this.principalPolicyCheckBox_CheckedChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(37, 217);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(38, 13);
            this.label7.TabIndex = 22;
            this.label7.Text = "Policy:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(3, 155);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(78, 13);
            this.label6.TabIndex = 19;
            this.label6.Text = "Principal Policy";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Location = new System.Drawing.Point(134, 155);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(325, 8);
            this.groupBox1.TabIndex = 20;
            this.groupBox1.TabStop = false;
            // 
            // shadowCopyPathTextBox
            // 
            this.shadowCopyPathTextBox.Location = new System.Drawing.Point(134, 57);
            this.shadowCopyPathTextBox.Name = "shadowCopyPathTextBox";
            this.shadowCopyPathTextBox.Size = new System.Drawing.Size(325, 20);
            this.shadowCopyPathTextBox.TabIndex = 17;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(37, 58);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(66, 13);
            this.label4.TabIndex = 16;
            this.label4.Text = "Cache Path:";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(134, 93);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(260, 59);
            this.label2.TabIndex = 18;
            this.label2.Text = "Shadow copy should normally be enabled. If it is disabled, the NUnit Gui may not " +
    "function correctly.";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(73, 13);
            this.label3.TabIndex = 13;
            this.label3.Text = "Shadow Copy";
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Location = new System.Drawing.Point(134, 0);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(325, 8);
            this.groupBox3.TabIndex = 14;
            this.groupBox3.TabStop = false;
            // 
            // enableShadowCopyCheckBox
            // 
            this.enableShadowCopyCheckBox.AutoSize = true;
            this.enableShadowCopyCheckBox.Location = new System.Drawing.Point(19, 24);
            this.enableShadowCopyCheckBox.Name = "enableShadowCopyCheckBox";
            this.enableShadowCopyCheckBox.Size = new System.Drawing.Size(128, 17);
            this.enableShadowCopyCheckBox.TabIndex = 15;
            this.enableShadowCopyCheckBox.Text = "Enable Shadow Copy";
            this.enableShadowCopyCheckBox.CheckedChanged += new System.EventHandler(this.enableShadowCopyCheckBox_CheckedChanged);
            // 
            // AdvancedEngineSettingsPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.principalPolicyListBox);
            this.Controls.Add(this.principalPolicyCheckBox);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.shadowCopyPathTextBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.enableShadowCopyCheckBox);
            this.Name = "AdvancedEngineSettingsPage";
            this.Size = new System.Drawing.Size(462, 339);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox principalPolicyListBox;
        private System.Windows.Forms.CheckBox principalPolicyCheckBox;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox shadowCopyPathTextBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckBox enableShadowCopyCheckBox;
    }
}
