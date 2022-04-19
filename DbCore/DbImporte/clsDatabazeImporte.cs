using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using DbCore.IMBUtils.DataBase;
using DbCore.IMBUtils.Messages;

namespace DbCore.DbImporte
{
    public class clsDatabazeImporte : DbData
    {

        /// <summary>
        /// Konstruktori bosh i klases se veprimeve me databazen clsDatabazeAsete.
        /// </summary>
        public clsDatabazeImporte()
            : base()
        {
        }
        public clsDatabazeImporte(DbData db) : base(db) { }
        public clsDatabazeImporte(string connectionName) : base(connectionName)
        {

        }

        #region Import koka flete kontabel

        /// <summary>
        /// IMPORTI FLETEVE KONTABEL:
        /// Ruan nepermjet store procedures ne kode fletet kontabel ne tabelat e importit.
        /// </summary>
        /// <returns>Kthen clsMesazh me atribut statusi True nese ruajtja kryhet me sukses ose False dhe mesazhin e gabimit ne te kundert.</returns>
        internal clsMesazh ruajKokaFleteKontabelPerImport(out int idKokaImport, int idKokaDokumentit, string nrKokaFleteKontabel, DateTime dateDokumentiFleteKontabel, string pershkrimiFleteKontabel, double vleftaFleteKontabel, string konfigAmbjenti, bool importuar, DateTime dateEksporti, DateTime dateImporti, int idNdermarrje)
        {
            idKokaImport = -1;
            dbManager.Open();
            dbManager.CreateParameters(11);
            dbManager.AddParameters(0, "@ID_KOKAIMPORT", idKokaImport, ParameterDirection.Output);
            dbManager.AddParameters(1, "@ID_DOKUMENTIMPORTUAR", idKokaDokumentit, ParameterDirection.Input);
            dbManager.AddParameters(2, "@NRKOKAFLETEKONTABEL", nrKokaFleteKontabel, ParameterDirection.Input);
            dbManager.AddParameters(3, "@DATEDOKUMENTIKOKAFLETEKONTABEL", dateDokumentiFleteKontabel, ParameterDirection.Input);
            dbManager.AddParameters(4, "@PERSHKRIMIFLETEKONTABEL", pershkrimiFleteKontabel, ParameterDirection.Input);
            dbManager.AddParameters(5, "@VLEFTAFLETEKONTABEL", vleftaFleteKontabel, ParameterDirection.Input);
            dbManager.AddParameters(6, "@KONFIGAMBJENTI", konfigAmbjenti, ParameterDirection.Input);
            dbManager.AddParameters(7, "@IMPORTUAR", importuar, ParameterDirection.Input);
            dbManager.AddParameters(8, "@DATE_EKSPORTI", dateEksporti, ParameterDirection.Input);
            dbManager.AddParameters(9, "@DATE_IMPORTIMI", dateImporti, ParameterDirection.Input);
            dbManager.AddParameters(10, "@IDNDERMARRJE", idNdermarrje, ParameterDirection.Input);

            dbManager.ExecuteNonQuery(CommandType.Text, @"INSERT INTO " + Properties.Settings.Default.DbCore_EmriDatabaze_Import + ".dbo." + Properties.Settings.Default.DbCore_EmriTabeles_ImportKokaFleteKontabel + @" 
(ID_DOKUMENTIMPORTUAR, NRKOKAFLETEKONTABEL, DATEDOKUMENTIKOKAFLETEKONTABEL, PERSHKRIMIFLETEKONTABEL, VLEFTAFLETEKONTABEL, KONFIGAMBJENTI, IMPORTUAR,
DATE_EKSPORTI, DATE_IMPORTIMI, IDNDERMARRJE)
VALUES (@ID_DOKUMENTIMPORTUAR, @NRKOKAFLETEKONTABEL, @DATEDOKUMENTIKOKAFLETEKONTABEL, @PERSHKRIMIFLETEKONTABEL, @VLEFTAFLETEKONTABEL, @KONFIGAMBJENTI, @IMPORTUAR,
@DATE_EKSPORTI, @DATE_IMPORTIMI, @IDNDERMARRJE) set @ID_KOKAIMPORT = @@identity");

            idKokaImport = int.Parse(dbManager.Parameters[0].Value.ToString());
            clsMesazh mesazh = new clsMesazh(true, "Ruatja perfundoi me sukses!");
            return mesazh;
        }

        internal int transferoPolicaNeTabelaImporti(string emerTabeleKoka, string emerTabeleTrupi, int idNdermarrje, int idPerdoruesi)
        {
            dbManager.Open();
            dbManager.CreateParameters(4);
            dbManager.AddParameters(0, "@EMERTABKOKA", emerTabeleKoka, ParameterDirection.Input);
            dbManager.AddParameters(1, "@EMERTABTRUPI", emerTabeleTrupi, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDNDERMARJE", idNdermarrje, ParameterDirection.Input);
            dbManager.AddParameters(3, "@IDPERDORUESI", idPerdoruesi, ParameterDirection.Input);
            return Convert.ToInt32(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_Integrim_lexoFSTNgaTabelaTempPolicash").ToString());
        }

        /// <summary>
        /// IMPORTI FLETEVE KONTABEL:
        /// Modifikon nepermjet store procedures ne kode fletet kontabel qe sapo u importuan sakte si te importuara.
        /// </summary>
        /// <returns>Kthen clsMesazh me atribut statusi True nese modifikimi kryhet me sukses ose False dhe mesazhin e gabimit ne te kundert.</returns>
        internal clsMesazh modifikoKokaFleteKontabelTeImportuara(int idKokaDokument, int idKokaImport)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@ID_DOKUMENTIMPORTUAR", idKokaDokument, ParameterDirection.Input);
            dbManager.AddParameters(1, "@ID_KOKAIMPORT", idKokaImport, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.Text, @"UPDATE " + Properties.Settings.Default.DbCore_EmriDatabaze_Import + ".dbo." + Properties.Settings.Default.DbCore_EmriTabeles_ImportKokaFleteKontabel
                + @" SET IMPORTUAR = 1, DATE_IMPORTIMI = GETDATE() WHERE ID_DOKUMENTIMPORTUAR = @ID_DOKUMENTIMPORTUAR AND ID_KOKAIMPORT = @ID_KOKAIMPORT");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgModifikimiMeSukses"]);
            return mesazh;
        }

        /// <summary>
        /// IMPORTI FLETEVE KONTABEL:
        /// Kthen nga databaza nepermjet store procedures ne kode nje DataTable me te gjitha kokat e fleteve kontabel te pa importuara.
        /// </summary>
        /// <returns>Kthen nje DataTable me kokat e importit te fleteve kontabel nga tabela T_TEMP_KOKA_IMPORT_FLETEKONTABEL.</returns>
        internal DataTable ktheKokaFleteKontabelTePaImportuara()
        {
            dbManager.Open();
            DataSet ds = dbManager.ExecuteDataSet(CommandType.Text, @"SELECT ID_KOKAIMPORT, ID_DOKUMENTIMPORTUAR, NRKOKAFLETEKONTABEL, DATEDOKUMENTIKOKAFLETEKONTABEL, PERSHKRIMIFLETEKONTABEL, VLEFTAFLETEKONTABEL, KONFIGAMBJENTI, 
IMPORTUAR, DATE_EKSPORTI, DATE_IMPORTIMI FROM " + Properties.Settings.Default.DbCore_EmriDatabaze_Import + ".dbo." + Properties.Settings.Default.DbCore_EmriTabeles_ImportKokaFleteKontabel + " WHERE IMPORTUAR = 0");
            return ds.Tables[0];
        }

        /// <summary>
        /// IMPORTI FLETEVE KONTABEL:
        /// Kthen nga databaza nepermjet store procedures ne kode nje DataTable me id e fundit te kokes se importit.
        /// </summary>
        /// <returns>Kthen nje int me id e fundit te kokes se importit te fleteve kontabel nga tabela T_TEMP_KOKA_IMPORT_FLETEKONTABEL.</returns>
        internal int ktheKokaFleteKontabelTeFundit()
        {
            dbManager.Open();
            DataSet ds = dbManager.ExecuteDataSet(CommandType.Text, @"SELECT TOP 1 ID_KOKAIMPORT, ID_DOKUMENTIMPORTUAR, NRKOKAFLETEKONTABEL, DATEDOKUMENTIKOKAFLETEKONTABEL, PERSHKRIMIFLETEKONTABEL, VLEFTAFLETEKONTABEL, KONFIGAMBJENTI, 
IMPORTUAR, DATE_EKSPORTI, DATE_IMPORTIMI FROM " + Properties.Settings.Default.DbCore_EmriDatabaze_Import + ".dbo." + Properties.Settings.Default.DbCore_EmriTabeles_ImportKokaFleteKontabel + @" ORDER BY ID_KOKAIMPORT DESC");
            if (ds == null)
                return -1;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return -1;

            int idKokaImport;
            int.TryParse(ds.Tables[0].Rows[0]["ID_KOKAIMPORT"].ToString(), out idKokaImport);
            return idKokaImport;
        }

        /// <summary>
        /// IMPORTI FLETEVE KONTABEL:
        /// Fshin fleten kontabel ne db te tabelat ndermjetese duke e bere fushen importuar 2.
        /// </summary>
        /// <returns>Kthen nje objekt clsMesazh me statusin true nqs eshte kryer veprimi me sukses dhe false ne te kundert.</returns>
        internal clsMesazh fshiKokaFleteKontabel(int idKokaFleteKont)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@ID_KOKAIMPORT", idKokaFleteKont, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_TEMP_KOKA_IMPORT_FLETEKONTABEL_updDel");
            return new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
        }

        /// <summary>
        /// IMPORTI FLETEVE KONTABEL:
        /// Kthen nga databaza nepermjet store procedures ne kode nje DataTable me id e fundit te kokes se importit.
        /// </summary>
        /// <returns>Kthen nje int me id e fundit te kokes se importit te fleteve kontabel nga tabela T_TEMP_KOKA_IMPORT_FLETEKONTABEL.</returns>
        internal string ktheNrFunditKokaFleteKontabel(DateTime dt, int idNdermarrje)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDNDERMARJE", idNdermarrje, ParameterDirection.Input);
            dbManager.AddParameters(1, "@DATEDOK", dt, ParameterDirection.Input);
            return (string)dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_TEMP_KOKA_IMPORT_FLETEKONTABEL_ktheNrFunditImportiSipasDates");
            //DataSet ds = dbManager.ExecuteDataSet(CommandType.Text, @"SELECT TOP 1 NRKOKAFLETEKONTABEL FROM " + Properties.Settings.Default.DbCore_EmriDatabaze_Import + ".dbo." + Properties.Settings.Default.DbCore_EmriTabeles_ImportKokaFleteKontabel + @"  WHERE DATEDOKUMENTIKOKAFLETEKONTABEL= " + dt + " ORDER BY ID_KOKAIMPORT DESC");
            //if (ds == null)
            //    return -1;
            //if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
            //    return -1;

            //int idKokaImport;
            //int.TryParse(ds.Tables[0].Rows[0]["ID_KOKAIMPORT"].ToString(), out idKokaImport);
            //return idKokaImport;
        }

        #endregion

        #region Import trupi flete kontabel

        /// <summary>
        /// IMPORTI FLETEVE KONTABEL:
        /// Ruan nepermjet store procedures ne kode fletet kontabel te trupit ne tabelat e importit.
        /// </summary>
        /// <returns>Kthen clsMesazh me atribut statusi True nese ruajtja kryhet me sukses ose False dhe mesazhin e gabimit ne te kundert.</returns>
        internal clsMesazh ruajTrupiFleteKontabelPerImport(int idTrupiImport, int idKokaImport, int idDokumentImporti, string nrLlogarise, string pershkrimTrupFleteKontabel, string monedha,
            double kursi, double vleftaDebiMonHuaj, double vleftaKrediMonHuaj, double vleftaDebiMonBaze, double vleftaKrediMonBaze)
        {
            idTrupiImport = -1;
            dbManager.Open();
            dbManager.CreateParameters(11);
            dbManager.AddParameters(0, "@ID_TRUPIIMPORT", idTrupiImport, ParameterDirection.Output);
            dbManager.AddParameters(1, "@ID_KOKA_IMPORT", idKokaImport, ParameterDirection.Input);
            dbManager.AddParameters(2, "@ID_DOKUMENTIMPORTUAR", idDokumentImporti, ParameterDirection.Input);
            dbManager.AddParameters(3, "@NRLLOGARI", nrLlogarise, ParameterDirection.Input);
            dbManager.AddParameters(4, "@PERSHKRIMITRUPFLETEKONTABEL", pershkrimTrupFleteKontabel, ParameterDirection.Input);
            dbManager.AddParameters(5, "@MONEDHA", monedha, ParameterDirection.Input);
            dbManager.AddParameters(6, "@KURSI", kursi, ParameterDirection.Input);
            dbManager.AddParameters(7, "@VLEFTADEBIMONHUAJ", vleftaDebiMonHuaj, ParameterDirection.Input);
            dbManager.AddParameters(8, "@VLEFTAKREDIMONHUAJ", vleftaKrediMonHuaj, ParameterDirection.Input);
            dbManager.AddParameters(9, "@VLEFTADEBIMONBAZE", vleftaDebiMonBaze, ParameterDirection.Input);
            dbManager.AddParameters(10, "@VLEFTAKREDIMONBAZE", vleftaKrediMonBaze, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.Text, @"INSERT INTO " + Properties.Settings.Default.DbCore_EmriDatabaze_Import + ".dbo." + Properties.Settings.Default.DbCore_EmriTabeles_ImportTrupiFleteKontabel + @" 
(ID_KOKA_IMPORT, ID_DOKUMENTIMPORTUAR, NRLLOGARI, PERSHKRIMITRUPFLETEKONTABEL, MONEDHA, KURSI, VLEFTADEBIMONHUAJ, VLEFTAKREDIMONHUAJ, VLEFTADEBIMONBAZE, 
VLEFTAKREDIMONBAZE)
VALUES (@ID_KOKA_IMPORT, @ID_DOKUMENTIMPORTUAR, @NRLLOGARI, @PERSHKRIMITRUPFLETEKONTABEL, @MONEDHA, @KURSI, @VLEFTADEBIMONHUAJ, @VLEFTAKREDIMONHUAJ,
@VLEFTADEBIMONBAZE, @VLEFTAKREDIMONBAZE) set @ID_TRUPIIMPORT = @@identity");

            idTrupiImport = int.Parse(dbManager.Parameters[0].Value.ToString());
            clsMesazh mesazh = new clsMesazh(true, "Ruatja perfundoi me sukses!");
            return mesazh;
        }

        /// <summary>
        /// IMPORTI FLETEVE KONTABEL:
        /// Kthen nga databaza nepermjet store procedures ne kode nje DataTable me te gjitha trupat e fleteve kontabel te nje koke per import.
        /// </summary>
        /// <returns>Kthen nje DataTable me trupat e importit te fleteve kontabel sipas kokes se importit nga tabela T_TEMP_TRUPI_IMPORT_FLETEKONTABEL.</returns>
        internal DataTable ktheTrupiFleteKontabelSipasIdDokumentiImportuar(int idDokumentiImportuar, int idKokaImport)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@ID_DOKUMENTIMPORTUAR", idDokumentiImportuar, ParameterDirection.Input);
            dbManager.AddParameters(1, "@ID_KOKA_IMPORT", idKokaImport, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.Text, @"SELECT ID_TRUPIIMPORT, ID_KOKA_IMPORT, ID_DOKUMENTIMPORTUAR, NRLLOGARI, PERSHKRIMITRUPFLETEKONTABEL, MONEDHA, KURSI, VLEFTADEBIMONHUAJ, VLEFTAKREDIMONHUAJ,
                VLEFTADEBIMONBAZE, VLEFTAKREDIMONBAZE FROM " + Properties.Settings.Default.DbCore_EmriDatabaze_Import + ".dbo." + Properties.Settings.Default.DbCore_EmriTabeles_ImportTrupiFleteKontabel + @" WHERE ID_DOKUMENTIMPORTUAR = @ID_DOKUMENTIMPORTUAR
                AND ID_KOKA_IMPORT = @ID_KOKA_IMPORT");
            return ds.Tables[0];
        }

        #endregion

        #region Import tabele AlbSig

        /// <summary>
        /// IMPORTI FLETEVE KONTABEL:
        /// Kthen nga databaza nepermjet store procedures ne kode nje DataTable me te gjitha kokat e fleteve kontabel te pa importuara.
        /// </summary>
        /// <returns>Kthen nje DataTable me kokat e importit te fleteve kontabel nga tabela T_TEMP_KOKA_IMPORT_FLETEKONTABEL.</returns>
        internal DataTable ktheFleteKontabelTePaImportuaraAlbSig()
        {
            dbManager.Open();
            DataSet ds = dbManager.ExecuteDataSet(CommandType.Text, @"SELECT NRLLOG, PERSHKRIMI, VEPRIMI, DATA_DOKUMENTIT, KODAGJENTI, TYPE, SUBTYPE, SUM(VLEFTA) AS VLEFTA, 
		SUM(VLEFTAMONLLOG) AS VLEFTAMONLLOG, MONEDHA, STATUSI
		FROM
		(
			SELECT NRLLOG + KODAGJENTI + TYPE + SUBTYPE + MONEDHA AS NRLLOG, PERSHKRIMI + ' ' + MUAJI + ' ' + VITI AS PERSHKRIMI, VEPRIMI, DATA_DOKUMENTIT, KODAGJENTI, 
			TYPE, SUBTYPE, VLEFTA, VLEFTAMONLLOG, MONEDHA, STATUSI 
			FROM " + Properties.Settings.Default.DbCore_EmriDatabaze_Import + ".dbo." + Properties.Settings.Default.DbCore_EmriTabeles_ImportAlbSigFleteKontabel + @" WHERE STATUSI = 0
		) AS TAB_IMPORT 
		GROUP BY NRLLOG, PERSHKRIMI, VEPRIMI, DATA_DOKUMENTIT, KODAGJENTI, TYPE, SUBTYPE, MONEDHA, STATUSI");
            return ds.Tables[0];
        }

        /// <summary>
        /// IMPORTI FLETEVE KONTABEL:
        /// Modifikon nepermjet store procedures ne kode fletet kontabel qe sapo u importuan sakte si te importuara.
        /// </summary>
        /// <returns>Kthen clsMesazh me atribut statusi True nese modifikimi kryhet me sukses ose False dhe mesazhin e gabimit ne te kundert.</returns>
        internal clsMesazh modifiFleteKontabelTeImportuaraAlbsig(DateTime dtDokumenti)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@DATA_DOKUMENTIT", dtDokumenti, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.Text, @"UPDATE " + Properties.Settings.Default.DbCore_EmriDatabaze_Import + ".dbo." + Properties.Settings.Default.DbCore_EmriTabeles_ImportAlbSigFleteKontabel + @" SET STATUSI = 1, DATA_ORE_LEXIMI = GETDATE() WHERE DATA_DOKUMENTIT = @DATA_DOKUMENTIT");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgModifikimiMeSukses"]);
            return mesazh;
        }

