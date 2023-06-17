using AutoMapper;
using BAL.Models;
using BAL.Services.Interfaces;
using DAL;
using DAL.Entities;
using DAL.Infrastructure;
using DAL.Repositories.Interfaces;
using ExcelDataReader;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Collections;
using System.Linq.Dynamic.Core;
using System.Data;
using static DAL.Entities.Class;

namespace BAL.Services.Implements
{
  public class ClassService : IClassService
  {

    public readonly FRMDbContext _context;
    private IClassRepository _classRepository;
    private IUnitOfWork _unitOfWork;
    private IMapper _mapper;
    private IClassSelectedDateRepository _classSelectedDateRepository;
    private IClassTraineeRepository _classTraineeRepository;
    private IClassAdminReporitory _classAdminRepository;
    private IClassMentorRepository _classMentorRepository;
    private ICurriculumRepository _curriculumRepository;
    private ITrainingProgramRepository _trainingProgramRepository;
    private ISyllabusRepository _syllabusRepository;
    private IHistoryTrainingProgramRepository _historyTrainingProgramRepository;
    private IClassStatusRepository _classStatusRepository;
    private IFsoftUnitRepository _fsoftUnitRepository;
    private IClassUniversityCodeRepository _classUniversityCodeRepository;
    private IClassTechnicalGroupRepository _classTechnicalGroupRepository;
    private IClassSiteRepository _classSiteRepository;
    private IClassFormatTypeRepository _classFormatTypeRepository;
    private IClassProgramCodeRepository _classProgramCodeRepository;
    private IAttendeeTypeRepository _attendeeTypeRepository;
    private IUserRepository _userRepository;
    private IClassLocationRepository _classLocationRepository;

    // private IClassUpdateHistoryRepository _classUpdateHistoryRepository;

    private ISessionRepository _sessionRepository;
    private IUnitRepository _unitRepository;
    private ILessonRepository _lessonRepository;
    private ILocationRepository _locationRepository;
    private ISyllabusService _syllabusServiece;

    public ClassService(FRMDbContext context,
     IClassRepository classRepository,
     IUnitOfWork unitOfWork,
      IMapper mapper,
     IClassSelectedDateRepository classSelectedDateRepository,
     IClassTraineeRepository classTraineeRepository,
     IClassAdminReporitory classAdminReporitory,
     IClassMentorRepository classMentorRepository,
     ICurriculumRepository curriculumRepository,
     ITrainingProgramRepository trainingProgramRepository,
     ISyllabusRepository syllabusRepository,
     IHistoryTrainingProgramRepository historyTrainingProgramRepository,
     IClassStatusRepository classStatusRepository,
     IFsoftUnitRepository fsoftUnitRepository,
     IClassUniversityCodeRepository classUniversityCodeRepository,
     IClassTechnicalGroupRepository classTechnicalGroupRepository,
     IClassSiteRepository classSiteRepository,
     IClassFormatTypeRepository classFormatTypeRepository,
     IClassProgramCodeRepository classProgramCodeRepository,
     IAttendeeTypeRepository attendeeTypeRepository,
     IUserRepository userRepository,
     IUnitRepository unitRepository,
     ISessionRepository sessionRepository,
     ILessonRepository lessonRepository,
     ILocationRepository locationRepository,
     IClassLocationRepository classLocationRepository,
     ISyllabusService syllabusService

 )
    {
      _context = context;
      _classRepository = classRepository;
      _unitOfWork = unitOfWork;
      _mapper = mapper;
      _classSelectedDateRepository = classSelectedDateRepository;
      _classTraineeRepository = classTraineeRepository;
      _classAdminRepository = classAdminReporitory;
      _classMentorRepository = classMentorRepository;
      _curriculumRepository = curriculumRepository;
      _trainingProgramRepository = trainingProgramRepository;
      _syllabusRepository = syllabusRepository;
      _historyTrainingProgramRepository = historyTrainingProgramRepository;
      _classStatusRepository = classStatusRepository;
      _classUniversityCodeRepository = classUniversityCodeRepository;
      _classTechnicalGroupRepository = classTechnicalGroupRepository;
      _fsoftUnitRepository = fsoftUnitRepository;
      _classSiteRepository = classSiteRepository;
      _classFormatTypeRepository = classFormatTypeRepository;
      _classProgramCodeRepository = classProgramCodeRepository;
      _attendeeTypeRepository = attendeeTypeRepository;
      _userRepository = userRepository;
      _classLocationRepository = classLocationRepository;
      _attendeeTypeRepository = attendeeTypeRepository;
      _unitRepository = unitRepository;
      _sessionRepository = sessionRepository;
      _lessonRepository = lessonRepository;
      _classLocationRepository = classLocationRepository;
      _locationRepository = locationRepository;
      _syllabusServiece = syllabusService;
    }


