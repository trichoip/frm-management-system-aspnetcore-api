using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Entities
{
  public class ClassSelectedDate
  {
    public long Id { get; set; }
    public long IdClass { get; set; }
    public virtual Class Class { get; set; }
    public DateTime ActiveDate { get; set; }
    public int Status { get; set; }
  }
}