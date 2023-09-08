namespace UCG.siteTRAXLite.Common.Utils
{
    public class StringHelper
    {
        public static string ToBase64(byte[] bytes, bool addbase64Prefix = false)
        {
            var base64Str = Convert.ToBase64String(bytes, 0, bytes.Length);
            if (addbase64Prefix)
            {
                base64Str = $"data:image/png;base64,{base64Str}";
            }
            return base64Str;
        }

        public static byte[] FromBase64(string base64Str)
        {
            var data = base64Str;
            if (base64Str.Contains("base64,"))
            {
                var replaceString = base64Str.Split(',')[0];
                data = base64Str.Replace($"{replaceString},", string.Empty);
            }
            return Convert.FromBase64String(data);
        }

        public static string AppendWithComma(string s, string append)
        {
            string appendStr = append ?? string.Empty;
            if (string.IsNullOrEmpty(s))
                return appendStr;
            else if (!string.IsNullOrEmpty(appendStr))
                return $"{s}, {appendStr}";
            else
                return s;
        }

        public static string BrowseFileKey(string data)
        {
            var result = "ImagesSelected";
            if (!string.IsNullOrEmpty(data))
                result = $"{result}_{data}";
            return result;
        }
    }
}
