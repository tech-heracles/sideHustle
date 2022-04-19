using DbCore.DbAdmin;
using DbCore.DbInventari;
using DbCore.DbKontabiliteti;
using DbCore.DbRegjistrim;
using DbCore.DbShare;
using DbCore.IMBUtils.DataBase;
using DbCore.IMBUtils.Messages;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Resources;

namespace DbCore.DbAsete
{
    /// <summary>
    /// MODULI ASETE:
    /// Klase e ndertuar per te mbajtur objektin e kokes se dokumentit te amotizimit.
    /// Te dhenat merret nga tabela T_ASETE_AMORTIZIMI_KOKA.
    /// </summary>
    public class clsAmortizimiKoka : ICloneable
    {
        #region Atribute

        private int idAmortizimi;
        private string nrDok;
        private int idNiveli;
        private int idKonfigurimAmbjenti;
        private DateTime dateDokumenti;
        private DateTime dateAmortizimi;
        private DateTime dateRegjistrimi;
        private int idNjesiAdministrative;
        private string shenime;
        private double amortizimiShteseTotal;
        private int idNderViti;
        private int idLlojStandarti;
        private int idStatusDokumenti;
        private int idNdermarrje;
        private int idPerdoruesi;
        private int idKrijuesi;
        private DateTime dtKrijimi;
        private DateTime dtModifikimi;
        private int idDokGjenerues;
        private int idNivelGjenerues;
        private int idKonfigGjenerues;
        private int idDokNga;
        private int idLlogKunderparti;
        private int nrRenditje;
        private colAmortizimiTrupiAbstract colTrupi;
        private colAmortizimiTrupiAbstract colTrupiRezerva;
        private clsKokaFleteKontabel oFleteKontabel;
        private clsKokaMagazina oKokaMagazina;
        private clsSerialPerRivleresimKoka rivleresimKoka;
        private DataRow rreshti;

        #endregion

        #region Properties

        /// <summary>
        /// MODULI ASETE:
        /// (int) Merr ose jep vlere id automatike te kokes se dokumentit te amortizimit.
        /// </summary>
        public int IdAmortizimi
        {
            get { return idAmortizimi; }
            set { idAmortizimi = value; }
        }

        /// <summary>
        /// MODULI ASETE:
        /// (string) Merr ose jep vlere numrit te dokumentit te amortizimit.
        /// </summary>
        public string NrDok
        {
            get { return nrDok; }
            set { nrDok = value; }
        }

        /// <summary>
        /// MODULI ASETE:
        /// (int) Merr ose jep vlere id se nenkategorise qe do te perdoret per fletet e amortizimit FA.
        /// </summary>
        public int IdNiveli
        {
            get { return idNiveli; }
            set { idNiveli = value; }
        }

        /// <summary>
        /// MODULI ASETE:
        /// (int) Merr ose jep vlere id se llojit te dokumentit te perdorur ne dokumentin e amotizimit.
        /// </summary>
        public int IdKonfigurimAmbjenti
        {
            get { return idKonfigurimAmbjenti; }
            set { idKonfigurimAmbjenti = value; }
        }

        /// <summary>
        /// MODULI ASETE:
        /// (DateTime) Merr ose jep vlere date se dokumentit te amortizimit.
        /// </summary>
        public DateTime DateDokumenti
        {
            get { return dateDokumenti; }
            set { dateDokumenti = value; }
        }

        /// <summary>
        /// MODULI ASETE:
        /// (DateTime) Merr ose jep vlere dates se kur eshte data e llogaritjes se amortizimit per dokumentin e amortizimit.
        /// </summary>
        public DateTime DateAmortizimi
        {
            get { return dateAmortizimi; }
            set { dateAmortizimi = value; }
        }

        /// <summary>
        /// MODULI ASETE:
        /// (DateTime) Merr ose jep vlere dates kur po behet regjistrimi i dokumentit te amortizimit.
        /// </summary>
        public DateTime DateRegjistrimi
        {
            get { return dateRegjistrimi; }
            set { dateRegjistrimi = value; }
        }

        /// <summary>
        /// MODULI ASETE:
        /// (int) Merr ose jep vlere id se njesi administrative ku po behet dokumenti i amortizimit.
        /// </summary>
        public int IdNjesiAdministrative
        {
            get { return idNjesiAdministrative; }
            set { idNjesiAdministrative = value; }
        }

        /// <summary>
        /// MODULI ASETE:
        /// (clsKokaFleteKontabel) Merr ose jep fleten kontabel nese eshte e gjeneruar.
        /// </summary>
        public clsKokaFleteKontabel OFleteKontabel
        {
            get
            {
                return oFleteKontabel;
            }
            set
            {
                oFleteKontabel = value;
            }
        }

        /// <summary>
        /// koka e dokumentit te magazines qe gjenerohet nga rivleresimi
        /// </summary>
        public clsKokaMagazina OKokaMagazina
        {
            get
            {
                return oKokaMagazina;
            }
            set
            {
                oKokaMagazina = value;
            }
        }

        /// <summary>
        /// koka e dokumentit te rivleresimit qe gjeneron amortizimin
        /// </summary>
        public clsSerialPerRivleresimKoka RivleresimKoka
        {
            get
            {
                return rivleresimKoka;
            }
            set
            {
                rivleresimKoka = value;
            }
        }

        /// <summary>
        /// MODULI ASETE:
        /// (string) Merr ose jep vlere shenimeve te ndryshme qe mund te vendosen ne dokumentin e amortizimit.
        /// </summary>
        public string Shenime
        {
            get { return shenime; }
            set { shenime = value; }
        }

        /// <summary>
        /// MODULI ASETE:
        /// (double) Merr ose jep vlere totalit te dokumentit te amortizimit ne lidhje me amortizimin shtese te llogaritur.
        /// </summary>
        public double AmortizimiShteseTotal
        {
            get { return amortizimiShteseTotal; }
            set { amortizimiShteseTotal = value; }
        }

        /// <summary>
        /// MODULI ASETE:
        /// (int) Merr ose jep vlere id se lidhjes se ndermarrjes me nje vit kalendarik te caktuar.
        /// </summary>
        public int IdNderViti
        {
            get { return idNderViti; }
            set { idNderViti = value; }
        }

        /// <summary>
        /// MODULI ASETE:
        /// (int) Merr ose jep vlere id se standartit te perdorur ne dokumentin e amortizimit.
        /// </summary>
        public int IdLlojStandarti
        {
            get { return idLlojStandarti; }
            set { idLlojStandarti = value; }
        }

        /// <summary>
        /// MODULI ASETE:
        /// (int) Merr ose jep vlere se ne cfare gjendje eshte dokumenti i amortizimit, i ruajtur, fshire, apo modifikuar.
        /// </summary>
        public int IdStatusDokumenti
        {
            get { return idStatusDokumenti; }
            set { idStatusDokumenti = value; }
        }

        /// <summary>
        /// MODULI ASETE:
        /// (int) Merr ose jep vlere id se ndermarrjes ne te cilen eshte dokumenti i amoritizimit.
        /// </summary>
        public int IdNdermarrje
        {
            get { return idNdermarrje; }
            set { idNdermarrje = value; }
        }

        /// <summary>
        /// MODULI ASETE:
        /// (DateTime) Merr ose jep vlere dates se krijimit te dokumentit te amortizimit.
        /// </summary>
        public DateTime DtKrijimi
        {
            get { return dtKrijimi; }
            set { dtKrijimi = value; }
        }

        /// <summary>
        /// MODULI ASETE:
        /// (DateTime) Merr ose jep vlere dates se modifikimit te dokumentit te amortizimit.
        /// </summary>
        public DateTime DtModifikimi
        {
            get { return dtModifikimi; }
            set { dtModifikimi = value; }
        }

        /// <summary>
        /// MODULI ASETE:
        /// (int) Merr ose jep vlere id se perdoruesit qe modifikon dokumentin e amortizimit.
        /// </summary>
        public int IdPerdoruesi
        {
            get { return idPerdoruesi; }
            set { idPerdoruesi = value; }
        }

        /// <summary>
        /// MODULI ASETE:
        /// (int) Merr ose jep vlere id se krijuesit qe e krijon per here te pare dokumentin e amortizimit.
        /// </summary>
        public int IdKrijuesi
        {
            get { return idKrijuesi; }
            set { idKrijuesi = value; }
        }

        /// <summary>
        /// MODULI ASETE:
        /// (int) Merr ose jep vlere id se dokumentit nga po gjenerohet dokumenti i amortizimit.
        /// </summary>
        public int IdDokGjenerues
        {
            get { return idDokGjenerues; }
            set { idDokGjenerues = value; }
        }

        /// <summary>
        /// MODULI ASETE:
        /// (int) Merr ose jep vlere id se nenkategorise te dokumentit qe e ka gjeneruar dokumentin e amortizimit.
        /// </summary>
        public int IdNivelGjenerues
        {
            get { return idNivelGjenerues; }
            set { idNivelGjenerues = value; }
        }

        /// <summary>
        /// MODULI ASETE:
        /// (int) Merr ose jep vlere id se llojit te dokumentit qe ka gjeneruar dokumentin e amortizimit.
        /// </summary>
        public int IdKonfigGjenerues
        {
            get { return idKonfigGjenerues; }
            set { idKonfigGjenerues = value; }
        }

        /// <summary>
        /// MODULI ASETE:
        /// (int) Merr ose jep vlere id se dokumentit nga vjen dokumenti i amortizimit.
        /// </summary>
        public int IdDokNga
        {
            get { return idDokNga; }
            set { idDokNga = value; }
        }

        /// <summary>
        /// MODULI ASETE:
        /// (int) Merr ose jep vlere id se llogarise kunderparti.
        /// </summary>
        public int IdLlogKunderparti
        {
            get { return idLlogKunderparti; }
            set { idLlogKunderparti = value; }
        }

        /// <summary>
        /// MODULI ASETE:
        /// (int) Merr ose jep vlere id se nr te renditjes.
        /// </summary>
        public int NrRenditje
        {
            get { return nrRenditje; }
            set { nrRenditje = value; }
        }

        /// <summary>
        /// MODULI ASETE:
        /// (colAmortizimiTrupi) Merr ose jep vlere trupit te dokumentit te amortizimit.
        /// </summary>
        public colAmortizimiTrupiAbstract ColTrupi
        {
            get { return colTrupi; }
            set { colTrupi = value; }
        }

        public colAmortizimiTrupiAbstract ColTrupiRezerva
        {
            get
            {
                return colTrupiRezerva;
            }

            set
            {
                colTrupiRezerva = value;
            }
        }

        #endregion

        #region Konstruktori

        /// <summary>
        /// MODULI ASETE:
        /// Krijon objektin bosh te klases clsAmortizimiKoka per koken e dokumentit te amortizimit.
        /// </summary>
        public clsAmortizimiKoka()
        {
            colTrupi = new colAmortizimiTrupi();
            oFleteKontabel = new clsKokaFleteKontabel();
            oKokaMagazina = new clsKokaMagazina();
            colTrupiRezerva = new colAmortizimiTrupiRezerva();
        }

        /// <summary>
        /// MODULI ASETE:
        /// Krijon objektin e klases clsAmortizimiKoka per id e kokes se dhene.
        /// </summary>
        /// <param name="id">(int) Id e kokes se amortizimit.</param>
        public clsAmortizimiKoka(int id)
        {
            clsDatabazeAsete moduliAsete = new clsDatabazeAsete();
            mbushAmortizimKokaObjekt(moduliAsete.ktheAmortizimKokaSipasId(id));
            moduliAsete.Dispose();
            colTrupi = new colAmortizimiTrupi();
            oFleteKontabel = new clsKokaFleteKontabel();
            oKokaMagazina = new clsKokaMagazina();
            colTrupiRezerva = new colAmortizimiTrupiRezerva();
        }

        public clsAmortizimiKoka(DataRow rreshti)
        {
            
            mbushAmortizimKokaObjekt(rreshti);
            colTrupi = new colAmortizimiTrupi();
            OFleteKontabel = new clsKokaFleteKontabel();
            colTrupiRezerva = new colAmortizimiTrupiRezerva();
        }

        #endregion

        #region Metoda Internal

        /// <summary>
        /// MODULI ASETE:
        /// Metoda perdoret per te mbushur te gjitha atributet e objektit clsAmortizimiKoka sipas te dhenave qe vijne nga cdo rresht i databazes nga tabela T_ASETE_AMORTIZIMI_KOKA.
        /// </summary>
        /// <param name="dbDataRowAmortizimKoka">(DataRow) Merr si parameter vetem nje rresht te kthyer nga tabelat ne databaze per te mbushur nje objekt.</param>
        /// <returns>(bool) Kthen true nese nuk ndodh asnje gabim gjate leximit te te dhenave, ne te kundert false.</returns>
        internal bool mbushAmortizimKokaObjekt(DataRow dbDataRowAmortizimKoka)
        {
            if (dbDataRowAmortizimKoka == null)
                return false;
            try
            {
                int.TryParse(dbDataRowAmortizimKoka["ID_AMORTIZIMI"].ToString(), out idAmortizimi);
                nrDok = dbDataRowAmortizimKoka["NR_DOK"].ToString();
                int.TryParse(dbDataRowAmortizimKoka["IDNIVELI"].ToString(), out idNiveli);
                int.TryParse(dbDataRowAmortizimKoka["IDKONFIGURIMAMBJENTI"].ToString(), out idKonfigurimAmbjenti);
                DateTime.TryParse(dbDataRowAmortizimKoka["DATE_DOKUMENTI"].ToString(), out dateDokumenti);
                DateTime.TryParse(dbDataRowAmortizimKoka["DATE_AMORTIZIMI"].ToString(), out dateAmortizimi);
                DateTime.TryParse(dbDataRowAmortizimKoka["DATE_REGJISTRIMI"].ToString(), out dateRegjistrimi);
                int.TryParse(dbDataRowAmortizimKoka["IDNJESIADMINISTRATIVE"].ToString(), out idNjesiAdministrative);
                shenime = dbDataRowAmortizimKoka["SHENIME"].ToString();
                double.TryParse(dbDataRowAmortizimKoka["AMORTIZIMISHTESE_TOTAL"].ToString(), out amortizimiShteseTotal);
                int.TryParse(dbDataRowAmortizimKoka["IDNDERVITI"].ToString(), out idNderViti);
                int.TryParse(dbDataRowAmortizimKoka["IDLLOJSTANDARTI"].ToString(), out idLlojStandarti);
                int.TryParse(dbDataRowAmortizimKoka["IDSTATUSDOK"].ToString(), out idStatusDokumenti);
                int.TryParse(dbDataRowAmortizimKoka["IDNDERMARJE"].ToString(), out idNdermarrje);
                int.TryParse(dbDataRowAmortizimKoka["IDPERDORUESI"].ToString(), out idPerdoruesi);
                int.TryParse(dbDataRowAmortizimKoka["IDKRIJUESI"].ToString(), out idKrijuesi);
                DateTime.TryParse(dbDataRowAmortizimKoka["DTKRIJIMI"].ToString(), out dtKrijimi);
                DateTime.TryParse(dbDataRowAmortizimKoka["DTMODIFIKIMI"].ToString(), out dtModifikimi);
                int.TryParse(dbDataRowAmortizimKoka["IDGJENERUES"].ToString(), out idDokGjenerues);
                int.TryParse(dbDataRowAmortizimKoka["IDNIVELGJENERUES"].ToString(), out idNivelGjenerues);
                int.TryParse(dbDataRowAmortizimKoka["IDKONFIGGJENERUES"].ToString(), out idKonfigGjenerues);
                int.TryParse(dbDataRowAmortizimKoka["IDDOKNGA"].ToString(), out idDokNga);
                int.TryParse(dbDataRowAmortizimKoka["ID_LLOGKUNDERPARTI"].ToString(), out idLlogKunderparti);
                int.TryParse(dbDataRowAmortizimKoka["NR_RENDITJE"].ToString(), out nrRenditje);
                return true;
            }
            catch (InvalidCastException)
            {
                throw new Exception("ERROR: Gabim gjate marrjes se kokes se dokumentit te amortizimit nga db-ja");
            }
        }

