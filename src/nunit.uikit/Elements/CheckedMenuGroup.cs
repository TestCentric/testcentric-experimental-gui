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

using System;
using System.Windows.Forms;

namespace NUnit.UiKit.Elements
{
    /// <summary>
    /// MenuElement is the implementation of ToolStripItem 
    /// used in the actual application.
    /// </summary>
    public class CheckedMenuGroup : ISelection
    {
        private ToolStripMenuItem[] _menuItems;

        public event CommandHandler SelectionChanged;

        public CheckedMenuGroup(string name, params ToolStripMenuItem[] menuItems)
        {
            this.Name = name;
            this._menuItems = menuItems;
            this.SelectedIndex = -1;

            for (int index = 0; index < menuItems.Length; index++)
            {
                ToolStripMenuItem menuItem = menuItems[index];

                // We need to handle this ourselves
                menuItem.CheckOnClick = false;

                if (menuItem.Checked)
                    // Select first menu item checked in designer
                    // and uncheck any others.
                    if (SelectedIndex == -1)
                        SelectedIndex = index;
                    else
                        menuItem.Checked = false;

                // Handle click by user
                menuItem.Click += menuItem_Click;
            }

            // If no items were checked, select first one
            if (SelectedIndex == -1 && menuItems.Length > 0)
            {
                menuItems[0].Checked = true;
                SelectedIndex = 0;
            }
        }

        // Note that all items must be on the same toolstrip
        private ToolStrip _toolStrip;
        public ToolStrip ToolStrip
        {
            get
            {
                if (_toolStrip == null && _menuItems.Length > 0)
                    _toolStrip = _menuItems[0].GetCurrentParent();

                return _toolStrip;
            }
        }

        public string Name { get; private set; }
        public string Text { get; set; }
        public int SelectedIndex 
        {
            get
            {
                for (int i = 0; i < _menuItems.Length; i++)
                    if (_menuItems[i].Checked)
                        return i;

                return -1;
            }
            set
            {
                InvokeIfRequired(() =>
                {
                    for (int i = 0; i < _menuItems.Length; i++)
                        _menuItems[i].Checked = value == i;
                });
            }
        }

        public string SelectedItem
        {
            get { return (string)_menuItems[SelectedIndex].Tag; }
            set
            {
                for (int i = 0; i < _menuItems.Length; i++)
                    if ((string)_menuItems[i].Tag == value)
                    {
                        SelectedIndex = i;
                        break;
                    }
            }
        }

        private bool _enabled;
        public bool Enabled
        {
            get { return _enabled; }
            set
            {
                _enabled = value;

                foreach (ToolStripMenuItem menuItem in _menuItems)
                    menuItem.Enabled = value;
            }
        }

        private bool _visible;
        public bool Visible
        {
            get { return _visible; }
            set
            {
                _visible = value;

                foreach (ToolStripMenuItem menuItem in _menuItems)
                    menuItem.Visible = value;
            }
        }

        void menuItem_Click(object sender, System.EventArgs e)
        {
            ToolStripMenuItem clicked = (ToolStripMenuItem)sender;

            // If user clicks selected item, ignore it
            if (!clicked.Checked)
            {
                for (int index = 0; index < _menuItems.Length; index++)
                {
                    ToolStripMenuItem item = _menuItems[index];

                    if (item == clicked)
                    {
                        item.Checked = true;
                        SelectedIndex = index;
                    }
                    else
                    {
                        item.Checked = false;
                    }
                }

                if (SelectionChanged != null)
                    SelectionChanged();
            }
        }

        public void EnableItem(string tag, bool enabled)
        {
            foreach (ToolStripMenuItem item in _menuItems)
                if ((string)item.Tag == tag)
                    item.Enabled = enabled;
        }

        public void InvokeIfRequired(MethodInvoker _delegate)
        {
            if (ToolStrip.InvokeRequired)
                ToolStrip.BeginInvoke(_delegate);
            else
                _delegate();
        }
    }
}
