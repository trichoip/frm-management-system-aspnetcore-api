using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BAL.Models
{
  public class ClassFUSViewModel
  {
    public long Id { get; set; }
    public string Name { get; set; }
    public IEnumerable<ClassFSUContactViewModel> ClassFSUContact { get; set; }
  }
}