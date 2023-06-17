using BAL.Models;
using DAL.Entities;
using static DAL.Entities.Class;

namespace BAL.Services.Interfaces
{
  public interface IClassService
  {
    List<Class> GetClassess(List<string>? key, List<string>? sortBy, List<long> location, DateTime? classTimeFrom, DateTime? classTimeTo, List<long> classTime, List<long> status, List<long> attendee, int FSU, int trainer, int pageNumber, int pageSize);
    List<ClassModel> ShowClasses(List<Class> classes);
    int CountClass(List<string>? key, List<long> location, DateTime? classTimeFrom, DateTime? classTimeTo, List<long> classTime, List<long> status, List<long> attendee, int FSU, int trainer);
    //Get Class
    Task UpdateClass(UpdateClassViewModel classViewModel);
    Task SaveAsDraft(UpdateClassViewModel classViewModel);
    //Training Program
    ClassViewModel GetById(long Id);
    Task<bool> Delete(long id);
    Task<bool> DeActivate(long id);
    Task<long> Duplicate(long id);
    Task<ClassDetailViewModel> GetDetail(long id);
    Task<List<ClassAttendeeViewModel>> GetClassAttendee(long idClass, int PageNumber, int PageSize);
    Task<ClassDetailTrainingViewModel> GetTrainingProgram(long id);

    IEnumerable<ClassViewModel> GetClassFilter(Class @class);
    IEnumerable<StudentClassViewModel> GetStudentClass(Class @class);
    Task<ClassCalenderViewModel> GetClassCalender(long idClass);
    IEnumerable<Class> GetClasses();

    //Get Class
    Task<List<ClassSearchViewModel>> GetClassByCodeService(string? classCode);

    //Training Program
    Task<List<ClassTrainingProgamViewModel>> GetClassTrainingProgams(string? name);
    public Task<ImportClassResponse> ImportCLasses(ImportClassRequest request, string path);

    void Save();
    void SaveAsync();
  }
}
