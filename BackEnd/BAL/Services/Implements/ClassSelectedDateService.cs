using AutoMapper;
using BAL.Models;
using BAL.Services.Interfaces;
using Castle.Core.Internal;
using DAL.Entities;
using DAL.Infrastructure;
using DAL.Repositories.Interfaces;

namespace BAL.Services.Implements
{
    public class ClassSelectedDateService : IClassSelectedDateService
    {
        private readonly IClassSelectedDateRepository _classSelectedDateRepository;
        private readonly IClassRepository _classRepository;
        private readonly IClassMentorRepository _classMentorRepository;
        private readonly IClassLocationRepository _classLocationRepository;
        private readonly ILocationRepository _locationRepository;
        private readonly IClassStatusRepository _classStatusRepository;
        private readonly IAttendeeTypeRepository _attendeeTypeRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private List<ClassCalenderViewModel> listClassCalenderViewModels = new();
        private IEnumerable<Class> listClass;
        private Class @class;
        private ClassLocation classLocation;
        private ClassStatus classStatus;
        private Location location;
        private AttendeeType attendeeType;
        private IUserRepository _userRepository;

        public ClassSelectedDateService
            (
            IClassSelectedDateRepository classSelectedDateRepository,
            IClassRepository classRepository,
            IClassMentorRepository classMentorRepository,
            IClassLocationRepository classLocationRepository,
            ILocationRepository locationRepository,
            IClassStatusRepository classStatusRepository,
            IAttendeeTypeRepository attendeeTypeRepository,
            IUnitOfWork unitOfWork, IMapper mapper,
            IUserRepository userRepository
            )
        {
            _classSelectedDateRepository = classSelectedDateRepository;
            _classRepository = classRepository;
            _classMentorRepository = classMentorRepository;
            _classLocationRepository = classLocationRepository;
            _locationRepository = locationRepository;
            _classStatusRepository = classStatusRepository;
            _attendeeTypeRepository = attendeeTypeRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userRepository = userRepository;
        }

        #region bhhiep
        public async Task<List<ClassCalenderViewModel>> GetCalendarsByWeek(List<Class> classes, string date, TrainingCalendarViewModel? trainingCalendarFilter)
        {
            if (classes.Count > 0)
            {
                if (trainingCalendarFilter != null)
                {
                    if (trainingCalendarFilter.KeyWord != null && classes.Count > 0)
                    {
                        classes = classes.Where(c => c.ClassCode.Contains(trainingCalendarFilter.KeyWord)).ToList();
                    }
                    if (trainingCalendarFilter.Locations != null && classes.Count > 0)
                    {
                        classes = classes.Where(c => c.Locations.Any(cl => trainingCalendarFilter.Locations.Any(tl => tl.Equals(cl.Location.Name)))).ToList();
                    }
                    if (trainingCalendarFilter.TimeClasses != null && classes.Count > 0)
                    {
                        List<Class> classesTemp = new List<Class>();
                        if (trainingCalendarFilter.TimeClasses.Any(tc => tc.Equals("Morning")))
                        {
                            classesTemp.AddRange(classes.Where(c => c.StartTimeLearning <= TimeSpan.Parse("12:00:00")).ToList());
                        }
                        if (trainingCalendarFilter.TimeClasses.Any(tc => tc.Equals("Noon")))
                        {
                            classesTemp.AddRange(classes.Where(c => c.StartTimeLearning >= TimeSpan.Parse("13:00:00")
                                                                 && c.StartTimeLearning <= TimeSpan.Parse("17:00:00")).ToList());
                        }
                        if (trainingCalendarFilter.TimeClasses.Any(tc => tc.Equals("Night")))
                        {
                            classesTemp.AddRange(classes.Where(c => c.StartTimeLearning >= TimeSpan.Parse("18:00:00")).ToList());
                        }
                        classes = classesTemp;
                    }
                    if (trainingCalendarFilter.Statuses != null && classes.Count > 0)
                    {
                        classes = classes.Where(c => trainingCalendarFilter.Statuses.Any(s => c.ClassStatus.Name.Equals(s))).ToList();
                    }
                    if (trainingCalendarFilter.Attendees != null && classes.Count > 0)
                    {
                        classes = classes.Where(c => trainingCalendarFilter.Attendees.Any(a => c.AttendeeType.Name.Equals(a))).ToList();
                    }
                    if (trainingCalendarFilter.IdFSU != null && classes.Count > 0)
                    {
                        classes = classes.Where(c => c.IdFSU == trainingCalendarFilter.IdFSU).ToList();
                    }
                    if (trainingCalendarFilter.IdTrainer != null && classes.Count > 0)
                    {
                        classes = classes.Where(c => c.ClassMentors.Any(cm => cm.IdUser == trainingCalendarFilter.IdTrainer)).ToList();
                    }
                }

                if (classes.Any())
                {
                    //Get the week of the selected date
                    DateTime parseDate = DateTime.Parse(date);
                    int currentDayofWeek = (int)parseDate.DayOfWeek;
                    IEnumerable<DateTime> DaysOfWeek = Enumerable.Range(-currentDayofWeek, 7).Select(days => parseDate.AddDays(days));
                    //Get the week of the selected date

                    var filteredClasses = (await _classSelectedDateRepository.GetSelectedDatesQueryAsync())
                        .Where(csd => classes.Select(c => c.Id).Any(oci => oci == csd.IdClass) && DaysOfWeek.Any(ad => ad.Equals(csd.ActiveDate)))
                        .OrderBy(csd => csd.ActiveDate)
                        .ToList();

                    if (trainingCalendarFilter != null)
                    {
                        if (trainingCalendarFilter.StartTime != null && filteredClasses.Count > 0)
                        {
                            filteredClasses = filteredClasses.Where(csd => csd.ActiveDate.Date.CompareTo(DateTime.Parse(trainingCalendarFilter.StartTime)) >= 0).ToList();
                        }
                        if (trainingCalendarFilter.EndTime != null && filteredClasses.Count > 0)
                        {
                            filteredClasses = filteredClasses.Where(csd => csd.ActiveDate.Date.CompareTo(DateTime.Parse(trainingCalendarFilter.EndTime)) <= 0).ToList();
                        }
                    }

                    List<ClassCalenderViewModel> classCalenderViewModels = _mapper.Map<List<ClassCalenderViewModel>>(filteredClasses);
                    MapSelectedDateClass(filteredClasses, classCalenderViewModels);
                    return classCalenderViewModels;
                }
                else throw new Exception("There is no class matched with the conditions of the filter");
            }
            else throw new Exception("There is no class currently");
        }
        #endregion

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void SaveAsync()
        {
            _unitOfWork.commitAsync();
        }

