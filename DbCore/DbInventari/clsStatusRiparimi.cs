using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DbCore.IMBUtils.Messages;

namespace DbCore.DbInventari
{
    /// <summary>
    ///  Kjo eshte klasa qe sherben per objektet qe perfaqsojne statuset e riparimit
    ///  (Te dhenat  merren nga tabela : T_STATUSRIPARIMI)
    /// </summary>
  public  class clsStatusRiparimi
    {
         #region Atribute

        private int id;
        private string kodi;
        private string  pershkrimi;
        private int idPerdoruesi;
        private int idKrijuesi;
        private int idNdermarje;
        private int idStatusDok;
        private DateTime dtKrijimi;
        private DateTime dtModifikimi;
        private string autorizimi;
        private DataRow rreshti;
        #endregion 

        #region Konstruktoret

        /// <summary>
        /// konstruktori me parametra
        /// </summary>
        /// <param name="id"></param>
        /// <param name="kod"></param>
        /// <param name="pershkrim"></param>
        /// <param name="idPerdoruesi"></param>
        /// <param name="idkrijuesi"></param>
        /// <param name="idnderm"></param>
        /// <param name="idstatusdok"></param>
        public clsStatusRiparimi(int id, string kod, String pershkrim, int idPerdoruesi,int idkrijuesi, int idnderm, int idstatusdok)
        {
            this.id = id;
            this.kodi = kod;
            this.pershkrimi = pershkrim;
            this.idPerdoruesi = idPerdoruesi;
            this.idKrijuesi = idkrijuesi;
            this.idNdermarje = idnderm;
            this.idStatusDok = idstatusdok;
        }
       /// <summary>
       /// konstruktori pa id
       /// </summary>
       /// <param name="kod"></param>
       /// <param name="pershkrim"></param>
       /// <param name="idPerdoruesi"></param>
       /// <param name="idkrijuesi"></param>
       /// <param name="idnderm"></param>
       /// <param name="idstatusdok"></param>
        public clsStatusRiparimi( string kod, String pershkrim, int idPerdoruesi,int idkrijuesi, int idnderm, int idstatusdok)
        {
            this.kodi = kod;
            this.pershkrimi = pershkrim;
            this.idPerdoruesi = idPerdoruesi;
            this.idKrijuesi = idkrijuesi;
            this.idNdermarje = idnderm;
            this.idStatusDok = idstatusdok;
        }
        /// <summary>
        /// konstruktor me 1 parameter
        /// </summary>
        /// <param name="id">id </param>
        public clsStatusRiparimi(int id, int idperdoruesi)
        {
            clsDatabaseInventari db = new clsDatabaseInventari();
            mbushStatusRiparime(db.merrStatusRiparime(id, idperdoruesi));
            db.Dispose();
        } 
      public clsStatusRiparimi(int id)
        {
            clsDatabaseInventari db = new clsDatabaseInventari();
            mbushStatusRiparime(db.merrStatusRiparimePaAutorizime(id));
            db.Dispose();
        }  
        public clsStatusRiparimi(int id,int idperdoruesi,clsDatabaseInventari db )
        {

            mbushStatusRiparime(db.merrStatusRiparime(id, idperdoruesi));
           
        }        
        /// <summary>
        /// konstruktori pa parametra
        /// </summary>
        public clsStatusRiparimi()
        { 
        }

