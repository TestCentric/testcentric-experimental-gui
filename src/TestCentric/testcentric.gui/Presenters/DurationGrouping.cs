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

namespace TestCentric.Gui.Presenters
{
    using Model;

    /// <summary>
    /// DurationGrouping groups tests by duration. The contents
    /// of the groups change during execution and the display
    /// icon changes according to the test results.
    /// </summary>
    public class DurationGrouping : TestGrouping
    {
        public DurationGrouping(GroupDisplayStrategy displayStrategy) : base(displayStrategy)
        {
        }

        #region Overrides

        public override void Load(IEnumerable<TestNode> tests)
        {
            Groups.Clear();

            // Predefine all TestGroups and TreeNodes
            Groups.Add(new TestGroup("Slow > 1 sec"));
            Groups.Add(new TestGroup("Medium > 100 ms"));
            Groups.Add(new TestGroup("Fast < 100 ms"));
            Groups.Add(new TestGroup("Not Run"));

            base.Load(tests);

            if (_displayStrategy.HasResults)
                foreach (var group in Groups)
                    group.ImageIndex = _displayStrategy.CalcImageIndexForGroup(group);
        }

        /// <summary>
        /// Post a test result to the tree, changing the treeNode
        /// color to reflect success or failure. Overridden here
        /// to allow for moving nodes from one group to another
        /// based on the result of running the test.
        /// </summary>
        public override void OnTestFinished(ResultNode result)
        {
            ChangeGroupsBasedOnTestResult(result, true);
        }

        protected override TestGroup[] SelectGroups(TestNode testNode)
        {
            return new TestGroup[] { SelectGroup(testNode) };
        }

        #endregion

        #region Helper Methods

        private TestGroup SelectGroup(TestNode testNode)
        {
            var group = Groups[3]; // NotRun

            var result = testNode as ResultNode;
            if (result == null)
                result = _displayStrategy.GetResultForTest(testNode);

            if (result != null)
            {
                group = result.Duration > 1.0
                    ? Groups[0]
                    : result.Duration > 0.1
                        ? Groups[1]
                        : Groups[2];
            }

            return group;
        }

        #endregion
    }
}
