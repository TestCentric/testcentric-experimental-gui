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

using System.IO;
using System.Xml;

namespace TestCentric.Gui
{
    /// <summary>
    /// XmlHelper provides static methods for basic XML operations
    /// </summary>
    /// <remarks>
    /// This helper class is used in various NUnit modules.
    /// Unused methods are currently commented out in those
    /// modules that don't make use of them.
    /// </remarks>
    public static class XmlHelper
    {
        /// <summary>
        /// Creates a new top level element node.
        /// </summary>
        /// <param name="name">The element name.</param>
        /// <returns>A new XmlNode</returns>
        public static XmlNode CreateTopLevelElement(string name)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml("<" + name + "/>");
            return doc.FirstChild;
        }

        public static XmlNode CreateXmlNode(string xml)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml);
            return doc.FirstChild;
        }

        ///// <summary>
        ///// Adds an attribute with a specified name and val to an existing XmlNode.
        ///// </summary>
        ///// <param name="treeNode">The treeNode to which the attribute should be added.</param>
        ///// <param name="name">The name of the attribute.</param>
        ///// <param name="val">The val of the attribute.</param>
        //public static void AddAttribute(this XmlNode treeNode, string name, string val)
        //{
        //    XmlAttribute attr = treeNode.OwnerDocument.CreateAttribute(name);
        //    attr.Value = val;
        //    treeNode.Attributes.Append(attr);
        //}

        ///// <summary>
        ///// Adds a new element as a child of an existing XmlNode and returns it.
        ///// </summary>
        ///// <param name="treeNode">The treeNode to which the element should be added.</param>
        ///// <param name="name">The element name.</param>
        ///// <returns>The newly created child element</returns>
        //public static XmlNode AddElement(XmlNode treeNode, string name)
        //{
        //    XmlNode childNode = treeNode.OwnerDocument.CreateElement(name);
        //    treeNode.AppendChild(childNode);
        //    return childNode;
        //}

        ///// <summary>
        ///// Adds the a new element as a child of an existing treeNode and returns it.
        ///// A CDataSection is added to the new element using the data provided.
        ///// </summary>
        ///// <param name="treeNode">The treeNode to which the element should be added.</param>
        ///// <param name="name">The element name.</param>
        ///// <param name="data">The data for the CDataSection.</param>
        ///// <returns></returns>
        //public static XmlNode AddElementWithCDataSection(XmlNode treeNode, string name, string data)
        //{
        //    XmlNode childNode = AddElement(treeNode, name);
        //    childNode.AppendChild(treeNode.OwnerDocument.CreateCDataSection(data));
        //    return childNode;
        //}

        #region Safe Attribute Access

        public static string GetAttribute(this XmlNode result, string name)
        {
            XmlAttribute attr = result.Attributes[name];

            return attr == null ? null : attr.Value;
        }

        public static int GetAttribute(this XmlNode result, string name, int defaultValue)
        {
            XmlAttribute attr = result.Attributes[name];

            return attr == null
                ? defaultValue
                : int.Parse(attr.Value, System.Globalization.CultureInfo.InvariantCulture);
        }

        //public static double GetAttribute(this XmlNode suiteResult, string name, double defaultValue)
        //{
        //    XmlAttribute attr = suiteResult.Attributes[name];

        //    return attr == null
        //        ? defaultValue
        //        : double.Parse(attr.Value, System.Globalization.CultureInfo.InvariantCulture);
        //}

        #endregion

        public static string ToFormattedString(System.Xml.XmlNode node, int indentation)
        {
            using (var sw = new System.IO.StringWriter())
            {
                using (var xw = new System.Xml.XmlTextWriter(sw))
                {
                    xw.Formatting = System.Xml.Formatting.Indented;
                    xw.Indentation = indentation;
                    node.WriteTo(xw);
                }
                return sw.ToString();
            }
        }

        public static string ToRtfString(XmlNode node, int indentationSize)
        {
            var converter = new Xml2RtfConverter(2);
            return converter.Convert(node);
        }
    }
}
