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
    public class MenuElement : ToolStripElement<ToolStripMenuItem>, IMenu, ICommand, IChecked
    {
        public event CommandHandler Execute;
        public event CommandHandler Popup;
        public event CommandHandler CheckedChanged;

        public MenuElement(ToolStripMenuItem menuItem)
            : base(menuItem)
        {
            menuItem.Click += delegate { if (Execute != null) Execute(); };
            menuItem.DropDownOpening += delegate { if (Popup != null) Popup(); };
            menuItem.CheckedChanged += delegate { if (CheckedChanged != null) CheckedChanged(); };
        }

        public MenuElement(string text) : this(new ToolStripMenuItem(text)) { }

        public MenuElement(string text, CommandHandler execute) : this(text)
        {
            this.Execute = execute;
        }

        public bool Checked
        {
            get { return ToolStripItem.Checked; }
            set
            {
                if (ToolStripItem.Checked != value)
                {
                    InvokeIfRequired(() =>
                    {
                        ToolStripItem.Checked = value;
                    });
                }
            }
        }

        public ToolStripItemCollection Items
        {
            get { return ToolStripItem.DropDown.Items; }
        }
    }
}
