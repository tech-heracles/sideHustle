using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DbCore.DbAdmin;
using DbCore.DbKontabiliteti;
using DbCore.DbRegjistrim;
using DbCore.IMBUtils.DataBase;
using DbCore.IMBUtils.Messages;
using System.Data.SqlClient;
using DbCore.IMBUtils.Fiskalizimi.Controls;

namespace DbCore.DbArkaBanka
{
    /// <remarks>
    ///  Kjo eshte klasa me e rendesishme e ketij moduli. Eshte nje klase e tille per secilin modul te projektit.
    ///  Ketu ndodhen metodat qe therriten  nga te gjithe objektet brenda projektit DbCore.DbArkaBanka
    ///  secila metode permban thirjet e Store procedures ne DB, duke i kaluar parametrat perkates
    ///  Metodat jane te ndara ne Regions sipas objekteve qe i therrasin keto metoda
    /// </remarks>
    public class clsDatabaseArkaBanka : DbData
    {

        public clsDatabaseArkaBanka() : base() { }
        public clsDatabaseArkaBanka(DbData db) : base(db) { }
        public clsDatabaseArkaBanka(string connectionName) : base(connectionName)
        {

        }

        /// <summary>
        /// Metodat e meposhtme i perkasin veprimeve qe kryhen nga objektet clsVeprimBankaKoka, clsVeprimBankaTrupi dhe colVeprimBankaKoka
        /// </summary>
        #region VEPRIMET ME BANKEN

