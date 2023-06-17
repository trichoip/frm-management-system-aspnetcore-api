using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Models
{
    public class LessonViewModel
    {
        public long? Id { get; set; }
        public string? Name { get; set; }
        public int? Duration { get; set; }
        public long? IdDeliveryType { get; set; }
        public string? DeliveryType { get; set; }
        public long? IdFormatType { get; set; }
        public string? FormatType { get; set; }
        public long? IdOutputStandard { get; set; }
        public string? OutputStandard { get; set; }
        public int? Status { get; set; }
        public List<MaterialViewModel>? Materials { get; set; }
    }

}
