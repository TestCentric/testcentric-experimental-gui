using System.Drawing;
using System.Windows.Forms;
using NUnit.Engine.Extensibility;

namespace TestCentric.Gui.Views.AddinPages
{
    public class ExtensionNodeView : Panel
    {
        private readonly IExtensionNode _node;

        public ExtensionNodeView(IExtensionNode node)
        {
            _node = node;
            SuspendLayout();
            AutoSize = true;
            BorderStyle = BorderStyle.Fixed3D;
            Dock = DockStyle.Top;
            Padding = new Padding(2);
            AddExtensionNode(this);
            ResumeLayout();
        }

        private void AddExtensionNode(Control control)
        {
            var panel = new Panel
            {
                AutoSize = true,
                Dock = DockStyle.Top,
            };
            var typeLabel = new Label
            {
                AutoSize = true,
                Dock = DockStyle.Fill,
                Text = _node.TypeName,
                TextAlign = ContentAlignment.MiddleLeft,
            };
            var enabledLabel = new Label
            {
                AutoSize = true,
                BackColor = _node.Enabled ? Color.LightGreen : Color.Pink,
                Dock = DockStyle.Right,
                Text = _node.Enabled ? "Enabled" : "Disabled",
                TextAlign = ContentAlignment.MiddleLeft,
            };
            panel.Controls.Add(typeLabel);
            panel.Controls.Add(enabledLabel);

            var table = CreateTableForExtensionNodeProperties();
            if (table.RowCount > 0)
            {
                control.Controls.Add(table);
                control.Controls.Add(new Panel
                {
                    Dock = DockStyle.Left,
                    Size = new Size(5, 0),
                });
            }

            control.Controls.Add(panel);
        }

        private TableLayoutPanel CreateTableForExtensionNodeProperties()
        {
            var table = new TableLayoutPanel
            {
                AutoSize = true,
                Dock = DockStyle.Top,
            };
            table.ColumnCount = 2;
            table.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));

            table.SuspendLayout();
            foreach (var propertyName in _node.PropertyNames)
            {
                table.RowCount++;
                table.RowStyles.Add(new RowStyle(SizeType.AutoSize));
                AddExtensionNodeProperties(table, propertyName);
            }
            table.ResumeLayout();

            return table;
        }

        private void AddExtensionNodeProperties(TableLayoutPanel control, string propertyName)
        {
            var row = control.RowCount;
            var propertyValue = "";
            foreach (var value in _node.GetValues(propertyName))
            {
                propertyValue += $"{value} ";
            }
            var label1 = new Label
            {
                AutoSize = true,
                Text = propertyName,
                TextAlign = ContentAlignment.MiddleLeft,
            };
            var label2 = new Label
            {
                AutoSize = true,
                Text = propertyValue,
                TextAlign = ContentAlignment.MiddleLeft,
            };

            control.Controls.Add(label1, 0, row);
            control.Controls.Add(label2, 1, row);
        }
    }
}
