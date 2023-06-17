using AutoMapper;
using BAL.Comparer;
using BAL.Models;
using BAL.Services.Interfaces;
using Castle.Core.Internal;
using DAL;
using DAL.Entities;
using DAL.Infrastructure;
using DAL.Repositories.Interfaces;
using ExcelDataReader;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Math.EC.Rfc7748;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DAL.Entities.Syllabus;
using static System.Collections.Specialized.BitVector32;
using Microsoft.AspNetCore.Http;
using Org.BouncyCastle.Operators;
using System.Xml.Linq;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Org.BouncyCastle.Bcpg;
using System.Linq.Dynamic.Core;


namespace BAL.Services.Implements
{
    // Enum Status
    enum Status
    {
        Inactive,   // = 0
        Active,     // = 1
        Draft,      // = 2
        Delete      // = 3
    }

    public class SyllabusService : ISyllabusService
    {
        public readonly FRMDbContext _context;
        private ISyllabusRepository _syllabusRepository;
        private ISessionRepository _sessionRepository;
        private IAssignmentSchemaRepository _assignmentSchemaRepository;
        private IMaterialRepository _materialRepository;
        private IUnitRepository _unitRepository;
        private ILessonRepository _lessonRepository;
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;
        private bool _saveAsDraftToCreate = false;
        // Team 6
        private IUserRepository _userRepository;
        private IHistorySyllabusRepository _historyRepository;
        private IHistoryMaterialRepository _historyMaterialRepository;
        private ICurriculumRepository _curriculumRepository;
        private ITrainingProgramRepository _trainingProgramRepository;
        private IHistoryTrainingProgramRepository _historyTrainingProgramRepository;
        //Team 4
        private IOutputStandardRepository _outputStandardRepository;
        private long _userID = 0;
        private ILevelRepository _levelRepository;
        private IDeliveryTypeRepository _deliveryTypeRepository;
        private IFormatTypeRepository _formatTypeRepository;


        public SyllabusService(
            FRMDbContext context,
            ISyllabusRepository syllabusRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IUnitRepository unitRepository,
            ILessonRepository lessonRepository,
            ISessionRepository sessionRepository,
            IAssignmentSchemaRepository assignmentSchemaRepository,
            IMaterialRepository materialRepository,
            ICurriculumRepository curriculumRepository,
            IUserRepository userRepository,
            IHistorySyllabusRepository historyRepository,
            IOutputStandardRepository outputStandardRepository,
            ITrainingProgramRepository trainingProgramRepository,
            IHistoryTrainingProgramRepository historyTrainingProgramRepository,
            IHistoryMaterialRepository historyMaterialRepository,
             ILevelRepository levelRepository,
             IDeliveryTypeRepository deliveryTypeRepository,
             IFormatTypeRepository formatTypeRepository

             )
        {
            _context = context;
            _syllabusRepository = syllabusRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _sessionRepository = sessionRepository;
            _assignmentSchemaRepository = assignmentSchemaRepository;
            _materialRepository = materialRepository;
            _unitRepository = unitRepository;
            _lessonRepository = lessonRepository;
            _curriculumRepository = curriculumRepository;
            _historyRepository = historyRepository;
            _historyMaterialRepository = historyMaterialRepository;
            _outputStandardRepository = outputStandardRepository;
            _userRepository = userRepository;
            _trainingProgramRepository = trainingProgramRepository;
            _historyTrainingProgramRepository = historyTrainingProgramRepository;
            _levelRepository = levelRepository;
            _deliveryTypeRepository = deliveryTypeRepository;
            _formatTypeRepository = formatTypeRepository;
        }



        #region Team 1 Syllabus Services

        private long GetUserIdFromClaims(List<Claim> claims)
        {
            // default user if there's no token
            long userId = 0;

            // Check if claims
            if (claims != null || claims.Count() > 0)
            {
                // Get Userame
                var username = claims.First(claim => claim.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name").Value;
                // Get UserID
                userId = _userRepository.GetUser(username).ID;
            }

            // Return 0 if there is no token, else return userId
            return userId;
        }

        private void SetUserIdForAction(List<Claim> claims)
        {
            this._userID = GetUserIdFromClaims(claims);
        }

        public void SetSaveAsDraftToCreateCheck(bool check)
        {
            _saveAsDraftToCreate = check;
        }

        public void DeactivateSyllabus(long id, List<Claim> claims)
        {
            //Set UserID for Action
            SetUserIdForAction(claims);

            // Check if syllabus with input id in Database
            var syllabus = _syllabusRepository.GetById(id);
            if (syllabus != null)
            {
                syllabus.Status = (int)Status.Inactive;
                var history = syllabus.HistorySyllabi.ToList();
                history.Add(new HistorySyllabus
                {
                    IdUser = _userID,
                    ModifiedOn = DateTime.Now,
                    Action = "De-Activate"
                });
                syllabus.HistorySyllabi = history;
                foreach (var session in syllabus.Sessions)
                {
                    DeactivateSession(_mapper.Map<SessionViewModel>(session));
                }
                _syllabusRepository.UpdateSyllabus(syllabus);
            }
            else
            {
                throw new Exception("No syllabus found!");
            }
        }

        public void ActivateSyllabus(long id, List<Claim> claims)
        {
            //Set UserID for Action
            SetUserIdForAction(claims);

            // Check if syllabus with input id in Database
            var syllabus = _syllabusRepository.GetById(id);
            if (syllabus != null)
            {
                syllabus.Status = (int)Status.Active;
                var history = syllabus.HistorySyllabi.ToList();
                history.Add(new HistorySyllabus
                {
                    IdUser = _userID,
                    ModifiedOn = DateTime.Now,
                    Action = "Activate"
                });
                syllabus.HistorySyllabi = history;
                foreach (var session in syllabus.Sessions)
                {
                    ActivateSession(_mapper.Map<SessionViewModel>(session));
                }
                _syllabusRepository.UpdateSyllabus(syllabus);
            }
            else
            {

                throw new Exception("No syllabus found!");
            }
        }

        public SyllabusViewModel GetById(long id)
        {
            var SyllabusDTO = _syllabusRepository.GetById(id);
            if (SyllabusDTO != null)
                return _mapper.Map<SyllabusViewModel>(SyllabusDTO);
            throw new Exception("No syllabus with that id!");
        }

        public void DuplicateSyllabus(long id, List<Claim> claims)
        {
            //Set UserID for Action
            SetUserIdForAction(claims);

            var oldSyllabus = _syllabusRepository.GetById(id);
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
                        IdUser = _userID,
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
                                                        IdUser = _userID,
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
            }
            else throw new Exception("No syllabus with that id!");
        }

