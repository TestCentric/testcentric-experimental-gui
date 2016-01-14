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
using System.Xml;

namespace NUnit.Gui.Model
{
    /// <summary>
    /// Argument used for all test events
    /// </summary>
    public class TestEventArgs : EventArgs
    {
        public TestEventArgs(TestAction action)
        {
            this.Action = action;
        }

        public TestEventArgs(TestAction action, TestNode test)
        {
            if (test == null)
                throw new ArgumentNullException("test");

            this.Action = action;
            this.Test = test;
        }

        public TestEventArgs(TestAction action, ResultNode result)
        {
            if (result == null)
                throw new ArgumentNullException("result");

            this.Action = action;
            this.Result = result;
        }

        public TestEventArgs(TestAction action, int testCount)
        {
            this.Action = action;
            this.TestCount = testCount;
        }

        public TestAction Action { get; private set; }
        public TestNode Test { get; private set; }
        public ResultNode Result { get; private set; }
        public int TestCount { get; private set; }
        //public TestOutput TestOutput { get; private set; }
    }
}
