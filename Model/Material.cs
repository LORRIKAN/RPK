using System.Collections.Generic;

#nullable disable

namespace RPK.Model
{
    public partial class Material
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

        public override string ToString()
        {
            return Name;
        }
    }
}
