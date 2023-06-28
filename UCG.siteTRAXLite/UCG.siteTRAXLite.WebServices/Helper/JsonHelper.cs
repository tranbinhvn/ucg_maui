using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UCG.siteTRAXLite.WebServices.Helper
{
    public static class JsonHelper
    {
        public static async Task<T> FromString<T>(string serialized) where T : class
        {
            try
            {
                var result = await Task.Run(() =>
                  JsonConvert.DeserializeObject<T>(serialized));
                return result;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
