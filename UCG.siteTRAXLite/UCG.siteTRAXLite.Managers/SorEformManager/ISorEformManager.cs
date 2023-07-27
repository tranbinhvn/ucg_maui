using UCG.siteTRAXLite.Entities.SorEforms;

namespace UCG.siteTRAXLite.Managers.SorEformManager
{
    public interface ISorEformManager
    {
        Task<List<SectionEntity>> GetSections(bool isConnected = true);
        Task<List<BreadcrumbEntity>> GetGenericSectionBreadcrumbs(bool isConnected = true);
    }
}
