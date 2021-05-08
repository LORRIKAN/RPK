
using System;
using System.Collections.Generic;

namespace RPK.Model.MathModel
{
    public partial class ParameterOfMaterialProperty : IEquatable<ParameterOfMaterialProperty>
    {
        public long ParameterId { get; set; }
        public long MaterialId { get; set; }
        public double ParameterValue { get; set; }

        public virtual Material Material { get; set; }
        public virtual Parameter Parameter { get; set; }

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
