namespace TestCentric.Gui.Views
{
    public partial class XmlView
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
      this.components = new System.ComponentModel.Container();
      this.header = new System.Windows.Forms.Label();
      this.xmlPanel = new System.Windows.Forms.Panel();
      this.xmlTextBox = new System.Windows.Forms.RichTextBox();
      this.xmlTextBoxContextMenuS = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.selectAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.wordWrapToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.xmlPanel.SuspendLayout();
      this.xmlTextBoxContextMenuS.SuspendLayout();
      this.SuspendLayout();
      // 
      // header
      // 
      this.header.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.header.BackColor = System.Drawing.SystemColors.Window;
      this.header.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.4F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.header.Location = new System.Drawing.Point(0, 0);
      this.header.Margin = new System.Windows.Forms.Padding(0);
      this.header.Name = "header";
      this.header.Padding = new System.Windows.Forms.Padding(6, 0, 0, 0);
      this.header.Size = new System.Drawing.Size(535, 28);
      this.header.TabIndex = 0;
      // 
      // xmlPanel
      // 
      this.xmlPanel.Controls.Add(this.xmlTextBox);
      this.xmlPanel.Dock = System.Windows.Forms.DockStyle.Fill;
      this.xmlPanel.Location = new System.Drawing.Point(0, 0);
      this.xmlPanel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.xmlPanel.Name = "xmlPanel";
      this.xmlPanel.Padding = new System.Windows.Forms.Padding(0, 33, 0, 0);
      this.xmlPanel.Size = new System.Drawing.Size(539, 665);
      this.xmlPanel.TabIndex = 30;
      // 
      // xmlTextBox
      // 
      this.xmlTextBox.ContextMenuStrip = this.xmlTextBoxContextMenuS;
      this.xmlTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
      this.xmlTextBox.Font = new System.Drawing.Font("Courier New", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.xmlTextBox.Location = new System.Drawing.Point(0, 33);
      this.xmlTextBox.Name = "xmlTextBox";
      this.xmlTextBox.ReadOnly = true;
      this.xmlTextBox.Size = new System.Drawing.Size(539, 632);
      this.xmlTextBox.TabIndex = 0;
      this.xmlTextBox.Text = "";
      this.xmlTextBox.WordWrap = false;
      // 
      // xmlTextBoxContextMenuS
      // 
      this.xmlTextBoxContextMenuS.ImageScalingSize = new System.Drawing.Size(24, 24);
      this.xmlTextBoxContextMenuS.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.selectAllToolStripMenuItem,
            this.copyToolStripMenuItem,
            this.wordWrapToolStripMenuItem});
      this.xmlTextBoxContextMenuS.Name = "xmlTextBoxContextMenuS";
      this.xmlTextBoxContextMenuS.Size = new System.Drawing.Size(212, 127);
      // 
      // selectAllToolStripMenuItem
      // 
      this.selectAllToolStripMenuItem.Name = "selectAllToolStripMenuItem";
      this.selectAllToolStripMenuItem.Size = new System.Drawing.Size(211, 30);
      this.selectAllToolStripMenuItem.Text = "Select All";
      // 
      // copyToolStripMenuItem
      // 
      this.copyToolStripMenuItem.Enabled = false;
      this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
      this.copyToolStripMenuItem.Size = new System.Drawing.Size(211, 30);
      this.copyToolStripMenuItem.Text = "Copy";
      // 
      // wordWrapToolStripMenuItem
      // 
      this.wordWrapToolStripMenuItem.CheckOnClick = true;
      this.wordWrapToolStripMenuItem.Name = "wordWrapToolStripMenuItem";
      this.wordWrapToolStripMenuItem.Size = new System.Drawing.Size(211, 30);
      this.wordWrapToolStripMenuItem.Text = "Word Wrap";
      // 
      // XmlView
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.AutoScroll = true;
      this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
      this.Controls.Add(this.header);
      this.Controls.Add(this.xmlPanel);
      this.Name = "XmlView";
      this.Size = new System.Drawing.Size(539, 665);
      this.xmlPanel.ResumeLayout(false);
      this.xmlTextBoxContextMenuS.ResumeLayout(false);
      this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label header;
        private System.Windows.Forms.Panel xmlPanel;
        private System.Windows.Forms.RichTextBox xmlTextBox;
    private System.Windows.Forms.ContextMenuStrip xmlTextBoxContextMenuS;
    private System.Windows.Forms.ToolStripMenuItem selectAllToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem wordWrapToolStripMenuItem;
  }
}