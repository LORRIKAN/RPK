#nullable disable


using System;
using System.Collections.Generic;

namespace RPK.Model.MathModel
{
    public partial class EmpiricalCoefficientOfMathModel : IEquatable<EmpiricalCoefficientOfMathModel>
    {
        public long ParameterId { get; set; }
        public long MaterialId { get; set; }
        public double ParameterValue { get; set; }

        public virtual Material Material { get; set; }
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
