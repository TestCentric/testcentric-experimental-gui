using System.ComponentModel;
using System.Windows.Forms;
using NUnit.Engine.Extensibility;

namespace TestCentric.Gui.Views.AddinPages
{
    using System.Drawing;

    internal class ExtensionPointView : GroupBox
    {
        private readonly IExtensionPoint _extensionPoint;
        private readonly ToolTip descriptionTooltip;
        private readonly IContainer components;

        public ExtensionPointView(IExtensionPoint point)
        {
            SuspendLayout();
            _extensionPoint = point;
            components = new Container();
            AutoSize = true;
            descriptionTooltip = new ToolTip(components)
            {
                InitialDelay = 100,
                ReshowDelay = 100,
            };
            descriptionTooltip.SetToolTip(this, point.Description);
            Text = point.Path;
            Dock = DockStyle.Top;
            AddExtensionNodes(this);
            Controls.Add(new Panel
            {
                Dock = DockStyle.Left,
                Size = new Size(20, 0),
            });
            ResumeLayout();
        }

        private void AddExtensionNodes(Control control)
        {
            bool addedAnyExtensionNodes = false;
            foreach (var node in _extensionPoint.Extensions)
            {
                control.Controls.Add(new ExtensionNodeView(node));
                addedAnyExtensionNodes = true;
            }

            if (!addedAnyExtensionNodes)
                control.Controls.Add(new Label { Text = "Has No Extensions", Dock = DockStyle.Top });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                components.Dispose();

            base.Dispose(disposing);
        }
    }
}
