using UCG.siteTRAXLite.Common.Constants;
using UCG.siteTRAXLite.Entities.SorEforms;

namespace UCG.siteTRAXLite.Logics
{
    public static class LogicPriceCode563B
    {
        public static bool CheckLogic(string logic, ActionItemEntity action)
        {
            return !string.IsNullOrEmpty(logic)
                    && logic.Equals(LogicConstant.Logic_Price_Code_563B)
                    && action.Title.Equals(LogicConstant.L563B_Travel_Title);
        }

        public static bool CheckResponse(ActionItemEntity action)
        {
            return action.Response != null
                    && !string.IsNullOrEmpty(action.Response.Value)
                    && !string.IsNullOrEmpty(action.ResponseName);
        }
    }
}
