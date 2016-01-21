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

using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Xml;

namespace NUnit.Gui.Presenters
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
            _model.TestLoaded += OnTestLoaded;
            _model.TestReloaded += OnTestReloaded;
            _model.TestUnloaded += OnTestUnloaded;
            _model.RunStarting += OnRunStarting;
            _model.RunFinished += OnRunFinished;
            _model.TestStarting += OnTestStarting;
            _model.TestFinished += OnTestFinished;
        }

        private void OnTestLoaded(TestNodeEventArgs ea)
        {
            _view.Initialize("Ready", ea.Test.TestCount);
        }

        private void OnTestReloaded(TestNodeEventArgs ea)
        {
            _view.Initialize("Reloaded", ea.Test.TestCount);
        }

        private void OnTestUnloaded(TestEventArgs ea)
        {
            _view.Initialize("Unloaded");
        }

        private void OnRunStarting(RunStartingEventArgs ea)
        {
            _view.RunStarting(ea.TestCount);
        }

        private void OnRunFinished(TestResultEventArgs ea)
        {
            _view.RunFinished(ea.Result.Duration);
        }

        public void OnTestStarting(TestNodeEventArgs e)
        {
            _view.SetStatus("Running : " + e.Test.Name);
        }

        private void OnTestFinished(TestResultEventArgs ea)
        {
            var result = ea.Result.Outcome;
            if (result.Status == TestStatus.Passed)
                _view.RecordSuccess();
            else if (result == ResultState.Failure)
                _view.RecordFailure();
            else if (result.Status == TestStatus.Failed)
                _view.RecordError();
        }
    }
}
