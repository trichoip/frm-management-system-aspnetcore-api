using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Interfaces
{
    public interface IAssignmentSchemaRepository
    {
        AssignmentSchema GetAssignmentSchema(long syllabusId);
        void UpdateAssignmentSchema(AssignmentSchema assignmentSchema);
    }
}
