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
using NUnit.Engine;

namespace TestCentric.Gui.Model
{
    public class TestModel : ITestModel
    {
        private const string PROJECT_LOADER_EXTENSION_PATH = "/NUnit/Engine/TypeExtensions/IProjectLoader";
        private const string NUNIT_PROJECT_LOADER = "NUnit.Engine.Services.ProjectLoaders.NUnitProjectLoader";
        private const string VISUAL_STUDIO_PROJECT_LOADER = "NUnit.Engine.Services.ProjectLoaders.VisualStudioProjectLoader";

        private ITestEngine _testEngine;
        private TestPackage _package;
        internal IDictionary<string, ResultNode> Results = new Dictionary<string, ResultNode>();

        // Our event dispatcher. Events are exposed through the Events
        // property. This is used when firing events from the model.
        private TestEventDispatcher _events;

        #region Constructor

        public TestModel(ITestEngine testEngine, CommandLineOptions options)
        {
            _testEngine = testEngine;
            Services = new TestServices(testEngine);
            Options = options;

            foreach (var node in Services.ExtensionService.GetExtensionNodes(PROJECT_LOADER_EXTENSION_PATH))
            {
                if (node.TypeName == NUNIT_PROJECT_LOADER)
                    NUnitProjectSupport = true;
                else if (node.TypeName == VISUAL_STUDIO_PROJECT_LOADER)
                    VisualStudioSupport = true;
            }

            _events = new TestEventDispatcher(this);
        }

        #endregion

        #region ITestModel Members

        // Event Dispatcher
        public ITestEvents Events { get { return _events; } }

        // Engine Services
        public ITestServices Services { get; }

        // Project Support
        public bool NUnitProjectSupport { get; }
        public bool VisualStudioSupport { get; }

        // Runtime Support
        private List<IRuntimeFramework> _runtimes;
        public IList<IRuntimeFramework> AvailableRuntimes
        {
            get
            {
                if (_runtimes == null)
                    _runtimes = GetAvailableRuntimes();

                return _runtimes;
            }
        }

        // Result Format Support
        private List<string> _resultFormats;
        public IEnumerable<string> ResultFormats
        {
            get
            {
                if (_resultFormats == null)
                {
                    _resultFormats = new List<string>();
                    foreach (string format in Services.ResultService.Formats)
                        _resultFormats.Add(format);
                }

                return _resultFormats;
            }
        }

        #region Properties

        public CommandLineOptions Options { get; private set; }

        public IDictionary<string, object> PackageSettings { get; } = new Dictionary<string, object>();

        public ITestRunner Runner { get; private set; }

        public bool IsPackageLoaded { get { return _package != null; } }

        public TestNode Tests { get; private set; }
        public bool HasTests { get { return Tests != null; } }

        public bool IsTestRunning
        {
            get { return Runner != null && Runner.IsTestRunning; }
        }

        public bool HasResults
        {
            get { return Results.Count > 0; }
        }

        private IList<string> _files;

        // The engine returns more values than we really want.
        // For example, we don't currently distinguish between
        // client and full profiles when executing tests. We
        // drop unwanted entries here. Even if some of these items
        // are removed in a later version of the engine, we may
        // have to retain this code to work with older engines.
        private List<IRuntimeFramework> GetAvailableRuntimes()
        {
            var runtimes = new List<IRuntimeFramework>();

            foreach (var runtime in GetService<IAvailableRuntimes>().AvailableRuntimes)
            {
                // Nothing below 2.0
                if (runtime.ClrVersion.Major < 2)
                    continue;

                // Remove erroneous entries for 4.5 Client profile
                if (IsErroneousNet45ClientProfile(runtime))
                    continue;

                // Skip duplicates
                var duplicate = false;
                foreach (var rt in runtimes)
                    if (rt.DisplayName == runtime.DisplayName)
                    {
                        duplicate = true;
                        break;
                    }

                if (!duplicate)
                    runtimes.Add(runtime);
            }

            runtimes.Sort((x, y) => x.DisplayName.CompareTo(y.DisplayName));

            // Now eliminate client entries where full entry follows
            for (int i = runtimes.Count - 2; i >= 0; i--)
            {
                var rt1 = runtimes[i];
                var rt2 = runtimes[i + 1];

                if (rt1.Id != rt2.Id)
                    continue;

                if (rt1.Profile == "Client" && rt2.Profile == "Full")
                    runtimes.RemoveAt(i);
            }

            return runtimes;
        }

