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
using System.Windows.Forms;
using NUnit.Engine;

namespace TestCentric.Gui.Settings
{
    public class GuiSettings : SettingsGroup
    {
        public GuiSettings(ISettings settingsService) : base(settingsService, "Gui") { }

        public MainFormSettings MainForm
        {
            get { return new MainFormSettings(SettingsService); }
        }

        public RecentProjectSettings RecentProjects
        {
            get { return new RecentProjectSettings(SettingsService); }
        }

        public TestTreeSettings TestTree
        {
            get { return new TestTreeSettings(SettingsService); }
        }

        private const string initialPageKey = "InitialSettingsPage";
        public string InitialSettingsPage
        {
            get { return GetSetting(initialPageKey, ""); }
            set { SaveSetting(initialPageKey, value); }
        }
    }

    public class MainFormSettings : SettingsGroup
    {
        public MainFormSettings(ISettings settingsService) : base(settingsService, "Gui.MainForm") { }

        private const string locationKey = "Location";
        public Point Location
        {
            get { return GetSetting(locationKey, new Point(10, 10)); }
            set { SaveSetting(locationKey, value); }
        }

        private const string sizeKey = "Size";
        public Size Size
        {
            get { return GetSetting(sizeKey, Size.Empty); }
            set { SaveSetting(sizeKey, value); }
        }

        private const string maximizedKey = "Maximized";
        public bool Maximized
        {
            get { return GetSetting(maximizedKey, false); }
            set { SaveSetting(maximizedKey, value); }
        }

        private const string fontKey = "Font";
        public Font Font
        {
            get { return GetSetting(fontKey, Form.DefaultFont); }
            set { SaveSetting(fontKey, value); }
        }
    }

    public class RecentProjectSettings : SettingsGroup
    {
        public RecentProjectSettings(ISettings settingsService) : base(settingsService, "Gui.RecentProjects") { }

        private const string maxFilesKey = "MaxFiles";
        public int MaxFiles
        {
            get { return GetSetting(maxFilesKey, 5); }
            set { SaveSetting(maxFilesKey, value); }
        }
    }
}
