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

using System.Collections.Generic;
using System.Windows.Forms;

namespace NUnit.UiKit.Elements
{
    /// <summary>
    /// MenuElement is the implementation of ToolStripItem 
    /// used in the actual application.
    /// </summary>
    public class CheckedMenuGroup : ISelection
    {
        // Fields are public for testing only
        public IList<ToolStripMenuItem> MenuItems = new List<ToolStripMenuItem>();
        public ToolStripMenuItem TopMenu;

        public event CommandHandler SelectionChanged;

        public CheckedMenuGroup(ToolStripMenuItem topMenu)
        {
            TopMenu = topMenu;
            Name = topMenu.Name + "Group";
            foreach (ToolStripMenuItem menuItem in topMenu.DropDown.Items)
                MenuItems.Add(menuItem);

            InitializeMenuItems();
        }

        public CheckedMenuGroup(string name, params ToolStripMenuItem[] menuItems)
        {
            Name = name;
            foreach (var menuItem in menuItems)
                MenuItems.Add(menuItem);

            InitializeMenuItems();
        }

        public void Refresh()
        {
            if (TopMenu != null)
            {
                MenuItems.Clear();
                foreach (ToolStripMenuItem menuItem in TopMenu.DropDown.Items)
                    MenuItems.Add(menuItem);
            }

            InitializeMenuItems();
        }

        private void InitializeMenuItems()
        {
            SelectedIndex = -1;

            for (int index = 0; index < MenuItems.Count; index++)
            {
                ToolStripMenuItem menuItem = MenuItems[index];

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
            if (SelectedIndex == -1 && MenuItems.Count > 0)
            {
                MenuItems[0].Checked = true;
                SelectedIndex = 0;
            }
        }

        // Note that all items must be on the same toolstrip
        private ToolStrip _toolStrip;
        public ToolStrip ToolStrip
        {
            get
            {
                if (_toolStrip == null && MenuItems.Count > 0)
                    _toolStrip = MenuItems[0].GetCurrentParent();

                return _toolStrip;
            }
        }

        public string Name { get; }

        public string Text { get; set; }

        public int SelectedIndex
        {
            get
            {
                for (int i = 0; i < MenuItems.Count; i++)
                    if (MenuItems[i].Checked)
                        return i;

                return -1;
            }
            set
            {
                InvokeIfRequired(() =>
                {
                    for (int i = 0; i < MenuItems.Count; i++)
                        MenuItems[i].Checked = value == i;
                });
            }
        }

        public string SelectedItem
        {
            get { return (string)MenuItems[SelectedIndex].Tag; }
            set
            {
                for (int i = 0; i < MenuItems.Count; i++)
                    if ((string)MenuItems[i].Tag == value)
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

                foreach (ToolStripMenuItem menuItem in MenuItems)
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

                foreach (ToolStripMenuItem menuItem in MenuItems)
                    menuItem.Visible = value;
            }
        }

        void menuItem_Click(object sender, System.EventArgs e)
        {
            ToolStripMenuItem clicked = (ToolStripMenuItem)sender;

            // If user clicks selected item, ignore it
            if (!clicked.Checked)
            {
                for (int index = 0; index < MenuItems.Count; index++)
                {
                    ToolStripMenuItem item = MenuItems[index];

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
            foreach (ToolStripMenuItem item in MenuItems)
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
