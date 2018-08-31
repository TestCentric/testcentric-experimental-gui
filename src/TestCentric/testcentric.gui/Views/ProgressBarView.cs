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

using System.Diagnostics;
using System.Windows.Forms;

namespace TestCentric.Gui.Views
{
    using Controls;

    public partial class ProgressBarView : UserControl, IProgressBarView
    {
        private int _maximum;

        public ProgressBarView()
        {
            InitializeComponent();
        }

        public void Initialize(int max)
        {
            Debug.Assert(max > 0, "Maximum value must be > 0");

            _maximum = max;
            _progress = 0;
            _status = ProgressBarStatus.Success;

            InvokeIfRequired(() =>
            {
                testProgressBar.Maximum = _maximum;
                testProgressBar.Value = _progress;
                testProgressBar.Status = _status;
            });
        }

        private int _progress;
        public int Progress
        {
            get { return _progress; }
            set
            {
                Debug.Assert(value <= _maximum, "Value must be <= maximum");

                _progress = value;
                InvokeIfRequired(() => { testProgressBar.Value = _progress; });
            }
        }

        private ProgressBarStatus _status;
        public ProgressBarStatus Status
        {
            get { return _status; }
            set
            {
                _status = value;
                InvokeIfRequired(() => { testProgressBar.Status = _status; });
            }
        }

        private void InvokeIfRequired(MethodInvoker _delegate)
        {
            if (testProgressBar.InvokeRequired)
                testProgressBar.BeginInvoke(_delegate);
            else
                _delegate();
        }
    }
}
