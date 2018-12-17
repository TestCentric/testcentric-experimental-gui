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

namespace TestCentric.Gui.Model
{
    public interface ITestModel : System.IDisposable
    {
        #region General Properties

        // Event Dispatcher
        ITestEvents Events { get; }

        // Engine Services
        ITestServices Services { get; }

        // Project Support
        bool NUnitProjectSupport { get; }
        bool VisualStudioSupport { get; }

        // List of available runtimes, based on the engine's list
        // but filtered to meet the GUI's requirements
        IList<IRuntimeFramework> AvailableRuntimes { get; }

        // Result Format Support
        IEnumerable<string> ResultFormats { get; }

        #endregion

        #region Current State of the Model

        bool IsPackageLoaded { get; }

        IDictionary<string, object> PackageSettings { get; }

        // TestNode hierarchy representing the discovered tests
        TestNode Tests { get; }

        // See if tests are available
        bool HasTests { get; }

        // See if a test is running
        bool IsTestRunning { get; }

        // Do we have results from running the test?
        bool HasResults { get; }

        #endregion

        #region Methods

        // Create a new empty project using a default name
        void NewProject();

        // Create a new project given a filename
        void NewProject(string filename);

        void SaveProject();

        // Load a TestPackage
        void LoadTests(IList<string> files);

        // Unload current TestPackage
        void UnloadTests();

        // Reload current TestPackage
        void ReloadTests();

        // Run all the tests
        void RunAllTests();

        // Run just the specified ITestItem
        void RunTests(ITestItem testItem);

        // Cancel the running test
        void CancelTestRun();

        // Get the result for a test if available
        ResultNode GetResultForTest(string id);

        // Broadcast event when SelectedTestItem changes
        void NotifySelectedItemChanged(ITestItem testItem);

        #endregion
    }
}
