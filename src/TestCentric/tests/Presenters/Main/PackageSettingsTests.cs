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

using NSubstitute;
using NUnit.Framework;

namespace TestCentric.Gui.Presenters.Main
{
    using Elements;

    public class PackageSettingsTests : MainPresenterTestBase
    {
        [TestCase("Single")]
        [TestCase("Multiple")]
        [TestCase("InProcess")]
        [TestCase("INVALID", Description = "Invalid Setting is passed on to the model")]
        public void ProcessModel_SettingChanged(string value)
        {
            View.ProcessModel.SelectedItem.Returns(value);

            View.ProcessModel.SelectionChanged += Raise.Event<CommandHandler>();

            Model.PackageSettings.Received(1)["ProcessModel"] = value;
        }

        [Test]
        public void ProcessModel_SetToDefault()
        {
            View.ProcessModel.SelectedItem.Returns("DEFAULT");

            View.ProcessModel.SelectionChanged += Raise.Event<CommandHandler>();

            Model.PackageSettings.Received(1).Remove("ProcessModel");
        }

        [TestCase("Single")]
        [TestCase("Multiple")]
        [TestCase("INVALID", Description = "Invalid Setting is passed on to the model")]
        public void DomainUsage_SettingChanged(string value)
        {
            View.DomainUsage.SelectedItem.Returns(value);

            View.DomainUsage.SelectionChanged += Raise.Event<CommandHandler>();

            Model.PackageSettings.Received(1)["DomainUsage"] = value;
        }

        [Test]
        public void DomainUsage_SetToDefault()
        {
            View.DomainUsage.SelectedItem.Returns("DEFAULT");

            View.DomainUsage.SelectionChanged += Raise.Event<CommandHandler>();

            Model.PackageSettings.Received(1).Remove("DomainUsage");
        }

        [TestCase("net-2.0")]
        [TestCase("net-4.5")]
        [TestCase("INVALID", Description = "Invalid Setting is passed on to the model")]
        [TestCase("net-2.0", "net-4.5")]
        public void SelectedRuntime_SettingChanged(params string[] settings)
        {
            foreach (var setting in settings)
            {
                View.SelectedRuntime.SelectedItem.Returns(setting);

                View.SelectedRuntime.SelectionChanged += Raise.Event<CommandHandler>();

                Model.PackageSettings.Received(1)["RuntimeFramework"] = setting;
            }
        }

        public void SelectedRuntime_MultipleChanges()
        {

        }

        [Test]
        public void SelectedRuntime_SetToDefault()
        {
            View.SelectedRuntime.SelectedItem.Returns("DEFAULT");

            View.SelectedRuntime.SelectionChanged += Raise.Event<CommandHandler>();

            Model.PackageSettings.Received(1).Remove("RuntimeFramework");
        }
    }
}
