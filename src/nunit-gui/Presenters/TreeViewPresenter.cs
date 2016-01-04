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
using System.Collections.Generic;
using System.Windows.Forms;
using System.Xml;

namespace NUnit.Gui.Presenters
{
    using Engine;
    using Model;
    using Views;

    /// <summary>
    /// TreeViewPresenter is the presenter for the TestTreeView
    /// </summary>
    public class TreeViewPresenter
    {
        /// <summary>
        /// Image indices for various test states - the values 
        /// must match the indices of the image list used and
        /// are ordered so that the higher values are those
        /// that propogate upwards.
        /// </summary>
        public const int InitIndex = 0;
        public const int SkippedIndex = 0;
        public const int InconclusiveIndex = 1;
        public const int SuccessIndex = 2;
        public const int IgnoredIndex = 3;
        public const int FailureIndex = 4;

        private ITestTreeView _view;
        private ITestModel _model;

        private DisplayStrategy _display;
        private string _displayFormat;

        private Dictionary<string, TreeNode> _nodeIndex = new Dictionary<string, TreeNode>();

        public TreeViewPresenter(ITestTreeView treeView, ITestModel model)
        {
            _view = treeView;
            _model = model;

            // Create the initial display, which assists the presenter
            // both by providing public methods and by handling events.
            _displayFormat = _model.Settings.Gui.TestTree.DisplayFormat;
            _display = CreateDisplayStrategy(_displayFormat);

            InitializeRunCommands();
            WireUpEvents();
        }

        private void WireUpEvents()
        {
            _model.TestLoaded += (ea) => InitializeRunCommands();
            _model.TestUnloaded += (ea) => InitializeRunCommands();
            _model.RunStarting += (ea) => InitializeRunCommands();
            _model.RunFinished += (ea) => InitializeRunCommands();

            _view.Load += (s,e) => _view.DisplayFormat.SelectedItem = _displayFormat;

            // Run button and dropdowns
            _view.RunButton.Execute += () =>
            {
                // Necessary test because we don't disable the button click
                if (_model.HasTests && !_model.IsTestRunning)
                    _model.RunTests(TestFilter.Empty);
            };
            _view.RunAllCommand.Execute += () => _model.RunTests(TestFilter.Empty);
            _view.RunSelectedCommand.Execute += () => _model.RunSelectedTest();
            _view.RunFailedCommand.Execute += () => _model.RunTests(TestFilter.Empty); // NYI
            _view.StopRunCommand.Execute += () => _model.CancelTestRun();

            // Change of display format
            _view.DisplayFormat.SelectionChanged += () =>
            {
                _displayFormat = _view.DisplayFormat.SelectedItem;
                _model.Settings.Gui.TestTree.DisplayFormat = _displayFormat;

                // Replace the existing display, which functions as an 
                // adjunct to the presenter by handling certain events.
                _display = CreateDisplayStrategy(_displayFormat);

                _display.Reload();
            };
        }

        #region Helper Methods

        private void InitializeRunCommands()
        {
            bool isRunning = _model.IsTestRunning;
            bool canRun = _model.HasTests && !isRunning;

            // TODO: Figure out how to disable the button click but not the dropdown.
            //_view.RunButton.Enabled = canRun;
            _view.RunAllCommand.Enabled = canRun;
            _view.RunSelectedCommand.Enabled = canRun;
            _view.RunFailedCommand.Enabled = canRun;
            _view.StopRunCommand.Enabled = isRunning;
        }

        private DisplayStrategy CreateDisplayStrategy(string format)
        {
            switch (format.ToUpperInvariant())
            {
                default:
                case "NUNIT_TREE":
                    return new NUnitTreeDisplayStrategy(_view, _model);
                case "FIXTURE_LIST":
                    return new FixtureListDisplayStrategy(_view, _model);
                case "TEST_LIST":
                    return new TestListDisplayStrategy(_view, _model);
            }
        }

        #endregion
    }
}
