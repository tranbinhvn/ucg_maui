using UCG.siteTRAXLite.DataContracts;
using UCG.siteTRAXLite.DataContracts.SorEformContracts;

namespace UCG.siteTRAXLite.WebServices.SorEformServices
{
    public interface ISorEformService
    {
        Task<ResponseResult<List<SectionDTO>>> GetSections();
        Task<ResponseResult<List<BreadcrumbDTO>>> GetGenericSectionBreadcrumbs();
    }
}
