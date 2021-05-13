using RPK.Model.Users;
using System;

namespace RPK.Presenter
{
    public abstract class LoginPresenterBase : PresenterBase
    {
        public abstract event Action<User> UserLoggedIn;
    }
}