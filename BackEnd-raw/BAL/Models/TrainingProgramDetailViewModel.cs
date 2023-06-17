using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Models
{
    public class TrainingProgramDetailViewModel
    {
        // Team6
        public long Id { get; set; }
        /// <summary>
        /// Traning program name
        /// </summary>
        public string Name { get; set; }
        public string Status { get; set; }
        /// <summary>
        /// Name user create
        /// </summary>
        public string Createby { get; set; }
        /// <summary>
        /// Time create (history training program)
        /// </summary>
        public DateTime Createon { get; set; }
        /// <summary>
        /// endDate - startDate
        /// </summary>
        public int Duration { get; set; }

   
        public int totalSession { get; set; }

        public List<SearchSyllabusViewModel> syllabuses { get; set; }
        // Team6

    }
}
