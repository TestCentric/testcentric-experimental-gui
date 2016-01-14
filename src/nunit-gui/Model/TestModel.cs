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

using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using NUnit.Engine;

namespace NUnit.Gui.Model
{
    public class TestModel : ITestModel, ITestEventListener
    {
        private ITestEngine _testEngine;
        private TestPackage _package;
        private IDictionary<string, ResultNode> _resultIndex = new Dictionary<string, ResultNode>();

        public TestModel(ITestEngine testEngine, CommandLineOptions options)
        {
            _testEngine = testEngine;
            Options = options;
            RecentFiles = testEngine.Services.GetService<IRecentFiles>();
            Settings = new Settings.SettingsModel(testEngine.Services.GetService<ISettings>());
        }

        #region ITestModel Members

        #region Events

        // Test loading events
        //public event TestEventHandler TestLoading;
        public event TestEventHandler TestLoaded;

        //public event TestEventHandler TestReloading;
        public event TestEventHandler TestReloaded;

        //public event TestEventHandler TestUnloading;
        public event TestEventHandler TestUnloaded;

        // Test running events
        public event TestEventHandler RunStarting;
        public event TestEventHandler RunFinished;
        public event TestEventHandler RunFailed;

        public event TestEventHandler SuiteStarting;
        public event TestEventHandler SuiteFinished;

        public event TestEventHandler TestStarting;
        public event TestEventHandler TestFinished;

        public event TestEventHandler TestException;
        public event TestEventHandler TestOutput;

        // Test Selection Event
        public event EventHandler SelectedTestChanged;

        #endregion

        #region Properties

        public CommandLineOptions Options { get; private set; }

        public IRecentFiles RecentFiles { get; private set; }

        public ITestRunner Runner { get; private set; }

        public bool IsPackageLoaded { get { return _package != null; } }

        public TestNode Tests { get; private set; }
        public bool HasTests { get { return Tests != null; } }

        public bool IsTestRunning
        {
            get { return Runner != null && Runner.IsTestRunning;  }
        }

        public bool HasResults
        {
            get { return _resultIndex.Count > 0; }
        }

        private ITestItem _selectedTest;
        private IList<string> _files;

        public ITestItem SelectedTest
        {
            get { return _selectedTest; }
            set
            {
                _selectedTest = value;
                if (SelectedTestChanged != null)
                    SelectedTestChanged(this, new EventArgs());
            }
        }

        public Settings.SettingsModel Settings { get; private set; }

        // TODO: We are directly using an engine class here rather
        // than going through the API - need to fix this.
        private List<RuntimeFramework> _runtimes;
        public IList<RuntimeFramework> AvailableRuntimes
        {
            get
            {
                if (_runtimes == null)
                {
                    _runtimes = new List<RuntimeFramework>();
                    foreach (var runtime in RuntimeFramework.AvailableFrameworks)
                        _runtimes.Add(runtime);
                    _runtimes.Sort((x, y) => x.DisplayName.CompareTo(y.DisplayName));
                }

                return _runtimes;
            }
        }

        #endregion

        #region Methods

