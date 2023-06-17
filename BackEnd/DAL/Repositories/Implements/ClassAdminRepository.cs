using DAL.Entities;
using DAL.Infrastructure;
using DAL.Repositories.Interfaces;

namespace DAL.Repositories.Implements
{
  public class ClassAdminRepository : RepositoryBase<ClassAdmin>, IClassAdminReporitory
  {
    private readonly FRMDbContext _dbContext;
    public ClassAdminRepository(IDbFactory dbFactory) : base(dbFactory)
    {
      this._dbContext = dbFactory.Init();
    }

    public void AddAdmin(ClassAdmin _classAdmin)
    {
      _dbSet.Add(_classAdmin);
    }

    public void DeleteAdmin(ClassAdmin classAdmin)
    {
      _dbSet.Remove(classAdmin);
    }

    public List<ClassAdmin> GetCLassAdmin(long idClass)
    {
      return _dbSet.Where(x => x.IdClass == idClass).ToList();
    }

    public ClassAdmin GetById(long id)
    {
      return _dbContext.ClassAdmins.FirstOrDefault(x => x.IdUser == id);
    }
    public IQueryable<ClassAdmin> GetClassAdminsById(long adminId)
    {
      return _dbSet.Where(ca => ca.IdUser == adminId);
    }

    public void AddAdminForImport(ClassAdmin classAdmin)
    {
      _dbSet.Add(classAdmin);
      _dbContext.SaveChanges();
      return;
    }
  }
}
