// ***********************************************************************
// Copyright (c) 2015 Charlie Poole
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

using TestCentric.Gui.Settings;

namespace TestCentric.Gui.Views.SettingsPages
{
    public partial class GuiSettingsPage : SettingsPage
    {
        public GuiSettingsPage(SettingsModel settings) : base("Gui.General", settings)
        {
            InitializeComponent();
        }

        public override void LoadSettings()
        {
            recentFilesCountTextBox.Text = Settings.Gui.RecentProjects.MaxFiles.ToString();
            initialDisplayComboBox.SelectedIndex = (int)Settings.Gui.TestTree.InitialTreeDisplay;
            saveVisualStateCheckBox.Checked = Settings.Gui.TestTree.SaveVisualState;
        }

        public override void ApplySettings()
        {
            Settings.Gui.TestTree.InitialTreeDisplay = (TreeDisplayStyle)initialDisplayComboBox.SelectedIndex;
            Settings.Gui.TestTree.SaveVisualState = saveVisualStateCheckBox.Checked;
        }

        private void recentFilesCountTextBox_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (recentFilesCountTextBox.Text.Length == 0)
            {
                recentFilesCountTextBox.Text = Settings.Gui.RecentProjects.MaxFiles.ToString();
                recentFilesCountTextBox.SelectAll();
                e.Cancel = true;
            }
            else
            {
                string errmsg = null;

                try
                {
                    int count = int.Parse(recentFilesCountTextBox.Text);

                    if (count < 0 || count > 24)
                    {
                        errmsg = string.Format("Number of files must be from 0 to 24");
                    }
                }
                catch
                {
                    errmsg = "Number of files must be numeric";
                }

                if (errmsg != null)
                {
                    recentFilesCountTextBox.SelectAll();
                    MessageDisplay.Error(errmsg);
                    e.Cancel = true;
                }
            }
        }

        private void recentFilesCountTextBox_Validated(object sender, System.EventArgs e)
        {
            int count = int.Parse(recentFilesCountTextBox.Text);
            Settings.Gui.RecentProjects.MaxFiles = count;
        }
    }
}
