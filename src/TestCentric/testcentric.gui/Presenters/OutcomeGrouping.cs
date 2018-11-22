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
    using Views;

    /// <summary>
    /// OutcomeGrouping groups tests by outcome. The contents
    /// of the groups change during execution but the display
    /// icon remains the same.
    /// </summary>
    public class OutcomeGrouping : TestGrouping
    {
        #region Constructor

        public OutcomeGrouping(GroupDisplayStrategy display) : base(display)
        {
            // Predefine all TestGroups and TreeNodes
            Groups.Add(new TestGroup("Failed", TestTreeView.FailureIndex));
            Groups.Add(new TestGroup("Passed", TestTreeView.SuccessIndex));
            Groups.Add(new TestGroup("Ignored", TestTreeView.WarningIndex));
            Groups.Add(new TestGroup("Inconclusive", TestTreeView.InconclusiveIndex));
            Groups.Add(new TestGroup("Skipped", TestTreeView.SkippedIndex));
            Groups.Add(new TestGroup("Not Run", TestTreeView.InitIndex));
        }

        #endregion

        #region Overrides

        public override void Load(IEnumerable<TestNode> tests)
        {
            foreach (TestGroup group in Groups)
                group.Clear();

            base.Load(tests);
        }

        /// <summary>
        /// Post a test result to the tree, changing the treeNode
        /// color to reflect success or failure. Overridden here
        /// to allow for moving nodes from one group to another
        /// based on the result of running the test.
        /// </summary>
        public override void OnTestFinished(ResultNode result)
        {
            ChangeGroupsBasedOnTestResult(result, false);
        }

        protected override TestGroup[] SelectGroups(TestNode testNode)
        {
            return new TestGroup[] { SelectGroup(testNode) };
        }

        #endregion

        #region Helper Methods

        private TestGroup SelectGroup(TestNode testNode)
        {
            var result = testNode as ResultNode;
            if (result == null)
                result = _displayStrategy.GetResultForTest(testNode);

            if (result != null)
                switch (result.Outcome.Status)
                {
                    case TestStatus.Failed:
                        return Groups[0];
                    case TestStatus.Passed:
                        return Groups[1];
                    case TestStatus.Skipped:
                        return result.Outcome.Label == "Ignored" ? Groups[2] : Groups[4];
                    case TestStatus.Inconclusive:
                        return Groups[3];
                }

            return Groups[5]; // Not Run
        }

        #endregion
    }
}
