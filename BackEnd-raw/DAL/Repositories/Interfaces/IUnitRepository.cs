using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Interfaces
{
    public interface IUnitRepository
    {
        //team01
        List<Unit> GetAllSessionUnit(long id);
        void UpdateIndex(long id, int newIndex);
        Unit GetById(long id);
        void Update(Unit entity);
        Unit Create(Unit unit);
        void DeleteUnit(long id);
        void Deactivate(long id);
        void Activate(long id);
        //team 01

        //team 06
        List<Unit> GetUnits(long id);
        //team 06

        //team4
        List<Unit> GetAllUnits();
        List<Unit> GetSessionUnits(long idSession);
    }
}
