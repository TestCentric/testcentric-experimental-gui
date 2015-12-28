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

using System.Text;
using System.Windows.Forms;

namespace NUnit.Gui.Presenters
{
    using Engine;
    using Model;

    /// <summary>
    /// A TestGroup is essentially a TestSelection with a
    /// name, display name and image index. It maintains
    /// a TreeNode property synchronized with the members
    /// of the group and implements ITestItem so that the
    /// settingsServiceServiceService can use it as the currently selected test.
    /// It can create a filter for running all the tests
    /// in the group.
    /// </summary>
    public class TestGroup : TestSelection, ITestItem
    {
        public TestGroup(string name) : this(name, -1) { }

        public TestGroup(string name, int imageIndex)
        {
            this.Name = name;
            this.ImageIndex = imageIndex;
            this.TreeNode = new TreeNode(DisplayName, imageIndex, imageIndex);
        }

        public string Name { get; private set; }

        public string DisplayName
        {
            get { return string.Format("{0} ({1})", Name, Count); }
        }

        public int ImageIndex { get; private set; }

        private TreeNode _treeNode;
        public TreeNode TreeNode
        {
            get
            {
                if (_treeNode != null)
                    _treeNode.Name = DisplayName; // Just refresh display name

                return _treeNode;
            }
            set { _treeNode = value; }
        }

        public TestFilter GetTestFilter()
        {
            StringBuilder sb = new StringBuilder("<filter><or>");

            foreach (TestNode test in this)
                sb.AppendFormat("<id>{0}</id>", test.Id);

            sb.Append("</or></filter>");

            return new TestFilter(sb.ToString());
        }
    }
}
