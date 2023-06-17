using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BAL.Models
{
  public class ClassLessonViewModel
  {
    public long Id { get; set; }
    public string Name { get; set; }
    public int Duration { get; set; }

    public string OutputStandard { get; set; }
    public string DeliveryType { get; set; }
    public string FormatType { get; set; }
  }
}