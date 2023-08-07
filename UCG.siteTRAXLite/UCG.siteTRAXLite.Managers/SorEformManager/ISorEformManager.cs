using UCG.siteTRAXLite.Entities.SorEforms;
using UCG.siteTRAXLite.Entities.SorEforms.Sections;

namespace UCG.siteTRAXLite.Managers.SorEformManager
{
    public interface ISorEformManager
    {
        Task<List<SectionEntity>> GetSections(bool isConnected = true);
        Task<List<StepperEntity>> GetGenericSectionSteppers(bool isConnected = true);
        Task<Take5StepperEntity> GetTake5Steppers(bool isConnected = true);
        Task<SorClaimsStepperEntity> GetSorClaimsSteppers(bool isConnected = true);
    }
}
