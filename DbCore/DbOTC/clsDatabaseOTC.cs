using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using AlphaWeb.Core.Interfaces.Data;
using AlphaWeb.Infrastructure.Data.AdoNet;
using DbCore.IMBUtils.DataBase;

namespace DbCore.DbOTC
{
    internal class clsDatabaseOTC : DbData
    {
        #region connection


        public clsDatabaseOTC()
        {
            
        }
        public clsDatabaseOTC(DbData db) : base(db) { }
        public clsDatabaseOTC(string connectionName)
            : base(connectionName)
        {

        }
        #endregion
        #region PAGESAT
        internal int RuajFature(string nrFature, string nrSerial, string nrKontrate, string emertimiKlientit, string kodKlienti, decimal vleraFillestareFatures, decimal interesi, string muajiFatures, string vitiFatures, DateTime dtFatures, int idPagesa, LlojFatureOSHEE llojFatureOSHEE)
        {
            dbManager.Open();
            dbManager.CreateParameters(13);
            dbManager.AddOutputParametersWithSize("@IDFATURA", 0, 0);
            dbManager.AddInputParameters("@nrFature", nrFature);
            dbManager.AddInputParameters("@nrSerial", nrSerial);
            dbManager.AddInputParameters("@nrKontrate", nrKontrate);
            dbManager.AddInputParameters("@emertimiKlientit", emertimiKlientit);
            dbManager.AddInputParameters("@kodKlienti", kodKlienti);
            dbManager.AddInputParameters("@vleraFillestareFatures", vleraFillestareFatures);
            dbManager.AddInputParameters("@interesi", interesi);
            dbManager.AddInputParameters("@muajiFatures", muajiFatures);
            dbManager.AddInputParameters("@vitiFatures", vitiFatures);
            dbManager.AddInputParameters("@dtFatures", dtFatures);
            dbManager.AddInputParameters("@idPagesa", idPagesa);
            dbManager.AddInputParameters("@llojFatureOSHEE", llojFatureOSHEE);
            dbManager.ExecuteNonQuery(System.Data.CommandType.StoredProcedure, "prc_T_OTC_FATURA_ins");
            return Convert.ToInt32(dbManager.Parameters[0].Value);
        }

        internal int RuajPagese(string nrSerial, string nrKontakti, bool merrSmsNjoftuese, DateTime dtPagese, int idNdermarrje, int idPerdoruesiALphaWeb, int idPerdoruesiMPESA, decimal komisioni, decimal vleraPaguar, OTCLlojSherbimi llojPagese, string transactionID, string mesazhiNgaUtiliteti, StatusOTC statusiPageses, int idTransferta, int idBalanca, string paguesi, int paguesKomisioni, bool dokAnullimi, int idDokAnullues)
        {
            dbManager.Open();
            dbManager.CreateParameters(20);
            dbManager.AddOutputParametersWithSize("@PagesaId", 0, 0);
            dbManager.AddInputParameters("@nrSerial", nrSerial);
            dbManager.AddInputParameters("@NrKontakti", nrKontakti);
            dbManager.AddInputParameters("@merrSmsNjoftuese", merrSmsNjoftuese);
            dbManager.AddInputParameters("@dtPagese", dtPagese);
            dbManager.AddInputParameters("@idNdermarrje", idNdermarrje);
            dbManager.AddInputParameters("@idPerdoruesiALphaWeb", idPerdoruesiALphaWeb);
            dbManager.AddInputParameters("@idPerdoruesiMPESA", idPerdoruesiMPESA);
            dbManager.AddInputParameters("@komisioni", komisioni);
            dbManager.AddInputParameters("@vleraPaguar", vleraPaguar);
            dbManager.AddInputParameters("@llojPagese", llojPagese);
            dbManager.AddInputParameters("@transactionID", transactionID);
            dbManager.AddInputParameters("@mesazhiNgaUtiliteti", mesazhiNgaUtiliteti);
            dbManager.AddInputParameters("@statusiPageses", statusiPageses);
            if (idTransferta == 0) dbManager.AddInputParameters("@idTransferta", DBNull.Value);
            else dbManager.AddInputParameters("@idTransferta", idTransferta);
            if (idBalanca == 0) dbManager.AddInputParameters("@idBalanca", DBNull.Value);
            else dbManager.AddInputParameters("@idBalanca", idBalanca);
            dbManager.AddInputParameters("@paguesi", paguesi);
            dbManager.AddInputParameters("@pagues_komisioni", paguesKomisioni);
            if (idDokAnullues != 0) dbManager.AddInputParameters("@ID_DOK_ANULLES", idDokAnullues);
            else dbManager.AddInputParameters("@ID_DOK_ANULLES", DBNull.Value);
            dbManager.AddInputParameters("@DOKANULLIMI", dokAnullimi);
            dbManager.ExecuteNonQuery(System.Data.CommandType.StoredProcedure, "prc_T_OTC_PAGESA_ins");
            return Convert.ToInt32(dbManager.Parameters[0].Value);
        }

