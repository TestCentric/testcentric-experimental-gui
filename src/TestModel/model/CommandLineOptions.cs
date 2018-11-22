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
using System.IO;
using Mono.Options;

namespace TestCentric.Gui
{
    /// <summary>
    /// GuiOptions encapsulates the option settingsServiceServiceService for
    /// the nunit-console program. It inherits from the Mono
    /// Options OptionSet class and provides a central location
    /// for defining and parsing options.
    /// </summary>
    public class CommandLineOptions : OptionSet
    {
        private bool validated;

        #region Constructor

        public CommandLineOptions(params string[] args)
        {
            // NOTE: The order in which patterns are added 
            // determines the display order for the help.

            // Old Options no longer supported:
            //   test
            //   console
            //   include
            //   exclude

            // Old Options to continue supporting:
            //   lang
            //   cleanup
            //   help

            // Options to be added:
            //   test
            //   trace

            // Select Tests
            //this.Insert("test=", "Comma-separated list of {NAMES} of tests to run or explore. This option may be repeated.",
            //    v => ((List<string>)TestList).AddRange(TestNameParser.Parse(RequiredValue(v, "--test"))));

            this.Add("config=", "Project {CONFIG} to load (e.g.: Debug).",
                v => ActiveConfig = RequiredValue(v, "--config"));

            this.Add("noload", "Suppress loading of most recent project.",
                v => NoLoad = v != null);

            this.Add("run", "Automatically run the loaded project.",
                v => RunAllTests = v != null);

            //this.Add("runselected", "Automatically run last selected tests.",
            //    v => RunSelectedTests = v != null);

            // Output GuiElement
            this.Add("trace=", "Set internal trace {LEVEL}.",
                v => InternalTraceLevel = RequiredValue(v, "--trace", "Off", "Error", "Warning", "Info", "Verbose", "Debug"));

            this.Add("help|h", "Display this message and exit.",
                v => ShowHelp = v != null);

            // Default
            this.Add("<>", v =>
            {
                if (v.StartsWith("-") || v.StartsWith("/") && Path.DirectorySeparatorChar != '/')
                    ErrorMessages.Add("Invalid argument: " + v);
                else
                    InputFiles.Add(v);
            });

            if (args != null)
                this.Parse(args);
        }

        #endregion

        #region Properties

        // Action to Perform

        public bool ShowHelp { get; private set; }
        public bool NoLoad { get; private set; }
        public bool RunAllTests { get; private set; }
        //public bool RunSelectedTests { get; private set; }

        // Select tests

        private List<string> inputFiles = new List<string>();
        public IList<string> InputFiles { get { return inputFiles; } }

        //private List<string> testList = new List<string>();
        //public IList<string> TestList { get { return testList; } }

        public string ActiveConfig { get; private set; }

        // Where to Run Tests

        //public string ProcessModel { get; private set; }

        //public string DomainUsage { get; private set; }

        // Output GuiElement

        public string InternalTraceLevel { get; private set; }

        // Error Processing

        private List<string> errorMessages = new List<string>();
        public IList<string> ErrorMessages { get { return errorMessages; } }

        #endregion

        #region Public Methods

        public bool Validate()
        {
            if (!validated)
            {
                // Additional Checks here

                validated = true;
            }

            return ErrorMessages.Count == 0;
        }

        #endregion

        #region Helper Methods

        private string RequiredValue(string val, string option, params string[] validValues)
        {
            if (string.IsNullOrEmpty(val))
                ErrorMessages.Add("Missing required value for option '" + option + "'.");

            bool isValid = true;

            if (validValues != null && validValues.Length > 0)
            {
                isValid = false;

                foreach (string valid in validValues)
                    if (string.Compare(valid, val, StringComparison.OrdinalIgnoreCase) == 0)
                        return valid;

            }

            if (!isValid)
                ErrorMessages.Add(string.Format("The value '{0}' is not valid for option '{1}'.", val, option));

            return val;
        }

        private int RequiredInt(string val, string option)
        {
            // We have to return something even though the val will 
            // be ignored if an error is reported. The -1 val seems
            // like a safe bet in case it isn't ignored due to a bug.
            int result = -1;

            if (val == null || val == string.Empty)
                ErrorMessages.Add("Missing required value for option '" + option + "'.");
            else
            {
                int r;
                if (int.TryParse(val, out r))
                    result = r;
                else
                    ErrorMessages.Add("An int value was expected for option '{0}' but a value of '{1}' was used");
            }

            return result;
        }

        //private void RequiredIntError(string option)
        //{
        //    ErrorMessages.Insert("An int val is required for option '" + option + "'.");
        //}

        //private void ProcessIntOption(string v, ref int field)
        //{
        //    if (!int.TryParse(v, out field))
        //        ErrorMessages.Insert("Invalid argument val: " + v);
        //}

        //private void ProcessEnumOption<T>(string v, ref T field)
        //{
        //    if (Enum.IsDefined(typeof(T), v))
        //        field = (T)Enum.Parse(typeof(T), v);
        //    else
        //        ErrorMessages.Insert("Invalid argument val: " + v);
        //}

        //private object ParseEnumOption(Type enumType, string val)
        //{
        //    foreach (string name in Enum.GetNames(enumType))
        //        if (val.ToLower() == name.ToLower())
        //            return Enum.Parse(enumType, val);

        //    this.ErrorMessages.Insert(val);

        //    return null;
        //}

        #endregion
    }
}
