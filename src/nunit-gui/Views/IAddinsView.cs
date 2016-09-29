using System;
using NUnit.Engine.Extensibility;

namespace NUnit.Gui.Views
{
    public interface IAddinsView : IDialog
    {
        void AddExtensionPoint(IExtensionPoint extensionPoint);
    }
}