        #region Group 5 - GetCalendarsByDate
        public async Task<List<ClassCalenderViewModel>> GetClassCalendarsFilter(long userID, TrainingCalendarViewModel? trainingCalendarFilter)
        {
            User user = await _userRepository.GetUserAsync(userID);
            List<ClassCalenderViewModel> classCalenders = null;
            List<ClassSelectedDate> classes = await _classSelectedDateRepository.GetSelectedDatesQueryAsync();
            if (trainingCalendarFilter != null)
            {
                classes = await GetClassCalendarsByFilter(userID, trainingCalendarFilter, classes);
            }
            switch (user.Role.Name)
            {
                case "Super Admin":
                    {
                        classCalenders = _mapper.Map<List<ClassCalenderViewModel>>(classes);
                        MapSelectedDateClass(classes, classCalenders);
                        break;
                    }
                case "Class Admin":
                    {
                        List<ClassSelectedDate> cls = new List<ClassSelectedDate>();
                        foreach (var item in classes)
                        {
                            foreach (var ca in item.Class.ClassAdmins)
                            {
                                if (ca.IdUser == userID)
                                {
                                    cls.Add(item);
                                }
                            }
                        }
                        classCalenders = _mapper.Map<List<ClassCalenderViewModel>>(cls);
                        MapSelectedDateClass(cls, classCalenders);
                        break;
                    }
                case "Trainer":
                    {
                        List<ClassSelectedDate> cls = new List<ClassSelectedDate>();
                        foreach (var item in classes)
                        {
                            foreach (var ca in item.Class.ClassMentors)
                            {
                                if (ca.IdUser == userID)
                                {
                                    cls.Add(item);
                                }
                            }
                        }
                        classCalenders = _mapper.Map<List<ClassCalenderViewModel>>(cls);
                        MapSelectedDateClass(cls, classCalenders);
                        break;
                    }
                case "Student":
                    {
                        List<ClassSelectedDate> cls = new List<ClassSelectedDate>();
                        foreach (var item in classes)
                        {
                            foreach (var ca in item.Class.ClassTrainees)
                            {
                                if (ca.IdUser == userID)
                                {
                                    cls.Add(item);
                                }
                            }
                        }
                        classCalenders = _mapper.Map<List<ClassCalenderViewModel>>(cls);
                        MapSelectedDateClass(cls, classCalenders);
                        break;
                    }
            }
            return classCalenders;
        }
        private async Task<List<ClassSelectedDate>> GetClassCalendarsByFilter(long userID, TrainingCalendarViewModel? trainingCalendarFilter, List<ClassSelectedDate> classes)
        {
            DateTime? startTime = null;
            DateTime? endTime = null;
            if (trainingCalendarFilter.StartTime != null)
            {
                startTime = DateTime.Parse(trainingCalendarFilter.StartTime);
                startTime = DateTime.Parse(startTime?.ToString("yyyy-MM-dd"));
            }
            if (trainingCalendarFilter.EndTime != null)
            {
                endTime = DateTime.Parse(trainingCalendarFilter.EndTime);
                endTime = DateTime.Parse(endTime?.ToString("yyyy-MM-dd"));
            }
            if (trainingCalendarFilter.StartTime != null && trainingCalendarFilter.EndTime != null)
            {
                classes = await _classSelectedDateRepository.GetSelectedDatesQueryAsync();
                classes = classes.Where(x => x.ActiveDate.Date.CompareTo(startTime) >= 0 && x.ActiveDate.Date.CompareTo(endTime) <= 0).ToList();
            }
            else if (trainingCalendarFilter.EndTime != null && trainingCalendarFilter.StartTime == null)
            {
                classes = await _classSelectedDateRepository.GetSelectedDatesQueryAsync();
                classes = classes.Where(x => x.ActiveDate.Date.CompareTo(endTime) <= 0).ToList();
            }
            else if (trainingCalendarFilter.EndTime == null && trainingCalendarFilter.StartTime != null)
            {
                classes = await _classSelectedDateRepository.GetSelectedDatesQueryAsync();
                classes = classes.Where(x => x.ActiveDate.Date.CompareTo(startTime) >= 0).ToList();
            }

            if (string.IsNullOrWhiteSpace(trainingCalendarFilter.KeyWord) == false)
            {
                classes = classes.Where(a => a.Class.ClassCode.Contains(trainingCalendarFilter.KeyWord)).ToList();
            }
            if (trainingCalendarFilter.Locations != null && trainingCalendarFilter.Locations.Length > 0)
            {
                if (trainingCalendarFilter.Locations.Length == 1)
                {
                    classes = classes.Where(a => a.Class.Locations.Where(b => b.Location.Name.ToLower().Contains(trainingCalendarFilter.Locations[0].ToLower())).IsNullOrEmpty() == false).ToList();
                }
                else
                {
                    foreach (var location in trainingCalendarFilter.Locations)
                    {
                        if (classes.Where(a => a.Class.Locations.Where(b => b.Location.Name.ToLower().Contains(location.ToLower())).IsNullOrEmpty() == false).IsNullOrEmpty())
                        {
                            continue;
                        }
                        classes = classes.Where(a => a.Class.Locations.Where(b => b.Location.Name.ToLower().Contains(location.ToLower())).IsNullOrEmpty() == false).ToList();
                    }
                }
            }
            if (trainingCalendarFilter.IdFSU != null)
            {
                classes = classes.Where(b => b.Class.IdFSU == trainingCalendarFilter.IdFSU).ToList();
            }
            if (trainingCalendarFilter.IdTrainer != null)
            {
                classes = classes.Where(c => c.Class.ClassMentors.Where(x => x.IdUser == trainingCalendarFilter.IdTrainer).IsNullOrEmpty() == false).ToList();
            }
            if (trainingCalendarFilter.TimeClasses.IsNullOrEmpty() == false && trainingCalendarFilter.TimeClasses.Length == 1)
            {
                if (trainingCalendarFilter.TimeClasses[0].Equals("Morning"))
                {
                    classes = classes.Where(x => x.Class.StartTimeLearning.Value.CompareTo(new TimeSpan(8, 0, 0)) >= 0 &&
                                   x.Class.EndTimeLearing.Value.CompareTo(new TimeSpan(12, 0, 0)) <= 0).ToList();
                    Console.WriteLine(classes.Count());
                }
                else if (trainingCalendarFilter.TimeClasses[0].Equals("Noon"))
                {
                    classes = classes.Where(x => x.Class.StartTimeLearning.Value.CompareTo(new TimeSpan(13, 0, 0)) >= 0 &&
                                   x.Class.EndTimeLearing.Value.CompareTo(new TimeSpan(17, 0, 0)) <= 0).ToList();
                }
                else if (trainingCalendarFilter.TimeClasses[0].Equals("Night"))
                {
                    classes = classes.Where(x => x.Class.StartTimeLearning.Value.CompareTo(new TimeSpan(18, 0, 0)) >= 0 &&
                                   x.Class.EndTimeLearing.Value.CompareTo(new TimeSpan(22, 0, 0)) <= 0).ToList();
                }
            }
            else if (trainingCalendarFilter.TimeClasses.IsNullOrEmpty() == false && trainingCalendarFilter.TimeClasses.Length == 2)
            {
                if (trainingCalendarFilter.TimeClasses[0].Equals("Morning") || trainingCalendarFilter.TimeClasses[0].Equals("Noon")
                    && trainingCalendarFilter.TimeClasses[1].Equals("Morning") || trainingCalendarFilter.TimeClasses[1].Equals("Noon"))
                {
                    classes = classes.Where(x => x.Class.StartTimeLearning.Value.CompareTo(new TimeSpan(8, 0, 0)) >= 0 &&
                                   x.Class.EndTimeLearing.Value.CompareTo(new TimeSpan(17, 0, 0)) <= 0).ToList();
                }
                else if (trainingCalendarFilter.TimeClasses[0].Equals("Morning") || trainingCalendarFilter.TimeClasses[0].Equals("Night")
                    && trainingCalendarFilter.TimeClasses[1].Equals("Morning") || trainingCalendarFilter.TimeClasses[1].Equals("Night"))
                {
                    classes = classes.Where(x => x.Class.StartTimeLearning.Value.CompareTo(new TimeSpan(8, 0, 0)) >= 0 &&
                                   x.Class.EndTimeLearing.Value.CompareTo(new TimeSpan(12, 0, 0)) <= 0
                                   || x.Class.StartTimeLearning.Value.CompareTo(new TimeSpan(18, 0, 0)) >= 0 &&
                                   x.Class.EndTimeLearing.Value.CompareTo(new TimeSpan(22, 0, 0)) <= 0).ToList();
                }
                else if (trainingCalendarFilter.TimeClasses[0].Equals("Noon") || trainingCalendarFilter.TimeClasses[0].Equals("Night")
                    && trainingCalendarFilter.TimeClasses[1].Equals("Noon") || trainingCalendarFilter.TimeClasses[1].Equals("Night"))
                {
                    classes = classes.Where(x => x.Class.StartTimeLearning.Value.CompareTo(new TimeSpan(13, 0, 0)) >= 0 &&
                                   x.Class.EndTimeLearing.Value.CompareTo(new TimeSpan(22, 0, 0)) <= 0).ToList();
                }
            }
            else if (trainingCalendarFilter.TimeClasses.IsNullOrEmpty() == false && trainingCalendarFilter.TimeClasses.Length == 3)
            {
                if (trainingCalendarFilter.TimeClasses[0].Equals("Morning") || trainingCalendarFilter.TimeClasses[0].Equals("Noon") || trainingCalendarFilter.TimeClasses[0].Equals("Night")
                    && trainingCalendarFilter.TimeClasses[1].Equals("Morning") || trainingCalendarFilter.TimeClasses[1].Equals("Noon") || trainingCalendarFilter.TimeClasses[1].Equals("Night")
                    && trainingCalendarFilter.TimeClasses[2].Equals("Morning") || trainingCalendarFilter.TimeClasses[2].Equals("Noon") || trainingCalendarFilter.TimeClasses[2].Equals("Night"))
                {
                    classes = classes.Where(x => x.Class.StartTimeLearning.Value.CompareTo(new TimeSpan(8, 0, 0)) >= 0 &&
                                   x.Class.EndTimeLearing.Value.CompareTo(new TimeSpan(22, 0, 0)) <= 0).ToList();
                }
            }


            if (trainingCalendarFilter.Statuses.IsNullOrEmpty() == false)
            {
                if (trainingCalendarFilter.Statuses.Length == 1)
                {
                    classes = classes.Where(x => x.Class.ClassStatus.Name.ToLower().Equals(trainingCalendarFilter.Statuses[0].ToLower())).ToList();
                }
                else
                {
                    foreach (var status in trainingCalendarFilter.Statuses)
                    {
                        if (classes.Where(x => x.Class.ClassStatus.Name.ToLower().Equals(status.ToLower())).IsNullOrEmpty())
                        {
                            continue;
                        }
                        classes = classes.Where(x => x.Class.ClassStatus.Name.ToLower().Equals(status.ToLower())).ToList();
                    }
                }
            }
            if (trainingCalendarFilter.Attendees.IsNullOrEmpty() == false)
            {
                if (trainingCalendarFilter.Attendees.Length == 1)
                {
                    classes = classes.Where(x => x.Class.AttendeeType.Name.ToLower().Equals(trainingCalendarFilter.Attendees[0].ToLower())).ToList();
                }
                else
                {
                    foreach (var attendee in trainingCalendarFilter.Attendees)
                    {
                        if (classes.Where(x => x.Class.AttendeeType.Name.ToLower().Equals(attendee.ToLower())).IsNullOrEmpty())
                        {
                            continue;
                        }
                        classes = classes.Where(x => x.Class.AttendeeType.Name.ToLower().Equals(attendee.ToLower())).ToList();
                    }
                }
            }
            return classes;
        }
        public async Task<List<ClassCalenderViewModel>> GetClassCalendars(long userID, DateTime date, TrainingCalendarViewModel? trainingCalendarFilter)
        {
            User user = await _userRepository.GetUserAsync(userID);
            List<ClassCalenderViewModel> classCalenders = null;
            List<ClassSelectedDate> classes = await _classSelectedDateRepository.GetSelectedDatesQueryAsync();
            classes = classes.Where(x => x.ActiveDate.Date.Equals(date)).ToList();
            if (trainingCalendarFilter != null)
            {
                classes = await GetClassCalendarsByFilter(userID, trainingCalendarFilter, classes);
            }

            switch (user.Role.Name)
            {
                case "Super Admin":
                    {
                        classCalenders = _mapper.Map<List<ClassCalenderViewModel>>(classes);
                        MapSelectedDateClass(classes, classCalenders);
                        break;
                    }
                case "Class Admin":
                    {
                        List<ClassSelectedDate> cls = new List<ClassSelectedDate>();
                        foreach (var item in classes)
                        {
                            foreach (var ca in item.Class.ClassAdmins)
                            {
                                if (ca.IdUser == userID)
                                {
                                    cls.Add(item);
                                }
                            }
                        }
                        classCalenders = _mapper.Map<List<ClassCalenderViewModel>>(cls);
                        MapSelectedDateClass(cls, classCalenders);
                        break;
                    }
                case "Trainer":
                    {
                        List<ClassSelectedDate> cls = new List<ClassSelectedDate>();
                        foreach (var item in classes)
                        {
                            foreach (var ca in item.Class.ClassMentors)
                            {
                                if (ca.IdUser == userID)
                                {
                                    cls.Add(item);
                                }
                            }
                        }
                        classCalenders = _mapper.Map<List<ClassCalenderViewModel>>(cls);
                        MapSelectedDateClass(cls, classCalenders);
                        break;
                    }
                case "Student":
                    {
                        List<ClassSelectedDate> cls = new List<ClassSelectedDate>();
                        foreach (var item in classes)
                        {
                            foreach (var ca in item.Class.ClassTrainees)
                            {
                                if (ca.IdUser == userID)
                                {
                                    cls.Add(item);
                                }
                            }
                        }
                        classCalenders = _mapper.Map<List<ClassCalenderViewModel>>(cls);
                        MapSelectedDateClass(cls, classCalenders);
                        break;
                    }
            }
            return classCalenders;
        }

