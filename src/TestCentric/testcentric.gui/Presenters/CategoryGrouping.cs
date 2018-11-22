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
using System.Xml;

namespace TestCentric.Gui.Presenters
{
    using Model;
    using Views;

    /// <summary>
    /// CategoryGrouping groups tests by category. A single
    /// test may fall into more than one group. The contents
    /// of the groups are stable once loaded but the
    /// icon changes according to the test results.
    /// </summary>
    public class CategoryGrouping : TestGrouping
    {
        public CategoryGrouping(GroupDisplayStrategy display) : base(display)
        {
        }

        #region Overrides

        public override void Load(IEnumerable<TestNode> tests)
        {
            Groups.Clear();
            Groups.Add(new TestGroup("None"));
            // Additional groups are added dynamically.

            base.Load(tests);

            if (_displayStrategy.HasResults)
                foreach (var group in Groups)
                    group.ImageIndex = _displayStrategy.CalcImageIndexForGroup(group);
        }

        public override void OnTestFinished(ResultNode result)
        {
            var imageIndex = DisplayStrategy.CalcImageIndex(result.Outcome);
            if (imageIndex >= TestTreeView.SuccessIndex)
            {
                var treeNodes = _displayStrategy.GetTreeNodesForTest(result);
                foreach (var treeNode in treeNodes)
                {
                    var parentNode = treeNode.Parent;
                    if (parentNode != null)
                    {
                        var group = parentNode.Tag as TestGroup;
                        if (group != null && imageIndex > group.ImageIndex)
                        {
                            parentNode.SelectedImageIndex = parentNode.ImageIndex = group.ImageIndex = imageIndex;
                        }
                    }
                }
            }
        }

        protected override TestGroup[] SelectGroups(TestNode testNode)
        {
            List<TestGroup> groups = new List<TestGroup>();

            foreach (XmlNode node in testNode.Xml.SelectNodes("properties/property[@name='Category']"))
            {
                var groupName = node.Attributes["value"].Value;
                var group = Groups.Find((g) => g.Name == groupName);//GetGroup(groupName);
                if (group == null)
                {
                    group = new TestGroup(groupName);
                    Groups.Add(group);
                }

                groups.Add(group);
            }

            if (groups.Count == 0)
                groups.Add(Groups[0]);

            return groups.ToArray();
        }

        #endregion

        #region Helper Methods

        private void AddGroup(TestGroup group)
        {
            Groups.Add(group);

            Groups.Sort((x, y) =>
            {
                bool xNone = x.Name == "None";
                bool yNone = y.Name == "None";

                if (xNone && yNone) return 0;

                if (xNone) return 1;

                if (yNone) return -1;

                return x.Name.CompareTo(y.Name);
            });
        }

        #endregion
    }
}
