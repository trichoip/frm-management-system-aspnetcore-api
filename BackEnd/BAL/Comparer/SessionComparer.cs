using BAL.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Comparer
{
    public class SessionComparer : IEqualityComparer<SessionViewModel>
    {
        public bool Equals(SessionViewModel? x, SessionViewModel? y)
        {
            return (x.Id == y.Id);
        }

        public int GetHashCode([DisallowNull] SessionViewModel obj)
        {
            int hCode = obj.Id.GetHashCode();
            return hCode.GetHashCode();
        }
    }
}