        #endregion

        #region Metoda Publike

        /// <summary>
        /// MODULI ASETE:
        /// Ruan objektin e kokes se amortizimit.
        /// </summary>
        /// <param name="moduliAsete">(clsDatabazeAsete) Merr objektin e transaksionit qe te lidhi veprimet me njera tjetren.</param>
        /// <param name="meKontabilizim">(int) Nese ruajtja ka kontabilizim apo jo.</param>
        /// <param name="idDokNgaFK">(int) Merr id e dokumentit nga fleta kontabel.</param>
        /// <param name="shfaqmesazhapolupe">(string) Mesazhin qe duhet te shfaqe.</param>
        /// <param name="iddokngaqk">(int) Merr id e dokumentit nga qendra e kostos.</param>
        /// <param name="trupivjeterqendra">(DbQendraKosto.colTrupiQendraKosto) Merr id e dokumentit te qendres se kostos.</param>
        /// <param name="idPeriudha">(int) Merr id e periudhes.</param>
        /// <param name="idkategoria">(int) Merr id e kategorise.</param>
        /// <param name="ngarivleresimi"> tregon nqs po ruhet nje dokument rivleresimi</param>
        /// <param name="seriale">koleksioni i serialeve te zgjedhura</param>
        /// <returns>Kthen clsMesazh me atribut statusi True nese ruajtja kryhet me sukses ose False dhe mesazhin e gabimit ne te kundert.</returns>
        public clsMesazh ruaj(int meKontabilizim, int idDokNgaFK, out string shfaqmesazhapolupe, int iddokngaqk, DbQendraKosto.colTrupiQendraKosto trupivjeterqendra, int idPeriudha, int idkategoria, colSerialetMagazine seriale, bool ngarivleresimi, IDictionary<string, object> hfregjistrime, bool modifikim, out string mesazhmevonshem)
        {
            mesazhmevonshem = "";
               clsMesazh pergjigja;
            bool kaNdryshimNumri;
            pergjigja = kontrolloAmortizim(out kaNdryshimNumri, hfregjistrime, modifikim);
            if (!pergjigja.Status)
            {
                shfaqmesazhapolupe = "jo";
                return pergjigja;
            }
            pergjigja = ruajAmortizimiKokaTransaksion(meKontabilizim, idDokNgaFK, out shfaqmesazhapolupe, iddokngaqk, trupivjeterqendra, idPeriudha, idkategoria, seriale, ngarivleresimi, out mesazhmevonshem);
            return pergjigja;
        }

        /// <summary>
        /// MODULI ASETE:
        /// Ruan objektin e kokes se amortizimit.
        /// </summary>
        /// <param name="moduliAsete">(clsDatabazeAsete) Merr objektin e transaksionit qe te lidhi veprimet me njera tjetren.</param>
        /// <param name="meKontabilizim">(int) Nese ruajtja ka kontabilizim apo jo.</param>
        /// <param name="idDokNgaFK">(int) Merr id e dokumentit nga fleta kontabel.</param>
        /// <param name="shfaqmesazhapolupe">(string) Mesazhin qe duhet te shfaqe.</param>
        /// <param name="iddokngaqk">(int) Merr id e dokumentit nga qendra e kostos.</param>
        /// <param name="trupivjeterqendra">(DbQendraKosto.colTrupiQendraKosto) Merr id e dokumentit te qendres se kostos.</param>
        /// <param name="idPeriudha">(int) Merr id e periudhes.</param>
        /// <param name="idkategoria">(int) Merr id e kategorise.</param>
        /// <param name="ngarivleresimi"> tregon nqs po ruhet nje dokument rivleresimi</param>
        /// <param name="seriale">koleksioni i serialeve te zgjedhura</param>
        /// <returns>Kthen clsMesazh me atribut statusi True nese ruajtja kryhet me sukses ose False dhe mesazhin e gabimit ne te kundert.</returns>
        public clsMesazh ruajTrans(int meKontabilizim, int idDokNgaFK, out string shfaqmesazhapolupe, int iddokngaqk, DbQendraKosto.colTrupiQendraKosto trupivjeterqendra, int idPeriudha, int idkategoria, colSerialetMagazine seriale, bool ngarivleresimi, IDictionary<string, object> hfregjistrime, ResourceManager rm, CultureInfo ci, bool modifikim)
        {
            string mesazhmevonshem = "";
            clsMesazh pergjigja;
            using (var scope = new MyTransactionScope())
            {
                pergjigja = ruaj(meKontabilizim, idDokNgaFK, out shfaqmesazhapolupe, iddokngaqk, trupivjeterqendra, idPeriudha, idkategoria, seriale, ngarivleresimi, hfregjistrime, modifikim, out mesazhmevonshem);
                if (!pergjigja) return pergjigja;
                scope.Complete();
            }
            return pergjigja;
        }

        private clsMesazh kontrolloAmortizim(out bool kaNdryshimNumri, IDictionary<string, object> hfregjistrime, bool modifikim)
        {
            kaNdryshimNumri = false;
            clsMesazh mes = new clsMesazh();
            clsDatabazeAsete moduliAsete = new clsDatabazeAsete();
            if (!modifikim)
                if (moduliAsete.ekzistonRegjistrimAmortizimi(idNiveli, idKonfigurimAmbjenti, dateDokumenti, nrDok, idNdermarrje, idLlojStandarti))
                    return new clsMesazh(false, "Ekziston një regjistrim me të njëjtin numër dokumenti!");
            if (hfregjistrime != null)
            {
                mes = kontrolloNrAutoAmortizim(out kaNdryshimNumri, hfregjistrime);
                if (!mes.Status)
                    return mes;
            }

            return new clsMesazh(true, "Kontrollet u kaluan me sukses");
        }
        public static bool kaVeprimeMeKeteAset(int idnivel, int idseriali, int idstandarti, int iddok)
        {
            using (clsDatabazeAsete db = new DbAsete.clsDatabazeAsete())
            {
                return db.kaVeprimeMeKeteAset(idnivel, idseriali, idstandarti, iddok);
            }
        }
        private clsMesazh kontrolloNrAutoAmortizim(out bool kaNdryshimNumri, IDictionary<string, object> hfregjistrime)
        {
            clsDatabaseAdmin db = new clsDatabaseAdmin();
            List<NrAuto> list = DbAdmin.clsNrAutom.kontrollogjithenumrat(db, hfregjistrime, this.dateDokumenti);
            if (NrAuto.ktheVlerenEre(list, "NrDok") != "")
                this.nrDok = NrAuto.ktheVlerenEre(list, "NrDok");
            DbCore.clsMesazh mes = NrAuto.ruajvlera(out kaNdryshimNumri, list, this.dateDokumenti, this.idPerdoruesi, this.idNdermarrje, db);
            return new clsMesazh(mes.Status, mes.PershkrimMesazhi);
        }

        /// <summary>
        /// MODULI ASETE:
        /// Modifikon objektin e rreshtit te kokes se amortizimit.
        /// </summary>
        /// <param name="moduliAsete">(clsDatabazeAsete) Merr objektin e transaksionit qe te lidhi veprimet me njera tjetren.</param>
        /// <returns>Kthen True nese modifikimi perfundon me sukses, ose False nese modifikimi jep gabim.</returns>
        public clsMesazh modifikoKokaPerRillogaritje(ResourceManager rm, CultureInfo ci, DbData dbData)
        {
            clsMesazh pergjigja = modifikoAmortizimiKokaTransaksionPerRillogaritje(rm, ci, dbData);
            return pergjigja;
        }

        /// <summary>
        /// MODULI ASETE:
        /// Fshin objektin e dokumentit te amortizimit.
        /// </summary>
        /// <param name="idPerdoruesi">(int) Id e perdoruesit qe ka modifikuar per here te fundit amortizimin e kokes.</param>
        /// <param name="idkategoria">(int) Merr id e kategorise.</param>
        /// <param name="rivleresim">tregon nqs dokumenti qe po fshihet eshte dokument rivleresimi. ne rastin e dokumentave te rivleresimit kemi disa dokumenta qe fshihen sipas standartit por shfaqim vetem nje</param>
        /// <param name="moduliAsete">(clsDatabazeAsete) Merr objektin e transaksionit qe te lidhi veprimet me njera tjetren.</param>
        /// <param name="trupiRi">koleksioni i trupit te ri, perdoret ne rastet e modifikimeve per te pare nese seriali qe po fshihet ekziston tek dokumenti i ri ne rast modifikimi apo jo</param>
        /// <returns>Kthen clsMesazh me atribut statusi True nese fshirja kryhet me sukses ose False dhe mesazhin e gabimit ne te kundert.</returns>
        public clsMesazh fshi(int idPerdoruesi, int idkategoria, bool rivleresim, colAmortizimiTrupiAbstract trupiRi)
        {
            clsMesazh pergjigja = new clsMesazh();
            if (rivleresim)// rastet per rivleresimin e aseteve qe kemi me shume se nje dokument por shfaqim vetem njerin
            {
                colAmortizimiKoka col = new colAmortizimiKoka();
                col.ktheAmortizimKokaSipasMagDateNenKatDheNrDok(dateDokumenti, idNjesiAdministrative, nrDok, idNdermarrje, idKonfigurimAmbjenti);///marim gjithe dokumentat per gjithe standartet
                bool rivleresimXSerial = col.Count > 1 ? false : true;
                foreach (clsAmortizimiKoka koka in col)
                {
                    pergjigja = koka.fshiAmortizimKokaTransaksion(idPerdoruesi, idkategoria, trupiRi, rivleresimXSerial);
                    if (!pergjigja.Status)
                        return pergjigja;
                }
                if (!pergjigja.Status)
                    return pergjigja;
            }
            if (idkategoria == 90)///rasti i amortizimit fillestar per te zeruar amortizimin fillestar
            {
                colAmortizimiTrupiAbstract coltrupi = new colAmortizimiTrupi();
                coltrupi.merrAmortizimTrupiSipasIdKokaAmortizimi(idAmortizimi);

                foreach (clsAmortizimiTrupiAbstract trup in coltrupi)
                {
                    clsAmortizimiFillestar amort = new clsAmortizimiFillestar();
                    amort.merrAmortizimFillestarSipasSerialitDheStandartit(trup.IdAQTSeriali, idLlojStandarti, trup.IdAmortizimKoka);
                    pergjigja = amort.fshi();
                    if (!pergjigja.Status)
                        return pergjigja;
                }
            }
            pergjigja = fshiAmortizimKokaTransaksion(idPerdoruesi, idkategoria, trupiRi, false);
            return pergjigja;
        }

        /// <summary>
        /// MODULI ASETE:
        /// Fshin objektin e dokumentit te amortizimit.
        /// </summary>
        /// <param name="idPerdoruesi">(int) Id e perdoruesit qe ka modifikuar per here te fundit amortizimin e kokes.</param>
        /// <param name="idkategoria">(int) Merr id e kategorise.</param>
        /// <param name="rivleresim">tregon nqs dokumenti qe po fshihet eshte dokument rivleresimi. ne rastin e dokumentave te rivleresimit kemi disa dokumenta qe fshihen sipas standartit por shfaqim vetem nje</param>
        /// <param name="moduliAsete">(clsDatabazeAsete) Merr objektin e transaksionit qe te lidhi veprimet me njera tjetren.</param>
        /// <param name="trupiRi">koleksioni i trupit te ri, perdoret ne rastet e modifikimeve per te pare nese seriali qe po fshihet ekziston tek dokumenti i ri ne rast modifikimi apo jo</param>
        /// <returns>Kthen clsMesazh me atribut statusi True nese fshirja kryhet me sukses ose False dhe mesazhin e gabimit ne te kundert.</returns>
        public clsMesazh fshiTrans(int idPerdoruesi, int idkategoria, bool rivleresim, colAmortizimiTrupiAbstract trupiRi)
        {
            clsMesazh pergjigja = new clsMesazh();
            using (var scope = new MyTransactionScope())
            {
                pergjigja = fshi(idPerdoruesi, idkategoria, rivleresim, trupiRi);
                if (!pergjigja.Status)
                    return pergjigja;
                scope.Complete();
            }
            return pergjigja;
        }

        /// <summary>
        /// MODULI ASETE:
        /// Modifikimi i dokumentit te amortizimit.
        /// </summary>
        /// <param name="meKontabilizim">(int) Merr kontabilizim e dokumentit te amortizimit.</param>
        /// <param name="lidhur">(bool) True nese eshte i lidhur me dokumente te tjere.</param>
        /// <param name="idperiudha">(int) Id e periudhes.</param>
        /// <param name="idkategoria">(int) Id e kategorise.</param>
        /// <param name="seriale">serialet e reja qe do ruhen</param>
        /// <param name="ngarivleresimi">tregon nqs dokumenti eshte dokument rivleresimi</param>
        /// <param name="colaqt">koleksion me serialet perdoret per te ruajtur vlerat e amortizimit fillestar</param>
        /// <param name="shfaqmesazhapolupe">(string) Mesazhi qe duhet te shfaqe ne mesazh ose ne lupe.</param>
        /// <returns>Kthen clsMesazh me atribut statusi True nese modifikimi kryhet me sukses ose False dhe mesazhin e gabimit ne te kundert.</returns>
        public clsMesazh modifiko(int meKontabilizim, bool lidhur, int idperiudha, int idkategoria, out string shfaqmesazhapolupe, colSerialetMagazine seriale, bool ngarivleresimi, colAmortizimiFillestar colaqt, ResourceManager rm, CultureInfo ci)
        {
            string mesazhmevonshem = "";
            clsMesazh u_modifikua;
            //data.krijoManager();
            shfaqmesazhapolupe = "jo";
            if (lidhur == false)
            {
                using (var scope = new MyTransactionScope())
                {
                    try
                    {
                        clsAmortizimiKoka kokavjeter = new clsAmortizimiKoka();
                        kokavjeter.ktheAmortizimKokaSipasId(idAmortizimi);
                        if (string.IsNullOrEmpty(kokavjeter.NrDok) || kokavjeter.IdStatusDokumenti == 2 || kokavjeter.idAmortizimi <= 0)
                        {
                            return new clsMesazh(false, "Dokumenti ka ndryshuar! Ju lutem rihapeni përsëri!");
                        }
                        if (idkategoria == 90)// per amortizimin fillestar
                        {
                            kokavjeter.colTrupi.merrAmortizimTrupiSipasIdKokaAmortizimi(idAmortizimi);

                            foreach (clsAmortizimiTrupiAbstract trup in kokavjeter.colTrupi)
                            {
                                clsAmortizimiFillestar amort = new clsAmortizimiFillestar();
                                amort.merrAmortizimFillestarSipasSerialitDheStandartit(trup.IdAQTSeriali, kokavjeter.idLlojStandarti, trup.IdAmortizimKoka);

                                u_modifikua = amort.fshi();//amortizimet e vjetra i fshijme
                                if (!u_modifikua.Status)
                                {
                                    return u_modifikua;
                                }
                            }
                        }

                        u_modifikua = kokavjeter.fshi(idPerdoruesi, idkategoria, ngarivleresimi, this.colTrupi);
                        if (!u_modifikua.Status)
                            return u_modifikua;

                        oKokaMagazina.IdDokNga = kokavjeter.oKokaMagazina.IdKokaMagazina;
                        oKokaMagazina.IdKrijuesi = kokavjeter.oKokaMagazina.IdKrijuesi;
                        idDokNga = kokavjeter.IdAmortizimi;
                        NrRenditje = kokavjeter.nrRenditje;
                        u_modifikua = ruaj(meKontabilizim, kokavjeter.OFleteKontabel.IdKokaFleteKontabel, out shfaqmesazhapolupe, kokavjeter.OFleteKontabel.KokaQendraKosto.IdKoka, kokavjeter.OFleteKontabel.KokaQendraKosto.ColTrupi, idperiudha, idkategoria, seriale, ngarivleresimi, null, true, out mesazhmevonshem);

                        if (!u_modifikua.Status)
                            return u_modifikua;

                        foreach (clsAmortizimiFillestar ser in colaqt)
                        {
                            if (ser.IdSerial == 0)
                                continue;
                            ser.IdDokNga = this.idAmortizimi;
                            u_modifikua = ser.modifiko();//serialet e reja vendosim amortizimin fillestar
                            if (!u_modifikua.Status)
                                return u_modifikua;
                        }
                        scope.Complete();
                    }
                    catch (Exception ex)
                    {
                        return new clsMesazh(false, ex.Message);
                    }
                }
                return u_modifikua;
            }
            using (var scope = new MyTransactionScope())
            {
                clsDatabazeAsete data = new clsDatabazeAsete();
                u_modifikua = data.modifikoAmortizimiKoka(idAmortizimi, nrDok, dateDokumenti, dateAmortizimi, dateRegjistrimi, idNjesiAdministrative, shenime, amortizimiShteseTotal, idLlojStandarti, idStatusDokumenti, idPerdoruesi);
                if (!u_modifikua.Status)
                    return u_modifikua;
                scope.Complete();
            }
            return u_modifikua;
        }
        /// <summary>
        /// merr id e dokumentave qe e kane lidhur kete dokument
        /// </summary>
        /// <returns></returns>
        public DataTable merrIdsDokLidhur()
        {
            DbAdmin.clsDatabaseAdmin dbAdmin = new DbAdmin.clsDatabaseAdmin();
            DataTable dt = dbAdmin.MerrDokLidhur(idAmortizimi, idNiveli, "T_ASETE_AMORTIZIMI_KOKA", "ID_AMORTIZIMI");
            dbAdmin.Dispose();
            return dt;
        }

