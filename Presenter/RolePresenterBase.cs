using RPK.Model.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPK.Presenter
{
    public abstract class RolePresenterBase : PresenterBase
    {
        public abstract event Action ReloginRequired;

        public virtual Role Role { get; set; }
    }
}
