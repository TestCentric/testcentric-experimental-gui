using System.Drawing;
using System.Windows.Forms;
using NUnit.Engine.Extensibility;

namespace TestCentric.Gui.Views.AddinPages
{
    public partial class AddinsView : Form, IAddinsView
    {
        public AddinsView()
        {
            InitializeComponent();
        }

        void IDialog.ShowDialog()
        {
            ShowDialog();
        }

        public void AddExtensionPoint(IExtensionPoint extensionPoint)
        {
            var extensionPointView = new ExtensionPointView(extensionPoint);
            availablePoints.Controls.Add(extensionPointView);
            availablePoints.Controls.Add(new Panel
            {
                Dock = DockStyle.Top,
                Size = new Size(0, 10),
            });
        }
    }
}
