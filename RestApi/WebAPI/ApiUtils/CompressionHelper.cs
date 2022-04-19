using System.IO;

namespace RestApi.WebAPI.ApiUtils
{
    /// <summary>
    /// klase helper per kompresimin e byteve 
    /// </summary>
    public class CompressionHelper
    {
        public static byte[] KompresoMeGZip(byte[] str)
        {
            if (str == null){
                return null;
            }

            using (var output = new MemoryStream())
            {
                using (
                    var compressor = new Ionic.Zlib.GZipStream(
                    output, Ionic.Zlib.CompressionMode.Compress,
                    Ionic.Zlib.CompressionLevel.BestSpeed))
                {
                    compressor.Write(str, 0, str.Length);
                }

                return output.ToArray();
            }
        }
    }
}
