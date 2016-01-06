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
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace NUnit.Gui.Presenters
{
    using Views;
    using Model;
    using Engine;
    using NUnit.UiKit.Elements;

    /// <summary>
    /// DisplayStrategy is the abstract base for the various
    /// strategies used to display tests in the tree control.
    /// It works both as a traditional strategy, with methods
    /// called by the TreeViewPresenter, and as a presenter
    /// in it's own right, since it is created with references
    /// to the view and model and can handle certain events.
    /// We currently support three different strategies:
    /// NunitTreeDisplay, TestListDisplay and FixtureListDisplay.
    /// </summary>
    public abstract class DisplayStrategy
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
        public const int IgnoredIndex = 3;
        public const int FailureIndex = 4;

        protected ITestTreeView _view;
        protected ITestModel _model;

        protected Dictionary<string, List<TreeNode>> _nodeIndex = new Dictionary<string, List<TreeNode>>();

        public ITreeViewElement Tree { get; private set; }

        #region Construction and Initialization

        public DisplayStrategy(ITestTreeView view, ITestModel model)
        {
            _view = view;
            _model = model;

            this.Tree = view.Tree;
            
            //CreateContextMenu();

            WireUpEvents();
        }

        private void WireUpEvents()
        {
            // Model actions
            _model.TestLoaded += (ea) => Load(ea.Test);
            _model.TestUnloaded += (ea) => ClearTree();
<<<<<<< HEAD
            _model.TestReloaded += (ea) => { ClearTree(); Load(ea.Test); };
            _model.TestFinished += (ea) => OnTestFinished(ea.Test);
            _model.SuiteFinished += (ea) => OnTestFinished(ea.Test);
=======
            _model.TestFinished += (ea) => OnTestFinished(ea.Result);
            _model.SuiteFinished += (ea) => OnTestFinished(ea.Result);
>>>>>>> refs/remotes/nunit/master

            // View actions
            _view.CollapseAllCommand.Execute += () => _view.CollapseAll();
            _view.ExpandAllCommand.Execute += () => _view.ExpandAll();
            _view.CollapseToFixturesCommand.Execute += () => CollapseToFixtures();
            _view.RunContextCommand.Execute += () => _model.RunTests(_view.Tree.ContextNode.Tag as ITestItem);

            // Node selected in tree
            Tree.SelectedNodeChanged += (tn) => _model.SelectedTest = tn.Tag as ITestItem;
        }

        #endregion

        #region Properties

        public bool HasResults
        {
            get { return _model.HasResults; }
        }

        public abstract string Description { get; }

        #endregion

        #region Protected Methods

        /// <summary>
        /// Load all tests into the tree, starting from a root TestNode.
        /// </summary>
        protected abstract void Load(TestNode testNode);

        protected virtual void OnTestFinished(ResultNode result)
        {
            int imageIndex = CalcImageIndex(result.Outcome);
            foreach(TreeNode treeNode in GetTreeNodesForTest(result))
                Tree.SetImageIndex(treeNode, imageIndex);
        }

        #endregion

        #region Public Methods

        // Called when either the display strategy or the grouping
        // changes. May need to distinguish these cases.
        public void Reload()
        {
            TestNode testNode = _model.Tests;
            if (testNode != null)
            {
                Load(testNode);

                TreeView treeControl = Tree.Control;

                if (treeControl != null) // TODO: Null when mocking - fix this
                    foreach (TreeNode treeNode in treeControl.Nodes)
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

            int imageIndex = SkippedIndex;

            switch (testNode.RunState)
            {
                case RunState.Ignored:
                    imageIndex = IgnoredIndex;
                    break;
                case RunState.NotRunnable:
                    imageIndex = FailureIndex;
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

        public int CalcImageIndex(ResultState outcome)
        {
            switch (outcome.Status)
            {
                case TestStatus.Inconclusive:
                    return InconclusiveIndex;
                case TestStatus.Passed:
                    return SuccessIndex;
                case TestStatus.Failed:
                    return FailureIndex;
                case TestStatus.Skipped:
                default:
                    return outcome.Label == "Ignored"
                        ? IgnoredIndex
                        : SkippedIndex;
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

        protected void CollapseToFixtures()
        {
            TreeView treeControl = _view.Tree.Control;
            if (treeControl != null) // TODO: Null when mocking - fix this
                foreach (TreeNode treeNode in _view.Tree.Control.Nodes)
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
            return _model.GetResultForTest(testNode);
        }

        #endregion
    }
}
