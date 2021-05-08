using System;
using System.Collections.Generic;

#nullable disable

namespace RPK.Model.Users
{
    public partial class Role : IEquatable<Role>
    {
        public Role()
        {
            Users = new HashSet<User>();
        }

        public long RoleId { get; set; }
        public string RoleName { get; set; }

        public virtual ICollection<User> Users { get; set; }

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
