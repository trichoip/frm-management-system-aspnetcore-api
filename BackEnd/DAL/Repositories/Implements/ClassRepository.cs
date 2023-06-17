using DAL.Entities;
using DAL.Infrastructure;
using DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories.Implements
{
  public class ClassRepository : RepositoryBase<Class>, IClassRepository
  {
    private readonly FRMDbContext _dbContext;
    private readonly IUnitOfWork _unitOfWork;

    public ClassRepository(IDbFactory dbFactory, IUnitOfWork unitOfWork) : base(dbFactory)
    {
      _dbContext = dbFactory.Init();
      _unitOfWork = unitOfWork;
    }
    public async Task<bool> DeleteClass(long id)
    {
      var exist = await _dbSet.Where(x => x.Id == id).FirstOrDefaultAsync();
      if (exist != null && exist.Status == 1)
      {

        _dbSet.Remove(exist);
        _unitOfWork.commitAsync();


        return true;
      }
      else
      {
        return true;
      }
    }


    public Class GetById(long Id)
    {
      return _dbSet.Include(x => x.TrainingProgram).ThenInclude(x => x.Curricula)
                    .FirstOrDefault(x => x.Id == Id);
    }

    public async Task<bool> DeActivate(long id)
    {
      var classDetail = await _dbSet.Where(x => x.Id == id).FirstOrDefaultAsync();
      if (classDetail == null)
        return false;
      classDetail.IdStatus = 9;
      classDetail.Status = 1;
      _dbSet.Update(classDetail);
      _unitOfWork.commitAsync();
      return true;
    }

    public async Task<long> Duplicate(long Id)
    {
      string c = "(Copy)";
      var target = await _dbSet.Include(x => x.Locations)
          .Include(x => x.ClassAdmins)
          .Include(x => x.ClassMentors)
          .Include(x => x.ClassUpdateHistories)
          .Include(x => x.TrainingProgram).ThenInclude(x => x.HistoryTrainingPrograms).ThenInclude(x => x.User)
          .Include(x => x.TrainingProgram).ThenInclude(x => x.Curricula).ThenInclude(x => x.Syllabus).ThenInclude(x => x.Sessions).ThenInclude(x => x.Units).ThenInclude(x => x.Lessons)
          .Include(x => x.ClassTrainees).FirstOrDefaultAsync(x => x.Id == Id);
      if (target == null)

        return 0;
      long newSyllabusId;
      var newClass = new Class();

      newClass.Name = target.Name.Insert(target.Name.Length, c);
      newClass.ClassCode = target.ClassCode;
      newClass.Status = target.Status;
      newClass.StartTimeLearning = target.StartTimeLearning;
      newClass.EndTimeLearing = target.EndTimeLearing;
      newClass.ReviewedBy = target.ReviewedBy;
      newClass.ReviewedUser = target.ReviewedUser;
      newClass.ReviewedOn = target.ReviewedOn;
      newClass.CreatedBy = target.CreatedBy;
      newClass.CreatedUser = target.CreatedUser;
      newClass.CreatedOn = DateTime.Now;
      newClass.ApprovedBy = target.ApprovedBy;
      newClass.ApprovedUser = target.ApprovedUser;
      newClass.ApprovedOn = DateTime.Now;
      newClass.PlannedAtendee = target.PlannedAtendee;
      newClass.ActualAttendee = target.ActualAttendee;
      newClass.AcceptedAttendee = target.AcceptedAttendee;
      newClass.CurrentSession = target.CurrentSession;
      newClass.CurrentUnit = target.CurrentUnit;
      newClass.StartYear = DateTime.Now.Year;
      newClass.StartDate = DateTime.Now;
      newClass.EndDate = target.EndDate;
      newClass.ClassNumber = target.ClassNumber;
      newClass.IdTechnicalGroup = target.IdTechnicalGroup;
      newClass.classTechnicalGroup = target.classTechnicalGroup;
      newClass.IdFSU = target.IdFSU;
      newClass.FsoftUnit = target.FsoftUnit;
      newClass.IdFSUContact = target.IdFSUContact;
      newClass.FSUContactPoint = target.FSUContactPoint;
      newClass.IdStatus = target.IdStatus;
      newClass.ClassStatus = target.ClassStatus;
      newClass.IdSite = target.IdSite;
      newClass.ClassSite = target.ClassSite;
      newClass.IdUniversity = target.IdUniversity;
      newClass.IdFormatType = target.IdFormatType;
      newClass.IdProgramContent = target.IdProgramContent;
      newClass.ClassProgramCode = target.ClassProgramCode;
      newClass.IdAttendeeType = target.IdAttendeeType;
      newClass.AttendeeType = target.AttendeeType;
      if (target.Locations != null)
      {
        var listLocation = new List<ClassLocation>();
        foreach (var classlocation in target.Locations)
        {
          ClassLocation newlocation = new ClassLocation()
          {
            IdLocation = classlocation.IdLocation,
          };
          listLocation.Add(newlocation);
        }
        newClass.Locations = listLocation;
      }

      if (_dbContext.ClassUpdateHistories.ToList().Count == 0)
      {
        var ListUpdatedHistory = new List<ClassUpdateHistory>();

        ClassUpdateHistory newUpdateHistory = new ClassUpdateHistory()
        {
            ModifyBy = newClass.CreatedBy,
            UpdateDate = DateTime.Now
        };
        ListUpdatedHistory.Add(newUpdateHistory);

        newClass.ClassUpdateHistories = ListUpdatedHistory;
      }
      else
      {
        var ListUpdatedHistory = new List<ClassUpdateHistory>();
        foreach (var updateHistory in _dbContext.ClassUpdateHistories.ToList())
        {
          ClassUpdateHistory newUpdateHistory = new ClassUpdateHistory()
          {
            ModifyBy = newClass.CreatedBy,
            UpdateDate = DateTime.Now
          };
          ListUpdatedHistory.Add(newUpdateHistory);
                    break;
                }
        newClass.ClassUpdateHistories = ListUpdatedHistory;
      }

      newClass.ClassSelectedDates = target.ClassSelectedDates;
            if(target.TrainingProgram!=null)
            {
                var newPropgram = new TrainingProgram();
                newPropgram.Name = target.TrainingProgram.Name + " (Copy)";
                newPropgram.Status = target.TrainingProgram.Status;
                
                if (target.TrainingProgram.HistoryTrainingPrograms != null)
                {
                    var ListHistoryTrainingProgram = new List<HistoryTrainingProgram>();
                    foreach (var historyTrainingProgram in target.TrainingProgram.HistoryTrainingPrograms)
                    {
                        HistoryTrainingProgram historyTrainingProgram1 = new HistoryTrainingProgram()
                        {
                            ModifiedOn = historyTrainingProgram.ModifiedOn,
                            User = historyTrainingProgram.User,
                        };
                        ListHistoryTrainingProgram.Add(historyTrainingProgram1);
                        break;
                    }
                    newPropgram.HistoryTrainingPrograms = ListHistoryTrainingProgram;
                }
                var oldSyllabus = GetSyllabus(target.TrainingProgram.Curricula.First().IdSyllabus);

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
                        IdUser = target.TrainingProgram.HistoryTrainingPrograms.First().IdUser,
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
                            newSession.Name = session.Name + "Copy";
                            newSession.Index = session.Index;
                            newSession.Status = session.Status;
                            if (session.Units != null)
                            {
                                var listUnit = new List<Unit>();
                                foreach (var unit in session.Units)
                                {
                                    Unit newUnit = new Unit();
                                    newUnit.Name = unit.Name + "Copy";
                                    newUnit.Index = unit.Index;
                                    newUnit.Status = unit.Status;
                                    if (unit.Lessons != null)
                                    {
                                        var listLesson = new List<Lesson>();
                                        foreach (var lesson in unit.Lessons)
                                        {
                                            Lesson newLesson = new Lesson();
                                            newLesson.Name = lesson.Name + "Copy";
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
                                                    newMaterial.Name = material.Name + "Copy";
                                                    newMaterial.HyperLink = material.HyperLink;
                                                    newMaterial.Status = material.Status;
                                                    // Add history
                                                    newMaterial.HistoryMaterials = new List<HistoryMaterial>
                                                {
                                                    new HistoryMaterial
                                                    {
                                                        IdUser = target.TrainingProgram.HistoryTrainingPrograms.First().IdUser,
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
                    _dbContext.Syllabi.Add(newSyllabus);
                     _unitOfWork.Commit();
                    newSyllabusId =  newSyllabus.Id;
                }
                else throw new Exception("No syllabus with that id!");

                if (target.TrainingProgram.Curricula != null)
                {
                    var ListCurricula = new List<Curriculum>();
                    foreach (var curricula in target.TrainingProgram.Curricula)
                    {
                        Curriculum newcurricula = new Curriculum()
                        {
                            NumberOrder = curricula.NumberOrder,
                            IdSyllabus = newSyllabusId

                        }
                        ;
                        ListCurricula.Add(newcurricula);
                    }
                    newPropgram.Curricula = ListCurricula;
                }
                newClass.TrainingProgram = newPropgram;
                newClass.IdProgram = newPropgram.Id;
            }
      if (target.ClassTrainees != null)
      {
        var listtrainees = new List<ClassTrainee>();
        foreach (var classtrainees in target.ClassTrainees)
        {
          ClassTrainee newtrainees = new ClassTrainee()
          {
            IdUser = classtrainees.IdUser,
          };
          listtrainees.Add(newtrainees);
        };



        newClass.ClassTrainees = listtrainees;
      }
      if (target.ClassMentors != null)
      {
        var listMentors = new List<ClassMentor>();
        foreach (var classmentor in target.ClassMentors)
        {
          ClassMentor newclassmentor = new ClassMentor()
          {
            IdUser = classmentor.IdUser,
          };
          listMentors.Add(newclassmentor);
        };
        newClass.ClassMentors = listMentors;
      }
      if (target.ClassAdmins != null)
      {
        var listAdmins = new List<ClassAdmin>();
        foreach (var classAdmin in target.ClassAdmins)
        {
          ClassAdmin newclassAdmin = new ClassAdmin()
          {
            IdUser = classAdmin.IdUser,
          };
          listAdmins.Add(newclassAdmin);
        };

        newClass.ClassAdmins = listAdmins;
      }

      _dbSet.Add(newClass);
     _unitOfWork.Commit();

            return newClass.Id;



    }
 public Syllabus GetSyllabus(long IdSyllabus)
{
    var result = _dbContext.Syllabi
       .Include(x => x.AssignmentSchema)
       .Include(x => x.Level)
       .Include(x => x.HistorySyllabi)
       .Include(x => x.Sessions.Where(x => x.Status != 3))
                           .ThenInclude(x => x.Units.Where(x => x.Status != 3))
                           .ThenInclude(x => x.Lessons.Where(x => x.Status != 3))
                           .ThenInclude(x => x.Materials.Where(x => x.Status != 3))
       .Include(x => x.Sessions.Where(x => x.Status != 3))
                           .ThenInclude(x => x.Units.Where(x => x.Status != 3))
                           .ThenInclude(x => x.Lessons.Where(x => x.Status != 3))
                           .ThenInclude(x => x.DeliveryType)
       .Include(x => x.Sessions.Where(x => x.Status != 3))
                           .ThenInclude(x => x.Units.Where(x => x.Status != 3))
                           .ThenInclude(x => x.Lessons.Where(x => x.Status != 3))
                           .ThenInclude(x => x.FormatType)
       .Include(x => x.Sessions.Where(x => x.Status != 3))
                           .ThenInclude(x => x.Units.Where(x => x.Status != 3))
                           .ThenInclude(x => x.Lessons.Where(x => x.Status != 3))
                           .ThenInclude(x => x.OutputStandard)
       .FirstOrDefault(x => x.Id == IdSyllabus && x.Status != 3);
    return result;
}

public async Task<Class> GetDetail(long id)
    {
      var result = await _dbSet.Where(x => x.Id == id)
          .Include(x => x.ApprovedUser)
          .Include(x => x.ClassStatus)
          .Include(x => x.CreatedUser)
          .Include(x => x.FsoftUnit).ThenInclude(x => x.FSUContactPoints)
          .Include(x => x.ClassSelectedDates)
          .Include(x => x.ClassFormatType)
          .Include(x => x.Locations).ThenInclude(x => x.Location)
          .Include(x => x.ClassProgramCode)
          .Include(x => x.ReviewedUser)
          .Include(x => x.ClassSite)
          .Include(x => x.classTechnicalGroup)
          .Include(x => x.ClassTrainees).ThenInclude(x => x.User)
          .Include(x => x.ClassUniversityCode)
          .Include(x => x.ClassMentors).ThenInclude(x => x.User)
          .Include(x => x.TrainingProgram).ThenInclude(x => x.HistoryTrainingPrograms).ThenInclude(x => x.User)
          .Include(x => x.TrainingProgram).ThenInclude(x => x.Curricula).ThenInclude(x => x.Syllabus).ThenInclude(x => x.Sessions).ThenInclude(x => x.Units).ThenInclude(x => x.Lessons)
          .Include(x => x.TrainingProgram).ThenInclude(x => x.Curricula).ThenInclude(x => x.Syllabus).ThenInclude(x => x.HistorySyllabi)
          .Include(x => x.TrainingProgram).ThenInclude(x => x.Curricula)

          .FirstOrDefaultAsync();

      if (result == null)
        return new Class();

      return result;
    }

    public async Task<Class> GetAttende(long idClass)
    {
      var result = await _dbSet.Where(x => x.Id == idClass)
          .Include(x => x.ClassTrainees).ThenInclude(x => x.User).ThenInclude(x => x.Role)
          .Include(x => x.ClassAdmins).ThenInclude(x => x.User).ThenInclude(x => x.Role)
          .Include(x => x.ClassMentors).ThenInclude(x => x.User).ThenInclude(x => x.Role)
          .FirstOrDefaultAsync();

      if (result == null)
        return new Class();
      return result;
    }

    public async Task<Class> GetTrainingProgram(long id)
    {
      var result = await _dbSet.Where(x => x.Id == id)
          .Include(x => x.TrainingProgram).ThenInclude(x => x.HistoryTrainingPrograms).ThenInclude(x => x.User)
          .Include(x => x.TrainingProgram).ThenInclude(x => x.Curricula).ThenInclude(x => x.Syllabus).ThenInclude(X => X.HistorySyllabi)
          .Include(x => x.TrainingProgram).ThenInclude(x => x.Curricula).ThenInclude(x => x.Syllabus).ThenInclude(x => x.Sessions).ThenInclude(x => x.Units).ThenInclude(x => x.Lessons)
          .FirstOrDefaultAsync();

      if (result == null)
        return new Class();
      return result;
    }

    public async Task<Class> GetClassById(long idClass)
    {
      return await _dbContext.Classes.Include(x => x.Locations).ThenInclude(x => x.Location)
                               .Include(x => x.ClassAdmins).ThenInclude(x => x.User)
                               .Include(x => x.ClassMentors).ThenInclude(x => x.User).FirstOrDefaultAsync(x => x.Id == idClass);
    }

    public IEnumerable<Class> GetWithFilter(Class entity)
    {
      List<Class> classList = new();

      if (entity.Id != null)
      {
        classList = _dbContext.Classes.Where(x => x.Id == entity.Id).ToList();
      }
      else if (entity.ClassCode != null)
      {
        classList = _dbContext.Classes.Where(x => x.ClassCode.Equals(entity.ClassCode)).ToList();
      }
      else if (entity.StartDate != null)
      {
        classList = _dbContext.Classes.Where(x => x.StartDate == entity.StartDate).ToList();
      }
      else if (entity.EndDate != null)
      {
        classList = _dbContext.Classes.Where(x => x.EndDate == entity.EndDate).ToList();
      }
      else if (entity.StartTimeLearning != null && entity.EndTimeLearing != null)
      {
        classList = _dbContext.Classes.Where(x => x.StartTimeLearning == entity.StartTimeLearning && x.EndTimeLearing == entity.EndTimeLearing).ToList();
      }
      else if (entity.IdFSU != null)
      {
        classList = _dbContext.Classes.Where(x => x.IdFSU == entity.IdFSU).ToList();
      }
      else if (entity.IdStatus != null)
      {
        classList = _dbContext.Classes.Where(x => x.IdStatus == entity.IdStatus).ToList();
      }
      else if (entity.IdAttendeeType != null)
      {
        classList = _dbContext.Classes.Where(x => x.IdAttendeeType == entity.IdAttendeeType).ToList();
      }

      return classList;
    }



    public void UpdateClass(Class _class)
    {
      var checkId = _dbSet.FirstOrDefault(x => x.Id == _class.Id);
      if (checkId != null)
      {
        _dbContext.Entry<Class>(checkId).State = EntityState.Detached;
        _dbSet.Update(_class);
      }

    }


    public async Task<List<Class>> GetClassByClassCode(string classCode)
    {
      var a = await _dbSet
         .Include(x => x.ClassStatus)
         .Include(x => x.Locations).ThenInclude(x => x.Location)
         .Include(x => x.ReviewedUser)
         .Include(x => x.CreatedUser)
         .Include(x => x.ApprovedUser)
         .Include(x => x.classTechnicalGroup)
         .Include(x => x.ClassSite)
         .Include(x => x.ClassUniversityCode)
         .Include(x => x.ClassFormatType)
         .Include(x => x.ClassProgramCode)
         .Include(x => x.ClassSelectedDates)
         .Include(x => x.ClassTrainees).ThenInclude(x => x.User).ThenInclude(x => x.Role)
         .Include(x => x.ClassAdmins).ThenInclude(x => x.User)
         .Include(x => x.ClassMentors).ThenInclude(x => x.User)
         .Include(x => x.FsoftUnit).ThenInclude(x => x.FSUContactPoints)
         .Include(x => x.TrainingProgram).ThenInclude(x => x.HistoryTrainingPrograms).ThenInclude(x => x.User)
         .Include(x => x.TrainingProgram).ThenInclude(x => x.Curricula).ThenInclude(x => x.Syllabus)
         .ThenInclude(x => x.Sessions).ThenInclude(x => x.Units)
         .ThenInclude(x => x.Lessons).ThenInclude(x => x.DeliveryType)
         .Include(x => x.TrainingProgram).ThenInclude(x => x.Curricula).ThenInclude(x => x.Syllabus)
         .ThenInclude(x => x.Sessions).ThenInclude(x => x.Units)
         .ThenInclude(x => x.Lessons).ThenInclude(x => x.OutputStandard)
         .Include(x => x.TrainingProgram).ThenInclude(x => x.Curricula).ThenInclude(x => x.Syllabus)
         .ThenInclude(x => x.Sessions).ThenInclude(x => x.Units)
         .ThenInclude(x => x.Lessons).ThenInclude(x => x.FormatType)
         .Include(x => x.TrainingProgram).ThenInclude(x => x.Curricula).ThenInclude(x => x.Syllabus).ThenInclude(x => x.HistorySyllabi)
         .Where(x => (x.ClassCode == default || x.ClassCode.Contains(classCode)))
         .ToListAsync();
      return a;

    }




    #region bhhiep
    public IQueryable<Class> GetClassesQuery()
    {
      return _dbSet;
    }
    #endregion
    public void CreateClassForImport(Class _class)
    {
      _dbSet.Add(_class);
      _dbContext.SaveChanges();
      return;
    }
    public List<Class> GetClasses()
    {
      var list = _dbSet.ToList();
      return list;
    }


    public IEnumerable<Class> GetClassess(List<string> key, List<long> location, DateTime? classTimeFrom, DateTime? classTimeTo, List<long> classTime, List<long> status, List<long> attendee, int FSU, int trainer)
    {
      //filter
      IEnumerable<Class> classes = new List<Class>();
      if (key != null && key.Count != 0)
      {
        foreach (var item in key)
        {
          classes = _dbContext.Classes.Include(x => x.TrainingProgram).ThenInclude(x => x.Curricula).ThenInclude(x => x.Syllabus)
              .Where(s => s.Name.ToLower().Contains(item.ToLower()) || s.ClassCode.ToLower().Contains(item.ToLower()))
                           .Select(s => s).ToList();
        }
      }
      else
      {
        classes = _dbSet.Include(x => x.TrainingProgram).ThenInclude(x => x.Curricula).ThenInclude(x => x.Syllabus).ToList();
      }

      // Search by location
      if (location != null && location.Count > 0)
      {
        classes = classes.Where(c => c.Locations.Any(l => location.Contains(l.IdLocation)));
      }
      // Search by class time from
      if (classTimeFrom != null)
      {
        classes = classes.Where(c => c.StartDate >= classTimeFrom);
      }
      // Search by class time to
      if (classTimeTo != null)
      {
        classes = classes.Where(c => c.EndDate <= classTimeTo);
      }
      // TODO: Search by classTime 
      // Search by status
      if (status != null && status.Count > 0)
      {
        classes = classes.Where(c => status.Contains(c.Status));

      }
      // Search by attendee
      if (attendee != null && attendee.Count > 0)
      {
        classes = classes.Where(c => attendee.Contains(c.IdAttendeeType ?? 0));
      }
      // Search by FSU
      if (FSU != 0)
      {
        classes = classes.Where(c => c.IdFSU == FSU);
      }
      // Search by trainer
      if (trainer != 0)
      {
        classes = classes.Where(c => c.ClassTrainees.Any(t => t.IdUser == trainer));
      }
      return classes.ToList();
    }
    public int CountClass()
    {
      return _dbContext.Classes.Count();
    }

  }
}
