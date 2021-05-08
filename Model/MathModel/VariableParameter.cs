using System;
using System.Collections.Generic;

namespace RPK.Model.MathModel
{
    public partial class VariableParameter : IEquatable<VariableParameter>
    {
        public long MaterialId { get; set; }
        public long CanalId { get; set; }
        public long ParameterId { get; set; }
        public double ValueUpperBound { get; set; }
        public double ValueLowerBound { get; set; }

        public virtual Canal Canal { get; set; }
        public virtual Material Material { get; set; }
        public virtual Parameter Parameter { get; set; }

        public override bool Equals(object obj)
        {
            return Equals(obj as VariableParameter);
        }

        public bool Equals(VariableParameter other)
        {
            return other != null &&
                   MaterialId == other.MaterialId &&
                   CanalId == other.CanalId &&
                   ParameterId == other.ParameterId;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(MaterialId, CanalId, ParameterId);
        }

        public static bool operator ==(VariableParameter left, VariableParameter right)
        {
            return EqualityComparer<VariableParameter>.Default.Equals(left, right);
        }

        public static bool operator !=(VariableParameter left, VariableParameter right)
        {
            return !(left == right);
        }
    }
}
