using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Configuration;
using AlphaWeb.Core.Interfaces.Data;
using DbCore.DbRegjistrim;
using DbCore.IMBUtils;
using DbCore.IMBUtils.DataBase;
using DbCore.IMBUtils.Extensions;
using DbCore.IMBUtils.Messages;
using DbCore.IMBUtils.Types;
using EO.Web.Internal;
using Converter = DbCore.IMBUtils.Types.Converter;

namespace DbCore.DbListPagesat
{
    /// <summary>
    ///  Kjo eshte klasa me e rendesishme e ketij moduli. Eshte nje klase e tille per secilin modul te projektit.
    ///  Ketu ndodhen metodat qe therriten  nga te gjithe objektet brenda projektit DbListPagesat
    ///  secila metode permban thirjet e Store procedures ne DB, duke i kaluar parametrat perkates
    ///  Metodat jane te ndara ne Regions sipas objekteve qe i therrasin keto metoda
    /// </summary>
    public class clsDatabazeListPagesa : DbData
    {


        public clsDatabazeListPagesa()
        {
        }


        public clsDatabazeListPagesa(DbData db) : base(db) { }
        public clsDatabazeListPagesa(string connectionName) : base(connectionName)
        {

        }

        internal IEnumerable<clsPunonjes> MerrPunonjesitSipasIds(List<int> ids, bool KlonimHiqPunonjesTeLarguar ,DateTime dtlp)
        {
            dbManager.Open();
            string salt = WebConfigurationManager.AppSettings["salt"];
            dbManager.CreateParameters(4);
            dbManager.AddParameters(0, "@PUNONJESITIDS", string.Join(",", ids), ParameterDirection.Input);
            dbManager.AddParameters(1, "@salt", salt, ParameterDirection.Input);
            dbManager.AddParameters(2, "@KlonimHiqPunonjesTeLarguar", KlonimHiqPunonjesTeLarguar, ParameterDirection.Input);
            dbManager.AddParameters(3, "@dtlp", dtlp, ParameterDirection.Input);
            return dbManager.GetIEnumerbale("prc_T_PUNONJES_selSipasIds", clsPunonjes.Krijo);
        }

        internal IEnumerable<clsKomponenteMuaji> MerrKomponenteMuajiSipasKompLpIds(List<int> kompLpIds)
        {
            string salt = WebConfigurationManager.AppSettings["salt"];
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@KOMPONENTEIDS", string.Join(",", kompLpIds), ParameterDirection.Input);
            dbManager.AddParameters(1, "@salt", salt, ParameterDirection.Input);
            return dbManager.GetIEnumerbale("prc_T_KOMPONENTEMUAJI_selAllSipasKomponenteIDs", clsKomponenteMuaji.Krijo);

        }

        internal void MerrStruktruraAdministrative(List<int> punonjesitIDs, IDataBaseReader obj)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@PUNONJESITIDS", string.Join(",", punonjesitIDs), ParameterDirection.Input);
            dbManager.FillCollection("prc_T_STRUKTURAADMINISTRATIVE_selSipasPunonjesIds", obj);
        }






        /// <summary>
        /// Metodat e meposhtme i perkasin veprimeve qe kryhen nga objektet clsStrukturaAdministrative dhe colStrukturaAdministrative
        /// </summary>
        #region STRUKTURA ADMINISTRATIVE

        /// <summary>
        /// ekzekuton prc_T_T_STRUKTURAADMINISTRATIVE_ins ne DB dhe kthen statusin e perfundimit te ekzekutimit te SP-se perkatese
        /// Shiko clsMesazh brenda ketij moduli per informacion te metejshem mbi vleren e kthimit te kesaj metode
        /// <param name="idStrukturaAdm">id ritese e struktures administrative</param>
        /// <param name="kodi">kodi e struktures administrative</param>
        /// <param name="emri">emri e struktures administrative</param>
        /// <param name="idPrindi"> id e prindit </param>
        /// <param name="personi"> personi e struktures administrative</param>
        /// <param name="nrTel">nr i telefonit</param>
        /// <param name="shenime">shenime </param>
        /// <param name="idPerdoruesi"> id e perdoruesit qe ka kryer veprimin</param>
        /// <param name="idnderm"> id e ndermarjes</param>
        /// <returns> nje objekt clsMesazh qe tregon nese ruajtja eshte kryer ne rregull apo jo.</returns>
        /// </summary>
        internal clsMesazh ruajStruktureAdm(out int idStrukturaAdm, string kodi, string emri, int idPrindi, string personi, string nrTel, string shenime, int idPerdoruesi, int idnderm, int idstatusdok, int qenderkosto, int idskemaqendrakosto, int llojqendre,bool aktiv)
        {
            idStrukturaAdm = -1;

            dbManager.Open();

            //shtimi i parametrave
            dbManager.CreateParameters(14);
            dbManager.AddParameters(0, "@IDSTRUKTURAADM", idStrukturaAdm, ParameterDirection.Output);
            dbManager.AddParameters(1, "@KODI", kodi, ParameterDirection.Input);
            dbManager.AddParameters(2, "@EMRI", emri, ParameterDirection.Input);
            if (idPrindi == 0) dbManager.AddParameters(3, "@IDPRINDI", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(3, "@IDPRINDI", idPrindi, ParameterDirection.Input);
            dbManager.AddParameters(4, "@PERSONI", personi, ParameterDirection.Input);
            dbManager.AddParameters(5, "@NRTEL", nrTel, ParameterDirection.Input);
            dbManager.AddParameters(6, "@SHENIME", shenime, ParameterDirection.Input);
            dbManager.AddParameters(7, "@IDPERDORUESI", idPerdoruesi, ParameterDirection.Input);
            dbManager.AddParameters(8, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            dbManager.AddParameters(9, "@IDSTATUSDOK", idstatusdok, ParameterDirection.Input);
            if (qenderkosto == 0 || qenderkosto == -1) dbManager.AddParameters(10, "@QENDRA_KOSTOS", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(10, "@QENDRA_KOSTOS", qenderkosto, ParameterDirection.Input);
            if (idskemaqendrakosto == 0 || idskemaqendrakosto == -1) dbManager.AddParameters(11, "@IDSKEMAQENDRAKOSTO", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(11, "@IDSKEMAQENDRAKOSTO", idskemaqendrakosto, ParameterDirection.Input);
            dbManager.AddParameters(12, "@LLOJQENDRE", llojqendre, ParameterDirection.Input);
            dbManager.AddParameters(13, "@aktiv", aktiv, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_T_STRUKTURAADMINISTRATIVE_ins");
            idStrukturaAdm = int.Parse(dbManager.Parameters[0].Value.ToString());
            return new clsMesazh(true, mesazhRuajtje);
        }

        internal Dictionary<string, int> GetDictionaryNrPersonalIdPunonjesi(int idNdermarrje)
        {

            string salt = WebConfigurationManager.AppSettings["salt"];
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddInputParameters("@IDNDERMARRJE", idNdermarrje);
            dbManager.AddInputParameters("@salt", salt);
            return dbManager.GetDictionary<string, int>("prc_T_PUNONJES_merrPunonjesTeVlefshemSipasNdermarrjes");

        }


        /// <summary>
        /// ekzekuton prc_T_STRUKTURAADMINISTRATIVE_upd ne DB dhe kthen statusin e perfundimit te ekzekutimit te SP-se perkatese
        /// Shiko clsMesazh brenda ketij moduli per informacion te metejshem mbi vleren e kthimit te kesaj metode
        /// <param name="idStrukturaAdm">id ritese e struktures administrative</param>
        /// <param name="kodi">kodi e struktures administrative</param>
        /// <param name="emri">emri e struktures administrative</param>
        /// <param name="idPrindi"> id e prindit </param>
        /// <param name="personi"> personi e struktures administrative</param>
        /// <param name="nrTel">nr i telefonit</param>
        /// <param name="shenime">shenime </param>
        /// <param name="idPerdoruesi"> id e perdoruesit qe ka kryer veprimin</param>
        /// <param name="idnderm"> id e ndermarjes</param>
        /// <returns> nje objekt clsMesazh qe tregon nese modifikimi eshte kryer ne rregull apo jo.</returns>
        /// </summary>
        internal clsMesazh modifikoStruktureAdm(int idStrukturaAdm, string kodi, string emri, int idPrindi, string personi, string nrTel, string shenime, int idPerdoruesi, int idnderm, int idstatusdok, int qenderkosto, int idskemaqendrakosto, int llojqendre,bool aktiv)
        {
            
            
                dbManager.Open();

                //shtimi i parametrave
                dbManager.CreateParameters(14);
                dbManager.AddParameters(0, "@IDSTRUKTURAADM", idStrukturaAdm, ParameterDirection.Input);
                dbManager.AddParameters(1, "@KODI", kodi, ParameterDirection.Input);
                dbManager.AddParameters(2, "@EMRI", emri, ParameterDirection.Input);
                if (idPrindi == 0) dbManager.AddParameters(3, "@IDPRINDI", DBNull.Value, ParameterDirection.Input);
                else dbManager.AddParameters(3, "@IDPRINDI", idPrindi, ParameterDirection.Input);
                dbManager.AddParameters(4, "@PERSONI", personi, ParameterDirection.Input);
                dbManager.AddParameters(5, "@NRTEL", nrTel, ParameterDirection.Input);
                dbManager.AddParameters(6, "@SHENIME", shenime, ParameterDirection.Input);
                dbManager.AddParameters(7, "@IDPERDORUESI", idPerdoruesi, ParameterDirection.Input);
                dbManager.AddParameters(8, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
                dbManager.AddParameters(9, "@IDSTATUSDOK", idstatusdok, ParameterDirection.Input);
                if (qenderkosto == 0) dbManager.AddParameters(10, "@QENDRA_KOSTOS", DBNull.Value, ParameterDirection.Input);
                else dbManager.AddParameters(10, "@QENDRA_KOSTOS", qenderkosto, ParameterDirection.Input);
                if (idskemaqendrakosto == 0) dbManager.AddParameters(11, "@IDSKEMAQENDRAKOSTO", DBNull.Value, ParameterDirection.Input);
                else dbManager.AddParameters(11, "@IDSKEMAQENDRAKOSTO", idskemaqendrakosto, ParameterDirection.Input);
                dbManager.AddParameters(12, "@LLOJQENDRE", llojqendre, ParameterDirection.Input);
                dbManager.AddParameters(13, "@AKTIV", aktiv, ParameterDirection.Input);
                dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_STRUKTURAADMINISTRATIVE_upd");
                return new clsMesazh(true, mesazhModifikimi);
            
        }

        internal string kaPrerjeDatashDitetELejes(DateTime dtFillimi, DateTime dtMbarimi, int idPunonjes)
        {
            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters("@DATEFILLIMI", dtFillimi, ParameterDirection.Input);
            dbManager.AddParameters("@DATEMBARIMI", dtMbarimi, ParameterDirection.Input);
            dbManager.AddParameters("@IDPUNONJES", idPunonjes, ParameterDirection.Input);
            var pergjigje = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_DITELEJE_kaPrerjeDatashLeja");
            if (pergjigje == null || pergjigje.Tables[0].Rows.Count == 0)
                return "";
            return pergjigje.Tables[0].Rows[0]["DATA"].ToString();
        }

        /// <summary>
        /// ekzekutohet sp-ja prc_T_STRUKTURAADMINISTRATIVE_del duke i kaluar id e struktures administrative 
        /// </summary>
        /// <param name="idStrukturaAdm">id e struktures qe do fshihet </param>
        /// <returns>nje objekt clsMesazh qe tregon nese fshirja eshte kryer ne rregull apo jo</returns>
        internal clsMesazh fshiStruktureAdministrative(int idStrukturaAdm)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDSTRUKTURAADM", idStrukturaAdm, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_STRUKTURAADMINISTRATIVE_del");
            clsMesazh mesazh = new clsMesazh(true, mesazhFshirje);
            return mesazh;
        }



        /// <summary>
        /// fshin strukturen administrative duke ndryshuar statusin e struktures ne te fshire
        /// </summary>
        /// <param name="idStruktureAdm">id e struktures</param>
        /// <param name="idperdoruesi"> id e perdoruesit qe ka kryer veprimin</param>
        /// <returns> clsMesazh qe tregon nese fshirja u krye me sukses apo jo</returns>
        internal clsMesazh fshiStruktureAdmStatus(int idStruktureAdm, int idperdoruesi)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDSTRUKTURAADM", idStruktureAdm, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUESI", idperdoruesi, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_STRUKTURAADMINISTRATIVE_upddel");
            clsMesazh mesazh = new clsMesazh(true, mesazhFshirje);
            return mesazh;
        }

        /// <summary>
        /// merr strukturen administrative sipas id
        /// </summary>
        /// <param name="idStruktureAdm">  id e struktures administrative</param>
        /// <returns> kthen datarow qe permban strukturen administrative me kete id</returns>
        internal IDataBaseReader ktheStrukture(int idStruktureAdm, IDataBaseReader obj)
        {

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDSTRUKTURAADM", idStruktureAdm, ParameterDirection.Input);
            dbManager.FillObject("prc_T_STRUKTURAADMINISTRATIVE_sel", obj);
            return obj;

        }

        /// <summary>
        /// merr gjithe strukturat administrative te nje ndermarje
        /// </summary>
        /// <param name="idnder"> id e ndermarjes</param>
        /// <returns> nje datatable qe permban nje koleksion me te gjithe strukturat administrative te ndermarjes</returns>
        internal void ktheGjitheStrukturatAdmSipasNdermarjes(int idnder, IDataBaseReader col)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDNDERMARJE", idnder, ParameterDirection.Input);
            dbManager.FillCollection("prc_T_STRUKTURAADMINISTRATIVE_merrStruktureAdmSipasNdermarjes", col);

        }

        /// <summary>
        /// merr gjithe strukturat administrative prind te nje ndermarje 
        /// </summary>
        /// <param name="idnder"> id e ndermarjes</param>
        /// <returns> nje datatable qe permban nje koleksion me te gjithe strukturat administrative prind te ndermarjes <returns>
        internal void ktheGjitheStrukturaAdministrativePrindiSipasNdermarjes(int idnder,bool merrtegjitha, IDataBaseReader collectionPerTuMbushur)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDNDERMARJE", idnder, ParameterDirection.Input);
            dbManager.AddParameters(1, "@merrtegjitha", merrtegjitha, ParameterDirection.Input);
            dbManager.FillCollection("prc_T_STRUKTURAADMINISTRATIVE_merrStruktureAdmPrindSipasNdermarjes", collectionPerTuMbushur);

        }
        /// <summary>
        /// merr gjithe strukturat administrative prind te nje ndermarje 
        /// </summary>
        /// <param name="idnder"> id e ndermarjes</param>
        /// <returns> nje datatable qe permban nje koleksion me te gjithe strukturat administrative prind te ndermarjes Raportuese <returns>
        internal void ktheGjitheStrukturaAdministrativePrindiSipasNdermarjesRaportuese(int idnder, IDataBaseReader col)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDNDERMARJE", idnder, ParameterDirection.Input);
            dbManager.FillCollection("prc_T_STRUKTURAADMINISTRATIVE_merrStruktureAdmPrindSipasNdermarjesRaportuese", col);
        }
        /// <summary>
        /// merr strukturen administrative te nje ndermarje me kete kod
        /// </summary>
        /// <param name="kodi"> kodi i struktures administrative</param>
        /// <param name="idnder"> id e ndermarjes</param>
        /// <returns>  data row me strukturen administrative te nje ndermarje me kete kod</returns>
        internal void ktheStruktureAdmSipasKodit(string kodi, int idnder, IDataBaseReader obj)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@KODI", kodi, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idnder, ParameterDirection.Input);
            dbManager.FillObject("prc_T_STRUKTURAADMINISTRATIVE_merrStruktureAdmSipasKodit", obj);
        }

        /// <summary>
        /// merr strukturen administrative te nje ndermarje me kete kod
        /// </summary>
        /// <param name="kodi"> kodi i struktures administrative</param>
        /// <param name="idnder"> id e ndermarjes</param>
        /// <returns>  data row me strukturen administrative te nje ndermarje me kete kod</returns>
        internal void ktheStruktureAdmSipasEmrit(string emri, int idnder, IDataBaseReader obj)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@EMRI", emri, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idnder, ParameterDirection.Input);
            dbManager.FillObject("prc_T_STRUKTURAADMINISTRATIVE_merrStruktureAdmSipasEmrit", obj);

        }

        /// <summary>
        /// merr gjithe strukturat te nje prindi
        /// </summary>
        /// <param name="idprindi">id e struktures prind</param>
        /// <returns>nje datatable qe permban nje koleksion me te gjitha strukturat bij te ketij prindi</returns>
        internal void ktheStruktureSipasPrindit(int idprindi, bool merrtegjitha,IDataBaseReader col)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDPRINDI", idprindi, ParameterDirection.Input);
            dbManager.AddParameters(1, "@merrtegjitha", merrtegjitha, ParameterDirection.Input);
            dbManager.FillCollection("prc_T_STRUKTURAADMINISTRATIVE_merrStrukturaAdminidstativeSipasPrindit", col);


        }

        internal void ktheStrukturatBijaSipasStruktPrind(string prinderit, IDataBaseReader col)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDPRINDI", prinderit, ParameterDirection.Input);
            dbManager.FillCollection("prc_T_STRUKTURAADMINISTRATIVE_merrStrukturatBijaSipasShumePrind", col);
        }

        /// <summary>
        /// perdoret per te kontrolluar nqs ekziston nje strukture administrative me kete kod
        /// behet kontrolli per qellime konsistence te informacionit ne DB, nuk lejon te celen struktura administrative te ndryshem me te njejtin kod
        /// </summary>
        /// <param name="kodi">kodi i struktures administrative</param>
        /// <param name="idNdermarje"> id e ndermarjes</param>
        /// <returns>nje objeckt boolean qe tregon nese ekziston apo jo nje strukture administrative me kete kod</returns>
        public bool ekzistonStruktureAdministrative(string kodi, int idNdermarje)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@KODI", kodi, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idNdermarje, ParameterDirection.Input);
            using (DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_STRUKTURAADMINISTRATIVE_existon"))
            {
                if (ds.Tables[0].Rows.Count == 1)
                    return true;
                if (ds.Tables[0].Rows.Count == 0)
                    return false;
                return true;
            }
        }

        /// <summary>
        /// perdoret per te kontrolluar nqs ekziston nje strukture administrative me kete kod
        /// behet kontrolli per qellime konsistence te informacionit ne DB, nuk lejon te celen struktura administrative te ndryshem me te njejtin kod
        /// </summary>
        /// <param name="kodi">kodi i struktures administrative</param>
        /// <param name="idNdermarje"> id e ndermarjes</param>
        /// <returns>nje objeckt boolean qe tregon nese ekziston apo jo nje strukture administrative me kete kod</returns>
        public bool ekzistonStruktureAdministrativeEmri(string emri, int idNdermarje)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@EMRI", emri, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idNdermarje, ParameterDirection.Input);
            using (DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_STRUKTURAADMINISTRATIVE_existonEmri"))
            {
                if (ds.Tables[0].Rows.Count == 1)
                    return true;
                if (ds.Tables[0].Rows.Count == 0)
                    return false;
                return true;
            }
        }

        /// <summary>
        /// perdoret per te kontrolluar nese nje strukture administrative ka bij. perdoret ne rastet e fshirjes se struktures administrative per te mos lejuar te fshihet nje strukture prind
        /// </summary>
        /// <param name="idprindi"> id e struktures</param>
        /// <returns> kthen nje objekt boolean qe tregon nese kjo strukture ka strukutra bij apo jo</returns>
        public bool kaBijStrukturaAdministrative(int idprindi)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDPRINDI", idprindi, ParameterDirection.Input);
            bool ekziston = Convert.ToBoolean(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_STRUKTURAADMINISTRATIVE_kaBij"));
            return ekziston;
            
           
        }
        public bool EshteBijaStruktura(int idprindi)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDPRINDI", idprindi, ParameterDirection.Input);
            bool ekziston = Convert.ToBoolean(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_STRUKTURAADMINISTRATIVE_prindJoAktiv"));
            return ekziston;

         }
        

        /// <summary>
        /// perdoret per te kontrolluar nese nje strukture administrative eshte e lidhur
        /// </summary>
        /// <param name="idstruktura"> id e struktures</param>
        /// <returns> kthen nje objekt boolean qe tregon nese kjo strukture ka strukutra bij apo jo</returns>
        public bool eshteILidhurStrukturaAdministrative(int idstruktura)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@idstruktadm", idstruktura, ParameterDirection.Input);
            using (DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_STRUKTURAADMINISTRATIVE_eshteILidhur"))
            {
                if (ds.Tables[0].Rows.Count == 1)
                    return true;
                if (ds.Tables[0].Rows.Count == 0)
                    return false;
                return true;
            }
        }

        /// <summary>
        /// merr  strukturen administrative sipas id
        /// </summary>
        /// <param name="idnderm">idndermarje</param>
        /// <param name="idStruktura"> id struktura</param>
        /// <returns> kthen data row me kete strukture</returns>
        internal DataRow merrStrukturaAdministrativeSipasNdermarjesDR(int idnderm, int idStruktura)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDSTRUKTURAADM", idStruktura, ParameterDirection.Input);
            using (DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_STRUKTURAADMINISTRATIVE_merrStrukturaAdmistrativeSipasNdermarjesDR"))
            {
                if (ds == null)
                    return null;
                if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                    return null;
                return ds.Tables[0].Rows[0];
            }
        }

        /// <summary>
        /// merr gjithe strukturat administrative sipas ndermarjes
        /// </summary>
        /// <param name="idnderm">id ndermarje</param>
        /// <returns> kthen nje datatable me te gjithe strukturat e kesaj ndermarje</returns>
        internal DataTable merrStrukturaAdmNdermarjeDT(int idnderm)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            using (DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_STRUKTURAADMINISTRATIVE_merrStrukturaNdermarjeDT"))
            {
                return ds.Tables[0];
            }
        }

        #endregion

        /// <summary>
        /// Metodat e meposhtme i perkasin veprimeve qe kryhen nga objektet clsGrupPunonjesish dhe colGrupePunonjesish
        /// </summary>
        #region GRUPE PUNONJESISH

        /// <summary>
        /// Ekzekuton prc_T_GRUPPunonjesish_ins per te ruajtur nje objekt clsGrupPunonjesish ne DB.
        ///<param name="grupPunonjesish">Objekt i tipit clsGrupPunonjesish qe do te ruhet ne DB</param>
        /// <returns> Kthen statusin e perfundimit te ekzekutimit te SP-se perkatese (nje objekt clsMesazh qe tregon nese veprimi eshte kryer me sukses apo jo.<seealso cref="clsMesazh"/>)</returns>
        /// </summary>
        internal clsMesazh ruajGrupPunonjesish(int idgrupPunonjesish, string nr, string pershkrim, int idperdoruesi, int idndermarje, int idstatusdok)
        {
            dbManager.Open();
            dbManager.CreateParameters(6);
            dbManager.AddParameters(0, "@IDGRUPPunonjesish", idgrupPunonjesish, ParameterDirection.Output);
            dbManager.AddParameters(1, "@NR", nr, ParameterDirection.Input);
            dbManager.AddParameters(2, "@PERSHKRIM", pershkrim, ParameterDirection.Input);
            dbManager.AddParameters(3, "@IDPERDORUESI", idperdoruesi, ParameterDirection.Input);
            dbManager.AddParameters(4, "@IDNDERMARJE", idndermarje, ParameterDirection.Input);
            dbManager.AddParameters(5, "@IDSTATUSDOK", idstatusdok, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_GRUPPUNONJESISH_ins");
            clsMesazh mesazh = new clsMesazh(true, mesazhRuajtje);
            return mesazh;
        }

        /// <summary>
        /// Ekzekuton prc_T_GRUPPunonjesish_upd per te modifikuar nje objekt clsGrupPunonjesish ne DB.
        ///<param name="grupPunonjesish">Objekt i tipit clsGrupPunonjesish qe do te modifikohet</param>
        /// <returns> Kthen statusin e perfundimit te ekzekutimit te SP-se perkatese (nje objekt clsMesazh qe tregon nese veprimi eshte kryer me sukses apo jo.<seealso cref="clsMesazh"/>)</returns>
        /// </summary>
        internal clsMesazh modifikoGrupPunonjesish(int idgrupPunonjesish, string nr, string pershkrim, int idperdoruesi, int idndermarje, int idstatusdok)
        {
            dbManager.Open();
            dbManager.CreateParameters(6);
            dbManager.AddParameters(0, "@IDGRUPPunonjesish", idgrupPunonjesish, ParameterDirection.Input);
            dbManager.AddParameters(1, "@NR", nr, ParameterDirection.Input);
            dbManager.AddParameters(2, "@PERSHKRIM", pershkrim, ParameterDirection.Input);
            dbManager.AddParameters(3, "@IDPERDORUESI", idperdoruesi, ParameterDirection.Input);
            dbManager.AddParameters(4, "@IDNDERMARJE", idndermarje, ParameterDirection.Input);
            dbManager.AddParameters(5, "@IDSTATUSDOK", idstatusdok, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_GRUPPUNONJESISH_upd");
            clsMesazh mesazh = new clsMesazh(true, mesazhModifikimi);
            return mesazh;
        }

        /// <summary>
        /// Ekzekuton prc_T_GRUPPunonjesish_del per te fshire nje objekt clsGrupPunonjesish ne DB.
        ///<param name="grupPunonjesish">Objekt i tipit clsGrupPunonjesish qe do te fshihet</param>
        /// <returns> Kthen statusin e perfundimit te ekzekutimit te SP-se perkatese (nje objekt clsMesazh qe tregon nese veprimi eshte kryer me sukses apo jo.<seealso cref="clsMesazh"/>)</returns>
        /// </summary>
        internal clsMesazh fshiGrupPunonjesish(int idgrupPunonjesish)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDGRUPPunonjesish", idgrupPunonjesish, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_GRUPPUNONJESISH_del");
            clsMesazh mesazh = new clsMesazh(true, mesazhFshirje);
            return mesazh;
        }

        /// <summary>
        /// fshin grupin duke i ndryshuar statusin
        /// </summary>
        /// <param name="idgrupPunonjesish"> id e grupit</param>
        /// <param name="idperdorues">idperdoruesit qe ka kryer veprimin</param>
        /// <returns> clsMesazh me statusin ne eshte kryer veprimi apo jo</returns>
        internal clsMesazh fshiGrupPunonjesishStatus(int idgrupPunonjesish, int idperdorues)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDGRUPPunonjesish", idgrupPunonjesish, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUESI", idperdorues, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_GRUPPUNONJESISH_upddel");
            clsMesazh mesazh = new clsMesazh(true, mesazhFshirje);
            return mesazh;
        }



        /// <summary>
        /// Merr gjithe objektet clsGrupPunonjesish ne DB.
        /// <param name="idnderm"> id e ndermarjes</param>
        /// <returns> Kthen nje collection me objekte clsGrupPunonjesish</returns>
        /// </summary>
        internal DataTable ktheGjitheGrupetPunonjesishSipasNdermarjes(int idnderm)
        {//metoda per te marre te gjithe grupet
            dbManager.Open();
            using (DataSet ds = dbManager.ExecuteDataSet(CommandType.Text, "select * from T_GRUPPunonjesish where idstatusdok=1 and IDNDERMARJE=" + idnderm))
            {
                return ds.Tables[0];
            }
        }

        /// <summary>
        /// Ekzekuton prc_T_GRUPPunonjesish_merrGrupPunonjesishSipasKodit per te marre nje objekt clsGrupPunonjesish ne DB duke filtruar sipas numrit te grupit te Punonjesishs.
        ///<param name="kodi">Numri(kodi) i grupit te Punonjesishs</param>
        ///<param name="idnderm">id e ndermarjes</param>
        /// <returns> Kthen nje collection me objekte clsGrupPunonjesish qe plotesojne kushtin</returns>
        /// </summary>
        internal DataRow ktheGrupPunonjesishSipasKodit(string kodi, int idnderm)
        {//metoda per te marre te grupin sipas kodit

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@NR", kodi, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            using (DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_GRUPPUNONJESISH_merrGrupPunonjesishSipasKodit"))
            {
                if (ds == null)
                    return null;
                if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                    return null;
                return ds.Tables[0].Rows[0];
            }
        }

        /// <summary>
        /// Ekzekuton prc_T_GRUPPunonjesish_ktheGrupPunonjesishSipasId per te marre nje objekt clsGrupPunonjesish ne DB duke filtruar sipas id-se te grupit te Punonjesishs.
        ///<param name="id">ID e grupit te Punonjesishs</param>
        /// <returns> Kthen nje collection me objekte clsGrupPunonjesish qe plotesojne kushtin</returns>
        /// </summary>
        internal DataRow merrGrupPunonjesishSipasId(int id)
        {//metoda per te marre grupin sipas id

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDGRUPPunonjesish", id, ParameterDirection.Input);
            using (DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_GRUPPUNONJESISH_ktheGrupArkePunonjesishSipasId"))
            {
                if (ds == null)
                    return null;
                if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                    return null;
                return ds.Tables[0].Rows[0];
            }
        }

        /// <summary>
        /// Ekzekuton prc_T_GRUPPunonjesish_ekzistonGrupPunonjesish per te kontrolluar nese ekziston nje objekt clsGrupPunonjesish ne DB duke filtruar sipas numrit te grupit te Punonjesishs.
        ///<param name="nr">Numri(kodi) i grupit te Punonjesishs</param>
        ///<param name="idnderm">id e ndermarjes</param>
        /// <returns> Kthen true nese ekziston nje objekt clsGrupPunonjesish qe ploteson kushtin</returns>
        /// </summary>
        public bool ekzistonGrupPunonjesish(string nr, int idnderm)
        {//kontrollon nese ekziston nje grup Punonjesish me kete nr
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@NR", nr, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            using (DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_GRUPPUNONJESISH_ekzistonGrupPUNONJESISH"))
            {
                if (ds.Tables[0].Rows.Count == 1)
                    return true;
                if (ds.Tables[0].Rows.Count == 0)
                    return false;
                return true;
            }
        }

        /// <summary>
        /// Ekzekuton prc_T_GRUPPunonjesish_kaPunonjes per te kontrolluar nese ekziston nje grup Punonjesish i caktuar ka banka apo jo.
        ///<param name="id">Id e grupit te Punonjesishs</param>
        /// <returns> Kthen true nese ky grup Punonjesish ka Punonjesish</returns>
        /// </summary>
        public bool kaPunonjes(int id)
        {

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDGRUPPunonjesish", id, ParameterDirection.Input);
            using (DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_GRUPPUNONJESISH_kaPunonjes"))
            {
                if (ds.Tables[0].Rows.Count == 1)
                    return true;
                if (ds.Tables[0].Rows.Count == 0)
                    return false;
                return true;
            }
        }

        #endregion

        /// <summary>
        /// Metodat e meposhtme i perkasin veprimeve qe kryhen nga objektet clsGrupKomponente dhe colGrupKomponente
        /// </summary>
        #region GRUPE KOMPONENTESH

        /// <summary>
        /// Ekzekuton prc_T_GRUPKOMPONENTE_ins per te ruajtur nje objekt clsGrupKomponente ne DB.
        ///<param name="grupPunonjesish">Objekt i tipit clsGrupKomponente qe do te ruhet ne DB</param>
        /// <returns> Kthen statusin e perfundimit te ekzekutimit te SP-se perkatese (nje objekt clsMesazh qe tregon nese veprimi eshte kryer me sukses apo jo.<seealso cref="clsMesazh"/>)</returns>
        /// </summary>
        internal clsMesazh ruajGrupKomponente(int id, string nr, string pershkrim, int idperdoruesi, int idkrijuesi, int idndermarje, int idstatusdok, int idrenditje)
        {

            dbManager.Open();
            dbManager.CreateParameters(8);
            dbManager.AddParameters(0, "@ID", id, ParameterDirection.Output);
            dbManager.AddParameters(1, "@KODI", nr, ParameterDirection.Input);
            dbManager.AddParameters(2, "@PERSHKRIMI", pershkrim, ParameterDirection.Input);
            dbManager.AddParameters(3, "@IDPERDORUESI", idperdoruesi, ParameterDirection.Input);
            dbManager.AddParameters(4, "@IDNDERMARJE", idndermarje, ParameterDirection.Input);
            dbManager.AddParameters(5, "@IDSTATUSDOK", idstatusdok, ParameterDirection.Input);
            dbManager.AddParameters(6, "@IDKRIJUESI", idkrijuesi, ParameterDirection.Input);
            dbManager.AddParameters(7, "@IDRENDITJE", idrenditje, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_GRUPKOMPONENTE_ins");
            clsMesazh mesazh = new clsMesazh(true, mesazhRuajtje);
            return mesazh;

        }

        /// <summary>
        /// Ekzekuton prc_T_GRUPKOMPONENTE_upd per te modifikuar nje objekt clsGrupKomponente ne DB.
        ///<param name="grupPunonjesish">Objekt i tipit clsGrupKomponente qe do te modifikohet</param>
        /// <returns> Kthen statusin e perfundimit te ekzekutimit te SP-se perkatese (nje objekt clsMesazh qe tregon nese veprimi eshte kryer me sukses apo jo.<seealso cref="clsMesazh"/>)</returns>
        /// </summary>
        internal clsMesazh modifikoGrupKomponente(int id, string nr, string pershkrim, int idperdoruesi, int idndermarje, int idstatusdok)
        {

            dbManager.Open();
            dbManager.CreateParameters(6);
            dbManager.AddParameters(0, "@ID", id, ParameterDirection.Input);
            dbManager.AddParameters(1, "@KODI", nr, ParameterDirection.Input);
            dbManager.AddParameters(2, "@PERSHKRIMi", pershkrim, ParameterDirection.Input);
            dbManager.AddParameters(3, "@IDPERDORUESI", idperdoruesi, ParameterDirection.Input);
            dbManager.AddParameters(4, "@IDNDERMARJE", idndermarje, ParameterDirection.Input);
            dbManager.AddParameters(5, "@IDSTATUSDOK", idstatusdok, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_GRUPKOMPONENTE_upd");
            clsMesazh mesazh = new clsMesazh(true, mesazhModifikimi);
            return mesazh;

        }

        /// <summary>
        /// Ekzekuton prc_T_GRUPKomponente_del per te fshire nje objekt clsGrupKomponente ne DB.
        ///<param name="grupPunonjesish">Objekt i tipit clsGrupKomponente qe do te fshihet</param>
        /// <returns> Kthen statusin e perfundimit te ekzekutimit te SP-se perkatese (nje objekt clsMesazh qe tregon nese veprimi eshte kryer me sukses apo jo.<seealso cref="clsMesazh"/>)</returns>
        /// </summary>
        internal clsMesazh fshiGrupKomponente(int id)
        {

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@ID", id, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_GRUPKOMPONENTE_del");
            clsMesazh mesazh = new clsMesazh(true, mesazhFshirje);
            return mesazh;

        }

        /// <summary>
        /// fshin grupin duke i ndryshuar statusin
        /// </summary>
        /// <param name="idgrupPunonjesish"> id e grupit</param>
        /// <param name="idperdorues">idperdoruesit qe ka kryer veprimin</param>
        /// <returns> clsMesazh me statusin ne eshte kryer veprimi apo jo</returns>
        internal clsMesazh fshiGrupKomponenteStatus(int id, int idperdorues)
        {

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@ID", id, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUESI", idperdorues, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_GRUPKOMPONENTE_upddel");
            clsMesazh mesazh = new clsMesazh(true, mesazhFshirje);
            return mesazh;

        }

        /// <summary>
        /// Merr gjithe objektet clsGrupKomponente ne DB.
        /// <param name="idnderm"> id e ndermarjes</param>
        /// <returns> Kthen nje collection me objekte clsGrupKomponente</returns>
        /// </summary>
        internal DataTable ktheGjitheGrupetKomponenteSipasNdermarjes(int idnderm)
        {//metoda per te marre te gjithe grupet

            dbManager.Open();
            using (DataSet ds = dbManager.ExecuteDataSet(CommandType.Text, "select * from T_GRUPKomponente where idstatusdok=1 and IDNDERMARJE=" + idnderm))
            {
                return ds.Tables[0];
            }
        }

        /// <summary>
        /// Ekzekuton prc_T_GRUPKomponente_merrGrupKomponenteSipasKodit per te marre nje objekt clsGrupKomponente ne DB duke filtruar sipas numrit te grupit te Punonjesishs.
        ///<param name="kodi">Numri(kodi) i grupit te Punonjesishs</param>
        ///<param name="idnderm">id e ndermarjes</param>
        /// <returns> Kthen nje collection me objekte clsGrupPunonjesish qe plotesojne kushtin</returns>
        /// </summary>
        internal DataRow ktheGrupKomponenteSipasKodit(string kodi, int idnderm)
        {//metoda per te marre te grupin sipas kodit

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@KODI", kodi, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            using (DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_GRUPKOMPONENTE_merrGrupSipasKodit"))
            {
                if (ds == null)
                    return null;
                if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                    return null;
                return ds.Tables[0].Rows[0];
            }
        }

        /// <summary>
        /// Ekzekuton prc_T_GRUPKomponente_ktheGrupSipasId per te marre nje objekt clsGrupKomponente ne DB duke filtruar sipas id-se te grupit te Komponente.
        ///<param name="id">ID e grupit te Komponente</param>
        /// <returns> Kthen nje collection me objekte clsGrupKomponente qe plotesojne kushtin</returns>
        /// </summary>
        internal DataRow merrGrupKomponenteSipasId(int id)
        {//metoda per te marre grupin sipas id

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@ID", id, ParameterDirection.Input);
            using (DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_GRUPKOMPONENTE_ktheGrupSipasId"))
            {
                if (ds == null)
                    return null;
                if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                    return null;
                return ds.Tables[0].Rows[0];
            }

        }

        /// <summary>
        /// Ekzekuton prc_T_GRUPKomponente_ekzistonGrup per te kontrolluar nese ekziston nje objekt clsGrupKomponente ne DB duke filtruar sipas numrit te grupit te Komponente.
        ///<param name="nr">Numri(kodi) i grupit te Punonjesishs</param>
        ///<param name="idnderm">id e ndermarjes</param>
        /// <returns> Kthen true nese ekziston nje objekt clsGrupKomponente qe ploteson kushtin</returns>
        /// </summary>
        public bool ekzistonGrupKomponente(string nr, int idnderm)
        {//kontrollon nese ekziston nje grup Punonjesish me kete nr

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@KODI", nr, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            using (DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_GRUPKOMPONENTE_ekzistonGrup"))
            {
                if (ds.Tables[0].Rows.Count == 1)
                    return true;
                if (ds.Tables[0].Rows.Count == 0)
                    return false;
                return true;
            }
        }

        /// <summary>
        /// Ekzekuton prc_T_GRUPKomponente_kaPunonjes per te kontrolluar nese ekziston nje grup Komponente i caktuar ka banka apo jo.
        ///<param name="id">Id e grupit te Komponente</param>
        /// <returns> Kthen true nese ky grup Komponente ka Punonjesish</returns>
        /// </summary>
        public bool kaKomponente(int id)
        {

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@ID", id, ParameterDirection.Input);
            using (DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_GRUPKOMPONENTE_kaKomponente"))
            {
                if (ds.Tables[0].Rows.Count == 1)
                    return true;
                if (ds.Tables[0].Rows.Count == 0)
                    return false;
                return true;
            }
        }

        #endregion

        /// <summary>
        /// Metodat e meposhtme i perkasin veprimeve qe kryhen nga objektet clsKomponentePage dhe colKomponentePage
        /// </summary>
        #region KOMPONENTE PAGE

        /// <summary>
        /// ekzekuton 	[prc_T_KOMPONENTEPAGE_ins] ne DB dhe kthen statusin e perfundimit te ekzekutimit te SP-se perkatese
        /// Shiko clsMesazh brenda ketij moduli per informacion te metejshem mbi vleren e kthimit te kesaj metode
        /// </summary>
        /// <param name="idKomponentePage"> id e komponentes</param>
        /// <param name="kodi">kodi</param>
        /// <param name="pershkrimi">pershkrimi</param>
        /// <param name="njesi">njesia nr, tab, formule</param>
        /// <param name="kodparam">kodi i parametrit</param>
        /// <param name="emerparam"> emri i parametrit</param>
        /// <param name="formula"> formula</param>
        /// <param name="idllogdebi">idllogdebi</param>
        /// <param name="idllogkredi">idllogkredi</param>
        /// <param name="aktivizimi"> aktive inaktive</param>
        /// <param name="tipi">tipi pagese,ndalese, llogaritese</param>
        /// <param name="lloji">lloji komponente apo listpagese</param>
        /// <param name="modeli">modeli </param>
        /// <param name="data">data e aktivizimit</param>
        /// <param name="idkonfig">id e konfigurimit</param>
        /// <param name="idPerdoruesi">id e perdoruesit</param>
        /// <param name="idnderm">id e ndermarjes</param>
        /// <param name="idstatusdok">id e statusdok</param>
        /// <returns>nje objekt clsMesazh qe tregon nese ruajtja eshte kryer ne rregull apo jo.</returns>
        internal clsMesazh ruajKomponentePage(out int idKomponentePage, string kodi, string pershkrimi, int njesi, string kodparam, string emerparam, string formula, int idllogdebi, int idllogkredi, bool aktivizimi, int tipi, bool lloji, int modeli, DateTime data, int idkonfig, int idPerdoruesi, int idnderm, int idstatusdok, int njesiparam, bool aplikopagemuaji, bool aplikoditemuaji, bool shfaqdefault, int idgrupkomponente, bool lejomodvlere, bool llogaritgjithmone, int idgrupniveli1, int idgrupniveli2, int idrenditje, string Shenime)
        {
            idKomponentePage = -1;

            dbManager.Open();
            dbManager.CreateParameters(29);
            dbManager.AddParameters(0, "@IDKOMPONENTEPAGE", idKomponentePage, ParameterDirection.Output);
            dbManager.AddParameters(1, "@KODI", kodi, ParameterDirection.Input);
            dbManager.AddParameters(2, "@PERSHKRIMI", pershkrimi, ParameterDirection.Input);
            dbManager.AddParameters(3, "@NJESI", njesi, ParameterDirection.Input);
            dbManager.AddParameters(4, "@PARAMKODI", kodparam, ParameterDirection.Input);
            dbManager.AddParameters(5, "@PARAMEMRI", emerparam, ParameterDirection.Input);
            dbManager.AddParameters(6, "@FORMULA", formula, ParameterDirection.Input);
            if (idllogdebi == 0 || idllogdebi == -1) dbManager.AddParameters(7, "@IDLLOGDEBI", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(7, "@IDLLOGDEBI", idllogdebi, ParameterDirection.Input);
            if (idllogkredi == 0 || idllogkredi == -1) dbManager.AddParameters(8, "@IDLLOGKREDI", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(8, "@IDLLOGKREDI", idllogkredi, ParameterDirection.Input);
            dbManager.AddParameters(9, "@AKTIVIZIMI", aktivizimi, ParameterDirection.Input);
            if (tipi == 0) dbManager.AddParameters(10, "@TIPI", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(10, "@TIPI", tipi, ParameterDirection.Input);
            dbManager.AddParameters(11, "@LLOJI", lloji, ParameterDirection.Input);
            dbManager.AddParameters(12, "@MODEL", modeli, ParameterDirection.Input);
            dbManager.AddParameters(13, "@DATA", data, ParameterDirection.Input);
            dbManager.AddParameters(14, "@IDKONFIG", idkonfig, ParameterDirection.Input);
            dbManager.AddParameters(15, "@IDPERDORUESI", idPerdoruesi, ParameterDirection.Input);
            dbManager.AddParameters(16, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            dbManager.AddParameters(17, "@IDSTATUSDOK", idstatusdok, ParameterDirection.Input);
            dbManager.AddParameters(18, "@PARAMNJESI", njesiparam, ParameterDirection.Input);
            dbManager.AddParameters(19, "@APLIKOPAGEMUAJI", aplikopagemuaji, ParameterDirection.Input);
            dbManager.AddParameters(20, "@APLIKODITEMUAJI", aplikoditemuaji, ParameterDirection.Input);
            dbManager.AddParameters(21, "@SHFAQDEFAULT", shfaqdefault, ParameterDirection.Input);
            dbManager.AddParameters(22, "@LEJOMODVLERE", lejomodvlere, ParameterDirection.Input);
            dbManager.AddParameters(23, "@LLOGARITGJITHMONE", llogaritgjithmone, ParameterDirection.Input);
            if (idgrupkomponente == 0 || idgrupkomponente == -1) dbManager.AddParameters(24, "@IDGRUPKOMPONENTE", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(24, "@IDGRUPKOMPONENTE", idgrupkomponente, ParameterDirection.Input);

            if (idgrupniveli1 == 0 || idgrupniveli1 == -1) dbManager.AddParameters(25, "@IDGRUPNIVEL1", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(25, "@IDGRUPNIVEL1", idgrupniveli1, ParameterDirection.Input);
            if (idgrupniveli2 == 0 || idgrupniveli2 == -1) dbManager.AddParameters(26, "@IDGRUPNIVEL2", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(26, "@IDGRUPNIVEL2", idgrupniveli2, ParameterDirection.Input);
            if (idrenditje == 0 || idrenditje == -1) dbManager.AddParameters(27, "@IDRENDITJE", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(27, "@IDRENDITJE", idrenditje, ParameterDirection.Input);
            dbManager.AddParameters(28, "@Shenime", Shenime, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_KOMPONENTEPAGE_ins");
            idKomponentePage = int.Parse(dbManager.Parameters[0].Value.ToString());
            return new clsMesazh(true, mesazhRuajtje);

        }

        /// <summary>
        /// ekzekuton 	[prc_T_KOMPONENTEPAGE_insDefaultSipasLlojit] ne DB dhe kthen statusin e perfundimit te ekzekutimit te SP-se perkatese
        /// shton komponentet default per kete ndermarje per kete lloj
        /// Shiko clsMesazh brenda ketij moduli per informacion te metejshem mbi vleren e kthimit te kesaj metode
        /// </summary>
        /// <param name="lloji">lloji komponente apo listpagese</param>
        /// <param name="data">data e aktivizimit</param>
        /// <param name="idPerdoruesi">id e perdoruesit</param>
        /// <param name="idnderm">id e ndermarjes</param>
        /// <returns>nje objekt clsMesazh qe tregon nese ruajtja eshte kryer ne rregull apo jo.</returns>
        internal clsMesazh ruajDefaultKomponentePage(bool lloji, DateTime data, int idPerdoruesi, int idnderm, int idndermnga)
        {

            dbManager.Open();
            dbManager.CreateParameters(5);
            dbManager.AddParameters(0, "@LLOJI", lloji, ParameterDirection.Input);
            dbManager.AddParameters(1, "@DATA", data, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDPERDORUESI", idPerdoruesi, ParameterDirection.Input);
            dbManager.AddParameters(3, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            dbManager.AddParameters(4, "@IDNDERMARJENGA", idndermnga, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_KOMPONENTEPAGE_insDefaultSipasLlojit");
            return new clsMesazh(true, mesazhRuajtje);

        }

        /// <summary>
        /// ekzekuton prc_T_KOMPONENTEPAGE_upd ne DB dhe kthen statusin e perfundimit te ekzekutimit te SP-se perkatese
        /// Shiko clsMesazh brenda ketij moduli per informacion te metejshem mbi vleren e kthimit te kesaj metode
        /// <param name="idKomponentePage"> id e komponentes</param>
        /// <param name="kodi">kodi</param>
        /// <param name="pershkrimi">pershkrimi</param>
        /// <param name="njesi">njesia nr, tab, formule</param>
        /// <param name="kodparam">kodi i parametrit</param>
        /// <param name="emerparam"> emri i parametrit</param>
        /// <param name="formula"> formula</param>
        /// <param name="idllogdebi">idllogdebi</param>
        /// <param name="idllogkredi">idllogkredi</param>
        /// <param name="aktivizimi"> aktive inaktive</param>
        /// <param name="tipi">tipi pagese,ndalese, llogaritese</param>
        /// <param name="lloji">lloji komponente apo listpagese</param>
        /// <param name="modeli">modeli </param>
        /// <param name="data">data e aktivizimit</param>
        /// <param name="idkonfig">id e konfigurimit</param>
        /// <param name="idPerdoruesi">id e perdoruesit</param>
        /// <param name="idnderm">id e ndermarjes</param>
        /// <param name="idstatusdok">id e statusdok</param>
        /// <returns> nje objekt clsMesazh qe tregon nese modifikimi eshte kryer ne rregull apo jo.</returns>
        /// </summary>
        internal clsMesazh modifikoKomponentePage(int idKomponentePage, string kodi, string pershkrimi, int njesi, string kodparam, string emerparam, string formula, int idllogdebi, int idllogkredi, bool aktivizimi, int tipi, bool lloji, int modeli, DateTime data, int idkonfig, int idPerdoruesi, int idnderm, int idstatusdok, int njesiparam, bool aplikopagemuaji, bool aplikoditemuaji, bool shfaqdefault, int idgrupkomponente, bool lejomodvlere, bool llogaritgjithmone, string Shenime)
        {

            dbManager.Open();
            dbManager.CreateParameters(26);
            dbManager.AddParameters(0, "@IDKOMPONENTEPAGE", idKomponentePage, ParameterDirection.Input);
            dbManager.AddParameters(1, "@KODI", kodi, ParameterDirection.Input);
            dbManager.AddParameters(2, "@PERSHKRIMI", pershkrimi, ParameterDirection.Input);
            dbManager.AddParameters(3, "@NJESI", njesi, ParameterDirection.Input);
            dbManager.AddParameters(4, "@PARAMKODI", kodparam, ParameterDirection.Input);
            dbManager.AddParameters(5, "@PARAMEMRI", emerparam, ParameterDirection.Input);
            dbManager.AddParameters(6, "@FORMULA", formula, ParameterDirection.Input);
            if (idllogdebi == 0 || idllogdebi == -1) dbManager.AddParameters(7, "@IDLLOGDEBI", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(7, "@IDLLOGDEBI", idllogdebi, ParameterDirection.Input);
            if (idllogkredi == 0 || idllogkredi == -1) dbManager.AddParameters(8, "@IDLLOGKREDI", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(8, "@IDLLOGKREDI", idllogkredi, ParameterDirection.Input);
            dbManager.AddParameters(9, "@AKTIVIZIMI", aktivizimi, ParameterDirection.Input);
            if (tipi == 0) dbManager.AddParameters(10, "@TIPI", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(10, "@TIPI", tipi, ParameterDirection.Input);
            dbManager.AddParameters(11, "@LLOJI", lloji, ParameterDirection.Input);
            dbManager.AddParameters(12, "@MODEL", modeli, ParameterDirection.Input);
            dbManager.AddParameters(13, "@DATA", data, ParameterDirection.Input);
            dbManager.AddParameters(14, "@IDKONFIG", idkonfig, ParameterDirection.Input);
            dbManager.AddParameters(15, "@IDPERDORUESI", idPerdoruesi, ParameterDirection.Input);
            dbManager.AddParameters(16, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            dbManager.AddParameters(17, "@IDSTATUSDOK", idstatusdok, ParameterDirection.Input);
            dbManager.AddParameters(18, "@PARAMNJESI", njesiparam, ParameterDirection.Input);
            dbManager.AddParameters(19, "@APLIKOPAGEMUAJI", aplikopagemuaji, ParameterDirection.Input);
            dbManager.AddParameters(20, "@APLIKODITEMUAJI", aplikoditemuaji, ParameterDirection.Input);
            dbManager.AddParameters(21, "@SHFAQDEFAULT", shfaqdefault, ParameterDirection.Input);
            dbManager.AddParameters(22, "@LEJOMODVLERE", lejomodvlere, ParameterDirection.Input);
            dbManager.AddParameters(23, "@LLOGARITGJITHMONE", llogaritgjithmone, ParameterDirection.Input);
            if (idgrupkomponente == 0 || idgrupkomponente == -1) dbManager.AddParameters(24, "@IDGRUPKOMPONENTE", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(24, "@IDGRUPKOMPONENTE", idgrupkomponente, ParameterDirection.Input);
            dbManager.AddParameters(25, "@Shenime", Shenime, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_KOMPONENTEPAGE_upd");
            return new clsMesazh(true, mesazhModifikimi);

        }

        /// <summary>
        /// ekzekutohet sp-ja prc_T_KOMPONENTEPAGE_del duke i kaluar id e komponentes te pages
        /// </summary>
        /// <param name="idKomponentePage">id e komponentes qe do fshihet </param>
        /// <returns>nje objekt clsMesazh qe tregon nese fshirja eshte kryer ne rregull apo jo</returns>
        internal clsMesazh fshiKomponentePage(int idKomponentePage)
        {

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDKOMPONENTEPAGE", idKomponentePage, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_KOMPONENTEPAGE_del");
            clsMesazh mesazh = new clsMesazh(true, mesazhFshirje);
            return mesazh;
        }

        /// <summary>
        /// fshin komponenten duke ndryshuar statusin e saj ne te fshire
        /// </summary>
        /// <param name="idKomponentePage">id e komponentes</param>
        /// <param name="idperdoruesi"> id e perdoruesit qe ka kryer veprimin</param>
        /// <returns> clsMesazh qe tregon nese fshirja u krye me sukses apo jo</returns>
        internal clsMesazh fshiKomponentePageStatus(int idKomponentePage, int idperdoruesi)
        {

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDKOMPONENTEPAGE", idKomponentePage, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUESI", idperdoruesi, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_KOMPONENTEPAGE_upddel");
            clsMesazh mesazh = new clsMesazh(true, mesazhFshirje);
            return mesazh;

        }

        /// <summary>
        /// merr komponenten e pages sipas id
        /// </summary>
        /// <param name="idkomponente">  id e komponentes</param>
        /// <returns> kthen datarow qe permban komponenten e pages me kete id</returns>
        internal void ktheKomponentePage(int idkomponente, clsKomponentePage komponPage)
        {

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDKOMPONENTEPAGE", idkomponente, ParameterDirection.Input);
            dbManager.FillObject<clsKomponentePage>("prc_T_KOMPONENTEPAGE_ktheKomponente", komponPage.mbushKomponente);
        }

        /// <summary>
        /// merr gjithe Komponente e pages te nje ndermarje sipas Llojit
        /// </summary>
        /// <param name="idnder"> id e ndermarjes</param>
        /// <param name="lloji">lloji</param>
        /// <returns> nje datatable qe permban nje koleksion me te gjithe komponentet e pages te ndermarjes</returns>
        internal IEnumerable<clsKomponentePage> ktheGjitheKomponentePageSipasNdermarjesDheLlojit(int idnder, bool lloji)
        {

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDNDERMARJE", idnder, ParameterDirection.Input);
            dbManager.AddParameters(1, "@LLOJI", lloji, ParameterDirection.Input);
            return dbManager.GetIEnumerbale("prc_T_KOMPONENTEPAGE_merrGjitheKomponentetSipasLlojit", clsKomponentePage.Krijo);

        }

        /// <summary>
        /// merr gjithe Komponente e pages te nje ndermarje sipas Llojit aktiv
        /// </summary>
        /// <param name="idnder"> id e ndermarjes</param>
        /// <param name="lloji">lloji</param>
        /// <returns> nje datatable qe permban nje koleksion me te gjithe komponentet e pages te ndermarjes</returns>
        internal IEnumerable<clsKomponentePage> ktheGjitheKomponentePageSipasNdermarjesDheLlojitAktiv(int idnder, bool lloji)
        {

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDNDERMARJE", idnder, ParameterDirection.Input);
            dbManager.AddParameters(1, "@LLOJI", lloji, ParameterDirection.Input);
            return dbManager.GetIEnumerbale("prc_T_KOMPONENTEPAGE_merrGjitheKomponentetSipasLlojitAktive", clsKomponentePage.Krijo);

        }

        /// <summary>
        /// merr gjithe Komponente e pages te nje ndermarje sipas Llojit dhe dates
        /// </summary>
        /// <param name="idnder"> id e ndermarjes</param>
        /// <param name="lloji">lloji</param>
        /// <param name="data"> data e aktivizimit</param>
        /// <returns> nje datatable qe permban nje koleksion me te gjithe komponentet e pages te ndermarjes</returns>
        internal IEnumerable<clsKomponentePage> ktheGjitheKomponentePageSipasNdermarjesDheLlojitDheDates(int idnder, bool lloji, DateTime data)
        {

            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@IDNDERMARJE", idnder, ParameterDirection.Input);
            dbManager.AddParameters(1, "@LLOJI", lloji, ParameterDirection.Input);
            dbManager.AddParameters(2, "@DATA", data, ParameterDirection.Input);
            return dbManager.GetIEnumerbale("prc_T_KOMPONENTEPAGE_merrGjitheKomponentetSipasLlojitDheDates", clsKomponentePage.Krijo);


        }
        internal IEnumerable<clsKomponentePage> ktheGjitheKomponentePageSipasNdermarjesDheLlojitDheDatesMeTeAfert(int idnder, bool lloji, DateTime data)
        {

            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@IDNDERMARJE", idnder, ParameterDirection.Input);
            dbManager.AddParameters(1, "@LLOJI", lloji, ParameterDirection.Input);
            dbManager.AddParameters(2, "@DATA", data, ParameterDirection.Input);
            return dbManager.GetIEnumerbale("prc_T_KOMPONENTEPAGE_merrGjitheKomponentetSipasLlojitDheDatesMeTeAfert", clsKomponentePage.Krijo);

        }
        internal IEnumerable<clsKomponentePage> ktheGjitheKomponentePageSipasNdermarjesDheLlojitDheDatesMeTeAfertFormule(int idnder, bool lloji, DateTime data)
        {

            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@IDNDERMARJE", idnder, ParameterDirection.Input);
            dbManager.AddParameters(1, "@LLOJI", lloji, ParameterDirection.Input);
            dbManager.AddParameters(2, "@DATA", data, ParameterDirection.Input);
            return dbManager.GetIEnumerbale("prc_T_KOMPONENTEPAGE_merrGjitheKomponentetSipasLlojitDheDatesMeTeAfertFormule", clsKomponentePage.Krijo);


        }
        internal IEnumerable<clsKomponentePage> ktheGjitheKomponentePageSipasNdermarjesDheLlojitDheDatesMeTeAfertNr(int idnder, bool lloji, DateTime data)
        {

            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@IDNDERMARJE", idnder, ParameterDirection.Input);
            dbManager.AddParameters(1, "@LLOJI", lloji, ParameterDirection.Input);
            dbManager.AddParameters(2, "@DATA", data, ParameterDirection.Input);
            return dbManager.GetIEnumerbale("prc_T_KOMPONENTEPAGE_merrGjitheKomponentetSipasLlojitDheDatesMeTeAfertNr", clsKomponentePage.Krijo);


        }
        /// <summary>
        /// merr komponente page te nje ndermarje me kete kod
        /// </summary>
        /// <param name="kodi"> kodi i komponentes te pages</param>
        /// <param name="idnder"> id e ndermarjes</param>
        /// <returns>  data row me komponenten e pages te nje ndermarje me kete kod</returns>
        internal bool ktheKomponentePageSipasKodit(string kodi, int idnder, clsKomponentePage komponPage)
        {

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@KODI", kodi, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idnder, ParameterDirection.Input);
            return dbManager.FillObject<clsKomponenteListPagesePunonjesi>("prc_T_KOMPONENTEPAGE_merrKomponenteSipasKodit", komponPage.mbushKomponente);



        }
        internal IEnumerable<PunonjesMeData> MerrIdPunonjesishMeDataAktivizimi(List<int> idPunonjesish, DateTime dtaktivizimi, int idNdermarrje)
        {
            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddInputParameters("@idpunonjesish", string.Join(",", idPunonjesish));
            dbManager.AddInputParameters("@Data", dtaktivizimi);
            dbManager.AddInputParameters("@idNdermarrje", idNdermarrje);
            return dbManager.GetIEnumerbale("prc_merrPunonjesMeDataAktivizimi", PunonjesMeData.Krijo);
        }
                
        internal IEnumerable<PunonjesMeSigurime> MerrIdPunonjesishMeIdSigurimiSipasDateAktivizimi(List<int> idPunonjesish, DateTime dtaktivizimi)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddInputParameters("@idpunonjesish", string.Join(",", idPunonjesish));
            dbManager.AddInputParameters("@Data", dtaktivizimi);
            return dbManager.GetIEnumerbale("prc_merrPunonjesMeIdSigurimi", PunonjesMeSigurime.Krijo);
        }
        /// <summary>
        /// merr komponente page te nje ndermarje me kete kod
        /// </summary>
        /// <param name="kodi"> kodi i komponentes te pages</param>
        /// <param name="idnder"> id e ndermarjes</param>
        /// <returns>  data row me komponenten e pages te nje ndermarje me kete kod</returns>
        internal bool ktheKomponentePageSipasKoditDheDates(string kodi, int idnder, DateTime data, clsKomponentePage komponPage)
        {

            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@KODI", kodi, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idnder, ParameterDirection.Input);
            dbManager.AddParameters(2, "@Data", data, ParameterDirection.Input);
            return dbManager.FillObject<clsKomponenteListPagesePunonjesi>("prc_T_KOMPONENTEPAGE_merrKomponenteSipasKoditDheDates", komponPage.mbushKomponente);


        }

        /// <summary>
        /// perdoret per te kontrolluar nqs ekziston nje komponente page me kete kod ne kete date
        /// behet kontrolli per qellime konsistence te informacionit ne DB, nuk lejon te celen komponente page te ndryshem me te njejtin kod
        /// </summary>
        /// <param name="kodi">kodi i komponentes te pages</param>
        /// <param name="idNdermarje"> id e ndermarjes</param>
        /// <param name="data">data e aktivizimit</param>
        /// <returns>nje objeckt boolean qe tregon nese ekziston apo jo nje komponente me kete kod</returns>
        public bool ekzistonKomponentePage(string kodi, int idNdermarje, DateTime data)
        {

            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@KODI", kodi, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idNdermarje, ParameterDirection.Input);
            dbManager.AddParameters(2, "@Data", data, ParameterDirection.Input); using (DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KOMPONENTEPAGE_ekzistonKomponente"))
            {
                if (ds.Tables[0].Rows.Count == 1)
                    return true;
                if (ds.Tables[0].Rows.Count == 0)
                    return false;
                return true;
            }
        }

        /// <summary>
        /// perdoret per te kontrolluar nqs ekziston nje komponente page me kete kod
        /// behet kontrolli per qellime konsistence te informacionit ne DB, nuk lejon te celen komponente page te ndryshem me te njejtin kod
        /// </summary>
        /// <param name="kodi">kodi i komponentes te pages</param>
        /// <param name="idNdermarje"> id e ndermarjes</param>
        /// <returns>nje objeckt boolean qe tregon nese ekziston apo jo nje komponente me kete kod</returns>
        public bool ekzistonKomponentePage(string kodi, int idNdermarje, bool lloji)
        {

            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@KODI", kodi, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idNdermarje, ParameterDirection.Input);
            dbManager.AddParameters(2, "@LLOJI", lloji, ParameterDirection.Input);
            using (DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KOMPONENTEPAGE_ekzistonKomponenteSipasKodit"))
            {
                if (ds.Tables[0].Rows.Count > 0)
                    return true;
                if (ds.Tables[0].Rows.Count == 0)
                    return false;
                return true;
            }
        }
        public bool ekzistonKomponentePageParametri(string param, int idNdermarje, string kodi, bool lloji)
        {

            dbManager.Open();
            dbManager.CreateParameters(4);
            dbManager.AddParameters(0, "@KODI", kodi, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idNdermarje, ParameterDirection.Input);
            dbManager.AddParameters(2, "@PARAM", param, ParameterDirection.Input);
            dbManager.AddParameters(3, "@LLOJI", lloji, ParameterDirection.Input);
            using (DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KOMPONENTEPAGE_ekzistonParameterSipasKodit"))
            {
                if (ds.Tables[0].Rows.Count > 0)
                    return true;
                if (ds.Tables[0].Rows.Count == 0)
                    return false;
                return true;
            }
        }
        /// <summary>
        /// perdoret per te kontrolluar nqs ekziston nje komponente page me kete kod ne formule
        /// behet kontrolli per qellime konsistence te informacionit ne DB,  te fshihet nje komponente qe eshte pjese e nje formule
        /// </summary>
        /// <param name="kodi">kodi i komponentes te pages</param>
        /// <param name="idNdermarje"> id e ndermarjes</param>
        /// <returns>nje objeckt boolean qe tregon nese ekziston apo jo nje komponente me kete kod</returns>
        public bool ekzistonKomponentePageNeFormule(string kodi, int idNdermarje)
        {

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@KODI", kodi, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idNdermarje, ParameterDirection.Input);
            using (DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KOMPONENTEPAGE_ekzistonNeFormule"))
            {
                if (ds.Tables[0].Rows.Count > 0)
                    return true;
                if (ds.Tables[0].Rows.Count == 0)
                    return false;
                return true;
            }
        }

        /// <summary>
        /// perdoret per te kontrolluar nqs ekziston nje komponente page ne kete date
        /// behet kontrolli per qellime konsistence te informacionit ne DB, nuk lejon te celen komponente page te ndryshem me te njejtin kod
        /// </summary>
        /// <param name="lloji">lloji</param>
        /// <param name="idNdermarje"> id e ndermarjes</param>
        /// <param name="date">data</param>
        /// <returns>nje objeckt boolean qe tregon nese ekziston apo jo nje komponente me kete kod</returns>
        public bool ekzistonDateKomponentePage(DateTime date, bool lloj, int idNdermarje)
        {

            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@DATA", date, ParameterDirection.Input);
            dbManager.AddParameters(1, "@LLOJI", lloj, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDNDERMARJE", idNdermarje, ParameterDirection.Input);
            using (DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KOMPONENTEPAGE_ekzistonDateKomponente"))
            {
                if (ds.Tables[0].Rows.Count == 1)
                    return true;
                if (ds.Tables[0].Rows.Count == 0)
                    return false;
                return true;
            }
        }

        /// <summary>
        /// merr  komponenten e pages sipas id
        /// </summary>
        /// <param name="idnderm">idndermarje</param>
        /// <param name="idkomponente"> id komponente</param>
        /// <returns> kthen data row me kete komponente</returns>
        internal DataRow merrKomponentePageSipasNdermarjesDR(int idnderm, int idkomponente)
        {

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDKOMPONENTEPAGE", idkomponente, ParameterDirection.Input);
            using (DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KOMPONENTEPAGE_ktheKomponenteDR"))
            {
                if (ds == null)
                    return null;
                if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                    return null;
                return ds.Tables[0].Rows[0];
            }

        }

        /// <summary>
        /// merr gjithe komponente page sipas ndermarjes sipas llojit
        /// </summary>
        /// <param name="idnderm">id ndermarje</param>
        /// <param name="lloji">lloji</param>
        /// <returns> kthen nje datatable me te gjithe komponente e kesaj ndermarje</returns>
        internal DataTable merrKomponentePageDTSipasLlojit(int idnderm, bool lloji)
        {

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            dbManager.AddParameters(1, "@LLOJI", lloji, ParameterDirection.Input);
            using (DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KOMPONENTEPAGE_ktheKomponenteDTSipasLlojit"))
            {
                return ds.Tables[0];
            }

        }

        /// <summary>
        /// merr gjithe komponente page sipas ndermarjes sipas llojit dhe dates
        /// </summary>
        /// <param name="idnderm">id ndermarje</param>
        /// <param name="lloji">lloji</param>
        /// <param name="data">data e aktivizimit</param>
        /// <returns> kthen nje datatable me te gjithe komponente e kesaj ndermarje</returns>
        internal DataTable merrKompnentePageNdermarjeDTSipasLlojitDheDates(int idnderm, bool lloji, DateTime data)
        {

            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            dbManager.AddParameters(1, "@LLOJI", lloji, ParameterDirection.Input);
            dbManager.AddParameters(2, "@DATA", data, ParameterDirection.Input);
            using (DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KOMPONENTEPAGE_ktheKomponenteDTSipasLlojitDheDates"))
            {
                return ds.Tables[0];
            }

        }

        /// <summary>
        /// merr gjithe datat e  komponenteve page sipas ndermarjes sipas llojit 
        /// </summary>
        /// <param name="idnderm">id ndermarje</param>
        /// <param name="lloji">lloji</param>
        /// <returns> kthen nje datatable me te gjithe datat e ndryshimeve te komponenteve</returns>
        internal DataTable merrDataKomponentePageSipasNdermarjesDheLlojit(int idnderm, bool lloji)
        {

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            dbManager.AddParameters(1, "@LLOJI", lloji, ParameterDirection.Input);
            using (DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KOMPONENTEPAGE_merrGjitheDatatSipasNdermarjesDheLlojit"))
            {
                return ds.Tables[0];
            }
        }

        #endregion

        /// <summary>
        /// Metodat e meposhtme i perkasin veprimeve qe kryhen nga objektet clsSigurimet dhe colSigurimet
        /// </summary>
        #region SIGURIME

        /// <summary>
        /// ekzekuton 	[prc_T_SIGURIME_ins] ne DB dhe kthen statusin e perfundimit te ekzekutimit te SP-se perkatese
        /// Shiko clsMesazh brenda ketij moduli per informacion te metejshem mbi vleren e kthimit te kesaj metode
        /// </summary>
        /// <param name="idSigurime">id e sigurimeve</param>
        /// <param name="kodi">kodi</param>
        /// <param name="data">data e aktiviximit</param>
        /// <param name="pagemin">paga minimale</param>
        /// <param name="pagemax">paga maksimale</param>
        /// <param name="sigshoqpun">sigurimet shoqerore punonjesi</param>
        /// <param name="sigshenpun">sigurimet shendetsore punonjesi</param>
        /// <param name="sigshoqnder">siguime shoqerore ndermarje</param>
        /// <param name="sigshennder">sigurime shendetsore ndermrje</param>
        /// <param name="modeli">modeli</param>
        /// <param name="idkonfig">id e konfigurimit</param>
        /// <param name="idPerdoruesi">id e perdoruesit</param>
        /// <param name="idnderm">id e ndermarjes</param>
        /// <param name="idstatusdok">id e statusdok</param>
        /// <returns>nje objekt clsMesazh qe tregon nese ruajtja eshte kryer ne rregull apo jo.</returns>
        internal clsMesazh ruajSigurime(out int idSigurime, string kodi, DateTime data, decimal pagemin, decimal pagemax, decimal sigshoqpun, decimal sigshenpun, decimal sigshoqnder, decimal sigshennder, int modeli, int idkonfig, int idPerdoruesi, int idnderm, int idstatusdok, decimal pageminshen, decimal pagemaxshen, decimal sigsuppun, decimal sigsupnder)
        {
            idSigurime = -1;

            dbManager.Open();
            dbManager.CreateParameters(18);
            dbManager.AddParameters(0, "@IDSIGURIME", idSigurime, ParameterDirection.Output);
            dbManager.AddParameters(1, "@KODI", kodi, ParameterDirection.Input);
            dbManager.AddParameters(2, "@DATA", data, ParameterDirection.Input);
            dbManager.AddParameters(3, "@PAGEMIN", pagemin, ParameterDirection.Input);
            dbManager.AddParameters(4, "@PAGEMAX", pagemax, ParameterDirection.Input);
            dbManager.AddParameters(5, "@SIGSHOQPUN", sigshoqpun, ParameterDirection.Input);
            dbManager.AddParameters(6, "@SIGSHENPUN", sigshenpun, ParameterDirection.Input);
            dbManager.AddParameters(7, "@SIGSHOQNDER", sigshoqnder, ParameterDirection.Input);
            dbManager.AddParameters(8, "@SIGSHENNDER", sigshennder, ParameterDirection.Input);
            dbManager.AddParameters(9, "@MODEL", modeli, ParameterDirection.Input);
            dbManager.AddParameters(10, "@IDKONFIG", idkonfig, ParameterDirection.Input);
            dbManager.AddParameters(11, "@IDPERDORUESI", idPerdoruesi, ParameterDirection.Input);
            dbManager.AddParameters(12, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            dbManager.AddParameters(13, "@IDSTATUSDOK", idstatusdok, ParameterDirection.Input);
            dbManager.AddParameters(14, "@PAGEMINShen", pageminshen, ParameterDirection.Input);
            dbManager.AddParameters(15, "@PAGEMAXShen", pagemaxshen, ParameterDirection.Input);
            dbManager.AddParameters(16, "@SIGSUPPUN", sigsuppun, ParameterDirection.Input);
            dbManager.AddParameters(17, "@SIGSUPNDER", sigsupnder, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_SIGURIME_ins");
            idSigurime = int.Parse(dbManager.Parameters[0].Value.ToString());
            return new clsMesazh(true, mesazhRuajtje);

        }

        internal DataRow ktheSigurimeSipasDatesMeTeAfert(int idnder, DateTime data)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDNDERMARJE", idnder, ParameterDirection.Input);
            dbManager.AddParameters(1, "@DATA", data, ParameterDirection.Input);
            using (DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_SIGURIME_merrSigurimeSipasDatesMeTeAfert"))
            {
                if (ds == null)
                    return null;
                if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                    return null;
                return ds.Tables[0].Rows[0];
            }
        }

        internal DataRow KtheSigurimeSipasDatesMeTeAfertPunonjes(int idnder, DateTime data, int idPunonjes)
        {
            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddInputParameters("@IDNDERMARJE", idnder);
            dbManager.AddInputParameters("@DATA", data);
            dbManager.AddInputParameters("@IDPUNONJES", idPunonjes);
            using (DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_SIGURIME_merrSigurimeSipasDatesMeTeAfertPunonjes"))
            {
                if (ds == null)
                    return null;
                if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                    return null;
                return ds.Tables[0].Rows[0];
            }
        }

        /// <summary>
        /// ekzekuton 	[prc_T_SIGURIME_insDefault] ne DB dhe kthen statusin e perfundimit te ekzekutimit te SP-se perkatese
        /// shton sigurimet default per kete ndermarje 
        /// Shiko clsMesazh brenda ketij moduli per informacion te metejshem mbi vleren e kthimit te kesaj metode
        /// </summary>
        /// <param name="idPerdoruesi">id e perdoruesit</param>
        /// <param name="idnderm">id e ndermarjes</param>
        /// <returns>nje objekt clsMesazh qe tregon nese ruajtja eshte kryer ne rregull apo jo.</returns>
        internal clsMesazh ruajDefaultSigurime(int idPerdoruesi, int idnderm)
        {

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDPERDORUESI", idPerdoruesi, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_SIGURIME_insDefault");
            return new clsMesazh(true, mesazhRuajtje);

        }

        /// <summary>
        /// ekzekuton prc_T_SIGURIME_upd ne DB dhe kthen statusin e perfundimit te ekzekutimit te SP-se perkatese
        /// Shiko clsMesazh brenda ketij moduli per informacion te metejshem mbi vleren e kthimit te kesaj metode
        /// <param name="idSigurime">id e sigurimeve</param>
        /// <param name="kodi">kodi</param>
        /// <param name="data">data e aktiviximit</param>
        /// <param name="pagemin">paga minimale</param>
        /// <param name="pagemax">paga maksimale</param>
        /// <param name="sigshoqpun">sigurimet shoqerore punonjesi</param>
        /// <param name="sigshenpun">sigurimet shendetsore punonjesi</param>
        /// <param name="sigshoqnder">siguime shoqerore ndermarje</param>
        /// <param name="sigshennder">sigurime shendetsore ndermrje</param>
        /// <param name="modeli">modeli</param>
        /// <param name="idkonfig">id e konfigurimit</param>
        /// <param name="idPerdoruesi">id e perdoruesit</param>
        /// <param name="idnderm">id e ndermarjes</param>
        /// <param name="idstatusdok">id e statusdok</param>
        /// <returns> nje objekt clsMesazh qe tregon nese modifikimi eshte kryer ne rregull apo jo.</returns>
        /// </summary>
        internal clsMesazh modifikoSigurime(int idSigurime, string kodi, DateTime data, decimal pagemin, decimal pagemax, decimal sigshoqpun, decimal sigshenpun, decimal sigshoqnder, decimal sigshennder, int modeli, int idkonfig, int idPerdoruesi, int idnderm, int idstatusdok, decimal pageminshen, decimal pagemaxshen, decimal sigsuppun, decimal sigsupnder)
        {

            dbManager.Open();
            dbManager.CreateParameters(18);
            dbManager.AddParameters(0, "@IDSIGURIME", idSigurime, ParameterDirection.Input);
            dbManager.AddParameters(1, "@KODI", kodi, ParameterDirection.Input);
            dbManager.AddParameters(2, "@DATA", data, ParameterDirection.Input);
            dbManager.AddParameters(3, "@PAGEMIN", pagemin, ParameterDirection.Input);
            dbManager.AddParameters(4, "@PAGEMAX", pagemax, ParameterDirection.Input);
            dbManager.AddParameters(5, "@SIGSHOQPUN", sigshoqpun, ParameterDirection.Input);
            dbManager.AddParameters(6, "@SIGSHENPUN", sigshenpun, ParameterDirection.Input);
            dbManager.AddParameters(7, "@SIGSHOQNDER", sigshoqnder, ParameterDirection.Input);
            dbManager.AddParameters(8, "@SIGSHENNDER", sigshennder, ParameterDirection.Input);
            dbManager.AddParameters(9, "@MODEL", modeli, ParameterDirection.Input);
            dbManager.AddParameters(10, "@IDKONFIG", idkonfig, ParameterDirection.Input);
            dbManager.AddParameters(11, "@IDPERDORUESI", idPerdoruesi, ParameterDirection.Input);
            dbManager.AddParameters(12, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            dbManager.AddParameters(13, "@IDSTATUSDOK", idstatusdok, ParameterDirection.Input);
            dbManager.AddParameters(14, "@PAGEMINShen", pageminshen, ParameterDirection.Input);
            dbManager.AddParameters(15, "@PAGEMAXShen", pagemaxshen, ParameterDirection.Input);
            dbManager.AddParameters(16, "@SIGSUPPUN", sigsuppun, ParameterDirection.Input);
            dbManager.AddParameters(17, "@SIGSUPNDER", sigsupnder, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_SIGURIME_upd");
            return new clsMesazh(true, mesazhModifikimi);

        }

        /// <summary>
        /// ekzekutohet sp-ja prc_T_KOMPONENTEPAGE_del duke i kaluar id e sigurimit
        /// </summary>
        /// <param name="idSigurime">id e sigurimit qe do fshihet </param>
        /// <returns>nje objekt clsMesazh qe tregon nese fshirja eshte kryer ne rregull apo jo</returns>
        internal clsMesazh fshiSigurime(int idSigurime)
        {

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDSIGURIME", idSigurime, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_SIGURIME_del");
            clsMesazh mesazh = new clsMesazh(true, mesazhFshirje);
            return mesazh;

        }

        /// <summary>
        /// fshin sigurimet duke ndryshuar statusin e saj ne te fshire
        /// </summary>
        /// <param name="idSigurime">id e sigurime</param>
        /// <param name="idperdoruesi"> id e perdoruesit qe ka kryer veprimin</param>
        /// <returns> clsMesazh qe tregon nese fshirja u krye me sukses apo jo</returns>
        internal clsMesazh fshiSigurimeStatus(int idSigurime, int idperdoruesi)
        {

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDSIGURIME", idSigurime, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUESI", idperdoruesi, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_SIGURIME_upddel");
            clsMesazh mesazh = new clsMesazh(true, mesazhFshirje);
            return mesazh;

        }

        /// <summary>
        /// merr sigurime sipas id
        /// </summary>
        /// <param name="idsigurime">  id e sigurime</param>
        /// <returns> kthen datarow qe permban sigurimin me kete id</returns>
        internal DataRow ktheSigurime(int idsigurime)
        {

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDSIGURIME", idsigurime, ParameterDirection.Input);
            using (DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_SIGURIME_ktheSigurime"))
            {
                if (ds == null)
                    return null;
                if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                    return null;
                return ds.Tables[0].Rows[0];
            }

        }

        /// <summary>
        /// merr gjithe sigurimet te nje ndermarje 
        /// </summary>
        /// <param name="idnder"> id e ndermarjes</param>
        /// <returns> nje datatable qe permban nje koleksion me te gjithe sigurimet te ndermarjes</returns>
        internal DataTable ktheGjitheSigurimetSipasNdermarjes(int idnder)
        {

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDNDERMARJE", idnder, ParameterDirection.Input);
            using (DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_SIGURIME_merrGjitheSigurimet"))
            {
                return ds.Tables[0];
            }

        }

        /// <summary>
        /// merr gjithe Sigurimet te nje ndermarje sipas  dates
        /// </summary>
        /// <param name="idnder"> id e ndermarjes</param>
        /// <param name="data"> data e aktivizimit</param>
        /// <returns> nje datatable qe permban nje koleksion me te gjithe sigurimet te ndermarjes</returns>
        internal DataTable ktheGjitheSigurimetSipasNdermarjesDheDates(int idnder, DateTime data)
        {

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDNDERMARJE", idnder, ParameterDirection.Input);
            dbManager.AddParameters(1, "@DATA", data, ParameterDirection.Input);
            using (DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_SIGURIME_merrGjitheSigurimeSipasDates"))
            {
                return ds.Tables[0];
            }

        }

        /// <summary>
        /// merr sigurime te nje ndermarje me kete kod dhe kete date
        /// </summary>
        /// <param name="kodi"> kodi i komponentes te pages</param>
        /// <param name="idnder"> id e ndermarjes</param>
        /// <param name="data">data </param>
        /// <returns>  data row me komponenten e pages te nje ndermarje me kete kod</returns>
        internal DataRow ktheSigurimeSipasKoditDheDates(string kodi, int idnder, DateTime data)
        {

            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@KODI", kodi, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idnder, ParameterDirection.Input);
            dbManager.AddParameters(2, "@DATA", data, ParameterDirection.Input);
            using (DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_SIGURIME_merrSigurimeSipasKoditDheDates"))
            {
                if (ds == null)
                    return null;
                if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                    return null;
                return ds.Tables[0].Rows[0];
            }

        }

        /// <summary>
        /// perdoret per te kontrolluar nqs ekziston nje sigurim me kete kod
        /// behet kontrolli per qellime konsistence te informacionit ne DB, nuk lejon te celen sigurime te ndryshem me te njejtin kod
        /// </summary>
        /// <param name="kodi">kodi i sigurimit</param>
        /// <param name="idNdermarje"> id e ndermarjes</param>
        /// <returns>nje objeckt boolean qe tregon nese ekziston apo jo nje sigurim me kete kod</returns>
        public bool ekzistonSigurime(string kodi, int idNdermarje)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@KODI", kodi, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idNdermarje, ParameterDirection.Input);
            using (DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_SIGURIME_ekzistonSigurimeSipasKodit"))
            {
                if (ds.Tables[0].Rows.Count == 1)
                    return true;
                if (ds.Tables[0].Rows.Count == 0)
                    return false;
                return true;
            }
        }

        /// <summary>
        /// merr  sigurime sipas id
        /// </summary>
        /// <param name="idnderm">idndermarje</param>
        /// <param name="idsigurime"> id komponente</param>
        /// <returns> kthen data row me kete komponente</returns>
        internal DataRow merrSigurimeSipasNdermarjesDR(int idnderm, int idsigurime)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDSIGURIME", idsigurime, ParameterDirection.Input);
            using (DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_SIGURIME_ktheSigurimeDR"))
            {
                if (ds == null)
                    return null;
                if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                    return null;
                return ds.Tables[0].Rows[0];
            }

        }

        /// <summary>
        /// merr gjithe sigurime sipas ndermarjes 
        /// </summary>
        /// <param name="idnderm">id ndermarje</param>
        /// <returns> kthen nje datatable me te gjithe sigurime e kesaj ndermarje</returns>
        internal DataTable merrSigurimeDT(int idnderm)
        {

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            using (DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_SIGURIME_ktheSigurimeDT"))
            {
                return ds.Tables[0];
            }
        }

        /// <summary>
        /// merr gjithe sigurime sipas ndermarjes  dhe dates
        /// </summary>
        /// <param name="idnderm">id ndermarje</param>
        /// <param name="data">data e aktivizimit</param>
        /// <returns> kthen nje datatable me te gjithe sigurimet e kesaj ndermarje</returns>
        internal DataTable merrSigurimeNdermarjeDTSipasDates(int idnderm, DateTime data)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            dbManager.AddParameters(1, "@DATA", data, ParameterDirection.Input);
            using (DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_SIGURIME_ktheSigurimeDTSipasDates"))
            {
                return ds.Tables[0];
            }
        }

        #endregion

        /// <summary>
        /// Metodat e meposhtme i perkasin veprimeve qe kryhen nga objektet clsTatime dhe colTatimet
        /// </summary>
        #region TATIME

        /// <summary>
        /// ekzekuton 	[prc_T_TATIME_ins] ne DB dhe kthen statusin e perfundimit te ekzekutimit te SP-se perkatese
        /// Shiko clsMesazh brenda ketij moduli per informacion te metejshem mbi vleren e kthimit te kesaj metode
        /// </summary>
        /// <param name="idTatime"> id e TATIME</param>
        /// <param name="data">data</param>
        /// <param name="min">minimumi</param>
        /// <param name="max">maksimumi</param>
        /// <param name="norma">norma</param>
        /// <param name="menyra"> menyra 0-progresive 1-totale</param>
        /// <param name="modeli">modeli </param>
        /// <param name="idkonfig">id e konfigurimit</param>
        /// <param name="idPerdoruesi">id e perdoruesit</param>
        /// <param name="idnderm">id e ndermarjes</param>
        /// <param name="idstatusdok">id e statusdok</param>
        /// <returns>nje objekt clsMesazh qe tregon nese ruajtja eshte kryer ne rregull apo jo.</returns>
        internal clsMesazh ruajTatim(out int idTatime, DateTime data, decimal min, decimal max, string norma, int menyra, int modeli, int idkonfig, int idPerdoruesi, int idnderm, int idstatusdok)
        {
            idTatime = -1;

            dbManager.Open();
            dbManager.CreateParameters(11);
            dbManager.AddParameters(0, "@IDTATIME", idTatime, ParameterDirection.Output);
            dbManager.AddParameters(1, "@DATA", data, ParameterDirection.Input);
            dbManager.AddParameters(2, "@MIN", min, ParameterDirection.Input);
            dbManager.AddParameters(3, "@MAX", max, ParameterDirection.Input);
            dbManager.AddParameters(4, "@NORMA", norma, ParameterDirection.Input);
            dbManager.AddParameters(5, "@MENYRA", menyra, ParameterDirection.Input);
            dbManager.AddParameters(6, "@MODEL", modeli, ParameterDirection.Input);
            dbManager.AddParameters(7, "@IDKONFIG", idkonfig, ParameterDirection.Input);
            dbManager.AddParameters(8, "@IDPERDORUESI", idPerdoruesi, ParameterDirection.Input);
            dbManager.AddParameters(9, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            dbManager.AddParameters(10, "@IDSTATUSDOK", idstatusdok, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_TATIME_ins");
            idTatime = int.Parse(dbManager.Parameters[0].Value.ToString());
            return new clsMesazh(true, mesazhRuajtje);
        }

        /// <summary>
        /// ekzekuton 	[prc_T_TATIME_insDefault] ne DB dhe kthen statusin e perfundimit te ekzekutimit te SP-se perkatese
        /// shton TATIMET default per kete ndermarje per kete lloj
        /// Shiko clsMesazh brenda ketij moduli per informacion te metejshem mbi vleren e kthimit te kesaj metode
        /// </summary>
        /// <param name="idPerdoruesi">id e perdoruesit</param>
        /// <param name="idnderm">id e ndermarjes</param>
        /// <returns>nje objekt clsMesazh qe tregon nese ruajtja eshte kryer ne rregull apo jo.</returns>
        internal clsMesazh ruajDefaultTatime(int idPerdoruesi, int idnderm)
        {

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDPERDORUESI", idPerdoruesi, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_TATIME_insDefault");
            return new clsMesazh(true, mesazhRuajtje);

        }

        /// <summary>
        /// ekzekuton prc_T_TATIME_upd ne DB dhe kthen statusin e perfundimit te ekzekutimit te SP-se perkatese
        /// Shiko clsMesazh brenda ketij moduli per informacion te metejshem mbi vleren e kthimit te kesaj metode
        /// <param name="idTatime"> id e TATIME</param>
        /// <param name="data">data</param>
        /// <param name="min">minimumi</param>
        /// <param name="max">maksimumi</param>
        /// <param name="norma">norma</param>
        /// <param name="menyra"> menyra 0-progresive 1-totale</param>
        /// <param name="modeli">modeli </param>
        /// <param name="idkonfig">id e konfigurimit</param>
        /// <param name="idPerdoruesi">id e perdoruesit</param>
        /// <param name="idnderm">id e ndermarjes</param>
        /// <param name="idstatusdok">id e statusdok</param>
        /// <returns> nje objekt clsMesazh qe tregon nese modifikimi eshte kryer ne rregull apo jo.</returns>
        /// </summary>
        internal clsMesazh modifikoTatim(int idTatime, DateTime data, decimal min, decimal max, string norma, int menyra, int modeli, int idkonfig, int idPerdoruesi, int idnderm, int idstatusdok)
        {

            dbManager.Open();
            dbManager.CreateParameters(11);
            dbManager.AddParameters(0, "@IDTATIME", idTatime, ParameterDirection.Input);
            dbManager.AddParameters(1, "@DATA", data, ParameterDirection.Input);
            dbManager.AddParameters(2, "@MIN", min, ParameterDirection.Input);
            dbManager.AddParameters(3, "@MAX", max, ParameterDirection.Input);
            dbManager.AddParameters(4, "@NORMA", norma, ParameterDirection.Input);
            dbManager.AddParameters(5, "@MENYRA", menyra, ParameterDirection.Input);
            dbManager.AddParameters(6, "@MODEL", modeli, ParameterDirection.Input);
            dbManager.AddParameters(7, "@IDKONFIG", idkonfig, ParameterDirection.Input);
            dbManager.AddParameters(8, "@IDPERDORUESI", idPerdoruesi, ParameterDirection.Input);
            dbManager.AddParameters(9, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            dbManager.AddParameters(10, "@IDSTATUSDOK", idstatusdok, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_TATIME_upd");
            return new clsMesazh(true, mesazhModifikimi);

        }

        /// <summary>
        /// ekzekutohet sp-ja prc_T_TATIME_del duke i kaluar id e tatimit
        /// </summary>
        /// <param name="idtatime">id e tatime qe do fshihet </param>
        /// <returns>nje objekt clsMesazh qe tregon nese fshirja eshte kryer ne rregull apo jo</returns>
        internal clsMesazh fshiTatim(int idtatime)
        {

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDTATIME", idtatime, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_TATIME_del");
            clsMesazh mesazh = new clsMesazh(true, mesazhFshirje);
            return mesazh;
        }

        /// <summary>
        /// fshin tatimin duke ndryshuar statusin e saj ne te fshire
        /// </summary>
        /// <param name="idtatime">id e tatimit</param>
        /// <param name="idperdoruesi"> id e perdoruesit qe ka kryer veprimin</param>
        /// <returns> clsMesazh qe tregon nese fshirja u krye me sukses apo jo</returns>
        internal clsMesazh fshiTatimStatus(int idtatime, int idperdoruesi)
        {

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDTATIME", idtatime, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUESI", idperdoruesi, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_TATIME_upddel");
            clsMesazh mesazh = new clsMesazh(true, mesazhFshirje);
            return mesazh;
        }

        /// <summary>
        /// merr tatime sipas id
        /// </summary>
        /// <param name="idtatime">  id e tatimit</param>
        /// <returns> kthen datarow qe permban tatimin me kete id</returns>
        internal DataRow ktheTatim(int idtatime)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDTATIME", idtatime, ParameterDirection.Input);
            using (DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_TATIME_ktheTatim"))
            {
                if (ds == null)
                    return null;
                if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                    return null;
                return ds.Tables[0].Rows[0];
            }
        }

        /// <summary>
        /// merr gjithe TATIME te nje ndermarje 
        /// </summary>
        /// <param name="idnder"> id e ndermarjes</param>
        /// <returns> nje datatable qe permban nje koleksion me te gjithe tatime te ndermarjes</returns>
        internal DataTable ktheGjitheTatimeSipasNdermarjes(int idnder)
        {

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDNDERMARJE", idnder, ParameterDirection.Input);
            using (DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_TATIME_merrGjitheTatimet"))
            {
                return ds.Tables[0];
            }

        }

        /// <summary>
        /// merr gjithe tatime te nje ndermarje sipas  dates
        /// </summary>
        /// <param name="idnder"> id e ndermarjes</param>
        /// <param name="data"> data e aktivizimit</param>
        /// <returns> nje datatable qe permban nje koleksion me te gjithe tatimet te ndermarjes</returns>
        internal DataTable ktheGjitheTatimeSipasNdermarjesDheDates(int idnder, DateTime data)
        {

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDNDERMARJE", idnder, ParameterDirection.Input);
            dbManager.AddParameters(1, "@DATA", data, ParameterDirection.Input);
            using (DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_TATIME_merrGjitheTatimetSipasDates"))
            {
                return ds.Tables[0];
            }

        }

        /// <summary>
        /// merr gjithe tatime te nje ndermarje sipas  dates
        /// </summary>
        /// <param name="idnder"> id e ndermarjes</param>
        /// <param name="data"> data e aktivizimit</param>
        /// <returns> nje datatable qe permban nje koleksion me te gjithe tatimet te ndermarjes</returns>
        internal DataTable ktheGjitheTatimeSipasNdermarjesDheDatesDeri(int idnder, DateTime data)
        {

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDNDERMARJE", idnder, ParameterDirection.Input);
            dbManager.AddParameters(1, "@DATA", data, ParameterDirection.Input);
            using (DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_TATIME_merrGjitheTatimetSipasDatesDeri"))
            {
                return ds.Tables[0];
            }

        }

        /// <summary>
        /// perdoret per te kontrolluar nqs ekziston nje tatim ne kete date
        /// </summary>
        /// <param name="idNdermarje"> id e ndermarjes</param>
        /// <param name="date">data</param>
        /// <returns>nje objeckt boolean qe tregon nese ekziston apo jo nje tatime ne kete date</returns>
        public bool ekzistonDateTatime(DateTime date, int idNdermarje)
        {

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@DATA", date, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idNdermarje, ParameterDirection.Input);
            using (DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_TATIME_ekzistonDateTatime"))
            {
                if (ds.Tables[0].Rows.Count == 1)
                    return true;
                if (ds.Tables[0].Rows.Count == 0)
                    return false;
                return true;
            }
        }

        /// <summary>
        /// merr  tatime sipas id
        /// </summary>
        /// <param name="idnderm">idndermarje</param>
        /// <param name="idtatime"> id tatime</param>
        /// <returns> kthen data row me kete tatime</returns>
        internal DataRow merrTatimeSipasNdermarjesDR(int idnderm, int idtatime)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDTATIME", idtatime, ParameterDirection.Input);
            using (DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_TATIME_ktheTatimDR"))
            {
                if (ds == null)
                    return null;
                if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                    return null;
                return ds.Tables[0].Rows[0];
            }
        }

        /// <summary>
        /// merr gjithe TATIME sipas ndermarjes 
        /// </summary>
        /// <param name="idnderm">id ndermarje</param>
        /// <returns> kthen nje datatable me te gjithe tatime e kesaj ndermarje</returns>
        internal DataTable merrTatimeDT(int idnderm)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            using (DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_TATIME_ktheKomponenteDT"))
            {
                return ds.Tables[0];
            }
        }

        /// <summary>
        /// merr gjithe tatime sipas ndermarjes sipas  dates
        /// </summary>
        /// <param name="idnderm">id ndermarje</param>
        /// <param name="data">data e aktivizimit</param>
        /// <returns> kthen nje datatable me te gjithe tatime e kesaj ndermarje</returns>
        internal DataTable merrTatimeNdermarjeDTSipasDates(int idnderm, DateTime data)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            dbManager.AddParameters(1, "@DATA", data, ParameterDirection.Input);
            using (DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_TATIME_ktheTatimeDTSipasDates"))
            {
                return ds.Tables[0];
            }
        }

        /// <summary>
        /// merr gjithe datat e  tatimeve sipas ndermarjes
        /// </summary>
        /// <param name="idnderm">id ndermarje</param>
        /// <returns> kthen nje datatable me te gjithe datat e ndryshimeve te tatimeve</returns>
        internal DataTable merrDataTatimeSipasNdermarjes(int idnderm)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            using (DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_TATIME_merrGjitheDatatSipasNdermarjes"))
            {
                return ds.Tables[0];
            }
        }

        #endregion

        /// <summary>
        /// Metodat e meposhtme i perkasin veprimeve qe kryhen nga objektet clsTipeKontrate dhe colTipeKontrate
        /// </summary>
        #region TIPE KONTRATE

        /// <summary>
        /// Ekzekuton prc_T_TIPKONTRATE_ins per te ruajtur nje objekt clsTipeKontrate ne DB.
        ///<param name="idTipKontrate">Objekt i tipit clsTipeKontrate qe do te ruhet ne DB</param>
        /// <returns> Kthen statusin e perfundimit te ekzekutimit te SP-se perkatese (nje objekt clsMesazh qe tregon nese veprimi eshte kryer me sukses apo jo.<seealso cref="clsMesazh"/>)</returns>
        /// </summary>
        internal clsMesazh ruajTipKontrate(int idTipKontrate, string kodi, int idperdoruesi, int idndermarje, int idstatusdok, string kodiang)
        {
            dbManager.Open();
            dbManager.CreateParameters(6);
            dbManager.AddParameters(0, "@IDTIPKONTRATE", idTipKontrate, ParameterDirection.Output);
            dbManager.AddParameters(1, "@KODI", kodi, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDPERDORUESI", idperdoruesi, ParameterDirection.Input);
            dbManager.AddParameters(3, "@IDNDERMARJE", idndermarje, ParameterDirection.Input);
            dbManager.AddParameters(4, "@IDSTATUSDOK", idstatusdok, ParameterDirection.Input);
            dbManager.AddParameters(5, "@KODIANG", kodiang, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_TIPKONTRATE_ins");
            clsMesazh mesazh = new clsMesazh(true, mesazhRuajtje);
            return mesazh;
        }

        /// <summary>
        /// Ekzekuton prc_T_TIPKONTRATE_upd per te modifikuar nje objekt clsTipeKontrate ne DB.
        ///<param name="idTipKontrate">Objekt i tipit clsTipeKontrate qe do te modifikohet</param>
        /// <returns> Kthen statusin e perfundimit te ekzekutimit te SP-se perkatese (nje objekt clsMesazh qe tregon nese veprimi eshte kryer me sukses apo jo.<seealso cref="clsMesazh"/>)</returns>
        /// </summary>
        internal clsMesazh modifikoTipKontrate(int idTipKontrate, string kodi, int idperdoruesi, int idndermarje, int idstatusdok, string kodiang)
        {
            dbManager.Open();
            dbManager.CreateParameters(6);
            dbManager.AddParameters(0, "@IDTIPKONTRATE", idTipKontrate, ParameterDirection.Input);
            dbManager.AddParameters(1, "@KODI", kodi, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDPERDORUESI", idperdoruesi, ParameterDirection.Input);
            dbManager.AddParameters(3, "@IDNDERMARJE", idndermarje, ParameterDirection.Input);
            dbManager.AddParameters(4, "@IDSTATUSDOK", idstatusdok, ParameterDirection.Input);
            dbManager.AddParameters(5, "@KODIANG", kodiang, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_TIPKONTRATE_upd");
            clsMesazh mesazh = new clsMesazh(true, mesazhModifikimi);
            return mesazh;
        }

        /// <summary>
        /// Ekzekuton prc_T_TIPKONTRATE_del per te fshire nje objekt clsTipeKontrate ne DB.
        ///<param name="idTipKontrate">Objekt i tipit clsTipeKontrate qe do te fshihet</param>
        /// <returns> Kthen statusin e perfundimit te ekzekutimit te SP-se perkatese (nje objekt clsMesazh qe tregon nese veprimi eshte kryer me sukses apo jo.<seealso cref="clsMesazh"/>)</returns>
        /// </summary>
        internal clsMesazh fshiTipKontrate(int idTipKontrate)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDTIPKONTRATE", idTipKontrate, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_TIPKONTRATE_del");
            clsMesazh mesazh = new clsMesazh(true, mesazhFshirje);
            return mesazh;
        }

        /// <summary>
        /// fshin tipin e kontrates duke i ndryshuar statusin
        /// </summary>
        /// <param name="idTipKontrate"> id e tip kontrate</param>
        /// <param name="idperdorues">idperdoruesit qe ka kryer veprimin</param>
        /// <returns> clsMesazh me statusin ne eshte kryer veprimi apo jo</returns>
        internal clsMesazh fshiTipKontrateStatus(int idTipKontrate, int idperdorues)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDTIPKONTRATE", idTipKontrate, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUESI", idperdorues, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_TIPKONTRATE_upddel");
            clsMesazh mesazh = new clsMesazh(true, mesazhFshirje);
            return mesazh;
        }

        /// <summary>
        /// Merr gjithe objektet clsTipeKontrate ne DB.
        /// <param name="idnderm"> id e ndermarjes</param>
        /// <returns> Kthen nje collection me objekte clsTipeKontrate</returns>
        /// </summary>
        internal DataTable ktheGjitheTipeKontrateSipasNdermarjes(int idnderm)
        {//metoda per te marre te gjithe grupet

            dbManager.Open();
            using (DataSet ds = dbManager.ExecuteDataSet(CommandType.Text, "select * from T_TIPKONTRATE where idstatusdok=1 and IDNDERMARJE=" + idnderm))
            {
                return ds.Tables[0];
            }
        }

        /// <summary>
        /// Ekzekuton prc_T_TIPKONTRATE_merrTipKontrateSipasKodit per te marre nje objekt clsTipeKontrate ne DB duke filtruar sipas numrit te tipit te kontrares
        ///<param name="kodi">Numri(kodi) i tipit te kontrates</param>
        ///<param name="idnderm">id e ndermarjes</param>
        /// <returns> Kthen nje collection me objekte clsTipeKontrate qe plotesojne kushtin</returns>
        /// </summary>
        internal DataRow ktheTipeKontrateSipasKodit(string kodi, int idnderm)
        {//metoda per te marre te grupin sipas kodit

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@KODI", kodi, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            using (DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_TIPKONTRATE_merrTipKontrateSipasKodit"))
            {
                if (ds == null)
                    return null;
                if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                    return null;
                return ds.Tables[0].Rows[0];
            }

        }
        internal DataRow ktheTipeKontrateSipasKoditAng(string kodi, int idnderm)
        {//metoda per te marre te grupin sipas kodit

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@KODIANG", kodi, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            using (DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_TIPKONTRATE_merrTipKontrateSipasKoditAng"))
            {
                if (ds == null)
                    return null;
                if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                    return null;
                return ds.Tables[0].Rows[0];
            }

        }

        /// <summary>
        /// Ekzekuton prc_T_TIPKONTRATE_ktheTipKontrateSipasId per te marre nje objekt clsTipeKontrate ne DB duke filtruar sipas id-se te tipit te kontrates.
        ///<param name="id">ID e tipit te kontrates</param>
        /// <returns> Kthen nje collection me objekte clsTipeKontrate qe plotesojne kushtin</returns>
        /// </summary>
        internal DataRow merrTipeKontrateSipasId(int id)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDTIPKONTRATE", id, ParameterDirection.Input);
            using (DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_TIPKONTRATE_ktheTipKontrateSipasId"))
            {
                if (ds == null)
                    return null;
                if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                    return null;
                return ds.Tables[0].Rows[0];
            }
        }

        /// <summary>
        /// Ekzekuton prc_T_TIPKONTRATE_ekzistonTipKontrate per te kontrolluar nese ekziston nje objekt clsTipeKontrate ne DB duke filtruar sipas numrit te tipit t kontrates
        ///<param name="nr">Numri(kodi) i tipit te kontrates</param>
        ///<param name="idnderm">id e ndermarjes</param>
        /// <returns> Kthen true nese ekziston nje objekt clsTipeKontrate qe ploteson kushtin</returns>
        /// </summary>
        public bool ekzistonTipKontrate(string kodi, int idnderm)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@KODI", kodi, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            using (DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_TIPKONTRATE_ekzistonTipKontrate"))
            {
                if (ds.Tables[0].Rows.Count == 1)
                    return true;
                if (ds.Tables[0].Rows.Count == 0)
                    return false;
                return true;
            }
        }
        public bool ekzistonTipKontrateAng(string kodiang, int idnderm)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@KODIAng", kodiang, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            using (DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_TIPKONTRATE_ekzistonTipKontrateAng"))
            {
                if (ds.Tables[0].Rows.Count == 1)
                    return true;
                if (ds.Tables[0].Rows.Count == 0)
                    return false;
                return true;
            }
        }

        /// <summary>
        /// Ekzekuton prc_T_TIPKONTRATE_kaPunesim per te kontrolluar nese ekziston nje tip kontrate i caktuar ka punesim apo jo.
        ///<param name="id">Id e tipit te kontrates</param>
        /// <returns> Kthen true nese ky tipi i kontrates eshte perdorur ne nje punesim</returns>
        /// </summary>
        public bool kaPunesim(int id)
        {

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDTIPKONTRATE", id, ParameterDirection.Input);
            using (DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_TIPKONTRATE_kaPunesim"))
            {
                if (ds.Tables[0].Rows.Count == 1)
                    return true;
                if (ds.Tables[0].Rows.Count == 0)
                    return false;
                return true;
            }
        }

        #endregion

        /// <summary>
        /// Metodat e meposhtme i perkasin veprimeve qe kryhen nga objektet clsSkemaSigurimi dhe colSkemaSigurimi
        /// </summary>
        #region SKEMA SIGURIMI

        /// <summary>
        /// ekzekuton 	[prc_T_SKEMASIGURIMI_ins] ne DB dhe kthen statusin e perfundimit te ekzekutimit te SP-se perkatese
        /// Shiko clsMesazh brenda ketij moduli per informacion te metejshem mbi vleren e kthimit te kesaj metode
        /// </summary>
        /// <param name="idSkemaSig"> id e skemes se sigurimeve</param>
        /// <param name="idPunonjes">id e punonjesve</param>
        /// <param name="idSigurimi">id e sigurimit</param>
        /// <param name="dtaktivizimi">data e aktivizimit</param>
        /// <returns>nje objekt clsMesazh qe tregon nese ruajtja eshte kryer ne rregull apo jo.</returns>
        internal clsMesazh ruajSkeme(out int idSkemaSig, int idPunonjes, int idSigurimi, DateTime dtaktivizimi, int idperdoruesi)
        {
            idSkemaSig = -1;

            dbManager.Open();
            dbManager.CreateParameters(5);
            dbManager.AddParameters(0, "@IDSKEMASIG", idSkemaSig, ParameterDirection.Output);
            dbManager.AddParameters(1, "@IDPUNONJES", idPunonjes, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDSIGURIMI", idSigurimi, ParameterDirection.Input);
            dbManager.AddParameters(3, "@DTAKTIVIZIMI", dtaktivizimi, ParameterDirection.Input);
            dbManager.AddParameters(4, "@IDPERDORUESI", idperdoruesi, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_SKEMASIGURIMI_ins");
            idSkemaSig = int.Parse(dbManager.Parameters[0].Value.ToString());
            return new clsMesazh(true, mesazhRuajtje);

        }

        /// <summary>
        /// ekzekuton prc_T_SKEMASIGURIMI_upd ne DB dhe kthen statusin e perfundimit te ekzekutimit te SP-se perkatese
        /// Shiko clsMesazh brenda ketij moduli per informacion te metejshem mbi vleren e kthimit te kesaj metode
        /// <param name="idSkemaSig"> id e skemes se sigurimeve</param>
        /// <param name="idPunonjes">id e punonjesve</param>
        /// <param name="idSigurimi">id e sigurimit</param>
        /// <param name="dtaktivizimi">data e aktivizimit</param>
        /// <returns> nje objekt clsMesazh qe tregon nese modifikimi eshte kryer ne rregull apo jo.</returns>
        /// </summary>
        internal clsMesazh modifikoSkeme(int idSkemaSig, int idPunonjes, int idSigurimi, DateTime dtaktivizimi, int idperdoruesi)
        {

            dbManager.Open();
            dbManager.CreateParameters(5);
            dbManager.AddParameters(0, "@IDSKEMASIG", idSkemaSig, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPUNONJES", idPunonjes, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDSIGURIMI", idSigurimi, ParameterDirection.Input);
            dbManager.AddParameters(3, "@DTAKTIVIZIMI", dtaktivizimi, ParameterDirection.Input);
            dbManager.AddParameters(4, "@IDPERDORUESI", idperdoruesi, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_SKEMASIGURIMI_upd");
            return new clsMesazh(true, mesazhModifikimi);

        }

        /// <summary>
        /// ekzekutohet sp-ja prc_T_SKEMASIGURIMI_del duke i kaluar id e skemes se sigurimeve
        /// </summary>
        /// <param name="idskemasig">id e skemes sig qe do fshihet </param>
        /// <returns>nje objekt clsMesazh qe tregon nese fshirja eshte kryer ne rregull apo jo</returns>
        internal clsMesazh fshiSkeme(int idskemasig)
        {

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDSKEMASIG", idskemasig, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_SKEMASIGURIMI_del");
            clsMesazh mesazh = new clsMesazh(true, mesazhFshirje);
            return mesazh;
        }

        /// <summary>
        /// merr skemen e sigurimit sipas id
        /// </summary>
        /// <param name="idskemaSig">  id e skemes se sigurimit</param>
        /// <returns> kthen datarow qe permban tatimin me kete id</returns>
        internal DataRow ktheSkeme(int idskemaSig)
        {

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDSKEMASIG", idskemaSig, ParameterDirection.Input);
            using (DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_SKEMASIGURIMI_sel"))
            {
                if (ds == null)
                    return null;
                if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                    return null;
                return ds.Tables[0].Rows[0];
            }
        }

        /// <summary>
        /// merr gjithe skemat e sigurimit te nje punonjesit sipas  dates
        /// </summary>
        /// <param name="idpunonjes"> id e punonjesit</param>
        /// <param name="data"> data e aktivizimit</param>
        /// <returns> nje datatable qe permban nje koleksion me te sigurimin e punonjesit ne ate date</returns>
        internal DataRow ktheSkemeSigurimiSipasPunonjesitDheDates(int idpunonjes, DateTime data)
        {

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDPUNONJES", idpunonjes, ParameterDirection.Input);
            dbManager.AddParameters(1, "@DTAKTIVIZIMI", data, ParameterDirection.Input);
            using (DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_SKEMASIGURIMI_merrSkemeSigSipasIdPunonjesiDheDate"))
            {
                if (ds == null)
                    return null;
                if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                    return null;
                return ds.Tables[0].Rows[0];
            }
        }

        /// <summary>
        /// merr gjithe skemat e sigurimit te nje punonjesit sipas  dates
        /// </summary>
        /// <param name="idpunonjes"> id e punonjesit</param>
        /// <param name="data"> data e aktivizimit</param>
        /// <returns> nje datatable qe permban nje koleksion me te sigurimin e punonjesit ne ate date</returns>
        internal DataRow ktheSkemeSigurimiSipasPunonjesitDheDatesDeri(int idpunonjes, DateTime data)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDPUNONJES", idpunonjes, ParameterDirection.Input);
            dbManager.AddParameters(1, "@DTAKTIVIZIMI", data, ParameterDirection.Input);
            using (DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_SKEMASIGURIMI_merrSkemeSigSipasIdPunonjesiDheDateDeri"))
            {
                if (ds == null)
                    return null;
                if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                    return null;
                return ds.Tables[0].Rows[0];
            }
        }
        public IEnumerable<clsSkemaSigurimi> MerrSigurimetPerGjithePunonjesitSipasDates(List<int> idPunonjesish, DateTime data)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDPUNONJESISH", string.Join(",", idPunonjesish), ParameterDirection.Input);
            dbManager.AddParameters(1, "@DTAKTIVIZIMI", data, ParameterDirection.Input);
            return dbManager.GetIEnumerbale("prc_T_SKEMASIGURIMI_merrSkemeSigSipasIdPunonjesishDheDateDeri", clsSkemaSigurimi.Krijo);
        }
        #endregion

        /// <summary>
        /// Metodat e meposhtme i perkasin veprimeve qe kryhen nga objektet clsPagaShtesa dhe colPagaShtesa
        /// </summary>
        #region PAGA DHE SHTESA

        /// <summary>
        /// ekzekuton 	[prc_T_PAGASHTESA_ins] ne DB dhe kthen statusin e perfundimit te ekzekutimit te SP-se perkatese
        /// Shiko clsMesazh brenda ketij moduli per informacion te metejshem mbi vleren e kthimit te kesaj metode
        /// </summary>
        /// <param name="idpagashtesa"> id e pages dhe shtesa</param>
        /// <param name="idPunonjes">id e punonjesve</param>
        /// <param name="idkomponente">id e komponentes te pages</param>
        /// <param name="dtaktivizimi">data e aktivizimit</param>
        /// <param name="vlera"> vlera e komponentes</param>
        /// <param name="vleraparam"> vlera e parametrit te komponentes</param>
        /// <returns>nje objekt clsMesazh qe tregon nese ruajtja eshte kryer ne rregull apo jo.</returns>
        internal clsMesazh ruajPagaShtesa(out int idpagashtesa, int idPunonjes, int idkomponente, DateTime dtaktivizimi, decimal vleraparam, decimal vlera, int idperdoruesi)
        {
            idpagashtesa = -1;
            string salt = WebConfigurationManager.AppSettings["salt"];
            dbManager.Open();
            dbManager.CreateParameters(8);
            dbManager.AddParameters(0, "@IDPAGASHTESA", idpagashtesa, ParameterDirection.Output);
            dbManager.AddParameters(1, "@IDPUNONJES", idPunonjes, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDKOMPONENTE", idkomponente, ParameterDirection.Input);
            dbManager.AddParameters(3, "@DTAKTIVIZIMI", dtaktivizimi, ParameterDirection.Input);
            dbManager.AddParameters(4, "@VLERAPARAM", vleraparam, ParameterDirection.Input);
            dbManager.AddParameters(5, "@VLERA", vlera, ParameterDirection.Input);
            dbManager.AddParameters(6, "@IDPERDORUESI", idperdoruesi, ParameterDirection.Input);
            dbManager.AddParameters(7, "@salt", salt, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_PAGASHTESA_ins");
            idpagashtesa = int.Parse(dbManager.Parameters[0].Value.ToString());
            return new clsMesazh(true, mesazhRuajtje);
        }

        /// <summary>
        /// ekzekuton prc_T_PAGASHTESA_upd ne DB dhe kthen statusin e perfundimit te ekzekutimit te SP-se perkatese
        /// Shiko clsMesazh brenda ketij moduli per informacion te metejshem mbi vleren e kthimit te kesaj metode
        /// <param name="idpagashtesa"> id e pages dhe shtesa</param>
        /// <param name="idPunonjes">id e punonjesve</param>
        /// <param name="idkomponente">id e komponentes te pages</param>
        /// <param name="dtaktivizimi">data e aktivizimit</param>
        /// <param name="vlera"> vlera e komponentes</param>
        /// <param name="vleraparam"> vlera e parametrit te komponentes</param>
        /// <returns> nje objekt clsMesazh qe tregon nese modifikimi eshte kryer ne rregull apo jo.</returns>
        /// </summary>
        internal clsMesazh modifikoPagaShtesa(int idpagashtesa, int idPunonjes, int idkomponente, DateTime dtaktivizimi, decimal vleraparam, decimal vlera, int idperdoruesi)
        {
            string salt = WebConfigurationManager.AppSettings["salt"];
            dbManager.Open();
            dbManager.CreateParameters(8);
            dbManager.AddParameters(0, "@IDPAGASHTESA", idpagashtesa, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPUNONJES", idPunonjes, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDKOMPONENTE", idkomponente, ParameterDirection.Input);
            dbManager.AddParameters(3, "@DTAKTIVIZIMI", dtaktivizimi, ParameterDirection.Input);
            dbManager.AddParameters(4, "@VLERAPARAM", vleraparam, ParameterDirection.Input);
            dbManager.AddParameters(5, "@VLERA", vlera, ParameterDirection.Input);
            dbManager.AddParameters(6, "@IDPERDORUESI", idperdoruesi, ParameterDirection.Input);
            dbManager.AddParameters(7, "@salt", salt, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_PAGASHTESA_upd");
            return new clsMesazh(true, mesazhModifikimi);
        }

        /// <summary>
        /// ekzekutohet sp-ja prc_T_PAGASHTESA_del duke i kaluar id e pages dhe shtesa
        /// </summary>
        /// <param name="idpagashtesa">id e paga shtesa qe do fshihet </param>
        /// <returns>nje objekt clsMesazh qe tregon nese fshirja eshte kryer ne rregull apo jo</returns>
        internal clsMesazh fshiPagaShtesa(int idpagashtesa)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDPAGASHTESA", idpagashtesa, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_PAGASHTESA_del");
            clsMesazh mesazh = new clsMesazh(true, mesazhFshirje);
            return mesazh;
        }

        /// <summary>
        /// merr pagen dhe shtesen sipas id
        /// </summary>
        /// <param name="idpagashtesa">  id e paga dhe shtesa</param>
        /// <returns> kthen datarow qe permban paga shtesa me kete id</returns>
        internal void kthePagaShtesa(int idpagashtesa, clsPagaShtesa pagaShtesa)
        {
            string salt = WebConfigurationManager.AppSettings["salt"];
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDPAGASHTESA", idpagashtesa, ParameterDirection.Input);
            dbManager.AddParameters(1, "@salt", salt, ParameterDirection.Input);
            dbManager.FillObject<clsPagaShtesa>("prc_T_PAGASHTESA_sel", pagaShtesa.mbushPagaShtesa);
        }
        internal DataTable merrPagaDheShtesaPerExport(int idndermarje)
        {

            dbManager.Open();
            string salt = WebConfigurationManager.AppSettings["salt"];
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDNDERMARJE", idndermarje, ParameterDirection.Input);
            dbManager.AddParameters(1, "@salt", salt, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_PAGASHTESA_export");

            return ds.Tables[0];

        }

        /// <summary>
        /// merr gjithe paga shtesa te nje punonjesit sipas  dates
        /// </summary>
        /// <param name="idpunonjes"> id e punonjesit</param>
        /// <param name="data"> data e aktivizimit</param>
        /// <returns> nje datatable qe permban nje koleksion me te paga shtesa ne ate date</returns>
        internal IEnumerable<clsPagaShtesa> kthePagaShtesaSipasPunonjesitDheDates(int idpunonjes, DateTime data)
        {
            string salt = WebConfigurationManager.AppSettings["salt"];
            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@IDPUNONJES", idpunonjes, ParameterDirection.Input);
            dbManager.AddParameters(1, "@DTAKTIVIZIMI", data, ParameterDirection.Input);
            dbManager.AddParameters(2, "@salt", salt, ParameterDirection.Input);
            return dbManager.GetIEnumerbale("prc_T_PAGASHTESA_merrPagaDheShtesaSipasIdPunonjesiDheDate", clsPagaShtesa.Krijo);

        }

        internal IEnumerable<clsPagaShtesa> kthePagaShtesaSipasPunonjesitDheDatesDheKompoenetes(int idpunonjes, DateTime data, int idkomponentes)
        {
            string salt = WebConfigurationManager.AppSettings["salt"];
            dbManager.Open();
            dbManager.CreateParameters(4);
            dbManager.AddParameters(0, "@IDPUNONJES", idpunonjes, ParameterDirection.Input);
            dbManager.AddParameters(1, "@DTAKTIVIZIMI", data, ParameterDirection.Input);
            dbManager.AddParameters(2, "@salt", salt, ParameterDirection.Input);
            dbManager.AddParameters(3, "@idkomponente", idkomponentes, ParameterDirection.Input);

            return dbManager.GetIEnumerbale("prc_T_PAGASHTESA_merrPagaDheShtesaSipasIdPunonjesiDheDateDheKomponente", clsPagaShtesa.Krijo);
        }

        /// <summary>
        /// merr gjithe paga shtesa te nje punonjesit sipas  dates
        /// </summary>
        /// <param name="idpunonjes"> id e punonjesit</param>
        /// <param name="data"> data e aktivizimit</param>
        /// <returns> nje datatable qe permban nje koleksion me te paga shtesa ne ate date</returns>
        internal IEnumerable<clsPagaShtesa> kthePagaShtesaSipasPunonjesitDheDatesMeTefundit(int idpunonjes, DateTime data)
        {
            string salt = WebConfigurationManager.AppSettings["salt"];
            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@IDPUNONJES", idpunonjes, ParameterDirection.Input);
            dbManager.AddParameters(1, "@DTAKTIVIZIMI", data, ParameterDirection.Input);
            dbManager.AddParameters(2, "@salt", salt, ParameterDirection.Input);
            return dbManager.GetIEnumerbale("prc_T_PAGASHTESA_merrPagaDheShtesaSipasIdPunonjesiDheDateMeTeFundit", clsPagaShtesa.Krijo);

        }
        internal IEnumerable<clsPagaShtesa> kthePagaShtesaSipasPunonjesveDheDatesMeTefundit(DataTable punonjesitMeData)
        {
            string salt = WebConfigurationManager.AppSettings["salt"];
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters("@PUNONJESITMEDATA", punonjesitMeData, ParameterDirection.Input);
            dbManager.AddParameters("@salt", salt, ParameterDirection.Input);
            return dbManager.GetIEnumerbale("prc_T_PAGASHTESA_merrPagaDheShtesaSipasIdPunonjesveDheDateMeTeFundit", clsPagaShtesa.Krijo);

        }

        /// <summary>
        /// merr gjithe paga shtesa te fillestare
        /// </summary>
        /// <param name="idndermarje">id e ndermarje</param>
        /// <param name="lloji">lloji i komponentes</param>
        /// <param name="data"> data e aktivizimit</param>
        /// <returns> nje datatable qe permban nje koleksion me te paga shtesa ne ate date</returns>
        internal IEnumerable<clsPagaShtesa> kthePagaShtesaFillestare(bool lloji, DateTime data, int idndermarje)
        {
            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@LLOJI", lloji, ParameterDirection.Input);
            dbManager.AddParameters(1, "@DTAKTIVIZIMI", data, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDNDERMARJE", idndermarje, ParameterDirection.Input);
            return dbManager.GetIEnumerbale("prc_T_PAGASHTESA_merrPagaDheShtesaFillestare", clsPagaShtesa.Krijo);
        }

        /// <summary>
        /// kthen paga shtesa sipas lloji te komponentes, dates, idndermarje, dt se ndryshimit dhe idpunonjesi
        /// </summary>
        /// <param name="lloji"></param>
        /// <param name="data"></param>
        /// <param name="idndermarje"></param>
        /// <param name="dtndryshimi"></param>
        /// <param name="idpunonjes"></param>
        /// <returns></returns>
        internal IEnumerable<clsPagaShtesa> kthePagaShtesaSipasPunonjesitDatesLlojitNdermarjes(bool lloji, DateTime data, int idndermarje, DateTime dtndryshimi, int idpunonjes)
        {
            string salt = WebConfigurationManager.AppSettings["salt"];
            dbManager.Open();
            dbManager.CreateParameters(6);
            dbManager.AddParameters(0, "@LLOJI", lloji, ParameterDirection.Input);
            dbManager.AddParameters(1, "@DTAKTIVIZIMI", data, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDNDERMARJE", idndermarje, ParameterDirection.Input);
            dbManager.AddParameters(3, "@DTNDRYSHIMI", dtndryshimi, ParameterDirection.Input);
            dbManager.AddParameters(4, "@IDPUNONJES", idpunonjes, ParameterDirection.Input);
            dbManager.AddParameters(5, "@salt", salt, ParameterDirection.Input);
            return dbManager.GetIEnumerbale("prc_T_PAGASHTESA_merrPagaDheShtesaSipasPunonjesNdermarjeDateLloji", clsPagaShtesa.Krijo);

        }

        /// <summary>
        /// merr gjithe datat e  paga shtesa sipas idpunonjesi
        /// </summary>
        /// <param name="idpunonjes">id punonjes</param>
        /// <returns> kthen nje datatable me te gjithe datat e ndryshimeve te paga shtesa</returns>
        internal DataTable merrDataPagaShtesaSipasIdPunonjes(int idpunonjes)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@idpunonjes", idpunonjes, ParameterDirection.Input);
            using (DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_PAGASHTESA_merrGjitheDatatSipasIdPunonjes"))
            {
                return ds.Tables[0];
            }
        }

        #endregion

        /// <summary>
        /// Metodat e meposhtme i perkasin veprimeve qe kryhen nga objektet clsKomponenteListPagesePunonjes dhe colKompoenenteListPagesePunonjes
        /// </summary>
        #region KOMPONENTE LISTPAGESE PUNONJES

        /// <summary>
        /// ekzekuton 	[prc_T_KOMPONENTELISTPAGESEPUNONJES_ins] ne DB dhe kthen statusin e perfundimit te ekzekutimit te SP-se perkatese
        /// Shiko clsMesazh brenda ketij moduli per informacion te metejshem mbi vleren e kthimit te kesaj metode
        /// </summary>
        /// <param name="idkompoentelistpagesepunonjes"> id e komponente listpagese punonjes</param>
        /// <param name="idPunonjes">id e punonjesve</param>
        /// <param name="idkomponente">id e komponentes te pages</param>
        /// <param name="dtaktivizimi">data e aktivizimit</param>
        /// <param name="edukshme">  e dukshme</param>
        /// <returns>nje objekt clsMesazh qe tregon nese ruajtja eshte kryer ne rregull apo jo.</returns>
        internal clsMesazh ruajKomponenteListPagesePunonjes(out int idkompoentelistpagesepunonjes, int idPunonjes, int idkomponente, DateTime dtaktivizimi, bool edukshme, bool edetyrueshme, int idperdoruesi, decimal vlera)
        {
            idkompoentelistpagesepunonjes = -1;
            string salt = WebConfigurationManager.AppSettings["salt"];
            dbManager.Open();
            dbManager.CreateParameters(9);
            dbManager.AddParameters(0, "@IDKOMPLISTPAGESE", idkompoentelistpagesepunonjes, ParameterDirection.Output);
            dbManager.AddParameters(1, "@IDPUNONJES", idPunonjes, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDKOMPONENTE", idkomponente, ParameterDirection.Input);
            dbManager.AddParameters(3, "@DTAKTIVIZIMI", dtaktivizimi, ParameterDirection.Input);
            dbManager.AddParameters(4, "@EDUKSHME", edukshme, ParameterDirection.Input);
            dbManager.AddParameters(5, "@EDETYRUESHME", edetyrueshme, ParameterDirection.Input);
            dbManager.AddParameters(6, "@IDPERDORUESI", idperdoruesi, ParameterDirection.Input);
            dbManager.AddParameters(7, "@VLERADEFAULT", vlera, ParameterDirection.Input);
            dbManager.AddParameters(8, "@salt", salt, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_KOMPONENTELISTPAGESEPUNONJES_ins");
            idkompoentelistpagesepunonjes = int.Parse(dbManager.Parameters[0].Value.ToString());
            return new clsMesazh(true, mesazhRuajtje);
        }

        /// <summary>
        /// ekzekuton prc_T_KOMPONENTELISTPAGESEPUNONJES_upd ne DB dhe kthen statusin e perfundimit te ekzekutimit te SP-se perkatese
        /// Shiko clsMesazh brenda ketij moduli per informacion te metejshem mbi vleren e kthimit te kesaj metode
        /// <param name="idkompoentelistpagesepunonjes"> id e komponente listpagese punonjes</param>
        /// <param name="idPunonjes">id e punonjesve</param>
        /// <param name="idkomponente">id e komponentes te pages</param>
        /// <param name="dtaktivizimi">data e aktivizimit</param>
        /// <param name="edukshme">  e dukshme</param>
        /// <returns> nje objekt clsMesazh qe tregon nese modifikimi eshte kryer ne rregull apo jo.</returns>
        /// </summary>
        internal clsMesazh modifikoKomponenteListPagesePunonjes(int idkompoentelistpagesepunonjes, int idPunonjes, int idkomponente, DateTime dtaktivizimi, bool edukshme, bool edetyrueshme, int idperdoruesi, decimal vlera)
        {
            string salt = WebConfigurationManager.AppSettings["salt"];
            dbManager.Open();
            dbManager.CreateParameters(9);
            dbManager.AddParameters(0, "@IDKOMPLISTPAGESE", idkompoentelistpagesepunonjes, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPUNONJES", idPunonjes, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDKOMPONENTE", idkomponente, ParameterDirection.Input);
            dbManager.AddParameters(3, "@DTAKTIVIZIMI", dtaktivizimi, ParameterDirection.Input);
            dbManager.AddParameters(4, "@EDUKSHME", edukshme, ParameterDirection.Input);
            dbManager.AddParameters(5, "@EDETYRUESHME", edetyrueshme, ParameterDirection.Input);
            dbManager.AddParameters(6, "@IDPERDORUESI", idperdoruesi, ParameterDirection.Input);
            dbManager.AddParameters(7, "@VLERADEFAULT", vlera, ParameterDirection.Input);
            dbManager.AddParameters(8, "@salt", salt, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_KOMPONENTELISTPAGESEPUNONJES_upd");
            return new clsMesazh(true, mesazhModifikimi);
        }

        /// <summary>
        /// ekzekutohet sp-ja prc_T_KOMPONENTELISTPAGESEPUNONJES_del duke i kaluar id e komponente list pagese punonjes
        /// </summary>
        /// <param name="idkompoentelistpagesepunonjes">id e komponentelistpagese punonjesi qe do fshihet </param>
        /// <returns>nje objekt clsMesazh qe tregon nese fshirja eshte kryer ne rregull apo jo</returns>
        internal clsMesazh fshiKomponenteListPagesePunonjes(int idkompoentelistpagesepunonjes, int idPerdoruesi)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDKOMPLISTPAGESE", idkompoentelistpagesepunonjes, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUESI", idPerdoruesi, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_KOMPONENTELISTPAGESEPUNONJES_del");
            clsMesazh mesazh = new clsMesazh(true, mesazhFshirje);
            return mesazh;
        }

        /// <summary>
        /// merr komponente listpagese punonjesi me id
        /// </summary>
        /// <param name="idkompoentelistpagesepunonjes">  id e komponentelist pagese punonjesi</param>
        /// <returns> kthen datarow qe permban komponente listpagese punonjesi me kete id</returns>
        internal void ktheKomponenteListPagesePunonjes(int idkompoentelistpagesepunonjes, IDataBaseReader objekti)
        {
            string salt = WebConfigurationManager.AppSettings["salt"];
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDKOMPLISTPAGESE", idkompoentelistpagesepunonjes, ParameterDirection.Input);
            dbManager.AddParameters(1, "@salt", salt, ParameterDirection.Input);
            dbManager.FillObject("prc_T_KOMPONENTELISTPAGESEPUNONJES_sel", objekti);

        }
        internal DataTable merrKomponenteListPagesePunonjesiPerExport(int idndermarje)
        {

            dbManager.Open();
            string salt = WebConfigurationManager.AppSettings["salt"];
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDNDERMARJE", idndermarje, ParameterDirection.Input);
            dbManager.AddParameters(1, "@salt", salt, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KOMPONENTELISTPAGESEPUNONJES_export");

            return ds.Tables[0];

        }
        /// <summary>
        /// merr gjithe komponente listpagese  te nje punonjesit sipas  dates
        /// </summary>
        /// <param name="idpunonjes"> id e punonjesit</param>
        /// <param name="data"> data e aktivizimit</param>
        /// <returns> nje datatable qe permban nje koleksion me te komponente listpagese te punonjesit ne ate date</returns>
        internal void ktheKomponenteListPagesePunonjesSipasPunonjesitDheDates(int idpunonjes, DateTime data, IDataBaseReader objekti)
        {
            string salt = WebConfigurationManager.AppSettings["salt"];
            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@IDPUNONJES", idpunonjes, ParameterDirection.Input);
            dbManager.AddParameters(1, "@DTAKTIVIZIMI", data, ParameterDirection.Input);
            dbManager.AddParameters(2, "@salt", salt, ParameterDirection.Input);
            dbManager.FillCollection("prc_T_KOMPONENTELISTPAGESEPUNONJES_merrKomponenteListPagesePunonjesSipasIdPunonjesiDheDate", objekti);


        }
        internal void ktheKomponenteListPagesePunonjesSipasPunonjesitDheDates(int idpunonjes, DateTime data, string komponenteKodi, IDataBaseReader objekti)
        {
            string salt = WebConfigurationManager.AppSettings["salt"];
            dbManager.Open();
            dbManager.CreateParameters(4);
            dbManager.AddParameters(0, "@IDPUNONJES", idpunonjes, ParameterDirection.Input);
            dbManager.AddParameters(1, "@DTAKTIVIZIMI", data, ParameterDirection.Input);
            dbManager.AddParameters(2, "@salt", salt, ParameterDirection.Input);
            dbManager.AddParameters(3, "@komponenteKodi", komponenteKodi, ParameterDirection.Input);
            dbManager.FillCollection("prc_T_KOMPONENTELISTPAGESEPUNONJES_merrKomponenteListPagesePunonjesSipasIdPunonjesiDheDateDheKomponentes", objekti);


        }

        /// <summary>
        /// merr gjithe komponente listpagese  fillestare
        /// </summary>
        /// <param name="idndermarje">id e ndermarje</param>
        /// <param name="lloji">lloji i komponentes</param>
        /// <param name="data"> data e aktivizimit</param>
        /// <returns> nje datatable qe permban nje koleksion me te komponente listpagese te punonjesit ne ate date</returns>
        internal void ktheKomponenteListPagesePunonjesiFillestare(bool lloji, DateTime data, int idndermarje, IDataBaseReader objekti)
        {
            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@LLOJI", lloji, ParameterDirection.Input);
            dbManager.AddParameters(1, "@DTAKTIVIZIMI", data, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDNDERMARJE", idndermarje, ParameterDirection.Input);
            dbManager.FillCollection("prc_T_KOMPONENTELISTPAGESEPUNONJES_merrKomponenteListPagesePunonjesFillestare", objekti);

        }

        /// <summary>
        /// merr gjithe komponente listpagese  fillestare
        /// </summary>
        /// <param name="idndermarje">id e ndermarje</param>
        /// <param name="lloji">lloji i komponentes</param>
        /// <param name="data"> data e aktivizimit</param>
        /// <returns> nje datatable qe permban nje koleksion me te komponente listpagese te punonjesit ne ate date</returns>
        internal void ktheKomponenteListPagesePunonjesiSipasPunonjesitDatesLlojitNdermarjes(bool lloji, DateTime data, int idndermarje, DateTime dtndryshimi, int idpunonjes, IDataBaseReader objekti)
        {
            string salt = WebConfigurationManager.AppSettings["salt"];

            dbManager.Open();
            dbManager.CreateParameters(6);
            dbManager.AddParameters(0, "@LLOJI", lloji, ParameterDirection.Input);
            dbManager.AddParameters(1, "@DTAKTIVIZIMI", data, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDNDERMARJE", idndermarje, ParameterDirection.Input);
            dbManager.AddParameters(3, "@DTNDRYSHIMI", dtndryshimi, ParameterDirection.Input);
            dbManager.AddParameters(4, "@IDPUNONJES", idpunonjes, ParameterDirection.Input);
            dbManager.AddParameters(5, "@salt", salt, ParameterDirection.Input);
            dbManager.FillCollection("prc_T_KOMPONENTELISTPAGESEPUNONJES_merrKomponenteListPagesePunonjesSipasIdPunonjesiDheDatedhellojitdhendermarjes", objekti);

        }
        internal void ktheKomponenteListPagesePunonjesiSipasPunonjesitDatesMeTeFundit(DateTime data, int idpunonjes, IDataBaseReader objekti)
        {
            string salt = WebConfigurationManager.AppSettings["salt"];
            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@DTAKTIVIZIMI", data, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPUNONJES", idpunonjes, ParameterDirection.Input);
            dbManager.AddParameters(2, "@salt", salt, ParameterDirection.Input);
            dbManager.FillCollection("prc_T_KOMPONENTELISTPAGESEPUNONJES_merrKomponenteListPagesePunonjesSipasIdPunonjesiMeTeFundit", objekti);

        }
        internal List<string> ktheKomponenteListPagesePunonjesiTeDetyrueshme(DateTime data, int idpunonjes)
        {

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@DTAKTIVIZIMI", data, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPUNONJES", idpunonjes, ParameterDirection.Input);
            return dbManager.GetList<string>("prc_T_KOMPONENTELISTPAGESEPUNONJES_merrKomponenteTeDetyrueshmePunonjesi");

        }

        internal decimal MerrVlereDefaultPerKomponente(string kodi, int idPunonjes, DateTime data)
        {
            string salt = WebConfigurationManager.AppSettings["salt"];
            dbManager.Open();
            dbManager.CreateParameters(4);
            dbManager.AddParameters(0, "@KODI", kodi, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPUNONJES", idPunonjes, ParameterDirection.Input);
            dbManager.AddParameters(2, "@DATA", data, ParameterDirection.Input);
            dbManager.AddParameters(3, "@SALT", salt, ParameterDirection.Input);

            var result = dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_KOMPONENTELISTPAGESEPUNONJES_merrVlereDefaultSipasKoditDheDates");
            if (null == result) return 0M;
            decimal vlera = 0;
            Converter.Parse(result.ToString(), out vlera, "vlereDefault");
            return vlera;

        }

        internal bool MerrEDukshmePerKomponente(string kodi, int idPunonjes, DateTime data)
        {
            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters("@KODI", kodi, ParameterDirection.Input);
            dbManager.AddParameters("@IDPUNONJES", idPunonjes, ParameterDirection.Input);
            dbManager.AddParameters("@DATE", data, ParameterDirection.Input);

            var result = dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_KOMPONENTELISTPAGESEPUNONJES_merrEDukshmeSipasKoditDheDates");
            if (result == null) return false;
            bool vlera = false;
            Converter.Parse(result.ToString(), out vlera, "eDukshme");
            return vlera;

        }
        /// <summary>
        /// merr gjithe datat e  komponente list pagese sipas idpunonjesi
        /// </summary>
        /// <param name="idpunonjes">id punonjes</param>
        /// <returns> kthen nje datatable me te gjithe datat e ndryshimeve te komponente listpagese</returns>

        internal DataTable merrDataKomponenteListPageseSipasIdPunonjes(int idpunonjes)
        {

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@idpunonjes", idpunonjes, ParameterDirection.Input);
            using (DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KOMPONENTELISTPAGESEPUNONJES_merrGjitheDatatSipasIdPunonjes"))
            {
                return ds.Tables[0];
            }
        }

        internal clsMesazh ModifikoKomponentetPerPunonjes(DateTime dtAktivizimi, int idPunonjesi, int idPerdoruesi, DataTable dt)
        {
            string salt = WebConfigurationManager.AppSettings["salt"];
            dbManager.Open();
            dbManager.CreateParameters(5);
            dbManager.AddInputParameters("@dtAktivizimi", dtAktivizimi);
            dbManager.AddInputParameters("@idPunonjesi", idPunonjesi);
            dbManager.AddInputParameters("@idPerdoruesi", idPerdoruesi);
            dbManager.AddInputParameters("@dtKomponente", dt);
            dbManager.AddInputParameters("@SALT", salt);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_KOMPONENTELISTPAGESEPUNONJES_modifikoSipasDates");
            return new clsMesazh(true, mesazhModifikimi);
        }

        #endregion

        /// <summary>
        /// Metodat e meposhtme i perkasin veprimeve qe kryhen nga objektet clsQendraKostoPunonjes dhe colQendraKostoPunonjes
        /// </summary>
        #region QENDRA KOSTO PUNONJES

        /// <summary>
        /// ekzekuton 	[prc_T_PUNESIM_ins] ne DB dhe kthen statusin e perfundimit te ekzekutimit te SP-se perkatese
        /// Shiko clsMesazh brenda ketij moduli per informacion te metejshem mbi vleren e kthimit te kesaj metode
        /// </summary>
        /// <param name="id"> id e punesimit</param>
        /// <param name="idpunesim"> id e punonjesit</param>
        /// <param name="idqk1"> id e departamentit</param>
        /// <param name="idqk2"> id e nendepartamentit</param>
        /// <param name="detyra">detyra </param>
        /// <param name="nrkontrate">nr i kontrates</param>
        /// <param name="idgrupiglobal"> tipi i kontrates</param>
        /// <param name="dtfillimi"> data e fillimit</param>
        /// <param name="dtperfundimi">data e perfundimit</param>
        /// <param name="llogbankare">llogaria bankare</param>
        /// <param name="idbanka"> idbanka</param>
        /// <param name="larguar">larguar</param>
        /// <param name="dtlargimi"> dt e largimit</param>
        /// <param name="aryeja">arsyesja</param>
        /// <param name="periudhanjoftimi"> periudha e njoftimit</param>
        /// <param name="neprove">ne prove</param>
        /// <param name="periudhaprove"> periudhe ne prove</param>
        ///  /// <param name="idgrupilocal"> grupi i punesimit</param>
        /// <returns>nje objekt clsMesazh qe tregon nese ruajtja eshte kryer ne rregull apo jo.</returns>
        internal clsMesazh ruajQendraKostoPunonjes(out int id, int idpunonjes, int idqk1, int idqk2, DateTime dtaktivizimi, int idperdoruesi)
        {
            id = -1;

            dbManager.Open();
            dbManager.CreateParameters(6);
            dbManager.AddParameters(0, "@ID", id, ParameterDirection.Output);
            dbManager.AddParameters(1, "@IDPUNONJES", idpunonjes, ParameterDirection.Input);
            if (idqk1 == 0)
                dbManager.AddParameters(2, "@IDQENDERKOSTO1", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(2, "@IDQENDERKOSTO1", idqk1, ParameterDirection.Input);
            if (idqk2 == 0) dbManager.AddParameters(3, "@IDQENDERKOSTO2", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(3, "@IDQENDERKOSTO2", idqk2, ParameterDirection.Input);

            dbManager.AddParameters(4, "@DTAKTIVIZIMI", dtaktivizimi, ParameterDirection.Input);
            dbManager.AddParameters(5, "@IDPERDORUESI", idperdoruesi, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_QENDRAKOSTOPUNONJES_ins");
            id = int.Parse(dbManager.Parameters[0].Value.ToString());
            return new clsMesazh(true, mesazhRuajtje);

        }

        /// <summary>
        /// ekzekuton prc_T_PUNESIM_upd ne DB dhe kthen statusin e perfundimit te ekzekutimit te SP-se perkatese
        /// Shiko clsMesazh brenda ketij moduli per informacion te metejshem mbi vleren e kthimit te kesaj metode
        /// <param name="idpunesim"> id e punesimit</param>
        /// <param name="idPunonjes"> id e punonjesit</param>
        /// <param name="iddepartament"> id e departamentit</param>
        /// <param name="idnendepartamet"> id e nendepartamentit</param>
        /// <param name="detyra">detyra </param>
        /// <param name="nrkontrate">nr i kontrates</param>
        /// <param name="idtipkontrate"> tipi i kontrates</param>
        /// <param name="dtfillimi"> data e fillimit</param>
        /// <param name="dtperfundimi">data e perfundimit</param>
        /// <param name="llogbankare">llogaria bankare</param>
        /// <param name="idbanka"> idbanka</param>
        /// <param name="larguar">larguar</param>
        /// <param name="dtlargimi"> dt e largimit</param>
        /// <param name="aryeja">arsyesja</param>
        /// <param name="periudhanjoftimi"> periudha e njoftimit</param>
        /// <param name="neprove">ne prove</param>
        /// <param name="periudhaprove"> periudhe ne prove</param>
        /// <returns> nje objekt clsMesazh qe tregon nese modifikimi eshte kryer ne rregull apo jo.</returns>
        /// </summary>
        internal clsMesazh modifikoQendraKostoPunonjes(int id, int idpunonjes, int idqk1, int idqk2, DateTime dtaktivizimi, int idperdoruesi)
        {

            dbManager.Open();
            dbManager.CreateParameters(6);
            dbManager.AddParameters(0, "@ID", id, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPUNONJES", idpunonjes, ParameterDirection.Input);
            if (idqk1 == 0)
                dbManager.AddParameters(2, "@IDQENDERKOSTO1", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(2, "@IDQENDERKOSTO1", idqk1, ParameterDirection.Input);
            if (idqk2 == 0) dbManager.AddParameters(3, "@IDQENDERKOSTO2", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(3, "@IDQENDERKOSTO2", idqk2, ParameterDirection.Input);

            dbManager.AddParameters(4, "@DTAKTIVIZIMI", dtaktivizimi, ParameterDirection.Input);
            dbManager.AddParameters(5, "@IDPERDORUESI", idperdoruesi, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_QENDRAKOSTOPUNONJES_upd");
            return new clsMesazh(true, mesazhModifikimi);

        }


        /// <summary>
        /// merr punesimin me id
        /// </summary>
        /// <param name="idPunesim">  id e punesimit</param>
        /// <returns> kthen datarow qe permban punesimin me kete id</returns>
        internal DataRow ktheQendraKostoPunonjes(int id)
        {

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@ID", id, ParameterDirection.Input);
            using (DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_QENDRAKOSTOPUNONJES_sel"))
            {
                if (ds == null)
                    return null;
                if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                    return null;
                return ds.Tables[0].Rows[0];
            }

        }

        /// <summary>
        /// merr gjithe punesimet e punonjesit
        /// </summary>
        /// <param name="idpunonjes"> id e punonjesit</param>
        /// <returns> nje datatable qe permban nje koleksion me te punesimet e punonjesit</returns>
        internal DataTable ktheQendraKostoPunonjesSipasIdPunonjesi(int idpunonjes)
        {

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDPUNONJES", idpunonjes, ParameterDirection.Input);
            using (DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_QENDRAKOSTOPUNONJES_merrQendraSipasPunonjesit"))
            {

                return ds.Tables[0];
            }

        }

        internal DataTable ktheQendraKostoNdermarrjesAndAutorizimeDTExport(int idnderm)
        {
            string salt = WebConfigurationManager.AppSettings["salt"];
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            dbManager.AddParameters(1, "@Salt", salt, ParameterDirection.Input);

            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_QENDRAKOSTOPUNONJES_merrQendraSipasNdermarjesExportDt");

            return ds.Tables[0];

        }
        internal bool ekzistonQKPerKetePunonjeMeKeteDateAktivizimi(string nrpersonal, DateTime dtaktivizimi, out int idpunesimi)
        {
            idpunesimi = 0;

            string salt = WebConfigurationManager.AppSettings["salt"];
            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@Nrpersonal", nrpersonal, ParameterDirection.Input);
            dbManager.AddParameters(1, "@salt", salt, ParameterDirection.Input);
            dbManager.AddParameters(2, "@dtaktivizimi", dtaktivizimi, ParameterDirection.Input);
            using (DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_t_qendrakostopunonjes_EkzistonSipasDtAktivizimiDhePunonjes."))
            {

                if (ds.Tables[0].Rows.Count == 1)
                {
                    idpunesimi = int.Parse(ds.Tables[0].Rows[0]["Id"].ToString());
                    return true;
                }
                if (ds.Tables[0].Rows.Count == 0)
                    return false;
                return true;
            }

        }

        #endregion
        /// <summary>
        /// Metodat e meposhtme i perkasin veprimeve qe kryhen nga objektet clsBandaPunonjes dhe colBandaPunonjes
        /// </summary>
        #region BANDA PUNONJES

        /// <summary>
        /// ekzekuton 	[prc_T_PUNESIM_ins] ne DB dhe kthen statusin e perfundimit te ekzekutimit te SP-se perkatese
        /// Shiko clsMesazh brenda ketij moduli per informacion te metejshem mbi vleren e kthimit te kesaj metode
        /// </summary>
        /// <param name="id"> id e punesimit</param>
        /// <param name="idpunesim"> id e punonjesit</param>
        /// <param name="idqk1"> id e departamentit</param>
        /// <param name="idqk2"> id e nendepartamentit</param>
        /// <param name="detyra">detyra </param>
        /// <param name="nrkontrate">nr i kontrates</param>
        /// <param name="idgrupiglobal"> tipi i kontrates</param>
        /// <param name="dtfillimi"> data e fillimit</param>
        /// <param name="dtperfundimi">data e perfundimit</param>
        /// <param name="llogbankare">llogaria bankare</param>
        /// <param name="idbanka"> idbanka</param>
        /// <param name="larguar">larguar</param>
        /// <param name="dtlargimi"> dt e largimit</param>
        /// <param name="aryeja">arsyesja</param>
        /// <param name="periudhanjoftimi"> periudha e njoftimit</param>
        /// <param name="neprove">ne prove</param>
        /// <param name="periudhaprove"> periudhe ne prove</param>
        ///  /// <param name="idgrupilocal"> grupi i punesimit</param>
        /// <returns>nje objekt clsMesazh qe tregon nese ruajtja eshte kryer ne rregull apo jo.</returns>
        internal clsMesazh ruajBandaPunonjes(out int id, int idpunonjes, int idgrupiglobal, int idgrupilocal, DateTime dtaktivizimi, int idperdoruesi)
        {
            id = -1;

            dbManager.Open();
            dbManager.CreateParameters(6);
            dbManager.AddParameters(0, "@ID", id, ParameterDirection.Output);
            dbManager.AddParameters(1, "@IDPUNONJES", idpunonjes, ParameterDirection.Input);

            if (idgrupiglobal == 0) dbManager.AddParameters(2, "@IDGRUPIMGLOBAL", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(2, "@IDGRUPIMGLOBAL", idgrupiglobal, ParameterDirection.Input);
            if (idgrupilocal == 0) dbManager.AddParameters(3, "@IDGRUPIMLOKAL", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(3, "@IDGRUPIMLOKAL", idgrupilocal, ParameterDirection.Input);

            dbManager.AddParameters(4, "@DTAKTIVIZIMI", dtaktivizimi, ParameterDirection.Input);
            dbManager.AddParameters(5, "@IDPERDORUESI", idperdoruesi, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_BANDAPUNONJES_ins");
            id = int.Parse(dbManager.Parameters[0].Value.ToString());
            return new clsMesazh(true, mesazhRuajtje);

        }

        /// <summary>
        /// ekzekuton prc_T_PUNESIM_upd ne DB dhe kthen statusin e perfundimit te ekzekutimit te SP-se perkatese
        /// Shiko clsMesazh brenda ketij moduli per informacion te metejshem mbi vleren e kthimit te kesaj metode
        /// <param name="idpunesim"> id e punesimit</param>
        /// <param name="idPunonjes"> id e punonjesit</param>
        /// <param name="iddepartament"> id e departamentit</param>
        /// <param name="idnendepartamet"> id e nendepartamentit</param>
        /// <param name="detyra">detyra </param>
        /// <param name="nrkontrate">nr i kontrates</param>
        /// <param name="idtipkontrate"> tipi i kontrates</param>
        /// <param name="dtfillimi"> data e fillimit</param>
        /// <param name="dtperfundimi">data e perfundimit</param>
        /// <param name="llogbankare">llogaria bankare</param>
        /// <param name="idbanka"> idbanka</param>
        /// <param name="larguar">larguar</param>
        /// <param name="dtlargimi"> dt e largimit</param>
        /// <param name="aryeja">arsyesja</param>
        /// <param name="periudhanjoftimi"> periudha e njoftimit</param>
        /// <param name="neprove">ne prove</param>
        /// <param name="periudhaprove"> periudhe ne prove</param>
        /// <returns> nje objekt clsMesazh qe tregon nese modifikimi eshte kryer ne rregull apo jo.</returns>
        /// </summary>
        internal clsMesazh modifikoBandaPunonjes(int id, int idpunonjes, int idgrupiglobal, int idgrupilocal, DateTime dtaktivizimi, int idperdoruesi)
        {

            dbManager.Open();
            dbManager.CreateParameters(6);
            dbManager.AddParameters(0, "@ID", id, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPUNONJES", idpunonjes, ParameterDirection.Input);

            if (idgrupiglobal == 0) dbManager.AddParameters(2, "@IDGRUPIMGLOBAL", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(2, "@IDGRUPIMGLOBAL", idgrupiglobal, ParameterDirection.Input);
            if (idgrupilocal == 0) dbManager.AddParameters(3, "@IDGRUPIMLOKAL", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(3, "@IDGRUPIMLOKAL", idgrupilocal, ParameterDirection.Input);

            dbManager.AddParameters(4, "@DTAKTIVIZIMI", dtaktivizimi, ParameterDirection.Input);
            dbManager.AddParameters(5, "@IDPERDORUESI", idperdoruesi, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_BANDAPUNONJES_upd");
            return new clsMesazh(true, mesazhModifikimi);

        }


        /// <summary>
        /// merr punesimin me id
        /// </summary>
        /// <param name="idPunesim">  id e punesimit</param>
        /// <returns> kthen datarow qe permban punesimin me kete id</returns>
        internal DataRow ktheBandaPunonjes(int id)
        {

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@ID", id, ParameterDirection.Input);
            using (DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_BANDAPUNONJES_sel"))
            {
                if (ds == null)
                    return null;
                if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                    return null;
                return ds.Tables[0].Rows[0];
            }

        }

        /// <summary>
        /// merr gjithe punesimet e punonjesit
        /// </summary>
        /// <param name="idpunonjes"> id e punonjesit</param>
        /// <returns> nje datatable qe permban nje koleksion me te punesimet e punonjesit</returns>
        internal DataTable ktheBandaPunonjesiSipasIdPunonjesi(int idpunonjes)
        {

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDPUNONJES", idpunonjes, ParameterDirection.Input);
            using (DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_BANDAPUNONJES_merrBandaSipasPunonjesit"))
            {

                return ds.Tables[0];
            }

        }

        internal DataTable ktheBandaNdermarrjesAndAutorizimeDTExport(int idnderm)
        {
            string salt = WebConfigurationManager.AppSettings["salt"];
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            dbManager.AddParameters(1, "@Salt", salt, ParameterDirection.Input);

            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_BANDAPUNONJES_merrBandaSipasNdermarjesExportDt");

            return ds.Tables[0];

        }
        internal bool ekzistonBandaPerKetePunonjeMeKeteDateAktivizimi(string nrpersonal, DateTime dtaktivizimi, out int idpunesimi)
        {
            idpunesimi = 0;

            string salt = WebConfigurationManager.AppSettings["salt"];
            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@Nrpersonal", nrpersonal, ParameterDirection.Input);
            dbManager.AddParameters(1, "@salt", salt, ParameterDirection.Input);
            dbManager.AddParameters(2, "@dtaktivizimi", dtaktivizimi, ParameterDirection.Input);
            using (DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_BANDAPUNONJES_EkzistonSipasDtAktivizimiDhePunonjes"))
            {

                if (ds.Tables[0].Rows.Count == 1)
                {
                    idpunesimi = int.Parse(ds.Tables[0].Rows[0]["Id"].ToString());
                    return true;
                }
                if (ds.Tables[0].Rows.Count == 0)
                    return false;
                return true;
            }

        }

        #endregion

        /// <summary>
        /// Metodat e meposhtme i perkasin veprimeve qe kryhen nga objektet clsBankaPunonjes dhe colBankaPunonjes
        /// </summary>
        #region BANKA PUNONJES

        /// <summary>
        /// ekzekuton 	[prc_T_PUNESIM_ins] ne DB dhe kthen statusin e perfundimit te ekzekutimit te SP-se perkatese
        /// Shiko clsMesazh brenda ketij moduli per informacion te metejshem mbi vleren e kthimit te kesaj metode
        /// </summary>
        /// <param name="id"> id e punesimit</param>
        /// <param name="idpunonjes"> id e punonjesit</param>
        /// <param name="llogaribankare"> id e departamentit</param>
        /// <param name="idbanka"> id e nendepartamentit</param>
        /// <param name="detyra">detyra </param>
        /// <param name="nrkontrate">nr i kontrates</param>
        /// <param name="limittel"> tipi i kontrates</param>
        /// <param name="dtfillimi"> data e fillimit</param>
        /// <param name="dtperfundimi">data e perfundimit</param>
        /// <param name="llogbankare">llogaria bankare</param>
        /// <param name="idbanka"> idbanka</param>
        /// <param name="larguar">larguar</param>
        /// <param name="dtlargimi"> dt e largimit</param>
        /// <param name="aryeja">arsyesja</param>
        /// <param name="periudhanjoftimi"> periudha e njoftimit</param>
        /// <param name="neprove">ne prove</param>
        /// <param name="periudhaprove"> periudhe ne prove</param>
        ///  /// <param name="limitinterneti"> grupi i punesimit</param>
        /// <returns>nje objekt clsMesazh qe tregon nese ruajtja eshte kryer ne rregull apo jo.</returns>
        internal clsMesazh ruajBankaPunonjes(out int id, int idpunonjes, string llogaribankare, int idbanka, decimal limittel, decimal limitinterneti, DateTime dtaktivizimi, int idperdoruesi)
        {
            id = -1;

            string salt = WebConfigurationManager.AppSettings["salt"];
            dbManager.Open();
            dbManager.CreateParameters(9);
            dbManager.AddParameters(0, "@ID", id, ParameterDirection.Output);
            dbManager.AddParameters(1, "@IDPUNONJES", idpunonjes, ParameterDirection.Input);
            dbManager.AddParameters(2, "@LLOGBANKARE", llogaribankare, ParameterDirection.Input);
            if (idbanka == 0) dbManager.AddParameters(3, "@IDBANKA", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(3, "@IDBANKA", idbanka, ParameterDirection.Input);
            dbManager.AddParameters(4, "@LIMITTEL", limittel, ParameterDirection.Input);
            dbManager.AddParameters(5, "@LIMITINTERNET", limitinterneti, ParameterDirection.Input);

            dbManager.AddParameters(6, "@DTAKTIVIZIMI", dtaktivizimi, ParameterDirection.Input);
            dbManager.AddParameters(7, "@IDPERDORUESI", idperdoruesi, ParameterDirection.Input);
            dbManager.AddParameters(8, "@salt", salt, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_BANKAPUNONJES_ins");
            id = int.Parse(dbManager.Parameters[0].Value.ToString());
            return new clsMesazh(true, mesazhRuajtje);

        }

        /// <summary>
        /// ekzekuton prc_T_PUNESIM_upd ne DB dhe kthen statusin e perfundimit te ekzekutimit te SP-se perkatese
        /// Shiko clsMesazh brenda ketij moduli per informacion te metejshem mbi vleren e kthimit te kesaj metode
        /// <param name="idpunesim"> id e punesimit</param>
        /// <param name="idPunonjes"> id e punonjesit</param>
        /// <param name="iddepartament"> id e departamentit</param>
        /// <param name="idnendepartamet"> id e nendepartamentit</param>
        /// <param name="detyra">detyra </param>
        /// <param name="nrkontrate">nr i kontrates</param>
        /// <param name="idtipkontrate"> tipi i kontrates</param>
        /// <param name="dtfillimi"> data e fillimit</param>
        /// <param name="dtperfundimi">data e perfundimit</param>
        /// <param name="llogbankare">llogaria bankare</param>
        /// <param name="idbanka"> idbanka</param>
        /// <param name="larguar">larguar</param>
        /// <param name="dtlargimi"> dt e largimit</param>
        /// <param name="aryeja">arsyesja</param>
        /// <param name="periudhanjoftimi"> periudha e njoftimit</param>
        /// <param name="neprove">ne prove</param>
        /// <param name="periudhaprove"> periudhe ne prove</param>
        /// <returns> nje objekt clsMesazh qe tregon nese modifikimi eshte kryer ne rregull apo jo.</returns>
        /// </summary>
        internal clsMesazh modifikoBankaPunonjes(int id, int idpunonjes, string llogaribankare, int idbanka, decimal limittel, decimal limitinterneti, DateTime dtaktivizimi, int idperdoruesi)
        {

            string salt = WebConfigurationManager.AppSettings["salt"];
            dbManager.Open();
            dbManager.CreateParameters(9);
            dbManager.AddParameters(0, "@ID", id, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPUNONJES", idpunonjes, ParameterDirection.Input);
            dbManager.AddParameters(2, "@LLOGBANKARE", llogaribankare, ParameterDirection.Input);
            if (idbanka == 0) dbManager.AddParameters(3, "@IDBANKA", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(3, "@IDBANKA", idbanka, ParameterDirection.Input);
            dbManager.AddParameters(4, "@LIMITTEL", limittel, ParameterDirection.Input);
            dbManager.AddParameters(5, "@LIMITINTERNET", limitinterneti, ParameterDirection.Input);

            dbManager.AddParameters(6, "@DTAKTIVIZIMI", dtaktivizimi, ParameterDirection.Input);
            dbManager.AddParameters(7, "@IDPERDORUESI", idperdoruesi, ParameterDirection.Input);
            dbManager.AddParameters(8, "@salt", salt, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_BANKAPUNONJES_upd");
            return new clsMesazh(true, mesazhModifikimi);

        }


        /// <summary>
        /// merr punesimin me id
        /// </summary>
        /// <param name="idPunesim">  id e punesimit</param>
        /// <returns> kthen datarow qe permban punesimin me kete id</returns>
        internal DataRow ktheBankaPunonjes(int id)
        {

            string salt = WebConfigurationManager.AppSettings["salt"];
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@ID", id, ParameterDirection.Input);
            dbManager.AddParameters(1, "@salt", salt, ParameterDirection.Input);
            using (DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_BANKAPUNONJES_sel"))
            {
                if (ds == null)
                    return null;
                if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                    return null;
                return ds.Tables[0].Rows[0];
            }

        }

        /// <summary>
        /// merr gjithe punesimet e punonjesit
        /// </summary>
        /// <param name="idpunonjes"> id e punonjesit</param>
        /// <returns> nje datatable qe permban nje koleksion me te punesimet e punonjesit</returns>
        internal DataTable ktheBankaPunonjesSipasIdPuneonjesi(int idpunonjes)
        {

            string salt = WebConfigurationManager.AppSettings["salt"];
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDPUNONJES", idpunonjes, ParameterDirection.Input);
            dbManager.AddParameters(1, "@salt", salt, ParameterDirection.Input);
            using (DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_BANKAPUNONJES_merrQendraSipasPunonjesit"))
            {

                return ds.Tables[0];
            }

        }
        internal DataRow ktheBankaPunonjesSipasIdPuneonjesiDheData(int idpunonjes, DateTime data)
        {

            string salt = WebConfigurationManager.AppSettings["salt"];
            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@IDPUNONJES", idpunonjes, ParameterDirection.Input);
            dbManager.AddParameters(1, "@data", data, ParameterDirection.Input);
            dbManager.AddParameters(2, "@salt", salt, ParameterDirection.Input);
            using (DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_BANKAPUNONJES_merrBankaSipasPunonjesitdheData"))
            {

                return ds.Tables[0].Rows[0];
            }

        }

        #endregion


        /// <summary>
        /// Metodat e meposhtme i perkasin veprimeve qe kryhen nga objektet clsPunesim dhe colPunesim
        /// </summary>
        #region PUNESIM

        /// <summary>
        /// ekzekuton 	[prc_T_PUNESIM_ins] ne DB dhe kthen statusin e perfundimit te ekzekutimit te SP-se perkatese
        /// Shiko clsMesazh brenda ketij moduli per informacion te metejshem mbi vleren e kthimit te kesaj metode
        /// </summary>
        /// <param name="idpunesim"> id e punesimit</param>
        /// <param name="idPunonjes"> id e punonjesit</param>
        /// <param name="iddepartament"> id e departamentit</param>
        /// <param name="idnendepartamet"> id e nendepartamentit</param>
        /// <param name="detyra">detyra </param>
        /// <param name="nrkontrate">nr i kontrates</param>
        /// <param name="idtipkontrate"> tipi i kontrates</param>
        /// <param name="dtfillimi"> data e fillimit</param>
        /// <param name="dtperfundimi">data e perfundimit</param>
        /// <param name="llogbankare">llogaria bankare</param>
        /// <param name="idbanka"> idbanka</param>
        /// <param name="larguar">larguar</param>
        /// <param name="dtlargimi"> dt e largimit</param>
        /// <param name="aryeja">arsyesja</param>
        /// <param name="periudhanjoftimi"> periudha e njoftimit</param>
        /// <param name="neprove">ne prove</param>
        /// <param name="periudhaprove"> periudhe ne prove</param>
        ///  /// <param name="idGrupPunonjesish"> grupi i punesimit</param>
        /// <returns>nje objekt clsMesazh qe tregon nese ruajtja eshte kryer ne rregull apo jo.</returns>
        internal clsMesazh ruajPunesim(out int idpunesim, int idPunonjes, int iddepartament, int idnendepartamet, string detyra, string nrkontrate, int idtipkontrate, DateTime dtfillimi, DateTime dtperfundimi, bool larguar, DateTime? dtlargimi, string aryeja, string periudhanjoftimi, bool neprove, string periudhaprove, int idGrupPunonjesish, int idprofesioni, int idtitullpune, bool punonjesturne, bool punonjesgatishmeri, int statusi, int ndryshimpozicioni, DateTime dtnenshkrimi, string shenime, DateTime dtaktivizimi, int idperdoruesi, bool meKomisione, int idkodeprofesione)
        {
            idpunesim = -1;

            dbManager.Open();
            dbManager.CreateParameters(28);
            dbManager.AddParameters(0, "@IDPUNESIM", idpunesim, ParameterDirection.Output);
            dbManager.AddParameters(1, "@IDPUNONJES", idPunonjes, ParameterDirection.Input);
            if (iddepartament == 0)
                dbManager.AddParameters(2, "@IDDEPARTAMENT", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(2, "@IDDEPARTAMENT", iddepartament, ParameterDirection.Input);
            if (idnendepartamet == 0) dbManager.AddParameters(3, "@IDNENDEPARTAMENT", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(3, "@IDNENDEPARTAMENT", idnendepartamet, ParameterDirection.Input);
            dbManager.AddParameters(4, "@DETYRA", detyra, ParameterDirection.Input);
            dbManager.AddParameters(5, "@NRKONTRATE", nrkontrate, ParameterDirection.Input);
            if (idtipkontrate == 0) dbManager.AddParameters(6, "@IDTIPKONTRATE", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(6, "@IDTIPKONTRATE", idtipkontrate, ParameterDirection.Input);
            dbManager.AddParameters(7, "@DTFILLIMI", dtfillimi, ParameterDirection.Input);
            if (dtperfundimi.Date.ToShortDateString() == "01/01/0001")
                dbManager.AddParameters(8, "@DTPERFUNDIMI", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(8, "@DTPERFUNDIMI", dtperfundimi, ParameterDirection.Input);
            dbManager.AddParameters(9, "@LARGUAR", larguar, ParameterDirection.Input);
            if (!dtlargimi.HasValue)
                dbManager.AddParameters(10, "@DTLARGIMI", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(10, "@DTLARGIMI", dtlargimi.Value, ParameterDirection.Input);
            dbManager.AddParameters(11, "@ARSYEJA", aryeja, ParameterDirection.Input);
            dbManager.AddParameters(12, "@PERIUDHANJOFTIMI", periudhanjoftimi, ParameterDirection.Input);
            dbManager.AddParameters(13, "@NEPROVE", neprove, ParameterDirection.Input);
            dbManager.AddParameters(14, "@PERIUDHAPROVE", periudhaprove, ParameterDirection.Input);
            if (idGrupPunonjesish == 0) dbManager.AddParameters(15, "@IDGRUPPUNONJESISH", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(15, "@IDGRUPPUNONJESISH", idGrupPunonjesish, ParameterDirection.Input);
            if (idprofesioni == 0) dbManager.AddParameters(16, "@IDPROFESIONI", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(16, "@IDPROFESIONI", idprofesioni, ParameterDirection.Input);
            if (idtitullpune == 0) dbManager.AddParameters(17, "@IDTITULLPUNE", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(17, "@IDTITULLPUNE", idtitullpune, ParameterDirection.Input);
            dbManager.AddParameters(18, "@PUNONJESTURNE", punonjesturne, ParameterDirection.Input);
            dbManager.AddParameters(19, "@PUNONJESGATISHMERI", punonjesgatishmeri, ParameterDirection.Input);
            dbManager.AddParameters(20, "@STATUSI", statusi, ParameterDirection.Input);
            dbManager.AddParameters(21, "@NDRYSHIMPOZICIONI", ndryshimpozicioni, ParameterDirection.Input);
            if (dtnenshkrimi.Date.ToShortDateString() == "01/01/0001")
                dbManager.AddParameters(22, "@DTNENSHKRIMI", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(22, "@DTNENSHKRIMI", dtnenshkrimi, ParameterDirection.Input);
            dbManager.AddParameters(23, "@SHENIME", shenime, ParameterDirection.Input);
            dbManager.AddParameters(24, "@DTAKTIVIZIMI", dtaktivizimi, ParameterDirection.Input);
            dbManager.AddParameters(25, "@IDPERDORUESI", idperdoruesi, ParameterDirection.Input);
            dbManager.AddParameters(26, "@MEKOMISIONE", meKomisione, ParameterDirection.Input);
            if (idkodeprofesione <= 0)
                dbManager.AddParameters(27, "@IDKODEPROFESIONE", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(27, "@IDKODEPROFESIONE", idkodeprofesione, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_PUNESIM_ins");
            idpunesim = int.Parse(dbManager.Parameters[0].Value.ToString());
            return new clsMesazh(true, mesazhRuajtje);

        }

        /// <summary>
        /// ekzekuton prc_T_PUNESIM_upd ne DB dhe kthen statusin e perfundimit te ekzekutimit te SP-se perkatese
        /// Shiko clsMesazh brenda ketij moduli per informacion te metejshem mbi vleren e kthimit te kesaj metode
        /// <param name="idpunesim"> id e punesimit</param>
        /// <param name="idPunonjes"> id e punonjesit</param>
        /// <param name="iddepartament"> id e departamentit</param>
        /// <param name="idnendepartamet"> id e nendepartamentit</param>
        /// <param name="detyra">detyra </param>
        /// <param name="nrkontrate">nr i kontrates</param>
        /// <param name="idtipkontrate"> tipi i kontrates</param>
        /// <param name="dtfillimi"> data e fillimit</param>
        /// <param name="dtperfundimi">data e perfundimit</param>
        /// <param name="llogbankare">llogaria bankare</param>
        /// <param name="idbanka"> idbanka</param>
        /// <param name="larguar">larguar</param>
        /// <param name="dtlargimi"> dt e largimit</param>
        /// <param name="aryeja">arsyesja</param>
        /// <param name="periudhanjoftimi"> periudha e njoftimit</param>
        /// <param name="neprove">ne prove</param>
        /// <param name="periudhaprove"> periudhe ne prove</param>
        /// <returns> nje objekt clsMesazh qe tregon nese modifikimi eshte kryer ne rregull apo jo.</returns>
        /// </summary>
        internal clsMesazh modifikoPunesim(int idpunesim, int idPunonjes, int iddepartament, int idnendepartamet, string detyra, string nrkontrate, int idtipkontrate, DateTime dtfillimi, DateTime dtperfundimi, bool larguar, DateTime? dtlargimi, string aryeja, string periudhanjoftimi, bool neprove, string periudhaprove, int idgruppunonjesish, int idprofesioni, int idtitullpune, bool punonjesturne, bool punonjesgatishmeri, int statusi, int ndryshimpozicioni, DateTime dtnenshkrimi, string shenime, DateTime dtaktivizimi, int idperdoruesi, bool meKomisione, int idkodeprofesione)
        {

            dbManager.Open();
            dbManager.CreateParameters(28);
            dbManager.AddParameters(0, "@IDPUNESIM", idpunesim, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPUNONJES", idPunonjes, ParameterDirection.Input);
            if (iddepartament == 0)
                dbManager.AddParameters(2, "@IDDEPARTAMENT", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(2, "@IDDEPARTAMENT", iddepartament, ParameterDirection.Input);
            if (idnendepartamet == 0) dbManager.AddParameters(3, "@IDNENDEPARTAMENT", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(3, "@IDNENDEPARTAMENT", idnendepartamet, ParameterDirection.Input);
            dbManager.AddParameters(4, "@DETYRA", detyra, ParameterDirection.Input);
            dbManager.AddParameters(5, "@NRKONTRATE", nrkontrate, ParameterDirection.Input);
            if (idtipkontrate == 0) dbManager.AddParameters(6, "@IDTIPKONTRATE", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(6, "@IDTIPKONTRATE", idtipkontrate, ParameterDirection.Input);
            dbManager.AddParameters(7, "@DTFILLIMI", dtfillimi, ParameterDirection.Input);
            if (dtperfundimi.Date.ToShortDateString() == "01/01/0001")
                dbManager.AddParameters(8, "@DTPERFUNDIMI", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(8, "@DTPERFUNDIMI", dtperfundimi, ParameterDirection.Input);

            dbManager.AddParameters(9, "@LARGUAR", larguar, ParameterDirection.Input);
            if (!dtlargimi.HasValue || DateTimeUtil.EshteNullOrDefault(dtlargimi.Value))
                dbManager.AddParameters(10, "@DTLARGIMI", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(10, "@DTLARGIMI", dtlargimi, ParameterDirection.Input);
            dbManager.AddParameters(11, "@ARSYEJA", aryeja, ParameterDirection.Input);
            dbManager.AddParameters(12, "@PERIUDHANJOFTIMI", periudhanjoftimi, ParameterDirection.Input);
            dbManager.AddParameters(13, "@NEPROVE", neprove, ParameterDirection.Input);
            dbManager.AddParameters(14, "@PERIUDHAPROVE", periudhaprove, ParameterDirection.Input);
            if (idgruppunonjesish == 0) dbManager.AddParameters(15, "@IDGRUPPUNONJESISH", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(15, "@IDGRUPPUNONJESISH", idgruppunonjesish, ParameterDirection.Input);
            if (idprofesioni <= 0) dbManager.AddParameters(16, "@IDPROFESIONI", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(16, "@IDPROFESIONI", idprofesioni, ParameterDirection.Input);
            if (idtitullpune <= 0) dbManager.AddParameters(17, "@IDTITULLPUNE", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(17, "@IDTITULLPUNE", idtitullpune, ParameterDirection.Input);
            dbManager.AddParameters(18, "@PUNONJESTURNE", punonjesturne, ParameterDirection.Input);
            dbManager.AddParameters(19, "@PUNONJESGATISHMERI", punonjesgatishmeri, ParameterDirection.Input);
            dbManager.AddParameters(20, "@STATUSI", statusi, ParameterDirection.Input);
            dbManager.AddParameters(21, "@NDRYSHIMPOZICIONI", ndryshimpozicioni, ParameterDirection.Input);
            if (dtnenshkrimi.Date.ToShortDateString() == "01/01/0001")
                dbManager.AddParameters(22, "@DTNENSHKRIMI", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(22, "@DTNENSHKRIMI", dtnenshkrimi, ParameterDirection.Input);
            dbManager.AddParameters(23, "@SHENIME", shenime, ParameterDirection.Input);
            dbManager.AddParameters(24, "@DTAKTIVIZIMI", dtaktivizimi, ParameterDirection.Input);
            dbManager.AddParameters(25, "@IDPERDORUESI", idperdoruesi, ParameterDirection.Input);
            dbManager.AddParameters(26, "@MEKOMISIONE", meKomisione, ParameterDirection.Input);
            if (idkodeprofesione <= 0)
                dbManager.AddParameters(27, "@IDKODEPROFESIONE", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(27, "@IDKODEPROFESIONE", idkodeprofesione, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_PUNESIM_upd");
            return new clsMesazh(true, mesazhModifikimi);

        }
        internal void kthePunesimSipasIdPuneonjesiTeFundit(int idpunonjes, DateTime data, clsPunesim punesim)
        {

            string salt = WebConfigurationManager.AppSettings["salt"];
            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@IDPUNONJES", idpunonjes, ParameterDirection.Input);
            dbManager.AddParameters(1, "@salt", salt, ParameterDirection.Input);
            dbManager.AddParameters(2, "@data", data, ParameterDirection.Input);
            dbManager.FillObject<clsPunesim>("prc_T_PUNESIM_merrPunesimSipasIdPunonjesiTeFundit", punesim.Mbush);

        }
        internal void kthePunesimSipasIdPuneonjesiTeParaFundit(int idpunonjes, DateTime data, clsPunesim punesim)
        {

            string salt = WebConfigurationManager.AppSettings["salt"];
            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@IDPUNONJES", idpunonjes, ParameterDirection.Input);
            dbManager.AddParameters(1, "@salt", salt, ParameterDirection.Input);
            dbManager.AddParameters(2, "@data", data, ParameterDirection.Input);
            dbManager.FillObject<clsPunesim>("prc_T_PUNESIM_merrPunesimSipasIdPunonjesiTeParaFundit", punesim.Mbush);

        }
        /// <summary>
        /// ekzekutohet sp-ja prc_T_PUNESIM_del duke i kaluar id e punesimi
        /// </summary>
        /// <param name="idpunesim">id e punesimit qe do fshihet </param>
        /// <returns>nje objekt clsMesazh qe tregon nese fshirja eshte kryer ne rregull apo jo</returns>
        internal clsMesazh fshiPunesim(int idpunesim)
        {

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDPUNESIM", idpunesim, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_PUNESIM_del");
            clsMesazh mesazh = new clsMesazh(true, mesazhFshirje);
            return mesazh;

        }

        /// <summary>
        /// merr punesimin me id
        /// </summary>
        /// <param name="idPunesim">  id e punesimit</param>
        /// <returns> kthen datarow qe permban punesimin me kete id</returns>
        internal void mbushPunesim(int idPunesim, clsPunesim punesim)
        {

            string salt = WebConfigurationManager.AppSettings["salt"];
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDPUNESIM", idPunesim, ParameterDirection.Input);
            dbManager.AddParameters(1, "@salt", salt, ParameterDirection.Input);
            dbManager.FillObject<clsPunesim>("prc_T_PUNESIM_sel", punesim.Mbush);


        }
        internal bool ekzistonPunesimPerKetePunonjeMeKeteDateAktivizimi(string nrpersonal, DateTime dtaktivizimi, out int idpunesimi)
        {
            idpunesimi = 0;

            string salt = WebConfigurationManager.AppSettings["salt"];
            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@Nrpersonal", nrpersonal, ParameterDirection.Input);
            dbManager.AddParameters(1, "@salt", salt, ParameterDirection.Input);
            dbManager.AddParameters(2, "@dtaktivizimi", dtaktivizimi, ParameterDirection.Input);
            using (DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_PUNESIM_EkzistonSipasDtAktivizimiDhePunonjes"))
            {

                if (ds.Tables[0].Rows.Count > 0)
                {
                    idpunesimi = int.Parse(ds.Tables[0].Rows[0]["IDPUNESIM"].ToString());
                    return true;
                }
                if (ds.Tables[0].Rows.Count == 0)
                    return false;
                return true;
            }

        }


        /// <summary>
        /// merr gjithe punesimet e punonjesit
        /// </summary>
        /// <param name="idpunonjes"> id e punonjesit</param>
        /// <returns> nje datatable qe permban nje koleksion me te punesimet e punonjesit</returns>
        internal IEnumerable<clsPunesim> kthePunesimSipasIdPuneonjesi(int idpunonjes)
        {

            string salt = WebConfigurationManager.AppSettings["salt"];
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDPUNONJES", idpunonjes, ParameterDirection.Input);
            dbManager.AddParameters(1, "@salt", salt, ParameterDirection.Input);
            return dbManager.GetIEnumerbale("prc_T_PUNESIM_merrPunesimSipasIdPunonjesi", clsPunesim.Krijo);


        }

        internal IEnumerable<clsPunesim> kthePunesimSipasIdPuneonjesiDt(int idpunonjes, int idgjuha)
        {

            string salt = WebConfigurationManager.AppSettings["salt"];
            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@IDPUNONJES", idpunonjes, ParameterDirection.Input);
            dbManager.AddParameters(1, "@salt", salt, ParameterDirection.Input);
            dbManager.AddParameters(2, "@idgjuha", idgjuha, ParameterDirection.Input);
            return dbManager.GetIEnumerbale("prc_T_PUNESIM_merrPunesimSipasIdPunonjesiDt", clsPunesim.KrijoPerGride);


        }

        internal clsMesazh ruajArsyeLargimi(out int id, string pershkrimi)
        {
            id = -1;

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@ID", id, ParameterDirection.Output);

            dbManager.AddParameters(1, "@PERSHKRIMI", pershkrimi, ParameterDirection.Input);

            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_ARSYEJALARGIMIT_ins");
            //id = int.Parse(dbManager.Parameters[0].Value.ToString());
            return new clsMesazh(true, mesazhRuajtje);

        }
        internal DataTable ktheArsye()
        {

            dbManager.Open();
            using (DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_ARSYEJALARGIMIT_sel"))
            {

                return ds.Tables[0];
            }

        }

        internal DataTable kthePunesimNdermarrjesAndAutorizimeDTExport(int idnderm)
        {
            string salt = WebConfigurationManager.AppSettings["salt"];
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            dbManager.AddParameters(1, "@Salt", salt, ParameterDirection.Input);

            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_PUNESIM_merrPunesimSipasNdermarjesDTExport");

            return ds.Tables[0];

        }
        #endregion

        /// <summary>
        /// Metodat e meposhtme i perkasin veprimeve qe kryhen nga objektet clsPunonjes dhe colPunonjes
        /// </summary>
        #region PUNONJES

        /// <summary>
        /// ekzekuton 	[prc_T_PUNONJES_ins] ne DB dhe kthen statusin e perfundimit te ekzekutimit te SP-se perkatese
        /// Shiko clsMesazh brenda ketij moduli per informacion te metejshem mbi vleren e kthimit te kesaj metode
        /// </summary>
        /// <param name="idpunonjes">id e punonjesit</param>
        /// <param name="nrpersonal">nr personal</param>
        /// <param name="emer">emri</param>
        /// <param name="mbiemer">mbiemri</param>
        /// <param name="atesia">atesia</param>
        /// <param name="datelindja">datelindja</param>
        /// <param name="nrsig">nr i sigurimeve</param>
        /// <param name="idqyteti">id e qyteti</param>
        /// <param name="adresa">adresa</param>
        /// <param name="telefoni">telefoni</param>
        /// <param name="email">emaili</param>
        /// <param name="aktiv">aktiv</param>
        /// <param name="emerkontakti">emerkontakti</param>
        /// <param name="mbiemerkontakti">mbiemerkontakti</param>
        /// <param name="telkontakti"> telefon kontakti</param>
        /// <param name="adresakontakti">adrese kontakti</param>
        /// <param name="emailkontakti">email kontakti</param>
        /// <param name="shenimekontakti">shenime kontakti</param>
        /// <param name="idgrupi">id grupi perdorues</param>
        /// <param name="llojpagese">lloj pagese 1-mujore, 2 ditore 3 orare</param>
        /// <param name="idmonedha">id e monedhes</param>
        /// <param name="idkonfig">id e konfig</param>
        /// <param name="idPerdoruesi">idperdoruesi</param>
        /// <param name="idnderm">idndermarje</param>
        /// <param name="idstatusdok">idstatusdok</param>
        /// <returns>nje objekt clsMesazh qe tregon nese ruajtja eshte kryer ne rregull apo jo.</returns>
        internal clsMesazh ruajPunonjes(out int idpunonjes, string nrpersonal, string emer, string mbiemer, string atesia, DateTime datelindja, string nrsig, int idqyteti, string adresa, string telefoni, string email, bool aktiv, string emerkontakti, string mbiemerkontakti, string telkontakti, string adresakontakti, string emailkontakti, string shenimekontakti, int idgrupi, int llojpagese, int idmonedha, int idkonfig, int idPerdoruesi, int idnderm, int idstatusdok, int idobjektivakosto, bool llogaritNgaListorare, int idkrijuesi, string sapid, string nrpashaporte, bool gjinia, int kombesia, bool kryefamiliar, int edukimi, int punameparshme, int vendndodhje, string nrjupiter, string username, string shenime, int nrRendor, string lejepune, string password, int idllogari)
        {
            idpunonjes = -1;

            string salt = WebConfigurationManager.AppSettings["salt"];
            dbManager.Open();
            dbManager.CreateParameters(44);
            dbManager.AddParameters(0, "@IDPUNONJES", idpunonjes, ParameterDirection.Output);
            dbManager.AddParameters(1, "@NRPERSONAL", nrpersonal, ParameterDirection.Input);
            dbManager.AddParameters(2, "@EMER", emer, ParameterDirection.Input);
            dbManager.AddParameters(3, "@MBIEMER", mbiemer, ParameterDirection.Input);
            dbManager.AddParameters(4, "@ATESIA", atesia, ParameterDirection.Input);
            dbManager.AddParameters(5, "@DATELINDJA", datelindja, ParameterDirection.Input);
            dbManager.AddParameters(6, "@NRSIG", nrsig, ParameterDirection.Input);
            if (idqyteti == 0) dbManager.AddParameters(7, "@IDQYTETI", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(7, "@IDQYTETI", idqyteti, ParameterDirection.Input);
            dbManager.AddParameters(8, "@ADRESA", adresa, ParameterDirection.Input);
            dbManager.AddParameters(9, "@TELEFON", telefoni, ParameterDirection.Input);
            dbManager.AddParameters(10, "@EMAIL", email, ParameterDirection.Input);
            dbManager.AddParameters(11, "@AKTIV", aktiv, ParameterDirection.Input);
            dbManager.AddParameters(12, "@EMERKONTAKTI", emerkontakti, ParameterDirection.Input);
            dbManager.AddParameters(13, "@MBIEMERKONTAKTI", mbiemerkontakti, ParameterDirection.Input);
            dbManager.AddParameters(14, "@TELKONTAKTI", telkontakti, ParameterDirection.Input);
            dbManager.AddParameters(15, "@ADRESAKONTAKTI", adresakontakti, ParameterDirection.Input);
            dbManager.AddParameters(16, "@EMAILKONTAKTI", emailkontakti, ParameterDirection.Input);
            dbManager.AddParameters(17, "@SHENIMEKONTAKTI", shenimekontakti, ParameterDirection.Input);
            if (idgrupi == 0) dbManager.AddParameters(18, "@IDGRUPPUNONJESISH", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(18, "@IDGRUPPUNONJESISH", idgrupi, ParameterDirection.Input);
            dbManager.AddParameters(19, "@LLOJPAGESE", llojpagese, ParameterDirection.Input);
            dbManager.AddParameters(20, "@IDMONEDHA", idmonedha, ParameterDirection.Input);
            dbManager.AddParameters(21, "@IDKONFIG", idkonfig, ParameterDirection.Input);
            dbManager.AddParameters(22, "@IDPERDORUESI", idPerdoruesi, ParameterDirection.Input);
            dbManager.AddParameters(23, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            dbManager.AddParameters(24, "@IDSTATUSDOK", idstatusdok, ParameterDirection.Input);
            if (idobjektivakosto == 0 || idobjektivakosto == -1) dbManager.AddParameters(25, "@IDOBJEKTIVAKOSTO", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(25, "@IDOBJEKTIVAKOSTO", idobjektivakosto, ParameterDirection.Input);
            dbManager.AddParameters(26, "@LlogaritNgaListorare", llogaritNgaListorare, ParameterDirection.Input);
            dbManager.AddParameters(27, "@IDKRIJUESI", idkrijuesi, ParameterDirection.Input);
            dbManager.AddParameters(28, "@SAPID", sapid, ParameterDirection.Input);
            dbManager.AddParameters(29, "@NRPASHAPORTE", nrpashaporte, ParameterDirection.Input);
            dbManager.AddParameters(30, "@GJINIA", gjinia, ParameterDirection.Input);
            if (kombesia == 0 || kombesia == -1) dbManager.AddParameters(31, "@KOMBESIA", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(31, "@KOMBESIA", kombesia, ParameterDirection.Input);
            dbManager.AddParameters(32, "@KRYEFAMILIAR", kryefamiliar, ParameterDirection.Input);
            dbManager.AddParameters(33, "@EDUKIMI", edukimi, ParameterDirection.Input);
            dbManager.AddParameters(34, "@PUNAMEPARSHME", punameparshme, ParameterDirection.Input);
            if (vendndodhje == 0 || vendndodhje == -1) dbManager.AddParameters(35, "@VENDNDODHJA", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(35, "@VENDNDODHJA", vendndodhje, ParameterDirection.Input);
            dbManager.AddParameters(36, "@NRJUPITER", nrjupiter, ParameterDirection.Input);
            dbManager.AddParameters(37, "@USERNAME", username, ParameterDirection.Input);
            dbManager.AddParameters(38, "@SHENIME", shenime, ParameterDirection.Input);
            dbManager.AddParameters(39, "@salt", salt, ParameterDirection.Input);
            dbManager.AddParameters(40, "@NRRENDOR", nrRendor, ParameterDirection.Input);
            dbManager.AddParameters(41, "@LEJEPUNE", lejepune, ParameterDirection.Input);
            dbManager.AddParameters(42, "@PASSWORD", password, ParameterDirection.Input);
            dbManager.AddParameters(43, "@IDLLOGARI", idllogari, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_PUNONJES_ins");
            idpunonjes = int.Parse(dbManager.Parameters[0].Value.ToString());
            return new clsMesazh(true, mesazhRuajtje);

        }

        /// <summary>
        /// ekzekuton prc_T_PUNONJES_upd ne DB dhe kthen statusin e perfundimit te ekzekutimit te SP-se perkatese
        /// Shiko clsMesazh brenda ketij moduli per informacion te metejshem mbi vleren e kthimit te kesaj metode
        /// <param name="idpunonjes">id e punonjesit</param>
        /// <param name="nrpersonal">nr personal</param>
        /// <param name="emer">emri</param>
        /// <param name="mbiemer">mbiemri</param>
        /// <param name="atesia">atesia</param>
        /// <param name="datelindja">datelindja</param>
        /// <param name="nrsig">nr i sigurimeve</param>
        /// <param name="idqyteti">id e qyteti</param>
        /// <param name="adresa">adresa</param>
        /// <param name="telefoni">telefoni</param>
        /// <param name="email">emaili</param>
        /// <param name="aktiv">aktiv</param>
        /// <param name="emerkontakti">emerkontakti</param>
        /// <param name="mbiemerkontakti">mbiemerkontakti</param>
        /// <param name="telkontakti"> telefon kontakti</param>
        /// <param name="adresakontakti">adrese kontakti</param>
        /// <param name="emailkontakti">email kontakti</param>
        /// <param name="shenimekontakti">shenime kontakti</param>
        /// <param name="idgrupi">id grupi perdorues</param>
        /// <param name="llojpagese">lloj pagese 1-mujore, 2 ditore 3 orare</param>
        /// <param name="idmonedha">id e monedhes</param>
        /// <param name="idkonfig">id e konfig</param>
        /// <param name="idPerdoruesi">idperdoruesi</param>
        /// <param name="idnderm">idndermarje</param>
        /// <param name="idstatusdok">idstatusdok</param>
        /// <returns> nje objekt clsMesazh qe tregon nese modifikimi eshte kryer ne rregull apo jo.</returns>
        /// </summary>
        internal clsMesazh modifikoPunonjes(int idpunonjes, string nrpersonal, string emer, string mbiemer, string atesia, DateTime datelindja, string nrsig, int idqyteti, string adresa, string telefoni, string email, bool aktiv, string emerkontakti, string mbiemerkontakti, string telkontakti, string adresakontakti, string emailkontakti, string shenimekontakti, int idgrupi, int llojpagese, int idmonedha, int idkonfig, int idPerdoruesi, int idnderm, int idstatusdok, int idobjektivakosto, bool llogaritNgaListorare, string sapid, string nrpashaporte, bool gjinia, int kombesia, bool kryefamiliar, int edukimi, int punameparshme, int vendndodhje, string nrjupiter, string username, string shenime, int nrRendor, string lejepune, int idllogari)
        {
            string salt = WebConfigurationManager.AppSettings["salt"];
            dbManager.Open();
            dbManager.CreateParameters(42);
            dbManager.AddParameters(0, "@IDPUNONJES", idpunonjes, ParameterDirection.Input);
            dbManager.AddParameters(1, "@NRPERSONAL", nrpersonal, ParameterDirection.Input);
            dbManager.AddParameters(2, "@EMER", emer, ParameterDirection.Input);
            dbManager.AddParameters(3, "@MBIEMER", mbiemer, ParameterDirection.Input);
            dbManager.AddParameters(4, "@ATESIA", atesia, ParameterDirection.Input);
            dbManager.AddParameters(5, "@DATELINDJA", datelindja, ParameterDirection.Input);
            dbManager.AddParameters(6, "@NRSIG", nrsig, ParameterDirection.Input);
            if (idqyteti == 0) dbManager.AddParameters(7, "@IDQYTETI", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(7, "@IDQYTETI", idqyteti, ParameterDirection.Input);
            dbManager.AddParameters(8, "@ADRESA", adresa, ParameterDirection.Input);
            dbManager.AddParameters(9, "@TELEFON", telefoni, ParameterDirection.Input);
            dbManager.AddParameters(10, "@EMAIL", email, ParameterDirection.Input);
            dbManager.AddParameters(11, "@AKTIV", aktiv, ParameterDirection.Input);
            dbManager.AddParameters(12, "@EMERKONTAKTI", emerkontakti, ParameterDirection.Input);
            dbManager.AddParameters(13, "@MBIEMERKONTAKTI", mbiemerkontakti, ParameterDirection.Input);
            dbManager.AddParameters(14, "@TELKONTAKTI", telkontakti, ParameterDirection.Input);
            dbManager.AddParameters(15, "@ADRESAKONTAKTI", adresakontakti, ParameterDirection.Input);
            dbManager.AddParameters(16, "@EMAILKONTAKTI", emailkontakti, ParameterDirection.Input);
            dbManager.AddParameters(17, "@SHENIMEKONTAKTI", shenimekontakti, ParameterDirection.Input);
            if (idgrupi == 0) dbManager.AddParameters(18, "@IDGRUPPUNONJESISH", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(18, "@IDGRUPPUNONJESISH", idgrupi, ParameterDirection.Input);
            dbManager.AddParameters(19, "@LLOJPAGESE", llojpagese, ParameterDirection.Input);
            dbManager.AddParameters(20, "@IDMONEDHA", idmonedha, ParameterDirection.Input);
            dbManager.AddParameters(21, "@IDKONFIG", idkonfig, ParameterDirection.Input);
            dbManager.AddParameters(22, "@IDPERDORUESI", idPerdoruesi, ParameterDirection.Input);
            dbManager.AddParameters(23, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            dbManager.AddParameters(24, "@IDSTATUSDOK", idstatusdok, ParameterDirection.Input);
            if (idobjektivakosto == 0 || idobjektivakosto == -1) dbManager.AddParameters(25, "@IDOBJEKTIVAKOSTO", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(25, "@IDOBJEKTIVAKOSTO", idobjektivakosto, ParameterDirection.Input);
            dbManager.AddParameters(26, "@LlogaritNgaListorare", llogaritNgaListorare, ParameterDirection.Input);
            dbManager.AddParameters(27, "@SAPID", sapid, ParameterDirection.Input);
            dbManager.AddParameters(28, "@NRPASHAPORTE", nrpashaporte, ParameterDirection.Input);
            dbManager.AddParameters(29, "@GJINIA", gjinia, ParameterDirection.Input);
            if (kombesia == 0 || kombesia == -1) dbManager.AddParameters(30, "@KOMBESIA", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(30, "@KOMBESIA", kombesia, ParameterDirection.Input);
            dbManager.AddParameters(31, "@KRYEFAMILIAR", kryefamiliar, ParameterDirection.Input);
            dbManager.AddParameters(32, "@EDUKIMI", edukimi, ParameterDirection.Input);
            dbManager.AddParameters(33, "@PUNAMEPARSHME", punameparshme, ParameterDirection.Input);
            if (vendndodhje == 0 || vendndodhje == -1) dbManager.AddParameters(34, "@VENDNDODHJA", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(34, "@VENDNDODHJA", vendndodhje, ParameterDirection.Input);
            dbManager.AddParameters(35, "@NRJUPITER", nrjupiter, ParameterDirection.Input);
            dbManager.AddParameters(36, "@USERNAME", username, ParameterDirection.Input);
            dbManager.AddParameters(37, "@SHENIME", shenime, ParameterDirection.Input);
            dbManager.AddParameters(38, "@salt", salt, ParameterDirection.Input);
            dbManager.AddParameters(39, "@NRRENDOR", nrRendor, ParameterDirection.Input);
            dbManager.AddParameters(40, "@LEJEPUNE", lejepune, ParameterDirection.Input);
            dbManager.AddParameters(41, "@IDLLOGARI", idllogari, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_PUNONJES_upd");
            return new clsMesazh(true, mesazhModifikimi);

        }

        internal DataTable kthePunonjesNdermarrjesAndAutorizimeDTExport(int idnderm)
        {
            string salt = WebConfigurationManager.AppSettings["salt"];
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            dbManager.AddParameters(1, "@Salt", salt, ParameterDirection.Input);

            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_PUNONJES_merrPunonjesSipasNdermarjesAndAutorizimDTExport");

            return ds.Tables[0];

        }
        internal clsMesazh ruajLogPunonjes(out int id, int idpunonjes, int idperdorues, int llojveprimi, string fushat, string vlerat)
        {
            id = -1;

            string salt = WebConfigurationManager.AppSettings["salt"];
            dbManager.Open();
            dbManager.CreateParameters(7);
            dbManager.AddParameters(0, "@ID", id, ParameterDirection.Output);
            dbManager.AddParameters(1, "@IDPUNONJES", idpunonjes, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDPERDORUESI", idperdorues, ParameterDirection.Input);
            dbManager.AddParameters(3, "@LLOJVEPRIMI", llojveprimi, ParameterDirection.Input);
            dbManager.AddParameters(4, "@FUSHAT", fushat, ParameterDirection.Input);
            dbManager.AddParameters(5, "@vlerat", vlerat, ParameterDirection.Input);
            dbManager.AddParameters(6, "@salt", salt, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_t_LOGUPUNONJES_ins");
            id = int.Parse(dbManager.Parameters[0].Value.ToString());
            return new clsMesazh(true, mesazhRuajtje);

        }


        internal clsMesazh modifikoLogPunonjes(int id, int idpunonjes, int idperdorues, int llojveprimi, string fushat, string vlerat)
        {
            string salt = WebConfigurationManager.AppSettings["salt"];
            dbManager.Open();
            dbManager.CreateParameters(7);
            dbManager.AddParameters(0, "@ID", id, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPUNONJES", idpunonjes, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDPERDORUESI", idperdorues, ParameterDirection.Input);
            dbManager.AddParameters(3, "@LLOJVEPRIMI", llojveprimi, ParameterDirection.Input);
            dbManager.AddParameters(4, "@FUSHAT", fushat, ParameterDirection.Input);
            dbManager.AddParameters(5, "@vlerat", vlerat, ParameterDirection.Input);
            dbManager.AddParameters(6, "@salt", salt, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_PUNONJES_upd");
            return new clsMesazh(true, mesazhModifikimi);

        }

        /// <summary>
        /// ekzekutohet sp-ja prc_T_PUNONJES_del duke i kaluar id e punonjesit
        /// </summary>
        /// <param name="idpunonjes">id e punonjesit qe do fshihet </param>
        /// <returns>nje objekt clsMesazh qe tregon nese fshirja eshte kryer ne rregull apo jo</returns>
        internal clsMesazh fshiPunonjes(int idpunonjes)
        {

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDPUNONJES", idpunonjes, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_PUNONJES_del");
            clsMesazh mesazh = new clsMesazh(true, mesazhFshirje);
            return mesazh;

        }

        /// <summary>
        /// fshin punonjesin duke ndryshuar statusin e saj ne te fshire
        /// </summary>
        /// <param name="idpunonjes">id e komponentes</param>
        /// <param name="idperdoruesi"> id e perdoruesit qe ka kryer veprimin</param>
        /// <returns> clsMesazh qe tregon nese fshirja u krye me sukses apo jo</returns>
        internal clsMesazh fshiPunonjesStatus(int idpunonjes, int idperdoruesi)
        {

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDPUNONJES", idpunonjes, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUESI", idperdoruesi, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_PUNONJES_upddel");
            clsMesazh mesazh = new clsMesazh(true, mesazhFshirje);
            return mesazh;
        }

        /// <summary>
        /// merr punonjesin  sipas id
        /// </summary>
        /// <param name="idpunonjes">  id e punonjes</param>
        /// <returns> kthen datarow qe permban punonjesin me kete id</returns>
        internal void kthePunonjes(int idpunonjes, clsPunonjes punonjes)
        {
            dbManager.Open();
            string salt = WebConfigurationManager.AppSettings["salt"];
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDPUNONJES", idpunonjes, ParameterDirection.Input);
            dbManager.AddParameters(1, "@salt", salt, ParameterDirection.Input);
            dbManager.FillObject<clsPunonjes>("prc_T_PUNONJES_sel", punonjes.mbushPunonjes);
        }
        /// <summary>
        /// merr punonjesin  sipas usename
        /// </summary>
        /// <param name="idpunonjes">  username e punonjes</param>
        /// <returns> kthen datarow qe permban punonjesin me kete username</returns>
        internal void ktheGjithePunonjesitSipasUserName(String username, clsPunonjes punonjes)
        {
            string salt = WebConfigurationManager.AppSettings["salt"];
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@USERNAME", username, ParameterDirection.Input);
            dbManager.AddParameters(1, "@salt", salt, ParameterDirection.Input);
            dbManager.FillObject<clsPunonjes>("prc_T_PUNONJES_merrPunonjesSipasUsername", punonjes.mbushPunonjes);


        }
        /// <summary>
        /// merr gjithe punonjesit e ndermarjes
        /// </summary>
        /// <param name="idnder"> id e ndermarjes</param>
        /// <param name="lloji">lloji</param>
        /// <returns> nje datatable qe permban nje koleksion me te gjithe punonjesit te ndermarjes</returns>
        internal IEnumerable<clsPunonjes> ktheGjithePunonjesitSipasNdermarjes(int idnder)
        {
            string salt = WebConfigurationManager.AppSettings["salt"];
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDNDERMARJE", idnder, ParameterDirection.Input);
            dbManager.AddParameters(1, "@salt", salt, ParameterDirection.Input);
            return dbManager.GetIEnumerbale("prc_T_PUNONJES_merrPunonjesSipasNdermarjes", clsPunonjes.Krijo);

        }

        /// <summary>
        /// merr gjithe punonjesit te nje ndermarje  aktiv
        /// </summary>
        /// <param name="idnder"> id e ndermarjes</param>
        /// <returns> nje datatable qe permban nje koleksion me te gjithe punonjesit te ndermarjes</returns>
        internal IEnumerable<clsPunonjes> ktheGjithePunonjesitSipasNdermarjesAktiv(int idnder)
        {
            string salt = WebConfigurationManager.AppSettings["salt"];
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDNDERMARJE", idnder, ParameterDirection.Input);
            dbManager.AddParameters(1, "@salt", salt, ParameterDirection.Input);
            return dbManager.GetIEnumerbale("prc_T_PUNONJES_merrPunonjesAktivSipasNdermarjes", clsPunonjes.Krijo);

        }

        /// <summary>
        /// merr punonjes te nje ndermarje me kete nr personal
        /// </summary>
        /// <param name="nrpersonal"> nr personal i punonjesit</param>
        /// <param name="idnder"> id e ndermarjes</param>
        /// <returns>  data row me punonjesin e kesaj ndermarje me kete kod</returns>
        internal void kthePunonjesSipasNrPersonal(string nrpersonal, int idnder, clsPunonjes punonjes)
        {
            dbManager.Open();
            dbManager.CreateParameters(3);
            string salt = WebConfigurationManager.AppSettings["salt"];
            dbManager.AddParameters(0, "@NRPERSONAL", nrpersonal, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idnder, ParameterDirection.Input);
            dbManager.AddParameters(2, "@salt", salt, ParameterDirection.Input);
            dbManager.FillObject<clsPunonjes>("prc_T_PUNONJES_merrPunonjesSipasNrPersonal", punonjes.mbushPunonjes);
        }
        internal IEnumerable<clsPunonjes> ktheColPunonjesSipasNrPersonal(string[] nrpersonals, int idnder)
        {
            dbManager.Open();
            dbManager.CreateParameters(3);
            string salt = WebConfigurationManager.AppSettings["salt"];
            dbManager.AddParameters(0, "@NRPERSONALS", string.Join(",", nrpersonals), ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idnder, ParameterDirection.Input);
            dbManager.AddParameters(2, "@salt", salt, ParameterDirection.Input);
            return dbManager.GetIEnumerbale<clsPunonjes>("prc_T_PUNONJES_merrPunonjesSipasNrPersonals", clsPunonjes.KrijoPunonjesSLim);
        }

        /// <summary>
        /// perdoret per te kontrolluar nqs ekziston nje punonjes me kete nrpersonal
        /// behet kontrolli per qellime konsistence te informacionit ne DB, nuk lejon te celen punonjes te ndryshem me te njejtin nr
        /// </summary>
        /// <param name="nrpersonal">nr personal</param>
        /// <param name="idNdermarje"> id e ndermarjes</param>
        /// <returns>nje objeckt boolean qe tregon nese ekziston apo jo nje punonjes me kete nr</returns>
        public bool ekzistonPunonjes(string nrpersonal, int idNdermarje)
        {
            string salt = WebConfigurationManager.AppSettings["salt"];
            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@NRPERSONAL", nrpersonal, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idNdermarje, ParameterDirection.Input);
            dbManager.AddParameters(2, "@salt", salt, ParameterDirection.Input);
            var ekziston = Convert.ToInt32(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_PUNONJES_ekzistonPunonjes"));
            return ekziston == 1;
        }
        public bool ekzistonPunonjesPerKontrolloEkzisto(string nrpersonal, int idNdermarje, int idpunonjes)
        {
            string salt = WebConfigurationManager.AppSettings["salt"];
            dbManager.Open();
            dbManager.CreateParameters(4);
            dbManager.AddParameters(0, "@NRPERSONAL", nrpersonal, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idNdermarje, ParameterDirection.Input);
            dbManager.AddParameters(2, "@salt", salt, ParameterDirection.Input);
            dbManager.AddParameters(3, "@IDPUNONJES", idpunonjes, ParameterDirection.Input);
            var ekziston = Convert.ToInt32(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_PUNONJES_ekzistonPunonjesKontrolloEkzistenca"));
            return ekziston == 1;
        }
        public bool ekzistonUsername(string username, int idNdermarje)
        {
            string salt = WebConfigurationManager.AppSettings["salt"];
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@USERNAME", username, ParameterDirection.Input);
            dbManager.AddParameters(1, "@salt", salt, ParameterDirection.Input);
            var ekziston = Convert.ToInt32(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_PUNONJES_ekzistonUsername"));
            return ekziston == 1;
        }
        public bool ekzistonPunonjesNrSig(string nrpersonal, int idNdermarje, bool aktiv, int idpunonjes)
        {
            string salt = WebConfigurationManager.AppSettings["salt"];
            dbManager.Open();
            dbManager.CreateParameters(5);
            dbManager.AddParameters(0, "@NRSig", nrpersonal, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idNdermarje, ParameterDirection.Input);
            dbManager.AddParameters(2, "@aktiv", aktiv, ParameterDirection.Input);
            dbManager.AddParameters(3, "@salt", salt, ParameterDirection.Input);
            dbManager.AddParameters(4, "@IDPUNONJES", idpunonjes, ParameterDirection.Input);
            var ekziston = Convert.ToInt32(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_PUNONJES_ekzistonPunonjesNrSig"));
            return ekziston == 1;
        }
        public bool ekzistonPunonjesSap(string sap, int idNdermarje, int idpunonjes)
        {
            string salt = WebConfigurationManager.AppSettings["salt"];
            dbManager.Open();
            dbManager.CreateParameters(4);
            dbManager.AddParameters(0, "@SAPID", sap, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idNdermarje, ParameterDirection.Input);
            dbManager.AddParameters(2, "@salt", salt, ParameterDirection.Input);
            dbManager.AddParameters(3, "@IDPUNONJES", idpunonjes, ParameterDirection.Input);
            var ekziston = Convert.ToInt32(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_PUNONJES_ekzistonPunonjesSap"));
            return ekziston == 1;
        }
        public bool ekzistonPunonjesnrllog(string llogbank, int idNdermarje, int idpunonjes)
        {
            string salt = WebConfigurationManager.AppSettings["salt"];
            dbManager.Open();
            dbManager.CreateParameters(4);
            dbManager.AddParameters(0, "@llogbank", llogbank, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idNdermarje, ParameterDirection.Input);
            dbManager.AddParameters(2, "@salt", salt, ParameterDirection.Input);
            dbManager.AddParameters(3, "@IDPUNONJES", idpunonjes, ParameterDirection.Input);
            var ekziston = Convert.ToInt32(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_PUNONJES_ekzistonPunonjesLlogBank"));
            return ekziston == 1;
        }

        /// <summary>
        /// merr  punonjes sipas id
        /// </summary>
        /// <param name="idpunonjes"> id punonjes</param>
        /// <returns> kthen data row me kete punonjes</returns>
        internal DataRow merrPunonjesDR(int idpunonjes)
        {
            dbManager.Open();
            string salt = WebConfigurationManager.AppSettings["salt"];
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDPUNONJES", idpunonjes, ParameterDirection.Input);
            dbManager.AddParameters(1, "@salt", salt, ParameterDirection.Input);
            using (DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_PUNONJES_merrPunonjesSipasNdermarjesDR"))
            {
                if (ds == null)
                    return null;
                if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                    return null;
                return ds.Tables[0].Rows[0];
            }
        }

        /// <summary>
        /// merr gjithe punonjes sipas ndermarjes
        /// </summary>
        /// <param name="idnderm">id ndermarje</param>
        /// <returns> kthen nje datatable me te gjithe punonjesit e kesaj ndermarje</returns>
        internal DataTable merrPunonjesDT(int idnderm, int idGJuha)
        {
            string salt = WebConfigurationManager.AppSettings["salt"];
            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDGJUHA", idGJuha, ParameterDirection.Input);

            dbManager.AddParameters(2, "@salt", salt, ParameterDirection.Input);

            using (DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_PUNONJES_merrPunonjesSipasNdermarjesDT"))
            {
                return ds.Tables[0];
            }
        }

        internal DataTable merrKombesiaDT()
        {
            dbManager.Open();
            string salt = WebConfigurationManager.AppSettings["salt"];
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@salt", salt, ParameterDirection.Input);

            using (DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KOMBESIA_ktheDt"))
            {
                return ds.Tables[0];
            }
        }
        internal String merrKombesiaSipasID(int id)
        {
            dbManager.Open();
            string salt = WebConfigurationManager.AppSettings["salt"];
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@ID", id, ParameterDirection.Input);
            dbManager.AddParameters(1, "@salt", salt, ParameterDirection.Input);

            using (DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KOMBESIA_ktheSipasId"))
            {
                if (ds == null)
                    return "";
                if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                    return "";
                return ds.Tables[0].Rows[0][0].ToString();
            }
        }
        internal int merrKombesiaSipasPershkrimin(string pershkrim)
        {
            dbManager.Open();
            string salt = WebConfigurationManager.AppSettings["salt"];
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@pershkrim", pershkrim, ParameterDirection.Input);
            dbManager.AddParameters(1, "@salt", salt, ParameterDirection.Input);

            using (DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KOMBESIA_ktheSipasPershkrimit"))
            {
                if (ds == null)
                    return -1;
                if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                    return -1;
                return int.Parse(ds.Tables[0].Rows[0][0].ToString());
            }
        }
        internal int merrKombesiaSipasPershkriminSHQ(string pershkrim)
        {
            dbManager.Open();
            string salt = WebConfigurationManager.AppSettings["salt"];
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@pershkrimshq", pershkrim, ParameterDirection.Input);
            dbManager.AddParameters(1, "@salt", salt, ParameterDirection.Input);

            using (DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KOMBESIA_ktheSipasPershkrimitSHQ"))
            {
                if (ds == null)
                    return -1;
                if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                    return -1;
                return int.Parse(ds.Tables[0].Rows[0][0].ToString());
            }
        }
        internal String merrNdryshimPozicioniSipasID(int id)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@ID", id, ParameterDirection.Input);

            using (DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_NDRYSHIMPOZICION_ktheSipasId"))
            {
                if (ds == null)
                    return "";
                if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                    return "";
                return ds.Tables[0].Rows[0][0].ToString();
            }
        }
        internal int merrNdryshimPozicioniSipasPershkrimit(string pershkrimi)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@pershkrimi", pershkrimi, ParameterDirection.Input);

            using (DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_NDRYSHIMPOZICION_ktheSipasPershkrimit"))
            {
                if (ds == null)
                    return 0;
                if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                    return 0;
                return int.Parse(ds.Tables[0].Rows[0][0].ToString());
            }
        }
        internal DataTable merrNdryshimPozicioniDT(int idgjuha)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDgjuha", idgjuha, ParameterDirection.Input);
            using (DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_NDRYSHIMPOZICIONI_ktheDt"))
            {
                return ds.Tables[0];
            }
        }
        /// <summary>
        /// merr gjithe punonjesit sipas ndermarjes aktiv per lupa
        /// </summary>
        /// <param name="idnderm">id ndermarje</param>
        /// <returns> kthen nje datatable me te gjithe komponente e kesaj ndermarje</returns>
        internal DataTable merrPunonjesAktivNdermarjeDT(int idnderm)
        {
            dbManager.Open();
            string salt = WebConfigurationManager.AppSettings["salt"];

            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            dbManager.AddParameters(1, "@salt", salt, ParameterDirection.Input);
            using (DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_PUNONJES_merrPunonjesSipasNdermarjesPerLupe"))
            {
                return ds.Tables[0];
            }
        }
        /// <summary>
        /// merr gjithe punonjesit sipas ndermarjes aktiv per lupa
        /// </summary>
        /// <param name="idnderm">id ndermarje</param>
        /// <returns> kthen nje datatable me te gjithe komponente e kesaj ndermarje</returns>
        internal DataTable merrPunonjesAktivNdermarjeDTNeNivelRaportues(int idnderm)
        {
            dbManager.Open();
            string salt = WebConfigurationManager.AppSettings["salt"];

            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            dbManager.AddParameters(1, "@salt", salt, ParameterDirection.Input);
            using (DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_PUNONJES_merrPunonjesSipasNdermarjesPerLupeNeNivelRaportues"))
            {
                return ds.Tables[0];
            }
        }
        internal DataTable merrPunonjesAktivNdermarjeDTJoTeLarguar(int idnderm, DateTime data, int iddep, int idnendep)
        {
            string salt = WebConfigurationManager.AppSettings["salt"];
            dbManager.Open();
            dbManager.CreateParameters(5);
            dbManager.AddParameters(0, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            dbManager.AddParameters(1, "@Data", data, ParameterDirection.Input);
            dbManager.AddParameters(2, "@salt", salt, ParameterDirection.Input);
            dbManager.AddParameters(3, "@iddep", iddep, ParameterDirection.Input);
            dbManager.AddParameters(4, "@idnendep", idnendep, ParameterDirection.Input);
            using (DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_PUNONJES_merrPunonjesSipasNdermarjesPerLupeJoTeLarguar"))
            {
                return ds.Tables[0];
            }
        }
        #endregion

        /// <summary>
        /// Metodat e meposhtme i perkasin veprimeve qe kryhen nga objektet clsKompListPagese dhe colKompListPagese
        /// </summary>
        #region KOMP LIST PAGESA

        /// <summary>
        /// ekzekuton 	[prc_T_KOMPLISTPAGESE_ins] ne DB dhe kthen statusin e perfundimit te ekzekutimit te SP-se perkatese
        /// Shiko clsMesazh brenda ketij moduli per informacion te metejshem mbi vleren e kthimit te kesaj metode
        /// </summary>
        /// <param name="idkomp"> id e komp</param>
        /// <param name="idtrupi">id e trupit</param>
        /// <param name="idkomponente">id e komponentes te pages</param>
        /// <param name="vlera"> vlera e komponentes</param>
        /// <param name="vleraparam"> vlera e parametrit te komponentes</param>
        /// <returns>nje objekt clsMesazh qe tregon nese ruajtja eshte kryer ne rregull apo jo.</returns>
        internal clsMesazh ruajKompListPagesa(out int idkomp, int idtrupi, int idkomponente, decimal vleraparam, decimal vlera, string shenime, bool modifikuar)
        {
            idkomp = -1;

            string salt = WebConfigurationManager.AppSettings["salt"];
            dbManager.Open();
            dbManager.CreateParameters(8);
            dbManager.AddParameters(0, "@ID", idkomp, ParameterDirection.Output);
            dbManager.AddParameters(1, "@IDTRUPI", idtrupi, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDKOMPONENTE", idkomponente, ParameterDirection.Input);
            dbManager.AddParameters(3, "@VLERAPARAM", vleraparam, ParameterDirection.Input);
            dbManager.AddParameters(4, "@VLERA", vlera, ParameterDirection.Input);
            dbManager.AddParameters(5, "@salt", salt, ParameterDirection.Input);
            dbManager.AddParameters(6, "@shenime", shenime, ParameterDirection.Input);
            dbManager.AddParameters(7, "@modifikuar", modifikuar, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_KOMPLISTPAGESE_ins");
            idkomp = int.Parse(dbManager.Parameters[0].Value.ToString());
            return new clsMesazh(true, mesazhRuajtje);

        }

        /// <summary>
        /// ekzekuton prc_T_KOMPLISTPAGESE_upd ne DB dhe kthen statusin e perfundimit te ekzekutimit te SP-se perkatese
        /// Shiko clsMesazh brenda ketij moduli per informacion te metejshem mbi vleren e kthimit te kesaj metode
        /// <param name="idkomp"> id e komp</param>
        /// <param name="idtrupi">id e trupit</param>
        /// <param name="idkomponente">id e komponentes te pages</param>
        /// <param name="vlera"> vlera e komponentes</param>
        /// <param name="vleraparam"> vlera e parametrit te komponentes</param>
        /// <returns> nje objekt clsMesazh qe tregon nese modifikimi eshte kryer ne rregull apo jo.</returns>
        /// </summary>
        internal clsMesazh modifikoKompListPagese(int idkomp, int idtrupi, int idkomponente, decimal vleraparam, decimal vlera, string shenime, bool modifikuar)
        {

            string salt = WebConfigurationManager.AppSettings["salt"];
            dbManager.Open();
            dbManager.CreateParameters(8);
            dbManager.AddParameters(0, "@ID", idkomp, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDTRUPI", idtrupi, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDKOMPONENTE", idkomponente, ParameterDirection.Input);
            dbManager.AddParameters(3, "@VLERAPARAM", vleraparam, ParameterDirection.Input);
            dbManager.AddParameters(4, "@VLERA", vlera, ParameterDirection.Input);
            dbManager.AddParameters(5, "@salt", salt, ParameterDirection.Input);
            dbManager.AddParameters(6, "@shenime", shenime, ParameterDirection.Input);
            dbManager.AddParameters(7, "@modifikuar", modifikuar, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_KOMPLISTPAGESE_upd");
            return new clsMesazh(true, mesazhModifikimi);

        }

        /// <summary>
        /// ekzekutohet sp-ja prc_T_KOMPLISTPAGESE_delSipasIdKomp duke i kaluar id e komplistpagese
        /// </summary>
        /// <param name="idkomp">id e komplistpagese qe do fshihet </param>
        /// <returns>nje objekt clsMesazh qe tregon nese fshirja eshte kryer ne rregull apo jo</returns>
        internal clsMesazh fshiKompListPagese(int idkomp)
        {

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@ID", idkomp, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_KOMPLISTPAGESE_delSipasIdKomp");
            clsMesazh mesazh = new clsMesazh(true, mesazhFshirje);
            return mesazh;

        }

        /// <summary>
        /// ekzekutohet sp-ja prc_T_KOMPLISTPAGESE_delSipasIdTrupi duke i kaluar id e komplistpagese
        /// </summary>
        /// <param name="idtrupi">id e trupit qe do fshihet </param>
        /// <returns>nje objekt clsMesazh qe tregon nese fshirja eshte kryer ne rregull apo jo</returns>
        internal clsMesazh fshiKompListPageseSipasIdTrupi(int idtrupi)
        {

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDTRUPI", idtrupi, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_KOMPLISTPAGESE_delSipasIdTrupi");
            clsMesazh mesazh = new clsMesazh(true, mesazhFshirje);
            return mesazh;

        }

        /// <summary>
        /// merr komplist pagese sipas id
        /// </summary>
        /// <param name="id">  id e komplist pagese</param>
        /// <returns> kthen datarow qe permban komplistpagese me kete id</returns>
        internal void ktheKompListPagese(int id, clsKompListPagese kompListPagese)
        {
            string salt = WebConfigurationManager.AppSettings["salt"];
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@ID", id, ParameterDirection.Input);
            dbManager.AddParameters(1, "@salt", salt, ParameterDirection.Input);

            dbManager.FillObject<clsKompListPagese>("prc_T_KOMPLISTPAGESE_sel", kompListPagese.mbushKomponenteListPagese);
        }

        /// <summary>
        /// merr gjithe komponente list pagese sipas idtrupi
        /// </summary>
        /// <param name="idtrupi"> id trupi</param>
        /// <returns> nje datatable qe permban nje koleksion me te komplistpagese</returns>
        internal IEnumerable<clsKompListPagese> ktheKompListPageseSipasIdTrupi(int idtrupi)
        {
            string salt = WebConfigurationManager.AppSettings["salt"];
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDTrupi", idtrupi, ParameterDirection.Input);
            dbManager.AddParameters(1, "@salt", salt, ParameterDirection.Input);
            return dbManager.GetIEnumerbale("prc_T_KOMPLISTPAGESE_selAllSipasTrupi", clsKompListPagese.Krijo);

        }

        /// <summary>
        /// merr gjithe komp list pagese fillestare
        /// </summary>
        /// <param name="idndermarje">id e ndermarje</param>
        /// <param name="lloji">lloji i komponentes</param>
        /// <param name="data"> data e aktivizimit</param>
        /// <returns> nje datatable qe permban nje koleksion me te komp list pagese ne ate date</returns>
        internal IEnumerable<clsKompListPagese> ktheKompListPageseFillestare(bool lloji, DateTime data, int idndermarje, int idpunonjes)
        {
            string salt = WebConfigurationManager.AppSettings["salt"];
            dbManager.Open();
            dbManager.CreateParameters(5);
            dbManager.AddParameters(0, "@LLOJI", lloji, ParameterDirection.Input);
            dbManager.AddParameters(1, "@DTAKTIVIZIMI", data, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDNDERMARJE", idndermarje, ParameterDirection.Input);
            dbManager.AddParameters(3, "@idpunonjes", idpunonjes, ParameterDirection.Input);
            dbManager.AddParameters(4, "@salt", salt, ParameterDirection.Input);
            return dbManager.GetIEnumerbale("prc_T_KOMPLISTPAGESE_merrKompListPageseFillestare", clsKompListPagese.Krijo);

        }
        internal IEnumerable<clsKompListPagese> ktheKompListPageseFillestarePaKomponenteMuaji(bool lloji, DateTime data, int idndermarje, int idpunonjes)
        {
            string salt = WebConfigurationManager.AppSettings["salt"];
            dbManager.Open();
            dbManager.CreateParameters(5);
            dbManager.AddParameters(0, "@LLOJI", lloji, ParameterDirection.Input);
            dbManager.AddParameters(1, "@DTAKTIVIZIMI", data, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDNDERMARJE", idndermarje, ParameterDirection.Input);
            dbManager.AddParameters(3, "@idpunonjes", idpunonjes, ParameterDirection.Input);
            dbManager.AddParameters(4, "@salt", salt, ParameterDirection.Input);

            return dbManager.GetIEnumerbale("prc_T_KOMPLISTPAGESE_merrKompListPageseFillestare", clsKompListPagese.Krijo);


        }
        internal IEnumerable<clsKompListPagese> ktheKompListPageseFillestarePerPunonjesitPaKomponenteMuaji(bool lloji, DateTime data, int idndermarje, List<int> punonjesitIDs)
        {
            string salt = WebConfigurationManager.AppSettings["salt"];
            dbManager.Open();
            dbManager.CreateParameters(5);
            dbManager.AddParameters(0, "@LLOJI", lloji, ParameterDirection.Input);
            dbManager.AddParameters(1, "@DTAKTIVIZIMI", data, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDNDERMARJE", idndermarje, ParameterDirection.Input);
            dbManager.AddParameters(3, "@PUNONJESITIDS", string.Join(",", punonjesitIDs), ParameterDirection.Input);
            dbManager.AddParameters(4, "@salt", salt, ParameterDirection.Input);

            return dbManager.GetIEnumerbale("prc_T_KOMPLISTPAGESE_merrKompListPageseFillestarePerPunonjesit", clsKompListPagese.Krijo);


        }
        internal IEnumerable<clsKompListPagese> ktheKompListPageseFillestarePerPunonjesitPaKomponenteMuajiBasic(bool lloji, DateTime data, int idndermarje, List<int> punonjesitIDs)
        {
            string salt = WebConfigurationManager.AppSettings["salt"];
            dbManager.Open();
            dbManager.CreateParameters(5);
            dbManager.AddParameters(0, "@LLOJI", lloji, ParameterDirection.Input);
            dbManager.AddParameters(1, "@DTAKTIVIZIMI", data, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDNDERMARJE", idndermarje, ParameterDirection.Input);
            dbManager.AddParameters(3, "@PUNONJESITIDS", string.Join(",", punonjesitIDs), ParameterDirection.Input);
            dbManager.AddParameters(4, "@salt", salt, ParameterDirection.Input);

            return dbManager.GetIEnumerbale("prc_T_KOMPLISTPAGESE_merrKompListPageseFillestarePerPunonjesitBasic", clsKompListPagese.KrijoBasic);


        }
        internal IEnumerable<clsKompListPagese> KtheKompListPageseFillestarePerPunonjesitSipasDatavePaKomponenteMuajiBasic(bool lloji, DateTime data, int idndermarje, List<PunonjesMeData> punonjesMeData)
        {
            string salt = WebConfigurationManager.AppSettings["salt"];
            dbManager.Open();
            dbManager.CreateParameters(4);
            dbManager.AddParameters("@LLOJI", lloji, ParameterDirection.Input);
            dbManager.AddParameters("@IDNDERMARJE", idndermarje, ParameterDirection.Input);
            dbManager.AddParameters("@PUNONJESMEDATA", ListExtensions.ToDataTable(punonjesMeData, "IdPunonjesi", "Data"), ParameterDirection.Input);
            dbManager.AddParameters("@salt", salt, ParameterDirection.Input);

            return dbManager.GetIEnumerbale("prc_T_KOMPLISTPAGESE_merrKompListPageseFillestarePerPunonjesitBasic", clsKompListPagese.KrijoBasicMeDate);


        }
        internal IEnumerable<clsKompListPagese> MerrKomponenteLispagesePerPunonjesitSipasDates(List<int> ids, DateTime data)
        {

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@PUNONJESITIDS", string.Join(",", ids), ParameterDirection.Input);
            dbManager.AddParameters(1, "@DTAKTIVIZIMI", data, ParameterDirection.Input);
            return dbManager.GetIEnumerbale("prc_T_KOMPLISTPAGESE_merrKompListPageseSipasPunonjesveDheDates", clsKompListPagese.Krijo);


        }
        internal IEnumerable<clsKompListPagese> MerrKomponenteLispagesePerPunonjesitSipasDatesBasic(List<int> ids, DateTime data)
        {

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@PUNONJESITIDS", string.Join(",", ids), ParameterDirection.Input);
            dbManager.AddParameters(1, "@DTAKTIVIZIMI", data, ParameterDirection.Input);
            return dbManager.GetIEnumerbale("prc_T_KOMPLISTPAGESE_merrKompListPageseSipasPunonjesveDheDatesBasic", clsKompListPagese.KrijoBasic);


        }
        /// <summary>
        /// merr gjithe komp list pagese sipas punonjesit dhe dates
        /// </summary>
        /// <param name="idpunonjes">lloji i punonjes</param>
        /// <param name="data"> data e aktivizimit</param>
        /// <param name="idNdermarrje">id e ndermarrjes</param>
        /// <returns> nje datatable qe permban nje koleksion me te komp list pagese ne ate date</returns>
        internal IEnumerable<clsKompListPagese> ktheKompListPagesePunonjesDheDate(int idPunonjes, DateTime data, int idNdermarrje)
        {
            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@IDPUNONJES", idPunonjes, ParameterDirection.Input);
            dbManager.AddParameters(1, "@DTAKTIVIZIMI", data, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDNDERMARJE", idNdermarrje, ParameterDirection.Input);
            return dbManager.GetIEnumerbale("prc_T_KOMPLISTPAGESE_merrKompListPageseSipasPunonjesit", clsKompListPagese.Krijo);
        }

        /// <summary>
        /// merr ditet e harxhuara te lejes
        /// </summary>
        /// <param name="idnderviti">id e ndervitit</param>
        /// <param name="idpunonjesi">id e punonjesit</param>
        /// <param name="data"> data e aktivizimit</param>
        /// <returns> shumen e diteve te konsumuara</returns>
        internal decimal ktheKompListPageseDiteTeHarxhuara(int idpunonjesi, DateTime data, int idnderviti)
        {

            string salt = WebConfigurationManager.AppSettings["salt"];
            dbManager.Open();
            dbManager.CreateParameters(4);
            dbManager.AddParameters(0, "@IDPunonjesi", idpunonjesi, ParameterDirection.Input);
            dbManager.AddParameters(1, "@date", data, ParameterDirection.Input);
            dbManager.AddParameters(2, "@idnderviti", idnderviti, ParameterDirection.Input);
            dbManager.AddParameters(3, "@salt", salt, ParameterDirection.Input);
            return Convert.ToDecimal(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_KOMPLISTPAGESE_merrDiteLejeTeHarxhuara"));

        }
        internal Dictionary<int, decimal> ktheKompListPageseDiteTeHarxhuara(List<int> idpunonjesish, DateTime data, int idnderviti)
        {

            string salt = WebConfigurationManager.AppSettings["salt"];
            dbManager.Open();
            dbManager.CreateParameters(4);
            dbManager.AddParameters(0, "@PUNONJESITIDS", string.Join(",", idpunonjesish), ParameterDirection.Input);
            dbManager.AddParameters(1, "@date", data, ParameterDirection.Input);
            dbManager.AddParameters(2, "@idnderviti", idnderviti, ParameterDirection.Input);
            dbManager.AddParameters(3, "@salt", salt, ParameterDirection.Input);
            return dbManager.GetDictionary<int, decimal>("prc_T_KOMPLISTPAGESE_merrDiteLejeTeHarxhuaraAll");

        }
        #endregion

        /// <summary>
        /// Metodat e meposhtme i perkasin veprimeve qe kryhen nga objektet clsKompListPagese dhe colKompListPagese
        /// </summary>
        #region KOMPONENTEMUAJI

        /// <summary>
        /// ekzekuton 	[prc_T_KOMPONENTEMUAJI_ins] ne DB dhe kthen statusin e perfundimit te ekzekutimit te SP-se perkatese
        /// Shiko clsMesazh brenda ketij moduli per informacion te metejshem mbi vleren e kthimit te kesaj metode
        /// </summary>
        /// <param name="idkomp"> id e komp</param>
        /// <param name="idtrupi">id e trupit</param>
        /// <param name="idkomponente">id e komponentes te pages</param>
        /// <param name="vlera"> vlera e komponentes</param>
        /// <param name="vleraparam"> vlera e parametrit te komponentes</param>
        /// <returns>nje objekt clsMesazh qe tregon nese ruajtja eshte kryer ne rregull apo jo.</returns>
        internal clsMesazh ruajKompMuaji(out int idkomp, int idkomplistpagese, int muaji, int viti, decimal vleraparam, decimal vlera)
        {
            idkomp = -1;

            string salt = WebConfigurationManager.AppSettings["salt"];
            dbManager.Open();
            dbManager.CreateParameters(7);
            dbManager.AddParameters(0, "@ID", idkomp, ParameterDirection.Output);
            dbManager.AddParameters(1, "@IDKOMPLIST", idkomplistpagese, ParameterDirection.Input);
            dbManager.AddParameters(2, "@muaji", muaji, ParameterDirection.Input);
            dbManager.AddParameters(3, "@VLERAPARAM", vleraparam, ParameterDirection.Input);
            dbManager.AddParameters(4, "@VLERA", vlera, ParameterDirection.Input);
            dbManager.AddParameters(5, "@salt", salt, ParameterDirection.Input);
            dbManager.AddParameters(6, "@viti", viti, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_KOMPONENTEMUAJI_ins");
            idkomp = int.Parse(dbManager.Parameters[0].Value.ToString());
            return new clsMesazh(true, mesazhRuajtje);

        }

        /// <summary>
        /// ekzekuton prc_T_KOMPONENTEMUAJI_upd ne DB dhe kthen statusin e perfundimit te ekzekutimit te SP-se perkatese
        /// Shiko clsMesazh brenda ketij moduli per informacion te metejshem mbi vleren e kthimit te kesaj metode
        /// <param name="idkomp"> id e komp</param>
        /// <param name="idtrupi">id e trupit</param>
        /// <param name="idkomponente">id e komponentes te pages</param>
        /// <param name="vlera"> vlera e komponentes</param>
        /// <param name="vleraparam"> vlera e parametrit te komponentes</param>
        /// <returns> nje objekt clsMesazh qe tregon nese modifikimi eshte kryer ne rregull apo jo.</returns>
        /// </summary>
        internal clsMesazh modifikoKompMuaji(int idkomp, int idkomplistpagese, int muaji, int viti, decimal vleraparam, decimal vlera)
        {

            string salt = WebConfigurationManager.AppSettings["salt"];
            dbManager.Open();
            dbManager.CreateParameters(7);
            dbManager.AddParameters(0, "@ID", idkomp, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDKOMPLIST", idkomplistpagese, ParameterDirection.Input);
            dbManager.AddParameters(2, "@muaji", muaji, ParameterDirection.Input);
            dbManager.AddParameters(3, "@VLERAPARAM", vleraparam, ParameterDirection.Input);
            dbManager.AddParameters(4, "@VLERA", vlera, ParameterDirection.Input);
            dbManager.AddParameters(5, "@salt", salt, ParameterDirection.Input);
            dbManager.AddParameters(6, "@viti", viti, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_KOMPONENTEMUAJI_upd");
            return new clsMesazh(true, mesazhModifikimi);

        }

        /// <summary>
        /// ekzekutohet sp-ja prc_T_KOMPONENTEMUAJI_delSipasIdKomp duke i kaluar id e komplistpagese
        /// </summary>
        /// <param name="idkomp">id e komplistpagese qe do fshihet </param>
        /// <returns>nje objekt clsMesazh qe tregon nese fshirja eshte kryer ne rregull apo jo</returns>
        internal clsMesazh fshiKompMuaji(int idkomp)
        {

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDKOMPLIST", idkomp, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_KOMPONENTEMUAJI_delSipasIdKomp");
            clsMesazh mesazh = new clsMesazh(true, mesazhFshirje);
            return mesazh;

        }


        /// <summary>
        /// merr gjithe komponente list pagese sipas idtrupi
        /// </summary>
        /// <param name="idtrupi"> id trupi</param>
        /// <returns> nje datatable qe permban nje koleksion me te komplistpagese</returns>
        internal IEnumerable<clsKomponenteMuaji> ktheKompMuajiSipasIdTrupi(int idkomp)
        {
            string salt = WebConfigurationManager.AppSettings["salt"];
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@idkomplistpagese", idkomp, ParameterDirection.Input);
            dbManager.AddParameters(1, "@salt", salt, ParameterDirection.Input);
            return dbManager.GetIEnumerbale("prc_T_KOMPONENTEMUAJI_selAllSipasTrupi", clsKomponenteMuaji.Krijo);

        }


        #endregion

        /// <summary>
        /// Metodat e meposhtme i perkasin veprimeve qe kryhen nga objektet clsTrupiListPagese dhe colTrupiListPagese
        /// </summary>

        #region TRUPI LIST PAGESE

        /// <summary>
        /// ekzekuton prc_T_TRUPILISTPAGESE_ins ne DB dhe kthen statusin e perfundimit te ekzekutimit te SP-se perkatese
        /// Shiko clsMesazh brenda ketij moduli per informacion te metejshem mbi vleren e kthimit te kesaj metode
        /// <param name="idPunonjes"> id e punonjes</param>
        /// <param name="idKoka"> id e kokes se dokumentit </param>
        /// <param name="idTrup">id ritese e trupit te dokumentit </param>
        /// <param name="paguar"> paguar</param>
        /// <returns> nje objekt clsMesazh qe tregon nese ruajtja eshte kryer ne rregull apo jo.</returns>
        /// </summary>
        internal clsMesazh ruajTrupiListPagese(out int idTrup, int idKoka, int idPunonjes, decimal paguar,
            string shenime, decimal cost)
        {
            idTrup = -1;

            string salt = WebConfigurationManager.AppSettings["salt"];
            dbManager.Open();
            dbManager.CreateParameters(7);
            dbManager.AddParameters(0, "@IDTRUPI", idTrup, ParameterDirection.Output);
            dbManager.AddParameters(1, "@IDKOKA", idKoka, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDPUNONJES", idPunonjes, ParameterDirection.Input);
            dbManager.AddParameters(3, "@PAGUAR", paguar, ParameterDirection.Input);
            dbManager.AddParameters(4, "@salt", salt, ParameterDirection.Input);
            dbManager.AddParameters(5, "@shenime", shenime, ParameterDirection.Input);
            dbManager.AddParameters(6, "@cost", cost, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_TRUPILISTPAGESE_ins");

            idTrup = int.Parse(dbManager.Parameters[0].Value.ToString());

            return new clsMesazh(true, mesazhRuajtje);

        }

        public clsMesazh RuajTrupListPagese(DataTable dtTrupi, DataTable dtKomp, DataTable dtKompMuaj)
        {
            string salt = WebConfigurationManager.AppSettings["salt"];
            dbManager.Open();
            dbManager.CreateParameters(4);
            dbManager.AddParameters(0, "@DTTRUPI", dtTrupi, ParameterDirection.Input);
            dbManager.AddParameters(1, "@DTKOMP", dtKomp, ParameterDirection.Input);
            dbManager.AddParameters(2, "@DTKOMPMUAJI", dtKompMuaj, ParameterDirection.Input);
            dbManager.AddParameters(3, "@salt", salt, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_TRUPILISTPAGESE_insDT");
            return new clsMesazh(true, mesazhRuajtje);
        }
        /// <summary>
        /// ekzekuton prc_T_TRUPILISTPAGESE_upd ne DB dhe kthen statusin e perfundimit te ekzekutimit te SP-se perkatese
        /// Shiko clsMesazh brenda ketij moduli per informacion te metejshem mbi vleren e kthimit te kesaj metode
        /// <param name="idPunonjes"> id e punonjes</param>
        /// <param name="idKoka"> id e kokes se dokumentit </param>
        /// <param name="idTrup">id ritese e trupit te dokumentit </param>
        /// <param name="paguar"> paguar</param>

        /// <returns> nje objekt clsMesazh qe tregon nese modifikimi eshte kryer ne rregull apo jo.</returns>
        /// </summary>
        internal clsMesazh modifikoTrupiListPagese(int idTrup, int idKoka, int idPunonjes, decimal paguar, string shenime, decimal cost)
        {
            string salt = WebConfigurationManager.AppSettings["salt"];

            dbManager.Open();
            dbManager.CreateParameters(7);
            dbManager.AddParameters(0, "@IDTRUPI", idTrup, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDKOKA", idKoka, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDPUNONJES", idPunonjes, ParameterDirection.Input);
            dbManager.AddParameters(3, "@PAGUAR", paguar, ParameterDirection.Input);
            dbManager.AddParameters(4, "@salt", salt, ParameterDirection.Input);
            dbManager.AddParameters(5, "@shenime", shenime, ParameterDirection.Input);
            dbManager.AddParameters(6, "@cost", cost, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_TRUPILISTPAGESE_upd");
            clsMesazh mesazh = new clsMesazh(true, mesazhModifikimi);
            return mesazh;
        }

        /// <summary>
        /// ekzekutohet sp-ja prc_T_TRUPILISTPAGESE_delSipasIdKoka duke i kaluar id e kokes se dokumentit
        /// </summary>
        /// <param name="idKoka"> id e kokes se dokumentit </param>
        /// <returns>nje objekt clsMesazh qe tregon nese fshirja eshte kryer ne rregull apo jo</returns>
        internal clsMesazh fshiTrupiListPageseSipasKoka(int idKoka)
        {

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDKOKA", idKoka, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_TRUPILISTPAGESE_delSipasIdKoka");
            clsMesazh mesazh = new clsMesazh(true, mesazhFshirje);
            return mesazh;

        }

        /// <summary>
        /// ekzekutohet sp-ja prc_T_TRUPILISTPAGESE_delSipasIdTrupi duke i kaluar id e trupit te dokumentit 
        /// </summary>
        /// <param name="idTrup">id ritese e trupit te dokumentit </param>
        /// <returns>nje objekt clsMesazh qe tregon nese fshirja eshte kryer ne rregull apo jo</returns>
        internal clsMesazh fshiTrupiListPageseSipasID(int idTrup)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDTRUPI", idTrup, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_TRUPILISTPAGESE_delSipasIdTrupi");
            clsMesazh mesazh = new clsMesazh(true, mesazhFshirje);
            return mesazh;

        }

        /// <summary>
        /// kthen datatable trupi dokumenti sipas kokes
        /// </summary>
        ///<param name="idKoka"> id e kokes se dokumentit</param>
        ///<returns>nje datatable qe permban nje koleksion me te gjitha trupat e dokumentit  te kesaj koke  </returns>
        internal DataTable ktheGjitheTrupiListPageseNgaKoka(int idKoka)
        {
            string salt = WebConfigurationManager.AppSettings["salt"];
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDKOKA", idKoka, ParameterDirection.Input);
            dbManager.AddParameters(1, "@salt", salt, ParameterDirection.Input);
            using (DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_TRUPILISTPAGESE_selAllSipasKoka"))
            {
                return ds.Tables[0];
            }
        }
        internal IEnumerable<clsTrupiListPagese> ktheGjitheTrupListPageseNgaKokaList(int idKoka)
        {
            string salt = WebConfigurationManager.AppSettings["salt"];
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDKOKA", idKoka, ParameterDirection.Input);
            dbManager.AddParameters(1, "@salt", salt, ParameterDirection.Input);
            return dbManager.GetIEnumerbale("prc_T_TRUPILISTPAGESE_selAllSipasKoka", clsTrupiListPagese.Krijo);
        }

        /// <summary>
        /// kthen datarow trupi dokumenti sipas idse se trupit te dokumentit
        /// </summary>
        ///<param name="idTrupi"> trupi i dokumentit te  nga merret id</param>
        ///<returns>nje datarow me te gjithe trupin e dokumentit te shitjes te kesaj id  </returns>
        internal DataRow ktheTrupiListPageseSipasID(int idTrupi)
        {
            string salt = WebConfigurationManager.AppSettings["salt"];
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDTRUPI", idTrupi, ParameterDirection.Input);
            dbManager.AddParameters(1, "@salt", salt, ParameterDirection.Input);
            using (DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_TRUPILISTPAGESE_sel"))
            {
                if (ds == null)
                    return null;
                if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                    return null;
                return ds.Tables[0].Rows[0];
            }
        }

        #endregion

        /// <summary>
        /// Metodat e meposhtme i perkasin veprimeve qe kryhen nga objektet clsKokaListPagese dhe colKokaListPagese
        /// </summary>
        #region KOKA LIST PAGESE

        /// <summary>
        /// ekzekuton prc_T_KOKALISTPAGESE_ins ne DB dhe kthen statusin e perfundimit te ekzekutimit te SP-se perkatese
        /// Shiko clsMesazh brenda ketij moduli per informacion te metejshem mbi vleren e kthimit te kesaj metode
        /// <param name="dtDk"> data e dokumentit </param>
        /// <param name="dtRegj"> data e regjistrimit te dokumentit /param>
        /// <param name="idmonedha"> id monedha.</param>
        /// <param name="iddep"> id e departamentit</param>
        /// <param name="idKonf"> id e konfigurimit te dokumentit</param>
        /// <param name="iddoknga">id e dokumentit nga e cila gjenerohet ne rastet e modifikimit </param>
        /// <param name="idnendep">id e magazines</param>
        /// <param name="idKoka">id ritese e kokes se magazines</param>
        /// <param name="idNder"> id e ndermarjes</param>
        /// <param name="idNdVt"> id e ndermarje vitit</param>
        /// <param name="idNiv"> id e nivelit te regjistrimit</param>
        /// <param name="idPer"> id e perdoruesit se e ben regjistrimin</param>
        /// <param name="muaji"> muaji</param>
        /// <param name="idSt"> id e statusit te dokumentit</param>
        /// <param name="nrDk">nr i dokumentit</param>
        /// <param name="totali"> totali</param>
        /// <param name="shenim"> shenime </param>
        /// <param name="kursi"> kursi</param>
        /// <param name="idGjenerues">id e dokumentit nga gjenerohet magazina nga nje ambjent tjeter</param>
        /// <param name="idKonfigGjenerues">id e konfigurimit nga gjenerohet dokumenti</param>
        /// <param name="idNivelGjenerues">id e nivelit nga gjenerohet dokumenti</param>
        /// <returns> nje objekt clsMesazh qe tregon nese ruajtja eshte kryer ne rregull apo jo.</returns>
        /// </summary>
        internal clsMesazh ruajKokaListPagese(out int idKoka, int idNiv, int idKonf, int iddep, int idnendep, DateTime dtDk, string nrDk, int muaji, decimal totali, int idmonedha,
            int iddoknga, decimal kursi, int idSt, int idNder, int idNdVt, int idPer, DateTime dtRegj, string shenim, int idNivelGjenerues, int idKonfigGjenerues, int idGjenerues, StatusAprovimi statusapp, decimal tolaindermarje)
        {
            idKoka = -1;

            dbManager.Open();
            dbManager.CreateParameters(23);
            dbManager.AddParameters(0, "@IDKOKA", idKoka, ParameterDirection.Output);
            dbManager.AddParameters(1, "@IDNIVEL", idNiv, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDKONFIGAMBJENTE", idKonf, ParameterDirection.Input);
            if (iddep == 0) dbManager.AddParameters(3, "@IDDEPARTAMENTI", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(3, "@IDDEPARTAMENTI", iddep, ParameterDirection.Input);
            if (idnendep == 0) dbManager.AddParameters(4, "@IDNENDEPARTAMENTI", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(4, "@IDNENDEPARTAMENTI", idnendep, ParameterDirection.Input);
            dbManager.AddParameters(5, "@NRDOK", nrDk, ParameterDirection.Input);
            dbManager.AddParameters(6, "@DTDOK", dtDk, ParameterDirection.Input);
            dbManager.AddParameters(7, "@MUAJI", muaji, ParameterDirection.Input);
            dbManager.AddParameters(8, "@TOTALI", totali, ParameterDirection.Input);
            dbManager.AddParameters(9, "@IDMONEDHA", idmonedha, ParameterDirection.Input);
            if (iddoknga == 0) dbManager.AddParameters(10, "@IDDOKNGA", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(10, "@IDDOKNGA", iddoknga, ParameterDirection.Input);
            dbManager.AddParameters(11, "@KURSI", kursi, ParameterDirection.Input);
            dbManager.AddParameters(12, "@IDSTATUSDOK", idSt, ParameterDirection.Input);
            dbManager.AddParameters(13, "@IDNDERMARJE", idNder, ParameterDirection.Input);
            dbManager.AddParameters(14, "@IDNDERVITI", idNdVt, ParameterDirection.Input);
            dbManager.AddParameters(15, "@IDPERDORUESI", idPer, ParameterDirection.Input);
            dbManager.AddParameters(16, "@DTREGJ", dtRegj, ParameterDirection.Input);
            dbManager.AddParameters(17, "@SHENIME", shenim, ParameterDirection.Input);
            if (idNivelGjenerues == 0) dbManager.AddParameters(18, "@IDNIVELGJENERUES", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(18, "@IDNIVELGJENERUES", idNivelGjenerues, ParameterDirection.Input);
            if (idKonfigGjenerues == 0) dbManager.AddParameters(19, "@IDKONFIGGJENERUES", DBNull.Value, ParameterDirection.Input);

            else dbManager.AddParameters(19, "@IDKONFIGGJENERUES", idKonfigGjenerues, ParameterDirection.Input);
            if (idGjenerues == 0) dbManager.AddParameters(20, "@IDGJENERUES", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(20, "@IDGJENERUES", idGjenerues, ParameterDirection.Input);
            dbManager.AddParameters(21, "@STATUSAPROVIMI", statusapp, ParameterDirection.Input);
            dbManager.AddParameters(22, "@TOTALINDERMARJE", tolaindermarje, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_KOKALISTPAGESE_ins");

            idKoka = int.Parse(dbManager.Parameters[0].Value.ToString());

            clsMesazh mesazh = new clsMesazh(true, mesazhRuajtje);
            return mesazh;
        }
        public bool ekzistonListepagesa(DateTime dtdok, DateTime dtpasardhese, int idNdermarrje)
        {
            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@DTDOK", dtdok, ParameterDirection.Input);
            dbManager.AddParameters(1, "@DTDOKPAS", dtpasardhese, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDNDERMARRJE", idNdermarrje, ParameterDirection.Input);
            int ekziston = Convert.ToInt32(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_KOKALISTEPAGESE_ekzistonListepagesa"));

            if (ekziston > 0)
                return true;
            else
                return false;

        }
        public bool ekzistonListePagesaPerPunonjes(DateTime dtdok, DateTime dtpasardhese, int idPunonjesi)
        {
            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@DTDOK", dtdok, ParameterDirection.Input);
            dbManager.AddParameters(1, "@DTDOKPAS", dtpasardhese, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDPUNONJESI", idPunonjesi, ParameterDirection.Input);
            int ekziston = Convert.ToInt32(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_KOKALISTEPAGESE_ekzistonListepagesaPerPunonjes"));
            return ekziston > 0;
        }
        public bool ekzistonListePagesaPerPunonjesSipasNrPersonal(string nrPersonal, int idNdermarrje, int viti, int muaji)
        {
            var salt = WebConfigurationManager.AppSettings["salt"];
            dbManager.Open();
            dbManager.CreateParameters(5);
            dbManager.AddParameters("@SALT", salt, ParameterDirection.Input);
            dbManager.AddParameters("@NRPERSONAL", nrPersonal, ParameterDirection.Input);
            dbManager.AddParameters("@MUAJI", muaji, ParameterDirection.Input);
            dbManager.AddParameters("@VITI", viti, ParameterDirection.Input);
            dbManager.AddParameters("@IDNDERMARRJE", idNdermarrje, ParameterDirection.Input);
            var result = dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_KOKALISTEPAGESE_ekzistonListepagesaPerPunonjesSipasNrPersonal");
            return Convert.ToInt32(result) > 0;

        }
        /// <summary>
        /// ekzekuton prc_T_KOKALISTPAGESE_upd ne DB dhe kthen statusin e perfundimit te ekzekutimit te SP-se perkatese
        /// Shiko clsMesazh brenda ketij moduli per informacion te metejshem mbi vleren e kthimit te kesaj metode
        /// <param name="dtDk"> data e dokumentit </param>
        /// <param name="dtRegj"> data e regjistrimit te dokumentit /param>
        /// <param name="idmonedha"> id monedha.</param>
        /// <param name="iddep"> id e departamentit</param>
        /// <param name="idKonf"> id e konfigurimit te dokumentit</param>
        /// <param name="iddoknga">id e dokumentit nga e cila gjenerohet ne rastet e modifikimit </param>
        /// <param name="idnendep">id e magazines</param>
        /// <param name="idKoka">id ritese e kokes se magazines</param>
        /// <param name="idNder"> id e ndermarjes</param>
        /// <param name="idNdVt"> id e ndermarje vitit</param>
        /// <param name="idNiv"> id e nivelit te regjistrimit</param>
        /// <param name="idPer"> id e perdoruesit se e ben regjistrimin</param>
        /// <param name="muaji"> muaji</param>
        /// <param name="idSt"> id e statusit te dokumentit</param>
        /// <param name="nrDk">nr i dokumentit</param>
        /// <param name="totali"> totali</param>
        /// <param name="shenim"> shenime </param>
        /// <param name="kursi"> kursi</param>
        /// <param name="idGjenerues">id e dokumentit nga gjenerohet magazina nga nje ambjent tjeter</param>
        /// <param name="idKonfigGjenerues">id e konfigurimit nga gjenerohet dokumenti</param>
        /// <param name="idNivelGjenerues">id e nivelit nga gjenerohet dokumenti</param>
        /// <returns> nje objekt clsMesazh qe tregon nese modifikimi eshte kryer ne rregull apo jo.</returns>
        /// </summary>
        internal clsMesazh modifikoKokaListPagese(int idKoka, int idNiv, int idKonf, int iddep, int idnendep, DateTime dtDk, string nrDk, int muaji, decimal totali, int idmonedha,
        int iddoknga, decimal kursi, int idSt, int idNder, int idNdVt, int idPer, DateTime dtRegj, string shenim, int idNivelGjenerues, int idKonfigGjenerues, int idGjenerues, StatusAprovimi statusapp, decimal tolaindermarje)
        {
            dbManager.Open();
            dbManager.CreateParameters(23);
            dbManager.AddParameters(0, "@IDKOKA", idKoka, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNIVEL", idNiv, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDKONFIGAMBJENTE", idKonf, ParameterDirection.Input);
            if (iddep == 0) dbManager.AddParameters(3, "@IDDEPARTAMENTI", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(3, "@IDDEPARTAMENTI", iddep, ParameterDirection.Input);
            if (idnendep == 0) dbManager.AddParameters(4, "@IDNENDEPARTAMENTI", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(4, "@IDNENDEPARTAMENTI", idnendep, ParameterDirection.Input);
            dbManager.AddParameters(5, "@NRDOK", nrDk, ParameterDirection.Input);
            dbManager.AddParameters(6, "@DTDOK", dtDk, ParameterDirection.Input);
            dbManager.AddParameters(7, "@MUAJI", muaji, ParameterDirection.Input);
            dbManager.AddParameters(8, "@TOTALI", totali, ParameterDirection.Input);
            dbManager.AddParameters(9, "@IDMONEDHA", idmonedha, ParameterDirection.Input);
            if (iddoknga == 0) dbManager.AddParameters(10, "@IDDOKNGA", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(10, "@IDDOKNGA", iddoknga, ParameterDirection.Input);
            dbManager.AddParameters(11, "@KURSI", kursi, ParameterDirection.Input);
            dbManager.AddParameters(12, "@IDSTATUSDOK", idSt, ParameterDirection.Input);
            dbManager.AddParameters(13, "@IDNDERMARJE", idNder, ParameterDirection.Input);
            dbManager.AddParameters(14, "@IDNDERVITI", idNdVt, ParameterDirection.Input);
            dbManager.AddParameters(15, "@IDPERDORUESI", idPer, ParameterDirection.Input);
            dbManager.AddParameters(16, "@DTREGJ", dtRegj, ParameterDirection.Input);
            dbManager.AddParameters(17, "@SHENIME", shenim, ParameterDirection.Input);
            if (idNivelGjenerues == 0) dbManager.AddParameters(18, "@IDNIVELGJENERUES", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(18, "@IDNIVELGJENERUES", idNivelGjenerues, ParameterDirection.Input);
            if (idKonfigGjenerues == 0) dbManager.AddParameters(19, "@IDKONFIGGJENERUES", DBNull.Value, ParameterDirection.Input);

            else dbManager.AddParameters(19, "@IDKONFIGGJENERUES", idKonfigGjenerues, ParameterDirection.Input);
            if (idGjenerues == 0) dbManager.AddParameters(20, "@IDGJENERUES", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(20, "@IDGJENERUES", idGjenerues, ParameterDirection.Input);
            dbManager.AddParameters(21, "@STATUSAPROVIMI", statusapp, ParameterDirection.Input);
            dbManager.AddParameters(22, "@TOTALINDERMARJE", tolaindermarje, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_KOKALISTPAGESE_upd");
            clsMesazh mesazh = new clsMesazh(true, mesazhModifikimi);
            return mesazh;
        }

        /// <summary>
        /// ekzekutohet sp-ja prc_T_KOKALISTPAGESE_del duke i kaluar id e kokes se dokumentit qe e marrim nga objekti 
        /// </summary>
        /// <param name="idkoka">id ritese e kokes </param>
        /// <returns>nje objekt clsMesazh qe tregon nese fshirja eshte kryer ne rregull apo jo</returns>
        internal clsMesazh fshiKokaListPagese(int idkoka)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDKOKA", idkoka, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_KOKALISTPAGESE_del");
            clsMesazh mesazh = new clsMesazh(true, mesazhFshirje);
            return mesazh;
        }

        internal clsMesazh kaloDokumentListPageseNeHistorik(int idKoka, int idModifikuesi, int idStatusDok)
        {
            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddInputParameters("@IDKOKA", idKoka);
            dbManager.AddInputParameters("@IDMODIFIKUESI", idModifikuesi);
            dbManager.AddInputParameters("@IDSTATUSDOK", idStatusDok);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_KOKALISTPAGESEHISTORIK_kaloDokumentNeHistorik");
            return new MesazhSuksesi();
        }
        /// <summary>
        /// kthen objekt koka dokumenti  sipas idse
        /// </summary>
        ///<param name="idkoka"> koka e dokumentit </param>
        ///<returns>nje objekt datarow qe permban nje koleksion me te gjitha kokat e dokumentit  me kete id  </returns>
        internal DataRow ktheKokaListPageseSipasID(int idkoka)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDKOKA", idkoka, ParameterDirection.Input);
            using (DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KOKALISTPAGESE_sel"))
            {
                if (ds == null)
                    return null;
                if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                    return null;
                return ds.Tables[0].Rows[0];
            }
        }

        /// <summary>
        /// kthen datarow koka dokumenti  sipas nivelit te dokumentit, nr te dokumentit dhe dates se dokumentit
        /// </summary>
        ///<param name="idNivel"> id e nivelit</param>
        ///<param name="nrdok"> nr i dokumentit te gjenerues</param>
        ///<param name="dtdok"> data e dokumentit</param>
        ///<returns>nje objekt datarow qe permban nje koleksion me te gjitha kokat e dokumentit  me kete nivel dokumenti, nr dokumenti dhe date dokumenti  </returns>
        internal DataRow ktheKokaListPageseSipasIdNivelNrDokDtDok(int idNivel, string nrdok, DateTime dtdok)
        {
            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@IDNNIVEL", idNivel, ParameterDirection.Input);
            dbManager.AddParameters(1, "@NRDOK", nrdok, ParameterDirection.Input);
            dbManager.AddParameters(2, "@DTDOK", dtdok, ParameterDirection.Input);

            using (DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KOKALISTPAGESE_selSipasIdNivelNrDokDtDok"))
            {
                if (ds == null)
                    return null;
                if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                    return null;
                return ds.Tables[0].Rows[0];
            }
        }

        /// <summary>
        /// kthen datatable koka dokumenti sipas ndermarje vitit 
        /// </summary>
        ///<param name="idNdermVit">id e ndermarje vitit</param>
        ///<returns>nje objekt datatable qe permban nje koleksion me te gjitha kokat e dokumentit   te kesaj ndermarje viti  </returns>
        internal DataTable ktheGjitheKokaListPagese(int idNdermVit)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDNDERVITi", idNdermVit, ParameterDirection.Input);
            using (DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KOKALISTPAGESE_selAllNderViti"))
            {
                return ds.Tables[0];
            }
        }

        /// <summary>
        /// perdoret per te kontrolluar nqs ekziston nje dokument  me nje nr dokumenti te marre si parameter
        /// behet kontrolli per qellime konsistence te informacionit ne DB, nuk lejon te celen dokumenta  te ndryshem me te njejtin kod ne nje ndermarje
        /// </summary>
        /// <param name="idNdermarrje"> id e ndermarjes</param>
        /// <param name="dtdok"> data e dokumentit </param>
        /// <param name="nrDok">nr i dokumentit</param>
        /// <returns>nje objeckt clsMesazh qe tregon nese ekziston apo jo nje dokument me kete kod</returns>
        internal bool ekzistonRegjistrimListPagese(string nrDok, DateTime dtdok, int idNdermarrje)
        {
            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@NRDOK", nrDok, ParameterDirection.Input);
            dbManager.AddParameters(1, "@DTDOK", dtdok, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDNDERMARJE", idNdermarrje, ParameterDirection.Input);
            using (DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KOKALISTPAGESE_ekzistonRegjistrimListPagese"))
            {
                if (ds.Tables[0].Rows.Count == 1)
                    return true;
                if (ds.Tables[0].Rows.Count == 0)
                    return false;
                return true;
            }
        }
        internal bool KaTeDhenaPerTeMarre(int idkoka)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDKOKA", idkoka, ParameterDirection.Input);
            using (DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KOKALISTPAGESE_kaTeDhenaPerTeMarre"))
            {
                if (ds.Tables[0].Rows.Count > 0)
                    return true;
                if (ds.Tables[0].Rows.Count == 0)
                    return false;
                return true;
            }
        }

        /// <summary>
        /// merr gjithe koka listpagesa dt sipas idnderviti dhe autorizimeve te perdoruesit
        /// </summary>
        /// <param name="idNdermVit">id nderm viti</param>
        /// <returns> data table me keto list pagesa</returns>
        internal DataTable merrKokaListPageseDT(int idNdermVit, int idperdoruesi)
        {//metoda per te marre te gjithe  kokat e fleteve kontabel te pa kontabilizuara

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDNDERVITI", idNdermVit, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUES", idperdoruesi, ParameterDirection.Input);
            using (DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KOKALISTPAGESE_merrKokaListPageseDT"))
            {
                return ds.Tables[0];
            }

        }

        internal int ktheIdStatusDokKokaListpag(int idkoka)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDKOKA", idkoka, ParameterDirection.Input);
            return Convert.ToInt32(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_KOKALISTPAGESE_merrIdStatusDok"));
        }
        #endregion

        /// <summary>
        /// Metodat e meposhtme i perkasin veprimeve qe kryhen nga objektet clsKategoriPage dhe colKategoriPage
        /// </summary>        
        #region KATEGORI PAGE

        /// <summary>
        /// kthen datarow kategorine sipas id sipas id-se
        /// </summary>
        ///<param name="idkategori"> id e kategorise</param>
        ///<returns> nje datarow qe permban kategorine sipas id-se </returns>
        internal DataRow merrKagetoriPage(int idkategori)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDKATEGORIPAGE", idkategori, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KATEGORIPAGE_ktheKategoriSipasID");

            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];
        }

        /// <summary>
        /// kthen datarow kategorine e pages sipas kodit dhe ndermarrjes dhe tipit
        /// </summary>
        ///<param name="kodi"> kodi </param>
        ///<param name="idndermarje"> id e ndermarrjes</param>
        ///<returns> nje datarow qe permban kategorine e pages sipas kodit dhe ndermarrjes dhe tipit</returns>
        internal DataRow merrKagetoriPageSipasKodit(string kodi, int idndermarje, int tipi)
        {
            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@KODI", kodi, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idndermarje, ParameterDirection.Input);
            dbManager.AddParameters(2, "@TIPI", tipi, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KATEGORIPAGE_ktheKategoriSipasKodit");

            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];
        }


        /// <summary>
        /// ekzekuton prc_T_KATEGORIPAGE_ins ne DB dhe kthen statusin e perfundimit te ekzekutimit te SP-se perkatese
        /// Shiko clsMesazh brenda ketij moduli per informacion te metejshem mbi vleren e kthimit te kesaj metode
        /// <param name="idkategori"> id ritese e kategorise</param>
        /// <param name="kodi">kodi</param>
        /// <param name="pershkrimi">pershkrimi </param>
        /// <param name="tipi">tipi</param>
        /// <param name="paga"> paga</param>
        /// <param name="idPerdoruesi">id e perdoruesit qe ka kryer veprimin</param>
        /// <param name="idNdermarje">id ndermarje</param>
        /// <param name="idstatusdok">id e statusit te kodifikimit</param>
        /// <returns> nje objekt clsMesazh qe tregon nese ruajtja eshte kryer ne rregull apo jo.</returns>
        /// </summary>

        internal clsMesazh ruajKategoriPage(out int idkategori, string kodi, String pershkrimi, int tipi, decimal paga, int idPerdoruesi, int idNdermarje, int idstatusdok)
        {
            idkategori = -1;

            dbManager.Open();
            clsMesazh mesazh = new clsMesazh();
            //shtimi i parametrave

            dbManager.CreateParameters(8);
            dbManager.AddParameters(0, "@IDKATEGORIPAGE", idkategori, ParameterDirection.Output);
            dbManager.AddParameters(1, "@KODI", kodi, ParameterDirection.Input);
            dbManager.AddParameters(2, "@PERSHKRIMI", pershkrimi, ParameterDirection.Input);
            dbManager.AddParameters(3, "@TIPI", tipi, ParameterDirection.Input);
            dbManager.AddParameters(4, "@PAGA", paga, ParameterDirection.Input);
            dbManager.AddParameters(5, "@IDPERDORUESI", idPerdoruesi, ParameterDirection.Input);
            dbManager.AddParameters(6, "@IDNDERMARJE", idNdermarje, ParameterDirection.Input);
            dbManager.AddParameters(7, "@IDSTATUSDOK", idstatusdok, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_KATEGORIPAGE_ins");
            mesazh = new clsMesazh(true, mesazhRuajtje);

            return mesazh;
        }

        /// <summary>
        /// ekzekuton prc_T_KATEGORIPAGE_upd ne DB dhe kthen statusin e perfundimit te ekzekutimit te SP-se perkatese
        /// Shiko clsMesazh brenda ketij moduli per informacion te metejshem mbi vleren e kthimit te kesaj metode
        /// <param name="idkategori"> id ritese e kategorise</param>
        /// <param name="kodi">kodi</param>
        /// <param name="pershkrimi">pershkrimi </param>
        /// <param name="tipi">tipi</param>
        /// <param name="paga"> paga</param>
        /// <param name="idPerdoruesi">id e perdoruesit qe ka kryer veprimin</param>
        /// <param name="idNdermarje">id ndermarje</param>
        /// <param name="idstatusdok">id e statusit te kodifikimit</param>
        /// <returns> nje objekt clsMesazh qe tregon nese modifikimi eshte kryer ne rregull apo jo.</returns>
        /// </summary>
        internal clsMesazh modifikoKategoriPage(int idkategori, string kodi, String pershkrimi, int tipi, decimal paga, int idPerdoruesi, int idNdermarje, int idstatusdok)
        {
            dbManager.Open();
            clsMesazh mesazh = new clsMesazh();
            //shtimi i parametrave

            dbManager.CreateParameters(8);
            dbManager.AddParameters(0, "@IDKATEGORIPAGE", idkategori, ParameterDirection.Input);
            dbManager.AddParameters(1, "@KODI", kodi, ParameterDirection.Input);
            dbManager.AddParameters(2, "@PERSHKRIMI", pershkrimi, ParameterDirection.Input);
            dbManager.AddParameters(3, "@TIPI", tipi, ParameterDirection.Input);
            dbManager.AddParameters(4, "@PAGA", paga, ParameterDirection.Input);
            dbManager.AddParameters(5, "@IDPERDORUESI", idPerdoruesi, ParameterDirection.Input);
            dbManager.AddParameters(6, "@IDNDERMARJE", idNdermarje, ParameterDirection.Input);
            dbManager.AddParameters(7, "@IDSTATUSDOK", idstatusdok, ParameterDirection.Input);

            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_KATEGORIPAGE_upd");
            mesazh = new clsMesazh(true, mesazhModifikimi);

            return mesazh;
        }

        /// <summary>
        /// ekzekuton prc_T_KATEGORIPAGE_upddel ne DB dhe kthen statusin e perfundimit te ekzekutimit te SP-se perkatese
        /// Shiko clsMesazh brenda ketij moduli per informacion te metejshem mbi vleren e kthimit te kesaj metode
        /// <param name="idkategori"> id ritese e kategorise se pages</param>
        /// <param name="idPerdoruesi">id e perdoruesit qe ka kryer veprimin</param>
        /// <returns> nje objekt clsMesazh qe tregon nese fshirja eshte kryer ne rregull apo jo.</returns>
        /// </summary>

        internal clsMesazh fshiKategori(int idkategori, int idperdoruesi)
        {
            dbManager.Open();
            //shtimi i parametrave
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDKATEGORIPAGE", idkategori, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUESI", idperdoruesi, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_KATEGORIPAGE_upddel");
            clsMesazh mesazh = new clsMesazh(true, mesazhFshirje);
            return mesazh;
        }



        /// <summary>
        /// kthen Datatable kategorite e pages sipas ndermarjes dhe tipit
        /// </summary>
        ///<param name="idndermarje">id e ndermarjes</param>
        ///<returns>nje datatable qe permban nje koleksion me te gjitha kategorite e pages sipas ndermarjes dhe tipit </returns>

        internal DataTable ktheGjitheKategoriPageSipasNdermarrjesDheTipit(int idndermarje, int tipi)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDNDERMARJE", idndermarje, ParameterDirection.Input);
            dbManager.AddParameters(1, "@Tipi", tipi, ParameterDirection.Input);

            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KATEGORIPAGE_ktheKategoriSipasTipit");

            return ds.Tables[0];
        }


        public bool ekzistonKategoriPage(String kod, int idndermarje, int tipi)
        {//kontrollon nqs ekziston nje kodifikim artikulli me kete kod

            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@KODI", kod, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idndermarje, ParameterDirection.Input);
            dbManager.AddParameters(2, "@TIPI", tipi, ParameterDirection.Input);
            int pergjigje = Convert.ToInt32(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_KATEGORIPAGE_ekzistonKategoriSipasTipit"));
            return Convert.ToBoolean(pergjigje);
        }

        public bool ekzistonKategoriPage(String kod, int idndermarje)
        {//kontrollon nqs ekziston nje kodifikim artikulli me kete kod
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@KODI", kod, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idndermarje, ParameterDirection.Input);
            int pergjigje = Convert.ToInt32(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_KATEGORIPAGE_ekzistonKategoriSipasKodit"));
            return Convert.ToBoolean(pergjigje);
        }

        public bool kaVeprimeKategoriPage(int id, int idndermarje)
        {//kontrollon nqs ka veprime me kete kodifikim artikulli

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDKATEGORIPAGE", id, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idndermarje, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KATEGORIPAGE_kaVeprime");
            if (ds.Tables[0].Rows.Count == 1)
                return true;
            if (ds.Tables[0].Rows.Count == 0)
                return false;
            return true;
        }
        #endregion

        /// <summary>
        /// Metodat e meposhtme i perkasin veprimeve qe kryhen nga objektet clsShtesaPage dhe colShtesaPage
        /// </summary>        
        #region SHTESA PAGE

        /// <summary>
        /// kthen datarow shtesa sipas id sipas id-se
        /// </summary>
        ///<param name="idshtesa"> id e shtesase</param>
        ///<returns> nje datarow qe permban shtesane sipas id-se </returns>
        internal DataRow merrShtesaPage(int idshtesa)
        {

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDSHTESAPAGE", idshtesa, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_SHTESAPAGE_ktheShteseSipasID");

            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];

        }

        /// <summary>
        /// kthen datarow shtesane e pages sipas kodit dhe ndermarrjes dhe tipit
        /// </summary>
        ///<param name="kodi"> kodi </param>
        ///<param name="idndermarje"> id e ndermarrjes</param>
        ///<returns> nje datarow qe permban shtesane e pages sipas kodit dhe ndermarrjes dhe tipit</returns>
        internal DataRow merrShtesaPageSipasKodit(string kodi, int idndermarje, int tipi)
        {
            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@KODI", kodi, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idndermarje, ParameterDirection.Input);
            dbManager.AddParameters(2, "@TIPI", tipi, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_SHTESAPAGE_ktheShteseSipasKodit");

            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];
        }


        /// <summary>
        /// ekzekuton prc_T_SHTESAPAGE_ins ne DB dhe kthen statusin e perfundimit te ekzekutimit te SP-se perkatese
        /// Shiko clsMesazh brenda ketij moduli per informacion te metejshem mbi vleren e kthimit te kesaj metode
        /// <param name="idshtesa"> id ritese e shtesase</param>
        /// <param name="kodi">kodi</param>
        /// <param name="klasa">klasa </param>
        /// <param name="tipi">tipi</param>
        /// <param name="vlera"> vlera</param>
        /// <param name="idPerdoruesi">id e perdoruesit qe ka kryer veprimin</param>
        /// <param name="idNdermarje">id ndermarje</param>
        /// <param name="idstatusdok">id e statusit te kodifikimit</param>
        /// <returns> nje objekt clsMesazh qe tregon nese ruajtja eshte kryer ne rregull apo jo.</returns>
        /// </summary>

        internal clsMesazh ruajShtesaPage(out int idshtesa, string kodi, String klasa, int tipi, decimal vlera, int idPerdoruesi, int idNdermarje, int idstatusdok)
        {
            idshtesa = -1;
            dbManager.Open();
            clsMesazh mesazh = new clsMesazh();
            //shtimi i parametrave

            dbManager.CreateParameters(8);
            dbManager.AddParameters(0, "@IDSHTESAPAGE", idshtesa, ParameterDirection.Output);
            dbManager.AddParameters(1, "@KODI", kodi, ParameterDirection.Input);
            dbManager.AddParameters(2, "@KLASA", klasa, ParameterDirection.Input);
            dbManager.AddParameters(3, "@TIPI", tipi, ParameterDirection.Input);
            dbManager.AddParameters(4, "@VLERA", vlera, ParameterDirection.Input);
            dbManager.AddParameters(5, "@IDPERDORUESI", idPerdoruesi, ParameterDirection.Input);
            dbManager.AddParameters(6, "@IDNDERMARJE", idNdermarje, ParameterDirection.Input);
            dbManager.AddParameters(7, "@IDSTATUSDOK", idstatusdok, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_SHTESAPAGE_ins");
            mesazh = new clsMesazh(true, mesazhRuajtje);

            return mesazh;
        }

        /// <summary>
        /// ekzekuton prc_T_SHTESAPAGE_upd ne DB dhe kthen statusin e perfundimit te ekzekutimit te SP-se perkatese
        /// Shiko clsMesazh brenda ketij moduli per informacion te metejshem mbi vleren e kthimit te kesaj metode
        /// <param name="idshtesa"> id ritese e shtesase</param>
        /// <param name="kodi">kodi</param>
        /// <param name="klasa">klasa </param>
        /// <param name="tipi">tipi</param>
        /// <param name="vlera"> vlera</param>
        /// <param name="idPerdoruesi">id e perdoruesit qe ka kryer veprimin</param>
        /// <param name="idNdermarje">id ndermarje</param>
        /// <param name="idstatusdok">id e statusit te kodifikimit</param>
        /// <returns> nje objekt clsMesazh qe tregon nese modifikimi eshte kryer ne rregull apo jo.</returns>
        /// </summary>
        internal clsMesazh modifikoShtesaPage(int idshtesa, string kodi, String klasa, int tipi, decimal vlera, int idPerdoruesi, int idNdermarje, int idstatusdok)
        {
            dbManager.Open();
            clsMesazh mesazh = new clsMesazh();
            //shtimi i parametrave

            dbManager.CreateParameters(8);
            dbManager.AddParameters(0, "@IDSHTESAPAGE", idshtesa, ParameterDirection.Input);
            dbManager.AddParameters(1, "@KODI", kodi, ParameterDirection.Input);
            dbManager.AddParameters(2, "@KLASA", klasa, ParameterDirection.Input);
            dbManager.AddParameters(3, "@TIPI", tipi, ParameterDirection.Input);
            dbManager.AddParameters(4, "@VLERA", vlera, ParameterDirection.Input);
            dbManager.AddParameters(5, "@IDPERDORUESI", idPerdoruesi, ParameterDirection.Input);
            dbManager.AddParameters(6, "@IDNDERMARJE", idNdermarje, ParameterDirection.Input);
            dbManager.AddParameters(7, "@IDSTATUSDOK", idstatusdok, ParameterDirection.Input);

            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_SHTESAPAGE_upd");
            mesazh = new clsMesazh(true, mesazhModifikimi);

            return mesazh;
        }

        /// <summary>
        /// ekzekuton prc_T_SHTESAPAGE_upddel ne DB dhe kthen statusin e perfundimit te ekzekutimit te SP-se perkatese
        /// Shiko clsMesazh brenda ketij moduli per informacion te metejshem mbi vleren e kthimit te kesaj metode
        /// <param name="idshtesa"> id ritese e shtesase se pages</param>
        /// <param name="idPerdoruesi">id e perdoruesit qe ka kryer veprimin</param>
        /// <returns> nje objekt clsMesazh qe tregon nese fshirja eshte kryer ne rregull apo jo.</returns>
        /// </summary>

        internal clsMesazh fshiShtesa(int idshtesa, int idperdoruesi)
        {
            dbManager.Open();
            //shtimi i parametrave
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDSHTESAPAGE", idshtesa, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUESI", idperdoruesi, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_SHTESAPAGE_upddel");
            clsMesazh mesazh = new clsMesazh(true, mesazhFshirje);
            return mesazh;
        }



        /// <summary>
        /// kthen Datatable shtesate e pages sipas ndermarjes dhe tipit
        /// </summary>
        ///<param name="idndermarje">id e ndermarjes</param>
        ///<returns>nje datatable qe permban nje koleksion me te gjitha shtesate e pages sipas ndermarjes dhe tipit </returns>

        internal DataTable ktheGjitheShtesaPageSipasNdermarrjesDheTipit(int idndermarje, int tipi)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDNDERMARJE", idndermarje, ParameterDirection.Input);
            dbManager.AddParameters(1, "@Tipi", tipi, ParameterDirection.Input);

            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_SHTESAPAGE_ktheShteseSipasTipit");

            return ds.Tables[0];
        }


        public bool ekzistonShtesaPage(String kod, int idndermarje, int tipi)
        {//kontrollon nqs ekziston nje kodifikim artikulli me kete kod

            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@KODI", kod, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idndermarje, ParameterDirection.Input);
            dbManager.AddParameters(2, "@TIPI", tipi, ParameterDirection.Input);
            int pergjigje = Convert.ToInt32(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_SHTESAPAGE_ekzistonShteseSipasTipit"));
            return Convert.ToBoolean(pergjigje);
        }

        public bool kaVeprimeShtesaPage(int id, int tipi, int idndermarje)
        {//kontrollon nqs ka veprime me kete kodifikim artikulli

            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@IDSHTESAPAGE", id, ParameterDirection.Input);
            dbManager.AddParameters(1, "@Tipi", tipi, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDNDERMARJE", idndermarje, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_SHTESAPAGE_kaVeprime");
            if (ds.Tables[0].Rows.Count == 1)
                return true;
            if (ds.Tables[0].Rows.Count == 0)
                return false;
            return true;
        }
        #endregion

        /// <summary>
        /// Metodat e meposhtme i perkasin veprimeve qe kryhen nga objektet clsSigurimeSuplementare dhe colSigurimeSuplementare
        /// </summary>        
        #region SIGURIME SUPLEMENTARE

        /// <summary>
        /// kthen datarow sigurime suplementare sipas id sipas id-se
        /// </summary>
        ///<param name="idsigurimi"> id e sigurimi</param>
        ///<returns> nje datarow qe permban sigurimin sipas id-se </returns>
        internal DataRow merrSigurimeSuplementare(int idsigurimi)
        {

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDSIGSUPLEMENTARE", idsigurimi, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_SIGURIMESUPLEMENTARE_ktheSigurimeSipasID");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];
        }

        /// <summary>
        /// kthen datarow sigurime sipas kodit dhe ndermarrjes
        /// </summary>
        ///<param name="kodi"> kodi </param>
        ///<param name="idndermarje"> id e ndermarrjes</param>
        ///<returns> nje datarow qe permban sigurimet sipas kodit dhe ndermarrjes </returns>
        internal DataRow merrSigurimeSuplementareSipasKodit(string kodi, int idndermarje)
        {

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@KODI", kodi, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idndermarje, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_SIGURIMESUPLEMENTARE_ktheSigurimeSipasKodit");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];

        }


        /// <summary>
        /// ekzekuton prc_T_SIGURIMESUPLEMENTARE_ins ne DB dhe kthen statusin e perfundimit te ekzekutimit te SP-se perkatese
        /// Shiko clsMesazh brenda ketij moduli per informacion te metejshem mbi vleren e kthimit te kesaj metode
        /// <param name="idsigurim"> id ritese e sigurimeve</param>
        /// <param name="kodi">kodi</param>
        /// <param name="grupi">grupi </param>
        /// <param name="dtaktivizimi">dtaktivizimi</param>
        /// <param name="perqindja"> perqindja</param>
        /// <param name="idPerdoruesi">id e perdoruesit qe ka kryer veprimin</param>
        /// <param name="idNdermarje">id ndermarje</param>
        /// <param name="idstatusdok">id e statusit te kodifikimit</param>
        /// <returns> nje objekt clsMesazh qe tregon nese ruajtja eshte kryer ne rregull apo jo.</returns>
        /// </summary>

        internal clsMesazh ruajSigurimeSuplementare(out int idsigurim, string kodi, String grupi, DateTime dtaktivizimi, decimal perqindja, int idPerdoruesi, int idNdermarje, int idstatusdok, string germa)
        {
            idsigurim = -1;

            dbManager.Open();
            clsMesazh mesazh = new clsMesazh();
            //shtimi i parametrave
            dbManager.CreateParameters(9);
            dbManager.AddParameters(0, "@IDSIGSUPLEMENTARE", idsigurim, ParameterDirection.Output);
            dbManager.AddParameters(1, "@KODI", kodi, ParameterDirection.Input);
            dbManager.AddParameters(2, "@GRUPI", grupi, ParameterDirection.Input);
            dbManager.AddParameters(3, "@DTAKTIVIZIMI", dtaktivizimi, ParameterDirection.Input);
            dbManager.AddParameters(4, "@PERQINDJA", perqindja, ParameterDirection.Input);
            dbManager.AddParameters(5, "@IDPERDORUESI", idPerdoruesi, ParameterDirection.Input);
            dbManager.AddParameters(6, "@IDNDERMARJE", idNdermarje, ParameterDirection.Input);
            dbManager.AddParameters(7, "@IDSTATUSDOK", idstatusdok, ParameterDirection.Input);
            dbManager.AddParameters(8, "@GERMA", germa, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_SIGURIMESUPLEMENTARE_ins");
            mesazh = new clsMesazh(true, mesazhRuajtje);
            return mesazh;

        }

        /// <summary>
        /// ekzekuton prc_T_SIGURIMESUPLEMENTARE_upd ne DB dhe kthen statusin e perfundimit te ekzekutimit te SP-se perkatese
        /// Shiko clsMesazh brenda ketij moduli per informacion te metejshem mbi vleren e kthimit te kesaj metode
        /// <param name="idsigurim"> id ritese e sigurimeve</param>
        /// <param name="kodi">kodi</param>
        /// <param name="grupi">grupi </param>
        /// <param name="dtaktivizimi">dtaktivizimi</param>
        /// <param name="perqindja"> perqindja</param>
        /// <param name="idPerdoruesi">id e perdoruesit qe ka kryer veprimin</param>
        /// <param name="idNdermarje">id ndermarje</param>
        /// <param name="idstatusdok">id e statusit te kodifikimit</param>
        /// <returns> nje objekt clsMesazh qe tregon nese modifikimi eshte kryer ne rregull apo jo.</returns>
        /// </summary>
        internal clsMesazh modifikoSigurimeSuplementare(int idsigurim, string kodi, String grupi, DateTime dtaktivizimi, decimal perqindja, int idPerdoruesi, int idNdermarje, int idstatusdok, string germa)
        {

            dbManager.Open();
            clsMesazh mesazh = new clsMesazh();
            //shtimi i parametrave
            dbManager.CreateParameters(9);
            dbManager.AddParameters(0, "@IDSIGSUPLEMENTARE", idsigurim, ParameterDirection.Input);
            dbManager.AddParameters(1, "@KODI", kodi, ParameterDirection.Input);
            dbManager.AddParameters(2, "@GRUPI", grupi, ParameterDirection.Input);
            dbManager.AddParameters(3, "@DTAKTIVIZIMI", dtaktivizimi, ParameterDirection.Input);
            dbManager.AddParameters(4, "@PERQINDJA", perqindja, ParameterDirection.Input);
            dbManager.AddParameters(5, "@IDPERDORUESI", idPerdoruesi, ParameterDirection.Input);
            dbManager.AddParameters(6, "@IDNDERMARJE", idNdermarje, ParameterDirection.Input);
            dbManager.AddParameters(7, "@IDSTATUSDOK", idstatusdok, ParameterDirection.Input);
            dbManager.AddParameters(8, "@GERMA", germa, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_SIGURIMESUPLEMENTARE_upd");
            mesazh = new clsMesazh(true, mesazhModifikimi);
            return mesazh;

        }

        /// <summary>
        /// ekzekuton prc_T_SIGURIMESUPLEMENTARE_upddel ne DB dhe kthen statusin e perfundimit te ekzekutimit te SP-se perkatese
        /// Shiko clsMesazh brenda ketij moduli per informacion te metejshem mbi vleren e kthimit te kesaj metode
        /// <param name="idsigurim"> id ritese e sigurimit</param>
        /// <param name="idPerdoruesi">id e perdoruesit qe ka kryer veprimin</param>
        /// <returns> nje objekt clsMesazh qe tregon nese fshirja eshte kryer ne rregull apo jo.</returns>
        /// </summary>

        internal clsMesazh fshiSigurimeSuplementare(int idsigurim, int idperdoruesi)
        {

            dbManager.Open();
            //shtimi i parametrave
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDSIGSUPLEMENTARE", idsigurim, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUESI", idperdoruesi, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_SIGURIMESUPLEMENTARE_upddel");
            clsMesazh mesazh = new clsMesazh(true, mesazhFshirje);
            return mesazh;

        }



        /// <summary>
        /// kthen Datatable sigurimit sipas ndermarjes 
        /// </summary>
        ///<param name="idndermarje">id e ndermarjes</param>
        ///<returns>nje datatable qe permban nje koleksion me te gjitha sigurimet sipas ndermarjes </returns>

        internal DataTable ktheGjitheSigurimeSuplementareSipasNdermarrjes(int idndermarje)
        {

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDNDERMARJE", idndermarje, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_SIGURIMESUPLEMENTARE_ktheSigurimeSipasNdermarjes");
            return ds.Tables[0];

        }
        /// <summary>
        /// kthen Datatable sigurimit sipas ndermarjes 
        /// </summary>
        ///<param name="idndermarje">id e ndermarjes</param>
        ///<returns>nje datatable qe permban nje koleksion me te gjitha sigurimet sipas ndermarjes </returns>

        internal DataTable ktheGjitheSigurimeSuplementareSipasNdermarrjesDheDates(int idndermarje, DateTime dtakt)
        {

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDNDERMARJE", idndermarje, ParameterDirection.Input);
            dbManager.AddParameters(1, "@DTAKTIVIZIMI", dtakt, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_SIGURIMESUPLEMENTARE_ktheSigurimSipasNdermarjesDheDates");
            return ds.Tables[0];

        }

        public bool ekzistonSigurimeSuplementare(String kod, int idndermarje)
        {//kontrollon nqs ekziston nje kodifikim artikulli me kete kod

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@KODI", kod, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idndermarje, ParameterDirection.Input);
            int pergjigje = Convert.ToInt32(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_SIGURIMESUPLEMENTARE_ekzistonSigurimi"));
            return Convert.ToBoolean(pergjigje);

        }

        public bool kaVeprimeSigurimeSuplementare(int id, int idndermarje)
        {//kontrollon nqs ka veprime me kete kodifikim artikulli

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDSIGSUPLEMENTARE", id, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idndermarje, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_SIGURIMESUPLEMENTARE_kaVeprime");
            if (ds.Tables[0].Rows.Count == 1)
                return true;
            if (ds.Tables[0].Rows.Count == 0)
                return false;
            return true;
        }
        #endregion

        /// <summary>
        /// Metodat e meposhtme i perkasin veprimeve qe kryhen nga objektet clsKokaKonfigListOrari dhe colKokaKonfigListOrari
        /// </summary>        
        #region KOKAKONFIGLISTORARI

        /// <summary>
        /// kthen datarow konfigurimin sipas id-se
        /// </summary>
        ///<param name="idkoka"> id e kokes</param>
        ///<returns> nje datarow qe permban konfigurimin list orari me kete id</returns>
        internal DataRow merrKonfigurimListOrari(int idkoka)
        {

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDKOKA", idkoka, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KOKAKONFIGLISTORARI_ktheSipasId");

            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];

        }




        /// <summary>
        /// ekzekuton prc_T_KOKAKONFIGLISTORARI_ins ne DB dhe kthen statusin e perfundimit te ekzekutimit te SP-se perkatese
        /// Shiko clsMesazh brenda ketij moduli per informacion te metejshem mbi vleren e kthimit te kesaj metode
        /// <param name="idkoka"> id ritese </param>
        /// <param name="orefillimi">ore fillimi </param>
        /// <param name="shenime">pershkrimi </param>
        /// <param name="koeficienti"> koeficienti</param>
        /// <param name="idPerdoruesi">id e perdoruesit qe ka kryer veprimin</param>
        /// <param name="idNdermarje">id ndermarje</param>
        /// <param name="idstatusdok">id e statusit </param>
        /// <returns> nje objekt clsMesazh qe tregon nese ruajtja eshte kryer ne rregull apo jo.</returns>
        /// </summary>

        internal clsMesazh ruajKonfigurimListOrari(out int idkoka, string orefillimi, string orembarimi, String shenime, decimal koeficienti, int idPerdoruesi, int idkrijuesi, int idNdermarje, int idstatusdok)
        {
            idkoka = -1;

            dbManager.Open();
            clsMesazh mesazh = new clsMesazh();
            //shtimi i parametrave

            dbManager.CreateParameters(9);
            dbManager.AddParameters(0, "@IDKOKA", idkoka, ParameterDirection.Output);
            dbManager.AddParameters(1, "@OREFILLIMI", orefillimi, ParameterDirection.Input);
            dbManager.AddParameters(2, "@OREMBARIMI", orembarimi, ParameterDirection.Input);
            dbManager.AddParameters(3, "@SHENIME", shenime, ParameterDirection.Input);
            dbManager.AddParameters(4, "@KOEFICIENTI", koeficienti, ParameterDirection.Input);
            dbManager.AddParameters(5, "@IDPERDORUESI", idPerdoruesi, ParameterDirection.Input);
            dbManager.AddParameters(6, "@IDKRIJUESI", idkrijuesi, ParameterDirection.Input);
            dbManager.AddParameters(7, "@IDNDERMARJE", idNdermarje, ParameterDirection.Input);
            dbManager.AddParameters(8, "@IDSTATUSDOK", idstatusdok, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_KOKAKONFIGLISTORARI_ins");
            idkoka = int.Parse(dbManager.Parameters[0].Value.ToString());
            mesazh = new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);

            return mesazh;

        }

        /// <summary>
        /// ekzekuton prc_T_KOKAKONFIGLISTORARI_upd ne DB dhe kthen statusin e perfundimit te ekzekutimit te SP-se perkatese
        /// Shiko clsMesazh brenda ketij moduli per informacion te metejshem mbi vleren e kthimit te kesaj metode
        /// <param name="idkoka"> id ritese </param>
        /// <param name="orefillimi">ora e fillimit </param>
        /// <param name="shenime">pershkrimi </param>
        /// <param name="koeficienti"> koeficienti</param>
        /// <param name="idPerdoruesi">id e perdoruesit qe ka kryer veprimin</param>
        /// <param name="idNdermarje">id ndermarje</param>
        /// <param name="idstatusdok">id e statusit </param>
        /// <returns> nje objekt clsMesazh qe tregon nese modifikimi eshte kryer ne rregull apo jo.</returns>
        /// </summary>
        internal clsMesazh modifikoKonfigurimListOrari(int idkoka, string orefillimi, string orembarimi, String shenime, decimal koeficienti, int idPerdoruesi, int idNdermarje, int idstatusdok)
        {

            dbManager.Open();
            clsMesazh mesazh = new clsMesazh();
            //shtimi i parametrave


            dbManager.CreateParameters(8);
            dbManager.AddParameters(0, "@IDKOKA", idkoka, ParameterDirection.Input);
            dbManager.AddParameters(1, "@OREFILLIMI", orefillimi, ParameterDirection.Input);
            dbManager.AddParameters(2, "@OREMBARIMI", orembarimi, ParameterDirection.Input);
            dbManager.AddParameters(3, "@SHENIME", shenime, ParameterDirection.Input);
            dbManager.AddParameters(4, "@KOEFICIENTI", koeficienti, ParameterDirection.Input);
            dbManager.AddParameters(5, "@IDPERDORUESI", idPerdoruesi, ParameterDirection.Input);
            dbManager.AddParameters(6, "@IDNDERMARJE", idNdermarje, ParameterDirection.Input);
            dbManager.AddParameters(7, "@IDSTATUSDOK", idstatusdok, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_KOKAKONFIGLISTORARI_upd");
            mesazh = new clsMesazh(true, MessagesResource.Messages["msgModifikimiMeSukses"]);

            return mesazh;

        }

        /// <summary>
        /// ekzekuton prc_T_KOKAKONFIGLISTORARI_upddel ne DB dhe kthen statusin e perfundimit te ekzekutimit te SP-se perkatese
        /// Shiko clsMesazh brenda ketij moduli per informacion te metejshem mbi vleren e kthimit te kesaj metode
        /// <param name="idkoka"> id ritese </param>
        /// <param name="idPerdoruesi">id e perdoruesit qe ka kryer veprimin</param>
        /// <returns> nje objekt clsMesazh qe tregon nese fshirja eshte kryer ne rregull apo jo.</returns>
        /// </summary>

        internal clsMesazh fshiKonfigurimListOrari(int idkoka, int idperdoruesi)
        {

            dbManager.Open();
            //shtimi i parametrave
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDKOKA", idkoka, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUESI", idperdoruesi, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_KOKAKONFIGLISTORARI_upddel");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
            return mesazh;

        }



        /// <summary>
        /// kthen Datatable konfigurim list orari sipas id ndermarjes
        /// </summary>
        ///<param name="idndermarje">id e ndermarjes</param>
        ///<returns>nje datatable qe permban nje koleksion me te gjitha konfigurimet e list orarit  te kesaj ndermarje </returns>

        internal DataTable ktheGjitheKonfigurimeListOrariSipasNdermarrjes(int idndermarje, int idgjuha)
        {

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDNDERMARJE", idndermarje, ParameterDirection.Input);
            dbManager.AddParameters(1, "@idgjuha", idgjuha, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KOKAKONFIGLISTORARI_merrSipasNdermarrjes");

            return ds.Tables[0];

        }



        #endregion

        /// <summary>
        /// Metodat e meposhtme i perkasin veprimeve qe kryhen nga objektet clsTrupiKonfigListOrari dhe colTrupiKonfigListOrari
        /// </summary>
        #region TRUPIKONFIGLISTORARI

        /// <summary>
        /// ekzekuton prc_T_TRUPIKONFIGLISTORARI_ins ne DB dhe kthen statusin e perfundimit te ekzekutimit te SP-se perkatese
        /// Shiko clsMesazh brenda ketij moduli per informacion te metejshem mbi vleren e kthimit te kesaj metode
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="idkoka">idkoka</param>
        /// <param name="ditejave">ditejave</param>
        /// <returns> nje objekt clsMesazh qe tregon nese ruajtja eshte kryer ne rregull apo jo.</returns>

        internal clsMesazh ruajTrupiKonfigurimListOrari(int id, int idkoka, string ditejave, string ditejave_eng)
        {

            dbManager.Open();
            dbManager.CreateParameters(4);
            dbManager.AddParameters(0, "@IDTRUPI", id, ParameterDirection.Output);
            dbManager.AddParameters(1, "@IDKOKA", idkoka, ParameterDirection.Input);
            dbManager.AddParameters(2, "@DITEJAVE", ditejave, ParameterDirection.Input);
            dbManager.AddParameters(3, "@DITEJAVE_ENG", ditejave_eng, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_TRUPIKONFIGLISTORARI_ins");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
            return mesazh;

        }


        /// <summary>
        /// ekzekutohet sp-ja prc_T_TRUPIKONFIGLISTORARI_del duke i kaluar id e kokes 
        /// </summary>
        /// <param name="idkoka"> </param>
        /// <returns>nje objekt clsMesazh qe tregon nese fshirja eshte kryer ne rregull apo jo</returns>
        internal clsMesazh fshiTrupiKonfigListOrariSipasIdKoka(int idkoka)
        {

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDKOKA", idkoka, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_TRUPIKONFIGLISTORARI_del");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
            return mesazh;

        }


        /// <summary>
        /// kthen trupin e kokes
        /// </summary>
        /// <param name="idkoka">idkoka</param>
        ///<returns>nje objekt trupin e kokes</returns>
        internal DataTable ktheTrupiKonfigListOrariSipasIdKoka(int idkoka)
        {

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDKOKA", idkoka, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_TRUPIKONFIGLISTORARI_selSipasIdKoka");
            return ds.Tables[0];

        }

        internal DataTable ktheTrupiKonfigListOrariSipasDites(int idndermarje, string dita)
        {

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@idndermarje", idndermarje, ParameterDirection.Input);
            dbManager.AddParameters(1, "@dita", dita, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_TRUPIKONFIGLISTORARI_selSipasDites");
            return ds.Tables[0];

        }
        #endregion

        /// <summary>
        /// Metodat e meposhtme i perkasin veprimeve qe kryhen nga objektet clsKalendariFestave dhe colKalendariFestave
        /// </summary>        
        #region KALENDARIFESTAVE

        /// <summary>
        /// kthen datarow kalendarin  sipas id-se
        /// </summary>
        ///<param name="id"> id </param>
        ///<returns> nje datarow qe permban konfigurimin list orari me kete id</returns>
        internal DataRow merrKalendarFestash(int id)
        {

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@ID", id, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KALENDARIFESTAVE_ktheSipasId");

            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];

        }




        /// <summary>
        /// ekzekuton prc_T_KALENDARIFESTAVE_ins ne DB dhe kthen statusin e perfundimit te ekzekutimit te SP-se perkatese
        /// Shiko clsMesazh brenda ketij moduli per informacion te metejshem mbi vleren e kthimit te kesaj metode
        /// <param name="id"> id ritese </param>
        /// <param name="data">ore fillimi </param>
        /// <param name="shenime">pershkrimi </param>
        /// <param name="koeficienti"> koeficienti</param>
        /// <param name="idPerdoruesi">id e perdoruesit qe ka kryer veprimin</param>
        /// <param name="idNdermarje">id ndermarje</param>
        /// <param name="idstatusdok">id e statusit </param>
        /// <returns> nje objekt clsMesazh qe tregon nese ruajtja eshte kryer ne rregull apo jo.</returns>
        /// </summary>

        internal clsMesazh ruajKalendarFestash(out int id, DateTime data, String shenime, decimal koeficienti, int idPerdoruesi, int idkrijuesi, int idNdermarje, int idstatusdok)
        {
            id = -1;

            dbManager.Open();
            clsMesazh mesazh = new clsMesazh();
            //shtimi i parametrave

            dbManager.CreateParameters(8);
            dbManager.AddParameters(0, "@ID", id, ParameterDirection.Output);
            dbManager.AddParameters(1, "@DATA", data, ParameterDirection.Input);
            dbManager.AddParameters(2, "@PERSHKRIMI", shenime, ParameterDirection.Input);
            dbManager.AddParameters(3, "@KOEFICIENTI", koeficienti, ParameterDirection.Input);
            dbManager.AddParameters(4, "@IDPERDORUESI", idPerdoruesi, ParameterDirection.Input);
            dbManager.AddParameters(5, "@IDKRIJUESI", idkrijuesi, ParameterDirection.Input);
            dbManager.AddParameters(6, "@IDNDERMARJE", idNdermarje, ParameterDirection.Input);
            dbManager.AddParameters(7, "@IDSTATUSDOK", idstatusdok, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_KALENDARIFESTAVE_ins");
            id = int.Parse(dbManager.Parameters[0].Value.ToString());
            mesazh = new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);

            return mesazh;

        }

        /// <summary>
        /// ekzekuton prc_T_KALENDARIFESTAVE_upd ne DB dhe kthen statusin e perfundimit te ekzekutimit te SP-se perkatese
        /// Shiko clsMesazh brenda ketij moduli per informacion te metejshem mbi vleren e kthimit te kesaj metode
        /// <param name="id"> id ritese </param>
        /// <param name="data">ora e fillimit </param>
        /// <param name="shenime">pershkrimi </param>
        /// <param name="koeficienti"> koeficienti</param>
        /// <param name="idPerdoruesi">id e perdoruesit qe ka kryer veprimin</param>
        /// <param name="idNdermarje">id ndermarje</param>
        /// <param name="idstatusdok">id e statusit </param>
        /// <returns> nje objekt clsMesazh qe tregon nese modifikimi eshte kryer ne rregull apo jo.</returns>
        /// </summary>
        internal clsMesazh modifikoKalendarFestash(int id, DateTime data, String shenime, decimal koeficienti, int idPerdoruesi, int idNdermarje, int idstatusdok)
        {

            dbManager.Open();
            clsMesazh mesazh = new clsMesazh();
            //shtimi i parametrave


            dbManager.CreateParameters(7);
            dbManager.AddParameters(0, "@ID", id, ParameterDirection.Input);
            dbManager.AddParameters(1, "@DATA", data, ParameterDirection.Input);
            dbManager.AddParameters(2, "@PERSHKRIMI", shenime, ParameterDirection.Input);
            dbManager.AddParameters(3, "@KOEFICIENTI", koeficienti, ParameterDirection.Input);
            dbManager.AddParameters(4, "@IDPERDORUESI", idPerdoruesi, ParameterDirection.Input);
            dbManager.AddParameters(5, "@IDNDERMARJE", idNdermarje, ParameterDirection.Input);
            dbManager.AddParameters(6, "@IDSTATUSDOK", idstatusdok, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_KALENDARIFESTAVE_upd");
            mesazh = new clsMesazh(true, MessagesResource.Messages["msgModifikimiMeSukses"]);

            return mesazh;

        }

        /// <summary>
        /// ekzekuton prc_T_KALENDARIFESTAVE_upddel ne DB dhe kthen statusin e perfundimit te ekzekutimit te SP-se perkatese
        /// Shiko clsMesazh brenda ketij moduli per informacion te metejshem mbi vleren e kthimit te kesaj metode
        /// <param name="id"> id ritese </param>
        /// <param name="idPerdoruesi">id e perdoruesit qe ka kryer veprimin</param>
        /// <returns> nje objekt clsMesazh qe tregon nese fshirja eshte kryer ne rregull apo jo.</returns>
        /// </summary>

        internal clsMesazh fshiKalendarFestash(int id, int idperdoruesi)
        {

            dbManager.Open();
            //shtimi i parametrave
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@ID", id, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUESI", idperdoruesi, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_KALENDARIFESTAVE_upddel");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
            return mesazh;
        }



        /// <summary>
        /// kthen Datatable konfigurim list orari sipas id ndermarjes
        /// </summary>
        ///<param name="idndermarje">id e ndermarjes</param>
        ///<returns>nje datatable qe permban nje koleksion me te gjitha konfigurimet e list orarit  te kesaj ndermarje </returns>

        internal DataTable ktheGjitheKalendarFestashSipasNdermarrjes(int idndermarje)
        {

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDNDERMARJE", idndermarje, ParameterDirection.Input);

            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KALENDARIFESTAVE_merrSipasNdermarrjes");

            return ds.Tables[0];

        }

        /// <summary>
        /// kontrollon nqs ekziston kjo date tek kalendari i festave ose jo
        /// </summary>
        /// <param name="data"></param>
        /// <param name="idndermarje"></param>
        /// <returns></returns>
        public bool ekzistonKalendarFestash(DateTime data, int idndermarje)
        {//kontrollon nqs ekziston nje kodifikim artikulli me kete kod

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@data", data, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idndermarje, ParameterDirection.Input);
            int pergjigje = Convert.ToInt32(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_KALENDARIFESTAVE_ekzistonFeste"));
            return Convert.ToBoolean(pergjigje);

        }


        #endregion



        /// <summary>
        /// Metodat e meposhtme i perkasin veprimeve qe kryhen nga objektet clsLegjendaListOrareve dhe colLegjendaListOrareve
        /// </summary>        
        #region LEGJENDA LIST ORAREVE

        /// <summary>
        /// kthen datarow legjendjen sipas id-se
        /// </summary>
        ///<param name="id"> id e kokes</param>
        ///<returns> nje datarow qe permban legjenden list orari me kete id</returns>
        internal DataRow merrLegjendaListOrari(int id)
        {

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@ID", id, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_LEGJENDALISTORAREVE_ktheSipasId");

            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];

        }

        /// <summary>
        /// kthen datarow me legjenden sipas kodit dhe ndermarjes
        /// </summary>
        ///<param name="kodi"> kodi i </param>
        ///<param name="idndermarje"> id e ndermarrjes</param>
        ///<returns> nje datarow qe permban legjenden  sipas kodit dhe  ndermarje</returns>
        internal DataRow merrLegjendaListOrariSipasKod(string kodi, int idndermarje)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@KODI", kodi, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idndermarje, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_LEGJENDALISTORAREVE_merrSipasKodit");

            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];

        }



        /// <summary>
        /// ekzekuton prc_T_LEGJENDALISTORAREVE_ins ne DB dhe kthen statusin e perfundimit te ekzekutimit te SP-se perkatese
        /// Shiko clsMesazh brenda ketij moduli per informacion te metejshem mbi vleren e kthimit te kesaj metode
        /// <param name="id"> id ritese </param>
        /// <param name="orefillimi">ore fillimi </param>
        /// <param name="shenime">pershkrimi </param>
        /// <param name="koeficienti"> koeficienti</param>
        /// <param name="idPerdoruesi">id e perdoruesit qe ka kryer veprimin</param>
        /// <param name="idNdermarje">id ndermarje</param>
        /// <param name="idstatusdok">id e statusit </param>
        /// <returns> nje objekt clsMesazh qe tregon nese ruajtja eshte kryer ne rregull apo jo.</returns>
        /// </summary>

        internal clsMesazh ruajLegjendeListOrari(out int id, string kodi, string orefillimi, string orembarimi, String shenime, decimal koeficienti, int idkomponente, int idPerdoruesi, int idkrijuesi, int idNdermarje, int idstatusdok)
        {
            id = -1;

            dbManager.Open();
            clsMesazh mesazh = new clsMesazh();
            //shtimi i parametrave

            dbManager.CreateParameters(11);
            dbManager.AddParameters(0, "@ID", id, ParameterDirection.Output);
            dbManager.AddParameters(1, "@OREFILLIMI", orefillimi, ParameterDirection.Input);
            dbManager.AddParameters(2, "@OREMBARIMI", orembarimi, ParameterDirection.Input);
            dbManager.AddParameters(3, "@PERSHKRIMI", shenime, ParameterDirection.Input);
            dbManager.AddParameters(4, "@KOEFICIENTI", koeficienti, ParameterDirection.Input);
            dbManager.AddParameters(5, "@IDPERDORUESI", idPerdoruesi, ParameterDirection.Input);
            dbManager.AddParameters(6, "@IDKRIJUESI", idkrijuesi, ParameterDirection.Input);
            dbManager.AddParameters(7, "@IDNDERMARJE", idNdermarje, ParameterDirection.Input);
            dbManager.AddParameters(8, "@IDSTATUSDOK", idstatusdok, ParameterDirection.Input);
            dbManager.AddParameters(9, "@KODI", kodi, ParameterDirection.Input);
            if (idkomponente == 0 || idkomponente == -1) dbManager.AddParameters(10, "@IDKOMPONENTELISTPAGESE", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(10, "@IDKOMPONENTELISTPAGESE", idkomponente, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_LEGJENDALISTORAREVE_ins");
            id = int.Parse(dbManager.Parameters[0].Value.ToString());
            mesazh = new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);

            return mesazh;
        }

        /// <summary>
        /// ekzekuton prc_T_LEGJENDALISTORAREVE_upd ne DB dhe kthen statusin e perfundimit te ekzekutimit te SP-se perkatese
        /// Shiko clsMesazh brenda ketij moduli per informacion te metejshem mbi vleren e kthimit te kesaj metode
        /// <param name="id"> id ritese </param>
        /// <param name="orefillimi">ora e fillimit </param>
        /// <param name="shenime">pershkrimi </param>
        /// <param name="koeficienti"> koeficienti</param>
        /// <param name="idPerdoruesi">id e perdoruesit qe ka kryer veprimin</param>
        /// <param name="idNdermarje">id ndermarje</param>
        /// <param name="idstatusdok">id e statusit </param>
        /// <returns> nje objekt clsMesazh qe tregon nese modifikimi eshte kryer ne rregull apo jo.</returns>
        /// </summary>
        internal clsMesazh modifikoLegjendeListOrari(int id, string kodi, string orefillimi, string orembarimi, String shenime, decimal koeficienti, int idkomponente, int idPerdoruesi, int idNdermarje, int idstatusdok)
        {
            dbManager.Open();
            clsMesazh mesazh = new clsMesazh();
            //shtimi i parametrave


            dbManager.CreateParameters(10);
            dbManager.AddParameters(0, "@ID", id, ParameterDirection.Input);
            dbManager.AddParameters(1, "@OREFILLIMI", orefillimi, ParameterDirection.Input);
            dbManager.AddParameters(2, "@OREMBARIMI", orembarimi, ParameterDirection.Input);
            dbManager.AddParameters(3, "@PERSHKRIMI", shenime, ParameterDirection.Input);
            dbManager.AddParameters(4, "@KOEFICIENTI", koeficienti, ParameterDirection.Input);
            dbManager.AddParameters(5, "@IDPERDORUESI", idPerdoruesi, ParameterDirection.Input);
            dbManager.AddParameters(6, "@IDNDERMARJE", idNdermarje, ParameterDirection.Input);
            dbManager.AddParameters(7, "@IDSTATUSDOK", idstatusdok, ParameterDirection.Input);
            dbManager.AddParameters(8, "@KODI", kodi, ParameterDirection.Input);
            if (idkomponente == 0 || idkomponente == -1) dbManager.AddParameters(9, "@IDKOMPONENTELISTPAGESE", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(9, "@IDKOMPONENTELISTPAGESE", idkomponente, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_LEGJENDALISTORAREVE_upd");
            mesazh = new clsMesazh(true, MessagesResource.Messages["msgModifikimiMeSukses"]);

            return mesazh;
        }
        internal clsMesazh modifikoLegjendeListOrariSipasKomponenteveTeReja(int idNdermarje)
        {
            dbManager.Open();
            clsMesazh mesazh = new clsMesazh();
            //shtimi i parametrave


            dbManager.CreateParameters(1);

            dbManager.AddParameters(0, "@IDNDERMARJE", idNdermarje, ParameterDirection.Input);

            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_LEGJENDALISTORAREVE_updSipasKomponenteveTeReja");
            mesazh = new clsMesazh(true, MessagesResource.Messages["msgModifikimiMeSukses"]);

            return mesazh;
        }

        /// <summary>
        /// ekzekuton prc_T_LEGJENDALISTORAREVE_upddel ne DB dhe kthen statusin e perfundimit te ekzekutimit te SP-se perkatese
        /// Shiko clsMesazh brenda ketij moduli per informacion te metejshem mbi vleren e kthimit te kesaj metode
        /// <param name="id"> id ritese </param>
        /// <param name="idPerdoruesi">id e perdoruesit qe ka kryer veprimin</param>
        /// <returns> nje objekt clsMesazh qe tregon nese fshirja eshte kryer ne rregull apo jo.</returns>
        /// </summary>

        internal clsMesazh fshiLegjendeListOrari(int id, int idperdoruesi)
        {
            dbManager.Open();
            //shtimi i parametrave
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@ID", id, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUESI", idperdoruesi, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_LEGJENDALISTORAREVE_upddel");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
            return mesazh;

        }



        /// <summary>
        /// kthen Datatable konfigurim list orari sipas id ndermarjes
        /// </summary>
        ///<param name="idndermarje">id e ndermarjes</param>
        ///<returns>nje datatable qe permban nje koleksion me te gjitha konfigurimet e list orarit  te kesaj ndermarje </returns>

        internal DataTable ktheGjitheLegjendaListOrariSipasNdermarrjes(int idndermarje)
        {

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDNDERMARJE", idndermarje, ParameterDirection.Input);

            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_LEGJENDALISTORAREVE_merrSipasNdermarrjes");

            return ds.Tables[0];

        }


        public bool ekzistonLegjendeListOrari(string kod, int idndermarje)
        {//kontrollon nqs ekziston nje kodifikim artikulli me kete kod

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@KODI", kod, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idndermarje, ParameterDirection.Input);
            int pergjigje = Convert.ToInt32(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_LEGJENDALISTORAREVE_ekzistonLegjende"));
            return Convert.ToBoolean(pergjigje);

        }
        public bool kaVeprimeSimboli(int id)
        {//kontrollon nqs ka veprime me kete kodifikim artikulli

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@ID", id, ParameterDirection.Input);

            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_LEGJENDALISTORAREVE_kaVeprime");
            if (ds.Tables[0].Rows.Count == 1)
                return true;
            if (ds.Tables[0].Rows.Count == 0)
                return false;
            return true;
        }

        #endregion

        /// <summary>
        /// Metodat e meposhtme i perkasin veprimeve qe kryhen nga objektet clsListOrare dhe colListOrare
        /// </summary>        
        #region LIST ORARE

        /// <summary>
        /// kthen datarow LIST ORARIN sipas id-se
        /// </summary>
        ///<param name="id"> id e kokes</param>
        ///<returns> nje datarow qe permban  list orari me kete id</returns>
        internal DataRow merrListOrari(int id)
        {

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@ID", id, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_LISTORARE_ktheSipasId");

            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];

        }





        /// <summary>
        /// ekzekuton prc_T_LISTORARE_ins ne DB dhe kthen statusin e perfundimit te ekzekutimit te SP-se perkatese
        /// Shiko clsMesazh brenda ketij moduli per informacion te metejshem mbi vleren e kthimit te kesaj metode
        /// <param name="id"> id ritese </param>
        /// <param name="orefillimi">ore fillimi </param>
        /// <param name="shenime">pershkrimi </param>
        /// <param name="koeficienti"> koeficienti</param>
        /// <param name="idPerdoruesi">id e perdoruesit qe ka kryer veprimin</param>
        /// <param name="idNdermarje">id ndermarje</param>
        /// <param name="idstatusdok">id e statusit </param>
        /// <returns> nje objekt clsMesazh qe tregon nese ruajtja eshte kryer ne rregull apo jo.</returns>
        /// </summary>

        internal clsMesazh ruajListOrari(out int id, int idpunonjesi, DateTime data, int idsimboli, int idPerdoruesi, int idkrijuesi, int idNdermarje, int idstatusdok)
        {
            id = -1;

            dbManager.Open();
            clsMesazh mesazh = new clsMesazh();
            //shtimi i parametrave

            dbManager.CreateParameters(8);
            dbManager.AddParameters(0, "@ID", id, ParameterDirection.Output);
            dbManager.AddParameters(1, "@data", data, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDPERDORUESI", idPerdoruesi, ParameterDirection.Input);
            dbManager.AddParameters(3, "@IDKRIJUESI", idkrijuesi, ParameterDirection.Input);
            dbManager.AddParameters(4, "@IDNDERMARJE", idNdermarje, ParameterDirection.Input);
            dbManager.AddParameters(5, "@IDSTATUSDOK", idstatusdok, ParameterDirection.Input);
            dbManager.AddParameters(6, "@idpunonjesi", idpunonjesi, ParameterDirection.Input);
            dbManager.AddParameters(7, "@idsimboli", idsimboli, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_LISTORARE_ins");
            id = int.Parse(dbManager.Parameters[0].Value.ToString());
            mesazh = new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);

            return mesazh;

        }

        /// <summary>
        /// ekzekuton prc_T_LISTORARE_upd ne DB dhe kthen statusin e perfundimit te ekzekutimit te SP-se perkatese
        /// Shiko clsMesazh brenda ketij moduli per informacion te metejshem mbi vleren e kthimit te kesaj metode
        /// <param name="id"> id ritese </param>
        /// <param name="orefillimi">ora e fillimit </param>
        /// <param name="shenime">pershkrimi </param>
        /// <param name="koeficienti"> koeficienti</param>
        /// <param name="idPerdoruesi">id e perdoruesit qe ka kryer veprimin</param>
        /// <param name="idNdermarje">id ndermarje</param>
        /// <param name="idstatusdok">id e statusit </param>
        /// <returns> nje objekt clsMesazh qe tregon nese modifikimi eshte kryer ne rregull apo jo.</returns>
        /// </summary>
        internal clsMesazh modifikoListOrari(int id, int idpunonjesi, DateTime data, int idsimboli, int idPerdoruesi, int idNdermarje, int idstatusdok)
        {

            dbManager.Open();
            clsMesazh mesazh = new clsMesazh();
            //shtimi i parametrave


            dbManager.CreateParameters(7);
            dbManager.AddParameters(0, "@ID", id, ParameterDirection.Input);
            dbManager.AddParameters(1, "@data", data, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDPERDORUESI", idPerdoruesi, ParameterDirection.Input);
            dbManager.AddParameters(3, "@IDNDERMARJE", idNdermarje, ParameterDirection.Input);
            dbManager.AddParameters(4, "@IDSTATUSDOK", idstatusdok, ParameterDirection.Input);
            dbManager.AddParameters(5, "@idpunonjesi", idpunonjesi, ParameterDirection.Input);
            dbManager.AddParameters(6, "@idsimboli", idsimboli, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_LISTORARE_upd");
            mesazh = new clsMesazh(true, MessagesResource.Messages["msgModifikimiMeSukses"]);

            return mesazh;
        }

        /// <summary>
        /// ekzekuton prc_T_LISTORARE_upddel ne DB dhe kthen statusin e perfundimit te ekzekutimit te SP-se perkatese
        /// Shiko clsMesazh brenda ketij moduli per informacion te metejshem mbi vleren e kthimit te kesaj metode
        /// <param name="id"> id ritese </param>
        /// <param name="idPerdoruesi">id e perdoruesit qe ka kryer veprimin</param>
        /// <returns> nje objekt clsMesazh qe tregon nese fshirja eshte kryer ne rregull apo jo.</returns>
        /// </summary>

        internal clsMesazh fshiListOrari(int id, int idperdoruesi)
        {

            dbManager.Open();
            //shtimi i parametrave
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@ID", id, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUESI", idperdoruesi, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_LISTORARE_upddel");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
            return mesazh;

        }
        internal clsMesazh fshiListOrariSipasDatesDhePunonjesit(DateTime date, int idpunonjesi)
        {

            dbManager.Open();
            //shtimi i parametrave
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@DATA", date, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPUNONJESI", idpunonjesi, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_LISTORARE_deleteListOrarPerPunonjesNeDate");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
            return mesazh;

        }



        /// <summary>
        /// kthen Datatable konfigurim list orari sipas id ndermarjes
        /// </summary>
        ///<param name="idndermarje">id e ndermarjes</param>
        ///<returns>nje datatable qe permban nje koleksion me te gjitha konfigurimet e list orarit  te kesaj ndermarje </returns>

        internal IEnumerable<clsListOrare> ktheGjitheListOrariSipasNdermarrjes(int idndermarje)
        {

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDNDERMARJE", idndermarje, ParameterDirection.Input);

            return dbManager.GetIEnumerbale("prc_T_LISTORARE_merrSipasNdermarrjes", clsListOrare.Krijo);



        }
        internal DataTable merrListOrarePerExport(int idndermarje, DateTime datefillimi, DateTime datembarimi)
        {

            string salt = WebConfigurationManager.AppSettings["salt"];
            dbManager.Open();
            dbManager.CreateParameters(4);
            dbManager.AddParameters(0, "@IDNDERMARJE", idndermarje, ParameterDirection.Input);
            dbManager.AddParameters(1, "@DataFillimi", datefillimi, ParameterDirection.Input);
            dbManager.AddParameters(2, "@dateMbarrimi", datembarimi, ParameterDirection.Input);
            dbManager.AddParameters(3, "@salt", salt, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_LISTORARE_merrPerExport");

            return ds.Tables[0];

        }
        internal DataTable ktheGjitheListOrariSipasPunonjesit(int idpunonjes)
        {

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDPUNONJESI", idpunonjes, ParameterDirection.Input);

            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_LISTORARE_merrSipasPunonjesit");

            return ds.Tables[0];

        }
        internal DataTable ktheGjitheListOrariSipasPunonjesitSipasPeriudhes(int idpunonjes, DateTime datefillimi, DateTime datembarimi)
        {

            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@IDPUNONJESI", idpunonjes, ParameterDirection.Input);
            dbManager.AddParameters(1, "@datefillimi", datefillimi, ParameterDirection.Input);
            dbManager.AddParameters(2, "@datembarimi", datembarimi, ParameterDirection.Input);

            return dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_LISTORARE_merrSipasPunonjesitDhePeriudhes").Tables[0];



        }
        public DataTable ktheGjitheListOrariSipasPunonjesveSipasPeriudhes(List<int> idPunonjesish, DateTime datafill, DateTime datembar)
        {
            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@IDPUNONJESISH", string.Join(",", idPunonjesish), ParameterDirection.Input);
            dbManager.AddParameters(1, "@datefillimi", datafill, ParameterDirection.Input);
            dbManager.AddParameters(2, "@datembarimi", datembar, ParameterDirection.Input);

            return dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_LISTORARE_merrSipasPunonjesveDhePeriudhes").Tables[0];
        }
        public bool ekzistonListOrari(DateTime data, int idpunonjes)
        {//kontrollon nqs ekziston nje kodifikim artikulli me kete kod

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@data", data, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPUNONJESI", idpunonjes, ParameterDirection.Input);
            int pergjigje = Convert.ToInt32(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_LISTORARE_ekzistonListOrarPerPunonjesNeDate"));
            return Convert.ToBoolean(pergjigje);

        }


        #endregion

        /// <summary>
        /// Metodat e meposhtme i perkasin veprimeve qe kryhen nga objektet clsOreShtese dhe colOreShtese
        /// </summary>        
        #region ORE SHTESE (komponente formule)

        /// <summary>
        /// kthen datarow ore shtese sipas id-se
        /// </summary>
        ///<param name="id"> id e kokes</param>
        ///<returns> nje datarow qe permban  ore shtese me kete id</returns>
        internal DataRow merrOreShtese(int id)
        {

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@ID", id, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_ORESHTESE_ktheSipasId");

            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];

        }





        /// <summary>
        /// ekzekuton prc_T_ORESHTESE_ins ne DB dhe kthen statusin e perfundimit te ekzekutimit te SP-se perkatese
        /// Shiko clsMesazh brenda ketij moduli per informacion te metejshem mbi vleren e kthimit te kesaj metode
        /// <param name="id"> id ritese </param>
        /// <param name="orefillimi">ore fillimi </param>
        /// <param name="shenime">pershkrimi </param>
        /// <param name="koeficienti"> koeficienti</param>
        /// <param name="idPerdoruesi">id e perdoruesit qe ka kryer veprimin</param>
        /// <param name="idNdermarje">id ndermarje</param>
        /// <param name="idstatusdok">id e statusit </param>
        /// <returns> nje objekt clsMesazh qe tregon nese ruajtja eshte kryer ne rregull apo jo.</returns>
        /// </summary>

        internal clsMesazh ruajOreShtese(out int id, int idpunonjesi, DateTime data, DateTime nga, DateTime ne, decimal totali, int idPerdoruesi, int idkrijuesi, int idNdermarje, int idstatusdok, int idkomponente, int muaji, int viti, string muajilp, int vitilp)
        {
            id = -1;

            dbManager.Open();
            //shtimi i parametrave

            dbManager.CreateParameters(15);
            dbManager.AddParameters(0, "@ID", id, ParameterDirection.Output);
            if (data == new DateTime()) dbManager.AddParameters(1, "@data", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(1, "@data", data, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDPERDORUESI", idPerdoruesi, ParameterDirection.Input);
            dbManager.AddParameters(3, "@IDKRIJUESI", idkrijuesi, ParameterDirection.Input);
            dbManager.AddParameters(4, "@IDNDERMARJE", idNdermarje, ParameterDirection.Input);
            dbManager.AddParameters(5, "@IDSTATUSDOK", idstatusdok, ParameterDirection.Input);
            dbManager.AddParameters(6, "@idpunonjesi", idpunonjesi, ParameterDirection.Input);
            if (nga == new DateTime()) dbManager.AddParameters(7, "@ngaora", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(7, "@ngaora", nga, ParameterDirection.Input);
            if (ne == new DateTime()) dbManager.AddParameters(8, "@neora", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(8, "@neora", ne, ParameterDirection.Input);
            dbManager.AddParameters(9, "@totali", totali, ParameterDirection.Input);
            dbManager.AddParameters(10, "@IDKOMPONENTE", idkomponente, ParameterDirection.Input);
            dbManager.AddParameters(11, "@MUAJI", muaji, ParameterDirection.Input);
            dbManager.AddParameters(12, "@VITI", viti, ParameterDirection.Input);
            dbManager.AddParameters(13, "@MUAJILP", muajilp, ParameterDirection.Input);
            dbManager.AddParameters(14, "@VITILP", vitilp, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_ORESHTESE_ins");
            return new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);

        }

        /// <summary>
        /// ekzekuton prc_T_ORESHTESE_upd ne DB dhe kthen statusin e perfundimit te ekzekutimit te SP-se perkatese
        /// Shiko clsMesazh brenda ketij moduli per informacion te metejshem mbi vleren e kthimit te kesaj metode
        /// <param name="id"> id ritese </param>
        /// <param name="orefillimi">ora e fillimit </param>
        /// <param name="shenime">pershkrimi </param>
        /// <param name="koeficienti"> koeficienti</param>
        /// <param name="idPerdoruesi">id e perdoruesit qe ka kryer veprimin</param>
        /// <param name="idNdermarje">id ndermarje</param>
        /// <param name="idstatusdok">id e statusit </param>
        /// <returns> nje objekt clsMesazh qe tregon nese modifikimi eshte kryer ne rregull apo jo.</returns>
        /// </summary>
        internal clsMesazh modifikoOreShtese(int id, int idpunonjesi, DateTime data, DateTime nga, DateTime ne, decimal totali, int idPerdoruesi, int idNdermarje, int idstatusdok, int idkomponente, int muaji, int viti, string muajilp, int vitilp)
        {

            dbManager.Open();
            clsMesazh mesazh = new clsMesazh();
            //shtimi i parametrave


            dbManager.CreateParameters(14);
            dbManager.AddParameters(0, "@ID", id, ParameterDirection.Input);
            dbManager.AddParameters(1, "@data", data, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDPERDORUESI", idPerdoruesi, ParameterDirection.Input);
            dbManager.AddParameters(3, "@IDNDERMARJE", idNdermarje, ParameterDirection.Input);
            dbManager.AddParameters(4, "@IDSTATUSDOK", idstatusdok, ParameterDirection.Input);
            dbManager.AddParameters(5, "@idpunonjesi", idpunonjesi, ParameterDirection.Input);
            dbManager.AddParameters(6, "@ngaora", nga, ParameterDirection.Input);
            dbManager.AddParameters(7, "@neora", ne, ParameterDirection.Input);
            dbManager.AddParameters(8, "@totali", totali, ParameterDirection.Input);
            dbManager.AddParameters(9, "@IDKOMPONENTE", idkomponente, ParameterDirection.Input);
            dbManager.AddParameters(10, "@MUAJI", muaji, ParameterDirection.Input);
            dbManager.AddParameters(11, "@VITI", viti, ParameterDirection.Input);
            dbManager.AddParameters(12, "@MUAJILP", muajilp, ParameterDirection.Input);
            dbManager.AddParameters(13, "@VITILP", vitilp, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_ORESHTESE_upd");
            mesazh = new clsMesazh(true, MessagesResource.Messages["msgModifikimiMeSukses"]);

            return mesazh;

        }

        /// <summary>
        /// ekzekuton prc_T_ORESHTESE_upddel ne DB dhe kthen statusin e perfundimit te ekzekutimit te SP-se perkatese
        /// Shiko clsMesazh brenda ketij moduli per informacion te metejshem mbi vleren e kthimit te kesaj metode
        /// <param name="id"> id ritese </param>
        /// <param name="idPerdoruesi">id e perdoruesit qe ka kryer veprimin</param>
        /// <returns> nje objekt clsMesazh qe tregon nese fshirja eshte kryer ne rregull apo jo.</returns>
        /// </summary>

        internal clsMesazh fshiOreShtese(int id, int idperdoruesi)
        {

            dbManager.Open();
            //shtimi i parametrave
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@ID", id, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUESI", idperdoruesi, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_ORESHTESE_upddel");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
            return mesazh;

        }



        /// <summary>
        /// kthen Datatable konfigurim list orari sipas id ndermarjes
        /// </summary>
        ///<param name="idndermarje">id e ndermarjes</param>
        ///<returns>nje datatable qe permban nje koleksion me te gjitha konfigurimet e list orarit  te kesaj ndermarje </returns>

        internal DataTable ktheGjitheOreShteseSipasNdermarrjes(int idndermarje)
        {

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDNDERMARJE", idndermarje, ParameterDirection.Input);

            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_ORESHTESE_merrSipasNdermarrjes");

            return ds.Tables[0];

        }
        internal DataTable merrOreShtesePerExport(int idndermarje)
        {

            dbManager.Open();
            string salt = WebConfigurationManager.AppSettings["salt"];
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDNDERMARJE", idndermarje, ParameterDirection.Input);
            dbManager.AddParameters(1, "@salt", salt, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_ORESHTESE_merrPerExport");

            return ds.Tables[0];

        }
        internal DataTable ktheGjitheOreShteseSipasPunonjesit(int idpunonjes)
        {

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDPUNONJESI", idpunonjes, ParameterDirection.Input);

            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_ORESHTESE_merrSipasPunonjesit");

            return ds.Tables[0];

        }
        internal DataTable ktheGjitheOreShteseSipasPunonjesitDhePeriudhes(int idpunonjes, string muaji, int viti, string kodkomponente)
        {


            dbManager.Open();
            dbManager.CreateParameters(4);
            dbManager.AddParameters(0, "@IDPUNONJESI", idpunonjes, ParameterDirection.Input);
            dbManager.AddParameters(1, "@muaji", muaji, ParameterDirection.Input);
            dbManager.AddParameters(2, "@viti", viti, ParameterDirection.Input);
            dbManager.AddParameters(3, "@kodkomponente", kodkomponente, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_ORESHTESE_merrSipasPunonjesitDhePeriudhes");

            return ds.Tables[0];
        }
        internal DataTable ktheGjitheOreShteseSipasPunonjesitDhePeriudhesAll(int IdPunonjesi, string muaji, int viti, string[] arrKodeKomp)
        {


            dbManager.Open();
            dbManager.CreateParameters(4);
            dbManager.AddParameters(0, "@IDPUNONJESI", IdPunonjesi, ParameterDirection.Input);
            dbManager.AddParameters(1, "@muaji", muaji, ParameterDirection.Input);
            dbManager.AddParameters(2, "@viti", viti, ParameterDirection.Input);
            dbManager.AddParameters(3, "@kodkomponenteAll", string.Join(",", arrKodeKomp), ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_ORESHTESE_merrSipasPunonjesitDhePeriudhesAll");

            return ds.Tables[0];
        }
        public IEnumerable<clsOreShtese> ktheGjitheOreShteseSipasPunonjesveDhePeriudhesAll(List<int> idpunonjesish, int muaji, int viti, int idKokaLp)
        {
            dbManager.Open();
            dbManager.CreateParameters(4);
            dbManager.AddParameters(0, "@IDPUNONJESISH", string.Join(",", idpunonjesish), ParameterDirection.Input);
            dbManager.AddParameters(1, "@muaji", muaji, ParameterDirection.Input);
            dbManager.AddParameters(2, "@viti", viti, ParameterDirection.Input);
            dbManager.AddParameters(3, "@idKokaLp", idKokaLp, ParameterDirection.Input);
            return dbManager.GetIEnumerbale("prc_T_ORESHTESE_merrSipasPunonjesveDhePeriudhesAll", clsOreShtese.KrijoBasic);

        }
        public bool ekzistonOreShtese(int idkomponente, int idpunonjesi, string muaji, int viti, string muajilp, int vitilp)
        {//kontrollon nqs ekziston nje kodifikim artikulli me kete kod

            dbManager.Open();
            dbManager.CreateParameters(6);
            dbManager.AddParameters(0, "@idkomponente", idkomponente, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPUNONJESI", idpunonjesi, ParameterDirection.Input);
            dbManager.AddParameters(2, "@muaji", muaji, ParameterDirection.Input);
            dbManager.AddParameters(3, "@viti", viti, ParameterDirection.Input);
            dbManager.AddParameters(4, "@muajilp", muajilp, ParameterDirection.Input);
            dbManager.AddParameters(5, "@vitilp", vitilp, ParameterDirection.Input);
            int pergjigje = Convert.ToInt32(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_t_oreshtese_ekzistonPerKomponentePunonjesDheMuaj"));
            return Convert.ToBoolean(pergjigje);

        }


        #endregion

        /// <summary>
        /// Metodat e meposhtme i perkasin veprimeve qe kryhen nga objektet clsKomponenteNr dhe colKomponenteNr
        /// </summary>        
        #region Komponente Nr

        /// <summary>
        /// kthen datarow KomponenteNr sipas id-se
        /// </summary>
        ///<param name="id"> id e kokes</param>
        ///<returns> nje datarow qe permban  ore shtese me kete id</returns>
        internal void merrKomponenteNr(int id, clsKomponenteNr komponNr)
        {

            string salt = WebConfigurationManager.AppSettings["salt"];
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@ID", id, ParameterDirection.Input);
            dbManager.AddParameters(1, "@salt", salt, ParameterDirection.Input);
            dbManager.FillObject<clsKomponenteNr>("prc_T_KOMPONENTENR_ktheSipasId", komponNr.mbushKomponenteNr);

            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KOMPONENTENR_ktheSipasId");




        }





        /// <summary>
        /// ekzekuton prc_T_KOMPONENTENR_ins ne DB dhe kthen statusin e perfundimit te ekzekutimit te SP-se perkatese
        /// Shiko clsMesazh brenda ketij moduli per informacion te metejshem mbi vleren e kthimit te kesaj metode
        /// <param name="id"> id ritese </param>
        /// <param name="orefillimi">ore fillimi </param>
        /// <param name="shenime">pershkrimi </param>
        /// <param name="koeficienti"> koeficienti</param>
        /// <param name="idPerdoruesi">id e perdoruesit qe ka kryer veprimin</param>
        /// <param name="idNdermarje">id ndermarje</param>
        /// <param name="idstatusdok">id e statusit </param>
        /// <returns> nje objekt clsMesazh qe tregon nese ruajtja eshte kryer ne rregull apo jo.</returns>
        /// </summary>

        internal clsMesazh ruajKomponenteNr(out int id, int idpunonjesi, decimal totali, int idPerdoruesi, int idkrijuesi, int idNdermarje, int idstatusdok, int idkomponente, int muaji, int viti, string muajilp, int vitilp)
        {
            id = -1;

            dbManager.Open();
            //shtimi i parametrave
            string salt = WebConfigurationManager.AppSettings["salt"];
            dbManager.CreateParameters(13);
            dbManager.AddParameters(0, "@ID", id, ParameterDirection.Output);
            dbManager.AddParameters(1, "@IDPERDORUESI", idPerdoruesi, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDKRIJUESI", idkrijuesi, ParameterDirection.Input);
            dbManager.AddParameters(3, "@IDNDERMARJE", idNdermarje, ParameterDirection.Input);
            dbManager.AddParameters(4, "@IDSTATUSDOK", idstatusdok, ParameterDirection.Input);
            dbManager.AddParameters(5, "@idpunonjesi", idpunonjesi, ParameterDirection.Input);
            dbManager.AddParameters(6, "@VLERA", totali, ParameterDirection.Input);
            dbManager.AddParameters(7, "@IDKOMPONENTE", idkomponente, ParameterDirection.Input);
            dbManager.AddParameters(8, "@MUAJI", muaji, ParameterDirection.Input);
            dbManager.AddParameters(9, "@VITI", viti, ParameterDirection.Input);
            dbManager.AddParameters(10, "@MUAJILP", muajilp, ParameterDirection.Input);
            dbManager.AddParameters(11, "@VITILP", vitilp, ParameterDirection.Input);
            dbManager.AddParameters(12, "@salt", salt, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_KOMPONENTENR_ins");
            id = int.Parse(dbManager.Parameters[0].Value.ToString());
            return new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);

        }

        /// <summary>
        /// ekzekuton prc_T_KOMPONENTENR_upd ne DB dhe kthen statusin e perfundimit te ekzekutimit te SP-se perkatese
        /// Shiko clsMesazh brenda ketij moduli per informacion te metejshem mbi vleren e kthimit te kesaj metode
        /// <param name="id"> id ritese </param>
        /// <param name="orefillimi">ora e fillimit </param>
        /// <param name="shenime">pershkrimi </param>
        /// <param name="koeficienti"> koeficienti</param>
        /// <param name="idPerdoruesi">id e perdoruesit qe ka kryer veprimin</param>
        /// <param name="idNdermarje">id ndermarje</param>
        /// <param name="idstatusdok">id e statusit </param>
        /// <returns> nje objekt clsMesazh qe tregon nese modifikimi eshte kryer ne rregull apo jo.</returns>
        /// </summary>
        internal clsMesazh modifikoKomponenteNr(int id, int idpunonjesi, decimal totali, int idPerdoruesi, int idNdermarje, int idstatusdok, int idkomponente, int muaji, int viti, string muajilp, int vitilp)
        {

            dbManager.Open();
            clsMesazh mesazh = new clsMesazh();
            //shtimi i parametrave
            string salt = WebConfigurationManager.AppSettings["salt"];

            dbManager.CreateParameters(12);
            dbManager.AddParameters(0, "@ID", id, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUESI", idPerdoruesi, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDNDERMARJE", idNdermarje, ParameterDirection.Input);
            dbManager.AddParameters(3, "@IDSTATUSDOK", idstatusdok, ParameterDirection.Input);
            dbManager.AddParameters(4, "@idpunonjesi", idpunonjesi, ParameterDirection.Input);
            dbManager.AddParameters(5, "@VLERA", totali, ParameterDirection.Input);
            dbManager.AddParameters(6, "@IDKOMPONENTE", idkomponente, ParameterDirection.Input);
            dbManager.AddParameters(7, "@MUAJI", muaji, ParameterDirection.Input);
            dbManager.AddParameters(8, "@VITI", viti, ParameterDirection.Input);
            dbManager.AddParameters(9, "@MUAJILP", muajilp, ParameterDirection.Input);
            dbManager.AddParameters(10, "@VITILP", vitilp, ParameterDirection.Input);
            dbManager.AddParameters(11, "@salt", salt, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_KOMPONENTENR_upd");
            mesazh = new clsMesazh(true, MessagesResource.Messages["msgModifikimiMeSukses"]);

            return mesazh;

        }

        /// <summary>
        /// ekzekuton prc_T_KOMPONENTENR_upddel ne DB dhe kthen statusin e perfundimit te ekzekutimit te SP-se perkatese
        /// Shiko clsMesazh brenda ketij moduli per informacion te metejshem mbi vleren e kthimit te kesaj metode
        /// <param name="id"> id ritese </param>
        /// <param name="idPerdoruesi">id e perdoruesit qe ka kryer veprimin</param>
        /// <returns> nje objekt clsMesazh qe tregon nese fshirja eshte kryer ne rregull apo jo.</returns>
        /// </summary>

        internal clsMesazh fshiKomponenteNr(int id, int idperdoruesi)
        {

            dbManager.Open();
            //shtimi i parametrave
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@ID", id, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUESI", idperdoruesi, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_KOMPONENTENR_upddel");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
            return mesazh;

        }



        /// <summary>
        /// kthen Datatable konfigurim list orari sipas id ndermarjes
        /// </summary>
        ///<param name="idndermarje">id e ndermarjes</param>
        ///<returns>nje datatable qe permban nje koleksion me te gjitha konfigurimet e list orarit  te kesaj ndermarje </returns>

        internal IEnumerable<clsKomponenteNr> ktheGjitheKomponenteNrSipasNdermarrjes(int idndermarje)
        {

            string salt = WebConfigurationManager.AppSettings["salt"];
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDNDERMARJE", idndermarje, ParameterDirection.Input);
            dbManager.AddParameters(1, "@salt", salt, ParameterDirection.Input);
            return dbManager.GetIEnumerbale("prc_T_KOMPONENTENR_merrSipasNdermarrjes", clsKomponenteNr.Krijo);


        }
        internal DataTable merrKomponenteNrPerExport(int idndermarje)
        {

            dbManager.Open();
            string salt = WebConfigurationManager.AppSettings["salt"];
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDNDERMARJE", idndermarje, ParameterDirection.Input);
            dbManager.AddParameters(1, "@salt", salt, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KOMPONENTENR_merrPerExport");

            return ds.Tables[0];

        }
        internal IEnumerable<clsKomponenteNr> ktheGjitheKomponenteNrSipasPunonjesit(int idpunonjes)
        {

            string salt = WebConfigurationManager.AppSettings["salt"];
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDPUNONJESI", idpunonjes, ParameterDirection.Input);
            dbManager.AddParameters(1, "@salt", salt, ParameterDirection.Input);
            return dbManager.GetIEnumerbale("prc_T_KOMPONENTENR_merrSipasPunonjesit", clsKomponenteNr.Krijo);


        }
        internal DataTable ktheGjitheKomponenteNrSipasPunonjesitDhePeriudhes(int idpunonjes, string muaji, int viti, string kodkomponente)
        {
            string salt = WebConfigurationManager.AppSettings["salt"];
            dbManager.Open();
            dbManager.CreateParameters(5);
            dbManager.AddParameters(0, "@IDPUNONJESI", idpunonjes, ParameterDirection.Input);
            dbManager.AddParameters(1, "@muaji", muaji, ParameterDirection.Input);
            dbManager.AddParameters(2, "@viti", viti, ParameterDirection.Input);
            dbManager.AddParameters(3, "@kodkomponente", kodkomponente, ParameterDirection.Input);
            dbManager.AddParameters(4, "@salt", salt, ParameterDirection.Input);
            return dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KOMPONENTENR_merrSipasPunonjesitDhePeriudhes").Tables[0];
        }


        public IEnumerable<clsKomponenteNr> ktheGjitheKomponenteNrSipasPunonjesveDhePeriudhes(List<int> idpunonjesish, int muaji, int viti, int idKokaLp)
        {
            string salt = WebConfigurationManager.AppSettings["salt"];
            dbManager.Open();
            dbManager.CreateParameters(5);
            dbManager.AddParameters(0, "@IDPUNONJESISH", string.Join(",", idpunonjesish), ParameterDirection.Input);
            dbManager.AddParameters(1, "@muaji", muaji, ParameterDirection.Input);
            dbManager.AddParameters(2, "@viti", viti, ParameterDirection.Input);
            dbManager.AddParameters(3, "@idKokaLp", idKokaLp, ParameterDirection.Input);
            dbManager.AddParameters(4, "@salt", salt, ParameterDirection.Input);
            return dbManager.GetIEnumerbale("prc_T_KOMPONENTENR_merrSipasPunonjesveDhePeriudhesAll", clsKomponenteNr.KrijoBasic);

        }

        public bool ekzistonKomponenteNr(int idkomponente, int idpunonjesi, string muaji, int viti, string muajilp, int vitilp)
        {//kontrollon nqs ekziston nje kodifikim artikulli me kete kod

            dbManager.Open();
            dbManager.CreateParameters(6);
            dbManager.AddParameters(0, "@idkomponente", idkomponente, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPUNONJESI", idpunonjesi, ParameterDirection.Input);
            dbManager.AddParameters(2, "@muaji", muaji, ParameterDirection.Input);
            dbManager.AddParameters(3, "@viti", viti, ParameterDirection.Input);
            dbManager.AddParameters(4, "@muajilp", muajilp, ParameterDirection.Input);
            dbManager.AddParameters(5, "@vitilp", vitilp, ParameterDirection.Input);
            int pergjigje = Convert.ToInt32(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_t_komponentenr_ekzistonPerKomponentePunonjesDheMuaj"));
            return Convert.ToBoolean(pergjigje);

        }



        #endregion
        /// <summary>
        /// Metodat e meposhtme i perkasin veprimeve qe kryhen nga objektet clsDiteLeje dhe colDiteLeje
        /// </summary>        
        #region DITE LEJE

        /// <summary>
        /// kthen datarow ore shtese sipas id-se
        /// </summary>
        ///<param name="id"> id e kokes</param>
        ///<returns> nje datarow qe permban  ore shtese me kete id</returns>
        internal DataRow merrDiteLeje(int id)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@ID", id, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_DITELEJE_ktheSipasId");

            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];


        }





        /// <summary>
        /// ekzekuton prc_T_DITELEJE_ins ne DB dhe kthen statusin e perfundimit te ekzekutimit te SP-se perkatese
        /// Shiko clsMesazh brenda ketij moduli per informacion te metejshem mbi vleren e kthimit te kesaj metode
        /// <param name="id"> id ritese </param>
        /// <param name="orefillimi">ore fillimi </param>
        /// <param name="shenime">pershkrimi </param>
        /// <param name="koeficienti"> koeficienti</param>
        /// <param name="idPerdoruesi">id e perdoruesit qe ka kryer veprimin</param>
        /// <param name="idNdermarje">id ndermarje</param>
        /// <param name="idstatusdok">id e statusit </param>
        /// <returns> nje objekt clsMesazh qe tregon nese ruajtja eshte kryer ne rregull apo jo.</returns>
        /// </summary>

        internal clsMesazh ruajDiteLeje(out int id, int idpunonjesi, DateTime datafillimi, DateTime datembarimi, decimal nrditesh, int idPerdoruesi, int idkrijuesi, int idNdermarje, int idstatusdok)
        {
            id = -1;

            dbManager.Open();
            clsMesazh mesazh = new clsMesazh();
            //shtimi i parametrave

            dbManager.CreateParameters(9);
            dbManager.AddParameters(0, "@ID", id, ParameterDirection.Output);
            if (datafillimi == new DateTime()) dbManager.AddParameters(1, "@datefillimi", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(1, "@datefillimi", datafillimi, ParameterDirection.Input);
            if (datembarimi == new DateTime()) dbManager.AddParameters(2, "@datembarimi", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(2, "@datembarimi", datembarimi, ParameterDirection.Input);
            dbManager.AddParameters(3, "@IDPERDORUESI", idPerdoruesi, ParameterDirection.Input);
            dbManager.AddParameters(4, "@IDKRIJUESI", idkrijuesi, ParameterDirection.Input);
            dbManager.AddParameters(5, "@IDNDERMARJE", idNdermarje, ParameterDirection.Input);
            dbManager.AddParameters(6, "@IDSTATUSDOK", idstatusdok, ParameterDirection.Input);
            dbManager.AddParameters(7, "@idpunonjesi", idpunonjesi, ParameterDirection.Input);
            dbManager.AddParameters(8, "@nrditesh", nrditesh, ParameterDirection.Input);

            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_DITELEJE_ins");
            id = int.Parse(dbManager.Parameters[0].Value.ToString());
            mesazh = new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);

            return mesazh;

        }

        /// <summary>
        /// ekzekuton prc_T_DITELEJE_upd ne DB dhe kthen statusin e perfundimit te ekzekutimit te SP-se perkatese
        /// Shiko clsMesazh brenda ketij moduli per informacion te metejshem mbi vleren e kthimit te kesaj metode
        /// <param name="id"> id ritese </param>
        /// <param name="orefillimi">ora e fillimit </param>
        /// <param name="shenime">pershkrimi </param>
        /// <param name="koeficienti"> koeficienti</param>
        /// <param name="idPerdoruesi">id e perdoruesit qe ka kryer veprimin</param>
        /// <param name="idNdermarje">id ndermarje</param>
        /// <param name="idstatusdok">id e statusit </param>
        /// <returns> nje objekt clsMesazh qe tregon nese modifikimi eshte kryer ne rregull apo jo.</returns>
        /// </summary>
        internal clsMesazh modifikoDiteLeje(int id, int idpunonjesi, DateTime datafillimi, DateTime datembarimi, decimal nrditesh, int idPerdoruesi, int idNdermarje, int idstatusdok)
        {

            dbManager.Open();
            clsMesazh mesazh = new clsMesazh();
            //shtimi i parametrave


            dbManager.CreateParameters(8);
            dbManager.AddParameters(0, "@ID", id, ParameterDirection.Input);
            if (datafillimi == new DateTime()) dbManager.AddParameters(1, "@datefillimi", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(1, "@datefillimi", datafillimi, ParameterDirection.Input);
            if (datembarimi == new DateTime()) dbManager.AddParameters(2, "@datembarimi", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(2, "@datembarimi", datembarimi, ParameterDirection.Input);
            dbManager.AddParameters(3, "@IDPERDORUESI", idPerdoruesi, ParameterDirection.Input);
            dbManager.AddParameters(4, "@IDNDERMARJE", idNdermarje, ParameterDirection.Input);
            dbManager.AddParameters(5, "@IDSTATUSDOK", idstatusdok, ParameterDirection.Input);
            dbManager.AddParameters(6, "@idpunonjesi", idpunonjesi, ParameterDirection.Input);
            dbManager.AddParameters(7, "@nrditesh", nrditesh, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_DITELEJE_upd");
            mesazh = new clsMesazh(true, MessagesResource.Messages["msgModifikimiMeSukses"]);

            return mesazh;

        }

        /// <summary>
        /// ekzekuton prc_T_DITELEJE_upddel ne DB dhe kthen statusin e perfundimit te ekzekutimit te SP-se perkatese
        /// Shiko clsMesazh brenda ketij moduli per informacion te metejshem mbi vleren e kthimit te kesaj metode
        /// <param name="id"> id ritese </param>
        /// <param name="idPerdoruesi">id e perdoruesit qe ka kryer veprimin</param>
        /// <returns> nje objekt clsMesazh qe tregon nese fshirja eshte kryer ne rregull apo jo.</returns>
        /// </summary>

        internal clsMesazh fshiDiteLeje(int id, int idperdoruesi)
        {

            dbManager.Open();
            //shtimi i parametrave
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@ID", id, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUESI", idperdoruesi, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_DITELEJE_upddel");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
            return mesazh;


        }



        /// <summary>
        /// kthen Datatable konfigurim list orari sipas id ndermarjes
        /// </summary>
        ///<param name="idndermarje">id e ndermarjes</param>
        ///<returns>nje datatable qe permban nje koleksion me te gjitha konfigurimet e list orarit  te kesaj ndermarje </returns>

        internal DataTable ktheGjitheDiteLejeSipasNdermarrjes(int idndermarje)
        {

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDNDERMARJE", idndermarje, ParameterDirection.Input);

            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_DITELEJE_merrSipasNdermarrjes");

            return ds.Tables[0];

        }
        internal DataTable merrDiteLejePerExport(int idndermarje)
        {

            dbManager.Open();
            string salt = WebConfigurationManager.AppSettings["salt"];
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDNDERMARJE", idndermarje, ParameterDirection.Input);
            dbManager.AddParameters(1, "@salt", salt, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_DITELEJE_merrPerExport");

            return ds.Tables[0];

        }
        internal DataTable ktheGjitheDiteLejeSipasPunonjesit(int idpunonjes)
        {

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDPUNONJESI", idpunonjes, ParameterDirection.Input);

            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_DITELEJE_merrSipasPunonjesit");

            return ds.Tables[0];

        }
        internal decimal ktheGjitheDiteLejeSipasPunonjesitDhePeriudhes(int idpunonjes, string muaji, int viti, int idKokaLp)
        {
            decimal ore = 0;


            dbManager.Open();
            dbManager.CreateParameters(4);
            dbManager.AddParameters("@IDPUNONJESI", idpunonjes, ParameterDirection.Input);
            dbManager.AddParameters("@muaji", muaji, ParameterDirection.Input);
            dbManager.AddParameters("@viti", viti, ParameterDirection.Input);
            dbManager.AddParameters("@IDKOKALP", idKokaLp, ParameterDirection.Input);

            var obj = dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_DITELEJE_merrSipasPunonjesitDhePeriudhes");
            decimal.TryParse(obj.ToString(), out ore);
            return ore;

        }
        internal Dictionary<int, decimal> ktheGjitheDiteLejeSipasPunonjesveDhePeriudhes(List<int> idpunonjesish, string muaji, int viti, int idKokaLp)
        {
            dbManager.Open();
            dbManager.CreateParameters(4);
            dbManager.AddParameters("@PUNONJESITIDS", string.Join(",", idpunonjesish), ParameterDirection.Input);
            dbManager.AddParameters("@muaji", muaji, ParameterDirection.Input);
            dbManager.AddParameters("@viti", viti, ParameterDirection.Input);
            dbManager.AddParameters("@IDKOKALP", idKokaLp, ParameterDirection.Input);

            return dbManager.GetDictionary<int, decimal>("prc_T_DITELEJE_merrSipasPunonjesveDhePeriudhes");
        }



        #endregion
        /// <summary>
        /// Metodat e meposhtme i perkasin veprimeve qe kryhen nga objektet clsKontrolliMjekesor dhe colKontrolliMjekesor
        /// </summary>        
        #region KONTROLLI MJEKESOR

        /// <summary>
        /// kthen datarow ore shtese sipas id-se
        /// </summary>
        ///<param name="id"> id e kokes</param>
        ///<returns> nje datarow qe permban  ore shtese me kete id</returns>
        internal DataRow merrKontrolliMjekesor(int id)
        {

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@ID", id, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KONTROLLIMJEKESOR_ktheSipasId");

            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];


        }





        /// <summary>
        /// ekzekuton prc_T_KONTROLLIMJEKESOR_ins ne DB dhe kthen statusin e perfundimit te ekzekutimit te SP-se perkatese
        /// Shiko clsMesazh brenda ketij moduli per informacion te metejshem mbi vleren e kthimit te kesaj metode
        /// <param name="id"> id ritese </param>
        /// <param name="orefillimi">ore fillimi </param>
        /// <param name="shenime">pershkrimi </param>
        /// <param name="koeficienti"> koeficienti</param>
        /// <param name="idPerdoruesi">id e perdoruesit qe ka kryer veprimin</param>
        /// <param name="idNdermarje">id ndermarje</param>
        /// <param name="idstatusdok">id e statusit </param>
        /// <returns> nje objekt clsMesazh qe tregon nese ruajtja eshte kryer ne rregull apo jo.</returns>
        /// </summary>

        internal clsMesazh ruajKontrolliMjekesor(out int id, int idpunonjesi, DateTime data, int idPerdoruesi, int idkrijuesi, int idNdermarje, int idstatusdok)
        {
            id = -1;

            dbManager.Open();
            clsMesazh mesazh = new clsMesazh();
            //shtimi i parametrave

            dbManager.CreateParameters(7);
            dbManager.AddParameters(0, "@ID", id, ParameterDirection.Output);
            if (data == new DateTime()) dbManager.AddParameters(1, "@data", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(1, "@data", data, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDPERDORUESI", idPerdoruesi, ParameterDirection.Input);
            dbManager.AddParameters(3, "@IDKRIJUESI", idkrijuesi, ParameterDirection.Input);
            dbManager.AddParameters(4, "@IDNDERMARJE", idNdermarje, ParameterDirection.Input);
            dbManager.AddParameters(5, "@IDSTATUSDOK", idstatusdok, ParameterDirection.Input);
            dbManager.AddParameters(6, "@idpunonjesi", idpunonjesi, ParameterDirection.Input);

            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_KONTROLLIMJEKESOR_ins");
            id = int.Parse(dbManager.Parameters[0].Value.ToString());
            mesazh = new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);

            return mesazh;

        }

        /// <summary>
        /// ekzekuton prc_T_KONTROLLIMJEKESOR_upd ne DB dhe kthen statusin e perfundimit te ekzekutimit te SP-se perkatese
        /// Shiko clsMesazh brenda ketij moduli per informacion te metejshem mbi vleren e kthimit te kesaj metode
        /// <param name="id"> id ritese </param>
        /// <param name="orefillimi">ora e fillimit </param>
        /// <param name="shenime">pershkrimi </param>
        /// <param name="koeficienti"> koeficienti</param>
        /// <param name="idPerdoruesi">id e perdoruesit qe ka kryer veprimin</param>
        /// <param name="idNdermarje">id ndermarje</param>
        /// <param name="idstatusdok">id e statusit </param>
        /// <returns> nje objekt clsMesazh qe tregon nese modifikimi eshte kryer ne rregull apo jo.</returns>
        /// </summary>
        internal clsMesazh modifikoKontrolliMjekesor(int id, int idpunonjesi, DateTime data, int idPerdoruesi, int idNdermarje, int idstatusdok)
        {

            dbManager.Open();
            clsMesazh mesazh = new clsMesazh();
            //shtimi i parametrave


            dbManager.CreateParameters(6);
            dbManager.AddParameters(0, "@ID", id, ParameterDirection.Input);
            if (data == new DateTime()) dbManager.AddParameters(1, "@data", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(1, "@data", data, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDPERDORUESI", idPerdoruesi, ParameterDirection.Input);
            dbManager.AddParameters(3, "@IDNDERMARJE", idNdermarje, ParameterDirection.Input);
            dbManager.AddParameters(4, "@IDSTATUSDOK", idstatusdok, ParameterDirection.Input);
            dbManager.AddParameters(5, "@idpunonjesi", idpunonjesi, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_KONTROLLIMJEKESOR_upd");
            mesazh = new clsMesazh(true, MessagesResource.Messages["msgModifikimiMeSukses"]);

            return mesazh;

        }

        /// <summary>
        /// ekzekuton prc_T_KONTROLLIMJEKESOR_upddel ne DB dhe kthen statusin e perfundimit te ekzekutimit te SP-se perkatese
        /// Shiko clsMesazh brenda ketij moduli per informacion te metejshem mbi vleren e kthimit te kesaj metode
        /// <param name="id"> id ritese </param>
        /// <param name="idPerdoruesi">id e perdoruesit qe ka kryer veprimin</param>
        /// <returns> nje objekt clsMesazh qe tregon nese fshirja eshte kryer ne rregull apo jo.</returns>
        /// </summary>

        internal clsMesazh fshiKontrolliMjekesor(int id, int idperdoruesi)
        {

            dbManager.Open();
            //shtimi i parametrave
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@ID", id, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUESI", idperdoruesi, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_KONTROLLIMJEKESOR_upddel");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
            return mesazh;

        }



        /// <summary>
        /// kthen Datatable konfigurim list orari sipas id ndermarjes
        /// </summary>
        ///<param name="idndermarje">id e ndermarjes</param>
        ///<returns>nje datatable qe permban nje koleksion me te gjitha konfigurimet e list orarit  te kesaj ndermarje </returns>

        internal DataTable ktheGjitheKontrolliMjekesorSipasNdermarrjes(int idndermarje)
        {

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDNDERMARJE", idndermarje, ParameterDirection.Input);

            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KONTROLLIMJEKESOR_merrSipasNdermarrjes");

            return ds.Tables[0];

        }
        internal DataTable merrKontrolliMjekesorPerExport(int idndermarje)
        {

            dbManager.Open();
            string salt = WebConfigurationManager.AppSettings["salt"];
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDNDERMARJE", idndermarje, ParameterDirection.Input);
            dbManager.AddParameters(1, "@salt", salt, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KONTROLLIMJEKESOR_merrPerExport");

            return ds.Tables[0];

        }

        internal DataTable ktheGjitheKontrolliMjekesorSipasPunonjesit(int idpunonjes)
        {

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDPUNONJESI", idpunonjes, ParameterDirection.Input);

            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KONTROLLIMJEKESOR_merrSipasPunonjesit");

            return ds.Tables[0];

        }




        #endregion
        /// <summary>
        /// Metodat e meposhtme i perkasin veprimeve qe kryhen nga objektet clsProfesioneTitujPune dhe colProfesioneTitujPune
        /// </summary>
        #region PROFESIONE TITUJ PUNE

        /// <summary>
        /// ekzekuton 	[prc_T_ProfesioneTitujPune_ins] ne DB dhe kthen statusin e perfundimit te ekzekutimit te SP-se perkatese
        /// Shiko clsMesazh brenda ketij moduli per informacion te metejshem mbi vleren e kthimit te kesaj metode
        /// </summary>
        /// <param name="idProfesioneTitujPune"> id e ProfesioneTitujPuneit</param>
        /// <param name="kodi">kodi</param>
        /// <param name="pershkrimi">pershkrimi</param>
        /// <param name="pershkrimiang">kostoja e planifikuar</param>
        /// <param name="idkrijuesi">idllog</param>
        /// <param name="aktiv"> aktive</param>
        /// <param name="lloji">tipi makineri, mjete,punonjes</param>
        /// <param name="idkonfig">id e konfigurimit</param>
        /// <param name="idPerdoruesi">id e perdoruesit</param>
        /// <param name="idnderm">id e ndermarjes</param>
        /// <param name="idstatusdok">id e statusdok</param>
        /// <returns>nje objekt clsMesazh qe tregon nese ruajtja eshte kryer ne rregull apo jo.</returns>
        /// <seealso cref="cs"/>
        internal clsMesazh ruajProfesioneTitujPune(out int idProfesioneTitujPune, string kodi, string pershkrimi, int lloji, string pershkrimiang, int idkrijuesi, bool aktiv, int idkonfig, int idPerdoruesi, int idnderm, int idstatusdok)
        {
            idProfesioneTitujPune = -1;

            dbManager.Open();
            dbManager.CreateParameters(11);
            dbManager.AddParameters(0, "@ID", idProfesioneTitujPune, ParameterDirection.Output);
            dbManager.AddParameters(1, "@KODI", kodi, ParameterDirection.Input);
            dbManager.AddParameters(2, "@PERSHKRIMI", pershkrimi, ParameterDirection.Input);
            dbManager.AddParameters(3, "@LLOJI", lloji, ParameterDirection.Input);
            dbManager.AddParameters(4, "@PERSHKRIMIANG", pershkrimiang, ParameterDirection.Input);
            dbManager.AddParameters(5, "@AKTIV", aktiv, ParameterDirection.Input);
            dbManager.AddParameters(6, "@IDKRIJUESI", idkrijuesi, ParameterDirection.Input);
            dbManager.AddParameters(7, "@IDKONFIG", idkonfig, ParameterDirection.Input);
            dbManager.AddParameters(8, "@IDPERDORUESI", idPerdoruesi, ParameterDirection.Input);
            dbManager.AddParameters(9, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            dbManager.AddParameters(10, "@IDSTATUSDOK", idstatusdok, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_PROFESIONETITUJPUNE_ins");
            idProfesioneTitujPune = int.Parse(dbManager.Parameters[0].Value.ToString());
            return new clsMesazh(true, mesazhRuajtje);

        }

        /// <summary>
        /// ekzekuton prc_T_ProfesioneTitujPune_upd ne DB dhe kthen statusin e perfundimit te ekzekutimit te SP-se perkatese
        /// Shiko clsMesazh brenda ketij moduli per informacion te metejshem mbi vleren e kthimit te kesaj metode
        /// </summary>
        /// <param name="idProfesioneTitujPune"> id e ProfesioneTitujPuneit</param>
        /// <param name="kodi">kodi</param>
        /// <param name="pershkrimi">pershkrimi</param>
        /// <param name="pershkrimiang">kostoja e planifikuar</param>
        /// <param name="idllog">idllog</param>
        /// <param name="aktiv"> aktive</param>
        /// <param name="lloji">tipi makineri, mjete,punonjes</param>
        /// <param name="idkonfig">id e konfigurimit</param>
        /// <param name="idPerdoruesi">id e perdoruesit</param>
        /// <param name="idnderm">id e ndermarjes</param>
        /// <param name="idstatusdok">id e statusdok</param>
        /// <returns> nje objekt clsMesazh qe tregon nese modifikimi eshte kryer ne rregull apo jo.</returns>
        internal clsMesazh modifikoProfesioneTitujPune(int idProfesioneTitujPune, string kodi, string pershkrimi, int lloji, string pershkrimiang, bool aktiv, int idkonfig, int idPerdoruesi, int idnderm, int idstatusdok)
        {

            dbManager.Open();
            dbManager.CreateParameters(10);
            dbManager.AddParameters(0, "@ID", idProfesioneTitujPune, ParameterDirection.Input);
            dbManager.AddParameters(1, "@KODI", kodi, ParameterDirection.Input);
            dbManager.AddParameters(2, "@PERSHKRIMI", pershkrimi, ParameterDirection.Input);
            dbManager.AddParameters(3, "@LLOJI", lloji, ParameterDirection.Input);
            dbManager.AddParameters(4, "@PERSHKRIMIANG", pershkrimiang, ParameterDirection.Input);
            dbManager.AddParameters(5, "@AKTIV", aktiv, ParameterDirection.Input);
            dbManager.AddParameters(6, "@IDKONFIG", idkonfig, ParameterDirection.Input);
            dbManager.AddParameters(7, "@IDPERDORUESI", idPerdoruesi, ParameterDirection.Input);
            dbManager.AddParameters(8, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            dbManager.AddParameters(9, "@IDSTATUSDOK", idstatusdok, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_PROFESIONETITUJPUNE_upd");
            return new clsMesazh(true, mesazhModifikimi);

        }

        /// <summary>
        /// ekzekutohet sp-ja prc_T_ProfesioneTitujPune_del duke i kaluar id e ProfesioneTitujPuneit
        /// </summary>
        /// <param name="id">id e ProfesioneTitujPuneit qe do fshihet </param>
        /// <returns>nje objekt clsMesazh qe tregon nese fshirja eshte kryer ne rregull apo jo</returns>
        internal clsMesazh fshiProfesioneTitujPune(int id)
        {

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@ID", id, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_PROFESIONETITUJPUNE_del");
            clsMesazh mesazh = new clsMesazh(true, mesazhFshirje);
            return mesazh;

        }

        /// <summary>
        /// fshin ProfesioneTitujPunein duke ndryshuar statusin e saj ne te fshire
        /// </summary>
        /// <param name="id">id e ProfesioneTitujPuneit</param>
        /// <param name="idperdoruesi"> id e perdoruesit qe ka kryer veprimin</param>
        /// <returns> clsMesazh qe tregon nese fshirja u krye me sukses apo jo</returns>
        internal clsMesazh fshiProfesioneTitujPuneStatus(int id, int idperdoruesi)
        {

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@ID", id, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUESI", idperdoruesi, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_PROFESIONETITUJPUNE_upddel");
            clsMesazh mesazh = new clsMesazh(true, mesazhFshirje);
            return mesazh;

        }

        /// <summary>
        /// merr ProfesioneTitujPunein sipas id
        /// </summary>
        /// <param name="id">  id e ProfesioneTitujPuneit</param>
        /// <returns> kthen datarow qe permban ProfesioneTitujPunein me kete id</returns>
        internal DataRow ktheProfesioneTitujPune(int id)
        {

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@ID", id, ParameterDirection.Input);
            using (DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_PROFESIONETITUJPUNE_merrSipasId"))
            {
                if (ds == null)
                    return null;
                if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                    return null;
                return ds.Tables[0].Rows[0];
            }

        }

        /// <summary>
        /// merr gjithe ProfesioneTitujPunet te nje ndermarje
        /// </summary>
        /// <param name="idnder"> id e ndermarjes</param>
        /// <returns> nje datatable qe permban nje koleksion me te gjithe ProfesioneTitujPunet te ndermarjes</returns>
        internal DataTable ktheGjitheProfesioneTitujPunetSipasNdermarjesDheLlojit(int idnder, int lloji)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDNDERMARJE", idnder, ParameterDirection.Input);
            dbManager.AddParameters(1, "@LLOJI", lloji, ParameterDirection.Input);
            using (DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_PROFESIONETITUJPUNE_merrGjitheSipasLlojit"))
            {
                return ds.Tables[0];
            }
        }

        /// <summary>
        /// merr gjithe ProfesioneTitujPunet te nje ndermarje  aktiv
        /// </summary>
        /// <param name="idnder"> id e ndermarjes</param>
        /// <returns> nje datatable qe permban nje koleksion me te gjithe ProfesioneTitujPunet te ndermarjes</returns>
        internal DataTable ktheGjitheProfesioneTitujPunetSipasNdermarjesDheLlojitAktiv(int idnder, int lloji)
        {

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDNDERMARJE", idnder, ParameterDirection.Input);
            dbManager.AddParameters(1, "@LLOJI", lloji, ParameterDirection.Input);

            using (DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_PROFESIONETITUJPUNE_merrGjitheSipasLlojitAktive"))
            {
                return ds.Tables[0];
            }

        }

        /// <summary>
        /// merr ProfesioneTitujPunet te nje ndermarje me kete kod
        /// </summary>
        /// <param name="kodi"> kodi i ProfesioneTitujPuneit</param>
        /// <param name="idnder"> id e ndermarjes</param>
        /// <returns>  data row me ProfesioneTitujPunein te nje ndermarje me kete kod</returns>
        internal DataRow ktheProfesioneTitujPuneSipasKodit(string kodi, int idnder, int lloji)
        {

            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@KODI", kodi, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idnder, ParameterDirection.Input);
            dbManager.AddParameters(2, "@LLOJI", lloji, ParameterDirection.Input);
            using (DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_PROFESIONETITUJPUNE_ktheSipasKodi"))
            {
                if (ds == null)
                    return null;
                if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                    return null;
                return ds.Tables[0].Rows[0];
            }

        }
        internal DataRow ktheProfesioneTitujPuneSipasPershkrimit(string pershkrimi, int idnder, int lloji, int idgjuha)
        {

            dbManager.Open();
            dbManager.CreateParameters(4);
            dbManager.AddParameters(0, "@pershkrimi", pershkrimi, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idnder, ParameterDirection.Input);
            dbManager.AddParameters(2, "@LLOJI", lloji, ParameterDirection.Input);
            dbManager.AddParameters(3, "@idgjuha", idgjuha, ParameterDirection.Input);
            using (DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_PROFESIONETITUJPUNE_ktheSipasPershkrimi"))
            {
                if (ds == null)
                    return null;
                if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                    return null;
                return ds.Tables[0].Rows[0];
            }

        }
        /// <summary>
        /// perdoret per te kontrolluar nqs ekziston nje ProfesioneTitujPune me kete kod 
        /// behet kontrolli per qellime konsistence te informacionit ne DB, nuk lejon te celen ProfesioneTitujPune te ndryshem me te njejtin kod
        /// </summary>
        /// <param name="kodi">kodi i ProfesioneTitujPuneit</param>
        /// <param name="idNdermarje"> id e ndermarjes</param>
        /// <returns>nje objeckt boolean qe tregon nese ekziston apo jo nje ProfesioneTitujPune me kete kod</returns>
        public bool ekzistonProfesioneTitujPune(string kodi, int idNdermarje, int lloji)
        {

            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@KODI", kodi, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idNdermarje, ParameterDirection.Input);
            dbManager.AddParameters(2, "@LLOJI", lloji, ParameterDirection.Input);
            using (DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_PROFESIONETITUJPUNE_ekzistonKod"))
            {
                if (ds.Tables[0].Rows.Count == 1)
                    return true;
                if (ds.Tables[0].Rows.Count == 0)
                    return false;
                return true;
            }
        }
        public bool ekzistonProfesioneTitujPunePershkrimi(string pershkrimi, int idNdermarje, int lloji, int id)
        {

            dbManager.Open();
            dbManager.CreateParameters(4);
            dbManager.AddParameters("@pershkrimi", pershkrimi, ParameterDirection.Input);
            dbManager.AddParameters("@IDNDERMARJE", idNdermarje, ParameterDirection.Input);
            dbManager.AddParameters("@LLOJI", lloji, ParameterDirection.Input);
            dbManager.AddParameters("@id", id, ParameterDirection.Input);
            return Convert.ToInt32(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_PROFESIONETITUJPUNE_ekzistonPershkrim")) > 0;

        }
        public bool ekzistonProfesioneTitujPunePershkrimiAng(string pershkrimi, int idNdermarje, int lloji, int id)
        {

            dbManager.Open();
            dbManager.CreateParameters(4);
            dbManager.AddParameters(0, "@pershkrimi", pershkrimi, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idNdermarje, ParameterDirection.Input);
            dbManager.AddParameters(2, "@LLOJI", lloji, ParameterDirection.Input);
            dbManager.AddParameters(3, "@id", id, ParameterDirection.Input);
            using (DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_PROFESIONETITUJPUNE_ekzistonPershkrimAng"))
            {
                if (ds.Tables[0].Rows.Count == 1)
                    return true;
                if (ds.Tables[0].Rows.Count == 0)
                    return false;
                return true;
            }
        }

        /// <summary>
        /// merr  ProfesioneTitujPunein sipas id ne forme data row
        /// </summary>
        /// <param name="idProfesioneTitujPune"> id ProfesioneTitujPune</param>
        /// <returns> kthen data row me kete ProfesioneTitujPune</returns>
        internal DataRow merrProfesioneTitujPuneSipasIdDR(int idProfesioneTitujPune)
        {

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@ID", idProfesioneTitujPune, ParameterDirection.Input);
            using (DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_PROFESIONETITUJPUNE_merrSipasIdDR"))
            {
                if (ds == null)
                    return null;
                if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                    return null;
                return ds.Tables[0].Rows[0];
            }

        }

        /// <summary>
        /// merr gjithe ProfesioneTitujPunet sipas ndermarjes ne forme data table
        /// </summary>
        /// <param name="idnderm">id ndermarje</param>
        /// <returns> kthen nje datatable me te gjithe ProfesioneTitujPunet e kesaj ndermarje</returns>
        internal DataTable merrProfesioneTitujPuneDT(int idnderm, int lloji)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            dbManager.AddParameters(1, "@LLOJI", lloji, ParameterDirection.Input);
            using (DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_PROFESIONETITUJPUNE_merrSipaSNdermarrjesDheLlojitDT"))
            {
                return ds.Tables[0];
            }

        }

        /// <summary>
        /// merr gjithe ProfesioneTitujPunet sipas ndermarjes aktive ne forme data table
        /// </summary>
        /// <param name="idnderm">id ndermarje</param>
        /// <returns> kthen nje datatable me te gjithe ProfesioneTitujPunet aktive e kesaj ndermarje</returns>
        internal DataTable merrProfesioneTitujPuneDTAktive(int idnderm, int lloji)
        {

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            dbManager.AddParameters(1, "@LLOJI", lloji, ParameterDirection.Input);
            using (DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_PROFESIONETITUJPUNE_merrSipasNdermarjeDheLlojiPerLupe"))
            {
                return ds.Tables[0];
            }
        }

        public bool kaVeprimeProfesione(int id)
        {//kontrollon nqs ka veprime me kete kodifikim artikulli

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@ID", id, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_PROFESIONETITUJPUNE_kaVeprime");
            if (ds.Tables[0].Rows.Count == 1)
                return true;
            if (ds.Tables[0].Rows.Count == 0)
                return false;
            return true;
        }


        #endregion

        /// <summary>
        /// Metodat e meposhtme i perkasin veprimeve qe kryhen nga objektet clsVendndodhjet dhe colVendndodhjet
        /// </summary>
        #region VENDNDODHJET

        /// <summary>
        /// ekzekuton 	[prc_T_Vendndodhjet_ins] ne DB dhe kthen statusin e perfundimit te ekzekutimit te SP-se perkatese
        /// Shiko clsMesazh brenda ketij moduli per informacion te metejshem mbi vleren e kthimit te kesaj metode
        /// </summary>
        /// <param name="id"> id e ProfesioneTitujPuneit</param>
        /// <param name="kodi">kodi</param>
        /// <param name="pershkrimi">pershkrimi</param>
        /// <param name="pershkrimiang">kostoja e planifikuar</param>
        /// <param name="idkrijuesi">idllog</param>
        /// <param name="aktiv"> aktive</param>
        /// <param name="lloji">tipi makineri, mjete,punonjes</param>
        /// <param name="idkonfig">id e konfigurimit</param>
        /// <param name="idPerdoruesi">id e perdoruesit</param>
        /// <param name="idnderm">id e ndermarjes</param>
        /// <param name="idstatusdok">id e statusdok</param>
        /// <returns>nje objekt clsMesazh qe tregon nese ruajtja eshte kryer ne rregull apo jo.</returns>
        /// <seealso cref="cs"/>
        internal clsMesazh ruajVendndodhjet(out int id, string kodi, string pershkrimi, int idkrijuesi, bool aktiv, int idkonfig, int idPerdoruesi, int idnderm, int idstatusdok)
        {
            id = -1;

            string salt = WebConfigurationManager.AppSettings["salt"];
            dbManager.Open();
            dbManager.CreateParameters(10);
            dbManager.AddParameters(0, "@ID", id, ParameterDirection.Output);
            dbManager.AddParameters(1, "@KODI", kodi, ParameterDirection.Input);
            dbManager.AddParameters(2, "@PERSHKRIMI", pershkrimi, ParameterDirection.Input);
            dbManager.AddParameters(3, "@AKTIV", aktiv, ParameterDirection.Input);
            dbManager.AddParameters(4, "@IDKRIJUESI", idkrijuesi, ParameterDirection.Input);
            dbManager.AddParameters(5, "@IDKONFIG", idkonfig, ParameterDirection.Input);
            dbManager.AddParameters(6, "@IDPERDORUESI", idPerdoruesi, ParameterDirection.Input);
            dbManager.AddParameters(7, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            dbManager.AddParameters(8, "@IDSTATUSDOK", idstatusdok, ParameterDirection.Input);
            dbManager.AddParameters(9, "@salt", salt, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_VENDNDODHJET_ins");
            id = int.Parse(dbManager.Parameters[0].Value.ToString());
            return new clsMesazh(true, mesazhRuajtje);

        }

        /// <summary>
        /// ekzekuton prc_T_Vendndodhjet_upd ne DB dhe kthen statusin e perfundimit te ekzekutimit te SP-se perkatese
        /// Shiko clsMesazh brenda ketij moduli per informacion te metejshem mbi vleren e kthimit te kesaj metode
        /// </summary>
        /// <param name="id"> id e ProfesioneTitujPuneit</param>
        /// <param name="kodi">kodi</param>
        /// <param name="pershkrimi">pershkrimi</param>
        /// <param name="pershkrimiang">kostoja e planifikuar</param>
        /// <param name="idllog">idllog</param>
        /// <param name="aktiv"> aktive</param>
        /// <param name="lloji">tipi makineri, mjete,punonjes</param>
        /// <param name="idkonfig">id e konfigurimit</param>
        /// <param name="idPerdoruesi">id e perdoruesit</param>
        /// <param name="idnderm">id e ndermarjes</param>
        /// <param name="idstatusdok">id e statusdok</param>
        /// <returns> nje objekt clsMesazh qe tregon nese modifikimi eshte kryer ne rregull apo jo.</returns>
        internal clsMesazh modifikoVendndodhjet(int id, string kodi, string pershkrimi, bool aktiv, int idkonfig, int idPerdoruesi, int idnderm, int idstatusdok)
        {

            string salt = WebConfigurationManager.AppSettings["salt"];
            dbManager.Open();
            dbManager.CreateParameters(9);
            dbManager.AddParameters(0, "@ID", id, ParameterDirection.Input);
            dbManager.AddParameters(1, "@KODI", kodi, ParameterDirection.Input);
            dbManager.AddParameters(2, "@PERSHKRIMI", pershkrimi, ParameterDirection.Input);
            dbManager.AddParameters(3, "@AKTIV", aktiv, ParameterDirection.Input);
            dbManager.AddParameters(4, "@IDKONFIG", idkonfig, ParameterDirection.Input);
            dbManager.AddParameters(5, "@IDPERDORUESI", idPerdoruesi, ParameterDirection.Input);
            dbManager.AddParameters(6, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            dbManager.AddParameters(7, "@IDSTATUSDOK", idstatusdok, ParameterDirection.Input);
            dbManager.AddParameters(8, "@salt", salt, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_VENDNDODHJET_upd");
            return new clsMesazh(true, mesazhModifikimi);

        }

        /// <summary>
        /// ekzekutohet sp-ja prc_T_Vendndodhjet_del duke i kaluar id 
        /// </summary>
        /// <param name="idProfesioneTitujPune">id e ProfesioneTitujPuneit qe do fshihet </param>
        /// <returns>nje objekt clsMesazh qe tregon nese fshirja eshte kryer ne rregull apo jo</returns>
        internal clsMesazh fshiVendndodhjet(int id)
        {

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@ID", id, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_VENDNDODHJET_del");
            clsMesazh mesazh = new clsMesazh(true, mesazhFshirje);
            return mesazh;

        }

        /// <summary>
        /// fshin vendndodhjet duke ndryshuar statusin e saj ne te fshire
        /// </summary>
        /// <param name="idProfesioneTitujPune">id e ProfesioneTitujPuneit</param>
        /// <param name="idperdoruesi"> id e perdoruesit qe ka kryer veprimin</param>
        /// <returns> clsMesazh qe tregon nese fshirja u krye me sukses apo jo</returns>
        internal clsMesazh fshiVendndodhjetStatus(int id, int idperdoruesi)
        {

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@ID", id, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUESI", idperdoruesi, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_VENDNDODHJET_upddel");
            clsMesazh mesazh = new clsMesazh(true, mesazhFshirje);
            return mesazh;

        }

        /// <summary>
        /// merr vendndodhjet sipas id
        /// </summary>
        /// <param name="idProfesioneTitujPune">  id e ProfesioneTitujPuneit</param>
        /// <returns> kthen datarow qe permban ProfesioneTitujPunein me kete id</returns>
        internal DataRow ktheVendndodhjet(int id)
        {

            string salt = WebConfigurationManager.AppSettings["salt"];
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@ID", id, ParameterDirection.Input);
            dbManager.AddParameters(1, "@salt", salt, ParameterDirection.Input);
            using (DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_VENDNDODHJET_merrSipasId"))
            {
                if (ds == null)
                    return null;
                if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                    return null;
                return ds.Tables[0].Rows[0];
            }

        }

        /// <summary>
        /// merr gjithe vendndodhjet te nje ndermarje
        /// </summary>
        /// <param name="idnder"> id e ndermarjes</param>
        /// <returns> nje datatable qe permban nje koleksion me te gjithe ProfesioneTitujPunet te ndermarjes</returns>
        internal DataTable ktheGjitheVendndodhjetSipasNdermarjes(int idnder)
        {

            string salt = WebConfigurationManager.AppSettings["salt"];
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDNDERMARJE", idnder, ParameterDirection.Input);
            dbManager.AddParameters(1, "@salt", salt, ParameterDirection.Input);
            using (DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_VENDNDODHJET_merrGjithe"))
            {
                return ds.Tables[0];
            }

        }

        /// <summary>
        /// merr gjithe vendndodhjet te nje ndermarje  aktiv
        /// </summary>
        /// <param name="idnder"> id e ndermarjes</param>
        /// <returns> nje datatable qe permban nje koleksion me te gjithe ProfesioneTitujPunet te ndermarjes</returns>
        internal DataTable ktheGjitheVendndodhjetSipasNdermarjesAktiv(int idnder)
        {

            string salt = WebConfigurationManager.AppSettings["salt"];
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDNDERMARJE", idnder, ParameterDirection.Input);
            dbManager.AddParameters(1, "@salt", salt, ParameterDirection.Input);
            using (DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_VENDNDODHJET_merrGjitheSipasAktive"))
            {
                return ds.Tables[0];
            }

        }

        /// <summary>
        /// merr vendndodhjet te nje ndermarje me kete kod
        /// </summary>
        /// <param name="kodi"> kodi i ProfesioneTitujPuneit</param>
        /// <param name="idnder"> id e ndermarjes</param>
        /// <returns>  data row me ProfesioneTitujPunein te nje ndermarje me kete kod</returns>
        internal DataRow ktheVendndodhjetSipasKodit(string kodi, int idnder)
        {

            string salt = WebConfigurationManager.AppSettings["salt"];
            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@KODI", kodi, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idnder, ParameterDirection.Input);
            dbManager.AddParameters(2, "@salt", salt, ParameterDirection.Input);
            using (DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_VENDNDODHJET_ktheSipasKodi"))
            {
                if (ds == null)
                    return null;
                if (ds.Tables[0].Rows.Count == 0)
                    return null;

                if (ds.Tables[0].Rows.Count > 1)
                    throw new MyException($"Ekziston me teper se nje vendndodhje me kodin {kodi}");

                return ds.Tables[0].Rows[0];
            }

        }
        internal DataRow ktheVendndodhjetSipasPershkrimi(string pershkrimi, int idnder)
        {
            string salt = WebConfigurationManager.AppSettings["salt"];
            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@pershkrimi", pershkrimi, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idnder, ParameterDirection.Input);
            dbManager.AddParameters(2, "@salt", salt, ParameterDirection.Input);
            using (DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_VENDNDODHJET_ktheSipasPershkrimit"))
            {
                if (ds == null)
                    return null;
                if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                    return null;
                return ds.Tables[0].Rows[0];
            }
        }

        /// <summary>
        /// perdoret per te kontrolluar nqs ekziston nje vendndodhjet me kete kod 
        /// behet kontrolli per qellime konsistence te informacionit ne DB, nuk lejon te celen vendndodhjet te ndryshem me te njejtin kod
        /// </summary>
        /// <param name="kodi">kodi i ProfesioneTitujPuneit</param>
        /// <param name="idNdermarje"> id e ndermarjes</param>
        /// <returns>nje objeckt boolean qe tregon nese ekziston apo jo nje vendndodhjet me kete kod</returns>
        public bool ekzistonVendndodhjet(string kodi, int idNdermarje)
        {
            string salt = WebConfigurationManager.AppSettings["salt"];
            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@KODI", kodi, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idNdermarje, ParameterDirection.Input);
            dbManager.AddParameters(2, "@salt", salt, ParameterDirection.Input);
            using (DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_VENDNDODHJET_ekzistonKod"))
            {
                if (ds.Tables[0].Rows.Count == 1)
                    return true;
                if (ds.Tables[0].Rows.Count == 0)
                    return false;
                return true;
            }
        }

        /// <summary>
        /// merr  vendndodhjet sipas id ne forme data row
        /// </summary>
        /// <param name="id"> id ProfesioneTitujPune</param>
        /// <returns> kthen data row me kete ProfesioneTitujPune</returns>
        internal DataRow merrVendndodhjetSipasIdDR(int id)
        {
            string salt = WebConfigurationManager.AppSettings["salt"];
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@ID", id, ParameterDirection.Input);
            dbManager.AddParameters(1, "@salt", salt, ParameterDirection.Input);
            using (DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_VENDNDODHJET_merrSipasIdDR"))
            {
                if (ds == null)
                    return null;
                if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                    return null;
                return ds.Tables[0].Rows[0];
            }
        }

        /// <summary>
        /// merr gjithe vendndodhjet sipas ndermarjes ne forme data table
        /// </summary>
        /// <param name="idnderm">id ndermarje</param>
        /// <returns> kthen nje datatable me te gjithe vendndodhjet e kesaj ndermarje</returns>
        internal DataTable merrVendndodhjetDT(int idnderm)
        {
            string salt = WebConfigurationManager.AppSettings["salt"];
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            dbManager.AddParameters(1, "@salt", salt, ParameterDirection.Input);
            using (DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_VENDNDODHJET_merrSipaSNdermarrjesDT"))
            {
                return ds.Tables[0];
            }
        }

        /// <summary>
        /// merr gjithe vendndodhjet sipas ndermarjes aktive ne forme data table
        /// </summary>
        /// <param name="idnderm">id ndermarje</param>
        /// <returns> kthen nje datatable me te gjithe ProfesioneTitujPunet aktive e kesaj ndermarje</returns>
        internal DataTable merrVendndodhjetDTAktive(int idnderm)
        {
            string salt = WebConfigurationManager.AppSettings["salt"];
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            dbManager.AddParameters(1, "@salt", salt, ParameterDirection.Input);
            using (DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_VENDNDODHJET_merrSipasNdermarjePerLupe"))
            {
                return ds.Tables[0];
            }
        }

        public bool kaVeprimeVendndodhjet(int id)
        {//kontrollon nqs ka veprime me kete kodifikim artikulli

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@ID", id, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_VENDNDODHJET_kaVeprime");
            if (ds.Tables[0].Rows.Count == 1)
                return true;
            if (ds.Tables[0].Rows.Count == 0)
                return false;
            return true;
        }

        #endregion

        /// <summary>
        /// Metodat e meposhtme i perkasin veprimeve qe kryhen nga objektet clsKodeProfesione dhe colKodeProfesione
        /// </summary>
        #region KODEPROFESIONE

        /// <summary>
        /// ekzekuton 	[prc_T_KodeProfesione_ins] ne DB dhe kthen statusin e perfundimit te ekzekutimit te SP-se perkatese
        /// Shiko clsMesazh brenda ketij moduli per informacion te metejshem mbi vleren e kthimit te kesaj metode
        /// </summary>
        /// <param name="id"> id e ProfesioneTitujPuneit</param>
        /// <param name="kodi">kodi</param>
        /// <param name="pershkrimi">pershkrimi</param>
        /// <param name="pershkrimiang">kostoja e planifikuar</param>
        /// <param name="idkrijuesi">idllog</param>
        /// <param name="aktiv"> aktive</param>
        /// <param name="lloji">tipi makineri, mjete,punonjes</param>
        /// <param name="idkonfig">id e konfigurimit</param>
        /// <param name="idPerdoruesi">id e perdoruesit</param>
        /// <param name="idnderm">id e ndermarjes</param>
        /// <param name="idstatusdok">id e statusdok</param>
        /// <returns>nje objekt clsMesazh qe tregon nese ruajtja eshte kryer ne rregull apo jo.</returns>
        /// <seealso cref="cs"/>
        internal clsMesazh ruajKodeProfesione(out int id, string kodi, string pershkrimi, int idkrijuesi, bool aktiv, int idPerdoruesi, int idnderm, int idstatusdok)
        {
            id = -1;

            dbManager.Open();
            dbManager.CreateParameters(8);
            dbManager.AddParameters(0, "@ID", id, ParameterDirection.Output);
            dbManager.AddParameters(1, "@KODI", kodi, ParameterDirection.Input);
            dbManager.AddParameters(2, "@PERSHKRIMI", pershkrimi, ParameterDirection.Input);
            dbManager.AddParameters(3, "@AKTIV", aktiv, ParameterDirection.Input);
            dbManager.AddParameters(4, "@IDKRIJUESI", idkrijuesi, ParameterDirection.Input);
            dbManager.AddParameters(5, "@IDPERDORUESI", idPerdoruesi, ParameterDirection.Input);
            dbManager.AddParameters(6, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            dbManager.AddParameters(7, "@IDSTATUSDOK", idstatusdok, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_KODEPROFESIONE_ins");
            id = int.Parse(dbManager.Parameters[0].Value.ToString());
            return new clsMesazh(true, mesazhRuajtje);
        }

        /// <summary>
        /// ekzekuton prc_T_KodeProfesione_upd ne DB dhe kthen statusin e perfundimit te ekzekutimit te SP-se perkatese
        /// Shiko clsMesazh brenda ketij moduli per informacion te metejshem mbi vleren e kthimit te kesaj metode
        /// </summary>
        /// <param name="id"> id e ProfesioneTitujPuneit</param>
        /// <param name="kodi">kodi</param>
        /// <param name="pershkrimi">pershkrimi</param>
        /// <param name="pershkrimiang">kostoja e planifikuar</param>
        /// <param name="idllog">idllog</param>
        /// <param name="aktiv"> aktive</param>
        /// <param name="lloji">tipi makineri, mjete,punonjes</param>
        /// <param name="idkonfig">id e konfigurimit</param>
        /// <param name="idPerdoruesi">id e perdoruesit</param>
        /// <param name="idnderm">id e ndermarjes</param>
        /// <param name="idstatusdok">id e statusdok</param>
        /// <returns> nje objekt clsMesazh qe tregon nese modifikimi eshte kryer ne rregull apo jo.</returns>
        internal clsMesazh modifikoKodeProfesione(int id, string kodi, string pershkrimi, bool aktiv, int idPerdoruesi, int idnderm, int idstatusdok)
        {

            dbManager.Open();
            dbManager.CreateParameters(7);
            dbManager.AddParameters(0, "@ID", id, ParameterDirection.Input);
            dbManager.AddParameters(1, "@KODI", kodi, ParameterDirection.Input);
            dbManager.AddParameters(2, "@PERSHKRIMI", pershkrimi, ParameterDirection.Input);
            dbManager.AddParameters(3, "@AKTIV", aktiv, ParameterDirection.Input);
            dbManager.AddParameters(4, "@IDPERDORUESI", idPerdoruesi, ParameterDirection.Input);
            dbManager.AddParameters(5, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            dbManager.AddParameters(6, "@IDSTATUSDOK", idstatusdok, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_KodeProfesione_upd");
            return new clsMesazh(true, mesazhModifikimi);
        }

        /// <summary>
        /// ekzekutohet sp-ja prc_T_KodeProfesione_del duke i kaluar id 
        /// </summary>
        /// <param name="idProfesioneTitujPune">id e ProfesioneTitujPuneit qe do fshihet </param>
        /// <returns>nje objekt clsMesazh qe tregon nese fshirja eshte kryer ne rregull apo jo</returns>
        internal clsMesazh fshiKodeProfesione(int id)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@ID", id, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_KodeProfesione_del");
            clsMesazh mesazh = new clsMesazh(true, mesazhFshirje);
            return mesazh;
        }

        /// <summary>
        /// fshin vendndodhjet duke ndryshuar statusin e saj ne te fshire
        /// </summary>
        /// <param name="idProfesioneTitujPune">id e ProfesioneTitujPuneit</param>
        /// <param name="idperdoruesi"> id e perdoruesit qe ka kryer veprimin</param>
        /// <returns> clsMesazh qe tregon nese fshirja u krye me sukses apo jo</returns>
        internal clsMesazh fshiKodeProfesioneStatus(int id, int idperdoruesi)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@ID", id, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUESI", idperdoruesi, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_KodeProfesione_upddel");
            clsMesazh mesazh = new clsMesazh(true, mesazhFshirje);
            return mesazh;
        }

        /// <summary>
        /// merr vendndodhjet sipas id
        /// </summary>
        /// <param name="idProfesioneTitujPune">  id e ProfesioneTitujPuneit</param>
        /// <returns> kthen datarow qe permban ProfesioneTitujPunein me kete id</returns>
        internal DataRow ktheKodeProfesione(int id)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@ID", id, ParameterDirection.Input);
            using (DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KodeProfesione_merrSipasId"))
            {
                if (ds == null)
                    return null;
                if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                    return null;
                return ds.Tables[0].Rows[0];
            }
        }

        /// <summary>
        /// merr gjithe vendndodhjet te nje ndermarje
        /// </summary>
        /// <param name="idnder"> id e ndermarjes</param>
        /// <returns> nje datatable qe permban nje koleksion me te gjithe ProfesioneTitujPunet te ndermarjes</returns>
        internal DataTable ktheGjitheKodeProfesioneSipasNdermarjes(int idnder)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDNDERMARJE", idnder, ParameterDirection.Input);
            using (DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KodeProfesione_merrGjithe"))
            {
                return ds.Tables[0];
            }
        }

        /// <summary>
        /// merr gjithe vendndodhjet te nje ndermarje  aktiv
        /// </summary>
        /// <param name="idnder"> id e ndermarjes</param>
        /// <returns> nje datatable qe permban nje koleksion me te gjithe ProfesioneTitujPunet te ndermarjes</returns>
        internal DataTable ktheGjitheKodeProfesioneSipasNdermarjesAktiv(int idnder)
        {

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDNDERMARJE", idnder, ParameterDirection.Input);

            using (DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KodeProfesione_merrGjitheSipasAktive"))
            {
                return ds.Tables[0];
            }
        }

        /// <summary>
        /// merr vendndodhjet te nje ndermarje me kete kod
        /// </summary>
        /// <param name="kodi"> kodi i ProfesioneTitujPuneit</param>
        /// <param name="idnder"> id e ndermarjes</param>
        /// <returns>  data row me ProfesioneTitujPunein te nje ndermarje me kete kod</returns>
        internal DataRow ktheKodeProfesioneSipasKodit(string kodi, int idnder)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@KODI", kodi, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idnder, ParameterDirection.Input);
            using (DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KodeProfesione_ktheSipasKodi"))
            {
                if (ds == null)
                    return null;
                if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                    return null;
                return ds.Tables[0].Rows[0];
            }
        }
        internal DataRow ktheKodeProfesioneSipasPershkrimi(string pershkrimi, int idnder)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@pershkrimi", pershkrimi, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idnder, ParameterDirection.Input);
            using (DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KodeProfesione_ktheSipasPershkrimit"))
            {
                if (ds == null)
                    return null;
                if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                    return null;
                return ds.Tables[0].Rows[0];
            }
        }

        /// <summary>
        /// perdoret per te kontrolluar nqs ekziston nje vendndodhjet me kete kod 
        /// behet kontrolli per qellime konsistence te informacionit ne DB, nuk lejon te celen vendndodhjet te ndryshem me te njejtin kod
        /// </summary>
        /// <param name="kodi">kodi i ProfesioneTitujPuneit</param>
        /// <param name="idNdermarje"> id e ndermarjes</param>
        /// <returns>nje objeckt boolean qe tregon nese ekziston apo jo nje vendndodhjet me kete kod</returns>
        public bool ekzistonKodeProfesione(string kodi, int idNdermarje)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@KODI", kodi, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idNdermarje, ParameterDirection.Input);
            using (DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KodeProfesione_ekzistonKod"))
            {
                if (ds.Tables[0].Rows.Count == 1)
                    return true;
                if (ds.Tables[0].Rows.Count == 0)
                    return false;
                return true;
            }
        }

        /// <summary>
        /// merr  vendndodhjet sipas id ne forme data row
        /// </summary>
        /// <param name="id"> id ProfesioneTitujPune</param>
        /// <returns> kthen data row me kete ProfesioneTitujPune</returns>
        internal DataRow merrKodeProfesioneSipasIdDR(int id)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@ID", id, ParameterDirection.Input);
            using (DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KodeProfesione_merrSipasIdDR"))
            {
                if (ds == null)
                    return null;
                if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                    return null;
                return ds.Tables[0].Rows[0];
            }
        }

        /// <summary>
        /// merr gjithe vendndodhjet sipas ndermarjes ne forme data table
        /// </summary>
        /// <param name="idnderm">id ndermarje</param>
        /// <returns> kthen nje datatable me te gjithe vendndodhjet e kesaj ndermarje</returns>
        internal DataTable merrKodeProfesioneDT(int idnderm)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            using (DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KodeProfesione_merrSipaSNdermarrjesDT"))
            {
                return ds.Tables[0];
            }
        }

        /// <summary>
        /// merr gjithe vendndodhjet sipas ndermarjes aktive ne forme data table
        /// </summary>
        /// <param name="idnderm">id ndermarje</param>
        /// <returns> kthen nje datatable me te gjithe ProfesioneTitujPunet aktive e kesaj ndermarje</returns>
        internal DataTable merrKodeProfesioneDTAktive(int idnderm)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            using (DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KodeProfesione_merrSipasNdermarjePerLupe"))
            {
                return ds.Tables[0];
            }
        }

        public bool kaVeprimeKodeProfesione(int id)
        {//kontrollon nqs ka veprime me kete kodifikim artikulli

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@ID", id, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KodeProfesione_kaVeprime");
            if (ds.Tables[0].Rows.Count == 1)
                return true;
            if (ds.Tables[0].Rows.Count == 0)
                return false;
            return true;
        }

        #endregion

        /// <summary>
        /// Metodat e meposhtme i perkasin veprimeve qe kryhen nga objektet clsGrupimeLocaleGlobale dhe colGrupimeLocaleGlobale
        /// </summary>
        #region GRUPIME LOCALE GLOBALE

        /// <summary>
        /// ekzekuton 	[prc_T_GrupimeLocaleGlobale_ins] ne DB dhe kthen statusin e perfundimit te ekzekutimit te SP-se perkatese
        /// Shiko clsMesazh brenda ketij moduli per informacion te metejshem mbi vleren e kthimit te kesaj metode
        /// </summary>
        /// <param name="idGrupimeLocaleGlobale"> id e GrupimeLocaleGlobaleit</param>
        /// <param name="kodi">kodi</param>
        /// <param name="pershkrimi">pershkrimi</param>
        /// <param name="pershkrimiang">kostoja e planifikuar</param>
        /// <param name="idkrijuesi">idllog</param>
        /// <param name="aktiv"> aktive</param>
        /// <param name="lloji">tipi makineri, mjete,punonjes</param>
        /// <param name="idkonfig">id e konfigurimit</param>
        /// <param name="idPerdoruesi">id e perdoruesit</param>
        /// <param name="idnderm">id e ndermarjes</param>
        /// <param name="idstatusdok">id e statusdok</param>
        /// <returns>nje objekt clsMesazh qe tregon nese ruajtja eshte kryer ne rregull apo jo.</returns>
        /// <seealso cref="cs"/>
        internal clsMesazh ruajGrupimeLocaleGlobale(out int idGrupimeLocaleGlobale, string kodi, string pershkrimi, int lloji, int idkrijuesi, bool aktiv, int idkonfig, int idPerdoruesi, int idnderm, int idstatusdok, int idprindi)
        {
            idGrupimeLocaleGlobale = -1;

            dbManager.Open();
            dbManager.CreateParameters(11);
            dbManager.AddParameters(0, "@ID", idGrupimeLocaleGlobale, ParameterDirection.Output);
            dbManager.AddParameters(1, "@KODI", kodi, ParameterDirection.Input);
            dbManager.AddParameters(2, "@PERSHKRIMI", pershkrimi, ParameterDirection.Input);
            dbManager.AddParameters(3, "@LLOJI", lloji, ParameterDirection.Input);
            dbManager.AddParameters(4, "@AKTIV", aktiv, ParameterDirection.Input);
            dbManager.AddParameters(5, "@IDKRIJUESI", idkrijuesi, ParameterDirection.Input);
            dbManager.AddParameters(6, "@IDKONFIG", idkonfig, ParameterDirection.Input);
            dbManager.AddParameters(7, "@IDPERDORUESI", idPerdoruesi, ParameterDirection.Input);
            dbManager.AddParameters(8, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            dbManager.AddParameters(9, "@IDSTATUSDOK", idstatusdok, ParameterDirection.Input);
            if (idprindi > 0)
                dbManager.AddParameters(10, "@Idprindi", idprindi, ParameterDirection.Input);
            else dbManager.AddParameters(10, "@idprindi", DBNull.Value, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_GRUPIMELOCALEGLOBALE_ins");
            idGrupimeLocaleGlobale = int.Parse(dbManager.Parameters[0].Value.ToString());
            return new clsMesazh(true, mesazhRuajtje);

        }

        /// <summary>
        /// ekzekuton prc_T_GrupimeLocaleGlobale_upd ne DB dhe kthen statusin e perfundimit te ekzekutimit te SP-se perkatese
        /// Shiko clsMesazh brenda ketij moduli per informacion te metejshem mbi vleren e kthimit te kesaj metode
        /// </summary>
        /// <param name="idGrupimeLocaleGlobale"> id e GrupimeLocaleGlobaleit</param>
        /// <param name="kodi">kodi</param>
        /// <param name="pershkrimi">pershkrimi</param>
        /// <param name="pershkrimiang">kostoja e planifikuar</param>
        /// <param name="idllog">idllog</param>
        /// <param name="aktiv"> aktive</param>
        /// <param name="lloji">tipi makineri, mjete,punonjes</param>
        /// <param name="idkonfig">id e konfigurimit</param>
        /// <param name="idPerdoruesi">id e perdoruesit</param>
        /// <param name="idnderm">id e ndermarjes</param>
        /// <param name="idstatusdok">id e statusdok</param>
        /// <returns> nje objekt clsMesazh qe tregon nese modifikimi eshte kryer ne rregull apo jo.</returns>
        internal clsMesazh modifikoGrupimeLocaleGlobale(int idGrupimeLocaleGlobale, string kodi, string pershkrimi, int lloji, bool aktiv, int idkonfig, int idPerdoruesi, int idnderm, int idstatusdok, int idprindi)
        {
            dbManager.Open();
            dbManager.CreateParameters(10);
            dbManager.AddParameters(0, "@ID", idGrupimeLocaleGlobale, ParameterDirection.Input);
            dbManager.AddParameters(1, "@KODI", kodi, ParameterDirection.Input);
            dbManager.AddParameters(2, "@PERSHKRIMI", pershkrimi, ParameterDirection.Input);
            dbManager.AddParameters(3, "@LLOJI", lloji, ParameterDirection.Input);
            dbManager.AddParameters(4, "@AKTIV", aktiv, ParameterDirection.Input);
            dbManager.AddParameters(5, "@IDKONFIG", idkonfig, ParameterDirection.Input);
            dbManager.AddParameters(6, "@IDPERDORUESI", idPerdoruesi, ParameterDirection.Input);
            dbManager.AddParameters(7, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            dbManager.AddParameters(8, "@IDSTATUSDOK", idstatusdok, ParameterDirection.Input);
            if (idprindi > 0)
                dbManager.AddParameters(9, "@Idprindi", idprindi, ParameterDirection.Input);
            else dbManager.AddParameters(9, "@idprindi", DBNull.Value, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_GRUPIMELOCALEGLOBALE_upd");
            return new clsMesazh(true, mesazhModifikimi);
        }

        /// <summary>
        /// ekzekutohet sp-ja prc_T_GrupimeLocaleGlobale_del duke i kaluar id e GrupimeLocaleGlobaleit
        /// </summary>
        /// <param name="id">id e GrupimeLocaleGlobaleit qe do fshihet </param>
        /// <returns>nje objekt clsMesazh qe tregon nese fshirja eshte kryer ne rregull apo jo</returns>
        internal clsMesazh fshiGrupimeLocaleGlobale(int id)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@ID", id, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_GRUPIMELOCALEGLOBALE_del");
            clsMesazh mesazh = new clsMesazh(true, mesazhFshirje);
            return mesazh;
        }

        /// <summary>
        /// fshin GrupimeLocaleGlobalein duke ndryshuar statusin e saj ne te fshire
        /// </summary>
        /// <param name="id">id e GrupimeLocaleGlobaleit</param>
        /// <param name="idperdoruesi"> id e perdoruesit qe ka kryer veprimin</param>
        /// <returns> clsMesazh qe tregon nese fshirja u krye me sukses apo jo</returns>
        internal clsMesazh fshiGrupimeLocaleGlobaleStatus(int id, int idperdoruesi)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@ID", id, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUESI", idperdoruesi, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_GRUPIMELOCALEGLOBALE_upddel");
            clsMesazh mesazh = new clsMesazh(true, mesazhFshirje);
            return mesazh;
        }

        /// <summary>
        /// merr GrupimeLocaleGlobalein sipas id
        /// </summary>
        /// <param name="id">  id e GrupimeLocaleGlobaleit</param>
        /// <returns> kthen datarow qe permban GrupimeLocaleGlobalein me kete id</returns>
        internal DataRow ktheGrupimeLocaleGlobale(int id)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@ID", id, ParameterDirection.Input);
            using (DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_GRUPIMELOCALEGLOBALE_merrSipasId"))
            {
                if (ds == null)
                    return null;
                if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                    return null;
                return ds.Tables[0].Rows[0];
            }
        }

        /// <summary>
        /// merr gjithe GrupimeLocaleGlobalet te nje ndermarje
        /// </summary>
        /// <param name="idnder"> id e ndermarjes</param>
        /// <returns> nje datatable qe permban nje koleksion me te gjithe GrupimeLocaleGlobalet te ndermarjes</returns>
        internal DataTable ktheGjitheGrupimeLocaleGlobaletSipasNdermarjesDheLlojit(int idnder, int lloji)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDNDERMARJE", idnder, ParameterDirection.Input);
            dbManager.AddParameters(1, "@LLOJI", lloji, ParameterDirection.Input);
            using (DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_GRUPIMELOCALEGLOBALE_merrGjitheSipasLlojit"))
            {
                return ds.Tables[0];
            }
        }

        /// <summary>
        /// merr gjithe GrupimeLocaleGlobalet te nje ndermarje  aktiv
        /// </summary>
        /// <param name="idnder"> id e ndermarjes</param>
        /// <returns> nje datatable qe permban nje koleksion me te gjithe GrupimeLocaleGlobalet te ndermarjes</returns>
        internal DataTable ktheGjitheGrupimeLocaleGlobaletSipasNdermarjesDheLlojitAktiv(int idnder, int lloji)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDNDERMARJE", idnder, ParameterDirection.Input);
            dbManager.AddParameters(1, "@LLOJI", lloji, ParameterDirection.Input);

            using (DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_GRUPIMELOCALEGLOBALE_merrGjitheSipasLlojitAktive"))
            {
                return ds.Tables[0];
            }
        }
        internal DataTable ktheGjitheGrupimeLocaleGlobaletSipasNdermarjesDhePrinditAktiv(int idnder, int idprindi)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDNDERMARJE", idnder, ParameterDirection.Input);
            dbManager.AddParameters(1, "@idprindi", idprindi, ParameterDirection.Input);

            using (DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_GRUPIMELOCALEGLOBALE_merrGjitheSipasPrinditAktive"))
            {
                return ds.Tables[0];
            }
        }
        /// <summary>
        /// merr GrupimeLocaleGlobalet te nje ndermarje me kete kod
        /// </summary>
        /// <param name="kodi"> kodi i GrupimeLocaleGlobaleit</param>
        /// <param name="idnder"> id e ndermarjes</param>
        /// <returns>  data row me GrupimeLocaleGlobalein te nje ndermarje me kete kod</returns>
        internal DataRow ktheGrupimeLocaleGlobaleSipasKodit(string kodi, int idnder, int lloji)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@KODI", kodi, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idnder, ParameterDirection.Input);
            // dbManager.AddParameters(2, "@LLOJI", lloji, ParameterDirection.Input);
            using (DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_GRUPIMELOCALEGLOBALE_ktheSipasKodi"))
            {
                if (ds == null)
                    return null;
                if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                    return null;
                return ds.Tables[0].Rows[0];
            }
        }

        /// <summary>
        /// perdoret per te kontrolluar nqs ekziston nje GrupimeLocaleGlobale me kete kod 
        /// behet kontrolli per qellime konsistence te informacionit ne DB, nuk lejon te celen GrupimeLocaleGlobale te ndryshem me te njejtin kod
        /// </summary>
        /// <param name="kodi">kodi i GrupimeLocaleGlobaleit</param>
        /// <param name="idNdermarje"> id e ndermarjes</param>
        /// <returns>nje objeckt boolean qe tregon nese ekziston apo jo nje GrupimeLocaleGlobale me kete kod</returns>
        public bool ekzistonGrupimeLocaleGlobale(string kodi, int idNdermarje, int lloji)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@KODI", kodi, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idNdermarje, ParameterDirection.Input);
            //  dbManager.AddParameters(2, "@LLOJI", lloji, ParameterDirection.Input);
            using (DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_GRUPIMELOCALEGLOBALE_ekzistonKod"))
            {
                if (ds.Tables[0].Rows.Count == 1)
                    return true;
                if (ds.Tables[0].Rows.Count == 0)
                    return false;
                return true;
            }
        }

        /// <summary>
        /// merr  GrupimeLocaleGlobalein sipas id ne forme data row
        /// </summary>
        /// <param name="idGrupimeLocaleGlobale"> id GrupimeLocaleGlobale</param>
        /// <returns> kthen data row me kete GrupimeLocaleGlobale</returns>
        internal DataRow merrGrupimeLocaleGlobaleSipasIdDR(int idGrupimeLocaleGlobale)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@ID", idGrupimeLocaleGlobale, ParameterDirection.Input);
            using (DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_GRUPIMELOCALEGLOBALE_merrSipasIdDR"))
            {
                if (ds == null)
                    return null;
                if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                    return null;
                return ds.Tables[0].Rows[0];
            }
        }

        /// <summary>
        /// merr gjithe GrupimeLocaleGlobalet sipas ndermarjes ne forme data table
        /// </summary>
        /// <param name="idnderm">id ndermarje</param>
        /// <returns> kthen nje datatable me te gjithe GrupimeLocaleGlobalet e kesaj ndermarje</returns>
        internal DataTable merrGrupimeLocaleGlobaleDT(int idnderm)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            //dbManager.AddParameters(1, "@LLOJI", lloji, ParameterDirection.Input);
            using (DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_GRUPIMELOCALEGLOBALE_merrSipaSNdermarrjesDheLlojitDT"))
            {
                return ds.Tables[0];
            }
        }

        /// <summary>
        /// merr gjithe GrupimeLocaleGlobalet sipas ndermarjes aktive ne forme data table
        /// </summary>
        /// <param name="idnderm">id ndermarje</param>
        /// <returns> kthen nje datatable me te gjithe GrupimeLocaleGlobalet aktive e kesaj ndermarje</returns>
        internal DataTable merrGrupimeLocaleGlobaleDTAktive(int idnderm, int lloji)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            dbManager.AddParameters(1, "@LLOJI", lloji, ParameterDirection.Input);
            using (DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_GRUPIMELOCALEGLOBALE_merrSipasNdermarjeDheLlojiPerLupe"))
            {
                return ds.Tables[0];
            }
        }
        internal DataTable merrGrupimeLocaleGlobaleDTAktiveSipasPrindit(int idnderm, int idprindi)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            dbManager.AddParameters(1, "@idprindi", idprindi, ParameterDirection.Input);
            using (DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_GRUPIMELOCALEGLOBALE_merrSipasNdermarjeDheLlojiPerLupeSipasPrindit"))
            {
                return ds.Tables[0];
            }
        }
        public bool kaVeprimeGrupime(int id)
        {//kontrollon nqs ka veprime me kete kodifikim artikulli

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@ID", id, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_GRUPIMELOCALEGLOBALE_kaVeprime");
            if (ds.Tables[0].Rows.Count == 1)
                return true;
            if (ds.Tables[0].Rows.Count == 0)
                return false;
            return true;
        }

        #endregion


        /// <summary>
        /// Metodat e meposhtme i perkasin veprimeve qe kryhen nga objektet clsPunonjes per People Finder
        /// </summary>
        #region People finder

        /// <summary>
        /// merr gjithe punonjesit e ndermarjes
        /// </summary>
        /// <param name="idnder"> id e ndermarjes</param>
        /// <param name="lloji">lloji</param>
        /// <returns> nje datatable qe permban nje koleksion me te gjithe punonjesit te ndermarjes</returns>
        internal DataTable ktheGjithePunonjesitSipasKerkimitPeopleFinder(string shprehjeKerkimi, string kodNdermarrje)
        {
            string salt = WebConfigurationManager.AppSettings["salt"];
            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@SHPREHJEKERKIMI", shprehjeKerkimi, ParameterDirection.Input);
            dbManager.AddParameters(1, "@KODNDERMARRJE", kodNdermarrje, ParameterDirection.Input);
            dbManager.AddParameters(2, "@SALT", salt, ParameterDirection.Input);
            using (DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_PUNONJES_merrPunonjesPerPeopleFinder"))
            {
                return ds.Tables[0];
            }
        }

        #endregion

        internal DataTable ktheACListePunonjesishLikeKodiEmerMbiemer(string kodi, int idNdermarrje)
        {
            string salt = WebConfigurationManager.AppSettings["salt"];
            dbManager.Open();
            dbManager.CreateParameters(3);            
            dbManager.AddParameters(0, "@IDNDERMARJE", idNdermarrje, ParameterDirection.Input);
            dbManager.AddParameters(1, "@prefix", kodi, ParameterDirection.Input);
            dbManager.AddParameters(2, "@SALT", salt, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_Punonjes_merrSipasEmritNrpersonalDheLlojitDheMbiemriLike");
            return ds.Tables[0];
        }

        internal DataRow merrNrTelSipasUsername(string username)
        {
            string salt = WebConfigurationManager.AppSettings["salt"];
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@USERNAME", username, ParameterDirection.Input);
            dbManager.AddParameters(1, "@SALT", salt, ParameterDirection.Input);
            using (DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_PUNONJES_merrNrTelSipasUsername"))
            {
                return ds.Tables[0].Rows[0];
            }
        }

        internal bool eshteLidhurPunMeVepArkeBanke( int idpunonjes)
        {
           
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDPUNONJES", idpunonjes, ParameterDirection.Input);
            return  Convert.ToBoolean(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_PUNONJES_LidhurMeVeprimeArkaBanka"));
        }
        
        internal DataTable merrEdukimeSipasIdNdermarje(int idndermarje, int idgjuha)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDNDERMARJE", idndermarje, ParameterDirection.Input);
            dbManager.AddParameters(1, "@idgjuha", idgjuha, ParameterDirection.Input);
            using (DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_EDUKIMI_sel"))
            {
                return ds.Tables[0];
            }
        }

        internal DataRow merrEdukimeSipasIdNdermarjeDhePershkrimit(int idndermarje, string pershkrimi, int idgjuha)
        {
            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@IDNDERMARJE", idndermarje, ParameterDirection.Input);
            dbManager.AddParameters(1, "@PERSHKRIMI", pershkrimi, ParameterDirection.Input);
            dbManager.AddParameters(2, "@idgjuha", idgjuha, ParameterDirection.Input);
            using (DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_EDUKIMI_merrSipasPershkrimitDheNdermarjes"))
            {
                if (ds == null)
                    return null;
                if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                    return null;
                return ds.Tables[0].Rows[0];
            }
        }

        internal DataRow merrEdukimeSipasId(int id)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@ID", id, ParameterDirection.Input);

            using (DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_EDUKIMI_merrSipasId"))
            {
                if (ds == null)
                    return null;
                if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                    return null;
                return ds.Tables[0].Rows[0];
            }
        }

        public IEnumerable<clsKompListPagese> MerrKomponenteListPageseSipasTrupave(List<int> ids)
        {
            string salt = WebConfigurationManager.AppSettings["salt"];
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@trupatIds", string.Join(",", ids), ParameterDirection.Input);
            dbManager.AddParameters(1, "@salt", salt, ParameterDirection.Input);
            return dbManager.GetIEnumerbale("prc_T_KOMPLISTPAGESE_selAllSipasTrupave", clsKompListPagese.Krijo);
        }

        internal IEnumerable<clsPunesim> kthePunesimTefunditPerPunonjesit(DateTime data, List<int> ids)
        {
            string salt = WebConfigurationManager.AppSettings["salt"];
            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@trupatIds", string.Join(",", ids), ParameterDirection.Input);
            dbManager.AddParameters(1, "@salt", salt, ParameterDirection.Input);
            dbManager.AddParameters(2, "@data", data, ParameterDirection.Input);
            return dbManager.GetIEnumerbale("prc_T_PUNESIM_merrPunesimSipasIdPunonjesveTeFundit", clsPunesim.Krijo);
        }

        public bool ekzistonListpagesePaPunesimAktivPerPunonjesin(int idPunonjes)
        {
            string salt = WebConfigurationManager.AppSettings["salt"];
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDPUNONJES", idPunonjes, ParameterDirection.Input);
            var ekziston = Convert.ToInt32(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_PUNESIM_kontrollPunonjesinPerListpagesePaPunesimAktiv"));
            return ekziston == 1;
        }

        internal IEnumerable<clsKomponentePage> ktheKomponentePagePerKetoId(List<int> ids)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@komponenteIds", string.Join(",", ids), ParameterDirection.Input);
            return dbManager.GetIEnumerbale("prc_T_KOMPONENTEPAGE_ktheKomponentePerIdte", clsKomponentePage.Krijo);
        }
    }
}