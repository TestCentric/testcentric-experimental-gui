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
using System.Xml;

namespace TestCentric.Gui.Presenters.Main
{
    using Model;
    using Views;

    public class WhenTestsAreReloaded : MainPresenterTestBase
    {
        [SetUp]
        public void SimulateTestLoad()
        {
            Model.HasTests.Returns(true);
            Model.IsTestRunning.Returns(false);

            XmlDocument doc = new XmlDocument();
            doc.LoadXml("<test-suite id='1'/>");
            TestNode testNode = new TestNode(doc.FirstChild);
            Model.Tests.Returns(testNode);
            Model.TestReloaded += Raise.Event<TestNodeEventHandler>(new TestNodeEventArgs(TestAction.TestReloaded, testNode));
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
        public void CloseCommand_IsEnabled()
        {
            View.CloseCommand.Received().Enabled = true;
        }

        [Test]
        public void SaveCommand_IsEnabled()
        {
            View.SaveCommand.Received().Enabled = true;
        }

        [Test]
        public void SaveAsCommand_IsEnabled()
        {
            View.SaveAsCommand.Received().Enabled = true;
        }

        [Test]
        public void SaveResults_IsEnabled()
        {
            View.SaveResultsCommand.Received().Enabled = false;
        }

        [Test]
        public void ReloadTests_IsEnabled()
        {
            View.ReloadTestsCommand.Received().Enabled = true;
        }

        [Test]
        public void ProcessModel_IsUnchanged()
        {
            View.ProcessModel.DidNotReceiveWithAnyArgs().SelectedItem = null;
        }

        [Test]
        public void DomainUsage_IsUnchanged()
        {
            View.DomainUsage.DidNotReceiveWithAnyArgs().SelectedItem = null;
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
        public void ProjectMenu_IsVisible()
        {
            View.ProjectMenu.Received().Visible = true;
        }
    }
}
