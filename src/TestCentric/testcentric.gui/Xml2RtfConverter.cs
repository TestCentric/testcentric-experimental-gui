using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Xml;

namespace TestCentric.Gui
{
    public class Xml2RtfConverter
    {
        public enum ColorKinds
        {
            Element = 1,
            Value,
            Attribute,
            String,
            Tag,
            Comment,
            CData
        }

        private Dictionary<ColorKinds, Color> _colorTable = new Dictionary<ColorKinds, Color>();
        private readonly int indentationSize;

        public Xml2RtfConverter(int indentationSize, Dictionary<ColorKinds, Color> colorTable = null)
        {
            this.indentationSize = indentationSize;
            if (colorTable != null)
                _colorTable = colorTable;
            else
            {
                _colorTable.Add(ColorKinds.Element, Color.Purple);
                _colorTable.Add(ColorKinds.Value, Color.Black);
                _colorTable.Add(ColorKinds.Attribute, Color.DarkGoldenrod);
                _colorTable.Add(ColorKinds.String, Color.DarkBlue);
                _colorTable.Add(ColorKinds.Tag, Color.Purple);
                _colorTable.Add(ColorKinds.Comment, Color.Gray);
                _colorTable.Add(ColorKinds.CData, Color.DarkBlue);
            }
        }

        public string Convert(XmlNode node)
        {
            var sb = new StringBuilder();
            CreateHeader(sb);
            AddXmlNode(sb, node, 0);
            return sb.ToString();
        }

        private void AddXmlNode(StringBuilder sb, XmlNode node, int indentationLevel)
        {
            var element = node as XmlElement;
            if (element != null)
            {
                AddXmlElement(sb, element, indentationLevel);
                return;
            }

            var xmlText = node as XmlText;
            if (xmlText != null)
            {
                AddXmlText(sb, indentationLevel, xmlText);
                return;
            }

            var xmlComment = node as XmlComment;
            if (xmlComment != null)
            {
                AddXmlComment(sb, indentationLevel, xmlComment);
                return;
            }

            var cdata = node as XmlCDataSection;
            if (cdata != null)
            {
                AddXmlCDataSection(sb, cdata);
                return;
            }
        }

        private static void AddXmlCDataSection(StringBuilder sb, XmlCDataSection cdata)
        {
            sb.Append(string.Format(@"\cf{0}<![CDATA[\par ", (int)ColorKinds.CData));
            var cdataLines = cdata.Value.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            var res = new List<string>(cdataLines.Length);
            foreach (var cdataLine in cdataLines)
            {
                res.Add(XmlEncode(cdataLine));
            }
            sb.Append(string.Join(@"\par", res.ToArray()));
            sb.Append(@"\par]]>\par");
        }

        private void AddXmlComment(StringBuilder sb, int indentationLevel, XmlComment xmlComment)
        {
            Indent(sb, indentationLevel);
            sb.Append(string.Format(@"\cf{0}<!--{1}-->\par", (int)ColorKinds.Comment, XmlEncode(xmlComment.Value)));
        }

        private void AddXmlText(StringBuilder sb, int indentationLevel, XmlText xmlText)
        {
            Indent(sb, indentationLevel);
            sb.Append(string.Format(@"\cf{0}{1}\par", (int)ColorKinds.Value, XmlEncode(xmlText.Value)));
        }

        private void AddXmlElement(StringBuilder sb, XmlElement element, int indentationLevel)
        {
            Indent(sb, indentationLevel);
            if (AddStartElement(sb, element)) return;
            AddElementContent(sb, element, indentationLevel);
            AddEndElement(sb, element);
        }

        private bool AddStartElement(StringBuilder sb, XmlElement element)
        {
            sb.Append(string.Format(@"\cf{0}<\cf{1}{2}", (int)ColorKinds.Tag, (int)ColorKinds.Element, element.Name));
            AddXmlAttributes(sb, element);
            //If Element has no content we can end the element now.
            if (!element.HasChildNodes)
            {
                sb.Append(string.Format(@" \cf{0}/>\par", (int)ColorKinds.Tag));
                return true;
            }
            //otherwise just end the start fo the element.
            sb.Append(string.Format(@"\cf{0}>", (int)ColorKinds.Tag));
            return false;
        }

        private static void AddXmlAttributes(StringBuilder sb, XmlElement element)
        {
            if (element.HasAttributes)
            {
                foreach (XmlAttribute attribute in element.Attributes)
                {
                    sb.Append(string.Format(@" \cf{0}{1}=\cf{2}'{3}'", (int)ColorKinds.Attribute, attribute.Name, (int)ColorKinds.String, XmlEncode(attribute.Value)));
                }
            }
        }

        private void AddElementContent(StringBuilder sb, XmlElement element, int indentationLevel)
        {
            if (HasSingleTextNode(element))
            {
                sb.Append(string.Format(@"\cf{0}{1}", (int)ColorKinds.Value, XmlEncode(element.FirstChild.Value)));
            }
            else if (element.HasChildNodes)
            {
                sb.Append(@"\par");
                foreach (XmlNode childNode in element.ChildNodes)
                {
                    AddXmlNode(sb, childNode, indentationLevel + 1);
                }
                Indent(sb, indentationLevel);
            }
        }

        private static void AddEndElement(StringBuilder sb, XmlElement element)
        {
            sb.Append(string.Format(@"\cf{0}</\cf{1}{2}\cf{0}>\par", (int)ColorKinds.Tag, (int)ColorKinds.Element, element.Name));
        }

        private void Indent(StringBuilder sb, int indentationLevel)
        {
            sb.Append(new string(' ', indentationSize * indentationLevel));
        }

        private bool HasSingleTextNode(XmlElement element)
        {
            return (element.ChildNodes.Count == 1) && (element.FirstChild is XmlText);
        }

        private static string XmlEncode(string value)
        {
            var sb = new StringBuilder();
            foreach (char c in value)
            {
                switch (c)
                {
                    //xml
                    case '\'':
                        sb.Append(@"&apos;");
                        break;
                    case '"':
                        sb.Append("&quot;");
                        break;
                    case '&':
                        sb.Append(@"&amp;");
                        break;
                    case '<':
                        sb.Append(@"&lt;");
                        break;
                    case '>':
                        sb.Append(@"&gt;");
                        break;
                    //these are for rtf-support
                    case '\\':
                        sb.Append(@"\\");
                        break;
                    case '{':
                        sb.Append(@"\{");
                        break;
                    case '}':
                        sb.Append(@"\}");
                        break;
                    default:
                        sb.Append(c);
                        break;
                }
            }
            return sb.ToString();
        }

        private void CreateHeader(StringBuilder sb)
        {
            sb.AppendLine(@"{\rtf1\ansi\ansicpg1252\deff0\deflang1033{\fonttbl{\f0\fnil\fcharset0 Courier New;}}");
            sb.Append(@"{{\colortbl ;");
            foreach (ColorKinds colorKind in Enum.GetValues(typeof(ColorKinds)))
            {
                Color color;
                if (_colorTable.TryGetValue(colorKind, out color))
                    sb.Append(string.Format(@"\red{0}\green{1}\blue{2};", color.R, color.G, color.B));
                else
                    sb.Append(string.Format(@"\red{0}\green{1}\blue{2};", 0, 0, 0));
            }
            sb.AppendLine("}}");
            sb.AppendLine(@"\viewkind4\uc1\pard\f0\fs20");
        }
    }
}
