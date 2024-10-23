using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using DbCore.IMBUtils.DataBase;
using DbCore.IMBUtils.Messages;

namespace DbCore.DbAsete
{
    /// <remarks>
    ///  Kjo eshte klasa me e rendesishme e ketij moduli. Eshte nje klase e tille per secilin modul te projektit.
    ///  Ketu ndodhen metodat qe therriten  nga te gjithe objektet brenda projektit DbCore.DbAsete
    ///  secila metode permban thirjet e Store procedures ne DB, duke i kaluar parametrat perkates
    ///  Metodat jane te ndara ne Regions sipas objekteve qe i therrasin keto metoda
    /// </remarks>
    public class clsDatabazeAsete : clsDatabazeAseteAbstract
    {

        public clsDatabazeAsete() { }
        public clsDatabazeAsete(DbData db) : base(db) { }
        public clsDatabazeAsete(string connectionName) : base(connectionName)
        {

        }

        #region Standarte amortizimi

        /// <summary>
        /// MODULI ASETE:
        /// Ruan ne tabelen T_ASETE_STANDART_AMORT objektin e standartit te amortizimi nepermjet procedures PRC_T_ASETE_STANDART_AMORT_INSERT.
        /// </summary>
        /// <param name="idStandarti">(int) Id automatike e standartit te amortizimit.</param>
        /// <param name="emertimi">(string) Emri i standartit te amortizimit.</param>
        /// <param name="pershkrimi">(string) Pershkrimi i standartit te amortizimit.</param>
        /// <param name="idStatusDokumenti">(int) Gjendja qe eshte standarti i amortizmit, i ruajtur(1), fshire(2), apo modifikuar.</param>
        /// <param name="idNdermarrja">(int) Id e ndermarrjes ne te cilen eshte krijuar standarti i amortizimit.</param>
        /// <param name="dtModifikimi">(DateTime) Data ne te cilen eshte bere modifikimi i fundit i id se standartit.</param>
        /// <param name="idPerdoruesi">(int) Id e perdoruesit qe modifikon dokumentin i fundit.</param>
        /// <param name="idKrijuesi">(int) Id e krijuesit te standartit te amortizimit.</param>
        /// <returns>Kthen clsMesazh me atribut statusi True nese ruajtja kryhet me sukses ose False dhe mesazhin e gabimit ne te kundert.</returns>
        internal clsMesazh ruajStandartAmortizimi(out int idStandarti, string emertimi, string pershkrimi, int idStatusDokumenti, int idNdermarrja, DateTime dtModifikimi, int idPerdoruesi, int idKrijuesi)
        {
            idStandarti = -1;

            dbManager.Open();
            dbManager.CreateParameters(8);
            dbManager.AddParameters(0, "@ID_STANDARTI", idStandarti, ParameterDirection.Output);
            dbManager.AddParameters(1, "@EMERTIMI", emertimi, ParameterDirection.Input);
            dbManager.AddParameters(2, "@PERSHKRIMI", pershkrimi, ParameterDirection.Input);
            dbManager.AddParameters(3, "@IDSTATUSDOK", idStatusDokumenti, ParameterDirection.Input);
            dbManager.AddParameters(4, "@IDNDERMARRJE", idNdermarrja, ParameterDirection.Input);
            dbManager.AddParameters(5, "@DTMODIFIKIMI", DBNull.Value, ParameterDirection.Input);
            dbManager.AddParameters(6, "@IDKRIJUESI", idPerdoruesi, ParameterDirection.Input);
            dbManager.AddParameters(7, "@IDPERDORUESI", idKrijuesi, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "PRC_T_ASETE_STANDART_AMORT_INSERT");
            idStandarti = int.Parse(dbManager.Parameters[0].Value.ToString());
            clsMesazh mesazh = new clsMesazh(true, "Ruatja perfundoi me sukses!");
            return mesazh;

        }

        internal Boolean aKaVeprimeMeLlojiAmortizimiTeNdryshemMePara(int idArtikulli, DateTime dateAmortizimiFundit, int idLlojAmort)
        {
            String sqlstr = "SELECT 1 FROM T_ASETE_AMORTIZIMI_TRUPI T " +
            "INNER JOIN T_ASETE_AMORTIZIMI_KOKA K ON K.ID_AMORTIZIMI = T.IDAMORTIZIMIKOKA " +
            "INNER JOIN T_ASETE_LIDHJE_ARTIKULL_LLOJAMORT A ON T.IDARTIKULL_LLOJAMORT = A.ID_ARTIKULL_LLOJAMORTIZIM " +
            "WHERE T.IDARTIKULLI = " + idArtikulli + " " +
            "AND K.IDSTATUSDOK = 1 " +
            "AND T.DATE_AMORTIZIMI < '" + dateAmortizimiFundit.ToString("yyyy-MM-dd HH:mm:ss.fff") + "' " +
            "AND A.IDLLOJAMORTIZIMI <> " + idLlojAmort;

            dbManager.Open();
            DataSet ds = dbManager.ExecuteDataSet(CommandType.Text, sqlstr);

            return ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0;
        }

        /// <summary>
        /// MODULI ASETE:
        /// Modifikon ne tabelen T_ASETE_STANDART_AMORT objektin e standartit te amortizimi nepermjet procedures PRC_T_ASETE_STANDART_AMORT_UPDATE.
        /// </summary>
        /// <param name="idStandarti">(int) Id automatike e standartit te amortizimit.</param>
        /// <param name="pershkrimi">(string) Pershkrimi i standartit te amortizimit.</param>
        /// <param name="idStatusDokumenti">(int) Gjendja qe eshte standarti i amortizmit, i ruajtur(1), fshire(2), apo modifikuar.</param>
        /// <param name="idPerdoruesi">(int) Id e perdoruesit qe modifikon dokumentin i fundit.</param>
        /// <returns>Kthen clsMesazh me atribut statusi True nese modifikimi kryhet me sukses ose False dhe mesazhin e gabimit ne te kundert.</returns>
        internal clsMesazh modifikimiStandartAmortizimi(int idStandarti, string pershkrimi, int idStatusDokumenti, int idPerdoruesi)
        {

            dbManager.Open();
            dbManager.CreateParameters(4);
            dbManager.AddParameters(0, "@ID_STANDARTI", idStandarti, ParameterDirection.Input);
            dbManager.AddParameters(1, "@PERSHKRIMI", pershkrimi, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDSTATUSDOK", idStatusDokumenti, ParameterDirection.Input);
            dbManager.AddParameters(3, "@IDPERDORUESI", idPerdoruesi, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "PRC_T_ASETE_STANDART_AMORT_UPDATE");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgModifikimiMeSukses"]);
            return mesazh;

        }

        /// <summary>
        /// MODULI ASETE:
        /// Fshin objektin e standartit te amortizimit ne tabelen T_ASETE_STANDART_AMORT nepermjet procedures PRC_T_ASETE_STANDART_AMORT_DELETE.
        /// </summary>
        /// <param name="idStandarti">(int) Id automatike e standartit te amortizimit.</param>
        /// <returns>Kthen clsMesazh me atribut statusi True nese fshirja kryhet me sukses ose False dhe mesazhin e gabimit ne te kundert.</returns>
        internal clsMesazh fshiStandartAmortizimi(int idStandarti)
        {

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@ID_STANDARTI", idStandarti, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "PRC_T_ASETE_STANDART_AMORT_DELETE");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
            return mesazh;

        }

        /// <summary>
        /// fshin standarti i amortizimit duke i ndryshuar statusin
        /// </summary>
        /// <param name="idStandarti">idstandarti</param>
        /// <param name="idperdoruesi"> idperdoruesi</param>
        /// <returns>Kthen clsMesazh me atribut statusi True nese fshirja kryhet me sukses ose False dhe mesazhin e gabimit ne te kundert.</returns>
        internal clsMesazh fshiStandartAmortizimiStatus(int idStandarti, int idperdoruesi)
        {

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@ID_STANDARTI", idStandarti, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUESI", idperdoruesi, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_ASETE_STANDART_AMORT_upddel");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
            return mesazh;

        }

        /// <summary>
        /// MODULI ASETE:
        /// Kthen nga databaza nepermjet procedures PRC_T_ASETE_STANDART_AMORT_SEL_SIPASNDERMARRJE nje DataTable me te gjitha standartet e amortizimit per ndermarrjen ne perdorim.
        /// </summary>
        /// <param name="idNder">(int) Id e ndermarrjes ne perdorim per te cilen do te merren standartet e amortizimit.</param>
        /// <returns>Kthen nje DataTable me standartet e amortizimit nga tabela T_ASETE_STANDART_AMORT.</returns>
        internal DataTable ktheStandarteAmortizimiTeNdermarrjes(int idNder)
        {

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDNDERMARRJE", idNder, ParameterDirection.Input);

            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "PRC_T_ASETE_STANDART_AMORT_SEL_SIPASNDERMARRJE");
            return ds.Tables[0];

        }

        /// <summary>
        /// MODULI ASETE:
        /// Kthen nga databaza nepermjet procedures PRC_T_ASETE_STANDART_AMORT_SEL_SIPASID nje DataRow me standartin e amortizimit per id e standartit te kerkuar.
        /// </summary>
        /// <param name="idStandarti">(int) Id e standartit te amortizimit te ruajtur ne databaze.</param>
        /// <returns>Kthen nje DataRow me standartin e amortizimit nga tabela T_ASETE_STANDART_AMORT.</returns>
        internal DataRow ktheStandartinAmortizimitSipasID(int idStandarti)
        {

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDSTANDART", idStandarti, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "PRC_T_ASETE_STANDART_AMORT_SEL_SIPASID");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];

        }

