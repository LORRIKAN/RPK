using System.Collections.Generic;

#nullable disable

namespace RPK.Model.MathModel
{
    public partial class Parameter
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
    }
}