        #endregion

        #region Eksport Shitje SQL

        public bool shtoFaturaNeTabeleEksportiKoka(DataTable dt)
        {
            dbManager.Open();
            dbManager.CreateInsertParameters(58);
            dbManager.AddInsertParameters(0, "@IDDOKUMENTIORIGJINAL", DbType.Decimal, "Id Shitje Koka");
            dbManager.AddInsertParameters(1, "@NENKATEGORIA", DbType.String, "Nenkategoria");
            dbManager.AddInsertParameters(2, "@LLOJDOKUMENTI", DbType.String, "Lloj Dokumenti");
            dbManager.AddInsertParameters(3, "@NRDOK", DbType.String, "Nr Dokumenti");
            dbManager.AddInsertParameters(4, "@NDERMARRJEKOD", DbType.String, "Kod Ndermarrje");
            dbManager.AddInsertParameters(5, "@DATEDOK", DbType.DateTime, "Date Dokumenti");
            dbManager.AddInsertParameters(6, "@KLIENTFURNITOR", DbType.String, "Klient/Furnitori");
            dbManager.AddInsertParameters(7, "@NRPROJEKTI", DbType.String, "Numer Projekti");
            dbManager.AddInsertParameters(8, "@NRSERIAL", DbType.String, "Numer Serial");
            dbManager.AddInsertParameters(9, "@DATEMATURIMI", DbType.DateTime, "Date Maturimi");
            dbManager.AddInsertParameters(10, "@MONEDHA", DbType.String, "Monedha");
            dbManager.AddInsertParameters(11, "@KURSI", DbType.Decimal, "Kursi");
            dbManager.AddInsertParameters(12, "@PERSHKRIMI", DbType.String, "Pershkrimi");
            dbManager.AddInsertParameters(13, "@DOGANA", DbType.Boolean, "Dogana");
            dbManager.AddInsertParameters(14, "@PIKESHITJEFURNIZIMI", DbType.String, "Pike Shitje/Furnizimi");
            dbManager.AddInsertParameters(15, "@DEGEADMINISTRATIVE", DbType.String, "Dege Administrative");
            dbManager.AddInsertParameters(16, "@MENYREPAGESE", DbType.String, "Menyre Pagese");
            dbManager.AddInsertParameters(17, "@DTREGJISTRIMI", DbType.DateTime, "Date Regjistrimi");
            dbManager.AddInsertParameters(18, "@ADRESEFATURIMI", DbType.String, "Adresa e Faturimit");
            dbManager.AddInsertParameters(19, "@ADRESEDERGIMI", DbType.String, "Adresa e Dergimit");
            dbManager.AddInsertParameters(20, "@TOTALZBRITJE", DbType.Decimal, "Total Zbritje");
            dbManager.AddInsertParameters(21, "@GRUPIMDOK1", DbType.String, "Grupim Dokumenti 1");
            dbManager.AddInsertParameters(22, "@GRUPIMDOK2", DbType.String, "Grupim Dokumenti 2");
            dbManager.AddInsertParameters(23, "@GRUPIMDOK3", DbType.String, "Grupim Dokumenti 3");
            dbManager.AddInsertParameters(24, "@AFATKOHOR", DbType.DateTime, "Afati Kohor");
            dbManager.AddInsertParameters(25, "@AGJENTSHITJE1", DbType.String, "Agjent Shitje 1");
            dbManager.AddInsertParameters(26, "@PERQINDJEAGJENT1", DbType.Decimal, "Perqindje Agjenti 1");
            dbManager.AddInsertParameters(27, "@AGJENTSHITJE2", DbType.String, "Agjent Shitje 2");
            dbManager.AddInsertParameters(28, "@PERQINDJEAGJENT2", DbType.Decimal, "Perqindje Agjenti 2");
            dbManager.AddInsertParameters(29, "@AGJENTSHITJE3", DbType.String, "Agjent Shitje 3");
            dbManager.AddInsertParameters(30, "@PERQINDJEAGJENT3", DbType.Decimal, "Perqindje Agjenti 3");
            dbManager.AddInsertParameters(31, "@EMERKLIENTI", DbType.String, "Emer Klienti");
            dbManager.AddInsertParameters(32, "@KONTAKTI", DbType.String, "Kontakti");
            dbManager.AddInsertParameters(33, "@KASE", DbType.Boolean, "Kase");
            dbManager.AddInsertParameters(34, "@KUPON", DbType.Boolean, "Kupon");
            dbManager.AddInsertParameters(35, "@DTFILLIMI", DbType.DateTime, "Date Fillimi");
            dbManager.AddInsertParameters(36, "@DTMBARIMI", DbType.DateTime, "Date Mbarimi");
            dbManager.AddInsertParameters(37, "@TRANSPORTUESI", DbType.String, "Transportues");
            dbManager.AddInsertParameters(38, "@MARRESI", DbType.String, "Marresi");
            dbManager.AddInsertParameters(39, "@AUTOMJETI", DbType.String, "Automjeti");
            dbManager.AddInsertParameters(40, "@KILOMETRA", DbType.String, "Kilometra");
            dbManager.AddInsertParameters(41, "@SHPENZIMEJOTEZBRITSHME", DbType.Boolean, "Shpenzime Jo Te Zbritshme");
            dbManager.AddInsertParameters(42, "@DTTRANSPORTIMI", DbType.DateTime, "Dt Transporti");
            dbManager.AddInsertParameters(43, "@KUSHTDERGIMI", DbType.String, "Kusht Dergimi");
            dbManager.AddInsertParameters(44, "@KUSHTPAGESE", DbType.String, "Kusht Pagese");
            dbManager.AddInsertParameters(45, "@TOTALI", DbType.Decimal, "Totali");
            dbManager.AddInsertParameters(46, "@TVSH", DbType.Decimal, "Tvsh Koka");
            dbManager.AddInsertParameters(47, "@IDSTATUSDOK", DbType.Int16, "Id Status Dok");
            dbManager.AddInsertParameters(48, "@IDNIVELGJENERUES", DbType.Decimal, "Id Nivel Gjenerues");
            dbManager.AddInsertParameters(49, "@IDKONFIGGJENERUES", DbType.Decimal, "Id Konfig Gjenerues");
            dbManager.AddInsertParameters(50, "@IDGJENERUES", DbType.Decimal, "Id Gjenerues");
            dbManager.AddInsertParameters(51, "@IDDOKNGA", DbType.Decimal, "Id Dok Nga");
            dbManager.AddInsertParameters(52, "@DTKRIJIMI", DbType.DateTime, "Dt Krijimi");
            dbManager.AddInsertParameters(53, "@TOTALIMETVSHMEZBRITJE", DbType.Decimal, "Totali Me Tvsh Me Zbritje");
            dbManager.AddInsertParameters(54, "@PERDORUES", DbType.String, "Perdoruesi");
            dbManager.AddInsertParameters(55, "@CASH", DbType.Decimal, "Cash");
            dbManager.AddInsertParameters(56, "@FATUREPERMBLEDHESE", DbType.Boolean, "Fature Permbledhese");
            dbManager.AddInsertParameters(57, "@ARKA", DbType.String, "Arka");
            dbManager.CreateParameters(0);//bejme clear koleksionin e parametrave te selektit
            dbManager.ExecuteInsert(dt, CommandType.StoredProcedure, "prc_T_TEMP_EKSPORTSHITJE_sel", CommandType.StoredProcedure, "prc_T_TEMP_EKSPORTSHITJE_ins");
            return true;
        }

