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
        }

        public T ToolStripItem { get; private set; }

        public string Name
        {
            get { return ToolStripItem.Name; }
        }

        public bool Enabled
        {
            get { return ToolStripItem.Enabled; }
            set { InvokeIfRequired(() => { ToolStripItem.Enabled = value; }); }
        }

        public bool Visible
        {
            get { return ToolStripItem.Visible; }
            set { InvokeIfRequired(() => { ToolStripItem.Visible = value; }); }
        }

        public string Text
        {
            get { return ToolStripItem.Text; }
            set { InvokeIfRequired(() => { ToolStripItem.Text = value; }); }
        }

        public string ToolTipText
        {
            get { return ToolStripItem.ToolTipText; }
            set { InvokeIfRequired(() => { ToolStripItem.ToolTipText = value; }); }
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