    public async Task UpdateClass(UpdateClassViewModel classViewModel)
    {
      var TPofClass = _classRepository.GetById(classViewModel.Id);
      if (TPofClass == null) { throw new Exception($"Class ID: {TPofClass.Id} does not exit in the system!!!"); }

      var ClassEntities = _mapper.Map<Class>(classViewModel);

      List<ClassSelectedDate> listActiveDate = new List<ClassSelectedDate>();
      //Add list active date in class
      foreach (var date in classViewModel.ActiveDate)
      {
        var newClassSelectedDate = new ClassSelectedDate
        {
          IdClass = classViewModel.Id,
          ActiveDate = date,
          Status = 1
        };
        // _classSelectedDateRepository.AddSeletedDate(newClassSelectedDate);
        listActiveDate.Add(newClassSelectedDate);
      }
      ClassEntities.ClassSelectedDates = listActiveDate;

      //Get list trainee in class
      List<ClassTrainee> listtrainee = _classTraineeRepository.GetCLassTrainee(classViewModel.Id);
      //Delete new admin in class
      foreach (var oldtrainee in listtrainee)
      {
        _classTraineeRepository.DeleteTrainee(oldtrainee);
      }
      //Add new trainee in class
      foreach (var newtrainee in classViewModel.IdTrainee)
      {
        User checkUser = await _userRepository.GetById(newtrainee);
        if (checkUser == null)
        {
          throw new Exception($"User ID {newtrainee} does not exist in the system!");
        }
        var newClassTrainee = new ClassTrainee
        {
          IdClass = classViewModel.Id,
          IdUser = newtrainee
        };
        _classTraineeRepository.AddTrainee(newClassTrainee);
      }

      //Get list Mentor in class
      List<ClassMentor> listmentor = _classMentorRepository.GetCLassMentor(classViewModel.Id);
      //Delete ole Mentor in class
      foreach (var oldmentor in listmentor)
      {
        _classMentorRepository.DeleteMentor(oldmentor);
      }
      //Add new Mentor in class
      foreach (var newmentor in classViewModel.IdMentor)
      {
        User checkUser = await _userRepository.GetById(newmentor);
        if (checkUser == null)
        {
          throw new Exception($"User ID {newmentor} does not exist in the system!");
        }
        var newClassMentor = new ClassMentor
        {
          IdClass = classViewModel.Id,
          IdUser = newmentor
        };
        _classMentorRepository.AddMentor(newClassMentor);
      }

      //Get list Admin in class
      List<ClassAdmin> listAdmin = _classAdminRepository.GetCLassAdmin(classViewModel.Id);
      //Delete old Admin in class
      foreach (var oldadmin in listAdmin)
      {
        _classAdminRepository.DeleteAdmin(oldadmin);
      }
      //Add new admin in class
      foreach (var newadmin in classViewModel.IdAdmin)
      {
        User checkUser = await _userRepository.GetById(newadmin);
        if (checkUser == null)
        {
          throw new Exception($"User ID {newadmin} does not exist in the system!");
        }
        var newClassAdmin = new ClassAdmin
        {
          IdClass = classViewModel.Id,
          IdUser = newadmin
        };
        _classAdminRepository.AddAdmin(newClassAdmin);
      }

      List<ClassLocation> listLocation = await _classLocationRepository.GetByClassId(classViewModel.Id);
      foreach (var oldLocation in listLocation)
      {
        _classLocationRepository.DeleteLocation(oldLocation);
      }
      //Add Class location
      foreach (var location in classViewModel.IdLocation)
      {
        Location checkLocation = _locationRepository.GetById(location);
        if (checkLocation == null)
        {
          throw new Exception($"Location ID {location} does not exist in the system!");
        }
        var locations = new ClassLocation
        {
          IdClass = classViewModel.Id,
          IdLocation = location
        };
        _classLocationRepository.AddLocation(locations);
      }
      _unitOfWork.Commit();


      if (TPofClass.IdProgram == null || TPofClass.IdProgram != classViewModel.IdProgram)
      {
        //Get entity old Training Program by id
        var oldTP = _trainingProgramRepository.GetbyId(classViewModel.IdProgram);
        if (oldTP == null || classViewModel.IdProgram != oldTP.Id)
        { throw new Exception($"Training Program Id: {classViewModel.IdProgram} does not exit in system"); };
        // new Training program view model
        ClassTrainingProgamViewModel newTP = new ClassTrainingProgamViewModel();
        // mapentity old training program to new training program view model
        newTP = _mapper.Map<ClassTrainingProgamViewModel>(oldTP);
        newTP.Id = null;
        newTP.Name = oldTP.Name + "(Coppy)";

        //map new training program view model to entity training program
        var TPentity = _mapper.Map<TrainingProgram>(newTP);

        TPentity.HistoryTrainingPrograms = new List<HistoryTrainingProgram>();
        List<HistoryTrainingProgram> listhistory = new List<HistoryTrainingProgram>();
        //add element old history to list history
        foreach (var item in oldTP.HistoryTrainingPrograms)
        {
          var newhtr = new HistoryTrainingProgram
          {
            IdUser = item.IdUser,
            ModifiedOn = item.ModifiedOn
          };
          listhistory.Add(newhtr);
        }
        TPentity.HistoryTrainingPrograms = listhistory;


        List<Curriculum> listcurri = new List<Curriculum>();

        //duplicate syllabus
        foreach (var syllabus in classViewModel.Syllabi)
        {
          var oldSyllabus = _syllabusRepository.GetById(syllabus.idSyllabus);
          if (oldSyllabus != null)
          {
            var newSyllabus = new Syllabus();
            newSyllabus.Name = oldSyllabus.Name + " (Copy)";
            newSyllabus.Description = oldSyllabus.Description;
            newSyllabus.Status = oldSyllabus.Status;
            newSyllabus.AssignmentSchema = new AssignmentSchema()
            {
              PercentQuiz = oldSyllabus.AssignmentSchema.PercentQuiz,
              PercentAssigment = oldSyllabus.AssignmentSchema.PercentAssigment,
              PercentFinal = oldSyllabus.AssignmentSchema.PercentFinal,
              PercentTheory = oldSyllabus.AssignmentSchema.PercentTheory,
              PercentFinalPractice = oldSyllabus.AssignmentSchema.PercentFinalPractice,
              PassingCriterial = oldSyllabus.AssignmentSchema.PassingCriterial
            };
            newSyllabus.Code = oldSyllabus.Code;
            newSyllabus.AttendeeNumber = oldSyllabus.AttendeeNumber;
            newSyllabus.Technicalrequirement = oldSyllabus.Technicalrequirement;
            newSyllabus.CourseObjectives = oldSyllabus.CourseObjectives;
            newSyllabus.TrainingPrinciple = oldSyllabus.TrainingPrinciple;
            newSyllabus.HyperLink = oldSyllabus.HyperLink;
            newSyllabus.IdLevel = oldSyllabus.IdLevel;
            newSyllabus.Level = oldSyllabus.Level;
            newSyllabus.Version = oldSyllabus.Version = 1.00F;
            // Add history
            newSyllabus.HistorySyllabi = new List<HistorySyllabus>
                {
                    new HistorySyllabus
                    {
                        IdUser = classViewModel.CreatedBy.Value,
                        ModifiedOn = DateTime.Now,
                        Action = "Duplicate"
                    }
                };
            // Add session, unit, lesson, material from old syllabus to new syllabus
            if (oldSyllabus.Sessions != null)
            {
              var listSession = new List<Session>();
              foreach (var session in oldSyllabus.Sessions)
              {
                Session newSession = new Session();
                newSession.Name = session.Name;
                newSession.Index = session.Index;
                newSession.Status = session.Status;
                if (session.Units != null)
                {
                  var listUnit = new List<Unit>();
                  foreach (var unit in session.Units)
                  {
                    Unit newUnit = new Unit();
                    newUnit.Name = unit.Name;
                    newUnit.Index = unit.Index;
                    newUnit.Status = unit.Status;
                    if (unit.Lessons != null)
                    {
                      var listLesson = new List<Lesson>();
                      foreach (var lesson in unit.Lessons)
                      {
                        Lesson newLesson = new Lesson();
                        newLesson.Name = lesson.Name;
                        newLesson.Duration = lesson.Duration;
                        newLesson.IdDeliveryType = lesson.IdDeliveryType;
                        newLesson.IdFormatType = lesson.IdFormatType;
                        newLesson.IdOutputStandard = lesson.IdOutputStandard;
                        newLesson.Status = lesson.Status;
                        if (lesson.Materials != null)
                        {
                          var listMaterial = new List<Material>();
                          foreach (var material in lesson.Materials)
                          {
                            Material newMaterial = new Material();
                            newMaterial.Name = material.Name;
                            newMaterial.HyperLink = material.HyperLink;
                            newMaterial.Status = material.Status;
                            // Add history
                            newMaterial.HistoryMaterials = new List<HistoryMaterial>
                                                {
                                                    new HistoryMaterial
                                                    {
                                                        IdUser = classViewModel.CreatedBy.Value,
                                                        ModifiedOn = DateTime.Now,
                                                        Action = "Duplicate"
                                                    }
                                                };
                            listMaterial.Add(newMaterial);
                          }
                          newLesson.Materials = listMaterial;
                        }
                        listLesson.Add(newLesson);
                      }
                      newUnit.Lessons = listLesson;
                    }
                    listUnit.Add(newUnit);
                  }
                  newSession.Units = listUnit;
                }
                listSession.Add(newSession);
              }
              newSyllabus.Sessions = listSession;
            }
            _syllabusRepository.CreateSyllabus(newSyllabus);
            _unitOfWork.Commit();
            TPentity.Curricula = new List<Curriculum>();

            Curriculum curriculum = new Curriculum
            {
              IdSyllabus = newSyllabus.Id,
              NumberOrder = syllabus.numberOrder
            };
            listcurri.Add(curriculum);
            TPentity.Curricula = listcurri;
          }
          else throw new Exception($"Syllabus Id:{syllabus.idSyllabus} does not exit in system!");

        }
        _trainingProgramRepository.Create(TPentity);
        ClassEntities.IdProgram = TPentity.Id;

      }
      else
      {
        // duoi db
        List<Curriculum> oldlistcur = _curriculumRepository.GetCurriculum(classViewModel.IdProgram);
        foreach (var item in oldlistcur)
        {
          _curriculumRepository.DeleteCurriculum(item);
        }

        foreach (var syllabus in classViewModel.Syllabi)
        {
          var oldSyllabus = _syllabusRepository.GetById(syllabus.idSyllabus);
          if (oldSyllabus != null)
          {
            var newSyllabus = new Syllabus();
            newSyllabus.Name = oldSyllabus.Name + " (Copy)";
            newSyllabus.Description = oldSyllabus.Description;
            newSyllabus.Status = oldSyllabus.Status;
            newSyllabus.AssignmentSchema = new AssignmentSchema()
            {
              PercentQuiz = oldSyllabus.AssignmentSchema.PercentQuiz,
              PercentAssigment = oldSyllabus.AssignmentSchema.PercentAssigment,
              PercentFinal = oldSyllabus.AssignmentSchema.PercentFinal,
              PercentTheory = oldSyllabus.AssignmentSchema.PercentTheory,
              PercentFinalPractice = oldSyllabus.AssignmentSchema.PercentFinalPractice,
              PassingCriterial = oldSyllabus.AssignmentSchema.PassingCriterial
            };
            newSyllabus.Code = oldSyllabus.Code;
            newSyllabus.AttendeeNumber = oldSyllabus.AttendeeNumber;
            newSyllabus.Technicalrequirement = oldSyllabus.Technicalrequirement;
            newSyllabus.CourseObjectives = oldSyllabus.CourseObjectives;
            newSyllabus.TrainingPrinciple = oldSyllabus.TrainingPrinciple;
            newSyllabus.HyperLink = oldSyllabus.HyperLink;
            newSyllabus.IdLevel = oldSyllabus.IdLevel;
            newSyllabus.Level = oldSyllabus.Level;
            newSyllabus.Version = oldSyllabus.Version = 1.00F;
            // Add history
            newSyllabus.HistorySyllabi = new List<HistorySyllabus>
                  {
                      new HistorySyllabus
                      {
                          IdUser = classViewModel.CreatedBy.Value,
                          ModifiedOn = DateTime.Now,
                          Action = "Duplicate"
                      }
                  };
            // Add session, unit, lesson, material from old syllabus to new syllabus
            if (oldSyllabus.Sessions != null)
            {
              var listSession = new List<Session>();
              foreach (var session in oldSyllabus.Sessions)
              {
                Session newSession = new Session();
                newSession.Name = session.Name;
                newSession.Index = session.Index;
                newSession.Status = session.Status;
                if (session.Units != null)
                {
                  var listUnit = new List<Unit>();
                  foreach (var unit in session.Units)
                  {
                    Unit newUnit = new Unit();
                    newUnit.Name = unit.Name;
                    newUnit.Index = unit.Index;
                    newUnit.Status = unit.Status;
                    if (unit.Lessons != null)
                    {
                      var listLesson = new List<Lesson>();
                      foreach (var lesson in unit.Lessons)
                      {
                        Lesson newLesson = new Lesson();
                        newLesson.Name = lesson.Name;
                        newLesson.Duration = lesson.Duration;
                        newLesson.IdDeliveryType = lesson.IdDeliveryType;
                        newLesson.IdFormatType = lesson.IdFormatType;
                        newLesson.IdOutputStandard = lesson.IdOutputStandard;
                        newLesson.Status = lesson.Status;
                        if (lesson.Materials != null)
                        {
                          var listMaterial = new List<Material>();
                          foreach (var material in lesson.Materials)
                          {
                            Material newMaterial = new Material();
                            newMaterial.Name = material.Name;
                            newMaterial.HyperLink = material.HyperLink;
                            newMaterial.Status = material.Status;
                            // Add history
                            newMaterial.HistoryMaterials = new List<HistoryMaterial>
                                                  {
                                                      new HistoryMaterial
                                                      {
                                                          IdUser = classViewModel.CreatedBy.Value,
                                                          ModifiedOn = DateTime.Now,
                                                          Action = "Duplicate"
                                                      }
                                                  };
                            listMaterial.Add(newMaterial);
                          }
                          newLesson.Materials = listMaterial;
                        }
                        listLesson.Add(newLesson);
                      }
                      newUnit.Lessons = listLesson;
                    }
                    listUnit.Add(newUnit);
                  }
                  newSession.Units = listUnit;
                }
                listSession.Add(newSession);
              }
              newSyllabus.Sessions = listSession;
            }
            _syllabusRepository.CreateSyllabus(newSyllabus);
            _unitOfWork.Commit();

            var newcur = new Curriculum
            {
              IdProgram = classViewModel.IdProgram,
              IdSyllabus = newSyllabus.Id,
              NumberOrder = syllabus.numberOrder
            };
            _curriculumRepository.CreateCurriculum(newcur);

          }
          else throw new Exception($"Syllabus Id:{syllabus.idSyllabus} does not exit in system!");
        }
      }
      //Set Status = 2 = Reviewing
      ClassEntities.IdStatus = 2;
      ClassEntities.CreatedBy = classViewModel.CreatedBy.Value;
      ClassEntities.CreatedOn = DateTime.Now;
      //add IdProgram = IdProgram duplicate

      //Get Class site 
      var sitename = _classSiteRepository.GetById(classViewModel.IdSite);
      //Get Atendee Type
      var typename = _attendeeTypeRepository.GetById(classViewModel.IdAttendeeType);
      //Get Class Program Code
      var programName = _classProgramCodeRepository.GetById(classViewModel.IdProgramContent);
      //Set Class Code
      ClassEntities.ClassCode = sitename.Site + "_" + typename.Name + "_" + programName.ProgramCode + "_" + ClassEntities.ClassNumber;

      ClassEntities.ClassUpdateHistories = new List<ClassUpdateHistory>()
              {
                new ClassUpdateHistory{
                ModifyBy=ClassEntities.CreatedBy,
                 UpdateDate=DateTime.Now
                 }
               };


      _classRepository.UpdateClass(ClassEntities);

    }



