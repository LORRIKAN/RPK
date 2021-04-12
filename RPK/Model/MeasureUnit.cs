using System.Collections.Generic;

#nullable disable

namespace RPK.Model
{
    public partial class MeasureUnit
    {
        public MeasureUnit()
        {
            Parameters = new HashSet<Parameter>();
        }

        public string Value { get; set; }

        public virtual ICollection<Parameter> Parameters { get; set; }
    }
}
