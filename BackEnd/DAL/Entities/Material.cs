using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Material
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string HyperLink { get; set; }
        public long IdLesson { get; set; }
        public int? Status { get; set; }
        public virtual Lesson Lesson { get; set; }
        public virtual IEnumerable<HistoryMaterial> HistoryMaterials { get; set; }

    }
}