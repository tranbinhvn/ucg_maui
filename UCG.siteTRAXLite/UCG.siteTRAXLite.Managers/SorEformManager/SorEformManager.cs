﻿using Microsoft.Maui.Networking;
using UCG.siteTRAXLite.Entities.SorEforms;
using UCG.siteTRAXLite.Managers.Mappers;
using UCG.siteTRAXLite.WebServices.SorEformServices;

namespace UCG.siteTRAXLite.Managers.SorEformManager
{
    public class SorEformManager : ManagerBase, ISorEformManager
    {
        private readonly ISorEformService iSorEformService;
        public SorEformManager(
             IConnectivity connectivity,
             IServiceEntityMapper mapper,
             ISorEformService _iSorEformService) : base(connectivity, mapper)
        {
            iSorEformService = _iSorEformService;
        }

        public async Task<List<BreadcrumbEntity>> GetGenericSectionBreadcrumbs(bool isConnected = true)
        {
            try
            {
                var section = await iSorEformService.GetGenericSectionBreadcrumbs();

                return Mapper.Map<List<BreadcrumbEntity>>(section.Result);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<SectionEntity>> GetSections(bool isConnected = true)
        {
            try
            {
                var section = await iSorEformService.GetSections();

                return Mapper.Map<List<SectionEntity>>(section.Result);
            }
            catch(Exception ex)
            {
                return null;
            }

        }
    }
}
