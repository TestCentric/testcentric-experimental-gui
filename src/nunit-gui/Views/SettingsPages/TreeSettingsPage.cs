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

using System;
using System.IO;
using System.Windows.Forms;

namespace NUnit.Gui.Views.SettingsPages
{
    using Model;
    using Model.Settings;

    public partial class TreeSettingsPage : SettingsPage
    {
        private static string treeImageDir;// = PathUtils.Combine(Assembly.GetExecutingAssembly(), "Images", "Tree");
        
        public TreeSettingsPage(SettingsModel settings) : base("Gui.Tree Display", settings)
        {
            InitializeComponent();
        }

        public override void LoadSettings()
        {
            initialDisplayComboBox.SelectedIndex = (int)Settings.Gui.TestTree.InitialTreeDisplay;
            clearResultsCheckBox.Checked = Settings.Gui.TestTree.ClearResultsOnReload;
            saveVisualStateCheckBox.Checked = Settings.Gui.TestTree.SaveVisualState;
            showCheckBoxesCheckBox.Checked = Settings.Gui.TestTree.ShowCheckBoxes;

            string[] altDirs = Directory.Exists(treeImageDir)
                ? Directory.GetDirectories(treeImageDir)
                : new string[0];

            foreach (string altDir in altDirs)
                imageSetListBox.Items.Add(Path.GetFileName(altDir));
            string imageSet = Settings.Gui.TestTree.AlternateImageSet;
            if (imageSetListBox.Items.Contains(imageSet))
                imageSetListBox.SelectedItem = imageSet;

            autoNamespaceSuites.Checked = Settings.Gui.TestTree.AutoNamespaceSuites;
            flatTestList.Checked = !autoNamespaceSuites.Checked;
        }

        public override void ApplySettings()
        {
            Settings.Gui.TestTree.InitialTreeDisplay = (TreeDisplayStyle)initialDisplayComboBox.SelectedIndex;
            Settings.Gui.TestTree.ClearResultsOnReload = clearResultsCheckBox.Checked;
            Settings.Gui.TestTree.SaveVisualState = saveVisualStateCheckBox.Checked;
            Settings.Gui.TestTree.ShowCheckBoxes = showCheckBoxesCheckBox.Checked;

            if (imageSetListBox.SelectedIndex >= 0)
                Settings.Gui.TestTree.AlternateImageSet = (string)imageSetListBox.SelectedItem;

            Settings.Gui.TestTree.AutoNamespaceSuites = autoNamespaceSuites.Checked;
        }

        private void toggleTestStructure(object sender, System.EventArgs e)
        {
            bool auto = autoNamespaceSuites.Checked = !autoNamespaceSuites.Checked;
            flatTestList.Checked = !auto;
        }

        public override bool HasChangesRequiringReload
        {
            get
            {
                return Settings.Gui.TestTree.AutoNamespaceSuites != autoNamespaceSuites.Checked;
            }
        }

        private void imageSetListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string imageSet = imageSetListBox.SelectedItem as string;

            if (imageSet != null)
                DisplayImageSet(imageSet);
        }

        private void DisplayImageSet(string imageSet)
        {
            string imageSetDir = Path.Combine(treeImageDir, imageSet);

            DisplayImage(imageSetDir, "Success", successImage);
            DisplayImage(imageSetDir, "Failure", failureImage);
            DisplayImage(imageSetDir, "Ignored", ignoredImage);
            DisplayImage(imageSetDir, "Inconclusive", inconclusiveImage);
            DisplayImage(imageSetDir, "Skipped", skippedImage);
        }

        private void DisplayImage(string imageDir, string filename, PictureBox box)
        {
            string[] extensions = { ".png", ".jpg" };

            foreach (string ext in extensions)
            {
                string filePath = Path.Combine(imageDir, filename + ext);
                if (File.Exists(filePath))
                {
                    box.Load(filePath);
                    break;
                }
            }
        }
    }
}
