#nullable disable


using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RPK.Model.MathModel
{
    [Display(Name = "Эмпирические коэффициенты математической модели")]
    public partial class EmpiricalCoefficientOfMathModel : ParameterTypeBase, IEquatable<EmpiricalCoefficientOfMathModel>
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

        public override bool Equals(object obj)
        {
            return Equals(obj as EmpiricalCoefficientOfMathModel);
        }

        public bool Equals(EmpiricalCoefficientOfMathModel other)
        {
            return other != null &&
                   ParameterId == other.ParameterId &&
                   MaterialId == other.MaterialId;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(ParameterId, MaterialId);
        }

        public static bool operator ==(EmpiricalCoefficientOfMathModel left, EmpiricalCoefficientOfMathModel right)
        {
            return EqualityComparer<EmpiricalCoefficientOfMathModel>.Default.Equals(left, right);
        }

        public static bool operator !=(EmpiricalCoefficientOfMathModel left, EmpiricalCoefficientOfMathModel right)
        {
            return !(left == right);
        }
    }
}
