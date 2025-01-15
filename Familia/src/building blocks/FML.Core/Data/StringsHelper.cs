namespace FML.Core.Data
{
    public static class StringsHelper
    {
        public static string ConvertStreamToBase64(Stream? stream)
        {
            if (stream == null)
            {
                return string.Empty;
            }

            using (var memoryStream = new MemoryStream())
            {
                stream.CopyTo(memoryStream);
                return Convert.ToBase64String(memoryStream.ToArray());
            }
        }
    }
}
