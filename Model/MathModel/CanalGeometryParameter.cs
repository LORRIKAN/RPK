#nullable disable

using RPK;

namespace RPK.Model.MathModel
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
