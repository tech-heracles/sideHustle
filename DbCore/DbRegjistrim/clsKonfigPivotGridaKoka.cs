using DbCore.DbAdmin;
using DbCore.IMBUtils.Messages;
using System;
using System.Data;

namespace DbCore.DbRegjistrim
{
    /// <summary>
    /// Kjo klase permban metodat e nevojshme per te punuar me te dhenat e tabeles : T_KONFIG_PIVOTGRIDA_KOKA 
    /// </summary>

    public class clsKonfigPivotGridaKoka
    {
        #region Atributet

        private int idKonfPivotGridaKoka;
        private string emerPivotGridaKoka;
        private string pershkrimiPivotGridaKoka;
        private int idModuli;
        private int idNdermarje;
        private int idPerdoruesi;
        private int idStatusDok;
        private DateTime dtKrijimi;
        private DateTime dtModifikimi;
        private colKonfigPivotGridaTrupi konfigPivotGridaTrupi;

        //private string layout;
        private string fieldStateLayout;

        private bool totalRreshta;
        private bool grandTotalRreshta;
        private bool totalKolona;
        private bool grandTotalKolona;
        private bool hiqVleraZero;
        private string filterExpression;
        private string autorizimet;
        private colLidhjetAutorizim oLidhjetAutorizim;
        private int idGjuha;
        private DataRow row;

        #endregion Atributet

        #region Konstruktoret

        /// <summary>
        /// Konstruktori me parametra i klases 
        /// </summary>
        /// <param name="idKonfig">  id e konfig koka </param>
        /// <param name="emer">      emri i konfigurimit </param>
        /// <param name="pershkrim"> pershkrimi i konfigurimit </param>
        /// <param name="idNder">    id e ndermarjes </param>
        /// <param name="idPerd">    id e perdoruesit </param>
        /// <param name="idStatus">  id status dokumenti </param>
        public clsKonfigPivotGridaKoka(int idKonfig, string emer, string pershkrim, int idMod, int idNder, int idPerd, int idStatus)
        {
            idKonfPivotGridaKoka = idKonfig;
            emerPivotGridaKoka = emer;
            pershkrimiPivotGridaKoka = pershkrim;
            idModuli = idMod;
            idNdermarje = idNder;
            idPerdoruesi = idPerd;
            idStatusDok = idStatus;
            konfigPivotGridaTrupi = new colKonfigPivotGridaTrupi();
        }

        /// <summary>
        /// Konstruktori qe kthen objektin e konfigurimit te pivot grides me id qe i kalohet si parameter 
        /// </summary>
        /// <param name="idGjuha"> </param>
        /// <param name="idKonfig"> id e kokes se konfigurimit </param>
        public clsKonfigPivotGridaKoka(int idGjuha, int idKonfig)
        {
            DbCore.DbRegjistrim.clsDatabaseRegjistrim dbRegj = new DbCore.DbRegjistrim.clsDatabaseRegjistrim();
            mbushKonfigPivotGridaKoka(idGjuha, dbRegj.merrKonfigPGKokaSipasID(idKonfig));
            dbRegj.Dispose();
        }

        /// <summary>
        /// Konstruktori qe kthen objektin e konfigurimit te pivot grides me emrin qe i kalohet si parameter 
        /// </summary>
        /// <param name="idGjuha">   </param>
        /// <param name="emriKonfig"> emri i konfigurmit </param>
        /// <param name="idNder">     id e ndermarrjes </param>
        public clsKonfigPivotGridaKoka(int idGjuha, string emriKonfig, int idNder)
        {
            DbCore.DbRegjistrim.clsDatabaseRegjistrim dbRegj = new DbCore.DbRegjistrim.clsDatabaseRegjistrim();
            mbushKonfigPivotGridaKoka(idGjuha, dbRegj.merrKonfigPGKokaSipasKodit(emriKonfig, idNder));
            dbRegj.Dispose();
        }

