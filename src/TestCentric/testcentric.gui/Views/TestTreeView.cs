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
    using Elements;

    public partial class TestTreeView : UserControl, ITestTreeView
    {
        /// <summary>
        /// Image indices for various test states - the values 
        /// must match the indices of the image list used and
        /// are ordered so that the higher values are those
        /// that propagate upwards.
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
            RunAllCommand = new ToolStripMenuElement(runAllMenuItem);
            RunSelectedCommand = new ToolStripMenuElement(runSelectedMenuItem);
            RunFailedCommand = new ToolStripMenuElement(runFailedMenuItem);
            StopRunCommand = new ToolStripMenuElement(stopRunMenuItem);

            FormatButton = new ToolStripElement(formatButton);
            DisplayFormat = new CheckedMenuGroup(
                "displayFormat",
                nunitTreeMenuItem, fixtureListMenuItem, testListMenuItem);
            GroupBy = new CheckedMenuGroup(
                "testGrouping",
                byAssemblyMenuItem, byFixtureMenuItem, byCategoryMenuItem, byOutcomeMenuItem, byDurationMenuItem);

            RunContextCommand = new ToolStripMenuElement(this.runMenuItem);
            RunCheckedCommand = new ToolStripMenuElement(this.runCheckedMenuItem);
            ShowCheckBoxes = new ToolStripMenuElement(showCheckboxesMenuItem);
            ExpandAllCommand = new ToolStripMenuElement(expandAllMenuItem);
            CollapseAllCommand = new ToolStripMenuElement(collapseAllMenuItem);
            CollapseToFixturesCommand = new ToolStripMenuElement(collapseToFixturesMenuItem);

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
        public IChecked ShowCheckBoxes { get; private set; }
        public ICommand ExpandAllCommand { get; private set; }
        public ICommand CollapseAllCommand { get; private set; }
        public ICommand CollapseToFixturesCommand { get; private set; }

        public IToolTip FormatButton { get; private set; }
        public ISelection DisplayFormat { get; private set; }
        public ISelection GroupBy { get; private set; }

        public ITreeView Tree { get; private set; }

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
