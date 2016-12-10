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
using System.Security.Principal;
using System.Text;
using System.Windows.Forms;

namespace NUnit.Gui.Views.SettingsPages
{
    using Model.Settings;

    public partial class AdvancedEngineSettingsPage : SettingsPage
    {
        public AdvancedEngineSettingsPage(SettingsModel settings)
            : base("Engine.Advanced", settings)
        {
            InitializeComponent();
        }

        public override void LoadSettings()
        {
            enableShadowCopyCheckBox.Checked = Settings.Engine.ShadowCopyFiles;
            shadowCopyPathTextBox.Text = Settings.Engine.ShadowCopyPath;

            principalPolicyCheckBox.Checked = principalPolicyListBox.Enabled =
                Settings.Engine.SetPrincipalPolicy;
            principalPolicyListBox.SelectedIndex = (int)Settings.Engine.PrincipalPolicy;
        }

        public override void ApplySettings()
        {
            Settings.Engine.ShadowCopyFiles = enableShadowCopyCheckBox.Checked;

            if (shadowCopyPathTextBox.Text != "")
                Settings.Engine.ShadowCopyPath = shadowCopyPathTextBox.Text;
            else
                Settings.Engine.RemoveSetting("ShadowCopyPath");

            Settings.Engine.SetPrincipalPolicy = principalPolicyCheckBox.Checked;

            Settings.Engine.PrincipalPolicy = principalPolicyCheckBox.Checked
                ? (PrincipalPolicy)principalPolicyListBox.SelectedIndex
                : PrincipalPolicy.UnauthenticatedPrincipal; // TODO: Should really remove setting
        }

        public override bool HasChangesRequiringReload
        {
            get
            {
                return enableShadowCopyCheckBox.Checked != Settings.Engine.ShadowCopyFiles
                    || shadowCopyPathTextBox.Text != Settings.Engine.ShadowCopyPath
                    || principalPolicyCheckBox.Checked != Settings.Engine.SetPrincipalPolicy
                    || principalPolicyListBox.SelectedIndex != (int)Settings.Engine.PrincipalPolicy;

            }
        }

        private void enableShadowCopyCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            //shadowCopyPathTextBox.Enabled = enableShadowCopyCheckBox.Checked;
        }

        private void principalPolicyCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            principalPolicyListBox.Enabled = principalPolicyCheckBox.Checked;
        }
    }
}
