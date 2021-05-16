using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class RowIndexIsInvalid : ValidationResult
    {
        public RowIndexIsInvalid(string errorMessage) : base(errorMessage)
        {
        }

        public RowIndexIsInvalid(string errorMessage, IEnumerable<string> memberNames) : base(errorMessage, memberNames)
        {
        }

        protected RowIndexIsInvalid(ValidationResult validationResult) : base(validationResult)
        {
        }
    }
}
