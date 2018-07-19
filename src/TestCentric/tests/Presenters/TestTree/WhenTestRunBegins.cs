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

namespace TestCentric.Gui.Presenters.TestTree
{
    using Model;

    public class WhenTestRunBegins : TestTreePresenterTestBase
    {
        [SetUp]
        protected void SimulateTestRunStarting()
        {
            _model.IsPackageLoaded.Returns(true);
            _model.HasTests.Returns(true);
            _model.IsTestRunning.Returns(true);
            _model.Events.RunStarting += Raise.Event<RunStartingEventHandler>(new RunStartingEventArgs(1234));
        }

        [Test]
        public void RunAllCommand_IsDisabled()
        {
            _view.RunAllCommand.Received().Enabled = false;
        }

        [Test]
        public void RunSelectedCommand_IsDisabled()
        {
            _view.RunSelectedCommand.Received().Enabled = false;
        }

        [Test]
        public void RunFailedCommand_IsDisabled()
        {
            _view.RunFailedCommand.Received().Enabled = false;
        }

        [Test]
        public void StopRunCommand_IsEnabled()
        {
            _view.StopRunCommand.Received().Enabled = true;
        }
    }
}
