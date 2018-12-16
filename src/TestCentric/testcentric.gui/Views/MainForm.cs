// ***********************************************************************
// Copyright (c) 2016 Charlie Poole
//
// Permission is hereby granted, free of charge, to any person obtaining
// a copy of this software and associated documentation files (the
// "Software"), to deal in the Software without restriction, including
// without limitation the rights to use, copy, modify, merge, publish,
// distribute, sublicense, and/or sell copies of the Software, and to
// permit persons to whom the Software is furnished to do so, subject to
// the following conditions:
// 
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
// OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
// WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
// ***********************************************************************

using System.ComponentModel;
using System.Windows.Forms;
using TestCentric.Gui.Controls;

namespace TestCentric.Gui.Views
{
    using Elements;

    public partial class MainForm : Form, IMainView
    {
        public event CommandHandler MainViewClosing;
        public event DragEventHandler DragDropFiles;

        public MainForm()
        {
            InitializeComponent();

            InitializeViewElements();
        }

        private void InitializeViewElements()
        {
            // File Menu
            FileMenu = new ToolStripMenuElement(fileToolStripMenuItem);
            NewProjectCommand = new ToolStripMenuElement(newProjectToolStripMenuItem);
            OpenProjectCommand = new ToolStripMenuElement(openProjectToolStripMenuItem);
            CloseCommand = new ToolStripMenuElement(closeToolStripMenuItem);
            SaveCommand = new ToolStripMenuElement(saveToolStripMenuItem);
            SaveAsCommand = new ToolStripMenuElement(saveAsToolStripMenuItem);
            SaveResultsCommand = new ToolStripMenuElement(saveResultsToolStripMenuItem);
            ReloadTestsCommand = new ToolStripMenuElement(reloadTestsToolStripMenuItem);
            SelectRuntimeMenu = new ToolStripMenuElement(selectRuntimeToolStripMenuItem);
            SelectedRuntime = new CheckedMenuGroup(selectRuntimeToolStripMenuItem);
            ProcessModel = new CheckedMenuGroup("processModel",
                defaultProcessToolStripMenuItem, inProcessToolStripMenuItem, singleProcessToolStripMenuItem, multipleProcessToolStripMenuItem);
            DomainUsage = new CheckedMenuGroup("domainUsage",
                defaultDomainToolStripMenuItem, singleDomainToolStripMenuItem, multipleDomainToolStripMenuItem);
            RunAsX86 = new ToolStripMenuElement(loadAsX86ToolStripMenuItem);
            RecentProjectsMenu = new ToolStripMenuElement(recentProjectsToolStripMenuItem);
            ExitCommand = new ToolStripMenuElement(exitToolStripMenuItem);

            // View Menu
            FullGuiCommand = new ToolStripMenuElement(fullGuiToolStripMenuItem);
            MiniGuiCommand = new ToolStripMenuElement(miniGuiToolStripMenuItem);
            GuiFontCommand = new ToolStripMenuElement(guiFontToolStripMenuItem);
            FixedFontCommand = new ToolStripMenuElement(fixedFontToolStripMenuItem);
            StatusBarCommand = new ToolStripMenuElement(statusBarToolStripMenuItem);

            // Project Menu
            ProjectMenu = new ToolStripMenuElement(projectToolStripMenuItem);

            // Tools Menu
            SettingsCommand = new ToolStripMenuElement(settingsToolStripMenuItem);
            AddinsCommand = new ToolStripMenuElement(addinsToolStripMenuItem);

            // Help Menu
            NUnitHelpCommand = new ToolStripMenuElement(nUnitHelpToolStripMenuItem);
            AboutNUnitCommand = new ToolStripMenuElement(aboutNUnitToolStripMenuItem);

            TestResult = new ControlElement(testResult);
            TestName = new ControlElement(testName);

            DialogManager = new DialogManager();
            MessageDisplay = new MessageDisplay();
        }

        #region Public Properties

        #region IMainView Members

        public bool IsMaximized
        {
            get { return WindowState == FormWindowState.Maximized; }
            set { WindowState = value ? FormWindowState.Maximized : FormWindowState.Normal; }
        }

        // File Menu
        public IToolStripMenu FileMenu { get; private set; }
        public ICommand NewProjectCommand { get; private set; }
        public ICommand OpenProjectCommand { get; private set; }
        public ICommand CloseCommand { get; private set; }
        public ICommand SaveCommand { get; private set; }
        public ICommand SaveAsCommand { get; private set; }
        public ICommand SaveResultsCommand { get; private set; }
        public ICommand ReloadTestsCommand { get; private set; }
        public IToolStripMenu SelectRuntimeMenu { get; private set; }
        public ISelection SelectedRuntime { get; private set; }
        public ISelection ProcessModel { get; private set; }
        public IChecked RunAsX86 { get; private set; }
        public ISelection DomainUsage { get; private set; }
        public IToolStripMenu RecentProjectsMenu { get; private set; }
        public ICommand ExitCommand { get; private set; }

        // View Menu
        public ICommand FullGuiCommand { get; private set; }
        public ICommand MiniGuiCommand { get; private set; }
        public ICommand GuiFontCommand { get; private set; }
        public ICommand FixedFontCommand { get; private set; }
        public ICommand StatusBarCommand { get; private set; }

        // Project Menu
        public IToolStripMenu ProjectMenu { get; private set; }

        // Tools Menu
        public ICommand SettingsCommand { get; private set; }
        public ICommand AddinsCommand { get; private set; }

        // Help Menu
        public ICommand NUnitHelpCommand { get; private set; }
        public ICommand AboutNUnitCommand { get; private set; }

        public IViewElement TestResult { get; private set; }
        public IViewElement TestName { get; private set; }

        public IDialogManager DialogManager { get; private set; }
        public IMessageDisplay MessageDisplay { get; private set; }

        public void OnTestAssembliesLoading(string message)
        {
            _messageForm = new TestCentric.Gui.Controls.LongRunningOperationDisplay(this, message);
        }

        public void OnTestAssembliesLoaded()
        {
            _messageForm?.Dispose();
            _messageForm = null;
        }

        private LongRunningOperationDisplay _messageForm;

        #endregion

        #region Subordinate Views Contained in MainForm

        // NOTE that these are available to the form as actual classes rather than interfaces.

        public TestTreeView TestTreeView { get { return testTreeView; } }
        public ProgressBarView ProgressBarView { get { return progressBarView; } }
        public StatusBarView StatusBarView { get { return statusBarView; } }
        public TestPropertiesView PropertiesView { get { return propertiesView; } }
        public XmlView XmlView { get { return xmlView; } }

        #endregion

        #endregion

        private void statusBarToolStripMenuItem_CheckedChanged(object sender, System.EventArgs e)
        {
            statusBarView.Visible = statusBarToolStripMenuItem.Checked;
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            MainViewClosing?.Invoke();
            base.OnClosing(e);
        }

        protected override void OnDragEnter(DragEventArgs drgevent)
        {
            if (drgevent.Data.GetDataPresent(DataFormats.FileDrop))
            {
                drgevent.Effect = DragDropEffects.Copy;
            }
        }

        protected override void OnDragDrop(DragEventArgs drgevent)
        {
            base.OnDragDrop(drgevent);

            string[] files = (string[])drgevent.Data.GetData(DataFormats.FileDrop);
            if (files != null)
                DragDropFiles?.Invoke(files);
        }

        private void TabControlSelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (tabControl1.SelectedTab.Equals(tabPage2))
                XmlView.InvokeFocus();
        }
    }
}
