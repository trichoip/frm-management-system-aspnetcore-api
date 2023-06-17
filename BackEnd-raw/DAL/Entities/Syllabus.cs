using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Syllabus
    {
        public long Id { get; set; }
        public virtual AssignmentSchema AssignmentSchema { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public int AttendeeNumber { get; set; }
        public float Version { get; set; }
        public string Technicalrequirement { get; set; }
        public string CourseObjectives { get; set; }
        public int Status { get; set; }
        public string TrainingPrinciple { get; set; }
        public string Description { get; set; }
        public string HyperLink { get; set; }
        public long IdLevel { get; set; }
        public virtual Level Level { get; set; }

        public class UpLoadExcelFileRequest
        {
            public IFormFile File { get; set; }
        }
        public class UpLoadExcelFileResponse
        {
            public bool IsSuccess { get; set; }
            public string Message { get; set; }
        }



        public virtual IEnumerable<SyllabusTrainer> SyllabusTrainers { get; set; }

        public virtual IEnumerable<HistorySyllabus> HistorySyllabi { get; set; }

        public virtual IEnumerable<Session> Sessions { get; set; }

        public virtual IEnumerable<Curriculum> Curricula { get; set; }
    }
}