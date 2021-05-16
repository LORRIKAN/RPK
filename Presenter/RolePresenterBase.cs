using RPK.Model.Users;
using System;
using System.Windows.Forms;

namespace RPK.Presenter
{
    public abstract class RolePresenterBase : PresenterBase
    {
        public abstract event Action ReloginRequired;

        public virtual Role Role { get; set; }

        public abstract Form Run(User user);
    }
}
