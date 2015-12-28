// ***********************************************************************
// Copyright (c) 2015 Charlie Poole
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
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace NUnit.Gui.Views.SettingsPages
{
    using Model.Settings;

    public partial class AssemblyIsolationSettingsPage : SettingsPage
    {
        public AssemblyIsolationSettingsPage(SettingsModel settings) : base("Engine.Assembly Isolation", settings)
        {
            InitializeComponent();
        }

        public override void LoadSettings()
        {
            switch (Settings.Engine.ProcessModel)
            {
                case "Separate":
                    separateProcessRadioButton.Checked = true;
                    sameProcessRadioButton.Checked = false;
                    multiProcessRadioButton.Checked = false;
                    break;
                case "Multiple":
                    multiProcessRadioButton.Checked = true;
                    sameProcessRadioButton.Checked = false;
                    separateProcessRadioButton.Checked = false;
                    break;
                default:
                    sameProcessRadioButton.Checked = true;
                    multiProcessRadioButton.Checked = false;
                    separateProcessRadioButton.Checked = false;
                    break;
            }

            bool multiDomain = Settings.Engine.DomainUsage == "Multiple";
            multiDomainRadioButton.Checked = multiDomain;
            singleDomainRadioButton.Checked = !multiDomain;
        }

        public override void ApplySettings()
        {
            if (multiProcessRadioButton.Checked)
                Settings.Engine.ProcessModel = "Multiple";
            else if (separateProcessRadioButton.Checked)
                Settings.Engine.ProcessModel = "Separate";
            else
                Settings.Engine.RemoveSetting("ProcessModel");

            if (multiDomainRadioButton.Checked)
                Settings.Engine.DomainUsage = "Multiple";
            else
                Settings.Engine.RemoveSetting("DomainUsage");
        }

        // TODO: Combine toggleProcessUsage and toggleMultiDomain
        private void toggleProcessUsage(object sender, EventArgs e)
        {
            bool enable = sameProcessRadioButton.Checked || separateProcessRadioButton.Checked;
            singleDomainRadioButton.Enabled = enable;
            multiDomainRadioButton.Enabled = enable;
        }

        private void toggleMultiDomain(object sender, System.EventArgs e)
        {
            bool multiDomain = multiDomainRadioButton.Checked = !multiDomainRadioButton.Checked;
            singleDomainRadioButton.Checked = !multiDomain;
        }

        public override bool HasChangesRequiringReload
        {
            get
            {
                return
                    Settings.Engine.ProcessModel != SelectedProcessModel ||
                    Settings.Engine.DomainUsage != SelectedDomainUsage;
            }
        }

        private string SelectedProcessModel
        {
            get
            {
                return separateProcessRadioButton.Checked
                    ? "Separate"
                    : multiProcessRadioButton.Checked
                        ? "Multiple"
                        : "Single";
            }
        }

        private string SelectedDomainUsage
        {
            get
            {
                return multiDomainRadioButton.Checked
                    ? "Multiple"
                    : "Single";
            }
        }
    }
}