        public bool ekzistonTabele(string emerTabele)
        {
            bool ekziston = false;
            try
            {
                dbManager.Open();
                ekziston = Convert.ToBoolean(dbManager.ExecuteScalar(CommandType.Text, "select case when exists((select * from information_schema.tables where table_name = '" + emerTabele + "')) then 1 else 0 end"));
                dbManager.CloseReader();
                return ekziston;
            }
            catch
            {
                try
                {
                    // Other RDBMS.  Graceful degradation
                    dbManager.Open();
                    ekziston = Convert.ToBoolean(dbManager.ExecuteScalar(CommandType.Text, "select 1 from " + emerTabele + " where 1 = 0"));
                    dbManager.CloseReader();
                    return ekziston;
                }
                catch
                {
                    ekziston = false;
                    return ekziston;
                }
            }
        }

        public bool ekzistonStoredProcedure(string emerSP)
        {
            bool ekziston = false;
            try
            {
                string query = "select COUNT(*) from sysobjects where type='P' and name='" + emerSP + "'";
                dbManager.Open();
                ekziston = Convert.ToBoolean(dbManager.ExecuteScalar(CommandType.Text, query));
                return ekziston;
            }
            catch
            {
                ekziston = false;
                return ekziston;
            }
        }

        public bool shtoFaturaNeTabeleEksportiTrupi(DataTable dt)
        {
            dbManager.Open();
            dbManager.CreateInsertParameters(24);
            dbManager.AddInsertParameters(0, "@IDTRUPIDOKUMENTIORIGJINAL", DbType.Decimal, "IdRreshtiShitje");
            dbManager.AddInsertParameters(1, "@IDKOKADOKUMENTIORIGJINAL", DbType.Decimal, "Id Shitje Koka");
            dbManager.AddInsertParameters(2, "@IDLLOJVEPRIMI", DbType.String, "Lloji");
            dbManager.AddInsertParameters(3, "@KODI", DbType.String, "Kodi");
            dbManager.AddInsertParameters(4, "@PERSHKRIMI", DbType.String, "Pershkrim Trupi");
            dbManager.AddInsertParameters(5, "@NJESIA", DbType.String, "Njesia");
            dbManager.AddInsertParameters(6, "@SASIA", DbType.Decimal, "Sasia");
            dbManager.AddInsertParameters(7, "@CMIMI", DbType.Decimal, "Cmimi");
            dbManager.AddInsertParameters(8, "@ZBRITJE", DbType.Decimal, "Zbritje Analitike");
            dbManager.AddInsertParameters(9, "@VLEFTAPATVSH", DbType.Decimal, "Vlefta pa TVSH");
            dbManager.AddInsertParameters(10, "@TVSH", DbType.String, "TVSH");
            dbManager.AddInsertParameters(11, "@VLEFTAMETVSH", DbType.Decimal, "Vlefta me TVSH");
            dbManager.AddInsertParameters(12, "@MAGAZINA", DbType.String, "Magazina");
            dbManager.AddInsertParameters(13, "@GJERESI", DbType.Decimal, "Gjeresi");
            dbManager.AddInsertParameters(14, "@GJATESI", DbType.Decimal, "Gjatesi");
            dbManager.AddInsertParameters(15, "@SASIPERMASE", DbType.Decimal, "Sasi Permase");
            dbManager.AddInsertParameters(16, "@DETAJIMI1", DbType.String, "Detajimi 1");
            dbManager.AddInsertParameters(17, "@DETAJIMI2", DbType.String, "Detajimi 2");
            dbManager.AddInsertParameters(18, "@SHENIME", DbType.String, "Shenime");
            dbManager.AddInsertParameters(19, "@LLOGSHPENZIMI", DbType.String, "Llogari Shpenzimi");
            dbManager.AddInsertParameters(20, "@DTFILLIMI", DbType.DateTime, "Date Fillimi Trupi");
            dbManager.AddInsertParameters(21, "@DTMBARIMI", DbType.DateTime, "Date Mbarimi Trupi");
            dbManager.AddInsertParameters(22, "@IDTRUPIKONVERTIMI", DbType.Decimal, "Id Trupi Konvertimi");
            dbManager.AddInsertParameters(23, "@SASIREZ", DbType.Decimal, "Sasi Rezervimi");
            dbManager.CreateParameters(0);//bejme clear koleksionin e parametrave te selektit
            dbManager.ExecuteInsert(dt, CommandType.StoredProcedure, "prc_T_TEMP_TRUPIEKSPORTSHITJE_sel", CommandType.StoredProcedure, "prc_T_TEMP_TRUPIEKSPORTSHITJE_ins");
            return true;
        }

