using DAL.Entities;

namespace BAL.Services.Interfaces
{
    public interface IClassAdminService
    {
        ClassAdmin GetById(long id);
        #region bhhiep
        Task<List<Class>> GetClassesById(long adminId); 
        #endregion
        void Save();
        void SaveAsync();
    }
}
