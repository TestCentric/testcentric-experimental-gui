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
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace NUnit.Gui.Presenters
{
    using Model;
    using Views;

    public class GroupByCategory : TestGrouping
    {
        public GroupByCategory(GroupDisplay display) : base(display)
        {
            // There are no predefined groups for this TestGrouping
        }

        protected override void AddGroup(TestGroup group)
        {
            base.AddGroup(group);

            _groupList.Sort((x, y) =>
            {
                bool xNone = x.Name == "None";
                bool yNone = y.Name == "None";

                if (xNone && yNone) return 0;

                if (xNone) return 1;

                if (yNone) return -1;

                return x.Name.CompareTo(y.Name);
            });
        }
        public override string[] SelectGroups(TestNode testNode)
        {
            List<string> categories = new List<string>();

            foreach (XmlNode node in testNode.Xml.SelectNodes("properties/property[@name='Category']"))
                categories.Add(node.GetAttribute("value"));

            if (categories.Count == 0)
                categories.Add("None");

            return categories.ToArray();
        }
    }
}
