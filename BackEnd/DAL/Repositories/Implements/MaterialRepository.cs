using DAL.Entities;
using DAL.Infrastructure;
using DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories.Implements
{
    public class MaterialRepository : RepositoryBase<Material>, IMaterialRepository
    {
        // Team6
        private readonly FRMDbContext _dbContext;
        public MaterialRepository(IDbFactory dbFactory) : base(dbFactory)
        {
            this._dbContext = dbFactory.Init();
        }

        public void DeleteMaterial(long id)
        {
            var result = _dbSet.FirstOrDefault(x => x.Id == id);
            if (result != null)
            {
                result.Status = 3;
            }
            //0. Active, 1.Deactive, 2.Draft, 3.Delete
        }



        // Team6
        public List<Material> GetLessonMaterials(long lessonId)
        {
            return _dbSet.Where(x => x.IdLesson == lessonId
            && x.Status != 3).ToList();
        }
        public Material Create(Material material)
        {
            _dbSet.Add(material);
            return material;
        }

        public void Update(Material material)
        {
            _dbSet.Update(material);
        }

        public Material GetById(long id)
        {
            return _dbSet.Include(x => x.HistoryMaterials).FirstOrDefault(x => x.Id == id && x.Status != 3);
        }
        public void Deactivate(long id)
        {
            var result = _dbSet.FirstOrDefault(x => x.Id == id);
            if (result != null && result.Status != 3)
            {
                result.Status = 0;
                _dbSet.Update(result);

                //Save Changes by UnitOfWork Commit()
            }
            else
            {
                throw new Exception("No material with that id");
            }
        }
        public void Activate(long id)
        {
            var result = _dbSet.FirstOrDefault(x => x.Id == id);
            if (result != null && result.Status != 3)
            {
                result.Status = 1;
                _dbSet.Update(result);

                //Save Changes by UnitOfWork Commit()
            }
            else
            {
                throw new Exception("No unit with that id");
            }
        }
    }
}
