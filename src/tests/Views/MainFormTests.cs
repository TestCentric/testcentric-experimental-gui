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
using NUnit.Framework;
using NUnit.UiKit.Elements;

namespace NUnit.Gui.Views
{
    public class MainFormTests
    {
        private IMainView _view;

        [SetUp]
        public void CreateView()
        {
            _view = new MainForm();
        }

        [Test]
        public void SelectedRuntimeTest()
        {
            var selectedRuntime = _view.SelectedRuntime as CheckedMenuGroup;

            Assert.NotNull(selectedRuntime, "SelectedRuntime not set properly");
            Assert.NotNull(selectedRuntime.TopMenu, "TopMenu is not set");
            Assert.That(selectedRuntime.MenuItems.Select((p) => p.Text), Is.EqualTo(new string[] { "Default" }));
            Assert.That(selectedRuntime.MenuItems.Select((p) => p.Tag), Is.EqualTo(new string[] { "DEFAULT" }));
        }

        [Test]
        public void ProcessModelTest()
        {
            var processModel = _view.ProcessModel as CheckedMenuGroup;

            Assert.NotNull(processModel, "ProcessModel not set properly");
            Assert.That(processModel.MenuItems.Select((p) => p.Text),
                Is.EqualTo(new string[] { "Default", "InProcess", "Single", "Multiple" }));
            Assert.That(processModel.MenuItems.Select((p) => p.Tag),
                Is.EqualTo(new string[] { "DEFAULT", "InProcess", "Single", "Multiple" }));
        }

        [Test]
        public void DomainUsageTest()
        {
            var domainUsage = _view.DomainUsage as CheckedMenuGroup;

            Assert.NotNull(domainUsage, "DomainUsage not set properly");
            Assert.That(domainUsage.MenuItems.Select((p) => p.Text),
                Is.EqualTo(new string[] { "Default", "Single", "Multiple" }));
            Assert.That(domainUsage.MenuItems.Select((p) => p.Tag),
                Is.EqualTo(new string[] { "DEFAULT", "Single", "Multiple" }));
        }
    }
}
