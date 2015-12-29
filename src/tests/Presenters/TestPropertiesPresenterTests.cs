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
    [TestCase("Assembly", "test.dll", "Runnable", ExpectedResult = "Assembly")]
    [TestCase("Assembly", "test.dll", "NotRunnable", ExpectedResult = "Assembly")]
    [TestCase("Assembly", "test.dll", "Ignored", ExpectedResult = "Assembly")]
    [TestCase("Assembly", "test.dll", "Explicit", ExpectedResult = "Assembly")]
    [TestCase("Assembly", "test.dll", "Skipped", ExpectedResult = "Assembly")]
    [TestCase("Assembly", "test.dll", "Unknown", ExpectedResult = "Assembly")]
    [TestCase("Assembly", "test.exe", "Runnable", ExpectedResult = "Assembly")]
    [TestCase("Assembly", "test.exe", "NotRunnable", ExpectedResult = "Assembly")]
    [TestCase("Assembly", "test.exe", "Ignored", ExpectedResult = "Assembly")]
    [TestCase("Assembly", "test.exe", "Explicit", ExpectedResult = "Assembly")]
    [TestCase("Assembly", "test.exe", "Skipped", ExpectedResult = "Assembly")]
    [TestCase("Assembly", "test.exe", "Unknown", ExpectedResult = "Assembly")]
    [TestCase("Assembly", "test.png", "Runnable", ExpectedResult = "Assembly")]
    [TestCase("Assembly", "test.png", "NotRunnable", ExpectedResult = "Unknown")]
    [TestCase("Assembly", "test.png", "Ignored", ExpectedResult = "Assembly")]
    [TestCase("Assembly", "test.png", "Explicit", ExpectedResult = "Assembly")]
    [TestCase("Assembly", "test.png", "Skipped", ExpectedResult = "Assembly")]
    [TestCase("Assembly", "test.png", "Unknown", ExpectedResult = "Assembly")]
    [TestCase("SomethingElse", "test.png", "Runnable", ExpectedResult = "SomethingElse")]
    [TestCase("SomethingElse", "test.png", "NotRunnable", ExpectedResult = "SomethingElse")]
    [TestCase("SomethingElse", "test.dll", "Runnable", ExpectedResult = "SomethingElse")]
    [TestCase("SomethingElse", "test.dll", "NotRunnable", ExpectedResult = "SomethingElse")]
    public string GetTestTypeTest(string type, string fullname, string runstate)
    {
      TestNode testNode = new TestNode(XmlHelper.CreateXmlNode(string.Format("<test-run id='1' type='{0}' fullname='{1}' runstate='{2}'><test-suite id='42'/></test-run>", type, fullname, runstate)));

      return TestPropertiesPresenter.GetTestType(testNode);
    }

  }
}