        /// <summary>
        /// Konstruktori default i klases 
        /// </summary>
        public clsKonfigPivotGridaKoka()
        {
        }

        public clsKonfigPivotGridaKoka(int idGjuha, DataRow row)
        {
            
            mbushKonfigPivotGridaKoka(idGjuha, row);
        }

        #endregion Konstruktoret

        #region Properties

        /// <summary>
        /// Kthen/Vendos ID-ne qe gjenerohet automatikisht. 
        /// </summary>
        public int IdKonfPivotGridaKoka
        {
            get { return idKonfPivotGridaKoka; }
            set { idKonfPivotGridaKoka = value; }
        }

        /// <summary>
        /// Kthen/Vendos emrin e konfigurimit te pivot grid-es 
        /// </summary>
        public string EmerPivotGridaKoka
        {
            get { return emerPivotGridaKoka; }
            set { emerPivotGridaKoka = value; }
        }

        /// <summary>
        /// Kthen/Vendos pershkrimin e konfigurimit te pivot grid-es 
        /// </summary>
        public string PershkrimiPivotGridaKoka
        {
            get { return pershkrimiPivotGridaKoka; }
            set { pershkrimiPivotGridaKoka = value; }
        }

        /// <summary>
        /// Kthen/Vendos id e modulit te modelit te infos se artikullit 
        /// </summary>
        public int IdModuli
        {
            get { return idModuli; }
            set { idModuli = value; }
        }

        /// <summary>
        /// Kthen/Vendos id e ndermarrjes se modelit te infos se artikullit 
        /// </summary>
        public int IdNdermarje
        {
            get { return idNdermarje; }
            set { idNdermarje = value; }
        }

        /// <summary>
        /// Kthen/Vendos id se perdoruesit te konfigurimit te pivot grid-es 
        /// </summary>
        public int IdPerdoruesi
        {
            get { return idPerdoruesi; }
            set { idPerdoruesi = value; }
        }

        /// <summary>
        /// Kthen/Vendos id e statusit te konfigurimit te pivot grid-es 
        /// </summary>
        public int IdStatusDok
        {
            get { return idStatusDok; }
            set { idStatusDok = value; }
        }

        /// <summary>
        /// Kthen/Vendos daten e krijimit te konfigurimit te pivot grid-es 
        /// </summary>
        public DateTime DtKrijimi
        {
            get { return dtKrijimi; }
            set { dtKrijimi = value; }
        }

        /// <summary>
        /// Kthen/Vendos daten e modifikimit te konfigurimit te pivot grid-es 
        /// </summary>
        public DateTime DtModifikimi
        {
            get { return dtModifikimi; }
            set { dtModifikimi = value; }
        }

        /// <summary>
        /// Kthen/Vendos collection-in me trupat e konfigurimit te pivot grid-es 
        /// </summary>
        public colKonfigPivotGridaTrupi KonfigPivotGridaTrupi
        {
            get { return konfigPivotGridaTrupi; }
            set { konfigPivotGridaTrupi = value; }
        }

        /// <summary>
        /// kthen/vendos gjendjen 
        /// </summary>
        public string FieldStateLayout
        {
            get { return fieldStateLayout==null?string.Empty:fieldStateLayout; }
            set { fieldStateLayout = value; }
        }

        //public string Layout
        //{
        //    get { return layout; }
        //    set { layout = value; }
        //}
        /// <summary>
        /// percakton nese do shfaqet ose je Totali per rreshta
        /// </summary>
        public bool TotalRreshta
        {
            get { return totalRreshta; }
            set { totalRreshta = value; }
        }

        public bool GrandTotalRreshta
        {
            get { return grandTotalRreshta; }
            set { grandTotalRreshta = value; }
        }

        public bool TotalKolona
        {
            get { return totalKolona; }
            set { totalKolona = value; }
        }

