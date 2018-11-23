// ***********************************************************************
// Copyright (c) 2017 Charlie Poole
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

using System;
using System.Drawing;
using System.Windows.Forms;

namespace TestCentric.Gui.Controls
{
    public class LongRunningOperationDisplay : Form
    {
        private readonly Cursor _ownerCursor;
        private readonly Label _operation;
        public LongRunningOperationDisplay(Form owner, string text)
        {
            Owner = owner;
            _ownerCursor = owner.Cursor;
            _operation = new Label()
            {
                BorderStyle = BorderStyle.FixedSingle,
                Dock = DockStyle.Fill,
                Font = new Font(FontFamily.GenericSansSerif, 10.2F, FontStyle.Italic, GraphicsUnit.Point, 0),
                TextAlign = ContentAlignment.MiddleCenter,
                Text = text
            };

            InitializeComponent();

            Show();
            Invalidate();
            Update();
        }

        private void InitializeComponent()
        {
            SuspendLayout();
            BackColor = Color.LightYellow;
            ClientSize = new Size(320, 40);
            ControlBox = false;
            Controls.Add(_operation);
            Cursor = Cursors.WaitCursor;
            FormBorderStyle = FormBorderStyle.None;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "LongRunningOperationDisplay";
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.Manual;
            ResumeLayout(false);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            ClientSize = new Size(320, 40);
            var origin = Owner.Location;
            origin.Offset(
                (Owner.Size.Width - Size.Width) / 2,
                (Owner.Size.Height - Size.Height) / 2);

            Location = origin;
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
                Owner.Cursor = _ownerCursor;

            base.Dispose(disposing);
        }
    }
}
