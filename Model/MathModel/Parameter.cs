using System;
using System.Collections.Generic;

#nullable disable

namespace RPK.Model.MathModel
{
    public partial class Parameter : IEquatable<Parameter>
    {
        public Parameter()
        {
            CanalGeometryParameters = new HashSet<CanalGeometryParameter>();
            EmpiricalCoefficientOfMathModels = new HashSet<EmpiricalCoefficientOfMathModel>();
            ParameterOfMaterialProperties = new HashSet<ParameterOfMaterialProperty>();
            VariableParameters = new HashSet<VariableParameter>();
        }

        public long ParameterId { get; set; }
        public string Name { get; set; }
        public string Designation { get; set; }
        public string MeasureUnit { get; set; }

        public virtual MeasureUnit MeasureUnitNavigation { get; set; }
        public virtual ICollection<CanalGeometryParameter> CanalGeometryParameters { get; set; }
        public virtual ICollection<EmpiricalCoefficientOfMathModel> EmpiricalCoefficientOfMathModels { get; set; }
        public virtual ICollection<ParameterOfMaterialProperty> ParameterOfMaterialProperties { get; set; }
        public virtual ICollection<VariableParameter> VariableParameters { get; set; }

        public override bool Equals(object obj)
        {
            return Equals(obj as Parameter);
        }

        public bool Equals(Parameter other)
        {
            return other != null &&
                   ParameterId == other.ParameterId;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(ParameterId);
        }

        public static bool operator ==(Parameter left, Parameter right)
        {
            return EqualityComparer<Parameter>.Default.Equals(left, right);
        }

        public static bool operator !=(Parameter left, Parameter right)
        {
            return !(left == right);
        }
    }
}