        /// <summary>
        /// tregon nese ky dokument eshte i lidhur
        /// </summary>
        /// <returns></returns>
        public bool eshteILidhur()
        {
            DbAdmin.clsDatabaseAdmin dbAdmin = new DbAdmin.clsDatabaseAdmin();
            bool lidhur = dbAdmin.eshteDokumentiILidhur(idAmortizimi, idNiveli, "T_ASETE_AMORTIZIMI_KOKA", "ID_AMORTIZIMI");
            dbAdmin.Dispose();
            return lidhur;
        }

        /// <summary>
        /// MODULI ASETE:
        /// Kthen amortizimin e kokes sipas id se kokes.
        /// </summary>
        /// <param name="id">(int) Id e kokes se amortizimit.</param>
        /// <returns>Kthen True nese mbushja e objektit kryhet me sukses, ne te kundert False.</returns>
        public bool ktheAmortizimKokaSipasId(int id)
        {
            using (clsDatabazeAsete moduliAsete = new clsDatabazeAsete())
            {
                return mbushAmortizimKokaObjekt(moduliAsete.ktheAmortizimKokaSipasId(id));
            }
        }

        /// <summary>
        /// MODULI ASETE:
        /// Kthen amortizimin e kokes sipas id se kokes ne transaksion.
        /// </summary>
        /// <param name="id">(int) Id e kokes se amortizimit.</param>
        /// <param name="moduliAsete">(clsDatabazeAsete) Merr objektin e transaksionit qe te lidhi veprimet me njera tjetren.</param>
        /// <returns>Kthen True nese mbushja e objektit kryhet me sukses, ne te kundert False.</returns>
        public bool ktheAmortizimKokaSipasId(int id, clsDatabazeAsete moduliAsete)
        {
            return mbushAmortizimKokaObjekt(moduliAsete.ktheAmortizimKokaSipasId(id));
        }

        #region Krijimi i dokumentit te amortizimit nga raste te ndryshme

        /// <summary>
        /// MODULI ASETE:
        /// Krijon dokumentin e amortizimit nga ndryshimi i statusit.
        /// </summary>
        /// <param name="dataNdryshimStatus">(DateTime) Data e ndryshimit te statusit te ri.</param>
        /// <param name="NjesiAdministrative">(DbRegjistrim.clsNjesiAdministrative) Njesia administrative ku po ndodh ndryshimi i statusit.</param>
        /// <param name="idLlojStandarti">(int) Id e llojit te standartit.</param>
        /// <param name="idNdermarrje">(int) Id e ndermarrjes qe po kryhet veprimi.</param>
        /// <param name="idNderViti">(int) id e lidhjes mes ndermarrjes dhe vitit fiskal.</param>
        /// <param name="idPerdoruesi">(int) Id e perdoruesit qe po kryen ndryshimin e statusit.</param>
        /// <param name="nrRreshti">(int) Numri i rreshtit qe po kryhet veprimi.</param>
        /// <param name="konfamortizimi">(DbShare.clsKonfigurimAmbjenti) Konfigurimi i ambjentit te amortizimit.</param>
        /// <param name="idnivelgjenerues">(int) Id llojit te nivelit qe e ka gjeneruar.</param>
        /// <param name="dbasete">(clsDatabazeAsete) Merr objektin e transaksionit qe te lidhi veprimet me njera tjetren.</param>
        /// <returns>Kthen True nese mbushja e objektit kryhet me sukses, ne te kundert False.</returns>
        public bool krijoDokumentAmortizimiNgaNdryshimiStatusit(DateTime dataNdryshimStatus, clsNjesiAdministrative NjesiAdministrative, int idLlojStandarti, int idNdermarrje, int idNderViti, int idPerdoruesi, out int nrRreshti, DbShare.clsKonfigurimAmbjenti konfamortizimi, int idnivelgjenerues, clsDatabazeAsete dbasete)
        {
            bool pergjigja = true;
            //Lloji i dokumentit FANS
            filloObjektin(NjesiAdministrative.Kodi + dataNdryshimStatus.ToString("ddMMyyyy"), konfamortizimi.IdNivel, konfamortizimi.IdKonfigAmbjente,
                dataNdryshimStatus, dataNdryshimStatus, DateTime.Now, NjesiAdministrative.IdNjesiAdministrative, idLlojStandarti, 0, 1, "Dokument amortizimi i krijuar nga ndryshimi i statusit", idNderViti,
                idNdermarrje, idPerdoruesi, idPerdoruesi, DateTime.Now, NjesiAdministrative.IdNjesiAdministrative, idnivelgjenerues, NjesiAdministrative.IdKonfig, 0, 0, 0);
            string mesazhmevonshem = "";
            pergjigja = colTrupi.krijoTrupiDokAmortizimiPerLlogaritje(this, NjesiAdministrative.IdHistorikFundit, true, out nrRreshti, 0, new List<object>(), null, out mesazhmevonshem).Status;
            if (!pergjigja)
                return pergjigja;
            amortizimiShteseTotal = colTrupi.Sum(item => item.AmortizimiShtese);
            return pergjigja;
        }

        /// <summary>
        /// MODULI ASETE:
        /// Krijon dokumentin e amortizimit nga dokumenti i blerjes.
        /// </summary>
        /// <param name="serialet">(colSerialetMagazine) Lista e serialeve qe po behen hyrje me kete dokument.</param>
        /// <param name="dokBlerjes">(DbRegjistrim.clsKokaShitje) Dokumenti i blerjes qe po gjeneron amortizimin.</param>
        /// <param name="idLlojStandarti">(int) Id e llojit te standartit.</param>
        /// <param name="konfamortizimi">(DbShare.clsKonfigurimAmbjenti) Konfigurimi i ambjentit te amortizimit.</param>
        /// <param name="iddoknga">(int) Id e dokumentit nga ka ardhur.</param>
        /// <param name="dbasete">(clsDatabazeAsete) Merr objektin e transaksionit qe te lidhi veprimet me njera tjetren.</param>
        /// <returns>Kthen True nese mbushja e objektit kryhet me sukses, ne te kundert False.</returns>
        public bool krijoDokumentAmortizimiNgaBlerjaMagazine(colSerialetMagazine serialet, clsKokaShitje dokBlerjes, int idLlojStandarti, DbShare.clsKonfigurimAmbjenti konfamortizimi, int iddoknga, int nrRenditja, clsDatabazeAsete dbasete)
        {
            //Lloji i dokumentit FAB
            filloObjektin(dokBlerjes.NrDok, konfamortizimi.IdNivel, konfamortizimi.IdKonfigAmbjente, dokBlerjes.DtDok, dokBlerjes.DtDok, dokBlerjes.DtRegjistrimi, dokBlerjes.OKokaMagazina.IdMagazina, idLlojStandarti, 0, dokBlerjes.IdStatusDok, dokBlerjes.Pershkrimi, dokBlerjes.IdNdermarrjeVit, dokBlerjes.IdNdermarrje, dokBlerjes.IdPerdoruesi, dokBlerjes.IdKrijuesi, DateTime.Now, dokBlerjes.IdShitjeKoka, dokBlerjes.IdNivel, dokBlerjes.IdKonfigAmbjente, iddoknga, 0, nrRenditja);
            bool pergjigja = colTrupi.krijoTrupiDokAmortizimiNgaVeprimeMagazineBlerje(serialet, dateAmortizimi, this.idLlojStandarti);
            amortizimiShteseTotal = colTrupi.Sum(item => item.AmortizimiShtese);
            return pergjigja;
        }

        /// <summary>
        /// MODULI ASETE:
        /// Krijon dokumentin e amortizimit per daljen nga dokumenti i daljes.
        /// </summary>
        /// <param name="serialet">(colSerialetMagazine) Lista e serialeve qe po behen hyrje me kete dokument.</param>
        /// <param name="serialeERinjTeNdashem">(colHistorikAQTSeriale) Lista e serialeve te rinj te krijuar te ndare nga serialet prind.</param>
        /// <param name="dokMagazine">(DbRegjistrim.clsKokaMagazina) Dokumenti i magazines qe po gjeneron amortizimin.</param>
        /// <param name="idLlojStandarti">(int) Id e llojit te standartit.</param>
        /// <param name="konfamortizimi">(DbShare.clsKonfigurimAmbjenti) Konfigurimi i ambjentit te amortizimit.</param>
        /// <param name="iddoknga">(int) Id e dokumentit nga ka ardhur.</param>
        /// <param name="mosLlogaritAmortizimShtese"></param>
        /// <param name="dbasete">(clsDatabazeAsete) Merr objektin e transaksionit qe te lidhi veprimet me njera tjetren.</param>
        /// <returns>Kthen True nese mbushja e objektit kryhet me sukses, ne te kundert False.</returns>
        public clsMesazh krijoDokumentAmortizimiNgaDaljaMagazine(colSerialetMagazine serialet, colHistorikAQTSeriale serialeERinjTeNdashem, clsKokaMagazina dokMagazine, int idLlojStandarti, DbShare.clsKonfigurimAmbjenti konfamortizimi, int iddoknga, int nrRenditja, bool mosLlogaritAmortizimShtese, int idMagKoka, out string mesazhmevonshem)
        {
            mesazhmevonshem = "";
               clsMesazh pergjigja = new clsMesazh(true);
            String konfigurimi = clsKonfigurimAmbjenti.ktheKodKonfigurimi(dokMagazine.IdKonfigAmbjente);
            int idkrijuesi = dokMagazine.IdKrijuesi;
            if (idkrijuesi == 0) idkrijuesi = idPerdoruesi;
            //clsKokaMagazina.merrKrijuesin(dokMagazine.NrDok, dokMagazine.IdNdermarrje, dokMagazine.DtDok, konfigurimi, dbregj);
            // if (idkrijuesi == 0) idkrijuesi = dokMagazine.IdPerdoruesi;
            filloObjektin(dokMagazine.NrDok, konfamortizimi.IdNivel, konfamortizimi.IdKonfigAmbjente, dokMagazine.DtDok, dokMagazine.DtDok, dokMagazine.DtDok, dokMagazine.IdMagazina, idLlojStandarti, 0, dokMagazine.IdStatusDok, dokMagazine.Shenime, dokMagazine.IdNdermarrjeVit,
                dokMagazine.IdNdermarrje, dokMagazine.IdPerdoruesi, idkrijuesi, DateTime.Now, dokMagazine.IdKokaMagazina, dokMagazine.IdNivel, dokMagazine.IdKonfigAmbjente, iddoknga, 0, nrRenditja);
            colAmortizimiTrupiAbstract trupiPlote = new colAmortizimiTrupi();
            pergjigja = colTrupi.krijoTrupiDokAmortizimiNgaVeprimeMagazineDaljaPerTransferim(ref trupiPlote, serialet, serialeERinjTeNdashem, dateAmortizimi, this.idLlojStandarti, idNdermarrje, konfamortizimi.IdKonfigAmbjente, mosLlogaritAmortizimShtese, nrRenditja, idMagKoka, out mesazhmevonshem);
            if (!pergjigja.Status)
                return pergjigja;
            string mesazhmevonshemrezerva = "";
            pergjigja = colTrupiRezerva.krijoTrupiDokAmortizimiNgaVeprimeMagazineDaljaPerTransferim(ref trupiPlote, serialet, serialeERinjTeNdashem, dateAmortizimi, this.idLlojStandarti, idNdermarrje, konfamortizimi.IdKonfigAmbjente, mosLlogaritAmortizimShtese, nrRenditja, idMagKoka, out mesazhmevonshemrezerva);
            if (!pergjigja.Status)
                return pergjigja;
            if (mesazhmevonshem == "" && mesazhmevonshemrezerva != "")
                mesazhmevonshem = mesazhmevonshemrezerva;
            amortizimiShteseTotal = colTrupi.Sum(item => item.AmortizimiShtese);
            return pergjigja;
        }

        /// <summary>
        /// MODULI ASETE:
        /// Krijon dokumentin e amortizimit per shitjen nga dokumenti i shitjes.
        /// </summary>
        /// <param name="serialet">(colSerialetMagazine) Lista e serialeve qe po behen hyrje me kete dokument.</param>
        /// <param name="serialeERinjTeNdashem">(colHistorikAQTSeriale) Lista e serialeve te rinj te krijuar te ndare nga serialet prind.</param>
        /// <param name="dokShitja">(DbRegjistrim.clsKokaShitje) Dokumenti i shitjes qe po gjeneron amortizimin.</param>
        /// <param name="idLlojStandarti">(int) Id e llojit te standartit.</param>
        /// <param name="konfamortizimi">(DbShare.clsKonfigurimAmbjenti) Konfigurimi i ambjentit te amortizimit.</param>
        /// <param name="iddoknga">(int) Id e dokumentit nga ka ardhur.</param>
        /// <param name="mosLlogaritAmortizimShtese"></param>
        /// <param name="dbasete">(clsDatabazeAsete) Merr objektin e transaksionit qe te lidhi veprimet me njera tjetren.</param>
        /// <returns>Kthen True nese mbushja e objektit kryhet me sukses, ne te kundert False.</returns>
        public clsMesazh krijoDokumentAmortizimiNgaShitjaMagazine(colSerialetMagazine serialet, colHistorikAQTSeriale serialeERinjTeNdashem, clsKokaShitje dokShitja, int idLlojStandarti, DbShare.clsKonfigurimAmbjenti konfamortizimi, int iddoknga, int nrRenditje, bool mosLlogaritAmortizimShtese, int idMagKoka, out string mesazhmevonshem)
        {
            mesazhmevonshem = "";
               clsMesazh pergjigja = new clsMesazh(true);
            //Lloji i dokumentit FAS
            filloObjektin(dokShitja.NrDok, konfamortizimi.IdNivel, konfamortizimi.IdKonfigAmbjente, dokShitja.DtDok, dokShitja.DtDok, dokShitja.DtDok, dokShitja.OKokaMagazina.IdMagazina, idLlojStandarti, 0, dokShitja.IdStatusDok, dokShitja.Pershkrimi,
                dokShitja.IdNdermarrjeVit, dokShitja.IdNdermarrje, dokShitja.IdPerdoruesi, dokShitja.IdKrijuesi, DateTime.Now, dokShitja.IdShitjeKoka, dokShitja.IdNivel, dokShitja.IdKonfigAmbjente, iddoknga, 0, nrRenditje);
            colAmortizimiTrupiAbstract trupiPlote = new colAmortizimiTrupi();
            pergjigja = colTrupi.krijoTrupiDokAmortizimiNgaVeprimeMagazineDaljaPerTransferim(ref trupiPlote, serialet, serialeERinjTeNdashem, dateAmortizimi, this.idLlojStandarti, idNdermarrje, konfamortizimi.IdKonfigAmbjente, mosLlogaritAmortizimShtese, nrRenditje, idMagKoka, out mesazhmevonshem);
            if (!pergjigja.Status)
                return pergjigja;
            string mesazhmevonshemrezerva = "";
            pergjigja = colTrupiRezerva.krijoTrupiDokAmortizimiNgaVeprimeMagazineDaljaPerTransferim(ref trupiPlote, serialet, serialeERinjTeNdashem, dateAmortizimi, this.idLlojStandarti, idNdermarrje, konfamortizimi.IdKonfigAmbjente, mosLlogaritAmortizimShtese, nrRenditje, idMagKoka, out mesazhmevonshemrezerva);
            if (!pergjigja.Status)
                return pergjigja;
            if (mesazhmevonshem == "" && mesazhmevonshemrezerva != "")
                mesazhmevonshem = mesazhmevonshemrezerva;
            amortizimiShteseTotal = colTrupi.Sum(item => item.AmortizimiShtese);
            return pergjigja;
        }

