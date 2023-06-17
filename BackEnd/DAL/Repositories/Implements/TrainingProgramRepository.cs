using DAL.Entities;
using DAL.Infrastructure;
using System.Security.Claims;
using DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories.Implements
{
    public class TrainingProgramRepository : RepositoryBase<TrainingProgram>, ITrainingProgramRepository
    {
        private readonly FRMDbContext _dbContext;
        private readonly IUnitOfWork _unitOfWork;

        public TrainingProgramRepository(IDbFactory dbFactory, IUnitOfWork unitOfWork) : base(dbFactory)
        {
            _dbContext = dbFactory.Init();
            _unitOfWork = unitOfWork;
        }
        public async Task<List<TrainingProgram>> GetAll()
        {
            var count = await _dbSet.CountAsync();
            return await _dbSet.Skip(count > 200 ? count - 200 : 0).Take(count)
                .Include(x => x.HistoryTrainingPrograms).ThenInclude(x => x.User)
                .Include(x => x.Curricula).ThenInclude(x => x.Syllabus).ThenInclude(x => x.Sessions).ThenInclude(x => x.Units).ThenInclude(x => x.Lessons)
                .Include(x => x.Classes).ThenInclude(x => x.ClassUpdateHistories)
          .ToListAsync();
        }
        public async Task<bool> Delete(long id)
        {
            var exist = await _dbSet.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (exist != null && exist.Status == 1)
            {

                _dbSet.Remove(exist);
                await _dbContext.SaveChangesAsync();

                return true;
            }
            else
            {
                return false;
            }
        }
        public async Task<List<TrainingProgram>> Edit(long id, string name)
        {

            var exist = await _dbSet.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (exist != null)
            {
                exist.Name = name;


            }


            return await _dbSet.ToListAsync();
        }
        public async Task<bool> DeActivate(long id)
        {
            var program = await _dbSet.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (program == null)
                return false;
            program.Status = 1;
            _dbSet.Update(program);
            _unitOfWork.Commit();
            return true;
        }

        public async Task<long> Duplicate(long id)
        {
            
            string c = "(Copy)";
            var oldtrainingPropgram = await _dbSet.Include(x => x.HistoryTrainingPrograms).ThenInclude(x => x.User)
                      .Include(x => x.Curricula).ThenInclude(x => x.Syllabus).ThenInclude(x => x.Sessions).ThenInclude(x => x.Units).ThenInclude(x => x.Lessons).FirstOrDefaultAsync(x => x.Id == id);
            List<long> newSyllabusId= new List<long>();
            if (oldtrainingPropgram == null)
                return 0;
            long newTrainingSyllabusID=0;
            var newPropgram = new TrainingProgram();
            newPropgram.Name = oldtrainingPropgram.Name + "Copy";
            newPropgram.Status = oldtrainingPropgram.Status;
            if (oldtrainingPropgram.HistoryTrainingPrograms != null)
            {
                var ListHistoryTrainingProgram = new List<HistoryTrainingProgram>();
                foreach (var historyTrainingProgram in oldtrainingPropgram.HistoryTrainingPrograms)
                {
                    HistoryTrainingProgram historyTrainingProgram1 = new HistoryTrainingProgram()
                    {
                        ModifiedOn = historyTrainingProgram.ModifiedOn,
                        User = historyTrainingProgram.User,
                    };
                    ListHistoryTrainingProgram.Add(historyTrainingProgram1);
                }
                newPropgram.HistoryTrainingPrograms = ListHistoryTrainingProgram;
                var oldSyllabus = GetSyllabus(oldtrainingPropgram.Curricula.First().IdSyllabus);
                
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
                        IdUser = oldtrainingPropgram.HistoryTrainingPrograms.First().IdUser,
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
                            newSession.Name = session.Name+"Copy";
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
                                                        IdUser = oldtrainingPropgram.HistoryTrainingPrograms.First().IdUser,
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
                    newTrainingSyllabusID = newSyllabus.Id; 

                }
                else throw new Exception("No syllabus with that id!");
            }
            if (oldtrainingPropgram.Curricula != null)
            {
                var ListCurricula = new List<Curriculum>();
                foreach (var curricula in oldtrainingPropgram.Curricula)
                {
                    Curriculum newcurricula = new Curriculum()
                    {
                        NumberOrder = curricula.NumberOrder,
                        IdSyllabus = newTrainingSyllabusID
                    }
                    ;
                    ListCurricula.Add(newcurricula);
                }
                newPropgram.Curricula = ListCurricula;
            }


            _dbSet.Add(newPropgram);
            _unitOfWork.Commit();
            return newPropgram.Id;
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



        public async Task<List<TrainingProgram>> GetByFilter(List<string> programNames)
        {
            var result = new List<TrainingProgram>();
            if (programNames.Count == 0)
            {
                var trainingPrograms = await GetAll();
                foreach (var trainingProgram in trainingPrograms)
                {
                    if (!result.Contains(trainingProgram))
                    {
                        result.Add(trainingProgram);
                    }
                }
            }
            foreach (var name in programNames)
            {
                var trainingPrograms = await _dbSet.Where(x => x.Name.Contains(name) || x.HistoryTrainingPrograms.First().User.UserName.Contains(name))
                    .Include(x => x.HistoryTrainingPrograms).ThenInclude(x => x.User)
                    .Include(x => x.Curricula).ThenInclude(x => x.Syllabus).ThenInclude(x => x.Sessions).ThenInclude(x => x.Units).ThenInclude(x => x.Lessons)
                    .ToListAsync();
                foreach (var trainingProgram in trainingPrograms)
                {
                    if (!result.Contains(trainingProgram))
                    {
                        result.Add(trainingProgram);
                    }
                }
            }
            return result;
        }

        public async Task<TrainingProgram> GetById(long id)
        {
            var result = await _dbSet.FirstOrDefaultAsync(x => x.Id == id);
            if (result == null)
            {
                return new TrainingProgram();
            }
            else
            {
                return result;
            }
        }

        public async Task<bool> Edit(long id, string name, int status)
        {
            var program = await _dbSet.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (program == null)
            {
                return false;
            }
            program.Name = name;
            program.Status = status;
            _dbSet.Update(program);
            _unitOfWork.commitAsync();
            return true;
        }
        public TrainingProgram Create(TrainingProgram trainingProgram)
        {
            _dbSet.Add(trainingProgram);

            _dbContext.SaveChanges();

            return trainingProgram;
        }

        public TrainingProgram GetbyId(long id)
        {
            return _dbSet.Include(x => x.HistoryTrainingPrograms)
                         .Include(x => x.Curricula)
                         .FirstOrDefault(x => x.Id == id);
        }

        //Training Program
        public List<TrainingProgram> GetTraingProgramAll()
        {
            return _dbSet.ToList();
        }

        public List<TrainingProgram> GetTraingProgramAllById(long programId)
        {
            return _dbSet.Where(x => x.Id == programId && x.Status != 3 && x.Status != 6).ToList();
        }


        public TrainingProgram CreateTrainingProgram(TrainingProgram trainingProgram)
        {
            _dbSet.Add(trainingProgram);
            _dbContext.SaveChanges();
            return trainingProgram;
        }

        //Training Program
        public async Task<List<TrainingProgram>> Search(string name)
        {
            var res = await _dbSet
                .Include(x => x.HistoryTrainingPrograms).ThenInclude(x => x.User)
                .Include(x => x.Curricula).ThenInclude(x => x.Syllabus).ThenInclude(x => x.HistorySyllabi)
                .Include(x => x.Curricula).ThenInclude(x => x.Syllabus).ThenInclude(x => x.Sessions).ThenInclude(x => x.Units)
                .ThenInclude(x => x.Lessons).ThenInclude(x => x.DeliveryType)
                .Include(x => x.Curricula).ThenInclude(x => x.Syllabus).ThenInclude(x => x.Sessions).ThenInclude(x => x.Units)
                .ThenInclude(x => x.Lessons).ThenInclude(x => x.OutputStandard)
                .Include(x => x.Curricula).ThenInclude(x => x.Syllabus).ThenInclude(x => x.Sessions).ThenInclude(x => x.Units)
                .ThenInclude(x => x.Lessons).ThenInclude(x => x.FormatType)
                .Where(x => (x.Name == default || x.Name.Contains(name)))
                .ToListAsync();

            return res;
        }
        //team4
        public List<TrainingProgram> GetAllForImport()
        {
            return this._dbSet.ToList();
        }

    public void AddForImport(string name, int status)
    {
      var training = new TrainingProgram { Name = name, Status = 1 };
      _dbContext.Add(training);
      _dbContext.SaveChanges();
      var his = new HistoryTrainingProgram { IdUser = 1, IdProgram = _dbContext.TrainingPrograms.Max(x => x.Id), ModifiedOn = DateTime.Now };
      _dbContext.Add(his);
      _dbContext.SaveChanges();
    }


    }
}

