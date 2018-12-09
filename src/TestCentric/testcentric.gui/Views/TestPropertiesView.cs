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
    using Elements;

    public partial class TestPropertiesView : UserControl, ITestPropertiesView
    {
        public event CommandHandler DisplayHiddenPropertiesChanged;

        public TestPropertiesView()
        {
            InitializeComponent();

            this.TestPanel = new ControlElement(testPanel);
            this.ResultPanel = new ControlElement(resultPanel);

            displayHiddenProperties.CheckedChanged += (s, e) =>
            {
                if (DisplayHiddenPropertiesChanged != null)
                    DisplayHiddenPropertiesChanged();
            };
        }

        public string Header
        {
            get { return header.Text; }
            set { InvokeIfRequired(() => { header.Text = value; }); }
        }

        public IViewElement TestPanel { get; private set; }
        public IViewElement ResultPanel { get; private set; }

        public string TestType
        {
            get { return testType.Text; }
            set { InvokeIfRequired(() => { testType.Text = value; }); }
        }

        public string FullName
        {
            get { return fullName.Text; }
            set { InvokeIfRequired(() => { fullName.Text = value; }); }
        }

        public string Description
        {
            get { return description.Text; }
            set { InvokeIfRequired(() => { description.Text = value; }); }
        }

        public string Categories
        {
            get { return categories.Text; }
            set { InvokeIfRequired(() => { categories.Text = value; }); }
        }

        public string TestCount
        {
            get { return testCount.Text; }
            set { InvokeIfRequired(() => { testCount.Text = value; }); }
        }

        public string RunState
        {
            get { return runState.Text; }
            set { InvokeIfRequired(() => { runState.Text = value; }); }
        }

        public string SkipReason
        {
            get { return skipReason.Text; }
            set { InvokeIfRequired(() => { skipReason.Text = value; }); }
        }

        public bool DisplayHiddenProperties
        {
            get { return displayHiddenProperties.Checked; }
        }

        public string Properties
        {
            get { return properties.Text; }
            set { properties.Text = value; }
        }

        public string Outcome
        {
            get { return outcome.Text; }
            set { InvokeIfRequired(() => { outcome.Text = value; }); }
        }

        public string ElapsedTime
        {
            get { return elapsedTime.Text; }
            set { InvokeIfRequired(() => { elapsedTime.Text = value; }); }
        }

        public string AssertCount
        {
            get { return assertCount.Text; }
            set { InvokeIfRequired(() => { assertCount.Text = value; }); }
        }

        public string Assertions
        {
            get { return assertions.Text; }
            set { InvokeIfRequired(() => { assertions.Text = value; }); }
        }

        public string Output
        {
            get { return output.Text; }
            set { InvokeIfRequired(() => { output.Text = value; }); }
        }

        #region Helper Methods

        private void InvokeIfRequired(MethodInvoker _delegate)
        {
            if (this.InvokeRequired)
                this.BeginInvoke(_delegate);
            else
                _delegate();
        }

        #endregion
    }
}
