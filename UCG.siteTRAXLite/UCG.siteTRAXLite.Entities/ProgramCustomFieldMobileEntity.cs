using System.Globalization;

namespace UCG.siteTRAXLite.Entities
{
    public class ProgramCustomFieldMobileEntity
    {
        public string CustomFieldName { get; set; }
        public int? DisplayOrder { get; set; }
        public string Value { get; set; }
        public string FieldType { get; set; }

        public string DisplayValue
        {
            get
            {
                if (!string.IsNullOrEmpty(FieldType) && (FieldType.ToLower().Equals("date") ||!string.IsNullOrEmpty(FieldType) && FieldType.ToLower().Equals("date/time")))
                {
                    if (!string.IsNullOrEmpty(Value) && !Value.Contains("0001-01-01T00:00:00.0000000"))
                    {
                        try
                        {
                            var answer = Convert.ToDateTime(Value, CultureInfo.CurrentCulture);
                            if (FieldType.ToLower().Equals("date"))
                                return answer.ToString("dd/MM/yyyy", CultureInfo.CurrentCulture);
                            else
                                return answer.ToString("dd/MM/yyyy h:mm tt", CultureInfo.CurrentCulture);
                        }
                        catch (Exception) { }
                    }
                }
                return Value;
            }
        }
    }
}
