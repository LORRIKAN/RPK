using System;
using System.Collections.Generic;

#nullable disable

namespace RPK.Model.MathModel
{
    public partial class Canal : IEquatable<Canal>
    {
        public Canal()
        {
            CanalGeometryParameters = new HashSet<CanalGeometryParameter>();
            VariableParameters = new HashSet<VariableParameter>();
        }

        public long CanalId { get; set; }
        public string Brand { get; set; }

        public virtual ICollection<CanalGeometryParameter> CanalGeometryParameters { get; set; }
        public virtual ICollection<VariableParameter> VariableParameters { get; set; }

        public override bool Equals(object obj)
        {
            return Equals(obj as Canal);
        }

        public bool Equals(Canal other)
        {
            return other != null &&
                   CanalId == other.CanalId;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(CanalId);
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
