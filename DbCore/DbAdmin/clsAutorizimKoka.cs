using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DbCore.IMBUtils.Messages;

namespace DbCore.DbAdmin
{
    /// <summary>
    ///  Kjo eshte klasa qe sherben per objektet qe perfaqsojne koken e nje konfigurmi
    ///  te caktuar per Autorizim(Te dhenat merren nga tabela :T_AUTORIZIMKOKA)
    /// </summary> 
    public class clsAutorizimKoka
    {
        #region Atributet

        private int idAutorizimKoka;
        private string kodiAutorizim;
        private string pershkrimAutorizim;
        private int idPerdoruesi;
        private colAutorizimetTrupi oColTrupi;
        private int idNdermarje;
        private int idStatusDok;
        private DateTime dtKrijimi;
        private DateTime dtModifikimi;
        private DataRow rreshti;


        #endregion

        #region Konstruktoret

        /// <summary>
        /// Konstruktori i klases
        /// </summary>
        public clsAutorizimKoka(int idAutorizimKoka, string kodiAutorizim, string pershkrimAutorizim, int idperdoruesi, int idndermarje, int idstatusdok)
        {
            this.idAutorizimKoka = idAutorizimKoka;
            this.kodiAutorizim = kodiAutorizim;
            this.pershkrimAutorizim = pershkrimAutorizim;
            this.idPerdoruesi = idperdoruesi;
            oColTrupi = new colAutorizimetTrupi();
            this.idNdermarje = idndermarje;
            this.idStatusDok = idstatusdok;

        }

        /// <summary>
        /// Konstruktori default i klases
        /// </summary>
        public clsAutorizimKoka()
        {
        }

        public clsAutorizimKoka(int id)
        {
            clsDatabaseAdmin data = new clsDatabaseAdmin();
            mbushAutorizimKoka(data.merrAutorizim(id));
            data.Dispose();
        }

        public clsAutorizimKoka(string kod)
        {
            clsDatabaseAdmin data = new clsDatabaseAdmin();
            mbushAutorizimKoka(data.merrAutorizim(kod));
            data.Dispose();
        }

