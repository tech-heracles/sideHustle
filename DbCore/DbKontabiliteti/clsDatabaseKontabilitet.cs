using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Resources;
using AlphaWeb.Core.SharedKernel;
using DbCore.IMBUtils.DataBase;
using DbCore.IMBUtils.Logging;
using DbCore.IMBUtils.Messages;

namespace DbCore.DbKontabiliteti
{
    /// <remarks>
    ///  Kjo eshte klasa me e rendesishme e ketij moduli. Eshte nje klase e tille per secilin modul te projektit.
    ///  Ketu ndodhen metodat qe therriten  nga te gjithe objektet brenda projektit DbCore.DbRegjistrim
    ///  secila metode permban thirjet e Store procedures ne DB, duke i kaluar parametrat perkates
    ///  Metodat jane te ndara ne Regions sipas objekteve qe i therrasin keto metoda
    /// </remarks>
    public class clsDatabaseKontabilitet : DbData
    {

        public clsDatabaseKontabilitet()
            : base()
        {
        }
        public clsDatabaseKontabilitet(DbData db) : base(db) { }
        public clsDatabaseKontabilitet(string connectionName) : base(connectionName)
        {

        }


        /// <summary>
        /// Metodat e meposhtme i perkasin veprimeve qe kryhen nga objektet clsLlogari dhe colLlogarite
        /// </summary>
        #region LLOGARITE

        /// <summary>
        /// Ekzekuton prc_T_LLOGARI_ins per te ruajtur nje objekt clsLlogari ne DB.
        /// <param name="idLlog">id e llogarise</param>
        /// <param name="nrLlog">nr i llogarise</param>
        /// <param name="emerLlog1">emri i pare i llogarise</param>
        /// <param name="emerLlog2">ermri i dyte i llogarise </param>
        /// <param name="qenderkosto">qendra e kostos</param>
        /// <param name="kpf1">kpf1</param>
        /// <param name="kpf2">kpf2</param>
        /// <param name="kpf3">kpf3</param>
        /// <param name="niveltakse">niveli i takses</param>
        /// <param name="mon">id e monedhes</param>
        /// <param name="grup">grupi qe ben pjese llogaria</param>
        /// <param name="nengrup">nengrupi qe ben pjese llogaria</param>
        /// <param name="llogkonsoliduese">llogaria konsoliduese</param>
        /// <param name="llogkoresponduese">llogaria korresponduese</param>
        /// <param name="idndermarja">id e ndermarrjes</param>
        /// <param name="idndermvit">id qe lidh ndermarrjen me vitin</param>
        /// <param name="idperdoruesi">id e perdoruesit</param>
        /// <param name="idkonfig">id e konfigurimit</param>
        /// <returns> Kthen statusin e perfundimit te ekzekutimit te SP-se perkatese (nje objekt clsMesazh qe tregon nese veprimi eshte kryer me sukses apo jo.<seealso cref="DbCore.clsMesazh"/>)</returns>
        /// </summary>
        internal int ruajLlog(int idLlog, string nrLlog, string emerLlog1, string emerLlog2, string emerLlogFr, int qenderkosto, int kpf1, int kpf2, int kpf3, int niveltakse, int mon,
            int grup, int nengrup, int llogkonsoliduese, int llogkoresponduese, int idndermarja, int idperdoruesi, int idkonfig, int idstatusdok, int idobjektivakosto, int idskemaqendrakosto, int llojqendre, int idkategorishpenzimi,
            bool aktiv , string shenime1, string shenime2, string shenime3, string shenime4, string shenime5)
        {
            dbManager.Open();
            dbManager.CreateParameters(29);
            dbManager.AddParameters(0, "@IDLLOGARI", idLlog, ParameterDirection.Output);
            dbManager.AddParameters(1, "@NRLLOGARI", nrLlog, ParameterDirection.Input);
            dbManager.AddParameters(2, "@EMERLLOGARI_1", emerLlog1, ParameterDirection.Input);
            dbManager.AddParameters(3, "@EMERLLOGARI_2", emerLlog2, ParameterDirection.Input);
            if (qenderkosto == 0 || qenderkosto == -1) dbManager.AddParameters(4, "@QENDRA_KOSTOS", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(4, "@QENDRA_KOSTOS", qenderkosto, ParameterDirection.Input);
            if (kpf1 == 0) dbManager.AddParameters(5, "@KPF1", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(5, "@KPF1", kpf1, ParameterDirection.Input);
            if (kpf2 == 0) dbManager.AddParameters(6, "@KPF2", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(6, "@KPF2", kpf2, ParameterDirection.Input);
            if (kpf3 == 0) dbManager.AddParameters(7, "@KPF3", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(7, "@KPF3", kpf3, ParameterDirection.Input);
            if (niveltakse == 0) dbManager.AddParameters(8, "@NIVEL_TAKSE", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(8, "@NIVEL_TAKSE", niveltakse, ParameterDirection.Input);

            dbManager.AddParameters(9, "@MONEDHA", mon, ParameterDirection.Input);
            dbManager.AddParameters(10, "@GRUP ", grup, ParameterDirection.Input);
            dbManager.AddParameters(11, "@NENGRUP", nengrup, ParameterDirection.Input);
            if (llogkonsoliduese == 0 || llogkonsoliduese == -1) dbManager.AddParameters(12, "@LLOGARI_KOSOLIDUESE", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(12, "@LLOGARI_KOSOLIDUESE", llogkonsoliduese, ParameterDirection.Input);
            if (llogkoresponduese == 0 || llogkoresponduese == -1) dbManager.AddParameters(13, "@LLOGARI_KORESPONDUESE", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(13, "@LLOGARI_KORESPONDUESE", llogkoresponduese, ParameterDirection.Input);
            dbManager.AddParameters(14, "@IDNDERMARJE", idndermarja, ParameterDirection.Input);
            dbManager.AddParameters(15, "@IDPERDORUESI", idperdoruesi, ParameterDirection.Input);
            dbManager.AddParameters(16, "@IDKONFIG", idkonfig, ParameterDirection.Input);
            dbManager.AddParameters(17, "@IDSTATUSDOK", idstatusdok, ParameterDirection.Input);
            if (idobjektivakosto == 0 || idobjektivakosto == -1) dbManager.AddParameters(18, "@IDOBJEKTIVAKOSTO", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(18, "@IDOBJEKTIVAKOSTO", idobjektivakosto, ParameterDirection.Input);
            if (idskemaqendrakosto == 0 || idskemaqendrakosto == -1) dbManager.AddParameters(19, "@IDSKEMAQENDRAKOSTO", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(19, "@IDSKEMAQENDRAKOSTO", idskemaqendrakosto, ParameterDirection.Input);
            dbManager.AddParameters(20, "@LLOJQENDRE", llojqendre, ParameterDirection.Input);
            if (idkategorishpenzimi == 0 || idkategorishpenzimi == -1) dbManager.AddParameters(21, "@IDKATEGORISHPENZIMI", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(21, "@IDKATEGORISHPENZIMI", idkategorishpenzimi, ParameterDirection.Input);
            dbManager.AddParameters(22, "@AKTIV", aktiv, ParameterDirection.Input);
            dbManager.AddParameters(23, "@LLOGSHENIME1", shenime1, ParameterDirection.Input);
            dbManager.AddParameters(24, "@LLOGSHENIME2", shenime2, ParameterDirection.Input);
            dbManager.AddParameters(25, "@LLOGSHENIME3", shenime3, ParameterDirection.Input);
            dbManager.AddParameters(26, "@LLOGSHENIME4", shenime4, ParameterDirection.Input);
            dbManager.AddParameters(27, "@LLOGSHENIME5", shenime5, ParameterDirection.Input);
            dbManager.AddParameters(28, "@EMERLLOGARI_fr", emerLlogFr, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_LLOGARI_ins");
            idLlog = int.Parse(dbManager.Parameters[0].Value.ToString());
            return idLlog;

        }

        /// <summary>
        /// Ekzekuton prc_T_LLOGARI_upd per te modifikuar nje objekt clsLlogari ne DB.
        /// <param name="idLlog">id e llogarise</param>
        /// <param name="nrLlog">nr i llogarise</param>
        /// <param name="emerLlog1">emri i pare i llogarise</param>
        /// <param name="emerLlog2">ermri i dyte i llogarise </param>
        /// <param name="qenderkosto">qendra e kostos</param>
        /// <param name="kpf1">kpf1</param>
        /// <param name="kpf2">kpf2</param>
        /// <param name="kpf3">kpf3</param>
        /// <param name="niveltakse">niveli i takses</param>
        /// <param name="mon">id e monedhes</param>
        /// <param name="grup">grupi qe ben pjese llogaria</param>
        /// <param name="nengrup">nengrupi qe ben pjese llogaria</param>
        /// <param name="llogkonsoliduese">llogaria konsoliduese</param>
        /// <param name="llogkoresponduese">llogaria korresponduese</param>
        /// <param name="idndermarja">id e ndermarrjes</param>        
        /// <param name="idperdoruesi">id e perdoruesit</param>
        /// <param name="idkonfig">id e konfigurimit</param>
        /// <param name="idkategorishpenzimi"></param>
        /// <param name="idobjektivakosto"></param>
        /// <param name="idskemaqendrakosto"></param>
        /// <param name="idstatusdok"></param>
        /// <param name="llojqendre"></param>
        /// <returns> Kthen statusin e perfundimit te ekzekutimit te SP-se perkatese (true nese veprimi eshte kryer me sukses)</returns>
        /// </summary>
        internal clsMesazh modifikoLlogari(int idLlog, string nrLlog, string emerLlog1, string emerLlog2, string emerLlogFr, int qenderkosto, int kpf1, int kpf2, int kpf3, int niveltakse, int mon,
            int grup, int nengrup, int llogkonsoliduese, int llogkoresponduese, int idndermarja, int idperdoruesi, int idkonfig, int idstatusdok, int idobjektivakosto, int idskemaqendrakosto, int llojqendre, int idkategorishpenzimi, 
            bool aktiv , string shenime1, string shenime2, string shenime3, string shenime4, string shenime5 )
        {
            dbManager.Open();
            dbManager.CreateParameters(29);
            dbManager.AddParameters(0, "@IDLLOGARI", idLlog, ParameterDirection.Input);
            dbManager.AddParameters(1, "@NRLLOGARI", nrLlog, ParameterDirection.Input);
            dbManager.AddParameters(2, "@EMERLLOGARI_1", emerLlog1, ParameterDirection.Input);
            dbManager.AddParameters(3, "@EMERLLOGARI_2", emerLlog2, ParameterDirection.Input);
            if (qenderkosto == 0 || qenderkosto == -1) dbManager.AddParameters(4, "@QENDRA_KOSTOS", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(4, "@QENDRA_KOSTOS", qenderkosto, ParameterDirection.Input);
            if (kpf1 == 0) dbManager.AddParameters(5, "@KPF1", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(5, "@KPF1", kpf1, ParameterDirection.Input);
            if (kpf2 == 0) dbManager.AddParameters(6, "@KPF2", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(6, "@KPF2", kpf2, ParameterDirection.Input);
            if (kpf3 == 0) dbManager.AddParameters(7, "@KPF3", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(7, "@KPF3", kpf3, ParameterDirection.Input);
            if (niveltakse == 0) dbManager.AddParameters(8, "@NIVEL_TAKSE", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(8, "@NIVEL_TAKSE", niveltakse, ParameterDirection.Input);
            dbManager.AddParameters(9, "@MONEDHA", mon, ParameterDirection.Input);
            dbManager.AddParameters(10, "@GRUP ", grup, ParameterDirection.Input);
            dbManager.AddParameters(11, "@NENGRUP", nengrup, ParameterDirection.Input);
            if (llogkonsoliduese == 0 || llogkonsoliduese == -1) dbManager.AddParameters(12, "@LLOGARI_KOSOLIDUESE", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(12, "@LLOGARI_KOSOLIDUESE", llogkonsoliduese, ParameterDirection.Input);
            if (llogkoresponduese == 0 || llogkoresponduese == -1) dbManager.AddParameters(13, "@LLOGARI_KORESPONDUESE", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(13, "@LLOGARI_KORESPONDUESE", llogkoresponduese, ParameterDirection.Input);
            dbManager.AddParameters(14, "@IDNDERMARJE", idndermarja, ParameterDirection.Input);
            dbManager.AddParameters(15, "@IDPERDORUESI", idperdoruesi, ParameterDirection.Input);
            dbManager.AddParameters(16, "@IDKONFIG", idkonfig, ParameterDirection.Input);
            dbManager.AddParameters(17, "@IDSTATUSDOK", idstatusdok, ParameterDirection.Input);
            if (idobjektivakosto == 0 || idobjektivakosto == -1) dbManager.AddParameters(18, "@IDOBJEKTIVAKOSTO", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(18, "@IDOBJEKTIVAKOSTO", idobjektivakosto, ParameterDirection.Input);
            if (idskemaqendrakosto == 0 || idskemaqendrakosto == -1) dbManager.AddParameters(19, "@IDSKEMAQENDRAKOSTO", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(19, "@IDSKEMAQENDRAKOSTO", idskemaqendrakosto, ParameterDirection.Input);
            dbManager.AddParameters(20, "@LLOJQENDRE", llojqendre, ParameterDirection.Input);
            if (idkategorishpenzimi == 0 || idkategorishpenzimi == -1) dbManager.AddParameters(21, "@IDKATEGORISHPENZIMI", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(21, "@IDKATEGORISHPENZIMI", idkategorishpenzimi, ParameterDirection.Input);
            dbManager.AddParameters(22, "@AKTIV", aktiv, ParameterDirection.Input);
            dbManager.AddParameters(23, "@LLOGSHENIME1", shenime1, ParameterDirection.Input);
            dbManager.AddParameters(24, "@LLOGSHENIME2", shenime2, ParameterDirection.Input);
            dbManager.AddParameters(25, "@LLOGSHENIME3", shenime3, ParameterDirection.Input);
            dbManager.AddParameters(26, "@LLOGSHENIME4", shenime4, ParameterDirection.Input);
            dbManager.AddParameters(27, "@LLOGSHENIME5", shenime5, ParameterDirection.Input);
            dbManager.AddParameters(28, "@EMERLLOGARI_fr", emerLlogFr, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_LLOGARI_upd");
            return new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
        }

        /// <summary>
        /// Ekzekuton prc_T_LLOGARI_del per te fshire nje objekt clsLlogari ne DB.
        /// <param name="idLlog">id e llogarise</param>
        /// <returns> Kthen statusin e perfundimit te ekzekutimit te SP-se perkatese (true nese veprimi eshte kryer me sukses)</returns>
        /// </summary>
        internal clsMesazh fshiLlogari(int idLlog)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDLLOGARI", idLlog, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_LLOGARI_del");
            return new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
        }
        internal clsMesazh fshiLlogariStatus(int idLlog, int idperdorues)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDLLOGARI", idLlog, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUESI", idperdorues, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_LLOGARI_upddel");
            return new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
        }

        /// <summary>
        /// Ekzekuton prc_T_LLOGARI_merrLlogariSipasKodit per te marre nje int ne DB duke filtruar sipas numrit te llogarise dhe ID-se se ndermarrjes.
        /// <param name="nrLlogari">numri i llogarise</param>
        /// <param name="idNdermarja">id e ndermarrjes</param>
        /// <returns> Kthen nje int qe ploteson kushtet</returns>
        /// </summary>
        internal int ktheIDLlogariSipasKodit(string nrLlogari, int idNdermarja)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@NRLLOGARI", nrLlogari, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idNdermarja, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_LLOGARI_merrLlogariSipasKodit");

            if (ds == null)
                return -1;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return -1;
            int idLlogari;
            int.TryParse(ds.Tables[0].Rows[0]["IDLLOGARI"].ToString(), out idLlogari);
            return idLlogari;
        }

        internal int ktheNivelTakseSipasIdLlogari(int idLlogari)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDLLOGARI", idLlogari, ParameterDirection.Input);
            return Convert.ToInt32(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_LLOGARI_ktheNivelTakse"));
        }

        internal int ktheNivelTakseSipasNrLlogari(string nrLlogari, int idNdermarrje)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@NRLLOGARI", nrLlogari, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idNdermarrje, ParameterDirection.Input);
            return Convert.ToInt32(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_LLOGARI_ktheNivelTakseSipasKodit"));
        }

        /// <summary>
        /// Ekzekuton prc_T_LLOGARI_merrLlogariSipasKodit per te marre nje int ne DB duke filtruar sipas numrit te llogarise dhe ID-se se ndermarrjes.
        /// <param name="nrLlogari">numri i llogarise</param>
        /// <param name="idNdermarja">id e ndermarrjes</param>
        /// <returns> Kthen nje int qe ploteson kushtet</returns>
        /// </summary>
        internal DataRow ktheLlogariSipasKodit(string nrLlogari, int idNdermarja)
        {

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@NRLLOGARI", nrLlogari, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idNdermarja, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_LLOGARI_merrLlogariSipasKodit");

            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];


        }

        /// <summary>
        /// Ekzekuton prc_T_LLOGARI_merrLlogariAktiveSipasKodit per te marre llogarine sipas kodit dhe ndermarrjes nqs ajo eshte aktive
        /// <param name="nrLlogari">numri i llogarise</param>
        /// <param name="idNdermarja">id e ndermarrjes</param>
        /// <returns> Kthen llogarine sipas kodit</returns>
        /// </summary>
        internal DataRow ktheLlogariAktiveSipasKodit(string nrLlogari, int idNdermarja)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@NRLLOGARI", nrLlogari, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idNdermarja, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_LLOGARI_merrLlogariAktiveSipasKodit");

            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];
        }
        /// <summary>
        /// Ekzekuton prc_T_LLOGARI_ktheLlogari per te marre nje objekt clsLlogari ne DB duke filtruar sipas ID-se se llogarise.
        /// <param name="id">Id e llogarise</param>
        /// <returns> Kthen nje objekt clsLlogari me ID sa ID-ja qe kalohet si parameter</returns>
        /// </summary>
        internal DataRow merrLlogari(int id)
        {

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDLLOGARI", id, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_LLOGARI_ktheLlogari");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];

        }

        /// <summary>
        /// Ekzekuton prc_T_LLOGARI_ktheLlogariSipasIdNenLlojLlog per te marre nje objekt clsLlogari ne DB duke filtruar sipas ID-se se nenllojit te llogarise dhe id se artikullit.
        ///<param name="idNenLlojLlogarie"></param>
        ///<param name="idArtikulli"></param>
        /// <returns> Kthen nje objekt clsLlogari</returns>
        /// </summary>
        internal DataRow merrLlogariSipasIdNenLlojLlog(int idNenLlojLlogarie, int idArtikulli)
        {

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDNENLLOJLLOGARIE", idNenLlojLlogarie, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDARTIKULLI", idArtikulli, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_LLOGARI_ktheLlogariSipasIdNenLlojLlog");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];
        }

        /// <summary>
        /// Ekzekuton prc_T_LLOGARI_ekzistonLlogari per te kontrolluar nese nje objekt clsLlogari gjendet DB duke filtruar sipas numrit te llogarise dhe ID-se se ndermarrjes.
        ///<param name="nrLlog">Numri i llogarise</param>
        /// <param name="idndermarje">Id e ndermarrjes</param>
        /// <returns> Kthen true nese ekziston objekti me keto te dhena</returns>
        /// </summary>
        internal bool ekzistonLlogari(String nrLlog, int idndermarje)
        {//kontrollon nqs ekziston nje llogari me kete numer

            dbManager.Open();
            dbManager.CreateParameters(2);
            if (nrLlog == null)
                dbManager.AddParameters(0, "@NRLLOGARI", "", ParameterDirection.Input);
            else
                dbManager.AddParameters(0, "@NRLLOGARI", nrLlog, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idndermarje, ParameterDirection.Input);

            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_LLOGARI_ekzistonLlogari");
            if (ds.Tables[0].Rows.Count == 1)
                return true;
            else if (ds.Tables[0].Rows.Count == 0)
                return false;
            else return true;

        }

        /// <summary>
        /// Kontrollon nqs llogaria me kete numer per kete ndermarrje eshte aktive
        ///<param name="nrLlog">Numri i llogarise</param>
        /// <param name="idndermarje">Id e ndermarrjes</param>
        /// <returns> Kthen true nese llogaria eshte aktive</returns>
        /// </summary>
        internal bool eshteLlogariAktive(String nrLlog, int idndermarje)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@NRLLOGARI", nrLlog, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idndermarje, ParameterDirection.Input);
            bool aktive = Convert.ToBoolean(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_LLOGARI_eshteLlogariAktive"));
            return aktive;
        }

        public bool ekzistonLlogariSipasID(int idLlogari)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDLLOGARI", idLlogari, ParameterDirection.Input);
            int pergjigje = Convert.ToInt32(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_LLOGARI_ekzistonLlogariSipasID"));
            return Convert.ToBoolean(pergjigje);
        }

        internal double merrGjendjeLlogari(int idllogari, DateTime date)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDLLOGARI", idllogari, ParameterDirection.Input);
            dbManager.AddParameters(1, "@DATE", date, ParameterDirection.Input);

            double gjendja = Convert.ToDouble(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_LLOGARI_merrGjendjeLlogarie"));
            return gjendja;
        }
        internal double merrGjendjeLlogariMonHuaj(int idllogari, DateTime date)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDLLOGARI", idllogari, ParameterDirection.Input);
            dbManager.AddParameters(1, "@DATE", date, ParameterDirection.Input);

            double gjendja = Convert.ToDouble(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_LLOGARI_merrGjendjeLlogarieMonHuaj"));
            return gjendja;
        }
        /// <summary>
        /// Ekzekuton prc_T_LLOGARI_merrLlogariteNgaKPF per te marre nje datatable ne DB duke filtruar sipas KPF-ve qe i jane caktuar llogarise.
        /// <param name="idKPF">Id e KPF</param>
        /// <returns> Kthen nje datatable me objekte clsLlogari</returns>
        /// </summary>
        internal DataTable ktheLlogariteNgaKPF(int idKPF)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDKPF", idKPF, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_LLOGARI_merrLlogariteNgaKPF");
            return ds.Tables[0];
        }
        /// <summary>
        /// Ekzekuton prc_T_LLOGARI_merrLlogariteNgaKPFlIKE per te marre nje datatable ne DB duke filtruar sipas KPF-ve qe i jane caktuar llogarise.
        /// <param name="idKPF">Id e KPF</param>
        /// <returns> Kthen nje datatable me objekte clsLlogari</returns>
        /// </summary>
        internal DataTable ktheLlogariteNgaKPFLike(string kodikpf, int idndermarje)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@KODIKPF", kodikpf, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idndermarje, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_LLOGARI_merrLlogariteNgaKPFLike");
            return ds.Tables[0];
        }

        /// <summary>
        /// Ekzekuton prc_T_LLOGARI_merrLlogariteNdermarrjes per te marre nje datatable ne DB duke filtruar sipas ID-se se ndermarrjes.
        /// <param name="idNdermarje">Id e ndermarrjes</param>
        /// <returns> Kthen nje datatable me objekte clsLlogari</returns>
        /// </summary>
        internal DataTable ktheLLogariteNdermarrjes(int idNdermarje)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDNDERMARJE", idNdermarje, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_LLOGARI_merrLlogariteNdermarrjes");
            return ds.Tables[0];
        }
        internal DataTable ktheLLogariteNdermarrjesTeMundshmePerQK(int idNdermarje)
        {

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDNDERMARJE", idNdermarje, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_LLOGARI_merrLlogariteNdermarrjesTeMundshmePerQK");
            return ds.Tables[0];

        }

        /// <summary>
        /// Ekzekuton prc_T_LLOGARI_merrLLogariteAzhornimit per te marre nje datatable me objekte clsLlogari ne DB duke filtruar sipas ID-se se ndermarrjes, ID-se lidhese ndermarrje - vit dhe ID-se se monedhes.
        /// <param name="idNdermarje">Id e ndermarrjes</param>
        /// <param name="idNdermarrjeVit">Id lidhese ndermarrje - vit</param>
        /// <param name="idMonedha">Id e monedhes (monedha e ndermarrjes)</param>
        /// <returns> Kthen nje datatable me objekte clsLlogari</returns>
        /// </summary>
        internal DataTable ktheLLogariteAzhornimit(int idNdermarje, int idMonedha)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDNDERMARJE", idNdermarje, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDMONEDHA", idMonedha, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_LLOGARI_merrLLogariteAzhornimit");
            return ds.Tables[0];
        }

        /// <summary>
        /// Ekzekuton prc_T_LLOGARI_merrGjendjetLlogariveAzhornimit per te marre nje collection me objekte clsLlogariKontabiliteti ne DB duke filtruar sipas ID-se se ndermarrjes, ID-se lidhese ndermarrje - vit, ID-se se monedhes dhe id-ve te llogarive.
        /// <param name="idNdermarje">Id e ndermarrjes</param>
        /// <param name="idNdermarrjeVit">Id lidhese ndermarrje - vit</param>
        /// <param name="idMonedha">Id e monedhes (monedha e ndermarrjes)</param>
        /// <param name="idLlogarive">Id-te e llogarive te cilave do tu merren vlerat per te llogaritur gjendjen</param>
        /// <returns> Kthen nje collection me objekte clsLlogariKontabiliteti</returns>
        /// </summary>
      
        public DataTable merrLlogFitimHumbjeKlientit(int idNdermarje, int idMonedha, int idLlogKlienti)
        {
            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@IDNDERMARJE", idNdermarje, ParameterDirection.Input);
            //dbManager.AddParameters(1, "@IDNDERVITI", idNdermarrjeVit, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDMONEDHA", idMonedha, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDLLOGARI", idLlogKlienti, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_LLOGARI_merrLlogFitimHumbjeKlientit");
            return ds.Tables[0];

        }

        /// <summary>
        /// Ekzekuton prc_T_LLOGARI_merrLlogariteNdermarrjesAndAutorizime per te marre nje datatable ne DB duke filtruar sipas ID-se se ndermarrjes dhe ID-se se perdoruesit (sipas autorizimeve).
        /// <param name="idNdermarje">Id e ndermarrjes</param>
        /// <param name="idperdorues">Id e perdoruesit</param>
        /// <returns> Kthen nje datatable me objekte clsLlogari</returns>
        /// </summary>
        internal DataTable ktheLLogariteNdermarrjesAndAutorizime(int idNdermarje, int idperdorues)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDNDERMARJE", idNdermarje, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUES", idperdorues, ParameterDirection.Input);

            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_LLOGARI_merrLlogariteNdermarrjesAndAutorizime");
            return ds.Tables[0];
        }
        internal DataTable ktheLLogariteNdermarrjesAndAutorizimePerAzhornim(int idNdermarje, int idperdorues)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDNDERMARJE", idNdermarje, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUES", idperdorues, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_LLOGARI_merrLlogariteNdermarrjesAndAutorizimePerAzhornim");
            return ds.Tables[0];
        }
        internal DataTable merrLlogariteLikeKodOsePershkDT(int idNdermarrje, int idPerdoruesi, string likeKodOsePershk, int pershk, string klasa)
        {
            dbManager.Open();
            dbManager.CreateParameters(5);
            dbManager.AddParameters(0, "@IDNDERMARJE", idNdermarrje, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUES", idPerdoruesi, ParameterDirection.Input);
            dbManager.AddParameters(2, "@KODPERSHKLLOG", likeKodOsePershk, ParameterDirection.Input);
            dbManager.AddParameters(3, "@PERSHK", pershk, ParameterDirection.Input);
            dbManager.AddParameters(4, "@KLASA", klasa, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_LLOGARI_merrLlogariteLikeKodPershkDT");
            return ds.Tables[0];
        }

        internal DataTable merrLlogariteLikeKodOsePershk(int idNder, int idperdorues, string kodPershkLlog)
        {
            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@IDNDERMARJE", idNder, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUES", idperdorues, ParameterDirection.Input);
            dbManager.AddParameters(2, "@KODPERSHKLLOG", kodPershkLlog, ParameterDirection.Input);

            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_LLOGARI_merrLlogariteLikeKodPershk");
            return ds.Tables[0];
        }
        internal DataTable merrLlogariteLikeKodOsePershkAng(int idNder, int idperdorues, string kodPershkLlog)
        {
            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@IDNDERMARJE", idNder, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUES", idperdorues, ParameterDirection.Input);
            dbManager.AddParameters(2, "@KODPERSHKLLOG", kodPershkLlog, ParameterDirection.Input);

            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_LLOGARI_merrLlogariteLikeKodPershkAng");
            return ds.Tables[0];
        }
        /// <summary>
        /// Ekzekuton prc_T_LLOGARI_merrLlogariteNdermarrjesAndAutorizimeLike per te marre nje collection me objekte clsLlogari ne DB duke filtruar sipas ID-se se ndermarrjes, ID-se se perdoruesit (sipas autorizimeve) dhe numrit te llogarise.
        /// <param name="idNdermarje">Id e ndermarrjes</param>
        /// <param name="idperdorues">Id e perdoruesit</param>
        /// <param name="nrLlogarie">Numri i llogarise (perdoret me "like" ne DB)</param>
        /// <returns> Kthen nje collection me objekte clsLlogari</returns>
        /// </summary>
        internal DataTable ktheLLogariteNdermarrjesAndAutorizimeLike(int idNdermarje, int idperdorues, string nrLlogarie)
        {
            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@IDNDERMARJE", idNdermarje, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUES", idperdorues, ParameterDirection.Input);
            dbManager.AddParameters(2, "@NRLLOGARI", nrLlogarie, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_LLOGARI_merrLlogariteNdermarrjesAndAutorizimeLikeCol");
            return ds.Tables[0];
        }

        /// <summary>
        /// Ekzekuton prc_T_LLOGARI_kaKPF per te kontrolluar nese ekziston nje objekt clsLlogari i lidhur me nje KPF te caktuar.
        ///<param name="idKPF">ID e KPF-se</param>
        /// <returns> Kthen true nese ekziston nje objekt qe ploteson kushtin</returns>
        /// </summary>
        public bool kaKPF(int idKPF)
        {//kontrollon nqs ka llogari kjo kpf

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDKPF", idKPF, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_LLOGARI_kaKPF");
            if (ds.Tables[0].Rows.Count >= 1)
                return true;
            else if (ds.Tables[0].Rows.Count == 0)
                return false;
            else return true;
        }

        /// <summary>
        /// Ekzekuton nje transaksion per te ruajtur nje objekt clsLlogari dhe collection-in e buxheteve te lidhur me te.
        /// Therret funksionin <see cref="DbCore.DbKontabiliteti.clsDatabaseKontabilitet.ruajLlogari"/>
        /// Therret funksionin <see cref="DbCore.DbKontabiliteti.clsDatabaseKontabiltet.ruajBuxhet"/>
        /// Therret funksionin <see cref="DbAdmin.clsDatabaseAdmin.ruajLidhjeAutorizim"/>
        /// Therret funksionin <see cref="DbAdmin.clsDatabaseAdmin.ruajVlera"/>        
        /// <param name="llogari">Objekt i tipit clsLlogari i cili po ruhet</param>
        /// <returns> Kthen statusin e perfundimit te ekzekutimit te SP-se perkatese (nje objekt clsMesazh qe tregon nese veprimi eshte kryer me sukses apo jo.<seealso cref="DbCore.clsMesazh"/>)</returns>
        /// </summary>


        /// <summary>
        /// Ekzekuton nje transaksion per te modifikuar nje objekt clsLlogari dhe collection-in e buxheteve te lidhur me te.
        /// Therret funksionin <see cref="DbCore.DbKontabiliteti.clsDatabaseKontabilitet.modifikoLlogari"/>
        /// Therret funksionin <see cref="DbCore.DbKontabiliteti.clsDatabaseKontabiltet.modifikoBuxhet"/>
        /// Therret funksionin <see cref="DbAdmin.clsDatabaseAdmin.ruajLidhjeAutorizim"/> ose <see cref="DbAdmin.clsDatabaseAdmin.modifikoLidhjeAutorizim"/> ose <see cref="DbAdmin.clsDatabaseAdmin.fshiLidhjeAutorizim"/> sipas rastit nese shtohen apo fshihen rreshta trupi
        /// Therret funksionin <see cref="DbAdmin.clsDatabaseAdmin.ruajVlera"/>  ose <see cref="DbAdmin.clsDatabaseAdmin.modifikoVlera"/>  ose <see cref="DbAdmin.clsDatabaseAdmin.fshiVlera"/> ipas rastit nese shtohen apo fshihen rreshta trupi
        /// <param name="llogari">Objekt i tipit clsLlogari i cili po ruhet</param>
        /// <returns> Kthen statusin e perfundimit te ekzekutimit te SP-se perkatese (nje objekt clsMesazh qe tregon nese veprimi eshte kryer me sukses apo jo.<seealso cref="DbCore.clsMesazh"/>)</returns>
        /// </summary>


        /// <summary>
        /// Ekzekuton prc_viewLLOGARITESIPASTIPIT_KontrolloTipin per te kontrolluar nese nje objekt clsLlogari eshte llogari klienti apo jo duke filtruar sipas ID-se se llogarise dhe tipit.
        /// <param name="idLlogarie">Id e llogarise</param>
        /// <returns> Kthen statusin e perfundimit te ekzekutimit te SP-se perkatese (nje objekt clsMesazh qe tregon nese llogaria eshte llogari klient apo jo.<seealso cref="DbCore.clsMesazh"/>)</returns>
        /// </summary>
        public clsMesazh eshteLlogariKlienti(string idLlogarie)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDLLOGARI", idLlogarie, ParameterDirection.Input);
            dbManager.AddParameters(1, "@TIPI", TipeLlogarish.Klient, ParameterDirection.Input);
            int nrRreshtash = (int)dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_viewLLOGARITESIPASTIPIT_KontrolloTipin");
            if (nrRreshtash > 0)
            {
                return new clsMesazh(true, "Llogari Klienti");
            }
            else if (nrRreshtash == 0)
            {
                return new clsMesazh(false, "Nuk eshte Llogari Klienti");
            }
            else
            {
                return new clsMesazh(true, "Ndodhi nje gabim i pa parashikuar. Nuk u gjet dot tipi i Llogarise!");
            }
        }

        /// <summary>
        /// Ekzekuton prc_viewLLOGARITESIPASTIPIT_KontrolloTipin per te kontrolluar nese nje objekt clsLlogari eshte llogari banke apo jo duke filtruar sipas ID-se se llogarise dhe tipit.
        /// <param name="idLlogarie">Id e llogarise</param>
        /// <returns> Kthen statusin e perfundimit te ekzekutimit te SP-se perkatese (nje objekt clsMesazh qe tregon nese llogaria eshte llogari banke apo jo.<seealso cref="DbCore.clsMesazh"/>)</returns>
        /// </summary>
        public clsMesazh eshteLlogariBanke(string idLlogarie)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDLLOGARI", idLlogarie, ParameterDirection.Input);
            dbManager.AddParameters(1, "@TIPI", TipeLlogarish.Banke, ParameterDirection.Input);
            int nrRreshtash = (int)dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_viewLLOGARITESIPASTIPIT_KontrolloTipin");
            if (nrRreshtash > 0)
            {
                return new clsMesazh(true, "Llogari Banke");
            }
            else if (nrRreshtash == 0)
            {
                return new clsMesazh(false, "Nuk eshte Llogari Banke");
            }
            else
            {
                return new clsMesazh(true, "Ndodhi nje gabim i pa parashikuar. Nuk u gjet dot tipi i Llogarise!");
            }
        }

        /// <summary>
        /// Ekzekuton prc_T_LLOGARI_ekzistonListaLlogarieve per te kontrolluar nese ekziston lista e llogarive duke filtruar sipas numrave te llogarive, numrit total te llogarive dhe ID-se se ndermarrjes.
        /// <param name="nrllogarish">Numri i llogarive</param>
        /// <param name="listellogarish">Liste me numrat e llogarive</param>
        /// <param name="idNdermarje">Id e ndermarrjes</param>
        /// <returns> Kthen statusin e perfundimit te ekzekutimit te SP-se perkatese (nje objekt clsMesazh qe tregon nese ekziston lista e llogarive apo jo.<seealso cref="DbCore.clsMesazh"/>)</returns>
        /// </summary>
        public clsMesazh ekzistonListeLlogarish(string listellogarish, int idNdermarje, int nrllogarish)
        {
            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@COUNT", nrllogarish, ParameterDirection.Input);
            dbManager.AddParameters(1, "@LISTALLOGARIVE", listellogarish, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDNDERMARJE", idNdermarje, ParameterDirection.Input);
            int nrRreshtash = (int)dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_LLOGARI_ekzistonListaLlogarieve");
            if (nrRreshtash == 1)
            {
                return new clsMesazh(true, "Te gjitha llogarite ekzistojne");
            }
            else if (nrRreshtash == 0)
            {
                return new clsMesazh(false, "Te pakten njera llogari nuk ekziston");
            }
            else
            {
                return new clsMesazh(false, "Ndodhi nje gabim i pa parashikuar.Nuk u kontrollua dot nese ekzistojne te gjitha llogarite e zgjedhura!");
            }
        }
        internal DataRow ktheLlogariNdermarrjesAndAutorizimeDR(int idnderm, int idperdorues, int idllogari, int idGjuha)
        {
            dbManager.Open();
            dbManager.CreateParameters(4);
            dbManager.AddParameters(0, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUES", idperdorues, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDLLOGARI", idllogari, ParameterDirection.Input);
            dbManager.AddParameters(3, "@IDGJUHA", idGjuha, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_LLOGARI_merrLlogariSipasNdermarjesAndAutorizimDR");

            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];
        }

        internal DataTable ktheLlogariNdermarrjesAndAutorizimeDT(int idnderm, int idperdorues, int idGjuha)
        {
            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUES", idperdorues, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDGJUHA", idGjuha, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_LLOGARI_merrLlogariSipasNdermarjesAndAutorizimDT");
            return ds.Tables[0];
        }

        internal DataTable ktheLlogariSipasNdermarjesAndAutorizimExport(int idnderm, int idperdorues, int lloji, string emerTabKoka, string emerFusheID)
        {
            dbManager.Open();
            dbManager.CreateParameters(5);
            dbManager.AddParameters(0, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUES", idperdorues, ParameterDirection.Input);
            dbManager.AddParameters(2, "@LLOJI", lloji, ParameterDirection.Input);
            dbManager.AddParameters(3, "@EMERTABKOKA", emerTabKoka, ParameterDirection.Input);
            dbManager.AddParameters(4, "@EMERFUSHEID", emerFusheID, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_LLOGARI_merrLlogariSipasNdermarjesAndAutorizimExport");
            return ds.Tables[0];
        }

        internal DataTable ktheLlogariNdermarrjesAndAutorizimeAktivDT(int idnderm, int idperdorues)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUES", idperdorues, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_LLOGARI_merrLlogariSipasNdermarjesAndAutorizimAktivDT");
            return ds.Tables[0];
        }
        internal DataTable ktheLlogariNdermarrjesAndAutorizimeAktivDTRaportuese(int idnderm, int idperdorues)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUES", idperdorues, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_LLOGARI_merrLlogariSipasNdermarjesAndAutorizimAktivDTRaportuese");
            return ds.Tables[0];
        }
        internal DataTable ktheLlogariNdermarrjesAndAutorizimeDTDheKokaFletekontabelPerQK(int idnderm, int idperdorues, int idkokafletekontabel)
        {
            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUES", idperdorues, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDKOKAFLETEKONTABEL", idkokafletekontabel, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_LLOGARI_merrLlogariSipasNdermarjesAndAutorizimDTDheKokaFletekontabelPerQK");

            return ds.Tables[0];
        }
        internal DataTable ktheLlogariNdermarrjesAndAutorizimeDTTeMundshmePerQK(int idnderm, int idperdorues)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUES", idperdorues, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_LLOGARI_merrLlogariSipasNdermarjesAndAutorizimDTTeMundshmePerQK");
            return ds.Tables[0];
        }
        internal DataTable ktheLlogariTeArdhuraDheShpenzime(int idnderm)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_LLOGARI_merrLlogariSipasNdermarjesTeArdhuraDheShpenzime");

            return ds.Tables[0];
        }

        public DataTable merrLlogariFature(int idDok)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@idDok", idDok, ParameterDirection.Input);
            return dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_LLOGARI_merrLlogariSipasIdDok").Tables[0];
        }
        public DataTable merrLlogariSipasIdve(System.Collections.Generic.List<int> idLlogarish)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@idLlogarish", String.Join(",", idLlogarish), ParameterDirection.Input);
            return dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_LLOGARI_merrLlogariSipasIdVe").Tables[0];
        }
        public DataTable merrNrLlogariSipasIdLlogarive(System.Collections.Generic.List<int> idLlogarish)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@idLlogarish", String.Join(",", idLlogarish), ParameterDirection.Input);
            return dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_LLOGARI_merrNrLlogariSipasIdLlogarive").Tables[0];
        }
        
        #endregion

        /// <summary>
        /// Metodat e meposhtme i perkasin veprimeve qe kryhen nga objektet clsGrupiLlogaria dhe colGrupetLlogaria
        /// </summary>
        #region GRUPET E LLOGARIVE

        /// <summary>
        /// Ekzekuton prc_T_GRUPILLOGARIA_ins per te ruajtur nje objekt clsGrupiLlogaria ne DB.
        /// <param name="idgrupiLlogaria">Id e grupit te llogarise</param>
        /// <param name="nrgrupiLlogaria">Numri i grupit te llogarise</param>
        /// <param name="pershkrimigrupiLlogaria">Pershkrimi i grupit te llogarise</param>
        /// <param name="idndermarje">Id e ndermarrjes</param>
        /// <returns> Kthen statusin e perfundimit te ekzekutimit te SP-se perkatese (nje objekt clsMesazh qe tregon nese veprimi eshte kryer me sukses apo jo.<seealso cref="DbCore.clsMesazh"/>)</returns>
        /// </summary>
        public clsMesazh ruajGrupLlogaria(out int idgrupiLlogaria, int nrgrupiLlogaria, String pershkrimigrupiLlogaria, int idndermarje)
        {
            idgrupiLlogaria = -1;

            dbManager.Open();
            dbManager.CreateParameters(4);
            dbManager.AddParameters(0, "@IDGRUPILLOGARIA", idgrupiLlogaria, ParameterDirection.Output);
            dbManager.AddParameters(1, "@NRGRUPILLOGARIA", nrgrupiLlogaria, ParameterDirection.Input);
            dbManager.AddParameters(2, "@PERSHKGRUPILLOGARIA", pershkrimigrupiLlogaria, ParameterDirection.Input);
            dbManager.AddParameters(3, "@IDNDERMARJE", idndermarje, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_GRUPILLOGARIA_ins");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
            return mesazh;
        }
        //[Obsolete("Perdor: clsMesazh ruajGrupLlogaria(int idgrupiLlogaria, int nrgrupiLlogaria, String pershkrimigrupiLlogaria, int idndermarje)", true)]
        //public clsMesazh ruajGrupLlogaria(clsGrupiLlogaria grupiLlogaria)
        //{
        //    IDBManager dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        //    dbManager.ConnectionString = dbManager.GetConnectionString();
        //    try
        //    {
        //        dbManager.Open();
        //        dbManager.CreateParameters(4);
        //        dbManager.AddParameters(0, "@IDGRUPILLOGARIA", grupiLlogaria.IdGrupiLlogaria, ParameterDirection.Output);
        //        dbManager.AddParameters(1, "@NRGRUPILLOGARIA", grupiLlogaria.NrGrupiLlogaria, ParameterDirection.Input);
        //        dbManager.AddParameters(2, "@PERSHKGRUPILLOGARIA", grupiLlogaria.PershkrimiGrupiLlogaria, ParameterDirection.Input);
        //        dbManager.AddParameters(3, "@IDNDERMARJE", grupiLlogaria.IdNdermarje, ParameterDirection.Input);
        //        dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_GRUPILLOGARIA_ins");
        //        clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
        //        return mesazh;
        //    }
        //    catch (Exception)
        //    {
        //        return new clsMesazh(false, ce.Message);
        //    }
        //    finally
        //    {
        //        dbManager.Dispose();
        //    }

        //}

        /// <summary>
        /// Ekzekuton prc_T_GRUPILLOGARIA_upd per te modifikuar nje objekt clsGrupiLlogaria ne DB.
        /// <param name="idgrupiLlogaria">Id e grupit te llogarise</param>
        /// <param name="nrgrupiLlogaria">Numri i grupit te llogarise</param>
        /// <param name="pershkrimigrupiLlogaria">Pershkrimi i grupit te llogarise</param>
        /// <param name="idndermarje">Id e ndermarrjes</param>
        /// <returns> Kthen statusin e perfundimit te ekzekutimit te SP-se perkatese (nje objekt clsMesazh qe tregon nese veprimi eshte kryer me sukses apo jo.<seealso cref="DbCore.clsMesazh"/>)</returns>
        /// </summary>
        public clsMesazh modifikoGrupLlogaria(int idgrupiLlogaria, int nrgrupiLlogaria, String pershkrimigrupiLlogaria, int idndermarje)
        {
            dbManager.Open();
            dbManager.CreateParameters(4);
            dbManager.AddParameters(0, "@IDGRUPILLOGARIA", idgrupiLlogaria, ParameterDirection.Input);
            dbManager.AddParameters(1, "@NRGRUPILLOGARIA", nrgrupiLlogaria, ParameterDirection.Input);
            dbManager.AddParameters(2, "@PERSHKGRUPILLOGARIA", pershkrimigrupiLlogaria, ParameterDirection.Input);
            dbManager.AddParameters(3, "@IDNDERMARJE", idndermarje, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_GRUPILLOGARIA_upd");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgModifikimiMeSukses"]);
            return mesazh;
        }
        //[Obsolete("Perdor: clsMesazh modifikoGrupLlogaria(int idgrupiLlogaria, int nrgrupiLlogaria, String pershkrimigrupiLlogaria, int idndermarje)", true)]
        //public clsMesazh modifikoGrupLlogaria(clsGrupiLlogaria grupiLlogaria)
        //{
        //    IDBManager dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        //    dbManager.ConnectionString = dbManager.GetConnectionString();
        //    try
        //    {
        //        dbManager.Open();
        //        dbManager.CreateParameters(4);
        //        dbManager.AddParameters(0, "@IDGRUPILLOGARIA", grupiLlogaria.IdGrupiLlogaria, ParameterDirection.Input);
        //        dbManager.AddParameters(1, "@NRGRUPILLOGARIA", grupiLlogaria.NrGrupiLlogaria, ParameterDirection.Input);
        //        dbManager.AddParameters(2, "@PERSHKGRUPILLOGARIA", grupiLlogaria.PershkrimiGrupiLlogaria, ParameterDirection.Input);
        //        dbManager.AddParameters(3, "@IDNDERMARJE", grupiLlogaria.IdNdermarje, ParameterDirection.Input);
        //        dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_GRUPILLOGARIA_upd");
        //        clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgModifikimiMeSukses"]);
        //        return mesazh;
        //    }
        //    catch (Exception)
        //    {
        //        return new clsMesazh(false, ce.Message);
        //    }
        //    finally
        //    {
        //        dbManager.Dispose();
        //    }
        //}

        /// <summary>
        /// Ekzekuton prc_T_GRUPILLOGARIA_del per te fshire nje objekt clsGrupiLlogaria ne DB.
        /// <param name="idgrupiLlogaria">Id e grupit te llogarise</param>
        /// <returns> Kthen statusin e perfundimit te ekzekutimit te SP-se perkatese (nje objekt clsMesazh qe tregon nese veprimi eshte kryer me sukses apo jo.<seealso cref="DbCore.clsMesazh"/>)</returns>
        /// </summary>
        public clsMesazh fshiGrupLlogaria(int idgrupiLlogaria)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDGRUPILLOGARIA", idgrupiLlogaria, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_GRUPILLOGARIA_del");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
            return mesazh;
        }
        //[Obsolete("Perdor: clsMesazh fshiGrupLlogaria(int idgrupiLlogaria)", true)]
        //public clsMesazh fshiGrupLlogaria(clsGrupiLlogaria grupiLlogaria)
        //{
        //    IDBManager dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        //    dbManager.ConnectionString = dbManager.GetConnectionString();
        //    try
        //    {
        //        dbManager.Open();
        //        dbManager.CreateParameters(1);
        //        dbManager.AddParameters(0, "@IDGRUPILLOGARIA", grupiLlogaria.IdGrupiLlogaria, ParameterDirection.Input);
        //        dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_GRUPILLOGARIA_del");
        //        clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
        //        return mesazh;
        //    }
        //    catch (Exception)
        //    {
        //        return new clsMesazh(false, ce.Message);
        //    }
        //    finally
        //    {
        //        dbManager.Dispose();
        //    }

        //}

        /// <summary>
        /// Ekzekuton prc_T_GRUPILLOGARIA_sel per te marre nje objekt datarow nga DB.
        /// <param name="idgrupi">Id e grupit te llogarise</param>
        /// </summary>
        public DataRow ktheGrupLlogaria(int idgrupi, int idGjuha)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDGRUPILLOGARIA", idgrupi, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDGJUHA", idGjuha, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_GRUPILLOGARIA_sel");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];
        }
        //[Obsolete("Perdor: DataRow merrGrupLlogaria(int idgrupi)", true)]
        //public colGrupetLlogaria merrGrupLlogaria(int idgrupi)
        //{
        //    IDBManager dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        //    dbManager.ConnectionString = dbManager.GetConnectionString();
        //    try
        //    {
        //        dbManager.Open();
        //        dbManager.CreateParameters(1);
        //        dbManager.AddParameters(0, "@IDGRUPILLOGARIA", idgrupi, ParameterDirection.Input);
        //        DataSet ds =  dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_GRUPILLOGARIA_sel");
        //        colGrupetLlogaria grupet = new colGrupetLlogaria();
        //        return grupet.mbushArrayListGrupetLlogaria(ds);
        //    }
        //    catch (Exception)
        //    {
        //        return new colGrupetLlogaria();
        //    }
        //    finally
        //    {
        //        dbManager.Dispose();
        //    }
        //}

        /// <summary>
        /// Ekzekuton prc_T_GRUPILLOGARIA_sel per te marre nje objekt clsGrupiLlogaria nga DB.
        /// <param name="kodi">kodi i grupit te llogarise</param>
        /// <param name="idnderm"> id e ndermarjes</param>
        /// </summary>
        public int ktheIDGrupLlogaria(string kodi, int idnderm, int idGjuha)
        {
            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@PERSHKGRUPILLOGARIA", kodi, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDGJUHA", idGjuha, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_GRUPILLOGARIA_selKod");

            if (ds == null)
                return -1;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return -1;
            int idGrupLlog;
            int.TryParse(ds.Tables[0].Rows[0]["IDGRUPILLOGARIA"].ToString(), out idGrupLlog);
            return idGrupLlog;
        }
        /// <summary>
        /// Ekzekuton prc_T_GRUPILLOGARIA_sel per te marre nje objekt clsGrupiLlogaria nga DB.
        /// <param name="kodi">kodi i grupit te llogarise</param>
        /// <param name="idnderm"> id e ndermarjes</param>
        /// </summary>
        public DataRow ktheGrupLlogaria(string kodi, int idnderm, int idGjuha)
        {
            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@PERSHKGRUPILLOGARIA", kodi, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDGJUHA", idGjuha, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_GRUPILLOGARIA_selKod");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];
        }
        //[Obsolete("Perdor: DataRow ktheGrupLlogaria(string kodi, int idnderm) ose int ktheIDGrupLlogaria(string kodi, int idnderm)", true)]
        //public colGrupetLlogaria merrGrupLlogaria(string kodi, int idnderm)
        //{
        //    IDBManager dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        //    dbManager.ConnectionString = dbManager.GetConnectionString();
        //    try
        //    {
        //        dbManager.Open();
        //        dbManager.CreateParameters(2);
        //        dbManager.AddParameters(0, "@PERSHKGRUPILLOGARIA", kodi, ParameterDirection.Input);
        //        dbManager.AddParameters(1, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
        //        DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_GRUPILLOGARIA_selKod");
        //        colGrupetLlogaria grupet = new colGrupetLlogaria();
        //        return grupet.mbushArrayListGrupetLlogaria(ds);
        //    }
        //    catch (Exception)
        //    {
        //        return new colGrupetLlogaria();
        //    }
        //    finally
        //    {
        //        dbManager.Dispose();
        //    }
        //}

        /// <summary>
        /// Ekzekuton prc_T_GRUPILLOGARIA_merrGjitheGrupetLlogaria per te marre nje collection me objekte clsGrupiLlogaria ne DB.
        /// <returns> Kthen nje collection me objekte clsGrupiLlogaria c</returns>
        /// </summary>
        public DataTable ktheGjitheGrupetLlogaria()
        {//metoda per te marre te gjithe grupet

            dbManager.Open();
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_GRUPILLOGARIA_merrGjitheGrupetLlogaria");
            return ds.Tables[0];
        }
        //[Obsolete("Perdor: DataTable merrGjitheGrupetLlogaria()", true)]
        //public colGrupetLlogaria merrGjitheGrupetLlogaria()
        //{//metoda per te marre te gjithe grupet
        //    IDBManager dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        //    dbManager.ConnectionString = dbManager.GetConnectionString();
        //    try
        //    {
        //        dbManager.Open();
        //        DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_GRUPILLOGARIA_merrGjitheGrupetLlogaria");
        //        colGrupetLlogaria grupetLlogaria = new colGrupetLlogaria();
        //        return grupetLlogaria.mbushArrayListGrupetLlogaria(ds);
        //    }
        //    catch (Exception)
        //    { 
        //        return new colGrupetLlogaria();
        //    }
        //    finally
        //    {
        //        dbManager.Dispose();
        //    }

        //}

        /// <summary>
        /// Ekzekuton prc_T_GRUPILLOGARIA_merrGjitheGrupetLlogariapozitive per te marre nje collection me objekte clsGrupiLlogaria ne DB duke filtruar sipas ID-se se ndermarrjes.
        /// <param name="idNdermarje">Id e ndermarrjes</param>
        /// <returns> Kthen nje collection me objekte clsGrupiLlogaria c</returns>
        /// </summary>
        public DataTable ktheGjitheGrupetLlogariapozitive(int idNdermarje, int idGjuha)
        {//metoda per te marre te gjithe grupet

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDNDERMARJE", idNdermarje, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDGJUHA", idGjuha, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_GRUPILLOGARIA_merrGjitheGrupetLlogariapozitive");
            return ds.Tables[0];
        }
        //[Obsolete("Perdor: DataTable merrGjitheGrupetLlogariapozitive(int idNdermarje)", true)]
        //public colGrupetLlogaria merrGjitheGrupetLlogariapozitive(int idNdermarje)
        //{//metoda per te marre te gjithe grupet
        //    IDBManager dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        //    dbManager.ConnectionString = dbManager.GetConnectionString();
        //    try
        //    {
        //        dbManager.Open();
        //        dbManager.CreateParameters(1);
        //        dbManager.AddParameters(0, "@IDNDERMARJE", idNdermarje, ParameterDirection.Input);
        //        DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_GRUPILLOGARIA_merrGjitheGrupetLlogariapozitive");
        //        colGrupetLlogaria grupetLlogaria = new colGrupetLlogaria();
        //        return grupetLlogaria.mbushArrayListGrupetLlogaria(ds);
        //    }
        //    catch (Exception)
        //    {
        //        return new colGrupetLlogaria();
        //    }
        //    finally
        //    {
        //        dbManager.Dispose();
        //    }
        //}

        #endregion

        /// <summary>
        /// Metodat e meposhtme i perkasin veprimeve qe kryhen nga objektet clsNenGrupiLlogaria dhe colNenGrupetLlogaria
        /// </summary>
        #region NENGRUPET E LLOGARIVE

        /// <summary>
        /// Ekzekuton prc_T_NENGRUPILLOGARIA_ins per te ruajtur nje objekt clsNenGrupiLlogaria ne DB.
        /// <param name="nengrupiLlogaria">Objekt i tipit clsNenGrupiLlogaria qe do te ruhet ne DB</param>
        /// <returns> Kthen statusin e perfundimit te ekzekutimit te SP-se perkatese (nje objekt clsMesazh qe tregon nese veprimi eshte kryer me sukses apo jo.<seealso cref="DbCore.clsMesazh"/>)</returns>
        /// </summary>
        internal clsMesazh ruajNenGrupLlogaria(out int idnengrupiLlogaria, int nrnengrupiLlogaria, String pershkriminengrupiLlogaria, int idgrupiLlogaria, int idnderm)
        { //metoda per ruajtjen e nengrupit
            idnengrupiLlogaria = -1;

            dbManager.Open();
            //shtimi i parametrave
            dbManager.CreateParameters(5);
            dbManager.AddParameters(0, "@IDNENGRUPILLOGARIA", idnengrupiLlogaria, ParameterDirection.Output);
            dbManager.AddParameters(1, "@NRNENGRUPILLOGARIA", nrnengrupiLlogaria, ParameterDirection.Input);
            dbManager.AddParameters(2, "@PERSHKNENGRUPILLOGARIA", pershkriminengrupiLlogaria, ParameterDirection.Input);
            dbManager.AddParameters(3, "@IDGRUPILLOGARIA", idgrupiLlogaria, ParameterDirection.Input);
            dbManager.AddParameters(4, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_NENGRUPILLOGARIA_ins");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
            return mesazh;
        }
        //[Obsolete("Perdor: DataRow ktheNenGrupLlogaria(int idndengrupi)", true)]
        //public clsMesazh ruajNenGrupLlogaria(clsNenGrupiLlogaria nengrupiLlogaria)
        //{ //metoda per ruajtjen e nengrupit
        //    IDBManager dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        //    dbManager.ConnectionString = dbManager.GetConnectionString();
        //    try
        //    {
        //        dbManager.Open();
        //        //shtimi i parametrave
        //        dbManager.CreateParameters(5);
        //        dbManager.AddParameters(0, "@IDNENGRUPILLOGARIA", nengrupiLlogaria.IdNenGrupiLlogaria, ParameterDirection.Output);
        //        dbManager.AddParameters(1, "@NRNENGRUPILLOGARIA", nengrupiLlogaria.NrNenGrupiLlogaria, ParameterDirection.Input);
        //        dbManager.AddParameters(2, "@PERSHKNENGRUPILLOGARIA", nengrupiLlogaria.PershkrimiNenGrupiLlogaria, ParameterDirection.Input);
        //        dbManager.AddParameters(3, "@IDGRUPILLOGARIA", nengrupiLlogaria.IdGrupiLlogaria, ParameterDirection.Input);
        //        dbManager.AddParameters(4, "@IDNDERMARJE", nengrupiLlogaria.IdNdermarje, ParameterDirection.Input);
        //        dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_NENGRUPILLOGARIA_ins");
        //        clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
        //        return mesazh;
        //    }
        //    catch (Exception)
        //    {
        //        return new clsMesazh(false, ce.Message);
        //    }
        //    finally
        //    {
        //        dbManager.Dispose();
        //    }
        //}

        /// <summary>
        /// Ekzekuton prc_T_NENGRUPILLOGARIA_upd per te modifikuar nje objekt clsNenGrupiLlogaria ne DB.
        /// <param name="nengrupiLlogaria">Objekt i tipit clsNenGrupiLlogaria qe do te modifikohet</param>
        /// <returns> Kthen statusin e perfundimit te ekzekutimit te SP-se perkatese (nje objekt clsMesazh qe tregon nese veprimi eshte kryer me sukses apo jo.<seealso cref="DbCore.clsMesazh"/>)</returns>
        /// </summary>
        internal clsMesazh modifikoNenGrupLlogaria(int idnengrupiLlogaria, int nrnengrupiLlogaria, String pershkriminengrupiLlogaria, int idgrupiLlogaria, int idnderm)
        {//metoda per modifikimin e nengrupeve

            dbManager.Open();
            //shtimi i parametrave
            dbManager.CreateParameters(5);
            dbManager.AddParameters(0, "@IDNENGRUPILLOGARIA", idnengrupiLlogaria, ParameterDirection.Input);
            dbManager.AddParameters(1, "@NRNENGRUPILLOGARIA", nrnengrupiLlogaria, ParameterDirection.Input);
            dbManager.AddParameters(2, "@PERSHKNENGRUPILLOGARIA", pershkriminengrupiLlogaria, ParameterDirection.Input);
            dbManager.AddParameters(3, "@IDGRUPILLOGARIA", idgrupiLlogaria, ParameterDirection.Input);
            dbManager.AddParameters(4, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_NENGRUPILLOGARIA_upd");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgModifikimiMeSukses"]);
            return mesazh;
        }
        //[Obsolete("Perdor: DataRow ktheNenGrupLlogaria(int idndengrupi)", true)]
        //public clsMesazh modifikoNenGrupLlogaria(clsNenGrupiLlogaria nengrupiLlogaria)
        //{//metoda per modifikimin e nengrupeve
        //    IDBManager dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        //    dbManager.ConnectionString = dbManager.GetConnectionString();
        //    try
        //    {
        //        dbManager.Open();
        //        //shtimi i parametrave
        //        dbManager.CreateParameters(5);
        //        dbManager.AddParameters(0, "@IDNENGRUPILLOGARIA", nengrupiLlogaria.IdNenGrupiLlogaria, ParameterDirection.Input);
        //        dbManager.AddParameters(1, "@NRNENGRUPILLOGARIA", nengrupiLlogaria.NrNenGrupiLlogaria, ParameterDirection.Input);
        //        dbManager.AddParameters(2, "@PERSHKNENGRUPILLOGARIA", nengrupiLlogaria.PershkrimiNenGrupiLlogaria, ParameterDirection.Input);
        //        dbManager.AddParameters(3, "@IDGRUPILLOGARIA", nengrupiLlogaria.IdGrupiLlogaria, ParameterDirection.Input);
        //        dbManager.AddParameters(4, "@IDNDERMARJE", nengrupiLlogaria.IdNdermarje, ParameterDirection.Input);
        //        dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_NENGRUPILLOGARIA_upd");
        //        clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgModifikimiMeSukses"]);
        //        return mesazh;
        //    }
        //    catch (Exception)
        //    {
        //        return new clsMesazh(false, ce.Message);
        //    }
        //    finally
        //    {
        //        dbManager.Dispose();
        //    }
        //}

        /// <summary>
        /// Ekzekuton prc_T_NENGRUPILLOGARIA_del per te fshire nje objekt clsNenGrupiLlogaria ne DB.
        /// <param name="nengrupiLlogaria">Objekt i tipit clsNenGrupiLlogaria qe do te fshihet</param>
        /// <returns> Kthen statusin e perfundimit te ekzekutimit te SP-se perkatese (nje objekt clsMesazh qe tregon nese veprimi eshte kryer me sukses apo jo.<seealso cref="DbCore.clsMesazh"/>)</returns>
        /// </summary>
        internal clsMesazh fshiNenGrupLlogaria(int idnengrupiLlogaria)
        {//metoda per fshirjen e nengrupeve
            dbManager.Open();
            //shtimi i parametrave
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDNENGRUPILLOGARIA", idnengrupiLlogaria, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_NENGRUPILLOGARIA_del");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
            return mesazh;
        }
        //[Obsolete("Perdor: DataRow ktheNenGrupLlogaria(int idndengrupi)", true)]
        //public clsMesazh fshiNenGrupLlogaria(clsNenGrupiLlogaria nengrupiLlogaria)
        //{//metoda per fshirjen e nengrupeve
        //    IDBManager dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        //    dbManager.ConnectionString = dbManager.GetConnectionString();
        //    try
        //    {
        //        dbManager.Open();
        //        //shtimi i parametrave
        //        dbManager.CreateParameters(1);
        //        dbManager.AddParameters(0, "@IDNENGRUPILLOGARIA", nengrupiLlogaria.IdNenGrupiLlogaria, ParameterDirection.Input);
        //        dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_NENGRUPILLOGARIA_del");
        //        clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
        //        return mesazh;
        //    }
        //    catch (Exception)
        //    {
        //        return new clsMesazh(false, ce.Message);
        //    }
        //    finally
        //    {
        //        dbManager.Dispose();
        //    }
        //}

        /// <summary>
        /// Ekzekuton prc_T_NENGRUPILLOGARIA_sel per te marre nje datarow ne DB.
        /// <param name="idndengrupi">Id e nengrupit te llogarise</param>
        /// </summary>
        internal DataRow ktheNenGrupLlogaria(int idndengrupi, int idGjuha)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDNENGRUPILLOGARIA", idndengrupi, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDGJUHA", idGjuha, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_NENGRUPILLOGARIA_sel");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];
        }
        //[Obsolete("Perdor: DataRow ktheNenGrupLlogaria(int idndengrupi)", true)]
        //public colNenGrupetLlogaria merrNenGrupLlogaria(int idndengrupi)
        //{
        //    IDBManager dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        //    dbManager.ConnectionString = dbManager.GetConnectionString();
        //    try
        //    {
        //        dbManager.Open();
        //        dbManager.CreateParameters(1);
        //        dbManager.AddParameters(0, "@IDNENGRUPILLOGARIA", idndengrupi, ParameterDirection.Input);
        //        DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_NENGRUPILLOGARIA_sel");
        //        colNenGrupetLlogaria nengrupet = new colNenGrupetLlogaria();
        //        return nengrupet.mbushArrayListNenGrupetLlogaria(ds);
        //    }
        //    catch (Exception)
        //    {
        //        return new colNenGrupetLlogaria();
        //    }
        //    finally
        //    {
        //        dbManager.Dispose();
        //    }
        //}

        /// <summary>
        /// Ekzekuton prc_T_NENGRUPILLOGARIA_sel per te marre id e nengrupit te llogarise ne DB.
        /// <param name="kodi">kodi i nengrupit te llogarise</param>
        /// <param name="idnder"> id e ndermarjes</param>
        /// </summary>
        internal int ktheNenGrupLlogaria(string kodi, int idnder, int idGjuha)
        {
            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@PERSHKNENGRUPILLOGARIA", kodi, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idnder, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDGJUHA", idGjuha, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_NENGRUPILLOGARIA_selKod");
            if (ds == null)
                return -1;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return -1;
            int idNenGrupLlogaria;
            int.TryParse(ds.Tables[0].Rows[0]["IDNENGRUPILLOGARIA"].ToString(), out idNenGrupLlogaria);
            return idNenGrupLlogaria;
        }
        //[Obsolete("Perdor: int ktheNenGrupLlogaria(string kodi, int idnder)", true)]
        //public colNenGrupetLlogaria merrNenGrupLlogaria(string kodi, int idnder)
        //{
        //    IDBManager dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        //    dbManager.ConnectionString = dbManager.GetConnectionString();
        //    try
        //    {
        //        dbManager.Open();
        //        dbManager.CreateParameters(2);
        //        dbManager.AddParameters(0, "@PERSHKNENGRUPILLOGARIA", kodi, ParameterDirection.Input);
        //        dbManager.AddParameters(1, "@IDNDERMARJE", idnder, ParameterDirection.Input);
        //        DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_NENGRUPILLOGARIA_selKod");
        //        colNenGrupetLlogaria nengrupet = new colNenGrupetLlogaria();
        //        return nengrupet.mbushArrayListNenGrupetLlogaria(ds);
        //    }
        //    catch (Exception)
        //    {
        //        return new colNenGrupetLlogaria();
        //    }
        //    finally
        //    {
        //        dbManager.Dispose();
        //    }
        //}

        /// <summary>
        /// Ekzekuton prc_T_NENGRUPILLOGARIA_merrGjitheNenGrupetLlogaria per te marre nje datatable ne DB duke filtruar sipas ID-se se ndermarrjes.
        /// <param name="idnderm">ID e ndermarrjes</param>
        /// <returns> Kthen nje datatable</returns>
        /// </summary>
        internal DataTable ktheGjitheNenGrupetLlogaria(int idnderm, int idGjuha)
        {//metoda per te marre te gjithe grupet
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDGJUHA", idGjuha, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_NENGRUPILLOGARIA_merrGjitheNenGrupetLlogaria");
            return ds.Tables[0];
        }
        //[Obsolete("Perdor: DataTable ktheGjitheNenGrupetLlogaria(int idnderm)", true)]
        //public colNenGrupetLlogaria merrGjitheNenGrupetLlogaria(int idnderm)
        //{//metoda per te marre te gjithe grupet
        //    IDBManager dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        //    dbManager.ConnectionString = dbManager.GetConnectionString();
        //    try
        //    {
        //        dbManager.Open();
        //        dbManager.CreateParameters(1);
        //        dbManager.AddParameters(0, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
        //        DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_NENGRUPILLOGARIA_merrGjitheNenGrupetLlogaria");
        //        colNenGrupetLlogaria nengrupetLlogaria = new colNenGrupetLlogaria();
        //        return nengrupetLlogaria.mbushArrayListNenGrupetLlogaria(ds);
        //    }
        //    catch (Exception)
        //    {  
        //        return new colNenGrupetLlogaria();
        //    }
        //    finally
        //    {
        //        dbManager.Dispose();
        //    }

        //}

        /// <summary>
        /// Ekzekuton prc_T_NENGRUPILLOGARIA_merrNenGrupetLlogariaSipasGrupit per te marre nje datatable ne DB duke filtruar sipas ID-se se grupit te llogarise.
        /// <param name="idGrupiLlogaria">Id e grupit te llogarise</param>
        /// <returns> Kthen nje datatable</returns>
        /// </summary>
        internal DataTable ktheNenGrupetLlogariaSipasGrupit(int idGrupiLlogaria, int idGjuha)
        {//metoda per te marre te gjithe grupet
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDGRUPILLOGARIA", idGrupiLlogaria, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDGJUHA", idGjuha, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_NENGRUPILLOGARIA_merrNenGrupetLlogariaSipasGrupit");
            return ds.Tables[0];
        }
        //[Obsolete("Perdor: DataTable ktheNenGrupetLlogariaSipasGrupit(int idGrupiLlogaria)", true)]
        //public colNenGrupetLlogaria merrNenGrupetLlogariaSipasGrupit(int idGrupiLlogaria)
        //{//metoda per te marre te gjithe grupet
        //    IDBManager dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        //    dbManager.ConnectionString = dbManager.GetConnectionString();
        //    try
        //    {
        //        dbManager.Open();
        //        dbManager.CreateParameters(1);
        //        dbManager.AddParameters(0, "@IDGRUPILLOGARIA", idGrupiLlogaria, ParameterDirection.Input);
        //        DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_NENGRUPILLOGARIA_merrNenGrupetLlogariaSipasGrupit");
        //        colNenGrupetLlogaria nengrupetLlogaria = new colNenGrupetLlogaria();
        //        return nengrupetLlogaria.mbushArrayListNenGrupetLlogaria(ds);
        //    }
        //    catch (Exception)
        //    {  
        //        return new colNenGrupetLlogaria();
        //    }
        //    finally
        //    {
        //        dbManager.Dispose();
        //    }
        //}

        /// <summary>
        /// Ekzekuton prc_T_NENGRUPILLOGARIA_merrNenGrupetLlogariaSipasGrupitPozitive per te marre nje datatable ne DB duke filtruar sipas ID-se se grupit te llogarise (ku kjo e fundit eshte >0).
        /// <param name="idGrupiLlogaria">Id e grupit te llogarise</param>
        /// <returns> Kthen nje datatable</returns>
        /// </summary>
        internal DataTable ktheNenGrupetLlogariaSipasGrupitPozitive(int idGrupiLlogaria, int idGjuha)
        {//metoda per te marre te gjithe grupet

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDGRUPILLOGARIA", idGrupiLlogaria, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDGJUHA", idGjuha, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_NENGRUPILLOGARIA_merrNenGrupetLlogariaSipasGrupitPozitive");
            return ds.Tables[0];
        }
        //[Obsolete("Perdor: DataTable ktheNenGrupetLlogariaSipasGrupitPozitive(int idGrupiLlogaria)", true)]
        //public colNenGrupetLlogaria merrNenGrupetLlogariaSipasGrupitPozitive(int idGrupiLlogaria)
        //{//metoda per te marre te gjithe grupet
        //    IDBManager dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        //    dbManager.ConnectionString = dbManager.GetConnectionString();
        //    try
        //    {
        //        dbManager.Open();
        //        dbManager.CreateParameters(1);
        //        dbManager.AddParameters(0, "@IDGRUPILLOGARIA", idGrupiLlogaria, ParameterDirection.Input);
        //        DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_NENGRUPILLOGARIA_merrNenGrupetLlogariaSipasGrupitPozitive");
        //        colNenGrupetLlogaria nengrupetLlogaria = new colNenGrupetLlogaria();
        //        return nengrupetLlogaria.mbushArrayListNenGrupetLlogaria(ds);
        //    }
        //    catch (Exception)
        //    {
        //        return new colNenGrupetLlogaria();
        //    }
        //    finally
        //    {
        //        dbManager.Dispose();
        //    }
        //}

        /// <summary>
        /// Ekzekuton prc_T_NENGRUPILLOGARIA_merrGjitheNenGrupetLlogariaPozitive per te marre nje datatable ne DB duke filtruar sipas ID-se se ndermarrjes (ku Id e ndegrupit eshte >0).
        /// <param name="idnder">Id e ndermarrjes</param>
        /// <returns> Kthen nje datatable</returns>
        /// </summary>
        internal DataTable ktheGjitheNenGrupetLlogariaPozitive(int idnder)
        {//metoda per te marre te gjithe grupet
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDNDERMARJE", idnder, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_NENGRUPILLOGARIA_merrGjitheNenGrupetLlogariaPozitive");
            return ds.Tables[0];
        }
        //[Obsolete("Perdor: DataTable ktheGjitheNenGrupetLlogariaPozitive(int idnder)", true)]
        //public colNenGrupetLlogaria merrGjitheNenGrupetLlogariaPozitive(int idnder)
        //{//metoda per te marre te gjithe grupet
        //    IDBManager dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        //    dbManager.ConnectionString = dbManager.GetConnectionString();
        //    try
        //    {
        //        dbManager.Open();
        //        dbManager.CreateParameters(1);
        //        dbManager.AddParameters(0, "@IDNDERMARJE", idnder, ParameterDirection.Input);
        //        DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_NENGRUPILLOGARIA_merrGjitheNenGrupetLlogariaPozitive");
        //        colNenGrupetLlogaria nengrupetLlogaria = new colNenGrupetLlogaria();
        //        return nengrupetLlogaria.mbushArrayListNenGrupetLlogaria(ds);
        //    }
        //    catch (Exception)
        //    {
        //        return new colNenGrupetLlogaria();
        //    }
        //    finally
        //    {
        //        dbManager.Dispose();
        //    }

        //}

        #endregion

        /// <summary>
        /// Metodat e meposhtme i perkasin veprimeve qe kryhen nga objektet clsLlojBuxheti dhe colLlojeBuxhetesh
        /// </summary>
        #region LLOJE BUXHETESH

        /// <summary>
        /// Ekzekuton prc_T_LLOJBUXHETI_ins per te ruajtur nje objekt clsLlojBuxheti ne DB.
        /// <param name="llojBuxheti">Objekt i tipit clsLlojBuxheti qe do te ruhet ne DB</param>
        /// <returns> Kthen statusin e perfundimit te ekzekutimit te SP-se perkatese (nje objekt clsMesazh qe tregon nese veprimi eshte kryer me sukses apo jo.<seealso cref="DbCore.clsMesazh"/>)</returns>
        /// </summary>
        internal clsMesazh ruajLlojBuxheti(out int idllojbuxheti, String kodllojbuxheti, bool perAutorizim)
        { //metoda per ruajtjen e LLOJIT TE BUXHETIT
            idllojbuxheti = -1;
            dbManager.Open();
            //shtimi i parametrave
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@IDLLOJBUXHETI", idllojbuxheti, ParameterDirection.Output);
            dbManager.AddParameters(1, "@KODBUXHETI", kodllojbuxheti, ParameterDirection.Input);
            dbManager.AddParameters(2, "@PERAUTORIZIM", perAutorizim, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_LLOJBUXHETI_ins");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
            return mesazh;
        }
        //[Obsolete("Perdor: DataTable ktheGjitheLlojeBuxhetesh()", true)]
        //public clsMesazh ruajLlojBuxheti(clsLlojBuxheti llojBuxheti)
        //{ //metoda per ruajtjen e LLOJIT TE BUXHETIT
        //    IDBManager dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        //    dbManager.ConnectionString = dbManager.GetConnectionString();
        //    try
        //    {
        //        dbManager.Open();
        //        //shtimi i parametrave
        //        dbManager.CreateParameters(2);
        //        dbManager.AddParameters(0, "@IDLLOJBUXHETI", llojBuxheti.IdLlojBuxheti, ParameterDirection.Output);
        //        dbManager.AddParameters(1, "@KODBUXHETI", llojBuxheti.KodLlojBuxheti, ParameterDirection.Input);
        //            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_LLOJBUXHETI_ins");
        //            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
        //            return mesazh;
        //    }
        //    catch (Exception)
        //    {
        //        return new clsMesazh(false, ce.Message);
        //    }
        //    finally
        //    {
        //        dbManager.Dispose();
        //    }

        //}

        /// <summary>
        /// Ekzekuton prc_T_LLOJBUXHETI_upd per te modifikuar nje objekt clsLlojBuxheti ne DB.
        /// <param name="llojBuxheti">Objekt i tipit clsLlojBuxheti qe do te modifikohet</param>
        /// <returns> Kthen statusin e perfundimit te ekzekutimit te SP-se perkatese (nje objekt clsMesazh qe tregon nese veprimi eshte kryer me sukses apo jo.<seealso cref="DbCore.clsMesazh"/>)</returns>
        /// </summary>
        internal clsMesazh modifikoLlojBuxheti(int idllojbuxheti, String kodllojbuxheti, bool perAutorizim)
        {//metoda per modifikimin e llojit te buxhetit

            dbManager.Open();
            //shtimi i parametrave
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@IDLLOJBUXHETI", idllojbuxheti, ParameterDirection.Input);
            dbManager.AddParameters(1, "@KODLLOJBUXHETI", kodllojbuxheti, ParameterDirection.Input);
            dbManager.AddParameters(2, "@PERAUTORIZIM", perAutorizim, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_LLOJBUXHETI_upd");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgModifikimiMeSukses"]);
            return mesazh;
        }
        //[Obsolete("Perdor: DataTable ktheGjitheLlojeBuxhetesh()", true)]
        //public clsMesazh modifikoLlojBuxheti(clsLlojBuxheti llojBuxheti)
        //{//metoda per modifikimin e llojit te buxhetit
        //    IDBManager dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        //    dbManager.ConnectionString = dbManager.GetConnectionString();
        //    try
        //    {
        //        dbManager.Open();
        //        //shtimi i parametrave
        //        dbManager.CreateParameters(2);
        //        dbManager.AddParameters(0, "@IDLLOJBUXHETI", llojBuxheti.IdLlojBuxheti, ParameterDirection.Input);
        //        dbManager.AddParameters(1, "@KODLLOJBUXHETI", llojBuxheti.KodLlojBuxheti, ParameterDirection.Input);
        //            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_LLOJBUXHETI_upd");
        //            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgModifikimiMeSukses"]);
        //            return mesazh;
        //    }
        //    catch (Exception)
        //    {
        //        return new clsMesazh(false, ce.Message);
        //    }
        //    finally
        //    {
        //        dbManager.Dispose();
        //    }
        //}

        /// <summary>
        /// Ekzekuton prc_T_LLOJBUXHETI_upd per te fshire nje objekt clsLlojBuxheti ne DB.
        /// <param name="llojBuxheti">Objekt i tipit clsLlojBuxheti qe do te fshihet</param>
        /// <returns> Kthen statusin e perfundimit te ekzekutimit te SP-se perkatese (nje objekt clsMesazh qe tregon nese veprimi eshte kryer me sukses apo jo.<seealso cref="DbCore.clsMesazh"/>)</returns>
        /// </summary>
        internal clsMesazh fshiLlojBuxheti(int idllojbuxheti)
        {//metoda per fshirjen e llojit te buxhetit

            dbManager.Open();
            //shtimi i parametrave
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDLLOJBUXHETI", idllojbuxheti, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_LLOJBUXHETI_del");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
            return mesazh;
        }
        //[Obsolete("Perdor: DataTable ktheGjitheLlojeBuxhetesh()", true)]
        //public clsMesazh fshiLlojBuxheti(clsLlojBuxheti llojBuxheti)
        //{//metoda per fshirjen e llojit te buxhetit
        //    IDBManager dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        //    dbManager.ConnectionString = dbManager.GetConnectionString();
        //    try
        //    {
        //        dbManager.Open();
        //        //shtimi i parametrave
        //        dbManager.CreateParameters(1);
        //        dbManager.AddParameters(0, "@IDLLOJBUXHETI", llojBuxheti.IdLlojBuxheti, ParameterDirection.Input);
        //        dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_LLOJBUXHETI_del");
        //        clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
        //        return mesazh;
        //    }
        //    catch (Exception)
        //    {
        //        return new clsMesazh(false, ce.Message);
        //    }
        //    finally
        //    {
        //        dbManager.Dispose();
        //    }
        //}

        /// <summary>
        /// Ekzekuton prc_T_LLOJBUXHETI_sel per te marre nje objekt clsLlojBuxheti ne DB.
        /// <param name="llojBuxheti">Objekt i tipit clsLlojBuxheti Id-ja e te cilit perdoret per filtrim ne DB</param>
        /// </summary>
        internal void merrLlojBuxheti(int idllojbuxheti)
        {// metoda per te marre nje LLOJ BUXHETI NE BAZE TE ID

            dbManager.Open();
            //shtimi i parametrave
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDLLOJBUXHETI", idllojbuxheti, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_LLOJBUXHETI_sel");
        }
        //[Obsolete("Perdor: DataTable ktheGjitheLlojeBuxhetesh()", true)]
        //public void merrLlojBuxheti(clsLlojBuxheti llojBuxheti)
        //{// metoda per te marre nje LLOJ BUXHETI NE BAZE TE ID
        //    IDBManager dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        //    dbManager.ConnectionString = dbManager.GetConnectionString();
        //    try
        //    {
        //        dbManager.Open();
        //        //shtimi i parametrave
        //        dbManager.CreateParameters(1);
        //        dbManager.AddParameters(0, "@IDLLOJBUXHETI", llojBuxheti.IdLlojBuxheti, ParameterDirection.Input);
        //        dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_LLOJBUXHETI_sel");
        //    }
        //    catch (Exception)
        //    {
        //    }
        //    finally
        //    {
        //        dbManager.Dispose();
        //    }
        //}

        /// <summary>
        /// Ekzekuton prc_T_LLOJBUXHETI_merrGjitheLlojeBuxhetesh per te marre nje datatable ne DB.
        /// <returns>Kthen nje datatable </returns>
        /// </summary>
        internal DataTable ktheGjitheLlojeBuxhetesh()
        {//metoda per te marre te gjithe llojet e buxheteve

            dbManager.Open();
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_LLOJBUXHETI_merrGjitheLlojeBuxhetesh");
            return ds.Tables[0];

        }

        /// <summary>
        /// Ekzekuton prc_T_LLOJBUXHETI_merrLlojeBuxheteshPerAutorizim per te marre nje datatable ne DB.
        /// <returns>Kthen nje datatable </returns>
        /// </summary>
        internal DataTable ktheLlojeBuxheteshPerAutorizim()
        {//metoda per te marre te gjithe llojet e buxheteve

            dbManager.Open();
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_LLOJBUXHETI_merrLlojeBuxheteshPerAutorizim");
            return ds.Tables[0];
        }
        //[Obsolete("Perdor: DataTable ktheGjitheLlojeBuxhetesh()", true)]
        //public colLlojeBuxhetesh merrGjitheLlojeBuxhetesh()
        //{//metoda per te marre te gjithe llojet e buxheteve
        //    IDBManager dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        //    dbManager.ConnectionString = dbManager.GetConnectionString();
        //    try
        //    {
        //        dbManager.Open();
        //        DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_LLOJBUXHETI_merrGjitheLlojeBuxhetesh");
        //        colLlojeBuxhetesh llojeBuxhetesh = new colLlojeBuxhetesh();
        //        return llojeBuxhetesh.mbushArrayListLlojeBuxhetesh(ds);
        //    }
        //    catch (Exception)
        //    { 
        //        return new colLlojeBuxhetesh();
        //    }
        //    finally
        //    {
        //        dbManager.Dispose();
        //    }
        //}

        /// <summary>
        /// Ekzekuton prc_T_LLOJBUXHETI_merrLlojBuxhetiSipasKodit per te marre id e llojit te buxhetit ne DB duke filtruar sipas kodit.
        /// <param name="kodi">Kodi i llojit te buxhetit</param>
        /// <returns>Kthen id e llojit te buxhetit </returns>
        /// </summary>
        internal int ktheLlojBuxhetiSipasKodit(String kodi)
        {
            ImbLogger.LogTraceShitje($"Filloi metoda per te marre llojin e buxhetit sipas kodi:{kodi}");
            //metoda per te marre te llojin e buxhetit sipas kodit
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@KODLLOJBUXHETI", kodi, ParameterDirection.Input);
            ImbLogger.LogTraceShitje($"Mbaroi metoda per te marre llojin e buxhetit sipas kodi:{kodi}");
            return Convert.ToInt32(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_LLOJBUXHETI_merrLlojBuxhetiSipasKodit"));
        }
        //[Obsolete("Perdor: int ktheLlojBuxhetiSipasKodit(String kodi)", true)]
        //public colLlojeBuxhetesh merrLlojBuxhetiSipasKodit(String kodi)
        //{//metoda per te marre te llojin e buxhetit sipas kodit
        //    IDBManager dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        //    dbManager.ConnectionString = dbManager.GetConnectionString();
        //    try
        //    {
        //        dbManager.Open();
        //        dbManager.CreateParameters(1);
        //        dbManager.AddParameters(0, "@KODLLOJBUXHETI", kodi, ParameterDirection.Input);
        //        DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_LLOJBUXHETI_merrLlojBuxhetiSipasKodit");
        //        colLlojeBuxhetesh llojeBuxhetesh = new colLlojeBuxhetesh();
        //        return llojeBuxhetesh.mbushArrayListLlojeBuxhetesh(ds);
        //    }
        //    catch (Exception)
        //    { 
        //        return new colLlojeBuxhetesh();
        //    }
        //    finally
        //    {
        //        dbManager.Dispose();
        //    }
        //}

        #endregion

        /// <summary>
        /// Metodat e meposhtme i perkasin veprimeve qe kryhen nga objektet clsBuxheti dhe colBuxhetet
        /// </summary>
        #region BUXHETI



        /// <summary>
        /// Ekzekuton prc_T_BUXHETI_ins per te ruajtur nje objekt clsBuxheti ne DB.
        /// <param name="idbuxheti">Id e buxhetit</param>
        /// <param name="idllojbuxheti">Id e llojit te buxhetit <seealso cref="DbCore.DbKontabiliteti.clsLlojBuxheti"/></param>
        /// <param name="idlidhese">Id lidhese</param>
        /// <param name="muaji">Muaji</param>
        /// <param name="buxh_1">Buxheti 1</param>
        /// <param name="buxh_2">Buxheti 2</param>
        /// <param name="gjen">Gjendja</param>
        /// <param name="diff1">Diferenca 1</param>
        /// <param name="diff2">Diferenca 2</param>
        /// <returns> Kthen statusin e perfundimit te ekzekutimit te SP-se perkatese (nje objekt clsMesazh qe tregon nese veprimi eshte kryer me sukses apo jo.<seealso cref="DbCore.clsMesazh"/>)</returns>
        /// </summary>
        public clsMesazh ruajBuxhet(out int idbuxheti, int idllojbuxheti, int idlidhese, String muaji, decimal buxh_1, decimal buxh_2, int idNderVit, bool eshteProjektBuxhet, string shenime, DateTime dtaktivizimi, int idkonfigurdherpagese)
        {
            idbuxheti = -1;
            dbManager.Open();
            dbManager.CreateParameters(11);
            dbManager.AddParameters(0, "@IDBUXHETI", idbuxheti, ParameterDirection.Output);
            dbManager.AddParameters(1, "@IDLLOJBUXHETI", idllojbuxheti, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDLIDHESE", idlidhese, ParameterDirection.Input);
            dbManager.AddParameters(3, "@MUAJ", muaji, ParameterDirection.Input);
            dbManager.AddParameters(4, "@BUXHETI_1", buxh_1, ParameterDirection.Input);
            dbManager.AddParameters(5, "@BUXHETI_2", buxh_2, ParameterDirection.Input);
            if (idNderVit == 0 || eshteProjektBuxhet)
                dbManager.AddParameters(6, "@IDNDERVITI", DBNull.Value, ParameterDirection.Input);
            else
                dbManager.AddParameters(6, "@IDNDERVITI", idNderVit, ParameterDirection.Input);
            if (!eshteProjektBuxhet)
                dbManager.AddParameters(7, "@IDVITIPROJEKTBUXHETI", DBNull.Value, ParameterDirection.Input);
            else
                dbManager.AddParameters(7, "@IDVITIPROJEKTBUXHETI", idNderVit, ParameterDirection.Input);
            dbManager.AddParameters(8, "@SHENIME", shenime, ParameterDirection.Input);
            dbManager.AddParameters(9, "@DtAktivizimi", dtaktivizimi, ParameterDirection.Input);
            if(idkonfigurdherpagese>0)
            dbManager.AddParameters(10, "@IDKONFIGURDHERPAGESE", idkonfigurdherpagese, ParameterDirection.Input);
            else   dbManager.AddParameters(10, "@IDKONFIGURDHERPAGESE", DBNull.Value, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_BUXHETI_ins");
            return new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
        }
        /// <summary>
        /// Ekzekuton prc_T_BUXHETI_MERGEDT per te ruajtur ne DB nje datatable me buxhete te gjeneruara nga trupi i pasqyres financiare.
        /// </summary>
        /// <param name="dt">Datatable me te gjitha buxhetet qe do te ruhen ne DB</param>
        /// <returns>Kthen statusin e perfundimit te ekzekutimit te SP-se perkatese</returns>
        internal clsMesazh ruajBuxhetDt(DataTable dt)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddInputParameters("@VLERAT_NDRYSHUARA", dt);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_BUXHETI_MERGEDT");
            return new clsMesazh(true, IMBUtils.Messages.MessagesResource.Messages["labelRaportMesazhRuajtjaPerfundoiSukses"]);
        }

        public clsMesazh ruajBuxhet(out int idbuxheti, int idllojbuxheti, int idlidhese, String muaji, decimal buxh_1, decimal buxh_2, DateTime dtaktivizimi, int idkonfigurdherpagese)
        {
            return ruajBuxhet(out idbuxheti, idllojbuxheti, idlidhese, muaji, buxh_1, buxh_2, 0, false, "", dtaktivizimi, idkonfigurdherpagese);
        }

        public clsMesazh ruajBuxhet(out int idbuxheti, int idllojbuxheti, int idlidhese, String muaji, decimal buxh_1, decimal buxh_2, int idNderVit, DateTime dtaktivizimi,int idkonfigurdherpagese, string shenime = "")
        {
            return ruajBuxhet(out idbuxheti, idllojbuxheti, idlidhese, muaji, buxh_1, buxh_2, idNderVit, false, shenime,dtaktivizimi,idkonfigurdherpagese);
        }

        /// <summary>
        /// Ekzekuton prc_T_BUXHETI_upd per te modifikuar nje objekt clsBuxheti ne DB per vitin e ndermarrjes se loguar.
        /// <param name="idbuxheti">Id e buxhetit</param>
        /// <param name="idllojbuxheti">Id e llojit te buxhetit <seealso cref="DbCore.DbKontabiliteti.clsLlojBuxheti"/></param>
        /// <param name="idlidhese">Id lidhese</param>
        /// <param name="muaji">Muaji</param>
        /// <param name="buxh_1">Buxheti 1</param>
        /// <param name="buxh_2">Buxheti 2</param>
        /// <param name="gjen">Gjendja</param>
        /// <param name="diff1">Diferenca 1</param>
        /// <param name="diff2">Diferenca 2</param>
        /// <returns> Kthen statusin e perfundimit te ekzekutimit te SP-se perkatese (nje objekt clsMesazh qe tregon nese veprimi eshte kryer me sukses apo jo.<seealso cref="DbCore.clsMesazh"/>)</returns>
        /// </summary>
        public clsMesazh modifikoBuxhet(int idbuxheti, int idllojbuxheti, int idlidhese, String muaji, decimal buxh_1, decimal buxh_2, int idNderVit, bool eshteProjektBuxhet, string shenime, DateTime dtaktivizimi, int idkonfigurdherpagese)
        {

            dbManager.Open();
            dbManager.CreateParameters(11);
            dbManager.AddParameters(0, "@IDBUXHETI", idbuxheti, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDLLOJBUXHETI", idllojbuxheti, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDLIDHESE", idlidhese, ParameterDirection.Input);
            dbManager.AddParameters(3, "@MUAJ", muaji, ParameterDirection.Input);
            dbManager.AddParameters(4, "@BUXHETI_1", buxh_1, ParameterDirection.Input);
            dbManager.AddParameters(5, "@BUXHETI_2", buxh_2, ParameterDirection.Input);
            if (idNderVit == 0 || eshteProjektBuxhet)
                dbManager.AddParameters(6, "@IDNDERVITI", DBNull.Value, ParameterDirection.Input);
            else
                dbManager.AddParameters(6, "@IDNDERVITI", idNderVit, ParameterDirection.Input);
            if (!eshteProjektBuxhet)
                dbManager.AddParameters(7, "@IDVITIPROJEKTBUXHETI", DBNull.Value, ParameterDirection.Input);
            else
                dbManager.AddParameters(7, "@IDVITIPROJEKTBUXHETI", idNderVit, ParameterDirection.Input);

            dbManager.AddParameters(8, "@SHENIME", shenime, ParameterDirection.Input);
            dbManager.AddParameters(9, "@DtAktivizimi", dtaktivizimi, ParameterDirection.Input);
            if (idkonfigurdherpagese > 0)
                dbManager.AddParameters(10, "@IDKONFIGURDHERPAGESE", idkonfigurdherpagese, ParameterDirection.Input);
            else dbManager.AddParameters(10, "@IDKONFIGURDHERPAGESE", DBNull.Value, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_BUXHETI_upd");
            return new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
        }

        /// <summary>
        /// Ekzekuton prc_T_BUXHETI_upd per te modifikuar nje objekt clsBuxheti ne DB.
        /// <param name="idbuxheti">Id e buxhetit</param>
        /// <param name="idllojbuxheti">Id e llojit te buxhetit <seealso cref="DbCore.DbKontabiliteti.clsLlojBuxheti"/></param>
        /// <param name="idlidhese">Id lidhese</param>
        /// <param name="muaji">Muaji</param>
        /// <param name="buxh_1">Buxheti 1</param>
        /// <param name="buxh_2">Buxheti 2</param>
        /// <param name="gjen">Gjendja</param>
        /// <param name="diff1">Diferenca 1</param>
        /// <param name="diff2">Diferenca 2</param>
        /// <returns> Kthen statusin e perfundimit te ekzekutimit te SP-se perkatese (nje objekt clsMesazh qe tregon nese veprimi eshte kryer me sukses apo jo.<seealso cref="DbCore.clsMesazh"/>)</returns>
        /// </summary>
        public clsMesazh modifikoBuxhet(int idbuxheti, int idllojbuxheti, int idlidhese, String muaji, decimal buxh_1, decimal buxh_2, DateTime dtaktivizimi, int idkonfigurdherpagese)
        {
            return modifikoBuxhet(idbuxheti, idllojbuxheti, idlidhese, muaji, buxh_1, buxh_2, 0, false, "", dtaktivizimi, idkonfigurdherpagese);
        }

        public clsMesazh modifikoBuxhet(int idbuxheti, int idllojbuxheti, int idlidhese, String muaji, decimal buxh_1, decimal buxh_2, int idNderViti,DateTime dtaktivizimi,int idkonfigurdherpagese, string shenime = "")
        {
            return modifikoBuxhet(idbuxheti, idllojbuxheti, idlidhese, muaji, buxh_1, buxh_2, idNderViti, false, shenime, dtaktivizimi,idkonfigurdherpagese);
        }

        //[Obsolete("Perdor: clsMesazh modifikoBuxhet(int idbuxheti, int idllojbuxheti, int idlidhese, String muaji, decimal buxh_1, decimal buxh_2)", true)]
        //public clsMesazh modifikoBuxhet(clsBuxheti buxheti)
        //{
        //    try
        //    {
        //        dbManager.CreateParameters(6);
        //        dbManager.AddParameters(0, "@IDBUXHETI", buxheti.IdBuxheti, ParameterDirection.Input);
        //        dbManager.AddParameters(1, "@IDLLOJBUXHETI", buxheti.IdLlojBuxheti, ParameterDirection.Input);
        //        dbManager.AddParameters(2, "@IDLIDHESE", buxheti.IdLidhese, ParameterDirection.Input);
        //        dbManager.AddParameters(3, "@MUAJ", buxheti.Muaj, ParameterDirection.Input);
        //        dbManager.AddParameters(4, "@BUXHETI_1", buxheti.Buxheti_1, ParameterDirection.Input);
        //        dbManager.AddParameters(5, "@BUXHETI_2", buxheti.Buxheti_2, ParameterDirection.Input);

        //        dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_BUXHETI_upd");
        //        return new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
        //    }
        //    catch (Exception)
        //    {
        //        return new clsMesazh(true, "Ndodhi nje gabim. Ruajtja nuk u krye!");
        //    }
        //}

        /// <summary>
        /// Ekzekuton prc_T_BUXHETI_del per te fshire nje objekt clsBuxheti ne DB.
        /// <param name="idlidhese">Id lidhese</param>
        /// <returns> Kthen statusin e perfundimit te ekzekutimit te SP-se perkatese (nje objekt clsMesazh qe tregon nese veprimi eshte kryer me sukses apo jo.<seealso cref="DbCore.clsMesazh"/>)</returns>
        /// </summary>
        public clsMesazh fshiBuxhet(int idlidhese)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDLIDHESE", idlidhese, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_BUXHETI_del");
            return new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
        }

        public bool ekzistonProjektBuxhetiPerKategorine(int idLidhese, int idLlojBuxheti, int idVitiProjektBuxheti, bool eshteProjektBuxhet,DateTime dtaktivizimi)
        {
            dbManager.Open();
            dbManager.CreateParameters(5);
            dbManager.AddParameters(0, "@IDLIDHESE", idLidhese, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDLLOJBUXHETI", idLlojBuxheti, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDVITI", idVitiProjektBuxheti, ParameterDirection.Input);
            dbManager.AddParameters(3, "@eshteProjektBuxheti", eshteProjektBuxhet, ParameterDirection.Input);
            dbManager.AddParameters(4, "@dtaktivizimi", dtaktivizimi, ParameterDirection.Input);
          
            return Convert.ToBoolean(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_Buxheti_ekzistonProjektBuxhetiPerKategorine"));
        }
 public bool ekzistonBuxhetPerDaten(int idLidhese, int idLlojBuxheti, int idVitiProjektBuxheti, DateTime dt)
        {
            dbManager.Open();
            dbManager.CreateParameters(4);
            dbManager.AddParameters(0, "@IDLIDHESE", idLidhese, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDLLOJBUXHETI", idLlojBuxheti, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDVITI", idVitiProjektBuxheti, ParameterDirection.Input);
            dbManager.AddParameters(3, "@dtaktivizimi", dt, ParameterDirection.Input); 
            return Convert.ToBoolean(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_Buxheti_ekzistonBuxhetPerDaten"));
        }

        //[Obsolete("Perdor: clsMesazh fshiBuxhet(int idlidhese)", true)]
        //public clsMesazh fshiBuxhet(clsBuxheti buxheti)
        //{
        //    try
        //    {
        //        dbManager.CreateParameters(1);
        //        dbManager.AddParameters(0, "@IDLIDHESE", buxheti.IdLidhese, ParameterDirection.Input);
        //        dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_BUXHETI_del");
        //        return new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
        //    }
        //    catch (Exception)
        //    {
        //        return new clsMesazh(true, "Fshirja perfundoi me gabime!");
        //    }
        //}

        /// <summary>
        /// Ekzekuton prc_T_BUXHETI_sel per te marre nje objekt clsBuxheti ne DB duke filtruar sipas ID-se lidhese
        /// <param name="idlidhese">Id lidhese</param>
        /// </summary>
        internal void merrBuxhet(int idlidhese)
        {// metoda per te marre nje BUXHET NE BAZE TE IDLIDHESE

            dbManager.Open();
            //shtimi i parametrave
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDLIDHESE", idlidhese, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_BUXHETI_sel");
        }
        //[Obsolete("Perdor: void merrBuxhet(int idlidhese)", true)]
        //public void merrBuxhet(clsBuxheti buxheti)
        //{// metoda per te marre nje BUXHET NE BAZE TE IDLIDHESE
        //    IDBManager dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        //    dbManager.ConnectionString = dbManager.GetConnectionString();
        //    try
        //    {
        //        dbManager.Open();
        //        //shtimi i parametrave
        //        dbManager.CreateParameters(1);
        //        dbManager.AddParameters(0, "@IDLIDHESE", buxheti.IdLidhese, ParameterDirection.Input);
        //        dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_BUXHETI_sel");
        //    }
        //    catch (Exception)
        //    {
        //    }
        //    finally
        //    {
        //        dbManager.Dispose();
        //    }
        //}

        /// <summary>
        /// Ekzekuton prc_T_BUXHETI_merrGjitheBuxhetet per te marre nje datatable ne DB.
        /// <returns>Kthen nje datatable</returns>
        /// </summary>
        internal DataTable ktheGjitheBuxhetet()
        {//metoda per te marre te gjithe  buxhetet
            dbManager.Open();
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_BUXHETI_merrGjitheBuxhetet");
            return ds.Tables[0];
        }

        /// <summary>
        /// merr buxhetet per qendrat e kostos
        /// </summary>
        /// <returns></returns>
        internal DataTable ktheBuxhetetPerQendratEKostos(int idLidhese, int idNdermViti)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.Command.CommandTimeout = 0;
            dbManager.AddParameters(0, "@IDLIDHESE", idLidhese, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERVITI", idNdermViti, ParameterDirection.Input);
            return dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_BUXHETI_merrBuxhetetSipasIdLidheseQKDheIdNderViti").Tables[0];
        }
        internal DataTable ktheBuxhetetPerQendratEKostos(int idLidhese, int idNdermViti, DateTime dtakt, int idkonfigurdherpagese)
        {
            dbManager.Open();
            dbManager.CreateParameters(4);
            dbManager.Command.CommandTimeout = 0;
            dbManager.AddParameters(0, "@IDLIDHESE", idLidhese, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERVITI", idNdermViti, ParameterDirection.Input);
            dbManager.AddParameters(2, "@DTAKTIVIZIMI", dtakt, ParameterDirection.Input);   dbManager.AddParameters(3, "@IDKONFIGURDHERPAGESE", idkonfigurdherpagese, ParameterDirection.Input);
            return dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_BUXHETI_merrBuxhetetSipasIdLidheseQKDheIdNderVitiDheDate").Tables[0];
        }
        internal DataTable ktheDataBuxhetetPerQendratEKostos(int idLidhese, int idNdermViti, int idllojbuxheti, int idviti, bool eshteprojekt)
        {
            dbManager.Open();
            dbManager.CreateParameters(5);
            dbManager.Command.CommandTimeout = 0;
            dbManager.AddParameters(0, "@IDLIDHESE", idLidhese, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERVITI", idNdermViti, ParameterDirection.Input);    
            dbManager.AddParameters(2, "@idllojbuxheti", idllojbuxheti, ParameterDirection.Input);
            dbManager.AddParameters(3, "@idviti", idviti, ParameterDirection.Input);
            dbManager.AddParameters(4, "@eshteprojekt", eshteprojekt, ParameterDirection.Input);
            return dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_buxheti_merrGjitheDatatSipasIdQKDheIdNderViti").Tables[0];
        }
        

        /// <summary>
        /// Ekzekuton prc_T_BUXHETI_merrBuxhetetSipasIdLidheseIdLlojBuxheti per te marre nje datatable ne DB duke filtruar sipas ID-se lidhese dhe ID-se se llojit te buxhetit.
        /// <param name="id">Id lidhese</param>
        /// <param name="idlloj">Id e llojit te buxhetit</param>
        /// <returns>Kthen nje datatable</returns>
        /// </summary>
        internal DataTable ktheBuxhetetSipasIdLidheseIdLlojBuxheti(int id, int idlloj)
        {//metoda per te marre te gjithe  buxhetet sipas id lidhese dhe llojit te buxhetit

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.Command.CommandTimeout = 0;
            dbManager.AddParameters(0, "@IDLIDHESE", id, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDLLOJBUXHETI", idlloj, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_BUXHETI_merrBuxhetetSipasIdLidheseIdLlojBuxheti");
            return ds.Tables[0];
        }

        /// <summary>
        /// Ekzekuton prc_T_BUXHETI_merrBuxhetetSipasIdLidheseKategoriShpenzimi per te marre nje datatable ne DB duke filtruar sipas ID-se lidhese dhe ID-se se llojit te buxhetit.
        /// <param name="id">Id lidhese</param>
        /// <param name="idlloj">Id e llojit te buxhetit</param>
        /// <param name="idNderViti">Nese eshte projekt buxhet kalohet si parameter IDVITI te tabela T_VITET_PROJEKTBUXHETI(kolona IDVITI te tbl T_BUXHETI), 
        /// perndr eshte IDNDERVITI te tabela T_NDERMARJEVITI qe eshte fusha IDNDERVITI te tabela T_BUXHETI</param>
        /// <param name="merrProjektBuxhetet">true kur lloji eshte Projekt buxhet dhe false kur lloji eshte Buxhet</param>
        /// <returns>Kthen nje datatable</returns>
        /// </summary>
        internal DataTable ktheBuxhetetSipasIdLidheseKategoriShpenzimi(int id, int idlloj, int idNderViti, bool merrProjektBuxhetet,DateTime dtAktivizimi)
        {//metoda per te marre te gjithe  buxhetet sipas id lidhese dhe llojit te buxhetit te kategorise se shpenzimit

            dbManager.Open();
            dbManager.CreateParameters(5);
            dbManager.Command.CommandTimeout = 0;
            dbManager.AddParameters(0, "@IDLIDHESE", id, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDLLOJBUXHETI", idlloj, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDNDERVITI", idNderViti, ParameterDirection.Input);
            dbManager.AddParameters(3, "@merrProjektBuxhetet", merrProjektBuxhetet, ParameterDirection.Input);
            dbManager.AddParameters(4, "@dtaktivizimi", dtAktivizimi, ParameterDirection.Input); 
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_BUXHETI_merrBuxhetetSipasIdLidheseKategoriShpenzimi");
            return ds.Tables[0];
        }

       
        internal DataTable ktheBuxhetetSipasIdLidheseDheDateAktivizimi(int id, int idNderViti, DateTime dtAktivizimi)
        {//metoda per te marre te gjithe  buxhetet sipas id lidhese dhe llojit te buxhetit te kategorise se shpenzimit

            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.Command.CommandTimeout = 0;
            dbManager.AddParameters(0, "@IDLIDHESE", id, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERVITI", idNderViti, ParameterDirection.Input);
            dbManager.AddParameters(2, "@dtaktivizimi", dtAktivizimi, ParameterDirection.Input);      
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_BUXHETI_merrBuxhetetSipasIdLidheseDheDateAktivizimi");
            return ds.Tables[0];
        }

        /// <summary>
        /// Ekzekuton prc_T_BUXHETI_merrBuxhetetSipasIdLidheseKodLlojBuxheti per te marre nje datatable ne DB duke filtruar sipas ID-se lidhese dhe kodit te llojit te buxhetit.
        /// <param name="id">Id lidhese</param>
        /// <param name="kodlloj">kodi i llojit te buxhetit</param>
        /// <returns>Kthen nje datatable</returns>
        /// </summary>
        internal DataTable ktheBuxhetetSipasIdLidheseKodLlojBuxheti(int id, string kodlloj)
        {//metoda per te marre te gjithe  buxhetet sipas id lidhese dhe llojit te buxhetit

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.Command.CommandTimeout = 0;
            dbManager.AddParameters(0, "@IDLIDHESE", id, ParameterDirection.Input);
            dbManager.AddParameters(1, "@KODLLOJBUXHETI", kodlloj, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_BUXHETI_merrBuxhetetSipasIdLidheseKodLlojBuxheti");
            return ds.Tables[0];
        }

        internal DataTable ktheBuxhetetSipasIdLidheseIdLlojBuxhetiDheIdNderviti(int id, int idlloj, int idnderviti, DateTime dt)
        {//metoda per te marre te gjithe  buxhetet sipas id lidhese dhe llojit te buxhetit

            dbManager.Open();
            dbManager.CreateParameters(4);
            dbManager.Command.CommandTimeout = 0;
            dbManager.AddParameters(0, "@IDLIDHESE", id, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDLLOJBUXHETI", idlloj, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDNDERVITI", idnderviti, ParameterDirection.Input);
            dbManager.AddParameters(3, "@dtaktivizimi", dt, ParameterDirection.Input);
          
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_BUXHETI_merrBuxhetetSipasIdLidheseIdLlojBuxhetiDheIdNderviti");
            return ds.Tables[0];
        }
        //[Obsolete("Perdor: DataTable ktheBuxhetetSipasIdLidheseIdLlojBuxheti(int id, int idlloj)", true)]
        //public colBuxhetet merrBuxhetetSipasIdLidheseIdLlojBuxheti(int id, int idlloj)
        //{//metoda per te marre te gjithe  buxhetet sipas id lidhese dhe llojit te buxhetit
        //    IDBManager dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        //    dbManager.ConnectionString = dbManager.GetConnectionString();
        //    try
        //    {
        //        dbManager.Open();
        //        dbManager.CreateParameters(2);
        //        dbManager.Command.CommandTimeout = 0;
        //        dbManager.AddParameters(0, "@IDLIDHESE", id, ParameterDirection.Input);
        //        dbManager.AddParameters(1, "@IDLLOJBUXHETI", idlloj, ParameterDirection.Input);
        //        DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_BUXHETI_merrBuxhetetSipasIdLidheseIdLlojBuxheti");
        //        colBuxhetet buxhetete = new colBuxhetet();
        //        return buxhetete.mbushArrayListBuxhetet(ds);
        //    }
        //    catch (Exception)
        //    {
        //        return new colBuxhetet();
        //    }
        //    finally
        //    {
        //        dbManager.Dispose();
        //    }

        //}

        #endregion

        /// <summary>
        /// Metodat e meposhtme i perkasin veprimeve qe kryhen nga objektet clsKPF dhe colKPFte per Llogarite standarte
        /// </summary>
        #region KPF

        /// <summary>
        /// Ekzekuton prc_T_KPF_ins per te ruajtur nje objekt clsKPF ne DB.
        /// <param name="idkpf">id e kpf-se</param>
        /// <param name="kodikpf">kodi i kpf-se</param>
        /// <param name="nivelikpf">niveli i kpf-se</param>
        /// <param name="emertimikpf">emertimi i kpf-se</param>
        /// <param name="inakt">aktive ose inaktive</param>
        /// <param name="shenimekpf">shenime</param>
        /// <param name="grupikpf">grupi i kpfse</param>
        /// <param name="idndermarje">id e ndermarrjes</param>
        /// <param name="idndervit">id qe lidh ndermarrjen me vitin</param>
        /// <param name="idperdoruesi">id e perdoruesit</param>
        /// <param name="idkonfig">id konfigurimit</param>
        /// <returns> Kthen statusin e perfundimit te ekzekutimit te SP-se perkatese (nje objekt clsMesazh qe tregon nese veprimi eshte kryer me sukses apo jo.<seealso cref="DbCore.clsMesazh"/>)</returns>
        /// </summary>
        internal clsMesazh ruajKPF(out int idkpf, string kodikpf, int nivelikpf, string emertimikpf, bool inakt, string shenimekpf, int grupikpf, int idndermarje,
            int idperdoruesi, int idkonfig, int idstatusdok, string emertimikpf_fr)
        {
            idkpf = -1;

            dbManager.Open();
            dbManager.CreateParameters(12);
            dbManager.AddParameters(0, "@IDKPF", idkpf, ParameterDirection.Output);
            dbManager.AddParameters(1, "@KODIKPF", kodikpf, ParameterDirection.Input);
            dbManager.AddParameters(2, "@NIVELIKPF", nivelikpf, ParameterDirection.Input);
            dbManager.AddParameters(3, "@EMERTIMIKPF", emertimikpf, ParameterDirection.Input);
            dbManager.AddParameters(4, "@INAKTIV", inakt, ParameterDirection.Input);
            dbManager.AddParameters(5, "@SHENIMEKPF", shenimekpf, ParameterDirection.Input);
            dbManager.AddParameters(6, "@GRUPIKPF", grupikpf, ParameterDirection.Input);
            dbManager.AddParameters(7, "@IDNDERMARJE", idndermarje, ParameterDirection.Input);
            dbManager.AddParameters(8, "@IDPERDORUESI", idperdoruesi, ParameterDirection.Input);
            dbManager.AddParameters(9, "@IDKONFIG", idkonfig, ParameterDirection.Input);
            dbManager.AddParameters(10, "@IDSTATUSDOK", idstatusdok, ParameterDirection.Input);
            dbManager.AddParameters(11, "@EMERTIMIKPF_fr", emertimikpf_fr, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_KPF_ins");
            idkpf = int.Parse(dbManager.Parameters[0].Value.ToString());
            return new clsMesazh(true, "Ruajtja pefundoi me sukses!");
        }
        
        /// <summary>
        /// Ekzekuton prc_T_KPF_upd per te modifikuar nje objekt clsKPF ne DB.
        /// <param name="idkpf">id e kpf-se</param>
        /// <param name="kodikpf">kodi i kpf-se</param>
        /// <param name="nivelikpf">niveli i kpf-se</param>
        /// <param name="emertimikpf">emertimi i kpf-se</param>
        /// <param name="inakt">aktive ose inaktive</param>
        /// <param name="shenimekpf">shenime</param>
        /// <param name="grupikpf">grupi i kpfse</param>
        /// <param name="idndermarje">id e ndermarrjes</param>
        /// <param name="idndervit">id qe lidh ndermarrjen me vitin</param>
        /// <param name="idperdoruesi">id e perdoruesit</param>
        /// <param name="idkonfig">id konfigurimit</param>
        /// <returns> Kthen statusin e perfundimit te ekzekutimit te SP-se perkatese (nje objekt clsMesazh qe tregon nese veprimi eshte kryer me sukses apo jo.<seealso cref="DbCore.clsMesazh"/>)</returns>
        /// </summary>
        internal clsMesazh modifikoKPF(int idkpf, string kodikpf, int nivelikpf, string emertimikpf, bool inakt, string shenimekpf, int grupikpf, int idndermarje,
            int idperdoruesi, int idkonfig, int idstatusdok, string emertimikpf_fr)
        {
            dbManager.Open();
            dbManager.CreateParameters(12);
            dbManager.AddParameters(0, "@IDKPF", idkpf, ParameterDirection.Input);
            dbManager.AddParameters(1, "@KODIKPF", kodikpf, ParameterDirection.Input);
            dbManager.AddParameters(2, "@NIVELIKPF", nivelikpf, ParameterDirection.Input);
            dbManager.AddParameters(3, "@EMERTIMIKPF", emertimikpf, ParameterDirection.Input);
            dbManager.AddParameters(4, "@INAKTIV", inakt, ParameterDirection.Input);
            dbManager.AddParameters(5, "@SHENIMEKPF", shenimekpf, ParameterDirection.Input);
            dbManager.AddParameters(6, "@GRUPIKPF", grupikpf, ParameterDirection.Input);
            dbManager.AddParameters(7, "@IDNDERMARJE", idndermarje, ParameterDirection.Input);
            dbManager.AddParameters(8, "@IDPERDORUESI", idperdoruesi, ParameterDirection.Input);
            dbManager.AddParameters(9, "@IDKONFIG", idkonfig, ParameterDirection.Input);
            dbManager.AddParameters(10, "@IDSTATUSDOK", idstatusdok, ParameterDirection.Input);
            dbManager.AddParameters(11, "@EMERTIMIKPF_fr", emertimikpf_fr, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_KPF_upd");
            return new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
        }

        /// <summary>
        /// Ekzekuton prc_T_KPF_del per te fshire nje objekt clsKPF ne DB duke filtruar sipas ID-se se KPF-se.
        /// <param name="idKPF">id e kpf-se</param>
        /// <returns> Kthen statusin e perfundimit te ekzekutimit te SP-se perkatese (nje objekt clsMesazh qe tregon nese veprimi eshte kryer me sukses apo jo.<seealso cref="DbCore.clsMesazh"/>)</returns>
        /// </summary>
        internal clsMesazh fshiKPF(int idKPF)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDKPF", idKPF, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_KPF_del");
            return new clsMesazh(true, "Fshirja perfundoi me sukse!");
        }

        internal clsMesazh fshiKPFStatus(int idKPF, int idperdoruesi)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDKPF", idKPF, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUESI", idperdoruesi, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_KPF_upddel");
            return new clsMesazh(true, "Fshirja perfundoi me sukse!");
        }
        

        /// <summary>
        /// Ekzekuton prc_T_KPF_sel per te marre nje objekt clsKPF ne DB duke filtruar sipas kodit te KPF-se.
        /// <param name="kodikpf">kodi i kpf-se</param>
        /// </summary>
        internal void merrKPF(string kodikpf)
        {// metoda per te marre nje kpf
            dbManager.Open();
            //shtimi i parametrave
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@KODKPF", kodikpf, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_KPF_sel");
        }

        internal DataTable ktheGrupimKlienteFurnitorePerEksport(int idndermarje)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDNDERMARJE", idndermarje, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_GRUPEKF_ktheGrupimKlienteFurnitorePerEksport");
            if (ds == null)
                return null;
            return ds.Tables[0];

        }


        //[Obsolete("Perdor: void merrKPF(string kodikpf)", true)]
        //public void merrKPF(clsKPF KPF)
        //{// metoda per te marre nje kpf
        //        dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        //        dbManager.ConnectionString = dbManager.GetConnectionString();
        //    try
        //    {
        //        dbManager.Open();
        //        //shtimi i parametrave
        //        dbManager.CreateParameters(1);
        //        dbManager.AddParameters(0, "@KODKPF", KPF.KodiKPF, ParameterDirection.Input);
        //        dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_KPF_sel");
        //    }
        //    catch (Exception)
        //    {
        //    }
        //    finally
        //    {
        //        dbManager.Dispose();
        //    }
        //}

        /// <summary>
        /// Ekzekuton nje transaksion per te fshire nje objekt clsKPF dhe autorizimet e lidhur me te.
        /// Therret funksionin <see cref="DbAdmin.clsDatabaseAdmin.fshiLidhjeAutorizim"/>
        /// Therret funksionin <see cref="DbCore.DbKontabiliteti.clsDatabaseKontabilitet.fshiKPF"/>
        /// <param name="KPF">Objekt i tipit clsKPF</param>
        /// <returns> Kthen statusin e perfundimit te ekzekutimit te transaksionit (nje objekt clsMesazh qe tregon nese veprimi eshte kryer me sukses apo jo.<seealso cref="DbCore.clsMesazh"/>)</returns>
        /// </summary>
        //[Obsolete("Perdor nga klasa perkatese: clsMesazh fshiKPFAndBuxhete(int idkpf)", true)]
        //public clsMesazh fshiKPFAndBuxhete(clsKPF KPF)
        //{//fshin KPFne dhe buxhetet perkatese
        //    //colLlojeBuxhetesh colLloj = merrLlojBuxhetiSipasKodit("KPF");
        //    DbAdmin.colLidhjetAutorizim colLidhjeAutorizim = new DbAdmin.colLidhjetAutorizim(KPF.IdKPF, DbCore.DbKontabiliteti.clsLlojBuxheti.mbushIDLlojBuxheti("KPF"));
        //    //DbAdmin.colLidhjetAutorizim colLidhjeAutorizim = new DbAdmin.clsDatabaseAdmin().merrLidhjeAutorizimSipasIdLidheseIdLloji(KPF.IdKPF, DbCore.DbKontabiliteti.clsLlojBuxheti.mbushIDLlojBuxheti("KPF"));
        //    clsMesazh mesazh = new clsMesazh();
        //    DbCore.clsMesazh mesazhAdmin = new DbCore.clsMesazh(true);
        //    dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        //    dbManager.ConnectionString = dbManager.GetConnectionString();
        //    dbManager.Open();
        //    dbManager.BeginTransaction();
        //    try
        //    {

        //        foreach (DbAdmin.clsLidhjeAutorizim o in colLidhjeAutorizim)
        //        {
        //            if (mesazhAdmin.Status)
        //            {
        //                mesazhAdmin = new DbAdmin.clsDatabaseAdmin().fshiLidhjeAutorizim(o.IdLidhjeAutorizim);
        //            }
        //            else
        //            {
        //                dbManager.Transaction.Rollback();
        //                mesazh.Status = false;
        //                mesazh.PershkrimMesazhi = mesazhAdmin.PershkrimMesazhi;
        //                return mesazh;
        //            }
        //        }
        //        if (mesazhAdmin.Status)
        //        {
        //            mesazh= fshiKPF(KPF.IdKPF);
        //            if (mesazh.Status)
        //            {
        //                dbManager.CommitTransaction();
        //                mesazh = new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
        //                return mesazh;
        //            }
        //            else
        //            {
        //                dbManager.Transaction.Rollback();
        //                return mesazh;
        //            }
        //        }
        //        else
        //        {
        //            dbManager.Transaction.Rollback();
        //            mesazh.Status = false;
        //            mesazh.PershkrimMesazhi = mesazhAdmin.PershkrimMesazhi;
        //            return mesazh;
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        dbManager.Transaction.Rollback();
        //        return new clsMesazh(false, ce.Message);
        //    }
        //    finally
        //    {
        //        dbManager.Dispose();
        //    }
        //}

        ///// <summary>
        /// Ekzekuton nje transaksion per te ruajtur nje objekt clsKPF dhe autorizimet e lidhur me te.
        /// Therret funksionin <see cref="DbCore.DbKontabiliteti.clsDatabaseKontabilitet.ruajKPF"/>
        /// Therret funksionin <see cref="DbAdmin.clsDatabaseAdmin.ruajLidhjeAutorizim"/>        
        /// <param name="KPF">Objekt i tipit clsKPF</param>
        /// <returns> Kthen statusin e perfundimit te ekzekutimit te transaksionit (nje objekt clsMesazh qe tregon nese veprimi eshte kryer me sukses apo jo.<seealso cref="DbCore.clsMesazh"/>)</returns>
        /// </summary>
        //[Obsolete("Perdor nga klasa perkatese: clsMesazh ruajKPFAndBuxhete(int idkpf, string kodikpf, int nivelikpf, string emertimikpf, bool inakt, string shenimekpf, int grupikpf, int idnder, int idndervit, " +
        //    "int idperdoruesi, int idkonfig, int idndermarje)", true)]
        //public clsMesazh ruajKPFAndBuxhete(clsKPF KPF, int idndermarje)
        //{//ruan KPFne dhe buxhetet perkatese
        //    //colLlojeBuxhetesh colLloj = merrLlojBuxhetiSipasKodit("KPF");
        //    clsMesazh mesazh = new clsMesazh();
        //    DbCore.clsMesazh mesazhAdmin = new DbCore.clsMesazh(true);
        //    dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        //    dbManager.ConnectionString = dbManager.GetConnectionString();
        //    dbManager.Open();
        //    dbManager.BeginTransaction();
        //    try
        //    {
        //        int idK;
        //        mesazh = ruajKPF(out idK, KPF.KodiKPF, KPF.NiveliKPF, KPF.EmertimiKPF, KPF.Inaktiv, KPF.ShenimeKPF, KPF.GrupiKPF, KPF.IdNdermarje, KPF.IdNderViti, KPF.IdPerdoruesi, KPF.IdKonfig);
        //        if (mesazh.Status)
        //        {
        //            KPF.IdKPF = idK;
        //            DbAdmin.clsDatabaseAdmin dbAdmin = new DbAdmin.clsDatabaseAdmin();
        //            int idregj = DbAdmin.clsListeAmbjenteCeljeRegjistrim.ktheIdCR("CKPF");
        //            //int idregj = dbAdmin.ktheListeAmbjentiCeljeRegjistrim("CKPF")[0].IdCR;
        //            int idlloji = DbAdmin.clsLlojKodi.ktheIDLlojKodi("Kod");
        //            //int idlloji = dbAdmin.ktheLlojKodi("Kod")[0].IdLlojKodi;
        //            if (new DbAdmin.clsACRNumraAutomatike(idregj, idlloji, idndermarje).IdLidhjeNrAuto != 0)
        //            {
        //                DbAdmin.clsACRNumraAutomatike ACR = new DbAdmin.clsACRNumraAutomatike(idregj, idlloji, idndermarje);
        //                DbAdmin.clsNrAutom NrAutom = new DbAdmin.clsNrAutom(ACR.IdNumraAutoLidhje);
        //                //DbAdmin.clsNrAutom NrAutom = dbAdmin.ktheNrAutom(ACR.IdNumraAutoLidhje)[0];
        //                int karakteremajtas = NrAutom.MajtasNrAutom.Length;
        //                int karakteredjathtas = ACR.VleraFunditLidhje.Length - NrAutom.DjathtasNrAutom.Length - karakteremajtas;
        //                string vle = ACR.VleraFunditLidhje.Substring(karakteremajtas, karakteredjathtas);
        //                string vlera = NrAutom.gjeneroNumrinAutomatikPasardhes(vle);
        //                ACR.VleraFunditLidhje = vlera;
        //                mesazhAdmin=  ACR.modifiko();
        //            }
        //            if (mesazhAdmin.Status)
        //            {
        //                if (KPF.IdAutorizimi != "")
        //                {
        //                    DbAdmin.colLidhjetAutorizim colLidhjet = new DbAdmin.colLidhjetAutorizim();
        //                    string[] pars1 = KPF.IdAutorizimi.Split(',');
        //                    for (int i = 0; i < pars1.Length; i++)
        //                    {
        //                        DbAdmin.clsLidhjeAutorizim lidhje = new DbAdmin.clsLidhjeAutorizim();
        //                        lidhje.IdAutorizimeKoka = DbAdmin.clsAutorizimKoka.ktheIDAutorizim(pars1[i]);
        //                        //lidhje.IdAutorizimeKoka = new DbAdmin.clsDatabaseAdmin().ktheAutorizim(pars1[i])[0].IdAutorizimKoka;
        //                        colLidhjet.Add(lidhje);
        //                    }
        //                    foreach (DbAdmin.clsLidhjeAutorizim o in colLidhjet)
        //                    {
        //                        if (mesazhAdmin.Status)
        //                        {
        //                            o.IdLloji = DbCore.DbKontabiliteti.clsLlojBuxheti.mbushIDLlojBuxheti("KPF");
        //                            o.IdLidhese = KPF.IdKPF;
        //                            mesazhAdmin = dbAdmin.ruajLidhjeAutorizim(o.IdLidhjeAutorizim, o.IdLidhese, o.IdLloji, o.IdAutorizimeKoka);
        //                        }
        //                        else
        //                        {
        //                            dbManager.Transaction.Rollback();
        //                            mesazh.Status = false;
        //                            mesazh.PershkrimMesazhi = mesazhAdmin.PershkrimMesazhi;
        //                            return mesazh;
        //                        }
        //                    }
        //                }
        //                if (mesazhAdmin.Status)
        //                {
        //                    dbManager.CommitTransaction();
        //                    mesazh = new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
        //                    return mesazh;
        //                }
        //                else
        //                {
        //                    dbManager.Transaction.Rollback();
        //                    mesazh.Status = false;
        //                    mesazh.PershkrimMesazhi = mesazhAdmin.PershkrimMesazhi;
        //                    return mesazh;
        //                }
        //            }
        //            else
        //            {
        //                dbManager.Transaction.Rollback();
        //                mesazh.Status = false;
        //                mesazh.PershkrimMesazhi = mesazhAdmin.PershkrimMesazhi;
        //                return mesazh;
        //            }
        //        }
        //        else
        //        {
        //            dbManager.Transaction.Rollback();
        //            return mesazh;
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        dbManager.Transaction.Rollback();
        //        return new clsMesazh(false, ce.Message);
        //    }
        //    finally
        //    {
        //        dbManager.Dispose();
        //    }
        //}

        /// <summary>
        /// Ekzekuton nje transaksion per te modifikuar nje objekt clsKPF dhe autorizimet e lidhur me te.
        /// Therret funksionin <see cref="DbCore.DbKontabiliteti.clsDatabaseKontabilitet.modifikoKPF"/>

        /// Therret funksionet <see cref="DbAdmin.clsDatabaseAdmin.ruajLidhjeAutorizim"/>, <see cref="DbAdmin.clsDatabaseAdmin.modifikoLidhjeAutorizim"/>, 
        /// <see cref="DbAdmin.clsDatabaseAdmin.fshiLidhjeAutorizim"/> sipas rasteve nese jane shtuar rreshta trupi apo jane fshire rreshta trupi  
        /// <param name="KPF">Objekt i tipit clsKPF</param>
        /// <returns> Kthen statusin e perfundimit te ekzekutimit te transaksionit (nje objekt clsMesazh qe tregon nese veprimi eshte kryer me sukses apo jo.<seealso cref="DbCore.clsMesazh"/>)</returns>
        /// </summary>
        //[Obsolete("Perdor nga klasa perkatese: clsMesazh modifikoKPFAndBuxhete(int idkpf, string kodikpf, int nivelikpf, string emertimikpf, bool inakt, string shenimekpf, int grupikpf, int idnder, int idndervit, " +
        //    "int idperdoruesi, int idkonfig)", true)]
        //public clsMesazh modifikoKPFAndBuxhete(clsKPF KPF)
        //{
        //    //colLlojeBuxhetesh colLloj = merrLlojBuxhetiSipasKodit("KPF");
        //    //DbAdmin.colLidhjetAutorizim colLidhjetAutorizim = new DbAdmin.clsDatabaseAdmin().merrLidhjeAutorizimSipasIdLidheseIdLloji(KPF.IdKPF, DbCore.DbKontabiliteti.clsLlojBuxheti.mbushIDLlojBuxheti("KPF"));
        //    DbAdmin.colLidhjetAutorizim colLidhjetAutorizim = new DbAdmin.colLidhjetAutorizim(KPF.IdKPF, DbCore.DbKontabiliteti.clsLlojBuxheti.mbushIDLlojBuxheti("KPF"));
        //    clsMesazh mesazh = new clsMesazh();
        //    DbCore.clsMesazh mesazhAdmin = new DbCore.clsMesazh(true);
        //    //modifikon llogarine dhe buxhetet perkatese
        //    dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        //    dbManager.ConnectionString = dbManager.GetConnectionString();
        //    dbManager.Open();
        //    dbManager.BeginTransaction();
        //    try
        //    {
        //        mesazh = modifikoKPF(KPF.IdKPF, KPF.KodiKPF, KPF.NiveliKPF, KPF.EmertimiKPF, KPF.Inaktiv, KPF.ShenimeKPF, KPF.GrupiKPF, KPF.IdNdermarje, KPF.IdNderViti, KPF.IdPerdoruesi, KPF.IdKonfig);
        //        if (mesazh.Status)
        //        {
        //            DbAdmin.colLidhjetAutorizim colLidhjet = new DbAdmin.colLidhjetAutorizim();
        //            if (KPF.IdAutorizimi != "")
        //            {

        //                string[] pars1 = KPF.IdAutorizimi.Split(',');
        //                for (int i = 0; i < pars1.Length; i++)
        //                {
        //                    DbAdmin.clsLidhjeAutorizim lidhje = new DbAdmin.clsLidhjeAutorizim();
        //                    lidhje.IdAutorizimeKoka = DbAdmin.clsAutorizimKoka.ktheIDAutorizim(pars1[i]);
        //                    //lidhje.IdAutorizimeKoka = new DbAdmin.clsDatabaseAdmin().ktheAutorizim(pars1[i])[0].IdAutorizimKoka;
        //                    colLidhjet.Add(lidhje);
        //                }

        //            }
        //            if (colLidhjetAutorizim.Count < colLidhjet.Count)//rasti kur jane shtuar rreshta trupi
        //            {
        //                for (int i = 0; i < colLidhjet.Count; i++)
        //                {
        //                    if (mesazhAdmin.Status)
        //                    {
        //                        colLidhjet[i].IdLloji = DbCore.DbKontabiliteti.clsLlojBuxheti.mbushIDLlojBuxheti("KPF");
        //                        colLidhjet[i].IdLidhese = KPF.IdKPF;
        //                        if (i < colLidhjetAutorizim.Count)
        //                        {

        //                            colLidhjet[i].IdLidhjeAutorizim = colLidhjetAutorizim[i].IdLidhjeAutorizim;
        //                            mesazhAdmin = new DbAdmin.clsDatabaseAdmin().modifikoLidhjeAutorizim(colLidhjet[i].IdLidhjeAutorizim, colLidhjet[i].IdLidhese, colLidhjet[i].IdLloji, colLidhjet[i].IdAutorizimeKoka);
        //                        }
        //                        else
        //                            mesazhAdmin = new DbAdmin.clsDatabaseAdmin().ruajLidhjeAutorizim(colLidhjet[i].IdLidhjeAutorizim, colLidhjet[i].IdLidhese, colLidhjet[i].IdLloji, colLidhjet[i].IdAutorizimeKoka);
        //                    }
        //                    else
        //                    {
        //                        dbManager.Transaction.Rollback();
        //                        mesazh.Status = false;
        //                        mesazh.PershkrimMesazhi = mesazhAdmin.PershkrimMesazhi;
        //                        return mesazh;
        //                    }
        //                }
        //            }
        //            else//rasti kur jane fshire rreshta
        //            {
        //                int count = 0;
        //                for (int i = 0; i < colLidhjetAutorizim.Count; i++)
        //                {
        //                    if (mesazhAdmin.Status)
        //                    {
        //                        if (count < colLidhjet.Count)
        //                        {
        //                            colLidhjet[i].IdLloji = DbCore.DbKontabiliteti.clsLlojBuxheti.mbushIDLlojBuxheti("KPF");
        //                            colLidhjet[i].IdLidhese = KPF.IdKPF;

        //                            colLidhjet[i].IdLidhjeAutorizim = colLidhjetAutorizim[i].IdLidhjeAutorizim;
        //                            mesazhAdmin = new DbAdmin.clsDatabaseAdmin().modifikoLidhjeAutorizim(colLidhjet[i].IdLidhjeAutorizim, colLidhjet[i].IdLidhese, colLidhjet[i].IdLloji, colLidhjet[i].IdAutorizimeKoka);
        //                        }
        //                        else
        //                        {
        //                            mesazhAdmin = new DbAdmin.clsDatabaseAdmin().fshiLidhjeAutorizim(colLidhjetAutorizim[i].IdLidhjeAutorizim);
        //                        }
        //                        count++;
        //                    }
        //                    else
        //                    {
        //                        dbManager.Transaction.Rollback();
        //                        mesazh.Status = false;
        //                        mesazh.PershkrimMesazhi = mesazhAdmin.PershkrimMesazhi;
        //                        return mesazh;
        //                    }
        //                }
        //            }
        //            if (mesazhAdmin.Status)
        //            {
        //                dbManager.CommitTransaction();
        //                mesazh = new clsMesazh(true, MessagesResource.Messages["msgModifikimiMeSukses"]);
        //                return mesazh;
        //            }
        //            else
        //            {
        //                dbManager.Transaction.Rollback();
        //                mesazh.Status = false;
        //                mesazh.PershkrimMesazhi = mesazhAdmin.PershkrimMesazhi;
        //                return mesazh;
        //            }
        //        }
        //        else
        //        {
        //            dbManager.Transaction.Rollback();
        //            return mesazh;
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        dbManager.Transaction.Rollback();
        //        return new clsMesazh(false, ce.Message);
        //    }
        //    finally
        //    {
        //        dbManager.Dispose();
        //    }
        //}

        /// <summary>
        /// Ekzekuton prc_T_KPF_merrGjitheKPFteSipasGrupit per te marre nje datatable duke filtruar sipas gripitKPF dhe id-se lidhese ndermarrje-vit.
        /// <param name="grupiKpf">Grupi KPF</param>
        /// <param name="idNdermVit">Id lidhese ndermarrje - vit</param>
        /// <returns> Kthen nje datatable qe plotesojne kushtet</returns>
        /// </summary>
        internal DataTable ktheGjitheKPFteSipasGrupit(int grupiKpf, int idndermarje)
        {//metoda per te marre te gjithe  kpf sipas grupit

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@GRUPIKPF", grupiKpf, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idndermarje, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KPF_merrGjitheKPFteSipasGrupit");
            return ds.Tables[0];
        }
        //[Obsolete("Perdor: DataTable ktheGjitheKPFteSipasGrupit(int grupiKpf, int idNdermVit)", true)]
        //public colKPFte merrGjitheKPFteSipasGrupit(int grupiKpf, int idNdermVit)
        //{//metoda per te marre te gjithe  kpf sipas grupit
        //    IDBManager dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        //    dbManager.ConnectionString = dbManager.GetConnectionString();
        //    try
        //    {
        //        dbManager.Open();
        //        dbManager.CreateParameters(2);
        //        dbManager.AddParameters(0, "@GRUPIKPF", grupiKpf, ParameterDirection.Input);
        //        dbManager.AddParameters(1, "@IDNDERVITI", idNdermVit, ParameterDirection.Input);
        //        DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KPF_merrGjitheKPFteSipasGrupit");
        //        colKPFte KPFte = new colKPFte();
        //        return KPFte.mbushArrayListKPFte(ds);
        //    }
        //    catch (Exception)
        //    {
        //        return new colKPFte();
        //    }
        //    finally
        //    {
        //        dbManager.Dispose();
        //    }
        //}

        /// <summary>
        /// Ekzekuton prc_T_KPF_ktheKPF per te marre nje datarow duke filtruar sipas id-se se KPF.
        /// <param name="id">ID e KPF</param>
        /// <returns> Kthen datarwow qe plotesojne kushtin</returns>
        /// </summary>
        internal DataRow merrKPF(int id)
        {//kthen kpf me kete id
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDKPF", id, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KPF_ktheKPF");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];

        }
        //[Obsolete("Perdor: DataRow merrKPF(int id)", true)]
        //public colKPFte ktheKPF(int id)
        //{//kthen kpf me kete id
        //    IDBManager dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        //    dbManager.ConnectionString = dbManager.GetConnectionString();
        //    try
        //    {
        //        dbManager.Open();
        //        dbManager.CreateParameters(1);
        //        dbManager.AddParameters(0, "@IDKPF", id, ParameterDirection.Input);
        //        DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KPF_ktheKPF");
        //        colKPFte colKPFte = new colKPFte();
        //        return colKPFte.mbushArrayListKPFte(ds);
        //    }
        //    catch (Exception)
        //    {
        //        return new colKPFte();
        //    }
        //    finally
        //    {
        //        dbManager.Dispose();
        //    }
        //}

        /// <summary>
        /// Ekzekuton prc_T_KPF_ktheKPFSipasKodit per te marre id e kpf-se duke filtruar sipas kodit te KPF dhe ID-se se ndermarrjes.
        /// <param name="kodi">Kodi i KPF</param>
        /// <param name="idnderm">Id e ndermarrjes</param>
        /// <returns> Kthen id e kpf-se qe plotesojne kushtet</returns>
        /// </summary>
        internal int merrKPF(string kodi, int idnderm, int grupi)
        {//kthen kpf me kete kod

            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@KODIKPF", kodi, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            dbManager.AddParameters(2, "@GRUPI", grupi, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KPF_ktheKPFSipasKodit");
            if (ds == null)
                return -1;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return -1;
            int idKPF;
            int.TryParse(ds.Tables[0].Rows[0]["IDKPF"].ToString(), out idKPF);
            return idKPF;
        }
        internal DataRow merrKPFSipasKodit(string kodi, int idnderm, int grupi)
        {//kthen kpf me kete kod

            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@KODIKPF", kodi, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            dbManager.AddParameters(2, "@GRUPI", grupi, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KPF_ktheKPFSipasKodit");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];
        }
        //[Obsolete("Perdor: int merrKPF(string kodi, int idnderm)", true)]
        //public colKPFte ktheKPF(string kodi, int idnderm)
        //{//kthen kpf me kete kod
        //    IDBManager dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        //    dbManager.ConnectionString = dbManager.GetConnectionString();
        //    try
        //    {
        //        dbManager.Open();
        //        dbManager.CreateParameters(2);
        //        dbManager.AddParameters(0, "@KODIKPF", kodi, ParameterDirection.Input);
        //        dbManager.AddParameters(1, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
        //        DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KPF_ktheKPFSipasKodit");
        //        colKPFte colKPFte = new colKPFte();
        //        return colKPFte.mbushArrayListKPFte(ds);
        //    }
        //    catch (Exception)
        //    {
        //        return new colKPFte();
        //    }
        //    finally
        //    {
        //        dbManager.Dispose();
        //    }
        //}

        /// <summary>
        /// Ekzekuton prc_T_KPF_ekzistonKPF per te kontrolluar nese ekziston ne DB nje objekt clsKPF duke filtruar sipas kodit te KPF dhe ID-se se ndermarrjes.
        /// <param name="kodikpf">Kodi i KPF</param>
        /// <param name="idnderm">Id e ndermarrjes</param>
        /// <returns> Kthen nje coolection me objekte clsKPF qe plotesojne kushtet</returns>
        /// </summary>
        public bool ekzistonKPF(String kodikpf, int idnderm, int grupi)
        {//kontrollon nese ekziston kpf me kete kod
            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@KODIKPF", kodikpf, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            dbManager.AddParameters(2, "@GRUPI", grupi, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KPF_ekzistonKPF");
            if (ds.Tables[0].Rows.Count == 1)
                return true;
            else if (ds.Tables[0].Rows.Count == 0)
                return false;
            else return true;
        }
        public bool eshtePrind(String kodikpf, int idnderm, int grupi, int niveli)
        {//kontrollon nese ekziston kpf me kete kod
            dbManager.Open();
            dbManager.CreateParameters(4);
            dbManager.AddParameters(0, "@KODIKPF", kodikpf, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            dbManager.AddParameters(2, "@grupikpf", grupi, ParameterDirection.Input);
            dbManager.AddParameters(3, "@NIVELIKPF", niveli, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KPF_eshtePrind");
            if (ds.Tables[0].Rows.Count > 0)
                return true;
            else if (ds.Tables[0].Rows.Count == 0)
                return false;
            else return true;
        }
        //nuk perdoret me sepse eshte hequr atributi fitim humbje
        ///// <summary>
        ///// Ekzekuton prc_T_KPF_fitimhumje per te kontrolluar kolonen kolonen Fitim/Humbje (nese shte true apo jo) te nje objekti clsKPF duke filtruar sipas gripitKPF dhe id-se se ndermarrjes.
        ///// <param name="grupi">Grupi KPF</param>
        ///// <param name="idnderm">Id e ndermarrjes</param>
        ///// <returns> Kthen true nese ekziston nje objekt clsKPF qe ploteson kushtin</returns>
        ///////// </summary>
        //public bool fitimhumbje(int grupi, int idnderm)
        //{
        //    IDBManager dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        //    dbManager.ConnectionString = dbManager.GetConnectionString();
        //    try
        //    {
        //        dbManager.Open();
        //        dbManager.CreateParameters(2);
        //        dbManager.AddParameters(0, "@GRUPIKPF", grupi , ParameterDirection.Input);
        //        dbManager.AddParameters(1, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
        //        DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KPF_fitimhumje");
        //        if (ds.Tables[0].Rows.Count == 1)
        //            return true;
        //        else if (ds.Tables[0].Rows.Count == 0)
        //            return false;
        //        else return true;
        //    }
        //    catch (Exception)
        //    {
        //        return true;
        //    }
        //    finally
        //    {
        //        dbManager.Dispose();
        //    }
        //}

        /// <summary>
        /// Ekzekuton prc_T_KPF_merrGjitheKPFteSipasGrupitPozitive per te marre nje datatable duke filtruar sipas grupit te KPF dhe ID-se lidhese te ndermarrjes me vitin.
        /// <param name="grupiKpf">Grupi KPF</param>
        /// <param name="idNdermVit">ID lidhese ndermarrje - vit</param>
        /// <returns> Kthen nje datatable qe plotesojne kushtet</returns>
        /// </summary>
        internal DataTable ktheGjitheKPFteSipasGrupitPozitive(int grupiKpf, int idNdermarje)
        {//metoda per te marre te gjithe  kpf sipas grupit

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@GRUPIKPF", grupiKpf, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERmarje", idNdermarje, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KPF_merrGjitheKPFteSipasGrupitPozitive");
            return ds.Tables[0];
        }
        //[Obsolete("Perdor: DataTable ktheGjitheKPFteSipasGrupitPozitive(int grupiKpf, int idNdermVit)", true)]
        //public colKPFte merrGjitheKPFteSipasGrupitPozitive(int grupiKpf, int idNdermVit)
        //{//metoda per te marre te gjithe  kpf sipas grupit
        //    IDBManager dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        //    dbManager.ConnectionString = dbManager.GetConnectionString();
        //    try
        //    {
        //        dbManager.Open();
        //        dbManager.CreateParameters(2);
        //        dbManager.AddParameters(0, "@GRUPIKPF", grupiKpf, ParameterDirection.Input);
        //        dbManager.AddParameters(1, "@IDNDERVITI", idNdermVit, ParameterDirection.Input);
        //        DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KPF_merrGjitheKPFteSipasGrupitPozitive");
        //        colKPFte KPFte = new colKPFte();
        //        return KPFte.mbushArrayListKPFte(ds);
        //    }
        //    catch (Exception)
        //    {
        //        return new colKPFte();
        //    }
        //    finally
        //    {
        //        dbManager.Dispose();
        //    }
        //}

        /// <summary>
        /// Ekzekuton prc_T_KPF_merrGjitheKPFteSipasGrupitPozitiveAndAutorizime per te marre nje datatable duke filtruar sipas grupit te KPF, ID-se lidhese te ndermarrjes me vitin dhe ID-se se perdoruesit.
        /// <param name="grupiKpf">Grupi KPF</param>
        /// <param name="idNdermVit">ID lidhese ndermarrje - vit</param>
        /// <param name="idperdoruesi">Id e perdoruesit</param>
        /// <returns> Kthen nje datatable qe plotesojne kushtet</returns>
        /// </summary>
        internal DataTable ktheGjitheKPFteSipasGrupitPozitiveAndAutorizime(int grupiKpf, int idNdermarje, int idperdoruesi)
        {//metoda per te marre te gjithe  kpf sipas grupit
            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@GRUPIKPF", grupiKpf, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERmarje", idNdermarje, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDPERDORUES", idperdoruesi, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KPF_merrGjitheKPFteSipasGrupitPozitiveAndAutorizime");
            return ds.Tables[0];
        }
        /// <summary>
        /// Ekzekuton prc_T_KPF_merrGjitheKPFteSipasGrupitPozitiveAndAutorizime per te marre nje datatable duke filtruar sipas grupit te KPF, ID-se lidhese te ndermarrjes me vitin dhe ID-se se perdoruesit.
        /// <param name="grupiKpf">Grupi KPF</param>
        /// <param name="idNdermVit">ID lidhese ndermarrje - vit</param>
        /// <param name="idperdoruesi">Id e perdoruesit</param>
        /// <returns> Kthen nje datatable qe plotesojne kushtet</returns>
        /// </summary>
        internal DataTable ktheGjitheKPFteSipasGrupitPozitiveAndAutorizimeNiveli1Dhe2(int grupiKpf, int idNdermarje, int idperdoruesi)
        {//metoda per te marre te gjithe  kpf sipas grupit

            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@GRUPIKPF", grupiKpf, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERmarje", idNdermarje, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDPERDORUES", idperdoruesi, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KPF_merrGjitheKPFteSipasGrupitPozitiveAndAutorizimeNiveli1Dhe2");
            return ds.Tables[0];
        }
       
        /// <summary>
        /// Ekzekuton prc_T_KPF_merrGjitheKPFteSipasGrupitPozitiveAndAutorizimeAktiv per te marre nje datatable duke filtruar sipas grupit te KPF, ID-se lidhese te ndermarrjes me vitin dhe ID-se se perdoruesit aktive
        /// <param name="grupiKpf">Grupi KPF</param>
        /// <param name="idNdermVit">ID lidhese ndermarrje - vit</param>
        /// <param name="idperdoruesi">Id e perdoruesit</param>
        /// <returns> Kthen nje datatable qe plotesojne kushtet</returns>
        /// </summary>
        internal DataTable ktheGjitheKPFteSipasGrupitPozitiveAndAutorizimeAktive(int grupiKpf, int idNdermarje, int idperdoruesi)
        {//metoda per te marre te gjithe  kpf sipas grupit

            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@GRUPIKPF", grupiKpf, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERmarje", idNdermarje, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDPERDORUES", idperdoruesi, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KPF_merrGjitheKPFteSipasGrupitPozitiveAndAutorizimeAktiv");
            return ds.Tables[0];
        }
        /// <summary>
        /// Ekzekuton prc_T_KPF_merrGjitheKPFteSipasGrupitPozitiveAndAutorizimeAktiv per te marre nje datatable duke filtruar sipas grupit te KPF, ID-se lidhese te ndermarrjes me vitin dhe ID-se se perdoruesit aktive
        /// <param name="grupiKpf">Grupi KPF</param>
        /// <param name="idNdermVit">ID lidhese ndermarrje - vit</param>
        /// <param name="idperdoruesi">Id e perdoruesit</param>
        /// <returns> Kthen nje datatable qe plotesojne kushtet</returns>
        /// </summary>
        internal DataTable ktheGjitheKPFteSipasGrupitAndAutorizimeLike(int grupiKpf, int idNdermarje, int idperdoruesi, string kodi)
        {//metoda per te marre te gjithe  kpf sipas grupit
            dbManager.Open();
            dbManager.CreateParameters(4);
            dbManager.AddParameters(0, "@GRUPIKPF", grupiKpf, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERmarje", idNdermarje, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDPERDORUES", idperdoruesi, ParameterDirection.Input);
            dbManager.AddParameters(3, "@KODIKPF", kodi + "%", ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KPF_merrGjitheKPFteSipasGrupitAndAutorizimeLike");
            return ds.Tables[0];
        }

        /// <summary>
        /// Ekzekuton prc_T_KPF_merrGjitheKPFteSipasGrupitPozitiveAndAutorizimeAktivBij per te marre nje datatable duke filtruar sipas grupit te KPF, ID-se lidhese te ndermarrjes me vitin dhe ID-se se perdoruesit aktive
        /// <param name="grupiKpf">Grupi KPF</param>
        /// <param name="idNdermVit">ID lidhese ndermarrje - vit</param>
        /// <param name="idperdoruesi">Id e perdoruesit</param>
        /// <returns> Kthen nje datatable qe plotesojne kushtet</returns>
        /// </summary>
        internal DataTable ktheGjitheKPFteSipasGrupitPozitiveAndAutorizimeAktiveBij(int grupiKpf, int idNdermarje, int idperdoruesi)
        {//metoda per te marre te gjithe  kpf sipas grupit
            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@GRUPIKPF", grupiKpf, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERmarje", idNdermarje, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDPERDORUES", idperdoruesi, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KPF_merrGjitheKPFteSipasGrupitPozitiveAndAutorizimeAktivBij");
            return ds.Tables[0];
        }
        //[Obsolete("Perdor: DataTable ktheGjitheKPFteSipasGrupitPozitiveAndAutorizimeAktive(int grupiKpf, int idNdermVit, int idperdoruesi)", true)]
        //public colKPFte merrGjitheKPFteSipasGrupitPozitiveAndAutorizimeAktive(int grupiKpf, int idNdermVit, int idperdoruesi)
        //{//metoda per te marre te gjithe  kpf sipas grupit
        //    IDBManager dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        //    dbManager.ConnectionString = dbManager.GetConnectionString();
        //    try
        //    {
        //        dbManager.Open();
        //        dbManager.CreateParameters(3);
        //        dbManager.AddParameters(0, "@GRUPIKPF", grupiKpf, ParameterDirection.Input);
        //        dbManager.AddParameters(1, "@IDNDERVITI", idNdermVit, ParameterDirection.Input);
        //        dbManager.AddParameters(2, "@IDPERDORUES", idperdoruesi, ParameterDirection.Input);
        //        DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KPF_merrGjitheKPFteSipasGrupitPozitiveAndAutorizimeAktiv");
        //        colKPFte KPFte = new colKPFte();
        //        return KPFte.mbushArrayListKPFte(ds);
        //    }
        //    catch (Exception)
        //    {
        //        return new colKPFte();
        //    }
        //    finally
        //    {
        //        dbManager.Dispose();
        //    }
        //}

        /// <summary>
        /// Ekzekuton prc_T_KPF_merrGjitheKPFteSipasGrupitPozitiveAndAutorizimeLike per te marre nje datarow duke filtruar sipas grupit te KPF, ID-se lidhese te ndermarrjes me vitin dhe ID-se se perdoruesit aktive
        /// <param name="grupiKpf">Grupi KPF</param>
        /// <param name="idNdermVit">ID lidhese ndermarrje - vit</param>
        /// <param name="idperdoruesi">Id e perdoruesit</param>
        /// <returns> Kthen nje datarow qe plotesojne kushtet</returns>
        /// </summary>
        internal DataRow ktheGjitheKPFteSipasGrupitPozitiveAndAutorizimeLike(int grupiKpf, int idNdermarje, int idperdoruesi, string kodi)
        {//metoda per te marre te gjithe  kpf sipas grupit

            dbManager.Open();
            dbManager.CreateParameters(4);
            dbManager.AddParameters(0, "@GRUPIKPF", grupiKpf, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERmarje", idNdermarje, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDPERDORUES", idperdoruesi, ParameterDirection.Input);
            dbManager.AddParameters(3, "@KODIKPF", kodi + "%", ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KPF_merrGjitheKPFteSipasGrupitPozitiveAndAutorizimeLike");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0)
                return null;
            return ds.Tables[0].Rows[0];
        }
        //[Obsolete("Perdor: DataRow ktheGjitheKPFteSipasGrupitPozitiveAndAutorizimeLike(int grupiKpf, int idNdermVit, int idperdoruesi, string kodi)", true)]
        //public colKPFte merrGjitheKPFteSipasGrupitPozitiveAndAutorizimeLike(int grupiKpf, int idNdermVit, int idperdoruesi, string kodi)
        //{//metoda per te marre te gjithe  kpf sipas grupit
        //    IDBManager dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        //    dbManager.ConnectionString = dbManager.GetConnectionString();
        //    try
        //    {
        //        dbManager.Open();
        //        dbManager.CreateParameters(4);
        //        dbManager.AddParameters(0, "@GRUPIKPF", grupiKpf, ParameterDirection.Input);
        //        dbManager.AddParameters(1, "@IDNDERVITI", idNdermVit, ParameterDirection.Input);
        //        dbManager.AddParameters(2, "@IDPERDORUES", idperdoruesi, ParameterDirection.Input);
        //        dbManager.AddParameters(3, "@KODIKPF", kodi + "%", ParameterDirection.Input);
        //        DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KPF_merrGjitheKPFteSipasGrupitPozitiveAndAutorizimeLike");
        //        colKPFte KPFte = new colKPFte();
        //        return KPFte.mbushArrayListKPFte(ds);
        //    }
        //    catch (Exception)
        //    {
        //        return new colKPFte();
        //    }
        //    finally
        //    {
        //        dbManager.Dispose();
        //    }
        //}

        internal DataRow ktheKPFNdermarrjesAndAutorizimeDR(int idnderm, int idperdorues, int idkpf)
        {
            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUES", idperdorues, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDKPF", idkpf, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KPF_merrKPFSipasNdermarjesAndAutorizimDR");

            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];
        }
        internal DataTable ktheKPFNdermarrjesAndAutorizimeDTGrupit(int idnderm, int idperdorues, int idgrupi)
        {
            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUES", idperdorues, ParameterDirection.Input);
            dbManager.AddParameters(2, "@GRUPIKPF", idgrupi, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KPF_merrKPFSipasNdermarjesAndAutorizimDTGrupit");

            return ds.Tables[0];
        }
        internal DataTable ktheLLogariteNdermarrjesAndAutorizimeLikeNew(int idNdermarje, int idperdorues, string nrLlogarie)
        {
            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@IDNDERMARJE", idNdermarje, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUES", idperdorues, ParameterDirection.Input);
            dbManager.AddParameters(2, "@NRLLOGARI", nrLlogarie, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_LLOGARI_merrLlogariteNdermarrjesAndAutorizimeLike");
            return ds.Tables[0];
        }
        internal DataTable ktheKPFNdermarrjesAndAutorizimePerLupeKPF(int idnderm, int idperdorues, int idgrupi)
        {
            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUES", idperdorues, ParameterDirection.Input);
            dbManager.AddParameters(2, "@GRUPIKPF", idgrupi, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KPF_merrKPFSipasNdermarjesAndAutorizimDTGrupitLupeKPF");

            return ds.Tables[0];
        }
        #endregion

        ///// <summary>
        ///// Metodat e meposhtme i perkasin veprimeve qe kryhen nga objektet clsSkemaKontabelKoka, clsSkemaKontabelTrupi dhe colSkemaKontabelKoka
        ///// </summary>
        //#region SKEMAT KONTABEL

        ///// <summary>
        ///// Ekzekuton prc_T_KOKASKEMAKONTAB_merrGjitheSkematKontabel per te marre nje datatable ne DB duke filtruar sipas ID-se se ndermarrjes.
        ///// <param name="idNderm">Id e ndermarrjes</param>
        ///// <returns> Kthen nje datatable qe plotesojne kushtin</returns>
        ///// </summary>
        //internal DataTable ktheGjitheSkematKontabel(int idNderm)
        //{
        //    if (dbManager == null)
        //    {
        //        dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        //        dbManager.ConnectionString = dbManager.GetConnectionString();
        //    }

        //    try
        //    {
        //        dbManager.Open();
        //        dbManager.CreateParameters(1);
        //        dbManager.AddParameters(0, "@IDNDERMARJE", idNderm, ParameterDirection.Input);
        //        DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KOKASKEMAKONTAB_merrGjitheSkematKontabel");
        //        return ds.Tables[0];
        //    }
        //    catch (Exception)
        //    {
        //        return null;
        //    }
        //}
        ////[Obsolete("Perdor: DataTable ktheGjitheSkematKontabel(int idNderm)", true)]
        ////public colSkemaKontabelKoka merrGjitheSkematKontabel(int idNderm)
        ////{
        ////    dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        ////    dbManager.ConnectionString = dbManager.GetConnectionString();

        ////    try
        ////    { 
        ////        dbManager.Open();
        ////        dbManager.CreateParameters(1);
        ////        dbManager.AddParameters(0, "@IDNDERMARJE", idNderm, ParameterDirection.Input);
        ////        DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KOKASKEMAKONTAB_merrGjitheSkematKontabel");
        ////        colSkemaKontabelKoka skemat = new colSkemaKontabelKoka();
        ////        return skemat.mbushArrayListSkemaKontKokat(ds);
        ////    }
        ////    catch (Exception)
        ////    {
        ////        return new colSkemaKontabelKoka();
        ////    }
        ////    finally
        ////    {
        ////        dbManager.Dispose();
        ////    }
        ////}

        ///// <summary>
        ///// Ekzekuton prc_T_KOKASKEMAKONTAB_merrGjitheSkematKontabelAndAutorizime per te marre nje datatable ne DB duke filtruar sipas ID-se se ndermarrjes dhe ID-se se perdoruesit.
        ///// <param name="idNderm">Id e ndermarrjes</param>
        ///// <param name="perdorues">Id e perdoruesit</param>
        ///// <returns> Kthen nje datatable qe plotesojne kushtin</returns>
        ///// </summary>
        //internal DataTable ktheGjitheSkematKontabelAndAutorizim(int idNderm, int perdorues)
        //{
        //    if (dbManager == null)
        //    {
        //        dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        //        dbManager.ConnectionString = dbManager.GetConnectionString();
        //    }

        //    try
        //    {
        //        dbManager.Open();
        //        dbManager.CreateParameters(2);
        //        dbManager.AddParameters(0, "@IDNDERMARJE", idNderm, ParameterDirection.Input);
        //        dbManager.AddParameters(1, "@IDPERDORUES", perdorues, ParameterDirection.Input);
        //        DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KOKASKEMAKONTAB_merrGjitheSkematKontabelAndAutorizime");
        //        return ds.Tables[0];
        //    }
        //    catch (Exception)
        //    {
        //        return null;
        //    }
        //}
        ////[Obsolete("Perdor: DataTable ktheGjitheSkematKontabelAndAutorizim(int idNderm, int perdorues)", true)]
        ////public colSkemaKontabelKoka merrGjitheSkematKontabelAndAutorizim(int idNderm, int perdorues)
        ////{
        ////    dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        ////    dbManager.ConnectionString = dbManager.GetConnectionString();

        ////    try
        ////    {
        ////        dbManager.Open();
        ////        dbManager.CreateParameters(2);
        ////        dbManager.AddParameters(0, "@IDNDERMARJE", idNderm, ParameterDirection.Input);
        ////        dbManager.AddParameters(1, "@IDPERDORUES", perdorues , ParameterDirection.Input);
        ////        DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KOKASKEMAKONTAB_merrGjitheSkematKontabelAndAutorizime");
        ////        colSkemaKontabelKoka skemat = new colSkemaKontabelKoka();
        ////        return skemat.mbushArrayListSkemaKontKokat(ds);
        ////    }
        ////    catch (Exception)
        ////    {
        ////        return new colSkemaKontabelKoka();
        ////    }
        ////    finally
        ////    {
        ////        dbManager.Dispose();
        ////    }
        ////}

        ///// <summary>
        ///// Ekzekuton prc_T_KOKASKEMAKONTAB_merrGjitheSkematKontabelDefault per te marre nje datatable te llojit "Default" duke filtruar sipas ID-se se ndermarrjes.
        ///// <param name="idNderm">Id e ndermarrjes</param>
        ///// <returns> Kthen nje datatable qe plotesojne kushtin</returns>
        ///// </summary>
        //internal DataTable ktheGjitheSkematKontabelDefault(int idNderm)
        //{
        //    if (dbManager == null)
        //    {
        //        dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        //        dbManager.ConnectionString = dbManager.GetConnectionString();
        //    }

        //    try
        //    {
        //        dbManager.Open();
        //        dbManager.CreateParameters(1);
        //        dbManager.AddParameters(0, "@IDNDERMARJE", idNderm, ParameterDirection.Input);
        //        DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KOKASKEMAKONTAB_merrGjitheSkematKontabelDefault");
        //        return ds.Tables[0];
        //    }
        //    catch (Exception)
        //    {
        //        return null;
        //    }
        //}
        ////[Obsolete("Perdor: DataTable ktheGjitheSkematKontabelDefault(int idNderm)", true)]
        ////public colSkemaKontabelKoka merrGjitheSkematKontabelDefault(int idNderm)
        ////{
        ////    dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        ////    dbManager.ConnectionString = dbManager.GetConnectionString();

        ////    try
        ////    {
        ////        dbManager.Open();
        ////        dbManager.CreateParameters(1);
        ////        dbManager.AddParameters(0, "@IDNDERMARJE", idNderm, ParameterDirection.Input);
        ////        DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KOKASKEMAKONTAB_merrGjitheSkematKontabelDefault");
        ////        colSkemaKontabelKoka skemat = new colSkemaKontabelKoka();
        ////        return skemat.mbushArrayListSkemaKontKokat(ds);
        ////    }
        ////    catch (Exception)
        ////    {
        ////        return new colSkemaKontabelKoka();
        ////    }
        ////    finally
        ////    {
        ////        dbManager.Dispose();
        ////    }
        ////}


        ////[Obsolete("Perdor: DataTable ktheGjitheSkemaKontabelRegjistrim(int idNdermVit)", true)]
        ////public colSkemaKontabelNew merrGjitheSkemaKontabelRegjistrim(int idNdermVit)
        ////{
        ////    IDBManager dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        ////    dbManager.ConnectionString = dbManager.GetConnectionString();
        ////    try
        ////    {
        ////        dbManager.Open();
        ////        //  dbManager.CreateParameters(1);
        ////        // dbManager.AddParameters(0, "@IDNDERVITI", idNdermVit, ParameterDirection.Input);
        ////        DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_SKEMEKONTABILITETI_merrAll");
        ////        colSkemaKontabelNew skemat = new colSkemaKontabelNew();
        ////        return skemat.mbushArrayListSkemeKontabelNew(ds);
        ////    }
        ////    catch (Exception)
        ////    {
        ////        return new colSkemaKontabelNew();
        ////    }
        ////    finally
        ////    {
        ////        dbManager.Dispose();
        ////    }
        ////}

        ///// <summary>
        ///// Ekzekuton prc_T_KOKASKEMAKONTAB_merrSkemeKontabelSipasKodit per te marre nje datarow duke filtruar sipas kodit te skemes.
        /////<param name="kodiSkemaKontabelKoka">kodi i kokes se skemes kontabel</param>
        ///// <returns> Kthen nje datarow qe ploteson kushtin</returns>
        ///// </summary>
        //internal DataRow ktheSkemeKontabelSipasKodit(String kodiSkemaKontabelKoka)
        //{//merrSkemeKontabelSipasKodit
        //    if (dbManager == null)
        //    {
        //        dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        //        dbManager.ConnectionString = dbManager.GetConnectionString();
        //    }
        //    try
        //    {
        //        dbManager.Open();
        //        dbManager.CreateParameters(1);
        //        dbManager.AddParameters(0, "@KOKASKEMAKONTABKODI", kodiSkemaKontabelKoka, ParameterDirection.Input);
        //        DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KOKASKEMAKONTAB_merrSkemeKontabelSipasKodit");
        //        if (ds == null)
        //            return null;
        //        if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
        //            return null;
        //        return ds.Tables[0].Rows[0];
        //    }
        //    catch (Exception)
        //    {
        //        return null;
        //    }
        //}
        ////[Obsolete("Perdor: DataRow ktheSkemeKontabelSipasKodit(String kodiSkemaKontabelKoka)", true)]
        ////public clsSkemaKontabelKoka merrSkemeKontabelSipasKodit(clsSkemaKontabelKoka o)
        ////{//merrSkemeKontabelSipasKodit
        ////    dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        ////    dbManager.ConnectionString = dbManager.GetConnectionString();
        ////    try
        ////    {
        ////        dbManager.Open();
        ////        dbManager.CreateParameters(1);
        ////        dbManager.AddParameters(0, "@KOKASKEMAKONTABKODI", o.KodiSkemaKontabelKoka, ParameterDirection.Input);
        ////        DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KOKASKEMAKONTAB_merrSkemeKontabelSipasKodit");
        ////        colSkemaKontabelKoka skemat = new colSkemaKontabelKoka();
        ////        skemat = skemat.mbushArrayListSkemaKontKokat(ds);
        ////        if (skemat.Count == 0)
        ////            return new clsSkemaKontabelKoka();
        ////        else
        ////            return skemat[0];
        ////    }
        ////    catch (Exception)
        ////    {
        ////        return new clsSkemaKontabelKoka();
        ////    }
        ////    finally
        ////    {
        ////        dbManager.Dispose();
        ////    }
        ////}

        ///// <summary>
        ///// Ekzekuton prc_T_KOKASKEMAKONTAB_merrSkemeKontabelSipasId per te marre nje datarow duke filtruar sipas ID-se se skemes.
        /////<param name="id">Id e kokes se skemes</param>
        ///// <returns> Kthen nje datarow qe ploteson kushtin</returns>
        ///// </summary>
        //internal DataRow ktheSkemeKontabelSipasId(int id)
        //{//merrSkemeKontabelSipasKodit
        //    if (dbManager == null)
        //    {
        //        dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        //        dbManager.ConnectionString = dbManager.GetConnectionString();
        //    }
        //    try
        //    {
        //        dbManager.Open();
        //        dbManager.CreateParameters(1);
        //        dbManager.AddParameters(0, "@KOKASKEMAKONTABID", id, ParameterDirection.Input);
        //        DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KOKASKEMAKONTAB_merrSkemeKontabelSipasId");
        //        if (ds == null)
        //            return null;
        //        if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
        //            return null;
        //        return ds.Tables[0].Rows[0];
        //    }
        //    catch (Exception)
        //    {
        //        return null;
        //    }
        //}
        ////[Obsolete("Perdor: DataRow ktheSkemeKontabelSipasId(int id)", true)]
        ////public clsSkemaKontabelKoka merrSkemeKontabelSipasId(int id)
        ////{//merrSkemeKontabelSipasKodit
        ////    dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        ////    dbManager.ConnectionString = dbManager.GetConnectionString();
        ////    try
        ////    {
        ////        dbManager.Open();
        ////        dbManager.CreateParameters(1);
        ////        dbManager.AddParameters(0, "@KOKASKEMAKONTABID", id, ParameterDirection.Input);
        ////        DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KOKASKEMAKONTAB_merrSkemeKontabelSipasId");
        ////        colSkemaKontabelKoka skemat = new colSkemaKontabelKoka();
        ////        skemat = skemat.mbushArrayListSkemaKontKokat(ds);
        ////        if (skemat.Count == 0)
        ////            return new clsSkemaKontabelKoka();
        ////        else
        ////            return skemat[0];
        ////    }
        ////    catch (Exception)
        ////    {
        ////        return new clsSkemaKontabelKoka();
        ////    }
        ////    finally
        ////    {
        ////        dbManager.Dispose();
        ////    }
        ////}

        ///// <summary>
        ///// Ekzekuton prc_T_TRUPISKEMAKONTAB_merrSkemeTrupiSipasIdKoka per te marre nje datatable duke filtruar sipas ID-se se skemes.
        /////<param name="id">Id e kokes se skemes</param>
        ///// <returns> Kthen nje datatable qe plotesojne kushtin</returns>
        ///// </summary>
        //internal DataTable ktheSkemeTrupiSipasIdKoka(string id)
        //{
        //    if (dbManager == null)
        //    {
        //        dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        //        dbManager.ConnectionString = dbManager.GetConnectionString();
        //    }

        //    try
        //    {
        //        dbManager.Open();
        //        dbManager.CreateParameters(1);
        //        dbManager.AddParameters(0, "@KOKASKEMAKONTABID", id, ParameterDirection.Input);
        //        DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_TRUPISKEMAKONTAB_merrSkemeTrupiSipasIdKoka");
        //        return ds.Tables[0];
        //    }
        //    catch (Exception)
        //    {
        //        return null;
        //    }
        //}
        ////[Obsolete("Perdor: DataTable ktheSkemeTrupiSipasIdKoka(string id)", true)]
        ////public colSkemaKontabelTrupi merrSkemeTrupiSipasIdKoka(string id)
        ////{
        ////    dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        ////    dbManager.ConnectionString = dbManager.GetConnectionString();

        ////    try
        ////    {
        ////        dbManager.Open();
        ////        dbManager.CreateParameters(1);
        ////        dbManager.AddParameters(0, "@KOKASKEMAKONTABID", id, ParameterDirection.Input);
        ////        DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_TRUPISKEMAKONTAB_merrSkemeTrupiSipasIdKoka");
        ////        colSkemaKontabelTrupi skematTrupi = new colSkemaKontabelTrupi();
        ////        skematTrupi = skematTrupi.mbushArrayListSkemaKontTrupi(ds);
        ////        if (skematTrupi.Count == 0)
        ////        {
        ////            return new colSkemaKontabelTrupi();
        ////        }
        ////        else
        ////        {
        ////            return skematTrupi;
        ////        }
        ////    }
        ////    catch (Exception)
        ////    {
        ////        return new colSkemaKontabelTrupi();
        ////    }
        ////    finally
        ////    {
        ////        dbManager.Dispose();
        ////    }
        ////}

        /////// <summary>
        /////// Ekzekuton nje transaksion per te ruajtur nje objekt clsSkemaKontabelKoka dhe trupin e tij.
        /////// <param name="koka">Objekt i tipit clsSkemaKontabelKoka qe do te ruhet</param>
        /////// Therret funksionin <see cref="DbCore.DbKontabiliteti.clsDatabaseKontabilitet.ruajKoken"/>
        /////// Therret funksionin <see cref="DbCore.DbKontabiliteti.clsDatabaseKontabilitet.ruajTrupin"/>
        /////// Therret funksionet <see cref="DbAdmin.clsDatabaseAdmin.ruajLidhjeAutorizim"/>
        /////// <returns> Kthen statusin e perfundimit te ekzekutimit te transaksionit (nje objekt clsMesazh qe tregon nese veprimi eshte kryer me sukses apo jo.<seealso cref="DbCore.clsMesazh"/>)</returns>
        /////// </summary>
        ////[Obsolete("Perdor nga klasa perkatese: ruajKokenTrupin(int idskemakontabelkoka, String kodiskemakontabelkoka, String pershkrimiskemakontabelkoka, bool aktivskemakontabelkoka, int idkursiskemakontabelkoka, int nrautoskemakontabelkoka, " +
        ////    "int idndermvit, int idperdoruesi, int idnderm)", true)]
        ////public clsMesazh ruajKokenTrupin(clsSkemaKontabelKoka koka)
        ////{
        ////    //colLlojeBuxhetesh colLloj = merrLlojBuxhetiSipasKodit("SkematKontabel");

        ////    bool statusVeprimi;
        ////    dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        ////    dbManager.ConnectionString = dbManager.GetConnectionString();
        ////    dbManager.Open();
        ////    dbManager.BeginTransaction();
        ////    clsMesazh mesazh = new clsMesazh();
        ////    DbCore.clsMesazh mesazhAdmin = new DbCore.clsMesazh(true);
        ////    try
        ////    {
        ////        int idS;
        ////        koka.IdSkemaKontabelKoka = ruajKoken(out idS, koka.KodiSkemaKontabelKoka, koka.PershkrimiSkemaKontabelKoka, koka.AktivSkemaKontabelKoka, koka.IdKursiSkemaKontabelKoka, 
        ////            koka.NrAutoSkemaKontabelKoka, koka.IdNderViti, koka.IdPerdoruesi, koka.IdNdermarje);
        ////        if (koka.IdSkemaKontabelKoka == 0)
        ////            statusVeprimi = false;
        ////        else statusVeprimi = true;

        ////        if (statusVeprimi)
        ////        {
        ////            koka.IdSkemaKontabelKoka = idS;
        ////            foreach (clsSkemaKontabelTrupi o in koka.OColTrupi)
        ////            {
        ////                if (statusVeprimi)
        ////                {
        ////                    o.IdKoka = koka.IdSkemaKontabelKoka;
        ////                    int idST;
        ////                    mesazh = ruajTrupin(out idST, o.DebiKrediSkemaKontabelTrupi, o.IdKoka, o.IdSkemaModel, o.IdLlogariSkemaKontabelTrupi);
        ////                }
        ////                else
        ////                {
        ////                    dbManager.Transaction.Rollback();
        ////                    return mesazh;
        ////                }
        ////            }
        ////            if (mesazh.Status)
        ////            {
        ////                if (koka.IdAutorizimSkemaKontabelKoka != "")
        ////                {
        ////                    DbAdmin.colLidhjetAutorizim colLidhjet = new DbAdmin.colLidhjetAutorizim();
        ////                    string[] pars1 = koka.IdAutorizimSkemaKontabelKoka.Split(',');
        ////                    for (int i = 0; i < pars1.Length; i++)
        ////                    {
        ////                        DbAdmin.clsLidhjeAutorizim lidhje = new DbAdmin.clsLidhjeAutorizim();
        ////                        lidhje.IdAutorizimeKoka = DbAdmin.clsAutorizimKoka.ktheIDAutorizim(pars1[i]);
        ////                        //lidhje.IdAutorizimeKoka = new DbAdmin.clsDatabaseAdmin().ktheAutorizim(pars1[i])[0].IdAutorizimKoka;
        ////                        colLidhjet.Add(lidhje);
        ////                    }
        ////                    foreach (DbAdmin.clsLidhjeAutorizim o in colLidhjet)
        ////                    {
        ////                        if (mesazhAdmin.Status)
        ////                        {
        ////                            o.IdLloji = DbCore.DbKontabiliteti.clsLlojBuxheti.mbushIDLlojBuxheti("SkematKontabel");
        ////                            o.IdLidhese = koka.IdSkemaKontabelKoka;
        ////                            mesazhAdmin = new DbAdmin.clsDatabaseAdmin().ruajLidhjeAutorizim(o.IdLidhjeAutorizim, o.IdLidhese, o.IdLloji, o.IdAutorizimeKoka);
        ////                        }
        ////                        else
        ////                        {
        ////                            dbManager.Transaction.Rollback();
        ////                            mesazh.Status = false;
        ////                            mesazh.PershkrimMesazhi = mesazhAdmin.PershkrimMesazhi;
        ////                            return mesazh;
        ////                        }
        ////                    }
        ////                }
        ////                if (mesazhAdmin.Status)
        ////                {
        ////                    dbManager.CommitTransaction();
        ////                    mesazh = new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
        ////                    return mesazh;
        ////                }
        ////                else
        ////                {
        ////                    dbManager.Transaction.Rollback();
        ////                    mesazh.Status = false;
        ////                    mesazh.PershkrimMesazhi = mesazhAdmin.PershkrimMesazhi;
        ////                    return mesazh;
        ////                }
        ////            }
        ////            else
        ////            {
        ////                dbManager.Transaction.Rollback();
        ////                return mesazh;
        ////            }
        ////        }
        ////        else
        ////        {
        ////            dbManager.Transaction.Rollback();
        ////            return mesazh;
        ////        }
        ////    }
        ////    catch (Exception)
        ////    {
        ////        dbManager.Transaction.Rollback();
        ////        return new clsMesazh(false, ce.Message);
        ////    }
        ////    finally
        ////    {
        ////        dbManager.Dispose();
        ////    }
        ////}

        ///// <summary>
        ///// Ekzekuton prc_T_KOKASKEMAKONTAB_ins per te ruajtur nje objekt clsSkemaKontabelKoka ne DB.
        ///// <param name="idskemakontabelkoka">id e kokes se skemes kontabel</param>
        ///// <param name="kodiskemakontabelkoka">kodi i kokes se skemes kotabel</param>
        ///// <param name="pershkrimiskemakontabelkoka">pershrkimi i kokes se skemes kontabel</param>
        ///// <param name="aktivskemakontabelkoka">nese koka e skemese kontabel eshte aktive ose jo</param>
        ///// <param name="idkursiskemakontabelkoka">id e kursit te skemes kontabel</param>
        ///// <param name="nrautoskemakontabelkoka">auto nr i skemes kontabel</param>
        ///// <param name="idndermvit">id qe lidh ndermarrjen me vitin</param>
        ///// <param name="idperdoruesi">id e perdoruesit</param>
        ///// <param name="idnderm">id e ndermarrjes</param>
        ///// <returns> Kthen statusin e perfundimit te ekzekutimit te SP-se perkatese (true nese veprimi eshte kryer me sukses)</returns>
        ///// </summary>
        //internal int ruajKoken(out int idskemakontabelkoka, String kodiskemakontabelkoka, String pershkrimiskemakontabelkoka, bool aktivskemakontabelkoka, int idkursiskemakontabelkoka, int nrautoskemakontabelkoka,
        //    int idndermvit, int idperdoruesi, int idnderm)
        //{
        //    idskemakontabelkoka = -1;
        //    if (dbManager == null)
        //    {
        //        dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        //        dbManager.ConnectionString = dbManager.GetConnectionString();
        //    }
        //    try
        //    {
        //        dbManager.Open();
        //        dbManager.CreateParameters(9);
        //        dbManager.AddParameters(0, "@KOKASKEMAKONTABID", idskemakontabelkoka, ParameterDirection.Output);
        //        dbManager.AddParameters(1, "@KOKASKEMAKONTABKODI", kodiskemakontabelkoka, ParameterDirection.Input);
        //        dbManager.AddParameters(2, "@KOKASKEMAKONTABPERSHK", pershkrimiskemakontabelkoka, ParameterDirection.Input);
        //        dbManager.AddParameters(3, "@KOKASKEMAKONTABAKTIV", aktivskemakontabelkoka, ParameterDirection.Input);
        //        dbManager.AddParameters(4, "@KOKASKEMAKONTABIDKURSI", idkursiskemakontabelkoka, ParameterDirection.Input);
        //        dbManager.AddParameters(5, "@KOKASKEMAKONTABNRAUTO", nrautoskemakontabelkoka, ParameterDirection.Input);
        //        dbManager.AddParameters(6, "@IDNDERVITI", idndermvit, ParameterDirection.Input);
        //        dbManager.AddParameters(7, "@IDPERDORUESI", idperdoruesi, ParameterDirection.Input);
        //        dbManager.AddParameters(8, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
        //        dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_KOKASKEMAKONTAB_ins");

        //        idskemakontabelkoka = int.Parse(dbManager.Parameters[0].Value.ToString());
        //        return idskemakontabelkoka;
        //    }
        //    catch (Exception)
        //    {

        //        return idskemakontabelkoka;
        //    }
        //}
        ////[Obsolete("Perdor: clsMesazh ruajKoken(int idskemakontabelkoka, String kodiskemakontabelkoka, String pershkrimiskemakontabelkoka, bool aktivskemakontabelkoka, int idkursiskemakontabelkoka, int nrautoskemakontabelkoka, " +
        ////    "int idndermvit, int idperdoruesi, int idnderm)", true)]
        ////public clsMesazh ruajKoken(clsSkemaKontabelKoka koka)
        ////{
        ////    try
        ////    {
        ////        dbManager.CreateParameters(9);
        ////        dbManager.AddParameters(0, "@KOKASKEMAKONTABID", koka.IdSkemaKontabelKoka, ParameterDirection.Output);
        ////        dbManager.AddParameters(1, "@KOKASKEMAKONTABKODI", koka.KodiSkemaKontabelKoka, ParameterDirection.Input);
        ////        dbManager.AddParameters(2, "@KOKASKEMAKONTABPERSHK", koka.PershkrimiSkemaKontabelKoka, ParameterDirection.Input);
        ////        dbManager.AddParameters(3, "@KOKASKEMAKONTABAKTIV", koka.AktivSkemaKontabelKoka, ParameterDirection.Input);
        ////        dbManager.AddParameters(4, "@KOKASKEMAKONTABIDKURSI", koka.IdKursiSkemaKontabelKoka, ParameterDirection.Input);
        ////        dbManager.AddParameters(5, "@KOKASKEMAKONTABNRAUTO", koka.NrAutoSkemaKontabelKoka, ParameterDirection.Input);
        ////        dbManager.AddParameters(6, "@IDNDERVITI", koka.IdNderViti, ParameterDirection.Input);
        ////        dbManager.AddParameters(7, "@IDPERDORUESI", koka.IdPerdoruesi , ParameterDirection.Input);
        ////        dbManager.AddParameters(8, "@IDNDERMARJE", koka.IdNdermarje, ParameterDirection.Input);
        ////        dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_KOKASKEMAKONTAB_ins");

        ////        koka.IdSkemaKontabelKoka = int.Parse(dbManager.Parameters[0].Value.ToString());
        ////        return new clsMesazh (true,MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
        ////    }
        ////    catch (Exception)
        ////    {

        ////        return new clsMesazh(false, ce.Message);
        ////    }
        ////}

        ///// <summary>
        ///// Ekzekuton prc_T_TRUPISKEMAKONTAB_ins per te ruajtur nje objekt clsSkemaKontabelKoka ne DB.
        ///// <param name="idskemakontabeltrupi">id e trupit te skemes kontabel</param>
        ///// <param name="debikrediskemakontabeltrupi">trupi eshte debi apo kredi</param>
        ///// <param name="idkoka">id e kokes</param>
        ///// <param name="idskemamodel">id e skemes se modelit</param>
        ///// <param name="idllogariskemakontabeltrupi">id e llogarise se trupit te skemes kontabel</param>
        ///// <returns> Kthen statusin e perfundimit te ekzekutimit te SP-se perkatese (true nese veprimi eshte kryer me sukses)</returns>
        ///// </summary>
        //internal clsMesazh ruajTrupin(out int idskemakontabeltrupi, String debikrediskemakontabeltrupi, int idkoka, int idskemamodel, int idllogariskemakontabeltrupi)
        //{
        //    idskemakontabeltrupi = -1;
        //    if (dbManager == null)
        //    {
        //        dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        //        dbManager.ConnectionString = dbManager.GetConnectionString();
        //    }
        //    try
        //    {
        //        dbManager.Open();
        //        dbManager.CreateParameters(5);
        //        dbManager.AddParameters(0, "@TRUPISKEMAKONTABID", idskemakontabeltrupi, ParameterDirection.Output);
        //        dbManager.AddParameters(1, "@KOKASKEMAKONTABID", idkoka, ParameterDirection.Input);
        //        dbManager.AddParameters(2, "@IDSKEMAMODEL", idskemamodel, ParameterDirection.Input);
        //        dbManager.AddParameters(3, "@TRUPISKEMAKONTABDEBIKREDI", debikrediskemakontabeltrupi, ParameterDirection.Input);
        //        dbManager.AddParameters(4, "@TRUPISKEMAKONTABLLOGARI", idllogariskemakontabeltrupi, ParameterDirection.Input);
        //        dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_TRUPISKEMAKONTAB_ins");
        //        return new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
        //    }
        //    catch (Exception ce)
        //    {

        //        return new clsMesazh(false, ce.Message);
        //    }
        //}
        ////[Obsolete("Perdor: ruajTrupin(int idskemakontabeltrupi, String debikrediskemakontabeltrupi, int idkoka, int idskemamodel, int idllogariskemakontabeltrupi)", true)]
        ////public clsMesazh ruajTrupin(clsSkemaKontabelTrupi trupi)
        ////{
        ////    try
        ////    {
        ////        dbManager.CreateParameters(5);
        ////        dbManager.AddParameters(0, "@TRUPISKEMAKONTABID", trupi.IdSkemaKontabelTrupi, ParameterDirection.Output);
        ////        dbManager.AddParameters(1, "@KOKASKEMAKONTABID", trupi.IdKoka, ParameterDirection.Input);
        ////        dbManager.AddParameters(2, "@IDSKEMAMODEL", trupi.IdSkemaModel, ParameterDirection.Input);
        ////        dbManager.AddParameters(3, "@TRUPISKEMAKONTABDEBIKREDI", trupi.DebiKrediSkemaKontabelTrupi, ParameterDirection.Input);
        ////        dbManager.AddParameters(4, "@TRUPISKEMAKONTABLLOGARI", trupi.IdLlogariSkemaKontabelTrupi, ParameterDirection.Input);
        ////        dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_TRUPISKEMAKONTAB_ins");
        ////        return new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
        ////    }
        ////    catch (Exception)
        ////    {

        ////        return new clsMesazh(false, ce.Message);
        ////    }
        ////}

        ///// <summary>
        ///// Ekzekuton prc_T_TRUPISKEMAKONTAB_ktheTrupinESkemes per te marre nje datatable duke filtruar sipas ID-se se kokes se skemes.
        /////<param name="id">Id e kokes se skemes</param>
        ///// <returns> Kthen nje datatable qe plotesojne kushtin</returns>
        ///// </summary>
        //internal DataTable merrTrupinESkemes(int id)
        //{
        //    if (dbManager == null)
        //    {
        //        dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        //        dbManager.ConnectionString = dbManager.GetConnectionString();
        //    }
        //    try
        //    {
        //        dbManager.Open();
        //        dbManager.CreateParameters(1);
        //        dbManager.AddParameters(0, "@KOKASKEMAKONTABID", id, ParameterDirection.Input);
        //        DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_TRUPISKEMAKONTAB_ktheTrupinESkemes");
        //        return ds.Tables[0];
        //    }
        //    catch (Exception)
        //    {
        //        return null;
        //    }

        //}
        ////[Obsolete("Perdor: DataTable merrTrupinESkemes(int id)", true)]
        ////public colSkemaKontabelTrupi ktheTrupinESkemes(int id)
        ////{
        ////    dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        ////    dbManager.ConnectionString = dbManager.GetConnectionString();           
        ////    try
        ////    {
        ////        dbManager.Open();
        ////        dbManager.CreateParameters(1);
        ////        dbManager.AddParameters(0, "@KOKASKEMAKONTABID", id, ParameterDirection.Input);
        ////        DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_TRUPISKEMAKONTAB_ktheTrupinESkemes");
        ////        colSkemaKontabelTrupi trupi = new colSkemaKontabelTrupi();
        ////        return trupi.mbushArrayListSkemaKontTrupi(ds);
        ////    }
        ////    catch (Exception)
        ////    {
        ////        return new colSkemaKontabelTrupi();
        ////    }
        ////    finally
        ////    {
        ////        dbManager.Dispose();
        ////    }

        ////}

        /////// <summary>
        /////// Ekzekuton nje transaksion per te modifikuar nje objekt clsSkemaKontabelKoka dhe trupin e tij.
        /////// <param name="koka">Objekt i tipit clsSkemaKontabelKoka qe do te modifikohet</param>
        /////// Therret funksionin <see cref="DbCore.DbKontabiliteti.clsDatabaseKontabilitet.modifikoKoken"/>
        /////// Therret funksionin <see cref="DbCore.DbKontabiliteti.clsDatabaseKontabilitet.fshiTrupinEKokes"/>
        /////// Therret funksionet <see cref="DbAdmin.clsDatabaseAdmin.ruajLidhjeAutorizim"/>, <see cref="DbAdmin.clsDatabaseAdmin.modifikoLidhjeAutorizim"/>, 
        /////// <see cref="DbAdmin.clsDatabaseAdmin.fshiLidhjeAutorizim"/> sipas rasteve nese jane shtuar rreshta trupi apo jane fshire rreshta trupi  
        /////// <returns> Kthen statusin e perfundimit te ekzekutimit te transaksionit (nje objekt clsMesazh qe tregon nese veprimi eshte kryer me sukses apo jo.<seealso cref="DbCore.clsMesazh"/>)</returns>
        /////// </summary>
        ////[Obsolete("Perdor nga klasa perkatese: modifikoKokenTrupin(int idskemakontabelkoka, String kodiskemakontabelkoka, String pershkrimiskemakontabelkoka, bool aktivskemakontabelkoka, int idkursiskemakontabelkoka, int nrautoskemakontabelkoka, " +
        ////    "int idperdoruesi, int idnderm)", true)]
        ////public clsMesazh modifikoKokenTrupin(clsSkemaKontabelKoka koka)
        ////{
        ////    //colLlojeBuxhetesh colLloj = merrLlojBuxhetiSipasKodit("SkematKontabel");
        ////    //DbAdmin.colLidhjetAutorizim colLidhjetAutorizim = new DbAdmin.clsDatabaseAdmin().merrLidhjeAutorizimSipasIdLidheseIdLloji(koka.IdSkemaKontabelKoka, DbCore.DbKontabiliteti.clsLlojBuxheti.mbushIDLlojBuxheti("SkematKontabel"));
        ////    DbAdmin.colLidhjetAutorizim colLidhjetAutorizim = new DbAdmin.colLidhjetAutorizim(koka.IdSkemaKontabelKoka, DbCore.DbKontabiliteti.clsLlojBuxheti.mbushIDLlojBuxheti("SkematKontabel"));
        ////    dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        ////    dbManager.ConnectionString = dbManager.GetConnectionString();
        ////    dbManager.Open();
        ////    dbManager.BeginTransaction();
        ////    clsMesazh mesazh = new clsMesazh();
        ////    DbCore.clsMesazh mesazhAdmin = new DbCore.clsMesazh(true);
        ////    try
        ////    {
        ////        mesazh = modifikoKoken(koka.IdSkemaKontabelKoka, koka.KodiSkemaKontabelKoka, koka.PershkrimiSkemaKontabelKoka, koka.AktivSkemaKontabelKoka, koka.IdKursiSkemaKontabelKoka,
        ////            koka.NrAutoSkemaKontabelKoka, koka.IdPerdoruesi, koka.IdNdermarje);
        ////        if (mesazh.Status)
        ////        {
        ////            mesazh= fshiTrupinEKokes(koka.IdSkemaKontabelKoka);
        ////            if (mesazh.Status)
        ////            {
        ////                foreach (clsSkemaKontabelTrupi o in koka.OColTrupi)
        ////                {
        ////                    if (mesazh.Status)
        ////                    {
        ////                        o.IdKoka = koka.IdSkemaKontabelKoka;
        ////                        int idSK;
        ////                        mesazh = ruajTrupin(out idSK, o.DebiKrediSkemaKontabelTrupi, o.IdKoka, o.IdSkemaModel, o.IdLlogariSkemaKontabelTrupi);
        ////                    }
        ////                    else
        ////                    {
        ////                        dbManager.Transaction.Rollback();
        ////                        return mesazh;
        ////                    }
        ////                }
        ////                if (mesazh.Status)
        ////                {
        ////                    DbAdmin.colLidhjetAutorizim colLidhjet = new DbAdmin.colLidhjetAutorizim();
        ////                    if (koka.IdAutorizimSkemaKontabelKoka != "")
        ////                    {

        ////                        string[] pars1 = koka.IdAutorizimSkemaKontabelKoka.Split(',');
        ////                        for (int i = 0; i < pars1.Length; i++)
        ////                        {
        ////                            DbAdmin.clsLidhjeAutorizim lidhje = new DbAdmin.clsLidhjeAutorizim();
        ////                            lidhje.IdAutorizimeKoka = DbAdmin.clsAutorizimKoka.ktheIDAutorizim(pars1[i]);
        ////                            //lidhje.IdAutorizimeKoka = new DbAdmin.clsDatabaseAdmin().ktheAutorizim(pars1[i])[0].IdAutorizimKoka;
        ////                            colLidhjet.Add(lidhje);
        ////                        }

        ////                    }
        ////                    if (colLidhjetAutorizim.Count < colLidhjet.Count)//rasti kur jane shtuar rreshta trupi
        ////                    {
        ////                        for (int i = 0; i < colLidhjet.Count; i++)
        ////                        {
        ////                            if (mesazhAdmin.Status)
        ////                            {
        ////                                colLidhjet[i].IdLloji = DbCore.DbKontabiliteti.clsLlojBuxheti.mbushIDLlojBuxheti("SkematKontabel");
        ////                                colLidhjet[i].IdLidhese = koka.IdSkemaKontabelKoka;
        ////                                if (i < colLidhjetAutorizim.Count)
        ////                                {

        ////                                    colLidhjet[i].IdLidhjeAutorizim = colLidhjetAutorizim[i].IdLidhjeAutorizim;
        ////                                    mesazhAdmin = new DbAdmin.clsDatabaseAdmin().modifikoLidhjeAutorizim(colLidhjet[i].IdLidhjeAutorizim, colLidhjet[i].IdLidhese, colLidhjet[i].IdLloji, colLidhjet[i].IdAutorizimeKoka);
        ////                                }
        ////                                else
        ////                                    mesazhAdmin = new DbAdmin.clsDatabaseAdmin().ruajLidhjeAutorizim(colLidhjet[i].IdLidhjeAutorizim, colLidhjet[i].IdLidhese, colLidhjet[i].IdLloji, colLidhjet[i].IdAutorizimeKoka);
        ////                            }
        ////                            else
        ////                            {
        ////                                dbManager.Transaction.Rollback();
        ////                                mesazh.Status = false;
        ////                                mesazh.PershkrimMesazhi = mesazhAdmin.PershkrimMesazhi;
        ////                                return mesazh;
        ////                            }
        ////                        }
        ////                    }
        ////                    else//rasti kur jane fshire rreshta
        ////                    {
        ////                        int count = 0;
        ////                        for (int i = 0; i < colLidhjetAutorizim.Count; i++)
        ////                        {
        ////                            if (mesazhAdmin.Status)
        ////                            {
        ////                                if (count < colLidhjet.Count)
        ////                                {
        ////                                    colLidhjet[i].IdLloji = DbCore.DbKontabiliteti.clsLlojBuxheti.mbushIDLlojBuxheti("SkematKontabel");
        ////                                    colLidhjet[i].IdLidhese = koka.IdSkemaKontabelKoka;

        ////                                    colLidhjet[i].IdLidhjeAutorizim = colLidhjetAutorizim[i].IdLidhjeAutorizim;
        ////                                    mesazhAdmin = new DbAdmin.clsDatabaseAdmin().modifikoLidhjeAutorizim(colLidhjet[i].IdLidhjeAutorizim, colLidhjet[i].IdLidhese, colLidhjet[i].IdLloji, colLidhjet[i].IdAutorizimeKoka);
        ////                                }
        ////                                else
        ////                                {
        ////                                    mesazhAdmin = new DbAdmin.clsDatabaseAdmin().fshiLidhjeAutorizim(colLidhjetAutorizim[i].IdLidhjeAutorizim);
        ////                                }
        ////                                count++;
        ////                            }
        ////                            else
        ////                            {
        ////                                dbManager.Transaction.Rollback();
        ////                                mesazh.Status = false;
        ////                                mesazh.PershkrimMesazhi = mesazhAdmin.PershkrimMesazhi;
        ////                                return mesazh;
        ////                            }
        ////                        }
        ////                    }
        ////                    if (mesazhAdmin.Status)
        ////                    {
        ////                        dbManager.CommitTransaction();
        ////                        mesazh = new clsMesazh(true, MessagesResource.Messages["msgModifikimiMeSukses"]);
        ////                        return mesazh;
        ////                    }
        ////                    else
        ////                    {
        ////                        dbManager.Transaction.Rollback();
        ////                        mesazh.Status = false;
        ////                        mesazh.PershkrimMesazhi = mesazhAdmin.PershkrimMesazhi;
        ////                        return mesazh;
        ////                    }
        ////                }
        ////                else
        ////                {
        ////                    dbManager.Transaction.Rollback();
        ////                    return mesazh;
        ////                }
        ////            }
        ////            else
        ////            {
        ////                dbManager.Transaction.Rollback();
        ////                return mesazh;
        ////            }
        ////        }
        ////        else
        ////        {
        ////            dbManager.Transaction.Rollback();
        ////            return mesazh;
        ////        }
        ////    }
        ////    catch (Exception)
        ////    {
        ////        dbManager.Transaction.Rollback();
        ////        return new clsMesazh(false, ce.Message);
        ////    }
        ////    finally
        ////    {
        ////        dbManager.Dispose();
        ////    }
        ////}

        ///// <summary>
        ///// Ekzekuton prc_T_KOKASKEMAKONTAB_upd per te modifikuar nje objekt clsSkemaKontabelKoka ne DB.
        ///// <param name="idskemakontabelkoka">id e kokes se skemes kontabel</param>
        ///// <param name="kodiskemakontabelkoka">kodi i kokes se skemes kotabel</param>
        ///// <param name="pershkrimiskemakontabelkoka">pershrkimi i kokes se skemes kontabel</param>
        ///// <param name="aktivskemakontabelkoka">nese koka e skemese kontabel eshte aktive ose jo</param>
        ///// <param name="idkursiskemakontabelkoka">id e kursit te skemes kontabel</param>
        ///// <param name="nrautoskemakontabelkoka">auto nr i skemes kontabel</param>
        ///// <param name="idperdoruesi">id e perdoruesit</param>
        ///// <param name="idnderm">id e ndermarrjes</param>
        ///// <returns> Kthen statusin e perfundimit te ekzekutimit te SP-se perkatese (true nese veprimi eshte kryer me sukses)</returns>
        ///// </summary>
        //internal clsMesazh modifikoKoken(int idskemakontabelkoka, String kodiskemakontabelkoka, String pershkrimiskemakontabelkoka, bool aktivskemakontabelkoka, int idkursiskemakontabelkoka, int nrautoskemakontabelkoka,
        //    int idperdoruesi, int idnderm)
        //{
        //    if (dbManager == null)
        //    {
        //        dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        //        dbManager.ConnectionString = dbManager.GetConnectionString();
        //    }
        //    try
        //    {
        //        dbManager.Open();
        //        dbManager.CreateParameters(8);
        //        dbManager.AddParameters(0, "@KOKASKEMAKONTABID", idskemakontabelkoka, ParameterDirection.Input);
        //        dbManager.AddParameters(1, "@KOKASKEMAKONTABKODI", kodiskemakontabelkoka, ParameterDirection.Input);
        //        dbManager.AddParameters(2, "@KOKASKEMAKONTABPERSHK", pershkrimiskemakontabelkoka, ParameterDirection.Input);
        //        dbManager.AddParameters(3, "@KOKASKEMAKONTABAKTIV", aktivskemakontabelkoka, ParameterDirection.Input);
        //        dbManager.AddParameters(4, "@KOKASKEMAKONTABIDKURSI", idkursiskemakontabelkoka, ParameterDirection.Input);
        //        dbManager.AddParameters(5, "@KOKASKEMAKONTABNRAUTO", nrautoskemakontabelkoka, ParameterDirection.Input);
        //        dbManager.AddParameters(6, "@IDPERDORUESI", idperdoruesi, ParameterDirection.Input);
        //        dbManager.AddParameters(7, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
        //        dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_KOKASKEMAKONTAB_upd");
        //        idskemakontabelkoka = int.Parse(dbManager.Parameters[0].Value.ToString());
        //        clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
        //        return mesazh;
        //    }
        //    catch (Exception ce)
        //    {

        //        return new clsMesazh(false, ce.Message);
        //    }
        //}
        ////[Obsolete("Perdor: clsMesazh modifikoKoken(int idskemakontabelkoka, String kodiskemakontabelkoka, String pershkrimiskemakontabelkoka, bool aktivskemakontabelkoka, int idkursiskemakontabelkoka, int nrautoskemakontabelkoka, " +
        ////    "int idperdoruesi, int idnderm)", true)]
        ////public clsMesazh modifikoKoken(clsSkemaKontabelKoka koka)
        ////{
        ////    try
        ////    {
        ////        dbManager.CreateParameters(8);
        ////        dbManager.AddParameters(0, "@KOKASKEMAKONTABID", koka.IdSkemaKontabelKoka, ParameterDirection.Input);
        ////        dbManager.AddParameters(1, "@KOKASKEMAKONTABKODI", koka.KodiSkemaKontabelKoka, ParameterDirection.Input);
        ////        dbManager.AddParameters(2, "@KOKASKEMAKONTABPERSHK", koka.PershkrimiSkemaKontabelKoka, ParameterDirection.Input);
        ////        dbManager.AddParameters(3, "@KOKASKEMAKONTABAKTIV", koka.AktivSkemaKontabelKoka, ParameterDirection.Input);
        ////        dbManager.AddParameters(4, "@KOKASKEMAKONTABIDKURSI", koka.IdKursiSkemaKontabelKoka, ParameterDirection.Input);
        ////        dbManager.AddParameters(5, "@KOKASKEMAKONTABNRAUTO", koka.NrAutoSkemaKontabelKoka, ParameterDirection.Input);
        ////        dbManager.AddParameters(6, "@IDPERDORUESI", koka.IdPerdoruesi, ParameterDirection.Input);
        ////        dbManager.AddParameters(7, "@IDNDERMARJE", koka.IdNdermarje, ParameterDirection.Input);
        ////        dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_KOKASKEMAKONTAB_upd");
        ////        koka.IdSkemaKontabelKoka = int.Parse(dbManager.Parameters[0].Value.ToString());
        ////        clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
        ////        return mesazh;
        ////    }
        ////    catch (Exception)
        ////    {

        ////        return new clsMesazh(false, ce.Message);
        ////    }
        ////}

        ///// <summary>
        ///// Ekzekuton prc_T_TRUPISKEMAKONTAB_fshiTrupinEKokes per te fshire nje objekt clsSkemaKontabelTrupi ne DB duke filtruar sipas ID-se se kokes.
        ///// <param name="idskemakontabelkoka">id e kokes se skemes kontabel</param>
        ///// <returns> Kthen statusin e perfundimit te ekzekutimit te SP-se perkatese (true nese veprimi eshte kryer me sukses)</returns>
        ///// </summary>
        //internal clsMesazh fshiTrupinEKokes(int idskemakontabelkoka)
        //{
        //    if (dbManager == null)
        //    {
        //        dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        //        dbManager.ConnectionString = dbManager.GetConnectionString();
        //    }
        //    try
        //    {
        //        dbManager.Open();
        //        dbManager.CreateParameters(1);
        //        dbManager.AddParameters(0, "@KOKASKEMAKONTABID", idskemakontabelkoka, ParameterDirection.Input);
        //        DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_TRUPISKEMAKONTAB_fshiTrupinEKokes");
        //        clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
        //        return mesazh;
        //    }
        //    catch (Exception ce)
        //    {

        //        return new clsMesazh(false, ce.Message);
        //    }
        //}
        ////[Obsolete("Perdor: clsMesazh fshiTrupinEKokes(int idskemakontabelkoka)", true)]
        ////public clsMesazh fshiTrupinEKokes(clsSkemaKontabelKoka koka)
        ////{
        ////    try
        ////    {
        ////        dbManager.CreateParameters(1);
        ////        dbManager.AddParameters(0, "@KOKASKEMAKONTABID", koka.IdSkemaKontabelKoka, ParameterDirection.Input);
        ////        DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_TRUPISKEMAKONTAB_fshiTrupinEKokes");
        ////        clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
        ////        return mesazh;
        ////    }
        ////    catch (Exception)
        ////    {

        ////        return new clsMesazh(false, ce.Message);
        ////    }
        ////}

        ///// <summary>
        ///// Ekzekuton prc_T_KOKASKEMAKONTAB_del per te fshire nje objekt clsSkemaKontabelKoka ne DB duke filtruar sipas ID-se se kokes.
        ///// <param name="idskemakontabelkoka">id e kokes se skemes kontabel</param>
        ///// <returns> Kthen statusin e perfundimit te ekzekutimit te SP-se perkatese (true nese veprimi eshte kryer me sukses)</returns>
        ///// </summary>
        //internal clsMesazh fshiKoken(int idskemakontabelkoka)
        //{
        //    if (dbManager == null)
        //    {
        //        dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        //        dbManager.ConnectionString = dbManager.GetConnectionString();
        //    }
        //    try
        //    {
        //        dbManager.Open();
        //        dbManager.CreateParameters(1);
        //        dbManager.AddParameters(0, "@KOKASKEMAKONTABID", idskemakontabelkoka, ParameterDirection.Input);
        //        DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KOKASKEMAKONTAB_del");
        //        clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
        //        return mesazh;
        //    }
        //    catch (Exception ce)
        //    {

        //        return new clsMesazh(false, ce.Message);
        //    }
        //}
        ////[Obsolete("Perdor: clsMesazh fshiKoken(int idskemakontabelkoka)", true)]
        ////public clsMesazh fshiKoken(clsSkemaKontabelKoka koka)
        ////{
        ////    try
        ////    {
        ////        dbManager.CreateParameters(1);
        ////        dbManager.AddParameters(0, "@KOKASKEMAKONTABID", koka.IdSkemaKontabelKoka, ParameterDirection.Input);
        ////        DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KOKASKEMAKONTAB_del");
        ////        clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
        ////        return mesazh;
        ////    }
        ////    catch (Exception)
        ////    {

        ////        return new clsMesazh(false, ce.Message);
        ////    }
        ////}

        /////// <summary>
        /////// Ekzekuton nje transaksion per te fshire nje objekt clsSkemaKontabelKoka dhe trupin e tij.
        /////// <param name="koka">Objekt i tipit clsSkemaKontabelKoka qe do te fshihet</param>
        /////// Therret funksionin <see cref="DbCore.DbKontabiliteti.clsDatabaseKontabilitet.fshiTrupinEKokes"/>
        /////// /// Therret funksionin <see cref="DbCore.DbKontabiliteti.clsDatabaseKontabilitet.fshiKoken"/>
        /////// <returns> Kthen statusin e perfundimit te ekzekutimit te transaksionit (nje objekt clsMesazh qe tregon nese veprimi eshte kryer me sukses apo jo.<seealso cref="DbCore.clsMesazh"/>)</returns>
        /////// </summary>
        ////[Obsolete("Perdor nga klasa perkatese: fshiSkemeKontabel(int idskemakontabelkoka)", true)]
        ////public clsMesazh fshiSkemeKontabel(clsSkemaKontabelKoka koka)
        ////{
        ////    //colLlojeBuxhetesh colLloj = merrLlojBuxhetiSipasKodit("SkematKontabel");
        ////    DbAdmin.colLidhjetAutorizim autorizime = new DbAdmin.colLidhjetAutorizim(koka.IdSkemaKontabelKoka, DbCore.DbKontabiliteti.clsLlojBuxheti.mbushIDLlojBuxheti("SkematKontabel"));
        ////    //DbAdmin.colLidhjetAutorizim autorizime = new DbAdmin.clsDatabaseAdmin().merrLidhjeAutorizimSipasIdLidheseIdLloji(koka.IdSkemaKontabelKoka, DbCore.DbKontabiliteti.clsLlojBuxheti.mbushIDLlojBuxheti("SkematKontabel"));
        ////    dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        ////    dbManager.ConnectionString = dbManager.GetConnectionString();
        ////    dbManager.Open();
        ////    dbManager.BeginTransaction();
        ////    clsMesazh mesazh = new clsMesazh();
        ////    DbCore.clsMesazh mesazhAdmin = new DbCore.clsMesazh(true);
        ////    try
        ////    {

        ////        mesazh =  fshiTrupinEKokes(koka.IdSkemaKontabelKoka);
        ////        if (mesazh.Status)
        ////        {
        ////            foreach (DbAdmin.clsLidhjeAutorizim o in autorizime)
        ////            {
        ////                if (mesazhAdmin.Status)
        ////                {
        ////                    mesazhAdmin = o.fshi();
        ////                }

        ////                else
        ////                {
        ////                    dbManager.Transaction.Rollback();
        ////                    mesazh.Status = false;
        ////                    mesazh.PershkrimMesazhi = mesazhAdmin.PershkrimMesazhi;
        ////                    return mesazh;
        ////                }
        ////            }
        ////            if (mesazhAdmin.Status)
        ////            {
        ////                mesazh=fshiKoken(koka);
        ////                if (mesazh.Status)
        ////                {
        ////                    dbManager.CommitTransaction();
        ////                    mesazh = new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
        ////                    return mesazh;
        ////                }
        ////                else
        ////                {
        ////                    dbManager.Transaction.Rollback();
        ////                    return mesazh;
        ////                }
        ////            }
        ////            else
        ////            {
        ////                dbManager.Transaction.Rollback();
        ////                mesazh.Status = false;
        ////                mesazh.PershkrimMesazhi = mesazhAdmin.PershkrimMesazhi;
        ////                return mesazh;
        ////            }
        ////        }
        ////        else
        ////        {
        ////            dbManager.Transaction.Rollback();
        ////            return mesazh;
        ////        }
        ////    }
        ////    catch (Exception)
        ////    {
        ////        dbManager.Transaction.Rollback();
        ////        return new clsMesazh(false, ce.Message);
        ////    }
        ////    finally
        ////    {
        ////        dbManager.Dispose();
        ////    }
        ////}

        //#endregion

        /// <summary>
        /// Metodat e meposhtme i perkasin veprimeve qe kryhen nga objektet clsPasqyreFinanciare dhe colPasqyratFinaciare, si dhe clsBilanc, clsPASH dhe clsCashFlow qe trashegojne nga klasa clsPasqyraFinanciare
        /// </summary>
        #region  PASQYRAT FINANCIARE

        ///// <summary>
        ///// Ruan nje objekt bilanci sebashku me trupin , llogarite dhe buxhetet
        ///// Nje objekt bilanci ka nje koleksion me trupin, llogarite dhe buxhetet
        ///// ruajtja e nje bilanci imponon ruajtjen edhe te nje colection-i me trupin ,llogarite dhe buxhetet
        ///// Mqs cdo rresht i ri qe shtohet ne DB kerkon thirrjen e nje SP-je me parametra dhe bilanci se bashku me trupin, llogarite dhe buxhetet konsiderohet si nje regjistrim,
        ///// perdoret nje transaksion qe imponon regjistrimin e rregullt te nje bilanci sebashku me trupin, llogarite dhe buxhetet perkates
        ///// </summary>
        /////<param name="bilanc">bilanci qe do te ruhet</param>
        /////  <returns> kthen nje obj clsMesazh per te identifikuar statusin e ruajtjes se te dhenave ne DB</returns>
        //[Obsolete("Perdor nga klasa perkatese: clsMesazh ruajBilanc(int idpasqyres, String kodipasqyres, String emertimipasqyres, String tipipasqyres, String metoda, int idndermarja, int vit, int idndermvit, int idperdoruesi, colTrupPasqyreFinaciare ocoltrupibilancit)", true)]
        //public clsMesazh ruajBilanc(clsBilanc bilanc)
        //{
        //    dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        //    dbManager.ConnectionString = dbManager.GetConnectionString();
        //    dbManager.Open();
        //    dbManager.BeginTransaction();
        //    clsMesazh mesazh = new clsMesazh();
        //    try
        //    {
        //        int idP;
        //        mesazh = ruajPasqyraFinanciareKoka(out idP, bilanc.KodiPasqyresFin, bilanc.EmertimiPasqyresFin, bilanc.TipiPasqyresFin, bilanc.Metoda, bilanc.IdNdermarja, bilanc.Viti, bilanc.IdNdermVit, bilanc.IdPerdoruesi);
        //        if (mesazh.Status)
        //        {
        //            bilanc.IdPasqyresFin = idP;
        //            foreach (clsTrupPasqyreFinanciare trupi in bilanc.OColTrupiBilancit)
        //            {
        //                if (mesazh.Status)
        //                {
        //                    trupi.IdKoka = bilanc.IdPasqyresFin;
        //                    int idT;
        //                    mesazh = ruajTrupinPasqyresFinanciare(out idT, trupi.IdKoka, trupi.PershkrimiZerit, trupi.PrindiZerit, trupi.NiveliZerit, trupi.LlojiZerit, trupi.GjeneroTotal);
        //                    if (mesazh.Status)
        //                    {
        //                        foreach (clsLlogariaTrupiPasqyres llog in trupi.OColLlogarite)
        //                        {
        //                            if (mesazh.Status)
        //                            {
        //                                trupi.IdTrupi = idT;
        //                                llog.IdTrupi = trupi.IdTrupi;
        //                                mesazh = ruajLlogariTrupiPasqyres(llog.IdLlogariaTrupi, llog.IdTrupi, llog.IdLlogaria, llog.Emertimi, llog.Gjendja, llog.Shenja, llog.IdPerdoruesi, llog.Lloji);
        //                            }

        //                            else
        //                            {
        //                                dbManager.Transaction.Rollback();
        //                                return mesazh;
        //                            }
        //                        }

        //                        foreach (clsBuxheti buxheti in trupi.OColBuxhetet)
        //                        {
        //                            if (mesazh.Status)
        //                            {
        //                                buxheti.IdLidhese = trupi.IdTrupi;
        //                                int idB;
        //                                mesazh = ruajBuxhet(out idB, buxheti.IdLlojBuxheti, buxheti.IdLidhese, buxheti.Muaj, buxheti.Buxheti_1, buxheti.Buxheti_2);
        //                            }
        //                            else
        //                            {
        //                                dbManager.Transaction.Rollback();
        //                                    return mesazh;
        //                            }
        //                        }
        //                    }
        //                    else
        //                    {
        //                        dbManager.Transaction.Rollback();
        //                        return mesazh;
        //                    }
        //                }
        //                else
        //                {
        //                    dbManager.Transaction.Rollback();
        //                    return mesazh;
        //                }
        //            }
        //            if (mesazh.Status)
        //            {
        //                dbManager.CommitTransaction();
        //                mesazh = new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
        //                return mesazh;
        //            }
        //            else
        //            {
        //                dbManager.Transaction.Rollback();
        //                return mesazh;
        //            }
        //        }

        //        else
        //        {
        //            dbManager.Transaction.Rollback();
        //            return mesazh;
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        dbManager.Transaction.Rollback();
        //        return new clsMesazh(false, ce.Message);
        //    }
        //    finally
        //    {
        //        dbManager.Dispose();
        //    }
        //}

        /// <summary>
        /// ekzekuton prc_T_PASQYRAFINANCIAREKOKA_ins ne DB dhe kthen statusin e perfundimit te ekzekutimit te SP-se perkatese
        /// Shiko clsMesazh brenda ketij moduli per informacion te metejshem mbi vleren e kthimit te kesaj metode
        /// <param name="idpasqyres">Id e pasqyres financiare</param>
        /// <param name="kodipasqyres">Kodi i pasqyres financiare</param>
        /// <param name="emertimipasqyres">Emertimi i pasqyres financiare</param>
        /// <param name="tipipasqyres">Tipi i pasqyres financiare - Bilanc, PASH, Cash Flow</param>
        /// <param name="metoda">Metoda - Direkt, Indirekt</param>
        /// <param name="idndermarja">Id e ndermarrjes</param>
        /// <param name="vit">Viti</param>
        /// <param name="idndermvit">Id lidhese ndermarrje vit</param>
        /// <param name="idperdoruesi">Id e perdoruesit</param>
        /// <returns> nje objekt clsMesazh qe tregon nese ruajtja eshte kryer ne rregull apo jo.</returns>
        /// </summary>
        internal clsMesazh ruajPasqyraFinanciareKoka(out int idpasqyres, String kodipasqyres, String emertimipasqyres, String tipipasqyres, String metodapasq, int idndermarja, int vit, int idperdoruesi, int idstatusdok, bool model)
        {
            idpasqyres = -1;

            dbManager.Open();
            dbManager.CreateParameters(10);
            dbManager.AddParameters(0, "@IDPASQFINKOKA", idpasqyres, ParameterDirection.Output);
            dbManager.AddParameters(1, "@KODIPASQFINKOKA", kodipasqyres, ParameterDirection.Input);
            dbManager.AddParameters(2, "@EMERTIMIPASQFINKOKA", emertimipasqyres, ParameterDirection.Input);
            dbManager.AddParameters(3, "@TIPIPASQFINKOKA", tipipasqyres, ParameterDirection.Input);
            dbManager.AddParameters(4, "@METODAPASQFINKOKA", metodapasq, ParameterDirection.Input);
            dbManager.AddParameters(5, "@IDNDERMARJE", idndermarja, ParameterDirection.Input);
            dbManager.AddParameters(6, "@VITI", vit, ParameterDirection.Input);
            //dbManager.AddParameters(7, "@IDNDERVITI", idndermvit, ParameterDirection.Input);
            dbManager.AddParameters(7, "@IDPERDORUESI", idperdoruesi, ParameterDirection.Input);
            dbManager.AddParameters(8, "@IDSTATUSDOK", idstatusdok, ParameterDirection.Input);
            dbManager.AddParameters(9, "@MODEL", model, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_PASQYRAFINANCIAREKOKA_ins");

            idpasqyres = int.Parse(dbManager.Parameters[0].Value.ToString());
            return new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
        }
        //[Obsolete("Perdor: clsMesazh ruajPasqyraFinanciareKoka(int idpasqyres, String kodipasqyres, String emertimipasqyres, String tipipasqyres, String metodapasq, int idndermarja, int vit, int idndermvit, int idperdoruesi)", true)]
        //public clsMesazh ruajPasqyraFinanciareKoka(clsPasqyreFinanciare koka)
        //{
        //    try
        //    {
        //        dbManager.CreateParameters(9);
        //        dbManager.AddParameters(0, "@IDPASQFINKOKA", koka.IdPasqyresFin, ParameterDirection.Output);
        //        dbManager.AddParameters(1, "@KODIPASQFINKOKA", koka.KodiPasqyresFin, ParameterDirection.Input);
        //        dbManager.AddParameters(2, "@EMERTIMIPASQFINKOKA", koka.EmertimiPasqyresFin, ParameterDirection.Input);
        //        dbManager.AddParameters(3, "@TIPIPASQFINKOKA", koka.TipiPasqyresFin, ParameterDirection.Input);
        //        dbManager.AddParameters(4, "@METODAPASQFINKOKA", koka.Metoda, ParameterDirection.Input);
        //        dbManager.AddParameters(5, "@IDNDERMARJE", koka.IdNdermarja, ParameterDirection.Input);
        //        dbManager.AddParameters(6, "@VITI", koka.Viti, ParameterDirection.Input);
        //        dbManager.AddParameters(7, "@IDNDERVITI", koka.IdNdermVit, ParameterDirection.Input);
        //        dbManager.AddParameters(8, "@IDPERDORUESI", koka.IdPerdoruesi , ParameterDirection.Input);
        //        dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_PASQYRAFINANCIAREKOKA_ins");

        //        koka.IdPasqyresFin = int.Parse(dbManager.Parameters[0].Value.ToString());
        //        return new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
        //    }
        //    catch (Exception)
        //    {

        //        return new clsMesazh(false, ce.Message);
        //    }
        //}

        /// <summary>
        /// ekzekuton prc_T_PASQYRAFINANCIAREKOKA_upd ne DB dhe kthen statusin e perfundimit te ekzekutimit te SP-se perkatese
        /// Shiko clsMesazh brenda ketij moduli per informacion te metejshem mbi vleren e kthimit te kesaj metode
        /// <param name="idpasqyres">Id e pasqyres financiare</param>
        /// <param name="kodipasqyres">Kodi i pasqyres financiare</param>
        /// <param name="emertimipasqyres">Emertimi i pasqyres financiare</param>
        /// <param name="tipipasqyres">Tipi i pasqyres financiare - Bilanc, PASH, Cash Flow</param>
        /// <param name="metoda">Metoda - Direkt, Indirekt</param>
        /// <param name="idperdoruesi">Id e perdoruesit</param>
        /// <returns> nje objekt clsMesazh qe tregon nese modifikimi eshte kryer ne rregull apo jo.</returns>
        /// </summary>
        internal clsMesazh modifikoPasqyraFinanciareKoka(int idpasqyres, String kodipasqyres, String emertimipasqyres, String tipipasqyres, String metodapasq, int idperdoruesi, int idstatusdok)
        {
            dbManager.Open();
            dbManager.CreateParameters(7);
            dbManager.AddParameters(0, "@IDPASQFINKOKA", idpasqyres, ParameterDirection.Input);
            dbManager.AddParameters(1, "@KODIPASQFINKOKA", kodipasqyres, ParameterDirection.Input);
            dbManager.AddParameters(2, "@EMERTIMIPASQFINKOKA", emertimipasqyres, ParameterDirection.Input);
            dbManager.AddParameters(3, "@TIPIPASQFINKOKA", tipipasqyres, ParameterDirection.Input);
            dbManager.AddParameters(4, "@METODAPASQFINKOKA", metodapasq, ParameterDirection.Input);
            dbManager.AddParameters(5, "@IDPERDORUESI", idperdoruesi, ParameterDirection.Input);
            dbManager.AddParameters(6, "@IDSTATUSDOK", idstatusdok, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_PASQYRAFINANCIAREKOKA_upd");

            idpasqyres = int.Parse(dbManager.Parameters[0].Value.ToString());
            return new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
        }
        //[Obsolete("Perdor: clsMesazh modifikoPasqyraFinanciareKoka(int idpasqyres, String kodipasqyres, String emertimipasqyres, String tipipasqyres, String metodapasq, int idperdoruesi)", true)]
        //public clsMesazh modifikoPasqyraFinanciareKoka(clsPasqyreFinanciare koka)
        //{
        //    try
        //    {
        //        dbManager.CreateParameters(6);
        //        dbManager.AddParameters(0, "@IDPASQFINKOKA", koka.IdPasqyresFin, ParameterDirection.Input);
        //        dbManager.AddParameters(1, "@KODIPASQFINKOKA", koka.KodiPasqyresFin, ParameterDirection.Input);
        //        dbManager.AddParameters(2, "@EMERTIMIPASQFINKOKA", koka.EmertimiPasqyresFin, ParameterDirection.Input);
        //        dbManager.AddParameters(3, "@TIPIPASQFINKOKA", koka.TipiPasqyresFin, ParameterDirection.Input);
        //        dbManager.AddParameters(4, "@METODAPASQFINKOKA", koka.Metoda, ParameterDirection.Input);
        //        dbManager.AddParameters(5, "@IDPERDORUESI", koka.IdPerdoruesi, ParameterDirection.Input);
        //        dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_PASQYRAFINANCIAREKOKA_upd");

        //        koka.IdPasqyresFin = int.Parse(dbManager.Parameters[0].Value.ToString());
        //        return new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
        //    }
        //    catch (Exception)
        //    {

        //        return new clsMesazh(false, ce.Message);
        //    }
        //}

        /// <summary>
        /// ekzekuton prc_T_PASQYRAFINANCIARETRUPI_ins ne DB dhe kthen statusin e perfundimit te ekzekutimit te SP-se perkatese
        /// Shiko clsMesazh brenda ketij moduli per informacion te metejshem mbi vleren e kthimit te kesaj metode
        /// <param name="idtrupi"> id e trupit te pasqyres financiare</param>
        /// <param name="idkoka"> id e kokes se pasqyres financiare</param>
        /// <param name="pershkrimizerit"> pershkrimi i pasqyres financiare</param>
        /// <param name="prindizerit"> prindi i zerit</param>
        /// <param name="nivelizerit"> niveli ne te cilen ndodhet pasqyra</param>
        /// <param name="llojizerit"> lloji i zerit</param>
        /// <param name="gjenerotot"> nese gjenerohet ose jo totali</param>
        /// <returns> nje objekt clsMesazh qe tregon nese ruajtja eshte kryer ne rregull apo jo.</returns>
        /// </summary>
        internal clsMesazh ruajTrupinPasqyresFinanciare(out int idtrupi, int idkoka, String pershkrimizerit, String prindizerit, int nivelizerit, String llojizerit, int gjenerotot, string kodizeri, bool shfaqbij)
        {
            idtrupi = -1;

            dbManager.Open();
            dbManager.CreateParameters(9);
            dbManager.AddParameters(0, "@IDPASQFINTRUPI", idtrupi, ParameterDirection.Output);
            dbManager.AddParameters(1, "@IDPASQFINKOKA", idkoka, ParameterDirection.Input);
            dbManager.AddParameters(2, "@PERSHKRIMIZERIT", pershkrimizerit, ParameterDirection.Input);
            dbManager.AddParameters(3, "@PRINDI", prindizerit, ParameterDirection.Input);
            dbManager.AddParameters(4, "@NIVELI", nivelizerit, ParameterDirection.Input);
            dbManager.AddParameters(5, "@LLOJIZERIT", llojizerit, ParameterDirection.Input);
            dbManager.AddParameters(6, "@GJENEROTOTAL", gjenerotot, ParameterDirection.Input);
            dbManager.AddParameters(7, "@KODZERI", kodizeri, ParameterDirection.Input);
            dbManager.AddParameters(8, "@SHFAQBIJ", shfaqbij, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_PASQYRAFINANCIARETRUPI_ins");

            idtrupi = int.Parse(dbManager.Parameters[0].Value.ToString());
            return new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
        }
        //[Obsolete("Perdor: clsMesazh ruajTrupinPasqyresFinanciare(int idtrupi, int idkoka, String pershkrimizerit, String prindizerit, int nivelizerit, String llojizerit, Boolean gjenerotot)", true)]
        //public clsMesazh ruajTrupinPasqyresFinanciare(clsTrupPasqyreFinanciare trupi)
        //{
        //    try
        //    {
        //        dbManager.CreateParameters(7);
        //        dbManager.AddParameters(0, "@IDPASQFINTRUPI", trupi.IdTrupi, ParameterDirection.Output);
        //        dbManager.AddParameters(1, "@IDPASQFINKOKA", trupi.IdKoka, ParameterDirection.Input);
        //        dbManager.AddParameters(2, "@PERSHKRIMIZERIT", trupi.PershkrimiZerit, ParameterDirection.Input);
        //        dbManager.AddParameters(3, "@PRINDI", trupi.PrindiZerit, ParameterDirection.Input);
        //        dbManager.AddParameters(4, "@NIVELI", trupi.NiveliZerit, ParameterDirection.Input);
        //        dbManager.AddParameters(5, "@LLOJIZERIT", trupi.LlojiZerit, ParameterDirection.Input);
        //        dbManager.AddParameters(6, "@GJENEROTOTAL", trupi.GjeneroTotal, ParameterDirection.Input);
        //        dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_PASQYRAFINANCIARETRUPI_ins");

        //        trupi.IdTrupi = int.Parse(dbManager.Parameters[0].Value.ToString());
        //        return new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
        //    }
        //    catch (Exception)
        //    {

        //        return new clsMesazh(false, ce.Message);
        //    }
        //}

        /// <summary>
        /// ekzekuton prc_T_LLOGARIATRUPIPASQ_ins ne DB dhe kthen statusin e perfundimit te ekzekutimit te SP-se perkatese
        /// Shiko clsMesazh brenda ketij moduli per informacion te metejshem mbi vleren e kthimit te kesaj metode
        /// <param name="idllogariatrupi">id e llogarise se trupit</param>
        /// <param name="idtrupi">id e trupit</param>
        /// <param name="idllogaria">id e llogarise</param>
        /// <param name="emertimillog">emertimi i llogarise </param>
        /// <param name="gjendjallog">gjendja e llogarise</param>
        /// <param name="shenjallog">shenja e llogarise</param>
        /// <param name="idperdoruesi">id e perdoruesit</param>
        /// <param name="lloji">lloji i perdoruesit</param>
        /// <returns> nje objekt clsMesazh qe tregon nese ruajtja eshte kryer ne rregull apo jo.</returns>
        /// </summary>
        //internal clsMesazh ruajLlogariTrupiPasqyres(int idllogariatrupi, int idtrupi, int idllogaria, String emertimillog, String gjendjallog, String shenjallog, int idperdoruesi, string lloji, bool shfaqbij)
        //{
        //    idllogariatrupi = -1;

        //    dbManager.Open();
        //    dbManager.CreateParameters(9);
        //    dbManager.AddParameters(0, "@IDLLOGARIATRUPIPASQ", idllogariatrupi, ParameterDirection.Output);
        //    dbManager.AddParameters(1, "@IDTRUPIPASQFIN", idtrupi, ParameterDirection.Input);
        //    dbManager.AddParameters(2, "@IDLLOGARI", idllogaria, ParameterDirection.Input);
        //    dbManager.AddParameters(3, "@EMERTIMI", emertimillog, ParameterDirection.Input);
        //    dbManager.AddParameters(4, "@GJENDJA", gjendjallog, ParameterDirection.Input);
        //    dbManager.AddParameters(5, "@SHENJA", shenjallog, ParameterDirection.Input);
        //    dbManager.AddParameters(6, "@IDPERDORUESI", idperdoruesi, ParameterDirection.Input);
        //    dbManager.AddParameters(7, "@LLOJI", lloji, ParameterDirection.Input);
        //    dbManager.AddParameters(8, "@SHFAQBIJ", shfaqbij, ParameterDirection.Input);
        //    dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_LLOGARIATRUPIPASQ_ins");
        //    return new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
        //}

        /// <summary>
        /// ekzekuton prc_T_LLOGARIATRUPIPASQ_MERGEDT ne DB dhe kthen statusin e perfundimit te ekzekutimit te SP-se perkatese
        /// </summary>
        /// <param name="dt">datatable me te gjitha llogarite qe duhet te ruhen ne DB</param>
        /// <returns>nje objekt clsMesazh qe tregon nese ruajtja eshte kryer ne rregull apo jo.</returns>
        internal clsMesazh ruajLlogariTrupiPasqyresDt(DataTable dt)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddInputParameters("@VLERAT_NDRYSHUARA", dt);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_LLOGARIATRUPIPASQ_MERGEDT");
            return new clsMesazh(true, IMBUtils.Messages.MessagesResource.Messages["labelRaportMesazhRuajtjaPerfundoiSukses"]);
        }
        //[Obsolete("Perdor: clsMesazh ruajLlogariTrupiPasqyres(int idllogariatrupi, int idtrupi, int idllogaria, String emertimillog, String gjendjallog, String shenjallog, int idperdoruesi, string lloji)", true)]
        //public clsMesazh ruajLlogariTrupiPasqyres(clsLlogariaTrupiPasqyres llogari)
        //{
        //    try
        //    {
        //        dbManager.CreateParameters(8);
        //        dbManager.AddParameters(0, "@IDLLOGARIATRUPIPASQ", llogari.IdLlogariaTrupi, ParameterDirection.Output);
        //        dbManager.AddParameters(1, "@IDTRUPIPASQFIN", llogari.IdTrupi, ParameterDirection.Input);
        //        dbManager.AddParameters(2, "@IDLLOGARI", llogari.IdLlogaria, ParameterDirection.Input);
        //        dbManager.AddParameters(3, "@EMERTIMI", llogari.Emertimi, ParameterDirection.Input);
        //        dbManager.AddParameters(4, "@GJENDJA", llogari.Gjendja, ParameterDirection.Input);
        //        dbManager.AddParameters(5, "@SHENJA", llogari.Shenja, ParameterDirection.Input);
        //        dbManager.AddParameters(6, "@IDPERDORUESI", llogari.IdPerdoruesi, ParameterDirection.Input);
        //        dbManager.AddParameters(7, "@LLOJI", llogari.Lloji, ParameterDirection.Input);
        //        dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_LLOGARIATRUPIPASQ_ins");
        //        return new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
        //    }
        //    catch (Exception)
        //    {

        //        return new clsMesazh(false, ce.Message);
        //    }
        //}

        ///// <summary>
        ///// Ruan nje objekt pash sebashku me trupin , llogarite dhe buxhetet
        ///// Nje objekt pash ka nje koleksion me trupin, llogarite dhe buxhetet
        ///// ruajtja e nje pash imponon ruajtjen edhe te nje colection-i me trupin ,llogarite dhe buxhetet
        ///// Mqs cdo rresht i ri qe shtohet ne DB kerkon thirrjen e nje SP-je me parametra dhe pash se bashku me trupin, llogarite dhe buxhetet konsiderohet si nje regjistrim,
        ///// perdoret nje transaksion qe imponon regjistrimin e rregullt te nje pash sebashku me trupin, llogarite dhe buxhetet perkates
        ///// </summary>
        /////<param name="pash">pash qe do te ruhet</param>
        /////  <returns> kthen nje obj clsMesazh per te identifikuar statusin e ruajtjes se te dhenave ne DB</returns>
        //[Obsolete("Perdor nga klasa perkatese: clsMesazh ruajPASH(int idpasqyres, String kodipasqyres, String emertimipasqyres, String tipipasqyres, String metoda, int idndermarja, int vit, int idndermvit, int idperdoruesi, colTrupPasqyreFinaciare ocoltrupipash)", true)]
        //public clsMesazh ruajPASH(clsPASH pash)
        //{
        //    dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        //    dbManager.ConnectionString = dbManager.GetConnectionString();
        //    dbManager.Open();
        //    dbManager.BeginTransaction();
        //    clsMesazh mesazh = new clsMesazh();
        //    try
        //    {
        //        int idP;
        //        mesazh = ruajPasqyraFinanciareKoka(out idP, pash.KodiPasqyresFin, pash.EmertimiPasqyresFin, pash.TipiPasqyresFin, pash.Metoda, pash.IdNdermarja, pash.Viti, pash.IdNdermVit, pash.IdPerdoruesi);
        //        if (mesazh.Status)
        //        {
        //            pash.IdPasqyresFin = idP;
        //            foreach (clsTrupPasqyreFinanciare trupi in pash.OColTrupiPASH)
        //            {
        //                if (mesazh.Status)
        //                {
        //                    trupi.IdKoka = pash.IdPasqyresFin;
        //                    int idT;
        //                    mesazh = ruajTrupinPasqyresFinanciare(out idT, trupi.IdKoka, trupi.PershkrimiZerit, trupi.PrindiZerit, trupi.NiveliZerit, trupi.LlojiZerit, trupi.GjeneroTotal);
        //                    foreach (clsLlogariaTrupiPasqyres llog in trupi.OColLlogarite)
        //                    {
        //                        if (mesazh.Status)
        //                        {
        //                            trupi.IdTrupi = idT;
        //                            llog.IdTrupi = trupi.IdTrupi;
        //                            mesazh = ruajLlogariTrupiPasqyres(llog.IdLlogariaTrupi, llog.IdTrupi, llog.IdLlogaria, llog.Emertimi, llog.Gjendja, llog.Shenja, llog.IdPerdoruesi, llog.Lloji);
        //                        }
        //                        else
        //                        {
        //                            dbManager.Transaction.Rollback();
        //                            return mesazh;
        //                        }
        //                    }

        //                    foreach (clsBuxheti buxheti in trupi.OColBuxhetet)
        //                    {
        //                        if (mesazh.Status)
        //                        {
        //                            buxheti.IdLidhese = trupi.IdTrupi;
        //                            int idB;
        //                            mesazh = ruajBuxhet(out idB, buxheti.IdLlojBuxheti, buxheti.IdLidhese, buxheti.Muaj, buxheti.Buxheti_1, buxheti.Buxheti_2);
        //                        }
        //                        else
        //                        {
        //                            dbManager.Transaction.Rollback();
        //                        return mesazh;
        //                        }
        //                    }
        //                }

        //                else
        //                {
        //                    dbManager.Transaction.Rollback();
        //                return mesazh;
        //                }
        //            }
        //            if (mesazh.Status)
        //            {
        //                dbManager.CommitTransaction();
        //                mesazh = new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
        //                return mesazh;
        //            }
        //            else
        //            {
        //                dbManager.Transaction.Rollback();
        //                return mesazh;
        //            }
        //        }
        //        else
        //        {
        //            dbManager.Transaction.Rollback();
        //        return mesazh;
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        dbManager.Transaction.Rollback();
        //        return new clsMesazh(false, ce.Message);
        //    }
        //    finally
        //    {
        //        dbManager.Dispose();
        //    }
        //}

        ///// <summary>
        ///// Ruan nje objekt cash flow sebashku me trupin , llogarite 
        ///// Nje objekt cash flow ka nje koleksion me trupin, llogarite 
        ///// ruajtja e nje cash flow imponon ruajtjen edhe te nje colection-i me trupin ,llogarite 
        ///// Mqs cdo rresht i ri qe shtohet ne DB kerkon thirrjen e nje SP-je me parametra dhe cash flow se bashku me trupin, llogarite  konsiderohet si nje regjistrim,
        ///// perdoret nje transaksion qe imponon regjistrimin e rregullt te nje cash flow sebashku me trupin, llogarite  perkates
        ///// </summary>
        /////<param name="cashFlow">cash flow qe do te ruhet</param>
        /////  <returns> kthen nje obj clsMesazh per te identifikuar statusin e ruajtjes se te dhenave ne DB</returns>
        //[Obsolete("Perdor nga klasa perkatese: clsMesazh ruajCashFlow(int idpasqyres, String kodipasqyres, String emertimipasqyres, String tipipasqyres, String metoda, int idndermarja, int vit, int idndermvit, int idperdoruesi, colTrupPasqyreFinaciare oColTrupiCashFlow)", true)]
        //public clsMesazh ruajCashFlow(clsCashFlow cashFlow)
        //{
        //    dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        //    dbManager.ConnectionString = dbManager.GetConnectionString();
        //    dbManager.Open();
        //    dbManager.BeginTransaction();
        //    clsMesazh mesazh = new clsMesazh();
        //    try
        //    {
        //        int idC;
        //        mesazh = ruajPasqyraFinanciareKoka(out idC, cashFlow.KodiPasqyresFin, cashFlow.EmertimiPasqyresFin, cashFlow.TipiPasqyresFin, cashFlow.Metoda, cashFlow.IdNdermarja, cashFlow.Viti, cashFlow.IdNdermVit, cashFlow.IdPerdoruesi);
        //        if (mesazh.Status)
        //        {
        //            cashFlow.IdPasqyresFin = idC;
        //            foreach (clsTrupPasqyreFinanciare trupi in cashFlow.OColTrupiCashFlow)
        //            {
        //                if (mesazh.Status)
        //                {
        //                    trupi.IdKoka = cashFlow.IdPasqyresFin;
        //                    int idT;
        //                    mesazh = ruajTrupinPasqyresFinanciare(out idT, trupi.IdKoka, trupi.PershkrimiZerit, trupi.PrindiZerit, trupi.NiveliZerit, trupi.LlojiZerit, trupi.GjeneroTotal);
        //                    if (mesazh.Status)
        //                    {
        //                        trupi.IdTrupi = idT;
        //                        foreach (clsLlogariaTrupiPasqyres llog in trupi.OColLlogarite)
        //                        {
        //                            if (mesazh.Status)
        //                            {
        //                                llog.IdTrupi = trupi.IdTrupi;
        //                                mesazh = ruajLlogariTrupiPasqyres(llog.IdLlogariaTrupi, llog.IdTrupi, llog.IdLlogaria, llog.Emertimi, llog.Gjendja, llog.Shenja, llog.IdPerdoruesi, llog.Lloji);
        //                            }

        //                            else
        //                            {
        //                                dbManager.Transaction.Rollback();
        //                                return mesazh;
        //                            }
        //                        }
        //                    }
        //                    else
        //                    {
        //                        dbManager.Transaction.Rollback();
        //                        return mesazh;
        //                    }
        //                }
        //                else
        //                {
        //                    dbManager.Transaction.Rollback();
        //                    return mesazh;
        //                }
        //            }
        //            if (mesazh.Status)
        //            {
        //                dbManager.CommitTransaction();
        //                mesazh = new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
        //                return mesazh;
        //            }
        //            else
        //            {
        //                dbManager.Transaction.Rollback();
        //                return mesazh;
        //            }
        //        }

        //        else
        //        {
        //            dbManager.Transaction.Rollback();
        //            return mesazh;
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        dbManager.Transaction.Rollback();
        //        return new clsMesazh(false, ce.Message);
        //    }
        //    finally
        //    {
        //        dbManager.Dispose();
        //    }
        //}

        /// <summary>
        /// kthen datatable pasqyra finaciare sipas tipi dhe ndermarjes
        /// </summary>
        ///<param name="idndermarje"> id e ndermarjes</param>
        ///<param name="idndermarjeviti">id e ndermarje vitit</param>
        ///<param name="tipi">tipi</param>
        ///<example>Bilanc, CashFlow, Pash</example>
        ///<returns>nje datatable me te gjitha pasqyrat financiare te kesaj ndermarje te ketij tipi  </returns>
        internal DataTable kthePasqyraFinaciareSipasTipitRaporte(string tipi, int idndermarje, int idraporti)
        {
            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@TIPIPASQFINKOKA", tipi, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idndermarje, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDRAPORTI", idraporti, ParameterDirection.Input);
            //dbManager.AddParameters(2, "@IDNDERVITI", idndermarjeviti, ParameterDirection.Input);
            //dbManager.AddParameters(2, "@IDNDERVITI", idndermarjeviti, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_PASQYRAFINANCIAREKOKA_MerrPasqyrafinaciareSipasTipitDheRaportit");
            return ds.Tables[0];
        }

        /// <summary>
        /// kthen datatable pasqyra finaciare sipas tipi dhe ndermarjes
        /// </summary>
        ///<param name="idndermarje"> id e ndermarjes</param>
        ///<param name="idndermarjeviti">id e ndermarje vitit</param>
        ///<param name="tipi">tipi</param>
        ///<example>Bilanc, CashFlow, Pash</example>
        ///<returns>nje datatable me te gjitha pasqyrat financiare te kesaj ndermarje te ketij tipi  </returns>
        internal DataTable kthePasqyraFinaciareSipasTipit(string tipi, int idndermarje)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@TIPIPASQFINKOKA", tipi, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idndermarje, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_PASQYRAFINANCIAREKOKA_MerrPasqyrafinaciareSipasTipit");
            return ds.Tables[0];
        }
        //[Obsolete("Perdor: DataTable kthePasqyraFinaciareSipasTipit(string tipi, int idndermarje, int idndermarjeviti)", true)]
        //public colPasqyratFinaciare merrPasqyraFinaciareSipasTipit(string tipi, int idndermarje, int idndermarjeviti)
        //{
        //    IDBManager dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        //    dbManager.ConnectionString = dbManager.GetConnectionString();
        //    try
        //    {
        //        dbManager.Open();
        //        dbManager.CreateParameters(3);
        //        dbManager.AddParameters(0, "@TIPIPASQFINKOKA", tipi, ParameterDirection.Input);
        //        dbManager.AddParameters(1, "@IDNDERMARJE", idndermarje, ParameterDirection.Input);
        //        dbManager.AddParameters(2, "@IDNDERVITI", idndermarjeviti , ParameterDirection.Input);
        //        DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_PASQYRAFINANCIAREKOKA_MerrPasqyrafinaciareSipasTipit");
        //        colPasqyratFinaciare pas = new colPasqyratFinaciare();
        //        return pas.mbushArrayListPasqyrat(ds);
        //    }
        //    catch (Exception)
        //    {
        //        return new colPasqyratFinaciare();
        //    }
        //    finally
        //    {
        //        dbManager.Dispose();
        //    }

        //}

        /// <summary>
        /// kthen datarow pasqyra finaciare sipas id
        /// </summary>
        ///<param name="id"> id e pasqyres financiare</param>
        ///<returns>nje datarow me pasqyren financiare me kete id  </returns>
        internal DataRow kthePasqyraFinaciareSipasId(int id)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDPASQFINKOKA", id, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_PASQYRAFINANCIAREKOKA_MerrPasqyrafinaciareSipasId");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];
        }

        /// <summary>
        /// kthen objektet trupi pasqyra finaciare sipas id kokes
        /// </summary>
        ///<param name="id"> id e kokes te pasqyres financiare</param>
        ///<returns>nje objekt colTrupPasqyreFinaciare qe permban nje koleksion me te gjitha trupat pasqyrat financiare me kete id koke </returns>
        internal DataTable kthePasqyraFinaciareTrupiSipasIdKoka(int id)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDPASQFINKOKA", id, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_PASQYRAFINANCIARETRUPI_merrSipasIdKoka");
            return ds.Tables[0];
        }

        /// <summary>
        /// kthen objektet trupi pasqyra finaciare sipas llojit te zerit dhe id kokes
        /// </summary>
        ///<param name="idkoka"> id e kokes te pasqyres financiare</param>
        ///<param name="lloji"> lloji i zerit</param>
        ///<example> aktive, detyrimet, kapitali, pash , llogarite cash, fluks shfrytezimi, fluks investues, fluks financiar</example>
        ///<returns>nje objekt colTrupPasqyreFinaciare qe permban nje koleksion me te gjitha trupat pasqyrat financiare me kete id koke dhe te ketij lloji </returns>
        internal DataTable kthePasqyraFinaciareTrupiSipasLlojit(string lloji, int idkoka)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@LLOJIZERIT", lloji, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPASQFINKOKA", idkoka, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_PASQYRAFINANCIARETRUPI_merrSipasLlojit");
            return ds.Tables[0];
        }

        /// <summary>
        /// kthen datatable sipas id se trupit
        /// </summary>
        ///<param name="id"> id e trupit te pasqyres financiare</param>
        ///<returns>nje datatable qe permban nje koleksion me te gjitha llogarite pasqyrat financiare me kete id trupi </returns>
        internal DataTable ktheLlogariPasqyraFinaciareTrupiSipasIdTrupi(int id)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDTRUPIPASQFIN", id, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_LLOGARIATRUPIPASQ_merrSipasIdTrupi");
            return ds.Tables[0];
        }

        internal clsMesazh fshiPasqyreKokaStatus(int idPasqyresFin, int idperdorues)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDPASQFINKOKA", idPasqyresFin, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUESI", idperdorues, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_PASQYRAFINANCIAREKOKA_upddel");
            return new clsMesazh(true, "Fshirja perfundoi me sukses");
        }

        /// <summary>
        /// ekzekutohet sp-ja prc_T_PASQYRAFINANCIARETRUPI_del duke i kaluar id e trupit se pasqyres financiare qe e marrim nga objekti clsTrupPasqyreFinanciare qe i kalohet si parameter
        /// </summary>
        /// <param name="idtrupi"> id e trupit te pasqyres financiare</param>
        /// <returns>nje objekt clsMesazh qe tregon nese fshirja eshte kryer ne rregull apo jo</returns>
        internal clsMesazh fshiPasqyreTrupi(int idtrupi)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDPASQFINTRUPI", idtrupi, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_PASQYRAFINANCIARETRUPI_del");
            return new clsMesazh(true, "Fshirja perfundoi me sukses");
        }

        /// <summary>
        /// ekzekutohet sp-ja prc_T_LLOGARIATRUPIPASQ_del duke i kaluar id e llogarise se pasqyres financiare qe e marrim nga objekti clsLlogariaTrupiPasqyres qe i kalohet si parameter
        /// </summary>
        /// <param name="idTrupi"> id e trupit te pasqyres </param>
        /// <returns>nje objekt clsMesazh qe tregon nese fshirja eshte kryer ne rregull apo jo</returns>
        internal clsMesazh fshiLlogariPasqyreTrupi(int idTrupi)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDLLOGARIATRUPIPASQ", idTrupi, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_LLOGARIATRUPIPASQ_del");
            return new clsMesazh(true, "Fshirja perfundoi me sukses");
        }

        ///// <summary>
        ///// Modifikon nje objekt bilanci sebashku me te trupin,llogarite dhe buxhetet
        ///// Nje objekt bilanci ka nje koleksion me trupin,llogarite dhe buxhetet perkates , 
        ///// modifikimi e nje bilanci imponon modifikimin edhe te nje colection-i me trupin,llogarite dhe buxhetet
        ///// Mqs cdo rresht i ri qe modifikon ne DB kerkon thirrjen e nje SP-je me parametra dhe bilanci bashke me trupin,llogarite dhe buxhetet konsiderohet si nje regjistrim,
        ///// perdoret nje transaksion qe imponon modifikimin e rregullt te nje bilanci sebashku me trupin,llogarite dhe buxhetet
        ///// 1. merren te gjithe rreshtat e trupit,llogarive dhe buxhetet te kesaj pasqyre
        ///// 2. fshihen rreshtat eksistues
        ///// 3. modifikohet koka e pasqyres financiare
        ///// 4. ruhet trupi,llogarite dhe buxhetet e modifikuara
        ///// </summary>
        /////<param name="pas"> bilanci qe do modifikohet</param>
        ///// <returns> kthen nje obj clsMesazh per te identifikuar statusin e modifikimit se te dhenave ne DB</returns>
        //[Obsolete("Perdor nga klasa perkatese: clsMesazh modifikoBilancAndBuxhete(int idpasqyres, String kodipasqyres, String emertimipasqyres, String tipipasqyres, String metoda, int idperdoruesi, colTrupPasqyreFinaciare ocoltrupibilancit)", true)]
        //public clsMesazh modifikoBilancAndBuxhete(clsBilanc pas)
        //{
        //    //clsPasqyreFinanciare paseksistuese=merrPasqyraFinaciareSipasId (pas.IdPasqyresFin )[0];
        //    //colTrupPasqyreFinaciare colTrupi = merrPasqyraFinaciareTrupiSipasIdKoka(pas.IdPasqyresFin);
        //    colTrupPasqyreFinaciare colTrupi = new colTrupPasqyreFinaciare(pas.IdPasqyresFin);
        //    colBuxhetet colBuxh = new colBuxhetet();
        //    colLlogariaTrupiPasqyres colLlog = new colLlogariaTrupiPasqyres();
        //    foreach (clsTrupPasqyreFinanciare t in colTrupi)
        //    {
        //        //colLlojeBuxhetesh colLloj = merrLlojBuxhetiSipasKodit("PasqyreFinanciare");
        //        colBuxh.AddRange(new colBuxhetet(t.IdTrupi, DbCore.DbKontabiliteti.clsLlojBuxheti.mbushIDLlojBuxheti("PasqyreFinanciare")));
        //        //colLlog.AddRange(merrLlogariPasqyraFinaciareTrupiSipasIdTrupi(t.IdTrupi));
        //        colLlog.AddRange(new colLlogariaTrupiPasqyres(t.IdTrupi));
        //    } 
        //    dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        //    dbManager.ConnectionString = dbManager.GetConnectionString();
        //    dbManager.Open();
        //    dbManager.BeginTransaction();
        //    clsMesazh mesazh = new clsMesazh(true);
        //    try
        //    {


        //        foreach (clsBuxheti o in colBuxh)
        //        {
        //            if (mesazh.Status)

        //                mesazh = fshiBuxhet(o.IdLidhese);

        //            else
        //            {
        //                dbManager.Transaction.Rollback();
        //                return mesazh;
        //            }
        //        }
        //            foreach (clsLlogariaTrupiPasqyres l in colLlog)
        //            {
        //                if (mesazh.Status)
        //                {
        //                    mesazh = fshiLlogariPasqyreTrupi(l.IdTrupi);
        //                }
        //                else
        //                {
        //                    dbManager.Transaction.Rollback();
        //                    return mesazh;
        //                }
        //            }
        //            foreach (clsTrupPasqyreFinanciare t in colTrupi)
        //            {
        //                if (mesazh.Status)
        //                {
        //                    mesazh= fshiPasqyreTrupi(t.IdTrupi);
        //                }
        //                else
        //                {
        //                    dbManager.Transaction.Rollback();
        //                    return mesazh;
        //                }
        //            }

        //            if (mesazh.Status)
        //            {
        //                mesazh= modifikoPasqyraFinanciareKoka(pas.IdPasqyresFin, pas.KodiPasqyresFin, pas.EmertimiPasqyresFin, pas.TipiPasqyresFin, pas.Metoda, pas.IdPerdoruesi);
        //                if (mesazh.Status)
        //                {
        //                    foreach (clsTrupPasqyreFinanciare trupi in pas.OColTrupiBilancit)
        //                    {
        //                        if (mesazh.Status)
        //                        {
        //                            trupi.IdKoka = pas.IdPasqyresFin;
        //                            int idT;
        //                            mesazh = ruajTrupinPasqyresFinanciare(out idT, trupi.IdKoka, trupi.PershkrimiZerit, trupi.PrindiZerit, trupi.NiveliZerit, trupi.LlojiZerit, trupi.GjeneroTotal);
        //                            if (mesazh.Status)
        //                            {
        //                                trupi.IdTrupi = idT;
        //                                foreach (clsLlogariaTrupiPasqyres llog in trupi.OColLlogarite)
        //                                {
        //                                    if (mesazh.Status)
        //                                    {
        //                                        llog.IdTrupi = trupi.IdTrupi;
        //                                        mesazh = ruajLlogariTrupiPasqyres(llog.IdLlogariaTrupi, llog.IdTrupi, llog.IdLlogaria, llog.Emertimi, llog.Gjendja, llog.Shenja, llog.IdPerdoruesi, llog.Lloji);
        //                                    }
        //                                    else
        //                                    {
        //                                        dbManager.Transaction.Rollback();
        //                                        return mesazh;
        //                                    }
        //                                }

        //                                foreach (clsBuxheti buxheti in trupi.OColBuxhetet)
        //                                {
        //                                    if (mesazh.Status)
        //                                    {
        //                                        buxheti.IdLidhese = trupi.IdTrupi;
        //                                        int idB;
        //                                        mesazh = ruajBuxhet(out idB, buxheti.IdLlojBuxheti, buxheti.IdLidhese, buxheti.Muaj, buxheti.Buxheti_1, buxheti.Buxheti_2);
        //                                    }

        //                                    else
        //                                    {
        //                                        dbManager.Transaction.Rollback();
        //                                        return mesazh;
        //                                    }
        //                                }
        //                            }
        //                            else
        //                            {
        //                                dbManager.Transaction.Rollback();
        //                                return mesazh;
        //                            }
        //                        }
        //                        else
        //                        {
        //                            dbManager.Transaction.Rollback();
        //                            return mesazh;
        //                        }
        //                    }
        //                    if (mesazh.Status)
        //                    {
        //                        dbManager.CommitTransaction();
        //                        mesazh = new clsMesazh(true, MessagesResource.Messages["msgModifikimiMeSukses"]);
        //                        return mesazh;
        //                    }
        //                    else
        //                    {
        //                        dbManager.Transaction.Rollback();
        //                        return mesazh;
        //                    }
        //                }
        //                else
        //                {
        //                    dbManager.Transaction.Rollback();
        //                    return mesazh;
        //                }
        //            }
        //            else
        //            {
        //                dbManager.Transaction.Rollback();
        //                return mesazh;
        //            }
        //    }
        //    catch (Exception)
        //    {
        //        dbManager.Transaction.Rollback();
        //        return new clsMesazh(false, ce.Message);
        //    }
        //    finally
        //    {
        //        dbManager.Dispose();
        //    }

        //}

        ///// <summary>
        ///// Modifikon nje objekt pash sebashku me te trupin,llogarite dhe buxhetet
        ///// Nje objekt pash ka nje koleksion me trupin,llogarite dhe buxhetet perkates , 
        ///// modifikimi e nje pash imponon modifikimin edhe te nje colection-i me trupin,llogarite dhe buxhetet
        ///// Mqs cdo rresht i ri qe modifikon ne DB kerkon thirrjen e nje SP-je me parametra dhe pash bashke me trupin,llogarite dhe buxhetet konsiderohet si nje regjistrim,
        ///// perdoret nje transaksion qe imponon modifikimin e rregullt te nje pash sebashku me trupin,llogarite dhe buxhetet
        ///// 1. merren te gjithe rreshtat e trupit,llogarive dhe buxhetet te kesaj pasqyre
        ///// 2. fshihen rreshtat eksistues
        ///// 3. modifikohet koka e pasqyres financiare
        ///// 4. ruhet trupi,llogarite dhe buxhetet e modifikuara
        ///// </summary>
        /////<param name="pas"> pash qe do modifikohet</param>
        ///// <returns> kthen nje obj clsMesazh per te identifikuar statusin e modifikimit se te dhenave ne DB</returns>
        //[Obsolete("Perdor nga klasa perkatese: clsMesazh modifikoPASHAndBuxhete(int idpasqyres, String kodipasqyres, String emertimipasqyres, String tipipasqyres, String metoda, int idperdoruesi, colTrupPasqyreFinaciare ocoltrupipash)", true)]
        //public clsMesazh modifikoPASHAndBuxhete(clsPASH  pas)
        //{
        //    //clsPasqyreFinanciare paseksistuese = merrPasqyraFinaciareSipasId(pas.IdPasqyresFin)[0];
        //    //colTrupPasqyreFinaciare colTrupi = merrPasqyraFinaciareTrupiSipasIdKoka(pas.IdPasqyresFin);
        //    colTrupPasqyreFinaciare colTrupi = new colTrupPasqyreFinaciare(pas.IdPasqyresFin);
        //    colBuxhetet colBuxh = new colBuxhetet();
        //    colLlogariaTrupiPasqyres colLlog = new colLlogariaTrupiPasqyres();
        //    foreach (clsTrupPasqyreFinanciare t in colTrupi)
        //    {
        //        //colLlojeBuxhetesh colLloj = merrLlojBuxhetiSipasKodit("PasqyreFinanciare");
        //        //colBuxh.AddRange(merrBuxhetetSipasIdLidheseIdLlojBuxheti(t.IdTrupi, DbCore.DbKontabiliteti.clsLlojBuxheti.mbushIDLlojBuxheti("PasqyreFinanciare")));
        //        colBuxh.AddRange(new colBuxhetet(t.IdTrupi, DbCore.DbKontabiliteti.clsLlojBuxheti.mbushIDLlojBuxheti("PasqyreFinanciare")));
        //        //colLlog.AddRange(merrLlogariPasqyraFinaciareTrupiSipasIdTrupi(t.IdTrupi));
        //        colLlog.AddRange(new colLlogariaTrupiPasqyres(t.IdTrupi));
        //    }
        //    dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        //    dbManager.ConnectionString = dbManager.GetConnectionString();
        //    dbManager.Open();
        //    dbManager.BeginTransaction();
        //    clsMesazh mesazh = new clsMesazh(true);
        //    try
        //    {


        //        foreach (clsBuxheti o in colBuxh)
        //        {
        //            if (mesazh.Status)
        //            {
        //                mesazh = fshiBuxhet(o.IdLidhese);
        //            }
        //            else
        //            {
        //                dbManager.Transaction.Rollback();
        //                return mesazh;
        //            }
        //        }
        //        foreach (clsLlogariaTrupiPasqyres l in colLlog)
        //        {
        //            if (mesazh.Status)
        //            {
        //                mesazh = fshiLlogariPasqyreTrupi(l.IdTrupi);
        //            }
        //            else
        //            {
        //                dbManager.Transaction.Rollback();
        //                return mesazh;
        //            }
        //        }
        //        foreach (clsTrupPasqyreFinanciare t in colTrupi)
        //        {
        //            if (mesazh.Status)
        //            {
        //                mesazh = fshiPasqyreTrupi(t.IdTrupi);
        //            }
        //            else
        //            {
        //                dbManager.Transaction.Rollback();
        //                return mesazh;
        //            }
        //        }
        //        if (mesazh.Status)
        //        {

        //            mesazh = modifikoPasqyraFinanciareKoka(pas.IdPasqyresFin, pas.KodiPasqyresFin, pas.EmertimiPasqyresFin, pas.TipiPasqyresFin, pas.Metoda, pas.IdPerdoruesi);
        //            if (mesazh.Status)
        //            {
        //                foreach (clsTrupPasqyreFinanciare trupi in pas.OColTrupiPASH)
        //                {
        //                    if (mesazh.Status)
        //                    {
        //                        trupi.IdKoka = pas.IdPasqyresFin;
        //                        int idT;
        //                        mesazh = ruajTrupinPasqyresFinanciare(out idT, trupi.IdKoka, trupi.PershkrimiZerit, trupi.PrindiZerit, trupi.NiveliZerit, trupi.LlojiZerit, trupi.GjeneroTotal);

        //                        foreach (clsLlogariaTrupiPasqyres llog in trupi.OColLlogarite)
        //                        {
        //                            if (mesazh.Status)
        //                            {
        //                                trupi.IdTrupi = idT;
        //                                llog.IdTrupi = trupi.IdTrupi;
        //                                mesazh = ruajLlogariTrupiPasqyres(llog.IdLlogariaTrupi, llog.IdTrupi, llog.IdLlogaria, llog.Emertimi, llog.Gjendja, llog.Shenja, llog.IdPerdoruesi, llog.Lloji);
        //                            }
        //                            else
        //                            {
        //                                dbManager.Transaction.Rollback();
        //                                return mesazh;
        //                            }
        //                        }

        //                        foreach (clsBuxheti buxheti in trupi.OColBuxhetet)
        //                        {
        //                            if (mesazh.Status)
        //                            {
        //                                buxheti.IdLidhese = trupi.IdTrupi;
        //                                int idB;
        //                                mesazh = ruajBuxhet(out idB, buxheti.IdLlojBuxheti, buxheti.IdLidhese, buxheti.Muaj, buxheti.Buxheti_1, buxheti.Buxheti_2);
        //                            }
        //                            else
        //                            {
        //                                dbManager.Transaction.Rollback();
        //                                return mesazh;
        //                            }
        //                        }
        //                    }
        //                    else
        //                    {
        //                        dbManager.Transaction.Rollback();
        //                        return mesazh;
        //                    }
        //                }
        //                if (mesazh.Status)
        //                {
        //                    dbManager.CommitTransaction();
        //                    mesazh = new clsMesazh(true, MessagesResource.Messages["msgModifikimiMeSukses"]);
        //                    return mesazh;
        //                }
        //                else
        //                {
        //                    dbManager.Transaction.Rollback();
        //                    return mesazh;
        //                }
        //            }
        //            else
        //            {
        //                dbManager.Transaction.Rollback();
        //                return mesazh;
        //            }
        //        }
        //        else
        //        {
        //            dbManager.Transaction.Rollback();
        //            return mesazh;
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        dbManager.Transaction.Rollback();
        //        return new clsMesazh(false, ce.Message);
        //    }
        //    finally
        //    {
        //        dbManager.Dispose();
        //    }

        //}

        ///// <summary>
        ///// Modifikon nje objekt cash flow sebashku me te trupin,llogarite 
        ///// Nje objekt cash flow ka nje koleksion me trupin,llogarite  perkates , 
        ///// modifikimi e nje cash flow imponon modifikimin edhe te nje colection-i me trupin,llogarite 
        ///// Mqs cdo rresht i ri qe modifikon ne DB kerkon thirrjen e nje SP-je me parametra dhe cash flow bashke me trupin,llogarite  konsiderohet si nje regjistrim,
        ///// perdoret nje transaksion qe imponon modifikimin e rregullt te nje cash flow sebashku me trupin,llogarite 
        ///// 1. merren te gjithe rreshtat e trupit,llogarive  te kesaj pasqyre
        ///// 2. fshihen rreshtat eksistues
        ///// 3. modifikohet koka e pasqyres financiare
        ///// 4. ruhet trupi,llogarite  e modifikuara
        ///// </summary>
        /////<param name="pas"> cash flow qe do modifikohet</param>
        ///// <returns> kthen nje obj clsMesazh per te identifikuar statusin e modifikimit se te dhenave ne DB</returns>
        //[Obsolete("Perdor nga klasa perkatese: clsMesazh modifikoCashFlowAndBuxhete(int idpasqyres, String kodipasqyres, String emertimipasqyres, String tipipasqyres, String metoda, int idperdoruesi, colTrupPasqyreFinaciare oColTrupiCashFlow)", true)]
        //public clsMesazh modifikoCashFlowAndBuxhete(clsCashFlow pas)
        //{
        //    //clsPasqyreFinanciare paseksistuese = merrPasqyraFinaciareSipasId(pas.IdPasqyresFin)[0];
        //    //colTrupPasqyreFinaciare colTrupi = merrPasqyraFinaciareTrupiSipasIdKoka(pas.IdPasqyresFin);
        //    colTrupPasqyreFinaciare colTrupi = new colTrupPasqyreFinaciare(pas.IdPasqyresFin);
        //    colLlogariaTrupiPasqyres colLlog = new colLlogariaTrupiPasqyres();
        //    foreach (clsTrupPasqyreFinanciare t in colTrupi)
        //    {
        //        colLlog.AddRange(new colLlogariaTrupiPasqyres(t.IdTrupi));
        //        //colLlog.AddRange(merrLlogariPasqyraFinaciareTrupiSipasIdTrupi(t.IdTrupi));
        //    }
        //    dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        //    dbManager.ConnectionString = dbManager.GetConnectionString();
        //    dbManager.Open();
        //    dbManager.BeginTransaction();
        //    clsMesazh mesazh = new clsMesazh(true);
        //    try
        //    {



        //        foreach (clsLlogariaTrupiPasqyres l in colLlog)
        //        {
        //            if (mesazh.Status)
        //            {
        //                mesazh = fshiLlogariPasqyreTrupi(l.IdTrupi);
        //            }
        //            else
        //            {
        //                dbManager.Transaction.Rollback();
        //                return mesazh;
        //            }
        //        }
        //        foreach (clsTrupPasqyreFinanciare t in colTrupi)
        //        {
        //            if (mesazh.Status)
        //            {
        //                mesazh = fshiPasqyreTrupi(t.IdTrupi);
        //            }
        //            else
        //            {
        //                dbManager.Transaction.Rollback();
        //                return mesazh;
        //            }
        //        }

        //        if (mesazh.Status)
        //        {
        //            mesazh = modifikoPasqyraFinanciareKoka(pas.IdPasqyresFin, pas.KodiPasqyresFin, pas.EmertimiPasqyresFin, pas.TipiPasqyresFin, pas.Metoda, pas.IdPerdoruesi);
        //            if (mesazh.Status)
        //            {
        //                foreach (clsTrupPasqyreFinanciare trupi in pas.OColTrupiCashFlow)
        //                {
        //                    if (mesazh.Status)
        //                    {
        //                        trupi.IdKoka = pas.IdPasqyresFin;
        //                        int idT;
        //                        mesazh = ruajTrupinPasqyresFinanciare(out idT, trupi.IdKoka, trupi.PershkrimiZerit, trupi.PrindiZerit, trupi.NiveliZerit, trupi.LlojiZerit, trupi.GjeneroTotal);
        //                        foreach (clsLlogariaTrupiPasqyres llog in trupi.OColLlogarite)
        //                        {
        //                            trupi.IdTrupi = idT;
        //                            if (mesazh.Status)
        //                            {
        //                                llog.IdTrupi = trupi.IdTrupi;
        //                                mesazh = ruajLlogariTrupiPasqyres(llog.IdLlogariaTrupi, llog.IdTrupi, llog.IdLlogaria, llog.Emertimi, llog.Gjendja, llog.Shenja, llog.IdPerdoruesi, llog.Lloji);
        //                            }
        //                            else
        //                            {
        //                                dbManager.Transaction.Rollback();
        //                                return mesazh;
        //                            }
        //                        }
        //                    }
        //                    else
        //                    {
        //                        dbManager.Transaction.Rollback();
        //                        return mesazh;
        //                    }


        //                }
        //                if (mesazh.Status)
        //                {
        //                    dbManager.CommitTransaction();
        //                    mesazh = new clsMesazh(true, MessagesResource.Messages["msgModifikimiMeSukses"]);
        //                    return mesazh;
        //                }
        //                else
        //                {
        //                    dbManager.Transaction.Rollback();
        //                    return mesazh;
        //                }
        //            }
        //            else
        //            {
        //                dbManager.Transaction.Rollback();
        //                return mesazh;
        //            }
        //        }
        //        else
        //        {
        //            dbManager.Transaction.Rollback();
        //            return mesazh;
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        dbManager.Transaction.Rollback();
        //        return new clsMesazh(false, ce.Message);
        //    }
        //    finally
        //    {
        //        dbManager.Dispose();
        //    }

        //}

        ///// <summary>
        ///// fshin nje objekt pasqyra financiare sebashku me  trupin,llogarite dhe buxhetet perkates
        ///// Nje objekt pasqyra financiare ka nje koleksion me trupin,llogarite dhe buxhetet, 
        ///// fshirja e nje pasqyra financiare imponon fshirjen edhe te nje colection-i me trupin,llogarite dhe buxhetet
        ///// Mqs cdo rresht i ri qe fshihet ne DB kerkon thirrjen e nje SP-je me parametra dhe pasqyra financiare bashke me trupin,llogarite dhe buxhetet konsiderohet si nje regjistrim,
        ///// perdoret nje transaksion qe imponon fshirjen e rregullt te nje pasqyra financiare sebashku me trupin,llogarite dhe buxhetet e saj

        ///// </summary>
        /////<param name="pas"> pasqyra finaciare qe do te fshihet</param>
        ///// <returns> kthen nje obj clsMesazh per te identifikuar statusin e fshirjes se te dhenave ne DB</returns>
        //[Obsolete("Perdor nga klasa perkatese: clsMesazh fshiPasqyraAndBuxhete(int idPasqyresFin)", true)]
        //public clsMesazh fshiPasqyraAndBuxhete(clsPasqyreFinanciare pas)
        //{//fshin pasqyren dhe buxhetet perkatese
        //    //colTrupPasqyreFinaciare colTrupi = merrPasqyraFinaciareTrupiSipasIdKoka(pas.IdPasqyresFin);
        //    colTrupPasqyreFinaciare colTrupi = new colTrupPasqyreFinaciare(pas.IdPasqyresFin);
        //    colBuxhetet colBuxh = new colBuxhetet();
        //    colLlogariaTrupiPasqyres colLlog = new colLlogariaTrupiPasqyres();
        //    foreach (clsTrupPasqyreFinanciare t in colTrupi)
        //    {
        //        //colLlojeBuxhetesh colLloj = merrLlojBuxhetiSipasKodit("PasqyreFinanciare");
        //        //colBuxh.AddRange(merrBuxhetetSipasIdLidheseIdLlojBuxheti(t.IdTrupi, DbCore.DbKontabiliteti.clsLlojBuxheti.mbushIDLlojBuxheti("PasqyreFinanciare")));
        //        colBuxh.AddRange(new colBuxhetet(t.IdTrupi, DbCore.DbKontabiliteti.clsLlojBuxheti.mbushIDLlojBuxheti("PasqyreFinanciare")));
        //        //colLlog.AddRange(merrLlogariPasqyraFinaciareTrupiSipasIdTrupi(t.IdTrupi));
        //        colLlog.AddRange(new colLlogariaTrupiPasqyres(t.IdTrupi));
        //    } 
        //    dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        //    dbManager.ConnectionString = dbManager.GetConnectionString();
        //    dbManager.Open();
        //    dbManager.BeginTransaction();
        //    clsMesazh mesazh = new clsMesazh(true);
        //    try
        //    {



        //        foreach (clsBuxheti o in colBuxh)
        //        {
        //            if (mesazh.Status)
        //            {
        //                mesazh = fshiBuxhet(o.IdLidhese);
        //            }
        //            else
        //            {
        //                dbManager.Transaction.Rollback();
        //                    return mesazh;
        //            }
        //        }
        //        foreach (clsLlogariaTrupiPasqyres l in colLlog)
        //        {
        //            if (mesazh.Status)
        //            {
        //                mesazh = fshiLlogariPasqyreTrupi(l.IdTrupi);
        //            }
        //            else
        //            {
        //                dbManager.Transaction.Rollback();
        //                    return mesazh;
        //            }
        //        }
        //        foreach (clsTrupPasqyreFinanciare t in colTrupi)
        //        {
        //            if (mesazh.Status)
        //            {
        //                mesazh = fshiPasqyreTrupi(t.IdTrupi);
        //            }
        //            else
        //            {
        //                dbManager.Transaction.Rollback();
        //                return mesazh;
        //            }
        //        }
        //        if (mesazh.Status)
        //        {
        //            mesazh=  fshiPasqyreKoka(pas.IdPasqyresFin);
        //            if (mesazh.Status)
        //            {
        //                dbManager.CommitTransaction();
        //                mesazh = new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
        //                return mesazh;
        //            }
        //            else
        //            {
        //                dbManager.Transaction.Rollback();
        //                return mesazh;
        //            }
        //        }
        //        else
        //        {
        //            dbManager.Transaction.Rollback();
        //            return mesazh;
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        dbManager.Transaction.Rollback();
        //        return new clsMesazh(false, ce.Message);
        //    }
        //    finally
        //    {
        //        dbManager.Dispose();
        //    }
        //}

        #endregion

        /// <summary>
        /// Metodat e meposhtme i perkasin veprimeve qe kryhen nga objektet clsGrupKontabilizimi dhe colGrupeKontabilizimi
        /// </summary>
        #region GRUPET E KONTABILIZIMIT

        /// <summary>
        /// Ekzekuton prc_T_GRUPKONTABILIZIMI_ins per te ruajtur nje objekt clsGrupKontabilizimi ne DB.
        /// <param name="idgrupkontablizimi">Id e grupit te kontabilizimit</param>
        /// <param name="nrgrupkontabilizimi">Numri i grupit te kontabilizimit</param>
        /// <param name="pershkrimgrupkontabilizimi">Pershkrimi i grupit te kontabilizimit</param>
        /// <param name="idperdoruesi">Id e perdoruesit</param>
        /// <param name="idnderm">Id e ndermarrjes</param>
        /// <returns> Kthen statusin e perfundimit te ekzekutimit te SP-se perkatese (nje objekt clsMesazh qe tregon nese veprimi eshte kryer me sukses apo jo.<seealso cref="DbCore.clsMesazh"/>)</returns>
        /// </summary>
        internal clsMesazh ruajGrupKontabilizimi(out int idgrupkontablizimi, string nrgrupkontabilizimi, string pershkrimgrupkontabilizimi, int idperdoruesi, int idnderm, int idstatusdok)
        {//ruan nje grup kontabilizimi
            idgrupkontablizimi = -1;
            dbManager.Open();
            dbManager.CreateParameters(6);
            dbManager.AddParameters(0, "@IDGRUPKONTABILIZIMI", idgrupkontablizimi, ParameterDirection.Output);
            dbManager.AddParameters(1, "@NRGRUPKONTABILIZIMI", nrgrupkontabilizimi, ParameterDirection.Input);
            dbManager.AddParameters(2, "@PERSHKRIMGRUPKONTABILIZIMI", pershkrimgrupkontabilizimi, ParameterDirection.Input);
            dbManager.AddParameters(3, "@IDPERDORUESI", idperdoruesi, ParameterDirection.Input);
            dbManager.AddParameters(4, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            dbManager.AddParameters(5, "@IDSTATUSDOK", idstatusdok, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_GRUPKONTABILIZIMI_ins");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
            return mesazh;
        }
        //[Obsolete("Perdor: ruajGrupKontabilizimi(int idgrupkontablizimi, string nrgrupkontabilizimi, string pershkrimgrupkontabilizimi, int idperdoruesi, int idnderm)", true)]
        //public clsMesazh ruajGrupKontabilizimi(clsGrupKontabilizimi grupKontabilizimi)
        //{//ruan nje grup kontabilizimi
        //    IDBManager dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        //    dbManager.ConnectionString = dbManager.GetConnectionString();
        //    try
        //    {
        //        dbManager.Open();
        //        dbManager.CreateParameters(5);
        //        dbManager.AddParameters(0, "@IDGRUPKONTABILIZIMI", grupKontabilizimi.IdGrupKontabilizimi, ParameterDirection.Output);
        //        dbManager.AddParameters(1, "@NRGRUPKONTABILIZIMI", grupKontabilizimi.NrGrupKontabilizimi, ParameterDirection.Input);
        //        dbManager.AddParameters(2, "@PERSHKRIMGRUPKONTABILIZIMI", grupKontabilizimi.PershkrimGrupKontabilizimi, ParameterDirection.Input);
        //        dbManager.AddParameters(3, "@IDPERDORUESI", grupKontabilizimi.IdPerdoruesi , ParameterDirection.Input);
        //        dbManager.AddParameters(4, "@IDNDERMARJE", grupKontabilizimi.IdNdermarje, ParameterDirection.Input);
        //        dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_GRUPKONTABILIZIMI_ins");
        //        clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
        //        return mesazh;
        //    }
        //    catch (Exception)
        //    {
        //        return new clsMesazh(false, ce.Message);
        //    }
        //    finally
        //    {
        //        dbManager.Dispose();
        //    }

        //}

        /// <summary>
        /// Ekzekuton prc_T_GRUPKONTABILIZIMI_upd per te modifikuar nje objekt clsGrupKontabilizimi ne DB.
        /// <param name="idgrupkontablizimi">Id e grupit te kontabilizimit</param>
        /// <param name="nrgrupkontabilizimi">Numri i grupit te kontabilizimit</param>
        /// <param name="pershkrimgrupkontabilizimi">Pershkrimi i grupit te kontabilizimit</param>
        /// <param name="idperdoruesi">Id e perdoruesit</param>
        /// <param name="idnderm">Id e ndermarrjes</param>
        /// <returns> Kthen statusin e perfundimit te ekzekutimit te SP-se perkatese (nje objekt clsMesazh qe tregon nese veprimi eshte kryer me sukses apo jo.<seealso cref="DbCore.clsMesazh"/>)</returns>
        /// </summary>
        internal clsMesazh modifikoGrupKontabilizimi(int idgrupkontablizimi, string nrgrupkontabilizimi, string pershkrimgrupkontabilizimi, int idperdoruesi, int idnderm, int idstatusdok)
        {//modifikon nje grupkontabilizimi
            dbManager.Open();
            dbManager.CreateParameters(6);
            dbManager.AddParameters(0, "@IDGRUPKONTABILIZIMI", idgrupkontablizimi, ParameterDirection.Input);
            dbManager.AddParameters(1, "@NRGRUPKONTABILIZIMI", nrgrupkontabilizimi, ParameterDirection.Input);
            dbManager.AddParameters(2, "@PERSHKRIMGRUPKONTABILIZIMI", pershkrimgrupkontabilizimi, ParameterDirection.Input);
            dbManager.AddParameters(3, "@IDPERDORUESI", idperdoruesi, ParameterDirection.Input);
            dbManager.AddParameters(4, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            dbManager.AddParameters(5, "@IDSTATUSDOK", idstatusdok, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_GRUPKONTABILIZIMI_upd");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgModifikimiMeSukses"]);
            return mesazh;
        }
        //[Obsolete("Perdor: modifikoGrupKontabilizimi(int idgrupkontablizimi, string nrgrupkontabilizimi, string pershkrimgrupkontabilizimi, int idperdoruesi, int idnderm)", true)]
        //public clsMesazh modifikoGrupKontabilizimi(clsGrupKontabilizimi grupKontabilizimi)
        //{//modifikon nje grupkontabilizimi
        //    IDBManager dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        //    dbManager.ConnectionString = dbManager.GetConnectionString();
        //    try
        //    {
        //        dbManager.Open();
        //        dbManager.CreateParameters(5);
        //        dbManager.AddParameters(0, "@IDGRUPKONTABILIZIMI", grupKontabilizimi.IdGrupKontabilizimi, ParameterDirection.Input);
        //        dbManager.AddParameters(1, "@NRGRUPKONTABILIZIMI", grupKontabilizimi.NrGrupKontabilizimi, ParameterDirection.Input);
        //        dbManager.AddParameters(2, "@PERSHKRIMGRUPKONTABILIZIMI", grupKontabilizimi.PershkrimGrupKontabilizimi, ParameterDirection.Input);
        //        dbManager.AddParameters(3, "@IDPERDORUESI", grupKontabilizimi.IdPerdoruesi, ParameterDirection.Input);
        //        dbManager.AddParameters(4, "@IDNDERMARJE", grupKontabilizimi.IdNdermarje, ParameterDirection.Input);
        //        dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_GRUPKONTABILIZIMI_upd");
        //        clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgModifikimiMeSukses"]);
        //        return mesazh;
        //    }
        //    catch (Exception)
        //    {
        //        return new clsMesazh(false, ce.Message);
        //    }
        //    finally
        //    {
        //        dbManager.Dispose();
        //    }
        //}

        /// <summary>
        /// Ekzekuton prc_T_GRUPKONTABILIZIMI_del per te fshire nje objekt clsGrupKontabilizimi ne DB.
        /// <param name="idgrupkontablizimi">Id e grupit te kontabilizimit</param>
        /// <returns> Kthen statusin e perfundimit te ekzekutimit te SP-se perkatese (nje objekt clsMesazh qe tregon nese veprimi eshte kryer me sukses apo jo.<seealso cref="DbCore.clsMesazh"/>)</returns>
        /// </summary>
        internal clsMesazh fshiGrupKontabilizimi(int idgrupkontablizimi)
        {//fshin nje grup kontabilizimi
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDGRUPKONTABILIZIMI", idgrupkontablizimi, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_GRUPKONTABILIZIMI_del");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
            return mesazh;
        }
        internal clsMesazh fshiGrupKontabilizimiStatus(int idgrupkontablizimi, int idperdoruesi)
        {//fshin nje grup kontabilizimi
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDGRUPKONTABILIZIMI", idgrupkontablizimi, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUESI", idperdoruesi, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_GRUPKONTABILIZIMI_upddel");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
            return mesazh;
        }
        //[Obsolete("Perdor: fshiGrupKontabilizimi(int idgrupkontablizimi)", true)]
        //public clsMesazh fshiGrupKontabilizimi(clsGrupKontabilizimi grupKontabilizimi)
        //{//fshin nje grup kontabilizimi
        //    IDBManager dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        //    dbManager.ConnectionString = dbManager.GetConnectionString();
        //    try
        //    {
        //        dbManager.Open();
        //        dbManager.CreateParameters(1);
        //        dbManager.AddParameters(0, "@IDGRUPKONTABILIZIMI", grupKontabilizimi.IdGrupKontabilizimi, ParameterDirection.Input);
        //        dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_GRUPKONTABILIZIMI_del");
        //        clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
        //        return mesazh;
        //    }
        //    catch (Exception)
        //    {
        //        return new clsMesazh(false, ce.Message);
        //    }
        //    finally
        //    {
        //        dbManager.Dispose();
        //    }

        //}

        /// <summary>
        /// Ekzekuton prc_T_GRUPKONTABILIZIMI_ktheGrupKontabilizimiSipasId per te marre nje objekt clsGrupKontabilizimi ne DB duke filtruar sipas ID-se se grupit te kontabilizimit.
        /// <param name="idgrupkontablizimi">Id e grupit te kontabilizimit</param>
        /// </summary>
        internal void merrGrupKontabilizimi(int idgrupkontablizimi)
        {//merr nje grup kontabilizimi

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDGRUPKONTABILIZIMI", idgrupkontablizimi, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_GRUPKONTABILIZIMI_ktheGrupKontabilizimiSipasId");
        }
        //[Obsolete("Perdor: merrGrupKontabilizimi(int idgrupkontablizimi)", true)]
        //public void merrGrupKontabilizimi(clsGrupKontabilizimi grupKontabilizimi)
        //{//merr nje grup kontabilizimi
        //    IDBManager dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        //    dbManager.ConnectionString = dbManager.GetConnectionString();
        //    try
        //    {
        //        dbManager.Open();
        //        dbManager.CreateParameters(1);
        //        dbManager.AddParameters(0, "@IDGRUPKONTABILIZIMI", grupKontabilizimi.IdGrupKontabilizimi, ParameterDirection.Input);
        //        dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_GRUPKONTABILIZIMI_ktheGrupKontabilizimiSipasId");
        //    }
        //    catch (Exception)
        //    {
        //    }
        //    finally
        //    {
        //        dbManager.Dispose();
        //    }
        //}

        /// <summary>
        /// Ekzekuton prc_T_GRUPKONTABILIZIMI_merrGjitheGrupetKontabilizimi per te marre nje collection me objekte clsGrupKontabilizimi ne DB duke filtruar sipas ID-se se ndermarrjes.
        /// <param name="idndermarje">Id e ndermarrjes</param>
        /// <returns>Kthen nje collection me objekte clsGrupKontabilizimi</returns>
        /// </summary>
        internal DataTable ktheGjitheGrupetKontabilizimi(int idndermarje)
        {//metoda per te marre te gjithe grupet

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDNDERMARJE", idndermarje, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_GRUPKONTABILIZIMI_merrGjitheGrupetKontabilizimi");

            return ds.Tables[0];
        }
        //[Obsolete("Perdor: DataTable ktheGjitheGrupetKontabilizimi(int idndermarje)", true)]
        //public colGrupeKontabilizimi merrGjitheGrupetKontabilizimi(int idndermarje)
        //{//metoda per te marre te gjithe grupet
        //    IDBManager dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        //    dbManager.ConnectionString = dbManager.GetConnectionString();
        //    try
        //    {
        //        dbManager.Open();
        //        dbManager.CreateParameters(1);
        //        dbManager.AddParameters(0, "@IDNDERMARJE", idndermarje, ParameterDirection.Input);
        //        DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_GRUPKONTABILIZIMI_merrGjitheGrupetKontabilizimi");
        //        colGrupeKontabilizimi grupetKontabilizimi = new colGrupeKontabilizimi();
        //        return grupetKontabilizimi.mbushArrayListGrupeKontabilizimi(ds);
        //    }
        //    catch (Exception)
        //    {
        //        return new colGrupeKontabilizimi();
        //    }
        //    finally
        //    {
        //        dbManager.Dispose();

        //    }

        //}

        /// <summary>
        /// Ekzekuton prc_T_GRUPKONTABILIZIMI_merrGrupKontabilizimiSipasKodit per te marre nje collection me objekte clsGrupKontabilizimi ne DB duke filtruar sipas numrit te grupit te kontabilizimit dhe ID-se se ndermarrjes.
        /// <param name="kodi">Numri i kodit te kontabilizimit</param>
        /// <param name="idnderm">Id e ndermarrjes</param>
        /// <returns>Kthen nje collection me objekte clsGrupKontabilizimi</returns>
        /// </summary>
        internal int ktheGrupKontabilizimiSipasKodit(string kodi, int idnderm)
        {//metoda per te marre te grupin sipas kodit
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@NRGRUPKONTABILIZIMI", kodi, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_GRUPKONTABILIZIMI_merrGrupKontabilizimiSipasKodit");

            if (ds == null)
                return -1;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return -1;
            int idGrupKontabilizim;
            int.TryParse(ds.Tables[0].Rows[0]["IDGRUPKONTABILIZIMI"].ToString(), out idGrupKontabilizim);
            return idGrupKontabilizim;
        }
        //[Obsolete("Perdor: int ktheGrupKontabilizimiSipasKodit(string kodi, int idnderm)", true)]
        //public colGrupeKontabilizimi merrGrupKontabilizimiSipasKodit(string kodi, int idnderm)
        //{//metoda per te marre te grupin sipas kodit
        //    IDBManager dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        //    dbManager.ConnectionString = dbManager.GetConnectionString();
        //    try
        //    {
        //        dbManager.Open();
        //        dbManager.CreateParameters(2);
        //        dbManager.AddParameters(0, "@NRGRUPKONTABILIZIMI", kodi, ParameterDirection.Input);
        //        dbManager.AddParameters(1, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
        //        DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_GRUPKONTABILIZIMI_merrGrupKontabilizimiSipasKodit");
        //        colGrupeKontabilizimi grupetKontabilizimi = new colGrupeKontabilizimi();
        //        return grupetKontabilizimi.mbushArrayListGrupeKontabilizimi(ds);
        //    }
        //    catch (Exception)
        //    {
        //        return new colGrupeKontabilizimi();
        //    }
        //    finally
        //    {
        //        dbManager.Dispose();
        //    }

        //}

        /// <summary>
        /// Ekzekuton prc_T_GRUPKONTABILIZIMI_ktheGrupKontabilizimiSipasId per te marre nje collection me objekte clsGrupKontabilizimi ne DB duke filtruar sipas ID-se se grupit te kontabilizimit.
        /// <param name="id">Id e grupit te kontabilizimit</param>
        /// <returns>Kthen nje collection me objekte clsGrupKontabilizimi</returns>
        /// </summary>
        internal DataRow merrGrupKontabilizimiSipasId(int id)
        {//metoda per te marre grupin sipas id

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDGRUPKONTABILIZIMI", id, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_GRUPKONTABILIZIMI_ktheGrupKontabilizimiSipasId");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];
        }
        //[Obsolete("Perdor: DataRow merrGrupKontabilizimiSipasId(int id)", true)]
        //public colGrupeKontabilizimi ktheGrupKontabilizimiSipasId(int id)
        //{//metoda per te marre grupin sipas id
        //    IDBManager dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        //    dbManager.ConnectionString = dbManager.GetConnectionString();
        //    try
        //    {
        //        dbManager.Open();
        //        dbManager.CreateParameters(1);
        //        dbManager.AddParameters(0, "@IDGRUPKONTABILIZIMI", id, ParameterDirection.Input);
        //        DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_GRUPKONTABILIZIMI_ktheGrupKontabilizimiSipasId");
        //        colGrupeKontabilizimi grupetKontabilizimi = new colGrupeKontabilizimi();
        //        return grupetKontabilizimi.mbushArrayListGrupeKontabilizimi(ds);
        //    }
        //    catch (Exception)
        //    {
        //        return new colGrupeKontabilizimi();
        //    }
        //    finally
        //    {
        //        dbManager.Dispose();
        //    }

        //}

        /// <summary>
        /// Ekzekuton prc_T_GRUPKONTABILIZIMI_merrGrupKontabilizimiSipasKodit per te kontrolluar ne ekziston nje objekt clsGrupKontabilizimi ne DB duke filtruar sipas numrit te grupit te kontabilizimit dhe ID-se se ndermarrjes.
        /// <param name="nr">Numri i kodit te kontabilizimit</param>
        /// <param name="idnderm">Id e ndermarrjes</param>
        /// <returns>Kthen true nese ekziston nje objekt clsGrupKontabilizimi qe ploteson kushtet</returns>
        /// </summary>
        public bool ekzistonGrupKontabilizimi(string nr, int idnderm)
        {//kontrollon nese ekziston nje grup kontabilizimi me kete nr

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@NRGRUPKONTABILIZIMI", nr, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_GRUPKONTABILIZIMI_merrGrupKontabilizimiSipasKodit");
            if (ds.Tables[0].Rows.Count == 1)
                return true;
            else if (ds.Tables[0].Rows.Count == 0)
                return false;
            else return true;
        }

        /// <summary>
        /// Ekzekuton prc_T_GRUPKONTABILIZIMI_kaFleteKontabel per te kontrolluar ne ekziston nje flete kontabel me nje grup te caktuar kontabilizimi DB.
        ///<param name="id">Id e grupit te kontabilizimit</param>
        /// <returns>Kthen true nese ekziston nje flete kontabel me grupin e dhene te kontabilizimit</returns>
        /// </summary>
        public bool kaFleteKontabel(int id)
        {//kontrollon nese ky grup ka fletekontabel

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDGRUPKONTABILIZIMI", id, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_GRUPKONTABILIZIMI_kaFleteKontabel");
            if (ds.Tables[0].Rows.Count == 1)
                return true;
            else if (ds.Tables[0].Rows.Count == 0)
                return false;
            else return true;
        }

        #endregion

        /// <summary>
        /// Metodat e meposhtme i perkasin veprimeve qe kryhen nga objektet clsKokaFleteKontabel, colKokatFletetKontabel
        /// </summary>
        #region  KOKA FLETE KONTABEL

        //internal int ruajKokaFleteKontabel(int idkokafletekontabel, string nrdokumentikokafletekontabel, DateTime datedokumentifletekontabel, DateTime dateregjistrimifletekontabel,
        //    string pershkrimfletekontabel, int idgrupkontabilizimi,
        //    //int idskemakontabel, 
        //    int idnderviti, bool kont, double vleftafletekontabel, int idperdoruesi,
        //    int idllojdok, int iddoknga, int idstatusdok, int idkonfigambjente, int idkonfiggjenerues, int idkategoria, int idnivel, int idnivelgjenerues, int idgjenerues,int idPeriudha , int idndermarje)
        //{ //metoda per ruajtjen e  koken e fletes kontabel
        //    return ruajKokaFleteKontabel(idkokafletekontabel,nrdokumentikokafletekontabel,datedokumentifletekontabel,dateregjistrimifletekontabel,pershkrimfletekontabel
        //        ,idgrupkontabilizimi,
        //        //idskemakontabel,
        //        idnderviti,kont,vleftafletekontabel,idperdoruesi,idllojdok,iddoknga,idstatusdok,idkonfigambjente
        //        ,idkonfiggjenerues,idkategoria,idnivel,idnivelgjenerues,idgjenerues, idPeriudha, idndermarje);   
        //}
        /// <summary>
        /// Ekzekuton prc_T_KOKAFLETEKONTABEL_ins per te ruajtur nje objekt clsKokaFleteKontabel ne DB.
        /// <param name="idkokafletekontabel">Id qe gjenerohet automatikisht</param>
        /// <param name="nrdokumentikokafletekontabel">Numri i dokumentit</param>
        /// <param name="datedokumentifletekontabel">Data e dokumentit</param>
        /// <param name="dateregjistrimifletekontabel">Data e regjistrimit</param>
        /// <param name="pershkrimfletekontabel">Pershkrimi</param>
        /// <param name="idgrupkontabilizimi">Grupi i kontabilizimit <seealso cref="DbCore.DbKontabiliteti.clsGrupKontabilizimi"/>"/></param>
        /// <param name="idskemakontabel">Skema kontabel <seealso cref="DbCore.DbKontabiliteti.clsSkemaKontabelKoka"/></param>
        /// <param name="idnderviti">Id lidhese ndermarrje-vit</param>
        /// <param name="kont">Kontabilizuar apo jo</param>
        /// <param name="vleftafletekontabel">Vlefta e fletes kontabel</param>
        /// <param name="idperdoruesi">Id e perdoruesit</param>
        /// <param name="idllojdok">Lloji i dokumentit</param>
        /// <param name="iddoknga">Id e dokumentit nga eshte gjeneruar fleta kontabel ne raste modifikimi dhe fshirje</param>
        /// <param name="idstatusdok">Satusi i dokumentit - Ruajtur, Draft</param>
        /// <param name="idkonfigambjente">id e konfigurimit te ambjentit</param>
        /// <param name="idkategoria">kategoria e dokumentit nga eshte gjeneruar fleta kontabel</param>
        /// <param name="idkonfiggjenerues"> id e konfigurimit te dokumentit nga eshte gjeneruar fleta kontabel</param>
        /// <param name="idgjenerues">id e dokumentit nga eshte gjeneruar fleta kontabel nga nje ambjent tjeter</param>
        /// <param name="idnivel"> id e nivelit </param>
        /// <param name="idnivelgjenerues">id e nivelit te dokumentit nga eshte gjeneruar fleta kontabel</param>
        /// <returns> Kthen statusin e perfundimit te ekzekutimit te SP-se perkatese (true nese veprimi eshte kryer me sukses)</returns>
        /// </summary>

        internal int ruajKokaFleteKontabel(int idkokafletekontabel, string nrdokumentikokafletekontabel, DateTime datedokumentifletekontabel, DateTime dateregjistrimifletekontabel, string pershkrimfletekontabel, int idgrupkontabilizimi, /*int idskemakontabel, */int idnderviti, bool kont, double vleftafletekontabel, int idperdoruesi, int idllojdok, int iddoknga, int idstatusdok, int idkonfigambjente, int idkonfiggjenerues, int idkategoria, int idnivel, int idnivelgjenerues, int idgjenerues, int idPeriudha, int idndermarje)
        {
            //metoda per ruajtjen e  koken e fletes kontabel

            dbManager.Open();
            dbManager.CreateParameters(23);
            dbManager.AddParameters(0, "@IDKOKAFLETEKONTABEL", idkokafletekontabel, ParameterDirection.Output);
            dbManager.AddParameters(1, "@NRKOKAFLETEKONTABEL", nrdokumentikokafletekontabel, ParameterDirection.Input);
            dbManager.AddParameters(2, "@DATEDOKUMENTIKOKAFLETEKONTABEL", datedokumentifletekontabel, ParameterDirection.Input);
            dbManager.AddParameters(3, "@DATEREGJISTRIMIKOKAFLETEKONTABEL", dateregjistrimifletekontabel, ParameterDirection.Input);
            dbManager.AddParameters(4, "@NRREFERENCEKOKAFLETEKONTABEL", 0, ParameterDirection.Input);
            dbManager.AddParameters(5, "@PERSHKRIMKOKAFLETEKONTABEL", pershkrimfletekontabel, ParameterDirection.Input);
            if (idgrupkontabilizimi < 1) dbManager.AddParameters(6, "@IDGRUPKONTABILIZIMI", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(6, "@IDGRUPKONTABILIZIMI", idgrupkontabilizimi, ParameterDirection.Input);
            //if(idskemakontabel==0)        
            dbManager.AddParameters(7, "@IDSKEMAKONTABEL", DBNull.Value, ParameterDirection.Input);
            //else dbManager.AddParameters(7, "@IDSKEMAKONTABEL", idskemakontabel, ParameterDirection.Input);
            dbManager.AddParameters(8, "@IDNDERVITI", idnderviti, ParameterDirection.Input);
            dbManager.AddParameters(9, "@KONTABILIZUAR", kont, ParameterDirection.Input);
            dbManager.AddParameters(10, "@VLEFTAFLETEKONTABEL", vleftafletekontabel, ParameterDirection.Input);
            dbManager.AddParameters(11, "@IDPERDORUESI", idperdoruesi, ParameterDirection.Input);
            dbManager.AddParameters(12, "@IDLLOJDOK", idllojdok, ParameterDirection.Input);
            if (iddoknga == -1 || iddoknga == 0) dbManager.AddParameters(13, "@IDDOKNGA", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(13, "@IDDOKNGA", iddoknga, ParameterDirection.Input);
            dbManager.AddParameters(14, "@IDSTATUSDOK", idstatusdok, ParameterDirection.Input);
            dbManager.AddParameters(15, "@IDKONFIGAMBJENTI", idkonfigambjente, ParameterDirection.Input);
            if (idkonfiggjenerues == 0) dbManager.AddParameters(16, "@IDKONFIGGJENERUES", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(16, "@IDKONFIGGJENERUES", idkonfiggjenerues, ParameterDirection.Input);
            dbManager.AddParameters(17, "@IDKATEGORIA", idkategoria, ParameterDirection.Input);
            dbManager.AddParameters(18, "@IDNIVEL", idnivel, ParameterDirection.Input);
            if (idnivelgjenerues == 0) dbManager.AddParameters(19, "@IDNIVELGJENERUES", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(19, "@IDNIVELGJENERUES", idnivelgjenerues, ParameterDirection.Input);
            if (idgjenerues == 0) dbManager.AddParameters(20, "@IDGJENERUES", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(20, "@IDGJENERUES", idgjenerues, ParameterDirection.Input);
            dbManager.AddParameters(21, "@IDPERIUDHKONTABEL", idPeriudha, ParameterDirection.Input);
            dbManager.AddParameters(22, "@IDNDERMARJE", idndermarje, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_KOKAFLETEKONTABEL_ins");

            idkokafletekontabel = int.Parse(dbManager.Parameters[0].Value.ToString());
            return idkokafletekontabel;
        }

        /// <summary>
        /// Ekzekuton prc_T_KOKAFLETEKONTABEL_upd per te modifikuar nje objekt clsKokaFleteKontabel ne DB.
        /// <param name="idkokafletekontabel">Id qe gjenerohet automatikisht</param>
        /// <param name="nrdokumentikokafletekontabel">Numri i dokumentit</param>
        /// <param name="datedokumentifletekontabel">Data e dokumentit</param>
        /// <param name="dateregjistrimifletekontabel">Data e regjistrimit</param>
        /// <param name="nrreferencefletekontabel">Numri i references</param>
        /// <param name="pershkrimfletekontabel">Pershkrimi</param>
        /// <param name="idgrupkontabilizimi">Grupi i kontabilizimit <seealso cref="DbCore.DbKontabiliteti.clsGrupKontabilizimi"/></param>
        /// <param name="idskemakontabel">Skema kontabel <seealso cref="DbCore.DbKontabiliteti.clsSkemaKontabelKoka"/></param>
        /// <param name="idnderviti">Id lidhese ndermarrje-vit</param>
        /// <param name="kont">Kontabilizuar apo jo</param>
        /// <param name="vleftafletekontabel">Vlefta e fletes kontabel</param>
        /// <param name="idperdoruesi">Id e perdoruesit</param>
        /// <param name="idllojdok">Lloji i dokumentit</param>
        /// <param name="idstatusdok">Satusi i dokumentit - Ruajtur, Draft</param>
        /// <returns> Kthen statusin e perfundimit te ekzekutimit te SP-se perkatese (true nese veprimi eshte kryer me sukses)</returns>
        /// </summary>
        internal clsMesazh modifikoKokaFleteKontabelLidh(int idkokafletekontabel, string nrdokumentikokafletekontabel, DateTime datedokumentifletekontabel, DateTime dateregjistrimifletekontabel,
            string nrreferencefletekontabel, string pershkrimfletekontabel, int idgrupkontabilizimi, int idnderviti, bool kont, double vleftafletekontabel, int idperdoruesi, int idllojdok, int idstatusdok, int idndermarje)
        {//metoda per modifikimin e  kokes se fletes kontabel 

            dbManager.Open();
            //shtimi i parametrave
            dbManager.CreateParameters(15);
            dbManager.AddParameters(0, "@IDKOKAFLETEKONTABEL", idkokafletekontabel, ParameterDirection.Input);
            dbManager.AddParameters(1, "@NRKOKAFLETEKONTABEL", nrdokumentikokafletekontabel, ParameterDirection.Input);
            dbManager.AddParameters(2, "@DATEDOKUMENTIKOKAFLETEKONTABEL", datedokumentifletekontabel, ParameterDirection.Input);
            dbManager.AddParameters(3, "@DATEREGJISTRIMIKOKAFLETEKONTABEL", dateregjistrimifletekontabel, ParameterDirection.Input);
            dbManager.AddParameters(4, "@NRREFERENCEKOKAFLETEKONTABEL", nrreferencefletekontabel, ParameterDirection.Input);
            dbManager.AddParameters(5, "@PERSHKRIMKOKAFLETEKONTABEL", pershkrimfletekontabel, ParameterDirection.Input);
            if (idgrupkontabilizimi == 0)
                dbManager.AddParameters(6, "@IDGRUPKONTABILIZIMI", DBNull.Value, ParameterDirection.Input);
            else
                dbManager.AddParameters(6, "@IDGRUPKONTABILIZIMI", idgrupkontabilizimi, ParameterDirection.Input);
            dbManager.AddParameters(7, "@IDSKEMAKONTABEL", DBNull.Value, ParameterDirection.Input);
            dbManager.AddParameters(8, "@IDNDERVITI", idnderviti, ParameterDirection.Input);
            dbManager.AddParameters(9, "@KONTABILIZUAR", kont, ParameterDirection.Input);
            dbManager.AddParameters(10, "@VLEFTAFLETEKONTABEL", vleftafletekontabel, ParameterDirection.Input);
            dbManager.AddParameters(11, "@IDPERDORUESI", idperdoruesi, ParameterDirection.Input);
            dbManager.AddParameters(12, "@IDLLOJDOK", idllojdok, ParameterDirection.Input);
            dbManager.AddParameters(13, "@IDSTATUSDOK", idstatusdok, ParameterDirection.Input);
            dbManager.AddParameters(14, "@IDNDERMARJE", idndermarje, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_KOKAFLETEKONTABEL_updLidh");
            idkokafletekontabel = int.Parse(dbManager.Parameters[0].Value.ToString());
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgModifikimiMeSukses"]);
            return mesazh;
        }

        /// <summary>
        /// Ekzekuton prc_T_KOKAFLETEKONTABEL_del per te fshire nje objekt clsKokaFleteKontabel ne DB.
        /// <param name="idkokafletekontabel">Id qe gjenerohet automatikisht</param>
        /// <returns> Kthen statusin e perfundimit te ekzekutimit te SP-se perkatese (true nese veprimi eshte kryer me sukses)</returns>
        /// </summary>
        internal clsMesazh fshiKokaFleteKontabel(int idkokafletekontabel)
        {//metoda per fshirjen e kokes se fletes kontabel 

            dbManager.Open();
            //shtimi i parametrave
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDKOKAFLETEKONTABEL", idkokafletekontabel, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_KOKAFLETEKONTABEL_del");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
            return mesazh;
        }

        /// <summary>
        /// Ekzekuton prc_T_KOKAFLETEKONTABEL_merrKokaFleteKontabelSipasID per te marre nje collection me objekte clsKokaFleteKontabel ne DB duke filtruar sipas ID-se se kokes se fletes kontabel.
        /// <param name="idkokafletekontabel">id e kokes se fletes kontabel</param>
        /// </summary>
        internal void merrKokaFleteKontabel(int idkokafletekontabel)
        {// metoda per te marre nje  koka flete kontabel ne baze te id

            dbManager.Open();
            //shtimi i parametrave
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDKOKAFLETEKONTABEL", idkokafletekontabel, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_KOKAFLETEKONTABEL_merrKokaFleteKontabelSipasID");
        }

        /// <summary>
        /// Ekzekuton prc_T_KOKAFLETEKONTABEL_merrGjitheFletetKontabel per te marre nje datatable ne DB duke filtruar sipas ID-se lidhese ndermarrje-vit.
        /// <param name="idNdermVit">Id lidhese ndermarrje - vit</param>
        /// <returns> Kthen nje datatable qe plotesojne kushtin</returns>
        /// </summary>
        internal DataTable ktheGjitheFletetKontabelTePaKontabilizuara(int idNdermVit)
        {//metoda per te marre te gjithe  kokat e fleteve kontabel te pa kontabilizuara

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDNDERVITI", idNdermVit, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KOKAFLETEKONTABEL_merrGjitheFletetKontabel");
            return ds.Tables[0];
        }

        /// <summary>
        /// Ekzekuton prc_T_KOKAFLETEKONTABEL_merrKokaFleteKontabelSipasKodit per te marre nje datarow ne DB duke filtruar sipas kodit te kokes se fletes kontabel.
        /// <param name="kodi">Kodi e kokes se fletes kontabel</param>
        /// <returns> Kthen nje datarow qe ploteson kushtin</returns>
        /// </summary>
        internal DataRow ktheKokaFleteKontabelSipasKodit(String kodi, int idnderviti)
        {//metoda per te marre koka flete kontabel sipas kodit

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@NRKOKAFLETEKONTABEL", kodi, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERVITI", idnderviti, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KOKAFLETEKONTABEL_merrKokaFleteKontabelSipasKodit");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];
        }
        /// <summary>
        /// Ekzekuton prc_T_KOKAFLETEKONTABEL_merrKokaFleteKontabelSipasKodit per te marre nje datarow ne DB duke filtruar sipas kodit te kokes se fletes kontabel.
        /// <param name="kodi">Kodi e kokes se fletes kontabel</param>
        /// <returns> Kthen nje datarow qe ploteson kushtin</returns>
        /// </summary>
        internal DataRow ktheKokaFleteKontabelSipasLlojDok(int idllojdok, int idnderviti)
        {//metoda per te marre koka flete kontabel sipas kodit

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDLLOJDOK", idllojdok, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERVITI", idnderviti, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KOKAFLETEKONTABEL_merrKokaFleteKontabelSipasLlojDok");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];

        }

        /// <summary>
        /// Ekzekuton prc_T_KOKAFLETEKONTABEL_merrKokaFleteKontabelSipasID per te marre nje datarow ne DB duke filtruar sipas ID-se se kokes se fletes kontabel.
        /// <param name="id">Id e kokes se fletes kontabel</param>
        /// <returns> Kthen nje datarow qe ploteson kushtin</returns>
        /// </summary>
        internal DataRow ktheKokaFleteKontabelSipasID(int id)
        {//metoda per te marre koka flete kontabel sipas kodit

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDKOKAFLETEKONTABEL", id, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KOKAFLETEKONTABEL_merrKokaFleteKontabelSipasID");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];

        }

        /// <summary>
        /// Ekzekuton prc_T_KOKAFLETEKONTABEL_merrKokaFleteKontabelSipasIDDokAndIdLlojDok per te marre nje datatable ne DB duke filtruar sipas ID-se dhe llojit te dokumentit.
        /// <param name="iddok">Id e dokumentit</param>
        /// <param name="idlloj">Id e llojit te dokumentit</param>
        /// <returns> Kthen nje datatable qe plotesojne kushtet</returns>
        /// </summary>
        internal DataTable ktheDataTableKokaFleteKontabelSipasIDDokAndIDLlojDok(int iddok, int idlloj)
        {//metoda per te marre koka flete kontabel sipas kodit
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDGJENERUES", iddok, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDLLOJDOK", idlloj, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KOKAFLETEKONTABEL_merrKokaFleteKontabelSipasIDDokAndIdLlojDok");
            return ds.Tables[0];
        }

        internal DataTable ktheDataTableKokaFleteKontabelSipasIDGjeneruesAndIdKategoria(int iddok, int idkat)
        {//metoda per te marre koka flete kontabel sipas kodit
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDGJENERUES", iddok, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDKATEGORIA", idkat, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KOKAFLETEKONTABEL_merrKokaFleteKontabelSipasIDDokGjeneruesAndIdKategoria");
            return ds.Tables[0];
        }

        internal DataRow ktheKokaFleteKontabelSipasIDGjeneruesAndIdKategoria(int iddok, int idkat)
        {//metoda per te marre koka flete kontabel sipas kodit


            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDGJENERUES", iddok, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDKATEGORIA", idkat, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KOKAFLETEKONTABEL_merrKokaFleteKontabelSipasIDDokGjeneruesAndIdKategoria");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];

        }
        internal DataRow ktheKokaFleteKontabelSipasIDGjeneruesAndKonfigGjenerues(int iddok, int idkonfgjenerues)
        {//metoda per te marre koka flete kontabel sipas kodit


            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDGJENERUES", iddok, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDKonfigGjenerues", idkonfgjenerues, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KOKAFLETEKONTABEL_merrKokaFleteKontabelSipasIDDokGjeneruesAndKonfigGjenerues");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];

        }

        internal DataRow ktheKokaFleteKontabelSipasIDGjeneruesAndIdKategoriaDheData(int iddok, int idkat, DateTime data)
        {//metoda per te marre koka flete kontabel sipas kodit


            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@IDGJENERUES", iddok, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDKATEGORIA", idkat, ParameterDirection.Input);
            dbManager.AddParameters(2, "@data", data, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KOKAFLETEKONTABEL_merrKokaFleteKontabelSipasIDDokGjeneruesAndIdKategoriaDheData");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];

        }
        /// <summary>
        /// Ekzekuton prc_T_KOKAFLETEKONTABEL_merrKokaFleteKontabelSipasIDDokAndIdLlojDok per te marre nje datarow ne DB duke filtruar sipas ID-se dhe llojit te dokumentit.
        /// <param name="iddok">Id e dokumentit</param>
        /// <param name="idlloj">Id e llojit te dokumentit</param>
        /// <returns> Kthen nje datarow qe plotesojne kushtet</returns>
        /// </summary>
        internal DataRow ktheKokaFleteKontabelSipasIDDokAndIDLlojDok(int iddok, int idlloj)
        {//metoda per te marre koka flete kontabel sipas kodit

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDGJENERUES", iddok, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDLLOJDOK", idlloj, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KOKAFLETEKONTABEL_merrKokaFleteKontabelSipasIDDokAndIdLlojDok");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];
        }

        /// <summary>
        /// Ekzekuton prc_T_KOKAFLETEKONTABEL_ekzistonKokaFleteKontabel per te kontrolluar nese ekziston nje objekt clsKokaFleteKontabel ne DB duke filtruar sipas kodit, dates dhe id-se lidhese ndermarrje-vit.
        /// <param name="kodi">Kodi i kokes se fletes kontabel</param>
        /// <param name="data">Data e kokes se fletes kontabel</param>
        /// <param name="ndervit">Id lidhese ndermarrje - vit</param>
        /// <param name="LlojDok">Lloji i dokumentit</param>
        /// <returns> Kthen true nese ekziston nje objekt clsKokaFleteKontabel qe ploteson kushtet</returns>
        /// </summary>
        public bool ekzistonKokaFleteKontabel(String kodi, DateTime data, int ndervit, string LlojDok)
        {//kontrollon nese ekziston nje koka flete kontabel me kete nr dhe kete date
            string dt = data.Year + "-" + data.Month + "-" + data.Day;

            dbManager.Open();
            dbManager.CreateParameters(4);
            dbManager.AddParameters(0, "@NRKOKAFLETEKONTABEL", kodi, ParameterDirection.Input);
            dbManager.AddParameters(1, "@DATEDOKUMENTIKOKAFLETEKONTABEL", data, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDNDERVITI", ndervit, ParameterDirection.Input);
            dbManager.AddParameters(3, "@LLOJDOK", LlojDok, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KOKAFLETEKONTABEL_ekzistonKokaFleteKontabel");
            if (ds.Tables[0].Rows.Count == 1)
                return true;
            else if (ds.Tables[0].Rows.Count == 0)
                return false;
            else return true;

        }
        public clsMesazh UpdateStatusDheDateDokumentaKontabelDB(string idte, string dtRegjistrimiKontabiliteti, int idperdoruesi)
        {
            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@idFletaKontabel", idte, ParameterDirection.Input);
            dbManager.AddParameters(1, "@DATEREGJISTRIMIKOKAFLETEKONTABEL", dtRegjistrimiKontabiliteti, ParameterDirection.Input);
            dbManager.AddParameters(2, "@idperdoruesi", idperdoruesi, ParameterDirection.Input);
            dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KOKAFLETEKONTABEL_UpdateStatusDokumentaKontabel");
            clsMesazh mesazh = new clsMesazh(true, "Kontabilizimi i dokumentave te selektuar u be me sukses!");
            return mesazh;
        }


        /// <summary>
        /// perdoret per te marre gjithe dokumentat e fletes kontabel sipas idndermarjevitit per nje periudhe te caktuar 
        /// </summary>
        /// <param name="dataderi"> data e mbarimit te periudhes</param>
        /// <param name="datanga"> data e fillimit te periudhes</param>
        /// <param name="idndermarje"> id e ndermarje vitit</param>
        ///<returns>nje objekt colDokumentat qe permban nje koleksion me te gjitha dokumentat e fletes kontabel per nje periudhe te caktuar te nje ndermarje viti      </returns>
        public DataTable ktheGjitheDokumentatFleteKontabelStatusDraft(int idndermarje, string datanga, string dataderi, int idperdorues, string nrdok, string llojdok, string grupkontabilizimi, int idPerdoruesFilter)
        {
            dbManager.Open();
            dbManager.CreateParameters(8);
            dbManager.AddParameters(0, "@IDNDERmarje", idndermarje, ParameterDirection.Input);
            dbManager.AddParameters(1, "@DATANGA", datanga, ParameterDirection.Input);
            dbManager.AddParameters(2, "@DATADERI", dataderi, ParameterDirection.Input);
            dbManager.AddParameters(3, "@IDPERDORUES", idperdorues, ParameterDirection.Input);
            dbManager.AddParameters(4, "@NRDOKUMENTI", nrdok, ParameterDirection.Input);
            dbManager.AddParameters(5, "@LLOJDOKUMENTI", llojdok, ParameterDirection.Input);
            dbManager.AddParameters(6, "@GRUPKONTABILIZIMI", grupkontabilizimi, ParameterDirection.Input);
            dbManager.AddParameters(7, "@IDPERDORUESFILTER", idPerdoruesFilter, ParameterDirection.Input);
            return dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KOKAFLETEKONTABEL_merrGjitheDokumentatFleteKontabelStatusDraft").Tables[0];

        }

        /// <summary>
        /// Ekzekuton prc_T_KOKAFLETEKONTABEL_upd_GrupKontabilizimi per te mofifikuar grupin e kontabilizimit te nje objekti clsKokaFleteKontabel.
        /// <param name="idkokafletekontabel">id koka e fletes kontabel</param>
        /// <param name="idgrupkontabilizimi">id e grupit te kontabilizimit</param>
        ///<returns> Kthen statusin e perfundimit te ekzekutimit te SP-se perkatese(nje objekt clsMesazh qe tregon nese veprimi eshte kryer me sukses apo jo.<seealso cref="DbCore.clsMesazh"/>)</returns>
        /// </summary>
        internal clsMesazh modifikoKokaFleteKontabelGrupKontabilizimi(int idkokafletekontabel, int idgrupkontabilizimi)
        {//metoda per modifikimin e grupit te kokes se fletes kontabel 

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDKOKAFLETEKONTABEL", idkokafletekontabel, ParameterDirection.Input);
            if (idgrupkontabilizimi == 0) dbManager.AddParameters(1, "@IDGRUPKONTABILIZIMI", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(1, "@IDGRUPKONTABILIZIMI", idgrupkontabilizimi, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_KOKAFLETEKONTABEL_upd_GrupKontabilizimi");
            idkokafletekontabel = int.Parse(dbManager.Parameters[0].Value.ToString());
            return new clsMesazh(true, "Modifikimi u krye me sukses!");
        }

        /// <summary>
        /// Ekzekuton transaksion per te kontabilizuar nje objekt clsKokaFleteKontabel 
        /// Ekzekuton prc_T_KOKAFLETEKONTABEL_upd_Kontabilizo per te modifikuar Kontabilizuar ne DB.
        /// Me pas shton llogarite e kontabilitetit ne DB. Therret funksionin <see cref="DBKontabiliteti.clsDatabaseKontabilitet.ruajLlogariKontabiliteti"/>
        /// <param name="idkokafletekontabel">id e kokes se fletes kontabel</param>
        /// <param name="kont">i kontabilizuar apo jo</param>
        ///<returns> Kthen statusin e perfundimit te ekzekutimit te transaksionit(nje objekt clsMesazh qe tregon nese veprimi eshte kryer me sukses apo jo.<seealso cref="DbCore.clsMesazh"/>)</returns>
        /// </summary>
        public clsMesazh modKokaFleteKontabelKontabilizo(int idkokafletekontabel, bool kont)
        {//metoda per modifikimin e kontabilizimin te kokes se fletes kontabel 
            clsMesazh mesazh = new clsMesazh(true);
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDKOKAFLETEKONTABEL", idkokafletekontabel, ParameterDirection.Input);
            dbManager.AddParameters(1, "@KONTABILIZUAR", kont, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_KOKAFLETEKONTABEL_upd_Kontabilizo");
            idkokafletekontabel = int.Parse(dbManager.Parameters[0].Value.ToString());
            return new clsMesazh(true, "Modifikimi u krye me sukses");
        }

        /// <summary>
        /// Ekzekuton prc_T_KOKAFLETEKONTABEL_upd_NdryshoStatusin per te mofifikuar statusin e nje objekti clsKokaFleteKontabel.
        /// <param name="idkokafletekontabel">id e kokes se fletes kontabel</param>
        /// <param name="idstatusdok">statusi i dokumentit</param>
        ///<returns> Kthen statusin e perfundimit te ekzekutimit te SP-se perkatese(nje objekt clsMesazh qe tregon nese veprimi eshte kryer me sukses apo jo.<seealso cref="DbCore.clsMesazh"/>)</returns>
        /// </summary>
        public clsMesazh modifikoKokaFleteKontabelStatus(int idkokafletekontabel, int idstatusdok)
        {//metoda per modifikimin e statusit te kokes se fletes kontabel 

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDKOKAFLETEKONTABEL", idkokafletekontabel, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDSTATUSDOK", idstatusdok, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_KOKAFLETEKONTABEL_upd_NdryshoStatusin");
            idkokafletekontabel = int.Parse(dbManager.Parameters[0].Value.ToString());
            return new clsMesazh(true, "Fshirja u krye me sukses!");

        }

        /// <summary>
        /// perdoret per te gjeneruar ne menyre automatike nr e references sipas ndermarjevitit
        /// </summary>
        /// <param name="idndermviti"> id e ndermarje vitit</param>
        /// <returns></returns>
        internal decimal gjeneroNrReference(int idndermviti)
        {

            {
                dbManager.Open();
                dbManager.CreateParameters(1);
                dbManager.AddParameters(0, "@IDNDERVITI", idndermviti, ParameterDirection.Input);
                return (decimal)dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_KOKAFLETEKONTABEL_gjeneroNrReference");
            }

        }

        internal DataTable merrFleteKontabelDT(int idNdermVit, int idperdorues, string datanga, string dataderi)
        {//metoda per te marre te gjithe  kokat e fleteve kontabel te pa kontabilizuara

            dbManager.Open();
            dbManager.CreateParameters(4);
            dbManager.AddParameters(0, "@IDNDERVITI", idNdermVit, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUES", idperdorues, ParameterDirection.Input);
            dbManager.AddParameters(2, "@DATANGA", datanga, ParameterDirection.Input);
            dbManager.AddParameters(3, "@DATADERI", dataderi, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KOKAFLETEKONTABEL_merrFleteKontabelDT");
            return ds.Tables[0];
        }

        /// <summary>
        /// perdoret per te gjeneruar ne menyre automatike nr e references sipas ndermarjevitit
        /// </summary>
        /// <param name="idNdermarje"> id e ndermarjes</param>
        /// 
        /// <returns></returns>
        internal string merrNrFunditPerImportFleteKontAlbsig(int idNdermarje, string dt)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDNDERMARJE", idNdermarje, ParameterDirection.Input);
            dbManager.AddParameters(1, "@DATEDOK", dt, ParameterDirection.Input);
            return (string)dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_KOKAFLETEKONTABEL_ktheNrFunditImportiSipasDates");
        }


        internal clsMesazh kaloNeHistorikKokaFleteKontabel(int idkoka)
        {

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDKOKA", idkoka, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_KOKAFLETEKONTABEL_hidhNeHistorik");
            clsMesazh mesazh = new clsMesazh(true, mesazhFshirje);
            return mesazh;

        }

        internal DataTable MerrFleteKontabelExport(int idNdermarrje, int idPerdorues, int idNdermarrjeViti, int lloji, string emerTabKoka, string emerFusheId, string idPerEksport)
        {
            dbManager.Open();
            dbManager.CreateParameters(7);
            dbManager.AddInputParameters("@IDNDERMARRJE", idNdermarrje);
            dbManager.AddInputParameters("@IDPERDORUES", idPerdorues);
            dbManager.AddInputParameters("@IDNDERMARRJEVITI", idNdermarrjeViti);
            dbManager.AddInputParameters("@LLOJI", lloji);
            dbManager.AddInputParameters("@EMERTABKOKA", emerTabKoka);
            dbManager.AddInputParameters("@EMERFUSHEID", emerFusheId);
            dbManager.AddInputParameters("@IDPEREKSPORT", idPerEksport);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KOKAFLETEKONTABEL_MerrFleteKontabelExport");
            return ds.Tables[0];
        }

        #endregion

        /// <summary>
        /// Metodat e meposhtme i perkasin veprimeve qe kryhen nga objektet clsTrupiFleteKontabel, colTrupatFletetKontabel
        /// </summary>
        #region  TRUPI FLETE KONTABEL

        /// <summary>
        /// Ekzekuton prc_T_TRUPIFLETEKONTABEL_ins per te ruajtur nje objekt clsTrupiFleteKontabel ne DB.
        /// <param name="idtrupifletekontabel">id e trupit te fletes kontabel</param>
        /// <param name="idkokafletekontabel">id e kokes se fleteve kontabel</param>
        /// <param name="idllogari">id e llogarise</param>
        /// <param name="pershkrimtrupifletekontabel">pershkrimi i trupit te fleteve kontabel</param>
        /// <param name="idmonedha">id e monedhes</param>
        /// <param name="kurs">kursi me monedhen baze</param>
        /// <param name="vleftadebitrupifletekontabel">vlefta debi e trupit te fleteve kontabel</param>
        /// <param name="vleftakreditrupifletekontabel">vlefta kredi e trupit te fleteve kontabel</param>
        /// <param name="vleftadebimonbazetrupifletakontabel">vlefta debi ne monedhen baze</param>
        /// <param name="vleftakredimonbazetrupifletakontabel">vlefta kredi ne monedhen baze</param>
        /// <returns> Kthen statusin e perfundimit te ekzekutimit te SP-se perkatese (true nese veprimi eshte kryer me sukses)</returns>
        /// </summary>
        internal clsMesazh ruajTrupiFleteKontabel(out int idtrupifletekontabel, int idkokafletekontabel, int idllogari, string pershkrimtrupifletekontabel, int idmonedha, double kurs, double vleftadebitrupifletekontabel,
                double vleftakreditrupifletekontabel, double vleftadebimonbazetrupifletakontabel, double vleftakredimonbazetrupifletakontabel)
        { //metoda per ruajtjen e  trupit flete kontabel
            idtrupifletekontabel = 0;

            dbManager.Open();
            //shtimi i parametrave
            dbManager.CreateParameters(10);
            dbManager.AddParameters(0, "@IDTRUPIFLETEKONTABEL", idtrupifletekontabel, ParameterDirection.Output);
            dbManager.AddParameters(1, "@IDKOKAFLETEKONTABEL", idkokafletekontabel, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDLLOGARI", idllogari, ParameterDirection.Input);
            dbManager.AddParameters(3, "@PERSHKRIMiTRUPIFLETEKONTABEL", pershkrimtrupifletekontabel, ParameterDirection.Input);
            dbManager.AddParameters(4, "@IDMONEDHA", idmonedha, ParameterDirection.Input);
            dbManager.AddParameters(5, "@KURSI", kurs, ParameterDirection.Input);
            dbManager.AddParameters(6, "@VLEFTADEBITRUPIFLETEKONTABEL", vleftadebitrupifletekontabel, ParameterDirection.Input);
            dbManager.AddParameters(7, "@VLEFTAKREDITRUPIFLETEKONTABEL", vleftakreditrupifletekontabel, ParameterDirection.Input);
            dbManager.AddParameters(8, "@VLEFTADEBIMONBAZETRUPIFLETEKONTABEL", vleftadebimonbazetrupifletakontabel, ParameterDirection.Input);
            dbManager.AddParameters(9, "@VLEFTAKREDIMONBAZETRUPIFLETEKONTABEL", vleftakredimonbazetrupifletakontabel, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_TRUPIFLETEKONTABEL_ins");
            idtrupifletekontabel = int.Parse(dbManager.Parameters[0].Value.ToString());

            clsMesazh mesazh = new clsMesazh(true, "Shtimi perfundoi me sukses!");
            return mesazh;

        }
        public clsMesazh RuajTrupFleteKontabel(DataTable dtTrupi)
        {

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@DTTRUPI", dtTrupi, ParameterDirection.Input);
          

            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_TRUPIFLETEKONTABEL_insDT");
            return new clsMesazh(true, mesazhRuajtje);
        }
        //[Obsolete("Perdor: clsMesazh ruajTrupiFleteKontabel(int idtrupifletekontabel, int idkokafletekontabel, int idllogari, string pershkrimtrupifletekontabel, int idmonedha, double kurs, double vleftadebitrupifletekontabel, " +
        //    "double vleftakreditrupifletekontabel, string kodmonedha, string kodiskemakontabel, double vleftadebimonbazetrupifletakontabel, double vleftakredimonbazetrupifletakontabel)", true)]
        //public clsMesazh ruajTrupiFleteKontabel(clsTrupiFleteKontabel trupiFleteKontabel)
        //{ //metoda per ruajtjen e  trupit flete kontabel
        //    try
        //    {
        //        //shtimi i parametrave
        //        dbManager.CreateParameters(10);
        //        dbManager.AddParameters(0, "@IDTRUPIFLETEKONTABEL", trupiFleteKontabel.IdTrupiFleteKontabel, ParameterDirection.Output);
        //        dbManager.AddParameters(1, "@IDKOKAFLETEKONTABEL", trupiFleteKontabel.IdKokaFleteKontabel, ParameterDirection.Input);
        //        dbManager.AddParameters(2, "@IDLLOGARI", trupiFleteKontabel.IdLlogari, ParameterDirection.Input);
        //        dbManager.AddParameters(3, "@PERSHKRIMiTRUPIFLETEKONTABEL", trupiFleteKontabel.PershkrimTrupiFleteKontabel, ParameterDirection.Input);
        //        dbManager.AddParameters(4, "@IDMONEDHA", trupiFleteKontabel.IdMonedha, ParameterDirection.Input);
        //        dbManager.AddParameters(5, "@KURSI", trupiFleteKontabel.Kursi, ParameterDirection.Input);
        //        dbManager.AddParameters(6, "@VLEFTADEBITRUPIFLETEKONTABEL", trupiFleteKontabel.VleftaDebiTrupiFleteKontabel, ParameterDirection.Input);
        //        dbManager.AddParameters(7, "@VLEFTAKREDITRUPIFLETEKONTABEL", trupiFleteKontabel.VleftaKrediTrupiFleteKontabel, ParameterDirection.Input);
        //        dbManager.AddParameters(8, "@VLEFTADEBIMONBAZETRUPIFLETEKONTABEL", trupiFleteKontabel.VleftaDebiMonBazeTrupiFleteKontabel, ParameterDirection.Input);
        //        dbManager.AddParameters(9, "@VLEFTAKREDIMONBAZETRUPIFLETEKONTABEL", trupiFleteKontabel.VleftaKrediMonBazeTrupiFleteKontabel, ParameterDirection.Input);
        //        dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_TRUPIFLETEKONTABEL_ins");
        //        trupiFleteKontabel.IdTrupiFleteKontabel = int.Parse(dbManager.Parameters[0].Value.ToString());

        //        clsMesazh mesazh = new clsMesazh(true, "Shtimi perfundoi me sukses!");
        //        return mesazh;
        //    }
        //    catch (Exception)
        //    {
        //        
        //        return new clsMesazh(false, ce.Message);
        //    }
        //}

        /// <summary>
        /// Ekzekuton prc_T_TRUPIFLETEKONTABEL_upd per te modifikuar nje objekt clsTrupiFleteKontabel ne DB.
        /// <param name="idtrupifletekontabel">id e trupit te fletes kontabel</param>
        /// <param name="idkokafletekontabel">id e kokes se fleteve kontabel</param>
        /// <param name="idllogari">id e llogarise</param>
        /// <param name="pershkrimtrupifletekontabel">pershkrimi i trupit te fleteve kontabel</param>
        /// <param name="idmonedha">id e monedhes</param>
        /// <param name="kurs">kursi me monedhen baze</param>
        /// <param name="vleftadebitrupifletekontabel">vlefta debi e trupit te fleteve kontabel</param>
        /// <param name="vleftakreditrupifletekontabel">vlefta kredi e trupit te fleteve kontabel</param>
        /// <param name="vleftadebimonbazetrupifletakontabel">vlefta debi ne monedhen baze</param>
        /// <param name="vleftakredimonbazetrupifletakontabel">vlefta kredi ne monedhen baze</param>
        /// <returns> Kthen statusin e perfundimit te ekzekutimit te SP-se perkatese (true nese veprimi eshte kryer me sukses)</returns>
        /// </summary>
        internal clsMesazh modifikoTrupiFleteKontabel(int idtrupifletekontabel, int idkokafletekontabel, int idllogari, string pershkrimtrupifletekontabel, int idmonedha, double kurs, double vleftadebitrupifletekontabel,
            double vleftakreditrupifletekontabel, double vleftadebimonbazetrupifletakontabel, double vleftakredimonbazetrupifletakontabel)
        {//metoda per modifikimin e  trupi fleta kontabel

            dbManager.Open();
            dbManager.CreateParameters(10);
            dbManager.AddParameters(0, "@IDTRUPIFLETEKONTABEL", idtrupifletekontabel, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDKOKAFLETEKONTABEL", idkokafletekontabel, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDLLOGARI", idllogari, ParameterDirection.Input);
            dbManager.AddParameters(3, "@PERSHKRIMiTRUPIFLETEKONTABEL", pershkrimtrupifletekontabel, ParameterDirection.Input);
            dbManager.AddParameters(4, "@IDMONEDHA", idmonedha, ParameterDirection.Input);
            dbManager.AddParameters(5, "@KURSI", kurs, ParameterDirection.Input);
            dbManager.AddParameters(6, "@VLEFTADEBITRUPIFLETEKONTABEL", vleftadebitrupifletekontabel, ParameterDirection.Input);
            dbManager.AddParameters(7, "@VLEFTAKREDITRUPIFLETEKONTABEL", vleftakreditrupifletekontabel, ParameterDirection.Input);
            dbManager.AddParameters(8, "@VLEFTADEBIMONBAZETRUPIFLETEKONTABEL", vleftadebimonbazetrupifletakontabel, ParameterDirection.Input);
            dbManager.AddParameters(9, "@VLEFTAKREDIMONBAZETRUPIFLETEKONTABEL", vleftakredimonbazetrupifletakontabel, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_TRUPIFLETEKONTABEL_upd");
            idtrupifletekontabel = int.Parse(dbManager.Parameters[0].Value.ToString());

            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgModifikimiMeSukses"]);
            return mesazh;

        }
        //[Obsolete("Perdor: clsMesazh modifikoTrupiFleteKontabel(int idtrupifletekontabel, int idkokafletekontabel, int idllogari, string pershkrimtrupifletekontabel, int idmonedha, double kurs, double vleftadebitrupifletekontabel, " +
        //    "double vleftakreditrupifletekontabel, double vleftadebimonbazetrupifletakontabel, double vleftakredimonbazetrupifletakontabel)", true)]
        //public clsMesazh modifikoTrupiFleteKontabel(clsTrupiFleteKontabel trupiFleteKontabel)
        //{//metoda per modifikimin e  trupi fleta kontabel
        //    try
        //    {
        //        dbManager.CreateParameters(10);
        //        dbManager.AddParameters(0, "@IDTRUPIFLETEKONTABEL", trupiFleteKontabel.IdTrupiFleteKontabel, ParameterDirection.Input );
        //        dbManager.AddParameters(1, "@IDKOKAFLETEKONTABEL", trupiFleteKontabel.IdKokaFleteKontabel, ParameterDirection.Input);
        //        dbManager.AddParameters(2, "@IDLLOGARI", trupiFleteKontabel.IdLlogari, ParameterDirection.Input);
        //        dbManager.AddParameters(3, "@PERSHKRIMiTRUPIFLETEKONTABEL", trupiFleteKontabel.PershkrimTrupiFleteKontabel, ParameterDirection.Input);
        //        dbManager.AddParameters(4, "@IDMONEDHA", trupiFleteKontabel.IdMonedha, ParameterDirection.Input);
        //        dbManager.AddParameters(5, "@KURSI", trupiFleteKontabel.Kursi, ParameterDirection.Input);
        //        dbManager.AddParameters(6, "@VLEFTADEBITRUPIFLETEKONTABEL", trupiFleteKontabel.VleftaDebiTrupiFleteKontabel, ParameterDirection.Input);
        //        dbManager.AddParameters(7, "@VLEFTAKREDITRUPIFLETEKONTABEL", trupiFleteKontabel.VleftaKrediTrupiFleteKontabel, ParameterDirection.Input);
        //        dbManager.AddParameters(8, "@VLEFTADEBIMONBAZETRUPIFLETEKONTABEL", trupiFleteKontabel.VleftaDebiMonBazeTrupiFleteKontabel, ParameterDirection.Input);
        //        dbManager.AddParameters(9, "@VLEFTAKREDIMONBAZETRUPIFLETEKONTABEL", trupiFleteKontabel.VleftaKrediMonBazeTrupiFleteKontabel, ParameterDirection.Input);
        //        dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_TRUPIFLETEKONTABEL_upd");
        //        trupiFleteKontabel.IdTrupiFleteKontabel = int.Parse(dbManager.Parameters[0].Value.ToString());

        //        clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgModifikimiMeSukses"]);
        //        return mesazh;
        //    }
        //    catch (Exception)
        //    {

        //        return new clsMesazh(false, ce.Message);
        //    }

        //}

        /// <summary>
        /// Ekzekuton prc_T_TRUPIFLETEKONTABEL_del per te fshire nje objekt clsTrupiFleteKontabel ne DB.
        /// <param name="idtrupifletekontabel">id e trupit te fletes kontabel</param>
        /// <returns> Kthen statusin e perfundimit te ekzekutimit te SP-se perkatese (true nese veprimi eshte kryer me sukses)</returns>
        /// </summary>
        internal clsMesazh fshiTrupiFleteKontabel(int idtrupifletekontabel)
        {//metoda per fshirjen e trupi fleta kontabel

            dbManager.Open();
            //shtimi i parametrave
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDTRUPIFLETEKONTABEL", idtrupifletekontabel, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_TRUPIFLETEKONTABEL_del");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
            return mesazh;
        }
        //[Obsolete("Perdor: clsMesazh fshiTrupiFleteKontabel(int idtrupifletekontabel)", true)]
        //public clsMesazh fshiTrupiFleteKontabel(clsTrupiFleteKontabel trupiFleteKontabel)
        //{//metoda per fshirjen e trupi fleta kontabel
        //    try
        //    {
        //        //shtimi i parametrave
        //        dbManager.CreateParameters(1);
        //        dbManager.AddParameters(0, "@IDTRUPIFLETEKONTABEL", trupiFleteKontabel.IdTrupiFleteKontabel, ParameterDirection.Input);
        //        dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_TRUPIFLETEKONTABEL_del");
        //        clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
        //        return mesazh;
        //    }
        //    catch (Exception)
        //    {

        //        return new clsMesazh(false, ce.Message);
        //    }

        //}

        /// <summary>
        /// Ekzekuton prc_T_TRUPIFLETEKONTABEL_sel per te marre nje objekt clsTrupiFleteKontabel ne DB duke filtruar sipas ID-se se trupit te fletes kontabel.
        /// <param name="idtrupifletekontabel">id e trupit te fletes kontabel</param>
        /// </summary>
        internal void merrTrupiFleteKontabel(int idtrupifletekontabel)
        {// metoda per te marre nje  trupi flete kontabel NE BAZE TE ID

            dbManager.Open();
            //shtimi i parametrave
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDTRUPIFLETEKONTABEL", idtrupifletekontabel, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_TRUPIFLETEKONTABEL_sel");

        }
        //[Obsolete("Perdor: void merrTrupiFleteKontabel(int idtrupifletekontabel)", true)]
        //public void merrTrupiFleteKontabel(clsTrupiFleteKontabel trupiFleteKontabel)
        //{// metoda per te marre nje  trupi flete kontabel NE BAZE TE ID
        //    IDBManager dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        //    dbManager.ConnectionString = dbManager.GetConnectionString();
        //    try
        //    {
        //        dbManager.Open();
        //        //shtimi i parametrave
        //        dbManager.CreateParameters(1);
        //        dbManager.AddParameters(0, "@IDTRUPIFLETEKONTABEL", trupiFleteKontabel.IdTrupiFleteKontabel, ParameterDirection.Input);
        //        dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_TRUPIFLETEKONTABEL_sel");
        //    }
        //    catch (Exception)
        //    {
        //    }
        //    finally
        //    {
        //        dbManager.Dispose();
        //    }
        //}

        /// <summary>
        /// Ekzekuton prc_T_TRUPIFLETEKONTABEL_merrGjitheTrupatFletetKontabel per te marre nje collection me gjithe objektet clsTrupiFleteKontabel ne DB.
        /// <returns> Kthen nje collection me objekte clsTrupiFleteKontabel</returns>
        /// </summary>
        internal DataTable ktheGjitheTrupatFletetKontabel()
        {//metoda per te marre te gjithe trupat e fleteve kontabel

            dbManager.Open();
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_TRUPIFLETEKONTABEL_merrGjitheTrupatFletetKontabel");
            return ds.Tables[0];
        }
        //[Obsolete("Perdor: DataTable ktheGjitheTrupatFletetKontabel()", true)]
        //public colTrupatFletetKontabel merrGjitheTrupatFletetKontabel()
        //{//metoda per te marre te gjithe trupat e fleteve kontabel
        //    IDBManager dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        //    dbManager.ConnectionString = dbManager.GetConnectionString();
        //    try
        //    {
        //        dbManager.Open();
        //        DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_TRUPIFLETEKONTABEL_merrGjitheTrupatFletetKontabel");
        //        colTrupatFletetKontabel trupatfletekontabel = new colTrupatFletetKontabel();
        //        return trupatfletekontabel.mbushArrayListTrupiFleteveKontabel(ds);
        //    }
        //    catch (Exception)
        //    {
        //        return new colTrupatFletetKontabel();
        //    }
        //    finally
        //    {
        //        dbManager.Dispose();
        //    }
        //}

        /// <summary>
        /// Ekzekuton prc_T_TRUPIFLETEKONTABEL_merrTrupatFleteveKontabelSipasIdTrupit per te marre nje collection me objekte clsTrupiFleteKontabel ne DB duke filtruar sipas ID-se se trupit te fletes kontabel.
        ///<param name="id">ID e trupit te fletes kontabel</param> 
        /// <returns> Kthen nje collection me objekte clsTrupiFleteKontabel me ID-ne e trupit sa ID e kaluar si parameter</returns>
        /// </summary>
        internal DataRow ktheTrupatFleteveKontabelSipasIdTrupit(int id)
        {//metoda per te marre trupat e fleteve kontabel sipas id se trupit

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDTRUPIFLETEKONTABEL", id, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_TRUPIFLETEKONTABEL_merrTrupatFleteveKontabelSipasIdTrupit");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];
        }
        //[Obsolete("Perdor: DataRow ktheTrupatFleteveKontabelSipasIdTrupit(int id)", true)]
        //public colTrupatFletetKontabel merrTrupatFleteveKontabelSipasIdTrupit(int  id)
        //{//metoda per te marre trupat e fleteve kontabel sipas id se trupit
        //    IDBManager dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        //    dbManager.ConnectionString = dbManager.GetConnectionString();
        //    try
        //    {
        //        dbManager.Open();
        //        dbManager.CreateParameters(1);
        //        dbManager.AddParameters(0, "@IDTRUPIFLETEKONTABEL", id, ParameterDirection.Input);
        //        DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_TRUPIFLETEKONTABEL_merrTrupatFleteveKontabelSipasIdTrupit");
        //        colTrupatFletetKontabel trupatfletekontabel = new colTrupatFletetKontabel();
        //        return trupatfletekontabel.mbushArrayListTrupiFleteveKontabel(ds);
        //    }
        //    catch (Exception)
        //    {
        //        return new colTrupatFletetKontabel();
        //    }
        //    finally
        //    {
        //        dbManager.Dispose();
        //    }
        //}

        /// <summary>
        /// Ekzekuton prc_T_TRUPIFLETEKONTABEL_merrTrupatFleteKontabelSipasKokes per te marre nje collection me objekte clsTrupiFleteKontabel ne DB duke filtruar sipas ID-se se kokes se fletes kontabel.
        /// <param name="idKoka">ID e kokes te fletes kontabel</param> 
        /// <returns> Kthen nje collection me objekte clsTrupiFleteKontabel me ID-ne e kokes sa ID e kaluar si parameter</returns>
        /// </summary>
        internal DataTable ktheTrupatFleteKontabelSipasKokes(int idKoka)
        {//metoda per te marre trupin flete kontabel sipas id se kokes

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDKOKAFLETEKONTABEL", idKoka, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_TRUPIFLETEKONTABEL_merrTrupatFleteKontabelSipasKokes");
            return ds.Tables[0];
        }
        internal DataTable ktheTrupatFleteKontabelSipasKokesPerQK(int idKoka)
        {//metoda per te marre trupin flete kontabel sipas id se kokes


            dbManager.Open();

            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDKOKAFLETEKONTABEL", idKoka, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_TRUPIFLETEKONTABEL_merrTrupatFleteKontabelSipasKokesPerQK");
            return ds.Tables[0];

        }
        internal DataRow ktheTrupatFleteKontabelSipasKokesDheLlogarisePerQK(int idKoka, int idllogari)
        {//metoda per te marre trupin flete kontabel sipas id se kokes

            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDKOKAFLETEKONTABEL", idKoka, ParameterDirection.Input);
            dbManager.AddParameters(1, "@idllogari", idllogari, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_TRUPIFLETEKONTABEL_merrTrup1FleteKontabelSipasKokesDheLlogarisePerQK");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];
        }
        //[Obsolete("Perdor: DataTable ktheTrupatFleteKontabelSipasKokes(int idKoka)", true)]
        //public colTrupatFletetKontabel merrTrupatFleteKontabelSipasKokes(int idKoka)
        //{//metoda per te marre trupin flete kontabel sipas id se kokes
        //    bool connectionIRi = false;
        //    if (dbManager == null || dbManager.Command == null)
        //    {

        //        dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        //        dbManager.ConnectionString = dbManager.GetConnectionString();
        //        dbManager.Open();
        //        connectionIRi = true;
        //    }
        //    try
        //    {

        //        dbManager.CreateParameters(1);
        //        dbManager.AddParameters(0, "@IDKOKAFLETEKONTABEL", idKoka, ParameterDirection.Input);
        //        DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_TRUPIFLETEKONTABEL_merrTrupatFleteKontabelSipasKokes");
        //        colTrupatFletetKontabel trupatfletekontabel = new colTrupatFletetKontabel();
        //        return trupatfletekontabel.mbushArrayListTrupiFleteveKontabel(ds);
        //    }
        //    catch (Exception)
        //    {
        //        return new colTrupatFletetKontabel();
        //    }
        //    finally
        //    {if(connectionIRi )
        //        dbManager.Dispose();
        //    }
        //}

        #endregion



        /// <summary>
        /// Metodat e meposhtme i perkasin veprimeve qe kryhen nga objektet clsTipAdrese dhe colTipeAdresash
        /// </summary>
        #region TIPE ADRESASH

        /// <summary>
        /// Ekzekuton prc_T_TIPADRESE_ins per te ruajtur nje objekt clsTipAdrese ne DB.
        /// <param name="tipAdrese">Objekt i tipit clsTipAdrese qe do te ruhet ne DB</param>
        /// <returns> Kthen statusin e perfundimit te ekzekutimit te SP-se perkatese (nje objekt clsMesazh qe tregon nese veprimi eshte kryer me sukses apo jo.<seealso cref="DbCore.clsMesazh"/>)</returns>
        /// </summary>
        internal clsMesazh ruajTipAdrese(out int idtipadrese, String pershkrimtipadrese)
        { //metoda per ruajtjen e tipit TE adreses
            idtipadrese = -1;

            dbManager.Open();
            //shtimi i parametrave
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDTIPADRESE", idtipadrese, ParameterDirection.Output);
            dbManager.AddParameters(1, "@PERSHKRIMTIPADRESE", pershkrimtipadrese, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_TIPADRESE_ins");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
            return mesazh;
        }
       
        /// <summary>
        /// Ekzekuton prc_T_TIPADRESE_upd per te modifikuar nje objekt clsTipAdrese ne DB.
        /// <param name="tipAdrese">Objekt i tipit clsTipAdrese qe do te modifikohet</param>
        /// <returns> Kthen statusin e perfundimit te ekzekutimit te SP-se perkatese (nje objekt clsMesazh qe tregon nese veprimi eshte kryer me sukses apo jo.<seealso cref="DbCore.clsMesazh"/>)</returns>
        /// </summary>
        internal clsMesazh modifikoTipAdrese(int idtipadrese, String pershkrimtipadrese)
        {//metoda per modifikimin e TIPIT TE ADRESES

            dbManager.Open();
            //shtimi i parametrave
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDTIPADRESE", idtipadrese, ParameterDirection.Input);
            dbManager.AddParameters(1, "@PERSHKRIMTIPADRESE", pershkrimtipadrese, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_TIPADRESE_upd");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgModifikimiMeSukses"]);
            return mesazh;

        }
        /// <summary>
        /// Ekzekuton prc_T_TIPADRESE_del per te fshire nje objekt clsTipAdrese ne DB.
        /// <param name="tipAdrese">Objekt i tipit clsTipAdrese qe do te fshihet</param>
        /// <returns> Kthen statusin e perfundimit te ekzekutimit te SP-se perkatese (nje objekt clsMesazh qe tregon nese veprimi eshte kryer me sukses apo jo.<seealso cref="DbCore.clsMesazh"/>)</returns>
        /// </summary>
        internal clsMesazh fshiTipAdrese(int idtipadrese)
        {//metoda per fshirjen e tipAdrese

            dbManager.Open();
            //shtimi i parametrave
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDTIPADRESE", idtipadrese, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_TIPADRESE_del");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
            return mesazh;
        }
      

        /// <summary>
        /// Ekzekuton prc_T_TIPADRESE_sel per te marre nje objekt clsTipAdrese ne DB.
        /// <param name="tipAdrese">Objekt i tipit clsTipAdrese qe do te ruhet ne DB</param>
        /// </summary>
        internal void merrTipAdrese(int idtipadrese)
        {// metoda per te marre nje tipAdrese NE BAZE TE ID

            dbManager.Open();
            //shtimi i parametrave
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDTIPADRESE", idtipadrese, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_TIPADRESE_sel");

        }
        /// <summary>
        /// Ekzekuton prc_T_TIPADRESE_merrGjitheTipetAdresave per te marre nje collection me objekte clsTipAdrese ne DB.
        /// <returns> Kthen nje collection me objekte clsTipAdrese</returns>
        /// </summary>
        internal DataTable ktheGjitheTipetAdresave()
        {//metoda per te marre te gjithe tipet e adresave

            dbManager.Open();
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_TIPADRESE_merrGjitheTipetAdresave");
            return ds.Tables[0];
        }


        /// <summary>
        /// Ekzekuton prc_T_TIPADRESE_merrTipAdreseSipasPershkrimit per te marre nje collection me objekte clsTipAdrese ne DB duke filtruar sipas "Pershkrimit te tipit te adreses".
        /// <param name="pershkrimi">Pershkrimi i tipit te adreses</param>
        /// <returns> Kthen nje collection me objekte clsTipAdrese</returns>
        /// </summary>
        internal DataTable ktheTipAdreseSipasPershkrimit(String pershkrimi)
        {//metoda per te marre te tip adrese sipas pershkrimit

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@PERSHKRIMTIPADRESE", pershkrimi, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_TIPADRESE_merrTipAdreseSipasPershkrimit");
            return ds.Tables[0];
        }
    

        #endregion

        /// <summary>
        /// Metodat e meposhtme i perkasin veprimeve qe kryhen nga objektet clsAdresaKlientFurnitor dhe colAdresatKlientFurnitor
        /// </summary>
        #region ADRESA KLIENT FURNITOR

        /// <summary>
        /// Ekzekuton prc_T_ADRESAKLIENTFURNITOR_ins per te ruajtur nje objekt clsAdresaKlientFurnitor ne DB.
        /// <param name="idadresaklientfurnitor">Id e adreses</param>
        /// <param name="idklientfurnitor">Id e klientit/furnitorit</param>
        /// <param name="idtipadrese">Id e tipit te adreses. Merret nga tabela: T_TIPADRESE</param>
        /// <param name="adr"></param>
        /// <returns> Kthen statusin e perfundimit te ekzekutimit te SP-se perkatese (nje objekt clsMesazh qe tregon nese veprimi eshte kryer me sukses apo jo.<seealso cref="DbCore.clsMesazh"/>)</returns>
        /// </summary>
        internal clsMesazh ruajAdrese(out int idadresaklientfurnitor, int idklientfurnitor, int idtipadrese, String adr, String kodiPost)
        {//ruajtja e adreses se klient furnitorit
            idadresaklientfurnitor = -1;

            dbManager.Open();
            dbManager.CreateParameters(5);
            dbManager.AddParameters(0, "@IDADRESAKLIENTFURNITOR", idadresaklientfurnitor, ParameterDirection.Output);
            dbManager.AddParameters(1, "@IDKLIENTFURNITOR", idklientfurnitor, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDTIPADRESE", idtipadrese, ParameterDirection.Input);
            dbManager.AddParameters(3, "@ADRESA", adr, ParameterDirection.Input);
            dbManager.AddParameters(4, "@KODIPOSTAR", kodiPost, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_ADRESAKLIENTFURNITOR_ins");
            return new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
        }
        [Obsolete("Perdor: clsMesazh ruajAdrese(int idadresaklientfurnitor, int idklientfurnitor, int idtipadrese, String adr)", true)]
        public clsMesazh ruajAdrese(clsAdresaKlientFurnitor adrese)
        {//ruajtja e adreses se klient furnitorit

            dbManager.CreateParameters(4);
            dbManager.AddParameters(0, "@IDADRESAKLIENTFURNITOR", adrese.IdAdresaKlientFurnitor, ParameterDirection.Output);
            dbManager.AddParameters(1, "@IDKLIENTFURNITOR", adrese.IdKlientFurnitor, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDTIPADRESE", adrese.IdTipAdrese, ParameterDirection.Input);
            dbManager.AddParameters(3, "@ADRESA", adrese.Adresa, ParameterDirection.Input);

            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_ADRESAKLIENTFURNITOR_ins");
            return new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
        }

        /// <summary>
        /// Ekzekuton prc_T_ADRESAKLIENTFURNITOR_upd per te modifikuar nje objekt clsAdresaKlientFurnitor ne DB.
        /// <param name="idadresaklientfurnitor">Id e adreses</param>
        /// <param name="idklientfurnitor">Id e klientit/furnitorit</param>
        /// <param name="idtipadrese">Id e tipit te adreses. Merret nga tabela: T_TIPADRESE</param>
        /// <param name="adr"></param>
        /// <returns> Kthen statusin e perfundimit te ekzekutimit te SP-se perkatese (nje objekt clsMesazh qe tregon nese veprimi eshte kryer me sukses apo jo.<seealso cref="DbCore.clsMesazh"/>)</returns>
        /// </summary>
        internal clsMesazh modifikoAdrese(int idadresaklientfurnitor, int idklientfurnitor, int idtipadrese, String adr, String kodiPost)
        {//modifikimi i adreses se klient furnitorit

            dbManager.Open();
            dbManager.CreateParameters(5);
            dbManager.AddParameters(0, "@IDADRESAKLIENTFURNITOR", idadresaklientfurnitor, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDKLIENTFURNITOR", idklientfurnitor, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDTIPADRESE", idtipadrese, ParameterDirection.Input);
            dbManager.AddParameters(3, "@ADRESA", adr, ParameterDirection.Input);
            dbManager.AddParameters(4, "@KODIPOSTAR", kodiPost, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_ADRESAKLIENTFURNITOR_upd");
            return new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
        }
        //[Obsolete("Perdor: clsMesazh modifikoAdrese(int idadresaklientfurnitor, int idklientfurnitor, int idtipadrese, String adr)", true)]
        //public clsMesazh modifikoAdrese(clsAdresaKlientFurnitor adrese)
        //{//modifikimi i adreses se klient furnitorit
        //    try
        //    {

        //        dbManager.CreateParameters(4);
        //        dbManager.AddParameters(0, "@IDADRESAKLIENTFURNITOR", adrese.IdAdresaKlientFurnitor, ParameterDirection.Input);
        //        dbManager.AddParameters(1, "@IDKLIENTFURNITOR", adrese.IdKlientFurnitor, ParameterDirection.Input);
        //        dbManager.AddParameters(2, "@IDTIPADRESE", adrese.IdTipAdrese, ParameterDirection.Input);
        //        dbManager.AddParameters(3, "@ADRESA", adrese.Adresa, ParameterDirection.Input);

        //        dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_ADRESAKLIENTFURNITOR_upd");
        //        return new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
        //    }
        //    catch (Exception)
        //    {
        //        return new clsMesazh(false, "Ndodhi nje gabim. Ruajtja nuk u krye!");
        //    }
        //}

        /// <summary>
        /// Ekzekuton prc_T_ADRESAKLIENTFURNITOR_del per te fshire nje objekt clsAdresaKlientFurnitor ne DB.
        /// <param name="idadresaklientfurnitor">Id e adreses</param>
        /// <returns> Kthen statusin e perfundimit te ekzekutimit te SP-se perkatese (nje objekt clsMesazh qe tregon nese veprimi eshte kryer me sukses apo jo.<seealso cref="DbCore.clsMesazh"/>)</returns>
        /// </summary>
        internal clsMesazh fshiAdrese(int idadresaklientfurnitor)
        {//fshirja e adreses se klient furnitorit

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDADRESAKLIENTFURNITOR", idadresaklientfurnitor, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_ADRESAKLIENTFURNITOR_del");
            return new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
        }
        //[Obsolete("Perdor: clsMesazh fshiAdrese(int idadresaklientfurnitor)", true)]
        //public clsMesazh fshiAdrese(clsAdresaKlientFurnitor adrese)
        //{//fshirja e adreses se klient furnitorit
        //    try
        //    {
        //        dbManager.CreateParameters(1);
        //        dbManager.AddParameters(0, "@IDADRESAKLIENTFURNITOR", adrese.IdAdresaKlientFurnitor, ParameterDirection.Input);
        //        dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_ADRESAKLIENTFURNITOR_del");
        //        return new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
        //    }
        //    catch (Exception)
        //    {
        //        return new clsMesazh(false, "Fshirja perfundoi me gabime!");
        //    }
        //}

        /// <summary>
        /// Ekzekuton prc_T_ADRESAKLIENTFURNITOR_sel per te marre nje objekt clsAdresaKlientFurnitor ne DB.
        /// <param name="idadresaklientfurnitor">Id e adreses</param>
        /// </summary>
        internal void merrAdrese(int idadresaklientfurnitor)
        {// metoda per te marre nje adresen ne base te id

            dbManager.Open();
            //shtimi i parametrave
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDADRESAKLIENTFURNITOR", idadresaklientfurnitor, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_ADRESAKLIENTFURNITOR_sel");

        }
   

        /// <summary>
        /// Ekzekuton prc_T_ADRESAKLIENTFURNITOR_merrGjitheAdresat per te marre nje datatable ne DB.
        /// <returns> Kthen nje datatable</returns>
        /// </summary>
        internal DataTable ktheGjitheAdresat()
        {//metoda per te marre te gjithe  adresat

            dbManager.Open();
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_ADRESAKLIENTFURNITOR_merrGjitheAdresat");
            return ds.Tables[0];
        }
  
        /// <summary>
        /// Ekzekuton prc_T_ADRESAKLIENTFURNITOR_merrAdreseSipasIdKlientFurnitor per te marre nje datatable ne DB duke filtruar sipas ID-se se klientit/furnitorit.
        /// <param name="id">ID e klientit/furnitorit</param>
        /// <returns> Kthen nje datatable</returns>
        /// </summary> 
        internal DataTable ktheAdreseSipasIdKlientFurnitor(int id)
        {//metoda per te marre te gjithe adresat sipas idklientfurnitor

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDKLIENTFURNITOR", id, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_ADRESAKLIENTFURNITOR_merrAdreseSipasIdKlientFurnitor");
            return ds.Tables[0];
        }

        #endregion

        /// <summary>
        /// Metodat e meposhtme i perkasin veprimeve qe kryhen nga objektet clsKontaktiKlientFurnitor dhe colKontaktiKlientFurnitor
        /// </summary>
        #region KONTAKTI KLIENT FURNITOR

        /// <summary>
        /// Ekzekuton prc_T_KONTAKTIKLIENTFURNITOR_ins per te ruajtur nje objekt clsKontaktiKlientFurnitor ne DB.
        /// <param name="kontakt">Objekt i tipit clsKontaktiKlientFurnitor qe do te ruhet ne DB</param>
        /// <returns> Kthen statusin e perfundimit te ekzekutimit te SP-se perkatese (nje objekt clsMesazh qe tregon nese veprimi eshte kryer me sukses apo jo.<seealso cref="DbCore.clsMesazh"/>)</returns>
        /// </summary>
        internal clsMesazh ruajKontakt(out int idkontaktiklientfurnitor, int idklientfurnitor, string emerkontakti, String mbiemerkontakti, string telkontakti, string faxkontakti, string celkontakti, string emailkontakti)
        {//ruajtja e kontaktit per klient furnitorin 
            idkontaktiklientfurnitor = -1;

            dbManager.Open();
            dbManager.CreateParameters(8);
            dbManager.AddParameters(0, "@IDKONTAKTIKLIENTFURNITOR", idkontaktiklientfurnitor, ParameterDirection.Output);
            dbManager.AddParameters(1, "@IDKLIENTFURNITOR", idklientfurnitor, ParameterDirection.Input);
            dbManager.AddParameters(2, "@EMERKONTAKTI", emerkontakti, ParameterDirection.Input);
            dbManager.AddParameters(3, "@MBIEMERKONTAKTI", mbiemerkontakti, ParameterDirection.Input);
            dbManager.AddParameters(4, "@TELKONTAKTI", telkontakti, ParameterDirection.Input);
            dbManager.AddParameters(5, "@FAXKONTAKTI", faxkontakti, ParameterDirection.Input);
            dbManager.AddParameters(6, "@CELKONTAKTI", celkontakti, ParameterDirection.Input);
            dbManager.AddParameters(7, "@EMAILKONTAKTI", emailkontakti, ParameterDirection.Input);

            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_KONTAKTIKLIENTFURNITOR_ins");
            return new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
        }
        //[Obsolete("Perdor: clsMesazh ruajKontakt(int idkontaktiklientfurnitor, int idklientfurnitor, string emerkontakti, String mbiemerkontakti, string telkontakti, string faxkontakti, string celkontakti, string emailkontakti)", true)]
        //public clsMesazh ruajKontakt(clsKontaktiKlientFurnitor kontakt)
        //{//ruajtja e kontaktit per klient furnitorin 
        //    try
        //    {
        //        dbManager.CreateParameters(8);
        //        dbManager.AddParameters(0, "@IDKONTAKTIKLIENTFURNITOR", kontakt.IdKontaktiKlientFurnitor, ParameterDirection.Output);
        //        dbManager.AddParameters(1, "@IDKLIENTFURNITOR", kontakt.IdKlientFurnitor, ParameterDirection.Input);
        //        dbManager.AddParameters(2, "@EMERKONTAKTI", kontakt.EmerKontakti, ParameterDirection.Input);
        //        dbManager.AddParameters(3, "@MBIEMERKONTAKTI", kontakt.MbiemerKontakti, ParameterDirection.Input);
        //        dbManager.AddParameters(4, "@TELKONTAKTI", kontakt.TelKontakti, ParameterDirection.Input);
        //        dbManager.AddParameters(5, "@FAXKONTAKTI", kontakt.FaxKontakti, ParameterDirection.Input);
        //        dbManager.AddParameters(6, "@CELKONTAKTI", kontakt.CelKontakti, ParameterDirection.Input);
        //        dbManager.AddParameters(7, "@EMAILKONTAKTI", kontakt.EmailKontakti, ParameterDirection.Input);

        //        dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_KONTAKTIKLIENTFURNITOR_ins");
        //        return new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
        //    }
        //    catch (Exception)
        //    {
        //        return new clsMesazh(false, "Ndodhi nje gabim. Ruajtja nuk u krye!");
        //    }
        //}

        /// <summary>
        /// Ekzekuton prc_T_KONTAKTIKLIENTFURNITOR_upd per te modifikuar nje objekt clsKontaktiKlientFurnitor ne DB.
        /// <param name="kontakt">Objekt i tipit clsKontaktiKlientFurnitor qe do te modifikohet</param>
        /// <returns> Kthen statusin e perfundimit te ekzekutimit te SP-se perkatese (nje objekt clsMesazh qe tregon nese veprimi eshte kryer me sukses apo jo.<seealso cref="DbCore.clsMesazh"/>)</returns>
        /// </summary>
        internal clsMesazh modifikoKontakt(int idkontaktiklientfurnitor, int idklientfurnitor, string emerkontakti, String mbiemerkontakti, string telkontakti, string faxkontakti, string celkontakti, string emailkontakti)
        {//modifikimi e kontaktit per klient furnitorin

            dbManager.Open();

            dbManager.CreateParameters(8);
            dbManager.AddParameters(0, "@IDKONTAKTIKLIENTFURNITOR", idkontaktiklientfurnitor, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDKLIENTFURNITOR", idklientfurnitor, ParameterDirection.Input);
            dbManager.AddParameters(2, "@EMERKONTAKTI", emerkontakti, ParameterDirection.Input);
            dbManager.AddParameters(3, "@MBIEMERKONTAKTI", mbiemerkontakti, ParameterDirection.Input);
            dbManager.AddParameters(4, "@TELKONTAKTI", telkontakti, ParameterDirection.Input);
            dbManager.AddParameters(5, "@FAXKONTAKTI", faxkontakti, ParameterDirection.Input);
            dbManager.AddParameters(6, "@CELKONTAKTI", celkontakti, ParameterDirection.Input);
            dbManager.AddParameters(7, "@EMAILKONTAKTI", emailkontakti, ParameterDirection.Input);

            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_KONTAKTIKLIENTFURNITOR_upd");
            return new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);

        }
        //[Obsolete("Perdor: clsMesazh modifikoKontakt(int idkontaktiklientfurnitor, int idklientfurnitor, string emerkontakti, String mbiemerkontakti, string telkontakti, string faxkontakti, string celkontakti, string emailkontakti)", true)]
        //public clsMesazh modifikoKontakt(clsKontaktiKlientFurnitor kontakt)
        //{//modifikimi e kontaktit per klient furnitorin
        //    try
        //    {

        //        dbManager.CreateParameters(8);
        //        dbManager.AddParameters(0, "@IDKONTAKTIKLIENTFURNITOR", kontakt.IdKontaktiKlientFurnitor, ParameterDirection.Input);
        //        dbManager.AddParameters(1, "@IDKLIENTFURNITOR", kontakt.IdKlientFurnitor, ParameterDirection.Input);
        //        dbManager.AddParameters(2, "@EMERKONTAKTI", kontakt.EmerKontakti, ParameterDirection.Input);
        //        dbManager.AddParameters(3, "@MBIEMERKONTAKTI", kontakt.MbiemerKontakti, ParameterDirection.Input);
        //        dbManager.AddParameters(4, "@TELKONTAKTI", kontakt.TelKontakti, ParameterDirection.Input);
        //        dbManager.AddParameters(5, "@FAXKONTAKTI", kontakt.FaxKontakti, ParameterDirection.Input);
        //        dbManager.AddParameters(6, "@CELKONTAKTI", kontakt.CelKontakti, ParameterDirection.Input);
        //        dbManager.AddParameters(7, "@EMAILKONTAKTI", kontakt.EmailKontakti, ParameterDirection.Input);

        //        dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_KONTAKTIKLIENTFURNITOR_upd");
        //        return new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
        //    }
        //    catch (Exception)
        //    {
        //        return new clsMesazh(false, "Ndodhi nje gabim. Ruajtja nuk u krye!");
        //    }
        //}

        /// <summary>
        /// Ekzekuton prc_T_KONTAKTIKLIENTFURNITOR_del per te fshire nje objekt clsKontaktiKlientFurnitor ne DB.
        /// <param name="kontakt">Objekt i tipit clsKontaktiKlientFurnitor qe do te fshihet</param>
        /// <returns> Kthen statusin e perfundimit te ekzekutimit te SP-se perkatese (nje objekt clsMesazh qe tregon nese veprimi eshte kryer me sukses apo jo.<seealso cref="DbCore.clsMesazh"/>)</returns>
        /// </summary>
        internal clsMesazh fshiKontakt(int idkontaktiklientfurnitor)
        {//fshirja e kontaktit per klient furnitorin

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDKONTAKTIKLIENTFURNITOR", idkontaktiklientfurnitor, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_KONTAKTIKLIENTFURNITOR_del");
            return new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);

        }
        //[Obsolete("Perdor: ", true)]
        //public clsMesazh fshiKontakt(clsKontaktiKlientFurnitor kontakt)
        //{//fshirja e kontaktit per klient furnitorin
        //    try
        //    {
        //        dbManager.CreateParameters(1);
        //        dbManager.AddParameters(0, "@IDKONTAKTIKLIENTFURNITOR", kontakt.IdKontaktiKlientFurnitor, ParameterDirection.Input);
        //        dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_KONTAKTIKLIENTFURNITOR_del");
        //        return new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
        //    }
        //    catch (Exception)
        //    {
        //        return new clsMesazh(false, "Fshirja perfundoi me gabime!");
        //    }
        //}

        /// <summary>
        /// Ekzekuton prc_T_KONTAKTIKLIENTFURNITOR_sel per te marre nje objekt clsKontaktiKlientFurnitor ne DB duke filtruar sipas ID-se se kontaktit.
        /// <param name="kontakt">Objekt i tipit clsKontaktiKlientFurnitor nga i cili merret ID per filtrim</param>
        /// </summary>
        internal void merrKontakt(int idkontaktiklientfurnitor)
        {// metoda per te marre nje kontaktin ne base te id

            dbManager.Open();
            //shtimi i parametrave
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDKONTAKTIKLIENTFURNITOR", idkontaktiklientfurnitor, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_KONTAKTIKLIENTFURNITOR_sel");

        }
      

        /// <summary>
        /// Ekzekuton prc_T_KONTAKTIKLIENTFURNITOR_merrGjitheKontaktet per te marre prc_T_KONTAKTIKLIENTFURNITOR_merrGjitheKontaktet ne DB.
        /// <returns> Kthen prc_T_KONTAKTIKLIENTFURNITOR_merrGjitheKontaktet</returns>
        /// </summary>
        internal DataTable ktheGjitheKontaktet()
        {//metoda per te marre te gjithe  kontaktet

            dbManager.Open();
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KONTAKTIKLIENTFURNITOR_merrGjitheKontaktet");
            return ds.Tables[0];
        }
     

        /// <summary>
        /// Ekzekuton prc_T_KONTAKTIKLIENTFURNITOR_merrKontaktSipasIdKlientFurnitor per te marre prc_T_KONTAKTIKLIENTFURNITOR_merrGjitheKontaktet ne DB duke filtruar sipas ID-se se kolientit/furnitorit.
        /// <param name="id">ID e klientit/furnitorit</param>
        /// <returns> Kthen prc_T_KONTAKTIKLIENTFURNITOR_merrGjitheKontaktet</returns>
        /// </summary>
        internal DataTable ktheKontaktSipasIdKlientFurnitor(int id)
        {//metoda per te marre te gjithe kontaktet sipas idklientfurnitor

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDKLIENTFURNITOR", id, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KONTAKTIKLIENTFURNITOR_merrKontaktSipasIdKlientFurnitor");
            return ds.Tables[0];
        }
       

        #endregion

        /// <summary>
        /// Metodat e meposhtme i perkasin veprimeve qe kryhen nga objektet clsKlientFurnitor dhe colKlienteFurnitore
        /// </summary>
        #region KLIENT FURNITOR

        /// <summary>
        /// Ekzekuton prc_T_KLIENTFURNITOR_ins per te ruajtur nje objekt clsKlientFurnitor ne DB.
        /// <param name="idKlientFurnitor">Id e klientit/furnitorit</param>
        /// <param name="kodKliFurn">Kodi i  klientit/furnitorit</param>
        /// <param name="idLlog"> Id e llogarise se  klientit/furnitorit</param>
        /// <param name="llojiKF">Lloji - klient apo furnitor</param>
        /// <param name="titulliKF">Titulli <example>Kompani, person fizik</example></param>
        /// <param name="aktivitetiKF">Aktiviteti i Kompani, person fizik</param>
        /// <param name="emertimiKF">Emertimi i klientit/furnitorit</param>
        /// <param name="emerKerkimiKF">Emri i kerkimit i klientit/furnitorit</param>
        /// <param name="niptKF">NIPT-i i klientit/furnitorit</param>
        /// <param name="qytetiKF">Qyteti i klientit/furnitorit</param>
        /// <param name="shtetiKF">Shteti i klientit/furnitorit</param>
        /// <param name="telKF">Nr i telefonit i klientit/furnitorit</param>
        /// <param name="faxKF">Nr i fax-it i klientit/furnitorit</param>
        /// <param name="celKF">Nr i celularit i klientit/furnitorit</param>
        /// <param name="emailKF">Adresa e-mail e klientit/furnitorit</param>
        /// <param name="webpageKF">Web page e klientit/furnitorit</param>
        /// <param name="ibanKF">IBAN e klientit/furnitorit</param>
        /// <param name="llogariBankareKF">Llogaria bankare e klientit/furnitorit</param>
        /// <param name="aktivKF">Tregues nese klienti/furnitori eshte aktiv</param>
        /// <param name="idLlogZbritje">Llogaria e zbritjes se klientit/furnitorit</param>
        /// <param name="idLlogariDytesore">Llogaria dytesore e klientit/furnitorit</param>
        /// <param name="idKushtePagese">Kushti i pageses se klientit/furnitorit</param>
        /// <param name="idMetoda">Metoda</param>
        /// <param name="maturimiKF">Maturimi</param>
        /// <param name="idKatZbritje">Kategoria e zbritjes</param>
        /// <param name="limitParalajmerues">Limiti paralajmerues</param>
        /// <param name="limitBllokues">Limiti bllokues</param>
        /// <param name="idKategoriKlienti">Kategoria e klientit/furnitorit</param>
        /// <param name="kushteDergimi">Kushti i dergimit</param>
        /// <param name="menyraTransportit">Menyra e transportit</param>
        /// <param name="ofertaAutomatike">Oferta automatike</param>
        /// <param name="vleraLimitPorositur">Vlera limit  e porositur</param>
        /// <param name="prioriteti">Prioriteti</param>
        /// <param name="cmimUlet">Cmimi i ulet</param>
        /// <param name="idNivelCmimi">Niveli i cmimit</param>
        /// <param name="idPerfaqesuesShitje">Agjenti i shitjes</param>
        /// <param name="idQenderKosto">Qendra e kostos</param>
        /// <param name="idFushata">Fushata</param>
        /// <param name="zbritjeAnalitike">Zbritja analitike</param>
        /// <param name="zbritjeTotal">Zbritja totale</param>
        /// <param name="idndermarja">ndermarrja</param>
        /// <param name="vit">Viti</param>
        /// <param name="idperdoruesi">Id e perdosruesit</param>
        /// <param name="idKonfig">id e konfigurimit</param>
        /// <param name="adresaBanka">adresa e bankes</param>
        /// <param name="emriBanka">emri i bankes</param>
        /// <param name="grupim1kf">grupimi 1 i klientit</param>
        /// /// <param name="grupim2kf">grupimi 2 i klientit</param>
        /// /// <param name="grupim3kf">grupimi 3 i klientit</param>
        /// <returns> Kthen statusin e perfundimit te ekzekutimit te SP-se perkatese (nje objekt clsMesazh qe tregon nese veprimi eshte kryer me sukses apo jo.<seealso cref="DbCore.clsMesazh"/>)</returns>
        /// </summary>
        internal int ruajKF(int idKlientFurnitor, string kodKliFurn, int idLlog, bool llojiKF, int titulliKF, string aktivitetiKF, string emertimiKF, string emerKerkimiKF, string niptKF, int qytetiKF, string shtetiKF, string telKF, string faxKF, string celKF, string emailKF, string webpageKF, string ibanKF, string llogariBankareKF, bool aktivKF, int idLlogZbritje, int idLlogariDytesore, int idKushtePagese, int idMetoda, int maturimiKF, int idKatZbritje, int limitParalajmerues, int limitBllokues, int idKategoriKlienti, string kushteDergimi, string menyraTransportit, bool ofertaAutomatike, decimal vleraLimitPorositur, int prioriteti, decimal cmimUlet, int idNivelCmimi, int idPerfaqesuesShitje, int idQenderKosto, int idFushata, int zbritjeAnalitike, decimal zbritjeTotal, int idndermarja, int vit, int idperdoruesi, int idKonfig, int idstatusdok, string licenca, string swift, int emriBanka, string adresaBanka, int grupim1kf, int grupim2kf, int grupim3kf, string nrtvsh, int idobjektivakosto, int idkrijuesi, int idndermarjebij, int llojporosie, bool kupon, string koordinata, int idPerfaqesuesShitje2, bool klientspecifik, bool fermer, bool autongarkese, bool shitjepatvsh, decimal perqindjeagjenti1, decimal perqindjeagjenti2, bool prospekt, string emailPajisje, string Shenime, int idkfkryesor, DateTime dteDatelindjaKF,int idPerfaqesuesShitje3, decimal perqindjeagjenti3, int idtvsh , string emertimFature ,bool llogaritKomision, string kodIntegrimi, string kodiisksh, bool meDogane, string tipiId , bool klientFiskalizimi,string kodiMobile = ""  )
        {


            //ruajtja e klient furnitorit

            dbManager.Open();
            if(klientFiskalizimi)
                dbManager.CreateParameters(81);
            else
                dbManager.CreateParameters(80);
            dbManager.AddParameters(0, "@IDKLIENTFURNITOR", idKlientFurnitor, ParameterDirection.Output);
            dbManager.AddParameters(1, "@KODKLIENTFURNITOR", kodKliFurn, ParameterDirection.Input);

            if (idLlog == 0 || idLlog == -1) dbManager.AddParameters(2, "@IDLLOGARI", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(2, "@IDLLOGARI", idLlog, ParameterDirection.Input);

            dbManager.AddParameters(3, "@LLOJIKF", llojiKF, ParameterDirection.Input);
            dbManager.AddParameters(4, "@TITULLIKF", titulliKF, ParameterDirection.Input);
            dbManager.AddParameters(5, "@AKTIVITETIKF", aktivitetiKF, ParameterDirection.Input);
            dbManager.AddParameters(6, "@EMERTIMIKF", emertimiKF, ParameterDirection.Input);
            dbManager.AddParameters(7, "@EMERKERKRIMIKF", emerKerkimiKF, ParameterDirection.Input);
            dbManager.AddParameters(8, "@NIPTKF", niptKF, ParameterDirection.Input);
            if (qytetiKF == 0) dbManager.AddParameters(9, "@QYTETIKF", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(9, "@QYTETIKF", qytetiKF, ParameterDirection.Input);
            dbManager.AddParameters(10, "@SHTETIKF", shtetiKF, ParameterDirection.Input);
            dbManager.AddParameters(11, "@TELKF", telKF, ParameterDirection.Input);
            dbManager.AddParameters(12, "@FAXKF", faxKF, ParameterDirection.Input);
            dbManager.AddParameters(13, "@CELKF", celKF, ParameterDirection.Input);
            dbManager.AddParameters(14, "@EMAILKF", emailKF, ParameterDirection.Input);
            dbManager.AddParameters(15, "@WEBPAGEKF", webpageKF, ParameterDirection.Input);
            dbManager.AddParameters(16, "@IBANKF", ibanKF, ParameterDirection.Input);
            dbManager.AddParameters(17, "@LLOGARIBANKAREKF", llogariBankareKF, ParameterDirection.Input);
            dbManager.AddParameters(18, "@AKTIVKF", aktivKF, ParameterDirection.Input);
            if (idLlogZbritje == 0 || idLlogZbritje == -1) dbManager.AddParameters(19, "@IDLLOGZBRITJE", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(19, "@IDLLOGZBRITJE", idLlogZbritje, ParameterDirection.Input);
            if (idLlogariDytesore == 0 || idLlogariDytesore == -1) dbManager.AddParameters(20, "@IDLLOGARIDYTESORE", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(20, "@IDLLOGARIDYTESORE", idLlogariDytesore, ParameterDirection.Input);
            if (idKushtePagese == 0) dbManager.AddParameters(21, "@IDKUSHTEPAGESE", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(21, "@IDKUSHTEPAGESE", idKushtePagese, ParameterDirection.Input);
            dbManager.AddParameters(22, "@IDMETODA", idMetoda, ParameterDirection.Input);
            if (maturimiKF == 0 || maturimiKF == -1) dbManager.AddParameters(23, "@MATURIMIKF", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(23, "@MATURIMIKF", maturimiKF, ParameterDirection.Input);
            if (idKatZbritje == 0 || idKatZbritje == -1) dbManager.AddParameters(24, "@IDKATZBRITJE", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(24, "@IDKATZBRITJE", idKatZbritje, ParameterDirection.Input);
            dbManager.AddParameters(25, "@LIMITPARALAJMERUES", limitParalajmerues, ParameterDirection.Input);
            dbManager.AddParameters(26, "@LIMITBLLOKUES", limitBllokues, ParameterDirection.Input);
            if (idKategoriKlienti == 0) dbManager.AddParameters(27, "@IDKATEGORIKLIENTI", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(27, "@IDKATEGORIKLIENTI", idKategoriKlienti, ParameterDirection.Input);
            if (kushteDergimi == "0") kushteDergimi = "";
            dbManager.AddParameters(28, "@KUSHTEDERGIMI", kushteDergimi, ParameterDirection.Input);
            if (menyraTransportit == "0") menyraTransportit = "";
            dbManager.AddParameters(29, "@MENYRATRASPORTIT", menyraTransportit, ParameterDirection.Input);
            dbManager.AddParameters(30, "@OFERTAAUTOMATIKE", ofertaAutomatike, ParameterDirection.Input);
            dbManager.AddParameters(31, "@VLERALIMITPOROSITUR", vleraLimitPorositur, ParameterDirection.Input);
            dbManager.AddParameters(32, "@PRIORITETI", prioriteti, ParameterDirection.Input);
            dbManager.AddParameters(33, "@CMIMULET", cmimUlet, ParameterDirection.Input);
            if (idNivelCmimi == 0) dbManager.AddParameters(34, "@IDNIVELCMIMI", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(34, "@IDNIVELCMIMI", idNivelCmimi, ParameterDirection.Input);
            if (idPerfaqesuesShitje == 0) dbManager.AddParameters(35, "@IDPERFAQESUESSHITJE", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(35, "@IDPERFAQESUESSHITJE", idPerfaqesuesShitje, ParameterDirection.Input);
            dbManager.AddParameters(36, "@IDQENDERKOSTO", idQenderKosto, ParameterDirection.Input);
            dbManager.AddParameters(37, "@IDFUSHATA", idFushata, ParameterDirection.Input);
            if (zbritjeAnalitike == 0 || zbritjeAnalitike == -1) dbManager.AddParameters(38, "@ZBRITJEANALITIKE", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(38, "@ZBRITJEANALITIKE", zbritjeAnalitike, ParameterDirection.Input);
            dbManager.AddParameters(39, "@ZBRITJETOTAL", zbritjeTotal, ParameterDirection.Input);
            dbManager.AddParameters(40, "@IDNDERMARJE", idndermarja, ParameterDirection.Input);
            dbManager.AddParameters(41, "@VITI", vit, ParameterDirection.Input);
            //dbManager.AddParameters(42, "@IDNDERVITI", idndermvit, ParameterDirection.Input);
            dbManager.AddParameters(42, "@IDPERDORUESI", idperdoruesi, ParameterDirection.Input);
            dbManager.AddParameters(43, "@IDKONFIG", idKonfig, ParameterDirection.Input);
            dbManager.AddParameters(44, "@IDSTATUSDOK", idstatusdok, ParameterDirection.Input);
            dbManager.AddParameters(45, "@LICENCA", licenca, ParameterDirection.Input);
            dbManager.AddParameters(46, "@SWIFT", swift, ParameterDirection.Input);
            dbManager.AddParameters(47, "@EMRIBANKA", emriBanka, ParameterDirection.Input);
            dbManager.AddParameters(48, "@ADRESABANKA", adresaBanka, ParameterDirection.Input);
            if (grupim1kf == 0)
                dbManager.AddParameters(49, "@GRUPIM1KF", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(49, "@GRUPIM1KF", grupim1kf, ParameterDirection.Input);
            if (grupim2kf == 0)
                dbManager.AddParameters(50, "@GRUPIM2KF", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(50, "@GRUPIM2KF", grupim2kf, ParameterDirection.Input);
            if (grupim3kf == 0)
                dbManager.AddParameters(51, "@GRUPIM3KF", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(51, "@GRUPIM3KF", grupim3kf, ParameterDirection.Input);

            dbManager.AddParameters(52, "@NRTVSH", nrtvsh, ParameterDirection.Input);
            if (idobjektivakosto == 0 || idobjektivakosto == -1) dbManager.AddParameters(53, "@IDOBJEKTIVAKOSTO", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(53, "@IDOBJEKTIVAKOSTO", idobjektivakosto, ParameterDirection.Input);
            dbManager.AddParameters(54, "@IDKRIJUESI", idkrijuesi, ParameterDirection.Input);
            if (idndermarjebij > 0) dbManager.AddParameters(55, "@IDNDERMARJEBIJ", idndermarjebij, ParameterDirection.Input);

            else dbManager.AddParameters(55, "@IDNDERMARJEBIJ", DBNull.Value, ParameterDirection.Input);
            if (llojporosie > 0)
                dbManager.AddParameters(56, "@LLOJPOROSIE", llojporosie, ParameterDirection.Input);
            else
                dbManager.AddParameters(56, "@LLOJPOROSIE", DBNull.Value, ParameterDirection.Input);
            dbManager.AddParameters(57, "@KUPON", kupon, ParameterDirection.Input);
            if (koordinata == "")
                dbManager.AddParameters(58, "@KOORDINATA", DBNull.Value, ParameterDirection.Input);
            else
                dbManager.AddParameters(58, "@KOORDINATA", koordinata, ParameterDirection.Input);
            if (idPerfaqesuesShitje2 == 0) dbManager.AddParameters(59, "@IDPERFAQESUESSHITJE2", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(59, "@IDPERFAQESUESSHITJE2", idPerfaqesuesShitje2, ParameterDirection.Input);
            dbManager.AddParameters(60, "@KLIENTSPECIFIK", klientspecifik, ParameterDirection.Input);
            dbManager.AddParameters(61, "@FERMER", fermer, ParameterDirection.Input);
            dbManager.AddParameters(62, "@AUTONGARKESE", autongarkese, ParameterDirection.Input);
            dbManager.AddParameters(63, "@SHITJEPATVSH", shitjepatvsh, ParameterDirection.Input);
            if (perqindjeagjenti1 == 0) dbManager.AddParameters(64, "@PERQINDJEAGJENTI", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(64, "@PERQINDJEAGJENTI", perqindjeagjenti1, ParameterDirection.Input);
            if (perqindjeagjenti2 == 0) dbManager.AddParameters(65, "@PERQINDJEAGJENTI2", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(65, "@PERQINDJEAGJENTI2", perqindjeagjenti2, ParameterDirection.Input);
            dbManager.AddParameters(66, "@PROSPEKT", prospekt, ParameterDirection.Input);
            dbManager.AddParameters(67, "@KODIMOBILE", kodiMobile, ParameterDirection.Input);
            dbManager.AddParameters(68, "@EMAILPERPAJISJE", emailPajisje, ParameterDirection.Input);
            dbManager.AddParameters(69, "@IDKLIENTFURNITORKRYESOR ", idkfkryesor, ParameterDirection.Input);
            dbManager.AddParameters(70, "@Shenime", Shenime, ParameterDirection.Input);
            dbManager.AddParameters(71, "@DatelindjaKF", dteDatelindjaKF, ParameterDirection.Input);
            if (idPerfaqesuesShitje3 == 0) dbManager.AddParameters(72, "@IDPERFAQESUESSHITJE3", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(72, "@IDPERFAQESUESSHITJE3", idPerfaqesuesShitje3, ParameterDirection.Input);
            if (perqindjeagjenti3 == 0) dbManager.AddParameters(73, "@PERQINDJEAGJENTI3", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(73, "@PERQINDJEAGJENTI3", perqindjeagjenti3, ParameterDirection.Input);
            if (idtvsh == 0) dbManager.AddParameters(74, "@IDTVSH", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(74, "@IDTVSH", idtvsh, ParameterDirection.Input);
            dbManager.AddParameters(75, "@EMERTIMFATURE", emertimFature, ParameterDirection.Input);
            dbManager.AddParameters(76, "@LLOGARITKOMISION", llogaritKomision, ParameterDirection.Input);
            dbManager.AddParameters(77, "@KODINTEGRIMI", kodIntegrimi, ParameterDirection.Input);
            dbManager.AddParameters(78, "@KODIISKSH", kodiisksh, ParameterDirection.Input);
            dbManager.AddParameters(79, "@MEDOGANE", meDogane, ParameterDirection.Input);
            if(klientFiskalizimi)
                dbManager.AddParameters(80, "@TIPIID", tipiId, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_KLIENTFURNITOR_ins");
            
            idKlientFurnitor = int.Parse(dbManager.Parameters[0].Value.ToString());
            //clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
            return idKlientFurnitor;
        }

        /// <summary>
        /// Ekzekuton prc_T_KLIENTFURNITOR_upd per te modifikuar nje objekt clsKlientFurnitor ne DB.
        /// <param name="idKlientFurnitor">Id e klientit/furnitorit</param>
        /// <param name="kodKliFurn">Kodi i  klientit/furnitorit</param>
        /// <param name="idLlog"> Id e llogarise se  klientit/furnitorit</param>
        /// <param name="llojiKF">Lloji - klient apo furnitor</param>
        /// <param name="titulliKF">Titulli <example>Kompani, person fizik</example></param>
        /// <param name="aktivitetiKF">Aktiviteti i Kompani, person fizik</param>
        /// <param name="emertimiKF">Emertimi i klientit/furnitorit</param>
        /// <param name="emerKerkimiKF">Emri i kerkimit i klientit/furnitorit</param>
        /// <param name="niptKF">NIPT-i i klientit/furnitorit</param>
        /// <param name="qytetiKF">Qyteti i klientit/furnitorit</param>
        /// <param name="shtetiKF">Shteti i klientit/furnitorit</param>
        /// <param name="telKF">Nr i telefonit i klientit/furnitorit</param>
        /// <param name="faxKF">Nr i fax-it i klientit/furnitorit</param>
        /// <param name="celKF">Nr i celularit i klientit/furnitorit</param>
        /// <param name="emailKF">Adresa e-mail e klientit/furnitorit</param>
        /// <param name="webpageKF">Web page e klientit/furnitorit</param>
        /// <param name="ibanKF">IBAN e klientit/furnitorit</param>
        /// <param name="llogariBankareKF">Llogaria bankare e klientit/furnitorit</param>
        /// <param name="aktivKF">Tregues nese klienti/furnitori eshte aktiv</param>
        /// <param name="idLlogZbritje">Llogaria e zbritjes se klientit/furnitorit</param>
        /// <param name="idLlogariDytesore">Llogaria dytesore e klientit/furnitorit</param>
        /// <param name="idKushtePagese">Kushti i pageses se klientit/furnitorit</param>
        /// <param name="idMetoda">Metoda</param>
        /// <param name="maturimiKF">Maturimi</param>
        /// <param name="idKatZbritje">Kategoria e zbritjes</param>
        /// <param name="limitParalajmerues">Limiti paralajmerues</param>
        /// <param name="limitBllokues">Limiti bllokues</param>
        /// <param name="idKategoriKlienti">Kategoria e klientit/furnitorit</param>
        /// <param name="kushteDergimi">Kushti i dergimit</param>
        /// <param name="menyraTransportit">Menyra e transportit</param>
        /// <param name="ofertaAutomatike">Oferta automatike</param>
        /// <param name="vleraLimitPorositur">Vlera limit  e porositur</param>
        /// <param name="prioriteti">Prioriteti</param>
        /// <param name="cmimUlet">Cmimi i ulet</param>
        /// <param name="idNivelCmimi">Niveli i cmimit</param>
        /// <param name="idPerfaqesuesShitje">Agjenti i shitjes</param>
        /// <param name="idQenderKosto">Qendra e kostos</param>
        /// <param name="idFushata">Fushata</param>
        /// <param name="zbritjeAnalitike">Zbritja analitike</param>
        /// <param name="zbritjeTotal">Zbritja totale</param>
        /// <param name="idperdoruesi">Id e perdosruesit</param>
        /// <param name="idKonfig">id e konfigurimit</param>
        /// <returns> Kthen statusin e perfundimit te ekzekutimit te SP-se perkatese (nje objekt clsMesazh qe tregon nese veprimi eshte kryer me sukses apo jo.<seealso cref="DbCore.clsMesazh"/>)</returns>
        /// </summary>
        internal clsMesazh modifikoKF(int idKlientFurnitor, string kodKliFurn, int idLlog, bool llojiKF, int titulliKF, string aktivitetiKF, string emertimiKF, string emerKerkimiKF, string niptKF, int qytetiKF, string shtetiKF, string telKF, string faxKF, string celKF, string emailKF, string webpageKF, string ibanKF, string llogariBankareKF, bool aktivKF, int idLlogZbritje, int idLlogariDytesore, int idKushtePagese, int idMetoda, int maturimiKF, int idKatZbritje, int limitParalajmerues, int limitBllokues, int idKategoriKlienti, string kushteDergimi, string menyraTransportit, bool ofertaAutomatike, decimal vleraLimitPorositur, int prioriteti, decimal cmimUlet, int idNivelCmimi, int idPerfaqesuesShitje, int idQenderKosto, int idFushata, int zbritjeAnalitike, decimal zbritjeTotal, int idperdoruesi, int idKonfig, int idstatusdok, string licenca, string swift, int emriBanka, string adresaBanka, int grupim1kf, int grupim2kf, int grupim3kf, string nrtvsh, int idobjektivakosto, int idkrijuesi, int idndermarjebij, int llojporosie, bool kupon, string koordinata, int idPerfaqesuesShitje2, bool klientspecifik, bool fermer, bool autongarkese, bool shitjepatvsh, decimal perqindjeagjent, decimal perqindjeagjent2, bool prospekt, string emailPajisje, string Shenime, DateTime dteDatelindjaKF,int idPerfaqesuesShitje3, decimal perqindjeagjent3, int idtvsh, string emertimFature ,bool llogaritKomision ,  string kodIntegrimi, string kodiisksh, bool meDogane,string tipiId,bool klientFiskalizimi,string kodiMobile = "", int idkfkryesor = 0)
        {
            //modifikimi i klient furnitorit

            dbManager.Open();
            if(klientFiskalizimi)
                dbManager.CreateParameters(79);
            else
                dbManager.CreateParameters(78);
            dbManager.AddParameters(0, "@IDKLIENTFURNITOR", idKlientFurnitor, ParameterDirection.Input);
            dbManager.AddParameters(1, "@KODKLIENTFURNITOR", kodKliFurn, ParameterDirection.Input);
            if (idLlog == 0 || idLlog == -1) dbManager.AddParameters(2, "@IDLLOGARI", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(2, "@IDLLOGARI", idLlog, ParameterDirection.Input);
            dbManager.AddParameters(3, "@LLOJIKF", llojiKF, ParameterDirection.Input);
            dbManager.AddParameters(4, "@TITULLIKF", titulliKF, ParameterDirection.Input);
            dbManager.AddParameters(5, "@AKTIVITETIKF", aktivitetiKF, ParameterDirection.Input);
            dbManager.AddParameters(6, "@EMERTIMIKF", emertimiKF, ParameterDirection.Input);
            dbManager.AddParameters(7, "@EMERKERKRIMIKF", emerKerkimiKF, ParameterDirection.Input);
            dbManager.AddParameters(8, "@NIPTKF", niptKF, ParameterDirection.Input);
            if (qytetiKF == 0) dbManager.AddParameters(9, "@QYTETIKF", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(9, "@QYTETIKF", qytetiKF, ParameterDirection.Input);
            dbManager.AddParameters(10, "@SHTETIKF", shtetiKF, ParameterDirection.Input);
            dbManager.AddParameters(11, "@TELKF", telKF, ParameterDirection.Input);
            dbManager.AddParameters(12, "@FAXKF", faxKF, ParameterDirection.Input);
            dbManager.AddParameters(13, "@CELKF", celKF, ParameterDirection.Input);
            dbManager.AddParameters(14, "@EMAILKF", emailKF, ParameterDirection.Input);
            dbManager.AddParameters(15, "@WEBPAGEKF", webpageKF, ParameterDirection.Input);
            dbManager.AddParameters(16, "@IBANKF", ibanKF, ParameterDirection.Input);
            dbManager.AddParameters(17, "@LLOGARIBANKAREKF", llogariBankareKF, ParameterDirection.Input);
            dbManager.AddParameters(18, "@AKTIVKF", aktivKF, ParameterDirection.Input);
            if (idLlogZbritje == 0 || idLlogZbritje == -1) dbManager.AddParameters(19, "@IDLLOGZBRITJE", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(19, "@IDLLOGZBRITJE", idLlogZbritje, ParameterDirection.Input);
            if (idLlogariDytesore == 0 || idLlogariDytesore == -1) dbManager.AddParameters(20, "@IDLLOGARIDYTESORE", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(20, "@IDLLOGARIDYTESORE", idLlogariDytesore, ParameterDirection.Input);
            if (idKushtePagese == 0) dbManager.AddParameters(21, "@IDKUSHTEPAGESE", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(21, "@IDKUSHTEPAGESE", idKushtePagese, ParameterDirection.Input);
            dbManager.AddParameters(22, "@IDMETODA", idMetoda, ParameterDirection.Input);
            if (maturimiKF == 0) dbManager.AddParameters(23, "@MATURIMIKF", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(23, "@MATURIMIKF", maturimiKF, ParameterDirection.Input);
            if (idKatZbritje == 0) dbManager.AddParameters(24, "@IDKATZBRITJE", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(24, "@IDKATZBRITJE", idKatZbritje, ParameterDirection.Input);
            dbManager.AddParameters(25, "@LIMITPARALAJMERUES", limitParalajmerues, ParameterDirection.Input);
            dbManager.AddParameters(26, "@LIMITBLLOKUES", limitBllokues, ParameterDirection.Input);
            if (idKategoriKlienti == 0) dbManager.AddParameters(27, "@IDKATEGORIKLIENTI", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(27, "@IDKATEGORIKLIENTI", idKategoriKlienti, ParameterDirection.Input);
            if (kushteDergimi == "0") kushteDergimi = "";
            dbManager.AddParameters(28, "@KUSHTEDERGIMI", kushteDergimi, ParameterDirection.Input);
            if (menyraTransportit == "0") menyraTransportit = "";
            dbManager.AddParameters(29, "@MENYRATRASPORTIT", menyraTransportit, ParameterDirection.Input);
            dbManager.AddParameters(30, "@OFERTAAUTOMATIKE", ofertaAutomatike, ParameterDirection.Input);
            dbManager.AddParameters(31, "@VLERALIMITPOROSITUR", vleraLimitPorositur, ParameterDirection.Input);
            dbManager.AddParameters(32, "@PRIORITETI", prioriteti, ParameterDirection.Input);
            dbManager.AddParameters(33, "@CMIMULET", cmimUlet, ParameterDirection.Input);
            if (idNivelCmimi == 0) dbManager.AddParameters(34, "@IDNIVELCMIMI", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(34, "@IDNIVELCMIMI", idNivelCmimi, ParameterDirection.Input);
            if (idPerfaqesuesShitje == 0) dbManager.AddParameters(35, "@IDPERFAQESUESSHITJE", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(35, "@IDPERFAQESUESSHITJE", idPerfaqesuesShitje, ParameterDirection.Input);

            dbManager.AddParameters(36, "@IDQENDERKOSTO", idQenderKosto, ParameterDirection.Input);
            dbManager.AddParameters(37, "@IDFUSHATA", idFushata, ParameterDirection.Input);
            if (zbritjeAnalitike == 0) dbManager.AddParameters(38, "@ZBRITJEANALITIKE", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(38, "@ZBRITJEANALITIKE", zbritjeAnalitike, ParameterDirection.Input);
            dbManager.AddParameters(39, "@ZBRITJETOTAL", zbritjeTotal, ParameterDirection.Input);
            dbManager.AddParameters(40, "@IDPERDORUESI", idperdoruesi, ParameterDirection.Input);
            dbManager.AddParameters(41, "@IDKONFIG", idKonfig, ParameterDirection.Input);
            dbManager.AddParameters(42, "@IDSTATUSDOK", idstatusdok, ParameterDirection.Input);
            dbManager.AddParameters(43, "@LICENCA", licenca, ParameterDirection.Input);
            dbManager.AddParameters(44, "@SWIFT", swift, ParameterDirection.Input);
            dbManager.AddParameters(45, "@EMRIBANKA", emriBanka, ParameterDirection.Input);
            dbManager.AddParameters(46, "@ADRESABANKA", adresaBanka, ParameterDirection.Input);
            if (grupim1kf == 0) dbManager.AddParameters(47, "@GRUPIM1KF", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(47, "@GRUPIM1KF", grupim1kf, ParameterDirection.Input);
            if (grupim2kf == 0) dbManager.AddParameters(48, "@GRUPIM2KF", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(48, "@GRUPIM2KF", grupim2kf, ParameterDirection.Input);
            if (grupim3kf == 0) dbManager.AddParameters(49, "@GRUPIM3KF", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(49, "@GRUPIM3KF", grupim3kf, ParameterDirection.Input);
            dbManager.AddParameters(50, "@NRTVSH", nrtvsh, ParameterDirection.Input);
            if (idobjektivakosto == 0 || idobjektivakosto == -1) dbManager.AddParameters(51, "@IDOBJEKTIVAKOSTO", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(51, "@IDOBJEKTIVAKOSTO", idobjektivakosto, ParameterDirection.Input);
            dbManager.AddParameters(52, "@IDKRIJUESI", idkrijuesi, ParameterDirection.Input);
            if (idndermarjebij > 0)
                dbManager.AddParameters(53, "@IDNDERMARJEBIJ", idndermarjebij, ParameterDirection.Input);
            else dbManager.AddParameters(53, "@IDNDERMARJEBIJ", DBNull.Value, ParameterDirection.Input);
            if (llojporosie > 0) dbManager.AddParameters(54, "@LLOJPOROSIE", llojporosie, ParameterDirection.Input);

            else dbManager.AddParameters(54, "@LLOJPOROSIE", DBNull.Value, ParameterDirection.Input);
            dbManager.AddParameters(55, "@KUPON", kupon, ParameterDirection.Input);
            if (koordinata == "")
                dbManager.AddParameters(56, "@KOORDINATA", DBNull.Value, ParameterDirection.Input);
            else
                dbManager.AddParameters(56, "@KOORDINATA", koordinata, ParameterDirection.Input);
            if (idPerfaqesuesShitje2 == 0) dbManager.AddParameters(57, "@IDPERFAQESUESSHITJE2", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(57, "@IDPERFAQESUESSHITJE2", idPerfaqesuesShitje2, ParameterDirection.Input);
            dbManager.AddParameters(58, "@KLIENTSPECIFIK", klientspecifik, ParameterDirection.Input);
            dbManager.AddParameters(59, "@FERMER", fermer, ParameterDirection.Input);
            dbManager.AddParameters(60, "@AUTONGARKESE", autongarkese, ParameterDirection.Input);
            dbManager.AddParameters(61, "@SHITJEPATVSH", shitjepatvsh, ParameterDirection.Input);


            if (perqindjeagjent == 0) dbManager.AddParameters(62, "@PERQINDJEAGJENTI", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(62, "@PERQINDJEAGJENTI", perqindjeagjent, ParameterDirection.Input);
            if (perqindjeagjent2 == 0) dbManager.AddParameters(63, "@PERQINDJEAGJENTI2", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(63, "@PERQINDJEAGJENTI2", perqindjeagjent2, ParameterDirection.Input);
            dbManager.AddParameters(64, "@PROSPEKT", prospekt, ParameterDirection.Input);
            dbManager.AddParameters(65, "@KODIMOBILE", kodiMobile, ParameterDirection.Input);
            dbManager.AddParameters(66, "@EMAILPERPAJISJE", emailPajisje, ParameterDirection.Input);
            dbManager.AddParameters(67, "@IDKLIENTFURNITORKRYESOR", idkfkryesor, ParameterDirection.Input);
            dbManager.AddParameters(68, "@Shenime", Shenime, ParameterDirection.Input);
            dbManager.AddParameters(69, "@DatelindjaKF", dteDatelindjaKF, ParameterDirection.Input);
            if (idPerfaqesuesShitje3 == 0) dbManager.AddParameters(70, "@IDPERFAQESUESSHITJE3", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(70, "@IDPERFAQESUESSHITJE3", idPerfaqesuesShitje3, ParameterDirection.Input);
            if (perqindjeagjent3 == 0) dbManager.AddParameters(71, "@PERQINDJEAGJENTI3", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(71, "@PERQINDJEAGJENTI3", perqindjeagjent3, ParameterDirection.Input);
            if (idtvsh == 0) dbManager.AddParameters(72, "@IDTVSH", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(72, "@IDTVSH", idtvsh, ParameterDirection.Input);
            dbManager.AddParameters(73, "@EMERTIMFATURE", emertimFature, ParameterDirection.Input);
            dbManager.AddParameters(74, "@LLOGARITKOMISION", llogaritKomision, ParameterDirection.Input);
            dbManager.AddParameters(75, "@KODINTEGRIMI", kodIntegrimi, ParameterDirection.Input);
            dbManager.AddParameters(76, "@KODIISKSH", kodiisksh, ParameterDirection.Input);
            dbManager.AddParameters(77, "@MEDOGANE", meDogane, ParameterDirection.Input);
            if(klientFiskalizimi)
                dbManager.AddParameters(78, "@TIPIID", tipiId, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_KLIENTFURNITOR_upd");
            idKlientFurnitor = int.Parse(dbManager.Parameters[0].Value.ToString());
            clsMesazh mesazh = new clsMesazh(true, IMBUtils.Messages.MessagesResource.Messages["labelRaportMesazhRuajtjaPerfundoiSukses"]);

            return mesazh;
        }

        /// <summary>
        /// Ekzekuton prc_T_KLIENTFURNITOR_del per te fshire nje objekt clsKlientFurnitor ne DB.
        /// <param name="idKlientFurnitor">Id e klientit/furnitorit</param>
        /// <returns> Kthen statusin e perfundimit te ekzekutimit te SP-se perkatese (nje objekt clsMesazh qe tregon nese veprimi eshte kryer me sukses apo jo.<seealso cref="DbCore.clsMesazh"/>)</returns>
        /// </summary>
        internal clsMesazh fshiKF(int idKlientFurnitor)
        {//fshirja e klient furnitorit

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDKLIENTFURNITOR", idKlientFurnitor, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_KLIENTFURNITOR_del");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
            return mesazh;
        }
        internal clsMesazh fshiKFStatus(int idKlientFurnitor, int idperdorues)
        {//fshirja e klient furnitorit

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDKLIENTFURNITOR", idKlientFurnitor, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUESI", idperdorues, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_KLIENTFURNITOR_upddel");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
            return mesazh;
        }

        /// <summary>
        /// Ekzekuton prc_T_KLIENTFURNITOR_ktheKlientFurnitorID per te marre nje datarow ne DB duke filtruar sipas kodit te klientit/furnitorit dhe sipas ID-se se ndermarrjes.
        /// <param name="kodKF">kodi i klient furnitorit</param>
        /// <param name="idNdermarrja">id e ndermarrjes</param>
        /// <returns>Kthen nje datarow</returns>
        /// </summary>
        internal DataRow ktheKlientFurnitorSipasKodit(string kodKF, int idNdermarrja)
        {// metoda per te marre nje llogari ne baze te nr te saj

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@KODKLIENTFURNITOR", kodKF, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idNdermarrja, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KLIENTFURNITOR_merrKlientFurnitorSipasKodit");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];
        }
        internal DataRow ktheKlientFurnitorSipasNdermarjeBij(int idndermarjebij, int idNdermarrja, int llojporosie)
        {// metoda per te marre nje llogari ne baze te nr te saj

            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@idndermarjebij", idndermarjebij, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idNdermarrja, ParameterDirection.Input);
            dbManager.AddParameters(2, "@llojporosie", llojporosie, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KLIENTFURNITOR_ktheKlientFurnitorSipasNdermarjeBij");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0)
                return null;
            return ds.Tables[0].Rows[0];
        }

        internal bool eshteNdermarrjeKlientiOwn(int idKlientFurnitor)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters("@IDKLIENTFURNITOR", idKlientFurnitor, ParameterDirection.Input);
            var obj = dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_KLIENTFURNITOR_T_NDERMARJE_eshteKlientINdermarrjesOwn");
            return  obj != null && Convert.ToBoolean(obj);
        }

        internal DataRow ktheKlientFurnitorSipasKoditAzhornim(int idKlientFurnitor, DateTime dtdok)
        {// metoda per te marre nje llogari ne baze te nr te saj

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@idKF", idKlientFurnitor, ParameterDirection.Input);
            dbManager.AddParameters(1, "@DtDok", dtdok, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KLIENTFURNITOR_merrKlientFurnitorSipasIdAzhornim");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];
        }

        internal DataRow ktheKlientFurnitorSipasKoditAzhornim(string kodKF, int idNdermarrja, DateTime dtdok)
        {// metoda per te marre nje llogari ne baze te nr te saj

            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@KODKLIENTFURNITOR", kodKF, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idNdermarrja, ParameterDirection.Input);
            dbManager.AddParameters(2, "@DtDok", dtdok, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KLIENTFURNITOR_merrKlientFurnitorSipasKoditAzhornim");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];

        }

        internal DataTable ktheDisaKlientFurnitorSipasIDAzhornim(string IDteKF, int idNdermarrja, DateTime dtdok)
        {
            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@IDte", IDteKF, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idNdermarrja, ParameterDirection.Input);
            dbManager.AddParameters(2, "@DtDok", dtdok, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KLIENTFURNITOR_merrDisaKlientFurnitorSipasIDAzhornim");

            return ds.Tables[0];
        }


        /// <summary>
        /// Ekzekuton prc_T_KLIENTFURNITOR_merrGjitheKlienteFurnitore per te marre nje datatable ne DB.
        /// <returns>Kthen nje datatable</returns>
        /// </summary>
        internal DataTable ktheGjitheKlienteFurnitore()
        {//metoda per te marre te gjithe kliente furnitoret

            dbManager.Open();
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KLIENTFURNITOR_merrGjitheKlienteFurnitore");
            return ds.Tables[0];

        }
        internal DataTable ktheGjitheKlienteFurnitoreSipasIdKarte(int idKarta)
        {//metoda per te marre te gjithe kliente furnitoret

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddInputParameters("@IDKARTA", idKarta);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KLIENTFURNITOR_ktheKlientFurnitorSipasIdKarte");
            return ds.Tables[0];

        }

        /// <summary>
        /// Ekzekuton prc_T_KLIENTFURNITOR_merrKlienteOseFurnitore per te marre nje datatable me klient/furnitoret aktiv ne DB duke filtruar sipas llojit(klient apo furnitor, id-se se ndermarrjes dhe id-se se perdoruesit.
        /// <param name="lloji">Lloji (klient apo furnitor)</param>
        /// <param name="idnderm">Id e ndermarrjes</param>
        /// <param name="idperdorues">Id e perdoruesit</param>
        /// <returns>Kthen nje datatable aktive</returns>
        /// </summary>
        internal DataTable ktheKlienteOseFurnitore(bool lloji, int idnderm, int idperdorues, bool meProspekt, int kfkryesor)
        {//metoda per te marre te gjithe kliente furnitoret

            dbManager.Open();
            dbManager.CreateParameters(5);
            dbManager.AddParameters(0, "@LLOJI", lloji, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDPERDORUES", idperdorues, ParameterDirection.Input);
            dbManager.AddParameters(3, "@MEPROSPEKT", meProspekt, ParameterDirection.Input);
            dbManager.AddParameters(4, "@KFKRYESOR", kfkryesor, ParameterDirection.Input);

            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KLIENTFURNITOR_merrKlienteOseFurnitore");
            return ds.Tables[0];

        }

        internal DataTable ktheKlienteOseFurnitore(string filter, long startIndex, long endIndex, bool lloji, int idnderm, int idperdorues, int kfkryesor)
        {//metoda per te marre te gjithe kliente furnitoret

            dbManager.Open();
            dbManager.CreateParameters(7);
            dbManager.AddParameters(0, "@LLOJI", lloji, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDPERDORUES", idperdorues, ParameterDirection.Input);
            dbManager.AddParameters(3, "@FILTER", filter, ParameterDirection.Input);
            dbManager.AddParameters(4, "@STARTINDEX", startIndex, ParameterDirection.Input);
            dbManager.AddParameters(5, "@ENDINDEX", endIndex, ParameterDirection.Input);
            dbManager.AddParameters(6, "@KFKRYESOR", kfkryesor, ParameterDirection.Input);


            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KLIENTFURNITOR_merrKlienteOseFurnitoreMeFilter");
            return ds.Tables[0];
        }

        internal DataTable ktheKlienteOseFurnitore(string filter, long startIndex, long endIndex, int idnderm, int idperdorues, int kfkryesor)
        {//metoda per te marre te gjithe kliente furnitoret

            dbManager.Open();
            dbManager.CreateParameters(6);
            dbManager.AddParameters(0, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUES", idperdorues, ParameterDirection.Input);
            dbManager.AddParameters(2, "@FILTER", filter, ParameterDirection.Input);
            dbManager.AddParameters(3, "@STARTINDEX", startIndex, ParameterDirection.Input);
            dbManager.AddParameters(4, "@ENDINDEX", endIndex, ParameterDirection.Input);
            dbManager.AddParameters(5, "@KFKRYESOR", kfkryesor, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KLIENTFURNITOR_merrKlienteOseFurnitoreMeFilterPaLloj");
            return ds.Tables[0];

        }



        /// <summary>
        /// Ekzekuton prc_T_KLIENTFURNITOR_merrKlienteFurnitoreNdermarrjes per te marre nje datatable ne DB duke filtruar sipas id-se se ndermarrjes dhe aktive.
        /// <param name="idnderm">Id e ndermarrjes</param>
        /// <returns>Kthen nje datatable</returns>
        /// </summary>
        internal DataTable ktheKlienteFurnitoreNdermarrjes(int idnderm)
        {//metoda per te marre te gjithe kliente furnitoret

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KLIENTFURNITOR_merrKlienteFurnitoreNdermarrjes");
            return ds.Tables[0];
        }


        /// <summary>
        /// Ekzekuton prc_T_KLIENTFURNITOR_merrKlienteFurnitoreNdermarrjesAndAutorizime per te marre nje datatable ne DB duke filtruar sipas id-se se ndermarrjes dhe id-se se perdoruesit.
        /// <param name="idnderm">Id e ndermarrjes</param>
        /// <param name="idperdorues">Id e perdoruesit</param>
        /// <returns>Kthen nje datatable</returns>
        /// </summary>
        internal DataTable ktheKlienteFurnitoreNdermarrjesAndAutorizime(int idnderm, int idperdorues)
        {//metoda per te marre te gjithe kliente furnitoret

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUES", idperdorues, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KLIENTFURNITOR_merrKlienteFurnitoreNdermarrjesAndAutorizime");
            return ds.Tables[0];
        }
        /// <summary>
        /// Ekzekuton prc_T_KLIENTFURNITOR_merrKlienteFurnitoreNdermarrjesAndAutorizime per te marre nje datatable ne DB duke filtruar sipas id-se se ndermarrjes dhe id-se se perdoruesit.
        /// <param name="idnderm">Id e ndermarrjes</param>
        /// <param name="idperdorues">Id e perdoruesit</param>
        /// <returns>Kthen nje datatable</returns>
        /// </summary>
        internal DataTable ktheKlienteFurnitoreNdermarrjesAndAutorizimeMonHuaj(int idnderm, int idperdorues, int idmonedha)
        {//metoda per te marre te gjithe kliente furnitoret

            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUES", idperdorues, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDMONEDHA", idmonedha, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KLIENTFURNITOR_merrKlienteFurnitoreNdermarrjesAndAutorizimeMonHuaj");
            return ds.Tables[0];
        }

        internal DataTable ktheKlienteFurnitoreNdermarrjesAndAutorizimeMonHuajMeGjendje(int idnderm, int idperdorues, int idmonedha, DateTime dtDok)
        {//metoda per te marre te gjithe kliente furnitoret ne monedhe te huaj me gjendje

            dbManager.Open();
            dbManager.CreateParameters(4);
            dbManager.AddParameters(0, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUES", idperdorues, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDMONEDHA", idmonedha, ParameterDirection.Input);
            dbManager.AddParameters(3, "@DtDok", dtDok, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KLIENTFURNITOR_merrKlienteFurnitoreNdermarrjesAndAutorizimeMonHuajMeGjendje");
            return ds.Tables[0];
        }

        internal string merrVeprimTeFunditKlientFurnitor(int idnderm, int idKlientFurnitor, DateTime dtDok)
        {//metoda per te marre te gjithe kliente furnitoret ne monedhe te huaj me gjendje

            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDKLIENTFURNITOR", idKlientFurnitor, ParameterDirection.Input);
            dbManager.AddParameters(2, "@DtDok", dtDok, ParameterDirection.Input);
            object obj = dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_KLIENTFURNITOR_merrVeprimTeFunditKlFurnitor");
            try
            {
                return obj.ToString();
            }
            catch
            {
                return "";
            }

        }


        /// <summary>
        /// Ekzekuton prc_T_KLIENTFURNITOR_merrKlienteFurnitoreNdermarrjesAndAutorizime per te marre nje datatable aktive ne DB duke filtruar sipas id-se se ndermarrjes dhe id-se se perdoruesit.
        /// <param name="idnderm">Id e ndermarrjes</param>
        /// <param name="idperdorues">Id e perdoruesit</param>
        /// <returns>Kthen nje datatable</returns>
        /// </summary>
        internal DataTable ktheKlienteFurnitoreAktivNdermarrjesAndAutorizime(int idnderm, int idperdorues)
        {//metoda per te marre te gjithe kliente furnitoret

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUES", idperdorues, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KLIENTFURNITOR_merrKlienteFurnitoreAktivNdermarrjesAndAutorizime");
            return ds.Tables[0];

        }
        internal DataTable ktheKlienteFurnitoreAktivNdermarrjesAndAutorizimeGjendje(int idnderm, int idperdorues, DateTime dtdok)
        {//metoda per te marre te gjithe kliente furnitoret

            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUES", idperdorues, ParameterDirection.Input);
            dbManager.AddParameters(2, "@DtDok", dtdok, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KLIENTFURNITOR_merrKlienteFurnitoreAktivNdermarrjesAndAutorizimeGjendje");
            return ds.Tables[0];
        }

        internal DataTable ktheKlienteFurnitoreAktivNdermarrjesAndAutorizimeGjendjeMonLlogDheMonBaze(int idnderm, int idperdorues, DateTime dtdok)
        {//metoda per te marre te gjithe kliente furnitoret

            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUES", idperdorues, ParameterDirection.Input);
            dbManager.AddParameters(2, "@DtDok", dtdok, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KLIENTFURNITOR_merrKlienteFurnitoreAktivNdermarrjesAndAutorizimeGjendjeMonLlogDheMonBaze");
            return ds.Tables[0];
        }

        /// <summary>
        /// Ekzekuton prc_T_KLIENTFURNITOR_merrKlienteFurnitoreNdermarrjesAndAutorizimeLike per te marre nje datatable ne DB duke filtruar sipas id-se se ndermarrjes, id-se se perdoruesit dhe kodit te klientit/furnitorit.
        /// <param name="idNdermarje">Id e ndermarrjes</param>
        /// <param name="idperdorues">Id e perdoruesit</param>
        /// <param name="kodKF">Kodi i klientit/furnitorit (ne DB filtrohet me "like")</param>
        /// <param name="tipKlientFurnitor">0 - kerkimi behet per te dy; 1- kerkimi behet per klient; 2- kerkimi behet per furnitor</param>
        /// <returns>Kthen nje datatable</returns>
        /// </summary>
        internal DataTable ktheKlienteFurnitoreNdermarrjesAndAutorizimeLike(int idNdermarje, int idperdorues, string kodKF, int tipKlientFurnitor)
        {// kthen gjithe llogarite  te kesaj ndermarje dhe autorizimet per perdoruesin

            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@IDNDERMARJE", idNdermarje, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUES", idperdorues, ParameterDirection.Input);
            dbManager.AddParameters(2, "@KODKLIENTFURNITOR", kodKF, ParameterDirection.Input);
            DataSet ds;
            switch (tipKlientFurnitor)
            {
                case 0:
                    ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KLIENTFURNITOR_merrKlienteFurnitoreNdermarrjesAndAutorizimeLike");
                    break;
                case 1:
                    ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KLIENTFURNITOR_merrKlienteNdermarrjesAndAutorizimeLike");
                    break;
                case 2:
                    ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KLIENTFURNITOR_merrFurnitorNdermarrjesAndAutorizimeLike");
                    break;
                default: throw new Exception("Tipi i klientFurnitor: " + tipKlientFurnitor + "; eshte dhene gabim");
            }
            return ds.Tables[0];
        }

        internal DataTable KtheKlienteFurnitoreSipasAutorizimeveLike(int idNdermarje, int idperdorues, string kodKF, int tipKlientFurnitor, int idKlientFurnitorKryesor)
        {
            dbManager.Open();
            dbManager.CreateParameters(5);
            dbManager.AddInputParameters("@IDNDERMARJE", idNdermarje);
            dbManager.AddInputParameters("@IDPERDORUES", idperdorues);
            dbManager.AddInputParameters("@KODKLIENTFURNITOR", kodKF);
            dbManager.AddInputParameters("@LLOJI", tipKlientFurnitor);
            dbManager.AddInputParameters("@IDKLIENTFURNITORKRYESOR", idKlientFurnitorKryesor);
            return dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KLIENTFURNITOR_KtheKlienteFurnitoreSipasAutorizimeveLike").Tables[0];
        }

        /// <summary>
        /// Ekzekuton prc_T_KLIENTFURNITOR_merrKlienteFurnitoreNdermarrjesAndAutorizimeLike per te marre nje datatable ne DB duke filtruar sipas id-se se ndermarrjes, id-se se perdoruesit dhe kodit te klientit/furnitorit.
        /// <param name="idNdermarje">Id e ndermarrjes</param>
        /// <param name="idperdorues">Id e perdoruesit</param>
        /// <param name="kodKF">nje string qe permban listen e kodeve te klienteve</param>
        /// <returns>Kthen nje datatable</returns>
        /// </summary>
        internal DataTable ktheKlienteFurnitoreNdermarrjesAndAutorizimeIn(int idNdermarje, int idperdorues, string kodKF)
        {// kthen gjithe llogarite  te kesaj ndermarje dhe autorizimet per perdoruesin

            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@IDNDERMARJE", idNdermarje, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUES", idperdorues, ParameterDirection.Input);
            dbManager.AddParameters(2, "@KODKLIENTFURNITOR", kodKF, ParameterDirection.Input);

            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KLIENTFURNITOR_merrKlienteFurnitoreNdermarrjesAndAutorizimeIn");
            return ds.Tables[0];
        }
        
        internal DataRow merrKlientFurnitorLikeKodi(string kodKf, int idnderm)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@KODKLIENTFURNITOR", kodKf, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KLIENTFURNITOR_ktheKlientFurnitorLikeKodi");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];
        }
        /// <summary>
        /// Ekzekuton prc_T_KLIENTFURNITOR_ktheKlientFurnitor per te marre nje datarow ne DB duke filtruar sipas id-se se ndermarrjes dhe kodit te klientit/furnitorit.
        /// <param name="idnderm">Id e ndermarrjes</param>
        /// <param name="nr">Kodi i klientit/furnitorit</param>
        /// <returns>Kthen nje datarow</returns>
        /// </summary>
        internal DataRow merrKlientFurnitor(string kodKf, int idnderm)
        {//kthen klient furnitorin sipas nr

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@KODKLIENTFURNITOR", kodKf, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KLIENTFURNITOR_ktheKlientFurnitor");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];
        }
        internal DataRow merrKlientFurnitor(string kodKf, int idnderm, int idperdorues)
        {
            //kthen klient furnitorin sipas nr

            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@KODKLIENTFURNITOR", kodKf, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDPERDORUES", idperdorues, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KLIENTFURNITOR_ktheKlientFurnitorMeAutorizim");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];
        }

        internal string merrKodKlientFurnitorSipasId(int idKlientFurnitor)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDKLIENTFURNITOR", idKlientFurnitor, ParameterDirection.Input);
            return Convert.ToString(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_KLIENTFURNITOR_ktheKodKlientFurnitorSipasId"));
        }

        /// <summary>
        /// Ekzekuton prc_T_KLIENTFURNITOR_ktheIdKlientFurnitorSipasKodit per te marre nje int id e klient furnitorit ne DB duke filtruar sipas id-se se ndermarrjes dhe kodit te klientit/furnitorit.
        /// <param name="idnderm">Id e ndermarrjes</param>
        /// <param name="kodKf">Kodi i klientit/furnitorit</param>
        /// <returns>Kthen id e klient furnitorit</returns>
        /// </summary>
        internal int merrIDKlientFurnitorSipasKodit(string kodKf, int idnderm)
        {//kthen klient furnitorin sipas nr
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@KODKLIENTFURNITOR", kodKf, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            return Convert.ToInt32(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_KLIENTFURNITOR_ktheIdKlientFurnitorSipasKodit"));
        }

        internal string merrKodKlientiSipasKodIntegrimi(string kodIntegrimi , int idnderm)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@KodIntegrimi", kodIntegrimi, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KLIENTFURNITOR_ktheIdKlientFurnitorSipasKodIntegrimi");
            if (ds == null)
                return "";
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return "";
            return ds.Tables[0].Rows[0]["KODKLIENTFURNITOR"].ToString(); 
        }
        internal string merrEmertiminKF(string kodKf, int idnderm)
        {//kthen klient furnitorin sipas nr

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@KODKLIENTFURNITOR", kodKf, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KLIENTFURNITOR_ktheKlientFurnitor");
            if (ds == null)
                return "";
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return "";
            string emertimiKF;
            emertimiKF = ds.Tables[0].Rows[0]["EMERTIMIKF"].ToString();
            return emertimiKF;
        }
        internal decimal merrDetyrimKF(int idkf, DateTime data)
        {//kthen klient furnitorin sipas nr
            decimal detyrimi = 0;

            dbManager.Open();

            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@IDKF", idkf, ParameterDirection.Input);
            dbManager.AddParameters(1, "@DTDOKUMENTI", data, ParameterDirection.Input);
            dbManager.AddParameters(2, "@GJENDJA", detyrimi, ParameterDirection.Output);
            return Convert.ToDecimal(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_KLIENTFURNITOR_ktheGjendjenkf"));
            //detyrimi = decimal.Parse(dbManager.Parameters[2].Value.ToString());
            //return detyrimi;
        }

        internal DataRow merrDetyrimKFMeparshem(int idkf, int idKokaShitje, DateTime data)
        {
            dbManager.Open();

            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@IDKF", idkf, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDKOKASHITJE", idKokaShitje, ParameterDirection.Input);
            dbManager.AddParameters(2, "@DTDOKUMENTI", data, ParameterDirection.Input);

            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KLIENTFURNITOR_ktheGjendjenkfMeparshem");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];

            //detyrimi = decimal.Parse(dbManager.Parameters[2].Value.ToString());
            //return detyrimi;
        }

        internal decimal merrDetyrimKFMonBaze(int idkf, DateTime data)
        {//kthen klient furnitorin sipas nr
            decimal detyrimimonbaze = 0;

            dbManager.Open();

            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@IDKF", idkf, ParameterDirection.Input);
            dbManager.AddParameters(1, "@DTDOKUMENTI", data, ParameterDirection.Input);
            dbManager.AddParameters(2, "@GJENDJA", detyrimimonbaze, ParameterDirection.Output);
            return Convert.ToDecimal(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_KLIENTFURNITOR_ktheGjendjenkfMonBaze"));

        }
        /// <summary>
        /// Ekzekuton prc_T_KLIENTFURNITOR_ktheKlientFurnitorID per te marre nje datarow ne DB duke filtruar sipas ID-se se klientit/furnitorit.
        /// <param name="idKlientFurnitor">id e klient furnitorit</param>
        /// <returns>Kthen nje datarow</returns>
        /// </summary>
        internal DataRow merrKlientFurnitorSipasID(int idKlientFurnitor)
        {//kthen klient furnitorin sipas nr

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDKLIENTFURNITOR", idKlientFurnitor, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KLIENTFURNITOR_ktheKlientFurnitorID");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];
            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="idKlientFurnitor"></param>
        /// <returns></returns>


        internal DataRow merrKlientFurnitorSipasPerqindjes(int idKlientFurnitor)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDKF", idKlientFurnitor, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KLIENTFURNITOR_ktheKlientFurnitorSipasPerqindjes");
            return ds.Tables[0].Rows[0];

        }






        internal DataTable merrKlientFurnitorSipasIDPaAutorizim(int idKlientFurnitor)
        {//kthen klient furnitorin sipas nr

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDKLIENTFURNITOR", idKlientFurnitor, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KLIENTFURNITOR_ktheKlientFurnitorID");

            return ds.Tables[0];
        }
        internal DataTable merrKlientFurnitorSipasID(int idKlientFurnitor, int idNderm, int idPerdoruesi)
        {//kthen klient furnitorin sipas nr

            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@IDKLIENTFURNITOR", idKlientFurnitor, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idNderm, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDPERDORUES", idPerdoruesi, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KLIENTFURNITOR_ktheKlientFurnitorIDMeAutorizim");
            //if (ds == null)
            //    return null;
            //if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
            //    return null;
            return ds.Tables[0];
        }

        internal DataRow merrKlientFurnitorSipasKoditDheNdermarjes(String kodi, int idndermarje, int idPerdorues)
        {
            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@KODI", kodi, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idndermarje, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDPERDORUES", idPerdorues, ParameterDirection.Input);

            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KLIENTFURNITOR_ktheKlientFurnitorKodNdermarje");
            //if (ds == null)
            //    return null;
            //if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
            //    return null;
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];
        }


        /// <summary>
        /// Ekzekuton prc_T_KLIENTFURNITOR_ekzistonKlientFurnitor per te kontrolluar nese ekziston nje objekt clsKlientFurnitor ne DB duke filtruar sipas id-se se ndermarrjes dhe kodit te klientit/furnitorit.
        /// <param name="kodKF">kodi i klient furnitorit</param>
        /// <param name="idNdermarrja">id e ndermarrjes</param>
        /// <returns>Kthen true nese ekziston nje objekt qe ploteson kushtet</returns>
        /// </summary>
        internal bool ekzistonKlientFurnitor(string kodKF, int idNdermarrja)
        {//kontrollon nqs ekziston nje klientfurnitor me kete numer

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@KODKLIENTFURNITOR", kodKF, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idNdermarrja, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KLIENTFURNITOR_ekzistonKlientFurnitor");
            if (ds.Tables[0].Rows.Count >= 1)
                return true;
            return false;
        }
        internal bool ekzistonKlientFurnitorPerKeteNdermarjeBij(int idndermarjebij, int idNdermarrja)
        {//kontrollon nqs ekziston nje klientfurnitor me kete numer

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDNDERMARJEBIJ", idndermarjebij, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idNdermarrja, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KLIENTFURNITOR_ekzistonKlientFurnitorNdermarjeBij");
            if (ds.Tables[0].Rows.Count >= 1)
                return true;
            return false;
        }
        internal bool ekzistonKlientFurnitorMeKeteNipt(string niptKF,string klient, int idNdermarja)
        {
            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@NIPTKF", niptKF, ParameterDirection.Input);
            dbManager.AddParameters(1, "@KLIENTFURNITOR", klient, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDNDERMARJE", idNdermarja, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KLIENTFURNITOR_ekzistonNIPT");
            if (ds.Tables[0].Rows.Count >= 1)
                return true;
            return false;
        }
        internal DataRow ktheKFNdermarrjesAndAutorizimeDR(int idnderm, int idperdorues, int IDKF)
        {
            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUES", idperdorues, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDKLIENTFURNITOR", IDKF, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KLIENTFURNITOR_ktheKFNdermarrjesAndAutorizimeDR");

            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];
        }

        internal DataTable ktheKFNdermarrjesAndAutorizimeDT(int idnderm, int idperdorues)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUES", idperdorues, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KLIENTFURNITOR_ktheKFNdermarrjesAndAutorizimeDT");

            return ds.Tables[0];
        }

        internal DataTable ktheKFNdermarrjesAndAutorizimeDTFiltered(int idnderm, int idperdorues, int idKonfig, string filter, string topRows, string sortOrder)
        {
            dbManager.Open();
            dbManager.CreateParameters(6);
            dbManager.AddParameters(0, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUES", idperdorues, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDKONFIGAMBJENTE", idKonfig, ParameterDirection.Input);
            dbManager.AddParameters(3, "@FILTER", filter, ParameterDirection.Input);
            dbManager.AddParameters(4, "@TOPROWS", topRows, ParameterDirection.Input);
            dbManager.AddParameters(5, "@SORTORDER", sortOrder, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KLIENTFURNITOR_ktheKFNdermarrjesAndAutorizimeDTFiltered");

            return ds.Tables[0];
        }

        internal DataTable ktheKFNdermarrjesAndAutorizimeDTPerCrm(int idnderm, int idperdorues)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUES", idperdorues, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KLIENTFURNITOR_ktheKFNdermarrjesAndAutorizimeDTPerCRM");

            return ds.Tables[0];
        }
        internal DataTable ktheKFNdermarrjesAndAutorizimeDTExport(int idnderm, int idperdorues, int lloji, string emerTabKoka, string emerFusheID)
        {
            dbManager.Open();
            dbManager.CreateParameters(5);
            dbManager.AddParameters(0, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUES", idperdorues, ParameterDirection.Input);
            dbManager.AddParameters(2, "@LLOJI", lloji, ParameterDirection.Input);
            dbManager.AddParameters(3, "@EMERTABKOKA", emerTabKoka, ParameterDirection.Input);
            dbManager.AddParameters(4, "@EMERFUSHEID", emerFusheID, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KLIENTFURNITOR_ktheKFNdermarrjesAndAutorizimeDTExport");

            return ds.Tables[0];
        }

        internal DataTable ktheKFNdermarrjesAndAutorizimeDTLupe(int idnderm, int idperdorues, bool meProspekt, int idKonfig, string filter, string topRows)
        {
            dbManager.Open();
            dbManager.CreateParameters(6);
            dbManager.AddParameters(0, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUES", idperdorues, ParameterDirection.Input);
            dbManager.AddParameters(2, "@MEPROSPEKT", meProspekt, ParameterDirection.Input);
            dbManager.AddParameters(3, "@IDKONFIGAMBJENTE", idKonfig, ParameterDirection.Input);
            dbManager.AddParameters(4, "@FILTER", filter, ParameterDirection.Input);
            dbManager.AddParameters(5, "@TOPROWS", topRows, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KLIENTFURNITOR_ktheKFNdermarrjesAndAutorizimeDTLupa");
            return ds.Tables[0];
        }

        /// <summary>
        /// Ekzekuton prc_T_KLIENTFURNITOR_merrKlienteSipasNdermarrjesDheDatesSeDatelindjes per te marre nje datatable ne DB duke filtruar sipas id-se se ndermarrjes , aktive dhe qe kane datelindjen sipas dates ne parameter.
        /// <param name="idnderm">Id e ndermarrjes</param>
        /// <param name="data">Data e datelindjes</param>
        /// <returns>Kthen nje datatable</returns>
        /// </summary>
        internal DataTable ktheKlientetQeKaneDitelindjenSipasNdermarrjesDheDates(int idnderm, DateTime data)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            dbManager.AddParameters(1, "@DATA", data, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KLIENTFURNITOR_merrKlienteSipasNdermarrjesDheDatesSeDatelindjes");
            return ds.Tables[0];
        }

        internal DataTable KtheKlientFurnitoreVartesSipasIdShitjeKoka(int idShitjeKoka)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddInputParameters("@IDSHITJEKOKA", idShitjeKoka);
            return dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KLIENTFURNITOR_KtheSipasIdShitjeKoka").Tables[0];
        }

        internal DataTable MerrKlienteFurnitoreVartesSipasKodeve(string kodetKlienteFurnitoreVartes, int idNdermarrja)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddInputParameters("@KODET", kodetKlienteFurnitoreVartes);
            dbManager.AddInputParameters("@IDNDERMARRJE", idNdermarrja);
            return dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KLIENTFURNITOR_MerrSipasKodeve").Tables[0];
        }

        internal DataTable MerrKlienteFurnitoreVartesIdve(string ids)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddInputParameters("@IDS", ids);
            return dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KLIENTFURNITOR_MerrSipasIdve").Tables[0];
        }
        /// <summary>
        /// Ekzekuton prc_T_KOKASHITJE_KLIENTVARTES_merrKlienteMeMarreveshjeAktive per te marre nje string me kode klientesh te cilet kane nje marreveshje aktive.
        /// <param name="kodet">string me kodet qe merren nga file, per tu kontrolluar</param>
        /// <param name="dtdok">Data e dokumentit te marreveshjes qe do te rregjistrohet (eshte data default)</param>
        /// <returns>Kthen nje string</returns>
        /// </summary>
        internal string KontrolloKlientMeMarreveshjeAktive(string kodet, DateTime dtdok, string idmarreveshje)
        {
            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddInputParameters("@KODET", kodet);
            dbManager.AddInputParameters("@DTDOK", dtdok);
            dbManager.AddInputParameters("@IDMARREVESHJE", idmarreveshje);
            return Convert.ToString(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_KOKASHITJE_KLIENTVARTES_merrKlienteMeMarreveshjeAktive"));
        }

        #endregion

        /// <summary>
        /// Metodat e meposhtme i perkasin veprimeve qe kryhen nga objektet clsGrupeKF dhe colGrupeKF
        /// </summary>        
        #region GRUPEKF

        /// <summary>
        /// kthen datarow kodifikim klient/furnitor sipas id-se
        /// </summary>
        ///<param name="idkodifikimi"> id e kodifikimit te kf</param>
        ///<returns> nje datarow qe permban kodifikimin e kf sipas id-se se kodifikimit</returns>
        internal DataRow merrKodifikimKF(int idgrupi)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDGRUPI", idgrupi, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_GRUPEKF_ktheKodifikimKFSipasId");

            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];
        }

     

        internal DataRow merrKodifikimKFSipasKodLloj(string kodgrupi, int idndermarje, int llojkodifikimi, int llojkf)
        {

            dbManager.Open();
            dbManager.CreateParameters(4);
            dbManager.AddParameters(0, "@KODGRUPI", kodgrupi, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idndermarje, ParameterDirection.Input);
            dbManager.AddParameters(2, "@LLOJKODIFIKIMI", llojkodifikimi, ParameterDirection.Input);
            dbManager.AddParameters(3, "@LLOJKF", llojkf, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_GRUPEKF_merrKodifikimKF");

            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];


        }
        /// <summary>
        /// ekzekuton prc_T_GRUPEKF_ins ne DB dhe kthen statusin e perfundimit te ekzekutimit te SP-se perkatese
        /// Shiko clsMesazh brenda ketij moduli per informacion te metejshem mbi vleren e kthimit te kesaj metode
        /// <param name="idKodifikimi"> id ritese e kodifikimit</param>
        /// <param name="kodKodifikimi">kodi i kodifikimit</param>
        /// <param name="pershkrimKodifikimi">pershkrimi i kodifikimit</param>
        /// <param name="idPrindi">id e prindit</param>
        /// <param name="nivelKodifikimi"> niveli i kodifikimit</param>
        /// <param name="idPerdoruesi">id e perdoruesit qe ka kryer veprimin</param>
        /// <param name="idNdermarje">id ndermarje</param>
        /// <param name="idstatusdok">id e statusit te kodifikimit</param>
        /// <param name="llojKodifikimi">lloji i kodifikimit, grupimi 1, grupimi 2, apo grupimi 3. </param>
        /// <param name="llojKF">lloji i kf (klient apo furnitor) </param>
        /// <returns> nje objekt clsMesazh qe tregon nese ruajtja eshte kryer ne rregull apo jo.</returns>
        /// </summary>

        internal clsMesazh ruajGrupKF(out int idGrupi, string kodgrup, String pershkrimgrup, int idPrindi, int nivelgrup, int idPerdoruesi, int idNdermarje, int idstatusdok, int llojKodifikimi, int llojKF)
        {
            idGrupi = 0;          
            dbManager.Open();            
            dbManager.CreateParameters(10);            
            dbManager.AddParameters(0, "@KODGRUPI", kodgrup, ParameterDirection.Input);
            dbManager.AddParameters(1, "@PERSHKRIMGRUPI", pershkrimgrup, ParameterDirection.Input);
            if (idPrindi == 0)
                dbManager.AddParameters(2, "@IDPRINDI", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(2, "@IDPRINDI", idPrindi, ParameterDirection.Input);
            dbManager.AddParameters(3, "@NIVELGRUPI", nivelgrup, ParameterDirection.Input);
            dbManager.AddParameters(4, "@IDPERDORUESI", idPerdoruesi, ParameterDirection.Input);
            dbManager.AddParameters(5, "@IDNDERMARJE", idNdermarje, ParameterDirection.Input);
            dbManager.AddParameters(6, "@IDSTATUSDOK", idstatusdok, ParameterDirection.Input);
            dbManager.AddParameters(7, "@LLOJKODIFIKIMI", llojKodifikimi, ParameterDirection.Input);
            dbManager.AddParameters(8, "@LLOJKF", llojKF, ParameterDirection.Input);
            dbManager.AddParameters(9, "@IDGRUPI", idGrupi, ParameterDirection.Output);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_GRUPEKF_ins");
            idGrupi = int.Parse(dbManager.Parameters[9].Value.ToString());
            return new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
        }

        /// <summary>
        /// ekzekuton prc_T_GRUPEKF_upd ne DB dhe kthen statusin e perfundimit te ekzekutimit te SP-se perkatese
        /// Shiko clsMesazh brenda ketij moduli per informacion te metejshem mbi vleren e kthimit te kesaj metode
        /// <param name="idKodifikimi"> id ritese e kodifikimit</param>
        /// <param name="kodKodifikimi">kodi i kodifikimit</param>
        /// <param name="pershkrimKodifikimi">pershkrimi i kodifikimit</param>
        /// <param name="idPrindi">id e prindit</param>
        /// <param name="nivelKodifikimi"> niveli i kodifikimit</param>
        /// <param name="idPerdoruesi">id e perdoruesit qe ka kryer veprimin</param>
        /// <param name="idNdermarje">id ndermarje</param>
        /// <param name="idstatusdok">id e satusit te kodifikimit</param>
        /// <param name="llojKodifikimi">lloji i kodifikimit, grupimi 1, grupimi 2, apo grupimi 3. </param>
        /// <param name="llojKF">lloji i kf (klient apo furnitor) </param>
        /// <returns> nje objekt clsMesazh qe tregon nese modifikimi eshte kryer ne rregull apo jo.</returns>
        /// </summary>
        internal clsMesazh modifikoGrupKF(int idgrup, string kodgrup, String pershkrimgrup, int idPrindi, int nivelgrup, int idPerdoruesi, int idNdermarje, int idstatusdok, int llojKodifikimi, int llojkf)
        {
            dbManager.Open();
            clsMesazh mesazh = new clsMesazh();
            //shtimi i parametrave

            dbManager.CreateParameters(10);
            dbManager.AddParameters(0, "@IDGRUPI", idgrup, ParameterDirection.Input);
            dbManager.AddParameters(1, "@KODGRUPI", kodgrup, ParameterDirection.Input);
            dbManager.AddParameters(2, "@PERSHKRIMGRUPI", pershkrimgrup, ParameterDirection.Input);
            if (idPrindi == 0) dbManager.AddParameters(3, "@IDPRINDI", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(3, "@IDPRINDI", idPrindi, ParameterDirection.Input);
            dbManager.AddParameters(4, "@NIVELGRUPI", nivelgrup, ParameterDirection.Input);
            dbManager.AddParameters(5, "@IDPERDORUESI", idPerdoruesi, ParameterDirection.Input);
            dbManager.AddParameters(6, "@IDNDERMARJE", idNdermarje, ParameterDirection.Input);
            dbManager.AddParameters(7, "@IDSTATUSDOK", idstatusdok, ParameterDirection.Input);
            dbManager.AddParameters(8, "@LLOJKODIFIKIMI", llojKodifikimi, ParameterDirection.Input);
            dbManager.AddParameters(9, "@LLOJKF", llojkf, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_GRUPEKF_upd");
            mesazh = new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);

            return mesazh;
        }

        /// <summary>
        /// ekzekuton prc_T_GRUPEKF_upddel ne DB dhe kthen statusin e perfundimit te ekzekutimit te SP-se perkatese
        /// Shiko clsMesazh brenda ketij moduli per informacion te metejshem mbi vleren e kthimit te kesaj metode
        /// <param name="idKodifikimi"> id ritese e kodifikimit</param>
        /// <param name="idPerdoruesi">id e perdoruesit qe ka kryer veprimin</param>
        /// <returns> nje objekt clsMesazh qe tregon nese fshirja eshte kryer ne rregull apo jo.</returns>
        /// </summary>

        internal clsMesazh fshiGrupKF(int idgrup, int idperdoruesi)
        {
            dbManager.Open();
            //shtimi i parametrave
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDGRUPI", idgrup, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUESI", idperdoruesi, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_GRUPEKF_upddel");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
            return mesazh;
        }

        internal DataTable merrGrupKFSipasLlojit(int llojkodifikimi, int idndermarje)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@LLOJKODIFIKIMI", llojkodifikimi, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idndermarje, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_GRUPEKF_ktheKodifikimKFSipasLlojit");

            return ds.Tables[0];
        }

        internal DataTable merrGrupKFSipasLlojitKodifikimDheLlojKF(int llojkodifikimi, int idndermarje, int llojkf)
        {
            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@LLOJKODIFIKIMI", llojkodifikimi, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idndermarje, ParameterDirection.Input);
            dbManager.AddParameters(2, "@LLOJIKF", llojkf, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_GRUPEKF_ktheKodifikimKFSipasLlojitKodifikimdheLlojiKF");

            return ds.Tables[0];
        }

        internal DataTable merrGrupKFSipasLlojitKF(int idndermarje, int llojkf)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDNDERMARJE", idndermarje, ParameterDirection.Input);
            dbManager.AddParameters(1, "@LLOJIKF", llojkf, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_GRUPEKF_ktheKodifikimKFSipasLlojitKF");

            return ds.Tables[0];
        }

        internal DataTable ktheGjitheGrupetKFSipasNdermarrjesJoPrind(int idndermarje, int llojkodifikim)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDNDERMARJE", idndermarje, ParameterDirection.Input);
            dbManager.AddParameters(1, "@llojGrup", llojkodifikim, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_GRUPEKF_merrSipasNdermarrjesJoPrind");

            return ds.Tables[0];
        }

        internal DataTable mbushGjitheGrupetKFSipasNdermarrjesJoPrindDheLlojit(int idndermarje, int llojkodifikimi, bool llojikf)
        {
            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@IDNDERMARJE", idndermarje, ParameterDirection.Input);
            dbManager.AddParameters(1, "@LLOJKODIFIKIMI", llojkodifikimi, ParameterDirection.Input);
            dbManager.AddParameters(2, "@llojikf", llojikf, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_GRUPEKF_merrSipasNdermarrjesJoPrindDheLloji");
            return ds.Tables[0];
        }

        internal DataTable ktheBijte(int idgrupi, int idNdermarje)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDGRUPI", idgrupi, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idNdermarje, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_GRUPEKF_ktheBij");

            return ds.Tables[0];
        }
        
        public bool eshtePrindKF(int idgrupi, int idNdermarje)
        {

            int nrbijsh = 0;

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDGRUPI", idgrupi, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idNdermarje, ParameterDirection.Input);
            nrbijsh = Convert.ToInt32(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_GRUPEKF_eshtePrind"));
            return Convert.ToBoolean(nrbijsh);
        }

        public bool ekzistonGrupKFSipasKodLloje(String kod, int idndermarje, int llojkodifikimi, int llojkf)
        {//kontrollon nqs ekziston nje kodifikim artikulli me kete kod

            dbManager.Open();
            dbManager.CreateParameters(4);
            dbManager.AddParameters(0, "@KODGRUPI", kod, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idndermarje, ParameterDirection.Input);
            dbManager.AddParameters(2, "@LLOJKODIFIKIMI", llojkodifikimi, ParameterDirection.Input);
            dbManager.AddParameters(3, "@LLOJKF", llojkf, ParameterDirection.Input);
            int pergjigje = Convert.ToInt32(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_GRUPEKF_ekzistonGrupKFSipasKodLloji"));
            return Convert.ToBoolean(pergjigje);
        }

        public bool ekzistonGrupKFSipasKodGrupi( String kodGrupi, int idNdermarje, int llojKodifikimi)
        {//kontrollon nqs ekziston nje kodifikim artikulli me kete kod

            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@KODGRUPI", kodGrupi, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idNdermarje, ParameterDirection.Input);
            dbManager.AddParameters(2, "@LLOJKODIFIKIMI", llojKodifikimi, ParameterDirection.Input);
            int pergjigje = Convert.ToInt32(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_GRUPEKF_ekzistonGrupKFSipasKodGrupi"));
            return Convert.ToBoolean(pergjigje);
        }

        public bool kaVeprimeGrupKF(int id)
        {//kontrollon nqs ka veprime me kete kodifikim artikulli

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDGRUPI", id, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_GRUPEKF_kaVeprime");
            if (ds.Tables[0].Rows.Count == 1)
                return true;
            else if (ds.Tables[0].Rows.Count == 0)
                return false;
            else return true;
        }
        #endregion
        

        /// <summary>
        /// Metodat e meposhtme i perkasin veprimeve qe kryhen nga objektet clsKokaSkemaFleteKontabel dhe colKokatSkematFletetKontabel
        /// </summary>
        #region  KOKA SKEMA FLETE KONTABEL

        /// <summary>
        /// Ekzekuton prc_T_KOKASKEMAFLETEKONTABEL_ins per te ruajtur nje objekt clsKokaSkemaFleteKontabel ne DB.
        /// <param name="idKokaSkemaFK">Id qe gjenerohet automatikisht</param>
        /// <param name="kodiKokaSkemaFK">Kodi</param>
        /// <param name="pershkrimiKokaSkemaFK">Pershkrimi</param>
        /// <param name="idPerdoruesi">Id e perdoruesit</param>
        /// <param name="idNderViti">Id lidhese ndermarrje - vit</param>
        /// <returns> Kthen statusin e perfundimit te ekzekutimit te SP-se perkatese (true nese veprimi eshte kryer me sukses)</returns>
        /// </summary>
        internal int ruajKokaSkemaFleteKontabel(int idKokaSkemaFK, string kodiKokaSkemaFK, string pershkrimiKokaSkemaFK, int idPerdoruesi, int idNdermarje, int idstatusdok)
        { //metoda per ruajtjen e  koken e skemes fletes kontabel

            dbManager.Open();
            //shtimi i parametrave
            dbManager.CreateParameters(6);
            dbManager.AddParameters(0, "@IDKOKASKEMAFK", idKokaSkemaFK, ParameterDirection.Output);
            dbManager.AddParameters(1, "@KODIKOKASKEMAFK", kodiKokaSkemaFK, ParameterDirection.Input);
            dbManager.AddParameters(2, "@PERSHKRIMIKOKASKEMAFK", pershkrimiKokaSkemaFK, ParameterDirection.Input);
            dbManager.AddParameters(3, "@IDPERDORUESI", idPerdoruesi, ParameterDirection.Input);
            dbManager.AddParameters(4, "@IDNDERMARJE", idNdermarje, ParameterDirection.Input);
            dbManager.AddParameters(5, "@IDSTATUSDOK", idstatusdok, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_KOKASKEMAFLETEKONTABEL_ins");

            idKokaSkemaFK = int.Parse(dbManager.Parameters[0].Value.ToString());
            return idKokaSkemaFK;

        }


        /// <summary>
        /// Ekzekuton prc_T_KOKASKEMAFLETEKONTABEL_upd per te modifikuar nje objekt clsKokaSkemaFleteKontabel ne DB.
        /// <param name="idKokaSkemaFK">Id qe gjenerohet automatikisht</param>
        /// <param name="kodiKokaSkemaFK">Kodi</param>
        /// <param name="pershkrimiKokaSkemaFK">Pershkrimi</param>
        /// <param name="idPerdoruesi">Id e perdoruesit</param>
        /// <param name="idNderViti">Id lidhese ndermarrje - vit</param>
        /// <returns> Kthen statusin e perfundimit te ekzekutimit te SP-se perkatese (true nese veprimi eshte kryer me sukses)</returns>
        /// </summary>
        internal clsMesazh modifikoKokaSkemaFleteKontabel(int idKokaSkemaFK, string kodiKokaSkemaFK, string pershkrimiKokaSkemaFK, int idPerdoruesi, int idNdermarje, int idstatusdok)
        {//metoda per modifikimin e  kokes se SKEMES fletes kontabel 

            dbManager.Open();
            //shtimi i parametrave
            dbManager.CreateParameters(6);
            dbManager.AddParameters(0, "@IDKOKASKEMAFK", idKokaSkemaFK, ParameterDirection.Input);
            dbManager.AddParameters(1, "@KODIKOKASKEMAFK", kodiKokaSkemaFK, ParameterDirection.Input);
            dbManager.AddParameters(2, "@PERSHKRIMIKOKASKEMAFK", pershkrimiKokaSkemaFK, ParameterDirection.Input);
            dbManager.AddParameters(3, "@IDPERDORUESI", idPerdoruesi, ParameterDirection.Input);
            dbManager.AddParameters(4, "@IDNDERMARJE", idNdermarje, ParameterDirection.Input);
            dbManager.AddParameters(5, "@IDSTATUSDOK", idstatusdok, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_KOKASKEMAFLETEKONTABEL_upd");
            idKokaSkemaFK = int.Parse(dbManager.Parameters[0].Value.ToString());
            return new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);


        }

        /// <summary>
        /// Ekzekuton prc_T_KOKASKEMAFLETEKONTABEL_del per te fshire nje objekt clsKokaSkemaFleteKontabel ne DB.
        /// <param name="idKokaSkemaFK">Id qe gjenerohet automatikisht</param>
        /// <returns> Kthen statusin e perfundimit te ekzekutimit te SP-se perkatese (true nese veprimi eshte kryer me sukses)</returns>
        /// </summary>
        internal clsMesazh fshiKokaSkemaFleteKontabel(int idKokaSkemaFK)
        {//metoda per fshirjen e kokes se fletes kontabel 

            dbManager.Open();
            //shtimi i parametrave
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDKOKASKEMAFK", idKokaSkemaFK, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_KOKASKEMAFLETEKONTABEL_del");
            return new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
        }

        /// <summary>
        /// Ekzekuton prc_T_KOKASKEMAFLETEKONTABEL_del per te fshire nje objekt clsKokaSkemaFleteKontabel ne DB.
        /// <param name="idKokaSkemaFK">Id qe gjenerohet automatikisht</param>
        /// <returns> Kthen statusin e perfundimit te ekzekutimit te SP-se perkatese (true nese veprimi eshte kryer me sukses)</returns>
        /// </summary>
        internal clsMesazh fshiStatusKokaSkemaFleteKontabel(int idKokaSkemaFK, int idperdoruesi, ResourceManager rm, CultureInfo ci)
        {//metoda per fshirjen e kokes se fletes kontabel 

            dbManager.Open();
            //shtimi i parametrave
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDKOKASKEMAFK", idKokaSkemaFK, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUESI", idperdoruesi, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_KOKASKEMAFLETEKONTABEL_upddel");
            return new clsMesazh(true, rm.GetString("msgFshirjaPerfundoiMeSukses", ci));

        }
        /// <summary>
        /// Ekzekuton prc_T_KOKASKEMAFLETEKONTABEL_merrGjitheSkematKontabelAndAutorizime per te marre nje datatable duke filtruar sipas ID-se lidhese ndermarrje-vit dhe ID-se se perdoruesit.
        /// <param name="idNdermarje">Id lidhese ndermarrje - vit</param>
        /// <param name="idperdoruesi">Id e perdoruesit</param>
        /// <returns> Kthen nje datatable qe plotesojne kushtin</returns>
        /// </summary>
        internal DataTable ktheGjitheSkematFletetKontabelSipasNdermarjesAndAutorizimit(int idNdermarje, int idperdoruesi)
        {//metoda per te marre te gjithe kokat e skemave fletekontabel sipas ndermarjes dhe autorizimit

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDNDERMARJE", idNdermarje, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUESI", idperdoruesi, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KOKASKEMAFLETEKONTABEL_merrGjitheSkematKontabelAndAutorizime");
            return ds.Tables[0];

        }

        /// <summary>
        /// Ekzekuton prc_T_KOKASKEMAFLETEKONTABEL_merrSkemeFKSipasKodit per te marre nje datarow duke filtruar sipas kodit te kokes se skemes.
        /// <param name="kodi">Kodi i kokes se skemes</param>
        /// <returns> Kthen nje datarow qe ploteson kushtin</returns>
        /// </summary>
        internal DataRow ktheKokaSkemaFleteKontabelSipasKodit(String kodi, int idndermarje)
        {//metoda per te marre koka flete kontabel sipas kodit

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@KODIKOKASKEMAFK", kodi, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idndermarje, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KOKASKEMAFLETEKONTABEL_merrSkemeFKSipasKodit");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];

        }


        /// <summary>
        /// Ekzekuton prc_T_KOKASKEMAFLETEKONTABEL_merrSkemeFKSipasId per te marre nje datarow duke filtruar sipas ID-se se kokes se skemes.
        /// <param name="id">Id e kokes se skemes</param>
        /// <returns> Kthen nje datarow qe ploteson kushtin</returns>
        /// </summary>
        internal DataRow ktheKokaSkemaFleteKontabelSipasID(int id)
        {//metoda per te marre koka flete kontabel sipas kodit

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDKOKASKEMAFK", id, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KOKASKEMAFLETEKONTABEL_merrSkemeFKSipasId");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];

        }

        /// <summary>
        /// Ekzekuton prc_T_KOKASKEMAFLETEKONTABEL_ekzistonKokaSkemaFK per te kontrolluar nese ekziston nje objekt clsKokaSkemaFleteKontabel ne DB me kodin qe kalohet si parameter.
        /// <param name="kodi">Kodi i skemes kontabel</param>
        /// <returns> Kthen true nese ekziston nje objekt clsKokaSkemaFleteKontabel qe ploteson kushtin</returns>
        /// </summary>
        public bool ekzistonKokaSkemaFleteKontabel(String kodi, int idndermarje)
        {//kontrollon nese ekziston nje koka flete kontabel me kete kod

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@KODIKOKASKEMAFK", kodi, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idndermarje, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "[prc_T_KOKASKEMAFLETEKONTABEL_ekzistonKokaSkemaFK]");
            if (ds.Tables[0].Rows.Count == 1)
                return true;
            else if (ds.Tables[0].Rows.Count == 0)
                return false;
            else return true;
        }

        #endregion

        /// <summary>
        /// Metodat e meposhtme i perkasin veprimeve qe kryhen nga objektet clsTrupiSkemaFleteKontabel dhe colTrupatSkematFletetKontabel
        /// </summary>
        #region  TRUPI SKEMA FLETE KONTABEL

        /// <summary>
        /// Ekzekuton prc_T_TRUPISKEMAFLETEKONTABEL_ins per te ruajtur nje objekt clsTrupiSkemaFleteKontabel ne DB.
        /// <param name="idTrupiSkemaFK">id e trupit te skemes FK</param>
        /// <param name="idKokaSkemaFK">id e kokes qe ka ate trup</param>
        /// <param name="idLlojDokSkemaFK">id e llojit te dokumentit</param>
        /// <param name="idLlogari">id e llogarise</param>
        /// <param name="debiKrediSkemaFK">nese eshte debi ose kredi skema</param>
        /// <returns> Kthen statusin e perfundimit te ekzekutimit te SP-se perkatese (true nese veprimi eshte kryer me sukses)</returns>
        /// </summary>
        internal clsMesazh ruajTrupiSkemaFleteKontabel(out int idTrupiSkemaFK, int idKokaSkemaFK, int idLlogari, string pershkrimi, int idmonedha, double kursi, double vleftadebi, double vleftakredi, double vleftamondebi, double vleftamonkredi)
        { //metoda per ruajtjen e  trupit skema flete kontabel
            idTrupiSkemaFK = -1;

            dbManager.Open();
            //shtimi i parametrave
            dbManager.CreateParameters(10);
            dbManager.AddParameters(0, "@IDTRUPISKEMAFK", idTrupiSkemaFK, ParameterDirection.Output);
            dbManager.AddParameters(1, "@IDKOKASKEMAFK", idKokaSkemaFK, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDLLOGARI", idLlogari, ParameterDirection.Input);
            dbManager.AddParameters(3, "@PERSHKRIMI", pershkrimi, ParameterDirection.Input);
            dbManager.AddParameters(4, "@IDMONEDHA", idmonedha, ParameterDirection.Input);
            dbManager.AddParameters(5, "@KURSI", kursi, ParameterDirection.Input);
            dbManager.AddParameters(6, "@VLEFTADEBI", vleftadebi, ParameterDirection.Input);
            dbManager.AddParameters(7, "@VLEFTAKREDI", vleftakredi, ParameterDirection.Input);
            dbManager.AddParameters(8, "@VLEFTADEBIMON", vleftamondebi, ParameterDirection.Input);
            dbManager.AddParameters(9, "@VLEFTAKREDIMON", vleftamonkredi, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_TRUPISKEMAFLETEKONTABEL_ins");
            idTrupiSkemaFK = int.Parse(dbManager.Parameters[0].Value.ToString());

            return new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
        }

        /// <summary>
        /// Ekzekuton prc_T_TRUPISKEMAFLETEKONTABEL_upd per te modifikuar nje objekt clsTrupiSkemaFleteKontabel ne DB.
        /// <param name="idTrupiSkemaFK">id e trupit te skemes FK</param>
        /// <param name="idKokaSkemaFK">id e kokes qe ka ate trup</param>
        /// <param name="idLlojDokSkemaFK">id e llojit te dokumentit</param>
        /// <param name="idLlogari">id e llogarise</param>
        /// <param name="debiKrediSkemaFK">nese eshte debi ose kredi skema</param>
        /// <returns> Kthen statusin e perfundimit te ekzekutimit te SP-se perkatese (true nese veprimi eshte kryer me sukses)</returns>
        /// </summary>
        internal clsMesazh modifikoTrupiSkemaFleteKontabel(int idTrupiSkemaFK, int idKokaSkemaFK, int idLlogari, string pershkrimi, int idmonedha, double kursi, double vleftadebi, double vleftakredi, double vleftamondebi, double vleftamonkredi)
        {//metoda per modifikimin e  trupi e skemes fleta kontabel
            dbManager.Open();
            dbManager.CreateParameters(10);
            dbManager.AddParameters(0, "@IDTRUPISKEMAFK", idTrupiSkemaFK, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDKOKASKEMAFK", idKokaSkemaFK, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDLLOGARI", idLlogari, ParameterDirection.Input);
            dbManager.AddParameters(3, "@PERSHKRIMI", pershkrimi, ParameterDirection.Input);
            dbManager.AddParameters(4, "@IDMONEDHA", idmonedha, ParameterDirection.Input);
            dbManager.AddParameters(5, "@KURSI", kursi, ParameterDirection.Input);
            dbManager.AddParameters(6, "@VLEFTADEBI", vleftadebi, ParameterDirection.Input);
            dbManager.AddParameters(7, "@VLEFTAKREDI", vleftakredi, ParameterDirection.Input);
            dbManager.AddParameters(8, "@VLEFTADEBIMON", vleftamondebi, ParameterDirection.Input);
            dbManager.AddParameters(9, "@VLEFTAKREDIMON", vleftamonkredi, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_TRUPISKEMAFLETEKONTABEL_upd");
            idTrupiSkemaFK = int.Parse(dbManager.Parameters[0].Value.ToString());

            return new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
        }


        /// <summary>
        /// Ekzekuton prc_T_TRUPISKEMAFLETEKONTABEL_del per te fshire nje objekt clsTrupiSkemaFleteKontabel ne DB.
        /// <param name="idTrupiSkemaFK">id e trupit te skemes FK</param>
        /// <returns> Kthen statusin e perfundimit te ekzekutimit te SP-se perkatese (true nese veprimi eshte kryer me sukses)</returns>
        /// </summary>
        internal clsMesazh fshiTrupiSkemaFleteKontabel(int idTrupiSkemaFK)
        {//metoda per fshirjen e trupi e skemes fleta kontabel

            dbManager.Open();
            //shtimi i parametrave
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDTRUPISKEMAFK", idTrupiSkemaFK, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_TRUPISKEMAFLETEKONTABEL_del");
            return new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
        }

        /// <summary>
        /// Ekzekuton prc_T_TRUPISKEMAFLETEKONTABEL_ktheTrupinESkemesSipasIdKoka per te marre nje collection me objekte clsTrupiSkemaFleteKontabel duke filtruar sipas ID-se se kokes se skemes.
        /// <param name="idKoka">ID e kokes se skemes</param>
        /// <returns> Kthen nje collection me objekte clsTrupiSkemaFleteKontabel qe plotesojne kushtin</returns>
        /// </summary>
        internal DataTable ktheTrupatSkematFletetKontabelSipasKokes(int idKoka)
        {//metoda per te marre trupin skemes flete kontabel sipas id se kokes

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDKOKASKEMAFK", idKoka, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_TRUPISKEMAFLETEKONTABEL_ktheTrupinESkemesSipasIdKoka");
            return ds.Tables[0];
        }


        #endregion

        /// <summary>
        /// Metodat e meposhtme i perkasin veprimeve qe kryhen nga objektet colSkemaKontabelNew, colNenLlojLlogarish, clsLlojLlogarish
        /// </summary>
        #region   SKEMA  KONTABEL NEW
        /// <summary>
        /// Ekzekuton prc_T_SKEMEKONTABILITETI_merrAll per te marre nje datatable duke filtruar sipas ID-se lidhese ndermarrje - vit.
        /// 
        /// <returns> Kthen nje datatable qe plotesojne kushtin</returns>
        /// </summary>
        internal DataTable ktheGjitheSkemaKontabelRegjistrim()
        {
            dbManager.Open();
            //  dbManager.CreateParameters(1);
            // dbManager.AddParameters(0, "@IDNDERVITI", idNdermVit, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_SKEMEKONTABILITETI_merrAll");
            return ds.Tables[0];
        }
        /// <summary>
        /// Ekzekuton prc_T_SKEMEKONTABILITETI_merrSkemeKontSipasID per te marre nje datarow duke filtruar sipas ID-se se skemes.
        /// <param name="id">ID e skemes</param>
        /// <returns> Kthen nje datarow qe plotesojne kushtin</returns>
        /// </summary>
        internal DataRow ktheSkemaKontabelNewSipasID(int id)
        {//metoda per te marre koka flete kontabel sipas kodit

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDSKEMKONT", id, ParameterDirection.Input);
            //dbManager.AddParameters(1, "@IDNDERVITI", idnderviti, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_SKEMEKONTABILITETI_merrSkemeKontSipasID");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];

        }


        /// <summary>
        /// Ekzekuton prc_T_SKEMEKONTABILITETI_merrSkemeKontSipasKodit per te marre id e skemes kontabel te re duke filtruar sipas kodit se skemes.
        /// <param name="kodi">kodi i  skemes</param>
        /// <returns> Kthen id e skemes kontabel te re qe plotesojne kushtin</returns>
        /// </summary>
        internal int ktheIDSkemaKontabelNewSipasKodi(string kodi)
        {//metoda per te marre koka flete kontabel sipas kodit

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@KODSKEMKONT", kodi, ParameterDirection.Input);
            //dbManager.AddParameters(1, "@IDNDERVITI", idnderviti, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_SKEMEKONTABILITETI_merrSkemeKontSipasKodit");
            if (ds == null)
                return -1;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return -1;
            int idSkemKontNew;
            int.TryParse(ds.Tables[0].Rows[0]["IDSKEMKONT"].ToString(), out idSkemKontNew);
            return idSkemKontNew;
        }


        /// <summary>
        /// Ekzekuton prc_T_SKEMAKONTABELTRUPI_merrSkemeTrupiSipasID per te marre nje datatable duke filtruar sipas ID-se se skemes.
        /// <param name="id">ID e skemes</param>
        /// <returns> Kthen nje datatable qe plotesojne kushtin</returns>
        /// </summary>
        internal DataTable ktheSkemaTrupiNewSipasID(string id)
        {//metoda per te marre koka flete kontabel sipas kodit

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDSKEMKONT", id, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_SKEMAKONTABELTRUPI_merrSkemeTrupiSipasID");
            return ds.Tables[0];
        }


        /// <summary>
        /// Ekzekuton prc_T_NENLLOJLLOGARISH_merrNenLlojLlogarieSipasID per te marre nje datarow duke filtruar sipas ID-se se nenllogarise.
        /// <param name="id">Id e nenllogarise</param>
        /// <returns> Kthen nje datarow qe ploteson kushtin</returns>
        /// </summary>
        internal DataRow ktheNenLlojLlogarieSipasID(int id)
        {//metoda per te marre koka flete kontabel sipas kodit


            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDNENLLOJLLOGARIE", id, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_NENLLOJLLOGARISH_merrNenLlojLlogarieSipasID");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];

        }


        /// <summary>
        /// Ekzekuton prc_T_NENLLOJLLOGARISH_merrNenLlojLlogarieSipasID per te marre nje datarow duke filtruar sipas ID-se se nenllogarise.
        /// <param name="id">Id e nenllogarise</param>
        /// <returns> Kthen nje datarow qe ploteson kushtin</returns>
        /// </summary>
        internal DataRow ktheNenLlojLlogarieSipasKodit(string kodi)
        {//metoda per te marre koka flete kontabel sipas kodit

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@KODNENLLOJLLOGARIE", kodi, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_NENLLOJLLOGARISH_merrNenLlojLlogarieSipasKodit");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];
        }


        /// <summary>
        /// Ekzekuton prc_T_LLOJLLOGARIE_merrLlojLlogarieSipasId per te marre nje datarow duke filtruar sipas ID-se se llojit te llogarise.
        /// <param name="id">Id e llojit te llogarise</param>
        /// <returns> Kthen nje datarow qe ploteson kushtin</returns>
        /// </summary>
        internal DataRow ktheLlojLlogarieSipasID(int id)
        {//metoda per te marre koka flete kontabel sipas kodit

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDLLOJLLOGARIE", id, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_LLOJLLOGARIE_merrLlojLlogarieSipasId");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];

        }


        /// <summary>
        /// Ekzekuton prc_T_LLOJLLOGARIE_merrLlojLlogarieSipasId per te marre nje datarow duke filtruar sipas ID-se se llojit te llogarise.
        /// <param name="kodi">kodi llojit te llogarise</param>
        /// <returns> Kthen nje datarow qe ploteson kushtin</returns>
        /// </summary>
        internal DataRow ktheLlojLlogarieSipasKodit(string kodi)
        {//metoda per te marre koka flete kontabel sipas kodit

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@KODILLOJLLOGARIE", kodi, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_LLOJLLOGARIE_merrLlojLlogarieSipasKodit");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];
        }


        #endregion

        /// <summary>
        /// Metodat e meposhtme i perkasin veprimeve qe kryhen nga objektet clsKushtPageseKoka, clsKushtPageseTrupi, colKushtPageseKoka, colKushtPageseTrupi
        /// </summary>
        #region  KUSHTE PAGESE

        /// <summary>
        /// Ekzekuton prc_T_KUSHTPAGESEKOKA_ins per te ruajtur nje objekt clsKushtPageseKoka ne DB.
        /// <param name="idkoka">id e kokes</param>
        /// <param name="kodi">kodi i kokes</param>
        /// <param name="emertimi">emertimi i kushtit</param>
        /// <param name="lloji">Lloji i kushtit</param>
        /// <param name="autorizim">autorizim</param>
        /// <param name="af">afati</param>
        /// <param name="nd">ndarjet</param>
        /// <param name="inter">intervalet</param>
        /// <param name="nr">numri</param>
        /// <param name="nrNdarje">numri i ndarjeve</param>
        /// <param name="idnderm">id e ndermarrjes</param>
        /// <returns> Kthen statusin e perfundimit te ekzekutimit te SP-se perkatese (true nese veprimi eshte kryer me sukses)</returns>
        /// </summary>
        internal int ruajKushtPageseKoka(int idkoka, string kodi, string emertimi, string lloji, int autorizim, int af, string nd, string inter, int nr, int nrNdarje, int idnderm, int idperdorues, int idstatusdok)
        {
            dbManager.Open();
            dbManager.CreateParameters(13);
            dbManager.AddParameters(0, "@IDKOKA", idkoka, ParameterDirection.Output);
            dbManager.AddParameters(1, "@KODI", kodi, ParameterDirection.Input);
            dbManager.AddParameters(2, "@EMERTIMI", emertimi, ParameterDirection.Input);
            dbManager.AddParameters(3, "@LLOJI", lloji, ParameterDirection.Input);
            dbManager.AddParameters(4, "@IDAUTORIZIM", autorizim, ParameterDirection.Input);
            dbManager.AddParameters(5, "@AFATI", af, ParameterDirection.Input);
            dbManager.AddParameters(6, "@NDARJA", nd, ParameterDirection.Input);
            dbManager.AddParameters(7, "@INTERVALI", inter, ParameterDirection.Input);
            dbManager.AddParameters(8, "@NUMRI", nr, ParameterDirection.Input);
            dbManager.AddParameters(9, "@NUMRINDARJEVE", nrNdarje, ParameterDirection.Input);
            dbManager.AddParameters(10, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            dbManager.AddParameters(11, "@IDPERDORUESI", idperdorues, ParameterDirection.Input);
            dbManager.AddParameters(12, "@IDSTATUSDOK", idstatusdok, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_KUSHTPAGESEKOKA_ins");

            idkoka = int.Parse(dbManager.Parameters[0].Value.ToString());
            return idkoka;
        }


        /// <summary>
        /// Ekzekuton prc_T_KUSHTPAGESEKOKA_ekzistonKushtPagese per te kontrolluar nese ekziston nje objekt clsKushtPageseKoka ne DB duke filtruar sipas kodit te kushtit te pageses dhe ID-se se ndermarrjes.
        ///<param name="kodiKushtPagese">Kodi i kushtit te pageses</param>
        ///<param name="idnderm">Id e ndermarrjes</param>
        /// <returns> Kthen true nese ekziston nje objekt clsKushtPageseKoka qe ploteson kushtin</returns>
        /// </summary>
        public bool ekzistonKushtPagese(String kodiKushtPagese, int idnderm)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@KODI", kodiKushtPagese, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KUSHTPAGESEKOKA_ekzistonKushtPagese");
            if (ds.Tables[0].Rows.Count == 1)
                return true;
            else if (ds.Tables[0].Rows.Count == 0)
                return false;
            else return true;
        }

        /// <summary>
        /// Ekzekuton prc_T_KUSHTPAGESEKOKA_opd per te modifikuar nje objekt clsKushtPageseKoka ne DB.
        /// <param name="idkoka">id e kokes</param>
        /// <param name="kodi">kodi i kokes</param>
        /// <param name="emertimi">emertimi i kushtit</param>
        /// <param name="lloji">Lloji i kushtit</param>
        /// <param name="autorizim">autorizim</param>
        /// <param name="af">afati</param>
        /// <param name="nd">ndarjet</param>
        /// <param name="inter">intervalet</param>
        /// <param name="nr">numri</param>
        /// <param name="nrNdarje">numri i ndarjeve</param>
        /// <param name="idnderm">id e ndermarrjes</param>
        /// <returns> Kthen statusin e perfundimit te ekzekutimit te SP-se perkatese (true nese veprimi eshte kryer me sukses)</returns>
        /// </summary>
        internal clsMesazh modifikoKushtPageseKoka(int idkoka, string kodi, string emertimi, string lloji, int autorizim, int af, string nd, string inter, int nr, int nrNdarje, int idnderm, int idperdorues, int idstatusdok)
        {
            dbManager.Open();
            dbManager.CreateParameters(13);
            dbManager.AddParameters(0, "@IDKOKA", idkoka, ParameterDirection.Input);
            dbManager.AddParameters(1, "@KODI", kodi, ParameterDirection.Input);
            dbManager.AddParameters(2, "@EMERTIMI", emertimi, ParameterDirection.Input);
            dbManager.AddParameters(3, "@LLOJI", lloji, ParameterDirection.Input);
            dbManager.AddParameters(4, "@IDAUTORIZIM", autorizim, ParameterDirection.Input);
            dbManager.AddParameters(5, "@AFATI", af, ParameterDirection.Input);
            dbManager.AddParameters(6, "@NDARJA", nd, ParameterDirection.Input);
            dbManager.AddParameters(7, "@INTERVALI", inter, ParameterDirection.Input);
            dbManager.AddParameters(8, "@NUMRI", nr, ParameterDirection.Input);
            dbManager.AddParameters(9, "@NUMRINDARJEVE", nrNdarje, ParameterDirection.Input);
            dbManager.AddParameters(10, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            dbManager.AddParameters(11, "@IDPERDORUESI", idperdorues, ParameterDirection.Input);
            dbManager.AddParameters(12, "@IDSTATUSDOK", idstatusdok, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_KUSHTPAGESEKOKA_upd");
            idkoka = int.Parse(dbManager.Parameters[0].Value.ToString());
            return new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
        }


        /// <summary>
        /// Ekzekuton prc_T_KUSHTPAGESEKOKA_del per te fshire nje objekt clsKushtPageseKoka ne DB.
        /// <param name="idkoka">id e kokes</param>
        /// <returns> Kthen statusin e perfundimit te ekzekutimit te SP-se perkatese (true nese veprimi eshte kryer me sukses)</returns>
        /// </summary>
        internal clsMesazh fshiKushtPageseKoka(int idkoka)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDKOKA", idkoka, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_KUSHTPAGESEKOKA_del");
            return new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
        }
        internal clsMesazh fshiKushtPageseKokaStatus(int idkoka, int idperdorues)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDKOKA", idkoka, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUESI", idperdorues, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_KUSHTPAGESEKOKA_upddel");
            return new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
        }


        /// <summary>
        /// Ekzekuton prc_T_KUSHTPAGESEKOKA_merrGjitheKushtetPageses per te marre nje collection me objekte clsKushtPageseKoka ne DB duke filtruar sipas ID-se se ndermarrjes.
        /// <param name="idNderm">Id e ndermarrjes</param>
        /// <returns> Kthen nje collection me objekte clsKushtPageseKoka qe plotesojne kushtin</returns>
        /// </summary>
        internal DataTable ktheGjitheKushtetPageses(int idNderm)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDNDERMARJE", idNderm, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KUSHTPAGESEKOKA_merrGjitheKushtetPageses");
            return ds.Tables[0];
        }


        /// <summary>
        /// Ekzekuton prc_T_KUSHTPAGESEKOKA_merrKushtPageseSipasKodit per te marre nje collection me objekte clsKushtPageseKoka ne DB duke filtruar sipas ID-se se ndermarrjes dhe kodit te kushtit te pageses.
        /// <param name="kodi">Kodi i kushtit te pageses</param>
        /// <param name="idnderm">Id e ndermarrjes</param>
        /// <returns> Kthen nje collection me objekte clsKushtPageseKoka qe plotesojne kushtin</returns>
        /// </summary>
        internal DataRow ktheKushtPageseSipasKodit(String kodi, int idnderm)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@KODI", kodi, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KUSHTPAGESEKOKA_merrKushtPageseSipasKodit");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];
        }


        /// <summary>
        /// Ekzekuton prc_T_KUSHTPAGESEKOKA_merrKushtPageseSipasID per te marre nje collection me objekte clsKushtPageseKoka ne DB duke filtruar sipas ID-se se kokes se kushtit te pageses.
        /// <param name="id">Id e kokes se kushtit te pageses</param>
        /// <returns> Kthen nje collection me objekte clsKushtPageseKoka qe plotesojne kushtin</returns>
        /// </summary>
        internal DataRow ktheKushtPageseSipasID(int id)
        {
            ImbLogger.LogTraceShitje($"Filloi metoda kthe kusht pagese me id:{id}");
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDKOKA", id, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KUSHTPAGESEKOKA_merrKushtPageseSipasID");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;

            ImbLogger.LogTraceShitje($"Mbaroi metoda kthe kusht pagese me id:{id}");
            return ds.Tables[0].Rows[0];
        }


        /// <summary>
        /// Ekzekuton prc_T_KUSHTPAGESETRUPI_ins per te ruajtur nje objekt clsKushtPageseTrupi ne DB.
        /// <param name="idtrupi">id e trupit</param>
        /// <param name="idkoka">id e kokes</param>
        /// <param name="inter">intervali</param>
        /// <param name="per">periudha</param>
        /// <param name="d">ditet</param>
        /// <param name="z">zbritja</param>
        /// <param name="kusht">kushtet</param>
        /// <returns> Kthen statusin e perfundimit te ekzekutimit te SP-se perkatese (true nese veprimi eshte kryer me sukses)</returns>
        /// </summary>
        internal clsMesazh ruajKushtPageseTrupi(out int idtrupi, int idkoka, String inter, String per, int d, int z, int kusht)
        {
            idtrupi = -1;

            dbManager.Open();
            dbManager.CreateParameters(7);
            dbManager.AddParameters(0, "@IDTRUPI", idtrupi, ParameterDirection.Output);
            dbManager.AddParameters(1, "@IDKOKA", idkoka, ParameterDirection.Input);
            dbManager.AddParameters(2, "@INTERVALI", inter, ParameterDirection.Input);
            dbManager.AddParameters(3, "@PERIUDHA", per, ParameterDirection.Input);
            dbManager.AddParameters(4, "@DITE", d, ParameterDirection.Input);
            dbManager.AddParameters(5, "@ZBRITJE", z, ParameterDirection.Input);
            dbManager.AddParameters(6, "@KUSHTPAGESE", kusht, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_KUSHTPAGESETRUPI_ins");
            idtrupi = int.Parse(dbManager.Parameters[0].Value.ToString());

            return new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
        }


        /// <summary>
        /// Ekzekuton prc_T_KUSHTPAGESETRUPI_upd per te modifikuar nje objekt clsKushtPageseTrupi ne DB.
        /// <param name="idtrupi">id e trupit</param>
        /// <param name="idkoka">id e kokes</param>
        /// <param name="inter">intervali</param>
        /// <param name="per">periudha</param>
        /// <param name="d">ditet</param>
        /// <param name="z">zbritja</param>
        /// <param name="kusht">kushtet</param>
        /// <returns> Kthen statusin e perfundimit te ekzekutimit te SP-se perkatese (true nese veprimi eshte kryer me sukses)</returns>
        /// </summary>
        internal clsMesazh modifikoKushtPageseTrupi(int idtrupi, int idkoka, String inter, String per, int d, int z, int kusht)
        {
            dbManager.Open();
            dbManager.CreateParameters(7);
            dbManager.AddParameters(0, "@IDTRUPI", idtrupi, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDKOKA", idkoka, ParameterDirection.Input);
            dbManager.AddParameters(2, "@INTERVALI", inter, ParameterDirection.Input);
            dbManager.AddParameters(3, "@PERIUDHA", per, ParameterDirection.Input);
            dbManager.AddParameters(4, "@DITE", d, ParameterDirection.Input);
            dbManager.AddParameters(5, "@ZBRITJE", z, ParameterDirection.Input);
            dbManager.AddParameters(6, "@KUSHTPAGESE", kusht, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_KUSHTPAGESETRUPI_upd");
            idtrupi = int.Parse(dbManager.Parameters[0].Value.ToString());

            return new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);

        }


        /// <summary>
        /// Ekzekuton prc_T_KUSHTPAGESETRUPI_del per te fshire nje objekt clsKushtPageseTrupi ne DB.
        /// <param name="idtrupi">id e trupit</param>
        /// <param name="idkoka">id e kokes</param>
        /// <param name="inter">intervali</param>
        /// <param name="per">periudha</param>
        /// <param name="d">ditet</param>
        /// <param name="z">zbritja</param>
        /// <param name="kusht">kushtet</param>
        /// <returns> Kthen statusin e perfundimit te ekzekutimit te SP-se perkatese (true nese veprimi eshte kryer me sukses)</returns>
        /// </summary>
        internal clsMesazh fshiKushtPageseTrupi(int idkoka)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDKOKA", idkoka, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_KUSHTPAGESETRUPI_del");
            return new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);

        }


        /// <summary>
        /// Ekzekuton prc_T_KUSHTPAGESETRUPI_merrTrupatKushtevePagesesSipasKokes per te marre nje collection me objekte clsKushtPageseTrupi ne DB duke filtruar sipas ID-se se kokes se kushtit te pageses.
        /// <param name="idKoka">Id e kokes se kushtit te pageses</param>
        /// <returns> Kthen nje collection me objekte clsKushtPageseTrupi qe plotesojne kushtin</returns>
        /// </summary>
        internal DataTable ktheTrupatKushtevePagesesSipasKokes(int idKoka)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDKOKA", idKoka, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KUSHTPAGESETRUPI_merrTrupatKushtevePagesesSipasKokes");
            return ds.Tables[0];
        }
    

        #endregion

        /// <summary>
        /// Metodat e meposhtme i perkasin veprimeve qe kryhen nga objektet clsKategoriShpenzimi dhe colKategoriShpenzimi
        /// </summary>
        #region KATEGORI SHPENZIMI

        /// <summary>
        /// ekzekuton 	[prc_T_KATEGORISHPENZIMI_ins] ne DB dhe kthen statusin e perfundimit te ekzekutimit te SP-se perkatese
        /// Shiko clsMesazh brenda ketij moduli per informacion te metejshem mbi vleren e kthimit te kesaj metode
        /// </summary>
        /// <param name="id"> id e kokes</param>
        /// <param name="kodi">kodi</param>
        /// <param name="pershkrimi">pershkrimi</param>
        /// <param name="idkrijuesi">id e krijuesit</param>
        /// <param name="idPerdoruesi">id e perdoruesit modifikues</param>
        /// <param name="idnderm">id e ndermarjes</param>
        /// <param name="idstatusdok">id e statusdok</param>
        /// <returns>nje objekt clsMesazh qe tregon nese ruajtja eshte kryer ne rregull apo jo.</returns>
        internal clsMesazh ruajKategoriShpenzimi(out int id, string kodi, string pershkrimi, int idkrijuesi, int idPerdoruesi, int idnderm, int idstatusdok, int idPrindi, int nivelKategorie, bool kategoriAktive)
        {
            id = -1;

            dbManager.Open();
            dbManager.CreateParameters(10);
            dbManager.AddParameters(0, "@ID", id, ParameterDirection.Output);
            dbManager.AddParameters(1, "@KODI", kodi, ParameterDirection.Input);
            dbManager.AddParameters(2, "@PERSHKRIMI", pershkrimi, ParameterDirection.Input);
            dbManager.AddParameters(3, "@IDKRIJUESI", idkrijuesi, ParameterDirection.Input);
            dbManager.AddParameters(4, "@IDPERDORUESI", idPerdoruesi, ParameterDirection.Input);
            dbManager.AddParameters(5, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            dbManager.AddParameters(6, "@IDSTATUSDOK", idstatusdok, ParameterDirection.Input);
            if (idPrindi == 0)
                dbManager.AddParameters(7, "@IDPRINDI", DBNull.Value, ParameterDirection.Input);
            else
                dbManager.AddParameters(7, "@IDPRINDI", idPrindi, ParameterDirection.Input);
            dbManager.AddParameters(8, "@NIVELKATEGORIE", nivelKategorie, ParameterDirection.Input);
            dbManager.AddParameters(9, "@KATEGORIAKTIVE", kategoriAktive, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_KATEGORISHPENZIMI_ins");
            id = int.Parse(dbManager.Parameters[0].Value.ToString());
            return new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
        }

        /// <summary>
        /// ekzekuton prc_T_KATEGORISHPENZIMI_upd ne DB dhe kthen statusin e perfundimit te ekzekutimit te SP-se perkatese
        /// Shiko clsMesazh brenda ketij moduli per informacion te metejshem mbi vleren e kthimit te kesaj metode
        /// </summary>
        /// <param name="idkoka"> id e kokes</param>
        /// <param name="kodi">kodi</param>
        /// <param name="pershkrimi">pershkrimi</param>
        /// <param name="idPerdoruesi">id e perdoruesit</param>
        /// <param name="idnderm">id e ndermarjes</param>
        /// <param name="idstatusdok">id e statusdok</param>
        /// <returns> nje objekt clsMesazh qe tregon nese modifikimi eshte kryer ne rregull apo jo.</returns>
        internal clsMesazh modifikoKategoriShpenzimi(int id, string kodi, string pershkrimi, int idPerdoruesi, int idnderm, int idstatusdok, int idPrindi, int nivelKategorie, bool kategoriAktive)
        {
            dbManager.Open();
            dbManager.CreateParameters(9);
            dbManager.AddParameters(0, "@ID", id, ParameterDirection.Input);
            dbManager.AddParameters(1, "@KODI", kodi, ParameterDirection.Input);
            dbManager.AddParameters(2, "@PERSHKRIMI", pershkrimi, ParameterDirection.Input);
            dbManager.AddParameters(3, "@IDPERDORUESI", idPerdoruesi, ParameterDirection.Input);
            dbManager.AddParameters(4, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            dbManager.AddParameters(5, "@IDSTATUSDOK", idstatusdok, ParameterDirection.Input);
            if (idPrindi == 0)
                dbManager.AddParameters(6, "@IDPRINDI", DBNull.Value, ParameterDirection.Input);
            else
                dbManager.AddParameters(6, "@IDPRINDI", idPrindi, ParameterDirection.Input);
            dbManager.AddParameters(7, "@NIVELKATEGORIE", nivelKategorie, ParameterDirection.Input);
            dbManager.AddParameters(8, "@KATEGORIAKTIVE", kategoriAktive, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_KATEGORISHPENZIMI_upd");
            return new clsMesazh(true, MessagesResource.Messages["msgModifikimiMeSukses"]);
        }

        /// <summary>
        /// ekzekutohet sp-ja prc_T_KATEGORISHPENZIMI_del duke i kaluar id 
        /// </summary>
        /// <param name="id">id  qe do fshihet </param>
        /// <returns>nje objekt clsMesazh qe tregon nese fshirja eshte kryer ne rregull apo jo</returns>
        internal clsMesazh fshiKategoriShpenzimi(int id)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@ID", id, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_KATEGORISHPENZIMI_del");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
            return mesazh;
        }

        /// <summary>
        /// fshin kategorine  duke ndryshuar statusin e saj ne te fshire
        /// </summary>
        /// <param name="idkoka">id e kokes</param>
        /// <param name="idperdoruesi"> id e perdoruesit qe ka kryer veprimin</param>
        /// <returns> clsMesazh qe tregon nese fshirja u krye me sukses apo jo</returns>
        internal clsMesazh fshiKategoriShpenzimiStatus(int id, int idperdoruesi)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@ID", id, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUESI", idperdoruesi, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_KATEGORISHPENZIMI_upddel");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
            return mesazh;
        }

        /// <summary>
        /// merr kategori shpenzimi sipas id
        /// </summary>
        /// <param name="id">  id e kokes</param>
        /// <returns> kthen datarow qe permban kategori shpenzimi me kete id</returns>
        internal DataRow ktheKategoriShpenzimi(int id)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@ID", id, ParameterDirection.Input);
            using (DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KATEGORISHPENZIMI_merrSipasId"))
            {
                if (ds == null)
                    return null;
                if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                    return null;
                return ds.Tables[0].Rows[0];
            }
        }

        /// <summary>
        /// merr gjithe kategori shpenzimi te nje ndermarje
        /// </summary>
        /// <param name="idnder"> id e ndermarjes</param>
        /// <returns> nje datatable qe permban nje koleksion me te gjithe kategori shpenzimi te ndermarjes</returns>
        internal DataTable ktheGjitheKategoriShpenzimiSipasNdermarjes(int idnder)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDNDERMARJE", idnder, ParameterDirection.Input);
            using (DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KATEGORISHPENZIMI_merrGjithe"))
            {
                return ds.Tables[0];
            }
        }

        /// <summary>
        /// merr gjithe vitet e ndermarrjes per te cilat nuk jane celur buxhetet e kategorise perkatese
        /// </summary>
        /// <param name="idnder"></param>
        /// <param name="idLidhese"></param>
        /// <param name="idLlojBuxheti"></param>
        /// <returns>nje datatable qe permban nje koleksion me te gjithe kategori shpenzimi te ndermarjes</returns>
        internal DataTable ktheGjitheVitetENdermPerBuxhetetSipasKategorise(int idnder, int idLidhese, int idLlojBuxheti)
        {
            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@IDNDERMARJE", idnder, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDLIDHESE", idLidhese, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDLLOJBUXHETI", idLlojBuxheti, ParameterDirection.Input);
            using (DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_NDERMARJEVITI_merrIdNdermarjeVitetPerBuxhetetEKatShpenzimit"))
            {
                return ds.Tables[0];
            }
        }

        /// <summary>
        /// merr gjithe vitet e ndermarrjes per te cilat nuk jane celur buxhetet e qendres se kostos
        /// </summary>
        /// <param name="idnder"></param>
        /// <param name="idLidhese"></param>
        /// <param name="idLlojBuxheti"></param>
        /// <returns>nje datatable qe permban nje koleksion me te gjithe qendrat e kostos se ndermarjes</returns>
        internal DataTable ktheGjitheVitetENdermPerBuxhetetSipasQendraKostos(int idnder, int idLidhese, int idLlojBuxheti)
        {
            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@IDNDERMARJE", idnder, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDLIDHESE", idLidhese, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDLLOJBUXHETI", idLlojBuxheti, ParameterDirection.Input);
            using (DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_NDERMARJEVITI_merrIdNdermarjeVitetPerBuxhetetEQendraKosto"))
            {
                return ds.Tables[0];
            }
        }

        /// <summary>
        /// merr kategori shpenzimi te nje ndermarje me kete kod
        /// </summary>
        /// <param name="kodi"> kodi i skemes</param>
        /// <param name="idnder"> id e ndermarjes</param>
        /// <returns>  data row me kategori shpenzimi  te nje ndermarje me kete kod</returns>
        internal DataRow ktheKategoriShpenzimiSipasKodit(string kodi, int idnder)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@KODI", kodi, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idnder, ParameterDirection.Input);
            using (DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KATEGORISHPENZIMI_ktheSipasKodit"))
            {
                if (ds == null)
                    return null;
                if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                    return null;
                return ds.Tables[0].Rows[0];
            }
        }

        /// <summary>
        /// perdoret per te kontrolluar nqs ekziston nje kategori shpenzimi me kete kod 
        /// behet kontrolli per qellime konsistence te informacionit ne DB, nuk lejon te celen skema te ndryshem me te njejtin kod
        /// </summary>
        /// <param name="kodi">kodi i skemes</param>
        /// <param name="idNdermarje"> id e ndermarjes</param>
        /// <returns>nje objeckt boolean qe tregon nese ekziston apo jo nje kategori shpenzimi me kete kod</returns>
        public bool ekzistonKategoriShpenzimi(string kodi, int idNdermarje)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@KODI", kodi, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idNdermarje, ParameterDirection.Input);
            using (DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KATEGORISHPENZIMI_ekzistonKod"))
            {
                if (ds.Tables[0].Rows.Count == 1)
                    return true;
                else
                    if (ds.Tables[0].Rows.Count == 0)
                    return false;
                else
                    return true;
            }
        }

        public int ktheIdKategoriShpenzimiSipasKodit(string kodi, int idNdermarje)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@KODI", kodi, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idNdermarje, ParameterDirection.Input);
            object objId = dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_KATEGORISHPENZIMI_ktheId");
            if (objId != null && objId != DBNull.Value)
                return Convert.ToInt32(objId);
            return 0;
        }

        internal int KtheNivelKategoriShpenzimi(int idKategoriShpenzimi)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddInputParameters("@ID", idKategoriShpenzimi);
            object objId = dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_KATEGORISHPENZIMI_KtheNivelSipasId");
            if (objId != null && objId != DBNull.Value)
                return Convert.ToInt32(objId);
            return 1;
        }

        public bool kaVeprimeKategoriShpenzimi(int id)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@ID", id, ParameterDirection.Input);
            using (DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KATEGORISHPENZIMI_kaveprime"))
            {
                if (ds.Tables[0].Rows.Count > 0)
                    return true;
                else
                    if (ds.Tables[0].Rows.Count == 0)
                    return false;
                else
                    return true;
            }
        }

        public bool kontrolloEshtePrindKategoriaShpenzimit(int id)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@ID", id, ParameterDirection.Input);
            if (Convert.ToInt32(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_KATEGORISHPENZIMI_eshtePrind")) > 0)
                return true;
            else
                return false;
        }
        public bool kontrolloEshtePrindKategoriaShpenzimit(int id,int idPrindi)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddInputParameters( "@ID", id);
            dbManager.AddInputParameters("@IDPRINDI", idPrindi);
            if (Convert.ToInt32(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_KATEGORISHPENZIMI_KaNdryshimPrindi")) > 0)
                return true;
            else
                return false;
        }

        /// <summary>
        /// merr  kategori shpenzimi sipas id ne forme data row
        /// </summary>
        /// <param name="id"> id e kategori shpenzimi</param>
        /// <returns> kthen data row me kete skeme</returns>
        internal DataRow merrKategoriShpenzimiSipasIdDR(int id)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@ID", id, ParameterDirection.Input);
            using (DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KATEGORISHPENZIMI_merrSipasIdDR"))
            {
                if (ds == null)
                    return null;
                if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                    return null;
                return ds.Tables[0].Rows[0];
            }
        }

        /// <summary>
        /// merr gjithe kategori shpenzimi sipas ndermarjes ne forme data table
        /// </summary>
        /// <param name="idnderm">id ndermarje</param>
        /// <returns> kthen nje datatable me te gjithe kategori shpenzimi e kesaj ndermarje</returns>
        internal DataTable merrKategoriShpenzimiDT(int idnderm, bool merrVetemJoPrind)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            dbManager.AddParameters(1, "@merrJoPrind", merrVetemJoPrind, ParameterDirection.Input);
            using (DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KATEGORISHPENZIMI_merrSipasNdermarrjesDT"))
            {
                return ds.Tables[0];
            }
        }

        internal DataTable merrBijatKategoriShpenzimiSipasPrindit(int idnderm, string prindi)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            dbManager.AddParameters(1, "@KODI", prindi, ParameterDirection.Input);
            using (DataSet ds = dbManager.ExecuteDataSet(CommandType.Text, "select kodi from dbo.getBijaKategoriShpenzimi(@KODI, @IDNDERMARJE)"))
            {
                return ds.Tables[0];
            }
        }

        internal IEnumerable<AutoCompleteItem> MerrKategoriShpenzimiPerAutoComplete(string infix, int idNdermarrje, int idPerdoruesi)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddInputParameters("@INFIX", infix);
            dbManager.AddInputParameters("@IDNDERMARRJE", idNdermarrje);
            return dbManager.GetIEnumerbale("prc_T_KATEGORISHPENZIMI_merrSipasNdermarrjesDTLike", AutoCompleteItem.Krijo);
        }

        internal DataTable ktheGjitheKategoriShpenzimiSipasKokaShitje(int idKokaShitje)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddInputParameters("@IDKOKASHITJE", idKokaShitje);

            return dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KATEGORISHPENZIMI_merrSipasIdKokaShitje").Tables[0];
        }

        internal bool kaVeprimePrindiKategoriShpenzimi(int id)
        {
            var result = 0;
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddInputParameters("@ID", id);

            int.TryParse(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_KATEGORISHPENZIMI_kaveprimePrindi").ToString(), out result);
            return result > 0;
        }
        
        internal DataTable MerrKategoriShpenzimiExport(int idNdermarrja)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddInputParameters("@IDNDERMARRJE", idNdermarrja);
            return dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KATEGORISHPENZIMI_MerrKategoriShpenzimiExport").Tables[0];
        }

        #endregion

        internal string ktheEmail(int IdKlientFurnitor)
        {

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDKLIENTFURNITOR", IdKlientFurnitor, ParameterDirection.Input);
            object obj = dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_KLIENTFURNITOR_ktheEmail");
            try
            {
                return obj.ToString();
            }
            catch
            {
                return "";
            }

        }

        internal string ktheEmailPerPajisje(int IdKlientFurnitor)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDKLIENTFURNITOR", IdKlientFurnitor, ParameterDirection.Input);
            return Convert.ToString(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_KLIENTFURNITOR_ktheEmailPerPajisje"));
        }

        /// <summary>
        /// kthen idBanken e kf qe ne tabele ruhet ne kolonen EmriBanka
        /// </summary>
        /// <param name="IdKlientFurnitor"></param>
        /// <returns></returns>
        internal int merrIdBankeKF(int IdKlientFurnitor)
        {

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDKLIENTFURNITOR", IdKlientFurnitor, ParameterDirection.Input);
            return Convert.ToInt32(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_KLIENTFURNITOR_ktheEmerBanke"));

        }

        internal bool merrLlojinKF(int IdKlientFurnitor)
        {

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDKLIENTFURNITOR", IdKlientFurnitor, ParameterDirection.Input);
            return Convert.ToBoolean(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_KLIENTFURNITOR_ktheLlojin"));

        }

        internal bool modifikuarKlient(int idKlientFurnitor, DateTime dateHapjeAmbjenti)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@idKlientFurnitor", idKlientFurnitor, ParameterDirection.Input);
            dbManager.AddParameters(1, "@DTMODIFIKIMI", dateHapjeAmbjenti, ParameterDirection.Input);
            return Convert.ToBoolean(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_KLIENTFURNITOR_ModifikuarKlientFurnitor"));
        }

        internal bool eshteKlientSpecifik(int idKlientFurnitor)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDKLIENTFURNITOR", idKlientFurnitor, ParameterDirection.Input);
            return Convert.ToBoolean(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_KLIENTFURNITOR_eshteKlientSpecifik"));
        }

        internal bool ekzistonPasqyreMeKeteKodSipasNdermarrje(string kodi, int idNdermarrje)
        {

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@KODIPASQFINKOKA", kodi, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idNdermarrje, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_PASQYRAFINANCIAREKOKA_ekzistonKodi");
            if (ds.Tables[0].Rows.Count >= 1)
                return true;
            return false;

        }

        #region Marreveshje per klient

        internal clsMesazh ruajMarreveshjePerKlient(int idLlojMarreveshje, int idKlienti)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDLLOJMARREVESHJE", idLlojMarreveshje, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDKLIENTI", idKlienti, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_MARREVESHJEPERKLIENT_ins");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
            return mesazh;
        }

        /// <summary>
        /// Ekzekuton prc_T_MARREVESHJEPERKLIENT_ktheGjitheMarreveshtjetSipasKlientit per te marre nje datatable ne DB duke filtruar sipas id-se se ndermarrjes dhe aktive.
        /// <param name="idnderm">Id e ndermarrjes</param>
        /// <returns>Kthen nje datatable</returns>
        /// </summary>
        internal DataTable ktheMarreveshjeKlienti(int idKlient)
        {//metoda per te marre te gjithe kliente furnitoret

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDKLIENTFURNITOR", idKlient, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_MARREVESHJEPERKLIENT_ktheGjitheMarreveshtjetSipasKlientit");
            return ds.Tables[0];
        }


        /// <summary>
        /// Ekzekuton prc_T_MARREVESHJEPERKLIENT_ktheGjitheMarreveshtjetSipasKlientit per te marre nje datatable ne DB duke filtruar sipas id-se se ndermarrjes dhe aktive.
        /// <param name="idnderm">Id e ndermarrjes</param>
        /// <returns>Kthen nje datatable</returns>
        /// </summary>
        internal DataTable ktheVetemMarreveshjeKlienti(int idKlient)
        {//metoda per te marre te gjithe kliente furnitoret

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDKLIENTFURNITOR", idKlient, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_MARREVESHJEPERKLIENT_ktheVetemMarreveshtjetEKlientit");
            return ds.Tables[0];
        }
        /// <summary>
        /// Kthen 3 vitet e ardhshme pas vleres se vitit qe i kalon si parameter
        /// Therret SP-ne <seealso cref="prc_T_VITET_PROJEKTBUXHETI_merrVitetEArdhshme"/>
        /// </summary>
        /// <param name="viti"></param>
        /// <returns></returns>
        internal DataTable ktheVitetPerProjektBuxhetet(string viti)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@VITI", viti, ParameterDirection.Input);
            return dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_VITET_PROJEKTBUXHETI_merrVitetEArdhshme").Tables[0];
        }

        /// <summary>
        /// Ekzekuton prc_T_LLOJMARREVESHJE_ktheGjitheLlojeMarreveshjesh per te marre nje datatable ne DB duke filtruar sipas id-se se ndermarrjes dhe aktive.
        /// <param name="idnderm">Id e ndermarrjes</param>
        /// <returns>Kthen nje datatable</returns>
        /// </summary>
        internal DataTable ktheGjitheLlojeMarreveshjesh()
        {//metoda per te marre te gjithe kliente furnitoret

            dbManager.Open();
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_LLOJMARREVESHJE_ktheGjitheLlojeMarreveshjesh");
            return ds.Tables[0];
        }

        /// <summary>
        /// Ekzekuton prc_T_MARREVESHJEPERKLIENT_fshiMarreveshtjetEKlientit per te fshire marreveshjet e lidhura me klientin
        /// <param name="idnderm">Id e ndermarrjes</param>
        /// <returns>Kthen nje datatable</returns>
        /// </summary>
        internal clsMesazh fshiMarreveshjeKlienti(int idKlient)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDKLIENTFURNITOR", idKlient, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_MARREVESHJEPERKLIENT_fshiMarreveshtjetEKlientit");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
            return mesazh;
        }

        #endregion
    }
}