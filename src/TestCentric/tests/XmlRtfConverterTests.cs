//  LEGAL INFORMATION:

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Xml;

using NUnit.Framework;

namespace TestCentric.Gui
{
    public class XmlRtfConverterTests
    {
        [Test]
        public void SimpleXmlTest()
        {
            var xml = @"
<A a='1' b='2'>
  <B/>
  <!-- this is a comment -->
  <C>C content</C>
  <D>
<![CDATA[
Within this Character Data block I can use < >
]]>
  </D>
</A>";
            //We use an empty color table - all colors will be black.
            var converter = new Xml2RtfConverter(2, new Dictionary<Xml2RtfConverter.ColorKinds, Color>());
            var doc = new XmlDocument();
            doc.LoadXml(xml);
            var rtf = converter.Convert(doc.FirstChild);
            //Check the header
            int headerLength = 259 + 3 * Environment.NewLine.Length;
            Assert.That(rtf.Substring(0, headerLength), Is.EqualTo("{\\rtf1\\ansi\\ansicpg1252\\deff0\\deflang1033{\\fonttbl{\\f0\\fnil\\fcharset0 Courier New;}}" + Environment.NewLine +
                @"{{\colortbl ;\red0\green0\blue0;\red0\green0\blue0;\red0\green0\blue0;\red0\green0\blue0;\red0\green0\blue0;\red0\green0\blue0;\red0\green0\blue0;}}" + Environment.NewLine +
                @"\viewkind4\uc1\pard\f0\fs20" + Environment.NewLine));
            //CHeck the content
            Assert.That(rtf.Substring(headerLength), Is.EqualTo(
                @"\cf5<\cf1A \cf3a=\cf4'1' \cf3b=\cf4'2'\cf5>\par" +
                @"  \cf5<\cf1B \cf5/>\par" +
                @"  \cf6<!-- this is a comment -->\par" +
                @"  \cf5<\cf1C\cf5>\cf2C content\cf5</\cf1C\cf5>\par" +
                @"  \cf5<\cf1D\cf5>\par" +
                @"\cf7<![CDATA[\par " +
                @"Within this Character Data block I can use &lt; &gt;\par" +  //note indentation in the CDATA is purely from based on the original CDATA content
                @"]]>\par" +
                @"  \cf5</\cf1D\cf5>\par" +
                @"\cf5</\cf1A\cf5>\par"));
        }
    }
}
