using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Lesson
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public int Duration { get; set; }
        public long IdDeliveryType { get; set; }
        public virtual DeliveryType DeliveryType { get; set; }
        public long IdFormatType { get; set; }
        public virtual FormatType FormatType { get; set; }
        public long IdOutputStandard { get; set; }
        public virtual OutputStandard OutputStandard { get; set; }
        public int? Status { get; set; }
        public long IdUnit { get; set; }
        public virtual Unit Unit { get; set; }

        public virtual IEnumerable<Material> Materials { get; set; }

    }
}