// ***********************************************************************
// Copyright (c) 2017 Charlie Poole
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

namespace NUnit.Gui.Views
{
    internal class XmlTextBoxView : IXmlTextBoxView
    {
        private RichTextBox _xmlTextBox;

        public XmlTextBoxView(RichTextBox xmlTextBox)
        {
            _xmlTextBox = xmlTextBox;
        }

        public bool WordWrap { get { return _xmlTextBox.WordWrap; } set { _xmlTextBox.WordWrap = value; } }

        public string SelectedText { get { return _xmlTextBox.SelectedText; } }

        public void Copy()
        {
            _xmlTextBox.Copy();
        }

        public void Focus()
        {
            _xmlTextBox.Focus();
        }

        public void SelectAll()
        {
            _xmlTextBox.SelectAll();
        }
    }
}