using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;
using DbCore.IMBUtils.Messages;
using DbCore.DbInventari;
using DbCore.DbRegjistrim;

namespace DbCore.DbAdmin
{
    public class clsKokaFormatImporti
    {
        /// <summary>
        /// Kjo klase permban metodat e nevojshme per te perdorur te dhenat e tabeles T_KOKAFORMATIMPORTI
        /// </summary>

        #region Atribute

        private int idKoka;
        private string kodi;
        private string pershkrimi;
        private int idKategori;
        private int idNdermarje;
        private int idPerdoruesi;
        private int idStatusDok;
        private DateTime dtKrijimit;
        private DateTime dtModifikimit;
        private colTrupiFormatImporti colTrupi;
        private string kategori;
        private int idSuperKategori;
        private DataRow rreshti;

        #endregion

        #region Konstruktoret

        /// <summary>
        /// Konstruktori me parametra
        /// </summary>
        /// <param name="idKoka">id </param>
        /// <param name="kodi">kodi</param>
        /// <param name="pershkrimi">pershkrimi</param>
        /// <param name="idkategori">kategoria</param>
        /// <param name="idNder">id ndermarrjes</param>
        /// <param name="idPerd">id perdoruesit</param>
        /// <param name="idStatus">id statusit dok</param>
        public clsKokaFormatImporti(int idKoka, string kodi, string pershkrimi, int idkategori, int idNder,
                                    int idPerd, int idStatus)
        {
            this.idKoka = idKoka;
            this.kodi = kodi;
            this.pershkrimi = pershkrimi;
            this.idKategori = idkategori;
            this.idNdermarje = idNder;
            this.idPerdoruesi = idPerd;
            this.idStatusDok = idStatus;
            colTrupi = new colTrupiFormatImporti();
        }
        /// <summary>
        /// Konstruktori qe kthen formatin e importit sipas id qe i kalohet si parameter
        /// </summary>
        /// <param name="idKoka">id </param>
        public clsKokaFormatImporti(int idKoka)
        {
            clsDatabaseAdmin data = new clsDatabaseAdmin();
            mbushFormatImporti(data.merrFormatImportiSipasID(idKoka));
            data.Dispose();
        }

        /// <summary>
        /// Konstruktori qe kthen formatin e importit sipas id qe i kalohet si parameter
        /// </summary>
        /// <param name="idKoka">id </param>
        public clsKokaFormatImporti(int idKoka, clsDatabaseAdmin data)
        {            
            mbushFormatImporti(data.merrFormatImportiSipasID(idKoka));            
        }

        /// <summary>
        /// Konstruktori qe kthen format importi sipas kodit dhe ndermarrjes qe i kalohet si parameter
        /// </summary>
        /// <param name="kodi">kodi </param>
        /// <param name="idNdermarje">id ndermarrjes </param>
        public clsKokaFormatImporti(string kodi, int idNdermarje)
        {
            clsDatabaseAdmin data = new clsDatabaseAdmin();
            mbushFormatImporti(data.merrFormatImportiSipasKodit(kodi, idNdermarje));
            data.Dispose();
        }

        /// <summary>
        /// Konstruktori default
        /// </summary>
        public clsKokaFormatImporti()
        {

        }

