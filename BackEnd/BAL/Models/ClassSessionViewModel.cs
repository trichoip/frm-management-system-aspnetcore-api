using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BAL.Models
{
  public class ClassSessionViewModel
  {
    public long Id { get; set; }
    public string Name { get; set; }
    public IEnumerable<ClassUnitViewModel>? Unit { get; set; }
  }
}