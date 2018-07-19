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

using System.Collections.Generic;
using System.Xml;
using NUnit.Framework;

namespace TestCentric.Gui.Presenters
{
    using Model;

    public class TestGroupTests
    {
        private TestGroup _group;

        [SetUp]
        public void CreateTestGroup()
        {
            _group = new TestGroup("NAME", 7);
            _group.Add(MakeTestNode("1", "Tom", "Runnable"));
            _group.Add(MakeTestNode("2", "Dick", "Explicit"));
            _group.Add(MakeTestNode("3", "Harry", "Runnable"));
        }

        [Test]
        public void GroupName()
        {
            Assert.That(_group.Name, Is.EqualTo("NAME"));
        }

        [Test]
        public void ImageIndex()
        {
            Assert.That(_group.ImageIndex, Is.EqualTo(7));
        }

        [Test]
        public void TestFilter()
        {
            var filter = XmlHelper.CreateXmlNode(_group.GetTestFilter().Text);
            Assert.That(filter.Name, Is.EqualTo("filter"));
            Assert.That(filter.ChildNodes.Count, Is.EqualTo(1));

            filter = filter.ChildNodes[0];
            Assert.That(filter.Name, Is.EqualTo("or"));
            Assert.That(filter.ChildNodes.Count, Is.EqualTo(2));

            var ids = new List<string>();
            foreach (XmlNode child in filter.ChildNodes)
                ids.Add(child.InnerText);
            Assert.That(ids, Is.EqualTo(new string[] { "1", "3" }));
        }

        private TestNode MakeTestNode(string id, string name, string runState)
        {
            return new TestNode(string.Format("<test-case id='{0}' name='{1}' runstate='{2}'/>", id, name, runState));
        }
    }
}
