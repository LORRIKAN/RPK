using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace RPK.Model.MathModel
{
    [Display(Name = "Материалы")]
    public partial class Material : BaseModel, IEquatable<Material>
    {
        public Material()
        {
            EmpiricalCoefficientOfMathModels = new HashSet<EmpiricalCoefficientOfMathModel>();
            ParameterOfMaterialProperties = new HashSet<ParameterOfMaterialProperty>();
            VariableParameters = new HashSet<VariableParameter>();
        }

        [Column("MaterialId")]
        [Display(Name = "Идентификатор материала")]
        [ReadOnly(true)]
        public long Id { get; set; }

        [Display(Name = "Наименование материала")]
        [Required]
        public string Name { get; set; }

        [Browsable(false)]
        public virtual ICollection<EmpiricalCoefficientOfMathModel> EmpiricalCoefficientOfMathModels { get; set; }

        [Browsable(false)]
        public virtual ICollection<ParameterOfMaterialProperty> ParameterOfMaterialProperties { get; set; }

        [Browsable(false)]
        public virtual ICollection<VariableParameter> VariableParameters { get; set; }

        public override Range UnchangeableRows => new(0, 0);

        public override bool Equals(object obj)
        {
            return Equals(obj as Material);
        }

        public bool Equals(Material other)
        {
            return other != null &&
                   Id == other.Id;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id);
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
