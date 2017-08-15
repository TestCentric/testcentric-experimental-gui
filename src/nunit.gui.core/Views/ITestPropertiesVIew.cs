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

using NUnit.UiKit.Elements;

namespace NUnit.Gui.Views
{
    public interface ITestPropertiesView : IView
    {
        event CommandHandler DisplayHiddenPropertiesChanged;

        bool Visible { get; set; }
        string Header { get; set; }
        IViewElement TestPanel { get; }
        IViewElement ResultPanel { get; }

        string TestType { get; set; }
        string FullName { get; set; }
        string Description { get; set; }
        string Categories { get; set; }
        string TestCount { get; set; }
        string RunState { get; set; }
        string SkipReason { get; set; }
        bool DisplayHiddenProperties { get; }
        string Properties { get; set; }
        string Outcome { get; set; }
        string ElapsedTime { get; set; }
        string AssertCount { get; set; }
        string Message { get; set; }
        string StackTrace { get; set; }
        string Output { get; set; }
    }
}
