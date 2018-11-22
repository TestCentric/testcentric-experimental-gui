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

namespace TestCentric.Gui.Views
{
    public class DialogManager : IDialogManager
    {
        #region IDialogManager Members

        public string[] GetFilesToOpen()
        {
            OpenFileDialog dlg = new OpenFileDialog();

            dlg.Title = "Open Project";
            dlg.Filter =
                "Projects & Assemblies(*.nunit,*.csproj,*.vbproj,*.vjsproj, *.vcproj,*.sln,*.dll,*.exe )|*.nunit;*.csproj;*.vjsproj;*.vbproj;*.vcproj;*.sln;*.dll;*.exe|" +
                "All Project Types (*.nunit,*.csproj,*.vbproj,*.vjsproj,*.vcproj,*.sln)|*.nunit;*.csproj;*.vjsproj;*.vbproj;*.vcproj;*.sln|" +
                "Test Projects (*.nunit)|*.nunit|" +
                "Solutions (*.sln)|*.sln|" +
                "C# Projects (*.csproj)|*.csproj|" +
                "J# Projects (*.vjsproj)|*.vjsproj|" +
                "VB Projects (*.vbproj)|*.vbproj|" +
                "C++ Projects (*.vcproj)|*.vcproj|" +
                "Assemblies (*.dll,*.exe)|*.dll;*.exe";
            //if (initialDirectory != null)
            //    dlg.InitialDirectory = initialDirectory;
            dlg.FilterIndex = 1;
            dlg.FileName = "";
            dlg.Multiselect = true;

            return dlg.ShowDialog() == DialogResult.OK
                ? dlg.FileNames
                : new string[0];
        }

        public string GetFileOpenPath(string filter)
        {
            OpenFileDialog dlg = new OpenFileDialog();

            dlg.Title = "Open Project";
            dlg.Filter = filter;
            //if (initialDirectory != null)
            //    dlg.InitialDirectory = initialDirectory;
            dlg.FilterIndex = 1;
            dlg.FileName = "";
            dlg.Multiselect = false;

            return dlg.ShowDialog() == DialogResult.OK
                ? dlg.FileNames[0]
                : null;
        }

        public string GetSaveAsPath(string filter)
        {
            SaveFileDialog dlg = new SaveFileDialog();

            dlg.Title = "Save Project";
            dlg.Filter = filter;
            dlg.FilterIndex = 1;
            dlg.FileName = "";

            return dlg.ShowDialog() == DialogResult.OK
                ? dlg.FileName
                : null;
        }

        public string GetFolderPath(string message, string initialPath)
        {
            FolderBrowserDialog browser = new FolderBrowserDialog();
            browser.Description = message;
            browser.SelectedPath = initialPath;
            return browser.ShowDialog() == DialogResult.OK
                ? browser.SelectedPath
                : null;
        }

        #endregion
    }
}
