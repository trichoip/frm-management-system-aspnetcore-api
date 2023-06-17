using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Models
{
    public class SearchSyllabusViewModel
    {
        public long Id { get; set; }
        public string? SyllabusName { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public int? Duration { get; set; }
        public string Code { get; set; }
        public int TotalSession { get; set; }
        public string Status { get; set; }
        public float? Version { get; set; }
        public string? CreateBy { get; set; }

    // Team6
    public List<SessionViewModel>? Sessions { get; set; }
    // Team6
  }
}
