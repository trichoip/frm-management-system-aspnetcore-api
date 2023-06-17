using AutoMapper;
using BAL.Models;
using BAL.Services.Interfaces;
using DAL.Entities;
using DAL.Infrastructure;
using DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BAL.Services.Implements
{
    public class ClassTraineeService: IClassTraineeService
    {
        private IClassTraineeRepository _classTraineeRepository;
        private IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;

        public ClassTraineeService(IClassTraineeRepository classTraineeRepository, IUnitOfWork unitOfWork, IMapper mapper, IUserRepository userRepository)
        {
            _classTraineeRepository = classTraineeRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task<List<Class>> GetClassesById(long traineeId)
        {
            return (await _classTraineeRepository.GetClassTraineesQuery(traineeId)
                                                .Include(ct => ct.Class).ThenInclude(c => c.AttendeeType)
                                                .Include(ct => ct.Class).ThenInclude(c => c.ClassStatus)
                                                .Include(ct => ct.Class).ThenInclude(c => c.Locations).ThenInclude(l => l.Location)
                                                .ToListAsync()).Select(cm => cm.Class).ToList(); ;
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void SaveAsync()
        {
            _unitOfWork.commitAsync();
        }

        public ClassTrainee GetById(long id)
        {
            return _classTraineeRepository.GetById(id);
        }
        #region Group 5 GetStudentClasses
        public async Task<List<ClassTraineeViewModel>> GetTraineeClasses(long traineeId)
        {
            var existedUser = await _userRepository.GetUserAsync(traineeId);
            if (existedUser != null)
            {
                if (existedUser.IdRole != 4)
                {
                    throw new Exception("The User's ID is not suitable trainee role.");
                }
                var traineeClassesQuery = await _classTraineeRepository.GetClassTraineesQuery(traineeId)
                                                                    .Include(x => x.User).Include(x => x.Class).ToListAsync();
                List<ClassTraineeViewModel> traineeClasses = _mapper.Map<List<ClassTraineeViewModel>>(traineeClassesQuery);
                return traineeClasses.Count == 0 ? null : traineeClasses;
            }
            throw new Exception("The User's ID does not exist in the system.");
        }
        #endregion
    }
}
