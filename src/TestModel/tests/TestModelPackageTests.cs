// ***********************************************************************
// Copyright (c) 2017 Charlie Poole
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

using System.Linq;
using NSubstitute;
using NUnit.Engine;
using NUnit.Framework;

namespace TestCentric.Gui.Model
{
    public class TestModelPackageTests
    {
        private TestModel _model;

        [SetUp]
        public void CreateModel()
        {
            var engine = Substitute.For<ITestEngine>();
            _model = new TestModel(engine);
        }

        [TestCase("my.test.assembly.dll")]
        [TestCase("one.dll", "two.dll", "three.dll")]
        public void PackageContainsOneSubPackagePerAssembly(params string[] assemblies)
        {
            TestPackage package = _model.MakeTestPackage(assemblies);

            Assert.That(package.SubPackages.Select(p => p.Name), Is.EqualTo(assemblies));
        }

        [TestCase("ProjectModel", "Single")]
        [TestCase("DomainUsage", "Multiple")]
        [TestCase("RuntimeFramework", "net-2.0")]
        public void PackageReflectsPackageSettings(string key, object value)
        {
            _model.PackageSettings[key] = value;
            TestPackage package = _model.MakeTestPackage(new[] { "my.dll" });

            Assert.That(package.Settings.ContainsKey(key));
            Assert.That(package.Settings[key], Is.EqualTo(value));
        }
    }
}
