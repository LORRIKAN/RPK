using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPK.Model
{
    public abstract class ParameterTypeBase : BaseModel
    {
        public abstract long ParameterId { get; set; }
    }
}