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
using System.Windows.Forms;

namespace TestCentric.Gui.Presenters
{
    using Model;
    using Settings;
    using Views;
	using Elements;

    /// <summary>
    /// DisplayStrategy is the abstract base for the various
    /// strategies used to display tests in the tree control.
    /// It works primarily as a traditional strategy, with methods
    /// called by the TreeViewPresenter, but may also function
    /// as a presenter in it's own right, since it is created 
    /// with references to the view and mode.
    /// 
    /// We currently support three different strategies:
    /// NunitTreeDisplay, TestListDisplay and FixtureListDisplay.
    /// </summary>
    public abstract class DisplayStrategy
    {
        protected ITestTreeView _view;
        protected ITestModel _model;
        protected SettingsModel _settings;

        protected Dictionary<string, List<TreeNode>> _nodeIndex = new Dictionary<string, List<TreeNode>>();

        public ITreeView Tree { get; private set; }

        #region Construction and Initialization

        public DisplayStrategy(ITestTreeView view, ITestModel model)
        {
            _view = view;
            _model = model;
            _settings = new SettingsModel(_model.Services.UserSettings);

            this.Tree = view.Tree;
        }

        #endregion

        #region Properties

        public bool HasResults
        {
            get { return _model.HasResults; }
        }

        public abstract string Description { get; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Load all tests into the tree, starting from a root TestNode.
        /// </summary>
        public abstract void OnTestLoaded(TestNode testNode);

        public void OnTestUnloaded()
        {
            ClearTree();
        }

        public virtual void OnTestFinished(ResultNode result)
        {
            int imageIndex = CalcImageIndex(result.Outcome);
            foreach (TreeNode treeNode in GetTreeNodesForTest(result))
                Tree.SetImageIndex(treeNode, imageIndex);
        }

        // Called when either the display strategy or the grouping
        // changes. May need to distinguish these cases.
        public void Reload()
        {
            TestNode testNode = _model.Tests;
            if (testNode != null)
            {
                OnTestLoaded(testNode);

                if (Tree.Nodes != null) // TODO: Null when mocked
                    foreach (TreeNode treeNode in Tree.Nodes)
                        ApplyResultsToTree(treeNode);
            }
        }

        #endregion

        #region Helper Methods

        protected void ClearTree()
        {
            Tree.Clear();
            _nodeIndex.Clear();
        }

        protected TreeNode MakeTreeNode(TestGroup group, bool recursive)
        {
            TreeNode treeNode = new TreeNode(GroupDisplayName(group), group.ImageIndex, group.ImageIndex);
            treeNode.Tag = group;

            if (recursive)
                foreach (TestNode test in group)
                    treeNode.Nodes.Add(MakeTreeNode(test, true));

            return treeNode;
        }

        public TreeNode MakeTreeNode(TestNode testNode, bool recursive)
        {
            TreeNode treeNode = new TreeNode(testNode.Name);
            treeNode.Tag = testNode;

            int imageIndex = TestTreeView.SkippedIndex;

            switch (testNode.RunState)
            {
                case RunState.Ignored:
                    imageIndex = TestTreeView.WarningIndex;
                    break;
                case RunState.NotRunnable:
                    imageIndex = TestTreeView.FailureIndex;
                    break;
            }

            treeNode.ImageIndex = treeNode.SelectedImageIndex = imageIndex;

            string id = testNode.Id;
            if (_nodeIndex.ContainsKey(id))
                _nodeIndex[id].Add(treeNode);
            else
            {
                var list = new List<TreeNode>();
                list.Add(treeNode);
                _nodeIndex.Add(id, list);
            }

            if (recursive)
                foreach (TestNode childNode in testNode.Children)
                    treeNode.Nodes.Add(MakeTreeNode(childNode, true));

            return treeNode;
        }

        public string GroupDisplayName(TestGroup group)
        {
            return string.Format("{0} ({1})", group.Name, group.Count);
        }

        public static int CalcImageIndex(ResultState outcome)
        {
            switch (outcome.Status)
            {
                case TestStatus.Inconclusive:
                    return TestTreeView.InconclusiveIndex;
                case TestStatus.Passed:
                    return TestTreeView.SuccessIndex;
                case TestStatus.Failed:
                    return TestTreeView.FailureIndex;
                case TestStatus.Warning:
                    return TestTreeView.WarningIndex;
                case TestStatus.Skipped:
                default:
                    return outcome.Label == "Ignored"
                        ? TestTreeView.WarningIndex
                        : TestTreeView.SkippedIndex;
            }
        }

        private void ApplyResultsToTree(TreeNode treeNode)
        {
            TestNode testNode = treeNode.Tag as TestNode;

            if (testNode != null)
            {
                ResultNode resultNode = GetResultForTest(testNode);
                if (resultNode != null)
                    treeNode.ImageIndex = treeNode.SelectedImageIndex = CalcImageIndex(resultNode.Outcome);
            }

            foreach (TreeNode childNode in treeNode.Nodes)
                ApplyResultsToTree(childNode);
        }

        public void CollapseToFixtures()
        {
            if (_view.Tree.Nodes != null) // TODO: Null when mocked
                foreach (TreeNode treeNode in _view.Tree.Nodes)
                    CollapseToFixtures(treeNode);
        }

        protected void CollapseToFixtures(TreeNode treeNode)
        {
            var testNode = treeNode.Tag as TestNode;
            if (testNode != null && testNode.Type == "TestFixture")
                treeNode.Collapse();
            else if (testNode == null || testNode.IsSuite)
            {
                treeNode.Expand();
                foreach (TreeNode child in treeNode.Nodes)
                    CollapseToFixtures(child);
            }
        }

        public List<TreeNode> GetTreeNodesForTest(TestNode testNode)
        {
            List<TreeNode> treeNodes;
            if (!_nodeIndex.TryGetValue(testNode.Id, out treeNodes))
                treeNodes = new List<TreeNode>();

            return treeNodes;
        }

        public ResultNode GetResultForTest(TestNode testNode)
        {
            return _model.GetResultForTest(testNode.Id);
        }

        #endregion
    }
}
