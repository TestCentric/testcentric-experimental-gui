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

using NSubstitute;
using NUnit.Framework;
using System.Xml;

namespace NUnit.Gui.Presenters.Main
{
    using Model;
    using Views;

    public class WhenTestCaseCompletes : MainPresenterTestBase
    {
        [SetUp]
        public void SimulateTestCaseCompletion()
        {
            var result = new ResultNode(XmlHelper.CreateXmlNode("<test-case id='1'/>"));

            Model.TestFinished += Raise.Event<TestEventHandler>(new TestEventArgs(TestAction.TestFinished, result));
        }

        [Test]
        public void StatusBar_RecordsSuccess()
        {
            View.StatusBar.Received().RecordSuccess();
        }
    }
}
