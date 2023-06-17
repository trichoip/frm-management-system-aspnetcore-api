using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Models
{
    public class RoleViewModel
    {
        public string Name { get; set; }  
        public string SyllabusPermission { get; set; }
        public string TrainingProgramPermission { get; set; }
        public string ClassPermission { get; set; }
        public string LearningMaterialPermission { get; set; }
    }
}
