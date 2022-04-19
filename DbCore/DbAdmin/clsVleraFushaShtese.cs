using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using AlphaWeb.Core.Interfaces.Data;

namespace DbCore.DbAdmin
{
    /// <summary>
    ///  Kjo eshte klasa qe sherben per objektet qe perfaqsojne vlerat qe i caktohen fushave shtese
    ///  (Te dhenat  merren nga tabela : T_VLERAFUSHASHTESE)
    /// </summary>
    public class clsVleraFushaShtese : IDataBaseReader
    {
        #region Atributet

        private int idVleraFushaShtese;
        private int idLidhese;
        private int idFushaShtese;
        private String vleraFushaShtese;
        private int idModeliFushaShtese;
        private string pershkrimiFushaShtese;
        private int tipiFushaShtese;
        private int nrRendor;
        private int idPerdoruesi;
        private DateTime dtKrijimi;
        private DateTime dtAktivizimi;
        private bool vleraFundit;
        private string shenime;
        private IDataRecord rreshti;

        #endregion Atributet

        #region Konstruktoret
        //konstruktoret
        /// <summary>
        /// Konstruktori default i klases
        /// </summary>
        public clsVleraFushaShtese()
        {
        }

        public clsVleraFushaShtese(IDataRecord rreshti)
        {
            Mbush(rreshti);
        }

        #endregion Konstruktoret

        #region Properties

        /// <summary>
        /// Kthen/Vendos ID-ne qe gjenerohet automatikisht per vleren e fushes shtese.
        /// </summary>
        public int IdVleraFushaShtese
        {
            get { return idVleraFushaShtese; }
            set { idVleraFushaShtese = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e lidhezes
        /// </summary>
        public int IdLidhese
        {
            get { return idLidhese; }
            set { idLidhese = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne  fushes shtese koresponduese nga tab T_FUSHASHTESE
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
        /// Kthen/Vendos ID-ne e tipit te fushes shtese koresponduese nga tab T_TIPIFUSHASHTESE
        /// </summary>
        public int TipiFushaShtese
        {
            get { return tipiFushaShtese; }
            set { tipiFushaShtese = value; }
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
        /// Kthen/Vendos vleren
        /// </summary>
        public string VleraFushaShtese
        {
            get { return vleraFushaShtese; }
            set { vleraFushaShtese = value; }
        }

        /// <summary>
        /// Kthen/Vendos pershkrimin
        /// </summary>
        public String PershkrimiFushaShtese
        {
            get { return pershkrimiFushaShtese; }
            set { pershkrimiFushaShtese = value; }
        }

        /// <summary>
        /// Kthen/Vendos id-ne e modelit koresponduese nga T_MODELIFUSHASHTESE
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
        /// numer rendor i fushes
        /// </summary>
        public int Nr_Rendor
        {
            get
            {
                return nrRendor;
            }

            set
            {
                nrRendor = value;
            }
        }

        public DateTime DtKrijimi
        {
            get
            {
                return dtKrijimi;
            }

            set
            {
                dtKrijimi = value;
            }
        }

        public DateTime DtAktivizimi
        {
            get
            {
                return dtAktivizimi;
            }

            set
            {
                dtAktivizimi = value;
            }
        }

        public bool VleraFundit
        {
            get
            {
                return vleraFundit;
            }

            set
            {
                vleraFundit = value;
            }
        }

        public int IdPerdoruesi
        {
            get
            {
                return idPerdoruesi;
            }

            set
            {
                idPerdoruesi = value;
            }
        }

        #endregion Properties

        #region Metoda Publike

        /// <summary>
        /// Modifikon objektin e veprimit ne tabelen T_VLERAFUSHASHTESE ne databaze.Therret funksionin
        /// :  <see cref="DbCore.DbAdmin.clsDatabaseAdmin.modifikoVlera"/>
        /// </summary>
        public clsMesazh Modifiko()
        {
            using (var data = new clsDatabaseAdmin())
            {
                return data.modifikoVlera(IdVleraFushaShtese, IdLidhese, IdFushaShtese, VleraFushaShtese, IdModeliFushaShtese);
            }

        }
        public clsVleraFushaShtese Clone()
        {
            return (clsVleraFushaShtese)MemberwiseClone();
        }


        public string MerrVlereDefaultOseEkzistuesen(string vleraEkzistuese, string vleraDefault)
        {
            return VleraFushaShtese == null ? vleraDefault : vleraEkzistuese;
        }
        #endregion Metoda Publike

        #region Metoda Internal

        public void Mbush(IDataRecord dbDataRowVlerFushShtese)
        {
            int.TryParse(dbDataRowVlerFushShtese["IDVLERAFUSHASHTESE"].ToString(), out idVleraFushaShtese);
            int.TryParse(dbDataRowVlerFushShtese["IDLIDHESE"].ToString(), out idLidhese);
            int.TryParse(dbDataRowVlerFushShtese["IDFUSHASHTESE"].ToString(), out idFushaShtese);
            vleraFushaShtese = dbDataRowVlerFushShtese["VLERAFUSHASHTESE"].ToString();
            int.TryParse(dbDataRowVlerFushShtese["IDMODELIFUSHASHTESE"].ToString(), out idModeliFushaShtese);
            pershkrimiFushaShtese = dbDataRowVlerFushShtese["Pershkrimifushashtese"].ToString();
            int.TryParse(dbDataRowVlerFushShtese["TIPIFUSHASHTESE"].ToString(), out tipiFushaShtese);
            int.TryParse(dbDataRowVlerFushShtese["NR_RENDOR"].ToString(), out nrRendor);

            DateTime.TryParse(dbDataRowVlerFushShtese["DT_AKTIVIZIMI"].ToString(), out dtAktivizimi);
            bool.TryParse(dbDataRowVlerFushShtese["VLERA_FUNDIT"].ToString(), out vleraFundit);
            DateTime.TryParse(dbDataRowVlerFushShtese["DT_KRIJIMI"].ToString(), out dtKrijimi);
            int.TryParse(dbDataRowVlerFushShtese["IDPERDORUESI"].ToString(), out idPerdoruesi);
        }

        #endregion Metoda Internal
    }
}