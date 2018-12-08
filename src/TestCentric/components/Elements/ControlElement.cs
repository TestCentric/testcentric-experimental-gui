// ***********************************************************************
// Copyright (c) 2015 Charlie Poole
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

using System.Drawing;
using System.Windows.Forms;

namespace TestCentric.Gui.Elements
{
    /// <summary>
    /// ControlElement is a generic wrapper for controls.
    /// </summary>
    public class ControlElement : IControlElement
    {
        protected Control _control;

        public ControlElement(Control control)
        {
            _control = control;
        }

        public Point Location
        {
            get { return _control.Location; }
            set { InvokeIfRequired(() => { _control.Location = value; }); }
        }

        public Size Size
        {
            get { return _control.Size; }
            set { InvokeIfRequired(() => { _control.Size = value; }); }
        }

        public Size ClientSize
        {
            get { return _control.ClientSize; }
            set { InvokeIfRequired(() => { _control.ClientSize = value; }); }
        }

        public bool Enabled
        {
            get { return _control.Enabled; }
            set { InvokeIfRequired(() => { _control.Enabled = value; }); }
        }

        public bool Visible
        {
            get { return _control.Visible; }
            set { InvokeIfRequired(() => { _control.Visible = value; }); }
        }

        public string Text
        {
            get { return _control.Text; }
            set { InvokeIfRequired(() => { _control.Text = value; }); }
        }

        public void InvokeIfRequired(MethodInvoker del)
        {
            if (_control.InvokeRequired)
                _control.BeginInvoke(del, new object[0]);
            else
                del();
        }
    }

    /// <summary>
    /// ControlElement is a generic wrapper for controls where the control 
    /// itself needs to be made publicly available.
    /// </summary>
    public class ControlElement<T> : ControlElement, IControlElement<T> where T : Control
    {
        public ControlElement(T control) : base(control)
        {
            this.Control = control;
        }

        public T Control { get; private set; }

    }
}