        public clsKokaFormatImporti(DataRow rreshti)
        {
            
            mbushFormatImporti(rreshti);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Kthen/Vendos ID-ne qe gjenerohet automatikisht.
        /// </summary>
        public int IdKoka
        {
            get { return idKoka; }
            set { idKoka = value; }
        }

        /// <summary>
        /// Kthen/Vendos kodin 
        /// </summary>
        public string Kodi
        {
            get { return kodi; }
            set { kodi = value; }
        }


        /// <summary>
        /// Kthen/Vendos pershkrimin 
        /// </summary>
        public string Pershkrimi
        {
            get { return pershkrimi; }
            set { pershkrimi = value; }
        }


        /// <summary>
        /// Kthen/Vendos  kategorine e formatit te importit
        /// </summary>
        public int IdKategori
        {
            get { return idKategori; }
            set { idKategori = value; }
        }

        /// <summary>
        /// Kthen/Vendos id e ndermarrjes 
        /// </summary>
        public int IdNdermarje
        {
            get { return idNdermarje; }
            set { idNdermarje = value; }
        }

        /// <summary>
        /// Kthen/Vendos id se perdoruesit 
        /// </summary>
        public int IdPerdoruesi
        {
            get { return idPerdoruesi; }
            set { idPerdoruesi = value; }
        }


        /// <summary>
        /// Kthen/Vendos id e statusit 
        /// </summary>
        public int IdStatusDok
        {
            get { return idStatusDok; }
            set { idStatusDok = value; }
        }

        /// <summary>
        /// Kthen/Vendos daten e krijimit 
        /// </summary>
        public DateTime DtKrijimit
        {
            get { return dtKrijimit; }
            set { dtKrijimit = value; }
        }

        /// <summary>
        /// Kthen/Vendos daten e modifikimit 
        /// </summary>
        public DateTime DtModifikimit
        {
            get { return dtModifikimit; }
            set { dtModifikimit = value; }
        }

        /// <summary>
        /// Kthen/Vendos collection-in me trupat 
        /// </summary>
        public colTrupiFormatImporti ColTrupi
        {
            get { return colTrupi; }
            set { colTrupi = value; }
        }


        public string Kategori
        {
            get
            {
                return kategori;
            }
            set
            {
                kategori = value;
            }
        }
        public int IdSuperKategori
        {
            get
            {
                return idSuperKategori;
            }
            set
            {
                idSuperKategori = value;
            }
        }
        #endregion

        #region Metoda Publike

        /// <summary>
        /// Ruan formatin e importit
        /// </summary>
        /// <returns>Mesazhin qe jep info ne lidhje me ruajtjen e sukseshme, ose gabimin qe ka ndodhur nese ka te tille</returns>
        public clsMesazh ruajFormatImporti()
        {
            clsDatabaseAdmin dbAdmin = new clsDatabaseAdmin();
            clsMesazh mesazh;

            dbAdmin.beginTransaksion();
            try
            {
                //Kontrollome nese ekziston ne DB model format importi, brenda ndermarrjes
                if (dbAdmin.ekzistonFormatImportiSipasKodNdermarje(this.kodi, this.idNdermarje))
                {
                    return new clsMesazh(false, "Ekziston nje format importi  me kete kod!");
                }
                int id;
                mesazh = dbAdmin.ruajFormatImportiKoka(out id, this.Kodi, this.Pershkrimi, this.IdKategori, this.IdNdermarje, this.IdPerdoruesi, this.IdStatusDok);
                if (!mesazh.Status)
                {
                    dbAdmin.rollbackTransaksion();
                    return new clsMesazh(false, mesazh.PershkrimMesazhi);
                }
                this.idKoka = id;
                foreach (clsTrupiFormatImporti tr in this.colTrupi)
                {
                    int idtrupi = 0;
                    mesazh = dbAdmin.ruajFormatImportiTrupi(idtrupi, id, tr.IdKontroll, tr.EmerImporti, tr.VleraDefault, tr.Visible, tr.Rendi, tr.Detyrueshme, tr.Shfaq, tr.Tipi, tr.DetyrueshmeDefault, tr.FusheKokeApoTrupi, tr.FusheType);
                    if (!mesazh.Status)
                    {
                        dbAdmin.rollbackTransaksion();
                        return new clsMesazh(false, mesazh.PershkrimMesazhi);
                    }
                }
                //Nqs ruhet me sukses edhe trupi kthejme mesazhin e suksesit
                dbAdmin.commitTransaksion();
                return new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
                //Ne te gjitha rastet e tjera kthejme mesazhin me pershkrimin e gabimit qe ka ndodhur                                             
            }
            catch (Exception e)
            {
                dbAdmin.rollbackTransaksion();
                return new clsMesazh(false, e.Message);
            }
        }

        /// <summary>
        /// Modifikon koken dhe trupin
        /// </summary>
        /// <returns>Mesazhin qe jep info ne lidhje me modifikimin e sukseshem, ose gabimin qe ka ndodhur nese ka te tille</returns>
        public clsMesazh modifikoFormatImporti()
        {
            clsDatabaseAdmin dbAdmin = new clsDatabaseAdmin();
            clsMesazh mesazh;

            dbAdmin.beginTransaksion();
            try
            {
                mesazh = dbAdmin.modifikoFormatImportiKoka(this.idKoka, this.Kodi, this.Pershkrimi, this.IdKategori, this.IdNdermarje, this.IdPerdoruesi, this.IdStatusDok);
                //Nqs ruhet me sukses koka e infos, vazhdojme me ruajtjen e trupit.
                if (!mesazh.Status)
                {
                    dbAdmin.rollbackTransaksion();
                    return new clsMesazh(false, mesazh.PershkrimMesazhi);
                }
                mesazh = dbAdmin.fshiFormatImportiTrupiSipasIdKoka(this.idKoka);
                if (!mesazh.Status)
                {
                    dbAdmin.rollbackTransaksion();
                    return new clsMesazh(false, mesazh.PershkrimMesazhi);
                }
                foreach (clsTrupiFormatImporti tr in this.colTrupi)
                {
                    int idtrupi = 0;
                    mesazh = dbAdmin.ruajFormatImportiTrupi(idtrupi, this.idKoka, tr.IdKontroll, tr.EmerImporti, tr.VleraDefault, tr.Visible, tr.Rendi, tr.Detyrueshme, tr.Shfaq, tr.Tipi, tr.DetyrueshmeDefault, tr.FusheKokeApoTrupi, tr.FusheType);
                    if (!mesazh.Status)
                    {
                        dbAdmin.rollbackTransaksion();
                        return new clsMesazh(false, mesazh.PershkrimMesazhi);
                    }
                }
                //Nqs ruhet me sukses edhe trupi kthejme mesazhin e suksesit                    
                dbAdmin.commitTransaksion();
                return new clsMesazh(true, MessagesResource.Messages["msgModifikimiMeSukses"]);
            }
            catch (Exception e)
            {
                dbAdmin.rollbackTransaksion();
                return new clsMesazh(false, e.Message);
            }
        }

        /// <summary>
        /// Fshin format importi
        /// </summary>
        /// <returns>Mesazhin qe jep info ne lidhje me fshirjen e sukseshme, ose gabimin qe ka ndodhur nese ka te tille</returns>
        public clsMesazh fshiFormatImporti()
        {
            DbCore.DbAdmin.clsDatabaseAdmin dbAdmin = new DbCore.DbAdmin.clsDatabaseAdmin();
            clsMesazh mesazh;

            dbAdmin.beginTransaksion();
            try
            {
                //Ndryshon statusin e infos se t(nuk e fshin ate plotesisht nga DB)
                mesazh = dbAdmin.fshiFormatImportiStatus(this.idKoka, this.idPerdoruesi);
                if (!mesazh.Status)
                {
                    dbAdmin.rollbackTransaksion();
                    return new clsMesazh(false, mesazh.PershkrimMesazhi);
                }
                //Ne rastin kur fshirja behet ne menyre te sukseshme bejme commit transkasionin dhe kthejme mesazhin e suksesit
                dbAdmin.commitTransaksion();
                return new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
            }
            catch (Exception e)
            {
                dbAdmin.rollbackTransaksion();
                return new clsMesazh(false, e.Message);
            }
        }


        /// <summary>
        /// Kthen nje datarow  me formatin e importit me kete id
        /// </summary>
        /// <returns></returns>
        public static DataRow ktheFormatImportiSipasID(int idInfo)
        {
            DbCore.DbAdmin.clsDatabaseAdmin dbAdmin = new DbCore.DbAdmin.clsDatabaseAdmin();
            DataRow result = dbAdmin.merrFormatImportiSipasID(idInfo);
            dbAdmin.Dispose();
            return result;
        }

        public static clsMesazh ruajTeDhenaNeTabeleEksporti(int idNdermarrje)
        {
            DbCore.DbAdmin.clsDatabaseAdmin dbAdmin = new DbCore.DbAdmin.clsDatabaseAdmin();
            clsMesazh sukses = dbAdmin.ruajTeDhenaNeTabeleEksporti(idNdermarrje);
            dbAdmin.Dispose();
            return sukses;
        }

        public static DataTable merrTeDhenatPerTabeleEksporti(int idNdermarrje)
        {
            DbCore.DbAdmin.clsDatabaseAdmin dbAdmin = new DbCore.DbAdmin.clsDatabaseAdmin();
            DataTable dt = dbAdmin.merrTeDhenatPerTabeleEksporti(idNdermarrje);
            dbAdmin.Dispose();
            return dt;
        }
        public static int ktheIdKokaSipasKoditdheKategorise(int idndermarrje, int idkategori, string kodi)
        {
            using (clsDatabaseAdmin db = new clsDatabaseAdmin())
                return db.ktheIdKokaFormatiSipasKoditdheKategorise(idndermarrje, idkategori, kodi);
        }

        public static clsMesazh ruajTeDhenaNeTabeleEksportiDt(DataTable dt)
        {
            DbCore.DbAdmin.clsDatabaseAdmin dbAdmin = new DbCore.DbAdmin.clsDatabaseAdmin();
            clsMesazh sukses = dbAdmin.ruajTeDhenaNeTabeleEksportiDt(dt);
            dbAdmin.Dispose();
            return sukses;
        }

        public static clsMesazh kontrolloDataTable(DataTable dt, int idformati, DataTable error, DataTable rreshtaok, DataTable rreshtajoOk, bool importim, int pozicionkodi, int idKategori, colTrupiFormatImporti col, int idndermarjes)
        {
            clsMesazh mes = null;

            try
            {
                
                (string emerNjesia1, string skema, string vleradefaultnjesia, string vleradefaultskema) vlerat = ktheVleraDefaultKontrolli(col);
                string emerNjesia1 = vlerat.emerNjesia1, skema = vlerat.skema, vleradefaultnjesia = vlerat.vleradefaultnjesia, vleradefaultskema = vlerat.vleradefaultskema;

                int i = 1;
                foreach (DataRow dr in dt.Rows)
                {
                    mes = kontrolloDataRow(dr, error, rreshtaok, rreshtajoOk, importim, pozicionkodi, idKategori, col, i, emerNjesia1, skema, vleradefaultnjesia, vleradefaultskema, true, idndermarjes);
                    if (!mes.Status)
                        break;
                    i++;
                }
                if (mes == null)
                    return new clsMesazh(true);
            }
            catch (Exception ex)
            {
                mes = new clsMesazh(false);
                object[] err = { "X", "Te dhenat ne tabele nuk jane te sakta!", "X" };
                error.Rows.Add(err);         
            }
            return mes;
        }

        public static (string emerNjesia1, string skema, string vleradefaultnjesia, string vleradefaultskema) ktheVleraDefaultKontrolli(colTrupiFormatImporti col)
        {
            string emerNjesia1 = "", skema = "", vleradefaultnjesia = "", vleradefaultskema = "";
            
            foreach (clsTrupiFormatImporti trup in col)
            {
                switch (trup.KodKontrolli)
                {
                    case "Njesia 1":
                        emerNjesia1 = trup.EmerImporti;
                        vleradefaultnjesia = trup.VleraDefault;
                        break;
                    case "Skema":
                        skema = trup.EmerImporti;
                        vleradefaultskema = trup.VleraDefault;
                        break;
                }
            }
            return (emerNjesia1, skema, vleradefaultnjesia, vleradefaultskema);
        }

        public static clsMesazh kontrolloDataRow(DataRow dr, DataTable error, DataTable rreshtaok, DataTable rreshtajoOk, bool importim, int pozicionkodi, int idKategori, colTrupiFormatImporti col, int i, string emerNjesia1, string skema, string vleradefaultnjesia, string vleradefaultskema, bool removeRowsFromOkTable, int? idndermarjes = null)
        {
            clsMesazh mes = null;
            foreach (clsTrupiFormatImporti trup in col)
            {
                try
                {
                    if (trup.Visible && trup.Shfaq && !String.IsNullOrEmpty(trup.VleraDefault)) //nese eshte e shfaqur dhe ka vlere default
                        continue;
                    if (trup.Visible && trup.Shfaq || trup.Detyrueshme)
                    {
                        if (trup.KodKontrolli == "Operatori")
                        {
                            var kodOperatori = dr[trup.EmerImporti].ToString();
                            if (String.IsNullOrEmpty(kodOperatori) is false)
                            {
                                int rowFounded = clsOperator.MerrEmerMbiemerOperatoriSipasKodOperatori(kodOperatori, idndermarjes.Value);
                                if (rowFounded == 0)
                                {
                                    object[] err1 = { dr[pozicionkodi], $"Operatori {dr[trup.EmerImporti].ToString()} nuk ekziston", i };
                                    error.Rows.Add(err1);
                                }
                            }
                        }
                        if (trup.KodKontrolli == "Procesi")
                        {
                            var Procesi = dr[trup.EmerImporti].ToString();
                            if (String.IsNullOrEmpty(Procesi) is false)
                            {
                                var rowFoundedProcesi = clsKokaShitje.kthePershkrimProcesi(Procesi);
                                if (rowFoundedProcesi.Rows.Count == 0)
                                {
                                    object[] err1 = { dr[pozicionkodi], $"Procesi {dr[trup.EmerImporti].ToString()} nuk ekziston", i };
                                    error.Rows.Add(err1);
                                }
                            }
                        }
                        if (trup.KodKontrolli == "E-invoice Type")
                        {
                            var eInvoiceType = dr[trup.EmerImporti].ToString();
                            if (String.IsNullOrEmpty(eInvoiceType) is false)
                            {
                                var rowFoundedEinvoiceType = clsKokaShitje.kthePershkrimTipiEinvoice(eInvoiceType);
                                if (rowFoundedEinvoiceType.Rows.Count == 0)
                                {
                                    object[] err1 = { dr[pozicionkodi], $"E-invoice Type {dr[trup.EmerImporti].ToString()} nuk ekziston", i };
                                    error.Rows.Add(err1);
                                }
                            }
                        }
                        if (trup.KodKontrolli == "Tipi i vetefaturimit")
                        {
                            var TipVetefaturimi = dr[trup.EmerImporti].ToString();
                            if ((TipVetefaturimi == "AGREEMENT" || TipVetefaturimi == "DOMESTIC" || TipVetefaturimi == "ABROAD" || TipVetefaturimi == "SELF" || TipVetefaturimi == "OTHER" || TipVetefaturimi == "") is false)
                            {
                                object[] err1 = { dr[pozicionkodi], $"Tipi i vetefaturimit {dr[trup.EmerImporti].ToString()} nuk ekziston", i };
                                error.Rows.Add(err1);
                            }
                        }
                    }
                    if (trup.Visible && trup.Shfaq && trup.Detyrueshme && trup.VleraDefault == "" && dr[trup.EmerImporti].ToString() == "")
                    {
                        if (trup.KodKontrolli == "Njesia 2" || trup.KodKontrolli == "Koeficenti")
                            if (vleradefaultnjesia != "" || dr[emerNjesia1].ToString() != "")
                                continue;
                        if (trup.KodKontrolli == "Tipi Id")
                            if (dr["Kod Klienti Integrimi"] != null && dr["Kod Klienti Integrimi"].ToString() != "")
                                continue;
                        if (trup.KodKontrolli == "Llogari Inventari" || trup.KodKontrolli == "Llogari Blerje" || trup.KodKontrolli == "Llogari Shitje" || trup.KodKontrolli == "Llogari tek te Tretet" || trup.KodKontrolli == "Llogari Shpenzimi" || trup.KodKontrolli == "Llogari Amortizimi" || trup.KodKontrolli == "Llogari Pakesimi")
                            if (vleradefaultskema != "" || dr[skema].ToString() != "")
                                continue;
                        if (trup.KodKontrolli == "Klient/Furnitori")
                            if (dr["Kod Klienti Integrimi"] != null && dr["Kod Klienti Integrimi"].ToString() != "")
                                continue;
                        
                        object[] err = { dr[pozicionkodi], trup.EmerImporti + " nuk duhet te jete bosh!", i };

                        error.Rows.Add(err);                        
                        mes = new clsMesazh(false);
                        rreshtajoOk.ImportRow(dr);
                        if(removeRowsFromOkTable)
                            rreshtaok.Rows.RemoveAt(i - rreshtajoOk.Rows.Count);
                        if (importim)
                        {
                            error.Rows[error.Rows.Count - 1][2] = rreshtajoOk.Rows.Count;
                        }
                        break;
                    }
                }
                catch (Exception)
                {
                    mes = new clsMesazh(false);
                    object[] err = { dr[pozicionkodi], trup.EmerImporti + " ka gabim!", i };
                    error.Rows.Add(err);
                    rreshtajoOk.ImportRow(dr);
                    if (removeRowsFromOkTable)
                        rreshtaok.Rows.RemoveAt(i - rreshtajoOk.Rows.Count);
                    if (importim)
                    {
                        error.Rows[error.Rows.Count - 1][2] = rreshtajoOk.Rows.Count;
                    }
                }
            }
            if (idKategori == 1 || idKategori == 2)
            {

                mes = kontrolloFushaZbritjes(col, dr);
                if (!mes.Status)
                {
                    object[] err = { dr[pozicionkodi], mes.PershkrimMesazhi, i };
                    error.Rows.Add(err);
                    rreshtajoOk.ImportRow(dr);
                    if (removeRowsFromOkTable)
                        rreshtaok.Rows.RemoveAt(i - rreshtajoOk.Rows.Count);
                    if (importim)
                    {
                        error.Rows[error.Rows.Count - 1][2] = rreshtajoOk.Rows.Count;
                    }
                }
            }
            if (mes == null)
                return new clsMesazh(true);
            return mes;
        }

        private static clsMesazh kontrolloFushaZbritjes(colTrupiFormatImporti fushatZbritjes, DataRow dr)
        {

            clsTrupiFormatImporti llojZbritje = fushatZbritjes.Find(x => x.KodKontrolli.Equals("Lloj Zbritje Totale"));
            clsTrupiFormatImporti zbritjeTotali = fushatZbritjes.Find(x => x.KodKontrolli.Equals("Total Zbritje"));

            string llojZbritjeDefault = llojZbritje != null ? llojZbritje.VleraDefault : String.Empty;
            string llojZbritjeVlere = (llojZbritje != null && llojZbritje.Shfaq && dr.Table.Columns.Contains("Lloj Zbritje Totale")) ? dr["Lloj Zbritje Totale"].ToString() : String.Empty;
            decimal zbritjeTotaliDefault = (zbritjeTotali != null && zbritjeTotali.VleraDefault != "") ? Convert.ToDecimal(zbritjeTotali.VleraDefault) : 0;
            decimal zbritjeTotaliVlere = (zbritjeTotali != null && zbritjeTotali.Shfaq && dr.Table.Columns.Contains("Total Zbritje") && !String.IsNullOrEmpty(dr["Total Zbritje"].ToString())) ? Convert.ToDecimal(dr["Total Zbritje"]) : 0;
            bool visibleLlojZbritje = llojZbritje != null ? llojZbritje.Visible : false;
            bool shfaqLlojZbritje = llojZbritje != null ? llojZbritje.Shfaq : false;


            if ((!String.IsNullOrEmpty(llojZbritjeDefault) && !llojZbritjeDefault.Equals("Perqindje") && !llojZbritjeDefault.Equals("Vlere") && String.IsNullOrEmpty(llojZbritjeVlere)) || 
                (!String.IsNullOrEmpty(llojZbritjeVlere) && llojZbritjeVlere != "Perqindje" && llojZbritjeVlere != "Vlere"))
                    return new clsMesazh(false, "Lloji i zbritjes duhet te jete 'Perqindje' ose 'Vlere'!");
            
            if (visibleLlojZbritje && shfaqLlojZbritje && String.IsNullOrEmpty(llojZbritjeDefault) && String.IsNullOrEmpty(llojZbritjeVlere) && (zbritjeTotaliDefault != 0 || zbritjeTotaliVlere != 0))
                    return new clsMesazh(false, "Ju lutem plotesoni llojin e zbritjes totale!");
            if((llojZbritjeVlere.Equals("Perqindje") || (llojZbritjeDefault.Equals("Perqindje") && String.IsNullOrEmpty(llojZbritjeVlere))) && 
                ( (zbritjeTotaliVlere>100 || zbritjeTotaliVlere<0) || ((zbritjeTotaliDefault>100 || zbritjeTotaliDefault<0) && String.IsNullOrEmpty(dr["Total Zbritje"].ToString())) ))
                    return new clsMesazh(false, "Perqindja e zbritjes totale duhet te jete nje numer nga 0-100!");

            return new clsMesazh(true);
        }

        public static bool kaveprime(int id)
        {
            using (clsDatabaseAdmin db = new clsDatabaseAdmin())
            {
               return db.kaVeprimeFormatImporti(id);
            }
        }

        public static bool eshteFormatLidhurMeSQL(int idFormati)
        {
            using (clsDatabaseAdmin db = new clsDatabaseAdmin())
            {
                return db.eshteFormatLidhurMeSQL(idFormati);
            }
        }
        #endregion

        #region Metoda Internal

        internal bool mbushFormatImporti(DataRow dbDataRow)
        {
            if (dbDataRow != null)
            {
                try
                {
                    int.TryParse(dbDataRow["IDKOKA"].ToString(), out idKoka);
                    kodi = dbDataRow["KODI"].ToString();
                    pershkrimi = dbDataRow["PERSHKRIMI"].ToString();
                    kategori = dbDataRow["KATEGORI"].ToString();
                    int.TryParse(dbDataRow["IDKATEGORI"].ToString(), out idKategori);
                    int.TryParse(dbDataRow["IDNDERMARJE"].ToString(), out idNdermarje);
                    int.TryParse(dbDataRow["IDPERDORUESI"].ToString(), out idPerdoruesi);
                    int.TryParse(dbDataRow["IDSTATUSDOK"].ToString(), out idStatusDok);
                    int.TryParse(dbDataRow["IDSUPERKATEGORI"].ToString(), out idSuperKategori);
                    DateTime.TryParse(dbDataRow["DTKRIJIMI"].ToString(), out dtKrijimit);
                    DateTime.TryParse(dbDataRow["DTMODIFIKIMI"].ToString(), out dtModifikimit);
                    colTrupi = DbCore.DbAdmin.colTrupiFormatImporti.merrFormatImportiTrupiSipasIdKoka(idKoka);
                    return true;
                }
                catch (InvalidCastException)
                {
                    throw new Exception("ERROR: Gabim gjate marrjes se kokes se format importi  nga db-ja");
                }
            }
            else
                return false;
        }

        #endregion


        /// <summary>
        /// Funksion qe kontrollon nese ekziston formati sipas id.
        /// </summary>
        /// <param name="formati">id e kokes se formatit te importit</param>
        /// <returns>True nese ekziston, false ne rast te kundert</returns>
        public static bool ekzistonFormati(int idFormati)
        {
            if (idFormati == 0) 
                return false;
            clsDatabaseAdmin db = new clsDatabaseAdmin();
            bool ekziston = db.ekzistonFormatImportiSipasID(idFormati);
            db.Dispose();
            return ekziston;
        }
        /// <summary>
        /// Funksion qe kontrollon nese ekziston formati sipas id.
        /// </summary>
        /// <param name="formati">id e kokes se formatit te importit</param>
        /// <returns>True nese ekziston, false ne rast te kundert</returns>
        
    }
}