        /// <summary>
        /// MODULI ASETE:
        /// Kthen nga databaza nepermjet procedures PRC_T_ASETE_STANDART_AMORT_SEL_SIPASPERSHKRIM nje DataRow me standartin e amortizimit per emertimin e standartit ne ndermarrjen ne perdorim.
        /// </summary>
        /// <param name="emertimi">(string) Emertimi i standartit te amortizimit qe kerkojme.</param>
        /// <param name="idNdermarrja">(int) Id e ndermarrjes ne perdorim per te cilen do te merren standartet e amortizimit.</param>
        /// <returns>Kthen nje DataRow me standartin e amortizimit nga tabela T_ASETE_STANDART_AMORT.</returns>
        internal DataRow ktheStandartinAmortizimitTeNdermarrjesSipasEmertimit(string emertimi, int idNdermarrja)
        {

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@EMERTIMI", emertimi, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARRJE", idNdermarrja, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "PRC_T_ASETE_STANDART_AMORT_SEL_SIPASEMERTIMIT");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];

        }

        /// <summary>
        /// MODULI ASETE:
        /// Kthen nga databaza nepermjet procedures PRC_T_ASETE_STANDART_AMORT_SEL_SIPASPERSHKRIM nje DataRow me standartin e amortizimit per emertimin e standartit ne ndermarrjen ne perdorim per te marre id e standartit te amortizimit. 
        /// </summary>
        /// <param name="emertimi">(string) Emertimi i standartit te amortizimit qe kerkojme.</param>
        /// <param name="idNdermarrja">(int) Id e ndermarrjes ne perdorim per te cilen do te merren standartet e amortizimit.</param>
        /// <returns>Kthen nje int me id standartin e amortizimit nga tabela T_ASETE_STANDART_AMORT.</returns>
        internal int ktheIDStandartinAmortizimitTeNdermarrjesSipasEmertimit(string emertimi, int idNdermarrja)
        {

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@EMERTIMI", emertimi, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARRJE", idNdermarrja, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "PRC_T_ASETE_STANDART_AMORT_SEL_SIPASEMERTIMIT");
            if (ds == null)
                return -1;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return -1;

            int idStandarti;
            int.TryParse(ds.Tables[0].Rows[0]["ID_STANDARTI"].ToString(), out idStandarti);
            return idStandarti;

        }

        /// <summary>
        /// MODULI ASETE:
        /// Kthen nga databaza nepermjet procedures PRC_T_ASETE_STANDART_AMORT_SEL_SIPASPERSHKRIM nje DataRow me standartin e amortizimit per pershkrimin e standartit ne ndermarrjen ne perdorim per te marre id e standartit te amortizimit. 
        /// </summary>
        /// <param name="emertimi">(string) Pershkrimi i standartit te amortizimit qe kerkojme.</param>
        /// <param name="idNdermarrja">(int) Id e ndermarrjes ne perdorim per te cilen do te merren standartet e amortizimit.</param>
        /// <returns>Kthen nje int me id standartin e amortizimit nga tabela T_ASETE_STANDART_AMORT.</returns>
        internal int ktheIDStandartinAmortizimitTeNdermarrjesSipasPershkrimit(string pershkrimi, int idNdermarrja)
        {

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@PERSHKRIMI", pershkrimi, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARRJE", idNdermarrja, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "PRC_T_ASETE_STANDART_AMORT_SEL_SIPASPERSHKRIMIT");
            if (ds == null)
                return -1;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return -1;

            int idStandarti;
            int.TryParse(ds.Tables[0].Rows[0]["ID_STANDARTI"].ToString(), out idStandarti);
            return idStandarti;

        }

        /// <summary>
        /// MODULI ASETE:
        /// Kthen nga databaza nepermjet procedures PRC_T_ASETE_STANDART_AMORT_SEL_SIPASPERSHKRIM nje DataRow me standartin e amortizimit per emertimin e standartit ne ndermarrjen ne perdorim per te pare nese ekziston emertimi i standartit njehere i regjistruar.
        /// </summary>
        /// <param name="emertimi">(string) Emertimi i standartit te amortizimit qe kerkojme.</param>
        /// <param name="idNdermarrja">(int) Id e ndermarrjes ne perdorim per te cilen do te merren standartet e amortizimit.</param>
        /// <returns>Kthen True nese ekziston njehere emertimi i kerkuar per ndermarrjen dhe False ne te kundert.</returns>
        internal bool ekzistonStandartiAmortizimit(string emertimi, int idNdermarrja)
        {

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@EMERTIMI", emertimi, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARRJE", idNdermarrja, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "PRC_T_ASETE_STANDART_AMORT_SEL_SIPASEMERTIMIT");
            if (ds == null)
                return true;
            if (ds.Tables[0].Rows.Count == 0)
                return false;
            return true;

        }

        /// <summary>
        /// MODULI ASETE:
        /// Kontrollon nese ka veprime me nje standart te caktuar.
        /// </summary>
        /// <param name="idstandart">(int) Id e standartit te amortizimit te ruajtur ne databaze.</param>
        /// <returns>Kthen True nese ka veprime te tjera dhe False nese nuk ka.</returns>
        internal bool kaveprimeStandartiAmortizimit(int idstandart)
        {

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDSTANDART", idstandart, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "PRC_T_ASETE_STANDART_AMORT_kaveprimi");
            if (ds == null)
                return true;
            if (ds.Tables[0].Rows.Count == 0)
                return false;
            return true;

        }

        #endregion

        #region Periudha llogaritje

        /// <summary>
        /// MODULI ASETE:
        /// Kthen nga databaza nepermjet procedures PRC_T_ASETE_PERIUDHA_LLOGARITJE_SEL_SIPASLLOJPERIUDHE nje DataTable me te gjitha periudhat e llogaritjes se amortizimit sipas llojit te periudhes.
        /// </summary>
        /// <param name="fundPeriudha">(bool) True nese kerkojme fundin e fillimi te llogaritjes se amortizimit, dhe false nese kerkojme periudhat e fillimit te llogaritjes se amortizimit.</param>
        /// <returns>Kthen nje DataTable me periudhat e llogaritjes se amortizimit nga tabela T_ASETE_PERIUDHA_LLOGARITJE.</returns>
        internal DataTable kthePeriudhaLlogaritjeSipasPeriudhes(bool fundPeriudha, int idGjuha)
        {

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@LLOJPERIUDHE", fundPeriudha, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDGJUHA", idGjuha, ParameterDirection.Input);

            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "PRC_T_ASETE_PERIUDHA_LLOGARITJE_SEL_SIPASLLOJPERIUDHE");
            return ds.Tables[0];

        }

        /// <summary>
        /// MODULI ASETE:
        /// Kthen nga databaza nepermjet procedures PRC_T_ASETE_PERIUDHA_LLOGARITJE_SEL_SIPASID nje DataRow me periudhen e llogaritjes per id e periudhes se llogaritjes se dhene.
        /// </summary>
        /// <param name="idPeriudhaLlogaritje">(int) Id e periudhes se llogaritjes te ruajtur ne databaze.</param>
        /// <returns>Kthen nje DataRow me periudhen e llogaritjes nga tabela T_ASETE_PERIUDHA_LLOGARITJE.</returns>
        internal DataRow kthePeriudhaLlogaritjeSipasID(int idPeriudhaLlogaritje)
        {

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@ID_PERIUDHE_LLOG_AMORT", idPeriudhaLlogaritje, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "PRC_T_ASETE_PERIUDHA_LLOGARITJE_SEL_SIPASID");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];

        }

        /// <summary>
        /// MODULI ASETE:
        /// Kthen nga databaza nepermjet procedures PRC_T_ASETE_PERIUDHA_LLOGARITJE_SEL_SIPASEMERTIMI nje DataRow me periudhen e llogaritjes per emertimin e periudhes se llogaritjes.
        /// </summary>
        /// <param name="emertimi">(string) Emertimi i periudhes se llogartitjes se amortizimit qe kerkojme.</param>
        /// <returns>Kthen nje DataRow me periudhen e llogaritjes nga tabela T_ASETE_PERIUDHA_LLOGARITJE.</returns>
        internal DataRow kthePeriudhaLlogaritjeSipasEmertimit(string emertimi)
        {

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@EMERTIMI", emertimi, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "PRC_T_ASETE_PERIUDHA_LLOGARITJE_SEL_SIPASEMERTIMI");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];

        }

        /// <summary>
        /// MODULI ASETE:
        /// Kthen nga databaza nepermjet procedures PRC_T_ASETE_PERIUDHA_LLOGARITJE_SEL_SIPASEMERTIMI nje DataRow me periudhen e llogaritjes per emertimin e periudhes per te marre id e periudhes se llogaritjes. 
        /// </summary>
        /// <param name="emertimi">(string) Emertimi i periudhes se llogaritjes qe kerkojme.</param>
        /// <returns>Kthen nje int me id periudhes se llogartijes nga tabela T_ASETE_PERIUDHA_LLOGARITJE.</returns>
        internal int ktheIDPeriudhLlogaritjeSipasEmertimit(string emertimi)
        {

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@EMERTIMI", emertimi, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "PRC_T_ASETE_PERIUDHA_LLOGARITJE_SEL_SIPASEMERTIMI");
            if (ds == null)
                return -1;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return -1;

            int idPeriudhaLlogaritje;
            int.TryParse(ds.Tables[0].Rows[0]["ID_PERIUDHE_LLOG_AMORT"].ToString(), out idPeriudhaLlogaritje);
            return idPeriudhaLlogaritje;

        }

        internal DataRow merrKarakteristikaStandarti(int idKodifikimArtikulli, int idStandarti)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Statuset e pergjithshem te magazinave

        /// <summary>
        /// MODULI ASETE:
        /// Ruan ne tabelen T_ASETE_STATUS_MAGAZINE objektin e statusit te magazinave nepermjet procedures PRC_T_ASETE_STATUS_MAGAZINE_INSERT.
        /// </summary>
        /// <param name="idStatusMagazine">(int) Id automatike e statusit te magazinave.</param>
        /// <param name="emertimi">(string) Emri i statusit te magazinave.</param>
        /// <param name="pershkrimi">(string) Pershkrimi i statusit te magazinave.</param>
        /// <param name="ePerdorshme">(bool) True nese statusi i magazinave eshte i perdorshem ne ndermarrjen ne perdorim, False ne te kundert.</param>
        /// <param name="idNdermarrja">(int) Id e ndermarrjes ne te cilen eshte krijuar statusi i magazinave.</param>
        /// <returns>Kthen clsMesazh me atribut statusi True nese ruajtja kryhet me sukses ose False dhe mesazhin e gabimit ne te kundert.</returns>
        internal clsMesazh ruajStatusMagazina(out int idStatusMagazine, string emertimi, string pershkrimi, bool ePerdorshme, int idNdermarrja)
        {
            idStatusMagazine = -1;

            dbManager.Open();
            dbManager.CreateParameters(5);
            dbManager.AddParameters(0, "@ID_STATUS_MAGAZINE", idStatusMagazine, ParameterDirection.Output);
            dbManager.AddParameters(1, "@EMERTIMI", emertimi, ParameterDirection.Input);
            dbManager.AddParameters(2, "@PERSHKRIMI", pershkrimi, ParameterDirection.Input);
            dbManager.AddParameters(3, "@E_PERDORSHME", ePerdorshme, ParameterDirection.Input);
            dbManager.AddParameters(4, "@IDNDERMARRJE", idNdermarrja, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "PRC_T_ASETE_STATUS_MAGAZINE_INSERT");
            idStatusMagazine = int.Parse(dbManager.Parameters[0].Value.ToString());
            clsMesazh mesazh = new clsMesazh(true, "Ruatja perfundoi me sukses!");
            return mesazh;

        }

        /// <summary>
        /// MODULI ASETE:
        /// Shtimi i statuseve default automatikisht ne krijimin e ndermarrjes se re sipas procedures.
        /// </summary>
        /// <param name="idNdermarrja">(int) Id e ndermarrjes baze.</param>
        /// <param name="idNdermarjeRe">(int) Id e ndermarrjes se re qe do shohen statuset.</param>
        /// <returns>Kthen clsMesazh me atribut statusi True nese ruajtja kryhet me sukses ose False dhe mesazhin e gabimit ne te kundert.</returns>
        internal clsMesazh ruajStatusMagazinaNderamrjeRe(int idNdermarrja, int idNdermarjeRe)
        {


            dbManager.Open();
            dbManager.CreateParameters(2);

            dbManager.AddParameters(0, "@idndermarjere", idNdermarjeRe, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARRJE", idNdermarrja, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "PRC_T_ASETE_STATUS_MAGAZINE_INSERTNdermarjeRe");

            clsMesazh mesazh = new clsMesazh(true, "Ruatja perfundoi me sukses!");
            return mesazh;

        }

        /// <summary>
        /// MODULI ASETE:
        /// Modifikon ne tabelen T_ASETE_STATUS_MAGAZINE objektin e statusit te magazinave nepermjet procedures PRC_T_ASETE_STANDART_AMORT_UPDATE.
        /// </summary>
        /// <param name="idStatusMagazine">(int) Id automatike e statusit te magazinave.</param>
        /// <param name="pershkrimi">(string) Pershkrimi i statusit te magazinave.</param>
        /// <param name="ePerdorshme">(bool) True nese statusi i magazinave eshte i perdorshem ne ndermarrjen ne perdorim, False ne te kundert.</param>
        /// <returns>Kthen clsMesazh me atribut statusi True nese modifikimi kryhet me sukses ose False dhe mesazhin e gabimit ne te kundert.</returns>
        internal clsMesazh modifikimiStatusMagazina(int idStatusMagazine, string pershkrimi, bool ePerdorshme)
        {

            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@ID_STATUS_MAGAZINE", idStatusMagazine, ParameterDirection.Input);
            dbManager.AddParameters(1, "@PERSHKRIMI", pershkrimi, ParameterDirection.Input);
            dbManager.AddParameters(2, "@E_PERDORSHME", ePerdorshme, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "PRC_T_ASETE_STANDART_AMORT_UPDATE");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgModifikimiMeSukses"]);
            return mesazh;

        }

        /// <summary>
        /// MODULI ASETE:
        /// Fshin objektin e statusit te magazines ne tabelen T_ASETE_STATUS_MAGAZINE nepermjet procedures PRC_T_ASETE_STATUS_MAGAZINE_DELETE. 
        /// Nuk fshihen dot 4 statuset default INAKTIVE, AKTIVE, RIPARIM, DEINSTALIM.
        /// </summary>
        /// <param name="idStatusMagazine">(int) Id automatike e statusit te magazinave.</param>
        /// <returns>Kthen clsMesazh me atribut statusi True nese fshirja kryhet me sukses ose False dhe mesazhin e gabimit ne te kundert.</returns>
        internal clsMesazh fshiStatusMagazina(int idStatusMagazine)
        {

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@ID_STATUS_MAGAZINE", idStatusMagazine, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "PRC_T_ASETE_STATUS_MAGAZINE_DELETE");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
            return mesazh;

        }

        /// <summary>
        /// MODULI ASETE:
        /// Kthen nga databaza nepermjet procedures PRC_T_ASETE_STATUS_MAGAZINE_SEL_PERDORSHME_SIPASNDERMARRJE nje DataTable me te gjitha statuset e pergjithshme ne perdorim te magazines per ndermarrjen ne perdorim.
        /// </summary>
        /// <param name="idNdermarrja">(int) Id e ndermarrjes ne perdorim per te cilen do te merren statuset e pergjithshme te magazines.</param>
        /// <returns>Kthen nje DataTable me statuset e pergjithshme ne perdorim te magazines nga tabela T_ASETE_STATUS_MAGAZINE.</returns>
        internal DataTable ktheStatusMagazineTePerdorshmeTeNdermarrjes(int idNdermarrja)
        {

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDNDERMARRJE", idNdermarrja, ParameterDirection.Input);

            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "PRC_T_ASETE_STATUS_MAGAZINE_SEL_PERDORSHME_SIPASNDERMARRJE");
            return ds.Tables[0];

        }

        /// <summary>
        /// MODULI ASETE:
        /// Kthen nga databaza nepermjet procedures PRC_T_ASETE_STATUS_MAGAZINE_SEL_SIPASNDERMARRJE nje DataTable me te gjitha statuset e pergjithshme te magazines per ndermarrjen ne perdorim.
        /// </summary>
        /// <param name="idNdermarrja">(int) Id e ndermarrjes ne perdorim per te cilen do te merren statuset e pergjithshme te magazines.</param>
        /// <returns>Kthen nje DataTable me statuset e pergjithshme te magazines nga tabela T_ASETE_STATUS_MAGAZINE.</returns>
        internal DataTable ktheStatusMagazineTeNdermarrjes(int idNdermarrja)
        {

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDNDERMARRJE", idNdermarrja, ParameterDirection.Input);

            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "PRC_T_ASETE_STATUS_MAGAZINE_SEL_SIPASNDERMARRJE");
            return ds.Tables[0];

        }

        /// <summary>
        /// MODULI ASETE:
        /// Kthen nga databaza nepermjet procedures PRC_T_ASETE_STATUS_MAGAZINE_SEL_PERDORSHME_SIPASID nje DataRow me statusin e pergjithshem te magazines per id e statusit.
        /// </summary>
        /// <param name="idStatusMagazine">(int) Id e statusit te pergjithshem te ruajtur ne databaze.</param>
        /// <returns>Kthen nje DataRow me statusin e pergjithshem te magazines nga tabela T_ASETE_STATUS_MAGAZINE.</returns>
        internal DataRow ktheStatusMagazineSipasID(int idStatusMagazine)
        {

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@ID_STATUS_MAGAZINE", idStatusMagazine, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "PRC_T_ASETE_STATUS_MAGAZINE_SEL_PERDORSHME_SIPASID");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];

        }

        /// <summary>
        /// MODULI ASETE:
        /// Kthen nga databaza nepermjet procedures PRC_T_ASETE_STATUS_MAGAZINE_SEL_PERDORSHME_SIPASEMERTIMI_NENDERMARRJE nje DataRow me statusin e pergjithshem te magazines per emertimin e kerkuar ne ndermarrjen ne perdorim.
        /// </summary>
        /// <param name="emertimi">(string) Emertimi i statusit te magazinave qe kerkojme.</param>
        /// <param name="idNdermarrja">(int) Id e ndermarrjes ne perdorim per te cilen do te merret statusi i magazinave.</param>
        /// <returns>Kthen nje DataRow me statusin e magazinave nga tabela T_ASETE_STATUS_MAGAZINE.</returns>
        internal DataRow ktheStatusMagazineTeNdermarrjesSipasEmertimit(string emertimi, int idNdermarrja)
        {

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@EMERTIMI", emertimi, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARRJE", idNdermarrja, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "PRC_T_ASETE_STATUS_MAGAZINE_SEL_PERDORSHME_SIPASEMERTIMI_NENDERMARRJE");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];

        }
        internal DataRow ktheStatusMagazineTeNdermarrjesSipasEmertimitPaVaresishtEPerdorshme(string emertimi, int idNdermarrja)
        {

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@EMERTIMI", emertimi, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARRJE", idNdermarrja, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "PRC_T_ASETE_STATUS_MAGAZINE_SEL_SIPASEMERTIMI_NENDERMARRJE");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];

        }
        /// <summary>
        /// MODULI ASETE:
        /// Kthen nga databaza nepermjet procedures PRC_T_ASETE_STATUS_MAGAZINE_SEL_PERDORSHME_SIPASEMERTIMI_NENDERMARRJE nje DataRow me statusin e magazinave per emertimin e statusit ne ndermarrjen ne perdorim per te marre id e statusit te magazinave. 
        /// </summary>
        /// <param name="emertimi">(string) Emertimi i statusit te magazinave qe kerkojme.</param>
        /// <param name="idNdermarrja">(int) Id e ndermarrjes ne perdorim per te cilen do te merret statusi i magazinave.</param>
        /// <returns>Kthen nje int me id e statuseve te magazinave nga tabela T_ASETE_STATUS_MAGAZINE.</returns>
        internal int ktheIDStatusMagazinesTeNdermarrjesSipasEmertimit(string emertimi, int idNdermarrja)
        {

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@EMERTIMI", emertimi, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARRJE", idNdermarrja, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "PRC_T_ASETE_STATUS_MAGAZINE_SEL_PERDORSHME_SIPASEMERTIMI_NENDERMARRJE");
            if (ds == null)
                return -1;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return -1;

            int idStatusMagazine;
            int.TryParse(ds.Tables[0].Rows[0]["ID_STATUS_MAGAZINE"].ToString(), out idStatusMagazine);
            return idStatusMagazine;

        }

        /// <summary>
        /// MODULI ASETE:
        /// Kthen nga databaza nepermjet procedures PRC_T_ASETE_STATUS_MAGAZINE_SEL_PERDORSHME_SIPASEMERTIMI_NENDERMARRJE nje DataRow me statusin e magazinave per id e statusit ne ndermarrjen ne perdorim per te marre emertimin e statusit te magazinave. 
        /// </summary>
        /// <param name="idStatusMagazina">(int) Id e statusit te magazines qe kerkojme.</param>
        /// <param name="idNdermarrja">(int) Id e ndermarrjes ne perdorim per te cilen do te kontrollohet nese ekziston emertimi i statusit.</param>
        /// <returns>Kthen nje string me emertimin e statuseve te magazinave nga tabela T_ASETE_STATUS_MAGAZINE.</returns>
        internal string ktheEmertimStatusMagazinesTeNdermarrjesSipasIDStatus(int idStatusMagazina, int idNdermarrja)
        {

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@ID_STATUS_MAGAZINE", idStatusMagazina, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARRJE", idNdermarrja, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "PRC_T_ASETE_STATUS_MAGAZINE_SEL_PERDORSHME_SIPASIDSTATUS_NENDERMARRJE");
            if (ds == null)
                return "";
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return "";

            string emertimStatusMagazine = ds.Tables[0].Rows[0]["EMERTIMI"].ToString();
            return emertimStatusMagazine;

        }

        /// <summary>
        /// MODULI ASETE:
        /// Kthen nga databaza nepermjet procedures PRC_T_ASETE_STATUS_MAGAZINE_SEL_SIPASEMERTIMI_NENDERMARRJE nje DataRow me statusin e magazinave per emertimin e statusit ne ndermarrjen ne perdorim per te pare nese ekziston emertimi i statusit te magazinave njehere i regjistruar.
        /// </summary>
        /// <param name="emertimi">(string) Emertimi i statusit te magazinave qe kerkojme.</param>
        /// <param name="idNdermarrja">(int) Id e ndermarrjes ne perdorim per te cilen do te merret statusi i magazinave.</param>
        /// <returns>Kthen True nese ekziston njehere emertimi i kerkuar per ndermarrjen dhe False ne te kundert.</returns>
        internal bool ekzistonStatusMagazines(string emertimi, int idNdermarrja)
        {

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@EMERTIMI", emertimi, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARRJE", idNdermarrja, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "PRC_T_ASETE_STATUS_MAGAZINE_SEL_SIPASEMERTIMI_NENDERMARRJE");
            if (ds == null)
                return true;
            if (ds.Tables[0].Rows.Count == 0)
                return false;
            return true;

        }

        #endregion

        #region Konfigurimi i standarteve

        /// <summary>
        /// MODULI ASETE:
        /// Ruan ne tabelen T_ASETE_LIDHJE_STANDART_STATUSMAG objektin e konfigurimit te standartit nepermjet procedures PRC_T_ASETE_LIDHJE_STANDART_STATUSMAG_INSERT.
        /// </summary>
        /// <param name="idKarakteristika">(int) Id automatike e karakteristikave te standartit.</param>
        /// <param name="idStandart">(int) Id e standartit per te cilen eshte kryer konfigurimi.</param>
        /// <param name="idKodifikimArtikulli">(int) Id e grupit per te cilen eshte kryer konfigurimi.</param>
        /// <param name="idKonfig">(int) Id e konfigurimit te elementeve ne User Interface.</param>
        /// <param name="idFillimAmortizimi">(int) Id e periudhes se fillimit te amortizimit.</param>
        /// <param name="idMbarimAmortizimi">(int) Id e periudhes se mbarimit te amortizimit.</param>
        /// <param name="perfshihetDitaPare">(bool) True nese do te perfshihet dita e dokumentit te blerjes ose hyrjes ne amortizimit ose False nese jo.</param>
        /// <param name="kontabilizim">(bool) True nese eshte konfigurim standarti qe do dhe krijim dokumenti ne kontabilizim ose False ne te kundert.</param>
        /// <param name="idNdermarrje">(int) Id e ndermarrjes ne perdorim qe ka konfigurimin e standartit.</param>
        /// <param name="idStatusDokumenti">(int) Gjendja qe eshte konfigurimi i standartit, i ruajtur(1), fshire(2), apo modifikuar.</param>
        /// <param name="idPerdoruesi">(int) Id e perdoruesit qe ka modifikuar per here te fundit konfigurimin e standartit.</param>
        /// <param name="idKrijuesi">(int) Id e krijuesit te konfigurimit te standartit.</param>
        /// <returns>Kthen clsMesazh me atribut statusi True nese ruajtja kryhet me sukses ose False dhe mesazhin e gabimit ne te kundert.</returns>
        internal clsMesazh ruajKonfigurimStandarti(out int idKarakteristika, int idStandart, int idKodifikimArtikulli, int idKonfig, int idFillimAmortizimi, int idMbarimAmortizimi, bool perfshihetDitaPare, bool kontabilizim, int idNdermarrje, int idStatusDokumenti, int idPerdoruesi, int idKrijuesi)
        {
            idKarakteristika = -1;

            dbManager.Open();
            dbManager.CreateParameters(12);
            dbManager.AddParameters(0, "@ID_STANDART_STATUSMAG", idKarakteristika, ParameterDirection.Output);
            dbManager.AddParameters(1, "@IDSTANDARTI", idStandart, ParameterDirection.Input);
            if (idKodifikimArtikulli < 1) dbManager.AddParameters(2, "@IDKODIFIKIMARTIKULLI", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(2, "@IDKODIFIKIMARTIKULLI", idKodifikimArtikulli, ParameterDirection.Input);
            dbManager.AddParameters(3, "@IDKONFIG", idKonfig, ParameterDirection.Input);
            dbManager.AddParameters(4, "@IDFILLIMIAMORTIZIMMUJOR", idFillimAmortizimi, ParameterDirection.Input);
            dbManager.AddParameters(5, "@IDMBARIMAMORTIZMIMUJOR", idMbarimAmortizimi, ParameterDirection.Input);
            dbManager.AddParameters(6, "@PERFSHIHET_DITA_PARE", perfshihetDitaPare, ParameterDirection.Input);
            dbManager.AddParameters(7, "@KONTABILIZIM", kontabilizim, ParameterDirection.Input);
            dbManager.AddParameters(8, "@IDNDERMARRJE", idNdermarrje, ParameterDirection.Input);
            dbManager.AddParameters(9, "@IDSTATUSDOK", idStatusDokumenti, ParameterDirection.Input);
            dbManager.AddParameters(10, "@IDPERDORUESI", idPerdoruesi, ParameterDirection.Input);
            dbManager.AddParameters(11, "@IDKRIJUESI", idKrijuesi, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "PRC_T_ASETE_LIDHJE_STANDART_STATUSMAG_INSERT");
            idKarakteristika = int.Parse(dbManager.Parameters[0].Value.ToString());
            clsMesazh mesazh = new clsMesazh(true, "Ruatja perfundoi me sukses!");
            return mesazh;

        }

        /// <summary>
        /// MODULI ASETE:
        /// Ruan konfigurimet standarte ne ndermarrjen e re qe krijohet sipas procedures PRC_T_ASETE_LIDHJE_STANDART_STATUSMAG_INSERTneNdermarjeRe.
        /// </summary>
        /// <param name="idKarakteristika">(int) Id automatike e karakteristikave te standartit.</param>
        /// <param name="idkodifikim">(int) Id e ndermarrjes baze.</param>
        /// <param name="idndermarjeRe">(int) Id e ndermarrjes se re qe do shohen statuset.</param>
        /// <param name="idPerdoruesi">(int) Id e perdoruesit qe kryen konfigurimin ne ndermarrjen e re.</param>
        /// <returns>Kthen clsMesazh me atribut statusi True nese ruajtja kryhet me sukses ose False dhe mesazhin e gabimit ne te kundert.</returns>
        internal clsMesazh ruajKonfigurimStandartiNdermarjeRe(out int idKarakteristika, int idkodifikim, int idndermarjeRe, int idPerdoruesi)
        {
            idKarakteristika = -1;

            dbManager.Open();
            dbManager.CreateParameters(4);
            dbManager.AddParameters(0, "@ID_STANDART_STATUSMAG", idKarakteristika, ParameterDirection.Output);

            dbManager.AddParameters(1, "@idkodifikim", idkodifikim, ParameterDirection.Input);
            dbManager.AddParameters(2, "@idndermarjere", idndermarjeRe, ParameterDirection.Input);
            dbManager.AddParameters(3, "@IDPERDORUESI", idPerdoruesi, ParameterDirection.Input);

            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "PRC_T_ASETE_LIDHJE_STANDART_STATUSMAG_INSERTneNdermarjeRe");
            idKarakteristika = int.Parse(dbManager.Parameters[0].Value.ToString());
            clsMesazh mesazh = new clsMesazh(true, "Ruatja perfundoi me sukses!");
            return mesazh;

        }

        /// <summary>
        /// MODULI ASETE:
        /// Modifikon ne tabelen T_ASETE_LIDHJE_STANDART_STATUSMAG objektin e konfigurimit te standartit nepermjet procedures PRC_T_ASETE_LIDHJE_STANDART_STATUSMAG_UPDATE.
        /// </summary>
        /// <param name="idKarakteristika">(int) Id automatike e karakteristikave te standartit.</param>
        /// <param name="idFillimAmortizimi">(int) Id e periudhes se fillimit te amortizimit.</param>
        /// <param name="idMbarimAmortizimi">(int) Id e periudhes se mbarimit te amortizimit.</param>
        /// <param name="perfshihetDitaPare">(bool) True nese do te perfshihet dita e dokumentit te blerjes ose hyrjes ne amortizimit ose False nese jo.</param>
        /// <param name="kontabilizim">(bool) True nese eshte konfigurim standarti qe do dhe krijim dokumenti ne kontabilizim ose False ne te kundert.</param>
        /// <param name="idStatusDokumenti">(int) Gjendja qe eshte konfigurimi i standartit, i ruajtur(1), fshire(2), apo modifikuar.</param>
        /// <param name="idPerdoruesi">(int) Id e perdoruesit qe ka modifikuar per here te fundit konfigurimin e standartit.</param>
        /// <returns>Kthen clsMesazh me atribut statusi True nese modifikimi kryhet me sukses ose False dhe mesazhin e gabimit ne te kundert.</returns>
        internal clsMesazh modifikimiKonfigurimStandarti(int idKarakteristika, int idFillimAmortizimi, int idMbarimAmortizimi, bool perfshihetDitaPare, bool kontabilizim, int idStatusDokumenti, int idPerdoruesi)
        {

            dbManager.Open();
            dbManager.CreateParameters(7);
            dbManager.AddParameters(0, "@ID_STANDART_STATUSMAG", idKarakteristika, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDFILLIMIAMORTIZIMMUJOR", idFillimAmortizimi, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDMBARIMAMORTIZMIMUJOR", idMbarimAmortizimi, ParameterDirection.Input);
            dbManager.AddParameters(3, "@PERFSHIHET_DITA_PARE", perfshihetDitaPare, ParameterDirection.Input);
            dbManager.AddParameters(4, "@KONTABILIZIM", kontabilizim, ParameterDirection.Input);
            dbManager.AddParameters(5, "@IDSTATUSDOK", idStatusDokumenti, ParameterDirection.Input);
            dbManager.AddParameters(6, "@IDPERDORUESI", idPerdoruesi, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "PRC_T_ASETE_LIDHJE_STANDART_STATUSMAG_UPDATE");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgModifikimiMeSukses"]);
            return mesazh;

        }

        /// <summary>
        /// MODULI ASETE:
        /// Fshin objektin e konfigurimit te standartit ne tabelen T_ASETE_LIDHJE_STANDART_STATUSMAG nepermjet procedures PRC_T_ASETE_STATUS_MAGAZINE_DELETE. 
        /// </summary>
        /// <param name="idKarakteristika">(int) Id automatike e karakteristikave te standartit.</param>
        /// <returns>Kthen clsMesazh me atribut statusi True nese fshirja kryhet me sukses ose False dhe mesazhin e gabimit ne te kundert.</returns>
        internal clsMesazh fshiKonfigurimStandarti(int idKarakteristika)
        {

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@ID_STANDART_STATUSMAG", idKarakteristika, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "PRC_T_ASETE_STATUS_MAGAZINE_DELETE");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
            return mesazh;

        }

        /// <summary>
        /// MODULI ASETE:
        /// Fshin karaktaristikat e standarti i amortizimit duke i ndryshuar statusin.
        /// </summary>
        /// <param name="idKarakteristika">(int) Id automatike e karakteristikave te standartit.</param>
        /// <param name="idperdoruesi">(int) Id e perdoruesit qe ka modifikuar per here te fundit konfigurimin e standartit.</param>
        /// <returns>Kthen clsMesazh me atribut statusi True nese fshirja kryhet me sukses ose False dhe mesazhin e gabimit ne te kundert.</returns>
        internal clsMesazh fshiKonfigurimStandartiStatus(int idKarakteristika, int idperdoruesi)
        {

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@ID_STANDART_STATUSMAG", idKarakteristika, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUESI", idperdoruesi, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_ASETE_LIDHJE_STANDART_STATUSMAG_upddel");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
            return mesazh;

        }

        /// <summary>
        /// MODULI ASETE:
        /// Fshin standartin e amortizimit te lidhur me nje kodifikim artikulli duke i ndryshuar statusin.
        /// </summary>
        /// <param name="idKofifikimArtikulli">(int) id e kodifikimit te artikullit te lidhur me standartin.</param>
        /// <param name="idperdoruesi">(int) Id e perdoruesit qe ka modifikuar per here te fundit konfigurimin e standartit.</param>
        /// <returns>Kthen clsMesazh me atribut statusi True nese fshirja kryhet me sukses ose False dhe mesazhin e gabimit ne te kundert.</returns>
        public clsMesazh fshiKonfigurimStandartiStatusSipasKodifilimArt(int idKofifikimArtikulli, int idperdoruesi)
        {

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDKODIFIKIMI", idKofifikimArtikulli, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUESI", idperdoruesi, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_ASETE_LIDHJE_STANDART_STATUSMAG_upddelSipasIdKodifikimArt");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
            return mesazh;

        }

        /// <summary>
        /// MODULI ASETE:
        /// Kthen nga databaza nepermjet procedures PRC_T_ASETE_LIDHJE_STANDART_STATUSMAG_SEL_SIPASIDNDERAMRJEDT nje DataTable me te gjitha konfigurimet e standarteve per ndermarrjen ne perdorim. Perdorur me emra konvencional ne store procedure.
        /// </summary>
        /// <param name="idnderm">(int) Id e ndermarrjes ne perdorim per te cilen eshte specifikuar konfigurimi i standartit.</param>
        /// <returns>Kthen nje DataTable me konfigurimet e standarteve nga tabela T_ASETE_LIDHJE_STANDART_STATUSMAG.</returns>
        internal DataTable merrSipasKonfigurimiStandartitDT(int idnderm)
        {

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDNDERMARRJE", idnderm, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "PRC_T_ASETE_LIDHJE_STANDART_STATUSMAG_SEL_SIPASIDNDERAMRJEDT");

            return ds.Tables[0];

        }

        /// <summary>
        /// MODULI ASETE:
        /// Kthen nga databaza nepermjet procedures PRC_T_ASETE_LIDHJE_STANDART_STATUSMAG_SEL_SIPASIDNDERAMRJEDT nje DataTable me te gjitha konfigurimet e standarteve per ndermarrjen ne perdorim.
        /// </summary>
        /// <param name="idNderm">(int) Id e ndermarrjes ne perdorim per te cilen eshte specifikuar konfigurimi i standartit.</param>
        /// <returns>Kthen nje DataTable me konfigurimet e standarteve nga tabela T_ASETE_LIDHJE_STANDART_STATUSMAG.</returns>
        internal DataTable ktheGjitheKonfigurimeStandartit(int idNderm)
        {

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDNDERMARRJE", idNderm, ParameterDirection.Input);

            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "PRC_T_ASETE_LIDHJE_STANDART_STATUSMAG_SEL_SIPASIDNDERAMRJE");
            return ds.Tables[0];

        }

        /// <summary>
        /// MODULI ASETE:
        /// Kthen nga databaza nepermjet procedures PRC_T_ASETE_LIDHJE_STANDART_STATUSMAG_SEL_SIPASID nje DataRow me konfigurimin e standartit per id e konfigurimit te standartit.
        /// </summary>
        /// <param name="idKonfigurimStandarti">(int) Id e konfigurimit te standartit te ruajtur ne databaze.</param>
        /// <returns>Kthen nje DataRow me konfigurimin e standartit nga tabela T_ASETE_LIDHJE_STANDART_STATUSMAG.</returns>
        internal DataRow ktheKonfigurimStandartiSipasID(int idKonfigurimStandarti)
        {

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@ID_STANDART_STATUSMAG", idKonfigurimStandarti, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "PRC_T_ASETE_LIDHJE_STANDART_STATUSMAG_SEL_SIPASID");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];

        }

        /// <summary>
        /// MODULI ASETE:
        /// Kthen nga databaza nepermjet procedures PRC_T_ASETE_LIDHJE_STANDART_STATUSMAG_SEL_SIPASNDER_STAT_GRUP_STAND nje DataRow me konfigurimin e standartit per karakteristikat unike te tij.
        /// </summary>
        /// <param name="idGrup">(int) Id e grupit per te cilen eshte specifikuar konfigurimi i standartit.</param>
        /// <param name="idStandart">(int) Id e standartit per te cilen eshte specifikuar konfigurimi i standartit.</param>
        /// <param name="idNdermarrja">(int) Id e ndermarrjes ne perdorim per te cilen eshte specifikuar konfigurimi i standartit.</param>
        /// <returns>Kthen nje DataRow me konfigurimin e standartit nga tabela T_ASETE_LIDHJE_STANDART_STATUSMAG.</returns>
        internal DataRow ktheKonfigurimStandartiTeNdermarrjesSipasStatGrupStandart(int idGrup, int idStandart, int idNdermarrja)
        {

            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@IDSTANDARTI", idStandart, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDKODIFIKIMARTIKULLI", idGrup, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDNDERMARRJE", idNdermarrja, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "PRC_T_ASETE_LIDHJE_STANDART_STATUSMAG_SEL_SIPASNDER_STAT_GRUP_STAND");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];

        }
        internal IEnumerable<clsKarakteristikaStandarti> merrKarakteristikaSipasNdermarrjesMeKontabilizim(int idNdermarrje)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddInputParameters("@idNdermarrje", idNdermarrje);
            return dbManager.GetIEnumerbale("prc_T_ASETE_LIDHJE_STANDART_STATUSMAG_merrAtoMeKonta", clsKarakteristikaStandarti.KrijoBasic);
        }
        /// <summary>
        /// MODULI ASETE:
        /// Kthen konfigurimin sipas id se karakteristikes sipas metodes PRC_T_ASETE_LIDHJE_STANDART_STATUSMAG_SEL_SIPASIDNDERAMRJEDR. Perdorur me emra konvencional ne store procedure.
        /// </summary>
        /// <param name="idKarakteristika">(int) Id automatike e karakteristikave te standartit.</param>
        /// <returns>Kthen nje DataRow me konfigurimin e standartit nga tabela T_ASETE_LIDHJE_STANDART_STATUSMAG.</returns>
        internal DataRow merrSipasKonfigurimiStandartitDR(int idKarakteristika)
        {

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@ID_STANDART_STATUSMAG", idKarakteristika, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "PRC_T_ASETE_LIDHJE_STANDART_STATUSMAG_SEL_SIPASIDNDERAMRJEDR");

            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];

        }

        /// <summary>
        /// MODULI ASETE:
        /// Kthen nga databaza nepermjet procedures PRC_T_ASETE_LIDHJE_STANDART_STATUSMAG_SEL_SIPASNDER_STAT_GRUP_STAND nje DataRow me konfigurimin e standartit ne ndermarrjen ne perdorim per karakteristikat unike te tij. 
        /// </summary>
        /// <param name="idGrup">(int) Id e grupit per te cilen eshte specifikuar konfigurimi i standartit.</param>
        /// <param name="idStandart">(int) Id e standartit per te cilen eshte specifikuar konfigurimi i standartit.</param>
        /// <param name="idNdermarrja">(int) Id e ndermarrjes ne perdorim per te cilen eshte specifikuar konfigurimi i standartit.</param>
        /// <returns>Kthen nje int me id e konfigurimit te standartit nga tabela T_ASETE_LIDHJE_STANDART_STATUSMAG.</returns>
        internal int ktheIDKonfigurimStandartiTeNdermarrjesSipasStatGrupStandart(int idGrup, int idStandart, int idNdermarrja)
        {

            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@IDSTANDARTI", idStandart, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDKODIFIKIMARTIKULLI", idGrup, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDNDERMARRJE", idNdermarrja, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "PRC_T_ASETE_LIDHJE_STANDART_STATUSMAG_SEL_SIPASNDER_STAT_GRUP_STAND");
            if (ds == null)
                return -1;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return -1;

            int idKonfigurimStandarti;
            int.TryParse(ds.Tables[0].Rows[0]["ID_STANDART_STATUSMAG"].ToString(), out idKonfigurimStandarti);
            return idKonfigurimStandarti;

        }

        /// <summary>
        /// MODULI ASETE:
        /// Kthen nga databaza nepermjet procedures PRC_T_ASETE_LIDHJE_STANDART_STATUSMAG_SEL_SIPASNDER_STAT_GRUP_STAND nje DataRow me konfigurimin e standartit sipas karakteristikave ne ndermarrjen ne perdorim per te pare nese ekziston konfigurimi i standartit njehere i regjistruar.
        /// </summary>
        /// <param name="idGrup">(int) Id e grupit per te cilen eshte specifikuar konfigurimi i standartit.</param>
        /// <param name="idStandart">(int) Id e standartit per te cilen eshte specifikuar konfigurimi i standartit.</param>
        /// <param name="idNdermarrja">(int) Id e ndermarrjes ne perdorim per te cilen eshte specifikuar konfigurimi i standartit.</param>
        /// <returns>Kthen True nese ekziston njehere konfigurimi i standartit i kerkuar per ndermarrjen dhe False ne te kundert.</returns>
        internal bool ekzistonKonfigurimiStandartit(int idGrup, int idStandart, int idNdermarrja)
        {

            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@IDSTANDARTI", idStandart, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDKODIFIKIMARTIKULLI", idGrup, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDNDERMARRJE", idNdermarrja, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "PRC_T_ASETE_LIDHJE_STANDART_STATUSMAG_SEL_SIPASNDER_STAT_GRUP_STAND");
            if (ds == null)
                return true;
            if (ds.Tables[0].Rows.Count == 0)
                return false;
            return true;

        }

        /// <summary>
        /// MODULI ASETE:
        /// Kthen nga databaza nepermjet procedures PRC_T_ASETE_LIDHJE_STANDART_STATUSMAG_SEL_SIPASIDSTANDART nje DataRow me konfigurimin e standartit sipas id se standartit per te pare nese ka veprime qe perdorin id e standartit qe ne kerkojme.
        /// </summary>
        /// <param name="idStandart">(int) Id e standartit per te cilen eshte specifikuar konfigurimi i standartit.</param>
        /// <returns>Kthen True nese ekziston nje ose me shume konfigurime standartesh me id e standartit te kerkuar dhe False ne te kundert.</returns>
        internal bool ekzistonKonfigurimiStandartitSipasIdStandart(int idStandart)
        {

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDSTANDARTI", idStandart, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "PRC_T_ASETE_LIDHJE_STANDART_STATUSMAG_SEL_SIPASIDSTANDART");
            if (ds == null)
                return true;
            if (ds.Tables[0].Rows.Count == 0)
                return false;
            return true;

        }

        #endregion

        #region Konfigurimi i standarteve trupi

        /// <summary>
        /// MODULI ASETE:
        /// Ruan ne tabelen T_ASETE_LIDHJE_STANDART_STATUSMAG_TRUPI objektin e trupit te konfigurimit te standartit nepermjet procedures PRC_T_ASETE_LIDHJE_STANDART_STATUSMAG_TRUPI_INSERT.
        /// </summary>
        /// <param name="idTrupiKarakteristikStandart">(int) Id automatike e trupit te karakteristikave te standartit.</param>
        /// <param name="idKokaKarakteristikStandart">(int) Id e kokes se karakteristikave te standartit.</param>
        /// <param name="idStatusMagazine">(int) Id e statusit te magazines per te cilen eshte kryer konfigurimi.</param>
        /// <param name="llogaritAmortizim">(bool) True nese duhet qe te llogaritet amortizimin kete kete konfigurim te bere ose False ne te kundert.</param>
        /// <param name="filloAmortiziminPas">(int) Muajt pas sa kohes nga blerja ose hyrja ne magazine do te filloje amortizimi.</param>
        /// <returns>Kthen True nese ruajtja kryhet me sukses ose False ne te kundert.</returns>
        internal clsMesazh ruajKonfigurimTrupiStandarti(out int idTrupiKarakteristikStandart, int idKokaKarakteristikStandart, int idStatusMagazine, bool llogaritAmortizim, int filloAmortiziminPas)
        {
            idTrupiKarakteristikStandart = -1;

            dbManager.Open();
            dbManager.CreateParameters(5);
            dbManager.AddParameters(0, "@ID_STANDART_STATUSMAG_TRUPI", idTrupiKarakteristikStandart, ParameterDirection.Output);
            dbManager.AddParameters(1, "@ID_STANDART_STATUSMAG_KOKA", idKokaKarakteristikStandart, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDSTATUSMAGAZINE", idStatusMagazine, ParameterDirection.Input);
            dbManager.AddParameters(3, "@LLOGARIT_AMORTIZIM", llogaritAmortizim, ParameterDirection.Input);
            dbManager.AddParameters(4, "@FILLO_AMORTIZIM_PAS", filloAmortiziminPas, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "PRC_T_ASETE_LIDHJE_STANDART_STATUSMAG_TRUPI_INSERT");
            idTrupiKarakteristikStandart = int.Parse(dbManager.Parameters[0].Value.ToString());
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
            return mesazh;

        }

        /// <summary>
        /// MODULI ASETE:
        /// Modifikon ne tabelen T_ASETE_LIDHJE_STANDART_STATUSMAG_TRUPI objektin e konfigurimit te standartit nepermjet procedures PRC_T_ASETE_LIDHJE_STANDART_STATUSMAG_TRUPI_UPDATE.
        /// </summary>
        /// <param name="idKokaKarakteristikStandart">(int) Id e kokes se karakteristikave te standartit.</param>
        /// <param name="idStatusMagazine">(int) Id e statusit te magazines per te cilen eshte kryer konfigurimi.</param>
        /// <param name="llogaritAmortizim">(bool) True nese duhet qe te llogaritet amortizimin kete kete konfigurim te bere ose False ne te kundert.</param>
        /// <param name="filloAmortiziminPas">(int) Muajt pas sa kohes nga blerja ose hyrja ne magazine do te filloje amortizimi.</param>
        /// <returns>Kthen True nese modifikimi kryhet me sukses ose False ne te kundert.</returns>
        internal clsMesazh modifikimiKonfigurimTrupiStandarti(int idKokaKarakteristikStandart, int idStatusMagazine, bool llogaritAmortizim, int filloAmortiziminPas)
        {
            dbManager.Open();
            dbManager.CreateParameters(4);
            dbManager.AddParameters(0, "@ID_STANDART_STATUSMAG_KOKA", idKokaKarakteristikStandart, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDSTATUSMAGAZINE", idStatusMagazine, ParameterDirection.Input);
            dbManager.AddParameters(2, "@LLOGARIT_AMORTIZIM", llogaritAmortizim, ParameterDirection.Input);
            dbManager.AddParameters(3, "@FILLO_AMORTIZIM_PAS", filloAmortiziminPas, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "PRC_T_ASETE_LIDHJE_STANDART_STATUSMAG_TRUPI_UPDATE");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgModifikimiMeSukses"]);
            return mesazh;
        }

        /// <summary>
        /// MODULI ASETE:
        /// Fshin objektin e konfigurimit te standartit ne tabelen T_ASETE_LIDHJE_STANDART_STATUSMAG_TRUPI nepermjet procedures PRC_T_ASETE_LIDHJE_STANDART_STATUSMAG_TRUPI_DELETE. 
        /// </summary>
        /// <param name="idKokaKarakteristikStandart">(int) Id e kokes se karakteristikave te standartit.</param>
        /// <returns>Kthen True nese fshirja kryhet me sukses ose False ne te kundert.</returns>
        internal clsMesazh fshiKonfigurimTrupiStandarti(int idKokaKarakteristikStandart)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@ID_STANDART_STATUSMAG_KOKA", idKokaKarakteristikStandart, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "PRC_T_ASETE_LIDHJE_STANDART_STATUSMAG_TRUPI_DELETE");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
            return mesazh;
        }

        /// <summary>
        /// MODULI ASETE:
        /// Kthen nga databaza nepermjet procedures PRC_T_ASETE_LIDHJE_STANDART_STATUSMAG_TRUPI_SEL_SIPASIDKOKA nje DataTable me trupin e konfigurimit te standartit per id e konfigurimit te standartit.
        /// </summary>
        /// <param name="idKokaKarakteristikStandart">(int) Id e kokes se karakteristikave te standartit.</param>
        /// <returns>Kthen nje DataRow me konfigurimin e standartit nga tabela T_ASETE_LIDHJE_STANDART_STATUSMAG_TRUPI.</returns>
        internal DataTable ktheKonfigurimStandartiTrupiSipasIDKoka(int idKokaKarakteristikStandart)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@ID_STANDART_STATUSMAG_KOKA", idKokaKarakteristikStandart, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "PRC_T_ASETE_LIDHJE_STANDART_STATUSMAG_TRUPI_SEL_SIPASIDKOKA");
            return ds.Tables[0];
        }

        /// <summary>
        /// MODULI ASETE:
        /// Kthen nga databaza nepermjet procedures PRC_T_ASETE_LIDHJE_STANDART_STATUSMAG_TRUPI_SEL_SIPASIDKOKA_IDSTATUS nje DataRow me trupin e konfigurimit te standartit per id e konfigurimit te standartit dhe status e magazines.
        /// </summary>
        /// <param name="idKokaKarakteristikStandart">(int) Id e kokes se karakteristikave te standartit.</param>
        /// <param name="idStatusMagazine">(int) Id e statusit te magazines.</param>
        /// <returns>Kthen nje DataRow me konfigurimin e standartit dhe statusin e magazines nga tabela T_ASETE_LIDHJE_STANDART_STATUSMAG_TRUPI.</returns>
        internal DataRow ktheKonfigurimStandartiTrupiSipasIDKokaIDStatus(int idKokaKarakteristikStandart, int idStatusMagazine)
        {

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@ID_STANDART_STATUSMAG_KOKA", idKokaKarakteristikStandart, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDSTATUSMAGAZINE", idStatusMagazine, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "PRC_T_ASETE_LIDHJE_STANDART_STATUSMAG_TRUPI_SEL_SIPASIDKOKA_IDSTATUS");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];

        }

        #endregion

        #region Historiku i statuseve te magazinave

        /// <summary>
        /// MODULI ASETE:
        /// Ruan ne tabelen T_ASETE_NJESIADMINISTRATIVE_HISTORIKU objektin e historikut te magazines nepermjet procedures PRC_T_ASETE_NJESIADMINISTRATIVE_HISTORIKU_INSERT.
        /// </summary>
        /// <param name="idHistorikuNjesiAdministrative">(int) Id automatike e historikut te statusit.</param>
        /// <param name="idNjesiAdministrative">(int) Id e njesise administrative per te cilen po ruhet historiku.</param>
        /// <param name="idStatusMagazine">(int) Id e statusit te magazines qe po behet ruajtja.</param>
        /// <param name="dataStatusit">(DateTime) Data ne te cilen hyn ne fuqi statusi i ri i magazines.</param>
        /// <param name="idStatusDokumenti">(int) Gjendja qe eshte konfigurimi i standartit, i ruajtur(1), fshire(2), apo modifikuar.</param>
        /// <param name="idPerdoruesi">(int) Id e perdoruesit qe ka modifikuar per here te fundit historikun e statusit.</param>
        /// <param name="idKrijuesi">(int) Id e krijuesit te konfigurimit te standartit.</param>
        /// <returns>Kthen clsMesazh me atribut statusi True nese ruajtja kryhet me sukses ose False dhe mesazhin e gabimit ne te kundert.</returns>
        internal clsMesazh ruajHistorikMagazine(out int idHistorikuNjesiAdministrative, int idNjesiAdministrative, int idStatusMagazine, DateTime dataStatusit, int idStatusDokumenti, int idPerdoruesi, int idKrijuesi)
        {
            idHistorikuNjesiAdministrative = 0;

            dbManager.Open();
            dbManager.CreateParameters(7);
            dbManager.AddParameters(0, "@ID_HISTORIKU_NJESI_ADMIN", idHistorikuNjesiAdministrative, ParameterDirection.Output);
            dbManager.AddParameters(1, "@ID_NJESI_ADMINISTRATIVE", idNjesiAdministrative, ParameterDirection.Input);
            dbManager.AddParameters(2, "@ID_STATUS_MAGAZINE", idStatusMagazine, ParameterDirection.Input);
            dbManager.AddParameters(3, "@DATA_STATUSIT", dataStatusit, ParameterDirection.Input);
            dbManager.AddParameters(4, "@IDSTATUSDOK", idStatusDokumenti, ParameterDirection.Input);
            dbManager.AddParameters(5, "@IDPERDORUESI", idPerdoruesi, ParameterDirection.Input);
            dbManager.AddParameters(6, "@IDKRIJUESI", idKrijuesi, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "PRC_T_ASETE_NJESIADMINISTRATIVE_HISTORIKU_INSERT");
            idHistorikuNjesiAdministrative = int.Parse(dbManager.Parameters[0].Value.ToString());
            return new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);

        }

        /// <summary>
        /// MODULI ASETE:
        /// Fshirja e historikut te statuseve duke i ndryshuar statuset ne te fshire (2).
        /// </summary>
        /// <param name="idHistorikuNjesiAdministrative">(int) Id automatike e historikut te statusit.</param>
        /// <param name="idPerdoruesi">(int) Id e perdoruesit qe ka modifikuar per here te fundit historikun e statusit.</param>
        /// <returns>Kthen clsMesazh me atribut statusi True nese modifikimi kryhet me sukses ose False dhe mesazhin e gabimit ne te kundert.</returns>
        internal clsMesazh modifikoHistorikMagazineStatus(int idHistorikuNjesiAdministrative, int idPerdoruesi)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@ID_HISTORIKU_NJESI_ADMIN", idHistorikuNjesiAdministrative, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUESI", idPerdoruesi, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "PRC_T_ASETE_NJESIADMINISTRATIVE_HISTORIKU_UPDATEDELETE");
            return new clsMesazh(true, MessagesResource.Messages["msgModifikimiMeSukses"]);

        }

        /// <summary>
        /// MODULI ASETE:
        /// Modifikimi i historikut te statuseve duke ndryshuar te njejtin status.
        /// </summary>
        /// <param name="idHistorikuNjesiAdministrative">(int) Id automatike e historikut te statusit.</param>
        /// <param name="dataStatusit">(DateTime) Data ne te cilen hyn ne fuqi statusi i ri i magazines.</param>
        /// <param name="idPerdoruesi">(int) Id e perdoruesit qe ka modifikuar per here te fundit historikun e statusit.</param>
        /// <returns>Kthen clsMesazh me atribut statusi True nese modifikimi kryhet me sukses ose False dhe mesazhin e gabimit ne te kundert.</returns>
        internal bool modifikoHistorikMagazine(int idHistorikuNjesiAdministrative, DateTime dataStatusit, int idPerdoruesi)
        {

            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@ID_HISTORIKU_NJESI_ADMIN", idHistorikuNjesiAdministrative, ParameterDirection.Input);
            dbManager.AddParameters(1, "@DATA_STATUSIT", dataStatusit, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDPERDORUESI", idPerdoruesi, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "PRC_T_ASETE_NJESIADMINISTRATIVE_HISTORIKU_UPDATE");
            return true;

        }

        /// <summary>
        /// MODULI ASETE:
        /// Fshin objektin e konfigurimit te standartit ne tabelen T_ASETE_LIDHJE_STANDART_STATUSMAG nepermjet procedures PRC_T_ASETE_STATUS_MAGAZINE_DELETE. 
        /// </summary>
        /// <param name="idHistorikuNjesiAdministrative">(int) Id automatike e historikut te statusit.</param>
        /// <returns>Kthen clsMesazh me atribut statusi True nese fshirja kryhet me sukses ose False dhe mesazhin e gabimit ne te kundert.</returns>
        internal bool fshiHistorikMagazine(int idHistorikuNjesiAdministrative)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@ID_HISTORIKU_NJESI_ADMIN", idHistorikuNjesiAdministrative, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "PRC_T_ASETE_NJESIADMINISTRATIVE_HISTORIKU_DELETE");
            return true;
        }

        /// <summary>
        /// MODULI ASETE:
        /// Kthen nga databaza nepermjet procedures PRC_T_ASETE_NJESIADMINISTRATIVE_HISTORIKU_SEL_SIPASIDNJESIADMINISTRATIVE nje DataTable me te gjithe historikun e magazines qe ne kerkojme per ndermarrjen ne perdorim.
        /// </summary>
        /// <param name="idNjesiAdministrative">(int) Id e njesise administrative qe duhet ti marrim historikun.</param>
        /// <returns>Kthen nje DataTable me historikun e magazines nga tabela T_ASETE_NJESIADMINISTRATIVE_HISTORIKU.</returns>
        internal DataTable ktheHistorikMagazinaSipasIdNjesiAdministrative(int idNjesiAdministrative)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@ID_NJESI_ADMINISTRATIVE", idNjesiAdministrative, ParameterDirection.Input);

            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "PRC_T_ASETE_NJESIADMINISTRATIVE_HISTORIKU_SEL_SIPASIDNJESIADMINISTRATIVE");
            return ds.Tables[0];
        }

        /// <summary>
        /// MODULI ASETE:
        /// Kthen nga databaza nepermjet procedures PRC_T_ASETE_NJESIADMINISTRATIVE_HISTORIKU_SEL_SIPASIDHISTORIKU nje DataRow me historikun e magazines per id e historikut te kerkuar.
        /// </summary>
        /// <param name="idHistorikuNjesiAdministrative">(int) Id automatike e historikut te njesive administrative.</param>
        /// <returns>Kthen nje DataRow me historikun e magazines nga tabela T_ASETE_NJESIADMINISTRATIVE_HISTORIKU.</returns>
        internal DataRow ktheHistorikMagazinaSipasIdHistoriku(int idHistorikuNjesiAdministrative)
        {

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@ID_HISTORIKU_NJESI_ADMIN", idHistorikuNjesiAdministrative, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "PRC_T_ASETE_NJESIADMINISTRATIVE_HISTORIKU_SEL_SIPASIDHISTORIKU");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];

        }

        /// <summary>
        /// MODULI ASETE:
        /// Kthen nga databaza nepermjet procedures PRC_T_ASETE_NJESIADMINISTRATIVE_HISTORIKU_SEL_TOP1_SIPASIDNJESIADMINISTRATIVE nje DataRow me historikun e fundit aktual te magazines per id e njesise administrative te kerkuar.
        /// </summary>
        /// <param name="idNjesiAdministrative">(int) Id e njesise administrative qe duhet ti marrim historikun.</param>
        /// <returns>Kthen nje DataRow me historikun e magazines nga tabela T_ASETE_NJESIADMINISTRATIVE_HISTORIKU.</returns>
        internal DataRow ktheHistorikMagazinaAktualeSipasIdNjesiAdministrative(int idNjesiAdministrative)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@ID_NJESI_ADMINISTRATIVE", idNjesiAdministrative, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "PRC_T_ASETE_NJESIADMINISTRATIVE_HISTORIKU_SEL_TOP1_SIPASIDNJESIADMINISTRATIVE");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];
        }

        /// <summary>
        /// MODULI ASETE:
        /// Kthen nga databaza nepermjet procedures PRC_T_ASETE_NJESIADMINISTRATIVE_HISTORIKU_SEL_parafunditSipasidNjesiAdministrative nje DataRow me historikun e parafundit te magazines per id e njesise administrative te kerkuar.
        /// </summary>
        /// <param name="idNjesiAdministrative">(int) Id e njesise administrative qe duhet ti marrim historikun.</param>
        /// <returns>Kthen nje DataRow me historikun e parafundit te magazines nga tabela T_ASETE_NJESIADMINISTRATIVE_HISTORIKU.</returns>
        internal DataRow ktheHistorikMagazinaParaFunditSipasIdNjesiAdministrative(int idNjesiAdministrative)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@ID_NJESI_ADMINISTRATIVE", idNjesiAdministrative, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "PRC_T_ASETE_NJESIADMINISTRATIVE_HISTORIKU_SEL_parafunditSipasidNjesiAdministrative");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];
        }

        /// <summary>
        /// MODULI ASETE:
        /// Kthen nga databaza nepermjet procedures PRC_T_ASETE_NJESIADMINISTRATIVE_HISTORIKU_SEL_parafunditSipasidNjesiAdministrative nje DataRow me historikun e parafundit te magazines per id e njesise administrative te kerkuar.
        /// </summary>
        /// <param name="idNjesiAdministrative">(int) Id e njesise administrative qe duhet ti marrim historikun.</param>
        /// <returns>Kthen nje int me id e historikut te parafundit te njesise administrative qe kerkojme nga tabela T_ASETE_NJESIADMINISTRATIVE_HISTORIKU.</returns>
        internal int ktheIDHistorikMagazinaParafunditSipasIdNjesiAdministrative(int idNjesiAdministrative)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@ID_NJESI_ADMINISTRATIVE", idNjesiAdministrative, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "PRC_T_ASETE_NJESIADMINISTRATIVE_HISTORIKU_SEL_parafunditSipasidNjesiAdministrative");
            if (ds == null)
                return -1;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return -1;

            int idHistorikMagazina;
            int.TryParse(ds.Tables[0].Rows[0]["ID_HISTORIKU_NJESI_ADMIN"].ToString(), out idHistorikMagazina);
            return idHistorikMagazina;
        }

        /// <summary>
        /// MODULI ASETE:
        /// Kthen nga databaza nepermjet procedures PRC_T_ASETE_NJESIADMINISTRATIVE_HISTORIKU_SEL_TOP1_SIPASIDNJESIADMINISTRATIVE nje DataRow me historikun e fundit aktual te magazines per id e njesise administrative te kerkuar.
        /// </summary>
        /// <param name="idNjesiAdministrative">(int) Id e njesise administrative qe duhet ti marrim historikun.</param>
        /// <returns>Kthen nje int me id e historikut te fundit te njesise administrative qe kerkojme nga tabela T_ASETE_NJESIADMINISTRATIVE_HISTORIKU.</returns>
        internal int ktheIDHistorikMagazinaAktualeSipasIdNjesiAdministrative(int idNjesiAdministrative)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@ID_NJESI_ADMINISTRATIVE", idNjesiAdministrative, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "PRC_T_ASETE_NJESIADMINISTRATIVE_HISTORIKU_SEL_TOP1_SIPASIDNJESIADMINISTRATIVE");
            if (ds == null)
                return -1;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return -1;

            int idHistorikMagazina;
            int.TryParse(ds.Tables[0].Rows[0]["ID_HISTORIKU_NJESI_ADMIN"].ToString(), out idHistorikMagazina);
            return idHistorikMagazina;
        }

        /// <summary>
        /// MODULI ASETE:
        /// Kthen nga databaza nepermjet procedures PRC_T_ASETE_NJESIADMINISTRATIVE_HISTORIKU_SEL_TOP1_SIPASIDNJESIADMINISTRATIVE nje DataRow me historikun e magazinave qe eshte i fundit per magazinen qe ne kerkojme.
        /// </summary>
        /// <param name="idNjesiAdministrative">(int) Id e njesise administrative qe duhet ti marrim historikun.</param>
        /// <returns>Kthen nje int me id e statusit te fundit te njesise administrative qe kerkojme nga tabela T_ASETE_NJESIADMINISTRATIVE_HISTORIKU.</returns>
        internal int ktheIDStatusMagazineNgaHistorikMagazinaSipasIdNjesiAdministrative(int idNjesiAdministrative)
        {

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@ID_NJESI_ADMINISTRATIVE", idNjesiAdministrative, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "PRC_T_ASETE_NJESIADMINISTRATIVE_HISTORIKU_SEL_TOP1_SIPASIDNJESIADMINISTRATIVE");
            if (ds == null)
                return -1;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return -1;

            int idStatusMagazine;
            int.TryParse(ds.Tables[0].Rows[0]["ID_STATUS_MAGAZINE"].ToString(), out idStatusMagazine);
            return idStatusMagazine;

        }

        /// <summary>
        /// MODULI ASETE:
        /// Kthen nga databaza nepermjet procedures PRC_T_ASETE_NJESIADMINISTRATIVE_HISTORIKU_SEL_SIPASIDNJESIADMINISTRATIVE_DATESTATUSI nje DataRow me historikun me te afert te dates se vendosur per nje magazine.
        /// </summary>
        /// <param name="idNjesiAdministrative">(int) Id e njesise administrative qe duhet ti marrim historikun.</param>
        /// <param name="dataStatusit">(DateTime) Data e statusit qe kerkojme.</param>
        /// <returns>Kthen ID e statusit te magazines me historikun me te aferte para dates se vendosur nga tabela T_ASETE_NJESIADMINISTRATIVE_HISTORIKU.</returns>
        internal int ktheIDStatusMagazinaSipasIdNjesiAdministrativeDateStatusi(int idNjesiAdministrative, DateTime dataStatusit)
        {

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@ID_NJESI_ADMINISTRATIVE", idNjesiAdministrative, ParameterDirection.Input);
            dbManager.AddParameters(1, "@DATA_STATUSIT", dataStatusit, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "PRC_T_ASETE_NJESIADMINISTRATIVE_HISTORIKU_SEL_SIPASIDNJESIADMINISTRATIVE_DATESTATUSI");
            if (ds == null)
                return -1;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return -1;

            int idStatusMagazine;
            int.TryParse(ds.Tables[0].Rows[0]["ID_STATUS_MAGAZINE"].ToString(), out idStatusMagazine);
            return idStatusMagazine;

        }

        /// <summary>
        /// MODULI ASETE:
        /// Kthen nga databaza nepermjet procedures PRC_T_ASETE_NJESIADMINISTRATIVE_HISTORIKU_SEL_TOP1_SIPASIDNJESIADMINISTRATIVE nje DataRow me historikun e magazinave qe eshte i fundit per magazinen qe ne kerkojme.
        /// </summary>
        /// <param name="idNjesiAdministrative">(int) Id e njesise administrative qe duhet ti marrim historikun.</param>
        /// <returns>Kthen nje DateTime me daten e statusit te fundit te njesise administrative qe kerkojme nga tabela T_ASETE_NJESIADMINISTRATIVE_HISTORIKU.</returns>
        internal DateTime ktheDateMagazineNgaHistorikMagazinaSipasIdNjesiAdministrative(int idNjesiAdministrative)
        {

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@ID_NJESI_ADMINISTRATIVE", idNjesiAdministrative, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "PRC_T_ASETE_NJESIADMINISTRATIVE_HISTORIKU_SEL_TOP1_SIPASIDNJESIADMINISTRATIVE");
            if (ds == null)
                return DateTime.Parse("01/01/1900");
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return DateTime.Parse("01/01/1900");

            DateTime dataStatusit;
            DateTime.TryParse(ds.Tables[0].Rows[0]["DATA_STATUSIT"].ToString(), out dataStatusit);
            return dataStatusit;

        }

        /// <summary>
        /// MODULI ASETE:
        /// Kthen nga databaza nepermjet procedures PRC_T_ASETE_NJESIADMINISTRATIVE_HISTORIKU_SEL_SIPASIDNJESIADMINISTRATIVE nje DataTable me historikun e magazinave per magazinen qe ne kerkojme.
        /// </summary>
        /// <param name="idNjesiAdministrative">(int) Id e njesise administrative qe duhet ti marrim historikun.</param>
        /// <returns>Kthen True nese ekziston nje ose me shume historik per magazinen qe ne kerkojme dhe False ne te kundert.</returns>
        internal bool ekzistonHistorikMagazinaSipasIdNjesiAdministrative(int idNjesiAdministrative)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@ID_NJESI_ADMINISTRATIVE", idNjesiAdministrative, ParameterDirection.Input);

            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "PRC_T_ASETE_NJESIADMINISTRATIVE_HISTORIKU_SEL_SIPASIDNJESIADMINISTRATIVE");
            if (ds == null)
                return true;
            if (ds.Tables[0].Rows.Count == 0)
                return false;
            return true;
        }

        /// <summary>
        /// MODULI ASETE:
        /// Kthen nga databaza nepermjet procedures PRC_T_ASETE_NJESIADMINISTRATIVE_HISTORIKU_SEL_SIPASIDSTATUSMAGAZINE nje DataRow me historikun e magazinave qe kane qene njehere ne gjendjen e id se statusit te magazines qe ne kerkojme.
        /// </summary>
        /// <param name="idNjesiAdministrative">(int) Id e njesise administrative qe duhet ti marrim historikun.</param>
        /// <param name="idStatusMagazine">(int) Id e statusit te magazines qe po behet ruajtja.</param>
        /// <returns>Kthen True nese ekziston nje ose me shume magazine qe ka patur si historik gjendjen e id se statusit te magazines qe ne kerkojme dhe False ne te kundert.</returns>
        internal bool ekzistonHistorikMagazinaSipasIdStatusMagazine(int idNjesiAdministrative, int idStatusMagazine)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@ID_NJESI_ADMINISTRATIVE", idNjesiAdministrative, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDSTATUSMAGAZINE", idStatusMagazine, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "PRC_T_ASETE_NJESIADMINISTRATIVE_HISTORIKU_SEL_SIPASIDSTATUSMAGAZINE");
            if (ds == null)
                return true;
            if (ds.Tables[0].Rows.Count == 0)
                return false;
            return true;
        }

        #endregion

        #region Lloj njesi administrative

        /// <summary>
        /// MODULI ASETE:
        /// Kthen nga databaza nepermjet procedures PRC_T_ASETE_LLOJ_NJESIADMINISTRATIVE_SEL nje DataTable me te gjitha lloje e njesive administrative.
        /// </summary>
        /// <returns>Kthen nje DataTable me llojet e njesive administrative nga tabela T_ASETE_LLOJ_NJESIADMINISTRATIVE.</returns>
        internal DataTable ktheLlojNjesiAdministrative()
        {
            dbManager.Open();
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "PRC_T_ASETE_LLOJ_NJESIADMINISTRATIVE_SEL");
            return ds.Tables[0];
        }

        /// <summary>
        /// MODULI ASETE:
        /// Kthen nga databaza nepermjet procedures PRC_T_ASETE_LLOJ_NJESIADMINISTRATIVE_SEL_SIPASEMERTIMI nje DataRow me llojin e njesi administrative sipas emertimit qe kerkojme.
        /// </summary>
        /// <param name="emertimi">(string) Emertimi i llojit te njesise administrative qe i kerkojme ID-ne.</param>
        /// <returns>Kthen nje int me id e llojit te njesise administrative qe kerkojme nga tabela T_ASETE_LLOJ_NJESIADMINISTRATIVE.</returns>
        internal int ktheIDLlojNjesiAdministrativeSipasEmertimi(string emertimi)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@EMERTIMI", emertimi, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "PRC_T_ASETE_LLOJ_NJESIADMINISTRATIVE_SEL_SIPASEMERTIMI");
            if (ds == null)
                return -1;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return -1;

            int idLlojNjesiAdministrative;
            int.TryParse(ds.Tables[0].Rows[0]["ID_LLOJ_NJESIADMINISTRATIVE"].ToString(), out idLlojNjesiAdministrative);
            return idLlojNjesiAdministrative;
        }

        /// <summary>
        /// MODULI ASETE:
        /// Kthen nga databaza nepermjet procedures PRC_T_ASETE_LLOJ_NJESIADMINISTRATIVE_SEL_SIPASID nje DataRow me llojin e njesi administrative sipas id qe kerkojme.
        /// </summary>
        /// <param name="idLlojNjesiAdministrative">(int) Id e llojit te njesise administrative qe kerkojme.</param>
        /// <returns>Kthen nje string me emertimin e llojit te njesise administrative sipas id qe kerkojme nga tabela T_ASETE_LLOJ_NJESIADMINISTRATIVE.</returns>
        internal string ktheEmertimiLlojNjesiAdministrativeSipasID(int idLlojNjesiAdministrative)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@ID_LLOJ_NJESIADMINISTRATIVE", idLlojNjesiAdministrative, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "PRC_T_ASETE_LLOJ_NJESIADMINISTRATIVE_SEL_SIPASID");
            if (ds == null)
                return "";
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return "";

            string emertimiLlojNjesiAdministrative = ds.Tables[0].Rows[0]["EMERTIMI"].ToString();
            return emertimiLlojNjesiAdministrative;
        }

        #endregion

        #region Amortizimi Koka

        /// <summary>
        /// MODULI ASETE:
        /// Ruan ne tabelen T_ASETE_AMORTIZIMI_KOKA objektin e kokes se amortizimit nepermjet procedures PRC_T_ASETE_AMORTIZIMI_KOKA_INSERT.
        /// </summary>
        /// <param name="idAmortizimi">(int) Id automatike e kokes se amortizimit.</param>
        /// <param name="nrDok">(string) Numri i dokumentit te amortizimit.</param>
        /// <param name="idNiveli">(int) Id e nenkategorise qe do te perdoret per fleten e amortizimit FA.</param>
        /// <param name="idKonfigurimAmbjenti">(int) Id e llojit te dokumentit qe eshte perdorur ne dokumentin e amortizimit.</param>
        /// <param name="dateDokumenti">(DateTime) Data e dokumentit te amortizimit.</param>
        /// <param name="dateAmortizimi">(DateTime) Data e llogaritjes se amortizimit per dokumentin e amortizimit.</param>
        /// <param name="dateRegjistrimi">(DateTime) Data qe eshte bere regjistrimi i dokumentit te amortizimit.</param>
        /// <param name="idNjesiAdministrative">(int) Id e njesise administrative per te cilen po behet regjistrimi i dokumentit te amortizimit.</param>
        /// <param name="shenime">(string) Shenime te ndryshme qe mund te vendosen ne dokumentin e amortizimit.</param>
        /// <param name="amortizimiShteseTotal">(float) Totali i dokumentit te amortizimit per amortizimin shtese te llogaritur.</param>
        /// <param name="idNderViti">(int) Id e lidhjes se ndermarrjes me nje vit kalendarik te caktuar.</param>
        /// <param name="idLlojStandarti">(int) Id e llojit te standartit per te cilen po behet llogaritja e amortizimit.</param>
        /// <param name="idStatusDokumenti">(int) Id se ne cfare gjendje eshte dokumenti i amortizimit, i ruajtur, fshire, apo modifikuar</param>
        /// <param name="idNdermarrje">(int) Id e ndermarrjes per te cilen po behet regjistrimi i amortizimit.</param>
        /// <param name="idPerdoruesi">(int) Id e perdoruesit te fundit qe ka modifikuar dokumentin e regjistrimit te amortizimit.</param>
        /// <param name="idKrijuesi">(int) Id e krijuesit per here te pare te dokumentit te amortizimit.</param>
        /// <param name="dtModifikimi">(DateTime) Data e modifikimit te dokumentit te amortizimit.</param>
        /// <param name="idDokGjenerues">(int) Id e dokumentit qe e ka gjeneruar dokumentin e amortizimit.</param>
        /// <param name="idNivelGjenerues">(int) Id e nenkategorise qe e ka gjeneruar dokumentin e amortizmit.</param>
        /// <param name="idKonfigGjenerues">(int) Id e llojit te dokumentit qe e ka gjeneruar dokumentin e amortizimit.</param>
        /// <param name="idDokNga">(int) Id e dokumentit nga vjen dokumenti i amortizimit.</param>
        /// <param name="idLlogKunderparti">(int) Id e llogarise kunderparti.</param>
        /// <param name="nrRenditje">(int) Numri i renditjes brenda dites.</param>
        /// <returns>Kthen clsMesazh me atribut statusi True nese ruajtja kryhet me sukses ose False dhe mesazhin e gabimit ne te kundert.</returns>
        internal clsMesazh ruajAmortizimiKoka(out int idAmortizimi, string nrDok, int idNiveli, int idKonfigurimAmbjenti, DateTime dateDokumenti, DateTime dateAmortizimi, DateTime dateRegjistrimi,
            int idNjesiAdministrative, string shenime, double amortizimiShteseTotal, int idNderViti, int idLlojStandarti, int idStatusDokumenti, int idNdermarrje, int idPerdoruesi,
            int idKrijuesi, DateTime dtModifikimi, int idDokGjenerues, int idNivelGjenerues, int idKonfigGjenerues, int idDokNga, int idLlogKunderparti, int nrRenditje)
        {
            idAmortizimi = 0;

            dbManager.Open();
            dbManager.CreateParameters(23);
            dbManager.AddParameters(0, "@ID_AMORTIZIMI", idAmortizimi, ParameterDirection.Output);
            dbManager.AddParameters(1, "@NR_DOK", nrDok, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDNIVELI", idNiveli, ParameterDirection.Input);
            dbManager.AddParameters(3, "@IDKONFIGURIMAMBJENTI", idKonfigurimAmbjenti, ParameterDirection.Input);
            dbManager.AddParameters(4, "@DATE_DOKUMENTI", dateDokumenti, ParameterDirection.Input);
            dbManager.AddParameters(5, "@DATE_AMORTIZIMI", dateAmortizimi, ParameterDirection.Input);
            dbManager.AddParameters(6, "@DATE_REGJISTRIMI", dateRegjistrimi, ParameterDirection.Input);
            if (idNjesiAdministrative < 1) dbManager.AddParameters(7, "@IDNJESIADMINISTRATIVE", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(7, "@IDNJESIADMINISTRATIVE", idNjesiAdministrative, ParameterDirection.Input);
            dbManager.AddParameters(8, "@SHENIME", shenime, ParameterDirection.Input);
            dbManager.AddParameters(9, "@AMORTIZIMISHTESE_TOTAL", amortizimiShteseTotal, ParameterDirection.Input);
            if (idNderViti < 1) dbManager.AddParameters(10, "@IDNDERVITI", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(10, "@IDNDERVITI", idNderViti, ParameterDirection.Input);
            dbManager.AddParameters(11, "@IDLLOJSTANDARTI", idLlojStandarti, ParameterDirection.Input);
            dbManager.AddParameters(12, "@IDSTATUSDOK", idStatusDokumenti, ParameterDirection.Input);
            dbManager.AddParameters(13, "@IDNDERMARJE", idNdermarrje, ParameterDirection.Input);
            dbManager.AddParameters(14, "@IDPERDORUESI", idPerdoruesi, ParameterDirection.Input);
            dbManager.AddParameters(15, "@IDKRIJUESI", idKrijuesi, ParameterDirection.Input);
            dbManager.AddParameters(16, "@DTMODIFIKIMI", dtModifikimi, ParameterDirection.Input);
            if (idNivelGjenerues < 1) dbManager.AddParameters(17, "@IDNIVELGJENERUES", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(17, "@IDNIVELGJENERUES", idNivelGjenerues, ParameterDirection.Input);
            if (idKonfigGjenerues < 1) dbManager.AddParameters(18, "@IDKONFIGGJENERUES", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(18, "@IDKONFIGGJENERUES", idKonfigGjenerues, ParameterDirection.Input);
            if (idDokGjenerues < 1) dbManager.AddParameters(19, "@IDGJENERUES", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(19, "@IDGJENERUES", idDokGjenerues, ParameterDirection.Input);
            if (idDokNga < 1) dbManager.AddParameters(20, "@IDDOKNGA", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(20, "@IDDOKNGA", idDokNga, ParameterDirection.Input);
            if (idLlogKunderparti < 1) dbManager.AddParameters(21, "@ID_LLOGKUNDERPARTI", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(21, "@ID_LLOGKUNDERPARTI", idLlogKunderparti, ParameterDirection.Input);
            dbManager.AddParameters(22, "@NR_RENDITJE", nrRenditje, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "PRC_T_ASETE_AMORTIZIMI_KOKA_INSERT");
            idAmortizimi = int.Parse(dbManager.Parameters[0].Value.ToString());
            return new clsMesazh(true, "Ruatja perfundoi me sukses!");

        }

        /// <summary>
        /// MODULI ASETE:
        /// Modifikon ne tabelen T_ASETE_AMORTIZIMI_KOKA objektin e kokes se amortizimit nepermjet procedures PRC_T_ASETE_AMORTIZIMI_KOKA_Update.
        /// </summary>
        /// <param name="idAmortizimi">(int) Id automatike e kokes se amortizimit.</param>
        /// <param name="nrDok">(string) Numri i dokumentit te amortizimit.</param>
        /// <param name="dateDokumenti">(DateTime) Data e dokumentit te amortizimit.</param>
        /// <param name="dateAmortizimi">(DateTime) Data e llogaritjes se amortizimit per dokumentin e amortizimit.</param>
        /// <param name="dateRegjistrimi">(DateTime) Data qe eshte bere regjistrimi i dokumentit te amortizimit.</param>
        /// <param name="idNjesiAdministrative">(int) Id e njesise administrative per te cilen po behet regjistrimi i dokumentit te amortizimit.</param>
        /// <param name="shenime">(string) Shenime te ndryshme qe mund te vendosen ne dokumentin e amortizimit.</param>
        /// <param name="amortizimiShteseTotal">(float) Totali i dokumentit te amortizimit per amortizimin shtese te llogaritur.</param>
        /// <param name="idLlojStandarti">(int) Id e llojit te standartit per te cilen po behet llogaritja e amortizimit.</param>
        /// <param name="idStatusDokumenti">(int) Id se ne cfare gjendje eshte dokumenti i amortizimit, i ruajtur, fshire, apo modifikuar</param>
        /// <param name="idPerdoruesi">(int) Id e perdoruesit te fundit qe ka modifikuar dokumentin e regjistrimit te amortizimit.</param>
        /// <returns>Kthen clsMesazh me atribut statusi True nese modifikimi kryhet me sukses ose False dhe mesazhin e gabimit ne te kundert.</returns>
        internal clsMesazh modifikoAmortizimiKoka(int idAmortizimi, string nrDok, DateTime dateDokumenti, DateTime dateAmortizimi, DateTime dateRegjistrimi, int idNjesiAdministrative, string shenime, double amortizimiShteseTotal, int idLlojStandarti, int idStatusDokumenti, int idPerdoruesi)
        {

            dbManager.Open();
            dbManager.CreateParameters(11);
            dbManager.AddParameters(0, "@ID_AMORTIZIMI", idAmortizimi, ParameterDirection.Input);
            dbManager.AddParameters(1, "@NR_DOK", nrDok, ParameterDirection.Input);
            dbManager.AddParameters(2, "@DATE_DOKUMENTI", dateDokumenti, ParameterDirection.Input);
            dbManager.AddParameters(3, "@DATE_AMORTIZIMI", dateAmortizimi, ParameterDirection.Input);
            dbManager.AddParameters(4, "@DATE_REGJISTRIMI", dateRegjistrimi, ParameterDirection.Input);
            if (idNjesiAdministrative < 1) dbManager.AddParameters(5, "@IDNJESIADMINISTRATIVE", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(5, "@IDNJESIADMINISTRATIVE", idNjesiAdministrative, ParameterDirection.Input);
            dbManager.AddParameters(6, "@SHENIME", shenime, ParameterDirection.Input);
            dbManager.AddParameters(7, "@AMORTIZIMISHTESE_TOTAL", amortizimiShteseTotal, ParameterDirection.Input);
            dbManager.AddParameters(8, "@IDLLOJSTANDARTI", idLlojStandarti, ParameterDirection.Input);
            dbManager.AddParameters(9, "@IDSTATUSDOK", idStatusDokumenti, ParameterDirection.Input);
            dbManager.AddParameters(10, "@IDPERDORUESI", idPerdoruesi, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "PRC_T_ASETE_AMORTIZIMI_KOKA_Update");

            return new clsMesazh(true, MessagesResource.Messages["msgModifikimiMeSukses"]);
        }

        /// <summary>
        /// MODULI ASETE:
        /// Fshirja e dokumentit te amortizimit duke i ndryshuar statuset ne te fshire (2) ne tebelen T_ASETE_AMORTIZIMI_KOKA nepermjet procedures PRC_T_ASETE_AMORTIZIMI_KOKA_UPDATEDELETE.
        /// </summary>
        /// <param name="idAmortizimiKoka">(int) Id automatike e amortizimit te kokes.</param>
        /// <param name="idPerdoruesi">(int) Id e perdoruesit qe ka modifikuar per here te fundit amortizimin e kokes.</param>
        /// <returns>Kthen clsMesazh me atribut statusi True nese modifikimi kryhet me sukses ose False dhe mesazhin e gabimit ne te kundert.</returns>
        internal clsMesazh modifikoAmortizimKokaStatus(int idAmortizimiKoka, int idPerdoruesi)
        {

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@ID_AMORTIZIMI", idAmortizimiKoka, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUESI", idPerdoruesi, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "PRC_T_ASETE_AMORTIZIMI_KOKA_UPDATEDELETE");
            return new clsMesazh(true, "Ruatja perfundoi me sukses!");

        }

        /// <summary>
        /// MODULI ASETE:
        /// Modifikimi i statusit te dokumentit ne tebelen T_ASETE_AMORTIZIMI_KOKA nepermjet procedures PRC_T_ASETE_AMORTIZIMI_KOKA_UPDStatus.
        /// </summary>
        /// <param name="idAmortizimiKoka">(int) Id automatike e amortizimit te kokes.</param>
        /// <param name="idPerdoruesi">(int) Id e perdoruesit qe ka modifikuar per here te fundit amortizimin e kokes.</param>
        /// <param name="status">(int) Id se ne cfare gjendje eshte dokumenti i amortizimit, i ruajtur, fshire, apo modifikuar</param>
        /// <returns>Kthen clsMesazh me atribut statusi True nese modifikimi kryhet me sukses ose False dhe mesazhin e gabimit ne te kundert.</returns>
        internal clsMesazh modifikoAmortizimKokaStatus(int idAmortizimiKoka, int idPerdoruesi, int status)
        {
            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@ID_AMORTIZIMI", idAmortizimiKoka, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUESI", idPerdoruesi, ParameterDirection.Input);
            dbManager.AddParameters(2, "@status", status, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "PRC_T_ASETE_AMORTIZIMI_KOKA_UPDStatus");
            return new clsMesazh(true, "Ruatja perfundoi me sukses!");
        }

        /// <summary>
        /// MODULI ASETE:
        /// Modifikon ne tabelen T_ASETE_AMORTIZIMI_KOKA objektin e kokes se amortizimit nepermjet procedures PRC_T_ASETE_AMORTIZIMI_KOKA_Update_AMORTSHTESE.
        /// </summary>
        /// <param name="idAmortizimi">(int) Id automatike e kokes se amortizimit.</param>
        /// <param name="amortizimiShteseTotal">(float) Totali i dokumentit te amortizimit per amortizimin shtese te llogaritur.</param>
        /// <param name="idPerdoruesi">(int) Id e perdoruesit te fundit qe ka modifikuar dokumentin e regjistrimit te amortizimit.</param>
        /// <returns>Kthen clsMesazh me atribut statusi True nese modifikimi kryhet me sukses ose False dhe mesazhin e gabimit ne te kundert.</returns>
        internal clsMesazh modifikoAmortizimiKokaPerRillogaritje(int idAmortizimi, double amortizimiShteseTotal, int idPerdoruesi)
        {
            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@ID_AMORTIZIMI", idAmortizimi, ParameterDirection.Input);
            dbManager.AddParameters(1, "@AMORTIZIMISHTESE_TOTAL", amortizimiShteseTotal, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDPERDORUESI", idPerdoruesi, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "PRC_T_ASETE_AMORTIZIMI_KOKA_Update_AMORTSHTESE");
            return new clsMesazh(true, MessagesResource.Messages["msgModifikimiMeSukses"]);
        }

        /// <summary>
        /// MODULI ASETE:
        /// Kthen nga databaza nepermjet procedures PRC_T_ASETE_AMORTIZIMI_KOKA_SEL_SIPASMAG_DATEAMORT nje DataTable me te amortizimet sipas dates dhe njesi administrative.
        /// </summary>
        /// <param name="dateAmortizimi">(DateTime) Data e amortizimit te dokumentit.</param>
        /// <param name="idNjesiAdministrative">(int) Id e njesise administrative per te cilen po behet regjistrimi i dokumentit te amortizimit.</param>
        /// <param name="idLlojStandarti">(int) Id e llojit te standartit per te cilen po behet llogaritja e amortizimit.</param>
        /// <param name="idNdermarrje">(int) Id e ndermarrjes per te cilen po behet regjistrimi i amortizimit.</param>
        /// <returns>Kthen nje DataTable me kokat e dokumenteve te amortizimit nga tabela T_ASETE_AMORTIZIMI_KOKA.</returns>
        internal DataTable ktheAmortizimKoka(DateTime dateAmortizimi, int idNjesiAdministrative, int idLlojStandarti, int idNdermarrje)
        {
            dbManager.Open();
            dbManager.CreateParameters(4);
            dbManager.AddParameters(0, "@DATE_AMORTIZIMI", dateAmortizimi, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNJESIADMINISTRATIVE", idNjesiAdministrative, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDLLOJSTANDARTI", idLlojStandarti, ParameterDirection.Input);
            dbManager.AddParameters(3, "@IDNDERMARJE", idNdermarrje, ParameterDirection.Input);
            return dbManager.ExecuteDataSet(CommandType.StoredProcedure, "PRC_T_ASETE_AMORTIZIMI_KOKA_SEL_SIPASMAG_DATEAMORT").Tables[0];
        }

        /// <summary>
        /// MODULI ASETE:
        /// Kthen nga databaza nepermjet procedures PRC_T_ASETE_AMORTIZIMI_KOKA_SEL_SIPAS_DATEAMORT nje DataTable me te amortizimet sipas dates.
        /// </summary>
        /// <param name="dateAmortizimi">(DateTime) Data e amortizimit te dokumentit.</param>
        /// <param name="idNjesiAdministrative">(int) Id e njesise administrative per te cilen po behet regjistrimi i dokumentit te amortizimit.</param>
        /// <param name="idLlojStandarti">(int) Id e llojit te standartit per te cilen po behet llogaritja e amortizimit.</param>
        /// <param name="idNdermarrje">(int) Id e ndermarrjes per te cilen po behet regjistrimi i amortizimit.</param>
        /// <returns>Kthen nje DataTable me kokat e dokumenteve te amortizimit nga tabela T_ASETE_AMORTIZIMI_KOKA.</returns>
        internal DataTable ktheAmortizimKoka(DateTime dateAmortizimi, int idLlojStandarti, int idNdermarrje)
        {
            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@DATE_AMORTIZIMI", dateAmortizimi, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDLLOJSTANDARTI", idLlojStandarti, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDNDERMARJE", idNdermarrje, ParameterDirection.Input);
            return dbManager.ExecuteDataSet(CommandType.StoredProcedure, "PRC_T_ASETE_AMORTIZIMI_KOKA_SEL_SIPAS_DATEAMORT").Tables[0];
        }

        public bool ekzistonRegjistrimAmortizimi(int idNiv, int idKonf, DateTime dtDk, string nrDk, int idNder, int idstandarti)
        {

            dbManager.Open();
            dbManager.CreateParameters(6);
            dbManager.AddParameters(0, "@NRDOK", nrDk, ParameterDirection.Input);
            dbManager.AddParameters(1, "@LLOJDOK", idNiv, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDNDERMARJE", idNder, ParameterDirection.Input);
            dbManager.AddParameters(3, "@DTDOK", dtDk, ParameterDirection.Input);
            dbManager.AddParameters(4, "@IDMODELI", idKonf, ParameterDirection.Input);
            dbManager.AddParameters(5, "@idstandarti", idstandarti, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_ASETE_AMORTIZIMI_KOKA_ekzistonRegjistrimAmortizimi");
            if (ds.Tables[0].Rows.Count == 1)
                return true;
            else if (ds.Tables[0].Rows.Count == 0)
                return false;
            else return true;

        }
        public bool kaVeprimeMeKeteAset(int idNiv, int idseriali, int idstandarti, int iddok)
        {

            dbManager.Open();
            dbManager.CreateParameters(4);
            dbManager.AddParameters(0, "@idnivel", idNiv, ParameterDirection.Input);
            dbManager.AddParameters(1, "@idseriali", idseriali, ParameterDirection.Input);
            dbManager.AddParameters(2, "@idllojstandarti", idstandarti, ParameterDirection.Input);
            dbManager.AddParameters(3, "@iddok", iddok, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "PRC_T_ASETE_AMORTIZIMI_KOKA_ekzistonAmortizimFillestar");
            if (ds.Tables[0].Rows.Count > 0)
                return true;
            else
                return false;


        }

        /// <summary>
        /// MODULI ASETE:
        /// Merr koken e amortizimit te tebela T_ASETE_AMORTIZIMI_KOKA nepermjet procedures PRC_T_ASETE_AMORTIZIMI_KOKA_SEL_SIPASDT.
        /// </summary>
        /// <param name="idnderviti">(int) Id e lidhjes se ndermarrjes me nje vit kalendarik te caktuar.</param>
        /// <param name="idperdoruesi">(int) Id e perdoruesit qe ka modifikuar per here te fundit amortizimin e kokes.</param>
        /// <returns>Kthen nje DataTable me kokat e amortizimit.</returns>
        internal DataTable ktheAmortizimKokaDT(int idnderviti, int idperdoruesi)
        {

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDNderviti", idnderviti, ParameterDirection.Input);
            dbManager.AddParameters(1, "@idperdoruesi", idperdoruesi, ParameterDirection.Input);
            return dbManager.ExecuteDataSet(CommandType.StoredProcedure, "PRC_T_ASETE_AMORTIZIMI_KOKA_SEL_SIPASDT").Tables[0];
        }

        /// <summary>
        /// MODULI ASETE:
        /// Kthen koken e amortizimit sipas amoritizimit fillestar te tebela T_ASETE_AMORTIZIMI_KOKA nepermjet procedures PRC_T_ASETE_AMORTIZIMI_KOKA_SEL_SIPASDTAmortizimFillestar.
        /// </summary>
        /// <param name="idnderviti">(int) Id e lidhjes se ndermarrjes me nje vit kalendarik te caktuar.</param>
        /// <param name="idperdoruesi">(int) Id e perdoruesit te fundit qe ka modifikuar dokumentin e regjistrimit te amortizimit.</param>
        /// <returns>Kthen nje DataTable me kokat e amortizimit.</returns>
        internal DataTable ktheAmortizimKokaSipasDtAmortizimFillestar(int idnderviti, int idperdoruesi)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDNderviti", idnderviti, ParameterDirection.Input);
            dbManager.AddParameters(1, "@idperdoruesi", idperdoruesi, ParameterDirection.Input);
            return dbManager.ExecuteDataSet(CommandType.StoredProcedure, "PRC_T_ASETE_AMORTIZIMI_KOKA_SEL_SIPASDTAmortizimFillestar").Tables[0];
        }

        /// <summary>
        /// MODULI ASETE:
        /// Kthen dokumentet e amortizimit per eksport sipas amoritizimit fillestar te tebela T_ASETE_AMORTIZIMI_KOKA nepermjet procedures 
        /// PRC_T_ASETE_AMORTIZIMI_KOKA_SEL_AmortizimFillestarPerEksport.
        /// </summary>
        /// <param name="idnderviti">(int) Id e lidhjes se ndermarrjes me nje vit kalendarik te caktuar.</param>
        /// <param name="idperdoruesi">(int) Id e perdoruesit te fundit qe ka modifikuar dokumentin e regjistrimit te amortizimit.</param>
        /// <returns>Kthen nje DataTable me kokat e amortizimit.</returns>
        internal DataTable ktheAmortizimFillestarPerEksport(int idNdermarje, int idnderviti, int idperdoruesi, string idPerEksport)
        {
            dbManager.Open();
            dbManager.CreateParameters(4);
            dbManager.AddParameters(0, "@IDNDERMARJE", idNdermarje, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERVITI", idnderviti, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDPERDORUESI", idperdoruesi, ParameterDirection.Input);
            dbManager.AddParameters(3, "@IDPEREKSPORT", idPerEksport, ParameterDirection.Input);
            return dbManager.ExecuteDataSet(CommandType.StoredProcedure, "PRC_T_ASETE_AMORTIZIMI_KOKA_SEL_AmortizimFillestarPerEksport").Tables[0];
        }

        internal DataTable ktheAmortizimRezervaPerEksport(int idNdermarje, int idNdermViti, string idPerEksport)
        {
            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@IDNDERMARJE", idNdermarje, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERVITI", idNdermViti, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDPEREKSPORT", idPerEksport, ParameterDirection.Input);
            return dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_ASETE_AMORTIZIMI_KOKA_AmortizimRezervaPerEksport").Tables[0];
        }

        /// <summary>
        /// MODULI ASETE:
        /// Kthen koken e amortizimit sipas rivleresimit te tebela T_ASETE_AMORTIZIMI_KOKA nepermjet procedures PRC_T_ASETE_AMORTIZIMI_KOKA_SEL_SIPASDTRivleresimAmortizimi.
        /// </summary>
        /// <param name="idnderviti">(int) Id e lidhjes se ndermarrjes me nje vit kalendarik te caktuar.</param>
        /// <param name="idperdoruesi">(int) Id e perdoruesit te fundit qe ka modifikuar dokumentin e regjistrimit te amortizimit.</param>
        /// <returns>Kthen nje DataTable me kokat e amortizimit.</returns>
        internal DataTable ktheAmortizimKokaSipasDtRivleresim(int idnderviti, int idperdoruesi)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDNderviti", idnderviti, ParameterDirection.Input);
            dbManager.AddParameters(1, "@idperdoruesi", idperdoruesi, ParameterDirection.Input);
            return dbManager.ExecuteDataSet(CommandType.StoredProcedure, "PRC_T_ASETE_AMORTIZIMI_KOKA_SEL_SIPASDTRivleresimAmortizimi").Tables[0];
        }

        /// <summary>
        /// MODULI ASETE:
        /// Kthen koken e amortizimit sipas id se gjeneruesit te tebela T_ASETE_AMORTIZIMI_KOKA nepermjet procedures PRC_T_ASETE_AMORTIZIMI_KOKA_SEL_SIPASIdGjeneruesi.
        /// </summary>
        /// <param name="idgjenerues">(int) Id e dokumentit qe e ka gjeneruar dokumentin e amortizimit.</param>
        /// <param name="idkonfiggjenerues">(int) Id e llojit te dokumentit qe e ka gjeneruar dokumentin e amortizimit.</param>
        /// <returns>Kthen nje DataTable me kokat e amortizimit.</returns>
        internal DataTable ktheAmortizimKokaSipasIdGjeneruesi(int idgjenerues, int idkonfiggjenerues)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDKonfiggjenerues", idkonfiggjenerues, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDGjenerues", idgjenerues, ParameterDirection.Input);
            return dbManager.ExecuteDataSet(CommandType.StoredProcedure, "PRC_T_ASETE_AMORTIZIMI_KOKA_SEL_SIPASIdGjeneruesi").Tables[0];
        }

        /// <summary>
        /// MODULI ASETE:
        /// Kthen nga databaza nepermjet procedures PRC_T_ASETE_AMORTIZIMI_KOKA_SEL_SIPASMAG_DATEAMORT_TRUPI nje DataTable me te amortizimet sipas dates dhe njesi administrative ne trupin e dokumentit.
        /// </summary>
        /// <param name="dateAmortizimi">(DateTime) Data e amortizimit te dokumentit.</param>
        /// <param name="idNjesiAdministrative">(int) Id e njesise administrative per te cilen po behet regjistrimi i dokumentit te amortizimit.</param>
        /// <param name="idLlojStandarti">(int) Id e llojit te standartit per te cilen po behet llogaritja e amortizimit.</param>
        /// <param name="idNdermarrje">(int) Id e ndermarrjes per te cilen po behet regjistrimi i amortizimit.</param>
        /// <returns>Kthen nje DataTable me kokat e dokumenteve te amortizimit nga tabela T_ASETE_AMORTIZIMI_KOKA.</returns>
        internal DataTable ktheAmortizimKokaSipasMagDateAmortSipasTrupi(DateTime dateAmortizimi, int idNjesiAdministrative, int idLlojStandarti, int idNdermarrje)
        {
            dbManager.Open();
            dbManager.CreateParameters(4);
            dbManager.AddParameters(0, "@DATE_AMORTIZIMI", dateAmortizimi, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNJESIADMINISTRATIVE", idNjesiAdministrative, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDLLOJSTANDARTI", idLlojStandarti, ParameterDirection.Input);
            dbManager.AddParameters(3, "@IDNDERMARJE", idNdermarrje, ParameterDirection.Input);
            return dbManager.ExecuteDataSet(CommandType.StoredProcedure, "PRC_T_ASETE_AMORTIZIMI_KOKA_SEL_SIPASMAG_DATEAMORT_TRUPI").Tables[0];
        }

        /// <summary>
        /// MODULI ASETE:
        /// Merr koken e amortizimit sipas id se kokes te tebela T_ASETE_AMORTIZIMI_KOKA nepermjet procedures PRC_T_ASETE_AMORTIZIMI_KOKA_SEL_SIPASId.
        /// </summary>
        /// <param name="id">(int) Id automatike e amortizimit te kokes.</param>
        /// <returns>Kthen DataRow koken e amortizimit sipas id-se.</returns>
        internal DataRow ktheAmortizimKokaSipasId(int id)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDAmortizimi", id, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "PRC_T_ASETE_AMORTIZIMI_KOKA_SEL_SIPASId");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];
        }

        /// <summary>
        /// MODULI ASETE:
        /// Kthen nga databaza nepermjet procedures PRC_T_ASETE_AMORTIZIMI_KOKA_SEL_SIPASMAG_DATE_NENKATDOK nje DataRow me dokumentin e amortizimit nga nje magazine ne nje date te caktuar me nje lloj dokumenti te caktuar ne nje nga standartet.
        /// </summary>
        /// <param name="idkonfigambjente">(int) Id e nenkategorise qe do te perdoret per fleten e amortizimit FA.</param>
        /// <param name="dateDokumenti">(DateTime) Data e dokumentit te amortizimit.</param>
        /// <param name="idNjesiAdministrative">(int) Id e njesise administrative per te cilen po behet regjistrimi i dokumentit te amortizimit.</param>
        /// <param name="idLlojStandarti">(int) Id e llojit te standartit per te cilen po behet llogaritja e amortizimit.</param>
        /// <param name="idNdermarrje">(int) Id e ndermarrjes per te cilen po behet regjistrimi i amortizimit.</param>
        /// <returns>Kthen nje DataRow me dokumentin e amortizimit sipas parametrave ne standartin specifik nga tabela T_ASETE_AMORTIZIMI_KOKA.</returns>
        internal DataRow ktheAmortizimKokaSipasMagDateNenKatDok(int idkonfigambjente, DateTime dateDokumenti, int idNjesiAdministrative, int idLlojStandarti, int idNdermarrje)
        {
            dbManager.Open();
            dbManager.CreateParameters(5);
            dbManager.AddParameters(0, "@IDKONFIGURIMAMBJENTI", idkonfigambjente, ParameterDirection.Input);
            dbManager.AddParameters(1, "@DATE_DOKUMENTI", dateDokumenti, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDNJESIADMINISTRATIVE", idNjesiAdministrative, ParameterDirection.Input);
            dbManager.AddParameters(3, "@IDLLOJSTANDARTI", idLlojStandarti, ParameterDirection.Input);
            dbManager.AddParameters(4, "@IDNDERMARJE", idNdermarrje, ParameterDirection.Input);

            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "PRC_T_ASETE_AMORTIZIMI_KOKA_SEL_SIPASMAG_DATE_NENKATDOK");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];
        }

        internal DataTable ktheAmortizimKokaSipasMagDateNenKatDheNrDok(int idkonfigambjente, DateTime dateDokumenti, int idNjesiAdministrative, string nrdok, int idNdermarrje)
        {

            dbManager.Open();
            dbManager.CreateParameters(5);
            dbManager.AddParameters(0, "@IDKONFIGURIMAMBJENTI", idkonfigambjente, ParameterDirection.Input);
            dbManager.AddParameters(1, "@DATE_DOKUMENTI", dateDokumenti, ParameterDirection.Input);

            dbManager.AddParameters(2, "@IDNJESIADMINISTRATIVE", idNjesiAdministrative, ParameterDirection.Input);
            dbManager.AddParameters(3, "@nrdok", nrdok, ParameterDirection.Input);
            dbManager.AddParameters(4, "@IDNDERMARJE", idNdermarrje, ParameterDirection.Input);

            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "PRC_T_ASETE_AMORTIZIMI_KOKA_SEL_SIPASDateMagNr");
            if (ds == null)
                return null;

            return ds.Tables[0];

        }

        /// <summary>
        /// MODULI ASETE:
        /// Kthen nga databaza nepermjet procedures PRC_T_ASETE_AMORTIZIMI_KOKA_SEL_SIPASMAG_DATE_NENKATDOK id e dokumentin te amortizimit nga nje magazine ne nje date te caktuar me nje lloj dokumenti te caktuar ne nje nga standartet.
        /// </summary>
        /// <param name="idNiveli">(int) Id e nenkategorise qe do te perdoret per fleten e amortizimit FA.</param>
        /// <param name="dateDokumenti">(DateTime) Data e dokumentit te amortizimit.</param>
        /// <param name="idNjesiAdministrative">(int) Id e njesise administrative per te cilen po behet regjistrimi i dokumentit te amortizimit.</param>
        /// <param name="idLlojStandarti">(int) Id e llojit te standartit per te cilen po behet llogaritja e amortizimit.</param>
        /// <param name="idNdermarrje">(int) Id e ndermarrjes per te cilen po behet regjistrimi i amortizimit.</param>
        /// <returns>Kthen id e dokumentit te amortizimit sipas parametrave ne standartin specifik nga tabela T_ASETE_AMORTIZIMI_KOKA.</returns>
        internal int ktheIDAmortizimKokaSipasMagDateNenKatDok(int idNiveli, DateTime dateDokumenti, int idNjesiAdministrative, int idLlojStandarti, int idNdermarrje)
        {
            dbManager.Open();
            dbManager.CreateParameters(5);
            dbManager.AddParameters(0, "@IDNIVELI", idNiveli, ParameterDirection.Input);
            dbManager.AddParameters(1, "@DATE_DOKUMENTI", dateDokumenti, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDNJESIADMINISTRATIVE", idNjesiAdministrative, ParameterDirection.Input);
            dbManager.AddParameters(3, "@IDLLOJSTANDARTI", idLlojStandarti, ParameterDirection.Input);
            dbManager.AddParameters(4, "@IDNDERMARJE", idNdermarrje, ParameterDirection.Input);

            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "PRC_T_ASETE_AMORTIZIMI_KOKA_SEL_SIPASMAG_DATE_NENKATDOK");
            if (ds == null)
                return -1;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return -1;

            int idAmortizimiKoka;
            int.TryParse(ds.Tables[0].Rows[0]["ID_AMORTIZIMI"].ToString(), out idAmortizimiKoka);
            return idAmortizimiKoka;
        }

        #endregion

        #region Amortizimi Trupi

        /// <summary>
        /// MODULI ASETE:
        /// Ruan ne tabelen T_ASETE_AMORTIZIMI_TRUPI objektin e trupit te amortizimit nepermjet procedures PRC_T_ASETE_AMORTIZIMI_TRUPI_INSERT.
        /// </summary>
        /// <param name="idAmortizimiTrupi">(int) Id automatike e amortizimit te trupit.</param>
        /// <param name="idAmortizimKoka">(int) Id automatike e kokes qe po gjeneron kete trup.</param>
        /// <param name="nrRendor">(int) Nr rendor i rreshtit te trupit.</param>
        /// <param name="idArtikulli">(int) Id e artikullit qe po amortizohet.</param>
        /// <param name="idArtikull_LlojAmortizimi">(int) Id se per cfare lloj amortizimi eshte rreshti qe po amortizohet.</param>
        /// <param name="dateAmortizimi">(DateTime) Data e amortizimit te rreshtit te trupit.</param>
        /// <param name="dateMePareAmortizimi">(DateTime) Data e meparshme e amortizimit e serialit qe ndodhet te rreshti.</param>
        /// <param name="dateMagazineInaktive">(DateTime) Data e nderrimit te statusit te magazines per kete serial. Perdoret per te llogaritur 3 muajt ne nje magazine para se te filloj te amortizoje,</param>
        /// <param name="idNjesiAdministrative">(int) Id e njesise administrative ku po ndodh amortizimi.</param>
        /// <param name="normaAmortizimi">(float) Norma e amortizimit me te cilen po llogaritet rreshti i amortizimit.</param>
        /// <param name="amortizimiShtese">(float) Amortizimi shtese i llogaritur per ditet e pallogaritura te amortizimit.</param>
        /// <param name="amortizimiGjithsej">(float) Amortizimi gjithsej qe kur eshte llogaritur per here te pare ne kete vit deri ne ditet aktuale.</param>
        /// <param name="amortizimiVjetor">(float) Amortizimi gjithsej qe kur eshte llogaritur per here te pare deri ne ditet aktuale</param>
        /// <param name="vleftaPlusMinus">(float) Vlera e amortizimit shtese qe i shtohet ose i hiqet rreshtit gjate levizjeve nga magazina aktuale.</param>
        /// <param name="hdAmortizimGjithsej">(float) Vlera e amortizimit gjithsej qe i shtohet ose i hiqet rreshtit gjate levizjeve nga magazina aktuale.</param>
        /// <param name="hdAmortizimVjetor">(float) Vlera e amortizimit vjetor qe i shtohet ose i hiqet rreshtit gjate levizjeve nga magazina aktuale.</param>
        /// <param name="vleftaGjendje">(float) Vlefta totale e blerjes se serialit qe po amortizojme.</param>
        /// <param name="diteAmortizimi">(float) Ditet e amortizimit aktual.</param>
        /// <param name="idAQTSeriali">(int) Id e serialit te aqt-se qe po amortizohet.</param>
        /// <returns>Kthen True nese ruajtja kryhet me sukses ose False ne te kundert.</returns>
        internal override clsMesazh ruajAmortizimiTrupi(out int idAmortizimiTrupi, int idAmortizimKoka, int nrRendor, int idArtikulli, int idArtikull_LlojAmortizimi, DateTime dateAmortizimi, DateTime dateMePareAmortizimi, DateTime dateMagazineInaktive, int idNjesiAdministrative,
            double normaAmortizimi, double amortizimiShtese, double amortizimiGjithsej, double amortizimiVjetor, double vleftaPlusMinus, double hdAmortizimGjithsej, double hdAmortizimVjetor, double vleftaGjendje, double diteAmortizimi, int idAQTSeriali, double vleftaShteseRivleresim)
        {
            idAmortizimiTrupi = 0;

            dbManager.Open();
            dbManager.CreateParameters(20);
            dbManager.AddParameters(0, "@ID_AMORTIZIMI_TRUPI", idAmortizimiTrupi, ParameterDirection.Output);
            dbManager.AddParameters(1, "@IDAMORTIZIMIKOKA", idAmortizimKoka, ParameterDirection.Input);
            dbManager.AddParameters(2, "@NRRENDOR", nrRendor, ParameterDirection.Input);
            dbManager.AddParameters(3, "@IDARTIKULLI", idArtikulli, ParameterDirection.Input);
            dbManager.AddParameters(4, "@IDARTIKULL_LLOJAMORT", idArtikull_LlojAmortizimi, ParameterDirection.Input);
            dbManager.AddParameters(5, "@DATE_AMORTIZIMI", dateAmortizimi, ParameterDirection.Input);
            dbManager.AddParameters(6, "@DATE_MEPARE_AMORTIZIMI", dateMePareAmortizimi, ParameterDirection.Input);
            dbManager.AddParameters(7, "@DATE_NDRYSHIM_STATUSMAGAZINE", dateMagazineInaktive, ParameterDirection.Input);
            dbManager.AddParameters(8, "@IDNJESIADMINISTRATIVE", idNjesiAdministrative, ParameterDirection.Input);
            dbManager.AddParameters(9, "@NORMAAMORTIZIMI", normaAmortizimi, ParameterDirection.Input);
            dbManager.AddParameters(10, "@AMORTIZIMISHTESE", amortizimiShtese, ParameterDirection.Input);
            dbManager.AddParameters(11, "@AMORTIZIMIGJITHSEJ", amortizimiGjithsej, ParameterDirection.Input);
            dbManager.AddParameters(12, "@AMORTIZIMIVJETOR", amortizimiVjetor, ParameterDirection.Input);
            dbManager.AddParameters(13, "@VLEFTAPLUSMINUS", vleftaPlusMinus, ParameterDirection.Input);
            dbManager.AddParameters(14, "@HD_AMORTGJITHSEJ", hdAmortizimGjithsej, ParameterDirection.Input);
            dbManager.AddParameters(15, "@HD_AMORTVJETOR", hdAmortizimVjetor, ParameterDirection.Input);
            dbManager.AddParameters(16, "@VLEFTAGJENDJE", vleftaGjendje, ParameterDirection.Input);
            dbManager.AddParameters(17, "@DITEAMORTIZIMI", diteAmortizimi, ParameterDirection.Input);
            dbManager.AddParameters(18, "@IDSERIALI", idAQTSeriali, ParameterDirection.Input);
            dbManager.AddParameters(19, "@VLEFTASHTESERIVLERESIM", vleftaShteseRivleresim, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "PRC_T_ASETE_AMORTIZIMI_TRUPI_INSERT");
            idAmortizimiTrupi = int.Parse(dbManager.Parameters[0].Value.ToString());
            return new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);

        }

        /// <summary>
        /// MODULI ASETE:
        /// Modifikon ne tabelen T_ASETE_AMORTIZIMI_TRUPI objektin e trupit te amortizimit nepermjet procedures PRC_T_ASETE_AMORTIZIMI_TRUPI_MODIFIKIM.
        /// </summary>
        /// <param name="idAmortizimiTrupi">(int) Id automatike e amortizimit te trupit.</param>
        /// <param name="idAmortizimKoka">(int) Id automatike e kokes qe po gjeneron kete trup.</param>
        /// <param name="dateMePareAmortizimi">(DateTime) Data e meparshme e amortizimit e serialit qe ndodhet te rreshti.</param>
        /// <param name="dateMagazineInaktive">(DateTime) Data e nderrimit te statusit te magazines per kete serial. Perdoret per te llogaritur 3 muajt ne nje magazine para se te filloj te amortizoje,</param>
        /// <param name="idNjesiAdministrative">(int) Id e njesise administrative ku po ndodh amortizimi.</param>
        /// <param name="normaAmortizimi">(float) Norma e amortizimit me te cilen po llogaritet rreshti i amortizimit.</param>
        /// <param name="amortizimiShtese">(float) Amortizimi shtese i llogaritur per ditet e pallogaritura te amortizimit.</param>
        /// <param name="amortizimiGjithsej">(float) Amortizimi gjithsej qe kur eshte llogaritur per here te pare ne kete vit deri ne ditet aktuale.</param>
        /// <param name="amortizimiVjetor">(float) Amortizimi gjithsej qe kur eshte llogaritur per here te pare deri ne ditet aktuale</param>
        /// <param name="vleftaPlusMinus">(float) Vlera e amortizimit shtese qe i shtohet ose i hiqet rreshtit gjate levizjeve nga magazina aktuale.</param>
        /// <param name="hdAmortizimGjithsej">(float) Vlera e amortizimit gjithsej qe i shtohet ose i hiqet rreshtit gjate levizjeve nga magazina aktuale.</param>
        /// <param name="hdAmortizimVjetor">(float) Vlera e amortizimit vjetor qe i shtohet ose i hiqet rreshtit gjate levizjeve nga magazina aktuale.</param>
        /// <param name="vleftaGjendje">(float) Vlefta totale e blerjes se serialit qe po amortizojme.</param>
        /// <param name="diteAmortizimi">(float) Ditet e amortizimit aktual.</param>
        /// <returns>Kthen True nese modifikimi kryhet me sukses ose False ne te kundert.</returns>
        internal override clsMesazh modifikoAmortizimiTrupiPerRillogaritje(int idAmortizimiTrupi, int idAmortizimKoka, DateTime dateMePareAmortizimi, DateTime dateMagazineInaktive, int idNjesiAdministrative,
            double normaAmortizimi, double amortizimiShtese, double amortizimiGjithsej, double amortizimiVjetor, double vleftaPlusMinus, double hdAmortizimGjithsej, double hdAmortizimVjetor, double vleftaGjendje, double diteAmortizimi, double vleftaShteseRivleresim)
        {
            dbManager.Open();
            dbManager.CreateParameters(15);
            dbManager.AddParameters(0, "@ID_AMORTIZIMI_TRUPI", idAmortizimiTrupi, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDAMORTIZIMIKOKA", idAmortizimKoka, ParameterDirection.Input);
            dbManager.AddParameters(2, "@DATE_MEPARE_AMORTIZIMI", dateMePareAmortizimi, ParameterDirection.Input);
            dbManager.AddParameters(3, "@DATE_NDRYSHIM_STATUSMAGAZINE", dateMagazineInaktive, ParameterDirection.Input);
            dbManager.AddParameters(4, "@IDNJESIADMINISTRATIVE", idNjesiAdministrative, ParameterDirection.Input);
            dbManager.AddParameters(5, "@NORMAAMORTIZIMI", normaAmortizimi, ParameterDirection.Input);
            dbManager.AddParameters(6, "@AMORTIZIMISHTESE", amortizimiShtese, ParameterDirection.Input);
            dbManager.AddParameters(7, "@AMORTIZIMIGJITHSEJ", amortizimiGjithsej, ParameterDirection.Input);
            dbManager.AddParameters(8, "@AMORTIZIMIVJETOR", amortizimiVjetor, ParameterDirection.Input);
            dbManager.AddParameters(9, "@VLEFTAPLUSMINUS", vleftaPlusMinus, ParameterDirection.Input);
            dbManager.AddParameters(10, "@HD_AMORTGJITHSEJ", hdAmortizimGjithsej, ParameterDirection.Input);
            dbManager.AddParameters(11, "@HD_AMORTVJETOR", hdAmortizimVjetor, ParameterDirection.Input);
            dbManager.AddParameters(12, "@VLEFTAGJENDJE", vleftaGjendje, ParameterDirection.Input);
            dbManager.AddParameters(13, "@DITEAMORTIZIMI", diteAmortizimi, ParameterDirection.Input);
            dbManager.AddParameters(14, "@VLEFTASHTESERIVLERESIM", vleftaShteseRivleresim, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "PRC_T_ASETE_AMORTIZIMI_TRUPI_MODIFIKIM");
            return new clsMesazh(true, MessagesResource.Messages["msgModifikimiMeSukses"]);

        }

        /// <summary>
        /// MODULI ASETE:
        /// Kthen nga databaza nepermjet procedures PRC_T_ASETE_AMORTIZIMI_TRUPI_SEL_SIPASIDKOKA nje DataTable me te gjithe trupin e kokes se dokumentit qe ne kerkojme.
        /// </summary>
        /// <param name="idAmortizimKoka">(int) Id automatike e kokes qe po gjeneron kete trup.</param>
        /// <returns>Kthen nje DataTable me trupin e nje koke te nje dokumenti amortizimi nga tabela T_ASETE_AMORTIZIMI_TRUPI.</returns>
        internal override DataTable ktheAmortizimTrupiSipasIdKokaAmortizimi(int idAmortizimKoka)
        {

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDAMORTIZIMIKOKA", idAmortizimKoka, ParameterDirection.Input);

            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "PRC_T_ASETE_AMORTIZIMI_TRUPI_SEL_SIPASIDKOKA");
            return ds.Tables[0];

        }

        /// <summary>
        /// MODULI ASETE:
        /// Kthen nga databaza nepermjet procedures PRC_T_ASETE_AMORTIZIMI_TRUPI_SEL_SIPASIDKOKAGrupSipasArtikullit nje DataTable me te gjithe trupin e kokes se dokumentit qe ne kerkojme sipas artikujve.
        /// </summary>
        /// <param name="idAmortizimKoka">(int) Id automatike e kokes qe po gjeneron kete trup.</param>
        /// <returns>Kthen nje DataTable me trupin e nje koke te nje dokumenti amortizimi nga tabela T_ASETE_AMORTIZIMI_TRUPI.</returns>
        internal override DataTable ktheAmortizimTrupiSipasIdKokaAmortizimiGrupSipasArtikullit(int idAmortizimKoka)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDAMORTIZIMIKOKA", idAmortizimKoka, ParameterDirection.Input);

            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "PRC_T_ASETE_AMORTIZIMI_TRUPI_SEL_SIPASIDKOKAGrupSipasArtikullit");
            return ds.Tables[0];
        }

        /// <summary>
        /// MODULI ASETE:
        /// Kthen nga databaza nepermjet procedures PRC_T_ASETE_AMORTIZIMI_TRUPI_SEL_SIPASIDKOKA_IDNJESI nje DataTable me te gjithe trupin e kokes se dokumentit ne magazinen qe ne kerkojme.
        /// </summary>
        /// <param name="idAmortizimKoka">(int) Id automatike e kokes qe po gjeneron kete trup.</param>
        /// <param name="idNjesiAdministrative">(int) Id e njesi administrative ku po ndodh ndryshimi i statusit.</param>
        /// <returns>Kthen nje DataTable me trupin e nje koke te nje dokumenti amortizimi ne magazinen qe kerkojme nga tabela T_ASETE_AMORTIZIMI_TRUPI.</returns>
        internal override DataTable ktheAmortizimTrupiSipasIdKokaAmortizimiIdNjesiAdministrative(int idAmortizimKoka, int idNjesiAdministrative)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDAMORTIZIMIKOKA", idAmortizimKoka, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNJESIADMINISTRATIVE", idNjesiAdministrative, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "PRC_T_ASETE_AMORTIZIMI_TRUPI_SEL_SIPASIDKOKA_IDNJESI");
            return ds.Tables[0];
        }

        /// <summary>
        /// MODULI ASETE:
        /// Kthen nga databaza nepermjet procedures PRC_T_ASETE_AMORTIZIMI_TRUPI_SEL_VEPRIMETEFUNDIT nje DataTable me te gjithe amortizimet e fundit per secilin serial ne ndermarrje.
        /// </summary>
        /// <param name="idNdermarrje">(int) Id e ndermarrjes per te cilen po behet regjistrimi i amortizimit.</param>
        /// <param name="dataMaksimaleZgjedhje">(DateTime) Data maksimale e zgjedhjeve te dokumetave te meparshem te amortizimit.</param>
        /// <param name="idLlojStandarti">(int) Id e llojit te standartit qe behet kontrolli.</param>
        /// <returns>Kthen nje DataTable me trupin e amortizimit te fundit per cdo serial te magazines qe kerkojme nga tabela T_ASETE_AMORTIZIMI_TRUPI.</returns>
        internal override DataTable ktheAmortizimTrupiAmortFunditSipasRenditjes(int idNdermarrje, DateTime dataMaksimaleZgjedhje, int idLlojStandarti, int nrRenditje)
        {

            dbManager.Open();
            dbManager.CreateParameters(4);
            dbManager.AddParameters(0, "@IDNDERMARJE", idNdermarrje, ParameterDirection.Input);
            dbManager.AddParameters(1, "@DATE_AMORTIZIMI", dataMaksimaleZgjedhje, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDLLOJSTANDARTI", idLlojStandarti, ParameterDirection.Input);
            dbManager.AddParameters(3, "@NR_RENDITJE", nrRenditje, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "PRC_T_ASETE_AMORTIZIMI_TRUPI_SEL_VEPRIMETEFUNDIT");
            return ds.Tables[0];

        }
        internal override DataTable ktheAmortizimTrupiAmortVeprimePas(int idNdermarrje, DateTime dataMaksimaleZgjedhje, int idLlojStandarti, int nrRenditje)
        {

            dbManager.Open();
            dbManager.CreateParameters(4);
            dbManager.AddParameters(0, "@IDNDERMARJE", idNdermarrje, ParameterDirection.Input);
            dbManager.AddParameters(1, "@DATE_AMORTIZIMI", dataMaksimaleZgjedhje, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDLLOJSTANDARTI", idLlojStandarti, ParameterDirection.Input);
            dbManager.AddParameters(3, "@NR_RENDITJE", nrRenditje, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "PRC_T_ASETE_AMORTIZIMI_TRUPI_SEL_kaVeprimePas");
            return ds.Tables[0];

        }

        internal override DataTable ktheAmortizimTrupiAmortFunditSipasRenditjes(int idNdermarrje, DateTime dataMaksimaleZgjedhje, int idLlojStandarti, int nrRenditje, int idDok)
        {

            dbManager.Open();
            dbManager.CreateParameters(5);
            dbManager.AddParameters(0, "@IDNDERMARJE", idNdermarrje, ParameterDirection.Input);
            dbManager.AddParameters(1, "@DATE_AMORTIZIMI", dataMaksimaleZgjedhje, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDLLOJSTANDARTI", idLlojStandarti, ParameterDirection.Input);
            dbManager.AddParameters(3, "@NR_RENDITJE", nrRenditje, ParameterDirection.Input);
            dbManager.AddParameters(4, "@IDDOK", idDok, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "PRC_T_ASETE_AMORTIZIMI_TRUPI_SEL_VEPRIMETEFUNDIT_idDok");
            return ds.Tables[0];

        }

        /// <summary>
        /// MODULI ASETE:
        /// Kthen nga databaza nepermjet procedures PRC_T_ASETE_AMORTIZIMI_TRUPI_SEL_SIPASSTANDART_IDNDERMARRJE nje DataTable me te gjithe trupat te mundur per rillogaritje ne varesi te dates vetem per artikujt qe kane serial.
        /// </summary>
        /// <param name="idNdermarrje">(int) Id e ndermarrjes per te cilen po behet regjistrimi i amortizimit.</param>
        /// <param name="dataFillimRillogaritje">(DateTime) Data minimale nga fillon rillogaritja.</param>
        /// <param name="idLlojStandarti">(int) Id e llojit te standartit qe behet kontrolli.</param>
        /// <returns>Kthen nje DataTable me trupin e amortizimit te per rillogaritje ne varesi te dates nga tabela T_ASETE_AMORTIZIMI_TRUPI.</returns>
        internal override DataTable ktheAmortizimTrupiPerRillogaritje(int idNdermarrje, DateTime dataFillimRillogaritje, int idLlojStandarti)
        {
            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@IDNDERMARJE", idNdermarrje, ParameterDirection.Input);
            dbManager.AddParameters(1, "@DATE_AMORTIZIMI", dataFillimRillogaritje, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDLLOJSTANDARTI", idLlojStandarti, ParameterDirection.Input);
            return dbManager.ExecuteDataSet(CommandType.StoredProcedure, "PRC_T_ASETE_AMORTIZIMI_TRUPI_SEL_SIPASSTANDART_IDNDERMARRJE").Tables[0];
        }

        /// <summary>
        /// MODULI ASETE:
        /// Kthen nga databaza nepermjet procedures PRC_T_ASETE_AMORTIZIMI_TRUPI_SEL_SIPASSTANDART_IDNDERMARRJE_ARTIKUJPASERIAL nje DataTable me te gjithe trupat te mundur per rillogaritje ne varesi te dates vetem per artikujt qe s'kane seriale.
        /// </summary>
        /// <param name="idNdermarrje">(int) Id e ndermarrjes per te cilen po behet regjistrimi i amortizimit.</param>
        /// <param name="dataFillimRillogaritje">(DateTime) Data minimale nga fillon rillogaritja.</param>
        /// <param name="idLlojStandarti">(int) Id e llojit te standartit qe behet kontrolli.</param>
        /// <returns>Kthen nje DataTable me trupin e amortizimit te per rillogaritje ne varesi te dates nga tabela T_ASETE_AMORTIZIMI_TRUPI.</returns>
        internal override DataTable ktheAmortizimTrupiPerRillogaritjeArtikujPaSerial(int idNdermarrje, DateTime dataFillimRillogaritje, int idLlojStandarti)
        {
            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@IDNDERMARJE", idNdermarrje, ParameterDirection.Input);
            dbManager.AddParameters(1, "@DATE_AMORTIZIMI", dataFillimRillogaritje, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDLLOJSTANDARTI", idLlojStandarti, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "PRC_T_ASETE_AMORTIZIMI_TRUPI_SEL_SIPASSTANDART_IDNDERMARRJE_ARTIKUJPASERIAL");
            return ds.Tables[0];
        }

        /// <summary>
        /// MODULI ASETE:
        /// Kthen nga databaza nepermjet procedures PRC_T_ASETE_AMORTIZIMI_TRUPI_SEL_VEPRIMETEFUNDIT_TESERIALIT nje DataTable me te gjithe amortizimet e fundit per serialin specifik ne ndermarrje.
        /// </summary>
        /// <param name="idNdermarrje">(int) Id e ndermarrjes per te cilen po behet regjistrimi i amortizimit.</param>
        /// <param name="dataMaksimaleZgjedhje">(DateTime) Data maksimale e zgjedhjeve te dokumetave te meparshem te amortizimit.</param>
        /// <param name="idLlojStandarti">(int) Id e llojit te standartit qe behet kontrolli.</param>
        /// <param name="idSeriali">(int) Id e serialit qe kerkojme veprimin paraardhes.</param>
        /// <returns>Kthen nje DataTable me trupin e amortizimit te fundit per serialin specifik qe kerkojme nga tabela T_ASETE_AMORTIZIMI_TRUPI.</returns>
        internal override DataRow ktheAmortizimTrupiAmortFunditSipasSerialit(int idNdermarrje, DateTime dataMaksimaleZgjedhje, int idLlojStandarti, int idSeriali, int renditje)
        {
            dbManager.Open();
            dbManager.CreateParameters(5);
            dbManager.AddParameters(0, "@IDNDERMARJE", idNdermarrje, ParameterDirection.Input);
            dbManager.AddParameters(1, "@DATE_AMORTIZIMI", dataMaksimaleZgjedhje, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDLLOJSTANDARTI", idLlojStandarti, ParameterDirection.Input);
            dbManager.AddParameters(3, "@IDSERIALI", idSeriali, ParameterDirection.Input);
            dbManager.AddParameters(4, "@NR_RENDITJE", renditje, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "PRC_T_ASETE_AMORTIZIMI_TRUPI_SEL_VEPRIMETEFUNDIT_TESERIALIT");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];
        }

        /// <summary>
        /// MODULI ASETE:
        /// Kthen nga databaza nepermjet procedures PRC_T_ASETE_AMORTIZIMI_TRUPI_SEL_SIPASIDSERIAL_DATEAMORT nje DataTable me rreshtat qe plotesojne kushtet.
        /// </summary>
        /// <param name="idSeriali">(int) Id e serialit te aqt-se qe po amortizohet.</param>
        /// <param name="dataAmortizimi">(DateTime) Data e amortizimit te rreshtit te trupit.</param>
        /// <param name="idStandartAmortizimi">(int) Id e standartit te amortizimit.</param>
        /// <returns>Kthen int numrin e rekordeve te gjetur nga tabela T_ASETE_NJESIADMINISTRATIVE_HISTORIKU.</returns>
        internal override int ktheNrAmortizimTrupiSipasIdSerialiDateAmortizimi(int idSeriali, DateTime dataAmortizimi, int idStandartAmortizimi)
        {

            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@IDSERIALI", idSeriali, ParameterDirection.Input);
            dbManager.AddParameters(1, "@DATE_AMORTIZIMI", dataAmortizimi, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDSTANDARTAMORTIZIMI", idStandartAmortizimi, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "PRC_T_ASETE_AMORTIZIMI_TRUPI_SEL_SIPASIDSERIAL_DATEAMORT");
            if (ds == null)
                return 0;
            return ds.Tables[0].Rows.Count;

        }

        internal override bool kaVeprimePasPerKeteSerial(int idSeriali, DateTime dataAmortizimi, int idStandartAmortizimi)
        {

            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@IDSERIALI", idSeriali, ParameterDirection.Input);
            dbManager.AddParameters(1, "@DATE_AMORTIZIMI", dataAmortizimi, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDSTANDARTAMORTIZIMI", idStandartAmortizimi, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "PRC_T_ASETE_AMORTIZIMI_TRUPI_kaAmortizimePas");
            if (ds == null)
                return false;
            if (ds.Tables[0].Rows.Count == 0)
                return false;
            else return true;


        }

        #endregion

        #region Lloje amortizimi

        /// <summary>
        /// MODULI ASETE:
        /// Kthen nga databaza nepermjet procedures PRC_T_ASETE_LLOJAMORTIZIMI_SEL nje DataTable me te llojet (menyrat) e amortizimeve.
        /// </summary>
        /// <returns>Kthen nje DataTable me llojin e amori nga tabela T_ASETE_LLOJAMORTIZIMI.</returns>
        internal DataTable ktheLlojAmortizimeshTeGjitha()
        {
            dbManager.Open();
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "PRC_T_ASETE_LLOJAMORTIZIMI_SEL");
            return ds.Tables[0];
        }

        /// <summary>
        /// MODULI ASETE:
        /// Kthen nga databaza nepermjet procedures PRC_T_ASETE_LLOJAMORTIZIMI_SEL_SIPASID nje DataRow me llojin e amortizimit te kerkuar sipas id se llojit te amortizimit.
        /// </summary>
        /// <param name="idLlojAmortizimi">(int) Id automatike e llojit te amortizimit.</param>
        /// <returns>Kthen nje string me emeritimin e llojit te amoritizimit nga tabela T_ASETE_LLOJAMORTIZIMI.</returns>
        internal string ktheEmeritimiLlojAmortizimiSipasID(int idLlojAmortizimi)
        {

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@ID_LLOJ_AMORTIZIMI", idLlojAmortizimi, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "PRC_T_ASETE_LLOJAMORTIZIMI_SEL_SIPASID");
            if (ds == null)
                return "";
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return "";

            string emertimiLlojAmortizimi = ds.Tables[0].Rows[0]["LLOJ_AMORTIZIMI"].ToString();
            return emertimiLlojAmortizimi;

        }

        internal int ktheIdLlojAmortizimiSipasEmertimit(string lloj)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@lloj", lloj, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "PRC_T_ASETE_LLOJAMORTIZIMI_SEL_Llojit");
            if (ds == null)
                return -1;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return -1;

            int idlloj = int.Parse(ds.Tables[0].Rows[0]["ID_LLOJ_AMORTIZIMI"].ToString());
            return idlloj;
        }

        #endregion

        #region Grup Norma Amortizimi

        /// <summary>
        /// MODULI ASETE:
        /// Ruan ne tabelen T_ASETE_LIDHJE_GRUP_LLOJAMORT objektin e normave te amortizimit te grupit nepermjet procedures PRC_T_ASETE_LIDHJE_GRUP_LLOJAMORT_INSERT.
        /// </summary>
        /// <param name="idLidhjeGrupLlojAmort">(int) Id automatike e normas se grupit dhe standartit.</param>
        /// <param name="idKonfigurimArtikujsh">(int) Id e grupit qe po i regjistrohet norma.</param>
        /// <param name="idLlojAmortizimi">(int) Id e llojit te amortizimit.</param>
        /// <param name="idStandartAmortizimi">Id e standartit qe po i regjistrohet norma.</param>
        /// <param name="normeMagazine">(bool) Nese amortizimi do te varet nga norma ne magazine ose nga ajo artikullit.</param>
        /// <param name="norme">(float) Norma e amortizimit te grupit.</param>
        /// <param name="idStatusDokumenti">(int) Id se ne cfare gjendje eshte regjistrimi i normes se amortizimit te grupit, i ruajtur, fshire, apo modifikuar</param>
        /// <param name="idNdermarrje">(int) Id e ndermarrjes per te cilen po behet regjistrimi.</param>
        /// <param name="idPerdoruesi">(int) Id e perdoruesit qe modifikon dokumentin i fundit.</param>
        /// <param name="idKrijuesi">(int) Id e krijuesit per here te pare te regjistrimit te normes.</param>
        /// <returns>Kthen clsMesazh me atribut statusi True nese ruajtja kryhet me sukses ose False dhe mesazhin e gabimit ne te kundert.</returns>
        internal clsMesazh ruajGrupNormaAmortizimi(out int idLidhjeGrupLlojAmort, int idKonfigurimArtikujsh, int idLlojAmortizimi, int idStandartAmortizimi, bool normeMagazine, double norme,
            int idStatusDokumenti, int idNdermarrje, int idPerdoruesi, int idKrijuesi)
        {
            idLidhjeGrupLlojAmort = -1;

            dbManager.Open();
            dbManager.CreateParameters(10);
            dbManager.AddParameters(0, "@ID_GRUP_LLOJAMORT", idLidhjeGrupLlojAmort, ParameterDirection.Output);
            dbManager.AddParameters(1, "@IDKONFIGURIMARTIKULLI", idKonfigurimArtikujsh, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDLLOJAMORTIZIMI", idLlojAmortizimi, ParameterDirection.Input);
            dbManager.AddParameters(3, "@IDSTANDARTAMORT", idStandartAmortizimi, ParameterDirection.Input);
            dbManager.AddParameters(4, "@NORMEMAGAZINE", normeMagazine, ParameterDirection.Input);
            dbManager.AddParameters(5, "@NORMA", norme, ParameterDirection.Input);
            dbManager.AddParameters(6, "@IDSTATUSDOK", idStatusDokumenti, ParameterDirection.Input);
            dbManager.AddParameters(7, "@IDNDERMARRJE", idNdermarrje, ParameterDirection.Input);
            dbManager.AddParameters(8, "@IDPERDORUESI", idPerdoruesi, ParameterDirection.Input);
            dbManager.AddParameters(9, "@IDKRIJUESI", idKrijuesi, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "PRC_T_ASETE_LIDHJE_GRUP_LLOJAMORT_INSERT");
            idLidhjeGrupLlojAmort = int.Parse(dbManager.Parameters[0].Value.ToString());
            clsMesazh mesazh = new clsMesazh(true, "Ruatja perfundoi me sukses!");
            return mesazh;
        }

        /// <summary>
        /// MODULI ASETE:
        /// Modifikon ne tabelen T_ASETE_LIDHJE_GRUP_LLOJAMORT objektin e normave te amortizimit te grupit nepermjet procedures PRC_T_ASETE_LIDHJE_GRUP_LLOJAMORT_UPDATE.
        /// </summary>
        /// <param name="idLidhjeGrupLlojAmort">(int) Id automatike e normas se grupit dhe standartit.</param>
        /// <param name="idLlojAmortizimi">(int) Id e llojit te amortizimit.</param>
        /// <param name="normeMagazine">(bool) Nese amortizimi do te varet nga norma ne magazine ose nga ajo artikullit.</param>
        /// <param name="norme">(float) Norma e amortizimit te grupit.</param>
        /// <param name="idPerdoruesi">(int) Id e perdoruesit qe modifikon dokumentin i fundit.</param>
        /// <returns>Kthen clsMesazh me atribut statusi True nese modifikimi kryhet me sukses ose False dhe mesazhin e gabimit ne te kundert.</returns>
        internal clsMesazh modifikimiGrupNormaAmortizimi(int idLidhjeGrupLlojAmort, int idLlojAmortizimi, bool normeMagazine, double norme, int idPerdoruesi)
        {
            dbManager.Open();
            dbManager.CreateParameters(5);
            dbManager.AddParameters(0, "@ID_GRUP_LLOJAMORT", idLidhjeGrupLlojAmort, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDLLOJAMORTIZIMI", idLlojAmortizimi, ParameterDirection.Input);
            dbManager.AddParameters(2, "@NORMEMAGAZINE", normeMagazine, ParameterDirection.Input);
            dbManager.AddParameters(3, "@NORMA", norme, ParameterDirection.Input);
            dbManager.AddParameters(4, "@IDPERDORUESI", idPerdoruesi, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "PRC_T_ASETE_LIDHJE_GRUP_LLOJAMORT_UPDATE");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgModifikimiMeSukses"]);
            return mesazh;
        }

        /// <summary>
        /// MODULI ASETE:
        /// Fshin objektin e normave te amortizimit te grupit ne tabelen T_ASETE_LIDHJE_GRUP_LLOJAMORT nepermjet procedures PRC_T_ASETE_LIDHJE_GRUP_LLOJAMORT_DELETE.
        /// </summary>
        /// <param name="idLidhjeGrupLlojAmort">(int) Id automatike e normas se grupit dhe standartit.</param>
        /// <returns>Kthen clsMesazh me atribut statusi True nese fshirja kryhet me sukses ose False dhe mesazhin e gabimit ne te kundert.</returns>
        internal clsMesazh fshiGrupNormaAmortizimi(int idLidhjeGrupLlojAmort)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@ID_GRUP_LLOJAMORT", idLidhjeGrupLlojAmort, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "PRC_T_ASETE_LIDHJE_GRUP_LLOJAMORT_DELETE");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
            return mesazh;
        }

        /// <summary>
        /// MODULI ASETE:
        /// Kthen nga databaza nepermjet procedures PRC_T_ASETE_LIDHJE_GRUP_LLOJAMORT_SEL nje DataTable me te gjitha normat e standarteve te grupit qe kerkohet.
        /// </summary>
        /// <param name="idKonfigArtikulli">(int) Id e grupit qe ka amortizimin.</param>
        /// <param name="idndermarje">(int) Id e ndermarrjes.</param>
        /// <returns>Kthen nje DataTable me te gjitha normat e standarteve te grupit nga tabela T_ASETE_LIDHJE_GRUP_LLOJAMORT.</returns>
        internal DataTable ktheGrupNormaAmortizimiTeGjitha(int idKonfigArtikulli, int idndermarje)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDKONFIGURIMARTIKULLI", idKonfigArtikulli, ParameterDirection.Input);
            dbManager.AddParameters(1, "@idndermarje", idndermarje, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "PRC_T_ASETE_LIDHJE_GRUP_LLOJAMORT_SEL");
            return ds.Tables[0];
        }

        internal DataTable ktheNormaperEkport(int idnderm)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_AmortizimRezervePerEksport");
            return ds.Tables[0];
        }


        /// <summary>
        /// MODULI ASETE:
        /// Kthen nga databaza nepermjet procedures PRC_T_ASETE_LIDHJE_GRUP_LLOJAMORT_SELFillestare nje DataTable me te gjitha normat e amortizimeve fillestare.
        /// </summary>
        /// <param name="idndermarje">(int) Id e ndermarrjes.</param>
        /// <returns>Kthen nje DataTable me te gjitha normat e amortizimeve fillestare nga tabela T_ASETE_LIDHJE_GRUP_LLOJAMORT.</returns>
        internal DataTable ktheGrupNormaAmortizimiFillestare(int idndermarje)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@idndermarje", idndermarje, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "PRC_T_ASETE_LIDHJE_GRUP_LLOJAMORT_SELFillestare");
            return ds.Tables[0];
        }

        /// <summary>
        /// MODULI ASETE:
        /// Kthen nga databaza nepermjet procedures PRC_T_ASETE_LIDHJE_GRUP_LLOJAMORT_SEL_SIPASIDKONFIGURIMI nje DataRow me normat e amortizimeve per standartin dhe grupin e kerkuar.
        /// </summary>
        /// <param name="idKonfigArtikulli">(int) Id e grupit qe ka amortizimin.</param>
        /// <param name="idStandarti">(int) Id e standartit qe ka amortizimin.</param>
        /// <returns>Kthen nje DataRow me normat e amortizimeve nga tabela T_ASETE_LIDHJE_GRUP_LLOJAMORT.</returns>
        internal DataRow ktheGrupNormaAmortizimiSipasIDKonfig(int idKonfigArtikulli, int idStandarti)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDKONFIGURIMARTIKULLI", idKonfigArtikulli, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDSTANDARTAMORT", idStandarti, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "PRC_T_ASETE_LIDHJE_GRUP_LLOJAMORT_SEL_SIPASIDKONFIGURIMI");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];
        }

        /// <summary>
        /// MODULI ASETE:
        /// Kthen nga databaza nepermjet procedures PRC_T_ASETE_LIDHJE_GRUP_LLOJAMORT_SEL_SIPASIDKONFIGURIMI nje DataRow me normat e amortizimeve per standartin dhe grupin e kerkuar.
        /// </summary>
        /// <param name="idKonfigArtikulli">(int) Id e grupit qe ka amortizimin.</param>
        /// <param name="idStandarti">(int) Id e standartit qe ka amortizimin.</param>
        /// <returns>Kthen id e automatike e normes se grupit per standartin e kerkuar nga tabela T_ASETE_LIDHJE_GRUP_LLOJAMORT.</returns>
        internal int ktheIDGrupNormaAmortizimiSipasIDKonfigStandart(int idKonfigArtikulli, int idStandarti)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDKONFIGURIMARTIKULLI", idKonfigArtikulli, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDSTANDARTAMORT", idStandarti, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "PRC_T_ASETE_LIDHJE_GRUP_LLOJAMORT_SEL_SIPASIDKONFIGURIMI");
            if (ds == null)
                return -1;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return -1;

            int idGrupNormaAmortizim;
            int.TryParse(ds.Tables[0].Rows[0]["ID_GRUP_LLOJAMORT"].ToString(), out idGrupNormaAmortizim);
            return idGrupNormaAmortizim;
        }

        /// <summary>
        /// MODULI ASETE:
        /// Kthen nga databaza nepermjet procedures PRC_T_ASETE_LIDHJE_GRUP_LLOJAMORT_SEL_SIPASIDKONFIGURIMI nje DataRow me normat e amortizimeve per standartin dhe grupin e kerkuar.
        /// </summary>
        /// <param name="idKonfigArtikulli">(int) Id e grupit qe ka amortizimin.</param>
        /// <param name="idStandarti">(int) Id e standartit qe ka amortizimin.</param>
        /// <returns>Kthen True nese ekziston njehere specifikimet per normen e grupit dhe standartit dhe False ne te kundert.</returns>
        internal bool ekzistonGrupNormaAmortizimit(int idKonfigArtikulli, int idStandarti)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDKONFIGURIMARTIKULLI", idKonfigArtikulli, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDSTANDARTAMORT", idStandarti, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "PRC_T_ASETE_LIDHJE_GRUP_LLOJAMORT_SEL_SIPASIDKONFIGURIMI");
            if (ds == null)
                return true;
            if (ds.Tables[0].Rows.Count == 0)
                return false;
            return true;
        }

        #endregion

        #region Artikull Norma Amortizimi

        /// <summary>
        /// MODULI ASETE:
        /// Ruan ne tabelen T_ASETE_LIDHJE_ARTIKULL_LLOJAMORT objektin e normave te amortizimit te artikullit nepermjet procedures PRC_T_ASETE_LIDHJE_ARTIKULL_LLOJAMORT_INSERT.
        /// </summary>
        /// <param name="idLidhjeArtikullLlojAmort">(int) Id automatike e normas se grupit dhe standartit.</param>
        /// <param name="idArtikulli">(int) Id e artikullit qe po i regjistrohet norma.</param>
        /// <param name="idLlojAmortizimi">(int) Id e llojit te amortizimit.</param>
        /// <param name="idStandartAmortizimi">Id e standartit qe po i regjistrohet norma.</param>
        /// <param name="normeMagazine">(bool) Nese amortizimi do te varet nga norma ne magazine ose nga ajo artikullit.</param>
        /// <param name="norme">(float) Norma e amortizimit te artikullit.</param>
        /// <returns>Kthen clsMesazh me atribut statusi True nese ruajtja kryhet me sukses ose False dhe mesazhin e gabimit ne te kundert.</returns>
        public override clsMesazh ruajArtikulliNormaAmortizimi(out int idLidhjeArtikullLlojAmort, int idArtikulli, int idLlojAmortizimi, int idStandartAmortizimi, bool normeMagazine, double norme, DateTime dtaktivizimi)
        {
            idLidhjeArtikullLlojAmort = -1;

            dbManager.Open();
            dbManager.CreateParameters(7);
            dbManager.AddParameters(0, "@ID_ARTIKULL_LLOJAMORTIZIM", idLidhjeArtikullLlojAmort, ParameterDirection.Output);
            dbManager.AddParameters(1, "@IDARTIKULLI", idArtikulli, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDLLOJAMORTIZIMI", idLlojAmortizimi, ParameterDirection.Input);
            dbManager.AddParameters(3, "@IDLLOJSTANDARTI", idStandartAmortizimi, ParameterDirection.Input);
            dbManager.AddParameters(4, "@NORMEMAGAZINE", normeMagazine, ParameterDirection.Input);
            dbManager.AddParameters(5, "@NORMA", norme, ParameterDirection.Input);
            dbManager.AddParameters(6, "@DTAKTIVIZIMI", dtaktivizimi, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "PRC_T_ASETE_LIDHJE_ARTIKULL_LLOJAMORT_INSERT");
            idLidhjeArtikullLlojAmort = int.Parse(dbManager.Parameters[0].Value.ToString());
            clsMesazh mesazh = new clsMesazh(true, "Ruatja perfundoi me sukses!");
            return mesazh;
        }

        /// <summary>
        /// MODULI ASETE:
        /// Modifikon ne tabelen T_ASETE_LIDHJE_ARTIKULL_LLOJAMORT objektin e normave te amortizimit te artikullit nepermjet procedures PRC_T_ASETE_LIDHJE_ARTIKULL_LLOJAMORT_UPDATE.
        /// </summary>
        /// <param name="idLidhjeArtikullLlojAmort">(int) Id automatike e normas se grupit dhe standartit.</param>
        /// <param name="idLlojAmortizimi">(int) Id e llojit te amortizimit.</param>
        /// <param name="normeMagazine">(bool) Nese amortizimi do te varet nga norma ne magazine ose nga ajo artikullit.</param>
        /// <param name="norme">(float) Norma e amortizimit te artikullit.</param>
        /// <returns>Kthen clsMesazh me atribut statusi True nese modifikimi kryhet me sukses ose False dhe mesazhin e gabimit ne te kundert.</returns>
        public override clsMesazh modifikimiArtikulliNormaAmortizimi(int idLidhjeArtikullLlojAmort, int idLlojAmortizimi, bool normeMagazine, double norme)
        {
            dbManager.Open();
            dbManager.CreateParameters(4);
            dbManager.AddParameters(0, "@ID_ARTIKULL_LLOJAMORTIZIM", idLidhjeArtikullLlojAmort, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDLLOJAMORTIZIMI", idLlojAmortizimi, ParameterDirection.Input);
            dbManager.AddParameters(2, "@NORMEMAGAZINE", normeMagazine, ParameterDirection.Input);
            dbManager.AddParameters(3, "@NORMA", norme, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "PRC_T_ASETE_LIDHJE_ARTIKULL_LLOJAMORT_UPDATE");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgModifikimiMeSukses"]);
            return mesazh;
        }

        /// <summary>
        /// MODULI ASETE:
        /// Fshin objektin e normave te amortizimit te artikullit ne tabelen T_ASETE_LIDHJE_ARTIKULL_LLOJAMORT nepermjet procedures PRC_T_ASETE_LIDHJE_ARTIKULL_LLOJAMORT_DELETE.
        /// </summary>
        /// <param name="idLidhjeArtikullLlojAmort">(int) Id automatike e normas se grupit dhe standartit.</param>
        /// <returns>Kthen clsMesazh me atribut statusi True nese fshirja kryhet me sukses ose False dhe mesazhin e gabimit ne te kundert.</returns>
        internal override clsMesazh fshiArtikullNormaAmortizimi(int idLidhjeArtikullLlojAmort)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@ID_ARTIKULL_LLOJAMORTIZIM", idLidhjeArtikullLlojAmort, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "PRC_T_ASETE_LIDHJE_ARTIKULL_LLOJAMORT_DELETE");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
            return mesazh;
        }

        /// <summary>
        /// MODULI ASETE:
        /// Kthen nga databaza nepermjet procedures PRC_T_ASETE_LIDHJE_ARTIKULL_LLOJAMORT_SEL nje DataTable me te gjitha normat e standarteve te artikullit qe kerkohet.
        /// </summary>
        /// <param name="idArtikulli">(int) Id e artikullit qe po i regjistrohet norma.</param>
        /// <param name="idndermarje">(int) Id e ndermarrjes.</param>
        /// <returns>Kthen nje DataTable me te gjitha normat e standarteve te artikullit nga tabela T_ASETE_LIDHJE_ARTIKULL_LLOJAMORT.</returns>
        internal override DataTable ktheArtikullNormaAmortizimiTeGjitha(int idArtikulli, int idndermarje, DateTime dtaktivizimi)
        {
            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@IDARTIKULLI", idArtikulli, ParameterDirection.Input);
            dbManager.AddParameters(1, "@idndermarje", idndermarje, ParameterDirection.Input);
            dbManager.AddParameters(2, "@DATA", dtaktivizimi, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "PRC_T_ASETE_LIDHJE_ARTIKULL_LLOJAMORT_SEL");
            return ds.Tables[0];
        }

        /// <summary>
        /// MODULI ASETE:
        /// Kthen nga databaza nepermjet procedures PRC_T_ASETE_LIDHJE_ARTIKULL_LLOJAMORT_SELSiKodifikimi nje DataTable me te gjitha normat e standarteve te artikullit qe kerkohet.
        /// </summary>
        /// <param name="idkodifikimi">(int) Id e grupit te artikullit</param>
        /// <param name="idndermarje">(int) Id e ndermarrjes.</param>
        /// <returns>Kthen nje DataTable me te gjitha normat e standarteve te artikullit nga tabela T_ASETE_LIDHJE_ARTIKULL_LLOJAMORT.</returns>
        internal override DataTable ktheArtikullNormaAmortizimiSipasIdKodifikimit(int idkodifikimi, int idndermarje, DateTime data)
        {
            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@IDKODIFIKIMARTIKULLI", idkodifikimi, ParameterDirection.Input);
            dbManager.AddParameters(1, "@idndermarje", idndermarje, ParameterDirection.Input);
            dbManager.AddParameters(2, "@data", data, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "PRC_T_ASETE_LIDHJE_ARTIKULL_LLOJAMORT_SELSiKodifikimi");
            return ds.Tables[0];
        }

        internal override DataTable merrDataNdryshimiNormaAmortizimi(int idkoka)
        {


            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDKOKA", idkoka, ParameterDirection.Input);
            using (DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_ASETE_LIDHJE_ARTIKULL_LLOJAMORT_merrGjitheDatatSipasIdKoka"))
            {
                return ds.Tables[0];
            }

        }
        /// <summary>
        /// MODULI ASETE:
        /// Kthen nga databaza nepermjet procedures PRC_T_ASETE_LIDHJE_ARTIKULL_LLOJAMORT_SELFillestare nje DataTable me te gjitha normat e standarteve te artikullit qe kerkohet.
        /// </summary>
        /// <param name="idndermarje">(int) Id e ndermarrjes.</param>
        /// <returns>Kthen nje DataTable me te gjitha normat e standarteve te artikullit nga tabela T_ASETE_LIDHJE_ARTIKULL_LLOJAMORT.</returns>
        internal override DataTable ktheArtikullNormaAmortizimiFillestare(int idndermarje, DateTime data)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@idndermarje", idndermarje, ParameterDirection.Input);
            dbManager.AddParameters(1, "@data", data, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "PRC_T_ASETE_LIDHJE_ARTIKULL_LLOJAMORT_SELFillestare");
            return ds.Tables[0];
        }

        /// <summary>
        /// MODULI ASETE:
        /// Kthen nga databaza nepermjet procedures PRC_T_ASETE_LIDHJE_ARTIKULL_LLOJAMORT_SEL_SIPASIDART_IDSTANDART nje DataRow me normat e amortizimeve per standartin dhe artikullin e kerkuar.
        /// </summary>
        /// <param name="idArtikulli">(int) Id e artikullit qe po i regjistrohet norma.</param>
        /// <param name="idStandarti">(int) Id e standartit qe ka amortizimin.</param>
        /// <returns>Kthen nje DataRow me normat e amortizimeve te artikullit nga tabela T_ASETE_LIDHJE_ARTIKULL_LLOJAMORT.</returns>
        internal override DataRow ktheArtikullNormaAmortizimiSipasIDArtikullStandart(int idArtikulli, int idStandarti, DateTime data)
        {

            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@IDARTIKULLI", idArtikulli, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDLLOJSTANDARTI", idStandarti, ParameterDirection.Input);
            dbManager.AddParameters(2, "@data", data, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "PRC_T_ASETE_LIDHJE_ARTIKULL_LLOJAMORT_SEL_SIPASIDART_IDSTANDART_DATE");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];

        }
        /// <summary>
        /// MODULI ASETE:
        /// Kthen nga databaza nepermjet procedures PRC_T_ASETE_LIDHJE_ARTIKULL_LLOJAMORT_METODEAMORTIZIMI_SEL_SIPASIDART_IDSTANDART_DATE 
        /// nje DataTable me normat e amortizimeve dhe metoden perkatese per standartin dhe artikullin e kerkuar.
        /// </summary>
        /// <param name="idArtikulli">(int) Id e artikullit qe po i regjistrohet norma.</param>
        /// <param name="idStandarti">(int) Id e standartit qe ka amortizimin.</param>
        ///  /// <param name="data">(int) data e amortizimit.</param>
        /// <returns>Kthen nje DataTable me normat e amortizimeve te artikullit nga tabela T_ASETE_LIDHJE_ARTIKULL_LLOJAMORT.</returns>
        internal override string merrNormenDheMetodenAmortizimitSipasIdArtikullStandartit(int idArtikulli, int idStandarti, DateTime data)
        {
            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@IDARTIKULLI", idArtikulli, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDLLOJSTANDARTI", idStandarti, ParameterDirection.Input);
            dbManager.AddParameters(2, "@data", data, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "PRC_T_ASETE_LIDHJE_ARTIKULL_LLOJAMORT_METODEAMORTIZIMI_SEL_SIPASIDART_IDSTANDART_DATE");
            if (ds == null)
                return null;
            return ds.Tables[0].Rows[0]["LLOJ_AMORTIZIMI"].ToString();
        }
        /// <summary>
        /// MODULI ASETE:
        /// Kthen nga databaza nepermjet procedures PRC_T_ASETE_LIDHJE_ARTIKULL_LLOJAMORT_SEL_SIPASIDART_IDSTANDART nje DataRow me normat e amortizimeve per standartin dhe artikullin e kerkuar.
        /// </summary>
        /// <param name="idArtikulli">(int) Id e artikullit qe po i regjistrohet norma.</param>
        /// <param name="idStandarti">(int) Id e standartit qe ka amortizimin.</param>
        /// <returns>Kthen id e automatike te normes se artikullit per standartin e kerkuar nga tabela T_ASETE_LIDHJE_ARTIKULL_LLOJAMORT.</returns>
        public override int ktheIDArtikullNormaAmortizimiSipasIDArtikullStandart(int idArtikulli, int idStandarti, DateTime data)
        {
            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@IDARTIKULLI", idArtikulli, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDLLOJSTANDARTI", idStandarti, ParameterDirection.Input);
            dbManager.AddParameters(2, "@data", data, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "PRC_T_ASETE_LIDHJE_ARTIKULL_LLOJAMORT_SEL_SIPASIDART_IDSTANDART");
            if (ds == null)
                return -1;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return -1;

            int idArtikullNormaAmortizim;
            int.TryParse(ds.Tables[0].Rows[0]["ID_ARTIKULL_LLOJAMORTIZIM"].ToString(), out idArtikullNormaAmortizim);
            return idArtikullNormaAmortizim;
        }

        /// <summary>
        /// MODULI ASETE:
        /// Kthen nga databaza nepermjet procedures PRC_T_ASETE_LIDHJE_ARTIKULL_LLOJAMORT_SEL_SIPASIDART_IDSTANDART nje DataRow me normat e amortizimeve per standartin dhe artikullin e kerkuar.
        /// </summary>
        /// <param name="idArtikulli">(int) Id e artikullit qe po i regjistrohet norma.</param>
        /// <param name="idStandarti">(int) Id e standartit qe ka amortizimin.</param>
        /// <returns>Kthen id e llojit te amortizimit per standartin dhe artikullin e kerkuar nga tabela T_ASETE_LIDHJE_ARTIKULL_LLOJAMORT.</returns>
        internal override int ktheIDLlojAmortizimiNormaAmortizimiSipasIDArtikullStandart(int idArtikulli, int idStandarti, DateTime data)
        {

            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@IDARTIKULLI", idArtikulli, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDLLOJSTANDARTI", idStandarti, ParameterDirection.Input);
            dbManager.AddParameters(2, "@data", data, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "PRC_T_ASETE_LIDHJE_ARTIKULL_LLOJAMORT_SEL_SIPASIDART_IDSTANDART_DATE");
            if (ds == null)
                return -1;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return -1;

            int idLlojAmortizimi;
            int.TryParse(ds.Tables[0].Rows[0]["ID_ARTIKULL_LLOJAMORTIZIM"].ToString(), out idLlojAmortizimi);
            return idLlojAmortizimi;

        }

        /// <summary>
        /// MODULI ASETE:
        /// Kthen nga databaza nepermjet procedures PRC_T_ASETE_LIDHJE_ARTIKULL_LLOJAMORT_SEL_SIPASID nje DataRow me normat e amortizimeve per id e kerkuar.
        /// </summary>
        /// <param name="idArtikulli">(int) Id e artikullit qe po i regjistrohet norma.</param>
        /// <param name="idStandarti">(int) Id e standartit qe ka amortizimin.</param>
        /// <returns>Kthen id e llojit te amortizimit per id e kerkuar nga tabela T_ASETE_LIDHJE_ARTIKULL_LLOJAMORT.</returns>
        internal override int ktheIDLlojAmortizimiNormaAmortizimiSipasID(int idArtikullLlojAmort)
        {

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@ID_ARTIKULL_LLOJAMORTIZIM", idArtikullLlojAmort, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "PRC_T_ASETE_LIDHJE_ARTIKULL_LLOJAMORT_SEL_SIPASID");
            if (ds == null)
                return -1;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return -1;

            int idLlojAmortizimi;
            int.TryParse(ds.Tables[0].Rows[0]["IDLLOJAMORTIZIMI"].ToString(), out idLlojAmortizimi);
            return idLlojAmortizimi;

        }

        /// <summary>
        /// MODULI ASETE:
        /// Kthen nga databaza nepermjet procedures PRC_T_ASETE_LIDHJE_ARTIKULL_LLOJAMORT_SEL_SIPASIDART_IDSTANDART nje DataRow me normat e amortizimeve per standartin dhe artikullin e kerkuar.
        /// </summary>
        /// <param name="idArtikulli">(int) Id e artikullit qe po i regjistrohet norma.</param>
        /// <param name="idStandarti">(int) Id e standartit qe ka amortizimin.</param>
        /// <returns>Kthen True nese ekziston njehere specifikimet per normen e artikullit dhe standartit dhe False ne te kundert.</returns>
        internal override bool ekzistonArtikullNormaAmortizimit(int idArtikulli, int idStandarti, DateTime data)
        {
            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@IDARTIKULLI", idArtikulli, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDLLOJSTANDARTI", idStandarti, ParameterDirection.Input);
            dbManager.AddParameters(2, "@data", data, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "PRC_T_ASETE_LIDHJE_ARTIKULL_LLOJAMORT_SEL_SIPASIDART_IDSTANDART");
            if (ds == null)
                return true;
            if (ds.Tables[0].Rows.Count == 0)
                return false;
            return true;
        }
        internal override DataTable ktheArtikujNormaAmortizimiDTExport(int idnderm)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@idndermarje", idnderm, ParameterDirection.Input);

            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "PRC_T_ASETE_LIDHJE_ARTIKULL_LLOJAMORT_SELDTExport");

            return ds.Tables[0];
        }
        

        internal override DataTable ktheArtikujNormaAmortizimiBrendaDatave(int idArtikulli, int idStandarti, DateTime dateFillimi, DateTime datePerfundimi)
        {
            dbManager.Open();
            dbManager.CreateParameters(4);
            dbManager.AddParameters(0, "@IDARTIKULLI", idArtikulli, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDLLOJSTANDARTI", idStandarti, ParameterDirection.Input);
            dbManager.AddParameters(2, "@DATEFILLIMI", dateFillimi, ParameterDirection.Input);
            dbManager.AddParameters(3, "@DATEMBARIMI", datePerfundimi, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "PRC_T_ASETE_LIDHJE_ARTIKULL_LLOJAMORT_SEL_SIPASIDART_IDSTANDART_MIDISDATEVE");
            return ds.Tables[0];
        }
        internal override bool ktheBoolArtikujNormaAmortizimiBrendaDatave(int idArtikulli, int idStandarti, DateTime dateFillimi, DateTime datePerfundimi)
        {
            dbManager.Open();
            dbManager.CreateParameters(4);
            dbManager.AddParameters(0, "@IDARTIKULLI", idArtikulli, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDLLOJSTANDARTI", idStandarti, ParameterDirection.Input);
            dbManager.AddParameters(2, "@DATEFILLIMI", dateFillimi, ParameterDirection.Input);
            dbManager.AddParameters(3, "@DATEMBARIMI", datePerfundimi, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "PRC_T_ASETE_LIDHJE_ARTIKULL_LLOJAMORT_SEL_SIPASIDART_IDSTANDART_MIDISDATEVE");
            return ds.Tables[0].Rows.Count > 1;
        }
        #endregion

        #region AQT Seriale

        /// <summary>
        /// MODULI ASETE:
        /// Ruan ne tabelen T_ASETE_AQTSERIALE objektin e serialit te aqt-se nepermjet procedures PRC_T_ASETE_AQTSERIALE_INSERT.
        /// </summary>
        /// <param name="idAQTSerial">(int) Id automatike e serialit te aqt-se qe ruajme.</param>
        /// <param name="aqtSerialKod">(string) Kodi i serialit te aqt-se qe ruajme.</param>
        /// <param name="aqtSerialPershkrim">(string) Pershkrimi i serialit te aqt-se qe po ruajme.</param>
        /// <param name="idAQTArt">(int) Id e artikullit qe ka kete serial.</param>
        /// <param name="aqtSerialDataHyrje">(DateTime) Data kur eshte bere hyrje ose blerje seriali.</param>
        /// <param name="aqtSerialDataMagAktive">(DateTime) Data kur eshte kaluar ne magazine aktive per here te pare seriali.</param>
        /// <param name="aqtSerialDataAmortizimfillestar">(DateTime) Data kur eshte kryer per here te pare amortizimi.</param>
        /// <param name="meSerialPerCope">(bool) Tregon nese seriali gjenerohet per cdo cope apo per dokument.</param>
        /// <param name="idNjesiAdministrativeAktuale">(int) Id e magazines ku eshte momentalisht seriali.</param>
        /// <param name="idHistorikAktualPaSerial">(int) Id e historik aktual te serialit. Plotesohet vetem kur eshte serial per dokument.</param>
        /// <param name="idStatusDokumenti">(int) Id se ne cfare gjendje eshte seriali i aqt-se, i ruajtur, fshire, apo modifikuar</param>
        /// <param name="idNdermarrje">(int) Id e ndermarrjes per te cilen po behet regjistrimi i serialit te aqt-se.</param>
        /// <param name="idPerdoruesi">(int) Id e perdoruesit te fundit qe ka modifikuar regjistrimin e serialit te aqt-se.</param>
        /// <param name="idKrijuesi">(int) Id e krijuesit per here te pare te serialit te aqt-se.</param>
        /// <param name="amortizimiFillestar"> (float) Amortizimi fillestar</param>
        /// <returns>Kthen clsMesazh me atribut statusi True nese ruajtja kryhet me sukses ose False dhe mesazhin e gabimit ne te kundert.</returns>
        internal clsMesazh ruajAQTSerial(out int idAQTSerial, string aqtSerialKod, string aqtSerialPershkrim, int idAQTArt, DateTime aqtSerialDataHyrje, DateTime aqtSerialDataMagAktive, DateTime aqtSerialDataAmortizimfillestar, bool meSerialPerCope, int idNjesiAdministrativeAktuale, int idHistorikAktualPaSerial, int idStatusDokumenti, int idNdermarrje, int idPerdoruesi, int idKrijuesi)
        {
            idAQTSerial = 0;

            dbManager.Open();
            dbManager.CreateParameters(14);
            dbManager.AddParameters(0, "@ID_AQTSERIAL", idAQTSerial, ParameterDirection.Output);
            dbManager.AddParameters(1, "@AQTSERIALKOD", aqtSerialKod, ParameterDirection.Input);
            dbManager.AddParameters(2, "@AQTSERIALPERSHK", aqtSerialPershkrim, ParameterDirection.Input);
            if (idAQTArt < 1) dbManager.AddParameters(3, "@IDAQTART", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(3, "@IDAQTART", idAQTArt, ParameterDirection.Input);
            if (aqtSerialDataHyrje == DateTime.MinValue) dbManager.AddParameters(4, "@AQTSERIALDATAHYRJE", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(4, "@AQTSERIALDATAHYRJE", aqtSerialDataHyrje, ParameterDirection.Input);
            if (aqtSerialDataMagAktive == DateTime.MinValue) dbManager.AddParameters(5, "@AQTSERIALDATAMAGAKTIVE", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(5, "@AQTSERIALDATAMAGAKTIVE", aqtSerialDataMagAktive, ParameterDirection.Input);
            if (aqtSerialDataAmortizimfillestar == DateTime.MinValue) dbManager.AddParameters(6, "@AQTSERIALDATAAMORTFILLESTAR", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(6, "@AQTSERIALDATAAMORTFILLESTAR", aqtSerialDataAmortizimfillestar, ParameterDirection.Input);
            dbManager.AddParameters(7, "@MESERIALPERCOPE", meSerialPerCope, ParameterDirection.Input);
            if (idNjesiAdministrativeAktuale < 1) dbManager.AddParameters(8, "@IDNJESIADMINISTRATIVEAKTUALE", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(8, "@IDNJESIADMINISTRATIVEAKTUALE", idNjesiAdministrativeAktuale, ParameterDirection.Input);
            if (idHistorikAktualPaSerial < 1) dbManager.AddParameters(9, "@IDHISTORIKAKTUALPASERIAL", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(9, "@IDHISTORIKAKTUALPASERIAL", idHistorikAktualPaSerial, ParameterDirection.Input);
            dbManager.AddParameters(10, "@IDNDERMARJE", idNdermarrje, ParameterDirection.Input);
            dbManager.AddParameters(11, "@IDPERDORUESI", idPerdoruesi, ParameterDirection.Input);
            dbManager.AddParameters(12, "@IDKRIJUESI", idKrijuesi, ParameterDirection.Input);
            dbManager.AddParameters(13, "@IDSTATUSDOK", idStatusDokumenti, ParameterDirection.Input);

            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "PRC_T_ASETE_AQTSERIALE_INSERT");
            idAQTSerial = int.Parse(dbManager.Parameters[0].Value.ToString());
            return new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);

        }

        /// <summary>
        /// MODULI ASETE:
        /// Fshirja e serialit te aqt-ve duke i ndryshuar statuset ne te fshire (2) ne tabelen T_ASETE_AQTSERIALE nepermjet procedures PRC_T_ASETE_AQTSERIALE_UPDATEDELETE.
        /// </summary>
        /// <param name="idAQTSerial">(int) Id automatike e serialit te aqt-se qe ruajme.</param>
        /// <param name="idPerdoruesi">(int) Id e perdoruesit te fundit qe ka modifikuar regjistrimin e serialit te aqt-se.</param>
        /// <returns>Kthen clsMesazh me atribut statusi True nese modifikimi kryhet me sukses ose False dhe mesazhin e gabimit ne te kundert.</returns>
        internal clsMesazh modifikoAQTSerialStatus(int idAQTSerial, int idPerdoruesi)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@ID_AQTSERIAL", idAQTSerial, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUESI", idPerdoruesi, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "PRC_T_ASETE_AQTSERIALE_UPDATEDELETE");
            return new clsMesazh(true, MessagesResource.Messages["msgModifikimiMeSukses"]);
        }

        internal clsMesazh modifikoAQTSerialStatusPaFshirje(int idAQTSerial, int idPerdoruesi, string aqtSerialKod, string aqtSerialPershkrim, int idAQTArt, DateTime aqtSerialDataHyrje, DateTime aqtSerialDataMagAktive, DateTime aqtSerialDataAmortizimfillestar, bool meSerialPerCope, int idNjesiAdministrativeAktuale, int idHistorikAktualPaSerial, int idStatusDokumenti, int idNdermarrje, int idKrijuesi)
        {
            dbManager.Open();
            dbManager.CreateParameters(14);
            dbManager.AddParameters(0, "@ID_AQTSERIAL", idAQTSerial, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUESI", idPerdoruesi, ParameterDirection.Input);
            dbManager.AddParameters(1, "@AQTSERIALKOD", aqtSerialKod, ParameterDirection.Input);
            dbManager.AddParameters(2, "@AQTSERIALPERSHK", aqtSerialPershkrim, ParameterDirection.Input);
            if (idAQTArt < 1) dbManager.AddParameters(3, "@IDAQTART", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(3, "@IDAQTART", idAQTArt, ParameterDirection.Input);
            if (aqtSerialDataHyrje == DateTime.MinValue) dbManager.AddParameters(4, "@AQTSERIALDATAHYRJE", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(4, "@AQTSERIALDATAHYRJE", aqtSerialDataHyrje, ParameterDirection.Input);
            if (aqtSerialDataMagAktive == DateTime.MinValue) dbManager.AddParameters(5, "@AQTSERIALDATAMAGAKTIVE", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(5, "@AQTSERIALDATAMAGAKTIVE", aqtSerialDataMagAktive, ParameterDirection.Input);
            if (aqtSerialDataAmortizimfillestar == DateTime.MinValue) dbManager.AddParameters(6, "@AQTSERIALDATAAMORTFILLESTAR", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(6, "@AQTSERIALDATAAMORTFILLESTAR", aqtSerialDataAmortizimfillestar, ParameterDirection.Input);
            dbManager.AddParameters(7, "@MESERIALPERCOPE", meSerialPerCope, ParameterDirection.Input);
            if (idNjesiAdministrativeAktuale < 1) dbManager.AddParameters(8, "@IDNJESIADMINISTRATIVEAKTUALE", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(8, "@IDNJESIADMINISTRATIVEAKTUALE", idNjesiAdministrativeAktuale, ParameterDirection.Input);
            if (idHistorikAktualPaSerial < 1) dbManager.AddParameters(9, "@IDHISTORIKAKTUALPASERIAL", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(9, "@IDHISTORIKAKTUALPASERIAL", idHistorikAktualPaSerial, ParameterDirection.Input);
            dbManager.AddParameters(10, "@IDNDERMARJE", idNdermarrje, ParameterDirection.Input);
            dbManager.AddParameters(11, "@IDPERDORUESI", idPerdoruesi, ParameterDirection.Input);
            dbManager.AddParameters(12, "@IDKRIJUESI", idKrijuesi, ParameterDirection.Input);
            dbManager.AddParameters(13, "@IDSTATUSDOK", idStatusDokumenti, ParameterDirection.Input);

            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "PRC_T_ASETE_AQTSERIALE_PERDITESO");
            //idAQTSerial = int.Parse(dbManager.Parameters[0].Value.ToString());

            // dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "PRC_T_ASETE_AQTSERIALE_UPDATEDELETE");
            return new clsMesazh(true, MessagesResource.Messages["msgModifikimiMeSukses"]);
        }
        internal clsMesazh modifikoAQTSerialStatusSipasArtikullit(int idartikulli, int idPerdoruesi)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDartikulli", idartikulli, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUESI", idPerdoruesi, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "PRC_T_ASETE_AQTSERIALE_UPDATEDELETESipasArtikullit");
            return new clsMesazh(true, MessagesResource.Messages["msgModifikimiMeSukses"]);
        }

        /// <summary>
        /// MODULI ASETE:
        /// Modifikimi i serialit te aqt-ve ne tabelen T_ASETE_AQTSERIALE nepermjet procedures PRC_T_ASETE_NJESIADMINISTRATIVE_HISTORIKU_UPDATE.
        /// </summary>
        /// <param name="idAQTSerial">(int) Id automatike e serialit te aqt-se qe ruajme.</param>
        /// <param name="aqtSerialKod">(string) Kodi i serialit te aqt-se qe ruajme.</param>
        /// <param name="aqtSerialPershkrim">(string) Pershkrimi i serialit te aqt-se qe po ruajme.</param>
        /// <param name="aqtSerialDataMagAktive">(DateTime) Data kur eshte kaluar ne magazine aktive per here te pare seriali.</param>
        /// <param name="aqtSerialDataAmortizimfillestar">(DateTime) Data kur eshte kryer per here te pare amortizimi.</param>
        /// <param name="idNjesiAdministrativeAktuale">(int) Id e magazines ku eshte momentalisht seriali.</param>
        /// <param name="idHistorikAktualPaSerial">(int) Id e historik aktual te serialit. Plotesohet vetem kur eshte serial per dokument.</param>
        /// <param name="idPerdoruesi">(int) Id e perdoruesit te fundit qe ka modifikuar regjistrimin e serialit te aqt-se.</param>
        /// <returns>Kthen clsMesazh me atribut statusi True nese modifikimi kryhet me sukses ose False dhe mesazhin e gabimit ne te kundert.</returns>
        internal clsMesazh modifikoAQTSerial(int idAQTSerial, string aqtSerialKod, string aqtSerialPershkrim, DateTime aqtSerialDataMagAktive, DateTime aqtSerialDataAmortizimfillestar,
            int idNjesiAdministrativeAktuale, int idHistorikAktualPaSerial, int idPerdoruesi)
        {
            dbManager.Open();
            dbManager.CreateParameters(8);
            dbManager.AddParameters(0, "@ID_AQTSERIAL", idAQTSerial, ParameterDirection.Input);
            dbManager.AddParameters(1, "@AQTSERIALKOD", aqtSerialKod, ParameterDirection.Input);
            dbManager.AddParameters(2, "@AQTSERIALPERSHK", aqtSerialPershkrim, ParameterDirection.Input);
            dbManager.AddParameters(3, "@AQTSERIALDATAMAGAKTIVE", aqtSerialDataMagAktive, ParameterDirection.Input);
            dbManager.AddParameters(4, "@AQTSERIALDATAAMORTFILLESTAR", aqtSerialDataAmortizimfillestar, ParameterDirection.Input);
            dbManager.AddParameters(5, "@IDNJESIADMINISTRATIVEAKTUALE", idNjesiAdministrativeAktuale, ParameterDirection.Input);
            dbManager.AddParameters(6, "@IDHISTORIKAKTUALPASERIAL", idHistorikAktualPaSerial, ParameterDirection.Input);
            dbManager.AddParameters(7, "@IDPERDORUESI", idPerdoruesi, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "PRC_T_ASETE_AQTSERIALE_UPDATE");
            return new clsMesazh(true, MessagesResource.Messages["msgModifikimiMeSukses"]);
        }

        /// <summary>
        /// MODULI ASETE:
        /// Modifikimi i serialit te aqt-ve ne tabelen T_ASETE_AQTSERIALE nepermjet procedures PRC_T_ASETE_NJESIADMINISTRATIVE_HISTORIKU_UPDATE.
        /// </summary>
        /// <param name="idAQTSerial">(int) Id automatike e serialit te aqt-se qe ruajme.</param>
        /// <param name="idHistorikAktualPaSerial">(int) Id e historik aktual te serialit. Plotesohet vetem kur eshte serial per dokument.</param>
        /// <returns>Kthen clsMesazh me atribut statusi True nese modifikimi kryhet me sukses ose False dhe mesazhin e gabimit ne te kundert.</returns>
        internal clsMesazh modifikoAQTSerialHistorik(int idAQTSerial, int idHistorikAktualPaSerial)
        {

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@ID_AQTSERIAL", idAQTSerial, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDHISTORIKAKTUALPASERIAL", idHistorikAktualPaSerial, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "PRC_T_ASETE_AQTSERIALE_UPDATEHISTORIK");
            return new clsMesazh(true, MessagesResource.Messages["msgModifikimiMeSukses"]);

        }


        /// <summary>
        /// MODULI ASETE:
        /// Modifikimi i serialit te aqt-ve ne tabelen T_ASETE_AQTSERIALE nepermjet procedures PRC_T_ASETE_AQTSERIALE_UPDATE_AQTSERIALDATAAMORTFILLESTAR.
        /// </summary>
        /// <param name="idAQTSerial">(int) Id automatike e serialit te aqt-se qe ruajme.</param>
        /// <param name="serialDataAmortizimfillestar">(DateTime) Data e amortizimit kur ka filluar per here te pare.</param>
        /// <returns>Kthen clsMesazh me atribut statusi True nese modifikimi kryhet me sukses ose False dhe mesazhin e gabimit ne te kundert.</returns>
        internal clsMesazh modifikoAQTSerialDataAmortizimfillestar(int idAQTSerial, DateTime serialDataAmortizimfillestar)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@ID_AQTSERIAL", idAQTSerial, ParameterDirection.Input);
            dbManager.AddParameters(1, "@AQTSERIALDATAAMORTFILLESTAR", serialDataAmortizimfillestar, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "PRC_T_ASETE_AQTSERIALE_UPDATE_AQTSERIALDATAAMORTFILLESTAR");
            return new clsMesazh(true, MessagesResource.Messages["msgModifikimiMeSukses"]);
        }

        /// <summary>
        /// MODULI ASETE:
        /// Modifikimi i serialit te aqt-ve ne tabelen T_ASETE_AQTSERIALE nepermjet procedures PRC_T_ASETE_NJESIADMINISTRATIVE_HISTORIKU_UPDATE.
        /// </summary>
        /// <param name="idAQTSerial">(int) Id automatike e serialit te aqt-se qe ruajme.</param>
        /// <param name="aqtDateHyrje">(DateTime) Data e hyrjes ose e blerjes se serialit.</param>
        /// <param name="aqtSerialDataMagAktive">(DateTime) Data kur eshte kaluar ne magazine aktive per here te pare seriali.</param>
        /// <param name="aqtSerialDataAmortizimfillestar">(DateTime) Data kur eshte kryer per here te pare amortizimi.</param>
        /// <param name="idNjesiAdministrativeAktuale">(int) Id e magazines ku eshte momentalisht seriali.</param>
        /// <param name="idHistorikAktualPaSerial">(int) Id e historik aktual te serialit. Plotesohet vetem kur eshte serial per dokument.</param>
        /// <param name="idPerdoruesi">(int) Id e perdoruesit te fundit qe ka modifikuar regjistrimin e serialit te aqt-se.</param>
        /// <param name="idstatusdok">(int) Id e statusit te dokumentit nese eshte i modifikuar, i ruajtuar apo i fshire.</param>
        /// <returns>Kthen clsMesazh me atribut statusi True nese modifikimi kryhet me sukses ose False dhe mesazhin e gabimit ne te kundert.</returns>
        internal clsMesazh modifikoAQTSerialNgaVeprimi(int idAQTSerial, DateTime aqtDateHyrje, DateTime aqtSerialDataMagAktive, DateTime aqtSerialDataAmortizimfillestar,
            int idNjesiAdministrativeAktuale, int idHistorikAktualPaSerial, int idPerdoruesi, int idstatusdok, bool modifikimblerje)
        {
            dbManager.Open();
            dbManager.CreateParameters(9);
            dbManager.AddParameters(0, "@ID_AQTSERIAL", idAQTSerial, ParameterDirection.Input);
            if (aqtDateHyrje == DateTime.MinValue) dbManager.AddParameters(1, "@AQTSERIALDATAHYRJE", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(1, "@AQTSERIALDATAHYRJE", aqtDateHyrje, ParameterDirection.Input);
            if (aqtSerialDataMagAktive == DateTime.MinValue) dbManager.AddParameters(2, "@AQTSERIALDATAMAGAKTIVE", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(2, "@AQTSERIALDATAMAGAKTIVE", aqtSerialDataMagAktive, ParameterDirection.Input);
            if (aqtSerialDataAmortizimfillestar == DateTime.MinValue) dbManager.AddParameters(3, "@AQTSERIALDATAAMORTFILLESTAR", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(3, "@AQTSERIALDATAAMORTFILLESTAR", aqtSerialDataAmortizimfillestar, ParameterDirection.Input);
            if (idNjesiAdministrativeAktuale < 1) dbManager.AddParameters(4, "@IDNJESIADMINISTRATIVEAKTUALE", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(4, "@IDNJESIADMINISTRATIVEAKTUALE", idNjesiAdministrativeAktuale, ParameterDirection.Input);
            if (idHistorikAktualPaSerial <= 0) dbManager.AddParameters(5, "@IDHISTORIKAKTUALPASERIAL", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(5, "@IDHISTORIKAKTUALPASERIAL", idHistorikAktualPaSerial, ParameterDirection.Input);
            dbManager.AddParameters(6, "@IDPERDORUESI", idPerdoruesi, ParameterDirection.Input);
            dbManager.AddParameters(7, "@idstatusdok", idstatusdok, ParameterDirection.Input);
            dbManager.AddParameters(8, "@modifikimblerje", modifikimblerje, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "PRC_T_ASETE_AQTSERIALE_UPDngaveprimi");
            return new clsMesazh(true, MessagesResource.Messages["msgModifikimiMeSukses"]);

        }

        /// <summary>
        /// MODULI ASETE:
        /// Fshin objektin e serialit te aqt-ve ne tabelen T_ASETE_AQTSERIALE nepermjet procedures PRC_T_ASETE_AQTSERIALE_DELETE. 
        /// </summary>
        /// <param name="idAQTSerial">(int) Id automatike e serialit te aqt-se qe ruajme.</param>
        /// <returns>Kthen clsMesazh me atribut statusi True nese fshirja kryhet me sukses ose False dhe mesazhin e gabimit ne te kundert.</returns>
        internal clsMesazh fshiAQTSerial(int idAQTSerial)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@ID_AQTSERIAL", idAQTSerial, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "PRC_T_ASETE_AQTSERIALE_DELETE");
            return new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
        }

        /// <summary>
        /// MODULI ASETE:
        /// Kthen nga databaza nepermjet procedures PRC_T_ASETE_AQTSERIALE_SEL_SIPASIDART nje DataTable me te gjitha serialet e artikullit per ndermarrjen ne perdorim.
        /// </summary>
        /// <param name="idArtikulli">(int) Id e artikullit per te cilen kerkojme serialet e aqt-se.</param>
        /// <param name="idNdermarrje">(int) Id e ndermarrjes ne perdorim per te cilen do te merren serialet e aqt-ve.</param>
        /// <returns>Kthen nje DataTable me te gjitha serialet e artikullit nga tabela T_ASETE_AQTSERIALE.</returns>
        internal DataTable ktheAQTSerialSipasIDArtikulli(int idArtikulli, int idNdermarrje)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDARTIKULLI", idArtikulli, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idNdermarrje, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "PRC_T_ASETE_AQTSERIALE_SEL_SIPASIDART");
            return ds.Tables[0];
        }

        /// <summary>
        /// MODULI ASETE:
        /// Kthen nga databaza nepermjet procedures PRC_T_ASETE_AQTSERIALE_SEL_SIPASIDART nje DataTable me te gjitha serialet e artikullit per ndermarrjen ne perdorim.
        /// </summary>
        /// <param name="idArtikulli">(int) Id e artikullit per te cilen kerkojme serialet e aqt-se.</param>
        /// <param name="idNdermarrje">(int) Id e ndermarrjes ne perdorim per te cilen do te merren serialet e aqt-ve.</param>
        /// <returns>Kthen nje DataTable me te gjitha serialet e artikullit nga tabela T_ASETE_AQTSERIALE.</returns>
        internal DataTable ktheAQTSerialSipasIDArtikulliDt(int idArtikulli, int idNdermarrje)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDARTIKULLI", idArtikulli, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idNdermarrje, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "PRC_T_ASETE_AQTSERIALE_SEL_SIPASIDARTDt");
            return ds.Tables[0];
        }

        /// <summary>
        /// MODULI ASETE:
        /// Kthen nga databaza nepermjet procedures PRC_T_ASETE_AQTSERIALE_SEL_SIPASIDART nje DataTable me te gjitha serialet e artikullit per ndermarrjen ne perdorim per artikujt me serial.
        /// </summary>
        /// <param name="idArtikulli">(int) Id e artikullit per te cilen kerkojme serialet e aqt-se.</param>
        /// <param name="idNdermarrje">(int) Id e ndermarrjes ne perdorim per te cilen do te merren serialet e aqt-ve.</param>
        /// <returns>Kthen nje DataTable me te gjitha serialet e artikullit nga tabela T_ASETE_AQTSERIALE.</returns>
        internal DataTable ktheAQTSerialSipasIDArtikulliPerArtikujMeSerial(int idArtikulli, int idNdermarrje)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDARTIKULLI", idArtikulli, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idNdermarrje, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "PRC_T_ASETE_AQTSERIALE_SEL_SIPASIDART_ARTIKUJMESERIAL");
            return ds.Tables[0];
        }

        /// <summary>
        /// MODULI ASETE:
        /// Kthen nga databaza nepermjet procedures PRC_T_ASETE_AQTSERIALE_SEL_SIPASIDART nje DataTable me te gjitha serialet e artikullit per ndermarrjen ne perdorim per artikujt pa serial dhe qe jane prind fillestar.
        /// </summary>
        /// <param name="idArtikulli">(int) Id e artikullit per te cilen kerkojme serialet e aqt-se.</param>
        /// <param name="idNdermarrje">(int) Id e ndermarrjes ne perdorim per te cilen do te merren serialet e aqt-ve.</param>
        /// <returns>Kthen nje DataTable me te gjitha serialet e artikullit nga tabela T_ASETE_AQTSERIALE.</returns>
        internal DataTable ktheAQTSerialSipasIDArtikulliPerArtikujPaSerial_VetemPrind(int idArtikulli, int idNdermarrje)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDARTIKULLI", idArtikulli, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idNdermarrje, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "PRC_T_ASETE_AQTSERIALE_SEL_SIPASIDART_ARTIKUJPASERIAL_PRIND");
            return ds.Tables[0];
        }

        /// <summary>
        /// MODULI ASETE:
        /// Kthen nga databaza nepermjet procedures PRC_T_ASETE_AQTSERIALE_SEL_SIPASIDNdermarje nje DataTable me te gjitha serialet per ndermarrjen ne perdorim.
        /// </summary>
        /// <param name="idNdermarrje">(int) Id e ndermarrjes ne perdorim per te cilen do te merren serialet e aqt-ve.</param>
        /// <returns>Kthen nje DataTable me te gjitha serialet nga tabela T_ASETE_AQTSERIALE.</returns>
        internal DataTable ktheAQTSerialSipasIDNdermarje(int idNdermarrje)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDNDERMARJE", idNdermarrje, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "PRC_T_ASETE_AQTSERIALE_SEL_SIPASIDNdermarje");
            return ds.Tables[0];
        }

        /// <summary>
        /// MODULI ASETE:
        /// Kthen nga databaza nepermjet procedures PRC_T_ASETE_AQTSERIALE_SEL_SIPASIDNdermarje nje DataTable me te gjitha serialet per ndermarrjen ne perdorim.
        /// </summary>
        /// <param name="idNdermarrje">(int) Id e ndermarrjes ne perdorim per te cilen do te merren serialet e aqt-ve.</param>
        /// <returns>Kthen nje DataTable me te gjitha serialet nga tabela T_ASETE_AQTSERIALE.</returns>
        internal DataTable ktheAQTSerialSipasIDNdermarjeDt(int idNdermarrje)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDNDERMARJE", idNdermarrje, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "PRC_T_ASETE_AQTSERIALE_SEL_SIPASIDNdermarjeDt");
            return ds.Tables[0];
        }

        /// <summary>
        /// MODULI ASETE:
        /// Serialet e paperdorura ne veprime amortizimi tek ketij artikulli
        /// </summary>
        /// <param name="idArtikulli">(int) Id e artikullit per te cilen kerkojme serialet e aqt-se.</param>
        /// <param name="idNdermarrje">(int) Id e ndermarrjes ne perdorim per te cilen do te merren serialet e aqt-ve.</param>
        /// <returns>Kthen nje DataTable me te gjitha serialet e paperdorura te artikullit nga tabela T_ASETE_AQTSERIALE.</returns>
        internal DataTable ktheAQTSerialSipasIDArtikulliTePaperdorura(int idArtikulli, int idNdermarrje)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDARTIKULLI", idArtikulli, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idNdermarrje, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "PRC_T_ASETE_AQTSERIALE_SEL_SIPASIDARTTePaperdorura");
            return ds.Tables[0];
        }

        /// <summary>
        /// MODULI ASETE:
        /// Serialet e paperdorura ne veprime amortizimi tek ketij artikulli
        /// </summary>
        /// <param name="idArtikulli">(int) Id e artikullit per te cilen kerkojme serialet e aqt-se.</param>
        /// <param name="idNdermarrje">(int) Id e ndermarrjes ne perdorim per te cilen do te merren serialet e aqt-ve.</param>
        /// <returns>Kthen nje DataTable me te gjitha serialet e paperdorura te artikullit nga tabela T_ASETE_AQTSERIALE.</returns>
        internal DataRow ktheAQTSerialSipasIdDr(int idAqtSerial)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@ID_AQTSERIAL", idAqtSerial, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "PRC_T_ASETE_AQTSERIALE_SEL_SIPASIDAQTSERIALdr");
            return ds.Tables[0].Rows[0];
        }

        /// <summary>
        /// MODULI ASETE:
        /// Serialet e paperdorura ne veprime amortizimi tek ketij artikulli ne kete magazine.
        /// </summary>
        /// <param name="idArtikulli">(int) Id e artikullit per te cilen kerkojme serialet e aqt-se.</param>
        /// <param name="idNdermarrje">(int) Id e ndermarrjes ne perdorim per te cilen do te merren serialet e aqt-ve.</param>
        /// <param name="idmag">(int) Id e magazines per te cilen kerkojme serialet qe ndodhen ne te.</param>
        /// <returns>Kthen nje DataTable me te gjitha serialet e artikullit ne magazine nga tabela T_ASETE_AQTSERIALE.</returns>
        internal DataTable ktheAQTSerialSipasIDArtikulliDheMagazineTeperdorura(int idArtikulli, int idNdermarrje, int idmag, DateTime data)
        {
            dbManager.Open();
            dbManager.CreateParameters(4);
            dbManager.AddParameters(0, "@IDARTIKULLI", idArtikulli, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idNdermarrje, ParameterDirection.Input);
            dbManager.AddParameters(2, "@idmag", idmag, ParameterDirection.Input);
            dbManager.AddParameters(3, "@data", data, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "PRC_T_ASETE_AQTSERIALE_SEL_SIPASIDARTDheMagazineTeperdorura");
            return ds.Tables[0];

        }

        internal DataTable KtheAqtSerialSipasIdArtikullDheMagazineTePerdoruraDraft(int idArtikulli, int idNdermarrje, int idmag)
        {
            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddInputParameters("@IDARTIKULLI", idArtikulli);
            dbManager.AddInputParameters("@IDNDERMARJE", idNdermarrje);
            dbManager.AddInputParameters("@IDMAG", idmag);
            return dbManager.ExecuteDataSet(CommandType.StoredProcedure, "PRC_T_ASETE_AQTSERIALE_KtheAqtSerialSipasIdArtikullDheMagazineTePerdoruraDraft").Tables[0];
        }

        internal DataTable ktheAQTSerialSipasIDArtikulliTeperdoruraTegjitha(int idArtikulli, int idNdermarrje)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDARTIKULLI", idArtikulli, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idNdermarrje, ParameterDirection.Input);

            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "PRC_T_ASETE_AQTSERIALE_SEL_SIPASIDARTTeperdoruraTeGjitha");
            return ds.Tables[0];
        }

        /// <summary>
        /// MODULI ASETE:
        /// Kthen nga databaza nepermjet procedures PRC_T_ASETE_AQTSERIALE_SEL_SIPASIDART_IDMAG nje DataTable me te gjitha serialet ne magazine te artikullit per ndermarrjen ne perdorim.
        /// </summary>
        /// <param name="idArtikulli">(int) Id e artikullit per te cilen kerkojme serialet e aqt-se.</param>
        /// <param name="idNjesiAdministrative">(int) Id e magazines per te cilen kerkojme serialet qe ndodhen ne te.</param>
        /// <param name="idNdermarrje">(int) Id e ndermarrjes ne perdorim per te cilen do te merren serialet e aqt-ve.</param>
        /// <returns>Kthen nje DataTable me te gjitha serialet e artikullit ne magazine nga tabela T_ASETE_AQTSERIALE.</returns>
        internal DataTable ktheAQTSerialSipasIDArtikulliIDMagazine(int idArtikulli, int idNjesiAdministrative, int idNdermarrje, DateTime data)
        {
            dbManager.Open();
            dbManager.CreateParameters(4);
            dbManager.AddParameters(0, "@IDARTIKULLI", idArtikulli, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNJESIADMINISTRATIVEAKTUALE", idNjesiAdministrative, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDNDERMARJE", idNdermarrje, ParameterDirection.Input);
            dbManager.AddParameters(3, "@data", data, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "PRC_T_ASETE_AQTSERIALE_SEL_SIPASIDART_IDMAG");
            return ds.Tables[0];
        }

        /// <summary>
        /// MODULI ASETE:
        /// Kthen nga databaza nepermjet procedures PRC_T_ASETE_AQTSERIALE_SEL_SIPASIDART_IDMAG_TEPAHYRE nje DataTable me te gjitha serialet ne magazine te artikullit per ndermarrjen ne perdorim qe nuk jane perdorur akoma ne blerje.
        /// </summary>
        /// <param name="idArtikulli">(int) Id e artikullit per te cilen kerkojme serialet e aqt-se.</param>
        /// <param name="idNdermarrje">(int) Id e ndermarrjes ne perdorim per te cilen do te merren serialet e aqt-ve.</param>
        /// <returns>Kthen nje DataTable me te gjitha serialet e artikullit ne magazine qe nuk jane perdorur akoma ne blerje nga tabela T_ASETE_AQTSERIALE.</returns>
        internal DataTable ktheAQTSerialSipasTePaHyre(int idArtikulli, int idNdermarrje)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDARTIKULLI", idArtikulli, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idNdermarrje, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "PRC_T_ASETE_AQTSERIALE_SEL_SIPASIDART_IDMAG_TEPAHYRE");
            return ds.Tables[0];
        }

        /// <summary>
        /// MODULI ASETE:
        /// Kthen nga databaza nepermjet procedures PRC_T_ASETE_AQTSERIALE_SEL_SIPASIDMAG nje DataTable me te gjitha serialet ne magazine per ndermarrjen ne perdorim.
        /// </summary>
        /// <param name="idNjesiAdministrative">(int) Id e magazines per te cilen kerkojme serialet qe ndodhen ne te.</param>
        /// <param name="idNdermarrje">(int) Id e ndermarrjes ne perdorim per te cilen do te merren serialet e aqt-ve.</param>
        /// <returns>Kthen nje DataTable me te gjitha serialet qe ndodhen ne magazine nga tabela T_ASETE_AQTSERIALE.</returns>
        internal DataTable ktheAQTSerialSipasIDMagazine(int idNjesiAdministrative, int idNdermarrje, DateTime data)
        {
            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@IDNJESIADMINISTRATIVEAKTUALE", idNjesiAdministrative, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idNdermarrje, ParameterDirection.Input);
            dbManager.AddParameters(2, "@data", data, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "PRC_T_ASETE_AQTSERIALE_SEL_SIPASIDMAG");
            return ds.Tables[0];
        }

        /// <summary>
        /// Kthen aqt serialet sipas nje dokumenti te caktuar dhe rreshtit te tij nepermjet procedures PRC_T_ASETE_AQTSERIALE_SEL_SipasDokumentiTeSerialeMagazine.
        /// </summary>
        /// <param name="iddok">(int) Id e dokumentit.</param>
        /// <param name="idkonfigambjente">(int) Id e konfigurimit te ambjentit.</param>
        /// <param name="nrreshti">(int) Numri i rreshtit te kerkuar.</param>
        /// <returns>Kthen nje DataTable me te gjitha serialet qe ndodhen ne dokumetin dhe rreshtin e tij nga tabela T_ASETE_AQTSERIALE.</returns>
        internal DataTable ktheAQTSerialSipasIdDokumentiDheNrRreshtiNgaSerialeMagazine(int iddok, int idkonfigambjente, int nrreshti)
        {
            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@IDDok", iddok, ParameterDirection.Input);
            dbManager.AddParameters(1, "@Idkonfigambjente", idkonfigambjente, ParameterDirection.Input);
            dbManager.AddParameters(2, "@nrreshti", nrreshti, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "PRC_T_ASETE_AQTSERIALE_SEL_SipasDokumentiTeSerialeMagazine");
            return ds.Tables[0];
        }

        /// <summary>
        /// MODULI ASETE:
        /// Kthen nga databaza nepermjet procedures PRC_T_ASETE_AQTSERIALE_SEL_SIPASIDAQTSERIAL nje DataRow me serialin e aqt-se.
        /// </summary>
        /// <param name="idAQTSerial">(int) Id automatike e serialit te aqt-se qe kerkojme.</param>
        /// <returns>Kthen nje DataRow me serialin e aqt-se nga tabela T_ASETE_AQTSERIALE.</returns>
        internal DataRow ktheAQTSerialSipasID(int idAQTSerial)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@ID_AQTSERIAL", idAQTSerial, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "PRC_T_ASETE_AQTSERIALE_SEL_SIPASIDAQTSERIAL");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];
        }

        /// <summary>
        /// MODULI ASETE:
        /// Kthen nga databaza nepermjet procedures PRC_T_ASETE_AQTSERIALE_SEL_SIPASSERIALKOD nje DataRow me serialin e aqt-se ne ndermarrjen ne perdorim.
        /// </summary>
        /// <param name="aqtKod">(string) Kodi i serialit te aqt-se qe kerkojme.</param>
        /// <param name="idNdermarrja">(int) Id e ndermarrjes ne perdorim per te cilen do te merren serialet e aqt-ve.</param>
        /// <returns>Kthen nje DataRow me serialin e aqt-se nga tabela T_ASETE_AQTSERIALE.</returns>
        internal DataRow ktheAQTSerialSipasKodAQT(string aqtKod, int idNdermarrja)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@AQTSERIALKOD", aqtKod, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idNdermarrja, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "PRC_T_ASETE_AQTSERIALE_SEL_SIPASSERIALKOD");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];
        }

        /// <summary>
        /// MODULI ASETE:
        /// Kthen nga databaza nepermjet procedures PRC_T_ASETE_AQTSERIALE_SEL_SIPASSERIALKOD nje DataRow me serialin e aqt-se ne ndermarrjen ne perdorim.
        /// </summary>
        /// <param name="aqtKod">(string) Kodi i serialit te aqt-se qe kerkojme.</param>
        /// <param name="idNdermarrja">(int) Id e ndermarrjes ne perdorim per te cilen do te merren serialet e aqt-ve.</param>
        /// <returns>Kthen nje int me id e serialit te aqt-se nga tabela T_ASETE_AQTSERIALE.</returns>
        internal int ktheIDAQTSerialSipasKodAQT(string aqtKod, int idNdermarrja)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@AQTSERIALKOD", aqtKod, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idNdermarrja, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "PRC_T_ASETE_AQTSERIALE_SEL_SIPASSERIALKOD");
            if (ds == null)
                return -1;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return -1;

            int idAQTSerial;
            int.TryParse(ds.Tables[0].Rows[0]["ID_AQTSERIAL"].ToString(), out idAQTSerial);
            return idAQTSerial;
        }

        /// <summary>
        /// MODULI ASETE:
        /// Kthen nga databaza nepermjet procedures PRC_T_ASETE_AQTSERIALE_SEL_SIPASIDAQTSERIAL nje DataRow me serialin e aqt-se.
        /// </summary>
        /// <param name="idAQTSerial">(int) Id automatike e serialit te aqt-se qe kerkojme.</param>
        /// <returns>Kthen id e artikullit qe eshte pjese perberese seriali nga tabela T_ASETE_AQTSERIALE.</returns>
        internal int ktheIDArtikullAQTSerialSipasID(int idAQTSerial)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@ID_AQTSERIAL", idAQTSerial, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "PRC_T_ASETE_AQTSERIALE_SEL_SIPASIDAQTSERIAL");
            if (ds == null)
                return -1;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return -1;

            int idArtikulli;
            int.TryParse(ds.Tables[0].Rows[0]["IDAQTART"].ToString(), out idArtikulli);
            return idArtikulli;
        }

        /// <summary>
        /// MODULI ASETE:
        /// Kthen nga databaza nepermjet procedures PRC_T_ASETE_AQTSERIALE_SEL_SIPASSERIALKOD nje DataRow me serialin e aqt-se ne ndermarrjen ne perdorim per te pare ne ekziston apo jo kodi i serialit.
        /// </summary>
        /// <param name="aqtKod">(string) Kodi i serialit te aqt-se qe kerkojme.</param>
        /// <param name="idNdermarrja">(int) Id e ndermarrjes ne perdorim per te cilen do te merren serialet e aqt-ve.</param>
        /// <returns>Kthen True nese ekziston njehere kodi i serialit per ndermarrjen dhe False ne te kundert.</returns>
        internal bool ekzistonAQTSerial(string aqtKod, int idNdermarrja)
        {

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@AQTSERIALKOD", aqtKod, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idNdermarrja, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "PRC_T_ASETE_AQTSERIALE_SEL_SIPASSERIALKOD");
            if (ds == null)
                return true;
            if (ds.Tables[0].Rows.Count == 0)
                return false;
            return true;

        }

        /// <summary>
        /// MODULI ASETE:
        /// Kthen nga databaza nepermjet procedures PRC_T_ASETE_AQTSERIALE_kaVeprime nje DataRow me id e serialit te aqt-se ne ndermarrjen ne perdorim per te pare ne ekziston apo jo kodi i serialit.
        /// </summary>
        /// <param name="idserial">(int) Id automatike e serialit te aqt-se qe kerkojme.</param>
        /// <param name="idNdermarrja">(int) Id e ndermarrjes ne perdorim per te cilen do te merren serialet e aqt-ve.</param>
        /// <returns>Kthen True nese ekziston njehere id e serialit per ndermarrjen dhe False ne te kundert.</returns>
        internal bool kaveprimeAQTSerial(int idserial, int idNdermarrja)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDSERIALI", idserial, ParameterDirection.Input);
            dbManager.AddParameters(1, "@idndermarje", idNdermarrja, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "PRC_T_ASETE_AQTSERIALE_kaVeprime");
            if (ds == null)
                return true;
            if (ds.Tables[0].Rows.Count == 0)
                return false;
            return true;
        }

        #endregion

        #region Amortizimi fillestar

        /// <summary>
        /// MODULI ASETE:
        /// Ruan ne tabelen T_ASETE_AQTSERIALE objektin e serialit te aqt-se nepermjet procedures PRC_T_ASETE_AQTSERIALE_INSERT.
        /// </summary>
        /// <param name="id">(int) Id automatike e serialit te aqt-se qe ruajme.</param>
        /// <param name="idSerial">(string) Kodi i serialit te aqt-se qe ruajme.</param>
        /// <param name="idStandarti">(string) Pershkrimi i serialit te aqt-se qe po ruajme.</param>
        /// <param name="iddokmag">(int) Id e dokumentit te magazinen ku eshte ruajtur amortizimi fillestar.</param>
        /// <param name="amortizimiFillestar"> (float) Amortizimi fillestar</param>
        /// <returns>Kthen clsMesazh me atribut statusi True nese ruajtja kryhet me sukses ose False dhe mesazhin e gabimit ne te kundert.</returns>
        internal clsMesazh ruajAmortizimFillestar(out int id, int idSerial, int idStandarti, int iddokmag, double amortizimiFillestar)
        {
            id = -1;

            dbManager.Open();
            dbManager.CreateParameters(5);
            dbManager.AddParameters(0, "@ID", id, ParameterDirection.Output);
            dbManager.AddParameters(1, "@IDSERIAL", idSerial, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDSTANDARTI", idStandarti, ParameterDirection.Input);
            dbManager.AddParameters(3, "@IDDOKNGA", iddokmag, ParameterDirection.Input);
            dbManager.AddParameters(4, "@AMORTIZIMIFILLESTAR", amortizimiFillestar, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "PRC_T_ASETE_AMORT_FILLESTAR_ins");
            id = int.Parse(dbManager.Parameters[0].Value.ToString());
            return new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
        }

        /// <summary>
        /// MODULI ASETE:
        /// Fshin objektin e serialit te aqt-ve ne tabelen T_ASETE_AQTSERIALE nepermjet procedures PRC_T_ASETE_AQTSERIALE_DELETE. 
        /// </summary>
        /// <param name="id">(int) Id automatike e serialit te aqt-se qe ruajme.</param>
        /// <returns>Kthen clsMesazh me atribut statusi True nese fshirja kryhet me sukses ose False dhe mesazhin e gabimit ne te kundert.</returns>
        internal clsMesazh fshiAmortizimFillestar(int id)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@ID", id, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "PRC_T_ASETE_AMORT_FILLESTAR_del");
            return new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
        }
        internal clsMesazh fshiAmortizimFillestarSipasIdSeriali(int idSerial)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDSERIAL", idSerial, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "PRC_T_ASETE_AMORT_FILLESTAR_delSipasIdSeriali");
            return new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
        }
        internal clsMesazh fshiAmortizimFillestarSipasIdSerialidheIdStandarti(int idSerial, int idStandarti, int idDokNga)
        {

            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@IDSERIAL", idSerial, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDSTANDARTI", idStandarti, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDDOKNGA", idDokNga, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "PRC_T_ASETE_AMORT_FILLESTAR_delSipasIdSerialiAndIdStandarti");
            return new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);

        }

        /// <summary>
        /// MODULI ASETE:
        /// Kthen nga databaza nepermjet procedures PRC_T_ASETE_AQTSERIALE_SEL_SIPASIDART nje DataTable me te gjitha serialet e artikullit per ndermarrjen ne perdorim.
        /// </summary>
        /// <param name="idSerial">(int) Id e artikullit per te cilen kerkojme serialet e aqt-se.</param>
        /// <param name="idStandart">(int) Id e ndermarrjes ne perdorim per te cilen do te merren serialet e aqt-ve.</param>
        /// <returns>Kthen nje DataTable me te gjitha serialet e artikullit nga tabela T_ASETE_AQTSERIALE.</returns>
        internal DataRow ktheAmortizimFillestarSipasIdSerialDheIdStandarti(int idSerial, int idStandart, int idDokNga)
        {

            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@IDSERIAL", idSerial, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDSTANDARTI", idStandart, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDDOKNGA", idDokNga, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "PRC_T_ASETE_AQTSERIALE_selSipasIdStandartiDheIdSeriali");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];

        }
        internal DataTable ktheAmortizimFillestarSipasIdSerial(int idSerial)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDSERIAL", idSerial, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "PRC_T_ASETE_AQTSERIALE_selSipasIdSeriali");
            return ds.Tables[0];
        }
        internal DataTable ktheAmortizimFillestarSipasIdStandarti(int idStandart)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);

            dbManager.AddParameters(0, "@IDSTANDARTI", idStandart, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "PRC_T_ASETE_AQTSERIALE_selSipasIdStandarti");
            return ds.Tables[0];
        }




        #endregion

        #region Dokument magazine Serial

        /// <summary>
        /// MODULI ASETE:
        /// Ruan ne tabelen T_ASETE_LIDHJE_MAGAZINE_SERIAL objektin e lidhjes mes serialit dhe dokumentit te magazines nepermjet procedures PRC_T_ASETE_LIDHJE_MAGAZINE_SERIAL_INSERT.
        /// </summary>
        /// <param name="idSerialTrupi">(int) Id automatike e lidhjes se serialit me dokumentin e magazines.</param>
        /// <param name="idDok">(int) Id e dokumentit te magazines</param>
        /// <param name="nrRendor">(int) Id e rreshtit qe ndodhet seriali.</param>
        /// <param name="idArtikulli">(int) Id e artikullit qe po regjistrohet.</param>
        /// <param name="idAQTSeriali">(int) Id e serialit te aqt-se qe po regjistrohet.</param>
        /// <param name="idNiveli">(int) Id e lloji kryesor te dokumentit.</param>
        /// <param name="idKonfigAmbjenti">(int) Id e nenllojit te dokumentit.</param>
        /// <param name="idNjesiAdministrative">(int) Id e magazines qe ka prekur dokumenti.</param>
        /// <param name="sasia">(float) Sasia e prekur nga dokumenti i magazines.</param>
        /// <param name="cmimi">(float) Cmimi i prekur nga dokumenti i magazines.</param>
        /// <param name="vlefta">(float) Vlefta e prekur nga dokumentii magazines</param>
        /// <param name="idStatusDokumenti">(int) Id se ne cfare gjendje eshte lidhja mes dokumentit dhe serialit, i ruajtur, fshire, apo modifikuar</param>
        /// <param name="idNdermarrje">(int) Id e ndermarrjes per te cilen po behet regjistrimi i lidhjes.</param>
        /// <param name="idPerdoruesi">(int) Id e perdoruesit te fundit qe ka modifikuar lidhjen.</param>
        /// <param name="idKrijuesi">(int) Id e krijuesit per here te pare te lidhjes.</param>
        /// <param name="dtModifikimi">(DateTime) Data e modifikimit te lidhjes.</param>
        /// <returns>Kthen clsMesazh me atribut statusi True nese modifikimi kryhet me sukses ose False dhe mesazhin e gabimit ne te kundert.</returns>
        internal clsMesazh ruajSerialetMagazine(out int idSerialTrupi, int idDok, int nrRendor, int idArtikulli, int idAQTSeriali, int idNiveli, int idKonfigAmbjenti, int idNjesiAdministrative, double sasia,
            double cmimi, double vlefta, int idStatusDokumenti, int idNdermarrje, int idPerdoruesi, int idKrijuesi, DateTime dtModifikimi)
        {
            idSerialTrupi = 0;

            dbManager.Open();
            dbManager.CreateParameters(16);
            dbManager.AddParameters(0, "@ID_SERIALTRUPI", idSerialTrupi, ParameterDirection.Output);
            dbManager.AddParameters(1, "@IDDOK", idDok, ParameterDirection.Input);
            dbManager.AddParameters(2, "@NRRENDDOK", nrRendor, ParameterDirection.Input);
            dbManager.AddParameters(3, "@IDARTIKULLI", idArtikulli, ParameterDirection.Input);
            dbManager.AddParameters(4, "@IDSERIALI", idAQTSeriali, ParameterDirection.Input);
            dbManager.AddParameters(5, "@IDNIVELI", idNiveli, ParameterDirection.Input);
            dbManager.AddParameters(6, "@IDKONFIGAMBJENTI", idKonfigAmbjenti, ParameterDirection.Input);
            dbManager.AddParameters(7, "@IDNJESIADMINISTRATIVE", idNjesiAdministrative, ParameterDirection.Input);
            dbManager.AddParameters(8, "@SASIA", sasia, ParameterDirection.Input);
            dbManager.AddParameters(9, "@CMIMI", cmimi, ParameterDirection.Input);
            dbManager.AddParameters(10, "@VLEFTA", vlefta, ParameterDirection.Input);
            dbManager.AddParameters(11, "@IDSTATUSDOK", idStatusDokumenti, ParameterDirection.Input);
            dbManager.AddParameters(12, "@IDNDERMARJE", idNdermarrje, ParameterDirection.Input);
            dbManager.AddParameters(13, "@IDPERDORUESI", idPerdoruesi, ParameterDirection.Input);
            dbManager.AddParameters(14, "@IDKRIJUESI", idKrijuesi, ParameterDirection.Input);
            if (dtModifikimi == DateTime.MinValue) dbManager.AddParameters(15, "@DTMODIFIKIMI", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(15, "@DTMODIFIKIMI", dtModifikimi, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "PRC_T_ASETE_LIDHJE_MAGAZINE_SERIAL_INSERT");
            if (ds.Tables.Count > 0)
            {
                return new clsMesazh(false, "Ndodhi nje gabim gjate ruajtjes!");
            }
            else
            {
                idSerialTrupi = int.Parse(dbManager.Parameters[0].Value.ToString());
                return new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
            }
        }
        internal clsMesazh RuajSerialeLidhjeMag(DataTable SerialeIns, bool modifikim)
        {
            this.dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@SERIALE", SerialeIns, ParameterDirection.Input);
            dbManager.AddParameters(1, "@modifikim", modifikim, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure,"PRC_T_ASETE_LIDHJE_MAGAZINE_SERIAL_INSERT_Dt");
            return new clsMesazh(true, MessagesResource.Messages["msgModifikimiMeSukses"]);
        }
        /// <summary>
        /// MODULI ASETE:
        /// Fshirja e lidhjes se dokumentit te magazines me serialin duke i ndryshuar statuset ne te fshire (2), ne tabelen T_ASETE_LIDHJE_MAGAZINE_SERIAL nepermjet procedures PRC_T_ASETE_LIDHJE_MAGAZINE_SERIAL_UPDATEDELETE.
        /// </summary>
        /// <param name="idSerialTrupi">(int) Id automatike e lidhjes se serialit me dokumentin e magazines.</param>
        /// <param name="idPerdoruesi">(int) Id e perdoruesit te fundit qe ka modifikuar lidhjen.</param>
        /// <returns>Kthen clsMesazh me atribut statusi True nese modifikimi kryhet me sukses ose False dhe mesazhin e gabimit ne te kundert.</returns>
        internal clsMesazh modifikoSerialetMagazineStatus(int idSerialTrupi, int idPerdoruesi)
        {

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@ID_SERIALTRUPI", idSerialTrupi, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUESI", idPerdoruesi, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "PRC_T_ASETE_LIDHJE_MAGAZINE_SERIAL_UPDATEDELETE");
            return new clsMesazh(true, MessagesResource.Messages["msgModifikimiMeSukses"]);

        }

        /// <summary>
        /// MODULI ASETE:
        /// Fshin objektin e konfigurimit te standartit ne tabelen T_ASETE_LIDHJE_MAGAZINE_SERIAL nepermjet procedures PRC_T_ASETE_LIDHJE_MAGAZINE_SERIAL_DELETE. 
        /// </summary>
        /// <param name="idDok">(int) Id e dokumentit te magazines</param>
        /// <returns>Kthen clsMesazh me atribut statusi True nese fshirja kryhet me sukses ose False dhe mesazhin e gabimit ne te kundert.</returns>
        internal clsMesazh fshiSerialetMagazine(int idDok)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDDOK", idDok, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "PRC_T_ASETE_LIDHJE_MAGAZINE_SERIAL_DELETE");
            return new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
        }

        /// <summary>
        /// MODULI ASETE:
        /// Kthen nga databaza nepermjet procedures PRC_T_ASETE_LIDHJE_MAGAZINE_SERIAL_SEL_SIPASIDSERIALI nje DataTable me te gjithe dokumentat ku eshte prekur seriali qe po kerkojme per ndermarrjen ne perdorim.
        /// </summary>
        /// <param name="idSerialAQT">(int) Id e serialit aqt qe po kerkojme.</param>
        /// <param name="idNdermarrje">(int) Id e ndermarrjes ne perdorim per te cilen do te merret serialet e dokumentit te magazines.</param>
        /// <returns>Kthen nje DataTable me te gjithe dokumentat ku eshte prekur seriali qe po kerkojme per ndermarrjen ne perdorim nga tabela T_ASETE_LIDHJE_MAGAZINE_SERIAL.</returns>
        internal DataTable ktheSerialetMagazineSipasIDSeriali(int idSerialAQT, int idNdermarrje)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDSERIALI", idSerialAQT, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idNdermarrje, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "PRC_T_ASETE_LIDHJE_MAGAZINE_SERIAL_SEL_SIPASIDSERIALI");
            return ds.Tables[0];
        }
        internal bool ktheSerialetMagazineKaVeprimePas(int idSerialAQT, int idNdermarrje, int nrrendor, DateTime data)
        {
            dbManager.Open();
            dbManager.CreateParameters(4);
            dbManager.AddParameters(0, "@IDSERIALI", idSerialAQT, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idNdermarrje, ParameterDirection.Input);
            dbManager.AddParameters(2, "@NRRENDOR", nrrendor, ParameterDirection.Input);
            dbManager.AddParameters(3, "@Data", data, ParameterDirection.Input); DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "PRC_T_ASETE_LIDHJE_MAGAZINE_SERIAL_KaVeprimePas");
            if (ds.Tables[0].Rows.Count > 0)
                return true;
            else
                return false;
        }
        internal bool ekzistonBlerjePerKeteSerial(int idSerialAQT, int idNdermarrje)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDSERIALI", idSerialAQT, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idNdermarrje, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "PRC_T_ASETE_LIDHJE_MAGAZINE_SERIAL_ekzistonBlerjePerKeteSerial");
            if (ds.Tables[0].Rows.Count > 0)
                return true;
            else
                return false;
        }

        /// <summary>
        /// MODULI ASETE:
        /// Kthen nga databaza nepermjet procedures PRC_T_ASETE_LIDHJE_MAGAZINE_SERIAL_SEL_SIPASIDIDDOK nje DataTable me te gjithe serialet e dokumentit qe po kerkojme per ndermarrjen ne perdorim.
        /// </summary>
        /// <param name="idDok">(int) Id e dokumentit te magazines qe ka perdorur serialin qe po kerkojme.</param>
        /// <param name="idNdermarrje">(int) Id e ndermarrjes ne perdorim per te cilen do te merret serialet e dokumentit te magazines.</param>
        /// <param name="idkonfigambjente">(int) Id e konfigurimit te ambjentit ne asete.</param>
        /// <returns>Kthen nje DataTable me te gjithe serialet e dokumentit qe po kerkojme per ndermarrjen ne perdorim nga tabela T_ASETE_LIDHJE_MAGAZINE_SERIAL.</returns>
        internal DataTable ktheSerialetMagazineSipasIDDokumenti(int idDok, int idNdermarrje, int idkonfigambjente)
        {
            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddInputParameters("@IDDOK", idDok);
            dbManager.AddInputParameters("@IDNDERMARJE", idNdermarrje);
            dbManager.AddInputParameters("@idkonfig", idkonfigambjente);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "PRC_T_ASETE_LIDHJE_MAGAZINE_SERIAL_SEL_SIPASIDIDDOK");
            return ds.Tables.Count == 0? null : ds.Tables[0];
        }

        /// <summary>
        /// MODULI ASETE:
        /// Kthen nga databaza nepermjet procedures PRC_T_ASETE_LIDHJE_MAGAZINE_SERIAL_SEL_SIPASIDSERIALI_IDMAG nje DataTable me te gjithe dokumentat qe perdorin serialin dhe magazinen qe kerkojme per ndermarrjen ne perdorim.
        /// </summary>
        /// <param name="idSerialAQT">(int) Id e serialit aqt qe po kerkojme.</param>
        /// <param name="idNjesiAdministrative">(int) Id e njesi administrative qe po kerkojme serialin.</param>
        /// <param name="idNdermarrje">(int) Id e ndermarrjes ne perdorim per te cilen do te merret serialet e dokumentit te magazines.</param>
        /// <returns>Kthen nje DataTable me te gjithe dokumentat qe perdorin serialin dhe magazinen qe kerkojme per ndermarrjen ne perdorim nga tabela T_ASETE_LIDHJE_MAGAZINE_SERIAL.</returns>
        internal DataTable ktheSerialetMagazineSipasIDSerialIDMag(int idSerialAQT, int idNjesiAdministrative, int idNdermarrje)
        {
            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@IDSERIALI", idSerialAQT, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNJESIADMINISTRATIVE", idNjesiAdministrative, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDNDERMARJE", idNdermarrje, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "PRC_T_ASETE_LIDHJE_MAGAZINE_SERIAL_SEL_SIPASIDSERIALI_IDMAG");
            return ds.Tables[0];
        }

        internal bool ekzistonSerialetMagazineSipasIDSerialIDMag(int idSerialAQT, int idNjesiAdministrative, int idNdermarrje)
        {
            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@IDSERIALI", idSerialAQT, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNJESIADMINISTRATIVE", idNjesiAdministrative, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDNDERMARJE", idNdermarrje, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "PRC_T_ASETE_LIDHJE_MAGAZINE_SERIAL_SEL_SIPASIDSERIALI_IDMAG");
            if (ds.Tables[0].Rows.Count > 0)
                return true;
            else
                return false;
        }


        /// <summary>
        /// MODULI ASETE:
        /// Kthen nga databaza nepermjet procedures PRC_T_ASETE_LIDHJE_MAGAZINE_SERIAL_SEL_SIPASIDSERIALI_IDMAG_LLOJDOK nje DataTable me te gjithe dokumentat sipas nje lloj dokumenti per serialin dhe magazinen qe kerkojme per ndermarrjen ne perdorim.
        /// </summary>
        /// <param name="idSerialAQT">(int) Id e serialit aqt qe po kerkojme.</param>
        /// <param name="idNjesiAdministrative">(int) Id e njesi administrative qe po kerkojme serialin.</param>
        /// <param name="idNivel">(int) Id e nivelit te pare te llojit te dokumentit.</param>
        /// <param name="idKonfigAmbjenti">(int) Id e nenllojit te dokumentit.</param>
        /// <param name="idNdermarrje">(int) Id e ndermarrjes ne perdorim per te cilen do te merret serialet e dokumentit te magazines.</param>
        /// <returns>Kthen nje DataTable me te gjithe dokumentat sipas nje lloj dokumenti per serialin dhe magazinen qe kerkojme per ndermarrjen ne perdorim nga tabela T_ASETE_LIDHJE_MAGAZINE_SERIAL.</returns>
        internal DataTable ktheSerialetMagazineSipasIDSerialIDMagLlojDok(int idSerialAQT, int idNjesiAdministrative, int idNivel, int idKonfigAmbjenti, int idNdermarrje)
        {
            dbManager.Open();
            dbManager.CreateParameters(5);
            dbManager.AddParameters(0, "@IDSERIALI", idSerialAQT, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNJESIADMINISTRATIVE", idNjesiAdministrative, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDNIVELI", idNivel, ParameterDirection.Input);
            dbManager.AddParameters(3, "@IDKONFIGAMBJENTI", idKonfigAmbjenti, ParameterDirection.Input);
            dbManager.AddParameters(4, "@IDNDERMARJE", idNdermarrje, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "PRC_T_ASETE_LIDHJE_MAGAZINE_SERIAL_SEL_SIPASIDSERIALI_IDMAG_LLOJDOK");
            return ds.Tables[0];
        }

        /// <summary>
        /// MODULI ASETE:
        /// Kthen nga databaza nepermjet procedures PRC_T_ASETE_LIDHJE_MAGAZINE_SERIAL_SEL_SIPASIDSERIALI_IDDOK nje DataRow me serialin sipas id se serialit dhe dokumentit.
        /// </summary>
        /// <param name="idDok">(int) Id e dokumentit te magazines qe ka perdorur serialin qe po kerkojme.</param>
        /// <param name="idSerial">(int) Id e serialit aqt qe po kerkojme.</param>
        /// <param name="idNdermarrja">(int) Id e ndermarrjes ne perdorim per te cilen do te merret serialet e dokumentit te magazines.</param>
        /// <returns>Kthen nje DataRow me serialin dhe dokumentin qe kerkojme nga tabela T_ASETE_LIDHJE_MAGAZINE_SERIAL.</returns>
        internal DataRow ktheSerialetMagazineSipasIDSerialIDDok(int idDok, int idSerial, int idNdermarrja)
        {
            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@IDDOK", idDok, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDSERIALI", idSerial, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDNDERMARJE", idNdermarrja, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "PRC_T_ASETE_LIDHJE_MAGAZINE_SERIAL_SEL_SIPASIDSERIALI_IDDOK");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];
        }
        internal double ktheSerialetMagazineSipasIDSerialIDDokSasi(int idDok, int idSerial, int idNdermarrja)
        {
            double sasi = 0;
            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@IDDOK", idDok, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDSERIALI", idSerial, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDNDERMARJE", idNdermarrja, ParameterDirection.Input);
            double.TryParse(dbManager.ExecuteScalar(CommandType.StoredProcedure, "PRC_T_ASETE_LIDHJE_MAGAZINE_SERIAL_SEL_SIPASIDSERIALI_IDDOKSasi").ToString(), out sasi);
            return sasi;
        }

        /// <summary>
        /// MODULI ASETE:
        /// Kthen nga databaza nepermjet procedures PRC_T_ASETE_LIDHJE_MAGAZINE_SERIAL_SEL_SIPASIDSERIALI_DOKFUNDIT nje DataRow me serialin sipas id se serialit dhe dokumentit te fundit.
        /// </summary>
        /// <param name="idSerial">(int) Id e serialit aqt qe po kerkojme.</param>
        /// <param name="idNdermarrja">(int) Id e ndermarrjes ne perdorim per te cilen do te merret serialet e dokumentit te magazines.</param>
        /// <returns>Kthen nje DataRow me serialin dhe dokumentin te fundit qe kerkojme nga tabela T_ASETE_LIDHJE_MAGAZINE_SERIAL.</returns>
        internal DataRow ktheSerialetMagazineSipasIDSerialDokFunditmerrVepriminFundit(int idSerial, int idNdermarrja, DateTime data, int nrrend)
        {

            dbManager.Open();
            dbManager.CreateParameters(4);
            dbManager.AddParameters(0, "@IDSERIALI", idSerial, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idNdermarrja, ParameterDirection.Input);
            dbManager.AddParameters(2, "@Data", data, ParameterDirection.Input);
            dbManager.AddParameters(3, "@nrrendditor", nrrend, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "PRC_T_ASETE_LIDHJE_MAGAZINE_SERIAL_SEL_SIPASIDSERIALIDheDates");

            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;

            return ds.Tables[0].Rows[0];

        }
        internal int ktheSerialetMagazineSipasIdmagFundit(int idSerial, int idNdermarrja, DateTime data)
        {

            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@IDSERIALI", idSerial, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idNdermarrja, ParameterDirection.Input);
            dbManager.AddParameters(2, "@Data", data, ParameterDirection.Input);

            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "PRC_T_ASETE_LIDHJE_MAGAZINE_SERIAL_SEL_SIPASIDSERIALIDheDatesMerrMagFundit");
            if (ds == null)
                return -1;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return -1;
            int idnjesi;
            int.TryParse(ds.Tables[0].Rows[0]["IDNJESIADMINISTRATIVE"].ToString(), out idnjesi);
            return idnjesi;

        }

        /// <summary>
        /// MODULI ASETE:
        /// Kthen nga databaza nepermjet procedures PRC_T_ASETE_LIDHJE_MAGAZINE_SERIAL_SEL_SIPASIDSERIALI_IDDOK nje DataRow me serialin sipas id se serialit dhe dokumentit.
        /// </summary>
        /// <param name="idDok">(int) Id e dokumentit te magazines qe ka perdorur serialin qe po kerkojme.</param>
        /// <param name="idSerial">(int) Id e serialit aqt qe po kerkojme.</param>
        /// <param name="idNdermarrja">(int) Id e ndermarrjes ne perdorim per te cilen do te merret serialet e dokumentit te magazines.</param>
        /// <returns>Kthen id e trupit te serialit dhe dokumentin qe kerkojme nga tabela T_ASETE_LIDHJE_MAGAZINE_SERIAL.</returns>
        internal int ktheIDSerialetMagazineSipasIDSerialIDDok(int idDok, int idSerial, int idNdermarrja)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDDOK", idDok, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDSERIALI", idSerial, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDNDERMARJE", idNdermarrja, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "PRC_T_ASETE_LIDHJE_MAGAZINE_SERIAL_SEL_SIPASIDSERIALI_IDDOK");
            if (ds == null)
                return -1;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return -1;

            int idSerialMagazine;
            int.TryParse(ds.Tables[0].Rows[0]["ID_SERIALTRUPI"].ToString(), out idSerialMagazine);
            return idSerialMagazine;
        }

        /// <summary>
        /// MODULI ASETE:
        /// Kthen nga databaza nepermjet procedures PRC_T_ASETE_LIDHJE_MAGAZINE_SERIAL_SEL_SIPASIDSERIALI_DOKFUNDIT nje DataRow me serialin sipas id se serialit dhe dokumentit te fundit qe eshte prekur seriali PER ARTIKUJT E PANDASHEM.
        /// PER ARTIIKUJT E NDASHEM SHIKO: ktheHistorikuAQTSerialVleftaSipasIDAQTSerialit
        /// </summary>
        /// <param name="idSerial">(int) Id e serialit aqt qe po kerkojme.</param>
        /// <param name="idNdermarrja">(int) Id e ndermarrjes ne perdorim per te cilen do te merret serialet e dokumentit te magazines.</param>
        /// <returns>Kthen nje float me vleren e fundit te serialit nga tabela T_ASETE_LIDHJE_MAGAZINE_SERIAL.</returns>
        internal double ktheSerialetMagazineSipasIDSerialDokFundit(int idSerial, int idNdermarrja, DateTime data, int nrrend)
        {
            dbManager.Open();
            dbManager.CreateParameters(4);
            dbManager.AddParameters(0, "@IDSERIALI", idSerial, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idNdermarrja, ParameterDirection.Input);
            dbManager.AddParameters(2, "@Data", data, ParameterDirection.Input);
            dbManager.AddParameters(3, "@nrrendditor", nrrend, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "PRC_T_ASETE_LIDHJE_MAGAZINE_SERIAL_SEL_SIPASIDSERIALIDheDates");
            if (ds == null)
                return -1;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return -1;

            double vlefta;
            double.TryParse(ds.Tables[0].Rows[0]["VLEFTA"].ToString(), out vlefta);
            return vlefta;

        }
        internal double ktheSerialetMagazineSipasIDSerialDheDates(int idSerial, int idNdermarrja, DateTime data, int nrrend)
        {
            dbManager.Open();
            dbManager.CreateParameters(4);
            dbManager.AddParameters(0, "@IDSERIALI", idSerial, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idNdermarrja, ParameterDirection.Input);
            dbManager.AddParameters(2, "@Data", data, ParameterDirection.Input);
            dbManager.AddParameters(3, "@nrrendditor", nrrend, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "PRC_T_ASETE_LIDHJE_MAGAZINE_SERIAL_SEL_SIPASIDSERIALIDheDates");
            if (ds == null)
                return -1;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return -1;

            double vlefta;
            double.TryParse(ds.Tables[0].Rows[0]["VLEFTA"].ToString(), out vlefta);
            return vlefta;
        }
        internal double ktheSerialetMagazineRezervaSipasIDSerialDheDates(int idSerial, int idNdermarrja, DateTime data)
        {
            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@IDSERIALI", idSerial, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idNdermarrja, ParameterDirection.Input);
            dbManager.AddParameters(2, "@Data", data, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "PRC_T_ASETE_AMORTIZIMI_REZERVA_TRUPI_SIPASIDSERIALIDheDates");
            if (ds == null)
                return -1;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return -1;

            double vlefta;
            double.TryParse(ds.Tables[0].Rows[0]["Gjendje"].ToString(), out vlefta);
            return vlefta;
        }

        /// <summary>
        /// MODULI ASETE:
        /// Kthen nga databaza nepermjet procedures PRC_T_ASETE_LIDHJE_MAGAZINE_SERIAL_SEL_SIPASIDSERIALI_DOKFUNDIT nje DataRow me serialin sipas id se serialit dhe dokumentit te fundit qe eshte prekur seriali.
        /// </summary>
        /// <param name="idSerial">(int) Id e serialit aqt qe po kerkojme.</param>
        /// <param name="idNdermarrja">(int) Id e ndermarrjes ne perdorim per te cilen do te merret serialet e dokumentit te magazines.</param>
        /// <returns>Kthen nje float me cmimin e fundit te serialit nga tabela T_ASETE_LIDHJE_MAGAZINE_SERIAL.</returns>
        internal double ktheSerialetMagazineCmimiSipasIDSerialDokFundit(int idSerial, int idNdermarrja, DateTime data, int nrrend)
        {
            dbManager.Open();
            dbManager.CreateParameters(4);
            dbManager.AddParameters(0, "@IDSERIALI", idSerial, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idNdermarrja, ParameterDirection.Input);
            dbManager.AddParameters(2, "@Data", data, ParameterDirection.Input);
            dbManager.AddParameters(3, "@nrrendditor", nrrend, ParameterDirection.Input);

            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "PRC_T_ASETE_LIDHJE_MAGAZINE_SERIAL_SEL_SIPASIDSERIALIDheDates");
            if (ds == null)
                return -1;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return -1;




            double cmimi;
            double.TryParse(ds.Tables[0].Rows[0]["CMIMI"].ToString(), out cmimi);
            return cmimi;

        }

        /// <summary>
        /// MODULI ASETE:
        /// Kthen nga databaza nepermjet procedures PRC_T_ASETE_LIDHJE_MAGAZINE_SERIAL_SEL_GjendjeTotaleartikulli vleften totale te gjendjes se artikullit ne magazine.
        /// </summary>
        /// <param name="idartikull">(int) Id e artikullit.</param>
        /// <param name="idNdermarrja">(int) Id e ndermarrjes per te cilen po behet kerkimi.</param>
        /// <param name="idmagazina">(int) Id e magazines qe kerkojme.</param>
        /// <returns>Kthen nje float me vleften totale te gjendjes se artikullit ne magazine nga tabela T_ASETE_LIDHJE_MAGAZINE_SERIAL.</returns>
        internal double ktheSerialetMagazineGjendjeTotaleArtikulliNeMagazine(int idartikull, int idNdermarrja, int idmagazina, DateTime data)
        {
            dbManager.Open();
            dbManager.CreateParameters(4);
            dbManager.AddParameters(0, "@IDaqt", idartikull, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idNdermarrja, ParameterDirection.Input);
            dbManager.AddParameters(2, "@idmagazina", idmagazina, ParameterDirection.Input);
            dbManager.AddParameters(3, "@data", data, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "PRC_T_ASETE_LIDHJE_MAGAZINE_SERIAL_SEL_GjendjeTotaleartikulli");
            if (ds == null)
                return -1;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return -1;

            double gjendje;
            double.TryParse(ds.Tables[0].Rows[0]["Gjendje"].ToString(), out gjendje);
            return gjendje;
        }
        internal double ktheGjendjeRezervaMagazine(int idartikull, int idNdermarrja, int idmagazina, DateTime data)
        {
            dbManager.Open();
            dbManager.CreateParameters(4);
            dbManager.AddParameters(0, "@IDaqt", idartikull, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idNdermarrja, ParameterDirection.Input);
            dbManager.AddParameters(2, "@idmagazina", idmagazina, ParameterDirection.Input);
            dbManager.AddParameters(3, "@data", data, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "PRC_T_ASETE_AMORTIZIMI_REZERVA_TRUPI_GjendjeTotaleartikulli");
            if (ds == null)
                return -1;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return -1;

            double gjendje;
            double.TryParse(ds.Tables[0].Rows[0]["Gjendje"].ToString(), out gjendje);
            return gjendje;
        }


        internal DataTable MerrAseteJoNeHarte(int idNdermarrje, int idnderviti, int idPerdorues, int idLayerType)
        {
            dbManager.Open();
            dbManager.CreateParameters(4);
            dbManager.AddParameters(0, "@IDNDERMARRJE", idNdermarrje, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMVIT", idnderviti, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDLAYERTYPE", idLayerType, ParameterDirection.Input);
            dbManager.AddParameters(3, "@IDPERDORUES", idPerdorues, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_ASETE_LIDHJE_MAGAZINE_SERIAL_perNdermarrje");
            return ds.Tables[0];
        }


        #endregion

        #region Historiku AQT pa Serial

        /// <summary>
        /// MODULI ASETE:
        /// Ruan ne tabelen T_ASETE_SERIALE_HISTORIKU objektin e historikut te serialit te aqt-se nepermjet procedures PRC_T_ASETE_SERIALE_HISTORIKU_INSERT.
        /// </summary>
        /// <param name="idHistoriku">(int) Id e historikut te serialeve grup te aqt-ve.</param>
        /// <param name="idAQTSeriale">(int) Id e serialit te aqt-se.</param>
        /// <param name="idPrindi">(int) Id e prindit direkt te id se serialit.</param>
        /// <param name="idPrindiFillestar">(int) Id e prindit fillestar te serialit.</param>
        /// <param name="idDok">(int) Id e dokumentit qe ka krijuar serialin.</param>
        /// <param name="sasiaProgresive">(float) Sasia progresive qe perfaqeson seriali.</param>
        /// <param name="cmimiProgresive">(float) Cmimi qe perfaqeson serialin.</param>
        /// <param name="vleftaProgresive">(float) Vlefta qe perfaqeson serialin.</param>
        /// <param name="idStatusDokumenti">(int) Id se ne cfare gjendje eshte lidhja mes dokumentit dhe serialit, i ruajtur, fshire, apo modifikuar</param>
        /// <param name="idNdermarrje">(int) Id e ndermarrjes per te cilen po behet regjistrimi i lidhjes.</param>
        /// <param name="idPerdoruesi">(int) Id e perdoruesit te fundit qe ka modifikuar lidhjen.</param>
        /// <param name="idKrijuesi">(int) Id e krijuesit per here te pare te lidhjes.</param>
        /// <param name="dtModifikimi">(DateTime) Data e modifikimit te lidhjes.</param>
        /// <returns>Kthen clsMesazh me atribut statusi True nese ruajtja kryhet me sukses ose False dhe mesazhin e gabimit ne te kundert.</returns>
        internal clsMesazh ruajHistorikAQTSerial(out int idHistoriku, int idAQTSeriale, int idPrindi, int idPrindiFillestar, int idDok, double sasiaProgresive,
            double cmimiProgresive, double vleftaProgresive, int idStatusDokumenti, int idNdermarrje, int idPerdoruesi, int idKrijuesi, DateTime dtModifikimi)
        {
            idHistoriku = 0;

            dbManager.Open();
            dbManager.CreateParameters(13);
            dbManager.AddParameters(0, "@ID_HISTORIKU", idHistoriku, ParameterDirection.Output);
            dbManager.AddParameters(1, "@IDAQTSERIALE", idAQTSeriale, ParameterDirection.Input);
            if (idPrindi < 1) dbManager.AddParameters(2, "@IDPRINDI", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(2, "@IDPRINDI", idPrindi, ParameterDirection.Input);
            if (idPrindiFillestar < 1) dbManager.AddParameters(3, "@IDPRINDIFILLESTAR", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(3, "@IDPRINDIFILLESTAR", idPrindiFillestar, ParameterDirection.Input);
            if (idDok < 1) dbManager.AddParameters(4, "@IDDOK", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(4, "@IDDOK", idDok, ParameterDirection.Input);
            dbManager.AddParameters(5, "@SASIAPROGRESIVE", sasiaProgresive, ParameterDirection.Input);
            dbManager.AddParameters(6, "@CMIMI", cmimiProgresive, ParameterDirection.Input);
            dbManager.AddParameters(7, "@VLEFTAPROGRESIVE", vleftaProgresive, ParameterDirection.Input);
            dbManager.AddParameters(8, "@IDSTATUSDOK", idStatusDokumenti, ParameterDirection.Input);
            dbManager.AddParameters(9, "@IDNDERMARJE", idNdermarrje, ParameterDirection.Input);
            dbManager.AddParameters(10, "@IDPERDORUESI", idPerdoruesi, ParameterDirection.Input);
            dbManager.AddParameters(11, "@IDKRIJUESI", idKrijuesi, ParameterDirection.Input);
            if (dtModifikimi == DateTime.MinValue) dbManager.AddParameters(12, "@DTMODIFIKIMI", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(12, "@DTMODIFIKIMI", dtModifikimi, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "PRC_T_ASETE_SERIALE_HISTORIKU_INSERT");
            idHistoriku = int.Parse(dbManager.Parameters[0].Value.ToString());
            return new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);

        }

        /// <summary>
        /// MODULI ASETE:
        /// Fshirja e historikut te serialeve duke i ndryshuar statuset ne te fshire (2) ne tabelen T_ASETE_SERIALE_HISTORIKU nepermjet procedures PRC_T_ASETE_SERIALE_HISTORIKU_UPDATEDELETE. 
        /// </summary>
        /// <param name="idDok">(int) Id e dokumentit qe ka krijuar serialin.</param>
        /// <param name="idPerdoruesi">(int) Id e perdoruesit te fundit qe ka modifikuar lidhjen.</param>
        /// <returns>Kthen clsMesazh me atribut statusi True nese modifikimi kryhet me sukses ose False dhe mesazhin e gabimit ne te kundert.</returns>
        internal clsMesazh modifikoHistorikAQTSerialStatus(int idDok, int idPerdoruesi)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDDOK", idDok, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUESI", idPerdoruesi, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "PRC_T_ASETE_SERIALE_HISTORIKU_UPDATEDELETE");
            return new clsMesazh(true, MessagesResource.Messages["msgModifikimiMeSukses"]);
        }

        /// <summary>
        /// MODULI ASETE:
        /// Modifikimi i historikut te serialeve duke i ndryshuar statuset ne tabelen T_ASETE_SERIALE_HISTORIKU nepermjet procedures PRC_T_ASETE_SERIALE_HISTORIKU_UPDATE. 
        /// </summary>
        /// <param name="idhistriku">(int) Id e historikut qe do modifikojme.</param>
        /// <param name="idPerdoruesi">(int) Id e perdoruesit te fundit qe ka modifikuar lidhjen.</param>
        /// <param name="iddok">(int) Id e dokumentit qe ka shkaktuar krijimin e serialit.</param>
        /// <param name="sasiaProgresive">(float) Sasia progresive qe perfaqeson seriali.</param>
        /// <param name="cmimiProgresive">(float) Cmimi qe perfaqeson serialin.</param>
        /// <param name="vleftaProgresive">(float) Vlefta qe perfaqeson serialin.</param>
        /// <param name="idstatusdok">(int) Id se ne cfare gjendje eshte lidhja mes dokumentit dhe serialit, i ruajtur, fshire, apo modifikuar</param>
        /// <returns>Kthen clsMesazh me atribut statusi True nese modifikimi kryhet me sukses ose False dhe mesazhin e gabimit ne te kundert.</returns>
        internal clsMesazh modifikoHistorikAQTSerial(int idhistriku, int idPerdoruesi, int iddok, double sasiaProgresive, double cmimiProgresive, double vleftaProgresive, int idstatusdok)
        {

            dbManager.Open();
            dbManager.CreateParameters(7);
            dbManager.AddParameters(0, "@ID_HISTORIKU", idhistriku, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUESI", idPerdoruesi, ParameterDirection.Input);
            if (iddok < 1) dbManager.AddParameters(2, "@IDDOK", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(2, "@IDDOK", iddok, ParameterDirection.Input);
            dbManager.AddParameters(3, "@SASIAPROGRESIVE", sasiaProgresive, ParameterDirection.Input);
            dbManager.AddParameters(4, "@CMIMI", cmimiProgresive, ParameterDirection.Input);
            dbManager.AddParameters(5, "@VLEFTAPROGRESIVE", vleftaProgresive, ParameterDirection.Input);
            dbManager.AddParameters(6, "@idstatusdok", idstatusdok, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "PRC_T_ASETE_SERIALE_HISTORIKU_UPDATE");
            return new clsMesazh(true, MessagesResource.Messages["msgModifikimiMeSukses"]);

        }

        /// <summary>
        /// MODULI ASETE:
        /// Fshirja e historikut te serialeve duke i ndryshuar statuset ne te fshire (2) ne tabelen T_ASETE_SERIALE_HISTORIKU nepermjet procedures PRC_T_ASETE_SERIALE_HISTORIKU_UPDATEDELETE. 
        /// </summary>
        /// <param name="idHistorik">(int) Id e historikut qe do modifikojme.</param>
        /// <param name="idPerdoruesi">(int) Id e perdoruesit te fundit qe ka modifikuar lidhjen.</param>
        /// <returns>Kthen clsMesazh me atribut statusi True nese modifikimi kryhet me sukses ose False dhe mesazhin e gabimit ne te kundert.</returns>
        internal clsMesazh modifikoHistorikAQTSerialStatusSipasHistorikID(int idHistorik, int idPerdoruesi)
        {

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@ID_HISTORIKU", idHistorik, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUESI", idPerdoruesi, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "PRC_T_ASETE_SERIALE_HISTORIKU_UPDATEDELETESIPASIDHIST");
            return new clsMesazh(true, MessagesResource.Messages["msgModifikimiMeSukses"]);

        }

        /// <summary>
        /// MODULI ASETE:
        /// Fshin objektin e historikut te serialit te aqt-ve ne tabelen T_ASETE_SERIALE_HISTORIKU nepermjet procedures PRC_T_ASETE_SERIALE_HISTORIKU_DELETE. 
        /// </summary>
        /// <param name="idHistoriku">(int) Id e historikut te serialeve grup te aqt-ve.</param>
        /// <returns>Kthen clsMesazh me atribut statusi True nese fshirja kryhet me sukses ose False dhe mesazhin e gabimit ne te kundert.</returns>
        internal clsMesazh fshiHistorikAQTSerial(int idHistoriku)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@ID_HISTORIKU", idHistoriku, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "PRC_T_ASETE_SERIALE_HISTORIKU_DELETE");
            return new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
        }

        /// <summary>
        /// MODULI ASETE:
        /// Kthen nga databaza nepermjet procedures PRC_T_ASETE_SERIALE_HISTORIKU_SEL_SIPASIDPRINDI nje DataTable me te gjithe historiket e serialeve sipas id-se se prindit per ndermarrjen ne perdorim.
        /// </summary>
        /// <param name="idPrind">(int) Id e prindit nje shkalle me siper nga eshte krijuar seriali.</param>
        /// <param name="idNdermarrje">(int) Id e ndermarrjes ne perdorim per te cilen do te merret historiku i aqt-se.</param>
        /// <returns>Kthen nje DataTable me te gjithe historikun e serialeve qe kane prindin qe kerkojme nga tabela T_ASETE_SERIALE_HISTORIKU.</returns>
        internal DataTable ktheHistorikAQTSerialSipasIDPrind(int idPrind, int idNdermarrje)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDPRINDI", idPrind, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idNdermarrje, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "PRC_T_ASETE_SERIALE_HISTORIKU_SEL_SIPASIDPRINDI");
            return ds.Tables[0];
        }

        /// <summary>
        /// MODULI ASETE:
        /// Kthen nga databaza nepermjet procedures PRC_T_ASETE_SERIALE_HISTORIKU_SEL_SIPASIDPRINDIFILLESTAR nje DataTable me te gjithe historiket e serialeve sipas id-se se prinderve fillestare per ndermarrjen ne perdorim.
        /// </summary>
        /// <param name="idPrindFillestar">(int) Id e prindit fillestar nga ka rrjedhur seriali.</param>
        /// <param name="idNdermarrje">(int) Id e ndermarrjes ne perdorim per te cilen do te merret historiku i aqt-se.</param>
        /// <returns>Kthen nje DataTable me te gjithe historikun e serialeve qe kane prindin fillestar qe kerkojme nga tabela T_ASETE_SERIALE_HISTORIKU.</returns>
        internal DataTable ktheHistorikAQTSerialSipasIDPrindFillestar(int idPrindFillestar, int idNdermarrje)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDPRINDIFILLESTAR", idPrindFillestar, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idNdermarrje, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "PRC_T_ASETE_SERIALE_HISTORIKU_SEL_SIPASIDPRINDIFILLESTAR");
            return ds.Tables[0];
        }

        /// <summary>
        /// MODULI ASETE:
        /// Kthen nga databaza nepermjet procedures PRC_T_ASETE_SERIALE_HISTORIKU_SEL_SIPASIDDOKUMENTMAG nje DataTable me te gjithe historiket e serialeve te nje dokumenti per ndermarrjen ne perdorim.
        /// </summary>
        /// <param name="idDok">(int) Id e dokumentit qe ka shkaktuar krijimin e serialit.</param>
        /// <param name="idNdermarrje">(int) Id e ndermarrjes ne perdorim per te cilen do te merret historiku i aqt-se.</param>
        /// <returns>Kthen nje DataTable me te gjithe historikun e serialeve qe jane gjeneruar nga i njejti id dokumenti nga tabela T_ASETE_SERIALE_HISTORIKU.</returns>
        internal DataTable ktheHistorikAQTSerialSipasIDDok(int idDok, int idNdermarrje)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDDOK", idDok, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idNdermarrje, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "PRC_T_ASETE_SERIALE_HISTORIKU_SEL_SIPASIDDOKUMENTMAG");
            return ds.Tables[0];
        }
        internal DataTable ktheHistorikAQTSerialSipasIDDokTeFshira(int idDok, int idNdermarrje)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDDOK", idDok, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idNdermarrje, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "PRC_T_ASETE_SERIALE_HISTORIKU_SEL_SIPASIDDOKUMENTMAGTeFshira");
            return ds.Tables[0];
        }

        /// <summary>
        /// MODULI ASETE:
        /// Kthen nga databaza nepermjet procedures PRC_T_ASETE_SERIALE_HISTORIKU_SEL_SIPASIDHISTORIKU nje DataRow me historikun e serialit sipas id se historikut.
        /// </summary>
        /// <param name="idHistorikAQTSerial">(int) Id automatike e historikut te serialit aqt per artikujt grup.</param>
        /// <param name="idNdermarrja">(int) Id e ndermarrjes ne perdorim per te cilen do te merret historiku i aqt-se.</param>
        /// <returns>Kthen nje DataRow me historikun e serialit sipas id se historikut nga tabela T_ASETE_SERIALE_HISTORIKU.</returns>
        internal DataRow ktheHistorikAQTSerialSipasID(int idHistorikAQTSerial, int idNdermarrja)
        {

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@ID_HISTORIKU", idHistorikAQTSerial, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idNdermarrja, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "PRC_T_ASETE_SERIALE_HISTORIKU_SEL_SIPASIDHISTORIKU");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];

        }

        /// <summary>
        /// MODULI ASETE:
        /// Kthen nga databaza nepermjet procedures PRC_T_ASETE_SERIALE_HISTORIKU_SEL_SIPASIDAQTSERIAL nje DataRow me historikun e serialit qe kerkojme ne ndermarrjen ne perdorim.
        /// </summary>
        /// <param name="idAQTSerial">(int) Id e serialit te artikullit AQT per te cilen po kerkohet historiku.</param>
        /// <param name="idNdermarrja">(int) Id e ndermarrjes ne perdorim per te cilen do te merret historiku i aqt-se.</param>
        /// <returns>Kthen nje DataRow me historikun e serialit qe kerkojme nga tabela T_ASETE_SERIALE_HISTORIKU.</returns>
        internal DataRow ktheHistorikAQTSerialSipasIDAQT(int idAQTSerial, int idNdermarrja)
        {

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDAQTSERIALE", idAQTSerial, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idNdermarrja, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "PRC_T_ASETE_SERIALE_HISTORIKU_SEL_SIPASIDAQTSERIAL");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];

        }

        /// <summary>
        /// MODULI ASETE:
        /// Kthen nga databaza nepermjet procedures PRC_T_ASETE_SERIALE_HISTORIKU_SEL_SIPASIDAQTSERIAL nje DataRow me historikun e serialit qe kerkojme ne ndermarrjen ne perdorim.
        /// </summary>
        /// <param name="idAQTSerial">(int) Id e serialit te artikullit AQT per te cilen po kerkohet historiku.</param>
        /// <param name="idNdermarrja">(int) Id e ndermarrjes ne perdorim per te cilen do te merret historiku i aqt-se.</param>
        /// <returns>Kthen nje int me id e historikut te serialit te aqt-se nga tabela T_ASETE_SERIALE_HISTORIKU.</returns>
        internal int ktheIDHistorikuAQTSerialSipasIDAQT(int idAQTSerial, int idNdermarrja)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDAQTSERIALE", idAQTSerial, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idNdermarrja, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "PRC_T_ASETE_SERIALE_HISTORIKU_SEL_SIPASIDAQTSERIAL");
            if (ds == null)
                return -1;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return -1;

            int idHistorikAQTSerial;
            int.TryParse(ds.Tables[0].Rows[0]["ID_HISTORIKU"].ToString(), out idHistorikAQTSerial);
            return idHistorikAQTSerial;
        }

        /// <summary>
        /// MODULI ASETE:
        /// Kthen nga databaza nepermjet procedures PRC_T_ASETE_SERIALE_HISTORIKU_SEL_SIPASIDAQTSERIAL nje DataRow me historikun e serialit qe kerkojme ne ndermarrjen ne perdorim.
        /// </summary>
        /// <param name="idAQTSerial">(int) Id e serialit te artikullit AQT per te cilen po kerkohet historiku.</param>
        /// <param name="idNdermarrja">(int) Id e ndermarrjes ne perdorim per te cilen do te merret historiku i aqt-se.</param>
        /// <returns>Kthen nje float me sasine aktuale te serialit te aqt-se nga tabela T_ASETE_SERIALE_HISTORIKU.</returns>
        internal double ktheHistorikuAQTSerialSasiSipasIDAQTSerialit(int idAQTSerial, int idNdermarrja)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDAQTSERIALE", idAQTSerial, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idNdermarrja, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "PRC_T_ASETE_SERIALE_HISTORIKU_SEL_SIPASIDAQTSERIAL");
            if (ds == null)
                return -1;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return -1;

            double sasiaTotaleSerial;
            double.TryParse(ds.Tables[0].Rows[0]["SASIAPROGRESIVE"].ToString(), out sasiaTotaleSerial);
            return sasiaTotaleSerial;
        }

        /// <summary>
        /// MODULI ASETE:
        /// Kthen nga databaza nepermjet procedures PRC_T_ASETE_SERIALE_HISTORIKU_SEL_SIPASIDAQTSERIAL nje DataRow me historikun e serialit qe kerkojme ne ndermarrjen ne perdorim.
        /// </summary>
        /// <param name="idAQTSerial">(int) Id e serialit te artikullit AQT per te cilen po kerkohet historiku.</param>
        /// <param name="idNdermarrja">(int) Id e ndermarrjes ne perdorim per te cilen do te merret historiku i aqt-se.</param>
        /// <returns>Kthen nje float me vleften aktuale te serialit te aqt-se nga tabela T_ASETE_SERIALE_HISTORIKU.</returns>
        internal double ktheHistorikuAQTSerialVleftaSipasIDAQTSerialit(int idAQTSerial, int idNdermarrja)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDAQTSERIALE", idAQTSerial, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idNdermarrja, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "PRC_T_ASETE_SERIALE_HISTORIKU_SEL_SIPASIDAQTSERIAL");
            if (ds == null)
                return -1;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return -1;

            double vleftaTotaleSerial;
            double.TryParse(ds.Tables[0].Rows[0]["VLEFTAPROGRESIVE"].ToString(), out vleftaTotaleSerial);
            return vleftaTotaleSerial;
        }

        /// <summary>
        /// MODULI ASETE:
        /// Kthen nga databaza nepermjet procedures PRC_T_ASETE_SERIALE_HISTORIKU_SEL_SHUMASASI_SIPASARTIKULL shumen totale te sasise per artikullin.
        /// </summary>
        /// <param name="idArtikull">(int) Id e artikullit.</param>
        /// <returns>Kthen nje float me shumen totale te sasise se artikullit nga tabela T_ASETE_LIDHJE_MAGAZINE_SERIAL.</returns>
        internal double ktheHistorikuAQTSerialShumaTotaleSasi(int idArtikull, int idnjesiadm, DateTime date)
        {
            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@IDAQTART", idArtikull, ParameterDirection.Input);
            dbManager.AddParameters(1, "@idnjesiadm", idnjesiadm, ParameterDirection.Input);
            dbManager.AddParameters(2, "@dtdok", date, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "PRC_T_ASETE_SERIALE_HISTORIKU_SEL_SHUMASASI_SIPASARTIKULL");
            if (ds == null)
                return -1;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return -1;

            double sasiaTotale;
            double.TryParse(ds.Tables[0].Rows[0]["SASIA"].ToString(), out sasiaTotale);
            return sasiaTotale;
        }

        /// <summary>
        /// MODULI ASETE:
        /// Kthen nga databaza nepermjet procedures PRC_T_ASETE_SERIALE_HISTORIKU_SEL_SIPASIDAQTSERIAL nje DataRow me historikun e serialit ne ndermarrjen ne perdorim per te pare ne ekziston apo jo historiku i serialit.
        /// </summary>
        /// <param name="idAQTSerial">(int) Id e serialit te artikullit AQT per te cilen po kerkohet historiku.</param>
        /// <param name="idNdermarrja">(int) Id e ndermarrjes ne perdorim per te cilen do te merret historiku i aqt-se.</param>
        /// <returns>Kthen True nese ekziston njehere historiku i serialit per ndermarrjen dhe False ne te kundert.</returns>
        internal bool ekzistonHistorikuAQTSerial(int idAQTSerial, int idNdermarrja)
        {

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDAQTSERIALE", idAQTSerial, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idNdermarrja, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "PRC_T_ASETE_SERIALE_HISTORIKU_SEL_SIPASIDAQTSERIAL");
            if (ds == null)
                return true;
            if (ds.Tables[0].Rows.Count == 0)
                return false;
            return true;

        }

        #endregion

        #region Koka e Rivleresimit

        /// <summary>
        /// MODULI ASETE:
        /// Ruan ne tabelen T_ASETE_SERIALxRIVLERESIM_KOKA objektin e kokes se rivleresimit nepermjet procedures PRC_T_ASETE_SERIALxRIVLERESIM_KOKA_INSERT.
        /// </summary>
        /// <param name="idKokaRivlersim">(int) Id automatike e kokes se rivleresimit.</param>
        /// <param name="nrDok">(string) Numri i dokumentit te amortizimit.</param>
        /// <param name="idNiveli">(int) Id e nenkategorise qe do te perdoret per fleten e amortizimit FA.</param>
        /// <param name="idKonfigurimAmbjenti">(int) Id e llojit te dokumentit qe eshte perdorur ne dokumentin e amortizimit.</param>
        /// <param name="dateDokumenti">(DateTime) Data e dokumentit te amortizimit.</param>
        /// <param name="dateRegjistrimi">(DateTime) Data qe eshte bere regjistrimi i dokumentit te amortizimit.</param>
        /// <param name="idNjesiAdministrative">(int) Id e njesise administrative per te cilen po behet regjistrimi i dokumentit te amortizimit.</param>
        /// <param name="pershkrimi">(string) Shenime te ndryshme qe mund te vendosen ne dokumentin e amortizimit.</param>
        /// <param name="idNderViti">(int) Id e lidhjes se ndermarrjes me nje vit kalendarik te caktuar.</param>
        /// <param name="idLlojStandarti">(int) Id e llojit te standartit per te cilen po behet llogaritja e amortizimit.</param>
        /// <param name="idStatusDokumenti">(int) Id se ne cfare gjendje eshte dokumenti i amortizimit, i ruajtur, fshire, apo modifikuar</param>
        /// <param name="idNdermarrje">(int) Id e ndermarrjes per te cilen po behet regjistrimi i amortizimit.</param>
        /// <param name="idPerdoruesi">(int) Id e perdoruesit te fundit qe ka modifikuar dokumentin e regjistrimit te amortizimit.</param>
        /// <param name="idKrijuesi">(int) Id e krijuesit per here te pare te dokumentit te amortizimit.</param>
        /// <param name="dtKrijimi">(DateTime) Data e krijimit te dokumentit te amortizimit.</param>
        /// <param name="idDokGjenerues">(int) Id e dokumentit qe e ka gjeneruar dokumentin e amortizimit.</param>
        /// <param name="idNivelGjenerues">(int) Id e nenkategorise qe e ka gjeneruar dokumentin e amortizmit.</param>
        /// <param name="idKonfigGjenerues">(int) Id e llojit te dokumentit qe e ka gjeneruar dokumentin e amortizimit.</param>
        /// <param name="idDokNga">(int) Id e dokumentit nga vjen dokumenti i amortizimit.</param>
        /// <param name="idLlogKunderparti">(int) Id e llogarise kunderparti.</param>
        /// <returns>Kthen clsMesazh me atribut statusi True nese ruajtja kryhet me sukses ose False dhe mesazhin e gabimit ne te kundert.</returns>
        internal clsMesazh ruajKokenERivleresimit(out int idKokaRivlersim, string nrDok, int idNiveli, int idKonfigurimAmbjenti, DateTime dateDokumenti, DateTime dateRegjistrimi,
            int idNjesiAdministrative, string pershkrimi, int idNderViti, int idLlojStandarti, int idStatusDokumenti, int idNdermarrje, int idPerdoruesi,
            int idKrijuesi, DateTime dtKrijimi, int idDokGjenerues, int idNivelGjenerues, int idKonfigGjenerues, int idDokNga, int idLlogKunderparti)
        {
            idKokaRivlersim = 0;

            dbManager.Open();
            dbManager.CreateParameters(20);
            dbManager.AddParameters(0, "@IDKOKARIVLERSIM", idKokaRivlersim, ParameterDirection.Output);
            dbManager.AddParameters(1, "@NR_DOK", nrDok, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDNIVELI", idNiveli, ParameterDirection.Input);
            dbManager.AddParameters(3, "@IDKONFIGURIMAMBJENTI", idKonfigurimAmbjenti, ParameterDirection.Input);
            dbManager.AddParameters(4, "@DATE_DOKUMENTI", dateDokumenti, ParameterDirection.Input);
            dbManager.AddParameters(5, "@DATE_REGJISTRIMI", dateRegjistrimi, ParameterDirection.Input);
            if (idNjesiAdministrative < 1) dbManager.AddParameters(6, "@IDNJESIADMINISTRATIVE", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(6, "@IDNJESIADMINISTRATIVE", idNjesiAdministrative, ParameterDirection.Input);
            dbManager.AddParameters(7, "@PERSHKRIMI", pershkrimi, ParameterDirection.Input);
            if (idNderViti < 1) dbManager.AddParameters(8, "@IDNDERVITI", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(8, "@IDNDERVITI", idNderViti, ParameterDirection.Input);
            dbManager.AddParameters(9, "@IDLLOJSTANDARTI", idLlojStandarti, ParameterDirection.Input);
            dbManager.AddParameters(10, "@IDSTATUSDOK", idStatusDokumenti, ParameterDirection.Input);
            dbManager.AddParameters(11, "@IDNDERMARJE", idNdermarrje, ParameterDirection.Input);
            dbManager.AddParameters(12, "@IDPERDORUESI", idPerdoruesi, ParameterDirection.Input);
            dbManager.AddParameters(13, "@IDKRIJUESI", idKrijuesi, ParameterDirection.Input);
            dbManager.AddParameters(14, "@DTMODIFIKIMI", dtKrijimi, ParameterDirection.Input);
            if (idNivelGjenerues < 1) dbManager.AddParameters(15, "@IDNIVELGJENERUES", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(15, "@IDNIVELGJENERUES", idNivelGjenerues, ParameterDirection.Input);
            if (idKonfigGjenerues < 1) dbManager.AddParameters(16, "@IDKONFIGGJENERUES", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(16, "@IDKONFIGGJENERUES", idKonfigGjenerues, ParameterDirection.Input);
            if (idDokGjenerues < 1) dbManager.AddParameters(17, "@IDGJENERUES", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(17, "@IDGJENERUES", idDokGjenerues, ParameterDirection.Input);
            if (idDokNga < 1) dbManager.AddParameters(18, "@IDDOKNGA", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(18, "@IDDOKNGA", idDokNga, ParameterDirection.Input);
            if (idLlogKunderparti < 1) dbManager.AddParameters(19, "@ID_LLOGKUNDERPARTI", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(19, "@ID_LLOGKUNDERPARTI", idLlogKunderparti, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "PRC_T_ASETE_SERIALxRIVLERESIM_KOKA_INSERT");
            idKokaRivlersim = int.Parse(dbManager.Parameters[0].Value.ToString());
            return new clsMesazh(true, "Ruatja perfundoi me sukses!");

        }

        /// <summary>
        /// MODULI ASETE:
        /// Fshirja e dokumentit te rivlersesimit duke i ndryshuar statuset ne te fshire (2) ne tebelen T_ASETE_SERIALxRIVLERESIM_KOKA nepermjet procedures PRC_T_ASETE_SERIALxRIVLERESIM_KOKA_UPDATEDELETE.
        /// </summary>
        /// <param name="idKokaRivlersim">(int) Id automatike e kokes se rivleresuar.</param>
        /// <param name="idPerdoruesi">(int) Id e perdoruesit qe ka modifikuar per here te fundit koken e rivlersimit.</param>
        /// <returns>Kthen clsMesazh me atribut statusi True nese modifikimi kryhet me sukses ose False dhe mesazhin e gabimit ne te kundert.</returns>
        internal clsMesazh modifikoRivleresimKokaStatus(int idKokaRivlersim, int idPerdoruesi)
        {

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDKOKARIVLERSIM", idKokaRivlersim, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUESI", idPerdoruesi, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "PRC_T_ASETE_SERIALxRIVLERESIM_KOKA_UPDATEDELETE");
            return new clsMesazh(true, "Ruatja perfundoi me sukses!");

        }

        /// <summary>
        /// kthen datarow koka dokumenti rivleresimi sipas idse se dokumentit gjenerues
        /// </summary>
        /// <param name="idGjenerues"> koka e dokumentit gjenerues te rivleresimit</param>
        /// <param name="lloj">lloji i dokumentit 1-hyrje 2-dalje</param>
        /// <returns>nje objekt datarow qe permban nje koleksion me te gjitha kokat e dokumentit te rivleresimit me kete id gjenerues </returns>
        /// <param name="idkonfiggjenerues"></param>
        internal DataRow ktheKokaRivleresimSipasIDGjenerues(int idGjenerues)
        {

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDGJENERUES", idGjenerues, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_ASETE_SERIALxRIVLERESIM_KOKA_merrSipasIdGjenerues");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];

        }

        #endregion

        #region Serialet per Rivleresim

        internal clsMesazh ruajSerialinERivleresuar(out int idSerialxRivleresim, int idKoka, int idSeriali, int idStandarti, double sasiaProgresive, double vleftaShtese, DateTime dataRivlersimi, int idNjesiAdministrative, int idNdermarrje, int idPerdoruesi, int idKrijuesi, int idStatusDokumenti)
        {
            idSerialxRivleresim = 0;

            dbManager.Open();
            dbManager.CreateParameters(12);
            dbManager.AddParameters(0, "@IDSERIALxSTANDART", idSerialxRivleresim, ParameterDirection.Output);
            dbManager.AddParameters(1, "@IDKOKARIVLERSIM", idKoka, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDSERIALI", idSeriali, ParameterDirection.Input);
            dbManager.AddParameters(3, "@IDSTANDARTI", idStandarti, ParameterDirection.Input);
            dbManager.AddParameters(4, "@SASIAPROGRESIVE", sasiaProgresive, ParameterDirection.Input);
            dbManager.AddParameters(5, "@VLEFTASHTESE", vleftaShtese, ParameterDirection.Input);
            dbManager.AddParameters(6, "@DATARIVLERESIM", dataRivlersimi, ParameterDirection.Input);
            dbManager.AddParameters(7, "@IDNJESIADMINISTRATIVE", idNjesiAdministrative, ParameterDirection.Input);
            dbManager.AddParameters(8, "@IDNDERMARRJE", idNdermarrje, ParameterDirection.Input);
            dbManager.AddParameters(9, "@IDPERDORUESI", idPerdoruesi, ParameterDirection.Input);
            dbManager.AddParameters(10, "@IDKRIJUESI", idKrijuesi, ParameterDirection.Input);
            dbManager.AddParameters(11, "@IDSTATUSDOK", idStatusDokumenti, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "PRC_T_ASETE_SERIALxRIVLERESIM_INSERT");
            idSerialxRivleresim = int.Parse(dbManager.Parameters[0].Value.ToString());
            return new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);

        }

        #endregion
    }
}
