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
using NUnit.Framework;
using NSubstitute;

namespace TestCentric.Gui.Presenters
{
    using Views;
    using Model;
    using Elements;

    public class TreeViewPresenterTests
    {
        private ITestTreeView _view;
        private ITestModel _model;
        private TreeViewPresenter _presenter;

        [SetUp]
        public void CreatePresenter()
        {
            _view = Substitute.For<ITestTreeView>();
            _model = Substitute.For<ITestModel>();

            _presenter = new TreeViewPresenter(_view, _model);

            // Make it look like the view loaded
            _view.Load += Raise.Event<System.EventHandler>(null, new System.EventArgs());
        }

        [TearDown]
        public void RemovePresenter()
        {
            //if (_presenter != null)
            //    _presenter.Dispose();

            _presenter = null;
        }

        //[Test]
        //public void WhenTestRunStarts_ResultsAreCleared()
        //{
        //    _settings.RunStarting += Raise.Event<TestEventHandler>(new TestEventArgs(TestAction.RunStarting, "Dummy", 1234));

        //    _view.Received().ClearResults();
        //}

        static object[] resultData = new object[] {
            new object[] { ResultState.Success, TestTreeView.SuccessIndex },
            new object[] { ResultState.Ignored, TestTreeView.WarningIndex },
            new object[] { ResultState.Failure, TestTreeView.FailureIndex },
            new object[] { ResultState.Inconclusive, TestTreeView.InconclusiveIndex },
            new object[] { ResultState.Skipped, TestTreeView.SkippedIndex },
            new object[] { ResultState.NotRunnable, TestTreeView.FailureIndex },
            new object[] { ResultState.Error, TestTreeView.FailureIndex },
            new object[] { ResultState.Cancelled, TestTreeView.FailureIndex }
        };

        [TestCaseSource("resultData")]
        public void WhenTestCaseCompletes_NodeShowsProperResult(ResultState resultState, int expectedIndex)
        {
            _model.IsPackageLoaded.Returns(true);
            _model.HasTests.Returns(true);
            _view.DisplayFormat.SelectedItem.Returns("NUNIT_TREE");

            var result = resultState.Status.ToString();
            var label = resultState.Label;

            var testNode = new TestNode("<test-run id='1'><test-case id='123'/></test-run>");
            var resultNode = new ResultNode(string.IsNullOrEmpty(label)
                ? string.Format("<test-case id='123' result='{0}'/>", result)
                : string.Format("<test-case id='123' result='{0}' label='{1}'/>", result, label));
            _model.Tests.Returns(testNode);

            //var treeNode = _adapter.MakeTreeNode(result);
            //_adapter.NodeIndex[suiteResult.Id] = treeNode;
            _model.Events.TestLoaded += Raise.Event<TestNodeEventHandler>(new TestNodeEventArgs(TestAction.TestLoaded, testNode));
            _model.Events.TestFinished += Raise.Event<TestResultEventHandler>(new TestResultEventArgs(TestAction.TestFinished, resultNode));

            _view.Tree.Received().SetImageIndex(Arg.Any<TreeNode>(), expectedIndex);
        }

        [Test]
        public void WhenDisplayFormatChanges_TreeIsReloaded()
        {
            TestNode testNode = new TestNode(XmlHelper.CreateXmlNode("<test-run id='1'><test-suite id='42'/></test-run>"));
            _model.Tests.Returns(testNode);
            _view.DisplayFormat.SelectedItem.Returns("NUNIT_TREE");
            _view.DisplayFormat.SelectionChanged += Raise.Event<CommandHandler>();

            _view.Tree.Received().Add(Arg.Is<TreeNode>((tn) => ((TestNode)tn.Tag).Id == "42"));
        }
    }
}
