using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DbCore.IMBUtils.DataBase;

namespace DbCore.DbTollona
{
    /// <summary>
    ///  Kjo eshte klasa me e rendesishme e ketij moduli. Eshte nje klase e tille per secilin modul te projektit.
    ///  Ketu ndodhen metodat qe therriten  nga te gjithe objektet brenda projektit DbProdhimi
    ///  secila metode permban thirjet e Store procedures ne DB, duke i kaluar parametrat perkates
    ///  Metodat jane te ndara ne Regions sipas objekteve qe i therrasin keto metoda
    /// </summary>
    public class clsDatabazeTollona : DbData
    {

        public clsDatabazeTollona():base()
        {
        }

        public clsDatabazeTollona(DbData db) : base(db) { }
        public clsDatabazeTollona(string connectionName) : base(connectionName)
        {

        }
        /// <summary>
        /// Metodat e meposhtme i perkasin veprimeve qe kryhen nga objektet clsArtikullTolloni dhe colArtikullTolloni
        /// </summary>
        #region ARTIKULLTOLLONI

        /// <summary>
        /// ekzekuton 	[prc_T_ARIKULLI_ins] ne DB dhe kthen statusin e perfundimit te ekzekutimit te SP-se perkatese
        /// Shiko clsMesazh brenda ketij moduli per informacion te metejshem mbi vleren e kthimit te kesaj metode
        /// </summary>
        /// <param name="idArtikull"> id e artikullit</param>
        /// <param name="kodi">kodi</param>
        /// <param name="pershkrimi">pershkrimi</param>
        /// <param name="idartwebinf">idartweb</param>
        /// <param name="aktiv"> aktive</param>
        /// <param name="idstatusdok">id e statusdok</param>
        /// <returns>nje objekt clsMesazh qe tregon nese ruajtja eshte kryer ne rregull apo jo.</returns>
        internal clsMesazh ruajArtTollon(out int idArtikull, string kodi, string pershkrimi, int idartwebinf, bool aktiv, int idstatusdok, string kodartikulliperberes, decimal koeficientartikulliperberes)
        {
            idArtikull = -1;
           
                dbManager.Open();
                dbManager.CreateParameters(8);
                dbManager.AddParameters(0, "@IDARTIKULLI", idArtikull, ParameterDirection.Output);
                dbManager.AddParameters(1, "@KODARTIKULLI", kodi, ParameterDirection.Input);
                dbManager.AddParameters(2, "@PERSHKRIMARTIKULLI", pershkrimi, ParameterDirection.Input);
                dbManager.AddParameters(3, "@AKTIV", aktiv, ParameterDirection.Input);
                dbManager.AddParameters(4, "@IDARTWEBINF", idartwebinf, ParameterDirection.Input);
                dbManager.AddParameters(5, "@IDSTATUSDOK", idstatusdok, ParameterDirection.Input);
                dbManager.AddParameters(6, "@KODARTIKULLIPERBERES", kodartikulliperberes, ParameterDirection.Input);
                dbManager.AddParameters(7, "@KOEFICIENTARTIKULLIPERBERES", koeficientartikulliperberes, ParameterDirection.Input);
                dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_ARIKULLITOLLONA_ins");
                idArtikull = int.Parse(dbManager.Parameters[0].Value.ToString());
                return new clsMesazh(true, mesazhRuajtje);

           
        }