        #region Krijimi i kokave te dokumetave te amortizimit per transferimmin

        /// <summary>
        /// MODULI ASETE:
        /// Krijon dokumentin e amortizimit per daljen nga dokumenti i transferimit.
        /// </summary>
        /// <param name="kokaPerHyrje">(clsAmortizimiKoka) Koka e amortizimit per dokumentat qe do behen hyrje.</param>
        /// <param name="serialet">(colSerialetMagazine) Lista e serialeve qe po behen hyrje me kete dokument.</param>
        /// <param name="serialeERinjTeNdashem">(colHistorikAQTSeriale) Lista e serialeve te rinj te krijuar te ndare nga serialet prind.</param>
        /// <param name="dokMagazine">(DbRegjistrim.clsKokaMagazina) Dokumenti i magazines qe po gjeneron amortizimin.</param>
        /// <param name="idLlojStandarti">(int) Id e llojit te standartit.</param>
        /// <param name="konfamortizimi">(DbShare.clsKonfigurimAmbjenti) Konfigurimi i ambjentit te amortizimit.</param>
        /// <param name="iddoknga">(int) Id e dokumentit nga ka ardhur.</param>
        /// <param name="mosLlogaritAmortizimShtese"></param>
        /// <param name="dbasete">(clsDatabazeAsete) Merr objektin e transaksionit qe te lidhi veprimet me njera tjetren.</param>
        /// <returns>Kthen True nese mbushja e objektit kryhet me sukses, ne te kundert False.</returns>
        public clsMesazh krijoDokumentAmortizimiNgaDaljaMagazinePerTransferim(ref clsAmortizimiKoka kokaPerHyrje, colSerialetMagazine serialet, colHistorikAQTSeriale serialeERinjTeNdashem, clsKokaMagazina dokMagazine, int idLlojStandarti, DbShare.clsKonfigurimAmbjenti konfamortizimi, int iddoknga, int nrRenditje, bool mosLlogaritAmortizimShtese, int idMagKoka, out string mesazhmevonshem)
        {
            mesazhmevonshem = "";
               clsMesazh pergjigja = new clsMesazh(true);
            //String konfigurimi = clsKonfigurimAmbjenti.ktheKodKonfigurimi(dokMagazine.IdKonfigAmbjente, dbshare);
            int idkrijuesi = dokMagazine.IdKrijuesi;
            if (idkrijuesi == 0) idkrijuesi = idPerdoruesi;
            //clsKokaMagazina.merrKrijuesin(dokMagazine.NrDok, dokMagazine.IdNdermarrje, dokMagazine.DtDok, konfigurimi, dbregj);
            //  if (idkrijuesi == 0) idkrijuesi = dokMagazine.IdPerdoruesi;
            filloObjektin(dokMagazine.NrDok, konfamortizimi.IdNivel, konfamortizimi.IdKonfigAmbjente, dokMagazine.DtDok, dokMagazine.DtDok, dokMagazine.DtRegjistrimi, dokMagazine.IdMagazina, idLlojStandarti, 0, dokMagazine.IdStatusDok, dokMagazine.Shenime, dokMagazine.IdNdermarrjeVit, dokMagazine.IdNdermarrje, dokMagazine.IdPerdoruesi, idkrijuesi, DateTime.Now, dokMagazine.IdKokaMagazina, dokMagazine.IdNivel, dokMagazine.IdKonfigAmbjente, iddoknga, 0, nrRenditje);
            //Marrja e trupit te ri vetem per hyrjen. Rasti kur i njejti serial del ne te njejtin dokument me shume se nje here.
            kokaPerHyrje = (clsAmortizimiKoka)this.Clone();
            pergjigja = colTrupi.krijoTrupiDokAmortizimiNgaVeprimeMagazineDaljaPerTransferim(ref kokaPerHyrje.colTrupi, serialet, serialeERinjTeNdashem, dateAmortizimi, this.idLlojStandarti, idNdermarrje, konfamortizimi.IdKonfigAmbjente, mosLlogaritAmortizimShtese, nrRenditje, idMagKoka, out mesazhmevonshem);
            if (!pergjigja.Status)
                return pergjigja;
            string mesazhmevonshemrezerva = "";
            pergjigja = colTrupiRezerva.krijoTrupiDokAmortizimiNgaVeprimeMagazineDaljaPerTransferim(ref kokaPerHyrje.colTrupiRezerva, serialet, serialeERinjTeNdashem, dateAmortizimi, this.idLlojStandarti, idNdermarrje, konfamortizimi.IdKonfigAmbjente, mosLlogaritAmortizimShtese, nrRenditje, idMagKoka, out mesazhmevonshem);
            if (!pergjigja.Status)
                return pergjigja;
            if (mesazhmevonshem == "" && mesazhmevonshemrezerva != "")
                mesazhmevonshem = mesazhmevonshemrezerva;
            amortizimiShteseTotal = colTrupi.Sum(item => item.AmortizimiShtese);
            kokaPerHyrje.amortizimiShteseTotal = amortizimiShteseTotal;
            return pergjigja;
        }

        /// <summary>
        /// MODULI ASETE:
        /// Krijon dokumentin e amortizimit per hyrjen nga dokumenti i transferimit.
        /// </summary>
        /// <param name="amortizimiKrijuarNgaDalja">(clsAmortizimiKoka) Dokumenti i amortizimit i krijuar per daljen nga magazina.</param>
        /// <param name="dokMagazine">(DbRegjistrim.clsKokaMagazina) Dokumenti i magazines qe po gjeneron amortizimin.</param>
        /// <param name="konfamortizimi">(DbShare.clsKonfigurimAmbjenti) Konfigurimi i ambjentit te amortizimit.</param>
        /// <param name="iddoknga">(int) Id e dokumentit nga ka ardhur.</param>
        /// <param name="dbasete">(clsDatabazeAsete) Merr objektin e transaksionit qe te lidhi veprimet me njera tjetren.</param>
        /// <returns>Kthen True nese mbushja e objektit kryhet me sukses, ne te kundert False.</returns>
        public bool krijoDokumentAmortizimiNgaHyrjaMagazinePerTransferim(clsAmortizimiKoka amortizimiKrijuarNgaDalja, clsKokaMagazina dokMagazine, DbShare.clsKonfigurimAmbjenti konfamortizimi, int iddoknga, int nrRenditje, colHistorikAQTSeriale serialetendashem)
        {
            bool pergjigja = true;
            String konfigurimi = clsKonfigurimAmbjenti.ktheKodKonfigurimi(dokMagazine.IdKonfigAmbjente);
            int idkrijuesi = dokMagazine.IdKrijuesi;
            if (idkrijuesi == 0) idkrijuesi = idPerdoruesi;
            //clsKokaMagazina.merrKrijuesin(dokMagazine.NrDok, dokMagazine.IdNdermarrje, dokMagazine.DtDok, konfigurimi, dbregj);
            //if (idkrijuesi == 0) idkrijuesi = dokMagazine.OMagazinaTransferim.IdPerdoruesi;
            filloObjektin(dokMagazine.OMagazinaTransferim.NrDok, konfamortizimi.IdNivel, konfamortizimi.IdKonfigAmbjente, amortizimiKrijuarNgaDalja.DateDokumenti, amortizimiKrijuarNgaDalja.DateDokumenti, dokMagazine.OMagazinaTransferim.DtRegjistrimi, dokMagazine.OMagazinaTransferim.IdMagazina, amortizimiKrijuarNgaDalja.IdLlojStandarti, 0, dokMagazine.OMagazinaTransferim.IdStatusDok, dokMagazine.OMagazinaTransferim.Shenime, dokMagazine.OMagazinaTransferim.IdNdermarrjeVit, dokMagazine.OMagazinaTransferim.IdNdermarrje, dokMagazine.OMagazinaTransferim.IdPerdoruesi, idkrijuesi, DateTime.Now, amortizimiKrijuarNgaDalja.idAmortizimi, amortizimiKrijuarNgaDalja.idNiveli, amortizimiKrijuarNgaDalja.idKonfigurimAmbjenti, iddoknga, 0, nrRenditje);
            pergjigja = colTrupi.krijoTrupiDokAmortizimiNgaVeprimeMagazineHyrjePerTransferim(amortizimiKrijuarNgaDalja, idNjesiAdministrative, serialetendashem, dokMagazine.OMagazinaTransferim);
            if (!pergjigja)
                return pergjigja;
            pergjigja = colTrupiRezerva.krijoTrupiDokAmortizimiNgaVeprimeMagazineHyrjePerTransferim(amortizimiKrijuarNgaDalja, idNjesiAdministrative, serialetendashem, dokMagazine.OMagazinaTransferim);
            if (!pergjigja)
                return pergjigja;
            amortizimiShteseTotal = colTrupi.Sum(item => item.AmortizimiShtese);
            return pergjigja;
        }

        #endregion

        /// <summary>
        /// MODULI ASETE:
        /// Krijon dokumentin e amortizimit per llogaritjet mujore te amortizimit.
        /// </summary>
        /// <param name="nrDok">(string) Numri i dokumentit qe po krijohet.</param>
        /// <param name="idNiveli">(int) Id e nekategorise se dokumentit.</param>
        /// <param name="idKonfigurimAmbjenti">(int) Id e llojit te dokumentit.</param>
        /// <param name="dateDokumenti">(DateTime) Data e dokumentit te krijuar te amortizimit.</param>
        /// <param name="dateAmortizimi">(DateTime) Data e amortizimit te dokumentit.</param>
        /// <param name="idNjesiAdministrative">(int) Id e njesise administrative ku po behet regjistrimi i amortizimit.</param>
        /// <param name="idLlojStandarti">(int) Id e llojit te standartit.</param>
        /// <param name="shenime">(string) Shenime te ndryshme qe vendosen ne ambjentit e ruajtjes te amortizimit.</param>
        /// <param name="idNderViti">(int) Id e lidhjes se ndemarrjes me vitin.</param>
        /// <param name="idNdermarrje">(int) Id e ndermarrjes ku po kryhet amortizimi.</param>
        /// <param name="idPerdoruesi">(int) Id e perdoruesit qe po kryhen amortizimin.</param>
        /// <param name="idstatusdok">(int) Id se ne cfare statusi ndodhet dokumenti, modifikuar, fshire apo i ruajtur.</param>
        /// <param name="nrRreshti">(int) Merr rreshtin qe ndodhet aktualisht procesi.</param>
        /// <param name="idart">(List objektesh) Id e artikujve ne nje list.</param>
        /// <param name="e">(EO.Web.ProgressTaskEventArgs) Merr progresin e progres barit.</param>
        /// <returns>Kthen True nese mbushja e objektit kryhet me sukses, ne te kundert False.</returns>
        public clsMesazh krijoDokumentAmortizimiPerLlogaritje(string nrDok, int idNiveli, int idKonfigurimAmbjenti, DateTime dateDokumenti, DateTime dateAmortizimi, int idNjesiAdministrative, int idLlojStandarti, string shenime, int idNderViti, int idNdermarrje, int idPerdoruesi, int idstatusdok, int idkrijuesi, out int nrRreshti, List<object> idart, EO.Web.ProgressTaskEventArgs e, out string mesazhmevonshem)
        {
            mesazhmevonshem = "";
            using (clsDatabazeAsete dbasete = new clsDatabazeAsete())
            {
                clsMesazh pergjigja = new clsMesazh(true, "U llogarit ne rregull!");
                //Lloji i dokumentit FA
                filloObjektin(nrDok, idNiveli, idKonfigurimAmbjenti, dateDokumenti, dateAmortizimi, DateTime.Now, idNjesiAdministrative, idLlojStandarti, 0, idstatusdok, shenime, idNderViti, idNdermarrje, idPerdoruesi,
                    idkrijuesi, DateTime.Now, 0, 0, 0, 0, 0, 0);
                pergjigja = colTrupi.krijoTrupiDokAmortizimiPerLlogaritje(this, 0, false, out nrRreshti, 0, idart, e, out mesazhmevonshem);
                if (!pergjigja.Status)
                    return pergjigja;
                string mesazhmevonshemrez = "";
                pergjigja=colTrupiRezerva.krijoTrupiDokAmortizimiPerLlogaritje(this, 0, false, out nrRreshti, 0, idart, e,out mesazhmevonshemrez);
                if (mesazhmevonshem == "" && mesazhmevonshemrez != "")
                    mesazhmevonshem = mesazhmevonshemrez;
                amortizimiShteseTotal = colTrupi.Sum(item => item.AmortizimiShtese);
                return pergjigja;
            }
        }

        /// <summary>
        /// MODULI ASETE:
        /// Krijon dokumentin e amortizimit per amortizimet fillestare te hyrjeve fillestare ne transaksion.
        /// </summary>
        /// <param name="nrDok">(string) Numri i dokumentit qe po krijohet.</param>
        /// <param name="idNiveli">(int) Id e nekategorise se dokumentit.</param>
        /// <param name="idKonfigurimAmbjenti">(int) Id e llojit te dokumentit.</param>
        /// <param name="dateDokumenti">(DateTime) Data e dokumentit te krijuar te amortizimit.</param>
        /// <param name="dateAmortizimi">(DateTime) Data e amortizimit te dokumentit.</param>
        /// <param name="idNjesiAdministrative">(int) Id e njesise administrative ku po behet regjistrimi i amortizimit.</param>
        /// <param name="idLlojStandarti">(int) Id e llojit te standartit.</param>
        /// <param name="shenime">(string) Shenime te ndryshme qe vendosen ne ambjentit e ruajtjes te amortizimit.</param>
        /// <param name="idNderViti">(int) Id e lidhjes se ndemarrjes me vitin.</param>
        /// <param name="idNdermarrje">(int) Id e ndermarrjes ku po kryhet amortizimi.</param>
        /// <param name="idPerdoruesi">(int) Id e perdoruesit qe po kryhen amortizimin.</param>
        /// <param name="dateRegjistrimi">(DateTime) Data e regjistrimit te dokumentit.</param>
        /// <param name="idstatusdok">(int) Id se ne cfare statusi ndodhet dokumenti, modifikuar, fshire apo i ruajtur.</param>
        /// <param name="idllogari">(int) Id e llogarise kunderparti.</param>
        /// <param name="colTrupiKrijuar">(colAmortizimiTrupi) Lista e trupit te krijuar ne griden e amortizimit fillestar.</param>
        /// <param name="amortizimifillestar">liste me vlerat e amortizimit fillestar</param>
        /// <param name="seriale"> koleksion bosh te serialeve e cila mbushet gjate llogaritjeve me serialet qe do preken nga ky veprim dhe iu vendosen vlerat perkatese te amortizimit fillestar</param>
        /// <returns>Kthen True nese mbushja e objektit kryhet me sukses, ne te kundert False.</returns>
        public clsMesazh krijoDokumentAmortizimiPerAmortizimFillestar(string nrDok, int idNiveli, int idKonfigurimAmbjenti, DateTime dateDokumenti, DateTime dateAmortizimi, int idNjesiAdministrative, int idLlojStandarti, string shenime, int idNderViti, int idNdermarrje, int idPerdoruesi, DateTime dateRegjistrimi, int idstatusdok, int idllogari, colAmortizimiTrupiAbstract colTrupiKrijuar, colAmortizimiFillestar seriale, List<double> amortizimifillestar, int idkrijuesi, out string mesazhmevonshem)
        {
            clsMesazh pergjigja = new clsMesazh(true, "U llogarit ne rregull!");

            //Lloji i dokumentit FAF
            filloObjektin(nrDok, idNiveli, idKonfigurimAmbjenti, dateDokumenti, dateAmortizimi, dateRegjistrimi, idNjesiAdministrative, idLlojStandarti, 0, idstatusdok, shenime, idNderViti, idNdermarrje, idPerdoruesi, idkrijuesi, DateTime.Now, 0, 0, 0, 0, idllogari, 0);

            pergjigja = colTrupiKrijuar.krijoTrupiDokAmortizimiPerAmortizimFillestar(this, seriale, amortizimifillestar,out mesazhmevonshem);
            if (!pergjigja.Status)
                return pergjigja;
            amortizimiShteseTotal = colTrupiKrijuar.Sum(item => item.AmortizimiShtese);
            if (colTrupiKrijuar.objektiKod == enumObjekteAmortizimi.ASETE)
                colTrupi =colTrupiKrijuar;
            else colTrupiRezerva = colTrupiKrijuar;
            return pergjigja;
        }

