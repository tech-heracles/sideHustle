using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using AlphaWeb.Core.SharedKernel;
using DbCore.DbAdmin;
using static System.Convert;

namespace DbCore.DbImporte
{
    public class clsKonfigImporti
    {
        public enum Dokumenta { Koka = 1, Trupi = 2, Receptura = 4 }

        #region Konstruktoret

        /// <summary>
        /// Konstruktori me parametra
        /// </summary>
        /// <param name="emer">emer</param>
        /// <param name="tipi">tipi</param>
        /// <param name="idkategori">kategoria</param>
        /// <param name="formati">formati i importit</param>
        /// <param name="emersheet">emer sheet excel</param>
        /// <param name="idNder">id ndermarrjes</param>
        /// <param name="idPerd">id perdoruesit</param>
        /// <param name="idStatus">id statusit dok</param>
        /// <param name="emerTabKoka">emri i tabeles se kokes nga merren te dhenat per import</param>
        /// <param name="emerTabTrupi">emri i tabeles se trupit nga merren te dhenat per import</param>
        /// <param name="gjeneroFatPermb">gjeneron fature permbledhese gjate importit ose jo</param>
        /// <param name="emerTabRec"></param>
        /// <param name="transferoFatura"></param>
        /// <param name="merrTePaImportuara"></param>
        /// <param name="dergoMeEmail"></param>
        /// <param name="emerTabKokaHistorik"></param>
        /// <param name="emerTabTrupiHistorik"></param>
        /// <param name="emerTabRecHistorik"></param>
        /// <param name="rimerrTeImportuara"></param>
        /// <param name="nrDokumentash"></param>
        /// <param name="id">id </param>
        public clsKonfigImporti(string emer, string tipi, int idkategori, int formati, string emersheet, int idNder, int idPerd, int idStatus, string emerTabKoka, string emerTabTrupi, bool gjeneroFatPermb, string emerTabRec, bool transferoFatura, bool merrTePaImportuara, bool dergoMeEmail, string emerTabKokaHistorik, string emerTabTrupiHistorik, string emerTabRecHistorik, bool rimerrTeImportuara, int? nrDokumentash)
        {
            this.Emer = emer;
            this.Tipi = tipi;
            this.Kategoria = idkategori;
            this.Formati = formati;
            this.EmerSheet = emersheet;
            this.IdNdermarje = idNder;
            this.IdPerdoruesi = idPerd;
            this.IdStatusDok = idStatus;
            this.EmerTabKoka = emerTabKoka;
            this.EmerTabTrupi = emerTabTrupi;
            this.GjeneroFaturePermbledhese = gjeneroFatPermb;
            this.EmerTabRec = emerTabRec;
            this.TransferoFatura = transferoFatura;
            this.MerrTePaImportuara = merrTePaImportuara;
            this.DergoMeEmail = dergoMeEmail;
            this.EmerTabKokaHistorik = !string.IsNullOrEmpty(emerTabKoka) ? $"{emerTabKoka}_HISTORIK" : string.Empty;
            this.EmerTabTrupiHistorik = !string.IsNullOrEmpty(emerTabTrupi) ? $"{emerTabTrupi}_HISTORIK" : string.Empty;
            this.EmerTabRecHistorik = !string.IsNullOrEmpty(emerTabRec) ? $"{emerTabRec}_HISTORIK" : string.Empty;
            this.RimerrTeImportuara = rimerrTeImportuara;
            this.NrDokumentash = nrDokumentash;
        }

        /// <summary>
        /// Konstruktori qe kthen konfigurim e importit sipas id qe i kalohet si parameter
        /// </summary>
        /// <param name="idKoka">id </param>
        public clsKonfigImporti(int idKoka)
        {
            clsDatabaseAdmin data = new clsDatabaseAdmin();
            Mbush(data.merrKonfigurimImportiSipasID(idKoka));
            data.Dispose();
        }

        /// <summary>
        /// Konstruktori qe kthen konfigurim importi sipas kodit dhe ndermarrjes qe i kalohet si parameter
        /// </summary>
        /// <param name="kodi">kodi </param>
        /// <param name="idNdermarje">id ndermarrjes </param>
        public clsKonfigImporti(string kodi, int idNdermarje)
        {
            using (clsDatabaseAdmin data = new clsDatabaseAdmin())
            {
                Mbush(data.merrKonfigurimImportiSipasKodit(kodi, idNdermarje));
            }
        }

        /// <summary>
        /// Konstruktori default
        /// </summary>
        public clsKonfigImporti()
        {

        }

        public clsKonfigImporti(IDataRecord rreshti)
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
        /// kthen/vendos emrin e tabeles se kokes nga importohen te dhenat (per transferim te faturave)
        /// </summary>
        public string EmerTabKoka { get; set; }

        /// <summary>
        /// kthen/vendos emrin e tabeles se trupit nga importohen te dhenat (per transferim te faturave)
        /// </summary>
        public string EmerTabTrupi { get; set; }

        /// <summary>
        /// Kthen/vendos nese importi i faturave do behet permbledhes apo jo. Perdoret per dokumentat e shitjes
        /// </summary>
        public bool GjeneroFaturePermbledhese { get; set; }

        public string EmerTabRec { get; set; }

        public bool TransferoFatura { get; set; }

        public bool MerrTePaImportuara { get; set; }

        public bool DergoMeEmail { get; set; }

        public string EmerTabKokaHistorik { get; set; }

        public string EmerTabTrupiHistorik { get; set; }

        public string EmerTabRecHistorik { get; set; }

        public bool RimerrTeImportuara { get; set; }