        private void MapSelectedDateClass(List<ClassSelectedDate> classes, List<ClassCalenderViewModel> classCalenders)
        {
            foreach (var cl in classes)
            {
                foreach (var clvm in classCalenders)
                {
                    if (cl.IdClass == clvm.Id)
                    {
                        clvm.Locations = _mapper.Map<IEnumerable<ClassLocationViewModel>>(cl.Class.Locations);
                        clvm.ClassMentors = _mapper.Map<IEnumerable<TrainerViewModel>>(cl.Class.ClassMentors);
                        clvm.ClassAdmins = _mapper.Map<IEnumerable<AdminViewModel>>(cl.Class.ClassAdmins);
                    }
                }
            }
        }
        #endregion

        public IEnumerable<ClassCalenderViewModel> GetClassSelectedDateFilter(TrainingCalendarViewModel trainingCalendarViewModel, string date)
        {
            if (trainingCalendarViewModel == null)
            {
                if (string.IsNullOrEmpty(date))
                {
                    listClassCalenderViewModels = _mapper.Map<List<ClassCalenderViewModel>>(_classSelectedDateRepository.GetClassSelectedDateAll(DateTime.Now));
                }
                else
                {
                    listClassCalenderViewModels = _mapper.Map<List<ClassCalenderViewModel>>(_classSelectedDateRepository.GetClassSelectedDateAll(DateTime.Parse(date)));
                }
            }
            else
            {
                if (trainingCalendarViewModel.KeyWord != null)
                {
                    @class = new()
                    {
                        ClassCode = trainingCalendarViewModel.KeyWord,
                    };

                    listClass = _classRepository.GetWithFilter(@class);

                    if (listClass.Any())
                    {
                        foreach (Class c in listClass)
                        {
                            if (date == null || date.Equals(""))
                            {
                                listClassCalenderViewModels.Add(GetByIdClass(c.Id, DateTime.Now));
                            }
                            else
                            {
                                listClassCalenderViewModels.Add(GetByIdClass(c.Id, DateTime.Parse(date)));
                            }
                        }
                    }
                }
                else if (trainingCalendarViewModel.Locations != null)
                {
                    foreach (var locationFilter in trainingCalendarViewModel.Locations)
                    {
                        location = _locationRepository.GetLocationByName(locationFilter);
                        classLocation = _classLocationRepository.GetClassByIdLocation(location.Id);

                        @class = new()
                        {
                            Id = classLocation.IdClass,
                        };

                        listClass = _classRepository.GetWithFilter(@class);

                        if (listClass.Any())
                        {
                            foreach (Class c in listClass)
                            {
                                if (date == null || date.Equals(""))
                                {
                                    listClassCalenderViewModels.Add(GetByIdClass(c.Id, DateTime.Now));
                                }
                                else
                                {
                                    listClassCalenderViewModels.Add(GetByIdClass(c.Id, DateTime.Parse(date)));
                                }
                            }
                        }
                    }
                }
                else if (trainingCalendarViewModel.StartTime != null)
                {
                    @class = new()
                    {
                        StartDate = DateTime.Parse(trainingCalendarViewModel.StartTime),
                    };

                    listClass = _classRepository.GetWithFilter(@class);

                    if (listClass.Any())
                    {
                        foreach (Class c in listClass)
                        {
                            if (date == null || date.Equals(""))
                            {
                                listClassCalenderViewModels.Add(GetByIdClass(c.Id, DateTime.Now));
                            }
                            else
                            {
                                listClassCalenderViewModels.Add(GetByIdClass(c.Id, DateTime.Parse(date)));
                            }
                        }
                    }
                }
                else if (trainingCalendarViewModel.EndTime != null)
                {
                    @class = new()
                    {
                        EndDate = DateTime.Parse(trainingCalendarViewModel.EndTime),
                    };

                    listClass = _classRepository.GetWithFilter(@class);

                    if (listClass.Any())
                    {
                        foreach (Class c in listClass)
                        {
                            if (date == null || date.Equals(""))
                            {
                                listClassCalenderViewModels.Add(GetByIdClass(c.Id, DateTime.Now));
                            }
                            else
                            {
                                listClassCalenderViewModels.Add(GetByIdClass(c.Id, DateTime.Parse(date)));
                            }
                        }
                    }
                }
                else if (trainingCalendarViewModel.TimeClasses != null)
                {
                    foreach (var timeClasses in trainingCalendarViewModel.TimeClasses)
                    {
                        switch (timeClasses)
                        {
                            case "Morning":
                                @class = new()
                                {
                                    StartTimeLearning = TimeSpan.FromHours(8),
                                    EndTimeLearing = TimeSpan.FromHours(12),
                                };

                                listClass = _classRepository.GetWithFilter(@class);

                                if (listClass.Any())
                                {
                                    foreach (Class c in listClass)
                                    {
                                        if (date == null || date.Equals(""))
                                        {
                                            listClassCalenderViewModels.Add(GetByIdClass(c.Id, DateTime.Now));
                                        }
                                        else
                                        {
                                            listClassCalenderViewModels.Add(GetByIdClass(c.Id, DateTime.Parse(date)));
                                        }
                                    }
                                }
                                break;
                            case "Noon":
                                @class = new()
                                {
                                    StartTimeLearning = TimeSpan.FromHours(13),
                                    EndTimeLearing = TimeSpan.FromHours(17),
                                };

                                listClass = _classRepository.GetWithFilter(@class);

                                if (listClass.Any())
                                {
                                    foreach (Class c in listClass)
                                    {
                                        if (date == null || date.Equals(""))
                                        {
                                            listClassCalenderViewModels.Add(GetByIdClass(c.Id, DateTime.Now));
                                        }
                                        else
                                        {
                                            listClassCalenderViewModels.Add(GetByIdClass(c.Id, DateTime.Parse(date)));
                                        }
                                    }
                                }
                                break;
                            case "Night":
                                @class = new()
                                {
                                    StartTimeLearning = TimeSpan.FromHours(18),
                                    EndTimeLearing = TimeSpan.FromHours(22),
                                };

                                listClass = _classRepository.GetWithFilter(@class);

                                if (listClass.Any())
                                {
                                    foreach (Class c in listClass)
                                    {
                                        if (date == null || date.Equals(""))
                                        {
                                            listClassCalenderViewModels.Add(GetByIdClass(c.Id, DateTime.Now));
                                        }
                                        else
                                        {
                                            listClassCalenderViewModels.Add(GetByIdClass(c.Id, DateTime.Parse(date)));
                                        }
                                    }
                                }
                                break;
                        }
                    }
                }
                else if (trainingCalendarViewModel.Statuses != null)
                {
                    foreach (var statusFilter in trainingCalendarViewModel.Statuses)
                    {
                        classStatus = _classStatusRepository.GetClassStatusByName(statusFilter);

                        @class = new()
                        {
                            IdStatus = classStatus.Id,
                        };

                        listClass = _classRepository.GetWithFilter(@class);

                        if (listClass.Any())
                        {
                            foreach (Class c in listClass)
                            {
                                if (date == null || date.Equals(""))
                                {
                                    listClassCalenderViewModels.Add(GetByIdClass(c.Id, DateTime.Now));
                                }
                                else
                                {
                                    listClassCalenderViewModels.Add(GetByIdClass(c.Id, DateTime.Parse(date)));
                                }
                            }
                        }
                    }
                }
                else if (trainingCalendarViewModel.Attendees != null)
                {
                    foreach (var attendeesFilter in trainingCalendarViewModel.Attendees)
                    {
                        attendeeType = _attendeeTypeRepository.GetAttendeeTypeByName(attendeesFilter);

                        @class = new()
                        {
                            IdAttendeeType = attendeeType.Id,
                        };

                        listClass = _classRepository.GetWithFilter(@class);

                        if (listClass.Any())
                        {
                            foreach (Class c in listClass)
                            {
                                if (date == null || date.Equals(""))
                                {
                                    listClassCalenderViewModels.Add(GetByIdClass(c.Id, DateTime.Now));
                                }
                                else
                                {
                                    listClassCalenderViewModels.Add(GetByIdClass(c.Id, DateTime.Parse(date)));
                                }
                            }
                        }
                    }
                }
                else if (trainingCalendarViewModel.IdFSU != null)
                {
                    @class = new()
                    {
                        IdFSU = trainingCalendarViewModel.IdFSU,
                    };

                    listClass = _classRepository.GetWithFilter(@class);

                    if (listClass.Any())
                    {
                        foreach (Class c in listClass)
                        {
                            if (date == null || date.Equals(""))
                            {
                                listClassCalenderViewModels.Add(GetByIdClass(c.Id, DateTime.Now));
                            }
                            else
                            {
                                listClassCalenderViewModels.Add(GetByIdClass(c.Id, DateTime.Parse(date)));
                            }
                        }
                    }
                }
                else if (trainingCalendarViewModel.IdTrainer != null)
                {
                    ClassMentor classMentor = _classMentorRepository.GetById(trainingCalendarViewModel.IdTrainer);

                    @class = new()
                    {
                        Id = classMentor.IdClass
                    };

                    listClass = _classRepository.GetWithFilter(@class);

                    if (listClass.Any())
                    {
                        foreach (Class c in listClass)
                        {
                            if (date == null || date.Equals(""))
                            {
                                listClassCalenderViewModels.Add(GetByIdClass(c.Id, DateTime.Now));
                            }
                            else
                            {
                                listClassCalenderViewModels.Add(GetByIdClass(c.Id, DateTime.Parse(date)));
                            }
                        }
                    }
                }
            }

            return listClassCalenderViewModels;
        }

