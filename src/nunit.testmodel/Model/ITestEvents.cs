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

namespace TestCentric.Gui.Model
{
    /// <summary>
    /// Delegates for all events related to the model
    /// </summary>
    public delegate void TestEventHandler(TestEventArgs args);
    public delegate void RunStartingEventHandler(RunStartingEventArgs args);
    public delegate void TestNodeEventHandler(TestNodeEventArgs args);
    public delegate void TestResultEventHandler(TestResultEventArgs args);
    public delegate void TestItemEventHandler(TestItemEventArgs args);
    public delegate void TestOutputEventHandler(TestOutputEventArgs args);
    public delegate void TestFilesLoadingEventHandler(TestFilesLoadingEventArgs args);

    /// <summary>
    /// ITestEvents provides events for all actions in the model, both those
    /// caused by an engine action like a test completion and those originated
    /// in the model itself like starting a test run.
    /// </summary>
    public interface ITestEvents
    {
        // Events related to loading and unloading tests.
        event TestFilesLoadingEventHandler TestsLoading;
        event TestEventHandler TestsReloading;
        event TestEventHandler TestsUnloading;

        event TestNodeEventHandler TestLoaded;
        event TestNodeEventHandler TestReloaded;
        event TestEventHandler TestUnloaded;

        // Events related to running tests
        event RunStartingEventHandler RunStarting;
        event TestNodeEventHandler SuiteStarting;
        event TestNodeEventHandler TestStarting;

        event TestResultEventHandler RunFinished;
        event TestResultEventHandler SuiteFinished;
        event TestResultEventHandler TestFinished;

        event TestOutputEventHandler TestOutput;

        // Event used to broadcast a change in the selected
        // item, so that all presenters may be notified.
        event TestItemEventHandler SelectedItemChanged;

        event TestEventHandler CategorySelectionChanged;
    }
}
