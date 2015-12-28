namespace NUnit.Gui.Views
{
    partial class ProgressBarView
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
            this.testProgressBar = new NUnit.UiKit.Controls.NUnitProgressBar();
            this.SuspendLayout();
            // 
            // testProgressBar
            // 
            this.testProgressBar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.testProgressBar.Location = new System.Drawing.Point(0, 0);
            this.testProgressBar.Name = "testProgressBar";
            this.testProgressBar.Size = new System.Drawing.Size(239, 14);
            this.testProgressBar.Status = NUnit.UiKit.Controls.TestProgressBarStatus.Success;
            this.testProgressBar.TabIndex = 0;
            // 
            // ProgressBarUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.testProgressBar);
            this.Name = "ProgressBarUserControl";
            this.Size = new System.Drawing.Size(239, 14);
            this.ResumeLayout(false);

        }

        #endregion

        private NUnit.UiKit.Controls.NUnitProgressBar testProgressBar;
    }
}
