using BAL.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Comparer
{
    public class LessonComparer : IEqualityComparer<LessonViewModel>
    {
        public bool Equals(LessonViewModel? x, LessonViewModel? y)
        {
            return x.Id == y.Id;
        }

        public int GetHashCode([DisallowNull] LessonViewModel obj)
        {
            int hCode = obj.Id.GetHashCode();
            return hCode.GetHashCode();
        }
    }
}
