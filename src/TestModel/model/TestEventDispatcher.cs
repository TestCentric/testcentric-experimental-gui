// ***********************************************************************
// Copyright (c) 2018 Charlie Poole
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
using NUnit.Engine;

namespace TestCentric.Gui.Model
{
    public class TestEventDispatcher : ITestEvents, ITestEventListener
    {
        private TestModel _model;

        public TestEventDispatcher(TestModel model)
        {
            _model = model;
        }

        #region Public Methods to Fire Events

        public void FireTestsLoading(IList<string> files)
        {
            TestsLoading?.Invoke(new TestFilesLoadingEventArgs(files));
        }

        //public void FireTestsReloading()
        //{
        //    TestsReloading?.Invoke(new TestEventArgs());
        //}

        //public void FireTestsUnloading()
        //{
        //    TestsUnloading?.Invoke(new TestEventArgs());
        //}

        public void FireTestLoaded(TestNode testNode)
        {
            TestLoaded?.Invoke(new TestNodeEventArgs(TestAction.TestLoaded, testNode));
        }

        public void FireTestUnloaded()
        {
            TestUnloaded?.Invoke(new TestEventArgs(TestAction.TestUnloaded));
        }

        public void FireTestReloaded(TestNode testNode)
        {
            TestReloaded?.Invoke(new TestNodeEventArgs(TestAction.TestReloaded, testNode));
        }

        public void FireSelectedItemChanged(ITestItem testItem)
        {
            SelectedItemChanged?.Invoke(new TestItemEventArgs(testItem));
        }

        //public void FireCategorySelectionChanged()
        //{
        //    CategorySelectionChanged?.Invoke(new TestEventArgs());
        //}

        #endregion

        #region ITestEvents Implementation

        // Test loading events
        public event TestFilesLoadingEventHandler TestsLoading;
        public event TestEventHandler TestsReloading;
        public event TestEventHandler TestsUnloading;

        public event TestNodeEventHandler TestLoaded;
        public event TestNodeEventHandler TestReloaded;
        public event TestEventHandler TestUnloaded;

        // Test running events
        public event RunStartingEventHandler RunStarting;
        public event TestResultEventHandler RunFinished;

        public event TestNodeEventHandler SuiteStarting;
        public event TestResultEventHandler SuiteFinished;

        public event TestNodeEventHandler TestStarting;
        public event TestResultEventHandler TestFinished;

        public event TestOutputEventHandler TestOutput;

        // Test Selection Event
        public event TestItemEventHandler SelectedItemChanged;

        public event TestEventHandler CategorySelectionChanged;

        #endregion

        #region ITestEventListener Implementation

        public void OnTestEvent(string report)
        {
            XmlNode xmlNode = XmlHelper.CreateXmlNode(report);

            switch (xmlNode.Name)
            {
                case "start-test":
                    TestStarting?.Invoke(new TestNodeEventArgs(TestAction.TestStarting, new TestNode(xmlNode)));
                    break;

                case "start-suite":
                    SuiteStarting?.Invoke(new TestNodeEventArgs(TestAction.SuiteStarting, new TestNode(xmlNode)));
                    break;

                case "start-run":
                    RunStarting?.Invoke(new RunStartingEventArgs(xmlNode.GetAttribute("count", -1)));
                    break;

                case "test-case":
                    ResultNode result = new ResultNode(xmlNode);
                    _model.Results[result.Id] = result;
                    TestFinished?.Invoke(new TestResultEventArgs(TestAction.TestFinished, result));
                    break;

                case "test-suite":
                    result = new ResultNode(xmlNode);
                    _model.Results[result.Id] = result;
                    SuiteFinished?.Invoke(new TestResultEventArgs(TestAction.SuiteFinished, result));
                    break;

                case "test-run":
                    result = new ResultNode(xmlNode);
                    _model.Results[result.Id] = result;
                    RunFinished?.Invoke(new TestResultEventArgs(TestAction.RunFinished, result));
                    break;
            }
        }

        #endregion
    }
}
