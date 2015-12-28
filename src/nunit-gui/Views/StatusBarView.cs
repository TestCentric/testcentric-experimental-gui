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

using System.Windows.Forms;

namespace NUnit.Gui.Views
{
    // Interface is used by presenter and tests
    public interface IStatusBarView : IView
    {
        void SetStatus(string text);
        void Initialize(string text);
        void Initialize(string text, int testCount);
        void RunStarting(int testCount);
        void RunFinished(double elapsedTime);
        void RecordSuccess();
        void RecordFailure();
        void RecordError();
    }

    public partial class StatusBarView : UserControl, IStatusBarView
    {
        private int _testsRun;
        private int _errors;
        private int _failures;

        public StatusBarView()
        {
            InitializeComponent();
            Initialize("Ready");
        }

        private int _testCount;

        private double _elapsedTime;
        public double ElapsedTime
        {
            get { return _elapsedTime; }
            set 
            { 
                _elapsedTime = value; 
                DisplayTime(); 
            }
        }

        public void SetStatus(string text)
        {
            InvokeIfRequired(() =>
            {
                StatusLabel.Text = text;
            });
        }

        public void Initialize(string text)
        {
            InvokeIfRequired(() =>
            {
                doInitialize(text);
            });
        }

        public void Initialize(string text, int testCount)
        {
            InvokeIfRequired(() =>
            {
                doInitialize(text, testCount);
            });
        }

        public void RunStarting(int testCount)
        {
            InvokeIfRequired(() =>
            {
                doInitialize("Running...", testCount);

                DisplayTestsRun();
                DisplayErrors();
                DisplayFailures();
                DisplayTime();
            });
        }

        public void RunFinished(double elapsedTime)
        {
            InvokeIfRequired(() =>
            {
                StatusLabel.Text = "Completed";
                ElapsedTime = elapsedTime;
            });
        }

        public void RecordSuccess()
        {
            InvokeIfRequired(() =>
            {
                IncrementTestsRun();
            });
        }

        public void RecordError()
        {
            InvokeIfRequired(() =>
            {
                IncrementTestsRun();
                IncrementErrors();
            });
        }

        public void RecordFailure()
        {
            InvokeIfRequired(() =>
            {
                IncrementTestsRun();
                IncrementFailures();
            });
        }

        #region Helper Methods

        private void doInitialize(string text)
        {
            _testCount = 0;
            _testsRun = 0;
            _errors = 0;
            _failures = 0;
            _elapsedTime = 0.0;

            StatusLabel.Text = text;
            testCountPanel.Text = "";
            testsRunPanel.Text = "";
            errorsPanel.Text = "";
            failuresPanel.Text = "";
            timePanel.Text = "";

            testCountPanel.Visible = false;
            testsRunPanel.Visible = false;
            errorsPanel.Visible = false;
            failuresPanel.Visible = false;
            timePanel.Visible = false;
        }

        private void doInitialize(string text, int testCount)
        {
            doInitialize(text);

            _testCount = testCount;
            DisplayTestCount();
        }

        private void DisplayTestCount()
        {
            DisplayPanel(testCountPanel, "Test Cases : " + _testCount.ToString());
        }

        private void DisplayTestsRun()
        {
            DisplayPanel(testsRunPanel, "Tests Run : " + _testsRun.ToString());
        }

        private void DisplayErrors()
        {
            DisplayPanel(errorsPanel, "Errors : " + _errors.ToString());
        }

        private void DisplayFailures()
        {
            DisplayPanel(failuresPanel, "Failures : " + _failures.ToString());
        }

        private void DisplayTime()
        {
            DisplayPanel(timePanel, "Time : " + _elapsedTime.ToString("F3"));
        }

        private void DisplayPanel(ToolStripStatusLabel panel, string text)
        {
            panel.Visible = true;
            panel.Text = text;
        }

        private void IncrementTestsRun()
        {
            _testsRun++;
            DisplayTestsRun();
        }

        private void IncrementErrors()
        {
            _errors++;
            DisplayErrors();
        }

        private void IncrementFailures()
        {
            _failures++;
            DisplayFailures();
        }

        private void InvokeIfRequired(MethodInvoker _delegate)
        {
            if (statusStrip1.InvokeRequired)
                statusStrip1.BeginInvoke(_delegate);
            else
                _delegate();
        }

        #endregion
    }
}
