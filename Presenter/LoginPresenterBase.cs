using RPK.Model.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RPK.Presenter
{
    public abstract class LoginPresenterBase : PresenterBase
    {
        public abstract event Action<User> UserLoggedIn;
    }
}