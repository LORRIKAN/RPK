using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPK.Login.Presenter
{
    public class User : Model.Users.User, IEquatable<User>
    {
        public override bool Equals(object obj)
        {
            return Equals(obj as User);
        }

        public bool Equals(User other)
        {
            return other != null &&
                   Login == other.Login &&
                   RoleId == other.RoleId;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Login, RoleId);
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