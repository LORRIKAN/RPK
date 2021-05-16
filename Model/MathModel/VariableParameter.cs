using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RPK.Model.MathModel
{
    [Display(Name = "Режимные параметры")]
    public partial class VariableParameter : ParameterTypeBase, IEquatable<VariableParameter>
    {
        [Display(Name = "Идентификатор материала")]
        [Required]
        public long MaterialId { get; set; }

        [Display(Name = "Идентификатор канала")]
        [Required]
        public long CanalId { get; set; }

        [Display(Name = "Идентификатор параметра")]
        [Required]
        public override long ParameterId { get; set; }

        [Display(Name = "Верхняя регламентная граница")]
        [Required]
        public double ValueUpperBound { get; set; }
        
        [Display(Name = "Нижняя регламентная граница")]
        [Required]
        public double ValueLowerBound { get; set; }

        [Browsable(false)]
        public virtual Canal Canal { get; set; }

        [Browsable(false)]
        public virtual Material Material { get; set; }

        [Browsable(false)]
        public virtual Parameter Parameter { get; set; }

        public override Range UnchangeableRows => new(0, 1);

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
