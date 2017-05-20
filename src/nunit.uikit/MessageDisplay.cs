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
using System.Text;
using System.Windows.Forms;

namespace NUnit.UiKit
{
    /// <summary>
    /// Summary description for MessageDisplay.
    /// </summary>
    public class MessageDisplay : IMessageDisplay
    {
        private static readonly string DEFAULT_CAPTION = "NUnit";

        private readonly string _defaultCaption;

        public MessageDisplay() : this(DEFAULT_CAPTION) { }

        public MessageDisplay(string caption)
        {
            this._defaultCaption = caption;
        }

        #region Public Methods

        #region Display

        public DialogResult Display(string message)
        {
            return Display(message, _defaultCaption, MessageBoxButtons.OK);
        }

        public DialogResult Display(string message, string caption)
        {
            return Display(message, caption, MessageBoxButtons.OK);
        }

        public DialogResult Display(string message, MessageBoxButtons buttons)
        {
            return MessageBox.Show(message, _defaultCaption, buttons);
        }

        public DialogResult Display(string message, string caption, MessageBoxButtons buttons)
        {
            return MessageBox.Show(message, caption, buttons, MessageBoxIcon.None);
        }

        #endregion

        #region Error

        public DialogResult Error(string message)
        {
            return Error(message, _defaultCaption, MessageBoxButtons.OK);
        }

        public DialogResult Error(string message, string caption)
        {
            return Error(message, caption, MessageBoxButtons.OK);
        }

        public DialogResult Error(string message, MessageBoxButtons buttons)
        {
            return MessageBox.Show(message, _defaultCaption, buttons);
        }

        public DialogResult Error(string message, string caption, MessageBoxButtons buttons)
        {
            return MessageBox.Show(message, caption, buttons, MessageBoxIcon.Stop);
        }

        public DialogResult Error(string message, Exception exception)
        {
            return Error(message, exception, MessageBoxButtons.OK);
        }

        public DialogResult Error(string message, Exception exception, MessageBoxButtons buttons)
        {
            return Error( BuildMessage(message, exception, false), buttons);
        }

        public DialogResult FatalError(string message, Exception exception)
        {
            return Error( BuildMessage(message, exception, true), MessageBoxButtons.OK);
        }

        #endregion

        #region Info

        public DialogResult Info(string message)
        {
            return Info(message, MessageBoxButtons.OK);
        }

        public DialogResult Info(string message, string caption)
        {
            return Info(message, caption, MessageBoxButtons.OK);
        }

        public DialogResult Info(string message, MessageBoxButtons buttons)
        {
            return Info(message, _defaultCaption, buttons);
        }

        public DialogResult Info(string message, string caption, MessageBoxButtons buttons)
        {
            return MessageBox.Show(message, caption, buttons, MessageBoxIcon.Information);
        }

        #endregion

        #region Ask

        public DialogResult Ask(string message)
        {
            return Ask(message, _defaultCaption, MessageBoxButtons.YesNo);
        }

        public DialogResult Ask(string message, string caption)
        {
            return Ask(message, caption, MessageBoxButtons.YesNo);
        }

        public DialogResult Ask(string message, MessageBoxButtons buttons)
        {
            return Ask(message, _defaultCaption, buttons);
        }

        public DialogResult Ask(string message, string caption, MessageBoxButtons buttons)
        {
            return MessageBox.Show(message, caption, buttons, MessageBoxIcon.Question);
        }

        #endregion

        #endregion

        #region Helper Methods

        private static string BuildMessage(Exception exception)
        {
            Exception ex = exception;
            StringBuilder sb = new StringBuilder();

            sb.AppendFormat("{0} : {1}", ex.GetType().ToString(), ex.Message);

            while (ex.InnerException != null)
            {
                ex = ex.InnerException;
                sb.AppendFormat("\r----> {0} : {1}", ex.GetType().ToString(), ex.Message);
            }

            return sb.ToString();
        }

        private static string BuildMessage(string message, Exception exception, bool isFatal)
        {
            string msg = message + Environment.NewLine + Environment.NewLine + BuildMessage(exception);

            return isFatal
                ? msg
                : msg + Environment.NewLine + Environment.NewLine + "For further information, use the Exception Details menu item.";
        }

        #endregion
    }
}
