using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using DbCore.IMBUtils.DataBase;
using DbCore.IMBUtils.Logging;
using AlphaWeb.Core.Interfaces.Data;
using DbCore.IMBUtils.Messages;
using System.Data.SqlClient;

namespace DbCore.DbInventari
{
    /// <remarks>
    ///  Kjo eshte klasa me e rendesishme e ketij moduli. Eshte nje klase e tille per secilin modul te projektit.
    ///  Ketu ndodhen metodat qe therriten  nga te gjithe objektet brenda projektit DbCore.DbInventari
    ///  secila metode permban thirjet e Store procedures ne DB, duke i kaluar parametrat perkates
    ///  Metodat jane te ndara ne Regions sipas objekteve qe i therrasin keto metoda
    /// </remarks>
    public class clsDatabaseInventari : DbData
    {

        public clsDatabaseInventari()
            : base()
        {
        }
        public clsDatabaseInventari(DbData db) : base(db) { }
        public clsDatabaseInventari(string connectionName) : base(connectionName)
        {

        }

        /// <summary>
        /// Metodat e meposhtme i perkasin veprimeve qe kryhen nga objektet clsKodbari dhe colKodbare
        /// </summary>
        #region KODBARI

        /// <summary>
        /// ekzekuton prc_T_KODBARI_ins ne DB dhe kthen statusin e perfundimit te ekzekutimit te SP-se perkatese
        /// Shiko clsMesazh brenda ketij moduli per informacion te metejshem mbi vleren e kthimit te kesaj metode
        ///<param name="idKodbari"> id e kodbarit</param>
        ///<param name="idArtikulli"> id e artikullit</param>
        ///<param name="pershkrimi"> pershkrimi i artikullit</param>
        ///<param name="idndermarje">id e ndermarjes</param>
        /// <returns> nje objekt clsMesazh qe tregon nese ruajtja eshte kryer ne rregull apo jo.</returns>
        /// </summary>
        internal clsMesazh ruajKodbar(int idArtikulli, String pershkrimi, int njesia, int idndermarje, int iddetajim1, int iddetajim2)
        {
            dbManager.Open();
            dbManager.CreateParameters(5);
            dbManager.AddParameters(0, "@IDARTIKULLI", idArtikulli, ParameterDirection.Input);
            dbManager.AddParameters(1, "@PERSHKRIMI", pershkrimi, ParameterDirection.Input);
            dbManager.AddParameters(2, "@NJESIA", njesia, ParameterDirection.Input);
            if (iddetajim1 > 0)
                dbManager.AddParameters(3, "@DETAJIM1", iddetajim1, ParameterDirection.Input);
            else dbManager.AddParameters(3, "@DETAJIM1", DBNull.Value, ParameterDirection.Input);
            if (iddetajim2 > 0)
                dbManager.AddParameters(4, "@DETAJIM2", iddetajim2, ParameterDirection.Input);
            else dbManager.AddParameters(4, "@DETAJIM2", DBNull.Value, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_KODBARI_ins");
            return new clsMesazh(true, IMBUtils.Messages.MessagesResource.Messages["labelRaportMesazhRuajtjaPerfundoiSukses"]);


        }

        internal DataTable GetDetajimeLookupSimpleTable(int idNdermarrje, int idPerdoruesi)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters("@IDNDERMARJE", idNdermarrje, ParameterDirection.Input);
            dbManager.AddParameters("@IDPERDORUES", idPerdoruesi, ParameterDirection.Input);
            return dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_DETAJIMARTIKULLI_GetDetajimeLookupSimpleTable").Tables[0];
        }

        internal DataTable GetNiveleCmimeshLookupSimpleTable(int idNdermarrje, int shitjeApoBlerje, int idPerdoruesi)
        {
            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters("@IDNDERMARJE", idNdermarrje, ParameterDirection.Input);
            dbManager.AddParameters("@LLOJINIVELCMIMI", shitjeApoBlerje, ParameterDirection.Input);
            dbManager.AddParameters("@IDPERDORUES", idPerdoruesi, ParameterDirection.Input);
            return dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_NIVELCMIMI_GetNiveleCmimeshLookupSimpleTable").Tables[0];
        }

        internal DataTable GetKodifikimeArtikulliLookupSimpleTable(int idNdermarrje)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters("@IDNDERM", idNdermarrje, ParameterDirection.Input);
            return dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KODIFIKIMARTIKULLI_GetKodifikimeArtikulliLookupSimpleTable").Tables[0];
        }

        internal DataTable KtheCmimArtikujshSipasNivelit(string artIds, int idNivelCmimi)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@ARTIDS", artIds, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNIVELCMIMI", idNivelCmimi, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_CMIMARTIKULLI_KtheCmimArtikujshSipasNivelit");
            return ds.Tables[0];
        }

        /// <summary>
        /// ekzekuton prc_T_KODBARI_upd ne DB dhe kthen statusin e perfundimit te ekzekutimit te SP-se perkatese
        /// Shiko clsMesazh brenda ketij moduli per informacion te metejshem mbi vleren e kthimit te kesaj metode
        ///<param name="idKodbari"> id e kodbarit</param>
        ///<param name="idArtikulli"> id e artikullit</param>
        ///<param name="pershkrimi"> pershkrimi i artikullit</param>
        /// <returns> nje objekt clsMesazh qe tregon nese modifikimi eshte kryer ne rregull apo jo.</returns>
        /// </summary>
        internal clsMesazh modifikoKodbar(int idKodbari, int idArtikulli, String pershkrimi, int njesia, int iddetajim1, int iddetajim2)
        {

            dbManager.Open();
            //shtimi i parametrave
            dbManager.CreateParameters(6);
            dbManager.AddParameters(0, "@IDKODBARI", idKodbari, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDARTIKULLI", idArtikulli, ParameterDirection.Input);
            dbManager.AddParameters(2, "@PERSHKRIMI", pershkrimi, ParameterDirection.Input);
            dbManager.AddParameters(3, "@NJESIA", njesia, ParameterDirection.Input);
            if (iddetajim1 > 0)
                dbManager.AddParameters(4, "@DETAJIM1", iddetajim1, ParameterDirection.Input);
            else dbManager.AddParameters(4, "@DETAJIM1", DBNull.Value, ParameterDirection.Input);
            if (iddetajim2 > 0)
                dbManager.AddParameters(5, "@DETAJIM2", iddetajim2, ParameterDirection.Input);
            else dbManager.AddParameters(5, "@DETAJIM2", DBNull.Value, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_KODBARI_upd");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgModifikimiMeSukses"]);
            return mesazh;


        }

        /// <summary>
        /// ekzekutohet sp-ja prc_T_KODBARI_del duke i kaluar id e kodbarit qe e marrim nga objekti clsKodbari qe i kalohet si parameter
        /// </summary>
        /// <param name="idKodbari"> id e kodbarit</param>
        /// <returns>nje objekt clsMesazh qe tregon nese fshirja eshte kryer ne rregull apo jo</returns>
        internal clsMesazh fshiKodbar(int idKodbari)
        {
            dbManager.Open();
            //shtimi i parametrave
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDKODBARI", idKodbari, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_KODBARI_del");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
            return mesazh;
        }

        internal bool eshteBarkodILidhur(int idBarkodi)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDBARKODI", idBarkodi, ParameterDirection.Input);
            // return Convert.ToBoolean(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_KODBARI_eshteILidhur").ToString());
            int nr = Convert.ToInt32(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_KODBARI_eshteILidhur"));
            return Convert.ToBoolean(nr);
        }




        internal bool ekzistonDetajimBarkodArtikulli(string kodartikulli, int idndermarje, int llojdetajim, string koddetajimi)
        {
            dbManager.Open();
            dbManager.CreateParameters(4);
            dbManager.AddParameters(0, "@KODARTIKULL", kodartikulli, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idndermarje, ParameterDirection.Input);
            dbManager.AddParameters(2, "@llojdetajimi", llojdetajim, ParameterDirection.Input);
            dbManager.AddParameters(3, "@koddetajimi", koddetajimi, ParameterDirection.Input);
            int nr = Convert.ToInt32(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_DETAJIMART_ekzistonDetajimBarkodArtikulli"));
            return Convert.ToBoolean(nr);
        }



        /// <summary>
        /// kthen objektet kodbar sipas idse
        /// </summary>
        ///<param name="idKodbari"> id e kodbarit</param>
        internal void merrKodbar(int idKodbari)
        {

            dbManager.Open();
            //shtimi i parametrave
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDKODBARI", idKodbari, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_KODBARI_sel");

        }

        /// <summary>
        /// kthen  datatable kodbaret 
        /// </summary>
        ///<returns>nje datatable qe permban nje koleksion me te gjitha kodbaret  </returns>
        internal DataTable ktheGjitheKodbaret()
        {

            dbManager.Open();
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KODBARI_merrTeGjitha");

            return ds.Tables[0];

        }

        /// <summary>
        /// kthen datatable sipas id artikullit
        /// </summary>
        ///<param name="idArtikulli">id e artikullit</param>
        ///<returns>nje datatable qe permban kodbaret e atij artikulli  </returns>
        internal DataTable ktheKodbarSipasIdArtikulli(int idArtikulli)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDARTIKULLI", idArtikulli, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KODBARI_merrSipasIdArtikulli");
            return ds.Tables[0];
        }

        internal DataRow ktheKodbarSipasIdArtikulliNjesiaKodbariIPare(int idArtikulli, int njesia)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDARTIKULLI", idArtikulli, ParameterDirection.Input);
            dbManager.AddParameters(1, "@NJESIA", njesia, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KODBARI_merrSipasIdArtikulliKodbarIparePerNjesine");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];
        }
        internal DataRow ktheKodbarSipasPershkrimit(string kodbar, int idNdermarrje)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@Kodbar", kodbar, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARRJE", idNdermarrje, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KODBARI_merrSipasKodbarit");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];
        }

        internal int MerrIdBarkodiSipasPershkrimiDheIdArtikulli(string barkodi, int idArtikull)
        {

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@PERSHKRIMKODBAR", barkodi, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDARTIKULLI", idArtikull, ParameterDirection.Input);
            return Convert.ToInt32(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_KODBARI_merrIdBarkodi"));

        }
        internal int MerrIdBarkodiSipasPershkrimit(string barkodi)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@PERSHKRIMKODBAR", barkodi, ParameterDirection.Input);
            return Convert.ToInt32(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_KODBARI_merrIdBarkodiSipasPershkrimit"));

        }

        //[Obsolete("Perdor: DataRow ktheKodbarSipasIdArtikulli(int idArtikulli)", true)]
        //public colKodbare merrKodbarSipasIdArtikulli(int idartikulli)
        //{//metoda per te marre kodbaret sipas id artikulli
        //    IDBManager dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        //    dbManager.ConnectionString = dbManager.GetConnectionString();
        //    try
        //    {
        //        dbManager.Open();
        //        dbManager.CreateParameters(1);
        //        dbManager.AddParameters(0, "@IDARTIKULLI", idartikulli , ParameterDirection.Input);
        //        DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KODBARI_merrSipasIdArtikulli");
        //        colKodbare colKodbare = new colKodbare();
        //        return colKodbare.mbushArrayListKodbaresh(ds);
        //    }
        //    catch (Exception)
        //    {
        //        return new colKodbare();
        //    }
        //    finally
        //    {
        //        dbManager.Dispose();
        //    }
        //}

        internal DataTable ktheArtikujKodbareNdermarrjesAndAutorizimePerLupeKodbari(int idnderm, int idperdorues)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUES", idperdorues, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KODBARI_merrArtikullKodbarSipasNdermarjesAndAutorizimPerLupeKodbari");
            return ds.Tables[0];

        }

        /// <summary>
        /// kthen numrin e kodbareve te artikullit
        /// </summary>
        /// <param name="idArtikulli"></param>
        /// <returns></returns>
        internal int ktheNrKodBarePerArtikullin(int idArtikulli)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDARTIKULLI", idArtikulli, ParameterDirection.Input);
            return Convert.ToInt32(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_KODBARI_merrNrKodbarPerIdArtikulli"));
        }

        internal int merrNjesiKodbari(int idart, string kodbari)
        {

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDARTIKULLI", idart, ParameterDirection.Input);
            dbManager.AddParameters(1, "@KODBARI", kodbari, ParameterDirection.Input);
            return Convert.ToInt32(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_KODBARI_merrNjesiKodbar"));

        }


        /// <summary>
        /// perdoret per te kontrolluar nqs ekziston nje kodbar me kete pershkrim
        /// behet kontrolli per qellime konsistence te informacionit ne DB, nuk lejon te celen kodbare te ndryshem me te njejtin pershkrim
        /// </summary>
        ///<param name="pershkrim"> pershkrimi i kodbarit</param>
        ///<param name="idndermarje"> id e ndermarjes</param>
        /// <returns>nje objeckt boolean qe tregon nese ekziston apo jo nje kodbar me kete pershkrim</returns>
        public bool ekzistonKodbar(String pershkrim, int idndermarje)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@PERSHKRIMI", pershkrim, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idndermarje, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KODBARI_ekzistonKodbar");
            if (ds.Tables[0].Rows.Count == 1)
                return true;
            else if (ds.Tables[0].Rows.Count == 0)
                return false;
            else return true;
        }

        internal void ruajCmimArtikulliDT(DataTable cmimetPerTuRuajtur)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@CMIME_NDRYSHUAR", cmimetPerTuRuajtur, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "PRC_T_CMIMEARTIKUJSH_MERGEDT");

        }

        #endregion

        /// <summary>
        /// Metodat e meposhtme i perkasin veprimeve qe kryhen nga objektet clsFurnitoreArtikulli dhe colFurnitoreArtikujsh
        /// </summary>
        #region FURNITOREARTIKULLI

        /// <summary>
        /// ekzekuton prc_T_FURNITOREARTIKULLI_ins ne DB dhe kthen statusin e perfundimit te ekzekutimit te SP-se perkatese
        /// Shiko clsMesazh brenda ketij moduli per informacion te metejshem mbi vleren e kthimit te kesaj metode
        ///<param name="idFurnitoreArtikulli"> id furnitor artikullit</param>
        ///<param name="idArtikulli"> id e artikullit </param>
        ///<param name="idFurnitori"> id e furnitorit </param>
        ///<param name="prioriteti"> prioriteti </param>
        /// <returns> nje objekt clsMesazh qe tregon nese ruajtja eshte kryer ne rregull apo jo.</returns>
        /// </summary>
        internal clsMesazh ruajFurnitoreArtikulli(out int idFurnitoreArtikulli, int idArtikulli, int idFurnitori, string prioriteti)
        {
            idFurnitoreArtikulli = -1;
            dbManager.Open();
            //shtimi i parametrave
            dbManager.CreateParameters(4);
            dbManager.AddParameters(0, "@IDFURNITOREARTIKULLI", idFurnitoreArtikulli, ParameterDirection.Output);
            dbManager.AddParameters(1, "@IDARTIKULLI", idArtikulli, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDFURNITORI", idFurnitori, ParameterDirection.Input);
            dbManager.AddParameters(3, "@PRIORITETI", prioriteti, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_FURNITOREARTIKULLI_ins");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
            return mesazh;

        }

        internal DataTable ktheGjitheTrupiMagazinaNgaKokaKlonim(int idKatDok, int id)
        {

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters("@IDKATDOK", idKatDok, ParameterDirection.Input);
            dbManager.AddParameters("@ID", id, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_TRUPIMAGAZINA_selAllSipasKokaKlonim");
            return ds.Tables[0];
        }

        //[Obsolete("Perdor: clsMesazh ruajFurnitoreArtikulli(int idFurnitoreArtikulli, int idArtikulli, int idFurnitori, string prioriteti)", true)]
        //public clsMesazh ruajFurnitoreArtikulli(clsFurnitoreArtikulli furnitoreArtikulli)
        //{ //metoda per ruajtjen e FurnitoreArtikulli
        //        try
        //    {
        //        //shtimi i parametrave
        //        dbManager.CreateParameters(4);
        //        dbManager.AddParameters(0, "@IDFURNITOREARTIKULLI", furnitoreArtikulli.IdFurnitoreArtikulli, ParameterDirection.Output);
        //        dbManager.AddParameters(1, "@IDARTIKULLI", furnitoreArtikulli.IdArtikulli, ParameterDirection.Input);
        //        dbManager.AddParameters(2, "@IDFURNITORI", furnitoreArtikulli.IdFurnitori, ParameterDirection.Input);
        //        dbManager.AddParameters(3, "@PRIORITETI", furnitoreArtikulli.Prioriteti, ParameterDirection.Input);
        //        dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_FURNITOREARTIKULLI_ins");
        //        clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
        //        return mesazh;
        //    }
        //    catch (Exception)
        //    {
        //        return new clsMesazh(false, ce.Message);
        //    }


        //}

        /// <summary>
        /// ekzekuton prc_T_FURNITOREARTIKULLI_upd ne DB dhe kthen statusin e perfundimit te ekzekutimit te SP-se perkatese
        /// Shiko clsMesazh brenda ketij moduli per informacion te metejshem mbi vleren e kthimit te kesaj metode
        ///<param name="idFurnitoreArtikulli"> id furnitor artikullit</param>
        ///<param name="idArtikulli"> id e artikullit </param>
        ///<param name="idFurnitori"> id e furnitorit </param>
        ///<param name="prioriteti"> prioriteti </param>
        /// <returns> nje objekt clsMesazh qe tregon nese modifikimi eshte kryer ne rregull apo jo.</returns>
        /// </summary>
        internal clsMesazh modifikoFurnitoreArtikulli(int idFurnitoreArtikulli, int idArtikulli, int idFurnitori, string prioriteti)
        {

            dbManager.Open();
            //shtimi i parametrave
            dbManager.CreateParameters(4);
            dbManager.AddParameters(0, "@IDFURNITOREARTIKULLI", idFurnitoreArtikulli, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDARTIKULLI", idArtikulli, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDFURNITORI", idFurnitori, ParameterDirection.Input);
            dbManager.AddParameters(3, "@PRIORITETI", prioriteti, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_FURNITOREARTIKULLI_upd");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgModifikimiMeSukses"]);
            return mesazh;

        }
        //[Obsolete("Perdor: clsMesazh modifikoFurnitoreArtikulli(int idFurnitoreArtikulli, int idArtikulli, int idFurnitori, string prioriteti)", true)]
        //public clsMesazh modifikoFurnitoreArtikulli(clsFurnitoreArtikulli furnitoreArtikulli)
        //{//metoda per modifikimin e FurnitoreArtikulli
        //    try
        //    {
        //        //shtimi i parametrave
        //        dbManager.CreateParameters(4);
        //        dbManager.AddParameters(0, "@IDFURNITOREARTIKULLI", furnitoreArtikulli.IdFurnitoreArtikulli, ParameterDirection.Input);
        //        dbManager.AddParameters(1, "@IDARTIKULLI", furnitoreArtikulli.IdArtikulli, ParameterDirection.Input);
        //        dbManager.AddParameters(2, "@IDFURNITORI", furnitoreArtikulli.IdFurnitori, ParameterDirection.Input);
        //        dbManager.AddParameters(3, "@PRIORITETI", furnitoreArtikulli.Prioriteti, ParameterDirection.Input);
        //        dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_FURNITOREARTIKULLI_upd");
        //        clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgModifikimiMeSukses"]);
        //        return mesazh;
        //    }
        //    catch (Exception)
        //    {
        //        return new clsMesazh(false, ce.Message);
        //    }
        //}

        /// <summary>
        /// ekzekutohet sp-ja prc_T_FURNITOREARTIKULLI_del duke i kaluar id e furnitorit te artikullit qe e marrim nga objekti clsFurnitoreArtikulli qe i kalohet si parameter
        /// </summary>
        /// <param name="idFurnitoreArtikulli"> id furnitor artikullit</param>
        /// <returns>nje objekt clsMesazh qe tregon nese fshirja eshte kryer ne rregull apo jo</returns>
        internal clsMesazh fshiFurnitoreArtikulli(int idFurnitoreArtikulli)
        {
            dbManager.Open();
            //shtimi i parametrave
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDFURNITOREARTIKULLI", idFurnitoreArtikulli, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_FURNITOREARTIKULLI_del");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
            return mesazh;


        }
        //[Obsolete("Perdor: clsMesazh fshiFurnitoreArtikulli(int idFurnitoreArtikulli)", true)]
        //public clsMesazh fshiFurnitoreArtikulli(clsFurnitoreArtikulli furnitoreArtikulli)
        //{//metoda per fshirjen e FurnitoreArtikulli
        //    try
        //    {
        //        //shtimi i parametrave
        //        dbManager.CreateParameters(1);
        //        dbManager.AddParameters(0, "@IDFURNITOREARTIKULLI", furnitoreArtikulli.IdFurnitoreArtikulli, ParameterDirection.Input);
        //        dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_FURNITOREARTIKULLI_del");
        //        clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
        //        return mesazh;
        //    }
        //    catch (Exception)
        //    {
        //        return new clsMesazh(false, ce.Message);
        //    }

        //}

        /// <summary>
        /// kthen objektet furnitor artikulli sipas idse
        /// </summary>
        ///<param name="idFurnitoreArtikulli"> id furnitor artikullit</param>
        internal void merrFurnitoreArtikulli(int idFurnitoreArtikulli)
        {
            dbManager.Open();
            //shtimi i parametrave
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDFURNITOREARTIKULLI", idFurnitoreArtikulli, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_FURNITOREARTIKULLI_sel");

        }
        //[Obsolete("Perdor: merrFurnitoreArtikulli(int idFurnitoreArtikulli)", true)]
        //public void merrFurnitoreArtikulli(clsFurnitoreArtikulli furnitoreArtikulli)
        //{// metoda per te marre nje FurnitoreArtikulli NE BAZE TE ID
        //    IDBManager dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        //    dbManager.ConnectionString = dbManager.GetConnectionString();
        //    try
        //    {
        //        dbManager.Open();
        //        //shtimi i parametrave
        //        dbManager.CreateParameters(1);
        //        dbManager.AddParameters(0, "@IDFURNITOREARTIKULLI", furnitoreArtikulli.IdFurnitoreArtikulli , ParameterDirection.Input);
        //        dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_FURNITOREARTIKULLI_sel");
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
        /// kthen datatable furnitor artikulli sipas id artikullit
        /// </summary>
        ///<param name="idartikulli">id e artikullit</param>
        ///<returns>nje datatable qe permban nje koleksion me te gjitha furnitoret  e ketij artikulli  </returns>
        internal DataTable ktheFurnitoreArtikulliSipasIdArtikulli(int idartikulli)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDARTIKULLI", idartikulli, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_FURNITOREARTIKULLI_merrSipasIdArtikulli");
            return ds.Tables[0];
        }
        //[Obsolete("Perdor: DataTable ktheFurnitoreArtikulliSipasIdArtikulli(int idartikulli)", true)]
        //public colFurnitoreArtikujsh merrFurnitoreArtikulliSipasIdArtikulli(int idartikulli)
        //{//metoda per te marre FurnitoreArtikulli sipas id artikulli
        //    IDBManager dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        //    dbManager.ConnectionString = dbManager.GetConnectionString();
        //    try
        //    {
        //        dbManager.Open();
        //        dbManager.CreateParameters(1);
        //        dbManager.AddParameters(0, "@IDARTIKULLI", idartikulli, ParameterDirection.Input);
        //        DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_FURNITOREARTIKULLI_merrSipasIdArtikulli");
        //        colFurnitoreArtikujsh colFurnitoreArtikujsh = new colFurnitoreArtikujsh();
        //        return colFurnitoreArtikujsh.mbushArrayListFurnitoreArtikujsh(ds);
        //    }
        //    catch (Exception)
        //    {
        //        return new colFurnitoreArtikujsh();
        //    }
        //    finally
        //    {
        //        dbManager.Dispose();
        //    }
        //}

        /// <summary>
        /// perdoret per te kontrolluar nqs ekziston ky furnitor  per kete artikull
        /// behet kontrolli per qellime konsistence te informacionit ne DB, nuk lejon te celen furnitore te njejte per te njejtin artikull dy here
        /// </summary>
        ///<param name="idartikulli"> id e artikullit</param>
        ///<param name="idfurnitori"> id e furnitorit</param>
        /// <returns>nje objeckt boolean qe tregon nese ekziston apo jo ky furnitor per kete artikull</returns>
        public bool ekzistonFurnitoreArtikulli(int idartikulli, int idfurnitori)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDARTIKULLI", idartikulli, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDFURNITORI", idfurnitori, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_FURNITOREARTIKULLI_ekzistonFurnitoreArtikulli");
            if (ds.Tables[0].Rows.Count == 1)
                return true;
            else if (ds.Tables[0].Rows.Count == 0)
                return false;
            else return true;
        }


        #endregion

        /// <summary>
        /// Metodat e meposhtme i perkasin veprimeve qe kryhen nga objektet clsArtikullZevendesues dhe colArtikujtZevendesues
        /// </summary>
        #region ARTIKULLZEVENDESUES

        /// <summary>
        /// ekzekuton prc_T_ARTIKULLIZEVENDESUES_ins ne DB dhe kthen statusin e perfundimit te ekzekutimit te SP-se perkatese
        /// Shiko clsMesazh brenda ketij moduli per informacion te metejshem mbi vleren e kthimit te kesaj metode
        ///<param name="idArtikulliZevendesues"> id tab lidhese per artikull dhe artikull zevendesues</param>
        ///<param name="idArtikulliKryesor"> id e artikullit kryesor</param>
        ///<param name="idArtikulliZevend"> id e artikullit zevendesues</param>
        ///<param name="prioriteti"> artikulli zevendesues i cili ruhet</param>
        /// <returns> nje objekt clsMesazh qe tregon nese ruajtja eshte kryer ne rregull apo jo.</returns>
        /// </summary>
        internal clsMesazh ruajArtikulliZevendesues(out int idArtikulliZevendesues, int idArtikulliKryesor, int idArtikulliZevend, string prioriteti)
        {
            idArtikulliZevendesues = -1;
            dbManager.Open();
            //shtimi i parametrave
            dbManager.CreateParameters(4);
            dbManager.AddParameters(0, "@IDARTIKULLIZEVENDESUES", idArtikulliZevendesues, ParameterDirection.Output);
            dbManager.AddParameters(1, "@IDARTIKULLIKRYESOR", idArtikulliKryesor, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDARTIKULLIZEVEND", idArtikulliZevend, ParameterDirection.Input);
            dbManager.AddParameters(3, "@PRIORITETI", prioriteti, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_ARTIKULLIZEVENDESUES_ins");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
            return mesazh;


        }
        //[Obsolete("Perdor: clsMesazh ruajArtikulliZevendesues(int idArtikulliZevendesues, int idArtikulliKryesor, int idArtikulliZevend, string prioriteti)", true)]
        //public clsMesazh ruajArtikulliZevendesues(clsArtikullZevendesues artikullZevendesues)
        //{ //metoda per ruajtjen e artikullZevendesues
        //    try
        //    {
        //        //shtimi i parametrave
        //        dbManager.CreateParameters(4);
        //        dbManager.AddParameters(0, "@IDARTIKULLIZEVENDESUES", artikullZevendesues.IdArtikulliZevendesues, ParameterDirection.Output);
        //        dbManager.AddParameters(1, "@IDARTIKULLIKRYESOR", artikullZevendesues.IdArtikulliKryesor, ParameterDirection.Input);
        //        dbManager.AddParameters(2, "@IDARTIKULLIZEVEND", artikullZevendesues.IdArtikulliZevend, ParameterDirection.Input);
        //        dbManager.AddParameters(3, "@PRIORITETI", artikullZevendesues.Prioriteti, ParameterDirection.Input);
        //        dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_ARTIKULLIZEVENDESUES_ins");
        //        clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
        //        return mesazh;
        //    }
        //    catch (Exception)
        //    {
        //        return new clsMesazh(false, ce.Message);
        //    }


        //}

        /// <summary>
        /// ekzekuton prc_T_ARTIKULLIZEVENDESUES_upd ne DB dhe kthen statusin e perfundimit te ekzekutimit te SP-se perkatese
        /// Shiko clsMesazh brenda ketij moduli per informacion te metejshem mbi vleren e kthimit te kesaj metode
        ///<param name="idArtikulliZevendesues"> id tab lidhese per artikull dhe artikull zevendesues</param>
        ///<param name="idArtikulliKryesor"> id e artikullit kryesor</param>
        ///<param name="idArtikulliZevend"> id e artikullit zevendesues</param>
        ///<param name="prioriteti"> artikulli zevendesues i cili ruhet</param>
        /// <returns> nje objekt clsMesazh qe tregon nese modifikimi eshte kryer ne rregull apo jo.</returns>
        /// </summary>
        internal clsMesazh modifikoArtikulliZevendesues(int idArtikulliZevendesues, int idArtikulliKryesor, int idArtikulliZevend, string prioriteti)
        {
            dbManager.Open();
            //shtimi i parametrave
            dbManager.CreateParameters(4);
            dbManager.AddParameters(0, "@IDARTIKULLIZEVENDESUES", idArtikulliZevendesues, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDARTIKULLIKRYESOR", idArtikulliKryesor, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDARTIKULLIZEVEND", idArtikulliZevend, ParameterDirection.Input);
            dbManager.AddParameters(3, "@PRIORITETI", prioriteti, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_ARTIKULLIZEVENDESUES_upd");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgModifikimiMeSukses"]);
            return mesazh;

        }
        //[Obsolete("Perdor: clsMesazh modifikoArtikulliZevendesues(int idArtikulliZevendesues, int idArtikulliKryesor, int idArtikulliZevend, string prioriteti)", true)]
        //public clsMesazh modifikoArtikulliZevendesues(clsArtikullZevendesues artikullZevendesues)
        //{//metoda per modifikimin e artikullZevendesues
        //    try
        //    {
        //        //shtimi i parametrave
        //        dbManager.CreateParameters(4);
        //        dbManager.AddParameters(0, "@IDARTIKULLIZEVENDESUES", artikullZevendesues.IdArtikulliZevendesues, ParameterDirection.Input);
        //        dbManager.AddParameters(1, "@IDARTIKULLIKRYESOR", artikullZevendesues.IdArtikulliKryesor, ParameterDirection.Input);
        //        dbManager.AddParameters(2, "@IDARTIKULLIZEVEND", artikullZevendesues.IdArtikulliZevend, ParameterDirection.Input);
        //        dbManager.AddParameters(3, "@PRIORITETI", artikullZevendesues.Prioriteti, ParameterDirection.Input);
        //        dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_ARTIKULLIZEVENDESUES_upd");
        //        clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgModifikimiMeSukses"]);
        //        return mesazh;
        //    }
        //    catch (Exception)
        //    {
        //        return new clsMesazh(false, ce.Message);
        //    }
        //}

        /// <summary>
        /// ekzekutohet sp-ja prc_T_ARTIKULLIZEVENDESUES_del duke i kaluar id e artikullit zevendesues qe e marrim nga objekti clsArtikullZevendesues qe i kalohet si parameter
        /// </summary>
        /// <param name="idArtikulliZevendesues"> id tab lidhese per artikull dhe artikull zevendesues</param>
        /// <returns>nje objekt clsMesazh qe tregon nese fshirja eshte kryer ne rregull apo jo</returns>
        internal clsMesazh fshiArtikulliZevendesues(int idArtikulliZevendesues)
        {
            dbManager.Open();
            //shtimi i parametrave
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDARTIKULLIZEVENDESUES", idArtikulliZevendesues, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_ARTIKULLIZEVENDESUES_del");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
            return mesazh;
        }
        //[Obsolete("Perdor: clsMesazh fshiArtikulliZevendesues(int idArtikulliZevendesues)", true)]
        //public clsMesazh fshiArtikulliZevendesues(clsArtikullZevendesues artikullZevendesues)
        //{//metoda per fshirjen e artikullZevendesues
        //    try
        //    {
        //        //shtimi i parametrave
        //        dbManager.CreateParameters(1);
        //        dbManager.AddParameters(0, "@IDARTIKULLIZEVENDESUES", artikullZevendesues.IdArtikulliZevendesues, ParameterDirection.Input);
        //        dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_ARTIKULLIZEVENDESUES_del");
        //        clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
        //        return mesazh;
        //    }
        //    catch (Exception)
        //    {
        //        return new clsMesazh(false, ce.Message);
        //    }

        //}

        /// <summary>
        /// kthen objektet artikullin zevendesues sipas idse
        /// </summary>
        ///<param name="idArtikulliZevendesues"> id tab lidhese per artikull dhe artikull zevendesues</param>
        internal void merrArtikulliZevendesues(int idArtikulliZevendesues)
        {
            dbManager.Open();
            //shtimi i parametrave
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDARTIKULLIZEVENDESUES", idArtikulliZevendesues, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_ARTIKULLIZEVENDESUES_sel");

        }
        //[Obsolete("Perdor: merrArtikulliZevendesues(int idArtikulliZevendesues)", true)]
        //public void merrArtikulliZevendesues(clsArtikullZevendesues artikullZevendesues)
        //{// metoda per te marre nje artikullZevendesues NE BAZE TE ID
        //    IDBManager dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        //    dbManager.ConnectionString = dbManager.GetConnectionString();
        //    try
        //    {
        //        dbManager.Open();
        //        //shtimi i parametrave
        //        dbManager.CreateParameters(1);
        //        dbManager.AddParameters(0, "@IDARTIKULLIZEVENDESUES", artikullZevendesues.IdArtikulliZevendesues, ParameterDirection.Input);
        //        dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_ARTIKULLIZEVENDESUES_sel");
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
        /// kthen objektet artikuj zevendesues sipas id artikullit qe zevendesohet
        /// </summary>
        ///<param name="idartikulli">id e artikullit</param>
        ///<returns>nje objekt colArtikujtZevendesues qe permban nje koleksion me te gjitha artikujt zevendesues te ketij artikulli  </returns>
        internal DataTable ktheArtikujZevendesuesSipasIdArtikulli(int idartikulli)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDARTIKULLIKRYESOR", idartikulli, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_ARTIKULLIZEVENDESUES_merrSipasIdArtikulli");

            return ds.Tables[0];

        }
        //[Obsolete("Perdor: DataTable ktheAtikujZevendesuesSipasIdArtikulli(int idartikulli)", true)]
        //public colArtikujtZevendesues merrArtikujZevendesuesSipasIdArtikulli(int idartikulli)
        //{//metoda per te marre ARTIKULLIN ZEVENDESUES sipas id artikulli
        //    IDBManager dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        //    dbManager.ConnectionString = dbManager.GetConnectionString();
        //    try
        //    {
        //        dbManager.Open();
        //        dbManager.CreateParameters(1);
        //        dbManager.AddParameters(0, "@IDARTIKULLIKRYESOR", idartikulli, ParameterDirection.Input);
        //        DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_ARTIKULLIZEVENDESUES_merrSipasIdArtikulli");
        //        colArtikujtZevendesues colArtikujtZevendesues = new colArtikujtZevendesues();
        //        return colArtikujtZevendesues.mbushArrayListArtikujshZevendesues(ds);
        //    }
        //    catch (Exception)
        //    {
        //        return new colArtikujtZevendesues();
        //    }
        //    finally
        //    {
        //        dbManager.Dispose();
        //    }
        //}

        /// <summary>
        /// perdoret per te kontrolluar nqs ekziston  ky artikull zevendesues per kete artikull kryesor 
        /// behet kontrolli per qellime konsistence te informacionit ne DB, nuk lejon te celen i njejti artikull zevendesues dy here per te njejtin artikull
        /// </summary>
        ///<param name="idartikullikryesor"> id e artikullit kryesor</param>
        ///<param name="idartikullizevendesues"> id e artikullit qe e zevendeson</param>
        /// <returns>nje objeckt boolean qe tregon nese ekziston apo jo ky artikull zevendesues per kete artikull kryesor</returns>
        public bool ekzistonArtikulliZevendesues(int idartikullikryesor, int idartikullizevendesues)
        {

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDARTIKULLIKRYESOR", idartikullikryesor, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDARTIKULLIZEVEND", idartikullizevendesues, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_ARTIKULLIZEVENDESUES_ekzistonArtikullZevendesues");
            if (ds.Tables[0].Rows.Count == 1)
                return true;
            else if (ds.Tables[0].Rows.Count == 0)
                return false;
            else return true;

        }

        #endregion

        /// <summary>
        /// Metodat e meposhtme i perkasin veprimeve qe kryhen nga objektet clsArtikullZevendesues dhe colArtikujtZevendesues
        /// </summary>
        #region SKEMAKONTABILITETIARTIKULLI

        /// <summary>
        /// ekzekuton prc_T_SKEMAKONTABILITETIARTIKULLI_ins ne DB dhe kthen statusin e perfundimit te ekzekutimit te SP-se perkatese
        /// Shiko clsMesazh brenda ketij moduli per informacion te metejshem mbi vleren e kthimit te kesaj metode
        /// <param name="idSkemaKontabilitetiArtikulli"> id ritese e skemes se kontabilitetit te artikullit</param>
        /// <param name="kodiSkemaKontabilitetiArtikulli"> kodi i skemes</param>
        /// <param name="pershkrimiSkemaKontabilitetiArtikulli"> pershkrimi i skemes</param>
        /// <param name="klasa"> klasa e artikullit te ciles i perket kjo skeme</param>
        /// <param name="idLlogariInventari"> id e llogarise se inventarit</param>
        /// <param name="idLlogariBlerje"> id e llogarise se blerjes</param>
        /// <param name="idLlogariShitje"> id e llogarise se shitjes</param>
        /// <param name="idLlogariTekTeTretet"> id e llogarise tek te tretet</param>
        /// <param name="idLlogariShpenzimi"> id e llogarise shpenzimeve</param>
        /// <param name="idnderm"> id e ndermarjes</param>
        /// <returns> nje objekt clsMesazh qe tregon nese ruajtja eshte kryer ne rregull apo jo.</returns>
        /// </summary>
        internal clsMesazh ruajSkemaKontabilitetiArtikulli(out int idSkemaKontabilitetiArtikulli, string kodiSkemaKontabilitetiArtikulli, string pershkrimiSkemaKontabilitetiArtikulli, int klasa, int idLlogariInventari, int idLlogariBlerje, int idLlogariShitje, int idLlogariTekTeTretet, int idLlogariShpenzimi, int idLlogariAmortizimi, int idLlogariPakesimi, int idNdermarje, bool llojiArt)
        {
            idSkemaKontabilitetiArtikulli = -1;
            dbManager.Open();
            //shtimi i parametrave
            dbManager.CreateParameters(13);
            dbManager.AddParameters(0, "@IDSKEMAKONTABILITETIARTIKULLI", idSkemaKontabilitetiArtikulli, ParameterDirection.Output);
            dbManager.AddParameters(1, "@KODISKEMAKONTABILITETIARTIKULLI", kodiSkemaKontabilitetiArtikulli, ParameterDirection.Input);
            dbManager.AddParameters(2, "@PERSHKRIMISKEMAKONTABILITETIARTIKULLI", pershkrimiSkemaKontabilitetiArtikulli, ParameterDirection.Input);
            dbManager.AddParameters(3, "@KLASA", klasa, ParameterDirection.Input);
            dbManager.AddParameters(4, "@IDLLOGARIINVENTARI", idLlogariInventari, ParameterDirection.Input);
            dbManager.AddParameters(5, "@IDLLOGARIBLERJE", idLlogariBlerje, ParameterDirection.Input);
            dbManager.AddParameters(6, "@IDLLOGARISHITJE", idLlogariShitje, ParameterDirection.Input);
            dbManager.AddParameters(7, "@IDLLOGARITEKTETRETET", idLlogariTekTeTretet, ParameterDirection.Input);
            dbManager.AddParameters(8, "@IDLLOGARISHPENZIME", idLlogariShpenzimi, ParameterDirection.Input);
            dbManager.AddParameters(9, "@IDLLOGARIAMORTIZIMI", idLlogariAmortizimi, ParameterDirection.Input);
            dbManager.AddParameters(10, "@IDNDERMARJE", idNdermarje, ParameterDirection.Input);
            dbManager.AddParameters(11, "@LLOJIART", llojiArt, ParameterDirection.Input);
            dbManager.AddParameters(12, "@IDLLOGARIPAKESIM", llojiArt, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_SKEMAKONTABILITETIARTIKULLI_ins");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
            return mesazh;

        }
        //[Obsolete("Perdor: clsMesazh ruajSkemaKontabilitetiArtikulli(int idSkemaKontabilitetiArtikulli, string kodiSkemaKontabilitetiArtikulli, string pershkrimiSkemaKontabilitetiArtikulli, int klasa, int idLlogariInventari, int idLlogariBlerje, int idLlogariShitje, int idLlogariTekTeTretet, int idLlogariShpenzimi, int idNdermarje)", true)]
        //public clsMesazh ruajSkemaKontabilitetiArtikulli(clsSkemaKontabilitetiArtikulli skemaKontabilitetiArtikulli)
        //{ //metoda per ruajtjen e SkemaKontabilitetiArtikulli
        //    IDBManager dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        //    dbManager.ConnectionString = dbManager.GetConnectionString();
        //    try
        //    {
        //        dbManager.Open();
        //        //shtimi i parametrave
        //        dbManager.CreateParameters(10);
        //        dbManager.AddParameters(0, "@IDSKEMAKONTABILITETIARTIKULLI", skemaKontabilitetiArtikulli.IdSkemaKontabilitetiArtikulli, ParameterDirection.Output);
        //        dbManager.AddParameters(1, "@KODISKEMAKONTABILITETIARTIKULLI", skemaKontabilitetiArtikulli.KodiSkemaKontabilitetiArtikulli, ParameterDirection.Input);
        //        dbManager.AddParameters(2, "@PERSHKRIMISKEMAKONTABILITETIARTIKULLI", skemaKontabilitetiArtikulli.PershkrimiSkemaKontabilitetiArtikulli, ParameterDirection.Input);
        //        dbManager.AddParameters(3, "@KLASA", skemaKontabilitetiArtikulli.Klasa, ParameterDirection.Input);
        //        dbManager.AddParameters(4, "@IDLLOGARIINVENTARI", skemaKontabilitetiArtikulli.IdLlogariInventari, ParameterDirection.Input);
        //        dbManager.AddParameters(5, "@IDLLOGARIBLERJE", skemaKontabilitetiArtikulli.IdLlogariBlerje, ParameterDirection.Input);
        //        dbManager.AddParameters(6, "@IDLLOGARISHITJE", skemaKontabilitetiArtikulli.IdLlogariShitje, ParameterDirection.Input);
        //        dbManager.AddParameters(7, "@IDLLOGARITEKTETRETET", skemaKontabilitetiArtikulli.IdLlogariTekTeTretet, ParameterDirection.Input);
        //        dbManager.AddParameters(8, "@IDLLOGARISHPENZIME", skemaKontabilitetiArtikulli.IdLlogariShpenzimi, ParameterDirection.Input);
        //        dbManager.AddParameters(9, "@IDNDERMARJE", skemaKontabilitetiArtikulli.IdNdermarje, ParameterDirection.Input);
        //        dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_SKEMAKONTABILITETIARTIKULLI_ins");
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
        /// ekzekuton prc_T_SKEMAKONTABILITETIARTIKULLI_upd ne DB dhe kthen statusin e perfundimit te ekzekutimit te SP-se perkatese
        /// Shiko clsMesazh brenda ketij moduli per informacion te metejshem mbi vleren e kthimit te kesaj metode
        /// <param name="idSkemaKontabilitetiArtikulli"> id ritese e skemes se kontabilitetit te artikullit</param>
        /// <param name="kodiSkemaKontabilitetiArtikulli"> kodi i skemes</param>
        /// <param name="pershkrimiSkemaKontabilitetiArtikulli"> pershkrimi i skemes</param>
        /// <param name="klasa"> klasa e artikullit te ciles i perket kjo skeme</param>
        /// <param name="idLlogariInventari"> id e llogarise se inventarit</param>
        /// <param name="idLlogariBlerje"> id e llogarise se blerjes</param>
        /// <param name="idLlogariShitje"> id e llogarise se shitjes</param>
        /// <param name="idLlogariTekTeTretet"> id e llogarise tek te tretet</param>
        /// <param name="idLlogariShpenzimi"> id e llogarise shpenzimeve</param>
        /// <param name="idnderm"> id e ndermarjes</param>
        /// <returns> nje objekt clsMesazh qe tregon nese modifikimi eshte kryer ne rregull apo jo.</returns>
        /// </summary>
        internal clsMesazh modifikoSkemaKontabilitetiArtikulli(int idSkemaKontabilitetiArtikulli, string kodiSkemaKontabilitetiArtikulli, string pershkrimiSkemaKontabilitetiArtikulli, int klasa, int idLlogariInventari, int idLlogariBlerje, int idLlogariShitje, int idLlogariTekTeTretet, int idLlogariShpenzimi, int idLlogariAmortizimi, int idLlogariPakesimi, int idNdermarje, bool llojiArt)
        {
            dbManager.Open();
            //shtimi i parametrave
            dbManager.CreateParameters(13);
            dbManager.AddParameters(0, "@IDSKEMAKONTABILITETIARTIKULLI", idSkemaKontabilitetiArtikulli, ParameterDirection.Input);
            dbManager.AddParameters(1, "@KODISKEMAKONTABILITETIARTIKULLI", kodiSkemaKontabilitetiArtikulli, ParameterDirection.Input);
            dbManager.AddParameters(2, "@PERSHKRIMISKEMAKONTABILITETIARTIKULLI", pershkrimiSkemaKontabilitetiArtikulli, ParameterDirection.Input);
            dbManager.AddParameters(3, "@KLASA", klasa, ParameterDirection.Input);
            dbManager.AddParameters(4, "@IDLLOGARIINVENTARI", idLlogariInventari, ParameterDirection.Input);
            dbManager.AddParameters(5, "@IDLLOGARIBLERJE", idLlogariBlerje, ParameterDirection.Input);
            dbManager.AddParameters(6, "@IDLLOGARISHITJE", idLlogariShitje, ParameterDirection.Input);
            dbManager.AddParameters(7, "@IDLLOGARITEKTETRETET", idLlogariTekTeTretet, ParameterDirection.Input);
            dbManager.AddParameters(8, "@IDLLOGARISHPENZIME", idLlogariShpenzimi, ParameterDirection.Input);
            dbManager.AddParameters(9, "@IDLLOGARIAMORTIZIMI", idLlogariAmortizimi, ParameterDirection.Input);
            dbManager.AddParameters(10, "@IDNDERMARJE", idNdermarje, ParameterDirection.Input);
            dbManager.AddParameters(11, "@LLOJIART", llojiArt, ParameterDirection.Input);
            dbManager.AddParameters(12, "@IDLLOGARIPAKESIM", idLlogariPakesimi, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_SKEMAKONTABILITETIARTIKULLI_upd");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgModifikimiMeSukses"]);
            return mesazh;

        }
        //[Obsolete("Perdor: clsMesazh modifikoSkemaKontabilitetiArtikulli(int idSkemaKontabilitetiArtikulli, string kodiSkemaKontabilitetiArtikulli, string pershkrimiSkemaKontabilitetiArtikulli, int klasa, int idLlogariInventari, int idLlogariBlerje, int idLlogariShitje, int idLlogariTekTeTretet, int idLlogariShpenzimi, int idNdermarje)", true)]
        //public clsMesazh modifikoSkemaKontabilitetiArtikulli(clsSkemaKontabilitetiArtikulli skemaKontabilitetiArtikulli)
        //{//metoda per modifikimin e skemaKontabilitetiArtikulli
        //    IDBManager dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        //    dbManager.ConnectionString = dbManager.GetConnectionString();
        //    try
        //    {
        //        dbManager.Open();
        //        //shtimi i parametrave
        //        dbManager.CreateParameters(10);
        //        dbManager.AddParameters(0, "@IDSKEMAKONTABILITETIARTIKULLI", skemaKontabilitetiArtikulli.IdSkemaKontabilitetiArtikulli, ParameterDirection.Input);
        //        dbManager.AddParameters(1, "@KODISKEMAKONTABILITETIARTIKULLI", skemaKontabilitetiArtikulli.KodiSkemaKontabilitetiArtikulli, ParameterDirection.Input);
        //        dbManager.AddParameters(2, "@PERSHKRIMISKEMAKONTABILITETIARTIKULLI", skemaKontabilitetiArtikulli.PershkrimiSkemaKontabilitetiArtikulli, ParameterDirection.Input);
        //        dbManager.AddParameters(3, "@KLASA", skemaKontabilitetiArtikulli.Klasa, ParameterDirection.Input);
        //        dbManager.AddParameters(4, "@IDLLOGARIINVENTARI", skemaKontabilitetiArtikulli.IdLlogariInventari, ParameterDirection.Input);
        //        dbManager.AddParameters(5, "@IDLLOGARIBLERJE", skemaKontabilitetiArtikulli.IdLlogariBlerje, ParameterDirection.Input);
        //        dbManager.AddParameters(6, "@IDLLOGARISHITJE", skemaKontabilitetiArtikulli.IdLlogariShitje, ParameterDirection.Input);
        //        dbManager.AddParameters(7, "@IDLLOGARITEKTETRETET", skemaKontabilitetiArtikulli.IdLlogariTekTeTretet, ParameterDirection.Input);
        //        dbManager.AddParameters(8, "@IDLLOGARISHPENZIME", skemaKontabilitetiArtikulli.IdLlogariShpenzimi, ParameterDirection.Input);
        //        dbManager.AddParameters(9, "@IDNDERMARJE", skemaKontabilitetiArtikulli.IdNdermarje, ParameterDirection.Input);
        //        dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_SKEMAKONTABILITETIARTIKULLI_upd");
        //        clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgModifikimiMeSukses"]);
        //        return mesazh;
        //    }
        //    catch (Exception)
        //    {
        //        return new clsMesazh(false, ce.Message);
        //    }
        //}

        /// <summary>
        /// ekzekutohet sp-ja prc_T_SKEMAKONTABILITETIARTIKULLI_del duke i kaluar id e skemes se kontabilitetit te artikullit qe e marrim nga objekti clsSkemaKontabilitetiArtikulli qe i kalohet si parameter
        /// </summary>
        /// <param name="idSkemaKontabilitetiArtikulli"> id ritese e skemes se kontabilitetit te artikullit</param>
        /// <returns>nje objekt clsMesazh qe tregon nese fshirja eshte kryer ne rregull apo jo</returns>
        internal clsMesazh fshiSkemaKontabilitetiArtikulli(int idSkemaKontabilitetiArtikulli)
        {
            dbManager.Open();
            //shtimi i parametrave
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDSKEMAKONTABILITETIARTIKULLI", idSkemaKontabilitetiArtikulli, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_SKEMAKONTABILITETIARTIKULLI_del");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
            return mesazh;

        }
        //[Obsolete("Perdor: clsMesazh fshiSkemaKontabilitetiArtikulli(int idSkemaKontabilitetiArtikulli)", true)]
        //public clsMesazh fshiSkemaKontabilitetiArtikulli(clsSkemaKontabilitetiArtikulli skemaKontabilitetiArtikulli)
        //{//metoda per fshirjen e skemaKontabilitetiArtikulli
        //    IDBManager dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        //    dbManager.ConnectionString = dbManager.GetConnectionString();
        //    try
        //    {
        //        dbManager.Open();
        //        //shtimi i parametrave
        //        dbManager.CreateParameters(1);
        //        dbManager.AddParameters(0, "@IDSKEMAKONTABILITETIARTIKULLI", skemaKontabilitetiArtikulli.IdSkemaKontabilitetiArtikulli, ParameterDirection.Input);
        //        dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_SKEMAKONTABILITETIARTIKULLI_del");
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
        /// kthen datarow skema kontabiliteti artikulli sipas idse
        /// </summary>
        ///<param name="id">id e skemes te kontabilitetit te artikullit</param>
        ///<returns>nje datarow qe mban skemen me kete id  </returns>
        internal DataRow ktheSkemaKontabilitetiArtikulli(int id)
        {
            dbManager.Open();
            //shtimi i parametrave
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDSKEMAKONTABILITETIARTIKULLI", id, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_SKEMAKONTABILITETIARTIKULLI_sel");

            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];


        }
        //[Obsolete("Perdor: DataRow merrSkemaKontabilitetiArtikulli(int id)", true)]
        //public colSkematKontabilitetiArtikulli merrSkemaKontabilitetiArtikulli(int id)
        //{// metoda per te marre nje skemaKontabilitetiArtikulli NE BAZE TE ID
        //    IDBManager dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        //    dbManager.ConnectionString = dbManager.GetConnectionString();
        //    try
        //    {
        //        dbManager.Open();
        //        //shtimi i parametrave
        //        dbManager.CreateParameters(1);
        //        dbManager.AddParameters(0, "@IDSKEMAKONTABILITETIARTIKULLI", id, ParameterDirection.Input);
        //        DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_SKEMAKONTABILITETIARTIKULLI_sel");
        //        colSkematKontabilitetiArtikulli colSkematKontabilitetiArtikulli = new colSkematKontabilitetiArtikulli();
        //        return colSkematKontabilitetiArtikulli.mbushArrayListSkemashKontabilitetiArtikulli(ds);

        //    }
        //    catch (Exception)
        //    { return new colSkematKontabilitetiArtikulli();
        //    }
        //    finally
        //    {
        //        dbManager.Dispose();
        //    }
        //}

        /// <summary>
        /// kthen datatable skema kontabiliteti artikulli 
        /// </summary>
        ///<returns>nje datatable qe permban nje koleksion me te gjitha skemat  </returns>
        internal DataTable ktheSkemaKontabilitetiArtikulliTeGjitha()
        {
            dbManager.Open();
            //shtimi i parametrave               
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_SKEMAKONTABILITETIARTIKULLI_merrTeGjithe");

            return ds.Tables[0];


        }




        //[Obsolete("Perdor: DataTable ktheSkemaKontabilitetiArtikulliTeGjitha()", true)]
        //public colSkematKontabilitetiArtikulli merrSkemaKontabilitetiArtikulliTeGjitha()
        //{// metoda per te marre nje skemaKontabilitetiArtikulli NE BAZE TE ID
        //    IDBManager dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        //    dbManager.ConnectionString = dbManager.GetConnectionString();
        //    try
        //    {
        //        dbManager.Open();
        //        //shtimi i parametrave               
        //        DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_SKEMAKONTABILITETIARTIKULLI_merrTeGjithe");
        //        colSkematKontabilitetiArtikulli colSkematKontabilitetiArtikulli = new colSkematKontabilitetiArtikulli();
        //        return colSkematKontabilitetiArtikulli.mbushArrayListSkemashKontabilitetiArtikulli(ds);

        //    }
        //    catch (Exception)
        //    {
        //        return new colSkematKontabilitetiArtikulli();
        //    }
        //    finally
        //    {
        //        dbManager.Dispose();
        //    }
        //}

        /// <summary>
        /// kthen datatable skema kontabiliteti artikulli sipas idse te ndermarjes
        /// </summary>
        ///<param name="idndermarje">id e ndermarjes</param>
        ///<returns>nje datatable qe permban nje koleksion me te gjitha skemat te kesaj ndermarje  </returns>
        internal DataTable ktheSkemaKontabilitetiArtikulliTeGjithaSipasNdermarjes(int idndermarje)
        {

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDNDERMARJE", idndermarje, ParameterDirection.Input);

            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_SKEMAKONTABILITETIARTIKULLI_merrTeGjitheSipasNdermarjes");

            return ds.Tables[0];


        }

        /// <summary>
        /// kthen datatable skema kontabiliteti artikulli sipas idse te ndermarjes dhe llojit te artikullit
        /// </summary>
        ///<param name="idndermarje">id e ndermarjes</param>
        ///<param name="llojiArt">lloji i artikullit</param>
        ///<returns>nje datatable qe permban nje koleksion me te gjitha skemat te kesaj ndermarje me ate tip artikulli </returns>
        internal DataTable ktheSkemaKontabilitetiArtikulliTeGjithaSipasNdermarjesDheLlojit(int idndermarje, bool llojiArt, int idKlasa)
        {
            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddInputParameters("@IDNDERMARJE", idndermarje);
            dbManager.AddInputParameters("@LLOJIART", llojiArt);
            dbManager.AddInputParameters("@IDKLASA", idKlasa);
            return dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_SKEMAKONTABILITETIARTIKULLI_merrTeGjitheSipasNdermarjesDheLlojit").Tables[0];


        }
        //[Obsolete("Perdor: DataTable ktheSkemaKontabilitetiArtikulliTeGjithaSipasNdermarjes(int idndermarje)", true)]
        //public colSkematKontabilitetiArtikulli merrSkemaKontabilitetiArtikulliTeGjithaSipasNdermarjes(int idndermarje)
        //{// metoda per te marre nje skemaKontabilitetiArtikulli NE BAZE TE ID
        //    IDBManager dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        //    dbManager.ConnectionString = dbManager.GetConnectionString();
        //    try
        //    {
        //        dbManager.Open();
        //        //shtimi i parametrave 
        //        ;
        //        dbManager.CreateParameters(1);
        //        dbManager.AddParameters(0, "@IDNDERMARJE", idndermarje, ParameterDirection.Input);

        //        DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_SKEMAKONTABILITETIARTIKULLI_merrTeGjitheSipasNdermarjes");
        //        colSkematKontabilitetiArtikulli colSkematKontabilitetiArtikulli = new colSkematKontabilitetiArtikulli();
        //        return colSkematKontabilitetiArtikulli.mbushArrayListSkemashKontabilitetiArtikulli(ds);

        //    }
        //    catch (Exception)
        //    {
        //        return new colSkematKontabilitetiArtikulli();
        //    }
        //    finally
        //    {
        //        dbManager.Dispose();
        //    }
        //}

        /// <summary>
        /// kthen datatable skema kontabiliteti artikulli sipas idse te ndermarjes dhe klases
        /// </summary>
        ///<param name="idnderm"> id e ndermarjes</param>
        ///<param name="klasa"> id e klases</param>
        ///<returns>nje objekt colSkematKontabilitetiArtikulli qe permban nje koleksion me te gjitha skemat te kesaj ndermarje dhe te kesaj klase  </returns>
        internal DataTable ktheSkemaKontabilitetiArtikulliSipasKlases(int klasa, int idnderm)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@KLASA", klasa, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_SKEMAKONTABILITETIARTIKULLI_merrSipasKlases");

            return ds.Tables[0];

        }
        //[Obsolete("Perdor: DataTable ktheSkemaKontabilitetiArtikulliSipasKlases(int klasa, int idnderm)", true)]
        //public colSkematKontabilitetiArtikulli merrSkemaKontabilitetiArtikulliSipasKlases(int klasa, int idnderm)
        //{//metoda per te marre te skemat kontabiliteti te artikullit sipas klases
        //    IDBManager dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        //    dbManager.ConnectionString = dbManager.GetConnectionString();
        //    try
        //    {
        //        dbManager.Open();
        //        dbManager.CreateParameters(2);
        //        dbManager.AddParameters(0, "@KLASA", klasa , ParameterDirection.Input);
        //        dbManager.AddParameters(1, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
        //        DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_SKEMAKONTABILITETIARTIKULLI_merrSipasKlases");
        //        colSkematKontabilitetiArtikulli colSkematKontabilitetiArtikulli = new colSkematKontabilitetiArtikulli();
        //        return colSkematKontabilitetiArtikulli.mbushArrayListSkemashKontabilitetiArtikulli(ds);
        //    }
        //    catch (Exception)
        //    {
        //        return new colSkematKontabilitetiArtikulli();
        //    }
        //    finally
        //    {
        //        dbManager.Dispose();
        //    }
        //}

        /// <summary>
        /// kthen datarow skema kontabiliteti artikulli sipas idse te ndermarjes me kete kod
        /// </summary>
        ///<param name="idnderm"> id e ndermarjes</param>
        ///<param name="kodi"> kodi i skemes</param>
        ///<returns>nje datarow qe mban skemen e kesaj ndermarje me kete kod  </returns>
        internal DataRow ktheSkemaKontabilitetiArtikulliSipasKodit(String kodi, int idnderm, bool llojart)
        {

            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@KODISKEMAKONTABILITETIARTIKULLI", kodi, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            dbManager.AddParameters(2, "@LLOJIART", llojart, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_SKEMAKONTABILITETIARTIKULLI_merrSipasKodit");


            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];

        }
        /// <summary>
        /// kthen id e skema kontabiliteti artikulli sipas idse te ndermarjes me kete kod
        /// </summary>
        ///<param name="idnderm"> id e ndermarjes</param>
        ///<param name="kodi"> kodi i skemes</param>
        ///<returns>nje id te skemes te kesaj ndermarje me kete kod  </returns>
        internal int ktheIdSkemaKontabilitetiArtikulliSipasKodit(String kodi, int idnderm, bool llojart)
        {

            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@KODISKEMAKONTABILITETIARTIKULLI", kodi, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            dbManager.AddParameters(2, "@LLOJIART", llojart, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_SKEMAKONTABILITETIARTIKULLI_merrSipasKodit");

            if (ds == null)
                return -1;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return -1;
            int idSkema;
            int.TryParse(ds.Tables[0].Rows[0]["IDSKEMAKONTABILITETIARTIKULLI"].ToString(), out idSkema);
            return idSkema;

        }
        //[Obsolete("Perdor: DataRow ktheSkemaKontabilitetiArtikulliSipasKodit(String kodi, int idnderm) ose int ktheIdSkemaKontabilitetiArtikulliSipasKodit(String kodi, int idnderm)", true)]
        //public colSkematKontabilitetiArtikulli merrSkemaKontabilitetiArtikulliSipasKodit(String kodi, int idnderm)
        //{//metoda per te marre te skemakontabilitetiartikulli sipas kodit
        //    IDBManager dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        //    dbManager.ConnectionString = dbManager.GetConnectionString();
        //    try
        //    {
        //        dbManager.Open();
        //        dbManager.CreateParameters(2);
        //        dbManager.AddParameters(0, "@KODISKEMAKONTABILITETIARTIKULLI", kodi, ParameterDirection.Input);
        //        dbManager.AddParameters(1, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
        //        DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_SKEMAKONTABILITETIARTIKULLI_merrSipasKodit");
        //        colSkematKontabilitetiArtikulli colSkematKontabilitetiArtikulli = new colSkematKontabilitetiArtikulli();
        //        return colSkematKontabilitetiArtikulli.mbushArrayListSkemashKontabilitetiArtikulli(ds);
        //    }
        //    catch (Exception)
        //    {
        //        return new colSkematKontabilitetiArtikulli();
        //    }
        //    finally
        //    {
        //        dbManager.Dispose();
        //    }
        //}

        #endregion

        /// <summary>
        /// Metodat e meposhtme i perkasin veprimeve qe kryhen nga objektet clsArtikullVfone dhe colArtikullVfone
        /// </summary>
        #region ARTIKULLI VFONE



        internal clsMesazh ruajArtikullVfone(out int id, int idartikull, string kodvfone, decimal pike, decimal vlere)
        {
            id = -1;

            try
            {
                dbManager.Open();
                dbManager.CreateParameters(5);
                dbManager.AddParameters(0, "@ID", id, ParameterDirection.Output);
                dbManager.AddParameters(1, "@IDARTIKULLI", idartikull, ParameterDirection.Input);
                dbManager.AddParameters(2, "@KODVFONE", kodvfone, ParameterDirection.Input);
                dbManager.AddParameters(3, "@PIKE", pike, ParameterDirection.Input);
                dbManager.AddParameters(4, "@VLERE", vlere, ParameterDirection.Input);

                dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_ARTIKULLVFONE_ins");
                clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
                return mesazh;
            }
            catch (Exception ce)
            {
                return new clsMesazh(false, ce.Message);
            }
        }

        /// <summary>
        /// kthen datatable artikuj perberes sipas id artikullit kryesor
        /// </summary>
        ///<param name="id">id e artikullit kryesor</param>
        ///<returns>nje datatable qe permban nje koleksion me te gjitha artikujt perberes te ketij artikulli  </returns>
        internal DataTable ktheArtikujVfoneSipasIdArt(int id)
        {

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDARTIKULLI", id, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_ARTIKULLVFONE_merrSipasIdArtikulli");

            return ds.Tables[0];

        }


        /// <summary>
        /// ekzekutohet sp-ja prc_T_ARTIKULLIPERBERES_del duke i kaluar id e artikullit kryesor qe e marrim nga objekti clsArtikulli qe i kalohet si parameter
        /// </summary>
        /// <param name="artikull"> artikulli kryesor nga i cili do te fshihen te gjithe artikujt perberes </param>
        /// <returns>nje objekt clsMesazh qe tregon nese fshirja eshte kryer ne rregull apo jo</returns>
        internal clsMesazh fshiArtikullVfone(int idArtikulli)
        {

            try
            {
                dbManager.Open();
                dbManager.CreateParameters(1);
                dbManager.AddParameters(0, "@IDARTIKULLI", idArtikulli, ParameterDirection.Input);
                dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_ARTIKULLVFONE_del");
                clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
                return mesazh;
            }
            catch (Exception ce)
            {
                return new clsMesazh(false, ce.Message);
            }
        }

        #endregion
        /// <summary>
        /// Metodat e meposhtme i perkasin veprimeve qe kryhen nga objektet clsArtikulli dhe colArtikujt
        /// </summary>
        #region ARTIKULLI

        /// <summary>
        /// ekzekuton prc_T_ARTIKULLI_ins ne DB dhe kthen statusin e perfundimit te ekzekutimit te SP-se perkatese
        /// Shiko clsMesazh brenda ketij moduli per informacion te metejshem mbi vleren e kthimit te kesaj metode 
        ///  </summary>
        /// <param name="idArtikulli"> id ritese e artikullit</param>
        /// <param name="kodArtikulli"> kodi i artkullit</param>
        /// <param name="pershkrimArtikulli"> pershkrimi i artikullit</param>
        /// <param name="pershkrimiAngArtikulli">pershkrimi i artikullit ne anglisht</param>
        /// <param name="kodiDoganorArtikulli">kodi doganor i artikullit</param>
        /// <param name="vendodhjeArtikulli">vendodhja e artikullit</param>
        /// <param name="kodifikimi1Artikulli">kodifikimi 1 i artikullit</param>
        /// <param name="kodifikimi2Artikulli"> kodifikimi 2 i artikullit</param>
        /// <param name="origjineArtikulli"> origjina e artikullit</param>
        /// <param name="njesi1Artikulli"> njesia e pare e artikullit</param>
        /// <param name="njesi2Artikulli">njesia e dyte e artikullit</param>
        /// <param name="koeficientArtikulli"> koeficienti midis njesive te artikullit</param>
        /// <param name="idFurnitoriKryesor">id e furnitorit kryesor</param>
        /// <param name="peshaBrutoArtikulli">pesha bruto e artikullit</param>
        /// <param name="peshaNetoArtikulli">pesha neto e artikullit</param>
        /// <param name="detajimArtikulli">detajimi i artikullit</param>
        /// <param name="klasa">klasa e artikullit</param>
        /// <param name="idSkemaKontabilitetiArtikulli">skema e kontabilitetit te artikullit</param>
        /// <param name="idLlogariInventari"> id llogari inventari</param>
        /// <param name="idLlogariBlerje">id llogari blerje</param>
        /// <param name="idLlogariShitje"> id llogari shitje</param>
        /// <param name="idLlogariTeTrete"> id llogari tek te tretet</param>
        /// <param name="idLlogariShpenzime"> id llogari shpenzimesh</param>
        /// <param name="minimumArtikulli">sasia min e artikullit</param>
        /// <param name="maximumArtikulli"> sasia max e artikullit</param>
        /// <param name="metodeKostojeArtikulli"> metode kostoje e artikullit</param>
        /// <param name="llogaritjaKMSHArtikulli"> llogaritja KMSH e artikullit</param>
        /// <param name="zevendesimAutomatikArtikulli"> zevendesimi automatik i artikullit</param>
        /// <param name="idPerdoruesi"> id e perdoruesit qe ka kryer veprimin</param>
        /// <param name="idNderViti"> id e ndermarje vitit</param>
        /// <param name="idnderm"> id e ndermarjes</param>
        /// <param name="kontrollgjendje"> kontrolli mbi gjendjen e artikullit</param>
        /// <param name="kontrollcmimi"> kontrolli i cmimit te artikullit</param>
        /// <param name="kontrollgjendjeartikulli"> kontrolli i gjendjes se detajimit artikullit</param>
        /// <param name="Tvsh">tvsh</param>
        /// <param name="idkonfig">id e konfigurimit</param>
        /// <param name="aktiv">aktiv</param>
        /// <param name="meSerial">merr nese artikulli do te jete me serial unik per cdo njesi apo per nje grup sasish te percaktuara ne nje dokument hyrjeje ose blerjeje.</param>
        /// <returns> nje objekt clsMesazh qe tregon nese ruajtja eshte kryer ne rregull apo jo.</returns>
        /// <param name="prodhimmeprososi">prodhim me porosi</param>
        internal int ruajArt(int idArtikulli, string kodArtikulli, string pershkrimArtikulli, string pershkrimiAngArtikulli, string kodiDoganorArtikulli, string vendodhjeArtikulli, int kodifikimi1Artikulli, int kodifikimi2Artikulli, string origjineArtikulli, int njesi1Artikulli, int njesi2Artikulli, decimal koeficientArtikulli, int idFurnitoriKryesor, decimal peshaBrutoArtikulli, decimal peshaNetoArtikulli, bool detajimArtikulli, int klasa, int idSkemaKontabilitetiArtikulli, int idLlogariInventari, int idLlogariBlerje, int idLlogariShitje, int idLlogariTeTrete, int idLlogariPakesim, int idLlogariShpenzime, int idLlogariAmortizimi, int idLlogariRezerve, int idLlogariPakesimRez, decimal minimumArtikulli, decimal maximumArtikulli, int metodeKostojeArtikulli, int llogaritjaKMSHArtikulli, int zevendesimAutomatikArtikulli, int idPerdoruesi, int idnderm, bool kontrollgjendje, bool kontrollcmimi, bool kontrollgjendjeartikulli, int Tvsh, int idkonfig, bool aktiv, int idStatusdok, bool llojiArt, decimal sasinjesi, decimal scrap, bool prodhimmeprososi, int idkategoridetajimi, int idKategoriDetajimi2, bool kontrollGjendjeDetajim2, int idobjektivakosto, int idLlojGarancie, decimal garancia, int idmag, bool irezervueshem, bool perTransferim, bool loan, bool dhurate, int aplikimdhurate, decimal pike, decimal vlere, string kodvfone, bool meSeriale, bool iShitshem, bool mbetjeshitshme, int idArtRaportuesi, bool perPeshore, string pershkrimFurnitori, string siperfaqjam2, string nrKontrate, string nrPasurie, string zonaKadastrale, string shasi, string marka, string modeli, string vitProdhimi, string tedhenateknike, bool meBarkodLogjik, string skemaBarkodit, int kodifikimi3Artikulli, bool aparatBazaar, string kodOferte, bool artikullIVjeter, int idkategoriseriali, bool merezerverivleresimi, int nrKaraktereTAC, bool llogaritKomision, int idLlogariKomisioni, int stokumaxvfone, string kodiibarit, bool irimbursueshem)
        {

            dbManager.Open();
            dbManager.CreateParameters(89);
            dbManager.AddParameters(0, "@IDARTIKULLI", idArtikulli, ParameterDirection.Output);
            dbManager.AddParameters(1, "@KODARTIKULLI", kodArtikulli, ParameterDirection.Input);
            dbManager.AddParameters(2, "@PERSHKRIMARTIKULLI", pershkrimArtikulli, ParameterDirection.Input);
            dbManager.AddParameters(3, "@PERSHKRIMIANGARTIKULLI", pershkrimiAngArtikulli, ParameterDirection.Input);
            dbManager.AddParameters(4, "@KODIDOGANORARTIKULLI", kodiDoganorArtikulli, ParameterDirection.Input);
            dbManager.AddParameters(5, "@VENDODHJEARTIKULLI", vendodhjeArtikulli, ParameterDirection.Input);
            if (kodifikimi1Artikulli == 0)
                dbManager.AddParameters(6, "@KODIFIKIMI1ARTIKULLI", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(6, "@KODIFIKIMI1ARTIKULLI", kodifikimi1Artikulli, ParameterDirection.Input);
            if (kodifikimi2Artikulli == 0)
                dbManager.AddParameters(7, "@KODIFIKIMI2ARTIKULLI", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(7, "@KODIFIKIMI2ARTIKULLI", kodifikimi2Artikulli, ParameterDirection.Input);
            dbManager.AddParameters(8, "@ORIGJINEARTIKULLI", origjineArtikulli, ParameterDirection.Input);
            if (njesi1Artikulli == 0)
                dbManager.AddParameters(9, "@NJESI1ARTIKULLI", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(9, "@NJESI1ARTIKULLI", njesi1Artikulli, ParameterDirection.Input);
            if (njesi2Artikulli == 0)
                dbManager.AddParameters(10, "@NJESI2ARTIKULLI", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(10, "@NJESI2ARTIKULLI", njesi2Artikulli, ParameterDirection.Input);
            dbManager.AddParameters(11, "@KOEFICENTARTIKULLI", koeficientArtikulli, ParameterDirection.Input);
            if (idFurnitoriKryesor == 0)
                dbManager.AddParameters(12, "@IDFURNITORIKRYESOR", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(12, "@IDFURNITORIKRYESOR", idFurnitoriKryesor, ParameterDirection.Input);
            dbManager.AddParameters(13, "@PESHABRUTOARTIKULLI", peshaBrutoArtikulli, ParameterDirection.Input);
            dbManager.AddParameters(14, "@PESHANETOARTIKULLI", peshaNetoArtikulli, ParameterDirection.Input);
            dbManager.AddParameters(15, "@DETAJIMARTIKULLI", detajimArtikulli, ParameterDirection.Input);
            if (klasa == 0)
                dbManager.AddParameters(16, "@KLASA", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(16, "@KLASA", klasa, ParameterDirection.Input);
            if (idSkemaKontabilitetiArtikulli == 0 || idSkemaKontabilitetiArtikulli == -1)
                dbManager.AddParameters(17, "@IDSKEMAKONTABILITETIARTIKULLI", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(17, "@IDSKEMAKONTABILITETIARTIKULLI", idSkemaKontabilitetiArtikulli, ParameterDirection.Input);
            if (idLlogariInventari == 0 || idLlogariInventari == -1)
                dbManager.AddParameters(18, "@IDLLOGARIINVENTARI", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(18, "@IDLLOGARIINVENTARI", idLlogariInventari, ParameterDirection.Input);
            if (idLlogariBlerje == 0 || idLlogariBlerje == -1)
                dbManager.AddParameters(19, "@IDLLOGARIBLERJE", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(19, "@IDLLOGARIBLERJE", idLlogariBlerje, ParameterDirection.Input);
            if (idLlogariShitje == 0 || idLlogariShitje == -1)
                dbManager.AddParameters(20, "@IDLLOGARISHITJE", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(20, "@IDLLOGARISHITJE", idLlogariShitje, ParameterDirection.Input);
            if (idLlogariTeTrete == 0 || idLlogariTeTrete == -1)
                dbManager.AddParameters(21, "@IDLLOGARITETRETE", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(21, "@IDLLOGARITETRETE", idLlogariTeTrete, ParameterDirection.Input);
            if (idLlogariShpenzime == 0 || idLlogariShpenzime == -1)
                dbManager.AddParameters(22, "@IDLLOGARISHPENZIME", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(22, "@IDLLOGARISHPENZIME", idLlogariShpenzime, ParameterDirection.Input);
            if (idLlogariAmortizimi == 0 || idLlogariAmortizimi == -1)
                dbManager.AddParameters(23, "@IDLLOGARIAMORTIZIMI", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(23, "@IDLLOGARIAMORTIZIMI", idLlogariAmortizimi, ParameterDirection.Input);
            dbManager.AddParameters(24, "@MINIMUMARTIKULLI", minimumArtikulli, ParameterDirection.Input);
            dbManager.AddParameters(25, "@MAXIMUMARTIKULLI", maximumArtikulli, ParameterDirection.Input);
            if (metodeKostojeArtikulli == 0)
                dbManager.AddParameters(26, "@METODEKOSTOJEARTIKULLI", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(26, "@METODEKOSTOJEARTIKULLI", metodeKostojeArtikulli, ParameterDirection.Input);
            dbManager.AddParameters(27, "@LLOGARITJAKMSHARTIKULLI", llogaritjaKMSHArtikulli, ParameterDirection.Input);
            dbManager.AddParameters(28, "@ZEVENDESIMAUTOMATIKARTIKULLI", zevendesimAutomatikArtikulli, ParameterDirection.Input);
            dbManager.AddParameters(29, "@IDPERDORUESI", idPerdoruesi, ParameterDirection.Input);
            dbManager.AddParameters(30, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            dbManager.AddParameters(31, "@KONTROLLGJENDJE", kontrollgjendje, ParameterDirection.Input);
            dbManager.AddParameters(32, "@KONTROLLCMIMI", kontrollcmimi, ParameterDirection.Input);
            dbManager.AddParameters(33, "@KONTROLLGJENDJEARTIKULLI", kontrollgjendjeartikulli, ParameterDirection.Input);
            if (Tvsh == 0)
                dbManager.AddParameters(34, "@IDTVSH", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(34, "@IDTVSH", Tvsh, ParameterDirection.Input);
            dbManager.AddParameters(35, "@IDKONFIG", idkonfig, ParameterDirection.Input);
            dbManager.AddParameters(36, "@AKTIV", aktiv, ParameterDirection.Input);
            dbManager.AddParameters(37, "@IDSTATUSDOK", idStatusdok, ParameterDirection.Input);
            dbManager.AddParameters(38, "@LLOJIART", llojiArt, ParameterDirection.Input);
            dbManager.AddParameters(39, "@SASINJESI", sasinjesi, ParameterDirection.Input);
            dbManager.AddParameters(40, "@SCRAP", scrap, ParameterDirection.Input);
            dbManager.AddParameters(41, "@PRODHIMMEPOROSI", prodhimmeprososi, ParameterDirection.Input);
            if (idkategoridetajimi == 0 || idkategoridetajimi == -1)
                dbManager.AddParameters(42, "@IDKATEGORIDETAJIMI", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(42, "@IDKATEGORIDETAJIMI", idkategoridetajimi, ParameterDirection.Input);
            if (idKategoriDetajimi2 == 0 || idKategoriDetajimi2 == -1)
                dbManager.AddParameters(43, "@IDKATEGORIDETAJIMI2", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(43, "@IDKATEGORIDETAJIMI2", idKategoriDetajimi2, ParameterDirection.Input);
            dbManager.AddParameters(44, "@KONTROLLGJENDJEDETAJIM2", kontrollGjendjeDetajim2, ParameterDirection.Input);
            if (idobjektivakosto == 0 || idobjektivakosto == -1)
                dbManager.AddParameters(45, "@IDOBJEKTIVAKOSTO", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(45, "@IDOBJEKTIVAKOSTO", idobjektivakosto, ParameterDirection.Input);
            if (idLlojGarancie == 0 || idLlojGarancie == -1)
                dbManager.AddParameters(46, "@IDLLOJGARANCIA", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(46, "@IDLLOJGARANCIA", idLlojGarancie, ParameterDirection.Input);
            dbManager.AddParameters(47, "@GARANCIA", garancia, ParameterDirection.Input);
            if (idmag == 0 || idmag == -1)
                dbManager.AddParameters(48, "@IDMAGAZINA", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(48, "@IDMAGAZINA", idmag, ParameterDirection.Input);
            dbManager.AddParameters(49, "@IREZERVUESHEM", irezervueshem, ParameterDirection.Input);
            dbManager.AddParameters(50, "@PERTRANSFERIM", perTransferim, ParameterDirection.Input);
            dbManager.AddParameters(51, "@LOAN", loan, ParameterDirection.Input);
            dbManager.AddParameters(52, "@DHURATE", dhurate, ParameterDirection.Input);
            if (aplikimdhurate == 0)
                dbManager.AddParameters(53, "@APLIKIMDHURATE", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(53, "@APLIKIMDHURATE", aplikimdhurate, ParameterDirection.Input);
            dbManager.AddParameters(54, "@PIKE", pike, ParameterDirection.Input);
            dbManager.AddParameters(55, "@VLERE", vlere, ParameterDirection.Input);
            dbManager.AddParameters(56, "@KODVFONE", kodvfone, ParameterDirection.Input);
            dbManager.AddParameters(57, "@MESERIAL", meSeriale, ParameterDirection.Input);
            if (idLlogariPakesim == 0 || idLlogariPakesim == -1)
                dbManager.AddParameters(58, "@IDLLOGARIPAKESIM", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(58, "@IDLLOGARIPAKESIM", idLlogariPakesim, ParameterDirection.Input);
            dbManager.AddParameters(59, "@ISHITSHEM", iShitshem, ParameterDirection.Input);
            dbManager.AddParameters(60, "@MBETJESHITJSHME", mbetjeshitshme, ParameterDirection.Input);
            if (idArtRaportuesi == 0 || idArtRaportuesi == -1)
                dbManager.AddParameters(61, "@IDARTRAPORTUESI", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(61, "@IDARTRAPORTUESI", idArtRaportuesi, ParameterDirection.Input);
            dbManager.AddParameters(62, "@PERPESHORE", perPeshore, ParameterDirection.Input);
            dbManager.AddParameters(63, "@PERSHKRIMTEFURNITORI", pershkrimFurnitori, ParameterDirection.Input);
            dbManager.AddParameters(64, "@SIPERFAQJAM2", siperfaqjam2, ParameterDirection.Input);
            dbManager.AddParameters(65, "@NRKONTRATE", nrKontrate, ParameterDirection.Input);
            dbManager.AddParameters(66, "@NRPASURIE", nrPasurie, ParameterDirection.Input);
            dbManager.AddParameters(67, "@ZONAKADASTRALE", zonaKadastrale, ParameterDirection.Input);
            dbManager.AddParameters(68, "@SHASIA", shasi, ParameterDirection.Input);
            dbManager.AddParameters(69, "@MARKA", marka, ParameterDirection.Input);
            dbManager.AddParameters(70, "@MODELI", modeli, ParameterDirection.Input);
            dbManager.AddParameters(71, "@VITPRODHIMI", vitProdhimi, ParameterDirection.Input);
            dbManager.AddParameters(72, "@TEDHENATEKNIKE", tedhenateknike, ParameterDirection.Input);
            dbManager.AddParameters(73, "@MEBARKODLOGJIK", meBarkodLogjik, ParameterDirection.Input);
            dbManager.AddParameters(74, "@SKEMABARKODIT", skemaBarkodit, ParameterDirection.Input);
            if (kodifikimi3Artikulli == 0)
                dbManager.AddParameters(75, "@KODIFIKIMI3ARTIKULLI", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(75, "@KODIFIKIMI3ARTIKULLI", kodifikimi3Artikulli, ParameterDirection.Input);
            dbManager.AddParameters(76, "@APARATBAZAAR", aparatBazaar, ParameterDirection.Input);
            dbManager.AddParameters(77, "@KODOFERTE", kodOferte, ParameterDirection.Input);
            dbManager.AddParameters(78, "@ARTIKULLIVJETER", artikullIVjeter, ParameterDirection.Input);
            if (idkategoriseriali == 0)
                dbManager.AddParameters(79, "@IDKATEGORISERIALI", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(79, "@IDKATEGORISERIALI", idkategoriseriali, ParameterDirection.Input);
            dbManager.AddParameters(80, "@REZERVERIVLERESIMI", merezerverivleresimi, ParameterDirection.Input);
            if (idLlogariRezerve == 0 || idLlogariRezerve == -1)
                dbManager.AddParameters(81, "@IDLLOGARIREZERVE", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(81, "@IDLLOGARIREZERVE", idLlogariRezerve, ParameterDirection.Input);
            if (idLlogariPakesimRez == 0 || idLlogariPakesimRez == -1)
                dbManager.AddParameters(82, "@IDLLOGARIPAKESIMREZ", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(82, "@IDLLOGARIPAKESIMREZ", idLlogariPakesimRez, ParameterDirection.Input);
            dbManager.AddParameters(83, "@KARAKTERETAC", nrKaraktereTAC, ParameterDirection.Input);
            dbManager.AddParameters(84, "@LLOGARITKOMISION", llogaritKomision, ParameterDirection.Input);
            dbManager.AddParameters(85, "@IDLLOGARIKOMISIONI", idLlogariKomisioni, ParameterDirection.Input);
            dbManager.AddParameters(86, "@STOKUMAXVFONE", stokumaxvfone, ParameterDirection.Input);
            dbManager.AddParameters(87, "@KODIIBARIT", kodiibarit, ParameterDirection.Input);
            dbManager.AddParameters(88, "@IRIMBURSUESHEM", irimbursueshem, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_ARTIKULLI_ins");
            idArtikulli = int.Parse(dbManager.Parameters[0].Value.ToString());
            return idArtikulli;

        }

        public DataTable merrArtikujSipasIdve(int idDok)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@idDok", idDok, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_ARTIKULLI_merrArtikujSipasIdDok");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0)
                return null;
            return ds.Tables[0];
        }

        public DataTable merrArtikujSipasIdAmortizimi(int idDok)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@idDok", idDok, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_ARTIKULLI_merrArtikujSipasIdDokAmortizimi");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0)
                return null;
            return ds.Tables[0];
        }
        public DataTable merrArtikujSipasIdve(List<int> idArtikuj)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@idArtikuj", String.Join(",", idArtikuj), ParameterDirection.Input);
            return dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_ARTIKULLI_merrArtikujSipasIdve").Tables[0];
        }
        public DataTable merrArtikuj(int id, String kodet)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@id", id, ParameterDirection.Input);
            dbManager.AddParameters(1, "@VLERA", kodet, ParameterDirection.Input);
            return dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_ARTIKULLI_merrArtikujqeNukKaKategorineESerialitTeDuhur").Tables[0];


        }
        public DataTable merrArtikujFatureMag(int idDok)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@idDok", idDok, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_ARTIKULLI_merrArtikujSipasIdDokMag");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0)
                return null;
            return ds.Tables[0];
        }
        /// <summary>
        /// ekzekuton prc_T_ARTIKULLI_upd ne DB dhe kthen statusin e perfundimit te ekzekutimit te SP-se perkatese
        /// Shiko clsMesazh brenda ketij moduli per informacion te metejshem mbi vleren e kthimit te kesaj metode
        /// </summary>
        /// <param name="idArtikulli"> id ritese e artikullit</param>
        /// <param name="kodArtikulli"> kodi i artkullit</param>
        /// <param name="pershkrimArtikulli"> pershkrimi i artikullit</param>
        /// <param name="pershkrimiAngArtikulli">pershkrimi i artikullit ne anglisht</param>
        /// <param name="kodiDoganorArtikulli">kodi doganor i artikullit</param>
        /// <param name="vendodhjeArtikulli">vendodhja e artikullit</param>
        /// <param name="kodifikimi1Artikulli">kodifikimi 1 i artikullit</param>
        /// <param name="kodifikimi2Artikulli"> kodifikimi 2 i artikullit</param>
        /// <param name="origjineArtikulli"> origjina e artikullit</param>
        /// <param name="njesi1Artikulli"> njesia e pare e artikullit</param>
        /// <param name="njesi2Artikulli">njesia e dyte e artikullit</param>
        /// <param name="koeficientArtikulli"> koeficienti midis njesive te artikullit</param>
        /// <param name="idFurnitoriKryesor">id e furnitorit kryesor</param>
        /// <param name="peshaBrutoArtikulli">pesha bruto e artikullit</param>
        /// <param name="peshaNetoArtikulli">pesha neto e artikullit</param>
        /// <param name="detajimArtikulli">detajimi i artikullit</param>
        /// <param name="klasa">klasa e artikullit</param>
        /// <param name="idSkemaKontabilitetiArtikulli">skema e kontabilitetit te artikullit</param>
        /// <param name="idLlogariInventari"> id llogari inventari</param>
        /// <param name="idLlogariBlerje">id llogari blerje</param>
        /// <param name="idLlogariShitje"> id llogari shitje</param>
        /// <param name="idLlogariTeTrete"> id llogari tek te tretet</param>
        /// <param name="idLlogariShpenzime"> id llogari shpenzimesh</param>
        /// <param name="minimumArtikulli">sasia min e artikullit</param>
        /// <param name="maximumArtikulli"> sasia max e artikullit</param>
        /// <param name="metodeKostojeArtikulli"> metode kostoje e artikullit</param>
        /// <param name="llogaritjaKMSHArtikulli"> llogaritja KMSH e artikullit</param>
        /// <param name="zevendesimAutomatikArtikulli"> zevendesimi automatik i artikullit</param>
        /// <param name="idPerdoruesi"> id e perdoruesit qe ka kryer veprimin</param>
        /// <param name="idNderViti"> id e ndermarje vitit</param>
        /// <param name="idnderm"> id e ndermarjes</param>
        /// <param name="kontrollgjendje"> kontrolli mbi gjendjen e artikullit</param>
        /// <param name="kontrollcmimi"> kontrolli i cmimit te artikullit</param>
        /// <param name="kontrollgjendjeartikulli"> kontrolli i gjendjes se detajimit artikullit</param>
        /// <param name="Tvsh">tvsh</param>
        /// <param name="idkonfig">id e konfigurimit</param>
        /// <param name="aktiv">aktiv</param>
        /// <param name="prodhimmeporosi"> prodhim me porosi</param>
        /// <param name="meSerial">merr nese artikulli do te jete me serial unik per cdo njesi apo per nje grup sasish te percaktuara ne nje dokument hyrjeje ose blerjeje.</param>
        /// <returns> nje objekt clsMesazh qe tregon nese modifikimi eshte kryer ne rregull apo jo.</returns>
        internal clsMesazh modifikoArt(int idArtikulli, string kodArtikulli, string pershkrimArtikulli, string pershkrimiAngArtikulli, string kodiDoganorArtikulli, string vendodhjeArtikulli, int kodifikimi1Artikulli, int kodifikimi2Artikulli, string origjineArtikulli, int njesi1Artikulli, int njesi2Artikulli, decimal koeficientArtikulli, int idFurnitoriKryesor, decimal peshaBrutoArtikulli, decimal peshaNetoArtikulli, bool detajimArtikulli, int klasa, int idSkemaKontabilitetiArtikulli, int idLlogariInventari, int idLlogariBlerje, int idLlogariShitje, int idLlogariTeTrete, int idLlogariShpenzime, int idLlogariAmortizimi, int idLlogariPakesimi, int idLlogariRezerve, int idLlogariPakesimRez, decimal minimumArtikulli, decimal maximumArtikulli, int metodeKostojeArtikulli, int llogaritjaKMSHArtikulli, int zevendesimAutomatikArtikulli, int idPerdoruesi, int idnderm, bool kontrollgjendje, bool kontrollcmimi, bool kontrollgjendjeartikulli, int Tvsh, int idkonfig, bool aktiv, int idstatusdok, bool llojiArt, decimal sasinjesi, decimal scrap, bool prodhimmeporosi, int idkategoridetajimi, int idKategoriDetajimi2, bool kontrolloGjendjeDetajim2, int idobjektivakosto, int idLlojGarancie, decimal garancia, int idmag, bool irezervueshem, bool perTransferim, bool loan, bool dhurate, int aplikimdhurate, decimal pike, decimal vlere, string kodvfone, bool meSerial, bool iShitshem, bool mbetjeshitshme, int idArtRaportuesi, bool perPeshore, string pershkrimFurnitori, string siperfaqjam2, string nrKontrate, string nrPasurie, string zonaKadastrale, string shasi, string marka, string modeli, string vitProdhimi, string tedhenateknike, bool meBarkodLogjik, string skemaBarkodit, int kodifikimi3Artikulli, bool aparatBazaar, string kodOferte, bool artikullIVjeter, int idkategoriseriali, bool merezerverivleresimi, int nrKaraktereTAC, bool llogaritKomision, int llogariKomisioni, int stokumaxvfone, string kodiibarit, bool irimbursueshem)
        {
            dbManager.Open();
            dbManager.CreateParameters(89);
            dbManager.AddParameters(0, "@IDARTIKULLI", idArtikulli, ParameterDirection.Input);
            dbManager.AddParameters(1, "@KODARTIKULLI", kodArtikulli, ParameterDirection.Input);
            dbManager.AddParameters(2, "@PERSHKRIMARTIKULLI", pershkrimArtikulli, ParameterDirection.Input);
            dbManager.AddParameters(3, "@PERSHKRIMIANGARTIKULLI", pershkrimiAngArtikulli, ParameterDirection.Input);
            dbManager.AddParameters(4, "@KODIDOGANORARTIKULLI", kodiDoganorArtikulli, ParameterDirection.Input);
            dbManager.AddParameters(5, "@VENDODHJEARTIKULLI", vendodhjeArtikulli, ParameterDirection.Input);
            if (kodifikimi1Artikulli == 0)
                dbManager.AddParameters(6, "@KODIFIKIMI1ARTIKULLI", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(6, "@KODIFIKIMI1ARTIKULLI", kodifikimi1Artikulli, ParameterDirection.Input);
            if (kodifikimi2Artikulli == 0)
                dbManager.AddParameters(7, "@KODIFIKIMI2ARTIKULLI", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(7, "@KODIFIKIMI2ARTIKULLI", kodifikimi2Artikulli, ParameterDirection.Input);
            dbManager.AddParameters(8, "@ORIGJINEARTIKULLI", origjineArtikulli, ParameterDirection.Input);
            if (njesi1Artikulli == 0)
                dbManager.AddParameters(9, "@NJESI1ARTIKULLI", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(9, "@NJESI1ARTIKULLI", njesi1Artikulli, ParameterDirection.Input);
            if (njesi2Artikulli == 0)
                dbManager.AddParameters(10, "@NJESI2ARTIKULLI", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(10, "@NJESI2ARTIKULLI", njesi2Artikulli, ParameterDirection.Input);
            dbManager.AddParameters(11, "@KOEFICENTARTIKULLI", koeficientArtikulli, ParameterDirection.Input);
            if (idFurnitoriKryesor == 0)
                dbManager.AddParameters(12, "@IDFURNITORIKRYESOR", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(12, "@IDFURNITORIKRYESOR", idFurnitoriKryesor, ParameterDirection.Input);
            dbManager.AddParameters(13, "@PESHABRUTOARTIKULLI", peshaBrutoArtikulli, ParameterDirection.Input);
            dbManager.AddParameters(14, "@PESHANETOARTIKULLI", peshaNetoArtikulli, ParameterDirection.Input);
            dbManager.AddParameters(15, "@DETAJIMARTIKULLI", detajimArtikulli, ParameterDirection.Input);
            if (klasa == 0)
                dbManager.AddParameters(16, "@KLASA", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(16, "@KLASA", klasa, ParameterDirection.Input);
            if (idSkemaKontabilitetiArtikulli == 0)
                dbManager.AddParameters(17, "@IDSKEMAKONTABILITETIARTIKULLI", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(17, "@IDSKEMAKONTABILITETIARTIKULLI", idSkemaKontabilitetiArtikulli, ParameterDirection.Input);
            if (idLlogariInventari == 0 || idLlogariInventari == -1)
                dbManager.AddParameters(18, "@IDLLOGARIINVENTARI", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(18, "@IDLLOGARIINVENTARI", idLlogariInventari, ParameterDirection.Input);
            if (idLlogariBlerje == 0 || idLlogariBlerje == -1)
                dbManager.AddParameters(19, "@IDLLOGARIBLERJE", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(19, "@IDLLOGARIBLERJE", idLlogariBlerje, ParameterDirection.Input);
            if (idLlogariShitje == 0 || idLlogariShitje == -1)
                dbManager.AddParameters(20, "@IDLLOGARISHITJE", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(20, "@IDLLOGARISHITJE", idLlogariShitje, ParameterDirection.Input);
            if (idLlogariTeTrete == 0 || idLlogariTeTrete == -1)
                dbManager.AddParameters(21, "@IDLLOGARITETRETE", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(21, "@IDLLOGARITETRETE", idLlogariTeTrete, ParameterDirection.Input);
            if (idLlogariShpenzime == 0 || idLlogariShpenzime == -1)
                dbManager.AddParameters(22, "@IDLLOGARISHPENZIME", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(22, "@IDLLOGARISHPENZIME", idLlogariShpenzime, ParameterDirection.Input);
            if (idLlogariAmortizimi == 0 || idLlogariAmortizimi == -1)
                dbManager.AddParameters(23, "@IDLLOGARIAMORTIZIMI", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(23, "@IDLLOGARIAMORTIZIMI", idLlogariAmortizimi, ParameterDirection.Input);
            dbManager.AddParameters(24, "@MINIMUMARTIKULLI", minimumArtikulli, ParameterDirection.Input);
            dbManager.AddParameters(25, "@MAXIMUMARTIKULLI", maximumArtikulli, ParameterDirection.Input);
            if (metodeKostojeArtikulli == 0)
                dbManager.AddParameters(26, "@METODEKOSTOJEARTIKULLI", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(26, "@METODEKOSTOJEARTIKULLI", metodeKostojeArtikulli, ParameterDirection.Input);
            dbManager.AddParameters(27, "@LLOGARITJAKMSHARTIKULLI", llogaritjaKMSHArtikulli, ParameterDirection.Input);
            dbManager.AddParameters(28, "@ZEVENDESIMAUTOMATIKARTIKULLI", zevendesimAutomatikArtikulli, ParameterDirection.Input);
            dbManager.AddParameters(29, "@IDPERDORUESI", idPerdoruesi, ParameterDirection.Input);
            dbManager.AddParameters(30, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            dbManager.AddParameters(31, "@KONTROLLGJENDJE", kontrollgjendje, ParameterDirection.Input);
            dbManager.AddParameters(32, "@KONTROLLCMIMI", kontrollcmimi, ParameterDirection.Input);
            dbManager.AddParameters(33, "@KONTROLLGJENDJEARTIKULLI", kontrollgjendjeartikulli, ParameterDirection.Input);
            if (Tvsh == 0)
                dbManager.AddParameters(34, "@IDTVSH", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(34, "@IDTVSH", Tvsh, ParameterDirection.Input);
            dbManager.AddParameters(35, "@IDKONFIG", idkonfig, ParameterDirection.Input);
            dbManager.AddParameters(36, "@AKTIV", aktiv, ParameterDirection.Input);
            dbManager.AddParameters(37, "@IDSTATUSDOK", idstatusdok, ParameterDirection.Input);
            dbManager.AddParameters(38, "@LLOJIART", llojiArt, ParameterDirection.Input);
            dbManager.AddParameters(39, "@SASINJESI", sasinjesi, ParameterDirection.Input);
            dbManager.AddParameters(40, "@SCRAP", scrap, ParameterDirection.Input);
            dbManager.AddParameters(41, "@PRODHIMMEPOROSI", prodhimmeporosi, ParameterDirection.Input);
            if (idkategoridetajimi == 0 || idkategoridetajimi == -1)
                dbManager.AddParameters(42, "@IDKATEGORIDETAJIMI", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(42, "@IDKATEGORIDETAJIMI", idkategoridetajimi, ParameterDirection.Input);
            if (idKategoriDetajimi2 == 0 || idKategoriDetajimi2 == -1)
                dbManager.AddParameters(43, "@IDKATEGORIDETAJIMI2", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(43, "@IDKATEGORIDETAJIMI2", idKategoriDetajimi2, ParameterDirection.Input);
            dbManager.AddParameters(44, "@KONTROLLGJENDJEDETAJIM2", kontrolloGjendjeDetajim2, ParameterDirection.Input);
            if (idobjektivakosto == 0 || idobjektivakosto == -1) dbManager.AddParameters(45, "@IDOBJEKTIVAKOSTO", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(45, "@IDOBJEKTIVAKOSTO", idobjektivakosto, ParameterDirection.Input);
            if (idLlojGarancie == 0 || idLlojGarancie == -1) dbManager.AddParameters(46, "@IDLLOJGARANCIA", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(46, "@IDLLOJGARANCIA", idLlojGarancie, ParameterDirection.Input);
            dbManager.AddParameters(47, "@GARANCIA", garancia, ParameterDirection.Input);
            if (idmag == 0 || idmag == -1)
                dbManager.AddParameters(48, "@IDMAGAZINA", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(48, "@IDMAGAZINA", idmag, ParameterDirection.Input);
            dbManager.AddParameters(49, "@IREZERVUESHEM", irezervueshem, ParameterDirection.Input);
            dbManager.AddParameters(50, "@PERTRANSFERIM", perTransferim, ParameterDirection.Input);
            dbManager.AddParameters(51, "@LOAN", loan, ParameterDirection.Input);
            dbManager.AddParameters(52, "@DHURATE", dhurate, ParameterDirection.Input);
            if (aplikimdhurate == 0)
                dbManager.AddParameters(53, "@APLIKIMDHURATE", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(53, "@APLIKIMDHURATE", aplikimdhurate, ParameterDirection.Input);
            dbManager.AddParameters(54, "@PIKE", pike, ParameterDirection.Input);
            dbManager.AddParameters(55, "@VLERE", vlere, ParameterDirection.Input);
            dbManager.AddParameters(56, "@KODVFONE", kodvfone, ParameterDirection.Input);
            dbManager.AddParameters(57, "@MESERIAL", meSerial, ParameterDirection.Input);
            if (idLlogariPakesimi == 0 || idLlogariPakesimi == -1)
                dbManager.AddParameters(58, "@IDLLOGARIPAKESIMI", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(58, "@IDLLOGARIPAKESIMI", idLlogariPakesimi, ParameterDirection.Input);
            dbManager.AddParameters(59, "@ISHITSHEM", iShitshem, ParameterDirection.Input);
            dbManager.AddParameters(60, "@MBETJESHITJSHME", mbetjeshitshme, ParameterDirection.Input);
            if (idArtRaportuesi == 0 || idArtRaportuesi == -1)
                dbManager.AddParameters(61, "@IDARTRAPORTUESI", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(61, "@IDARTRAPORTUESI", idArtRaportuesi, ParameterDirection.Input);
            dbManager.AddParameters(62, "@PERPESHORE", perPeshore, ParameterDirection.Input);
            dbManager.AddParameters(63, "@PERSHKRIMTEFURNITORI", pershkrimFurnitori, ParameterDirection.Input);
            dbManager.AddParameters(64, "@SIPERFAQJAM2", siperfaqjam2, ParameterDirection.Input);
            dbManager.AddParameters(65, "@NRKONTRATE", nrKontrate, ParameterDirection.Input);
            dbManager.AddParameters(66, "@NRPASURIE", nrPasurie, ParameterDirection.Input);
            dbManager.AddParameters(67, "@ZONAKADASTRALE", zonaKadastrale, ParameterDirection.Input);
            dbManager.AddParameters(68, "@SHASIA", shasi, ParameterDirection.Input);
            dbManager.AddParameters(69, "@MARKA", marka, ParameterDirection.Input);
            dbManager.AddParameters(70, "@MODELI", modeli, ParameterDirection.Input);
            dbManager.AddParameters(71, "@VITPRODHIMI", vitProdhimi, ParameterDirection.Input);
            dbManager.AddParameters(72, "@TEDHENATEKNIKE", tedhenateknike, ParameterDirection.Input);
            dbManager.AddParameters(73, "@MEBARKODLOGJIK", meBarkodLogjik, ParameterDirection.Input);
            dbManager.AddParameters(74, "@SKEMABARKODIT", skemaBarkodit, ParameterDirection.Input);
            if (kodifikimi3Artikulli == 0)
                dbManager.AddParameters(75, "@KODIFIKIMI3ARTIKULLI", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(75, "@KODIFIKIMI3ARTIKULLI", kodifikimi3Artikulli, ParameterDirection.Input);
            dbManager.AddParameters(76, "@APARATBAZAAR", aparatBazaar, ParameterDirection.Input);
            dbManager.AddParameters(77, "@KODOFERTE", kodOferte, ParameterDirection.Input);
            dbManager.AddParameters(78, "@ARTIKULLIVJETER", artikullIVjeter, ParameterDirection.Input);
            if (idkategoriseriali == 0)
                dbManager.AddParameters(79, "@IDKATEGORISERIALI", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(79, "@IDKATEGORISERIALI", idkategoriseriali, ParameterDirection.Input);

            dbManager.AddParameters(80, "@REZERVERIVLERESIMI", merezerverivleresimi, ParameterDirection.Input);
            if (idLlogariRezerve == 0 || idLlogariRezerve == -1)
                dbManager.AddParameters(81, "@IDLLOGARIREZERVE", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(81, "@IDLLOGARIREZERVE", idLlogariRezerve, ParameterDirection.Input);
            if (idLlogariPakesimRez == 0 || idLlogariPakesimRez == -1)
                dbManager.AddParameters(82, "@IDLLOGARIPAKESIMREZ", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(82, "@IDLLOGARIPAKESIMREZ", idLlogariPakesimRez, ParameterDirection.Input);
            dbManager.AddParameters(83, "@NRKARAKTERETAC", nrKaraktereTAC, ParameterDirection.Input);
            dbManager.AddParameters(84, "@LLOGARITKOMISION", llogaritKomision, ParameterDirection.Input);
            dbManager.AddParameters(85, "@IDLLOGARIKOMISIONI", llogariKomisioni, ParameterDirection.Input);
            dbManager.AddParameters(86, "@STOKUMAXVFONE", stokumaxvfone, ParameterDirection.Input);
            dbManager.AddParameters(87, "@KODIIBARIT", kodiibarit, ParameterDirection.Input);
            dbManager.AddParameters(88, "@IRIMBURSUESHEM", irimbursueshem, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_ARTIKULLI_upd");
            idArtikulli = int.Parse(dbManager.Parameters[0].Value.ToString());
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
            return mesazh;

        }
        public bool seshteTransferuarTekBijArtikull(string kodartikulli, int idndermarje)
        {

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@KODArtikulli", kodartikulli, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idndermarje, ParameterDirection.Input);
            int pergjigje = Convert.ToInt32(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_ARTIKULLI_eshteTransferuarTekBij"));
            return Convert.ToBoolean(pergjigje);

        }

        internal String ktheTeDhenaPerArtikullin(string kodArt)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@KODARTIKULLI", kodArt, ParameterDirection.Input);
            var pergjigje = dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_Artikulli_ktheTeDhenaPerArtikullin");
            return pergjigje.ToString();

        }
        internal DataTable kontrolloEkzistencNeMgaOwn(string kodart, int idtvsh, int idkl)
        {
            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@KODARTIKULLI", kodart, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDTVSH", idtvsh, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDKLIENT", idkl, ParameterDirection.Input);
            var ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_kontrolloEkzistencNeMgaOwn");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0];
        }
        internal clsMesazh fshiArtStatus(int idArtikulli, int idperdoruesi)
        {

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDARTIKULLI", idArtikulli, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUESI", idperdoruesi, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_ARTIKULLI_upddel");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
            return mesazh;

        }

        /// <summary>
        /// Kthen objektet artikuj aktiv sipas kodit dhe ndermarrjes
        /// </summary>
        /// <param name="kodArtikulli">Merr si string kodin e Artikullit</param>
        /// <param name="idNdermarje">Merr si integer id e ndermarrjes</param>
        /// <returns>Kthen nje DataRow me artikullin e kerkuar</returns>
        internal DataRow ktheArtikullSipasKodit(string kodArtikulli, int idNdermarje)
        {
            ImbLogger.LogTraceShitje($"Filloi metoda ktheArtikullSipasKodit sipas kodArtikulli:{kodArtikulli} dhe idNdermarje:{idNdermarje}");
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@KODARTIKULLI", kodArtikulli, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idNdermarje, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_ARTIKULLI_merrArtikullSipasKodit");
            ImbLogger.LogTraceShitje($"Mbaroi metoda ktheArtikullSipasKodit sipas kodArtikulli:{kodArtikulli} dhe idNdermarje:{idNdermarje}");
            if (ds == null)
            {
                ImbLogger.LogTraceShitje($"Metoda ktheArtikullSipasKodit sipas kodArtikulli:{kodArtikulli} dhe idNdermarje:{idNdermarje} ktheu null ");
                return null;
            }

            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
            {
                ImbLogger.LogTraceShitje($"Metoda ktheArtikullSipasKodit sipas kodArtikulli:{kodArtikulli} dhe idNdermarje:{idNdermarje} ktheu null ");
                return null;
            }

            return ds.Tables[0].Rows[0];

        }
        internal decimal ktheKoeficentArtikulli(string kodartikulli, int idnder)
        {
            ImbLogger.LogTraceShitje($"Filloi metoda ktheKoeficentArtikulli sipas kodartikulli:{kodartikulli}, idnder:{idnder}");
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@KODARTIKULLI", kodartikulli, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idnder, ParameterDirection.Input);
            ImbLogger.LogTraceShitje($"Mbaroi metoda ktheKoeficentArtikulli sipas kodartikulli:{kodartikulli}, idnder:{idnder}");
            return Convert.ToDecimal(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_Artikulli_ktheKoeficentArtikulliSipasKodit"));



        }
        internal bool ktheArtikullLoan(int idArtikulli)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDARTIKULLI", idArtikulli, ParameterDirection.Input);
            return Convert.ToBoolean(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_Artikulli_ktheLoan").ToString());
        }

        internal DataRow ktheArtikullSipasKoditDheAutorizime(string kodArtikulli, int idNdermarje, int idperdorues)
        {

            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@KODARTIKULLI", kodArtikulli, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idNdermarje, ParameterDirection.Input);
            dbManager.AddParameters(2, "@Idperdorues", idperdorues, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_ARTIKULLI_merrArtikullSipasKoditDheAutorizime");

            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];

        }

        /// <summary>
        /// nese nuk gjendet artikulli me kodin infixkodi atehere kthen te parin me kodin e ngjashem
        /// </summary>
        /// <param name="prefixKodi"></param>
        /// <param name="idNderm"></param>
        internal DataRow merrArtikullinEPareSipasKodit(string infixKodi, int idNderm, int idperdoruesi)
        {

            dbManager.Open();
            dbManager.CreateParameters(3);
            // infixKodi = "%" + infixKodi + "%";
            dbManager.AddParameters(0, "@KODARTIKULLI", infixKodi, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idNderm, ParameterDirection.Input);
            dbManager.AddParameters(2, "@Idperdorues", idperdoruesi, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_ARTIKULLI_findFirstArtikullSipasKodit");
            if (ds.Tables[0].Rows.Count == 1)
                return ds.Tables[0].Rows[0];
            dbManager.CreateParameters(3);
            infixKodi = "%" + infixKodi + "%";
            dbManager.AddParameters(0, "@KODARTIKULLI", infixKodi, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idNderm, ParameterDirection.Input);
            dbManager.AddParameters(2, "@Idperdorues", idperdoruesi, ParameterDirection.Input);
            ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_ARTIKULLI_findFirstArtikullSipasKodit");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0)
                return null;
            return ds.Tables[0].Rows[0];

        }


        /// <summary>
        /// kthen objektet artikuj aktiv sipas kodbarit dhe ndermarrjes
        /// </summary>
        /// <param name="kodbari">Merr si string kodbarin</param>
        /// <param name="idNdermarrja">merr si integer id e ndermarrjes</param>
        /// <returns>kthen nje DataRow me artikullin e kerkuar</returns>
        internal DataRow ktheArtikullSipasKodbarit(string kodbari, int idNdermarrja)
        {

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@KODBARI", kodbari, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idNdermarrja, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_ARTIKULLI_merrArtikullSipasKodbarit");

            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];

        }
        internal DataRow ktheArtikullSipasDetajimit(string detajimi, int idNdermarrja)
        {

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@Detajimi", detajimi, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idNdermarrja, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_ARTIKULLI_merrArtikullSipasDetajimit");

            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];

        }

        /// <summary>
        /// kthen objeket  artikuj sipas magazinave
        /// </summary>
        ///<param name="idmag">id e magazinave</param>
        ///<returns>nje objekt colArtikujt qe permban nje koleksion me artikuj te ketyre magazinave  </returns>
        internal DataTable ktheArtikujSipasMagazinave(string idmag)
        {


            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDMAGAZINA", idmag, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_ARTIKULLI_merrArtikullSipasMagazines");

            return ds.Tables[0];

        }
        internal DataRow ktheArtikullNdermarrjesAndAutorizimeDR(int idnderm, int idperdorues, int idArtikull, bool kosto, bool gjendje)
        {

            dbManager.Open();
            dbManager.CreateParameters(5);
            dbManager.AddParameters(0, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUES", idperdorues, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDARTIKULL", idArtikull, ParameterDirection.Input);
            dbManager.AddParameters(3, "@KOSTO", kosto, ParameterDirection.Input);
            dbManager.AddParameters(4, "@GJENDJE", gjendje, ParameterDirection.Input);

            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_ARTIKULLI_merrArtikullSipasNdermarjesAndAutorizimDR");

            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];

        }
        internal DataTable ktheArtikujNdermarrjesAndAutorizimeDTExport(int idnderm, int idperdorues, bool llojart, int lloji, string emerTabKoka, string emerFusheID)
        {

            dbManager.Open();
            dbManager.CreateParameters(6);
            dbManager.AddParameters(0, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUES", idperdorues, ParameterDirection.Input);
            dbManager.AddParameters(2, "@llojart", llojart, ParameterDirection.Input);
            dbManager.AddParameters(3, "@LLOJI", lloji, ParameterDirection.Input);
            dbManager.AddParameters(4, "@EMERTABKOKA", emerTabKoka, ParameterDirection.Input);
            dbManager.AddParameters(5, "@EMERFUSHEID", emerFusheID, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_ARTIKULLI_merrArtikullSipasNdermarjesAndAutorizimDTExport");

            return ds.Tables[0];

        }

        internal DataTable ktheArtikujNdermarrjesAndAutorizimeDTGjeneroKodbar(int idnderm, int idperdorues, bool llojart)
        {

            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUES", idperdorues, ParameterDirection.Input);
            dbManager.AddParameters(2, "@llojart", llojart, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_ARTIKULLI_merrArtikullSipasNdermarjesAndAutorizimDTGjeneroKodbar");

            return ds.Tables[0];

        }

        internal DataTable ktheArtikujNdermarrjesAndAutorizimeDT(int idnderm, int idperdorues, bool kostoSasi, bool gjendje, bool cmime, bool cmimeMeTvsh, string postStringTvsh)
        {

            dbManager.Open();
            dbManager.CreateParameters(7);
            dbManager.AddParameters(0, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUES", idperdorues, ParameterDirection.Input);
            dbManager.AddParameters(2, "@KOSTO", kostoSasi, ParameterDirection.Input);
            dbManager.AddParameters(3, "@GJENDJE", gjendje, ParameterDirection.Input);
            dbManager.AddParameters(4, "@CMIMET", cmime, ParameterDirection.Input);
            dbManager.AddParameters(5, "@CMIMETMETVSH", cmimeMeTvsh, ParameterDirection.Input);
            dbManager.AddParameters(6, "@POSTFIXTVSH", postStringTvsh, ParameterDirection.Input);
            return dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_ARTIKULLI_merrArtikullSipasNdermarjesAndAutorizimDT").Tables[0];

        }
        internal DataTable ktheArtikujNdermarrjesAndAutorizimeDTfilter(int idnderm, int idperdorues, bool kostoSasi, string filter)
        {


            dbManager.Open();
            dbManager.CreateParameters(4);
            dbManager.AddParameters(0, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUES", idperdorues, ParameterDirection.Input);
            dbManager.AddParameters(2, "@KOSTOSASI", kostoSasi, ParameterDirection.Input);
            dbManager.AddParameters(3, "@filter", filter, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_ARTIKULLI_merrArtikullSipasNdermarjesAndAutorizimDTFilter");
            return ds.Tables[0];

        }

        internal DataTable ktheArtikujNdermarrjesAndAutorizimeDheLlojDT(int idnderm, int idperdorues, bool llojartikulli, bool kostoSasi)
        {

            dbManager.Open();
            dbManager.CreateParameters(4);
            dbManager.AddParameters(0, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUES", idperdorues, ParameterDirection.Input);
            dbManager.AddParameters(2, "@KOSTOSASI", kostoSasi, ParameterDirection.Input);
            dbManager.AddParameters(3, "@Llojartikulli", llojartikulli, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_ARTIKULLI_merrArtikullSipasNdermarjesAndAutorizimDheLlojDT");
            return ds.Tables[0];

        }

        internal DataTable ktheArtikujAktivNdermarrjesAndAutorizimeDT(int idnderm, int idperdorues)
        {

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUES", idperdorues, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_ARTIKULLI_merrArtikullAktivSipasNdermarjesAndAutorizimDT");

            return ds.Tables[0];

        }

        internal DataTable MerrSipasArtikujAktivNdermarrjesAndAutorizimeAc(int idNdermarrje, int idPerdoruesi, string kodi)
        {
            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@IDNDERMARJE", idNdermarrje, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUES", idPerdoruesi, ParameterDirection.Input);
            dbManager.AddParameters(2, "@KODI", kodi, ParameterDirection.Input);

            return dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_ARTIKULLI_MerrSipasArtikujAktivNdermarrjesAndAutorizimeAc").Tables[0];
        }

        internal DataTable ktheArtikujLoanDT(int idnderm, int idmagazina, int idartikulli, DateTime date)
        {

            dbManager.Open();
            dbManager.CreateParameters(4);
            dbManager.AddParameters(0, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDMagazina", idmagazina, ParameterDirection.Input);
            dbManager.AddParameters(2, "@idartikulli", idartikulli, ParameterDirection.Input);
            dbManager.AddParameters(3, "@date", date, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_ARTIKULLI_merrArtikullLoanDt");

            return ds.Tables[0];

        }

        internal DataTable ktheArtikujNdermarrjesAndAutorizimePerLupeArtikulli(int idnderm, int idperdorues, bool kostoSasi, int idMag, bool cmime, bool cmimeMeTvsh, string postStringTvsh, bool iShitshem, string dategjendje, int kodifikim1, int kodifikim2, int kodifikim3, bool dhurataVFOne)
        {

            dbManager.Open();
            dbManager.CreateParameters(13);
            dbManager.AddParameters(0, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUES", idperdorues, ParameterDirection.Input);
            dbManager.AddParameters(2, "@KOSTOSASI", kostoSasi, ParameterDirection.Input);
            dbManager.AddParameters(3, "@IDMAG", idMag, ParameterDirection.Input);
            dbManager.AddParameters(4, "@Cmim", cmime, ParameterDirection.Input);
            dbManager.AddParameters(5, "@CMIMETMETVSH", cmimeMeTvsh, ParameterDirection.Input);
            dbManager.AddParameters(6, "@POSTFIXTVSH", postStringTvsh, ParameterDirection.Input);
            dbManager.AddParameters(7, "@ISHITSHEM", iShitshem, ParameterDirection.Input);
            dbManager.AddParameters(8, "@dategjendje", dategjendje, ParameterDirection.Input);
            dbManager.AddParameters(9, "@KODIFIKIM1", kodifikim1, ParameterDirection.Input);
            dbManager.AddParameters(10, "@KODIFIKIM2", kodifikim2, ParameterDirection.Input);
            dbManager.AddParameters(11, "@KODIFIKIM3", kodifikim3, ParameterDirection.Input);
            dbManager.AddParameters(12, "@DHURATEVFONE", dhurataVFOne, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_ARTIKULLI_merrArtikullSipasNdermarjesAndAutorizimPerLupeArtikulli");
            return ds.Tables[0];

        }
        internal DataTable ktheArtikujNdermarrjesAndAutorizimePerLupeArtikulliFilter(int idnderm, int idperdorues, bool kostoSasi, int idMag, bool cmime, bool cmimeMeTvsh, string postStringTvsh, bool iShitshem, string dategjendje, string filter, bool merrArtikuj, int kodifikim1, int kodifikim2, int kodifikim3, bool dhurataVFOne)
        {

            dbManager.Open();
            dbManager.CreateParameters(15);
            dbManager.AddParameters(0, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUES", idperdorues, ParameterDirection.Input);
            dbManager.AddParameters(2, "@KOSTOSASI", kostoSasi, ParameterDirection.Input);
            dbManager.AddParameters(3, "@IDMAG", idMag, ParameterDirection.Input);
            dbManager.AddParameters(4, "@Cmim", cmime, ParameterDirection.Input);
            dbManager.AddParameters(5, "@CMIMETMETVSH", cmimeMeTvsh, ParameterDirection.Input);
            dbManager.AddParameters(6, "@POSTFIXTVSH", postStringTvsh, ParameterDirection.Input);
            dbManager.AddParameters(7, "@ISHITSHEM", iShitshem, ParameterDirection.Input);
            dbManager.AddParameters(8, "@dategjendje", dategjendje, ParameterDirection.Input);
            dbManager.AddParameters(9, "@filter", filter, ParameterDirection.Input);
            dbManager.AddParameters(10, "@MERRARTIKUJ", merrArtikuj, ParameterDirection.Input);
            dbManager.AddParameters(11, "@KODIFIKIM1", kodifikim1, ParameterDirection.Input);
            dbManager.AddParameters(12, "@KODIFIKIM2", kodifikim2, ParameterDirection.Input);
            dbManager.AddParameters(13, "@KODIFIKIM3", kodifikim3, ParameterDirection.Input);
            dbManager.AddParameters(14, "@DHURATEVFONE", dhurataVFOne, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_ARTIKULLI_merrArtikullSipasNdermarjesAndAutorizimPerLupeArtikulliFilter");
            return ds.Tables[0];

        }

        internal DataTable ktheArtikujNdermarrjesAndAutorizimeSipasPikeve(int idnderm, int idperdorues, decimal pike, int idmagazina, DateTime data)
        {

            dbManager.Open();
            dbManager.CreateParameters(5);
            dbManager.AddParameters(0, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUES", idperdorues, ParameterDirection.Input);
            dbManager.AddParameters(2, "@PIKE", pike, ParameterDirection.Input);
            dbManager.AddParameters(3, "@IDMAGAZINA", idmagazina, ParameterDirection.Input);
            dbManager.AddParameters(4, "@DATA", data, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_ARTIKULLI_merrArtikullSipasNdermarjesAndAutorizimSipasPikeve");
            return ds.Tables[0];

        }
        internal DataTable ktheArtikujNdermarrjesAndAutorizimeSipasKodit(int idnderm, int idperdorues, string kodi, int idmagazina, DateTime data)
        {


            dbManager.Open();
            dbManager.CreateParameters(5);
            dbManager.AddParameters(0, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUES", idperdorues, ParameterDirection.Input);
            dbManager.AddParameters(2, "@kodi", kodi, ParameterDirection.Input);
            dbManager.AddParameters(3, "@IDMAGAZINA", idmagazina, ParameterDirection.Input);
            dbManager.AddParameters(4, "@DATA", data, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_ARTIKULLI_merrArtikullSipasNdermarjesAndAutorizimSipasKodit");
            return ds.Tables[0];

        }

        internal DataTable ktheArtikujNdermarrjesAndAutorizimeSipasBazaarit(int idnderm, int idperdorues, int idmagazina, DateTime data)
        {
            dbManager.Open();
            dbManager.CreateParameters(4);
            dbManager.AddParameters(0, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUES", idperdorues, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDMAGAZINA", idmagazina, ParameterDirection.Input);
            dbManager.AddParameters(3, "@DATA", data, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_ARTIKULLI_merrArtikullSipasNdermarjesAndAutorizimSipasBazaar");
            return ds.Tables[0];
        }

        /// <summary>
        /// kthen te gjithe artikujt, pervec atyre te klasave perbere dhe prodhim
        /// </summary>
        /// <param name="idnderm">Id e ndermarrjes</param>
        /// <param name="idperdorues">Id e perdoruesit</param>
        /// <returns></returns>
        internal DataTable ktheArtikujNdermarrjesAndAutorizimePerLupeArtikulliJoPerbProdhim(int idnderm, int idperdorues, bool kostoSasia, int idMag, bool cmime, bool cmimeMeTvsh, string postStringTvsh, bool iShitshem, int kodifikim1, int kodifikim2, int kodifikim3)
        {

            dbManager.Open();
            dbManager.CreateParameters(11);
            dbManager.AddParameters(0, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUES", idperdorues, ParameterDirection.Input);
            dbManager.AddParameters(2, "@KOSTOSASI", kostoSasia, ParameterDirection.Input);
            dbManager.AddParameters(3, "@IDMAG", idMag, ParameterDirection.Input);
            dbManager.AddParameters(4, "@Cmim", cmime, ParameterDirection.Input);
            dbManager.AddParameters(5, "@CMIMETMETVSH", cmimeMeTvsh, ParameterDirection.Input);
            dbManager.AddParameters(6, "@POSTFIXTVSH", postStringTvsh, ParameterDirection.Input);
            dbManager.AddParameters(7, "@ISHITSHEM", iShitshem, ParameterDirection.Input);
            dbManager.AddParameters(8, "@KODIFIKIM1", kodifikim1, ParameterDirection.Input);
            dbManager.AddParameters(9, "@KODIFIKIM2", kodifikim2, ParameterDirection.Input);
            dbManager.AddParameters(10, "@KODIFIKIM3", kodifikim3, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_ARTIKULLI_merrArtikullSipasNdermarjesAndAutorizimPerLupeArtikulliJoPerbProdhim");
            return ds.Tables[0];

        }
        internal DataTable ktheArtikujNdermarrjesAndAutorizimePerLupeArtikulliPerbere(int idnderm, int idperdorues, bool kostoSasia, int idMag, bool cmime, bool cmimeMeTvsh, string postStringTvsh, bool iShitshem, int kodifikim1, int kodifikim2, int kodifikim3)
        {

            dbManager.Open();
            dbManager.CreateParameters(11);
            dbManager.AddParameters(0, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUES", idperdorues, ParameterDirection.Input);
            dbManager.AddParameters(2, "@KOSTOSASI", kostoSasia, ParameterDirection.Input);
            dbManager.AddParameters(3, "@IDMAG", idMag, ParameterDirection.Input);
            dbManager.AddParameters(4, "@Cmim", cmime, ParameterDirection.Input);
            dbManager.AddParameters(5, "@CMIMETMETVSH", cmimeMeTvsh, ParameterDirection.Input);
            dbManager.AddParameters(6, "@POSTFIXTVSH", postStringTvsh, ParameterDirection.Input);
            dbManager.AddParameters(7, "@ISHITSHEM", iShitshem, ParameterDirection.Input);
            dbManager.AddParameters(8, "@KODIFIKIM1", kodifikim1, ParameterDirection.Input);
            dbManager.AddParameters(9, "@KODIFIKIM2", kodifikim2, ParameterDirection.Input);
            dbManager.AddParameters(10, "@KODIFIKIM3", kodifikim3, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_ARTIKULLI_merrArtikullSipasNdermarjesAndAutorizimPerLupeArtikulliPerbere");
            return ds.Tables[0];

        }
        internal DataTable ktheArtikujNdermarrjesAndAutorizimePerLupeArtikulliJoPerbProdhimFilter(int idnderm, int idperdorues, bool kostoSasia, int idMag, bool cmime, bool cmimeMeTvsh, string postStringTvsh, bool iShitshem, string filter, bool merrArtikuj, int kodifikim1, int kodifikim2, int kodifikim3)
        {

            dbManager.Open();
            dbManager.CreateParameters(13);
            dbManager.AddParameters(0, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUES", idperdorues, ParameterDirection.Input);
            dbManager.AddParameters(2, "@KOSTOSASI", kostoSasia, ParameterDirection.Input);
            dbManager.AddParameters(3, "@IDMAG", idMag, ParameterDirection.Input);
            dbManager.AddParameters(4, "@Cmim", cmime, ParameterDirection.Input);
            dbManager.AddParameters(5, "@CMIMETMETVSH", cmimeMeTvsh, ParameterDirection.Input);
            dbManager.AddParameters(6, "@POSTFIXTVSH", postStringTvsh, ParameterDirection.Input);
            dbManager.AddParameters(7, "@ISHITSHEM", iShitshem, ParameterDirection.Input);
            dbManager.AddParameters(8, "@filter", filter, ParameterDirection.Input);
            dbManager.AddParameters(9, "@MERRARTIKUJ", merrArtikuj, ParameterDirection.Input);
            dbManager.AddParameters(10, "@KODIFIKIM1", kodifikim1, ParameterDirection.Input);
            dbManager.AddParameters(11, "@KODIFIKIM2", kodifikim2, ParameterDirection.Input);
            dbManager.AddParameters(12, "@KODIFIKIM3", kodifikim3, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_ARTIKULLI_merrArtikullSipasNdermarjesAndAutorizimPerLupeArtikulliJoPerbProdhimFilter");
            return ds.Tables[0];

        }
        internal DataTable ktheArtikujNdermarrjesAndAutorizimePerLupeArtikulliPerbereFilter(int idnderm, int idperdorues, bool kostoSasia, int idMag, bool cmime, bool cmimeMeTvsh, string postStringTvsh, bool iShitshem, string filter, bool merrArtikuj, int kodifikim1, int kodifikim2, int kodifikim3)
        {

            dbManager.Open();
            dbManager.CreateParameters(13);
            dbManager.AddParameters(0, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUES", idperdorues, ParameterDirection.Input);
            dbManager.AddParameters(2, "@KOSTOSASI", kostoSasia, ParameterDirection.Input);
            dbManager.AddParameters(3, "@IDMAG", idMag, ParameterDirection.Input);
            dbManager.AddParameters(4, "@Cmim", cmime, ParameterDirection.Input);
            dbManager.AddParameters(5, "@CMIMETMETVSH", cmimeMeTvsh, ParameterDirection.Input);
            dbManager.AddParameters(6, "@POSTFIXTVSH", postStringTvsh, ParameterDirection.Input);
            dbManager.AddParameters(7, "@ISHITSHEM", iShitshem, ParameterDirection.Input);
            dbManager.AddParameters(8, "@filter", filter, ParameterDirection.Input);
            dbManager.AddParameters(9, "@MERRARTIKUJ", merrArtikuj, ParameterDirection.Input);
            dbManager.AddParameters(10, "@KODIFIKIM1", kodifikim1, ParameterDirection.Input);
            dbManager.AddParameters(11, "@KODIFIKIM2", kodifikim2, ParameterDirection.Input);
            dbManager.AddParameters(12, "@KODIFIKIM3", kodifikim3, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_ARTIKULLI_merrArtikullSipasNdermarjesAndAutorizimPerLupeArtikulliPerbereFilter");
            return ds.Tables[0];

        }

        /// <summary>
        /// kthen te gjithe artikujt, per prodhim pra inventar, ne proces dhe prodhim
        /// </summary>
        /// <param name="idnderm">Id e ndermarrjes</param>
        /// <param name="idperdorues">Id e perdoruesit</param>
        /// <returns></returns>
        internal DataTable ktheArtikujNdermarrjesAndAutorizimePerLupeArtikulliPerProdhim(int idnderm, int idperdorues, bool kostoSasi, int idMag, bool cmime, bool cmimeMeTvsh, string postStringTvsh, bool iShitshem, int kodifikim1, int kodifikim2, int kodifikim3)
        {

            dbManager.Open();
            dbManager.CreateParameters(11);
            dbManager.AddParameters(0, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUES", idperdorues, ParameterDirection.Input);
            dbManager.AddParameters(2, "@KOSTOSASI", kostoSasi, ParameterDirection.Input);
            dbManager.AddParameters(3, "@IDMAG", idMag, ParameterDirection.Input);
            dbManager.AddParameters(4, "@Cmim", cmime, ParameterDirection.Input);
            dbManager.AddParameters(5, "@CMIMETMETVSH", cmimeMeTvsh, ParameterDirection.Input);
            dbManager.AddParameters(6, "@POSTFIXTVSH", postStringTvsh, ParameterDirection.Input);
            dbManager.AddParameters(7, "@ISHITSHEM", iShitshem, ParameterDirection.Input);
            dbManager.AddParameters(8, "@KODIFIKIM1", kodifikim1, ParameterDirection.Input);
            dbManager.AddParameters(9, "@KODIFIKIM2", kodifikim2, ParameterDirection.Input);
            dbManager.AddParameters(10, "@KODIFIKIM3", kodifikim3, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_ARTIKULLI_merrArtikullSipasNdermarjesAndAutorizimPerLupeArtikulliPerProdhim");
            return ds.Tables[0];

        }
        internal DataTable ktheArtikujNdermarrjesAndAutorizimePerLupeArtikulliPerProdhimFilter(int idnderm, int idperdorues, bool kostoSasi, int idMag, bool cmime, bool cmimeMeTvsh, string postStringTvsh, bool iShitshem, string filter, bool merrArtikuj, int kodifikim1, int kodifikim2, int kodifikim3)
        {

            dbManager.Open();
            dbManager.CreateParameters(13);
            dbManager.AddParameters(0, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUES", idperdorues, ParameterDirection.Input);
            dbManager.AddParameters(2, "@KOSTOSASI", kostoSasi, ParameterDirection.Input);
            dbManager.AddParameters(3, "@IDMAG", idMag, ParameterDirection.Input);
            dbManager.AddParameters(4, "@Cmim", cmime, ParameterDirection.Input);
            dbManager.AddParameters(5, "@CMIMETMETVSH", cmimeMeTvsh, ParameterDirection.Input);
            dbManager.AddParameters(6, "@POSTFIXTVSH", postStringTvsh, ParameterDirection.Input);
            dbManager.AddParameters(7, "@ISHITSHEM", iShitshem, ParameterDirection.Input);
            dbManager.AddParameters(8, "@filter", filter, ParameterDirection.Input);
            dbManager.AddParameters(9, "@MERRARTIKUJ", merrArtikuj, ParameterDirection.Input);
            dbManager.AddParameters(10, "@KODIFIKIM1", kodifikim1, ParameterDirection.Input);
            dbManager.AddParameters(11, "@KODIFIKIM2", kodifikim2, ParameterDirection.Input);
            dbManager.AddParameters(12, "@KODIFIKIM3", kodifikim3, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_ARTIKULLI_merrArtikullSipasNdermarjesAndAutorizimPerLupeArtikulliPerProdhimFilter");
            return ds.Tables[0];

        }
        /// <summary>
        /// kthen te gjithe artikujt,  ne proces dhe prodhim
        /// </summary>
        /// <param name="idnderm">Id e ndermarrjes</param>
        /// <param name="idperdorues">Id e perdoruesit</param>
        /// <returns></returns>
        internal DataTable ktheArtikujNdermarrjesAndAutorizimePerLupeArtikulliProdhimi(int idnderm, int idperdorues, bool kostoSasi, int idMag, bool cmime, bool cmimeMeTvsh, string postStringTvsh, bool iShitshem, int kodifikim1, int kodifikim2, int kodifikim3)
        {

            dbManager.Open();
            dbManager.CreateParameters(11);
            dbManager.AddParameters(0, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUES", idperdorues, ParameterDirection.Input);
            dbManager.AddParameters(2, "@KOSTOSASI", kostoSasi, ParameterDirection.Input);
            dbManager.AddParameters(3, "@IDMAG", idMag, ParameterDirection.Input);
            dbManager.AddParameters(4, "@Cmim", cmime, ParameterDirection.Input);
            dbManager.AddParameters(5, "@CMIMETMETVSH", cmimeMeTvsh, ParameterDirection.Input);
            dbManager.AddParameters(6, "@POSTFIXTVSH", postStringTvsh, ParameterDirection.Input);
            dbManager.AddParameters(7, "@ISHITSHEM", iShitshem, ParameterDirection.Input);
            dbManager.AddParameters(8, "@KODIFIKIM1", kodifikim1, ParameterDirection.Input);
            dbManager.AddParameters(9, "@KODIFIKIM2", kodifikim2, ParameterDirection.Input);
            dbManager.AddParameters(10, "@KODIFIKIM3", kodifikim3, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_ARTIKULLI_merrArtikullSipasNdermarjesAndAutorizimPerLupeArtikulliProdhimi");

            return ds.Tables[0];

        }
        internal DataTable ktheArtikujNdermarrjesAndAutorizimePerLupeArtikulliProdhimiPlanifikimi(int idnderm, int idperdorues, bool kostoSasi, int idMag, bool cmime, bool cmimeMeTvsh, string postStringTvsh, bool iShitshem, int idplanifikimi, int kodifikim1, int kodifikim2, int kodifikim3)
        {

            dbManager.Open();
            dbManager.CreateParameters(12);
            dbManager.AddParameters(0, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUES", idperdorues, ParameterDirection.Input);
            dbManager.AddParameters(2, "@KOSTOSASI", kostoSasi, ParameterDirection.Input);
            dbManager.AddParameters(3, "@IDMAG", idMag, ParameterDirection.Input);
            dbManager.AddParameters(4, "@Cmim", cmime, ParameterDirection.Input);
            dbManager.AddParameters(5, "@CMIMETMETVSH", cmimeMeTvsh, ParameterDirection.Input);
            dbManager.AddParameters(6, "@POSTFIXTVSH", postStringTvsh, ParameterDirection.Input);
            dbManager.AddParameters(7, "@ISHITSHEM", iShitshem, ParameterDirection.Input);
            dbManager.AddParameters(8, "@IDPLANIFIKIMI", idplanifikimi, ParameterDirection.Input);
            dbManager.AddParameters(9, "@KODIFIKIM1", kodifikim1, ParameterDirection.Input);
            dbManager.AddParameters(10, "@KODIFIKIM2", kodifikim2, ParameterDirection.Input);
            dbManager.AddParameters(11, "@KODIFIKIM3", kodifikim3, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_ARTIKULLI_merrArtikullSipasNdermarjesAndAutorizimPerLupeArtikulliProdhimiPlanifikimi");

            return ds.Tables[0];

        }
        internal DataTable ktheArtikujNdermarrjesAndAutorizimePerLupeArtikulliProdhimiFilter(int idnderm, int idperdorues, bool kostoSasi, int idMag, bool cmime, bool cmimeMeTvsh, string postStringTvsh, bool iShitshem, string filter, bool merrArtikuj, int kodifikim1, int kodifikim2, int kodifikim3)
        {

            dbManager.Open();
            dbManager.CreateParameters(13);
            dbManager.AddParameters(0, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUES", idperdorues, ParameterDirection.Input);
            dbManager.AddParameters(2, "@KOSTOSASI", kostoSasi, ParameterDirection.Input);
            dbManager.AddParameters(3, "@IDMAG", idMag, ParameterDirection.Input);
            dbManager.AddParameters(4, "@Cmim", cmime, ParameterDirection.Input);
            dbManager.AddParameters(5, "@CMIMETMETVSH", cmimeMeTvsh, ParameterDirection.Input);
            dbManager.AddParameters(6, "@POSTFIXTVSH", postStringTvsh, ParameterDirection.Input);
            dbManager.AddParameters(7, "@ISHITSHEM", iShitshem, ParameterDirection.Input);
            dbManager.AddParameters(8, "@filter", filter, ParameterDirection.Input);
            dbManager.AddParameters(9, "@MERRARTIKUJ", merrArtikuj, ParameterDirection.Input);
            dbManager.AddParameters(10, "@KODIFIKIM1", kodifikim1, ParameterDirection.Input);
            dbManager.AddParameters(11, "@KODIFIKIM2", kodifikim2, ParameterDirection.Input);
            dbManager.AddParameters(12, "@KODIFIKIM3", kodifikim3, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_ARTIKULLI_merrArtikullSipasNdermarjesAndAutorizimPerLupeArtikulliProdhimiFilter");

            return ds.Tables[0];

        }
        internal DataTable ktheArtikujNdermarrjesAndAutorizimePerLupeArtikulliProdhimiFilterPlanifikimi(int idnderm, int idperdorues, bool kostoSasi, int idMag, bool cmime, bool cmimeMeTvsh, string postStringTvsh, bool iShitshem, string filter, int idplanfikimi, bool merrArtikuj, int kodifikim1, int kodifikim2, int kodifikim3)
        {

            dbManager.Open();
            dbManager.CreateParameters(14);
            dbManager.AddParameters(0, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUES", idperdorues, ParameterDirection.Input);
            dbManager.AddParameters(2, "@KOSTOSASI", kostoSasi, ParameterDirection.Input);
            dbManager.AddParameters(3, "@IDMAG", idMag, ParameterDirection.Input);
            dbManager.AddParameters(4, "@Cmim", cmime, ParameterDirection.Input);
            dbManager.AddParameters(5, "@CMIMETMETVSH", cmimeMeTvsh, ParameterDirection.Input);
            dbManager.AddParameters(6, "@POSTFIXTVSH", postStringTvsh, ParameterDirection.Input);
            dbManager.AddParameters(7, "@ISHITSHEM", iShitshem, ParameterDirection.Input);
            dbManager.AddParameters(8, "@filter", filter, ParameterDirection.Input);
            dbManager.AddParameters(9, "@idplanifikimi", idplanfikimi, ParameterDirection.Input);
            dbManager.AddParameters(10, "@MERRARTIKUJ", merrArtikuj, ParameterDirection.Input);
            dbManager.AddParameters(11, "@KODIFIKIM1", kodifikim1, ParameterDirection.Input);
            dbManager.AddParameters(12, "@KODIFIKIM2", kodifikim2, ParameterDirection.Input);
            dbManager.AddParameters(13, "@KODIFIKIM3", kodifikim3, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_ARTIKULLI_merrArtikullSipasNdermarjesAndAutorizimPerLupeArtikulliProdhimiFilterPlanifikim");

            return ds.Tables[0];

        }

        /// <summary>
        /// kthen objeket  artikuj sipas ndermarjes dhe autorizimeve per rivleresim pra jo te pastokueshmit, te perberet dhe sherbim
        /// </summary>
        ///<param name="idnderm"> id e ndermarjes</param>
        ///<param name="idperdorues"> id e perdoruesit</param>
        ///<returns>nje objekt colArtikujt qe permban nje koleksion me artikuj te kesaj ndermarje dhe per te cilat ky perdorues ka autorizim  </returns>
        internal DataTable ktheArtikujNdermarrjesAndAutorizimePerRivleresim(int idnderm, int idperdorues)
        {

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUES", idperdorues, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_ARTIKULLI_merrArtikullSipasNdermarjesAndAutorizimPerRivleresim");
            return ds.Tables[0];

        }

        /// <summary>
        /// kthen datatable artikuj aktiv sipas ndermarjes dhe autorizimeve dhe qe fillojne me kete kod
        /// </summary>
        ///<param name="idNder"> id e ndermarjes</param>
        ///<param name="idperdorues"> id e perdoruesit</param>
        ///<param name="kodArtikulli"> kodi i artikullit</param>
        ///<returns>nje DataTable qe permban nje koleksion me artikuj te kesaj ndermarje dhe per te cilat ky perdorues ka autorizim dhe qe fillojne me kete kod  </returns>
        internal DataTable ktheArtikujNdermarrjesAndAutorizimeLike(int idNder, int idperdorues, string kodArtikulli)
        {

            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@IDNDERMARJE", idNder, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUES", idperdorues, ParameterDirection.Input);
            dbManager.AddParameters(2, "@KODARTIKULLI", kodArtikulli, ParameterDirection.Input);

            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_ARTIKULLI_merrArtikullSipasNdermarjesAndAutorizimLike");
            return ds.Tables[0];


        }

        /// <summary>
        /// kthen datatable artikuj aktiv sipas ndermarjes dhe autorizimeve dhe qe fillojne me kete kod dhe qe s'jane te klasave perbere apo prodhim
        /// </summary>
        ///<param name="idNder"> id e ndermarjes</param>
        ///<param name="idperdorues"> id e perdoruesit</param>
        ///<param name="kodArtikulli"> kodi i artikullit, pra prefixi</param>
        ///<returns>nje DataTable qe permban nje koleksion me artikuj te kesaj ndermarje dhe per te cilat ky perdorues ka autorizim dhe qe fillojne me kete kod  </returns>
        internal DataTable ktheArtikujNdermarrjesAndAutorizimeLikeJoPerbProdhim(int idNder, int idperdorues, string kodArtikulli)
        {

            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@IDNDERMARJE", idNder, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUES", idperdorues, ParameterDirection.Input);
            dbManager.AddParameters(2, "@KODARTIKULLI", kodArtikulli, ParameterDirection.Input);

            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_ARTIKULLI_merrArtikullSipasNdermarjesAndAutorizimJoPerbProdhim");
            return ds.Tables[0];
        }
        /// <summary>
        /// kthen datatable artikuj aktiv sipas ndermarjes dhe autorizimeve dhe qe fillojne me kete kod dhe qe per prodhim
        /// </summary>
        ///<param name="idNder"> id e ndermarjes</param>
        ///<param name="idperdorues"> id e perdoruesit</param>
        ///<param name="kodArtikulli"> kodi i artikullit, pra prefixi</param>
        ///<returns>nje DataTable qe permban nje koleksion me artikuj te kesaj ndermarje dhe per te cilat ky perdorues ka autorizim dhe qe fillojne me kete kod  </returns>
        internal DataTable ktheArtikujNdermarrjesAndAutorizimeLikePerProdhim(int idNder, int idperdorues, string kodArtikulli)
        {

            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@IDNDERMARJE", idNder, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUES", idperdorues, ParameterDirection.Input);
            dbManager.AddParameters(2, "@KODARTIKULLI", kodArtikulli, ParameterDirection.Input);

            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_ARTIKULLI_merrArtikullSipasNdermarjesAndAutorizimPerProdhim");
            return ds.Tables[0];
        }



        /// <summary>
        /// kthen datatable artikuj aktiv sipas ndermarjes dhe autorizimeve dhe qe fillojne me kete kodbar
        /// </summary>
        ///<param name="idNder"> id e ndermarjes</param>
        ///<param name="idperdorues"> id e perdoruesit</param>
        ///<param name="kodbar"> kodbari i artikullit</param>
        ///<returns>nje datatable qe permban nje koleksion me artikuj te kesaj ndermarje dhe per te cilat ky perdorues ka autorizim dhe qe fillojne me kete kodbar  </returns>
        internal DataTable ktheArtikujNdermarrjesAndAutorizimeLikeKodbar(int idNder, int idperdorues, string kodbar)
        {

            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@IDNDERMARJE", idNder, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUES", idperdorues, ParameterDirection.Input);
            dbManager.AddParameters(2, "@KODBARI", kodbar, ParameterDirection.Input);

            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_ARTIKULLI_merrArtikullSipasNdermarjesAndAutorizimLikeKodbari");

            return ds.Tables[0]; ;
        }


        /// <summary>
        /// kthen datatable artikuj sipas id-se
        /// </summary>
        ///<param name="id"> id e artikullit</param>
        ///<returns>nje datatable qe permban nje koleksion me artikuj me kete id  </returns>
        internal DataRow merrArtikull(int id)
        {

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDARTIKULLI", id, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_ARTIKULLI_sel");

            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];
        }

        /// <summary>
        /// kthen id e artikullit sipas id-se dhe idndermarrjes me ane te nje store procedure
        /// </summary>
        ///<param name="kod"> kod i artikullit</param>
        ///<param name="idnderm"> id e ndermarjes</param>
        ///<returns>nje int id artikullit </returns>
        internal int merrIdArtikull(string kod, int idnderm)
        {

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@KODARTIKULLI", kod, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_ARTIKULLI_merrArtikullSipasKodit");
            if (ds == null)
                return -1;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return -1;
            int idArtikulli;
            int.TryParse(ds.Tables[0].Rows[0]["IDARTIKULLI"].ToString(), out idArtikulli);
            return idArtikulli;

        }
        /// <summary>
        /// kthen Datatable  artikuj sipas kodit dhe ndermarjes
        /// </summary>
        ///<param name="idnderm"> id e ndermarjes</param>
        ///<param name="kod"> kodi i artikullit</param>
        ///<returns>nje datatable qe permban nje koleksion me artikuj me kete kod te kesaj ndermarje  </returns>
        internal DataRow merrArtikull(string kod, int idnderm)
        {

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@KODARTIKULLI", kod, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_ARTIKULLI_merrArtikullSipasKodit");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];

        }
        internal DataTable merrArtikuj(string kode, int idnderm)
        {

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@KODARTIKUJSH", kode, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_ARTIKULLI_merrArtikujSipasKodeve");
            if (ds == null)
                return null;
            return ds.Tables[0];

        }

        /// <summary>
        /// perdoret per te kontrolluar nqs ekziston  ky kod artikulli 
        /// behet kontrolli per qellime konsistence te informacionit ne DB, nuk lejon te celen artikuj te ndryshem me te njejtin kod
        /// </summary>
        ///<param name="idnderm"> id e ndermarjes</param>
        ///<param name="kod">kodi i artikullit</param>
        /// <returns>nje objeckt boolean qe tregon nese ekziston apo jo nje artikull me kete kod</returns>
        public bool ekzistonArtikull(String kod, int idnderm)
        {
            ImbLogger.LogTraceShitje($"Filloi metoda ekzistonArtikull me kod:{kod} dhe idnderm:{idnderm}");
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@KODARTIKULLI", kod, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            ImbLogger.LogTraceShitje($"Mbaroi metoda ekzistonArtikull me kod:{kod} dhe idnderm:{idnderm}");
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_ARTIKULLI_ekzistonArtikull");
            if (ds.Tables[0].Rows.Count == 0)
            {
                ImbLogger.LogTraceShitje("Nuk u gjet artikull ne Db");
                return false;
            }
            ImbLogger.LogTraceShitje("U gjet artikull ne Db");
            return true;
        }


        public bool ekzistonKodbarArtikulli(String kod, int idnderm)
        {

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@KODBARARTIKULLI", kod, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            int count = 0;
            int.TryParse(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_ARTIKULLI_ekzistonKodbarArtikulli").ToString(), out count);
            if (count == 1)
                return true;
            return false;
        }
        public DataTable gjejArtikujQeMungojne(string kodet, int idnderm)
        {

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@KODET", kodet, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            var ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_ARTIKULLI_gjejArtikujQeMungojne");

            return ds == null || ds.Tables[0].Rows.Count == 0 ? null : ds.Tables[0];

        }


        public DataTable ktheKodbarePerEksport(int idnderm)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KODBARI_ktheKodbareEksport");
            if (ds == null)
                return null;
            return ds.Tables[0];
        }

        public DataTable merrKodbarSipasNjesivePareSipasIdve(int idDok)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@idDok", idDok, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KODBARI_merrSipasIdShitje");
            if (ds == null)
                return null;

            return ds.Tables[0];
        }
        public DataTable merrKodbarSipasNjesivePareSipasIdveMag(int idDok)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@idDok", idDok, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KODBARI_merrSipasIdMagazine");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0)
                return null;
            return ds.Tables[0];
        }

        public bool eshteTransferuarTekBijArtikull(string kodartikulli, int idndermarje)
        {

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@KODArtikulli", kodartikulli, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idndermarje, ParameterDirection.Input);
            int pergjigje = Convert.ToInt32(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_ARTIKULLI_eshteTransferuarTekBij"));
            return Convert.ToBoolean(pergjigje);
        }
        /// <summary>
        /// merr listen e artikujve sipas kod/kodbar/pershkrim
        /// </summary>
        /// <param name="idNder"></param>
        /// <param name="idperdorues"></param>
        /// <param name="kodPershkArtikulli"></param>
        /// <param name="pershk"></param>
        /// <param name="grup"></param>
        /// <param name="iShitshem"></param>
        /// <param name="merrVetemAfatgjate"></param>
        /// <param name="merrSipasDetajimit"></param>
        /// <param name="klasa"></param>
        /// <param name="gjitheObjektin">percakton nese lista do ktheje gjithe objektet apo vetem id/kod/pershkrim</param>
        /// <returns></returns>
        internal DataTable merrArtikujLikeKodPershkKodbarDT(int idNder, int idperdorues, string kodPershkArtikulli, int pershk, string grup, bool iShitshem, bool merrVetemAfatgjate, bool merrSipasDetajimit, string klasa, bool gjitheObjektin)
        {
            dbManager.Open();
            dbManager.CreateParameters(10);
            dbManager.AddParameters(0, "@IDNDERMARJE", idNder, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUES", idperdorues, ParameterDirection.Input);
            dbManager.AddParameters(2, "@KODPERSHKARTIKULLI", kodPershkArtikulli, ParameterDirection.Input);
            dbManager.AddParameters(3, "@PERSHK", pershk, ParameterDirection.Input);
            dbManager.AddParameters(4, "@grup", grup, ParameterDirection.Input);
            dbManager.AddParameters(5, "@ISHITSHEM", iShitshem ? 1 : 0, ParameterDirection.Input);
            dbManager.AddParameters(6, "@MERRVETEMAFATGJATE", merrVetemAfatgjate ? 1 : 0, ParameterDirection.Input);
            dbManager.AddParameters(7, "@MERRSIPASDETAJIMIT", merrSipasDetajimit, ParameterDirection.Input);
            dbManager.AddParameters(8, "@KLASA", klasa, ParameterDirection.Input);
            dbManager.AddParameters(9, "@GJITHEOBJEKTIN", gjitheObjektin, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_ARTIKULLI_merrArtikullLikeKodPershkKodbarEShpejt");
            return ds.Tables[0];
        }

        internal string merrPershkrimArtikulliNgaKodbari(int idNder, string kodBari)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@KODBARI", kodBari, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idNder, ParameterDirection.Input);
            return Convert.ToString(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_ARTIKULLI_merrPershkrimArtikullSipasKodbarit"));
        }

        internal bool eshteERezervueshmeRecepturaAparat(int idArtikull)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDARTIKULL", idArtikull, ParameterDirection.Input);
            return Convert.ToBoolean(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_ARTIKULLI_eshteERezervueshmeRecepturaAparat"));
        }

        internal DataTable merrArtikujLikeKodPershkKodbarDTJoProdhim(int idNder, int idperdorues, string kodPershkArtikulli, int pershk)
        {
            dbManager.Open();
            dbManager.CreateParameters(4);
            dbManager.AddParameters(0, "@IDNDERMARJE", idNder, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUES", idperdorues, ParameterDirection.Input);
            dbManager.AddParameters(2, "@KODPERSHKARTIKULLI", kodPershkArtikulli, ParameterDirection.Input);
            dbManager.AddParameters(3, "@PERSHK", pershk, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_ARTIKULLI_merrArtikullLikeKodPershkKodbarEShpejtJoProdhim");
            return ds.Tables[0];

        }
        internal DataTable merrArtikujLikeKodPershkKodbarDTPerbere(int idNder, int idperdorues, string kodPershkArtikulli, int pershk)
        {
            dbManager.Open();
            dbManager.CreateParameters(4);
            dbManager.AddParameters(0, "@IDNDERMARJE", idNder, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUES", idperdorues, ParameterDirection.Input);
            dbManager.AddParameters(2, "@KODPERSHKARTIKULLI", kodPershkArtikulli, ParameterDirection.Input);
            dbManager.AddParameters(3, "@PERSHK", pershk, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_ARTIKULLI_merrArtikullLikeKodPershkKodbarEShpejtPerbere");
            return ds.Tables[0];

        }
        internal DataTable merrArtikujLikeKodPershkKodbarDTArtProdhim(int idNder, int idperdorues, string kodPershkArtikulli, int pershk)
        {

            dbManager.Open();
            dbManager.CreateParameters(4);
            dbManager.AddParameters(0, "@IDNDERMARJE", idNder, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUES", idperdorues, ParameterDirection.Input);
            dbManager.AddParameters(2, "@KODPERSHKARTIKULLI", kodPershkArtikulli, ParameterDirection.Input);
            dbManager.AddParameters(3, "@PERSHK", pershk, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_ARTIKULLI_merrArtikullLikeKodPershkKodbarEShpejtartProdhim");
            return ds.Tables[0];

        }
        internal DataTable merrArtikujLikeKodPershkKodbarDTArtProdhimPlanifikim(int idNder, int idperdorues, string kodPershkArtikulli, int pershk, int idplanifikimi)
        {

            dbManager.Open();
            dbManager.CreateParameters(5);
            dbManager.AddParameters(0, "@IDNDERMARJE", idNder, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUES", idperdorues, ParameterDirection.Input);
            dbManager.AddParameters(2, "@KODPERSHKARTIKULLI", kodPershkArtikulli, ParameterDirection.Input);
            dbManager.AddParameters(3, "@PERSHK", pershk, ParameterDirection.Input);
            dbManager.AddParameters(4, "@IDPLANIFIKIMI", idplanifikimi, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_ARTIKULLI_merrArtikullLikeKodPershkKodbarEShpejtartProdhimPlanifikim");
            return ds.Tables[0];

        }
        internal DataTable merrArtikujLikeKodPershkKodbarDTProdhim(int idNder, int idperdorues, string kodPershkArtikulli, int pershk)
        {

            dbManager.Open();
            dbManager.CreateParameters(4);
            dbManager.AddParameters(0, "@IDNDERMARJE", idNder, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUES", idperdorues, ParameterDirection.Input);
            dbManager.AddParameters(2, "@KODPERSHKARTIKULLI", kodPershkArtikulli, ParameterDirection.Input);
            dbManager.AddParameters(3, "@PERSHK", pershk, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_ARTIKULLI_merrArtikullLikeKodPershkKodbarEShpejtProdhim");
            return ds.Tables[0];

        }

        internal DataTable ktheArtikujLikeKodOsePershkDT(int idNder, int idperdorues, string kodPershkArtikulli, int pershk)
        {


            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@IDNDERMARJE", idNder, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUES", idperdorues, ParameterDirection.Input);
            dbManager.AddParameters(2, "@KODPERSHKARTIKULLI", kodPershkArtikulli, ParameterDirection.Input);
            dbManager.AddParameters(3, "@PERSHK", pershk, ParameterDirection.Input);

            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "[prc_T_ARTIKULLI_merrArtikullLikeKodPershkEShpejt]");
            return ds.Tables[0];


        }

        internal bool kaGjendjeArtikulliApoJo(int idArtikull)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDARTIKULLI", idArtikull, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_TRUPIMAGAZINA_ktheGjendjeArtikulliSot");

            if (ds.Tables[0].Rows.Count >= 1)
                return true;
            else
                return false;
        }


        #endregion

        /// <summary>
        /// Metodat e meposhtme i perkasin veprimeve qe kryhen nga objektet clsArtikulliPerberes dhe colArtikulliPerberes
        /// </summary>
        #region ARTIKULLI I PERBERE

        /// <summary>
        /// ekzekuton prc_T_ARTIKULLIPERBERES_ins ne DB dhe kthen statusin e perfundimit te ekzekutimit te SP-se perkatese
        /// Shiko clsMesazh brenda ketij moduli per informacion te metejshem mbi vleren e kthimit te kesaj metode
        /// </summary>
        /// <param name="id"> id  ritese e artikullit perberes</param>
        /// <param name="lloj">lloji i artikullit</param>
        /// <param name="idart"> id e artikullit kryesor</param>
        /// <param name="idlidhart"> id e artikullit perberes</param>
        /// <param name="koef"> koeficienti</param>
        /// <param name="sc"> scrap</param>
        /// <param name="dataderi">data deri kur eshte aktive receptura</param>
        /// <param name="dtndryshimi">data nga eshte aktive receptura</param>
        /// <param name="idlidheseakt">id e aktivitetit perberes</param>
        /// <param name="ngastoku">nga stoku</param>
        /// <returns> nje objekt clsMesazh qe tregon nese ruajtja eshte kryer ne rregull apo jo.</returns>

        internal clsMesazh ruajArtikullPerbere(out int id, int lloj, int idart, int idlidhart, decimal koef, decimal sc, int idlidheseakt, bool ngastoku, DateTime dtndryshimi)
        {
            id = -1;

            dbManager.Open();
            dbManager.CreateParameters(9);
            dbManager.AddParameters(0, "@ID", id, ParameterDirection.Output);
            dbManager.AddParameters(1, "@LLOJI", lloj, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDARTIKULLIKRYESOR", idart, ParameterDirection.Input);
            if (idlidhart == 0)
                dbManager.AddParameters(3, "@IDLIDHESEART", DBNull.Value, ParameterDirection.Input);
            else
                dbManager.AddParameters(3, "@IDLIDHESEART", idlidhart, ParameterDirection.Input);
            dbManager.AddParameters(4, "@KOEFICIENTI", koef, ParameterDirection.Input);
            dbManager.AddParameters(5, "@SCRAP", sc, ParameterDirection.Input);
            if (idlidheseakt == 0)
                dbManager.AddParameters(6, "@IDLIDHESEAKT", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(6, "@IDLIDHESEAKT", idlidheseakt, ParameterDirection.Input);
            dbManager.AddParameters(7, "@GJITHMONENGASTOKU", ngastoku, ParameterDirection.Input);
            if (dtndryshimi.ToShortDateString() == "01/01/0001") dbManager.AddParameters(8, "@DTNDRYSHIMI", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(8, "@DTNDRYSHIMI", dtndryshimi, ParameterDirection.Input);

            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_ARTIKULLIPERBERES_ins");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
            return mesazh;

        }
        //[Obsolete("Perdor: clsMesazh ruajArtikullPerbere(int id, int lloj, int idart, int idlidh, int koef, int vl)", true)]
        //public clsMesazh ruajArtikullPerbere(clsArtikulliPerberes artikull)
        //{
        //    try
        //    {
        //        dbManager.CreateParameters(6);
        //        dbManager.AddParameters(0, "@ID", artikull.IdArtikulliPerberes, ParameterDirection.Output);
        //        dbManager.AddParameters(1, "@LLOJI", artikull.Lloji, ParameterDirection.Input);
        //        dbManager.AddParameters(2, "@IDARTIKULLIKRYESOR", artikull.IdArtikulliKryesor, ParameterDirection.Input);
        //        dbManager.AddParameters(3, "@IDLIDHESE", artikull.IdLidhese, ParameterDirection.Input);
        //        dbManager.AddParameters(4, "@KOEFICIENTI", artikull.Koeficienti, ParameterDirection.Input);
        //        dbManager.AddParameters(5, "@VLERA", artikull.Vlera, ParameterDirection.Input);
        //        dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_ARTIKULLIPERBERES_ins");
        //        clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
        //        return mesazh;
        //    }
        //    catch (Exception)
        //    {
        //        return new clsMesazh(false, ce.Message);
        //    }
        //}

        /// <summary>
        /// kthen datatable artikuj perberes sipas id artikullit kryesor
        /// </summary>
        ///<param name="id">id e artikullit kryesor</param>
        ///<returns>nje datatable qe permban nje koleksion me te gjitha artikujt perberes te ketij artikulli  </returns>
        internal DataTable ktheArtikujPerberesSipasIdArtikullitKryesor(int id)
        {

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDARTIKULLIKRYESOR", id, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_ARTIKULLIPERBERES_merrArtikujPerberesSipasIdArtikullitKryesor");

            return ds.Tables[0];

        }

        /// <summary>
        /// kthen datatable artikuj perberes sipas id artikullit kryesor
        /// </summary>
        ///<param name="id">id e artikullit kryesor</param>
        ///<returns>nje datatable qe permban nje koleksion me te gjitha artikujt perberes te ketij artikulli  </returns>
        internal DataTable ktheArtikujPerberesSipasIdArtikujve_Kryesor(string id, int idNdermarrje, DateTime data)
        {

            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@IDARTIKULLIKRYESOR", id, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERRMARJE", idNdermarrje, ParameterDirection.Input);
            dbManager.AddParameters(2, "@DATA", data.ToShortDateString(), ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_GrupimPerberesish_merrArtikujPerberesSipasArtikujveKryesor");

            return ds.Tables[0];

        }
        /// <summary>
        /// kthen datatable artikuj perberes sipas id artikullit kryesor
        /// </summary>
        ///<param name="id">id e artikullit kryesor</param>
        ///<returns>nje datatable qe permban nje koleksion me te gjitha artikujt perberes te ketij artikulli  </returns>
        internal DataTable ktheArtikujPerberesSipasIdArtikullitKryesorPare(int id, DateTime data)
        {

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDARTIKULLIKRYESOR", id, ParameterDirection.Input);
            dbManager.AddParameters(1, "@DATA", data, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_ARTIKULLIPERBERES_merrArtikullParePerberesSipasIdArtikullitKryesor");

            return ds.Tables[0];

        }

        internal String ktheKoeficentArtikullPerberesSipasIdArtikullitKryesor(int id)
        {

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDARTIKULLIKRYESOR", id, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_ARTIKULLIPERBERES_merrKoeficentArtikullPerberesSipasIdArtikullitKryesor");

            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0]["KOEFICIENTI"].ToString();

        }


        /// <summary>
        /// kthen datatable artikuj perberes sipas id artikullit kryesor per lupen e infos se artikujve te perbere
        /// </summary>
        ///<param name="id">id e artikullit kryesor</param>
        ///<returns>nje datatable qe permban nje koleksion me te gjitha artikujt perberes te ketij artikulli  </returns>
        internal DataTable ktheArtikujPerberesSipasIdArtikullitKryesorLupa(int id, DateTime data)
        {

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDARTIKULLIKRYESOR", id, ParameterDirection.Input);
            dbManager.AddParameters(1, "@DATA", data, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_ARTIKULLIPERBERES_merrArtikujPerberesSipasIdArtikullitKryesorLupa");

            return ds.Tables[0];
        }
        /// <summary>
        /// kthen datatable artikuj qe jane perberes sipas id artikullit kryesor dhe dates
        /// </summary>
        ///<param name="id">id e artikullit kryesor</param>
        ///<param name="data">data e ndryshimit</param>
        ///<returns>nje datatable qe permban nje koleksion me te gjitha artikujt qe jane perberes te ketij artikulli  </returns>
        internal DataTable ktheArtikujPerberesSipasIdArtikullitKryesorDheDate(int id, DateTime data)
        {

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDARTIKULLIKRYESOR", id, ParameterDirection.Input);
            dbManager.AddParameters(1, "@DATA", data, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_ARTIKULLI_merrArtikujPerberesSipasIdArtikullitKryesorDheDates");

            return ds.Tables[0];
        }
        /// <summary>
        /// kthen datatable artikuj te perbere sipas id artikullit perberes per lupen e infos se artikujve te perbere
        /// </summary>
        ///<param name="id">id e artikullit kryesor</param>
        ///<returns>nje datatable qe permban nje koleksion me te gjitha artikujt perberes te ketij artikulli  </returns>
        internal DataTable ktheArtikujtEPerbereSipasIdArtikullitPerberes(int id, DateTime data)
        {

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDARTIKULLIPERBERES", id, ParameterDirection.Input);
            dbManager.AddParameters(1, "@DATA", data, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_ARTIKULLIPERBERES_merrArtikujTePerbereSipasIdArtikullitPerberesLupa");

            return ds.Tables[0];
        }


        /// <summary>
        /// kthen datatable grupim perberesish sipas id artikullit kryesor per lupen e infos se grupimit te perberesve
        /// </summary>
        ///<param name="id">id e artikullit kryesor</param>
        ///<returns>nje datatable qe permban nje koleksion me te gjitha grupimet e  perberesve te ketij artikulli  </returns>
        internal DataTable ktheGrupimPerberesishSipasIdArtikullitKryesorLupa(string id, DateTime data)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDARTIKULLIKRYESOR", id, ParameterDirection.Input);
            dbManager.AddParameters(1, "@DATA", data, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_GRUPIMPERBERESISH_merrGrupimPerberesishSipasIdArtikullitKryesorLupa");
            return ds.Tables[0];
        }



        /// <summary>
        /// kthen datatable artikuj perberes sipas id artikullit kryesor
        /// </summary>
        ///<param name="id">id e artikullit kryesor</param>
        ///<returns>nje datatable qe permban nje koleksion me te gjitha artikujt perberes te ketij artikulli  </returns>
        internal DataTable ktheArtikujPerberesSipasIdArtikulliLidhes(int id)
        {

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDARTIKULLIKRYESOR", id, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_ARTIKULLIPERBERES_merrArtikujPerberesSipasIdLidhese");

            return ds.Tables[0];

        }
        internal DataTable ktheArtikujPerberesSipasIdArtikujve(string ids, DateTime data)
        {

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters("@IDARTIKUJKRYESOR", ids, ParameterDirection.Input);
            dbManager.AddParameters("@DATE", data, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_ARTIKULLIPERBERES_merrArtikujtPerberesSipasIdArtikullitKryesor");

            return ds.Tables[0];
        }
        /// <summary>
        /// kthen datatable artikuj perberes sipas id artikullit kryesor dhe dates
        /// </summary>
        ///<param name="id">id e artikullit kryesor</param>
        ///<returns>nje datatable qe permban nje koleksion me te gjitha artikujt perberes te ketij artikulli  </returns>
        internal bool ekzistonArtikullPerberesPerKeteDate(int id, DateTime data, int idlidhese, int lloji)
        {

            dbManager.Open();
            dbManager.CreateParameters(4);
            dbManager.AddParameters(0, "@IDARTIKULLIKRYESOR", id, ParameterDirection.Input);
            dbManager.AddParameters(1, "@DATA", data, ParameterDirection.Input);
            dbManager.AddParameters(2, "@idlidhes", idlidhese, ParameterDirection.Input);
            dbManager.AddParameters(3, "@lloji", lloji, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_ARTIKULLIPERBERES_ekzistonReceptureNeKeteDate");

            if (ds.Tables[0].Rows.Count == 1)
                return true;
            else if (ds.Tables[0].Rows.Count == 0)
                return false;
            else return true;

        }
        internal DataTable ktheArtikujPerberesSipasIdArtikullitKryesorDheDates(int id, DateTime data)
        {

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDARTIKULLIKRYESOR", id, ParameterDirection.Input);
            dbManager.AddParameters(1, "@DATA", data, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_ARTIKULLIPERBERES_merrArtikujPerberesSipasIdArtikullitKryesorDheDates");

            return ds.Tables[0];

        }
        internal DataTable ktheArtikujPerberesSipasIdArtikullitKryesorDheDatesMeTeAfertProdhim(int id, DateTime data)
        {

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDARTIKULLIKRYESOR", id, ParameterDirection.Input);
            dbManager.AddParameters(1, "@DATA", data, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_ARTIKULLIPERBERES_merrArtikujPerberesSipasIdArtikullitKryesorDheDatesPerArtikujProdhimJoNgaStoku");

            return ds.Tables[0];

        }
        internal DataTable ktheArtikujPerberesSipasIdArtikullitKryesorDheDatesMeTeAfert(int id, DateTime data)
        {
            ImbLogger.LogTraceShitje($"Filloi metoda ktheArtikujPerberesSipasIdArtikullitKryesorDheDatesMeTeAfert sipas id:{id}, dhe data:{data}");
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDARTIKULLIKRYESOR", id, ParameterDirection.Input);
            dbManager.AddParameters(1, "@DATA", data, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_ARTIKULLIPERBERES_merrArtikujPerberesSipasIdArtikullitKryesorDheDatesMeTeAfert");

            ImbLogger.LogTraceShitje($"Mbaroi metoda ktheArtikujPerberesSipasIdArtikullitKryesorDheDatesMeTeAfert sipas id:{id}, dhe data:{data}");
            return ds.Tables[0];

        }
        //internal DataTable ktheArtikujPerberesSipasIdArtikullitKryesorMeKod(int id)
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
        //        dbManager.AddParameters(0, "@IDARTIKULLIKRYESOR", id, ParameterDirection.Input);
        //        DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_ARTIKULLIPERBERES_merrArtikujPerberesSipasIdArtikullitKryesorMeKod");

        //        return ds.Tables[0];
        //    }
        //    catch (Exception)
        //    {
        //        return null;
        //    }
        //}
        //[Obsolete("Perdor: DataTable ktheArtikujPerberesSipasIdArtikullitKryesor(int id)", true)]
        //public colArtikulliPerberes merrArtikujPerberesSipasIdArtikullitKryesor(int id)
        //{
        //    dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        //    dbManager.ConnectionString = dbManager.GetConnectionString();

        //    try
        //    {
        //        dbManager.Open();
        //        dbManager.CreateParameters(1);
        //        dbManager.AddParameters(0, "@IDARTIKULLIKRYESOR", id, ParameterDirection.Input);
        //        DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_ARTIKULLIPERBERES_merrArtikujPerberesSipasIdArtikullitKryesor");
        //        colArtikulliPerberes artikujt = new colArtikulliPerberes();
        //        return artikujt.mbushArrayListArtikujPerberes(ds);
        //    }
        //    catch (Exception)
        //    {
        //        return new colArtikulliPerberes();
        //    }
        //    finally
        //    {
        //        dbManager.Dispose();
        //    }
        //}

        /// <summary>
        /// ekzekutohet sp-ja prc_T_ARTIKULLIPERBERES_del duke i kaluar id e artikullit kryesor qe e marrim nga objekti clsArtikulli qe i kalohet si parameter
        /// </summary>
        /// <param name="artikull"> artikulli kryesor nga i cili do te fshihen te gjithe artikujt perberes </param>
        /// <returns>nje objekt clsMesazh qe tregon nese fshirja eshte kryer ne rregull apo jo</returns>
        internal clsMesazh fshiArtikujPerberes(int idArtikulli)
        {

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDARTIKULLIKRYESOR", idArtikulli, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_ARTIKULLIPERBERES_del");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
            return mesazh;

        }
        /// <summary>
        /// ekzekutohet sp-ja prc_T_ARTIKULLIPERBERES_del duke i kaluar id e artikullit kryesor qe e marrim nga objekti clsArtikulli qe i kalohet si parameter
        /// </summary>
        /// <param name="artikull"> artikulli kryesor nga i cili do te fshihen te gjithe artikujt perberes </param>
        /// <returns>nje objekt clsMesazh qe tregon nese fshirja eshte kryer ne rregull apo jo</returns>
        internal clsMesazh fshiArtikujPerberesSipasDates(int idArtikulli, DateTime data)
        {

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDARTIKULLIKRYESOR", idArtikulli, ParameterDirection.Input);
            dbManager.AddParameters(1, "@DATA", data, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_ARTIKULLIPERBERES_delSipasDates");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
            return mesazh;

        }
        internal DataTable merrRecepturaPerExport(int idndermarje)
        {
            try
            {
                dbManager.Open();
                dbManager.CreateParameters(1);
                dbManager.AddParameters(0, "@IDNDERMARJE", idndermarje, ParameterDirection.Input);
                DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_ARTIKULLIPERBERES_merrPerExport");

                return ds.Tables[0];
            }
            catch (Exception)
            {
                return null;
            }
        }
        /// <summary>
        /// merr gjithe datat e ndryshimit te ketij artikulli perberes
        /// </summary>
        /// <param name="idkoka">id e kokes, pra id e artikullit kryesor</param>
        /// <returns> kthen nje datatable me te gjithe datat e ndryshimeve te aktiviteteve</returns>
        internal DataTable merrDataNdryshimiArktikujPerberes(int idkoka)
        {


            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDKOKA", idkoka, ParameterDirection.Input);
            using (DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_ARTIKULLIPERBERES_merrGjitheDatatSipasIdKoka"))
            {
                return ds.Tables[0];
            }

        }
        #region T_GJENDJEARTIKULLI
        internal DataTable ktheGjendjeMinMaxArtikulli(int idArtikulli)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDARTIKULLI", idArtikulli, ParameterDirection.Input);
            return dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_GJENDJEARTIKULLI_merrSipasIdArt").Tables[0];
        }

        internal DataTable ktheGjendjeMinMaxArtikulliSipasMag(int idArtikulli, int idMagazina)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters("@IDARTIKULLI", idArtikulli, ParameterDirection.Input);
            dbManager.AddParameters("@IDMAGAZINA", idMagazina, ParameterDirection.Input);
            return dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_GJENDJEARTIKULLI_merrSipasIdArtIdMag").Tables[0];
        }
        internal DataTable ktheGjendjeMinMaxArtikulliSipasMag(int idArtikulli, string kodMagazina, int idNdermarrje)
        {
            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters("@IDARTIKULLI", idArtikulli, ParameterDirection.Input);
            dbManager.AddParameters("@KODMAGAZINA", kodMagazina, ParameterDirection.Input);
            dbManager.AddParameters("@IDNDERMARRJE", idNdermarrje, ParameterDirection.Input);
            return dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_GJENDJEARTIKULLI_merrSipasIdArtKodMag").Tables[0];
        }
        internal clsMesazh ruajGjendjeArtikulli(out int id, int idartikulli, int idmagazina, double gjendjamin, double gjendjamax)
        {
            id = -1;

            dbManager.Open();
            dbManager.CreateParameters(5);
            dbManager.AddParameters(0, "@IDGJENDJEARTIKULLI", id, ParameterDirection.Output);
            dbManager.AddParameters(1, "@IDARTIKULLI", idartikulli, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDMAGAZINA", idmagazina, ParameterDirection.Input);
            dbManager.AddParameters(3, "@GJENDJAMIN", gjendjamin, ParameterDirection.Input);
            dbManager.AddParameters(4, "@GJENDJAMAX", gjendjamax, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_GJENDJEARTIKULLI_ins");
            clsMesazh mesazh = new clsMesazh(true, IMBUtils.Messages.MessagesResource.Messages["labelRaportMesazhRuajtjaPerfundoiSukses"]);
            return mesazh;

        }
        internal clsMesazh fshiGjendjeArtikulli(int idartikulli)
        {

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDARTIKULLI", idartikulli, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_GJENDJEARTIKULLI_del");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
            return mesazh;
        }
        #endregion

        //[Obsolete("Perdor: clsMesazh fshiArtikujPerberes(int idArtikulli)", true)]
        //private clsMesazh fshiArtikujPerberes(clsArtikulli artikulli)
        //{
        //    IDBManager dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        //    dbManager.ConnectionString = dbManager.GetConnectionString();
        //    try
        //    {
        //        dbManager.Open();
        //        dbManager.CreateParameters(1);
        //        dbManager.AddParameters(0, "@IDARTIKULLIKRYESOR", artikulli.IdArtikulli, ParameterDirection.Input);
        //        dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_ARTIKULLIPERBERES_del");
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
        /// Metodat e meposhtme i perkasin veprimeve qe kryhen nga objektet clsArtikullPerberesTemplateKoka,clsArtikulliPerberesTemplateTrupi dhe colArtikulliPerberesTemplateKoka,colArtikulliPerberesTemplateTrupi
        /// </summary>
        #region TEMPLATE - ARTIKULL I PERBERE

        //public clsMesazh ruajTemplateArtikullPerberes(clsArtikullPerberesTemplateKoka koka)
        //{
        //    dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        //    dbManager.ConnectionString = dbManager.GetConnectionString();
        //    dbManager.Open();
        //    dbManager.BeginTransaction();

        //    try
        //    {
        //        ruajTemplateArtikullPerberesKoka(koka);

        //        foreach (clsArtikulliPerberesTemplateTrupi o in koka.oColTrupi)
        //        {
        //            o.IdKoka = koka.IdKoka;
        //            ruajTemplateArtikullPerberesTrupi(o);
        //        }
        //        dbManager.CommitTransaction();
        //        clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
        //        return mesazh;
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
        /// ekzekuton prc_T_ARTIKULLIPERBERESTEMPLATEKOKA_ins ne DB dhe kthen statusin e perfundimit te ekzekutimit te SP-se perkatese
        /// Shiko clsMesazh brenda ketij moduli per informacion te metejshem mbi vleren e kthimit te kesaj metode
        /// <param name="id"> id ritese e trupit</param>
        /// <param name="k">id e kokes</param>
        /// <param name="p"> pershrimi </param>
        /// <returns> nje objekt boolean qe tregon nese ruajtja eshte kryer ne rregull apo jo.</returns>
        /// </summary>
        internal clsMesazh ruajTemplateArtikullPerberesKoka(out int id, string k, string p, int idndermarje)
        {
            id = -1;

            dbManager.Open();
            dbManager.CreateParameters(4);
            dbManager.AddParameters(0, "@IDKOKA", id, ParameterDirection.Output);
            dbManager.AddParameters(1, "@KODI", k, ParameterDirection.Input);
            dbManager.AddParameters(2, "@PERSHKRIMI", p, ParameterDirection.Input);
            dbManager.AddParameters(3, "@IDNDERMARJE", idndermarje, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_ARTIKULLIPERBERESTEMPLATEKOKA_ins");

            id = int.Parse(dbManager.Parameters[0].Value.ToString());
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
            return mesazh;

        }
        [Obsolete("Perdor: clsMesazh ruajTemplateArtikullPerberesKoka(int id, string k, string p)", true)]
        public clsMesazh ruajTemplateArtikullPerberesKoka(clsArtikullPerberesTemplateKoka koka)
        {

            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@IDKOKA", koka.IdKoka, ParameterDirection.Output);
            dbManager.AddParameters(1, "@KODI", koka.Kodi, ParameterDirection.Input);
            dbManager.AddParameters(2, "@PERSHKRIMI", koka.Pershkrimi, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_ARTIKULLIPERBERESTEMPLATEKOKA_ins");

            koka.IdKoka = int.Parse(dbManager.Parameters[0].Value.ToString());
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
            return mesazh;

        }

        /// <summary>
        /// ekzekuton prc_T_ARTIKULLIPERBERESTEMPLATETRUPI_ins ne DB dhe kthen statusin e perfundimit te ekzekutimit te SP-se perkatese
        /// Shiko clsMesazh brenda ketij moduli per informacion te metejshem mbi vleren e kthimit te kesaj metode
        /// <param name="idtr"> id ritese e trupit</param>
        /// <param name="idko">id e kokes</param>
        /// <param name="lloj"> lloji</param>
        /// <param name="idlidh"> id e artikullit perberes</param>
        /// <param name="koef"> koeficienti</param>
        /// <param name="vl"> vlera</param>
        /// <returns> nje objekt boolean qe tregon nese ruajtja eshte kryer ne rregull apo jo.</returns>
        /// </summary>
        internal clsMesazh ruajTemplateArtikullPerberesTrupi(out int idtr, int idko, int lloj, int idlidhart, int koef, decimal vl, int idlidhesellog)
        {
            idtr = -1;

            dbManager.Open();
            dbManager.CreateParameters(7);
            dbManager.AddParameters(0, "@IDTRUPI", idtr, ParameterDirection.Output);
            dbManager.AddParameters(1, "@LLOJI", lloj, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDKOKA", idko, ParameterDirection.Input);
            if (idlidhart == 0)
                dbManager.AddParameters(3, "@IDLIDHESEART", DBNull.Value, ParameterDirection.Input);
            else
                dbManager.AddParameters(3, "@IDLIDHESEART", idlidhart, ParameterDirection.Input);
            dbManager.AddParameters(4, "@KOEFICIENTI", koef, ParameterDirection.Input);
            dbManager.AddParameters(5, "@VLERA", vl, ParameterDirection.Input);
            if (idlidhesellog == 0)
                dbManager.AddParameters(6, "@IDLIDHESELLOG", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(6, "@IDLIDHESELLOG", idlidhesellog, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_ARTIKULLIPERBERESTEMPLATETRUPI_ins");

            idtr = int.Parse(dbManager.Parameters[0].Value.ToString());
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
            return mesazh;

        }
        [Obsolete("Perdor: clsMesazh ruajTemplateArtikullPerberesTrupi(int idtr, int idko, int lloj, int idlidh, int koef, int vl)", true)]
        public clsMesazh ruajTemplateArtikullPerberesTrupi(clsArtikulliPerberesTemplateTrupi trupi)
        {

            dbManager.CreateParameters(6);
            dbManager.AddParameters(0, "@IDTRUPI", trupi.IdTrupi, ParameterDirection.Output);
            dbManager.AddParameters(1, "@LLOJI", trupi.Lloji, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDKOKA", trupi.IdKoka, ParameterDirection.Input);
            dbManager.AddParameters(3, "@IDLIDHESE", trupi.IdLidheseArt, ParameterDirection.Input);
            dbManager.AddParameters(4, "@KOEFICIENTI", trupi.Koeficienti, ParameterDirection.Input);
            dbManager.AddParameters(5, "@VLERA", trupi.Vlera, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_ARTIKULLIPERBERESTEMPLATETRUPI_ins");

            trupi.IdTrupi = int.Parse(dbManager.Parameters[0].Value.ToString());
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
            return mesazh;

        }

        /// <summary>
        /// kthen  datatable template artikuj perberes 
        /// </summary>
        ///<returns>nje datatable qe permban nje koleksion me te gjitha templatet artikujt perberes</returns>
        internal DataTable ktheGjitheTemplatetArtikujvePerberes(int idndermarje)
        {

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDNDERMARJE", idndermarje, ParameterDirection.Output);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_ARTIKULLIPERBERESTEMPLATEKOKA_merrGjitheTemplatetArtikujvePerberes");

            return ds.Tables[0];

        }
        //[Obsolete("Perdor: bool ktheGjitheTemplatetArtikujvePerberes()", true)]
        //public colArtikulliPerberesTemplateKoka merrGjitheTemplatetArtikujvePerberes()
        //{
        //    IDBManager dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        //    dbManager.ConnectionString = dbManager.GetConnectionString();
        //    try
        //    {
        //        dbManager.Open();
        //        DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_ARTIKULLIPERBERESTEMPLATEKOKA_merrGjitheTemplatetArtikujvePerberes");
        //        colArtikulliPerberesTemplateKoka kokat = new colArtikulliPerberesTemplateKoka();
        //        return kokat.mbushArrayListArtikujPerberes(ds);
        //    }
        //    catch (Exception)
        //    {
        //        return new colArtikulliPerberesTemplateKoka();
        //    }
        //    finally
        //    {
        //        dbManager.Dispose();
        //    }
        //}

        /// <summary>
        /// kthen dataTable trup template artikuj perberes sipas idse se kokes
        /// </summary>
        /// <param name="idKoka"> id koka e templatit </param>
        /// <returns> nje dataTable qe permban nje koleksion me te gjitha trupat e templatet qe kane kete id koke</returns>
        internal DataTable ktheTrupTemplateSipasKokes(int idKoka)
        {


            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDKOKA", idKoka, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_ARTIKULLIPERBERESTEMPLATETRUPI_merrTrupTemplateSipasKokes");

            return ds.Tables[0];

        }
        //[Obsolete("Perdor: DataTable ktheTrupTemplateSipasKokes(int idKoka)", true)]
        //public colArtikulliPerberesTemplateTrupi merrTrupTemplateSipasKokes(int idKoka)
        //{
        //    IDBManager dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        //    dbManager.ConnectionString = dbManager.GetConnectionString();
        //    try
        //    {
        //        dbManager.Open();
        //        dbManager.CreateParameters(1);
        //        dbManager.AddParameters(0, "@IDKOKA", idKoka, ParameterDirection.Input);
        //        DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_ARTIKULLIPERBERESTEMPLATETRUPI_merrTrupTemplateSipasKokes");
        //        colArtikulliPerberesTemplateTrupi trupat = new colArtikulliPerberesTemplateTrupi();
        //        return trupat.mbushArrayListArtikujPerberes(ds);
        //    }
        //    catch (Exception)
        //    {
        //        return new colArtikulliPerberesTemplateTrupi();
        //    }
        //    finally
        //    {
        //        dbManager.Dispose();
        //    }
        //}

        /// <summary>
        /// kthen templetin artikulli perberes sipas id
        /// </summary>
        /// <param name="id"> id koka e templatit</param>
        /// <returns> nje datarow qe permban templetin qe ka kete id</returns>
        internal DataRow ktheTemplateArtikulliPerberesSipasID(int id)
        {


            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDKOKA", id, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_ARTIKULLIPERBERESTEMPLATEKOKA_merrTemplateArtikulliPerberesSipasID");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];

        }
        //[Obsolete("Perdor: DataTable ktheTemplateArtikulliPerberesSipasID(int id)", true)]
        //public colArtikulliPerberesTemplateKoka merrTemplateArtikulliPerberesSipasID(int id)
        //{
        //    IDBManager dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        //    dbManager.ConnectionString = dbManager.GetConnectionString();
        //    try
        //    {
        //        dbManager.Open();
        //        dbManager.CreateParameters(1);
        //        dbManager.AddParameters(0, "@IDKOKA", id, ParameterDirection.Input);
        //        DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_ARTIKULLIPERBERESTEMPLATEKOKA_merrTemplateArtikulliPerberesSipasID");
        //        colArtikulliPerberesTemplateKoka kokat = new colArtikulliPerberesTemplateKoka();
        //        return kokat.mbushArrayListArtikujPerberes(ds);
        //    }
        //    catch (Exception)
        //    {
        //        return new colArtikulliPerberesTemplateKoka();
        //    }
        //    finally
        //    {
        //        dbManager.Dispose();
        //    }
        //}

        /// <summary>
        /// kthen id e kokes se  template artikulli perberes sipas kodit 
        /// </summary>
        /// <param name="kodi"> kodi i templatit</param>
        /// <returns>nje integer qe permban id e kokes se templatin qe ka kete kod</returns>
        internal int ktheIdKokaTemplateArtikulliPerberesSipasKodit(String kodi, int idndermarje)
        {

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@KODI", kodi, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idndermarje, ParameterDirection.Output);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_ARTIKULLIPERBERESTEMPLATEKOKA_merrTemplateArtikulliPerberesSipasKodit");
            if (ds == null)
                return -1;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return -1;
            int idKoka;
            int.TryParse(ds.Tables[0].Rows[0]["IDKOKA"].ToString(), out idKoka);
            return idKoka;

        }
        //[Obsolete("Perdor: DataRow ktheTemplateArtikulliPerberesSipasKodit(String kodi)", true)]
        //public colArtikulliPerberesTemplateKoka merrTemplateArtikulliPerberesSipasKodit(String kodi)
        //{
        //    IDBManager dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        //    dbManager.ConnectionString = dbManager.GetConnectionString();
        //    try
        //    {
        //        dbManager.Open();
        //        dbManager.CreateParameters(1);
        //        dbManager.AddParameters(0, "@KODI", kodi, ParameterDirection.Input);
        //        DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_ARTIKULLIPERBERESTEMPLATEKOKA_merrTemplateArtikulliPerberesSipasKodit");
        //        colArtikulliPerberesTemplateKoka kokat = new colArtikulliPerberesTemplateKoka();
        //        return kokat.mbushArrayListArtikujPerberes(ds);
        //    }
        //    catch (Exception)
        //    {
        //        return new colArtikulliPerberesTemplateKoka();
        //    }
        //    finally
        //    {
        //        dbManager.Dispose();
        //    }
        //}

        #endregion

        #endregion

        /// <summary>
        /// Metodat e meposhtme i perkasin veprimeve qe kryhen nga objektet clsKlasaArtikulli dhe colKlasaArtikulli
        /// </summary>
        #region KLASAARTIKULLI

        /// <summary>
        /// kthen datatable klasa artikulli
        /// </summary>
        ///<returns>nje datatable qe permban nje koleksion me te gjitha klasat e artikujve  </returns>
        internal DataTable ktheGjitheKlasaArtikulli()
        {


            dbManager.Open();
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KLASAARTIKULLI _merrGjitheKlasat");
            return ds.Tables[0];

        }
        internal DataRow merrKodOperatoriSipasEmritDheMbiemrit(string emri, string mbiemri, int idndermarje)
        {
            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@IDNDERMARJE", idndermarje, ParameterDirection.Input);
            dbManager.AddParameters(1, "@EMRI", emri, ParameterDirection.Input);
            dbManager.AddParameters(2, "@MBIEMRI", mbiemri, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_OPERATORE_merrKodOperatoriSipasEmritDheMbiemrit");
            return ds.Tables[0].Rows[0];
        }
        internal DataRow merrEmerDheMbiemerOperatoriSipasId(int id, int idndermarje)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDOPERATOR", id, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARRJE", idndermarje, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_OPERATORE_merrEmrinDheMbiemrinSipasId");
            return ds.Tables[0].Rows[0];
        }
        internal DataRow merrIdOperatoriSipasKodOperatori(string KodOperatori, int idndermarje)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@KODI", KodOperatori, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARRJE", idndermarje, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_OPERATORE_merrIdOperatoriSipasKodOperatori");
            if (ds.Tables[0].Rows.Count==0)
            {
                DataTable table = new DataTable();
                DataRow row = table.NewRow();
                
                return row;
            }
            else 
            return ds.Tables[0].Rows[0];
        }
        internal int merrEmerMbiemerOperatoriSipasKodOperatori(string KodOperatori, int idndermarje)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@KODI", KodOperatori, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARRJE", idndermarje, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_OPERATORE_merrEmerDheMbiemerOperatoriSipasKodOperatori");
            return ds.Tables[0].Rows.Count;
        }

        //[Obsolete("Perdor: DataTable ktheGjitheKlasaArtikulli()", false)]
        //public colKlasaArtikulli merrGjitheKlasaArtikulli()
        //{
        //    IDBManager dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        //    dbManager.ConnectionString = dbManager.GetConnectionString();
        //    try
        //    {
        //        dbManager.Open();

        //        DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KLASAARTIKULLI _merrGjitheKlasat");
        //        colKlasaArtikulli colKlasaArtikulli = new colKlasaArtikulli();
        //        return colKlasaArtikulli.mbushArrayListKlasaArtikulli(ds);
        //    }
        //    catch (Exception)
        //    {
        //        return new colKlasaArtikulli();
        //    }
        //    finally
        //    {
        //        dbManager.Dispose();
        //    }

        //}

        /// <summary>
        /// kthen datarow klasat e artikullit sipas id
        /// </summary>
        /// <param name="id"> id e klases se artikullit</param>
        /// <returns> nje datarow qe permban nje koleksion klasen e artikullit qe ka kete id</returns>
        internal DataRow ktheKlasaArtikulliSipasId(int id)
        {


            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDKLASAARTIKULLI", id, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KLASAARTIKULLI _merrKlasaSipasId");

            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];

        }
        //[Obsolete("Perdor: DataRow ktheKlasaArtikulliSipasId(int id)", false)]
        //public colKlasaArtikulli merrKlasaArtikulliSipasId(int Id)
        //{
        //    IDBManager dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        //    dbManager.ConnectionString = dbManager.GetConnectionString();
        //    try
        //    {
        //        dbManager.Open();
        //        dbManager.CreateParameters(1);
        //        dbManager.AddParameters(0, "@IDKLASAARTIKULLI", Id, ParameterDirection.Input);
        //        DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KLASAARTIKULLI _merrKlasaSipasId");
        //        colKlasaArtikulli colKlasaArtikulli = new colKlasaArtikulli();
        //        return colKlasaArtikulli.mbushArrayListKlasaArtikulli(ds);
        //    }
        //    catch (Exception)
        //    {
        //        return new colKlasaArtikulli();
        //    }
        //    finally
        //    {
        //        dbManager.Dispose();
        //    }

        //}

        /// <summary>
        /// kthen datatable e artikullit sipas pershkrimit
        /// </summary>
        /// <param name="pershkrimi"> pershkrimi i klases se artikujve</param>
        /// <returns> nje datatable qe permban nje koleksion me te gjitha klasat e artikujve qe kane kete pershkrim</returns>
        internal DataRow ktheKlasaArtikulliSipasPershkrimit(string pershkrimi)
        {


            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@PERSHKRIMIKLASAARTIKULLI", pershkrimi, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KLASAARTIKULLI _merrKlasaSipasPershkrimit");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];

        }
        //[Obsolete("Perdor: DataTable ktheKlasaArtikulliSipasPershkrimit(string pershkrimi)", false)]
        //public colKlasaArtikulli merrKlasaArtikulliSipasId(string pershkrimi)
        //{
        //    IDBManager dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        //    dbManager.ConnectionString = dbManager.GetConnectionString();
        //    try
        //    {
        //        dbManager.Open();
        //        dbManager.CreateParameters(1);
        //        dbManager.AddParameters(0, "@PERSHKRIMIKLASAARTIKULLI", pershkrimi , ParameterDirection.Input);
        //        DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KLASAARTIKULLI _merrKlasaSipasPershkrimit");
        //        colKlasaArtikulli colKlasaArtikulli = new colKlasaArtikulli();
        //        return colKlasaArtikulli.mbushArrayListKlasaArtikulli(ds);
        //    }
        //    catch (Exception)
        //    {
        //        return new colKlasaArtikulli();
        //    }
        //    finally
        //    {
        //        dbManager.Dispose();
        //    }

        //}

        #endregion

        /// <summary>
        /// Metodat e meposhtme i perkasin veprimeve qe kryhen nga objektet clsNivelCmimi dhe colNiveleCmimesh
        /// </summary>
        #region NIVEL CMIMI

        ///// <summary>
        ///// ekzekuton prc_T_NIVELCMIMI_ins ne DB dhe kthen statusin e perfundimit te ekzekutimit te SP-se perkatese
        ///// procedura krijon edhe nr tjeter automatik nqs niveli i cmimit eshte i lidhur me nr automatik
        ///// Shiko clsMesazh brenda ketij moduli per informacion te metejshem mbi vleren e kthimit te kesaj metode
        ///// <param name="idNivelCmimi">id ritese e nivelit te cmimit</param>
        ///// <param name="kodNivelCmimi">kodi i nivelit te cmimit</param>
        ///// <param name="pershkrimNivelCmimi">pershkrimi i nivelit te cmimit</param>
        ///// <param name="idPrindi"> id e prindit </param>
        ///// <param name="llojiNivelCmimi"> lloji i nivelit te cmimit</param>
        ///// <param name="idMonedha">id e monedhes</param>
        ///// <param name="brutoNetoNivelCmimi"> bruto/Neto </param>
        ///// <param name="prioritetiNivelCmimi"> prioriteti</param>
        ///// <param name="idPerdoruesi"> id e perdoruesit qe ka kryer veprimin</param>
        ///// <param name="idNderViti"> id e ndermarje vitit</param>
        ///// <param name="idnderm"> id e ndermarjes</param>
        ///// <param name="idndermarje">id e ndermarjes</param>
        ///// <returns> nje objekt clsMesazh qe tregon nese ruajtja eshte kryer ne rregull apo jo.</returns>
        ///// </summary>        
        //internal bool ruajNivCmimi(out int idNivelCmimi, string kodNivelCmimi, string pershkrimNivelCmimi, int idPrindi, int llojiNivelCmimi, int idMonedha, int brutoNetoNivelCmimi, int prioritetiNivelCmimi, int idPerdoruesi, int idnderm, int idkonfig, int idstatusdok, bool njesiTeVarura, bool teVaruraNgaMonedha, bool nivelcmimbaze)
        //{ //metoda per ruajtjen e nivelit te cmimeve
        //    idNivelCmimi = -1;
        //    bool ruaj = false;
        //    if (dbManager == null)
        //    {
        //        dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        //        dbManager.ConnectionString = dbManager.GetConnectionString();
        //    }

        //    try
        //    {
        //        dbManager.Open();

        //        //shtimi i parametrave
        //        dbManager.CreateParameters(15);
        //        dbManager.AddParameters(0, "@IDNIVELCMIMI", idNivelCmimi, ParameterDirection.Output);
        //        dbManager.AddParameters(1, "@KODNIVELCMIMI", kodNivelCmimi, ParameterDirection.Input);
        //        dbManager.AddParameters(2, "@PERSHKRIMNIVELCMIMI", pershkrimNivelCmimi, ParameterDirection.Input);
        //        if(idPrindi==0)   dbManager.AddParameters(3, "@IDPRINDI", DBNull.Value, ParameterDirection.Input);
        //        else  dbManager.AddParameters(3, "@IDPRINDI", idPrindi, ParameterDirection.Input);
        //        dbManager.AddParameters(4, "@LLOJINIVELCMIMI", llojiNivelCmimi, ParameterDirection.Input);
        //        dbManager.AddParameters(5, "@IDMONEDHA", idMonedha, ParameterDirection.Input);
        //        dbManager.AddParameters(6, "@BRUTONETONIVELCMIMI", brutoNetoNivelCmimi, ParameterDirection.Input);
        //        dbManager.AddParameters(7, "@PRIORITETINIVELCMIMI", prioritetiNivelCmimi, ParameterDirection.Input);
        //        dbManager.AddParameters(8, "@IDPERDORUESI", idPerdoruesi, ParameterDirection.Input);
        //        //dbManager.AddParameters(9, "@IDNDERVITI", idNderViti, ParameterDirection.Input);
        //        dbManager.AddParameters(9, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
        //        dbManager.AddParameters(10, "@IDKONFIG", idkonfig, ParameterDirection.Input);
        //        dbManager.AddParameters(11, "@IDSTATUSDOK", idstatusdok, ParameterDirection.Input);
        //        dbManager.AddParameters(12, "@NJESITEVARURA", njesiTeVarura, ParameterDirection.Input);
        //        dbManager.AddParameters(13, "@TEVARURANGAMONEDHA", teVaruraNgaMonedha, ParameterDirection.Input);
        //        dbManager.AddParameters(14, "@NIVELCMIMIBAZE", nivelcmimbaze, ParameterDirection.Input);
        //        dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_NIVELCMIMI_ins");
        //        idNivelCmimi = int.Parse(dbManager.Parameters[0].Value.ToString());
        //        ruaj = true;
        //        return ruaj;
        //    }
        //    catch (Exception)
        //    {
        //        return false;
        //    }
        //}
        internal bool kontrollDetajimNjejteNdermarrje(int id, int detajimi, int idNdermarje)
        {
            dbManager.Open();
            //shtimi i parametrave
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@IdNiveli", id, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idNdermarje, ParameterDirection.Input);
            dbManager.AddParameters(2, "@Detajimi", detajimi, ParameterDirection.Input);
            return !Convert.ToBoolean(Convert.ToInt32(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_NIVELCMIMI_merrNumerNiveleshPaKeteDetajim")));

        }

        internal int merrDetajimCmimeshNdermarrje(int idNdermarje)
        {
            dbManager.Open();
            //shtimi i parametrave
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDNDERMARJE", idNdermarje, ParameterDirection.Input);
            var result = dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_NIVELCMIMI_merrDetajimCmimeshNdermarrje");
            if (result == null)
                return 0;
            return Convert.ToInt32(result);

        }

        /// <summary>
        /// ekzekuton prc_T_NIVELCMIMI_ins ne DB dhe kthen statusin e perfundimit te ekzekutimit te SP-se perkatese
        /// procedura krijon edhe nr tjeter automatik nqs niveli i cmimit eshte i lidhur me nr automatik
        /// Shiko clsMesazh brenda ketij moduli per informacion te metejshem mbi vleren e kthimit te kesaj metode
        /// <param name="idNivelCmimi">id ritese e nivelit te cmimit</param>
        /// <param name="kodNivelCmimi">kodi i nivelit te cmimit</param>
        /// <param name="pershkrimNivelCmimi">pershkrimi i nivelit te cmimit</param>
        /// <param name="idPrindi"> id e prindit </param>
        /// <param name="llojiNivelCmimi"> lloji i nivelit te cmimit</param>
        /// <param name="idMonedha">id e monedhes</param>
        /// <param name="brutoNetoNivelCmimi"> bruto/Neto </param>
        /// <param name="prioritetiNivelCmimi"> prioriteti</param>
        /// <param name="idPerdoruesi"> id e perdoruesit qe ka kryer veprimin</param>
        /// <param name="idNderViti"> id e ndermarje vitit</param>
        /// <param name="idnderm"> id e ndermarjes</param>
        /// <param name="idndermarje">id e ndermarjes</param>
        /// <returns> nje objekt clsMesazh qe tregon nese ruajtja eshte kryer ne rregull apo jo.</returns>
        /// </summary>

        internal clsMesazh ruajNivelCmimi(out int idNivelCmimi, string kodNivelCmimi, string pershkrimNivelCmimi, int idPrindi, int llojiNivelCmimi, int idMonedha, int brutoNetoNivelCmimi, int prioritetiNivelCmimi, int idPerdoruesi, int idnderm, int idkonfig, int idstatusdok, bool njesiTeVarura, bool teVaruraNgaMonedha, bool nivelcmimbaze, int detajim, int idCmimRetail)
        { //metoda per ruajtjen e nivelit te cmimeve
            idNivelCmimi = -1;

            dbManager.Open();
            //shtimi i parametrave
            dbManager.CreateParameters(17);
            dbManager.AddParameters(0, "@IDNIVELCMIMI", idNivelCmimi, ParameterDirection.Output);
            dbManager.AddParameters(1, "@KODNIVELCMIMI", kodNivelCmimi, ParameterDirection.Input);
            dbManager.AddParameters(2, "@PERSHKRIMNIVELCMIMI", pershkrimNivelCmimi, ParameterDirection.Input);
            if (idPrindi == 0) dbManager.AddParameters(3, "@IDPRINDI", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(3, "@IDPRINDI", idPrindi, ParameterDirection.Input);
            dbManager.AddParameters(4, "@LLOJINIVELCMIMI", llojiNivelCmimi, ParameterDirection.Input);
            dbManager.AddParameters(5, "@IDMONEDHA", idMonedha, ParameterDirection.Input);
            dbManager.AddParameters(6, "@BRUTONETONIVELCMIMI", brutoNetoNivelCmimi, ParameterDirection.Input);
            dbManager.AddParameters(7, "@PRIORITETINIVELCMIMI", prioritetiNivelCmimi, ParameterDirection.Input);
            dbManager.AddParameters(8, "@IDPERDORUESI", idPerdoruesi, ParameterDirection.Input);
            dbManager.AddParameters(9, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            dbManager.AddParameters(10, "@IDKONFIG", idkonfig, ParameterDirection.Input);
            dbManager.AddParameters(11, "@IDSTATUSDOK", idstatusdok, ParameterDirection.Input);
            dbManager.AddParameters(12, "@NJESITEVARURA", njesiTeVarura, ParameterDirection.Input);
            dbManager.AddParameters(13, "@TEVARURANGAMONEDHA", teVaruraNgaMonedha, ParameterDirection.Input);
            dbManager.AddParameters(14, "@NIVELCMIMIBAZE", nivelcmimbaze, ParameterDirection.Input);
            dbManager.AddParameters(15, "@Detajim", detajim, ParameterDirection.Input);
            dbManager.AddParameters(16, "@IDCMIMRETAIL", idCmimRetail, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_NIVELCMIMI_ins");
            idNivelCmimi = int.Parse(dbManager.Parameters[0].Value.ToString());
            return new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);

        }
        //[Obsolete("Perdor: clsMesazh ruajNivCmimi(int idNivelCmimi, string kodNivelCmimi, string pershkrimNivelCmimi, int idPrindi, int llojiNivelCmimi, int idMonedha, int brutoNetoNivelCmimi, int prioritetiNivelCmimi, int idPerdoruesi, int idNderViti, int idnderm, int idndermarje)", true)]
        //internal clsMesazh ruajNivelCmimi(int idNivelCmimi, string kodNivelCmimi, string pershkrimNivelCmimi, int idPrindi, int llojiNivelCmimi, int idMonedha, int brutoNetoNivelCmimi, int prioritetiNivelCmimi, int idPerdoruesi, int idNderViti, int idnderm, int idndermarje)
        //{ //metoda per ruajtjen e nivelit te cmimeve
        //    bool ruaj = false;
        //    clsMesazh mesazh = new clsMesazh();
        //    if (dbManager == null)
        //    {
        //        dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        //        dbManager.ConnectionString = dbManager.GetConnectionString();
        //    }

        //    try
        //    {
        //        dbManager.Open();

        //        //shtimi i parametrave
        //        dbManager.CreateParameters(11);
        //        dbManager.AddParameters(0, "@IDNIVELCMIMI", idNivelCmimi, ParameterDirection.Output);
        //        dbManager.AddParameters(1, "@KODNIVELCMIMI", kodNivelCmimi, ParameterDirection.Input);
        //        dbManager.AddParameters(2, "@PERSHKRIMNIVELCMIMI", pershkrimNivelCmimi, ParameterDirection.Input);
        //        dbManager.AddParameters(3, "@IDPRINDI", idPrindi, ParameterDirection.Input);
        //        dbManager.AddParameters(4, "@LLOJINIVELCMIMI", llojiNivelCmimi, ParameterDirection.Input);
        //        dbManager.AddParameters(5, "@IDMONEDHA", idMonedha, ParameterDirection.Input);
        //        dbManager.AddParameters(6, "@BRUTONETONIVELCMIMI", brutoNetoNivelCmimi, ParameterDirection.Input);
        //        dbManager.AddParameters(7, "@PRIORITETINIVELCMIMI", prioritetiNivelCmimi, ParameterDirection.Input);
        //        dbManager.AddParameters(8, "@IDPERDORUESI", idPerdoruesi, ParameterDirection.Input);
        //        dbManager.AddParameters(9, "@IDNDERVITI", idNderViti, ParameterDirection.Input);
        //        dbManager.AddParameters(10, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
        //        dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_NIVELCMIMI_ins");
        //        mesazh = new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
        //        DbAdmin.clsDatabaseAdmin dbAdmin = new DbAdmin.clsDatabaseAdmin();
        //        int idregj = DbAdmin.clsListeAmbjenteCeljeRegjistrim.ktheIdCR("CNC");
        //        //int idregj = dbAdmin.ktheListeAmbjentiCeljeRegjistrim("CNC")[0].IdCR;
        //        int idlloji = DbAdmin.clsLlojKodi.ktheIDLlojKodi("Kod");
        //        //int idlloji = dbAdmin.ktheLlojKodi("Kod")[0].IdLlojKodi;
        //        if (new DbAdmin.clsACRNumraAutomatike(idregj, idlloji, idndermarje).IdLidhjeNrAuto!=0)
        //        {
        //            DbAdmin.clsACRNumraAutomatike ACR = new DbAdmin.clsACRNumraAutomatike(idregj, idlloji, idndermarje);
        //            DbAdmin.clsNrAutom NrAutom = new clsNrAutom(ACR.IdNumraAutoLidhje);
        //            //DbAdmin.clsNrAutom NrAutom = dbAdmin.ktheNrAutom(ACR.IdNumraAutoLidhje)[0];
        //            int karakteremajtas = NrAutom.MajtasNrAutom.Length;
        //            int karakteredjathtas = ACR.VleraFunditLidhje.Length - NrAutom.DjathtasNrAutom.Length - karakteremajtas;
        //            string vle = ACR.VleraFunditLidhje.Substring(karakteremajtas, karakteredjathtas);
        //            string vlera = NrAutom.gjeneroNumrinAutomatikPasardhes(vle);
        //            ACR.VleraFunditLidhje = vlera;
        //            ACR.modifiko();
        //        }
        //        ruaj = true;

        //    }
        //    catch (Exception)
        //    {
        //        return new clsMesazh(false, ce.Message);
        //    }
        //    if (ruaj == true)
        //    {
        //        if (idPrindi != 0)
        //        {
        //            colNiveleCmimesh colNivele = new colNiveleCmimesh();
        //            colNivele.mbushNivelSipasPrindit(idPrindi);
        //            //colNiveleCmimesh colNivele = merrNivelCmimiSipasPrindit(nivelCmimi.IdPrindi);
        //            clsNivelCmimi nivelPrindi = new clsNivelCmimi();
        //            nivelPrindi.IdNivelCmimi = idPrindi;
        //            colNivele.Add(new clsNivelCmimi(nivelPrindi.IdNivelCmimi));
        //            //colNivele.Add(merrNivelCmimi(nivelPrindi)[0]);
        //            if (colNivele.Count > 0)
        //                if (prioritetiNivelCmimi <= colNivele[0].PrioritetiNivelCmimi)
        //                {
        //                    foreach (clsNivelCmimi n in colNivele)
        //                        if (n.PrioritetiNivelCmimi >= prioritetiNivelCmimi && n.KodNivelCmimi != kodNivelCmimi)
        //                        {
        //                            n.PrioritetiNivelCmimi = n.PrioritetiNivelCmimi + 1;
        //                            modifikoNivelCmimi(n.IdNivelCmimi, n.KodNivelCmimi, n.PershkrimNivelCmimi, n.IdPrindi, n.LlojiNivelCmimi, n.IdMonedha, n.BrutoNetoNivelCmimi, n.PrioritetiNivelCmimi, n.IdPerdoruesi, n.IdNderViti, n.IdNdermarje);
        //                            //modifikoNivelCmimi(n);
        //                        }

        //                }
        //        }
        //    }
        //    return mesazh;
        //}
        //[Obsolete("Perdor: clsMesazh ruajNivelCmimi(int idNivelCmimi, string kodNivelCmimi, string pershkrimNivelCmimi, int idPrindi, int llojiNivelCmimi, int idMonedha, int brutoNetoNivelCmimi, int prioritetiNivelCmimi, int idPerdoruesi, int idNderViti, int idnderm, int idndermarje)", true)]
        //public clsMesazh ruajNivelCmimi(clsNivelCmimi  nivelCmimi, int idndermarje)
        //{ //metoda per ruajtjen e nivelit te cmimeve
        //    bool ruaj = false;
        //    clsMesazh mesazh = new clsMesazh();
        //    IDBManager dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        //    dbManager.ConnectionString = dbManager.GetConnectionString();
        //    try
        //    {
        //        dbManager.Open();

        //        //shtimi i parametrave
        //        dbManager.CreateParameters(11);
        //        dbManager.AddParameters(0, "@IDNIVELCMIMI", nivelCmimi.IdNivelCmimi, ParameterDirection.Output);
        //        dbManager.AddParameters(1, "@KODNIVELCMIMI", nivelCmimi.KodNivelCmimi, ParameterDirection.Input);
        //        dbManager.AddParameters(2, "@PERSHKRIMNIVELCMIMI", nivelCmimi.PershkrimNivelCmimi, ParameterDirection.Input);
        //        dbManager.AddParameters(3, "@IDPRINDI", nivelCmimi.IdPrindi, ParameterDirection.Input);
        //        dbManager.AddParameters(4, "@LLOJINIVELCMIMI", nivelCmimi.LlojiNivelCmimi, ParameterDirection.Input);
        //        dbManager.AddParameters(5, "@IDMONEDHA", nivelCmimi.IdMonedha, ParameterDirection.Input);
        //        dbManager.AddParameters(6, "@BRUTONETONIVELCMIMI", nivelCmimi.BrutoNetoNivelCmimi, ParameterDirection.Input);
        //        dbManager.AddParameters(7, "@PRIORITETINIVELCMIMI", nivelCmimi.PrioritetiNivelCmimi, ParameterDirection.Input);
        //        dbManager.AddParameters(8, "@IDPERDORUESI", nivelCmimi.IdPerdoruesi, ParameterDirection.Input);
        //        dbManager.AddParameters(9, "@IDNDERVITI", nivelCmimi.IdNderViti, ParameterDirection.Input);
        //        dbManager.AddParameters(10, "@IDNDERMARJE", nivelCmimi.IdNdermarje, ParameterDirection.Input);
        //        dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_NIVELCMIMI_ins");
        //            mesazh = new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
        //        DbAdmin.clsDatabaseAdmin dbAdmin = new DbAdmin.clsDatabaseAdmin();
        //        int idregj = DbAdmin.clsListeAmbjenteCeljeRegjistrim.ktheIdCR("CNC");
        //        //int idregj = dbAdmin.ktheListeAmbjentiCeljeRegjistrim("CNC")[0].IdCR;
        //        int idlloji = DbAdmin.clsLlojKodi.ktheIDLlojKodi("Kod");
        //        //int idlloji = dbAdmin.ktheLlojKodi("Kod")[0].IdLlojKodi;
        //        if (new DbAdmin.clsACRNumraAutomatike(idregj, idlloji, idndermarje ).IdLidhjeNrAuto!=0)
        //        {
        //            DbAdmin.clsACRNumraAutomatike ACR = new DbAdmin.clsACRNumraAutomatike(idregj, idlloji, idndermarje);
        //            DbAdmin.clsNrAutom NrAutom = new clsNrAutom(ACR.IdNumraAutoLidhje);
        //            //DbAdmin.clsNrAutom NrAutom = dbAdmin.ktheNrAutom(ACR.IdNumraAutoLidhje)[0];
        //            int karakteremajtas = NrAutom.MajtasNrAutom.Length;
        //            int karakteredjathtas = ACR.VleraFunditLidhje.Length - NrAutom.DjathtasNrAutom.Length - karakteremajtas;
        //            string vle = ACR.VleraFunditLidhje.Substring(karakteremajtas, karakteredjathtas);
        //            string vlera = NrAutom.gjeneroNumrinAutomatikPasardhes(vle);
        //            ACR.VleraFunditLidhje = vlera;
        //            ACR.modifiko();
        //        }
        //        ruaj = true;

        //    }
        //    catch (Exception)
        //    {
        //        return new clsMesazh(false, ce.Message);
        //    }
        //    finally
        //    {
        //        dbManager.Dispose();
        //    }
        //    if (ruaj == true)
        //    {
        //        if (nivelCmimi.IdPrindi != 0)
        //        {
        //            colNiveleCmimesh colNivele = new colNiveleCmimesh();
        //            colNivele.mbushNivelSipasPrindit(nivelCmimi.IdPrindi);
        //            //colNiveleCmimesh colNivele = merrNivelCmimiSipasPrindit(nivelCmimi.IdPrindi);
        //            clsNivelCmimi nivelPrindi = new clsNivelCmimi();
        //            nivelPrindi.IdNivelCmimi = nivelCmimi.IdPrindi;
        //            colNivele.Add(new clsNivelCmimi(nivelPrindi.IdNivelCmimi));
        //            //colNivele.Add(merrNivelCmimi(nivelPrindi)[0]);
        //            if (colNivele.Count > 0)
        //            if (nivelCmimi.PrioritetiNivelCmimi <= colNivele[0].PrioritetiNivelCmimi)
        //            {
        //                foreach (clsNivelCmimi n in colNivele)
        //                    if (n.PrioritetiNivelCmimi >= nivelCmimi.PrioritetiNivelCmimi && n.KodNivelCmimi  !=nivelCmimi.KodNivelCmimi  )
        //                    {
        //                        n.PrioritetiNivelCmimi = n.PrioritetiNivelCmimi + 1;
        //                        modifikoNivelCmimi(n.IdNivelCmimi, n.KodNivelCmimi, n.PershkrimNivelCmimi, n.IdPrindi, n.LlojiNivelCmimi, n.IdMonedha, n.BrutoNetoNivelCmimi, n.PrioritetiNivelCmimi, n.IdPerdoruesi, n.IdNderViti, n.IdNdermarje);
        //                        //modifikoNivelCmimi(n);
        //                    }

        //            }
        //        }
        //    }
        //    return mesazh;
        //}

        ///// <summary>
        ///// ekzekuton prc_T_NIVELCMIMI_upd ne DB dhe kthen statusin e perfundimit te ekzekutimit te SP-se perkatese
        ///// Shiko clsMesazh brenda ketij moduli per informacion te metejshem mbi vleren e kthimit te kesaj metode
        ///// <param name="idNivelCmimi">id ritese e nivelit te cmimit</param>
        ///// <param name="kodNivelCmimi">kodi i nivelit te cmimit</param>
        ///// <param name="pershkrimNivelCmimi">pershkrimi i nivelit te cmimit</param>
        ///// <param name="idPrindi"> id e prindit </param>
        ///// <param name="llojiNivelCmimi"> lloji i nivelit te cmimit</param>
        ///// <param name="idMonedha">id e monedhes</param>
        ///// <param name="brutoNetoNivelCmimi"> bruto/Neto </param>
        ///// <param name="prioritetiNivelCmimi"> prioriteti</param>
        ///// <param name="idPerdoruesi"> id e perdoruesit qe ka kryer veprimin</param>
        ///// <param name="idNderViti"> id e ndermarje vitit</param>
        ///// <param name="idnderm"> id e ndermarjes</param>
        ///// <returns> nje objekt clsMesazh qe tregon nese modifikimi eshte kryer ne rregull apo jo.</returns>
        ///// </summary>
        //internal bool modifikoNivCmimi(int idNivelCmimi, string kodNivelCmimi, string pershkrimNivelCmimi, int idPrindi, int llojiNivelCmimi, int idMonedha, int brutoNetoNivelCmimi, int prioritetiNivelCmimi, int idPerdoruesi, int idnderm, int idkonfig, int idstatusdok, bool njesiTeVarura, bool teVaruraNgaMonedha, bool nivelcmimbaze)
        //{
        //    bool ruaj = false;
        //    if (dbManager == null)
        //    {
        //        dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        //        dbManager.ConnectionString = dbManager.GetConnectionString();
        //    }
        //    try
        //    {
        //        dbManager.Open();
        //        //shtimi i parametrave
        //        dbManager.CreateParameters(15);
        //        dbManager.AddParameters(0, "@IDNIVELCMIMI", idNivelCmimi, ParameterDirection.Input);
        //        dbManager.AddParameters(1, "@KODNIVELCMIMI", kodNivelCmimi, ParameterDirection.Input);
        //        dbManager.AddParameters(2, "@PERSHKRIMNIVELCMIMI", pershkrimNivelCmimi, ParameterDirection.Input);
        //        if (idPrindi == 0) dbManager.AddParameters(3, "@IDPRINDI", DBNull.Value, ParameterDirection.Input);
        //        else dbManager.AddParameters(3, "@IDPRINDI", idPrindi, ParameterDirection.Input);
        //        dbManager.AddParameters(4, "@LLOJINIVELCMIMI", llojiNivelCmimi, ParameterDirection.Input);
        //        dbManager.AddParameters(5, "@IDMONEDHA", idMonedha, ParameterDirection.Input);
        //        dbManager.AddParameters(6, "@BRUTONETONIVELCMIMI", brutoNetoNivelCmimi, ParameterDirection.Input);
        //        dbManager.AddParameters(7, "@PRIORITETINIVELCMIMI", prioritetiNivelCmimi, ParameterDirection.Input);
        //        dbManager.AddParameters(8, "@IDPERDORUESI", idPerdoruesi, ParameterDirection.Input);
        //        //dbManager.AddParameters(9, "@IDNDERVITI", idNderViti, ParameterDirection.Input);
        //        dbManager.AddParameters(9, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
        //        dbManager.AddParameters(10, "@IDKONFIG", idkonfig, ParameterDirection.Input);
        //        dbManager.AddParameters(11, "@IDSTATUSDOK", idstatusdok, ParameterDirection.Input);
        //        dbManager.AddParameters(12, "@NJESITEVARURA", njesiTeVarura, ParameterDirection.Input);
        //        dbManager.AddParameters(13, "@TEVARURANGAMONEDHA", teVaruraNgaMonedha, ParameterDirection.Input);
        //        dbManager.AddParameters(14, "@NIVELCMIMIBAZE", nivelcmimbaze, ParameterDirection.Input);
        //        dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_NIVELCMIMI_upd");
        //        clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
        //        ruaj = true;
        //        return ruaj;
        //    }
        //    catch (Exception)
        //    {
        //        return false;
        //    }
        //}

        /// <summary>
        /// ekzekuton prc_T_NIVELCMIMI_upd ne DB dhe kthen statusin e perfundimit te ekzekutimit te SP-se perkatese
        /// Shiko clsMesazh brenda ketij moduli per informacion te metejshem mbi vleren e kthimit te kesaj metode
        /// <param name="idNivelCmimi">id ritese e nivelit te cmimit</param>
        /// <param name="kodNivelCmimi">kodi i nivelit te cmimit</param>
        /// <param name="pershkrimNivelCmimi">pershkrimi i nivelit te cmimit</param>
        /// <param name="idPrindi"> id e prindit </param>
        /// <param name="llojiNivelCmimi"> lloji i nivelit te cmimit</param>
        /// <param name="idMonedha">id e monedhes</param>
        /// <param name="brutoNetoNivelCmimi"> bruto/Neto </param>
        /// <param name="prioritetiNivelCmimi"> prioriteti</param>
        /// <param name="idPerdoruesi"> id e perdoruesit qe ka kryer veprimin</param>
        /// <param name="idNderViti"> id e ndermarje vitit</param>
        /// <param name="idnderm"> id e ndermarjes</param>
        /// <returns> nje objekt clsMesazh qe tregon nese modifikimi eshte kryer ne rregull apo jo.</returns>
        /// </summary>

        internal clsMesazh modifikoNivCmimi(int idNivelCmimi, string kodNivelCmimi, string pershkrimNivelCmimi, int idPrindi, int llojiNivelCmimi, int idMonedha, int brutoNetoNivelCmimi, int prioritetiNivelCmimi, int idPerdoruesi, int idnderm, int idkonfig, int idstatusdok, bool njesiTeVarura, bool teVaruraNgaMonedha, bool nivelcmimbaze, int detajim, int idCmimRetail)
        {

            dbManager.Open();
            //shtimi i parametrave
            dbManager.CreateParameters(17);
            dbManager.AddParameters(0, "@IDNIVELCMIMI", idNivelCmimi, ParameterDirection.Input);
            dbManager.AddParameters(1, "@KODNIVELCMIMI", kodNivelCmimi, ParameterDirection.Input);
            dbManager.AddParameters(2, "@PERSHKRIMNIVELCMIMI", pershkrimNivelCmimi, ParameterDirection.Input);
            if (idPrindi == 0) dbManager.AddParameters(3, "@IDPRINDI", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(3, "@IDPRINDI", idPrindi, ParameterDirection.Input);
            dbManager.AddParameters(4, "@LLOJINIVELCMIMI", llojiNivelCmimi, ParameterDirection.Input);
            dbManager.AddParameters(5, "@IDMONEDHA", idMonedha, ParameterDirection.Input);
            dbManager.AddParameters(6, "@BRUTONETONIVELCMIMI", brutoNetoNivelCmimi, ParameterDirection.Input);
            dbManager.AddParameters(7, "@PRIORITETINIVELCMIMI", prioritetiNivelCmimi, ParameterDirection.Input);
            dbManager.AddParameters(8, "@IDPERDORUESI", idPerdoruesi, ParameterDirection.Input);
            dbManager.AddParameters(9, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            dbManager.AddParameters(10, "@IDKONFIG", idkonfig, ParameterDirection.Input);
            dbManager.AddParameters(11, "@IDSTATUSDOK", idstatusdok, ParameterDirection.Input);
            dbManager.AddParameters(12, "@NJESITEVARURA", njesiTeVarura, ParameterDirection.Input);
            dbManager.AddParameters(13, "@TEVARURANGAMONEDHA", teVaruraNgaMonedha, ParameterDirection.Input);
            dbManager.AddParameters(14, "@NIVELCMIMIBAZE", nivelcmimbaze, ParameterDirection.Input);

            dbManager.AddParameters(15, "@Detajim", detajim, ParameterDirection.Input);

            if (idCmimRetail == 0) dbManager.AddParameters(16, "@IDCMIMRETAIL", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(16, "@IDCMIMRETAIL", idCmimRetail, ParameterDirection.Input);


            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_NIVELCMIMI_upd");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
            return mesazh;

        }
        //[Obsolete("Perdor: bool modifikoNivCmimi(int idNivelCmimi, string kodNivelCmimi, string pershkrimNivelCmimi, int idPrindi, int llojiNivelCmimi, int idMonedha, int brutoNetoNivelCmimi, int prioritetiNivelCmimi, int idPerdoruesi, int idNderViti, int idnderm, int idndermarje)", true)]
        //public bool modifikoNivCmimi(clsNivelCmimi nivelCmimi)
        //{
        //    bool ruaj = false;
        //    IDBManager dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        //    dbManager.ConnectionString = dbManager.GetConnectionString();
        //    try
        //    {
        //        dbManager.Open();

        //        //shtimi i parametrave
        //        dbManager.CreateParameters(11);
        //        dbManager.AddParameters(0, "@IDNIVELCMIMI", nivelCmimi.IdNivelCmimi, ParameterDirection.Input);
        //        dbManager.AddParameters(1, "@KODNIVELCMIMI", nivelCmimi.KodNivelCmimi, ParameterDirection.Input);
        //        dbManager.AddParameters(2, "@PERSHKRIMNIVELCMIMI", nivelCmimi.PershkrimNivelCmimi, ParameterDirection.Input);
        //        dbManager.AddParameters(3, "@IDPRINDI", nivelCmimi.IdPrindi, ParameterDirection.Input);
        //        dbManager.AddParameters(4, "@LLOJINIVELCMIMI", nivelCmimi.LlojiNivelCmimi, ParameterDirection.Input);
        //        dbManager.AddParameters(5, "@IDMONEDHA", nivelCmimi.IdMonedha, ParameterDirection.Input);
        //        dbManager.AddParameters(6, "@BRUTONETONIVELCMIMI", nivelCmimi.BrutoNetoNivelCmimi, ParameterDirection.Input);
        //        dbManager.AddParameters(7, "@PRIORITETINIVELCMIMI", nivelCmimi.PrioritetiNivelCmimi, ParameterDirection.Input);
        //        dbManager.AddParameters(8, "@IDPERDORUESI", nivelCmimi.IdPerdoruesi, ParameterDirection.Input);
        //        dbManager.AddParameters(9, "@IDNDERVITI", nivelCmimi.IdNderViti, ParameterDirection.Input);
        //        dbManager.AddParameters(10, "@IDNDERMARJE", nivelCmimi.IdNdermarje, ParameterDirection.Input);
        //        dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_NIVELCMIMI_upd");
        //        clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
        //        ruaj = true;
        //        return ruaj;
        //    }
        //    catch (Exception)
        //    {
        //        return false;
        //    }
        //    finally
        //    {
        //        dbManager.Dispose();
        //    }
        //}

        //    /// <summary>
        //    /// modifikon nje nivel cmimi duke ndryshuar prioritetet e te gjithe nivele te tjera te te njejtit prind ne varesi te ndryshimit te nivelit qe u modifikua
        //    /// </summary>
        //    /// <param name="idNivelCmimi">id ritese e nivelit te cmimit</param>
        //    /// <param name="kodNivelCmimi">kodi i nivelit te cmimit</param>
        //    /// <param name="pershkrimNivelCmimi">pershkrimi i nivelit te cmimit</param>
        //    /// <param name="idPrindi"> id e prindit </param>
        //    /// <param name="llojiNivelCmimi"> lloji i nivelit te cmimit</param>
        //    /// <param name="idMonedha">id e monedhes</param>
        //    /// <param name="brutoNetoNivelCmimi"> bruto/Neto </param>
        //    /// <param name="prioritetiNivelCmimi"> prioriteti</param>
        //    /// <param name="idPerdoruesi"> id e perdoruesit qe ka kryer veprimin</param>
        //    /// <param name="idNderViti"> id e ndermarje vitit</param>
        //    /// <param name="idnderm"> id e ndermarjes</param>
        //    /// <returns> kthen nje obj clsMesazh per te identifikuar statusin e modifikimit se te dhenave ne DB</returns>
        //    [Obsolete("Perdor nga klasa perkatese: clsMesazh modifikoNivelCmimi(int idNivelCmimi, string kodNivelCmimi, string pershkrimNivelCmimi, int idPrindi, int llojiNivelCmimi, int idMonedha, int brutoNetoNivelCmimi, int prioritetiNivelCmimi, int idPerdoruesi, int idNderViti, int idnderm)", true)]
        //    internal clsMesazh modifikoNivelCmimi(int idNivelCmimi, string kodNivelCmimi, string pershkrimNivelCmimi, int idPrindi, int llojiNivelCmimi, int idMonedha, int brutoNetoNivelCmimi, int prioritetiNivelCmimi, int idPerdoruesi, int idNderViti, int idnderm)
        //    {//metoda per modifikimin e nivelit te cmimit
        //        bool ruaj = false;
        //        clsMesazh mesazh = new clsMesazh();
        //        clsNivelCmimi nivelipara = new clsNivelCmimi(idNivelCmimi);
        //        //clsNivelCmimi nivelipara = merrNivelCmimi(nivelCmimi)[0];
        //        ruaj = modifikoNivCmimi(idNivelCmimi, kodNivelCmimi, pershkrimNivelCmimi, idPrindi, llojiNivelCmimi, idMonedha, brutoNetoNivelCmimi, prioritetiNivelCmimi, idPerdoruesi, idNderViti, idnderm,0);
        //        if (ruaj == true)
        //        {
        //            mesazh = new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
        //            if (idPrindi == 0)
        //            {
        //                colNiveleCmimesh nivelet = new colNiveleCmimesh();
        //                nivelet.mbushNivelSipasPrindit(idNivelCmimi);
        //                //colNiveleCmimesh nivelet = merrNivelCmimiSipasPrindit(nivelCmimi.IdNivelCmimi);
        //                if (nivelet.Count > 0)
        //                    if (nivelet[0].LlojiNivelCmimi != llojiNivelCmimi)
        //                    {
        //                        foreach (clsNivelCmimi c in nivelet)
        //                        {
        //                            c.LlojiNivelCmimi = llojiNivelCmimi;
        //                            modifikoNivCmimi(c.IdNivelCmimi, c.KodNivelCmimi, c.PershkrimNivelCmimi, c.IdPrindi, c.LlojiNivelCmimi, c.IdMonedha, c.BrutoNetoNivelCmimi, c.PrioritetiNivelCmimi, c.IdPerdoruesi, c.IdNderViti, c.IdNdermarje,0);
        //                            //modifikoNivCmimi(c);
        //                        }
        //                    }
        //            }
        //            if (idPrindi != 0)
        //            {
        //                if (nivelipara.IdPrindi == idPrindi)
        //                {
        //                    if (nivelipara.PrioritetiNivelCmimi != prioritetiNivelCmimi)
        //                    {
        //                        colNiveleCmimesh colNivele = new colNiveleCmimesh();
        //                        colNivele.mbushNivelSipasPrindit(idPrindi);
        //                        //colNiveleCmimesh colNivele = merrNivelCmimiSipasPrindit(nivelCmimi.IdPrindi);
        //                        clsNivelCmimi nivelPrindi = new clsNivelCmimi();
        //                        nivelPrindi.IdNivelCmimi = idPrindi;
        //                        colNivele.Add(new clsNivelCmimi(nivelPrindi.IdNivelCmimi));
        //                        //colNivele.Add(merrNivelCmimi(nivelPrindi)[0]);
        //                        if (prioritetiNivelCmimi < nivelipara.PrioritetiNivelCmimi)
        //                        {
        //                            foreach (clsNivelCmimi n in colNivele)
        //                                if (n.PrioritetiNivelCmimi >= prioritetiNivelCmimi && n.PrioritetiNivelCmimi < nivelipara.PrioritetiNivelCmimi && n.KodNivelCmimi != kodNivelCmimi)
        //                                {
        //                                    n.PrioritetiNivelCmimi = n.PrioritetiNivelCmimi + 1;
        //                                    modifikoNivCmimi(n.IdNivelCmimi, n.KodNivelCmimi, n.PershkrimNivelCmimi, n.IdPrindi, n.LlojiNivelCmimi, n.IdMonedha, n.BrutoNetoNivelCmimi, n.PrioritetiNivelCmimi, n.IdPerdoruesi, n.IdNderViti, n.IdNdermarje,0);
        //                                    //modifikoNivCmimi(n);
        //                                }

        //                        }
        //                        else
        //                        {
        //                            foreach (clsNivelCmimi n in colNivele)
        //                                if (n.PrioritetiNivelCmimi <= prioritetiNivelCmimi && n.PrioritetiNivelCmimi > nivelipara.PrioritetiNivelCmimi && n.KodNivelCmimi != kodNivelCmimi)
        //                                {
        //                                    n.PrioritetiNivelCmimi = n.PrioritetiNivelCmimi - 1;
        //                                    modifikoNivCmimi(n.IdNivelCmimi, n.KodNivelCmimi, n.PershkrimNivelCmimi, n.IdPrindi, n.LlojiNivelCmimi, n.IdMonedha, n.BrutoNetoNivelCmimi, n.PrioritetiNivelCmimi, n.IdPerdoruesi, n.IdNderViti, n.IdNdermarje,0);
        //                                    //modifikoNivCmimi(n);
        //                                }
        //                        }

        //                    }
        //                }
        //                else
        //                {
        //                    colNiveleCmimesh colNivele = new colNiveleCmimesh();
        //                    colNivele.mbushNivelSipasPrindit(idPrindi);
        //                    //colNiveleCmimesh colNivele = merrNivelCmimiSipasPrindit(nivelCmimi.IdPrindi);
        //                    clsNivelCmimi nivelPrindi = new clsNivelCmimi();
        //                    nivelPrindi.IdNivelCmimi = idPrindi;
        //                    colNivele.Add(new clsNivelCmimi(nivelPrindi.IdNivelCmimi));
        //                    //colNivele.Add(merrNivelCmimi(nivelPrindi)[0]);
        //                    if (colNivele.Count > 0)
        //                        if (prioritetiNivelCmimi <= colNivele[0].PrioritetiNivelCmimi)
        //                        {
        //                            foreach (clsNivelCmimi n in colNivele)
        //                                if (n.PrioritetiNivelCmimi >= prioritetiNivelCmimi && n.KodNivelCmimi != kodNivelCmimi)
        //                                {
        //                                    n.PrioritetiNivelCmimi = n.PrioritetiNivelCmimi + 1;
        //                                    modifikoNivCmimi(n.IdNivelCmimi, n.KodNivelCmimi, n.PershkrimNivelCmimi, n.IdPrindi, n.LlojiNivelCmimi, n.IdMonedha, n.BrutoNetoNivelCmimi, n.PrioritetiNivelCmimi, n.IdPerdoruesi, n.IdNderViti, n.IdNdermarje,0);
        //                                    //modifikoNivCmimi(n);
        //                                }

        //                        }
        //                }
        //            }
        //            else
        //            {
        //                if (nivelipara.PrioritetiNivelCmimi != prioritetiNivelCmimi)
        //                {
        //                    colNiveleCmimesh colNivele = new colNiveleCmimesh();
        //                    colNivele.mbushNivelSipasPrindit(idNivelCmimi);
        //                    //colNiveleCmimesh colNivele = merrNivelCmimiSipasPrindit(nivelCmimi.IdNivelCmimi );

        //                    if (prioritetiNivelCmimi < nivelipara.PrioritetiNivelCmimi)
        //                    {
        //                        foreach (clsNivelCmimi n in colNivele)
        //                            if (n.PrioritetiNivelCmimi >= prioritetiNivelCmimi && n.PrioritetiNivelCmimi < nivelipara.PrioritetiNivelCmimi && n.KodNivelCmimi != kodNivelCmimi)
        //                            {
        //                                n.PrioritetiNivelCmimi = n.PrioritetiNivelCmimi + 1;
        //                                modifikoNivCmimi(n.IdNivelCmimi, n.KodNivelCmimi, n.PershkrimNivelCmimi, n.IdPrindi, n.LlojiNivelCmimi, n.IdMonedha, n.BrutoNetoNivelCmimi, n.PrioritetiNivelCmimi, n.IdPerdoruesi, n.IdNderViti, n.IdNdermarje,0);
        //                                //modifikoNivCmimi(n);
        //                            }

        //                    }
        //                    else
        //                    {
        //                        foreach (clsNivelCmimi n in colNivele)
        //                            if (n.PrioritetiNivelCmimi <= prioritetiNivelCmimi && n.PrioritetiNivelCmimi > nivelipara.PrioritetiNivelCmimi && n.KodNivelCmimi != kodNivelCmimi)
        //                            {
        //                                n.PrioritetiNivelCmimi = n.PrioritetiNivelCmimi - 1;
        //                                modifikoNivCmimi(n.IdNivelCmimi, n.KodNivelCmimi, n.PershkrimNivelCmimi, n.IdPrindi, n.LlojiNivelCmimi, n.IdMonedha, n.BrutoNetoNivelCmimi, n.PrioritetiNivelCmimi, n.IdPerdoruesi, n.IdNderViti, n.IdNdermarje,0);
        //                                //modifikoNivCmimi(n);
        //                            }
        //                    }

        //                }
        //            }
        //        }

        //        return mesazh;
        //    }
        //    [Obsolete("Perdor: clsMesazh modifikoNivelCmimi(int idNivelCmimi, string kodNivelCmimi, string pershkrimNivelCmimi, int idPrindi, int llojiNivelCmimi, int idMonedha, int brutoNetoNivelCmimi, int prioritetiNivelCmimi, int idPerdoruesi, int idNderViti, int idnderm)", true)]
        //    public clsMesazh modifikoNivelCmimi(clsNivelCmimi  nivelCmimi)
        //    {//metoda per modifikimin e nivelit te cmimit
        //        bool ruaj = false;
        //        clsMesazh mesazh = new clsMesazh();
        //        clsNivelCmimi nivelipara = new clsNivelCmimi(nivelCmimi.IdNivelCmimi);
        //        //clsNivelCmimi nivelipara = merrNivelCmimi(nivelCmimi)[0];
        //        ruaj = modifikoNivCmimi(nivelCmimi.IdNivelCmimi, nivelCmimi.KodNivelCmimi, nivelCmimi.PershkrimNivelCmimi, nivelCmimi.IdPrindi, nivelCmimi.LlojiNivelCmimi, nivelCmimi.IdMonedha, nivelCmimi.BrutoNetoNivelCmimi, nivelCmimi.PrioritetiNivelCmimi, nivelCmimi.IdPerdoruesi, nivelCmimi.IdNderViti, nivelCmimi.IdNdermarje,0);
        //        //ruaj = modifikoNivCmimi(nivelCmimi);
        //        if (ruaj == true)
        //        {
        //            mesazh = new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
        //            if (nivelCmimi.IdPrindi == 0)
        //            {
        //                colNiveleCmimesh nivelet = new colNiveleCmimesh();
        //                nivelet.mbushNivelSipasPrindit(nivelCmimi.IdNivelCmimi);
        //                //colNiveleCmimesh nivelet = merrNivelCmimiSipasPrindit(nivelCmimi.IdNivelCmimi);
        //                if (nivelet.Count > 0)
        //                    if (nivelet[0].LlojiNivelCmimi != nivelCmimi.LlojiNivelCmimi)
        //                    {
        //                        foreach (clsNivelCmimi c in nivelet)
        //                        {
        //                            c.LlojiNivelCmimi = nivelCmimi.LlojiNivelCmimi;
        //                            modifikoNivCmimi(c.IdNivelCmimi, c.KodNivelCmimi, c.PershkrimNivelCmimi, c.IdPrindi, c.LlojiNivelCmimi, c.IdMonedha, c.BrutoNetoNivelCmimi, c.PrioritetiNivelCmimi, c.IdPerdoruesi, c.IdNderViti, c.IdNdermarje,0);
        //                            //modifikoNivCmimi(c);
        //                        }
        //                    }
        //            }
        //            if (nivelCmimi.IdPrindi != 0)
        //            {
        //                if (nivelipara.IdPrindi == nivelCmimi.IdPrindi)
        //                {
        //                    if (nivelipara.PrioritetiNivelCmimi != nivelCmimi.PrioritetiNivelCmimi)
        //                    {
        //                        colNiveleCmimesh colNivele = new colNiveleCmimesh();
        //                        colNivele.mbushNivelSipasPrindit(nivelCmimi.IdPrindi);
        //                        //colNiveleCmimesh colNivele = merrNivelCmimiSipasPrindit(nivelCmimi.IdPrindi);
        //                        clsNivelCmimi nivelPrindi = new clsNivelCmimi();
        //                        nivelPrindi.IdNivelCmimi = nivelCmimi.IdPrindi;
        //                        colNivele.Add(new clsNivelCmimi(nivelPrindi.IdNivelCmimi));
        //                        //colNivele.Add(merrNivelCmimi(nivelPrindi)[0]);
        //                        if (nivelCmimi.PrioritetiNivelCmimi < nivelipara.PrioritetiNivelCmimi)
        //                        {
        //                            foreach (clsNivelCmimi n in colNivele)
        //                                if (n.PrioritetiNivelCmimi >= nivelCmimi.PrioritetiNivelCmimi && n.PrioritetiNivelCmimi < nivelipara.PrioritetiNivelCmimi && n.KodNivelCmimi != nivelCmimi.KodNivelCmimi)
        //                                {
        //                                    n.PrioritetiNivelCmimi = n.PrioritetiNivelCmimi + 1;
        //                                    modifikoNivCmimi(n.IdNivelCmimi, n.KodNivelCmimi, n.PershkrimNivelCmimi, n.IdPrindi, n.LlojiNivelCmimi, n.IdMonedha, n.BrutoNetoNivelCmimi, n.PrioritetiNivelCmimi, n.IdPerdoruesi, n.IdNderViti, n.IdNdermarje,0);
        //                                    //modifikoNivCmimi(n);
        //                                }

        //                        }
        //                        else
        //                        {
        //                            foreach (clsNivelCmimi n in colNivele)
        //                                if (n.PrioritetiNivelCmimi <= nivelCmimi.PrioritetiNivelCmimi && n.PrioritetiNivelCmimi > nivelipara.PrioritetiNivelCmimi && n.KodNivelCmimi != nivelCmimi.KodNivelCmimi)
        //                                {
        //                                    n.PrioritetiNivelCmimi = n.PrioritetiNivelCmimi - 1;
        //                                    modifikoNivCmimi(n.IdNivelCmimi, n.KodNivelCmimi, n.PershkrimNivelCmimi, n.IdPrindi, n.LlojiNivelCmimi, n.IdMonedha, n.BrutoNetoNivelCmimi, n.PrioritetiNivelCmimi, n.IdPerdoruesi, n.IdNderViti, n.IdNdermarje,0);
        //                                    //modifikoNivCmimi(n);
        //                                }
        //                        }

        //                    }
        //                }
        //                else
        //                {
        //                    colNiveleCmimesh colNivele = new colNiveleCmimesh();
        //                    colNivele.mbushNivelSipasPrindit(nivelCmimi.IdPrindi);
        //                    //colNiveleCmimesh colNivele = merrNivelCmimiSipasPrindit(nivelCmimi.IdPrindi);
        //                    clsNivelCmimi nivelPrindi = new clsNivelCmimi();
        //                    nivelPrindi.IdNivelCmimi = nivelCmimi.IdPrindi;
        //                    colNivele.Add(new clsNivelCmimi(nivelPrindi.IdNivelCmimi));
        //                    //colNivele.Add(merrNivelCmimi(nivelPrindi)[0]);
        //                    if (colNivele.Count > 0)
        //                        if (nivelCmimi.PrioritetiNivelCmimi <= colNivele[0].PrioritetiNivelCmimi)
        //                        {
        //                            foreach (clsNivelCmimi n in colNivele)
        //                                if (n.PrioritetiNivelCmimi >= nivelCmimi.PrioritetiNivelCmimi && n.KodNivelCmimi != nivelCmimi.KodNivelCmimi)
        //                                {
        //                                    n.PrioritetiNivelCmimi = n.PrioritetiNivelCmimi + 1;
        //                                    modifikoNivCmimi(n.IdNivelCmimi, n.KodNivelCmimi, n.PershkrimNivelCmimi, n.IdPrindi, n.LlojiNivelCmimi, n.IdMonedha, n.BrutoNetoNivelCmimi, n.PrioritetiNivelCmimi, n.IdPerdoruesi, n.IdNderViti, n.IdNdermarje,0);
        //                                    //modifikoNivCmimi(n);
        //                                }

        //                        }
        //                }
        //            }
        //            else
        //            {
        //                if (nivelipara.PrioritetiNivelCmimi != nivelCmimi.PrioritetiNivelCmimi)
        //                {
        //                    colNiveleCmimesh colNivele = new colNiveleCmimesh();
        //                    colNivele.mbushNivelSipasPrindit(nivelCmimi.IdNivelCmimi);
        //                    //colNiveleCmimesh colNivele = merrNivelCmimiSipasPrindit(nivelCmimi.IdNivelCmimi );

        //                    if (nivelCmimi.PrioritetiNivelCmimi < nivelipara.PrioritetiNivelCmimi)
        //                    {
        //                        foreach (clsNivelCmimi n in colNivele)
        //                            if (n.PrioritetiNivelCmimi >= nivelCmimi.PrioritetiNivelCmimi && n.PrioritetiNivelCmimi < nivelipara.PrioritetiNivelCmimi && n.KodNivelCmimi != nivelCmimi.KodNivelCmimi)
        //                            {
        //                                n.PrioritetiNivelCmimi = n.PrioritetiNivelCmimi + 1;
        //                                modifikoNivCmimi(n.IdNivelCmimi, n.KodNivelCmimi, n.PershkrimNivelCmimi, n.IdPrindi, n.LlojiNivelCmimi, n.IdMonedha, n.BrutoNetoNivelCmimi, n.PrioritetiNivelCmimi, n.IdPerdoruesi, n.IdNderViti, n.IdNdermarje,0);
        //                                //modifikoNivCmimi(n);
        //                            }

        //                    }
        //                    else
        //                    {
        //                        foreach (clsNivelCmimi n in colNivele)
        //                            if (n.PrioritetiNivelCmimi <= nivelCmimi.PrioritetiNivelCmimi && n.PrioritetiNivelCmimi > nivelipara.PrioritetiNivelCmimi && n.KodNivelCmimi != nivelCmimi.KodNivelCmimi)
        //                            {
        //                                n.PrioritetiNivelCmimi = n.PrioritetiNivelCmimi - 1;
        //                                modifikoNivCmimi(n.IdNivelCmimi, n.KodNivelCmimi, n.PershkrimNivelCmimi, n.IdPrindi, n.LlojiNivelCmimi, n.IdMonedha, n.BrutoNetoNivelCmimi, n.PrioritetiNivelCmimi, n.IdPerdoruesi, n.IdNderViti, n.IdNdermarje,0);
        //                                //modifikoNivCmimi(n);
        //                            }
        //                    }

        //                }
        //            }
        //        }

        //return mesazh;
        //    }

        /// <summary>
        /// ekzekutohet sp-ja prc_T_NIVELCMIMI_del duke i kaluar id e nivelit te cmimit qe e marrim nga objekti clsNivelCmimi qe i kalohet si parameter
        /// </summary>
        /// <param name="idNivelCmimi"> niveli i cmimit  qe do fshihet </param>
        /// <returns>nje objekt clsMesazh qe tregon nese fshirja eshte kryer ne rregull apo jo</returns>
        internal clsMesazh fshiNivelCmimi(int idNivelCmimi)
        {

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDNIVELCMIMI", idNivelCmimi, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_NIVELCMIMI_del");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
            return mesazh;

        }
        internal clsMesazh fshiNivelCmimiStatus(int idNivelCmimi, int idperdoruesi)
        {

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDNIVELCMIMI", idNivelCmimi, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUESI", idperdoruesi, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_NIVELCMIMI_upddel");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
            return mesazh;

        }
        //[Obsolete("Perdor: clsMesazh fshiNivelCmimi(int idNivelCmimi)", true)]
        //public clsMesazh fshiNivelCmimi(clsNivelCmimi nivelCmimi)
        //{//metoda per fshirjen e nivelit te cmimit
        //    IDBManager dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        //    dbManager.ConnectionString = dbManager.GetConnectionString();
        //    try
        //    {
        //        dbManager.Open();
        //        dbManager.CreateParameters(1);
        //        dbManager.AddParameters(0, "@IDNIVELCMIMI", nivelCmimi.IdNivelCmimi , ParameterDirection.Input);
        //        dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_NIVELCMIMI_del");
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
        /// merr nivelet e cmimit sipas id
        /// </summary>
        /// <param name="idNivelCmimi"> niveli i cmimit qe i merret id</param>
        /// <returns> kthen datarow qe permban nivelin e cmimit me kete id</returns>
        internal DataRow ktheNivelCmimi(int idNivelCmimi)
        {

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDNIVELCMIMI", idNivelCmimi, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_NIVELCMIMI_sel");

            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];

        }

        /// <summary>
        /// merr nivelet e cmimit sipas id
        /// </summary>
        /// <param name="idNivelCmimi"> niveli i cmimit qe i merret id</param>
        /// <returns> kthen datarow qe permban nivelin e cmimit me kete id</returns>
        internal DataTable ktheNiveleCmimi(int idNivelCmimi)
        {

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDNIVELCMIMI", idNivelCmimi, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_NIVELCMIMI_sel");

            return ds.Tables[0];

        }

        //[Obsolete("Perdor: DataRow ktheNivelCmimi(int id)", true)]
        //public colNiveleCmimesh merrNivelCmimi(clsNivelCmimi nivelCmimi)
        //{// metoda per te marre nje nivelin e cmimit NE BAZE TE ID
        //    IDBManager dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        //    dbManager.ConnectionString = dbManager.GetConnectionString();
        //    try
        //    {
        //        dbManager.Open();
        //        //shtimi i parametrave
        //        dbManager.CreateParameters(1);
        //        dbManager.AddParameters(0, "@IDNIVELCMIMI", nivelCmimi.IdNivelCmimi , ParameterDirection.Input);
        //        DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_NIVELCMIMI_sel");
        //        colNiveleCmimesh colNiveleCmimesh = new colNiveleCmimesh();
        //        return colNiveleCmimesh.mbushArrayListNiveleCmimesh(ds);
        //    }
        //    catch (Exception)
        //    {
        //        return new colNiveleCmimesh();
        //    }
        //    finally
        //    {
        //        dbManager.Dispose();
        //    }
        //}

        /// <summary>
        /// merr gjithe nivelet e cmimeve te nje ndermarje
        /// </summary>
        /// <param name="idnder"> id e ndermarjes</param>
        /// <returns> nje datatable qe permban nje koleksion me te gjithe nivelet e cmimeve te ndermarjes</returns>
        internal DataTable ktheGjitheNiveleCmimeshSipasNdermarjes(int idnder)
        {

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDNDERMARJE", idnder, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_NIVELCMIMI_merrNivelCmimiSipasNdermarjes");

            return ds.Tables[0];

        }

        internal DataTable ktheNiveleCmimeshMeFilter(string filter, long startIndex, long endIndex, int idnderm, int lloji)
        {

            dbManager.Open();
            dbManager.CreateParameters(5);
            dbManager.AddParameters(0, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            dbManager.AddParameters(1, "@FILTER", filter, ParameterDirection.Input);
            dbManager.AddParameters(2, "@STARTINDEX", startIndex, ParameterDirection.Input);
            dbManager.AddParameters(3, "@ENDINDEX", endIndex, ParameterDirection.Input);
            dbManager.AddParameters(4, "@lloji", lloji, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_NIVELCMIMI_merrNivelCmimiSipasNdermarjesMeFilter");
            return ds.Tables[0];

        }

        //[Obsolete("Perdor: DataTable ktheGjitheNiveleCmimeshSipasNdermarjes(int idnder)", true)]
        //public colNiveleCmimesh  merrGjitheNiveleCmimeshSipasNdermarjes(int idnder)
        //{//metoda per te marre te gjithe nivelet e cmimeve
        //    IDBManager dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        //    dbManager.ConnectionString = dbManager.GetConnectionString();
        //    try
        //    {
        //        dbManager.Open();
        //        dbManager.CreateParameters(1);
        //        dbManager.AddParameters(0, "@IDNDERMARJE", idnder, ParameterDirection.Input);
        //        DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_NIVELCMIMI_merrNivelCmimiSipasNdermarjes");
        //        colNiveleCmimesh colNiveleCmimesh = new colNiveleCmimesh();
        //        return colNiveleCmimesh.mbushArrayListNiveleCmimesh(ds);
        //    }
        //    catch (Exception)
        //    {
        //        return new colNiveleCmimesh();
        //    }
        //    finally
        //    {
        //        dbManager.Dispose();
        //    }
        //}

        /// <summary>
        /// merr gjithe nivelet e cmimeve prind te nje ndermarje 
        /// </summary>
        /// <param name="idnder"> id e ndermarjes</param>
        /// <returns> nje datatable qe permban nje koleksion me te gjithe nivelet e cmimive prind te ndermarjes <returns>
        internal DataTable ktheGjitheNiveleCmimeshPrindiSipasNdermarjes(int idnder)
        {

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDNDERMARJE", idnder, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_NIVELCMIMI_merrNivelCmimiPrindSipasNdermarjes");

            return ds.Tables[0];

        }
        internal DataTable ktheGjitheNiveleCmimeshMeMonedhePrindiSipasNdermarjes(int idnder)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDNDERMARJE", idnder, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_NIVELCMIMI_merrNivelCmimiPrindSipasNdermarjesMeMonedhe");

            return ds.Tables[0];


        }
        //[Obsolete("Perdor: DataTable ktheGjitheNiveleCmimeshPrindiSipasNdermarjes(int idnder)", true)]
        //public colNiveleCmimesh merrGjitheNiveleCmimeshPrindiSipasNdermarjes(int idnder)
        //{//metoda per te marre te gjithe nivelet e cmimeve prind
        //    IDBManager dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        //    dbManager.ConnectionString = dbManager.GetConnectionString();
        //    try
        //    {
        //        dbManager.Open();
        //        dbManager.CreateParameters(1);
        //        dbManager.AddParameters(0, "@IDNDERMARJE", idnder, ParameterDirection.Input);
        //        DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_NIVELCMIMI_merrNivelCmimiPrindSipasNdermarjes");
        //        colNiveleCmimesh colNiveleCmimesh = new colNiveleCmimesh();
        //        return colNiveleCmimesh.mbushArrayListNiveleCmimesh(ds);
        //    }
        //    catch (Exception)
        //    {
        //        return new colNiveleCmimesh();
        //    }
        //    finally
        //    {
        //        dbManager.Dispose();
        //    }
        //}

        /// <summary>
        /// merr id e nivelit te cmimit te nje ndermarje me kete kod
        /// </summary>
        /// <param name="kodi"> kodi i nivelit te cmimit</param>
        /// <param name="idnder"> id e ndermarjes</param>
        /// <returns> nje int id e nivelit te cmimit te nje ndermarje me kete kod</returns>
        internal int ktheIdNivelCmimiSipasKodit(string kodi, int idnder)
        {

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@KODNIVELCMIMI", kodi, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idnder, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_NIVELCMIMI_merrNivelCmimiSipasKodit");

            if (ds == null)
                return -1;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return -1;
            int idNivelCmimi;
            int.TryParse(ds.Tables[0].Rows[0]["IDNIVELCMIMI"].ToString(), out idNivelCmimi);
            return idNivelCmimi;

        }

        internal int ktheIdNivelCmimiSipasPershkrimit(string pershkrimi, int idnder)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@PERSHKRIMNIVELCMIMI", pershkrimi, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idnder, ParameterDirection.Input);
            return Convert.ToInt32(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_NIVELCMIMI_merrIdNivelCmimiSipasPershkrimit"));
        }

        internal DataRow ktheNivelCmimiSipasKodit(string kodi, int idnder)
        {

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@KODNIVELCMIMI", kodi, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idnder, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_NIVELCMIMI_merrNivelCmimiSipasKodit");

            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];

        }

        /// <summary>
        /// kthen true nese ka nivel cmimi baze per ndermarrjen
        /// </summary>
        /// <param name="kodi"> kodi i nivelit te cmimit</param>
        /// <param name="idnder"> id e ndermarjes</param>
        /// <returns> nje int id e nivelit te cmimit te nje ndermarje me kete kod</returns>
        internal bool kaNivelCmimiBaze(int idnder, int lloji)
        {

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDNDERMARJE", idnder, ParameterDirection.Input);
            dbManager.AddParameters(1, "@lloji", lloji, ParameterDirection.Input);
            int pergjigje = Convert.ToInt32(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_NIVELCMIMI_kaNivelCmimiBazeSipasLlojit"));
            return Convert.ToBoolean(pergjigje);

        }
        /// <summary>
        /// kthen true nese artikulli eshte me serial 
        /// </summary>
        /// <param name="idArtikulli">id e artikullit</param>
        /// <returns></returns>
        internal bool EshteMeSerial(int idArtikulli)
        {

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDARTIKULLI", idArtikulli, ParameterDirection.Input);
            int meSerial = Convert.ToInt32(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_Artikulli_MeSerial"));
            return Convert.ToBoolean(meSerial);

        }
        /// <summary>
        /// kthen true nese artikulli eshte me detajim 
        /// </summary>
        /// <param name="idArtikulli">id e artikullit</param>
        /// <returns></returns>
        internal bool EshteDetajim(int idArtikulli)
        {

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDARTIKULLI", idArtikulli, ParameterDirection.Input);
            int meDetajim = Convert.ToInt32(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_Artikulli_MeDetajim"));
            return Convert.ToBoolean(meDetajim);

        }

        /// <summary>
        /// kthen nivelin e cmimit baze per ndermarrjen
        /// </summary>
        /// <param name="kodi"> kodi i nivelit te cmimit</param>
        /// <param name="idnder"> id e ndermarjes</param>
        /// <returns> nje int id e nivelit te cmimit te nje ndermarje me kete kod</returns>
        internal DataRow merrNivelCmimiBaze(int idnder, int lloji)
        {
            ImbLogger.LogTraceShitje($"Filloi metoda merrNivelCmimiBaze sipas idnder:{idnder} dhe lloji :{lloji}");
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDNDERMARJE", idnder, ParameterDirection.Input);
            dbManager.AddParameters(1, "@lloji", lloji, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_NIVELCMIMI_merrNivelCmimiBazeDheLlojit");
            if (ds == null)
            {
                ImbLogger.LogTraceShitje($"Metoda merrNivelCmimiBaze sipas idnder:{idnder} dhe lloji :{lloji} ktheu null");
                return null;
            }

            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
            {
                ImbLogger.LogTraceShitje($"Metoda merrNivelCmimiBaze sipas idnder:{idnder} dhe lloji :{lloji} ktheu null");
                return null;
            }

            ImbLogger.LogTraceShitje($"Mbaroi metoda merrNivelCmimiBaze sipas idnder:{idnder} dhe lloji :{lloji}");
            return ds.Tables[0].Rows[0];
        }

        /// <summary>
        /// kthen nivelin e cmimit baze per ndermarrjen
        /// </summary>
        /// <param name="kodi"> kodi i nivelit te cmimit</param>
        /// <param name="idnder"> id e ndermarjes</param>
        /// <returns> nje int id e nivelit te cmimit te nje ndermarje me kete kod</returns>
        internal DataTable merrKodeNiveleCmimiSipasNdermarrjesDhePerdoruesit(int idNdermarrje, int idPerdorues)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDNDERMARJE", idNdermarrje, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUES", idPerdorues, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_NIVELCMIMI_selPershkrimetSipasNdermarrjesDhePerdoruesit");
            if (ds == null)
                return null;
            return ds.Tables[0];
        }


        /// <summary>
        /// merr gjithe nivelet e cmimit te nje prindi
        /// </summary>
        /// <param name="idprindi">id e nivelit prind</param>
        /// <returns>nje datatable qe permban nje koleksion me te gjitha nivelet e cmimeve bij te ketij prindi</returns>
        internal DataTable ktheNivelSipasPrindit(int idprindi)
        {

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDPRINDI", idprindi, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_NIVELCMIMI_merrNivelCmimiSipasPrindit");

            return ds.Tables[0];

        }
        //[Obsolete("Perdor: DataTable ktheNivelSipasPrindit(int idprindi)", true)]
        //public colNiveleCmimesh merrNivelCmimiSipasPrindit(int idprindi)
        //{//metoda per te marre NIVEL CMIMI sipas KODIT
        //    IDBManager dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        //    dbManager.ConnectionString = dbManager.GetConnectionString();
        //    try
        //    {
        //        dbManager.Open();
        //        dbManager.CreateParameters(1);
        //        dbManager.AddParameters(0, "@IDPRINDI", idprindi , ParameterDirection.Input);
        //        DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_NIVELCMIMI_merrNivelCmimiSipasPrindit");
        //        colNiveleCmimesh colNiveleCmimesh = new colNiveleCmimesh();
        //        return colNiveleCmimesh.mbushArrayListNiveleCmimesh(ds);
        //    }
        //    catch (Exception)
        //    {
        //        return new colNiveleCmimesh();
        //    }
        //    finally
        //    {
        //        dbManager.Dispose();
        //    }
        //}

        /// <summary>
        /// merr gjithe nivelet e cmimit me kete pershkrim
        /// </summary>
        /// <param name="pershkrim"> pershkrimi i nivelit te cmimit</param>
        /// <param name="idndermarje"> id e ndermarjes</param>
        /// <returns> nje datatable qe permban nje koleksion me te gjitha nivelet e cmimeve me kete pershkrim te kesaj ndermarje</returns>
        internal DataRow ktheNivelCmimiSipasPershkrimit(string pershkrim, int idndermarje)
        {

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@PERSHKRIMNIVELCMIMI", pershkrim, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idndermarje, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_NIVELCMIMI_merrNivelCmimiSipasPershkrimit");
            return ds.Tables[0].Rows[0];

        }

        /// <summary>
        /// perdoret per te kontrolluar nqs ekziston nje nivel cmimi me kete kod
        /// behet kontrolli per qellime konsistence te informacionit ne DB, nuk lejon te celen nivele cmimi te ndryshem me te njejtin kod
        /// </summary>
        /// <param name="kodNivelCmimi">kodi i nivelit te cmimit</param>
        /// <param name="idNdermarje"> id e ndermarjes</param>
        /// <returns>nje objeckt boolean qe tregon nese ekziston apo jo nje nivel cmimi me kete kod</returns>
        public bool ekzistonNivelCmimi(string kodNivelCmimi, int idNdermarje)
        {

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@KODNIVELCMIMI", kodNivelCmimi, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idNdermarje, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_NIVELCMIMI_existon");
            if (ds.Tables[0].Rows.Count == 1)
                return true;
            else if (ds.Tables[0].Rows.Count == 0)
                return false;
            else return true;

        }
        public bool ekzistonPershkrimiNivelCmimi(string pershkrimNivelCmimi, int idNdermarje, int idNivelCmimi)
        {

            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@PERSHKRIMNIVELCMIMI", pershkrimNivelCmimi, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idNdermarje, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDNIVELCMIMI", idNivelCmimi, ParameterDirection.Input);
            int ekziston = Convert.ToInt32(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_NIVELCMIMI_PershkrimiEkziston"));
            if (ekziston > 0)
                return true;
            else
                return false;
        }

        /// <summary>
        /// perdoret per te kontrolluar nese nje nivel cmimi ka bij. perdoret ne rastet e fshirjes se nivelit te cmimit per te mos lejuar te fshihet nje nivel prind
        /// </summary>
        /// <param name="idprindi"> id e nivelit</param>
        /// <returns> kthen nje objekt boolean qe tregon nese ky nivel ka nivele bij apo jo</returns>
        public bool kaBijNivelCmimi(int idprindi)
        {//kontrollon nqs ky nivelcmimi ka bij           

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDPRINDI", idprindi, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_NIVELCMIMI_kaBij");
            if (ds.Tables[0].Rows.Count == 1)
                return true;
            else if (ds.Tables[0].Rows.Count == 0)
                return false;
            else return true;

        }
        internal DataRow merrNivelCmimiSipasNdermarjesDR(int idnderm, int idnivel)
        {


            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNIVELCMIMI", idnivel, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_NIVELCMIMI_merrNivelCmimiSipasNdermarjesDR");

            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];

        }
        internal int merrIdMonedhaSipasNivelCmimi(int idNivelCmimi, int idnderm)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNIVELCMIMI", idNivelCmimi, ParameterDirection.Input);
            return Convert.ToInt32(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_NIVELCMIMI_merrNivelCmimiSipasIdMonedha"));
        }
        internal DataTable merrNiveleNdermarjeDT(int idnderm, int idperd)
        {


            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUES", idperd, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_NIVELCMIMI_merrNiveleNdermarjeDT");
            return ds.Tables[0];

        }

        internal DataTable merrNiveleNdermarjeDTBlerjeShitje(int idnderm, int llojniveli, int idperd)
        {


            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            dbManager.AddParameters(1, "@LLOJINIVELCMIMI", llojniveli, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDPERDORUES", idperd, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_NIVELCMIMI_merrNiveleNdermarjeDTBlerjeShitje");

            return ds.Tables[0];

        }

        internal DataTable merrNiveleNdermarjeDTBlerjeShitjeMeAutorizime(int idnderm, int llojniveli, int idPerdorues)
        {

            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            dbManager.AddParameters(1, "@LLOJINIVELCMIMI", llojniveli, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDPERDORUES", idPerdorues, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_NIVELCMIMI_merrNiveleNdermarjeDTBlerjeShitjeMeAutorizime");
            return ds.Tables[0];

        }
        internal DataTable merrNiveleNdermarjeDTBlerjeDheShitjeMeAutorizime(int idnderm, int idPerdorues)
        {

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUES", idPerdorues, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_NIVELCMIMI_merrNiveleNdermarjeDTBlerjeDheShitjeMeAutorizime");
            return ds.Tables[0];

        }
        internal DataTable merrNiveleNdermarjeDTBlerjeShitjeMeAutorizimePerAutocompleteCmimesh(int idnderm, int llojniveli, int idPerdorues)
        {

            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            dbManager.AddParameters(1, "@LLOJINIVELCMIMI", llojniveli, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDPERDORUES", idPerdorues, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_NIVELCMIMI_merrNiveleNdermarjeDTBlerjeShitjeMeAutorizimeAutoCompleteShitje");
            return ds.Tables[0];

        }
        #endregion

        /// <summary>
        /// Metodat e meposhtme i perkasin veprimeve qe kryhen nga objektet clsCmimArtikulli dhe colCmimeArtikujsh
        /// </summary>
        #region CMIM ARTIKULLI

        /// <summary>
        /// ekzekuton prc_T_CMIMARTIKULLI_ins ne DB dhe kthen statusin e perfundimit te ekzekutimit te SP-se perkatese
        /// Shiko clsMesazh brenda ketij moduli per informacion te metejshem mbi vleren e kthimit te kesaj metode
        /// <param name="idCmimArtikulli"> id ritese e cmimit te artikullit</param>
        /// <param name="idArtikulli"> id e artikullit</param>
        /// <param name="idNivelCmimi"> id e nivelit te cmimit</param>
        /// <param name="idNjesia">id njesia e artikullit</param>
        /// <param name="idMonedha"> id monedha</param>
        /// <param name="dateFillimit"> data e fillimit te periudhes per kete cmim</param>
        /// <param name="dateMbarimit"> data e mbarimit te periudhes per kete cmim</param>
        /// <param name="sasiMin"> sasia minimale</param>
        /// <param name="sasiMax"> sasia maksimale</param>
        /// <param name="cmimi">cmimi</param>
        /// <param name="idPerdoruesi"> id  e perdoruesit qe e ka kryer veprimin</param>
        /// <param name="idNderViti">id e ndermarje vitit</param>
        /// <param name="idNdermarje">id e ndermarjes</param>
        /// <param name="idkonfig">id e konfigurimit</param>
        /// <returns> nje objekt clsMesazh qe tregon nese ruajtja eshte kryer ne rregull apo jo.</returns>
        /// </summary>
        internal clsMesazh ruajCmimArtikulli(out int idCmimArtikulli, int idArtikulli, int idNivelCmimi, int idNjesia, int idMonedha,
        DateTime dateFillimit, DateTime dateMbarimit, decimal sasiMin, decimal sasiMax, decimal cmimi, int idPerdoruesi, int idNdermarje, int idkonfig, int idstatusdok, int idnjesia2, decimal cmimi2, DateTime kohefillimi, DateTime kohembarimi, int idDetajim)//, string formula
        { //metoda per ruajtjen e cmimArtikulli
            idCmimArtikulli = -1;

            dbManager.Open();

            //shtimi i parametrave
            dbManager.CreateParameters(19);
            dbManager.AddParameters(0, "@IDCMIMARTIKULLI", idCmimArtikulli, ParameterDirection.Output);
            dbManager.AddParameters(1, "@IDARTIKULLI", idArtikulli, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDNIVELCMIMI", idNivelCmimi, ParameterDirection.Input);
            dbManager.AddParameters(3, "@IDNJESIA", idNjesia, ParameterDirection.Input);
            dbManager.AddParameters(4, "@IDMONEDHA", idMonedha, ParameterDirection.Input);
            dbManager.AddParameters(5, "@DTFILLIMIT", dateFillimit, ParameterDirection.Input);
            dbManager.AddParameters(6, "@DTMBARIMIT", dateMbarimit, ParameterDirection.Input);
            dbManager.AddParameters(7, "@SASIMIN", sasiMin, ParameterDirection.Input);
            dbManager.AddParameters(8, "@SASIMAX", sasiMax, ParameterDirection.Input);
            dbManager.AddParameters(9, "@CMIMI", cmimi, ParameterDirection.Input);
            dbManager.AddParameters(10, "@IDPERDORUESI", idPerdoruesi, ParameterDirection.Input);
            //dbManager.AddParameters(11, "@IDNDERVITI", idNderViti, ParameterDirection.Input);
            dbManager.AddParameters(11, "@IDNDERMARJE", idNdermarje, ParameterDirection.Input);
            dbManager.AddParameters(12, "@IDKONFIG", idkonfig, ParameterDirection.Input);
            dbManager.AddParameters(13, "@IDSTATUSDOK", idstatusdok, ParameterDirection.Input);
            dbManager.AddParameters(14, "@IDNJESIA2", idnjesia2, ParameterDirection.Input);
            dbManager.AddParameters(15, "@CMIMI2", cmimi2, ParameterDirection.Input);
            dbManager.AddParameters(16, "@KOHEFILLIMI", kohefillimi, ParameterDirection.Input);
            dbManager.AddParameters(17, "@KOHEMBARIMI", kohembarimi, ParameterDirection.Input);
            dbManager.AddParameters(18, "@IdDetajim", idDetajim, ParameterDirection.Input);

            //dbManager.AddParameters(16, "@FORMULA", formula, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_CMIMARTIKULLI_ins");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
            return mesazh;

        }


        /// <summary>
        /// ekzekuton prc_T_CMIMARTIKULLI_upd ne DB dhe kthen statusin e perfundimit te ekzekutimit te SP-se perkatese
        /// Shiko clsMesazh brenda ketij moduli per informacion te metejshem mbi vleren e kthimit te kesaj metode
        /// <param name="idCmimArtikulli"> id ritese e cmimit te artikullit</param>
        /// <param name="idArtikulli"> id e artikullit</param>
        /// <param name="idNivelCmimi"> id e nivelit te cmimit</param>
        /// <param name="idNjesia">id njesia e artikullit</param>
        /// <param name="idMonedha"> id monedha</param>
        /// <param name="dateFillimit"> data e fillimit te periudhes per kete cmim</param>
        /// <param name="dateMbarimit"> data e mbarimit te periudhes per kete cmim</param>
        /// <param name="sasiMin"> sasia minimale</param>
        /// <param name="sasiMax"> sasia maksimale</param>
        /// <param name="cmimi">cmimi</param>
        /// <param name="idPerdoruesi"> id  e perdoruesit qe e ka kryer veprimin</param>
        /// <param name="idNderViti">id e ndermarje vitit</param>
        /// <param name="idNdermarje">id e ndermarjes</param>
        /// <param name="idkonfig">id e konfigurimit</param>
        /// <returns> nje objekt clsMesazh qe tregon nese modifikimi eshte kryer ne rregull apo jo.</returns>
        /// </summary>
        internal clsMesazh modifikoCmimArtikulli(int idCmimArtikulli, int idArtikulli, int idNivelCmimi, int idNjesia, int idMonedha,
        DateTime dateFillimit, DateTime dateMbarimit, decimal sasiMin, decimal sasiMax, decimal cmimi, int idPerdoruesi, int idNdermarje,
        int idkonfig, int idstatusdok, int idnjesia2, decimal cmimi2, DateTime kohefillimi, DateTime kohembarimi, int idDetajim)//, string formula
        {

            dbManager.Open();
            //shtimi i parametrave
            dbManager.CreateParameters(19);
            dbManager.AddParameters(0, "@IDCMIMARTIKULLI", idCmimArtikulli, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDARTIKULLI", idArtikulli, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDNIVELCMIMI", idNivelCmimi, ParameterDirection.Input);
            dbManager.AddParameters(3, "@IDNJESIA", idNjesia, ParameterDirection.Input);
            dbManager.AddParameters(4, "@IDMONEDHA", idMonedha, ParameterDirection.Input);
            dbManager.AddParameters(5, "@DTFILLIMIT", dateFillimit, ParameterDirection.Input);
            dbManager.AddParameters(6, "@DTMBARIMIT", dateMbarimit, ParameterDirection.Input);
            dbManager.AddParameters(7, "@SASIMIN", sasiMin, ParameterDirection.Input);
            dbManager.AddParameters(8, "@SASIMAX", sasiMax, ParameterDirection.Input);
            dbManager.AddParameters(9, "@CMIMI", cmimi, ParameterDirection.Input);
            dbManager.AddParameters(10, "@IDPERDORUESI", idPerdoruesi, ParameterDirection.Input);
            //dbManager.AddParameters(11, "@IDNDERVITI", idNderViti, ParameterDirection.Input);
            dbManager.AddParameters(11, "@IDNDERMARJE", idNdermarje, ParameterDirection.Input);
            dbManager.AddParameters(12, "@IDKONFIG", idkonfig, ParameterDirection.Input);
            dbManager.AddParameters(13, "@IDSTATUSDOK", idstatusdok, ParameterDirection.Input);
            dbManager.AddParameters(14, "@IDNJESIA2", idnjesia2, ParameterDirection.Input);
            dbManager.AddParameters(15, "@CMIMI2", cmimi2, ParameterDirection.Input);
            dbManager.AddParameters(16, "@KOHEFILLIMI", kohefillimi, ParameterDirection.Input);
            dbManager.AddParameters(17, "@KOHEMBARIMI", kohembarimi, ParameterDirection.Input);
            dbManager.AddParameters(18, "@IdDetajim", idDetajim, ParameterDirection.Input);
            //dbManager.AddParameters(16, "@FORMULA", formula, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_CMIMARTIKULLI_upd");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
            return mesazh;

        }

        internal void hidhCmimArtikulliNeHistorik(int idCmimArtikulli)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters("@IDCMIMARTIKULLI", idCmimArtikulli, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_CMIMARTIKULLI_hidhNeHistorik");

        }
        /// <summary>
        /// ekzekutohet sp-ja prc_T_CMIMARTIKULLI_del duke i kaluar id e cmimit te artikullit qe e marrim nga objekti clsCmimArtikulli qe i kalohet si parameter
        /// </summary>
        /// <param name="idCmimArtikulli"> id ritese e cmimit te artikullit</param>
        /// <returns>nje objekt clsMesazh qe tregon nese fshirja eshte kryer ne rregull apo jo</returns>
        internal clsMesazh fshiCmimArtikulli(int idCmimArtikulli)
        {//metoda per fshirjen e cmimArtikulli

            dbManager.Open();
            //shtimi i parametrave
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDCMIMARTIKULLI", idCmimArtikulli, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_CMIMARTIKULLI_del");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
            return mesazh;

        }
        internal clsMesazh fshiCmimArtikulliStatus(int idCmimArtikulli, int idperdorues)
        {//metoda per fshirjen e cmimArtikulli

            dbManager.Open();
            //shtimi i parametrave
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDCMIMARTIKULLI", idCmimArtikulli, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUESI", idperdorues, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_CMIMARTIKULLI_upddel");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
            return mesazh;

        }

        /// <summary>
        /// kthen dataTable cmim artikujsh  sipas idse
        /// </summary>
        ///<param name="idCmimArtikulli"> cmimi i artikullit qe do i merret id</param>
        ///<returns> nje datatable qe permban nje koleksion me te gjithe cmimet e artikujve me kete id</returns>
        internal DataTable ktheCmimArtikulliDtExport(int idndermarje)
        {


            dbManager.Open();
            //shtimi i parametrave
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@idndermarje", idndermarje, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_CMIMARTIKULLI_merrDTExport");

            return ds.Tables[0];

        }
        internal DataTable ktheCmimArtikulli(int idCmimArtikulli)
        {


            dbManager.Open();
            //shtimi i parametrave
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDCMIMARTIKULLI", idCmimArtikulli, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_CMIMARTIKULLI_sel");

            return ds.Tables[0];

        }

        /// <summary>
        /// kthen datatable artikujsh sipas nivelit
        /// </summary>
        /// <param name="kodartikulli"> kodi i artikullit</param>
        /// <param name="idnivelcmimi"> id e nivelit te cmimit</param>
        /// <param name="idperdorues"> id e perdoruesit</param>
        /// <param name="idndermarje"> id e ndermarjes</param>
        /// <returns>nje datatable qe permban nje koleksion me te gjitha cmimet e artikujve te ketij niveli</returns>
        internal DataTable ktheCmimArtikulliSipasNivelitMeDhePaDetajim(string kodartikulli, string idnivelcmimi, int idperdorues, int idndermarje, int iddetajim)
        {
            dbManager.Open();
            dbManager.CreateParameters(5);
            dbManager.AddParameters(0, "@KODARTIKULLI", kodartikulli, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNIVELCMIMI", idnivelcmimi, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDPERDORUES", idperdorues, ParameterDirection.Input);
            dbManager.AddParameters(3, "@IDNDERMARJE", idndermarje, ParameterDirection.Input);
            dbManager.AddParameters(4, "@IdDetajim", iddetajim, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_CMIMARTIKULLI_merrCmimArtikulliSipasNivelitMeDhePaDetajim");
            return ds.Tables[0];
        }

        internal DataTable ktheCmimArtikulliSipasNivelit(int idartikulli, string idnivelcmimi, int idperdorues, int idndermarje, int iddetajim = 0)
        {

            dbManager.Open();
            dbManager.CreateParameters(4);
            dbManager.AddParameters(0, "@IDARTIKULLI", idartikulli, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNIVELCMIMI", idnivelcmimi, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDPERDORUES", idperdorues, ParameterDirection.Input);
            dbManager.AddParameters(3, "@IDNDERMARJE", idndermarje, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_CMIMARTIKULLI_merrCmimIdArtikulliSipasNivelit");
            return ds.Tables[0];

        }
        
        internal IEnumerable<clsCmimArtikulli> merrCmimetAllPaFiltra(int idNdermarrje, int idPerdoruesi, int shitjeBlerje, int llogaritKosto, bool vetemDetajimet, int idNivelCmimi)
        {
            clsCmimArtikulli.IdAutomatike = -1;
            dbManager.Open();
            dbManager.CreateParameters(5);
            dbManager.AddParameters("@IDPERDORUES", idPerdoruesi, ParameterDirection.Input);
            dbManager.AddParameters("@IDNDERMARJE", idNdermarrje, ParameterDirection.Input);
            dbManager.AddParameters("@CMIMSHITJEBLERJE", shitjeBlerje, ParameterDirection.Input);
            dbManager.AddParameters("@LLOGARITKOSTO", llogaritKosto, ParameterDirection.Input);
            dbManager.AddParameters("@IDNIVELCMIMI", idNivelCmimi, ParameterDirection.Input);
            return vetemDetajimet && shitjeBlerje == 0 ? dbManager.GetIEnumerbale("prc_T_CMIMARTIKULLI_merrCmimetAllPaFiltraDetajime", clsCmimArtikulli.Krijo) : dbManager.GetIEnumerbale("prc_T_CMIMARTIKULLI_merrCmimetAllPaFiltra", clsCmimArtikulli.Krijo);


        }

        internal DataTable merrCmimetAllPaFiltraDT(int idNdermarrje, int idPerdoruesi, int shitjeBlerje, int llogaritKosto, bool vetemDetajimet)
        {
            clsCmimArtikulli.IdAutomatike = -1;
            dbManager.Open();
            dbManager.CreateParameters(4);
            dbManager.AddParameters(0, "@IDPERDORUES", idPerdoruesi, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idNdermarrje, ParameterDirection.Input);
            dbManager.AddParameters(2, "@CMIMSHITJEBLERJE", shitjeBlerje, ParameterDirection.Input);
            dbManager.AddParameters(3, "@LLOGARITKOSTO", llogaritKosto, ParameterDirection.Input);
            DataSet ds = vetemDetajimet && shitjeBlerje == 0 ? dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_CMIMARTIKULLI_merrCmimetAllPaFiltraDetajime") : dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_CMIMARTIKULLI_merrCmimetAllPaFiltra");

            return ds.Tables[0];

        }

        internal DataTable ktheCmimArtikulliSipasArtikullit(int idartikulli, int idndermarje)
        {



            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDARTIKULLI", idartikulli, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idndermarje, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_CMIMARTIKULLI_merrCmimArtikulliSipasArtikullit");

            return ds.Tables[0];

        }

        internal DataTable ktheCmimArtikulliSipasArtikullitMeKosto(int idartikulli, int idndermarje)
        {


            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDARTIKULLI", idartikulli, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idndermarje, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_CMIMARTIKULLI_merrCmimArtikulliSipasArtikullitMeKosto");

            return ds.Tables[0];

        }

        internal DataTable ktheCmimArtikulliSipasArtikullitMeKostoMeAutorizime(int idartikulli, int idndermarje, int idPerdorues)
        {


            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@IDARTIKULLI", idartikulli, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idndermarje, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDPERDORUES", idPerdorues, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_CMIMARTIKULLI_merrCmimArtikulliSipasArtikullitMeKostoMeAutorizime");

            return ds.Tables[0];

        }
        internal DataTable ktheCmimArtikulliShitjeDheBlerjeSipasArtikullitMeKostoMeAutorizime(int idartikulli, int idndermarje, int idPerdorues)
        {


            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@IDARTIKULLI", idartikulli, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idndermarje, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDPERDORUES", idPerdorues, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_CMIMARTIKULLI_merrCmimArtikulliShitjeDheBlerjeSipasArtikullitMeKostoMeAutorizime");

            return ds.Tables[0];

        }
        internal DataTable ktheCmimeArtikulliAll(int idndermarje)
        {



            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDNDERMARJE", idndermarje, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_CMIMARTIKULLI_merrCmimeArtikulliAll");

            return ds.Tables[0];

        }

        internal bool ekzistonCmimArtikulli(int idartikulli, int idnivel, out int idcmimi)
        {



            idcmimi = 0;
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDARTIKULLI", idartikulli, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDniveli", idnivel, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_CMIMARTIKULLI_ekziston");
            if (ds.Tables[0].Rows.Count == 0)
                return false;
            else
            {
                idcmimi = Convert.ToInt32(ds.Tables[0].Rows[0][0]);
                return true;
            }


        }
        internal int merrCmimArtikull(int idArtikull, int idNivel)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDARTIKULLI", idArtikull, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDniveli", idNivel, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_CMIMARTIKULLI_merrId");
            if (ds.Tables[0].Rows.Count == 0)
                return 0;
            else
                return Convert.ToInt32(ds.Tables[0].Rows[0][0]);
        }
        //[Obsolete("Perdor: DataTable ktheCmimArtikulliSipasNivelit(string kodartikulli, string idnivelcmimi, int idperdorues, int idndermarje)", true)]
        //public colCmimeArtikujsh merrCmimArtikulliSipasNivelit(string kodartikulli,string idnivelcmimi,int idperdorues, int idndermarje)
        //{
        //    IDBManager dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        //    dbManager.ConnectionString = dbManager.GetConnectionString();
        //    try
        //    {
        //        dbManager.Open();
        //        dbManager.CreateParameters(4);
        //        dbManager.AddParameters(0, "@KODARTIKULLI", kodartikulli, ParameterDirection.Input);
        //        dbManager.AddParameters(1, "@IDNIVELCMIMI", idnivelcmimi, ParameterDirection.Input);
        //        dbManager.AddParameters(2, "@IDPERDORUES", idperdorues, ParameterDirection.Input);
        //        dbManager.AddParameters(3, "@IDNDERMARJE", idndermarje, ParameterDirection.Input);
        //        DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_CMIMARTIKULLI_merrCmimArtikulliSipasNivelit");
        //        colCmimeArtikujsh colCmimeArtikujsh = new colCmimeArtikujsh();
        //        return colCmimeArtikujsh.mbushArrayListCmimeArtikujsh(ds);
        //    }
        //    catch (Exception)
        //    {
        //        return new colCmimeArtikujsh();
        //    }
        //    finally
        //    {
        //        dbManager.Dispose();
        //    }
        //}

        #endregion

        /// <summary>
        /// Metodat e meposhtme i perkasin veprimeve qe kryhen nga objektet clsNjesiArtikulli dhe colNjesiteArtikulli
        /// </summary>
        #region NJESI ARTIKULLI

        /// <summary>
        /// ekzekuton prc_T_NJESIARTIKULLI_ins ne DB dhe kthen statusin e perfundimit te ekzekutimit te SP-se perkatese
        /// Shiko clsMesazh brenda ketij moduli per informacion te metejshem mbi vleren e kthimit te kesaj metode
        /// <param name="idNjesia"> id ritese e njesise</param>
        /// <param name="kodNjesia"> kod njesia</param>
        /// <param name="pershkrimNjesia">pershkrim njesia</param>
        /// <param name="idPerdoruesi"> id e perdoruesit qe ka kryer veprimin</param>
        /// <param name="idNderViti"> id ndermarje viti</param>
        /// <param name="idnderm"> id ndermarje</param>
        /// <returns> nje objekt clsMesazh qe tregon nese ruajtja eshte kryer ne rregull apo jo.</returns>
        /// </summary>
        internal clsMesazh ruajNjesiArtikulli(out int idNjesia, string kodNjesia, String pershkrimNjesia, int idPerdoruesi, int idnderm, int idstatudsok, string kodEinvoice, bool klientFiskalizimi)
        { //metoda per ruajtjen e njesise se Artikullit
            idNjesia = -1;

            dbManager.Open();
            clsMesazh mesazh = new clsMesazh();
            if(klientFiskalizimi)
                dbManager.CreateParameters(7);
            else
                dbManager.CreateParameters(6);
            dbManager.AddParameters(0, "@IDNJESIA", idNjesia, ParameterDirection.Output);
            dbManager.AddParameters(1, "@KODNJESIA", kodNjesia, ParameterDirection.Input);
            dbManager.AddParameters(2, "@PERSHKRIMNJESIA", pershkrimNjesia, ParameterDirection.Input);
            dbManager.AddParameters(3, "@IDPERDORUESI", idPerdoruesi, ParameterDirection.Input);
            //dbManager.AddParameters(4, "@IDNDERVITI", idNderViti, ParameterDirection.Input);
            dbManager.AddParameters(4, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            dbManager.AddParameters(5, "@IDSTATUSDOK", idstatudsok, ParameterDirection.Input);
            if(klientFiskalizimi)
                dbManager.AddParameters(6, "@KODIEINVOICE", kodEinvoice, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_NJESIARTIKULLI_ins");
            idNjesia = int.Parse(dbManager.Parameters[0].Value.ToString());
            mesazh = new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);

            return mesazh;

        }
        //[Obsolete("Perdor: clsMesazh ruajNjesiArtikulli(int idNjesia, string kodNjesia, String pershkrimNjesia, int idPerdoruesi, int idNderViti, int idnderm)", true)]
        //public clsMesazh ruajNjesiArtikulli(clsNjesiArtikulli njesiArtikulli)
        //{ //metoda per ruajtjen e njesise se Artikullit
        //    IDBManager dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        //    dbManager.ConnectionString = dbManager.GetConnectionString();
        //    try
        //    {
        //        dbManager.Open();
        //        clsMesazh mesazh = new clsMesazh();            
        //        dbManager.CreateParameters(6);
        //        dbManager.AddParameters(0, "@IDNJESIA", njesiArtikulli.IdNjesia, ParameterDirection.Output);
        //        dbManager.AddParameters(1, "@KODNJESIA", njesiArtikulli.KodNjesia, ParameterDirection.Input);
        //        dbManager.AddParameters(2, "@PERSHKRIMNJESIA", njesiArtikulli.PershkrimNjesia, ParameterDirection.Input);
        //        dbManager.AddParameters(3, "@IDPERDORUESI", njesiArtikulli.IdPerdoruesi, ParameterDirection.Input);
        //        dbManager.AddParameters(4, "@IDNDERVITI", njesiArtikulli.IdNderViti, ParameterDirection.Input);
        //        dbManager.AddParameters(5, "@IDNDERMARJE", njesiArtikulli.IdNdermarje, ParameterDirection.Input);
        //        dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_NJESIARTIKULLI_ins");
        //        mesazh = new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);

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
        /// ekzekuton prc_T_NJESIARTIKULLI_upd ne DB dhe kthen statusin e perfundimit te ekzekutimit te SP-se perkatese
        /// Shiko clsMesazh brenda ketij moduli per informacion te metejshem mbi vleren e kthimit te kesaj metode
        /// <param name="idNjesia"> id ritese e njesise</param>
        /// <param name="kodNjesia"> kod njesia</param>
        /// <param name="pershkrimNjesia">pershkrim njesia</param>
        /// <param name="idPerdoruesi"> id e perdoruesit qe ka kryer veprimin</param>
        /// <param name="idNderViti"> id ndermarje viti</param>
        /// <param name="idnderm"> id ndermarje</param>
        /// <returns> nje objekt clsMesazh qe tregon nese modifikimi eshte kryer ne rregull apo jo.</returns>
        /// </summary>
        internal clsMesazh modifikoNjesiArtikulli(int idNjesia, string kodNjesia, String pershkrimNjesia, int idPerdoruesi, int idnderm, int idstatusdok, string kodEinvoice, bool klientFiskalizimi)
        {//metoda per modifikimin e njesise se artikullit


            dbManager.Open();
            clsMesazh mesazh = new clsMesazh();
            if(klientFiskalizimi)
                dbManager.CreateParameters(7);
            else
                dbManager.CreateParameters(6);
            dbManager.AddParameters(0, "@IDNJESIA", idNjesia, ParameterDirection.Input);
            dbManager.AddParameters(1, "@KODNJESIA", kodNjesia, ParameterDirection.Input);
            dbManager.AddParameters(2, "@PERSHKRIMNJESIA", pershkrimNjesia, ParameterDirection.Input);
            dbManager.AddParameters(3, "@IDPERDORUESI", idPerdoruesi, ParameterDirection.Input);
            //dbManager.AddParameters(4, "@IDNDERVITI", idNderViti, ParameterDirection.Input);
            dbManager.AddParameters(4, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            dbManager.AddParameters(5, "@IDSTATUSDOK", idstatusdok, ParameterDirection.Input);
            if(klientFiskalizimi)
                dbManager.AddParameters(6, "@KODIEINVOICE", kodEinvoice, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_NJESIARTIKULLI_upd");
            mesazh = new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);

            return mesazh;

        }
        //[Obsolete("Perdor: clsMesazh modifikoNjesiArtikulli(int idNjesia, string kodNjesia, String pershkrimNjesia, int idPerdoruesi, int idNderViti, int idnderm)", true)]
        //public clsMesazh modifikoNjesiArtikulli(clsNjesiArtikulli njesiArtikulli)
        //{//metoda per modifikimin e njesise se artikullit
        //    IDBManager dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        //    dbManager.ConnectionString = dbManager.GetConnectionString();
        //    try
        //    {
        //        dbManager.Open();
        //        clsMesazh mesazh = new clsMesazh();
        //        dbManager.CreateParameters(6);
        //        dbManager.AddParameters(0, "@IDNJESIA", njesiArtikulli.IdNjesia, ParameterDirection.Input);
        //        dbManager.AddParameters(1, "@KODNJESIA", njesiArtikulli.KodNjesia, ParameterDirection.Input);
        //        dbManager.AddParameters(2, "@PERSHKRIMNJESIA", njesiArtikulli.PershkrimNjesia, ParameterDirection.Input);
        //        dbManager.AddParameters(3, "@IDPERDORUESI", njesiArtikulli.IdPerdoruesi, ParameterDirection.Input);
        //        dbManager.AddParameters(4, "@IDNDERVITI", njesiArtikulli.IdNderViti, ParameterDirection.Input);
        //        dbManager.AddParameters(5, "@IDNDERMARJE", njesiArtikulli.IdNdermarje, ParameterDirection.Input);
        //        dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_NJESIARTIKULLI_upd");
        //        mesazh = new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);

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
        /// ekzekutohet sp-ja prc_T_NJESIARTIKULLI_del duke i kaluar id e njesise se artikullit qe e marrim nga objekti clsNjesiArtikulli qe i kalohet si parameter
        /// </summary>
        /// <param name="idNjesia"> njesia e artikullit qe do fshihet </param>
        /// <returns>nje objekt clsMesazh qe tregon nese fshirja eshte kryer ne rregull apo jo</returns>
        internal clsMesazh fshiNjesiArtikulli(int idNjesia)
        {//metoda per fshirjen e njesise se Artikullit


            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDNJESIA", idNjesia, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_NJESIARTIKULLI_del");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
            return mesazh;

        }
        internal clsMesazh fshiNjesiArtikulliStatus(int idNjesia, int idperdoruesi)
        {//metoda per fshirjen e njesise se Artikullit


            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDNJESIA", idNjesia, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUESI", idperdoruesi, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_NJESIARTIKULLI_upddel");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
            return mesazh;

        }
        //[Obsolete("Perdor: clsMesazh fshiNjesiArtikulli(int idNjesia)", true)]
        //public clsMesazh fshiNjesiArtikulli(clsNjesiArtikulli njesiArtikulli)
        //{//metoda per fshirjen e njesise se Artikullit
        //    IDBManager dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        //    dbManager.ConnectionString = dbManager.GetConnectionString();
        //    try
        //    {
        //        dbManager.Open();
        //        dbManager.CreateParameters(1);
        //        dbManager.AddParameters(0, "@IDNJESIA", njesiArtikulli.IdNjesia , ParameterDirection.Input);
        //        dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_NJESIARTIKULLI_del");
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
        /// kthen objektet njesi artikulli sipas idse
        /// </summary>
        ///<param name="idNjesia"> njesia e Artikullit qe do i merret id</param>
        internal void merrNjesiArtikulliPakthyer(int idNjesia)
        {// metoda per te marre nje njesi artikulli NE BAZE TE ID

            dbManager.Open();
            //shtimi i parametrave
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDNJESIA", idNjesia, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_NJESIARTIKULLI_sel");

        }





        //[Obsolete("Perdor: merrNjesiArtikulliPakthyer(int idNjesia)", true)]
        //public void merrNjesiArtikulli(clsNjesiArtikulli  njesiArtikulli)
        //{// metoda per te marre nje njesi artikulli NE BAZE TE ID
        //    IDBManager dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        //    dbManager.ConnectionString = dbManager.GetConnectionString();
        //    try
        //    {
        //        dbManager.Open();
        //        //shtimi i parametrave
        //        dbManager.CreateParameters(1);
        //        dbManager.AddParameters(0, "@IDNJESIA", njesiArtikulli .IdNjesia , ParameterDirection.Input);
        //        dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_NJESIARTIKULLI_sel");
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
        /// kthen objektet njesi artikulli sipas id ndermarjes
        /// </summary>
        ///<param name="idnder">id e ndermarjes</param>
        ///<returns>nje objekt colNjesiteArtikulli qe permban nje koleksion me te gjitha njesite e artikullit e kesaj ndermarje </returns>
        internal DataTable ktheGjitheNjesiteArtikulliSipasNdermarrjes(int idnder)
        {

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDNDERMARJE", idnder, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_NJESIARTIKULLI_merrSipasNdermarrjes");

            return ds.Tables[0];

        }
        //[Obsolete("Perdor: DataTable ktheGjitheNjesiteArtikulliSipasNdermarrjes(int idnder)", true)]
        //public colNjesiteArtikulli  merrGjitheNjesiteArtikulliSipasNdermarrjes(int  idnder)
        //{//metoda per te marre te gjithe njesite
        //    IDBManager dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        //    dbManager.ConnectionString = dbManager.GetConnectionString();
        //    try
        //    {
        //        dbManager.Open();
        //        dbManager.CreateParameters(1);
        //        dbManager.AddParameters(0, "@IDNDERMARJE", idnder , ParameterDirection.Input);               
        //        DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_NJESIARTIKULLI_merrSipasNdermarrjes");
        //        colNjesiteArtikulli colNjesiteArtikulli = new colNjesiteArtikulli();
        //        return colNjesiteArtikulli.mbushArrayListNjesishArtikulli(ds);
        //    }
        //    catch (Exception)
        //    {
        //        return new colNjesiteArtikulli();
        //    }
        //    finally
        //    {
        //        dbManager.Dispose();
        //    }
        //}

        /// <summary>
        /// kthen objektet njesi artikulli sipas idse
        /// </summary>
        ///<param name="idnjesia"> id e njesise se artikullit</param>
        ///<returns> nje objekt colNjesiteArtikulli qe permban nje koleksion me te gjithe njesite e artikujve me kete id</returns>
        internal DataRow merrNjesiArtikulli(int idnjesia)
        {

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDNJESIA", idnjesia, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_NJESIARTIKULLI_ktheNjesiArtikulliSipasId");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];

        }
        //[Obsolete("Perdor: DataRow ktheNjesiAritkulli(int idnjesia)", true)]
        //public colNjesiteArtikulli ktheNjesiAritkulli(int idnjesia)
        //{//metoda per te marre NJESINE SIPAS ID
        //    IDBManager dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        //    dbManager.ConnectionString = dbManager.GetConnectionString();
        //    try
        //    {
        //        dbManager.Open();
        //        dbManager.CreateParameters(1);
        //        dbManager.AddParameters(0, "@IDNJESIA", idnjesia , ParameterDirection.Input);
        //        DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_NJESIARTIKULLI_ktheNjesiArtikulliSipasId");
        //        colNjesiteArtikulli colNjesiteArtikulli = new colNjesiteArtikulli();
        //        return colNjesiteArtikulli.mbushArrayListNjesishArtikulli(ds);
        //    }
        //    catch (Exception)
        //    {
        //        return new colNjesiteArtikulli();
        //    }
        //    finally
        //    {
        //        dbManager.Dispose();
        //    }
        //}

        /// <summary>
        /// kthen objektet njesi artikulli sipas kodit
        /// </summary>
        ///<param name="kodnjesia"> kodin e njesise se artikullit</param>
        ///<returns> nje objekt colNjesiteArtikulli qe permban nje koleksion me te gjithe njesite e artikujve me kete kod</returns>
        internal DataRow merrNjesiArtikulliMeKod(string kodnjesia, int idNderm)
        {

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@KODNJESIA", kodnjesia, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idNderm, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_NJESIARTIKULLI_ktheNjesiArtikulliSipasKodit");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];

        }
        //[Obsolete("Perdor: DataTable ktheNjesiAritkulli(string  kodnjesia)", true)]
        //public colNjesiteArtikulli ktheNjesiAritkulli(string  kodnjesia)
        //{//metoda per te marre NJESINE SIPAS kodit
        //    IDBManager dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        //    dbManager.ConnectionString = dbManager.GetConnectionString();
        //    try
        //    {
        //        dbManager.Open();
        //        dbManager.CreateParameters(1);
        //        dbManager.AddParameters(0, "@KODNJESIA", kodnjesia, ParameterDirection.Input);
        //        DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_NJESIARTIKULLI_ktheNjesiArtikulliSipasKodit");
        //        colNjesiteArtikulli colNjesiteArtikulli = new colNjesiteArtikulli();
        //        return colNjesiteArtikulli.mbushArrayListNjesishArtikulli(ds);
        //    }
        //    catch (Exception)
        //    {
        //        return new colNjesiteArtikulli();
        //    }
        //    finally
        //    {
        //        dbManager.Dispose();
        //    }
        //}

        /// <summary>
        /// kthen objektet njesi artikulli sipas pershkrimit dhe idndermarjes
        /// </summary>
        ///<param name="idnderm"> id e ndermarjes</param>
        ///<param name="pershkrimnjesia"> pershkrimi i njesise</param>
        ///<returns> nje objekt colNjesiteArtikulli qe permban nje koleksion me te gjithe njesite e artikujve me kete pershkrim te kesaj ndermarje</returns>
        internal int ktheIdNjesiArtikulli(string pershkrimnjesia, int idnderm)
        {

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@PERSHKRIMNJESIA", pershkrimnjesia, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_NJESIARTIKULLI_ktheNjesiArtikulliSipasPershkrimit");

            if (ds == null)
                return -1;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return -1;
            int idNjesiArtikulli;
            int.TryParse(ds.Tables[0].Rows[0]["IDNJESIA"].ToString(), out idNjesiArtikulli);
            return idNjesiArtikulli;

        }


        /// <summary>
        /// kthen objektet njesi artikulli sipas kodit dhe idndermarjes
        /// </summary>
        ///<param name="idnderm"> id e ndermarjes</param>
        ///<param name="pershkrimnjesia"> pershkrimi i njesise</param>
        ///<returns> nje objekt colNjesiteArtikulli qe permban nje koleksion me te gjithe njesite e artikujve me kete kod te kesaj ndermarje</returns>
        internal int ktheIdNjesiArtikulliPerArtikull(string kodi, int idnderm)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@KODNJESIA", kodi, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            return Convert.ToInt32(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_NJESIARTIKULLI_ktheIdNjesiArtikulliSipasKodit"));

        }

        /// <summary>
        /// kthen objektet njesi artikulli sipas kodit dhe idndermarjes
        /// </summary>
        ///<param name="idnderm"> id e ndermarjes</param>
        ///<param name="pershkrimnjesia"> pershkrimi i njesise</param>
        ///<returns> nje objekt colNjesiteArtikulli qe permban nje koleksion me te gjithe njesite e artikujve me kete kod te kesaj ndermarje</returns>
        internal int ktheIdNjesiArtikulliSipasArtikulli(string kodi, int idnderm, int idArtikull)
        {
            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@KODNJESIA", kodi, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDARTIKULL", idArtikull, ParameterDirection.Input);
            int id = Convert.ToInt32(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_NJESIARTIKULLI_ktheIdNjesiArtikulliSipasKoditDheArtikullit"));
            return id;
        }

        /// <summary>
        /// kthen objektet njesi artikulli sipas pershkrimit dhe idndermarjes
        /// </summary>
        ///<param name="idnderm"> id e ndermarjes</param>
        ///<param name="pershkrimnjesia"> pershkrimi i njesise</param>
        ///<returns> nje objekt colNjesiteArtikulli qe permban nje koleksion me te gjithe njesite e artikujve me kete pershkrim te kesaj ndermarje</returns>
        internal DataRow merrNjesiArtikulliMePershk(string pershkrimnjesia, int idnderm)
        {

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@PERSHKRIMNJESIA", pershkrimnjesia, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_NJESIARTIKULLI_ktheNjesiArtikulliSipasPershkrimit");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];

        }
        //[Obsolete("Perdor: int ktheIdNjesiAritkulli(string pershkrimnjesia, int idnderm) ose DataRow ktheNjesiAritkulli(string pershkrimnjesia, int idnderm)", true)]
        //public colNjesiteArtikulli ktheNjesiAritkulli(string pershkrimnjesia, int idnderm)
        //{//metoda per te marre NJESINE SIPAS pershkrimit
        //    IDBManager dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        //    dbManager.ConnectionString = dbManager.GetConnectionString();
        //    try
        //    {
        //        dbManager.Open();
        //        dbManager.CreateParameters(2);
        //        dbManager.AddParameters(0, "@PERSHKRIMNJESIA", pershkrimnjesia, ParameterDirection.Input);
        //        dbManager.AddParameters(1, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
        //        DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_NJESIARTIKULLI_ktheNjesiArtikulliSipasPershkrimit");
        //        colNjesiteArtikulli colNjesiteArtikulli = new colNjesiteArtikulli();
        //        return colNjesiteArtikulli.mbushArrayListNjesishArtikulli(ds);
        //    }
        //    catch (Exception)
        //    {
        //        return new colNjesiteArtikulli();
        //    }
        //    finally
        //    {
        //        dbManager.Dispose();
        //    }
        //}

        /// <summary>
        /// perdoret per te kontrolluar nqs ekziston nje njesi artikulli me kete kod ne kete ndermarje
        /// behet kontrolli per qellime konsistence te informacionit ne DB, nuk lejon te celen njesi artikulli te ndryshem me te njejtin kod
        /// </summary>
        ///<param name="id"> id e ndermarjes</param>
        ///<param name="kod"> kodi i njesise se artikullit</param>
        /// <returns>nje objekt boolean qe tregon nese ekziston apo jo nje njesi artikulli me kete kod</returns>
        public bool ekzistonNjesiArtikulli(String kod, int id)
        {//kontrollon nqs ekziston nje Njesi artikulli me kete kod

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@KODNJESIA", kod, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", id, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_NJESIARTIKULLI_ekzistonNjesiArtikulli");
            if (ds.Tables[0].Rows.Count == 1)
                return true;
            else if (ds.Tables[0].Rows.Count == 0)
                return false;
            else return true;

        }

        /// <summary>
        /// perdoret per te kontrolluar nese kjo njesi artikulli eshte e lidhur me artikuj.
        /// kjo behet per te mos lejuar te fshihet nje njesi artikulli te lidhur me nje artikull
        /// </summary>
        /// <param name="id"> id e njesise se artikullit</param>
        /// <returns> nje objekt boolean qe tregon nese kjo njesi eshte e lidhur me nje artikull apo jo</returns>
        public bool kaVeprimeNjesiArtikulli(int id)
        {//kontrollon nqs ka veprime me kete njesi artikulli

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDNJESIA", id, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_NJESIARTIKULLI_kaVeprime");
            if (ds.Tables[0].Rows.Count == 1)
                return true;
            else if (ds.Tables[0].Rows.Count == 0)
                return false;
            else return true;

        }

        public DataTable merrNjesiArtikulliFature(int idDok)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@idDok", idDok, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_NJESIARTIKULLI_merrNjesiArtikulliSipasIdDok");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0)
                return null;
            return ds.Tables[0];
        }

        public DataTable merrNjesiArtikujshSipasIdve(List<int> idNjesiArtikuj)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@idNjesiArtikujsh", String.Join(",", idNjesiArtikuj), ParameterDirection.Input);
            return dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_NJESIARTIKULLI_merrNjesiteSipasIdve").Tables[0];
        }

        #endregion

        /// <summary>
        /// Metodat e meposhtme i perkasin veprimeve qe kryhen nga objektet clsKodifikimArtikulli dhe colKodifikimeArtikulli
        /// </summary>
        #region KODIFIKIM ARTIKULLI

        /// <summary>
        /// ekzekuton prc_T_KODIFIKIMARTIKULLI_ins ne DB dhe kthen statusin e perfundimit te ekzekutimit te SP-se perkatese
        /// Shiko clsMesazh brenda ketij moduli per informacion te metejshem mbi vleren e kthimit te kesaj metode
        /// <param name="idKodifikimi"> id ritese e kodifikimit</param>
        /// <param name="kodKodifikimi">kodi i kodifikimit</param>
        /// <param name="pershkrimKodifikimi">pershkrimi i kodifikimit</param>
        /// <param name="idPrindi">id e prindit</param>
        /// <param name="nivelKodifikimi"> niveli i kodifikimit</param>
        /// <param name="idPerdoruesi">id e perdoruesit qe ka kryer veprimin</param>
        /// <param name="idNderViti">id ndermarje viti</param>
        /// <param name="idNdermarje">id ndermarje</param>
        /// <param name="idSkemaKontabel">Id e skemes kontabel qe do te perdoret per grupin e artikujve afatgjate</param>
        /// <param name="idLlogariVlere">Id e llogarise se vleres qe do te perdoret per grupin e artikujve afatgjate</param>
        /// <param name="idLlogariShitje">Id e llogarise se shitjes qe do te perdoret per grupin e artikujve afatgjate</param>
        /// <param name="idLlogariInventari">Id e llogarise se inventarit qe do te perdoret per grupin e artikujve afatgjate</param>
        /// <param name="idLlogariShpenzimi">Id e llogarise se shpenzimeve qe do te perdoret per grupin e artikujve afatgjate</param>
        /// <param name="idLlogariNeProces">Id e llogarise ne proces qe do te perdoret per grupin e artikujve afatgjate</param>
        /// <param name="idLlogariAmortizimi">Id e llogarise se amortizimit qe do te perdoret per grupin e artikujve afatgjate</param>
        /// <param name="idFormatiSerial">Id e formatit te serialit qe do te perdoren per artikujt afatgjate te ketij grupi</param>
        /// <returns> nje objekt clsMesazh qe tregon nese ruajtja eshte kryer ne rregull apo jo.</returns>
        /// </summary>
        internal clsMesazh ruajKodifikimArtikulli(out int idKodifikimi, string kodKodifikimi, String pershkrimKodifikimi, int idPrindi, int nivelKodifikimi, int idPerdoruesi, int idNdermarje, int idstatusdok, int llojKodifikimi, int idSkemaKontabel, int idLlogariVlere, int idllogariShitje, int idLlogariInventari, int idLlogariShpenzimi, int idLlogariNeProces, int idLlogariAmortizimi, int idFormatiSerial, bool llojartikulli, int idllogaripakesim)
        {
            //metoda per ruajtjen e kodifikim se Artikullit
            idKodifikimi = -1;

            dbManager.Open();
            clsMesazh mesazh = new clsMesazh();
            //shtimi i parametrave

            dbManager.CreateParameters(19);
            dbManager.AddParameters(0, "@IDKODIFIKIMI", idKodifikimi, ParameterDirection.Output);
            dbManager.AddParameters(1, "@KODKODIFIKIMI", kodKodifikimi, ParameterDirection.Input);
            dbManager.AddParameters(2, "@PERSHKRIMKODIFIKIMI", pershkrimKodifikimi, ParameterDirection.Input);
            if (idPrindi == 0) dbManager.AddParameters(3, "@IDPRINDI", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(3, "@IDPRINDI", idPrindi, ParameterDirection.Input);
            dbManager.AddParameters(4, "@NIVELKODIFIKIMI", nivelKodifikimi, ParameterDirection.Input);
            dbManager.AddParameters(5, "@IDPERDORUESI", idPerdoruesi, ParameterDirection.Input);
            dbManager.AddParameters(6, "@IDNDERMARJE", idNdermarje, ParameterDirection.Input);
            dbManager.AddParameters(7, "@IDSTATUSDOK", idstatusdok, ParameterDirection.Input);
            dbManager.AddParameters(8, "@LLOJKODIFIKIMI", llojKodifikimi, ParameterDirection.Input);
            if (idSkemaKontabel < 1) dbManager.AddParameters(9, "@IDSKEMAKONTABEL", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(9, "@IDSKEMAKONTABEL", idSkemaKontabel, ParameterDirection.Input);
            if (idLlogariVlere < 1) dbManager.AddParameters(10, "@IDLLOGARIVLERE", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(10, "@IDLLOGARIVLERE", idLlogariVlere, ParameterDirection.Input);
            if (idllogariShitje < 1) dbManager.AddParameters(11, "@IDLLOGARISHITJE", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(11, "@IDLLOGARISHITJE", idllogariShitje, ParameterDirection.Input);
            if (idLlogariInventari < 1) dbManager.AddParameters(12, "@IDLLOGARIINVENTARI", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(12, "@IDLLOGARIINVENTARI", idLlogariInventari, ParameterDirection.Input);
            if (idLlogariShpenzimi < 1) dbManager.AddParameters(13, "@IDLLOGARISHPENZIMI", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(13, "@IDLLOGARISHPENZIMI", idLlogariShpenzimi, ParameterDirection.Input);
            if (idLlogariNeProces < 1) dbManager.AddParameters(14, "@IDLLOGARINEPROCES", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(14, "@IDLLOGARINEPROCES", idLlogariNeProces, ParameterDirection.Input);
            if (idLlogariAmortizimi < 1) dbManager.AddParameters(15, "@IDLLOGARIAMORTIZIMI", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(15, "@IDLLOGARIAMORTIZIMI", idLlogariAmortizimi, ParameterDirection.Input);
            if (idFormatiSerial < 1) dbManager.AddParameters(16, "@IDFORMATISERIALIT", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(16, "@IDFORMATISERIALIT", idFormatiSerial, ParameterDirection.Input);
            dbManager.AddParameters(17, "@LLOJARTIKULLI", llojartikulli, ParameterDirection.Input);
            if (idllogaripakesim < 1) dbManager.AddParameters(18, "@IDLLOGARIPAKESIM", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(18, "@IDLLOGARIPAKESIM", idllogaripakesim, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_KODIFIKIMARTIKULLI_ins");
            idKodifikimi = int.Parse(dbManager.Parameters[0].Value.ToString());
            mesazh = new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);

            return mesazh;

        }
        //[Obsolete("Perdor: clsMesazh ruajKodifikimArtikulli(int idKodifikimi, string kodKodifikimi, String pershkrimKodifikimi, int idPrindi, int nivelKodifikimi, int idPerdoruesi, int idNderViti, int idNdermarje)", true)]
        //public clsMesazh ruajKodifikimArtikulli(clsKodifikimArtikulli kodifikimArtikulli)
        //{ //metoda per ruajtjen e kodifikim se Artikullit
        //    IDBManager dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        //    dbManager.ConnectionString = dbManager.GetConnectionString();
        //    try
        //    {
        //        dbManager.Open();
        //        clsMesazh mesazh = new clsMesazh();
        //        //shtimi i parametrave

        //        dbManager.CreateParameters(8);
        //        dbManager.AddParameters(0, "@IDKODIFIKIMI", kodifikimArtikulli.IdKodifikimi, ParameterDirection.Output);
        //        dbManager.AddParameters(1, "@KODKODIFIKIMI", kodifikimArtikulli.KodKodifikimi, ParameterDirection.Input);
        //        dbManager.AddParameters(2, "@PERSHKRIMKODIFIKIMI", kodifikimArtikulli.PershkrimKodifikimi, ParameterDirection.Input);
        //        dbManager.AddParameters(3, "@IDPRINDI", kodifikimArtikulli.IdPrindi, ParameterDirection.Input);
        //        dbManager.AddParameters(4, "@NIVELKODIFIKIMI", kodifikimArtikulli.NivelKodifikimi, ParameterDirection.Input);
        //        dbManager.AddParameters(5, "@IDPERDORUESI", kodifikimArtikulli.IdPerdoruesi, ParameterDirection.Input);
        //        dbManager.AddParameters(6, "@IDNDERVITI", kodifikimArtikulli.IdNderViti, ParameterDirection.Input);
        //        dbManager.AddParameters(7, "@IDNDERMARJE", kodifikimArtikulli.IdNdermarje, ParameterDirection.Input);
        //        dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_KODIFIKIMARTIKULLI_ins");
        //        mesazh = new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);

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
        /// ekzekuton prc_T_KODIFIKIMARTIKULLI_upd ne DB dhe kthen statusin e perfundimit te ekzekutimit te SP-se perkatese
        /// Shiko clsMesazh brenda ketij moduli per informacion te metejshem mbi vleren e kthimit te kesaj metode
        /// <param name="idKodifikimi"> id ritese e kodifikimit</param>
        /// <param name="kodKodifikimi">kodi i kodifikimit</param>
        /// <param name="pershkrimKodifikimi">pershkrimi i kodifikimit</param>
        /// <param name="idPrindi">id e prindit</param>
        /// <param name="nivelKodifikimi"> niveli i kodifikimit</param>
        /// <param name="idPerdoruesi">id e perdoruesit qe ka kryer veprimin</param>
        /// <param name="idNderViti">id ndermarje viti</param>
        /// <param name="idNdermarje">id ndermarje</param>
        /// <param name="idSkemaKontabel">Id e skemes kontabel qe do te perdoret per grupin e artikujve afatgjate</param>
        /// <param name="idLlogariVlere">Id e llogarise se vleres qe do te perdoret per grupin e artikujve afatgjate</param>
        /// <param name="idLlogariShitje">Id e llogarise se shitjes qe do te perdoret per grupin e artikujve afatgjate</param>
        /// <param name="idLlogariInventari">Id e llogarise se inventarit qe do te perdoret per grupin e artikujve afatgjate</param>
        /// <param name="idLlogariShpenzimi">Id e llogarise se shpenzimeve qe do te perdoret per grupin e artikujve afatgjate</param>
        /// <param name="idLlogariNeProces">Id e llogarise ne proces qe do te perdoret per grupin e artikujve afatgjate</param>
        /// <param name="idLlogariAmortizimi">Id e llogarise se amortizimit qe do te perdoret per grupin e artikujve afatgjate</param>
        /// <param name="idFormatiSerial">Id e formatit te serialit qe do te perdoren per artikujt afatgjate te ketij grupi</param>
        /// <returns> nje objekt clsMesazh qe tregon nese modifikimi eshte kryer ne rregull apo jo.</returns>
        /// </summary>
        internal clsMesazh modifikoKodifikimArtikulli(int idKodifikimi, string kodKodifikimi, String pershkrimKodifikimi, int idPrindi, int nivelKodifikimi, int idPerdoruesi, int idNdermarje, int idstatusdok, int llojKodifikimi, int idSkemaKontabel, int idLlogariVlere, int idllogariShitje, int idLlogariInventari, int idLlogariShpenzimi, int idLlogariNeProces, int idLlogariAmortizimi, int idFormatiSerial, bool llojartikulli, int idllogpakesim)
        {
            //metoda per modifikimin e KodifikimArtikulli

            dbManager.Open();
            clsMesazh mesazh = new clsMesazh();
            //shtimi i parametrave

            dbManager.CreateParameters(19);
            dbManager.AddParameters(0, "@IDKODIFIKIMI", idKodifikimi, ParameterDirection.Input);
            dbManager.AddParameters(1, "@KODKODIFIKIMI", kodKodifikimi, ParameterDirection.Input);
            dbManager.AddParameters(2, "@PERSHKRIMKODIFIKIMI", pershkrimKodifikimi, ParameterDirection.Input);
            if (idPrindi == 0) dbManager.AddParameters(3, "@IDPRINDI", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(3, "@IDPRINDI", idPrindi, ParameterDirection.Input);
            dbManager.AddParameters(4, "@NIVELKODIFIKIMI", nivelKodifikimi, ParameterDirection.Input);
            dbManager.AddParameters(5, "@IDPERDORUESI", idPerdoruesi, ParameterDirection.Input);
            //dbManager.AddParameters(6, "@IDNDERVITI", idNderViti, ParameterDirection.Input);
            dbManager.AddParameters(6, "@IDNDERMARJE", idNdermarje, ParameterDirection.Input);
            dbManager.AddParameters(7, "@IDSTATUSDOK", idstatusdok, ParameterDirection.Input);
            dbManager.AddParameters(8, "@LLOJKODIFIKIMI", llojKodifikimi, ParameterDirection.Input);
            if (idSkemaKontabel < 1) dbManager.AddParameters(9, "@IDSKEMAKONTABEL", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(9, "@IDSKEMAKONTABEL", idSkemaKontabel, ParameterDirection.Input);
            if (idLlogariVlere < 1) dbManager.AddParameters(10, "@IDLLOGARIVLERE", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(10, "@IDLLOGARIVLERE", idLlogariVlere, ParameterDirection.Input);
            if (idllogariShitje < 1) dbManager.AddParameters(11, "@IDLLOGARISHITJE", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(11, "@IDLLOGARISHITJE", idllogariShitje, ParameterDirection.Input);
            if (idLlogariInventari < 1) dbManager.AddParameters(12, "@IDLLOGARIINVENTARI", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(12, "@IDLLOGARIINVENTARI", idLlogariInventari, ParameterDirection.Input);
            if (idLlogariShpenzimi < 1) dbManager.AddParameters(13, "@IDLLOGARISHPENZIMI", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(13, "@IDLLOGARISHPENZIMI", idLlogariShpenzimi, ParameterDirection.Input);
            if (idLlogariNeProces < 1) dbManager.AddParameters(14, "@IDLLOGARINEPROCES", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(14, "@IDLLOGARINEPROCES", idLlogariNeProces, ParameterDirection.Input);
            if (idLlogariAmortizimi < 1) dbManager.AddParameters(15, "@IDLLOGARIAMORTIZIMI", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(15, "@IDLLOGARIAMORTIZIMI", idLlogariAmortizimi, ParameterDirection.Input);
            if (idFormatiSerial < 1) dbManager.AddParameters(16, "@IDFORMATISERIALIT", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(16, "@IDFORMATISERIALIT", idFormatiSerial, ParameterDirection.Input);
            dbManager.AddParameters(17, "@LLOJARTIKULLI", llojartikulli, ParameterDirection.Input);
            if (idllogpakesim < 1) dbManager.AddParameters(18, "@IDLLOGARIPAKESIM", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(18, "@IDLLOGARIPAKESIM", idllogpakesim, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_KODIFIKIMARTIKULLI_upd");
            mesazh = new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);

            return mesazh;

        }
        //[Obsolete("Perdor: clsMesazh modifikoKodifikimArtikulli(int idKodifikimi, string kodKodifikimi, String pershkrimKodifikimi, int idPrindi, int nivelKodifikimi, int idPerdoruesi, int idNderViti, int idNdermarje)", true)]
        //public clsMesazh modifikoKodifikimArtikulli(clsKodifikimArtikulli kodifikimArtikulli)
        //{//metoda per modifikimin e KodifikimArtikulli
        //    IDBManager dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        //    dbManager.ConnectionString = dbManager.GetConnectionString();
        //    try
        //    {
        //        dbManager.Open();
        //        clsMesazh mesazh = new clsMesazh();
        //        //shtimi i parametrave

        //        dbManager.CreateParameters(8);
        //        dbManager.AddParameters(0, "@IDKODIFIKIMI", kodifikimArtikulli.IdKodifikimi, ParameterDirection.Input);
        //        dbManager.AddParameters(1, "@KODKODIFIKIMI", kodifikimArtikulli.KodKodifikimi, ParameterDirection.Input);
        //        dbManager.AddParameters(2, "@PERSHKRIMKODIFIKIMI", kodifikimArtikulli.PershkrimKodifikimi, ParameterDirection.Input);
        //        dbManager.AddParameters(3, "@IDPRINDI", kodifikimArtikulli.IdPrindi, ParameterDirection.Input);
        //        dbManager.AddParameters(4, "@NIVELKODIFIKIMI", kodifikimArtikulli.NivelKodifikimi, ParameterDirection.Input);
        //        dbManager.AddParameters(5, "@IDPERDORUESI", kodifikimArtikulli.IdPerdoruesi, ParameterDirection.Input);
        //        dbManager.AddParameters(6, "@IDNDERVITI", kodifikimArtikulli.IdNderViti, ParameterDirection.Input);
        //        dbManager.AddParameters(7, "@IDNDERMARJE", kodifikimArtikulli.IdNdermarje, ParameterDirection.Input);
        //        dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_KODIFIKIMARTIKULLI_upd");
        //        mesazh = new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);

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
        /// ekzekutohet sp-ja prc_T_KODIFIKIMARTIKULLI_del duke i kaluar id e kodifikimit te artikullit qe e marrim nga objekti clsKodifikimArtikulli qe i kalohet si parameter
        /// </summary>
        /// <param name="idKodifikimi"> id ritese e kodifikimit</param>
        /// <returns>nje objekt clsMesazh qe tregon nese fshirja eshte kryer ne rregull apo jo</returns>
        internal clsMesazh fshiKodifikimArtikulli(int idKodifikimi)
        {//metoda per fshirjen e KodifikimArtikulli

            dbManager.Open();
            //shtimi i parametrave
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDKODIFIKIMI", idKodifikimi, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_KODIFIKIMARTIKULLI_del");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
            return mesazh;

        }
        internal clsMesazh fshiKodifikimArtikulliStatus(int idKodifikimi, int idperdoruesi)
        {//metoda per fshirjen e KodifikimArtikulli

            dbManager.Open();
            //shtimi i parametrave
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDKODIFIKIMI", idKodifikimi, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUESI", idperdoruesi, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_KODIFIKIMARTIKULLI_upddel");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
            return mesazh;

        }
        //[Obsolete("Perdor: clsMesazh fshiKodifikimArtikulli(int idKodifikimi)", true)]
        //public clsMesazh fshiKodifikimArtikulli(clsKodifikimArtikulli kodifikimArtikulli)
        //{//metoda per fshirjen e KodifikimArtikulli
        //    IDBManager dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        //    dbManager.ConnectionString = dbManager.GetConnectionString();
        //    try
        //    {
        //        dbManager.Open();
        //        //shtimi i parametrave
        //        dbManager.CreateParameters(1);
        //        dbManager.AddParameters(0, "@IDKODIFIKIMI", kodifikimArtikulli.IdKodifikimi, ParameterDirection.Input);
        //        dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_KODIFIKIMARTIKULLI_del");
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
        /// kthen objektet kodifikimin e artikulli sipas idse
        /// </summary>
        /// <param name="idKodifikimi"> id ritese e kodifikimit</param>
        internal void merrKodifikimArtikulliPaKthim(int idKodifikimi)
        {// metoda per te marre nje KodifikimArtikulli NE BAZE TE ID

            dbManager.Open();
            //shtimi i parametrave
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDKODIFIKIMI", idKodifikimi, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_KODIFIKIMARTIKULLI_sel");

        }
        //[Obsolete("Perdor: merrKodifikimArtikulli(int idKodifikimi)", true)]
        //public void merrKodifikimArtikulli(clsKodifikimArtikulli kodifikimArtikulli)
        //{// metoda per te marre nje KodifikimArtikulli NE BAZE TE ID
        //    IDBManager dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        //    dbManager.ConnectionString = dbManager.GetConnectionString();
        //    try
        //    {
        //        dbManager.Open();
        //        //shtimi i parametrave
        //        dbManager.CreateParameters(1);
        //        dbManager.AddParameters(0, "@IDKODIFIKIMI", kodifikimArtikulli.IdKodifikimi, ParameterDirection.Input);
        //        dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_KODIFIKIMARTIKULLI_sel");
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
        /// kthen Datatable artikulli sipas id ndermarjes
        /// </summary>
        ///<param name="idndermarje">id e ndermarjes</param>
        ///<returns>nje datatable qe permban nje koleksion me te gjitha kodifikimet e artikullit te kesaj ndermarje </returns>
        internal DataTable ktheGjitheKodifikimetArtikulliSipasNdermarrjes(int idndermarje)
        {

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDNDERMARJE", idndermarje, ParameterDirection.Input);

            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KODIFIKIMARTIKULLI_merrSipasNdermarrjes");

            return ds.Tables[0];

        }

        /// <summary>
        /// kthen Datatable artikulli sipas id ndermarjes
        /// </summary>
        ///<param name="idndermarje">id e ndermarjes</param>
        ///<returns>nje datatable qe permban nje koleksion me te gjitha kodifikimet e artikullit te kesaj ndermarje </returns>
        internal DataTable ktheGjitheKodifikimetArtikulliSipasNdermarrjesExport(int idndermarje, int lloji, string emerTabKoka, string emerFusheID)
        {
            dbManager.Open();
            dbManager.CreateParameters(4);
            dbManager.AddParameters(0, "@IDNDERMARJE", idndermarje, ParameterDirection.Input);
            dbManager.AddParameters(1, "@LLOJI", lloji, ParameterDirection.Input);
            dbManager.AddParameters(2, "@EMERTABKOKA", emerTabKoka, ParameterDirection.Input);
            dbManager.AddParameters(3, "@EMERFUSHEID", emerFusheID, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KODIFIKIMARTIKULLI_DtExport");
            return ds.Tables[0];
        }


        internal DataTable merrKodifikimArtikulliSipasLlojit(int llojkodifikimi, int idndermarje, bool llojartikulli)
        {

            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@LLOJKODIFIKIMI", llojkodifikimi, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idndermarje, ParameterDirection.Input);
            dbManager.AddParameters(2, "@llojartikulli", llojartikulli, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KODIFIKIMARTIKULLI_ktheKodifikimArtikulliSipasLlojit");

            return ds.Tables[0];

        }

        internal DataTable MerrKodifikimArtikulliSipasLlojitAc(int llojkodifikimi, int idndermarje, bool llojartikulli, string kodi)
        {
            dbManager.Open();
            dbManager.CreateParameters(4);
            dbManager.AddParameters(0, "@LLOJKODIFIKIMI", llojkodifikimi, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idndermarje, ParameterDirection.Input);
            dbManager.AddParameters(2, "@LLOJARTIKULLI", llojartikulli, ParameterDirection.Input);
            dbManager.AddParameters(3, "@KODI", kodi, ParameterDirection.Input);

            return dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KODIFIKIMARTIKULLI_MerrKodifikimArtikulliSipasLlojitAc").Tables[0];
        }

        internal DataTable merrKodifikimArtikulliSipasLlojitDheNivelKodifikimit(int llojkodifikimi, int idndermarje, bool llojartikulli, int nivelKodifikimi)
        {


            dbManager.Open();
            dbManager.CreateParameters(4);
            dbManager.AddParameters(0, "@LLOJKODIFIKIMI", llojkodifikimi, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idndermarje, ParameterDirection.Input);
            dbManager.AddParameters(2, "@llojartikulli", llojartikulli, ParameterDirection.Input);
            dbManager.AddParameters(3, "@NivelKodifikimi", llojartikulli, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KODIFIKIMARTIKULLI_ktheKodifikimArtikulliSipasLlojitDheNivelKodifikimit");

            return ds.Tables[0];

        }
        internal DataTable merrKodifikimArtikulliSipasLlojKodifikimit(int llojkodifikimi, int idndermarje)
        {


            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@LLOJKODIFIKIMI", llojkodifikimi, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idndermarje, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KODIFIKIMARTIKULLI_ktheKodifikimArtikulliSipasLlojitKodifikimit");

            return ds.Tables[0];

        }

        internal DataTable merrKodifikimArtikulliSipasLlojKodDheNivelKodifikimit(int llojkodifikimi, int idndermarje, int nivelKodifikimi)
        {
            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@LLOJKODIFIKIMI", llojkodifikimi, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idndermarje, ParameterDirection.Input);
            dbManager.AddParameters(2, "@NivelKodifikimi", nivelKodifikimi, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KODIFIKIMARTIKULLI_ktheKodifikimArtikulliSipasLlojDheNivelKodifikimit");

            return ds.Tables[0];

        }
        internal DataTable merrKodifikimArtikulliSipasLlojitDt(int llojkodifikimi, int idndermarje, bool llojartikulli)
        {


            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@LLOJKODIFIKIMI", llojkodifikimi, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idndermarje, ParameterDirection.Input);
            dbManager.AddParameters(2, "@llojartikulli", llojartikulli, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KODIFIKIMARTIKULLI_ktheKodifikimArtikulliSipasLlojitDt");

            return ds.Tables[0];

        }
        //[Obsolete("Perdor: DataTable KtheGjitheKodifikimetArtikulliSipasNdermarrjes(int idndermarje)", true)]
        //public colKodifikimeArtikulli  merrGjitheKodifikimetArtikulliSipasNdermarrjes(int idndermarje)
        //{//metoda per te marre te gjithe njesite
        //    IDBManager dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        //    dbManager.ConnectionString = dbManager.GetConnectionString();
        //    try
        //    {
        //        dbManager.Open();
        //        dbManager.CreateParameters(1);
        //        dbManager.AddParameters(0, "@IDNDERMARJE", idndermarje, ParameterDirection.Input);

        //        DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KODIFIKIMARTIKULLI_merrSipasNdermarrjes");
        //        colKodifikimeArtikulli colKodifikimeArtikulli = new colKodifikimeArtikulli();
        //        return colKodifikimeArtikulli.mbushArrayListKodifikimeshArtikulli(ds);
        //    }
        //    catch (Exception)
        //    {
        //        return new colKodifikimeArtikulli();
        //    }
        //    finally
        //    {
        //        dbManager.Dispose();
        //    }
        //}

        /// <summary>
        /// kthen datatable kodifikim artikulli sipas id ndermarjes te cilat nuk jane prind
        /// </summary>
        ///<param name="idndermarje">id e ndermarjes</param>
        ///<returns>nje datatable qe permban nje koleksion me te gjitha kodifikimet e artikullit te kesaj ndermarje qe nuk jane prind </returns>
        internal DataTable ktheGjitheKodifikimetArtikulliSipasNdermarrjesJoPrind(int idndermarje, bool llojartikulli)
        {

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDNDERMARJE", idndermarje, ParameterDirection.Input);
            dbManager.AddParameters(1, "@llojartikulli", llojartikulli, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KODIFIKIMARTIKULLI_merrSipasNdermarrjesJoPrind");

            return ds.Tables[0];

        }

        internal DataTable mbushGjitheKodifikimetArtikulliSipasNdermarrjesJoPrindDheLlojit(int idndermarje, int llojkodifikimi, bool llojartikulli)
        {

            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@IDNDERMARJE", idndermarje, ParameterDirection.Input);
            dbManager.AddParameters(1, "@LLOJKODIFIKIMI", llojkodifikimi, ParameterDirection.Input);
            dbManager.AddParameters(2, "@llojartikulli", llojartikulli, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KODIFIKIMARTIKULLI_merrSipasNdermarrjesJoPrindDheLloji");

            return ds.Tables[0];

        }
        internal DataTable mbushGjitheKodifikimetArtikulliSipasNdermarrjesLlojitNiveli1(int idndermarje, int llojkodifikimi, bool llojartikulli)
        {

            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@IDNDERMARJE", idndermarje, ParameterDirection.Input);
            dbManager.AddParameters(1, "@LLOJKODIFIKIMI", llojkodifikimi, ParameterDirection.Input);
            dbManager.AddParameters(2, "@llojartikulli", llojartikulli, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KODIFIKIMARTIKULLI_merrSipasNdermarrjesLlojiNiveli1");

            return ds.Tables[0];

        }
        //[Obsolete("Perdor: DataTable ktheGjitheKodifikimetArtikulliSipasNdermarrjesJoPrind(int idndermarje)", true)]
        //public colKodifikimeArtikulli merrGjitheKodifikimetArtikulliSipasNdermarrjesJoPrind(int idndermarje)
        //{//metoda per te marre te gjithe njesite
        //    IDBManager dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        //    dbManager.ConnectionString = dbManager.GetConnectionString();
        //    try
        //    {
        //        dbManager.Open();
        //        dbManager.CreateParameters(1);
        //        dbManager.AddParameters(0, "@IDNDERMARJE", idndermarje, ParameterDirection.Input);

        //        DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KODIFIKIMARTIKULLI_merrSipasNdermarrjesJoPrind");
        //        colKodifikimeArtikulli colKodifikimeArtikulli = new colKodifikimeArtikulli();
        //        return colKodifikimeArtikulli.mbushArrayListKodifikimeshArtikulli(ds);
        //    }
        //    catch (Exception)
        //    {
        //        return new colKodifikimeArtikulli();
        //    }
        //    finally
        //    {
        //        dbManager.Dispose();
        //    }
        //}

        /// <summary>
        /// kthen datarow kodifikim artikulli sipas idse
        /// </summary>
        ///<param name="idkodifikimi"> id e kodifikimit te artikullit</param>
        ///<returns> nje datarow qe permban kodifikimin e artikullit sipas id se kodifikimit</returns>
        internal DataRow merrKodifikimArtikulli(int idkodifikimi)
        {

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDKODIFIKIMI", idkodifikimi, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KODIFIKIMARTIKULLI_ktheKodifikimArtikulliSipasId");

            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];


        }
        //[Obsolete("Perdor: DataRow merrKodifikimArtikulli(int idkodifikimi)", true)]
        //public colKodifikimeArtikulli ktheKodifikimArtikulli(int idkodifikimi)
        //{//metoda per te marre KODIFIKIMIN SIPAS ID
        //    IDBManager dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        //    dbManager.ConnectionString = dbManager.GetConnectionString();
        //    try
        //    {
        //        dbManager.Open();
        //        dbManager.CreateParameters(1);
        //        dbManager.AddParameters(0, "@IDKODIFIKIMI", idkodifikimi , ParameterDirection.Input);
        //        DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KODIFIKIMARTIKULLI_ktheKodifikimArtikulliSipasId");
        //        colKodifikimeArtikulli colKodifikimeArtikulli = new colKodifikimeArtikulli();
        //        return colKodifikimeArtikulli.mbushArrayListKodifikimeshArtikulli(ds);
        //    }
        //    catch (Exception)
        //    {
        //        return new colKodifikimeArtikulli();
        //    }
        //    finally
        //    {
        //        dbManager.Dispose();
        //    }
        //}



        internal DataRow merrKodifikimArtikulliKodLloj(string kodkodifikimi, int idndermarje, int llojkod, bool llojartikulli)
        {
            dbManager.Open();
            dbManager.CreateParameters(4);
            dbManager.AddParameters(0, "@KODKODIFIKIMI", kodkodifikimi, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idndermarje, ParameterDirection.Input);
            dbManager.AddParameters(2, "@LLOJKODIFIKIMI", llojkod, ParameterDirection.Input);
            dbManager.AddParameters(3, "@llojartikulli", llojartikulli, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KODIFIKIMARTIKULLI_ktheKodifikimArtikulliSipasKoditLloj");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];

        }


        internal DataRow merrKodifikimArtikulliKod(string kodkodifikimi, int idndermarje, int llojkod)
        {

            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@KODKODIFIKIMI", kodkodifikimi, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idndermarje, ParameterDirection.Input);
            dbManager.AddParameters(2, "@LLOJKODIFIKIMI", llojkod, ParameterDirection.Input);

            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KODIFIKIMARTIKULLI_ktheKodifikimArtikulliSipasKoditPaLloj");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];

        }


        //[Obsolete("Perdor: DataRow ktheKodifikimArtikulli(string kodkodifikimi, int idndermarje)", true)]
        //public colKodifikimeArtikulli ktheKodifikimArtikulli(string kodkodifikimi, int idndermarje)
        //{//metoda per te marre KODIFIKIMIN SIPAS kodit
        //    IDBManager dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        //    dbManager.ConnectionString = dbManager.GetConnectionString();
        //    try
        //    {
        //        dbManager.Open();
        //        dbManager.CreateParameters(2);
        //        dbManager.AddParameters(0, "@KODKODIFIKIMI", kodkodifikimi , ParameterDirection.Input);
        //        dbManager.AddParameters(1, "@IDNDERMARJE",  idndermarje , ParameterDirection.Input);
        //        DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KODIFIKIMARTIKULLI_ktheKodifikimArtikulliSipasKodit");
        //        colKodifikimeArtikulli colKodifikimeArtikulli = new colKodifikimeArtikulli();
        //        return colKodifikimeArtikulli.mbushArrayListKodifikimeshArtikulli(ds);
        //    }
        //    catch (Exception)
        //    {
        //        return new colKodifikimeArtikulli();
        //    }
        //    finally
        //    {
        //        dbManager.Dispose();
        //    }
        //}

        /// <summary>
        /// kthen datatable kodifikim artikulli sipas prindit
        /// </summary>
        ///<param name="idprindi"> id e kodifikimit prind te artikullit</param>
        ///<returns> nje datatable qe permban nje koleksion me te gjithe kodifikimet e artikujve me kete id prindi</returns>
        internal DataTable ktheKodifikimArtikulliSipasPrindit(int idprindi)
        {

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDPRINDI", idprindi, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KODIFIKIMARTIKULLI_ktheKodifikimArtikulliSipasPrindit");

            return ds.Tables[0];

        }

        internal DataTable ktheKodifikimArtikulliSipasPershkrimit(string pershkrimi, int idndermarrje)
        {

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@PERSHKRIMI", pershkrimi, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idndermarrje, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KODIFIKIMARTIKULLI_ktheKodifikimArtikulliSipasPershkrimit");

            return ds.Tables[0];

        }

        internal DataTable mbushKodifikimArtikulliSipasPrinditDheLlojit(int idprindi, int llojkodifikimartikulli)
        {

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDPRINDI", idprindi, ParameterDirection.Input);
            dbManager.AddParameters(1, "@LLOJKODIFIKIMI", llojkodifikimartikulli, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KODIFIKIMARTIKULLI_ktheKodifikimArtikulliSipasPrinditDheLlojit");

            return ds.Tables[0];

        }

        internal DataTable ktheBijte(int idKodifikim, int idNdermarje)
        {

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDKODIFIKIMI", idKodifikim, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idNdermarje, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KODIFIKIMARTIKULLI_ktheBij");

            return ds.Tables[0];

        }
        //[Obsolete("Perdor: DataTable ktheKodifikimArtikulliSipasPrindit(int idprindi)", true)]
        //public colKodifikimeArtikulli merrKodifikimArtikulliSipasPrindit(int idprindi)
        //{//metoda per te marre KODIFIKIMIN SIPAS prindit
        //    IDBManager dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        //    dbManager.ConnectionString = dbManager.GetConnectionString();
        //    try
        //    {
        //        dbManager.Open();
        //        dbManager.CreateParameters(1);
        //        dbManager.AddParameters(0, "@IDPRINDI", idprindi, ParameterDirection.Input);
        //        DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KODIFIKIMARTIKULLI_ktheKodifikimArtikulliSipasPrindit");
        //        colKodifikimeArtikulli colKodifikimeArtikulli = new colKodifikimeArtikulli();
        //        return colKodifikimeArtikulli.mbushArrayListKodifikimeshArtikulli(ds);
        //    }
        //    catch (Exception)
        //    {
        //        return new colKodifikimeArtikulli();
        //    }
        //    finally
        //    {
        //        dbManager.Dispose();
        //    }
        //}

        public DataTable mbushLlojetKodifikime()
        {

            dbManager.Open();
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KODIFIKIMARTIKULLI_merrLlojKodifikimi");

            return ds.Tables[0];

        }

        /// <summary>
        /// perdoret per te kontrolluar nese ky kodifikim artikulli eshte e lidhur me artikuj.
        /// kjo behet per te mos lejuar te fshihet nje kodifikim artikulli te lidhur me nje artikull
        /// </summary>
        /// <param name="id"> id e kodifikimit te artikullit</param>
        /// <returns> nje objekt boolean qe tregon nese ky kodifikim eshte e lidhur me nje artikull apo jo</returns>
        public bool kaVeprimeKodifikimArtikulli(int id)
        {//kontrollon nqs ka veprime me kete kodifikim artikulli

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDKODIFIKIMI", id, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KODIFIKIMARTIKULLI_kaVeprime");
            if (ds.Tables[0].Rows.Count == 1)
                return true;
            else if (ds.Tables[0].Rows.Count == 0)
                return false;
            else return true;

        }
        public bool kaVeprimeKodifikimArtikulliPaStandart(int id)
        {//kontrollon nqs ka veprime me kete kodifikim artikulli           
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDKODIFIKIMI", id, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KODIFIKIMARTIKULLI_kaVeprimePaStandart");
            if (ds.Tables[0].Rows.Count == 1)
                return true;
            else if (ds.Tables[0].Rows.Count == 0)
                return false;
            else return true;

        }

        /// <summary>
        /// perdoret per te treguar nese ky kodifikim eshte prind i ndonje kodifikimi tjeter apo jo
        /// </summary>
        /// <param name="idKodifikimi"> id ritese e kodifikimit</param>
        /// <param name="idNdermarje">id ndermarje</param>
        /// <returns> nje objekt clsMesazh qe tregon nese ky kodifikim eshte prind apo jo</returns>
        [Obsolete("perdor: eshtePrind(idKodifikmi)", false)]
        public clsMesazh eshtePrind(int idKodifikimi, int idNdermarje)
        {
            int nrbijsh = 0;

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDKODIFIKIMI", idKodifikimi, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idNdermarje, ParameterDirection.Input);
            nrbijsh = Convert.ToInt32(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_KODIFIKIMARTIKULLI_eshtePrind"));
            if (nrbijsh > 0)
            {
                return new clsMesazh(true, "Kodifikimi nuk mund te zgjidhet sepse eshte prind");
            }
            else
            {
                return new clsMesazh(false);
            }

        }
        internal bool eshtePrind(int idKodifikimi)
        {
            int nrbijsh = 0;

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDKODIFIKIMI", idKodifikimi, ParameterDirection.Input);
            nrbijsh = Convert.ToInt32(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_KODIFIKIMARTIKULLI_eshtePrindNew"));
            if (nrbijsh == 0)
                return false;
            return true;

        }
        public bool ekzistonKodifikimArtikulli(int idKodifikimi)
        {

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDKODIFIKIMI", idKodifikimi, ParameterDirection.Input);
            int pergjigje = Convert.ToInt32(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_KODIFIKIMARTIKULLI_ekzistonKodifikimArti"));
            return Convert.ToBoolean(pergjigje);

        }

        public bool ekzistonKodifikimArtikulliKodLloj(string kodkodifikimi, int idndermarje, int llojkod)
        {

            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@KODKODIFIKIMI", kodkodifikimi, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idndermarje, ParameterDirection.Input);
            dbManager.AddParameters(2, "@LLOJKODIFIKIMI", llojkod, ParameterDirection.Input);
            int pergjigje = Convert.ToInt32(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_KODIFIKIMARTIKULLI_ekzistonKodifikimArtiSipasKodLloj"));
            return Convert.ToBoolean(pergjigje);

        }

        public int ktheIdKodifikimArtikulliKodLloj(string kodkodifikimi, int idndermarje, int llojkod, bool llojartikulli)
        {

            dbManager.Open();
            dbManager.CreateParameters(4);
            dbManager.AddParameters(0, "@KODKODIFIKIMI", kodkodifikimi, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idndermarje, ParameterDirection.Input);
            dbManager.AddParameters(2, "@LLOJKODIFIKIMI", llojkod, ParameterDirection.Input);
            dbManager.AddParameters(3, "@llojartikulli", llojartikulli, ParameterDirection.Input);
            int idja = Convert.ToInt32(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_KODIFIKIMARTIKULLI_ktheIdKodifikimArtiSipasKodLloj"));
            return idja;

        }

        public string ktheKodKodifikimArtikulliSipasId(int idKodifikimi)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDKODIFIKIMI", idKodifikimi, ParameterDirection.Input);
            return dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_KODIFIKIMARTIKULLI_ktheKodKodifikimArtiSipasId").ToString();
        }

        public bool eshteTransferuarTekBij(string kodkodifikimi, int idndermarje, int llojkod)
        {

            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@KODKODIFIKIMI", kodkodifikimi, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idndermarje, ParameterDirection.Input);
            dbManager.AddParameters(2, "@LLOJKODIFIKIMI", llojkod, ParameterDirection.Input);
            int pergjigje = Convert.ToInt32(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_KODIFIKIMARTIKULLI_eshteTransferuarTekBij"));
            return Convert.ToBoolean(pergjigje);

        }

        internal int MerrIdKodifikimiSipasKodit(string kodKodifikimi, int idNdermarrje)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@KODKODIFIKIMI", kodKodifikimi, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idNdermarrje, ParameterDirection.Input);
            return Convert.ToInt32(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_KODIFIKIMARTIKULLI_ktheKodifikimArtikulliSipasKodit"));
        }

        #endregion

        /// <summary>
        /// Metodat e meposhtme i perkasin veprimeve qe kryhen nga objektet clsTrupiKategoriZbritje dhe colTrupatKategoriteZbritjes
        /// </summary>
        #region TRUPI KATEGORI ZBRITJE

        /// <summary>
        /// ekzekuton prc_T_TRUPIKATEGORIZBRITJE_ins ne DB dhe kthen statusin e perfundimit te ekzekutimit te SP-se perkatese
        /// Shiko clsMesazh brenda ketij moduli per informacion te metejshem mbi vleren e kthimit te kesaj metode
        /// <param name="idTrupiKategoriZbritje"> id ritese e trupit te kategori zbritje</param>
        /// <param name="idKokaKategoriZbritje"> id e kokes</param>
        /// <param name="dateFillimi"> data e fillimit</param>
        /// <param name="dateMbarimi"> data e mbarimit</param>
        /// <param name="vleraMin"> vlera minimale</param>
        /// <param name="vleraMax"> vlera maksimale</param>
        /// <param name="lloji"> lloji</param>
        /// <param name="zbritja"> zbritja</param>
        /// <param name="idPerdoruesi"> id e perdoruesit qe ka kryer veprimin</param>
        /// <param name="idNderViti"> id e ndermarje vitit</param>
        /// <param name="prioriteti"> prioriteti</param>
        /// <returns> nje objekt clsMesazh qe tregon nese ruajtja eshte kryer ne rregull apo jo.</returns>
        /// </summary>
        internal clsMesazh ruajTrupiKategoriZbritje(out int idTrupiKategoriZbritje, int idKokaKategoriZbritje, DateTime dateFillimi, DateTime dateMbarimi, decimal vleraMin,
            decimal vleraMax, int lloji, decimal zbritja, int idPerdoruesi, int prioriteti)
        { //metoda per ruajtjen e TRUPIT KategoriZbritje
            idTrupiKategoriZbritje = -1;

            dbManager.Open();
            clsMesazh mesazh = new clsMesazh();
            //shtimi i parametrave

            dbManager.CreateParameters(10);
            dbManager.AddParameters(0, "@IDTRUPIKATEGORIZBRITJE", idTrupiKategoriZbritje, ParameterDirection.Output);
            dbManager.AddParameters(1, "@IDKOKAKATEGORIZBRITJE", idKokaKategoriZbritje, ParameterDirection.Input);
            dbManager.AddParameters(2, "@DATEFILLIMI", dateFillimi, ParameterDirection.Input);
            dbManager.AddParameters(3, "@DATEMBARIMI", dateMbarimi, ParameterDirection.Input);
            dbManager.AddParameters(4, "@VLERAMIN", vleraMin, ParameterDirection.Input);
            dbManager.AddParameters(5, "@VLERAMAX", vleraMax, ParameterDirection.Input);
            dbManager.AddParameters(6, "@LLOJI", lloji, ParameterDirection.Input);
            dbManager.AddParameters(7, "@ZBRITJA", zbritja, ParameterDirection.Input);
            dbManager.AddParameters(8, "@IDPERDORUESI", idPerdoruesi, ParameterDirection.Input);
            //dbManager.AddParameters(9, "@IDNDERVITI", idNderViti, ParameterDirection.Input);
            dbManager.AddParameters(9, "@PRIORITETI", prioriteti, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_TRUPIKATEGORIZBRITJE_ins");
            mesazh = new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);

            return mesazh;



        }
        //[Obsolete("Perdor: clsMesazh ruajTrupiKategoriZbritje(int idTrupiKategoriZbritje, int idKokaKategoriZbritje, DateTime dateFillimi, DateTime dateMbarimi, decimal vleraMin, " +
        //    "decimal vleraMax, int lloji, decimal zbritja, int idPerdoruesi, int idNderViti, int prioriteti)",true)]
        //public clsMesazh ruajTrupiKategoriZbritje(clsTrupiKategoriZbritje kategoriZbritje)
        //{ //metoda per ruajtjen e TRUPIT KategoriZbritje
        //        try
        //    {
        //            clsMesazh mesazh = new clsMesazh();
        //        //shtimi i parametrave

        //        dbManager.CreateParameters(11);
        //        dbManager.AddParameters(0, "@IDTRUPIKATEGORIZBRITJE", kategoriZbritje.IdTrupiKategoriZbritje , ParameterDirection.Output);
        //        dbManager.AddParameters(1, "@IDKOKAKATEGORIZBRITJE", kategoriZbritje.IdKokaKategoriZbritje, ParameterDirection.Input);
        //            dbManager.AddParameters(2, "@DATEFILLIMI", kategoriZbritje.DateFillimi, ParameterDirection.Input);
        //        dbManager.AddParameters(3, "@DATEMBARIMI", kategoriZbritje.DateMbarimi, ParameterDirection.Input);
        //        dbManager.AddParameters(4, "@VLERAMIN", kategoriZbritje.VleraMin, ParameterDirection.Input);
        //        dbManager.AddParameters(5, "@VLERAMAX", kategoriZbritje.VleraMax, ParameterDirection.Input);
        //        dbManager.AddParameters(6, "@LLOJI", kategoriZbritje.Lloji, ParameterDirection.Input);
        //        dbManager.AddParameters(7, "@ZBRITJA", kategoriZbritje.Zbritja, ParameterDirection.Input);
        //        dbManager.AddParameters(8, "@IDPERDORUESI", kategoriZbritje.IdPerdoruesi, ParameterDirection.Input);
        //        dbManager.AddParameters(9, "@IDNDERVITI", kategoriZbritje.IdNderViti, ParameterDirection.Input);
        //        dbManager.AddParameters(10, "@PRIORITETI", kategoriZbritje.Prioriteti, ParameterDirection.Input);
        //        dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_TRUPIKATEGORIZBRITJE_ins");
        //        mesazh = new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);

        //        return mesazh;
        //    }
        //    catch (Exception)
        //    {
        //        return new clsMesazh(false, ce.Message);
        //    }


        //}

        /// <summary>
        /// ekzekuton prc_T_TRUPIKATEGORIZBRITJE_upd ne DB dhe kthen statusin e perfundimit te ekzekutimit te SP-se perkatese
        /// Shiko clsMesazh brenda ketij moduli per informacion te metejshem mbi vleren e kthimit te kesaj metode
        /// <param name="idTrupiKategoriZbritje"> id ritese e trupit te kategori zbritje</param>
        /// <param name="idKokaKategoriZbritje"> id e kokes</param>
        /// <param name="dateFillimi"> data e fillimit</param>
        /// <param name="dateMbarimi"> data e mbarimit</param>
        /// <param name="vleraMin"> vlera minimale</param>
        /// <param name="vleraMax"> vlera maksimale</param>
        /// <param name="lloji"> lloji</param>
        /// <param name="zbritja"> zbritja</param>
        /// <param name="idPerdoruesi"> id e perdoruesit qe ka kryer veprimin</param>
        /// <param name="idNderViti"> id e ndermarje vitit</param>
        /// <param name="prioriteti"> prioriteti</param>
        /// <returns> nje objekt clsMesazh qe tregon nese modifikimi eshte kryer ne rregull apo jo.</returns>
        /// </summary>
        internal clsMesazh modifikoTrupiKategoriZbritje(int idTrupiKategoriZbritje, int idKokaKategoriZbritje, DateTime dateFillimi, DateTime dateMbarimi, decimal vleraMin,
            decimal vleraMax, int lloji, decimal zbritja, int idPerdoruesi, int prioriteti)
        {//metoda per modifikimin e TRUPIT KategoriZbritje

            dbManager.Open();
            clsMesazh mesazh = new clsMesazh();
            //shtimi i parametrave

            dbManager.CreateParameters(10);
            dbManager.AddParameters(0, "@IDTRUPIKATEGORIZBRITJE", idTrupiKategoriZbritje, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDKOKAKATEGORIZBRITJE", idKokaKategoriZbritje, ParameterDirection.Input);
            dbManager.AddParameters(2, "@DATEFILLIMI", dateFillimi, ParameterDirection.Input);
            dbManager.AddParameters(3, "@DATEMBARIMI", dateMbarimi, ParameterDirection.Input);
            dbManager.AddParameters(4, "@VLERAMIN", vleraMin, ParameterDirection.Input);
            dbManager.AddParameters(5, "@VLERAMAX", vleraMax, ParameterDirection.Input);
            dbManager.AddParameters(6, "@LLOJI", lloji, ParameterDirection.Input);
            dbManager.AddParameters(7, "@ZBRITJA", zbritja, ParameterDirection.Input);
            dbManager.AddParameters(8, "@IDPERDORUESI", idPerdoruesi, ParameterDirection.Input);
            //dbManager.AddParameters(9, "@IDNDERVITI", idNderViti, ParameterDirection.Input);
            dbManager.AddParameters(9, "@PRIORITETI", prioriteti, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_TRUPIKATEGORIZBRITJE_upd");
            mesazh = new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);

            return mesazh;


        }
        //[Obsolete("Perdor: clsMesazh modifikoTrupiKategoriZbritje(int idTrupiKategoriZbritje, int idKokaKategoriZbritje, DateTime dateFillimi, DateTime dateMbarimi, decimal vleraMin, " +
        //    "decimal vleraMax, int lloji, decimal zbritja, int idPerdoruesi, int idNderViti, int prioriteti)",true)]
        //public clsMesazh modifikoTrupiKategoriZbritje(clsTrupiKategoriZbritje kategoriZbritje)
        //{//metoda per modifikimin e TRUPIT KategoriZbritje
        //        try
        //    {
        //            clsMesazh mesazh = new clsMesazh();
        //        //shtimi i parametrave

        //        dbManager.CreateParameters(11);
        //        dbManager.AddParameters(0, "@IDTRUPIKATEGORIZBRITJE", kategoriZbritje.IdTrupiKategoriZbritje, ParameterDirection.Input);
        //        dbManager.AddParameters(1, "@IDKOKAKATEGORIZBRITJE", kategoriZbritje.IdKokaKategoriZbritje, ParameterDirection.Input);
        //        dbManager.AddParameters(2, "@DATEFILLIMI", kategoriZbritje.DateFillimi, ParameterDirection.Input);
        //        dbManager.AddParameters(3, "@DATEMBARIMI", kategoriZbritje.DateMbarimi, ParameterDirection.Input);
        //        dbManager.AddParameters(4, "@VLERAMIN", kategoriZbritje.VleraMin, ParameterDirection.Input);
        //        dbManager.AddParameters(5, "@VLERAMAX", kategoriZbritje.VleraMax, ParameterDirection.Input);
        //        dbManager.AddParameters(6, "@LLOJI", kategoriZbritje.Lloji, ParameterDirection.Input);
        //        dbManager.AddParameters(7, "@ZBRITJA", kategoriZbritje.Zbritja, ParameterDirection.Input);
        //        dbManager.AddParameters(8, "@IDPERDORUESI", kategoriZbritje.IdPerdoruesi, ParameterDirection.Input);
        //        dbManager.AddParameters(9, "@IDNDERVITI", kategoriZbritje.IdNderViti, ParameterDirection.Input);
        //        dbManager.AddParameters(10, "@PRIORITETI", kategoriZbritje.Prioriteti, ParameterDirection.Input);
        //        dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_TRUPIKATEGORIZBRITJE_upd");
        //        mesazh = new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);

        //        return mesazh;
        //    }
        //    catch (Exception)
        //    {
        //        return new clsMesazh(false, ce.Message);
        //    }


        //}

        /// <summary>
        /// ekzekutohet sp-ja prc_T_TRUPIKATEGORIZBRITJE_del duke i kaluar id e trupit te kategori zbritje qe e marrim nga objekti clsTrupiKategoriZbritje qe i kalohet si parameter
        /// </summary>
        /// <param name="idTrupiKategoriZbritje"> id ritese e trupit te kategori zbritje</param>
        /// <returns>nje objekt clsMesazh qe tregon nese fshirja eshte kryer ne rregull apo jo</returns>
        internal clsMesazh fshiTrupiKategoriZbritje(int idTrupiKategoriZbritje)
        {//metoda per fshirjen e TRUPIT KategoriZbritje

            dbManager.Open();
            //shtimi i parametrave
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDTRUPIKATEGORIZBRITJE", idTrupiKategoriZbritje, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_TRUPIKATEGORIZBRITJE_del");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
            return mesazh;


        }
        //[Obsolete("Perdor: clsMesazh fshiTrupiKategoriZbritje(int idTrupiKategoriZbritje)",true)]
        //public clsMesazh fshiTrupiKategoriZbritje(clsTrupiKategoriZbritje kategoriZbritje)
        //{//metoda per fshirjen e TRUPIT KategoriZbritje
        //    try
        //    {
        //            //shtimi i parametrave
        //        dbManager.CreateParameters(1);
        //        dbManager.AddParameters(0, "@IDTRUPIKATEGORIZBRITJE", kategoriZbritje.IdTrupiKategoriZbritje , ParameterDirection.Input);
        //        dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_TRUPIKATEGORIZBRITJE_del");
        //        clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
        //        return mesazh;
        //    }
        //    catch (Exception)
        //    {
        //        return new clsMesazh(false, ce.Message);
        //    }

        //}

        /// <summary>
        /// kthen objektet trupi kategori zbritje sipas idse
        /// </summary>
        ///<param name="idTrupiKategoriZbritje">trupi kategori zbritje qe do i merret id</param>
        internal void merrTrupiKategoriZbritjePaKthim(int idTrupiKategoriZbritje)
        {// metoda per te marre nje KategoriZbritje NE BAZE TE ID

            dbManager.Open();
            //shtimi i parametrave
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDTRUPIKATEGORIZBRITJE", idTrupiKategoriZbritje, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_TRUPIKATEGORIZBRITJE_sel");

        }
        //[Obsolete("Perdor: merrTrupiKategoriZbritje(int idTrupiKategoriZbritje)",true)]
        //public void merrTrupiKategoriZbritje(clsTrupiKategoriZbritje kategoriZbritje)
        //{// metoda per te marre nje KategoriZbritje NE BAZE TE ID
        //    IDBManager dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        //    dbManager.ConnectionString = dbManager.GetConnectionString();
        //    try
        //    {
        //        dbManager.Open();
        //        //shtimi i parametrave
        //        dbManager.CreateParameters(1);
        //        dbManager.AddParameters(0, "@IDTRUPIKATEGORIZBRITJE", kategoriZbritje.IdTrupiKategoriZbritje , ParameterDirection.Input);
        //        dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_TRUPIKATEGORIZBRITJE_sel");
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
        /// kthen datatable kategori zbritje sipas id ndermarje vitit
        /// </summary>
        ///<param name="idnderviti">id e ndermarje vitit</param>
        ///<returns>nje datatable qe permban nje koleksion me te gjitha trupat e kategori zbritjes te kesaj ndermarje viti </returns>
        internal DataTable ktheGjitheTrupatKategoriZbritjeSipasNdermarrjes(int idnderviti)
        {

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDNDERVITI", idnderviti, ParameterDirection.Input);

            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_TRUPIKATEGORIZBRITJE_merrSipasNdermarrjes");

            return ds.Tables[0];

        }
        //[Obsolete("Perdor: DataTable ktheGjitheTrupatKategoriZbritjeSipasNdermarrjes(int idnderviti)", true)]
        //public colTrupatKategoriteZbritjes merrGjitheTrupatKategoriZbritjeSipasNdermarrjes(int idnderviti)
        //{//metoda per te marre te gjithe TRUPAT KategoriZbritje
        //    IDBManager dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        //    dbManager.ConnectionString = dbManager.GetConnectionString();
        //    try
        //    {
        //        dbManager.Open();
        //        dbManager.CreateParameters(1);
        //        dbManager.AddParameters(0, "@IDNDERVITI", idnderviti, ParameterDirection.Input);

        //        DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_TRUPIKATEGORIZBRITJE_merrSipasNdermarrjes");
        //        colTrupatKategoriteZbritjes colKategoriteZbritjes = new colTrupatKategoriteZbritjes();
        //        return colKategoriteZbritjes.mbushArrayListTrupashKategorishZbritje(ds);
        //    }
        //    catch (Exception)
        //    {
        //        return new colTrupatKategoriteZbritjes();
        //    }
        //    finally
        //    {
        //        dbManager.Dispose();
        //    }
        //}

        /// <summary>
        /// kthen datarow trupi kategori zbritje sipas idse
        /// </summary>
        ///<param name="idkatzbritje"> id e trupit te kategori zbritjes</param>
        ///<returns> nje datarow qe permban trupin kategori zbritje me kete id</returns>
        internal DataRow merrTrupiKategoriZbritje(int idkatzbritje)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDTRUPIKATEGORIZBRITJE", idkatzbritje, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_TRUPIKATEGORIZBRITJE_ktheKategoriZbrijteSipasId");


            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];

        }
        //[Obsolete("Perdor: DataRow merrTrupiKategoriZbritje(int idkatzbritje)", true)]
        //public colTrupatKategoriteZbritjes ktheTrupiKategoriZbritje(int idkatzbritje)
        //{//metoda per te marre TRUPI KategoriZbritje SIPAS ID
        //    IDBManager dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        //    dbManager.ConnectionString = dbManager.GetConnectionString();
        //    try
        //    {
        //        dbManager.Open();
        //        dbManager.CreateParameters(1);
        //        dbManager.AddParameters(0, "@IDTRUPIKATEGORIZBRITJE", idkatzbritje, ParameterDirection.Input);
        //        DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_TRUPIKATEGORIZBRITJE_ktheKategoriZbrijteSipasId");
        //        colTrupatKategoriteZbritjes colKategoriteZbritjes = new colTrupatKategoriteZbritjes();
        //        return colKategoriteZbritjes.mbushArrayListTrupashKategorishZbritje(ds);
        //    }
        //    catch (Exception)
        //    {
        //        return new colTrupatKategoriteZbritjes();
        //    }
        //    finally
        //    {
        //        dbManager.Dispose();
        //    }
        //}

        /// <summary>
        /// kthen datatable  trupi kategori zbritje sipas idse se kokes
        /// </summary>
        ///<param name="idKokaKategoriZbritje"> id e kokes te kategori zbritjes</param>
        ///<returns> nje datatable qe permban nje koleksion me te gjithe trupat kategori zbritje me kete id koke</returns>
        internal DataTable ktheTrupatKategoriZbritjeSipasKokes(int idKokaKategoriZbritje)
        {

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDKOKAKATEGORIZBRITJE", idKokaKategoriZbritje, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_TRUPIKATEGORIZBRITJE_ktheKategoriZbritjeSipasPrindit");
            return ds.Tables[0];

        }
        //[Obsolete("Perdor: DataTable ktheTrupatKategoriZbritjeSipasKokes(int idKokaKategoriZbritje)", true)]
        //public colTrupatKategoriteZbritjes merrTrupatKategoriZbritjeSipasKokes(int idKokaKategoriZbritje)
        //{//metoda per te marre TRUPAT KategoriZbritje SIPAS kokes
        //    IDBManager dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        //    dbManager.ConnectionString = dbManager.GetConnectionString();
        //    try
        //    {
        //        dbManager.Open();
        //        dbManager.CreateParameters(1);
        //        dbManager.AddParameters(0, "@IDKOKAKATEGORIZBRITJE", idKokaKategoriZbritje , ParameterDirection.Input);
        //        DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_TRUPIKATEGORIZBRITJE_ktheKategoriZbritjeSipasPrindit");
        //        colTrupatKategoriteZbritjes colKategoriteZbritjes = new colTrupatKategoriteZbritjes();
        //        return colKategoriteZbritjes.mbushArrayListTrupashKategorishZbritje(ds);
        //    }
        //    catch (Exception)
        //    {
        //        return new colTrupatKategoriteZbritjes();
        //    }
        //    finally
        //    {
        //        dbManager.Dispose();
        //    }
        //}

        #endregion

        /// <summary>
        /// Metodat e meposhtme i perkasin veprimeve qe kryhen nga objektet clsKokaKategoriZbritje dhe colKokatKategoriteZbritjes
        /// </summary>
        #region KOKA KATEGORI ZBRITJE

        /// <summary>
        /// ekzekuton prc_T_KOKAKATEGORIZBRITJE_ins ne DB dhe kthen statusin e perfundimit te ekzekutimit te SP-se perkatese
        /// Shiko clsMesazh brenda ketij moduli per informacion te metejshem mbi vleren e kthimit te kesaj metode
        /// <param name="idKokaKategoriZbritje"> id ritese e kokes se kategorise zbritje</param>
        /// <param name="kodKategoriZbritje"> kod kategori zbritje</param>
        /// <param name="pershkrimKategoriZbritje"> pershkrim kategori zbritje</param>
        /// <param name="idPerdoruesi"> id e perdoruesit qe ka kryer veprimin</param>
        /// <param name="idNderViti"> id e ndermarje vitit</param>
        /// <param name="zbritja"> zbritja</param>
        /// <param name="idnderm"> id e ndermarjes</param>
        /// <returns> nje objekt boolean qe tregon nese ruajtja eshte kryer ne rregull apo jo.</returns>
        /// </summary>
        internal int ruajKokaKategoriZbritje(int idKokaKategoriZbritje, string kodKategoriZbritje, string pershkrimKategoriZbritje, int idPerdoruesi, decimal zbritja, int idnderm, int idstatusdok, int idmonedha)
        {//ruajtja e kokamakro

            dbManager.Open();
            dbManager.CreateParameters(8);
            dbManager.AddParameters(0, "@IDKOKAKATEGORIZBRITJE", idKokaKategoriZbritje, ParameterDirection.Output);
            dbManager.AddParameters(1, "@KODKATEGORIZBRITJE", kodKategoriZbritje, ParameterDirection.Input);
            dbManager.AddParameters(2, "@PERSHKRIMKATEGORIZBRITJE", pershkrimKategoriZbritje, ParameterDirection.Input);
            dbManager.AddParameters(3, "@IDPERDORUESI", idPerdoruesi, ParameterDirection.Input);
            //dbManager.AddParameters(4, "@IDNDERVITI", idNderViti, ParameterDirection.Input);
            dbManager.AddParameters(4, "@ZBRITJA", zbritja, ParameterDirection.Input);
            dbManager.AddParameters(5, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            dbManager.AddParameters(6, "@IDSTATUSDOK", idstatusdok, ParameterDirection.Input);
            dbManager.AddParameters(7, "@IDMONEDHA", idmonedha, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_KOKAKATEGORIZBRITJE_ins");
            idKokaKategoriZbritje = int.Parse(dbManager.Parameters[0].Value.ToString());
            return idKokaKategoriZbritje;


        }
        //[Obsolete("Perdor: clsMesazh ruajKokaKategoriZbritje(int idKokaKategoriZbritje,string kodKategoriZbritje, string pershkrimKategoriZbritje, int idPerdoruesi, int idNderViti, decimal zbritja, int idnderm)",true)]
        //public clsMesazh ruajKokaKategoriZbritje(clsKokaKategoriZbritje  kategorizbritje)
        //{//ruajtja e kokamakro
        //    try
        //    {
        //        dbManager.CreateParameters(7);
        //        dbManager.AddParameters(0, "@IDKOKAKATEGORIZBRITJE", kategorizbritje.IdKokaKategoriZbritje, ParameterDirection.Output);
        //        dbManager.AddParameters(1, "@KODKATEGORIZBRITJE", kategorizbritje.KodKategoriZbritje, ParameterDirection.Input);
        //        dbManager.AddParameters(2, "@PERSHKRIMKATEGORIZBRITJE", kategorizbritje.PershkrimKategoriZbritje, ParameterDirection.Input);
        //        dbManager.AddParameters(3, "@IDPERDORUESI", kategorizbritje.IdPerdoruesi, ParameterDirection.Input);
        //        dbManager.AddParameters(4, "@IDNDERVITI", kategorizbritje.IdNderViti, ParameterDirection.Input);
        //        dbManager.AddParameters(5, "@ZBRITJA", kategorizbritje.Zbritja, ParameterDirection.Input);
        //        dbManager.AddParameters(6, "@IDNDERMARJE", kategorizbritje.IdNdermarje, ParameterDirection.Input);
        //        dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_KOKAKATEGORIZBRITJE_ins");
        //        kategorizbritje.IdKokaKategoriZbritje = int.Parse(dbManager.Parameters[0].Value.ToString());
        //        clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
        //        return mesazh;
        //    }
        //    catch (Exception)
        //    {
        //        return new clsMesazh(false, ce.Message);
        //    }

        //}

        /// <summary>
        /// ekzekuton prc_T_KOKAKATEGORIZBRITJE_upd ne DB dhe kthen statusin e perfundimit te ekzekutimit te SP-se perkatese
        /// Shiko clsMesazh brenda ketij moduli per informacion te metejshem mbi vleren e kthimit te kesaj metode
        /// </summary>
        /// <param name="idKokaKategoriZbritje"> id ritese e kokes se kategorise zbritje</param>
        /// <param name="kodKategoriZbritje"> kod kategori zbritje</param>
        /// <param name="pershkrimKategoriZbritje"> pershkrim kategori zbritje</param>
        /// <param name="idPerdoruesi"> id e perdoruesit qe ka kryer veprimin</param>
        /// <param name="idNderViti"> id e ndermarje vitit</param>
        /// <param name="zbritja"> zbritja</param>
        /// <param name="idnderm"> id e ndermarjes</param>
        /// <returns> nje objekt clsMesazh qe tregon nese modifikimi eshte kryer ne rregull apo jo.</returns>

        internal clsMesazh modifikoKokaKategoriZbritje(int idKokaKategoriZbritje, string kodKategoriZbritje, string pershkrimKategoriZbritje, int idPerdoruesi, decimal zbritja, int idnderm, int idstatusdok, int idmonedha)
        {//modifikimi i kokamakro

            dbManager.Open();
            dbManager.CreateParameters(8);
            dbManager.AddParameters(0, "@IDKOKAKATEGORIZBRITJE", idKokaKategoriZbritje, ParameterDirection.Input);
            dbManager.AddParameters(1, "@KODKATEGORIZBRITJE", kodKategoriZbritje, ParameterDirection.Input);
            dbManager.AddParameters(2, "@PERSHKRIMKATEGORIZBRITJE", pershkrimKategoriZbritje, ParameterDirection.Input);
            dbManager.AddParameters(3, "@IDPERDORUESI", idPerdoruesi, ParameterDirection.Input);
            //dbManager.AddParameters(4, "@IDNDERVITI", idNderViti, ParameterDirection.Input);
            dbManager.AddParameters(4, "@ZBRITJA", zbritja, ParameterDirection.Input);
            dbManager.AddParameters(5, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            dbManager.AddParameters(6, "@IDSTATUSDOK", idstatusdok, ParameterDirection.Input);
            dbManager.AddParameters(7, "@IDMONEDHA", idmonedha, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_KOKAKATEGORIZBRITJE_upd");
            idKokaKategoriZbritje = int.Parse(dbManager.Parameters[0].Value.ToString());
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
            return mesazh;

        }
        //[Obsolete("Perdor: clsMesazh modifikoKokaKategoriZbritje(int idKokaKategoriZbritje,string kodKategoriZbritje, string pershkrimKategoriZbritje, int idPerdoruesi, int idNderViti, decimal zbritja, int idnderm)",true)]
        //public clsMesazh modifikoKokaKategoriZbritje(clsKokaKategoriZbritje kategorizbritje)
        //{//modifikimi i kokamakro
        //    try
        //    {
        //        dbManager.CreateParameters(6);
        //        dbManager.AddParameters(0, "@IDKOKAKATEGORIZBRITJE", kategorizbritje.IdKokaKategoriZbritje, ParameterDirection.Input);
        //        dbManager.AddParameters(1, "@KODKATEGORIZBRITJE", kategorizbritje.KodKategoriZbritje, ParameterDirection.Input);
        //        dbManager.AddParameters(2, "@PERSHKRIMKATEGORIZBRITJE", kategorizbritje.PershkrimKategoriZbritje, ParameterDirection.Input);
        //        dbManager.AddParameters(3, "@IDPERDORUESI", kategorizbritje.IdPerdoruesi, ParameterDirection.Input);
        //        dbManager.AddParameters(4, "@IDNDERVITI", kategorizbritje.IdNderViti, ParameterDirection.Input);
        //        dbManager.AddParameters(5, "@ZBRITJA", kategorizbritje.Zbritja , ParameterDirection.Input);
        //        dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_KOKAKATEGORIZBRITJE_upd");
        //        kategorizbritje.IdKokaKategoriZbritje = int.Parse(dbManager.Parameters[0].Value.ToString());
        //        clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
        //        return mesazh;
        //    }
        //    catch (Exception)
        //    {
        //        return new clsMesazh(false, ce.Message);
        //    }
        //}

        /// <summary>
        /// ekzekutohet sp-ja prc_T_KOKAKATEGORIZBRITJE_del duke i kaluar id e kokes te kategori zbritje qe e marrim nga objekti clsKokaKategoriZbritje qe i kalohet si parameter
        /// </summary>
        /// <param name="idKokaKategoriZbritje"> id ritese e kokes se kategorise zbritje</param>
        /// <returns>nje objekt boolean qe tregon nese fshirja eshte kryer ne rregull apo jo</returns>
        internal clsMesazh fshiKokaKategoriZbritje(int idKokaKategoriZbritje)
        {//fshirja e KokaKategoriZbritje

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDKOKAKATEGORIZBRITJE", idKokaKategoriZbritje, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_KOKAKATEGORIZBRITJE_del");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
            return mesazh;

        }
        internal clsMesazh fshiKokaKategoriZbritjeStatus(int idKokaKategoriZbritje, int idperdoruesi)
        {//fshirja e KokaKategoriZbritje

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDKOKAKATEGORIZBRITJE", idKokaKategoriZbritje, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUESI", idperdoruesi, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_KOKAKATEGORIZBRITJE_upddel");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
            return mesazh;

        }
        //[Obsolete("Perdor: clsMesazh fshiKokaKategoriZbritje(int idKokaKategoriZbritje)",true)]
        //public clsMesazh fshiKokaKategoriZbritje(clsKokaKategoriZbritje kategorizbritje)
        //{//fshirja e KokaKategoriZbritje
        //    try
        //    {
        //        dbManager.CreateParameters(1);
        //        dbManager.AddParameters(0, "@IDKOKAKATEGORIZBRITJE", kategorizbritje.IdKokaKategoriZbritje, ParameterDirection.Input);
        //        dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_KOKAKATEGORIZBRITJE_del");
        //        clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
        //        return mesazh;
        //    }
        //    catch (Exception)
        //    {
        //        return new clsMesazh(false, ce.Message);
        //    }
        //}

        ///// <summary>
        ///// fshin nje objekt kategori zbritje sebashku me trupin
        ///// Nje objekt kategori zbritje ka nje koleksion me trupin , 
        ///// fshirja e nje kategori zbritje imponon fshirjen edhe te nje colection-i me trupin
        ///// Mqs cdo rresht i ri qe fshihet ne DB kerkon thirrjen e nje SP-je me parametra dhe kategori zbritje bashke me trupin konsiderohet si nje regjistrim,
        ///// perdoret nje transaksion qe imponon fshirjen e rregullt te nje kategori zbritje sebashku me trupin
        ///// </summary>
        ///// <param name="idKokaKategoriZbritje"> id ritese e kokes se kategorise zbritje</param>
        ///// <returns> kthen nje obj clsMesazh per te identifikuar statusin e fshirjes se te dhenave ne DB</returns>
        //[Obsolete("Perdor nga klasa perkatese: clsMesazh fshiKategoriZbritje(int idKokaKategoriZbritje)", true)]
        //internal clsMesazh fshiKategoriZbritje(int idKokaKategoriZbritje)
        //{//fshin kategorizbritje

        //    //colTrupatKategoriteZbritjes  trupat = merrTrupatKategoriZbritjeSipasKokes(kategorizbritje.IdKokaKategoriZbritje);
        //    colTrupatKategoriteZbritjes trupat = new colTrupatKategoriteZbritjes();
        //    trupat.mbushTrupatKategoriZbritjeSipasKokes(idKokaKategoriZbritje);
        //    if (dbManager == null)
        //    {
        //        dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        //        dbManager.ConnectionString = dbManager.GetConnectionString();
        //    }
        //    dbManager.Open();
        //    dbManager.BeginTransaction();
        //    clsMesazh mesazh = new clsMesazh(true);
        //    try
        //    {
        //        foreach (clsTrupiKategoriZbritje  o in trupat)
        //        {
        //            if (mesazh.Status)
        //            {
        //                mesazh = fshiTrupiKategoriZbritje(o.IdTrupiKategoriZbritje);
        //                //mesazh = fshiTrupiKategoriZbritje(o);
        //            }
        //            else
        //            {
        //                dbManager.Transaction.Rollback();
        //                    return mesazh;
        //            }
        //        }
        //        if (mesazh.Status)
        //        {
        //            mesazh = fshiKokaKategoriZbritje(idKokaKategoriZbritje);
        //            //mesazh =  fshiKokaKategoriZbritje(kategorizbritje);
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
        //[Obsolete("Perdor: clsMesazh fshiKategoriZbritje(int idKokaKategoriZbritje) ",true)]
        //public clsMesazh fshiKategoriZbritje(clsKokaKategoriZbritje kategorizbritje)
        //{//fshin kategorizbritje

        //    //colTrupatKategoriteZbritjes  trupat = merrTrupatKategoriZbritjeSipasKokes(kategorizbritje.IdKokaKategoriZbritje);
        //    colTrupatKategoriteZbritjes trupat = new colTrupatKategoriteZbritjes();
        //    trupat.mbushTrupatKategoriZbritjeSipasKokes(kategorizbritje.IdKokaKategoriZbritje);
        //    dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        //    dbManager.ConnectionString = dbManager.GetConnectionString();
        //    dbManager.Open();
        //    dbManager.BeginTransaction();
        //    clsMesazh mesazh = new clsMesazh(true);
        //    try
        //    {
        //        foreach (clsTrupiKategoriZbritje  o in trupat)
        //        {
        //            if (mesazh.Status)
        //            {
        //                //mesazh = fshiTrupiKategoriZbritje(o.IdTrupiKategoriZbritje);
        //                mesazh = fshiTrupiKategoriZbritje(o);
        //            }
        //            else
        //            {
        //                dbManager.Transaction.Rollback();
        //                    return mesazh;
        //            }
        //        }
        //        if (mesazh.Status)
        //        {
        //            mesazh =  fshiKokaKategoriZbritje(kategorizbritje.IdKokaKategoriZbritje);
        //            //mesazh =  fshiKokaKategoriZbritje(kategorizbritje);
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
        //}

        /// <summary>
        /// kthen objektet koka kategori zbritje sipas idse
        /// </summary>
        /// <param name="idKokaKategoriZbritje"> id ritese e kokes se kategorise zbritje</param>
        internal void merrKokaKategoriZbritjePaKthim(int idKokaKategoriZbritje)
        {// metoda per te marre nje KokaKategoriZbritje ne baze te id te tij

            dbManager.Open();
            //shtimi i parametrave
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDKOKAKATEGORIZBRITJE", idKokaKategoriZbritje, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_KOKAKATEGORIZBRITJE_sel");


        }
        //[Obsolete("Perdor: merrKokaKategoriZbritje(int idKokaKategoriZbritje)",true)]
        //public void merrKokaKategoriZbritje(clsKokaKategoriZbritje kategorizbritje)
        //{// metoda per te marre nje KokaKategoriZbritje ne baze te id te tij
        //    IDBManager dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        //    dbManager.ConnectionString = dbManager.GetConnectionString();
        //    try
        //    {
        //        dbManager.Open();
        //        //shtimi i parametrave
        //        dbManager.CreateParameters(1);
        //        dbManager.AddParameters(0, "@IDKOKAKATEGORIZBRITJE", kategorizbritje.IdKokaKategoriZbritje, ParameterDirection.Input);
        //        dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_KOKAKATEGORIZBRITJE_sel");

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
        /// kthen datatable koka kategori zbritje sipas id ndermarjes
        /// </summary>
        ///<param name="idnderm">id e ndermarjes</param>
        ///<returns>nje datatable qe permban nje koleksion me te gjitha kokat e kategori zbritjes te kesaj ndermarje  </returns>
        internal DataTable ktheKokaKategoriZbritjeSipasNdermarrjes(int idnderm)
        {

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KOKAKATEGORIZBRITJE_merrSipasNdermarrjes");

            return ds.Tables[0];

        }
        //[Obsolete("Perdor: DataTable ktheKokaKategoriZbritjeSipasNdermarrjes(int idnderm)", true)]
        //public colKokatKategoriteZbritjes merrKokaKategoriZbritjeSipasNdermarrjes(int idnderm)
        //{//metoda per te marre te gjithe koka kategori zbritje e nje ndermarje 
        //    IDBManager dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        //    dbManager.ConnectionString = dbManager.GetConnectionString();
        //    try
        //    {
        //        dbManager.Open();
        //        dbManager.CreateParameters(1);
        //        dbManager.AddParameters(0, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
        //        DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KOKAKATEGORIZBRITJE_merrSipasNdermarrjes");
        //        colKokatKategoriteZbritjes colKokatKategoriteZbritjes = new colKokatKategoriteZbritjes();
        //        return colKokatKategoriteZbritjes.mbushArrayListKokashKategorishZbritje(ds);
        //    }
        //    catch (Exception)
        //    {
        //        return new colKokatKategoriteZbritjes();
        //    }
        //    finally
        //    {
        //        dbManager.Dispose();
        //    }

        //}

        /// <summary>
        /// kthen datarow  koka kategori zbritje sipas idse
        /// </summary>
        ///<param name="id"> id e kokes te kategori zbritjes</param>
        ///<returns> nje datarow qe permban koken kategori zbritje sipas id-se</returns>
        internal DataRow merrKokaKategoriZbritje(int id)
        {

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDKOKAKATEGORIZBRITJE", id, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KOKAKATEGORIZBRITJE_ktheKategoriZbritjeSipasId");

            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];


        }
        //[Obsolete("Perdor: DataRow merrKokaKategoriZbritje(int id)", true)]
        //public colKokatKategoriteZbritjes ktheKokaKategoriZbritje(int id)
        //{//kthen KokaKategoriZbritje sipas id
        //    IDBManager dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        //    dbManager.ConnectionString = dbManager.GetConnectionString();
        //    try
        //    {
        //        dbManager.Open();
        //        dbManager.CreateParameters(1);
        //        dbManager.AddParameters(0, "@IDKOKAKATEGORIZBRITJE", id, ParameterDirection.Input);
        //        DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KOKAKATEGORIZBRITJE_ktheKategoriZbritjeSipasId");
        //        colKokatKategoriteZbritjes colKokatKategoriteZbritjes = new colKokatKategoriteZbritjes();
        //        return colKokatKategoriteZbritjes.mbushArrayListKokashKategorishZbritje(ds);

        //    }
        //    catch (Exception)
        //    {
        //        return new colKokatKategoriteZbritjes();
        //    }
        //    finally
        //    {
        //        dbManager.Dispose();
        //    }

        //}

        /// <summary>
        /// kthen datarow  koka kategori zbritje sipas kodit
        /// </summary>
        ///<param name="kod"> kodi i kokes te kategori zbritjes</param>
        ///<param name="idnder"> id e ndermarrjes</param>
        ///<returns> nje datarow qe permban koken e kategorise se zbritjes me kete kod dhe id ndermarrje</returns>
        internal int merrKokaKategoriZbritje(string kod, int idnder)
        {

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@KODKATEGORIZBRITJE", kod, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idnder, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KOKAKATEGORIZBRITJE_ktheKategoriZbritjeSipasKodit");

            if (ds == null)
                return -1;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return -1;
            int idKokaKategZbritje;
            int.TryParse(ds.Tables[0].Rows[0]["IDKOKAKATEGORIZBRITJE"].ToString(), out idKokaKategZbritje);
            return idKokaKategZbritje;


        }


        /// <summary>
        /// perdoret per te kontrolluar nqs ekziston nje kategori zbritje me kete kod ne kete ndermarje
        /// behet kontrolli per qellime konsistence te informacionit ne DB, nuk lejon te celen kategori zbritje te ndryshem me te njejtin kod
        /// </summary>
        ///<param name="idndermarje"> id e ndermarjes</param>
        ///<param name="kod"> kodi i kategorise se zbritjes</param>
        /// <returns>nje objekt boolean qe tregon nese ekziston apo jo nje kategori zbritje me kete kod</returns>
        public bool ekzistonKategoriZbritje(String kod, int idndermarje)
        {//kontrollon nqs ekziston nje KategoriZbritje me kete kod

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@KODKATEGORIZBRITJE", kod, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idndermarje, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KOKAKATEGORIZBRITJE_ekzistonKategoriZbritje");
            if (ds.Tables[0].Rows.Count == 1)
                return true;
            else if (ds.Tables[0].Rows.Count == 0)
                return false;
            else return true;

        }


        /// <summary>
        /// perdoret per te kontrolluar nqs ekziston nje kategori zbritje me kete vlere zbritje ne kete ndermarje
        /// behet kontrolli per qellime konsistence te informacionit ne DB, nuk lejon te celen kategori zbritje te ndryshem me te njejtin vlere zbritje
        /// </summary>
        /// <param name="idndermarje"> id e ndermarjes</param>
        /// <param name="zbritja"> vlera e zbritjes</param>
        /// <returns>nje objekt boolean qe tregon nese ekziston apo jo nje kategori zbritje me kete vlere</returns>
        /// <param name="idmonedha"></param>
        public bool ekzistonVlereZbritje(Decimal zbritja, int idndermarje, int idmonedha)
        {//kontrollon nqs ekziston nje KategoriZbritje me kete vlere

            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@VLEREKATEGORIZBRITJE", zbritja, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idndermarje, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDMONEDHA", idmonedha, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KOKAKATEGORIZBRITJE_ekzistonVleraZbritje");
            if (ds.Tables[0].Rows.Count == 1)
                return true;
            else if (ds.Tables[0].Rows.Count == 0)
                return false;
            else return true;

        }


        /// <summary>
        /// perdoret per te kontrolluar nese kjo kategori zbritje eshte e lidhur me klient furnitor.
        /// kjo behet per te mos lejuar te fshihet nje kategori zbritje te lidhur me nje klient furnitor
        /// </summary>
        /// <param name="id"> id e kategorise se zbritjes</param>
        /// <returns> nje objekt boolean qe tregon nese kjo kategori zbritje eshte e lidhur me nje klient furnitor apo jo</returns>
        public bool kaVeprimeKategoriZbritje(int id)
        {

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters("@IDKOKAKATEGORIZBRITJE", id, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KOKAKATEGORIZBRITJE_kaVeprime");
            return ds.Tables[0].Rows.Count != 0;

        }
        internal DataRow merrSipasKategoriNdermarrjesDR(int idnderm, int idperdorues, int idkategori)
        {


            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDKOKAKATEGORIZBRITJE", idkategori, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KOKAKATEGORIZBRITJE_merrSipasKategoriNdermarrjesDR");

            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];

        }
        internal DataTable merrSipasKategoriteNdermarrjesDT(int idnderm, int idperdorues)
        {


            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KOKAKATEGORIZBRITJE_merrSipasKategoriteNdermarrjesDT");

            return ds.Tables[0];

        }

        #endregion

        /// <summary>
        /// Metodat e meposhtme i perkasin veprimeve qe kryhen nga objektet clsNivelZbritje dhe colNiveleZbritjesh
        /// </summary>
        #region NIVEL ZBRITJE

        /// <summary>
        /// ekzekuton prc_T_NIVELZBRITJE_ins ne DB dhe kthen statusin e perfundimit te ekzekutimit te SP-se perkatese
        /// procedura krijon edhe nr tjeter automatik nqs niveli i zbritjes eshte i lidhur me nr automatik
        /// Shiko clsMesazh brenda ketij moduli per informacion te metejshem mbi vleren e kthimit te kesaj metode
        /// <param name="idNivelZbritje"> id ritese e nivelit te zbritjes</param>
        /// <param name="kodNivelZbritje"> kodi i nivelit te zbritjes</param>
        /// <param name="pershkrimNivelZbritje"> pershkrimi i nivelit te zbritjes</param>
        /// <param name="idPrindi"> id e prindit</param>
        /// <param name="prioritetiNivelZbritje"> prioriteti i nivelit te zbritjes</param>
        /// <param name="idPerdoruesi"> id e perdoruesit qe ka kryer veprimin</param>
        /// <param name="idNderViti"> id e ndermarje vitit</param>
        /// <param name="idnderm"> id e ndermarjes</param>
        /// <param name="idndermarje">id e ndermarjes</param>
        /// <returns> nje objekt clsMesazh qe tregon nese ruajtja eshte kryer ne rregull apo jo.</returns>
        /// </summary>
        internal bool ruajNivZbritje(out int idNivelZbritje, string kodNivelZbritje, string pershkrimNivelZbritje, int idPrindi, int prioritetiNivelZbritje, int idPerdoruesi, int idnderm, int idkonfig, int idstatusdok)
        { //metoda per ruajtjen e nivelit te zbritjeve
            idNivelZbritje = -1;

            dbManager.Open();

            //shtimi i parametrave
            dbManager.CreateParameters(9);
            dbManager.AddParameters(0, "@IDNIVELZBRITJE", idNivelZbritje, ParameterDirection.Output);
            dbManager.AddParameters(1, "@KODNIVELZBRITJE", kodNivelZbritje, ParameterDirection.Input);
            dbManager.AddParameters(2, "@PERSHKRIMNIVELZBRITJE", pershkrimNivelZbritje, ParameterDirection.Input);
            if (idPrindi == 0) dbManager.AddParameters(3, "@IDPRINDI", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(3, "@IDPRINDI", idPrindi, ParameterDirection.Input);

            dbManager.AddParameters(4, "@PRIORITETINIVELZBRITJE", prioritetiNivelZbritje, ParameterDirection.Input);
            dbManager.AddParameters(5, "@IDPERDORUESI", idPerdoruesi, ParameterDirection.Input);
            //dbManager.AddParameters(6, "@IDNDERVITI", idNderViti, ParameterDirection.Input);
            dbManager.AddParameters(6, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            dbManager.AddParameters(7, "@IDKONFIG", idkonfig, ParameterDirection.Input);
            dbManager.AddParameters(8, "@IDSTATUSDOK", idstatusdok, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_NIVELZBRITJE_ins");
            idNivelZbritje = int.Parse(dbManager.Parameters[0].Value.ToString());
            return true;

        }

        //[Obsolete("Perdor: clsMesazh ruajNivZbritje(int idNivelZbritje, string kodNivelZbritje, string pershkrimNivelZbritje, int idPrindi, int prioritetiNivelZbritje, int idPerdoruesi, int idNderViti, int idnderm, int idndermarje)", true)]
        //internal clsMesazh ruajNivelZbritje(int idNivelZbritje, string kodNivelZbritje, string pershkrimNivelZbritje, int idPrindi, int prioritetiNivelZbritje, int idPerdoruesi, int idNderViti, int idnderm, int idndermarje)
        //{ //metoda per ruajtjen e nivelit te zbritjeve
        //    bool ruaj = false;
        //    clsNivelZbritje nivelZbritje = new clsNivelZbritje(idNivelZbritje, kodNivelZbritje, pershkrimNivelZbritje, idPrindi, prioritetiNivelZbritje, idPerdoruesi, idNderViti, idnderm,0);
        //    clsMesazh mesazh = new clsMesazh();
        //    if (dbManager == null)
        //    {
        //        dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        //        dbManager.ConnectionString = dbManager.GetConnectionString();
        //    }
        //    try
        //    {
        //        dbManager.Open();

        //        //shtimi i parametrave
        //        dbManager.CreateParameters(8);
        //        dbManager.AddParameters(0, "@IDNIVELZBRITJE", idNivelZbritje, ParameterDirection.Output);
        //        dbManager.AddParameters(1, "@KODNIVELZBRITJE", kodNivelZbritje, ParameterDirection.Input);
        //        dbManager.AddParameters(2, "@PERSHKRIMNIVELZBRITJE", pershkrimNivelZbritje, ParameterDirection.Input);
        //        dbManager.AddParameters(3, "@IDPRINDI", idPrindi, ParameterDirection.Input);
        //        dbManager.AddParameters(4, "@PRIORITETINIVELZBRITJE", prioritetiNivelZbritje, ParameterDirection.Input);
        //        dbManager.AddParameters(5, "@IDPERDORUESI", idPerdoruesi, ParameterDirection.Input);
        //        dbManager.AddParameters(6, "@IDNDERVITI", idNderViti, ParameterDirection.Input);
        //        dbManager.AddParameters(7, "@IDNDERMARJE", idndermarje, ParameterDirection.Input);
        //        dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_NIVELZBRITJE_ins");
        //        mesazh = new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
        //        DbAdmin.clsDatabaseAdmin dbAdmin = new DbAdmin.clsDatabaseAdmin();
        //        int idregj = DbAdmin.clsListeAmbjenteCeljeRegjistrim.ktheIdCR("CNZ");
        //        //int idregj = dbAdmin.ktheListeAmbjentiCeljeRegjistrim("CNZ")[0].IdCR;
        //        int idlloji = DbAdmin.clsLlojKodi.ktheIDLlojKodi("Kod");
        //        //int idlloji = dbAdmin.ktheLlojKodi("Kod")[0].IdLlojKodi;
        //        if (new DbAdmin.clsACRNumraAutomatike(idregj, idlloji, idndermarje).IdLidhjeNrAuto!=0)
        //        {
        //            DbAdmin.clsACRNumraAutomatike ACR = new DbAdmin.clsACRNumraAutomatike(idregj, idlloji, idndermarje);
        //            DbAdmin.clsNrAutom NrAutom = new clsNrAutom(ACR.IdNumraAutoLidhje);
        //            //DbAdmin.clsNrAutom NrAutom = dbAdmin.ktheNrAutom(ACR.IdNumraAutoLidhje)[0];
        //            int karakteremajtas = NrAutom.MajtasNrAutom.Length;
        //            int karakteredjathtas = ACR.VleraFunditLidhje.Length - NrAutom.DjathtasNrAutom.Length - karakteremajtas;
        //            string vle = ACR.VleraFunditLidhje.Substring(karakteremajtas, karakteredjathtas);
        //            string vlera = NrAutom.gjeneroNumrinAutomatikPasardhes(vle);
        //            ACR.VleraFunditLidhje = vlera;
        //            ACR.modifiko();
        //        }
        //        ruaj = true;

        //    }
        //    catch (Exception)
        //    {
        //        return new clsMesazh(false, ce.Message);
        //    }
        //    if (ruaj == true)
        //    {
        //        if (nivelZbritje.IdPrindi != 0)
        //        {
        //            //colNiveleZbritjesh colNivele = merrNivelZbritjeSipasPrindit(nivelZbritje.IdPrindi);
        //            clsNivelZbritje clsNivele = new clsNivelZbritje();
        //            clsNivelZbritje nivelPrindi = new clsNivelZbritje();
        //            nivelPrindi.IdNivelZbritje = nivelZbritje.IdPrindi;
        //            nivelPrindi.IdNdermarje = nivelZbritje.IdNdermarje;
        //            clsNivele.mbushNivelZbritje(nivelPrindi.IdNivelZbritje, nivelPrindi.IdNdermarje);
        //            //colNivele.Add(merrNivelZbritje (nivelPrindi)[0] );
        //            if (clsNivele != null)
        //                //if (colNivele.Count > 0)
        //                if (nivelZbritje.PrioritetiNivelZbritje <= clsNivele.PrioritetiNivelZbritje)
        //                //if (nivelZbritje.PrioritetiNivelZbritje <= colNivele[0].PrioritetiNivelZbritje)
        //                {
        //                    //foreach (clsNivelZbritje n in colNivele)
        //                    if (clsNivele.PrioritetiNivelZbritje >= nivelZbritje.PrioritetiNivelZbritje && clsNivele.KodNivelZbritje != nivelZbritje.KodNivelZbritje)
        //                    {
        //                        clsNivele.PrioritetiNivelZbritje = clsNivele.PrioritetiNivelZbritje + 1;
        //                        modifikoNivelZbritje(clsNivele.IdNivelZbritje, clsNivele.KodNivelZbritje, clsNivele.PershkrimNivelZbritje, clsNivele.IdPrindi, clsNivele.PrioritetiNivelZbritje, clsNivele.IdPerdoruesi, clsNivele.IdNderViti, clsNivele.IdNdermarje);
        //                        //modifikoNivelZbritje(clsNivele);
        //                    }

        //                }
        //        }
        //    }
        //    return mesazh;
        //}
        //[Obsolete("Perdor: clsMesazh ruajNivelZbritje(int idNivelZbritje, string kodNivelZbritje, string pershkrimNivelZbritje, int idPrindi, int prioritetiNivelZbritje, int idPerdoruesi, int idNderViti, int idnderm, int idndermarje)", true)]
        //public clsMesazh ruajNivelZbritje(clsNivelZbritje nivelZbritje, int idndermarje)
        //{ //metoda per ruajtjen e nivelit te zbritjeve
        //    bool ruaj = false;
        //    clsMesazh mesazh = new clsMesazh();
        //    IDBManager dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        //    dbManager.ConnectionString = dbManager.GetConnectionString();
        //    try
        //    {
        //        dbManager.Open();

        //        //shtimi i parametrave
        //        dbManager.CreateParameters(8);
        //        dbManager.AddParameters(0, "@IDNIVELZBRITJE", nivelZbritje.IdNivelZbritje, ParameterDirection.Output);
        //        dbManager.AddParameters(1, "@KODNIVELZBRITJE", nivelZbritje.KodNivelZbritje, ParameterDirection.Input);
        //        dbManager.AddParameters(2, "@PERSHKRIMNIVELZBRITJE", nivelZbritje.PershkrimNivelZbritje, ParameterDirection.Input);
        //        dbManager.AddParameters(3, "@IDPRINDI", nivelZbritje.IdPrindi, ParameterDirection.Input);
        //        dbManager.AddParameters(4, "@PRIORITETINIVELZBRITJE", nivelZbritje.PrioritetiNivelZbritje, ParameterDirection.Input);
        //        dbManager.AddParameters(5, "@IDPERDORUESI", nivelZbritje.IdPerdoruesi, ParameterDirection.Input);
        //        dbManager.AddParameters(6, "@IDNDERVITI", nivelZbritje.IdNderViti, ParameterDirection.Input);
        //        dbManager.AddParameters(7, "@IDNDERMARJE", nivelZbritje.IdNdermarje, ParameterDirection.Input);
        //        dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_NIVELZBRITJE_ins");
        //        mesazh = new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
        //        DbAdmin.clsDatabaseAdmin dbAdmin = new DbAdmin.clsDatabaseAdmin();
        //        int idregj = DbAdmin.clsListeAmbjenteCeljeRegjistrim.ktheIdCR("CNZ");
        //        //int idregj = dbAdmin.ktheListeAmbjentiCeljeRegjistrim("CNZ")[0].IdCR;
        //        //int idlloji = dbAdmin.ktheLlojKodi("Kod")[0].IdLlojKodi;
        //        int idlloji = DbAdmin.clsLlojKodi.ktheIDLlojKodi("Kod");
        //        if (new DbAdmin.clsACRNumraAutomatike(idregj, idlloji, idndermarje).IdLidhjeNrAuto!=0)
        //        {
        //            DbAdmin.clsACRNumraAutomatike ACR = new DbAdmin.clsACRNumraAutomatike(idregj, idlloji, idndermarje);
        //            DbAdmin.clsNrAutom NrAutom = new clsNrAutom(ACR.IdNumraAutoLidhje);
        //            //DbAdmin.clsNrAutom NrAutom = dbAdmin.ktheNrAutom(ACR.IdNumraAutoLidhje)[0];
        //            int karakteremajtas = NrAutom.MajtasNrAutom.Length;
        //            int karakteredjathtas = ACR.VleraFunditLidhje.Length - NrAutom.DjathtasNrAutom.Length - karakteremajtas;
        //            string vle = ACR.VleraFunditLidhje.Substring(karakteremajtas, karakteredjathtas);
        //            string vlera = NrAutom.gjeneroNumrinAutomatikPasardhes(vle);
        //            ACR.VleraFunditLidhje = vlera;
        //            ACR.modifiko();
        //        }
        //        ruaj = true;

        //    }
        //    catch (Exception)
        //    {
        //        return new clsMesazh(false, ce.Message);
        //    }
        //    finally
        //    {
        //        dbManager.Dispose();
        //    }
        //    if (ruaj == true)
        //    {
        //        if (nivelZbritje.IdPrindi != 0)
        //        {
        //            //colNiveleZbritjesh colNivele = merrNivelZbritjeSipasPrindit(nivelZbritje.IdPrindi);
        //            clsNivelZbritje clsNivele = new clsNivelZbritje();
        //            clsNivelZbritje nivelPrindi=new clsNivelZbritje ();
        //            nivelPrindi.IdNivelZbritje =nivelZbritje.IdPrindi ;
        //            nivelPrindi.IdNdermarje = nivelZbritje.IdNdermarje;
        //            clsNivele.mbushNivelZbritje(nivelPrindi.IdNivelZbritje, nivelPrindi.IdNdermarje);
        //            //colNivele.Add(merrNivelZbritje (nivelPrindi)[0] );
        //            if (clsNivele != null)
        //            //if (colNivele.Count > 0)
        //                if (nivelZbritje.PrioritetiNivelZbritje <= clsNivele.PrioritetiNivelZbritje)
        //                //if (nivelZbritje.PrioritetiNivelZbritje <= colNivele[0].PrioritetiNivelZbritje)
        //                {
        //                    //foreach (clsNivelZbritje n in colNivele)
        //                    if (clsNivele.PrioritetiNivelZbritje >= nivelZbritje.PrioritetiNivelZbritje && clsNivele.KodNivelZbritje != nivelZbritje.KodNivelZbritje)
        //                        {
        //                            clsNivele.PrioritetiNivelZbritje = clsNivele.PrioritetiNivelZbritje + 1;
        //                            modifikoNivelZbritje(clsNivele.IdNivelZbritje, clsNivele.KodNivelZbritje, clsNivele.PershkrimNivelZbritje, clsNivele.IdPrindi, clsNivele.PrioritetiNivelZbritje, clsNivele.IdPerdoruesi, clsNivele.IdNderViti, clsNivele.IdNdermarje);
        //                            //modifikoNivelZbritje(clsNivele);
        //                        }

        //                }
        //        }
        //    }
        //    return mesazh;
        //}

        /// <summary>
        /// ekzekuton prc_T_NIVELZBRITJE_upd ne DB dhe kthen statusin e perfundimit te ekzekutimit te SP-se perkatese
        /// Shiko clsMesazh brenda ketij moduli per informacion te metejshem mbi vleren e kthimit te kesaj metode
        /// <param name="idNivelZbritje"> id ritese e nivelit te zbritjes</param>
        /// <param name="kodNivelZbritje"> kodi i nivelit te zbritjes</param>
        /// <param name="pershkrimNivelZbritje"> pershkrimi i nivelit te zbritjes</param>
        /// <param name="idPrindi"> id e prindit</param>
        /// <param name="prioritetiNivelZbritje"> prioriteti i nivelit te zbritjes</param>
        /// <param name="idPerdoruesi"> id e perdoruesit qe ka kryer veprimin</param>
        /// <param name="idNderViti"> id e ndermarje vitit</param>
        /// <param name="idnderm"> id e ndermarjes</param>
        /// <returns> nje objekt clsMesazh qe tregon nese modifikimi eshte kryer ne rregull apo jo.</returns>
        /// </summary>
        internal bool modifikoNivZbritje(int idNivelZbritje, string kodNivelZbritje, string pershkrimNivelZbritje, int idPrindi, int prioritetiNivelZbritje, int idPerdoruesi, int idnderm, int idkonfig, int idstatusdok)
        {
            bool ruaj = false;

            dbManager.Open();

            //shtimi i parametrave
            dbManager.CreateParameters(9);
            dbManager.AddParameters(0, "@IDNIVELZBRITJE", idNivelZbritje, ParameterDirection.Input);
            dbManager.AddParameters(1, "@KODNIVELZBRITJE", kodNivelZbritje, ParameterDirection.Input);
            dbManager.AddParameters(2, "@PERSHKRIMNIVELZBRITJE", pershkrimNivelZbritje, ParameterDirection.Input);
            if (idPrindi == 0) dbManager.AddParameters(3, "@IDPRINDI", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(3, "@IDPRINDI", idPrindi, ParameterDirection.Input);

            dbManager.AddParameters(4, "@PRIORITETINIVELZBRITJE", prioritetiNivelZbritje, ParameterDirection.Input);
            dbManager.AddParameters(5, "@IDPERDORUESI", idPerdoruesi, ParameterDirection.Input);
            //dbManager.AddParameters(6, "@IDNDERVITI", idNderViti, ParameterDirection.Input);
            dbManager.AddParameters(6, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            dbManager.AddParameters(7, "@IDKONFIG", idkonfig, ParameterDirection.Input);
            dbManager.AddParameters(8, "@IDSTATUSDOK", idstatusdok, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_NIVELZBRITJE_upd");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
            ruaj = true;
            return ruaj;

        }
        //[Obsolete("Perdor: bool modifikoNivZbritje(int idNivelZbritje, string kodNivelZbritje, string pershkrimNivelZbritje, int idPrindi, int prioritetiNivelZbritje, int idPerdoruesi, int idNderViti, int idnderm)", true)]
        //public bool modifikoNivZbritje(clsNivelZbritje nivelZbritje)
        //{
        //    bool ruaj = false;
        //    IDBManager dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        //    dbManager.ConnectionString = dbManager.GetConnectionString();
        //    try
        //    {
        //        dbManager.Open();

        //        //shtimi i parametrave
        //        dbManager.CreateParameters(8);
        //        dbManager.AddParameters(0, "@IDNIVELZBRITJE", nivelZbritje.IdNivelZbritje, ParameterDirection.Input);
        //        dbManager.AddParameters(1, "@KODNIVELZBRITJE", nivelZbritje.KodNivelZbritje, ParameterDirection.Input);
        //        dbManager.AddParameters(2, "@PERSHKRIMNIVELZBRITJE", nivelZbritje.PershkrimNivelZbritje, ParameterDirection.Input);
        //        dbManager.AddParameters(3, "@IDPRINDI", nivelZbritje.IdPrindi, ParameterDirection.Input);
        //        dbManager.AddParameters(4, "@PRIORITETINIVELZBRITJE", nivelZbritje.PrioritetiNivelZbritje, ParameterDirection.Input);
        //        dbManager.AddParameters(5, "@IDPERDORUESI", nivelZbritje.IdPerdoruesi, ParameterDirection.Input);
        //        dbManager.AddParameters(6, "@IDNDERVITI", nivelZbritje.IdNderViti, ParameterDirection.Input);
        //        dbManager.AddParameters(7, "@IDNDERMARJE", nivelZbritje.IdNdermarje, ParameterDirection.Input);
        //        dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_NIVELZBRITJE_upd");
        //        clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
        //        ruaj = true;
        //        return ruaj;
        //    }
        //    catch (Exception)
        //    {
        //        return false;
        //    }
        //    finally
        //    {
        //        dbManager.Dispose();
        //    }
        //}

        ///// <summary>
        ///// modifikon nje nivel zbritje duke ndryshuar prioritetet e te gjithe nivele te tjera te te njejtit prind ne varesi te ndryshimit te nivelit qe u modifikua
        ///// </summary>
        ///// <param name="idNivelZbritje"> id ritese e nivelit te zbritjes</param>
        ///// <param name="kodNivelZbritje"> kodi i nivelit te zbritjes</param>
        ///// <param name="pershkrimNivelZbritje"> pershkrimi i nivelit te zbritjes</param>
        ///// <param name="idPrindi"> id e prindit</param>
        ///// <param name="prioritetiNivelZbritje"> prioriteti i nivelit te zbritjes</param>
        ///// <param name="idPerdoruesi"> id e perdoruesit qe ka kryer veprimin</param>
        ///// <param name="idNderViti"> id e ndermarje vitit</param>
        ///// <param name="idnderm"> id e ndermarjes</param>
        ///// <returns> kthen nje obj clsMesazh per te identifikuar statusin e modifikimit se te dhenave ne DB</returns>
        //[Obsolete("Perdor nga klasa perkatese: modifikoNivelZbritje(int idNivelZbritje, string kodNivelZbritje, string pershkrimNivelZbritje, int idPrindi, int prioritetiNivelZbritje, int idPerdoruesi, int idNderViti, int idnderm)", true)]
        //internal clsMesazh modifikoNivelZbritje(int idNivelZbritje, string kodNivelZbritje, string pershkrimNivelZbritje, int idPrindi, int prioritetiNivelZbritje, int idPerdoruesi, int idNderViti, int idnderm)
        //{//metoda per modifikimin e nivelit te zbritje
        //    bool ruaj = false;
        //    clsMesazh mesazh = new clsMesazh();
        //    clsNivelZbritje nivelZbritje = new clsNivelZbritje(idNivelZbritje, kodNivelZbritje, pershkrimNivelZbritje, idPrindi, prioritetiNivelZbritje, idPerdoruesi, idNderViti, idnderm,0);
        //    clsNivelZbritje nivelipara = new clsNivelZbritje();
        //    nivelipara.mbushNivelZbritje(nivelZbritje.IdNivelZbritje, nivelZbritje.IdNdermarje);
        //    //clsNivelZbritje nivelipara = merrNivelZbritje(nivelZbritje)[0];
        //    ruaj = modifikoNivZbritje(idNivelZbritje, kodNivelZbritje, pershkrimNivelZbritje, idPrindi, prioritetiNivelZbritje, idPerdoruesi, idNderViti, idnderm,0);
        //    if (ruaj == true)
        //    {
        //        mesazh = new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);

        //        if (nivelZbritje.IdPrindi != 0)
        //        {
        //            if (nivelipara.IdPrindi == nivelZbritje.IdPrindi)
        //            {
        //                if (nivelipara.PrioritetiNivelZbritje != nivelZbritje.PrioritetiNivelZbritje)
        //                {
        //                    //colNiveleZbritjesh colNivele = merrNivelZbritjeSipasPrindit(nivelZbritje.IdPrindi);
        //                    clsNivelZbritje clsNivele = new clsNivelZbritje();
        //                    clsNivelZbritje nivelPrindi = new clsNivelZbritje();
        //                    nivelPrindi.IdNivelZbritje = nivelZbritje.IdPrindi;
        //                    nivelPrindi.IdNdermarje = nivelZbritje.IdNdermarje;
        //                    clsNivele.mbushNivelZbritje(nivelPrindi.IdNivelZbritje, nivelPrindi.IdNdermarje);
        //                    //colNivele.Add(merrNivelZbritje(nivelPrindi)[0]);
        //                    if (nivelZbritje.PrioritetiNivelZbritje < nivelipara.PrioritetiNivelZbritje)
        //                    {
        //                        //foreach (clsNivelZbritje n in colNivele)
        //                        if (clsNivele.PrioritetiNivelZbritje >= nivelZbritje.PrioritetiNivelZbritje && clsNivele.PrioritetiNivelZbritje < nivelipara.PrioritetiNivelZbritje && clsNivele.KodNivelZbritje != nivelZbritje.KodNivelZbritje)
        //                        {
        //                            clsNivele.PrioritetiNivelZbritje = clsNivele.PrioritetiNivelZbritje + 1;
        //                            modifikoNivZbritje(clsNivele.IdNivelZbritje, clsNivele.KodNivelZbritje, clsNivele.PershkrimNivelZbritje, clsNivele.IdPrindi, clsNivele.PrioritetiNivelZbritje, clsNivele.IdPerdoruesi, clsNivele.IdNderViti, clsNivele.IdNdermarje,0);
        //                            //modifikoNivZbritje(clsNivele);
        //                        }

        //                    }
        //                    else
        //                    {
        //                        //foreach (clsNivelZbritje n in colNivele)
        //                        if (clsNivele.PrioritetiNivelZbritje <= nivelZbritje.PrioritetiNivelZbritje && clsNivele.PrioritetiNivelZbritje > nivelipara.PrioritetiNivelZbritje && clsNivele.KodNivelZbritje != nivelZbritje.KodNivelZbritje)
        //                        {
        //                            clsNivele.PrioritetiNivelZbritje = clsNivele.PrioritetiNivelZbritje - 1;
        //                            modifikoNivZbritje(clsNivele.IdNivelZbritje, clsNivele.KodNivelZbritje, clsNivele.PershkrimNivelZbritje, clsNivele.IdPrindi, clsNivele.PrioritetiNivelZbritje, clsNivele.IdPerdoruesi, clsNivele.IdNderViti, clsNivele.IdNdermarje,0);
        //                            //modifikoNivZbritje(clsNivele);
        //                        }
        //                    }

        //                }
        //            }
        //            else
        //            {
        //                //colNiveleZbritjesh colNivele = merrNivelZbritjeSipasPrindit(nivelZbritje.IdPrindi);
        //                clsNivelZbritje clsNivele = new clsNivelZbritje();
        //                clsNivelZbritje nivelPrindi = new clsNivelZbritje();
        //                nivelPrindi.IdNivelZbritje = nivelZbritje.IdPrindi;
        //                nivelPrindi.IdNdermarje = nivelZbritje.IdNdermarje;
        //                clsNivele.mbushNivelZbritje(nivelPrindi.IdNivelZbritje, nivelPrindi.IdNdermarje);
        //                //colNivele.Add(merrNivelZbritje(nivelPrindi)[0]);
        //                if (clsNivele != null)
        //                    //if (colNivele.Count > 0)
        //                    if (nivelZbritje.PrioritetiNivelZbritje <= clsNivele.PrioritetiNivelZbritje)
        //                    //if (nivelZbritje.PrioritetiNivelZbritje <= colNivele[0].PrioritetiNivelZbritje)
        //                    {
        //                        //foreach (clsNivelZbritje n in colNivele)
        //                        if (clsNivele.PrioritetiNivelZbritje >= nivelZbritje.PrioritetiNivelZbritje && clsNivele.KodNivelZbritje != nivelZbritje.KodNivelZbritje)
        //                        {
        //                            clsNivele.PrioritetiNivelZbritje = clsNivele.PrioritetiNivelZbritje + 1;
        //                            modifikoNivZbritje(clsNivele.IdNivelZbritje, clsNivele.KodNivelZbritje, clsNivele.PershkrimNivelZbritje, clsNivele.IdPrindi, clsNivele.PrioritetiNivelZbritje, clsNivele.IdPerdoruesi, clsNivele.IdNderViti, clsNivele.IdNdermarje,0);
        //                            //modifikoNivZbritje(clsNivele);
        //                        }

        //                    }
        //            }
        //        }
        //        else
        //        {
        //            if (nivelipara.PrioritetiNivelZbritje != nivelZbritje.PrioritetiNivelZbritje)
        //            {
        //                colNiveleZbritjesh colNivele = new colNiveleZbritjesh();
        //                colNivele.mbushNiveleZbritjeshSipasPrindit(nivelZbritje.IdNivelZbritje);
        //                //colNiveleZbritjesh colNivele = merrNivelZbritjeSipasPrindit(nivelZbritje.IdNivelZbritje);

        //                if (nivelZbritje.PrioritetiNivelZbritje < nivelipara.PrioritetiNivelZbritje)
        //                {
        //                    foreach (clsNivelZbritje n in colNivele)
        //                        if (n.PrioritetiNivelZbritje >= nivelZbritje.PrioritetiNivelZbritje && n.PrioritetiNivelZbritje < nivelipara.PrioritetiNivelZbritje && n.KodNivelZbritje != nivelZbritje.KodNivelZbritje)
        //                        {
        //                            n.PrioritetiNivelZbritje = n.PrioritetiNivelZbritje + 1;
        //                            modifikoNivZbritje(n.IdNivelZbritje, n.KodNivelZbritje, n.PershkrimNivelZbritje, n.IdPrindi, n.PrioritetiNivelZbritje, n.IdPerdoruesi, n.IdNderViti, n.IdNdermarje,0);
        //                            //modifikoNivZbritje(n);
        //                        }

        //                }
        //                else
        //                {                                                                                                                                                                             
        //                    foreach (clsNivelZbritje n in colNivele)
        //                        if (n.PrioritetiNivelZbritje <= nivelZbritje.PrioritetiNivelZbritje && n.PrioritetiNivelZbritje > nivelipara.PrioritetiNivelZbritje && n.KodNivelZbritje != nivelZbritje.KodNivelZbritje)
        //                        {
        //                            n.PrioritetiNivelZbritje = n.PrioritetiNivelZbritje - 1;
        //                            modifikoNivZbritje(n.IdNivelZbritje, n.KodNivelZbritje, n.PershkrimNivelZbritje, n.IdPrindi, n.PrioritetiNivelZbritje, n.IdPerdoruesi, n.IdNderViti, n.IdNdermarje,0);
        //                            //modifikoNivZbritje(n);
        //                        }
        //                }

        //            }
        //        }
        //    }

        //    return mesazh;
        //}
        //[Obsolete("Perdor: clsMesazh modifikoNivelZbritje(int idNivelZbritje, string kodNivelZbritje, string pershkrimNivelZbritje, int idPrindi, int prioritetiNivelZbritje, int idPerdoruesi, int idNderViti, int idnderm)", true)]
        //public clsMesazh modifikoNivelZbritje(clsNivelZbritje nivelZbritje)
        //{//metoda per modifikimin e nivelit te zbritje
        //    bool ruaj = false;
        //    clsMesazh mesazh = new clsMesazh();
        //    clsNivelZbritje nivelipara = new clsNivelZbritje();
        //    nivelipara.mbushNivelZbritje(nivelZbritje.IdNivelZbritje, nivelZbritje.IdNdermarje);
        //    //clsNivelZbritje nivelipara = merrNivelZbritje(nivelZbritje)[0];
        //    ruaj = modifikoNivZbritje(nivelZbritje.IdNivelZbritje, nivelZbritje.KodNivelZbritje, nivelZbritje.PershkrimNivelZbritje, nivelZbritje.IdPrindi, nivelZbritje.PrioritetiNivelZbritje, nivelZbritje.IdPerdoruesi, nivelZbritje.IdNderViti, nivelZbritje.IdNdermarje,0);
        //    //ruaj = modifikoNivZbritje(nivelZbritje);
        //    if (ruaj == true)
        //    {
        //        mesazh = new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);

        //        if (nivelZbritje.IdPrindi != 0)
        //        {
        //            if (nivelipara.IdPrindi == nivelZbritje.IdPrindi)
        //            {
        //                if (nivelipara.PrioritetiNivelZbritje != nivelZbritje.PrioritetiNivelZbritje)
        //                {
        //                    //colNiveleZbritjesh colNivele = merrNivelZbritjeSipasPrindit(nivelZbritje.IdPrindi);
        //                    clsNivelZbritje clsNivele = new clsNivelZbritje();
        //                    clsNivelZbritje nivelPrindi = new clsNivelZbritje();
        //                    nivelPrindi.IdNivelZbritje = nivelZbritje.IdPrindi;
        //                    nivelPrindi.IdNdermarje = nivelZbritje.IdNdermarje;
        //                    clsNivele.mbushNivelZbritje(nivelPrindi.IdNivelZbritje, nivelPrindi.IdNdermarje);
        //                    //colNivele.Add(merrNivelZbritje(nivelPrindi)[0]);
        //                    if (nivelZbritje.PrioritetiNivelZbritje < nivelipara.PrioritetiNivelZbritje)
        //                    {
        //                        //foreach (clsNivelZbritje n in colNivele)
        //                        if (clsNivele.PrioritetiNivelZbritje >= nivelZbritje.PrioritetiNivelZbritje && clsNivele.PrioritetiNivelZbritje < nivelipara.PrioritetiNivelZbritje && clsNivele.KodNivelZbritje != nivelZbritje.KodNivelZbritje)
        //                            {
        //                                clsNivele.PrioritetiNivelZbritje = clsNivele.PrioritetiNivelZbritje + 1;
        //                                modifikoNivZbritje(clsNivele.IdNivelZbritje, clsNivele.KodNivelZbritje, clsNivele.PershkrimNivelZbritje, clsNivele.IdPrindi, clsNivele.PrioritetiNivelZbritje, clsNivele.IdPerdoruesi, clsNivele.IdNderViti, clsNivele.IdNdermarje,0);
        //                                //modifikoNivZbritje(clsNivele);
        //                            }

        //                    }
        //                    else
        //                    {
        //                        //foreach (clsNivelZbritje n in colNivele)
        //                        if (clsNivele.PrioritetiNivelZbritje <= nivelZbritje.PrioritetiNivelZbritje && clsNivele.PrioritetiNivelZbritje > nivelipara.PrioritetiNivelZbritje && clsNivele.KodNivelZbritje != nivelZbritje.KodNivelZbritje)
        //                            {
        //                                clsNivele.PrioritetiNivelZbritje = clsNivele.PrioritetiNivelZbritje - 1;
        //                                modifikoNivZbritje(clsNivele.IdNivelZbritje, clsNivele.KodNivelZbritje, clsNivele.PershkrimNivelZbritje, clsNivele.IdPrindi, clsNivele.PrioritetiNivelZbritje, clsNivele.IdPerdoruesi, clsNivele.IdNderViti, clsNivele.IdNdermarje,0);
        //                                //modifikoNivZbritje(clsNivele);
        //                            }
        //                    }

        //                }
        //            }
        //            else
        //            {
        //                //colNiveleZbritjesh colNivele = merrNivelZbritjeSipasPrindit(nivelZbritje.IdPrindi);
        //                clsNivelZbritje clsNivele = new clsNivelZbritje();
        //                clsNivelZbritje nivelPrindi = new clsNivelZbritje();
        //                nivelPrindi.IdNivelZbritje = nivelZbritje.IdPrindi;
        //                nivelPrindi.IdNdermarje = nivelZbritje.IdNdermarje;
        //                clsNivele.mbushNivelZbritje(nivelPrindi.IdNivelZbritje, nivelPrindi.IdNdermarje);
        //                //colNivele.Add(merrNivelZbritje(nivelPrindi)[0]);
        //                if (clsNivele !=null)
        //                //if (colNivele.Count > 0)
        //                    if (nivelZbritje.PrioritetiNivelZbritje <= clsNivele.PrioritetiNivelZbritje)
        //                    //if (nivelZbritje.PrioritetiNivelZbritje <= colNivele[0].PrioritetiNivelZbritje)
        //                    {
        //                        //foreach (clsNivelZbritje n in colNivele)
        //                        if (clsNivele.PrioritetiNivelZbritje >= nivelZbritje.PrioritetiNivelZbritje && clsNivele.KodNivelZbritje != nivelZbritje.KodNivelZbritje)
        //                            {
        //                                clsNivele.PrioritetiNivelZbritje = clsNivele.PrioritetiNivelZbritje + 1;
        //                                modifikoNivZbritje(clsNivele.IdNivelZbritje, clsNivele.KodNivelZbritje, clsNivele.PershkrimNivelZbritje, clsNivele.IdPrindi, clsNivele.PrioritetiNivelZbritje, clsNivele.IdPerdoruesi, clsNivele.IdNderViti, clsNivele.IdNdermarje,0);
        //                                //modifikoNivZbritje(clsNivele);
        //                            }

        //                    }
        //            }
        //        }
        //        else
        //        {
        //            if (nivelipara.PrioritetiNivelZbritje != nivelZbritje.PrioritetiNivelZbritje)
        //            {
        //                colNiveleZbritjesh colNivele = new colNiveleZbritjesh();
        //                colNivele.mbushNiveleZbritjeshSipasPrindit(nivelZbritje.IdNivelZbritje);
        //                //colNiveleZbritjesh colNivele = merrNivelZbritjeSipasPrindit(nivelZbritje.IdNivelZbritje);

        //                if (nivelZbritje.PrioritetiNivelZbritje < nivelipara.PrioritetiNivelZbritje)
        //                {
        //                    foreach (clsNivelZbritje n in colNivele)
        //                        if (n.PrioritetiNivelZbritje >= nivelZbritje.PrioritetiNivelZbritje && n.PrioritetiNivelZbritje < nivelipara.PrioritetiNivelZbritje && n.KodNivelZbritje != nivelZbritje.KodNivelZbritje)
        //                        {
        //                            n.PrioritetiNivelZbritje = n.PrioritetiNivelZbritje + 1;
        //                            modifikoNivZbritje(n.IdNivelZbritje, n.KodNivelZbritje, n.PershkrimNivelZbritje, n.IdPrindi, n.PrioritetiNivelZbritje, n.IdPerdoruesi, n.IdNderViti, n.IdNdermarje,0);
        //                            //modifikoNivZbritje(n);
        //                        }

        //                }
        //                else
        //                {
        //                    foreach (clsNivelZbritje n in colNivele)
        //                        if (n.PrioritetiNivelZbritje <= nivelZbritje.PrioritetiNivelZbritje && n.PrioritetiNivelZbritje > nivelipara.PrioritetiNivelZbritje && n.KodNivelZbritje != nivelZbritje.KodNivelZbritje)
        //                        {
        //                            n.PrioritetiNivelZbritje = n.PrioritetiNivelZbritje - 1;
        //                            modifikoNivZbritje(n.IdNivelZbritje, n.KodNivelZbritje, n.PershkrimNivelZbritje, n.IdPrindi, n.PrioritetiNivelZbritje, n.IdPerdoruesi, n.IdNderViti, n.IdNdermarje,0);
        //                            //modifikoNivZbritje(n);
        //                        }
        //                }

        //            }
        //        }
        //    }

        //    return mesazh;
        //}

        /// <summary>
        /// ekzekutohet sp-ja prc_T_NIVELZBRITJE_del duke i kaluar id e nivelit te zbritjes qe e marrim nga objekti clsNivelZbritje qe i kalohet si parameter
        /// </summary>
        /// <param name="idNivelZbritje"> niveli i zbritjes  qe do fshihet </param>
        /// <returns>nje objekt clsMesazh qe tregon nese fshirja eshte kryer ne rregull apo jo</returns>
        internal clsMesazh fshiNivelZbritje(int idNivelZbritje)
        {//metoda per fshirjen e nivelit te zbritje

            dbManager.Open();
            //shtimi i parametrave
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDNIVELZBRITJE", idNivelZbritje, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_NIVELZBRITJE_del");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
            return mesazh;

        }
        internal clsMesazh fshiNivelZbritjeStatus(int idNivelZbritje, int idperdoruesi)
        {//metoda per fshirjen e nivelit te zbritje

            dbManager.Open();
            //shtimi i parametrave
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDNIVELZBRITJE", idNivelZbritje, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUESI", idperdoruesi, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_NIVELZBRITJE_upddel");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
            return mesazh;

        }
        //[Obsolete("Perdor: clsMesazh fshiNivelZbritje(int idNivelZbritje)", true)]
        //public clsMesazh fshiNivelZbritje(clsNivelZbritje nivelZbritje)
        //{//metoda per fshirjen e nivelit te zbritje
        //    IDBManager dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        //    dbManager.ConnectionString = dbManager.GetConnectionString();
        //    try
        //    {
        //        dbManager.Open();
        //        //shtimi i parametrave
        //        dbManager.CreateParameters(1);
        //        dbManager.AddParameters(0, "@IDNIVELZBRITJE", nivelZbritje.IdNivelZbritje, ParameterDirection.Input);
        //        dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_NIVELZBRITJE_del");
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
        /// merr nivelet e zbritje sipas id
        /// </summary>
        /// <param name="idNivelZbritje"> niveli i zbritjes qe i merret id</param>
        /// 
        /// <returns> kthen nje objekt colNiveleZbritjesh qe permban nje koleksion me te gjithe nivelet e zbritjes me kete id</returns>
        internal DataRow ktheNivelZbritje(int idNivelZbritje)
        {

            dbManager.Open();
            //shtimi i parametrave
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDNIVELZBRITJE", idNivelZbritje, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_NIVELZBRITJE_sel");


            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];

        }
        //[Obsolete("Perdor: DataRow merrNivelZbritje(int idNivelZbritje, int idNdermarje)", true)]
        //public colNiveleZbritjesh merrNivelZbritje(clsNivelZbritje nivelZbritje)
        //{// metoda per te marre nje nivelin e zbritje NE BAZE TE ID
        //    IDBManager dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        //    dbManager.ConnectionString = dbManager.GetConnectionString();
        //    try
        //    {
        //        dbManager.Open();
        //        //shtimi i parametrave
        //        dbManager.CreateParameters(2);
        //        dbManager.AddParameters(0, "@IDNIVELZBRITJE", nivelZbritje.IdNivelZbritje, ParameterDirection.Input);
        //        dbManager.AddParameters(1, "@IDNDERMARJE", nivelZbritje.IdNdermarje, ParameterDirection.Input);
        //        DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_NIVELZBRITJE_sel");
        //        colNiveleZbritjesh colNiveleZbritjesh = new colNiveleZbritjesh();
        //        return colNiveleZbritjesh.mbushArrayListNiveleZbritjesh(ds);
        //    }
        //    catch (Exception)
        //    {
        //        return new colNiveleZbritjesh();
        //    }
        //    finally
        //    {
        //        dbManager.Dispose();
        //    }
        //}

        /// <summary>
        /// merr gjithe nivelet e zbritjes te nje ndermarje
        /// </summary>
        /// <param name="idnderm"> id e ndermarjes</param>
        /// <returns> nje objekt colNiveleZbritjesh qe permban nje koleksion me te gjithe nivelet e zbritjes te ndermarjes</returns>
        internal DataTable ktheGjitheNiveleZbritjeshSipasNdermarjes(int idnderm)
        {

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_NIVELZBRITJE_merrNivelZbritjeSipasNdermarjes");
            return ds.Tables[0];

        }
        //[Obsolete("Perdor: DataTable merrGjitheNiveleZbritjeshSipasNdermarjes(int idnderm)", true)]
        //public colNiveleZbritjesh merrGjitheNiveleZbritjeshSipasNdermarjes(int idnderm)
        //{//metoda per te marre te gjithe nivelet e zbritjeve
        //    IDBManager dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        //    dbManager.ConnectionString = dbManager.GetConnectionString();
        //    try
        //    {
        //        dbManager.Open();
        //        dbManager.CreateParameters(1);
        //        dbManager.AddParameters(0, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
        //        DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_NIVELZBRITJE_merrNivelZbritjeSipasNdermarjes");
        //        colNiveleZbritjesh colNiveleZbritjesh = new colNiveleZbritjesh();
        //        return colNiveleZbritjesh.mbushArrayListNiveleZbritjesh(ds);
        //    }
        //    catch (Exception)
        //    {
        //        return new colNiveleZbritjesh();
        //    }
        //    finally
        //    {
        //        dbManager.Dispose();
        //    }
        //}

        /// <summary>
        /// merr gjithe nivelet e zbritjes prind te nje ndermarje 
        /// </summary>
        /// <param name="idnderm"> id e ndermarjes</param>
        /// <returns> nje objekt colNiveleZbritjesh qe permban nje koleksion me te gjithe nivelet e zbritjes prind te ndermarjes <returns>
        internal DataTable ktheGjitheNiveleZbritjeshPrindiSipasNdermarjes(int idnderm)
        {

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_NIVELZBRITJE_merrNivelZbritjePrindSipasNdermarjes");
            return ds.Tables[0];

        }
        //[Obsolete("Perdor: DataTable merrGjitheNiveleZbritjeshPrindiSipasNdermarjes(int idnderm)", true)]
        //public colNiveleZbritjesh merrGjitheNiveleZbritjeshPrindiSipasNdermarjes(int idnderm)
        //{//metoda per te marre te gjithe nivelet e zbritjeve prind
        //    IDBManager dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        //    dbManager.ConnectionString = dbManager.GetConnectionString();
        //    try
        //    {
        //        dbManager.Open();
        //        dbManager.CreateParameters(1);
        //        dbManager.AddParameters(0, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
        //        DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_NIVELZBRITJE_merrNivelZbritjePrindSipasNdermarjes");
        //        colNiveleZbritjesh colNiveleZbritjesh = new colNiveleZbritjesh();
        //        return colNiveleZbritjesh.mbushArrayListNiveleZbritjesh(ds);
        //    }
        //    catch (Exception)
        //    {
        //        return new colNiveleZbritjesh();
        //    }
        //    finally
        //    {
        //        dbManager.Dispose();
        //    }
        //}

        /// <summary>
        /// merr gjithe nivelet e zbritjes te nje ndermarje me kete kod
        /// </summary>
        /// <param name="kodi"> kodi i nivelit te zbritjes</param>
        /// <param name="idnderm"> id e ndermarjes</param>
        /// <returns> nje objekt colNiveleZbritjesh qe permban nje koleksion me te gjithe nivelet e zbritjes te nje ndermarje me kete kod</returns>
        internal int ktheNivelZbritjeSipasKodit(string kodi, int idnderm)
        {

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@KODNIVELZBRITJE", kodi, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_NIVELZBRITJE_merrNivelZbritjeSipasKodit");
            if (ds == null)
                return -1;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return -1;
            int idNivelZbritje;
            int.TryParse(ds.Tables[0].Rows[0]["IDNIVELZBRITJE"].ToString(), out idNivelZbritje);
            return idNivelZbritje;

        }
        //[Obsolete("Perdor: int merrNivelZbritjeSipasKodit(string kodi,int idnderm)", true)]
        //public colNiveleZbritjesh merrNivelZbritjeSipasKodit(string kodi,int idnderm)
        //{//metoda per te marre NIVEL ZBRITJE sipas KODIT
        //    IDBManager dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        //    dbManager.ConnectionString = dbManager.GetConnectionString();
        //    try
        //    {
        //        dbManager.Open();
        //        dbManager.CreateParameters(2);
        //        dbManager.AddParameters(0, "@KODNIVELZBRITJE", kodi, ParameterDirection.Input);
        //        dbManager.AddParameters(1, "@IDNDERMARJE", idnderm , ParameterDirection.Input);
        //        DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_NIVELZBRITJE_merrNivelZbritjeSipasKodit");
        //        colNiveleZbritjesh colNiveleZbritjesh = new colNiveleZbritjesh();
        //        return colNiveleZbritjesh.mbushArrayListNiveleZbritjesh(ds);
        //    }
        //    catch (Exception)
        //    {
        //        return new colNiveleZbritjesh();
        //    }
        //    finally
        //    {
        //        dbManager.Dispose();
        //    }
        //}

        /// <summary>
        /// merr gjithe nivelet e zbritjes te nje prindi
        /// </summary>
        /// <param name="idprindi">id e nivelit prind</param>
        /// <returns>nje objekt colNiveleZbritjesh qe permban nje koleksion me te gjitha nivelet e zbritjes  bij te ketij prindi</returns>
        internal DataTable ktheNivelZbritjeSipasPrindit(int idprindi)
        {

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDPRINDI", idprindi, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_NIVELZBRITJE_merrNivelZbritjeSipasPrindit");
            return ds.Tables[0];

        }
        //[Obsolete("Perdor: DataTable merrNivelZbritjeSipasPrindit(int idprindi)", true)]
        //public colNiveleZbritjesh merrNivelZbritjeSipasPrindit(int idprindi)
        //{//metoda per te marre NIVEL ZBRITJE sipas primdit
        //    IDBManager dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        //    dbManager.ConnectionString = dbManager.GetConnectionString();
        //    try
        //    {
        //        dbManager.Open();
        //        dbManager.CreateParameters(1);
        //        dbManager.AddParameters(0, "@IDPRINDI", idprindi, ParameterDirection.Input);
        //        DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_NIVELZBRITJE_merrNivelZbritjeSipasPrindit");
        //        colNiveleZbritjesh colNiveleZbritjesh = new colNiveleZbritjesh();
        //        return colNiveleZbritjesh.mbushArrayListNiveleZbritjesh(ds);
        //    }
        //    catch (Exception)
        //    {
        //        return new colNiveleZbritjesh();
        //    }
        //    finally
        //    {
        //        dbManager.Dispose();
        //    }
        //}

        /// <summary>
        /// merr datarow nivelin e zbritjes  me kete pershkrim
        /// </summary>
        /// <param name="pershkrim"> pershkrimi i nivelit te zbritjes</param>
        /// <param name="idnderm"> id e ndermarjes</param>
        /// <returns> nje datarow qe permban nivelin e zbritjes me kete pershkrim te kesaj ndermarje</returns>
        internal DataRow ktheNivelZbritjeSipasPershkrimit(string pershkrim, int idnderm)
        {

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@PERSHKRIMNIVELZBRITJE", pershkrim, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_NIVELZBRITJE_merrNivelZbritjeSipasPershkrimit");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];

        }
        /// <summary>
        /// merr int id e nivelet te zbritjes  me kete pershkrim
        /// </summary>
        /// <param name="pershkrim"> pershkrimi i nivelit te zbritjes</param>
        /// <param name="idnderm"> id e ndermarjes</param>
        /// <returns> nje int qe permban nivelin e zbritjes me kete pershkrim te kesaj ndermarje</returns>
        internal int ktheIDNivelZbritjeSipasPershkrimit(string pershkrim, int idnderm)
        {

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@PERSHKRIMNIVELZBRITJE", pershkrim, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_NIVELZBRITJE_merrNivelZbritjeSipasPershkrimit");
            if (ds == null)
                return -1;
            if (ds.Tables[0].Rows.Count == 0)
                return -1;
            int idMaturimi;
            int.TryParse(ds.Tables[0].Rows[0]["IDNIVELZBRITJE"].ToString(), out idMaturimi);
            return idMaturimi;

        }
        //[Obsolete("Perdor: DataTable merrNivelZbritjeSipasPershkrimit(string pershkrim, int idnderm)", true)]
        //public colNiveleZbritjesh merrNivelZbritjeSipasPershkrimit(string pershkrim, int idnderm)
        //{//metoda per te marre NIVEL ZBRITJE sipas pershkrimit
        //    IDBManager dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        //    dbManager.ConnectionString = dbManager.GetConnectionString();
        //    try
        //    {
        //        dbManager.Open();
        //        dbManager.CreateParameters(2);
        //        dbManager.AddParameters(0, "@PERSHKRIMNIVELZBRITJE", pershkrim, ParameterDirection.Input);
        //        dbManager.AddParameters(1, "@IDNDERMARJE", idnderm , ParameterDirection.Input);
        //        DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_NIVELZBRITJE_merrNivelZbritjeSipasPershkrimit");
        //        colNiveleZbritjesh colNiveleZbritjesh = new colNiveleZbritjesh();
        //        return colNiveleZbritjesh.mbushArrayListNiveleZbritjesh(ds);
        //    }
        //    catch (Exception)
        //    {
        //        return new colNiveleZbritjesh();
        //    }
        //    finally
        //    {
        //        dbManager.Dispose();
        //    }
        //}

        /// <summary>
        /// perdoret per te kontrolluar nqs ekziston nje nivel zbritje me kete kod
        /// behet kontrolli per qellime konsistence te informacionit ne DB, nuk lejon te celen nivele zbritje te ndryshem me te njejtin kod
        /// </summary>
        ///<param name="idndermarje"> id e ndermarjes</param>
        ///<param name="kod"> kodi i nivelit te zbritjes</param>
        /// <returns>nje objeckt boolean qe tregon nese ekziston apo jo nje nivel zbritje me kete kod</returns>
        public bool ekzistonNivelZbritje(String kod, int idndermarje)
        {//kontrollon nqs ekziston nje nivelzbritje me kete kod

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@KODNIVELZBRITJE", kod, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idndermarje, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_NIVELZBRITJE_existon");
            if (ds.Tables[0].Rows.Count == 1)
                return true;
            else if (ds.Tables[0].Rows.Count == 0)
                return false;
            else return true;

        }

        /// <summary>
        /// perdoret per te kontrolluar nese nje nivel zbritje ka bij. perdoret ne rastet e fshirjes se nivelit te zbritjes per te mos lejuar te fshihet nje nivel prind
        /// </summary>
        /// <param name="idprindi"> id e nivelit</param>
        /// <returns> kthen nje objekt boolean qe tregon nese ky nivel ka nivele bij apo jo</returns>
        public bool kaBijNivelZbritje(int idprindi)
        {//kontrollon nqs ky nivelcmimi ka bij

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDPRINDI", idprindi, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_NIVELZBRITJE_kaBij");
            if (ds.Tables[0].Rows.Count == 1)
                return true;
            else if (ds.Tables[0].Rows.Count == 0)
                return false;
            else return true;

        }
        internal DataRow merrNivelZbritjeSipasNdermarjesDR(int idnderm, int idnivel)
        {


            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNIVELZBRITJE", idnivel, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_NIVELZBRITJE_merrNivelZbritjeSipasNdermarjesDR");

            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];

        }
        internal DataTable merrNiveleZbritjeshNdermarjeDT(int idnderm)
        {


            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_NIVELZBRITJE_merrNiveleZbritjeshNdermarjeDT");

            return ds.Tables[0];

        }

        internal DataTable MerrNivelZbritjeSipasNdermarrjesAc(int idNdermarrje, string kodi)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDNDERMARJE", idNdermarrje, ParameterDirection.Input);
            dbManager.AddParameters(1, "@KODI", kodi, ParameterDirection.Input);

            return dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_NIVELZBRITJE_MerrNivelZbritjeJoPrindSipasNdermarjesAc").Tables[0];
        }

        #endregion

        /// <summary>
        /// Metodat e meposhtme i perkasin veprimeve qe kryhen nga objektet clsZbritjeAnalitike dhe colZbritjetAnalitike
        /// </summary>
        #region ZBRITJE ANALITIKE

        /// <summary>
        /// perdoret per te ruajtur zbritjet e artikujve. Perdoret nje transaksion ne menyre qe te ruhen te gjitha zbritjet.
        /// Nqs ekziston nje zbritje per kete artikull atehere modifikohet zbritja e tij perndryshe ruhet artikulli me zbritjen e re
        ///<param name="zbritjetAnalitike"> koleksioni me zbritjet e artikujve qe do te ruhen</param>
        /// <returns> nje objekt clsMesazh qe tregon nese ruajtja eshte kryer ne rregull apo jo.</returns>
        /// </summary>
        //    [Obsolete("Perdor nga klasa perkatese: clsMesazh ruajZbritje(colZbritjetAnalitike  zbritjetAnalitike)", true)]
        //    public clsMesazh ruajZbritje(colZbritjetAnalitike  zbritjetAnalitike)
        //    {
        //        dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        //        dbManager.ConnectionString = dbManager.GetConnectionString();
        //        dbManager.Open();
        //        dbManager.BeginTransaction();
        //        clsMesazh mesazh = new clsMesazh(true);
        //        try
        //        {
        //            foreach (clsZbritjeAnalitike  c in zbritjetAnalitike)
        //            {
        //                if (mesazh.Status)
        //                {
        //                    if (c.IdZbritjeAnalitike == 0)
        //                    {
        //                        int idZ;
        //                        mesazh = ruajZbritjeAnalitike(out idZ, c.IdArtikulli, c.IdNivelZbritje, c.IdNjesia, c.DateFillimi, c.DateMbarimi, c.SasiMin, c.SasiMax,
        //                            c.VleftaMin, c.VleftaMax, c.LlojZbritje, c.Zbritja, c.IdPerdoruesi, c.IdNderViti, c.IdNdermarje, c.IdKonfig);
        //                        //mesazh= ruajZbritjeAnalitike(c);
        //                    }
        //                    else
        //                        mesazh = modifikoZbritjeAnalitike(c.IdZbritjeAnalitike, c.IdArtikulli, c.IdNivelZbritje, c.IdNjesia, c.DateFillimi, c.DateMbarimi, c.SasiMin, c.SasiMax,
        //c.VleftaMin, c.VleftaMax, c.LlojZbritje, c.Zbritja, c.IdPerdoruesi, c.IdNderViti, c.IdNdermarje, c.IdKonfig);
        //                        //mesazh= modifikoZbritjeAnalitike(c);
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
        //        catch (Exception)
        //        {
        //            dbManager.Transaction.Rollback();
        //            return new clsMesazh(false, ce.Message);
        //        }
        //        finally
        //        {
        //            dbManager.Dispose();
        //        }
        //    }

        /// <summary>
        /// ekzekuton prc_T_ZBRITJEANALITIKE_ins ne DB dhe kthen statusin e perfundimit te ekzekutimit te SP-se perkatese
        /// Shiko clsMesazh brenda ketij moduli per informacion te metejshem mbi vleren e kthimit te kesaj metode
        /// <param name="idZbritjaAnalitike"> id ritese e zbritjes analitike</param>
        /// <param name="idArtikulli"> id e artikullit</param>
        /// <param name="idNivelZbritje"> id e nivelit te zbritjes</param>
        /// <param name="idNjesia"> id e njesise</param>
        /// <param name="dateFillimit"> data e fillimit</param>
        /// <param name="dateMbarimit"> data e mbarimit</param>
        /// <param name="sasiMin"> sasi min</param>
        /// <param name="sasiMax"> sasi max</param>
        /// <param name="vleftaMin"> vlefta min</param>
        /// <param name="vleftaMax"> vlefta max</param>
        /// <param name="llojZbritje"> lloj zbritje</param>
        /// <param name="zbritja"> zbritja</param>
        /// <param name="idPerdoruesi">id perdoruesi</param>
        /// <param name="idNderViti"> id nderviti</param>
        /// <param name="idNdermarje"> id ndermarje</param>
        /// <param name="idkonfig">id e konfigurimit</param>
        /// <returns> nje objekt clsMesazh qe tregon nese ruajtja eshte kryer ne rregull apo jo.</returns>
        /// </summary>
        internal clsMesazh ruajZbritjeAnalitike(out int idZbritjaAnalitike, int idArtikulli, int idNivelZbritje, int idNjesia, DateTime dateFillimit, DateTime dateMbarimit,
            decimal sasiMin, decimal sasiMax, decimal vleftaMin, decimal vleftaMax, int llojZbritje, decimal zbritja, int idPerdoruesi, int idNdermarje,
            int idkonfig, int idstatusdok, int idnjesia2, decimal zbritja2)
        { //metoda per ruajtjen e zbritjeAnalitike
            idZbritjaAnalitike = -1;

            dbManager.Open();

            //shtimi i parametrave
            dbManager.CreateParameters(18);
            dbManager.AddParameters(0, "@IDZBRITJEANALITIKE", idZbritjaAnalitike, ParameterDirection.Output);
            dbManager.AddParameters(1, "@IDARTIKULLI", idArtikulli, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDNIVELZBRITJE", idNivelZbritje, ParameterDirection.Input);
            dbManager.AddParameters(3, "@IDNJESIA", idNjesia, ParameterDirection.Input);
            dbManager.AddParameters(4, "@DTFILLIMIT", dateFillimit, ParameterDirection.Input);
            dbManager.AddParameters(5, "@DTMBARIMIT", dateMbarimit, ParameterDirection.Input);
            dbManager.AddParameters(6, "@SASIMIN", sasiMin, ParameterDirection.Input);
            dbManager.AddParameters(7, "@SASIMAX", sasiMax, ParameterDirection.Input);
            dbManager.AddParameters(8, "@VLEFTAMIN", vleftaMin, ParameterDirection.Input);
            dbManager.AddParameters(9, "@VLEFTAMAX", vleftaMax, ParameterDirection.Input);
            dbManager.AddParameters(10, "@LLOJZBRITJE", llojZbritje, ParameterDirection.Input);
            dbManager.AddParameters(11, "@ZBRITJA", zbritja, ParameterDirection.Input);
            dbManager.AddParameters(12, "@IDPERDORUESI", idPerdoruesi, ParameterDirection.Input);
            //dbManager.AddParameters(13, "@IDNDERVITI", idNderViti, ParameterDirection.Input);
            dbManager.AddParameters(13, "@IDNDERMARJE", idNdermarje, ParameterDirection.Input);
            dbManager.AddParameters(14, "@IDKONFIG", idkonfig, ParameterDirection.Input);
            dbManager.AddParameters(15, "@IDSTATUSDOK", idstatusdok, ParameterDirection.Input);
            dbManager.AddParameters(16, "@IDNJESIA2", idnjesia2, ParameterDirection.Input);
            dbManager.AddParameters(17, "@ZBRITJA2", zbritja2, ParameterDirection.Input);

            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_ZBRITJEANALITIKE_ins");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
            return mesazh;


        }
        //[Obsolete("Perdor: clsMesazh ruajZbritjeAnalitike(int idZbritjaAnalitike, int idArtikulli, int idNivelZbritje, int idNjesia, DateTime dateFillimit, DateTime dateMbarimit, " +  
        //    "decimal sasiMin, decimal sasiMax, decimal vleftaMin, decimal vleftaMax, int zbritjeNeVlere, decimal zbritja, int idPerdoruesi, int idNderViti, int idNdermarje, " + 
        //    "int idkonfig)",true)]
        //public clsMesazh ruajZbritjeAnalitike(clsZbritjeAnalitike zbritjeAnalitike)
        //{ //metoda per ruajtjen e zbritjeAnalitike


        //    IDBManager dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        //    dbManager.ConnectionString = dbManager.GetConnectionString();
        //    try
        //    {
        //        dbManager.Open();

        //        //shtimi i parametrave
        //        dbManager.CreateParameters(16);
        //        dbManager.AddParameters(0, "@IDZBRITJEANALITIKE", zbritjeAnalitike.IdZbritjeAnalitike, ParameterDirection.Output);
        //        dbManager.AddParameters(1, "@IDARTIKULLI", zbritjeAnalitike.IdArtikulli, ParameterDirection.Input);
        //        dbManager.AddParameters(2, "@IDNIVELZBRITJE", zbritjeAnalitike.IdNivelZbritje, ParameterDirection.Input);
        //        dbManager.AddParameters(3, "@IDNJESIA", zbritjeAnalitike.IdNjesia, ParameterDirection.Input);
        //            dbManager.AddParameters(4, "@DTFILLIMIT", zbritjeAnalitike.DateFillimi, ParameterDirection.Input);
        //        dbManager.AddParameters(5, "@DTMBARIMIT", zbritjeAnalitike.DateMbarimi, ParameterDirection.Input);
        //        dbManager.AddParameters(6, "@SASIMIN", zbritjeAnalitike.SasiMin, ParameterDirection.Input);
        //        dbManager.AddParameters(7, "@SASIMAX", zbritjeAnalitike.SasiMax, ParameterDirection.Input);
        //        dbManager.AddParameters(8, "@VLEFTAMIN", zbritjeAnalitike.VleftaMin, ParameterDirection.Input);
        //        dbManager.AddParameters(9, "@VLEFTAMAX", zbritjeAnalitike.VleftaMax, ParameterDirection.Input);
        //        dbManager.AddParameters(10, "@LLOJZBRITJE", zbritjeAnalitike.LlojZbritje, ParameterDirection.Input);
        //        dbManager.AddParameters(11, "@ZBRITJA", zbritjeAnalitike.Zbritja, ParameterDirection.Input);
        //        dbManager.AddParameters(12, "@IDPERDORUESI", zbritjeAnalitike.IdPerdoruesi, ParameterDirection.Input);
        //        dbManager.AddParameters(13, "@IDNDERVITI", zbritjeAnalitike.IdNderViti, ParameterDirection.Input);
        //        dbManager.AddParameters(14, "@IDNDERMARJE", zbritjeAnalitike.IdNdermarje, ParameterDirection.Input);
        //        dbManager.AddParameters(15, "@IDKONFIG", zbritjeAnalitike.IdKonfig, ParameterDirection.Input);
        //        dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_ZBRITJEANALITIKE_ins");
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
        /// ekzekuton prc_T_ZBRITJEANALITIKE_upd ne DB dhe kthen statusin e perfundimit te ekzekutimit te SP-se perkatese
        /// Shiko clsMesazh brenda ketij moduli per informacion te metejshem mbi vleren e kthimit te kesaj metode
        /// <param name="idZbritjaAnalitike"> id ritese e zbritjes analitike</param>
        /// <param name="idArtikulli"> id e artikullit</param>
        /// <param name="idNivelZbritje"> id e nivelit te zbritjes</param>
        /// <param name="idNjesia"> id e njesise</param>
        /// <param name="dateFillimit"> data e fillimit</param>
        /// <param name="dateMbarimit"> data e mbarimit</param>
        /// <param name="sasiMin"> sasi min</param>
        /// <param name="sasiMax"> sasi max</param>
        /// <param name="vleftaMin"> vlefta min</param>
        /// <param name="vleftaMax"> vlefta max</param>
        /// <param name="llojZbritje"> lloj zbritje</param>
        /// <param name="zbritja"> zbritja</param>
        /// <param name="idPerdoruesi">id perdoruesi</param>
        /// <param name="idNderViti"> id nderviti</param>
        /// <param name="idNdermarje"> id ndermarje</param>
        /// <param name="idkonfig">id e konfigurimit</param>
        /// <returns> nje objekt clsMesazh qe tregon nese modifikimi eshte kryer ne rregull apo jo.</returns>
        /// </summary>
        internal clsMesazh modifikoZbritjeAnalitike(int idZbritjaAnalitike, int idArtikulli, int idNivelZbritje, int idNjesia, DateTime dateFillimit, DateTime dateMbarimit,
            decimal sasiMin, decimal sasiMax, decimal vleftaMin, decimal vleftaMax, int llojZbritje, decimal zbritja, int idPerdoruesi, int idNdermarje,
            int idkonfig, int idstatusdok, int idnjesia2, decimal zbritja2)
        {
            dbManager.Open();

            //shtimi i parametrave
            dbManager.CreateParameters(18);
            dbManager.AddParameters(0, "@IDZBRITJEANALITIKE", idZbritjaAnalitike, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDARTIKULLI", idArtikulli, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDNIVELZBRITJE", idNivelZbritje, ParameterDirection.Input);
            dbManager.AddParameters(3, "@IDNJESIA", idNjesia, ParameterDirection.Input);
            dbManager.AddParameters(4, "@DTFILLIMIT", dateFillimit, ParameterDirection.Input);
            dbManager.AddParameters(5, "@DTMBARIMIT", dateMbarimit, ParameterDirection.Input);
            dbManager.AddParameters(6, "@SASIMIN", sasiMin, ParameterDirection.Input);
            dbManager.AddParameters(7, "@SASIMAX", sasiMax, ParameterDirection.Input);
            dbManager.AddParameters(8, "@VLEFTAMIN", vleftaMin, ParameterDirection.Input);
            dbManager.AddParameters(9, "@VLEFTAMAX", vleftaMax, ParameterDirection.Input);
            dbManager.AddParameters(10, "@LLOJZBRITJE", llojZbritje, ParameterDirection.Input);
            dbManager.AddParameters(11, "@ZBRITJA", zbritja, ParameterDirection.Input);
            dbManager.AddParameters(12, "@IDPERDORUESI", idPerdoruesi, ParameterDirection.Input);
            //dbManager.AddParameters(13, "@IDNDERVITI", idNderViti, ParameterDirection.Input);
            dbManager.AddParameters(13, "@IDNDERMARJE", idNdermarje, ParameterDirection.Input);
            dbManager.AddParameters(14, "@IDKONFIG", idkonfig, ParameterDirection.Input);
            dbManager.AddParameters(15, "@IDSTATUSDOK", idstatusdok, ParameterDirection.Input);
            dbManager.AddParameters(16, "@IDNJESIA2", idnjesia2, ParameterDirection.Input);
            dbManager.AddParameters(17, "@ZBRITJA2", zbritja2, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_ZBRITJEANALITIKE_upd");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
            return mesazh;


        }

        internal void ruajZbritjeAnalitikeDT(DataTable zbritjetPerTuRuajtur)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@ZBRITJE_NDRYSHUAR", zbritjetPerTuRuajtur, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "PRC_T_ZBRITJEANALITIKE_MERGEDT");

        }

        //[Obsolete("Perdor: clsMesazh modifikoZbritjeAnalitike(int idZbritjaAnalitike, int idArtikulli, int idNivelZbritje, int idNjesia, DateTime dateFillimit, DateTime dateMbarimit, " +
        //    "decimal sasiMin, decimal sasiMax, decimal vleftaMin, decimal vleftaMax, int zbritjeNeVlere, decimal zbritja, int idPerdoruesi, int idNderViti, int idNdermarje, " +
        //    "int idkonfig)", true)]
        //public clsMesazh modifikoZbritjeAnalitike(clsZbritjeAnalitike zbritjeAnalitike)
        //{
        //    IDBManager dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        //    dbManager.ConnectionString = dbManager.GetConnectionString();
        //    try
        //    {
        //        dbManager.Open();

        //        //shtimi i parametrave
        //        dbManager.CreateParameters(16);
        //        dbManager.AddParameters(0, "@IDZBRITJEANALITIKE", zbritjeAnalitike.IdZbritjeAnalitike, ParameterDirection.Input);
        //        dbManager.AddParameters(1, "@IDARTIKULLI", zbritjeAnalitike.IdArtikulli, ParameterDirection.Input);
        //        dbManager.AddParameters(2, "@IDNIVELZBRITJE", zbritjeAnalitike.IdNivelZbritje, ParameterDirection.Input);
        //        dbManager.AddParameters(3, "@IDNJESIA", zbritjeAnalitike.IdNjesia, ParameterDirection.Input);
        //        dbManager.AddParameters(4, "@DTFILLIMIT", zbritjeAnalitike.DateFillimi, ParameterDirection.Input);
        //        dbManager.AddParameters(5, "@DTMBARIMIT", zbritjeAnalitike.DateMbarimi, ParameterDirection.Input);
        //        dbManager.AddParameters(6, "@SASIMIN", zbritjeAnalitike.SasiMin, ParameterDirection.Input);
        //        dbManager.AddParameters(7, "@SASIMAX", zbritjeAnalitike.SasiMax, ParameterDirection.Input);
        //        dbManager.AddParameters(8, "@VLEFTAMIN", zbritjeAnalitike.VleftaMin, ParameterDirection.Input);
        //        dbManager.AddParameters(9, "@VLEFTAMAX", zbritjeAnalitike.VleftaMax, ParameterDirection.Input);
        //        dbManager.AddParameters(10, "@LLOJZBRITJE", zbritjeAnalitike.LlojZbritje, ParameterDirection.Input);
        //        dbManager.AddParameters(11, "@ZBRITJA", zbritjeAnalitike.Zbritja, ParameterDirection.Input);
        //        dbManager.AddParameters(12, "@IDPERDORUESI", zbritjeAnalitike.IdPerdoruesi, ParameterDirection.Input);
        //        dbManager.AddParameters(13, "@IDNDERVITI", zbritjeAnalitike.IdNderViti, ParameterDirection.Input);
        //        dbManager.AddParameters(14, "@IDNDERMARJE", zbritjeAnalitike.IdNdermarje, ParameterDirection.Input);
        //        dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_ZBRITJEANALITIKE_upd");
        //        dbManager.AddParameters(15, "@IDKONFIG", zbritjeAnalitike.IdKonfig, ParameterDirection.Input);
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
        /// ekzekutohet sp-ja prc_T_ZBRITJEANALITIKE_del duke i kaluar id e zbritjes analitike te artikullit qe e marrim nga objekti clsZbritjeAnalitike qe i kalohet si parameter
        /// </summary>
        /// <param name="idZbritjeAnalitike"> zbritja analitike e  artikullit qe do fshihet </param>
        /// <returns>nje objekt clsMesazh qe tregon nese fshirja eshte kryer ne rregull apo jo</returns>
        internal clsMesazh fshiZbritjeAnalitike(int idZbritjeAnalitike)
        {//metoda per fshirjen e ZbritjeAnalitike

            dbManager.Open();
            //shtimi i parametrave
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDZBRITJEANALITIKE", idZbritjeAnalitike, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_ZBRITJEANALITIKE_del");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
            return mesazh;

        }
        internal clsMesazh fshiZbritjeAnalitikeStatus(int idZbritjeAnalitike, int idperdoruesi)
        {//metoda per fshirjen e ZbritjeAnalitike

            dbManager.Open();
            //shtimi i parametrave
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDZBRITJEANALITIKE", idZbritjeAnalitike, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUESI", idperdoruesi, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_ZBRITJEANALITIKE_upddel");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
            return mesazh;

        }
        //[Obsolete("Perdor: clsMesazh fshiZbritjeAnalitike(int idZbritjeAnalitike)", true)]
        //public clsMesazh fshiZbritjeAnalitike(clsZbritjeAnalitike zbritjeAnalitike)
        //{//metoda per fshirjen e ZbritjeAnalitike
        //    IDBManager dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        //    dbManager.ConnectionString = dbManager.GetConnectionString();
        //    try
        //    {
        //        dbManager.Open();
        //        //shtimi i parametrave
        //        dbManager.CreateParameters(1);
        //        dbManager.AddParameters(0, "@IDZBRITJEANALITIKE", zbritjeAnalitike.IdZbritjeAnalitike, ParameterDirection.Input);
        //        dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_ZBRITJEANALITIKE_del");
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
        /// kthen datarow zbritje analitike  sipas idse
        /// </summary>
        ///<param name="idZbritjeAnalitike"> zbritja analitike e artikullit qe do i merret id</param>
        ///<returns> nje datarow me zbritjen analitike te artikullit me kete id</returns>
        internal DataRow ktheZbritjeAnalitike(int idZbritjeAnalitike)
        {
            dbManager.Open();
            //shtimi i parametrave
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDZBRITJEANALITIKE", idZbritjeAnalitike, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_ZBRITJEANALITIKE_sel");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];
        }

        internal int ktheIdZbritjeAnalitikeSipasArtikullitDheNivelit(int idNivelZbritje, int idArtikulli, int idNdermarrje)
        {
            dbManager.Open();
            //shtimi i parametrave
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@IDARTIKULLI", idArtikulli, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNIVELZBRITJE", idNivelZbritje, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDNDERMARJE", idNdermarrje, ParameterDirection.Input);
            return Convert.ToInt32(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_ZBRITJEANALITIKE_ktheIdZbritjeSipasNivelitDheArtikullit"));
        }

        //[Obsolete("Perdor: DataRow ktheZbritjeAnalitike(int idZbritjeAnalitike)", true)]
        //public colZbritjetAnalitike merrZbritjeAnalitike(clsZbritjeAnalitike zbritjeAnalitike)
        //{// metoda per te marre nje ZbritjeAnalitike NE BAZE TE ID
        //    IDBManager dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        //    dbManager.ConnectionString = dbManager.GetConnectionString();
        //    try
        //    {
        //        dbManager.Open();
        //        //shtimi i parametrave
        //        dbManager.CreateParameters(1);
        //        dbManager.AddParameters(0, "@IDZBRITJEANALITIKE", zbritjeAnalitike.IdZbritjeAnalitike, ParameterDirection.Input);
        //        DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_ZBRITJEANALITIKE_sel");
        //        colZbritjetAnalitike colZbritjetAnalitike = new colZbritjetAnalitike();
        //        return colZbritjetAnalitike.mbushArrayListZbritjeshAnalitike(ds);
        //    }
        //    catch (Exception)
        //    {
        //        return new colZbritjetAnalitike();
        //    }
        //    finally
        //    {
        //        dbManager.Dispose();
        //    }
        //}

        /// <summary>
        /// kthen datatable zbritje analitike artikujsh sipas filtrave te kaluar si parametra
        /// </summary>
        /// <param name="kodartikulli"> kodin e artikullit</param>
        /// <param name="kodbar"> kodbarin</param>
        /// <param name="pershkrimi1">pershkrimin 1 te artikullit</param>
        /// <param name="pershkrimi2"> pershkrimin 2 te artikullit</param>
        /// <param name="kodifikimi1"> kodifikimin 1 te artikullit</param>
        /// <param name="kodifikimi2">kodifikimin 2 te artikullit</param>
        /// <param name="furnitori"> furnitorin e artikullit</param>
        /// <param name="njesia"> njesine e pare ose te dyte  te artikullit</param>
        /// <param name="datafillimit"> daten e fillimit te cmimit</param>
        /// <param name="datambarimit"> daten e mbarimit te cmimit</param>
        /// <param name="idndervit"> id e ndermarje vitit</param>
        /// <param name="idnivelzbritje"> id e nivelit te zbritjes</param>
        /// <param name="idperdorues"> id e perdoruesit</param>
        /// <param name="idndermarje"> id e ndermarjes</param>
        /// <returns> nje datatable qe permban nje koleksion me te gjithe zbritjet analitike te artikujve qe plotesojne filtrat e kaluar si parametra</returns>
        internal DataTable ktheZbritjeAnalitikeSipasFiltrit(string kodartikulli, string kodbar, string pershkrimi1, string pershkrimi2, string kodifikimi1, string kodifikimi2, string furnitori, string njesia, string datafillimit, string datambarimit, string idnivelzbritje, int idperdorues, int idndermarje)
        {

            DateTime dtfill = new DateTime();
            DateTime dtmbar = new DateTime();
            dbManager.Open();
            //shtimi i parametrave
            dbManager.CreateParameters(13);
            if (datafillimit != "")
                dtfill = DateTime.Parse(datafillimit);
            if (datambarimit != "")
                dtmbar = DateTime.Parse(datambarimit);

            dbManager.AddParameters(0, "@KODARTIKULLI", kodartikulli, ParameterDirection.Input);
            dbManager.AddParameters(1, "@KODBARARTIKULLI", kodbar, ParameterDirection.Input);
            dbManager.AddParameters(2, "@PERSHKRIMARTIKULLI", pershkrimi1, ParameterDirection.Input);
            dbManager.AddParameters(3, "@PERSHKRIMANGARTIKULLI", pershkrimi2, ParameterDirection.Input);
            dbManager.AddParameters(4, "@KODIFIKIMARTIKULLI1 ", kodifikimi1, ParameterDirection.Input);
            dbManager.AddParameters(5, "@KODIFIKIMARTIKULLI2", kodifikimi2, ParameterDirection.Input);
            dbManager.AddParameters(6, "@FURNITORI", furnitori, ParameterDirection.Input);
            //dbManager.AddParameters(7, "@IDNDERVITI", idndervit, ParameterDirection.Input);
            dbManager.AddParameters(7, "@NJESIA", njesia, ParameterDirection.Input);
            if (datafillimit == "")
                dbManager.AddParameters(8, "@DATAFILLIMIT", datafillimit, ParameterDirection.Input);
            else
                dbManager.AddParameters(8, "@DATAFILLIMIT", dtfill, ParameterDirection.Input);
            if (datambarimit == "")
                dbManager.AddParameters(9, "@DATAMBARIMIT", datambarimit, ParameterDirection.Input);
            else
                dbManager.AddParameters(9, "@DATAMBARIMIT", dtmbar, ParameterDirection.Input);
            dbManager.AddParameters(10, "@IDNIVELZBRITJE", idnivelzbritje, ParameterDirection.Input);
            dbManager.AddParameters(11, "@IDPERDORUES", idperdorues, ParameterDirection.Input);
            dbManager.AddParameters(12, "@IDNDERMARJE", idndermarje, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_ZBRITJEANALITIKE_merrZbritjetSipasFiltrit");

            return ds.Tables[0];

        }

        internal DataTable ktheZbritjeAnalitikeDt(int idndermarje)
        {


            dbManager.Open();
            dbManager.CreateParameters(1);

            dbManager.AddParameters(0, "@IDNDERMARJE", idndermarje, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_ZBRITJEANALITIKE_merrZbritjetDt");

            return ds.Tables[0];

        }

        //[Obsolete("Perdor: DataTable ktheZbritjeAnalitikeSipasFiltrit(string kodartikulli, string kodbar, string pershkrimi1, string pershkrimi2, string kodifikimi1, string kodifikimi2, string furnitori, string njesia, string datafillimit, string datambarimit, int idndervit, string idnivelzbritje, int idperdorues, int idndermarje)", true)]
        //public colZbritjetAnalitike merrZbritjeAnalitikeSipasFiltrit(string kodartikulli, string kodbar, string pershkrimi1, string pershkrimi2, string kodifikimi1, string kodifikimi2, string furnitori, string njesia, string datafillimit, string datambarimit, int idndervit, string idnivelzbritje, int idperdorues, int idndermarje)
        //{// metoda per te marre nje ZbritjeAnalitike NE BAZE TE filtrave
        //    IDBManager dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        //    dbManager.ConnectionString = dbManager.GetConnectionString();
        //    try
        //    {
        //        DateTime dtfill = new DateTime();
        //        DateTime dtmbar = new DateTime();
        //        dbManager.Open();
        //        //shtimi i parametrave
        //        dbManager.CreateParameters(14);
        //        if (datafillimit != "")
        //            dtfill = DateTime.Parse(datafillimit);
        //        if (datambarimit != "")
        //            dtmbar = DateTime.Parse(datambarimit);

        //        dbManager.AddParameters(0, "@KODARTIKULLI", kodartikulli, ParameterDirection.Input);
        //        dbManager.AddParameters(1, "@KODBARARTIKULLI", kodbar, ParameterDirection.Input);
        //        dbManager.AddParameters(2, "@PERSHKRIMARTIKULLI", pershkrimi1, ParameterDirection.Input);
        //        dbManager.AddParameters(3, "@PERSHKRIMANGARTIKULLI", pershkrimi2, ParameterDirection.Input);
        //        dbManager.AddParameters(4, "@KODIFIKIMARTIKULLI1 ", kodifikimi1, ParameterDirection.Input);
        //        dbManager.AddParameters(5, "@KODIFIKIMARTIKULLI2", kodifikimi2, ParameterDirection.Input);
        //        dbManager.AddParameters(6, "@FURNITORI", furnitori, ParameterDirection.Input);
        //        dbManager.AddParameters(7, "@IDNDERVITI", idndervit, ParameterDirection.Input);
        //        dbManager.AddParameters(8, "@NJESIA", njesia, ParameterDirection.Input);
        //        if (datafillimit == "")
        //            dbManager.AddParameters(9, "@DATAFILLIMIT", datafillimit, ParameterDirection.Input);
        //        else
        //            dbManager.AddParameters(9, "@DATAFILLIMIT", dtfill, ParameterDirection.Input);
        //        if (datambarimit == "")
        //            dbManager.AddParameters(10, "@DATAMBARIMIT", datambarimit, ParameterDirection.Input);
        //        else
        //            dbManager.AddParameters(10, "@DATAMBARIMIT", dtmbar, ParameterDirection.Input);
        //        dbManager.AddParameters(11, "@IDNIVELZBRITJE", idnivelzbritje, ParameterDirection.Input);
        //        dbManager.AddParameters(12, "@IDPERDORUES", idperdorues, ParameterDirection.Input);
        //        dbManager.AddParameters(13, "@IDNDERMARJE", idndermarje, ParameterDirection.Input);
        //        DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_ZBRITJEANALITIKE_merrZbritjetSipasFiltrit");
        //        colZbritjetAnalitike colZbritjetAnalitike = new colZbritjetAnalitike();
        //        return colZbritjetAnalitike.mbushArrayListZbritjeshAnalitike(ds);
        //    }
        //    catch (Exception)
        //    {
        //        return new colZbritjetAnalitike();
        //    }
        //    finally
        //    {
        //        dbManager.Dispose();
        //    }
        //}

        /// <summary>
        /// kthen datatable zbritje analitike artikujsh sipas nivelit
        /// </summary>
        /// <param name="kodartikulli"> kodi i artikullit</param>
        /// <param name="idnivelzbritje"> id e nivelit te zbritjes</param>
        /// <param name="idperdorues"> id e perdoruesit</param>
        /// <param name="idndermarje"> id e ndermarjes</param>
        /// <returns>nje datatable qe permban nje koleksion me te gjitha zbritjet analitike te artikujve te ketij niveli</returns>
        internal DataTable ktheZbritjeAnalitikeSipasNivelit(string kodartikulli, string idnivelzbritje, int idperdorues, int idndermarje)
        {

            dbManager.Open();
            dbManager.CreateParameters(4);
            dbManager.AddParameters(0, "@KODARTIKULLI", kodartikulli, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNIVELZBRITJE", idnivelzbritje, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDPERDORUES", idperdorues, ParameterDirection.Input);
            dbManager.AddParameters(3, "@IDNDERMARJE", idndermarje, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_ZBRITJEANALITIKE_merrZbritjeAnalitikeSipasNivelit");

            return ds.Tables[0];

        }
        internal DataTable ktheZbritjeAnalitikeSipasNivelit(string kodartikulli, int idnivelzbritje, int idperdorues, int idndermarje)
        {

            dbManager.Open();
            dbManager.CreateParameters(4);
            dbManager.AddParameters(0, "@KODARTIKULLI", kodartikulli, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNIVELZBRITJE", idnivelzbritje, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDPERDORUES", idperdorues, ParameterDirection.Input);
            dbManager.AddParameters(3, "@IDNDERMARJE", idndermarje, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_ZBRITJEANALITIKE_merrZbritjeAnalitikeSipasNivelit");

            return ds.Tables[0];

        }
        //[Obsolete("Perdor: DataTable  ktheZbritjeAnalitikeSipasNivelit(string kodartikulli, string idnivelzbritje, int idperdorues, int idndermarje)", true)]
        //public colZbritjetAnalitike  merrZbritjeAnalitikeSipasNivelit(string kodartikulli, string idnivelzbritje, int idperdorues, int idndermarje)
        //{
        //    IDBManager dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        //    dbManager.ConnectionString = dbManager.GetConnectionString();
        //    try
        //    {
        //        dbManager.Open();
        //        dbManager.CreateParameters(4);
        //        dbManager.AddParameters(0, "@KODARTIKULLI", kodartikulli, ParameterDirection.Input);
        //        dbManager.AddParameters(1, "@IDNIVELZBRITJE", idnivelzbritje, ParameterDirection.Input);
        //        dbManager.AddParameters(2, "@IDPERDORUES", idperdorues, ParameterDirection.Input);
        //        dbManager.AddParameters(3, "@IDNDERMARJE", idndermarje, ParameterDirection.Input);
        //        DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_ZBRITJEANALITIKE_merrZbritjeAnalitikeSipasNivelit");
        //        colZbritjetAnalitike colZbritjeArtikujsh = new colZbritjetAnalitike();
        //        return colZbritjeArtikujsh.mbushArrayListZbritjeshAnalitike(ds);
        //    }
        //    catch (Exception)
        //    {
        //        return new colZbritjetAnalitike();
        //    }
        //    finally
        //    {
        //        dbManager.Dispose();
        //    }
        //}

        /// <summary>
        /// kthen dataTable zbritjesh analitike sipas ndermarrjes
        /// </summary>
        ///<param name="idNdermarrje"> id e ndermarrjes </param>
        ///<returns> nje datatable qe permban nje koleksion me te gjithe zbritjet analitike qe i perkasin kesaj ndermarrjeje</returns>
        internal DataTable ktheZbritjeAnalitikeDtExport(int idndermarje)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDNDERMARJE", idndermarje, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_ZBRITJEANALITIKE_merrSipasNdermarrjesDTExport");
            return ds.Tables[0];
        }

        #endregion

        /// <summary>
        /// Metodat e meposhtme i perkasin veprimeve qe kryhen nga objektet clsMaturimi dhe colMaturimet
        /// </summary>
        #region MATURIME

        /// <summary>
        /// ekzekuton prc_T_MATURIMI_ins ne DB dhe kthen statusin e perfundimit te ekzekutimit te SP-se perkatese
        /// Shiko clsMesazh brenda ketij moduli per informacion te metejshem mbi vleren e kthimit te kesaj metode
        /// <param name="kodMaturimi"> kodi i maturimit</param>
        /// <param name="pershkrimMaturimi"> pershrkimi i maturimit</param>
        /// <param name="llojMaturimi"> lloji i maturimit</param>
        /// <param name="idDateFillimi">id e dates se fillimit te periudhes se maturimit</param>
        /// <param name="idPeriudha"> id e periudhes</param>
        /// <param name="percaktimMaturimi"> percaktimi i maturimit</param>
        /// <param name="idNderViti"> id e ndermarje vitit</param>
        /// <param name="idPerdoruesi"> id e perdoruesit qe e ka kryer veprimin</param>
        /// <param name="idnderm"> id e ndermarjes</param>
        /// </summary>
        internal int RuajMaturim(string kodMaturimi, string pershkrimMaturimi, bool llojMaturimi, DateFillimiMaturiteti idDateFillimi, PeriudheMaturiteti idPeriudha, int percaktimMaturimi, int idPerdoruesi, int idnderm, int idstatusdok)
        {
            dbManager.Open();
            dbManager.CreateParameters(10);
            dbManager.AddParameters(0, "@IDMATURIMI", -1, ParameterDirection.Output);
            dbManager.AddParameters(1, "@KODMATURIMI", kodMaturimi, ParameterDirection.Input);
            dbManager.AddParameters(2, "@PERSHKRIMMATURIMI", pershkrimMaturimi, ParameterDirection.Input);
            dbManager.AddParameters(3, "@LLOJMATURIMI", llojMaturimi, ParameterDirection.Input);
            dbManager.AddParameters(4, "@IDDATEFILLIMI", idDateFillimi, ParameterDirection.Input);
            dbManager.AddParameters(5, "@IDPERIUDHA", idPeriudha, ParameterDirection.Input);
            dbManager.AddParameters(6, "@PERCAKTIMMATURIMI", percaktimMaturimi, ParameterDirection.Input);
            dbManager.AddParameters(7, "@IDPERDORUESI", idPerdoruesi, ParameterDirection.Input);
            dbManager.AddParameters(8, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            dbManager.AddParameters(9, "@IDSTATUSDOK", idstatusdok, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_MATURIMI_ins");
            return int.Parse(dbManager.Parameters[0].Value.ToString());
        }

        /// <summary>
        /// ekzekuton prc_T_MATURIMI_upd ne DB dhe kthen statusin e perfundimit te ekzekutimit te SP-se perkatese
        /// Shiko clsMesazh brenda ketij moduli per informacion te metejshem mbi vleren e kthimit te kesaj metode
        /// <param name="idMaturimi"> id ritese e maturimit</param>
        /// <param name="kodMaturimi"> kodi i maturimit</param>
        /// <param name="pershkrimMaturimi"> pershrkimi i maturimit</param>
        /// <param name="llojMaturimi"> lloji i maturimit</param>
        /// <param name="idDateFillimi">id e dates se fillimit te periudhes se maturimit</param>
        /// <param name="idPeriudha"> id e periudhes</param>
        /// <param name="percaktimMaturimi"> percaktimi i maturimit</param>
        /// <param name="idNderViti"> id e ndermarje vitit</param>
        /// <param name="idPerdoruesi"> id e perdoruesit qe e ka kryer veprimin</param>
        /// <param name="idnderm"> id e ndermarjes</param>
        /// <returns> nje objekt boolean qe tregon nese modifikimi eshte kryer ne rregull apo jo.</returns>
        /// </summary>
        internal void ModifikoMaturim(int idMaturimi, string kodMaturimi, string pershkrimMaturimi, bool llojMaturimi, DateFillimiMaturiteti idDateFillimi, PeriudheMaturiteti idPeriudha, int percaktimMaturimi, int idPerdoruesi, int idnderm, int idstatusdok)
        {
            dbManager.Open();
            dbManager.CreateParameters(10);
            dbManager.AddParameters(0, "@IDMATURIMI", idMaturimi, ParameterDirection.Input);
            dbManager.AddParameters(1, "@KODMATURIMI", kodMaturimi, ParameterDirection.Input);
            dbManager.AddParameters(2, "@PERSHKRIMMATURIMI", pershkrimMaturimi, ParameterDirection.Input);
            dbManager.AddParameters(3, "@LLOJMATURIMI", llojMaturimi, ParameterDirection.Input);
            dbManager.AddParameters(4, "@IDDATEFILLIMI", idDateFillimi, ParameterDirection.Input);
            dbManager.AddParameters(5, "@IDPERIUDHA", idPeriudha, ParameterDirection.Input);
            dbManager.AddParameters(6, "@PERCAKTIMMATURIMI", percaktimMaturimi, ParameterDirection.Input);
            dbManager.AddParameters(7, "@IDPERDORUESI", idPerdoruesi, ParameterDirection.Input);
            dbManager.AddParameters(8, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            dbManager.AddParameters(9, "@IDSTATUSDOK", idstatusdok, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_MATURIMI_upd");
        }

        internal clsMesazh FshiMaturim(int idMaturimi, int idperdoruesi)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDMATURIMI", idMaturimi, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUESI", idperdoruesi, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_MATURIMI_upddel");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
            return mesazh;
        }

        /// <summary>
        /// kthen datarow maturimi sipas idse
        /// </summary>
        ///<param name="id"> id e maturimit</param>
        ///<returns> nje datarow qe permban maturimin me kete id</returns>
        internal DataRow MerrMaturim(int id)
        {

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDMATURIMI", id, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_MATURIMI_ktheMaturimSipasID");

            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];

        }

        internal DataRow MerrMaturimDr(int id)
        {

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDMATURIMI", id, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_MATURIMI_merrMaturimSipasNdermarrjesDR");

            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];

        }

        /// <summary>
        /// kthen objektet maturimi sipas kodit
        /// </summary>
        /// <param name="kod"> kodi i maturimit</param>
        /// <param name="idndermarje"> id e ndermarjes</param>
        /// <returns> nje  objekt colMaturimet qe permban nje koleksion me te gjithe maturimet me kete kod te kesaj ndermarje</returns>
        internal int MerrIdMaturim(string kod, int idndermarje)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@KODMATURIMI", kod, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idndermarje, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_MATURIMI_ktheMaturimSipasKodit");

            if (ds == null)
                return -1;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return -1;

            int.TryParse(ds.Tables[0].Rows[0]["IDMATURIMI"].ToString(), out int idMaturimi);

            return idMaturimi;
        }

        /// <summary>
        /// perdoret per te kontrolluar nqs ekziston nje maturim me kete kod ne kete ndermarje
        /// behet kontrolli per qellime konsistence te informacionit ne DB, nuk lejon te celen maturime te ndryshem me te njejtin kod
        /// </summary>
        ///<param name="idnderm"> id e ndermarjes</param>
        ///<param name="kodi"> kodi i maturimit</param>
        /// <returns>nje objekt boolean qe tregon nese ekziston apo jo nje maturim me kete kod</returns>
        public bool EkzistonMaturim(string kodi, int idnderm)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@KODMATURIMI", kodi, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_MATURIMI_ekzistonMaturim");
            if (ds.Tables[0].Rows.Count == 1)
                return true;
            else if (ds.Tables[0].Rows.Count == 0)
                return false;
            else
                return true;
        }

        /// <summary>
        /// kthen datatable maturimi per klientet ose per furnitoret
        /// </summary>
        /// <param name="lloji">lloji klient apo furnitor true-klient false-furnitor</param>
        /// <param name="idndermvit"> id e ndermarje vitit</param>
        /// <param name="idperdorues"> id e perdoruesit</param>
        /// <returns> nje datatable qe permban nje koleksion me te gjithe maturimet per klientet/ furnitoret te nje ndermarje sipas autorizimeve</returns>
        internal DataTable MerrMaturimKlientiOseFurnitori(bool lloji, int idnderm)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@LLOJMATURIMI", lloji, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idnderm, ParameterDirection.Input);

            return dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_MATURIMI_merrMaturimKlientiOseFurnitori").Tables[0];
        }

        /// <summary>
        /// perdoret per te kontrolluar nese ky maturim  eshte i lidhur me klient furnitor.
        /// kjo behet per te mos lejuar te fshihet nje maturim te lidhur me nje klient furnitor
        /// </summary>
        /// <param name="id"> id e maturimit</param>
        /// <returns> nje objekt boolean qe tregon nese ky maturim eshte i lidhur me nje klient furnitor apo jo</returns>
        public bool KaVeprimeMaturimi(int id)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDMATURIMI", id, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_MATURIMI_kaVeprime");
            if (ds.Tables[0].Rows.Count == 1)
                return true;
            else if (ds.Tables[0].Rows.Count == 0)
                return false;
            else
                return true;
        }

        internal DataTable MerrMaturimeSipasNdermarrjes(int idnderm)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            return dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_MATURIMI_merrMaturimSipasNdermarrjes").Tables[0];
        }

        internal DataTable MerrMaturimSipasNdermarrjesDt(int idnderm)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            return dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_MATURIMI_merrMaturimSipasNdermarrjesDT").Tables[0];
        }

        #endregion

        /// <summary>
        /// Metodat e meposhtme i perkasin veprimeve qe kryhen nga objektet clsTrupiMakro dhe colTrupatMakro
        /// </summary>
        #region TRUPI MAKRO

        /// <summary>
        /// ekzekuton prc_T_TRUPIMAKRO_ins ne DB dhe kthen statusin e perfundimit te ekzekutimit te SP-se perkatese
        /// Shiko clsMesazh brenda ketij moduli per informacion te metejshem mbi vleren e kthimit te kesaj metode
        /// <param name="idTrupiMakro"> id ritese e trupit te makros</param>
        /// <param name="idKokaMakro"> id e kokes se makros</param>
        /// <param name="idLlojMakro"> id e lloji te makros</param>
        /// <param name="idProdukti"> id e produktit</param>
        /// <param name="pershkrimi"> pershkrimi</param>
        /// <param name="idFunksionMakro"> id e funksionit te makros</param>
        /// <param name="vlera"> vlera</param>
        /// <param name="renditja"> renditja</param>
        /// <returns> nje objekt clsMesazh qe tregon nese ruajtja eshte kryer ne rregull apo jo.</returns>
        /// </summary>
        internal clsMesazh ruajTrupiMakro(out int idTrupiMakro, int idKokaMakro, int idLlojMakro, int idProdukti, string pershkrimi, int idFunksionMakro, decimal vlera, int renditja)
        { //metoda per ruajtjen e TRUPIT makros
            idTrupiMakro = -1;

            dbManager.Open();
            clsMesazh mesazh = new clsMesazh();
            //shtimi i parametrave

            dbManager.CreateParameters(8);
            dbManager.AddParameters(0, "@IDTRUPIMAKRO", idTrupiMakro, ParameterDirection.Output);
            dbManager.AddParameters(1, "@IDLLOJIMAKRO", idLlojMakro, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDPRODUKTI", idProdukti, ParameterDirection.Input);
            dbManager.AddParameters(3, "@IDFUNKSIONI", idFunksionMakro, ParameterDirection.Input);
            dbManager.AddParameters(4, "@VLERA", vlera, ParameterDirection.Input);
            dbManager.AddParameters(5, "@RENDITJA", renditja, ParameterDirection.Input);
            dbManager.AddParameters(6, "@IDKOKAMAKRO", idKokaMakro, ParameterDirection.Input);
            dbManager.AddParameters(7, "@PERSHKRIMI", pershkrimi, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_TRUPIMAKRO_ins");
            mesazh = new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);

            return mesazh;



        }
        //[Obsolete("Perdor: clsMesazh ruajTrupiMakro(int idTrupiMakro, int idKokaMakro, int idLlojMakro, int idProdukti, string pershkrimi, int idFunksionMakro, decimal vlera, int renditja)", true)]
        //public clsMesazh ruajTrupiMakro(clsTrupiMakro trupiMakro)
        //{ //metoda per ruajtjen e TRUPIT makros
        //    try
        //    {
        //        clsMesazh mesazh = new clsMesazh();
        //        //shtimi i parametrave

        //        dbManager.CreateParameters(8);
        //        dbManager.AddParameters(0, "@IDTRUPIMAKRO", trupiMakro.IdTrupiMakro, ParameterDirection.Output);
        //        dbManager.AddParameters(1, "@IDLLOJIMAKRO", trupiMakro.IdLlojMakro, ParameterDirection.Input);
        //        dbManager.AddParameters(2, "@IDPRODUKTI", trupiMakro.IdProdukti, ParameterDirection.Input);
        //        dbManager.AddParameters(3, "@IDFUNKSIONI", trupiMakro.IdFunksionMakro, ParameterDirection.Input);
        //        dbManager.AddParameters(4, "@VLERA", trupiMakro.Vlera, ParameterDirection.Input);
        //        dbManager.AddParameters(5, "@RENDITJA", trupiMakro.Renditja, ParameterDirection.Input);
        //        dbManager.AddParameters(6, "@IDKOKAMAKRO", trupiMakro.IdKokaMakro, ParameterDirection.Input);
        //        dbManager.AddParameters(7, "@PERSHKRIMI", trupiMakro.Pershkrimi, ParameterDirection.Input);
        //            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_TRUPIMAKRO_ins");
        //        mesazh = new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);

        //        return mesazh;
        //    }
        //    catch (Exception)
        //    {
        //        return new clsMesazh(false, ce.Message);
        //    }


        //}

        /// <summary>
        /// ekzekuton prc_T_TRUPIMAKRO_upd ne DB dhe kthen statusin e perfundimit te ekzekutimit te SP-se perkatese
        /// Shiko clsMesazh brenda ketij moduli per informacion te metejshem mbi vleren e kthimit te kesaj metode
        /// <param name="idTrupiMakro"> id ritese e trupit te makros</param>
        /// <param name="idKokaMakro"> id e kokes se makros</param>
        /// <param name="idLlojMakro"> id e lloji te makros</param>
        /// <param name="idProdukti"> id e produktit</param>
        /// <param name="pershkrimi"> pershkrimi</param>
        /// <param name="idFunksionMakro"> id e funksionit te makros</param>
        /// <param name="vlera"> vlera</param>
        /// <param name="renditja"> renditja</param>
        /// <returns> nje objekt clsMesazh qe tregon nese modifikimi eshte kryer ne rregull apo jo.</returns>
        /// </summary>
        internal clsMesazh modifikoTrupiMakro(int idTrupiMakro, int idKokaMakro, int idLlojMakro, int idProdukti, string pershkrimi, int idFunksionMakro, decimal vlera, int renditja)
        {//metoda per modifikimin e TRUPIT MAKRO

            dbManager.Open();
            clsMesazh mesazh = new clsMesazh();
            //shtimi i parametrave

            dbManager.CreateParameters(8);
            dbManager.AddParameters(0, "@IDTRUPIMAKRO", idTrupiMakro, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDLLOJIMAKRO", idLlojMakro, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDPRODUKTI", idProdukti, ParameterDirection.Input);
            dbManager.AddParameters(3, "@IDFUNKSIONI", idFunksionMakro, ParameterDirection.Input);
            dbManager.AddParameters(4, "@VLERA", vlera, ParameterDirection.Input);
            dbManager.AddParameters(5, "@RENDITJA", renditja, ParameterDirection.Input);
            dbManager.AddParameters(6, "@IDKOKAMAKRO", idKokaMakro, ParameterDirection.Input);
            dbManager.AddParameters(7, "@PERSHKRIMI", pershkrimi, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_TRUPIMAKRO_upd");
            mesazh = new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);

            return mesazh;



        }
        //[Obsolete("Perdor: clsMesazh modifikoTrupiMakro(int idTrupiMakro, int idKokaMakro, int idLlojMakro, int idProdukti, string pershkrimi, int idFunksionMakro, decimal vlera, int renditja)", true)]
        //public clsMesazh modifikoTrupiMakro(clsTrupiMakro trupiMakro)
        //{//metoda per modifikimin e TRUPIT MAKRO
        //    try
        //    {
        //        clsMesazh mesazh = new clsMesazh();
        //        //shtimi i parametrave

        //        dbManager.CreateParameters(8);
        //        dbManager.AddParameters(0, "@IDTRUPIMAKRO", trupiMakro.IdTrupiMakro, ParameterDirection.Input );
        //        dbManager.AddParameters(1, "@IDLLOJIMAKRO", trupiMakro.IdLlojMakro, ParameterDirection.Input);
        //        dbManager.AddParameters(2, "@IDPRODUKTI", trupiMakro.IdProdukti, ParameterDirection.Input);
        //        dbManager.AddParameters(3, "@IDFUNKSIONI", trupiMakro.IdFunksionMakro, ParameterDirection.Input);
        //        dbManager.AddParameters(4, "@VLERA", trupiMakro.Vlera, ParameterDirection.Input);
        //        dbManager.AddParameters(5, "@RENDITJA", trupiMakro.Renditja, ParameterDirection.Input);
        //        dbManager.AddParameters(6, "@IDKOKAMAKRO", trupiMakro.IdKokaMakro, ParameterDirection.Input);
        //        dbManager.AddParameters(7, "@PERSHKRIMI", trupiMakro.Pershkrimi, ParameterDirection.Input);
        //        dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_TRUPIMAKRO_upd");
        //        mesazh = new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);

        //        return mesazh;
        //    }
        //    catch (Exception)
        //    {
        //        return new clsMesazh(false, ce.Message);
        //    }


        //}

        /// <summary>
        /// ekzekutohet sp-ja prc_T_TRUPIMAKRO_del duke i kaluar id e trupit te makros qe e marrim nga objekti clsTrupiMakro qe i kalohet si parameter
        /// </summary>
        /// <param name="idTrupiMakro"> id ritese e trupit te makros</param>
        /// <returns>nje objekt clsMesazh qe tregon nese fshirja eshte kryer ne rregull apo jo</returns>
        internal clsMesazh fshiTrupiMakro(int idTrupiMakro)
        {//metoda per fshirjen e TRUPIT MAKRO

            dbManager.Open();
            //shtimi i parametrave
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDTRUPIMAKRO", idTrupiMakro, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_TRUPIMAKRO_del");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
            return mesazh;

        }
        //[Obsolete("Perdor: clsMesazh fshiTrupiMakro(int idTrupiMakro)", true)]
        //public clsMesazh fshiTrupiMakro(clsTrupiMakro trupiMakro)
        //{//metoda per fshirjen e TRUPIT MAKRO
        //    try
        //    {
        //        //shtimi i parametrave
        //        dbManager.CreateParameters(1);
        //        dbManager.AddParameters(0, "@IDTRUPIMAKRO", trupiMakro.IdTrupiMakro, ParameterDirection.Input);
        //        dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_TRUPIMAKRO_del");
        //        clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
        //        return mesazh;
        //    }
        //    catch (Exception)
        //    {
        //        return new clsMesazh(false, ce.Message);
        //    }

        //}

        /// <summary>
        /// kthen datarow trupi makro sipas idse
        /// </summary>
        ///<param name="idtrupi"> id e trupit te makros</param>
        ///<returns> nje datarow qe mban trupin e makros me kete id</returns>
        internal DataRow merrTrupiMakro(int idtrupi)
        {

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDTRUPIMAKRO", idtrupi, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_TRUPIMAKRO_ktheTrupiMakroSipasID");

            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];

        }
        //[Obsolete("Perdor: DataRow ktheTrupiMakro(int idtrupi)", true)]
        //public colTrupatMakro ktheTrupiMakro(int idtrupi)
        //{//metoda per te marre TRUPI makro SIPAS ID
        //    IDBManager dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        //    dbManager.ConnectionString = dbManager.GetConnectionString();
        //    try
        //    {
        //        dbManager.Open();
        //        dbManager.CreateParameters(1);
        //        dbManager.AddParameters(0, "@IDTRUPIMAKRO", idtrupi , ParameterDirection.Input);
        //        DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_TRUPIMAKRO_ktheTrupiMakroSipasID");
        //        colTrupatMakro colMakro = new colTrupatMakro();
        //        return colMakro.mbushArrayListTrupaMakrosh(ds);
        //    }
        //    catch (Exception)
        //    {
        //        return new colTrupatMakro();
        //    }
        //    finally
        //    {
        //        dbManager.Dispose();
        //    }
        //}

        /// <summary>
        /// kthen datatable trupi makro sipas idse te kokes
        /// </summary>
        ///<param name="idkoka"> id e kokes te makros</param>
        ///<returns> nje datatable qe permban nje koleksion me te gjithe trupat makro me kete id koke</returns>
        internal DataTable ktheTrupatMakroSipasKokes(int idkoka)
        {

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDKOKAMAKRO", idkoka, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_TRUPIMAKRO_ktheTrupiMakroSipasIDKoka");
            return ds.Tables[0];

        }
        //[Obsolete("Perdor: DataTable merrTrupatMakroSipasKokes(int idkoka)", true)]
        //public colTrupatMakro merrTrupatMakroSipasKokes(int idkoka)
        //{//metoda per te marre TRUPAT Makro SIPAS kokes
        //    IDBManager dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        //    dbManager.ConnectionString = dbManager.GetConnectionString();
        //    try
        //    {
        //        dbManager.Open();
        //        dbManager.CreateParameters(1);
        //        dbManager.AddParameters(0, "@IDKOKAMAKRO", idkoka , ParameterDirection.Input);
        //        DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_TRUPIMAKRO_ktheTrupiMakroSipasIDKoka");
        //        colTrupatMakro colTrupatMakro = new colTrupatMakro();
        //        return colTrupatMakro.mbushArrayListTrupaMakrosh(ds);
        //    }
        //    catch (Exception)
        //    {
        //        return new colTrupatMakro();
        //    }
        //    finally
        //    {
        //        dbManager.Dispose();
        //    }
        //}

        internal DataTable MerrMakroFature(int idDok)
        {

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@idDok", idDok, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KOKAMAKRO_ktheKokaMakroSipasIdDok");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0)
                return null;
            return ds.Tables[0];
        }

        #endregion

        /// <summary>
        /// Metodat e meposhtme i perkasin veprimeve qe kryhen nga objektet clsKokaMakro dhe colKokatMakro
        /// </summary>
        #region KOKA MAKRO

        /// <summary>
        /// ekzekuton prc_T_KOKAMAKRO_ins ne DB dhe kthen statusin e perfundimit te ekzekutimit te SP-se perkatese
        /// Shiko clsMesazh brenda ketij moduli per informacion te metejshem mbi vleren e kthimit te kesaj metode
        /// <param name="idKokaMakro">id ritese e kokes se makros</param>
        /// <param name="kodKokaMakro"> kodi i makros</param>
        /// <param name="pershkrimKokaMakro"> pershkrimi i makros</param>
        /// <param name="idNderViti">id e ndermarje vitit</param>
        /// <param name="idPerdoruesi"> id e perdoruesit qe ka kryer veprimin</param>
        /// <param name="idNdermarje">id e ndermarjes</param>
        /// <returns> nje objekt boolean qe tregon nese ruajtja eshte kryer ne rregull apo jo.</returns>
        /// </summary>
        internal int ruajKokaMakro(int idKokaMakro, string kodKokaMakro, string pershkrimKokaMakro, int idPerdoruesi, int idNdermarje, int idstatusdok)
        {//ruajtja e kokamakro

            dbManager.Open();
            dbManager.CreateParameters(6);
            dbManager.AddParameters(0, "@IDKOKAMAKRO", idKokaMakro, ParameterDirection.Output);
            dbManager.AddParameters(1, "@KODIKOKAMAKRO", kodKokaMakro, ParameterDirection.Input);
            dbManager.AddParameters(2, "@PERSHKRIMIKOKAMAKRO", pershkrimKokaMakro, ParameterDirection.Input);
            dbManager.AddParameters(3, "@IDPERDORUESI", idPerdoruesi, ParameterDirection.Input);
            //dbManager.AddParameters(4, "@IDNDERVITI", idNderViti, ParameterDirection.Input);
            dbManager.AddParameters(4, "@IDNDERMARJE", idNdermarje, ParameterDirection.Input);
            dbManager.AddParameters(5, "@IDSTATUSDOK", idstatusdok, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_KOKAMAKRO_ins");
            idKokaMakro = int.Parse(dbManager.Parameters[0].Value.ToString());
            //clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
            return idKokaMakro;


        }
        //[Obsolete("Perdor: clsMesazh ruajKokaMakro(int idKokaMakro, string kodKokaMakro, string pershkrimKokaMakro, int idNderViti, int idPerdoruesi, int idNdermarje)", true)]
        //public clsMesazh ruajKokaMakro(clsKokaMakro makro)
        //{//ruajtja e kokamakro
        //    try
        //    {
        //        dbManager.CreateParameters(6);
        //        dbManager.AddParameters(0, "@IDKOKAMAKRO", makro.IdKokaMakro, ParameterDirection.Output);
        //        dbManager.AddParameters(1, "@KODIKOKAMAKRO", makro.KodiKokaMakro, ParameterDirection.Input);
        //        dbManager.AddParameters(2, "@PERSHKRIMIKOKAMAKRO", makro.PershkrimiKokaMakro, ParameterDirection.Input);
        //        dbManager.AddParameters(3, "@IDPERDORUESI", makro.IdPerdoruesi, ParameterDirection.Input);
        //        dbManager.AddParameters(4, "@IDNDERVITI", makro.IdNderViti, ParameterDirection.Input);
        //        dbManager.AddParameters(5, "@IDNDERMARJE", makro.IdNdermarje, ParameterDirection.Input);
        //        dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_KOKAMAKRO_ins");
        //        makro.IdKokaMakro = int.Parse(dbManager.Parameters[0].Value.ToString());
        //        clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
        //        return mesazh;
        //    }
        //    catch (Exception)
        //    {
        //        return new clsMesazh(false, ce.Message);
        //    }

        //}

        /// <summary>
        /// ekzekuton prc_T_KOKAMAKRO_upd ne DB dhe kthen statusin e perfundimit te ekzekutimit te SP-se perkatese
        /// Shiko clsMesazh brenda ketij moduli per informacion te metejshem mbi vleren e kthimit te kesaj metode
        /// <param name="idKokaMakro">id ritese e kokes se makros</param>
        /// <param name="kodKokaMakro"> kodi i makros</param>
        /// <param name="pershkrimKokaMakro"> pershkrimi i makros</param>
        /// <param name="idNderViti">id e ndermarje vitit</param>
        /// <param name="idPerdoruesi"> id e perdoruesit qe ka kryer veprimin</param>
        /// <param name="idNdermarje">id e ndermarjes</param>
        /// <returns> nje objekt clsMesazh qe tregon nese modifikimi eshte kryer ne rregull apo jo.</returns>
        /// </summary>
        internal clsMesazh modifikoKokaMakro(int idKokaMakro, string kodKokaMakro, string pershkrimKokaMakro, int idPerdoruesi, int idNdermarje, int idstatusdok)
        {//modifikimi i kokamakro

            dbManager.Open();
            dbManager.CreateParameters(6);
            dbManager.AddParameters(0, "@IDKOKAMAKRO", idKokaMakro, ParameterDirection.Input);
            dbManager.AddParameters(1, "@KODIKOKAMAKRO", kodKokaMakro, ParameterDirection.Input);
            dbManager.AddParameters(2, "@PERSHKRIMIKOKAMAKRO", pershkrimKokaMakro, ParameterDirection.Input);
            dbManager.AddParameters(3, "@IDPERDORUESI", idPerdoruesi, ParameterDirection.Input);
            //dbManager.AddParameters(4, "@IDNDERVITI", idNderViti, ParameterDirection.Input);
            dbManager.AddParameters(4, "@IDNDERMARJE", idNdermarje, ParameterDirection.Input);
            dbManager.AddParameters(5, "@IDSTATUSDOK", idstatusdok, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_KOKAMAKRO_upd");
            idKokaMakro = int.Parse(dbManager.Parameters[0].Value.ToString());
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
            return mesazh;

        }
        //[Obsolete("Perdor: clsMesazh modifikoKokaMakro(int idKokaMakro, string kodKokaMakro, string pershkrimKokaMakro, int idNderViti, int idPerdoruesi, int idNdermarje)", true)]
        //public clsMesazh modifikoKokaMakro(clsKokaMakro makro)
        //{//modifikimi i kokamakro
        //    try
        //    {
        //        dbManager.CreateParameters(6);
        //        dbManager.AddParameters(0, "@IDKOKAMAKRO", makro.IdKokaMakro, ParameterDirection.Input);
        //        dbManager.AddParameters(1, "@KODIKOKAMAKRO", makro.KodiKokaMakro, ParameterDirection.Input);
        //        dbManager.AddParameters(2, "@PERSHKRIMIKOKAMAKRO", makro.PershkrimiKokaMakro, ParameterDirection.Input);
        //        dbManager.AddParameters(3, "@IDPERDORUESI", makro.IdPerdoruesi, ParameterDirection.Input);
        //        dbManager.AddParameters(4, "@IDNDERVITI", makro.IdNderViti, ParameterDirection.Input);
        //        dbManager.AddParameters(5, "@IDNDERMARJE", makro.IdNdermarje, ParameterDirection.Input);
        //        dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_KOKAMAKRO_upd");
        //        makro.IdKokaMakro = int.Parse(dbManager.Parameters[0].Value.ToString());
        //        clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
        //        return mesazh;
        //    }
        //    catch (Exception)
        //    {
        //        return new clsMesazh(false, ce.Message);
        //    }
        //}

        /// <summary>
        /// ekzekutohet sp-ja prc_T_KOKAMAKRO_del duke i kaluar id e kokes te makros qe e marrim nga objekti clsKokaMakro qe i kalohet si parameter
        /// </summary>
        /// <param name="idKokaMakro">id ritese e kokes se makros</param>
        /// <returns>nje objekt boolean qe tregon nese fshirja eshte kryer ne rregull apo jo</returns>
        internal clsMesazh fshiKokaMakro(int idKokaMakro)
        {//fshirja e KokaMakro

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDKOKAMAKRO", idKokaMakro, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_KOKAMAKRO_del");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
            return mesazh;

        }
        internal clsMesazh fshiKokaMakroStatus(int idKokaMakro, int idperdorues)
        {//fshirja e KokaMakro

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDKOKAMAKRO", idKokaMakro, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUESI", idperdorues, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_KOKAMAKRO_upddel");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
            return mesazh;

        }
        //[Obsolete("Perdor: clsMesazh fshiKokaMakro(int idKokaMakro)", true)]
        //public clsMesazh fshiKokaMakro(clsKokaMakro makro)
        //{//fshirja e KokaMakro
        //    try
        //    {
        //        dbManager.CreateParameters(1);
        //        dbManager.AddParameters(0, "@IDKOKAMAKRO", makro.IdKokaMakro, ParameterDirection.Input);
        //        dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_KOKAMAKRO_del");
        //        clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
        //        return mesazh;
        //    }
        //    catch (Exception)
        //    {
        //        return new clsMesazh(false, ce.Message);
        //    }
        //}

        ///// <summary>
        ///// fshin nje objekt makro sebashku me trupin dhe autorizimet
        ///// Nje objekt makro ka nje koleksion me trupin  dhe autorizimet, 
        ///// fshirja e nje makro imponon fshirjen edhe te nje colection-i me trupin dhe autorizimet
        ///// Mqs cdo rresht i ri qe fshihet ne DB kerkon thirrjen e nje SP-je me parametra dhe makro bashke me trupin dhe autorizimet konsiderohet si nje regjistrim,
        ///// perdoret nje transaksion qe imponon fshirjen e rregullt te nje makro sebashku me trupin dhe autorizimet
        ///// </summary>
        ///// <param name="idKokaMakro">id ritese e kokes se makros</param>
        ///// <returns> kthen nje obj clsMesazh per te identifikuar statusin e fshirjes se te dhenave ne DB</returns>
        //[Obsolete("Perdor nga klasa perkatese: public clsMesazh fshiMakro(int idKokaMakro)", true)]
        //internal clsMesazh fshiMakro(int idKokaMakro)
        //{//fshin makro
        //    //DbCore.DbKontabiliteti.clsDatabaseKontabilitet db = new clsDatabaseKontabilitet();
        //    //colLlojeBuxhetesh colLloj = db.merrLlojBuxhetiSipasKodit("Makro");
        //    DbAdmin.colLidhjetAutorizim colLidhjeAutorizim = new DbAdmin.colLidhjetAutorizim(idKokaMakro, DbCore.DbKontabiliteti.clsLlojBuxheti.mbushIDLlojBuxheti("Makro"));
        //    //DbAdmin.colLidhjetAutorizim colLidhjeAutorizim = new DbAdmin.clsDatabaseAdmin().merrLidhjeAutorizimSipasIdLidheseIdLloji(idKokaMakro, DbCore.DbKontabiliteti.clsLlojBuxheti.mbushIDLlojBuxheti("Makro"));
        //    colTrupatMakro trupat = new colTrupatMakro(idKokaMakro);
        //    //colTrupatMakro  trupat =merrTrupatMakroSipasKokes (makro.IdKokaMakro);
        //    if (dbManager == null)
        //    {
        //        dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        //        dbManager.ConnectionString = dbManager.GetConnectionString();
        //    }
        //    dbManager.Open();
        //    dbManager.BeginTransaction();
        //    clsMesazh mesazh = new clsMesazh(true);
        //    DbCore.clsMesazh mesazhAdmin = new DbCore.clsMesazh(true);
        //    try
        //    {
        //        foreach (clsTrupiMakro o in trupat)
        //        {
        //            if (mesazh.Status)
        //            {
        //                mesazh = fshiTrupiMakro(o.IdTrupiMakro);
        //                //mesazh = fshiTrupiMakro(o);
        //            }
        //            else
        //            {
        //                dbManager.Transaction.Rollback();
        //                return mesazh;
        //            }
        //        }
        //        if (mesazh.Status)
        //        {
        //            foreach (DbAdmin.clsLidhjeAutorizim o in colLidhjeAutorizim)
        //            {
        //                if (mesazhAdmin.Status)
        //                    mesazhAdmin = new DbAdmin.clsDatabaseAdmin().fshiLidhjeAutorizim(o.IdLidhjeAutorizim);

        //                else
        //                {
        //                    dbManager.Transaction.Rollback();
        //                    mesazh.Status = false;
        //                    mesazh.PershkrimMesazhi = mesazhAdmin.PershkrimMesazhi;
        //                    return mesazh;
        //                }
        //            }
        //            if (mesazhAdmin.Status)
        //            {
        //                mesazh = fshiKokaMakro(idKokaMakro);
        //                if (mesazh.Status)
        //                {
        //                    dbManager.CommitTransaction();
        //                    mesazh = new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
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
        //}
        //[Obsolete("Perdor: clsMesazh fshiMakro(int idKokaMakro)", true)]
        //public clsMesazh fshiMakro(clsKokaMakro makro)
        //{//fshin makro
        //    //DbCore.DbKontabiliteti.clsDatabaseKontabilitet db = new clsDatabaseKontabilitet();
        //    //colLlojeBuxhetesh colLloj = db.merrLlojBuxhetiSipasKodit("Makro");
        //    DbAdmin.colLidhjetAutorizim colLidhjeAutorizim = new DbAdmin.colLidhjetAutorizim(makro.IdKokaMakro, DbCore.DbKontabiliteti.clsLlojBuxheti.mbushIDLlojBuxheti("Makro"));
        //    //DbAdmin.colLidhjetAutorizim colLidhjeAutorizim = new DbAdmin.clsDatabaseAdmin().merrLidhjeAutorizimSipasIdLidheseIdLloji(makro.IdKokaMakro, DbCore.DbKontabiliteti.clsLlojBuxheti.mbushIDLlojBuxheti("Makro"));
        //    colTrupatMakro trupat = new colTrupatMakro(makro.IdKokaMakro);
        //    //colTrupatMakro  trupat =merrTrupatMakroSipasKokes (makro.IdKokaMakro);
        //    dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        //    dbManager.ConnectionString = dbManager.GetConnectionString();
        //    dbManager.Open();
        //    dbManager.BeginTransaction();
        //    clsMesazh mesazh = new clsMesazh(true);
        //    DbCore.clsMesazh mesazhAdmin = new DbCore.clsMesazh(true);
        //    try
        //    {
        //        foreach (clsTrupiMakro o in trupat)
        //        {
        //            if (mesazh.Status)
        //            {
        //                mesazh = fshiTrupiMakro(o.IdTrupiMakro);
        //                //mesazh = fshiTrupiMakro(o);
        //            }
        //            else
        //            {
        //                dbManager.Transaction.Rollback();
        //                return mesazh;
        //            }
        //        }
        //        if (mesazh.Status)
        //        {
        //            foreach (DbAdmin.clsLidhjeAutorizim o in colLidhjeAutorizim)
        //            {
        //                if (mesazhAdmin.Status)
        //                    mesazhAdmin = new DbAdmin.clsDatabaseAdmin().fshiLidhjeAutorizim(o.IdLidhjeAutorizim);

        //                else
        //                {
        //                    dbManager.Transaction.Rollback();
        //                    mesazh.Status = false;
        //                    mesazh.PershkrimMesazhi = mesazhAdmin.PershkrimMesazhi;
        //                    return mesazh;
        //                }
        //            }
        //            if (mesazhAdmin.Status)
        //            {
        //                mesazh = fshiKokaMakro(makro.IdKokaMakro);
        //                //mesazh=fshiKokaMakro(makro);
        //                if (mesazh.Status)
        //                {
        //                    dbManager.CommitTransaction();
        //                    mesazh = new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
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
        /// kthen datatable makro sipas ndermarjes dhe autorizimit
        /// </summary>
        /// <param name="idndermarje"> id e ndermarjes</param>
        /// <param name="idperdorues"> id e perdoruesi</param>
        /// <returns> nje datatable qe permban nje koleksion me kokat e makros te kesaj ndermarje dhe per te cilat ka autorizim ky perdorues</returns>
        internal DataTable ktheMakroSipasNdermarrjesAndAutorizime(int idndermarje, int idperdorues)
        {

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDNDERMARJE", idndermarje, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUES", idperdorues, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KOKAMAKRO_merrKokaMakroSipasNdermarrjesAndAutorizime");
            return ds.Tables[0];

        }
        //[Obsolete("Perdor: DataTable ktheMakroSipasNdermarrjesAndAutorizime(int idndermarje, int idperdorues)", true)]
        //public colKokatMakro  merrMakroSipasNdermarrjesAndAutorizime(int idndermarje, int idperdorues)
        //{//metoda per te marre te gjithe makrot
        //    IDBManager dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        //    dbManager.ConnectionString = dbManager.GetConnectionString();
        //    try
        //    {
        //        dbManager.Open();
        //        dbManager.CreateParameters(2);
        //        dbManager.AddParameters(0, "@IDNDERMARJE", idndermarje, ParameterDirection.Input);
        //        dbManager.AddParameters(1, "@IDPERDORUES", idperdorues, ParameterDirection.Input);
        //        DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KOKAMAKRO_merrKokaMakroSipasNdermarrjesAndAutorizime");
        //        colKokatMakro  colMakro = new colKokatMakro ();
        //        return colMakro.mbushArrayListKokaMakrosh(ds);
        //    }
        //    catch (Exception)
        //    {
        //        return new colKokatMakro ();
        //    }
        //    finally
        //    {
        //        dbManager.Dispose();
        //    }

        //}

        /// <summary>
        /// kthen datatable makro sipas ndermarjes dhe autorizimit qe fillojne me kete kod
        /// </summary>
        /// <param name="idndermarje"> id e ndermarjes</param>
        /// <param name="idperdorues"> id e perdoruesit</param>
        /// <param name="kodi"> kodi me te cilin fillon</param>
        /// <returns> nje datatable qe permban nje koleksion me kokat e makros te kesaj ndermarje dhe per te cilat ka autorizim ky perdorues dhe qe fillojne me kete kod</returns>
        internal DataTable ktheMakroSipasNdermarrjesAndAutorizimeLike(int idndermarje, int idperdorues, string kodi)
        {

            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@IDNDERMARJE", idndermarje, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUES", idperdorues, ParameterDirection.Input);
            dbManager.AddParameters(2, "@KODIKOKAMAKRO", kodi, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KOKAMAKRO_merrKokaMakroSipasNdermarrjesAndAutorizimeLike");

            return ds.Tables[0];

        }
        //[Obsolete("Perdor: DataTable ktheMakroSipasNdermarrjesAndAutorizimeLike(int idndermarje, int idperdorues, string kodi)", true)]
        //public colKokatMakro merrMakroSipasNdermarrjesAndAutorizimeLike(int idndermarje, int idperdorues, string kodi)
        //{//metoda per te marre te gjithe makrot
        //    IDBManager dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        //    dbManager.ConnectionString = dbManager.GetConnectionString();
        //    try
        //    {
        //        dbManager.Open();
        //        dbManager.CreateParameters(3);
        //        dbManager.AddParameters(0, "@IDNDERMARJE", idndermarje, ParameterDirection.Input);
        //        dbManager.AddParameters(1, "@IDPERDORUES", idperdorues, ParameterDirection.Input);
        //        dbManager.AddParameters(2, "@KODIKOKAMAKRO", kodi, ParameterDirection.Input);
        //        DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KOKAMAKRO_merrKokaMakroSipasNdermarrjesAndAutorizimeLike");
        //        colKokatMakro colMakro = new colKokatMakro();
        //        return colMakro.mbushArrayListKokaMakrosh(ds);
        //    }
        //    catch (Exception)
        //    {
        //        return new colKokatMakro();
        //    }
        //    finally
        //    {
        //        dbManager.Dispose();
        //    }

        //}

        /// <summary>
        /// kthen datarow koka makro me kete id
        /// </summary>
        /// <param name="id"> id e kokes se makros</param>
        /// <returns> nje datarow qe permban koken e makros me ate id</returns>
        internal DataRow merrKokaMakro(int id)
        {

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDKOKAMAKRO", id, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KOKAMAKRO_ktheKokaMakroSipasID");

            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];


        }
        //[Obsolete("Perdor: DataRow ktheKokaMakro(int id)", true)]
        //public colKokatMakro ktheKokaMakro(int id)
        //{//kthen KokaMakro sipas id
        //    IDBManager dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        //    dbManager.ConnectionString = dbManager.GetConnectionString();
        //    try
        //    {
        //        dbManager.Open();
        //        dbManager.CreateParameters(1);
        //        dbManager.AddParameters(0, "@IDKOKAMAKRO", id, ParameterDirection.Input);
        //        DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KOKAMAKRO_ktheKokaMakroSipasID");
        //        colKokatMakro colMakro = new colKokatMakro();
        //        return colMakro.mbushArrayListKokaMakrosh(ds);

        //    }
        //    catch (Exception)
        //    {
        //        return new colKokatMakro();
        //    }
        //    finally
        //    {
        //        dbManager.Dispose();
        //    }

        //}

        /// <summary>
        /// kthen int id koka makro me kete kod ne kete ndermarje
        /// </summary>
        /// <param name="kod"> kodi i makros</param>
        /// <param name="idndermarje"> id e ndermarjes</param>
        /// <returns>nje int qe permban id koken e makros me kete kod ne kete ndermarje</returns>
        internal int merrKokaMakro(string kod, int idndermarje)
        {

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@KODIKOKAMAKRO", kod, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idndermarje, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KOKAMAKRO_ktheKokaMakroSipasKodit");

            if (ds == null)
                return -1;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return -1;
            int idKokaMakro;
            int.TryParse(ds.Tables[0].Rows[0]["IDKOKAMAKRO"].ToString(), out idKokaMakro);
            return idKokaMakro;


        }
        internal DataRow merrKokaMakroSipasKodit(string kod, int idndermarje)
        {

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@KODIKOKAMAKRO", kod, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idndermarje, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KOKAMAKRO_ktheKokaMakroSipasKodit");

            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];


        }
        //[Obsolete("Perdor: DataRow merrKokaMakro(string kod, int idndermarje)", true)]
        //public colKokatMakro ktheKokaMakro(string kod, int idndermarje)
        //{//kthen KokaMakro sipas kodit
        //    IDBManager dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        //    dbManager.ConnectionString = dbManager.GetConnectionString();
        //    try
        //    {
        //        dbManager.Open();
        //        dbManager.CreateParameters(2);
        //        dbManager.AddParameters(0, "@KODIKOKAMAKRO", kod, ParameterDirection.Input);
        //        dbManager.AddParameters(1, "@IDNDERMARJE", idndermarje, ParameterDirection.Input);
        //        DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KOKAMAKRO_ktheKokaMakroSipasKodit");
        //        colKokatMakro colMakro = new colKokatMakro();
        //        return colMakro.mbushArrayListKokaMakrosh(ds);

        //    }
        //    catch (Exception)
        //    {
        //        return new colKokatMakro();
        //    }
        //    finally
        //    {
        //        dbManager.Dispose();
        //    }

        //}

        ///// <summary>
        ///// Ruan nje objekt makro sebashku me trupin dhe autorizimet
        ///// Nje objekt makro ka nje koleksion me trupat dhe autorizimet, 
        ///// ruajtja e nje makro imponon ruajtjen edhe te nje colection-i me trupat dhe autorizimet
        ///// Mqs cdo rresht i ri qe shtohet ne DB kerkon thirrjen e nje SP-je me parametra dhe makro bashke me trupat dhe autorizimet konsiderohet si nje regjistrim,
        ///// perdoret nje transaksion qe imponon regjistrimin e rregullt te nje makro sebashku me trupat dhe autorizimet
        ///// </summary>
        ///// <param name="idKokaMakro">id ritese e kokes se makros</param>
        ///// <param name="kodKokaMakro"> kodi i makros</param>
        ///// <param name="pershkrimKokaMakro"> pershkrimi i makros</param>
        ///// <param name="idNderViti">id e ndermarje vitit</param>
        ///// <param name="idPerdoruesi"> id e perdoruesit qe ka kryer veprimin</param>
        ///// <param name="idNdermarje">id e ndermarjes</param>
        ///// <returns> kthen nje obj clsMesazh per te identifikuar statusin e ruajtjes se te dhenave ne DB</returns>     
        //[Obsolete("Perdor nga klasa perkatese: clsMesazh ruajMakro(int idKokaMakro, string kodKokaMakro, string pershkrimKokaMakro, int idNderViti, int idPerdoruesi, int idNdermarje)", true)]
        //internal clsMesazh ruajMakro(int idKokaMakro, string kodKokaMakro, string pershkrimKokaMakro, int idNderViti, int idPerdoruesi, int idNdermarje)
        //{//ruan makro
        //    //DbCore.DbKontabiliteti.clsDatabaseKontabilitet db = new clsDatabaseKontabilitet();

        //    //colLlojeBuxhetesh colLloj = db.merrLlojBuxhetiSipasKodit("Makro");
        //    clsKokaMakro makro = new clsKokaMakro(idKokaMakro, kodKokaMakro, pershkrimKokaMakro, idNderViti, idPerdoruesi, idNdermarje);
        //    bool statusVeprimi;
        //    if (dbManager == null)
        //    {
        //        dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        //        dbManager.ConnectionString = dbManager.GetConnectionString();
        //    }
        //    dbManager.Open();
        //    dbManager.BeginTransaction();
        //    clsMesazh mesazh = new clsMesazh();
        //    DbCore.clsMesazh mesazhAdmin = new DbCore.clsMesazh(true);
        //    try
        //    {
        //        idKokaMakro = ruajKokaMakro(idKokaMakro, kodKokaMakro, pershkrimKokaMakro, idNderViti, idPerdoruesi, idNdermarje);
        //        if (idKokaMakro == 0)
        //            statusVeprimi = false;
        //        else statusVeprimi = true;

        //        if (statusVeprimi)
        //        {
        //            if (makro.IdNivelAutorizimi != "")
        //            {
        //                DbAdmin.colLidhjetAutorizim colLidhjet = new DbAdmin.colLidhjetAutorizim();
        //                string[] pars1 = makro.IdNivelAutorizimi.Split(',');
        //                for (int i = 0; i < pars1.Length; i++)
        //                {
        //                    DbAdmin.clsLidhjeAutorizim lidhje = new DbAdmin.clsLidhjeAutorizim();
        //                    lidhje.IdAutorizimeKoka = DbAdmin.clsAutorizimKoka.ktheIDAutorizim(pars1[i]);
        //                    //lidhje.IdAutorizimeKoka = new DbAdmin.clsDatabaseAdmin().ktheAutorizim(pars1[i])[0].IdAutorizimKoka;
        //                    colLidhjet.Add(lidhje);
        //                }
        //                foreach (DbAdmin.clsLidhjeAutorizim o in colLidhjet)
        //                {
        //                    if (mesazhAdmin.Status)
        //                    {
        //                        o.IdLloji = DbCore.DbKontabiliteti.clsLlojBuxheti.mbushIDLlojBuxheti("Makro");
        //                        o.IdLidhese = idKokaMakro;
        //                        mesazhAdmin = new DbAdmin.clsDatabaseAdmin().ruajLidhjeAutorizim(o.IdLidhjeAutorizim, o.IdLidhese, o.IdLloji, o.IdAutorizimeKoka);
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
        //                foreach (clsTrupiMakro o in makro.OColTrupatMakro)
        //                {
        //                    if (statusVeprimi)
        //                    {
        //                        o.IdKokaMakro = idKokaMakro;
        //                        int idT;
        //                        mesazh = ruajTrupiMakro(out idT, o.IdKokaMakro, o.IdLlojMakro, o.IdProdukti, o.Pershkrimi, o.IdFunksionMakro, o.Vlera, o.Renditja);
        //                        //mesazh= ruajTrupiMakro(o);
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
        //                    mesazh = new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
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
        //}
        //[Obsolete("Perdor: clsMesazh ruajMakro(int idKokaMakro, string kodKokaMakro, string pershkrimKokaMakro, int idNderViti, int idPerdoruesi, int idNdermarje)", true)]
        //public clsMesazh ruajMakro(clsKokaMakro makro)
        //{//ruan makro
        //    //DbCore.DbKontabiliteti.clsDatabaseKontabilitet db = new clsDatabaseKontabilitet();

        //    //colLlojeBuxhetesh colLloj = db.merrLlojBuxhetiSipasKodit("Makro");

        //    bool statusVeprimi;
        //    dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        //    dbManager.ConnectionString = dbManager.GetConnectionString();
        //    dbManager.Open();
        //    dbManager.BeginTransaction();
        //    clsMesazh mesazh = new clsMesazh();
        //    DbCore.clsMesazh mesazhAdmin = new DbCore.clsMesazh(true);
        //    try
        //    {
        //        makro.IdKokaMakro = ruajKokaMakro(makro.IdKokaMakro, makro.KodiKokaMakro, makro.PershkrimiKokaMakro, makro.IdNderViti, makro.IdPerdoruesi, makro.IdNdermarje);
        //        //mesazh= ruajKokaMakro(makro);
        //        if (makro.IdKokaMakro == 0)
        //            statusVeprimi = false;
        //        else statusVeprimi = true;

        //        if (statusVeprimi)
        //        {
        //            if (makro.IdNivelAutorizimi != "")
        //            {
        //                DbAdmin.colLidhjetAutorizim colLidhjet = new DbAdmin.colLidhjetAutorizim();
        //                string[] pars1 = makro.IdNivelAutorizimi.Split(',');
        //                for (int i = 0; i < pars1.Length; i++)
        //                {
        //                    DbAdmin.clsLidhjeAutorizim lidhje = new DbAdmin.clsLidhjeAutorizim();
        //                    lidhje.IdAutorizimeKoka = DbAdmin.clsAutorizimKoka.ktheIDAutorizim(pars1[i]);
        //                    //lidhje.IdAutorizimeKoka = new DbAdmin.clsDatabaseAdmin().ktheAutorizim(pars1[i])[0].IdAutorizimKoka;
        //                    colLidhjet.Add(lidhje);
        //                }
        //                foreach (DbAdmin.clsLidhjeAutorizim o in colLidhjet)
        //                {
        //                    if (mesazhAdmin.Status)
        //                    {
        //                        o.IdLloji = DbCore.DbKontabiliteti.clsLlojBuxheti.mbushIDLlojBuxheti("Makro");
        //                        o.IdLidhese = makro.IdKokaMakro;
        //                        mesazhAdmin = new DbAdmin.clsDatabaseAdmin().ruajLidhjeAutorizim(o.IdLidhjeAutorizim, o.IdLidhese, o.IdLloji, o.IdAutorizimeKoka);
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
        //                foreach (clsTrupiMakro o in makro.OColTrupatMakro)
        //                {
        //                    if (statusVeprimi)
        //                    {
        //                        o.IdKokaMakro = makro.IdKokaMakro;
        //                        int idT;
        //                        mesazh = ruajTrupiMakro(out idT, o.IdKokaMakro, o.IdLlojMakro, o.IdProdukti, o.Pershkrimi, o.IdFunksionMakro, o.Vlera, o.Renditja);
        //                        //mesazh= ruajTrupiMakro(o);
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
        //                    mesazh = new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
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
        /// Modifikon nje objekt makro sebashku me trupin dhe autorizimet
        /// Nje objekt makro ka nje koleksion me trupat dhe autorizimet, 
        /// modifikimi e nje makro imponon modifikimin edhe te nje colection-i me trupat dhe autorizimet
        /// Mqs cdo rresht i ri qe modifikohet ne DB kerkon thirrjen e nje SP-je me parametra dhe makro bashke me trupat dhe autorizimet konsiderohet si nje regjistrim,
        /// perdoret nje transaksion qe imponon modifikimin e rregullt te nje makro sebashku me trupat dhe autorizimet
        /// </summary>
        /// <param name="idKokaMakro">id ritese e kokes se makros</param>
        /// <param name="kodKokaMakro"> kodi i makros</param>
        /// <param name="pershkrimKokaMakro"> pershkrimi i makros</param>
        /// <param name="idNderViti">id e ndermarje vitit</param>
        /// <param name="idPerdoruesi"> id e perdoruesit qe ka kryer veprimin</param>
        /// <param name="idNdermarje">id e ndermarjes</param>
        /// <returns> kthen nje obj clsMesazh per te identifikuar statusin e modifikimit te te dhenave ne DB</returns>     
        //[Obsolete("Perdor nga klasa perkatese: clsMesazh modifikoMakro(int idKokaMakro, string kodKokaMakro, string pershkrimKokaMakro, int idNderViti, int idPerdoruesi, int idNdermarje)", true)]
        //internal clsMesazh modifikoMakro(int idKokaMakro, string kodKokaMakro, string pershkrimKokaMakro, int idNderViti, int idPerdoruesi, int idNdermarje)
        //{
        //    //modifikon Makro
        //    //DbCore.DbKontabiliteti.clsDatabaseKontabilitet db = new clsDatabaseKontabilitet();

        //    //colLlojeBuxhetesh colLloj = db.merrLlojBuxhetiSipasKodit("Makro");
        //    DbAdmin.colLidhjetAutorizim colLidhjetAutorizim = new DbAdmin.colLidhjetAutorizim(idKokaMakro, DbCore.DbKontabiliteti.clsLlojBuxheti.mbushIDLlojBuxheti("Makro"));
        //    //DbAdmin.colLidhjetAutorizim colLidhjetAutorizim = new DbAdmin.clsDatabaseAdmin().merrLidhjeAutorizimSipasIdLidheseIdLloji(idKokaMakro, DbCore.DbKontabiliteti.clsLlojBuxheti.mbushIDLlojBuxheti("Makro"));
        //    colTrupatMakro trupat = new colTrupatMakro(idKokaMakro);
        //    clsKokaMakro makro = new clsKokaMakro(idKokaMakro, kodKokaMakro, pershkrimKokaMakro, idNderViti, idPerdoruesi, idNdermarje);
        //    //colTrupatMakro trupat = merrTrupatMakroSipasKokes(makro.IdKokaMakro);
        //    if (dbManager == null)
        //    {
        //        dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        //        dbManager.ConnectionString = dbManager.GetConnectionString();
        //    }
        //    dbManager.Open();
        //    dbManager.BeginTransaction();
        //    clsMesazh mesazh = new clsMesazh();
        //    DbCore.clsMesazh mesazhAdmin = new DbCore.clsMesazh(true);
        //    try
        //    {
        //        mesazh = modifikoKokaMakro(idKokaMakro, kodKokaMakro, pershkrimKokaMakro, idNderViti, idPerdoruesi, idNdermarje);
        //        if (mesazh.Status)
        //        {
        //            DbAdmin.colLidhjetAutorizim colLidhjet = new DbAdmin.colLidhjetAutorizim();
        //            if (makro.IdNivelAutorizimi != "")
        //            {

        //                string[] pars1 = makro.IdNivelAutorizimi.Split(',');
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
        //                        colLidhjet[i].IdLloji = DbCore.DbKontabiliteti.clsLlojBuxheti.mbushIDLlojBuxheti("Makro");
        //                        colLidhjet[i].IdLidhese = idKokaMakro;
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
        //                            colLidhjet[i].IdLloji = DbCore.DbKontabiliteti.clsLlojBuxheti.mbushIDLlojBuxheti("Makro");
        //                            colLidhjet[i].IdLidhese = idKokaMakro;

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
        //                if (trupat.Count < makro.OColTrupatMakro.Count)//rasti kur jane shtuar rreshta trupi
        //                {
        //                    for (int i = 0; i < makro.OColTrupatMakro.Count; i++)
        //                    {
        //                        if (mesazh.Status)
        //                        {
        //                            makro.OColTrupatMakro[i].IdKokaMakro = idKokaMakro;
        //                            if (i < trupat.Count)
        //                            {

        //                                makro.OColTrupatMakro[i].IdTrupiMakro = trupat[i].IdTrupiMakro;
        //                                mesazh = modifikoTrupiMakro(makro.OColTrupatMakro[i].IdTrupiMakro, makro.OColTrupatMakro[i].IdKokaMakro, makro.OColTrupatMakro[i].IdLlojMakro, makro.OColTrupatMakro[i].IdProdukti, makro.OColTrupatMakro[i].Pershkrimi, makro.OColTrupatMakro[i].IdFunksionMakro, makro.OColTrupatMakro[i].Vlera, makro.OColTrupatMakro[i].Renditja);
        //                                //mesazh=  modifikoTrupiMakro(makro.OColTrupatMakro[i]);
        //                            }
        //                            else
        //                            {
        //                                int idoC;
        //                                mesazh = ruajTrupiMakro(out idoC, makro.OColTrupatMakro[i].IdKokaMakro, makro.OColTrupatMakro[i].IdLlojMakro, makro.OColTrupatMakro[i].IdProdukti, makro.OColTrupatMakro[i].Pershkrimi, makro.OColTrupatMakro[i].IdFunksionMakro, makro.OColTrupatMakro[i].Vlera, makro.OColTrupatMakro[i].Renditja);
        //                                //mesazh=  ruajTrupiMakro(makro.OColTrupatMakro[i]);
        //                            }
        //                        }
        //                        else
        //                        {
        //                            dbManager.Transaction.Rollback();
        //                            return mesazh;
        //                        }
        //                    }
        //                }
        //                else//rasti kur jane fshire rreshta
        //                {
        //                    int count = 0;
        //                    for (int i = 0; i < trupat.Count; i++)
        //                    {
        //                        if (mesazh.Status)
        //                        {
        //                            if (count < makro.OColTrupatMakro.Count)
        //                            {

        //                                makro.OColTrupatMakro[i].IdKokaMakro = idKokaMakro;

        //                                makro.OColTrupatMakro[i].IdTrupiMakro = trupat[i].IdTrupiMakro;
        //                                mesazh = modifikoTrupiMakro(makro.OColTrupatMakro[i].IdTrupiMakro, makro.OColTrupatMakro[i].IdKokaMakro, makro.OColTrupatMakro[i].IdLlojMakro, makro.OColTrupatMakro[i].IdProdukti, makro.OColTrupatMakro[i].Pershkrimi, makro.OColTrupatMakro[i].IdFunksionMakro, makro.OColTrupatMakro[i].Vlera, makro.OColTrupatMakro[i].Renditja);
        //                                //mesazh=  modifikoTrupiMakro(makro.OColTrupatMakro[i]);
        //                            }
        //                            else
        //                            {
        //                                mesazh = fshiTrupiMakro(trupat[i].IdTrupiMakro);
        //                                //mesazh=  fshiTrupiMakro(trupat[i]);
        //                            }
        //                            count++;
        //                        }
        //                        else
        //                        {
        //                            dbManager.Transaction.Rollback();
        //                            return mesazh;
        //                        }
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
        //}
        ////[Obsolete("Perdor: clsMesazh modifikoMakro(int idKokaMakro, string kodKokaMakro, string pershkrimKokaMakro, int idNderViti, int idPerdoruesi, int idNdermarje)", true)]
        //public clsMesazh modifikoMakro(clsKokaMakro makro)
        //{
        //    //modifikon Makro
        //    //DbCore.DbKontabiliteti.clsDatabaseKontabilitet db = new clsDatabaseKontabilitet();

        //    //colLlojeBuxhetesh colLloj = db.merrLlojBuxhetiSipasKodit("Makro");
        //    DbAdmin.colLidhjetAutorizim colLidhjetAutorizim = new DbAdmin.colLidhjetAutorizim(makro.IdKokaMakro, DbCore.DbKontabiliteti.clsLlojBuxheti.mbushIDLlojBuxheti("Makro"));
        //    //DbAdmin.colLidhjetAutorizim colLidhjetAutorizim = new DbAdmin.clsDatabaseAdmin().merrLidhjeAutorizimSipasIdLidheseIdLloji(makro.IdKokaMakro, DbCore.DbKontabiliteti.clsLlojBuxheti.mbushIDLlojBuxheti("Makro"));
        //    colTrupatMakro trupat = new colTrupatMakro(makro.IdKokaMakro);
        //    //colTrupatMakro trupat = merrTrupatMakroSipasKokes(makro.IdKokaMakro);
        //    dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        //    dbManager.ConnectionString = dbManager.GetConnectionString();
        //    dbManager.Open();
        //    dbManager.BeginTransaction();
        //    clsMesazh mesazh = new clsMesazh();
        //    DbCore.clsMesazh mesazhAdmin = new DbCore.clsMesazh(true);
        //    try
        //    {
        //        mesazh = modifikoKokaMakro(makro.IdKokaMakro, makro.KodiKokaMakro, makro.PershkrimiKokaMakro, makro.IdNderViti, makro.IdPerdoruesi, makro.IdNdermarje);
        //        //mesazh = modifikoKokaMakro(makro);
        //        if (mesazh.Status)
        //        {
        //            DbAdmin.colLidhjetAutorizim colLidhjet = new DbAdmin.colLidhjetAutorizim();
        //            if (makro.IdNivelAutorizimi != "")
        //            {

        //                string[] pars1 = makro.IdNivelAutorizimi.Split(',');
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
        //                        colLidhjet[i].IdLloji = DbCore.DbKontabiliteti.clsLlojBuxheti.mbushIDLlojBuxheti("Makro");
        //                        colLidhjet[i].IdLidhese = makro.IdKokaMakro;
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
        //                            colLidhjet[i].IdLloji = DbCore.DbKontabiliteti.clsLlojBuxheti.mbushIDLlojBuxheti("Makro");
        //                            colLidhjet[i].IdLidhese = makro.IdKokaMakro;

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
        //                if (trupat.Count < makro.OColTrupatMakro.Count)//rasti kur jane shtuar rreshta trupi
        //                {
        //                    for (int i = 0; i < makro.OColTrupatMakro.Count; i++)
        //                    {
        //                        if (mesazh.Status)
        //                        {
        //                            makro.OColTrupatMakro[i].IdKokaMakro = makro.IdKokaMakro;
        //                            if (i < trupat.Count)
        //                            {

        //                                makro.OColTrupatMakro[i].IdTrupiMakro = trupat[i].IdTrupiMakro;
        //                                mesazh = modifikoTrupiMakro(makro.OColTrupatMakro[i].IdTrupiMakro, makro.OColTrupatMakro[i].IdKokaMakro, makro.OColTrupatMakro[i].IdLlojMakro, makro.OColTrupatMakro[i].IdProdukti, makro.OColTrupatMakro[i].Pershkrimi, makro.OColTrupatMakro[i].IdFunksionMakro, makro.OColTrupatMakro[i].Vlera, makro.OColTrupatMakro[i].Renditja);
        //                                //mesazh=  modifikoTrupiMakro(makro.OColTrupatMakro[i]);
        //                            }
        //                            else
        //                            {
        //                                int idoC;
        //                                mesazh = ruajTrupiMakro(out idoC, makro.OColTrupatMakro[i].IdKokaMakro, makro.OColTrupatMakro[i].IdLlojMakro, makro.OColTrupatMakro[i].IdProdukti, makro.OColTrupatMakro[i].Pershkrimi, makro.OColTrupatMakro[i].IdFunksionMakro, makro.OColTrupatMakro[i].Vlera, makro.OColTrupatMakro[i].Renditja);
        //                                //mesazh=  ruajTrupiMakro(makro.OColTrupatMakro[i]);
        //                            }
        //                        }
        //                        else
        //                        {
        //                            dbManager.Transaction.Rollback();
        //                            return mesazh;
        //                        }
        //                    }
        //                }
        //                else//rasti kur jane fshire rreshta
        //                {
        //                    int count = 0;
        //                    for (int i = 0; i < trupat.Count; i++)
        //                    {
        //                        if (mesazh.Status)
        //                        {
        //                            if (count < makro.OColTrupatMakro.Count)
        //                            {

        //                                makro.OColTrupatMakro[i].IdKokaMakro = makro.IdKokaMakro;

        //                                makro.OColTrupatMakro[i].IdTrupiMakro = trupat[i].IdTrupiMakro;
        //                                mesazh = modifikoTrupiMakro(makro.OColTrupatMakro[i].IdTrupiMakro, makro.OColTrupatMakro[i].IdKokaMakro, makro.OColTrupatMakro[i].IdLlojMakro, makro.OColTrupatMakro[i].IdProdukti, makro.OColTrupatMakro[i].Pershkrimi, makro.OColTrupatMakro[i].IdFunksionMakro, makro.OColTrupatMakro[i].Vlera, makro.OColTrupatMakro[i].Renditja);
        //                                //mesazh=  modifikoTrupiMakro(makro.OColTrupatMakro[i]);
        //                            }
        //                            else
        //                            {
        //                                mesazh = fshiTrupiMakro(trupat[i].IdTrupiMakro);
        //                                //mesazh=  fshiTrupiMakro(trupat[i]);
        //                            }
        //                            count++;
        //                        }
        //                        else
        //                        {
        //                            dbManager.Transaction.Rollback();
        //                            return mesazh;
        //                        }
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
        //                mesazh.Status = false;
        //                mesazh.PershkrimMesazhi = mesazhAdmin.PershkrimMesazhi;
        //                return mesazh;
        //            }
        //        }
        //        else
        //        {
        //            dbManager.Transaction.Rollback();
        //                return mesazh;
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
        /// perdoret per te kontrolluar nqs ekziston nje makro me kete kod ne kete ndermarje
        /// behet kontrolli per qellime konsistence te informacionit ne DB, nuk lejon te celen makros te ndryshem me te njejtin kod
        /// </summary>
        ///<param name="idndermarje"> id e ndermarjes</param>
        ///<param name="kod"> kodi i makros</param>
        /// <returns>nje objekt boolean qe tregon nese ekziston apo jo nje makro me kete kod</returns>
        public bool ekzistonMakro(String kod, int idndermarje)
        {//kontrollon nqs ekziston nje Makro me kete kod

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@KODIKOKAMAKRO", kod, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idndermarje, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KOKAMAKRO_ekzistonMakro");
            if (ds.Tables[0].Rows.Count == 1)
                return true;
            else if (ds.Tables[0].Rows.Count == 0)
                return false;
            else return true;

        }

        /// <summary>
        /// perdoret per te kontrolluar nese kjo makro eshte e lidhur me makro te tjera.
        /// kjo behet per te mos lejuar te fshihet nje makro te lidhur me nje makro tjeter
        /// </summary>
        /// <param name="id"> id e makros</param>
        /// <returns> nje objekt boolean qe tregon nese kjo makro eshte e lidhur me nje makro tjeter apo jo</returns>
        public bool kaVeprimeMakro(int id)
        {//kontrollon nqs ka veprime me kete makro

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDKOKAMAKRO", id, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KOKAMAKRO_kaVeprime");
            if (ds.Tables[0].Rows.Count == 1)
                return true;
            else if (ds.Tables[0].Rows.Count == 0)
                return false;
            else return true;

        }

        #endregion

        /// <summary>
        /// Metodat e meposhtme i perkasin veprimeve qe kryhen nga objektet colLlojeMakrosh 
        /// </summary>
        #region LLOJ MAKROSH

        /// <summary>
        /// kthen datatable lloj makro 
        /// </summary>
        /// <returns> nje datatable qe permban nje koleksion me te gjithe llojet e makrove</returns>
        internal DataTable ktheGjitheLlojeMakrosh()
        {

            dbManager.Open();
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_LLOJMAKRO _merrGjitheLlojMakro");
            return ds.Tables[0];

        }
        //[Obsolete("Perdor: DataTable ktheGjitheLlojeMakrosh()", true)]
        //public colLlojeMakrosh  merrGjitheLlojeMakrosh()
        //{
        //    IDBManager dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        //    dbManager.ConnectionString = dbManager.GetConnectionString();
        //    try
        //    {
        //        dbManager.Open();

        //        DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_LLOJMAKRO _merrGjitheLlojMakro");
        //        colLlojeMakrosh colLlojeMakrosh = new colLlojeMakrosh();
        //        return colLlojeMakrosh.mbushArrayListLlojeMakrosh(ds);
        //    }
        //    catch (Exception)
        //    {
        //        return new colLlojeMakrosh();
        //    }
        //    finally
        //    {
        //        dbManager.Dispose();
        //    }

        //}

        #endregion
        /// <summary>
        /// Metodat e meposhtme i perkasin veprimeve qe kryhen nga objektet colMetodakostoje 
        /// </summary>
        #region METODA KOSTOJE

        /// <summary>
        /// kthen datatable metode kostoje 
        /// </summary>
        /// <returns> nje datatable qe permban nje koleksion me te gjithe metoda kostoje</returns>
        internal DataTable ktheGjitheMetodeKostoje()
        {

            dbManager.Open();
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_METODEKOSTOJE_merrTeGjitha");
            return ds.Tables[0];

        }
        /// <summary>
        /// kthen datatable metode kostoje  sipas id
        /// </summary>
        /// <returns> nje datatable qe permban nje koleksion me te gjithe metoda kostoje</returns>
        internal DataRow ktheMetodeKostojeSipasId(int idmetode)
        {

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDMETODEKOSTOJE", idmetode, ParameterDirection.Input);

            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_METODEKOSTOJE_merrSipasId");
            return ds.Tables[0].Rows[0];

        }
        /// <summary>
        /// kthen datatable metode kostoje   sipas kodit
        /// </summary>
        /// <returns> nje datatable qe permban nje koleksion me te gjithe metoda kostoje</returns>
        internal DataRow ktheMetodKostojeSipasKodit(string kodi)
        {

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@KODI", kodi, ParameterDirection.Input);

            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_METODEKOSTOJE_merrSipasKodit");
            return ds.Tables[0].Rows[0];

        }


        #endregion
        /// <summary>
        /// Metodat e meposhtme i perkasin veprimeve qe kryhen nga objektet colFunksioneMakrosh 
        /// </summary>
        #region FUNKSIONE MAKROSH

        /// <summary>
        /// kthen datatable funksione makro 
        /// </summary>
        /// <returns> nje datatable qe permban nje koleksion me te gjithe funksionet e makrove</returns>
        internal DataTable ktheGjitheFunksioneMakrosh()
        {

            dbManager.Open();

            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_FUNKSIONMAKRO _merrGjitheFunksionMakro");
            return ds.Tables[0];


        }
        //[Obsolete("Perdor: DataTable ktheGjitheFunksioneMakrosh()", true)]
        //public colFunksioneMakrosh  merrGjitheFunksioneMakrosh()
        //{
        //    IDBManager dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        //    dbManager.ConnectionString = dbManager.GetConnectionString();
        //    try
        //    {
        //        dbManager.Open();

        //        DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_FUNKSIONMAKRO _merrGjitheFunksionMakro");
        //        colFunksioneMakrosh colFunksioneMakrosh = new colFunksioneMakrosh();
        //        return colFunksioneMakrosh.mbushArrayListFunksioneMakrosh(ds);
        //    }
        //    catch (Exception)
        //    {
        //        return new colFunksioneMakrosh();
        //    }
        //    finally
        //    {
        //        dbManager.Dispose();
        //    }

        //}

        #endregion

        /// <summary>
        /// Metodat e meposhtme i perkasin veprimeve qe kryhen nga objektet clsDetajimArtikulli dhe colDetajimeArtikulli
        /// </summary>
        #region DETAJIM ARTIKULLI

        /// <summary>
        /// ekzekuton prc_T_DETAJIMARTIKULLI_ins ne DB dhe kthen statusin e perfundimit te ekzekutimit te SP-se perkatese
        /// Shiko clsMesazh brenda ketij moduli per informacion te metejshem mbi vleren e kthimit te kesaj metode
        /// <param name="idDetajimArtikulli"> id ritese e detajimit</param>
        /// <param name="kodDetajimArtikulli">kodi i detajimit</param>
        /// <param name="llojDetajimArtikulli">lloji i detajimit</param>
        /// <param name="pershkrimDetajimArtikulli">peshkrimi i detajimit</param>
        /// <param name="idPerdoruesi"> id e perdoruesit qe e ka kryer veprimin</param>
        /// <param name="idNderViti"> id e ndermarje vitit</param>
        /// <param name="kategoriDetajimi"> kategoria e detajimit</param>
        /// <param name="idndermrje">id e ndermarjes</param>
        /// <returns> nje objekt boolean qe tregon nese ruajtja eshte kryer ne rregull apo jo.</returns>
        /// </summary>
        internal clsMesazh ruajDetajimArtikulli(out int idDetajimArtikulli, string kodDetajimArtikulli, int llojDetajimArtikulli, string pershkrimDetajimArtikulli, int idPerdoruesi, int kategoriDetajimi, int idndermrje, int idstatusdok, int loan)
        {//ruajtja e detajimArtikulli
            idDetajimArtikulli = 0;

            dbManager.Open();
            dbManager.CreateParameters(9);
            dbManager.AddParameters(0, "@IDDETAJIMARTIKULLI", idDetajimArtikulli, ParameterDirection.Output);
            dbManager.AddParameters(1, "@KODDETAJIMARTIKULLI", kodDetajimArtikulli, ParameterDirection.Input);
            dbManager.AddParameters(2, "@LLOJDETAJIMARTIKULLI", llojDetajimArtikulli, ParameterDirection.Input);
            dbManager.AddParameters(3, "@PERSHKRIMDETAJIMARTIKULLI", pershkrimDetajimArtikulli, ParameterDirection.Input);
            dbManager.AddParameters(4, "@IDPERDORUESI", idPerdoruesi, ParameterDirection.Input);
            dbManager.AddParameters(5, "@KATEGORIDETAJIMI", kategoriDetajimi, ParameterDirection.Input);
            dbManager.AddParameters(6, "@IDNDERMARJE", idndermrje, ParameterDirection.Input);
            dbManager.AddParameters(7, "@IDSTATUSDOK", idstatusdok, ParameterDirection.Input);
            dbManager.AddParameters(8, "@LOAN", loan, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_DETAJIMARTIKULLI_ins");
            idDetajimArtikulli = int.Parse(dbManager.Parameters[0].Value.ToString());
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
            return mesazh;

        }
        //[Obsolete("Perdor: clsMesazh ruajDetajimArtikulli(int idDetajimArtikulli, string kodDetajimArtikulli, int llojDetajimArtikulli, string pershkrimDetajimArtikulli, int idPerdoruesi, int idNderViti, int kategoriDetajimi, int idndermrje)", true)]
        //public clsMesazh ruajDetajimArtikulli(clsDetajimArtikulli detajimArtikulli)
        //{//ruajtja e detajimArtikulli
        //    try
        //    {
        //        dbManager.CreateParameters(8);
        //        dbManager.AddParameters(0, "@IDDETAJIMARTIKULLI", detajimArtikulli.IdDetajimArtikulli, ParameterDirection.Output);
        //        dbManager.AddParameters(1, "@KODDETAJIMARTIKULLI", detajimArtikulli.KodDetajimArtikulli, ParameterDirection.Input);
        //        dbManager.AddParameters(2, "@LLOJDETAJIMARTIKULLI", detajimArtikulli.LlojDetajimArtikulli, ParameterDirection.Input);
        //        dbManager.AddParameters(3, "@PERSHKRIMDETAJIMARTIKULLI", detajimArtikulli.PershkrimDetajimArtikulli, ParameterDirection.Input);
        //        dbManager.AddParameters(4, "@IDPERDORUESI", detajimArtikulli.IdPerdoruesi, ParameterDirection.Input);
        //        dbManager.AddParameters(5, "@IDNDERVITI", detajimArtikulli.IdNderViti, ParameterDirection.Input);
        //        dbManager.AddParameters(6, "@KATEGORIDETAJIMI", detajimArtikulli.KategoriDetajimi, ParameterDirection.Input);
        //        dbManager.AddParameters(7, "@IDNDERMARJE", detajimArtikulli.IdNdermarje , ParameterDirection.Input);
        //        dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_DETAJIMARTIKULLI_ins");
        //        detajimArtikulli.IdDetajimArtikulli = int.Parse(dbManager.Parameters[0].Value.ToString());
        //        clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
        //        return mesazh;
        //    }
        //    catch (Exception)
        //    {
        //        return new clsMesazh(false, ce.Message);
        //    }

        //}

        /// <summary>
        /// ekzekuton prc_T_DETAJIMARTIKULLI_upd ne DB dhe kthen statusin e perfundimit te ekzekutimit te SP-se perkatese
        /// Shiko clsMesazh brenda ketij moduli per informacion te metejshem mbi vleren e kthimit te kesaj metode
        /// <param name="idDetajimArtikulli"> id ritese e detajimit</param>
        /// <param name="kodDetajimArtikulli">kodi i detajimit</param>
        /// <param name="llojDetajimArtikulli">lloji i detajimit</param>
        /// <param name="pershkrimDetajimArtikulli">peshkrimi i detajimit</param>
        /// <param name="idPerdoruesi"> id e perdoruesit qe e ka kryer veprimin</param>
        /// <param name="idNderViti"> id e ndermarje vitit</param>
        /// <param name="kategoriDetajimi"> kategoria e detajimit</param>
        /// <param name="idndermrje">id e ndermarjes</param>
        /// <returns> nje objekt clsMesazh qe tregon nese modifikimi eshte kryer ne rregull apo jo.</returns>
        /// </summary>
        internal clsMesazh modifikoDetajimArtikulli(int idDetajimArtikulli, string kodDetajimArtikulli, int llojDetajimArtikulli, string pershkrimDetajimArtikulli, int idPerdoruesi, int kategoriDetajimi, int idndermrje, int idstatusdok, int loan)
        {
            ImbLogger.LogTraceShitje($"Filloi metoda modifkoDetajimArtikulli me parametra => idDetajimArtikulli:{idDetajimArtikulli}, kodDetajimArtikulli:{kodDetajimArtikulli}, llojDetajimArtikulli:{llojDetajimArtikulli}, pershkrimDetajimArtikulli:{pershkrimDetajimArtikulli}, idPerdorues:{idPerdoruesi}, katgoriDetajimi:{kategoriDetajimi}, idstatusdok:{idstatusdok}, loan:{loan}");
            dbManager.Open();
            dbManager.CreateParameters(9);
            dbManager.AddParameters(0, "@IDDETAJIMARTIKULLI", idDetajimArtikulli, ParameterDirection.Input);
            dbManager.AddParameters(1, "@KODDETAJIMARTIKULLI", kodDetajimArtikulli, ParameterDirection.Input);
            dbManager.AddParameters(2, "@LLOJDETAJIMARTIKULLI", llojDetajimArtikulli, ParameterDirection.Input);
            dbManager.AddParameters(3, "@PERSHKRIMDETAJIMARTIKULLI", pershkrimDetajimArtikulli, ParameterDirection.Input);
            dbManager.AddParameters(4, "@IDPERDORUESI", idPerdoruesi, ParameterDirection.Input);
            dbManager.AddParameters(5, "@KATEGORIDETAJIMI", kategoriDetajimi, ParameterDirection.Input);
            dbManager.AddParameters(6, "@IDNDERMARJE", idndermrje, ParameterDirection.Input);
            dbManager.AddParameters(7, "@IDSTATUSDOK", idstatusdok, ParameterDirection.Input);
            dbManager.AddParameters(8, "@LOAN", loan, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_DETAJIMARTIKULLI_upd");
            idDetajimArtikulli = int.Parse(dbManager.Parameters[0].Value.ToString());
            ImbLogger.LogTraceShitje($"Mbaroi metoda modifkoDetajimArtikulli me parametra => idDetajimArtikulli:{idDetajimArtikulli}, kodDetajimArtikulli:{kodDetajimArtikulli}, llojDetajimArtikulli:{llojDetajimArtikulli}, pershkrimDetajimArtikulli:{pershkrimDetajimArtikulli}, idPerdorues:{idPerdoruesi}, katgoriDetajimi:{kategoriDetajimi}, idstatusdok:{idstatusdok}, loan:{loan}");
            ImbLogger.LogTraceShitje("Ruajtja perfundoi me sukses!");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
            return mesazh;

        }
        
        internal DataTable merrDetajimeSipasArtikullitMagazinesDheRadhesSeHyrjes(string kodiArtikullit, int idndermarje, string kodmagazina, DateTime data, bool promocione)
        {


            try
            {
                dbManager.Open();
                dbManager.CreateParameters(5);
                dbManager.AddParameters(0, "@KODARTIKULLI", kodiArtikullit, ParameterDirection.Input);
                dbManager.AddParameters(1, "@IDNDERMARJE", idndermarje, ParameterDirection.Input);
                dbManager.AddParameters(2, "@kodmagazine", kodmagazina, ParameterDirection.Input);
                dbManager.AddParameters(3, "@data", data, ParameterDirection.Input);
                dbManager.AddParameters(4, "@promocione", promocione, ParameterDirection.Input);
                DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_DETAJIMARTIKULLI_merrDetajimeSipasArtikullitMagazinesDheRadhesSeHyrjes");
                return ds.Tables[0];
            }
            catch (Exception ce)
            {
                return null;
            }
        }
        /// <summary>
        /// ekzekutohet sp-ja prc_T_DETAJIMARTIKULLI_del duke i kaluar id e detajimit qe e marrim nga objekti clsDetajimArtikulli qe i kalohet si parameter
        /// </summary>
        /// <param name="idDetajimArtikulli"> id ritese e detajimit</param>
        /// <returns>nje objekt boolean qe tregon nese fshirja eshte kryer ne rregull apo jo</returns>
        internal clsMesazh fshiDetajimArtikulli(int idDetajimArtikulli)
        {//fshirja e DetajimArtikulli


            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDDETAJIMARTIKULLI", idDetajimArtikulli, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_DETAJIMARTIKULLI_del");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
            return mesazh;

        }
        internal clsMesazh fshiDetajimArtikulliStatus(int idDetajimArtikulli, int idperdorues)
        {//fshirja e DetajimArtikulli


            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDDETAJIMARTIKULLI", idDetajimArtikulli, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUESI", idperdorues, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_DETAJIMARTIKULLI_upddel");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
            return mesazh;

        }
        internal clsMesazh ndryshoLoanDetajimArt(int idDetajimArtikulli, int idperdorues, int loan)
        {//fshirja e DetajimArtikulli


            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@IDDETAJIMARTIKULLI", idDetajimArtikulli, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUESI", idperdorues, ParameterDirection.Input);
            dbManager.AddParameters(2, "@LOAN", loan, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_DETAJIMARTIKULLI_updLoan");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
            return mesazh;

        }
        //[Obsolete("Perdor: clsMesazh fshiDetajimArtikulli(int idDetajimArtikulli)", true)]
        //public clsMesazh fshiDetajimArtikulli(clsDetajimArtikulli detajimArtikulli)
        //{//fshirja e DetajimArtikulli
        //    try
        //    {
        //        dbManager.CreateParameters(1);
        //        dbManager.AddParameters(0, "@IDDETAJIMARTIKULLI", detajimArtikulli.IdDetajimArtikulli, ParameterDirection.Input);
        //        dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_DETAJIMARTIKULLI_del");
        //        clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
        //        return mesazh;
        //    }
        //    catch (Exception)
        //    {
        //        return new clsMesazh(false, ce.Message);
        //    }
        //}

        ///// <summary>
        ///// fshin nje objekt detajimet sebashku me  autorizimet
        ///// Nje objekt detajim artikulli ka nje koleksion me  autorizimet, 
        ///// fshirja e nje detajim artikulli imponon fshirjen edhe te nje colection-i me  autorizimet
        ///// Mqs cdo rresht i ri qe fshihet ne DB kerkon thirrjen e nje SP-je me parametra dhe detajim artikulli bashke me  autorizimet konsiderohet si nje regjistrim,
        ///// perdoret nje transaksion qe imponon fshirjen e rregullt te nje detajimi sebashku me  autorizimet
        ///// behet dhe kontrolli nese me kete detajim ka veprimi per te mos lejuar te fshihet nje detajim me te cilin ka veprime
        ///// </summary>
        ///// <param name="idDetajimArtikulli"> id ritese e detajimit</param>
        ///// <returns> kthen nje obj clsMesazh per te identifikuar statusin e fshirjes se te dhenave ne DB</returns>
        //[Obsolete("Perdor: clsMesazh fshiDetajim(int idDetajimArtikulli)", true)]
        //internal clsMesazh fshiDetajim(int idDetajimArtikulli)
        //{//fshin detajimArtikulli
        //    DbCore.DbKontabiliteti.clsDatabaseKontabilitet db = new clsDatabaseKontabilitet();

        //    if (dbManager == null)
        //    {
        //        dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        //        dbManager.ConnectionString = dbManager.GetConnectionString();
        //    }
        //    dbManager.Open();
        //    dbManager.BeginTransaction();
        //    clsMesazh mesazh = new clsMesazh();
        //    DbCore.clsMesazh mesazhAdmin = new DbCore.clsMesazh(true);
        //    try
        //    {
        //        mesazh = kaVeprimeDetajim(idDetajimArtikulli);
        //        if (!mesazh.Status)
        //        {
        //            //colLlojeBuxhetesh colLloj = db.merrLlojBuxhetiSipasKodit("DetajimArtikulli");
        //            DbAdmin.colLidhjetAutorizim colLidhjeAutorizim = new DbAdmin.colLidhjetAutorizim(idDetajimArtikulli, DbCore.DbKontabiliteti.clsLlojBuxheti.mbushIDLlojBuxheti("DetajimArtikulli"));
        //            //DbAdmin.colLidhjetAutorizim colLidhjeAutorizim = new DbAdmin.clsDatabaseAdmin().merrLidhjeAutorizimSipasIdLidheseIdLloji(idDetajimArtikulli, DbCore.DbKontabiliteti.clsLlojBuxheti.mbushIDLlojBuxheti("DetajimArtikulli"));
        //            foreach (DbAdmin.clsLidhjeAutorizim o in colLidhjeAutorizim)
        //            {
        //                if (mesazhAdmin.Status)
        //                    mesazhAdmin = new DbAdmin.clsDatabaseAdmin().fshiLidhjeAutorizim(o.IdLidhjeAutorizim);

        //                else
        //                {
        //                    dbManager.Transaction.Rollback();
        //                    mesazh.Status = false;
        //                    mesazh.PershkrimMesazhi = mesazhAdmin.PershkrimMesazhi;
        //                    return mesazh;
        //                }
        //            }
        //            if (mesazhAdmin.Status)
        //            {
        //                mesazh = fshiDetajimArtikulli(idDetajimArtikulli);
        //                if (mesazh.Status)
        //                {

        //                    dbManager.CommitTransaction();
        //                    mesazh = new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
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
        //                mesazh.Status = false;
        //                mesazh.PershkrimMesazhi = mesazhAdmin.PershkrimMesazhi;
        //                return mesazh;
        //            }
        //        }
        //        else
        //        {
        //            return mesazh;
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        dbManager.Transaction.Rollback();
        //        return new clsMesazh(false, ce.Message);
        //    }
        //}
        //[Obsolete("Perdor: clsMesazh fshiDetajim(int idDetajimArtikulli)", true)]
        //public clsMesazh fshiDetajim(clsDetajimArtikulli detajimArtikulli)
        //{//fshin detajimArtikulli
        //    DbCore.DbKontabiliteti.clsDatabaseKontabilitet db = new clsDatabaseKontabilitet();            

        //    dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        //    dbManager.ConnectionString = dbManager.GetConnectionString();
        //    dbManager.Open();
        //    dbManager.BeginTransaction();
        //    clsMesazh mesazh = new clsMesazh();
        //    DbCore.clsMesazh mesazhAdmin = new DbCore.clsMesazh(true);
        //    try
        //    {
        //            mesazh = kaVeprimeDetajim(detajimArtikulli.IdDetajimArtikulli);
        //        if (!mesazh.Status)
        //        {
        //            //colLlojeBuxhetesh colLloj = db.merrLlojBuxhetiSipasKodit("DetajimArtikulli");
        //            DbAdmin.colLidhjetAutorizim colLidhjeAutorizim = new DbAdmin.colLidhjetAutorizim(detajimArtikulli.IdDetajimArtikulli, DbCore.DbKontabiliteti.clsLlojBuxheti.mbushIDLlojBuxheti("DetajimArtikulli"));
        //            //DbAdmin.colLidhjetAutorizim colLidhjeAutorizim = new DbAdmin.clsDatabaseAdmin().merrLidhjeAutorizimSipasIdLidheseIdLloji(detajimArtikulli.IdDetajimArtikulli, DbCore.DbKontabiliteti.clsLlojBuxheti.mbushIDLlojBuxheti("DetajimArtikulli"));
        //            foreach (DbAdmin.clsLidhjeAutorizim o in colLidhjeAutorizim)
        //            {
        //                if (mesazhAdmin.Status)
        //                    mesazhAdmin = new DbAdmin.clsDatabaseAdmin().fshiLidhjeAutorizim(o.IdLidhjeAutorizim);

        //                else
        //                {
        //                    dbManager.Transaction.Rollback();
        //                    mesazh.Status = false;
        //                    mesazh.PershkrimMesazhi = mesazhAdmin.PershkrimMesazhi;
        //                    return mesazh;
        //                }
        //            }
        //            if (mesazhAdmin.Status)
        //            {
        //                mesazh= fshiDetajimArtikulli(detajimArtikulli.IdDetajimArtikulli);
        //                if (mesazh.Status)
        //                {

        //                    dbManager.CommitTransaction();
        //                    mesazh = new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
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
        //                mesazh.Status = false;
        //                mesazh.PershkrimMesazhi = mesazhAdmin.PershkrimMesazhi;
        //                return mesazh;
        //            }
        //        }
        //        else
        //        {
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
        /// kthen dataTable detajim artikulli sipas ndermarjes dhe autorizimit
        /// </summary>
        /// <param name="idndermarje"> id e ndermarjes</param>
        /// <param name="idperdorues"> id e perdoruesi</param>
        /// <returns> nje datatable qe permban nje koleksion me detajimet e kesaj ndermarje dhe per te cilat ka autorizim ky perdorues</returns>
        internal DataTable ktheDetajimeSipasNdermarrjesAndAutorizime(int idndermarje, int idperdorues)
        {


            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDNDERMARJE", idndermarje, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUES", idperdorues, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_DETAJIMARTIKULLI_merrDetajimArtikulliSipasNdermarrjesAndAutorizime");

            return ds.Tables[0];


        }
        //[Obsolete("Perdor: DataTable ktheDetajimeSipasNdermarrjesAndAutorizime(int idndermarje, int idperdorues)", true)]
        //public colDetajimeArtikulli merrDetajimeSipasNdermarrjesAndAutorizime(int idndermarje, int idperdorues)
        //{//metoda per te marre te gjithe detajimArtikullit
        //    IDBManager dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        //    dbManager.ConnectionString = dbManager.GetConnectionString();
        //    try
        //    {
        //        dbManager.Open();
        //        dbManager.CreateParameters(2);
        //        dbManager.AddParameters(0, "@IDNDERMARJE", idndermarje, ParameterDirection.Input);
        //        dbManager.AddParameters(1, "@IDPERDORUES", idperdorues, ParameterDirection.Input);
        //        DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_DETAJIMARTIKULLI_merrDetajimArtikulliSipasNdermarrjesAndAutorizime");
        //        colDetajimeArtikulli colDetajimeArtikulli = new colDetajimeArtikulli();
        //        return colDetajimeArtikulli.mbushArrayListDetajimArtikullish(ds);
        //    }
        //    catch (Exception)
        //    {
        //        return new colDetajimeArtikulli();
        //    }
        //    finally
        //    {
        //        dbManager.Dispose();
        //    }

        //}

        /// <summary>
        /// kthen datatable detajim artikulli sipas artikullit, ndermarjes dhe autorizimeve
        /// </summary>
        /// <param name="kodiArtikullit"> kodi i artikullit</param>
        /// <param name="idndermarje"> id e ndermarjes</param>
        /// <param name="idperdorues"> id e perdoruesit</param>
        /// <returns> nje datatable qe permban nje koleksion me detajimet e ketij artikulli , te kesaj ndermarje dhe per te cilat ka autorizim ky perdorues</returns>
        internal DataTable ktheDetajimeSipasArtikullitAndNdermarrjesAndAutorizime(string kodiArtikullit, int idndermarje, int idperdorues)
        {


            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@KODARTIKULLI", kodiArtikullit, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idndermarje, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDPERDORUES", idperdorues, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_DETAJIMARTIKULLI_merrDetajimeSipasArtikullitAndNdermarrjesAndAutorizime");
            return ds.Tables[0];
        }

        /// <summary>
        /// kthen datatable detajim artikulli sipas artikullit, ndermarjes dhe autorizimeve
        /// </summary>
        /// <param name="kodiArtikullit"> kodi i artikullit</param>
        /// <param name="idndermarje"> id e ndermarjes</param>
        /// <param name="idperdorues"> id e perdoruesit</param>
        /// <returns> nje datatable qe permban nje koleksion me detajimet e ketij artikulli , te kesaj ndermarje dhe per te cilat ka autorizim ky perdorues</returns>
        internal DataTable ktheDetajimeSipasMeLlojArtikullitAndNdermarrjesAndAutorizime(string kodiArtikullit, int idndermarje, int idperdorues)
        {
            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@KODARTIKULLI", kodiArtikullit, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idndermarje, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDPERDORUES", idperdorues, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_DETAJIMARTIKULLI_merrDetajimeMeLlojSipasArtikullitAndNdermarrjesAndAutorizime");
            return ds.Tables[0];
        }

        /// <summary>
        /// kthen datatable detajim artikulli sipas artikullit, ndermarjes dhe autorizimeve DHE LLOJIT
        /// </summary>
        /// <param name="kodiArtikullit"> kodi i artikullit</param>
        /// <param name="idndermarje"> id e ndermarjes</param>
        /// <param name="idperdorues"> id e perdoruesit</param>
        /// <param name="lloji">lloji detajim i pare apo i dyte</param>
        /// <returns> nje datatable qe permban nje koleksion me detajimet e ketij artikulli , te kesaj ndermarje dhe per te cilat ka autorizim ky perdorues sipas llojit</returns>
        internal DataTable ktheDetajimeSipasArtikullitAndNdermarrjesAndAutorizimeSipasLlojit(string kodiArtikullit, int idndermarje, int idperdorues, int lloji)
        {

            dbManager.Open();
            dbManager.CreateParameters(4);
            dbManager.AddParameters(0, "@KODARTIKULLI", kodiArtikullit, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idndermarje, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDPERDORUES", idperdorues, ParameterDirection.Input);
            dbManager.AddParameters(3, "@LLOJDETAJIM", lloji, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_DETAJIMARTIKULLI_merrDetajimeSipasArtikullitAndNdermarrjesAndAutorizimeSipasLlojit");

            return ds.Tables[0];

        }

        internal DataTable ktheDetajimeSipasIdArtikullitAndNdermarrjesAndAutorizimeSipasLlojit(int idArtikullit, int idndermarje, int idperdorues, int lloji)
        {

            dbManager.Open();
            dbManager.CreateParameters(4);
            dbManager.AddParameters(0, "@IDARTIKULLI", idArtikullit, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idndermarje, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDPERDORUES", idperdorues, ParameterDirection.Input);
            dbManager.AddParameters(3, "@LLOJDETAJIM", lloji, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_DETAJIMARTIKULLI_merrDetajimeSipasIdArtikullitAndNdermarrjesAndAutorizimeSipasLlojit");
            return ds.Tables[0];

        }

        /// <summary>
        /// kthen datatable detajim artikulli sipas artikullit, ndermarjes dhe autorizimeve DHE LLOJIT
        /// </summary>
        /// <param name="kodiArtikullit"> kodi i artikullit</param>
        /// <param name="idndermarje"> id e ndermarjes</param>
        /// <param name="idperdorues"> id e perdoruesit</param>
        /// <param name="lloji">lloji detajim i pare apo i dyte</param>
        /// <returns> nje datatable qe permban nje koleksion me detajimet e ketij artikulli , te kesaj ndermarje dhe per te cilat ka autorizim ky perdorues sipas llojit</returns>
        internal DataTable ktheDetajimeSipasArtikullitNdermarrjesKategoriseDheLlojit(string kodiArtikullit, int idndermarje, int idperdorues, int idKategori, int lloji)
        {


            dbManager.Open();
            dbManager.CreateParameters(5);
            dbManager.AddParameters(0, "@KODARTIKULLI", kodiArtikullit, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idndermarje, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDPERDORUES", idperdorues, ParameterDirection.Input);
            dbManager.AddParameters(3, "@KATEGORIDETAJIMI", idKategori, ParameterDirection.Input);
            dbManager.AddParameters(4, "@LLOJDETAJIM", lloji, ParameterDirection.Input); //kategoria e pare apo e dyte
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_DETAJIMARTIKULLI_merrDetajimeSipasArtikullitNdermarrjesKategoriseDheLlojit");
            return ds.Tables[0];

        }

        /// <summary>
        /// kthen datatable detajim artikulli sipas artikullit, ndermarjes dhe autorizimeve DHE LLOJIT
        /// </summary>
        /// <param name="kodiArtikullit"> kodi i artikullit</param>
        /// <param name="idndermarje"> id e ndermarjes</param>
        /// <param name="idperdorues"> id e perdoruesit</param>
        /// <param name="lloji">lloji detajim i pare apo i dyte</param>
        /// <returns> nje datatable qe permban nje koleksion me detajimet e ketij artikulli , te kesaj ndermarje dhe per te cilat ka autorizim ky perdorues sipas llojit</returns>
        internal DataTable ktheDetajimeSipasArtikullitNdermarrjesDheLlojit(string kodiArtikullit, int idndermarje, int idPerdorues, int lloji)
        {

            dbManager.Open();
            dbManager.CreateParameters(4);
            dbManager.AddParameters(0, "@KODARTIKULLI", kodiArtikullit, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idndermarje, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDPERDORUES", idPerdorues, ParameterDirection.Input);
            dbManager.AddParameters(3, "@LLOJDETAJIM", lloji, ParameterDirection.Input); //kategoria e pare apo e dyte
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_DETAJIMARTIKULLI_merrDetajimeSipasArtikullitNdermarrjesDheLlojit");
            return ds.Tables[0];

        }

        /// <summary>
        /// kthen datatable detajim artikulli sipas artikullit, ndermarjes dhe autorizimeve DHE LLOJIT
        /// </summary>
        /// <param name="kodiArtikullit"> kodi i artikullit</param>
        /// <param name="idndermarje"> id e ndermarjes</param>
        /// <param name="idperdorues"> id e perdoruesit</param>
        /// <param name="lloji">lloji detajim i pare apo i dyte</param>
        /// <returns> nje datatable qe permban nje koleksion me detajimet e ketij artikulli , te kesaj ndermarje dhe per te cilat ka autorizim ky perdorues sipas llojit</returns>
        internal DataTable ktheDetajimeSipasArtikullitNdermarrjesKategorise(string kodiArtikullit, int idndermarje, int idperdorues, int idKategori)
        {

            dbManager.Open();
            dbManager.CreateParameters(4);
            dbManager.AddParameters(0, "@KODARTIKULLI", kodiArtikullit, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idndermarje, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDPERDORUES", idperdorues, ParameterDirection.Input);
            dbManager.AddParameters(3, "@KATEGORIDETAJIMI", idKategori, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_DETAJIMARTIKULLI_merrDetajimeSipasArtikullitNdermarrjesKategorise");
            return ds.Tables[0];

        }

        /// <summary>
        /// kthen datatable detajim artikulli sipas artikullit, ndermarjes dhe autorizimeve DHE LLOJIT
        /// </summary>
        /// <param name="kodiArtikullit"> kodi i artikullit</param>
        /// <param name="idndermarje"> id e ndermarjes</param>
        /// <param name="idperdorues"> id e perdoruesit</param>
        /// <param name="lloji">lloji detajim i pare apo i dyte</param>
        /// <returns> nje datatable qe permban nje koleksion me detajimet e ketij artikulli , te kesaj ndermarje dhe per te cilat ka autorizim ky perdorues sipas llojit</returns>
        internal DataTable ktheDetajimeSipasNdermarrjesKategoriseDheLlojit(int idndermarje, int idperdorues, int idKategori, int lloji)
        {


            dbManager.Open();
            dbManager.CreateParameters(4);
            dbManager.AddParameters(0, "@IDNDERMARJE", idndermarje, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUES", idperdorues, ParameterDirection.Input);
            dbManager.AddParameters(2, "@KATEGORIDETAJIMI", idKategori, ParameterDirection.Input);
            dbManager.AddParameters(3, "@LLOJDETAJIM", lloji, ParameterDirection.Input); //kategoria e pare apo e dyte
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_DETAJIMARTIKULLI_merrDetajimeSipasNdermarrjesKategoriseDheLlojit");
            return ds.Tables[0];

        }

        //[Obsolete("Perdor: DataTable ktheDetajimeSipasArtikullitAndNdermarrjesAndAutorizime(string kodiArtikullit, int idndermarje, int idperdorues)", true)] 
        //public colDetajimeArtikulli merrDetajimeSipasArtikullitAndNdermarrjesAndAutorizime(string kodiArtikullit,int idndermarje, int idperdorues)
        //{//metoda per te marre te gjithe detajimArtikullit
        //    IDBManager dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        //    dbManager.ConnectionString = dbManager.GetConnectionString();
        //    try
        //    {
        //        dbManager.Open();
        //        dbManager.CreateParameters(3);
        //        dbManager.AddParameters(0, "@KODARTIKULLI", kodiArtikullit, ParameterDirection.Input);
        //        dbManager.AddParameters(1, "@IDNDERMARJE", idndermarje, ParameterDirection.Input);
        //        dbManager.AddParameters(2, "@IDPERDORUES", idperdorues, ParameterDirection.Input);
        //        DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_DETAJIMARTIKULLI_merrDetajimeSipasArtikullitAndNdermarrjesAndAutorizime");
        //        colDetajimeArtikulli colDetajimeArtikulli = new colDetajimeArtikulli();
        //        return colDetajimeArtikulli.mbushArrayListDetajimArtikullish(ds);
        //    }
        //    catch (Exception)
        //    {
        //        return new colDetajimeArtikulli();
        //    }
        //    finally
        //    {
        //        dbManager.Dispose();
        //    }

        //}

        /// <summary>
        /// kthen datatable artikulli sipas ndermarjes, autorizimit, pershkrimit dhe kategorise
        /// </summary>
        /// <param name="idndermarje"> id e ndermarjes</param>
        /// <param name="idperdorues"> id e perdoruesit</param>
        /// <param name="pershkriminga"> pershkrimi nga</param>
        /// <param name="pershkrimideri"> pershkrimi deri</param>
        /// <param name="kategori"> kategoria e detajimit</param>
        /// <returns>nje datatable qe permban nje koleksion me te gjitha detajimet e kesaj ndermarje per te cilat ka autorizim ky perdorues qe kane pershkrimin midis ketyre pershkrimeve dhe jane te kesaj kategorie</returns>
        internal DataTable ktheDetajimeSipasNdermarrjesAndAutorizimeAndKategoribetweenPershkrimi(int idndermarje, int idperdorues, string pershkriminga, string pershkrimideri, int kategori)
        {


            dbManager.Open();
            dbManager.CreateParameters(5);
            dbManager.AddParameters(0, "@IDNDERMARJE", idndermarje, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUES", idperdorues, ParameterDirection.Input);
            dbManager.AddParameters(2, "@PERSHKRIMINGA", pershkriminga, ParameterDirection.Input);
            dbManager.AddParameters(3, "@PERSHKRIMIDERI", pershkrimideri, ParameterDirection.Input);
            dbManager.AddParameters(4, "@KATEGORI", kategori, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_DETAJIMARTIKULLI_merrDetajimArtikulliSipasNdermarrjesAndAutorizimeAndKategoriBetweenPershkrimi");

            return ds.Tables[0];


        }
        //[Obsolete("Perdor: DataTable ktheDetajimeSipasNdermarrjesAndAutorizimeAndKategoribetweenPershkrimi(int idndermarje, int idperdorues, string pershkriminga, string pershkrimideri, int kategori)", true)]
        //public colDetajimeArtikulli merrDetajimeSipasNdermarrjesAndAutorizimeAndKategoribetweenPershkrimi(int idndermarje, int idperdorues, string pershkriminga, string pershkrimideri, int kategori)
        //{//metoda per te marre te gjithe detajimArtikullit
        //    IDBManager dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        //    dbManager.ConnectionString = dbManager.GetConnectionString();
        //    try
        //    {
        //        dbManager.Open();
        //        dbManager.CreateParameters(5);
        //        dbManager.AddParameters(0, "@IDNDERMARJE", idndermarje, ParameterDirection.Input);
        //        dbManager.AddParameters(1, "@IDPERDORUES", idperdorues, ParameterDirection.Input);
        //        dbManager.AddParameters(2, "@PERSHKRIMINGA", pershkriminga, ParameterDirection.Input);
        //        dbManager.AddParameters(3, "@PERSHKRIMIDERI", pershkrimideri, ParameterDirection.Input);
        //        dbManager.AddParameters(4, "@KATEGORI", kategori, ParameterDirection.Input);
        //        DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_DETAJIMARTIKULLI_merrDetajimArtikulliSipasNdermarrjesAndAutorizimeAndKategoriBetweenPershkrimi");
        //        colDetajimeArtikulli colDetajimeArtikulli = new colDetajimeArtikulli();
        //        return colDetajimeArtikulli.mbushArrayListDetajimArtikullish(ds);
        //    }
        //    catch (Exception)
        //    {
        //        return new colDetajimeArtikulli();
        //    }
        //    finally
        //    {
        //        dbManager.Dispose();
        //    }

        //}

        /// <summary>
        /// kthen datatable detajim artikulli sipas ndermarjes, autorizimit dhe kategorise
        /// </summary>
        /// <param name="idndermarje"> id e ndermarjes</param>
        /// <param name="idperdorues"> id e perdoruesit</param>
        /// <param name="kategoriDetajimi"> kategoria e detajimit</param>
        /// <returns>nje datatable qe permban nje koleksion me te gjitha detajimet e kesaj ndermarje per te cilat ka autorizim ky perdorues  dhe jane te kesaj kategorie</returns>
        internal DataTable ktheDetajimeSipasNdermarrjesAndAutorizimeSipasKategorise(int idndermarje, int idperdorues, int kategoriDetajimi)
        {

            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@IDNDERMARJE", idndermarje, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUES", idperdorues, ParameterDirection.Input);
            dbManager.AddParameters(2, "@KATEGORIDETAJIMI", kategoriDetajimi, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_DETAJIMARTIKULLI_merrDetajimArtikulliSipasNdermarrjesAndAutorizimeSipasKategorise");
            return ds.Tables[0];

        }
        internal DataTable ktheDetajimeSipasNdermarrjesAndAutorizimeSipasKategoriseJoArtikulli(int idndermarje, int idperdorues, int kategoriDetajimi, int idartikulli)
        {

            dbManager.Open();
            dbManager.CreateParameters(4);
            dbManager.AddParameters(0, "@IDNDERMARJE", idndermarje, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUES", idperdorues, ParameterDirection.Input);
            dbManager.AddParameters(2, "@KATEGORIDETAJIMI", kategoriDetajimi, ParameterDirection.Input);
            dbManager.AddParameters(3, "@idartikulli", idartikulli, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_DETAJIMARTIKULLI_merrDetajimArtikulliSipasNdermarrjesAndAutorizimeSipasKategoriseJoTeArtikullit");
            return ds.Tables[0];

        }
        //[Obsolete("DataTable ktheDetajimeSipasNdermarrjesAndAutorizimeSipasKategorise(int idndermarje, int idperdorues, int kategoriDetajimi)", true)]
        //public colDetajimeArtikulli merrDetajimeSipasNdermarrjesAndAutorizimeSipasKategorise(int idndermarje, int idperdorues, int kategoriDetajimi)
        //{//metoda per te marre te gjithe detajimArtikullit
        //    IDBManager dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        //    dbManager.ConnectionString = dbManager.GetConnectionString();
        //    try
        //    {
        //        dbManager.Open();
        //        dbManager.CreateParameters(3);
        //        dbManager.AddParameters(0, "@IDNDERMARJE", idndermarje, ParameterDirection.Input);
        //        dbManager.AddParameters(1, "@IDPERDORUES", idperdorues, ParameterDirection.Input);
        //        dbManager.AddParameters(2, "@KATEGORIDETAJIMI", kategoriDetajimi, ParameterDirection.Input);
        //        DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_DETAJIMARTIKULLI_merrDetajimArtikulliSipasNdermarrjesAndAutorizimeSipasKategorise");
        //        colDetajimeArtikulli colDetajimeArtikulli = new colDetajimeArtikulli();
        //        return colDetajimeArtikulli.mbushArrayListDetajimArtikullish(ds);
        //    }
        //    catch (Exception)
        //    {
        //        return new colDetajimeArtikulli();
        //    }
        //    finally
        //    {
        //        dbManager.Dispose();
        //    }

        //}

        /// <summary>
        /// kthen datatable detajim artikulli sipas ndermarjes, autorizimit,artikullit dhe qe iu fillon kodi me kete koddetajimi
        /// </summary>
        /// <param name="idNdermarje">id e ndermarjes</param>
        /// <param name="idperdorues"> id e perdoruesit</param>
        /// <param name="kodDetajimi"> kodi i detajimit</param>
        /// <param name="kodArtikulli"> kodi i artikullit</param>
        /// <param name="lloji">lloji i detajimit</param>
        /// <returns> nje datatable qe permban nje koleksion me te gjitha detajimet e kesaj ndermarje , te ketij artikulli per te cilat ka autorizim ky perdorues  dhe qe fillojne me kete kod detajimi</returns>
        internal DataTable ktheDetajimeSipasArtikullitAndNdermarrjesAndAutorizimeLike(int idNdermarje, int idperdorues, string kodDetajimi, string kodArtikulli, int lloji)
        {


            dbManager.Open();
            dbManager.CreateParameters(5);
            dbManager.AddParameters(0, "@IDNDERMARJE", idNdermarje, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUES", idperdorues, ParameterDirection.Input);
            dbManager.AddParameters(2, "@KODDETAJIMI", kodDetajimi + "%", ParameterDirection.Input);
            dbManager.AddParameters(3, "@KODARTIKULLI", kodArtikulli, ParameterDirection.Input);
            dbManager.AddParameters(4, "@LLOJDETAJIMI", lloji, ParameterDirection.Input);

            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_DETAJIMARTIKULLI_merrDetajimeSipasArtikullitAndNdermarrjesAndAutorizimeLike");

            return ds.Tables[0];


        }
        /// <summary>
        /// kthen datatable detajim artikulli sipas ndermarjes, autorizimit,artikullit dhe qe iu fillon kodi me kete koddetajimi
        /// </summary>
        /// <param name="idNdermarje">id e ndermarjes</param>
        /// <param name="idperdorues"> id e perdoruesit</param>
        /// <param name="kodDetajimi"> kodi i detajimit</param>
        /// <param name="kodArtikulli"> kodi i artikullit</param>
        /// <param name="lloji">lloji i detajimit</param>
        /// <returns> nje datatable qe permban nje koleksion me te gjitha detajimet e kesaj ndermarje , te ketij artikulli per te cilat ka autorizim ky perdorues  dhe qe fillojne me kete kod detajimi</returns>
        internal DataTable ktheDetajimeSipasArtikullitAndNdermarrjesAndAutorizimeLikeNewPati(int idNdermarje, int idperdorues, string kodDetajimi, string kodArtikulli, int lloji, bool sipasGjendjes)
        {
            dbManager.Open();
            dbManager.CreateParameters(6);
            dbManager.AddParameters(0, "@IDNDERMARJE", idNdermarje, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUES", idperdorues, ParameterDirection.Input);
            dbManager.AddParameters(2, "@KODDETAJIMI", kodDetajimi + "%", ParameterDirection.Input);
            dbManager.AddParameters(3, "@KODARTIKULLI", kodArtikulli, ParameterDirection.Input);
            dbManager.AddParameters(4, "@LLOJDETAJIMI", lloji, ParameterDirection.Input);
            dbManager.AddParameters(5, "@SIPASGJENDJES", sipasGjendjes, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_DETAJIMARTIKULLI_merrDetajimeSipasArtikullitAndNdermarrjesAndAutorizimeLikeNew");
            return ds.Tables[0];
        }

        internal DataTable ktheDetajimeSipasArtikujveAndNdermarrjesAndAutorizimeLike(int idNdermarje, int idperdorues, string kodDetajimi, string kodArtikulli, int lloji, bool sipasGjendjes)
        {
            dbManager.Open();
            dbManager.CreateParameters(6);
            dbManager.AddParameters(0, "@IDNDERMARJE", idNdermarje, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUES", idperdorues, ParameterDirection.Input);
            dbManager.AddParameters(2, "@KODDETAJIMI", kodDetajimi + "%", ParameterDirection.Input);
            dbManager.AddParameters(3, "@KODARTIKULLI", kodArtikulli, ParameterDirection.Input);
            dbManager.AddParameters(4, "@LLOJDETAJIMI", lloji, ParameterDirection.Input);
            dbManager.AddParameters(5, "@SIPASGJENDJES", sipasGjendjes, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_DETAJIMARTIKULLI_merrDetajimeSipasArtikujveAndNdermarrjesAndAutorizime");
            return ds.Tables[0];
        }

        /// <summary>
        /// kthen datatable detajim artikulli sipas ndermarjes, autorizimit,artikullit dhe  me kete koddetajimi
        /// </summary>
        /// <param name="idNdermarje">id e ndermarjes</param>
        /// <param name="idperdorues"> id e perdoruesit</param>
        /// <param name="kodDetajimi"> kodi i detajimit</param>
        /// <param name="kodArtikulli"> kodi i artikullit</param>
        /// <param name="lloji">lloji i detajimit</param>
        /// <returns> nje datatable qe permban nje koleksion me te gjitha detajimet e kesaj ndermarje , te ketij artikulli per te cilat ka autorizim ky perdorues  dhe  me kete kod detajimi</returns>
        internal DataRow ktheDetajimeSipasArtikullitDheKodit(int idNdermarje, int idperdorues, string kodDetajimi, string kodArtikulli, int lloji)
        {


            dbManager.Open();
            dbManager.CreateParameters(5);
            dbManager.AddParameters(0, "@IDNDERMARJE", idNdermarje, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUES", idperdorues, ParameterDirection.Input);
            dbManager.AddParameters(2, "@KODDETAJIMI", kodDetajimi, ParameterDirection.Input);
            dbManager.AddParameters(3, "@KODARTIKULLI", kodArtikulli, ParameterDirection.Input);
            dbManager.AddParameters(4, "@LLOJDETAJIMI", lloji, ParameterDirection.Input);

            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_DETAJIMARTIKULLI_merrDetajimeSipasArtikulliDheKodit");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0)
                return null;
            return ds.Tables[0].Rows[0];


        }
        internal DataRow ktheDetajimeSipasArtikullitDheKoditEkzistonTekRegjistrimi(int idNdermarje, int idperdorues, string kodDetajimi, string kodArtikulli, int lloji, int idkokamag)
        {

            dbManager.Open();
            dbManager.CreateParameters(6);
            dbManager.AddParameters(0, "@IDNDERMARJE", idNdermarje, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUES", idperdorues, ParameterDirection.Input);
            dbManager.AddParameters(2, "@KODDETAJIMI", kodDetajimi, ParameterDirection.Input);
            dbManager.AddParameters(3, "@KODARTIKULLI", kodArtikulli, ParameterDirection.Input);
            dbManager.AddParameters(4, "@LLOJDETAJIMI", lloji, ParameterDirection.Input);
            dbManager.AddParameters(5, "@IDKOKAMAG", idkokamag, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_DETAJIMARTIKULLI_merrDetajimeSipasArtikulliDheKoditEkzistonTekRegjistrimi");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0)
                return null;
            return ds.Tables[0].Rows[0];

        }
        internal DataRow ktheDetajimeSipasArtikullitDheKoditEkzistonTekRegjistrimiSiSerial(int idNdermarje, int idperdorues, string kodDetajimi, string kodArtikulli, int lloji, int idkokamag)
        {

            dbManager.Open();
            dbManager.CreateParameters(6);
            dbManager.AddParameters(0, "@IDNDERMARJE", idNdermarje, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUES", idperdorues, ParameterDirection.Input);
            dbManager.AddParameters(2, "@KODDETAJIMI", kodDetajimi, ParameterDirection.Input);
            dbManager.AddParameters(3, "@KODARTIKULLI", kodArtikulli, ParameterDirection.Input);
            dbManager.AddParameters(4, "@LLOJDETAJIMI", lloji, ParameterDirection.Input);
            dbManager.AddParameters(5, "@IDKOKAMAG", idkokamag, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_DETAJIMARTIKULLI_merrDetajimeSipasArtikulliDheKoditEkzistonTekRegjistrimiSiSerial");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0)
                return null;
            return ds.Tables[0].Rows[0];

        }
        internal DataRow ktheDetajimeSipasArtikullitDheIDEkzistonTekRegjistrimi(int idNdermarje, int idperdorues, int iddetajim, int idartikulli, int lloji, int idkokamag)
        {

            dbManager.Open();
            dbManager.CreateParameters(6);
            dbManager.AddParameters(0, "@IDNDERMARJE", idNdermarje, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUES", idperdorues, ParameterDirection.Input);
            dbManager.AddParameters(2, "@iddetajimi", iddetajim, ParameterDirection.Input);
            dbManager.AddParameters(3, "@idARTIKULLI", idartikulli, ParameterDirection.Input);
            dbManager.AddParameters(4, "@LLOJDETAJIMI", lloji, ParameterDirection.Input);
            dbManager.AddParameters(5, "@IDKOKAMAG", idkokamag, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_DETAJIMARTIKULLI_merrDetajimeSipasArtikulliDheIdEkzistonTekRegjistrimi");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0)
                return null;
            return ds.Tables[0].Rows[0];


        }
        //[Obsolete("Perdor: DataTable ktheDetajimeSipasArtikullitAndNdermarrjesAndAutorizimeLike(int idNdermarje, int idperdorues, string kodDetajimi, string kodArtikulli)", true)]
        //public colDetajimeArtikulli merrDetajimeSipasArtikullitAndNdermarrjesAndAutorizimeLike(int idNdermarje, int idperdorues, string kodDetajimi, string kodArtikulli)
        //{
        //    IDBManager dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        //    dbManager.ConnectionString = dbManager.GetConnectionString();
        //    try
        //    {
        //        dbManager.Open();
        //        dbManager.CreateParameters(4);
        //        dbManager.AddParameters(0, "@IDNDERMARJE", idNdermarje, ParameterDirection.Input);
        //        dbManager.AddParameters(1, "@IDPERDORUES", idperdorues, ParameterDirection.Input);
        //        dbManager.AddParameters(2, "@KODDETAJIMI", kodDetajimi + "%", ParameterDirection.Input);
        //        dbManager.AddParameters(3, "@KODARTIKULLI", kodArtikulli, ParameterDirection.Input);

        //        DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_DETAJIMARTIKULLI_merrDetajimeSipasArtikullitAndNdermarrjesAndAutorizimeLike");
        //        colDetajimeArtikulli colDetajimeArtikulli = new colDetajimeArtikulli();
        //        return colDetajimeArtikulli.mbushArrayListDetajimArtikullish(ds);

        //    }
        //    catch (Exception)
        //    {
        //        return new colDetajimeArtikulli();
        //    }
        //    finally
        //    {
        //        dbManager.Dispose();
        //    }
        //}
        internal DataRow merrDetajimArtikulli(int idDetajim)
        {
            ImbLogger.LogTraceShitje($"Filloi metoda merrDetajimArtikulli sipas idDetajim:{idDetajim}");
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDDETAJIMARTIKULLI", idDetajim, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_DETAJIMARTIKULLI_ktheDetajimArtikulliSipasIdNew");
            if (ds == null)
            {
                ImbLogger.LogTraceShitje($"Metoda merrDetajimArtikulli sipas idDetajim:{idDetajim} ktheu null");
                return null;
            }
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
            {
                ImbLogger.LogTraceShitje($"Metoda merrDetajimArtikulli sipas idDetajim:{idDetajim} ktheu null");
                return null;
            }
            ImbLogger.LogTraceShitje($"Mbaroi metoda merrDetajimArtikulli sipas idDetajim:{idDetajim}");
            return ds.Tables[0].Rows[0];

        }

        internal DataRow merrDetajimArtikulliSipasIdDr(int idDetajim)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDDETAJIMARTIKULLI", idDetajim, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_DETAJIMARTIKULLI_ktheDetajimArtikulliSipasIdDr");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];
        }

        internal DataRow ktheDetajimArtSipasIdArtikulliDheDetajimit(int idartikulli, int iddetajim)
        {
            ImbLogger.LogTraceShitje($"Filloi metoda ktheDetajimArtSipasIdArtikulliDheDetajimit sipas idartikulli:{idartikulli}, iddetajim:{iddetajim}");
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDARTIKULLI", idartikulli, ParameterDirection.Input);
            dbManager.AddParameters(1, "@iddetajimartikulli", iddetajim, ParameterDirection.Input); DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_DETAJIMART_merrDetajimArtSipasArtikullitDheDetajimit");
            ImbLogger.LogTraceShitje($"Mbaroi metoda ktheDetajimArtSipasIdArtikulliDheDetajimit sipas idartikulli:{idartikulli}, iddetajim:{iddetajim}");
            if (ds == null)
            {
                ImbLogger.LogTraceShitje($"Metoda ktheDetajimArtSipasIdArtikulliDheDetajimit  sipas idartikulli:{idartikulli}, iddetajim:{iddetajim} ktheu null");
                return null;
            }

            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
            {
                ImbLogger.LogTraceShitje($"Metoda ktheDetajimArtSipasIdArtikulliDheDetajimit  sipas idartikulli:{idartikulli}, iddetajim:{iddetajim} ktheu null");
                return null;
            }
            return ds.Tables[0].Rows[0];

        }
        /// <summary>
        /// kthen datatable detajim artikulli sipas id dhe idndermarje
        /// </summary>
        /// <param name="id"> id e detajimit</param>
        /// <param name="idndermarje"> id e ndermarjes</param>
        /// <returns> nje datatable qe permban nje koleksion me te gjithe detajimet me kete id te kesaj ndermarje</returns>
        [Obsolete("Perdor: internal DataRow merrDetajimArtikulli(int idDetajim)", true)]
        internal DataTable merrDetajimArtikulli(int id, int idndermarje)
        {


            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDDETAJIMARTIKULLI", id, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idndermarje, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_DETAJIMARTIKULLI_ktheDetajimArtikulliSipasID");
            return ds.Tables[0];

        }

        /// <summary>
        /// kthen datatable detajim artikulli me kete kod te kesaj ndermarje
        /// </summary>
        /// <param name="kod"> kodi i detajimit</param>
        /// <param name="idndermarje"> id e ndermarjes</param>
        /// <returns> nje datatable qe permban nje koleksion me te gjitha detajimet e artikullit me kete kod te kesaj ndermarje</returns>
        internal DataRow merrDetajimArtikulli(string kod, int idndermarje)
        {
            ImbLogger.LogTraceShitje($"Filloi metoda merrDetajimArtikulli nga db me kod:{kod} dhe idndermarje:{idndermarje}");
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@KODDETAJIMARTIKULLI", kod, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idndermarje, ParameterDirection.Input);

            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_DETAJIMARTIKULLI_ktheDetajimArtikulliSipasKodit");
            if (ds == null)
            {
                ImbLogger.LogTraceShitje($"Metoda merrDetajimArtikulli nga db me kod:{kod} dhe idndermarje:{idndermarje} ktheu null");
                return null;
            }

            if (ds.Tables[0].Rows.Count == 0)
            {
                ImbLogger.LogTraceShitje($"Metoda merrDetajimArtikulli nga db me kod:{kod} dhe idndermarje:{idndermarje} ktheu null");
                return null;
            }


            ImbLogger.LogTraceShitje($"Mbaroi metoda merrDetajimArtikulli nga db me kod:{kod} dhe idndermarje:{idndermarje}");
            return ds.Tables[0].Rows[0];
        }

        internal int merrIdDetajimArtikulliSipasKodit(string kod, int idndermarje)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@KODDETAJIMARTIKULLI", kod, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idndermarje, ParameterDirection.Input);
            int idDetajim = 0;
            int.TryParse(Convert.ToString(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_DETAJIMARTIKULLI_ktheIdDetajimArtikulliSipasKodit")), out idDetajim);
            return idDetajim;
        }

        /// <summary>
        /// perdoret per te kontrolluar nqs ekziston nje detajim me kete kod ne kete ndermarje
        /// behet kontrolli per qellime konsistence te informacionit ne DB, nuk lejon te celen detajimeve te ndryshem me te njejtin kod
        /// </summary>
        ///<param name="idndermarje"> id e ndermarjes</param>
        ///<param name="kod"> kodi i detajimit</param>
        /// <returns>nje objekt boolean qe tregon nese ekziston apo jo nje detajim me kete kod</returns>
        public bool ekzistonDetajim(String kod, int idndermarje)
        {
            ImbLogger.LogTraceShitje($"Filloi metoda eksitonDetajim ne DB sipas kod:{kod} dhe idndermarje:{idndermarje}");
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@KODDETAJIMARTIKULLI", kod, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idndermarje, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_DETAJIMARTIKULLI_ekzistonDetajimArtikulli");
            ImbLogger.LogTraceShitje($"Filloi metoda eksitonDetajim ne DB sipas kod:{kod} dhe idndermarje:{idndermarje}");
            if (ds.Tables[0].Rows.Count == 1)
            {
                ImbLogger.LogTraceShitje($"Metoda eksitonDetajim ne DB sipas kod:{kod} dhe idndermarje:{idndermarje} ktheu true");
                return true;
            }

            else if (ds.Tables[0].Rows.Count == 0)
            {
                ImbLogger.LogTraceShitje($"Metoda eksitonDetajim ne DB sipas kod:{kod} dhe idndermarje:{idndermarje} ktheu false");
                return false;
            }

            else return true;

        }

        public bool ekzistonDetajimLidhurMeArtikullin(String kodDet, int idndermarje, string kodArt, int lloji)
        {
            ImbLogger.LogTraceShitje($"Filloi metoda ekzistonDetajimLidhurMeArtikullin ne DB sipas kodDet:{kodDet}, idndermarje:{idndermarje},kodArt:{kodArt} dhe lloji:{lloji}");
            dbManager.Open();
            dbManager.CreateParameters(4);
            dbManager.AddParameters(0, "@KODARTIKULL", kodArt, ParameterDirection.Input);
            dbManager.AddParameters(1, "@KODDETAJIMARTIKULLI", kodDet, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDNDERMARJE", idndermarje, ParameterDirection.Input);
            dbManager.AddParameters(3, "@LLOJI", lloji, ParameterDirection.Input);
            int pergjigje = Convert.ToInt32(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_DETAJIMART_ekzistonDetajimLidhurMeArtikullin"));
            ImbLogger.LogTraceShitje($"Mbaroi metoda ekzistonDetajimLidhurMeArtikullin ne DB sipas kodDet:{kodDet}, idndermarje:{idndermarje},kodArt:{kodArt} dhe lloji:{lloji}");
            return Convert.ToBoolean(pergjigje);

        }

        public bool ekzistonDetajimLidhurMeArtikullinSipasId(int idDet, int idndermarje, string kodArt, int lloji)
        {

            dbManager.Open();
            dbManager.CreateParameters(4);
            dbManager.AddParameters(0, "@KODARTIKULL", kodArt, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDDETAJIMARTIKULLI", idDet, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDNDERMARJE", idndermarje, ParameterDirection.Input);
            dbManager.AddParameters(3, "@LLOJI", lloji, ParameterDirection.Input);
            int pergjigje = Convert.ToInt32(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_DETAJIMART_ekzistonDetajimLidhurMeArtikullinSipasId"));
            return Convert.ToBoolean(pergjigje);

        }

        public bool eshteDetajimLidhurMeArtikull(String kodDet, int idndermarje)
        {

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@KODDETAJIMARTIKULLI", kodDet, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idndermarje, ParameterDirection.Input);
            int pergjigje = Convert.ToInt32(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_DETAJIMART_eshteDetajimLidhurMeArtikull"));
            return Convert.ToBoolean(pergjigje);

        }
        /// <summary>
        /// perdoret per te kontrolluar nese ky detajim eshte i lidhur me artikuj.
        /// kjo behet per te mos lejuar te fshihet nje detajimi te lidhur me artikuj.
        /// </summary>
        /// <param name="id"> id e detajimit</param>
        /// <returns> nje objekt boolean qe tregon nese ky detajim eshte i lidhur me artikuj  apo jo</returns>
        public clsMesazh kaVeprimeDetajim(int id)
        {//kontrollon nqs ka veprime

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDDETAJIMARTIKULLI", id, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_DETAJIMARTIKULLI_kaVeprime");
            if (ds.Tables[0].Rows.Count == 1)
                return new clsMesazh(true, "Ekziston artikull per kete detajim!");
            else if (ds.Tables[0].Rows.Count == 0)
                return new clsMesazh(false);
            else return new clsMesazh(true, "Gabim i paparashikuar!");

        }
        public clsMesazh kaVeprimeDetajimRegj(int id, int idartikulli)
        {//kontrollon nqs ka veprime

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDDETAJIMARTIKULLI", id, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDARTIKULLI", idartikulli, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_DETAJIMARTIKULLI_kaVeprimeRegj");
            if (ds.Tables[0].Rows.Count > 0)
                return new clsMesazh(true, "Ka veprime me kete detajim!");
            else if (ds.Tables[0].Rows.Count == 0)
                return new clsMesazh(false, "Nuk ka veprime me kete detajim");
            else return new clsMesazh(true, "Gabim i paparashikuar!");

        }
        public bool kontrolloDetajimLidhur(int id)
        {//kontrollon nqs ka veprime

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDARTIKULLI", id, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_DETAJIMARTIKULLI_kontrolloDetajimLidhur");
            if (ds.Tables[0].Rows.Count > 0)
                return true;
            else if (ds.Tables[0].Rows.Count == 0)
                return false;
            else return true;

        }
        public bool KontrolloDetajimLidhurSipasLlojit(int id, int lloji)
        {//kontrollon nqs ka veprime

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDARTIKULLI", id, ParameterDirection.Input);
            dbManager.AddParameters(1, "@lloji", lloji, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_DETAJIMARTIKULLI_kontrolloDetajimLidhurSipasLlojit");
            if (ds.Tables[0].Rows.Count > 0)
                return true;
            else if (ds.Tables[0].Rows.Count == 0)
                return false;
            else return true;

        }

        public DataTable merrDetajimeFature(int idDok)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@idDok", idDok, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_DETAJIMARTIKULLI_merrDetajimeSipasIdDok");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0)
                return null;
            return ds.Tables[0];
        }



        public DataTable merrDetajime2Fature(int idDok)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@idDok", idDok, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_DETAJIMARTIKULLI_merrDetajime2SipasIdDok");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0)
                return null;
            return ds.Tables[0];
        }

        internal DataTable merrDetajimePerEksport(int idnderm)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_DETAJIMARTIKULLI_merrSipasNdermarrjesPerEksport");
            return ds.Tables[0];
        }

        #endregion

        #region Formula

        internal DataRow merrFormulen(int idFormula)
        {

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDFORMULA", idFormula, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_FORMULA_ktheFormuleSipasId");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];

        }

        /// <summary>
        /// ekzekuton prc_T_DETAJIMARTIKULLI_ins ne DB dhe kthen statusin e perfundimit te ekzekutimit te SP-se perkatese
        /// Shiko clsMesazh brenda ketij moduli per informacion te metejshem mbi vleren e kthimit te kesaj metode
        /// <param name="idDetajimArtikulli"> id ritese e detajimit</param>
        /// <param name="kodDetajimArtikulli">kodi i detajimit</param>
        /// <param name="llojDetajimArtikulli">lloji i detajimit</param>
        /// <param name="pershkrimDetajimArtikulli">peshkrimi i detajimit</param>
        /// <param name="idPerdoruesi"> id e perdoruesit qe e ka kryer veprimin</param>
        /// <param name="idNderViti"> id e ndermarje vitit</param>
        /// <param name="kategoriDetajimi"> kategoria e detajimit</param>
        /// <param name="idndermrje">id e ndermarjes</param>
        /// <returns> nje objekt boolean qe tregon nese ruajtja eshte kryer ne rregull apo jo.</returns>
        /// </summary>
        internal clsMesazh ruajFormule(out int idFormula, string kodFormula, string pershkrimFormula, int idndermarje, int idPerdoruesi, int idstatusdok)
        {//ruajtja e detajimArtikulli
            idFormula = -1;

            dbManager.Open();
            dbManager.CreateParameters(6);
            dbManager.AddParameters(0, "@IDFORMULA", idFormula, ParameterDirection.Output);
            dbManager.AddParameters(1, "@KODFORMULA", kodFormula, ParameterDirection.Input);
            dbManager.AddParameters(2, "@PERSHKRIMFORMULA", pershkrimFormula, ParameterDirection.Input);
            dbManager.AddParameters(3, "@IDNDERMARJE", idndermarje, ParameterDirection.Input);
            dbManager.AddParameters(4, "@IDPERDORUES", idPerdoruesi, ParameterDirection.Input);
            dbManager.AddParameters(5, "@IDSTATUSDOK", idstatusdok, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_FORMULA_ins");
            idFormula = int.Parse(dbManager.Parameters[0].Value.ToString());
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
            return mesazh;
        }

        internal clsMesazh modifikoFormule(int idFormula, string kodFormula, string pershkrimFormula, int idndermarje, int idPerdoruesi, int idstatusdok)
        {//ruajtja e formules

            dbManager.Open();
            dbManager.CreateParameters(6);
            dbManager.AddParameters(0, "@IDFORMULA", idFormula, ParameterDirection.Input);
            dbManager.AddParameters(1, "@KODFORMULA", kodFormula, ParameterDirection.Input);
            dbManager.AddParameters(2, "@PERSHKRIMFORMULA", pershkrimFormula, ParameterDirection.Input);
            dbManager.AddParameters(3, "@IDNDERMARJE", idndermarje, ParameterDirection.Input);
            dbManager.AddParameters(4, "@IDPERDORUES", idPerdoruesi, ParameterDirection.Input);
            dbManager.AddParameters(5, "@IDSTATUSDOK", idstatusdok, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_FORMULA_upd");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
            return mesazh;

        }

        internal clsMesazh fshiFormule(int idFormula)
        {//ruajtja e formules

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDFORMULA", idFormula, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_FORMULA_updDel");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
            return mesazh;

        }

        internal DataTable ktheFormulatENdermarrjes(int idNdermarrje)
        {

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDNDERMARJE", idNdermarrje, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_FORMULA_ktheFormulatSipasNdermarrjes");
            return ds.Tables[0];

        }

        internal bool ekzistonFormuleSipasKodit(string kodFormula, int idNdermarrje)
        {

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@KODFORMULA", kodFormula, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idNdermarrje, ParameterDirection.Input);
            int pergjigje = Convert.ToInt32(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_FORMULA_ekzistonFormuleSipasKodit"));
            return Convert.ToBoolean(pergjigje);

        }

        internal bool ekzistonFormuleSipasPershkrimit(string pershkrimFormula, int idNdermarrje)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@PERSHKRIMFORMULA", pershkrimFormula, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idNdermarrje, ParameterDirection.Input);
            int pergjigje = Convert.ToInt32(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_FORMULA_ekzistonFormuleSipasPershkrimit"));
            return Convert.ToBoolean(pergjigje);

        }

        #endregion

        /// <summary>
        /// Metodat e meposhtme i perkasin veprimeve qe kryhen nga objektet clsDetajimArt dhe colDetajimeArt qe mbajne lidhjet e artikullit me detajimet
        /// </summary>
        #region DETAJIM ART

        //tabela qe lidh detajimet me artikujt
        /// <summary>
        /// ekzekuton prc_T_DETAJIMART_ins ne DB dhe kthen statusin e perfundimit te ekzekutimit te SP-se perkatese
        /// Shiko clsMesazh brenda ketij moduli per informacion te metejshem mbi vleren e kthimit te kesaj metode
        /// </summary>
        /// <param name="idDetajimArt"> id ritese e detajimit</param>
        /// <param name="idArtikulli"> id e artikullit</param>
        /// <param name="idDetajimArtikulli"> id e detajimit te artikullit</param>
        /// <param name="lloji"> lloji i detajimit</param>
        /// <returns> nje objekt clsMesazh qe tregon nese ruajtja eshte kryer ne rregull apo jo.</returns>

        internal clsMesazh ruajDetajimArt(int idArtikulli, int idDetajimArtikulli, int lloji)
        { //metoda per ruajtjen e DetajimArt
            ImbLogger.LogWarningShitje("Filloi metoda per ruajtjen");
            int idDetajimArt = 0;
            dbManager.Open();
            dbManager.CreateParameters(4);
            dbManager.AddParameters(0, "@IDDETAJIMART", idDetajimArt, ParameterDirection.Output);
            dbManager.AddParameters(1, "@IDARTIKULLI", idArtikulli, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDDETAJIMARTIKULLI", idDetajimArtikulli, ParameterDirection.Input);
            dbManager.AddParameters(3, "@LLOJDETAJIM", lloji, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_DETAJIMART_ins");
            ImbLogger.LogWarningShitje("Mbaroi metoda per ruajtjen");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
            return mesazh;
        }


        /// <summary>
        /// ekzekuton prc_T_DETAJIMART_upd ne DB dhe kthen statusin e perfundimit te ekzekutimit te SP-se perkatese
        /// Shiko clsMesazh brenda ketij moduli per informacion te metejshem mbi vleren e kthimit te kesaj metode
        /// </summary>
        /// 
        /// <param name="idDetajimArt"> id ritese e detajimit</param>
        /// <param name="idArtikulli"> id e artikullit</param>
        /// <param name="idDetajimArtikulli"> id e detajimit te artikullit</param>
        /// <param name="lloji">lloji i detajimit</param>
        /// <returns> nje objekt clsMesazh qe tregon nese modifikimi eshte kryer ne rregull apo jo.</returns>
        internal clsMesazh modifikoDetajimArt(int idDetajimArt, int idArtikulli, int idDetajimArtikulli, int lloji)
        {//metoda per modifikimin e DetajimArt


            dbManager.Open();
            //shtimi i parametrave
            dbManager.CreateParameters(4);
            dbManager.AddParameters(0, "@IDDETAJIMART", idDetajimArt, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDARTIKULLI", idArtikulli, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDDETAJIMARTIKULLI", idDetajimArtikulli, ParameterDirection.Input);
            dbManager.AddParameters(3, "@LLOJDETAJIM", lloji, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_DETAJIMART_upd");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgModifikimiMeSukses"]);
            return mesazh;

        }
        //[Obsolete("Perdor: clsMesazh modifikoDetajimArt(int idDetajimArt, int idArtikulli, int idDetajimArtikulli)", true)]
        //public clsMesazh modifikoDetajimArt(clsDetajimPerArt detajimArt)
        //{//metoda per modifikimin e DetajimArt
        //    try
        //    {
        //        //shtimi i parametrave
        //        dbManager.CreateParameters(3);
        //        dbManager.AddParameters(0, "@IDDETAJIMART", detajimArt.IdDetajimArt, ParameterDirection.Input);
        //        dbManager.AddParameters(1, "@IDARTIKULLI", detajimArt.IdArtikulli, ParameterDirection.Input);
        //        dbManager.AddParameters(2, "@IDDETAJIMARTIKULLI", detajimArt.IdDetajimArtikulli, ParameterDirection.Input);
        //        dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_DETAJIMART_upd");
        //        clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgModifikimiMeSukses"]);
        //        return mesazh;
        //    }
        //    catch (Exception)
        //    {
        //        return new clsMesazh(false, ce.Message);
        //    }
        //}

        /// <summary>
        /// ekzekutohet sp-ja prc_T_DETAJIMART_del duke i kaluar id e detajim artikullit qe e marrim nga objekti clsDetajimArt qe i kalohet si parameter
        /// </summary>
        /// <param name="idDetajimArt"> detajim artikullit qe do fshihet </param>
        /// <returns>nje objekt clsMesazh qe tregon nese fshirja eshte kryer ne rregull apo jo</returns>
        internal clsMesazh fshiDetajimArt(int idDetajimArt)
        {//metoda per fshirjen e DetajimArt

            ImbLogger.LogTraceShitje("Filloi metoda fshiDetajiArt");
            dbManager.Open();
            //shtimi i parametrave
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDDETAJIMART", idDetajimArt, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_DETAJIMART_del");
            ImbLogger.LogTraceShitje("Mbaroi metoda fshiDetajiArt");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
            return mesazh;


        }
        //[Obsolete("Perdor: clsMesazh fshiDetajimArt(int idDetajimArt)", true)]
        //public clsMesazh fshiDetajimArt(clsDetajimPerArt detajimArt)
        //{//metoda per fshirjen e DetajimArt
        //    try
        //    {
        //        //shtimi i parametrave
        //        dbManager.CreateParameters(1);
        //        dbManager.AddParameters(0, "@IDDETAJIMART", detajimArt.IdDetajimArt, ParameterDirection.Input);
        //        dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_DETAJIMART_del");
        //        clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
        //        return mesazh;
        //    }
        //    catch (Exception)
        //    {
        //        return new clsMesazh(false, ce.Message);
        //    }

        //}

        /// <summary>
        /// kthen datatable detajim artikulli sipas id se artikullit
        /// </summary>
        /// <param name="idartikulli"> id e artikullit</param>
        /// <returns> nje datatable qe permban nje koleksion me te gjitha detajim artikulli te ketij artikulli</returns>
        internal DataTable ktheDetajimArtSipasIdArtikulli(int idartikulli)
        {


            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDARTIKULLI", idartikulli, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_DETAJIMART_merrDetajimArtSipasArtikullit");

            return ds.Tables[0];

        }

        /// <summary>
        /// kthen datatable detajim artikulli sipas id se artikullit DHE LLOJIT
        /// </summary>
        /// <param name="idartikulli"> id e artikullit</param>
        /// <param name="lloji">lloji i detajimit i pare apo i dyte</param>
        /// <returns> nje datatable qe permban nje koleksion me te gjitha detajim artikulli te ketij artikulli SIPAS LLOJIT</returns>
        internal DataTable ktheDetajimArtSipasIdArtikulliDheLlojit(int idartikulli, int lloji)
        {

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDARTIKULLI", idartikulli, ParameterDirection.Input);
            dbManager.AddParameters(1, "@LLOJDETAJIM", lloji, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_DETAJIMART_merrDetajimArtSipasArtikullitDheLlojit");

            return ds.Tables[0];

        }
        //[Obsolete("Perdor: DataTable ktheDetajimArtSipasIdArtikulli(int idartikulli)", true)]
        //public colDetajimePerArt  merrDetajimArtSipasIdArtikulli(int idartikulli)
        //{//metoda per te marre DetajimArt sipas id artikulli
        //    IDBManager dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        //    dbManager.ConnectionString = dbManager.GetConnectionString();
        //    try
        //    {
        //        dbManager.Open();
        //        dbManager.CreateParameters(1);
        //        dbManager.AddParameters(0, "@IDARTIKULLI", idartikulli, ParameterDirection.Input);
        //        DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_DETAJIMART_merrDetajimArtSipasArtikullit");
        //        colDetajimePerArt colDetajimeArt = new colDetajimePerArt();
        //        return colDetajimeArt.mbushArrayListDetajimeshArt(ds);
        //    }
        //    catch (Exception)
        //    {
        //        return new colDetajimePerArt();
        //    }
        //    finally
        //    {
        //        dbManager.Dispose();
        //    }
        //}

        #endregion

        /// <summary>
        /// Metodat e meposhtme i perkasin veprimeve qe kryhen nga objektet colKategoriDetajimArtikulli
        /// </summary>
        #region KATEGORI DETAJIMI

        /// <summary>
        /// kthen datatable kategori detajimi 
        /// </summary>
        /// <returns> nje datatable qe permban nje koleksion me te gjithe kategorite e detajimit</returns>
        internal DataTable ktheGjitheKategoriteDetajimit()
        {


            dbManager.Open();
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KATEGORIDETAJIMARTIKULLI_merrGjitheKategoriteDetajimit");

            return ds.Tables[0];

        }
        //[Obsolete("Perdor: DataTable ktheGjitheKategoriteDetajimit()", false)]
        //public colKategoriDetajimArtikulli merrGjitheKategoriteDetajimit()
        //{//metoda per te marre te gjithe kategorite e detajimeve
        //    IDBManager dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        //    dbManager.ConnectionString = dbManager.GetConnectionString();
        //    try
        //    {
        //        dbManager.Open();
        //        DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KATEGORIDETAJIMARTIKULLI_merrGjitheKategoriteDetajimit");
        //        colKategoriDetajimArtikulli col = new colKategoriDetajimArtikulli();
        //        return col.mbushArrayListkategoriDetajimesh(ds);
        //    }
        //    catch (Exception)
        //    {
        //        return new colKategoriDetajimArtikulli();
        //    }
        //    finally
        //    {
        //        dbManager.Dispose();
        //    }

        //}

        /// <summary>
        /// kthen objektet kategori detajimi sipas id
        /// </summary>
        /// <param name="idkategori"> id e kategorise</param>
        /// <returns> nje objekt colKategoriDetajimiArtikulli qe permban nje koleksion me te gjithe kategorite e detajimit  me kete id</returns>
        internal DataRow merrKategoriDetajimiSipasID(int idkategori)
        {

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDKATEGORI", idkategori, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KATEGORIDETAJIMARTIKULLI_ktheKategoriDetajimiSipasID");

            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];


        }
        internal DataRow merrKategoriDetajimiSipasKodit(string kodi)
        {


            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@KODI", kodi, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KATEGORIDETAJIMARTIKULLI_ktheKategoriDetajimiSipasKODIT");

            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];


        }
        //[Obsolete("Perdor: DataRow merrKategoriDetajimiSipasID(int idkategori)", false)]
        //public colKategoriDetajimArtikulli ktheKategoriDetajimiSipasID(int idkategori)
        //{
        //    IDBManager dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        //    dbManager.ConnectionString = dbManager.GetConnectionString();
        //    try
        //    {
        //        dbManager.Open();
        //        dbManager.CreateParameters(1);
        //        dbManager.AddParameters(0, "@IDKATEGORI", idkategori, ParameterDirection.Input);
        //        DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KATEGORIDETAJIMARTIKULLI_ktheKategoriDetajimiSipasID");
        //        colKategoriDetajimArtikulli kategori = new colKategoriDetajimArtikulli();
        //        return kategori.mbushArrayListkategoriDetajimesh(ds);

        //    }
        //    catch (Exception)
        //    {
        //        return new colKategoriDetajimArtikulli();
        //    }
        //    finally
        //    {
        //        dbManager.Dispose();
        //    }

        //}

        #endregion

        /// <summary>
        /// Metodat e meposhtme i perkasin veprimeve qe kryhen nga objektet clsNjesiVartese dhe colNjesiVartese
        /// </summary>
        #region NJESI VARTESE

        /// <summary>
        /// ekzekuton prc_T_NJESIVARTESE_ins ne DB dhe kthen statusin e perfundimit te ekzekutimit te SP-se perkatese
        /// Shiko clsMesazh brenda ketij moduli per informacion te metejshem mbi vleren e kthimit te kesaj metode
        /// <param name="adr"> adresa e njesise </param>
        /// <param name="idllog">id inventarizimi</param>
        /// <param name="idNjes"> id ritese e njesise </param>
        /// <param name="idPerd">id e perdoruesit qe e ka ruajtur</param>
        /// <param name="kod">kodi i njesise </param>
        /// <param name="nderm">id e ndermarjes</param>
        /// <param name="pershk"> pershkrimi</param>
        /// <returns> nje objekt clsMesazh qe tregon nese ruajtja eshte kryer ne rregull apo jo.</returns>
        /// </summary>
        internal clsMesazh ruajNjesiVartese(out int idNjes, string kod, string pershk, string adr, int idllog, int nderm, int idPerd, int idkonf, int idstatusdok)
        {
            idNjes = -1;

            dbManager.Open();
            dbManager.CreateParameters(9);
            dbManager.AddParameters(0, "@IDNJESIVARTESE", idNjes, ParameterDirection.Output);
            dbManager.AddParameters(1, "@KODI", kod, ParameterDirection.Input);
            dbManager.AddParameters(2, "@PERSHKRIMI", pershk, ParameterDirection.Input);
            dbManager.AddParameters(3, "@ADRESA", adr, ParameterDirection.Input);
            dbManager.AddParameters(4, "@IDLLOGARI", idllog, ParameterDirection.Input);
            dbManager.AddParameters(5, "@IDNDERMARJE", nderm, ParameterDirection.Input);
            dbManager.AddParameters(6, "@IDPERDORUESI", idPerd, ParameterDirection.Input);
            dbManager.AddParameters(7, "@IDKONFIG", idkonf, ParameterDirection.Input);
            dbManager.AddParameters(8, "@IDSTATUSDOK", idstatusdok, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_NJESIVARTESE_ins");
            idNjes = int.Parse(dbManager.Parameters[0].Value.ToString());
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
            return mesazh;

        }

        /// <summary>
        /// perdoret per te kontrolluar nqs ekziston nje njesi  me nje kod te marre si parameter
        /// behet kontrolli per qellime konsistence te informacionit ne DB, nuk lejon te celen njesi   te ndryshem me te njejtin kod ne nje ndermarje
        /// </summary>
        /// <param name="kod">kodi i njesise </param>
        /// <param name="nderm">id e ndermarjes</param>
        /// <returns>nje objeckt boolean qe tregon nese ekziston apo jo nje njesi  me kete kod</returns>
        public bool ekzistonKodNjesiVartese(string kod, int nderm)
        {

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@KODI", kod, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", nderm, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_NJESIVARTESE_ekzistonKod");
            if (ds.Tables[0].Rows.Count == 1)
                return true;
            else if (ds.Tables[0].Rows.Count == 0)
                return false;
            else return true;

        }

        /// <summary>
        /// ekzekuton prc_T_NJESIVARTESE_upd ne DB dhe kthen statusin e perfundimit te ekzekutimit te SP-se perkatese
        /// Shiko clsMesazh brenda ketij moduli per informacion te metejshem mbi vleren e kthimit te kesaj metode
        /// <param name="adr"> adresa e njesise </param>
        /// <param name="idllog">id llogarise</param>
        /// <param name="idNjes"> id ritese e njesise </param>
        /// <param name="idPerd">id e perdoruesit qe e ka ruajtur</param>
        /// <param name="kod">kodi i njesise administrative</param>
        /// <param name="nderm">id e ndermarjes</param>
        /// <param name="pershk"> pershkrimi</param>
        /// <returns> nje objekt clsMesazh qe tregon nese modifikimi eshte kryer ne rregull apo jo.</returns>
        /// </summary>
        internal clsMesazh modifikoNjesiVartese(int idNjes, string kod, string pershk, string adr, int idllog, int nderm, int idPerd, int idkonf, int idstatusdok)
        {

            dbManager.Open();
            dbManager.CreateParameters(9);
            dbManager.AddParameters(0, "@IDNJESIVARTESE", idNjes, ParameterDirection.Input);
            dbManager.AddParameters(1, "@KODI", kod, ParameterDirection.Input);
            dbManager.AddParameters(2, "@PERSHKRIMI", pershk, ParameterDirection.Input);
            dbManager.AddParameters(3, "@ADRESA", adr, ParameterDirection.Input);
            dbManager.AddParameters(4, "@IDLLOGARI", idllog, ParameterDirection.Input);
            dbManager.AddParameters(5, "@IDNDERMARJE", nderm, ParameterDirection.Input);
            dbManager.AddParameters(6, "@IDPERDORUESI", idPerd, ParameterDirection.Input);
            dbManager.AddParameters(7, "@IDKONFIG", idkonf, ParameterDirection.Input);
            dbManager.AddParameters(8, "@IDSTATUSDOK", idstatusdok, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_NJESIVARTESE_upd");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgModifikimiMeSukses"]);
            return mesazh;

        }

        /// <summary>
        /// ekzekutohet sp-ja prc_T_NJESIVARTESE_del duke i kaluar id e njesise  qe e marrim nga objekti clsNjesiVartese qe i kalohet si parameter
        /// </summary>
        /// <param name="idNjes"> id ritese e njesise </param>
        /// <returns>nje objekt clsMesazh qe tregon nese fshirja eshte kryer ne rregull apo jo</returns>
        internal clsMesazh fshiNjesiVartese(int idNjes)
        {

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDNJESIVARTESE", idNjes, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_NJESIVARTESE_del");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
            return mesazh;

        }

        internal clsMesazh fshiNjesiVarteseStatus(int idNjes, int idperdoruesi)
        {

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDNJESIVARTESE", idNjes, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUESI", idperdoruesi, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_NJESIVARTESE_upddel");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
            return mesazh;

        }
        /// <summary>
        /// kthen objektet datatable njesi  sipas ndermarjes 
        /// </summary>
        ///<param name="idNderm">id e ndermarjes</param>
        ///<returns>nje objekt datatable qe permban nje koleksion me te gjitha njesite    te kesaj ndermarje   </returns>
        internal DataTable ktheGjitheNjesiVartese(int idNderm)
        {

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDNDERMARJE", idNderm, ParameterDirection.Input);

            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_NJESIVARTESE_merrGjitheNjesite");
            return ds.Tables[0];

        }

        /// <summary>
        /// kthen datarow njesi administartive sipas ndermarjes dhe kodit
        /// </summary>
        /// <param name="kodi"> kodi i njesise </param>
        /// <param name="idNderm"> id e ndermarrjes</param>
        /// <returns>nje objekt datarow qe permban  njesine    me kete kod te kesaj ndermarje   </returns>
        internal DataRow ktheNjesiVarteseSipasKodit(string kodi, int idNderm)
        {

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@KODI", kodi, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idNderm, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_NJESIVARTESE_ktheNjesi");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];

        }

        /// <summary>
        /// kthen string kodin e njesise  sipas idse
        /// </summary>
        ///<param name="idNjesiVartese"> id e njesise </param>
        ///<returns>nje string kodin e njesise  me kete id</returns>
        internal string ktheKodiNjesiVarteseSipasiD(int idNjesiVartese)
        {

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDNJESIVARTESE", idNjesiVartese, ParameterDirection.Input);

            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_NJESIVARTESE_merrNjesiSipasId");

            if (ds == null)
                return "";
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return "";
            string kodiNjesiVartese;
            kodiNjesiVartese = ds.Tables[0].Rows[0]["KODI"].ToString() + " (" + ds.Tables[0].Rows[0]["PERSHKRIMI"].ToString();
            return kodiNjesiVartese;

        }
        /// <summary>
        /// kthen objekt datarow njesi  sipas idse
        /// </summary>
        ///<param name="idnjesivartese"> id e njesise </param>
        ///<returns>nje objekt datarow qe permban  njesine    me kete id  </returns>
        internal DataRow ktheNjesiVarteseSipasiD(int idnjesivartese)
        {

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDNJESIVARTESE", idnjesivartese, ParameterDirection.Input);

            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_NJESIVARTESE_merrNjesiSipasId");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];

        }

        /// <summary>
        /// perdoret per te kontrolluar nese ka veprime me kete njesi administrative
        /// </summary>
        ///<param name="id"> id e njesise administrative</param>
        ///<param name="idnderm"> id e ndermarjes</param>
        /// <returns>nje objeckt boolean qe tregon nese  ka veprime me kete njesi administrative apo jo</returns>
        public bool kaVeprimeNjesiVartese(int id, int idnderm)
        {

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDNJESIVARTESE", id, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_NJESIVARTESE_kaVeprime");
            if (ds.Tables[0].Rows.Count == 1)
                return true;
            else if (ds.Tables[0].Rows.Count == 0)
                return false;
            else return true;

        }
        internal DataRow merrSipasNjesiNdermarrjesDR(int iddege)
        {


            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDNJESIVARTESE", iddege, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_NJESIVARTESE_merrSipasNjesiNdermarrjesDR");

            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];

        }
        internal DataTable merrSipasNjesiNdermarrjesDT(int idnderm)
        {


            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_NJESIVARTESE_merrSipasNjesiNdermarrjesDT");

            return ds.Tables[0];

        }

        internal DataTable merrSipasNjesiNdermarrjesDTMeFilter(string filter, long startIndex, long endIndex, int idnderm)
        {
            dbManager.Open();
            dbManager.CreateParameters(4);
            dbManager.AddParameters(0, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            dbManager.AddParameters(1, "@FILTER", filter, ParameterDirection.Input);
            dbManager.AddParameters(2, "@startIndex", startIndex, ParameterDirection.Input);
            dbManager.AddParameters(3, "@endIndex", endIndex, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_NJESIVARTESE_merrSipasNjesiNdermarrjesDTMeFilter");

            return ds.Tables[0];

        }

        internal DataTable merrSipasNjesiNdermarrjesDTMeID(int idnderm, int value)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNJESIVARTESE", value, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_NJESIVARTESE_merrSipasNjesiNdermarrjesDTMeID");

            return ds.Tables[0];

        }

        #endregion


        /// <summary>
        /// Metodat e meposhtme i perkasin veprimeve qe kryhen nga objektet clsGarancia
        /// </summary>
        #region GARANCIA

        internal DataTable merrGarrancite()
        {

            dbManager.Open();
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_GARANCIA_ktheGaranciAll");

            if (ds == null)
                return null;
            return ds.Tables[0];

        }

        internal DataRow merrGaranciSipasId(int idGarncia)
        {

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDLLOJGARANCI", idGarncia, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_GARANCIA_ktheGaranciipasId");

            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];

        }

        internal DataRow merrGaranciSipasKod(string kodGarncia)
        {

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@KODLLOJGARANCI", kodGarncia, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_GARANCIA_ktheGaranciSipasKodit");

            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];

        }

        #endregion


        /// <summary>
        /// Metodat e meposhtme i perkasin veprimeve qe kryhen nga objektet clsLlojDifekti dhe colLlojDifekti
        /// </summary>
        #region LLOJ DIFEKTI



        internal clsMesazh ruajLlojDifekti(out int id, string kodi, String pershkrimi, int idPerdoruesi, int idkrijuesi, int idnderm, int idstatudsok)
        {
            id = -1;

            dbManager.Open();
            clsMesazh mesazh = new clsMesazh();
            dbManager.CreateParameters(7);
            dbManager.AddParameters(0, "@ID", id, ParameterDirection.Output);
            dbManager.AddParameters(1, "@KODi", kodi, ParameterDirection.Input);
            dbManager.AddParameters(2, "@PERSHKRIMi", pershkrimi, ParameterDirection.Input);
            dbManager.AddParameters(3, "@IDPERDORUESI", idPerdoruesi, ParameterDirection.Input);
            dbManager.AddParameters(4, "@IDKRIJUESI", idkrijuesi, ParameterDirection.Input);
            dbManager.AddParameters(5, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            dbManager.AddParameters(6, "@IDSTATUSDOK", idstatudsok, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_LLOJDIFEKTI_ins");
            mesazh = new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);

            return mesazh;

        }

        /// <summary>
        /// modifikon llojin e difektit
        /// </summary>
        /// <param name="id"></param>
        /// <param name="kodi"></param>
        /// <param name="pershkrimi"></param>
        /// <param name="idPerdoruesi"></param>
        /// <param name="idnderm"></param>
        /// <param name="idstatudsok"></param>
        /// <returns></returns>
        internal clsMesazh modifikoLlojDifekti(int id, string kodi, String pershkrimi, int idPerdoruesi, int idnderm, int idstatudsok)
        {//metoda per modifikimin e njesise se artikullit

            dbManager.Open();
            clsMesazh mesazh = new clsMesazh();
            dbManager.CreateParameters(6);
            dbManager.AddParameters(0, "@ID", id, ParameterDirection.Input);
            dbManager.AddParameters(1, "@KODi", kodi, ParameterDirection.Input);
            dbManager.AddParameters(2, "@PERSHKRIMi", pershkrimi, ParameterDirection.Input);
            dbManager.AddParameters(3, "@IDPERDORUESI", idPerdoruesi, ParameterDirection.Input);
            dbManager.AddParameters(4, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            dbManager.AddParameters(5, "@IDSTATUSDOK", idstatudsok, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_LLOJDIFEKTI_upd");
            mesazh = new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);

            return mesazh;

        }

        /// <summary>
        /// fshin llojin e difektit
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        internal clsMesazh fshiLlojDifekti(int id)
        {//metoda per fshirjen e njesise se Artikullit

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@ID", id, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_LLOJDIFEKTI_del");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
            return mesazh;

        }
        /// <summary>
        /// fshin llojin e difektit duke i ndryshuar statusin
        /// </summary>
        /// <param name="id"></param>
        /// <param name="idperdoruesi"></param>
        /// <returns></returns>
        internal clsMesazh fshiLlojDifektiStatus(int id, int idperdoruesi)
        {//metoda per fshirjen e njesise se Artikullit

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@ID", id, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUESI", idperdoruesi, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_LLOJDIFEKTI_upddel");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
            return mesazh;

        }


        /// <summary>
        /// kthen gjithe llojet e difektit te ndermarjes
        /// </summary>
        /// <param name="idnder"></param>
        /// <returns></returns>
        internal DataTable ktheGjithellojdifektiSipasNdermarrjes(int idnder)
        {

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDNDERMARJE", idnder, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_LLOJDIFEKTI_merrSipasNdermarrjes");

            return ds.Tables[0];

        }

        /// <summary>
        /// mer llojdifekti sipas id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        internal DataRow merrLlojDifekti(int id)
        {

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@ID", id, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_LLOJDIFEKTI_ktheLlojDifektiSipasId");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];

        }
        /// <summary>
        /// merr lloj difekti sipas kodit dhe ndermarjes
        /// </summary>
        /// <param name="kod"></param>
        /// <param name="idNderm"></param>
        /// <returns></returns>
        internal DataRow merrLlojDifektiMeKod(string kod, int idNderm)
        {

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@KODi", kod, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idNderm, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_LLOJDIFEKTI_ktheLlojDifektiSipasKodit");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];

        }
        /// <summary>
        /// merr idllojdifekti sipas pershkrimit
        /// </summary>
        /// <param name="pershkrim"></param>
        /// <param name="idnderm"></param>
        /// <returns></returns>
        internal int ktheIdLlojDifektiSipasPershkrimi(string pershkrim, int idnderm)
        {

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@PERSHKRIMi", pershkrim, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_LLOJDIFEKTI_ktheLlojDifektiSipasPershkrimit");

            if (ds == null)
                return -1;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return -1;
            int id;
            int.TryParse(ds.Tables[0].Rows[0]["ID"].ToString(), out id);
            return id;

        }
        /// <summary>
        /// merr lloj difekti sipas pershkrimit dhe ndermarjes
        /// </summary>
        /// <param name="pershkrimi"></param>
        /// <param name="idnderm"></param>
        /// <returns></returns>
        internal DataRow merrLlojDifektiMePershk(string pershkrimi, int idnderm)
        {

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@PERSHKRIMi", pershkrimi, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_LLOJDIFEKTI_ktheLlojDifektiSipasPershkrimit");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];

        }
        /// <summary>
        /// kontrollon nqs ekziston nje lloj difekti me kete kod per kete ndermarje
        /// </summary>
        /// <param name="kod"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool ekzistonLlojDifekti(String kod, int id)
        {//kontrollon nqs ekziston nje Njesi artikulli me kete kod

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@KODi", kod, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", id, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_LLOJDIFEKTI_ekzistonLlojDifekti");
            if (ds.Tables[0].Rows.Count == 1)
                return true;
            else if (ds.Tables[0].Rows.Count == 0)
                return false;
            else return true;

        }
        public bool eshteTransferuarTekBijLlojDifekti(string kodi, int idndermarje)
        {

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@KODI", kodi, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idndermarje, ParameterDirection.Input);
            int pergjigje = Convert.ToInt32(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_LLOJDIFEKTI_eshteTransferuarTekBij"));
            return Convert.ToBoolean(pergjigje);

        }

        /// <summary>
        /// perdoret per te kontrolluar nese kjo njesi artikulli eshte e lidhur me artikuj.
        /// kjo behet per te mos lejuar te fshihet nje njesi artikulli te lidhur me nje artikull
        /// </summary>
        /// <param name="id"> id e njesise se artikullit</param>
        /// <returns> nje objekt boolean qe tregon nese kjo njesi eshte e lidhur me nje artikull apo jo</returns>
        public bool kaVeprimeLlojDifekti(int id)
        {//kontrollon nqs ka veprime me kete njesi artikulli

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@ID", id, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_LLOJDIFEKTI_kaVeprime");
            if (ds.Tables[0].Rows.Count == 1)
                return true;
            else if (ds.Tables[0].Rows.Count == 0)
                return false;
            else return true;

        }
        internal clsMesazh ruajLlojDifektiNeNdermarjeTeRe(int idndermarje, int idndermarjenga)
        {//metoda per fshirjen e njesise se Artikullit
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDNDERMARJE", idndermarje, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJENGA", idndermarjenga, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_LLOJDIFEKTI_kopjoNeNdermarjeRe");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
            return mesazh;

        }

        #endregion

        /// <summary>
        /// Metodat e meposhtme i perkasin veprimeve qe kryhen nga objektet clsStatusRiparimi dhe colStatusRiparimi
        /// </summary>
        #region STATUS RIPARIMI



        internal clsMesazh ruajStatusRiparimi(out int id, string kodi, String pershkrimi, int idPerdoruesi, int idkrijuesi, int idnderm, int idstatudsok)
        {
            id = -1;

            dbManager.Open();
            clsMesazh mesazh = new clsMesazh();
            dbManager.CreateParameters(7);
            dbManager.AddParameters(0, "@ID", id, ParameterDirection.Output);
            dbManager.AddParameters(1, "@KODi", kodi, ParameterDirection.Input);
            dbManager.AddParameters(2, "@PERSHKRIMi", pershkrimi, ParameterDirection.Input);
            dbManager.AddParameters(3, "@IDPERDORUESI", idPerdoruesi, ParameterDirection.Input);
            dbManager.AddParameters(4, "@IDKRIJUESI", idkrijuesi, ParameterDirection.Input);
            dbManager.AddParameters(5, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            dbManager.AddParameters(6, "@IDSTATUSDOK", idstatudsok, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_STATUSRIPARIMi_ins");
            id = int.Parse(dbManager.Parameters[0].Value.ToString());
            mesazh = new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);

            return mesazh;

        }

        /// <summary>
        /// modifikon statusin e riparimit
        /// </summary>
        /// <param name="id"></param>
        /// <param name="kodi"></param>
        /// <param name="pershkrimi"></param>
        /// <param name="idPerdoruesi"></param>
        /// <param name="idnderm"></param>
        /// <param name="idstatudsok"></param>
        /// <returns></returns>
        internal clsMesazh modifikoStatusRiparimi(int id, string kodi, String pershkrimi, int idPerdoruesi, int idnderm, int idstatudsok)
        {//metoda per modifikimin e njesise se artikullit

            dbManager.Open();
            clsMesazh mesazh = new clsMesazh();
            dbManager.CreateParameters(6);
            dbManager.AddParameters(0, "@ID", id, ParameterDirection.Input);
            dbManager.AddParameters(1, "@KODi", kodi, ParameterDirection.Input);
            dbManager.AddParameters(2, "@PERSHKRIMi", pershkrimi, ParameterDirection.Input);
            dbManager.AddParameters(3, "@IDPERDORUESI", idPerdoruesi, ParameterDirection.Input);
            dbManager.AddParameters(4, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            dbManager.AddParameters(5, "@IDSTATUSDOK", idstatudsok, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_STATUSRIPARIMi_upd");
            mesazh = new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);

            return mesazh;

        }

        /// <summary>
        /// fshin statusin e riparimit
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        internal clsMesazh fshiStatusRiparimi(int id)
        {//metoda per fshirjen e njesise se Artikullit

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@ID", id, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_STATUSRIPARIMi_del");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
            return mesazh;

        }
        /// <summary>
        /// fshin statusin e riparimit duke i ndryshuar statusin
        /// </summary>
        /// <param name="id"></param>
        /// <param name="idperdoruesi"></param>
        /// <returns></returns>
        internal clsMesazh fshiStatusRiparimiStatus(int id, int idperdoruesi)
        {//metoda per fshirjen e njesise se Artikullit

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@ID", id, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUESi", idperdoruesi, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_STATUSRIPARIMi_upddel");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
            return mesazh;

        }
        internal clsMesazh ruajStatusRiparimiNeNdermarjeTeRe(int idndermarje, int idndermarjenga)
        {//metoda per fshirjen e njesise se Artikullit

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDNDERMARJE", idndermarje, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJENGA", idndermarjenga, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_STATUSRIPARIMI_kopjoNeNdermarjeRe");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
            return mesazh;

        }


        /// <summary>
        /// kthen gjithe status riparimi te ndermarjes sipas autorizimeve
        /// </summary>
        /// <param name="idnder"></param>
        /// <returns></returns>
        internal DataTable ktheGjitheStatusRiparimeSipasNdermarrjes(int idnder, int idperdoruesi)
        {

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDNDERMARJE", idnder, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUES", idperdoruesi, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_STATUSRIPARIMi_merrSipasNdermarrjes");

            return ds.Tables[0];

        }
        internal DataTable ktheGjitheStatusRiparimeSipasNdermarrjesPaAutorizime(int idnder)
        {

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDNDERMARJE", idnder, ParameterDirection.Input);

            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_STATUSRIPARIMI_merrSipasNdermarrjesPaAutorizime");

            return ds.Tables[0];

        }
        /// <summary>
        /// mer status riparimi  sipas id dhe autorizimeve
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        internal DataRow merrStatusRiparime(int id, int idperdoreusi)
        {

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@ID", id, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUES", idperdoreusi, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_STATUSRIPARIMI_ktheStatusRiparimiSipasId");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];

        }
        internal DataRow merrStatusRiparimePaAutorizime(int id)
        {

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@ID", id, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_STATUSRIPARIMI_ktheStatusRiparimiSipasIdPaautorizime");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];

        }
        /// <summary>
        /// merr status riparimi sipas kodit dhe ndermarjes dhe autorizimeve
        /// </summary>
        /// <param name="kod"></param>
        /// <param name="idNderm"></param>
        /// <returns></returns>
        internal DataRow merrStatusRiparimiMeKod(string kod, int idNderm, int idperdoreusi)
        {

            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@KODi", kod, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idNderm, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDPERDORUES", idperdoreusi, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_STATUSRIPARIMI_ktheStatusRiparimiSipasKodit");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];

        }
        /// <summary>
        /// merr idstatusriparimi sipas pershkrimit
        /// </summary>
        /// <param name="pershkrim"></param>
        /// <param name="idnderm"></param>
        /// <returns></returns>
        internal int ktheIdStatusRiparimiSipasPershkrimi(string pershkrim, int idnderm, int idperdoruesi)
        {

            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@PERSHKRIMi", pershkrim, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDPERDORUES", idperdoruesi, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_STATUSRIPARIMI_ktheStatusRiparimiSipasPershkrimit");

            if (ds == null)
                return -1;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return -1;
            int id;
            int.TryParse(ds.Tables[0].Rows[0]["ID"].ToString(), out id);
            return id;

        }
        /// <summary>
        /// merr status riparimi sipas pershkrimit dhe ndermarjes
        /// </summary>
        /// <param name="pershkrimi"></param>
        /// <param name="idnderm"></param>
        /// <returns></returns>
        internal DataRow merrStatusRiparimiMePershk(string pershkrimi, int idnderm, int idperdoruesi)
        {

            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@PERSHKRIMi", pershkrimi, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDPERDORUES", idperdoruesi, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_STATUSRIPARIMI_ktheStatusRiparimiSipasPershkrimit");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];

        }
        /// <summary>
        /// kontrollon nqs ekziston nje status riparimi me kete kod per kete ndermarje
        /// </summary>
        /// <param name="kod"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool ekzistonStatusRiparimi(String kod, int id)
        {//kontrollon nqs ekziston nje Njesi artikulli me kete kod

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@KODi", kod, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", id, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_STATUSRIPARIMI_ekzistonStatusRiparimi");
            if (ds.Tables[0].Rows.Count == 1)
                return true;
            else if (ds.Tables[0].Rows.Count == 0)
                return false;
            else return true;

        }
        public bool eshteTransferuarTekBijStatusRiparimi(string kodi, int idndermarje)
        {

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@KODI", kodi, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idndermarje, ParameterDirection.Input);
            int pergjigje = Convert.ToInt32(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_STATUSRIPARIMI_eshteTransferuarTekBij"));
            return Convert.ToBoolean(pergjigje);

        }
        /// <summary>
        /// perdoret per te kontrolluar nese kjo njesi artikulli eshte e lidhur me artikuj.
        /// kjo behet per te mos lejuar te fshihet nje njesi artikulli te lidhur me nje artikull
        /// </summary>
        /// <param name="id"> id e njesise se artikullit</param>
        /// <returns> nje objekt boolean qe tregon nese kjo njesi eshte e lidhur me nje artikull apo jo</returns>
        public bool kaVeprimeStatusRiparimi(int id)
        {//kontrollon nqs ka veprime me kete njesi artikulli

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@ID", id, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_STATUSRIPARIMI_kaVeprime");
            if (ds.Tables[0].Rows.Count == 1)
                return true;
            else if (ds.Tables[0].Rows.Count == 0)
                return false;
            else return true;

        }

        #endregion

        /// <summary>
        /// Metodat e meposhtme i perkasin veprimeve qe kryhen nga objektet clsPrintimeKase dhe colPrintimeNeKase
        /// </summary>
        #region DERGIME NE KASE

        /// <summary>
        /// Ruajtja ne tabelen qe ruan statuset e printimeve ne kase
        /// </summary>
        /// <param name="idKasa">Id e regjistrimit ne tabelen qe ruan statuset e printimeve ne kase</param>
        /// <param name="idShitje">Id e dokumentit te shitjes</param>
        /// <param name="printimi">True nese eshte kryer printimi ne kase dhe anasjelltas</param>
        /// <param name="dergimi">True nese eshte kryer dergimi per ne kase nga AlphaWEB-i</param>
        /// <param name="ip">Ip e pajisjes nga do te kryehet printimi</param>
        /// <param name="idShop">Id e dyqanit qe po kryen printimin</param>
        /// <param name="pershkrimi">Pershkrimi i mesazhit te gabimit</param>
        /// <param name="dataPrintimit">Data kur behet regjistrimi ne tabelen e printimeve ne kase</param>
        /// <param name="idUser">Id e userit qe po kryehen printimin ne kase</param>
        /// <returns>Kthen id e re te regjistrimit ne tabelen qe ruan statuset e printimeve ne kase</returns>
        internal int ruajDergimiKase(int idKasa, int idShitje, bool printimi, bool dergimi, string ip, int idShop, string pershkrimi, DateTime dataPrintimit, int idUser, int idBanka)
        {

            dbManager.Open();
            dbManager.CreateParameters(10);
            dbManager.AddParameters(0, "@ID_KASA", idKasa, ParameterDirection.Output);
            if (idShitje > 0)
                dbManager.AddParameters(1, "@SHITJE_ID", idShitje, ParameterDirection.Input);
            else dbManager.AddParameters(1, "@SHITJE_ID", DBNull.Value, ParameterDirection.Input);
            dbManager.AddParameters(2, "@PRINTUAR", printimi, ParameterDirection.Input);
            dbManager.AddParameters(3, "@DERGUAR", dergimi, ParameterDirection.Input);
            dbManager.AddParameters(4, "@IP", ip, ParameterDirection.Input);
            dbManager.AddParameters(5, "@ID_SHOP", idShop, ParameterDirection.Input);
            dbManager.AddParameters(6, "@PERSHKRIMI", pershkrimi, ParameterDirection.Input);
            dbManager.AddParameters(7, "@DATA_PRINTIMIT", dataPrintimit, ParameterDirection.Input);
            dbManager.AddParameters(8, "@ID_USER", idUser, ParameterDirection.Input);
            if (idBanka > 0)
                dbManager.AddParameters(9, "@BANKAID", idBanka, ParameterDirection.Input);
            else dbManager.AddParameters(9, "@BANKAID", DBNull.Value, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_DERGIME_NE_KASE_ins");
            idKasa = int.Parse(dbManager.Parameters[0].Value.ToString());
            return idKasa;

        }

        /// <summary>
        /// Modifikon statuset ne tabelen e ruajtjes se statuseve te printimeve ne kase
        /// </summary>
        /// <param name="idKasa">Id e regjistrimit ne tabelen qe ruan statuset e printimeve ne kase</param>
        /// <param name="idShitje">Id e dokumentit te shitjes</param>
        /// <param name="printimi">True nese eshte kryer printimi ne kase dhe anasjelltas</param>
        /// <param name="dergimi">True nese eshte kryer dergimi per ne kase nga AlphaWEB-i</param>
        /// <param name="ip">Ip e pajisjes nga do te kryehet printimi</param>
        /// <param name="idShop">Id e dyqanit qe po kryen printimin</param>
        /// <param name="pershkrimi">Pershkrimi i mesazhit te gabimit</param>
        /// <param name="dataPrintimit">Data kur behet regjistrimi ne tabelen e printimeve ne kase</param>
        /// <param name="idUser">Id e userit qe po kryehen printimin ne kase</param>
        /// <returns>Kthen nje mesazh nese eshte kryer apo jo modifikimi me gjithe pershkrimin e errorit ose suksesit</returns>
        internal clsMesazh modifikoDergimiKase(int idKasa, int idShitje, bool printimi, bool dergimi, string ip, int idShop, string pershkrimi, DateTime dataPrintimit, int idUser)
        {


            dbManager.Open();
            dbManager.CreateParameters(9);
            dbManager.AddParameters(0, "@ID_KASA", idKasa, ParameterDirection.Input);
            dbManager.AddParameters(1, "@SHITJE_ID", idShitje, ParameterDirection.Input);
            dbManager.AddParameters(2, "@PRINTUAR", printimi, ParameterDirection.Input);
            dbManager.AddParameters(3, "@DERGUAR", dergimi, ParameterDirection.Input);
            dbManager.AddParameters(4, "@IP", ip, ParameterDirection.Input);
            dbManager.AddParameters(5, "@ID_SHOP", idShop, ParameterDirection.Input);
            dbManager.AddParameters(6, "@PERSHKRIMI", pershkrimi, ParameterDirection.Input);
            dbManager.AddParameters(7, "@DATA_PRINTIMIT", dataPrintimit, ParameterDirection.Input);
            dbManager.AddParameters(8, "@ID_USER", idUser, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_DERGIME_NE_KASE_upd");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
            return mesazh;

        }

        /// <summary>
        /// Merr te gjitha regjistrimet ne tabelen qe ruan statuset e printimeve ne kase
        /// </summary>
        /// <returns>Kthen DataTable te te dhenave nga databaza</returns>
        internal DataTable ktheTeGjithaRegjistrimetPerKase()
        {

            dbManager.Open();
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_DERGIME_NE_KASE_sel");

            return ds.Tables[0];

        }

        /// <summary>
        /// Merr rreshtin nga regjistrimet ne tabelen qe ruan statuset e printimeve ne kase qe ka id e dokumentit te shitjes
        /// </summary>
        /// <param name="idShitje">Id e dokumentit te shitjes</param>
        /// <returns>Kthen DataRown te te nje rreshti nga databaza</returns>
        internal DataRow ktheDergimeKaseSipasIdShitje(int idShitje)
        {



            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@ID_SHITJE", idShitje, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_DERGIME_NE_KASE_sel_sipas_idDok");

            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];

        }
        internal DataRow ktheDergimeKaseSipasIdBanka(int idbanka)
        {


            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@BANKAID", idbanka, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_DERGIME_NE_KASE_sel_sipas_idDokBanka");

            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];

        }

        /// <summary>
        /// Merr rreshtin nga regjistrimet ne tabelen qe ruan statuset e printimeve ne kase qe ka id e dokumentit te shitjes dhe id e dyqanit
        /// </summary>
        /// <param name="idShitje">Id e dokumentit te shitjes</param>
        /// <param name="idShop">Id e dyqanit</param>
        /// <returns>Kthen DataRown te te nje rreshti nga databaza</returns>
        internal DataRow ktheDergimeKaseSipasIdShitjeDheIdShop(int idShitje, int idShop)
        {

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@ID_SHITJE", idShitje, ParameterDirection.Input);
            dbManager.AddParameters(1, "@ID_SHOP", idShop, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_DERGIME_NE_KASE_sel_sipas_idDok_idShop");

            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];

        }

        /// <summary>
        /// Merr te gjitha regjistrimet ne tabelen qe ruan statuset e printimeve ne kase qe kane id e dyqanit dhe userin perkates
        /// </summary>
        /// <param name="idShop">Id e dyqanit</param>
        /// <param name="idUser">Id e userit</param>
        /// <returns>Kthen DataTable te te dhenave nga databaza</returns>
        internal DataTable ktheDergimeKaseSipasIdShop(int idShop, int idUser)
        {

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@ID_SHOP", idShop, ParameterDirection.Input);
            dbManager.AddParameters(1, "@ID_USER", idUser, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_DERGIME_NE_KASE_sel_sipas_idShop");

            return ds.Tables[0];

        }

        /// <summary>
        /// Merr rreshtin nga regjistrimet ne tabelen qe ruan statuset e printimeve ne kase qe ka id e dokumentit te shitjes me nje status printimi te caktuar
        /// </summary>
        /// <param name="idShitje">Id e dokumentit te shitjes</param>
        /// <param name="statusiPrintimi">Nese kerkojme te printuar(true), ose nese kerkojme te paprintuarat(false) ne kase</param>
        /// <returns>Kthen DataRown te te nje rreshti nga databaza</returns>
        internal DataRow ktheDergimeKaseSipasIdShitjeDheStatus(int idShitje, bool statusiPrintimi)
        {

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@ID_SHITJE", idShitje, ParameterDirection.Input);
            dbManager.AddParameters(1, "@STATUS", statusiPrintimi, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_DERGIME_NE_KASE_sel_sipas_idDok_STATUS");

            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];

        }

        /// <summary>
        /// Merr rreshtin nga regjistrimet ne tabelen qe ruan statuset e printimeve ne kase qe ka id e dokumentit te shitjes me nje status printimi te caktuar ne nje dyqan
        /// </summary>
        /// <param name="idShitje">Id e dokumentit te shitjes</param>
        /// <param name="idShop">Id e dyqanit</param>
        /// <param name="statusiPrintimi">Nese kerkojme te printuar(true), ose nese kerkojme te paprintuarat(false) ne kase</param>
        /// <returns>Kthen DataRown te te nje rreshti nga databaza</returns>
        internal DataRow ktheDergimeKaseSipasIdShitjeDheIdShopDheStatus(int idShitje, int idShop, bool statusiPrintimi)
        {

            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@ID_SHITJE", idShitje, ParameterDirection.Input);
            dbManager.AddParameters(1, "@ID_SHOP", idShop, ParameterDirection.Input);
            dbManager.AddParameters(2, "@STATUS", statusiPrintimi, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_DERGIME_NE_KASE_sel_sipas_idDok_idShop_STATUS");

            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];

        }

        /// <summary>
        /// Merr te gjitha regjistrimet ne tabelen qe ruan statuset e printimeve ne kase qe kane id e dyqanit dhe userin perkates me nje status printimi te caktuar
        /// </summary>
        /// <param name="idShop">Id e dyqanit</param>
        /// <param name="statusiPrintimi">Nese kerkojme te printuar(true), ose nese kerkojme te paprintuarat(false) ne kase</param>
        /// <param name="idUser">Id e userit</param>
        /// <returns>Kthen DataTable te te dhenave nga databaza</returns>
        internal DataTable ktheDergimeKaseSipasIdShopDheStatus(int idShop, bool statusiPrintimi, int idUser)
        {

            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@ID_SHOP", idShop, ParameterDirection.Input);
            dbManager.AddParameters(1, "@STATUS", statusiPrintimi, ParameterDirection.Input);
            dbManager.AddParameters(2, "ID_USER", idUser, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_DERGIME_NE_KASE_sel_sipas_idShop_STATUS");

            return ds.Tables[0];

        }
        /// <summary>
        /// Merr te gjitha regjistrimet ne tabelen qe ruan statuset e printimeve ne kase qe kane id e dyqanit dhe userin perkates me nje status printimi te caktuar dhe ndermarjen
        /// </summary>
        /// <param name="idShop">Id e dyqanit</param>
        /// <param name="statusiPrintimi">Nese kerkojme te printuar(true), ose nese kerkojme te paprintuarat(false) ne kase</param>
        /// <param name="idUser">Id e userit</param>
        /// <param name="idndermarje"> id e ndermarjes</param>
        /// <returns>Kthen DataTable te te dhenave nga databaza</returns>
        internal DataTable ktheDergimeKaseSipasIdShopIdNdermDheStatus(int idShop, bool statusiPrintimi, int idUser, int idndermarje, int idnivel)
        {



            dbManager.Open();
            dbManager.CreateParameters(5);
            dbManager.AddParameters(0, "@ID_SHOP", idShop, ParameterDirection.Input);
            dbManager.AddParameters(1, "@STATUS", statusiPrintimi, ParameterDirection.Input);
            dbManager.AddParameters(2, "ID_USER", idUser, ParameterDirection.Input);
            dbManager.AddParameters(3, "IDNDERMARJE", idndermarje, ParameterDirection.Input);
            dbManager.AddParameters(4, "@IDNIVEL", idnivel, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_DERGIME_NE_KASE_sel_sipas_idShop_idNdermarje_STATUS");

            return ds.Tables[0];

        }
        internal DataTable ktheDergimeKaseSipasIdShopIdNdermDheStatusBanka(int idShop, bool statusiPrintimi, int idUser, int idndermarje)
        {
            dbManager.Open();
            dbManager.CreateParameters(4);
            dbManager.AddParameters(0, "@ID_SHOP", idShop, ParameterDirection.Input);
            dbManager.AddParameters(1, "@STATUS", statusiPrintimi, ParameterDirection.Input);
            dbManager.AddParameters(2, "ID_USER", idUser, ParameterDirection.Input);
            dbManager.AddParameters(3, "IDNDERMARJE", idndermarje, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_DERGIME_NE_KASE_sel_sipas_idShop_idNdermarje_STATUSBanka");

            return ds.Tables[0];

        }
        #endregion


        /// <summary>
        /// Metodat e meposhtme i perkasin veprimeve qe kryhen nga objektet clsAutomjete dhe colAutomjete
        /// </summary>
        #region AUTOMJETET

        internal clsMesazh ruajAutomjet(out int idAutomjeti, string nrShasie, string targa, int modelAuto, int viti, double kilometra, string kodMotorri, int idstatusdok, int idKlienti, int idNdermarje, int idKrijuesi, int idPerdoruesi, string marka)
        { //metoda per ruajtjen e automjetit
            idAutomjeti = -1;

            dbManager.Open();
            clsMesazh mesazh = new clsMesazh();
            //shtimi i parametrave
            dbManager.CreateParameters(13);
            dbManager.AddParameters(0, "@IDAUTOMJETI", idAutomjeti, ParameterDirection.Output);
            dbManager.AddParameters(1, "@NRSHASIE", nrShasie, ParameterDirection.Input);
            dbManager.AddParameters(2, "@TARGA", targa, ParameterDirection.Input);
            if (modelAuto == 0 || modelAuto == -1)
                dbManager.AddParameters(3, "@MODELAUTOMJETI", DBNull.Value, ParameterDirection.Input);
            else
                dbManager.AddParameters(3, "@MODELAUTOMJETI", modelAuto, ParameterDirection.Input);
            dbManager.AddParameters(4, "@VITPRODHIMI", viti, ParameterDirection.Input);
            dbManager.AddParameters(5, "@KILOMETRA", kilometra, ParameterDirection.Input);
            dbManager.AddParameters(6, "@KODMOTORRI", kodMotorri, ParameterDirection.Input);
            dbManager.AddParameters(7, "@IDSTATUSDOK", idstatusdok, ParameterDirection.Input);
            if (idKlienti == 0 || idKlienti == -1)
                dbManager.AddParameters(8, "@IDKLIENTI", DBNull.Value, ParameterDirection.Input);
            else
                dbManager.AddParameters(8, "@IDKLIENTI", idKlienti, ParameterDirection.Input);
            dbManager.AddParameters(9, "@IDNDERMARRJE", idNdermarje, ParameterDirection.Input);
            dbManager.AddParameters(10, "@IDKRIJUES", idKrijuesi, ParameterDirection.Input);
            dbManager.AddParameters(11, "@IDPERDORUES", idPerdoruesi, ParameterDirection.Input);
            dbManager.AddParameters(12, "@MARKA", marka, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_AUTOMJETE_ins");
            idAutomjeti = int.Parse(dbManager.Parameters[0].Value.ToString());
            mesazh = new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
            return mesazh;

        }

        /// <summary>
        /// ekzekuton prc_T_AUTOMJETE_upd ne DB dhe kthen statusin e perfundimit te ekzekutimit te SP-se perkatese
        /// Shiko clsMesazh brenda ketij moduli per informacion te metejshem mbi vleren e kthimit te kesaj metode        
        /// <returns> nje objekt clsMesazh qe tregon nese modifikimi eshte kryer ne rregull apo jo.</returns>        
        internal clsMesazh modifikoAutomjet(int idAutomjeti, string nrShasie, string targa, int modelAuto, int viti, double kilometra, string kodMotorri, int idstatusdok, int idKlienti, int idNdermarje, int idPerdoruesi, string marka)
        {//metoda per modifikimin e automjetit

            dbManager.Open();
            clsMesazh mesazh = new clsMesazh();
            //shtimi i parametrave
            dbManager.CreateParameters(12);
            dbManager.AddParameters(0, "@IDAUTOMJETI", idAutomjeti, ParameterDirection.Input);
            dbManager.AddParameters(1, "@NRSHASIE", nrShasie, ParameterDirection.Input);
            dbManager.AddParameters(2, "@TARGA", targa, ParameterDirection.Input);
            if (modelAuto == 0) dbManager.AddParameters(3, "@MODELAUTOMJETI", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(3, "@MODELAUTOMJETI", modelAuto, ParameterDirection.Input);
            dbManager.AddParameters(4, "@VITPRODHIMI", viti, ParameterDirection.Input);
            dbManager.AddParameters(5, "@KILOMETRA", kilometra, ParameterDirection.Input);
            dbManager.AddParameters(6, "@KODMOTORRI", kodMotorri, ParameterDirection.Input);
            dbManager.AddParameters(7, "@IDSTATUSDOK", idstatusdok, ParameterDirection.Input);
            if (idKlienti == 0 || idKlienti == -1)
                dbManager.AddParameters(8, "@IDKLIENTI", DBNull.Value, ParameterDirection.Input);
            else
                dbManager.AddParameters(8, "@IDKLIENTI", idKlienti, ParameterDirection.Input);
            dbManager.AddParameters(9, "@IDNDERMARRJE", idNdermarje, ParameterDirection.Input);
            dbManager.AddParameters(10, "@IDPERDORUES", idPerdoruesi, ParameterDirection.Input);
            dbManager.AddParameters(11, "@MARKA", marka, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_AUTOMJETE_upd");
            mesazh = new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
            return mesazh;

        }

        internal clsMesazh fshiAutomjetMeStatusDok(int idAutomjeti, int idperdoruesi)
        {//metoda per fshirjen e automjetit            

            dbManager.Open();
            //shtimi i parametrave
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDAUTOMJETI", idAutomjeti, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUES", idperdoruesi, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_AUTOMJETE_updDel");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
            return mesazh;

        }

        /// <summary>
        /// perdoret per te kontrolluar nqs ekziston nje automjet me kete numer shasie ne kete ndermarje
        /// behet kontrolli per qellime konsistence te informacionit ne DB, nuk lejon te celen automjete te ndryshem me te njejtin numer shasie
        /// </summary>
        ///<param name="idndermarje"> id e ndermarjes</param>
        ///<param name="nrShasie"> numri i shasise</param>
        /// <returns>nje objekt boolean qe tregon nese ekziston apo jo nje automjet me kete nr shasie</returns>
        public bool ekzistonAutomjet(String nrShasie, int idndermarje)
        {

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDNDERMARJE", idndermarje, ParameterDirection.Input);
            dbManager.AddParameters(1, "@NRSHASIE", nrShasie, ParameterDirection.Input);
            Object ekz = dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_AUTOMJETE_ekzistonAutomjet");
            bool ekziston = Convert.ToBoolean(ekz);
            return ekziston;

        }

        /// <summary>
        /// perdoret per te kontrolluar nqs ekziston nje automjet me kete numer shasie ne kete ndermarje
        /// behet kontrolli per qellime konsistence te informacionit ne DB, nuk lejon te celen automjete te ndryshem me te njejtin numer shasie
        /// </summary>
        ///<param name="idndermarje"> id e ndermarjes</param>
        ///<param name="nrShasie"> numri i shasise</param>
        /// <returns>nje objekt boolean qe tregon nese ekziston apo jo nje automjet me kete nr shasie</returns>
        public bool ekzistonAutomjetTjeter(String nrShasie, int idauto, int idndermarje)
        {

            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@IDNDERMARJE", idndermarje, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDAUTO", idauto, ParameterDirection.Input);
            dbManager.AddParameters(2, "@NRSHASIE", nrShasie, ParameterDirection.Input);
            Object ekz = dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_AUTOMJETE_ekzistonAutomjetTjeter");
            bool ekziston = Convert.ToBoolean(ekz);
            return ekziston;

        }
        /// <summary>
        /// perdoret per te kontrolluar nqs ekziston nje automjet me kete numer shasie ne kete ndermarje
        /// behet kontrolli per qellime konsistence te informacionit ne DB, nuk lejon te celen automjete te ndryshem me te njejtin numer shasie
        /// </summary>
        ///<param name="idndermarje"> id e ndermarjes</param>
        ///<param name="nrShasie"> numri i shasise</param>
        /// <returns>nje objekt boolean qe tregon nese ekziston apo jo nje automjet me kete nr shasie</returns>
        public bool ekzistonAutomjetSipasTarges(String targa, int idndermarje)
        {

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDNDERMARJE", idndermarje, ParameterDirection.Input);
            dbManager.AddParameters(1, "@TARGA", targa, ParameterDirection.Input);
            Object ekz = dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_AUTOMJETE_ekzistonAutomjetSipasTarges");
            bool ekziston = Convert.ToBoolean(ekz);
            return ekziston;

        }

        /// <summary>
        /// kthen datarow automjeti sipas idse
        /// </summary>
        ///<param name="idAuto"> id e automjetit</param>
        ///<returns> nje datarow qe permban automjetin sipas id se automjetit</returns>
        internal DataRow merrAutomjetSipasId(int idAuto)
        {
            ImbLogger.LogTraceShitje($"Filloi metoda merr automjet sipas id auto:{idAuto}");
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDAUTOMJET", idAuto, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_AUTOMJETE_merrSipasId");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            ImbLogger.LogTraceShitje($"Mbaroi metoda merr automjet sipas id auto:{idAuto}");
            return ds.Tables[0].Rows[0];

        }

        /// <summary>
        /// kthen targen e automjetit sipas idse
        /// </summary>
        ///<param name="idAuto"> id e automjetit</param>
        ///<returns> nje datarow qe permban automjetin sipas id se automjetit</returns>
        internal string merrTargeAutomjetSipasId(int idAuto)
        {

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDAUTOMJET", idAuto, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_AUTOMJETE_merrSipasId");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0]["TARGA"].ToString();

        }

        internal string merrKodbarinSipasIdArtikulli(string kodartikulli, int idNdermarrje)
        {

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@KODARTIKULLI", kodartikulli, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idNdermarrje, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KODBARIPARE_merrSipasArt");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0]["PERSHKRIMI"].ToString();

        }

        internal int merrIdKodbarinSipasIdArtikulli(string kodartikulli, int idNdermarrje)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@KODARTIKULLI", kodartikulli, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idNdermarrje, ParameterDirection.Input);
            var result = dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_KODBARIPARE_merrIdSipasArt");
            if (result == null)
                return 0;
            else
                return Convert.ToInt32(result.ToString());
        }
        
        /// <summary>
        /// kthen datarow automjeti sipas targes dhe ndermarrjes
        /// </summary>
        ///<param name="idAuto"> id e automjetit</param>
        ///<returns> nje datarow qe permban automjetin sipas id se automjetit</returns>
        internal int merrIdAutomjetiSipasShasise(string shasia, int idNdermarrje)
        {

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDNDERMARJE", idNdermarrje, ParameterDirection.Input);
            dbManager.AddParameters(1, "@SHASIA", shasia, ParameterDirection.Input);
            int idAuto = Convert.ToInt32(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_AUTOMJETE_merrIdAutomjetiSipasNdermarrjesDheShasise"));
            return idAuto;

        }



        /// <summary>
        /// kthen datarow automjeti sipas targes dhe ndermarrjes
        /// </summary>
        ///<param name="idAuto"> id e automjetit</param>
        ///<returns> nje datarow qe permban automjetin sipas id se automjetit</returns>
        internal int merrIdAutomjetiSipasTarges(string targa, int idNdermarrje)
        {

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDNDERMARJE", idNdermarrje, ParameterDirection.Input);
            dbManager.AddParameters(1, "@TARGA", targa, ParameterDirection.Input);
            int idAuto = Convert.ToInt32(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_AUTOMJETE_merrIdAutomjetiSipasNdermarrjesDheTarges"));
            return idAuto;

        }

        /// <summary>
        /// kthen datarow automjeti sipas targes dhe ndermarrjes
        /// </summary>
        ///<param name="idAuto"> id e automjetit</param>
        ///<returns> nje datarow qe permban automjetin sipas id se automjetit</returns>
        internal DataRow merrAutomjetSipasTarges(string targa, int idNdermarrje)
        {

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDNDERMARJE", idNdermarrje, ParameterDirection.Input);
            dbManager.AddParameters(1, "@TARGA", targa, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_AUTOMJETE_merrSipasNdermarrjesDheTarges");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];

        }
        /// <summary>
        /// kthen datarow automjeti sipas idse
        /// </summary>
        ///<param name="idAuto"> id e automjetit</param>
        ///<returns> nje datarow qe permban automjetin sipas id se automjetit</returns>
        internal DataTable merrAutomjetSipasIdDt(int idAuto)
        {

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDAUTOMJET", idAuto, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_AUTOMJETE_merrSipasId");
            if (ds == null)
                return null;
            return ds.Tables[0];

        }

        /// <summary>
        /// kthen datarow automjeti sipas id se ndermarrjes dhe numrit te shasise
        /// </summary>
        ///<param name="idNdermarrje"> id e ndermarrjes</param>
        ///<param name="nrShasie"> numer shasie</param>
        ///<returns> nje datarow qe permban automjetin</returns>
        internal DataRow merrAutomjetSipasNdermarrjeDheNrShasie(int idNdermarrje, string nrShasie)
        {

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDNDERMARJE", idNdermarrje, ParameterDirection.Input);
            dbManager.AddParameters(1, "@NRSHASIE", nrShasie, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_AUTOMJETE_merrSipasNdermarrjesDheNrShasie");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];

        }

        /// <summary>
        /// kthen datatable me automjetet e ndermarrjes
        /// </summary>
        ///<param name="idNdermarrje"> id e ndermarrjes</param>
        ///<returns> nje datatable qe permban automjetet e ndermarrjes</returns>
        internal DataTable merrAutomjetSipasNdermarrjes(int idNdermarrje)
        {

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDNDERMARJE", idNdermarrje, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_AUTOMJETE_merrSipasNdermarrjes");
            if (ds == null)
                return null;
            return ds.Tables[0];

        }

        /// <summary>
        /// kthen datatable me automjetet e ndermarrjes dhe klientit
        /// </summary>
        ///<param name="idNdermarrje"> id e ndermarrjes</param>
        ///<returns> nje datatable qe permban automjetet e ndermarrjes</returns>
        internal DataTable merrAutomjetSipasNdermarrjesDheKlientit(int idNdermarrje, int idKlienti)
        {

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDNDERMARJE", idNdermarrje, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDKLIENTI", idKlienti, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_AUTOMJETE_merrSipasNdermarrjesDheKlientit");
            if (ds == null)
                return null;
            return ds.Tables[0];

        }

        /// <summary>
        /// kthen datatable me automjetet e ndermarrjes dhe klientit per kombon
        /// </summary>
        ///<param name="idNdermarrje"> id e ndermarrjes</param>
        ///<returns> nje datatable qe permban automjetet e ndermarrjes</returns>
        internal DataTable merrAutomjetSipasNdermarrjesDheKlientitPerKombo(int idNdermarrje, int idKlienti, string filter, long startIndex, long endIndex)
        {

            dbManager.Open();
            dbManager.CreateParameters(5);
            dbManager.AddParameters(0, "@IDNDERMARJE", idNdermarrje, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDKLIENTI", idKlienti, ParameterDirection.Input);
            dbManager.AddParameters(2, "@FILTER", filter, ParameterDirection.Input);
            dbManager.AddParameters(3, "@STARTINDEX", startIndex, ParameterDirection.Input);
            dbManager.AddParameters(4, "@ENDINDEX", endIndex, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_AUTOMJETE_merrSipasNdermarrjesDheKlientitPerKombo");
            if (ds == null)
                return null;
            return ds.Tables[0];

        }

        /// <summary>
        /// kthen datatable me automjetet e ndermarrjes dhe klientit per kombon
        /// </summary>
        ///<param name="idNdermarrje"> id e ndermarrjes</param>
        ///<returns> nje datatable qe permban automjetet e ndermarrjes</returns>
        internal DataTable merrAutomjetSipasNdermarrjesPerKombo(int idNdermarrje, string filter, long startIndex, long endIndex)
        {

            dbManager.Open();
            dbManager.CreateParameters(4);
            dbManager.AddParameters(0, "@IDNDERMARJE", idNdermarrje, ParameterDirection.Input);
            dbManager.AddParameters(1, "@FILTER", filter, ParameterDirection.Input);
            dbManager.AddParameters(2, "@STARTINDEX", startIndex, ParameterDirection.Input);
            dbManager.AddParameters(3, "@ENDINDEX", endIndex, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_AUTOMJETE_merrSipasNdermarrjesPerKombo");
            if (ds == null)
                return null;
            return ds.Tables[0];

        }

        #endregion

        /// <summary>
        /// Metodat e meposhtme i perkasin veprimeve qe kryhen nga objektet clsModelAutomjeti dhe colModeleAutomjetesh
        /// </summary>
        #region MODELE AUTOMJETESH

        internal clsMesazh ruajModelAutomjeti(out int idModelAutomjeti, string kodModeli, string pershkrimModeli, int idstatusdok, int idNdermarje, int idKrijuesi, int idPerdoruesi)
        { //metoda per ruajtjen e modelit te automjetit
            idModelAutomjeti = -1;
            clsMesazh mesazh = new clsMesazh();

            dbManager.Open();

            //shtimi i parametrave
            dbManager.CreateParameters(7);
            dbManager.AddParameters(0, "@IDMODELI", idModelAutomjeti, ParameterDirection.Output);
            dbManager.AddParameters(1, "@KODMODELI", kodModeli, ParameterDirection.Input);
            dbManager.AddParameters(2, "@PERSHKRIMMODELI", pershkrimModeli, ParameterDirection.Input);
            dbManager.AddParameters(3, "@IDSTATUSDOK", idstatusdok, ParameterDirection.Input);
            dbManager.AddParameters(4, "@IDNDERMARRJE", idNdermarje, ParameterDirection.Input);
            dbManager.AddParameters(5, "@IDKRIJUES", idKrijuesi, ParameterDirection.Input);
            dbManager.AddParameters(6, "@IDPERDORUES", idPerdoruesi, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_MODELAUTOMJETI_ins");
            idModelAutomjeti = int.Parse(dbManager.Parameters[0].Value.ToString());
            mesazh = new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
            return mesazh;

        }

        /// <summary>
        /// ekzekuton prc_T_MODELAUTOMJETI_upd ne DB dhe kthen statusin e perfundimit te ekzekutimit te SP-se perkatese
        /// Shiko clsMesazh brenda ketij moduli per informacion te metejshem mbi vleren e kthimit te kesaj metode        
        /// <returns> nje objekt clsMesazh qe tregon nese modifikimi eshte kryer ne rregull apo jo.</returns>        
        internal clsMesazh modifikoModelAutomjet(int idModeli, string kodModeli, string pershkrimModeli, int idstatusdok, int idNdermarje, int idPerdoruesi)
        {//metoda per modifikimin e modelit te automjetit
            clsMesazh mesazh = new clsMesazh();

            dbManager.Open();
            //shtimi i parametrave
            dbManager.CreateParameters(6);
            dbManager.AddParameters(0, "@IDMODELI", idModeli, ParameterDirection.Input);
            dbManager.AddParameters(1, "@KODMODELI", kodModeli, ParameterDirection.Input);
            dbManager.AddParameters(2, "@PERSHKRIMMODELI", pershkrimModeli, ParameterDirection.Input);
            dbManager.AddParameters(3, "@IDSTATUSDOK", idstatusdok, ParameterDirection.Input);
            dbManager.AddParameters(4, "@IDNDERMARRJE", idNdermarje, ParameterDirection.Input);
            dbManager.AddParameters(5, "@IDPERDORUES", idPerdoruesi, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_MODELAUTOMJETI_upd");
            mesazh = new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
            return mesazh;

        }

        internal clsMesazh fshiModelAutomjetiMeStatusDok(int idModeli, int idperdoruesi)
        {//metoda per fshirjen e modelit te automjetit            

            dbManager.Open();
            //shtimi i parametrave
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDMODELI", idModeli, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUES", idperdoruesi, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_MODELAUTOMJETI_updDel");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
            return mesazh;

        }

        /// <summary>
        /// kthen datatable me automjetet e ndermarrjes
        /// </summary>
        ///<param name="idNdermarrje"> id e ndermarrjes</param>
        ///<returns> nje datatable qe permban automjetet e ndermarrjes</returns>
        internal DataTable merrModeleAutomjeteshSipasNdermarrjes(int idNdermarrje)
        {

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDNDERMARRJE", idNdermarrje, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_MODELAUTOMJETI_merrTeGjitha");
            if (ds == null)
                return null;
            return ds.Tables[0];

        }

        /// <summary>
        /// kthen datarow modelin e automjetit sipas id se ndermarrjes dhe kodit
        /// </summary>
        ///<param name="idNdermarrje"> id e ndermarrjes </param>
        ///<param name="kodi"> kodi </param>
        ///<returns> nje datarow qe permban modelin e automjetit</returns>
        internal DataRow merrModelAutomjetiSipasNdermarrjeDheKodit(int idNdermarrje, string kodi)
        {

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDNDERMARJE", idNdermarrje, ParameterDirection.Input);
            dbManager.AddParameters(1, "@KODMODELI", kodi, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_MODELAUTOMJETI_merrSipasNdermDheKodModeli");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];
        }

        internal DataTable merrModelAutomjetiSipasNdermarrjeDheKoditPerKombo(string filter, long startIndex, long endIndex, int idnderm)
        {//metoda per te marre te gjithe kliente furnitoret

            dbManager.Open();
            dbManager.CreateParameters(4);
            dbManager.AddParameters(0, "@IDNDERMARRJE", idnderm, ParameterDirection.Input);
            dbManager.AddParameters(1, "@FILTER", filter, ParameterDirection.Input);
            dbManager.AddParameters(2, "@STARTINDEX", startIndex, ParameterDirection.Input);
            dbManager.AddParameters(3, "@ENDINDEX", endIndex, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_MODELAUTOMJETI_merrSipasNdermDheKodModeliPerKombo");
            return ds.Tables[0];
        }

        /// <summary>
        /// kthen datarow modelin e automjetit sipas id se ndermarrjes dhe kodit
        /// </summary>
        ///<param name="idNdermarrje"> id e ndermarrjes </param>
        ///<param name="kodi"> kodi </param>
        ///<returns> nje datarow qe permban modelin e automjetit</returns>
        internal DataRow merrModelAutomjetiSipasId(int idModeli)
        {

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDMODELAUTO", idModeli, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_MODELAUTOMJETI_merrSipasId");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];

        }

        /// <summary>
        /// kthen datatable modelin e automjetit sipas id se ndermarrjes dhe kodit
        /// </summary>
        ///<param name="idNdermarrje"> id e ndermarrjes </param>
        ///<param name="kodi"> kodi </param>
        ///<returns> nje datarow qe permban modelin e automjetit</returns>
        internal DataTable merrModelAutomjetiSipasIdDt(int idModeli)
        {

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDMODELAUTO", idModeli, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_MODELAUTOMJETI_merrSipasId");
            if (ds == null)
                return null;
            return ds.Tables[0];

        }

        /// <summary>
        /// perdoret per te kontrolluar nqs ekziston nje automjet me kete numer shasie ne kete ndermarje
        /// behet kontrolli per qellime konsistence te informacionit ne DB, nuk lejon te celen automjete te ndryshem me te njejtin numer shasie
        /// </summary>
        ///<param name="idndermarje"> id e ndermarjes</param>
        ///<param name="nrShasie"> numri i shasise</param>
        /// <returns>nje objekt boolean qe tregon nese ekziston apo jo nje automjet me kete nr shasie</returns>
        public bool ekzistonModelAutomjeti(String kodi, int idndermarje)
        {

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDNDERMARJE", idndermarje, ParameterDirection.Input);
            dbManager.AddParameters(1, "@KODMODELI", kodi, ParameterDirection.Input);
            Object ekziston = dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_MODELAUTOMJETI_ekzistonSipasKodiDheNdermarrje");
            bool ekz = Convert.ToBoolean(ekziston);
            return ekz;

        }

        public bool eshteLidhurModelAutomjeti(int idModeli)
        {

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDMODELI", idModeli, ParameterDirection.Input);
            Object lidhur = dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_MODELAUTOMJETI_merrLidhurMeAutomjet");
            bool lidh = Convert.ToBoolean(lidhur);
            return lidh;

        }

        #endregion


        /// <summary>
        /// Metodat e meposhtme i perkasin veprimeve qe kryhen nga objektet clsTransportues dhe colTransportues
        /// </summary>
        #region TRANSPORTUESI/OPERATORI

        internal clsMesazh ruajTransportues(out int idTransportues, string emertimi, string nipt, string adresa, string tel, int idstatusdok, int idNdermarje, int idKrijuesi, int idPerdoruesi, string targa, bool aktiv, string tipiId,bool klientFiskalizimi)
        { //metoda per ruajtjen e transportuesit
            idTransportues = -1;

            dbManager.Open();
            clsMesazh mesazh = new clsMesazh();
            //shtimi i parametrave
            if(klientFiskalizimi)
                dbManager.CreateParameters(12);
            else
                dbManager.CreateParameters(11);
            dbManager.AddParameters(0, "@IDTRANSPORTUES", idTransportues, ParameterDirection.Output);
            dbManager.AddParameters(1, "@EMERTIMI", emertimi, ParameterDirection.Input);
            dbManager.AddParameters(2, "@NIPT", nipt, ParameterDirection.Input);
            dbManager.AddParameters(3, "@ADRESA", adresa, ParameterDirection.Input);
            dbManager.AddParameters(4, "@TEL", tel, ParameterDirection.Input);
            dbManager.AddParameters(5, "@IDSTATUSDOK", idstatusdok, ParameterDirection.Input);
            dbManager.AddParameters(6, "@IDNDERMARRJE", idNdermarje, ParameterDirection.Input);
            dbManager.AddParameters(7, "@IDKRIJUES", idKrijuesi, ParameterDirection.Input);
            dbManager.AddParameters(8, "@IDPERDORUES", idPerdoruesi, ParameterDirection.Input);
            dbManager.AddParameters(9, "@TARGA", targa, ParameterDirection.Input);
            dbManager.AddParameters(10, "@AKTIV", aktiv, ParameterDirection.Input);
            if(klientFiskalizimi)
                dbManager.AddParameters(11, "@TIPIID", tipiId, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_TRANSPORTUES_ins");
            idTransportues = int.Parse(dbManager.Parameters[0].Value.ToString());
            mesazh = new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
            return mesazh;

        }
        internal string merrTargeTransportuesiSipasId(int idtransportues)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDTRANSPORTUES", idtransportues, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_TRANSPORTUES_merrSipasId");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0]["TARGA"].ToString();
        }
        /// <summary>
        /// ekzekuton prc_T_TRANSPORTUES_upd ne DB dhe kthen statusin e perfundimit te ekzekutimit te SP-se perkatese
        /// Shiko clsMesazh brenda ketij moduli per informacion te metejshem mbi vleren e kthimit te kesaj metode        
        /// <returns> nje objekt clsMesazh qe tregon nese modifikimi eshte kryer ne rregull apo jo.</returns>        
        internal clsMesazh modifikoTransportues(int idTransportues, string emertimi, string nipt, string adresa, string tel, int idstatusdok, int idNdermarje, int idPerdoruesi, string targa, bool aktiv, string tipiId, bool klientFiskalizimi)
        {//metoda per modifikimin e transportuesit

            dbManager.Open();
            clsMesazh mesazh = new clsMesazh();
            //shtimi i parametrave
            if(klientFiskalizimi)
                dbManager.CreateParameters(11);
            else
                dbManager.CreateParameters(10);
            dbManager.AddParameters(0, "@IDTRANSPORTUES", idTransportues, ParameterDirection.Input);
            dbManager.AddParameters(1, "@EMERTIMI", emertimi, ParameterDirection.Input);
            dbManager.AddParameters(2, "@NIPT", nipt, ParameterDirection.Input);
            dbManager.AddParameters(3, "@ADRESA", adresa, ParameterDirection.Input);
            dbManager.AddParameters(4, "@TEL", tel, ParameterDirection.Input);
            dbManager.AddParameters(5, "@IDSTATUSDOK", idstatusdok, ParameterDirection.Input);
            dbManager.AddParameters(6, "@IDNDERMARRJE", idNdermarje, ParameterDirection.Input);
            dbManager.AddParameters(7, "@IDPERDORUES", idPerdoruesi, ParameterDirection.Input);
            dbManager.AddParameters(8, "@TARGA", targa, ParameterDirection.Input);
            dbManager.AddParameters(9, "@AKTIV", aktiv, ParameterDirection.Input);
            if (klientFiskalizimi)
                dbManager.AddParameters(10, "@TIPIID", tipiId, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_TRANSPORTUES_upd");
            mesazh = new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
            return mesazh;

        }

        internal clsMesazh fshiTransportuesMeStatusDok(int idTransportues, int idperdoruesi)
        {//metoda per fshirjen e transportuesit            

            dbManager.Open();
            //shtimi i parametrave
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDTRANSPORTUES", idTransportues, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUES", idperdoruesi, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_TRANSPORTUES_updDel");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
            return mesazh;

        }

        /// <summary>
        /// perdoret per te kontrolluar nqs ekziston nje transportues me kete emertim ne kete ndermarje
        /// behet kontrolli per qellime konsistence te informacionit ne DB, nuk lejon te celen trasportues te ndryshem me te njejtin emertim
        /// </summary>
        /// <param name="emertimi"> emertimi i transportuesit</param>
        ///<param name="idndermarje"> id e ndermarjes</param>
        /// <returns>nje objekt boolean qe tregon nese ekziston apo jo nje transportues me kete emertim</returns>
        public bool ekzistonTrasportues(String emertimi, int idndermarje)
        {

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDNDERMARJE", idndermarje, ParameterDirection.Input);
            dbManager.AddParameters(1, "@EMERTIMI", emertimi, ParameterDirection.Input);
            Object ekz = dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_TRANSPORTUES_ekzistonTransportues");
            bool ekziston = Convert.ToBoolean(ekz);
            return ekziston;

        }
        public bool ekzistonOperatori(String kodi, int idndermarje)
        {

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDNDERMARJE", idndermarje, ParameterDirection.Input);
            dbManager.AddParameters(1, "@KODI", kodi, ParameterDirection.Input);
            Object ekz = dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_OPERATORE_ekzistonTransportues");
            bool ekziston = Convert.ToBoolean(ekz);
            return ekziston;

        }
        internal DataRow merrKodOperatoriSipasId(int id, int idndermarje)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDOPERATOR", id, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARRJE", idndermarje, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_OPERATORE_merrkodOperatoriSipasId");
            if (ds.Tables[0].Rows.Count == 0)
                return null;
            else
                return ds.Tables[0].Rows[0];
        }
        /// <summary>
        /// kthen datarow transportuesi sipas id-se
        /// </summary>
        ///<param name="idTransportues"> id e transportuesit</param>
        ///<returns> nje datarow qe permban transportuesit sipas id se transportuesit</returns>
        internal DataRow merrTransportuesSipasId(int idTransportues)
        {
            ImbLogger.LogTraceShitje($"Filloi metoda merr transportues sipas id:{idTransportues}");
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDTRANSPORTUES", idTransportues, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_TRANSPORTUES_merrSipasId");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            ImbLogger.LogTraceShitje($"Mbaroi metoda merr transportues sipas id:{idTransportues}");
            return ds.Tables[0].Rows[0];

        }

        /// <summary>
        /// kthen datarow transportuesi sipas emertimit dhe ndermarrjes
        /// </summary>
        ///<param name="emertimi"> emertimi</param>
        ///<param name="idNdermarrje"> id e idNdermarrjes</param>
        ///<returns> nje datarow qe kthen transportuesin sipas emertimit dhe ndermarrjes</returns>
        internal DataRow merrTransportuesSipasEmertimit(string emertimi, int idNdermarrje)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDNDERMARJE", idNdermarrje, ParameterDirection.Input);
            dbManager.AddParameters(1, "@EMERTIMI", emertimi, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_TRANSPORTUES_merrSipasNdermarrjesDheEmertimit");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];
        }

        /// <summary>
        /// kthen datarow transportuesi sipas emertimit dhe ndermarrjes
        /// </summary>
        ///<param name="emertimi"> emertimi</param>
        ///<param name="idNdermarrje"> id e idNdermarrjes</param>
        ///<returns> nje datarow qe kthen transportuesin sipas emertimit dhe ndermarrjes</returns>
        internal int merrIdTransportuesSipasEmertimit(string emertimi, int idNdermarrje)
        {
            ImbLogger.LogTraceShitje($"Merr id e transportuesit sipas emertimit: {emertimi} dhe id ndermarrjes:{idNdermarrje}");
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDNDERMARJE", idNdermarrje, ParameterDirection.Input);
            dbManager.AddParameters(1, "@EMERTIMI", emertimi, ParameterDirection.Input);
            ImbLogger.LogTraceShitje($"Merrja id e transportuesit sipas emertimit: {emertimi} dhe id ndermarrjes:{idNdermarrje} perfundoi.");
            return Convert.ToInt32(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_TRANSPORTUES_merrIdSipasNdermarrjesDheEmertimit"));
        }

        internal string merrEmertimTransportuesSipasId(int idTransportues)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDTRANSPORTUES", idTransportues, ParameterDirection.Input);
            return Convert.ToString(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_TRANSPORTUES_merrEmertimSipasId"));
        }

        /// <summary>
        /// kthen datarow transportuesi sipas idse
        /// </summary>
        ///<param name="idTransportues"> id e transportuesit</param>
        ///<returns> nje datarow qe permban transportuesin sipas id se transportuesit</returns>
        internal DataTable merrTransportuesSipasIdDt(int idTransportues)
        {

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDTRANSPORTUES", idTransportues, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_TRANSPORTUES_merrSipasId");
            if (ds == null)
                return null;
            return ds.Tables[0];

        }

        /// <summary>
        /// kthen datatable me transportuesit e ndermarrjes
        /// </summary>
        ///<param name="idNdermarrje"> id e ndermarrjes</param>
        ///<returns> nje datatable qe permban automjetet e ndermarrjes</returns>
        internal DataTable merrTransportuesSipasNdermarrjes(int idNdermarrje)
        {

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDNDERMARJE", idNdermarrje, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_TRANSPORTUES_merrSipasNdermarrjes");
            if (ds == null)
                return null;
            return ds.Tables[0];

        }

        internal DataTable merrTransportuesSipasNdermarrjesLupe(int idNdermarrje)
        {

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDNDERMARJE", idNdermarrje, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_TRANSPORTUES_merrSipasNdermarrjesAktivet");
            if (ds == null)
                return null;
            return ds.Tables[0];

        }

        internal DataTable merrTransportuesSipasNdermarrjesDtSmall(int idNdermarrje)
        {

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDNDERMARJE", idNdermarrje, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_TRANSPORTUES_merrSipasNdermarrjesDtSmall");
            if (ds == null)
                return null;
            return ds.Tables[0];

        }

        internal DataTable merrTransportuesSipasNdermarrjesMeFilter(string filter, long startIndex, long endIndex, int idnderm)
        {//metoda per te marre te gjithe kliente furnitoret

            dbManager.Open();
            dbManager.CreateParameters(4);
            dbManager.AddParameters(0, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            dbManager.AddParameters(1, "@FILTER", filter, ParameterDirection.Input);
            dbManager.AddParameters(2, "@STARTINDEX", startIndex, ParameterDirection.Input);
            dbManager.AddParameters(3, "@ENDINDEX", endIndex, ParameterDirection.Input);

            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_TRANSPORTUES_merrTransportuesSipasNdermMeFilter");
            return ds.Tables[0];

        }

        internal DataTable ktheGjitheTransportuesit(int idndermarje)
        {

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDNDERMARJE", idndermarje, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_TRANSPORTUES_merrGjitheTransportuesit");
            return ds.Tables[0];

        }

        internal clsMesazh RuajOperator(out int id, string kodi, string emri, string mbiemri, bool aktiv, int idStatusDok, int idNdermarrje, int idPerdorues)
        {
            id = 0;
            try
            {
                dbManager.Open();
                dbManager.CreateParameters(8);
                dbManager.AddParameters(0, "@IDOPERATOR", id, ParameterDirection.Output);
                dbManager.AddParameters(1, "@KODI", kodi, ParameterDirection.Input);
                dbManager.AddParameters(2, "@EMRI", emri, ParameterDirection.Input);
                dbManager.AddParameters(3, "@MBIEMRI", mbiemri, ParameterDirection.Input);
                dbManager.AddParameters(4, "@AKTIV", aktiv, ParameterDirection.Input);
                dbManager.AddParameters(5, "@IDSTATUSDOK", idStatusDok, ParameterDirection.Input);
                dbManager.AddParameters(6, "@IDNDERMARRJE", idNdermarrje, ParameterDirection.Input);
                dbManager.AddParameters(7, "@IDPERDORUESI", idPerdorues, ParameterDirection.Input);

                dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_OPERATORE_ins");
                id = int.Parse(dbManager.Parameters[0].Value.ToString());
                return new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
            }
            catch (SqlException ex)
            {
                if (ex.Number == 2627)
                    return new clsMesazh(false, "Ekziston nje operator me kete kod ne kete ndermarrje!");

                else
                    return new clsMesazh(false, MessagesResource.Messages["msgGabimRuajtje"]);
            }
        }

        internal clsMesazh ModifikoOperator(int id, string kodi, string emri, string mbiemri, bool aktiv, int idStatusDok, int idNdermarrje, int idPerdorues)
        {
            dbManager.Open();
            dbManager.CreateParameters(8);
            dbManager.AddParameters(0, "@IDOPERATOR", id, ParameterDirection.Input);
            dbManager.AddParameters(1, "@KODI", kodi, ParameterDirection.Input);
            dbManager.AddParameters(2, "@EMRI", emri, ParameterDirection.Input);
            dbManager.AddParameters(3, "@MBIEMRI", mbiemri, ParameterDirection.Input);
            dbManager.AddParameters(4, "@AKTIV", aktiv, ParameterDirection.Input);
            dbManager.AddParameters(5, "@IDSTATUSDOK", idStatusDok, ParameterDirection.Input);
            dbManager.AddParameters(6, "@IDNDERMARRJE", idNdermarrje, ParameterDirection.Input);
            dbManager.AddParameters(7, "@IDPERDORUESI", idPerdorues, ParameterDirection.Input);

            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_OPERATORE_upd");
            return new clsMesazh(true, MessagesResource.Messages["msgModifikimiMeSukses"]);
        }

        internal clsMesazh FshiOperator(int idOperator)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDOPERATOR", idOperator, ParameterDirection.Input);

            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_OPERATORE_del");
            return new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
        }

        internal DataTable ktheGjitheOperatoret(int idndermarje)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDNDERMARRJE", idndermarje, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_Operatore_merrGjitheOperatoret");
            return ds.Tables[0];
        }

        #endregion

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

        public DataTable merrLlojKosto()
        {//kthen klient furnitorin sipas nr

            dbManager.Open();
            dbManager.CreateParameters(0);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_RAPORTE_FORMULA_sel");
            return ds.Tables[0];
        }

        #region elemente per integrim
        internal IEnumerable<clsElementePerIntegrim> MerrElementePerIntegrim(int idNdermarrje)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDNDERMARJE", idNdermarrje, ParameterDirection.Input);
            return dbManager.GetIEnumerbale("prc_T_Elemente_Per_Integrim_merrSipasNdermarrjes", clsElementePerIntegrim.Krijo);
        }

        internal IEnumerable<clsElementePerIntegrim> MerrElementePerIntegrimSipasLlojit(int idNdermarrje, int idLloji, bool tePalidhura)
        {
            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@IDNDERMARJE", idNdermarrje, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDLLOJI", idLloji, ParameterDirection.Input);
            dbManager.AddParameters(2, "@TEPALIDHURA", tePalidhura, ParameterDirection.Input);
            return dbManager.GetIEnumerbale("prc_T_Elemente_Per_Integrim_merrSipasNdermarrjesDheLlojit", clsElementePerIntegrim.Krijo);
        }

        internal IEnumerable<clsElementePerIntegrim> MerrElementePerIntegrimTeLidhura(int idNdermarrje, int idLloji)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDNDERMARJE", idNdermarrje, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDLLOJI", idLloji, ParameterDirection.Input);
            return dbManager.GetIEnumerbale("prc_T_Elemente_Per_Integrim_merrSipasNdermarrjesDheLlojitTeLidhura", clsElementePerIntegrim.Krijo);
        }

        internal clsMesazh fshiUpdateStatusDokElementiPerIntegrim(int idElementi, int idModifikuesi)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDELEMENTI", idElementi, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDMODIFIKUESI", idModifikuesi, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_ELEMENTE_PER_INTEGRIM_updateDel");
            return new clsMesazh(true, MessagesResource.Messages["msgModifikimiMeSukses"]);
        }

        internal clsMesazh ruajElementinPerIntegrim(out int idElementi, string kodi, string emertimi, int lloji, bool aktiv, string shenime, DateTime? dateRegjistrimi, int idPerdoruesi, int idNdermarje)
        {
            idElementi = -1;
            dbManager.Open();
            dbManager.CreateParameters(9);
            dbManager.AddParameters(0, "@IDELEMENTI", idElementi, ParameterDirection.Output);
            dbManager.AddParameters(1, "@KODI", kodi, ParameterDirection.Input);
            dbManager.AddParameters(2, "@EMERTIMI", emertimi, ParameterDirection.Input);
            dbManager.AddParameters(3, "@LLOJI", lloji, ParameterDirection.Input);
            dbManager.AddParameters(4, "@AKTIV", aktiv, ParameterDirection.Input);
            if (shenime != null)
                dbManager.AddParameters(5, "@SHENIME", shenime, ParameterDirection.Input);
            else
                dbManager.AddParameters(5, "@SHENIME", DBNull.Value, ParameterDirection.Input);
            if (dateRegjistrimi != null)
                dbManager.AddParameters(6, "@DATEREGJISTRIMI", dateRegjistrimi, ParameterDirection.Input);
            else
                dbManager.AddParameters(6, "@DATEREGJISTRIMI", DBNull.Value, ParameterDirection.Input);
            dbManager.AddParameters(7, "@IDPERDORUESI", idPerdoruesi, ParameterDirection.Input);
            dbManager.AddParameters(8, "@IDNDERMARJE", idNdermarje, ParameterDirection.Input);

            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_ELEMENTE_PER_INTEGRIM_insert");
            idElementi = int.Parse(dbManager.Parameters[0].Value.ToString());

            return new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
        }
        internal clsMesazh modifikoElementinPerIntegrim(int idElementi, string emertimi, bool aktiv, string shenime, int idPerdoruesi)
        {

            dbManager.Open();
            dbManager.CreateParameters(5);
            dbManager.AddParameters(0, "@IDELEMENTI", idElementi, ParameterDirection.Input);
            dbManager.AddParameters(1, "@EMERTIMI", emertimi, ParameterDirection.Input);
            dbManager.AddParameters(2, "@AKTIV", aktiv, ParameterDirection.Input);
            if (shenime != null)
                dbManager.AddParameters(3, "@SHENIME", shenime, ParameterDirection.Input);
            else
                dbManager.AddParameters(3, "@SHENIME", DBNull.Value, ParameterDirection.Input);

            dbManager.AddParameters(4, "@IDMODIFIKUESI", idPerdoruesi, ParameterDirection.Input);

            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_ELEMENTE_PER_INTEGRIM_modifiko");

            return new clsMesazh(true, MessagesResource.Messages["msgModifikimiMeSukses"]);
        }

        internal int MerrIdElementiPerIntegrimSipasKoditDheNdermarrjes(string kodi, int idNdermarrje)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@KODI", kodi, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idNdermarrje, ParameterDirection.Input);
            int idMag = Convert.ToInt32(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_ELEMENTE_PER_INTEGRIM_merrIdSipasKodit"));
            return idMag;
        }
        internal void MerrElementPerIntegrimSipasId(int idElement, clsElementePerIntegrim element)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDELEMENTI", idElement, ParameterDirection.Input);
            dbManager.FillObject<clsElementePerIntegrim>("prc_T_ELEMENTE_PER_INTEGRIM_MerrSipasID", element.Mbush);
        }

        internal bool ekzistonElementPerIntegrimMeKeteKodPerKeteNdermarje(string kodi, int idNdermarje)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@KODI", kodi, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idNdermarje, ParameterDirection.Input);

            int pergjigja = Convert.ToInt32(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_ELEMENTE_PER_INTEGRIM_ekzistonKyDokument"));

            return pergjigja > 0;
        }
        internal bool eshteILidhurKyElement(int idElementi)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDELEMENTI", idElementi, ParameterDirection.Input);

            int pergjigja = Convert.ToInt32(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_ELEMENTE_PER_INTEGRIM_eshteILidhur"));

            return pergjigja > 0;
        }
        internal IEnumerable<clsLlojElementiPerIntegrim> merrLlojElementiPerIntegrim()
        {
            dbManager.Open();
            dbManager.CreateParameters(0);
            return dbManager.GetIEnumerbale("prc_T_LLOJ_ELEMENTI_PER_INTEGRIM_select", clsLlojElementiPerIntegrim.Krijo);
        }
        #endregion elemente per integrim

        #region PAJISJE

        /// <summary>
        /// kthen datarow automjeti sipas idse
        /// </summary>
        ///<param name="idAuto"> id e automjetit</param>
        ///<returns> nje datarow qe permban automjetin sipas id se automjetit</returns>
        internal DataRow merrPajisjeSipasId(int idPajisje)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDPAJISJE", idPajisje, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_PAJISJE_merrSipasIdPajisje");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];
        }

        /// <summary>
        /// kthen datatable me automjetet e ndermarrjes
        /// </summary>
        ///<param name="idNdermarrje"> id e ndermarrjes</param>
        ///<returns> nje datatable qe permban automjetet e ndermarrjes</returns>
        internal DataTable merrPajisjetSipasNdermarrjes(int idNdermarrje)
        {

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDNDERMARJE", idNdermarrje, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_PAJISJE_merrSipasNdermarrjes");
            if (ds == null)
                return null;
            return ds.Tables[0];
        }

        /// <summary>
        /// kthen datatable me automjetet e ndermarrjes
        /// </summary>
        ///<param name="idNdermarrje"> id e ndermarrjes</param>
        ///<returns> nje datatable qe permban automjetet e ndermarrjes</returns>
        internal DataTable merrPajisjetSipasNdermarrjesAktive(int idNdermarrje)
        {

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDNDERMARJE", idNdermarrje, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_PAJISJE_merrSipasNdermarrjesAktive");
            if (ds == null)
                return null;
            return ds.Tables[0];
        }

        /// <summary>
        /// kthen datatable me automjetet e ndermarrjes
        /// </summary>
        ///<param name="idNdermarrje"> id e ndermarrjes</param>
        ///<returns> nje datatable qe permban automjetet e ndermarrjes</returns>
        internal DataTable merrLlojeKonvertimeXPajisje()
        {
            dbManager.Open();
            dbManager.CreateParameters(0);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_LLOJEKONVERTIMIPORTOKALLE_Sel");
            if (ds == null)
                return null;
            return ds.Tables[0];
        }

        internal clsMesazh modifikoPajisje(int idPajisje, string fjalekalimi, int idKlienti, bool regjistruar, bool aktive, int idPerdorues, double koeficentFitimi, double koeficentKonsumi)
        {
            dbManager.Open();
            dbManager.CreateParameters(8);
            dbManager.AddParameters(0, "@IDPAJISJE", idPajisje, ParameterDirection.Input);
            dbManager.AddParameters(1, "@FJALEKALIMI", fjalekalimi, ParameterDirection.Input);
            if (idKlienti <= 0)
                dbManager.AddParameters(2, "@IDKLIENTI", DBNull.Value, ParameterDirection.Input);
            else
                dbManager.AddParameters(2, "@IDKLIENTI", idKlienti, ParameterDirection.Input);
            dbManager.AddParameters(3, "@REGJISTRUAR", regjistruar, ParameterDirection.Input);
            dbManager.AddParameters(4, "@AKTIVE", aktive, ParameterDirection.Input);
            dbManager.AddParameters(5, "@IDPERDORUES", idPerdorues, ParameterDirection.Input);
            dbManager.AddParameters(6, "@KOEFICENTFITIMI", koeficentFitimi, ParameterDirection.Input);
            dbManager.AddParameters(7, "@KOEFICENTKONSUMI", koeficentKonsumi, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_PAJISJE_upd");
            return new clsMesazh(true, MessagesResource.Messages["msgModifikimiMeSukses"]);
        }

        internal bool ekzistonPajisjeAktiveDheRegjistruarSipasKoditDheKlientit(string kodi, int idKlienti, int idNdermarrje)
        {
            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@KODI", kodi, ParameterDirection.Input);
            dbManager.AddParameters(1, "@KLIENTI", idKlienti, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDNDERMARJE", idNdermarrje, ParameterDirection.Input);
            return Convert.ToBoolean(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_PAJISJE_ekzistonPajisjePerKlientDheKodRegjistruar"));
        }

        #endregion

        #region Seriale Unike

        internal clsMesazh ruajSerialeUnike(out int id, string kodi, string emertimi, int idLlojSeriali, string nrKaraktere, string formuleSpecifike, int idNdermarrje, int idPerdoruesi, int idStatusDok, bool aktiv, bool serialKryesor)
        {
            id = 0;
            dbManager.Open();
            dbManager.CreateParameters(11);
            dbManager.AddParameters(0, "@ID", id, ParameterDirection.Output);
            dbManager.AddParameters(1, "@KODI", kodi, ParameterDirection.Input);
            dbManager.AddParameters(2, "@EMERTIMI", emertimi, ParameterDirection.Input);
            dbManager.AddParameters(3, "@IDLLOJ_SERIALE", idLlojSeriali, ParameterDirection.Input);
            dbManager.AddParameters(4, "@NR_KARAKTERE", nrKaraktere, ParameterDirection.Input);
            dbManager.AddParameters(5, "@FORMULE_SPECIFIKE", formuleSpecifike, ParameterDirection.Input);
            dbManager.AddParameters(6, "@IDNDERMARJE", idNdermarrje, ParameterDirection.Input);
            dbManager.AddParameters(7, "@IDPERDORUESI", idPerdoruesi, ParameterDirection.Input);
            dbManager.AddParameters(8, "@IDStatusdok", idStatusDok, ParameterDirection.Input);
            dbManager.AddParameters(9, "@aktiv", aktiv, ParameterDirection.Input);
            dbManager.AddParameters(10, "@serial_kryesor", serialKryesor, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_SERIALEUNIKE_INS");
            id = int.Parse(dbManager.Parameters[0].Value.ToString());
            return new clsMesazh(true, "Ruatja perfundoi me sukses!");
        }

        internal clsMesazh modifikoSerialeUnike(int id, string emertimi, int idLlojSeriali, string nrKaraktere, string formuleSpecifike, int idPerdoruesi, int idstatusdok, bool aktiv, bool serialKryesor)
        {
            dbManager.Open();
            dbManager.CreateParameters(9);
            dbManager.AddParameters(0, "@ID", id, ParameterDirection.Input);
            dbManager.AddParameters(1, "@EMERTIMI", emertimi, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDLLOJ_SERIALE", idLlojSeriali, ParameterDirection.Input);
            dbManager.AddParameters(3, "@NR_KARAKTERE", nrKaraktere, ParameterDirection.Input);
            dbManager.AddParameters(4, "@FORMULE_SPECIFIKE", formuleSpecifike, ParameterDirection.Input);
            dbManager.AddParameters(5, "@IDPERDORUESI", idPerdoruesi, ParameterDirection.Input);
            dbManager.AddParameters(6, "@idstatusdok", idstatusdok, ParameterDirection.Input);
            dbManager.AddParameters(7, "@aktiv", aktiv, ParameterDirection.Input);
            dbManager.AddParameters(8, "@serial_kryesor", serialKryesor, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_SERIALEUNIKE_UPD");
            return new clsMesazh(true, MessagesResource.Messages["msgModifikimiMeSukses"]);
        }

        internal clsMesazh fshiSerialeUnike(int id)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@ID", id, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_SERIALEUNIKE_DEL");
            return new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
        }

        internal void merrSerialinUnik(string kodi, int idNdermarje, clsSerialeUnike clsSerialeUnike)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddInputParameters("@KODI", kodi);
            dbManager.AddInputParameters("@IDNDERMARJE", idNdermarje);
            dbManager.FillObject("prc_T_SERIALEUNIKE_SEL_SIPASKODIT", clsSerialeUnike);
        }


        internal void mbushSerialeUnikeSipasFormatit(int idFormati, colSerialeUnike colSerialeUnike)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddInputParameters("@ID_FORMATI", idFormati); //behej me pare lidhja me idKategori, tani ndermjetesohet nga idFormati
            dbManager.FillCollection("prc_T_SERIALEUNIKE_SEL_SIPASFORMATIT", colSerialeUnike);
        }

        internal void mbushSerialeSipasNdermarrje(int idNdermarrja, IDataBaseReader colSerialeUnike)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddInputParameters("@IDNDERMARJE", idNdermarrja);
            dbManager.FillCollection("prc_T_SERIALEUNIKE_SEL_SIPASNDERMARRJE", colSerialeUnike);
        }
        internal void mbushSerialeSipasID(int id, IDataBaseReader clsSerialeUnike)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddInputParameters("@ID", id);
            dbManager.FillObject("prc_T_SERIALEUNIKE_SEL_SIPASID", clsSerialeUnike);
        }
        internal DataTable mbushSerialeSipasNdermarrjeDT(int idNdermarrja)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddInputParameters("@IDNDERMARJE", idNdermarrja);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_SERIALEUNIKE_DT"); if (ds == null)
                return null;
            return ds.Tables[0];
        }
        internal DataRow mbushSerialeSipasNdermarrjeDR(int id)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddInputParameters("@ID", id);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_SERIALEUNIKE_DR");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];

        }

        #endregion

        #region Seriale Unike Fusha

        internal void mbushSerialeUnikeFusha(IDataBaseReader colSerialeUnikeFusha)
        {
            dbManager.Open();
            dbManager.CreateParameters(0);
            dbManager.FillCollection("prc_T_SERIALEUNIKEFUSHA", colSerialeUnikeFusha);
        }


        internal void mbushSerialeFushSipasFushes(string fusha, IDataBaseReader clsSerialeUnikeFusha)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddInputParameters("FUSHA", fusha);
            dbManager.FillObject("prc_T_SERIALEUNIKEFUSHA_SIPASFUSHES", clsSerialeUnikeFusha);
        }


        #endregion

        #region Seriale Unike Kategori

        internal clsMesazh ruajSerialeUnikeKategori(out int id, string kategori, string pershkrimi, string tipFormati, string simboliNdares, int idNdermarje, int idPerdoruesi, int idstatusdok, bool meEmertimKolone)
        {
            id = 0;
            dbManager.Open();
            dbManager.CreateParameters(9);
            dbManager.AddParameters(0, "@ID", id, ParameterDirection.Output);
            dbManager.AddParameters(1, "@KATEGORI", kategori, ParameterDirection.Input);
            dbManager.AddParameters(2, "@PERSHKRIM", pershkrimi, ParameterDirection.Input);
            dbManager.AddParameters(3, "@TIPFORMATI", tipFormati, ParameterDirection.Input);
            dbManager.AddParameters(4, "@SIMBOLI_NDARES", simboliNdares, ParameterDirection.Input);
            dbManager.AddParameters(5, "@IDNDERMARJE", idNdermarje, ParameterDirection.Input);
            dbManager.AddParameters(6, "@IDPERDORUESI", idPerdoruesi, ParameterDirection.Input);
            dbManager.AddParameters(7, "@IDSTATUSDOK", idstatusdok, ParameterDirection.Input);
            dbManager.AddParameters(8, "@MEEMERTIMKOLONE", meEmertimKolone, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_SERIALEUNIKE_KATEGORI_INS");
            id = int.Parse(dbManager.Parameters[0].Value.ToString());
            return new clsMesazh(true, "Ruatja perfundoi me sukses!");
        }


        internal clsMesazh modifikoSerialeUnikeKategori(int id, string pershkrimi, string tipFormati, string simboliNdares, int idPerdoruesi, bool meEmertimKolone)
        {
            dbManager.Open();
            dbManager.CreateParameters(6);
            dbManager.AddParameters(0, "@ID", id, ParameterDirection.Input);
            dbManager.AddParameters(1, "@PERSHKRIM", pershkrimi, ParameterDirection.Input);
            dbManager.AddParameters(2, "@TIPFORMATI", tipFormati, ParameterDirection.Input);
            dbManager.AddParameters(3, "@SIMBOLI_NDARES", simboliNdares, ParameterDirection.Input);
            dbManager.AddParameters(4, "@IDPERDORUESI", idPerdoruesi, ParameterDirection.Input);
            dbManager.AddParameters(5, "@MEEMERTIMKOLONE", meEmertimKolone, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_SERIALEUNIKE_KATEGORI_UPD");
            return new clsMesazh(true, MessagesResource.Messages["msgModifikimiMeSukses"]);
        }


        internal clsMesazh FshiSerialeUnikeKategori(int id)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@ID", id, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_SERIALEUNIKE_KATEGORI_DEL");
            return new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
        }

        internal void mbushSerialeKategoriSipasNdermarrje(int idNdermarrje, colSerialeUnikeKategori colSerialeUnikeKategori)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddInputParameters("@IDNDERMARJE", idNdermarrje);
            dbManager.FillCollection("prc_T_SERIALEUNIKE_KATEGORI_SEL_SIPASNDERMARRJE", colSerialeUnikeKategori);
        }

        internal void merrSerialeKategori(string kategori, int idNdermarje, clsSerialeUnikeKategori clsSerialeUnikeKategori)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddInputParameters("@KATEGORI", kategori);
            dbManager.AddInputParameters("@IDNDERMARJE", idNdermarje);
            dbManager.FillObject("prc_T_SERIALEUNIKE_KATEGORI_SEL_SIPASKATEGORI", clsSerialeUnikeKategori);
        }
        internal void mbushSerialeKategoriSipasID(int id, IDataBaseReader clsSerialeUnikekategori)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddInputParameters("@ID", id);
            dbManager.FillObject("prc_T_SERIALEUNIKE_KATEGORI_SEL_SIPASid", clsSerialeUnikekategori);
        }
        internal DataTable mbushSerialeKategoriSipasNdermarrjeDT(int idNdermarrja)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddInputParameters("@IDNDERMARJE", idNdermarrja);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_SERIALEUNIKE_KATEGORI_Dt"); if (ds == null)
                return null;
            return ds.Tables[0];
        }
        internal DataRow mbushSerialeKategoriSipasNdermarrjeDR(int id)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddInputParameters("@ID", id);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_SERIALEUNIKE_KATEGORI_Dr");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];

        }
        #endregion

        #region Seriale Unike Formate

        internal clsMesazh ruajSerialeUnikeFormat(out int id, string kodi, string pershkrimi, int idKategoriSeriali, int idNdermarje, int idPerdoruesi, bool aktiv)
        {
            id = 0;
            dbManager.Open();
            dbManager.CreateParameters(7);
            dbManager.AddParameters(0, "@ID", id, ParameterDirection.Output);
            dbManager.AddParameters(1, "@KODI", kodi, ParameterDirection.Input);
            dbManager.AddParameters(2, "@PERSHKRIM", pershkrimi, ParameterDirection.Input);
            dbManager.AddParameters(3, "@IDKATEGORI", idKategoriSeriali, ParameterDirection.Input);
            dbManager.AddParameters(4, "@IDNDERMARJE", idNdermarje, ParameterDirection.Input);
            dbManager.AddParameters(5, "@IDPERDORUESI", idPerdoruesi, ParameterDirection.Input);
            dbManager.AddParameters(6, "@AKTIV", aktiv, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_SERIALEUNIKE_FORMATE_INS");
            id = int.Parse(dbManager.Parameters[0].Value.ToString());
            return new clsMesazh(true, "Ruatja perfundoi me sukses!");
        }

        internal clsMesazh modifikoSerialeUnikeFormat(int id, string pershkrimi, int idKategoriSeriali, int idPerdoruesi, bool aktiv)
        {
            dbManager.Open();
            dbManager.CreateParameters(5);
            dbManager.AddParameters(0, "@ID", id, ParameterDirection.Input);
            dbManager.AddParameters(1, "@PERSHKRIM", pershkrimi, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDKATEGORI", idKategoriSeriali, ParameterDirection.Input);
            dbManager.AddParameters(3, "@IDPERDORUESI", idPerdoruesi, ParameterDirection.Input);
            dbManager.AddParameters(4, "@AKTIV", aktiv, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_SERIALEUNIKE_FORMATE_UPD");
            return new clsMesazh(true, MessagesResource.Messages["msgModifikimiMeSukses"]);
        }

        internal clsMesazh fshiSerialeUnikeFormat(int id)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@ID", id, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_SERIALEUNIKE_FORMATE_DEL");
            return new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
        }

        internal void merrSerialeFormate(string kodi, int idNdermarje, clsSerialeUnikeFormate clsSerialeUnikeFormate)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddInputParameters("@KODI", kodi);
            dbManager.AddInputParameters("@IDNDERMARJE", idNdermarje);
            dbManager.FillObject("prc_T_SERIALEUNIKE_FORMATE_SEL_SIPASKODI", clsSerialeUnikeFormate);
        }

        internal void mbushSerialeFormateSipasNdermarrje(int idNdermarrje, colSerialeUnikeFormate colSerialeUnikeFormate)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddInputParameters("@IDNDERMARJE", idNdermarrje);
            dbManager.FillCollection("prc_T_SERIALEUNIKE_FORMATE_SEL_SIPASNDERMARRJE", colSerialeUnikeFormate);
        }

        internal DataRow mbushSerialeFormateSipasNdermarrjeDR(int id)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddInputParameters("@ID", id);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_SERIALEUNIKE_FORMATE_Dr");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];
        }

        internal DataTable mbushSerialeFormateSipasNdermarrjeDT(int idNdermarrja)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddInputParameters("@IDNDERMARJE", idNdermarrja);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_SERIALEUNIKE_FORMATE_Dt");
            if (ds == null)
                return null;
            return ds.Tables[0];
        }

        internal void mbushFormateSipasKategorise(int idKategori, colSerialeUnikeFormate colSerialeUnikeFormate)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddInputParameters("@IDKATEGORI", idKategori);
            dbManager.FillCollection("prc_T_SERIALEUNIKE_FORMATE_SEL_SIPASKATEGORI", colSerialeUnikeFormate);
        }

        #endregion

        #region Seriale Unike X Format (emri tabeles ka ngelur T_SERIALEUNIKE_X_KATEGORI)

        internal clsMesazh ruajLidhjeSerialeUnikeFormat(int id, int idKategori)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@ID_KATEGORI", idKategori, ParameterDirection.Input);
            dbManager.AddParameters(1, "@ID_SERIALUNIKE", id, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_SERIALEUNIKE_X_KATEGORI_INS");
            return new clsMesazh(true, "Ruatja perfundoi me sukses!");
        }

        internal clsMesazh fshiGjitheLidhjeSerialeUnikeFormat(int idFormat)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@ID_KATEGORI", idFormat, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_SERIALEUNIKE_X_KATEGORI_DEL");
            return new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
        }

        internal void mbushSerialeFormateSipasID(int id, IDataBaseReader clsSerialeUnikeFormate)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddInputParameters("@ID", id);
            dbManager.FillObject("prc_T_SERIALEUNIKE_FORMATE_SEL_SIPASid", clsSerialeUnikeFormate);
        }

        #endregion

        #region Seriale Unike Fusha X Kategoria
        internal clsMesazh ruajLidhjeSerialeFushaKategori(int id, int idKategori, string emertimi)
        {
            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddInputParameters("@ID_FUSHA", id);
            dbManager.AddInputParameters("@ID_KATEGORI", idKategori);
            dbManager.AddInputParameters("@EMERTIMI", emertimi);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_SERIALEUNIKE_FUSHA_X_KATEGORI_INS");
            return new clsMesazh(true, "Ruatja perfundoi me sukses!");
        }

        internal clsMesazh fshiGjitheLidhjeSerialeFushaImportiKategori(int idKategori)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddInputParameters("@ID_KATEGORI", idKategori);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_SERIALEUNIKE_FUSHA_X_KATEGORI_DEL");
            return new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
        }


        internal void mbushFushaImportiSipasKategori(int idKategori, colSerialeUnikeFusha colSerialeUnikeFusha)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddInputParameters("@ID_KATEGORI", idKategori);
            dbManager.FillCollection("prc_T_SERIALEUNIKE_FUSHA_SEL_SIPASKATEGORI", colSerialeUnikeFusha);
        }


        #endregion

        #region Seriale Unike Magazine

        internal clsMesazh ruajSerialeUnikeMagazine(out int id, int idTrupiMagazine, int idKokaMagazine, int idLlojDokumentMagazine, int idKategoriSeriali, int idFormatSeriali, int idArtikulli, int idTVSH, float cmimi, float sasia, int idSeti, string serialiKryesore, bool shfaqSerialKryesorNeGride)
        {
            id = 0;
            dbManager.Open();
            dbManager.CreateParameters(13);
            dbManager.AddParameters(0, "@ID", id, ParameterDirection.Output);
            dbManager.AddParameters(1, "@ID_TRUPI_MAGAZINE", idTrupiMagazine, ParameterDirection.Input);
            dbManager.AddParameters(2, "@ID_KOKA_MAGAZINE", idKokaMagazine, ParameterDirection.Input);
            dbManager.AddParameters(3, "@ID_KATEGORI_SERIALI", idKategoriSeriali, ParameterDirection.Input);
            dbManager.AddParameters(4, "@ID_FORMAT_SERIALI", idFormatSeriali, ParameterDirection.Input);
            dbManager.AddParameters(5, "@ID_ARTIKULLI", idArtikulli, ParameterDirection.Input);
            dbManager.AddParameters(6, "@IDLLOJDOKUMENTIMAGAZINE", idLlojDokumentMagazine, ParameterDirection.Input);
            dbManager.AddParameters(7, "@CMIMI", cmimi, ParameterDirection.Input);
            if (idTVSH > 0)
                dbManager.AddParameters(8, "@ID_TVSH", idTVSH, ParameterDirection.Input);
            else
                dbManager.AddParameters(8, "@ID_TVSH", DBNull.Value, ParameterDirection.Input);
            dbManager.AddParameters(9, "@SASIA", sasia, ParameterDirection.Input);
            dbManager.AddParameters(10, "@SERIALI_KRYESOR", serialiKryesore, ParameterDirection.Input);
            dbManager.AddParameters(11, "@ID_SETI", idSeti, ParameterDirection.Input);
            dbManager.AddParameters(12, "@SHFAQSERIALKRYESORNEGRIDE", shfaqSerialKryesorNeGride, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_SERIALEUNIKE_LIDHJE_MAGAZINE_INS_ARTIKULL");
            id = int.Parse(dbManager.Parameters[0].Value.ToString());
            return new clsMesazh(true, "Ruatja perfundoi me sukses!");
        }

        internal clsMesazh ruajSerialeUnikeMagazine(out int id, int idTrupiMagazine, int idKokaMagazine, int idLlojDokumentMagazine, int idKategoriSeriali, int idFormatSeriali, int idArtikulli, int idTVSH,
            float cmimi, float sasia, int idSeti, string serialiKryesore, string serialiDytesor, string cardSerialNo, string phoneSerialNo, string userCode, string airtime)
        {
            id = 0;
            dbManager.Open();
            dbManager.CreateParameters(17);
            dbManager.AddParameters(0, "@ID", id, ParameterDirection.Output);
            dbManager.AddParameters(1, "@ID_TRUPI_MAGAZINE", idTrupiMagazine, ParameterDirection.Input);
            dbManager.AddParameters(2, "@ID_KOKA_MAGAZINE", idKokaMagazine, ParameterDirection.Input);
            dbManager.AddParameters(3, "@ID_KATEGORI_SERIALI", idKategoriSeriali, ParameterDirection.Input);
            dbManager.AddParameters(4, "@ID_FORMAT_SERIALI", idFormatSeriali, ParameterDirection.Input);
            dbManager.AddParameters(5, "@ID_ARTIKULLI", idArtikulli, ParameterDirection.Input);
            dbManager.AddParameters(6, "@IDLLOJDOKUMENTIMAGAZINE", idLlojDokumentMagazine, ParameterDirection.Input);
            dbManager.AddParameters(7, "@CMIMI", cmimi, ParameterDirection.Input);
            dbManager.AddParameters(8, "@ID_TVSH", idTVSH, ParameterDirection.Input);
            dbManager.AddParameters(9, "@SASIA", sasia, ParameterDirection.Input);
            dbManager.AddParameters(10, "@SERIALI_KRYESOR", serialiKryesore, ParameterDirection.Input);
            dbManager.AddParameters(11, "@ID_SETI", idSeti, ParameterDirection.Input);
            dbManager.AddParameters(12, "@SERIALI_DYTESOR", serialiDytesor, ParameterDirection.Input);
            dbManager.AddParameters(13, "@CARDSERIALNO", cardSerialNo, ParameterDirection.Input);
            dbManager.AddParameters(14, "@PHONESERIALNO", phoneSerialNo, ParameterDirection.Input);
            dbManager.AddParameters(15, "@USERCODE", userCode, ParameterDirection.Input);
            dbManager.AddParameters(16, "@AIRTIME", airtime, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_SERIALEUNIKE_LIDHJE_MAGAZINE_INS_KARTA");
            id = int.Parse(dbManager.Parameters[0].Value.ToString());
            return new clsMesazh(true, "Ruatja perfundoi me sukses!");
        }

        internal clsMesazh ruajSerialeUnikeMagazine(out int id, int idTrupiMagazine, int idKokaMagazine, int idLlojDokumentMagazine, int idKategoriSeriali, int idFormatSeriali, int idArtikulli, int idTVSH,
            float cmimi, float sasia, int idSeti, string serialiKryesore, string shitBatch, string batchPerPack, string cardsPerBatch, string cardPartNo)
        {
            id = 0;
            dbManager.Open();
            dbManager.CreateParameters(16);
            dbManager.AddParameters(0, "@ID", id, ParameterDirection.Output);
            dbManager.AddParameters(1, "@ID_TRUPI_MAGAZINE", idTrupiMagazine, ParameterDirection.Input);
            dbManager.AddParameters(2, "@ID_KOKA_MAGAZINE", idKokaMagazine, ParameterDirection.Input);
            dbManager.AddParameters(3, "@ID_KATEGORI_SERIALI", idKategoriSeriali, ParameterDirection.Input);
            dbManager.AddParameters(4, "@ID_FORMAT_SERIALI", idFormatSeriali, ParameterDirection.Input);
            dbManager.AddParameters(5, "@ID_ARTIKULLI", idArtikulli, ParameterDirection.Input);
            dbManager.AddParameters(6, "@IDLLOJDOKUMENTIMAGAZINE", idLlojDokumentMagazine, ParameterDirection.Input);
            dbManager.AddParameters(7, "@CMIMI", cmimi, ParameterDirection.Input);
            dbManager.AddParameters(8, "@ID_TVSH", idTVSH, ParameterDirection.Input);
            dbManager.AddParameters(9, "@SASIA", sasia, ParameterDirection.Input);
            dbManager.AddParameters(10, "@SERIALI_KRYESOR", serialiKryesore, ParameterDirection.Input);
            dbManager.AddParameters(11, "@ID_SETI", idSeti, ParameterDirection.Input);
            dbManager.AddParameters(12, "@SHITBATCH", shitBatch, ParameterDirection.Input);
            dbManager.AddParameters(13, "@BATCHPERPACK", batchPerPack, ParameterDirection.Input);
            dbManager.AddParameters(14, "@CARDSPERBATCH", cardsPerBatch, ParameterDirection.Input);
            dbManager.AddParameters(15, "@CARDPARTNO", cardPartNo, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_SERIALEUNIKE_LIDHJE_MAGAZINE_INS_RINGARKUES");
            id = int.Parse(dbManager.Parameters[0].Value.ToString());
            return new clsMesazh(true, "Ruatja perfundoi me sukses!");
        }

        internal void mbushSerialeUnikeLidhjeMagSipasIdKokaMag(int idKokaMagazina, colSerialeUnikeMagazina colSerialeUnikeMagazina)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddInputParameters("@ID_KOKA_MAGAZINE", idKokaMagazina);
            dbManager.FillCollection("prc_T_SERIALEUNIKE_LIDHJE_MAGAZINE_KTHE_SERIALE_ID_KOKA_MAGAZINA", colSerialeUnikeMagazina);
        }

        internal DataTable MerrGjendjeSerialiUnikRingarkues(string seriali, bool serialKryesor, DateTime date, int idNdermarrje, int idDok)
        {
            dbManager.Open();
            dbManager.CreateParameters(5);
            dbManager.AddParameters("@SERIALI", seriali, ParameterDirection.Input);
            dbManager.AddParameters("@IDNDERMARRJE", idNdermarrje, ParameterDirection.Input);
            dbManager.AddParameters("@DATE", date, ParameterDirection.Input);
            dbManager.AddParameters("@SERIALKRYESOR", serialKryesor, ParameterDirection.Input);
            dbManager.AddParameters("@IDDOK", idDok, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_SERIALEUNIKE_LIDHJE_MAGAZINE_KONTROLLOGJENDJE_RINGARKUES");
            return ds.Tables[0];
        }

        internal DataTable MerrGjendjeSerialiUnik(string seriali, bool serialKryesor, DateTime date, int idNdermarrje, int idDok)
        {
            dbManager.Open();
            dbManager.CreateParameters(5);
            dbManager.AddParameters("@SERIALI", seriali, ParameterDirection.Input);
            dbManager.AddParameters("@IDNDERMARRJE", idNdermarrje, ParameterDirection.Input);
            dbManager.AddParameters("@DATE", date, ParameterDirection.Input);
            dbManager.AddParameters("@SERIALKRYESOR", serialKryesor, ParameterDirection.Input);
            dbManager.AddParameters("@IDDOK", idDok, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_SERIALEUNIKE_LIDHJE_MAGAZINE_KONTROLLOGJENDJE");
            return ds.Tables[0];
        }
        internal DataTable MerrSerialPerKthim(string seriali, string idtrupa, int idDok)
        {
            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters("@SERIALI", seriali, ParameterDirection.Input);
            dbManager.AddParameters("@IDTRUPIS", idtrupa, ParameterDirection.Input);
            dbManager.AddParameters("@IDKOKASHITJE", idDok, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_SERIALEUNIKE_LIDHJE_MAGAZINE_MERRSERIALPERKTHIM");
            return ds.Tables[0];
        }
        internal DataTable MerrSerialPerKthimNgaDetajim(string seriali, int idDok)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters("@SERIALI", seriali, ParameterDirection.Input);
            dbManager.AddParameters("@IDKOKASHITJE", idDok, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_SERIALEUNIKE_LIDHJE_MAGAZINE_MERRSERIALPERKTHIMNGADETAJIM");
            return ds.Tables[0];
        }
        internal DataTable MerrSerialetPerKthimNeRradhe(string seriali, string idtrupa, int idDok, int sasia, string serialetNeGride)
        {
            dbManager.Open();
            dbManager.CreateParameters(5);
            dbManager.AddParameters("@SERIALI", seriali, ParameterDirection.Input);
            dbManager.AddParameters("@IDTRUPIS", idtrupa, ParameterDirection.Input);
            dbManager.AddParameters("@IDKOKASHITJE", idDok, ParameterDirection.Input);
            dbManager.AddParameters("@SASIA", sasia, ParameterDirection.Input);
            dbManager.AddParameters("@SERIALETNEGRIDE", serialetNeGride, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_SERIALEUNIKE_LIDHJE_MAGAZINE_MERRSERIALETPERKTHIM_NE_RRADHE");
            return ds.Tables[0];
        }
        internal string MerrSerialinEPareNeRradhe(int idArtikulli, bool serialKryesor, DateTime date, int idNdermarrje, int idDok)
        {
            dbManager.Open();
            dbManager.CreateParameters(5);
            dbManager.AddParameters("@IDARTIKULLI", idArtikulli, ParameterDirection.Input);
            dbManager.AddParameters("@IDNDERMARRJE", idNdermarrje, ParameterDirection.Input);
            dbManager.AddParameters("@DATE", date, ParameterDirection.Input);
            dbManager.AddParameters("@SERIALKRYESOR", serialKryesor, ParameterDirection.Input);
            dbManager.AddParameters("@IDDOK", idDok, ParameterDirection.Input);
            return Convert.ToString(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_SERIALEUNIKE_LIDHJE_MAGAZINE_MerrSerialinERradhes"));
        }

        internal DataTable MerrSerialetNeRradhe(int idArtikulli, bool serialKryesor, string date, int idNdermarrje, string seriali, int sasia, string serialetNeGride, int idMag, int idDok)
        {
            dbManager.Open();
            dbManager.CreateParameters(9);
            dbManager.AddParameters("@IDARTIKULLI", idArtikulli, ParameterDirection.Input);
            dbManager.AddParameters("@IDNDERMARRJE", idNdermarrje, ParameterDirection.Input);
            dbManager.AddParameters("@IDMAG", idMag, ParameterDirection.Input);
            dbManager.AddParameters("@DATE", date, ParameterDirection.Input);
            dbManager.AddParameters("@SERIALKRYESOR", serialKryesor, ParameterDirection.Input);
            dbManager.AddParameters("@SASIA", sasia, ParameterDirection.Input);
            dbManager.AddParameters("@SERIALETNEGRIDE", serialetNeGride, ParameterDirection.Input);
            dbManager.AddParameters("@SERIALI", seriali, ParameterDirection.Input);
            dbManager.AddParameters("@IDDOK", idDok, ParameterDirection.Input);
            var ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_SERIALEUNIKE_LIDHJE_MAGAZINE_MerrSerialetNeRradhe");
            return ds.Tables[0];
        }
        internal DataTable MerrSerialetNeRradheRingarkues(int idArtikulli, bool serialKryesor, string date, int idNdermarrje, string seriali, int sasia, string serialetNeGride, int idMag, int llojSeriali, int idDok)
        {
            dbManager.Open();
            dbManager.CreateParameters(10);
            dbManager.AddParameters("@IDARTIKULLI", idArtikulli, ParameterDirection.Input);
            dbManager.AddParameters("@IDNDERMARRJE", idNdermarrje, ParameterDirection.Input);
            dbManager.AddParameters("@IDMAG", idMag, ParameterDirection.Input);
            dbManager.AddParameters("@DATE", date, ParameterDirection.Input);
            dbManager.AddParameters("@SERIALKRYESOR", serialKryesor, ParameterDirection.Input);
            dbManager.AddParameters("@SASIA", sasia, ParameterDirection.Input);
            dbManager.AddParameters("@SERIALETNEGRIDE", serialetNeGride, ParameterDirection.Input);
            dbManager.AddParameters("@SERIALI", seriali, ParameterDirection.Input);
            dbManager.AddParameters("@LLOJ_SERIALI", llojSeriali, ParameterDirection.Input);
            dbManager.AddParameters("@IDDOK", idDok, ParameterDirection.Input);
            var ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_SERIALEUNIKE_LIDHJE_MAGAZINE_MerrSerialetNeRradheRingarkues");
            return ds.Tables[0];
        }

        internal DataTable MerrRaportinGjendjaEArtikujveMeSeriale(int idNdermarrje, string dtMbarimi, string filter, string filterNrSeriale)
        {
            dbManager.Open();
            dbManager.CreateParameters(4);
            dbManager.AddInputParameters("@idndermarje", idNdermarrje);
            dbManager.AddInputParameters("@filterDtDok2", dtMbarimi);
            dbManager.AddInputParameters("@filter", filter);
            dbManager.AddInputParameters("@filterNrSeriale", filterNrSeriale);

            return dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_SERIALEUNIKE_LIDHJE_MAGAZINE_GjendjaEArtikujveMeSeriale").Tables[0];
        }

        #endregion

        #region Gjendja e Artikujve me IMEI
        internal DataTable MerrRaportinGjendjaEArtikujveMeImei(string rapEmriReal, int idNdermarrje, int idPerdoruesi, string dtFillimi, string dtMbarimi, string dtDok, string dtRegjistrimi, string filter)
        {
            dbManager.Open();
            dbManager.CreateParameters(21);
            dbManager.AddInputParameters("@IdNdermarje", idNdermarrje);
            dbManager.AddInputParameters("@idPerdoruesi", idPerdoruesi);
            dbManager.AddInputParameters("@filterDtDok", dtDok);
	        dbManager.AddInputParameters("@filterDtDok1", dtFillimi);
	        dbManager.AddInputParameters("@filterDtDok2", dtMbarimi);
	        dbManager.AddInputParameters("@filterDtRegj", dtRegjistrimi);
            dbManager.AddInputParameters("@filter", filter);
            dbManager.AddInputParameters("@filterNumerLlogarie", string.Empty);
	        dbManager.AddInputParameters("@filterFurnitor", string.Empty);
	        dbManager.AddInputParameters("@filterkodifikimartP", string.Empty);
	        dbManager.AddInputParameters("@filterkodifikimartD", string.Empty);
	        dbManager.AddInputParameters("@filterMagazina", string.Empty);
	        dbManager.AddInputParameters("@filterKartela", string.Empty);
	        dbManager.AddInputParameters("@IdRaport", string.Empty);
	        dbManager.AddInputParameters("@filterNjesiArtikulli", string.Empty);
	        dbManager.AddInputParameters("@filterFurnitorArt", string.Empty);
	        dbManager.AddInputParameters("@filterDegeAdministrative", string.Empty);
	        dbManager.AddInputParameters("@filterDetajimP", string.Empty);
	        dbManager.AddInputParameters("@filterkompania", string.Empty);
	        dbManager.AddInputParameters("@filterAfishoArtikuj", string.Empty);
	        dbManager.AddInputParameters("@filterStatusShperndarje", string.Empty);

            if(rapEmriReal == "gjendjaArtikujveIMEIEkspozitor")
                return dbManager.ExecuteDataSet(CommandType.StoredProcedure, "PRC_RAP_MAGAZINAGJENDJAMAGAZINESSIPASDET_EXP").Tables[0];
            else
                return dbManager.ExecuteDataSet(CommandType.StoredProcedure, "PRC_RAP_MAGAZINAGJENDJAMAGAZINESSIPASDET2").Tables[0];
        }
        #endregion
    }
}