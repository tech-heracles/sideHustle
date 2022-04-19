using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DbCore.DbShare
{
    public class clsAlternativaKushti
    {
        #region Atributet

        private int idAlternativaKushti;
        private string alternativa;
        private int idKushti;
        private string kodi;
        private DataRow rreshti;

        #endregion

        #region Properties

        public int IdAlternativaKushti { get { return idAlternativaKushti; } set { idAlternativaKushti= value; } }
        public string Alternativa { get { return alternativa; } set { alternativa = value; } }
        public int IdKushti { get { return idKushti; } set { idKushti = value; } }
        public string Kodi { get { return kodi; } set { kodi = value; } }

        #endregion

        #region Konstruktoret

        public clsAlternativaKushti()
        {

        }

        public clsAlternativaKushti(int idAlternativ)
        {
            if (idAlternativ == 0)
                return;
            using (clsDatabaseShare shareDB = new clsDatabaseShare())
            {
                mbushAlternativKusht(shareDB.merrAlternativKushtiSipasId(idAlternativ));
            }
        }

        public clsAlternativaKushti(int idAlternativ, clsDatabaseShare shareDB)
        {
            if (idAlternativ == 0)
                return;
            mbushAlternativKusht(shareDB.merrAlternativKushtiSipasId(idAlternativ));
        }

        public clsAlternativaKushti(int idalt, string alt, int idkusht)
        {
            idAlternativaKushti = idalt;
            alternativa = alt;
            idKushti = idkusht;
        }

        public clsAlternativaKushti(DataRow rreshti)
        {
            mbushAlternativKusht(rreshti);
        }

        #endregion

        #region Metoda Publike

        public clsMesazh ruaj()
        {
            clsDatabaseShare data = new clsDatabaseShare();
            clsMesazh u_ruajt = data.ruajAlternativKushti(idAlternativaKushti, alternativa, idKushti);
            data.Dispose();
            return u_ruajt;
        }

        public clsMesazh modifiko()
        {
            clsDatabaseShare data = new clsDatabaseShare();
            clsMesazh u_modifikua = data.modifikoAlternativKushti(idAlternativaKushti, alternativa, idKushti);
            data.Dispose();
            return u_modifikua;
        }

        public clsMesazh fshi()
        {
            clsDatabaseShare data = new clsDatabaseShare();
            clsMesazh u_fshi = data.fshiAlternativKushti(idAlternativaKushti);
            data.Dispose();
            return u_fshi;
        }

        /// <summary>
        /// kthen alternativen sipas id konfigurim ambjentit dhe kodit te kushtit
        /// </summary>
        /// <param name="idKonfigAmbjente"></param>
        /// <param name="kushtKod"></param>
        /// <returns></returns>
        public static string getAlternativa(int idKonfigAmbjente, string kushtKod)
        {
            using (clsDatabaseShare data = new clsDatabaseShare())
            {
                return data.ktheAlternativeSipasKushtitDheIdKonfig(idKonfigAmbjente, kushtKod);
            }
        }

        public static double? getVleraSipasId(int idKonfigAmbjente, string kushtKod)
        {
            using (clsDatabaseShare data = new clsDatabaseShare())
            {
                return data.ktheVlereSipasKushtitDheIdKonfig(idKonfigAmbjente, kushtKod);
            }
        }

        public static string getAlternativa(int idKonfigAmbjente, string kushtKod, clsDatabaseShare shareDB)
        {
            return shareDB.TransCache.ktheAlternativeSipasKushtitDheIdKonfig(idKonfigAmbjente, kushtKod, shareDB);
        }

        public static string getAlternativaSipasIdTrupiDok(int idTrupi, string kushtKod, clsDatabaseShare shareDB)
        {
            return shareDB.ktheAlternativeSipasKushtitDheIdTrupiDok(idTrupi, kushtKod);
        }

        public static double? ktheVlereReKushtShitje(int vlera)
        {
            using (clsDatabaseShare data = new clsDatabaseShare())
            {
                return data.ktheVlereKonfigAmbjentiSipasVleresKushttShitje(vlera);
            }
        }

        public static int ktheIdPerVlerePerdorues(String vlere)
        {
            using (clsDatabaseShare data = new clsDatabaseShare())
            {
                return data.ktheIdKonfigAmbjentiSipasVleresSeMinKushttShitje(vlere);
            }
        }

        public static string ktheVlereKushtiSipasIdAlternativeMultiselect(int vlera)
        {
            using (clsDatabaseShare data = new clsDatabaseShare())
            {
                return data.ktheVlereKushtiSipasIdAlternativeMultiselect(vlera);
            }
        }

        public static int ktheIdKonfigAmbjentiSipasVleresSeKushtitMultiselectLupa(string vlere)
        {
            using (clsDatabaseShare data = new clsDatabaseShare())
            {
                return data.ktheIdKonfigAmbjentiSipasVleresSeKushtitMultiselectLupa(vlere);
            }
        }

        public static int ktheVlereAlternativaKushti(DataTable kushtAlternativa, string kodi)
        {
            int idVleraKushti = -1;
            if (kushtAlternativa.Select("KODI = '" + kodi + "'").Length > 0)
            {
                DataRow kushti = kushtAlternativa.Select("KODI = '" + kodi + "'").First();
                return Convert.ToInt32(kushti["IDKUSHTEMPLATE"]);
            }
            return idVleraKushti;
        }

        #endregion

        #region Metoda Private

        private static string merrLlogariDheBijaSipasAlternativesZLL_B(int idAlternativa, int idNdermarrje)
        {
            using (clsDatabaseShare db = new clsDatabaseShare())
            {
                return db.merrLlogariDheBijaSipasAlternativesZLL_B(idAlternativa, idNdermarrje);
            }
        }

        #endregion

        #region Metoda Internal

        internal bool mbushAlternativKusht(DataRow dbDataRowAlternativaKushti)
        {
            if (dbDataRowAlternativaKushti != null)
            {
                try
                {
                    int.TryParse(dbDataRowAlternativaKushti["IDALTERNATIVEKUSHTI"].ToString(), out idAlternativaKushti);
                    int.TryParse(dbDataRowAlternativaKushti["IDKUSHTI"].ToString(), out idKushti);
                    alternativa = dbDataRowAlternativaKushti["ALTERNATIVA"].ToString();
                    return true;
                }
                catch (InvalidCastException)
                {
                    throw new Exception("ERROR: Gabim gjate marrjes se alternatives se kushtit nga db-ja");
                }
            }
            else
                return false;
        }

        internal static string[] merrLlogariDheBijaPerKushtin(int idKonfigAmbjente, string kushtKod, int idNdermarrje)
        {
            int alternativa = 1; //Kjo eshte alternativa e pare e shtuar (default)
            using (clsDatabaseShare data = new clsDatabaseShare())
            {
                alternativa = data.kthevlereSipasKushtitDheIdKonfig(idKonfigAmbjente, kushtKod);
            }
            return merrLlogariDheBijaSipasAlternativesZLL_B(alternativa, idNdermarrje).Trim().Split(',');
        }
        
        /// <summary>
        /// krijon nje objekt te kesaj klase duke u bazuar mbi nje sqlDataRecord
        /// </summary>
        /// <param name="record"></param>
        /// <returns></returns>
        internal static clsAlternativaKushti Krijo(IDataRecord record)
        {
            clsAlternativaKushti alternativaNew = new clsAlternativaKushti();
            int.TryParse(record["IDALTERNATIVEKUSHTI"].ToString(), out alternativaNew.idAlternativaKushti);
            int.TryParse(record["IDKUSHTI"].ToString(), out alternativaNew.idKushti);
            alternativaNew.kodi = record["Kodi"].ToString();
            alternativaNew.alternativa = record["ALTERNATIVA"].ToString();
            return alternativaNew;
        }

        #endregion

    }
}