        // Some early versions of the engine return .NET 4.5 Client profile, which doesn't really exist
        private static bool IsErroneousNet45ClientProfile(IRuntimeFramework runtime)
        {
            return runtime.FrameworkVersion.Major == 4 && runtime.FrameworkVersion.Minor > 0 && runtime.Profile == "Client";
        }

        #endregion


        #region Methods

        public void OnStartup()
        {
            if (Options.InputFiles.Count > 0)
            {
                LoadTests(Options.InputFiles);
            }
            else if (!Options.NoLoad && Services.RecentFiles.Entries.Count > 0)
            {
                var entry = Services.RecentFiles.Entries[0];
                if (!string.IsNullOrEmpty(entry) && System.IO.File.Exists(entry))
                    LoadTests(new[] { entry });
            }

            if (Options.RunAllTests && IsPackageLoaded)
                RunAllTests();
        }

        public void Dispose()
        {
            if (IsPackageLoaded)
                UnloadTests();

            if (Runner != null)
                Runner.Dispose();
        }

        public void NewProject()
        {
            NewProject("Dummy");
        }

        public const string PROJECT_SAVE_MESSAGE =
            "It's not yet decided if we will implement saving of projects. The alternative is to require use of the project editor, which already supports this.";

        public void NewProject(string filename)
        {
            throw new NotImplementedException(PROJECT_SAVE_MESSAGE);
        }

        public void SaveProject()
        {
            throw new NotImplementedException(PROJECT_SAVE_MESSAGE);
        }

        public void LoadTests(IList<string> files)
        {
            if (IsPackageLoaded)
                UnloadTests();

            _files = files;
            _events.FireTestsLoading(files);

            _package = MakeTestPackage(files);

            Runner = _testEngine.GetRunner(_package);

            Tests = new TestNode(Runner.Explore(TestFilter.Empty));

            Results.Clear();

            _events.FireTestLoaded(Tests);

            foreach (var subPackage in _package.SubPackages)
                Services.RecentFiles.SetMostRecent(subPackage.FullName);
        }

        public void UnloadTests()
        {
            Runner.Unload();
            Tests = null;
            _package = null;
            _files = null;
            Results.Clear();

            _events.FireTestUnloaded();
        }

        public void ReloadTests()
        {
            Runner.Unload();
            Results.Clear();
            Tests = null;

            _package = MakeTestPackage(_files);

            Runner = _testEngine.GetRunner(_package);

            Tests = new TestNode(Runner.Explore(TestFilter.Empty));

            Results.Clear();

            _events.FireTestReloaded(Tests);
        }

        #region Helper Methods

        // Public for testing only
        public TestPackage MakeTestPackage(IList<string> testFiles)
        {
            var package = new TestPackage(testFiles);

            // We use AddSetting rather than just setting the value because
            // it propagates the setting to all subprojects.

            if (Options.InternalTraceLevel != null)
                package.AddSetting(EnginePackageSettings.InternalTraceLevel, Options.InternalTraceLevel);

            // We use shadow copy so that the user may re-compile while the gui is running.
            package.AddSetting(EnginePackageSettings.ShadowCopyFiles, true);

            foreach (var entry in PackageSettings)
                package.AddSetting(entry.Key, entry.Value);

            return package;
        }

        #endregion

        public void RunAllTests()
        {
            RunTests(TestFilter.Empty);
        }

        public void RunTests(ITestItem testItem)
        {
            if (testItem != null)
                RunTests(testItem.GetTestFilter());
        }

        private void RunTests(TestFilter filter)
        {
            Runner.RunAsync(_events, filter);
        }

        public void CancelTestRun()
        {
            Runner.StopRun(false);
        }

        public ResultNode GetResultForTest(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                ResultNode result;
                if (Results.TryGetValue(id, out result))
                    return result;
            }

            return null;
        }

        public void NotifySelectedItemChanged(ITestItem testItem)
        {
            _events.FireSelectedItemChanged(testItem);
        }

        #endregion

        #endregion

        #region IServiceLocator Members

        public T GetService<T>() where T : class
        {
            return _testEngine.Services.GetService<T>();
        }

        public object GetService(Type serviceType)
        {
            return _testEngine.Services.GetService(serviceType);
        }

        #endregion
    }
}
