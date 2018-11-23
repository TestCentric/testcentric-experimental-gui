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
using System.Windows.Forms;
using TestCentric.Gui;

namespace TestCentric.Gui.Views
{
    using Settings;
    using Elements;

    public partial class SettingsPage : UserControl
    {
        // Constructor used by the Windows.Forms Designer.
        // For some reason, this must be present for any
        // UserControl used as a base for another user control.
        public SettingsPage()
        {
            InitializeComponent();
        }

        // Constructor we use in creating page for SettingsDialog
        public SettingsPage(string key, SettingsModel settings)
        {
            InitializeComponent();

            this.Key = key;
            this.Settings = settings;
            this.MessageDisplay = new MessageDisplay("NUnit Settings");
        }

        #region Properties

        protected SettingsModel Settings { get; private set; }

        protected IMessageDisplay MessageDisplay { get; private set; }

        public string Key { get; private set; }

        public bool SettingsLoaded { get; private set; }

        public virtual bool HasChangesRequiringReload
        {
            get { return false; }
        }

        #endregion

        #region Public Methods

        public virtual void LoadSettings()
        {
        }

        public virtual void ApplySettings()
        {
        }

        #endregion

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!DesignMode)
            {
                this.LoadSettings();
                SettingsLoaded = true;
            }
        }
    }
}
