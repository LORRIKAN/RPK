#nullable disable

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace RPK.Model.MathModel
{
    [Display(Name = "Геометрические параметры каналов")]
    public partial class CanalGeometryParameter : ParameterTypeBase, IEquatable<CanalGeometryParameter>
    {
        [Display(Name = "Идентификатор канала")]
        [Required]
        public long CanalId { get; set; }

        [Display(Name = "Идентификатор параметра")]
        [Required]
        public override long ParameterId { get; set; }

        [Display(Name = "Значение параметра")]
        [Required]
        public double ParameterValue { get; set; }

        [Browsable(false)]
        public virtual Canal Canal { get; set; }

        [Browsable(false)]
        public virtual Parameter Parameter { get; set; }

        public override Range UnchangeableRows => new(0, 2);

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