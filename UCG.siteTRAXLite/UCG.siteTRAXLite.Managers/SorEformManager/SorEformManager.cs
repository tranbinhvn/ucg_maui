using Microsoft.Maui.Networking;
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

        public async Task<List<string>> GetOutcomeNames( bool isConnected = true)
        {
            if (isConnected)
            {
                try
                {
                    var response = await iSorEformService.GetOutcomeNames();
                    CheckIfNetWorkOk(response.Message);

                    return Mapper.Map<List<string>>(response.Result);
                }
                catch (Exception ex)
                {
                    return null;
                }
            }

            return null;
        }

        public async Task<List<ActionItemEntity>> GetActionsByOutcome(string outcomeName, bool isConnected = true)
        {
            if (isConnected)
            {
                try
                {
                    var response = await iSorEformService.GetActionsByOutcome(outcomeName);
                    CheckIfNetWorkOk(response.Message);

                    return Mapper.Map<List<ActionItemEntity>>(response.Result);
                }
                catch (Exception ex)
                {
                    return null;
                }
            }

            return null;
        }
    }
}
