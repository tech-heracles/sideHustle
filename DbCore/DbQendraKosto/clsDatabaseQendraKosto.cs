using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DbCore.IMBUtils.DataBase;


namespace DbCore.DbQendraKosto
{

    /// <summary>
    ///  Kjo eshte klasa me e rendesishme e ketij moduli. Eshte nje klase e tille per secilin modul te projektit.
    ///  Ketu ndodhen metodat qe therriten  nga te gjithe objektet brenda projektit DbQendraKosto
    ///  secila metode permban thirjet e Store procedures ne DB, duke i kaluar parametrat perkates
    ///  Metodat jane te ndara ne Regions sipas objekteve qe i therrasin keto metoda
    /// </summary>
    public class clsDatabaseQendraKosto : DbData
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public clsDatabaseQendraKosto() : base() { }
        public clsDatabaseQendraKosto(DbData db) : base(db) { }
        public clsDatabaseQendraKosto(string connectionName) : base(connectionName) { }

        /// <summary>
        /// Metodat e meposhtme i perkasin veprimeve qe kryhen nga objektet clsLlogariShperndarjeQK dhe colLlogariShperndarjeQK
        /// </summary>
        #region LLOGARI SHPERNDARJE QENDRA KOSTO

        /// <summary>
        /// ekzekuton prc_T_LLOGARISHPERNDARJEQK_ins ne DB dhe kthen statusin e perfundimit te ekzekutimit te SP-se perkatese
        /// Shiko clsMesazh brenda ketij moduli per informacion te metejshem mbi vleren e kthimit te kesaj metode
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="idkpf">id e kpf</param>
        /// <param name="idndermarje">id e ndermarjes</param>
        /// <param name="idperdoruesi"> id e perdoruesit</param>
        /// <returns> nje objekt clsMesazh qe tregon nese ruajtja eshte kryer ne rregull apo jo.</returns>
        internal clsMesazh ruajLlogariShperndarjeQK(out int id, int idkpf, int idndermarje, int idperdoruesi)
        {
            id = -1;

            dbManager.Open();
            dbManager.CreateParameters(4);
            dbManager.AddParameters(0, "@IDLLOGSHPERNDARJE", id, ParameterDirection.Output);
            dbManager.AddParameters(1, "@IDKPF", idkpf, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDNDERMARJE", idndermarje, ParameterDirection.Input);
            dbManager.AddParameters(3, "@IDPERDORUESI", idperdoruesi, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_LLOGARISHPERNDARJEQK_ins");
            id = int.Parse(dbManager.Parameters[0].Value.ToString());
            clsMesazh mesazh = new clsMesazh(true, mesazhRuajtje);
            return mesazh;
        }

        /// <summary>
        /// ekzekutohet sp-ja prc_T_LLOGARISHPERNDARJEQK_del duke i kaluar id e llogarise se shperndarjes
        /// </summary>
        /// <param name="id"> id e llogarise se shpernarjes </param>
        /// <returns>nje objekt clsMesazh qe tregon nese fshirja eshte kryer ne rregull apo jo</returns>
        internal clsMesazh fshiLlogariShperndarjeQK(int id)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDLLOGSHPERNDARJE", id, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_LLOGARISHPERNDARJEQK_del");
            clsMesazh mesazh = new clsMesazh(true, mesazhFshirje);
            return mesazh;
        }