        public IEnumerable<ClassCalenderViewModel> GetClassSelectedDateFilterWithIDClass(long idClass, TrainingCalendarViewModel trainingCalendarViewModel, string date)
        {
            List<ClassCalenderViewModel> listClassCalenderViewModels = new();

            if (trainingCalendarViewModel == null)
            {
                if (date == null || date.Equals(""))
                {
                    listClassCalenderViewModels = _mapper.Map<List<ClassCalenderViewModel>>(_classSelectedDateRepository.GetClassSelectedDateByID(idClass, DateTime.Now));
                }
                else
                {
                    listClassCalenderViewModels = _mapper.Map<List<ClassCalenderViewModel>>(_classSelectedDateRepository.GetClassSelectedDateByID(idClass, DateTime.Parse(date)));
                }
            }
            else
            {
                if (trainingCalendarViewModel.KeyWord != null)
                {
                    @class = new()
                    {
                        ClassCode = trainingCalendarViewModel.KeyWord,
                    };

                    listClass = _classRepository.GetWithFilter(@class);

                    if (listClass.Any())
                    {
                        foreach (Class c in listClass)
                        {
                            if (date == null || date.Equals(""))
                            {
                                listClassCalenderViewModels.Add(GetByIdClassFilter(idClass, c.Id, DateTime.Now));
                            }
                            else
                            {
                                listClassCalenderViewModels.Add(GetByIdClassFilter(idClass, c.Id, DateTime.Parse(date)));
                            }
                        }
                    }
                }
                else if (trainingCalendarViewModel.Locations != null)
                {
                    foreach (var locationFilter in trainingCalendarViewModel.Locations)
                    {
                        location = _locationRepository.GetLocationByName(locationFilter);
                        classLocation = _classLocationRepository.GetClassByIdLocation(location.Id);

                        @class = new()
                        {
                            Id = classLocation.IdClass,
                        };

                        listClass = _classRepository.GetWithFilter(@class);

                        if (listClass.Any())
                        {
                            foreach (Class c in listClass)
                            {
                                if (date == null || date.Equals(""))
                                {
                                    listClassCalenderViewModels.Add(GetByIdClassFilter(idClass, c.Id, DateTime.Now));
                                }
                                else
                                {
                                    listClassCalenderViewModels.Add(GetByIdClassFilter(idClass, c.Id, DateTime.Parse(date)));
                                }
                            }
                        }
                    }
                }
                else if (trainingCalendarViewModel.StartTime != null)
                {
                    @class = new()
                    {
                        StartDate = DateTime.Parse(trainingCalendarViewModel.StartTime),
                    };

                    listClass = _classRepository.GetWithFilter(@class);

                    if (listClass.Any())
                    {
                        foreach (Class c in listClass)
                        {
                            if (date == null || date.Equals(""))
                            {
                                listClassCalenderViewModels.Add(GetByIdClassFilter(idClass, c.Id, DateTime.Now));
                            }
                            else
                            {
                                listClassCalenderViewModels.Add(GetByIdClassFilter(idClass, c.Id, DateTime.Parse(date)));
                            }
                        }
                    }
                }
                else if (trainingCalendarViewModel.EndTime != null)
                {
                    @class = new()
                    {
                        EndDate = DateTime.Parse(trainingCalendarViewModel.EndTime),
                    };

                    listClass = _classRepository.GetWithFilter(@class);

                    if (listClass.Any())
                    {
                        foreach (Class c in listClass)
                        {
                            if (date == null || date.Equals(""))
                            {
                                listClassCalenderViewModels.Add(GetByIdClassFilter(idClass, c.Id, DateTime.Now));
                            }
                            else
                            {
                                listClassCalenderViewModels.Add(GetByIdClassFilter(idClass, c.Id, DateTime.Parse(date)));
                            }
                        }
                    }
                }
                else if (trainingCalendarViewModel.TimeClasses != null)
                {
                    foreach (var timeClasses in trainingCalendarViewModel.TimeClasses)
                    {
                        switch (timeClasses)
                        {
                            case "Morning":
                                @class = new()
                                {
                                    StartTimeLearning = TimeSpan.FromHours(8),
                                    EndTimeLearing = TimeSpan.FromHours(12),
                                };

                                listClass = _classRepository.GetWithFilter(@class);

                                if (listClass.Any())
                                {
                                    foreach (Class c in listClass)
                                    {
                                        if (date == null || date.Equals(""))
                                        {
                                            listClassCalenderViewModels.Add(GetByIdClassFilter(idClass, c.Id, DateTime.Now));
                                        }
                                        else
                                        {
                                            listClassCalenderViewModels.Add(GetByIdClassFilter(idClass, c.Id, DateTime.Parse(date)));
                                        }
                                    }
                                }
                                break;
                            case "Noon":
                                @class = new()
                                {
                                    StartTimeLearning = TimeSpan.FromHours(13),
                                    EndTimeLearing = TimeSpan.FromHours(17),
                                };

                                listClass = _classRepository.GetWithFilter(@class);

                                if (listClass.Any())
                                {
                                    foreach (Class c in listClass)
                                    {
                                        if (date == null || date.Equals(""))
                                        {
                                            listClassCalenderViewModels.Add(GetByIdClassFilter(idClass, c.Id, DateTime.Now));
                                        }
                                        else
                                        {
                                            listClassCalenderViewModels.Add(GetByIdClassFilter(idClass, c.Id, DateTime.Parse(date)));
                                        }
                                    }
                                }
                                break;
                            case "Night":
                                @class = new()
                                {
                                    StartTimeLearning = TimeSpan.FromHours(18),
                                    EndTimeLearing = TimeSpan.FromHours(22),
                                };

                                listClass = _classRepository.GetWithFilter(@class);

                                if (listClass.Any())
                                {
                                    foreach (Class c in listClass)
                                    {
                                        if (date == null || date.Equals(""))
                                        {
                                            listClassCalenderViewModels.Add(GetByIdClassFilter(idClass, c.Id, DateTime.Now));
                                        }
                                        else
                                        {
                                            listClassCalenderViewModels.Add(GetByIdClassFilter(idClass, c.Id, DateTime.Parse(date)));
                                        }
                                    }
                                }
                                break;
                        }
                    }
                }
                else if (trainingCalendarViewModel.Statuses != null)
                {
                    foreach (var statusFilter in trainingCalendarViewModel.Statuses)
                    {
                        classStatus = _classStatusRepository.GetClassStatusByName(statusFilter);

                        @class = new()
                        {
                            IdStatus = classStatus.Id,
                        };

                        listClass = _classRepository.GetWithFilter(@class);

                        if (listClass.Any())
                        {
                            foreach (Class c in listClass)
                            {
                                if (date == null || date.Equals(""))
                                {
                                    listClassCalenderViewModels.Add(GetByIdClassFilter(idClass, c.Id, DateTime.Now));
                                }
                                else
                                {
                                    listClassCalenderViewModels.Add(GetByIdClassFilter(idClass, c.Id, DateTime.Parse(date)));
                                }
                            }
                        }
                    }
                }
                else if (trainingCalendarViewModel.Attendees != null)
                {
                    foreach (var attendeesFilter in trainingCalendarViewModel.Attendees)
                    {
                        attendeeType = _attendeeTypeRepository.GetAttendeeTypeByName(attendeesFilter);

                        @class = new()
                        {
                            IdAttendeeType = attendeeType.Id,
                        };

                        listClass = _classRepository.GetWithFilter(@class);

                        if (listClass.Any())
                        {
                            foreach (Class c in listClass)
                            {
                                if (date == null || date.Equals(""))
                                {
                                    listClassCalenderViewModels.Add(GetByIdClassFilter(idClass, c.Id, DateTime.Now));
                                }
                                else
                                {
                                    listClassCalenderViewModels.Add(GetByIdClassFilter(idClass, c.Id, DateTime.Parse(date)));
                                }
                            }
                        }
                    }
                }
                else if (trainingCalendarViewModel.IdFSU != null)
                {
                    @class = new()
                    {
                        IdFSU = trainingCalendarViewModel.IdFSU,
                    };

                    listClass = _classRepository.GetWithFilter(@class);

                    if (listClass.Any())
                    {
                        foreach (Class c in listClass)
                        {
                            if (date == null || date.Equals(""))
                            {
                                listClassCalenderViewModels.Add(GetByIdClassFilter(idClass, c.Id, DateTime.Now));
                            }
                            else
                            {
                                listClassCalenderViewModels.Add(GetByIdClassFilter(idClass, c.Id, DateTime.Parse(date)));
                            }
                        }
                    }
                }
                else if (trainingCalendarViewModel.IdTrainer != null)
                {
                    ClassMentor classMentor = _classMentorRepository.GetById(trainingCalendarViewModel.IdTrainer);

                    @class = new()
                    {
                        Id = classMentor.IdClass
                    };

                    listClass = _classRepository.GetWithFilter(@class);

                    if (listClass.Any())
                    {
                        foreach (Class c in listClass)
                        {
                            if (date == null || date.Equals(""))
                            {
                                listClassCalenderViewModels.Add(GetByIdClassFilter(idClass, c.Id, DateTime.Now));
                            }
                            else
                            {
                                listClassCalenderViewModels.Add(GetByIdClassFilter(idClass, c.Id, DateTime.Parse(date)));
                            }
                        }
                    }
                }
            }

            return listClassCalenderViewModels;
        }

