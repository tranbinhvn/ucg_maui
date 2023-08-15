using UCG.siteTRAXLite.DataContracts;
using UCG.siteTRAXLite.DataContracts.SorEformContracts;
using UCG.siteTRAXLite.DataContracts.SorEformContracts.Sections;
using UCG.siteTRAXLite.Entities.SorEforms.Sections;

namespace UCG.siteTRAXLite.WebServices.SorEformServices
{
    public interface ISorEformService
    {
        Task<ResponseResult<List<SectionDTO>>> GetSections();
        Task<ResponseResult<List<StepperDTO>>> GetGenericSectionSteppers();
        Task<ResponseResult<Take5StepperDTO>> GetTake5Steppers();
        Task<ResponseResult<SorClaimsStepperDTO>> GetSorClaimsSectionSteppers();
    }
}
