using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DbCore.DbCRM
{
    public class clsKokaAnketa
    {
        #region Atribute

        private int idKokaAnketa;
        private string kodi;
        private string pershkrimi;
        private DateTime dtFillimi;
        private DateTime dtMbarimi;
        private int idStatusDok;
        private DateTime dtKrijimi;
        private int idKrijues;
        private DateTime dtModifikimi;
        private int idModifikues;
        private int idNdermarje;
        private colTrupiAnketa colTrupi;

        #endregion

        #region Properties
        /// <summary>
        /// 
        /// </summary>
        public int IdKokaAnketa
        {
            get { return idKokaAnketa; }
            set { idKokaAnketa = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Kodi
        {
            get { return kodi; }
            set { kodi = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Pershkrimi
        {
            get { return pershkrimi; }
            set { pershkrimi = value; }
        }

        public DateTime DtFillimi
        {
            get { return dtFillimi; }
            set { dtFillimi = value; }
        }

        public DateTime DtMbarimi
        {
            get { return dtMbarimi; }
            set { dtMbarimi = value; }
        }

        public int IdStatusDok
        {
            get { return idStatusDok; }
            set { idStatusDok = value; }
        }

        public DateTime DtKrijimi
        {
            get { return dtKrijimi; }
            set { dtKrijimi = value; }
        }

        public int IdKrijues
        {
            get { return idKrijues; }
            set { idKrijues = value; }
        }

        public DateTime DtModifikimi
        {
            get { return dtModifikimi; }
            set { dtModifikimi = value; }
        }

        public int IdModifikues
        {
            get { return idModifikues; }
            set { idModifikues = value; }
        }

        public int IdNdermarje
        {
            get { return idNdermarje; }
            set { idNdermarje = value; }
        }

        public colTrupiAnketa ColTrupi
        {
            get
            {
                return colTrupi;
            }
            set
            {
                colTrupi = value;
            }
        }
        #endregion

        #region Konstruktoret

        public clsKokaAnketa()
        {

        }
        public clsKokaAnketa(int id)
        {
            using (clsDatabaseCRM data = new clsDatabaseCRM())
            {
                mbushKokaAnketa(data.merrKokaAnketeSipasId(id));
            }

        }
        public clsKokaAnketa(int idKokaAnketa, string kodi, string pershkrimi, DateTime dtFillimi, DateTime dtMbarimi, int idStatusDok, int idKrijues, int idModifikues, int idNdermarje)
        {
            this.IdKokaAnketa = idKokaAnketa;
            this.kodi = kodi;
            this.pershkrimi = pershkrimi;
            this.dtFillimi = dtFillimi;
            this.dtMbarimi = dtMbarimi;
            this.idStatusDok = idStatusDok;
            this.idKrijues = idKrijues;
            this.idModifikues = idModifikues;
            this.idNdermarje = idNdermarje;
            this.colTrupi = new colTrupiAnketa();
            if (dtMbarimi < dtFillimi)
                throw new Exception("Data e mbarimit duhet te jete me e madhe se data e fillimit!");

        }
        #endregion

        #region Metoda Publike

        public clsMesazh ruaj()
        {
            clsDatabaseCRM data = new clsDatabaseCRM();
            data.beginTransaksion();
            try
            {
                clsMesazh u_ruajt = ruaj(data);
                if (u_ruajt.Status)
                    data.commitTransaksion();
                else data.rollbackTransaksion();
                return u_ruajt;
            }
            catch (Exception ex)
            {
                data.rollbackTransaksion();
                return new clsMesazh(false, ex.Message);
            }
        }

        public clsMesazh ruaj(clsDatabaseCRM data)
        {
            int id;
            if (data.ekzistonAnketa(this.kodi, this.idNdermarje))
            {
                return new clsMesazh(false, "Ekziston nje ankete  me kete kod!");
            }
            clsMesazh u_ruajt = data.ruajKokaAnketa(out id, this.kodi, this.pershkrimi, this.dtFillimi, this.dtMbarimi, this.idStatusDok, this.idKrijues, this.idNdermarje);
            this.idKokaAnketa = id;
            if (!u_ruajt.Status)
                return u_ruajt;
            foreach (clsTrupiAnketa tr in this.colTrupi)
            {
                int idtr = 0;
                u_ruajt = data.ruajTrupiAnketa(out idtr, this.idKokaAnketa, tr.IdOpsionAnketa, tr.Detyrueshme, tr.IdStatusDok);
                if (!u_ruajt.Status)
                    return u_ruajt;
            }
            return u_ruajt;
        }

        public static bool kaPrerjeAnketashKlienti(int idndermarje, int idklienti, DateTime dtfillimi, DateTime dtmbarimi)
        {
            clsDatabaseCRM db = new clsDatabaseCRM();
            bool kaPrerje = kaPrerjeAnketashKlienti(idndermarje, idklienti, dtfillimi, dtmbarimi, db);
            db.Dispose();
            return kaPrerje;
        }

        public static bool kaPrerjeAnketashKlienti(int idndermarje, int idklienti, DateTime dtfillimi, DateTime dtmbarimi, clsDatabaseCRM db)
        {
            return db.kaPrerjeAnketashKlienti(idndermarje, idklienti, dtfillimi, dtmbarimi);
        }

        public clsMesazh modifiko()
        {
            clsDatabaseCRM data = new clsDatabaseCRM();
            data.beginTransaksion();
            try
            {
                clsMesazh u_modifikua = modifiko(data);
                if (u_modifikua.Status)
                    data.commitTransaksion();
                else data.rollbackTransaksion();
                return u_modifikua;
            }
            catch (Exception ex)
            {
                data.rollbackTransaksion();
                return new clsMesazh(false, ex.Message);
            }

        }

        public clsMesazh modifiko(clsDatabaseCRM data)
        {
            clsMesazh u_modifikua = data.modifikoKokaAnkete(this.idKokaAnketa, this.kodi, this.pershkrimi, this.dtFillimi, this.dtMbarimi, this.idStatusDok, this.idModifikues, this.idNdermarje);

            if (!u_modifikua.Status)
                return u_modifikua;

            if (clsKlientAnketa.eshteLidhur(this.IdKokaAnketa) && this.DtFillimi < DateTime.Now)
                return u_modifikua;
                //nese anketa eshte e lidhur dhe nese data e fillimit te anketes eshte me e vogel se data e sotme e anketes modifikohet edhe trupi i saj,
                //ne rast te kundert modifikohen vetem fushat e kokes se anketes
                u_modifikua = data.fshiTrupAnkete(this.idKokaAnketa);
                if (!u_modifikua.Status)
                {
                    return u_modifikua;

                }
                foreach (clsTrupiAnketa tr in this.colTrupi)
                {
                    int idtr = 0;
                    u_modifikua = data.ruajTrupiAnketa(out idtr, this.idKokaAnketa, tr.IdOpsionAnketa, tr.Detyrueshme, tr.IdStatusDok);
                    if (!u_modifikua.Status)
                        return u_modifikua;
                }           
            return u_modifikua;
        }

        public clsMesazh fshi()
        {
            clsDatabaseCRM data = new clsDatabaseCRM();
            clsMesazh u_fshi = data.fshiKokaAnkete(IdKokaAnketa, idModifikues);
            data.Dispose();
            return u_fshi;
        }

        public bool mbushKokeAnkete(int idKokaAnketa)
        {
            clsDatabaseCRM data = new clsDatabaseCRM();
            bool sukses = mbushKokaAnketa(data.merrKokaAnketeSipasId(idKokaAnketa));
            data.Dispose();
            return sukses;
        }

        public DataRow ktheKokeAnketeDt(int idKokaAnketa)
        {
            clsDatabaseCRM data = new clsDatabaseCRM();
            DataRow dr = data.merrKokaAnketeSipasId(idKokaAnketa);
            data.Dispose();
            return dr;
        }

        /// <summary>
        /// kontrollon nese anketa eshte e lidhur
        /// </summary>
        /// <returns></returns>
        public bool kaVeprimeAnketa()
        {
            return new clsDatabaseCRM().kaVeprimeCRMListaAnketa(this.IdKokaAnketa);
        }
        #endregion

        #region Metoda Internal

        internal bool mbushKokaAnketa(DataRow dbDataRowKokaAnketa)
        {
            if (dbDataRowKokaAnketa != null)
            {
                try
                {
                    int.TryParse(dbDataRowKokaAnketa["IDKOKAANKETA"].ToString(), out idKokaAnketa);
                    kodi = dbDataRowKokaAnketa["KODI"].ToString();
                    pershkrimi = dbDataRowKokaAnketa["PERSHKRIMI"].ToString();
                    DateTime.TryParse(dbDataRowKokaAnketa["DTFILLIMI"].ToString(), out dtFillimi);
                    DateTime.TryParse(dbDataRowKokaAnketa["DTMBARIMI"].ToString(), out dtMbarimi);
                    int.TryParse(dbDataRowKokaAnketa["IDSTATUSDOK"].ToString(), out idStatusDok);
                    DateTime.TryParse(dbDataRowKokaAnketa["DTKRIJIMI"].ToString(), out dtKrijimi);
                    DateTime.TryParse(dbDataRowKokaAnketa["DTMODIFIKIMI"].ToString(), out dtModifikimi);
                    int.TryParse(dbDataRowKokaAnketa["IDKRIJUESI"].ToString(), out idKrijues);
                    int.TryParse(dbDataRowKokaAnketa["IDMODIFIKUESI"].ToString(), out idModifikues);
                    int.TryParse(dbDataRowKokaAnketa["IDNDERMARJE"].ToString(), out idNdermarje);
                    return true;
                }
                catch (InvalidCastException)
                {
                    throw new Exception("ERROR: Gabim gjate marrjes se fushave te kokes se anketes nga db-ja");
                }
            }
            else
                return false;
        }

        #endregion


    }
}