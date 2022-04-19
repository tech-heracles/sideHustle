using DbCore.IMBUtils.Types;
using DevExpress.Web;
using Newtonsoft.Json;

namespace AlphaWebCommon.WebUtils.ASPxControlExtensions
{
    public static class ASPxHiddenFieldExtensions
    {
        /// <summary>
        /// merr nje vlere nga hidden field dhe e kthen ate ne tipin e kerkuar
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="hf"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static T Get<T>(this ASPxHiddenField hf, string key)
        {
            var vleraNgaHf = hf.Get(key);
            return Converter.MerrVlereOseDefault<T>(vleraNgaHf);
        }

        /// <summary>
        /// perdoret per te marr nje vlere nga hidden field dhe per ta deserializuar ate ne tipin e kerkuar
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="hf"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static T GetObject<T>(this ASPxHiddenField hf, string key)
        {
            var vleraNgaHf = hf.Get<string>(key);
            return string.IsNullOrWhiteSpace(vleraNgaHf) ? default(T) : JsonConvert.DeserializeObject<T>(vleraNgaHf);
        }

        /// <summary>
        /// Ruan te serializuar nje vlere ne hiddenfield
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="hf"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static void SetObject<T>(this ASPxHiddenField hf, string key, T value)
        {      
            hf.Set(key, JsonConvert.SerializeObject(value));
        }
    }
}