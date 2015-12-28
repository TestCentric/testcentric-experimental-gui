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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NUnit.Gui.Model
{
    using Framework;

    public class TestSelectionTests
    {
        private TestSelection selection;

        [SetUp]
        public void CreateSelection()
        {
            selection = new TestSelection();
            selection.Add(MakeTestNode("1", "Tom", "Passed"));
            selection.Add(MakeTestNode("2", "Dick", "Failed"));
            selection.Add(MakeTestNode("3", "Harry", "Passed"));
        }

        [Test]
        public void SortByNameTest()
        {
            var sorted = selection.SortBy((x, y) => x.Name.CompareTo(y.Name));

            Assert.That(sorted, Is.SameAs(selection));
            Assert.That(sorted[0].Name, Is.EqualTo("Dick"));
            Assert.That(sorted[1].Name, Is.EqualTo("Harry"));
            Assert.That(sorted[2].Name, Is.EqualTo("Tom"));
        }

        [Test]
        public void SortByResultTest()
        {
            var sorted = selection.SortBy((x, y) => x.Outcome.ToString().CompareTo(y.Outcome.ToString()));

            Assert.That(sorted, Is.SameAs(selection));
            Assert.That(sorted[0].Name, Is.EqualTo("Dick"));
        }

        [Test]
        public void GroupByResultTest()
        {
            var groups = selection.GroupBy((tn) => tn.Outcome.Status.ToString());

            Assert.That(groups.Keys, Is.EqualTo(new string[] { "Passed", "Failed" }));
            Assert.That(groups["Passed"].Count, Is.EqualTo(2));
            Assert.That(groups["Failed"].Count, Is.EqualTo(1));
            Assert.That(groups["Failed"][0].Name, Is.EqualTo("Dick"));
        }

        [Test]
        public void RemoveTestNode()
        {
            selection.RemoveId("2");
            Assert.That(selection.Count, Is.EqualTo(2));
        }

        private TestNode MakeTestNode(string id, string name, string result)
        {
            return new TestNode(XmlHelper.CreateXmlNode(string.Format("<test-case id='{0}' name='{1}' result='{2}'/>", id, name, result)));
        }
    }
}
