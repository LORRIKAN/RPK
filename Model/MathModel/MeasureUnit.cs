using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace RPK.Model.MathModel
{
    [Display(Name = "Единицы измерения")]
    public partial class MeasureUnit : BaseModel, IEquatable<MeasureUnit>
    {
        public MeasureUnit()
        {
            Parameters = new HashSet<Parameter>();
        }

        [Display(Name = "Единица измерения")]
        [Required]
        public string Value { get; set; }

        [Browsable(false)]
        public virtual ICollection<Parameter> Parameters { get; set; }

        public override bool Equals(object obj)
        {
            return Equals(obj as MeasureUnit);
        }

        public bool Equals(MeasureUnit other)
        {
            return other != null &&
                   Value == other.Value;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Value);
        }

        public static bool operator ==(MeasureUnit left, MeasureUnit right)
        {
            return EqualityComparer<MeasureUnit>.Default.Equals(left, right);
        }

        public static bool operator !=(MeasureUnit left, MeasureUnit right)
        {
            return !(left == right);
        }
    }
}
