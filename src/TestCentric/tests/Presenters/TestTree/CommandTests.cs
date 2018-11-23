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

using System.Windows.Forms;
using NUnit.Engine;
using NUnit.Framework;
using NSubstitute;

namespace TestCentric.Gui.Presenters.TestTree
{
    using Model;
    using Elements;

    public class CommandTests : TestTreePresenterTestBase
    {
        [Test]
        public void ToolStrip_RunButton_RunsAllTests()
        {
            _model.HasTests.Returns(true);
            _model.IsTestRunning.Returns(false);
            _view.RunButton.Execute += Raise.Event<CommandHandler>();
            _model.Received().RunAllTests();
        }

        [Test]
        public void ToolStrip_RunAllCommand_RunsAllTests()
        {
            _view.RunAllCommand.Execute += Raise.Event<CommandHandler>();
            _model.Received().RunAllTests();
        }

        [Test]
        public void ToolStrip_RunSelectedCommand_RunsSelectedTest()
        {
            var testNode = new TestNode("<test-case id='123'/>");
            var treeNode = new TreeNode("test");
            treeNode.Tag = testNode;

            _view.Tree.SelectedNodeChanged += Raise.Event<TreeNodeActionHandler>(treeNode);
            _view.RunSelectedCommand.Execute += Raise.Event<CommandHandler>();
            _model.Received().RunTests(testNode);
        }

        [Test]
        public void ToolStrip_RunFailedCommand_RunsAllTests()
        {
            _view.RunFailedCommand.Execute += Raise.Event<CommandHandler>();
            _model.Received().RunAllTests();
        }

        [Test]
        public void ContextMenu_RunCommand_RunsSpecifiedTest()
        {
            var xmlNode = XmlHelper.CreateXmlNode("<test-case id='5'/>");
            var treeNode = new TreeNode("MyTest");
            treeNode.Tag = new TestNode(xmlNode);

            _view.Tree.SelectedNodeChanged += Raise.Event<TreeNodeActionHandler>(treeNode);
            _view.RunContextCommand.Execute += Raise.Event<CommandHandler>();

            _model.Received().RunTests(Arg.Is<TestNode>((t) => t.Id == "5"));
        }

        [Test]
        public void ContextMenu_ExpandAll_ExpandsTree()
        {
            _view.ExpandAllCommand.Execute += Raise.Event<CommandHandler>();

            _view.Received().ExpandAll();
        }

        [Test]
        public void ContextMenu_CollapseAll_CollapsesTree()
        {
            _view.CollapseAllCommand.Execute += Raise.Event<CommandHandler>();

            _view.Received().CollapseAll();
        }

        [Test]
        public void ContextMenu_CollapseToFixtures()
        {
            // TreeNodeCollection has no constructor!
            //View.Tree.Nodes.Returns(new TreeView().Nodes);
            _view.CollapseToFixturesCommand.Execute += Raise.Event<CommandHandler>();

            // TODO: Develop a real test using mock-fixture
        }
    }
}
