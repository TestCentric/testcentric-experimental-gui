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
    /// name and image index for use in the tree display.
    /// Its TreeNode property is externally set and updated.
    /// It can create a filter for running all the tests
    /// in the group.
    /// </summary>
    public class TestGroup : TestSelection, ITestItem
    {
        #region Constructors

        public TestGroup(string name) : this(name, -1) { }

        public TestGroup(string name, int imageIndex)
        {
            Name = name;
            ImageIndex = imageIndex;
        }

        #endregion

        #region Properties

        public string Name { get; private set; }

        public int ImageIndex { get; set; }

        public TreeNode TreeNode { get; set;  }

        #endregion

        public TestFilter GetTestFilter()
        {
            StringBuilder sb = new StringBuilder("<filter><or>");

            foreach (TestNode test in this)
                if (test.RunState != RunState.Explicit)
                    sb.AppendFormat("<id>{0}</id>", test.Id);

            sb.Append("</or></filter>");

            return new TestFilter(sb.ToString());
        }
    }
}
