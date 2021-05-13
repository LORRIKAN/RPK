using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace RPK.Model.MathModel
{
    [Display(Name = "Параметры")]
    public partial class Parameter : BaseModel, IEquatable<Parameter>
    {
        public Parameter()
        {
            CanalGeometryParameters = new HashSet<CanalGeometryParameter>();
            EmpiricalCoefficientOfMathModels = new HashSet<EmpiricalCoefficientOfMathModel>();
            ParameterOfMaterialProperties = new HashSet<ParameterOfMaterialProperty>();
            VariableParameters = new HashSet<VariableParameter>();
        }

        [Display(Name = "Идентификатор параметра")]
        [ReadOnly(true)]
        public long ParameterId { get; set; }

        [Display(Name = "Наименование параметра")]
        [Required]
        public string Name { get; set; }

        [Display(Name = "Обозначение параметра")]
        [Required]
        public string Designation { get; set; }

        [Display(Name = "Единица измерения")]
        public string MeasureUnit { get; set; }

        [Browsable(false)]
        public virtual MeasureUnit MeasureUnitNavigation { get; set; }

        [Browsable(false)]
        public virtual ICollection<CanalGeometryParameter> CanalGeometryParameters { get; set; }

        [Browsable(false)]
        public virtual ICollection<EmpiricalCoefficientOfMathModel> EmpiricalCoefficientOfMathModels { get; set; }

        [Browsable(false)]
        public virtual ICollection<ParameterOfMaterialProperty> ParameterOfMaterialProperties { get; set; }

        [Browsable(false)]
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
