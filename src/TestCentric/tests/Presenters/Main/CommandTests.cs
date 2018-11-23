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

using System.Collections.Generic;
using System.IO;
using NUnit.Engine;
using NUnit.Framework;
using NSubstitute;

namespace TestCentric.Gui.Presenters.Main
{
    using Views;
    using Elements;

    public class CommandTests : MainPresenterTestBase
    {
        [Test]
        public void NewProjectCommand_CallsNewProject()
        {
            View.NewProjectCommand.Execute += Raise.Event<CommandHandler>();
            // This is NYI, change when we implement it
            Model.DidNotReceive().NewProject();
        }

        [Test]
        public void OpenProjectCommand_CallsLoadTests()
        {
            var files = new string[] { Path.GetFullPath("/path/to/test.dll") };
            View.DialogManager.GetFilesToOpen().Returns(files);

            View.OpenProjectCommand.Execute += Raise.Event<CommandHandler>();
            Model.Received().LoadTests(Arg.Any<IList<string>>());
            Model.Received().LoadTests(Arg.Is<IList<string>>((p) => p.Count == 1));
        }

        [Test]
        public void OpenProjectCommand_WhenLoadFails_DoesNotCallLoadTest()
        {
            View.DialogManager.GetFileOpenPath(null).ReturnsForAnyArgs("/path/to/test.dll");
            Model.HasTests.Returns(false);

            View.OpenProjectCommand.Execute += Raise.Event<CommandHandler>();
            Model.DidNotReceive().LoadTests(Arg.Any<IList<string>>());
        }

        [Test]
        public void CloseCommand_CallsUnloadTest()
        {
            View.CloseCommand.Execute += Raise.Event<CommandHandler>();
            Model.Received().UnloadTests();
        }

        [Test]
        public void SaveCommand_CallsSaveProject()
        {
            View.SaveCommand.Execute += Raise.Event<CommandHandler>();
            // This is NYI, change when we implement it
            Model.DidNotReceive().SaveProject();
        }

        [Test]
        public void SaveAsCommand_CallsSaveProject()
        {
            View.SaveAsCommand.Execute += Raise.Event<CommandHandler>();
            // This is NYI, change when we implement it
            Model.DidNotReceive().SaveProject();
        }

        public void SaveResultsCommand_CallsSaveResults()
        {
        }

        [Test]
        public void ReloadTestsCommand_CallsReloadTests()
        {
            View.ReloadTestsCommand.Execute += Raise.Event<CommandHandler>();
            Model.Received().ReloadTests();
        }

        public void SelectRuntimeCommand_PopsUpMenu()
        {
        }

        public void RecentProjectsMenu_PopsUpMenu()
        {
        }

        public void ExitCommand_CallsExit()
        {
        }
    }
}