        public int? NrDokumentash { get; set; }

        #endregion

        #region Metoda Publike

        /// <summary>
        /// Ruan konfig e importit
        /// </summary>
        /// <returns>Mesazhin qe jep info ne lidhje me ruajtjen e sukseshme, ose gabimin qe ka ndodhur nese ka te tille</returns>
        public void Ruaj(int idSuperKategori)
        {
            using (var dbAdmin = new clsDatabaseAdmin())
            using (var scope = new MyTransactionScope())
            {
                //Kontrollome nese ekziston ne DB model format importi, brenda ndermarrjes
                if (dbAdmin.ekzistonKonfigurimImportiSipasKodNdermarje(Emer, IdNdermarje))
                {
                    throw new MyException("Ekziston nje konfigurim importi me kete kod!");
                }

                Id = dbAdmin.ruajKonfigurimImportiKoka(Emer, Tipi, Kategoria, Formati, EmerSheet, IdNdermarje,
                    IdPerdoruesi, IdStatusDok, EmerTabKoka, EmerTabTrupi, GjeneroFaturePermbledhese, EmerTabRec,
                    TransferoFatura, MerrTePaImportuara, DergoMeEmail, EmerTabKokaHistorik, EmerTabTrupiHistorik,
                    EmerTabRecHistorik, RimerrTeImportuara, NrDokumentash);

                if (DergoMeEmail && !KaKonfigEmail())
                {
                    throw new MyException("Nuk ka e-mail te konfiguruar per kete template!");
                }

                if (Tipi == "SQL")
                {
                    //kontrollojme nqs ekzistojne tabelela me keto emertime
                    var mesazh = clsFunksione.kontrolloEkzistenceTabelashDheSP(idSuperKategori, EmerTabKoka,
                        EmerTabTrupi, false, Kategoria, EmerTabRec, true, EmerTabKokaHistorik, EmerTabTrupiHistorik,
                        EmerTabRecHistorik);
                    if (mesazh.Status)
                    {
                        throw new MyException(mesazh.PershkrimMesazhi);
                    }

                    KontrolloTabelaHistoriku();

                    var col = colTrupiFormatImporti.merrFormatImportiTrupiSipasIdKoka(Formati, dbAdmin);
                    //krijojme tabelen e kokes
                    KrijoTabelaImportiSipasTemplate(col, EmerTabKoka, dbAdmin, 1);
                    KrijoTabelaImportiSipasTemplate(col, EmerTabKokaHistorik, dbAdmin, 10);
                    KrijoStoredProcedurePerTabele(col, EmerTabKoka, EmerTabKokaHistorik, dbAdmin, 1);

                    if (idSuperKategori == (int)SuperKategori.Regjistrime)
                    {
                        //krijojme tabelen e trupit
                        KrijoTabelaImportiSipasTemplate(col, EmerTabTrupi, dbAdmin, 2);
                        KrijoTabelaImportiSipasTemplate(col, EmerTabTrupiHistorik, dbAdmin, 11);
                        KrijoStoredProcedurePerTabele(col, EmerTabTrupi, EmerTabTrupiHistorik, dbAdmin, 2);

                        if (Kategoria == 45)
                        {
                            //krijojme tabelen e recepturave
                            KrijoTabelaImportiSipasTemplate(col, EmerTabRec, dbAdmin, 4);
                            KrijoTabelaImportiSipasTemplate(col, EmerTabRecHistorik, dbAdmin, 12);
                            KrijoStoredProcedurePerTabele(col, EmerTabRec, EmerTabRecHistorik, dbAdmin, 4);
                        }
                    }
                }

                scope.Complete();
            }
        }

