// ***********************************************************************
// Copyright (c) 2017 Charlie Poole
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

using System.Globalization;
using System.Text;

namespace NUnit.Gui.Model
{
    public static class ResultSummaryReporter
    {
        public static string WriteSummaryReport(ResultSummary summary)
        {
            var writer = new StringBuilder();
            writer.AppendLine($"Overall result: {summary.OverallResult}");

            writer.AppendUICultureFormattedNumber("  Test Count: ", summary.TestCount);
            writer.AppendUICultureFormattedNumber(", Passed: ", summary.PassCount);
            writer.AppendUICultureFormattedNumber(", Failed: ", summary.FailedCount);
            writer.AppendUICultureFormattedNumber(", Warnings: ", summary.WarningCount);
            writer.AppendUICultureFormattedNumber(", Inconclusive: ", summary.InconclusiveCount);
            writer.AppendUICultureFormattedNumber(", Skipped: ", summary.TotalSkipCount);
            writer.AppendLine();

            if (summary.FailedCount > 0)
            {
                writer.AppendUICultureFormattedNumber("    Failed Tests - Failures: ", summary.FailureCount);
                writer.AppendUICultureFormattedNumber(", Errors: ", summary.ErrorCount);
                writer.AppendUICultureFormattedNumber(", Invalid: ", summary.InvalidCount);
                writer.AppendLine();
            }
            if (summary.TotalSkipCount > 0)
            {
                writer.AppendUICultureFormattedNumber("    Skipped Tests - Ignored: ", summary.IgnoreCount);
                writer.AppendUICultureFormattedNumber(", Explicit: ", summary.ExplicitCount);
                writer.AppendUICultureFormattedNumber(", Other: ", summary.SkipCount);
                writer.AppendLine();
            }

            writer.AppendLine($"  Start time: {summary.StartTime:u}");
            writer.AppendLine($"    End time: {summary.EndTime:u}");
            writer.AppendLine($"    Duration: {summary.Duration:0.000}");

            return writer.ToString();
        }

        private static void AppendUICultureFormattedNumber(this StringBuilder sb, string label, int number)
        {
            sb.Append(label);
            sb.Append(number.ToString("n0", CultureInfo.CurrentUICulture));
        }
    }
}