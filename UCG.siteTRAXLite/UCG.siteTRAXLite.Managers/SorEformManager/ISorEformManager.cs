using System.Linq.Expressions;
using UCG.siteTRAXLite.DataObjects;
using UCG.siteTRAXLite.Entities.SorEforms;
using UCG.siteTRAXLite.Entities.SorEforms.Sections;
using UCG.siteTRAXLite.Repositories.Hazard;

namespace UCG.siteTRAXLite.Managers.SorEformManager
{
    public interface ISorEformManager : ILocalManagerBase<HazardEntity, HazardDataObject, IHazardRepository>
    {
        Task<List<SectionEntity>> GetSections(bool isConnected = true);
        Task<List<StepperEntity>> GetGenericSectionSteppers(bool isConnected = true);
        Task<Take5StepperEntity> GetTake5Steppers(bool isConnected = true);
        Task<SorClaimsStepperEntity> GetSorClaimsSteppers(bool isConnected = true);
        Task<int> SaveHazard(HazardEntity hazardEntity, bool isConnected = true);
        Task<bool> SaveListHazard(List<HazardEntity> hazardEntities, bool isConnected = true);
        Task<List<HazardEntity>> GetHazardsFromLocal(bool isConnected = true);
        Task<bool> DeleteAllHazards(bool isConnected = true);
    }
}