        public bool GrandTotalKolona
        {
            get { return grandTotalKolona; }
            set { grandTotalKolona = value; }
        }
        public bool HiqVleraZero
        {
            get { return hiqVleraZero; }
            set { hiqVleraZero = value; }
        }
        /// <summary>
        /// kthen/vendos filterExpreesion per griden 
        /// </summary>
        public string FilterExpression
        {
            get { return filterExpression==null?string.Empty:filterExpression; }
            set { filterExpression = value; }
        }

        /// <summary>
        /// kthen/vendos autorizimet qe ka ky raport
        /// </summary>
        public string Autorizimet
        {
            get { return autorizimet; }
            set { autorizimet = value; }
        }
        /// <summary>
        /// kthen/vendos autorizimet per kete rekord 
        /// </summary>
        public colLidhjetAutorizim OLidhjetAutorizim
        {
            get { return oLidhjetAutorizim; }
            set { oLidhjetAutorizim = value; }
        }

        #endregion Properties

        #region Metoda Publike

        /// <summary>
        /// Kthen te gjitha konfigurimet e privot grides qe i perkasin ndermarrjes qe i kalohet si parameter 
        /// </summary>
        /// <param name="idNdermarje"></param>
        /// <returns></returns>
        public static DataTable ktheKonfigPGPerGride(int idNdermarje, int idPerdoruesi, int idModuli)
        {
            DbCore.DbRegjistrim.clsDatabaseRegjistrim dbRegj = new DbCore.DbRegjistrim.clsDatabaseRegjistrim();
            DataTable dt = dbRegj.merrKonfigPGKokaSipasNdermarrjesPerGride(idNdermarje, idPerdoruesi,idModuli);
            dbRegj.Dispose();
            return dt;
        }

        public static DataRow ktheKonfigPGSipasID(int idKonfigPG)
        {
            DbCore.DbRegjistrim.clsDatabaseRegjistrim dbRegj = new DbCore.DbRegjistrim.clsDatabaseRegjistrim();
            DataRow dr = dbRegj.merrKonfigPGKokaSipasID(idKonfigPG);
            dbRegj.Dispose();
            return dr;
        }

        /// <summary>
        /// Ruan konfigurimin e pivot grides 
        /// </summary>
        /// <returns>
        /// Mesazhin qe jep info ne lidhje me ruajtjen e sukseshme, ose gabimin qe ka ndodhur nese
        /// ka te tille
        /// </returns>
        public clsMesazh ruajKonfigurimPG()
        {
            clsDatabaseRegjistrim dbRegj = new clsDatabaseRegjistrim();
            clsMesazh mesazh;
            //dbRegj.krijoManager();
            try
            {
                dbRegj.beginTransaksion();

                mesazh = dbRegj.ruajKonfigPGKoka(out this.idKonfPivotGridaKoka, this.EmerPivotGridaKoka, this.PershkrimiPivotGridaKoka, this.IdModuli, this.IdNdermarje, this.IdPerdoruesi, this.IdStatusDok, this.FieldStateLayout, this.totalKolona, this.grandTotalKolona, this.totalRreshta, this.grandTotalRreshta, this.FilterExpression, this.HiqVleraZero);
                //Nqs ruhet me sukses koka e konfigurimit, vazhdojme me ruajtjen e trupit.
                if (!mesazh.Status)
                {
                    dbRegj.rollbackTransaksion();
                    return new clsMesazh(false, mesazh.PershkrimMesazhi);
                }

                DbAdmin.clsDatabaseAdmin dbAdmin = new DbAdmin.clsDatabaseAdmin(dbRegj );
                DbKontabiliteti.clsDatabaseKontabilitet dbKont = new DbKontabiliteti.clsDatabaseKontabilitet(dbRegj );
                foreach (DbAdmin.clsLidhjeAutorizim o in OLidhjetAutorizim)
                {
                    o.IdLloji = DbCore.DbKontabiliteti.clsLlojBuxheti.mbushIDLlojBuxheti("RaportePivotGrid", dbKont);
                    o.IdLidhese = this.idKonfPivotGridaKoka;
                    mesazh = dbAdmin.ruajLidhjeAutorizim(o.IdLidhjeAutorizim, o.IdLidhese, o.IdLloji, o.IdAutorizimeKoka, 1);
                    if (!mesazh.Status)
                        return mesazh;
                }

                foreach (clsKonfigPivotGridaTrupi tr in this.konfigPivotGridaTrupi)
                {
                    mesazh = dbRegj.ruajKonfigPGTrupi(this.idKonfPivotGridaKoka, tr.IdKolonaPG, tr.Visibility, tr.Zona, tr.Rendi, tr.Width, tr.LlojGrupimi);
                    if (!mesazh.Status)
                    {
                        dbRegj.rollbackTransaksion();
                        return new clsMesazh(false, mesazh.PershkrimMesazhi);
                    }
                }
                //Nqs ruhet me sukses edhe trupi kthejme mesazhin e suksesit
                dbRegj.commitTransaksion();
                return new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
                //Ne te gjitha rastet e tjera kthejme mesazhin me pershkrimin e gabimit qe ka ndodhur
            }
            catch (Exception e)
            {
                dbRegj.rollbackTransaksion();
                return new clsMesazh(false, e.Message);
            }
        }