        /// <summary>
        /// Modifikon konfig importi
        /// </summary>
        /// <returns>Mesazhin qe jep info ne lidhje me modifikimin e sukseshem, ose gabimin qe ka ndodhur nese ka te tille</returns>
        public void Modifiko(clsKonfigImporti oldKonfig, int idSuperKategori)
        {
            using (var dbAdmin = new clsDatabaseAdmin())
            using (var scope = new MyTransactionScope())
            {
                dbAdmin.modifikoKonfigurimImportiKoka(Id, Emer, Tipi, Kategoria, Formati, EmerSheet, IdNdermarje, IdPerdoruesi, IdStatusDok, EmerTabKoka, EmerTabTrupi, GjeneroFaturePermbledhese, EmerTabRec, TransferoFatura, MerrTePaImportuara, DergoMeEmail, EmerTabKokaHistorik, EmerTabTrupiHistorik, EmerTabRecHistorik, RimerrTeImportuara, NrDokumentash);

                if (DergoMeEmail && !KaKonfigEmail())
                {
                    throw new MyException("Nuk ka e-mail te konfiguruar per kete template!");
                }

                if (Tipi == "SQL")
                {
                    if (oldKonfig.Formati == Formati && oldKonfig.EmerTabKoka == EmerTabKoka && oldKonfig.EmerTabTrupi == EmerTabTrupi)
                    {
                        if (oldKonfig.EmerTabKokaHistorik != EmerTabKokaHistorik ||
                            oldKonfig.EmerTabTrupiHistorik != EmerTabTrupiHistorik ||
                            oldKonfig.EmerTabRecHistorik != EmerTabRecHistorik)
                        {
                            KrijoTabelaHistoriku(dbAdmin, idSuperKategori);
                        }

                        scope.Complete();
                        return;
                    }

                    //nqs plotesohet ky kushti, nuk duhet bere gje tjeter, prandaj bejme commit transaksionin dhe return ketu.
                    if (oldKonfig.Formati != Formati && oldKonfig.EmerTabKoka == EmerTabKoka)
                    {
                        throw new MyException("Tabela e kokes ekziston, ju lutemi zgjidhni nje emertim tjeter!");
                    }

                    if (idSuperKategori == (int)SuperKategori.Regjistrime && oldKonfig.Formati != Formati && oldKonfig.EmerTabTrupi == EmerTabTrupi)
                    {
                        throw new MyException("Tabela e trupit ekziston, ju lutemi zgjidhni nje emertim tjeter!");
                    }

                    var mesazh = clsFunksione.kontrolloEkzistenceTabelashDheSP(idSuperKategori, EmerTabKoka, EmerTabTrupi, false, Kategoria, EmerTabRec, true, EmerTabKokaHistorik, EmerTabTrupiHistorik, EmerTabRecHistorik);
                    if (mesazh.Status)
                    {
                        throw new MyException(mesazh.PershkrimMesazhi);
                    }

                    KontrolloTabelaHistoriku();

                    var col = colTrupiFormatImporti.merrFormatImportiTrupiSipasIdKoka(Formati, dbAdmin);

                    if (oldKonfig.EmerTabKoka != EmerTabKoka)
                    {
                        KrijoTabelaImportiSipasTemplate(col, EmerTabKoka, dbAdmin, 1);
                        KrijoTabelaImportiSipasTemplate(col, EmerTabKokaHistorik, dbAdmin, 10);
                        KrijoStoredProcedurePerTabele(col, EmerTabKoka, EmerTabKokaHistorik, dbAdmin, 1);
                    }
                    if (idSuperKategori == (int)SuperKategori.Regjistrime)
                    {
                        if (oldKonfig.EmerTabTrupi != EmerTabTrupi)
                        {
                            KrijoTabelaImportiSipasTemplate(col, EmerTabTrupi, dbAdmin, 2);
                            KrijoTabelaImportiSipasTemplate(col, EmerTabTrupiHistorik, dbAdmin, 11);
                            KrijoStoredProcedurePerTabele(col, EmerTabTrupi, EmerTabTrupiHistorik, dbAdmin, 2);
                        }
                        if (Kategoria == 45 && oldKonfig.EmerTabRec != EmerTabRec)
                        {
                            //krijojme tabelen e recepturave
                            KrijoTabelaImportiSipasTemplate(col, EmerTabRec, dbAdmin, 4);
                            KrijoTabelaImportiSipasTemplate(col, EmerTabRecHistorik, dbAdmin, 12);
                            KrijoStoredProcedurePerTabele(col, EmerTabRec, EmerTabRecHistorik, dbAdmin, 4);
                        }
                    }
                }
                //Nqs ruhet me sukses edhe trupi kthejme mesazhin e suksesit                    
                scope.Complete();
            }
        }

        /// <summary>
        /// Fshin konfig importi
        /// </summary>
        /// <returns>Mesazhin qe jep info ne lidhje me fshirjen e sukseshme, ose gabimin qe ka ndodhur nese ka te tille</returns>
        public static void Fshi(int id, int idPerdoruesi)
        {
            using (var dbAdmin = new clsDatabaseAdmin())
            using (var scope = new MyTransactionScope())
            {

                dbAdmin.fshiKonfigurimImportiStatus(id, idPerdoruesi);

                scope.Complete();
            }
        }

        public static List<clsKonfigImporti> KtheKonfigImportiNdermarrjesSipasTeDrejtave(int idNdermarja, int idViti, int idPerdoruesi, string komponente)
        {
            using (var data = new clsDatabaseAdmin())
                return data.merrGjitheKonfigurimImportiNdermarrjesSipasTeDrejtave(idNdermarja, idViti, idPerdoruesi,
                    komponente).ToList();
        }

        public void KrijoInsertSipasTemplate(ref DataTable dataTable, int objekti, ref colTrupiFormatImporti col)
        {
            var dbAdmin = new clsDatabaseAdmin();
            col = colTrupiFormatImporti.merrFormatImportiTrupiSipasIdKoka(Formati, dbAdmin);
            var atribute = new StringBuilder();
            var parameter = new StringBuilder();
            var fusha = new StringBuilder();
            KrijoParametra(ref dataTable, objekti, col, ref atribute, ref parameter, ref fusha);
            if (!dbAdmin.ekzistonStoredProcedure($"prc_{(objekti == (int)Dokumenta.Koka ? EmerTabKoka : (objekti == (int)Dokumenta.Trupi ? EmerTabTrupi : EmerTabRec))}_ins"))
            {
                KrijoSP_InsertSipasTemplateDB(atribute, parameter, fusha, objekti, dbAdmin);
            }
        }

        #endregion

        #region Metoda Private

