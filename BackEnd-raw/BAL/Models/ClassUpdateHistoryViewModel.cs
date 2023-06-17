using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BAL.Models
{
  public class ClassUpdateHistoryViewModel
  {
    public long IdClass { get; set; }
    public virtual ClassViewModel Class { get; set; }
    public long ModifyBy { get; set; }
    public virtual UserViewModel User { get; set; }
    public DateTime UpdateDate { get; set; }
  }
}