        public clsStatusRiparimi(DataRow rreshti)
        {
            
            mbushStatusRiparime(rreshti);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Kthen/Vendos ID-ne qe gjenerohet automatikisht.
        /// </summary>
        public int Id
        {
            get { return id  ; }
            set { id  = value; }
        }
        /// <summary>
        /// Kthen/Vendos kod .
        /// </summary>
        public String Kodi
        {
            get { return kodi ; }
            set { kodi  = value; }
        }
        /// <summary>
        /// Kthen/Vendos ID-ne e perdoruesit.
        /// </summary>
        public int IdPerdoruesi
        {
            get { return idPerdoruesi ; }
            set { idPerdoruesi  = value; }
        }
        /// <summary>
        /// Kthen/Vendos pershkrimin .
        /// </summary>
        public String Pershkrimi
        {
            get { return pershkrimi ; }
            set { pershkrimi  = value; }
        }
        /// <summary>
        /// Kthen/Vendos ID-ne  e krijuesit.
        /// </summary>
        public int IdKrijuesi
        {
            get { return idKrijuesi; }
            set { idKrijuesi = value; }
        }
        /// <summary>
        /// Kthen/Vendos ID-ne  e ndermarjes.
        /// </summary>
        public int IdNdermarje
        {
            get { return idNdermarje; }
            set { idNdermarje = value; }
        }
        public int IdStatusDok
            {
            get { return idStatusDok; }
            set { idStatusDok = value; }
            }
        public DateTime DtKrijimi
            {
            get { return dtKrijimi; }

            }
        public DateTime DtModifikimi
            {
            get { return dtModifikimi; }

            }
      /// <summary>
      /// autorizimet e perdoruesve
      /// </summary>
        public string Autorizimi
        {
            get
            {
                return autorizimi;
            }
            set
            {
                autorizimi = value;
            }
        }
        #endregion

        #region Metoda Publike
        /// <summary>
        /// konstruktor me 2 parametra
        /// </summary>
        /// <param name="pershkrimi">pershkrim </param>
        /// <param name="idnderm">id e ndermarrjes</param>
        public bool mbushStatusRiparimiMePershk(string pershkrimi, int idnderm, int idperdoruesi)
        {
            clsDatabaseInventari db = new clsDatabaseInventari();
            bool sukses = mbushStatusRiparime(db.merrStatusRiparimiMePershk(pershkrimi, idnderm, idperdoruesi));
            db.Dispose();
            return sukses;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="kod"></param>
        /// <param name="idnderm"></param>
        /// <returns></returns>
        public bool mbushStatusRiparmiMeKod(string kod, int idnderm, int idperdoruesi)
        {
            clsDatabaseInventari db = new clsDatabaseInventari();
            bool sukses = mbushStatusRiparime(db.merrStatusRiparimiMeKod(kod, idnderm, idperdoruesi));
            db.Dispose();
            return sukses;
        }
        public bool mbushStatusRiparimiMeKod(string kod, int idnderm,int idperdoruesi, clsDatabaseInventari db)
        {

            bool sukses = mbushStatusRiparime(db.merrStatusRiparimiMeKod(kod, idnderm, idperdoruesi));
          
            return sukses;
        }
        public static bool ekzistonStatusRiparimi(string kod, int idnderm) 
        {
            clsDatabaseInventari db = new clsDatabaseInventari();
            bool sukses = db.ekzistonStatusRiparimi(kod, idnderm);
            db.Dispose();
            return sukses;
        }
        public clsMesazh transfero(object[] idkodifikimi, List<object> idndermarje, int idperdoruesi)
        {
            clsMesazh mesazh = new clsMesazh();
            bool sukses = true;
            for (int i = 0; i < idkodifikimi.Length; i++)
            {
                clsStatusRiparimi kod = new clsStatusRiparimi(int.Parse(idkodifikimi[i].ToString()), idperdoruesi);
                foreach (object id in idndermarje)
                {
                    clsDatabaseInventari db = new clsDatabaseInventari();
                    try
                    {
                        db.beginTransaksion();
                        mesazh = kontrollotransferim(kod, int.Parse(id.ToString()), db, idperdoruesi);
                        if (!mesazh.Status)
                        {
                            db.rollbackTransaksion();
                            sukses = false;
                        }
                        db.commitTransaksion();
                    }
                    catch
                    {
                        db.rollbackTransaksion();
                        sukses = false;
                    }
                }
            }
            if (sukses) { mesazh.PershkrimMesazhi = "Transferimi mbaroi me sukses!"; mesazh.Status = true; }
            else { mesazh.PershkrimMesazhi = "Disa nga llojet e difekteve nuk u transferuan!"; mesazh.Status = false; }
            return mesazh;
        }

        public clsMesazh kontrollotransferim(clsStatusRiparimi kod, int idndermarje, clsDatabaseInventari db, int idperdoruesi)
        {
            clsMesazh mesazh = new clsMesazh(true, "Transferimi mbaroi me sukses!");
            if (!db.ekzistonStatusRiparimi(kod.Kodi, idndermarje))
            {
                kod.idPerdoruesi = idperdoruesi;
                kod.idNdermarje = idndermarje;
                mesazh = kod.ruaj(db);
                if (!mesazh.Status)
                    return mesazh;
            }
            else
            {
                clsStatusRiparimi kodnderm = new  clsStatusRiparimi ();
                kodnderm.mbushStatusRiparimiMeKod(kod.kodi, idndermarje,idPerdoruesi, db);
                if (kodnderm.dtModifikimi < kod.dtModifikimi)
                {
                    kod.idPerdoruesi = idperdoruesi;
                    kod.idNdermarje = idndermarje;
                    kod.id = kodnderm.id;
                    mesazh = kod.modifiko(db);
                    if (!mesazh.Status)
                        return mesazh;
                }

            }
            return mesazh;
        }
       /// <summary>
       /// ruan statusin e riparimit
       /// </summary>
       /// <returns></returns>
        public clsMesazh ruaj()
        {
            clsDatabaseInventari data = new clsDatabaseInventari();
            data.beginTransaksion();
            clsMesazh u_ruajt = ruaj(data);

            if (!u_ruajt.Status)
                data.rollbackTransaksion();
            else data.commitTransaksion();
            return u_ruajt;
        } 
        public clsMesazh ruaj( clsDatabaseInventari data )
        {
          
            int id;
            clsMesazh u_ruajt = data.ruajStatusRiparimi(out id, this.Kodi, this.Pershkrimi, this.IdPerdoruesi,this.IdKrijuesi, this.IdNdermarje, this.idStatusDok);
            if (!u_ruajt.Status)
                return u_ruajt;
            DbAdmin.clsDatabaseAdmin dbAdmin = new DbAdmin.clsDatabaseAdmin(data );
            if (!string.IsNullOrWhiteSpace(autorizimi))
            {
                DbAdmin.colLidhjetAutorizim colLidhjet = new DbAdmin.colLidhjetAutorizim();
                DbKontabiliteti.clsDatabaseKontabilitet dbKont = new DbKontabiliteti.clsDatabaseKontabilitet(data );
                string[] pars1 = autorizimi.Split(',');
                for (int i = 0; i < pars1.Length; i++)
                {
                    DbAdmin.clsLidhjeAutorizim lidhje = new DbAdmin.clsLidhjeAutorizim();
                    lidhje.IdAutorizimeKoka = DbAdmin.clsAutorizimKoka.ktheIDAutorizim(pars1[i]);
                    colLidhjet.Add(lidhje);
                }
                foreach (DbAdmin.clsLidhjeAutorizim o in colLidhjet)
                {
                    o.IdLloji = DbCore.DbKontabiliteti.clsLlojBuxheti.mbushIDLlojBuxheti("StatusRiparimi", dbKont);
                    o.IdLidhese = id;
                    u_ruajt = dbAdmin.ruajLidhjeAutorizim(o.IdLidhjeAutorizim, o.IdLidhese, o.IdLloji, o.IdAutorizimeKoka, 1);
                    if (!u_ruajt.Status)
                    {
                        //dbInv.rollbackTransaksion();
                        return u_ruajt;
                    }
                }
            }
            this.id = id;
            return u_ruajt;
        }
        public static bool eshteTransferuarTekBij(string kodi, int idndermarje)
        {
            clsDatabaseInventari db = new clsDatabaseInventari();
            bool ekziston = db.eshteTransferuarTekBijLlojDifekti(kodi, idndermarje);
            db.Dispose();
            return ekziston;
        }
       /// <summary>
       /// modifikon status riparimi
       /// </summary>
       /// <returns></returns>
       public clsMesazh modifiko()
        {
            using (var scope = new MyTransactionScope())
            using (var data = new clsDatabaseInventari())
            {
                clsMesazh u_modifikua = modifiko(data);
                if (u_modifikua)
                    scope.Complete();

                return u_modifikua;
            }
        }
        public clsMesazh modifiko(clsDatabaseInventari data)
       {
           DbAdmin.clsDatabaseAdmin dbAdmin = new DbAdmin.clsDatabaseAdmin(data );
           DbAdmin.colLidhjetAutorizim colLidhjetAutorizim = new DbAdmin.colLidhjetAutorizim(id, "StatusRiparimi", dbAdmin);
            clsMesazh u_modifikua = data.modifikoStatusRiparimi(this.Id, this.Kodi, this.Pershkrimi, this.IdPerdoruesi, this.IdNdermarje, this.idStatusDok);
            if (!u_modifikua.Status)
                return u_modifikua;
            DbAdmin.colLidhjetAutorizim colLidhjet = new DbAdmin.colLidhjetAutorizim();
            if (autorizimi != "")
            {
                string[] pars1 = autorizimi.Split(',');
                for (int i = 0; i < pars1.Length; i++)
                {
                    DbAdmin.clsLidhjeAutorizim lidhje = new DbAdmin.clsLidhjeAutorizim();
                    lidhje.IdAutorizimeKoka = DbAdmin.clsAutorizimKoka.ktheIDAutorizim(pars1[i].Trim());
                    colLidhjet.Add(lidhje);
                }
            }
            u_modifikua = clsFunksione.modifikoLidhjeAutorizimSipasLlojitTeBuxhetit(colLidhjet, "StatusRiparimi", id, colLidhjetAutorizim, new DbKontabiliteti.clsDatabaseKontabilitet(data), dbAdmin, false,idPerdoruesi);
            
            if (u_modifikua.Status)
                u_modifikua.PershkrimMesazhi = MessagesResource.Messages["msgModifikimiMeSukses"];
            return u_modifikua;
        }
       /// <summary>
       /// fshin statusin e riparimit duke i ndryshuar statusin
       /// </summary>
       /// <returns></returns>
        public clsMesazh fshi()
        {
            clsDatabaseInventari data = new clsDatabaseInventari();
            clsMesazh u_fshi = data.fshiStatusRiparimiStatus(this.Id, this.idPerdoruesi);
            data.Dispose();
           
            return u_fshi;
        }
     

        #endregion

        #region Metoda Internal

       /// <summary>
       /// mbush status riparimi nga databaza
       /// </summary>
       /// <param name="db"></param>
       /// <returns></returns>
        internal bool mbushStatusRiparime(DataRow db)
        {

            if (db != null)
            {

                try
                {

                    int.TryParse(db["ID"].ToString(), out id);
                    kodi = db["KODI"].ToString();
                    pershkrimi = db["PERSHKRIMI"].ToString();
                    int.TryParse(db["IDPERDORUESI"].ToString(), out idPerdoruesi);
                    int.TryParse(db["IDKRIJUESI"].ToString(), out idKrijuesi);
                    int.TryParse(db["IDNDERMARJE"].ToString(), out idNdermarje);
                    int.TryParse(db["IDSTATUSDOK"].ToString(), out idStatusDok);
                    DateTime.TryParse(db["DTKRIJIMI"].ToString(), out dtKrijimi);
                    DateTime.TryParse(db["DTMODIFIKIMI"].ToString(), out dtModifikimi);
                    if (db.Table.Columns.Contains("Autorizimi") && db["Autorizimi"] != null)
                        this.Autorizimi = db["Autorizimi"].ToString();
                    //DbAdmin.colLidhjetAutorizim lidhje = new DbAdmin.colLidhjetAutorizim(id, "StatusRiparimi" );
                   
                    //if (lidhje.Count != 0)
                    //{
                    //    autorizimi = DbAdmin.clsAutorizimKoka.ktheKodAutorizim(lidhje[0].IdAutorizimeKoka);
                    //     for (int i = 1; i < lidhje.Count; i++)
                    //        autorizimi += "," + DbAdmin.clsAutorizimKoka.ktheKodAutorizim(lidhje[i].IdAutorizimeKoka);
                        
                    //}
                    //else
                    //    autorizimi = "";
                    return true;
                }
                catch (InvalidCastException)
                {
                    throw new Exception("ERROR: Gabim gjate marrjes se statusit te riparimit nga db-ja");
                }
            }
            else
                return false;

        }

        #endregion
    }
}