        /// <summary>
        /// Modifikon koken dhe trupin e konfigurimit te raporteve 
        /// </summary>
        /// <returns>
        /// Mesazhin qe jep info ne lidhje me modifikimin e sukseshem, ose gabimin qe ka ndodhur
        /// nese ka te tille
        /// </returns>
        /// <param name="idGjuha"></param>
        public clsMesazh modifikoKonfigurimPG(int idGjuha)
        {
            clsDatabaseRegjistrim dbRegj = new clsDatabaseRegjistrim();
            DbAdmin.colLidhjetAutorizim colLidhjetAutorizim = new DbAdmin.colLidhjetAutorizim(IdKonfPivotGridaKoka, "RaportePivotGrid");
            clsMesazh mesazh = new clsMesazh();
            //dbRegj.krijoManager();

            try
            {
                dbRegj.beginTransaksion();
                mesazh = dbRegj.modifikoKonfigPGKoka(this.IdKonfPivotGridaKoka, this.EmerPivotGridaKoka, this.pershkrimiPivotGridaKoka, this.IdModuli, this.IdNdermarje, this.IdPerdoruesi, this.IdStatusDok, this.fieldStateLayout, this.totalKolona, this.grandTotalKolona, this.totalRreshta, this.grandTotalRreshta, this.filterExpression, this.HiqVleraZero);
                //Nqs ruhet me sukses koka konfigurimit te raportit, vazhdojme me ruajtjen e trupit.

                if (mesazh.Status)
                {
                    clsDatabaseAdmin dbAdmin = new DbAdmin.clsDatabaseAdmin(dbRegj );
                    DbKontabiliteti.clsDatabaseKontabilitet dbKont = new DbKontabiliteti.clsDatabaseKontabilitet(dbRegj );
                    for (int i = 0; i < this.OLidhjetAutorizim.Count; i++)
                    {
                        if (mesazh.Status)
                        {
                            int idAutorizimKoka = OLidhjetAutorizim[i].IdAutorizimeKoka;
                            if (idAutorizimKoka == -1)
                                continue;
                            OLidhjetAutorizim[i].IdLloji = DbCore.DbKontabiliteti.clsLlojBuxheti.mbushIDLlojBuxheti("RaportePivotGrid", dbKont);
                            OLidhjetAutorizim[i].IdLidhese = this.idKonfPivotGridaKoka;
                            clsLidhjeAutorizim lidhjeNjejte = colLidhjetAutorizim.Find(x => x.IdAutorizimeKoka == idAutorizimKoka);
                            if (lidhjeNjejte != null)
                            {   //i heqim nga collectioni autorizimet qe sjane ndryshuar sepse ne te do ngelen vetem autorizimet qe do te fshihen(vendosen status 2)
                                colLidhjetAutorizim.Remove(lidhjeNjejte);
                                continue;
                            }
                            mesazh = dbAdmin.ruajLidhjeAutorizim(OLidhjetAutorizim[i].IdLidhjeAutorizim, OLidhjetAutorizim[i].IdLidhese, OLidhjetAutorizim[i].IdLloji, OLidhjetAutorizim[i].IdAutorizimeKoka, 1);
                        }
                        else
                        {
                            dbRegj.rollbackTransaksion();
                            mesazh.Status = false;
                            mesazh.PershkrimMesazhi = mesazh.PershkrimMesazhi;
                            return mesazh;
                        }
                    }
                    //fshihen autorizimet e vjetra. Ps: Jo ato qe kane ngelur njesoj, ato do ngelen si jane
                  
                    mesazh = colLidhjetAutorizim.FshiLidhjeAutorizim(colLidhjetAutorizim, IdPerdoruesi, dbAdmin);
                    if (!mesazh.Status)
                    {
                        dbRegj.rollbackTransaksion();
                        mesazh.Status = false;
                        mesazh.PershkrimMesazhi = mesazh.PershkrimMesazhi;
                        return mesazh;
                    }


                    DbCore.DbRegjistrim.colKonfigPivotGridaTrupi trupat = DbCore.DbRegjistrim.colKonfigPivotGridaTrupi.ktheKonfigPGSipasIDKoka(idGjuha, this.IdKonfPivotGridaKoka);

                    int t = 0;
                    int numer = 0;
                    foreach (clsKonfigPivotGridaTrupi tr in this.konfigPivotGridaTrupi)
                    {
                        numer++;
                        mesazh = dbRegj.modifikoKonfigPGTrupi(trupat[t++].IdKonfigPGTrupi, this.idKonfPivotGridaKoka, tr.IdKolonaPG, tr.Visibility, tr.Zona, tr.Rendi, tr.Width, tr.LlojGrupimi);
                        if (!mesazh.Status)
                        {
                            dbRegj.rollbackTransaksion();
                            return new clsMesazh(false, mesazh.PershkrimMesazhi);
                        }
                    }
                    //Nqs ruhet me sukses edhe trupi kthejme mesazhin e suksesit
                    if (mesazh.Status)
                    {
                        dbRegj.commitTransaksion();
                        return new clsMesazh(true, MessagesResource.Messages["msgModifikimiMeSukses"]);
                    }
                    //Ne te gjitha rastet e tjera kthejme mesazhin me pershkrimin e gabimit qe ka ndodhur
                    else
                    {
                        dbRegj.rollbackTransaksion();
                        return new clsMesazh(false, mesazh.PershkrimMesazhi);
                    }
                }
                else
                {
                    dbRegj.rollbackTransaksion();
                    return new clsMesazh(false, mesazh.PershkrimMesazhi);
                }
            }
            catch (Exception e)
            {
                dbRegj.rollbackTransaksion();
                return new clsMesazh(false, e.Message);
            }
        }

