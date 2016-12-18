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
using NUnit.UiKit.Elements;

namespace NUnit.Gui.Views
{
    public partial class TestTreeView : UserControl, ITestTreeView
    {
        /// <summary>
        /// Image indices for various test states - the values 
        /// must match the indices of the image list used and
        /// are ordered so that the higher values are those
        /// that propogate upwards.
        /// </summary>
        public const int InitIndex = 0;
        public const int SkippedIndex = 0;
        public const int InconclusiveIndex = 1;
        public const int SuccessIndex = 2;
        public const int WarningIndex = 3;
        public const int FailureIndex = 4;

        public TestTreeView()
        {
            InitializeComponent();

            RunButton = new SplitButtonElement(runButton);
            RunAllCommand = new MenuElement(runAllMenuItem);
            RunSelectedCommand = new MenuElement(runSelectedMenuItem);
            RunFailedCommand = new MenuElement(runFailedMenuItem);
            StopRunCommand = new MenuElement(stopRunMenuItem);

            FormatButton = new ToolStripElement<ToolStripDropDownButton>(formatButton);
            DisplayFormat = new CheckedMenuGroup(
                "displayFormat", 
                nunitTreeMenuItem, fixtureListMenuItem, testListMenuItem);
            GroupBy = new CheckedMenuGroup(
                "testGrouping", 
                byAssemblyMenuItem, byFixtureMenuItem, byCategoryMenuItem, byOutcomeMenuItem, byDurationMenuItem);

            RunContextCommand = new MenuElement(this.runMenuItem);
            RunCheckedCommand = new MenuElement(this.runCheckedMenuItem);
            ShowCheckBoxesCommand = new MenuElement(showCheckboxesMenuItem);
            ExpandAllCommand = new MenuElement(expandAllMenuItem);
            CollapseAllCommand = new MenuElement(collapseAllMenuItem);
            CollapseToFixturesCommand = new MenuElement(collapseToFixturesMenuItem);

            Tree = new TreeViewElement(treeView);
        }

        #region Properties

        public ICommand RunButton { get; private set; }
        public ICommand RunAllCommand { get; private set; }
        public ICommand RunSelectedCommand { get; private set; }
        public ICommand RunFailedCommand { get; private set; }
        public ICommand StopRunCommand { get; private set; }

        public ICommand RunContextCommand { get; private set; }
        public ICommand RunCheckedCommand { get; private set; }
        public IChecked ShowCheckBoxesCommand { get; private set; }
        public ICommand ExpandAllCommand { get; private set; }
        public ICommand CollapseAllCommand { get; private set; }
        public ICommand CollapseToFixturesCommand { get; private set; }

        public IToolStripElement<ToolStripDropDownButton> FormatButton { get; private set; }
        public ISelection DisplayFormat { get; private set; }
        public ISelection GroupBy { get; private set; }

        public ITreeViewElement Tree { get; private set; }

        #endregion

        #region Public Methods

        public void ExpandAll()
        {
            Tree.ExpandAll();
        }

        public void CollapseAll()
        {
            Tree.CollapseAll();
        }

        #endregion

        #region Helper Methods

        private void InvokeIfRequired(MethodInvoker _delegate)
        {
            if (treeView.InvokeRequired)
                treeView.Invoke(_delegate);
            else
                _delegate();
        }

        #endregion
    }
}