        public clsMesazh shtoFaturaNeTabeleEksporti(DataTable dt, DbCore.DbAdmin.colTrupiFormatImporti col, string emerTabele, int kokeApoTrup, int kategoria)
        {
            int i = 0;
            dbManager.Open();
            dbManager.CreateInsertParameters(col.ktheNumerFushash(kokeApoTrup, kategoria));
            foreach (DbAdmin.clsTrupiFormatImporti tr in col)
            {
                if ((kokeApoTrup == 1 && ((tr.Visible && tr.FusheKokeApoTrupi == 1) || tr.FusheKokeApoTrupi == 3 || tr.KodKontrolli == "Kod Ndermarrje")) || (kokeApoTrup == 2 && ((tr.Visible && tr.FusheKokeApoTrupi == 2) || tr.FusheKokeApoTrupi == 3 || (kategoria == 45 && tr.FusheKokeApoTrupi == 5))) || (kokeApoTrup == 4 && ((tr.Visible && tr.FusheKokeApoTrupi == 4) || tr.FusheKokeApoTrupi == 5 || tr.KodKontrolli == "IdReceptura")))
                {
                    DbType tipi;
                    switch (tr.FusheType.ToLower())
                    {
                        case "varchar(100)":
                        case "varchar(20)":
                        case "varchar(max)":
                            tipi = DbType.String;
                            break;
                        case "bit":
                            tipi = DbType.Boolean;
                            break;
                        case "float":
                            tipi = DbType.Double;
                            break;
                        case "decimal(18, 3)":
                            tipi = DbType.Decimal;
                            break;
                        case "numeric(18, 0)":
                        case "int":
                            tipi = DbType.Int64;
                            break;
                        case "date":
                            tipi = DbType.Date;
                            break;
                        case "datetime":
                            tipi = DbType.DateTime;
                            break;
                        default:
                            tipi = DbType.String;
                            break;
                    }
                    dbManager.AddInsertParameters(i, "@" + tr.EmerImporti.Replace(" ", "").Replace("/", "").Replace("-", ""), tipi, tr.EmerImporti);
                    i++;
                }
            }
            dbManager.CreateParameters(0);//bejme clear koleksionin e parametrave te selektit                
            dbManager.ExecuteInsert(dt, CommandType.StoredProcedure, "prc_" + emerTabele + "_sel", CommandType.StoredProcedure, "prc_" + emerTabele + "_ins");
            return new clsMesazh(true, "Eksporti mbaroi me sukses!");
        }

