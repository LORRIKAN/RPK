
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RPK.Model.MathModel
{
    [Display(Name = "Параметры свойств материалов")]
    public partial class ParameterOfMaterialProperty : ParameterTypeBase, IEquatable<ParameterOfMaterialProperty>
    {
        [Display(Name = "Идентификатор параметра")]
        [Required]
        public override long ParameterId { get; set; }

        [Display(Name = "Идентификатор материала")]
        [Required]
        public long MaterialId { get; set; }

        [Display(Name = "Значение параметра")]
        [Required]
        public double ParameterValue { get; set; }

        [Browsable(false)]
        public virtual Material Material { get; set; }

        [Browsable(false)]
        public virtual Parameter Parameter { get; set; }

        public override Range UnchangeableRows => new(0, 2);

        public override bool Equals(object obj)
        {
            return Equals(obj as ParameterOfMaterialProperty);
        }

        public bool Equals(ParameterOfMaterialProperty other)
        {
            return other != null &&
                   ParameterId == other.ParameterId &&
                   MaterialId == other.MaterialId;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(ParameterId, MaterialId);
        }

        public static bool operator ==(ParameterOfMaterialProperty left, ParameterOfMaterialProperty right)
        {
            return EqualityComparer<ParameterOfMaterialProperty>.Default.Equals(left, right);
        }

        public static bool operator !=(ParameterOfMaterialProperty left, ParameterOfMaterialProperty right)
        {
            return !(left == right);
        }
    }
}
