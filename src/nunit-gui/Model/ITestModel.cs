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

using System.Collections.Generic;
using NUnit.Engine;

namespace NUnit.Gui.Model
{
    /// <summary>
    /// The delegate for all events related to running tests
    /// </summary>
    public delegate void TestEventHandler(TestEventArgs args);

    public interface ITestModel : IServiceLocator, System.IDisposable
    {
        #region Events

        // Events related to loading and unloading tests.
        //event TestEventHandler TestLoading;
        event TestEventHandler TestLoaded;

        //event TestEventHandler TestReloading;
        event TestEventHandler TestReloaded;

        //event TestEventHandler TestUnloading;
        event TestEventHandler TestUnloaded;

        // Events related to a running a set of tests
        event TestEventHandler RunStarting;
        event TestEventHandler SuiteStarting;
        event TestEventHandler TestStarting;

        event TestEventHandler RunFinished;
        event TestEventHandler SuiteFinished;
        event TestEventHandler TestFinished;

        event TestEventHandler RunFailed;

        /// <summary>
        /// An unhandled exception was thrown during a test run,
        /// and it cannot be associated with a particular test failure.
        /// </summary>
        event TestEventHandler TestException;

        /// <summary>
        /// Console Out/Error
        /// </summary>
        event TestEventHandler TestOutput;

        event System.EventHandler SelectedTestChanged;

        #endregion

        #region Properties

        bool IsPackageLoaded { get; }

        // TestNode hierarchy representing the discovered tests
        TestNode Tests { get; }

        // See if tests are available
        bool HasTests { get; }

        // See if a test is running
        bool IsTestRunning { get; }

        //// Our last test results
        //TestResult TestResult { get; }

        bool HasResults { get; }

        // The currently selected test item
        ITestItem SelectedTest { get; set; }

        Settings.SettingsModel Settings { get; }

        IList<RuntimeFramework> AvailableRuntimes { get; }

        #endregion

        #region Methods

        // Create a new empty project using a default name
        void NewProject();

        // Create a new project given a filename
        void NewProject(string filename);

        void SaveProject();

        // Load a TestPackage
        void LoadTests(TestPackage package);

        // Unload current TestPackage
        void UnloadTests();

        // Reload current TestPackage
        void ReloadTests();

        // Run just the specified ITestItem
        void RunTests(ITestItem testItem);

        // Run the currently selected test or group
        void RunSelectedTest();

        // Run the tests in the current TestPackage
        // using the provided filter.
        void RunTests( NUnit.Engine.TestFilter filter );

        // Cancel the running test
        void CancelTestRun();

        // Get the result for a test if available
        ResultNode GetResultForTest(TestNode testNode);
        
        #endregion
    }
}
