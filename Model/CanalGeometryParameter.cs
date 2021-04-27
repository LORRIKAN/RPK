#nullable disable

namespace RPK.Model
{
    public partial class CanalGeometryParameter
    {
        public long CanalId { get; set; }
        public long ParameterId { get; set; }
        public double ParameterValue { get; set; }

        public virtual Canal Canal { get; set; }
        public virtual Parameter Parameter { get; set; }
    }
}
