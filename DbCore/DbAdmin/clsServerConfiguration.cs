using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DbCore.IMBUtils.Types;

namespace DbCore.DbAdmin
{
    public class clsServerConfiguration 
    {

        private Dictionary<string, object> configs;

        public Dictionary<string, object> Configs
        {
            get
            {
                return configs;
            }

            set
            {
                configs = value;
            }
        }

        public ICollection Keys
        {
            get
            {
                return ((IDictionary)configs).Keys;
            }
        }

        public ICollection Values
        {
            get
            {
                return ((IDictionary)configs).Values;
            }
        }

        public bool IsReadOnly
        {
            get
            {
                return ((IDictionary)configs).IsReadOnly;
            }
        }


        public int Count
        {
            get
            {
                return ((IDictionary)configs).Count;
            }
        }


        public object this[object key]=> ((IDictionary)configs)[key];

        public clsServerConfiguration()
        {
            configs = merrGjitheKonfigurimet();
        }

        public static Dictionary<string, object> merrGjitheKonfigurimet()
        {
            using (clsDatabaseAdmin dbAdmin = new clsDatabaseAdmin())
            {
                return dbAdmin.LexoGjitheKonfigurimetNgaDb();
            }
        }

        private static object LexoKonfigurimSipasKey(string key, string connString = "")
        {
            using (clsDatabaseAdmin dbAdmin = new clsDatabaseAdmin(connString))
            {
                return dbAdmin.LexoKonfigurimSipasKeyNgaDb(key);
            }
        }
        public static T LexoKonfigurimSipasKey<T>(ServerKonfigKey key)
        {
            return LexoKonfigurimSipasKey<T>(key,"");
        }
        public static T LexoKonfigurimSipasKey<T>(ServerKonfigKey key, string connName)
        {
            return Converter.MerrVlereOseDefault<T>(LexoKonfigurimSipasKey(key.ToString(), connName));
        }
        public bool Contains(object key)
        {
            return ((IDictionary)configs).Contains(key);
        }


        public IDictionaryEnumerator GetEnumerator()
        {
            return ((IDictionary)configs).GetEnumerator();
        }

        public void CopyTo(Array array, int index)
        {
            ((IDictionary)configs).CopyTo(array, index);
        }
    }
    public enum ServerKonfigKey
    {
        Klienti,
        Gjuha_Default,
        DERGO_PIN_SMS,
        DOMAINNAME,
        GIS_GEOSERVER_REQTIMEOUT,
        GIS_USERNAME_DEFAULT,
        GIS_USERPASSWORD_DEFAULT,
        NODE_SERVER_URL,
        URL_MOBILE,
        DEFAULT_PAGE_MOBILE,
        LOGIN_DEFAULT_VIT_AKTUAL,
        FAKE_RESPONSE,
        fakePin,
        LOADING_URL,
        FORMAT_EKSPORTI_TRANSFER_USH,
        FORMAT_EKSPORTI_TRANSFER_USH_KPP,
        FORMAT_EKSPORTI_TRANSFER_KTHIM,
        OrariPunesVod,
        GOLDEN_MESAZH_FITUES,
        PLUS_MESAZH_FITUES, 
        MPESA_MESAZH_SUKSESI,
        MPESA_MESAZH_DESHTIMI,
        OTC_PAGESA_TIMEOUT,
        MPESA_RECEIVER_PARTY_,
        OTC_SMS_KONFIG,
        MPESA_DEFAULT_REQUEST,
        GOLDEN_KOD_FITUES,
        GOLDEN_DISCOUNT,
        GOLDEN_COST,
        SMS_SENDER_CONFIG,
        KONFIGURIM_TRANSFERIM_USH_FDTK,
        MAXRETRY_TRANS,
        Verbosity,
        IP_KASE,
        SESSION_CACHE_TIMEOUT,
        CSV_ENCODING,
        URL_SUBJEKTEPASIV,
        DOC_SAVING_TIMEOUT_IN_MIN
    }
}
