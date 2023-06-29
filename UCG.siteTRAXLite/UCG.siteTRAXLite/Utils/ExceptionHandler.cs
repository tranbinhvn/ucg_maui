using UCG.siteTRAXLite.Common.Constants;
using UCG.siteTRAXLite.DataContracts;

namespace UCG.siteTRAXLite.Utils
{
    public static class ExceptionHandler
    {
        public static string GetErrorMessage(ResponseCode code, string errorMsg = null)
        {
            if (!string.IsNullOrEmpty(errorMsg))
            {
                return errorMsg;
            }
            if (code == ResponseCode.APINOTFOUND)
            {
                return MessageStrings.Error_ServerNotFound;
            }
            else if (code == ResponseCode.APIWRONGVERION)
            {
                return MessageStrings.Error_WrongAPIVerion;
            }
            else if (code == ResponseCode.BADREQUEST)
            {
                return MessageStrings.Error_BadRequest;
            }
            else if (code == ResponseCode.CONNECTIONERROR)
            {
                return MessageStrings.Error_ConnectionError;
            }
            else if (code == ResponseCode.ERROR)
            {
                return MessageStrings.Error_SomeThingWrong;
            }
            else if (code == ResponseCode.NOPERMISSION)
            {
                return MessageStrings.Error_Permission;
            }
            else if (code == ResponseCode.SERVERERROR)
            {
                return MessageStrings.Error_Server;
            }
            else if (code == ResponseCode.UNAUTHORISE)
            {
                return MessageStrings.Error_Unauthorise;
            }
            return MessageStrings.Error_SomeThingWrong;
        }
    }
}
