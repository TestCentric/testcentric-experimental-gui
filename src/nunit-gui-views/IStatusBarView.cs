namespace NUnit.Gui.Views
{
    public interface IStatusBarView : IView
    {
        void SetStatus(string text);
        void Initialize(string text);
        void Initialize(string text, int testCount);
        void RunStarting(int testCount);
        void RunFinished(double elapsedTime);
        void RecordSuccess();
        void RecordFailure();
        void RecordError();
    }
}
