// ***********************************************************************
// Copyright (c) 2015-2018 Charlie Poole
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
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace TestCentric.Gui.Controls
{
    public class TipWindow : Form
    {
        // Margin of screen, used to limit TipWindow expansion
        private const int SCREEN_EDGE = 20;
        private const int SCREEN_MARGIN = 2 * SCREEN_EDGE;

        // Padding to leave inside the TipWindow around the text
        private const int PADDING_LEFT = 4;
        private const int PADDING_RIGHT = 4;
        private const int PADDING_TOP = 4;
        private const int PADDING_BOTTOM = 4;

        /// <summary>
        /// Direction in which to expand
        /// </summary>
        public enum ExpansionStyle
        {
            Horizontal,
            Vertical,
            Both
        }

        #region Instance Variables

        /// <summary>
        /// The control for which we are showing expanded text
        /// </summary>
        private Control _control;

        /// <summary>
        /// Timer used for auto-close
        /// </summary>
        private System.Windows.Forms.Timer _autoCloseTimer;

        /// <summary>
        /// Timer used for mouse leave delay
        /// </summary>
        private System.Windows.Forms.Timer _mouseLeaveTimer;

        /// <summary>
        /// Rectangle used to display text
        /// </summary>
        private Rectangle _textRect;

        #endregion

        #region Construction and Initialization

        public TipWindow(Control control)
        {
            InitializeComponent();
            InitControl(control);

            // Note: This causes an error if called on a listbox
            // with no item as yet selected, therefore, it is handled
            // differently in the constructor for a listbox.
            TipText = control.Text;
        }

        public TipWindow(ListBox listbox, int index)
        {
            InitializeComponent();
            InitControl(listbox);

            ItemBounds = listbox.GetItemRectangle(index);
            TipText = listbox.Items[index].ToString();
        }

        private void InitControl(Control control)
        {
            _control = control;
            Owner = control.FindForm();
            ItemBounds = control.ClientRectangle;

            ControlBox = false;
            MaximizeBox = false;
            MinimizeBox = false;
            BackColor = Color.LightYellow;
            FormBorderStyle = FormBorderStyle.None;
            StartPosition = FormStartPosition.Manual;

            Font = control.Font;
        }

        private void InitializeComponent()
        {
            // 
            // TipWindow
            // 
            BackColor = System.Drawing.Color.LightYellow;
            ClientSize = new System.Drawing.Size(292, 268);
            ControlBox = false;
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "TipWindow";
            ShowInTaskbar = false;
            StartPosition = System.Windows.Forms.FormStartPosition.Manual;

        }

        protected override void OnLoad(System.EventArgs e)
        {
            // At this point, further changes to the properties
            // of the label will have no effect on the tip.
            Point origin = _control.Parent.PointToScreen(_control.Location);
            origin.Offset(ItemBounds.Left, ItemBounds.Top);
            if (!Overlay) origin.Offset(0, ItemBounds.Height);
            Location = origin;

            Graphics g = Graphics.FromHwnd(Handle);
            Screen screen = Screen.FromControl(_control);
            SizeF layoutArea = new SizeF(screen.WorkingArea.Width - SCREEN_MARGIN, screen.WorkingArea.Height - SCREEN_MARGIN);
            if (Expansion == ExpansionStyle.Vertical)
                layoutArea.Width = ItemBounds.Width;
            else if (Expansion == ExpansionStyle.Horizontal)
                layoutArea.Height = ItemBounds.Height;

            Size sizeNeeded = Size.Ceiling(g.MeasureString(TipText, Font, layoutArea));

            // If the needed width is smaller than that of the original label,
            // it can be visually confusing, so we adjust. This can only happen
            // with ExpansionStyle.Both, so we won't get here unless either the
            // height or the width is greater.
            if (sizeNeeded.Width < ItemBounds.Width)
                sizeNeeded.Width = ItemBounds.Width;

            ClientSize = sizeNeeded;
            Size = sizeNeeded + new Size(PADDING_LEFT + PADDING_RIGHT, PADDING_TOP + PADDING_BOTTOM);
            _textRect = new Rectangle(PADDING_LEFT, PADDING_TOP, sizeNeeded.Width, sizeNeeded.Height);

            // Catch mouse leaving the control
            _control.MouseLeave += new EventHandler(control_MouseLeave);

            // Catch the form that holds the control closing
            _control.FindForm().Closed += new EventHandler(control_FormClosed);

            if (Right > screen.WorkingArea.Right)
            {
                Left = Math.Max(
                    screen.WorkingArea.Right - Width - SCREEN_EDGE,
                    screen.WorkingArea.Left + SCREEN_EDGE);
            }

            if (Bottom > screen.WorkingArea.Bottom - SCREEN_EDGE)
            {
                if (Overlay)
                    Top = Math.Max(
                        screen.WorkingArea.Bottom - Height - SCREEN_EDGE,
                        screen.WorkingArea.Top + SCREEN_EDGE);

                if (Bottom > screen.WorkingArea.Bottom - SCREEN_EDGE)
                    Height = screen.WorkingArea.Bottom - SCREEN_EDGE - Top;

            }

            if (AutoCloseDelay > 0)
            {
                _autoCloseTimer = new System.Windows.Forms.Timer();
                _autoCloseTimer.Interval = AutoCloseDelay;
                _autoCloseTimer.Tick += new EventHandler(OnAutoClose);
                _autoCloseTimer.Start();
            }
        }

        #endregion

        #region Properties

        public bool Overlay { get; set; }
        public ExpansionStyle Expansion { get; set; }
        public int AutoCloseDelay { get; set; }
        public int MouseLeaveDelay { get; set; }
        public string TipText { get; set; }
        public Rectangle ItemBounds { get; set; }
        public bool WantClicks { get; set; }

        #endregion

        #region Event Handlers

        protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
        {
            base.OnPaint(e);

            Graphics g = e.Graphics;
            Rectangle outlineRect = ClientRectangle;
            outlineRect.Width--;
            outlineRect.Height--;
            g.DrawRectangle(Pens.Black, outlineRect);
            g.DrawString(TipText, Font, Brushes.Black, _textRect);
        }

        private void OnAutoClose(object sender, System.EventArgs e)
        {
            Close();
        }

        protected override void OnMouseEnter(System.EventArgs e)
        {
            if (_mouseLeaveTimer != null)
            {
                _mouseLeaveTimer.Stop();
                _mouseLeaveTimer.Dispose();
                System.Diagnostics.Debug.WriteLine("Entered TipWindow - stopped mouseLeaveTimer");
            }
        }

        protected override void OnMouseLeave(System.EventArgs e)
        {
            if (MouseLeaveDelay > 0)
            {
                _mouseLeaveTimer = new System.Windows.Forms.Timer();
                _mouseLeaveTimer.Interval = MouseLeaveDelay;
                _mouseLeaveTimer.Tick += new EventHandler(OnAutoClose);
                _mouseLeaveTimer.Start();
                System.Diagnostics.Debug.WriteLine("Left TipWindow - started mouseLeaveTimer");
            }
        }

        /// <summary>
        /// The form our label is on closed, so we should. 
        /// </summary>
        private void control_FormClosed(object sender, System.EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// The mouse left the label. We ignore if we are
        /// overlaying the label but otherwise start a
        /// delay for closing the window
        /// </summary>
        private void control_MouseLeave(object sender, System.EventArgs e)
        {
            if (MouseLeaveDelay > 0 && !Overlay)
            {
                _mouseLeaveTimer = new System.Windows.Forms.Timer();
                _mouseLeaveTimer.Interval = MouseLeaveDelay;
                _mouseLeaveTimer.Tick += new EventHandler(OnAutoClose);
                _mouseLeaveTimer.Start();
                System.Diagnostics.Debug.WriteLine("Left Control - started mouseLeaveTimer");
            }
        }

        #endregion

        [DllImport("user32.dll")]
        static extern uint SendMessage(
            IntPtr hwnd,
            int msg,
            IntPtr wparam,
            IntPtr lparam
            );

        protected override void WndProc(ref Message m)
        {
            uint WM_LBUTTONDOWN = 0x201;
            uint WM_RBUTTONDOWN = 0x204;
            uint WM_MBUTTONDOWN = 0x207;

            if (m.Msg == WM_LBUTTONDOWN || m.Msg == WM_RBUTTONDOWN || m.Msg == WM_MBUTTONDOWN)
            {
                if (m.Msg != WM_LBUTTONDOWN)
                    Close();
                SendMessage(_control.Handle, m.Msg, m.WParam, m.LParam);
            }
            else
            {
                base.WndProc(ref m);
            }
        }
    }
}
