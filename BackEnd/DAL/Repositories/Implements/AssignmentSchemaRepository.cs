using DAL.Entities;
using DAL.Infrastructure;
using DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Implements
{
    public class AssignmentSchemaRepository : RepositoryBase<AssignmentSchema>, IAssignmentSchemaRepository
    {
        private readonly FRMDbContext _dbContext;
        public AssignmentSchemaRepository(IDbFactory dbFactory) : base(dbFactory)
        {
            _dbContext = dbFactory.Init();
        }

        public AssignmentSchema GetAssignmentSchema(long syllabusId)
        {
            var result = _dbSet.Where(x => x.IDSyllabus == syllabusId).FirstOrDefault();
            return result;
        }
        public void UpdateAssignmentSchema(AssignmentSchema assignmentSchema)
        {
            var result = _dbSet.FirstOrDefault(x => x.IDSyllabus == assignmentSchema.IDSyllabus);
            if (result != null)
            {
                _dbContext.Entry<AssignmentSchema>(result).State = EntityState.Detached;
                _dbSet.Update(assignmentSchema);
            }
        }

    }
}