    //GetDuration
    public int GetTrainingProgramDuration(long idSyllasbus)
    {
      int duration = 0;
      IEnumerable<Session> sessions = _sessionRepository.GetAllSessions();
      IEnumerable<Unit> units = _unitRepository.GetAllUnits();
      IEnumerable<Lesson> lessons = _lessonRepository.GetAllLessons();
      var sesss = sessions.Where(s => s.IdSyllabus == idSyllasbus);
      foreach (var se in sesss)
      {
        var uni = units.Where(s => s.IdSession == se.Id);
        foreach (var un in uni)
        {
          var less = lessons.Where(s => s.IdUnit == un.Id);
          foreach (var le in less)
          {
            duration = duration + le.Duration;
          }
        }
      }
      return duration;
    }

    //GetClassByClassCode

    public async Task<List<ClassSearchViewModel>> GetClassByCodeService(string? classCode)
    {
      var classByClassCode = await _classRepository.GetClassByClassCode(classCode);
      if (classByClassCode == null)
      {
        return null;
      }
      var result = classByClassCode?.Select(x => new ClassSearchViewModel()
      {
        Id = x.Id,
        Name = x.Name,
        ClassCode = x.ClassCode,
        Status = x.ClassStatus.Name,
        StartTimeLearning = x.StartTimeLearning,
        EndTimeLearing = x.EndTimeLearing,
        Location = x.Locations?.Select(x => x.Location.Name).ToList(),
        ReviewBy = x.ReviewedUser?.FullName,
        ReviewedOn = x.ReviewedOn,
        CreatedBy = x.CreatedUser?.FullName,
        CreatedOn = x.CreatedOn,
        Approve = x.ApprovedUser?.FullName,
        ApproveOn = x.ApprovedOn,
        PlannedAtendee = x.PlannedAtendee,
        ActualAttendee = x.ActualAttendee,
        AcceptedAttendee = x.AcceptedAttendee,
        ProgramCode = x.ClassProgramCode?.ProgramCode,
        TechnicalGroup = x.classTechnicalGroup?.Name,
        UniversityCode = x.ClassUniversityCode?.UniversityCode,
        FormatType = x.ClassFormatType?.Name,
        Site = x.ClassSite?.Site,
        ClassNumber = x.ClassNumber,
        ActiveDate = x.ClassSelectedDates?.Select(x => x.ActiveDate).ToList(),
        Mentor = x.ClassMentors?.Select(x => x.User.FullName).ToList(),
        Admin = x.ClassAdmins?.Select(x => x.User.FullName).ToList(),

        ClassFSU = new ClassFUSViewModel
        {
          ClassFSUContact = x.FsoftUnit?.FSUContactPoints?.Select(f => new ClassFSUContactViewModel
          {
            Contact = f.Contact,
            Id = f.Id,
          }).ToList(),
          Id = x.FsoftUnit.Id,
          Name = x.FsoftUnit.Name,

        },
        ClassTrainingProgram = new ClassTrainingProgamViewModel
        {
          Id = x.TrainingProgram?.Id,
          Duration = GetTrainingProgramDuration(x.TrainingProgram.Curricula.Select(x => x.Syllabus.Id).FirstOrDefault()),
          ModifiedBy = x.TrainingProgram.HistoryTrainingPrograms.Select(x => x?.User.FullName)?.FirstOrDefault(),
          Name = x.TrainingProgram?.Name,
          ModifiedOn = x.TrainingProgram?.HistoryTrainingPrograms.Select(x => x?.ModifiedOn).FirstOrDefault(),
          Status = Status(x.TrainingProgram.Status),
          Sysllabus = x.TrainingProgram?.Curricula?.Select(s => new ClassSyllabusViewModel
          {
            Id = s.IdSyllabus,
            Name = s.Syllabus.Name,
            Version = s.Syllabus.Version,
            Status = Status(s.Syllabus.Status),
            ModifiedOn = s.Syllabus.HistorySyllabi.Select(s => s.ModifiedOn).FirstOrDefault(),
            ClassSession = s.Syllabus.Sessions.Select(l => new ClassSessionViewModel()
            {
              Id = l.Id,
              Name = l.Name,
              Unit = l.Units.Select(u => new ClassUnitViewModel()
              {
                Id = u.Id,
                Name = u.Name,
                ClassLeson = u.Lessons.Select(les => new ClassLessonViewModel()
                {
                  Id = les.Id,
                  Duration = les.Duration,
                  Name = les.Name,
                  DeliveryType = les.DeliveryType.Name,
                  FormatType = les.FormatType.Name,
                  OutputStandard = les.OutputStandard.Name,
                })
              })
            })
          }).ToList(),

        },
        ClassAttendee = x.ClassTrainees?.Select(t => new ClassAttendeeViewModel()
        {
          Role = t.User.Role.Name,
          Id = t.User.IdRole,
          FullName = t.User.FullName,
          Image = t.User.Image
        }).ToList(),


      }).ToList();

      return result;
    }
    public string Status(int a)
    {

      switch (a)
      {
        case 0:
          return "Inactive";
        case 1:
          return "Active";
        case 2:
          return "Draft";
        case 3:
          return "Delete";
        default:
          return null;
      }
      // return null;

    }

    //GetTrainingProgramByName
    public async Task<List<ClassTrainingProgamViewModel>> GetClassTrainingProgams(string? name)
    {
      var result = await _trainingProgramRepository.Search(name);
      if (result == null)
      {
        return null;
      }
      var res = result.Select(x => new ClassTrainingProgamViewModel()
      {
        Id = x.Id,
        Duration = GetTrainingProgramDuration(x.Curricula.Select(x => x.Syllabus.Id).FirstOrDefault()),
        ModifiedBy = x.HistoryTrainingPrograms.Select(x => x.User.UserName).FirstOrDefault(),
        ModifiedOn = x.HistoryTrainingPrograms.Select(x => x.ModifiedOn).FirstOrDefault(),
        Status = Status(x.Status),
        Name = x.Name,
        Sysllabus = x.Curricula.Select(s => new ClassSyllabusViewModel()
        {
          Id = s.IdSyllabus,
          ModifiedOn = s.Syllabus.HistorySyllabi.Select(x => x.ModifiedOn).FirstOrDefault(),
          Name = s.Syllabus.Name,
          Status = Status(s.Syllabus.Status),
          Version = s.Syllabus.Version,
          ClassSession = s.Syllabus.Sessions.Select(l => new ClassSessionViewModel()
          {
            Id = l.Id,
            Name = l.Name,
            Unit = l.Units.Select(u => new ClassUnitViewModel()
            {
              Id = u.Id,
              Name = u.Name,
              ClassLeson = u.Lessons.Select(les => new ClassLessonViewModel()
              {
                Id = les.Id,
                Duration = les.Duration,
                Name = les.Name,
                DeliveryType = les.DeliveryType.Name,
                FormatType = les.FormatType.Name,
                OutputStandard = les.OutputStandard.Name,
              })
            })
          })

        }).ToList(),

      }).ToList();
      return res;

    }



