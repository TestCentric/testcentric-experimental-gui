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

using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using NUnit.Engine;

namespace NUnit.Gui.Presenters
{
    using Model;
    using Views;
    using Views.SettingsPages;

    public class MainPresenter : System.IDisposable
    {
        IMainView _view;
        ITestModel _model;

        private Dictionary<string, TreeNode> _nodeIndex = new Dictionary<string, TreeNode>();

        #region Construction and Initialization

        public MainPresenter(IMainView view, ITestModel model)
        {
            _view = view;
            _model = model;

            InitializeMainMenu();
            WireUpEvents();
        }

        private void WireUpEvents()
        {
            // Model Events
            _model.TestLoaded += (ea) => InitializeMainMenu();
            _model.TestUnloaded += (ea) => InitializeMainMenu();
            _model.TestReloaded += (ea) => InitializeMainMenu();
            _model.RunStarting += (ea) => InitializeMainMenu();
            _model.RunFinished += (ea) => InitializeMainMenu();

            // View Events
            _view.Load += MainForm_Load;
            _view.FormClosing += MainForm_Closing;

            _view.NewProjectCommand.Execute += _model.NewProject;
            _view.OpenProjectCommand.Execute += OnOpenProjectCommand;
            _view.CloseCommand.Execute += _model.UnloadTests;
            _view.SaveCommand.Execute += _model.SaveProject;
            _view.SaveAsCommand.Execute += _model.SaveProject;
            _view.ReloadTestsCommand.Execute += OnReloadTestsCommand;
            _view.RecentProjectsMenu.Popup += RecentProjectsMenu_Popup;
            _view.SelectRuntimeMenu.Popup += SelectRuntimeMenu_Popup;

            _view.SettingsCommand.Execute += OpenSettingsDialogCommand_Execute;
            _view.AddinsCommand.Execute += () =>
                { MessageBox.Show("This will display the Addins Dialog", "Not Yet Implemented"); };

            _view.NUnitHelpCommand.Execute += () =>
                { MessageBox.Show("This will show Help", "Not Yet Implemented");  };
            _view.AboutNUnitCommand.Execute += () =>
                { MessageBox.Show("This will show the About Box", "Not Yet Implemented"); };
        }

        #endregion

        #region Handlers for Model Events

        private void InitializeMainMenu()
        {
            bool isTestRunning = _model.IsTestRunning;
            bool canCloseOrSave = _model.HasTests && !isTestRunning;

            // File Menu
            _view.NewProjectCommand.Enabled = !isTestRunning;
            _view.OpenProjectCommand.Enabled = !isTestRunning;
            _view.CloseCommand.Enabled = canCloseOrSave;
            _view.SaveCommand.Enabled = canCloseOrSave;
            _view.SaveAsCommand.Enabled = canCloseOrSave;
            _view.SaveResultsCommand.Enabled = canCloseOrSave && _model.HasResults;
            _view.ReloadTestsCommand.Enabled = canCloseOrSave;
            _view.SelectRuntimeMenu.Enabled = canCloseOrSave;
            _view.RecentProjectsMenu.Enabled = !isTestRunning;
            _view.ExitCommand.Enabled = true;

            // Project Menu
            _view.ProjectMenu.Enabled = _view.ProjectMenu.Visible = _model.HasTests;
        }

        #endregion

        #region Handlers for View Events

        private void MainForm_Load(object sender, System.EventArgs e)
        {
            var location = _model.Settings.Gui.MainForm.Location;
            var size = _model.Settings.Gui.MainForm.Size;
            if (size == Size.Empty)
                size = _view.Size;

            if (size.Width < 160) size.Width = 160;
            if (size.Height < 32) size.Height = 32;

            if (!IsVisiblePosition(location, size))
                location = new Point(0, 0);

            _view.Location = location;
            _view.Size = size;

            // Set to maximized if required
            if (_model.Settings.Gui.MainForm.Maximized)
                _view.WindowState = FormWindowState.Maximized;

            // Set the font to use
            _view.Font = _model.Settings.Gui.MainForm.Font;

            _model.OnStartup();
        }

        private void MainForm_Closing(object sender, FormClosingEventArgs ea)
        {
            var windowState = _view.WindowState;
            var location = _view.Location;
            var size = _view.Size;

            if (windowState == FormWindowState.Normal)
            {
                _model.Settings.Gui.MainForm.Location = location;
                _model.Settings.Gui.MainForm.Size = size;
                _model.Settings.Gui.MainForm.Maximized = false;

                //this.statusBar.SizingGrip = true;
            }
            else if (windowState == FormWindowState.Maximized)
            {
                _model.Settings.Gui.MainForm.Maximized = true;

                //this.statusBar.SizingGrip = false;
            }
        }

        #region Command Handlers

        private void OnOpenProjectCommand()
        {
            string[] files = _view.DialogManager.GetFilesToOpen();
            if (files.Length > 0)
                _model.LoadTests(files);
        }

        private void OnReloadTestsCommand()
        {
           _model.ReloadTests();
        }

        void OpenSettingsDialogCommand_Execute()
        {
            // The SettingsDialog has been ported from an older version of
            // NUnit and doesn't use an MVP structure. The dialog deals
            // directly with the model.
            using (var dialog = new SettingsDialog((Form)_view, _model.Settings))
            {
                dialog.ShowDialog();
            }
        }

        #endregion

        #region Menu Popup Handlers

        private void RecentProjectsMenu_Popup()
        {
            var dropDownItems = _view.RecentProjectsMenu.ToolStripItem.DropDownItems;
            dropDownItems.Clear();
            int num = 0;
            foreach (string entry in _model.RecentFiles.Entries)
            {
                var menuText = string.Format("{0} {1}", ++num, entry);
                var menuItem = new ToolStripMenuItem(menuText);
                menuItem.Click += (sender, ea) =>
                {
                    string path = ((ToolStripMenuItem)sender).Text.Substring(2);
                    _model.LoadTests(new [] {path});
                };
                dropDownItems.Add(menuItem);
            }
        }

        private void SelectRuntimeMenu_Popup()
        {
            var dropDownItems = _view.SelectRuntimeMenu.ToolStripItem.DropDownItems;
            dropDownItems.Clear();
            foreach (var runtime in _model.AvailableRuntimes)
                dropDownItems.Add(runtime.DisplayName);
        }

        #endregion

        #endregion


        // Ensure that the proposed window position intersects
        // at least one screen area.
        private static bool IsVisiblePosition(Point location, Size size)
        {
            Rectangle myArea = new Rectangle(location, size);
            bool intersect = false;
            foreach (System.Windows.Forms.Screen screen in System.Windows.Forms.Screen.AllScreens)
            {
                intersect |= myArea.IntersectsWith(screen.WorkingArea);
            }
            return intersect;
        }


        #region IDispose Implementation

        public void Dispose()
        {
        }

        #endregion
    }
}
