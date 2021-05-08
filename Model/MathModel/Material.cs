using System;
using System.Collections.Generic;

#nullable disable

namespace RPK.Model.MathModel
{
    public partial class Material : IEquatable<Material>
    {
        public Material()
        {
            EmpiricalCoefficientOfMathModels = new HashSet<EmpiricalCoefficientOfMathModel>();
            ParameterOfMaterialProperties = new HashSet<ParameterOfMaterialProperty>();
            VariableParameters = new HashSet<VariableParameter>();
        }

        public long MaterialId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<EmpiricalCoefficientOfMathModel> EmpiricalCoefficientOfMathModels { get; set; }
        public virtual ICollection<ParameterOfMaterialProperty> ParameterOfMaterialProperties { get; set; }
        public virtual ICollection<VariableParameter> VariableParameters { get; set; }

        public override bool Equals(object obj)
        {
            return Equals(obj as Material);
        }

        public bool Equals(Material other)
        {
            return other != null &&
                   MaterialId == other.MaterialId;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(MaterialId);
        }

        public override string ToString()
        {
            return Name;
        }

        public static bool operator ==(Material left, Material right)
        {
            return EqualityComparer<Material>.Default.Equals(left, right);
        }

        public static bool operator !=(Material left, Material right)
        {
            return !(left == right);
        }
    }
}
