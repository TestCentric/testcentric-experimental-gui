using NUnit.Engine;

namespace TestCentric.Gui.Presenters
{
    using Views;

    public class AddinsPresenter
    {
        private readonly IAddinsView _view;
        private readonly IExtensionService _extensionService;

        public AddinsPresenter(IAddinsView view, IExtensionService extensionService)
        {
            _view = view;
            _extensionService = extensionService;
        }

        public void Show()
        {
            _view.SuspendLayout();
            foreach (var extensionPoint in _extensionService.ExtensionPoints)
            {
                _view.AddExtensionPoint(extensionPoint);
            }
            _view.ResumeLayout();

            _view.ShowDialog();
        }
    }
}
