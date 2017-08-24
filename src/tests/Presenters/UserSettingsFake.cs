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
using System.ComponentModel;
using NUnit.Engine;

namespace NUnit.Gui.Presenters
{
    // TODO: With latest changes, this is no longer used.
    // Keeping it for a while, as we may end up wanting
    // to write more tests that use it.
    class UserSettingsFake : ISettings
    {
        private Dictionary<string, object> _settings = new Dictionary<string, object>();

        public event SettingsEventHandler Changed;

        public object GetSetting(string settingName)
        {
            if (_settings.ContainsKey(settingName))
                return _settings[settingName];

            return null;
        }

        public T GetSetting<T>(string settingName, T defaultValue)
        {
            object result = GetSetting(settingName);

            if (result == null)
                return defaultValue;

            if (result is T)
                return (T)result;

            try
            {
                TypeConverter converter = TypeDescriptor.GetConverter(typeof(T));
                if (converter == null)
                    return defaultValue;

                return (T)converter.ConvertFrom(result);
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }

        public void RemoveSetting(string settingName)
        {
            _settings.Remove(settingName);

            if (Changed != null)
                Changed(this, new SettingsEventArgs(settingName));
        }

        public void RemoveGroup(string groupName)
        {
            throw new NotImplementedException();
        }

        public void SaveSetting(string settingName, object settingValue)
        {
            _settings[settingName] = settingValue;

            Changed?.Invoke(this, new SettingsEventArgs(settingName));
        }
    }
}
