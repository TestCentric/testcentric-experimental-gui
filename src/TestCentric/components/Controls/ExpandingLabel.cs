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
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace TestCentric.Gui.Controls
{
    /// <summary>
    /// A special type of label which can display a tooltip-like
    /// window to show the full extent of any text which doesn't 
    /// fit. The window may be placed directly over the label
    /// or immediately beneath it and will expand to fit in
    /// a horizontal, vertical or both directions as needed.
    /// </summary>
    public class ExpandingLabel : System.Windows.Forms.Label
    {
        #region Instance Variables

        /// <summary>
        /// Our window for displaying expanded text
        /// </summary>
        private TipWindow tipWindow;

        /// <summary>
        /// Direction of expansion
        /// </summary>
        private TipWindow.ExpansionStyle expansion = TipWindow.ExpansionStyle.Horizontal;

        /// <summary>
        /// True if tipWindow may overlay the label
        /// </summary>
        private bool overlay = true;

        /// <summary>
        /// Time in milliseconds that the tip window
        /// will remain displayed.
        /// </summary>
        private int autoCloseDelay = 0;

        /// <summary>
        /// Time in milliseconds that the window stays
        /// open after the mouse leaves the control.
        /// </summary>
        private int mouseLeaveDelay = 300;

        /// <summary>
        /// If true, a context menu with Copy is displayed which
        /// allows copying contents to the clipboard.
        /// </summary>
        private bool copySupported = false;

        #endregion

        #region Properties

        [Browsable(false)]
        public bool Expanded
        {
            get { return tipWindow != null && tipWindow.Visible; }
        }

        [Category("Behavior"), DefaultValue(TipWindow.ExpansionStyle.Horizontal)]
        public TipWindow.ExpansionStyle Expansion
        {
            get { return expansion; }
            set { expansion = value; }
        }

        [Category("Behavior"), DefaultValue(true)]
        [Description("Indicates whether the tip window should overlay the label")]
        public bool Overlay
        {
            get { return overlay; }
            set { overlay = value; }
        }

        /// <summary>
        /// Time in milliseconds that the tip window
        /// will remain displayed.
        /// </summary>
        [Category("Behavior"), DefaultValue(0)]
        [Description("Time in milliseconds that the tip is displayed. Zero indicates no automatic timeout.")]
        public int AutoCloseDelay
        {
            get { return autoCloseDelay; }
            set { autoCloseDelay = value; }
        }

        /// <summary>
        /// Time in milliseconds that the window stays
        /// open after the mouse leaves the control.
        /// Reentering the control resets this.
        /// </summary>
        [Category("Behavior"), DefaultValue(300)]
        [Description("Time in milliseconds that the tip is displayed after the mouse levaes the control")]
        public int MouseLeaveDelay
        {
            get { return mouseLeaveDelay; }
            set { mouseLeaveDelay = value; }
        }

        [Category("Behavior"), DefaultValue(false)]
        [Description("If true, displays a context menu with Copy")]
        public bool CopySupported
        {
            get { return copySupported; }
            set
            {
                copySupported = value;
                if (copySupported)
                    base.ContextMenu = null;
            }
        }

        /// <summary>
        /// Override Text property to set up copy menu if
        /// the val is non-empty.
        /// </summary>
        public override string Text
        {
            get { return base.Text; }
            set
            {
                base.Text = value;

                if (copySupported)
                {
                    if (value == null || value == string.Empty)
                    {
                        if (this.ContextMenu != null)
                        {
                            this.ContextMenu.Dispose();
                            this.ContextMenu = null;
                        }
                    }
                    else
                    {
                        this.ContextMenu = new System.Windows.Forms.ContextMenu();
                        MenuItem copyMenuItem = new MenuItem("Copy", new EventHandler(CopyToClipboard));
                        this.ContextMenu.MenuItems.Add(copyMenuItem);
                    }
                }
            }
        }

        #endregion

        #region Public Methods

        public void Expand()
        {
            if (!Expanded)
            {
                tipWindow = new TipWindow(this);
                tipWindow.Closed += new EventHandler(tipWindow_Closed);
                tipWindow.Expansion = this.Expansion;
                tipWindow.Overlay = this.Overlay;
                tipWindow.AutoCloseDelay = this.AutoCloseDelay;
                tipWindow.MouseLeaveDelay = this.MouseLeaveDelay;
                tipWindow.WantClicks = this.CopySupported;
                tipWindow.Show();
            }
        }

        public void Unexpand()
        {
            if (Expanded)
            {
                tipWindow.Close();
            }
        }

        #endregion

        #region Event Handlers

        private void tipWindow_Closed(object sender, EventArgs e)
        {
            tipWindow = null;
        }

        protected override void OnMouseHover(System.EventArgs e)
        {
            Graphics g = Graphics.FromHwnd(Handle);
            SizeF sizeNeeded = g.MeasureString(Text, Font);
            bool expansionNeeded =
                Width < (int)sizeNeeded.Width ||
                Height < (int)sizeNeeded.Height;

            if (expansionNeeded) Expand();
        }

        /// <summary>
        /// Copy contents to clipboard
        /// </summary>
        private void CopyToClipboard(object sender, EventArgs e)
        {
            Clipboard.SetDataObject(this.Text);
        }

        #endregion
    }
}
