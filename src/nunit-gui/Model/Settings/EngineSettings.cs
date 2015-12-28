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

using System.Drawing;
using System.Security.Principal;
using System.Windows.Forms;

namespace NUnit.Gui.Model.Settings
{
    using Engine;

    /// <summary>
    /// SettingsModel is the top level of a set of wrapper
    /// classes that provide type-safe access to settingsService.
    /// </summary>
    public class EngineSettings : SettingsWrapper
    {
        public EngineSettings(ISettings settingsService) : base(settingsService, "Engine.Options") { }

        private const string processModelKey = "ProcessModel";
        public string ProcessModel
        {
            get { return GetSetting(processModelKey, "Default"); }
            set { SaveSetting(processModelKey, value); }
        }

        private const string domainUsageKey = "DomainUsage";
        public string DomainUsage
        {
            get { return GetSetting(domainUsageKey, "Default"); }
            set { SaveSetting(domainUsageKey, value); }
        }

        private const string reloadOnChangeKey = "ReloadOnChange";
        public bool ReloadOnChange
        {
            get { return GetSetting(reloadOnChangeKey, true); }
            set { SaveSetting(reloadOnChangeKey, value); }
        }

        private const string rerunOnChangeKey = "RerunOnChange";
        public bool RerunOnChange
        {
            get { return GetSetting(rerunOnChangeKey, false); }
            set { SaveSetting(rerunOnChangeKey, value); }
        }

        private const string reloadOnRunKey = "ReloadOnRun";
        public bool ReloadOnRun
        {
            get { return GetSetting(reloadOnRunKey, false); }
            set { SaveSetting(reloadOnRunKey, value); }
        }

        private const string runtimeSelectionEnabledKey = "RuntimeSelectionEnabled";
        public bool RuntimeSelectionEnabled
        {
            get { return GetSetting(runtimeSelectionEnabledKey, true); }
            set { SaveSetting(runtimeSelectionEnabledKey, value); }
        }

        private const string shadowCopyFilesKey = "ShadowCopyFiles";
        public bool ShadowCopyFiles
        {
            get { return GetSetting(shadowCopyFilesKey, true); }
            set { SaveSetting(shadowCopyFilesKey, value); }
        }

        private const string shadowCopyPathKey = "ShadowCopyPath";
        public string ShadowCopyPath
        {
            get { return GetSetting(shadowCopyPathKey, string.Empty); }
            set { SaveSetting(shadowCopyPathKey, value); }
        }

        private const string setPrincipalPolicyKey = "SetPrincipalPolicy";
        public bool SetPrincipalPolicy
        {
            get { return GetSetting(setPrincipalPolicyKey, false); }
            set { SaveSetting(setPrincipalPolicyKey, value); }
        }

        private const string principalPolicyKey = "PrincipalPolicy";
        public PrincipalPolicy PrincipalPolicy
        {
            get { return GetSetting(principalPolicyKey, PrincipalPolicy.UnauthenticatedPrincipal); }
            set { SaveSetting(principalPolicyKey, value); }
        }
    }
}
