using BAL.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Comparer
{
    public class UnitComparer : IEqualityComparer<UnitViewModel>
    {
        public bool Equals(UnitViewModel? x, UnitViewModel? y)
        {
            return x.Id == y.Id;
        }

        public int GetHashCode([DisallowNull] UnitViewModel obj)
        {
            int hCode = obj.Id.GetHashCode();
            return hCode.GetHashCode();
        }
    }
}
