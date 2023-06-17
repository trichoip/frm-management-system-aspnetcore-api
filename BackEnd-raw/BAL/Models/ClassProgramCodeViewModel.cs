using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BAL.Models
{
  public class ClassProgramCodeViewModel
  {
    public long Id { get; set; }
    public string ProgramCode { get; set; }
    public virtual IEnumerable<ClassViewModel> Classes { get; set; }
  }
}