        private void Mbush(DataRow dbDataRow)
        {
            if (dbDataRow == null) return;
            try
            {
                Id = !IsDBNull(dbDataRow["ID"])
                    ? ToInt32(dbDataRow["ID"])
                    : 0;
                Emer = dbDataRow["EMER"].ToString();
                Tipi = dbDataRow["TIPI"].ToString();
                EmerSheet = dbDataRow["EMERSHEET"].ToString();
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
                DtKrijimit = !IsDBNull(dbDataRow["DTKRIJIMI"])
                    ? ToDateTime(dbDataRow["DTKRIJIMI"])
                    : DateTime.MinValue;
                DtModifikimit = !IsDBNull(dbDataRow["DTMODIFIKIMI"])
                    ? ToDateTime(dbDataRow["DTMODIFIKIMI"])
                    : DateTime.MinValue;
                EmerTabKoka = dbDataRow["EMERTABELEKOKA"].ToString();
                EmerTabTrupi = dbDataRow["EMERTABELETRUPI"].ToString();
                GjeneroFaturePermbledhese = !IsDBNull(dbDataRow["GJENEROFATUREPERMB"]) &&
                                            ToBoolean(dbDataRow["GJENEROFATUREPERMB"]);
                EmerTabRec = dbDataRow["EMERTABELEREC"].ToString();
                TransferoFatura = !IsDBNull(dbDataRow["TRANSFEROFATURA"]) &&
                                  ToBoolean(dbDataRow["TRANSFEROFATURA"]);
                MerrTePaImportuara = !IsDBNull(dbDataRow["MERRTEPAIMPORTUARA"]) &&
                                     ToBoolean(dbDataRow["MERRTEPAIMPORTUARA"]);
                DergoMeEmail = !IsDBNull(dbDataRow["DERGOMEEMAIL"]) &&
                               ToBoolean(dbDataRow["DERGOMEEMAIL"]);
                EmerTabKokaHistorik = dbDataRow["EMERTABELEKOKA_HISTORIK"].ToString();
                EmerTabTrupiHistorik = dbDataRow["EMERTABELETRUPI_HISTORIK"].ToString();
                EmerTabRecHistorik = dbDataRow["EMERTABELEREC_HISTORIK"].ToString();
                RimerrTeImportuara = !IsDBNull(dbDataRow["RIMERRTEIMPORTUARA"]) &&
                                     ToBoolean(dbDataRow["RIMERRTEIMPORTUARA"]);

                if (int.TryParse(dbDataRow["NRDOKUMENTASH"].ToString(), out var nrDok))
                    NrDokumentash = nrDok;
            }
            catch (InvalidCastException)
            {
                throw new Exception("ERROR: Gabim gjate marrjes se konfig importi  nga db-ja");
            }
        }

        private void Mbush(IDataRecord dbDataRow)
        {
            if (dbDataRow == null) return;
            try
            {
                Id = !IsDBNull(dbDataRow["ID"])
                    ? ToInt32(dbDataRow["ID"])
                    : 0;
                Emer = dbDataRow["EMER"].ToString();
                Tipi = dbDataRow["TIPI"].ToString();
                EmerSheet = dbDataRow["EMERSHEET"].ToString();
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
                DtKrijimit = !IsDBNull(dbDataRow["DTKRIJIMI"])
                    ? ToDateTime(dbDataRow["DTKRIJIMI"])
                    : DateTime.MinValue;
                DtModifikimit = !IsDBNull(dbDataRow["DTMODIFIKIMI"])
                    ? ToDateTime(dbDataRow["DTMODIFIKIMI"])
                    : DateTime.MinValue;
                EmerTabKoka = dbDataRow["EMERTABELEKOKA"].ToString();
                EmerTabTrupi = dbDataRow["EMERTABELETRUPI"].ToString();
                GjeneroFaturePermbledhese = !IsDBNull(dbDataRow["GJENEROFATUREPERMB"]) &&
                                            ToBoolean(dbDataRow["GJENEROFATUREPERMB"]);
                EmerTabRec = dbDataRow["EMERTABELEREC"].ToString();
                TransferoFatura = !IsDBNull(dbDataRow["TRANSFEROFATURA"]) &&
                                  ToBoolean(dbDataRow["TRANSFEROFATURA"]);
                MerrTePaImportuara = !IsDBNull(dbDataRow["MERRTEPAIMPORTUARA"]) &&
                                     ToBoolean(dbDataRow["MERRTEPAIMPORTUARA"]);
                DergoMeEmail = !IsDBNull(dbDataRow["DERGOMEEMAIL"]) &&
                               ToBoolean(dbDataRow["DERGOMEEMAIL"]);
                EmerTabKokaHistorik = dbDataRow["EMERTABELEKOKA_HISTORIK"].ToString();
                EmerTabTrupiHistorik = dbDataRow["EMERTABELETRUPI_HISTORIK"].ToString();
                EmerTabRecHistorik = dbDataRow["EMERTABELEREC_HISTORIK"].ToString();
                RimerrTeImportuara = !IsDBNull(dbDataRow["RIMERRTEIMPORTUARA"]) &&
                                     ToBoolean(dbDataRow["RIMERRTEIMPORTUARA"]);

                if (int.TryParse(dbDataRow["NRDOKUMENTASH"].ToString(), out var nrDok))
                    NrDokumentash = nrDok;
            }
            catch (InvalidCastException)
            {
                throw new Exception("ERROR: Gabim gjate marrjes se konfig importi  nga db-ja");
            }
        }

        private void KrijoTabelaHistoriku(clsDatabaseAdmin dbAdmin, int idSuperKategori)
        {
            var col = colTrupiFormatImporti.merrFormatImportiTrupiSipasIdKoka(Formati, dbAdmin);
            KontrolloTabelaHistoriku();

            KrijoTabelaImportiSipasTemplate(col, EmerTabKokaHistorik, dbAdmin, 10);
            KrijoStoredProcedurePerTabele(col, EmerTabKoka, EmerTabKokaHistorik, dbAdmin, 1);

            if (idSuperKategori == (int)SuperKategori.Regjistrime)
            {
                KrijoTabelaImportiSipasTemplate(col, EmerTabTrupiHistorik, dbAdmin, 11);
                KrijoStoredProcedurePerTabele(col, EmerTabTrupi, EmerTabTrupiHistorik, dbAdmin, 2);

                if (Kategoria == 45)
                {
                    KrijoTabelaImportiSipasTemplate(col, EmerTabRecHistorik, dbAdmin, 12);
                    KrijoStoredProcedurePerTabele(col, EmerTabRec, EmerTabRecHistorik, dbAdmin, 4);
                }
            }
        }

