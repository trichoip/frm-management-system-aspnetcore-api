using DAL.Entities;

namespace DAL.Repositories.Interfaces
{
  public interface IClassRepository
  {
    Task<bool> DeleteClass(long id);
    Class GetById(long Id);
    Task<bool> DeActivate(long id);
    Task<long> Duplicate(long Id);
    Task<Class> GetDetail(long id);
    Task<Class> GetAttende(long idClass);
    Task<Class> GetTrainingProgram(long id);
    void UpdateClass(Class _class);

    IEnumerable<Class> GetClassess(List<string> key, List<long> location, DateTime? classTimeFrom, DateTime? classTimeTo, List<long> classTime, List<long> status, List<long> attendee, int FSU, int trainer);
    IEnumerable<Class> GetWithFilter(Class entity);
    Task<Class> GetClassById(long idClass);

    //GetClass
    Task<List<Class>> GetClassByClassCode(string classCode);
    List<Class> GetClasses();
        Syllabus GetSyllabus(long IdSyllabus);
    #region bhhiep
    IQueryable<Class> GetClassesQuery();
    #endregion
    void CreateClassForImport(Class _class);
    int CountClass();
    }
}


