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

using NUnit.Framework;

namespace TestCentric.Gui.Model
{
    public class TestNodeTests
    {
        [Test]
        public void CreateTestCase()
        {
            var testNode = new TestNode("<test-case id='123' name='SomeTest' fullname='A.B.C.SomeTest' runstate='Ignored'/>");

            Assert.That(testNode.Id, Is.EqualTo("123"));
            Assert.That(testNode.Name, Is.EqualTo("SomeTest"));
            Assert.That(testNode.FullName, Is.EqualTo("A.B.C.SomeTest"));
            Assert.That(testNode.Type, Is.EqualTo("TestCase"));
            Assert.That(testNode.TestCount, Is.EqualTo(1));
            Assert.That(testNode.RunState, Is.EqualTo(RunState.Ignored));
            Assert.False(testNode.IsSuite);
        }

        [Test]
        public void CreateTestSuite()
        {
            var testNode = new TestNode("<test-suite id='123' type='Assembly' name='mytest.dll' fullname='/A/B/C/mytest.dll' testcasecount='42' runstate='Runnable'/>");

            Assert.That(testNode.Id, Is.EqualTo("123"));
            Assert.That(testNode.Name, Is.EqualTo("mytest.dll"));
            Assert.That(testNode.FullName, Is.EqualTo("/A/B/C/mytest.dll"));
            Assert.That(testNode.Type, Is.EqualTo("Assembly"));
            Assert.That(testNode.TestCount, Is.EqualTo(42));
            Assert.That(testNode.RunState, Is.EqualTo(RunState.Runnable));
            Assert.That(testNode.IsSuite);
        }

        [Test]
        public void CreateTestRun()
        {
            var testNode = new TestNode("<test-run id='123' name='mytest.dll' fullname='/A/B/C/mytest.dll' testcasecount='99'/>");

            Assert.That(testNode.Id, Is.EqualTo("123"));
            Assert.That(testNode.Name, Is.EqualTo("mytest.dll"));
            Assert.That(testNode.FullName, Is.EqualTo("/A/B/C/mytest.dll"));
            Assert.That(testNode.TestCount, Is.EqualTo(99));
            Assert.That(testNode.IsSuite);
        }
    }
}
