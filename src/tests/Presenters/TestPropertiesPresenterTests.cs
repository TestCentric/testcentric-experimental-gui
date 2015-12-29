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

using System.Windows.Forms;

using NSubstitute;

using NUnit.Framework;
using NUnit.Framework.Internal.Filters;
using NUnit.Gui.Model;
using NUnit.UiKit.Elements;

namespace NUnit.Gui.Presenters
{
  public class TestPropertiesPresenterTests
  {
    [TestCase("test.dll", "Runnable", ExpectedResult = "Assembly")]
    [TestCase("test.dll", "NotRunnable", ExpectedResult = "Assembly")]
    [TestCase("test.dll", "Ignored", ExpectedResult = "Assembly")]
    [TestCase("test.dll", "Explicit", ExpectedResult = "Assembly")]
    [TestCase("test.dll", "Skipped", ExpectedResult = "Assembly")]
    [TestCase("test.dll", "Unknown", ExpectedResult = "Assembly")]
    [TestCase("test.exe", "Runnable", ExpectedResult = "Assembly")]
    [TestCase("test.exe", "NotRunnable", ExpectedResult = "Assembly")]
    [TestCase("test.exe", "Ignored", ExpectedResult = "Assembly")]
    [TestCase("test.exe", "Explicit", ExpectedResult = "Assembly")]
    [TestCase("test.exe", "Skipped", ExpectedResult = "Assembly")]
    [TestCase("test.exe", "Unknown", ExpectedResult = "Assembly")]
    [TestCase("test.png", "Runnable", ExpectedResult = "Assembly")]
    [TestCase("test.png", "NotRunnable", ExpectedResult = "Unknown")]
    [TestCase("test.png", "Ignored", ExpectedResult = "Assembly")]
    [TestCase("test.png", "Explicit", ExpectedResult = "Assembly")]
    [TestCase("test.png", "Skipped", ExpectedResult = "Assembly")]
    [TestCase("test.png", "Unknown", ExpectedResult = "Assembly")]
    public string GetTestTypeTest(string fullname, string runstate)
    {
      TestNode testNode = new TestNode(XmlHelper.CreateXmlNode(string.Format("<test-run id='1' type='Assembly' fullname='{0}' runstate='{1}'><test-suite id='42'/></test-run>", fullname, runstate)));

      return TestPropertiesPresenter.GetTestType(testNode);
    }

  }
}