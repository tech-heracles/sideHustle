using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using AlphaWeb.Core.SharedKernel;
using static System.Convert;

namespace DbCore.DbAdmin
{
    public class clsKonfigExporti
    {
        #region Konstruktoret
        
        /// <summary>
        /// Konstruktori me parametra
        /// </summary>
        /// <param name="id">id </param>
        /// <param name="emer">emer</param>
        /// <param name="tipi">tipi</param>
        /// <param name="idkategori">kategoria</param>
        /// <param name="emersheet">emer sheet excel</param>
        /// <param name="formati">formati i Exportit</param>
        /// <param name="idNder">id ndermarrjes</param>
        /// <param name="idPerd">id perdoruesit</param>
        /// <param name="idStatus">id statusit dok</param>
        /// <param name="emerskedari">emri i skedari ku do ruhet exporti</param>
        /// <param name="filtri"> filtri</param>
        public clsKonfigExporti(int id, string emer, string tipi, int idkategori, int formati, string emersheet, int idNder, int idPerd, int idStatus, string emerskedari, int filtri, DateTime datePerFiltrim, string formatDestinacion, string urlDestinacion, string ndermarrjeDestinacion, string emerTabKoka, string emerTabTrupi, int lloji, string emerTabRec, bool merrDokTeModifikuar, bool merrDokTeFshire)
        {
            Id = id;
            Emer = emer;
            Tipi = tipi;
            Kategoria = idkategori;
            Formati = formati;
            EmerSheet = emersheet;
            IdNdermarje = idNder;
            IdPerdoruesi = idPerd;
            IdStatusDok = idStatus;
            EmerSkedari = emerskedari;
            Filtri = filtri;
            DatePerFiltrim = datePerFiltrim;
            FormatDestinacion = formatDestinacion;
            UrlDestinacion = urlDestinacion;
            NdermarrjeDestinacion = ndermarrjeDestinacion;
            EmerTabKoka = emerTabKoka;
            EmerTabTrupi = emerTabTrupi;
            Lloji = lloji;
            EmerTabRec = emerTabRec;
            MerrDokTeModifikuar = merrDokTeModifikuar;
            MerrDokTeFshire = merrDokTeFshire;
        }

        /// <summary>
        /// Konstruktori qe kthen konfigurim e Exportit sipas id qe i kalohet si parameter
        /// </summary>
        /// <param name="idKoka">id </param>
        public clsKonfigExporti(int idKoka)
        {
            clsDatabaseAdmin data = new clsDatabaseAdmin();
            Mbush(data.merrKonfigurimExportiSipasID(idKoka));
            data.Dispose();
        }

        /// <summary>
        /// Konstruktori qe kthen konfigurim Exporti sipas kodit dhe ndermarrjes qe i kalohet si parameter
        /// </summary>
        /// <param name="kodi">kodi </param>
        /// <param name="idNdermarje">id ndermarrjes </param>
        public clsKonfigExporti(string kodi, int idNdermarje)
        {
            clsDatabaseAdmin data = new clsDatabaseAdmin();
            Mbush(data.merrKonfigurimExportiSipasKodit(kodi, idNdermarje));
            data.Dispose();
        }

