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
using System.Text;
using System.Xml;
using NUnit.Engine;

namespace NUnit.Gui.Model
{
    /// <summary>
    /// TestNode represents a single NUnit test result in the test model.
    /// </summary>
    public class ResultNode : TestNode
    {
        #region Construction and Initialization

        public ResultNode(XmlNode xmlNode) : base(xmlNode)
        {
            InitializeResultProperties();
        }

        public ResultNode(string xmlText) : base(xmlText)
        {
            InitializeResultProperties();
        }

        private void InitializeResultProperties()
        {
            Status = GetStatus();
            Label = GetAttribute("label");
            Outcome = new ResultState(Status, Label);
            AssertCount = GetAttribute("asserts", 0);
            var duration = GetAttribute("duration");
            Duration = duration != null
                ? double.Parse(duration)
                : 0.0;
        }

        #endregion

        #region Public Properties

        public TestStatus Status { get; private set; }
        public string Label { get; private set; }
        public ResultState Outcome { get; private set; }
        public int AssertCount { get; private set; }
        public double Duration { get; private set; }

        #endregion

        #region Helper Methods

        private TestStatus GetStatus()
        {
            string status = GetAttribute("result");
            switch (status)
            {
                case "Passed":
                default:
                    return TestStatus.Passed;
                case "Inconclusive":
                    return TestStatus.Inconclusive;
                case "Failed":
                    return TestStatus.Failed;
                case "Skipped":
                    return TestStatus.Skipped;
            }
        }

        #endregion
    }
}