        public async Task<bool> Delete(long id)
        {
            //getbyID
            var statusId = GetById(id);
            //check if status
            if (statusId.Status == 0 && statusId.IdStatus != 9)
            {
                throw new Exception("");
            }
            else
                //kep mess throw ex ("Mess")
                //Controller Try catch
                await _trainingProgramRepository.Delete(statusId.IdProgram);
            return await _classRepository.DeleteClass(id);
        }
        public ClassViewModel GetById(long Id)
        {
            ClassViewModel classes = null;
            classes = _mapper.Map<ClassViewModel>(_classRepository.GetById(Id));
            return classes;
        }
        public async Task<bool> DeActivate(long id)
        {
            return await _classRepository.DeActivate(id);
        }

    // public async Task EditClass(long classId)
    // {
    //   //List<ClassLocation> resultBeMap = await _classLocationRepository.GetByClassId(classId);
    //   //var result = new List<ObjectResponseModel>();
    //   //foreach(var item in resultBeMap)
    //   //{
    //   //    var resModel = new ObjectResponseModel
    //   //    {
    //   //        LocationName = item.Location.,
    //   //        ClassName = item.Class.,
    //   //        ClassAdmin = item.Class.ClassAdmins.Select(x => new ClassAdminViewModel
    //   //        {
    //   //            Id = x.IdClass,
    //   //            Image = x.User.Image.ToString(),

    //   //        }).ToList()
    //   //    }
    //   //    result.Add(resModel);
    //   //}

    // }

    public async Task<long> Duplicate(long id)
    {
      return await _classRepository.Duplicate(id);
    }

    public async Task<ClassDetailViewModel> GetDetail(long id)
    {
      var classDetail = await _classRepository.GetDetail(id);

            if (classDetail == new Class())
            {
                return new ClassDetailViewModel();
            }
            var result = new ClassDetailViewModel
            {
                Name = classDetail.Name,
                Status = classDetail.ClassStatus.Name,
                ActiveDate = classDetail.ClassSelectedDates.Select(x => x.ActiveDate).ToArray(),
                ApproveOn = classDetail.ApprovedOn,
                Approve = classDetail.ApprovedUser.UserName,
                ClassCode = classDetail.ClassCode,
                ClassFSU = new ClassFUSViewModel
                {
                    ClassFSUContact = classDetail.FsoftUnit.FSUContactPoints.Select(x => new ClassFSUContactViewModel
                    {
                        Contact = x.Contact,
                        Id = x.Id,
                    }).ToList(),
                    Id = classDetail.FsoftUnit.Id,
                    Name = classDetail.FsoftUnit.Name,

                }
          ,
                ClassNumber = classDetail.ClassNumber,
                ClassTrainingProgram = new ClassDetailTrainingViewModel
                {

                    Duration = GetDuration(classDetail.TrainingProgram.Curricula.Select(x => x.Syllabus).ToList()),
                    Id = classDetail.TrainingProgram.Id,
                    ModifiedBy = classDetail.TrainingProgram.HistoryTrainingPrograms
              .Select(x => x.User.UserName).Last(),
                    ModifiedOn = classDetail.TrainingProgram.HistoryTrainingPrograms
              .Select(x => x.ModifiedOn).Last(),
                    Name = classDetail.TrainingProgram.Name,
                    Status = classDetail.TrainingProgram.Status,
                    Syllabus = classDetail.TrainingProgram.Curricula.Select(x => new ClassDetailSyllabusViewModel
                    {
                        Name = x.Syllabus.Name,
                        Id = x.Syllabus.Id,
                        Status = x.Syllabus.Status,
                        ModifiedOn = x.Syllabus.HistorySyllabi.First().ModifiedOn,
                        Version = x.Syllabus.Version,
                        ModifiedBy = x.Syllabus.HistorySyllabi.First().User.UserName,
                        Duration = GetDuration(x.TrainingProgram.Curricula.Select(x => x.Syllabus).ToList())
                    }).ToList(),
                },
                CreatedBy = classDetail.CreatedUser.UserName,
                CreatedOn = classDetail.CreatedOn,
                ReviewOn = classDetail.ReviewedOn,
                EndTimeLearing = classDetail.EndTimeLearing,
                FormatType = classDetail.ClassFormatType.Name,
                Id = classDetail.Id,
                Locations = classDetail.Locations.Select(x => x.Location.Name).ToList(),
                ProgramCode = classDetail.ClassProgramCode.ProgramCode,
                ReviewBy = classDetail.ReviewedUser.UserName,
                Site = classDetail.ClassSite.Site,
                StartTimeLearning = classDetail.StartTimeLearning,
                TechnicalGroup = classDetail.classTechnicalGroup.Name,
                Trainer = classDetail.ClassTrainees.Select(x => x.User.UserName).ToList(),
                UniversityCode = classDetail.ClassUniversityCode.UniversityCode,


      };
      return result;
    }


    private int GetDuration(List<Syllabus> syllabuses)
    {
      var result = 0;
      foreach (var syllabus in syllabuses)
      {
        if (syllabus != null)
        {
          foreach (var ses in syllabus.Sessions)
          {
            if (ses != null)
            {
              foreach (var unit in ses.Units)
              {
                if (unit != null)
                {
                  foreach (var less in unit.Lessons)
                  {
                    result += less.Duration;
                  }
                }
              }
            }
          }
        }
      }
      return result;
    }
    public async Task<List<ClassAttendeeViewModel>> GetClassAttendee(long idClass, int PageNumber, int PageSize)
    {
      #region GetListAttendee
      var classAttendee = await _classRepository.GetAttende(idClass);
      if (classAttendee == new Class())
      {
        return new List<ClassAttendeeViewModel>();
      }
      var result = new List<ClassAttendeeViewModel>();

            classAttendee.ClassTrainees.ToList().ForEach(x => result.Add(new ClassAttendeeViewModel
            {

                Role = x.User.Role.Name,
                FullName = x.User.FullName,
                Id = x.User.ID,
                Image = x.User.Image
            }));
            classAttendee.ClassMentors.ToList().ForEach(x =>
            {
                var attendee = new ClassAttendeeViewModel
                {
                    Role = x.User.Role.Name,
                    FullName = x.User.FullName,
                    Id = x.User.ID,
                    Image = x.User.Image
                };
                if (!result.Contains(attendee))
                {
                    result.Add(attendee);
                }
            }
            );
            classAttendee.ClassAdmins.ToList().ForEach(x =>
            {
                var attendee = new ClassAttendeeViewModel
                {
                    Role = x.User.Role.Name,
                    FullName = x.User.FullName,
                    Id = x.User.ID,
                    Image = x.User.Image
                };
                if (!result.Contains(attendee))
                {
                    result.Add(attendee);
                }
            });
            #endregion
            #region Pagination
            int CurrentPage = PageNumber, TotalPage = PageSize;
            int count = result.Count();
            int TotalCount = count;

      // Calculating Totalpage by Dividing (No of Records / Pagesize)  
      int TotalPages = (int)Math.Ceiling(count / (double)PageSize);

      return result.OrderByDescending(x => x.Id).Skip((CurrentPage - 1) * PageSize).Take(PageSize).ToList();
      #endregion

    }

        public async Task<ClassDetailTrainingViewModel> GetTrainingProgram(long id)
        {
            var classDetail = await _classRepository.GetDetail(id);

            if (classDetail == new Class())
                return new ClassDetailTrainingViewModel();

            return new ClassDetailTrainingViewModel
            {
                Id = classDetail.TrainingProgram.Id,
                ModifiedBy = classDetail.TrainingProgram.HistoryTrainingPrograms
                    .Select(x => x.User.UserName).Last(),
                ModifiedOn = classDetail.TrainingProgram.HistoryTrainingPrograms
                    .Select(x => x.ModifiedOn).Last(),
                Name = classDetail.TrainingProgram.Name,
                Status = classDetail.TrainingProgram.Status,
                Duration = GetDuration(classDetail.TrainingProgram.Curricula.Select(x => x.Syllabus).ToList()),
                Syllabus = classDetail.TrainingProgram.Curricula.Select(x => new ClassDetailSyllabusViewModel
                {
                    Name = x.Syllabus.Name,
                    Id = x.Syllabus.Id,
                    Status = x.Syllabus.Status,
                    ModifiedOn = x.Syllabus.HistorySyllabi.Single().ModifiedOn,
                    Version = x.Syllabus.Version,
                    ModifiedBy = x.Syllabus.HistorySyllabi.Single().User.UserName,
                    Duration = GetDuration(x.TrainingProgram.Curricula.Select(x => x.Syllabus).ToList())

                }).ToList(),
            };

    }