        private void KrijoSP_InsertSipasTemplateDB(StringBuilder parameter, StringBuilder parameterKoka, StringBuilder fushat, int kokeApoTrup, clsDatabaseAdmin dbAdmin)
        {
            var query = new StringBuilder();
            query.Append(string.Format("CREATE PROCEDURE prc_{0}_ins ( {1} ) AS BEGIN DECLARE @Err Int INSERT INTO {0} ( {2} ) VALUES ( {3} ) Set @Err = @@Error RETURN @Err END ", kokeApoTrup == (int)Dokumenta.Koka ? EmerTabKoka : EmerTabTrupi, parameterKoka, fushat, parameter));
            dbAdmin.krijoObjektSql(query);
        }

        private void KrijoParametra(ref DataTable dt, int kokeApoTrup, colTrupiFormatImporti col, ref StringBuilder parameter, ref StringBuilder parameterKoka, ref StringBuilder fushat)
        {
            foreach (var tr in col)
            {
                if (kokeApoTrup == (int)Dokumenta.Koka
                    && (tr.Visible && tr.FusheKokeApoTrupi == (int)Dokumenta.Koka || tr.FusheKokeApoTrupi == 3 || tr.KodKontrolli == "Kod Ndermarrje")
                    || kokeApoTrup == (int)Dokumenta.Trupi
                    && (tr.Visible && tr.FusheKokeApoTrupi == (int)Dokumenta.Trupi || tr.FusheKokeApoTrupi == 3 || Kategoria == 45 && tr.FusheKokeApoTrupi == 5)
                    || kokeApoTrup == (int)Dokumenta.Receptura
                    && (tr.Visible && tr.FusheKokeApoTrupi == (int)Dokumenta.Receptura || tr.FusheKokeApoTrupi == 5))
                {
                    parameterKoka.Append("@" + tr.EmerImporti.Replace(" ", "").Replace("/", "").Replace("-", "") + " " + tr.FusheType + ", ");
                    parameter.Append("@" + tr.EmerImporti.Replace(" ", "").Replace("/", "").Replace("-", "") + ", ");
                    fushat.Append("[" + tr.EmerImporti + "], ");
                    dt.Columns.Add(tr.EmerImporti);
                }
            }
            parameterKoka.Length -= 2;
            parameter.Length -= 2;
            fushat.Length -= 2;
        }

        private bool KaKonfigEmail()
        {
            using (var sharedb = new clsDatabaseAdmin())
                return sharedb.kaKonfigurimEmalPerKonfigImporti(Id);
        }

        /// <summary>
        /// Funksion qe krijon tabelat e eksportit ne databaze sipas formatit te caktuar.
        /// </summary>
        /// <param name="col"></param>
        /// <param name="emerTabele">Emri i tabeles qe do krijohet</param>
        /// <param name="dbAdmin"></param>
        /// <param name="kokeApoTrup">Perdoret per te percaktuar nese do krijohet tabele koke, apo trupi.</param>
        /// <returns></returns>
        private void KrijoTabelaImportiSipasTemplate(colTrupiFormatImporti col, string emerTabele, clsDatabaseAdmin dbAdmin, int kokeApoTrup)
        {
            var query = new StringBuilder();
            query.Append($"CREATE TABLE [{emerTabele}] ( ");

            var eshteTabHistoriku = kokeApoTrup == 10;

            switch (kokeApoTrup)
            {
                case 1:
                    query.Append($"IDIMPORTSHITJE INT IDENTITY, DTIMPORTI DATETIME CONSTRAINT DefValDTIMPORTI{emerTabele} DEFAULT getdate(), ");
                    break;
                case 2:
                    query.Append("IDIMPORTTRUPISHITJE INT IDENTITY, ");
                    break;
                case 4:
                    query.Append("IDIMPORTRECEPTURA INT IDENTITY, ");
                    break;
                case 10:
                    query.Append($"IDIMPORTSHITJEHISTORIK INT IDENTITY PRIMARY KEY, IDIMPORTSHITJE INT, DTIMPORTI DATETIME, DTMODIFIKIMI DATETIME CONSTRAINT DefValDTMODIFIKIMI{emerTabele} DEFAULT getdate(), ");
                    kokeApoTrup = 1;
                    break;
                case 11:
                    query.Append($"IDIMPORTTRUPISHITJEHISTORIK INT IDENTITY PRIMARY KEY, IDIMPORTTRUPISHITJE INT, DTMODIFIKIMI DATETIME CONSTRAINT DefValDTMODIFIKIMI{emerTabele} DEFAULT getdate(), ");
                    kokeApoTrup = 2;
                    break;
                case 12:
                    query.Append($"IDIMPORTRECEPTURAHISTORIK INT IDENTITY PRIMARY KEY, IDIMPORTRECEPTURA INT, DTMODIFIKIMI DATETIME CONSTRAINT DefValDTMODIFIKIMI{emerTabele} DEFAULT getdate(), ");
                    kokeApoTrup = 4;
                    break;
            }

            var colFushat = colTrupiFormatImporti.merrFormatImportiTrupiSipasIdKoka(Formati, dbAdmin);
            foreach (var tr in col)
            {
                if ((kokeApoTrup == 1 && ((tr.Visible && tr.FusheKokeApoTrupi == 1) || tr.FusheKokeApoTrupi == 3 || tr.KodKontrolli == "Kod Ndermarrje"))
                    || (kokeApoTrup == 2 && ((tr.Visible && tr.FusheKokeApoTrupi == 2) || tr.FusheKokeApoTrupi == 3 || (Kategoria == 45 && tr.FusheKokeApoTrupi == 5)))
                    || (kokeApoTrup == 4 && (tr.Visible && tr.FusheKokeApoTrupi == 4 || tr.FusheKokeApoTrupi == 5)))
                {
                    colFushat.Add(tr);
                    query.Append($"[{tr.EmerImporti}] {tr.FusheType}");
                    if (tr.FusheKokeApoTrupi == 3 || tr.KodKontrolli == "Kod Ndermarrje" || tr.FusheKokeApoTrupi == 5)
                        query.Append(" NOT NULL ");
                    query.Append(", ");
                    if (!eshteTabHistoriku && kokeApoTrup == 1 && tr.FusheKokeApoTrupi == 3)
                        query.Append($" CONSTRAINT Unique_{tr.EmerImporti.Replace(" ", string.Empty)}{emerTabele} UNIQUE([{tr.EmerImporti}]),");
                }
            }

            if (kokeApoTrup == 1)
                query.Append($"STATUSIMPORTI int CONSTRAINT DefValSTATUSIMPORTI{emerTabele} DEFAULT 0, DTLEXIMI DATETIME, ");

            if (colFushat.Count > 1)
            {
                query.Length -= 2;
            }

            query.Append(")");
            dbAdmin.krijoObjektSql(query);
        }