        public ClassCalenderViewModel GetByIdClass(long idClass, DateTime? date)
        {
            return _mapper.Map<ClassCalenderViewModel>(_classSelectedDateRepository.GetByIdClass(idClass, date));
        }

        public ClassCalenderViewModel GetByIdClassFilter(long idClass, long idClassFilter, DateTime? date)
        {
            return _mapper.Map<ClassCalenderViewModel>(_classSelectedDateRepository.GetByIdClassFilter(idClass, idClassFilter, date));
        }

        public IEnumerable<ClassCalenderViewModel> GetClassSelectedDateFilter(TrainingCalendarViewModel trainingCalendarViewModel)
        {
            if (trainingCalendarViewModel == null)
            {
                listClassCalenderViewModels = _mapper.Map<List<ClassCalenderViewModel>>(_classSelectedDateRepository.GetClassSelectedDateAll(null));
            }
            else
            {
                if (trainingCalendarViewModel.KeyWord != null)
                {
                    @class = new()
                    {
                        ClassCode = trainingCalendarViewModel.KeyWord,
                    };

                    listClass = _classRepository.GetWithFilter(@class);

                    if (listClass.Any())
                    {
                        foreach (Class c in listClass)
                        {
                            listClassCalenderViewModels.Add(GetByIdClass(c.Id, null));
                        }
                    }
                }
                else if (trainingCalendarViewModel.Locations != null)
                {
                    foreach (var locationFilter in trainingCalendarViewModel.Locations)
                    {
                        location = _locationRepository.GetLocationByName(locationFilter);
                        classLocation = _classLocationRepository.GetClassByIdLocation(location.Id);

                        @class = new()
                        {
                            Id = classLocation.IdClass,
                        };

                        listClass = _classRepository.GetWithFilter(@class);

                        if (listClass.Any())
                        {
                            foreach (Class c in listClass)
                            {
                                listClassCalenderViewModels.Add(GetByIdClass(c.Id, null));
                            }
                        }
                    }
                }
                else if (trainingCalendarViewModel.StartTime != null)
                {
                    @class = new()
                    {
                        StartDate = DateTime.Parse(trainingCalendarViewModel.StartTime),
                    };

                    listClass = _classRepository.GetWithFilter(@class);

                    if (listClass.Any())
                    {
                        foreach (Class c in listClass)
                        {
                            listClassCalenderViewModels.Add(GetByIdClass(c.Id, null));
                        }
                    }
                }
                else if (trainingCalendarViewModel.EndTime != null)
                {
                    @class = new()
                    {
                        EndDate = DateTime.Parse(trainingCalendarViewModel.EndTime),
                    };

                    listClass = _classRepository.GetWithFilter(@class);

                    if (listClass.Any())
                    {
                        foreach (Class c in listClass)
                        {
                            listClassCalenderViewModels.Add(GetByIdClass(c.Id, null));
                        }
                    }
                }
                else if (trainingCalendarViewModel.TimeClasses != null)
                {
                    foreach (var timeClasses in trainingCalendarViewModel.TimeClasses)
                    {
                        switch (timeClasses)
                        {
                            case "Morning":
                                @class = new()
                                {
                                    StartTimeLearning = TimeSpan.FromHours(8),
                                    EndTimeLearing = TimeSpan.FromHours(12),
                                };

                                listClass = _classRepository.GetWithFilter(@class);

                                if (listClass.Any())
                                {
                                    foreach (Class c in listClass)
                                    {
                                        listClassCalenderViewModels.Add(GetByIdClass(c.Id, null));
                                    }
                                }
                                break;
                            case "Noon":
                                @class = new()
                                {
                                    StartTimeLearning = TimeSpan.FromHours(13),
                                    EndTimeLearing = TimeSpan.FromHours(17),
                                };

                                listClass = _classRepository.GetWithFilter(@class);

                                if (listClass.Any())
                                {
                                    foreach (Class c in listClass)
                                    {
                                        listClassCalenderViewModels.Add(GetByIdClass(c.Id, null));
                                    }
                                }
                                break;
                            case "Night":
                                @class = new()
                                {
                                    StartTimeLearning = TimeSpan.FromHours(18),
                                    EndTimeLearing = TimeSpan.FromHours(22),
                                };

                                listClass = _classRepository.GetWithFilter(@class);

                                if (listClass.Any())
                                {
                                    foreach (Class c in listClass)
                                    {
                                        listClassCalenderViewModels.Add(GetByIdClass(c.Id, null));
                                    }
                                }
                                break;
                        }
                    }
                }
                else if (trainingCalendarViewModel.Statuses != null)
                {
                    foreach (var statusFilter in trainingCalendarViewModel.Statuses)
                    {
                        classStatus = _classStatusRepository.GetClassStatusByName(statusFilter);

                        @class = new()
                        {
                            IdStatus = classStatus.Id,
                        };

                        listClass = _classRepository.GetWithFilter(@class);

                        if (listClass.Any())
                        {
                            foreach (Class c in listClass)
                            {
                                listClassCalenderViewModels.Add(GetByIdClass(c.Id, null));
                            }
                        }
                    }
                }
                else if (trainingCalendarViewModel.Attendees != null)
                {
                    foreach (var attendeesFilter in trainingCalendarViewModel.Attendees)
                    {
                        attendeeType = _attendeeTypeRepository.GetAttendeeTypeByName(attendeesFilter);

                        @class = new()
                        {
                            IdAttendeeType = attendeeType.Id,
                        };

                        listClass = _classRepository.GetWithFilter(@class);

                        if (listClass.Any())
                        {
                            foreach (Class c in listClass)
                            {
                                listClassCalenderViewModels.Add(GetByIdClass(c.Id, null));
                            }
                        }
                    }
                }
                else if (trainingCalendarViewModel.IdFSU != null)
                {
                    @class = new()
                    {
                        IdFSU = trainingCalendarViewModel.IdFSU,
                    };

                    listClass = _classRepository.GetWithFilter(@class);

                    if (listClass.Any())
                    {
                        foreach (Class c in listClass)
                        {
                            listClassCalenderViewModels.Add(GetByIdClass(c.Id, null));
                        }
                    }
                }
                else if (trainingCalendarViewModel.IdTrainer != null)
                {
                    ClassMentor classMentor = _classMentorRepository.GetById(trainingCalendarViewModel.IdTrainer);

                    @class = new()
                    {
                        Id = classMentor.IdClass
                    };

                    listClass = _classRepository.GetWithFilter(@class);

                    if (listClass.Any())
                    {
                        foreach (Class c in listClass)
                        {
                            listClassCalenderViewModels.Add(GetByIdClass(c.Id, null));
                        }
                    }
                }
            }

            return listClassCalenderViewModels;
        }

