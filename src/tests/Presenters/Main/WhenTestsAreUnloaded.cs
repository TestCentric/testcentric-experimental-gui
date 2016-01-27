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

namespace NUnit.Gui.Presenters.Main
{
    using Model;

    public class WhenTestsAreUnloaded : MainPresenterTestBase
    {
        [SetUp]
        public void SimulateTestUnload()
        {
            Model.HasTests.Returns(false);
            Model.IsTestRunning.Returns(false);
            Model.TestUnloaded += Raise.Event<TestEventHandler>(new TestEventArgs(TestAction.TestUnloaded));
        }

#if NYI
        [Test]
        public void NewProject_IsEnabled()
        {
            View.NewProjectCommand.Received().Enabled = true;
        }
#endif

        [Test]
        public void OpenProject_IsEnabled()
        {
            View.OpenProjectCommand.Received().Enabled = true;
        }

        [Test]
        public void CloseCommand_IsDisabled()
        {
            View.CloseCommand.Received().Enabled = false;
        }

        [Test]
        public void SaveCommand_IsDisabled()
        {
            View.SaveCommand.Received().Enabled = false;
        }

        [Test]
        public void SaveAsCommand_IsDisabled()
        {
            View.SaveAsCommand.Received().Enabled = false;
        }

        [Test]
        public void SaveResults_IsEnabled()
        {
            View.SaveResultsCommand.Received().Enabled = false;
        }

        [Test]
        public void ReloadTests_IsDisabled()
        {
            View.ReloadTestsCommand.Received().Enabled = false;
        }

        [Test]
        public void SelectRuntimeMenu_IsDisabled()
        {
            View.SelectRuntimeMenu.Received().Enabled = false;
        }

        [Test]
        public void RecentProjects_IsEnabled()
        {
            View.RecentProjectsMenu.Received().Enabled = true;
        }

        [Test]
        public void ExitCommand_IsEnabled()
        {
            View.ExitCommand.Received().Enabled = true;
        }

        [Test]
        public void ProjectMenu_IsInvisible()
        {
            View.ProjectMenu.Received().Visible = false;
        }
    }
}
