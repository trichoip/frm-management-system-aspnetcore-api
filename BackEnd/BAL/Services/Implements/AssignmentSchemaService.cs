using BAL.Services.Interfaces;
using DAL.Entities;
using DAL.Infrastructure;
using DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Services.Implements
{
    public class AssignmentSchemaService: IAssignmentSchemaService
    {
        private IAssignmentSchemaRepository _assignmentSchemaRepository;
        private IUnitOfWork _unitOfWork;

        public AssignmentSchemaService(IAssignmentSchemaRepository assignmentSchemaRepository, IUnitOfWork unitOfWork)
        {
            _assignmentSchemaRepository = assignmentSchemaRepository;
            _unitOfWork = unitOfWork;
        }
        public AssignmentSchema GetAssignmentSchema(long id)
        {
            return _assignmentSchemaRepository.GetAssignmentSchema(id);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void SaveAsync()
        {
            _unitOfWork.commitAsync();
        }
    }
}
