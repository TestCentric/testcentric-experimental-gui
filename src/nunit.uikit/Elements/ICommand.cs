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
    /// CommandHandler is used to request an action
    /// </summary>
    public delegate void CommandHandler();

    /// <summary>
    /// CommandHandler<typeparamref name="T"/> is used to request an action
    /// taking a single argument/>
    /// </summary>
    public delegate void CommandHandler<T>(T arg);

    /// <summary>
    /// The ICommand interface represents a menu toolStripItem,
    /// which executes a command.
    /// </summary>
    public interface ICommand : IViewElement
    {
        /// <summary>
        /// Execute event is raised to signal the presenter
        /// to execute the command with which this menu
        /// toolStripItem is associated.
        /// </summary>
        event CommandHandler Execute;

        string ToolTipText { get; set; }
    }

    public interface ICommand<T> : IViewElement
    {
        event CommandHandler<T> Execute;

        string ToolTipText { get; set; }
    }
}