    public IEnumerable<ClassViewModel> GetClassFilter(Class @class)
    {
      return _mapper.Map<List<ClassViewModel>>(_classRepository.GetWithFilter(@class));
    }

    public IEnumerable<StudentClassViewModel> GetStudentClass(Class @class)
    {
      return _mapper.Map<List<StudentClassViewModel>>(_classRepository.GetWithFilter(@class));
    }

    public async Task<ClassCalenderViewModel> GetClassCalender(long idClass)
    {

      Class @class = await _classRepository.GetClassById(idClass);
      if (@class != null)
      {
        ClassCalenderViewModel classCalenderViewModel = _mapper.Map<ClassCalenderViewModel>(@class);
        classCalenderViewModel.Locations = _mapper.Map<IEnumerable<ClassLocationViewModel>>(@class.Locations);
        classCalenderViewModel.ClassAdmins = _mapper.Map<IEnumerable<AdminViewModel>>(@class.ClassAdmins);
        classCalenderViewModel.ClassMentors = _mapper.Map<IEnumerable<TrainerViewModel>>(@class.ClassMentors);
        return classCalenderViewModel;
      }
      throw new Exception("The class's Id does not exist in the system.");
    }


    public async Task SaveAsDraft(UpdateClassViewModel classViewModel)
    {
      var TPofClass = _classRepository.GetById(classViewModel.Id);
      if (TPofClass == null) { throw new Exception($"Class ID: {TPofClass.Id} does not exit in the system!!!"); }
      var ClassEntities = _mapper.Map<Class>(classViewModel);

      List<ClassSelectedDate> listActiveDate = new List<ClassSelectedDate>();
      //Add list active date in class
      foreach (var date in classViewModel.ActiveDate)
      {
        var newClassSelectedDate = new ClassSelectedDate
        {
          IdClass = classViewModel.Id,
          ActiveDate = date,
          Status = 1
        };
        // _classSelectedDateRepository.AddSeletedDate(newClassSelectedDate);
        listActiveDate.Add(newClassSelectedDate);
      }
      ClassEntities.ClassSelectedDates = listActiveDate;

      //Get list trainee in class
      List<ClassTrainee> listtrainee = _classTraineeRepository.GetCLassTrainee(classViewModel.Id);
      //Delete new admin in class
      foreach (var oldtrainee in listtrainee)
      {
        _classTraineeRepository.DeleteTrainee(oldtrainee);
      }
      //Add new trainee in class
      foreach (var newtrainee in classViewModel.IdTrainee)
      {
        User checkUser = await _userRepository.GetById(newtrainee);
        if (checkUser == null)
        {
          throw new Exception($"User ID {newtrainee} does not exist in the system!");
        }
        var newClassTrainee = new ClassTrainee
        {
          IdClass = classViewModel.Id,
          IdUser = newtrainee
        };
        _classTraineeRepository.AddTrainee(newClassTrainee);
      }

      //Get list Mentor in class
      List<ClassMentor> listmentor = _classMentorRepository.GetCLassMentor(classViewModel.Id);
      //Delete ole Mentor in class
      foreach (var oldmentor in listmentor)
      {
        _classMentorRepository.DeleteMentor(oldmentor);
      }
      //Add new Mentor in class
      foreach (var newmentor in classViewModel.IdMentor)
      {
        User checkUser = await _userRepository.GetById(newmentor);
        if (checkUser == null)
        {
          throw new Exception($"User ID {newmentor} does not exist in the system!");
        }
        var newClassMentor = new ClassMentor
        {
          IdClass = classViewModel.Id,
          IdUser = newmentor
        };
        _classMentorRepository.AddMentor(newClassMentor);
      }

      //Get list Admin in class
      List<ClassAdmin> listAdmin = _classAdminRepository.GetCLassAdmin(classViewModel.Id);
      //Delete old Admin in class
      foreach (var oldadmin in listAdmin)
      {
        _classAdminRepository.DeleteAdmin(oldadmin);
      }
      //Add new admin in class
      foreach (var newadmin in classViewModel.IdAdmin)
      {
        User checkUser = await _userRepository.GetById(newadmin);
        if (checkUser == null)
        {
          throw new Exception($"User ID {newadmin} does not exist in the system!");
        }
        var newClassAdmin = new ClassAdmin
        {
          IdClass = classViewModel.Id,
          IdUser = newadmin
        };
        _classAdminRepository.AddAdmin(newClassAdmin);
      }

      List<ClassLocation> listLocation = await _classLocationRepository.GetByClassId(classViewModel.Id);
      foreach (var oldLocation in listLocation)
      {
        _classLocationRepository.DeleteLocation(oldLocation);
      }
      //Add Class location
      foreach (var location in classViewModel.IdLocation)
      {
        Location checkLocation = _locationRepository.GetById(location);
        if (checkLocation == null)
        {
          throw new Exception($"Location ID {location} does not exist in the system!");
        }
        var locations = new ClassLocation
        {
          IdClass = classViewModel.Id,
          IdLocation = location
        };
        _classLocationRepository.AddLocation(locations);
      }
      _unitOfWork.Commit();


      if (TPofClass.IdProgram == null || TPofClass.IdProgram != classViewModel.IdProgram)
      {
        //Get entity old Training Program by id
        var oldTP = _trainingProgramRepository.GetbyId(classViewModel.IdProgram);
        if (oldTP == null || classViewModel.IdProgram != oldTP.Id)
        { throw new Exception($"Training Program Id: {classViewModel.IdProgram} does not exit in system"); };
        // new Training program view model
        ClassTrainingProgamViewModel newTP = new ClassTrainingProgamViewModel();
        // mapentity old training program to new training program view model
        newTP = _mapper.Map<ClassTrainingProgamViewModel>(oldTP);
        newTP.Id = null;
        newTP.Name = oldTP.Name + "(Coppy)";

        //map new training program view model to entity training program
        var TPentity = _mapper.Map<TrainingProgram>(newTP);

        TPentity.HistoryTrainingPrograms = new List<HistoryTrainingProgram>();
        List<HistoryTrainingProgram> listhistory = new List<HistoryTrainingProgram>();
        //add element old history to list history
        foreach (var item in oldTP.HistoryTrainingPrograms)
        {
          var newhtr = new HistoryTrainingProgram
          {
            IdUser = item.IdUser,
            ModifiedOn = item.ModifiedOn
          };
          listhistory.Add(newhtr);
        }
        TPentity.HistoryTrainingPrograms = listhistory;


        List<Curriculum> listcurri = new List<Curriculum>();

        //duplicate syllabus
        foreach (var syllabus in classViewModel.Syllabi)
        {
          var oldSyllabus = _syllabusRepository.GetById(syllabus.idSyllabus);
          if (oldSyllabus != null)
          {
            var newSyllabus = new Syllabus();
            newSyllabus.Name = oldSyllabus.Name + " (Copy)";
            newSyllabus.Description = oldSyllabus.Description;
            newSyllabus.Status = oldSyllabus.Status;
            newSyllabus.AssignmentSchema = new AssignmentSchema()
            {
              PercentQuiz = oldSyllabus.AssignmentSchema.PercentQuiz,
              PercentAssigment = oldSyllabus.AssignmentSchema.PercentAssigment,
              PercentFinal = oldSyllabus.AssignmentSchema.PercentFinal,
              PercentTheory = oldSyllabus.AssignmentSchema.PercentTheory,
              PercentFinalPractice = oldSyllabus.AssignmentSchema.PercentFinalPractice,
              PassingCriterial = oldSyllabus.AssignmentSchema.PassingCriterial
            };
            newSyllabus.Code = oldSyllabus.Code;
            newSyllabus.AttendeeNumber = oldSyllabus.AttendeeNumber;
            newSyllabus.Technicalrequirement = oldSyllabus.Technicalrequirement;
            newSyllabus.CourseObjectives = oldSyllabus.CourseObjectives;
            newSyllabus.TrainingPrinciple = oldSyllabus.TrainingPrinciple;
            newSyllabus.HyperLink = oldSyllabus.HyperLink;
            newSyllabus.IdLevel = oldSyllabus.IdLevel;
            newSyllabus.Level = oldSyllabus.Level;
            newSyllabus.Version = oldSyllabus.Version = 1.00F;
            // Add history
            newSyllabus.HistorySyllabi = new List<HistorySyllabus>
                {
                    new HistorySyllabus
                    {
                        IdUser = classViewModel.CreatedBy.Value,
                        ModifiedOn = DateTime.Now,
                        Action = "Duplicate"
                    }
                };
            // Add session, unit, lesson, material from old syllabus to new syllabus
            if (oldSyllabus.Sessions != null)
            {
              var listSession = new List<Session>();
              foreach (var session in oldSyllabus.Sessions)
              {
                Session newSession = new Session();
                newSession.Name = session.Name;
                newSession.Index = session.Index;
                newSession.Status = session.Status;
                if (session.Units != null)
                {
                  var listUnit = new List<Unit>();
                  foreach (var unit in session.Units)
                  {
                    Unit newUnit = new Unit();
                    newUnit.Name = unit.Name;
                    newUnit.Index = unit.Index;
                    newUnit.Status = unit.Status;
                    if (unit.Lessons != null)
                    {
                      var listLesson = new List<Lesson>();
                      foreach (var lesson in unit.Lessons)
                      {
                        Lesson newLesson = new Lesson();
                        newLesson.Name = lesson.Name;
                        newLesson.Duration = lesson.Duration;
                        newLesson.IdDeliveryType = lesson.IdDeliveryType;
                        newLesson.IdFormatType = lesson.IdFormatType;
                        newLesson.IdOutputStandard = lesson.IdOutputStandard;
                        newLesson.Status = lesson.Status;
                        if (lesson.Materials != null)
                        {
                          var listMaterial = new List<Material>();
                          foreach (var material in lesson.Materials)
                          {
                            Material newMaterial = new Material();
                            newMaterial.Name = material.Name;
                            newMaterial.HyperLink = material.HyperLink;
                            newMaterial.Status = material.Status;
                            // Add history
                            newMaterial.HistoryMaterials = new List<HistoryMaterial>
                                                {
                                                    new HistoryMaterial
                                                    {
                                                        IdUser = classViewModel.CreatedBy.Value,
                                                        ModifiedOn = DateTime.Now,
                                                        Action = "Duplicate"
                                                    }
                                                };
                            listMaterial.Add(newMaterial);
                          }
                          newLesson.Materials = listMaterial;
                        }
                        listLesson.Add(newLesson);
                      }
                      newUnit.Lessons = listLesson;
                    }
                    listUnit.Add(newUnit);
                  }
                  newSession.Units = listUnit;
                }
                listSession.Add(newSession);
              }
              newSyllabus.Sessions = listSession;
            }
            _syllabusRepository.CreateSyllabus(newSyllabus);
            _unitOfWork.Commit();
            TPentity.Curricula = new List<Curriculum>();

            Curriculum curriculum = new Curriculum
            {
              IdSyllabus = newSyllabus.Id,
              NumberOrder = syllabus.numberOrder
            };
            listcurri.Add(curriculum);
            TPentity.Curricula = listcurri;
          }
          else throw new Exception($"Syllabus Id:{syllabus.idSyllabus} does not exit in system!");

        }
        _trainingProgramRepository.Create(TPentity);
        ClassEntities.IdProgram = TPentity.Id;

      }
      else
      {
        // duoi db
        List<Curriculum> oldlistcur = _curriculumRepository.GetCurriculum(classViewModel.IdProgram);
        foreach (var item in oldlistcur)
        {
          _curriculumRepository.DeleteCurriculum(item);
        }

        foreach (var syllabus in classViewModel.Syllabi)
        {
          var oldSyllabus = _syllabusRepository.GetById(syllabus.idSyllabus);
          if (oldSyllabus != null)
          {
            var newSyllabus = new Syllabus();
            newSyllabus.Name = oldSyllabus.Name + " (Copy)";
            newSyllabus.Description = oldSyllabus.Description;
            newSyllabus.Status = oldSyllabus.Status;
            newSyllabus.AssignmentSchema = new AssignmentSchema()
            {
              PercentQuiz = oldSyllabus.AssignmentSchema.PercentQuiz,
              PercentAssigment = oldSyllabus.AssignmentSchema.PercentAssigment,
              PercentFinal = oldSyllabus.AssignmentSchema.PercentFinal,
              PercentTheory = oldSyllabus.AssignmentSchema.PercentTheory,
              PercentFinalPractice = oldSyllabus.AssignmentSchema.PercentFinalPractice,
              PassingCriterial = oldSyllabus.AssignmentSchema.PassingCriterial
            };
            newSyllabus.Code = oldSyllabus.Code;
            newSyllabus.AttendeeNumber = oldSyllabus.AttendeeNumber;
            newSyllabus.Technicalrequirement = oldSyllabus.Technicalrequirement;
            newSyllabus.CourseObjectives = oldSyllabus.CourseObjectives;
            newSyllabus.TrainingPrinciple = oldSyllabus.TrainingPrinciple;
            newSyllabus.HyperLink = oldSyllabus.HyperLink;
            newSyllabus.IdLevel = oldSyllabus.IdLevel;
            newSyllabus.Level = oldSyllabus.Level;
            newSyllabus.Version = oldSyllabus.Version = 1.00F;
            // Add history
            newSyllabus.HistorySyllabi = new List<HistorySyllabus>
                  {
                      new HistorySyllabus
                      {
                          IdUser = classViewModel.CreatedBy.Value,
                          ModifiedOn = DateTime.Now,
                          Action = "Duplicate"
                      }
                  };
            // Add session, unit, lesson, material from old syllabus to new syllabus
            if (oldSyllabus.Sessions != null)
            {
              var listSession = new List<Session>();
              foreach (var session in oldSyllabus.Sessions)
              {
                Session newSession = new Session();
                newSession.Name = session.Name;
                newSession.Index = session.Index;
                newSession.Status = session.Status;
                if (session.Units != null)
                {
                  var listUnit = new List<Unit>();
                  foreach (var unit in session.Units)
                  {
                    Unit newUnit = new Unit();
                    newUnit.Name = unit.Name;
                    newUnit.Index = unit.Index;
                    newUnit.Status = unit.Status;
                    if (unit.Lessons != null)
                    {
                      var listLesson = new List<Lesson>();
                      foreach (var lesson in unit.Lessons)
                      {
                        Lesson newLesson = new Lesson();
                        newLesson.Name = lesson.Name;
                        newLesson.Duration = lesson.Duration;
                        newLesson.IdDeliveryType = lesson.IdDeliveryType;
                        newLesson.IdFormatType = lesson.IdFormatType;
                        newLesson.IdOutputStandard = lesson.IdOutputStandard;
                        newLesson.Status = lesson.Status;
                        if (lesson.Materials != null)
                        {
                          var listMaterial = new List<Material>();
                          foreach (var material in lesson.Materials)
                          {
                            Material newMaterial = new Material();
                            newMaterial.Name = material.Name;
                            newMaterial.HyperLink = material.HyperLink;
                            newMaterial.Status = material.Status;
                            // Add history
                            newMaterial.HistoryMaterials = new List<HistoryMaterial>
                                                  {
                                                      new HistoryMaterial
                                                      {
                                                          IdUser = classViewModel.CreatedBy.Value,
                                                          ModifiedOn = DateTime.Now,
                                                          Action = "Duplicate"
                                                      }
                                                  };
                            listMaterial.Add(newMaterial);
                          }
                          newLesson.Materials = listMaterial;
                        }
                        listLesson.Add(newLesson);
                      }
                      newUnit.Lessons = listLesson;
                    }
                    listUnit.Add(newUnit);
                  }
                  newSession.Units = listUnit;
                }
                listSession.Add(newSession);
              }
              newSyllabus.Sessions = listSession;
            }
            _syllabusRepository.CreateSyllabus(newSyllabus);
            _unitOfWork.Commit();

            var newcur = new Curriculum
            {
              IdProgram = classViewModel.IdProgram,
              IdSyllabus = newSyllabus.Id,
              NumberOrder = syllabus.numberOrder
            };
            _curriculumRepository.CreateCurriculum(newcur);

          }
          else throw new Exception($"Syllabus Id:{syllabus.idSyllabus} does not exit in system!");
        }
      }
      //Set Status = 1 = Draft
      ClassEntities.IdStatus = 1;
      ClassEntities.CreatedBy = classViewModel.CreatedBy.Value;
      ClassEntities.CreatedOn = DateTime.Now;


      //Get Class site 
      var sitename = _classSiteRepository.GetById(classViewModel.IdSite);
      //Get Atendee Type
      var typename = _attendeeTypeRepository.GetById(classViewModel.IdAttendeeType);
      //Get Class Program Code
      var programName = _classProgramCodeRepository.GetById(classViewModel.IdProgramContent);
      //Set Class Code
      ClassEntities.ClassCode = sitename.Site + "_" + typename.Name + "_" + programName.ProgramCode + "_" + ClassEntities.ClassNumber;

      ClassEntities.ClassUpdateHistories = new List<ClassUpdateHistory>()
              {
                new ClassUpdateHistory{
                ModifyBy=ClassEntities.CreatedBy,
                 UpdateDate=DateTime.Now
                 }
               };


      _classRepository.UpdateClass(ClassEntities);
    }
    public void Save()
    {
      _unitOfWork.Commit();
    }

