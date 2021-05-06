using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPK.Login.Presenter
{
    public class Role : RPK.Model.Users.Role, IEquatable<Role>
    {
        public override bool Equals(object obj)
        {
            return Equals(obj as Role);
        }

        public bool Equals(Role other)
        {
            return other != null &&
                   RoleId == other.RoleId;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(RoleId);
        }

        public static bool operator ==(Role left, Role right)
        {
            return EqualityComparer<Role>.Default.Equals(left, right);
        }

        public static bool operator !=(Role left, Role right)
        {
            return !(left == right);
        }
    }
}