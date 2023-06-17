using System;
using System.Linq;
using System.Text;
using DAL.Entities;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace BAL.Models
{
    public class TrainingProgramViewModel
    {
        public long Id { get; set; }
        /// <summary>
        /// Traning program name
        /// </summary>
        public string Name { get; set; }
        public int Status { get; set; }
        /// <summary>
        /// Name user create
        /// </summary>
        public string? Createby { get; set; }
        /// <summary>
        /// Time create (history training program)
        /// </summary>
        public DateTime? Createon { get; set; }
        /// <summary>
        /// endDate - startDate
        /// </summary>
        public int? Duration  { get; set; }

        

    }
}
