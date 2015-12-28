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

    public class NUnitTreeDisplayStrategy : DisplayStrategy
    {
        #region Construction and Initialization

        public NUnitTreeDisplayStrategy(ITestTreeView view, ITestModel model) : base(view, model) 
        {
            _view.GroupBy.Enabled = false;
            _view.CollapseToFixturesCommand.Enabled = true;
        }

        //protected override void CreateContextMenu()
        //{
        //    base.CreateContextMenu();

        //    Tree.ContextMenu.Add(new ToolStripMenuItem("Collapse to Fixtures", null,
        //        (s, e) =>
        //        {
        //            foreach (TreeNode node in _view.Tree.Control.Nodes)
        //                CollapseToFixtures(node);
        //        }));
        //}

        #endregion

        protected override void Load(TestNode testNode)
        {
            ClearTree();

            foreach (var topLevelNode in testNode.Children)
            {
                var treeNode = MakeTreeNode(topLevelNode, true);

                _view.Tree.Add(treeNode);

                SetInitialExpansion(treeNode);
            }
        }

        private void SetInitialExpansion(TreeNode treeNode)
        {
            if (_view.Tree.VisibleCount >= treeNode.GetNodeCount(true))
                treeNode.ExpandAll();
            else
                CollapseToFixtures(treeNode);
        }
    }
}