        private void KrijoStoredProcedurePerTabele(colTrupiFormatImporti col, string emerTabele, string emerTabeleHistoriku, clsDatabaseAdmin dbAdmin, int kokeApoTrup)
        {
            KrijoStoredProcedurePerTabUpdate(col, emerTabele, emerTabeleHistoriku, dbAdmin, kokeApoTrup);
            KrijoStoredProcedurePerTabEksportiInsert(col, emerTabeleHistoriku, dbAdmin, kokeApoTrup);
            KrijoStoredProcedurePerTabDelete(col, emerTabele, emerTabeleHistoriku, dbAdmin, kokeApoTrup);
        }

        private void KontrolloTabelaHistoriku()
        {
            using (var moduliImporte = new clsDatabazeImporte())
            {
                if (!string.IsNullOrEmpty(EmerTabKokaHistorik))
                    EmerTabKokaHistorik = clsFunksione.ekzistonTabeleHistoriku(moduliImporte, EmerTabKokaHistorik, 0);
                if (!string.IsNullOrEmpty(EmerTabTrupiHistorik))
                    EmerTabTrupiHistorik = clsFunksione.ekzistonTabeleHistoriku(moduliImporte, EmerTabTrupiHistorik, 0);
                if (!string.IsNullOrEmpty(EmerTabRecHistorik))
                    EmerTabRecHistorik = clsFunksione.ekzistonTabeleHistoriku(moduliImporte, EmerTabRecHistorik, 0);
            }
        }

