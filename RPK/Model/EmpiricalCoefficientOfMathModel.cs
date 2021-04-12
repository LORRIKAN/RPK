#nullable disable

namespace RPK.Model
{
    public partial class EmpiricalCoefficientOfMathModel
    {
        public long ParameterId { get; set; }
        public long MaterialId { get; set; }
        public double ParameterValue { get; set; }

        public virtual Material Material { get; set; }
        public virtual Parameter Parameter { get; set; }
    }
}
