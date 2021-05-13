using RPK.Model.Users;
using System;

namespace RPK.Presenter
{
    public abstract class RolePresenterBase : PresenterBase
    {
        public abstract event Action ReloginRequired;

        public virtual Role Role { get; set; }
    }
}
