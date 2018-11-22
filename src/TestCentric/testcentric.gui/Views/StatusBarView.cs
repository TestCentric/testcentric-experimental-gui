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

using System.Windows.Forms;

namespace TestCentric.Gui.Views
{
    public partial class StatusBarView : UserControl, IStatusBarView
    {
        // Counters are maintained in the view even though
        // they more properly belong in the presenter. This
        // allows everything to be done in one invocation.
        private int _testsRun;
        private int _passedCount;
        private int _failedCount;
        private int _warningCount;
        private int _inconclusiveCount;
        private ToolTip _resultSummaryToolTip;

        public StatusBarView()
        {
            InitializeComponent();
            _resultSummaryToolTip = new ToolTip()
            {
                IsBalloon = true,
                UseAnimation = true,
                UseFading = true,
            };
        }

        public void OnTestLoaded(int testCount)
        {
            ClearCounters();

            InvokeIfRequired(() =>
            {
                HideAllButStatusPanel();
                DisplayTestCount(testCount);
            });
        }

        public void OnTestUnloaded()
        {
            ClearCounters();

            InvokeIfRequired(() =>
            {
                HideAllButStatusPanel();
            });
        }

        public void OnRunStarting(int testCount)
        {
            ClearCounters();

            InvokeIfRequired(() =>
            {
                HideAllButStatusPanel();

                DisplayTestCount(testCount);
                DisplayTestsRun();
                DisplayPassed();
                DisplayFailed();
                DisplayWarnings();
                DisplayInconclusive();
                DisplayTime(0.0);
            });
        }

        public void OnRunFinished(double elapsedTime)
        {
            InvokeIfRequired(() =>
            {
                StatusLabel.Text = "Completed";
                DisplayTime(elapsedTime);
            });
        }

        public void OnTestRunSummaryCompiled(string testRunSummary)
        {
            InvokeIfRequired(() =>
            {
                _resultSummaryToolTip.ToolTipTitle = "Tests Run Summary";
                _resultSummaryToolTip.Show(testRunSummary, this, 10000);
            });
        }

        public void OnTestStarting(string name)
        {
            InvokeIfRequired(() =>
            {
                StatusLabel.Text = name;
            });
        }

        public void OnTestPassed()
        {
            _testsRun++;
            _passedCount++;

            InvokeIfRequired(() =>
            {
                DisplayTestsRun();
                DisplayPassed();
            });
        }

        public void OnTestFailed()
        {
            _testsRun++;
            _failedCount++;

            InvokeIfRequired(() =>
            {
                DisplayTestsRun();
                DisplayFailed();
            });
        }

        public void OnTestWarning()
        {
            _testsRun++;
            _warningCount++;

            InvokeIfRequired(() =>
            {
                DisplayTestsRun();
                DisplayWarnings();
            });
        }

        public void OnTestInconclusive()
        {
            _testsRun++;
            _inconclusiveCount++;

            InvokeIfRequired(() =>
            {
                DisplayTestsRun();
                DisplayInconclusive();
            });
        }

        #region Helper Methods

        private void ClearCounters()
        {
            InvokeIfRequired(() => _resultSummaryToolTip.Hide(this));
            _testsRun = 0;
            _passedCount = 0;
            _failedCount = 0;
            _warningCount = 0;
            _inconclusiveCount = 0;
        }

        private void HideAllButStatusPanel()
        {
            testCountPanel.Visible = false;
            testsRunPanel.Visible = false;
            passedPanel.Visible = false;
            failedPanel.Visible = false;
            warningsPanel.Visible = false;
            inconclusivePanel.Visible = false;
            timePanel.Visible = false;
        }

        private void DisplayTestCount(int count)
        {
            testCountPanel.Text = "Tests : " + count.ToString();
            testCountPanel.Visible = true;
        }

        private void DisplayTestsRun()
        {
            testsRunPanel.Text = "Run : " + _testsRun.ToString();
            testsRunPanel.Visible = true;
        }

        private void DisplayPassed()
        {
            passedPanel.Text = "Passed : " + _passedCount.ToString();
            passedPanel.Visible = true;
        }

        private void DisplayFailed()
        {
            failedPanel.Text = "Failed : " + _failedCount.ToString();
            failedPanel.Visible = true;
        }

        private void DisplayWarnings()
        {
            warningsPanel.Text = "Warnings : " + _warningCount.ToString();
            warningsPanel.Visible = true;
        }

        private void DisplayInconclusive()
        {
            inconclusivePanel.Text = "Inconclusive : " + _inconclusiveCount.ToString();
            inconclusivePanel.Visible = true;
        }

        private void DisplayTime(double time)
        {
            timePanel.Text = "Time : " + time.ToString("F3");
            timePanel.Visible = true;
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
