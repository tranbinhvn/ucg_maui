using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UCG.siteTRAXLite.DataContracts.SorEformContracts;
using UCG.siteTRAXLite.DataContracts;
using UCG.siteTRAXLite.Entities.SorEforms;

namespace UCG.siteTRAXLite.Managers.SorEformManager
{
    public interface ISorEformManager
    {
        Task<List<string>> GetOutcomeNames(bool isConnected = true);
        Task<List<ActionItemEntity>> GetActionsByOutcome(string outcomeName, bool isConnected = true);
    }
}