        public List<Class> GetClassess(List<string>? key, List<string>? sortBy, List<long> location, DateTime? classTimeFrom, DateTime? classTimeTo, List<long> classTime, List<long> status, List<long> attendee, int FSU, int trainer, int pageNumber, int pageSize)
        {
            var classes = _classRepository.GetClassess(key, location, classTimeFrom, classTimeTo, classTime, status, attendee, FSU, trainer);
            // paging
            classes = classes.Skip((pageNumber - 1) * pageSize).Take(pageSize);

            // sort
            //if (!string.IsNullOrEmpty(sortBy))
            //{
            //    classes = classes.OrderBy(sy => sy.Name).ToList();
            //    switch (sortBy)
            //    {

            //        case "tens_desc": classes = classes.OrderByDescending(sy => sy.Name).ToList(); break;
            //        case "code_desc": classes = classes.OrderByDescending(sy => sy.ClassCode).ToList(); break;
            //        case "tens_asc": classes = classes.OrderBy(sy => sy.Name).ToList(); break;
            //        case "code_asc": classes = classes.OrderBy(sy => sy.ClassCode).ToList(); break;

            //    }
            //}
            foreach (var item in sortBy)
            {
                classes = classes.AsQueryable().OrderBy(item).ToList();
            }
            return classes.ToList();
        }

