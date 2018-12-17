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
using System.IO;
using System.Windows.Forms;
using Mono.Options;
using NUnit.Engine;

namespace TestCentric.Gui
{
    using Model;
    using Views;
    using Presenters;

    public static class AppEntry
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        public static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Run(args);
        }

        /// <summary>
        /// The main entry point for the application, minus GUI initialization
        /// logic. This method is intended for wrapper applications that
        /// already do their own GUI initializations that would conflict with
        /// those done in <see cref="Main"/>.
        /// </summary>
        public static void Run(string[] args)
        {
            CommandLineOptions options = new CommandLineOptions();
            try
            {
                options.Parse(args);
            }
            catch (OptionException ex)
            {
                string msg = string.Format("{0} {1}", ex.Message, ex.OptionName);
                MessageBox.Show(msg, "NUnit - OptionException", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!options.Validate())
            {
                var errMessage = new string[options.ErrorMessages.Count];
                options.ErrorMessages.CopyTo(errMessage, 0);
                var msg =
                    "There were the following errors parsing the options:" + Environment.NewLine +
                    string.Join(Environment.NewLine, errMessage) + Environment.NewLine +
                    Environment.NewLine +
                    "Use the option '--help' to show the possible options and their values.";
                MessageBox.Show(msg, "NUnit - Problem parsing options", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (options.ShowHelp)
            {
                ShowHelpText(options);
                return;
            }

            var testEngine = TestEngineActivator.CreateInstance(true);
            if (options.InternalTraceLevel != null)
                testEngine.InternalTraceLevel = (InternalTraceLevel)Enum.Parse(typeof(InternalTraceLevel), options.InternalTraceLevel);

            var model = new TestModel(testEngine);

            var form = new MainForm();
            new MainPresenter(form, model, options);
            new ProgressBarPresenter(form.ProgressBarView, model);
            new TreeViewPresenter(form.TestTreeView, model);
            new StatusBarPresenter(form.StatusBarView, model);
            new TestPropertiesPresenter(form.PropertiesView, model);
            new XmlPresenter(form.XmlView, model);

            //new RecentFiles(settingsServiceServiceService._settings);
            //new RecentFilesPresenter(form, settingsServiceServiceService);

            try
            {
                Application.Run(form);
            }
            finally
            {
                testEngine.Dispose();
            }
        }

        private static readonly string NL = Environment.NewLine;

        private static void ShowHelpText(CommandLineOptions options)
        {
            StringWriter writer = new StringWriter();

            writer.WriteLine("NUNIT [inputfiles] [options]");
            writer.WriteLine();
            writer.WriteLine("Loads and optionally runs a set of NUnit tests using the GUI runner.");
            writer.WriteLine();
            writer.WriteLine("InputFiles:");
            writer.WriteLine("   One or more assemblies or test projects of a recognized type.");
            writer.WriteLine();
            writer.WriteLine("Options:");
            options.WriteOptionDescriptions(writer);
            writer.WriteLine();
            writer.WriteLine("Description:");
            writer.WriteLine("   By default, this command loads the tests contained in the input");
            writer.WriteLine("   specified, displaying them in the GUI runner, where the user may");
            writer.WriteLine("   run some or all of them as desired. If no input files are given");
            writer.WriteLine("   the tests contained in the most recently used project or assembly");
            writer.WriteLine("   are loaded, unless the --noload option is specified.");
            writer.WriteLine();
            writer.WriteLine("   CATEGORIES may be specified singly, as a comma-separated list or");
            writer.WriteLine("   as a category expression.");
            writer.WriteLine();
            writer.WriteLine("   Trace LEVEL may be Off, Error, Warning, Info, Verbose or Debug.");

            MessageBox.Show(writer.GetStringBuilder().ToString(), "NUnit - Help");
        }
    }
}