        public void OnStartup()
        {
            if (Options.InputFiles.Count > 0)
            {
                LoadTests(Options.InputFiles);
            }
            else if (!Options.NoLoad && RecentFiles.Entries.Count > 0)
            {
                var entry = RecentFiles.Entries[0];
                if (!string.IsNullOrEmpty(entry) && System.IO.File.Exists(entry))
                    LoadTests(new[] { entry });
            }

            if (Options.RunAllTests && IsPackageLoaded)
                RunTests(TestFilter.Empty);
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public void NewProject()
        {
            NewProject("Dummy");
        }

        public void NewProject(string filename)
        {
            MessageBox.Show("It's not yet decided if we will implement saving of projects. The alternative is to require use of the project editor, which already supports this.", "Not Yet Implemented");
        }

        public void SaveProject()
        {
            MessageBox.Show("It's not yet decided if we will implement saving of projects. The alternative is to require use of the project editor, which already supports this.", "Not Yet Implemented");
        }

        public void LoadTests(IList<string> files)
        {
            if (IsPackageLoaded)
                UnloadTests();

            _files = files;
            _package = MakeTestPackage(files);

            //if (TestLoading != null)
            //    TestLoading(new TestEventArgs(TestAction.TestLoading));

            try
            {
                Runner = _testEngine.GetRunner(_package);
                // TODO: Error here when multiple files are run
                Tests = new TestNode(Runner.Explore(TestFilter.Empty));

                _resultIndex.Clear();
            }
            catch (Exception ex)
            {
                //if (TestException != null)
                //    TestException(new TestEventArgs(TestAction.TestLoadFailed, ex));
            }

            if (TestLoaded != null)
                TestLoaded(new TestEventArgs(TestAction.TestLoaded, Tests));

            foreach (var subPackage in _package.SubPackages)
                RecentFiles.SetMostRecent(subPackage.FullName);
        }

        public void UnloadTests()
        {
            //if (TestUnloading != null)
            //    TestUnloading(new TestEventArgs(TestAction.TestUnloading));

            try
            {
                Runner.Unload();
                Tests = null;
                _package = null;
                _files = null;
                _resultIndex.Clear();
            }
            catch (Exception ex)
            {
                //if (TestException != null)
                //    TestException(new TestEventArgs(TestAction.TestUnloadFailed, ex));
            }

            if (TestUnloaded != null)
                TestUnloaded(new TestEventArgs(TestAction.TestUnloaded));
        }

        public void ReloadTests()
        {
            //if (TestReloading != null)
            //    TestReloading(new TestEventArgs(TestAction.TestReloading));
            Runner.Unload();
            _resultIndex.Clear();
            Tests = null;

            try
            {
                _package = MakeTestPackage(_files);
                Runner = _testEngine.GetRunner(_package);
                // TODO: Error here when multiple files are run
                Tests = new TestNode(Runner.Explore(TestFilter.Empty));

                _resultIndex.Clear();
            }
            catch (Exception ex)
            {
                //if (TestException != null)
                //    TestException(new TestEventArgs(TestAction.TestLoadFailed, ex));
            }

            if (TestReloaded != null)
                TestReloaded(new TestEventArgs(TestAction.TestReloaded, Tests));
        }


        #region Helper Methods

        private TestPackage MakeTestPackage(string name)
        {
            var package = new TestPackage(name);
            ApplyOptionsToPackage(package);
            return package;
        }

        private TestPackage MakeTestPackage(IList<string> testFiles)
        {
            var package = new TestPackage(testFiles);
            ApplyOptionsToPackage(package);
            return package;
        }

        private TestPackage ApplyOptionsToPackage(TestPackage package)
        {
            // TODO: Remove temporary Settings used in testing GUI
            package.Settings["ProcessModel"] = "InProcess";
            package.Settings["NumberOfTestWorkers"] = 0;

            //if (options.ProcessModel != null)
            //    package.AddSetting("ProcessModel", options.ProcessModel);

            //if (options.DomainUsage != null)
            //    package.AddSetting("DomainUsage", options.DomainUsage);

            //if (options.ActiveConfig != null)
            //    package.AddSetting("ActiveConfig", options.ActiveConfig);

            if (Options.InternalTraceLevel != null)
                package.Settings["InternalTraceLevel"] = Options.InternalTraceLevel;

            return package;
        }

        #endregion


        public void RunTests(ITestItem testItem)
        {
            if (testItem != null)
                RunTests(testItem.GetTestFilter());
        }

        public void RunSelectedTest()
        {
            RunTests(this.SelectedTest);
        }

        public void RunTests(TestFilter filter)
        {
            try
            {
                Runner.RunAsync(this, filter);
            }
            catch (Exception ex)
            {
                //if (RunFailed != null)
                //    RunFailed(new TestEventArgs(TestAction.RunFailed, ex));
            }
        }

        public void CancelTestRun()
        {
            Runner.StopRun(false);
        }

        public ResultNode GetResultForTest(TestNode testNode)
        {
            if (testNode != null)
            {
                ResultNode result;
                if (_resultIndex.TryGetValue(testNode.Id, out result))
                    return result;
            }

            return null;
        }

        #endregion

        #endregion

        #region IServiceLocator Members

        public T GetService<T>() where T: class
        {
            return _testEngine.Services.GetService<T>();
        }

        public object GetService(Type serviceType)
        {
            return _testEngine.Services.GetService(serviceType);
        }

        #endregion

        #region ITestEventListener Members

        public void OnTestEvent(string report)
        {
            XmlNode xmlNode = XmlHelper.CreateXmlNode(report);

            switch (xmlNode.Name)
            {
                case "start-test":
                    if (TestStarting != null)
                        TestStarting(new TestEventArgs(TestAction.TestStarting, new TestNode(xmlNode)));
                    break;

                case "start-suite":
                    if (SuiteStarting != null)
                        SuiteStarting(new TestEventArgs(TestAction.SuiteStarting, new TestNode(xmlNode)));
                    break;

                case "start-run":
                    if (RunStarting != null)
                        RunStarting(new TestEventArgs(TestAction.RunStarting, xmlNode.GetAttribute("count", -1)));
                    break;

                case "test-case":
                    ResultNode result = new ResultNode(xmlNode);
                    _resultIndex[result.Id] = result;
                    if (TestFinished != null)
                        TestFinished(new TestEventArgs(TestAction.TestFinished, result));
                    break;

                case "test-suite":
                    result = new ResultNode(xmlNode);
                    _resultIndex[result.Id] = result;
                    if (SuiteFinished != null)
                        SuiteFinished(new TestEventArgs(TestAction.TestFinished, result));
                    break;

                case "test-run":
                    result = new ResultNode(xmlNode);
                    _resultIndex[result.Id] = result;
                    if (RunFinished != null)
                        RunFinished(new TestEventArgs(TestAction.RunFinished, result));
                    break;
            }
        }

        #endregion
    }
}
