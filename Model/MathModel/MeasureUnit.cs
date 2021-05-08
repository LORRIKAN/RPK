using System;
using System.Collections.Generic;

#nullable disable

namespace RPK.Model.MathModel
{
    public partial class MeasureUnit : IEquatable<MeasureUnit>
    {
        public MeasureUnit()
        {
            Parameters = new HashSet<Parameter>();
        }

        public string Value { get; set; }

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
