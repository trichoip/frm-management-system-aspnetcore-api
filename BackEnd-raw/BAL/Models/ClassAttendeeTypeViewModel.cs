using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BAL.Models
{
  public class ClassAttendeeTypeViewModel
  {
    public long Id { get; set; }
    public string Name { get; set; }
    public virtual IEnumerable<ClassViewModel> Classes { get; set; }
  }
}