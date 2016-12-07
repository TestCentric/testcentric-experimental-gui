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

        #region Constructor

        public TestModel(ITestEngine testEngine, CommandLineOptions options)
        {
            _testEngine = testEngine;
            Options = options;
            RecentFiles = testEngine.Services.GetService<IRecentFiles>();
            Settings = new Settings.SettingsModel(testEngine.Services.GetService<ISettings>());
        }

        #endregion

        #region ITestModel Members

        #region Events

        // Test loading events
        public event TestNodeEventHandler TestLoaded;
        public event TestNodeEventHandler TestReloaded;
        public event TestEventHandler TestUnloaded;

        // Test running events
        public event RunStartingEventHandler RunStarting;
        public event TestResultEventHandler RunFinished;

        public event TestNodeEventHandler SuiteStarting;
        public event TestResultEventHandler SuiteFinished;

        public event TestNodeEventHandler TestStarting;
        public event TestResultEventHandler TestFinished;

        // Test Selection Event
        public event TestItemEventHandler SelectedItemChanged;

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

        private IList<string> _files;

        public Settings.SettingsModel Settings { get; private set; }

        // TODO: We are directly using an engine class here rather
        // than going through the API - need to fix this.
        // Note that the engine returns more values than we really
        // want to list. For example, we don't distinguish between
        // client and full profiles when executing tests. So we do
        // a lot of manipulation of this list here. Even if some of
        // the problems in the engine are fixed, we may have to
        // retain this code in order to work with older engines.
        private List<RuntimeFramework> _runtimes;
        public IList<RuntimeFramework> AvailableRuntimes
        {
            get
            {
                if (_runtimes == null)
                {
                    _runtimes = new List<RuntimeFramework>();
                    foreach (var runtime in RuntimeFramework.AvailableFrameworks)
                    {
                        // Nothing below 2.0
                        if (runtime.ClrVersion.Major < 2)
                            continue;

                        // Remove erroneous entries for 4.5 Client profile
                        if (runtime.FrameworkVersion.Major == 4 && runtime.FrameworkVersion.Minor > 0)
                            if (runtime.DisplayName.EndsWith("Client"))
                                continue;

                        // Skip duplicates
                        var duplicate = false;
                        foreach (var rt in _runtimes)
                            if (rt.DisplayName == runtime.DisplayName)
                            {
                                duplicate = true;
                                break;
                            }

                        if (!duplicate)
                            _runtimes.Add(runtime);
                    }

                    _runtimes.Sort((x, y) => x.DisplayName.CompareTo(y.DisplayName));

                    // Now eliminate child entries where full entry follows
                    for (int i = _runtimes.Count - 2; i >=0; i--)
                    {
                        var rt1 = _runtimes[i];
                        var rt2 = _runtimes[i + 1];

                        if (rt1.Runtime != rt2.Runtime)
                            continue;
                        if (rt1.FrameworkVersion != rt2.FrameworkVersion)
                            continue;

                        if (_runtimes[i].DisplayName.EndsWith(" - Client") &&
                            _runtimes[i+1].DisplayName.EndsWith(" - Full"))
                        {
                            _runtimes.RemoveAt(i);
                        }
                    }
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
                RunAllTests();
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

            Runner = _testEngine.GetRunner(_package);

            Tests = new TestNode(Runner.Explore(TestFilter.Empty));

            _resultIndex.Clear();

            if (TestLoaded != null)
                TestLoaded(new TestNodeEventArgs(TestAction.TestLoaded, Tests));

            foreach (var subPackage in _package.SubPackages)
                RecentFiles.SetMostRecent(subPackage.FullName);
        }

        public void UnloadTests()
        {
            Runner.Unload();
            Tests = null;
            _package = null;
            _files = null;
            _resultIndex.Clear();

            if (TestUnloaded != null)
                TestUnloaded(new TestEventArgs(TestAction.TestUnloaded));
        }

        public void ReloadTests()
        {
            ReloadTests(null);
        }

        public void ReloadTests(string runtime)
        {
            Runner.Unload();
            _resultIndex.Clear();
            Tests = null;

            _package = MakeTestPackage(_files);
            if (runtime != null)
                _package.Settings["RuntimeFramework"] = runtime;
            Runner = _testEngine.GetRunner(_package);

            Tests = new TestNode(Runner.Explore(TestFilter.Empty));

            _resultIndex.Clear();

            if (TestReloaded != null)
                TestReloaded(new TestNodeEventArgs(TestAction.TestReloaded, Tests));
        }

        #region Helper Methods

        private TestPackage MakeTestPackage(IList<string> testFiles)
        {
            var package = new TestPackage(testFiles);

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

        public void RunAllTests()
        {
            // Temp fix
            //Runner.RunAsync(this, TestFilter.Empty);
            Runner.Run(this, TestFilter.Empty);
        }

        public void RunTests(ITestItem testItem)
        {
            if (testItem != null)
                Runner.RunAsync(this, testItem.GetTestFilter());
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

        public void NotifySelectedItemChanged(ITestItem testItem)
        {
            if (SelectedItemChanged != null)
                SelectedItemChanged(new TestItemEventArgs(testItem));
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
                        TestStarting(new TestNodeEventArgs(TestAction.TestStarting, new TestNode(xmlNode)));
                    break;

                case "start-suite":
                    if (SuiteStarting != null)
                        SuiteStarting(new TestNodeEventArgs(TestAction.SuiteStarting, new TestNode(xmlNode)));
                    break;

                case "start-run":
                    if (RunStarting != null)
                        RunStarting(new RunStartingEventArgs(xmlNode.GetAttribute("count", -1)));
                    break;

                case "test-case":
                    ResultNode result = new ResultNode(xmlNode);
                    _resultIndex[result.Id] = result;
                    if (TestFinished != null)
                        TestFinished(new TestResultEventArgs(TestAction.TestFinished, result));
                    break;

                case "test-suite":
                    result = new ResultNode(xmlNode);
                    _resultIndex[result.Id] = result;
                    if (SuiteFinished != null)
                        SuiteFinished(new TestResultEventArgs(TestAction.TestFinished, result));
                    break;

                case "test-run":
                    result = new ResultNode(xmlNode);
                    _resultIndex[result.Id] = result;
                    if (RunFinished != null)
                        RunFinished(new TestResultEventArgs(TestAction.RunFinished, result));
                    break;
            }
        }

        #endregion
    }
}
