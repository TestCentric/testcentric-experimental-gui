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

using System.Drawing;

namespace TestCentric.Gui.Views
{
    using Elements;

    /// <summary>
    /// Handler when the main view has files dragged and dropped
    /// </summary>
    /// <param name="filesNames">list of file names of files dropped. Never null, potentially empty</param>
    public delegate void DragEventHandler(string[] filesNames);

    /// <summary>
    /// Event when a close event is called by main view
    /// </summary>
    public interface IMainView : IView
    {
        // General Window info
        Point Location { get; set; }
        Size Size { get; set; }
        bool IsMaximized { get; set; }
        Font Font { get; set; }

        event CommandHandler MainViewClosing;
        event DragEventHandler DragDropFiles;

        // File Menu
        IToolStripMenu FileMenu { get; }
        ICommand NewProjectCommand { get; }
        ICommand OpenProjectCommand { get; }
        ICommand CloseCommand { get; }
        ICommand SaveCommand { get; }
        ICommand SaveAsCommand { get; }
        ICommand SaveResultsCommand { get; }
        ICommand ReloadTestsCommand { get; }
        IToolStripMenu SelectRuntimeMenu { get; }
        ISelection SelectedRuntime { get; }
        ISelection ProcessModel { get; }
        IChecked RunAsX86 { get; }
        ISelection DomainUsage { get; }
        IToolStripMenu RecentProjectsMenu { get; }
        ICommand ExitCommand { get; }

        // View Menu
        // Full Gui
        // Mini Gui
        // Gui Font
        // Fixed Font
        // Status Bar

        // Project Menu
        IToolStripMenu ProjectMenu { get; }
        // Configurations
        // Insert Assembly
        // Insert Project
        // Edit

        // Tools Menu
        // Test Assemblies
        // Exception Details
        // Open Log Directory
        ICommand SettingsCommand { get; }
        ICommand AddinsCommand { get; }

        // Help Menu
        ICommand NUnitHelpCommand { get; }
        ICommand AboutNUnitCommand { get; }

        // Tabs
        // Test
        IViewElement TestResult { get; }
        IViewElement TestName { get; }

        // Result

        // Output

        // Dialogs
        IDialogManager DialogManager { get; }

        // Messages
        IMessageDisplay MessageDisplay { get; }

        void OnTestAssembliesLoading(string message);
        void OnTestAssembliesLoaded();
    }
}
