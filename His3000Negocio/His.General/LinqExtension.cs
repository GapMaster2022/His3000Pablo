using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Dynamic;  

namespace His.General
{
    public static class LinqExtension
    {
        // ordena asendentemente de forma dinamica un IEnumerable<T> usando una determinada propiedad
        public static IEnumerable<T> OrdenarPorPropiedad<T>(this IEnumerable<T> list, string property)
        {
            return list.AsQueryable().OrderBy(property);
        }

        // ordena el IEnumerable<T>
        public static void SortByProperty<T>(this IEnumerable<T> list, string property)
        {
            list = list.AsQueryable().OrderBy(property);
        }
    }
}
