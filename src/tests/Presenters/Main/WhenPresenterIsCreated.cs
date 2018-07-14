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
    public class WhenPresenterIsCreated : MainPresenterTestBase
    {
#if NYI
        [Test]
        public void NewProject_IsEnabled()
        {
            View.NewProjectCommand.Received(1).Enabled = true;
        }
#endif

        [Test]
        public void OpenProject_IsEnabled()
        {
            View.OpenProjectCommand.Received(1).Enabled = true;
        }

        [Test]
        public void CloseCommand_IsDisabled()
        {
            View.CloseCommand.Received(1).Enabled = false;
        }

        [Test]
        public void SaveCommand_IsDisabled()
        {
            View.SaveCommand.Received(1).Enabled = false;
        }

        [Test]
        public void SaveAsCommand_IsDisabled()
        {
            View.SaveAsCommand.Received(1).Enabled = false;
        }

        [Test]
        public void SaveResults_IsDisabled()
        {
            View.SaveResultsCommand.Received(1).Enabled = false;
        }

        [Test]
        public void ReloadTests_IsDisabled()
        {
            View.ReloadTestsCommand.Received(1).Enabled = false;
        }

        [Test]
        public void RecentProjects_IsEnabled()
        {
            View.RecentProjectsMenu.Received(1).Enabled = true;
        }

        [Test]
        public void ExitCommand_IsEnabled()
        {
            View.ExitCommand.Received(1).Enabled = true;
        }

        [Test]
        public void ProjectMenu_IsDisabled()
        {
            View.ProjectMenu.Received(1).Enabled = false;
        }

        [Test]
        public void ProjectMenu_IsInvisible()
        {
            View.ProjectMenu.Received(1).Visible = false;
        }

    }
}
