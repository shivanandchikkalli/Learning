using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OCP
{
    class FilterItems<T> : IFilter<T>
    {
        public IEnumerable<T> Filter(IEnumerable<T> items, ISpecification<T> spec)
        {
            foreach (var item in items)
                if (spec.IsSatified(item))
                    yield return item;
        }
    }
}
