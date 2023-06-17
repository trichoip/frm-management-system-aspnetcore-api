using System;
using AutoMapper;
using BAL.Models;
using System.Linq;
using System.Text;
using DAL.Entities;
using DAL.Infrastructure;
using System.Threading.Tasks;
using BAL.Services.Interfaces;
using System.Collections.Generic;
using DAL.Repositories.Implements;
using DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace BAL.Services.Implements
{
    public class TrainingProgramService : ITrainingProgramService
    {
        private ITrainingProgramRepository _trainingProgramRepository;
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;
        private ICurriculumRepository _curriculumRepository;
        private IHistoryTrainingProgramRepository _historyTrainingProgramRepository;
        private IUserRepository _userRepository;

        public TrainingProgramService(ITrainingProgramRepository trainingProgramRepository, 
            IUnitOfWork unitOfWork, IHistoryTrainingProgramRepository historyTrainingProgramRepository, 
            ICurriculumRepository curriculumRepository, IMapper mapper, IUserRepository userRepository)
        {
            _trainingProgramRepository = trainingProgramRepository;
            _unitOfWork = unitOfWork;
            _historyTrainingProgramRepository = historyTrainingProgramRepository;
            _curriculumRepository = curriculumRepository;
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public TrainingProgram CreateTrainingProgram(ProgramViewModel newProgram)
        {
            List<Curriculum> curriculums = new List<Curriculum>();            
            TrainingProgram trainingProgram = new TrainingProgram
            {
                Name = newProgram.name,
                Status = 1,
            };
            foreach (var syllabus in newProgram.syllabi)
            {
                Curriculum curriculum = new Curriculum
                {
                    IdProgram = trainingProgram.Id,
                    IdSyllabus = syllabus.idSyllabus,
                    NumberOrder = syllabus.numberOrder,
                };
                curriculums.Add(curriculum);
            }
            trainingProgram.Curricula = curriculums;
            var user = _userRepository.GetUser(newProgram.createdBy);
            trainingProgram.HistoryTrainingPrograms = new List<HistoryTrainingProgram>()
            {
                new HistoryTrainingProgram
                {
                    IdUser = user.ID,
                    IdProgram = trainingProgram.Id,
                    ModifiedOn = DateTime.Now,
                }
            };
            TrainingProgram result = _trainingProgramRepository.CreateTrainingProgram(trainingProgram);
            return result;
        }



        public async Task<List<TrainingProgramViewModel>> GetAll(string? sortBy, int pagesize)
        {
            var trainingList = await _trainingProgramRepository.GetAll();
            int PageNumber = 1, PageSize= pagesize;
            var result = trainingList.Select(X => new TrainingProgramViewModel
            {
                Createby = X.HistoryTrainingPrograms.First().User.UserName,
                Createon = X.HistoryTrainingPrograms.First().ModifiedOn,
                Duration = GetDuration(X.Curricula.Select(x => x.Syllabus).ToList()),
                Id = X.Id,
                Name = X.Name,
                Status = X.Status,
            }).ToList();
             var result1 = result.Skip((PageNumber - 1) * PageSize).Take(PageSize).ToList();
            result1.OrderBy(cl => cl.Name).ToList();
            if (!string.IsNullOrEmpty(sortBy))
            {
                switch (sortBy)
                {
                    case "name_desc": result1 = result1.OrderByDescending(cl => cl.Name).ToList(); break;
                    case "name_asc": result1 = result1.OrderBy(cl => cl.Name).ToList(); break;
                    case "Id_desc": result1 = result1.OrderByDescending(cl => cl.Id).ToList(); break;
                    case "Id_asc": result1 = result1.OrderBy(cl => cl.Id).ToList(); break;
                    case "createby_desc": result1 = result1.OrderByDescending(cl => cl.Createby).ToList(); break;
                    case "createby_asc": result1 = result1.OrderBy(cl => cl.Createby).ToList(); break;
                    case "createon_desc": result1 = result1.OrderByDescending(cl => cl.Createon).ToList(); break;
                    case "createon_asc": result1 = result1.OrderBy(cl => cl.Createon).ToList(); break;
                    case "duration_desc": result1 = result1.OrderByDescending(cl => cl.Duration).ToList(); break;
                    case "duration_asc": result1 = result1.OrderBy(cl => cl.Duration).ToList(); break;
                    case "status_desc": result1 = result1.OrderByDescending(cl => cl.Status).ToList(); break;
                    case "status_asc": result1 = result1.OrderBy(cl => cl.Status).ToList(); break;


                }
            }

            return result1.ToList();
        }
        public async Task<bool> Delete(long id)
        {
            //getbyID
            var statusId = GetById(id);
            //check if status
            if (statusId.Status == 0)
            {
                throw new Exception("");
            }
            else
                //kep mess throw ex ("Mess")
                //Controller Try catch
                return await _trainingProgramRepository.Delete(id);
        }
        public TrainingProgram GetById(long Id)
        {
            TrainingProgram trainingprogram = null;
            trainingprogram = _trainingProgramRepository.GetbyId(Id);
            return trainingprogram;
        }

        public async Task<bool> Edit(long id, string name, int status)
        {
            return await _trainingProgramRepository.Edit(id, name, status);
        }
        public async Task<bool> DeActivate(long id)
        {
            return await _trainingProgramRepository.DeActivate(id);

        }
        public async Task<long> Duplicate(long id)
        {
            return await _trainingProgramRepository.Duplicate(id);

        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void SaveAsync()
        {
            _unitOfWork.commitAsync();
        }

        public async Task<List<TrainingProgramViewModel>> GetByFilter(List<string> programNames)
        {
            var trainingList = await _trainingProgramRepository.GetByFilter(programNames);
            var result = trainingList.Select(X => new TrainingProgramViewModel
            {
                Createby = X.HistoryTrainingPrograms.First().User.UserName,
                Createon = X.HistoryTrainingPrograms.First().ModifiedOn,
                Duration = GetDuration(X.Curricula.Select(x => x.Syllabus).ToList()),
                Id = X.Id,
                Name = X.Name,
                Status = X.Status,
            }).ToList();
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
    }
}