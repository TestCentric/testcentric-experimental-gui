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
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace NUnit.Gui.Presenters
{
    using Model;
    using Views;

    public abstract class ResultGrouping : TestGrouping
    {
        public ResultGrouping(GroupDisplay display) : base(display) { }

        public override void AdjustGroupsBasedOnTestResult(TestNode result)
        {
            var treeNodes = _display.GetTreeNodesForTest(result);

            // Result may be for a TestNode not shown in the tree
            if (treeNodes.Count > 0)
            {
                // This implementation ignores any but the first node
                // since the adjustment of groups is currently only
                // made for groupings that display each node once.
                var treeNode = treeNodes[0];
                var oldParent = treeNode.Parent;
                var oldGroup = oldParent.Tag as TestGroup;
                var newGroupName = SelectSingleGroup(result);

                if (oldGroup.Name != newGroupName)
                {
                    var newGroup = this[newGroupName];
                    var newParent = newGroup.TreeNode;

                    oldGroup.RemoveId(result.Id);
                    // TODO: Insert in order
                    newGroup.Add(result);

                    _display.Tree.InvokeIfRequired(() =>
                    {
                        treeNode.Remove();
                        oldParent.Text = oldGroup.DisplayName;

                        // TODO: Superfluous?
                        //if (oldParent.Nodes.Count == 0)
                        //    oldParent.Remove();

                        newParent.Nodes.Add(treeNode);
                        newParent.Text = newGroup.DisplayName;
                        newParent.Expand();

                        // If we have added or removed a group, refresh display of all groups
                        if (oldGroup.Count == 0 || newGroup.Count == 1)
                        {
                            _display.Tree.Clear();
                            TreeNode topNode = null;
                            foreach (var group in this)
                            {
                                if (group.Count > 0)
                                {
                                    _display.Tree.Add(group.TreeNode);
                                    if (topNode == null)
                                        topNode = group.TreeNode;
                                }
                            }
                            if (topNode != null)
                                topNode.EnsureVisible();
                        }
                    });
                }
            }
        }
    }
}
