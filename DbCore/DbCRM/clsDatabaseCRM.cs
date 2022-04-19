using System;
using System.Collections.Generic;
using System.Data;
using DbCore.IMBUtils.DataBase;
using DbCore.IMBUtils.Messages;

namespace DbCore.DbCRM
{
    public class clsDatabaseCRM : DbData
    {
        public clsDatabaseCRM()
            : base()
        { }


        public clsDatabaseCRM(DbData db)
            : base(db)
        {
        }
        public clsDatabaseCRM(string connectionName) : base(connectionName)
        {

        }

        #region KlientAnketa

        /// <summary>
        /// ekzekuton  prc_T_CRM_KLIENT_ANKETA_ins
        /// </summary>
        /// <param name="idKlientAnketa"></param>
        /// <param name="idKlient"></param>
        /// <param name="idKokaAnketa"></param>
        /// <param name="idKrijuesi"></param>
        /// <param name="dtKrijimi"></param>
        /// <param name="idstatudsok"></param>
        /// <returns></returns>
        internal clsMesazh ruajKlientAnketa(out int idKlientAnketa, int idKlient, int idKokaAnketa, int idKrijuesi, int idstatudsok, int idNdermarrje)
        { //metoda per ruajtjen e KlientAnketes
            idKlientAnketa = -1;

            dbManager.Open();
            clsMesazh mesazh = new clsMesazh();
            dbManager.CreateParameters(6);
            dbManager.AddParameters(0, "@IDKLIENTANKETE", idKlientAnketa, ParameterDirection.Output);
            dbManager.AddParameters(1, "@IDKLIENT", idKlient, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDKOKAANKETA", idKokaAnketa, ParameterDirection.Input);
            dbManager.AddParameters(3, "@IDKRIJUESI", idKrijuesi, ParameterDirection.Input);
            dbManager.AddParameters(4, "@IDSTATUSDOK", idstatudsok, ParameterDirection.Input);
            dbManager.AddParameters(5, "@IDNDERMARRJE", idNdermarrje, ParameterDirection.Input);

            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_CRM_KLIENT_ANKETA_ins");
            idKlientAnketa = int.Parse(dbManager.Parameters[0].Value.ToString());
            mesazh = new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);

            return mesazh;
        }

        /// <summary>
        /// ekzekuton prc_T_CRM_KLIENT_ANKETA_upd ne DB dhe kthen statusin e perfundimit te ekzekutimit te SP-se perkatese
        /// </summary>
        /// <param name="idKlientAnketa"></param>
        /// <param name="idKlient"></param>
        /// <param name="idKokaAnketa"></param>
        /// <param name="idModifikuesi"></param>
        /// <param name="idstatusdok"></param>
        /// <returns></returns>
        internal clsMesazh modifikoKlientAnkete(int idKlientAnketa, int idKlient, int idKokaAnketa, int idModifikuesi, int idstatusdok)
        {//metoda per modifikimin e KlientAnketes
            dbManager.Open();
            clsMesazh mesazh = new clsMesazh();
            dbManager.CreateParameters(5);
            dbManager.AddParameters(0, "@IDKLIENTANKETE", idKlientAnketa, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDKLIENT", idKlient, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDKOKAANKETA", idKokaAnketa, ParameterDirection.Input);
            dbManager.AddParameters(3, "@IDMODIFIKUESI", idModifikuesi, ParameterDirection.Input);
            dbManager.AddParameters(4, "@IDSTATUSDOK", idstatusdok, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_CRM_KLIENT_ANKETA_upd");
            mesazh = new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);

            return mesazh;
        }

        internal clsMesazh fshiKlientAnketaStatus(int idKlientAnketa, int idModifikuesi)
        {//metoda per fshirjen e KlientAnketa
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDKLIENTANKETE", idKlientAnketa, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDMODIFIKUESI", idModifikuesi, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_CRM_KLIENT_ANKETA_upddel");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
            return mesazh;
        }

        internal clsMesazh fshiKlientAnketaStatusSipasKlientitAktive(int idKlient, int idModifikuesi)
        {//metoda per fshirjen e KlientAnketa
            int result = -1;
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDKLIENT", idKlient, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDMODIFIKUESI", idModifikuesi, ParameterDirection.Input);
            result = dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_CRM_KLIENT_ANKETA_fshiAnketaSipasKlientitAktive");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
            return mesazh;
        }