        public DataTable GjejDokumentaEkzistues(string emerTabele, string primaryKey, string ids)
        {
            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddInputParameters("@EMERTABELE", emerTabele);
            dbManager.AddInputParameters("@PRIMARY_KEY", primaryKey);
            dbManager.AddInputParameters("@IDS", ids);
            var ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_EKSPORTSQL_GjejDokumentaEkzistues");
            if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
                return null;

            return ds.Tables[0];
        }


        public clsMesazh shtoFaturaNeTabeleImporti(DataTable dt, DbCore.DbAdmin.colTrupiFormatImporti col, string emerTabele, int kokeApoTrup, int kategoria, bool insert)
        {
            int i = 0;
            dbManager.Open();
            int nrParameters = col.ktheNumerFushash(kokeApoTrup, kategoria);
            if (!insert)
                nrParameters += 1;
            if (!insert && kokeApoTrup == 1)
                nrParameters += 2;

            dbManager.CreateInsertParameters(nrParameters);

            foreach (DbAdmin.clsTrupiFormatImporti tr in col)
            {
                if ((kokeApoTrup == 1 && ((tr.Visible && tr.FusheKokeApoTrupi == 1) || tr.FusheKokeApoTrupi == 3 || tr.KodKontrolli == "Kod Ndermarrje")) || (kokeApoTrup == 2 && ((tr.Visible && tr.FusheKokeApoTrupi == 2) || tr.FusheKokeApoTrupi == 3 || (kategoria == 45 && tr.FusheKokeApoTrupi == 5))) || (kokeApoTrup == 4 && ((tr.Visible && tr.FusheKokeApoTrupi == 4) || tr.FusheKokeApoTrupi == 5 || tr.KodKontrolli == "IdReceptura")))
                {
                    DbType tipi;
                    switch (tr.FusheType.ToLower())
                    {
                        case "varchar(100)":
                        case "varchar(20)":
                        case "varchar(max)":
                            tipi = DbType.String;
                            break;
                        case "bit":
                            tipi = DbType.Boolean;
                            break;
                        case "float":
                            tipi = DbType.Double;
                            break;
                        case "decimal(18, 3)":
                            tipi = DbType.Decimal;
                            break;
                        case "numeric(18, 0)":
                        case "int":
                            tipi = DbType.Int64;
                            break;
                        case "date":
                            tipi = DbType.Date;
                            break;
                        case "datetime":
                            tipi = DbType.DateTime;
                            break;
                        default:
                            tipi = DbType.String;
                            break;
                    }
                        dbManager.AddInsertParameters(i, "@" + tr.EmerImporti.Replace(" ", "").Replace("/", "").Replace("-", ""), tipi, tr.EmerImporti);
                    i++;
                }
            }
            dbManager.CreateParameters(0);//bejme clear koleksionin e parametrave te selektit             
            if (insert)
                dbManager.ExecuteInsert(dt, CommandType.StoredProcedure, "prc_" + emerTabele + "_sel", CommandType.StoredProcedure, "prc_" + emerTabele + "_ins");
            else
            {
                switch (kokeApoTrup)
                {
                    case 1:
                        dbManager.AddInsertParameters(nrParameters - 3, "@IDIMPORTSHITJE", DbType.Int64, "IDIMPORTSHITJE");
                        dbManager.AddInsertParameters(nrParameters - 2, "@DTIMPORTI", DbType.DateTime, "DTIMPORTI");
                        dbManager.AddInsertParameters(nrParameters - 1, "@DTLEXIMI", DbType.DateTime, "DTLEXIMI");
                        break;
                    case 2:
                        dbManager.AddInsertParameters(nrParameters - 1, "@IDIMPORTTRUPISHITJE", DbType.Int64, "IDIMPORTTRUPISHITJE");
                        break;
                    case 4:
                        dbManager.AddInsertParameters(nrParameters - 1, "@IDIMPORTRECEPTURA", DbType.Int64, "IDIMPORTRECEPTURA");
                        break;
                }
                dbManager.ExecuteInsert(dt, CommandType.StoredProcedure, "prc_" + emerTabele + "_upd");
            }
            return new clsMesazh(true, "Eksporti mbaroi me sukses!");
        }
        public clsMesazh fshiFaturaNgaTabeleImporti(DataTable dt, DbCore.DbAdmin.colTrupiFormatImporti col, string emerTabele, int kokeApoTrup, int kategoria)
        {
            int i = 0;
            dbManager.Open();
            int nrParameters = col.ktheNumerFushash(kokeApoTrup, kategoria) + 1;
            if (kokeApoTrup == 1)
                nrParameters += 2;

            dbManager.CreateInsertParameters(nrParameters);

            foreach (DbAdmin.clsTrupiFormatImporti tr in col)
            {
                if ((kokeApoTrup == 1 && ((tr.Visible && tr.FusheKokeApoTrupi == 1) || tr.FusheKokeApoTrupi == 3 || tr.KodKontrolli == "Kod Ndermarrje")) || (kokeApoTrup == 2 && ((tr.Visible && tr.FusheKokeApoTrupi == 2) || tr.FusheKokeApoTrupi == 3 || (kategoria == 45 && tr.FusheKokeApoTrupi == 5))) || (kokeApoTrup == 4 && ((tr.Visible && tr.FusheKokeApoTrupi == 4) || tr.FusheKokeApoTrupi == 5 || tr.KodKontrolli == "IdReceptura")))
                {
                    DbType tipi;
                    switch (tr.FusheType.ToLower())
                    {
                        case "varchar(100)":
                        case "varchar(20)":
                        case "varchar(max)":
                            tipi = DbType.String;
                            break;
                        case "bit":
                            tipi = DbType.Boolean;
                            break;
                        case "float":
                            tipi = DbType.Double;
                            break;
                        case "decimal(18, 3)":
                            tipi = DbType.Decimal;
                            break;
                        case "numeric(18, 0)":
                        case "int":
                            tipi = DbType.Int64;
                            break;
                        case "date":
                            tipi = DbType.Date;
                            break;
                        case "datetime":
                            tipi = DbType.DateTime;
                            break;
                        default:
                            tipi = DbType.String;
                            break;
                    }
                    dbManager.AddInsertParameters(i, "@" + tr.EmerImporti.Replace(" ", "").Replace("/", "").Replace("-", ""), tipi, tr.EmerImporti);
                    i++;
                }
            }
            dbManager.CreateParameters(0);//bejme clear koleksionin e parametrave te selektit             
            switch (kokeApoTrup)
            {
                case 1:
                    dbManager.AddInsertParameters(nrParameters - 3, "@IDIMPORTSHITJE", DbType.Int64, "IDIMPORTSHITJE");
                    dbManager.AddInsertParameters(nrParameters - 2, "@DTIMPORTI", DbType.DateTime, "DTIMPORTI");
                    dbManager.AddInsertParameters(nrParameters - 1, "@DTLEXIMI", DbType.DateTime, "DTLEXIMI");
                    break;
                case 2:
                    dbManager.AddInsertParameters(nrParameters - 1, "@IDIMPORTTRUPISHITJE", DbType.Int64, "IDIMPORTTRUPISHITJE");
                    break;
                case 4:
                    dbManager.AddInsertParameters(nrParameters - 1, "@IDIMPORTRECEPTURA", DbType.Int64, "IDIMPORTRECEPTURA");
                    break;
            }
            dbManager.ExecuteInsert(dt, CommandType.StoredProcedure, "prc_" + emerTabele + "_del");
            return new clsMesazh(true, "Fshirja mbaroi me sukses!");
        }

