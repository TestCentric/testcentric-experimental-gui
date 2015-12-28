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

using System.Windows.Forms;

namespace NUnit.UiKit.Elements
{
    /// <summary>
    /// ToolStripItem is a generic wrapper for ToolStripItems
    /// </summary>
    public class ToolStripElement<T> : IToolStripElement<T> where T : ToolStripItem
    {
        public ToolStripElement(T toolStripItem)
        {
            this.ToolStripItem = toolStripItem;

            _visible = toolStripItem.Visible;
            _enabled = toolStripItem.Enabled;
            _text = toolStripItem.Text;
        }

        public T ToolStripItem { get; private set; }

        public string Name
        {
            get { return ToolStripItem.Name; }
        }

        private bool _enabled;
        public bool Enabled
        {
            get { return _enabled; }
            set
            {
                if (_enabled != value)
                {
                    _enabled = value;
                    InvokeIfRequired(() => { ToolStripItem.Enabled = value; });
                }
            }
        }

        private bool _visible;
        public bool Visible
        {
            get { return _visible; }
            set
            {
                if (_visible != value)
                {
                    _visible = value;
                    InvokeIfRequired(() => { ToolStripItem.Visible = value; });
                }
            }
        }

        private string _text;
        public string Text
        {
            get { return _text; }
            set
            {
                if (_text != value)
                {
                    _text = value;
                    InvokeIfRequired(() => { ToolStripItem.Text = value; });
                }
            }
        }

        private string _toolTipText;
        public string ToolTipText
        {
            get { return ToolStripItem.ToolTipText; }
            set
            {
                _toolTipText = value;
                InvokeIfRequired(() => { ToolStripItem.ToolTipText = value; });
            }
        }

        public void InvokeIfRequired(MethodInvoker del)
        {
            var toolStrip = ToolStripItem.GetCurrentParent();

            if (toolStrip != null && toolStrip.InvokeRequired)
                toolStrip.BeginInvoke(del, new object[0]);
            else
                del();
        }
    }
}
