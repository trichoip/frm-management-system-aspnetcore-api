using BAL.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Comparer
{
    public class MaterialComparer : IEqualityComparer<MaterialViewModel>
    {
        public bool Equals(MaterialViewModel? x, MaterialViewModel? y)
        {
            return x.Id == y.Id;
        }

        public int GetHashCode([DisallowNull] MaterialViewModel obj)
        {
            int hCode = obj.Id.GetHashCode();
            return hCode.GetHashCode();
        }
    }
}
