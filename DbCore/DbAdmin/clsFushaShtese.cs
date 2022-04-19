using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Globalization;
using System.Resources;
using AlphaWeb.Core.Interfaces.Data;
using DbCore.IMBUtils.DataBase;
using DbCore.IMBUtils.Validation;
using DbCore.IMBUtils.Messages;

namespace DbCore.DbAdmin
{
    /// <summary>
    ///  Kjo eshte klasa qe sherben per objektet qe perfaqsojne nje fushe shtese
    ///  qe i perket nje modeli te caktuar(Te dhenat  merren nga tabela : T_FUSHASHTESE)
    /// </summary>
    public class clsFushaShtese : IDataBase
    {
        #region Atributet

        private int idFushaShtese;
        private int idModeliFushaShtese;
        private string pershkrimiFushaShtese;
        private int tipiFushaShtese;
        private int gjatesiaFushaShtese;
        private int atiTipiFushaShtese;
        private string pershkrimiPrindi;
        private bool detyrueshme;
        private bool lejueshme;
        private bool shfaq;
        private string vlereDefault;
        private string pershkrimiEng;
        private string kodi;
        private int idGjuha;
        private string shenime;
        private DataRow rreshti;
        private CultureInfo ci;//= new CultureInfo("sq-AL");
        private ResourceManager rm=new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));




        #endregion Atributet

        #region Konstruktoret

        /// <summary>
        /// Konstruktori i klases
        /// </summary>
        public clsFushaShtese(int idfushashtese, int idmodelifushashtese, String pershkrimifushashtese, int tipifushashtese, int gjatesiafushashtese, int atitipifushashtese, string vlereDefault, bool shfaq, bool lejueshme, bool detyrueshme, string kodi, string pershkrimiEng, string shenime, int idgjuha)
        {
           
                idFushaShtese = idfushashtese;
                idModeliFushaShtese = idmodelifushashtese;
                pershkrimiFushaShtese = pershkrimifushashtese;
                tipiFushaShtese = tipifushashtese;
                gjatesiaFushaShtese = gjatesiafushashtese;
                atiTipiFushaShtese = atitipifushashtese;
                Shfaq = shfaq;
                Lejueshme = lejueshme;
                Detyrueshme = detyrueshme;
                Kodi = kodi;
                PershkrimiEng = pershkrimiEng;
                VlereDefault = vlereDefault;
                Shenime = shenime;
                idGjuha = idgjuha;





        }


        /// <summary>
        /// konstruktor me 1 parameter
        /// </summary>
        /// <param name="kodi">kod i fushes shtese</param>
        public clsFushaShtese(String kodi)
        {
            using (clsDatabaseAdmin data = new clsDatabaseAdmin())
                data.ktheFushatShteseSipasKodit(kodi, this);

        }

        /// <summary>
        /// Konstruktori default i klases
        /// </summary>
        public clsFushaShtese()
        {
        }

        #endregion Konstruktoret

        #region Properties

        /// <summary>
        /// Kthen/Vendos ID-ne qe gjenerohet automatikisht.
        /// </summary>
        public int IdFushaShtese
        {
            get
            {
                return idFushaShtese;
            }
            set
            {
                idFushaShtese = value;
            }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e modelit te ciles i perket kjo fushe.
        /// </summary>
        public int IdModeliFushaShtese
        {
            get
            {
                return idModeliFushaShtese;
            }
            set
            {
                idModeliFushaShtese = value;
            }
        }

        /// <summary>
        /// Kthen/Vendos pershkrimin e fushes.
        /// </summary>
        public String PershkrimiFushaShtese
        {
            get { return pershkrimiFushaShtese; }
            set { pershkrimiFushaShtese = value; }
        }

        /// <summary>
        /// Kthen/Vendos tipin e fushes shtese, nese eshte String, Integer etj. Per me shume te
        /// shikohet tabela "??!"
        /// </summary>
        public int TipiFushaShtese
        {
            get { return tipiFushaShtese; }
            set { tipiFushaShtese = value; }
        }

        /// <summary>
        /// Kthen/Vendos gjatesine maksimale qe i caktohet kesaj fushe.
        /// </summary>
        public int GjatesiaFushaShtese
        {
            get { return gjatesiaFushaShtese; }
            set { gjatesiaFushaShtese = value; }
        }
        /// <summary>
        /// Kthen/Vendos shenime
        /// </summary>
        public string Shenime
        {
            get { return shenime; }
            set { shenime = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e atit per kete fushe. Ati perdoret ne rastin kur kemi titull
        /// apo tip listbox
        /// </summary>
        public int AtiTipiFushaShtese
        {
            get { return atiTipiFushaShtese; }
            set { atiTipiFushaShtese = value; }
        }

        public string PershkrimiPrindit { get { return pershkrimiPrindi; } set { pershkrimiPrindi = value; } }

        public string Kodi
        {
            get
            {
                return kodi;
            }

            set
            {
                kodi = value;
            }
        }

        public string PershkrimiEng
        {
            get
            {
                return pershkrimiEng;
            }

            set
            {
                pershkrimiEng = value;
            }
        }

        public string VlereDefault
        {
            get
            {
                return vlereDefault;
            }

            set
            {
                vlereDefault = value;
            }
        }

        public bool Shfaq
        {
            get
            {
                return shfaq;
            }

            set
            {
                shfaq = value;
            }
        }

        public bool Lejueshme
        {
            get
            {
                return lejueshme;
            }

            set
            {
                lejueshme = value;
            }
        }

        public bool Detyrueshme
        {
            get
            {
                return detyrueshme;
            }

            set
            {
                detyrueshme = value;
            }
        }

        public int IdGjuha
        {
            get
            {
                return idGjuha;
            }

            set
            {
                idGjuha = value;
            }
        }
        #endregion Properties

        #region Metoda Private
        private clsMesazh kontrolloFushaShtese()
        {
            //if (kodi == "")            
            //        return new clsMesazh(false, "Plotesoni kodin e fushes shtese!");
            clsMesazh kontrollkodiFushaShtese = clsFunksione.kontrolloKaraktereMeMesazh(kodi, FusheKontrolli.Kodi, false);
            if (!kontrollkodiFushaShtese.Status)
                return kontrollkodiFushaShtese;

            //if (pershkrimiFushaShtese == "")
            //    return new clsMesazh(false, "Plotesoni pershkrimin e fushes shtese!");
            clsMesazh kontrollpershkrimiFushaShtese = clsFunksione.kontrolloKaraktereMeMesazh(pershkrimiFushaShtese, FusheKontrolli.Emri, true);
            if (!kontrollpershkrimiFushaShtese.Status)
                return kontrollpershkrimiFushaShtese;


            //if (pershkrimiEng == "")
            //    return new clsMesazh(false, "Plotesoni pershkrimin anglisht te fushes shtese!");
            clsMesazh kontrollpershkrimienfFushaShtese = clsFunksione.kontrolloKaraktereMeMesazh(pershkrimiEng, FusheKontrolli.Emri, true);
            if (!kontrollpershkrimienfFushaShtese.Status)
                return kontrollpershkrimienfFushaShtese;

            return new clsMesazh(true, "Kontrollet e Fushave Shtese u kaluan me sukses");
        }
        #endregion


        #region Metoda Publike

        public clsMesazh Ruaj()
        {
            var idFS = 0;
           

                clsMesazh mesazh = this.kontrolloFushaShtese();
            if (!mesazh.Status)
                throw new Exception(mesazh.PershkrimMesazhi);
        

    

            using (clsDatabaseAdmin dbAdmin = new clsDatabaseAdmin())
            {
                var mes = dbAdmin.ruajFushaShtese(out idFS, IdModeliFushaShtese, PershkrimiFushaShtese, TipiFushaShtese, GjatesiaFushaShtese, AtiTipiFushaShtese, Shfaq, Detyrueshme, Lejueshme, VlereDefault, Kodi, PershkrimiEng, Shenime);
                IdFushaShtese = idFS;
                return mes;
            }
        }
        public clsMesazh Modifiko()
        {
           
              clsMesazh mesazh = this.kontrolloFushaShtese();
                if (!mesazh.Status)
                    throw new Exception(mesazh.PershkrimMesazhi);
            

            using (clsDatabaseAdmin dbAdmin = new clsDatabaseAdmin())
            {
                return dbAdmin.modifikoFushaShtese(IdFushaShtese, IdModeliFushaShtese, PershkrimiFushaShtese, TipiFushaShtese, GjatesiaFushaShtese, AtiTipiFushaShtese, Shfaq, Detyrueshme, Lejueshme, VlereDefault, Kodi, PershkrimiEng, Shenime);
            }
        }
        public clsMesazh Fshi()
        {
            using (clsDatabaseAdmin dbAdmin = new clsDatabaseAdmin())
            {
                return dbAdmin.fshiFushaShtese(IdFushaShtese);
            }

        }

        internal clsMesazh ruajFushePerLidhjetEkzistuese()
        {
            using (var dbAdmin = new clsDatabaseAdmin())
            {
                return dbAdmin.ruajVleraPerLidhjetEkzistuese(0, IdFushaShtese, VlereDefault, IdModeliFushaShtese);
            }
        }

        public void Mbush(IDataRecord dbDataRowFushShtese)
        {
            int.TryParse(dbDataRowFushShtese["IDFUSHASHTESE"].ToString(), out idFushaShtese);
            int.TryParse(dbDataRowFushShtese["IDMODELIFUSHASHTESE"].ToString(), out idModeliFushaShtese);
            pershkrimiFushaShtese = dbDataRowFushShtese["PERSHKRIMIFUSHASHTESE"].ToString();
            int.TryParse(dbDataRowFushShtese["TIPIFUSHASHTESE"].ToString(), out tipiFushaShtese);
            int.TryParse(dbDataRowFushShtese["GJATESIAFUSHASHTESE"].ToString(), out gjatesiaFushaShtese);
            int.TryParse(dbDataRowFushShtese["ATITIPIFUSHASHTESE"].ToString(), out atiTipiFushaShtese);

            bool.TryParse(dbDataRowFushShtese["LEJUESHME"].ToString(), out lejueshme);
            bool.TryParse(dbDataRowFushShtese["SHFAQ"].ToString(), out shfaq);
            bool.TryParse(dbDataRowFushShtese["DETYRUESHME"].ToString(), out detyrueshme);
            vlereDefault = dbDataRowFushShtese["VLEREDEFAULT"].ToString();
            pershkrimiEng = dbDataRowFushShtese["PERSHKRIMIENG"].ToString();
            shenime = dbDataRowFushShtese["SHENIME"].ToString();
            kodi = dbDataRowFushShtese["KODI"].ToString();
        }
        #endregion
        #region Metoda Internal


        #endregion Metoda Internal
    }
}