        public clsMesazh krijoDokumentAmortizimiPerAmortizimFillestarPerImport(string nrDok, string kodNiveli, DateTime dateDokumenti, DateTime dateAmortizimi, string kodMag, int idMag, string llojStandarti, string shenime, int idNderViti, int idNdermarrje, int idPerdoruesi, DateTime dateRegjistrimi, int idstatusdok, string nrLlogari, colAmortizimiTrupiAbstract colTrupiKrijuar, colAmortizimiFillestar seriale, List<double> amortizimifillestar, int idkrijuesi, clsKonfigurimAmbjenti konfigAmbjenti, clsPeriudhaKontabel periudha, int idSt, ResourceManager rm, CultureInfo ci)
        {
            string mesazhmevonshem = "";
            int idNivel = clsNivelRegjistrimi.ktheIdNivelRegjistrimiSipasKodi(kodNiveli, idNdermarrje);

            clsNivelRegjistrimi nivelRegj = new clsNivelRegjistrimi();
            nivelRegj.Kodi = kodNiveli;
            nivelRegj.IdNdermarje = idNdermarrje;
            nivelRegj.merrNivelRegjSipasKodi();
            idNivel = nivelRegj.IdNivel;

            if (dateDokumenti.Year != clsNdermarrjeViti.ktheKodVitiSipasIdNdermViti(idNderViti))
                return new clsMesazh(false, rm.GetString("msgDataNukPerketVititUshtrimor", ci));

            int idLlog = 0;
            if (nrLlogari == "")
                return new clsMesazh(false, "Plotesoni llogarine kunderparti!");
            if (!clsLlogari.ekzistonLlogari(nrLlogari, idNdermarrje))
                return new clsMesazh(false, "Llogaria kunderparti nuk ekziston!");
            idLlog = clsLlogari.mbushIDLlogariSipasKodit(nrLlogari, idNdermarrje);


            if (nivelRegj.IdKategori != konfigAmbjenti.IdKategori || nivelRegj.IdNivel != konfigAmbjenti.IdNivel)
                return new clsMesazh(false, "Ky lloj dokumenti nuk i perket nenkategorise se zgjedhur!");

            int idStandarti = clsStandarteAmortizim.merrIDStandartinAmortizimitTeNdermarrjesSipasPershkrimit(llojStandarti, idNdermarrje);

            String mesazhGabimi;
            if (!DbCore.clsFunksione.checkPeriudheKontabel(out mesazhGabimi, dateDokumenti, periudha, idSt))
            {
                return new clsMesazh(false, mesazhGabimi);
            }

            return krijoDokumentAmortizimiPerAmortizimFillestar(nrDok, idNivel, konfigAmbjenti.IdKonfigAmbjente, dateDokumenti, dateAmortizimi, idMag, idStandarti, shenime, idNderViti, idNdermarrje, idPerdoruesi, dateRegjistrimi, idstatusdok, idLlog, colTrupiKrijuar, seriale, amortizimifillestar, idkrijuesi, out mesazhmevonshem);
        }

        /// <summary>
        /// MODULI ASETE:
        /// Krijon dokumentin e amortizimit per dokumentin e rivleresimit te asetit.
        /// </summary>
        /// <param name="nrDok">(string) Numri i dokumentit qe po krijohet.</param>
        /// <param name="idNiveli">(int) Id e nekategorise se dokumentit.</param>
        /// <param name="idKonfigurimAmbjenti">(int) Id e llojit te dokumentit.</param>
        /// <param name="dateDokumenti">(DateTime) Data e dokumentit te krijuar te amortizimit.</param>
        /// <param name="dateAmortizimi">(DateTime) Data e amortizimit te dokumentit.</param>
        /// <param name="idNjesiAdministrative">(int) Id e njesise administrative ku po behet regjistrimi i amortizimit.</param>
        /// <param name="idLlojStandarti">(int) Id e llojit te standartit.</param>
        /// <param name="shenime">(string) Shenime te ndryshme qe vendosen ne ambjentit e ruajtjes te amortizimit.</param>
        /// <param name="idNderViti">(int) Id e lidhjes se ndemarrjes me vitin.</param>
        /// <param name="idNdermarrje">(int) Id e ndermarrjes ku po kryhet amortizimi.</param>
        /// <param name="idPerdoruesi">(int) Id e perdoruesit qe po kryhen amortizimin.</param>
        /// <param name="colTrupiKrijuar">(colAmortizimiTrupi) Lista e trupit te krijuar ne griden e amortizimit fillestar.</param>
        /// <param name="dateRegjistrimi">data e regjistrimit te dokumentit</param>
        /// <param name="gjeneromagazine"> tregon nese dokumenti do gjeneroje dokument magazine apo jo</param>
        /// <param name="idllogari">id e llogarise kunderparti</param>
        /// <param name="idstatusdok">id e statusit te dokumentit</param>
        /// <param name="konfmag"> konfigurimi i magazines</param>
        /// <param name="krijoTrupTani">eshte variabel boolean qe tregon nese do krijohet trupi i dokumentit tani apo me vone. me vone duhet per rastet e modifikimit sepse duhet te fshihet njehere dokumenti ekzistues pastaj te behen llogaritjet sepse ndryshe jepte vlera gabim</param>
        /// <param name="ndryshovleraseriale">perdoret per te ndryshuar vlerat e serialeve sipas rivleresimit vetem kur behet dokumenti i standartit te pare dhe jo per standartet e tjera</param>
        /// <param name="seriale">serialet e magazines per te cilat do kryhen veprimet eshte bosh dhe mbushet gjate krijimit te trupit</param>
        /// <returns>Kthen True nese krijimi kryhet me sukses, ne te kundert False.</returns>
        public clsMesazh krijoDokumentAmortizimiPerRivleresim(string nrDok, int idNiveli, int idKonfigurimAmbjenti, DateTime dateDokumenti, DateTime dateAmortizimi, int idNjesiAdministrative, int idLlojStandarti, string shenime, int idNderViti, int idNdermarrje, int idPerdoruesi, DateTime dateRegjistrimi, int idstatusdok, int idllogari, colAmortizimiTrupiAbstract colTrupiKrijuar, DbShare.clsKonfigurimAmbjenti konfmag, bool gjeneromagazine, colSerialetMagazine seriale, bool ndryshovleraseriale, bool krijoTrupTani, bool rivleresimXStandart, out string mesazhmevonshem)
        {
            mesazhmevonshem = "";
              clsMesazh pergjigja = new clsMesazh(true, "U llogarit ne rregull!");
            clsDatabazeAsete dbasete = new clsDatabazeAsete();
            clsDatabaseShare dbshare = new clsDatabaseShare(dbasete);
            clsDatabaseRegjistrim dbregj = new clsDatabaseRegjistrim(dbasete);
            String konfigurimi = clsKonfigurimAmbjenti.ktheKodKonfigurimi(idKonfigurimAmbjenti, dbshare);
            clsKokaMagazina koka = new clsKokaMagazina(idNiveli, nrDok, dateDokumenti);
            //colSerialetPerRivleresim serialetXRivleresim;
            int krijuesi = koka.IdKrijuesi;
            if (krijuesi == 0) krijuesi = idPerdoruesi;
            // clsAmortizimiKoka.merrKrijuesin(nrDok, idNdermarrje, dateDokumenti, konfigurimi, dbregj);
            //if (krijuesi == 0) krijuesi = idPerdoruesi;
            //Lloji i dokumentit FRA
            filloObjektin(nrDok, idNiveli, idKonfigurimAmbjenti, dateDokumenti, dateAmortizimi, dateRegjistrimi, idNjesiAdministrative, idLlojStandarti, 0, idstatusdok, shenime, idNderViti, idNdermarrje, idPerdoruesi, krijuesi, DateTime.Now, 0, 0, 0, 0, idllogari, 0);
            colTrupi = colTrupiKrijuar;
           
            if (krijoTrupTani)
                pergjigja = krijoTrupiDokumentAmortizimiPerRivleresim(false, konfmag, seriale, ndryshovleraseriale, rivleresimXStandart, out mesazhmevonshem);
            if (rivleresimXStandart)
            {
                rivleresimKoka = new clsSerialPerRivleresimKoka(nrDok, idNiveli, idKonfigurimAmbjenti, dateDokumenti, dateRegjistrimi, idNjesiAdministrative, shenime, idNderViti, idLlojStandarti, idstatusdok, idNdermarrje, idPerdoruesi, krijuesi, DateTime.Now, idAmortizimi, idNiveli, idKonfigurimAmbjenti, 0, idllogari, colTrupi, dbasete);
            }
            
             
            if (gjeneromagazine)
                oKokaMagazina = krijoMagazineNgaRivleresimi(this, konfmag, seriale, ndryshovleraseriale, dbasete);
            else oKokaMagazina = new clsKokaMagazina();
            return pergjigja;
        }

        /// <summary>
        /// krijon trupin e dokumentit te rivleresimit. eshte ndare per arsye se gjate modifikimit duhet te kryhen veprimet pas fshirjes te dokumentit ekzistues
        /// </summary>
        /// <param name="dbasete">clsdatabaze asete per rastet e transaksionit</param>
        /// <param name="gjeneromagazine">tregon nese do gjenerohet dokument magazine apo jo</param>
        /// <param name="konfmag">konfigurimi i dokumentit te magazines</param>
        /// <param name="seriale">koleksioni i serilaeve i cili mbushet gjate krijimit te trupi</param>
        /// <param name="ndryshovleraseriale">tregon nese do ndryshohen vlerat e serialeve. duhet vetem per standartin e pare dhe jo per te tjeret</param>
        /// <returns>kthen mesazh nese trupi eshte krijuar ne rregull</returns>
        public clsMesazh krijoTrupiDokumentAmortizimiPerRivleresim(bool gjeneromagazine, DbShare.clsKonfigurimAmbjenti konfmag, colSerialetMagazine seriale, bool ndryshovleraseriale, bool rivleresimXStandart, out string mesazhmevonshem)
        {
            clsMesazh pergjigja = new clsMesazh(true, "U llogarit ne rregull!");
            pergjigja = colTrupi.krijoTrupiDokAmortizimiPerRivleresim(0, this, rivleresimXStandart,out mesazhmevonshem);
            if (!pergjigja.Status)
                return pergjigja;
            amortizimiShteseTotal = colTrupi.Sum(item => item.AmortizimiShtese);
            if (gjeneromagazine)
                oKokaMagazina.OcolTrupiMagazina = ruajTrupinMagazines(colTrupi, idStatusDokumenti, idNdermarrje, seriale, ndryshovleraseriale, oKokaMagazina.DtDok);
            return pergjigja;
        }

        /// <summary>
        /// krijon dokumentin e magazines per rivleresimin
        /// </summary>
        /// <param name="koka">koka e dokumentit te rivleresimit</param>
        /// <param name="konfmag">konfigurimi i dokumentit te magazines</param>
        /// <param name="seriale">serialet qe do mbushen gjate krijimit te trupit</param>
        /// <param name="ndryshovleraseriale">tregon nese do duhet te ndryshohen vlerat e serialeve, duhet vetem per standartin e pare dhe jo per te tjeret </param>
        /// <param name="dbasete">clsdatabase asete per rastet e transaksionit</param>
        /// <returns>kthen dokumentin e magazines</returns>
        private clsKokaMagazina krijoMagazineNgaRivleresimi(clsAmortizimiKoka koka, DbShare.clsKonfigurimAmbjenti konfmag, colSerialetMagazine seriale, bool ndryshovleraseriale, clsDatabazeAsete dbasete)
        {
            clsMesazh mesazh = new clsMesazh();
            string shenime = "";
            if (koka.Shenime != String.Empty) shenime = koka.Shenime;
            else
            {
                shenime = "Nga rivleresimi i amortizimit";
            }
            colTrupiMagazina coltrupi = ruajTrupinMagazines( (koka.ColTrupi.Count!=0?koka.colTrupi:koka.colTrupiRezerva), koka.idStatusDokumenti, koka.idNdermarrje, seriale, ndryshovleraseriale, koka.dateDokumenti);
            int magazina = 0;
            if (coltrupi.Count > 0)
                magazina = coltrupi[0].IdMag;
            bool magnjejte = true; double shuma = 0;
            foreach (clsTrupiMagazina tt in coltrupi)
            {
                shuma += tt.Vlefta;
                if (magazina != tt.IdMag)
                    magnjejte = false;
            }
            if (!magnjejte)
                magazina = 0;
            clsNjesiAdministrative njesi = new clsNjesiAdministrative(magazina);
            if (magazina == 0)
                njesi.Kodi = "";
            clsKokaMagazina kokamg = new clsKokaMagazina();
           
            mesazh = kokamg.krijoMagazine(false,  0, konfmag.IdNivel, konfmag.IdKonfigAmbjente, 0, "", magazina, njesi.Kodi, koka.dateDokumenti, koka.NrDok, 1, "", konfmag.IdKategori, 0, 0, koka.IdStatusDokumenti, koka.IdNdermarrje, koka.idNderViti, koka.IdPerdoruesi, koka.dateRegjistrimi, 1, shenime, koka.IdNiveli, koka.idKonfigurimAmbjenti, koka.idAmortizimi, 0, "", 0, "", 0, "", false, 0, 0, 0, "", "", "", coltrupi, new clsKokaMagazina(), new clsKokaFleteKontabel(), new clsKokaRezervime(), new clsDatabaseRegjistrim(), true, 0, "", 0, true, koka.IdPerdoruesi, koka.dateDokumenti, 0, "", string.Empty, string.Empty, 0, null,false,false,"","",0);
            if (!mesazh.Status)
                throw new Exception(mesazh.PershkrimMesazhi);
            return kokamg;
        }