        internal void MerrPageseSipasId(int PagesaId, IDataBaseReader objekti)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddInputParameters("@PAGESAID", PagesaId);
            dbManager.FillCollection("prc_T_OTC_PAGESA_merrSipasId", objekti);
        }
        internal void MerrFaturaSipasIdPagese(int idPagesa, IDataBaseReader col)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddInputParameters("@PAGESAID", idPagesa);
            dbManager.FillCollection("prc_T_OTC_FATURA_merrSipasIdPagese", col);
        }
        #endregion
        #region MPESAOPERATIONS

        internal void MerrCheckBalanceSipasID(int id, IDataBaseReader objekti)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddInputParameters("@ID", id);
            dbManager.FillObject("prc_T_OTC_MPESAOPERATION_checkBalance_selSipasID", objekti);
        }
        internal int RuajBalancen(string conversationId, string nrLlogarie, decimal? balanca, StatusOTC statusTransaksioni, string mesazhTransaksioni, int idPerdoruesiAlphaWeb, int idPerdoruesiMpesa, int idNdermarrje)
        {
            dbManager.Open();
            dbManager.CreateParameters(9);
            dbManager.AddOutputParametersWithSize("@ID", 0, 0);
            dbManager.AddInputParameters("@conversationID", conversationId);
            dbManager.AddInputParameters("@nrLlogarie", nrLlogarie);
            dbManager.AddInputParameters("@balanca", balanca);
            dbManager.AddInputParameters("@statusTransaksioni", statusTransaksioni);
            dbManager.AddInputParameters("@mesazhTransaksioni", mesazhTransaksioni);
            dbManager.AddInputParameters("@idPerdoruesiAlphaWeb", idPerdoruesiAlphaWeb);
            dbManager.AddInputParameters("@idPerdoruesiMpesa", idPerdoruesiMpesa);
            dbManager.AddInputParameters("@idNdermarrje", idNdermarrje);
            dbManager.ExecuteNonQuery(System.Data.CommandType.StoredProcedure, "prc_T_OTC_MPESAOPERATION_checkBalance_ins");
            return Convert.ToInt32(dbManager.Parameters[0].Value);
        }

        internal clsMesazh updateIdTransferta(int pagesaID, int idTransferta)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddInputParameters("@PAGESAID", pagesaID);
            dbManager.AddInputParameters("@IDTRANSFERTA", idTransferta);
            dbManager.ExecuteNonQuery(System.Data.CommandType.StoredProcedure, "prc_T_OTC_PAGESA_updateIdTransferimi");
            return new clsMesazh(true);
        }
        internal clsMesazh UpdateIdDokAnullues(int PagesaId, int idDokAnullues)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddInputParameters("@PAGESAID", PagesaId);
            dbManager.AddInputParameters("@ID_DOK_ANULLUES", idDokAnullues);
            dbManager.ExecuteNonQuery(System.Data.CommandType.StoredProcedure, "prc_T_OTC_PAGESA_updateIdDokAnullues");
            return new clsMesazh(true);
        }
        internal clsMesazh ModifikoBalancen(int id, string conversationId, string nrLlogarie, decimal? balanca, StatusOTC statusTransaksioni, string mesazhTransaksioni, int idPerdoruesiAlphaWeb, int idPerdoruesiMpesa, int idNdermarrje)
        {
            dbManager.Open();
            dbManager.CreateParameters(9);
            dbManager.AddInputParameters("@id", id);
            dbManager.AddInputParameters("@conversationId", conversationId);
            dbManager.AddInputParameters("@nrLlogarie", nrLlogarie);
            dbManager.AddInputParameters("@balanca", balanca);
            dbManager.AddInputParameters("@statusTransaksioni", statusTransaksioni);
            dbManager.AddInputParameters("@mesazhTransaksioni", mesazhTransaksioni);
            dbManager.AddInputParameters("@idPerdoruesiAlphaWeb", idPerdoruesiAlphaWeb);
            dbManager.AddInputParameters("@idPerdoruesiMpesa", idPerdoruesiMpesa);
            dbManager.AddInputParameters("@idNdermarrje", idNdermarrje);
            dbManager.ExecuteNonQuery(System.Data.CommandType.StoredProcedure, "prc_T_OTC_MPESAOPERATION_checkBalance_upd");
            return new clsMesazh(true);

        }
        /// <summary>
        /// todo sipas dokumentit
        /// </summary>
        /// <param name="conversationID"></param>
        /// <param name="nrLlogarie"></param>
        /// <param name="statusi"></param>
        /// <returns></returns>
        internal int RuajTransferte(string conversationId, string nrLlogarie, string llogariETransferimit, decimal vleraTransferuar, StatusOTC statusTransaksioni, string mesazhTransaksioni, int idPerdoruesiAlphaWeb, int idPerdoruesiMpesa, int idNdermarrje)
        {

            dbManager.Open();
            dbManager.CreateParameters(10);
            dbManager.AddOutputParametersWithSize("@ID", 0, 0);
            dbManager.AddInputParameters("@conversationID", conversationId);
            dbManager.AddInputParameters("@nrLlogarie", nrLlogarie);
            dbManager.AddInputParameters("@llogariETransferimit", llogariETransferimit);
            dbManager.AddInputParameters("@vleraTransferuar", vleraTransferuar);
            dbManager.AddInputParameters("@statusTransaksioni", statusTransaksioni);
            dbManager.AddInputParameters("@mesazhTransaksioni", mesazhTransaksioni);
            dbManager.AddInputParameters("@idPerdoruesiAlphaWeb", idPerdoruesiAlphaWeb);
            dbManager.AddInputParameters("@idPerdoruesiMpesa", idPerdoruesiMpesa);
            dbManager.AddInputParameters("@idNdermarrje", idNdermarrje);
            dbManager.ExecuteNonQuery(System.Data.CommandType.StoredProcedure, "prc_T_OTC_MPESAOPERATION_tansferMoney_ins");
            return Convert.ToInt32(dbManager.Parameters[0].Value);
        }
        internal clsMesazh ModifikoTransferte(int id, string conversationId, string nrLlogarie, string llogariETransferimit, decimal vleraTransferuar, StatusOTC statusTransaksioni, string mesazhTransaksioni, int idPerdoruesiAlphaWeb, int idPerdoruesiMpesa, int idNdermarrje)
        {
            dbManager.Open();
            dbManager.CreateParameters(10);
            dbManager.AddInputParameters("@id", id);
            dbManager.AddInputParameters("@conversationID", conversationId);
            dbManager.AddInputParameters("@nrLlogarie", nrLlogarie);
            dbManager.AddInputParameters("@llogariETransferimit", llogariETransferimit);
            dbManager.AddInputParameters("@vleraTransferuar", vleraTransferuar);
            dbManager.AddInputParameters("@statusTransaksioni", statusTransaksioni);
            dbManager.AddInputParameters("@mesazhTransaksioni", mesazhTransaksioni);
            dbManager.AddInputParameters("@idPerdoruesiAlphaWeb", idPerdoruesiAlphaWeb);
            dbManager.AddInputParameters("@idPerdoruesiMpesa", idPerdoruesiMpesa);
            dbManager.AddInputParameters("@idNdermarrje", idNdermarrje);
            dbManager.ExecuteNonQuery(System.Data.CommandType.StoredProcedure, "prc_T_OTC_MPESAOPERATION_tansferMoney_upd");
            return new clsMesazh(true, "Modifikimi  u krye me sukses!");

        }

        internal clsMesazh ModifikoPagese(int id, string nrSerial, string nrKontakti, bool merrSmsNjoftuese, DateTime dtPagese, int idNdermarrje, int idPerdoruesiALphaWeb, int idPerdoruesiMPESA, decimal komisioni, decimal vleraPaguar, OTCLlojSherbimi llojPagese, string transactionID, string mesazhiNgaUtiliteti, StatusOTC statusiPageses, int idTransferta, int idBalanca, DateTime dtKrijimi, DateTime dtModifikimi)
        {
            dbManager.Open();
            dbManager.CreateParameters(18);
            dbManager.AddInputParameters("@id", id);
            dbManager.AddInputParameters("@nrSerial", nrSerial);
            dbManager.AddInputParameters("@nrKontakti", nrKontakti);
            dbManager.AddInputParameters("@merrSmsNjoftuese", merrSmsNjoftuese);
            dbManager.AddInputParameters("@dtPagese", dtPagese);
            dbManager.AddInputParameters("@idNdermarrje", idNdermarrje);
            dbManager.AddInputParameters("@idPerdoruesiALphaWeb", idPerdoruesiALphaWeb);
            dbManager.AddInputParameters("@idPerdoruesiMPESA", idPerdoruesiMPESA);
            dbManager.AddInputParameters("@komisioni", komisioni);
            dbManager.AddInputParameters("@vleraPaguar", vleraPaguar);
            dbManager.AddInputParameters("@llojPagese", llojPagese);
            dbManager.AddInputParameters("@transactionID", transactionID);
            dbManager.AddInputParameters("@mesazhiNgaUtiliteti", mesazhiNgaUtiliteti);
            dbManager.AddInputParameters("@statusiPageses", statusiPageses);
            dbManager.AddInputParameters("@idTransferta", idTransferta);
            dbManager.AddInputParameters("@idBalanca", idBalanca);
            dbManager.AddInputParameters("@dtKrijimi", dtKrijimi);
            dbManager.AddInputParameters("@dtModifikimi", dtModifikimi);
            dbManager.ExecuteNonQuery(System.Data.CommandType.StoredProcedure, "prc_T_OTC_PAGESA_upd");
            return new clsMesazh(true, "Modifikimi u krye me sukses!");
        }
        internal void MerrTransferteSipasID(int id, IDataBaseReader objekti)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddInputParameters("@ID", id);
            dbManager.FillObject("prc_T_OTC_MPESAOPERATION_transferMoney_selSipasID", objekti);
        }
        internal clsMesazh ModifikoStatusMPESAOperation(string conversationID, StatusOTC statusi, string mesazhi)
        {
            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddInputParameters("@conversationID", conversationID);
            dbManager.AddInputParameters("@statusTransaksioni", statusi);
            dbManager.AddInputParameters("@mesazhTransaksioni", mesazhi);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_OTC_MPESAOPERATION_updStatus");
            return new clsMesazh(true);
        }
        #endregion

        #region komisionet
        internal int RuajKomision(OTCLlojSherbimi llojFature, TipKomisioni tipKomisioni, OTCPaguesi paguesi, double vlera, double perqindjeVodafone, double perqindjeDealer, int idPerdoruesiAlphaWeb, int idNdermarrje)
        {
            dbManager.Open();
            dbManager.CreateParameters(9);
            dbManager.AddOutputParametersWithSize("@ID", 0, 0);
            dbManager.AddInputParameters("@LLOJFATURE", llojFature);
            dbManager.AddInputParameters("@TIPKOMISIONI", tipKomisioni);
            dbManager.AddInputParameters("@PAGUESI", paguesi);
            dbManager.AddInputParameters("@VLERA", vlera);
            dbManager.AddInputParameters("@PERQINDJEVODAFONE", perqindjeVodafone);
            dbManager.AddInputParameters("@IDPERDORUESIALPHAWEB", idPerdoruesiAlphaWeb);
            dbManager.AddInputParameters("@PERQINDJEDEALER", perqindjeDealer);
            dbManager.AddInputParameters("@IDNDERMARRJE", idNdermarrje);
            dbManager.ExecuteNonQuery(System.Data.CommandType.StoredProcedure, "prc_T_OTC_KOMISION_ins");
            return Convert.ToInt32(dbManager.Parameters[0].Value);
        }
        internal clsMesazh ModifikoKomision(int idKomision, OTCLlojSherbimi llojFature, TipKomisioni tipKomisioni, OTCPaguesi paguesi, double vlera, double perqindjeVodafone, double perqindjeDealer, int idPerdoruesiAlphaWeb, int idNdermarrje)
        {
            dbManager.Open();
            dbManager.CreateParameters(9);
            dbManager.AddInputParameters("@IDKOMISION", idKomision);
            dbManager.AddInputParameters("@LLOJFATURE", llojFature);
            dbManager.AddInputParameters("@TIPKOMISIONI", tipKomisioni);
            dbManager.AddInputParameters("@PAGUESI", paguesi);
            dbManager.AddInputParameters("@VLERA", vlera);
            dbManager.AddInputParameters("@PERQINDJEVODAFONE", perqindjeVodafone);
            dbManager.AddInputParameters("@IDPERDORUESIALPHAWEB", idPerdoruesiAlphaWeb);
            dbManager.AddInputParameters("@PERQINDJEDEALER", perqindjeDealer);
            dbManager.AddInputParameters("@IDNDERMARRJE", idNdermarrje);
            dbManager.ExecuteNonQuery(System.Data.CommandType.StoredProcedure, "prc_T_OTC_KOMISION_upd");
            return new DbCore.clsMesazh(true, "Komisioni u modifikua me sukses!");
        }

        internal clsMesazh FshiKomision(int idKomision)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddInputParameters("@IDKOMISION", idKomision);
            dbManager.ExecuteNonQuery(System.Data.CommandType.StoredProcedure, "prc_T_OTC_KOMISION_del");

            return new DbCore.clsMesazh(true, "Komisioni u fshi me sukses!");
        }

        internal void MerrKomisionet(IDataBaseReader objekti)
        {
            dbManager.Open();
            dbManager.CreateParameters(0);
            dbManager.FillCollection("prc_T_OTC_KOMISION_sel", objekti);
        }

        internal void MerrKomision(OTCLlojSherbimi model, IDataBaseReader objekti)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddInputParameters("@LLOJFATURE", model);
            dbManager.FillCollection("prc_T_OTC_KOMISION_selSipasLlojit", objekti);
        }

        #endregion komisionet

        #region login


        internal int RuajUserMPESA(int idPerdoruesiAlphaWeb, int idNdermarrje, string userName, string dyqani, string emerDyqani, string emer, string mbiemer, string adreseDyqani, string emerDealer, string password)
        {
            dbManager.Open();
            dbManager.CreateParameters(11);
            dbManager.AddOutputParametersWithSize("@idPerdoruesiMPESA", 0, 0);
            dbManager.AddInputParameters("@idPerdoruesiALPHAWEB", idPerdoruesiAlphaWeb);
            dbManager.AddInputParameters("@idNdermarrje", idNdermarrje);
            dbManager.AddInputParameters("@username", userName);
            dbManager.AddInputParameters("@dyqani", dyqani);
            dbManager.AddInputParameters("@emerdyqani", emerDyqani);
            dbManager.AddInputParameters("@emer", emer);
            dbManager.AddInputParameters("@mbiemer", mbiemer);
            dbManager.AddInputParameters("@adresedyqani", adreseDyqani);
            dbManager.AddInputParameters("@emerdealer", emerDealer);
            dbManager.AddInputParameters("@password", password);
            dbManager.ExecuteNonQuery(System.Data.CommandType.StoredProcedure, "prc_T_OTC_USERMPESA_ins");
            return Convert.ToInt32(dbManager.Parameters[0].Value);
        }

        internal clsMesazh ModifikoUserMPESA(int idPerdoruesiMPESA, int idPerdoruesiAlphaWeb, int idNdermarrje, string userName, string dyqani, string emerDyqani, string emer, string mbiemer, string adreseDyqani, string emerDealer, string password)
        {
            dbManager.Open();
            dbManager.CreateParameters(11);
            dbManager.AddInputParameters("@idPerdoruesiMPESA", idPerdoruesiMPESA);
            dbManager.AddInputParameters("@idPerdoruesiALPHAWEB", idPerdoruesiAlphaWeb);
            dbManager.AddInputParameters("@idNdermarrje", idNdermarrje);
            dbManager.AddInputParameters("@username", userName);
            dbManager.AddInputParameters("@dyqani", dyqani);
            dbManager.AddInputParameters("@emerdyqani", emerDyqani);
            dbManager.AddInputParameters("@emer", emer);
            dbManager.AddInputParameters("@mbiemer", mbiemer);
            dbManager.AddInputParameters("@adresedyqani", adreseDyqani);
            dbManager.AddInputParameters("@emerdealer", emerDealer);
            dbManager.AddInputParameters("@password", password);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_OTC_USERMPESA_upd");
            return new clsMesazh(true, "Perdoruesi u modifikua me sukses!");
        }


        internal void MerrPerdoruesinMPESASipasUserAlphaWeb(int idPerdoruesiAlphaWeb, IDataBaseReader objekti)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddInputParameters("@IDPERDORUESIALPHAWEB", idPerdoruesiAlphaWeb);
            dbManager.FillCollection("prc_MerrPerdoruesinMPESA_SipasPerdoruesAlphaWeb", objekti);
        }
        internal void MerrPerdoruesinMPESA(string username, IDataBaseReader objekti)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddInputParameters("@USERNAME", username);
            dbManager.FillCollection("prc_MerrPerdoruesinMPESA_SipasUsername", objekti);
        }

        internal void MerrPerdoruesinMPESA(int idPerdoruesiMPESA, IDataBaseReader objekti)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddInputParameters("@IDUSERMPESA", idPerdoruesiMPESA);
            dbManager.FillCollection("prc_MerrPerdoruesinMPESA", objekti);
        }
        #endregion login

        internal clsMesazh RuajLog(int pagesaID, string veprimi, string kerkesa, string pergjigja, int idPerdoruesiMPESA, DateTime dtKrijimi)
        {
            dbManager.Open();
            dbManager.CreateParameters(6);
            dbManager.AddInputParameters("@PAGESAID", pagesaID);
            dbManager.AddInputParameters("@VEPRIMI", veprimi);
            dbManager.AddInputParameters("@KERKESA", kerkesa);
            dbManager.AddInputParameters("@PERGJIGJA", pergjigja);
            dbManager.AddInputParameters("@DTKRIJIMI", dtKrijimi);
            dbManager.AddInputParameters("@IDPERDORUESIMPESA", idPerdoruesiMPESA);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_OTC_USERMPESA_ins");
            return new clsMesazh(true, "Logu u ruajt me sukses!");
        }
        internal DataTable ktheFatureSipasIdNdermarrje(int idNdermarrje)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddInputParameters("@idNdermarrje", idNdermarrje);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_OTC_ktheFatura_sipasNdermarrjes");
            return ds.Tables[0];
        }
        internal DataTable ktheFatureSipasIdPerdoruesMPESA(int idPerdoruesiMPESA)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddInputParameters("@idPerdoruesMPESA", idPerdoruesiMPESA);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_OTC_ktheFatura_sipasPerdoruesMPESA");
            return ds.Tables[0];
        }
        internal DataTable ktheFatureSipasKodDyqani(string kodDyqani)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddInputParameters("@kodDyqani", kodDyqani);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_OTC_ktheFatura_sipasDyqanit");
            return ds.Tables[0];
        }

        internal DataTable ktheGjitheFaturat()
        {
            dbManager.Open();
            dbManager.CreateParameters(0);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_OTC_ktheFatura");
            return ds.Tables[0];
        }
        internal void MerrInstancatPerOshee(IDataBaseReader colOTCInstanceOSHEE)
        {
            dbManager.Open();
            dbManager.FillCollection("prc_T_OTC_INSTANCE_OSHEE_selAll", colOTCInstanceOSHEE);
        }


        internal clsMesazh ekzistonFaturaSipasNrSerial(string nrSerial, int llojPagese)
        {

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddInputParameters("@nrSerial", nrSerial);
            dbManager.AddInputParameters("@llojPagese", llojPagese);
            int ekziston = Convert.ToInt32(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_OTC_PAGESA_ekzistonFatureSipasNrSerial"));
            if (ekziston > 0)
                return new clsMesazh(true, "Ekziston fature me numer serial " + nrSerial);
            else
                return new clsMesazh(false);
        }
    }
}
