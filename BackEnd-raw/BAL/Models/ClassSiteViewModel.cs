using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BAL.Models
{
  public class ClassSiteViewModel
  {
    public long? Id { get; set; }
    public string? Site { get; set; }
    public virtual IEnumerable<ClassViewModel> Classes { get; set; }

  }
}