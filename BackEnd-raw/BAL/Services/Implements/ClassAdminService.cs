using AutoMapper;
using BAL.Services.Interfaces;
using DAL.Entities;
using DAL.Infrastructure;
using DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BAL.Services.Implements
{
    public class ClassAdminService: IClassAdminService
    {
        private readonly IClassAdminReporitory _classAdminReporitory;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ClassAdminService(IClassAdminReporitory classAdminReporitory, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _classAdminReporitory = classAdminReporitory;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<Class>> GetClassesById(long adminId)
        {
            return (await _classAdminReporitory.GetClassAdminsById(adminId)
                                                .Include(ca => ca.Class).ThenInclude(c => c.AttendeeType)
                                                .Include(ca => ca.Class).ThenInclude(c => c.ClassStatus)
                                                .Include(ca => ca.Class).ThenInclude(c => c.Locations).ThenInclude(l => l.Location)
                                                .ToListAsync()).Select(ca => ca.Class).ToList();
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void SaveAsync()
        {
            _unitOfWork.commitAsync();
        }

        public ClassAdmin GetById(long id)
        {
            return _classAdminReporitory.GetById(id);
        }
    }
}
