namespace NUnit.Gui.Views
{
    partial class TestPropertiesView
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
            this.header = new System.Windows.Forms.Label();
            this.testTypeLabel = new System.Windows.Forms.Label();
            this.testType = new System.Windows.Forms.Label();
            this.fullNameLabel = new System.Windows.Forms.Label();
            this.descriptionLabel = new System.Windows.Forms.Label();
            this.categoriesLabel = new System.Windows.Forms.Label();
            this.categories = new System.Windows.Forms.Label();
            this.propertiesLabel = new System.Windows.Forms.Label();
            this.properties = new System.Windows.Forms.Label();
            this.reasonLabel = new System.Windows.Forms.Label();
            this.skipReason = new System.Windows.Forms.Label();
            this.displayHiddenProperties = new System.Windows.Forms.CheckBox();
            this.outcomeLabel = new System.Windows.Forms.Label();
            this.outcome = new System.Windows.Forms.Label();
            this.elapsedTimeLabel = new System.Windows.Forms.Label();
            this.elapsedTime = new System.Windows.Forms.Label();
            this.assertCountLabel = new System.Windows.Forms.Label();
            this.assertCount = new System.Windows.Forms.Label();
            this.messageLabel = new System.Windows.Forms.Label();
            this.stackTraceLabel = new System.Windows.Forms.Label();
            this.testCount = new System.Windows.Forms.Label();
            this.runStateLabel = new System.Windows.Forms.Label();
            this.testCountLabel = new System.Windows.Forms.Label();
            this.runState = new System.Windows.Forms.Label();
            this.stackTrace = new NUnit.UiKit.Controls.ExpandingLabel();
            this.message = new NUnit.UiKit.Controls.ExpandingLabel();
            this.fullName = new NUnit.UiKit.Controls.ExpandingLabel();
            this.description = new NUnit.UiKit.Controls.ExpandingLabel();
            this.resultPanel = new System.Windows.Forms.Panel();
            this.testPanel = new System.Windows.Forms.Panel();
            this.resultPanel.SuspendLayout();
            this.testPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // header
            // 
            this.header.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.header.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.4F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.header.Location = new System.Drawing.Point(3, 0);
            this.header.Name = "header";
            this.header.Size = new System.Drawing.Size(522, 22);
            this.header.TabIndex = 0;
            // 
            // testTypeLabel
            // 
            this.testTypeLabel.Location = new System.Drawing.Point(3, 0);
            this.testTypeLabel.Name = "testTypeLabel";
            this.testTypeLabel.Size = new System.Drawing.Size(58, 13);
            this.testTypeLabel.TabIndex = 1;
            this.testTypeLabel.Text = "Test Type:";
            // 
            // testType
            // 
            this.testType.Location = new System.Drawing.Point(72, 0);
            this.testType.Name = "testType";
            this.testType.Size = new System.Drawing.Size(117, 13);
            this.testType.TabIndex = 2;
            // 
            // fullNameLabel
            // 
            this.fullNameLabel.Location = new System.Drawing.Point(3, 17);
            this.fullNameLabel.Name = "fullNameLabel";
            this.fullNameLabel.Size = new System.Drawing.Size(57, 13);
            this.fullNameLabel.TabIndex = 3;
            this.fullNameLabel.Text = "Full Name:";
            // 
            // descriptionLabel
            // 
            this.descriptionLabel.Location = new System.Drawing.Point(3, 34);
            this.descriptionLabel.Name = "descriptionLabel";
            this.descriptionLabel.Size = new System.Drawing.Size(63, 13);
            this.descriptionLabel.TabIndex = 5;
            this.descriptionLabel.Text = "Description:";
            // 
            // categoriesLabel
            // 
            this.categoriesLabel.Location = new System.Drawing.Point(3, 52);
            this.categoriesLabel.Name = "categoriesLabel";
            this.categoriesLabel.Size = new System.Drawing.Size(60, 13);
            this.categoriesLabel.TabIndex = 7;
            this.categoriesLabel.Text = "Categories:";
            // 
            // categories
            // 
            this.categories.Location = new System.Drawing.Point(72, 52);
            this.categories.Name = "categories";
            this.categories.Size = new System.Drawing.Size(467, 13);
            this.categories.TabIndex = 8;
            // 
            // propertiesLabel
            // 
            this.propertiesLabel.Location = new System.Drawing.Point(3, 103);
            this.propertiesLabel.Name = "propertiesLabel";
            this.propertiesLabel.Padding = new System.Windows.Forms.Padding(0, 1, 0, 0);
            this.propertiesLabel.Size = new System.Drawing.Size(57, 14);
            this.propertiesLabel.TabIndex = 15;
            this.propertiesLabel.Text = "Properties:";
            // 
            // properties
            // 
            this.properties.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.properties.Location = new System.Drawing.Point(32, 124);
            this.properties.Name = "properties";
            this.properties.Size = new System.Drawing.Size(521, 56);
            this.properties.TabIndex = 17;
            // 
            // reasonLabel
            // 
            this.reasonLabel.Location = new System.Drawing.Point(3, 86);
            this.reasonLabel.Name = "reasonLabel";
            this.reasonLabel.Size = new System.Drawing.Size(47, 13);
            this.reasonLabel.TabIndex = 13;
            this.reasonLabel.Text = "Reason:";
            // 
            // skipReason
            // 
            this.skipReason.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.skipReason.Location = new System.Drawing.Point(72, 86);
            this.skipReason.Name = "skipReason";
            this.skipReason.Size = new System.Drawing.Size(467, 13);
            this.skipReason.TabIndex = 14;
            // 
            // displayHiddenProperties
            // 
            this.displayHiddenProperties.CheckAlign = System.Drawing.ContentAlignment.TopLeft;
            this.displayHiddenProperties.Location = new System.Drawing.Point(72, 102);
            this.displayHiddenProperties.Name = "displayHiddenProperties";
            this.displayHiddenProperties.Size = new System.Drawing.Size(157, 19);
            this.displayHiddenProperties.TabIndex = 16;
            this.displayHiddenProperties.Text = "Display hidden properties";
            this.displayHiddenProperties.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.displayHiddenProperties.UseVisualStyleBackColor = true;
            // 
            // outcomeLabel
            // 
            this.outcomeLabel.Location = new System.Drawing.Point(0, 0);
            this.outcomeLabel.Name = "outcomeLabel";
            this.outcomeLabel.Size = new System.Drawing.Size(53, 13);
            this.outcomeLabel.TabIndex = 18;
            this.outcomeLabel.Text = "Outcome:";
            // 
            // outcome
            // 
            this.outcome.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.outcome.Location = new System.Drawing.Point(81, 0);
            this.outcome.Name = "outcome";
            this.outcome.Size = new System.Drawing.Size(144, 13);
            this.outcome.TabIndex = 19;
            // 
            // elapsedTimeLabel
            // 
            this.elapsedTimeLabel.Location = new System.Drawing.Point(0, 17);
            this.elapsedTimeLabel.Name = "elapsedTimeLabel";
            this.elapsedTimeLabel.Size = new System.Drawing.Size(74, 13);
            this.elapsedTimeLabel.TabIndex = 21;
            this.elapsedTimeLabel.Text = "Elapsed Time:";
            // 
            // elapsedTime
            // 
            this.elapsedTime.Location = new System.Drawing.Point(81, 17);
            this.elapsedTime.Name = "elapsedTime";
            this.elapsedTime.Size = new System.Drawing.Size(67, 13);
            this.elapsedTime.TabIndex = 22;
            // 
            // assertCountLabel
            // 
            this.assertCountLabel.Location = new System.Drawing.Point(164, 17);
            this.assertCountLabel.Name = "assertCountLabel";
            this.assertCountLabel.Size = new System.Drawing.Size(44, 13);
            this.assertCountLabel.TabIndex = 23;
            this.assertCountLabel.Text = "Asserts:";
            // 
            // assertCount
            // 
            this.assertCount.Location = new System.Drawing.Point(231, 17);
            this.assertCount.Name = "assertCount";
            this.assertCount.Size = new System.Drawing.Size(49, 13);
            this.assertCount.TabIndex = 24;
            // 
            // messageLabel
            // 
            this.messageLabel.Location = new System.Drawing.Point(0, 34);
            this.messageLabel.Name = "messageLabel";
            this.messageLabel.Size = new System.Drawing.Size(53, 13);
            this.messageLabel.TabIndex = 25;
            this.messageLabel.Text = "Message:";
            // 
            // stackTraceLabel
            // 
            this.stackTraceLabel.Location = new System.Drawing.Point(0, 90);
            this.stackTraceLabel.Name = "stackTraceLabel";
            this.stackTraceLabel.Size = new System.Drawing.Size(69, 13);
            this.stackTraceLabel.TabIndex = 27;
            this.stackTraceLabel.Text = "Stack Trace:";
            // 
            // testCount
            // 
            this.testCount.Location = new System.Drawing.Point(72, 69);
            this.testCount.Name = "testCount";
            this.testCount.Size = new System.Drawing.Size(71, 13);
            this.testCount.TabIndex = 10;
            // 
            // runStateLabel
            // 
            this.runStateLabel.Location = new System.Drawing.Point(167, 69);
            this.runStateLabel.Name = "runStateLabel";
            this.runStateLabel.Size = new System.Drawing.Size(61, 13);
            this.runStateLabel.TabIndex = 11;
            this.runStateLabel.Text = "Run State: ";
            // 
            // testCountLabel
            // 
            this.testCountLabel.Location = new System.Drawing.Point(3, 69);
            this.testCountLabel.Name = "testCountLabel";
            this.testCountLabel.Size = new System.Drawing.Size(62, 13);
            this.testCountLabel.TabIndex = 9;
            this.testCountLabel.Text = "Test Count:";
            // 
            // runState
            // 
            this.runState.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.runState.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.runState.Location = new System.Drawing.Point(234, 69);
            this.runState.Name = "runState";
            this.runState.Size = new System.Drawing.Size(319, 13);
            this.runState.TabIndex = 12;
            // 
            // stackTrace
            // 
            this.stackTrace.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.stackTrace.Expansion = NUnit.UiKit.Controls.TipWindow.ExpansionStyle.Both;
            this.stackTrace.Location = new System.Drawing.Point(70, 90);
            this.stackTrace.Name = "stackTrace";
            this.stackTrace.Size = new System.Drawing.Size(483, 61);
            this.stackTrace.TabIndex = 28;
            // 
            // message
            // 
            this.message.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.message.Expansion = NUnit.UiKit.Controls.TipWindow.ExpansionStyle.Both;
            this.message.Location = new System.Drawing.Point(69, 34);
            this.message.Name = "message";
            this.message.Size = new System.Drawing.Size(484, 56);
            this.message.TabIndex = 26;
            // 
            // fullName
            // 
            this.fullName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.fullName.Location = new System.Drawing.Point(72, 17);
            this.fullName.Name = "fullName";
            this.fullName.Size = new System.Drawing.Size(481, 13);
            this.fullName.TabIndex = 4;
            // 
            // description
            // 
            this.description.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.description.AutoEllipsis = true;
            this.description.Location = new System.Drawing.Point(86, 34);
            this.description.Name = "description";
            this.description.Size = new System.Drawing.Size(467, 13);
            this.description.TabIndex = 6;
            // 
            // resultPanel
            // 
            this.resultPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.resultPanel.Controls.Add(this.elapsedTime);
            this.resultPanel.Controls.Add(this.outcomeLabel);
            this.resultPanel.Controls.Add(this.outcome);
            this.resultPanel.Controls.Add(this.stackTrace);
            this.resultPanel.Controls.Add(this.elapsedTimeLabel);
            this.resultPanel.Controls.Add(this.assertCountLabel);
            this.resultPanel.Controls.Add(this.stackTraceLabel);
            this.resultPanel.Controls.Add(this.assertCount);
            this.resultPanel.Controls.Add(this.messageLabel);
            this.resultPanel.Controls.Add(this.message);
            this.resultPanel.Location = new System.Drawing.Point(0, 219);
            this.resultPanel.Name = "resultPanel";
            this.resultPanel.Size = new System.Drawing.Size(553, 154);
            this.resultPanel.TabIndex = 29;
            // 
            // testPanel
            // 
            this.testPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.testPanel.Controls.Add(this.testTypeLabel);
            this.testPanel.Controls.Add(this.runState);
            this.testPanel.Controls.Add(this.testCountLabel);
            this.testPanel.Controls.Add(this.testCount);
            this.testPanel.Controls.Add(this.testType);
            this.testPanel.Controls.Add(this.runStateLabel);
            this.testPanel.Controls.Add(this.fullNameLabel);
            this.testPanel.Controls.Add(this.propertiesLabel);
            this.testPanel.Controls.Add(this.fullName);
            this.testPanel.Controls.Add(this.properties);
            this.testPanel.Controls.Add(this.descriptionLabel);
            this.testPanel.Controls.Add(this.reasonLabel);
            this.testPanel.Controls.Add(this.description);
            this.testPanel.Controls.Add(this.skipReason);
            this.testPanel.Controls.Add(this.categoriesLabel);
            this.testPanel.Controls.Add(this.displayHiddenProperties);
            this.testPanel.Controls.Add(this.categories);
            this.testPanel.Location = new System.Drawing.Point(0, 25);
            this.testPanel.Name = "testPanel";
            this.testPanel.Size = new System.Drawing.Size(553, 188);
            this.testPanel.TabIndex = 30;
            // 
            // TestPropertiesView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.testPanel);
            this.Controls.Add(this.resultPanel);
            this.Controls.Add(this.header);
            this.Name = "TestPropertiesView";
            this.Size = new System.Drawing.Size(556, 373);
            this.resultPanel.ResumeLayout(false);
            this.testPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label header;
        private System.Windows.Forms.Label testTypeLabel;
        private System.Windows.Forms.Label testType;
        private System.Windows.Forms.Label fullNameLabel;
        private NUnit.UiKit.Controls.ExpandingLabel fullName;
        private System.Windows.Forms.Label descriptionLabel;
        private NUnit.UiKit.Controls.ExpandingLabel description;
        private System.Windows.Forms.Label categoriesLabel;
        private System.Windows.Forms.Label categories;
        private System.Windows.Forms.Label propertiesLabel;
        private System.Windows.Forms.Label properties;
        private System.Windows.Forms.Label reasonLabel;
        private System.Windows.Forms.Label skipReason;
        private System.Windows.Forms.CheckBox displayHiddenProperties;
        private System.Windows.Forms.Label outcomeLabel;
        private System.Windows.Forms.Label outcome;
        private System.Windows.Forms.Label elapsedTimeLabel;
        private System.Windows.Forms.Label elapsedTime;
        private System.Windows.Forms.Label assertCountLabel;
        private System.Windows.Forms.Label assertCount;
        private System.Windows.Forms.Label messageLabel;
        private NUnit.UiKit.Controls.ExpandingLabel message;
        private System.Windows.Forms.Label stackTraceLabel;
        private NUnit.UiKit.Controls.ExpandingLabel stackTrace;
        private System.Windows.Forms.Label testCount;
        private System.Windows.Forms.Label runStateLabel;
        private System.Windows.Forms.Label testCountLabel;
        private System.Windows.Forms.Label runState;
        private System.Windows.Forms.Panel resultPanel;
        private System.Windows.Forms.Panel testPanel;
    }
}