    public List<ClassModel> ShowClasses(List<Class> classes)
    {
      List<ClassModel> classesList = new List<ClassModel>();
      foreach (var c in classes)
      {
        string className = c.Name;
        string classCode = c.ClassCode;
        DateTime createdOn = c.CreatedOn;
        string createdBy = _userRepository.GetUser(c.CreatedBy).UserName;
        string attendee = _attendeeTypeRepository.GetById(c.IdAttendeeType).Name;
        string location = _classSiteRepository.GetById(c.IdSite).Site;
        string FSU = _fsoftUnitRepository.GetById(c.IdFSU).Name;
        int duration = GetClassDuration(c);
        classesList.Add(new ClassModel { ClassName = className, ClassCode = classCode, CreatedOn = createdOn, CreatedBy = createdBy, Attendee = attendee, Location = location, FSU = FSU, Duration = duration });
      }

      return classesList;
    }

    public int CountClass(List<string>? key, List<long> location, DateTime? classTimeFrom, DateTime? classTimeTo, List<long> classTime, List<long> status, List<long> attendee, int FSU, int trainer)
    {
      var classes = _classRepository.GetClassess(key, location, classTimeFrom, classTimeTo, classTime, status, attendee, FSU, trainer);
      return classes.Count();
    }

    public int GetClassDuration(Class classes)
    {
      int result = 0;
      if (classes.TrainingProgram != null)
      {

        if (classes.TrainingProgram.Curricula != null)
        {


          foreach (var item in classes.TrainingProgram.Curricula)
          {
            result = result + GetDuration(item.Syllabus);
          }
        }
      }
      return result;
    }


    public int GetDuration(Syllabus syl)
    {
      int a = 0;
      var se = _sessionRepository.GetSyllabusSession(syl.Id);
      foreach (var session in se)
      {
        var un = _unitRepository.GetSessionUnits(session.Id);
        foreach (var unit in un)
        {
          var les = _lessonRepository.GetUnitLessons(unit.Id);
          foreach (var lession in les)
          {
            a = a + lession.Duration;
          }
        }
      }
      return a;
    }

        #region bhhiep
        public IEnumerable<Class> GetClasses()
        {
            return _classRepository.GetClassesQuery().Include(c => c.AttendeeType)
                                   .Include(c => c.Locations).ThenInclude(x => x.Location).Include(c => c.ClassStatus);
        }

