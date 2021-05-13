#nullable disable

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RPK.Model.Users
{
    [Display(Name = "Пользователи")]
    public partial class User : BaseModel, IEquatable<User>
    {
        [Display(Name = "Логин")]
        [Required]
        public string Login { get; set; }

        [Display(Name = "Пароль")]
        [Required]
        public string Password { get; set; }

        [Display(Name = "Идентификатор роли")]
        public long RoleId { get; set; }

        [Browsable(false)]
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
