using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

        [Column("RoleId")]
        [Display(Name = "Идентификатор роли")]
        [ReadOnly(true)]
        public long Id { get; set; }

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
                   Id == other.Id;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id);
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
