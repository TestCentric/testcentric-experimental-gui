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

namespace TestCentric.Gui.Presenters.Main
{
    using Model;

    public class WhenTestRunBegins : MainPresenterTestBase
    {
        [SetUp]
        protected void SimulateTestRunStarting()
        {
            Model.HasTests.Returns(true);
            Model.IsTestRunning.Returns(true);
            Model.Events.RunStarting += Raise.Event<RunStartingEventHandler>(new RunStartingEventArgs(1234));
        }

#if NYI
        [Test]
        public void NewProject_IsDisabled()
        {
            Assert.That(View.NewProjectCommand.Enabled == false);
        }
#endif

        [Test]
        public void OpenProject_IsDisabled()
        {
            Assert.That(View.OpenProjectCommand.Enabled == false);
        }

        [Test]
        public void CloseCommand_IsDisabled()
        {
            Assert.That(View.CloseCommand.Enabled == false);
        }

        [Test]
        public void SaveCommand_IsDisabled()
        {
            Assert.That(View.SaveCommand.Enabled == false);
        }

        [Test]
        public void SaveAsCommand_IsDisabled()
        {
            Assert.That(View.SaveAsCommand.Enabled == false);
        }

        [Test]
        public void SaveResults_IsDisabled()
        {
            Assert.That(View.SaveResultsCommand.Enabled == false);
        }

        [Test]
        public void ReloadTests_IsDisabled()
        {
            Assert.That(View.ReloadTestsCommand.Enabled == false);
        }

        [Test]
        public void RecentProjects_IsDisabled()
        {
            Assert.That(View.RecentProjectsMenu.Enabled == false);
        }

        [Test]
        public void ExitCommand_IsEnabled()
        {
            Assert.That(View.ExitCommand.Enabled == true);
        }

        [Test]
        public void ProjectMenu_IsVisible()
        {
            Assert.That(View.ProjectMenu.Visible == true);
        }
    }
}
