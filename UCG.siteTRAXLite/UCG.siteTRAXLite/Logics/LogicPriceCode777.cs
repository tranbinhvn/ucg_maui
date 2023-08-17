using UCG.siteTRAXLite.Common.Constants;
using UCG.siteTRAXLite.Entities.SorEforms;

namespace UCG.siteTRAXLite.Logics
{
    public static class LogicPriceCode777
    {
        public static PriceCodeEntity GetPriceCode(List<ActionItemEntity> actions)
        {
            var priceCode777 = new PriceCodeEntity();

            var priceCodeQuestions = actions.Where(a => !string.IsNullOrEmpty(a.Logic) && a.Logic.Equals(MessageStrings.Logic_Price_Code_777, StringComparison.OrdinalIgnoreCase));

            if (priceCodeQuestions == null || !priceCodeQuestions.Any())
                return null;
                
            priceCode777.PriceCode = "777";
            var numberOfMeterQuestions = priceCodeQuestions.FirstOrDefault(a => a.Title.Equals(MessageStrings.Number_Of_Meters_Question, StringComparison.OrdinalIgnoreCase));

            if (numberOfMeterQuestions == null || numberOfMeterQuestions.EResponseType != SorEformsResponseType.Number)
                return null;   

            if (int.TryParse(numberOfMeterQuestions.Response.Value, out int response))
            {
                if (response < 5)
                {
                    priceCode777.QTY = "1";
                }
                else if (response >= 5 && response <= 10)
                {
                    priceCode777.QTY = "3";
                }
                else
                {
                    priceCode777.QTY = "submit for review";
                }
            }

            return priceCode777;
        }
    }
}
