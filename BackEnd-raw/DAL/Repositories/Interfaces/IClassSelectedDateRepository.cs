using DAL.Entities;

namespace DAL.Repositories.Interfaces
{
    public interface IClassSelectedDateRepository
    {
        IEnumerable<ClassSelectedDate> GetClassSelectedDateAll(DateTime? date);
        IEnumerable<ClassSelectedDate> GetClassSelectedDateByID(long idClass, DateTime? date);
        ClassSelectedDate GetByIdClass(long idClass, DateTime? date);
        ClassSelectedDate GetByIdClassFilter(long idClass, long idClassFilter, DateTime? date);
        void AddSeletedDate(ClassSelectedDate _classSelectedDate);
        Task<List<ClassSelectedDate>> GetSelectedDatesQueryAsync();
        List<ClassSelectedDate> GetSeletedDateListById(long ClassId);
    }
}
