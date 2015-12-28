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
using System.Xml;
using NUnit.Engine;

namespace NUnit.Gui.Model
{
    public delegate string GroupingFunction(TestNode testNode);

    /// <summary>
    /// TestSelection is an extended List of TestNodes, with
    /// the addition of a SortBy method. Since the Gui is 
    /// designed to run under the .NET 2.0 runtime or later,
    /// we can't use the extension methods for List.
    /// </summary>
    public class TestSelection : List<TestNode>
    {
        //private List<TestNode> _selection = new List<TestNode>();

        //public void Add(TestNode result)
        //{
        //    _selection.Add(result);
        //}

        /// <summary>
        /// Remove the test node with a specific id from the selection
        /// </summary>
        /// <param name="id"></param>
        public void RemoveId(string id)
        {
            for(int index = 0; index < Count; index++)
                if (this[index].Id == id)
                {
                    RemoveAt(index);
                    break;
                }
        }

        public TestSelection SortBy(Comparison<TestNode> comparer)
        {
            Sort(comparer);
            return this;
        }

        public IDictionary<string, TestSelection> GroupBy(GroupingFunction groupingFunction)
        {
            var groups = new Dictionary<string, TestSelection>();

            foreach (TestNode testNode in this)
            {
                var groupName = groupingFunction(testNode);

                TestSelection group = null;
                if (!groups.ContainsKey(groupName))
                {
                    group = new TestSelection();
                    groups[groupName] = group;
                }
                else
                {
                    group = groups[groupName];
                }

                group.Add(testNode);
            }

            return groups;
        }

        //IEnumerator<TestNode> IEnumerable<TestNode>.GetEnumerator()
        //{
        //    return _selection.GetEnumerator();
        //}

        //IEnumerator IEnumerable.GetEnumerator()
        //{
        //    return _selection.GetEnumerator();
        //}
    }
}
