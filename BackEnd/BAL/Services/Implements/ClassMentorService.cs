using AutoMapper;
using BAL.Models;
using BAL.Services.Interfaces;
using DAL.Entities;
using DAL.Infrastructure;
using DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BAL.Services.Implements
{
    public class ClassMentorService: IClassMentorService
    {
        private readonly IClassMentorRepository _classMentorRepository;
        private readonly IUserService _userService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ClassMentorService(IClassMentorRepository classMentorRepository, IUnitOfWork unitOfWork, IMapper mapper, IUserService userService)
        {
            _classMentorRepository = classMentorRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userService = userService;
        }

        public async Task<List<Class>> GetClassesById(long mentorId)
        {
            return (await _classMentorRepository.GetMentorClassesQuery(mentorId)
                                                .Include(cm => cm.Class).ThenInclude(c => c.AttendeeType)
                                                .Include(cm => cm.Class).ThenInclude(c => c.ClassStatus)
                                                .Include(cm => cm.Class).ThenInclude(c => c.Locations).ThenInclude(l => l.Location)
                                                .ToListAsync()).Select(cm => cm.Class).ToList();
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void SaveAsync()
        {
            _unitOfWork.commitAsync();
        }

        public ClassMentor GetById(long id)
        {
            return _classMentorRepository.GetById(id);
        }
        #region Group 5 - GetMentorClasses
        public async Task<List<ClassMentorViewModel>> GetMentorClasses(long mentorId)
        {
            //1. check mentorId có tồn tại hay không
            
            var existedUser = _userService.GetByID(mentorId);
            if(existedUser != null)
            {
                if (existedUser.IdRole != 3)
                {
                    throw new Exception("The User's ID is not suitable trainer role.");
                }
                var mentorClassesQuery = _classMentorRepository.GetMentorClassesQuery(mentorId)
                                                           .Include(x => x.User)
                                                           .Include(x => x.Class);

                List<ClassMentor> classMentors = await mentorClassesQuery.ToListAsync();
                return classMentors.Count == 0 ?  null : _mapper.Map<List<ClassMentorViewModel>>(classMentors);
            }
            throw new Exception("The User's ID does not exist in the system.");
        }
        #endregion
    }
}
