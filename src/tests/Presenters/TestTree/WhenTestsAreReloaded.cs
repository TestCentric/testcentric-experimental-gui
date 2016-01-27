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

using NSubstitute;
using NUnit.Framework;

namespace NUnit.Gui.Presenters.TestTree
{
    using Model;
    using Views;

    public class WhenTestsAreReloaded : TestTreePresenterTestBase
    {
        [SetUp]
        public void SimulateTestReload()
        {
            _model.IsPackageLoaded.Returns(true);
            _model.HasTests.Returns(true);
            _model.IsTestRunning.Returns(false);

            _model.TestLoaded += Raise.Event<TestNodeEventHandler>(new TestNodeEventArgs(TestAction.TestReloaded, new TestNode("<test-run/>")));
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
    }
}
