using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace RPK.Model.MathModel
{
    [Display(Name = "Каналы")]
    public partial class Canal : BaseModel, IEquatable<Canal>
    {
        public Canal()
        {
            CanalGeometryParameters = new HashSet<CanalGeometryParameter>();
            VariableParameters = new HashSet<VariableParameter>();
        }

        [Column("CanalId")]
        [Display(Name = "Идентификатор канала")]
        [ReadOnly(true)]
        public long Id { get; set; }

        [Display(Name = "Наименование канала")]
        [Required]
        public string Brand { get; set; }

        [Browsable(false)]
        public virtual ICollection<CanalGeometryParameter> CanalGeometryParameters { get; set; }

        [Browsable(false)]
        public virtual ICollection<VariableParameter> VariableParameters { get; set; }

        public override bool Equals(object obj)
        {
            return Equals(obj as Canal);
        }

        public bool Equals(Canal other)
        {
            return other != null &&
                   Id == other.Id;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id);
        }

        public static bool operator ==(Canal left, Canal right)
        {
            return EqualityComparer<Canal>.Default.Equals(left, right);
        }

        public static bool operator !=(Canal left, Canal right)
        {
            return !(left == right);
        }

        public override string ToString()
        {
            return Brand;
        }
    }
}
