namespace NUnit.Gui.Views.SettingsPages
{
    partial class RuntimeSelectionSettingsPage
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
            this.runtimeSelectionCheckBox = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.SuspendLayout();
            // 
            // runtimeSelectionCheckBox
            // 
            this.runtimeSelectionCheckBox.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.runtimeSelectionCheckBox.Location = new System.Drawing.Point(29, 32);
            this.runtimeSelectionCheckBox.Name = "runtimeSelectionCheckBox";
            this.runtimeSelectionCheckBox.Size = new System.Drawing.Size(372, 48);
            this.runtimeSelectionCheckBox.TabIndex = 18;
            this.runtimeSelectionCheckBox.Text = "Select default runtime version based on target framework of test assembly";
            this.runtimeSelectionCheckBox.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(93, 13);
            this.label3.TabIndex = 17;
            this.label3.Text = "Runtime Selection";
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Location = new System.Drawing.Point(163, 9);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(266, 8);
            this.groupBox3.TabIndex = 16;
            this.groupBox3.TabStop = false;
            // 
            // RuntimeSelectionSettingsPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.runtimeSelectionCheckBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.groupBox3);
            this.Name = "RuntimeSelectionSettingsPage";
            this.Size = new System.Drawing.Size(442, 208);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox runtimeSelectionCheckBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox3;
    }
}
