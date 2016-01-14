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

using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace NUnit.Gui.Presenters
{
    using Views;
    using Model;

    /// <summary>
    /// FixtureListDisplayStrategy is used to display lists
    /// of fixtures grouped in various ways.
    /// </summary>
    public class FixtureListDisplayStrategy : GroupDisplayStrategy
    {
        #region Construction and Initialization

        public FixtureListDisplayStrategy(ITestTreeView view, ITestModel model) : base(view, model)
        {
            SetDefaultTestGrouping();
            _view.CollapseToFixturesCommand.Enabled = true;

            // Ugly Hack! We should not be referencing view components here.
            // TODO: Create a better inteface for a CheckedMenuGroup
            var checkedMenuGroup = _view.GroupBy as UiKit.Elements.CheckedMenuGroup;
            if (checkedMenuGroup != null)
                checkedMenuGroup.EnableItem("FIXTURE", false);
        }

        #endregion

        #region Public Members

        public override string Description
        {
            get { return "Fixtures By " + DefaultGroupSetting; }
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

                        foreach (TestNode fixture in GetTestFixtures(assembly))
                            treeNode.Nodes.Add(MakeTreeNode(fixture, true));

                        _view.Tree.Add(treeNode);
                        CollapseToFixtures(treeNode);
                    }
                    break;

                case "CATEGORY":
                case "OUTCOME":
                case "DURATION":
                    _grouping.Load(GetTestFixtures(testNode));

                    UpdateDisplay();

                    break;
            }
        }

        #endregion

        #region Protected Members

        protected override string DefaultGroupSetting
        {
            get { return _model.Settings.Gui.TestTree.FixtureList.GroupBy; }
            set { _model.Settings.Gui.TestTree.FixtureList.GroupBy = value; }
        }

        #endregion

        #region Private Members

        private TestSelection GetTestFixtures(TestNode testNode)
        {
            return testNode
                .Select((node) => node.Type == "TestFixture")
                .SortBy((x, y) => x.Name.CompareTo(y.Name));
        }

        #endregion
    }
}
