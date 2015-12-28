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

    public abstract class TestGrouping : IEnumerable<TestGroup>
    {
        protected GroupDisplay _display;

        private Dictionary<string, TestGroup> _groupDictionary = new Dictionary<string, TestGroup>();
        protected List<TestGroup> _groupList = new List<TestGroup>();

        public TestGrouping(GroupDisplay display)
        {
            _display = display;
        }

        public TestGroup this[string name]
        {
            get { return _groupDictionary[name]; }
        }

        public TestGroup this[int index]
        {
            get { return _groupList[index]; }
        }

        public void AddGroup(string name)
        {
            AddGroup(name, -1);
        }

        public void AddGroup(string name, int imageIndex)
        {
            AddGroup(new TestGroup(name, imageIndex));
        }

        protected virtual void AddGroup(TestGroup group)
        {
            _groupDictionary.Add(group.Name, group);
            _groupList.Add(group);
        }

        private void ClearGroups()
        {
            _groupDictionary.Clear();
            _groupList.Clear();
        }

        public void Load(TestSelection selection)
        {
            ClearGroups();

            foreach (TestNode testNode in selection)
            {
                foreach (string groupName in SelectGroups(testNode))
                {
                    TestGroup group = null;
                    if (_groupDictionary.ContainsKey(groupName))
                        group = _groupDictionary[groupName];
                    else
                    {
                        group = new TestGroup(groupName);
                        AddGroup(group);
                    }
                    group.Add(testNode);
                }
            }
        }

        public string SelectSingleGroup(TestNode testNode)
        {
            var groups = SelectGroups(testNode);
            return groups.Length > 0 ? groups[0] : null;
        }

        public abstract string[] SelectGroups(TestNode testNode);

        public virtual void AdjustGroupsBasedOnTestResult(TestNode result)
        {
            // Base implementation does nothing
        }

        public IEnumerator<TestGroup> GetEnumerator()
        {
            return _groupList.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _groupList.GetEnumerator();
        }
    }
}
