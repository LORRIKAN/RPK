using System;
using System.ComponentModel;
using System.Linq;

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
