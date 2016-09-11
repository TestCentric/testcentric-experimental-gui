using NUnit.UiKit.Elements;
using System.Xml;

namespace NUnit.Gui.Views
{
    public interface IXmlView : IView
    {
        bool Visible { get; set; }
        string Header { get; set; }
        IViewElement XmlPanel { get; }
        XmlNode TestXml { get; set; }
    }
}
