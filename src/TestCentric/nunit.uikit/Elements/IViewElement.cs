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

using System.Windows.Forms;

namespace NUnit.UiKit.Elements
{
    /// <summary>
    /// The IViewElement interface wraps an individual gui
    /// item like a control or toolstrip item. It is generally
    /// exposed by views and is the base of other interfaces
    /// in the NUnit.UiKit.Elements namespace.
    /// </summary>
    public interface IViewElement
    {
        /// <summary>
        /// Gets the Name of the element in the progressBar
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets or sets the Enabled status of the element
        /// </summary>
        bool Enabled { get; set; }

        /// <summary>
        /// Gets or sets the Visible status of the element
        /// </summary>
        bool Visible { get; set; }

        /// <summary>
        /// Gets or sets the Text of an element
        /// </summary>
        string Text { get; set; }

        /// <summary>
        /// Invoke a delegate if necessary, otherwise just call it
        /// </summary>
        void InvokeIfRequired(MethodInvoker _delegate);
    }
}
