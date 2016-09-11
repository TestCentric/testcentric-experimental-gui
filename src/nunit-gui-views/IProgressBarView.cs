using NUnit.UiKit.Controls;

namespace NUnit.Gui.Views
{
    public interface IProgressBarView : IView
    {
        int Progress { get; set; }
        TestProgressBarStatus Status { get; set; }

        void Initialize(int max);
    }
}
