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
using System.IO;
using System.Text;
using System.Xml;

namespace TestCentric.Gui.Presenters
{
    using Model;
    using Views;

    public class TestPropertiesPresenter
    {
        private readonly ITestPropertiesView _view;
        private readonly ITestModel _model;

        private ITestItem _selectedItem;

        public TestPropertiesPresenter(ITestPropertiesView view, ITestModel model)
        {
            _view = view;
            _model = model;

            _view.Visible = false;

            WireUpEvents();
        }

        private void WireUpEvents()
        {
            _model.Events.TestLoaded += (ea) => _view.Visible = true;
            _model.Events.TestReloaded += (ea) => _view.Visible = true;
            _model.Events.TestUnloaded += (ea) => _view.Visible = false;
            _model.Events.RunFinished += (ea) => DisplaySelectedItem();
            _model.Events.SelectedItemChanged += (ea) => OnSelectedItemChanged(ea.TestItem);
            _view.DisplayHiddenPropertiesChanged += () => DisplaySelectedItem();
        }

        private void OnSelectedItemChanged(ITestItem testItem)
        {
            _selectedItem = testItem;
            DisplaySelectedItem();
        }

        private void DisplaySelectedItem()
        {
            TestNode testNode = _selectedItem as TestNode;
            ResultNode resultNode = null;

            // TODO: Insert checks for errors in the XML
            if (_selectedItem != null)
            {
                _view.Header = _selectedItem.Name;

                if (testNode != null)
                {
                    _view.TestPanel.Visible = true;
                    _view.SuspendLayout();

                    DisplayTestInfo(testNode);

                    resultNode = _model.GetResultForTest(testNode.Id);
                    if (resultNode != null)
                        DisplayResultInfo(resultNode);

                    _view.ResumeLayout();
                }
            }

            _view.TestPanel.Visible = testNode != null;
            // HACK: results won't display on Linux otherwise
            if (Path.DirectorySeparatorChar == '/') // Running on Linux or Unix
                _view.ResultPanel.Visible = true;
            else
                _view.ResultPanel.Visible = resultNode != null;

            // TODO: We should actually try to set the font for bold items
            // dynamically, since the global application font may be changed.
        }

        private void DisplayTestInfo(TestNode testNode)
        {
            _view.TestType = GetTestType(testNode);
            _view.FullName = testNode.FullName;
            _view.Description = testNode.GetProperty("Description");
            _view.Categories = testNode.GetPropertyList("Category");
            _view.TestCount = testNode.TestCount.ToString();
            _view.RunState = testNode.RunState.ToString();
            _view.SkipReason = testNode.GetProperty("_SKIPREASON");

            DisplayTestProperties(testNode);
        }

        private void DisplayTestProperties(TestNode testNode)
        {
            var sb = new StringBuilder();
            foreach (string item in testNode.GetAllProperties(_view.DisplayHiddenProperties))
            {
                if (sb.Length > 0)
                    sb.Append(Environment.NewLine);
                sb.Append(item);
            }
            _view.Properties = sb.ToString();
        }

        private void DisplayResultInfo(ResultNode resultNode)
        {
            _view.Outcome = resultNode.Outcome.ToString();

            _view.ElapsedTime = resultNode.Duration.ToString("f3");
            _view.AssertCount = resultNode.AssertCount.ToString();

            DisplayAssertionResults(resultNode);
            DisplayOutput(resultNode);
        }

        private void DisplayAssertionResults(ResultNode resultNode)
        {
            StringBuilder sb;
            var assertionNodes = resultNode.Xml.SelectNodes("assertions/assertion");
            var assertionResults = new List<AssertionResult>();

            foreach (XmlNode assertion in assertionNodes)
                assertionResults.Add(new AssertionResult(assertion));

            // If there were no actual assertionresult entries, we fake
            // one if there is a message to display
            if (assertionResults.Count == 0)
            {
                if (resultNode.Outcome.Status == TestStatus.Failed)
                {
                    string status = resultNode.Outcome.Label ?? "Failed";
                    XmlNode failure = resultNode.Xml.SelectSingleNode("failure");
                    if (failure != null)
                        assertionResults.Add(new AssertionResult(failure, status));
                }
                else
                {
                    string status = resultNode.Outcome.Label ?? "Skipped";
                    XmlNode reason = resultNode.Xml.SelectSingleNode("reason");
                    if (reason != null)
                        assertionResults.Add(new AssertionResult(reason, status));
                }
            }

            sb = new StringBuilder();
            int index = 0;
            foreach (var assertion in assertionResults)
            {
                sb.AppendFormat("{0}) {1}\n", ++index, assertion.Status);
                sb.AppendLine(assertion.Message);
                if (assertion.StackTrace != null)
                    sb.AppendLine(AdjustStackTrace(assertion.StackTrace));

            }

            _view.Assertions = sb.ToString();
        }

        // Some versions of the framework return the stacktrace
        // without leading spaces, so we add them if needed.
        // TODO: Make sure this is valid across various cultures.
        private const string LEADING_SPACES = "   ";

        private static string AdjustStackTrace(string stackTrace)
        {
            // Check if no adjustment needed. We assume that all
            // lines start the same - either with or without spaces.
            if (stackTrace.StartsWith(LEADING_SPACES))
                return stackTrace;

            var sr = new StringReader(stackTrace);
            var sb = new StringBuilder();
            string line = sr.ReadLine();
            while (line != null)
            {
                sb.Append(LEADING_SPACES);
                sb.AppendLine(line);
                line = sr.ReadLine();
            }

            return sb.ToString();
        }

        private void DisplayOutput(ResultNode resultNode)
        {
            var output = resultNode.Xml.SelectSingleNode("output");
            _view.Output = output != null ? output.InnerText : "";
        }

        public static string GetTestType(TestNode testNode)
        {
            if (testNode.RunState == RunState.NotRunnable
                && testNode.Type == "Assembly"
                && !String.IsNullOrEmpty(testNode.FullName))
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

        #region Nested AssertionResult Class

        public struct AssertionResult
        {
            public AssertionResult(XmlNode assertion, string status)
                : this(assertion)
            {
                Status = status;
            }

            public AssertionResult(XmlNode assertion)
            {
                Status = assertion.GetAttribute("label") ?? assertion.GetAttribute("result");
                Message = assertion.SelectSingleNode("message")?.InnerText;
                StackTrace = assertion.SelectSingleNode("stack-trace")?.InnerText;
            }

            public string Status { get; }
            public string Message { get; }
            public string StackTrace { get; }
        }

        #endregion
    }
}