        /// <summary>
        /// merr burimet te nje ndermarje me kete kod
        /// </summary>
        /// <param name="kodi"> kodi i burimit</param>
        /// <param name="idartweb"> id e ndermarjes</param>
        /// <returns>  data row me burimin te nje ndermarje me kete kod</returns>
        internal int ktheIdArtikulliTollonaSipasIdArtWeb( int idartweb)
        {
           
                dbManager.Open();
                dbManager.CreateParameters(1);

                dbManager.AddParameters(0, "@idartwebinf", idartweb, ParameterDirection.Input);
                using (DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_ARTIKULLITOLLONA_merrIdArtikullSipasIdWebInf"))
                {
                    if (ds == null)
                        return -1;
                    if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                        return -1;
                    int idArtikulli;
                    int.TryParse(ds.Tables[0].Rows[0]["IDARTIKULLI"].ToString(), out idArtikulli);
                    return idArtikulli;
                }
            
        }

        /// <summary>
        /// perdoret per te kontrolluar nqs ekziston artikulli ne db e tollonave
        /// </summary>
        /// <param name="idartwebinf"> id e artikullit ne webinf</param>
        /// <returns>nje objeckt boolean qe tregon nese ekziston apo jo artikulli tek db e tollonave</returns>
        public bool ekzistonArtikulli(int idartwebinf)
        {
          
                dbManager.Open();
                dbManager.CreateParameters(1);
                dbManager.AddParameters(0, "@idartwebinf", idartwebinf, ParameterDirection.Input);
                using (DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_ARTIKULLITOLLONA_ekzistonArtikull"))
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

      

        #endregion

        /// <summary>
        /// Metodat e meposhtme i perkasin veprimeve qe kryhen nga objektet clsShitjeMeSerial dhe colShitjeMeSerial
        /// </summary>
        #region SHITJEMESERIAL

        /// <summary>
        /// ekzekuton 	[prc_T_SHITJEMESERIAL_ins] ne DB dhe kthen statusin e perfundimit te ekzekutimit te SP-se perkatese
        /// Shiko clsMesazh brenda ketij moduli per informacion te metejshem mbi vleren e kthimit te kesaj metode
        /// </summary>
        /// <param name="id"> id </param>
        /// <param name="seriali">seriali</param>
        /// <param name="sasia">sasia</param>
        /// <param name="cmimi">cmimi</param>
        /// <param name="dtfillimi">dtfillimi</param>
        /// <param name="idtrupishitje">id e trupit te shitjes ne web</param>
        /// <param name="idartikulli">id e artikullit</param>
        /// <param name="dtmbarimi">dt mbarimi</param>
   
        /// <param name="idstatusdok">id e statusdok</param>
        /// <returns>nje objekt clsMesazh qe tregon nese ruajtja eshte kryer ne rregull apo jo.</returns>

        internal clsMesazh ruajShitjeMeSerial(out int id, string seriali, double sasia, double cmimi, int idtrupishitje, DateTime dtfillimi,DateTime dtmbarimi, int idartikulli,  int idstatusdok)
        {
            id = 0;
            
                dbManager.Open();
                dbManager.CreateParameters(9);
                dbManager.AddParameters(0, "@ID", id, ParameterDirection.Output);
                dbManager.AddParameters(1, "@SERIALI", seriali, ParameterDirection.Input);
                dbManager.AddParameters(2, "@SASIA", sasia, ParameterDirection.Input);
                dbManager.AddParameters(3, "@CMIMI", cmimi, ParameterDirection.Input);
                dbManager.AddParameters(4, "@IDTRUPISHITJE", idtrupishitje, ParameterDirection.Input);
                dbManager.AddParameters(5, "@DTFILLIMI", dtfillimi, ParameterDirection.Input);
                dbManager.AddParameters(6, "@IDARTIKULLI", idartikulli, ParameterDirection.Input);
                dbManager.AddParameters(7, "@DTMBARRIMI", dtmbarimi, ParameterDirection.Input);
                dbManager.AddParameters(8, "@IDSTATUSDOK", idstatusdok, ParameterDirection.Input);
                dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_SHITJEMESERIALTOLLONA_ins");
                id = int.Parse(dbManager.Parameters[0].Value.ToString());
                return new clsMesazh(true, mesazhRuajtje);

            
        }

   
        /// <summary>
        /// fshin aktivitet  duke ndryshuar statusin e saj ne te fshire
        /// </summary>
        /// <param name="idkoka">id e kokes</param>
        /// <param name="idperdoruesi"> id e perdoruesit qe ka kryer veprimin</param>
        /// <returns> clsMesazh qe tregon nese fshirja u krye me sukses apo jo</returns>
        internal clsMesazh ndryshoStatusSeriali(int id, int idstatusi)
        {
           
                dbManager.Open();
                dbManager.CreateParameters(2);
                dbManager.AddParameters(0, "@ID", id, ParameterDirection.Input);
                dbManager.AddParameters(1, "@IDSTATUSDOK", idstatusi, ParameterDirection.Input);
                dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_SHITJEMESERIALTOLLONA_updstatus");
                clsMesazh mesazh = new clsMesazh(true, mesazhFshirje);
                return mesazh;
           
        }

  
        /// <summary>
        /// merr aktivitetin te nje ndermarje me kete kod
        /// </summary>
        /// <param name="kodi"> kodi i aktivitetit</param>
        /// <param name="idnder"> id e ndermarjes</param>
        /// <returns>  data row me aktivitetin te nje ndermarje me kete kod</returns>
        internal DataRow ktheShitjeMeSerialSipasSerialitDheArtikullit(int idartikulli, string seriali)
        {
            
                dbManager.Open();

                dbManager.CreateParameters(2);
                dbManager.AddParameters(0, "@idartikull", idartikulli, ParameterDirection.Input);
                dbManager.AddParameters(1, "@seriali", seriali, ParameterDirection.Input);
                using (DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_SHITJEMESERIALTOLLONA_merrTeDhenaSerialiSipasKodSerialiDheIdArtikulli"))
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
        public bool kaTollonaShitja(int idkokashitje)
        {

                dbManager.Open();
                dbManager.CreateParameters(1);
                dbManager.AddParameters(0, "@idkokashitje", idkokashitje, ParameterDirection.Input);
                using (DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_TRUPISHITJETOLLONA_kaTollonaShitja"))
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
        public bool ekzistonSeriali( string seriali)
        {
           
                dbManager.Open();
                dbManager.CreateParameters(1);
        
                dbManager.AddParameters(0, "@seriali", seriali, ParameterDirection.Input);
                using (DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_SHITJEMESERIALTOLLONA_ekzistonSerialSipasArtikullit"))
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
        /// merr gjithe aktivitetet sipas ndermarjes ne forme data table
        /// </summary>
        /// <param name="idnderm">id ndermarje</param>
        /// <returns> kthen nje datatable me te gjithe aktivitet e kesaj ndermarje</returns>
        internal DataTable merrShitjeMeSerialKonsumuaraPerImport(int idnderm,DateTime date)
        {

                dbManager.Open();
                dbManager.CreateParameters(2);
                dbManager.AddParameters(0, "@IDNDERMARJE", idnderm, ParameterDirection.Input); 
                dbManager.AddParameters(1, "@date", date, ParameterDirection.Input);
                using (DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_SHITJEMESERIALTOLLONA_merrKonsumimePerImport"))
                {
                    return ds.Tables[0];
                }

        } 
        internal clsMesazh merrShitjeMeSerialKonsumuarandryshoCmimin(DateTime date)
        {

                dbManager.Open();
                dbManager.CreateParameters(1);
                dbManager.AddParameters(0, "@date", date, ParameterDirection.Input);
                dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_SHITJEMESERIAL_updateCmim");
                clsMesazh mesazh = new clsMesazh(true, mesazhFshirje);
                return mesazh;

           
        }
        internal DataTable merrShitjeMeSerialPerRaport(int idnderm)
        {

                dbManager.Open();
                dbManager.CreateParameters(1);
                dbManager.AddParameters(0, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
                using (DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_SHITJEMESERIALTOLLONA_raportiPerTollonat"))
                {
                    return ds.Tables[0];
                }

        }
        internal DataTable merrShitjeMeSerialTrupiKonsumuaraPerImport(DateTime dtdok, string pikeshitje)
        {

                dbManager.Open();
                dbManager.CreateParameters(2);
                dbManager.AddParameters(0, "@dtdok", dtdok, ParameterDirection.Input);
                dbManager.AddParameters(1, "@pikeshitje", pikeshitje, ParameterDirection.Input);
                using (DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_SHITJEMESERIALTOLLONA_merrKonsumimeTrupiPerImport"))
                {
                    return ds.Tables[0];
                }

        }
        #endregion
        /// <summary>
        /// Metodat e meposhtme i perkasin veprimeve qe kryhen nga objektet clsShitjeMeSerial dhe colShitjeMeSerial
        /// </summary>
        #region TOLLONALETER

        /// <summary>
        /// ekzekuton 	[prc_T_TOLLONALETER_ins] ne DB dhe kthen statusin e perfundimit te ekzekutimit te SP-se perkatese
        /// Shiko clsMesazh brenda ketij moduli per informacion te metejshem mbi vleren e kthimit te kesaj metode
        /// </summary>
        /// <param name="id"> id </param>
        /// <param name="seriali">seriali</param>
        /// <param name="sasia">sasia</param>
        /// <param name="cmimi">cmimi</param>
        /// <param name="dtfillimi">dtfillimi</param>
        /// <param name="idtrupishitje">id e trupit te shitjes ne web</param>
        /// <param name="idartikulli">id e artikullit</param>
        /// <param name="dtmbarimi">dt mbarimi</param>
   
        /// <param name="idstatusdok">id e statusdok</param>
        /// <returns>nje objekt clsMesazh qe tregon nese ruajtja eshte kryer ne rregull apo jo.</returns>

        internal clsMesazh ruajTollonaLeter(out int id, string fillimseriali, double totalilitra, string mbarimseriali, int idtrupishitje,string kodklienti, string pershkrimklienti, int idartikulli,  bool statusi)
        {
            id = 0;
            
                dbManager.Open();
                dbManager.CreateParameters(9);
                dbManager.AddParameters(0, "@ID", id, ParameterDirection.Output);
                dbManager.AddParameters(1, "@FILLIMSERIALI", fillimseriali, ParameterDirection.Input);
                dbManager.AddParameters(2, "@TOTALILITRA", totalilitra, ParameterDirection.Input);
                dbManager.AddParameters(3, "@MBARIMSERIALI", mbarimseriali, ParameterDirection.Input);
                dbManager.AddParameters(4, "@IDTRUPISHITJE", idtrupishitje, ParameterDirection.Input);
                dbManager.AddParameters(5, "@KODKLIENTI", kodklienti, ParameterDirection.Input);
                dbManager.AddParameters(6, "@IDARTIKULLI", idartikulli, ParameterDirection.Input);
                dbManager.AddParameters(7, "@PERSHKRIMKLIENTI", pershkrimklienti, ParameterDirection.Input);
                dbManager.AddParameters(8, "@STATUS", statusi, ParameterDirection.Input);
                dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_TOLLONALETER_ins");
                id = int.Parse(dbManager.Parameters[0].Value.ToString());
                return new clsMesazh(true, mesazhRuajtje);

            
        } 
        internal clsMesazh ruajTollonaLeterKonsumimi(out int id, string seriali,int perdorur, DateTime dateperdorimi,string kodstacioni,string pershkrimstacioni, double sasia, double cmimi, double vlefta, string kodklienti, string pershkrimklienti,  bool statusi)
        {
                id = 0;
            
                dbManager.Open();
                dbManager.CreateParameters(12);
                dbManager.AddParameters(0, "@ID", id, ParameterDirection.Output);
                dbManager.AddParameters(1, "@seriali", seriali, ParameterDirection.Input);
                dbManager.AddParameters(2, "@perdorur", perdorur, ParameterDirection.Input);
                dbManager.AddParameters(3, "@dataperdorimi", dateperdorimi, ParameterDirection.Input);
                dbManager.AddParameters(4, "@kodstacioni", kodstacioni, ParameterDirection.Input);
                dbManager.AddParameters(5, "@pershkrimstacioni", pershkrimstacioni, ParameterDirection.Input);
                dbManager.AddParameters(6, "@sasia", sasia, ParameterDirection.Input);
                dbManager.AddParameters(7, "@cmimi", cmimi, ParameterDirection.Input);
                dbManager.AddParameters(8, "@vlefta", vlefta, ParameterDirection.Input);
                dbManager.AddParameters(9, "@kodklienti", kodklienti, ParameterDirection.Input);
                dbManager.AddParameters(10, "@pershkrimklienti", pershkrimklienti, ParameterDirection.Input);
                dbManager.AddParameters(11, "@status", statusi, ParameterDirection.Input);
                dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_KONSUMIMTOLLONALETER_ins");
                id = int.Parse(dbManager.Parameters[0].Value.ToString());
                return new clsMesazh(true, mesazhRuajtje);

            
        }

        /// <summary>
        /// fshin aktivitet  duke ndryshuar statusin e saj ne te fshire
        /// </summary>
        /// <param name="idkoka">id e kokes</param>
        /// <param name="idperdoruesi"> id e perdoruesit qe ka kryer veprimin</param>
        /// <returns> clsMesazh qe tregon nese fshirja u krye me sukses apo jo</returns>
        internal clsMesazh ndryshoStatusTolloniLeter(string pikeshitje,DateTime dtdok, bool status)
        {

            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@pikeshitje", pikeshitje, ParameterDirection.Input);
            dbManager.AddParameters(1, "@dtdok", dtdok, ParameterDirection.Input);
            dbManager.AddParameters(2, "@status", status, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_KONSUMIMTOLLONALETER_ndryshoStatus");
            clsMesazh mesazh = new clsMesazh(true, mesazhFshirje);
            return mesazh;

        }


        ///// <summary>
        ///// merr aktivitetin te nje ndermarje me kete kod
        ///// </summary>
        ///// <param name="kodi"> kodi i aktivitetit</param>
        ///// <param name="idnder"> id e ndermarjes</param>
        ///// <returns>  data row me aktivitetin te nje ndermarje me kete kod</returns>
        //internal DataRow ktheShitjeMeSerialSipasSerialitDheArtikullit(int idartikulli, string seriali)
        //{
            
        //        dbManager.Open();

        //        dbManager.CreateParameters(2);
        //        dbManager.AddParameters(0, "@idartikull", idartikulli, ParameterDirection.Input);
        //        dbManager.AddParameters(1, "@seriali", seriali, ParameterDirection.Input);
        //        using (DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_SHITJEMESERIALTOLLONA_merrTeDhenaSerialiSipasKodSerialiDheIdArtikulli"))
        //        {
        //            if (ds == null)
        //                return null;
        //            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
        //                return null;
        //            return ds.Tables[0].Rows[0];
        //        }
           
        //}

        /// <summary>
        /// perdoret per te kontrolluar nqs ekziston nje aktivitet me kete kod 
        /// behet kontrolli per qellime konsistence te informacionit ne DB, nuk lejon te celen aktivitete te ndryshem me te njejtin kod
        /// </summary>
        /// <param name="kodi">kodi i aktivitetit</param>
        /// <param name="idNdermarje"> id e ndermarjes</param>
        /// <returns>nje objeckt boolean qe tregon nese ekziston apo jo nje aktivitet me kete kod</returns>
        public bool kaTollonaShitjaKastrati(int idkokashitje)
        {

                dbManager.Open();
                dbManager.CreateParameters(1);
                dbManager.AddParameters(0, "@idkokashitje", idkokashitje, ParameterDirection.Input);
                using (DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_TRUPISHITJETOLLONA_kaTollonaShitjaKastrati"))
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
        public bool ekzistonSerialiKastrati(string fillseriali, string mbarimserial)
        {

            dbManager.Open();
            dbManager.CreateParameters(2);

            dbManager.AddParameters(0, "@fillimseriali", fillseriali, ParameterDirection.Input);
            dbManager.AddParameters(1, "@mbarimseriali", mbarimserial, ParameterDirection.Input);
            using (DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_TOLLONALETER_ekzistonSerialSipasArtikullit"))
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
        public bool eshteKonsumuarSerialiLeter(string seriali)
        {

            dbManager.Open();
            dbManager.CreateParameters(1);

            dbManager.AddParameters(0, "@seriali", seriali, ParameterDirection.Input);

            using (DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KONSUMIMTOLLONALETER_eshteKonsumuarTolloni"))
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
        /// merr gjithe aktivitetet sipas ndermarjes ne forme data table
        /// </summary>
        /// <param name="idnderm">id ndermarje</param>
        /// <returns> kthen nje datatable me te gjithe aktivitet e kesaj ndermarje</returns>
        internal DataTable merrTollonaLeterKonsumuaraPerImport(int idnderm, DateTime date)
        {

                dbManager.Open();
                dbManager.CreateParameters(2);
                dbManager.AddParameters(0, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
                dbManager.AddParameters(1, "@date", date, ParameterDirection.Input);
                using (DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KONSUMIMTOLLONALETER_merrKonsumimePerImport"))
                {
                    return ds.Tables[0];
                }

        }
        internal DataTable merrTollonaLeterPerRaport(int idnderm, DateTime dtfillimi, DateTime dtmbarimi, DateTime dtfillimiexe, DateTime dtmbarimiexe)
        {

                dbManager.Open();
                dbManager.CreateParameters(5);
                dbManager.AddParameters(0, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
                dbManager.AddParameters(1, "@dtfillimi", dtfillimi, ParameterDirection.Input);
                dbManager.AddParameters(2, "@dtmbarimi", dtmbarimi, ParameterDirection.Input);
                dbManager.AddParameters(3, "@dtfillimiexe", dtfillimiexe, ParameterDirection.Input);
                dbManager.AddParameters(4, "@dtmbarimiexe", dtmbarimiexe, ParameterDirection.Input);
                using (DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_TOLLONALETER_raportiPerTollonat"))
                {
                    return ds.Tables[0];
                }

        }
        internal DataTable merrTollonaLeterTrupiKonsumuaraPerImport(DateTime dtdok, string pikeshitje, int idndermarje)
        {

                dbManager.Open();
                dbManager.CreateParameters(3);
                dbManager.AddParameters(0, "@dtdok", dtdok, ParameterDirection.Input);
                dbManager.AddParameters(1, "@pikeshitje", pikeshitje, ParameterDirection.Input);  
                dbManager.AddParameters(2, "@idndermarje", idndermarje, ParameterDirection.Input);
                using (DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KONSUMIMTOLLONALETER_merrKonsumimeTrupiPerImport"))
                {
                    return ds.Tables[0];
                }

        }
        #endregion

        /// <summary>
        /// Metodat e meposhtme i perkasin veprimeve qe kryhen nga objektet clsShitjeMeSerial dhe colShitjeMeSerial
        /// </summary>
        #region TOLLONAELEKTRONIK

        /// <summary>
        /// ekzekuton 	[prc_T_tollonaELEKTRONIK_ins] ne DB dhe kthen statusin e perfundimit te ekzekutimit te SP-se perkatese
        /// Shiko clsMesazh brenda ketij moduli per informacion te metejshem mbi vleren e kthimit te kesaj metode
        /// </summary>
        /// <param name="id"> id </param>
        /// <param name="seriali">seriali</param>
        /// <param name="sasia">sasia</param>
        /// <param name="cmimi">cmimi</param>
        /// <param name="dtfillimi">dtfillimi</param>
        /// <param name="idtrupishitje">id e trupit te shitjes ne web</param>
        /// <param name="idartikulli">id e artikullit</param>
        /// <param name="dtmbarimi">dt mbarimi</param>

        /// <param name="idstatusdok">id e statusdok</param>
        /// <returns>nje objekt clsMesazh qe tregon nese ruajtja eshte kryer ne rregull apo jo.</returns>

        internal clsMesazh ruajTollonaElektronik(out int id, string kodartikulli, double totalilitra,  int idtrupishitje, string kodklienti, string pershkrimklienti, int idartikulli, bool statusi)
        {
            id = 0;

            dbManager.Open();
            dbManager.CreateParameters(8);
            dbManager.AddParameters(0, "@ID", id, ParameterDirection.Output);
            dbManager.AddParameters(1, "@KODARTIKULLI", kodartikulli, ParameterDirection.Input);
            dbManager.AddParameters(2, "@TOTALILITRA", totalilitra, ParameterDirection.Input);
            dbManager.AddParameters(3, "@IDTRUPISHITJE", idtrupishitje, ParameterDirection.Input);
            dbManager.AddParameters(4, "@KODKLIENTI", kodklienti, ParameterDirection.Input);
            dbManager.AddParameters(5, "@IDARTIKULLI", idartikulli, ParameterDirection.Input);
            dbManager.AddParameters(6, "@PERSHKRIMKLIENTI", pershkrimklienti, ParameterDirection.Input);
            dbManager.AddParameters(7, "@STATUS", statusi, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_tollonaELEKTRONIK_ins");
            id = int.Parse(dbManager.Parameters[0].Value.ToString());
            return new clsMesazh(true, mesazhRuajtje);


        }

        /// <summary>
        /// fshin aktivitet  duke ndryshuar statusin e saj ne te fshire
        /// </summary>
        /// <param name="idkoka">id e kokes</param>
        /// <param name="idperdoruesi"> id e perdoruesit qe ka kryer veprimin</param>
        /// <returns> clsMesazh qe tregon nese fshirja u krye me sukses apo jo</returns>
        internal clsMesazh ndryshoStatusTollonElektronik(DateTime dtdok, string pikeshitje, bool status)
        {

            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@dtdok", dtdok, ParameterDirection.Input);
            dbManager.AddParameters(1, "@pikeshitje", pikeshitje, ParameterDirection.Input);
            dbManager.AddParameters(2, "@status", status, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_KONSUMIMTOLLONAELEKTRONIK_ndryshoStatus");
            clsMesazh mesazh = new clsMesazh(true, mesazhFshirje);
            return mesazh;

        }
    
        internal clsMesazh ndryshoStatusTollonElektronikSpecifik(DateTime dtdok, string pikeshitje, bool status, string kodklienti)
        {

            dbManager.Open();
            dbManager.CreateParameters(4);
            dbManager.AddParameters(0, "@dtdok", dtdok, ParameterDirection.Input);
            dbManager.AddParameters(1, "@pikeshitje", pikeshitje, ParameterDirection.Input);
            dbManager.AddParameters(2, "@status", status, ParameterDirection.Input);    
            dbManager.AddParameters(3, "@kodklienti", kodklienti, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_KONSUMIMTOLLONAELEKTRONIK_ndryshoStatusSpecifik");
            clsMesazh mesazh = new clsMesazh(true, mesazhFshirje);
            return mesazh;

        }

        ///// <summary>
        ///// merr aktivitetin te nje ndermarje me kete kod
        ///// </summary>
        ///// <param name="kodi"> kodi i aktivitetit</param>
        ///// <param name="idnder"> id e ndermarjes</param>
        ///// <returns>  data row me aktivitetin te nje ndermarje me kete kod</returns>
        //internal DataRow ktheShitjeMeSerialSipasSerialitDheArtikullit(int idartikulli, string seriali)
        //{

        //        dbManager.Open();

        //        dbManager.CreateParameters(2);
        //        dbManager.AddParameters(0, "@idartikull", idartikulli, ParameterDirection.Input);
        //        dbManager.AddParameters(1, "@seriali", seriali, ParameterDirection.Input);
        //        using (DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_SHITJEMESERIALTOLLONA_merrTeDhenaSerialiSipasKodSerialiDheIdArtikulli"))
        //        {
        //            if (ds == null)
        //                return null;
        //            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
        //                return null;
        //            return ds.Tables[0].Rows[0];
        //        }

        //}

        /// <summary>
        /// perdoret per te kontrolluar nqs ekziston nje aktivitet me kete kod 
        /// behet kontrolli per qellime konsistence te informacionit ne DB, nuk lejon te celen aktivitete te ndryshem me te njejtin kod
        /// </summary>
        /// <param name="kodi">kodi i aktivitetit</param>
        /// <param name="idNdermarje"> id e ndermarjes</param>
        /// <returns>nje objeckt boolean qe tregon nese ekziston apo jo nje aktivitet me kete kod</returns>
        public bool kaTollonaShitjaKastratiElektronik(int idkokashitje)
        {

                dbManager.Open();
                dbManager.CreateParameters(1);
                dbManager.AddParameters(0, "@idkokashitje", idkokashitje, ParameterDirection.Input);
                using (DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_TRUPISHITJETOLLONAELEKTRONIK_kaTollonaShitjaKastrati"))
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
        /// merr gjithe aktivitetet sipas ndermarjes ne forme data table
        /// </summary>
        /// <param name="idnderm">id ndermarje</param>
        /// <returns> kthen nje datatable me te gjithe aktivitet e kesaj ndermarje</returns>
        internal DataTable merrTollonaElektronikKonsumuaraPerImport(int idnderm, DateTime date)
        {

                dbManager.Open();
                dbManager.CreateParameters(2);
                dbManager.AddParameters(0, "@IDNDERMARJE", idnderm, ParameterDirection.Input); 
                dbManager.AddParameters(1, "@date", date, ParameterDirection.Input);
                using (DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KONSUMIMTOLLONAELEKTRONIK_merrKonsumimePerImport"))
                {
                    return ds.Tables[0];
                }

        } 
        internal DataTable merrTollonaElektronikKonsumuaraPerImportSpecifik(int idnderm, DateTime date)
        {

                dbManager.Open();
                dbManager.CreateParameters(2);
                dbManager.AddParameters(0, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
                dbManager.AddParameters(1, "@date", date, ParameterDirection.Input);
                using (DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KONSUMIMTOLLONAELEKTRONIK_merrKonsumimePerImportSpecifik"))
                {
                    return ds.Tables[0];
                }

        }
       
        internal DataTable merrTollonaElektronikTrupiKonsumuaraPerImport(DateTime dtdok, string pikeshitje)
        {

                dbManager.Open();
                dbManager.CreateParameters(2);
                dbManager.AddParameters(0, "@dtdok", dtdok, ParameterDirection.Input);
                dbManager.AddParameters(1, "@pikeshitje", pikeshitje, ParameterDirection.Input);
                using (DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KONSUMIMTOLLONAELEKTRONIK_merrKonsumimeTrupiPerImport"))
                {
                    return ds.Tables[0];
                }
        } 
        internal DataTable merrTollonaElektronikTrupiKonsumuaraPerImportSpecifik(DateTime dtdok, string pikeshitje,string kodklienti)
        {

                dbManager.Open();
                dbManager.CreateParameters(3);
                dbManager.AddParameters(0, "@dtdok", dtdok, ParameterDirection.Input);
                dbManager.AddParameters(1, "@pikeshitje", pikeshitje, ParameterDirection.Input); 
                dbManager.AddParameters(2, "@kodklienti", kodklienti, ParameterDirection.Input);
                using (DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KONSUMIMTOLLONAELEKTRONIK_merrKonsumimeTrupiPerImportSpecifik"))
                {
                    return ds.Tables[0];
                }

        }
        #endregion

        /// <summary>
        /// Metodat e meposhtme i perkasin veprimeve qe kryhen nga objektet clsAktiviteteTrupi dhe colAktiviteteTrupi
        /// </summary>
        #region PERDORUESI

        /// <summary>
        /// ekzekuton 	[prc_T_PERDORUESITOLLONA_ins] ne DB dhe kthen statusin e perfundimit te ekzekutimit te SP-se perkatese
        /// Shiko clsMesazh brenda ketij moduli per informacion te metejshem mbi vleren e kthimit te kesaj metode
        /// </summary>
        /// <param name="idperdoruesi"> id e trupit</param>
        /// <param name="idkoka">id e kokes</param>
        /// <param name="idburimi">id e burimit</param>
        /// <param name="koha">koha</param>
        /// <returns>nje objekt clsMesazh qe tregon nese ruajtja eshte kryer ne rregull apo jo.</returns>
        /// <param name="dtNdryshimi">data e ndryshimit</param>
        internal clsMesazh ruajPerdorues(out int idperdoruesi, string emer, string mbiemer , string username, string password, bool admin, bool passperkohshem,bool aktiv,int idstatusdok)
        {
            idperdoruesi = -1;

                dbManager.Open();
                dbManager.CreateParameters(9);
                dbManager.AddParameters(0, "@IDPERDORUESI", idperdoruesi, ParameterDirection.Output);
                dbManager.AddParameters(1, "@EMER", emer, ParameterDirection.Input);
                dbManager.AddParameters(2, "@MBIEMER", mbiemer, ParameterDirection.Input);
                dbManager.AddParameters(3, "@USERNAME", username, ParameterDirection.Input);
                dbManager.AddParameters(4, "@PASSWORD", password, ParameterDirection.Input);
                dbManager.AddParameters(5, "@ADMIN", admin, ParameterDirection.Input);
                dbManager.AddParameters(6, "@PASSPERKOHSHEM", passperkohshem, ParameterDirection.Input);
                dbManager.AddParameters(7, "@IDSTATUSDOK", idstatusdok, ParameterDirection.Input);
                dbManager.AddParameters(8, "@AKTIV", aktiv, ParameterDirection.Input);
                dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_PERDORUESITOLLONA_ins");
                idperdoruesi = int.Parse(dbManager.Parameters[0].Value.ToString());
                return new clsMesazh(true, mesazhRuajtje);

        }
        public bool ekzistonPerdorues(string username)
        {

                dbManager.Open();
                dbManager.CreateParameters(1);
                dbManager.AddParameters(0, "@username", username, ParameterDirection.Input);
                using (DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_PERDORUESITOLLONA_ekzistonPerdorues"))
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

 
        #endregion

      

    }
}
