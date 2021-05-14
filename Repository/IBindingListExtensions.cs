using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public static class IBindingListExtensions
    {
        public static Type GetDataType(this IBindingList data)
        {
            return data
                    .GetType().GetProperties().First(prop => prop.GetIndexParameters().Length > 0).PropertyType;
        }
    }
}