        public bool shtoDokumentaMagazineNeTabeleEksportiKoka(DataTable dt)
        {
            dbManager.Open();
            dbManager.CreateInsertParameters(28);
            dbManager.AddInsertParameters(0, "@IDDOKUMENTIORIGJINAL", DbType.Decimal, "Id Koka Magazina");
            dbManager.AddInsertParameters(1, "@NENKATEGORIA", DbType.String, "Nenkategoria");
            dbManager.AddInsertParameters(2, "@LLOJDOKUMENTI", DbType.String, "Lloj Dokumenti");
            dbManager.AddInsertParameters(3, "@NRDOK", DbType.String, "Nr Dokumenti");
            dbManager.AddInsertParameters(4, "@NDERMARRJEKOD", DbType.String, "Kod Ndermarrje");
            dbManager.AddInsertParameters(5, "@DATEDOK", DbType.DateTime, "Date Dokumenti");
            dbManager.AddInsertParameters(6, "@KLIENTFURNITOR", DbType.String, "Klient/Furnitori");
            dbManager.AddInsertParameters(7, "@NRPROJEKTI", DbType.String, "Numer Projekti");
            dbManager.AddInsertParameters(8, "@PERSHKRIMI", DbType.String, "Pershkrimi");
            dbManager.AddInsertParameters(9, "@DEGEADMINISTRATIVE", DbType.String, "Dege Administrative");
            dbManager.AddInsertParameters(10, "@AUTOMJETI", DbType.String, "Automjeti");
            dbManager.AddInsertParameters(11, "@MAGAZINA", DbType.String, "Magazina");
            dbManager.AddInsertParameters(12, "@PERDORUES", DbType.String, "Perdoruesi");
            dbManager.AddInsertParameters(13, "@SHENIME", DbType.String, "Shenime");
            dbManager.AddInsertParameters(14, "@IDNIVELGJENERUES", DbType.String, "Id Nivel Gjenerues");
            dbManager.AddInsertParameters(15, "@IDKONFIGGJENERUES", DbType.String, "Id Konfig Gjenerues");
            dbManager.AddInsertParameters(16, "@IDGJENERUES", DbType.Decimal, "Id Gjenerues");
            dbManager.AddInsertParameters(17, "@LLOGARI", DbType.String, "Llogari");
            dbManager.AddInsertParameters(18, "@NJESIVARTESE", DbType.String, "Njesi Vartese");
            dbManager.AddInsertParameters(19, "@MEKONFIRMIM", DbType.Boolean, "Me Konfirmim");
            dbManager.AddInsertParameters(20, "@GRUPIMDOK1", DbType.String, "Grupim Dok Magazine 1");
            dbManager.AddInsertParameters(21, "@GRUPIMDOK2", DbType.String, "Grupim Dok Magazine 2");
            dbManager.AddInsertParameters(22, "@GRUPIMDOK3", DbType.String, "Grupim Dok Magazine 3");
            dbManager.AddInsertParameters(23, "@MAGAZIENIERI", DbType.String, "Magazinieri");
            dbManager.AddInsertParameters(24, "@ADRESA", DbType.String, "Adresa");
            dbManager.AddInsertParameters(25, "@IDKRIJUESI", DbType.Decimal, "Id Krijuesi");
            dbManager.AddInsertParameters(26, "@IDNDERMVIT", DbType.String, "Id Nderm Vit");
            dbManager.AddInsertParameters(27, "@IDDOKNGA", DbType.String, "Id Dok Nga");
            dbManager.CreateParameters(0);//bejme clear koleksionin e parametrave te selektit
            dbManager.ExecuteInsert(dt, CommandType.StoredProcedure, "prc_T_TEMP_EKSPORTMAGAZINA_sel", CommandType.StoredProcedure, "prc_T_TEMP_EKSPORTMAGAZINE_ins");
            return true;
        }