        /// <summary>
        /// ekzekutohet sp-ja prc_T_LLOGARISHPERNDARJEQK_delSipasNdermarjes duke i kaluar id e ndermarjes
        /// </summary>
        /// <param name="idndermarje"> id e ndermarjes </param>
        /// <returns>nje objekt clsMesazh qe tregon nese fshirja eshte kryer ne rregull apo jo</returns>
        internal clsMesazh fshiLlogariShperndarjeQKSipasIdNdermarje(int idndermarje)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDNDERMARJE", idndermarje, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_LLOGARISHPERNDARJEQK_delSipasNdermarjes");
            clsMesazh mesazh = new clsMesazh(true, mesazhFshirje);
            return mesazh;
        }

        /// <summary>
        /// ekzekutohet sp-ja prc_T_LLOGARISHPERNDARJEQK_upd 
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="idkpf">id e kpf</param>
        /// <param name="idndermarje">id e ndermarjes</param>
        /// <param name="idperdoruesi"> id e perdoruesit</param>
        /// <returns>nje objekt clsMesazh qe tregon nese modifikimi eshte kryer ne rregull apo jo</returns>
        internal clsMesazh modifikoLlogariShperndarjeQK(int id, int idkpf, int idndermarje, int idperdoruesi)
        {
            dbManager.Open();
            dbManager.CreateParameters(4);
            dbManager.AddParameters(0, "@IDLLOGSHPERNDARJE", id, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDKPF", idkpf, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDNDERMARJE", idndermarje, ParameterDirection.Input);
            dbManager.AddParameters(3, "@IDPERDORUESI", idperdoruesi, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_LLOGARISHPERNDARJEQK_upd");
            clsMesazh mesazh = new clsMesazh(true, mesazhModifikimi);
            return mesazh;
        }

        /// <summary>
        /// kthen llogarishperndarje qk sipas ndermarjes
        /// </summary>
        /// <param name="idndermarje">id e ndermarjes</param>
        ///<returns>nje objekt llogarite e shperndarjes qk sipas ndermarjes</returns>
        internal DataTable ktheLlogariShpernarjeQKSipasNdermarjes(int idndermarje)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDNDERMARJE", idndermarje, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_LLOGARISHPERNDARJEQK_merrSipasIdNdermarje");
            return ds.Tables[0];
        }

        internal bool ekzistonLlogariShpernarjeQKSipasNdermarjes(int idndermarje)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDNdermarje", idndermarje, ParameterDirection.Input);
            return Convert.ToBoolean(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_LLOGARISHPERNDARJEQK_ekzistonllogSipasNderm"));
        }

        /// <summary>
        /// kthen llogarishperndarje qk sipas idkpf
        /// </summary>
        /// <param name="idkpf">id e kpf</param>
        ///<returns>nje objekt llogarite e shperndarjes qk sipas kpf</returns>
        internal DataRow ktheLlogariShpernarjeQKSipasKPF(int idkpf)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDKPF", idkpf, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_LLOGARISHPERNDARJEQK_merrSipasIdKpf");
            if (ds == null || ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];
        }

        /// <summary>
        /// kthen llogarishperndarje qk sipas ID
        /// </summary>
        /// <param name="id">id </param>
        ///<returns>nje objekt llogarite e shperndarjes qk sipas id</returns>
        internal DataRow ktheLlogariShpernarjeQKSipasId(int id)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@ID", id, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_LLOGARISHPERNDARJEQK_sel");
            if (ds == null || ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];
        }

        public bool kaVeprime(int idkpf, int idNdermarje)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDKPF", idkpf, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idNdermarje, ParameterDirection.Input);
            using (DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_LLOGARISHPERNDARJEQK_kaVeprime"))
            {
                if (ds.Tables[0].Rows.Count == 0)
                    return false;
                else
                    return true;
            }
        }

        #endregion

        /// <summary>
        /// Metodat e meposhtme i perkasin veprimeve qe kryhen nga objektet colMenyreMesazhi 
        /// </summary>
        #region MENYRE MESAZHI

        /// <summary>
        /// kthen datatable menyre mesazhi
        /// </summary>
        /// <returns> nje datatable qe permban nje koleksion me te gjithe menyre meszhi</returns>
        internal DataTable ktheGjitheMenyreMesazhi(int idGjuha)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@idGjuha", idGjuha, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_MENYREMESAZHI_merrTeGjitha");
            return ds.Tables[0];
        }

        /// <summary>
        /// kthen datatable  menyre mesazhi  sipas id
        /// </summary>
        /// <returns> nje DataRow qe permban nje koleksion me te MENYRE MESAZHI</returns>
        internal DataRow ktheMenyreMesazhiSipasId(int idmetode, int idGjuha)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDMENYREMESAZHI", idmetode, ParameterDirection.Input);
            dbManager.AddParameters(1, "@idGjuha", idGjuha, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_MENYREMESAZHI_merrSipasId");
            return ds.Tables[0].Rows[0];
        }
        /// <summary>
        /// kthen DataRow  menyre mesazhi sipas kodit
        /// </summary>
        /// <returns> nje DataRow qe permban nje koleksion me te menyre mesazhi</returns>
        internal DataRow ktheMenyreMesazhiSipasKodit(string kodi)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@MENYRA", kodi, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_MENYREMESAZHI_merrSipasKodit");
            return ds.Tables[0].Rows[0];
        }
        
        #endregion

        /// <summary>
        /// Metodat e meposhtme i perkasin veprimeve qe kryhen nga objektet clsKonfigurimQK dhe colKonfigurimQK
        /// </summary>
        #region KONFIGURIM QENDRA KOSTO

        /// <summary>
        /// ekzekuton prc_T_KONFIGURIMQK_ins ne DB dhe kthen statusin e perfundimit te ekzekutimit te SP-se perkatese
        /// Shiko clsMesazh brenda ketij moduli per informacion te metejshem mbi vleren e kthimit te kesaj metode
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="idmenyreMesazhi">id e kpf</param>
        /// <param name="idndermarje">id e ndermarjes</param>
        /// <param name="idperdoruesi"> id e perdoruesit</param>
        /// <returns> nje objekt clsMesazh qe tregon nese ruajtja eshte kryer ne rregull apo jo.</returns>
        internal clsMesazh ruajKonfigurimQK(out int id, int prioriteti1, int prioriteti2, int prioriteti3, int prioriteti4, int idmenyreMesazhi, int idndermarje, int idperdoruesi, int idkrijuesi, int prioriteti5)
        {
            id = -1;

            dbManager.Open();
            dbManager.CreateParameters(10);
            dbManager.AddParameters(0, "@IDKONFIGURIMQK", id, ParameterDirection.Output);
            dbManager.AddParameters(1, "@PRIORITETI1", prioriteti1, ParameterDirection.Input);
            dbManager.AddParameters(2, "@PRIORITETI2", prioriteti2, ParameterDirection.Input);
            dbManager.AddParameters(3, "@PRIORITETI3", prioriteti3, ParameterDirection.Input);
            dbManager.AddParameters(4, "@PRIORITETI4", prioriteti4, ParameterDirection.Input);
            dbManager.AddParameters(5, "@IDMENYREMESAZHI", idmenyreMesazhi, ParameterDirection.Input);
            dbManager.AddParameters(6, "@IDNDERMARJE", idndermarje, ParameterDirection.Input);
            dbManager.AddParameters(7, "@IDPERDORUESI", idperdoruesi, ParameterDirection.Input);
            dbManager.AddParameters(8, "@IDKRIJUESI", idkrijuesi, ParameterDirection.Input);
            dbManager.AddParameters(9, "@PRIORITETI5", prioriteti5, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_KONFIGURIMQK_ins");
            id = int.Parse(dbManager.Parameters[0].Value.ToString());
            clsMesazh mesazh = new clsMesazh(true, mesazhRuajtje);
            return mesazh;
        }

        /// <summary>
        /// ekzekutohet sp-ja prc_T_KONFIGURIMQK_del duke i kaluar id e konfigurimit
        /// </summary>
        /// <param name="id"> id e konfigurimit </param>
        /// <returns>nje objekt clsMesazh qe tregon nese fshirja eshte kryer ne rregull apo jo</returns>
        internal clsMesazh fshiKonfigurimQK(int id)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDKONFIGURIMQK", id, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_KONFIGURIMQK_del");
            clsMesazh mesazh = new clsMesazh(true, mesazhFshirje);
            return mesazh;
        }

        /// <summary>
        /// ekzekutohet sp-ja prc_T_KONFIGURIMQK_delSipasNdermarjes duke i kaluar id e ndermarjes
        /// </summary>
        /// <param name="idndermarje"> id e ndermarjes </param>
        /// <returns>nje objekt clsMesazh qe tregon nese fshirja eshte kryer ne rregull apo jo</returns>
        internal clsMesazh fshiKonfigurimQKSipasIdNdermarje(int idndermarje, int idperdoruesi)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDNDERMARJE", idndermarje, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUESI", idperdoruesi, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_KONFIGURIMQK_delSipasNdermarjes");
            clsMesazh mesazh = new clsMesazh(true, mesazhFshirje);
            return mesazh;
        }

        /// <summary>
        /// ekzekutohet sp-ja prc_T_KONFIGURIMQK_upd 
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="idmenyreMesazhi">id e menyre mesazhi</param>
        /// <param name="idndermarje">id e ndermarjes</param>
        /// <param name="idperdoruesi"> id e perdoruesit</param>
        /// <returns>nje objekt clsMesazh qe tregon nese modifikimi eshte kryer ne rregull apo jo</returns>
        internal clsMesazh modifikoKonfigurimQK(int id, int prioriteti1, int prioriteti2, int prioriteti3, int prioriteti4, int idmenyreMesazhi, int idndermarje, int idperdoruesi, int idkrijuesi, int prioriteti5)
        {
            dbManager.Open();
            dbManager.CreateParameters(10);
            dbManager.AddParameters(0, "@IDKONFIGURIMQK", id, ParameterDirection.Input);
            dbManager.AddParameters(1, "@PRIORITETI1", prioriteti1, ParameterDirection.Input);
            dbManager.AddParameters(2, "@PRIORITETI2", prioriteti2, ParameterDirection.Input);
            dbManager.AddParameters(3, "@PRIORITETI3", prioriteti3, ParameterDirection.Input);
            dbManager.AddParameters(4, "@PRIORITETI4", prioriteti4, ParameterDirection.Input);
            dbManager.AddParameters(5, "@IDMENYREMESAZHI", idmenyreMesazhi, ParameterDirection.Input);
            dbManager.AddParameters(6, "@IDNDERMARJE", idndermarje, ParameterDirection.Input);
            dbManager.AddParameters(7, "@IDPERDORUESI", idperdoruesi, ParameterDirection.Input);
            dbManager.AddParameters(8, "@IDKRIJUESI", idkrijuesi, ParameterDirection.Input);
            dbManager.AddParameters(9, "@PRIORITETI5", prioriteti5, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_KONFIGURIMQK_upd");
            clsMesazh mesazh = new clsMesazh(true, mesazhModifikimi);
            return mesazh;
        }

        /// <summary>
        /// kthen konfigurim qk sipas ndermarjes
        /// </summary>
        /// <param name="idndermarje">id e ndermarjes</param>
        ///<returns>nje objekt konfigurim qk sipas ndermarjes</returns>
        internal DataRow ktheKonfigurimQKSipasNdermarjes(int idndermarje, int idperdoruesi)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDNDERMARJE", idndermarje, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUESI", idperdoruesi, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KONFIGURIMQK_merrSipasIdNdermarje");
            if (ds == null || ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];
        }
        
        /// <summary>
        /// kthen konfigurim qk sipas ID
        /// </summary>
        /// <param name="id">id </param>
        ///<returns>nje objekt konfigurim qk sipas id</returns>
        internal DataRow ktheKonfigurimQKSipasId(int id)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDKONFIGURIMQK", id, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KONFIGURIMQK_sel");
            if (ds == null || ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];
        }

        #endregion

        /// <summary>
        /// Metodat e meposhtme i perkasin veprimeve qe kryhen nga objektet clsQendraKosto dhe colQendraKosto
        /// </summary>
        #region QENDRA KOSTO

        /// <summary>
        /// ekzekuton prc_T_QENDRAKOSTO_ins ne DB dhe kthen statusin e perfundimit te ekzekutimit te SP-se perkatese
        /// Shiko clsMesazh brenda ketij moduli per informacion te metejshem mbi vleren e kthimit te kesaj metode
        /// <param name="id">id ritese e qendres se kostos</param>
        /// <param name="kodi">kodi </param>
        /// <param name="pershkrimi">pershkrimi</param>
        /// <param name="idPrindi"> id e prindit </param>
        /// <param name="idmonedha"> id e monedhes</param>
        /// <param name="aktiv">aktiv</param>
        /// <param name="idkonfig">id e konfigurimit </param>
        /// <param name="idPerdoruesi"> id e perdoruesit qe ka kryer veprimin</param>
        /// <param name="idnderm"> id e ndermarjes</param>
        /// <param name="niveli"> niveli i qendres ne hierarkine prind bije, ku prindi ka nivelin 1</param>
        /// <returns> nje objekt clsMesazh qe tregon nese ruajtja eshte kryer ne rregull apo jo.</returns>
        /// </summary>
        internal clsMesazh ruajQenderKosto(out int id, string kodi, string pershkrimi, int idPrindi, int idmonedha, bool aktiv, int idkonfig, int idPerdoruesi, int idnderm, int idstatusdok, int niveli)
        {
            id = -1;

            dbManager.Open();
            dbManager.CreateParameters(11);
            dbManager.AddParameters(0, "@ID", id, ParameterDirection.Output);
            dbManager.AddParameters(1, "@KODI", kodi, ParameterDirection.Input);
            dbManager.AddParameters(2, "@PERSHKRIMI", pershkrimi, ParameterDirection.Input);
            if (idPrindi == 0) dbManager.AddParameters(3, "@IDPRINDI", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(3, "@IDPRINDI", idPrindi, ParameterDirection.Input);
            dbManager.AddParameters(4, "@IDMONEDHA", idmonedha, ParameterDirection.Input);
            dbManager.AddParameters(5, "@AKTIV", aktiv, ParameterDirection.Input);
            dbManager.AddParameters(6, "@IDKONFIG", idkonfig, ParameterDirection.Input);
            dbManager.AddParameters(7, "@IDPERDORUESI", idPerdoruesi, ParameterDirection.Input);
            dbManager.AddParameters(8, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            dbManager.AddParameters(9, "@IDSTATUSDOK", idstatusdok, ParameterDirection.Input);
            dbManager.AddParameters(10, "@NIVELI", niveli, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_QENDRAKOSTO_ins");
            id = int.Parse(dbManager.Parameters[0].Value.ToString());
            return new clsMesazh(true, clsDatabaseQendraKosto.mesazhRuajtje);
        }

        /// <summary>
        /// ekzekuton prc_T_QENDRAKOSTO_upd ne DB dhe kthen statusin e perfundimit te ekzekutimit te SP-se perkatese
        /// Shiko clsMesazh brenda ketij moduli per informacion te metejshem mbi vleren e kthimit te kesaj metode
        /// <param name="id">id ritese e qendres se kostos</param>
        /// <param name="kodi">kodi </param>
        /// <param name="pershkrimi">pershkrimi</param>
        /// <param name="idPrindi"> id e prindit </param>
        /// <param name="idmonedha"> id e monedhes</param>
        /// <param name="aktiv">aktiv</param>
        /// <param name="idkonfig">id e konfigurimit </param>
        /// <param name="idPerdoruesi"> id e perdoruesit qe ka kryer veprimin</param>
        /// <param name="idnderm"> id e ndermarjes</param>
        /// <returns> nje objekt clsMesazh qe tregon nese modifikimi eshte kryer ne rregull apo jo.</returns>
        /// </summary>
        internal clsMesazh modifikoQenderKosto(int id, string kodi, string pershkrimi, int idPrindi, int idmonedha, bool aktiv, int idkonfig, int idPerdoruesi, int idnderm, int idstatusdok, int niveli)
        {
            dbManager.Open();
            dbManager.CreateParameters(11);
            dbManager.AddParameters(0, "@ID", id, ParameterDirection.Input);
            dbManager.AddParameters(1, "@KODI", kodi, ParameterDirection.Input);
            dbManager.AddParameters(2, "@PERSHKRIMI", pershkrimi, ParameterDirection.Input);
            if (idPrindi == 0) dbManager.AddParameters(3, "@IDPRINDI", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(3, "@IDPRINDI", idPrindi, ParameterDirection.Input);
            dbManager.AddParameters(4, "@IDMONEDHA", idmonedha, ParameterDirection.Input);
            dbManager.AddParameters(5, "@AKTIV", aktiv, ParameterDirection.Input);
            dbManager.AddParameters(6, "@IDKONFIG", idkonfig, ParameterDirection.Input);
            dbManager.AddParameters(7, "@IDPERDORUESI", idPerdoruesi, ParameterDirection.Input);
            dbManager.AddParameters(8, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            dbManager.AddParameters(9, "@IDSTATUSDOK", idstatusdok, ParameterDirection.Input);
            dbManager.AddParameters(10, "@NIVELI", niveli, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_QENDRAKOSTO_upd");
            return new clsMesazh(true, clsDatabaseQendraKosto.mesazhModifikimi);
        }

        /// <summary>
        /// fshin qendren e kostos duke ndryshuar statusin  ne te fshire
        /// </summary>
        /// <param name="id">id e qendres</param>
        /// <param name="idperdoruesi"> id e perdoruesit qe ka kryer veprimin</param>
        /// <returns> clsMesazh qe tregon nese fshirja u krye me sukses apo jo</returns>
        internal clsMesazh fshiQenderKostoStatus(int id, int idperdoruesi)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@ID", id, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUESI", idperdoruesi, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_QENDRAKOSTO_upddel");
            clsMesazh mesazh = new clsMesazh(true, clsDatabaseQendraKosto.mesazhFshirje);
            return mesazh;
        }

        /// <summary>
        /// merr qendren sipas id
        /// </summary>
        /// <param name="id">  id e qendres</param>
        /// <returns> kthen datarow qe permban qendren me kete id</returns>
        internal DataRow ktheQenderKosto(int id)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@ID", id, ParameterDirection.Input);
            using (DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_QENDRAKOSTO_sel"))
            {
                if (ds == null || ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                    return null;
                return ds.Tables[0].Rows[0];
            }
        }

        /// <summary>
        /// merr gjithe qendra kostoje te nje ndermarje
        /// </summary>
        /// <param name="idnder"> id e ndermarjes</param>
        /// <returns> nje datatable qe permban nje koleksion me te gjithe qendra kostoje te ndermarjes</returns>
        internal DataTable ktheGjitheQendraKostoSipasNdermarjes(int idnder)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDNDERMARJE", idnder, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_QENDRAKOSTO_merrQenderKostoSipasNdermarjes");
            return ds.Tables[0];
        }

        /// <summary>
        /// merr gjithe qendra kostoje te nje ndermarje aktiv
        /// </summary>
        /// <param name="idnder"> id e ndermarjes</param>
        /// <returns> nje datatable qe permban nje koleksion me te gjithe qendra kostoje te ndermarjes</returns>
        internal DataTable ktheGjitheQendraKostoSipasNdermarjesAktiv(int idnder)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDNDERMARJE", idnder, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_QENDRAKOSTO_merrQenderKostoSipasNdermarjesAktiv");
            return ds.Tables[0];
        }

        /// <summary>
        /// merr gjithe qendra kostoje prind te nje ndermarje 
        /// </summary>
        /// <param name="idnder"> id e ndermarjes</param>
        /// <returns> nje datatable qe permban nje koleksion me te gjithe qendra kostoje prind te ndermarjes </returns>
        internal DataTable ktheGjitheQendraKostoPrindiSipasNdermarjes(int idnder)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDNDERMARJE", idnder, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_QENDRAKOSTO_merrQendraKostoPrindSipasNdermarjes");
            return ds.Tables[0];
        }

        /// <summary>
        /// merr gjithe qendra kostoje prind te nje ndermarje  aktiv
        /// </summary>
        /// <param name="idnder"> id e ndermarjes</param>
        /// <returns> nje datatable qe permban nje koleksion me te gjithe qendra kostoje prind te ndermarjes <returns>
        internal DataTable ktheGjitheQendraKostoPrindiSipasNdermarjesAktiv(int idnder)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDNDERMARJE", idnder, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_QENDRAKOSTO_merrQendraKostoPrindSipasNdermarjesAktiv");
            return ds.Tables[0];
        }

        internal DataTable ktheGjitheQendraKostoPrindiNiveli1SipasNdermarjesAktiv(int idnder)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDNDERMARJE", idnder, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_QENDRAKOSTO_merrQendraKostoPrindNiveli1SipasNdermarjesAktiv");
            return ds.Tables[0];
        }

        /// <summary>
        /// merr gjithe qendra kostoje bij te nje ndermarje  aktiv
        /// </summary>
        /// <param name="idnder"> id e ndermarjes</param>
        /// <returns> nje datatable qe permban nje koleksion me te gjithe qendra kostoje bij te ndermarjes <returns>
        internal DataTable ktheGjitheQendraKostoBijSipasNdermarjesAktiv(int idnder)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDNDERMARJE", idnder, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_QENDRAKOSTO_merrQendraKostoBijSipasNdermarjesAktiv");
            return ds.Tables[0];
        }

        /// <summary>
        /// merr gjithe qendra kostoje prind  qe kane bij te nje ndermarje  aktiv
        /// </summary>
        /// <param name="idnder"> id e ndermarjes</param>
        /// <returns> nje datatable qe permban nje koleksion me te gjithe qendra kostoje prind te ndermarjes <returns>
        internal bool ktheGjitheQendraKostoPrindJoFundoreSipasNdermarjesAktiv(int idnder, string Kodi)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDNDERMARJE", idnder, ParameterDirection.Input);
            dbManager.AddParameters(1, "@KODI", Kodi, ParameterDirection.Input);
            return Convert.ToBoolean(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_QENDRAKOSTO_merrQendraKostoPrindJoFundoreSipasNdermarjesAktiv"));
        }



        /// <summary>
        /// merr qender kosto te nje ndermarje me kete kod
        /// </summary>
        /// <param name="kodi"> kodi </param>
        /// <param name="idnder"> id e ndermarjes</param>
        /// <returns>  data row me qender kosto te nje ndermarje me kete kod</returns>
        internal DataRow ktheQenderKostoSipasKodit(string kodi, int idnder)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@KODI", kodi, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idnder, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_QENDRAKOSTO_merrQenderKostoSipasKodit");
            if (ds == null || ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];
        }

        /// <summary>
        /// merr gjithe qendrat te nje prindi
        /// </summary>
        /// <param name="idprindi">id e qendres prind</param>
        /// <returns>nje datatable qe permban nje koleksion me te gjitha qendrat bij te ketij prindi</returns>
        internal DataTable ktheQendraKostoSipasPrindit(int idprindi)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDPRINDI", idprindi, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_QENDRAKOSTO_merrQendraKostoSipasPrindit");
            return ds.Tables[0];
        }

        internal bool eshtePrindQendraKosto(int idprindi)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDPRINDI", idprindi, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_QENDRAKOSTO_eshtePrindQendraKosto");
            if (ds.Tables[0].Rows.Count == 0)
                return false;
            return true;
        }

        /// <summary>
        /// perdoret per te kontrolluar nqs ekziston nje qender kostoje me kete kod
        /// behet kontrolli per qellime konsistence te informacionit ne DB, nuk lejon te celen qendra kostoje te ndryshem me te njejtin kod
        /// </summary>
        /// <param name="kodi">kodi qendres</param>
        /// <param name="idNdermarje"> id e ndermarjes</param>
        /// <returns>nje objeckt boolean qe tregon nese ekziston apo jo nje qender me kete kod</returns>
        public bool ekzistonQenderKostoje(string kodi, int idNdermarje)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@KODI", kodi, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idNdermarje, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_QENDRAKOSTO_existon");
            if (ds.Tables[0].Rows.Count == 0)
                return false;
            return true;
        }

        public int ktheIdQenderKostoje(string kodi, int idNdermarje)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@KODI", kodi, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idNdermarje, ParameterDirection.Input);
            return Convert.ToInt32(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_QENDRAKOSTO_ktheIdSipasKoditDheNderm"));
        }

        public int ktheIdMonedhenEQK(int idQK)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@ID", idQK, ParameterDirection.Input);
            return Convert.ToInt32(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_QENDRAKOSTO_ktheIDMonedhenSipasIDQK"));
        }

        /// <summary>
        /// perdoret per te kontrolluar nese nje qender kostoje ka bij. perdoret ne rastet e fshirjes se qendres per te mos lejuar te fshihet nje qender prind
        /// </summary>
        /// <param name="idprindi"> id e qendres</param>
        /// <returns> kthen nje objekt boolean qe tregon nese kjo qender ka qendra bij apo jo</returns>
        public bool kaBijQendraKostoje(int idprindi)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDPRINDI", idprindi, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_QENDRAKOSTO_kaBij");
            if (ds.Tables[0].Rows.Count == 0)
                return false;
            return true;
        }

        /// <summary>
        /// merr  qender sipas id
        /// </summary>
        /// <param name="idnderm">idndermarje</param>
        /// <param name="id"> id </param>
        /// <returns> kthen data row me kete qender</returns>
        internal DataRow merrQendraKostoDR(int id)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@ID", id, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_QENDRAKOSTO_merrQendraKostojeDR");
            if (ds == null || ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];
        }

        /// <summary>
        /// merr gjithe qendra kosto sipas ndermarjes
        /// </summary>
        /// <param name="idnderm">id ndermarje</param>
        /// <returns> kthen nje datatable me te gjithe qendra kosto e kesaj ndermarje</returns>
        internal DataTable merrQendraKostoDT(int idnderm)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_QENDRAKOSTO_merrQendraKostoDT");
            return ds.Tables[0];
        }

        #endregion

        /// <summary>
        /// Metodat e meposhtme i perkasin veprimeve qe kryhen nga objektet clsKokaSkemaQK dhe colKokaSkemaQK
        /// </summary>
        #region KOKA SKEMA QENDRA KOSTO

        /// <summary>
        /// ekzekuton 	[prc_T_KOKASKEMAQK_ins] ne DB dhe kthen statusin e perfundimit te ekzekutimit te SP-se perkatese
        /// Shiko clsMesazh brenda ketij moduli per informacion te metejshem mbi vleren e kthimit te kesaj metode
        /// </summary>
        /// <param name="idkoka"> id e kokes</param>
        /// <param name="kodi">kodi</param>
        /// <param name="pershkrimi">pershkrimi</param>
        /// <param name="idkonfig">id e konfigurimit</param>
        /// <param name="idPerdoruesi">id e perdoruesit</param>
        /// <param name="idnderm">id e ndermarjes</param>
        /// <param name="idstatusdok">id e statusdok</param>
        /// <returns>nje objekt clsMesazh qe tregon nese ruajtja eshte kryer ne rregull apo jo.</returns>
        internal clsMesazh ruajKokaSkemaQK(out int idkoka, string kodi, string pershkrimi, int idkonfig, int idPerdoruesi, int idnderm, int idstatusdok)
        {
            idkoka = -1;

            dbManager.Open();
            dbManager.CreateParameters(7);
            dbManager.AddParameters(0, "@IDKOKA", idkoka, ParameterDirection.Output);
            dbManager.AddParameters(1, "@KODI", kodi, ParameterDirection.Input);
            dbManager.AddParameters(2, "@PERSHKRIMI", pershkrimi, ParameterDirection.Input);
            dbManager.AddParameters(3, "@IDKONFIG", idkonfig, ParameterDirection.Input);
            dbManager.AddParameters(4, "@IDPERDORUESI", idPerdoruesi, ParameterDirection.Input);
            dbManager.AddParameters(5, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            dbManager.AddParameters(6, "@IDSTATUSDOK", idstatusdok, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_KOKASKEMAQK_ins");
            idkoka = int.Parse(dbManager.Parameters[0].Value.ToString());
            return new clsMesazh(true, mesazhRuajtje);
        }

        /// <summary>
        /// ekzekuton prc_T_KOKASKEMAQK_upd ne DB dhe kthen statusin e perfundimit te ekzekutimit te SP-se perkatese
        /// Shiko clsMesazh brenda ketij moduli per informacion te metejshem mbi vleren e kthimit te kesaj metode
        /// </summary>
        /// <param name="idkoka"> id e kokes</param>
        /// <param name="kodi">kodi</param>
        /// <param name="pershkrimi">pershkrimi</param>
        /// <param name="idkonfig">id e konfigurimit</param>
        /// <param name="idPerdoruesi">id e perdoruesit</param>
        /// <param name="idnderm">id e ndermarjes</param>
        /// <param name="idstatusdok">id e statusdok</param>
        /// <returns> nje objekt clsMesazh qe tregon nese modifikimi eshte kryer ne rregull apo jo.</returns>
        internal clsMesazh modifikoKokaSkemaQK(int idkoka, string kodi, string pershkrimi, int idkonfig, int idPerdoruesi, int idnderm, int idstatusdok)
        {
            dbManager.Open();
            dbManager.CreateParameters(7);
            dbManager.AddParameters(0, "@IDKOKA", idkoka, ParameterDirection.Input);
            dbManager.AddParameters(1, "@KODI", kodi, ParameterDirection.Input);
            dbManager.AddParameters(2, "@PERSHKRIMI", pershkrimi, ParameterDirection.Input);
            dbManager.AddParameters(3, "@IDKONFIG", idkonfig, ParameterDirection.Input);
            dbManager.AddParameters(4, "@IDPERDORUESI", idPerdoruesi, ParameterDirection.Input);
            dbManager.AddParameters(5, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            dbManager.AddParameters(6, "@IDSTATUSDOK", idstatusdok, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_KOKASKEMAQK_upd");
            return new clsMesazh(true, mesazhModifikimi);
        }

        /// <summary>
        /// fshin skemen  duke ndryshuar statusin e saj ne te fshire
        /// </summary>
        /// <param name="idkoka">id e kokes</param>
        /// <param name="idperdoruesi"> id e perdoruesit qe ka kryer veprimin</param>
        /// <returns> clsMesazh qe tregon nese fshirja u krye me sukses apo jo</returns>
        internal clsMesazh fshiKokaSkemaQKStatus(int idkoka, int idperdoruesi)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDKOKA", idkoka, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUESI", idperdoruesi, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_KOKASKEMAQK_upddel");
            clsMesazh mesazh = new clsMesazh(true, mesazhFshirje);
            return mesazh;
        }

        /// <summary>
        /// merr skemen sipas id
        /// </summary>
        /// <param name="idkoka">  id e kokes</param>
        /// <returns> kthen datarow qe permban skemen koka me kete id</returns>
        internal DataRow ktheKokaSkema(int idkoka)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDKOKA", idkoka, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KOKASKEMAQK_merrSkemaSipasId");
            if (ds == null || ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];
        }

        /// <summary>
        /// merr gjithe skemat te nje ndermarje
        /// </summary>
        /// <param name="idnder"> id e ndermarjes</param>
        /// <returns> nje datatable qe permban nje koleksion me te gjithe skemat te ndermarjes</returns>
        internal DataTable ktheGjitheSkematSipasNdermarjes(int idnder)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDNDERMARJE", idnder, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KOKASKEMAQK_merrGjitheSkemat");
            return ds.Tables[0];
        }

        /// <summary>
        /// merr gjithe skemat te nje ndermarje like
        /// </summary>
        /// <param name="idnder"> id e ndermarjes</param>
        /// <param name="kodi">kodi </param>
        /// <returns> nje datatable qe permban nje koleksion me te gjithe skemat te ndermarjes qe permbajne kete kod</returns>
        internal DataTable ktheGjitheSkematSipasNdermarjesLike(int idnder, string kodi)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDNDERMARJE", idnder, ParameterDirection.Input);
            dbManager.AddParameters(1, "@KODI", kodi, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KOKASKEMAQK_merrGjitheSkemaLike");
            return ds.Tables[0];
        }

        /// <summary>
        /// merr skemen te nje ndermarje me kete kod
        /// </summary>
        /// <param name="kodi"> kodi i skemes</param>
        /// <param name="idnder"> id e ndermarjes</param>
        /// <returns>  data row me skemen te nje ndermarje me kete kod</returns>
        internal DataRow ktheKokaSkemaSipasKodit(string kodi, int idnder)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@KODI", kodi, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idnder, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KOKASKEMAQK_ktheSkemeSipasKodit");
            if (ds == null || ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];
        }

        /// <summary>
        /// perdoret per te kontrolluar nqs ekziston nje skeme me kete kod 
        /// behet kontrolli per qellime konsistence te informacionit ne DB, nuk lejon te celen skema te ndryshem me te njejtin kod
        /// </summary>
        /// <param name="kodi">kodi i skemes</param>
        /// <param name="idNdermarje"> id e ndermarjes</param>
        /// <returns>nje objeckt boolean qe tregon nese ekziston apo jo nje skeme me kete kod</returns>
        public bool ekzistonKokaSkema(string kodi, int idNdermarje)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@KODI", kodi, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idNdermarje, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KOKASKEMAQK_ekzistonKod");
            if (ds.Tables[0].Rows.Count == 0)
                return false;
            return true;
        }

        /// <summary>
        /// merr  skeme sipas id ne forme data row
        /// </summary>
        /// <param name="id"> id e skemes</param>
        /// <returns> kthen data row me kete skeme</returns>
        internal DataRow merrKokaSkemeSipasIdDR(int id)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDKOKA", id, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KOKASKEMAQK_merrSipasIdDR");
            if (ds == null || ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];
        }

        /// <summary>
        /// merr gjithe skemat sipas ndermarjes ne forme data table
        /// </summary>
        /// <param name="idnderm">id ndermarje</param>
        /// <returns> kthen nje datatable me te gjithe skemat e kesaj ndermarje</returns>
        internal DataTable merrKokaSkemaDT(int idnderm)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KOKASKEMAQK_merrSipasNdermarrjesDT");
            return ds.Tables[0];
        }
       
        #endregion

        /// <summary>
        /// Metodat e meposhtme i perkasin veprimeve qe kryhen nga objektet clsTrupiSkemaQK dhe colTrupiSkemaQK
        /// </summary>
        #region TRUPI SKEMA QENDRA KOSTO

        /// <summary>
        /// ekzekuton 	[prc_T_TRUPISKEMAQK_ins] ne DB dhe kthen statusin e perfundimit te ekzekutimit te SP-se perkatese
        /// Shiko clsMesazh brenda ketij moduli per informacion te metejshem mbi vleren e kthimit te kesaj metode
        /// </summary>
        /// <param name="idtrupi"> id e trupit</param>
        /// <param name="idkoka">id e kokes</param>
        /// <param name="idqk">id e qendra kosto</param>
        /// <param name="perqindje">perqindja</param>
        /// <returns>nje objekt clsMesazh qe tregon nese ruajtja eshte kryer ne rregull apo jo.</returns>
        internal clsMesazh ruajTrupiSkemaQK(out int idtrupi, int idkoka, int idqk, decimal perqindje)
        {
            idtrupi = -1;

            dbManager.Open();
            dbManager.CreateParameters(4);
            dbManager.AddParameters(0, "@IDTRUPI", idtrupi, ParameterDirection.Output);
            dbManager.AddParameters(1, "@IDKOKA", idkoka, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDQK", idqk, ParameterDirection.Input);
            dbManager.AddParameters(3, "@PERQINDJA", perqindje, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_TRUPISKEMAQK_ins");
            idtrupi = int.Parse(dbManager.Parameters[0].Value.ToString());
            return new clsMesazh(true, mesazhRuajtje);
        }

        /// <summary>
        /// ekzekuton prc_T_TRUPISKEMAQK_upd ne DB dhe kthen statusin e perfundimit te ekzekutimit te SP-se perkatese
        /// Shiko clsMesazh brenda ketij moduli per informacion te metejshem mbi vleren e kthimit te kesaj metode
        /// </summary>
        /// <param name="idtrupi"> id e trupit</param>
        /// <param name="idkoka">id e kokes</param>
        /// <param name="idqk">id e qendra kosto</param>
        /// <param name="perqindje">perqindja</param>
        /// <returns> nje objekt clsMesazh qe tregon nese modifikimi eshte kryer ne rregull apo jo.</returns>
        internal clsMesazh modifikoTrupiSkemaQK(int idtrupi, int idkoka, int idqk, decimal perqindje)
        {
            dbManager.Open();
            dbManager.CreateParameters(4);
            dbManager.AddParameters(0, "@IDTRUPI", idtrupi, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDKOKA", idkoka, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDQK", idqk, ParameterDirection.Input);
            dbManager.AddParameters(3, "@PERQINDJA", perqindje, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_TRUPISKEMAQK_upd");
            return new clsMesazh(true, mesazhModifikimi);
        }

        /// <summary>
        /// ekzekutohet sp-ja prc_T_TRUPISKEMAQK_del duke i kaluar id e trupit
        /// </summary>
        /// <param name="idTrupit">id e trupit qe do fshihet </param>
        /// <returns>nje objekt clsMesazh qe tregon nese fshirja eshte kryer ne rregull apo jo</returns>
        internal clsMesazh fshiTrupiSkemaQK(int idTrupit)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDTRUPI", idTrupit, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_TRUPISKEMAQK_del");
            clsMesazh mesazh = new clsMesazh(true, mesazhFshirje);
            return mesazh;
        }

        /// <summary>
        /// ekzekutohet sp-ja prc_T_TRUPISKEMAQK_delSipasIdKoka duke i kaluar id e trupit
        /// </summary>
        /// <param name="idkoka">id e kokes qe do fshihet </param>
        /// <returns>nje objekt clsMesazh qe tregon nese fshirja eshte kryer ne rregull apo jo</returns>
        internal clsMesazh fshiTrupiSkemaQKSipasIdKoka(int idKoka)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDKOKA", idKoka, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_TRUPISKEMAQK_delSipasIdKoka");
            clsMesazh mesazh = new clsMesazh(true, mesazhFshirje);
            return mesazh;
        }

        /// <summary>
        /// merr skema trupi sipas id trupi
        /// </summary>
        /// <param name="idtrupi">  id e trupit</param>
        /// <returns> kthen datarow qe permban skema trupi me kete id</returns>
        internal DataRow ktheTrupiSkemaQK(int idtrupi)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDTRUPI", idtrupi, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_TRUPISKEMAQK_sel");
            if (ds == null || ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];
        }

        /// <summary>
        /// merr skema trupi sipas idkokes
        /// </summary>
        /// <param name="idkoka"> id e kokes</param>
        /// <returns> nje datatable qe permban nje koleksion me te gjithe trupin e kokes</returns>
        internal DataTable ktheTrupiSkemaQKSipasIdKoka(int idkoka)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDKOKA", idkoka, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_TRUPISKEMAQK_merrSipasIdKoke");
            return ds.Tables[0];
        }

        #endregion

        /// <summary>
        /// Metodat e meposhtme i perkasin veprimeve qe kryhen nga objektet clsObjektivaKosto dhe colObjektivaKosto
        /// </summary>
        #region OBJEKTIVA KOSTO

        /// <summary>
        /// ekzekuton prc_T_OBJEKTIVAKOSTO_ins ne DB dhe kthen statusin e perfundimit te ekzekutimit te SP-se perkatese
        /// Shiko clsMesazh brenda ketij moduli per informacion te metejshem mbi vleren e kthimit te kesaj metode   
        /// </summary>
        /// <param name="id">id ritese e objektives se kostos</param>
        /// <param name="kodi">kodi </param>
        /// <param name="pershkrimi">pershkrimi</param>
        /// <param name="nga"> data nga </param>
        /// <param name="deri"> data deri</param>
        /// <param name="aktiv">aktiv</param>
        /// <param name="idkonfig">id e konfigurimit </param>
        /// <param name="idPerdoruesi"> id e perdoruesit qe ka kryer veprimin</param>
        /// <param name="idnderm"> id e ndermarjes</param>
        /// <returns> nje objekt clsMesazh qe tregon nese ruajtja eshte kryer ne rregull apo jo.</returns>
        internal clsMesazh ruajObjektivKosto(out int id, string kodi, string pershkrimi, DateTime nga, DateTime deri, bool aktiv, int idkonfig, int idPerdoruesi, int idnderm, int idstatusdok)
        {
            id = -1;

            dbManager.Open();
            dbManager.CreateParameters(10);
            dbManager.AddParameters(0, "@ID", id, ParameterDirection.Output);
            dbManager.AddParameters(1, "@KODI", kodi, ParameterDirection.Input);
            dbManager.AddParameters(2, "@PERSHKRIMI", pershkrimi, ParameterDirection.Input);
            dbManager.AddParameters(3, "@NGA", nga, ParameterDirection.Input);
            if (deri == new DateTime()) dbManager.AddParameters(4, "@DERI", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(4, "@DERI", deri, ParameterDirection.Input);
            dbManager.AddParameters(5, "@AKTIV", aktiv, ParameterDirection.Input);
            dbManager.AddParameters(6, "@IDKONFIG", idkonfig, ParameterDirection.Input);
            dbManager.AddParameters(7, "@IDPERDORUESI", idPerdoruesi, ParameterDirection.Input);
            dbManager.AddParameters(8, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            dbManager.AddParameters(9, "@IDSTATUSDOK", idstatusdok, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_OBJEKTIVAKOSTO_ins");
            id = int.Parse(dbManager.Parameters[0].Value.ToString());
            return new clsMesazh(true, clsDatabaseQendraKosto.mesazhRuajtje);
        }

        /// <summary>
        /// ekzekuton prc_T_OBJEKTIVAKOSTO_upd ne DB dhe kthen statusin e perfundimit te ekzekutimit te SP-se perkatese
        /// Shiko clsMesazh brenda ketij moduli per informacion te metejshem mbi vleren e kthimit te kesaj metode   
        /// </summary>
        /// <param name="id">id ritese e objektives se kostos</param>
        /// <param name="kodi">kodi </param>
        /// <param name="pershkrimi">pershkrimi</param>
        /// <param name="nga"> data nga </param>
        /// <param name="deri"> data deri</param>
        /// <param name="aktiv">aktiv</param>
        /// <param name="idkonfig">id e konfigurimit </param>
        /// <param name="idPerdoruesi"> id e perdoruesit qe ka kryer veprimin</param>
        /// <param name="idnderm"> id e ndermarjes</param>
        /// <returns> nje objekt clsMesazh qe tregon nese modifikimi eshte kryer ne rregull apo jo.</returns>
        internal clsMesazh modifikoObjektivKosto(int id, string kodi, string pershkrimi, DateTime nga, DateTime deri, bool aktiv, int idkonfig, int idPerdoruesi, int idnderm, int idstatusdok)
        {
            dbManager.Open();
            dbManager.CreateParameters(10);
            dbManager.AddParameters(0, "@ID", id, ParameterDirection.Input);
            dbManager.AddParameters(1, "@KODI", kodi, ParameterDirection.Input);
            dbManager.AddParameters(2, "@PERSHKRIMI", pershkrimi, ParameterDirection.Input);
            dbManager.AddParameters(3, "@NGA", nga, ParameterDirection.Input);
            if (deri == new DateTime()) dbManager.AddParameters(4, "@DERI", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(4, "@DERI", deri, ParameterDirection.Input);
            dbManager.AddParameters(5, "@AKTIV", aktiv, ParameterDirection.Input);
            dbManager.AddParameters(6, "@IDKONFIG", idkonfig, ParameterDirection.Input);
            dbManager.AddParameters(7, "@IDPERDORUESI", idPerdoruesi, ParameterDirection.Input);
            dbManager.AddParameters(8, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            dbManager.AddParameters(9, "@IDSTATUSDOK", idstatusdok, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_OBJEKTIVAKOSTO_upd");
            return new clsMesazh(true, clsDatabaseQendraKosto.mesazhModifikimi);
        }

        /// <summary>
        /// fshin objektivin e kostos duke ndryshuar statusin  ne te fshire
        /// </summary>
        /// <param name="id">id e objektivit</param>
        /// <param name="idperdoruesi"> id e perdoruesit qe ka kryer veprimin</param>
        /// <returns> clsMesazh qe tregon nese fshirja u krye me sukses apo jo</returns>
        internal clsMesazh fshiObjektivKostoStatus(int id, int idperdoruesi)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@ID", id, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUESI", idperdoruesi, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_OBJEKTIVAKOSTO_upddel");
            clsMesazh mesazh = new clsMesazh(true, clsDatabaseQendraKosto.mesazhFshirje);
            return mesazh;
        }

        /// <summary>
        /// merr objektiv sipas id
        /// </summary>
        /// <param name="id">  id e objektivit</param>
        /// <returns> kthen datarow qe permban objektiv me kete id</returns>
        internal DataRow ktheObjektivKosto(int id)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@ID", id, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_OBJEKTIVAKOSTO_sel");
            if (ds == null || ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];
        }

        /// <summary>
        /// merr gjithe objektiva kostoje te nje ndermarje
        /// </summary>
        /// <param name="idnder"> id e ndermarjes</param>
        /// <returns> nje datatable qe permban nje koleksion me te gjithe objektiva kostoje te ndermarjes</returns>
        internal DataTable ktheGjitheObjektivaKostoSipasNdermarjes(int idnder)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDNDERMARJE", idnder, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_OBJEKTIVAKOSTO_merrObjektivKostoSipasNdermarjes");
            return ds.Tables[0];
        }

        /// <summary>
        /// merr gjithe objektiva kostoje te nje ndermarje aktiv
        /// </summary>
        /// <param name="idnder"> id e ndermarjes</param>
        /// <returns> nje datatable qe permban nje koleksion me te gjithe objektiva kostoje te ndermarjes</returns>
        internal DataTable ktheGjitheObjektivaKostoSipasNdermarjesAktiv(int idnder)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDNDERMARJE", idnder, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_OBJEKTIVAKOSTO_merrObjektivKostoSipasNdermarjesAktiv");
            return ds.Tables[0];
        }

        /// <summary>
        /// merr objektiva kosto te nje ndermarje me kete kod
        /// </summary>
        /// <param name="kodi"> kodi </param>
        /// <param name="idnder"> id e ndermarjes</param>
        /// <returns>  data row me objektiva kosto te nje ndermarje me kete kod</returns>
        internal DataRow ktheObjektivKostoSipasKodit(string kodi, int idnder)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@KODI", kodi, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idnder, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_OBJEKTIVAKOSTO_merrObjektivKostoSipasKodit");
            if (ds == null || ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];
        }
        
        /// <summary>
        /// perdoret per te kontrolluar nqs ekziston nje objektiv kostoje me kete kod
        /// behet kontrolli per qellime konsistence te informacionit ne DB, nuk lejon te celen objektiv kostoje te ndryshem me te njejtin kod
        /// </summary>
        /// <param name="kodi">kodi objektives</param>
        /// <param name="idNdermarje"> id e ndermarjes</param>
        /// <returns>nje objeckt boolean qe tregon nese ekziston apo jo nje objektiv me kete kod</returns>
        public bool ekzistonObjektivKostoje(string kodi, int idNdermarje)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@KODI", kodi, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idNdermarje, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_OBJEKTIVAKOSTO_existon");
            if (ds.Tables[0].Rows.Count == 0)
                return false;
            return true;
        }

        /// <summary>
        /// merr  objektiv sipas id
        /// </summary>
        /// <param name="idnderm">idndermarje</param>
        /// <param name="id"> id </param>
        /// <returns> kthen data row me kete objektiv</returns>
        internal DataRow merrObjektivKostoDR(int id)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@ID", id, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_OBJEKTIVAKOSTO_merrObjektivKostojeDR");
            if (ds == null || ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];
        }

        /// <summary>
        /// merr gjithe objektiva kosto sipas ndermarjes
        /// </summary>
        /// <param name="idnderm">id ndermarje</param>
        /// <returns> kthen nje datatable me te gjithe objektiva kosto e kesaj ndermarje</returns>
        internal DataTable merrObjektivKostoDT(int idnderm)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_OBJEKTIVAKOSTO_merrObjektivKostoDT");
            return ds.Tables[0];
        }

        #endregion

        /// <summary>
        /// Metodat e meposhtme i perkasin veprimeve qe kryhen nga objektet clsKokaQendraKosto dhe colKokaQendraKosto
        /// </summary>
        #region KOKA QENDRA KOSTO

        /// <summary>
        /// ekzekuton prc_T_KOKAQENDRAKOSTO_ins ne DB dhe kthen statusin e perfundimit te ekzekutimit te SP-se perkatese
        /// Shiko clsMesazh brenda ketij moduli per informacion te metejshem mbi vleren e kthimit te kesaj metode
        /// </summary>
        /// <param name="dtDk"> data e dokumentit te qendra kosto</param>
        /// <param name="dtRegj"> data e regjistrimit te dokumentit te qendra kosto</param>
        /// <param name="nrRef"> nr i references te fletes kontabel</param>
        /// <param name="idKonf"> id e konfigurimit te dokumentit</param>
        /// <param name="iddoknga">id e dokumentit nga e cila gjenerohet ne rastet e modifikimit </param>
        /// <param name="idkoka">id ritese e kokes se qendres se kostos</param>
        /// <param name="idNder"> id e ndermarjes</param>
        /// <param name="idNdVt"> id e ndermarje vitit</param>
        /// <param name="idNiv"> id e nivelit te regjistrimit</param>
        /// <param name="idPer"> id e perdoruesit se e ben regjistrimin</param>
        /// <param name="idSt"> id e statusit te dokumentit</param>
        /// <param name="nrDk">nr i dokumentit</param>
        /// <param name="pershkrim"> shenime </param>
        /// <param name="idGjenerues">id e dokumentit nga gjenerohet qendra e kostos nga nje ambjent tjeter</param>
        /// <param name="idKonfigGjenerues">id e konfigurimit nga gjenerohet dokumenti</param>
        /// <param name="idNivelGjenerues">id e nivelit nga gjenerohet dokumenti</param>
        /// <returns> nje objekt clsMesazh qe tregon nese ruajtja eshte kryer ne rregull apo jo.</returns>
        internal clsMesazh ruajKokaQendraKosto(out int idkoka, int idNiv, int idKonf, int nrRef, DateTime dtDk, string nrDk, int iddoknga, int idSt, int idNder, int idNdVt, int idPer, DateTime dtRegj, string pershkrim, int idNivelGjenerues, int idKonfigGjenerues, int idGjenerues)
        {
            idkoka = 0;

            dbManager.Open();
            dbManager.CreateParameters(16);
            dbManager.AddParameters(0, "@IDKOKA", idkoka, ParameterDirection.Output);
            dbManager.AddParameters(1, "@IDNIVEL", idNiv, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDKONFIGAMBJENTE", idKonf, ParameterDirection.Input);
            dbManager.AddParameters(3, "@NRREF", nrRef, ParameterDirection.Input);
            dbManager.AddParameters(4, "@NRDOK", nrDk, ParameterDirection.Input);
            dbManager.AddParameters(5, "@DTDOK", dtDk, ParameterDirection.Input);
            if (iddoknga == 0)
                dbManager.AddParameters(6, "@IDDOKNGA", DBNull.Value, ParameterDirection.Input);
            else
                dbManager.AddParameters(6, "@IDDOKNGA", iddoknga, ParameterDirection.Input);
            dbManager.AddParameters(7, "@IDSTATUSDOK", idSt, ParameterDirection.Input);
            dbManager.AddParameters(8, "@IDNDERMARJE", idNder, ParameterDirection.Input);
            dbManager.AddParameters(9, "@IDNDERVITI", idNdVt, ParameterDirection.Input);
            dbManager.AddParameters(10, "@IDPERDORUESI", idPer, ParameterDirection.Input);
            dbManager.AddParameters(11, "@DTREGJ", dtRegj, ParameterDirection.Input);
            if (pershkrim == null)
                dbManager.AddParameters(12, "@PERSHKRIMI", DBNull.Value, ParameterDirection.Input);
            else
                dbManager.AddParameters(12, "@PERSHKRIMI", pershkrim, ParameterDirection.Input);
            if (idNivelGjenerues == 0) dbManager.AddParameters(13, "@IDNIVELGJENERUES", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(13, "@IDNIVELGJENERUES", idNivelGjenerues, ParameterDirection.Input);
            if (idKonfigGjenerues == 0) dbManager.AddParameters(14, "@IDKONFIGGJENERUES", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(14, "@IDKONFIGGJENERUES", idKonfigGjenerues, ParameterDirection.Input);
            if (idGjenerues == 0) dbManager.AddParameters(15, "@IDGJENERUES", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(15, "@IDGJENERUES", idGjenerues, ParameterDirection.Input);

            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_KOKAQENDRAKOSTO_ins");
            idkoka = int.Parse(dbManager.Parameters[0].Value.ToString());

            clsMesazh mesazh = new clsMesazh(true, mesazhRuajtje);
            return mesazh;
        }

        /// <summary>
        /// Ekzekuton prc_T_KOKAQENDRAKOSTO_upd ne DB dhe kthen statusin e perfundimit te ekzekutimit te SP-se perkatese
        /// Ben update tabelen e kokes dhe nese pershkrimi qe ka patur dok ne koke me pare eshte i ndryshem nga ai qe po i kalohet tani, do te behet update dhe pershkrimi qe gjendet ne gjithe rreshtat e trupit te dok, ne te kunder do te lihet sic eshte.
        /// Shiko clsMesazh brenda ketij moduli per informacion te metejshem mbi vleren e kthimit te kesaj metode 
        /// </summary>
        /// <param name="dtDk"> data e dokumentit te qendra kosto</param>
        /// <param name="dtRegj"> data e regjistrimit te dokumentit te qendra kosto</param>
        /// <param name="nrRef"> nr i references te fletes kontabel</param>
        /// <param name="idKonf"> id e konfigurimit te dokumentit</param>
        /// <param name="iddoknga">id e dokumentit nga e cila gjenerohet ne rastet e modifikimit </param>
        /// <param name="idkoka">id ritese e kokes se qendres se kostos</param>
        /// <param name="idNder"> id e ndermarjes</param>
        /// <param name="idNdVt"> id e ndermarje vitit</param>
        /// <param name="idNiv"> id e nivelit te regjistrimit</param>
        /// <param name="idPer"> id e perdoruesit se e ben regjistrimin</param>
        /// <param name="idSt"> id e statusit te dokumentit</param>
        /// <param name="nrDk">nr i dokumentit</param>
        /// <param name="pershkrim"> shenime </param>
        /// <param name="idGjenerues">id e dokumentit nga gjenerohet qendra e kostos nga nje ambjent tjeter</param>
        /// <param name="idKonfigGjenerues">id e konfigurimit nga gjenerohet dokumenti</param>
        /// <param name="idNivelGjenerues">id e nivelit nga gjenerohet dokumenti</param>
        internal clsMesazh modifikoKokaQenderKosto(int idkoka, int idNiv, int idKonf, int nrRef, DateTime dtDk, string nrDk, int idSt, int idNder, int idNdVt, int idPer, DateTime dtRegj, string pershkrim)
        {
            dbManager.Open();
            dbManager.CreateParameters(12);
            dbManager.AddParameters(0, "@IDKOKA", idkoka, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNIVEL", idNiv, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDKONFIGAMBJENTE", idKonf, ParameterDirection.Input);
            dbManager.AddParameters(3, "@NRREF", nrRef, ParameterDirection.Input);
            dbManager.AddParameters(4, "@NRDOK", nrDk, ParameterDirection.Input);
            dbManager.AddParameters(5, "@DTDOK", dtDk, ParameterDirection.Input);
            dbManager.AddParameters(6, "@IDSTATUSDOK", idSt, ParameterDirection.Input);
            dbManager.AddParameters(7, "@IDNDERMARJE", idNder, ParameterDirection.Input);
            dbManager.AddParameters(8, "@IDNDERVITI", idNdVt, ParameterDirection.Input);
            dbManager.AddParameters(9, "@IDPERDORUESI", idPer, ParameterDirection.Input);
            dbManager.AddParameters(10, "@DTREGJ", dtRegj, ParameterDirection.Input);
            dbManager.AddParameters(11, "@PERSHKRIMI", pershkrim, ParameterDirection.Input);

            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_KOKAQENDRAKOSTO_upd");
            clsMesazh mesazh = new clsMesazh(true, mesazhModifikimi);
            return mesazh;
        }
        /// <summary>
        /// ekzekutohet sp-ja prc_T_KOKAQENDRAKOSTO_del duke i kaluar id e kokes se dokumentit qe e marrim nga objekti clsKokaQenderKosto qe i kalohet si parameter
        /// </summary>
        /// <param name="idkoka">id ritese e kokes </param>
        /// <returns>nje objekt clsMesazh qe tregon nese fshirja eshte kryer ne rregull apo jo</returns>
        internal clsMesazh fshiKokaQenderKosto(int idkoka)
        {

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDKOKA", idkoka, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_KOKAQENDRAKOSTO_del");
            clsMesazh mesazh = new clsMesazh(true, mesazhFshirje);
            return mesazh;

        }
        internal clsMesazh kaloNeHistorikKokaQenderKosto(int idkoka, int idPerdoruesi, int idStatusDok)
        {

            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@IDKOKA", idkoka, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUESI", idPerdoruesi, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDSTATUSDOK", idStatusDok, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_KOKAQENDRAKOSTO_hidhNeHistorik");
            clsMesazh mesazh = new clsMesazh(true, mesazhFshirje);
            return mesazh;

        }

        /// <summary>
        /// kthen objekt koka dokumenti qender kosto sipas idse
        /// </summary>
        ///<param name="idkoka"> koka e dokumentit te qender kosto</param>
        ///<returns>nje objekt datarow qe permban nje koleksion me te gjitha kokat e dokumentit te qender kosto me kete id  </returns>
        internal DataRow ktheKokaQenderKostoSipasID(int idkoka)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDKOKA", idkoka, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KOKAQENDRAKOSTO_sel");
            if (ds == null || ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];
        }

        internal DataRow ktheKokaQenderKostoSipasIDGjeneruesDheKonfig(int idgjenerues, int idkonfiggjenerues)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@idgjenerues", idgjenerues, ParameterDirection.Input);
            dbManager.AddParameters(1, "@idkonfigambjente", idkonfiggjenerues, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KOKAQENDRAKOSTO_merrSipasIdGjenerues");
            if (ds == null || ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];
        }

        internal int ktheIdStatusDokSipasIdKoka(int idKoka)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDKOKA", idKoka, ParameterDirection.Input);
            return Convert.ToInt32(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_KOKAQENDRAKOSTO_merrIdStatusDokSipasId"));
        }


        /// <summary>
        /// perdoret per te kontrolluar nqs ekziston nje dokument qender kosto me nje nr dokumenti te marre si parameter
        /// behet kontrolli per qellime konsistence te informacionit ne DB, nuk lejon te celen dokumenta  te ndryshem me te njejtin kod ne nje ndermarje
        /// </summary>
        /// <param name="idNdermarrje"> id e ndermarjes</param>
        /// <param name="dtdok"> data e dokumentit te qendres se kostos</param>
        /// <param name="idKonfigAmbjente"> id e konfigurimit te dokumentit</param>
        /// <param name="nrDok">nr i dokumentit</param>
        /// <returns>nje objeckt clsMesazh qe tregon nese ekziston apo jo nje dokument me kete kod</returns>
        internal bool ekzistonRegjistrimQenderKosto(int idKonfigAmbjente, string nrDok, DateTime dtdok, int idNdermarrje)
        {
            dbManager.Open();
            dbManager.CreateParameters(4);
            dbManager.AddParameters(0, "@LLOJI", idKonfigAmbjente, ParameterDirection.Input);
            dbManager.AddParameters(1, "@NRDOK", nrDok, ParameterDirection.Input);
            dbManager.AddParameters(2, "@DTDOK", dtdok, ParameterDirection.Input);
            dbManager.AddParameters(3, "@IDNDERMARJE", idNdermarrje, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KOKAQENDRAKOSTO_ekzistonRegjistrimQenderKosto");
            if (ds.Tables[0].Rows.Count == 0)
                return false;
            return true;
        }

        /// <summary>
        /// merr kokat e qender kosto sipas idndervitit dhe autorizimeve ne forme datatable //metoda per te marre te gjithe  kokat e fleteve kontabel te pa kontabilizuara
        /// </summary>
        /// <param name="idNdermVit">idndermvit</param>
        /// <param name="idperdoruesi">idperdoruesi</param>
        /// <returns>data table me keto te dhena</returns>
        internal DataTable merrKokaQenderKostoDT(int idNdermVit, int idperdoruesi)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDNDERVITI", idNdermVit, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUES", idperdoruesi, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KOKAQENDRAKOSTO_merrKokaQenderKostoDT");
            return ds.Tables[0];
        }

        internal DataTable ktheReshtaDokQKPerRivleresimFifoArtikulli(int idartikulli, int idmagazina, DateTime datanga, DateTime dataderi)
        {
            dbManager.Open();
            dbManager.CreateParameters(4);
            dbManager.AddParameters(0, "@IDARTIKULL", idartikulli, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDMAG", idmagazina, ParameterDirection.Input);
            dbManager.AddParameters(2, "@DATANGA", datanga, ParameterDirection.Input);
            dbManager.AddParameters(3, "@DATADERI", dataderi, ParameterDirection.Input);
            return dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KOKAQENDRAKOSTO_riVleresimFIFOmerrReshta").Tables[0];
        }
        #endregion

        /// <summary>
        /// Metodat e meposhtme i perkasin veprimeve qe kryhen nga objektet clsTrupiQendraKosto dhe colTrupiQendraKosto
        /// </summary>
        #region TRUPI QENDRA KSOTO

        public clsMesazh RuajTrupQendraKostoDT(DataTable dtTrupi)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@DTTRUPI", dtTrupi, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_TRUPIQENDRAKOSTO_insDT");
            return new clsMesazh(true, mesazhRuajtje);
        }

        /// <summary>
        /// kthen datatable trupi dokumenti te qender kosto sipas kokes
        /// </summary>
        /// <param name="idkoka">id e kokes se dokumentit </param>
        ///<returns>nje objekt datatable qe permban nje koleksion me te gjitha trupat e dokumentit te qendra kosto te kesaj koke  </returns>
        internal DataTable ktheGjitheTrupiQendraKostoNgaKoka(int idkoka)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDKOKA", idkoka, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_TRUPIQENDRAKOSTO_merrTrupatSipasKokes");
            return ds.Tables[0];
        }

        /// <summary>
        /// kthen datarow trupi dokumenti te qendra kosto  sipas idse se trupit te dokumentit
        /// </summary>
        /// <param name="idtrupi">id ritese e trupit te dokumentit </param>
        ///<returns>nje objekt datarow qe permban nje koleksion me te gjitha trupat e dokumentit te qendra kosto te kesaj id  </returns>
        internal DataRow ktheTrupiQendraKostoSipasID(int idtrupi)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDTRUPI", idtrupi, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_TRUPIQENDRAKOSTO_sel");
            if (ds == null || ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];
        }

        #endregion
    }
}