        /// <summary>
        /// krijon trupin e magazines per dokumentin e gjeneruar nga rivleresimi i aseteve
        /// </summary>
        /// <param name="trupish">trupi i dokumentit te amortizimit</param>
        /// <param name="idstatusdok">id e statusit te dokumentit</param>
        /// <param name="idndermarje">idndermarje</param>
        /// <param name="seriale">serialet qe do krijohen </param>
        /// <param name="ndryshovleraseriale">tregon nese do ndryshohet vlerat e serialet, duhet vetem per standartin e pare dhe jo per te tjerat</param>
        /// <param name="dbasete">clsdatabaze asete per rastet e transaksioneve</param>
        /// <returns>kthen trupin e dokumentit te magazines</returns>
        private colTrupiMagazina ruajTrupinMagazines(colAmortizimiTrupiAbstract trupish, int idstatusdok, int idndermarje, colSerialetMagazine seriale, bool ndryshovleraseriale, DateTime data)
        {

            colTrupiMagazina trupat = new colTrupiMagazina();
            clsTrupiMagazina trupi;

            for (int i = 0; i < trupish.Count; i++)
            {
                DbInventari.clsArtikulli art = new DbInventari.clsArtikulli(trupish[i].IdArtikulli);
                trupi = new clsTrupiMagazina(0, 0, 1, trupish[i].IdArtikulli, art.KodArtikulli, art.PershkrimArtikulli, art.Njesi1Artikulli, 0, trupish[i].VleftaPlusMinus, trupish[i].VleftaPlusMinus, 1, 1, 0, 0, trupish[i].IdNjesiAdministrative, data, idstatusdok, 0, -1, 0, 0, -1, 0, 0, 0, 0, 0, 0, art, "",0,0);
                if (ndryshovleraseriale)///kur eshte trup krijohen serialet qe do preken nga ky dokument dhe iu vendosen vlerat e reja sipas rivleresimit, duhen vetem per standartin e pare dhe jo per te tjerat sepse do perseriteshin serialet dhe vlerat do dilnin keq
                {
                    clsSerialetMagazine serialmag = new clsSerialetMagazine();
                    serialmag.ktheSerialetMagazineSipasIDSerialDokFunditmerrVepriminFundit(trupish[i].IdAQTSeriali, idndermarje, data, 0);

                    serialmag.Vlefta = trupish[i].VleftaGjendje + trupish[i].VleftaPlusMinus;
                    serialmag.Cmimi = serialmag.Sasia != 0 ? (serialmag.Vlefta / serialmag.Sasia) : 0;
                    serialmag.NrRendor = i;
                    seriale.Add(serialmag);
                }
                trupat.Add(trupi);

            }
            return trupat;
        }

        /// <summary>
        /// MODULI ASETE:
        /// Krijon dokumentin e amortizimit per shperndarjen e shpenzimeve qe i behet dokumentit te blerjes.
        /// </summary>
        /// <param name="kokamag">dokumenti i magazines i krijuar gjate shperndarjes se shpenzimeve</param>
        /// <param name="idLlojStandarti">(int) Id e llojit te standartit.</param>
        /// <param name="iddoknga"> id e dokumentit nga vjen gjate modifikimit</param>
        /// <param name="konfamortizimi"> konfigurimi i dokumentit te magazines</param>
        /// <param name="colseriale">koleksion bosh me seriale i cili mbushet gjate krijimit te dokumentit</param>
        /// <param name="trupiPerKetedokmag"> pjesa e trupit te shperndarjes se shpenzimeve qe i perkasin kesaj date dhe do perdoret per gjenerimin e ketij dokumenti amortizimi</param>
        /// <param name="dbasete">(clsDatabazeAsete) Merr objektin e transaksionit qe te lidhi veprimet me njera tjetren.</param>
        /// <returns>Kthen True nese krijimi kryhet me sukses, ne te kundert False.</returns>
        public clsMesazh krijoDokumentAmortizimiPerShperndarjeShpezime(DbRegjistrim.clsKokaMagazina kokamag, int idLlojStandarti, DbShare.clsKonfigurimAmbjenti konfamortizimi, int iddoknga, int nrRenditje, List<clsShperndarjeShpenzimeTrupi> trupiPerKetedokmag, colSerialetMagazine colseriale)
        {
            //Lloji i dokumentit FASS
            filloObjektin(kokamag.NrDok, konfamortizimi.IdNivel, konfamortizimi.IdKonfigAmbjente, kokamag.DtDok, kokamag.DtDok, kokamag.DtRegjistrimi, kokamag.IdMagazina, idLlojStandarti, 0, kokamag.IdStatusDok, kokamag.Shenime, kokamag.IdNdermarrjeVit, kokamag.IdNdermarrje, kokamag.IdPerdoruesi, kokamag.IdPerdoruesi, DateTime.Now, kokamag.IdKokaMagazina, kokamag.IdNivel, kokamag.IdKonfigAmbjente, iddoknga, 0, nrRenditje);
            clsMesazh pergjigja = colTrupi.krijoTrupiDokAmortizimiPerShperndarjeShpenzimesh(this, trupiPerKetedokmag, kokamag, colseriale);
            if (!pergjigja.Status)
                return pergjigja;
            amortizimiShteseTotal = colTrupi.Sum(item => item.AmortizimiShtese);
            return pergjigja;
        }

        /// <summary>
        /// rillogarti amortizimin e aseteve
        /// </summary>
        /// <param name="gjitheKokat"></param>
        /// <param name="idNdermarrje">id e ndermarjes</param>
        /// <param name="idLlojStandarti">lloji i standartit per te cilin behet amortizimi</param>
        /// <param name="dataFillimiRillogaritje">data e fillimit te rillogaritjes</param>
        /// <param name="serialePerRillogaritje"> koleksioni me seriale per te cilat do behet rillogaritja</param>
        /// <param name="idPerdoruesi">id e perdoruesit</param>
        /// <param name="meSerial">True nese llogaritja do te behet per artikujt me serial dhe False nese llogaritja do te behet per artikujt pa serial</param>
        /// <param name="e"> progresbari per te ecur gjate rillogaritjes </param>
        /// <returns>kthen true ose false nqs rillogaritja eshte kryer me sukses</returns>
        public clsMesazh rillogaritAmortizim(colAmortizimiKoka gjitheKokat, int idNdermarrje, int idLlojStandarti,string StandartiEmertim, DateTime dataFillimiRillogaritje, colAQTSeriale serialePerRillogaritje, int idPerdoruesi, bool meSerial, EO.Web.ProgressTaskEventArgs e, ResourceManager rm, CultureInfo ci)
        {

            clsMesazh pergjigje = new clsMesazh();
            try
            {
                //using (var scope = new MyTransactionScope())
                //{
                    colAmortizimiTrupiAbstract amortizimiTrupiIMundurPerRillogaritje = new colAmortizimiTrupi();
                    colAmortizimiTrupiAbstract amortizimiTrupiIMundurPerRillogaritjeNew = new colAmortizimiTrupi();
                    colAmortizimiTrupiAbstract amortizimiTrupiIMundurPerRillogaritjeRez = new colAmortizimiTrupiRezerva();
                    colAmortizimiTrupiAbstract amortizimiTrupiIMundurPerRillogaritjeNewRez = new colAmortizimiTrupiRezerva();
                    if (meSerial)
                    {
                        amortizimiTrupiIMundurPerRillogaritje.merrAmortizimTrupiPerRillogaritje(idNdermarrje, dataFillimiRillogaritje, idLlojStandarti);
                        amortizimiTrupiIMundurPerRillogaritjeNew.AddRange(amortizimiTrupiIMundurPerRillogaritje.Where(p => serialePerRillogaritje.Any(p2 => p2.IdAQTSerial == p.IdAQTSeriali)));
                        amortizimiTrupiIMundurPerRillogaritjeRez.merrAmortizimTrupiPerRillogaritje(idNdermarrje, dataFillimiRillogaritje, idLlojStandarti);
                        amortizimiTrupiIMundurPerRillogaritjeNewRez.AddRange(amortizimiTrupiIMundurPerRillogaritjeRez.Where(p => serialePerRillogaritje.Any(p2 => p2.IdAQTSerial == p.IdAQTSeriali)));
                        pergjigje = colAmortizimiTrupiAbstract.rillogariTrupinAmortizimArtikuj(gjitheKokat, idNdermarrje, idLlojStandarti, StandartiEmertim, dataFillimiRillogaritje, idPerdoruesi, meSerial, e, rm, ci, amortizimiTrupiIMundurPerRillogaritjeNew, amortizimiTrupiIMundurPerRillogaritjeNewRez);
                    }
                    else
                    {
                        //e.UpdateProgress(50);
                        amortizimiTrupiIMundurPerRillogaritje.merrAmortizimTrupiPerRillogaritjeArtikujPaSerial(idNdermarrje, dataFillimiRillogaritje, idLlojStandarti);
                        amortizimiTrupiIMundurPerRillogaritjeNew.AddRange(amortizimiTrupiIMundurPerRillogaritje.Where(p => serialePerRillogaritje.Any(p2 => p2.IdAQTSerial == p.IdPrindFillestar)));

                        amortizimiTrupiIMundurPerRillogaritjeRez.merrAmortizimTrupiPerRillogaritjeArtikujPaSerial(idNdermarrje, dataFillimiRillogaritje, idLlojStandarti);
                        amortizimiTrupiIMundurPerRillogaritjeNewRez.AddRange(amortizimiTrupiIMundurPerRillogaritjeRez.Where(p => serialePerRillogaritje.Any(p2 => p2.IdAQTSerial == p.IdPrindFillestar)));


                        pergjigje = colAmortizimiTrupiAbstract.rillogariTrupinAmortizimArtikuj(gjitheKokat, idNdermarrje, idLlojStandarti, StandartiEmertim, dataFillimiRillogaritje, idPerdoruesi, meSerial, e, rm, ci, amortizimiTrupiIMundurPerRillogaritjeNew, amortizimiTrupiIMundurPerRillogaritjeNewRez);
                    }
                    
                    //scope.Complete();
                    return pergjigje;
                //}
            }
            catch (MyException m)
            {
                throw m;
            }
            catch (Exception ex)
            {
                return pergjigje;
            }
        }

        #endregion

        /// <summary>
        /// MODULI ASETE:
        /// Merr dokumentin e amortizimit ne standartin specifik sipas parametrave te kerkuar.
        /// </summary>
        /// <param name="idkonfigambjente">(int) Id e nenkategorise qe do te perdoret per fleten e amortizimit FA.</param>
        /// <param name="dateDokumenti">(DateTime) Data e dokumentit te amortizimit.</param>
        /// <param name="idNjesiAdministrative">(int) Id e njesise administrative per te cilen po behet regjistrimi i dokumentit te amortizimit.</param>
        /// <param name="idLlojStandarti">(int) Id e llojit te standartit per te cilen po behet llogaritja e amortizimit.</param>
        /// <param name="idNdermarrje">(int) Id e ndermarrjes per te cilen po behet regjistrimi i amortizimit.</param>
        /// <param name="moduliAsete">(clsDatabazeAsete) Merr objektin e transaksionit qe te lidhi veprimet me njera tjetren.</param>
        /// <returns>Kthen True nese nuk ndodh asnje gabim gjate marrjes se dokumentit te amortizimit ose False ne te kundert.</returns>
        public bool merrAmortizimKokaSipasMagDateNenKatDok(int idkonfigambjente, DateTime dateDokumenti, int idNjesiAdministrative, int idLlojStandarti, int idNdermarrje, clsDatabazeAsete moduliAsete)
        {

            bool pergjigja = mbushAmortizimKokaObjekt(moduliAsete.ktheAmortizimKokaSipasMagDateNenKatDok(idkonfigambjente, dateDokumenti, idNjesiAdministrative, idLlojStandarti, idNdermarrje));

            return pergjigja;
        }

        /// <summary>
        /// MODULI ASETE:
        /// Merr id e kokes se dokumentit te amortizimit ne standartin specifik sipas parametrave te kerkuar.
        /// </summary>
        /// <param name="idNiveli">(int) Id e nenkategorise qe do te perdoret per fleten e amortizimit FA.</param>
        /// <param name="dateDokumenti">(DateTime) Data e dokumentit te amortizimit.</param>
        /// <param name="idNjesiAdministrative">(int) Id e njesise administrative per te cilen po behet regjistrimi i dokumentit te amortizimit.</param>
        /// <param name="idLlojStandarti">(int) Id e llojit te standartit per te cilen po behet llogaritja e amortizimit.</param>
        /// <param name="idNdermarrje">(int) Id e ndermarrjes per te cilen po behet regjistrimi i amortizimit.</param>
        /// <returns>Kthen id e kokes se amortizimit nese nuk ndodh asnje gabim gjate marrjes se dokumentit te amortizimit ose False ne te kundert.</returns>
        public static int merrIDAmortizimKokaSipasMagDateNenKatDok(int idNiveli, DateTime dateDokumenti, int idNjesiAdministrative, int idLlojStandarti, int idNdermarrje)
        {
            clsDatabazeAsete moduliAsete = new clsDatabazeAsete();
            int pergjigja = moduliAsete.ktheIDAmortizimKokaSipasMagDateNenKatDok(idNiveli, dateDokumenti, idNjesiAdministrative, idLlojStandarti, idNdermarrje);
            moduliAsete.Dispose();
            return pergjigja;
        }

        public static int merrKrijuesin(String nrdok, int idnderm, DateTime data, String lloji, clsDatabaseRegjistrim db)
        {

            int idkrijuesi = db.merrKrijuesinAmortizimi(nrdok, idnderm, data, lloji, 0);

            return idkrijuesi;
        }

        /// <summary>
        /// Metode publike per krijimin e nje objekt klone me hapesira pointimi te ndryshe nga objekti qe po e krijon
        /// </summary>
        /// <returns>Kthen nje objekt identik me ate qe po e krijon</returns>
        public object Clone()
        {
            return this.MemberwiseClone();
        }

        #endregion

        #region Metoda Private

        /// <summary>
        /// MODULI ASETE:
        /// Krijon objektin e plote te klases, qe te perdoret nga te gjitha metodat qe mbushin objekti.
        /// </summary>
        /// <param name="nrDok">(string) Numri i dokumentit qe po krijohet.</param>
        /// <param name="idNiveli">(int) Id e nekategorise se dokumentit.</param>
        /// <param name="idKonfigurimAmbjenti">(int) Id e llojit te dokumentit.</param>
        /// <param name="dateDokumenti">(DateTime) Data e dokumentit te krijuar te amortizimit.</param>
        /// <param name="dateAmortizimi">(DateTime) Data e amortizimit te dokumentit.</param>
        /// <param name="dateRegjistrimi">(DateTime) Data e regjistrimit te dokumentit te amortizimit.</param>
        /// <param name="idNjesiAdministrative">(int) Id e njesise administrative ku po behet regjistrimi i amortizimit.</param>
        /// <param name="idLlojStandarti">(int) Id e llojit te standartit.</param>
        /// <param name="amortizimiShteseTotal">(double) Amortizmi total shtese i dokumentit te amortizimit.</param>
        /// <param name="idStatusDokumenti">(int) Id e statusit te dokumentit se ne cfare gjendje eshte dokumenti i amortizimit, i ruajtur, fshire, apo modifikuar.</param>
        /// <param name="shenime">(string) Shenime te ndryshme qe vendosen ne ambjentit e ruajtjes te amortizimit.</param>
        /// <param name="idNderViti">(int) Id e lidhjes se ndemarrjes me vitin.</param>
        /// <param name="idNdermarrje">(int) Id e ndermarrjes ku po kryhet amortizimi.</param>
        /// <param name="idPerdoruesi">(int) Id e perdoruesit qe po kryhen amortizimin.</param>
        /// <param name="idKrijuesi">(int) Id e perdoruesit qe ka krijuar amortizimin.</param>
        /// <param name="dtModifikimi">(DateTime) Data e modifikimit te fundit te objektit.</param>
        /// <param name="idDokGjenerues">(int) Id e dokumentit nga po gjenerohet dokumenti i amortizimit.</param>
        /// <param name="idNivelGjenerues">(int) Id e nenkategorise se dokumentit qe ka gjeneruar dokumentin e amortizimit.</param>
        /// <param name="idKonfigGjenerues">(int) Id e llojit te dokumentit qe ka gjeneruar dokumentin e amoritizmit.</param>
        /// <param name="idDokNga">(int) Id e dokumentit nga vjen dokumenti i amortizimit.</param>
        /// <param name="idllogari">(int) Id e llogarise kunderparti.</param>
        private void filloObjektin(string nrDok, int idNiveli, int idKonfigurimAmbjenti, DateTime dateDokumenti, DateTime dateAmortizimi, DateTime dateRegjistrimi, int idNjesiAdministrative, int idLlojStandarti, int amortizimiShteseTotal, int idStatusDokumenti, string shenime, int idNderViti, int idNdermarrje, int idPerdoruesi, int idKrijuesi, DateTime dtModifikimi, int idDokGjenerues, int idNivelGjenerues, int idKonfigGjenerues, int idDokNga, int idllogari, int nrRenditje)
        {
            this.nrDok = nrDok;
            this.idNiveli = idNiveli;
            this.idKonfigurimAmbjenti = idKonfigurimAmbjenti;
            this.dateDokumenti = dateDokumenti;
            this.dateAmortizimi = dateAmortizimi;
            this.dateRegjistrimi = dateRegjistrimi;
            this.idNjesiAdministrative = idNjesiAdministrative;
            this.shenime = shenime;
            this.idNderViti = idNderViti;
            this.idLlojStandarti = idLlojStandarti;
            this.amortizimiShteseTotal = amortizimiShteseTotal;
            this.idStatusDokumenti = idStatusDokumenti;
            this.idNdermarrje = idNdermarrje;
            this.idPerdoruesi = idPerdoruesi;
            this.idKrijuesi = idKrijuesi;
            this.dtModifikimi = dtModifikimi;
            this.idDokGjenerues = idDokGjenerues;
            this.idNivelGjenerues = idNivelGjenerues;
            this.idKonfigGjenerues = idKonfigGjenerues;
            this.idDokNga = idDokNga;
            this.nrRenditje = nrRenditje;
            IdLlogKunderparti = idllogari;
            colTrupi = new colAmortizimiTrupi();
            colTrupiRezerva = new colAmortizimiTrupiRezerva();
            oFleteKontabel = new clsKokaFleteKontabel();
        }