        public bool shtoDokumentaMagazineNeTabeleEksportiTrupi(DataTable dt)
        {
            dbManager.Open();
            dbManager.CreateInsertParameters(12);
            dbManager.AddInsertParameters(0, "@IDTRUPIDOKUMENTIORIGJINAL", DbType.Decimal, "IdRreshtiMagazina");
            dbManager.AddInsertParameters(1, "@IDKOKADOKUMENTIORIGJINAL", DbType.Decimal, "Id Koka Magazina");
            dbManager.AddInsertParameters(2, "@KODARTIKULLI", DbType.String, "Kodi i Artikullit");
            dbManager.AddInsertParameters(3, "@NJESIA", DbType.String, "Njesia");
            dbManager.AddInsertParameters(4, "@SASIA", DbType.Decimal, "Sasia");
            dbManager.AddInsertParameters(5, "@CMIMI", DbType.Decimal, "Cmimi");
            dbManager.AddInsertParameters(6, "@VLEFTA", DbType.Decimal, "Vlefta");
            dbManager.AddInsertParameters(7, "@KOEFICENTI", DbType.Decimal, "Koeficenti");
            dbManager.AddInsertParameters(8, "@SHENJA", DbType.String, "Shenja");
            dbManager.AddInsertParameters(9, "@DETAJIMI1", DbType.String, "Detajimi 1");
            dbManager.AddInsertParameters(10, "@DETAJIMI2", DbType.String, "Detajimi 2");
            dbManager.AddInsertParameters(11, "@MAGDESTINACION", DbType.String, "Mag Destinacion");
            dbManager.CreateParameters(0);//bejme clear koleksionin e parametrave te selektit
            dbManager.ExecuteInsert(dt, CommandType.StoredProcedure, "prc_T_TEMP_TRUPIEKSPORTMAGAZINA_sel", CommandType.StoredProcedure, "prc_T_TEMP_TRUPIEKSPORTMAGAZINA_ins");
            return true;
        }
        internal void FshiDokumentatTeDuplikuar(string emerTabele, string primaryKey)
        {

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@TABELA", emerTabele, ParameterDirection.Input);
            dbManager.AddParameters(1, "@PRIMARYKEY", primaryKey, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_TEMP_IMPORT_FshiDokumentatTeDuplikuar");

        }

        #endregion

        internal DataTable merrObjektePerImportSQL(string emerTab, string ndermarrjeKey, string ndermarjeKodi, bool merrTePaImportuara, bool rimerrteimportuara, int? nrDokumentash)
        {
            dbManager.Open();
            dbManager.CreateParameters(6);
            dbManager.AddParameters(0, "@TABEKZEKUTIM", emerTab, ParameterDirection.Input);
            dbManager.AddParameters(1, "@NDERMARRJEKEY", ndermarrjeKey, ParameterDirection.Input);
            dbManager.AddParameters(2, "@NDERMARJEKODI", ndermarjeKodi, ParameterDirection.Input);
            dbManager.AddParameters(3, "@MERRTEPAIMPORTUARA", merrTePaImportuara, ParameterDirection.Input);
            dbManager.AddParameters(4, "@RIMERRTEIMPORTUARA", rimerrteimportuara, ParameterDirection.Input);
            dbManager.AddParameters(5, "@NRDOKUMENTASH", nrDokumentash, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_TEMP_IMPORTKokaMerrTeDhenaPerNdermarrjeSipasLlojitDT");
            return ds.Tables[0];
        }

        internal clsMesazh updateDokTabeleTemportal(string idDokImporti, int idNdermarrje, int statusi, string emerTabKoka, string emerFushePrimaryKey, string emerFusheNdermarrje)
        {
            dbManager.Open();
            dbManager.CreateParameters(6);
            dbManager.AddParameters(0, "@IDDOKIMPORTI", idDokImporti, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idNdermarrje, ParameterDirection.Input);
            dbManager.AddParameters(2, "@STATUSI", statusi, ParameterDirection.Input);
            dbManager.AddParameters(3, "@EMERTABELEKOKA", emerTabKoka, ParameterDirection.Input);
            dbManager.AddParameters(4, "@EMERFUSHEPRIMARYKEY", emerFushePrimaryKey, ParameterDirection.Input);
            dbManager.AddParameters(5, "@EMERFUSHENDERMARRJE", emerFusheNdermarrje, ParameterDirection.Input);
            bool importuar = Convert.ToInt32(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_TEMP_KOKASHITJE_updateStatusImporti")) == 1;
            if (importuar)
                return new MesazhGabimi("Ky rresht eshte importuar me pare! Ju lutem ringarkoni te dhenat.");

            return new clsMesazh(true, MessagesResource.Messages["msgModifikimiMeSukses"]);
        }
    }
}