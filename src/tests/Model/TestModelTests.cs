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

using NUnit.Engine;
using NUnit.Framework;
using NUnit.Gui.Tests.Assemblies;

namespace NUnit.Gui.Model
{
    [Explicit("Second TestEngine conflicts with main engine")]
    public class TestModelTests
    {
        private const string MOCK_ASSEMBLY = "mock-assembly.dll";

        private ITestModel _model;

        [OneTimeSetUp]
        public void CreateTestModel()
        {
            var engine = TestEngineActivator.CreateInstance();
            Assert.NotNull(engine, "Unable to create engine instance for testing");

            _model = new TestModel(engine, new CommandLineOptions());

            _model.LoadTests(new []{MOCK_ASSEMBLY});
        }

        [Test]
        public void CheckStateAfterLoading()
        {
            Assert.That(_model.HasTests, "HasTests");
            Assert.NotNull(_model.Tests, "Tests");
            Assert.False(_model.HasResults, "HasResults");

            var testRun = _model.Tests;
            // A quirk of the engine: test-run does not have a runstate attribute
            Assert.That(testRun.RunState, Is.EqualTo(RunState.Unknown), "RunState of test-run");
            Assert.That(testRun.TestCount, Is.EqualTo(MockAssembly.Tests), "TestCount of test-run");
            Assert.That(testRun.Children.Count, Is.EqualTo(1), "Child count of test-run");

            var testAssembly = testRun.Children[0];
            Assert.That(testAssembly.RunState, Is.EqualTo(RunState.Runnable), "RunState of assembly");
            Assert.That(testAssembly.TestCount, Is.EqualTo(MockAssembly.Tests), "TestCount of assembly");
        }

        [Test]
        public void CheckStateAfterRunningTests()
        {
            _model.RunAllTests();

            Assert.That(_model.HasTests, "HasTests");
            Assert.NotNull(_model.Tests, "Tests");
            Assert.False(_model.HasResults, "HasResults");
        }

        [Test]
        public void CheckStateAfterUnloading()
        {
            _model.RunAllTests();

            //Assert.False(_model.HasTests, "HasTests");
            //Assert.Null(_model.Tests, "Tests");
            Assert.False(_model.HasResults, "HasResults");
        }
    }
}
