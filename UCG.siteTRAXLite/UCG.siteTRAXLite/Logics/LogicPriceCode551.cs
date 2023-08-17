using UCG.siteTRAXLite.Common.Constants;
using UCG.siteTRAXLite.Entities.SorEforms;

namespace UCG.siteTRAXLite.Logics
{
    public static class LogicPriceCode551
    {
        public static bool CheckLogic(string logic, ActionItemEntity action)
        {
            return !string.IsNullOrEmpty(logic)
                    && logic.Equals(LogicConstant.Logic_Price_Code_551)
                    && action.Title.Equals(LogicConstant.LPC551_Travel_Title);
        }

        public static bool CheckResponse(ActionItemEntity action)
        {
            return action.Response != null
                    && !string.IsNullOrEmpty(action.Response.Value)
                    && !string.IsNullOrEmpty(action.ResponseName);
        }

        public static string GetValidation(ResponseDataItemEntity data)
        {
            if (data.Value.Equals(LogicConstant.LPC551_FM_Authorisation_Option))
            {
                return LogicConstant.LPC551_FM_Authorisation_Message;
            }
            if (data.Value.Equals(LogicConstant.LPC551_RM_Authorisation_Option))
            {
                return LogicConstant.LPC551_RM_Authorisation_Message;
            }

            return "";
        }
    }
}