        public IEnumerable<ClassCalenderViewModel> GetClassSelectedDateFilterWithIDClass(long idClass, TrainingCalendarViewModel trainingCalendarViewModel)
        {
            List<ClassCalenderViewModel> listClassCalenderViewModels = new();

            if (trainingCalendarViewModel == null)
            {
                listClassCalenderViewModels = _mapper.Map<List<ClassCalenderViewModel>>(_classSelectedDateRepository.GetClassSelectedDateByID(idClass, null));
            }
            else
            {
                if (trainingCalendarViewModel.KeyWord != null)
                {
                    @class = new()
                    {
                        ClassCode = trainingCalendarViewModel.KeyWord,
                    };

                    listClass = _classRepository.GetWithFilter(@class);

                    if (listClass.Any())
                    {
                        foreach (Class c in listClass)
                        {
                            listClassCalenderViewModels.Add(GetByIdClassFilter(idClass, c.Id, null));
                        }
                    }
                }
                else if (trainingCalendarViewModel.Locations != null)
                {
                    foreach (var locationFilter in trainingCalendarViewModel.Locations)
                    {
                        location = _locationRepository.GetLocationByName(locationFilter);
                        classLocation = _classLocationRepository.GetClassByIdLocation(location.Id);

                        @class = new()
                        {
                            Id = classLocation.IdClass,
                        };

                        listClass = _classRepository.GetWithFilter(@class);

                        if (listClass.Any())
                        {
                            foreach (Class c in listClass)
                            {
                                listClassCalenderViewModels.Add(GetByIdClassFilter(idClass, c.Id, null));
                            }
                        }
                    }
                }
                else if (trainingCalendarViewModel.StartTime != null)
                {
                    @class = new()
                    {
                        StartDate = DateTime.Parse(trainingCalendarViewModel.StartTime),
                    };

                    listClass = _classRepository.GetWithFilter(@class);

                    if (listClass.Any())
                    {
                        foreach (Class c in listClass)
                        {
                            listClassCalenderViewModels.Add(GetByIdClassFilter(idClass, c.Id, null));
                        }
                    }
                }
                else if (trainingCalendarViewModel.EndTime != null)
                {
                    @class = new()
                    {
                        EndDate = DateTime.Parse(trainingCalendarViewModel.EndTime),
                    };

                    listClass = _classRepository.GetWithFilter(@class);

                    if (listClass.Any())
                    {
                        foreach (Class c in listClass)
                        {
                            listClassCalenderViewModels.Add(GetByIdClassFilter(idClass, c.Id, null));
                        }
                    }
                }
                else if (trainingCalendarViewModel.TimeClasses != null)
                {
                    foreach (var timeClasses in trainingCalendarViewModel.TimeClasses)
                    {
                        switch (timeClasses)
                        {
                            case "Morning":
                                @class = new()
                                {
                                    StartTimeLearning = TimeSpan.FromHours(8),
                                    EndTimeLearing = TimeSpan.FromHours(12),
                                };

                                listClass = _classRepository.GetWithFilter(@class);

                                if (listClass.Any())
                                {
                                    foreach (Class c in listClass)
                                    {
                                        listClassCalenderViewModels.Add(GetByIdClassFilter(idClass, c.Id, null));
                                    }
                                }
                                break;
                            case "Noon":
                                @class = new()
                                {
                                    StartTimeLearning = TimeSpan.FromHours(13),
                                    EndTimeLearing = TimeSpan.FromHours(17),
                                };

                                listClass = _classRepository.GetWithFilter(@class);

                                if (listClass.Any())
                                {
                                    foreach (Class c in listClass)
                                    {
                                        listClassCalenderViewModels.Add(GetByIdClassFilter(idClass, c.Id, null));
                                    }
                                }
                                break;
                            case "Night":
                                @class = new()
                                {
                                    StartTimeLearning = TimeSpan.FromHours(18),
                                    EndTimeLearing = TimeSpan.FromHours(22),
                                };

                                listClass = _classRepository.GetWithFilter(@class);

                                if (listClass.Any())
                                {
                                    foreach (Class c in listClass)
                                    {
                                        listClassCalenderViewModels.Add(GetByIdClassFilter(idClass, c.Id, null));
                                    }
                                }
                                break;
                        }
                    }
                }
                else if (trainingCalendarViewModel.Statuses != null)
                {
                    foreach (var statusFilter in trainingCalendarViewModel.Statuses)
                    {
                        classStatus = _classStatusRepository.GetClassStatusByName(statusFilter);

                        @class = new()
                        {
                            IdStatus = classStatus.Id,
                        };

                        listClass = _classRepository.GetWithFilter(@class);

                        if (listClass.Any())
                        {
                            foreach (Class c in listClass)
                            {
                                listClassCalenderViewModels.Add(GetByIdClassFilter(idClass, c.Id, null));
                            }
                        }
                    }
                }
                else if (trainingCalendarViewModel.Attendees != null)
                {
                    foreach (var attendeesFilter in trainingCalendarViewModel.Attendees)
                    {
                        attendeeType = _attendeeTypeRepository.GetAttendeeTypeByName(attendeesFilter);

                        @class = new()
                        {
                            IdAttendeeType = attendeeType.Id,
                        };

                        listClass = _classRepository.GetWithFilter(@class);

                        if (listClass.Any())
                        {
                            foreach (Class c in listClass)
                            {
                                listClassCalenderViewModels.Add(GetByIdClassFilter(idClass, c.Id, null));
                            }
                        }
                    }
                }
                else if (trainingCalendarViewModel.IdFSU != null)
                {
                    @class = new()
                    {
                        IdFSU = trainingCalendarViewModel.IdFSU,
                    };

                    listClass = _classRepository.GetWithFilter(@class);

                    if (listClass.Any())
                    {
                        foreach (Class c in listClass)
                        {
                            listClassCalenderViewModels.Add(GetByIdClassFilter(idClass, c.Id, null));
                        }
                    }
                }
                else if (trainingCalendarViewModel.IdTrainer != null)
                {
                    ClassMentor classMentor = _classMentorRepository.GetById(trainingCalendarViewModel.IdTrainer);

                    @class = new()
                    {
                        Id = classMentor.IdClass
                    };

                    listClass = _classRepository.GetWithFilter(@class);

                    if (listClass.Any())
                    {
                        foreach (Class c in listClass)
                        {
                            listClassCalenderViewModels.Add(GetByIdClassFilter(idClass, c.Id, null));
                        }
                    }
                }
            }

            return listClassCalenderViewModels;
        }
    }
}
