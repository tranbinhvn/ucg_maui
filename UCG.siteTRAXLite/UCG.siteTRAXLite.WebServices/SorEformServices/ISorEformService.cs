using UCG.siteTRAXLite.DataContracts;
using UCG.siteTRAXLite.DataContracts.SorEformContracts;

namespace UCG.siteTRAXLite.WebServices.SorEformServices
{
    public interface ISorEformService
    {
        Task<ResponseResult<List<string>>> GetOutcomeNames();
        Task<ResponseResult<List<ActionItemDto>>> GetActionsByOutcome(string outcomeName);
    }
}
