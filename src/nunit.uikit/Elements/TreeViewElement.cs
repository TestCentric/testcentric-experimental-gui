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

using System.Collections.Generic;
using System.Windows.Forms;

namespace NUnit.UiKit.Elements
{
    /// <summary>
    /// TreeViewElement extends ControlElement for wrapping a TreeView.
    /// </summary>
    public class TreeViewElement : ControlElement<TreeView>, ITreeViewElement
    {
        public event TreeNodeActionHandler SelectedNodeChanged;

        public TreeViewElement(TreeView treeView)
            : base(treeView)
        {
            _checkBoxes = treeView.CheckBoxes;

            treeView.AfterSelect += (s, e) => 
            {
                if (SelectedNodeChanged != null)
                    SelectedNodeChanged(e.Node);
            };

            treeView.MouseDown += (s, e) =>
            {
                if (e.Button == MouseButtons.Right)
                {
                    ContextNode = treeView.GetNodeAt(e.X, e.Y);
                    if (ContextNode == null)
                        ContextNode = treeView.SelectedNode;
                    if (ContextNode == null)
                        ContextNode = treeView.Nodes[0];
                }
            };
        }

        private IContextMenuElement contextMenu;
        public IContextMenuElement ContextMenu
        {
            get 
            {
                if (contextMenu == null && Control.ContextMenuStrip != null)
                    contextMenu = new ContextMenuElement(Control.ContextMenuStrip);

                return contextMenu;
            }
            set 
            {
                InvokeIfRequired(() =>
                {
                    contextMenu = value;
                    Control.ContextMenuStrip = contextMenu.Control;
                });
            }
        }

        private bool _checkBoxes;
        public bool CheckBoxes
        {
            get { return _checkBoxes; }
            set { InvokeIfRequired(() => { Control.CheckBoxes = _checkBoxes = value; }); }
        }

        public int VisibleCount
        {
            get { return Control.VisibleCount; }
        }

        public TreeNode SelectedNode
        {
            get { return Control.SelectedNode; }
        }

        public TreeNode ContextNode { get; private set; }

        public void Clear()
        {
            InvokeIfRequired(() => { Control.Nodes.Clear(); });
        }

        public void ExpandAll()
        {
            InvokeIfRequired(() => { Control.ExpandAll(); });
        }

        public void CollapseAll()
        {
            InvokeIfRequired(() => { Control.CollapseAll(); });
        }

        public void Add(TreeNode treeNode)
        {
            Add(treeNode, false);
        }

        public void Load(TreeNode treeNode)
        {
            Add(treeNode, true);
        }

        public void SetImageIndex(TreeNode treeNode, int imageIndex)
        {
            InvokeIfRequired(() =>
            {
                treeNode.ImageIndex = treeNode.SelectedImageIndex = imageIndex;
            });
        }

        #region Helper Methods

        private void Add(TreeNode treeNode, bool doClear)
        {
            InvokeIfRequired(() =>
            {
                if (doClear)
                    Control.Nodes.Clear();

                Control.Nodes.Add(treeNode);
                if (Control.SelectedNode == null)
                    Control.SelectedNode = treeNode;
            });
        }

        #endregion
    }
}