        private void KrijoStoredProcedurePerTabUpdate(colTrupiFormatImporti col, string emerTabele, string emerTabeleHistoriku, clsDatabaseAdmin dbAdmin, int kokeApoTrup)
        {
            string primaryKey = string.Empty;
            switch (kokeApoTrup)
            {
                case 1:
                    primaryKey = "IDIMPORTSHITJE";
                    break;
                case 2:
                    primaryKey = "IDIMPORTTRUPISHITJE";
                    break;
                case 4:
                    primaryKey = "IDIMPORTRECEPTURA";
                    break;
            }
            StringBuilder query = new StringBuilder();
            query.Append("CREATE PROCEDURE prc_" + emerTabele + "_upd");
            query.Append(" ( ");
            query.Append("@" + primaryKey + " NUMERIC(18, 0), ");
            query.Append(kokeApoTrup == 1 ? "@DTLEXIMI datetime, " : "");
            query.Append(kokeApoTrup == 1 ? "@DTIMPORTI datetime, " : "");
            foreach (clsTrupiFormatImporti tr in col)
            {
                if ((kokeApoTrup == 1 && ((tr.Visible && tr.FusheKokeApoTrupi == 1) || tr.FusheKokeApoTrupi == 3 || tr.KodKontrolli == "Kod Ndermarrje"))
                    || (kokeApoTrup == 2 && ((tr.Visible && tr.FusheKokeApoTrupi == 2) || tr.FusheKokeApoTrupi == 3 || (Kategoria == 45 && tr.FusheKokeApoTrupi == 5)))
                    || (kokeApoTrup == 4 && (tr.Visible && tr.FusheKokeApoTrupi == 4 || tr.FusheKokeApoTrupi == 5)))
                {
                    string emerImporti = tr.EmerImporti;
                    query.Append("@");
                    query.Append(emerImporti.Replace(" ", "").Replace("/", "").Replace("-", ""));
                    query.Append(" ");
                    query.Append(tr.FusheType);
                    query.Append(", ");
                }
            }
            query.Length -= 2;
            query.Append(" ) ");
            query.Append(" AS BEGIN ");
            query.Append(" DECLARE @Err Int	");

            foreach (clsTrupiFormatImporti tr in col)
            {
                if ((kokeApoTrup == 1 && ((tr.Visible && tr.FusheKokeApoTrupi == 1) || tr.FusheKokeApoTrupi == 3 || tr.KodKontrolli == "Kod Ndermarrje"))
                    || (kokeApoTrup == 2 && ((tr.Visible && tr.FusheKokeApoTrupi == 2) || tr.FusheKokeApoTrupi == 3 || (Kategoria == 45 && tr.FusheKokeApoTrupi == 5)))
                    || (kokeApoTrup == 4 && ((tr.Visible && tr.FusheKokeApoTrupi == 4) || tr.FusheKokeApoTrupi == 5)))
                {
                    string emerImporti = tr.EmerImporti;
                    query.Append("DECLARE @");
                    query.Append(emerImporti.Replace(" ", "").Replace("/", "").Replace("-", ""));
                    query.Append("_temp ");
                    query.Append(tr.FusheType);
                    query.Append($" = (SELECT [{emerImporti}] FROM {emerTabele} WHERE {primaryKey} = @{primaryKey})");
                    query.Append(" ;");
                }
            }

            query.Append(" UPDATE [" + emerTabele + "]");
            query.Append(" SET ");
            foreach (clsTrupiFormatImporti tr in col)
            {
                if ((kokeApoTrup == 1 && ((tr.Visible && tr.FusheKokeApoTrupi == 1) || tr.FusheKokeApoTrupi == 3 || tr.KodKontrolli == "Kod Ndermarrje"))
                    || (kokeApoTrup == 2 && ((tr.Visible && tr.FusheKokeApoTrupi == 2) || tr.FusheKokeApoTrupi == 3 || (Kategoria == 45 && tr.FusheKokeApoTrupi == 5)))
                    || (kokeApoTrup == 4 && ((tr.Visible && tr.FusheKokeApoTrupi == 4) || tr.FusheKokeApoTrupi == 5)))
                {
                    string emerImporti = tr.EmerImporti;
                    query.Append("[");
                    query.Append(tr.EmerImporti);
                    query.Append("] = ");
                    query.Append("@");
                    query.Append(emerImporti.Replace(" ", "").Replace("/", "").Replace("-", ""));
                    query.Append(" ");
                    query.Append(", ");
                }
            }
            query.Length -= 2;
            query.Append($" where [{primaryKey}]= @{primaryKey.Replace(" ", "").Replace("/", "").Replace("-", "")} ");

            query.Append($"exec prc_{emerTabeleHistoriku}_ins ");
            query.Append("@" + primaryKey + " = @" + primaryKey + ", ");
            query.Append(kokeApoTrup == 1 ? "@DTLEXIMI = @DTLEXIMI, " : "");
            query.Append(kokeApoTrup == 1 ? "@DTIMPORTI = @DTIMPORTI, " : "");
            foreach (clsTrupiFormatImporti tr in col)
            {
                if ((kokeApoTrup == 1 && ((tr.Visible && tr.FusheKokeApoTrupi == 1) || tr.FusheKokeApoTrupi == 3 || tr.KodKontrolli == "Kod Ndermarrje"))
                    || (kokeApoTrup == 2 && ((tr.Visible && tr.FusheKokeApoTrupi == 2) || tr.FusheKokeApoTrupi == 3 || (Kategoria == 45 && tr.FusheKokeApoTrupi == 5)))
                    || (kokeApoTrup == 4 && ((tr.Visible && tr.FusheKokeApoTrupi == 4) || tr.FusheKokeApoTrupi == 5)))
                {
                    string emerImporti = tr.EmerImporti;
                    query.Append("@");
                    query.Append(emerImporti.Replace(" ", "").Replace("/", "").Replace("-", ""));
                    query.Append(" = ");
                    query.Append("@");
                    query.Append(emerImporti.Replace(" ", "").Replace("/", "").Replace("-", ""));
                    query.Append("_temp");
                    query.Append(" ");
                    query.Append(", ");
                }
            }
            query.Length -= 2;

            query.Append(" Set @Err = @@Error RETURN @Err  END ");

            dbAdmin.krijoObjektSql(query);
        }

