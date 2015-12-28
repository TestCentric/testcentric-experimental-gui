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

namespace NUnit.UiKit.Elements
{
    /// <summary>
    /// ControlElement is a generic wrapper for controls.
    /// </summary>
    public class ControlElement<T> : IControlElement<T> where T : Control
    {
        public ControlElement(T control)
        {
            this.Control = control;
        }

        public T Control { get; private set; }

        public Point Location
        {
            get { return Control.Location; }
            set { InvokeIfRequired(() => { Control.Location = value; }); }
        }

        public Size Size
        {
            get { return Control.Size; }
            set { InvokeIfRequired(() => { Control.Size = value; }); }
        }

        public Size ClientSize
        {
            get { return Control.ClientSize; }
            set { InvokeIfRequired(() => { Control.ClientSize = value; }); }
        }

        public string Name
        {
            get { return Control.Name; }
        }

        public bool Enabled
        {
            get { return Control.Enabled; }
            set { InvokeIfRequired(() => { Control.Enabled = value; }); }
        }

        public bool Visible
        {
            get { return Control.Visible; }
            set { InvokeIfRequired(() => { Control.Visible = value; }); }
        }

        public string Text
        {
            get { return Control.Text; }
            set { InvokeIfRequired(() => { Control.Text = value; }); }
        }

        public void InvokeIfRequired(MethodInvoker del)
        {
            if (Control.InvokeRequired)
                Control.BeginInvoke(del, new object[0]);
            else
                del();
        }
    }
}
