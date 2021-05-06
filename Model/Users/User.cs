using System;
using System.Collections.Generic;

#nullable disable

namespace RPK.Model.Users
{
    public partial class User
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public long RoleId { get; set; }

        public virtual Role Role { get; set; }
    }
}
