using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Interfaces
{
  public interface ITrainingProgramRepository
  {
    Task<List<TrainingProgram>> GetAll();
    Task<bool> Delete(long id);
    Task<bool> DeActivate(long id);
    Task<long> Duplicate(long id);
    Task<List<TrainingProgram>> GetByFilter(List<string> programNames);
    Task<TrainingProgram> GetById(long id);
    Task<bool> Edit(long id, string name, int status);

    TrainingProgram GetbyId(long id);

    TrainingProgram Create(TrainingProgram trainingProgram);

    Task<List<TrainingProgram>> Search(string name);

        //Training Program
    List<TrainingProgram> GetTraingProgramAll();

    List<TrainingProgram> GetTraingProgramAllById(long programId);

    
    TrainingProgram CreateTrainingProgram(TrainingProgram trainingProgram);

    //team4
    List<TrainingProgram> GetAllForImport();
    void AddForImport(string name, int status);
        Syllabus GetSyllabus(long IdSyllabus);
    }
}