        private clsMesazh gjeneroKontabilitetAmortizimi(int meKontabilizim, int idDokNgaFK, ref string shfaqmesazhapolupe, int iddokngaqk, DbQendraKosto.colTrupiQendraKosto trupivjeterqendra, int idPeriudha, int idkategoria, clsDatabaseKontabilitet dbKont)
        {
            clsMesazh pergjigja = new clsMesazh(true);

            string pershkrimDokKontabiliteti;

            if (shenime == "")
                pershkrimDokKontabiliteti = "Nga amortizimi";
            else
                pershkrimDokKontabiliteti = shenime;
            clsKokaFleteKontabel oFleteKontabel = new clsKokaFleteKontabel();
            pergjigja = clsKokaFleteKontabel.gjeneroKontabilizimAmortizim(IdAmortizimi, idNiveli, idKonfigurimAmbjenti, dateDokumenti, nrDok, amortizimiShteseTotal, idNdermarrje, idNderViti, idPerdoruesi, dateRegjistrimi, colTrupi, colTrupiRezerva, pershkrimDokKontabiliteti, idDokNgaFK, 21, idPeriudha, idkategoria, out shfaqmesazhapolupe, iddokngaqk, trupivjeterqendra, idLlojStandarti, idLlogKunderparti, dbKont, out oFleteKontabel);
            if (!pergjigja.Status)
            {

                return pergjigja;
            }

            if (oFleteKontabel.OColTrupi.Count > 0)
            {
                oFleteKontabel.IdGjenerues = IdAmortizimi;
                oFleteKontabel.NrDukumentiKokaFleteKontabel = nrDok;

                if (meKontabilizim == 1)
                    oFleteKontabel.Kontabilizuar = true;
                else
                    oFleteKontabel.Kontabilizuar = false;
                pergjigja = oFleteKontabel.Ruaj(dbKont);
                if (!pergjigja.Status)
                {

                    return pergjigja;
                }
            }
            return pergjigja;
        }
        /// <summary>
        /// MODULI ASETE:
        /// Ruan objektin e kokes se amortizimit.
        /// </summary>
        /// <param name="moduliAsete">(clsDatabazeAsete) Merr objektin e transaksionit qe te lidhi veprimet me njera tjetren.</param>
        /// <param name="meKontabilizim">(int) Nese ruajtja ka kontabilizim apo jo.</param>
        /// <param name="idDokNgaFK">(int) Merr id e dokumentit nga fleta kontabel.</param>
        /// <param name="shfaqmesazhapolupe">(string) Mesazhin qe duhet te shfaqe.</param>
        /// <param name="iddokngaqk">(int) Merr id e dokumentit nga qendra e kostos.</param>
        /// <param name="trupivjeterqendra">(DbQendraKosto.colTrupiQendraKosto) Merr id e dokumentit te qendres se kostos.</param>
        /// <param name="idPeriudha">(int) Merr id e periudhes.</param>
        /// <param name="idkategoria">(int) Merr id e kategorise.</param>
        /// <param name="seriale">serilet qe do ruhen</param>
        /// <param name="ngarivleresimi"> tregon nese vjen nga dokument rivleresimi</param>
        /// <returns>Kthen clsMesazh me atribut statusi True nese ruajtja kryhet me sukses ose False dhe mesazhin e gabimit ne te kundert.</returns>
        private clsMesazh ruajAmortizimiKokaTransaksion(int meKontabilizim, int idDokNgaFK, out string shfaqmesazhapolupe, int iddokngaqk, DbQendraKosto.colTrupiQendraKosto trupivjeterqendra, int idPeriudha, int idkategoria, colSerialetMagazine seriale, bool ngarivleresimi, out string mesazhmevonshem)
        {
            mesazhmevonshem = "";
               shfaqmesazhapolupe = "jo";
            clsDatabazeAsete moduliAsete = new clsDatabazeAsete();
            clsMesazh pergjigja = moduliAsete.ruajAmortizimiKoka(out idAmortizimi, nrDok, idNiveli, idKonfigurimAmbjenti, dateDokumenti, dateAmortizimi, dateRegjistrimi, idNjesiAdministrative, shenime, amortizimiShteseTotal, idNderViti, idLlojStandarti, idStatusDokumenti, idNdermarrje, idPerdoruesi, idKrijuesi, dtModifikimi, idDokGjenerues, idNivelGjenerues, idKonfigGjenerues, idDokNga, idLlogKunderparti, nrRenditje);
            if (pergjigja.Status)
            {
                #region Trupi i amortizimit

                foreach (clsAmortizimiTrupiAbstract rreshtTrupi in colTrupi)
                {
                    rreshtTrupi.IdAmortizimKoka = idAmortizimi;
                    if (rreshtTrupi.IdArtikull_LlojAmortizimi == -1)
                        return new clsMesazh(false, "Mungon norma e amortizimit per artikull " + rreshtTrupi.Artikull.KodArtikulli + "!"); ;
                    pergjigja = rreshtTrupi.ruaj();
                    if (!pergjigja.Status)
                    {
                        return pergjigja;
                    }
                    //Update fushen e serialit aqtSerialDataAmortizimfillestar nese kjo fushe eshte null dhe ne regjistrimin aktual ka amortizim shtese.
                    //Me vone
                    //////clsAQTSeriale seriali = new clsAQTSeriale();
                    //////seriali.merrAQTSerialSipasID(rreshtTrupi.IdAQTSeriali, moduliAsete);
                    //////if (rreshtTrupi.AmortizimiShtese != 0.0 && seriali.AqtSerialDataAmortizimfillestar == null)
                    //////{
                    //////    pergjigja = seriali.modifikimAQTSerialDataAmortizimfillestar(rreshtTrupi.DateAmortizimi.AddDays(-rreshtTrupi.DiteAmortizimi + 1), moduliAsete);
                    //////    if (!pergjigja.Status)
                    //////    {
                    //////        return pergjigja;
                    //////    }
                    //////}
                }
                foreach (clsAmortizimiTrupiAbstract rreshtTrupi in colTrupiRezerva)
                {
                    rreshtTrupi.IdAmortizimKoka = idAmortizimi;
                    if (rreshtTrupi.IdArtikull_LlojAmortizimi == -1)
                        return new clsMesazh(false, "Mungon norma e amortizimit per artikull " + rreshtTrupi.Artikull.KodArtikulli + "!"); ;
                    pergjigja = rreshtTrupi.ruaj();
                    if (!pergjigja.Status)
                    {
                        return pergjigja;
                    }
                   
                }

                #endregion

                #region Rivleresimi ne rastet per standart specifik

                if (ngarivleresimi)
                {
                    if (rivleresimKoka != null) //nqs ka dokument rivlersimi per standart specifik
                    {
                        pergjigja = rivleresimKoka.ruaj(idAmortizimi);
                        if (!pergjigja.Status)
                        {
                            return pergjigja;
                        }
                    }
                }

                #endregion

                #region Magazina nga Amortizimi

                if (oKokaMagazina.NrDok != null) //nqs ka dokument magazine
                {
                    clsDatabaseShare dbshare = new clsDatabaseShare();
                    oKokaMagazina.IdGjenerues = idAmortizimi;
                    var gjithmone = clsAlternativaKushti.getAlternativa(oKokaMagazina.IdKonfigAmbjente, "GJKGJ", dbshare) == "Po";
                    clsDatabaseRegjistrim dbRegj = new clsDatabaseRegjistrim();
                    pergjigja = oKokaMagazina.ruaj(false, 0, null, idPeriudha, "", 0, 0, dbRegj, out shfaqmesazhapolupe, 0, new DbQendraKosto.colTrupiQendraKosto(), false, seriale, new DbAsete.colSerialetMagazine(), new DbShare.clsKonfigurimAmbjenti(), new DbShare.clsKonfigurimAmbjenti(), 0, 0, new DbQendraKosto.colTrupiQendraKosto(), false, new clsKokaShitje(), ngarivleresimi, gjithmone, new colAmortizimiKoka(), 0, false, false, new colTrupiMagazina(), new colAmortizimiKoka(), new int[0], false, false, false,out mesazhmevonshem, null, false, false);
                    if (!pergjigja.Status)
                    {
                        return pergjigja;
                    }
                }

                #endregion

                #region Kontabilizimi nga Amortizimi

                if (idStatusDokumenti == 1 && meKontabilizim != 0)
                {
                    clsDatabaseKontabilitet dbKont = new clsDatabaseKontabilitet();
                    pergjigja = gjeneroKontabilitetAmortizimi(meKontabilizim, idDokNgaFK, ref shfaqmesazhapolupe, iddokngaqk, trupivjeterqendra, idPeriudha, idkategoria, dbKont);
                    if (!pergjigja.Status)
                        return pergjigja;
                }

                #endregion
            }
            return pergjigja;
        }

        /// <summary>
        /// MODULI ASETE:
        /// Fshin fleten kontabel te gjeneruar nga dokumenti i amortizimit.
        /// </summary>
        /// <param name="idPerdoruesi">(int) Id e perdoruesit qe ka modifikuar per here te fundit amortizimin e kokes.</param>
        /// <param name="idkategoria">(int) Merr id e kategorise.</param>
        /// <param name="dbkontab">(clsDatabaseKontabilitet) Merr objektin e transaksionit qe te lidhi veprimet me njera tjetren me modulin e kontabilitetit.</param>
        /// <returns>Kthen clsMesazh me atribut statusi True nese fshirja kryhet me sukses ose False dhe mesazhin e gabimit ne te kundert.</returns>
        private clsMesazh fshifleteKontabelAmortizimi(int idPerdoruesi, int idkategoria, clsDatabaseKontabilitet dbkontab)
        {
            clsMesazh pergjigja = new clsMesazh(true);
            clsKokaFleteKontabel newclsKokaFleteKontabel = new clsKokaFleteKontabel(idAmortizimi, idkategoria, dbkontab);///fleta kontabel e dokumentit
            if (newclsKokaFleteKontabel.NrDukumentiKokaFleteKontabel != null)
                OFleteKontabel = newclsKokaFleteKontabel;
            else
                OFleteKontabel = new clsKokaFleteKontabel();

            if (OFleteKontabel.IdKokaFleteKontabel != 0)/// nqs dokumenti eshte i kontabilizuar kalojme dokumentin me status fshire dhe nqs eshte me stornim krijojme dokumentin e kundert
            {
                pergjigja = OFleteKontabel.fshiupd(dbkontab);
                if (!pergjigja.Status)
                    return pergjigja;
            }
            return pergjigja;
        }

        /// <summary>
        /// fshin dokumentin e magazines te gjeneruar nga amortizimi
        /// </summary>
        /// <param name="idPerdoruesi">id e perdoruesit</param>
        /// <param name="dbregj">clsdatabase regjistrimi per rastet e transaksionit</param>
        /// <returns>mesazh qe tregon nese eshte fshire apo jo dokumenti i magazines</returns>
        private clsMesazh fshifleteMagazineAmortizimi(int idPerdoruesi, clsDatabaseRegjistrim dbregj)
        {

            clsMesazh pergjigja = new clsMesazh(true);
            oKokaMagazina = new clsKokaMagazina();
            oKokaMagazina.mbushKokaMagazinaSipasIDGjenerues(idAmortizimi, 1, idKonfigurimAmbjenti, dbregj);

            if (oKokaMagazina.IdKokaMagazina != 0)
            {
                bool kaveprimepas = false;
                pergjigja = oKokaMagazina.fshiMagazina(oKokaMagazina.IdKokaMagazina, idPerdoruesi, dbregj, false, false, false, new colSerialetMagazine(), out kaveprimepas, true);
                if (!pergjigja.Status)
                    return pergjigja;

            }

            return pergjigja;
        }

        /// <summary>
        /// Fshin dokumentin e rivlersimit per standart
        /// </summary>
        /// <param name="idPerdoruesi">id e perdoruesit</param>
        /// <param name="moduliAsete">clsDatabazeAsete per rastet e transaksionit</param>
        /// <returns>mesazh qe tregon nese eshte fshire apo jo dokumenti i magazines</returns>
        private clsMesazh fshiRivlersimKokaPerStandart(int idPerdoruesi)
        {

            clsMesazh pergjigja = new clsMesazh(true);
            rivleresimKoka = new clsSerialPerRivleresimKoka(idAmortizimi);
            clsDatabazeAsete moduliAsete = new clsDatabazeAsete();
            if (rivleresimKoka.IdKokaRivlersim != 0)
            {
                pergjigja = moduliAsete.modifikoRivleresimKokaStatus(rivleresimKoka.IdKokaRivlersim, idPerdoruesi);
            }

            return pergjigja;
        }

        /// <summary>
        /// MODULI ASETE:
        /// Fshin objektin e dokumentit te amortizimit duke i ndryshuar statusin e dokumentit rreshtit qe po fshihet.
        /// </summary>
        /// <param name="idPerdoruesi">(int) Id e perdoruesit qe ka modifikuar per here te fundit amortizimin e kokes.</param>
        /// <param name="idkategoria">(int) Merr id e kategorise.</param>
        /// <param name="moduliAsete">(clsDatabazeAsete) Merr objektin e transaksionit qe te lidhi veprimet me njera tjetren.</param>
        /// <param name="trupiRi">trupi i dokumentit te ri te amortizimit ne rastet e modifikimeve. duhet per te pare nese nje serial qe po fshihet ekziston ne dokumentin e ri</param>
        /// <returns>Kthen clsMesazh me atribut statusi True nese fshirja kryhet me sukses ose False dhe mesazhin e gabimit ne te kundert.</returns>
        internal clsMesazh fshiAmortizimKokaTransaksion(int idPerdoruesi, int idkategoria, colAmortizimiTrupiAbstract trupiRi, bool rivleresimXStandart)
        {
            clsDatabaseKontabilitet dbkontab = new clsDatabaseKontabilitet();
            clsDatabaseRegjistrim dbregj = new clsDatabaseRegjistrim();
            clsDatabazeAsete moduliAsete = new clsDatabazeAsete();
            this.colTrupi.merrAmortizimTrupiSipasIdKokaAmortizimi(this.idAmortizimi);
            clsMesazh pergjigja = kontrolloTrup(this.colTrupi, this.idKonfigurimAmbjenti, this.idLlojStandarti, trupiRi);
            if (!pergjigja.Status)
                return pergjigja;
            pergjigja = fshifleteKontabelAmortizimi(idPerdoruesi, idkategoria, dbkontab);
            if (!pergjigja.Status)
                return pergjigja;
            pergjigja = fshifleteMagazineAmortizimi(idPerdoruesi, dbregj);
            if (!pergjigja.Status)
                return pergjigja;
            if (rivleresimXStandart)
            {
                pergjigja = fshiRivlersimKokaPerStandart(idPerdoruesi);
                if (!pergjigja.Status)
                    return pergjigja;
            }
            pergjigja = moduliAsete.modifikoAmortizimKokaStatus(idAmortizimi, idPerdoruesi);
            return pergjigja;
        }

