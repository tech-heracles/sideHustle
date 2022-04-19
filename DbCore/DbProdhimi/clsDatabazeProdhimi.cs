using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DbCore.IMBUtils.DataBase;

namespace DbCore.DbProdhimi
{
    /// <summary>
    ///  Kjo eshte klasa me e rendesishme e ketij moduli. Eshte nje klase e tille per secilin modul te projektit.
    ///  Ketu ndodhen metodat qe therriten  nga te gjithe objektet brenda projektit DbProdhimi
    ///  secila metode permban thirjet e Store procedures ne DB, duke i kaluar parametrat perkates
    ///  Metodat jane te ndara ne Regions sipas objekteve qe i therrasin keto metoda
    /// </summary>
    public class clsDatabazeProdhimi : DbData
    {
        public clsDatabazeProdhimi():base()
        {
        }



        public clsDatabazeProdhimi(DbData db) : base(db) { }
        public clsDatabazeProdhimi(string connectionName) : base(connectionName)
        {

        }
        /// <summary>
        /// Metodat e meposhtme i perkasin veprimeve qe kryhen nga objektet clsBurime dhe colBurimet
        /// </summary>
        #region BURIME

        /// <summary>
        /// ekzekuton 	[prc_T_BURIME_ins] ne DB dhe kthen statusin e perfundimit te ekzekutimit te SP-se perkatese
        /// Shiko clsMesazh brenda ketij moduli per informacion te metejshem mbi vleren e kthimit te kesaj metode
        /// </summary>
        /// <param name="idburim"> id e burimit</param>
        /// <param name="kodi">kodi</param>
        /// <param name="pershkrimi">pershkrimi</param>
        /// <param name="kostoplan">kostoja e planifikuar</param>
        /// <param name="idllog">idllog</param>
        /// <param name="aktiv"> aktive</param>
        /// <param name="tipi">tipi makineri, mjete,punonjes</param>
        /// <param name="idkonfig">id e konfigurimit</param>
        /// <param name="idPerdoruesi">id e perdoruesit</param>
        /// <param name="idnderm">id e ndermarjes</param>
        /// <param name="idstatusdok">id e statusdok</param>
        /// <returns>nje objekt clsMesazh qe tregon nese ruajtja eshte kryer ne rregull apo jo.</returns>
        /// <seealso cref="TipBurimi.cs"/>
        internal clsMesazh ruajBurim(out int idburim, string kodi, string pershkrimi, int tipi, decimal kostoplan, int idllog, bool aktiv, int idkonfig, int idPerdoruesi, int idnderm, int idstatusdok)
        {
            idburim = -1;
                dbManager.Open();
                dbManager.CreateParameters(11);
                dbManager.AddParameters(0, "@IDBURIMI", idburim, ParameterDirection.Output);
                dbManager.AddParameters(1, "@KODI", kodi, ParameterDirection.Input);
                dbManager.AddParameters(2, "@EMERTIMI", pershkrimi, ParameterDirection.Input);
                dbManager.AddParameters(3, "@TIPI", tipi, ParameterDirection.Input);
                dbManager.AddParameters(4, "@KOSTOPLAN", kostoplan, ParameterDirection.Input);
                dbManager.AddParameters(5, "@AKTIV", aktiv, ParameterDirection.Input);
                if (idllog == 0 || idllog == -1) dbManager.AddParameters(6, "@IDLLOGARI", DBNull.Value, ParameterDirection.Input);
                else dbManager.AddParameters(6, "@IDLLOGARI", idllog, ParameterDirection.Input);
                dbManager.AddParameters(7, "@IDKONFIG", idkonfig, ParameterDirection.Input);
                dbManager.AddParameters(8, "@IDPERDORUESI", idPerdoruesi, ParameterDirection.Input);
                dbManager.AddParameters(9, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
                dbManager.AddParameters(10, "@IDSTATUSDOK", idstatusdok, ParameterDirection.Input);
                dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_BURIME_ins");
                idburim = int.Parse(dbManager.Parameters[0].Value.ToString());
                return new clsMesazh(true, mesazhRuajtje);
        }

        /// <summary>
        /// ekzekuton prc_T_BURIME_upd ne DB dhe kthen statusin e perfundimit te ekzekutimit te SP-se perkatese
        /// Shiko clsMesazh brenda ketij moduli per informacion te metejshem mbi vleren e kthimit te kesaj metode
        /// </summary>
        /// <param name="idburim"> id e burimit</param>
        /// <param name="kodi">kodi</param>
        /// <param name="pershkrimi">pershkrimi</param>
        /// <param name="kostoplan">kostoja e planifikuar</param>
        /// <param name="idllog">idllog</param>
        /// <param name="aktiv"> aktive</param>
        /// <param name="tipi">tipi makineri, mjete,punonjes</param>
        /// <param name="idkonfig">id e konfigurimit</param>
        /// <param name="idPerdoruesi">id e perdoruesit</param>
        /// <param name="idnderm">id e ndermarjes</param>
        /// <param name="idstatusdok">id e statusdok</param>
        /// <returns> nje objekt clsMesazh qe tregon nese modifikimi eshte kryer ne rregull apo jo.</returns>
        internal clsMesazh modifikoBurim(int idburim, string kodi, string pershkrimi, int tipi, decimal kostoplan, int idllog, bool aktiv, int idkonfig, int idPerdoruesi, int idnderm, int idstatusdok)
        {
                dbManager.Open();
                dbManager.CreateParameters(11);
                dbManager.AddParameters(0, "@IDBURIMI", idburim, ParameterDirection.Input);
                dbManager.AddParameters(1, "@KODI", kodi, ParameterDirection.Input);
                dbManager.AddParameters(2, "@EMERTIMI", pershkrimi, ParameterDirection.Input);
                dbManager.AddParameters(3, "@TIPI", tipi, ParameterDirection.Input);
                dbManager.AddParameters(4, "@KOSTOPLAN", kostoplan, ParameterDirection.Input);
                dbManager.AddParameters(5, "@AKTIV", aktiv, ParameterDirection.Input);
                if (idllog == 0 || idllog == -1) dbManager.AddParameters(6, "@IDLLOGARI", DBNull.Value, ParameterDirection.Input);
                else dbManager.AddParameters(6, "@IDLLOGARI", idllog, ParameterDirection.Input);
                dbManager.AddParameters(7, "@IDKONFIG", idkonfig, ParameterDirection.Input);
                dbManager.AddParameters(8, "@IDPERDORUESI", idPerdoruesi, ParameterDirection.Input);
                dbManager.AddParameters(9, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
                dbManager.AddParameters(10, "@IDSTATUSDOK", idstatusdok, ParameterDirection.Input);
                dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_BURIME_upd");
                return new clsMesazh(true, mesazhModifikimi);

        }

        /// <summary>
        /// ekzekutohet sp-ja prc_T_BURIME_del duke i kaluar id e burimit
        /// </summary>
        /// <param name="idburim">id e burimit qe do fshihet </param>
        /// <returns>nje objekt clsMesazh qe tregon nese fshirja eshte kryer ne rregull apo jo</returns>
        internal clsMesazh fshiBurim(int idburim)
        {
                dbManager.Open();
                dbManager.CreateParameters(1);
                dbManager.AddParameters(0, "@IDBURIMI", idburim, ParameterDirection.Input);
                dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_BURIME_del");
                clsMesazh mesazh = new clsMesazh(true, mesazhFshirje);
                return mesazh;
        }

        /// <summary>
        /// fshin burimin duke ndryshuar statusin e saj ne te fshire
        /// </summary>
        /// <param name="idburim">id e burimit</param>
        /// <param name="idperdoruesi"> id e perdoruesit qe ka kryer veprimin</param>
        /// <returns> clsMesazh qe tregon nese fshirja u krye me sukses apo jo</returns>
        internal clsMesazh fshiBurimStatus(int idburim, int idperdoruesi)
        {
                dbManager.Open();
                dbManager.CreateParameters(2);
                dbManager.AddParameters(0, "@IDBURIMI", idburim, ParameterDirection.Input);
                dbManager.AddParameters(1, "@IDPERDORUESI", idperdoruesi, ParameterDirection.Input);
                dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_BURIME_upddel");
                clsMesazh mesazh = new clsMesazh(true, mesazhFshirje);
                return mesazh;
        }

        /// <summary>
        /// merr burimin sipas id
        /// </summary>
        /// <param name="idburim">  id e burimit</param>
        /// <returns> kthen datarow qe permban burimin me kete id</returns>
        internal DataRow ktheBurim(int idburim)
        {
                dbManager.Open();
                dbManager.CreateParameters(1);
                dbManager.AddParameters(0, "@IDBURIMI", idburim, ParameterDirection.Input);
                using (DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_BURIME_merrBurimSipasId"))
                {
                    if (ds == null)
                        return null;
                    if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                        return null;
                    return ds.Tables[0].Rows[0];
                }
        }

        /// <summary>
        /// merr gjithe burimet te nje ndermarje
        /// </summary>
        /// <param name="idnder"> id e ndermarjes</param>
        /// <returns> nje datatable qe permban nje koleksion me te gjithe burimet te ndermarjes</returns>
        internal DataTable ktheGjitheBurimetSipasNdermarjes(int idnder)
        {
                dbManager.Open();
                dbManager.CreateParameters(1);
                dbManager.AddParameters(0, "@IDNDERMARJE", idnder, ParameterDirection.Input);
                using (DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_BURIME_merrGjitheBurime"))
                {
                    return ds.Tables[0];
                }
        }

        /// <summary>
        /// merr gjithe burimet te nje ndermarje  aktiv
        /// </summary>
        /// <param name="idnder"> id e ndermarjes</param>
        /// <returns> nje datatable qe permban nje koleksion me te gjithe burimet te ndermarjes</returns>
        internal DataTable ktheGjitheBurimetSipasNdermarjesAktiv(int idnder)
        {
                dbManager.Open();
                dbManager.CreateParameters(1);
                dbManager.AddParameters(0, "@IDNDERMARJE", idnder, ParameterDirection.Input);
                using (DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_BURIME_merrGjitheBurimeAktive"))
                {
                    return ds.Tables[0];
                }
        }

        /// <summary>
        /// merr burimet te nje ndermarje me kete kod
        /// </summary>
        /// <param name="kodi"> kodi i burimit</param>
        /// <param name="idnder"> id e ndermarjes</param>
        /// <returns>  data row me burimin te nje ndermarje me kete kod</returns>
        internal DataRow ktheBurimSipasKodit(string kodi, int idnder)
        {
                dbManager.Open();
                dbManager.CreateParameters(2);
                dbManager.AddParameters(0, "@KODI", kodi, ParameterDirection.Input);
                dbManager.AddParameters(1, "@IDNDERMARJE", idnder, ParameterDirection.Input);
                using (DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_BURIME_ktheBurim"))
                {
                    if (ds == null)
                        return null;
                    if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                        return null;
                    return ds.Tables[0].Rows[0];
                }
        }

        /// <summary>
        /// perdoret per te kontrolluar nqs ekziston nje burim me kete kod 
        /// behet kontrolli per qellime konsistence te informacionit ne DB, nuk lejon te celen burime te ndryshem me te njejtin kod
        /// </summary>
        /// <param name="kodi">kodi i burimit</param>
        /// <param name="idNdermarje"> id e ndermarjes</param>
        /// <returns>nje objeckt boolean qe tregon nese ekziston apo jo nje burim me kete kod</returns>
        public bool ekzistonBurim(string kodi, int idNdermarje)
        {
                dbManager.Open();
                dbManager.CreateParameters(2);
                dbManager.AddParameters(0, "@KODI", kodi, ParameterDirection.Input);
                dbManager.AddParameters(1, "@IDNDERMARJE", idNdermarje, ParameterDirection.Input);
                using (DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_BURIME_ekzistonKod"))
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
        /// merr  burimin sipas id ne forme data row
        /// </summary>
        /// <param name="idburim"> id burim</param>
        /// <returns> kthen data row me kete burim</returns>
        internal DataRow merrBurimSipasIdDR(int idburim)
        {
                dbManager.Open();
                dbManager.CreateParameters(1);
                dbManager.AddParameters(0, "@IDBURIMI", idburim, ParameterDirection.Input);
                using (DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_BURIME_merrSipasIdDR"))
                {
                    if (ds == null)
                        return null;
                    if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                        return null;
                    return ds.Tables[0].Rows[0];
                }
        }

        /// <summary>
        /// merr gjithe burimet sipas ndermarjes ne forme data table
        /// </summary>
        /// <param name="idnderm">id ndermarje</param>
        /// <returns> kthen nje datatable me te gjithe burimet e kesaj ndermarje</returns>
        internal DataTable merrBurimDT(int idnderm)
        {
                dbManager.Open();
                dbManager.CreateParameters(1);
                dbManager.AddParameters(0, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
                using (DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_BURIME_merrSipaSNdermarrjesDT"))
                {
                    return ds.Tables[0];
                }
        }

        /// <summary>
        /// merr gjithe burimet sipas ndermarjes aktive ne forme data table
        /// </summary>
        /// <param name="idnderm">id ndermarje</param>
        /// <returns> kthen nje datatable me te gjithe burimet aktive e kesaj ndermarje</returns>
        internal DataTable merrBurimDTAktive(int idnderm)
        {
                dbManager.Open();
                dbManager.CreateParameters(1);
                dbManager.AddParameters(0, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
                using (DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_BURIME_merrSipasNdermarjePerLupe"))
                {
                    return ds.Tables[0];
                }
        }
        internal DataTable ktheGjitheBurimetSipasNdermarjesLikeDt(int idnder, string kodi)
        {
                dbManager.Open();
                dbManager.CreateParameters(2);
                dbManager.AddParameters(0, "@IDNDERMARJE", idnder, ParameterDirection.Input);
                dbManager.AddParameters(1, "@KODI", kodi, ParameterDirection.Input);
                using (DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "[prc_T_BURIME_merrSipaSNdermarrjesDTLike]"))
                {
                    return ds.Tables[0];
                }
        }

        #endregion

        /// <summary>
        /// Metodat e meposhtme i perkasin veprimeve qe kryhen nga objektet clsAktiviteteKoka dhe colAktiviteteKoka
        /// </summary>
        #region AKTIVITETE KOKA

        /// <summary>
        /// ekzekuton 	[prc_T_AKTIVITETEKOKA_ins] ne DB dhe kthen statusin e perfundimit te ekzekutimit te SP-se perkatese
        /// Shiko clsMesazh brenda ketij moduli per informacion te metejshem mbi vleren e kthimit te kesaj metode
        /// </summary>
        /// <param name="idkoka"> id e kokes</param>
        /// <param name="kodi">kodi</param>
        /// <param name="emertimi">emertimi</param>
        /// <param name="pershkrimi">pershkrimi</param>
        /// <param name="kohaplan">koha e planifikuar</param>
        /// <param name="njesikohe">njesi kohe sek,min,ore,dite</param>
        /// <param name="idkonfig">id e konfigurimit</param>
        /// <param name="idPerdoruesi">id e perdoruesit</param>
        /// <param name="idnderm">id e ndermarjes</param>
        /// <param name="idstatusdok">id e statusdok</param>
        /// <returns>nje objekt clsMesazh qe tregon nese ruajtja eshte kryer ne rregull apo jo.</returns>
        /// <seealso cref="NjeisKohe.cs"/>
        internal clsMesazh ruajAktiviteteKoka(out int idkoka, string kodi, string emertimi, string pershkrimi, int njesikohe, decimal kohaplan, int idkonfig, int idPerdoruesi, int idnderm, int idstatusdok)
        {
            idkoka = -1;
                dbManager.Open();
                dbManager.CreateParameters(10);
                dbManager.AddParameters(0, "@IDKOKA", idkoka, ParameterDirection.Output);
                dbManager.AddParameters(1, "@KODI", kodi, ParameterDirection.Input);
                dbManager.AddParameters(2, "@EMERTIMI", emertimi, ParameterDirection.Input);
                dbManager.AddParameters(3, "@PERSHKRIMI", pershkrimi, ParameterDirection.Input);
                dbManager.AddParameters(4, "@NJESIKOHE", njesikohe, ParameterDirection.Input);
                dbManager.AddParameters(5, "@KOHAPLAN", kohaplan, ParameterDirection.Input);
                dbManager.AddParameters(6, "@IDKONFIG", idkonfig, ParameterDirection.Input);
                dbManager.AddParameters(7, "@IDPERDORUESI", idPerdoruesi, ParameterDirection.Input);
                dbManager.AddParameters(8, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
                dbManager.AddParameters(9, "@IDSTATUSDOK", idstatusdok, ParameterDirection.Input);
                dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_AKTIVITETEKOKA_ins");
                idkoka = int.Parse(dbManager.Parameters[0].Value.ToString());
                return new clsMesazh(true, mesazhRuajtje);
        }

        /// <summary>
        /// ekzekuton prc_T_AKTIVITETEKOKA_upd ne DB dhe kthen statusin e perfundimit te ekzekutimit te SP-se perkatese
        /// Shiko clsMesazh brenda ketij moduli per informacion te metejshem mbi vleren e kthimit te kesaj metode
        /// </summary>
        /// <param name="idkoka"> id e kokes</param>
        /// <param name="kodi">kodi</param>
        /// <param name="emertimi">emertimi</param>
        /// <param name="pershkrimi">pershkrimi</param>
        /// <param name="kohaplan">koha e planifikuar</param>
        /// <param name="njesikohe">njesi kohe sek,min,ore,dite</param>
        /// <param name="idkonfig">id e konfigurimit</param>
        /// <param name="idPerdoruesi">id e perdoruesit</param>
        /// <param name="idnderm">id e ndermarjes</param>
        /// <param name="idstatusdok">id e statusdok</param>
        /// <returns> nje objekt clsMesazh qe tregon nese modifikimi eshte kryer ne rregull apo jo.</returns>
        internal clsMesazh modifikoAktiviteteKoka(int idkoka, string kodi, string emertimi, string pershkrimi, int njesikohe, decimal kohaplan, int idkonfig, int idPerdoruesi, int idnderm, int idstatusdok)
        {

                dbManager.Open();
                dbManager.CreateParameters(10);
                dbManager.AddParameters(0, "@IDKOKA", idkoka, ParameterDirection.Input);
                dbManager.AddParameters(1, "@KODI", kodi, ParameterDirection.Input);
                dbManager.AddParameters(2, "@EMERTIMI", emertimi, ParameterDirection.Input);
                dbManager.AddParameters(3, "@PERSHKRIMI", pershkrimi, ParameterDirection.Input);
                dbManager.AddParameters(4, "@NJESIKOHE", njesikohe, ParameterDirection.Input);
                dbManager.AddParameters(5, "@KOHAPLAN", kohaplan, ParameterDirection.Input);
                dbManager.AddParameters(6, "@IDKONFIG", idkonfig, ParameterDirection.Input);
                dbManager.AddParameters(7, "@IDPERDORUESI", idPerdoruesi, ParameterDirection.Input);
                dbManager.AddParameters(8, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
                dbManager.AddParameters(9, "@IDSTATUSDOK", idstatusdok, ParameterDirection.Input);
                dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_AKTIVITETEKOKA_upd");
                return new clsMesazh(true, mesazhModifikimi);
        }

        /// <summary>
        /// ekzekutohet sp-ja prc_T_AKTIVITETEKOKA_del duke i kaluar id e kokes
        /// </summary>
        /// <param name="idkoka">id e kokes qe do fshihet </param>
        /// <returns>nje objekt clsMesazh qe tregon nese fshirja eshte kryer ne rregull apo jo</returns>
        internal clsMesazh fshiAktiviteteKoka(int idkoka)
        {
                dbManager.Open();
                dbManager.CreateParameters(1);
                dbManager.AddParameters(0, "@IDKOKA", idkoka, ParameterDirection.Input);
                dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_AKTIVITETEKOKA_del");
                clsMesazh mesazh = new clsMesazh(true, mesazhFshirje);
                return mesazh;
        }

        /// <summary>
        /// fshin aktivitet  duke ndryshuar statusin e saj ne te fshire
        /// </summary>
        /// <param name="idkoka">id e kokes</param>
        /// <param name="idperdoruesi"> id e perdoruesit qe ka kryer veprimin</param>
        /// <returns> clsMesazh qe tregon nese fshirja u krye me sukses apo jo</returns>
        internal clsMesazh fshiAktiviteteKokaStatus(int idkoka, int idperdoruesi)
        {
                dbManager.Open();
                dbManager.CreateParameters(2);
                dbManager.AddParameters(0, "@IDKOKA", idkoka, ParameterDirection.Input);
                dbManager.AddParameters(1, "@IDPERDORUESI", idperdoruesi, ParameterDirection.Input);
                dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_AKTIVITETEKOKA_upddel");
                clsMesazh mesazh = new clsMesazh(true, mesazhFshirje);
                return mesazh;
        }

        /// <summary>
        /// merr aktivitetin sipas id
        /// </summary>
        /// <param name="idkoka">  id e kokes</param>
        /// <returns> kthen datarow qe permban aktivitet koka me kete id</returns>
        internal DataRow ktheAkivitetKoka(int idkoka)
        {
                dbManager.Open();
                dbManager.CreateParameters(1);
                dbManager.AddParameters(0, "@IDKOKA", idkoka, ParameterDirection.Input);
                using (DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_AKTIVITETEKOKA_merrAktivitetSipasId"))
                {
                    if (ds == null)
                        return null;
                    if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                        return null;
                    return ds.Tables[0].Rows[0];
                }
        }

        /// <summary>
        /// merr gjithe aktivitetet te nje ndermarje
        /// </summary>
        /// <param name="idnder"> id e ndermarjes</param>
        /// <returns> nje datatable qe permban nje koleksion me te gjithe aktivitetet te ndermarjes</returns>
        internal DataTable ktheGjitheAktivitetetSipasNdermarjes(int idnder)
        {
                dbManager.Open();
                dbManager.CreateParameters(1);
                dbManager.AddParameters(0, "@IDNDERMARJE", idnder, ParameterDirection.Input);
                using (DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_AKTIVITETEKOKA_merrGjitheAktivitetet"))
                {
                    return ds.Tables[0];
                }
        }

        /// <summary>
        /// merr gjithe aktivitetet te nje ndermarje like
        /// </summary>
        /// <param name="idnder"> id e ndermarjes</param>
        /// <param name="kodi">kodi </param>
        /// <returns> nje datatable qe permban nje koleksion me te gjithe aktivitetet te ndermarjes qe permbajne kete kod</returns>
        internal DataTable ktheGjitheAktivitetetSipasNdermarjesLike(int idnder, string kodi)
        {
                dbManager.Open();
                dbManager.CreateParameters(2);
                dbManager.AddParameters(0, "@IDNDERMARJE", idnder, ParameterDirection.Input);
                dbManager.AddParameters(1, "@KODI", kodi, ParameterDirection.Input);
                using (DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_AKTIVITETEKOKA_merrGjitheAktivitetetLike"))
                {
                    return ds.Tables[0];
                }
        }
        internal DataTable ktheGjitheAktivitetetSipasNdermarjesLikeDt(int idnder, string kodi)
        {
                dbManager.Open();
                dbManager.CreateParameters(2);
                dbManager.AddParameters(0, "@IDNDERMARJE", idnder, ParameterDirection.Input);
                dbManager.AddParameters(1, "@KODI", kodi, ParameterDirection.Input);
                using (DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_AKTIVITETEKOKA_merrSipasNdermarrjesDTLike"))
                {
                    return ds.Tables[0];
                }
        }
        internal DataTable ktheGjitheAktivitetetSipasNdermarjesDheBurimiLikeDt(int idnder, string kodi, int idburimi)
        {
                dbManager.Open();
                dbManager.CreateParameters(3);
                dbManager.AddParameters(0, "@IDNDERMARJE", idnder, ParameterDirection.Input);
                dbManager.AddParameters(1, "@KODI", kodi, ParameterDirection.Input);
                dbManager.AddParameters(2, "@idburimi", idburimi, ParameterDirection.Input);
                using (DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_AKTIVITETEKOKA_merrSipasNdermarrjesDheBurimitDTLike"))
                {
                    return ds.Tables[0];
                }
        }
        /// <summary>
        /// merr aktivitetin te nje ndermarje me kete kod
        /// </summary>
        /// <param name="kodi"> kodi i aktivitetit</param>
        /// <param name="idnder"> id e ndermarjes</param>
        /// <returns>  data row me aktivitetin te nje ndermarje me kete kod</returns>
        internal DataRow ktheAktivitetinSipasKodit(string kodi, int idnder)
        {
                dbManager.Open();
                dbManager.CreateParameters(2);
                dbManager.AddParameters(0, "@KODI", kodi, ParameterDirection.Input);
                dbManager.AddParameters(1, "@IDNDERMARJE", idnder, ParameterDirection.Input);
                using (DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_AKTIVITETEKOKA_ktheAktivitet"))
                {
                    if (ds == null)
                        return null;
                    if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                        return null;
                    return ds.Tables[0].Rows[0];
                }
        }

        /// <summary>
        /// perdoret per te kontrolluar nqs ekziston nje aktivitet me kete kod 
        /// behet kontrolli per qellime konsistence te informacionit ne DB, nuk lejon te celen aktivitete te ndryshem me te njejtin kod
        /// </summary>
        /// <param name="kodi">kodi i aktivitetit</param>
        /// <param name="idNdermarje"> id e ndermarjes</param>
        /// <returns>nje objeckt boolean qe tregon nese ekziston apo jo nje aktivitet me kete kod</returns>
        public bool ekzistonAktivitet(string kodi, int idNdermarje)
        {
                dbManager.Open();
                dbManager.CreateParameters(2);
                dbManager.AddParameters(0, "@KODI", kodi, ParameterDirection.Input);
                dbManager.AddParameters(1, "@IDNDERMARJE", idNdermarje, ParameterDirection.Input);
                using (DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_AKTIVITETEKOKA_ekzistonKod"))
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
        /// merr  aktivitet sipas id ne forme data row
        /// </summary>
        /// <param name="idaktivitet"> id e aktivitetit</param>
        /// <returns> kthen data row me kete aktivitet</returns>
        internal DataRow merrAktivitetSipasIdDR(int idaktivitet)
        {
                dbManager.Open();
                dbManager.CreateParameters(1);
                dbManager.AddParameters(0, "@IDKOKA", idaktivitet, ParameterDirection.Input);
                using (DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_AKTIVITETEKOKA_merrSipasIdDR"))
                {
                    if (ds == null)
                        return null;
                    if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                        return null;
                    return ds.Tables[0].Rows[0];
                }
        }

        /// <summary>
        /// merr gjithe aktivitetet sipas ndermarjes ne forme data table
        /// </summary>
        /// <param name="idnderm">id ndermarje</param>
        /// <returns> kthen nje datatable me te gjithe aktivitet e kesaj ndermarje</returns>
        internal DataTable merrAktiviteteDT(int idnderm)
        {
                dbManager.Open();
                dbManager.CreateParameters(1);
                dbManager.AddParameters(0, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
                using (DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_AKTIVITETEKOKA_merrSipasNdermarrjesDT"))
                {
                    return ds.Tables[0];
                }
        }
        internal DataTable merrAktiviteteSipasBurimitDT(int idnderm, int idburimi)
        {
                dbManager.Open();
                dbManager.CreateParameters(2);
                dbManager.AddParameters(0, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
                dbManager.AddParameters(1, "@idburimi", idburimi, ParameterDirection.Input);
                using (DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_AKTIVITETEKOKA_merrSipasNdermarrjesSipasBurimitDT"))
                {
                    return ds.Tables[0];
                }
        }
        #endregion

        /// <summary>
        /// Metodat e meposhtme i perkasin veprimeve qe kryhen nga objektet clsAktiviteteTrupi dhe colAktiviteteTrupi
        /// </summary>
        #region AKTIVITETE TRUPI

        /// <summary>
        /// ekzekuton 	[prc_T_AKTIVITETETRUPI_ins] ne DB dhe kthen statusin e perfundimit te ekzekutimit te SP-se perkatese
        /// Shiko clsMesazh brenda ketij moduli per informacion te metejshem mbi vleren e kthimit te kesaj metode
        /// </summary>
        /// <param name="idtrupi"> id e trupit</param>
        /// <param name="idkoka">id e kokes</param>
        /// <param name="idburimi">id e burimit</param>
        /// <param name="koha">koha</param>
        /// <returns>nje objekt clsMesazh qe tregon nese ruajtja eshte kryer ne rregull apo jo.</returns>
        /// <param name="dtNdryshimi">data e ndryshimit</param>
        internal clsMesazh ruajAktiviteteTrupi(out int idtrupi, int idkoka, int idburimi, decimal koha, DateTime dtNdryshimi)
        {
            idtrupi = -1;
                dbManager.Open();
                dbManager.CreateParameters(5);
                dbManager.AddParameters(0, "@IDTRUPI", idtrupi, ParameterDirection.Output);
                dbManager.AddParameters(1, "@IDKOKA", idkoka, ParameterDirection.Input);
                dbManager.AddParameters(2, "@IDBURIMI", idburimi, ParameterDirection.Input);
                dbManager.AddParameters(3, "@KOHA", koha, ParameterDirection.Input);
                dbManager.AddParameters(4, "@DTNDRYSHIMI", dtNdryshimi, ParameterDirection.Input);
                dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_AKTIVITETETRUPI_ins");
                idtrupi = int.Parse(dbManager.Parameters[0].Value.ToString());
                return new clsMesazh(true, mesazhRuajtje);
        }

        /// <summary>
        /// ekzekuton prc_T_AKTIVITETETRUPI_upd ne DB dhe kthen statusin e perfundimit te ekzekutimit te SP-se perkatese
        /// Shiko clsMesazh brenda ketij moduli per informacion te metejshem mbi vleren e kthimit te kesaj metode
        /// </summary>
        /// <param name="idtrupi"> id e trupit</param>
        /// <param name="idkoka">id e kokes</param>
        /// <param name="idburimi">id e burimit</param>
        /// <param name="koha">koha</param>
        /// <param name="dtndryshimi">data e ndryshimit</param>
        /// <returns> nje objekt clsMesazh qe tregon nese modifikimi eshte kryer ne rregull apo jo.</returns>
        internal clsMesazh modifikoAktiviteteTrupi(int idtrupi, int idkoka, int idburimi, decimal koha, DateTime dtndryshimi)
        {
                dbManager.Open();
                dbManager.CreateParameters(5);
                dbManager.AddParameters(0, "@IDTRUPI", idtrupi, ParameterDirection.Input);
                dbManager.AddParameters(1, "@IDKOKA", idkoka, ParameterDirection.Input);
                dbManager.AddParameters(2, "@IDBURIMI", idburimi, ParameterDirection.Input);
                dbManager.AddParameters(3, "@KOHA", koha, ParameterDirection.Input);
                dbManager.AddParameters(4, "@DTNDRYSHIMI", dtndryshimi, ParameterDirection.Input);
                dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_AKTIVITETETRUPI_upd");
                return new clsMesazh(true, mesazhModifikimi);
        }

        /// <summary>
        /// ekzekutohet sp-ja prc_T_AKTIVITETETRUPI_del duke i kaluar id e trupit
        /// </summary>
        /// <param name="idTrupit">id e trupit qe do fshihet </param>
        /// <returns>nje objekt clsMesazh qe tregon nese fshirja eshte kryer ne rregull apo jo</returns>
        internal clsMesazh fshiAktiviteteTrupi(int idTrupit)
        {

                dbManager.Open();
                dbManager.CreateParameters(1);
                dbManager.AddParameters(0, "@IDTRUPI", idTrupit, ParameterDirection.Input);
                dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_AKTIVITETETRUPI_del");
                clsMesazh mesazh = new clsMesazh(true, mesazhFshirje);
                return mesazh;
        }

        /// <summary>
        /// ekzekutohet sp-ja prc_T_AKTIVITETETRUPI_delSipasIdKoka duke i kaluar id e trupit
        /// </summary>
        /// <param name="idkoka">id e kokes qe do fshihet </param>
        /// <returns>nje objekt clsMesazh qe tregon nese fshirja eshte kryer ne rregull apo jo</returns>
        internal clsMesazh fshiAktiviteteTrupiSipasIdKoka(int idKoka)
        {

                dbManager.Open();
                dbManager.CreateParameters(1);
                dbManager.AddParameters(0, "@IDKOKA", idKoka, ParameterDirection.Input);
                dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_AKTIVITETETRUPI_delSipasIdKoka");
                clsMesazh mesazh = new clsMesazh(true, mesazhFshirje);
                return mesazh;
        }

        /// <summary>
        /// merr aktivitete trupi sipas id trupi
        /// </summary>
        /// <param name="idtrupi">  id e trupit</param>
        /// <returns> kthen datarow qe permban aktivitet trupi me kete id</returns>
        internal DataRow ktheAktivitetTrupi(int idtrupi)
        {

                dbManager.Open();
                dbManager.CreateParameters(1);
                dbManager.AddParameters(0, "@IDTRUPI", idtrupi, ParameterDirection.Input);
                using (DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_AKTIVITETETRUPI_sel"))
                {
                    if (ds == null)
                        return null;
                    if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                        return null;
                    return ds.Tables[0].Rows[0];
                }

        }

        /// <summary>
        /// merr aktivitete trupi sipas idkokes
        /// </summary>
        /// <param name="idkoka"> id e kokes</param>
        /// <returns> nje datatable qe permban nje koleksion me te gjithe trupin e kokes</returns>
        internal DataTable ktheAktiviteteTrupiSipasIdKoka(int idkoka)
        {

                dbManager.Open();
                dbManager.CreateParameters(1);
                dbManager.AddParameters(0, "@IDKOKA", idkoka, ParameterDirection.Input);
                using (DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_AKTIVITETETRUPI_merrSipasIdKoke"))
                {
                    return ds.Tables[0];
                }

        }
        /// <summary>
        /// merr aktivitete trupi sipas idkokes dhe dt se ndryshimit
        /// </summary>
        /// <param name="idkoka"> id e kokes</param>
        /// <param name="dtNdryshimi">data e ndryshimit</param>
        /// <returns> nje datatable qe permban nje koleksion me te gjithe trupin e kokes</returns>
        internal DataTable ktheAktiviteteTrupiSipasIdKokaDheDtNdryshimi(int idkoka, DateTime dtNdryshimi)
        {

                dbManager.Open();
                dbManager.CreateParameters(2);
                dbManager.AddParameters(0, "@IDKOKA", idkoka, ParameterDirection.Input);
                dbManager.AddParameters(1, "@DTNDRYSHIMI", dtNdryshimi, ParameterDirection.Input);
                using (DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_AKTIVITETETRUPI_merrSipasIdKokeDheDtNdryshimi"))
                {
                    return ds.Tables[0];
                }

        }

        /// <summary>
        /// merr gjithe datat e ndryshimit te ketij aktiviiteti
        /// </summary>
        /// <param name="idkoka">id e kokes</param>
        /// <returns> kthen nje datatable me te gjithe datat e ndryshimeve te aktiviteteve</returns>
        internal DataTable merrDataNdryshimiAktiviteti(int idkoka)
        {

                dbManager.Open();
                dbManager.CreateParameters(1);
                dbManager.AddParameters(0, "@IDKOKA", idkoka, ParameterDirection.Input);
                using (DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_AKTIVITETETRUPI_merrGjitheDatatSipasIdKoka"))
                {
                    return ds.Tables[0];
                }

        }

        #endregion

        /// <summary>
        /// Metodat e meposhtme i perkasin veprimeve qe kryhen nga objektet clsKokaPlanifikim dhe colKokaPlanifikim
        /// </summary>
        #region KOKA PLANIFIKIM

        /// <summary>
        /// ekzekuton prc_T_KOKAPLANIFIKIM_ins ne DB dhe kthen statusin e perfundimit te ekzekutimit te SP-se perkatese
        /// Shiko clsMesazh brenda ketij moduli per informacion te metejshem mbi vleren e kthimit te kesaj metode
        /// </summary>
        /// <param name="dtDk"> data e dokumentit te planifikimit</param>
        /// <param name="dtRegj"> data e regjistrimit te dokumentit te planifikimit</param>
        /// <param name="idKlFurn"> id e klient furnitorit me te cilin lidhet</param>
        /// <param name="idKonf"> id e konfigurimit te dokumentit</param>
        /// <param name="iddoknga">id e dokumentit nga e cila gjenerohet ne rastet e modifikimit </param>
        /// <param name="idMag">id e magazines</param>
        /// <param name="idkokaPlanifikim">id ritese e kokes se planifikimit</param>
        /// <param name="idNder"> id e ndermarjes</param>
        /// <param name="idNdVt"> id e ndermarje vitit</param>
        /// <param name="idNiv"> id e nivelit te regjistrimit</param>
        /// <param name="idPer"> id e perdoruesit se e ben regjistrimin</param>
        /// <param name="idSt"> id e statusit te dokumentit</param>
        /// <param name="nrDk">nr i dokumentit</param>
        /// <param name="shenim"> shenime </param>
        /// <param name="idGjenerues">id e dokumentit nga gjenerohet planifikimi nga nje ambjent tjeter</param>
        /// <param name="idKonfigGjenerues">id e konfigurimit nga gjenerohet dokumenti</param>
        /// <param name="idNivelGjenerues">id e nivelit nga gjenerohet dokumenti</param>
        /// <returns> nje objekt clsMesazh qe tregon nese ruajtja eshte kryer ne rregull apo jo.</returns>
        internal clsMesazh ruajKokaPlanifikim(out int idkokaPlanifikim, int idNiv, int idKonf, int idKlFurn, int idMag, DateTime dtDk, string nrDk, int iddoknga, int idSt, int idNder, int idNdVt, int idPer, DateTime dtRegj, string shenim, int idNivelGjenerues, int idKonfigGjenerues, int idGjenerues, int idgrup1, int idgrup2, int idgrup3, DateTime afatkohor, int idRaportDesign, int idNjesiProdhimi)
        {
            idkokaPlanifikim = 0;


            dbManager.Open();
            dbManager.CreateParameters(23);
            dbManager.AddParameters(0, "@IDKOKAPLANIFIKIM", idkokaPlanifikim, ParameterDirection.Output);
            dbManager.AddParameters(1, "@IDNIVEL", idNiv, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDKONFIGAMBJENTE", idKonf, ParameterDirection.Input);
            if (idKlFurn == 0) dbManager.AddParameters(3, "@IDKLIENTi", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(3, "@IDKLIENTi", idKlFurn, ParameterDirection.Input);
            if (idMag == 0) dbManager.AddParameters(4, "@IDMAG", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(4, "@IDMAG", idMag, ParameterDirection.Input);
            dbManager.AddParameters(5, "@NRDOK", nrDk, ParameterDirection.Input);
            dbManager.AddParameters(6, "@DTDOK", dtDk, ParameterDirection.Input);
            if (iddoknga == 0) dbManager.AddParameters(7, "@IDDOKNGA", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(7, "@IDDOKNGA", iddoknga, ParameterDirection.Input);
            dbManager.AddParameters(8, "@IDSTATUSDOK", idSt, ParameterDirection.Input);
            dbManager.AddParameters(9, "@IDNDERMARJE", idNder, ParameterDirection.Input);
            dbManager.AddParameters(10, "@IDNDERVITI", idNdVt, ParameterDirection.Input);
            dbManager.AddParameters(11, "@IDPERDORUESI", idPer, ParameterDirection.Input);
            dbManager.AddParameters(12, "@DTREGJ", dtRegj, ParameterDirection.Input);
            dbManager.AddParameters(13, "@SHENIME", shenim, ParameterDirection.Input);
            if (idNivelGjenerues == 0) dbManager.AddParameters(14, "@IDNIVELGJENERUES", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(14, "@IDNIVELGJENERUES", idNivelGjenerues, ParameterDirection.Input);
            if (idKonfigGjenerues == 0) dbManager.AddParameters(15, "@IDKONFIGGJENERUES", DBNull.Value, ParameterDirection.Input);

            else dbManager.AddParameters(15, "@IDKONFIGGJENERUES", idKonfigGjenerues, ParameterDirection.Input);
            if (idGjenerues == 0) dbManager.AddParameters(16, "@IDGJENERUES", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(16, "@IDGJENERUES", idGjenerues, ParameterDirection.Input);
            if (idgrup1 == 0) dbManager.AddParameters(17, "@IDGRUP1", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(17, "@IDGRUP1", idgrup1, ParameterDirection.Input);
            if (idgrup2 == 0) dbManager.AddParameters(18, "@IDGRUP2", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(18, "@IDGRUP2", idgrup2, ParameterDirection.Input);
            if (idgrup3 == 0) dbManager.AddParameters(19, "@IDGRUP3", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(19, "@IDGRUP3", idgrup3, ParameterDirection.Input);
            dbManager.AddParameters(20, "@AFATIKOHOR", afatkohor, ParameterDirection.Input);
            if (idRaportDesign == 0)
                dbManager.AddParameters(21, "@IDRAPORTDESING", DBNull.Value, ParameterDirection.Input);
            else
                dbManager.AddParameters(21, "@IDRAPORTDESING", idRaportDesign, ParameterDirection.Input);
            if(idNjesiProdhimi <=0)
                dbManager.AddParameters(22, "@IDNJESIPRODHIMI", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(22, "@IDNJESIPRODHIMI", idNjesiProdhimi, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_KOKAPLANIFIKIM_ins");

            idkokaPlanifikim = int.Parse(dbManager.Parameters[0].Value.ToString());

            clsMesazh mesazh = new clsMesazh(true, mesazhRuajtje);
            return mesazh;

        }

        /// <summary>
        /// ekzekuton prc_T_KOKAPLANIFIKIM_upd ne DB dhe kthen statusin e perfundimit te ekzekutimit te SP-se perkatese
        /// Shiko clsMesazh brenda ketij moduli per informacion te metejshem mbi vleren e kthimit te kesaj metode 
        /// </summary>
        /// <param name="dtDk"> data e dokumentit te planifikimit</param>
        /// <param name="dtRegj"> data e regjistrimit te dokumentit te planifikimit</param>
        /// <param name="idKlFurn"> id e klient furnitorit me te cilin lidhet</param>
        /// <param name="idKonf"> id e konfigurimit te dokumentit</param>
        /// <param name="iddoknga">id e dokumentit nga e cila gjenerohet ne rastet e modifikimit </param>
        /// <param name="idMag">id e magazines</param>
        /// <param name="idkokaPlanifikim">id ritese e kokes se planifikimit</param>
        /// <param name="idNder"> id e ndermarjes</param>
        /// <param name="idNdVt"> id e ndermarje vitit</param>
        /// <param name="idNiv"> id e nivelit te regjistrimit</param>
        /// <param name="idPer"> id e perdoruesit se e ben regjistrimin</param>
        /// <param name="idSt"> id e statusit te dokumentit</param>
        /// <param name="nrDk">nr i dokumentit</param>
        /// <param name="shenim"> shenime </param>
        /// <param name="idGjenerues">id e dokumentit nga gjenerohet planifikimi nga nje ambjent tjeter</param>
        /// <param name="idKonfigGjenerues">id e konfigurimit nga gjenerohet dokumenti</param>
        /// <param name="idNivelGjenerues">id e nivelit nga gjenerohet dokumenti</param>
        /// <returns> nje objekt clsMesazh qe tregon nese modifikimi eshte kryer ne rregull apo jo.</returns>
        internal clsMesazh modifikoKokaPlanifikim(int idkokaPlanifikim, int idNiv, int idKonf, int idKlFurn, int idMag, DateTime dtDk, string nrDk, int iddoknga, int idSt, int idNder, int idNdVt, int idPer, DateTime dtRegj, string shenim, int idNivelGjenerues, int idKonfigGjenerues, int idGjenerues, int idgrup1, int idgrup2, int idgrup3, DateTime afatkohor, int idRaportDesign, int idNjesiProdhimi)
        {
            dbManager.Open();
            dbManager.CreateParameters(22);
            dbManager.AddParameters(0, "@IDKOKAPLANIFIKIM", idkokaPlanifikim, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNIVEL", idNiv, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDKONFIGAMBJENTE", idKonf, ParameterDirection.Input);
            if (idKlFurn == 0) dbManager.AddParameters(3, "@IDKLIENTi", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(3, "@IDKLIENTi", idKlFurn, ParameterDirection.Input);
            if (idMag == 0) dbManager.AddParameters(4, "@IDMAG", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(4, "@IDMAG", idMag, ParameterDirection.Input);
            dbManager.AddParameters(5, "@NRDOK", nrDk, ParameterDirection.Input);
            dbManager.AddParameters(6, "@DTDOK", dtDk, ParameterDirection.Input);
            if (iddoknga == 0) dbManager.AddParameters(7, "@IDDOKNGA", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(7, "@IDDOKNGA", iddoknga, ParameterDirection.Input);
            dbManager.AddParameters(8, "@IDSTATUSDOK", idSt, ParameterDirection.Input);
            dbManager.AddParameters(9, "@IDNDERMARJE", idNder, ParameterDirection.Input);
            dbManager.AddParameters(10, "@IDNDERVITI", idNdVt, ParameterDirection.Input);
            dbManager.AddParameters(11, "@DTREGJ", dtRegj, ParameterDirection.Input);
            dbManager.AddParameters(12, "@SHENIME", shenim, ParameterDirection.Input);
            if (idNivelGjenerues == 0) dbManager.AddParameters(13, "@IDNIVELGJENERUES", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(13, "@IDNIVELGJENERUES", idNivelGjenerues, ParameterDirection.Input);
            if (idKonfigGjenerues == 0) dbManager.AddParameters(14, "@IDKONFIGGJENERUES", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(14, "@IDKONFIGGJENERUES", idKonfigGjenerues, ParameterDirection.Input);
            if (idGjenerues == 0) dbManager.AddParameters(15, "@IDGJENERUES", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(15, "@IDGJENERUES", idGjenerues, ParameterDirection.Input);
            if (idgrup1 == 0) dbManager.AddParameters(16, "@IDGRUP1", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(16, "@IDGRUP1", idgrup1, ParameterDirection.Input);
            if (idgrup2 == 0) dbManager.AddParameters(17, "@IDGRUP2", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(17, "@IDGRUP2", idgrup2, ParameterDirection.Input);
            if (idgrup3 == 0) dbManager.AddParameters(18, "@IDGRUP3", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(18, "@IDGRUP3", idgrup3, ParameterDirection.Input);
            dbManager.AddParameters(19, "@AFATIKOHOR", afatkohor, ParameterDirection.Input);
            if (idRaportDesign == 0)
                dbManager.AddParameters(20, "@IDRAPORTDESING", DBNull.Value, ParameterDirection.Input);
            else
                dbManager.AddParameters(20, "@IDRAPORTDESING", idRaportDesign, ParameterDirection.Input);
            if (idNjesiProdhimi <= 0)
                dbManager.AddParameters(21, "@IDNJESIPRODHIMI", DBNull.Value, ParameterDirection.Input);
            else
                dbManager.AddParameters(21, "@IDNJESIPRODHIMI", idNjesiProdhimi, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_KOKAPLANIFIKIM_upd");
            clsMesazh mesazh = new clsMesazh(true, mesazhModifikimi);
            return mesazh;
        }

        /// <summary>
        /// ekzekutohet sp-ja prc_T_KOKAPLANIFIKIM_upddel duke i kaluar id e kokes se dokumentit qe e marrim nga objekti clsKokaPlanifikim qe i kalohet si parameter
        /// </summary>
        /// <param name="idkokaplanifikim">id ritese e kokes se planifikimit</param>
        /// <param name="idPer">merret id e perdoruesit qe ben fshirjen</param>
        /// <returns>nje objekt clsMesazh qe tregon nese fshirja eshte kryer ne rregull apo jo</returns>
        internal clsMesazh fshiKokaPlanifikim(int idkokaplanifikim, int idPer)
        {

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDKOKAPLANIFIKIM", idkokaplanifikim, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUESI", idPer, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_KOKAPLANIFIKIM_upddel");
            clsMesazh mesazh = new clsMesazh(true, mesazhFshirje);
            return mesazh;

        }

        internal clsMesazh kaloNeHistorikKokaPlanifikim(int idkoka)
        {

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDKOKA", idkoka, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_KOKAPLANIFIKIM_hidhNeHistorik");
            clsMesazh mesazh = new clsMesazh(true, mesazhFshirje);
            return mesazh;

        }


        /// <summary>
        /// kthen objekt koka dokumenti planifikim sipas idse
        /// </summary>
        ///<param name="idkokaplanifikim"> koka e dokumentit te planifikimit</param>
        ///<returns>nje objekt datarow qe permban nje koleksion me te gjitha kokat e dokumentit te planifikimit me kete id  </returns>
        internal DataRow ktheKokaPlanifikimSipasID(int idkokaplanifikim)
        {

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDKOKAPLANIFIKIM", idkokaplanifikim, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KOKAPLANIFIKIM_sel");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];

        }

        /// <summary>
        /// kthen datarow koka dokumenti planifikim sipas idse dokumenti nga qe eshte gjeneruar nga modifikimi
        /// </summary>
        ///<param name="idDokNga"> koka e dokumentit nga te planifikimit</param>
        ///<returns>nje objekt datarow qe permban nje koleksion me te gjitha kokat e dokumentit te planifikimit me kete id dok nga </returns>
        internal DataRow ktheKokaPlanifikimitSipasIDDokNga(int idDokNga)
        {

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDDOKNGA", idDokNga, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KOKAPLANIFIKIM_merrSipasIdDokNga");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];

        }

        /// <summary>
        /// kthen datarow koka dokumenti planifikimit sipas nivelit te dokumentit, nr te dokumentit dhe dates se dokumentit
        /// </summary>
        ///<param name="idNivel"> id e nivelit</param>
        ///<param name="nrdok"> nr i dokumentit te gjenerues</param>
        ///<param name="dtdok"> data e dokumentit</param>
        ///<returns>nje objekt datarow qe permban nje koleksion me te gjitha kokat e dokumentit te planifikimit me kete nivel dokumenti, nr dokumenti dhe date dokumenti  </returns>
        internal DataRow ktheKokaPlanifikimSipasIdNivelNrDokDtDok(int idNivel, string nrdok, DateTime dtdok)
        {

            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@IDNNIVEL", idNivel, ParameterDirection.Input);
            dbManager.AddParameters(1, "@NRDOK", nrdok, ParameterDirection.Input);
            dbManager.AddParameters(2, "@DTDOK", dtdok, ParameterDirection.Input);

            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KOKAPLANIFIKIM_selSipasIdNivelNrDokDtDok");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];

        }

        /// <summary>
        /// kthen datatable koka dokumenti planifikim sipas ndermarje vitit 
        /// </summary>
        ///<param name="idNdermVit">id e ndermarje vitit</param>
        ///<returns>nje objekt datatable qe permban nje koleksion me te gjitha kokat e dokumentit te planifikimit  te kesaj ndermarje viti  </returns>
        internal DataTable ktheGjitheKokaPlanifikim(int idNdermVit)
        {

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDNDERMVIT", idNdermVit, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KOKAPLANIFIKIM_selAllNdermVit");
            return ds.Tables[0];

        }

        /// <summary>
        /// kthen datatable trupi dokumenti te planifikimit sipas kokes
        /// </summary>
        ///<param name="idkokaplanifikim">koka e dokumentit te planifikimit</param>
        ///<returns>nje objekt datatable qe permban nje koleksion me te gjitha trupat e dokumentit te planifikimit te kesaj koke  </returns>
        internal DataTable ktheTrupiPlanifikim(int idkokaplanifikim, int idndermarje)
        {

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDKOKAPLANIFIKIM", idkokaplanifikim, ParameterDirection.Input);
            dbManager.AddParameters(1, "@idndermarje", idndermarje, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_TRUPIPLANIFIKIM_selAllSipasKoka");
            return ds.Tables[0];

        }
        /// <summary>
        /// kthen datatable trupi dokumenti te planifikimit sipas kokes
        /// </summary>
        ///<param name="idkokaplanifikim">koka e dokumentit te planifikimit</param>
        ///<returns>nje objekt datatable qe permban nje koleksion me te gjitha trupat e dokumentit te planifikimit te kesaj koke  </returns>
        internal DataTable ktheTrupiPlanifikimSipasAutorizimit(int idkokaplanifikim, int idndermarje, int idperdorues)
        {

            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@IDKOKAPLANIFIKIM", idkokaplanifikim, ParameterDirection.Input);
            dbManager.AddParameters(1, "@idndermarje", idndermarje, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDPERDORUES", idperdorues, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_TRUPIPLANIFIKIM_selAllSipasKokaDheAutorizimit");
            return ds.Tables[0];

        }

        /// <summary>
        /// perdoret per te kontrolluar nqs ekziston nje dokument planifikimi me nje nr dokumenti te marre si parameter
        /// behet kontrolli per qellime konsistence te informacionit ne DB, nuk lejon te celen dokumenta  te ndryshem me te njejtin kod ne nje ndermarje
        /// </summary>
        /// <param name="idNdermarrje"> id e ndermarjes</param>
        /// <param name="dtdok"> data e dokumentit te planifikimit</param>
        /// <param name="idKonfigAmbjente"> id e konfigurimit te dokumentit</param>
        /// <param name="nrDok">nr i dokumentit</param>
        /// <returns>nje objeckt clsMesazh qe tregon nese ekziston apo jo nje dokument me kete kod</returns>
        internal bool ekzistonRegjistrimPlanifikimi(int idKonfigAmbjente, string nrDok, DateTime dtdok, int idNdermarrje)
        {

            dbManager.Open();
            dbManager.CreateParameters(4);
            dbManager.AddParameters(0, "@LLOJI", idKonfigAmbjente, ParameterDirection.Input);
            dbManager.AddParameters(1, "@NRDOK", nrDok, ParameterDirection.Input);
            dbManager.AddParameters(2, "@DTDOK", dtdok, ParameterDirection.Input);
            dbManager.AddParameters(3, "@IDNDERMARJE", idNdermarrje, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KOKAPLANIFIKIM_ekzistonRegjistrimPlanifikimi");
            if (ds.Tables[0].Rows.Count == 1)
                return true;
            else if (ds.Tables[0].Rows.Count == 0)
                return false;
            else return true;

        }

        /// <summary>
        /// merr kokat e planifikimit sipas idndervitit dhe autorizimeve ne forme datatable
        /// </summary>
        /// <param name="idNdermVit">idndermvit</param>
        /// <param name="idperdoruesi">idperdoruesi</param>
        /// <returns>data table me keto te dhena</returns>
        internal DataTable merrKokaPlanifikimDT(int idNdermVit, int idperdoruesi)
        {//metoda per te marre te gjithe  kokat e fleteve kontabel te pa kontabilizuara

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDNDERVITI", idNdermVit, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUES", idperdoruesi, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KOKAPLANIFIKIM_merrKokaPlanifikimDT");
            return ds.Tables[0];

        }

        internal bool kaAutorizimKokaPlanifikim(int idkoka, int idperdoruesi)
        {


            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDKOKA", idkoka, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUES", idperdoruesi, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KOKAPLANIFIKIM_kaAutorizim");
            if (ds.Tables[0].Rows.Count == 1)
                return true;
            else if (ds.Tables[0].Rows.Count == 0)
                return false;
            else return true;


        }
        internal string eshteEkzekutuarPlanifikimi(int idkoka, int idndermarje)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@iddok", idkoka, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERmarje", idndermarje, ParameterDirection.Input);
            string ngjyra = dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_KOKAPLANIFIKIM_eshteEkzekutuar").ToString();
            return ngjyra;
        }
        
        internal int ktheIdNjesiProdhimiSipasKokePlanifikim(int idkoka)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDKOKAPLANIFIKIM", idkoka, ParameterDirection.Input);

            int idNjesiProdhimi = Convert.ToInt32(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_KOKAPLANIFIKIM_merrNjesiProdhimiSipasIdPlanifikim"));
            return idNjesiProdhimi;
        }

        #endregion

        /// <summary>
        /// Metodat e meposhtme i perkasin veprimeve qe kryhen nga objektet clsTrupiPlanifikim dhe colTrupiPlanifikim
        /// </summary>
        #region TRUPI PLANIFIKIM

        /// <summary>
        /// ekzekuton prc_T_TRUPIPLANIFIKIM_ins ne DB dhe kthen statusin e perfundimit te ekzekutimit te SP-se perkatese
        /// Shiko clsMesazh brenda ketij moduli per informacion te metejshem mbi vleren e kthimit te kesaj metode
        /// </summary>
        /// <param name="idArt"> id e artikullit</param>
        /// <param name="idmag">id e magazines</param>
        /// <param name="idkokaplanifikim">id e kokes se dokumentit te planifikimit</param>
        /// <param name="idtrupiplanifikim">id ritese e trupit te dokumentit te planifikimit</param>
        /// <param name="idNjes">id e njesise se artikullit</param>
        /// <param name="sas"> sasia e artikullit</param>
        /// <returns> nje objekt clsMesazh qe tregon nese ruajtja eshte kryer ne rregull apo jo.</returns>
        internal clsMesazh ruajTrupiPlanifikim(out int idtrupiplanifikim, int idkokaplanifikim, int idArt, int idNjes, double sas, int idmag, double gjeresi, double gjatesi, double sasipermase, int idurdherporosi, string shenime, int detajim1, int detajim2)
        {
            idtrupiplanifikim = 0;

            dbManager.Open();
            dbManager.CreateParameters(13);
            dbManager.AddParameters(0, "@IDTRUPIPLANIFIKIM", idtrupiplanifikim, ParameterDirection.Output);
            dbManager.AddParameters(1, "@IDKOKAPLANIFIKIM", idkokaplanifikim, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDARTIKULLI", idArt, ParameterDirection.Input);
            if (idNjes == 0 || idNjes == -1) dbManager.AddParameters(3, "@IDNJESIA", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(3, "@IDNJESIA", idNjes, ParameterDirection.Input);
            dbManager.AddParameters(4, "@SASIA", sas, ParameterDirection.Input);
            if (idmag == 0 || idmag == -1) dbManager.AddParameters(5, "@IDMAG", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(5, "@IDMAG", idmag, ParameterDirection.Input);
            dbManager.AddParameters(6, "@GJATESI", gjatesi, ParameterDirection.Input);
            dbManager.AddParameters(7, "@GJERESI", gjeresi, ParameterDirection.Input);
            dbManager.AddParameters(8, "@SASIPERMASE", sasipermase, ParameterDirection.Input);
            if (idurdherporosi == 0 || idurdherporosi == -1) dbManager.AddParameters(9, "@IDURDHERPOROSI", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(9, "@IDURDHERPOROSI", idurdherporosi, ParameterDirection.Input);
            dbManager.AddParameters(10, "@SHENIME", shenime, ParameterDirection.Input);
            if (detajim1 == 0 || detajim1 == -1) dbManager.AddParameters(11, "@DETAJIM1", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(11, "@DETAJIM1", detajim1, ParameterDirection.Input);
            if (detajim2 == 0 || detajim2 == -1) dbManager.AddParameters(12, "@DETAJIM2", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(12, "@DETAJIM2", detajim2, ParameterDirection.Input);

            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_TRUPIPLANIFIKIM_ins");

            idtrupiplanifikim = int.Parse(dbManager.Parameters[0].Value.ToString());

            clsMesazh mesazh = new clsMesazh(true, mesazhRuajtje);
            return mesazh;

        }

        /// <summary>
        /// ekzekuton prc_T_TRUPIPLANIFIKIM_upd ne DB dhe kthen statusin e perfundimit te ekzekutimit te SP-se perkatese
        /// Shiko clsMesazh brenda ketij moduli per informacion te metejshem mbi vleren e kthimit te kesaj metode
        /// </summary>
        /// <param name="idArt"> id e artikullit</param>
        /// <param name="idmag">id e magazines</param>
        /// <param name="idkokaplanifikim">id e kokes se dokumentit te planifikimit</param>
        /// <param name="idtrupiplanifikim">id ritese e trupit te dokumentit te planifikimit</param>
        /// <param name="idNjes">id e njesise se artikullit</param>
        /// <param name="sas"> sasia e artikullit</param>
        /// <returns> nje objekt clsMesazh qe tregon nese modifikimi eshte kryer ne rregull apo jo.</returns>
        internal clsMesazh modifikoTrupiPlanifikim(int idtrupiplanifikim, int idkokaplanifikim, int idArt, int idNjes, double sas, int idmag, double gjeresi, double gjatesi, double sasipermase, int idurdherporosi, string shenime, int detajim1, int detajim2)
        {

            dbManager.Open();
            dbManager.CreateParameters(13);
            dbManager.AddParameters(0, "@IDTRUPIPLANIFIKIM", idtrupiplanifikim, ParameterDirection.Output);
            dbManager.AddParameters(1, "@IDKOKAPLANIFIKIM", idkokaplanifikim, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDARTIKULLI", idArt, ParameterDirection.Input);
            if (idNjes == 0 || idNjes == -1) dbManager.AddParameters(3, "@IDNJESIA", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(3, "@IDNJESIA", idNjes, ParameterDirection.Input);
            dbManager.AddParameters(4, "@SASIA", sas, ParameterDirection.Input);
            if (idmag == 0 || idmag == -1) dbManager.AddParameters(5, "@IDMAG", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(5, "@IDMAG", idmag, ParameterDirection.Input);
            dbManager.AddParameters(6, "@GJATESI", gjatesi, ParameterDirection.Input);
            dbManager.AddParameters(7, "@GJERESI", gjeresi, ParameterDirection.Input);
            dbManager.AddParameters(8, "@SASIPERMASE", sasipermase, ParameterDirection.Input);
            if (idurdherporosi == 0 || idurdherporosi == -1) dbManager.AddParameters(9, "@IDURDHERPOROSI", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(9, "@IDURDHERPOROSI", idurdherporosi, ParameterDirection.Input);
            dbManager.AddParameters(10, "@SHENIME", shenime, ParameterDirection.Input);
            if (detajim1 == 0 || detajim1 == -1) dbManager.AddParameters(11, "@DETAJIM1", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(11, "@DETAJIM1", detajim1, ParameterDirection.Input);
            if (detajim2 == 0 || detajim2 == -1) dbManager.AddParameters(12, "@DETAJIM2", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(12, "@DETAJIM2", detajim2, ParameterDirection.Input);

            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_TRUPIPLANIFIKIM_upd");
            clsMesazh mesazh = new clsMesazh(true, mesazhModifikimi);
            return mesazh;

        }
        internal clsMesazh modifikoTrupiPlanifikimIdUrdherPorosi(int idurdherporosiVjeter, int idurdherporosi)
        {

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDURDHERPOROSIVJETER", idurdherporosiVjeter, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDURDHERPOROSI", idurdherporosi, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_TRUPIPLANIFIKIM_updIdUrdherPorosi");
            clsMesazh mesazh = new clsMesazh(true, mesazhModifikimi);
            return mesazh;

        }
        /// <summary>
        /// ekzekutohet sp-ja prc_T_TRUPIPLANIFIKIM_delSipasIdKoka duke i kaluar id e kokes se dokumentit te planfikimit qe e marrim nga objekti clsTrupiPlanifikim qe i kalohet si parameter
        /// </summary>
        /// <param name="idkokaplanifikim">id e kokes se dokumentit te planifikimit</param>
        /// <returns>nje objekt clsMesazh qe tregon nese fshirja eshte kryer ne rregull apo jo</returns>
        internal clsMesazh fshiTrupiPlanifikimSipasKoka(int idkokaplanifikim)
        {
            
                dbManager.Open();
                dbManager.CreateParameters(1);
                dbManager.AddParameters(0, "@IDKOKAPLANIFIKIM", idkokaplanifikim, ParameterDirection.Input);
                dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_TRUPIPLANIFIKIM_delSipasIdKoka");
                clsMesazh mesazh = new clsMesazh(true, mesazhFshirje);
                return mesazh;
            
        }

        /// <summary>
        /// ekzekutohet sp-ja prc_T_TRUPIPLANIFIKIM_delSipasIdTrupi duke i kaluar id e trupit te dokumentit te planifikimit qe e marrim nga objekti clsTrupiPlanifikim qe i kalohet si parameter
        /// </summary>
        /// <param name="idtrupiplanifikim">id ritese e trupit te dokumentit te planifikimit</param>
        /// <returns>nje objekt clsMesazh qe tregon nese fshirja eshte kryer ne rregull apo jo</returns>
        internal clsMesazh fshiTrupiPlanifikimSipasID(int idtrupiplanifikim)
        {
            
                dbManager.Open();
                dbManager.CreateParameters(1);
                dbManager.AddParameters(0, "@IDTRUPIPLANIFIKIM", idtrupiplanifikim, ParameterDirection.Input);
                dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_TRUPIPLANIFIKIM_delSipasIdTrupi");
                clsMesazh mesazh = new clsMesazh(true, mesazhFshirje);
                return mesazh;
           
        }

        /// <summary>
        /// kthen datatable trupi dokumenti te planifikimit sipas kokes
        /// </summary>
        /// <param name="idkokaplanifikim">id e kokes se dokumentit te planifikimit</param>
        ///<returns>nje objekt datatable qe permban nje koleksion me te gjitha trupat e dokumentit te planifikimit te kesaj koke  </returns>
        internal DataTable ktheGjitheTrupiPlanifikimNgaKoka(int idkokaplanifikim, int idndermarje)
        {
           
                dbManager.Open();
                dbManager.CreateParameters(2);
                dbManager.AddParameters(0, "@IDKOKAPLANIFIKIM", idkokaplanifikim, ParameterDirection.Input);
                dbManager.AddParameters(1, "@idndermarje", idndermarje, ParameterDirection.Input);
                DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_TRUPIPLANIFIKIM_selAllSipasKoka");
                return ds.Tables[0];
            
        }

        /// <summary>
        /// kthen datarow trupi dokumenti te planifikimit  sipas idse se trupit te dokumentit
        /// </summary>
        /// <param name="idtrupiplanifikim">id ritese e trupit te dokumentit te planifikimit</param>
        ///<returns>nje objekt datarow qe permban nje koleksion me te gjitha trupat e dokumentit te planfikimit te kesaj id  </returns>
        internal DataRow ktheTrupiPlanifikimSipasID(int idtrupiplanifikim, int idndermarje)
        {
            
                dbManager.Open();
                dbManager.CreateParameters(2);
                dbManager.AddParameters(0, "@IDTRUPIPLANIFIKIM", idtrupiplanifikim, ParameterDirection.Input);
                dbManager.AddParameters(1, "@idndermarje", idndermarje, ParameterDirection.Input);
                DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_TRUPIPLANIFIKIM_sel");
                if (ds == null)
                    return null;
                if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                    return null;
                return ds.Tables[0].Rows[0];
           
        }

        #endregion

        /// <summary>
        /// Metodat e meposhtme i perkasin veprimeve qe kryhen nga objektet clsKokaSkedulimProdhimi dhe colKokaSkedulimProdhimi
        /// </summary>
        #region KOKA SKEDULIM PRODHIMI

        /// <summary>
        /// ekzekuton prc_T_KOKASKEDULIMPRODHIMI_ins ne DB dhe kthen statusin e perfundimit te ekzekutimit te SP-se perkatese
        /// Shiko clsMesazh brenda ketij moduli per informacion te metejshem mbi vleren e kthimit te kesaj metode
        /// </summary>
        /// <param name="dtDk"> data e dokumentit </param>
        /// <param name="dtRegj"> data e regjistrimit te dokumentit </param>
        /// <param name="idburimi"> id e burimit me te cilin lidhet</param>
        /// <param name="idKonf"> id e konfigurimit te dokumentit</param>
        /// <param name="iddoknga">id e dokumentit nga e cila gjenerohet ne rastet e modifikimit </param>
        /// <param name="idplanifikimi">id e planifikimit me te cilen eshte lidhur</param>
        /// <param name="idkoka">id ritese e kokes </param>
        /// <param name="idNder"> id e ndermarjes</param>
        /// <param name="idNdVt"> id e ndermarje vitit</param>
        /// <param name="idNiv"> id e nivelit te regjistrimit</param>
        /// <param name="idPer"> id e perdoruesit se e ben regjistrimin</param>
        /// <param name="idSt"> id e statusit te dokumentit</param>
        /// <param name="nrDk">nr i dokumentit</param>
        /// <param name="shenim"> shenime </param>
        /// <param name="idGjenerues">id e dokumentit nga gjenerohet  nga nje ambjent tjeter</param>
        /// <param name="idKonfigGjenerues">id e konfigurimit nga gjenerohet dokumenti</param>
        /// <param name="idNivelGjenerues">id e nivelit nga gjenerohet dokumenti</param>
        /// <returns> nje objekt clsMesazh qe tregon nese ruajtja eshte kryer ne rregull apo jo.</returns>
        internal clsMesazh ruajKokaSkedulimProdhimi(out int idkoka, int idNiv, int idKonf, int idburimi, int idplanifikimi, DateTime dtDk, string nrDk, int iddoknga, int idSt, int idNder, int idNdVt, int idPer, DateTime dtRegj, string shenim, int idNivelGjenerues, int idKonfigGjenerues, int idGjenerues, int idkrijuesi)
        {
            idkoka = -1;

                dbManager.Open();
                dbManager.CreateParameters(18);
                dbManager.AddParameters(0, "@IDKOKA", idkoka, ParameterDirection.Output);
                dbManager.AddParameters(1, "@IDNIVEL", idNiv, ParameterDirection.Input);
                dbManager.AddParameters(2, "@IDKONFIGAMBJENTE", idKonf, ParameterDirection.Input);
                if (idburimi == 0) dbManager.AddParameters(3, "@IDBURIMI", DBNull.Value, ParameterDirection.Input);
                else dbManager.AddParameters(3, "@IDBURIMI", idburimi, ParameterDirection.Input);
                if (idplanifikimi == 0) dbManager.AddParameters(4, "@IDPLANIFIKIMI", DBNull.Value, ParameterDirection.Input);
                else dbManager.AddParameters(4, "@IDPLANIFIKIMI", idplanifikimi, ParameterDirection.Input);
                dbManager.AddParameters(5, "@NRDOK", nrDk, ParameterDirection.Input);
                dbManager.AddParameters(6, "@DTDOK", dtDk, ParameterDirection.Input);
                if (iddoknga == 0) dbManager.AddParameters(7, "@IDDOKNGA", DBNull.Value, ParameterDirection.Input);
                else dbManager.AddParameters(7, "@IDDOKNGA", iddoknga, ParameterDirection.Input);
                dbManager.AddParameters(8, "@IDSTATUSDOK", idSt, ParameterDirection.Input);
                dbManager.AddParameters(9, "@IDNDERMARJE", idNder, ParameterDirection.Input);
                dbManager.AddParameters(10, "@IDNDERVITI", idNdVt, ParameterDirection.Input);
                dbManager.AddParameters(11, "@IDPERDORUESI", idPer, ParameterDirection.Input);
                dbManager.AddParameters(12, "@DTREGJ", dtRegj, ParameterDirection.Input);
                dbManager.AddParameters(13, "@SHENIME", shenim, ParameterDirection.Input);
                if (idNivelGjenerues == 0) dbManager.AddParameters(14, "@IDNIVELGJENERUES", DBNull.Value, ParameterDirection.Input);
                else dbManager.AddParameters(14, "@IDNIVELGJENERUES", idNivelGjenerues, ParameterDirection.Input);
                if (idKonfigGjenerues == 0) dbManager.AddParameters(15, "@IDKONFIGGJENERUES", DBNull.Value, ParameterDirection.Input);

                else dbManager.AddParameters(15, "@IDKONFIGGJENERUES", idKonfigGjenerues, ParameterDirection.Input);
                if (idGjenerues == 0) dbManager.AddParameters(16, "@IDGJENERUES", DBNull.Value, ParameterDirection.Input);
                else dbManager.AddParameters(16, "@IDGJENERUES", idGjenerues, ParameterDirection.Input);
                if (idkrijuesi == 0) dbManager.AddParameters(17, "@IDKRIJUESI", DBNull.Value, ParameterDirection.Input);
                else dbManager.AddParameters(17, "@IDKRIJUESI", idkrijuesi, ParameterDirection.Input);

                dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_KOKASKEDULIMPRODHIMI_ins");

                idkoka = int.Parse(dbManager.Parameters[0].Value.ToString());

                clsMesazh mesazh = new clsMesazh(true, mesazhRuajtje);
                return mesazh;
        }

        /// <summary>
        /// ekzekuton prc_T_KOKASKEDULIMPRODHIMI_upd ne DB dhe kthen statusin e perfundimit te ekzekutimit te SP-se perkatese
        /// Shiko clsMesazh brenda ketij moduli per informacion te metejshem mbi vleren e kthimit te kesaj metode 
        /// </summary>
        /// <param name="dtDk"> data e dokumentit </param>
        /// <param name="dtRegj"> data e regjistrimit te dokumentit </param>
        /// <param name="idburimi"> id e burimit me te cilin lidhet</param>
        /// <param name="idKonf"> id e konfigurimit te dokumentit</param>
        /// <param name="iddoknga">id e dokumentit nga e cila gjenerohet ne rastet e modifikimit </param>
        /// <param name="idplanifikimi">id e planifikimit</param>
        /// <param name="idkoka">id ritese e kokes </param>
        /// <param name="idNder"> id e ndermarjes</param>
        /// <param name="idNdVt"> id e ndermarje vitit</param>
        /// <param name="idNiv"> id e nivelit te regjistrimit</param>
        /// <param name="idPer"> id e perdoruesit se e ben regjistrimin</param>
        /// <param name="idSt"> id e statusit te dokumentit</param>
        /// <param name="nrDk">nr i dokumentit</param>
        /// <param name="shenim"> shenime </param>
        /// <param name="idGjenerues">id e dokumentit nga gjenerohet  nga nje ambjent tjeter</param>
        /// <param name="idKonfigGjenerues">id e konfigurimit nga gjenerohet dokumenti</param>
        /// <param name="idNivelGjenerues">id e nivelit nga gjenerohet dokumenti</param>
        /// <returns> nje objekt clsMesazh qe tregon nese modifikimi eshte kryer ne rregull apo jo.</returns>
        internal clsMesazh modifikoKokaSkedulimProdhimi(int idkoka, int idNiv, int idKonf, int idburimi, int idplanifikimi, DateTime dtDk, string nrDk, int iddoknga, int idSt, int idNder, int idNdVt, int idPer, DateTime dtRegj, string shenim, int idNivelGjenerues, int idKonfigGjenerues, int idGjenerues)
        {

                dbManager.Open();
                dbManager.CreateParameters(17);
                //  dbManager.CreateParameters(20);
                dbManager.AddParameters(0, "@IDKOKA", idkoka, ParameterDirection.Input);
                dbManager.AddParameters(1, "@IDNIVEL", idNiv, ParameterDirection.Input);
                dbManager.AddParameters(2, "@IDKONFIGAMBJENTE", idKonf, ParameterDirection.Input);
                if (idburimi == 0) dbManager.AddParameters(3, "@IDBURIMI", DBNull.Value, ParameterDirection.Input);
                else dbManager.AddParameters(3, "@IDBURIMI", idburimi, ParameterDirection.Input);
                if (idplanifikimi == 0) dbManager.AddParameters(4, "@IDPLANIFIKIMI", DBNull.Value, ParameterDirection.Input);
                else dbManager.AddParameters(4, "@IDPLANIFIKIMI", idplanifikimi, ParameterDirection.Input);
                dbManager.AddParameters(5, "@NRDOK", nrDk, ParameterDirection.Input);
                dbManager.AddParameters(6, "@DTDOK", dtDk, ParameterDirection.Input);
                if (iddoknga == 0) dbManager.AddParameters(7, "@IDDOKNGA", DBNull.Value, ParameterDirection.Input);
                else dbManager.AddParameters(7, "@IDDOKNGA", iddoknga, ParameterDirection.Input);
                dbManager.AddParameters(8, "@IDSTATUSDOK", idSt, ParameterDirection.Input);
                dbManager.AddParameters(9, "@IDNDERMARJE", idNder, ParameterDirection.Input);
                dbManager.AddParameters(10, "@IDNDERVITI", idNdVt, ParameterDirection.Input);
                dbManager.AddParameters(11, "@IDPERDORUESI", idPer, ParameterDirection.Input);
                dbManager.AddParameters(12, "@DTREGJ", dtRegj, ParameterDirection.Input);
                dbManager.AddParameters(13, "@SHENIME", shenim, ParameterDirection.Input);
                if (idNivelGjenerues == 0) dbManager.AddParameters(14, "@IDNIVELGJENERUES", DBNull.Value, ParameterDirection.Input);
                else dbManager.AddParameters(14, "@IDNIVELGJENERUES", idNivelGjenerues, ParameterDirection.Input);
                if (idKonfigGjenerues == 0) dbManager.AddParameters(15, "@IDKONFIGGJENERUES", DBNull.Value, ParameterDirection.Input);

                else dbManager.AddParameters(15, "@IDKONFIGGJENERUES", idKonfigGjenerues, ParameterDirection.Input);
                if (idGjenerues == 0) dbManager.AddParameters(16, "@IDGJENERUES", DBNull.Value, ParameterDirection.Input);
                else dbManager.AddParameters(16, "@IDGJENERUES", idGjenerues, ParameterDirection.Input);
                dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_KOKASKEDULIMPRODHIMI_upd");
                clsMesazh mesazh = new clsMesazh(true, mesazhModifikimi);
                return mesazh;
        }

        /// <summary>
        /// ekzekutohet sp-ja prc_T_KOKASKEDULIMPRODHIMI_upddel duke i kaluar id e kokes se dokumentit qe e marrim nga objekti clsKokaSkedulimProdhimi qe i kalohet si parameter
        /// </summary>
        /// <param name="idkoka">id ritese e kokes </param>
        /// <param name="idPer">merret id e perdoruesit qe ben fshirjen</param>
        /// <returns>nje objekt clsMesazh qe tregon nese fshirja eshte kryer ne rregull apo jo</returns>
        internal clsMesazh fshiKokaSkedulimProdhimi(int idkoka, int idPer)
        {

                dbManager.Open();
                dbManager.CreateParameters(2);
                dbManager.AddParameters(0, "@IDKOKA", idkoka, ParameterDirection.Input);
                dbManager.AddParameters(1, "@IDPERDORUESI", idPer, ParameterDirection.Input);
                dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_KOKASKEDULIMPRODHIMI_upddel");
                clsMesazh mesazh = new clsMesazh(true, mesazhFshirje);
                return mesazh;

        }


        /// <summary>
        /// kthen objekt koka dokumenti skedulim prodhim sipas idse
        /// </summary>
        ///<param name="idkoka"> koka e dokumentit </param>
        ///<returns>nje objekt datarow qe permban nje koleksion me te gjitha kokat e dokumentit  me kete id  </returns>
        internal DataRow ktheKokaSkedulimProdhimiSipasID(int idkoka)
        {

                dbManager.Open();
                dbManager.CreateParameters(1);
                dbManager.AddParameters(0, "@IDKOKA", idkoka, ParameterDirection.Input);
                DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KOKASKEDULIMPRODHIMI_sel");
                if (ds == null)
                    return null;
                if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                    return null;
                return ds.Tables[0].Rows[0];
        }

        /// <summary>
        /// kthen datarow koka dokumenti skedulim prodhimit sipas idse dokumenti nga qe eshte gjeneruar nga modifikimi
        /// </summary>
        ///<param name="idDokNga"> koka e dokumentit nga ka ardhur</param>
        ///<returns>nje objekt datarow qe permban nje koleksion me te gjitha kokat e dokumentit  me kete id dok nga </returns>
        internal DataRow ktheKokaSkedulimProdhimiSipasIDDokNga(int idDokNga)
        {

                dbManager.Open();
                dbManager.CreateParameters(1);
                dbManager.AddParameters(0, "@IDDOKNGA", idDokNga, ParameterDirection.Input);
                DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KOKASKEDULIMPRODHIMI_merrSipasIdDokNga");
                if (ds == null)
                    return null;
                if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                    return null;
                return ds.Tables[0].Rows[0];
        }

        /// <summary>
        /// kthen datarow koka dokumenti skedulim prodhimi sipas nivelit te dokumentit, nr te dokumentit dhe dates se dokumentit
        /// </summary>
        ///<param name="idNivel"> id e nivelit</param>
        ///<param name="nrdok"> nr i dokumentit te gjenerues</param>
        ///<param name="dtdok"> data e dokumentit</param>
        ///<returns>nje objekt datarow qe permban nje koleksion me te gjitha kokat e dokumentit te skedulim prodhimit me kete nivel dokumenti, nr dokumenti dhe date dokumenti  </returns>
        internal DataRow ktheKokaSkedulimProdhimiSipasIdNivelNrDokDtDok(int idNivel, string nrdok, DateTime dtdok)
        {

                dbManager.Open();
                dbManager.CreateParameters(3);
                dbManager.AddParameters(0, "@IDNNIVEL", idNivel, ParameterDirection.Input);
                dbManager.AddParameters(1, "@NRDOK", nrdok, ParameterDirection.Input);
                dbManager.AddParameters(2, "@DTDOK", dtdok, ParameterDirection.Input);

                DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KOKASKEDULIMPRODHIMI_selSipasIdNivelNrDokDtDok");
                if (ds == null)
                    return null;
                if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                    return null;
                return ds.Tables[0].Rows[0];
        }

        /// <summary>
        /// kthen datatable koka dokumenti skedulim prodhimi sipas ndermarje vitit 
        /// </summary>
        ///<param name="idNdermVit">id e ndermarje vitit</param>
        ///<returns>nje objekt datatable qe permban nje koleksion me te gjitha kokat e dokumentit te skedulim prodhimi  te kesaj ndermarje viti  </returns>
        internal DataTable ktheGjitheKokaSkedulimProdhimi(int idNdermVit)
        {

                dbManager.Open();
                dbManager.CreateParameters(1);
                dbManager.AddParameters(0, "@IDNDERMVIT", idNdermVit, ParameterDirection.Input);
                DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KOKASKEDULIMPRODHIMI_selAllNdermVit");
                return ds.Tables[0];

        }

        /// <summary>
        /// kthen datatable trupi dokumenti te skedulim prodhimit sipas kokes
        /// </summary>
        ///<param name="idkoka">koka e dokumentit </param>
        ///<param name="idndermarje"> id e ndermarjes</param>
        ///<returns>nje objekt datatable qe permban nje koleksion me te gjitha trupat e dokumentit te skedulim prodhimit te kesaj koke  </returns>
        internal DataTable ktheTrupiSkedulimProdhimi(int idkoka, int idndermarje)
        {

                dbManager.Open();
                dbManager.CreateParameters(2);
                dbManager.AddParameters(0, "@IDKOKA", idkoka, ParameterDirection.Input);
                dbManager.AddParameters(1, "@idndermarje", idndermarje, ParameterDirection.Input);
                DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_TRUPISKEDULIMPRODHIMI_selAllSipasKoka");
                return ds.Tables[0];

        }
        /// <summary>
        /// kthen datatable trupi dokumenti te skedulim prodhimi sipas kokes
        /// </summary>
        ///<param name="idkoka">koka e dokumentit </param>
        ///<param name="idndermarje"> id e ndermarjes</param>
        ///<param name="idperdorues">id e perdoruesit per te pare autorizimet</param>
        ///<returns>nje objekt datatable qe permban nje koleksion me te gjitha trupat e dokumentit te planifikimit te kesaj koke  </returns>
        internal DataTable ktheTrupiSkedulimProdhimiSipasAutorizimit(int idkoka, int idndermarje, int idperdorues)
        {

                dbManager.Open();
                dbManager.CreateParameters(3);
                dbManager.AddParameters(0, "@IDKOKA", idkoka, ParameterDirection.Input);
                dbManager.AddParameters(1, "@idndermarje", idndermarje, ParameterDirection.Input);
                dbManager.AddParameters(2, "@IDPERDORUES", idperdorues, ParameterDirection.Input);
                DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_TRUPISKEDULIMPRODHIMI_selAllSipasKokaDheAutorizimit");
                return ds.Tables[0];

        }

        /// <summary>
        /// perdoret per te kontrolluar nqs ekziston nje dokument skedulim prodhimi me nje nr dokumenti te marre si parameter
        /// behet kontrolli per qellime konsistence te informacionit ne DB, nuk lejon te celen dokumenta  te ndryshem me te njejtin kod ne nje ndermarje
        /// </summary>
        /// <param name="idNdermarrje"> id e ndermarjes</param>
        /// <param name="dtdok"> data e dokumentit </param>
        /// <param name="idKonfigAmbjente"> id e konfigurimit te dokumentit</param>
        /// <param name="nrDok">nr i dokumentit</param>
        /// <returns>nje objeckt clsMesazh qe tregon nese ekziston apo jo nje dokument me kete kod</returns>
        internal bool ekzistonRegjistrimSkedulimProdhimi(int idKonfigAmbjente, string nrDok, DateTime dtdok, int idNdermarrje)
        {

                dbManager.Open();
                dbManager.CreateParameters(4);
                dbManager.AddParameters(0, "@LLOJI", idKonfigAmbjente, ParameterDirection.Input);
                dbManager.AddParameters(1, "@NRDOK", nrDok, ParameterDirection.Input);
                dbManager.AddParameters(2, "@DTDOK", dtdok, ParameterDirection.Input);
                dbManager.AddParameters(3, "@IDNDERMARJE", idNdermarrje, ParameterDirection.Input);
                DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KOKASKEDULIMPRODHIMI_ekzistonRegjistrim");
                if (ds.Tables[0].Rows.Count == 1)
                    return true;
                else if (ds.Tables[0].Rows.Count == 0)
                    return false;
                else return true;

        }

        /// <summary>
        /// merr kokat e skedulim prodhimit sipas idndervitit dhe autorizimeve ne forme datatable
        /// </summary>
        /// <param name="idNdermVit">idndermvit</param>
        /// <param name="idperdoruesi">idperdoruesi</param>
        /// <returns>data table me keto te dhena</returns>
        internal DataTable merrKokaSkedulimProdhimiDT(int idNdermVit, int idperdoruesi)
        {//metoda per te marre te gjithe  kokat e fleteve kontabel te pa kontabilizuara

                dbManager.Open();
                dbManager.CreateParameters(2);
                dbManager.AddParameters(0, "@IDNDERVITI", idNdermVit, ParameterDirection.Input);
                dbManager.AddParameters(1, "@IDPERDORUES", idperdoruesi, ParameterDirection.Input);
                DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KOKASKEDULIMPRODHIMI_merrKokaSkedulimDT");
                return ds.Tables[0];

        }
        /// <summary>
        /// kontrollon nese nje perdorues i caktuar ka autorizime per ta hapur kete dokument
        /// </summary>
        /// <param name="idkoka">id e kokes se dokumentit</param>
        /// <param name="idperdoruesi">id e perdoruesit qe po kryhen veprimin</param>
        /// <returns>kthen true ose false
        /// </returns>
        internal bool kaAutorizimKokaSkedulimProdhimi(int idkoka, int idperdoruesi)
        {

                dbManager.Open();
                dbManager.CreateParameters(2);
                dbManager.AddParameters(0, "@IDKOKA", idkoka, ParameterDirection.Input);
                dbManager.AddParameters(1, "@IDPERDORUES", idperdoruesi, ParameterDirection.Input);
                DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KOKASKEDULIMPRODHIMI_kaAutorizim");
                if (ds.Tables[0].Rows.Count == 1)
                    return true;
                else if (ds.Tables[0].Rows.Count == 0)
                    return false;
                else return true;

        }
        #endregion

        /// <summary>
        /// Metodat e meposhtme i perkasin veprimeve qe kryhen nga objektet clsTrupiSkedulimProdhimi dhe colTrupiSkedulimProdhimi
        /// </summary>
        #region TRUPI SKEDULIM PRODHIMI

        /// <summary>
        /// ekzekuton prc_T_TRUPISKEDULIMPRODHIMI_ins ne DB dhe kthen statusin e perfundimit te ekzekutimit te SP-se perkatese
        /// Shiko clsMesazh brenda ketij moduli per informacion te metejshem mbi vleren e kthimit te kesaj metode
        /// </summary>
        /// <param name="idArt"> id e artikullit</param>
        /// <param name="idmag">id e magazines</param>
        /// <param name="idkokaplanifikim">id e kokes se dokumentit te planifikimit</param>
        /// <param name="idtrupiplanifikim">id ritese e trupit te dokumentit te planifikimit</param>
        /// <param name="idNjes">id e njesise se artikullit</param>
        /// <param name="sas"> sasia e artikullit</param>
        /// <returns> nje objekt clsMesazh qe tregon nese ruajtja eshte kryer ne rregull apo jo.</returns>
        internal clsMesazh ruajTrupiSkedulimProdhimi(out int idtrupi, int idkoka, int idburimi, int idplanifikimi, int idaktiviteti, int idprodukti, DateTime data, DateTime nga, DateTime ne, decimal koha, int njesia, decimal kosto, decimal kostototale, string shenime)
        {
            idtrupi = -1;

                dbManager.Open();
                dbManager.CreateParameters(14);
                dbManager.AddParameters(0, "@IDTRUPI", idtrupi, ParameterDirection.Output);
                dbManager.AddParameters(1, "@IDKOKA", idkoka, ParameterDirection.Input);
                if (idburimi == 0 || idburimi == -1) dbManager.AddParameters(2, "@IDBURIMI", DBNull.Value, ParameterDirection.Input);
                else dbManager.AddParameters(2, "@IDBURIMI", idburimi, ParameterDirection.Input);
                if (idplanifikimi == 0 || idplanifikimi == -1) dbManager.AddParameters(3, "@IDPLANIFIKIMI", DBNull.Value, ParameterDirection.Input);
                else dbManager.AddParameters(3, "@IDPLANIFIKIMI", idplanifikimi, ParameterDirection.Input);
                if (idaktiviteti == 0 || idaktiviteti == -1) dbManager.AddParameters(4, "@IDAKTIVITETI", DBNull.Value, ParameterDirection.Input);
                else dbManager.AddParameters(4, "@IDAKTIVITETI", idaktiviteti, ParameterDirection.Input);
                if (idprodukti == 0 || idprodukti == -1) dbManager.AddParameters(5, "@IDPRODUKTI", DBNull.Value, ParameterDirection.Input);
                else dbManager.AddParameters(5, "@IDPRODUKTI", idprodukti, ParameterDirection.Input);
                dbManager.AddParameters(6, "@DATA", data, ParameterDirection.Input);
                dbManager.AddParameters(7, "@NGA", nga.ToLongTimeString(), ParameterDirection.Input);
                dbManager.AddParameters(8, "@NE", ne.ToLongTimeString(), ParameterDirection.Input);
                dbManager.AddParameters(9, "@KOHA", koha, ParameterDirection.Input);
                dbManager.AddParameters(10, "@NJESIA", njesia, ParameterDirection.Input);
                dbManager.AddParameters(11, "@KOSTO", kosto, ParameterDirection.Input);
                dbManager.AddParameters(12, "@KOSTOTOTALE", kostototale, ParameterDirection.Input);
                dbManager.AddParameters(13, "@SHENIME", shenime, ParameterDirection.Input);
                dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_TRUPISKEDULIMPRODHIMI_ins");

                idtrupi = int.Parse(dbManager.Parameters[0].Value.ToString());

                clsMesazh mesazh = new clsMesazh(true, mesazhRuajtje);
                return mesazh;

        }

        /// <summary>
        /// ekzekuton prc_T_TRUPISKEDULIMPRODHIMI_upd ne DB dhe kthen statusin e perfundimit te ekzekutimit te SP-se perkatese
        /// Shiko clsMesazh brenda ketij moduli per informacion te metejshem mbi vleren e kthimit te kesaj metode
        /// </summary>
        /// <param name="idArt"> id e artikullit</param>
        /// <param name="idmag">id e magazines</param>
        /// <param name="idkokaplanifikim">id e kokes se dokumentit te planifikimit</param>
        /// <param name="idtrupiplanifikim">id ritese e trupit te dokumentit te planifikimit</param>
        /// <param name="idNjes">id e njesise se artikullit</param>
        /// <param name="sas"> sasia e artikullit</param>
        /// <returns> nje objekt clsMesazh qe tregon nese modifikimi eshte kryer ne rregull apo jo.</returns>
        internal clsMesazh modifikoTrupiSkedulimProdhimi(int idtrupi, int idkoka, int idburimi, int idplanifikimi, int idaktiviteti, int idprodukti, DateTime data, DateTime nga, DateTime ne, decimal koha, int njesia, decimal kosto, decimal kostototale, string shenime)
        {

                dbManager.Open();
                dbManager.CreateParameters(14);
                dbManager.AddParameters(0, "@IDTRUPI", idtrupi, ParameterDirection.Input);
                dbManager.AddParameters(1, "@IDKOKA", idkoka, ParameterDirection.Input);
                if (idburimi == 0 || idburimi == -1) dbManager.AddParameters(2, "@IDBURIMI", DBNull.Value, ParameterDirection.Input);
                else dbManager.AddParameters(2, "@IDBURIMI", idburimi, ParameterDirection.Input);
                if (idplanifikimi == 0 || idplanifikimi == -1) dbManager.AddParameters(3, "@IDPLANIFIKIMI", DBNull.Value, ParameterDirection.Input);
                else dbManager.AddParameters(3, "@IDPLANIFIKIMI", idplanifikimi, ParameterDirection.Input);
                if (idaktiviteti == 0 || idaktiviteti == -1) dbManager.AddParameters(4, "@IDAKTIVITETI", DBNull.Value, ParameterDirection.Input);
                else dbManager.AddParameters(4, "@IDAKTIVITETI", idaktiviteti, ParameterDirection.Input);
                if (idprodukti == 0 || idprodukti == -1) dbManager.AddParameters(5, "@IDPRODUKTI", DBNull.Value, ParameterDirection.Input);
                else dbManager.AddParameters(5, "@IDPRODUKTI", idprodukti, ParameterDirection.Input);
                dbManager.AddParameters(6, "@DATA", data, ParameterDirection.Input);
                dbManager.AddParameters(7, "@NGA", nga.ToLongTimeString(), ParameterDirection.Input);
                dbManager.AddParameters(8, "@NE", ne.ToLongTimeString(), ParameterDirection.Input);
                dbManager.AddParameters(9, "@KOHA", koha, ParameterDirection.Input);
                dbManager.AddParameters(10, "@NJESIA", njesia, ParameterDirection.Input);
                dbManager.AddParameters(11, "@KOSTO", kosto, ParameterDirection.Input);
                dbManager.AddParameters(12, "@KOSTOTOTALE", kostototale, ParameterDirection.Input);
                dbManager.AddParameters(13, "@SHENIME", shenime, ParameterDirection.Input);
                dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_TRUPISKEDULIMPRODHIMI_upd");
                clsMesazh mesazh = new clsMesazh(true, mesazhModifikimi);
                return mesazh;

        }

        /// <summary>
        /// ekzekutohet sp-ja prc_T_TRUPISKEDULIMPRODHIMI_delSipasIdKoka duke i kaluar id e kokes se dokumentit te skedulim prodhimi qe e marrim nga  qe i kalohet si parameter
        /// </summary>
        /// <param name="idkoka">id e kokes se dokumentit te </param>
        /// <returns>nje objekt clsMesazh qe tregon nese fshirja eshte kryer ne rregull apo jo</returns>
        internal clsMesazh fshiTrupiSkedulimProdhimiSipasKoka(int idkoka)
        {

                dbManager.Open();
                dbManager.CreateParameters(1);
                dbManager.AddParameters(0, "@IDKOKA", idkoka, ParameterDirection.Input);
                dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_TRUPISKEDULIMPRODHIMI_delSipasIdKoka");
                clsMesazh mesazh = new clsMesazh(true, mesazhFshirje);
                return mesazh;

        }

        /// <summary>
        /// ekzekutohet sp-ja prc_T_TRUPISKEDULIMPRODHIMI_delSipasIdTrupi duke i kaluar id e trupit te dokumentit  qe e marrim nga objekti  qe i kalohet si parameter
        /// </summary>
        /// <param name="idtrupi">id ritese e trupit te dokumentit </param>
        /// <returns>nje objekt clsMesazh qe tregon nese fshirja eshte kryer ne rregull apo jo</returns>
        internal clsMesazh fshiTrupiSkedulimProdhimiSipasID(int idtrupi)
        {

                dbManager.Open();
                dbManager.CreateParameters(1);
                dbManager.AddParameters(0, "@IDTRUPI", idtrupi, ParameterDirection.Input);
                dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_TRUPISKEDULIMPRODHIMI_delSipasIdTrupi");
                clsMesazh mesazh = new clsMesazh(true, mesazhFshirje);
                return mesazh;

        }

        /// <summary>
        /// kthen datatable trupi dokumenti te skedulim prodhimi sipas kokes
        /// </summary>
        /// <param name="idkoka">id e kokes se dokumentit </param>
        ///<returns>nje objekt datatable qe permban nje koleksion me te gjitha trupat e dokumentit  te kesaj koke  </returns>
        internal DataTable ktheGjitheTrupiSkedulimProdhimiNgaKoka(int idkoka, int idndermarje)
        {

                dbManager.Open();
                dbManager.CreateParameters(2);
                dbManager.AddParameters(0, "@IDKOKA", idkoka, ParameterDirection.Input);
                dbManager.AddParameters(1, "@idndermarje", idndermarje, ParameterDirection.Input);
                DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_TRUPISKEDULIMPRODHIMI_selAllSipasKoka");
                return ds.Tables[0];

        }

        /// <summary>
        /// kthen datarow trupi dokumenti   sipas idse se trupit te dokumentit
        /// </summary>
        /// <param name="idtrupi">id ritese e trupit te dokumentit </param>
        ///<returns>nje objekt datarow qe permban nje koleksion me te gjitha trupat e dokumentit  te kesaj id  </returns>
        internal DataRow ktheTrupiSkedulimProdhimiSipasID(int idtrupi, int idndermarje)
        {

                dbManager.Open();
                dbManager.CreateParameters(2);
                dbManager.AddParameters(0, "@IDTRUPI", idtrupi, ParameterDirection.Input);
                dbManager.AddParameters(1, "@idndermarje", idndermarje, ParameterDirection.Input);
                DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_TRUPISKEDULIMPRODHIMI_sel");
                if (ds == null)
                    return null;
                if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                    return null;
                return ds.Tables[0].Rows[0];

        }

        #endregion
        /// <summary>
        /// Metodat e meposhtme i perkasin veprimeve qe kryhen nga objektet clsKokaEkzekutim dhe colKokaEkzekutim
        /// </summary>
        #region KOKA EKZEKUTIM PRODHIMI

        /// <summary>
        /// ekzekuton prc_T_KOKAEKZEKUTIMPRODHIMI_ins ne DB dhe kthen statusin e perfundimit te ekzekutimit te SP-se perkatese
        /// Shiko clsMesazh brenda ketij moduli per informacion te metejshem mbi vleren e kthimit te kesaj metode
        /// </summary>
        /// <param name="dtDk"> data e dokumentit te ekzekutimit</param>
        /// <param name="dtRegj"> data e regjistrimit te dokumentit te ekzekutimit</param>
        /// <param name="idmagprodukti"> id magazines ku do shkoje produkti</param>
        /// <param name="idKonf"> id e konfigurimit te dokumentit</param>
        /// <param name="iddoknga">id e dokumentit nga e cila gjenerohet ne rastet e modifikimit </param>
        /// <param name="idmagrecepture">id e magazines nga merren receturat</param>
        /// <param name="idkokaEkzekutim">id ritese e kokes se planifikimit</param>
        /// <param name="idNder"> id e ndermarjes</param>
        /// <param name="idNdVt"> id e ndermarje vitit</param>
        /// <param name="idNiv"> id e nivelit te regjistrimit</param>
        /// <param name="idPer"> id e perdoruesit se e ben regjistrimin</param>
        /// <param name="idSt"> id e statusit te dokumentit</param>
        /// <param name="nrDk">nr i dokumentit</param>
        /// <param name="shenim"> shenime </param>
        /// <param name="idGjenerues">id e dokumentit nga gjenerohet planifikimi nga nje ambjent tjeter</param>
        /// <param name="idKonfigGjenerues">id e konfigurimit nga gjenerohet dokumenti</param>
        /// <param name="idNivelGjenerues">id e nivelit nga gjenerohet dokumenti</param>
        /// <param name="totali"> totali i dokumentit</param>
        /// <returns> nje objekt clsMesazh qe tregon nese ruajtja eshte kryer ne rregull apo jo.</returns>
        internal clsMesazh ruajKokaEkzekutim(out int idkokaEkzekutim, int idNiv, int idKonf, int idmagprodukti, int idmagrecepture, DateTime dtDk, string nrDk, int iddoknga, int idSt, int idNder, int idNdVt, int idPer, DateTime dtRegj, string shenim, int idNivelGjenerues, int idKonfigGjenerues, int idGjenerues, double totali, int idgrup1, int idgrup2, int idgrup3, int idNjesiProdhimi)
        {
            idkokaEkzekutim = -1;

                dbManager.Open();
                dbManager.CreateParameters(22);
                dbManager.AddParameters(0, "@IDKOKA", idkokaEkzekutim, ParameterDirection.Output);
                dbManager.AddParameters(1, "@IDNIVEL", idNiv, ParameterDirection.Input);
                dbManager.AddParameters(2, "@IDKONFIGAMBJENTE", idKonf, ParameterDirection.Input);
                if (idmagprodukti == 0) dbManager.AddParameters(3, "@IDMAGPRODUKTI", DBNull.Value, ParameterDirection.Input);
                else dbManager.AddParameters(3, "@IDMAGPRODUKTI", idmagprodukti, ParameterDirection.Input);
                if (idmagrecepture == 0) dbManager.AddParameters(4, "@IDMAGRECEPTURA", DBNull.Value, ParameterDirection.Input);
                else dbManager.AddParameters(4, "@IDMAGRECEPTURA", idmagrecepture, ParameterDirection.Input);
                dbManager.AddParameters(5, "@NRDOK", nrDk, ParameterDirection.Input);
                dbManager.AddParameters(6, "@DTDOK", dtDk, ParameterDirection.Input);
                if (iddoknga == 0) dbManager.AddParameters(7, "@IDDOKNGA", DBNull.Value, ParameterDirection.Input);
                else dbManager.AddParameters(7, "@IDDOKNGA", iddoknga, ParameterDirection.Input);
                dbManager.AddParameters(8, "@IDSTATUSDOK", idSt, ParameterDirection.Input);
                dbManager.AddParameters(9, "@IDNDERMARJE", idNder, ParameterDirection.Input);
                dbManager.AddParameters(10, "@IDNDERVITI", idNdVt, ParameterDirection.Input);
                dbManager.AddParameters(11, "@IDPERDORUESI", idPer, ParameterDirection.Input);
                dbManager.AddParameters(12, "@DTREGJ", dtRegj, ParameterDirection.Input);
                dbManager.AddParameters(13, "@SHENIME", shenim, ParameterDirection.Input);
                if (idNivelGjenerues == 0) dbManager.AddParameters(14, "@IDNIVELGJENERUES", DBNull.Value, ParameterDirection.Input);
                else dbManager.AddParameters(14, "@IDNIVELGJENERUES", idNivelGjenerues, ParameterDirection.Input);
                if (idKonfigGjenerues == 0) dbManager.AddParameters(15, "@IDKONFIGGJENERUES", DBNull.Value, ParameterDirection.Input);

                else dbManager.AddParameters(15, "@IDKONFIGGJENERUES", idKonfigGjenerues, ParameterDirection.Input);
                if (idGjenerues == 0) dbManager.AddParameters(16, "@IDGJENERUES", DBNull.Value, ParameterDirection.Input);
                else dbManager.AddParameters(16, "@IDGJENERUES", idGjenerues, ParameterDirection.Input);
                dbManager.AddParameters(17, "@TOTALI", totali, ParameterDirection.Input);
                if (idgrup1 == 0) dbManager.AddParameters(18, "@IDGRUP1", DBNull.Value, ParameterDirection.Input);
                else dbManager.AddParameters(18, "@IDGRUP1", idgrup1, ParameterDirection.Input);
                if (idgrup2 == 0) dbManager.AddParameters(19, "@IDGRUP2", DBNull.Value, ParameterDirection.Input);
                else dbManager.AddParameters(19, "@IDGRUP2", idgrup2, ParameterDirection.Input);
                if (idgrup3 == 0) dbManager.AddParameters(20, "@IDGRUP3", DBNull.Value, ParameterDirection.Input);
                else dbManager.AddParameters(20, "@IDGRUP3", idgrup3, ParameterDirection.Input);
                if (idNjesiProdhimi<=0)
                    dbManager.AddParameters(21, "@IDNJESIPRODHIMI", DBNull.Value, ParameterDirection.Input);
                else
                    dbManager.AddParameters(21, "@IDNJESIPRODHIMI", idNjesiProdhimi, ParameterDirection.Input);                    
                dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_KOKAEKZEKUTIMPRODHIMI_ins");
                idkokaEkzekutim = int.Parse(dbManager.Parameters[0].Value.ToString());
                clsMesazh mesazh = new clsMesazh(true, mesazhRuajtje);
                return mesazh;

        }

        /// <summary>
        /// ekzekuton prc_T_KOKAEKZEKUTIMPRODHIMI_upd ne DB dhe kthen statusin e perfundimit te ekzekutimit te SP-se perkatese
        /// Shiko clsMesazh brenda ketij moduli per informacion te metejshem mbi vleren e kthimit te kesaj metode 
        /// </summary>
        /// <param name="dtDk"> data e dokumentit te ekzekutimit</param>
        /// <param name="dtRegj"> data e regjistrimit te dokumentit te ekzekutimit</param>
        /// <param name="idmagprodukti"> id magazines ku do shkoje produkti</param>
        /// <param name="idKonf"> id e konfigurimit te dokumentit</param>
        /// <param name="iddoknga">id e dokumentit nga e cila gjenerohet ne rastet e modifikimit </param>
        /// <param name="idmagrecepture">id e magazines nga merren receturat</param>
        /// <param name="idkokaEkzekutim">id ritese e kokes se planifikimit</param>
        /// <param name="idNder"> id e ndermarjes</param>
        /// <param name="idNdVt"> id e ndermarje vitit</param>
        /// <param name="idNiv"> id e nivelit te regjistrimit</param>
        /// <param name="idPer"> id e perdoruesit se e ben regjistrimin</param>
        /// <param name="idSt"> id e statusit te dokumentit</param>
        /// <param name="nrDk">nr i dokumentit</param>
        /// <param name="shenim"> shenime </param>
        /// <param name="idGjenerues">id e dokumentit nga gjenerohet planifikimi nga nje ambjent tjeter</param>
        /// <param name="idKonfigGjenerues">id e konfigurimit nga gjenerohet dokumenti</param>
        /// <param name="idNivelGjenerues">id e nivelit nga gjenerohet dokumenti</param>
        /// <param name="totali"> totali i dokumentit</param>
        /// <returns> nje objekt clsMesazh qe tregon nese modifikimi eshte kryer ne rregull apo jo.</returns>
        internal clsMesazh modifikoKokaEkzekutim(int idkokaEkzekutim, int idNiv, int idKonf, int idmagprodukti, int idmagrecepture, DateTime dtDk, string nrDk, int iddoknga, int idSt, int idNder, int idNdVt, int idPer, DateTime dtRegj, string shenim, int idNivelGjenerues, int idKonfigGjenerues, int idGjenerues, double totali, int idgrup1, int idgrup2, int idgrup3, int idNjesiProdhimi)
        {
                dbManager.Open();
                dbManager.CreateParameters(21);
                dbManager.AddParameters(0, "@IDKOKA", idkokaEkzekutim, ParameterDirection.Input);
                dbManager.AddParameters(1, "@IDNIVEL", idNiv, ParameterDirection.Input);
                dbManager.AddParameters(2, "@IDKONFIGAMBJENTE", idKonf, ParameterDirection.Input);
                if (idmagprodukti == 0)
                    dbManager.AddParameters(3, "@IDMAGPRODUKTI", DBNull.Value, ParameterDirection.Input);
                else
                    dbManager.AddParameters(3, "@IDMAGPRODUKTI", idmagprodukti, ParameterDirection.Input);
                if (idmagrecepture == 0)
                    dbManager.AddParameters(4, "@IDMAGRECEPTURA", DBNull.Value, ParameterDirection.Input);
                else
                    dbManager.AddParameters(4, "@IDMAGRECEPTURA", idmagrecepture, ParameterDirection.Input);
                dbManager.AddParameters(5, "@NRDOK", nrDk, ParameterDirection.Input);
                dbManager.AddParameters(6, "@DTDOK", dtDk, ParameterDirection.Input);
                if (iddoknga == 0)
                    dbManager.AddParameters(7, "@IDDOKNGA", DBNull.Value, ParameterDirection.Input);
                else
                    dbManager.AddParameters(7, "@IDDOKNGA", iddoknga, ParameterDirection.Input);
                dbManager.AddParameters(8, "@IDSTATUSDOK", idSt, ParameterDirection.Input);
                dbManager.AddParameters(9, "@IDNDERMARJE", idNder, ParameterDirection.Input);
                dbManager.AddParameters(10, "@IDNDERVITI", idNdVt, ParameterDirection.Input);
                dbManager.AddParameters(11, "@DTREGJ", dtRegj, ParameterDirection.Input);
                dbManager.AddParameters(12, "@SHENIME", shenim, ParameterDirection.Input);
                if (idNivelGjenerues == 0)
                    dbManager.AddParameters(13, "@IDNIVELGJENERUES", DBNull.Value, ParameterDirection.Input);
                else
                    dbManager.AddParameters(13, "@IDNIVELGJENERUES", idNivelGjenerues, ParameterDirection.Input);
                if (idKonfigGjenerues == 0)
                    dbManager.AddParameters(14, "@IDKONFIGGJENERUES", DBNull.Value, ParameterDirection.Input);
                else dbManager.AddParameters(14, "@IDKONFIGGJENERUES", idKonfigGjenerues, ParameterDirection.Input);
                if (idGjenerues == 0)
                    dbManager.AddParameters(15, "@IDGJENERUES", DBNull.Value, ParameterDirection.Input);
                else
                    dbManager.AddParameters(15, "@IDGJENERUES", idGjenerues, ParameterDirection.Input);
                dbManager.AddParameters(16, "@TOTALI", totali, ParameterDirection.Input);
                if (idgrup1 == 0)
                    dbManager.AddParameters(17, "@IDGRUP1", DBNull.Value, ParameterDirection.Input);
                else
                    dbManager.AddParameters(17, "@IDGRUP1", idgrup1, ParameterDirection.Input);
                if (idgrup2 == 0)
                    dbManager.AddParameters(18, "@IDGRUP2", DBNull.Value, ParameterDirection.Input);
                else
                    dbManager.AddParameters(18, "@IDGRUP2", idgrup2, ParameterDirection.Input);
                if (idgrup3 == 0)
                    dbManager.AddParameters(19, "@IDGRUP3", DBNull.Value, ParameterDirection.Input);
                else
                    dbManager.AddParameters(19, "@IDGRUP3", idgrup3, ParameterDirection.Input);
                if (idNjesiProdhimi == 0)
                    dbManager.AddParameters(20, "@IDNJESIPRODHIMI", DBNull.Value, ParameterDirection.Input);
                else
                    dbManager.AddParameters(20, "@IDNJESIPRODHIMI", idNjesiProdhimi, ParameterDirection.Input);
                dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_KOKAEKZEKUTIMPRODHIMI_upd");
                clsMesazh mesazh = new clsMesazh(true, mesazhModifikimi);
                return mesazh;

        }

        /// <summary>
        /// ekzekuton prc_T_KOKAEKZEKUTIMPRODHIMI_updTotal ne DB dhe kthen statusin e perfundimit te ekzekutimit te SP-se perkatese
        /// perdoret per te update-uar totslin kur shtojme nje ekzekutim te ri
        /// Shiko clsMesazh brenda ketij moduli per informacion te metejshem mbi vleren e kthimit te kesaj metode 
        /// </summary>

        /// <param name="totali"> totali i dokumentit</param>
        /// <returns> nje objekt clsMesazh qe tregon nese modifikimi eshte kryer ne rregull apo jo.</returns>
        internal clsMesazh modifikoTotalKokaEkzekutim(int idkokaEkzekutim, double totali)
        {

                dbManager.Open();
                dbManager.CreateParameters(2);
                dbManager.AddParameters(0, "@IDKOKA", idkokaEkzekutim, ParameterDirection.Input);
                dbManager.AddParameters(1, "@TOTALI", totali, ParameterDirection.Input);

                dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_KOKAEKZEKUTIMPRODHIMI_updTotal");
                clsMesazh mesazh = new clsMesazh(true, mesazhModifikimi);
                return mesazh;

        }

        internal clsMesazh kaloNeHistorikKokaEkzekutim(int idkoka)
        {

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDKOKA", idkoka, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_KOKAEKZEKUTIMPRODHIMI_hidhNeHistorik");
            clsMesazh mesazh = new clsMesazh(true, mesazhFshirje);
            return mesazh;

        }

        /// <summary>
        /// ekzekutohet sp-ja prc_T_KOKAEKZEKUTIMPRODHIMI_upddel duke i kaluar id e kokes se dokumentit qe e marrim nga objekti clsKokaEkzekutim qe i kalohet si parameter
        /// </summary>
        /// <param name="idkokaekzekutim">id ritese e kokes se ekzekutimit</param>
        /// <returns>nje objekt clsMesazh qe tregon nese fshirja eshte kryer ne rregull apo jo</returns>
        internal clsMesazh fshiKokaEkzekutim(int idkokaekzekutim, int idPer)
        {

                dbManager.Open();
                dbManager.CreateParameters(2);
                dbManager.AddParameters(0, "@IDKOKA", idkokaekzekutim, ParameterDirection.Input);
                dbManager.AddParameters(1, "@IDPERDORUESI", idPer, ParameterDirection.Input);
                dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_KOKAEKZEKUTIMPRODHIMI_upddel");
                clsMesazh mesazh = new clsMesazh(true, mesazhFshirje);
                return mesazh;

        }
        ///<summary>
        ///
        ///</summary>
        internal clsMesazh fshiLidhesSkedulimReceptura(int idkokaekzekutim)
        {

                dbManager.Open();
                dbManager.CreateParameters(1);
                dbManager.AddParameters(0, "@IDKOKA", idkokaekzekutim, ParameterDirection.Input);

                dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_LIDHESSKEDULIMEKZEKUTIM_del");
                clsMesazh mesazh = new clsMesazh(true, mesazhFshirje);
                return mesazh;

        }
        /// kthen objekt koka dokumenti ekzekutim sipas idse
        /// </summary>
        ///<param name="idkokaekzekutim"> koka e dokumentit te ekzekutimit</param>
        ///<returns>nje objekt datarow qe permban nje koleksion me te gjitha kokat e dokumentit te ekzekutimit me kete id  </returns>
        internal DataRow ktheKokaEkzekutimSipasID(int idkokaekzekutim)
        {

                dbManager.Open();
                dbManager.CreateParameters(1);
                dbManager.AddParameters(0, "@IDKOKA", idkokaekzekutim, ParameterDirection.Input);
                DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KOKAEKZEKUTIMPRODHIMI_sel");
                if (ds == null)
                    return null;
                if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                    return null;
                return ds.Tables[0].Rows[0];

        }

        /// <summary>
        /// kthen datarow koka dokumenti ekzekutimit sipas idse dokumenti nga qe eshte gjeneruar nga modifikimi
        /// </summary>
        ///<param name="idDokNga"> koka e dokumentit nga te ekzekutimit</param>
        ///<returns>nje objekt datarow qe permban nje koleksion me te gjitha kokat e dokumentit te ekzekutimit me kete id dok nga </returns>
        internal DataRow ktheKokaEkzekutimSipasIDDokNga(int idDokNga)
        {

                dbManager.Open();
                dbManager.CreateParameters(1);
                dbManager.AddParameters(0, "@IDDOKNGA", idDokNga, ParameterDirection.Input);
                DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KOKAEKZEKUTIMPRODHIMI_merrSipasIdDokNga");
                if (ds == null)
                    return null;
                if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                    return null;
                return ds.Tables[0].Rows[0];

        }

        /// <summary>
        /// kthen datarow koka dokumenti ekzekutimi sipas nivelit te dokumentit, nr te dokumentit dhe dates se dokumentit
        /// </summary>
        ///<param name="idNivel"> id e nivelit</param>
        ///<param name="nrdok"> nr i dokumentit te gjenerues</param>
        ///<param name="dtdok"> data e dokumentit</param>
        ///<returns>nje objekt datarow qe permban nje koleksion me te gjitha kokat e dokumentit te ekzekutimit me kete nivel dokumenti, nr dokumenti dhe date dokumenti  </returns>
        internal DataRow ktheKokaEkzekutimSipasIdNivelNrDokDtDok(int idNivel, string nrdok, DateTime dtdok)
        {

                dbManager.Open();
                dbManager.CreateParameters(3);
                dbManager.AddParameters(0, "@IDNNIVEL", idNivel, ParameterDirection.Input);
                dbManager.AddParameters(1, "@NRDOK", nrdok, ParameterDirection.Input);
                dbManager.AddParameters(2, "@DTDOK", dtdok, ParameterDirection.Input);

                DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KOKAEKZEKUTIMPRODHIMI_selSipasIdNivelNrDokDtDok");
                if (ds == null)
                    return null;
                if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                    return null;
                return ds.Tables[0].Rows[0];

        }

        /// <summary>
        /// kthen datatable koka dokumenti ekzekutimit sipas ndermarje vitit 
        /// </summary>
        ///<param name="idNdermVit">id e ndermarje vitit</param>
        ///<returns>nje objekt datatable qe permban nje koleksion me te gjitha kokat e dokumentit te ekzekutimit  te kesaj ndermarje viti  </returns>
        internal DataTable ktheGjitheKokaEkzekutimi(int idNdermVit)
        {

                dbManager.Open();
                dbManager.CreateParameters(1);
                dbManager.AddParameters(0, "@IDNDERMVIT", idNdermVit, ParameterDirection.Input);
                DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KOKAEKZEKUTIMPRODHIMI_selAllNdermVit");
                return ds.Tables[0];

        }

        /// <summary>
        /// kthen datatable trupi dokumenti te ekzekutimit sipas kokes
        /// </summary>
        ///<param name="idkokaekzekutim">koka e dokumentit te ekzekutimit</param>
        ///<returns>nje objekt datatable qe permban nje koleksion me te gjitha trupat e dokumentit te ekzekutimit te kesaj koke  </returns>
        internal DataTable ktheProduktProdhimi(int idkokaekzekutim)
        {

                dbManager.Open();
                dbManager.CreateParameters(1);
                dbManager.AddParameters(0, "@IDKOKA", idkokaekzekutim, ParameterDirection.Input);
                DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_PRODUKTPRODHIMI_selAllSipasKoka");
                return ds.Tables[0];

        }

        /// <summary>
        /// perdoret per te kontrolluar nqs ekziston nje dokument ekzekutimi me nje nr dokumenti te marre si parameter
        /// behet kontrolli per qellime konsistence te informacionit ne DB, nuk lejon te celen dokumenta  te ndryshem me te njejtin kod ne nje ndermarje
        /// </summary>
        /// <param name="idNdermarrje"> id e ndermarjes</param>
        /// <param name="dtdok"> data e dokumentit te planifikimit</param>
        /// <param name="idKonfigAmbjente"> id e konfigurimit te dokumentit</param>
        /// <param name="nrDok">nr i dokumentit</param>
        /// <returns>nje objeckt clsMesazh qe tregon nese ekziston apo jo nje dokument me kete kod</returns>
        internal bool ekzistonRegjistrimEkzekutimi(int idKonfigAmbjente, string nrDok, DateTime dtdok, int idNdermarrje)
        {
            dbManager.Open();
            dbManager.CreateParameters(4);
            dbManager.AddParameters(0, "@LLOJI", idKonfigAmbjente, ParameterDirection.Input);
            dbManager.AddParameters(1, "@NRDOK", nrDok, ParameterDirection.Input);
            dbManager.AddParameters(2, "@DTDOK", dtdok, ParameterDirection.Input);
            dbManager.AddParameters(3, "@IDNDERMARJE", idNdermarrje, ParameterDirection.Input);
            return Convert.ToInt32(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_KOKAEKZEKUTIMPRODHIMI_ekzistonRegjistrimEkzekutim")) > 0;
        }

        /// <summary>
        /// merr kokat e ekzekutimi sipas idndervitit dhe autorizimeve ne forme datatable
        /// </summary>
        /// <param name="idNdermVit">idndermvit</param>
        /// <param name="idperdoruesi">idperdoruesi</param>
        /// <returns>data table me keto te dhena</returns>
        internal DataTable merrKokaEkzekutimDT(int idNdermVit, int idperdoruesi)
        {//metoda per te marre te gjithe  kokat e fleteve kontabel te pa kontabilizuara

                dbManager.Open();
                dbManager.CreateParameters(2);
                dbManager.AddParameters(0, "@IDNDERVITI", idNdermVit, ParameterDirection.Input);
                dbManager.AddParameters(1, "@IDPERDORUES", idperdoruesi, ParameterDirection.Input);
                DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KOKAEKZEKUTIMPRODHIMI_merrKokaEkzekutimProdhimiDT");
                return ds.Tables[0];

        }

        internal DataTable merrEkzekutimePerEksport(int idnderm, int idperdorues, int idNderViti, int lloji, string emerTabKoka, string emerFusheId, string idPerEksport)
        {
            dbManager.Open();
            dbManager.CreateParameters(7);
            dbManager.AddParameters(0, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUES", idperdorues, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDNDERVITI", idNderViti, ParameterDirection.Input);
            dbManager.AddParameters(3, "@LLOJI", lloji, ParameterDirection.Input);
            dbManager.AddParameters(4, "@EMERTABKOKA", emerTabKoka, ParameterDirection.Input);
            dbManager.AddParameters(5, "@EMERFUSHEID", emerFusheId, ParameterDirection.Input);
            dbManager.AddParameters(6, "@IDPEREKSPORT", idPerEksport, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KOKAEKZEKUTIMPRODHIMI_merrEkzekutimProdhimiPerEksport");
            return ds.Tables[0];
        }

        internal bool kaAutorizimKokaEkzekutim(int idkoka, int idperdoruesi)
        {

                dbManager.Open();
                dbManager.CreateParameters(2);
                dbManager.AddParameters(0, "@IDKOKA", idkoka, ParameterDirection.Input);
                dbManager.AddParameters(1, "@IDPERDORUES", idperdoruesi, ParameterDirection.Input);
                DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KOKAEKZEKUTIMPRODHIMI_kaAutorizim");
                if (ds.Tables[0].Rows.Count == 1)
                    return true;
                else if (ds.Tables[0].Rows.Count == 0)
                    return false;
                else return true;

        }

        internal DataTable merrDokEkzekutimProdhimiPerImport(string emerTabEkz, string emerTabProd, string emerTabRec, string ndermarrjeKey, string ndermarjeKodi, string primaryKeyEkzekutimi, string primaryKeyProdukte, bool merrTePaImportuara, bool rimerrTeImportuara, int? nrDokumentash)
        {

                dbManager.Open();
                dbManager.CreateParameters(10);
                dbManager.AddParameters(0, "@TABEKZEKUTIM", emerTabEkz, ParameterDirection.Input);
                dbManager.AddParameters(1, "@TABPRODUKT", emerTabProd, ParameterDirection.Input);
                dbManager.AddParameters(2, "@TABRECEPTURAT", emerTabRec, ParameterDirection.Input);
                dbManager.AddParameters(3, "@NDERMARRJEKEY", ndermarrjeKey, ParameterDirection.Input);
                dbManager.AddParameters(4, "@NDERMARJEKODI", ndermarjeKodi, ParameterDirection.Input);
                dbManager.AddParameters(5, "@PRIMARYKEYEKZEKUTIM", primaryKeyEkzekutimi, ParameterDirection.Input);
                dbManager.AddParameters(6, "@PRIMARYKEYPRODUKT", primaryKeyProdukte, ParameterDirection.Input);
                dbManager.AddParameters(7, "@MERRTEPAIMPORTUARA", merrTePaImportuara, ParameterDirection.Input);
                dbManager.AddParameters(8, "@RIMERRTEIMPORTUARA", rimerrTeImportuara, ParameterDirection.Input);
                dbManager.AddParameters(9, "@NRDOKUMENTASH", nrDokumentash, ParameterDirection.Input);
                DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_TEMP_IMPORTEkzekutimProdhimiMerrTeDhenaPerNdermarrjeSipasLlojitDT");
                return ds.Tables[0];

        }

        internal DataTable merrDokEkzekutimProdhimiPerImport(string emerTabEkz, string emerTabProd, string ndermarrjeKey, string ndermarjeKodi, string primaryKeyEkzekutimi, bool merrTePaImportuara, bool riMerrTeImportuara, int? nrDokumentash)
        {

            dbManager.Open();
            dbManager.CreateParameters(8);
            dbManager.AddParameters(0, "@TABEKZEKUTIM", emerTabEkz, ParameterDirection.Input);
            dbManager.AddParameters(1, "@TABPRODUKT", emerTabProd, ParameterDirection.Input);
            dbManager.AddParameters(2, "@NDERMARRJEKEY", ndermarrjeKey, ParameterDirection.Input);
            dbManager.AddParameters(3, "@NDERMARJEKODI", ndermarjeKodi, ParameterDirection.Input);
            dbManager.AddParameters(4, "@PRIMARYKEYEKZEKUTIM", primaryKeyEkzekutimi, ParameterDirection.Input);
            dbManager.AddParameters(5, "@MERRTEPAIMPORTUARA", merrTePaImportuara, ParameterDirection.Input);
            dbManager.AddParameters(6, "@NRDOKUMENTASH", nrDokumentash, ParameterDirection.Input);
            dbManager.AddParameters(7, "@RIMERRTEIMPORTUARA", riMerrTeImportuara, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_TEMP_IMPORTEkzekutimKokeTrupMerrTeDhenaPerNdermarrjeSipasLlojitDT");
            return ds.Tables[0];

        }

        internal DataTable merrDokEkzekutimKokaPerImport(string emerTabEkz, string ndermarrjeKey, string ndermarjeKodi)
        {
            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@TABEKZEKUTIM", emerTabEkz, ParameterDirection.Input);
            dbManager.AddParameters(1, "@NDERMARRJEKEY", ndermarrjeKey, ParameterDirection.Input);
            dbManager.AddParameters(2, "@NDERMARJEKODI", ndermarjeKodi, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_TEMP_IMPORTKokaMerrTeDhenaPerNdermarrjeSipasLlojitDT");
            return ds.Tables[0];
        }

        #endregion

        /// <summary>
        /// Metodat e meposhtme i perkasin veprimeve qe kryhen nga objektet clsProduktProdhimi dhe colProduktProdhimi
        /// </summary>
        #region PRODUKT PRODHIMI

        /// <summary>
        /// ekzekuton prc_T_PRODUKTPRODHIMI_ins ne DB dhe kthen statusin e perfundimit te ekzekutimit te SP-se perkatese
        /// Shiko clsMesazh brenda ketij moduli per informacion te metejshem mbi vleren e kthimit te kesaj metode
        /// </summary>
        /// <param name="idArt"> id e artikullit</param>
        /// <param name="idmag">id e magazines</param>
        /// <param name="idkoka">id e kokes se dokumentit te ekzekutimit</param>
        /// <param name="id">id ritese e produktit</param>
        /// <param name="idNjes">id e njesise se artikullit</param>
        /// <param name="sasiplan"> sasia e planifikuar e artikullit</param>
        /// <param name="kosto">kostoja e artikullit</param>
        /// <param name="sasiakt">sasia aktuale</param>
        /// <returns> nje objekt clsMesazh qe tregon nese ruajtja eshte kryer ne rregull apo jo.</returns>
        internal clsMesazh ruajProduktProdhimi(out int id, int idkoka, int idArt, int idNjes, double sasiplan, double sasiakt, double kosto, int idmag, double gjeresiplan, double gjatesiplan, double sasiPermase, int idplanifikim, int idurdherporosi, int idDetajim1, int idDetajim2)
        {
            id = -1;

                dbManager.Open();
                dbManager.CreateParameters(15);
                dbManager.AddParameters(0, "@ID", id, ParameterDirection.Output);
                dbManager.AddParameters(1, "@IDKOKA", idkoka, ParameterDirection.Input);
                dbManager.AddParameters(2, "@IDARTIKULLI", idArt, ParameterDirection.Input);
                if (idNjes == 0 || idNjes == -1) dbManager.AddParameters(3, "@IDNJESIA", DBNull.Value, ParameterDirection.Input);
                else dbManager.AddParameters(3, "@IDNJESIA", idNjes, ParameterDirection.Input);
                dbManager.AddParameters(4, "@SASIAPLAN", sasiplan, ParameterDirection.Input);
                if (idmag == 0 || idmag == -1) dbManager.AddParameters(5, "@IDMAG", DBNull.Value, ParameterDirection.Input);
                else dbManager.AddParameters(5, "@IDMAG", idmag, ParameterDirection.Input);
                dbManager.AddParameters(6, "@SASIAAKT", sasiakt, ParameterDirection.Input);
                dbManager.AddParameters(7, "@KOSTO", kosto, ParameterDirection.Input);
                dbManager.AddParameters(8, "@GJERESIPLAN", gjeresiplan, ParameterDirection.Input);
                dbManager.AddParameters(9, "@GJATESIPLAN", gjatesiplan, ParameterDirection.Input);
                dbManager.AddParameters(10, "@SASIPERMASE", sasiPermase, ParameterDirection.Input);
                if (idplanifikim == 0 || idplanifikim == -1) dbManager.AddParameters(11, "@idplanifikim", DBNull.Value, ParameterDirection.Input);
                else dbManager.AddParameters(11, "@idplanifikim", idplanifikim, ParameterDirection.Input);
                if (idurdherporosi == 0 || idurdherporosi == -1) dbManager.AddParameters(12, "@IDURDHERPOROSI", DBNull.Value, ParameterDirection.Input);
                else dbManager.AddParameters(12, "@IDURDHERPOROSI", idurdherporosi, ParameterDirection.Input);
                if (idDetajim1 == 0 || idDetajim1 == -1) dbManager.AddParameters(13, "@IDDETAJIM1", DBNull.Value, ParameterDirection.Input);
                else dbManager.AddParameters(13, "@IDDETAJIM1", idDetajim1, ParameterDirection.Input);
                if (idDetajim2 == 0 || idDetajim2 == -1) dbManager.AddParameters(14, "@IDDETAJIM2", DBNull.Value, ParameterDirection.Input);
                else dbManager.AddParameters(14, "@IDDETAJIM2", idDetajim2, ParameterDirection.Input);
                dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_PRODUKTPRODHIMI_ins");
                id = int.Parse(dbManager.Parameters[0].Value.ToString());
                clsMesazh mesazh = new clsMesazh(true, mesazhRuajtje);
                return mesazh;
        }

        /// <summary>
        /// ekzekuton prc_T_PRODUKTPRODHIMI_upd ne DB dhe kthen statusin e perfundimit te ekzekutimit te SP-se perkatese
        /// Shiko clsMesazh brenda ketij moduli per informacion te metejshem mbi vleren e kthimit te kesaj metode
        /// </summary>
        /// <param name="idArt"> id e artikullit</param>
        /// <param name="idmag">id e magazines</param>
        /// <param name="idkoka">id e kokes se dokumentit te ekzekutimit</param>
        /// <param name="id">id ritese e produktit</param>
        /// <param name="idNjes">id e njesise se artikullit</param>
        /// <param name="sasiplan"> sasia e planifikuar e artikullit</param>
        /// <param name="kosto">kostoja e artikullit</param>
        /// <param name="sasiakt">sasia aktuale</param>
        /// <returns> nje objekt clsMesazh qe tregon nese modifikimi eshte kryer ne rregull apo jo.</returns>
        internal clsMesazh modifikoProduktProdhimi(int id, int idkoka, int idArt, int idNjes, double sasiplan, double sasiakt, double kosto, int idmag, double gjeresiplan, double gjatesiplan, double sasiPermase, int idplanifikim, int idurdherporosi, int idDetajim1, int idDetajim2)
        {

                dbManager.Open();
                dbManager.CreateParameters(15);
                dbManager.AddParameters(0, "@ID", id, ParameterDirection.Input);
                dbManager.AddParameters(1, "@IDKOKA", idkoka, ParameterDirection.Input);
                dbManager.AddParameters(2, "@IDARTIKULLI", idArt, ParameterDirection.Input);
                if (idNjes == 0 || idNjes == -1) dbManager.AddParameters(3, "@IDNJESIA", DBNull.Value, ParameterDirection.Input);
                else dbManager.AddParameters(3, "@IDNJESIA", idNjes, ParameterDirection.Input);
                dbManager.AddParameters(4, "@SASIAPLAN", sasiplan, ParameterDirection.Input);
                if (idmag == 0 || idmag == -1) dbManager.AddParameters(5, "@IDMAG", DBNull.Value, ParameterDirection.Input);
                else dbManager.AddParameters(5, "@IDMAG", idmag, ParameterDirection.Input);
                dbManager.AddParameters(6, "@SASIAAKT", sasiakt, ParameterDirection.Input);
                dbManager.AddParameters(7, "@KOSTO", kosto, ParameterDirection.Input);
                dbManager.AddParameters(8, "@GJERESIPLAN", gjeresiplan, ParameterDirection.Input);
                dbManager.AddParameters(9, "@GJATESIPLAN", gjatesiplan, ParameterDirection.Input);
                dbManager.AddParameters(10, "@SASIPERMASE", sasiPermase, ParameterDirection.Input);
                if (idplanifikim == 0 || idplanifikim == -1) dbManager.AddParameters(11, "@idplanifikim", DBNull.Value, ParameterDirection.Input);
                else dbManager.AddParameters(11, "@idplanifikim", idplanifikim, ParameterDirection.Input);
                if (idurdherporosi == 0 || idurdherporosi == -1) dbManager.AddParameters(12, "@IDURDHERPOROSI", DBNull.Value, ParameterDirection.Input);
                else dbManager.AddParameters(12, "@IDURDHERPOROSI", idurdherporosi, ParameterDirection.Input);
                if (idDetajim1 == 0 || idDetajim1 == -1) dbManager.AddParameters(13, "@IDDETAJIM1", DBNull.Value, ParameterDirection.Input);
                else dbManager.AddParameters(13, "@IDDETAJIM1", idDetajim1, ParameterDirection.Input);
                if (idDetajim2 == 0 || idDetajim2 == -1) dbManager.AddParameters(14, "@IDDETAJIM2", DBNull.Value, ParameterDirection.Input);
                else dbManager.AddParameters(14, "@IDDETAJIM2", idDetajim2, ParameterDirection.Input);
                dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_PRODUKTPRODHIMI_upd");
                clsMesazh mesazh = new clsMesazh(true, mesazhModifikimi);
                return mesazh;

        }
        internal clsMesazh modifikoProduktProdhimiIdUrdherPorosi(int idurdherporosiVjeter, int idurdherporosi)
        {

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDURDHERPOROSIVJETER", idurdherporosiVjeter, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDURDHERPOROSI", idurdherporosi, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_PRODUKTPRODHIMI_updIdUrdherPorosi");
            clsMesazh mesazh = new clsMesazh(true, mesazhModifikimi);
            return mesazh;

        }
        internal clsMesazh modifikoProduktProdhimiIdplanifikim(int idplanifikimvjeter, int idplanifikim)
        {

                dbManager.Open();
                dbManager.CreateParameters(2);
                dbManager.AddParameters(0, "@IDPLANIFIKIMVJETER", idplanifikimvjeter, ParameterDirection.Input);
                dbManager.AddParameters(1, "@IDPLANIFIKIM", idplanifikim, ParameterDirection.Input);
                dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_PRODUKTPRODHIMI_updIdPlanifikim");
                clsMesazh mesazh = new clsMesazh(true, mesazhModifikimi);
                return mesazh;

        }
        /// <summary>
        /// ekzekutohet sp-ja prc_T_PRODUKTPRODHIMI_delSipasIdKoka duke i kaluar id e kokes se dokumentit te ekzekutimit 
        /// </summary>
        /// <param name="idkoka">id e kokes se dokumentit te planifikimit</param>
        /// <returns>nje objekt clsMesazh qe tregon nese fshirja eshte kryer ne rregull apo jo</returns>
        internal clsMesazh fshiProduktProdhimiSipasKoka(int idkoka)
        {

                dbManager.Open();
                dbManager.CreateParameters(1);
                dbManager.AddParameters(0, "@IDKOKA", idkoka, ParameterDirection.Input);
                dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_PRODUKTPRODHIMI_delSipasIdKoka");
                clsMesazh mesazh = new clsMesazh(true, mesazhFshirje);
                return mesazh;

        }

        /// <summary>
        /// ekzekutohet sp-ja prc_T_PRODUKTPRODHIMI_delSipasId duke i kaluar id e produktit
        /// </summary>
        /// <param name="id">id ritese produktit te prodhimit</param>
        /// <returns>nje objekt clsMesazh qe tregon nese fshirja eshte kryer ne rregull apo jo</returns>
        internal clsMesazh fshiProduktProdhimiSipasID(int id)
        {

                dbManager.Open();
                dbManager.CreateParameters(1);
                dbManager.AddParameters(0, "@ID", id, ParameterDirection.Input);
                dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_PRODUKTPRODHIMI_delSipasId");
                clsMesazh mesazh = new clsMesazh(true, mesazhFshirje);
                return mesazh;

        }

        /// <summary>
        /// kthen datatable produktet e prodhimit sipas id koka
        /// </summary>
        /// <param name="idkoka">id e kokes se dokumentit te ekzekutimit</param>
        ///<returns>nje objekt datatable qe permban nje koleksion me te gjitha produktet e prodhimit te ketij dokumenti</returns>
        internal DataTable ktheGjitheProdukteProdhimiNgaKoka(int idkoka)
        {

                dbManager.Open();
                dbManager.CreateParameters(1);
                dbManager.AddParameters(0, "@IDKOKA", idkoka, ParameterDirection.Input);
                DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_PRODUKTPRODHIMI_selAllSipasKoka");
                return ds.Tables[0];

        }

        /// <summary>
        /// kthen datarow produkt prodhimi me kete id
        /// </summary>
        /// <param name="id">id ritese e produkt prodhimi</param>
        ///<returns>nje objekt datarow qe permban nje koleksion me produkt prodhimin me kete id  </returns>
        internal DataRow ktheProduktProdhimiSipasID(int id)
        {

                dbManager.Open();
                dbManager.CreateParameters(1);
                dbManager.AddParameters(0, "@ID", id, ParameterDirection.Input);
                DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_PRODUKTPRODHIMI_sel");
                if (ds == null)
                    return null;
                if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                    return null;
                return ds.Tables[0].Rows[0];

        }

        #endregion

        /// <summary>
        /// Metodat e meposhtme i perkasin veprimeve qe kryhen nga objektet clsRecepturaProdhimi dhe colRecepturaProdhimi
        /// </summary>
        #region RECEPTURA PRODHIMI

        /// <summary>
        /// ekzekuton prc_T_RECEPTURAPRODHIMI_ins ne DB dhe kthen statusin e perfundimit te ekzekutimit te SP-se perkatese
        /// Shiko clsMesazh brenda ketij moduli per informacion te metejshem mbi vleren e kthimit te kesaj metode
        /// </summary>
        /// <param name="idArt"> id e artikullit</param>
        /// <param name="idmag">id e magazines</param>
        /// <param name="idprodukti">id e produktit</param>
        /// <param name="id">id ritese e recepturave</param>
        /// <param name="idburimi">id e burimit</param>
        /// <param name="sasia"> sasia e  artikullit/burimit</param>
        /// <param name="kosto">kostoja e artikullit/burimit</param>
        /// <param name="scrap">scrapi</param>
        /// <param name="lloji">lloji artikull apo burim</param>
        /// <param name="sasiaakt">sasia aktuale</param>
        /// <returns> nje objekt clsMesazh qe tregon nese ruajtja eshte kryer ne rregull apo jo.</returns>
        /// <param name="idNjesia">id e njesise</param>
        /// <param name="firoperqindje">firoperqindje</param>
        internal clsMesazh ruajRecepturaProdhimi(out int id, int idprodukti, int idArt, int idburimi, double sasia, double scrap, double kosto, int idmag, int lloji, double sasiaakt, int idNjesia, double firoperqindje, int iddetajimi, int iddetajimi2)
        {
            id = -1;

                dbManager.Open();
                dbManager.CreateParameters(14);
                dbManager.AddParameters(0, "@ID", id, ParameterDirection.Output);
                dbManager.AddParameters(1, "@IDPRODUKTI", idprodukti, ParameterDirection.Input);
                if (idArt == 0 || idArt == -1)
                    dbManager.AddParameters(2, "@IDARTIKULLI", DBNull.Value, ParameterDirection.Input);
                else dbManager.AddParameters(2, "@IDARTIKULLI", idArt, ParameterDirection.Input);
                if (idburimi == 0 || idburimi == -1) dbManager.AddParameters(3, "@IDBURIMI", DBNull.Value, ParameterDirection.Input);
                else dbManager.AddParameters(3, "@IDBURIMI", idburimi, ParameterDirection.Input);
                dbManager.AddParameters(4, "@SASIA", sasia, ParameterDirection.Input);
                if (idmag == 0 || idmag == -1) dbManager.AddParameters(5, "@IDMAG", DBNull.Value, ParameterDirection.Input);
                else dbManager.AddParameters(5, "@IDMAG", idmag, ParameterDirection.Input);
                dbManager.AddParameters(6, "@SCRAP", scrap, ParameterDirection.Input);
                dbManager.AddParameters(7, "@KOSTO", kosto, ParameterDirection.Input);
                dbManager.AddParameters(8, "@LLOJI", lloji, ParameterDirection.Input);
                dbManager.AddParameters(9, "@SASIAAKT", sasiaakt, ParameterDirection.Input);
                dbManager.AddParameters(10, "@IDNJESIA", idNjesia, ParameterDirection.Input);
                dbManager.AddParameters(11, "@FIROPERQ", firoperqindje, ParameterDirection.Input);
            if (iddetajimi == 0 || iddetajimi == -1)
                dbManager.AddParameters(12, "@IDDETAJIMI", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(12, "@IDDETAJIMI", iddetajimi, ParameterDirection.Input);
            if (iddetajimi2 == 0 || iddetajimi2 == -1)
                dbManager.AddParameters(13, "@IDDETAJIMI2", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(13, "@IDDETAJIMI2", iddetajimi2, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_RECEPTURAPRODHIMI_ins");

                id = int.Parse(dbManager.Parameters[0].Value.ToString());

                clsMesazh mesazh = new clsMesazh(true, mesazhRuajtje);
                return mesazh;

        }
        internal clsMesazh ruajLidhesRecepturaSkedulim(out int id, int idreceptura, int idskedulim)
        {
            id = -1;

                dbManager.Open();
                dbManager.CreateParameters(3);
                dbManager.AddParameters(0, "@ID", id, ParameterDirection.Output);
                dbManager.AddParameters(1, "@IDRECEPTURAPRODHIMI", idreceptura, ParameterDirection.Input);
                dbManager.AddParameters(2, "@IDTRUPISKEDULIMPRODHIMI", idskedulim, ParameterDirection.Input);

                dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_LIDHESSKEDULIMEKZEKUTIM_ins");

                id = int.Parse(dbManager.Parameters[0].Value.ToString());

                clsMesazh mesazh = new clsMesazh(true, mesazhRuajtje);
                return mesazh;

        }
        /// <summary>
        /// ekzekuton prc_T_RECEPTURAPRODHIMI_upd ne DB dhe kthen statusin e perfundimit te ekzekutimit te SP-se perkatese
        /// Shiko clsMesazh brenda ketij moduli per informacion te metejshem mbi vleren e kthimit te kesaj metode
        /// </summary>
        /// <param name="idArt"> id e artikullit</param>
        /// <param name="idmag">id e magazines</param>
        /// <param name="idprodukti">id e produktit</param>
        /// <param name="id">id ritese e recepturave</param>
        /// <param name="idburimi">id e burimit</param>
        /// <param name="sasia"> sasia e  artikullit/burimit</param>
        /// <param name="kosto">kostoja e artikullit/burimit</param>
        /// <param name="scrap">scrapi</param>
        /// <param name="lloji">lloji artikull apo burim</param>
        /// <returns> nje objekt clsMesazh qe tregon nese modifikimi eshte kryer ne rregull apo jo.</returns>
        /// <param name="idnjesia">id e njesise</param>
        /// <param name="firoperqindje"></param>
        internal clsMesazh modifikoRecepturaProdhimi(int id, int idprodukti, int idArt, int idburimi, double sasia, double scrap, double kosto, int idmag, int lloji, double sasiaakt, int idnjesia, double firoperqindje, int iddetajimi, int iddetajimi2)
        {

                dbManager.Open();
                dbManager.CreateParameters(14);
                dbManager.AddParameters(0, "@ID", id, ParameterDirection.Input);
                dbManager.AddParameters(1, "@IDPRODUKTI", idprodukti, ParameterDirection.Input);
                if (idArt == 0 || idArt == -1)
                    dbManager.AddParameters(2, "@IDARTIKULLI", DBNull.Value, ParameterDirection.Input);
                else dbManager.AddParameters(2, "@IDARTIKULLI", idArt, ParameterDirection.Input);
                if (idburimi == 0 || idburimi == -1) dbManager.AddParameters(3, "@IDBURIMI", DBNull.Value, ParameterDirection.Input);
                else dbManager.AddParameters(3, "@IDBURIMI", idburimi, ParameterDirection.Input);
                dbManager.AddParameters(4, "@SASIA", sasia, ParameterDirection.Input);
                if (idmag == 0 || idmag == -1) dbManager.AddParameters(5, "@IDMAG", DBNull.Value, ParameterDirection.Input);
                else dbManager.AddParameters(5, "@IDMAG", idmag, ParameterDirection.Input);
                dbManager.AddParameters(6, "@SCRAP", scrap, ParameterDirection.Input);
                dbManager.AddParameters(7, "@KOSTO", kosto, ParameterDirection.Input);
                dbManager.AddParameters(8, "@LLOJI", lloji, ParameterDirection.Input);
                dbManager.AddParameters(9, "@SASIAAKT", sasiaakt, ParameterDirection.Input);
                dbManager.AddParameters(10, "@IDNJESIA", idnjesia, ParameterDirection.Input);
                dbManager.AddParameters(11, "@FIROPERQ", firoperqindje, ParameterDirection.Input);
            if (iddetajimi == 0 || iddetajimi == -1)
                dbManager.AddParameters(12, "@IDDETAJIMI", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(12, "@IDDETAJIMI", iddetajimi, ParameterDirection.Input);
            if (iddetajimi2 == 0 || iddetajimi2 == -1)
                dbManager.AddParameters(13, "@IDDETAJIMI2", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(13, "@IDDETAJIMI2", iddetajimi2, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_RECEPTURAPRODHIMI_upd");
                clsMesazh mesazh = new clsMesazh(true, mesazhModifikimi);
                return mesazh;

        }

        /// <summary>
        /// ekzekutohet sp-ja prc_T_RECEPTURAPRODHIMI_delSipasIDPRODUKTI duke i kaluar id e produktit 
        /// </summary>
        /// <param name="idprodukti">id produktit</param>
        /// <returns>nje objekt clsMesazh qe tregon nese fshirja eshte kryer ne rregull apo jo</returns>
        internal clsMesazh fshiRecepturaProdhimiSipasIdProdukti(int idprodukti)
        {

                dbManager.Open();
                dbManager.CreateParameters(1);
                dbManager.AddParameters(0, "@IDPRODUKTI", idprodukti, ParameterDirection.Input);
                dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_RECEPTURAPRODHIMI_delSipasIDPRODUKTI");
                clsMesazh mesazh = new clsMesazh(true, mesazhFshirje);
                return mesazh;

        }

        /// <summary>
        /// ekzekutohet sp-ja prc_T_RECEPTURAPRODHIMI_delSipasId duke i kaluar id e receptures
        /// </summary>
        /// <param name="id">id ritese receptures te prodhimit</param>
        /// <returns>nje objekt clsMesazh qe tregon nese fshirja eshte kryer ne rregull apo jo</returns>
        internal clsMesazh fshiRecepturaProdhimiSipasID(int id)
        {

                dbManager.Open();
                dbManager.CreateParameters(1);
                dbManager.AddParameters(0, "@ID", id, ParameterDirection.Input);
                dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_RECEPTURAPRODHIMI_delSipasId");
                clsMesazh mesazh = new clsMesazh(true, mesazhFshirje);
                return mesazh;

        }

        /// <summary>
        /// kthen datatable recepturat e prodhimit sipas id produkti
        /// </summary>
        /// <param name="idprodukti">id e produktit</param>
        ///<returns>nje objekt datatable qe permban nje koleksion me te gjitha recepturat e prodhimit te ketij produkti</returns>
        internal DataTable ktheGjitheRecepturatSipasProduktit(int idprodukti)
        {

                dbManager.Open();
                dbManager.CreateParameters(1);
                dbManager.AddParameters(0, "@IDPRODUKTI", idprodukti, ParameterDirection.Input);
                DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_RECEPTURAPRODHIMI_selAllSipasIdProdukti");
                return ds.Tables[0];

        }

        /// <summary>
        /// kthen datatable recepturat e prodhimit sipas se artikullit
        /// </summary>
        /// <param name="idartikulli">id e artikullit</param>
        ///<returns>nje objekt datatable qe permban nje koleksion me te gjitha recepturat e prodhimit te ketij artikulli</returns>
        internal DataTable ktheGjitheRecepturatSipasArtikullit(int idartikulli, DateTime data, int idplanifikimi, string sasiburimi)
        {

                dbManager.Open();
                dbManager.CreateParameters(4);
                dbManager.AddParameters(0, "@IDARTIKULLI", idartikulli, ParameterDirection.Input);
                dbManager.AddParameters(1, "@Data", data, ParameterDirection.Input);
                dbManager.AddParameters(2, "@idplanifikim", idplanifikimi, ParameterDirection.Input);
                dbManager.AddParameters(3, "@sasiburimi", sasiburimi, ParameterDirection.Input);
                DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_RECEPTURAPRODHIMI_selAllSipasIdArtikulli");
                return ds.Tables[0];

        }
        /// <summary>
        /// kthen datatable recepturat e prodhimit sipas id koka
        /// </summary>
        /// <param name="idkoka">id e kokes se dokumentit te ekzekutimit</param>
        ///<returns>nje objekt datatable qe permban nje koleksion me te gjitha recepturat e prodhimit te ketij dokumenti</returns>
        internal DataTable ktheGjitheRecepturaProdhimiNgaKoka(int idkoka)
        {

                dbManager.Open();
                dbManager.CreateParameters(1);
                dbManager.AddParameters(0, "@IDKOKA", idkoka, ParameterDirection.Input);
                DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_RECEPTURAPRODHIMI_selAllSipasIdKoka");
                return ds.Tables[0];

        }

        /// <summary>
        /// kthen datarow recepture prodhimi me kete id
        /// </summary>
        /// <param name="id">id ritese e receptures prodhimi</param>
        ///<returns>nje objekt datarow qe permban nje koleksion me recepture prodhimin me kete id  </returns>
        internal DataRow ktheReceptureProdhimiSipasID(int id)
        {

                dbManager.Open();
                dbManager.CreateParameters(1);
                dbManager.AddParameters(0, "@ID", id, ParameterDirection.Input);
                DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_RECEPTURAPRODHIMI_sel");
                if (ds == null)
                    return null;
                if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                    return null;
                return ds.Tables[0].Rows[0];

        }

        #endregion

        /// <summary>
        /// Metodat e meposhtme i perkasin veprimeve qe kryhen nga objektet clsPlanifikimEkzekutim dhe colPlanifikimEkzekutim
        /// </summary>
        #region PLANIFIKIM EKZEKUTIM

        /// <summary>
        /// ekzekuton prc_T_PLANIFIKIM_EKZEKUTIM_ins ne DB dhe kthen statusin e perfundimit te ekzekutimit te SP-se perkatese
        /// Shiko clsMesazh brenda ketij moduli per informacion te metejshem mbi vleren e kthimit te kesaj metode
        /// </summary> 
        /// <param name="id">id</param>
        /// <param name="idPlanifikimi">idDok te planifikimit</param>
        /// <param name="idEkzekutimi">idDok te ekzekutimit</param>
        /// <param name="idKonfigambjenteEkzekutimi"> id e konfigurimit te dokumentit te planifikimit</param>
        /// <param name="idKonfigambjetePlanifikuar"> id e konfigurimit te dokumentit te ekzekutimit</param>
        /// <returns> nje objekt clsMesazh qe tregon nese ruajtja eshte kryer ne rregull apo jo.</returns>
        internal clsMesazh ruajPlanifikimEkzekutim(int id, int idPlanifikimi, int idEkzekutimi, int idKonfigambjetePlanifikuar, int idKonfigambjenteEkzekutimi)
        {

                dbManager.Open();
                dbManager.CreateParameters(5);
                dbManager.AddParameters(0, "@ID", id, ParameterDirection.Output);
                dbManager.AddParameters(1, "@IDPLANIFIKIMI", idPlanifikimi, ParameterDirection.Input);
                dbManager.AddParameters(2, "@IDEKZEKUTIMI", idEkzekutimi, ParameterDirection.Input);
                dbManager.AddParameters(3, "@IDKONFIGPLANIFIKIMI", idKonfigambjetePlanifikuar, ParameterDirection.Input);
                dbManager.AddParameters(4, "@IDKONFIGEKZEKUTIMI", idKonfigambjenteEkzekutimi, ParameterDirection.Input);
                dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_PLANIFIKIM_EKZEKUTIM_ins");
                clsMesazh mesazh = new clsMesazh(true, mesazhRuajtje);
                return mesazh;

        }

        /// <summary>
        /// ekzekutohet sp-ja prc_T_PLANIFIKIM_EKZEKUTIM_delSipasIdPlanifikimi duke i kaluar id e dokumentit te planifikimit 
        /// </summary>
        /// <param name="idplanifikimi">id e dok te planifikimit </param>
        /// <returns>nje objekt clsMesazh qe tregon nese fshirja eshte kryer ne rregull apo jo</returns>
        internal clsMesazh fshiSipasIdPlanifikimi(int idplanifikimi)
        {

                dbManager.Open();
                dbManager.CreateParameters(1);
                dbManager.AddParameters(0, "@IDPLANIFIKIMI", idplanifikimi, ParameterDirection.Input);
                dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_PLANIFIKIM_EKZEKUTIM_delSipasIdPlanifikimi");
                clsMesazh mesazh = new clsMesazh(true, mesazhFshirje);
                return mesazh;

        }

        /// <summary>
        /// ekzekutohet sp-ja prc_T_PLANIFIKIM_EKZEKUTIM_updIdPlanifikimi duke i kaluar id e dokumentit te planifikimit 
        /// </summary>
        /// <param name="iddokplanifikimi">id e dokumentit te planfikimit </param>
        /// <param name="newiddokplanifikimi">id e re e dokumentit te planfikimit</param>
        /// <returns>nje objekt clsMesazh qe tregon nese modifikimi eshte kryer ne rregull apo jo</returns>
        internal clsMesazh modifikoSipasIdPlanifikimi(int iddokplanifikimi, int newiddokplanifikimi)
        {

                dbManager.Open();
                dbManager.CreateParameters(2);
                dbManager.AddParameters(0, "@IDPLANIFIKIMI", iddokplanifikimi, ParameterDirection.Input);
                dbManager.AddParameters(1, "@NEWIDPLANIFIKIMI", newiddokplanifikimi, ParameterDirection.Input);
                dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_PLANIFIKIM_EKZEKUTIM_updIdPlanifikimi");
                clsMesazh mesazh = new clsMesazh(true, mesazhModifikimi);
                return mesazh;

        }

        /// <summary>
        /// ekzekutohet sp-ja prc_T_PLANIFIKIM_EKZEKUTIM_delSipasIdEkzekutimi duke i kaluar id e dokumentit te ekzekutimit
        /// </summary>
        /// <param name="idekzekutimi">id e dokumentit te ekzekutimit </param>
        /// <returns>nje objekt clsMesazh qe tregon nese fshirja eshte kryer ne rregull apo jo</returns>
        internal clsMesazh fshiSipasIdEkzekutimi(int idekzekutimi)
        {

                dbManager.Open();
                dbManager.CreateParameters(1);
                dbManager.AddParameters(0, "@IDEKZEKUTIMI", idekzekutimi, ParameterDirection.Input);
                dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_PLANIFIKIM_EKZEKUTIM_delSipasIdEkzekutimi");
                clsMesazh mesazh = new clsMesazh(true, mesazhFshirje);
                return mesazh;

        }

        /// <summary>
        /// kthen dokumentat e  planifikimit te ketij dokumenti
        /// </summary>
        /// <param name="idekzekutimit">id e dokumentit te ekzekutimit</param>
        ///<returns>nje objekt me dokumentat e planifikimi te ketij dokumenti</returns>
        internal DataTable ktheDokPlanifikimiTeDokEkzekutimi(int idekzekutimit)
        {

                dbManager.Open();
                dbManager.CreateParameters(1);
                dbManager.AddParameters(0, "@IDEKZEKUTIMI", idekzekutimit, ParameterDirection.Input);
                DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_PLANIFIKIM_EKZEKUTIM_selSipasIdEkzekutimi");
                return ds.Tables[0];

        }

        #endregion

        /// <summary>
        /// Metodat e meposhtme i perkasin veprimeve qe kryhen nga objektet clsUrdherPorosiPlanifikim dhe colUrdherPorosiPlanifikim
        /// </summary>
        #region URDHERPOROSI PLANIFIKIM

        /// <summary>
        /// ekzekuton prc_T_URDHERPOROSI_PLANIFIKIM_ins ne DB dhe kthen statusin e perfundimit te ekzekutimit te SP-se perkatese
        /// Shiko clsMesazh brenda ketij moduli per informacion te metejshem mbi vleren e kthimit te kesaj metode
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="idurdher">id e urdher porosise</param>
        /// <param name="idplanifikim">id e planifikimit</param>
        /// <param name="idkonfigplanifikim"> id e konfigurimit te dokumentit te planifikimit</param>
        /// <param name="idkonfigurdher"> id e konfigurimit te dokumentit te urdherit</param>
        /// <returns> nje objekt clsMesazh qe tregon nese ruajtja eshte kryer ne rregull apo jo.</returns>
        internal clsMesazh ruajUrdherPorosiPlanifikim(int id, int idurdher, int idplanifikim, int idkonfigurdher, int idkonfigplanifikim)
        {

                dbManager.Open();
                dbManager.CreateParameters(5);
                dbManager.AddParameters(0, "@ID", id, ParameterDirection.Output);
                dbManager.AddParameters(1, "@IDURDHERPOROSIA", idurdher, ParameterDirection.Input);
                dbManager.AddParameters(2, "@IDPLANIFIKIMI", idplanifikim, ParameterDirection.Input);
                dbManager.AddParameters(3, "@IDKONFIGURDHER", idkonfigurdher, ParameterDirection.Input);
                dbManager.AddParameters(4, "@IDKONFIGPLANIFIKIMI", idkonfigplanifikim, ParameterDirection.Input);
                dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_URDHERPOROSI_PLANIFIKIM_ins");
                clsMesazh mesazh = new clsMesazh(true, mesazhRuajtje);
                return mesazh;

        }

        /// <summary>
        /// ekzekutohet sp-ja prc_T_URDHERPOROSI_PLANIFIKIM_delSipasIdUrdherPorosi duke i kaluar id e dokumentit te urdher porosise 
        /// </summary>
        /// <param name="idurdher"> id e urdherit </param>
        /// <returns>nje objekt clsMesazh qe tregon nese fshirja eshte kryer ne rregull apo jo</returns>
        internal clsMesazh fshiUrdherPorosiPlanifikimiSipasIdUrdheri(int idurdher)
        {

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDURDHERPOROSIA", idurdher, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_URDHERPOROSI_PLANIFIKIM_delSipasIdUrdherPorosi");
            clsMesazh mesazh = new clsMesazh(true, mesazhFshirje);
            return mesazh;

        }

        /// <summary>
        /// ekzekutohet sp-ja prc_T_URDHERPOROSI_PLANIFIKIM_updIdUrdherPorosia duke i kaluar id e urdher porosise
        /// </summary>
        /// <param name="idurdher">id e urdherit qe u modifikua </param>
        /// <param name="newidurdher">id e re e urdherit</param>
        /// <returns>nje objekt clsMesazh qe tregon nese modifikimi eshte kryer ne rregull apo jo</returns>
        internal clsMesazh modifikoUrdherPorosiPlanifikimIdUrdherin(int idurdher, int newidurdher)
        {

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDURDHERPOROSIA", idurdher, ParameterDirection.Input);
            dbManager.AddParameters(1, "@NEWIDURDHERPOROSIA", newidurdher, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_URDHERPOROSI_PLANIFIKIM_updIdUrdherPorosia");
            clsMesazh mesazh = new clsMesazh(true, mesazhModifikimi);
            return mesazh;

        }

        /// <summary>
        /// ekzekutohet sp-ja prc_T_URDHERPOROSI_PLANIFIKIM_delSipasIdPlanifikimi duke i kaluar id e planifikimit
        /// </summary>
        /// <param name="idplanifikim"> id e planifikimit </param>
        /// <returns>nje objekt clsMesazh qe tregon nese fshirja eshte kryer ne rregull apo jo</returns>
        internal clsMesazh fshiUrdherPorosiPlanifikimSipasIdPlanfikimi(int idplanifikim)
        {

                dbManager.Open();
                dbManager.CreateParameters(1);
                dbManager.AddParameters(0, "@IDPLANIFIKIMI", idplanifikim, ParameterDirection.Input);
                dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_URDHERPOROSI_PLANIFIKIM_delSipasIdPlanifikimi");
                clsMesazh mesazh = new clsMesazh(true, mesazhFshirje);
                return mesazh;

        }

        /// <summary>
        /// kthen dokumentat e urdher porosise te ketij planifikimi
        /// </summary>
        /// <param name="idplanifikimi">id e dokumentit te planifikimit</param>
        ///<returns>nje objekt me dokumentat e urdher porosise te ketij planifikimi</returns>
        internal DataTable ktheDokUrdherPorosiTePlanifikimir(int idplanifikimi)
        {

                dbManager.Open();
                dbManager.CreateParameters(1);
                dbManager.AddParameters(0, "@IDPLANIFIKIMI", idplanifikimi, ParameterDirection.Input);
                DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_URDHERPOROSI_PLANIFIKIM_selSipasIdPlanifikimi");
                return ds.Tables[0];

        }

        #endregion
    }
}
