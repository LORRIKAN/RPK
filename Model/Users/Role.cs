﻿using System;
using System.Collections.Generic;

#nullable disable

namespace RPK.Model.Users
{
    public partial class Role
    {
        public Role()
        {
            Users = new HashSet<User>();
        }

        public long RoleId { get; set; }
        public string Role1 { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
