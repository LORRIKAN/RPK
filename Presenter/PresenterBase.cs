using System.Windows.Forms;

namespace RPK.Presenter
{
    public abstract class PresenterBase
    {
        public abstract Form Form { get; }

        public virtual Form Run()
        {
            return Form;
        }
    }
}
