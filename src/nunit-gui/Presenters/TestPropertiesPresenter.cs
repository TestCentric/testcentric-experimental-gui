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
using System.IO;
using System.Text;

namespace NUnit.Gui.Presenters
{
    using Model;
    using Views;

    public class TestPropertiesPresenter
    {
        private readonly ITestPropertiesView _view;
        private readonly ITestModel _model;

        private int _maxY = 0;
        private int _nextY = 4;

        public TestPropertiesPresenter(ITestPropertiesView view, ITestModel model)
        {
            _view = view;
            _model = model;

            _view.Visible = false;

            WireUpEvents();
        }

        private void WireUpEvents()
        {
            _model.TestLoaded += (ea) => _view.Visible = true;
            _model.TestUnloaded += (ea) => _view.Visible = false;
            _model.RunFinished += (ea) => DisplayTestProperties();
            _model.SelectedTestChanged += (s, ea) => DisplayTestProperties();
            _view.DisplayHiddenPropertiesChanged += DisplayTestProperties;
        }

        private void DisplayTestProperties()
        {
            // TODO: Insert checks for errors in the XML

            var testItem = _model.SelectedTest;
            var testNode = testItem as TestNode;
            var resultNode = _model.GetResultForTest(testNode);

            _view.TestPanel.Visible = testNode != null;
            _view.ResultPanel.Visible = resultNode != null;

            // TODO: We should actually try to set the font for bold items
            // dynamically, since the global application font may be changed.
            if (testNode != null)
            {
                _view.SuspendLayout();

                _view.Header = testNode.Name;
                _view.TestType = GetTestType(testNode);
                _view.FullName = testNode.FullName;
                _view.Description = testNode.GetProperty("Description");
                _view.Categories = testNode.GetPropertyList("Category");
                _view.TestCount = testNode.TestCount.ToString();
                _view.RunState = testNode.RunState.ToString();
                _view.SkipReason = testNode.GetProperty("_SKIPREASON");

                var sb = new StringBuilder();
                foreach (string item in testNode.GetAllProperties(_view.DisplayHiddenProperties))
                {
                    if (sb.Length > 0)
                        sb.Append(Environment.NewLine);
                    sb.Append(item);
                }
                _view.Properties = sb.ToString();

                if (resultNode != null)
                {
                    _view.Outcome = resultNode.Outcome.ToString();

                    _view.ElapsedTime = resultNode.Duration.ToString("f3");
                    _view.AssertCount = resultNode.AssertCount.ToString();

                    // TODO: Check for message and stack trace in other cases than failure
                    var message = resultNode.Xml.SelectSingleNode("failure/message");
                    _view.Message = message != null ? message.InnerText : "";

                    var stackTrace = resultNode.Xml.SelectSingleNode("failure/stack-trace");
                    _view.StackTrace = stackTrace != null ? stackTrace.InnerText : "";
                }

                _view.ResumeLayout();
            }
            else if (testItem != null)
            {
                _view.Header = testItem.Name;
            }
        }

        public static string GetTestType(TestNode testNode)
        {
            if (testNode.RunState == RunState.NotRunnable && testNode.Type == "Assembly")
            {
                var fi = new FileInfo(testNode.FullName);
                string extension = fi.Extension.ToLower();
                if (extension != ".exe" && extension != ".dll")
                  return "Unknown";
            }
            return testNode.Type;
        }

        #region Helper Methods

        // Sometimes, the message may have leading blank lines and/or
        // may be longer than Windows really wants to display.
        private string TrimMessage(string message)
        {
            if (message != null)
            {
                if (message.Length > 64000)
                    message = message.Substring(0, 64000);

                int start = 0;
                for (int i = 0; i < message.Length; i++)
                {
                    switch (message[i])
                    {
                        case ' ':
                        case '\t':
                            break;
                        case '\r':
                        case '\n':
                            start = i + 1;
                            break;

                        default:
                            return start == 0 ? message : message.Substring(start);
                    }
                }
            }

            return message;
        }

        #endregion
    }
}
