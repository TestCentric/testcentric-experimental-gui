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
using System.Collections.Generic;
using System.Windows.Forms;

namespace TestCentric.Gui.Elements
{
    /// <summary>
    /// MenuElement is the implemented here using a ToolStripItem 
    /// but the view exposes each element using one of the three 
    /// key interfaces (IMenu, ICommand or IChecked) which should
    /// not contain any control-specific logic.
    /// </summary>
    public class ToolStripMenuElement : ToolStripElement, IToolStripMenu, ICommand, IChecked
    {
        public event CommandHandler Execute;
        public event CommandHandler Popup;
        public event CommandHandler CheckedChanged;

        private ToolStripMenuItem _menuItem;

        public ToolStripMenuElement(ToolStripMenuItem menuItem)
            : base(menuItem)
        {
            _menuItem = menuItem;

            menuItem.Click += (s, e) => Execute?.Invoke();
            menuItem.DropDownOpening += (s, e) => Popup?.Invoke();
            menuItem.CheckedChanged += (s, e) => CheckedChanged?.Invoke();
        }

        public ToolStripMenuElement(string text) : this(new ToolStripMenuItem(text)) { }

        public ToolStripMenuElement(string text, CommandHandler execute) : this(text)
        {
            this.Execute = execute;
        }

        public bool Checked
        {
            get { return _menuItem.Checked; }
            set
            {
                if (_menuItem.Checked != value)
                {
                    InvokeIfRequired(() =>
                    {
                        _menuItem.Checked = value;
                    });
                }
            }
        }

        public ToolStripItemCollection Items
        {
            get { return _menuItem.DropDown.Items; }
        }
    }
}
