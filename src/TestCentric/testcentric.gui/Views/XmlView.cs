// ***********************************************************************
// Copyright (c) 2016 Charlie Poole
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
using System.Xml;

namespace TestCentric.Gui.Views
{
    using Elements;

    public partial class XmlView : UserControl, IXmlView
    {
        private XmlNode _testXml;
        public event CommandHandler SelectAllCommand;
        public event CommandHandler SelectionChanged;
        public event CommandHandler CopyCommand;
        public event CommandHandler WordWrapChanged;
        public event CommandHandler ViewGotFocus;

        public XmlView()
        {
            InitializeComponent();

            XmlPanel = new ControlElement(xmlPanel);
            CopyToolStripMenuItem = new ToolStripMenuElement(copyToolStripMenuItem);
            WordWrapToolStripMenuItem = new ToolStripMenuElement(wordWrapToolStripMenuItem);
            selectAllToolStripMenuItem.Click += (s, a) =>
            {
                if (SelectAllCommand != null)
                    SelectAllCommand();
            };

            xmlTextBox.SelectionChanged += (s, a) =>
            {
                if (SelectionChanged != null)
                    SelectionChanged();
            };

            copyToolStripMenuItem.Click += (s, a) =>
            {
                if (CopyCommand != null)
                    CopyCommand();
            };

            wordWrapToolStripMenuItem.CheckedChanged += (s, a) =>
            {
                if (WordWrapChanged != null)
                    WordWrapChanged();
            };
        }

        public string Header
        {
            get { return header.Text; }
            set { InvokeIfRequired(() => { header.Text = value; }); }
        }

        public IViewElement XmlPanel { get; private set; }

        public ICommand CopyToolStripMenuItem { get; private set; }

        public IChecked WordWrapToolStripMenuItem { get; private set; }

        public bool WordWrap
        {
            get { return xmlTextBox.WordWrap; }
            set { InvokeIfRequired(() => xmlTextBox.WordWrap = value ); }
        }

        public XmlNode TestXml
        {
            get { return _testXml; }
            set
            {
                _testXml = value;
                InvokeIfRequired(() => xmlTextBox.Rtf = TestXml != null ? XmlHelper.ToRtfString(_testXml, 2) : "");
            }
        }

        public string SelectedText
        {
            get { return xmlTextBox.SelectedText; }
            set { InvokeIfRequired(() => xmlTextBox.SelectedText = value); }
        }

        public void SelectAll()
        {
            xmlTextBox.Focus();
            xmlTextBox.SelectAll();
        }

        public void Copy()
        {
            xmlTextBox.Copy();
        }

        public void InvokeFocus()
        {
            ViewGotFocus?.Invoke();
        }

        #region Helper Methods

        private void InvokeIfRequired(MethodInvoker _delegate)
        {
            if (this.InvokeRequired)
                this.BeginInvoke(_delegate);
            else
                _delegate();
        }

        #endregion

    }
}