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
    public delegate void TreeNodeActionHandler(TreeNode treeNode);

    /// <summary>
    /// The ITreeViewElement interface provides additional methods
    /// used when wrapping a TreeView.
    /// </summary>
    public interface ITreeViewElement : IControlElement<TreeView>
    {
        event TreeNodeActionHandler SelectedNodeChanged;

        bool CheckBoxes { get; set; }
        int VisibleCount { get; }

        TreeNode SelectedNode { get; set; }
        IList<TreeNode> CheckedNodes { get; }

        IContextMenuElement ContextMenu { get; set; }

        void Clear();
        void ExpandAll();
        void CollapseAll();
        void Add(TreeNode treeNode);
        void Load(TreeNode treeNode);
        void SetImageIndex(TreeNode treeNode, int imageIndex);
    }
}
