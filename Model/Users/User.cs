#nullable disable

using System;
using System.Collections.Generic;

namespace RPK.Model.Users
{
    public partial class User : IEquatable<User>
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public long RoleId { get; set; }

        public virtual Role Role { get; set; }

        public override bool Equals(object obj)
        {
            return Equals(obj as User);
        }

        public bool Equals(User other)
        {
            return other != null &&
                   Login == other.Login;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Login);
        }

        public static bool operator ==(User left, User right)
        {
            return EqualityComparer<User>.Default.Equals(left, right);
        }

        public static bool operator !=(User left, User right)
        {
            return !(left == right);
        }
    }
}