        /// <summary>
        /// Fshin konfigurimin e raporteve 
        /// </summary>
        /// <returns>
        /// Mesazhin qe jep info ne lidhje me fshirjen e sukseshme, ose gabimin qe ka ndodhur nese
        /// ka te tille
        /// </returns>
        public clsMesazh fshiKonfigurimPG()
        {
            DbCore.DbRegjistrim.clsDatabaseRegjistrim dbRegj = new DbCore.DbRegjistrim.clsDatabaseRegjistrim();
            clsMesazh mesazh = new clsMesazh();
            //dbRegj.krijoManager();
            dbRegj.beginTransaksion();
            try
            {
                //Ndryshon statusin e konfigurimit te raportit(nuk e fshin ate plotesisht nga DB)
                mesazh = dbRegj.fshiKonfigPGStatus(this.idKonfPivotGridaKoka, this.idPerdoruesi);
                if (mesazh.Status)
                {
                    //Ne rastin kur fshirja behet ne menyre te sukseshme bejme commit transkasionin dhe kthejme mesazhin e suksesit
                    dbRegj.commitTransaksion();
                    return new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
                }
                //Ne rastet e tjera, kur ka ndodhur nje gabim, kthehet mesazhi me pershkrimin e gabimit
                else
                {
                    dbRegj.rollbackTransaksion();
                    return new clsMesazh(false, mesazh.PershkrimMesazhi);
                }
            }
            catch (Exception e)
            {
                dbRegj.rollbackTransaksion();
                return new clsMesazh(false, e.Message);
            }
        }

