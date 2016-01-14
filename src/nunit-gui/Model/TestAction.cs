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

namespace NUnit.Gui.Model
{
    /// <summary>
    /// The TestAction enumeration is used to distiguish events
    /// that are fired during the execution of a test run.
    /// </summary>
    public enum TestAction
    {
        TestLoaded,
        TestUnloaded,
        TestReloaded,

        /// <summary>
        /// The test run is starting.
        /// </summary>
        RunStarting,

        /// <summary>
        /// The test run has completed.
        /// </summary>
        RunFinished,

        // TODO: DO we need this or is an exception sufficient?
        /// <summary>
        /// The test run failed to complete due to a catastrophic error.
        /// </summary>
        RunFailed,

        /// <summary>
        /// A suite of tests is about to execute.
        /// </summary>
        SuiteStarting,

        /// <summary>
        /// A suite of tests has completed execution.
        /// </summary>
        SuiteFinished,

        /// <summary>
        /// A single test case is about to execute.
        /// </summary>
        TestStarting,

        /// <summary>
        /// A single test case has completed execution.
        /// </summary>
        TestFinished,

        /// <summary>
        /// An unhandled exception was fired during the execution
        /// of a test run and it isn't possible to determine which
        /// teset caused it.
        /// </summary>
        TestException,

        /// <summary>
        /// A test has created some text output.
        /// </summary>
        TestOutput
    }
}
