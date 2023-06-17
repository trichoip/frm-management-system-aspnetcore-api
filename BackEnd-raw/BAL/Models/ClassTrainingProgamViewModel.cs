using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BAL.Models
{
  public class ClassTrainingProgamViewModel
  {
    public long? Id { get; set; }
    public int Duration { get; set; }
    public string? ModifiedBy { get; set; }
    public string? Name { get; set; }
    public DateTime? ModifiedOn { get; set; }
    public string? Status { get; set; }
    public virtual List<ClassSyllabusViewModel> Sysllabus { get; set; }

  }
}