        /// <summary>
        /// Ekzekuton prc_T_VEPRIMBANKAKOKA_ins per te ruajtur nje objekt clsVeprimBankaKoka ne DB.
        ///<param name="koka">Objekt i tipit clsVeprimBankaKoka qe do te ruhet ne DB</param>
        /// <returns> Kthen statusin e perfundimit te ekzekutimit te SP-se perkatese (nje objekt clsMesazh qe tregon nese veprimi eshte kryer me sukses apo jo.<seealso cref="DbCore.clsMesazh"/>)</returns>
        /// </summary>
        internal clsMesazh ruajVeprimBankeKoka(out int idkoka, int idbanka, double kurs, DateTime datedokumenti, DateTime dateregjistrimi, String nrdokumenti, int nrreference, String nrserial, String pershkrimikoka, int idmenyrepagese, double vlerakoka, double vleramonedhabaze, double komisionibankar, double komisionimonedhabaze, String llojiveprimit, int idperdoruesi, int idllojdokumenti, int idstatusdokumenti, int idnderviti, int idkonfigambjente, int idnivelgjenerues, int idkonfiggjenerues, int idgjenerues, int idnivel, int iddoknga, int iddegeadministrative, int idndermarje, int idgrup1, int idgrup2, int idgrup3, int idllogkrediti, string shoqeria, string customernumber, int idAutomjet, int idrapdesign, string financieri, string dhenesiMarresi, string arketari, bool kase, StatusAprovimi statusapp, string arsyeanullimi, int iddokanullimi, string nrllogari, bool printo, int idkrijuesi, DateTime dtKrijimi)
        {
            idkoka = 0;
            dbManager.Open();
            dbManager.CreateParameters(46);
            dbManager.AddParameters(0, "@IDKOKA", idkoka, ParameterDirection.Output);
            dbManager.AddParameters(1, "@IDBANKA", idbanka, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDKURSI", kurs, ParameterDirection.Input);
            dbManager.AddParameters(3, "@DATEDOKUMENTI", datedokumenti, ParameterDirection.Input);
            dbManager.AddParameters(4, "@DATEREGJISTRIMI", dateregjistrimi, ParameterDirection.Input);
            dbManager.AddParameters(5, "@NRDOKUMENTI", nrdokumenti, ParameterDirection.Input);
            dbManager.AddParameters(6, "@NRREFERENCE", nrreference, ParameterDirection.Input);
            dbManager.AddParameters(7, "@NRSERIAL", nrserial, ParameterDirection.Input);
            dbManager.AddParameters(8, "@PERSHKRIMIKOKA", pershkrimikoka, ParameterDirection.Input);
            dbManager.AddParameters(9, "@IDMENYREPAGESE", idmenyrepagese, ParameterDirection.Input);
            dbManager.AddParameters(10, "@VLERA", vlerakoka, ParameterDirection.Input);
            dbManager.AddParameters(11, "@VLERAMONEDHABAZE", vleramonedhabaze, ParameterDirection.Input);
            dbManager.AddParameters(12, "@KOMISIONI", komisionibankar, ParameterDirection.Input);
            dbManager.AddParameters(13, "@KOMISIONIMONEDHABAZE", komisionimonedhabaze, ParameterDirection.Input);
            dbManager.AddParameters(14, "@LLOJIVEPRIMIT", llojiveprimit, ParameterDirection.Input);
            dbManager.AddParameters(15, "@IDPERDORUESI", idperdoruesi, ParameterDirection.Input);
            dbManager.AddParameters(16, "@IDLLOJDOK", idllojdokumenti, ParameterDirection.Input);
            dbManager.AddParameters(17, "@IDSTATUSDOK", idstatusdokumenti, ParameterDirection.Input);
            dbManager.AddParameters(18, "@IDNDERVITI", idnderviti, ParameterDirection.Input);
            dbManager.AddParameters(19, "@IDKONFIGAMBJENTE", idkonfigambjente, ParameterDirection.Input);
            if (idnivelgjenerues == 0) dbManager.AddParameters(20, "@IDNIVELGJENERUES", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(20, "@IDNIVELGJENERUES", idnivelgjenerues, ParameterDirection.Input);
            if (idkonfiggjenerues == 0) dbManager.AddParameters(21, "@IDKONFIGGJENERUES", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(21, "@IDKONFIGGJENERUES", idkonfiggjenerues, ParameterDirection.Input);
            if (idgjenerues == 0) dbManager.AddParameters(22, "@IDGJENERUES", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(22, "@IDGJENERUES", idgjenerues, ParameterDirection.Input);
            dbManager.AddParameters(23, "@IDNIVEL", idnivel, ParameterDirection.Input);
            if (iddoknga == 0) dbManager.AddParameters(24, "@IDDOKNGA", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(24, "@IDDOKNGA", iddoknga, ParameterDirection.Input);
            if (iddegeadministrative == 0) dbManager.AddParameters(25, "@IDDEGEADMINISTRATIVE", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(25, "@IDDEGEADMINISTRATIVE", iddegeadministrative, ParameterDirection.Input);
            dbManager.AddParameters(26, "@IDNDERMARJE", idndermarje, ParameterDirection.Input);
            if (idgrup1 == 0) dbManager.AddParameters(27, "@IDGRUP1", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(27, "@IDGRUP1", idgrup1, ParameterDirection.Input);
            if (idgrup2 == 0) dbManager.AddParameters(28, "@IDGRUP2", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(28, "@IDGRUP2", idgrup2, ParameterDirection.Input);
            if (idgrup3 == 0 || idgrup3 == -1) dbManager.AddParameters(29, "@IDGRUP3", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(29, "@IDGRUP3", idgrup3, ParameterDirection.Input);
            if (idllogkrediti == 0 || idllogkrediti == -1) dbManager.AddParameters(30, "@IDLLOGKREDITI", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(30, "@IDLLOGKREDITI", idllogkrediti, ParameterDirection.Input);
            dbManager.AddParameters(31, "@SHOQERIA", shoqeria, ParameterDirection.Input);
            dbManager.AddParameters(32, "@CUSTOMERNUMBER", customernumber, ParameterDirection.Input);
            if (idAutomjet == 0 || idAutomjet == -1)
                dbManager.AddParameters(33, "@IDAUTOMJETI", DBNull.Value, ParameterDirection.Input);
            else
                dbManager.AddParameters(33, "@IDAUTOMJETI", idAutomjet, ParameterDirection.Input);
            if (idrapdesign == 0) dbManager.AddParameters(34, "@IDRAPORTDESIGN", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(34, "@IDRAPORTDESIGN", idrapdesign, ParameterDirection.Input);
            dbManager.AddParameters(35, "@FINANCIERI", financieri, ParameterDirection.Input);
            dbManager.AddParameters(36, "@DHENESI_MARRESI", dhenesiMarresi, ParameterDirection.Input);
            dbManager.AddParameters(37, "@ARKETARI", arketari, ParameterDirection.Input);
            dbManager.AddParameters(38, "@Kase", kase, ParameterDirection.Input);
            dbManager.AddParameters(39, "@STATUSAPROVIMI", statusapp, ParameterDirection.Input);
            dbManager.AddParameters(40, "@ARSYEANULLIMI", arsyeanullimi, ParameterDirection.Input);
            if (iddokanullimi == 0 || iddokanullimi == -1) dbManager.AddParameters(41, "@IDDOKANULLIMI", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(41, "@IDDOKANULLIMI", iddokanullimi, ParameterDirection.Input);
            dbManager.AddParameters(42, "@NRLLOGARI", nrllogari, ParameterDirection.Input);
            dbManager.AddParameters(43, "@PRINTO", printo, ParameterDirection.Input);
            dbManager.AddParameters(44, "@IDKRIJUESI", idkrijuesi, ParameterDirection.Input);
            dbManager.AddParameters(45, "@DTKRIJIMI", dtKrijimi, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_VEPRIMBANKAKOKA_ins");

            idkoka = int.Parse(dbManager.Parameters[0].Value.ToString());
            return new clsMesazh(true);

        }
        internal clsMesazh ruajVeprimBankeNeHistorik(int idkoka, int idStatusDokumenti, int idPerdoruesi)
        {
            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters("@IDKOKA", idkoka, ParameterDirection.Input);
            dbManager.AddParameters("@IDSTATUSDOK", idStatusDokumenti, ParameterDirection.Input);
            dbManager.AddParameters("@IDPERDORUESI", idPerdoruesi, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_VEPRIMBANKAKOKA_HISTORIK_ins");
            return new clsMesazh(true);

        }


        /// <summary>
        /// Ekzekuton prc_T_VEPRIMBANKATRUPI_ins per te ruajtur nje objekt clsVeprimBankaTrupi ne DB.
        ///<param name="trupi">Objekt i tipit clsVeprimBankaTrupi qe do te ruhet ne DB</param>
        /// <returns> Kthen statusin e perfundimit te ekzekutimit te SP-se perkatese (nje objekt clsMesazh qe tregon nese veprimi eshte kryer me sukses apo jo.<seealso cref="DbCore.clsMesazh"/>)</returns>
        /// </summary>
        internal clsMesazh ruajVeprimBankeTrupi(int idtrupi, int idkoka, String llojisubjektit, int idsubjekti, String pershkrimitrupi, String debikredi, int idfatura, double zbritjafatures, double kreditetfatures, double vlerapaguar, double vlerapaguarmonedhabaze, double vlerapaark, double kursiFatures, int idnivel, int idopsionpagese, string nrtel, string muaji, string kodi, double vlerafillestare, double vlerambetur, string statusFature, bool meKursFature)
        {
            dbManager.Open();
            dbManager.CreateParameters(22);
            //dbManager.CreateParameters(21);
            dbManager.AddParameters(0, "@IDTRUPI", idtrupi, ParameterDirection.Output);
            dbManager.AddParameters(1, "@IDKOKA", idkoka, ParameterDirection.Input);
            dbManager.AddParameters(2, "@LLOJI", llojisubjektit, ParameterDirection.Input);
            dbManager.AddParameters(3, "@IDSUBJEKTI", idsubjekti, ParameterDirection.Input);
            dbManager.AddParameters(4, "@PERSHKRIMITRUPI", pershkrimitrupi, ParameterDirection.Input);
            dbManager.AddParameters(5, "@DEBIKREDI", debikredi, ParameterDirection.Input);
            if (idfatura == 0)
                dbManager.AddParameters(6, "@IDFATURA", DBNull.Value, ParameterDirection.Input);
            else
                dbManager.AddParameters(6, "@IDFATURA", idfatura, ParameterDirection.Input);
            dbManager.AddParameters(7, "@ZBRITJA", zbritjafatures, ParameterDirection.Input);
            dbManager.AddParameters(8, "@KREDITET", kreditetfatures, ParameterDirection.Input);
            dbManager.AddParameters(9, "@VLERAPAGUAR", vlerapaguar, ParameterDirection.Input);
            dbManager.AddParameters(10, "@VLERAPAGUARMONEDHABAZE", vlerapaguarmonedhabaze, ParameterDirection.Input);
            dbManager.AddParameters(11, "@VLERAPAARKETUESHME", vlerapaark, ParameterDirection.Input);
            dbManager.AddParameters(12, "@KMK", kursiFatures, ParameterDirection.Input);
            if (idnivel == 0)
                dbManager.AddParameters(13, "@IDNIVEL", DBNull.Value, ParameterDirection.Input);
            else
                dbManager.AddParameters(13, "@IDNIVEL", idnivel, ParameterDirection.Input);
            if (idopsionpagese == 0)
                dbManager.AddParameters(14, "@IDOPSIONEPAGESE", DBNull.Value, ParameterDirection.Input);
            else
                dbManager.AddParameters(14, "@IDOPSIONEPAGESE", idopsionpagese, ParameterDirection.Input);
            dbManager.AddParameters(15, "@NRTEL", nrtel, ParameterDirection.Input);
            dbManager.AddParameters(16, "@MUAJI", muaji, ParameterDirection.Input);
            dbManager.AddParameters(17, "@KODI", kodi, ParameterDirection.Input);
            dbManager.AddParameters(18, "@VLERAFILLESTARE", vlerafillestare, ParameterDirection.Input);
            dbManager.AddParameters(19, "@VLERAMBETUR", vlerambetur, ParameterDirection.Input);
            dbManager.AddParameters(20, "@STATUSFATURE", statusFature, ParameterDirection.Input);
            dbManager.AddParameters(21, "@MEKURSFATURE", meKursFature, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_VEPRIMBANKATRUPI_ins");
            return new clsMesazh(true);
        }
        internal DataTable ktheGjitheVeprimetBanka(int idnderviti, int idkatdok)
        {
            

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDNDERVITI", idnderviti, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDKATDOK", idkatdok, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_VEPRIMBANKAKOKA_merrGjitheVeprimetBanka");
            return ds.Tables[0];

        }
       internal DataTable ktheGjitheVeprimetBankaPerTuLidhur(int idkokashitje)
        {
            

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@iddok", idkokashitje, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_VEPRIMBANKAKOKA_merrDokumentaBankePerTuLidhur");
            return ds.Tables[0];

        }
        /// <summary>
        /// Ekzekuton prc_T_VEPRIMBANKAKOKA_merrLlojetVeprimeveBanka per te marre llojet e veprimeve bankare (psh Terheqje, Derdhje).
        /// <returns> Kthen nje dataset me llojet e vprimeve</returns>
        /// </summary>
        public DataSet merrLlojetVeprimeveBanka()
        {
           
            dbManager.Open();
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_VEPRIMBANKAKOKA_merrLlojetVeprimeveBanka");
            return ds;

        }

        /// <summary>
        /// Ekzekuton prc_T_STATUSDOKUMENTI_merrStatusinDokumentave per te marre statusin e veprimeve bankare.
        /// <returns> Kthen nje dataset me statuset e veprimeve bankare</returns>
        /// </summary>
        public DataSet merrStatusinDokumentave() //TODO NESTILA
        {
          

            dbManager.Open();
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_STATUSDOKUMENTI_merrStatusinDokumentave");
            return ds;

        }

       
        /// <summary>
        /// Ekzekuton prc_T_VEPRIMBANKAKOKA_upd per te modifikuar nje objekt clsVeprimBankaKoka ne DB.
        ///<param name="koka">Objekt i tipit clsVeprimBankaKoka qe do te modifikohet</param>
        /// <returns> Kthen true nese veprimi eshte kryer me sukses.</returns>
        /// </summary>
        internal clsMesazh modifikoVeprimBankeKoka(int idkoka, int idbanka, double kurs, DateTime datedokumenti, DateTime dateregjistrimi, String nrdokumenti, int nrreference, String nrserial, String pershkrimikoka, int idmenyrepagese, double vlerakoka, double vleramonedhabaze, double komisionibankar, double komisionimonedhabaze, String llojiveprimit, int idperdoruesi, int idllojdokumenti, int idstatusdokumenti, int idnderviti, int idkonfigambjente, int idnivelgjenerues, int idkonfiggjenerues, int idgjenerues, int idnivel, int iddoknga, int iddegeadministrative, int idndermarje, int idgrup1, int idgrup2, int idgrup3, int idllogkrediti, string shoqeria, string customernumber, int idAutomjet, int idrapdesign, string financieri, string dhenesiMarresi, string arketari, bool kase, StatusAprovimi statusapp, string arsyeanullimi, int iddokanullimi, string nrllogari, bool printo, int idkrijuesi)
        {

            dbManager.Open();
            dbManager.CreateParameters(40);
            dbManager.AddParameters(0, "@IDKOKA", idkoka, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDBANKA", idbanka, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDKURSI", kurs, ParameterDirection.Input);
            dbManager.AddParameters(3, "@DATEDOKUMENTI", datedokumenti, ParameterDirection.Input);
            dbManager.AddParameters(4, "@DATEREGJISTRIMI", dateregjistrimi, ParameterDirection.Input);
            dbManager.AddParameters(5, "@NRDOKUMENTI", nrdokumenti, ParameterDirection.Input);
            dbManager.AddParameters(6, "@NRREFERENCE", nrreference, ParameterDirection.Input);
            dbManager.AddParameters(7, "@NRSERIAL", nrserial, ParameterDirection.Input);
            dbManager.AddParameters(8, "@PERSHKRIMIKOKA", pershkrimikoka, ParameterDirection.Input);
            dbManager.AddParameters(9, "@IDMENYREPAGESE", idmenyrepagese, ParameterDirection.Input);
            dbManager.AddParameters(10, "@VLERA", vlerakoka, ParameterDirection.Input);
            dbManager.AddParameters(11, "@VLERAMONEDHABAZE", vleramonedhabaze, ParameterDirection.Input);
            dbManager.AddParameters(12, "@KOMISIONI", komisionibankar, ParameterDirection.Input);
            dbManager.AddParameters(13, "@KOMISIONIMONEDHABAZE", komisionimonedhabaze, ParameterDirection.Input);
            dbManager.AddParameters(14, "@LLOJIVEPRIMIT", llojiveprimit, ParameterDirection.Input);
            dbManager.AddParameters(15, "@IDPERDORUESI", idperdoruesi, ParameterDirection.Input);
            dbManager.AddParameters(16, "@IDLLOJDOK", idllojdokumenti, ParameterDirection.Input);
            dbManager.AddParameters(17, "@IDSTATUSDOK", idstatusdokumenti, ParameterDirection.Input);
            dbManager.AddParameters(18, "@IDNDERVITI", idnderviti, ParameterDirection.Input);
            dbManager.AddParameters(19, "@IDKONFIGAMBJENTE", idkonfigambjente, ParameterDirection.Input);
            if (idnivelgjenerues == 0) dbManager.AddParameters(20, "@IDNIVELGJENERUES", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(20, "@IDNIVELGJENERUES", idnivelgjenerues, ParameterDirection.Input);
            if (idkonfiggjenerues == 0) dbManager.AddParameters(21, "@IDKONFIGGJENERUES", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(21, "@IDKONFIGGJENERUES", idkonfiggjenerues, ParameterDirection.Input);
            if (idgjenerues == 0) dbManager.AddParameters(22, "@IDGJENERUES", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(22, "@IDGJENERUES", idgjenerues, ParameterDirection.Input);
            dbManager.AddParameters(23, "@IDNIVEL", idnivel, ParameterDirection.Input);
            if (iddoknga == 0) dbManager.AddParameters(24, "@IDDOKNGA", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(24, "@IDDOKNGA", iddoknga, ParameterDirection.Input);
            if (iddegeadministrative == 0) dbManager.AddParameters(25, "@IDDEGEADMINISTRATIVE", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(25, "@IDDEGEADMINISTRATIVE", iddegeadministrative, ParameterDirection.Input);
            dbManager.AddParameters(26, "@IDNDERMARJE", idndermarje, ParameterDirection.Input);
            if (idgrup1 == 0) dbManager.AddParameters(27, "@IDGRUP1", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(27, "@IDGRUP1", idgrup1, ParameterDirection.Input);
            if (idgrup2 == 0) dbManager.AddParameters(28, "@IDGRUP2", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(28, "@IDGRUP2", idgrup2, ParameterDirection.Input);
            if (idgrup3 == 0) dbManager.AddParameters(29, "@IDGRUP3", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(29, "@IDGRUP3", idgrup3, ParameterDirection.Input);
            if (idllogkrediti == 0 || idllogkrediti == -1)
                dbManager.AddParameters(30, "@IDLLOGKREDITI", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(30, "@IDLLOGKREDITI", idllogkrediti, ParameterDirection.Input);
            dbManager.AddParameters(31, "@SHOQERIA", shoqeria, ParameterDirection.Input);
            dbManager.AddParameters(32, "@CUSTOMERNUMBER", customernumber, ParameterDirection.Input);
            if (idAutomjet == 0 || idAutomjet == -1)
                dbManager.AddParameters(33, "@IDAUTOMJETI", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(33, "@IDAUTOMJETI", idAutomjet, ParameterDirection.Input);
            if (idrapdesign == 0) dbManager.AddParameters(34, "@IDRAPORTDESIGN", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(34, "@IDRAPORTDESIGN", idrapdesign, ParameterDirection.Input);
            dbManager.AddParameters(35, "@FINANCIERI", financieri, ParameterDirection.Input);
            dbManager.AddParameters(36, "@DHENESI_MARRESI", dhenesiMarresi, ParameterDirection.Input);
            dbManager.AddParameters(37, "@ARKETARI", arketari, ParameterDirection.Input);
            dbManager.AddParameters(38, "@PRINTO", printo, ParameterDirection.Input);
            dbManager.AddParameters(39, "@IDKRIJUESI", idkrijuesi, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_VEPRIMBANKAKOKA_upd");
            idkoka = int.Parse(dbManager.Parameters[0].Value.ToString());
            return new clsMesazh(true, "Ruajtja perfundoi me sukses");

        }

        /// <summary>
        /// Ekzekuton prc_T_VEPRIMBANKAKOKA_upd per te modifikuar nje objekt clsVeprimBankaKoka ne DB.
        ///<param name="koka">Objekt i tipit clsVeprimBankaKoka qe do te modifikohet</param>
        /// <returns> Kthen true nese veprimi eshte kryer me sukses.</returns>
        /// </summary>
        internal clsMesazh modifikoVeprimBankeKokaLidh(int idkoka, int idbanka, double kurs, DateTime datedokumenti, DateTime dateregjistrimi, String nrdokumenti, int nrreference, String nrserial, String pershkrimikoka, int idmenyrepagese, double vlerakoka, double vleramonedhabaze, double komisionibankar, double komisionimonedhabaze, String llojiveprimit, int idperdoruesi, int idllojdokumenti, int idstatusdokumenti, int iddegeadministrative, int idgrup1, int idgrup2, int idgrup3, int idllogkrediti, string shoqeria, string customernumber, int idAutomjeti, int idrapdesign, string financieri, string dhenesiMarresi, string arketari, bool kase, StatusAprovimi statusapp, string arsyeanullimi, int iddokanullimi, string nrllogari, bool printo, int idkrijuesi)
        {

            dbManager.Open();
            dbManager.CreateParameters(37);
            dbManager.AddParameters(0, "@IDKOKA", idkoka, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDBANKA", idbanka, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDKURSI", kurs, ParameterDirection.Input);
            dbManager.AddParameters(3, "@DATEDOKUMENTI", datedokumenti, ParameterDirection.Input);
            dbManager.AddParameters(4, "@DATEREGJISTRIMI", dateregjistrimi, ParameterDirection.Input);
            dbManager.AddParameters(5, "@NRDOKUMENTI", nrdokumenti, ParameterDirection.Input);
            dbManager.AddParameters(6, "@NRREFERENCE", nrreference, ParameterDirection.Input);
            dbManager.AddParameters(7, "@NRSERIAL", nrserial, ParameterDirection.Input);
            dbManager.AddParameters(8, "@PERSHKRIMIKOKA", pershkrimikoka, ParameterDirection.Input);
            dbManager.AddParameters(9, "@IDMENYREPAGESE", idmenyrepagese, ParameterDirection.Input);
            dbManager.AddParameters(10, "@VLERA", vlerakoka, ParameterDirection.Input);
            dbManager.AddParameters(11, "@VLERAMONEDHABAZE", vleramonedhabaze, ParameterDirection.Input);
            dbManager.AddParameters(12, "@KOMISIONI", komisionibankar, ParameterDirection.Input);
            dbManager.AddParameters(13, "@KOMISIONIMONEDHABAZE", komisionimonedhabaze, ParameterDirection.Input);
            dbManager.AddParameters(14, "@LLOJIVEPRIMIT", llojiveprimit, ParameterDirection.Input);
            dbManager.AddParameters(15, "@IDPERDORUESI", idperdoruesi, ParameterDirection.Input);
            dbManager.AddParameters(16, "@IDLLOJDOK", idllojdokumenti, ParameterDirection.Input);
            dbManager.AddParameters(17, "@IDSTATUSDOK", idstatusdokumenti, ParameterDirection.Input);
            if (iddegeadministrative == 0) dbManager.AddParameters(18, "@IDDEGEADMINISTRATIVE", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(18, "@IDDEGEADMINISTRATIVE", iddegeadministrative, ParameterDirection.Input);
            if (idgrup1 == 0) dbManager.AddParameters(19, "@IDGRUP1", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(19, "@IDGRUP1", idgrup1, ParameterDirection.Input);
            if (idgrup2 == 0) dbManager.AddParameters(20, "@IDGRUP2", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(20, "@IDGRUP2", idgrup2, ParameterDirection.Input);
            if (idgrup3 == 0) dbManager.AddParameters(21, "@IDGRUP3", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(21, "@IDGRUP3", idgrup3, ParameterDirection.Input);
            if (idllogkrediti == 0 || idllogkrediti == -1)  dbManager.AddParameters(22, "@IDLLOGKREDITI", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(22, "@IDLLOGKREDITI", idllogkrediti, ParameterDirection.Input);
            dbManager.AddParameters(23, "@SHOQERIA", shoqeria, ParameterDirection.Input);
            dbManager.AddParameters(24, "@CUSTOMERNUMBER", customernumber, ParameterDirection.Input);
            if (idAutomjeti == 0 || idAutomjeti == -1)
                dbManager.AddParameters(25, "@IDAUTOMJETI", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(25, "@IDAUTOMJETI", idAutomjeti, ParameterDirection.Input);
            if (idrapdesign == 0) dbManager.AddParameters(26, "@IDRAPORTDESIGN", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(26, "@IDRAPORTDESIGN", idrapdesign, ParameterDirection.Input);
            dbManager.AddParameters(27, "@FINANCIERI", financieri, ParameterDirection.Input);
            dbManager.AddParameters(28, "@DHENESI_MARRESI", dhenesiMarresi, ParameterDirection.Input);
            dbManager.AddParameters(29, "@ARKETARI", arketari, ParameterDirection.Input);
            dbManager.AddParameters(30, "@Kase", kase, ParameterDirection.Input);
            dbManager.AddParameters(31, "@STATUSAPROVIMI", statusapp, ParameterDirection.Input);
            dbManager.AddParameters(32, "@ARSYEANULLIMI", arsyeanullimi, ParameterDirection.Input);
            if (iddokanullimi == 0 || iddokanullimi == -1) dbManager.AddParameters(33, "@IDDOKANULLIMI", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(33, "@IDDOKANULLIMI", iddokanullimi, ParameterDirection.Input);
            dbManager.AddParameters(34, "@NRLLOGARI", nrllogari, ParameterDirection.Input);
            dbManager.AddParameters(35, "@PRINTO", printo, ParameterDirection.Input);
            dbManager.AddParameters(36, "@IDKRIJUESI", idkrijuesi, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_VEPRIMBANKAKOKA_updLidh");
            idkoka = int.Parse(dbManager.Parameters[0].Value.ToString());
            return new clsMesazh(true, "Ruajtja perfundoi me sukses");

        }

        /// <summary>
        /// Ekzekuton prc_T_VEPRIMBANKATRUPI_del per te fshire trupin e nje objekti clsVeprimBankaKoka ne DB.
        ///<param name="koka">Objekt i tipit clsVeprimBankaKoka trupi i te cilit do te fshihet</param>
        /// <returns> Kthen true nese veprimi eshte kryer me sukses.</returns>
        /// </summary>
        internal clsMesazh fshiVeprimBankeTrupi(int idkoka)
        {
           

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDKOKA", idkoka, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_VEPRIMBANKATRUPI_del");
            return new clsMesazh(true, "Fshirja perfundoi me sukses");

        }

        /// <summary>
        /// merr IDRAPORTDESING sipas IDKOKA.  Therret SP-ne: 
        /// <see cref="prc_T_VEPRIMBANKAKOKA_merrIdRaportDesign"/>
        /// </summary>
        /// <param name="IDKOKA"></param>
        /// <returns></returns>
        internal int ktheIdRaportDesignSipasIdkokes(int idKoka)
        {

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDKOKA", idKoka, ParameterDirection.Input);
            return Convert.ToInt32(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_VEPRIMBANKAKOKA_merrIdRaportDesign"));
        }
        /// <summary>
        /// merr IDSTATUSDOK sipas IDKOKA.  Therret SP-ne: 
        /// <see cref="prc_T_VEPRIMBANKAKOKA_merrIdStatusDok"/>
        /// </summary>
        /// <param name="IDKOKA"></param>
        /// <returns></returns>
        internal int ktheIdStatusDokumentiSipasIdkokes(int idKoka)
        {

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDKOKA", idKoka, ParameterDirection.Input);
            return Convert.ToInt32(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_VEPRIMBANKAKOKA_merrIdStatusDok"));
        }

        /// <summary>
        /// Ekzekuton prc_T_VEPRIMBANKAKOKA_del per te fshire nje objekt clsVeprimBankaKoka ne DB.
        ///<param name="koka">Objekt i tipit clsVeprimBankaKoka qe do te fshihet</param>
        /// <returns> Kthen true nese veprimi eshte kryer me sukses.</returns>
        /// </summary>

        internal clsMesazh fshiVeprimBankeKoka(int idkoka)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDKOKA", idkoka, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_VEPRIMBANKAKOKA_del");
            return new clsMesazh(true, "Fshirja perfundoi me sukses");

        }
        /// <summary>
        /// Ekzekuton prc_T_VEPRIMBANKAKOKA_ktheVeprimBanke per te marre nje collection me objekte clsVeprimBankaKoka ne DB duke filtruar sipas id-se se kokes se veprimit bankar.
        ///<param name="idKoka">Id e kokes se veprimit bankar</param>
        /// <returns> Kthen nje collection me objekte clsVeprimBankaKoka qe plotesojne kushtin</returns>
        /// </summary>
        internal DataRow merrVeprimBanke(int idKoka)
        {

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDKOKA", idKoka, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_VEPRIMBANKAKOKA_ktheVeprimBanke");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];

        }

        internal bool KontrolloAnulluar(int idKoka)
        {

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDKOKA", idKoka, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_VEPRIMBANKAKOKA_kontrolloAnulluar");
            if (ds == null)
                return true;
            if (ds.Tables[0].Rows.Count == 0)
                return false;
            return true;
        }

        /// <summary>
        /// Ekzekuton prc_T_VEPRIMBANKAKOKA_ekzistonVeprimBanke per te kontrolluar nese ekziston nje objekt clsVeprimBankaKoka duke filtruar sipas llojit te veprimit, id-se se bankes, numrit dhe dates se dokumentit.
        ///<param name="oKoka">Objekt i tipit clsVeprimBankaKoka nga ku merren vlerat per filtrim ne DB</param>
        /// <returns> Kthen true nese ekziston nje veprim bankar qe ploteson kushtet.</returns>
        /// </summary>
        internal bool ekzistonVeprimBanke(int idbanka, DateTime datedokumenti, String nrdokumenti, int idKonfigAbmbjente)
        {
            dbManager.Open();
            dbManager.CreateParameters(4);
            dbManager.AddParameters(0, "@IDKONFIGAMBJENTE", idKonfigAbmbjente, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDBANKA", idbanka, ParameterDirection.Input);
            dbManager.AddParameters(2, "@NRDOKUMENTI", nrdokumenti, ParameterDirection.Input);
            dbManager.AddParameters(3, "@DATEDOKUMENTI", datedokumenti, ParameterDirection.Input);
            return Convert.ToInt32(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_VEPRIMBANKAKOKA_ekzistonVeprimBanke")) > 0;
        }

        internal bool ekzistonNumerSerialUnikPerKeteNivelDheNdermarrjeKokaArkaBanka(int idKoka,int idNiveli, string nrSerial, int idNdermarrje)
        {
            dbManager.Open();
            dbManager.CreateParameters(4);
            dbManager.AddParameters("@IDKOKA", idKoka, ParameterDirection.Input);
            dbManager.AddParameters("@IDNIVELI", idNiveli, ParameterDirection.Input);
            dbManager.AddParameters("@IDNDERMARRJE", idNdermarrje, ParameterDirection.Input);
            dbManager.AddParameters("@NRSERIAL", nrSerial, ParameterDirection.Input);
            return Convert.ToInt32(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_VEPRIMBANKAKOKA_ekzistonNumerSerialPerKeteNivel")) == 1;
        }
        /// <summary>
        /// Ekzekuton prc_T_VEPRIMBANKATRUPI_merrTrupinSipasVeprimBankeKoka per te marre trupin e nje veprimi banke duke filtruar sipas id-se se kokes se veprimit.
        ///<param name="idkoka">Id e kokes se veprimit bankar</param>
        /// <returns> Kthen nje collection me objekte colVeprimBankaTrupi</returns>
        /// </summary>
        internal DataTable ktheTrupinSipasVeprimBankeKoka(int idkoka)
        {
           

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDKOKA", idkoka, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_VEPRIMBANKATRUPI_merrTrupinSipasVeprimBankeKoka");
            return ds.Tables[0];

        }
    
        /// <summary>
        /// Ekzekuton prc_T_VEPRIMBANKAKOKA_merrVeprimBankeKokaSipasKodit per te marre nje collection me objekte clsVeprimBankaKoka ne DB duke filtruar sipas numrit te dokumentit te kokes se veprimit bankar.
        ///<param name="kodi">Numri i dokumentit te kokes se veprimit bankar</param>
        /// <returns> Kthen nje collection me objekte clsVeprimBankaKoka qe plotesojne kushtin</returns>
        /// </summary>
        internal DataTable ktheVeprimBankeKokaSipasKodit(String kodi)
        {
            

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@NRDOK", kodi, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_VEPRIMBANKAKOKA_merrVeprimBankeKokaSipasKodit");
            return ds.Tables[0];


        }

        /// <summary>
        /// Ekzekuton prc_T_VEPRIMBANKAKOKA_merrVeprimArkaBankaPerEksport per te marre dokumentat e veprime arka/banka per eksport        
        /// <returns> Kthen nje DataTable me dokumente arka/banka qe plotesojne kushtet e where te sp.</returns>
        /// </summary>
        internal DataTable kthedokVeprimeArkaBankaPerEksport(int idNdermarrje, int idPerdorues, int idNderViti, int idKatDok, int lloji, string emerTabKoka, string emerFusheId, string idPerEksport)
        {
            dbManager.Open();
            dbManager.CreateParameters(8);
            dbManager.AddParameters(0, "@IDNDERMARJE", idNdermarrje, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUES", idPerdorues, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDNDERVITI", idNderViti, ParameterDirection.Input);
            dbManager.AddParameters(3, "@IDKATDOK", idKatDok, ParameterDirection.Input);
            dbManager.AddParameters(4, "@LLOJI", lloji, ParameterDirection.Input);
            dbManager.AddParameters(5, "@EMERTABKOKA", emerTabKoka, ParameterDirection.Input);
            dbManager.AddParameters(6, "@EMERFUSHEID", emerFusheId, ParameterDirection.Input);
            dbManager.AddParameters(7, "@IDPEREKSPORT", idPerEksport, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_VEPRIMBANKAKOKA_merrVeprimArkaBankaPerEksport");
            return ds.Tables[0];
        }

        internal DataTable merrVeprimBankaDT(int idnderviti, int idkatdok, int idperdoruesi, string datanga, string dataderi, string lloji)
        {

            dbManager.Open();
            dbManager.CreateParameters(6);
            dbManager.AddParameters(0, "@IDNDERVITI", idnderviti, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDKATDOK", idkatdok, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDPERDORUES", idperdoruesi, ParameterDirection.Input);
            dbManager.AddParameters(3, "@DATANGA", datanga, ParameterDirection.Input);
            dbManager.AddParameters(4, "@DATADERI", dataderi, ParameterDirection.Input);
            dbManager.AddParameters(5, "@LLOJI", lloji, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_VEPRIMBANKAKOKA_merrVeprimBankaDT");
            return ds.Tables[0];

        }

        public DataTable merrKlientFurnitorEmalPerDok(string idsKokaDok)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters("@IDDOKKOKA", idsKokaDok, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_VEPRIMBANKAKOKA_merrKlientFurnitorEmailPerDok");
            return ds.Tables[0];
        }

        internal DataTable merrGjitheOpsionePagese(int lloji)
        {

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@lloji", lloji, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_OPSIONEPAGESE_merrGjithe");
            return ds.Tables[0];

        }

        internal void merrKlienteTerminated(colKlienteTerminated klienteListPerTuMbushur)
        {

            dbManager.Open();
            dbManager.CreateParameters(0);
            dbManager.FillObject("prc_T_KLIENTE_TERMINATED_merrKlientet", klienteListPerTuMbushur);


        }
        internal DataRow ktheGjendjeDitoreSipasIdArke(int idArka, DateTime dtKrijimi)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDARKA", idArka, ParameterDirection.Input);
            dbManager.AddParameters(1, "@DTKRIJIMI", dtKrijimi, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_GJENDJEARKEDITORE_selSipasIdArke");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];
        }
        internal DataRow merrOpsionePageseSipasPershkrimit(string pershkrimi)
        {


            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@Pershkrimi", pershkrimi, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_OPSIONEPAGESE_merrSipasPershkrimit");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];


        }
        internal bool kaAutorizimKokaBanka(int idkoka, int idperdoruesi)
        {

         

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDKOKA", idkoka, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUES", idperdoruesi, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_VEPRIMBANKAKOKA_kaAutorizim");
            if (ds.Tables[0].Rows.Count == 1)
                return true;
            else if (ds.Tables[0].Rows.Count == 0)
                return false;
            else return true;
        }

        internal DataTable merrDokArkaBankaPerImport(string emerTabKoka, string emerTabTrupi, string ndermarrjeKey, string ndermarjeKodi, string nenkategoria, int lloji, string primaryKey, bool merrTePaImportuara, bool rimerrTeImportuara, int? nrDokumentash)
        {
            dbManager.Open();
            dbManager.CreateParameters(10);
            dbManager.AddParameters(0, "@T_TEMP_KOKAARKA", emerTabKoka, ParameterDirection.Input);
            dbManager.AddParameters(1, "@T_TEMP_TRUPIARKA", emerTabTrupi, ParameterDirection.Input);
            dbManager.AddParameters(2, "@NDERMARRJEKEY", ndermarrjeKey, ParameterDirection.Input);
            dbManager.AddParameters(3, "@NDERMARJEKODI", ndermarjeKodi, ParameterDirection.Input);
            dbManager.AddParameters(4, "@NENKATEGORIA", nenkategoria, ParameterDirection.Input);
            dbManager.AddParameters(5, "@LLOJI", lloji, ParameterDirection.Input);
            dbManager.AddParameters(6, "@PRIMARYKEY", primaryKey, ParameterDirection.Input);
            dbManager.AddParameters(7, "@MERRTEPAIMPORTUARA", merrTePaImportuara, ParameterDirection.Input);
            dbManager.AddParameters(8, "@RIMERRTEIMPORTUARA", rimerrTeImportuara, ParameterDirection.Input);
            dbManager.AddParameters(9, "@NRDOKUMENTASH", nrDokumentash, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_TEMP_IMPORT_ARKAmerrTeDhenaPerNdermarrjeSipasLlojitDT");
            return ds.Tables[0];
        }

        internal clsMesazh ruajGjendjeArkeDitore(out int id, int arka, DateTime data, double vlera, int idNdermarrje, int idStatusDok, int idPerdoruesi)
        {
            id = -1;
            try
            {
                dbManager.Open();
                dbManager.CreateParameters(7);
                dbManager.AddParameters(0, "@IDGJENDJEDITORE", id, ParameterDirection.Output);
                dbManager.AddParameters(1, "@ARKA", arka, ParameterDirection.Input);
                dbManager.AddParameters(2, "@DATA", data, ParameterDirection.Input);
                dbManager.AddParameters(3, "@VLERA", vlera, ParameterDirection.Input);
                dbManager.AddParameters(4, "@IDNDERMARRJE", idNdermarrje, ParameterDirection.Input);
                dbManager.AddParameters(5, "@IDSTATUSDOK", idStatusDok, ParameterDirection.Input);
                dbManager.AddParameters(6, "@IDPERDORUESI", idPerdoruesi, ParameterDirection.Input);

                dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_GJENDJEARKEDITORE_ins");
                id = int.Parse(dbManager.Parameters[0].Value.ToString());
                return new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
            }
            catch (SqlException ex)
            {
                if (ex.Number == 2627) 
                    return new clsMesazh(false, "Ekziston nje regjistrim per kete arke dhe kete date!");
                
                else
                    return new clsMesazh(false, MessagesResource.Messages["msgGabimRuajtje"]); 
            }
        }

        internal clsMesazh modifikoGjendjeArkeDitore(int idGjendje, double vlera, int idNdermarrje, int idStatusDok, int idPerdoruesi)
        {
            dbManager.Open();
            dbManager.CreateParameters(5);
            dbManager.AddParameters(0, "@IDGJENDJEDITORE", idGjendje, ParameterDirection.Input);
            dbManager.AddParameters(1, "@VLERA", vlera, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDNDERMARRJE", idNdermarrje, ParameterDirection.Input);
            dbManager.AddParameters(3, "@IDSTATUSDOK", idStatusDok, ParameterDirection.Input);
            dbManager.AddParameters(4, "@IDPERDORUESI", idPerdoruesi, ParameterDirection.Input);

            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_GJENDJEARKEDITORE_upd");
            return new clsMesazh(true, MessagesResource.Messages["msgModifikimiMeSukses"]);
        }

        internal clsMesazh fshiGjendjeArkeDitore(int idGjendje)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDGJENDJEDITORE", idGjendje, ParameterDirection.Input);
            
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_GJENDJEARKEDITORE_del");
            return new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
        }

        internal DataTable ktheGjendjeArkeDitoreSipasNdermarrje(int idndermarje)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDNDERMARRJE", idndermarje, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_GJENDJEARKEDITORE_selSipasNdermarrjes");
            return ds.Tables[0];
        }

        internal DataRow ktheGjendjeDitoreSipasIdDR(int idGjendje)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDGJENDJEDITORE", idGjendje, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_GJENDJEARKEDITORE_selSipasIdDR");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];
        }
        #endregion

        /// <summary>
        /// Metodat e meposhtme i perkasin veprimeve qe kryhen nga objektet clsBanka, colBankat
        /// </summary>
        #region BANKAT

        /// <summary>
        /// Ekzekuton prc_T_BANKA_ins per te ruajtur nje objekt clsBanka ne DB.
        ///<param name="banka">Objekt i tipit clsBanka qe do te ruhet ne DB</param>
        /// <returns> Kthen statusin e perfundimit te ekzekutimit te SP-se perkatese (nje objekt clsMesazh qe tregon nese veprimi eshte kryer me sukses apo jo.<seealso cref="DbCore.clsMesazh"/>)</returns>
        /// </summary>
        internal clsMesazh ruajBanke(out int idBanka, string kodiBanka, string emerBanka, int idTipiBanka, string nrLlogariBanka, string iban, int idGrupBanke, string shenimeBanka, bool aktivBanka,
            int idLlogariKontabilizimi, int idMonedhaBanka, string rrugaBanka, string qytetiBanka, string shtetiBanka, string zipKodBanka, string telBanka, string emerKontaktiBanka, string mbiemerKontaktiBanka, 
            string telKontaktiBanka, string faxKontaktiBanka, string celKontaktiBanka, string emailKontaktiBanka, int idPerdoruesi, int komis, int idnderm, bool llojarkabanka, string adresakontaktibanka, 
            int idkonfig, int iddegeadministrative, int idstatusdok, string dega, string nrKlienti, string valuta, string kodiLlogarise, string tipi, string kodiTCR, string nrRendor, bool shfaqNeEinvoice)
        {
            idBanka = -1;
            bool klientIFiskalizuar = clsKontrollePerFiskalizimin.ktheNeseKlientiEshteAzhornuarPerFiskalizimV5();
            /*banka.dega,banka.nrKlienti,banka.valuta,banka.kodiLlogarise,banka.tipi*/
            dbManager.Open();
            if(klientIFiskalizuar)
                dbManager.CreateParameters(38);
            else
                dbManager.CreateParameters(37);
            dbManager.AddParameters(0, "@IDBANKA", idBanka, ParameterDirection.Output);
            dbManager.AddParameters(1, "@KODIBANKA", kodiBanka, ParameterDirection.Input);
            dbManager.AddParameters(2, "@EMERBANKA", emerBanka, ParameterDirection.Input);
            dbManager.AddParameters(3, "@IDTIPIBANKA", idTipiBanka, ParameterDirection.Input);
            dbManager.AddParameters(4, "@NRLLOGARIBANKA", nrLlogariBanka, ParameterDirection.Input);
            dbManager.AddParameters(5, "@IBAN", iban, ParameterDirection.Input);
            if (idGrupBanke == 0) dbManager.AddParameters(6, "@IDGRUPBANKE", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(6, "@IDGRUPBANKE", idGrupBanke, ParameterDirection.Input);
            dbManager.AddParameters(7, "@SHENIMEBANKA", shenimeBanka, ParameterDirection.Input);
            dbManager.AddParameters(8, "@AKTIVBANKA", aktivBanka, ParameterDirection.Input);
            if (idLlogariKontabilizimi == 0 || idLlogariKontabilizimi == -1) dbManager.AddParameters(9, "@IDLLOGARIKONTABILIZIMI", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(9, "@IDLLOGARIKONTABILIZIMI", idLlogariKontabilizimi, ParameterDirection.Input);
            if (idMonedhaBanka == 0) dbManager.AddParameters(10, "@IDMONEDHABANKA", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(10, "@IDMONEDHABANKA", idMonedhaBanka, ParameterDirection.Input);
            dbManager.AddParameters(11, "@RRUGABANKA", rrugaBanka, ParameterDirection.Input);
            dbManager.AddParameters(12, "@QYTETIBANKA", qytetiBanka, ParameterDirection.Input);
            dbManager.AddParameters(13, "@SHTETIBANKA", shtetiBanka, ParameterDirection.Input);
            dbManager.AddParameters(14, "@ZIPKODBANKA", zipKodBanka, ParameterDirection.Input);
            dbManager.AddParameters(15, "@TELBANKA ", telBanka, ParameterDirection.Input);
            dbManager.AddParameters(16, "@EMERKONTAKTIBANKA", emerKontaktiBanka, ParameterDirection.Input);
            dbManager.AddParameters(17, "@MBIEMERKONTAKTIBANKA", mbiemerKontaktiBanka, ParameterDirection.Input);
            dbManager.AddParameters(18, "@TELKONTAKTIBANKA", telKontaktiBanka, ParameterDirection.Input);
            dbManager.AddParameters(19, "@FAXKONTAKTIBANKA", faxKontaktiBanka, ParameterDirection.Input);
            dbManager.AddParameters(20, "@CELKONTAKTIBANKA", celKontaktiBanka, ParameterDirection.Input);
            dbManager.AddParameters(21, "@EMAILKONTAKTIBANKA", emailKontaktiBanka, ParameterDirection.Input);
            dbManager.AddParameters(22, "@IDPERDORUESI", idPerdoruesi, ParameterDirection.Input);
            if (komis == 0 || komis == -1) dbManager.AddParameters(23, "@KOMISIONI", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(23, "@KOMISIONI", komis, ParameterDirection.Input);
            dbManager.AddParameters(24, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            dbManager.AddParameters(25, "@LLOJARKABANKA", llojarkabanka, ParameterDirection.Input);
            dbManager.AddParameters(26, "@ADRESAKONTAKTIBANKA", adresakontaktibanka, ParameterDirection.Input);
            dbManager.AddParameters(27, "@IDKONFIG", idkonfig, ParameterDirection.Input);
            if (iddegeadministrative == 0) dbManager.AddParameters(28, "@IDDEGEADMINISTRATIVE", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(28, "@IDDEGEADMINISTRATIVE", iddegeadministrative, ParameterDirection.Input);
            dbManager.AddParameters(29, "@IDSTATUSDOK", idstatusdok, ParameterDirection.Input);
            dbManager.AddParameters(30, "@DEGA", dega, ParameterDirection.Input);
            dbManager.AddParameters(31, "@VALUTA", valuta, ParameterDirection.Input);
            dbManager.AddParameters(32, "@NRKLIENTI", nrKlienti, ParameterDirection.Input);
            dbManager.AddParameters(33, "@KODILLOGARISE", kodiLlogarise, ParameterDirection.Input);
            dbManager.AddParameters(34, "@TIPI", tipi, ParameterDirection.Input);
            dbManager.AddParameters(35, "@KODITCR", kodiTCR, ParameterDirection.Input);
            dbManager.AddParameters(36, "@NUMRIRENDOR", nrRendor, ParameterDirection.Input);
            if (klientIFiskalizuar)
                dbManager.AddParameters(37, "@SHFAQNEEINVOICE", shfaqNeEinvoice, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_BANKA_ins");
            idBanka = int.Parse(dbManager.Parameters[0].Value.ToString());
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
            return mesazh;
        }
        
        /// <summary>
        /// Ekzekuton prc_T_BANKA_upd per te modifikuar nje objekt clsBanka ne DB.
        ///<param name="banka">Objekt i tipit clsBanka qe do te modifikohet</param>
        /// <returns> Kthen statusin e perfundimit te ekzekutimit te SP-se perkatese (nje objekt clsMesazh qe tregon nese veprimi eshte kryer me sukses apo jo.<seealso cref="DbCore.clsMesazh"/>)</returns>
        /// </summary>
        internal clsMesazh modifikoBanke(int idBanka, string kodiBanka, string emerBanka, int idTipiBanka, string nrLlogariBanka, string iban, int idGrupBanke, string shenimeBanka, bool aktivBanka,
            int idLlogariKontabilizimi, int idMonedhaBanka, string rrugaBanka, string qytetiBanka, string shtetiBanka, string zipKodBanka, string telBanka, string emerKontaktiBanka, string mbiemerKontaktiBanka,
            string telKontaktiBanka, string faxKontaktiBanka, string celKontaktiBanka, string emailKontaktiBanka, int idPerdoruesi, int komis, int idnderm, bool llojarkabanka, string adresakontaktibanka, int idkonfig,
            int iddegeadministrative, int idstatusdok, string dega, string nrKlienti, string valuta, string kodiLlogarise, string tipi, string kodiTCR, string nrRendor, bool shfaqNeEinvoice)
        {
            bool klientIFiskalizuar = clsKontrollePerFiskalizimin.ktheNeseKlientiEshteAzhornuarPerFiskalizimV5();
            dbManager.Open();
            if(klientIFiskalizuar)
                dbManager.CreateParameters(38);
            else
                dbManager.CreateParameters(37);
            dbManager.AddParameters(0, "@IDBANKA", idBanka, ParameterDirection.Input);
            dbManager.AddParameters(1, "@KODIBANKA", kodiBanka, ParameterDirection.Input);
            dbManager.AddParameters(2, "@EMERBANKA", emerBanka, ParameterDirection.Input);
            dbManager.AddParameters(3, "@IDTIPIBANKA", idTipiBanka, ParameterDirection.Input);
            dbManager.AddParameters(4, "@NRLLOGARIBANKA", nrLlogariBanka, ParameterDirection.Input);
            dbManager.AddParameters(5, "@IBAN", iban, ParameterDirection.Input);
            if (idGrupBanke == 0) dbManager.AddParameters(6, "@IDGRUPBANKE", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(6, "@IDGRUPBANKE", idGrupBanke, ParameterDirection.Input);
            dbManager.AddParameters(7, "@SHENIMEBANKA", shenimeBanka, ParameterDirection.Input);
            dbManager.AddParameters(8, "@AKTIVBANKA", aktivBanka, ParameterDirection.Input);
            if (idLlogariKontabilizimi == 0 || idLlogariKontabilizimi == -1) dbManager.AddParameters(9, "@IDLLOGARIKONTABILIZIMI", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(9, "@IDLLOGARIKONTABILIZIMI", idLlogariKontabilizimi, ParameterDirection.Input);
            if (idMonedhaBanka == 0) dbManager.AddParameters(10, "@IDMONEDHABANKA", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(10, "@IDMONEDHABANKA", idMonedhaBanka, ParameterDirection.Input);
            dbManager.AddParameters(11, "@RRUGABANKA", rrugaBanka, ParameterDirection.Input);
            dbManager.AddParameters(12, "@QYTETIBANKA", qytetiBanka, ParameterDirection.Input);
            dbManager.AddParameters(13, "@SHTETIBANKA", shtetiBanka, ParameterDirection.Input);
            dbManager.AddParameters(14, "@ZIPKODBANKA", zipKodBanka, ParameterDirection.Input);
            dbManager.AddParameters(15, "@TELBANKA ", telBanka, ParameterDirection.Input);
            dbManager.AddParameters(16, "@EMERKONTAKTIBANKA", emerKontaktiBanka, ParameterDirection.Input);
            dbManager.AddParameters(17, "@MBIEMERKONTAKTIBANKA", mbiemerKontaktiBanka, ParameterDirection.Input);
            dbManager.AddParameters(18, "@TELKONTAKTIBANKA", telKontaktiBanka, ParameterDirection.Input);
            dbManager.AddParameters(19, "@FAXKONTAKTIBANKA", faxKontaktiBanka, ParameterDirection.Input);
            dbManager.AddParameters(20, "@CELKONTAKTIBANKA", celKontaktiBanka, ParameterDirection.Input);
            dbManager.AddParameters(21, "@EMAILKONTAKTIBANKA", emailKontaktiBanka, ParameterDirection.Input);
            dbManager.AddParameters(22, "@IDPERDORUESI", idPerdoruesi, ParameterDirection.Input);
            if (komis == 0 || komis == -1) dbManager.AddParameters(23, "@KOMISIONI", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(23, "@KOMISIONI", komis, ParameterDirection.Input);
            dbManager.AddParameters(24, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            dbManager.AddParameters(25, "@LLOJARKABANKA", llojarkabanka, ParameterDirection.Input);
            dbManager.AddParameters(26, "@ADRESAKONTAKTIBANKA", adresakontaktibanka, ParameterDirection.Input);
            dbManager.AddParameters(27, "@IDKONFIG", idkonfig, ParameterDirection.Input);
            if (iddegeadministrative == 0) dbManager.AddParameters(28, "@IDDEGEADMINISTRATIVE", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(28, "@IDDEGEADMINISTRATIVE", iddegeadministrative, ParameterDirection.Input);
            dbManager.AddParameters(29, "@IDSTATUSDOK", idstatusdok, ParameterDirection.Input);
            dbManager.AddParameters(30, "@DEGA", dega, ParameterDirection.Input);
            dbManager.AddParameters(31, "@VALUTA", valuta, ParameterDirection.Input);
            dbManager.AddParameters(32, "@NRKLIENTI", nrKlienti, ParameterDirection.Input);
            dbManager.AddParameters(33, "@KODILLOGARISE", kodiLlogarise, ParameterDirection.Input);
            dbManager.AddParameters(34, "@TIPI", tipi, ParameterDirection.Input);
            dbManager.AddParameters(35, "@KODITCR", kodiTCR, ParameterDirection.Input);
            dbManager.AddParameters(36, "@NUMRIRENDOR", nrRendor, ParameterDirection.Input);
            if(klientIFiskalizuar)
                dbManager.AddParameters(37, "@SHFAQNEEINVOICE", shfaqNeEinvoice, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_BANKA_upd");
            idBanka = int.Parse(dbManager.Parameters[0].Value.ToString());
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgModifikimiMeSukses"]);
            return mesazh;
        }
       
        /// <summary>
        /// Ekzekuton prc_T_BANKA_del per te fshire nje objekt clsBanka ne DB.
        ///<param name="banka">Objekt i tipit clsBanka qe do te fshihet</param>
        /// <returns> Kthen statusin e perfundimit te ekzekutimit te SP-se perkatese (nje objekt clsMesazh qe tregon nese veprimi eshte kryer me sukses apo jo.<seealso cref="DbCore.clsMesazh"/>)</returns>
        /// </summary>
        internal clsMesazh fshiBanke(int idBanka)
        {//fshin nje banke

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDBANKA", idBanka, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_BANKA_del");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
            return mesazh;


        }
        internal clsMesazh fshiBankeStatus(int idBanka, int idperdoruesi)
        {//fshin nje banke

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDBANKA", idBanka, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUESI", idperdoruesi, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_BANKA_upddel");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
            return mesazh;
        }
       
        /// <summary>
        /// Ekzekuton prc_T_BANKA_merrArkeBankeSipasKodit per te marre nje objekt clsBanka duke filtruar sipas kodit dhe id-se se ndermarrjes.
        ///<param name="kodi">Kodi i bankes</param>
        ///<param name="idnderm">Id e ndermarrjes</param>
        ///<returns>Kthen nje objekt clsBanka qe ploteson kushtet</returns>
        /// </summary>
        internal DataRow ktheBankeSipasKodit(string kodi, int idnderm)
        {// metoda per te marre nje banka ne baze te kodi te saj

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@KODIBANKA", kodi, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_BANKA_merrArkeBankeSipasKodit");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];

        }
        internal DataRow ktheBankeSipasKoditMeAutorizime(string kodi, int idnderm, int idperdorues)
        {// metoda per te marre nje banka ne baze te kodi te saj

            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@KODIBANKA", kodi, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDPERDORUES", idperdorues, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_BANKA_merrArkeBankeSipasKoditMeAutorizime");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];
        }

        /// <summary>
        /// Ekzekuton prc_T_BANKA_merrGjitheArkatBankat per te marre nje objekt clsBanka duke filtruar sipas id-se se perdoruesit dhe id-se se ndermarrjes.
        ///<param name="idperdorues">Id e perdoruesit</param>
        ///<param name="idnderm">Id e ndermarrjes</param>
        ///<returns>Kthen nje collection me objekte clsBanka qe plotesojne kushtet</returns>
        /// </summary>
        internal DataTable ktheGjitheBankatSipasAutorizimeve(int idnderm, int idperdorues)
        {//metoda per te marre te gjithe bankat

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUES", idperdorues, ParameterDirection.Input);

            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_BANKA_merrGjitheArkatBankat");
            return ds.Tables[0];
        }

        /// <summary>
        /// Ekzekuton prc_T_BANKA_merrGjitheArkatBankat per te marre nje objekt clsBanka duke filtruar sipas id-se se perdoruesit dhe id-se se ndermarrjes aktive.
        ///<param name="idperdorues">Id e perdoruesit</param>
        ///<param name="idnderm">Id e ndermarrjes</param>
        ///<returns>Kthen nje collection me objekte clsBanka qe plotesojne kushtet</returns>
        /// </summary>
        internal DataTable ktheGjitheBankatSipasAutorizimeveAktive(int idnderm, int idperdorues)
        {//metoda per te marre te gjithe bankat
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUES", idperdorues, ParameterDirection.Input);

            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_BANKA_merrGjitheArkatBankatAktive");
            return ds.Tables[0];
        }

        /// <summary>
        /// Ekzekuton prc_T_BANKA_merrGjitheArkatBankatSipasLlojit per te marre nje objekt clsBanka duke filtruar sipas id-se se perdoruesit dhe id-se se ndermarrjes aktive.
        ///<param name="idperdorues">Id e perdoruesit</param>
        ///<param name="idnderm">Id e ndermarrjes</param>
        ///<param name="lloji"> lloji true-banke, false-arke</param>
        ///<returns>Kthen nje collection me objekte clsBanka qe plotesojne kushtet</returns>
        /// </summary>
        internal DataTable ktheGjitheBankatSipasAutorizimeveSipasLlojit(int idnderm, int idperdorues, bool lloji, bool MerrNdemarrjeBija)
        {//metoda per te marre te gjithe bankat

            dbManager.Open();
            dbManager.CreateParameters(4);
            dbManager.AddParameters(0, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUES", idperdorues, ParameterDirection.Input);
            dbManager.AddParameters(2, "@LLOJARKABANKA", lloji, ParameterDirection.Input);
            dbManager.AddParameters(3, "@MERRNDERRMARJEBIJA", MerrNdemarrjeBija, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_BANKA_merrGjitheArkatBankatSipasLlojit");
            return ds.Tables[0];
        }

        /// <summary>
        /// Ekzekuton prc_T_BANKA_merrGjitheArkatBankatAktiveSipasAutorizimeveDheLlojit per te marre nje datatable te arkave apo bankave sipas autorizimit dhe llojit.
        ///<param name="idperdorues">Id e perdoruesit</param>
        ///<param name="idnderm">Id e ndermarrjes</param>
        ///<param name="lloji"> lloji true-banke, false-arke</param>
        ///<returns>Kthen nje collection me objekte clsBanka qe plotesojne kushtet</returns>
        /// </summary>
        internal DataTable ktheGjitheArkatBankatAktiveSipasAutorizimeveDheLlojit(int idnderm, int idperdorues, bool lloji)
        {//metoda per te marre te gjithe bankat            

            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUES", idperdorues, ParameterDirection.Input);
            dbManager.AddParameters(2, "@LLOJARKABANKA", lloji, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_BANKA_merrGjitheArkatBankatAktiveSipasAutorizimeveDheLlojit");
            return ds.Tables[0];
        }

        /// <summary>
        /// Ekzekuton prc_T_BANKA_merrGjitheArkatBankatSipasLlojit per te marre nje objekt clsBanka duke filtruar sipas id-se se perdoruesit dhe id-se se ndermarrjes aktive.
        ///<param name="idperdorues">Id e perdoruesit</param>
        ///<param name="idnderm">Id e ndermarrjes</param>
        ///<param name="lloji"> lloji true-banke, false-arke</param>
        ///<returns>Kthen nje collection me objekte clsBanka qe plotesojne kushtet</returns>
        /// </summary>
        internal DataTable ktheGjitheBankatSipasAutorizimeveSipasLlojitAll(int idnderm, bool lloji)
        {//metoda per te marre te gjithe bankat

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            dbManager.AddParameters(1, "@LLOJARKABANKA", lloji, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_BANKA_merrGjitheArkatBankatSipasLlojitAll");
            return ds.Tables[0];
        }
        

        internal DataTable ktheGjitheBankatSipasAutorizimeveLupa(int idnderm, int idperdorues)
        {//metoda per te marre te gjithe bankat

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUES", idperdorues, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_BANKA_merrPopupArkaBankat");
            return ds.Tables[0];
        }

        public DataTable merrBankaPerNdermarrje(int idnderm, int idperdorues)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUES", idperdorues, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_BANKA_merrGjitheBankatSipasNdermarjes");
            return ds.Tables[0];
        }

        internal DataTable ktheArkaBankaMefilter(string filter, long startIndex, long endIndex, bool lloji, int idnderm, int idperdorues)
        {//metoda per te marre te gjithe kliente furnitoret


            dbManager.Open();
            dbManager.CreateParameters(6);
            dbManager.AddParameters(0, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUES", idperdorues, ParameterDirection.Input);
            dbManager.AddParameters(2, "@LLOJARKABANKA", lloji, ParameterDirection.Input);
            dbManager.AddParameters(3, "@FILTER", filter, ParameterDirection.Input);
            dbManager.AddParameters(4, "@STARTINDEX", startIndex, ParameterDirection.Input);
            dbManager.AddParameters(5, "@ENDINDEX", endIndex, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_BANKA_merrGjitheArkatBankatSipasLlojitMeFilter");
            return ds.Tables[0];
        }

      
        /// <summary>
        /// Ekzekuton prc_T_BANKA_merrPopupArkaBankat per te marre nje dataset me objekte clsBanka duke filtruar sipas id-se se perdoruesit dhe id-se se ndermarrjes aktive.
        ///<param name="idnderm">Id e ndermarrjes</param>
        ///<param name="idperdorues">Id e perdoruesit</param>
        ///<returns>Kthen nje dataset me objekte clsBanka</returns>
        /// </summary>
        public DataSet merrPopupBankatSipasAutorizimeve(int idnderm, int idperdorues)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUES", idperdorues, ParameterDirection.Input);

            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_BANKA_merrPopupArkaBankat");
            return ds;
        }

        /// <summary>
        /// Ekzekuton prc_T_BANKA_merrPopupArkaBankatSipasLlojit per te marre nje dataset me objekte clsBanka duke filtruar sipas id-se se perdoruesit dhe id-se se ndermarrjes aktive.
        ///<param name="idnderm">Id e ndermarrjes</param>
        ///<param name="idperdorues">Id e perdoruesit</param>
        ///<param name="lloj">lloji true-banke, false-arke</param>
        ///<returns>Kthen nje dataset me objekte clsBanka</returns>
        /// </summary>
        public DataSet merrPopupBankatSipasAutorizimeveSipasLlojit(int idnderm, int idperdorues, bool lloj)
        {
            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUES", idperdorues, ParameterDirection.Input);
            dbManager.AddParameters(2, "@LLOJARKABANKA", lloj, ParameterDirection.Input);

            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_BANKA_merrPopupArkaBankatSipasLlojit");
            return ds;
        }

        /// <summary>
        /// Ekzekuton prc_T_BANKA_ktheArkeBanke per te marre nje objekt clsBanka duke filtruar sipas id-se se bankes.
        ///<param name="id">Id e bankes</param>
        ///<returns>Kthen nje collection me objekte clsBanka qe plotesojne kushtin</returns>
        /// </summary>
        internal DataRow merrBanke(int id)
        {//kthen banken sipas id            

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDBANKA", id, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_BANKA_ktheArkeBanke");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];
        }

        internal DataTable merrBankeSipasId(int id)
        {//kthen banken sipas id

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDBANKA", id, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_BANKA_ktheArkeBanke");
            if (ds == null)
                return null;
            return ds.Tables[0];

        }

        /// <summary>
        /// kthen Id e bankes ne baze te kodit te bankes dhe id ndermarrjes. Therret SP-ne:
        /// <see cref="prc_T_BANKA_ktheIdBankeSipasKodit"/>
        /// </summary>
        /// <param name="kodBanka"></param>
        /// <param name="idNdermarrje"></param>
        /// <returns></returns>
        internal int ktheIdBankaSipasKodDheIdNderm(string kodBanka, int idNdermarrje)
        {

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@KODIBANKA", kodBanka, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idNdermarrje, ParameterDirection.Input);
            return Convert.ToInt32(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_BANKA_ktheIdBankeSipasKodit"));

        }

        /// <summary>
        /// kthen Id e bankes ne baze te kodit te bankes dhe id ndermarrjes. Therret SP-ne:
        /// <see cref="prc_T_BANKA_ktheIdBankeSipasKodit"/>
        /// </summary>
        /// <param name="kodBanka"></param>
        /// <param name="idNdermarrje"></param>
        /// <returns></returns>
        internal int ktheLlojBankaSipasKodDheIdNderm(string kodBanka, int idNdermarrje)
        {

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@KODIBANKA", kodBanka, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idNdermarrje, ParameterDirection.Input);
            object idllojbanka = dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_BANKA_ktheLlojBankeSipasKodit");
            if (idllojbanka == null)
                return -1;
            else
                return Convert.ToInt32(idllojbanka);

        }

        /// <summary>
        /// kthen Id e monedhes se bankes ne baze te kodit te bankes dhe id ndermarrjes. Therret SP-ne:
        /// <see cref="prc_T_BANKA_ktheIdBankeSipasKodit"/>
        /// </summary>
        /// <param name="kodBanka"></param>
        /// <param name="idNdermarrje"></param>
        /// <returns></returns>
        internal int ktheIdMonedhaBankaSipasKodDheIdNderm(string kodBanka, int idNdermarrje)
        {

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@KODIBANKA", kodBanka, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idNdermarrje, ParameterDirection.Input);
            return Convert.ToInt32(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_BANKA_ktheIdMonedheBankeSipasKodit"));

        }

        /// <summary>
        /// Ekzekuton prc_T_BANKA_ekzistonArkeBanke per te kontrolluar nese ekziston nje objekt clsBanka ne DB duke filtruar sipas kodit te bankes dhe id-se se ndermarrjes.
        ///<param name="banka">Objekt i tipit clsBanka</param>
        /// <returns> Kthen statusin e perfundimit te ekzekutimit te SP-se perkatese (nje objekt clsMesazh qe tregon nese ekziston banka apo jo.<seealso cref="DbCore.clsMesazh"/>)</returns>
        /// </summary>
        public clsMesazh ekzistonBanke(string kodiBanka, int idndermarrje)
        {//kontrollon nqs ekziston nje banka me kete kod

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@KODIBANKA", kodiBanka, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idndermarrje, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_BANKA_ekzistonArkeBanke");
            if (ds.Tables[0].Rows.Count > 0)
                return new clsMesazh(true, "Ekziston nje arke banke me kete kod!");
            else if (ds.Tables[0].Rows.Count == 0)
                return new clsMesazh(false);
            else return new clsMesazh(true);

        }

        /// <summary>
        /// Ekzekuton prc_T_BANKA_ktheGjendjenBankes per te marre gjendjen e nje objekti clsBanka duke filtruar sipas id-se se bankes, id-se se perdoruesit, id-se lidhese ndermarrje - vit dhe dates se dokumentit
        ///<param name="idBanke">Id e bankes</param>
        /// <param name="idndermarje">Id lidhese ndermarrje - vit</param>
        ///<param name="idPerdoruesi">Id e perdoruesit</param>
        ///<param name="dtDokumenti">Data e dokumentit</param>
        ///<returns>Kthen vleren e gjendjes se bankes</returns>
        /// </summary>
        public double ktheGjendjenBankes(int idBanke,bool llojiArkaBanka, int idPerdoruesi, int idndermarje, DateTime dtDokumenti)
        {//kontrollon nqs ekziston nje banka me kete kod
            double gjendja = double.Parse("0.00");

            dbManager.Open();
            dbManager.CreateParameters(6);
            dbManager.AddParameters(0, "@GJENDJA", 0.0, ParameterDirection.Output);
            dbManager.AddParameters(1, "@IDBANKA", idBanke, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDLLOJIARKABANKA", llojiArkaBanka, ParameterDirection.Input);
            dbManager.AddParameters(3, "@IDPERDORUESI", idPerdoruesi, ParameterDirection.Input);
            dbManager.AddParameters(4, "@IDNDERmarje", idndermarje, ParameterDirection.Input);
            dbManager.AddParameters(5, "@DTDOKUMENTI", dtDokumenti, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_BANKA_ktheGjendjenBankes");
            gjendja = double.Parse(dbManager.Parameters[0].Value.ToString());
            return gjendja;

        }
        internal DataRow ktheABNdermarrjesAndAutorizimeDR(int idnderm, int idperdorues, int idbanka)
        {
            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUES", idperdorues, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDBANKA", idbanka, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_BANKA_ktheABNdermarrjesAndAutorizimeDR");

            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];
        }

        internal DataTable ktheABNdermarrjesAndAutorizimeDT(int idnderm, int idperdorues, bool MerrNdemarrjeBija)
        {
            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUES", idperdorues, ParameterDirection.Input);
            dbManager.AddParameters(2, "@MERRNDERRMARJEBIJA", MerrNdemarrjeBija, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_BANKA_ktheABNdermarrjesAndAutorizimeDT");

            return ds.Tables[0];
        }

        internal DataTable ktheABNdermarrjesAndAutorizimeDTSipasFiltrit(int idnderm, int idperdorues,string filter, int startIndex, int endIndex)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUES", idperdorues, ParameterDirection.Input);
            dbManager.AddParameters(2, "@FILTER", filter, ParameterDirection.Input);
            dbManager.AddParameters(3, "@STARTINDEX", startIndex, ParameterDirection.Input);
            dbManager.AddParameters(4, "@ENDINDEX", endIndex, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_BANKA_ktheABNdermarrjesAndAutorizimeDTSipasFiltrit");

            return ds.Tables[0];
        }

        internal DataTable ktheABNdermarrjesAndAutorizimeDTSipasMonedhes(int idnderm, int idperdorues, int idmonnderm, int idmonklienti)
        {
            dbManager.Open();
            dbManager.CreateParameters(4);
            dbManager.AddParameters(0, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUES", idperdorues, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDMONNDERM", idmonnderm, ParameterDirection.Input);
            dbManager.AddParameters(3, "@IDMONKLIENTI", idmonklienti, ParameterDirection.Input);

            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_BANKA_ktheABNdermarrjesAndAutorizimeDTSipasMonedhes");

            return ds.Tables[0];
        }

        internal DataTable ktheABNdermarrjesAndAutorizimeDTSipasMonedhesDheFiltrit(int idnderm, int idperdorues, int idmonnderm, int idmonklienti, string filter, int startIndex, int endIndex)
        {
            dbManager.Open();
            dbManager.CreateParameters(7);
            dbManager.AddParameters(0, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUES", idperdorues, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDMONNDERM", idmonnderm, ParameterDirection.Input);
            dbManager.AddParameters(3, "@IDMONKLIENTI", idmonklienti, ParameterDirection.Input);
            dbManager.AddParameters(4, "@FILTER", filter, ParameterDirection.Input);
            dbManager.AddParameters(5, "@STARTINDEX", startIndex, ParameterDirection.Input);
            dbManager.AddParameters(6, "@ENDINDEX", endIndex, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_BANKA_ktheABNdermarrjesAndAutorizimeDTSipasMonedhesDheFiltrit");

            return ds.Tables[0];
        }
        internal DataTable ktheABNdermarrjesAndAutorizimeDTLupa(int idnderm, int idperdorues, bool MerrNdemarrjeBija)
        {
            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUES", idperdorues, ParameterDirection.Input);
            dbManager.AddParameters(2, "@MERRNDERRMARJEBIJA", MerrNdemarrjeBija, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_BANKA_ktheABNdermarrjesAndAutorizimeDTLupa");
            return ds.Tables[0];
        }

        #endregion

        /// <summary>
        /// Metodat e meposhtme i perkasin veprimeve qe kryhen nga objektet clsGrupBanke dhe colGrupeBanke
        /// </summary>
        #region GRUPE BANKE

        /// <summary>
        /// Ekzekuton prc_T_GRUPBANKE_ins per te ruajtur nje objekt clsGrupBanke ne DB.
        ///<param name="grupBanke">Objekt i tipit clsGrupBanke qe do te ruhet ne DB</param>
        /// <returns> Kthen statusin e perfundimit te ekzekutimit te SP-se perkatese (nje objekt clsMesazh qe tregon nese veprimi eshte kryer me sukses apo jo.<seealso cref="DbCore.clsMesazh"/>)</returns>
        /// </summary>
        internal clsMesazh ruajGrupBanke(int idgrupbanke, string nrgrupbanke, string pershkrimgrupbanke, int idperdoruesi, bool llojarkabanka, int idndermarje, int idstatusdok)
        {//ruan nje grup Banke

            dbManager.Open();
            dbManager.CreateParameters(7);
            dbManager.AddParameters(0, "@IDGRUPBANKE", idgrupbanke, ParameterDirection.Output);
            dbManager.AddParameters(1, "@NRGRUPBANKE", nrgrupbanke, ParameterDirection.Input);
            dbManager.AddParameters(2, "@PERSHKRIMGRUPBANKE", pershkrimgrupbanke, ParameterDirection.Input);
            dbManager.AddParameters(3, "@IDPERDORUESI", idperdoruesi, ParameterDirection.Input);
            dbManager.AddParameters(4, "@LLOJARKABANKA", llojarkabanka, ParameterDirection.Input);
            dbManager.AddParameters(5, "@IDNDERMARJE", idndermarje, ParameterDirection.Input);
            dbManager.AddParameters(6, "@IDSTATUSDOK", idstatusdok, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_GRUPBANKE_ins");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
            return mesazh;

        }
      
        /// <summary>
        /// Ekzekuton prc_T_GRUPBANKE_upd per te modifikuar nje objekt clsGrupBanke ne DB.
        ///<param name="grupBanke">Objekt i tipit clsGrupBanke qe do te modifikohet</param>
        /// <returns> Kthen statusin e perfundimit te ekzekutimit te SP-se perkatese (nje objekt clsMesazh qe tregon nese veprimi eshte kryer me sukses apo jo.<seealso cref="DbCore.clsMesazh"/>)</returns>
        /// </summary>
        internal clsMesazh modifikoGrupBanke(int idgrupbanke, string nrgrupbanke, string pershkrimgrupbanke, int idperdoruesi, bool llojarkabanka, int idndermarje, int idstatusdok)
        {//modifikon nje grupBanke

            dbManager.Open();
            dbManager.CreateParameters(7);
            dbManager.AddParameters(0, "@IDGRUPBANKE", idgrupbanke, ParameterDirection.Input);
            dbManager.AddParameters(1, "@NRGRUPBANKE", nrgrupbanke, ParameterDirection.Input);
            dbManager.AddParameters(2, "@PERSHKRIMGRUPBANKE", pershkrimgrupbanke, ParameterDirection.Input);
            dbManager.AddParameters(3, "@IDPERDORUESI", idperdoruesi, ParameterDirection.Input);
            dbManager.AddParameters(4, "@LLOJARKABANKA", llojarkabanka, ParameterDirection.Input);
            dbManager.AddParameters(5, "@IDNDERMARJE", idndermarje, ParameterDirection.Input);
            dbManager.AddParameters(6, "@IDSTATUSDOK", idstatusdok, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_GRUPBANKE_upd");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgModifikimiMeSukses"]);
            return mesazh;

        }

        /// <summary>
        /// Ekzekuton prc_T_GRUPBANKE_del per te fshire nje objekt clsGrupBanke ne DB.
        ///<param name="grupBanke">Objekt i tipit clsGrupBanke qe do te fshihet</param>
        /// <returns> Kthen statusin e perfundimit te ekzekutimit te SP-se perkatese (nje objekt clsMesazh qe tregon nese veprimi eshte kryer me sukses apo jo.<seealso cref="DbCore.clsMesazh"/>)</returns>
        /// </summary>
        internal clsMesazh fshiGrupBanke(int idgrupbanke)
        {//fshin nje grup Banke

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDGRUPBANKE", idgrupbanke, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_GRUPBANKE_del");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
            return mesazh;

        }
        internal clsMesazh fshiGrupBankeStatus(int idgrupbanke, int idperdorues)
        {//fshin nje grup Banke

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDGRUPBANKE", idgrupbanke, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUESI", idperdorues, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_GRUPBANKE_upddel");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
            return mesazh;

        }
       
        /// <summary>
        /// Nuk perdoret
        /// </summary>
        internal void merrGrupBanke(int idgrupbanke)
        {//merr nje grup Banke

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDGRUPBANKE", idgrupbanke, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_GRUPBANKE_sel");

        }
       
        /// <summary>
        /// Merr gjithe objektet clsGrupBanke ne DB.
        /// <param name="idnderm"> id e ndermarjes</param>
        /// <returns> Kthen nje collection me objekte clsGrupBanke</returns>
        /// </summary>
        internal DataTable ktheGjitheGrupetBankeSipasNdermarjes(int idnderm)
        {//metoda per te marre te gjithe grupet

            dbManager.Open();
            DataSet ds = dbManager.ExecuteDataSet(CommandType.Text, "select * from T_GRUPBANKE where idstatusdok=1 and IDNDERMARJE=" + idnderm);
            return ds.Tables[0];

        }
       
        /// <summary>
        /// Merr gjithe objektet clsGrupBanke ne DB sipas llojit.
        /// <param name="lloji"> lloji true-banka, false-arka</param>
        /// <param name="idnderm">id e ndermarjes</param>
        /// <returns> Kthen nje collection me objekte clsGrupBanke</returns>
        /// </summary>
        internal DataTable ktheGjitheGrupetBankeSipasLlojit(bool lloji, int idnderm)
        {//metoda per te marre te gjithe grupet

            dbManager.Open();
            DataSet ds = dbManager.ExecuteDataSet(CommandType.Text, "select * from T_GRUPBANKE where idstatusdok=1 and LLOJARKABANKA='" + lloji + "' and idndermarje=" + idnderm);
            return ds.Tables[0];

        }
       
        /// <summary>
        /// Ekzekuton prc_T_GRUPBANKE_merrGrupArkeBankeSipasKodit per te marre nje objekt clsGrupBanke ne DB duke filtruar sipas numrit te grupit te bankes.
        ///<param name="kodi">Numri(kodi) i grupit te bankes</param>
        ///<param name="idnderm">id e ndermarjes</param>
        /// <returns> Kthen nje collection me objekte clsGrupBanke qe plotesojne kushtin</returns>
        /// </summary>
        internal DataRow ktheGrupBankeSipasKodit(string kodi, int idnderm)
        {//metoda per te marre te grupin sipas kodit

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@NRGRUPBANKE", kodi, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_GRUPBANKE_merrGrupArkeBankeSipasKodit");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];

        }
        
        /// <summary>
        /// Ekzekuton prc_T_GRUPBANKE_ktheGrupArkeBankeSipasId per te marre nje objekt clsGrupBanke ne DB duke filtruar sipas id-se te grupit te bankes.
        ///<param name="id">ID e grupit te bankes</param>
        /// <returns> Kthen nje collection me objekte clsGrupBanke qe plotesojne kushtin</returns>
        /// </summary>
        internal DataRow merrGrupBankeSipasId(int id)
        {//metoda per te marre grupin sipas id

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDGRUPBANKE", id, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_GRUPBANKE_ktheGrupArkeBankeSipasId");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];

        }

        /// <summary>
        /// Ekzekuton prc_T_GRUPBANKE_ekzistonGrupArkeBanke per te kontrolluar nese ekziston nje objekt clsGrupBanke ne DB duke filtruar sipas numrit te grupit te bankes.
        ///<param name="nr">Numri(kodi) i grupit te bankes</param>
        ///<param name="idnderm">id e ndermarjes</param>
        /// <returns> Kthen true nese ekziston nje objekt clsGrupBanke qe ploteson kushtin</returns>
        /// </summary>
        public bool ekzistonGrupBanke(string nr, int idnderm)
        {//kontrollon nese ekziston nje grup Banke me kete nr
          

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@NRGRUPBANKE", nr, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_GRUPBANKE_ekzistonGrupArkeBanke");
            if (ds.Tables[0].Rows.Count == 1)
                return true;
            else if (ds.Tables[0].Rows.Count == 0)
                return false;
            else return true;

        }

        /// <summary>
        /// Ekzekuton prc_T_GRUPBANKE_kaArkeBanke per te kontrolluar nese ekziston nje grup banke i caktuar ka banka apo jo.
        ///<param name="id">Id e grupit te bankes</param>
        /// <returns> Kthen true nese ky grup banke ka banke</returns>
        /// </summary>
        public bool kaBanke(int id)
        {//kontrollon nese ky grup ka banke
            
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDGRUPBANKE", id, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_GRUPBANKE_kaArkeBanke");
            if (ds.Tables[0].Rows.Count == 1)
                return true;
            else if (ds.Tables[0].Rows.Count == 0)
                return false;
            else return true;

        }

        #endregion

        /// <summary>
        /// Metodat e meposhtme i perkasin veprimeve qe kryhen nga objektet colKonfigUrdherPagese dhe clsKonfigUrdherPagese
        /// </summary>
        #region KONFIG URDHER PAGESE

        /// <summary>
        /// Ekzekuton prc_T_KONFIGURDHERPAGESE_ins per te ruajtur nje objekt clsKonfigUrdherPagese ne DB.
        /// <returns> Kthen statusin e perfundimit te ekzekutimit te SP-se perkatese (nje objekt clsMesazh qe tregon nese veprimi eshte kryer me sukses apo jo.<seealso cref="DbCore.clsMesazh"/>)</returns>
        /// </summary>
        internal clsMesazh ruajKonfigUrdherPagese(out int id, string kodi, string pershkrimi, int lloji, int idperdoruesi, int idndermarje, int idstatusdok)
        {
            id = -1;

            dbManager.Open();
            dbManager.CreateParameters(7);
            dbManager.AddParameters(0, "@ID", id, ParameterDirection.Output);
            dbManager.AddParameters(1, "@KODI", kodi, ParameterDirection.Input);
            dbManager.AddParameters(2, "@PERSHKRIMI", pershkrimi, ParameterDirection.Input);
            dbManager.AddParameters(3, "@LLOJI", lloji, ParameterDirection.Input);
            dbManager.AddParameters(4, "@IDPERDORUESI", idperdoruesi, ParameterDirection.Input);
            dbManager.AddParameters(5, "@IDNDERMARJE", idndermarje, ParameterDirection.Input);
            dbManager.AddParameters(6, "@IDSTATUSDOK", idstatusdok, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_KONFIGURDHERPAGESE_ins");
            id = int.Parse(dbManager.Parameters[0].Value.ToString());
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
            return mesazh;

        }

        /// <summary>
        /// Ekzekuton prc_T_KONFIGURDHERPAGESE_upd per te modifikuar nje objekt clsKonfigUrdherPagese ne DB.
        /// <returns> Kthen statusin e perfundimit te ekzekutimit te SP-se perkatese (nje objekt clsMesazh qe tregon nese veprimi eshte kryer me sukses apo jo.<seealso cref="DbCore.clsMesazh"/>)</returns>
        /// </summary>
        internal clsMesazh modifikoKonfigUrdherPagese(int id, string kodi, string pershkrimi, int lloji, int idperdoruesi, int idndermarje, int idstatusdok)
        {//modifikon nje grupBanke

            dbManager.Open();
            dbManager.CreateParameters(7);
            dbManager.AddParameters(0, "@ID", id, ParameterDirection.Input);
            dbManager.AddParameters(1, "@KODI", kodi, ParameterDirection.Input);
            dbManager.AddParameters(2, "@PERSHKRIMI", pershkrimi, ParameterDirection.Input);
            dbManager.AddParameters(3, "@LLOJI", lloji, ParameterDirection.Input);
            dbManager.AddParameters(4, "@IDPERDORUESI", idperdoruesi, ParameterDirection.Input);
            dbManager.AddParameters(5, "@IDNDERMARJE", idndermarje, ParameterDirection.Input);
            dbManager.AddParameters(6, "@IDSTATUSDOK", idstatusdok, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_KONFIGURDHERPAGESE_upd");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgModifikimiMeSukses"]);
            return mesazh;

        }

        /// <summary>
        /// Ekzekuton prc_T_KONFIGURDHERPAGESE_del per te fshire nje objekt clsKonfigUrdherPagese ne DB.
        /// <returns> Kthen statusin e perfundimit te ekzekutimit te SP-se perkatese (nje objekt clsMesazh qe tregon nese veprimi eshte kryer me sukses apo jo.<seealso cref="DbCore.clsMesazh"/>)</returns>
        /// </summary>
        internal clsMesazh fshiKonfigUrdherPagese(int id)
        {

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@ID", id, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_KONFIGURDHERPAGESE_del");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
            return mesazh;

        }

        /// <summary>
        /// fshin konfigurimin duke i ndryshuar statusin ne te fshire
        /// </summary>
        /// <param name="id">id konfigurimi</param>
        /// <param name="idperdorues">idperdoruesi</param>
        /// <returns></returns>
        internal clsMesazh fshiKonfigUrdherPageseStatus(int id, int idperdorues)
        {

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@ID", id, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUESI", idperdorues, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_KONFIGURDHERPAGESE_upddel");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
            return mesazh;

        }

        /// <summary>
        /// Merr gjithe objektet clskonfigurdherpagese ne DB.
        /// <param name="idnderm"> id e ndermarjes</param>
        /// <param name="lloji">lloji</param>
        /// <returns> Kthen nje collection me objekte clsKonfigUrdherPagese</returns>
        /// </summary>
        internal DataTable ktheGjitheKonfigUrdherPageseSipasNdermarjesDheLlojit(int idnderm, int lloji)
        {

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@LLOJI", lloji, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KONFIGURDHERPAGESE_merrSipasNdermarrjesDheLlojit");
            return ds.Tables[0];

        }
        /// <summary>
        /// Merr gjithe objektet clskonfigurdherpagese ne DB.
        /// <param name="idnderm"> id e ndermarjes</param>
        /// <param name="lloji">lloji</param>
        /// <returns> Kthen nje collection me objekte clsKonfigUrdherPagese</returns>
        /// </summary>
        internal DataTable ktheGjitheKonfigUrdherPageseSipasNdermarjesDheLlojitDheKodiLike(int idnderm, int lloji, string kodi)
        {

            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@LLOJI", lloji, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            dbManager.AddParameters(2, "@prefix", kodi, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KONFIGURDHERPAGESE_merrSipasNdermarrjesDheLlojitDheKodiLike");
            return ds.Tables[0];

        }

        /// <summary>
        /// Ekzekuton prc_T_KONFIGURDHERPAGESE_merrKonfigUrdherPageseSipasKoditDheLlojit per te marre nje objekt  ne DB duke filtruar kodit, ndermarjes dhe llojit.
        ///<param name="kodi">kodi</param>
        ///<param name="idnderm">id e ndermarjes</param>
        ///<param name="lloji">lloji</param>
        /// <returns> Kthen nje collection me objekte  qe plotesojne kushtin</returns>
        /// </summary>
        internal DataRow ktheKonfigUrdherPageseSipasKoditDheLlojit(string kodi, int idnderm, int lloji)
        {

            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@KODI", kodi, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            dbManager.AddParameters(2, "@LLOJI", lloji, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KONFIGURDHERPAGESE_merrKonfigUrdherPageseSipasKoditDheLlojit");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];

        }

        /// <summary>
        /// Ekzekuton prc_T_KONFIGURDHERPAGESE_ktheKonfigSipasId per te marre nje objekt  ne DB duke filtruar sipas id-se .
        ///<param name="id">ID </param>
        /// <returns> Kthen nje collection me objekte  qe plotesojne kushtin</returns>
        /// </summary>
        internal DataRow merrKonfigUrdherPageseSipasId(int id)
        {

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@ID", id, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KONFIGURDHERPAGESE_ktheKonfigSipasId");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];

        }

        /// <summary>
        /// Ekzekuton prc_T_KONFIGURDHERPAGESE_ekzistonKonfigurimSipasLlojit per te kontrolluar nese ekziston nje objekt  ne DB duke filtruar sipas kodit ndermarjes dhe llojit
        ///<param name="kodi">kodi</param>
        ///<param name="idnderm">id e ndermarjes</param>
        ///<param name="lloji">lloji </param>
        /// <returns> Kthen true nese ekziston nje objekt  qe ploteson kushtin</returns>
        /// </summary>
        public bool ekzistonKonfigUrdherPagese(string kodi, int idnderm, int lloji)
        {
           
            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@KODI", kodi, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            dbManager.AddParameters(2, "@LLOJI", lloji, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KONFIGURDHERPAGESE_ekzistonKonfigurimSipasLlojit");
            if (ds.Tables[0].Rows.Count == 1)
                return true;
            else if (ds.Tables[0].Rows.Count == 0)
                return false;
            else return true;

        }

        /// <summary>
        /// Ekzekuton prc_T_KONFIGURDHERPAGESE_kaVeprime per te kontrolluar nese ka veprime me kete konfigurim.
        ///<param name="id">Id e konfigurimit</param>
        /// <returns> Kthen true nese ka veprime</returns>
        /// </summary>
        public bool kaVeprime(int id)
        {
           
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@ID", id, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KONFIGURDHERPAGESE_kaVeprime");
            if (ds.Tables[0].Rows.Count == 1)
                return true;
            else if (ds.Tables[0].Rows.Count == 0)
                return false;
            else return true;

        }        

        #endregion


        /// <summary>
        /// Metodat e meposhtme i perkasin veprimeve qe kryhen nga objektet clsTrupiUrdherPagese dhe colTrupiUrdherPagese
        /// </summary>
        #region TRUPI URDHER PAGESE

        /// <summary>
        /// ekzekuton prc_T_TRUPIUDHERPAGESA_ins ne DB dhe kthen statusin e perfundimit te ekzekutimit te SP-se perkatese
        /// Shiko clsMesazh brenda ketij moduli per informacion te metejshem mbi vleren e kthimit te kesaj metode
        /// <param name="idgrupi"> id e grupit</param>
        /// <param name="idKoka"> id e kokes se dokumentit </param>
        /// <param name="idTrup">id ritese e trupit te dokumentit </param>
        /// <param name="shuma"> shuma</param>
        /// <param name="idkapitulli">id e kapitullit</param>
        /// <param name="idllogana">id e llogarise analitike</param>
        /// <param name="idllogart"> id e llogarise se artikullit</param>
        /// <param name="idtitulli">id e titullit</param>
        /// <param name="kodprojekti"> kodi i projektit</param>
        /// <param name="objekti">objekti</param>
        /// <returns> nje objekt clsMesazh qe tregon nese ruajtja eshte kryer ne rregull apo jo.</returns>
        /// </summary>
        internal clsMesazh ruajTrupiUrdherPagese(out int idTrup, int idKoka, int idgrupi, int idtitulli, int idkapitulli, int idllogart, int idllogana, string kodprojekti, decimal shuma, string objekti)
        {
            idTrup = -1;

            dbManager.Open();
            dbManager.CreateParameters(10);
            dbManager.AddParameters(0, "@IDTRUPI", idTrup, ParameterDirection.Output);
            dbManager.AddParameters(1, "@IDKOKA", idKoka, ParameterDirection.Input);
            if (idgrupi == 0) dbManager.AddParameters(2, "@IDGRUPI", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(2, "@IDGRUPI", idgrupi, ParameterDirection.Input);
            if (idtitulli == 0) dbManager.AddParameters(3, "@IDTITULLI", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(3, "@IDTITULLI", idtitulli, ParameterDirection.Input);
            if (idkapitulli == 0) dbManager.AddParameters(4, "@IDKAPITULLI", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(4, "@IDKAPITULLI", idkapitulli, ParameterDirection.Input);
            if (idllogart == 0) dbManager.AddParameters(5, "@IDLLOGARTIKULLI", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(5, "@IDLLOGARTIKULLI", idllogart, ParameterDirection.Input);
            if (idllogana == 0) dbManager.AddParameters(6, "@IDLLOGANALIZA", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(6, "@IDLLOGANALIZA", idllogana, ParameterDirection.Input);
            dbManager.AddParameters(7, "@KODPROJEKTI", kodprojekti, ParameterDirection.Input);
            dbManager.AddParameters(8, "@OBJEKTI", objekti, ParameterDirection.Input);
            dbManager.AddParameters(9, "@SHUMA", shuma, ParameterDirection.Input);

            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_TRUPIUDHERPAGESA_ins");

            idTrup = int.Parse(dbManager.Parameters[0].Value.ToString());

            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
            return mesazh;

        }

        /// <summary>
        /// ekzekuton prc_T_TRUPIUDHERPAGESA_upd ne DB dhe kthen statusin e perfundimit te ekzekutimit te SP-se perkatese
        /// Shiko clsMesazh brenda ketij moduli per informacion te metejshem mbi vleren e kthimit te kesaj metode
        /// <param name="idgrupi"> id e grupit</param>
        /// <param name="idKoka"> id e kokes se dokumentit </param>
        /// <param name="idTrup">id ritese e trupit te dokumentit </param>
        /// <param name="shuma"> shuma</param>
        /// <param name="idkapitulli">id e kapitullit</param>
        /// <param name="idllogana">id e llogarise analitike</param>
        /// <param name="idllogart"> id e llogarise se artikullit</param>
        /// <param name="idtitulli">id e titullit</param>
        /// <param name="kodprojekti"> kodi i projektit</param>
        /// <param name="objekti">objekti</param>
        /// <returns> nje objekt clsMesazh qe tregon nese modifikimi eshte kryer ne rregull apo jo.</returns>
        /// </summary>
        internal clsMesazh modifikoTrupiUrdherPagese(int idTrup, int idKoka, int idgrupi, int idtitulli, int idkapitulli, int idllogart, int idllogana, string kodprojekti, decimal shuma, string objekti)
        {

            dbManager.Open();
            dbManager.CreateParameters(10);
            dbManager.AddParameters(0, "@IDTRUPI", idTrup, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDKOKA", idKoka, ParameterDirection.Input);
            if (idgrupi == 0) dbManager.AddParameters(2, "@IDGRUPI", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(2, "@IDGRUPI", idgrupi, ParameterDirection.Input);
            if (idtitulli == 0) dbManager.AddParameters(3, "@IDTITULLI", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(3, "@IDTITULLI", idtitulli, ParameterDirection.Input);
            if (idkapitulli == 0) dbManager.AddParameters(4, "@IDKAPITULLI", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(4, "@IDKAPITULLI", idkapitulli, ParameterDirection.Input);
            if (idllogart == 0) dbManager.AddParameters(5, "@IDLLOGARTIKULLI", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(5, "@IDLLOGARTIKULLI", idllogart, ParameterDirection.Input);
            if (idllogana == 0) dbManager.AddParameters(6, "@IDLLOGANALIZA", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(6, "@IDLLOGANALIZA", idllogana, ParameterDirection.Input);
            dbManager.AddParameters(7, "@KODPROJEKTI", kodprojekti, ParameterDirection.Input);
            dbManager.AddParameters(8, "@OBJEKTI", objekti, ParameterDirection.Input);
            dbManager.AddParameters(9, "@SHUMA", shuma, ParameterDirection.Input);

            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_TRUPIUDHERPAGESA_upd");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgModifikimiMeSukses"]);
            return mesazh;

        }

        /// <summary>
        /// ekzekutohet sp-ja prc_T_TRUPIUDHERPAGESA_delSipasIdKoka duke i kaluar id e kokes se dokumentit
        /// </summary>
        /// <param name="idKoka"> id e kokes se dokumentit </param>
        /// <returns>nje objekt clsMesazh qe tregon nese fshirja eshte kryer ne rregull apo jo</returns>
        internal clsMesazh fshiTrupiUrdherPageseSipasKoka(int idKoka)
        {

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDKOKA", idKoka, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_TRUPIUDHERPAGESA_delSipasIdKoka");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
            return mesazh;

        }

        /// <summary>
        /// ekzekutohet sp-ja prc_T_TRUPIUDHERPAGESA_delSipasIdTrupi duke i kaluar id e trupit te dokumentit 
        /// </summary>
        /// <param name="idTrup">id ritese e trupit te dokumentit </param>
        /// <returns>nje objekt clsMesazh qe tregon nese fshirja eshte kryer ne rregull apo jo</returns>
        internal clsMesazh fshiTrupiUrdherPageseSipasID(int idTrup)
        {

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDTRUPI", idTrup, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_TRUPIUDHERPAGESA_delSipasIdTrupi");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
            return mesazh;

        }

        /// <summary>
        /// kthen datatable trupi dokumenti sipas kokes
        /// </summary>
        ///<param name="idKoka"> id e kokes se dokumentit</param>
        ///<returns>nje datatable qe permban nje koleksion me te gjitha trupat e dokumentit  te kesaj koke  </returns>
        internal DataTable ktheGjitheTrupiUrdherPageseNgaKoka(int idKoka)
        {

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDKOKA", idKoka, ParameterDirection.Input);
            using (DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_TRUPIUDHERPAGESA_selAllSipasKoka"))
            {
                return ds.Tables[0];
            }

        }

        /// <summary>
        /// kthen datarow trupi dokumenti sipas idse se trupit te dokumentit
        /// </summary>
        ///<param name="idTrupi"> trupi i dokumentit te  nga merret id</param>
        ///<returns>nje datarow me te gjithe trupin e dokumentit te shitjes te kesaj id  </returns>
        internal DataRow ktheTrupiUrdherPageseSipasID(int idTrupi)
        {

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDTRUPI", idTrupi, ParameterDirection.Input);
            using (DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_TRUPIUDHERPAGESA_sel"))
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
        /// Metodat e meposhtme i perkasin veprimeve qe kryhen nga objektet clsKokaUrdherPagese dhe colKokaUrdherPagese
        /// </summary>
        #region KOKA URDHER PAGESE

        /// <summary>
        /// ekzekuton prc_T_KOKAURDHERPAGESE_ins ne DB dhe kthen statusin e perfundimit te ekzekutimit te SP-se perkatese
        /// Shiko clsMesazh brenda ketij moduli per informacion te metejshem mbi vleren e kthimit te kesaj metode
        /// <param name="dtDk"> data e dokumentit </param>
        /// <param name="dtdokngjitur"> data e dokumentit ngjitur </param>
        /// <param name="nipti"> nipti</param>
        /// <param name="nrkupon"> nr kuponi</param>
        /// <param name="idKonf"> id e konfigurimit te dokumentit</param>
        /// <param name="iddoknga">id e dokumentit nga e cila gjenerohet ne rastet e modifikimit </param>
        /// <param name="nrpunonjesve">nr punonjesve</param>
        /// <param name="idKoka">id ritese e kokes se magazines</param>
        /// <param name="idNder"> id e ndermarjes</param>
        /// <param name="idNdVt"> id e ndermarje vitit</param>
        /// <param name="idNiv"> id e nivelit te regjistrimit</param>
        /// <param name="idPer"> id e perdoruesit se e ben regjistrimin</param>
        /// <param name="emriperfituesit"> emri perfituesit</param>
        /// <param name="idSt"> id e statusit te dokumentit</param>
        /// <param name="nrDk">nr i dokumentit</param>
        /// <param name="totali"> totali</param>
        /// <param name="nrllogbankare"> nr llog bankare </param>
        /// <param name="emribankes"> emer banke</param>
        /// <param name="idGjenerues">id e dokumentit nga gjenerohet magazina nga nje ambjent tjeter</param>
        /// <param name="idKonfigGjenerues">id e konfigurimit nga gjenerohet dokumenti</param>
        /// <param name="idNivelGjenerues">id e nivelit nga gjenerohet dokumenti</param>
        /// <param name="adresa">adresa</param>
        /// <param name="dtaprovimi">date aprovimi</param>
        /// <param name="llojdok"> lloj dokumenti</param>
        /// <param name="nrdokngjitur">nr dok ngjitur</param>
        /// <returns> nje objekt clsMesazh qe tregon nese ruajtja eshte kryer ne rregull apo jo.</returns>
        /// </summary>
        internal clsMesazh ruajKokaUrdherPagese(out int idKoka, int idNiv, int idKonf, string nrkupon, string nrpunonjesve, DateTime dtDk, string nrDk, string emriperfituesit, decimal totali, string nipti,
            int iddoknga, string emribankes, int idSt, int idNder, int idNdVt, int idPer, DateTime dtdokngjitur, string nrllogbankare, string adresa, int llojdok, string nrdokngjitur, DateTime dtaprovimi, int idNivelGjenerues, int idKonfigGjenerues, int idGjenerues, int idRaportDesign, string urdheruesi, string kontabilisti, string nenpunesThesari)
        {
            idKoka = -1;

            dbManager.Open();
            dbManager.CreateParameters(29);
            dbManager.AddParameters(0, "@IDKOKA", idKoka, ParameterDirection.Output);
            dbManager.AddParameters(1, "@IDNIVEL", idNiv, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDKONFIGAMBJENTE", idKonf, ParameterDirection.Input);
            dbManager.AddParameters(3, "@ADRESA", adresa, ParameterDirection.Input);
            dbManager.AddParameters(4, "@NRKUPONI", nrkupon, ParameterDirection.Input);
            dbManager.AddParameters(5, "@NRDOKNG", nrdokngjitur, ParameterDirection.Input);
            dbManager.AddParameters(6, "@NRPUNONJESVE", nrpunonjesve, ParameterDirection.Input);
            dbManager.AddParameters(7, "@NRDOK", nrDk, ParameterDirection.Input);
            dbManager.AddParameters(8, "@DTDOK", dtDk, ParameterDirection.Input);
            dbManager.AddParameters(9, "@EMRIPERFITUESIT", emriperfituesit, ParameterDirection.Input);
            dbManager.AddParameters(10, "@TOTALI", totali, ParameterDirection.Input);
            dbManager.AddParameters(11, "@NIPTI", nipti, ParameterDirection.Input);
            if (iddoknga == 0) dbManager.AddParameters(12, "@IDDOKNGA", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(12, "@IDDOKNGA", iddoknga, ParameterDirection.Input);
            dbManager.AddParameters(13, "@EMRIBANKES", emribankes, ParameterDirection.Input);
            dbManager.AddParameters(14, "@IDSTATUSDOK", idSt, ParameterDirection.Input);
            dbManager.AddParameters(15, "@IDNDERMARJE", idNder, ParameterDirection.Input);
            dbManager.AddParameters(16, "@IDNDERVITI", idNdVt, ParameterDirection.Input);
            dbManager.AddParameters(17, "@IDPERDORUESI", idPer, ParameterDirection.Input);
            dbManager.AddParameters(18, "@DTDOKNG", dtdokngjitur, ParameterDirection.Input);
            dbManager.AddParameters(19, "@NRLLOGBANKARE", nrllogbankare, ParameterDirection.Input);
            dbManager.AddParameters(20, "@LLOJDOKNG", llojdok, ParameterDirection.Input);
            dbManager.AddParameters(21, "@DTDOKAPROVIMI", dtaprovimi, ParameterDirection.Input);
            if (idNivelGjenerues == 0) dbManager.AddParameters(22, "@IDNIVELGJENERUES", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(22, "@IDNIVELGJENERUES", idNivelGjenerues, ParameterDirection.Input);
            if (idKonfigGjenerues == 0) dbManager.AddParameters(23, "@IDKONFIGGJENERUES", DBNull.Value, ParameterDirection.Input);

            else dbManager.AddParameters(23, "@IDKONFIGGJENERUES", idKonfigGjenerues, ParameterDirection.Input);
            if (idGjenerues == 0) dbManager.AddParameters(24, "@IDGJENERUES", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(24, "@IDGJENERUES", idGjenerues, ParameterDirection.Input);
            if (idRaportDesign == 0)
                dbManager.AddParameters(25, "@IDRAPORTDESING", DBNull.Value, ParameterDirection.Input);
            else
                dbManager.AddParameters(25, "@IDRAPORTDESING", idRaportDesign, ParameterDirection.Input);
            dbManager.AddParameters(26, "@URDHERUESI", urdheruesi, ParameterDirection.Input);
            dbManager.AddParameters(27, "@KONTABILISTI", kontabilisti, ParameterDirection.Input);
            dbManager.AddParameters(28, "@NENPUNESTHESARI", nenpunesThesari, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_KOKAURDHERPAGESA_ins");

            idKoka = int.Parse(dbManager.Parameters[0].Value.ToString());

            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
            return mesazh;

        }

        /// <summary>
        /// ekzekuton prc_T_KOKAURDHERPAGESE_upd ne DB dhe kthen statusin e perfundimit te ekzekutimit te SP-se perkatese
        /// Shiko clsMesazh brenda ketij moduli per informacion te metejshem mbi vleren e kthimit te kesaj metode
        /// <param name="dtDk"> data e dokumentit </param>
        /// <param name="dtdokngjitur"> data e dokumentit ngjitur </param>
        /// <param name="nipti"> nipti</param>
        /// <param name="nrkupon"> nr kuponi</param>
        /// <param name="idKonf"> id e konfigurimit te dokumentit</param>
        /// <param name="iddoknga">id e dokumentit nga e cila gjenerohet ne rastet e modifikimit </param>
        /// <param name="nrpunonjesve">nr punonjesve</param>
        /// <param name="idKoka">id ritese e kokes se magazines</param>
        /// <param name="idNder"> id e ndermarjes</param>
        /// <param name="idNdVt"> id e ndermarje vitit</param>
        /// <param name="idNiv"> id e nivelit te regjistrimit</param>
        /// <param name="idPer"> id e perdoruesit se e ben regjistrimin</param>
        /// <param name="emriperfituesit"> emri perfituesit</param>
        /// <param name="idSt"> id e statusit te dokumentit</param>
        /// <param name="nrDk">nr i dokumentit</param>
        /// <param name="totali"> totali</param>
        /// <param name="nrllogbankare"> nr llog bankare </param>
        /// <param name="emribankes"> emer banke</param>
        /// <param name="idGjenerues">id e dokumentit nga gjenerohet magazina nga nje ambjent tjeter</param>
        /// <param name="idKonfigGjenerues">id e konfigurimit nga gjenerohet dokumenti</param>
        /// <param name="idNivelGjenerues">id e nivelit nga gjenerohet dokumenti</param>
        /// <param name="adresa">adresa</param>
        /// <param name="dtaprovimi">date aprovimi</param>
        /// <param name="llojdok"> lloj dokumenti</param>
        /// <param name="nrdokngjitur">nr dok ngjitur</param>
        /// <returns> nje objekt clsMesazh qe tregon nese modifikimi eshte kryer ne rregull apo jo.</returns>
        /// </summary>
        internal clsMesazh modifikoKokaUrdherPagese(int idKoka, int idNiv, int idKonf, string nrkupon, string nrpunonjesve, DateTime dtDk, string nrDk, string emriperfituesit, decimal totali, string nipti,
            int iddoknga, string emribankes, int idSt, int idNder, int idNdVt, int idPer, DateTime dtdokngjitur, string nrllogbankare, string adresa, int llojdok, string nrdokngjitur, DateTime dtaprovimi, int idNivelGjenerues, int idKonfigGjenerues, int idGjenerues, int idRaportDesign, string urdheruesi, string kontabilisti, string nenpunesThesari)
        {

            dbManager.Open();
            dbManager.CreateParameters(29);
            dbManager.AddParameters(0, "@IDKOKA", idKoka, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNIVEL", idNiv, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDKONFIGAMBJENTE", idKonf, ParameterDirection.Input);
            dbManager.AddParameters(3, "@ADRESA", adresa, ParameterDirection.Input);
            dbManager.AddParameters(4, "@NRKUPONI", nrkupon, ParameterDirection.Input);
            dbManager.AddParameters(5, "@NRDOKNG", nrdokngjitur, ParameterDirection.Input);
            dbManager.AddParameters(6, "@NRPUNONJESVE", nrpunonjesve, ParameterDirection.Input);
            dbManager.AddParameters(7, "@NRDOK", nrDk, ParameterDirection.Input);
            dbManager.AddParameters(8, "@DTDOK", dtDk, ParameterDirection.Input);
            dbManager.AddParameters(9, "@EMRIPERFITUESIT", emriperfituesit, ParameterDirection.Input);
            dbManager.AddParameters(10, "@TOTALI", totali, ParameterDirection.Input);
            dbManager.AddParameters(11, "@NIPTI", nipti, ParameterDirection.Input);
            if (iddoknga == 0) dbManager.AddParameters(12, "@IDDOKNGA", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(12, "@IDDOKNGA", iddoknga, ParameterDirection.Input);
            dbManager.AddParameters(13, "@EMRIBANKES", emribankes, ParameterDirection.Input);
            dbManager.AddParameters(14, "@IDSTATUSDOK", idSt, ParameterDirection.Input);
            dbManager.AddParameters(15, "@IDNDERMARJE", idNder, ParameterDirection.Input);
            dbManager.AddParameters(16, "@IDNDERVITI", idNdVt, ParameterDirection.Input);
            dbManager.AddParameters(17, "@IDPERDORUESI", idPer, ParameterDirection.Input);
            dbManager.AddParameters(18, "@DTDOKNG", dtdokngjitur, ParameterDirection.Input);
            dbManager.AddParameters(19, "@NRLLOGBANKARE", nrllogbankare, ParameterDirection.Input);
            dbManager.AddParameters(20, "@LLOJDOKNG", llojdok, ParameterDirection.Input);
            dbManager.AddParameters(21, "@DTDOKAPROVIMI", dtaprovimi, ParameterDirection.Input);
            if (idNivelGjenerues == 0) dbManager.AddParameters(22, "@IDNIVELGJENERUES", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(22, "@IDNIVELGJENERUES", idNivelGjenerues, ParameterDirection.Input);
            if (idKonfigGjenerues == 0) dbManager.AddParameters(23, "@IDKONFIGGJENERUES", DBNull.Value, ParameterDirection.Input);

            else dbManager.AddParameters(23, "@IDKONFIGGJENERUES", idKonfigGjenerues, ParameterDirection.Input);
            if (idGjenerues == 0) dbManager.AddParameters(24, "@IDGJENERUES", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(24, "@IDGJENERUES", idGjenerues, ParameterDirection.Input);
            if (idRaportDesign == 0)
                dbManager.AddParameters(25, "@IDRAPORTDESING", DBNull.Value, ParameterDirection.Input);
            else
                dbManager.AddParameters(25, "@IDRAPORTDESING", idRaportDesign, ParameterDirection.Input);
            dbManager.AddParameters(26, "@URDHERUESI", urdheruesi, ParameterDirection.Input);
            dbManager.AddParameters(27, "@KONTABILISTI", kontabilisti, ParameterDirection.Input);
            dbManager.AddParameters(28, "@NENPUNESTHESARI", nenpunesThesari, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_KOKAURDHERPAGESa_upd");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgModifikimiMeSukses"]);
            return mesazh;

        }

        /// <summary>
        /// ekzekutohet sp-ja prc_T_KOKAURDHERPAGESE_del duke i kaluar id e kokes se dokumentit qe e marrim nga objekti 
        /// </summary>
        /// <param name="idkoka">id ritese e kokes </param>
        /// <returns>nje objekt clsMesazh qe tregon nese fshirja eshte kryer ne rregull apo jo</returns>
        internal clsMesazh fshiKokaUrdherPagese(int idkoka)
        {

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDKOKA", idkoka, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_KOKAURDHERPAGESa_del");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
            return mesazh;

        }

        /// <summary>
        /// kthen objekt koka dokumenti  sipas idse
        /// </summary>
        ///<param name="idkoka"> koka e dokumentit </param>
        ///<returns>nje objekt datarow qe permban nje koleksion me te gjitha kokat e dokumentit  me kete id  </returns>
        internal DataRow ktheKokaUrdherPageseSipasID(int idkoka)
        {

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDKOKA", idkoka, ParameterDirection.Input);
            using (DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KOKAURDHERPAGESA_sel"))
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
        internal DataRow ktheKokaUrdherPageseSipasIdNivelNrDokDtDok(int idNivel, string nrdok, DateTime dtdok)
        {


            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@IDNNIVEL", idNivel, ParameterDirection.Input);
            dbManager.AddParameters(1, "@NRDOK", nrdok, ParameterDirection.Input);
            dbManager.AddParameters(2, "@DTDOK", dtdok, ParameterDirection.Input);

            using (DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KOKAURDHERPAGESA_selSipasIdNivelNrDokDtDok"))
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
        internal DataTable ktheGjitheKokaUrdherPagese(int idNdermVit)
        {

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDNDERVITi", idNdermVit, ParameterDirection.Input);
            using (DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KOKAURDHERPAGESA_selAllNderViti"))
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
        internal bool ekzistonRegjistrimUrdherPagese(string nrDok, DateTime dtdok, int idNdermarrje)
        {

            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@NRDOK", nrDok, ParameterDirection.Input);
            dbManager.AddParameters(1, "@DTDOK", dtdok, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDNDERMARJE", idNdermarrje, ParameterDirection.Input);
            using (DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KOKAURDHERPAGESA_ekzistonRegjistrim"))
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

        /// <summary>
        /// merr gjithe koka listpagesa dt sipas idnderviti
        /// </summary>
        /// <param name="idNdermVit">id nderm viti</param>
        /// <returns> data table me keto list pagesa</returns>
        internal DataTable merrKokaUrdherPageseDT(int idNdermVit, int idperdoruesi)
        {//metoda per te marre te gjithe  kokat e fleteve kontabel te pa kontabilizuara

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDNDERVITI", idNdermVit, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUES", idperdoruesi, ParameterDirection.Input);
            using (DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KOKAURDHERPAGESA_merrKokaUrdherPageseDT"))
            {
                return ds.Tables[0];
            }


        }
        #endregion


        #region BRM

        internal clsMesazh ruajArketimPerTransferimNeBRM(string llojPagese, string msisdn, string emerKlienti, string nrLlogarie, string nrSerial, DateTime dtArketimi, string Perdoruesi, string shenimet, string muajFature, string kodFature, double vlerePagesePerFature, double totaliPageses, int idVeprimeBankaKoka)
        {
            dbManager.Open();
            dbManager.CreateParameters(13);
            dbManager.AddParameters(0, "@LLOJ_PAGESE", llojPagese, ParameterDirection.Input);
            dbManager.AddParameters(1, "@MSISDN", msisdn, ParameterDirection.Input);
            dbManager.AddParameters(2, "@EMER_KLIENTI", emerKlienti, ParameterDirection.Input);
            dbManager.AddParameters(3, "@NR_LLOGARIE", nrLlogarie, ParameterDirection.Input);
            dbManager.AddParameters(4, "@NR_SERIAL", nrSerial, ParameterDirection.Input);
            dbManager.AddParameters(5, "@DATE_ARKETIMI", dtArketimi, ParameterDirection.Input);
            dbManager.AddParameters(6, "@PERDORUESI", Perdoruesi, ParameterDirection.Input);
            dbManager.AddParameters(7, "@SHENIMET", shenimet, ParameterDirection.Input);
            dbManager.AddParameters(8, "@MUAJ_FATURE", muajFature, ParameterDirection.Input);
            dbManager.AddParameters(9, "@KOD_FATURE", kodFature, ParameterDirection.Input);
            dbManager.AddParameters(10, "@VLERE_PAGESE_PER_FATURE", vlerePagesePerFature, ParameterDirection.Input);
            dbManager.AddParameters(11, "@TOTALI_PAGESES", totaliPageses, ParameterDirection.Input);
            dbManager.AddParameters(12, "@IDVEPRIMEBANKAKOKA", idVeprimeBankaKoka, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_TEMP_ARKETIMET_BRM_ins");
            return new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);

        }
        internal clsMesazh ShtoRreshtTeRiPerAnullim(int idKoka, bool perAnullim)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDKOKA", idKoka, ParameterDirection.Input);
            dbManager.AddParameters(1, "@PERANULLIM", perAnullim, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_TEMP_ARKETIMET_BRM_insPerAnullim");
            return new clsMesazh(true);
        }


        internal bool updateArketimDerguarGabim(string id, string statusSTR, string errorDescr)
        {
            //GTODO skripti
            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@ID", id, ParameterDirection.Input);
            dbManager.AddParameters(1, "@STATUSSTR", statusSTR, ParameterDirection.Input);
            dbManager.AddParameters(2, "@ERRORDESCR", errorDescr, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_TEMP_ARKETIMET_BRM_UPD_DERGUAR_error");
            return true;
        }
        internal bool updateArketimDerguarInProgress(string id)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@ID", id, ParameterDirection.Input);
            return Convert.ToBoolean(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_TEMP_ARKETIMET_BRM_UPD_INPROGRESS").ToString()); // kthen true ose false nese e gjen me statustr 2 qe dmth in progress
        }
        internal DataTable merrTempArketimePerBRMCon(bool anullim)
        {
            //GTODO merr faturat per anullim ose per dergim ne varesi te parametrit ndrysho sp
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@ANULLIM", anullim, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_TEMP_ARKETIMET_BRM_sel");
            return ds.Tables[0];

        }

        internal bool updateArketimDerguarCon(string id, string transID, bool anullim, string statusSTR, string errorDescr)
        {

            //GTODO ndrysho skriptin ne db te marri dy params
            dbManager.Open();
            dbManager.CreateParameters(5);
            dbManager.AddParameters(0, "@ID", id, ParameterDirection.Input);
            dbManager.AddParameters(1, "@TRANSID", transID, ParameterDirection.Input);
            dbManager.AddParameters(2, "@ANULLIM", anullim, ParameterDirection.Input);
            dbManager.AddParameters(3, "@STATUSSTR", statusSTR, ParameterDirection.Input);
            dbManager.AddParameters(4, "@ERRORDESCR", errorDescr, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_TEMP_ARKETIMET_BRM_UPD_DERGUAR");
            return true;
        }
        #endregion


    }
}
