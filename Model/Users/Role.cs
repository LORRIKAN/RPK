using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace RPK.Model.Users
{
    [Display(Name = "Роли")]
    public partial class Role : BaseModel, IEquatable<Role>
    {
        public Role()
        {
            Users = new HashSet<User>();
        }

        [Display(Name = "Идентификатор роли")]
        [ReadOnly(true)]
        public long RoleId { get; set; }

        [Display(Name = "Наименование роли")]
        [Required]
        public string RoleName { get; set; }

        [Browsable(false)]
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