        public void CreateSyllabus(SyllabusViewModel syllabus, List<Claim> claims)
        {
            //Set UserID for Action
            SetUserIdForAction(claims);

            //----------------------Add New Syllabus----------------------
            // Set Status to 1 when create
            syllabus.Status = (int)Status.Active;
            // Set Version to 1.00 when create
            syllabus.Version = 1.00F;

            // Package newSyllabus for Create
            Syllabus newSyllabus = _mapper.Map<Syllabus>(syllabus);

            //----------------------Add HistorySyllabus----------------------
            newSyllabus.HistorySyllabi = new List<HistorySyllabus>
                {
                    new HistorySyllabus
                    {
                        IdUser = _userID,
                        ModifiedOn = DateTime.Now,
                        Action = "Create"
                    }
                };

            //----------------------Add HistoryMaterial / Pre-set status for Sessions, Unit, Lessons, Materials----------------------
            // Check if Sessions in Syllabus is null
            if (newSyllabus.Sessions != null)
            {
                foreach (var session in newSyllabus.Sessions)
                {
                    // Set Status to 1 when create
                    session.Status = (int)Status.Active;
                    // Check if Units in Session is null
                    if (session.Units != null)
                    {
                        foreach (var unit in session.Units)
                        {
                            // Set Status to 1 when create
                            unit.Status = (int)Status.Active;
                            // Check if Lessons in Unit is null
                            if (unit.Lessons != null)
                            {
                                foreach (var lesson in unit.Lessons)
                                {
                                    // Set Status to 1 when create
                                    lesson.Status = (int)Status.Active;
                                    // Check if Material in Lesson is null
                                    if (lesson.Materials != null)
                                    {
                                        foreach (var material in lesson.Materials)
                                        {
                                            // Set Status to 1 when create
                                            material.Status = (int)Status.Active;
                                            material.HistoryMaterials = new List<HistoryMaterial>
                                                {
                                                    new HistoryMaterial
                                                    {
                                                        IdUser = _userID,
                                                        ModifiedOn = DateTime.Now,
                                                        Action = "Create"
                                                    }
                                                };
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            // Create new Syllabus
            _syllabusRepository.CreateSyllabus(newSyllabus);

        }

        public void UpdateSyllabus(SyllabusViewModel syllabus, List<Claim> claims)
        {
            //Set UserID for Action
            SetUserIdForAction(claims);

            if (syllabus.Id != null)
            {
                var oldSyllabus = _syllabusRepository.GetById((long)syllabus.Id);

                if (oldSyllabus != null)
                {
                    oldSyllabus.Name = syllabus.Name;
                    oldSyllabus.Code = syllabus.Code;
                    oldSyllabus.AttendeeNumber = (int)syllabus.AttendeeNumber;
                    // Only Non-Draft syllabus can auto increase Version number
                    if (oldSyllabus.Version != (int)Status.Draft)
                    {
                        oldSyllabus.Version = oldSyllabus.Version + 1;
                    }
                    oldSyllabus.Technicalrequirement = syllabus.Technicalrequirement;
                    oldSyllabus.CourseObjectives = syllabus.CourseObjectives;
                    oldSyllabus.TrainingPrinciple = syllabus.TrainingPrinciple;
                    oldSyllabus.Description = syllabus.Description;
                    oldSyllabus.HyperLink = syllabus.HyperLink;
                    oldSyllabus.AssignmentSchema = _mapper.Map<AssignmentSchema>(syllabus.AssignmentSchema);

                    // If SaveAsDraftToCreate == true then update status to Active
                    if (_saveAsDraftToCreate)
                    {
                        //-----Add Syllabus History when Create-----
                        var historySyllabus = oldSyllabus.HistorySyllabi.ToList();
                        historySyllabus.Add(new HistorySyllabus
                        {
                            IdUser = _userID,
                            IdSyllabus = (long)syllabus.Id,
                            ModifiedOn = DateTime.Now,
                            Action = "Create"
                        });
                        oldSyllabus.HistorySyllabi = historySyllabus;
                        // Change Status
                        oldSyllabus.Status = (int)Status.Active;
                    }
                    else
                    {
                        //-----Add Syllabus History when Update-----
                        var historySyllabus = oldSyllabus.HistorySyllabi.ToList();
                        historySyllabus.Add(new HistorySyllabus
                        {
                            IdUser = _userID,
                            IdSyllabus = (long)syllabus.Id,
                            ModifiedOn = DateTime.Now,
                            Action = "Update"
                        });
                        oldSyllabus.HistorySyllabi = historySyllabus;
                    }


                    // Update Syllabus
                    _syllabusRepository.UpdateSyllabus(oldSyllabus);

                    // Update Sessions
                    UpdateSessions(syllabus);
                }
                else throw new Exception("This syllabus has been deleted!");
            }

            else throw new Exception("There is no Syllabus with that ID!");

        }

        private void UpdateSessions(SyllabusViewModel syllabus)
        {
            SessionComparer comparer = new SessionComparer();
            var oldSessionList = _mapper.Map<List<SessionViewModel>>(_sessionRepository.GetSessions((long)syllabus.Id));
            var newSessionList = syllabus.Sessions;
            var excludeList = oldSessionList.Except(newSessionList, comparer);

            foreach (var session in newSessionList)
            {
                if (session.Id != null)
                {
                    var target = _sessionRepository.GetById((long)session.Id);
                    target.Name = session.Name;
                    target.Index = session.Index;

                    // If SaveAsDraftToCreate == true then update status to Active
                    if (_saveAsDraftToCreate)
                    {
                        target.Status = (int)Status.Active;
                    }
                    _sessionRepository.Update(target);
                    UpdateUnit(session);
                }
                else
                {
                    List<Unit> units = ExtractUnitList(session);
                    Session newSession = new Session()
                    {
                        Name = session.Name,
                        Index = session.Index,
                        Status = 1,
                        IdSyllabus = (long)syllabus.Id,
                        Units = units
                    };
                    _sessionRepository.Create(newSession);
                }
            }

            foreach (var session in excludeList)
            {
                RemoveSession(session);
            }
        }

        private void UpdateUnit(SessionViewModel session)
        {
            UnitComparer comparer = new UnitComparer();
            var oldUnitList = _mapper.Map<List<UnitViewModel>>(_unitRepository.GetAllSessionUnit((long)session.Id));
            var newUnitList = session.Units;
            var excludeList = oldUnitList.Except(newUnitList, comparer);

            foreach (var unit in newUnitList)
            {

                if (unit.Id != null)
                {
                    var target = _unitRepository.GetById((long)unit.Id);
                    target.Name = unit.Name;
                    target.Index = unit.Index;

                    // If SaveAsDraftToCreate == true then update status to Active
                    if (_saveAsDraftToCreate)
                    {
                        target.Status = (int)Status.Active;
                    }
                    _unitRepository.Update(target);
                    UpdateLesson(unit);
                }
                else
                {
                    List<Lesson> lessons = ExtractLessonList(unit);
                    Unit newUnit = new Unit()
                    {
                        Name = unit.Name,
                        Index = unit.Index,
                        Status = 1,
                        IdSession = (long)session.Id,
                        Lessons = lessons
                    };
                    _unitRepository.Create(newUnit);
                }
            }

            foreach (var unit in excludeList)
            {
                RemoveUnit(unit);
            }
        }

        private void UpdateLesson(UnitViewModel unit)
        {
            LessonComparer comparer = new LessonComparer();
            var oldLessonList = _mapper.Map<List<LessonViewModel>>(_lessonRepository.GetAllUnitLessons((long)unit.Id));
            var newLessonList = unit.Lessons;
            var excludeList = oldLessonList.Except(newLessonList, comparer);

            foreach (var lesson in newLessonList)
            {

                if (lesson.Id != null)
                {
                    var target = _lessonRepository.GetById((long)lesson.Id);
                    target.Name = lesson.Name;
                    target.Duration = (int)lesson.Duration;
                    // If SaveAsDraftToCreate == true then update status to Active
                    if (_saveAsDraftToCreate)
                    {
                        target.Status = (int)Status.Active;
                    }
                    _lessonRepository.UpdateLesson(target);
                    UpdateLessonMaterial(lesson);
                }
                else
                {
                    var materialList = ExtractMarterialList(lesson);
                    Lesson newLesson = new Lesson()
                    {
                        Name = lesson.Name,
                        Duration = (int)lesson.Duration,
                        IdDeliveryType = (long)lesson.IdDeliveryType,
                        IdFormatType = (long)lesson.IdFormatType,
                        IdOutputStandard = (long)lesson.IdOutputStandard,
                        Status = 1,
                        IdUnit = (long)unit.Id,
                        Materials = materialList
                    };
                    _lessonRepository.Create(newLesson);
                }
            }

            foreach (var lesson in excludeList)
            {
                RemoveLesson(lesson);
            }
        }

        private void UpdateLessonMaterial(LessonViewModel lesson)
        {
            MaterialComparer comparer = new MaterialComparer();
            var oldMaterialList = _mapper.Map<List<MaterialViewModel>>(_materialRepository.GetLessonMaterials((long)lesson.Id));
            var newMaterialList = lesson.Materials;
            var excludeList = oldMaterialList.Except(newMaterialList, comparer);

            foreach (var material in newMaterialList)
            {

                if (material.Id != null)
                {
                    var target = _materialRepository.GetById((long)material.Id);
                    target.Name = material.Name;
                    target.HyperLink = material.HyperLink;

                    // If SaveAsDraftToCreate == true then update status to Active
                    if (_saveAsDraftToCreate)
                    {
                        target.Status = (int)Status.Active;
                        //-----Add Material History when Create-----
                        var historyMaterial = target.HistoryMaterials.ToList();
                        historyMaterial.Add(new HistoryMaterial
                        {
                            IdUser = _userID,
                            IdMaterial = (long)material.Id,
                            ModifiedOn = DateTime.Now,
                            Action = "Create"
                        });
                        target.HistoryMaterials = historyMaterial;
                    }
                    else
                    {
                        //-----Add Material History when Update-----
                        var historyMaterial = target.HistoryMaterials.ToList();
                        historyMaterial.Add(new HistoryMaterial
                        {
                            IdUser = _userID,
                            IdMaterial = (long)material.Id,
                            ModifiedOn = DateTime.Now,
                            Action = "Update"
                        });
                        target.HistoryMaterials = historyMaterial;
                    }
                    _materialRepository.Update(target);
                }
                else
                {

                    //Better call Create Material Logic to create with
                    //History recorded at the time of creation
                    var newMaterial = _mapper.Map<Material>(material);
                    newMaterial.IdLesson = (long)lesson.Id;
                    newMaterial.Status = (int)Status.Active;
                    //-----Add Material History when Create-----
                    newMaterial.HistoryMaterials = new List<HistoryMaterial>
                    {
                        new HistoryMaterial
                        {
                            IdUser = _userID,
                            ModifiedOn = DateTime.Now,
                            Action = "Create"
                        }
                    };
                    _materialRepository.Create(newMaterial);
                }
            }

            foreach (var material in excludeList)
            {
                RemoveMaterial(material);
            }
        }

        private void RemoveSession(SessionViewModel session)
        {
            var target = _sessionRepository.GetById((long)session.Id);
            if (target != null)
            {
                target.Status = (int)Status.Delete;
                _sessionRepository.Update(target);
                foreach (var unit in session.Units)
                {
                    RemoveUnit(_mapper.Map<UnitViewModel>(unit));
                }
            }

        }

        private void RemoveUnit(UnitViewModel unit)
        {
            var target = _unitRepository.GetById((long)unit.Id);
            if (target != null)
            {
                target.Status = (int)Status.Delete;
                _unitRepository.Update(target);
                foreach (var lesson in unit.Lessons)
                {
                    RemoveLesson(_mapper.Map<LessonViewModel>(lesson));
                }
            }
        }

        private void RemoveLesson(LessonViewModel lesson)
        {
            var target = _lessonRepository.GetById((long)lesson.Id);
            if (target != null)
            {
                target.Status = (int)Status.Delete;
                _lessonRepository.UpdateLesson(target);
                foreach (var material in target.Materials)
                {
                    RemoveMaterial(_mapper.Map<MaterialViewModel>(material));
                }
            }
        }

        private void RemoveMaterial(MaterialViewModel material)
        {
            var target = _materialRepository.GetById((long)material.Id);

            //-----Add Material History when Remove-----
            var historyMaterial = target.HistoryMaterials.ToList();
            historyMaterial.Add(new HistoryMaterial
            {
                IdUser = _userID,
                IdMaterial = (long)material.Id,
                ModifiedOn = DateTime.Now,
                Action = "Delete"
            });
            target.HistoryMaterials = historyMaterial;

            if (target != null)
            {
                target.Status = (int)Status.Delete;
                _materialRepository.Update(target);
            }
        }

        private void DeactivateSession(SessionViewModel session)
        {
            var target = _sessionRepository.GetById((long)session.Id);
            if (target != null)
            {
                target.Status = (int)Status.Inactive;
                _sessionRepository.Update(target);
                foreach (var unit in session.Units)
                {
                    DeactivateUnit(_mapper.Map<UnitViewModel>(unit));
                }
            }

        }

        private void DeactivateUnit(UnitViewModel unit)
        {
            var target = _unitRepository.GetById((long)unit.Id);
            if (target != null)
            {
                target.Status = (int)Status.Inactive;
                _unitRepository.Update(target);
                foreach (var lesson in unit.Lessons)
                {
                    DeactivateLesson(_mapper.Map<LessonViewModel>(lesson));
                }
            }
        }

        private void DeactivateLesson(LessonViewModel lesson)
        {
            var target = _lessonRepository.GetById((long)lesson.Id);
            if (target != null)
            {
                target.Status = (int)Status.Inactive;
                _lessonRepository.UpdateLesson(target);
                foreach (var material in target.Materials)
                {
                    DeactivateMaterial(_mapper.Map<MaterialViewModel>(material));
                }
            }
        }

        private void DeactivateMaterial(MaterialViewModel material)
        {
            var target = _materialRepository.GetById((long)material.Id);

            //-----Add Material History when Remove-----
            var historyMaterial = target.HistoryMaterials.ToList();
            historyMaterial.Add(new HistoryMaterial
            {
                IdUser = _userID,
                IdMaterial = (long)material.Id,
                ModifiedOn = DateTime.Now,
                Action = "Deactivate"
            });
            target.HistoryMaterials = historyMaterial;

            if (target != null)
            {
                target.Status = (int)Status.Inactive;
                _materialRepository.Update(target);
            }
        }

        private void ActivateSession(SessionViewModel session)
        {
            var target = _sessionRepository.GetById((long)session.Id);
            if (target != null)
            {
                target.Status = (int)Status.Active;
                _sessionRepository.Update(target);
                foreach (var unit in session.Units)
                {
                    ActivateUnit(_mapper.Map<UnitViewModel>(unit));
                }
            }

        }

        private void ActivateUnit(UnitViewModel unit)
        {
            var target = _unitRepository.GetById((long)unit.Id);
            if (target != null)
            {
                target.Status = (int)Status.Active;
                _unitRepository.Update(target);
                foreach (var lesson in unit.Lessons)
                {
                    ActivateLesson(_mapper.Map<LessonViewModel>(lesson));
                }
            }
        }

        private void ActivateLesson(LessonViewModel lesson)
        {
            var target = _lessonRepository.GetById((long)lesson.Id);
            if (target != null)
            {
                target.Status = (int)Status.Active;
                _lessonRepository.UpdateLesson(target);
                foreach (var material in target.Materials)
                {
                    ActivateMaterial(_mapper.Map<MaterialViewModel>(material));
                }
            }
        }

        private void ActivateMaterial(MaterialViewModel material)
        {
            var target = _materialRepository.GetById((long)material.Id);

            //-----Add Material History when Remove-----
            var historyMaterial = target.HistoryMaterials.ToList();
            historyMaterial.Add(new HistoryMaterial
            {
                IdUser = _userID,
                IdMaterial = (long)material.Id,
                ModifiedOn = DateTime.Now,
                Action = "Activate"
            });
            target.HistoryMaterials = historyMaterial;

            if (target != null)
            {
                target.Status = (int)Status.Active;
                _materialRepository.Update(target);
            }
        }

        private List<Session> ExtractSessionList(SyllabusViewModel syllabus)
        {
            List<Session> sessions = new List<Session>();
            foreach (var session in syllabus.Sessions)
            {
                List<Unit> units = ExtractUnitList(session);
                Session newSession = new Session()
                {
                    Name = session.Name,
                    Index = (int)session.Index,
                    Status = 1,
                    Units = units
                };
                sessions.Add(newSession);
            }
            return sessions;
        }

        private List<Unit> ExtractUnitList(SessionViewModel session)
        {
            List<Unit> unitList = new List<Unit>();
            foreach (var unit in session.Units)
            {
                List<Lesson> lessons = ExtractLessonList(unit);
                Unit newUnit = new Unit()
                {
                    Name = unit.Name,
                    Index = (int)unit.Index,
                    Status = 1,
                    Lessons = lessons
                };
                unitList.Add(newUnit);
            }
            return unitList;
        }

        private List<Lesson> ExtractLessonList(UnitViewModel unit)
        {
            var lessonList = new List<Lesson>();
            foreach (var lesson in unit.Lessons)
            {
                var materialList = ExtractMarterialList(lesson);
                Lesson newLesson = new Lesson()
                {
                    Name = lesson.Name,
                    Duration = (int)lesson.Duration,
                    IdDeliveryType = (int)lesson.IdDeliveryType,
                    IdFormatType = (int)lesson.IdFormatType,
                    IdOutputStandard = (int)lesson.IdOutputStandard,
                    Status = 1,
                    Materials = materialList
                };
                lessonList.Add(newLesson);
            }
            return lessonList;
        }

        private List<Material> ExtractMarterialList(LessonViewModel lesson)
        {
            var materialList = new List<Material>();
            foreach (var material in lesson.Materials)
            {
                Material newMaterial = new Material()
                {
                    Name = material.Name,
                    HyperLink = material.HyperLink,
                    Status = 1
                };

                materialList.Add(newMaterial);
            }
            return materialList;
        }

        public void DeleteSyllabus(long id, List<Claim> claims)
        {
            //Set UserID for Action
            SetUserIdForAction(claims);

            var syllabus = _syllabusRepository.GetById(id);
            if (syllabus != null)
            {
                syllabus.Status = (int)Status.Delete;
                var history = syllabus.HistorySyllabi.ToList();
                history.Add(new HistorySyllabus
                {
                    IdUser = _userID,
                    ModifiedOn = DateTime.Now,
                    Action = "Delete"
                });
                syllabus.HistorySyllabi = history;
                foreach (var session in syllabus.Sessions)
                {
                    RemoveSession(_mapper.Map<SessionViewModel>(session));
                }
                _syllabusRepository.UpdateSyllabus(syllabus);
            }
            else
            {

                throw new Exception("No syllabus found!");
            }
        }

        public void SaveAsDraft(SyllabusViewModel syllabus, List<Claim> claims)
        {
            //Set UserID for Action
            SetUserIdForAction(claims);

            //----------------------Add New Syllabus----------------------
            // Set Status to 2 when Save As Draft
            syllabus.Status = (int)Status.Draft;
            // Set Version to 0.00 when Save As Draft
            syllabus.Version = 0.00F;

            // Package newSyllabus for Create
            Syllabus newSyllabus = _mapper.Map<Syllabus>(syllabus);



            //----------------------Add HistorySyllabus----------------------
            newSyllabus.HistorySyllabi = new List<HistorySyllabus>
                {
                    new HistorySyllabus
                    {
                        IdUser = _userID,
                        ModifiedOn = DateTime.Now,
                        Action = "Save As Draft"
                    }
                };

            //----------------------Add HistoryMaterial / Pre-set status for Sessions, Unit, Lessons, Materials----------------------
            // Check if Sessions in Syllabus is null
            if (newSyllabus.Sessions != null)
            {
                foreach (var session in newSyllabus.Sessions)
                {
                    // Set Status to 1 when create
                    session.Status = (int)Status.Draft;
                    // Check if Units in Session is null
                    if (session.Units != null)
                    {
                        foreach (var unit in session.Units)
                        {
                            // Set Status to 1 when create
                            unit.Status = (int)Status.Draft;
                            // Check if Lessons in Unit is null
                            if (unit.Lessons != null)
                            {
                                foreach (var lesson in unit.Lessons)
                                {
                                    // Set Status to 1 when create
                                    lesson.Status = (int)Status.Draft;
                                    // Check if Material in Lesson is null
                                    if (lesson.Materials != null)
                                    {
                                        foreach (var material in lesson.Materials)
                                        {
                                            // Set Status to 1 when create
                                            material.Status = (int)Status.Draft;
                                            material.HistoryMaterials = new List<HistoryMaterial>
                                            {
                                                new HistoryMaterial
                                                {
                                                    IdUser = _userID,
                                                    ModifiedOn = DateTime.Now,
                                                    Action = "Save AS Draft"
                                                }
                                            };
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            // Create new Syllabus
            _syllabusRepository.CreateSyllabus(newSyllabus);

        }

        #endregion

        public List<SyllabusModel> GetAll(List<string>? key, int PAGE_SIZE, DateTime? from, DateTime? to, List<string>? sortBy, int page)
        {
            #region search
            List<Syllabus> syllab = new List<Syllabus>();

            syllab = _syllabusRepository.SearchAndFilterSyllabus(key, from, to).OrderBy(s => s.Name).ToList();
            #endregion

            #region sort 
            List<SyllabusModel> order = ShowSyllabuses(syllab);

            foreach (var item in sortBy) {
                order = order.AsQueryable().OrderBy(item).ToList();
            }
            #endregion
            return order;
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

        public DateTime GetCreatedOn(Syllabus syl)
        {
            HistorySyllabus histo = _historyRepository.GetCreatedOnHistorySyllabi(syl.Id);
            return histo.ModifiedOn;
        }

        public string GetCreatedBy(Syllabus syl)
        {
            HistorySyllabus histo = _historyRepository.GetCreatedOnHistorySyllabi(syl.Id);
            User a = _userRepository.GetUserById(histo.IdUser);
            return a.FullName;
        }

        // Team6
        public int GetAllSession(Syllabus syl)
        {
            IEnumerable<Session> sessions = _sessionRepository.GetAllSessions();
            IEnumerable<Unit> units = _unitRepository.GetAllUnits();
            IEnumerable<Lesson> lessons = _lessonRepository.GetAllLessons();
            var sess = sessions.Where(s => s.IdSyllabus == syl.Id);
            return sess.Count();
        }
        // Team6
        public string GetOutputStandard(Syllabus syl)
        {
            List<string> result = new List<string>();
            var se = _sessionRepository.GetSyllabusSession(syl.Id);
            foreach (var session in se)
            {
                var un = _unitRepository.GetSessionUnits(session.Id);
                foreach (var unit in un)
                {
                    var les = _lessonRepository.GetUnitLessons(unit.Id);
                    foreach (var lession in les)
                    {
                        OutputStandard a = _outputStandardRepository.GetOutputStandard(lession.IdOutputStandard);
                        if (a != null)
                        {
                            bool flag = true;
                            foreach (var item in result)
                            {
                                if (a.Name == item) flag = false;
                            }
                            if (flag) result.Add(a.Name);
                        }
                    }
                }
            }
            string res = "";
            foreach (var item in result)
            {
                res = res + item + " ";
            }
            return res;
        }

        public List<SyllabusModel> ShowSyllabuses(List<Syllabus> syllabuses)
        {
            List<SyllabusModel> result = new List<SyllabusModel>();
            foreach (var syllabus in syllabuses)
            {
                string name = syllabus.Name;
                string code = syllabus.Code;
                DateTime createdOn = GetCreatedOn(syllabus);
                string createdBy = GetCreatedBy(syllabus);
                int duration = GetDuration(syllabus);
                string outputStandard = GetOutputStandard(syllabus);
                int status = syllabus.Status;
                result.Add(new SyllabusModel { Name = name, Code = code, CreatedOn = createdOn, CreatedBy = createdBy, Duration = duration, OutputStandard = outputStandard, Status = status });
            }
            return result; //ForMember
        }




        public TrainingProgramDetailViewModel GetDetailTrainingProgram(long id)
        {
            List<SearchSyllabusViewModel> syllabuses = new List<SearchSyllabusViewModel>();
            TrainingProgramDetailViewModel viewModel = new TrainingProgramDetailViewModel();
            TrainingProgram trainingProgram = _trainingProgramRepository.GetbyId(id);
            viewModel = _mapper.Map<TrainingProgramDetailViewModel>(trainingProgram);

            if (viewModel == null)
            {
                return null;
            }

            var curriculums = _curriculumRepository.GetCurriculumsQuery(id)
                                .Include(x => x.Syllabus)
                                .ThenInclude(x => x.HistorySyllabi)
                                .ThenInclude(x => x.User).OrderBy(x => x.NumberOrder)
                                .ToList();

            var histories = _historyTrainingProgramRepository.GetHistoryTrainingQuery(id)
                                 .Include(x => x.User).ToList();


            foreach (var c in curriculums)
            {


                if (c.Syllabus.Status != 3)
                {
                    Syllabus syllabi = _syllabusRepository.GetById(c.Syllabus.Id);
                    var syllabus = _mapper.Map<SearchSyllabusViewModel>(syllabi);
                    syllabus.CreateBy = c.Syllabus.HistorySyllabi.OrderBy(x => x.ModifiedOn).FirstOrDefault()?.User.FullName;
                    syllabus.ModifiedOn = c.Syllabus.HistorySyllabi.OrderBy(x => x.ModifiedOn).FirstOrDefault()?.ModifiedOn;
                    syllabus.Duration = GetDuration(c.Syllabus);
                    viewModel.Duration += GetDuration(c.Syllabus);
                    syllabus.TotalSession = GetAllSession(c.Syllabus);
                    syllabus.Status = GetStatus(syllabi.Status);
                    viewModel.totalSession += GetAllSession(c.Syllabus);
                    syllabuses.Add(syllabus);

                }
            }

            viewModel.Createon = histories.OrderBy(x => x.ModifiedOn).FirstOrDefault()!.ModifiedOn;
            viewModel.Createby = histories.OrderBy(x => x.ModifiedOn).FirstOrDefault()!.User.FullName;
            viewModel.syllabuses = syllabuses;
            viewModel.Status = GetStatus(trainingProgram.Status);

            return viewModel;
        }

        //
        public String GetStatus(int status)
        {
            switch (status)
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
                    return "Undefine";

            }
        }

        public List<SearchSyllabusViewModel> SearchSyllabusByName(string name)
        {
            List<SearchSyllabusViewModel> viewModel = new List<SearchSyllabusViewModel>();

            var listSyllabus = _syllabusRepository.SearchSyllabusByNameQuery(name)
                .Include(x => x.HistorySyllabi)
                .ThenInclude(x => x.User).ToList();
            foreach (var syllabus in listSyllabus)
            {
                if (syllabus.Status == 1)
                {
                    var view = _mapper.Map<SearchSyllabusViewModel>(syllabus);
                    view.ModifiedOn = syllabus.HistorySyllabi.OrderBy(x => x.ModifiedOn).FirstOrDefault().ModifiedOn;
                    view.CreateBy = syllabus.HistorySyllabi.OrderBy(x => x.ModifiedOn).FirstOrDefault().User.FullName;
                    view.TotalSession = GetAllSession(syllabus);
                    view.Duration = GetDuration(syllabus);
                    view.Status = GetStatus(syllabus.Status);
                    viewModel.Add(view);
                }

            }
            return viewModel;
        }
        // Team6





        public async Task<Syllabus.UpLoadExcelFileResponse> UploadExcelFile(Syllabus.UpLoadExcelFileRequest request, string path)

        {
            UpLoadExcelFileResponse response = new UpLoadExcelFileResponse();
            var syllabuses = new List<Syllabus>();


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
                    for (int i = 5; i < dataset.Tables[0].Rows.Count; i++)
                    {
                        Syllabus syllabus = new Syllabus();

                        //var syllabuss = _syllabusRepository.GetAll();

                        syllabus.Name = dataset.Tables[0].Rows[i].ItemArray[0] != null ? Convert.ToString(dataset.Tables[0].Rows[i].ItemArray[0]).ToString() : "-1";

                        syllabus.Code = dataset.Tables[0].Rows[i].ItemArray[1] != null ? Convert.ToString(dataset.Tables[0].Rows[i].ItemArray[1]).ToString() : "-1";

                        syllabus.Version = dataset.Tables[0].Rows[i].ItemArray[2] != null ? Convert.ToInt32(dataset.Tables[0].Rows[i].ItemArray[2]) : -1;

                        syllabus.AttendeeNumber = dataset.Tables[0].Rows[i].ItemArray[3] != null ? Convert.ToInt32(dataset.Tables[0].Rows[i].ItemArray[3]) : -1;

                        syllabus.CourseObjectives = dataset.Tables[0].Rows[i].ItemArray[4] != null ? Convert.ToString(dataset.Tables[0].Rows[i].ItemArray[4]).ToString() : "-1";

                        syllabus.Technicalrequirement = dataset.Tables[0].Rows[i].ItemArray[9] != null ? Convert.ToString(dataset.Tables[0].Rows[i].ItemArray[9]).ToString() : "-1";

                        syllabus.Status = 1;



                        syllabus.TrainingPrinciple = dataset.Tables[0].Rows[i].ItemArray[17] != null ? Convert.ToString(dataset.Tables[0].Rows[i].ItemArray[17]).ToString() : "-1";


                        syllabus.Description = "";

                        syllabus.HyperLink = "";


                        AssignmentSchema assignmentSchema = new AssignmentSchema();

                        assignmentSchema.PercentQuiz = dataset.Tables[0].Rows[i].ItemArray[10] != null ? Convert.ToInt32(dataset.Tables[0].Rows[i].ItemArray[10]) : -1;

                        assignmentSchema.PercentAssigment = dataset.Tables[0].Rows[i].ItemArray[11] != null ? Convert.ToInt32(dataset.Tables[0].Rows[i].ItemArray[11]) : -1;

                        assignmentSchema.PercentFinal = dataset.Tables[0].Rows[i].ItemArray[12] != null ? Convert.ToInt32(dataset.Tables[0].Rows[i].ItemArray[12]) : -1;

                        assignmentSchema.PercentTheory = dataset.Tables[0].Rows[i].ItemArray[13] != null ? Convert.ToInt32(dataset.Tables[0].Rows[i].ItemArray[13]) : -1;

                        assignmentSchema.PercentFinalPractice = dataset.Tables[0].Rows[i].ItemArray[14] != null ? Convert.ToInt32(dataset.Tables[0].Rows[i].ItemArray[14]) : -1;

                        assignmentSchema.PassingCriterial = dataset.Tables[0].Rows[i].ItemArray[15] != null ? Convert.ToInt32(dataset.Tables[0].Rows[i].ItemArray[15]) : -1;

                        syllabus.AssignmentSchema = assignmentSchema;




                        string _Level = dataset.Tables[0].Rows[i].ItemArray[24] != null ? Convert.ToString(dataset.Tables[0].Rows[i].ItemArray[24]) : "-1";
                        var level = _levelRepository.GetAll();
                        foreach (var item in level)
                        {
                            if (item.Name.Equals(_Level))
                            {
                                syllabus.IdLevel = item.Id;
                            }
                        }


                        List<Session> sessions = new List<Session>();



                        for (int j = 2; j < dataset.Tables[1].Rows.Count; j++)
                        {


                            string nameSessions = dataset.Tables[1].Rows[j].ItemArray[1] != null ? Convert.ToString(dataset.Tables[1].Rows[j].ItemArray[1]).ToString() : "-1";
                            if (!nameSessions.Equals(""))
                            {

                                Session session = new Session();

                                session.Name = dataset.Tables[1].Rows[j].ItemArray[1] != null ? Convert.ToString(dataset.Tables[1].Rows[j].ItemArray[1]).ToString() : "-1";


                                session.Index = dataset.Tables[1].Rows[j].ItemArray[0] != null ? Convert.ToInt32(dataset.Tables[1].Rows[j].ItemArray[0]) : -1;
                                session.Status = 1;


                                List<Unit> units = new List<Unit>();

                                for (int k = 2; k < dataset.Tables[1].Rows.Count; k++)

                                {
                                    string nameSession = dataset.Tables[1].Rows[k].ItemArray[1] != null ? Convert.ToString(dataset.Tables[1].Rows[k].ItemArray[1]).ToString() : "-1";
                                    if (nameSession != session.Name && !nameSession.Equals(""))
                                    {
                                        break;
                                    }

                                    Unit unit = new Unit();

                                    string name = dataset.Tables[1].Rows[k].ItemArray[3] != null ? Convert.ToString(dataset.Tables[1].Rows[k].ItemArray[3]).ToString() : "-1";
                                    if (!name.Equals(""))
                                    {
                                        unit.Name = name;
                                        unit.Index = dataset.Tables[1].Rows[k].ItemArray[2] != null ? Convert.ToInt32(dataset.Tables[1].Rows[k].ItemArray[2]) : -1;
                                        unit.Status = 1;

                                        List<Lesson> lessones = new List<Lesson>();

                                        for (int l = k; l < dataset.Tables[1].Rows.Count; l++)

                                        {
                                            string nameUnit = dataset.Tables[1].Rows[l].ItemArray[3] != null ? Convert.ToString(dataset.Tables[1].Rows[l].ItemArray[3]).ToString() : "-1";
                                            if (nameUnit != unit.Name && !nameUnit.Equals(""))
                                            {
                                                break;
                                            }

                                            Lesson lesson = new Lesson();


                                            lesson.Name = dataset.Tables[1].Rows[l].ItemArray[5] != null ? Convert.ToString(dataset.Tables[1].Rows[l].ItemArray[5]).ToString() : "-1";

                                            lesson.Duration = dataset.Tables[1].Rows[l].ItemArray[9] != null ? Convert.ToInt32(dataset.Tables[1].Rows[l].ItemArray[9]) : -1;

                                            string _deliveryType = dataset.Tables[1].Rows[l].ItemArray[8] != null ? Convert.ToString(dataset.Tables[1].Rows[l].ItemArray[8]) : "-1";

                                            var deliveryType = _deliveryTypeRepository.GetAll();

                                            foreach (var item in deliveryType)
                                            {
                                                if (item.Name.Equals(_deliveryType))
                                                {


                                                    lesson.IdDeliveryType = item.Id;


                                                }
                                            }

                                            string _formatType = dataset.Tables[1].Rows[l].ItemArray[6] != null ? Convert.ToString(dataset.Tables[1].Rows[l].ItemArray[6]) : "-1";
                                            var formatType = _formatTypeRepository.GetAll();

                                            foreach (var item in formatType)
                                            {
                                                if (item.Name.Equals(_formatType))
                                                {


                                                    lesson.IdFormatType = item.Id;
                                                }
                                            }
                                            string _outputStandard = dataset.Tables[1].Rows[l].ItemArray[7] != null ? Convert.ToString(dataset.Tables[1].Rows[l].ItemArray[7]) : "-1";
                                            var outputStandard = _outputStandardRepository.GetAll();
                                            foreach (var item in outputStandard)
                                            {
                                                if (item.Name.Equals(_outputStandard))
                                                {
                                                    lesson.IdOutputStandard = item.Id;
                                                }
                                            }




                                            lesson.Status = 1;

                                            Material material = new Material();
                                            material.Name = dataset.Tables[1].Rows[l].ItemArray[10] != null ? Convert.ToString(dataset.Tables[1].Rows[l].ItemArray[10]).ToString() : "-1";
                                            material.HyperLink = "";
                                            IEnumerable<Material> materials = new List<Material>()
                                     {
                                        material
                                      };
                                            lesson.Materials = materials;


                                            lessones.Add(lesson);

                                        }
                                        unit.Lessons = lessones;
                                        units.Add(unit);
                                    }

                                }
                                session.Units = units;
                                sessions.Add(session);
                            }

                        }
                        syllabus.Sessions = sessions;

                        syllabuses.Add(syllabus);



                    }

                    stream.Close();
                    if (syllabuses.Count > 0)
                    {
                        foreach (var syllabus in syllabuses)

                        {
                            _syllabusRepository.CreateSyllabusForImport(syllabus);
                            List<Syllabus> syllabusList = _syllabusRepository.GetAll();
                            HistorySyllabus history = new HistorySyllabus
                            {
                                IdUser = 1,
                                IdSyllabus = syllabusList.Max(x => x.Id),
                                ModifiedOn = DateTime.Today,
                                Action = "Create"
                            };
                            _historyRepository.AddHistorySyllabusForImport(history);




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