        public clsAutorizimKoka(DataRow rreshti)
        {
           
            mbushAutorizimKoka(rreshti);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Kthen ID-ne qe gjenerohet automatikisht.
        /// </summary>
        public int IdAutorizimKoka
        {
            get
            {
                return idAutorizimKoka;
            }
            set
            {
                idAutorizimKoka = value;
            }
        }

        /// <summary>
        /// Kthen kodin e konfigurimit te Autorizimit.
        /// </summary>
        public string KodiAutorizim
        {
            get
            {
                return kodiAutorizim;
            }
            set
            {
                kodiAutorizim = value;
            }
        }

        /// <summary>
        /// Kthen pershkrimin e konfigurimit te Autorizimit.
        /// </summary>
        public string PershkrimAutorizim
        {
            get
            {
                return pershkrimAutorizim;
            }
            set
            {
                pershkrimAutorizim = value;
            }
        }

        /// <summary>
        /// Kthen ID-ne e perdoruesit qe krijovi kete Konfigurim.
        /// </summary>
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
        public int IdNdermarje
        {
            get
            {
                return idNdermarje;
            }
            set
            {
                idNdermarje = value;
            }
        }
        public int IdStatusDok
        {
            get
            {
                return idStatusDok;
            }
            set
            {
                idStatusDok = value;
            }
        }
        public DateTime DtKrijimi
        {
            get
            {
                return dtKrijimi;
            }

        }
        public DateTime DtModifikimi
        {
            get
            {
                return dtModifikimi;
            }

        }
        /// <summary>
        /// Kthen nje collecton me objekte te tipit <code>clsAutorizimTrupi</code>
        /// </summary>
        public colAutorizimetTrupi OColTrupi
        {
            get { return oColTrupi; }
            set { oColTrupi = value; }
        }

        #endregion

        #region Metoda Publike

        /// <summary>
        /// Ruan objektin e Kokes se autorizimit ne databaze.
        /// </summary>
        public clsMesazh ruaj()
        {//metoda qe therret klasen clsDatabaseAdmin per ruajtjen e nje autorizim 
            //clsDatabaseAdmin  data = new clsDatabaseAdmin();
            clsMesazh u_ruajt = ruajAutorizimKokaAndTrupi(this);
            return u_ruajt;
        }

        /// <summary>
        /// Modifikon objektin e Kokes se autorizimit.
        /// </summary>
        public clsMesazh modifiko()
        {//metoda qe therret klasen clsDatabaseAdmin per modifikimin e nje autorizim
            //clsDatabaseAdmin data = new clsDatabaseAdmin();
            clsMesazh u_modifikua = modifikoAutorizimKokaAndTrupi(this);
            return u_modifikua;
        }

        /// <summary>
        /// Fshin objektin e Kokes se autorizimit ne databaze.
        /// </summary>
        //public clsMesazh fshi()
        //{//metoda qe therret klasen clsDatabaseAdmin per fshirjen e nje autorizim
        //    //clsDatabaseAdmin data = new clsDatabaseAdmin();
        //    clsMesazh u_fshi = fshiAutorizimKokaAndTrupi(this);
        //    return u_fshi;
        //}

        /// <summary>
        /// Merr objektin e Kokes se autorizimit nga  databaza. Nuk perdoret.
        /// </summary>
        //public void merr()
        //{//metoda qe therret klasen clsDatabaseAdmin per marrjen e nje autorizim
        //    clsDatabaseAdmin data = new clsDatabaseAdmin();
        //    data.merrAutorizimKoka(this.IdAutorizimKoka);
        //    data.Dispose();
        //}

        /// <summary>
        /// Kthen nje objekt te tiptit <code>clsAutorizimKoka</code>, te cilin e merr nga databaza sipas kodit
        /// </summary>
        public clsAutorizimKoka merrAutorizimSipasKodit()
        {//metoda qe therret klasen clsDatabaseAdmin per marrjen e nje autorizim
            clsAutorizimKoka data = new clsAutorizimKoka(this.IdAutorizimKoka);
            return data;
        }

        public clsAutorizimKoka merrAutorizimNgaKodi() //TODO Kevi
        {//metoda qe therret klasen clsDatabaseAdmin per marrjen e nje autorizim
            clsAutorizimKoka data = new clsAutorizimKoka(this.KodiAutorizim);
            return data;
        }
   
        public static string ktheKodAutorizim(int id)
        {
            clsDatabaseAdmin data = new clsDatabaseAdmin();
            string kodi = data.merrKodAutorizim(id);
            data.Dispose();
            return kodi;
        }
        public static string ktheKodAutorizim(int id, clsDatabaseAdmin data)
        {

            string kodi = data.merrKodAutorizim(id);

            return kodi;
        }

        public static int ktheIDAutorizim(string kod)
        {
            using (clsDatabaseAdmin data = new clsDatabaseAdmin())
            {
                return data.merrIDAutorizim(kod);
            }
        }
        public static int ktheIDAutorizim(string kod, clsDatabaseAdmin data)
        {

            return data.merrIDAutorizim(kod);
        }

        public static bool kaAutorizimSipasPerdoruesit(string kod, int idPerdorues)
        {
            using (clsDatabaseAdmin data = new clsDatabaseAdmin())
            {
                return data.kaAutorizimSipasKodDhePerdoruesi(kod, idPerdorues);
            }
        }

        public clsMesazh ruajAutorizimKokaAndTrupi(clsAutorizimKoka autorizimKoka)
        {//transaksioni per te ruajtur edhe koken edhe trupin njekohesisht.
            clsDatabaseAdmin dbAdmin = new clsDatabaseAdmin();
            dbAdmin.beginTransaksion();
            clsMesazh mesazh;
            try
            {
                if (dbAdmin.ekzistonAutorizim(autorizimKoka.KodiAutorizim))
                {
                    dbAdmin.rollbackTransaksion();
                    return new clsMesazh(false, "Ekziston nje autorizim me kete kod!");
                }
                int idA;
                mesazh = dbAdmin.ruajAutorizimKoka(out idA, autorizimKoka.KodiAutorizim, autorizimKoka.PershkrimAutorizim, autorizimKoka.IdPerdoruesi, autorizimKoka.idNdermarje, autorizimKoka.idStatusDok);
                if (!mesazh.Status)
                {
                    dbAdmin.rollbackTransaksion();
                    return mesazh;
                }
                autorizimKoka.IdAutorizimKoka = idA;
                foreach (clsAutorizimTrupi o in autorizimKoka.OColTrupi)
                {//behet ruajtja e autorizim trupi per cdo perdorues qe eshte selektuar

                    o.IdAutorizimKoka = autorizimKoka.IdAutorizimKoka;
                    mesazh = dbAdmin.ruajAutorizimTrupi(o.IdAutorizimTrupi, o.IdAutorizimKoka, o.IdPerdorues);
                    if (!mesazh.Status)
                    {
                        dbAdmin.rollbackTransaksion();
                        return mesazh;
                    }
                }
                dbAdmin.commitTransaksion();
                mesazh = new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
                return mesazh;
            }
            catch (Exception)
            {
                dbAdmin.rollbackTransaksion();
                return new clsMesazh(false, "Ndodhi nje gabim gjate: ruajtjes se autorizimit");
            }
        }

        public clsMesazh modifikoAutorizimKokaAndTrupi(clsAutorizimKoka autorizimKoka)
        {//transaksioni per te modifikuar edhe koken edhe trupin e nje autorizimi
            //marrim gjithe trupat eksistues
            colAutorizimetTrupi trupi = new colAutorizimetTrupi(autorizimKoka.IdAutorizimKoka);
            //colAutorizimetTrupi trupi = merrAutorizimTrupiNgaIdAutorizimKoka(autorizimKoka.IdAutorizimKoka);
            clsMesazh mesazh;
            clsDatabaseAdmin dbAdmin = new clsDatabaseAdmin();

            dbAdmin.beginTransaksion();

            try
            {
                mesazh = dbAdmin.modifikoAutorizimKoka(autorizimKoka.IdAutorizimKoka, autorizimKoka.KodiAutorizim, autorizimKoka.PershkrimAutorizim, autorizimKoka.IdPerdoruesi, autorizimKoka.idNdermarje, autorizimKoka.idStatusDok);
                if (!mesazh.Status)
                {
                    dbAdmin.rollbackTransaksion();
                    return mesazh;
                }
                for (int j = 0; j < autorizimKoka.OColTrupi.Count; j++)
                    for (int i = 0; i < trupi.Count; i++)
                        if (autorizimKoka.OColTrupi[j].IdPerdorues == trupi[i].IdPerdorues)
                        {
                            autorizimKoka.OColTrupi.Remove(autorizimKoka.OColTrupi[j]);
                            autorizimKoka.OColTrupi.Insert(j, (clsAutorizimTrupi)trupi[i]);
                        }
                foreach (clsAutorizimTrupi o in autorizimKoka.OColTrupi)
                {
                    o.IdAutorizimKoka = autorizimKoka.IdAutorizimKoka;
                    //nqs ky perdorues ekziston atehere e heqim nga kolectioni trupi perndryshe e shtojme ate ne database
                    if (trupi.Contains(o))
                        trupi.Remove(o);
                    else
                    {
                        mesazh = dbAdmin.ruajAutorizimTrupi(o.IdAutorizimTrupi, o.IdAutorizimKoka, o.IdPerdorues);
                    }
                    if (!mesazh.Status)
                    {
                        dbAdmin.rollbackTransaksion();
                        return mesazh;
                    }
                }
                foreach (clsAutorizimTrupi auto in trupi)
                { //fshihen te gjithe trupat per te cilet perdoruesit nuk jane me te selektuar.
                    mesazh = dbAdmin.fshiAutorizimTrupi(auto.IdAutorizimTrupi);
                    if (!mesazh.Status)
                    {
                        dbAdmin.rollbackTransaksion();
                        return mesazh;
                    }
                }
                dbAdmin.commitTransaksion();
                mesazh = new clsMesazh(true, MessagesResource.Messages["msgModifikimiMeSukses"]);
                return mesazh;
            }
            catch (Exception ce)
            {
                dbAdmin.rollbackTransaksion();
                return new clsMesazh(false, ce.Message);
            }
        }

        public clsMesazh fshi()
        {
            clsDatabaseAdmin db = new clsDatabaseAdmin();
            clsMesazh mesazh = db.fshiAutorizimKokaStatus(this.idAutorizimKoka, this.idPerdoruesi);
            db.Dispose();
            return mesazh;
        }

        public clsMesazh fshiAutorizimKokaAndTrupi(clsAutorizimKoka autorizimKoka)
        {//transaksioni per te fshire nje autorizim koke dhe trupat
            colAutorizimetTrupi trupi = new colAutorizimetTrupi(autorizimKoka.IdAutorizimKoka);
            //colAutorizimetTrupi trupi = merrAutorizimTrupiNgaIdAutorizimKoka(autorizimKoka.IdAutorizimKoka);
            clsMesazh mesazh;
            clsDatabaseAdmin dbAdmin = new clsDatabaseAdmin();
            dbAdmin.beginTransaksion();
            try
            {
                foreach (clsAutorizimTrupi o in trupi)
                {
                    mesazh = dbAdmin.fshiAutorizimTrupi(o.IdAutorizimTrupi);
                    if (!mesazh.Status)
                    {
                        dbAdmin.rollbackTransaksion();
                        return mesazh;
                    }
                }
                mesazh = dbAdmin.fshiAutorizimKoka(autorizimKoka.IdAutorizimKoka);
                if (!mesazh.Status)
                {
                    dbAdmin.rollbackTransaksion();
                    return mesazh;
                }
                dbAdmin.commitTransaksion();
                mesazh = new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
                return mesazh;
            }
            catch (Exception ce)
            {
                dbAdmin.rollbackTransaksion();
                return new clsMesazh(false, ce.Message);
            }
        }

        #endregion

        #region Metoda Internal

        internal bool mbushAutorizimKoka(DataRow dbDataRowAutorizimKoka)
        {
            if (dbDataRowAutorizimKoka != null)
            {
                try
                {
                    int.TryParse(dbDataRowAutorizimKoka["IDAUTORIZIMEKOKA"].ToString(), out idAutorizimKoka);
                    kodiAutorizim = dbDataRowAutorizimKoka["KODAUTORIZIME"].ToString();
                    pershkrimAutorizim = dbDataRowAutorizimKoka["PERSHKRIMAUTORIZIME"].ToString();
                    int.TryParse(dbDataRowAutorizimKoka["IDPERDORUESI"].ToString(), out idPerdoruesi);
                    int.TryParse(dbDataRowAutorizimKoka["IDNDERMARJE"].ToString(), out idNdermarje);
                    int.TryParse(dbDataRowAutorizimKoka["IDSTATUSDOK"].ToString(), out idStatusDok);
                    DateTime.TryParse(dbDataRowAutorizimKoka["DTKRIJIMI"].ToString(), out dtKrijimi);
                    DateTime.TryParse(dbDataRowAutorizimKoka["DTMODIFIKIMI"].ToString(), out dtModifikimi);
                    return true;
                }
                catch (InvalidCastException)
                {
                    throw new Exception("ERROR: Gabim gjate marrjes se kokes se autorizimit nga db-ja");
                }
            }
            else
                return false;
        }

        #endregion
    }
}