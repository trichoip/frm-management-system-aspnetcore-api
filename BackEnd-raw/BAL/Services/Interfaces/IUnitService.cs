using BAL.Models;

namespace BAL.Services.Interfaces
{
    public interface IUnitService
    {

        // Team6
        List<UnitViewModel> GetUnits(long id);
        // Team6
        void Save();
        void SaveAsync();
    }
}
