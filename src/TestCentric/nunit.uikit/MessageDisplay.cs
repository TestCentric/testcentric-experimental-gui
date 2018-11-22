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
            _defaultCaption = caption;
        }

        #region Public Methods

        #region Display

        public DialogResult Display(string message)
        {
            return Display(message, _defaultCaption, MessageButtons.OK);
        }

        public DialogResult Display(string message, string caption)
        {
            return Display(message, caption, MessageButtons.OK);
        }

        public DialogResult Display(string message, MessageButtons buttons)
        {
            return Display(message, _defaultCaption, buttons);
        }

        public DialogResult Display(string message, string caption, MessageButtons buttons)
        {
            return ShowMessageBox(message, caption, buttons, MessageBoxIcon.None);
        }

        #endregion

        #region Error

        public DialogResult Error(string message)
        {
            return Error(message, _defaultCaption, MessageButtons.OK);
        }

        public DialogResult Error(string message, string caption)
        {
            return Error(message, caption, MessageButtons.OK);
        }

        public DialogResult Error(string message, MessageButtons buttons)
        {
            return Error(message, _defaultCaption, buttons);
        }

        public DialogResult Error(string message, string caption, MessageButtons buttons)
        {
            return ShowMessageBox(message, caption, buttons, MessageBoxIcon.Stop);
        }

        public DialogResult Error(string message, Exception exception)
        {
            return Error(message, exception, MessageButtons.OK);
        }

        public DialogResult Error(string message, Exception exception, MessageButtons buttons)
        {
            return Error(BuildMessage(message, exception), buttons);
        }

        #endregion

        #region Info

        public DialogResult Info(string message)
        {
            return Info(message, MessageButtons.OK);
        }

        public DialogResult Info(string message, string caption)
        {
            return Info(message, caption, MessageButtons.OK);
        }

        public DialogResult Info(string message, MessageButtons buttons)
        {
            return Info(message, _defaultCaption, buttons);
        }

        public DialogResult Info(string message, string caption, MessageButtons buttons)
        {
            return ShowMessageBox(message, caption, buttons, MessageBoxIcon.Information);
        }

        #endregion

        #region Ask

        public DialogResult Ask(string message)
        {
            return ShowMessageBox(message, _defaultCaption, MessageButtons.YesNo, MessageBoxIcon.Question);
        }

        public DialogResult Ask(string message, string caption)
        {
            return ShowMessageBox(message, caption, MessageButtons.YesNo, MessageBoxIcon.Question);
        }

        #endregion

        #endregion

        #region Helper Methods

        private static DialogResult ShowMessageBox(string message, string caption, MessageButtons buttons, MessageBoxIcon icon)
        {
            var messageBoxButtons =
                buttons == MessageButtons.YesNo
                    ? MessageBoxButtons.YesNo
                    : buttons == MessageButtons.OKCancel
                        ? MessageBoxButtons.OKCancel
                        : MessageBoxButtons.OK;

            var result = MessageBox.Show(message, caption, messageBoxButtons, icon);

            switch (result)
            {
                default:
                case System.Windows.Forms.DialogResult.None:
                    return DialogResult.None;
                case System.Windows.Forms.DialogResult.OK:
                    return DialogResult.OK;
                case System.Windows.Forms.DialogResult.Cancel:
                    return DialogResult.Cancel;
                case System.Windows.Forms.DialogResult.Yes:
                    return DialogResult.Yes;
                case System.Windows.Forms.DialogResult.No:
                    return DialogResult.No;
            }
        }

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

        private static string BuildMessage(string message, Exception exception)
        {
            return message + Environment.NewLine + Environment.NewLine + BuildMessage(exception);
        }

        #endregion
    }
}
