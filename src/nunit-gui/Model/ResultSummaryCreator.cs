// ***********************************************************************
// Copyright (c) 2017 Charlie Poole
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
using System.Xml;

namespace NUnit.Gui.Model
{
    public class ResultSummaryCreator
    {
        public static ResultSummary FromResultNode(ResultNode resultNode)
        {
            var result = resultNode.Xml;

            if (result.Name != "test-run")
                throw new InvalidOperationException("Expected <test-run> as top-level element but was <" + result.Name + ">");

            var summary = new ResultSummary();
            summary.OverallResult = resultNode.Outcome.Status.ToString();
            summary.Duration = result.GetAttribute("duration", 0.0);
            summary.StartTime = result.GetAttribute("start-time", DateTime.MinValue);
            summary.EndTime =  result.GetAttribute("end-time", DateTime.MaxValue);
            Summarize(result, summary);
            return summary;
        }

        private static void Summarize(XmlNode node, ResultSummary summary)
        {
            string type = node.GetAttribute("type");
            string status = node.GetAttribute("result");
            string label = node.GetAttribute("label");

            switch (node.Name)
            {
                case "test-case":
                    summary.TestCount++;

                    SummarizeTestCase(summary, status, label);
                    break;

                case "test-suite":
                    SummarizeTestSuite(node.ChildNodes, summary, type, status, label);
                    break;

                case "test-run":
                    Summarize(node.ChildNodes, summary);
                    break;
            }
        }

        private static void SummarizeTestCase(ResultSummary summary, string status, string label)
        {
            switch (status)
            {
                case "Passed":
                    summary.PassCount++;
                    break;
                case "Failed":
                    if (label == null)
                        summary.FailureCount++;
                    else if (label == "Invalid")
                        summary.InvalidCount++;
                    else
                        summary.ErrorCount++;
                    break;
                case "Warning":
                    summary.WarningCount++;
                    break;
                case "Inconclusive":
                    summary.InconclusiveCount++;
                    break;
                case "Skipped":
                    if (label == "Ignored")
                        summary.IgnoreCount++;
                    else if (label == "Explicit")
                        summary.ExplicitCount++;
                    else
                        summary.SkipCount++;
                    break;
                default:
                    summary.SkipCount++;
                    break;
            }
        }

        private static void SummarizeTestSuite(XmlNodeList childNodes, ResultSummary summary, string type, string status, string label)
        {
            if (status == "Failed" && label == "Invalid")
            {
                if (type == "Assembly") summary.InvalidAssemblies++;
                else summary.InvalidTestFixtures++;
            }
            if (type == "Assembly" && status == "Failed" && label == "Error")
            {
                summary.InvalidAssemblies++;
                summary.UnexpectedError = true;
            }

            Summarize(childNodes, summary);
        }

        private static void Summarize(XmlNodeList nodes, ResultSummary summary)
        {
            foreach (XmlNode childResult in nodes)
                Summarize(childResult, summary);
        }
    }
}