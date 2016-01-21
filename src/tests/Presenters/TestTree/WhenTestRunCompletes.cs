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
using NUnit.UiKit.Elements;
using System.Xml;

namespace NUnit.Gui.Presenters.TestTree
{
    using Model;

    public class WhenTestRunCompletes : TestTreePresenterTestBase
    {
        [SetUp]
        public void SimulateTestRunFinish()
        {
            _model.IsPackageLoaded.Returns(true);
            _model.HasTests.Returns(true);
            _model.IsTestRunning.Returns(false);

            var resultNode = new ResultNode("<test-suite/>");

            _model.RunFinished += Raise.Event<TestResultEventHandler>(new TestResultEventArgs(TestAction.RunFinished, resultNode));
        }


        [Test]
        public void RunAllCommand_IsEnabled()
        {
            _view.RunAllCommand.Received().Enabled = true;
        }

        [Test]
        public void RunSelectedCommand_IsEnabled()
        {
            _view.RunSelectedCommand.Received().Enabled = true;
        }

        [Test]
        public void RunFailedCommand_IsEnabled()
        {
            _view.RunFailedCommand.Received().Enabled = true;
        }

        [Test]
        public void StopRunCommand_IsDisabled()
        {
            _view.StopRunCommand.Received().Enabled = false;
        }

        [Test]
        public void RunAllMenuItemRunsAllTests()
        {
            _view.RunAllCommand.Execute += Raise.Event<CommandHandler>();
            _model.Received().RunAllTests();
        }
    }
}