        /// <summary>
        /// kontrollon nese lejohet te fshihet apo jo ky serial. nese ka veprime te mevonshme
        /// </summary>
        /// <param name="colAmortizimiTrupi">trupi i dokumentit qe po fshihet</param>
        /// <param name="idkonfigambjente">konfigurimi sepse kontrollohet vetem per konfigurime te caktuara</param>
        /// <param name="idStandartAmortizimi">standarti i amortizimit</param>
        /// <param name="colAmortizimiTrupiIRI">trupi i dokumentit te ri ne rastet e modifikimit</param>
        /// <param name="moduliAsete">clsdatabaze asete per rastet e transaksionit</param>
        /// <returns>clsmesazh me true nqs nuk ka veprime dhe false nqs ka veprime</returns>
        private clsMesazh kontrolloTrup(colAmortizimiTrupiAbstract colAmortizimiTrupi, int idkonfigambjente, int idStandartAmortizimi, colAmortizimiTrupiAbstract colAmortizimiTrupiIRI)
        {
            clsMesazh kaveprime = new clsMesazh(true, "Nuk ka veprime!");
            //DbShare.clsKonfigurimAmbjenti konf = new clsKonfigurimAmbjenti(idkonfigambjente, moduliAsete );
            string kodKonfig = DbShare.clsKonfigurimAmbjenti.ktheKodKonfigurimi(idkonfigambjente, new clsDatabaseShare());
            if (kodKonfig == "FAB" || kodKonfig == "FAFanalitike" || kodKonfig == "FAFpermbledhese" || kodKonfig == "FANS")//per konfigurime te caktuara
            {
                foreach (clsAmortizimiTrupiAbstract trup in colAmortizimiTrupi)
                {
                    if (colAmortizimiTrupiIRI.Find(x => x.IdAQTSeriali == trup.IdAQTSeriali) == null)//nqs eksiton seriali ne trupin e ri ne rastet e modifikimit
                    {
                        bool veprime = clsAmortizimiTrupiAbstract.kaVeprimePasPerKeteSerial(trup.IdAQTSeriali, trup.DateAmortizimi, idStandartAmortizimi, trup.objektiKod);
                        //kontrollon nese ka veprime me vone
                        if (veprime)
                        {
                            clsAQTSeriale ser = new clsAQTSeriale();
                            ser.merrAQTSerialSipasID(trup.IdAQTSeriali);
                            return new clsMesazh(false, "Ka veprime te mevonshme me serialin " + ser.AqtSerialKod + "!");
                        }
                    }
                }
            }
            return kaveprime;
        }

        /// <summary>
        /// MODULI ASETE:
        /// Modifikon statusin e dokumentit te kokes se amortizimit ne transaksion.
        /// </summary>
        /// <param name="idPerdoruesi">(int) Id e perdoruesit qe ka modifikuar per here te fundit amortizimin e kokes.</param>
        /// <param name="idstatus">(int) Id e statusit te dokumentit se ne cfare gjendje eshte dokumenti i amortizimit, i ruajtur, fshire, apo modifikuar.</param>
        /// <param name="idkategoria">(int) Merr id e kategorise.</param>
        /// <param name="moduliAsete">(clsDatabazeAsete) Merr objektin e transaksionit qe te lidhi veprimet me njera tjetren.</param>
        /// <returns>Kthen clsMesazh me atribut statusi True nese modifikimi kryhet me sukses ose False dhe mesazhin e gabimit ne te kundert.</returns>
        public clsMesazh modifikoStatusAmortizimKokaTransaksion(int idPerdoruesi, int idstatus, int idkategoria)
        {
            clsMesazh pergjigja;
            clsDatabazeAsete moduliAsete = new clsDatabazeAsete();
            if (idstatus == 4 || idstatus == 2)
            {
                clsDatabaseKontabilitet dbkontab = new clsDatabaseKontabilitet();
                pergjigja = fshifleteKontabelAmortizimi(idPerdoruesi, idkategoria, dbkontab);
                if (!pergjigja.Status)
                    return pergjigja;
            }
            pergjigja = moduliAsete.modifikoAmortizimKokaStatus(idAmortizimi, idPerdoruesi, idstatus);
            return pergjigja;
        }

        private void mbushTeDhenaPerMagazinen(clsDatabaseShare dbshare, clsDatabaseRegjistrim dbregj, clsDatabaseInventari dbinv, clsDatabaseKontabilitet dbkont, DbCore.DbQendraKosto.clsDatabaseQendraKosto dbqendra, clsKokaMagazina mag, out int meKontabilizim, out string pershkrimFK, out bool gjithmone, int idkokamag)
        {
            mag.mbushKokaMagazinaSipasID(idkokamag, dbregj);
            mag.OcolTrupiMagazina.mbushGjitheTrupiMagazinaNgaKoka(mag.IdKokaMagazina, dbregj);


            colArtikujt colArt = new colArtikujt(mag.IdKokaMagazina, dbinv);
            int j2 = 0;
            foreach (clsTrupiMagazina trup in mag.OcolTrupiMagazina)
            {
                if (trup.IdLlojVeprimi == 1)
                    trup.Element = colArt[j2];
                j2++;
            }

            mag.OFleteKontabel = new clsKokaFleteKontabel(mag.IdKokaMagazina, 6, dbkont);
            mag.OFleteKontabel.KokaQendraKosto = new DbQendraKosto.clsKokaQendraKosto();

            mag.OFleteKontabel.KokaQendraKosto.KtheKokaQKSipasIDGjeneruesDheKonfigMeTrup(mag.OFleteKontabel.IdKokaFleteKontabel, mag.OFleteKontabel.IdKonfigAmbjente, dbqendra);
            mag.OMagazinaTransferim = new clsKokaMagazina();
            mag.OMagazinaTransferim.mbushKokaMagazinaSipasIDGjenerues(mag.IdKokaMagazina, 1, mag.IdKonfigAmbjente, dbregj);
            mag.OMagazinaTransferim.OcolTrupiMagazina.mbushGjitheTrupiMagazinaNgaKoka(mag.OMagazinaTransferim.IdKokaMagazina, dbregj);
            colArtikujt coleksistues1 = new colArtikujt(mag.OMagazinaTransferim.IdKokaMagazina, dbinv);
            int i2 = 0;
            foreach (clsTrupiMagazina trup in mag.OMagazinaTransferim.OcolTrupiMagazina)
            {
                if (trup.IdLlojVeprimi == 1)
                    trup.Element = coleksistues1[i2];
                i2++;
            }
            meKontabilizim = clsAlternativaKushti.getAlternativa(mag.IdKonfigAmbjente, "GJK", dbshare) != "Jo" ? 1 : 0;
            if (mag.Shenime != String.Empty)
                pershkrimFK = mag.Shenime;
            else if (mag.IdLlojDokumentiMagazine == 1)
                pershkrimFK = "Nga hyrjet e magazinës";
            else
                pershkrimFK = "Nga daljet e magazinës";
            gjithmone = false;

            if (clsAlternativaKushti.getAlternativa(mag.IdKonfigAmbjente, "GJKGJ", dbshare) == "Po")
                gjithmone = true;
        }
        private clsMesazh rikrijoKontabilitetinPerRillogaritjeAmortizimiMagazina(ResourceManager rm, CultureInfo ci, string shfaqmesazhapolupe, int idPeriudha, DbData dbData)
        {
            clsMesazh pergjigja = new clsMesazh(true);
            colAmortizimiKoka col = new colAmortizimiKoka();
            col.Add(this);
            clsDatabaseShare dbshare = new clsDatabaseShare(dbData);
            clsDatabaseRegjistrim dbregj = new clsDatabaseRegjistrim(dbData);
            clsDatabaseInventari dbinv = new clsDatabaseInventari(dbData);
            clsDatabaseKontabilitet dbkont = new clsDatabaseKontabilitet(dbData);
            DbCore.DbQendraKosto.clsDatabaseQendraKosto dbqendra = new DbQendraKosto.clsDatabaseQendraKosto(dbData);
            DbShare.clsKonfigurimAmbjenti konf = new clsKonfigurimAmbjenti(idKonfigGjenerues, dbshare);
            int idkokamag = 0;
            if (konf.IdKategori == 1 || konf.IdKategori == 2)
            {
                clsKokaMagazina magshitje = new clsKokaMagazina();
                magshitje.mbushKokaMagazinaSipasIDGjenerues(idDokGjenerues, konf.IdKategori == 1 ? 2 : 1, idKonfigGjenerues);
                idkokamag = magshitje.IdKokaMagazina;
            }

            if (konf.IdKategori == 6)
            {
                idkokamag = idDokGjenerues;
            }
            clsKokaMagazina mag = new clsKokaMagazina();


            int meKontabilizim;
            string pershkrimFK;
            bool gjithmone;
            mbushTeDhenaPerMagazinen(dbshare, dbregj, dbinv, dbkont, dbqendra, mag, out meKontabilizim, out pershkrimFK, out gjithmone, idkokamag);

            pergjigja = mag.OFleteKontabel.fshiupd(dbkont);//fshijme kontabilitetin ekzistues
            if (!pergjigja.Status)
            {
                return pergjigja;
            }
            pergjigja = clsKokaMagazina.GjeneroKontabilitet(mag.IdKokaMagazina, mag.IdNivel, mag.IdKonfigAmbjente, mag.DtDok, mag.NrDok, mag.Vlefta, mag.IdStatusDok, mag.IdNdermarrje, mag.IdNdermarrjeVit, mag.IdPerdoruesi, mag.DtRegjistrimi, mag.IdDegeAdministrative, mag.OcolTrupiMagazina, mag.OFleteKontabel, mag.OMagazinaTransferim, meKontabilizim, idPeriudha, dbregj, pershkrimFK, 6, mag.OFleteKontabel.IdKokaFleteKontabel, mag.IdLlogari, mag.IdNjesiVartese, mag.MeKonfirmim, ref shfaqmesazhapolupe, mag.OFleteKontabel.KokaQendraKosto.IdKoka, mag.OFleteKontabel.KokaQendraKosto.ColTrupi, gjithmone, col, pergjigja, mag.IdMagazina);
            if (!pergjigja.Status)
            {

                return pergjigja;
            }

            return pergjigja;
        }
        private void mbushTeDhenaPerAmortizimin(out clsKokaFleteKontabel newclsKokaFleteKontabel)
        {
            colTrupi = new colAmortizimiTrupi();
            colTrupi.merrAmortizimTrupiSipasIdKokaAmortizimi(idAmortizimi);
            colTrupiRezerva = new colAmortizimiTrupiRezerva();
            colTrupiRezerva.merrAmortizimTrupiSipasIdKokaAmortizimi(idAmortizimi);
            colArtikujt colart = new colArtikujt(IdAmortizimi, "Amortizim");
            for (int i = 0; i < colTrupi.Count; i++)
            {
                colTrupi[i].Artikull = colart[i];
            }
            for (int j = 0; j < colTrupiRezerva.Count; j++)
            {
                colTrupiRezerva[j].Artikull = colart.Find(x=>x.IdArtikulli==colTrupiRezerva[j].IdArtikulli);
            }
            newclsKokaFleteKontabel = new clsKokaFleteKontabel();
            newclsKokaFleteKontabel.MerrFleteSipasIdGjeneruesDheKonfigGjenerues(IdAmortizimi, IdKonfigurimAmbjenti, new clsDatabaseKontabilitet());

            newclsKokaFleteKontabel.KokaQendraKosto = new DbQendraKosto.clsKokaQendraKosto();
            newclsKokaFleteKontabel.KokaQendraKosto.KtheKokaQKSipasIDGjeneruesDheKonfig(newclsKokaFleteKontabel.IdKokaFleteKontabel, newclsKokaFleteKontabel.IdKonfigAmbjente, new DbQendraKosto.clsDatabaseQendraKosto());
            newclsKokaFleteKontabel.KokaQendraKosto.ColTrupi.mbushTrupiQendraKosto(newclsKokaFleteKontabel.KokaQendraKosto.IdKoka, new DbQendraKosto.clsDatabaseQendraKosto());
        }

        private clsMesazh rikrijoKontabilitetinPerRillogaritjeAmortizimi(ResourceManager rm, CultureInfo ci, DbData dbData)
        {
            clsMesazh pergjigja = new clsMesazh(true);
            string shfaqmesazhapolupe = "";

            clsKokaFleteKontabel newclsKokaFleteKontabel;

            mbushTeDhenaPerAmortizimin(out newclsKokaFleteKontabel);
            if (newclsKokaFleteKontabel.IdKokaFleteKontabel > 0)
            {
        
                pergjigja = newclsKokaFleteKontabel.Fshiupdam(dbData);//fshijme kontabilitetin ekzistues
                if (!pergjigja.Status)
                {

                    return pergjigja;
                }
                //rikrijojme kontabilitetin
                pergjigja = gjeneroKontabilitetAmortizimi(1, newclsKokaFleteKontabel.IdKokaFleteKontabel, ref shfaqmesazhapolupe, newclsKokaFleteKontabel.KokaQendraKosto.IdKoka, newclsKokaFleteKontabel.KokaQendraKosto.ColTrupi, newclsKokaFleteKontabel.IdPeriudha, newclsKokaFleteKontabel.IdKategoria, new clsDatabaseKontabilitet(dbData));
                if (!pergjigja.Status)
                {

                    return pergjigja;
                }
                if (idKonfigGjenerues != 0)
                {

                    pergjigja = rikrijoKontabilitetinPerRillogaritjeAmortizimiMagazina(rm, ci, shfaqmesazhapolupe, newclsKokaFleteKontabel.IdPeriudha, dbData);
                    if (!pergjigja.Status)
                        return pergjigja;
                }
            }
            return pergjigja;
        }
        /// <summary>
        /// MODULI ASETE:
        /// Modifikon objektin e rreshtit te kokes te amortizimit ne transaksion.
        /// </summary>
        /// <param name="moduliAsete">(clsDatabazeAsete) Merr objektin e transaksionit qe te lidhi veprimet me njera tjetren.</param>
        /// <returns>Kthen True nese modifikimi perfundon me sukses, ose False nese modifikimi jep gabim.</returns>
        private clsMesazh modifikoAmortizimiKokaTransaksionPerRillogaritje(ResourceManager rm, CultureInfo ci, DbData dbData)
        {
            clsDatabazeAsete moduliAsete = new clsDatabazeAsete(dbData);
            clsMesazh pergjigja = moduliAsete.modifikoAmortizimiKokaPerRillogaritje(idAmortizimi, amortizimiShteseTotal, idPerdoruesi);
            if (!pergjigja.Status)
                return pergjigja;
            if (idStatusDokumenti == 1)//rikrijojme dhe njehere kontabilitetin per dokumentat e rillogaritjes
            {
                pergjigja = rikrijoKontabilitetinPerRillogaritjeAmortizimi(rm, ci, dbData);
                if (!pergjigja.Status)
                    return pergjigja;
            }
            return pergjigja;
        }

        #endregion
    }
}
