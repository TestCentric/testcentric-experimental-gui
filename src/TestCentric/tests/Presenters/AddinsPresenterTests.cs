using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Engine;
using NUnit.Engine.Extensibility;
using NUnit.Framework;
using NSubstitute;

namespace TestCentric.Gui.Presenters
{
    using Views;

    [TestFixture]
    public class AddinsPresenterTests
    {
        private AddinViewFake _view;
        private AddinsPresenter _presenter;
        private IExtensionService _extensionService;
        private IExtensionPoint _extensionPoint;
        private List<IExtensionNode> _extensionNodes;

        [SetUp]
        public void Setup()
        {
            _view = new AddinViewFake();
            _extensionService = Substitute.For<IExtensionService>();
            _extensionPoint = Substitute.For<IExtensionPoint>();

            _extensionNodes = new List<IExtensionNode>();
            _extensionPoint.Extensions.Returns(_extensionNodes);
            _extensionService.ExtensionPoints.Returns(new[] { _extensionPoint });
            _presenter = new AddinsPresenter(_view, _extensionService);
        }

        [TearDown]
        public void TearDown()
        {
            _presenter = null;
            _view = null;
            _extensionService = null;
        }

        [Test]
        public void WhenExtensionPointHasNoExtensions_ItIsStillDisplayed()
        {
            _extensionNodes.Clear();

            _presenter.Show();

            Assert.That(_view.AddedExtensionPoints.Count, Is.EqualTo(_extensionService.ExtensionPoints.Count()));
        }

        private class AddinViewFake : IAddinsView
        {
            public readonly List<IExtensionNode> AddedExtensionNodes = new List<IExtensionNode>();
            public readonly List<IExtensionPoint> AddedExtensionPoints = new List<IExtensionPoint>();

            public void SuspendLayout() { }

            public void ResumeLayout() { }

            public void ShowDialog() { }

            public void Close() { }

            public void AddExtensionPoint(IExtensionPoint extensionPoint)
            {
                AddedExtensionPoints.Add(extensionPoint);
            }

            public event EventHandler Load = delegate { };
        }
    }
}
