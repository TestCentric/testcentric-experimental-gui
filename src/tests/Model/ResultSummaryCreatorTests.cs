using NUnit.Framework;
using System;

namespace NUnit.Gui.Model
{
    [TestFixture]
    public class ResultSummaryCreatorTests
    {
        [Test]
        public void WhenResultNodeIsNotForTestRunExceptionIsThrown()
        {
            Assert.That(() => CreateResultSummary("<anything-other-than-test-run/>"), Throws.InstanceOf<InvalidOperationException>());

            Assert.That(() => CreateResultSummary("<test-run/>"), Throws.Nothing);
        }
        
        [Test]
        public void WhenResultIsNotSpecified_PassedIsDefault()
        {
            var summary = CreateResultSummary("<test-run/>");

            Assert.That(summary.OverallResult, Is.EqualTo("Passed"));

            summary = CreateResultSummary("<test-run result='Failed'/>");

            Assert.That(summary.OverallResult, Is.EqualTo("Failed"));
        }

        [Test]
        public void WhenDurationIsNotSpecified_ZeroIsDefault()
        {
            var summary = CreateResultSummary("<test-run/>");

            Assert.That(summary.Duration, Is.EqualTo(0.0));

            summary = CreateResultSummary("<test-run duration='1.9'/>");

            Assert.That(summary.Duration, Is.EqualTo(1.9));
        }

        [Test]
        public void WhenStartTimeIsNotSpecified_DateTimeMinValueIsDefault()
        {
            var summary = CreateResultSummary("<test-run/>");

            Assert.That(summary.StartTime, Is.EqualTo(DateTime.MinValue));

            var expectedDate = new DateTime(2017, 7, 8, 6, 19, 23);
            summary = CreateResultSummary($"<test-run start-time='{expectedDate.ToString("u")}'/>");

            Assert.That(summary.StartTime, Is.EqualTo(expectedDate));
        }

        [Test]
        public void WhenEndTimeIsNotSpecified_DateTimeMaxValueIsDefault()
        {
            var summary = CreateResultSummary("<test-run/>");

            Assert.That(summary.EndTime, Is.EqualTo(DateTime.MaxValue));

            var expectedDate = new DateTime(2017, 7, 8, 6, 21, 46);
            summary = CreateResultSummary($"<test-run end-time='{expectedDate.ToString("u")}'/>");

            Assert.That(summary.EndTime, Is.EqualTo(expectedDate));
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
        public void WhenNoResultIsNotSpecifiedInTestCase_SkipCountIsIncremented()
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
                "<test-suite result='Failed' label='Error' type='Assembly'/>" +
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
