namespace NUnit.Gui.Views.SettingsPages
{
    partial class AssemblyIsolationSettingsPage
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
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.sameProcessRadioButton = new System.Windows.Forms.RadioButton();
            this.separateProcessRadioButton = new System.Windows.Forms.RadioButton();
            this.multiProcessRadioButton = new System.Windows.Forms.RadioButton();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.singleDomainRadioButton = new System.Windows.Forms.RadioButton();
            this.multiDomainRadioButton = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 128);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(114, 13);
            this.label2.TabIndex = 50;
            this.label2.Text = "Default Domain Usage";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Location = new System.Drawing.Point(186, 128);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(202, 8);
            this.groupBox2.TabIndex = 49;
            this.groupBox2.TabStop = false;
            // 
            // sameProcessRadioButton
            // 
            this.sameProcessRadioButton.AutoSize = true;
            this.sameProcessRadioButton.Checked = true;
            this.sameProcessRadioButton.Location = new System.Drawing.Point(19, 25);
            this.sameProcessRadioButton.Name = "sameProcessRadioButton";
            this.sameProcessRadioButton.Size = new System.Drawing.Size(205, 17);
            this.sameProcessRadioButton.TabIndex = 48;
            this.sameProcessRadioButton.TabStop = true;
            this.sameProcessRadioButton.Text = "Run tests directly in the NUnit process";
            this.sameProcessRadioButton.Click += new System.EventHandler(this.toggleProcessUsage);
            // 
            // separateProcessRadioButton
            // 
            this.separateProcessRadioButton.AutoSize = true;
            this.separateProcessRadioButton.Location = new System.Drawing.Point(19, 58);
            this.separateProcessRadioButton.Name = "separateProcessRadioButton";
            this.separateProcessRadioButton.Size = new System.Drawing.Size(204, 17);
            this.separateProcessRadioButton.TabIndex = 47;
            this.separateProcessRadioButton.Text = "Run tests in a single separate process";
            this.separateProcessRadioButton.Click += new System.EventHandler(this.toggleProcessUsage);
            // 
            // multiProcessRadioButton
            // 
            this.multiProcessRadioButton.AutoSize = true;
            this.multiProcessRadioButton.Location = new System.Drawing.Point(19, 91);
            this.multiProcessRadioButton.Name = "multiProcessRadioButton";
            this.multiProcessRadioButton.Size = new System.Drawing.Size(239, 17);
            this.multiProcessRadioButton.TabIndex = 46;
            this.multiProcessRadioButton.Text = "Run tests in a separate process per Assembly";
            this.multiProcessRadioButton.Click += new System.EventHandler(this.toggleProcessUsage);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(114, 13);
            this.label3.TabIndex = 45;
            this.label3.Text = "Default Process Model";
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Location = new System.Drawing.Point(186, 0);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(202, 8);
            this.groupBox3.TabIndex = 44;
            this.groupBox3.TabStop = false;
            // 
            // singleDomainRadioButton
            // 
            this.singleDomainRadioButton.AutoCheck = false;
            this.singleDomainRadioButton.AutoSize = true;
            this.singleDomainRadioButton.Checked = true;
            this.singleDomainRadioButton.Location = new System.Drawing.Point(19, 182);
            this.singleDomainRadioButton.Name = "singleDomainRadioButton";
            this.singleDomainRadioButton.Size = new System.Drawing.Size(194, 17);
            this.singleDomainRadioButton.TabIndex = 42;
            this.singleDomainRadioButton.TabStop = true;
            this.singleDomainRadioButton.Text = "Use a single AppDomain for all tests";
            this.singleDomainRadioButton.Click += new System.EventHandler(this.toggleMultiDomain);
            // 
            // multiDomainRadioButton
            // 
            this.multiDomainRadioButton.AutoCheck = false;
            this.multiDomainRadioButton.AutoSize = true;
            this.multiDomainRadioButton.Location = new System.Drawing.Point(19, 152);
            this.multiDomainRadioButton.Name = "multiDomainRadioButton";
            this.multiDomainRadioButton.Size = new System.Drawing.Size(220, 17);
            this.multiDomainRadioButton.TabIndex = 41;
            this.multiDomainRadioButton.Text = "Use a separate AppDomain per Assembly";
            this.multiDomainRadioButton.Click += new System.EventHandler(this.toggleMultiDomain);
            // 
            // AssemblyIsolationSettingsPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label2);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.sameProcessRadioButton);
            this.Controls.Add(this.separateProcessRadioButton);
            this.Controls.Add(this.multiProcessRadioButton);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.singleDomainRadioButton);
            this.Controls.Add(this.multiDomainRadioButton);
            this.Name = "AssemblyIsolationSettingsPage";
            this.Size = new System.Drawing.Size(391, 247);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton sameProcessRadioButton;
        private System.Windows.Forms.RadioButton separateProcessRadioButton;
        private System.Windows.Forms.RadioButton multiProcessRadioButton;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton singleDomainRadioButton;
        private System.Windows.Forms.RadioButton multiDomainRadioButton;
    }
}
