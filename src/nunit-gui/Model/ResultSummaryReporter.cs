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
using System.IO;
using System.Text;

namespace NUnit.Gui.Model
{
    public static class ResultSummaryReporter
    {
        public static string WriteSummaryReport(ResultSummary summary)
        {
            var sb = new StringBuilder();
            using (var writer = new StringWriter(sb))
            {
                writer.WriteLabelLine("Overall result: ", summary.OverallResult);

                writer.WriteSummaryCount("  Test Count: ", summary.TestCount);
                writer.WriteSummaryCount(", Passed: ", summary.PassCount);
                writer.WriteSummaryCount(", Failed: ", summary.FailedCount/*, ColorStyle.Failure*/);
                writer.WriteSummaryCount(", Warnings: ", summary.WarningCount/*, ColorStyle.Warning*/);
                writer.WriteSummaryCount(", Inconclusive: ", summary.InconclusiveCount);
                writer.WriteSummaryCount(", Skipped: ", summary.TotalSkipCount);
                writer.WriteLine();

                if (summary.FailedCount > 0)
                {
                    writer.WriteSummaryCount("    Failed Tests - Failures: ", summary.FailureCount);
                    writer.WriteSummaryCount(", Errors: ", summary.ErrorCount/*, ColorStyle.Error*/);
                    writer.WriteSummaryCount(", Invalid: ", summary.InvalidCount);
                    writer.WriteLine();
                }
                if (summary.TotalSkipCount > 0)
                {
                    writer.WriteSummaryCount("    Skipped Tests - Ignored: ", summary.IgnoreCount);
                    writer.WriteSummaryCount(", Explicit: ", summary.ExplicitCount);
                    writer.WriteSummaryCount(", Other: ", summary.SkipCount);
                    writer.WriteLine();
                }

                writer.WriteLabelLine("  Start time: ", summary.StartTime.ToString("u"));
                writer.WriteLabelLine("    End time: ", summary.EndTime.ToString("u"));
                writer.WriteLabelLine("    Duration: ", string.Format(NumberFormatInfo.InvariantInfo, "{0:0.000} seconds", summary.Duration));

                return sb.ToString();
            }
        }

        private static void WriteLabelLine(this TextWriter writer, string line, object option)
        {
            writer.WriteLabel(line, option);
            writer.WriteLine();
        }

        private static void WriteLabel(this TextWriter writer, string label, object option)
        {
            writer.Write(label);
            writer.Write(option.ToString());
        }

        private static void WriteSummaryCount(this TextWriter writer, string label, int count)
        {
            writer.WriteLabel(label, count.ToString(CultureInfo.CurrentUICulture));
        }
    }
}