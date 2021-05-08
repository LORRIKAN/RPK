#nullable disable


using System;
using System.Collections.Generic;

namespace RPK.Model.MathModel
{
    public partial class CanalGeometryParameter : IEquatable<CanalGeometryParameter>
    {
        public long CanalId { get; set; }
        public long ParameterId { get; set; }
        public double ParameterValue { get; set; }

        public virtual Canal Canal { get; set; }
        public virtual Parameter Parameter { get; set; }

        public override bool Equals(object obj)
        {
            return Equals(obj as CanalGeometryParameter);
        }

        public bool Equals(CanalGeometryParameter other)
        {
            return other != null &&
                   CanalId == other.CanalId &&
                   ParameterId == other.ParameterId;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(CanalId, ParameterId);
        }

        public static bool operator ==(CanalGeometryParameter left, CanalGeometryParameter right)
        {
            return EqualityComparer<CanalGeometryParameter>.Default.Equals(left, right);
        }

        public static bool operator !=(CanalGeometryParameter left, CanalGeometryParameter right)
        {
            return !(left == right);
        }
    }
}
