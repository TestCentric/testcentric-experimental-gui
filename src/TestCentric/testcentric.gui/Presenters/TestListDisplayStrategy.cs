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

namespace TestCentric.Gui.Presenters
{
    using Model;
    using Views;

    /// <summary>
    /// TestListDisplayStrategy is used to display lists
    /// of test cases grouped in various ways.
    /// </summary>
    public class TestListDisplayStrategy : GroupDisplayStrategy
    {
        #region Construction and Initialization

        public TestListDisplayStrategy(ITestTreeView view, ITestModel model) : base(view, model)
        {
            SetDefaultTestGrouping();
            _view.CollapseToFixturesCommand.Enabled = false;
        }

        #endregion

        #region Public Members

        public override string Description
        {
            get { return "Tests By " + DefaultGroupSetting; }
        }

        public override void OnTestLoaded(TestNode testNode)
        {
            ClearTree();

            switch (DefaultGroupSetting)
            {
                default:
                case "ASSEMBLY":
                    foreach (TestNode assembly in testNode
                        .Select((node) => node.IsSuite && node.Type == "Assembly"))
                    {
                        TreeNode treeNode = MakeTreeNode(assembly, false);

                        foreach (TestNode test in GetTestCases(assembly))
                            treeNode.Nodes.Add(MakeTreeNode(test, true));

                        _view.Tree.Add(treeNode);
                        treeNode.ExpandAll();
                    }
                    break;

                case "FIXTURE":
                    foreach (TestNode fixture in testNode
                        .Select((node) => node.IsSuite && node.Type == "TestFixture"))
                    {
                        TreeNode treeNode = MakeTreeNode(fixture, false);

                        foreach (TestNode test in GetTestCases(fixture))
                            treeNode.Nodes.Add(MakeTreeNode(test, true));

                        _view.Tree.Add(treeNode);
                        treeNode.ExpandAll();
                    }
                    break;

                case "CATEGORY":
                case "OUTCOME":
                case "DURATION":
                    _grouping.Load(GetTestCases(testNode));

                    UpdateDisplay();

                    break;
            }
        }

        #endregion

        #region Protected Members

        protected override string DefaultGroupSetting
        {
            get { return _settings.Gui.TestTree.TestList.GroupBy; }
            set { _settings.Gui.TestTree.TestList.GroupBy = value; }
        }

        #endregion

        #region Private Members

        private TestSelection GetTestCases(TestNode testNode)
        {
            return testNode
                .Select((n) => !n.IsSuite)
                .SortBy((x, y) => x.Name.CompareTo(y.Name));
        }

        #endregion
    }
}
