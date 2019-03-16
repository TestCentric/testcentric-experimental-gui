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

namespace TestCentric.Gui.Views
{
    using Elements;

    // Interface used for testing
    public interface ITestTreeView : IView
    {
        ICommand RunButton { get; }
        ICommand RunAllCommand { get; }
        ICommand RunSelectedCommand { get; }
        ICommand RunFailedCommand { get; }
        ICommand StopRunCommand { get; }

        ICommand DebugButton { get; }
        ICommand DebugAllCommand { get; }
        ICommand DebugSelectedCommand { get; }
        ICommand DebugFailedCommand { get; }

        IToolTip FormatButton { get; }
        ISelection DisplayFormat { get; }
        ISelection GroupBy { get; }

        ICommand RunContextCommand { get; }
        ICommand RunCheckedCommand { get; }
        ICommand DebugContextCommand { get; }
        ICommand DebugCheckedCommand { get; }
        IChecked ShowCheckBoxes { get; }
        ICommand ExpandAllCommand { get; }
        ICommand CollapseAllCommand { get; }
        ICommand CollapseToFixturesCommand { get; }

        void ExpandAll();
        void CollapseAll();

        ITreeView Tree { get; }
    }
}
