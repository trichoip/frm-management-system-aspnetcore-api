using BAL.Models;

namespace BAL.Services.Interfaces
{
    public interface IMaterialService
    {
        // Team6
        List<MaterialViewModel> GetMaterials(long id);
        public void DeleteMaterial(long id);
        public void UpdateMaterial(MaterialViewModel materialViewModel);
        // Team6
        void Save();
        void SaveAsync();
    }
}