        internal clsMesazh fshiKlientAnketaStatusSipasKlientit(int idKlient, int idModifikuesi)
        {//metoda per fshirjen e KlientAnketa
            int result = -1;
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDKLIENT", idKlient, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDMODIFIKUESI", idModifikuesi, ParameterDirection.Input);
            result = dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_CRM_KLIENT_ANKETA_fshiAnketaSipasKlientitAktive");
            clsMesazh mesazh;
            if (result == 0)
                mesazh = new clsMesazh(false, "Anketa nuk u fshi!");
            else
                mesazh = new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
            return mesazh;
        }

        /// <summary>
        /// kthen objektet KlientAnketa sipas idse
        /// </summary>
        ///<param name="idnjesia"> id e KlientAnketa</param>
        ///<returns> nje objekt colKlientAnketa qe permban nje koleksion me te gjithe klientAnketa-t me kete id</returns>
        internal DataRow merrKlientAnketa(int idKlientAnkete)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDKLIENTANKETE", idKlientAnkete, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_CRM_KLIENT_ANKETA_ktheKlientAnketaSipasId");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];
        }

        /// <summary>
        /// kthen objektet KlientAnketa sipas idse
        /// </summary>
        ///<param name="idnjesia"> id e KlientAnketa</param>
        ///<returns> nje objekt colKlientAnketa qe permban nje koleksion me te gjithe klientAnketa-t me kete id</returns>
        internal DataTable merrKlientAnketaSipasKlientit(int idKlient)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDKLIENT", idKlient, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_CRM_KLIENT_ANKETA_ktheKlientAnketaSipasIdKlienti");
            if (ds == null)
                return null;

            return ds.Tables[0];
        }

        internal bool ekzistonKlientAnketa(int idKlienti, int idKokaAnketa)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDKLIENTI", idKlienti, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDKOKAANKETA", idKokaAnketa, ParameterDirection.Input);
            bool ekziston = Convert.ToBoolean(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_CRM_KLIENT_ANKETA_ekzistonAnketaPerKlientin"));
            return ekziston;
        }

        #endregion KlientAnketa

        #region OPSIONE ANKETE

        internal clsMesazh ruajOpsionAnkete(out int idOpsion, string emertimi, int idstatudsok, int idPerdoruesi, int idnderm)
        {
            idOpsion = -1;
            dbManager.Open();
            clsMesazh mesazh = new clsMesazh();
            dbManager.CreateParameters(5);
            dbManager.AddParameters(0, "@IDOPSIONI", idOpsion, ParameterDirection.Output);
            dbManager.AddParameters(1, "@EMERTIMI", emertimi, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDSTATUSDOK", idstatudsok, ParameterDirection.Input);
            dbManager.AddParameters(3, "@IDKRIJUESI", idPerdoruesi, ParameterDirection.Input);
            dbManager.AddParameters(4, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_CRM_OPSIONE_ANKETE_ins");
            idOpsion = int.Parse(dbManager.Parameters[0].Value.ToString());
            mesazh = new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
            return mesazh;
        }

       

        internal clsMesazh modifikoOpsionAnkete(int idOpsion, string emertimi, int idstatudsok, int idModifikues, int idnderm)
        {
            dbManager.Open();
            clsMesazh mesazh = new clsMesazh();
            dbManager.CreateParameters(5);
            dbManager.AddParameters(0, "@IDOPSIONI", idOpsion, ParameterDirection.Input);
            dbManager.AddParameters(1, "@EMERTIMI", emertimi, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDSTATUSDOK", idstatudsok, ParameterDirection.Input);
            dbManager.AddParameters(3, "@IDMODIFIKUESI", idModifikues, ParameterDirection.Input);
            dbManager.AddParameters(4, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_CRM_OPSIONE_ANKETE_upd");
            mesazh = new clsMesazh(true, MessagesResource.Messages["msgModifikimiMeSukses"]);
            return mesazh;
        }

        internal clsMesazh fshiOpsionAnkete(int idOpsion, int idModifikues)
        {
            dbManager.Open();
            clsMesazh mesazh = new clsMesazh();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDOPSIONI", idOpsion, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDMODIFIKUES", idModifikues, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_CRM_OPSIONE_ANKETE_updDel");
            mesazh = new clsMesazh(true, MessagesResource.Messages["msgModifikimiMeSukses"]);
            return mesazh;
        }

        internal DataRow merrOpsionAnketeSipasId(int idOpsion)
        {
            dbManager.Open();
            clsMesazh mesazh = new clsMesazh();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDOPSIONI", idOpsion, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_CRM_OPSIONE_ANKETE_selGjitheFushatSipasIdOpsioni");
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];
        }

        public bool kaVeprimeOpsion(int id)
        {//kontrollon nqs ka veprime me kete opsion artikulli
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDOPSIONI", id, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_CRM_TRUPIANKETA_selSipasIdOpsioni");
            if (ds.Tables[0].Rows.Count >= 1)
                return true;
            else if (ds.Tables[0].Rows.Count == 0)
                return false;
            else return true;
        }

        public DataTable merrOpsionAnketeSipasNdermarrjes(int idNderm)
        {
            dbManager.Open();
            clsMesazh mesazh = new clsMesazh();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDNDERMARJE", idNderm, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_CRM_OPSIONE_ANKETE_selGjitheFushatSipasNdermarrje");
            return ds.Tables[0];
        }

        /// <summary>
        /// perdoret per te kontrolluar nqs ekziston nje opsion me kete emertim ne kete ndermarje
        /// behet kontrolli per qellime konsistence te informacionit ne DB, nuk lejon te celen opsione te ndryshem me te njejtin emertim
        /// </summary>
        ///<param name="idnderm"> id e ndermarjes</param>
        ///<param name="emertimi"> emertimi i opsionit</param>
        /// <returns>nje objekt boolean qe tregon nese ekziston apo jo nje opsion me kete emertim</returns>
        public bool ekzistonOpsionAnketa(int idopsioni, String emertimi, int idnderm)
        {//kontrollon nqs ekziston nje opsion ankete me kete emertim


            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@EMERTIMI", emertimi, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDOPSIONI", idopsioni, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_CRM_OPSIONE_ANKETE_ekzistonOpsionAnketa");
            if (ds.Tables[0].Rows.Count >= 1)
                return true;
            else if (ds.Tables[0].Rows.Count == 0)
                return false;
            else return true;
        }

        #endregion OPSIONE ANKETE

        #region KOKA ANKETA

        internal clsMesazh ruajKokaAnketa(out int idKokaAnkete, string kodi, string pershkrimi, DateTime dtFillimi, DateTime dtMbarimi, int idstatudsok, int idKrijuesi, int idNdermarrje)
        {
            idKokaAnkete = -1;
            dbManager.Open();
            clsMesazh mesazh = new clsMesazh();
            dbManager.CreateParameters(8);
            dbManager.AddParameters(0, "@IDKOKAANKETA", idKokaAnkete, ParameterDirection.Output);
            dbManager.AddParameters(1, "@KODI", kodi, ParameterDirection.Input);
            dbManager.AddParameters(2, "@PERSHKRIMI", pershkrimi, ParameterDirection.Input);
            dbManager.AddParameters(3, "@DTFILLIMI", dtFillimi, ParameterDirection.Input);
            dbManager.AddParameters(4, "@DTMBARIMI", dtMbarimi, ParameterDirection.Input);
            dbManager.AddParameters(5, "@IDSTATUSDOK", idstatudsok, ParameterDirection.Input);
            dbManager.AddParameters(6, "@IDKRIJUESI", idKrijuesi, ParameterDirection.Input);
            dbManager.AddParameters(7, "@IDNDERMARJE", idNdermarrje, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_CRM_KOKAANKETA_ins");
            idKokaAnkete = int.Parse(dbManager.Parameters[0].Value.ToString());
            mesazh = new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
            return mesazh;
        }

        internal clsMesazh modifikoKokaAnkete(int idKokaAnkete, string kodi, string pershkrimi, DateTime dtFillimi, DateTime dtMbarimi, int idstatudsok, int idModifikues, int idNdermarrje)
        {
            dbManager.Open();
            clsMesazh mesazh = new clsMesazh();
            dbManager.CreateParameters(8);
            dbManager.AddParameters(0, "@IDKOKAANKETA", idKokaAnkete, ParameterDirection.Input);
            dbManager.AddParameters(1, "@KODI", kodi, ParameterDirection.Input);
            dbManager.AddParameters(2, "@PERSHKRIMI", pershkrimi, ParameterDirection.Input);
            dbManager.AddParameters(3, "@DTFILLIMI", dtFillimi, ParameterDirection.Input);
            dbManager.AddParameters(4, "@DTMBARIMI", dtMbarimi, ParameterDirection.Input);
            dbManager.AddParameters(5, "@IDSTATUSDOK", idstatudsok, ParameterDirection.Input);
            dbManager.AddParameters(6, "@IDMODIFIKUESI", idModifikues, ParameterDirection.Input);
            dbManager.AddParameters(7, "@IDNDERMARJE", idNdermarrje, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_CRM_KOKAANKETA_upd");
            mesazh = new clsMesazh(true, MessagesResource.Messages["msgModifikimiMeSukses"]);
            return mesazh;
        }

        internal clsMesazh fshiKokaAnkete(int idKoka, int idModifikues)
        {
            dbManager.Open();
            clsMesazh mesazh = new clsMesazh();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDKOKAANKETA", idKoka, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDMODIFIKUESI", idModifikues, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_CRM_KOKAANKETA_upddel");
            mesazh = new clsMesazh(true, MessagesResource.Messages["msgModifikimiMeSukses"]);
            return mesazh;
        }

        /// <summary>
        /// perdoret per te kontrolluar nqs ekziston nje opsion me kete emertim ne kete ndermarje
        /// behet kontrolli per qellime konsistence te informacionit ne DB, nuk lejon te celen opsione te ndryshem me te njejtin emertim
        /// </summary>
        ///<param name="idnderm"> id e ndermarjes</param>
        ///<param name="emertimi"> emertimi i opsionit</param>
        /// <returns>nje objekt boolean qe tregon nese ekziston apo jo nje opsion me kete emertim</returns>
        public bool ekzistonAnketa(String kodi, int idnderm)
        {//kontrollon nqs ekziston nje opsion ankete me kete emertim


            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@KODI", kodi, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_CRM_KOKAANKETA_ekzistonAnketa");
            if (ds.Tables[0].Rows.Count >= 1)
                return true;
            else if (ds.Tables[0].Rows.Count == 0)
                return false;
            else return true;
        }

        public bool kaVeprimeCRMListaAnketa(int idkokaanketa)
        {//kontrollon nqs ka veprime me kete ankete
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDKOKAANKETA", idkokaanketa, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "T_CRM_KLIENT_ANKETA_selSipasIdKokaAnketa");
            if (ds.Tables[0].Rows.Count >= 1)
                return true;
            else if (ds.Tables[0].Rows.Count == 0)
                return false;
            else return true;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="idnderm"></param>
        /// <param name="idklient"></param>
        /// <param name="dtfillimi"></param>
        /// <param name="dtmbarimi"></param>
        /// <returns></returns>
        public bool kaPrerjeAnketashKlienti(int idnderm, int idklient, DateTime dtfillimi, DateTime dtmbarimi)
        {//kontrollon

            dbManager.Open();
            dbManager.CreateParameters(4);
            dbManager.AddParameters(0, "@idklient", idklient, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            dbManager.AddParameters(2, "@dtfillimi", dtfillimi, ParameterDirection.Input);
            dbManager.AddParameters(3, "@dtmbarimi", dtmbarimi, ParameterDirection.Input);
            //dbManager.AddParameters(4, "@idKokaAnketa", idKokaAnketa, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_CRM_KOKAANKETA_kaPrerjeAnketashSipasKlientit");
            if (ds.Tables[0].Rows.Count >= 1)
                return true;
            else if (ds.Tables[0].Rows.Count == 0)
                return false;
            else return true;
        }

        internal DataRow merrKokaAnketeSipasId(int idKoka)
        {
            dbManager.Open();
            clsMesazh mesazh = new clsMesazh();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDKOKAANKETA", idKoka, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_CRM_KOKAANKETA_sel");
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];
        }

        internal DataTable merrKokaAnketeSipasNdermarrjes(int idNderm)
        {
            dbManager.Open();
            clsMesazh mesazh = new clsMesazh();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDNDERMARRJE", idNderm, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_CRM_KOKAANKETA_selSipasIdNdermarrje");
            return ds.Tables[0];
        }

        internal DataTable merrKokaAnketeSipasNdermarrjesAktive(int idNderm)
        {
            dbManager.Open();
            clsMesazh mesazh = new clsMesazh();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDNDERMARRJE", idNderm, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_CRM_KOKAANKETA_selSipasIdNdermarrjeAktive");
            return ds.Tables[0];
        }

        internal bool EshteAnketaELidhur(int idKokaAnkete)
        {
            var exist = 0;
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddInputParameters("@IDKOKAANKETA", idKokaAnkete);
            int.TryParse(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_CRM_KOKAANKETA_eshteLidhur")?.ToString(), out exist);
            return exist==1;
        }

        #endregion KOKA ANKETA

        #region TRUPI ANKETA

        internal clsMesazh ruajTrupiAnketa(out int idTrupiAnkete, int idKokaAnketa, int idOpsioniAnkete, bool detyrueshme, int idStatuDok)
        {
            idTrupiAnkete = -1;
            dbManager.Open();
            clsMesazh mesazh = new clsMesazh();
            dbManager.CreateParameters(5);
            dbManager.AddParameters(0, "@IDTRUPIANKETA", idTrupiAnkete, ParameterDirection.Output);
            dbManager.AddParameters(1, "@IDKOKAANKETA", idKokaAnketa, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDOPSIONIANKETA", idOpsioniAnkete, ParameterDirection.Input);
            dbManager.AddParameters(3, "@DETYRUESHME", detyrueshme, ParameterDirection.Input);
            dbManager.AddParameters(4, "@IDSTATUSDOK", idStatuDok, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_CRM_TRUPIANKETA_ins");
            idTrupiAnkete = int.Parse(dbManager.Parameters[0].Value.ToString());
            mesazh = new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
            return mesazh;
        }

        internal clsMesazh modifikoTrupiAnkete(int idTrupiAnkete, int idKokaAnketa, int idOpsioniAnkete, bool detyrueshme, int idStatuDok)
        {
            dbManager.Open();
            clsMesazh mesazh = new clsMesazh();
            dbManager.CreateParameters(5);
            dbManager.AddParameters(0, "@IDTRUPIANKETA", idTrupiAnkete, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDKOKAANKETA", idKokaAnketa, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDOPSIONIANKETA", idOpsioniAnkete, ParameterDirection.Input);
            dbManager.AddParameters(3, "@DETYRUESHME", detyrueshme, ParameterDirection.Input);
            dbManager.AddParameters(4, "@IDSTATUSDOK", idStatuDok, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_CRM_TRUPIANKETA_upd");
            mesazh = new clsMesazh(true, MessagesResource.Messages["msgModifikimiMeSukses"]);
            return mesazh;
        }

        internal clsMesazh fshiTrupAnkete(int idKoka)
        {
            dbManager.Open();
            clsMesazh mesazh = new clsMesazh();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDKOKAANKETA", idKoka, ParameterDirection.Input);

            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_CRM_TRUPIANKETA_updDel");
            mesazh = new clsMesazh(true, MessagesResource.Messages["msgModifikimiMeSukses"]);
            return mesazh;
        }

        internal DataRow merrTrupAnketeSipasId(int idTrupi)
        {
            dbManager.Open();
            clsMesazh mesazh = new clsMesazh();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDTRUPIANKETA", idTrupi, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_CRM_TRUPIANKETA_selSipasIdTrupi");
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];
        }

        internal DataTable merrTrupAnketeSipasIdKoka(int idKoka)
        {
            dbManager.Open();
            clsMesazh mesazh = new clsMesazh();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDKOKAANKETA", idKoka, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_CRM_TRUPIANKETA_selSipasIdKoka");
            return ds.Tables[0];
        }

        internal DataTable merrTrupAnketeAllOpsioneSipasNdermarje(int idndermarje)
        {
            dbManager.Open();
            clsMesazh mesazh = new clsMesazh();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@idndermarje", idndermarje, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_CRM_TRUPIANKETA_selAllOpsioneSipasNdermarje");
            return ds.Tables[0];
        }

        internal DataTable merrTrupAnketeOpsioneTePamaraSipasNdermarje(int idndermarje, int idkoka)
        {
            dbManager.Open();
            clsMesazh mesazh = new clsMesazh();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@idndermarje", idndermarje, ParameterDirection.Input);
            dbManager.AddParameters(1, "@idkoka", idkoka, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_CRM_TRUPIANKETA_selOpsioneTepamaraSipasNdermarje");
            return ds.Tables[0];
        }

        #endregion TRUPI ANKETA

        #region SKEDULER

        internal clsMesazh ruajTakim(out int idAuto, int idPerdoruesi, int idKlienti, DateTime data, int idKrijuesi, DateTime dtKrijimi, int idModifikimi, DateTime dtModifikimi, int idStatusDok, DateTime startDate, DateTime endTime, bool allDay, string description, int idNdermarje, int lloji, string koordFillimi, string koordMbarimi, int status)
        {
            idAuto = -1;
            dbManager.Open();
            clsMesazh mesazh = new clsMesazh();
            dbManager.CreateParameters(15);
            dbManager.AddParameters(0, "@IDAUTO", idAuto, ParameterDirection.Output);
            dbManager.AddParameters(1, "@IDPERDORUESI", idPerdoruesi, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDKLIENTI", idKlienti, ParameterDirection.Input);
            dbManager.AddParameters(3, "@DATA", data, ParameterDirection.Input);
            dbManager.AddParameters(4, "@IDKRIJUESI", idKrijuesi, ParameterDirection.Input);
            dbManager.AddParameters(5, "@IDSTATUSDOK", idStatusDok, ParameterDirection.Input);
            dbManager.AddParameters(6, "@STARTDATE", startDate, ParameterDirection.Input);
            dbManager.AddParameters(7, "@ENDTIME", endTime, ParameterDirection.Input);
            dbManager.AddParameters(8, "@ALLDAY", allDay, ParameterDirection.Input);
            dbManager.AddParameters(9, "@DESCRIPTION", description, ParameterDirection.Input);
            dbManager.AddParameters(10, "@IDNDERMARJE", idNdermarje, ParameterDirection.Input);
            dbManager.AddParameters(11, "@LLOJI", lloji, ParameterDirection.Input);
            if (string.IsNullOrWhiteSpace(koordFillimi)) dbManager.AddParameters(12, "@KOORDINATEFILLIMI", DBNull.Value, ParameterDirection.Input); else dbManager.AddParameters(12, "@KOORDINATEFILLIMI", koordFillimi, ParameterDirection.Input);

            if (string.IsNullOrWhiteSpace(koordFillimi)) dbManager.AddParameters(13, "@KOORDINATEMBARIMI", DBNull.Value, ParameterDirection.Input); else dbManager.AddParameters(13, "@KOORDINATEMBARIMI", koordFillimi, ParameterDirection.Input);

            dbManager.AddParameters(14, "@STATUS", status, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_CRM_SKEDULER_ins");
            idAuto = int.Parse(dbManager.Parameters[0].Value.ToString());
            mesazh = new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
            return mesazh;
        }

        internal clsMesazh modifikoTakim(int idAuto, int idPerdoruesi, int idKlienti, DateTime data, int idKrijuesi, DateTime dtKrijimi, int idModifikimi, DateTime dtModifikimi, int idStatusDok, DateTime startDate, DateTime endTime, bool allDay, string description, int idNdermarje, int lloji, string koordFillimi, string koordMbarimi, int status)
        {
            dbManager.Open();
            clsMesazh mesazh = new clsMesazh();
            dbManager.CreateParameters(15);
            dbManager.AddParameters(0, "@IDAUTO", idAuto, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUESI", idPerdoruesi, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDKLIENTI", idKlienti, ParameterDirection.Input);
            dbManager.AddParameters(3, "@DATA", data, ParameterDirection.Input);
            dbManager.AddParameters(4, "@IDMODIFIKIMI", idModifikimi, ParameterDirection.Input);
            dbManager.AddParameters(5, "@IDSTATUSDOK", idStatusDok, ParameterDirection.Input);
            dbManager.AddParameters(6, "@STARTDATE", startDate, ParameterDirection.Input);
            dbManager.AddParameters(7, "@ENDTIME", endTime, ParameterDirection.Input);
            dbManager.AddParameters(8, "@ALLDAY", allDay, ParameterDirection.Input);
            dbManager.AddParameters(9, "@DESCRIPTION", description, ParameterDirection.Input);
            dbManager.AddParameters(10, "@IDNDERMARJE", idNdermarje, ParameterDirection.Input);
            dbManager.AddParameters(11, "@LLOJI", lloji, ParameterDirection.Input);
            dbManager.AddParameters(12, "@KOORDINATEFILLIMI", DBNull.Value, ParameterDirection.Input);
            dbManager.AddParameters(13, "@KOORDINATEMBARIMI", DBNull.Value, ParameterDirection.Input);
            dbManager.AddParameters(14, "@STATUS", status, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_CRM_SKEDULER_upd");
            mesazh = new clsMesazh(true, MessagesResource.Messages["msgModifikimiMeSukses"]);
            return mesazh;
        }

        internal clsMesazh fshiTakim(int idAuto, int idModifikues)
        {
            dbManager.Open();
            clsMesazh mesazh = new clsMesazh();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDAUTO", idAuto, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDMODIFIKUESI", idModifikues, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_CRM_SKEDULER_updDel");
            mesazh = new clsMesazh(true, MessagesResource.Messages["msgModifikimiMeSukses"]);
            return mesazh;
        }

        internal DataRow merrTakimSipasId(int idAuto)
        {
            dbManager.Open();
            clsMesazh mesazh = new clsMesazh();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDAUTO", idAuto, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_CRM_SKEDULER_selGjitheFushatSipasIdTakimi");
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];
        }

        internal bool ekzistonTakimKlientiNeKeteDate(int idAuto, int idklienti, DateTime data)
        {
            dbManager.Open();
            clsMesazh mesazh = new clsMesazh();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@IDKlienti", idklienti, ParameterDirection.Input);
            dbManager.AddParameters(1, "@data", data, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDAUTO", idAuto, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_CRM_SKEDULER_ekzistonTakimKlientiNeKeteDate");
            if (ds.Tables[0].Rows.Count == 0)
                return false;
            else if (ds.Tables[0].Rows.Count > 0)
                return true;
            else return true;
        }

        internal bool ekzistonDetyreNeKeteDatePerAgjent(int idAuto, int idAgjenti, int idDetyre, DateTime data, int idNdermarrje)
        {
            dbManager.Open();
            clsMesazh mesazh = new clsMesazh();
            dbManager.CreateParameters(5);
            dbManager.AddParameters(0, "IDAUTO", idAuto, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDAGJENTI", idAgjenti, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDDETYRE", idDetyre, ParameterDirection.Input);
            dbManager.AddParameters(3, "@DATA", data, ParameterDirection.Input);
            dbManager.AddParameters(4, "@IDNDERMARRJE", idNdermarrje, ParameterDirection.Input);
            int result = Convert.ToInt32(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_CRM_SKEDULER_ekzistonDetyreNeKeteDitePerAgjent"));

            return (result > 0);
        }

        internal DataTable merrTakimeSipasNdermarrjes(int idNderm)
        {
            dbManager.Open();
            clsMesazh mesazh = new clsMesazh();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDNDERMARJE", idNderm, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_CRM_SKEDULER_selGjitheFushatSipasNdermarrje");
            return ds.Tables[0];
        }

        internal DataTable merrTakimeEPanisurSipasNdermarrjesDheDates(string kodNdermarrje, DateTime data)
        {
            dbManager.Open();
            clsMesazh mesazh = new clsMesazh();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@KODNDERMARRJE", kodNdermarrje, ParameterDirection.Input);
            dbManager.AddParameters(1, "@DATA", data, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_CRM_SKEDULER_merrGJitheTakimetEPanisurSipasNdermarrjesDheDates");
            return ds.Tables[0];
        }

        internal DataTable merrTakimeSipasNdermarrjesAndAgjent(int idNderm, int idagjent)
        {
            dbManager.Open();
            clsMesazh mesazh = new clsMesazh();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDNDERMARJE", idNderm, ParameterDirection.Input);
            dbManager.AddParameters(1, "@idagjent", idagjent, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_CRM_SKEDULER_selGjitheFushatSipasNdermarrjeAndAgjenti");
            return ds.Tables[0];
        }

        internal clsMesazh KlonoTakimet(DateTime fromDateStart, DateTime fromDateEnd, DateTime toDateStart, DateTime toDateEnd, int idAgjenti, int idPerdoruesi, int idNdermarrje, bool klonoDetyra, bool klonoKlient)
        {
            dbManager.Open();
            dbManager.CreateParameters(9);
            dbManager.AddParameters(0, "@FROMSTARTDATE", fromDateStart, ParameterDirection.Input);
            dbManager.AddParameters(1, "@FROMENDDATE", fromDateEnd, ParameterDirection.Input);
            dbManager.AddParameters(2, "@TOSTARTDATE", toDateStart, ParameterDirection.Input);
            dbManager.AddParameters(3, "@TOENDDATE", toDateEnd, ParameterDirection.Input);
            dbManager.AddParameters(4, "@IDPERDORUESI", idPerdoruesi, ParameterDirection.Input);
            dbManager.AddParameters(5, "@IDNDERMARRJE", idNdermarrje, ParameterDirection.Input);
            dbManager.AddParameters(6, "@IDAGJENTI", idAgjenti, ParameterDirection.Input);
            dbManager.AddParameters(7, "@KLONODETYRA", klonoDetyra, ParameterDirection.Input);
            dbManager.AddParameters(8, "@KLONOKLIENT", klonoKlient, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_CRM_SKEDULER_klonoTakimet");
            return new clsMesazh(true, "Klonimi perfundoi me sukses!");
        }

        internal clsMesazh KontrolloDatat(DateTime fromDateStart, DateTime fromDateEnd, DateTime toDateStart, DateTime toDateEnd, int idNdermarrje, int idPerdoruesi, bool klonoDetyra, bool klonoKlient)
        {
            bool dataMeTakime = false;
            bool dataPaTakime = false;
            clsMesazh mesazh;

            dbManager.Open();
            dbManager.CreateParameters(10);
            dbManager.AddParameters(0, "@FROMSTARTDATE", fromDateStart, ParameterDirection.Input);
            dbManager.AddParameters(1, "@FROMENDDATE", fromDateEnd, ParameterDirection.Input);
            dbManager.AddParameters(2, "@TOSTARTDATE", toDateStart, ParameterDirection.Input);
            dbManager.AddParameters(3, "@TOENDDATE", toDateEnd, ParameterDirection.Input);
            dbManager.AddParameters(4, "@DATAMETAKIME", dataMeTakime, ParameterDirection.Output);
            dbManager.AddParameters(5, "@DATAPATAKIME", dataPaTakime, ParameterDirection.Output);
            dbManager.AddParameters(6, "@IDNDERMARRJE", idNdermarrje, ParameterDirection.Input);
            dbManager.AddParameters(7, "@IDPERDORUESI", idPerdoruesi, ParameterDirection.Input);
            dbManager.AddParameters(8, "@KLONODETYRA", klonoDetyra, ParameterDirection.Input);
            dbManager.AddParameters(9, "@KLONOKLIENT", klonoKlient, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "PRC_T_CRM_SKEDULER_ekzistojneTakime");

            bool.TryParse(dbManager.Parameters[4].Value.ToString(), out dataMeTakime);
            bool.TryParse(dbManager.Parameters[5].Value.ToString(), out dataPaTakime);
            if (dataMeTakime || dataPaTakime)
            {
                string pershkrim = "";
                mesazh = new clsMesazh(TipMesazhi.Informim, pershkrim);

                if (dataPaTakime)
                {
                    pershkrim += "Periudha e klonimit ka data pa takime! ";
                }
                if (dataMeTakime)
                {
                    pershkrim += "Periudha  e ardhme ka data me takime!";
                }
                mesazh = new clsMesazh(TipMesazhi.Informim, pershkrim);
            }
            else
                mesazh = new clsMesazh(true);
            return mesazh;
        }

        #endregion SKEDULER

        #region KokaKlientAnketaAgjent

        /// <summary>
        /// ekzekuton  prc_T_CRM_KLIENT_ANKETA_ins
        /// </summary>
        /// <param name="idKlientAnketa"></param>
        /// <param name="idKlient"></param>
        /// <param name="idKokaAnketa"></param>
        /// <param name="idKrijuesi"></param>
        /// <param name="dtKrijimi"></param>
        /// <param name="idstatudsok"></param>
        /// <returns></returns>
        internal clsMesazh ruajKokaKlientAnketaAgjent(out int idKokaKlientAnketa, int IdKlientAnkete, int idPerdoruesi, string shenime, DateTime dtVeprimi, int idndermarje)
        { //metoda per ruajtjen e KlientAnketes
            idKokaKlientAnketa = -1;
            dbManager.Open();
            clsMesazh mesazh = new clsMesazh();
            dbManager.CreateParameters(6);
            dbManager.AddParameters(0, "@IDKOKAKLIENTANKETE", idKokaKlientAnketa, ParameterDirection.Output);
            dbManager.AddParameters(1, "@IDKLIENTANKETE", IdKlientAnkete, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDPERDORUESI", idPerdoruesi, ParameterDirection.Input);
            dbManager.AddParameters(3, "@SHENIME", shenime, ParameterDirection.Input);
            dbManager.AddParameters(4, "@DTVEPRIMI", dtVeprimi, ParameterDirection.Output);
            dbManager.AddParameters(5, "@IDNDERMARJE", idndermarje, ParameterDirection.Output);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_CRM_KOKAKLIENT_ANKETA_AGJENT_ins");
            idKokaKlientAnketa = int.Parse(dbManager.Parameters[0].Value.ToString());
            mesazh = new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
            return mesazh;
        }

        internal DataTable merrKlientAnketaAgjentSipasNdermjes(int idndermarje, string datanga, string dataderi, int idPerdoruesi)
        {
            dbManager.Open();
            dbManager.CreateParameters(4);
            dbManager.AddParameters(0, "@IDNDERMARJE", idndermarje, ParameterDirection.Input);
            dbManager.AddParameters(1, "@datanga", datanga, ParameterDirection.Input);
            dbManager.AddParameters(2, "@dataderi", dataderi, ParameterDirection.Input);
            dbManager.AddParameters(3, "@IDPERDORUES", idPerdoruesi, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_CRM_KOKAKLIENT_ANKETA_AGJENT_selSipasNdermarjesDT");
            if (ds == null)
                return null;

            return ds.Tables[0];
        }

        /// <summary>
        /// kthen objektet KokaKlientAnketaAgjent sipas idse dhe ndermarrjes
        /// </summary>
        /// <param name="idKokaKlientAnkete"></param>
        /// <param name="idndermarje"></param>
        /// <returns></returns>
        internal DataRow merrKokaKlientAnketaAgjent(int idKokaKlientAnkete, int idndermarje)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDKOKAKLIENTANKETE", idKokaKlientAnkete, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idndermarje, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_CRM_KOKAKLIENT_ANKETA_AGJENT_ktheKokaKlientAnketaAgjentSipasId");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];
        }

        #endregion KokaKlientAnketaAgjent

        #region TRUPIKLIENTANKETAAGJENT

        internal clsMesazh ruajTrupiKlientAnketaAgjent(out int idTrupiKlientAnkete, int idKokaKlientAnketa, int idTrupiAnkete, int statusveprimi, string shenimetrupi)
        {
            idTrupiKlientAnkete = -1;
            dbManager.Open();
            clsMesazh mesazh = new clsMesazh();
            dbManager.CreateParameters(5);
            dbManager.AddParameters(0, "@IDTRUPIKLIENTANKETE", idTrupiKlientAnkete, ParameterDirection.Output);
            dbManager.AddParameters(1, "@IDKOKAKLIENTANKETE", idKokaKlientAnketa, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDTRUPIANKETA", idTrupiAnkete, ParameterDirection.Input);
            dbManager.AddParameters(3, "@STATUSVEPRIMI", statusveprimi, ParameterDirection.Input);
            dbManager.AddParameters(4, "@SHENIMETRUPI", shenimetrupi, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_CRM_TRUPIKLIENT_ANKETA_AGJENT_ins");
            idTrupiKlientAnkete = int.Parse(dbManager.Parameters[0].Value.ToString());
            mesazh = new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
            return mesazh;
        }

        internal clsMesazh modifikoTrupiKlientAnketeAgjent(int idTrupiKlientAnkete, int idKokaKlientAnketa, int idTrupiAnkete, int statusveprimi, string shenimetrupi)
        {
            dbManager.Open();
            clsMesazh mesazh = new clsMesazh();
            dbManager.CreateParameters(5);
            dbManager.AddParameters(0, "@IDTRUPIKLIENTANKETE", idTrupiKlientAnkete, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDKOKAKLIENTANKETE", idKokaKlientAnketa, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDTRUPIANKETA", idTrupiAnkete, ParameterDirection.Input);
            dbManager.AddParameters(3, "@STATUSVEPRIMI", statusveprimi, ParameterDirection.Input);
            dbManager.AddParameters(4, "@SHENIMETRUPI", shenimetrupi, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_CRM_TRUPIKLIENT_ANKETA_AGJENT_upd");
            mesazh = new clsMesazh(true, MessagesResource.Messages["msgModifikimiMeSukses"]);
            return mesazh;
        }

        internal bool kaShenimeTrupiKlientAnketaAgjent(int idTrupi)
        {
            dbManager.Open();
            clsMesazh mesazh = new clsMesazh();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDTRUPI", idTrupi, ParameterDirection.Input);
            int kaShenime = Convert.ToInt32(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_CRM_TRUPIKLIENT_ANKETA_AGJENT_kaShenime"));

            return kaShenime > 0;
        }

        internal DataRow merrTrupiKlientAnketeAgjentSipasId(int idTrupi)
        {
            dbManager.Open();
            clsMesazh mesazh = new clsMesazh();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDTRUPIKLIENTANKETE", idTrupi, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_CRM_TRUPIKLIENT_ANKETA_AGJENT_selSipasIdTrupi");
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];
        }

        internal DataTable merrTrupiKlientAnketeAgjentSipasIdKoka(int idKoka)
        {
            dbManager.Open();
            clsMesazh mesazh = new clsMesazh();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDKOKAKLIENTANKETE", idKoka, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_CRM_TRUPIKLIENT_ANKETA_AGJENT_selSipasIdKoka");
            return ds.Tables[0];
        }

        internal DataTable merrTrupiKlientAnketeAgjentSipasIdKokaDt(int idKoka)
        {
            dbManager.Open();
            clsMesazh mesazh = new clsMesazh();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDKOKAKLIENTANKETE", idKoka, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_CRM_TRUPIKLIENT_ANKETA_AGJENT_selSipasIdKokaDt");
            return ds.Tables[0];
        }

        #endregion TRUPIKLIENTANKETAAGJENT

        #region FotoAnkete

        public byte[] merrFoto(int idtrupi)
        {


            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDTRUPIKLIENTANKETE", idtrupi, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_CRM_FOTO_ANKETE_ktheFoto");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            DataRow dbDataRowNdermarrja = ds.Tables[0].Rows[0];
            if (!String.IsNullOrEmpty(dbDataRowNdermarrja["FOTO"].ToString()))
                return (byte[])dbDataRowNdermarrja["FOTO"];
            return null;
        }

        internal clsMesazh ruajFotoAnkete(out int idFotoAnkete, int idTrupiKlientAnkete, byte[] foto)
        {
            idFotoAnkete = -1;
            dbManager.Open();
            clsMesazh mesazh = new clsMesazh();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@IDFOTOANKETE", idFotoAnkete, ParameterDirection.Output);
            dbManager.AddParameters(1, "@IDTRUPIKLIENTANKETE", idTrupiKlientAnkete, ParameterDirection.Input);
            dbManager.AddParameters(2, "@FOTO", foto, ParameterDirection.Input);

            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_CRM_FOTO_ANKETE_ins");
            idFotoAnkete = int.Parse(dbManager.Parameters[0].Value.ToString());
            mesazh = new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
            return mesazh;
        }

        #endregion FotoAnkete

        #region DETYRA

        internal DataTable merrDetyratPerNdermarrjeKategoriAutorizim(int idNdermarrje, int idPerdoruesi, int kategoria)
        {
            dbManager.Open();
            clsMesazh mesazh = new clsMesazh();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@IDNDERMARRJE", idNdermarrje, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUESI", idPerdoruesi, ParameterDirection.Input);
            dbManager.AddParameters(2, "@KATEGORIA", kategoria, ParameterDirection.Input);

            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_CRM_DETYRA_MERR_DETYRA_KATEGORI_AUTORIZIMDT");
            return ds.Tables[0];
        }

        internal DataRow merrDetyreSipasID(int idDetyre)
        {
            dbManager.Open();
            clsMesazh mesazh = new clsMesazh();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDDETYRE", idDetyre, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "T_CRM_DETYRA_MERR_DETYRE_SIPAS_ID");
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];
        }

        internal clsMesazh fshiDetyre(int idDetyre, int idModikuesi)
        {
            dbManager.Open();
            clsMesazh mesazh = new clsMesazh();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDDETYRE", idDetyre, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDMODIFIKUESi", idModikuesi, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_CRM_DETYRA_updDel");
            mesazh = new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
            return mesazh;
        }

        internal bool ekzistonDetyreMeKeteKod(int idNdermarrje, string kodi)
        {
            clsMesazh mesazh = new clsMesazh();
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDNDERMARRJE", idNdermarrje, ParameterDirection.Input);
            dbManager.AddParameters(1, "@KODI", kodi, ParameterDirection.Input);
            int pergjigje = Convert.ToInt32(dbManager.ExecuteScalar(CommandType.StoredProcedure, "PRC_T_CRM_DETYRA_eksistonDetyreMeKeteKod"));
            return Convert.ToBoolean(pergjigje);
        }

        internal clsMesazh ruajDetyre(out int idDetyre, int rendesia, int kategoria, string kodi, int idKrijuesi, int idStatusDok, string pershkrimi, int idNdermarrje)
        {
            idDetyre = -1;
            dbManager.Open();
            clsMesazh mesazh = new clsMesazh();
            dbManager.CreateParameters(8);
            dbManager.AddParameters(0, "@IDDETYRE", idDetyre, ParameterDirection.Output);
            dbManager.AddParameters(1, "@KODI", kodi, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDKRIJUESI", idKrijuesi, ParameterDirection.Input);
            dbManager.AddParameters(3, "@IDSTATUSDOK", idStatusDok, ParameterDirection.Input);
            dbManager.AddParameters(4, "@IDNDERMARJE", idNdermarrje, ParameterDirection.Input);
            dbManager.AddParameters(5, "@KATEGORIA", kategoria, ParameterDirection.Input);
            dbManager.AddParameters(6, "@RENDESIA", rendesia, ParameterDirection.Input);
            dbManager.AddParameters(7, "@PERSHKRIMI", pershkrimi, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_CRM_DETYRA_ins");
            idDetyre = int.Parse(dbManager.Parameters[0].Value.ToString());
            mesazh = new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
            return mesazh;
        }

        internal DataTable merrDetyratPerNdermarrje(int idNdermarrje)
        {
            dbManager.Open();
            clsMesazh mesazh = new clsMesazh();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDNDERMARRJE", idNdermarrje, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_CRM_DETYRA_MERR_GJITHE_DETYRAT");
            return ds.Tables[0];
        }

        internal clsMesazh modifikoDetyre(int idDetyre, int rendesia, int kategoria, string kodi, int idModifikuesi, int idStatusDok, string pershkrimi, int idNdermarrje)
        {
            dbManager.Open();
            clsMesazh mesazh = new clsMesazh();
            dbManager.CreateParameters(7);
            dbManager.AddParameters(0, "@IDDETYRE", idDetyre, ParameterDirection.Input);
            dbManager.AddParameters(1, "@KODI", kodi, ParameterDirection.Input);
            dbManager.AddParameters(2, "@KATEGORIA", kategoria, ParameterDirection.Input);
            dbManager.AddParameters(3, "@RENDESIA", rendesia, ParameterDirection.Input);
            dbManager.AddParameters(4, "@PERSHKRIMI", pershkrimi, ParameterDirection.Input);
            dbManager.AddParameters(5, "@IDSTATUSDOK", idStatusDok, ParameterDirection.Input);
            dbManager.AddParameters(6, "@IDMODIFIMI", idModifikuesi, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "PRC_T_CRM_DETYRA_UPD");

            return new clsMesazh(true, MessagesResource.Messages["msgModifikimiMeSukses"]);
        }

        internal DataTable merrDetyreHistoriku(int idndermarje, int idPerdoruesi)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDNDERMARRJE", idndermarje, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUESI", idPerdoruesi, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_CRM_SKEDULER_ktheHistorikDetyrash");
            if (ds == null)
                return null;

            return ds.Tables[0];
        }

        internal DataTable merrDetyratSipasKategorisDheNdermarrjes(int idNdermarrje, int kategoria)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDNDERMARRJE", idNdermarrje, ParameterDirection.Input);
            dbManager.AddParameters(1, "@KATEGORIA", kategoria, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_CRM_DETYRA_MerrDetyraSipasNdermarrjeDheKategoris");
            if (ds == null)
                return null;

            return ds.Tables[0];
        }

        #endregion DETYRA

        #region DETYREKLIENT

        internal DataRow merrDetyreKlient(int idDetyreKlient)
        {
            dbManager.Open();
            clsMesazh mesazh = new clsMesazh();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDDETYREKLIENT", idDetyreKlient, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "T_CRM_DETYRE_KLIENT_merrDetyreKlientSipasId");
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];
        }

        internal clsMesazh ruajDetyreKlient(out int idDetyreKlient, int IdKlient, int idDetyre, int idNdermarrje, DateTime dtFillimi, DateTime dtMbarimi, int idKrijuesi, int idStatusDok)
        {
            idDetyreKlient = -1;
            dbManager.Open();
            clsMesazh mesazh = new clsMesazh();
            dbManager.CreateParameters(8);
            dbManager.AddParameters(0, "@IDDETYREKLIENT", idDetyreKlient, ParameterDirection.Output);
            dbManager.AddParameters(1, "@IDKLIENT", IdKlient, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDDETYRE", idDetyre, ParameterDirection.Input);
            dbManager.AddParameters(3, "@IDSTATUSDOK", idStatusDok, ParameterDirection.Input);
            dbManager.AddParameters(4, "@IDNDERMARJE", idNdermarrje, ParameterDirection.Input);

            dbManager.AddParameters(5, "@IDKRIJUESI", idKrijuesi, ParameterDirection.Input);
            dbManager.AddParameters(6, "@DTFILLIMI", dtFillimi, ParameterDirection.Input);
            dbManager.AddParameters(7, "@DTMBARIMI", dtMbarimi, ParameterDirection.Input);

            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "T_CRM_DETYRE_KLIENT_ins");
            idDetyreKlient = int.Parse(dbManager.Parameters[0].Value.ToString());
            mesazh = new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
            return mesazh;
        }

        internal clsMesazh modifikoDetyreKlient(int idDetyreKlient, int idKlient, int IdDetyre, int idNdermarrje, DateTime dtFillimi, DateTime dtMbarimi, int idModifikuesi, int idStatusDok)
        {
            dbManager.Open();
            clsMesazh mesazh = new clsMesazh();
            dbManager.CreateParameters(8);
            dbManager.AddParameters(0, "@IDDETYREKLIENT", idDetyreKlient, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDKLIENT", idKlient, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDDETYRE", IdDetyre, ParameterDirection.Input);
            dbManager.AddParameters(3, "@IDSTATUSDOK", idStatusDok, ParameterDirection.Input);
            dbManager.AddParameters(4, "@IDNDERMARRJE", idNdermarrje, ParameterDirection.Input);

            dbManager.AddParameters(5, "@IDMODIFIKUESI", idModifikuesi, ParameterDirection.Input);
            dbManager.AddParameters(6, "@DTFILLIMI", dtFillimi, ParameterDirection.Input);
            dbManager.AddParameters(7, "@DTMBARIMI", dtMbarimi, ParameterDirection.Input);

            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "T_CRM_DETYRE_KLIENT_UPD");

            return new clsMesazh(true, MessagesResource.Messages["msgModifikimiMeSukses"]);
        }

        internal bool ekzistonDetyreKlient(int idKlient, int idDetyra, int idNdermarrje, DateTime dtFillimi, DateTime dtMbarimi)
        {
            clsMesazh mesazh = new clsMesazh();
            dbManager.Open();
            dbManager.CreateParameters(5);
            dbManager.AddParameters(0, "@IDKLIENT", idKlient, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDDETYRE", idDetyra, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDNDERMARRJE", idNdermarrje, ParameterDirection.Input);
            dbManager.AddParameters(3, "@DTFILLIMI", dtFillimi, ParameterDirection.Input);
            dbManager.AddParameters(4, "@DTMBARIMI", dtMbarimi, ParameterDirection.Input);
            int pergjigje = Convert.ToInt32(dbManager.ExecuteScalar(CommandType.StoredProcedure, "T_CRM_DETYRE_KLIENT_ekzistonDetyreKlient"));
            return Convert.ToBoolean(pergjigje);
        }

        internal clsMesazh fshiDetyreKlient(int idDetyreKlient, int idModifikuesi)
        {
            dbManager.Open();
            clsMesazh mesazh = new clsMesazh();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDDETYREKLIENT", idDetyreKlient, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDMODIFIKUESI", idModifikuesi, ParameterDirection.Input);

            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "T_CRM_DETYRE_KLIENT_updDel");

            return new clsMesazh(true, MessagesResource.Messages["msgModifikimiMeSukses"]);
        }

        internal DataTable merrDetyratPerKlientMeDataVlefshmerie(int idNdermarrje, int idPerdoruesi, int idKlienti)
        {
            dbManager.Open();
            clsMesazh mesazh = new clsMesazh();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@IDNDERMARRJE", idNdermarrje, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUESI", idPerdoruesi, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDKLIENTI", idKlienti, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "T_CRM_DETYRE_KLIENT_merrDetyratAutorizimMeDataVlefshmerie");
            return ds.Tables[0];
        }

        /// <summary>
        /// merr detyrat qe i jane lidhur nje klienti specifik
        /// </summary>
        /// <param name="idklienti"></param>
        /// <returns></returns>
        internal DataTable merrDetyreKlientSipasKlientit(int idklienti)
        {
            dbManager.Open();
            clsMesazh mesazh = new clsMesazh();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDKLIENTI", idklienti, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "T_CRM_DETYRE_KLIENT_merrDetyreKlientSipasKlientit");
            return ds.Tables[0];
        }

        #endregion DETYREKLIENT

        internal clsMesazh fshiDetyraTePaNisura(int idKlient, int idModifikuesi)
        {
            dbManager.Open();
            clsMesazh mesazh = new clsMesazh();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDKLIENT", idKlient, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDMODIFIKUESI", idModifikuesi, ParameterDirection.Input);

            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "T_CRM_DETYRE_KLIENT_updDelPerKlient");

            return new clsMesazh(true, MessagesResource.Messages["msgModifikimiMeSukses"]);
        }

        /// <summary>
        /// kthen true nese ekziston nje kombinim detyre-klient e intervali i te ciles pritet me intervalin e ri dtFillimi-dtMbarimi
        /// </summary>
        /// <param name="idNdermarrje"></param>
        /// <param name="idKlienti"></param>
        /// <param name="idDetyre"></param>
        /// <param name="dtFillimi"></param>
        /// <param name="dtMbarimi"></param>
        /// <returns></returns>
        internal bool kaPrerjeVlefshmerieDetyra(int idNdermarrje, int idKlienti, int idDetyre, DateTime dtFillimi, DateTime dtMbarimi)
        {
            dbManager.Open();
            dbManager.CreateParameters(5);
            dbManager.AddParameters(0, "@IDNDERMARRJE", idNdermarrje, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDKLIENT", idKlienti, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDDETYRE", idDetyre, ParameterDirection.Input);
            dbManager.AddParameters(3, "@DTFILLIMI", dtFillimi, ParameterDirection.Input);
            dbManager.AddParameters(4, "@DTMBARIMI", dtMbarimi, ParameterDirection.Input);

            return Convert.ToBoolean(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_CRM_DETYRE_KLIENT_KaPrerjeDetyre"));
        }

        /// <summary>
        /// kthen detyrat dhe anketat qe i jane lidhur nje klienti
        /// </summary>
        /// <param name="klientID"></param>
        /// <param name="idNdermarrje"></param>
        /// <param name="vetemTeVlefshmet"></param>
        /// <returns></returns>
        internal DataTable merrDetyratDheAnketat(int klientID, int idNdermarrje, bool vetemTeVlefshmet)
        {
            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@IDNDERMARRJE", idNdermarrje, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDKLIENT", klientID, ParameterDirection.Input);
            dbManager.AddParameters(2, "@VETEMTEVLEFSHMET", vetemTeVlefshmet, ParameterDirection.Input);

            return dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_CRM_DETYRE_KLIENT_merrDetyraDheAnketa").Tables[0];
        }

        internal DataTable merrDetyratPerTakim(int idTakimi)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDDETYREKLIENT", idTakimi, ParameterDirection.Input);

            return dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_CRM_DETYRE_KLIENT_merrDetyraDheAnketa").Tables[0];
        }

        internal clsMesazh ruajDetyreKlientAgjent(out int p1, int p2, int p3, string p4, DateTime dateTime, int p5)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// kthen detyrat qe ka bere nje agjent tek nje klient ne nje date te caktuar
        /// </summary>
        /// <param name="idNdermarrje"></param>
        /// <param name="idAgjenti"></param>
        /// <param name="idKlienti"></param>
        /// <param name="dtTakimi"></param>
        /// <returns></returns>
        internal DataTable merrDetyraKlientAgjent(int idNdermarrje, int idAgjenti, int idKlienti, string dtTakimi, int idPerdoruesi)
        {
            dbManager.Open();
            dbManager.CreateParameters(5);
            dbManager.AddParameters(0, "@IDNDERMARRJE", idNdermarrje, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDAGJENTI", idAgjenti, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDKLIENTI", idKlienti, ParameterDirection.Input);
            if (!string.IsNullOrWhiteSpace(dtTakimi))
                dbManager.AddParameters(3, "@DTTAKIMI", dtTakimi, ParameterDirection.Input);
            else dbManager.AddParameters(3, "@DTTAKIMI", DBNull.Value, ParameterDirection.Input);
            dbManager.AddParameters(4, "@IDPERDORUESI", idPerdoruesi, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_CRM_DETYRE_KLIENT_AGJENT_sipasKlientAgjentDtVeprimi");
            if (ds == null)
                return null;

            return ds.Tables[0];
        }

        /// <summary>
        /// kthe historikun e takimeve gjate periudhes
        /// </summary>
        /// <param name="idNdermarrje"></param>
        /// <param name="idPerdoruesi"></param>
        /// <param name="dataNga"></param>
        /// <param name="dataDeri"></param>
        /// <returns></returns>
        internal DataTable merrHistorikTakimesh(int idNdermarrje, int idPerdoruesi, string dataNga, string dataDeri)
        {
            dbManager.Open();
            dbManager.CreateParameters(4);
            dbManager.AddParameters(0, "@IDNDERMARJE", idNdermarrje, ParameterDirection.Input);
            dbManager.AddParameters(1, "@DATANGA", dataNga, ParameterDirection.Input);
            dbManager.AddParameters(2, "@DATADERI", dataDeri, ParameterDirection.Input);
            dbManager.AddParameters(3, "@IDPERDORUES", idPerdoruesi, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_CRM_SKEDULER_MerrHistorikuTakimesh");
            if (ds == null)
                return null;

            return ds.Tables[0];
        }

        /// <summary>
        //
        /// </summary>
        /// <param name="idNdermarrje"></param>
        /// <param name="idAgjenti"></param>
        /// <param name="idKlienti"></param>
        /// <param name="dtTakimi"></param>
        /// <param name="idPerdoruesi"></param>
        /// <returns></returns>
        internal DataTable merrTrupKlientAnketaAgjent(int idNdermarrje, int idAgjenti, int idKlienti, string dtTakimi, int idPerdoruesi)
        {
            dbManager.Open();
            dbManager.CreateParameters(5);
            dbManager.AddParameters(0, "@IDNDERMARRJE", idNdermarrje, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDAGJENTI", idAgjenti, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDKLIENTI", idKlienti, ParameterDirection.Input);
            if (!string.IsNullOrWhiteSpace(dtTakimi))
                dbManager.AddParameters(3, "@DTTAKIMI", dtTakimi, ParameterDirection.Input);
            else dbManager.AddParameters(3, "@DTTAKIMI", DBNull.Value, ParameterDirection.Input);
            dbManager.AddParameters(4, "@IDPERDORUESI", idPerdoruesi, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_CRM_TRUPIKLIENT_ANKETA_AGJENT_sipasKlientAgjentDtVeprimi");
            if (ds == null)
                return null;

            return ds.Tables[0];
        }

        internal bool EshtePeriudhaReMeEVogelSeEvjetra(int idNdermarrje, int idKlienti, DateTime dtFillimi, DateTime dtMbarimi, int idDetyre)
        {
            dbManager.Open();
            dbManager.CreateParameters(5);
            dbManager.AddParameters(0, "@IDNDERMARRJE", idNdermarrje, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDKLIENT", idKlienti, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDDETYRE", idDetyre, ParameterDirection.Input);
            dbManager.AddParameters(3, "@DTFILLIMI", dtFillimi, ParameterDirection.Input);
            dbManager.AddParameters(4, "@DTMBARIMI", dtMbarimi, ParameterDirection.Input);

            return Convert.ToBoolean(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_CRM_DETYRE_KLIENT_krahesoPeriudha"));
        }

        internal DataTable merrLevizjetEAgjenteve(int idNdermarrje, string filter)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDNDERMARJE", idNdermarrje, ParameterDirection.Input);
            dbManager.AddParameters(1, "@FILTER", filter, ParameterDirection.Input);

            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "T_CRM_SKEDULER_LevizjeAgjentesh");
            if (ds == null)
                return null;

            return ds.Tables[0];
        }

        public IEnumerable<clsTakimePerKontroll> MerrListenETakimevePerKontroll(DateTime data, string kodNdermarrje)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@KODNDERMARRJE", kodNdermarrje, ParameterDirection.Input);
            dbManager.AddParameters(1, "@DATA", data, ParameterDirection.Input);

            return dbManager.GetIEnumerbale("prc_T_CRM_TAKIMEPERKONTROLL_merrListenSipasNdermarrjesDheDates", clsTakimePerKontroll.Krijo);
        }

        internal bool UpdateStatusLexuarTakimePerKontroll(int IdTakimPerKontroll)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDTAKIPERKONTROLL", IdTakimPerKontroll, ParameterDirection.Input);

            return Convert.ToBoolean(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_CRM_TAKIMEPERKONTROLL_updateStatusLexuar"));
        }

        internal clsMesazh shtoTakimTeKontrolluar(out int idTakimePerKontroll, string Perdorues, string KodNdermarrje, bool Lexuar, string Uuid, string RegID)
        {
            idTakimePerKontroll = -1;
            dbManager.Open();
            clsMesazh mesazh = new clsMesazh();
            dbManager.CreateParameters(6);
            dbManager.AddParameters(0, "@PERDORUES", Perdorues, ParameterDirection.Input);
            dbManager.AddParameters(1, "@KODNDERMARRJE", KodNdermarrje, ParameterDirection.Input);
            dbManager.AddParameters(2, "@LEXUAR", Lexuar, ParameterDirection.Input);
            dbManager.AddParameters(3, "@UUID", Uuid, ParameterDirection.Input);
            dbManager.AddParameters(4, "@REGID", RegID, ParameterDirection.Input);
            dbManager.AddParameters(5, "@IDTAKIMEPERKONTROLL", idTakimePerKontroll, ParameterDirection.Output);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "T_CRM_TAKIMEPERKONTROLL_insert");

            return new clsMesazh(true, MessagesResource.Messages["msgModifikimiMeSukses"]);
        }
    }
}