using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Models
{
    public class SyllabusViewModel
    {
        public long? Id { get; set; }
        public string? Name { get; set; }
        public string? Code { get; set; }
        public int? AttendeeNumber { get; set; }
        public float? Version { get; set; }
        public string? Technicalrequirement { get; set; }
        public string? CourseObjectives { get; set; }
        public int? Status { get; set; }
        public string? TrainingPrinciple { get; set; }
        public string? Description { get; set; }
        public string? HyperLink { get; set; }
        public long? IdLevel { get; set; }
        public string? LevelName { get; set; }
        // team 01
        // AssignmentSchema
        public AssignmentSchemaViewModel? AssignmentSchema { get; set; }
        // Lists
        public List<SessionViewModel>? Sessions { get; set; }
        // team 01

        // Team6
        public HistorySyllabusViewModel? HistorySyllabus { get; set; }
        // Team6
    }

}
