#nullable disable

namespace RPK.Model
{
    public partial class VariableParameter
    {
        public long MaterialId { get; set; }
        public long CanalId { get; set; }
        public long ParameterId { get; set; }
        public double ValueUpperBound { get; set; }
        public double ValueLowerBound { get; set; }

        public virtual Canal Canal { get; set; }
        public virtual Material Material { get; set; }
        public virtual Parameter Parameter { get; set; }
    }
}
