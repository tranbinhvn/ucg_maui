﻿using Microsoft.Maui.Networking;
using UCG.siteTRAXLite.DataObjects;
using UCG.siteTRAXLite.Entities.SorEforms;
using UCG.siteTRAXLite.Entities.SorEforms.Sections;
using UCG.siteTRAXLite.Managers.Mappers;
using UCG.siteTRAXLite.Repositories.Hazard;
using UCG.siteTRAXLite.WebServices.SorEformServices;

namespace UCG.siteTRAXLite.Managers.SorEformManager
{
    public class SorEformManager : LocalManagerBase<HazardEntity, HazardDataObject, IHazardRepository>, ISorEformManager
    {
        private readonly ISorEformService iSorEformService;
        public SorEformManager(
             IConnectivity connectivity,
             IServiceEntityMapper mapper,
             ISorEformService _iSorEformService,
             IHazardRepository repo) : base(connectivity, mapper, repo)
        {
            iSorEformService = _iSorEformService;
        }

        public async Task<List<StepperEntity>> GetGenericSectionSteppers(bool isConnected = true)
        {
            try
            {
                var steppers = await iSorEformService.GetGenericSectionSteppers();

                return Mapper.Map<List<StepperEntity>>(steppers.Result);
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

        public async Task<SorClaimsStepperEntity> GetSorClaimsSteppers(bool isConnected = true)
        {
            try
            {
                var sorClaimsStepper = await iSorEformService.GetSorClaimsSectionSteppers();

                return Mapper.Map<SorClaimsStepperEntity>(sorClaimsStepper.Result);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<Take5StepperEntity> GetTake5Steppers(bool isConnected = true)
        {
            try
            {
                var take5Stepper = await iSorEformService.GetTake5Steppers();

                return Mapper.Map<Take5StepperEntity>(take5Stepper.Result);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<HazardEntity>> GetHazardsFromLocal(bool isConnected = true)
        {
            var response = repository.All();
            return Mapper.Map<List<HazardEntity>>(response);
        }

        public async Task<int> SaveHazard(HazardEntity hazardEntity, bool isConnected = true)
        {
            var dataObject = Mapper.Map<HazardDataObject>(hazardEntity);
            return repository.Save(dataObject);
        }

        public async Task<bool> SaveListHazard(List<HazardEntity> hazardEntities, bool isConnected = true)
        {
            var listDataObjects = Mapper.Map<List<HazardDataObject>>(hazardEntities);
            return repository.SaveList(listDataObjects);
        }

        public async Task<bool> DeleteAllHazards(bool isConnected = true)
        {
            try
            {
                repository.DeleteAll();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
