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

using System.Windows.Forms;
using NUnit.UiKit;
using NUnit.UiKit.Elements;

namespace NUnit.Gui.Views
{
    public partial class MainForm : Form, IMainView
    {
        public MainForm()
        {
            InitializeComponent();

            InitializeViewElements();
        }

        private void InitializeViewElements()
        {
            // File Menu
            FileMenu = new MenuElement(fileToolStripMenuItem);
            NewProjectCommand = new MenuElement(newProjectToolStripMenuItem);
            OpenProjectCommand = new MenuElement(openProjectToolStripMenuItem);
            CloseCommand = new MenuElement(closeToolStripMenuItem);
            SaveCommand = new MenuElement(saveToolStripMenuItem);
            SaveAsCommand = new MenuElement(saveAsToolStripMenuItem);
            SaveResultsCommand = new MenuElement(saveResultsToolStripMenuItem);
            ReloadTestsCommand = new MenuElement(reloadTestsToolStripMenuItem);
            SelectRuntimeMenu = new MenuElement(selectRuntimeToolStripMenuItem);
            SelectedRuntime = new CheckedMenuGroup(selectRuntimeToolStripMenuItem);
            ProcessModel = new CheckedMenuGroup("processModel",
                defaultProcessToolStripMenuItem, inProcessToolStripMenuItem, singleProcessToolStripMenuItem, multipleProcessToolStripMenuItem);
            DomainUsage = new CheckedMenuGroup("domainUsage",
                defaultDomainToolStripMenuItem, singleDomainToolStripMenuItem, multipleDomainToolStripMenuItem);
            RecentProjectsMenu = new MenuElement(recentProjectsToolStripMenuItem);
            ExitCommand = new MenuElement(exitToolStripMenuItem);

            // View Menu
            FullGuiCommand = new MenuElement(fullGuiToolStripMenuItem);
            MiniGuiCommand = new MenuElement(miniGuiToolStripMenuItem);
            GuiFontCommand = new MenuElement(guiFontToolStripMenuItem);
            FixedFontCommand = new MenuElement(fixedFontToolStripMenuItem);
            StatusBarCommand = new MenuElement(statusBarToolStripMenuItem);

            // Project Menu
            ProjectMenu = new MenuElement(projectToolStripMenuItem);

            // Tools Menu
            SettingsCommand = new MenuElement(settingsToolStripMenuItem);
            AddinsCommand = new MenuElement(addinsToolStripMenuItem);

            // Help Menu
            NUnitHelpCommand = new MenuElement(nUnitHelpToolStripMenuItem);
            AboutNUnitCommand = new MenuElement(aboutNUnitToolStripMenuItem);

            TestResult = new ControlElement<Label>(testResult);
            TestName = new ControlElement<Label>(testName);

            DialogManager = new DialogManager();
            MessageDisplay = new MessageDisplay();
        }

        #region Public Properties

        #region IMainView Members

        // File Menu
        public IMenu FileMenu { get; private set; }
        public ICommand NewProjectCommand { get; private set; }
        public ICommand OpenProjectCommand { get; private set; }
        public ICommand CloseCommand { get; private set; }
        public ICommand SaveCommand { get; private set; }
        public ICommand SaveAsCommand { get; private set; }
        public ICommand SaveResultsCommand { get; private set; }
        public ICommand ReloadTestsCommand { get; private set; }
        public IMenu SelectRuntimeMenu { get; private set; }
        public ISelection SelectedRuntime { get; private set; }
        public ISelection ProcessModel { get; private set; }
        public bool LoadAsX86
        {
            get { return loadAsX86ToolStripMenuItem.Checked; }
            set { loadAsX86ToolStripMenuItem.Checked = value; }
        }
        public ISelection DomainUsage { get; private set; }
        public IMenu RecentProjectsMenu { get; private set; }
        public ICommand ExitCommand { get; private set; }

        // View Menu
        public ICommand FullGuiCommand { get; private set; }
        public ICommand MiniGuiCommand { get; private set; }
        public ICommand GuiFontCommand { get; private set; }
        public ICommand FixedFontCommand { get; private set; }
        public ICommand StatusBarCommand { get; private set; }

        // Project Menu
        public IMenu ProjectMenu { get; private set; }

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

        #endregion

        #region Subordinate Views Contained in MainForm

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
    }
}