        private void KrijoStoredProcedurePerTabEksportiInsert(colTrupiFormatImporti col, string emerTabele, clsDatabaseAdmin dbAdmin, int kokeApoTrup)
        {
            string primaryKey = string.Empty;
            switch (kokeApoTrup)
            {
                case 1:
                    primaryKey = "IDIMPORTSHITJE";
                    break;
                case 2:
                    primaryKey = "IDIMPORTTRUPISHITJE";
                    break;
                case 4:
                    primaryKey = "IDIMPORTRECEPTURA";
                    break;
            }

            StringBuilder query = new StringBuilder();
            clsTrupiFormatImporti idkoka = new clsTrupiFormatImporti();
            query.Append("CREATE PROCEDURE prc_" + emerTabele + "_ins");
            query.Append(" ( ");
            query.Append("@" + primaryKey + " NUMERIC(18, 0), ");
            query.Append(kokeApoTrup == 1 ? "@DTLEXIMI datetime, " : "");
            query.Append(kokeApoTrup == 1 ? "@DTIMPORTI datetime, " : "");
            foreach (clsTrupiFormatImporti tr in col)
            {
                if ((kokeApoTrup == 1 && ((tr.Visible && tr.FusheKokeApoTrupi == 1) || tr.FusheKokeApoTrupi == 3 || tr.KodKontrolli == "Kod Ndermarrje"))
                    || (kokeApoTrup == 2 && ((tr.Visible && tr.FusheKokeApoTrupi == 2) || tr.FusheKokeApoTrupi == 3 || (Kategoria == 45 && tr.FusheKokeApoTrupi == 5)))
                    || (kokeApoTrup == 4 && ((tr.Visible && tr.FusheKokeApoTrupi == 4) || tr.FusheKokeApoTrupi == 5)))
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
            query.Append("[" + primaryKey + "], ");
            query.Append(kokeApoTrup == 1 ? "[DTLEXIMI], " : "");
            query.Append(kokeApoTrup == 1 ? "[DTIMPORTI], " : "");
            foreach (clsTrupiFormatImporti tr in col)
            {
                if ((kokeApoTrup == 1 && ((tr.Visible && tr.FusheKokeApoTrupi == 1) || tr.FusheKokeApoTrupi == 3 || tr.KodKontrolli == "Kod Ndermarrje"))
                    || (kokeApoTrup == 2 && ((tr.Visible && tr.FusheKokeApoTrupi == 2) || tr.FusheKokeApoTrupi == 3 || (Kategoria == 45 && tr.FusheKokeApoTrupi == 5)))
                    || (kokeApoTrup == 4 && ((tr.Visible && tr.FusheKokeApoTrupi == 4) || tr.FusheKokeApoTrupi == 5)))
                {
                    query.Append("[");
                    query.Append(tr.EmerImporti);
                    query.Append("], ");
                }
            }
            query.Length -= 2;
            query.Append(" )  Select   ");
            query.Append("@");
            query.Append(primaryKey.Replace(" ", "").Replace("/", "").Replace("-", ""));
            query.Append(", ");
            query.Append(kokeApoTrup == 1 ? "@DTLEXIMI, " : "");
            query.Append(kokeApoTrup == 1 ? "@DTIMPORTI, " : "");
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
            query.Append(" Set @Err = @@Error RETURN @Err  END ");
            dbAdmin.krijoObjektSql(query);
        }

        private void KrijoStoredProcedurePerTabDelete(colTrupiFormatImporti col, string emerTabele, string emerTabeleHistoriku, clsDatabaseAdmin dbAdmin, int kokeApoTrup)
        {
            string primaryKey = string.Empty;
            switch (kokeApoTrup)
            {
                case 1:
                    primaryKey = "IDIMPORTSHITJE";
                    break;
                case 2:
                    primaryKey = "IDIMPORTTRUPISHITJE";
                    break;
                case 4:
                    primaryKey = "IDIMPORTRECEPTURA";
                    break;
            }
            StringBuilder query = new StringBuilder();
            query.Append("CREATE PROCEDURE prc_" + emerTabele + "_del");
            query.Append(" ( ");
            query.Append("@" + primaryKey + " NUMERIC(18, 0), ");
            query.Append(kokeApoTrup == 1 ? "@DTLEXIMI datetime, " : "");
            query.Append(kokeApoTrup == 1 ? "@DTIMPORTI datetime, " : "");
            foreach (clsTrupiFormatImporti tr in col)
            {
                if ((kokeApoTrup == 1 && ((tr.Visible && tr.FusheKokeApoTrupi == 1) || tr.FusheKokeApoTrupi == 3 || tr.KodKontrolli == "Kod Ndermarrje"))
                    || (kokeApoTrup == 2 && ((tr.Visible && tr.FusheKokeApoTrupi == 2) || tr.FusheKokeApoTrupi == 3 || (Kategoria == 45 && tr.FusheKokeApoTrupi == 5)))
                    || (kokeApoTrup == 4 && (tr.Visible && tr.FusheKokeApoTrupi == 4 || tr.FusheKokeApoTrupi == 5)))
                {
                    string emerImporti = tr.EmerImporti;
                    query.Append("@");
                    query.Append(emerImporti.Replace(" ", "").Replace("/", "").Replace("-", ""));
                    query.Append(" ");
                    query.Append(tr.FusheType);
                    query.Append(", ");
                }
            }
            query.Length -= 2;
            query.Append(" ) ");
            query.Append(" AS BEGIN ");
            query.Append(" DECLARE @Err Int	");

            query.Append($"exec prc_{emerTabeleHistoriku}_ins ");
            query.Append("@" + primaryKey + " = @" + primaryKey + ", ");
            query.Append(kokeApoTrup == 1 ? "@DTLEXIMI = @DTLEXIMI, " : "");
            query.Append(kokeApoTrup == 1 ? "@DTIMPORTI = @DTIMPORTI, " : "");
            foreach (clsTrupiFormatImporti tr in col)
            {
                if ((kokeApoTrup == 1 && ((tr.Visible && tr.FusheKokeApoTrupi == 1) || tr.FusheKokeApoTrupi == 3 || tr.KodKontrolli == "Kod Ndermarrje"))
                    || (kokeApoTrup == 2 && ((tr.Visible && tr.FusheKokeApoTrupi == 2) || tr.FusheKokeApoTrupi == 3 || (Kategoria == 45 && tr.FusheKokeApoTrupi == 5)))
                    || (kokeApoTrup == 4 && ((tr.Visible && tr.FusheKokeApoTrupi == 4) || tr.FusheKokeApoTrupi == 5)))
                {
                    string emerImporti = tr.EmerImporti;
                    query.Append("@");
                    query.Append(emerImporti.Replace(" ", "").Replace("/", "").Replace("-", ""));
                    query.Append(" = ");
                    query.Append("@");
                    query.Append(emerImporti.Replace(" ", "").Replace("/", "").Replace("-", ""));
                    query.Append(" ");
                    query.Append(", ");
                }
            }
            query.Length -= 2;

            query.Append(" DELETE FROM [" + emerTabele + "]");
            query.Append($" where [{primaryKey}]= @{primaryKey.Replace(" ", "").Replace("/", "").Replace("-", "")} ");

            query.Append(" Set @Err = @@Error RETURN @Err  END ");
            dbAdmin.krijoObjektSql(query);
        }

        #endregion

        #region Metoda Internal

        internal static clsKonfigImporti Create(IDataRecord dataRecord)
        {
            return new clsKonfigImporti(dataRecord);
        }

        #endregion
    }
}