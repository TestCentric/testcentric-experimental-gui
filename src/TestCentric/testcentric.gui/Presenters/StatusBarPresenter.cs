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
using System.Windows.Forms;

namespace TestCentric.Gui.Presenters
{
    using Model;
    using Views;

    /// <summary>
    /// TreeViewAdapter provides a higher-level interface to
    /// a TreeView control used to display tests.
    /// </summary>
    public class StatusBarPresenter
    {
        private IStatusBarView _view;
        private ITestModel _model;

        private Dictionary<string, TreeNode> _nodeIndex = new Dictionary<string, TreeNode>();

        public StatusBarPresenter(IStatusBarView view, ITestModel model)
        {
            _view = view;
            _model = model;

            WireUpEvents();
        }

        private void WireUpEvents()
        {
            _model.Events.TestLoaded += OnTestLoaded;
            _model.Events.TestReloaded += OnTestReloaded;
            _model.Events.TestUnloaded += OnTestUnloaded;
            _model.Events.RunStarting += OnRunStarting;
            _model.Events.RunFinished += OnRunFinished;
            _model.Events.TestStarting += OnTestStarting;
            _model.Events.TestFinished += OnTestFinished;
        }

        private void OnTestLoaded(TestNodeEventArgs ea)
        {
            _view.OnTestLoaded(ea.Test.TestCount);
        }

        private void OnTestReloaded(TestNodeEventArgs ea)
        {
            _view.OnTestLoaded(ea.Test.TestCount);
        }

        private void OnTestUnloaded(TestEventArgs ea)
        {
            _view.OnTestUnloaded();
        }

        private void OnRunStarting(RunStartingEventArgs ea)
        {
            _view.OnRunStarting(ea.TestCount);
        }

        private void OnRunFinished(TestResultEventArgs ea)
        {
            _view.OnRunFinished(ea.Result.Duration);
            var summary = ResultSummaryCreator.FromResultNode(ea.Result);
            _view.OnTestRunSummaryCompiled(ResultSummaryReporter.WriteSummaryReport(summary));
        }

        public void OnTestStarting(TestNodeEventArgs e)
        {
            _view.OnTestStarting(e.Test.Name);
        }

        private void OnTestFinished(TestResultEventArgs ea)
        {
            var result = ea.Result.Outcome;

            switch (result.Status)
            {
                case TestStatus.Passed:
                    _view.OnTestPassed();
                    break;
                case TestStatus.Failed:
                    _view.OnTestFailed();
                    break;
                case TestStatus.Warning:
                    _view.OnTestWarning();
                    break;
                case TestStatus.Inconclusive:
                    _view.OnTestInconclusive();
                    break;
            }
        }
    }
}
