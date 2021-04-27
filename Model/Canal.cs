using System.Collections.Generic;

#nullable disable

namespace RPK.Model
{
    public partial class Canal
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

        public override string ToString()
        {
            return Brand;
        }
    }
}
