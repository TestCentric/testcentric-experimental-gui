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

using NUnit.Framework;
using System;

namespace TestCentric.Gui.Model
{
    [TestFixture]
    public class ResultSummaryCreatorTests
    {
        [Test]
        public void WhenResultNodeIsNotForTestRunExceptionIsThrown()
        {
            Assert.That(() => CreateResultSummary("<anything-other-than-test-run/>"),
                Throws.InstanceOf<InvalidOperationException>());

            Assert.That(() => CreateResultSummary("<test-run/>"),
                Throws.Nothing);
        }

        [Test]
        public void ResultOfTestRunIsValueOfOverallResult()
        {
            var innerXml = "<test-case result='Passed'/>";
            var summary = CreateResultSummary($"<test-run result='Failed'>{innerXml}</test-run>");

            Assert.That(summary.OverallResult, Is.EqualTo("Failed"));
        }

        [Test]
        public void WhenResultIsNotSpecified_PassedIsDefault()
        {
            var innerXml = "<test-case result='Failed'/>";
            var summary = CreateResultSummary($"<test-run>{innerXml}</test-run>");

            Assert.That(summary.OverallResult, Is.EqualTo("Passed"));
        }

        [Test]
        public void DurationOfTestRunIsValueOfDuration()
        {
            var innerXml = "<test-case duration='999'/>";
            var summary = CreateResultSummary($"<test-run duration='1.9'>{innerXml}</test-run>");

            Assert.That(summary.Duration, Is.EqualTo(1.9));
        }

        [Test]
        public void WhenDurationIsNotSpecified_ZeroIsDefault()
        {
            var innerXml = "<test-case duration='999'/>";
            var summary = CreateResultSummary($"<test-run>{innerXml}</test-run>");

            Assert.That(summary.Duration, Is.EqualTo(0.0));
        }

        [Test]
        public void StartTimeOfTestRunIsValueOfStartTime()
        {
            var expectedDate = new DateTime(2017, 7, 8, 6, 19, 23);
            var innerXml = $"<test-case start-time='{DateTime.MinValue:u}'/>";
            var summary = CreateResultSummary($"<test-run start-time='{expectedDate:u}'>{innerXml}</test-run>");

            Assert.That(summary.StartTime, Is.EqualTo(expectedDate));
        }

        [Test]
        public void WhenStartTimeIsNotSpecified_DateTimeMinValueIsDefault()
        {
            var innerXml = $"<test-case start-time='{DateTime.MaxValue:u}'/>";
            var summary = CreateResultSummary($"<test-run>{innerXml}</test-run>");

            Assert.That(summary.StartTime, Is.EqualTo(DateTime.MinValue));
        }

        [Test]
        public void EndTimeOfTestRunIsValueOfEndTime()
        {
            var expectedDate = new DateTime(2017, 7, 8, 6, 19, 23);
            var innerXml = $"<test-case end-time='{DateTime.MaxValue:u}'/>";
            var summary = CreateResultSummary($"<test-run end-time='{expectedDate:u}'>{innerXml}</test-run>");

            Assert.That(summary.EndTime, Is.EqualTo(expectedDate));
        }

        [Test]
        public void WhenEndTimeIsNotSpecified_DateTimeMaxValueIsDefault()
        {
            var innerXml = $"<test-case end-time='{DateTime.MinValue:u}'/>";
            var summary = CreateResultSummary($"<test-run>{innerXml}</test-run>");

            Assert.That(summary.EndTime, Is.EqualTo(DateTime.MaxValue));
        }

        [Test]
        public void TestCountIsCountOfEachNestedTestCase()
        {
            var innerXml =
                "<test-case/>" +
                "<test-case/>" +
                "<test-suite>" +
                    "<test-case/>" +
                    "<test-case/>" +
                "</test-suite>";
            var summary = CreateResultSummary($"<test-run>{innerXml}</test-run>");

            Assert.That(summary.TestCount, Is.EqualTo(4));
        }

        [Test]
        public void WhenNoResultIsSpecifiedInTestCase_SkipCountIsIncremented()
        {
            var innerXml =
                "<test-case result='Passed'/>" +
                "<test-case/>";
            var summary = CreateResultSummary($"<test-run>{innerXml}</test-run>");

            Assert.That(summary.TestCount, Is.EqualTo(2));
            Assert.That(summary.PassCount, Is.EqualTo(1));
            Assert.That(summary.SkipCount, Is.EqualTo(1));
        }

        [Test]
        public void ExtendedFailureInformationAreBasedOnLabel()
        {
            var innerXml =
                "<test-case result='Failed'/>" +
                "<test-case result='Failed' label='Invalid'/>" +
                "<test-case result='Failed' label='Anything else increases ErrorCount'/>" +
                "<test-case result='Failed' label='I am not null'/>";
            var summary = CreateResultSummary($"<test-run>{innerXml}</test-run>");

            Assert.That(summary.TestCount, Is.EqualTo(4));
            Assert.That(summary.FailedCount, Is.EqualTo(4));
            Assert.That(summary.FailureCount, Is.EqualTo(1));
            Assert.That(summary.InvalidCount, Is.EqualTo(1));
            Assert.That(summary.ErrorCount, Is.EqualTo(2));
        }

        [Test]
        public void ExtendedSkipInformationAreBasedOnLabel()
        {
            var innerXml =
                "<test-case result='Skipped'/>" +
                "<test-case result='Skipped' label='Ignored'/>" +
                "<test-case result='Skipped' label='Explicit'/>" +
                "<test-case result='Skipped' label='Anything else increases SkippedCount'/>" +
                "<test-case result='Skipped' label='I am not null'/>";
            var summary = CreateResultSummary($"<test-run>{innerXml}</test-run>");

            Assert.That(summary.TestCount, Is.EqualTo(5));
            Assert.That(summary.TotalSkipCount, Is.EqualTo(5));
            Assert.That(summary.SkipCount, Is.EqualTo(3));
            Assert.That(summary.IgnoreCount, Is.EqualTo(1));
            Assert.That(summary.ExplicitCount, Is.EqualTo(1));
        }

        [Test]
        public void InvalidTestSuitesAreTracked()
        {
            var innerXml =
                "<test-suite result='Failed' label='Invalid'/>" +
                "<test-suite result='Failed' label='Invalid' type='Assembly'/>";
            var summary = CreateResultSummary($"<test-run>{innerXml}</test-run>");

            Assert.That(summary.InvalidTestFixtures, Is.EqualTo(1));
            Assert.That(summary.InvalidAssemblies, Is.EqualTo(1));
            Assert.That(summary.UnexpectedError, Is.False);
        }

        [Test]
        public void ErrorAssembliesMarkSummaryAsUnexpectedError()
        {
            var innerXml =
                "<test-suite result='Failed' label='Invalid' type='Assembly'/>" +
                "<test-suite result='Failed' label='Error' type='Assembly'/>";
            var summary = CreateResultSummary($"<test-run>{innerXml}</test-run>");

            Assert.That(summary.InvalidAssemblies, Is.EqualTo(2));
            Assert.That(summary.UnexpectedError, Is.True);
        }

        private ResultSummary CreateResultSummary(string xml)
        {
            var resultNode = new ResultNode(xml);

            return ResultSummaryCreator.FromResultNode(resultNode);
        }
    }
}