        #endregion Metoda Publike

        #region Metoda Internal




        internal bool mbushKonfigPivotGridaKoka(int idGjuha, DataRow dbDataRowKonfigPGKoka)
        {
            if (dbDataRowKonfigPGKoka != null)
            {
                try
                {
                    int.TryParse(dbDataRowKonfigPGKoka["IDKONFPIVOTGRIDAKOKA"].ToString(), out idKonfPivotGridaKoka);
                    emerPivotGridaKoka = dbDataRowKonfigPGKoka["EMRIPIVOTGRIDKOKA"].ToString();
                    pershkrimiPivotGridaKoka = dbDataRowKonfigPGKoka["PERSHKRIMIPIVOTGRIDKOKA"].ToString();
                    int.TryParse(dbDataRowKonfigPGKoka["IDNDERMARJE"].ToString(), out idNdermarje);
                    int.TryParse(dbDataRowKonfigPGKoka["IDPERDORUESI"].ToString(), out idPerdoruesi);
                    int.TryParse(dbDataRowKonfigPGKoka["IDSTATUSDOK"].ToString(), out idStatusDok);
                    DateTime.TryParse(dbDataRowKonfigPGKoka["DTKRIJIMI"].ToString(), out dtKrijimi);
                    DateTime.TryParse(dbDataRowKonfigPGKoka["DTMODIFIKIMI"].ToString(), out dtModifikimi);
                    //layout =Convert.ToString(dbDataRowKonfigPGKoka["LAYOUT"]);
                    fieldStateLayout = Convert.ToString(dbDataRowKonfigPGKoka["COLLAPSEDSTATELAYOUT"]);

                    bool.TryParse(Convert.ToString(dbDataRowKonfigPGKoka["GRANDTOTALRRESHTA"]), out grandTotalRreshta);
                    bool.TryParse(Convert.ToString(dbDataRowKonfigPGKoka["TOTALRRESHTA"]), out totalRreshta);
                    bool.TryParse(Convert.ToString(dbDataRowKonfigPGKoka["GRANDTOTALKOLONA"]), out grandTotalKolona);
                    bool.TryParse(Convert.ToString(dbDataRowKonfigPGKoka["TOTALKOLONA"]), out totalKolona);
                    bool.TryParse(Convert.ToString(dbDataRowKonfigPGKoka["HIQVLERAZERO"]), out hiqVleraZero);

                    filterExpression = Convert.ToString(dbDataRowKonfigPGKoka["FILTEREXPRESSION"]);
                    autorizimet =Convert.ToString(dbDataRowKonfigPGKoka["AUTORIZIME"]);
                    konfigPivotGridaTrupi = DbCore.DbRegjistrim.colKonfigPivotGridaTrupi.ktheKonfigPGSipasIDKoka(idGjuha, idKonfPivotGridaKoka);
                    return true;
                }
                catch (InvalidCastException)
                {
                    throw new Exception("ERROR: Gabim gjate marrjes se kokes se konfigurimit te pivot gird-es nga DB!");
                }
            }
            else
                return false;
        }

        #endregion Metoda Internal
    }
}