using DAL.Entities;
using BAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace BAL.Services.Interfaces
{
    public interface ITrainingProgramService
    {
        TrainingProgram CreateTrainingProgram(ProgramViewModel newProgram);
        void Save();
        void SaveAsync();
        Task<List<TrainingProgramViewModel>> GetAll(string? sortBy, int pagesize);
        Task<bool> Delete(long id);
        Task<bool> Edit(long id, string name, int status);
        Task<bool> DeActivate(long id);
        Task<long> Duplicate(long id);
        Task<List<TrainingProgramViewModel>> GetByFilter(List<string> programNames);
    }
}


