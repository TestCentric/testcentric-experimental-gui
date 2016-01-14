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
    using Engine;
    using Model;
    using Views;

    /// <summary>
    /// TreeViewPresenter is the presenter for the TestTreeView
    /// </summary>
    public class TreeViewPresenter
    {
        private ITestTreeView _view;
        private ITestModel _model;

        private DisplayStrategy _strategy;

        private Dictionary<string, TreeNode> _nodeIndex = new Dictionary<string, TreeNode>();

        #region Constructor

        public TreeViewPresenter(ITestTreeView treeView, ITestModel model)
        {
            _view = treeView;
            _model = model;

            InitializeRunCommands();
            WireUpEvents();
        }

        #endregion

        #region Private Members

        private void WireUpEvents()
        {
            // Model actions
            _model.TestLoaded += (ea) =>
            {
                _strategy.OnTestLoaded(ea.Test);
                InitializeRunCommands();
            };

            _model.TestReloaded += (ea) =>
            {
                _strategy.OnTestLoaded(ea.Test);
                InitializeRunCommands();
            };

            _model.TestUnloaded += (ea) =>
            {
                _strategy.OnTestUnloaded();
                InitializeRunCommands();
            };

            _model.RunStarting += (ea) => InitializeRunCommands();
            _model.RunFinished += (ea) => InitializeRunCommands();

            _model.TestFinished += (ea) => _strategy.OnTestFinished(ea.Result);
            _model.SuiteFinished += (ea) => _strategy.OnTestFinished(ea.Result);

            // View actions - Initial Load
            _view.Load += (s, e) =>
            {
                SetDefaultDisplayStrategy();
            };

            // View context commands
            _view.CollapseAllCommand.Execute += () => _view.CollapseAll();
            _view.ExpandAllCommand.Execute += () => _view.ExpandAll();
            _view.CollapseToFixturesCommand.Execute += () => _strategy.CollapseToFixtures();
            _view.RunContextCommand.Execute += () => _model.RunTests(_view.Tree.ContextNode.Tag as ITestItem);

            // Node selected in tree
            _view.Tree.SelectedNodeChanged += (tn) => _model.SelectedTest = tn.Tag as ITestItem;

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
                SetDisplayStrategy(_view.DisplayFormat.SelectedItem);

                _strategy.Reload();
            };
        }

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

        private void SetDefaultDisplayStrategy()
        {
            CreateDisplayStrategy(Settings.DisplayFormat);
        }

        private void SetDisplayStrategy(string format)
        {
            CreateDisplayStrategy(format);
            Settings.DisplayFormat = format;
        }

        private void CreateDisplayStrategy(string format)
        {
            switch (format.ToUpperInvariant())
            {
                default:
                case "NUNIT_TREE":
                    _strategy = new NUnitTreeDisplayStrategy(_view, _model);
                    break;
                case "FIXTURE_LIST":
                    _strategy = new FixtureListDisplayStrategy(_view, _model);
                    break;
                case "TEST_LIST":
                    _strategy = new TestListDisplayStrategy(_view, _model);
                    break;
            }

            _view.FormatButton.ToolTipText = _strategy.Description;
            _view.DisplayFormat.SelectedItem = format;
        }

        private Model.Settings.TestTreeSettings Settings
        {
            get { return _model.Settings.Gui.TestTree; }
        }

        #endregion
    }
}