    public async Task<Class.ImportClassResponse> ImportCLasses(Class.ImportClassRequest request, string path)
    {
      ImportClassResponse response = new ImportClassResponse();
      var classes = new List<Class>();
      response.IsSuccess = true;
      response.Message = "Successful";
      try
      {
        if (request.File.FileName.ToLower().Contains(".xlsx"))
        {
          FileStream stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
          System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
          IExcelDataReader reader = ExcelReaderFactory.CreateReader(stream);
          DataSet dataset = reader.AsDataSet(
              configuration: new ExcelDataSetConfiguration()
              {
                UseColumnDataType = false,
                ConfigureDataTable = (tableReader) => new ExcelDataTableConfiguration()
                {
                  UseHeaderRow = true

                }
              });
          string _classAdmin = "";
          string _classMentor = "";
          string _classMentor2 = "";
          string _classLocation = "";
          for (int i = 0; i < dataset.Tables[0].Rows.Count; i++)
          {
            Class _class = new Class();

            //fix lai kieu convert

            _class.Name = dataset.Tables[0].Rows[i].ItemArray[3] != null ? Convert.ToString(dataset.Tables[0].Rows[i].ItemArray[3]).ToString() : "-1";
            _class.ClassCode = dataset.Tables[0].Rows[i].ItemArray[3] != null ? Convert.ToString(dataset.Tables[0].Rows[i].ItemArray[3]).ToString() : "-1";

            string _createdBy = dataset.Tables[0].Rows[i].ItemArray[13] != null ? Convert.ToString(dataset.Tables[0].Rows[i].ItemArray[13]).ToString() : "-1";
            var users = _userRepository.GetUsersForImport();
            bool flag = true;
            foreach (var item in users)
            {
              if (item.FullName == _createdBy)
              {
                _class.CreatedBy = item.ID;
                flag = false; break;
              }
            }
            if (flag)
            {
              User us = new User { UserName = _createdBy, Password = "123", FullName = _createdBy, DateOfBirth = DateTime.Parse("01-01-0001"), Gender = Char.Parse("M"), Phone = "", Email = "", Address = "", Status = 1, IdRole = 1 };
              _userRepository.CreateUserForImport(us);
              users = _userRepository.GetUsersForImport();
              _class.CreatedBy = users.Max(s => s.ID);
            }

            string _trainingProgram = dataset.Tables[0].Rows[i].ItemArray[11] != null ? Convert.ToString(dataset.Tables[0].Rows[i].ItemArray[11]).ToString() : "-1";
            var trainingProgram = _trainingProgramRepository.GetAllForImport();
            bool flag2 = true;
            foreach (var item in trainingProgram)
            {
              if (item.Name == _trainingProgram)
              {
                _class.IdProgram = item.Id;
                flag2 = false; break;
              }
            }
            if (flag2)
            {
              _trainingProgramRepository.AddForImport(_trainingProgram, 1);
              trainingProgram = _trainingProgramRepository.GetAllForImport();
              _class.IdProgram = trainingProgram.Max(s => s.Id);
            }

            _class.CreatedOn = DateTime.Today;
            _class.ClassNumber = dataset.Tables[0].Rows[i].ItemArray[2] != null ? Convert.ToInt32(dataset.Tables[0].Rows[i].ItemArray[2]) : -1;
            _class.StartYear = dataset.Tables[0].Rows[i].ItemArray[32] != null ? Convert.ToInt32(dataset.Tables[0].Rows[i].ItemArray[32]) : -1;
            _class.StartDate = dataset.Tables[0].Rows[i].ItemArray[15] != null ? DateTime.Parse(dataset.Tables[0].Rows[i].ItemArray[15].ToString()) : null;
            _class.EndDate = dataset.Tables[0].Rows[i].ItemArray[16] != null ? DateTime.Parse(dataset.Tables[0].Rows[i].ItemArray[16].ToString()) : null;

            string _status = dataset.Tables[0].Rows[i].ItemArray[4] != null ? Convert.ToString(dataset.Tables[0].Rows[i].ItemArray[4]).ToString() : "-1";
            var classStatus = _classStatusRepository.GetAll();
            bool _flag = true;
            foreach (var item in classStatus)
            {
              if (item.Name.Equals(_status))
              {
                _class.Status = Convert.ToInt32(item.Id);
                _class.IdStatus = item.Id;
                _flag = false; break;
              }
            }
            if (_flag)
            {
              ClassStatus status = new ClassStatus { Name = _status };
              _classStatusRepository.CreateClassStatusForImport(status);
              classStatus = _classStatusRepository.GetAll();
              _class.Status = Convert.ToInt32(classStatus.Max(s => s.Id));
            }

            string _FSU = dataset.Tables[0].Rows[i].ItemArray[8] != null ? Convert.ToString(dataset.Tables[0].Rows[i].ItemArray[8]).ToString() : "-1";
            var fsoftUnit = _fsoftUnitRepository.GetAll();
            bool flag6 = true;
            foreach (var item in fsoftUnit)
            {
              if (item.Name.Equals(_FSU))
              {
                _class.IdFSU = item.Id;
                flag6 = false; break;
              }
            }
            if (flag6)
            {
              FsoftUnit fsoftU = new FsoftUnit { Name = _FSU, Status = 1 };
              _fsoftUnitRepository.CreateFSUForImport(fsoftU);
              fsoftUnit = _fsoftUnitRepository.GetAll();
              _class.IdFSU = Convert.ToInt32(fsoftUnit.Max(s => s.Id));
            }

            string _university = dataset.Tables[0].Rows[i].ItemArray[9] != null ? Convert.ToString(dataset.Tables[0].Rows[i].ItemArray[9]).ToString() : "-1";
            var uniCode = _classUniversityCodeRepository.GetAll();
            bool flag7 = true;
            foreach (var item in uniCode)
            {
              if (item.UniversityCode.Equals(_university))
              {
                _class.IdUniversity = item.Id;
                flag7 = false; break;
              }
            }
            if (flag7)
            {
              ClassUniversityCode classUniversityCode = new ClassUniversityCode { UniversityCode = _university };
              _classUniversityCodeRepository.CreateUniCodeForImport(classUniversityCode);
              uniCode = _classUniversityCodeRepository.GetAll();
              _class.IdUniversity = Convert.ToInt32(uniCode.Max(s => s.Id));
            }

            string _technicalGroup = dataset.Tables[0].Rows[i].ItemArray[10] != null ? Convert.ToString(dataset.Tables[0].Rows[i].ItemArray[10]).ToString() : "-1";
            var technical = _classTechnicalGroupRepository.GetAll();
            bool flag8 = true;
            foreach (var item in technical)
            {
              if (item.Name.Equals(_technicalGroup))
              {
                _class.IdTechnicalGroup = item.Id;
                flag8 = false; break;
              }
            }
            if (flag8)
            {
              ClassTechnicalGroup classTechnicalGroup = new ClassTechnicalGroup { Name = _technicalGroup };
              _classTechnicalGroupRepository.CreateTecniGroupForImport(classTechnicalGroup);
              technical = _classTechnicalGroupRepository.GetAll();
              _class.IdTechnicalGroup = Convert.ToInt32(technical.Max(s => s.Id));
            }

            string _site = dataset.Tables[0].Rows[i].ItemArray[1] != null ? Convert.ToString(dataset.Tables[0].Rows[i].ItemArray[1]).ToString() : "-1";
            var classSite = _classSiteRepository.GetAll();
            bool flag9 = true;
            foreach (var item in classSite)
            {
              if (item.Site.Equals(_site))
              {
                _class.IdSite = item.Id;
                flag9 = false; break;
              }
            }
            if (flag9)
            {
              ClassSite site = new ClassSite { Site = _site };
              _classSiteRepository.CreateClassSiteForImport(site);
              classSite = _classSiteRepository.GetAll();
              _class.IdSite = Convert.ToInt32(classSite.Max(s => s.Id));
            }

            string _formatType = dataset.Tables[0].Rows[i].ItemArray[7] != null ? Convert.ToString(dataset.Tables[0].Rows[i].ItemArray[7]).ToString() : "-1";
            var classFormat = _classFormatTypeRepository.GetAll();
            bool flag10 = true;
            foreach (var item in classFormat)
            {
              if (item.Name.Equals(_formatType))
              {
                _class.IdFormatType = item.Id;
                flag10 = false; break;
              }
            }
            if (flag10)
            {
              ClassFormatType classFormatType = new ClassFormatType { Name = _formatType };
              _classFormatTypeRepository.CreateFormatTypeForImport(classFormatType);
              classFormat = _classFormatTypeRepository.GetAll();
              _class.IdFormatType = Convert.ToInt32(classFormat.Max(s => s.Id));
            }

            string _programCode = dataset.Tables[0].Rows[i].ItemArray[12] != null ? Convert.ToString(dataset.Tables[0].Rows[i].ItemArray[12]).ToString() : "-1";
            var classProgram = _classProgramCodeRepository.GetAll();
            bool flag11 = true;
            foreach (var item in classProgram)
            {
              if (item.ProgramCode.Equals(_programCode))
              {
                _class.IdProgramContent = item.Id;
                flag11 = false; break;
              }
            }
            if (flag11)
            {
              ClassProgramCode classProgramCode = new ClassProgramCode { ProgramCode = _programCode };
              _classProgramCodeRepository.CreateProgramCodeForImport(classProgramCode);
              classProgram = _classProgramCodeRepository.GetAll();
              _class.IdProgramContent = Convert.ToInt32(classProgram.Max(s => s.Id));
            }

            string _attendeeType = dataset.Tables[0].Rows[i].ItemArray[5] != null ? Convert.ToString(dataset.Tables[0].Rows[i].ItemArray[5]).ToString() : "-1";
            var attendee = _attendeeTypeRepository.GetAll();
            bool flag12 = true;
            foreach (var item in attendee)
            {
              if (item.Name.Equals(_attendeeType))
              {
                _class.IdAttendeeType = item.Id;
                flag12 = false; break;
              }
            }
            if (flag12)
            {
              AttendeeType attendeeType = new AttendeeType { Name = _attendeeType };
              _attendeeTypeRepository.CreateAttendeeTypeForImport(attendeeType);
              attendee = _attendeeTypeRepository.GetAll();
              _class.IdAttendeeType = Convert.ToInt32(attendee.Max(s => s.Id));
            }

            classes.Add(_class);

            _classAdmin = dataset.Tables[0].Rows[i].ItemArray[20] != null ? Convert.ToString(dataset.Tables[0].Rows[i].ItemArray[20]).ToString() : "-1";
            _classMentor = dataset.Tables[0].Rows[i].ItemArray[18] != null ? Convert.ToString(dataset.Tables[0].Rows[i].ItemArray[18]).ToString() : "-1";
            _classMentor2 = dataset.Tables[0].Rows[i].ItemArray[19] != null ? Convert.ToString(dataset.Tables[0].Rows[i].ItemArray[19]).ToString() : "-1";
            _classLocation = dataset.Tables[0].Rows[i].ItemArray[21] != null ? Convert.ToString(dataset.Tables[0].Rows[i].ItemArray[21]).ToString() : "-1";

          }
          stream.Close();
          if (classes.Count > 0)
          {
            foreach (var _class in classes)
            {
              _classRepository.CreateClassForImport(_class);

              ClassAdmin classAdmin = new ClassAdmin();
              var users = _userRepository.GetUsersForImport();
              bool flag3 = true;
              foreach (var item in users)
              {
                if (item.FullName == _classAdmin)
                {
                  classAdmin.IdUser = item.ID;
                  flag3 = false; break;
                }
              }
              if (flag3)
              {
                User us1 = new User { UserName = _classAdmin, Password = "123", FullName = _classAdmin, DateOfBirth = DateTime.Parse("01-01-0001"), Gender = Char.Parse("M"), Phone = "", Email = "", Address = "", Status = 1, IdRole = 1 };
                _userRepository.CreateUserForImport(us1);
                users = _userRepository.GetUsersForImport();
                classAdmin.IdUser = users.Max(s => s.ID);
              }
              classes = _classRepository.GetClasses();
              classAdmin.IdClass = classes.Max(s => s.Id);
              _classAdminRepository.AddAdminForImport(classAdmin);

              if (!_classMentor.Equals(""))
              {
                ClassMentor classMentor = new ClassMentor();
                bool flag4 = true;
                foreach (var item in users)
                {
                  if (item.FullName == _classMentor)
                  {
                    classMentor.IdUser = item.ID;
                    flag4 = false; break;
                  }
                }
                if (flag4)
                {
                  User us2 = new User { UserName = _classMentor, Password = "123", FullName = _classMentor, DateOfBirth = DateTime.Parse("01-01-0001"), Gender = Char.Parse("M"), Phone = "", Email = "", Address = "", Status = 1, IdRole = 1 };
                  _userRepository.CreateUserForImport(us2);
                  users = _userRepository.GetUsersForImport();
                  classMentor.IdUser = users.Max(s => s.ID);
                }
                classes = _classRepository.GetClasses();
                classMentor.IdClass = classes.Max(s => s.Id);
                _classMentorRepository.AddMentorForImport(classMentor);
              }

              if (!_classMentor2.Equals(""))
              {
                ClassMentor classMentor2 = new ClassMentor();
                bool flag5 = true;
                foreach (var item in users)
                {
                  if (item.FullName == _classMentor2)
                  {
                    classMentor2.IdUser = item.ID;
                    flag5 = false; break;
                  }
                }
                if (flag5)
                {
                  User us3 = new User { UserName = _classMentor2, Password = "123", FullName = _classMentor2, DateOfBirth = DateTime.Parse("01-01-0001"), Gender = Char.Parse("M"), Phone = "", Email = "", Address = "", Status = 1, IdRole = 1 };
                  _userRepository.CreateUserForImport(us3);
                  users = _userRepository.GetUsersForImport();
                  classMentor2.IdUser = users.Max(s => s.ID);
                }
                classes = _classRepository.GetClasses();
                classMentor2.IdClass = classes.Max(s => s.Id);
                _classMentorRepository.AddMentorForImport(classMentor2);
              }

              long _idLocation = 0;
              switch (_classLocation)
              {
                case "FT1":
                  _idLocation = 1;
                  break;
                case "FT2":
                  _idLocation = 2;
                  break;
                case "FT3":
                  _idLocation = 3;
                  break;
              }
              classes = _classRepository.GetClasses();
              ClassLocation classLocation = new ClassLocation();
              _classLocationRepository.AddClassLocation(new ClassLocation { IdClass = classes.Max(s => s.Id), IdLocation = _idLocation });
            }
          }

        }
        else
        {
          response.IsSuccess = false;
          response.Message = "Incorrect File";
          return response;
        }


      }
      catch (Exception ex)
      {
        response.IsSuccess = false;
        response.Message = ex.Message;

      }
      finally
      {
        await _context.DisposeAsync();
      }
      return response;
    }
    #endregion

    public void SaveAsync()
    {
      _unitOfWork.commitAsync();
    }

  }
}









