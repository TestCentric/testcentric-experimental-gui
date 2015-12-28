namespace NUnit.Gui.Views
{
    partial class StatusBarView
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
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.StatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.testCountPanel = new System.Windows.Forms.ToolStripStatusLabel();
            this.testsRunPanel = new System.Windows.Forms.ToolStripStatusLabel();
            this.errorsPanel = new System.Windows.Forms.ToolStripStatusLabel();
            this.failuresPanel = new System.Windows.Forms.ToolStripStatusLabel();
            this.timePanel = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StatusLabel,
            this.testCountPanel,
            this.testsRunPanel,
            this.errorsPanel,
            this.failuresPanel,
            this.timePanel});
            this.statusStrip1.Location = new System.Drawing.Point(0, -1);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(579, 24);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // StatusLabel
            // 
            this.StatusLabel.Name = "StatusLabel";
            this.StatusLabel.Size = new System.Drawing.Size(444, 19);
            this.StatusLabel.Spring = true;
            this.StatusLabel.Text = "Status";
            this.StatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // testCountPanel
            // 
            this.testCountPanel.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.testCountPanel.BorderStyle = System.Windows.Forms.Border3DStyle.Etched;
            this.testCountPanel.Name = "testCountPanel";
            this.testCountPanel.Size = new System.Drawing.Size(81, 19);
            this.testCountPanel.Text = "Test Cases : 0";
            this.testCountPanel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.testCountPanel.Visible = false;
            // 
            // testsRunPanel
            // 
            this.testsRunPanel.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.testsRunPanel.BorderStyle = System.Windows.Forms.Border3DStyle.Etched;
            this.testsRunPanel.Name = "testsRunPanel";
            this.testsRunPanel.Size = new System.Drawing.Size(77, 19);
            this.testsRunPanel.Text = "Tests Run : 0";
            this.testsRunPanel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.testsRunPanel.Visible = false;
            // 
            // errorsPanel
            // 
            this.errorsPanel.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.errorsPanel.BorderStyle = System.Windows.Forms.Border3DStyle.Etched;
            this.errorsPanel.Name = "errorsPanel";
            this.errorsPanel.Size = new System.Drawing.Size(56, 19);
            this.errorsPanel.Text = "Errors : 0";
            this.errorsPanel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.errorsPanel.Visible = false;
            // 
            // failuresPanel
            // 
            this.failuresPanel.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.failuresPanel.BorderStyle = System.Windows.Forms.Border3DStyle.Etched;
            this.failuresPanel.Name = "failuresPanel";
            this.failuresPanel.Size = new System.Drawing.Size(66, 19);
            this.failuresPanel.Text = "Failures : 0";
            this.failuresPanel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.failuresPanel.Visible = false;
            // 
            // timePanel
            // 
            this.timePanel.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.timePanel.BorderStyle = System.Windows.Forms.Border3DStyle.Etched;
            this.timePanel.Name = "timePanel";
            this.timePanel.Size = new System.Drawing.Size(62, 19);
            this.timePanel.Text = "Time : 0.0";
            this.timePanel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.timePanel.Visible = false;
            // 
            // StatusBarView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.statusStrip1);
            this.Name = "StatusBarView";
            this.Size = new System.Drawing.Size(579, 23);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        public System.Windows.Forms.ToolStripStatusLabel StatusLabel;
        public System.Windows.Forms.ToolStripStatusLabel testCountPanel;
        public System.Windows.Forms.ToolStripStatusLabel testsRunPanel;
        public System.Windows.Forms.ToolStripStatusLabel errorsPanel;
        public System.Windows.Forms.ToolStripStatusLabel failuresPanel;
        private System.Windows.Forms.ToolStripStatusLabel timePanel;
    }
}