        private clsKonfigExporti(IDataRecord rreshti)
        {
            Mbush(rreshti);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Kthen/Vendos ID-ne qe gjenerohet automatikisht.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Kthen/Vendos emer 
        /// </summary>
        public string Emer { get; set; }

        /// <summary>
        /// Kthen/Vendos tipin excel, text ect 
        /// </summary>
        public string Tipi { get; set; }


        /// <summary>
        /// Kthen/Vendos  kategorine 
        /// </summary>
        public int Kategoria { get; set; }

        /// <summary>
        /// kthen / vendos formatin 
        /// </summary>
        public int Formati { get; set; }

        /// <summary>
        /// kthen /vendos emrin e sheet
        /// </summary>
        public string EmerSheet { get; set; }
        
        /// <summary>
        /// emri i skedarit ku do ruhen te dhenat
        /// </summary>
        public string EmerSkedari { get; set; }

        /// <summary>
        /// filtrimi i te dhenave
        /// </summary>
        public int Filtri { get; set; }

        /// <summary>
        /// Kthen/Vendos id e ndermarrjes 
        /// </summary>
        public int IdNdermarje { get; set; }

        /// <summary>
        /// Kthen/Vendos id se perdoruesit 
        /// </summary>
        public int IdPerdoruesi { get; set; }


        /// <summary>
        /// Kthen/Vendos id e statusit 
        /// </summary>
        public int IdStatusDok { get; set; }

        /// <summary>
        /// Kthen/Vendos daten e krijimit 
        /// </summary>
        public DateTime DtKrijimit { get; private set; }

        /// <summary>
        /// Kthen/Vendos daten e modifikimit 
        /// </summary>
        public DateTime DtModifikimit { get; private set; }

        /// <summary>
        /// Kthen/Vendos daten qe do perdoret si filter
        /// </summary>
        public DateTime DatePerFiltrim { get; set; }

        /// <summary>
        /// kthen/vendos formatin destinacion (per transferim te faturave)
        /// </summary>
        public string FormatDestinacion { get; set; }

        /// <summary>
        /// kthen/vendos ndermarrjen destinacion (per transferim te faturave)
        /// </summary>
        public string NdermarrjeDestinacion { get; set; }

        /// <summary>
        /// kthen/vendos url destinacion (per transferim te faturave)
        /// </summary>
        public string UrlDestinacion { get; set; }

        /// <summary>
        /// kthen/vendos emrin e tabeles se kokes ku eksportohen te dhenat (per transferim te faturave)
        /// </summary>
        public string EmerTabKoka { get; set; }

        /// <summary>
        /// kthen/vendos emrin e tabeles se trupit ku eksportohen te dhenat (per transferim te faturave)
        /// </summary>
        public string EmerTabTrupi { get; set; }

        /// <summary>
        /// Kthen/Vendos llojin e import/eksportit, me sql apo me file
        /// </summary>
        public int Lloji { get; set; }

        /// <summary>
        /// kthen/vendos emrin e tabeles se recepturave ku eksportohen te dhenat (per transferim te faturave, ekzekutim prodhimi)
        /// </summary>
        public string EmerTabRec { get; set; }

        public bool MerrDokTeModifikuar { get; set; }

        public bool MerrDokTeFshire { get; set; }

        #endregion

        #region Metoda Publike

        public static List<clsKonfigExporti> KtheKonfigExportiNdermarrjesSipasTeDrejtave(int idNdermarja, int idViti, int idPerdoruesi, string komponente)
        {
            using (var data = new clsDatabaseAdmin())
                return data.MerrGjitheKonfigurimExportiNdermarrjesSipasTeDrejtave(idNdermarja, idViti, idPerdoruesi,
                    komponente).ToList();
        }

        public void RuajKonfigurimExporti(int idSuperKategori)
        {
            using (var scope = new MyTransactionScope())
            using (var dbAdmin = new clsDatabaseAdmin())
            {
                RuajKonfigExportiMeTabela(dbAdmin, out var col, idSuperKategori);

                if (Lloji == 2)
                {
                    RuajKonfigExportiMeSp(dbAdmin, col, idSuperKategori);
                }

                scope.Complete();
            }
        }

        public void ModifikoKonfigurimExporti(clsKonfigExporti oldKonfig, int idSuperKategori)
        {
            using (var scope = new MyTransactionScope())
            using (var dbAdmin = new clsDatabaseAdmin())
            {
                ModifikoKonfigExporti(dbAdmin, oldKonfig, out var col, idSuperKategori);

                if (Lloji == 2)
                {
                    RuajKonfigExportiMeSpModifikim(dbAdmin, col, oldKonfig, idSuperKategori);
                }
                scope.Complete();
            }
        }

        /// <summary>
        /// Fshi konfigurimin.
        /// </summary>
        /// <param name="id">Identifikuesi i konfigurimit.</param>
        /// <param name="idPerdoruesi">Identifikuesi i perdoruesit.</param>
        public static void Fshi(int id, int idPerdoruesi)
        {
            using (var dbAdmin = new clsDatabaseAdmin())
            using (var scope = new MyTransactionScope())
            {
                dbAdmin.fshiKonfigurimExportiStatus(id, idPerdoruesi);

                scope.Complete();
            }
        }

        #endregion

        #region Metoda Private

        /// <summary>
        /// Ruan konfig e Exportit
        /// </summary>
        /// <returns>Mesazhin qe jep info ne lidhje me ruajtjen e sukseshme, ose gabimin qe ka ndodhur nese ka te tille</returns>
        private void RuajKonfigExportiMeTabela(clsDatabaseAdmin dbAdmin, out colTrupiFormatImporti col, int idSuperKategori)
        {
            col = new colTrupiFormatImporti();

            //Kontrollome nese ekziston ne DB model format Exporti, brenda ndermarrjes
            if (Ekziston(dbAdmin))
            {
                throw new MyException("Ekziston nje konfigurim exporti  me kete kod!");
            }

            Id = dbAdmin.ruajKonfigurimExportiKoka(Emer, Tipi, Kategoria, Formati, EmerSheet, EmerSkedari, Filtri, IdNdermarje, IdPerdoruesi, IdStatusDok, DatePerFiltrim, FormatDestinacion, UrlDestinacion, NdermarrjeDestinacion, EmerTabKoka, EmerTabTrupi, Lloji, EmerTabRec, MerrDokTeModifikuar, MerrDokTeFshire);

            if (Lloji == 2)
            {
                //kontrollojme nqs ekzistojne tabelela me keto emertime
                var mesazh = clsFunksione.kontrolloEkzistenceTabelashDheSP(idSuperKategori, EmerTabKoka, EmerTabTrupi, true, Kategoria, EmerTabRec, false, null, null, null);
                if (mesazh.Status)
                {
                    throw new MyException(mesazh.PershkrimMesazhi);
                }

                col = colTrupiFormatImporti.merrFormatImportiTrupiSipasIdKoka(Formati, dbAdmin);

                KrijoTabelaEksportiSipasTemplate(col, EmerTabKoka, dbAdmin, 1);

                if (idSuperKategori == (int)SuperKategori.Regjistrime)
                {
                    KrijoTabelaEksportiSipasTemplate(col, EmerTabTrupi, dbAdmin, 2);

                    if (Kategoria == 45) //rasti per ekzekutim prodhimi
                    {
                        KrijoTabelaEksportiSipasTemplate(col, EmerTabRec, dbAdmin, 4);
                    }
                }
            }
        }

        private void RuajKonfigExportiMeSpModifikim(clsDatabaseAdmin dbAdmin, colTrupiFormatImporti col, clsKonfigExporti oldKonfig, int idSuperKategori)
        {
            if (oldKonfig.EmerTabKoka != EmerTabKoka)
            {
                KrijoStoredProcedurePerTabEksportiSelect(col, EmerTabKoka, dbAdmin, 1);
                KrijoStoredProcedurePerTabEksportiInsert(col, EmerTabKoka, dbAdmin, 1);
            }

            if (idSuperKategori == (int)SuperKategori.Regjistrime)
            {
                if (oldKonfig.EmerTabTrupi != EmerTabTrupi)
                {
                    KrijoStoredProcedurePerTabEksportiSelect(col, EmerTabTrupi, dbAdmin, 2);
                    KrijoStoredProcedurePerTabEksportiInsert(col, EmerTabTrupi, dbAdmin, 2);
                }

                if (Kategoria == 45 && oldKonfig.EmerTabRec != EmerTabRec)
                {
                    KrijoStoredProcedurePerTabEksportiSelect(col, EmerTabRec, dbAdmin, 4);
                    KrijoStoredProcedurePerTabEksportiInsert(col, EmerTabRec, dbAdmin, 4);
                }
            }

        }

        private void RuajKonfigExportiMeSp(clsDatabaseAdmin dbAdmin, colTrupiFormatImporti col, int idSuperKategori)
        {
            KrijoStoredProcedurePerTabEksportiSelect(col, EmerTabKoka, dbAdmin, 1);
            KrijoStoredProcedurePerTabEksportiInsert(col, EmerTabKoka, dbAdmin, 1);

            if (idSuperKategori == (int)SuperKategori.Regjistrime)
            {
                KrijoStoredProcedurePerTabEksportiSelect(col, EmerTabTrupi, dbAdmin, 2);
                KrijoStoredProcedurePerTabEksportiInsert(col, EmerTabTrupi, dbAdmin, 2);

                if (Kategoria == 45)
                {
                    KrijoStoredProcedurePerTabEksportiSelect(col, EmerTabRec, dbAdmin, 4);
                    KrijoStoredProcedurePerTabEksportiInsert(col, EmerTabRec, dbAdmin, 4);
                }

            }
        }

        /// <summary>
        /// Funksion qe krijon tabelat e eksportit ne databaze sipas formatit te caktuar.
        /// </summary>
        /// <param name="col"></param>
        /// <param name="emerTabele">Emri i tabeles qe do krijohet</param>
        /// <param name="dbAdmin"></param>
        /// <param name="kokeApoTrup">Perdoret per te percaktuar nese do krijohet tabele koke, apo trupi.</param>
        /// <returns></returns>
        private void KrijoTabelaEksportiSipasTemplate(colTrupiFormatImporti col, string emerTabele, clsDatabaseAdmin dbAdmin, int kokeApoTrup)
        {
            StringBuilder query = new StringBuilder();
            query.Append("CREATE TABLE ");
            query.Append(emerTabele);
            query.Append(" ( ");
            if (kokeApoTrup == 1)
                query.Append("IDEKSPORT INT IDENTITY, ");
            else if (kokeApoTrup == 2)
                query.Append("IDEKSPORTTRUPI INT IDENTITY, ");
            else if (kokeApoTrup == 4)
                query.Append("IDEKSPORTRECEPTURA INT IDENTITY, ");
            colTrupiFormatImporti colFushat = new colTrupiFormatImporti();
            foreach (clsTrupiFormatImporti tr in col)
            {
                if ((kokeApoTrup == 1 && ((tr.Visible && tr.FusheKokeApoTrupi == 1) || tr.FusheKokeApoTrupi == 3 || tr.KodKontrolli == "Kod Ndermarrje")) ||
                    (kokeApoTrup == 2 && ((tr.Visible && tr.FusheKokeApoTrupi == 2) || tr.FusheKokeApoTrupi == 3 || (Kategoria == 45 && tr.FusheKokeApoTrupi == 5))) ||
                    (kokeApoTrup == 4 && ((tr.Visible && tr.FusheKokeApoTrupi == 4) || tr.FusheKokeApoTrupi == 5)))
                {
                    colFushat.Add(tr);
                    query.Append("[");
                    query.Append(tr.EmerImporti);
                    query.Append("]");
                    query.Append(" ");
                    query.Append(tr.FusheType);
                    if (tr.FusheKokeApoTrupi == 3 || tr.KodKontrolli == "Kod Ndermarrje" || tr.FusheKokeApoTrupi == 5)
                        query.Append(" NOT NULL ");
                    query.Append(", ");
                }
            }
            if (kokeApoTrup == 1)
            {
                query.Append("DTLEXIMI");
                query.Append(" ");
                query.Append("DATETIME");
                query.Append(", ");
                query.Append("LEXUAR");
                query.Append(" ");
                query.Append("bit ");
                query.Append("CONSTRAINT DefValLexuar" + emerTabele + " DEFAULT 0");
                query.Append(", ");
            }
            if (colFushat.Count > 1)
            {
                query.Length -= 2;
            }
            query.Append(")");
            dbAdmin.krijoObjektSql(query);
        }

        private void KrijoStoredProcedurePerTabEksportiSelect(colTrupiFormatImporti col, string emerTabele, clsDatabaseAdmin dbAdmin, int kokeApoTrup)
        {
            var query = new StringBuilder();
            query.Append(" CREATE PROCEDURE prc_" + emerTabele + "_sel ");
            query.Append(" ");
            query.Append(" AS BEGIN ");
            query.Append(" SELECT ");
            foreach (clsTrupiFormatImporti tr in col)
            {
                if ((kokeApoTrup == 1 && ((tr.Visible && tr.FusheKokeApoTrupi == 1) || tr.FusheKokeApoTrupi == 3 || tr.KodKontrolli == "Kod Ndermarrje")) || (kokeApoTrup == 2 && ((tr.Visible && tr.FusheKokeApoTrupi == 2) || tr.FusheKokeApoTrupi == 3 || (Kategoria == 45 && tr.FusheKokeApoTrupi == 5))) || (kokeApoTrup == 4 && ((tr.Visible && tr.FusheKokeApoTrupi == 4) || tr.FusheKokeApoTrupi == 5)))
                {
                    query.Append("[");
                    query.Append(tr.EmerImporti);
                    query.Append("], ");
                }
            }

            query.Length -= 2;
            query.Append(" FROM ");
            query.Append(emerTabele); //mbaron krijimi i sp se selektit
            query.Append(" END ");

            dbAdmin.krijoObjektSql(query);
        }

        private void KrijoStoredProcedurePerTabEksportiInsert(colTrupiFormatImporti col, string emerTabele, clsDatabaseAdmin dbAdmin, int kokeApoTrup)
        {
            var query = new StringBuilder();
            clsTrupiFormatImporti idkoka = new clsTrupiFormatImporti();
            query.Append("CREATE PROCEDURE prc_" + emerTabele + "_ins");
            query.Append(" ( ");

            foreach (clsTrupiFormatImporti tr in col)
            {
                if ((kokeApoTrup == 1 && ((tr.Visible && tr.FusheKokeApoTrupi == 1) || tr.FusheKokeApoTrupi == 3 || tr.KodKontrolli == "Kod Ndermarrje")) || (kokeApoTrup == 2 && ((tr.Visible && tr.FusheKokeApoTrupi == 2) || tr.FusheKokeApoTrupi == 3 || (Kategoria == 45 && tr.FusheKokeApoTrupi == 5))) || (kokeApoTrup == 4 && ((tr.Visible && tr.FusheKokeApoTrupi == 4) || tr.FusheKokeApoTrupi == 5)))
                {
                    if (kokeApoTrup == 1 && tr.FusheKokeApoTrupi == 3)
                        idkoka = tr;
                    query.Append("@");
                    query.Append(tr.EmerImporti.Replace(" ", "").Replace("/", "").Replace("-", ""));
                    query.Append(" ");
                    query.Append(tr.FusheType);
                    query.Append(", ");
                }
            }
            query.Length -= 2;
            query.Append(" ) ");
            query.Append(" AS BEGIN ");
            query.Append(" DECLARE @Err Int	");
            query.Append(" INSERT INTO " + emerTabele);
            query.Append(" ( ");
            foreach (clsTrupiFormatImporti tr in col)
            {
                if ((kokeApoTrup == 1 && ((tr.Visible && tr.FusheKokeApoTrupi == 1) || tr.FusheKokeApoTrupi == 3 || tr.KodKontrolli == "Kod Ndermarrje")) || (kokeApoTrup == 2 && ((tr.Visible && tr.FusheKokeApoTrupi == 2) || tr.FusheKokeApoTrupi == 3 || (Kategoria == 45 && tr.FusheKokeApoTrupi == 5))) || (kokeApoTrup == 4 && ((tr.Visible && tr.FusheKokeApoTrupi == 4) || tr.FusheKokeApoTrupi == 5)))
                {
                    query.Append("[");
                    query.Append(tr.EmerImporti);
                    query.Append("], ");
                }
            }
            query.Length -= 2;
            query.Append(" )  Select   ");
            foreach (clsTrupiFormatImporti tr in col)
            {
                if ((kokeApoTrup == 1 && ((tr.Visible && tr.FusheKokeApoTrupi == 1) || tr.FusheKokeApoTrupi == 3 || tr.KodKontrolli == "Kod Ndermarrje")) || (kokeApoTrup == 2 && ((tr.Visible && tr.FusheKokeApoTrupi == 2) || tr.FusheKokeApoTrupi == 3 || (Kategoria == 45 && tr.FusheKokeApoTrupi == 5))) || (kokeApoTrup == 4 && ((tr.Visible && tr.FusheKokeApoTrupi == 4) || tr.FusheKokeApoTrupi == 5)))
                {
                    query.Append("@");
                    query.Append(tr.EmerImporti.Replace(" ", "").Replace("/", "").Replace("-", ""));
                    query.Append(", ");
                }
            }
            query.Length -= 2;
            if (kokeApoTrup == 1 && idkoka.IdTrupi > 0)
                query.Append($" where not exists (select 1 from {emerTabele} where [{idkoka.EmerImporti}]= @{idkoka.EmerImporti.Replace(" ", "").Replace("/", "").Replace("-", "")} )");
            query.Append(" Set @Err = @@Error RETURN @Err  END ");
            dbAdmin.krijoObjektSql(query);
        }

        /// <summary>
        /// Modifikon konfig Exporti
        /// </summary>
        /// <returns>Mesazhin qe jep info ne lidhje me modifikimin e sukseshem, ose gabimin qe ka ndodhur nese ka te tille</returns>
        private void ModifikoKonfigExporti(clsDatabaseAdmin dbAdmin, clsKonfigExporti oldKonfig, out colTrupiFormatImporti col, int idSuperKategori)
        {
            col = new colTrupiFormatImporti();
            dbAdmin.modifikoKonfigurimExportiKoka(Id, Emer, Tipi, Kategoria, Formati, EmerSheet, EmerSkedari, Filtri, IdNdermarje, IdPerdoruesi, IdStatusDok, DatePerFiltrim, FormatDestinacion,
                UrlDestinacion, NdermarrjeDestinacion, EmerTabKoka, EmerTabTrupi, Lloji, EmerTabRec,
                MerrDokTeModifikuar, MerrDokTeFshire);

            if (Lloji == 2)
            {
                if ((Kategoria != 45 && oldKonfig.Formati == Formati && oldKonfig.EmerTabKoka == EmerTabKoka &&
                     oldKonfig.EmerTabTrupi == EmerTabTrupi) ||
                    (Kategoria == 45 && oldKonfig.Formati == Formati && oldKonfig.EmerTabKoka == EmerTabKoka &&
                     oldKonfig.EmerTabTrupi == EmerTabTrupi && oldKonfig.EmerTabRec == EmerTabRec))
                {
                    //todo Check this Mihal
                    return;
                }

                if (oldKonfig.Formati != Formati && oldKonfig.EmerTabKoka == EmerTabKoka)
                {
                    throw new MyException("Tablela e kokes ekziston, ju lutemi zgjidhni nje emertim tjeter!");
                }

                if (idSuperKategori == (int)SuperKategori.Regjistrime)
                {
                    if (oldKonfig.Formati != Formati && oldKonfig.EmerTabTrupi == EmerTabTrupi)
                    {
                        throw new MyException("Tablela e trupit ekziston, ju lutemi zgjidhni nje emertim tjeter!");
                    }

                    if (Kategoria == 45 && oldKonfig.Formati != Formati && oldKonfig.EmerTabRec == EmerTabRec)
                    {
                        throw new MyException("Tablela e recepturave ekziston, ju lutemi zgjidhni nje emertim tjeter!");
                    }
                }

                var mesazh = clsFunksione.kontrolloEkzistenceTabelashDheSP(idSuperKategori, EmerTabKoka, EmerTabTrupi,
                    true, Kategoria, EmerTabRec, false, null, null, null);
                if (mesazh.Status)
                {
                    throw new MyException(mesazh.PershkrimMesazhi);
                }

                col = colTrupiFormatImporti.merrFormatImportiTrupiSipasIdKoka(Formati, dbAdmin);

                //krijojme tabelen e kokes
                KrijoTabelaEksportiSipasTemplate(col, EmerTabKoka, dbAdmin, 1);

                if (idSuperKategori == (int)SuperKategori.Regjistrime)
                {
                    //krijojme tabelen e trupit
                    KrijoTabelaEksportiSipasTemplate(col, EmerTabTrupi, dbAdmin, 2);

                    //krijojme tabelen e recepturave nese kategoria eshte ekzekutim prodhimi
                    if (Kategoria == 45)
                    {
                        KrijoTabelaEksportiSipasTemplate(col, EmerTabRec, dbAdmin, 4);
                    }
                }
            }
        }

        private void Mbush(DataRow dbDataRow)
        {
            if (dbDataRow != null)
            {
                try
                {
                    Id = !IsDBNull(dbDataRow["ID"])
                        ? ToInt32(dbDataRow["ID"])
                        : 0;
                    Emer = dbDataRow["EMER"].ToString();
                    Tipi = dbDataRow["TIPI"].ToString();
                    EmerSheet = dbDataRow["EMERSHEET"].ToString();
                    EmerSkedari = dbDataRow["EMERSKEDARI"].ToString();
                    Kategoria = !IsDBNull(dbDataRow["KATEGORIA"])
                        ? ToInt32(dbDataRow["KATEGORIA"])
                        : 0;
                    Formati = !IsDBNull(dbDataRow["FORMATI"])
                        ? ToInt32(dbDataRow["FORMATI"])
                        : 0;
                    IdNdermarje = !IsDBNull(dbDataRow["IDNDERMARJE"])
                        ? ToInt32(dbDataRow["IDNDERMARJE"])
                        : 0;
                    IdPerdoruesi = !IsDBNull(dbDataRow["IDPERDORUESI"])
                        ? ToInt32(dbDataRow["IDPERDORUESI"])
                        : 0;
                    IdStatusDok = !IsDBNull(dbDataRow["IDSTATUSDOK"])
                        ? ToInt32(dbDataRow["IDSTATUSDOK"])
                        : 0;
                    Filtri = !IsDBNull(dbDataRow["FILTRI"])
                        ? ToInt32(dbDataRow["FILTRI"])
                        : 0;
                    DtKrijimit = !IsDBNull(dbDataRow["DTKRIJIMI"])
                        ? ToDateTime(dbDataRow["DTKRIJIMI"])
                        : DateTime.MinValue;
                    DtModifikimit = !IsDBNull(dbDataRow["DTMODIFIKIMI"])
                        ? ToDateTime(dbDataRow["DTMODIFIKIMI"])
                        : DateTime.MinValue;
                    DatePerFiltrim = !IsDBNull(dbDataRow["DATEPERFILTRIM"])
                        ? ToDateTime(dbDataRow["DATEPERFILTRIM"])
                        : DateTime.MinValue;
                    FormatDestinacion = dbDataRow["FORMATDESTINACION"].ToString();
                    UrlDestinacion = dbDataRow["URLDESTINACION"].ToString();
                    NdermarrjeDestinacion = dbDataRow["NDERMARRJEDESTINACION"].ToString();
                    EmerTabKoka = dbDataRow["EMERTABELEKOKA"].ToString();
                    EmerTabTrupi = dbDataRow["EMERTABELETRUPI"].ToString();
                    Lloji = !IsDBNull(dbDataRow["LLOJI"])
                        ? ToInt32(dbDataRow["LLOJI"])
                        : 0;
                    EmerTabRec = dbDataRow["EMERTABELEREC"].ToString();
                    MerrDokTeModifikuar = !IsDBNull(dbDataRow["MERRDOKTEMODIFIKUAR"]) &&
                                          ToBoolean(dbDataRow["MERRDOKTEMODIFIKUAR"]);
                    MerrDokTeFshire = !IsDBNull(dbDataRow["MERRDOKTEFSHIRE"]) &&
                                      ToBoolean(dbDataRow["MERRDOKTEFSHIRE"]);
                }
                catch (InvalidCastException)
                {
                    throw new Exception("ERROR: Gabim gjate marrjes se konfig exporti  nga db-ja");
                }
            }
        }

        private bool Ekziston(clsDatabaseAdmin db)
        {
            return db.ekzistonKonfigurimExportiSipasKodNdermarje(Emer, IdNdermarje);
        }

        private void Mbush(IDataRecord dbDataRow)
        {
            if (dbDataRow != null)
            {
                try
                {
                    Id = !IsDBNull(dbDataRow["ID"])
                        ? ToInt32(dbDataRow["ID"])
                        : 0;
                    Emer = dbDataRow["EMER"].ToString();
                    Tipi = dbDataRow["TIPI"].ToString();
                    EmerSheet = dbDataRow["EMERSHEET"].ToString();
                    EmerSkedari = dbDataRow["EMERSKEDARI"].ToString();
                    Kategoria = !IsDBNull(dbDataRow["KATEGORIA"])
                        ? ToInt32(dbDataRow["KATEGORIA"])
                        : 0;
                    Formati = !IsDBNull(dbDataRow["FORMATI"])
                        ? ToInt32(dbDataRow["FORMATI"])
                        : 0;
                    IdNdermarje = !IsDBNull(dbDataRow["IDNDERMARJE"])
                        ? ToInt32(dbDataRow["IDNDERMARJE"])
                        : 0;
                    IdPerdoruesi = !IsDBNull(dbDataRow["IDPERDORUESI"])
                        ? ToInt32(dbDataRow["IDPERDORUESI"])
                        : 0;
                    IdStatusDok = !IsDBNull(dbDataRow["IDSTATUSDOK"])
                        ? ToInt32(dbDataRow["IDSTATUSDOK"])
                        : 0;
                    Filtri = !IsDBNull(dbDataRow["FILTRI"])
                        ? ToInt32(dbDataRow["FILTRI"])
                        : 0;
                    DtKrijimit = !IsDBNull(dbDataRow["DTKRIJIMI"])
                        ? ToDateTime(dbDataRow["DTKRIJIMI"])
                        : DateTime.MinValue;
                    DtModifikimit = !IsDBNull(dbDataRow["DTMODIFIKIMI"])
                        ? ToDateTime(dbDataRow["DTMODIFIKIMI"])
                        : DateTime.MinValue;
                    DatePerFiltrim = !IsDBNull(dbDataRow["DATEPERFILTRIM"])
                        ? ToDateTime(dbDataRow["DATEPERFILTRIM"])
                        : DateTime.MinValue;
                    FormatDestinacion = dbDataRow["FORMATDESTINACION"].ToString();
                    UrlDestinacion = dbDataRow["URLDESTINACION"].ToString();
                    NdermarrjeDestinacion = dbDataRow["NDERMARRJEDESTINACION"].ToString();
                    EmerTabKoka = dbDataRow["EMERTABELEKOKA"].ToString();
                    EmerTabTrupi = dbDataRow["EMERTABELETRUPI"].ToString();
                    Lloji = !IsDBNull(dbDataRow["LLOJI"])
                        ? ToInt32(dbDataRow["LLOJI"])
                        : 0;
                    EmerTabRec = dbDataRow["EMERTABELEREC"].ToString();
                    MerrDokTeModifikuar = !IsDBNull(dbDataRow["MERRDOKTEMODIFIKUAR"]) &&
                                          ToBoolean(dbDataRow["MERRDOKTEMODIFIKUAR"]);
                    MerrDokTeFshire = !IsDBNull(dbDataRow["MERRDOKTEFSHIRE"]) &&
                                      ToBoolean(dbDataRow["MERRDOKTEFSHIRE"]);
                }
                catch (InvalidCastException)
                {
                    throw new Exception("ERROR: Gabim gjate marrjes se konfig exporti  nga db-ja");
                }
            }
        }

        #endregion

        #region Internal Methods

        internal static clsKonfigExporti Create(IDataRecord dataRecord)
        {
            return new clsKonfigExporti(dataRecord);
        } 

        #endregion
    }
}