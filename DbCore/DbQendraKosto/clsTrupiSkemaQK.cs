using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DbCore.DbQendraKosto
{
    /// <summary>
    ///  Kjo eshte klasa qe sherben per objektet qe perfaqsojne nje aktivitet trupi
    ///  (Te dhenat  merren nga tabela : T_TRUPISKEMAQK)
    /// </summary>
    public class clsTrupiSkemaQK
    {

        /// <summary>
        /// mesazh gabimi per te plotesuar qendren e kostos
        /// </summary>
        private const string STR_PlotesoniBurimit = "Plotesoni qendren e kostos!";
        /// <summary>
        /// mesazh gabimi kur qendra nuk ekziston
        /// </summary>
        private const string STR_BurimiNukEkziston = "Qendra e kostos nuk ekziston!";
        /// <summary>
        /// mesazh kur kontrollet kalohen me sukses
        /// </summary>
        private const string STR_KontrolletEAktivitetitUKaluanMeSukses = "Kontrollet e Skemes u kaluan me sukses";
        /// <summary>
        /// mesazh kur mbushja perfundon me sukses
        /// </summary>
        public static string mbushjeSukses = "Skema trupi u mbush me sukses";
        /// <summary>
        /// mesazh gabimi gjate marrjes se te dhenave
        /// </summary>
        public static string gabimNeTeDhena = "ERROR: Gabim gjate marrjes se Skema trupi nga db-ja";
        /// <summary>
        /// mesazh gabimi kur nuk merren te dhenat
        /// </summary>
        public static string drbosh = "Mbushja nuk u krye sepse nuk u morr asgje nga db-ja";
        #region Atribute
        /// <summary>
        /// id  ritese e trupit
        /// </summary>
        private int idTrupi;
        /// <summary>
        /// id e kokes
        /// </summary>
        private int idKoka;
        /// <summary>
        /// id e qendres se kosto
        /// </summary>
        private int idQK;
        /// <summary>
        ///  perqindja
        /// </summary>
        private decimal perqindja;
        /// <summary>
        /// kodi i qendres se kostos
        /// </summary>
        private string kodi;
        /// <summary>
        /// pershkrimi i qendres se kostos
        /// </summary>
        private string pershkrimi;
        private DataRow rreshti;


        #endregion

        #region Konstruktoret
        /// <summary>
        /// konstruktori pa parametra
        /// </summary>
        public clsTrupiSkemaQK()
        {
        }

        /// <summary>
        /// konstruktori me parameter
        /// </summary>
        /// <param name="idTrupi">id e trupit</param>
        /// <param name="idKoka"> id e kokes</param>
        /// <param name="idqk"> id e qendra kosto</param>
        /// <param name="perqindja">perqindja</param>

        public clsTrupiSkemaQK(int idTrupi, int idKoka, int idqk, decimal perqindja)
        {
            this.idTrupi = idTrupi;
            this.idKoka = idKoka;
            this.idQK = idqk;
            this.perqindja = perqindja;

        }

        /// <summary>
        /// konstruktori me parameter
        /// </summary>
        /// <param name="idtrupi">id e trupit</param>
        /// <param name="idkoka"> id e kokes</param>
        /// <param name="idburimi"> id e burimit</param>
        /// <param name="perqindja">koha e planifikuar</param>
        /// <param name="kodi"> kodi i burimit </param>
        /// <param name="pershkrim">pershkrimi i burimit</param>
        /// <param name="kostoburimi"> kostoja e burimit ne nje ore </param>
        /// <param name="kosto"> kostoja e burimit ne kohen e planifikuar</param>
        /// <param name="idndermarje"> id e ndermarjes</param>
        /// <param name="dtndryshimi"> data e ndryshimit</param>
        public clsTrupiSkemaQK(int idtrupi, int idkoka, int idqk, decimal perqindja, string kodi, string pershkrim,  int idndermarje)
        {
            try
            {
                idTrupi = idtrupi;
                idKoka = idkoka;
                idQK = idqk;
                this.perqindja = perqindja;
               this. kodi = kodi;
                pershkrimi = pershkrim;

                clsMesazh mesazh = kontrolloSkeme(idndermarje);
                if (!mesazh.Status)
                    throw new Exception(mesazh.PershkrimMesazhi);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// <summary>
        /// konstruktor me 1 parameter
        /// </summary>
        /// <param name="idtrupi">id e trupit</param>
        public clsTrupiSkemaQK(int idtrupi)
        {
            clsDatabaseQendraKosto db = new clsDatabaseQendraKosto();
            mbushTrupiSkema(db.ktheTrupiSkemaQK(idtrupi));
            db.Dispose();
        }

        public clsTrupiSkemaQK(DataRow rreshti)
        {
            
            mbushTrupiSkema(rreshti);
        }
        #endregion

        #region Properties
        /// <summary>
        /// id e trupit
        /// </summary>
        public int IdTrupi
        {
            get
            {
                return
                    idTrupi;
            }
            set
            {
                idTrupi = value;
            }
        }

        /// <summary>
        /// kodi 
        /// </summary>
        public string Kodi
        {
            get
            {
                return kodi;
            }
            set
            {
                kodi = value;
            }

        }

        /// <summary>
        /// id e kokes
        /// </summary>
        public int IdKoka
        {
            get
            {
                return idKoka;
            }
            set
            {
                idKoka = value;
            }
        }

        /// <summary>
        /// id e qender kosto
        /// </summary>
        public int IdQK
        {
            get
            {
                return idQK;
            }
            set
            {
                idQK = value;
            }
        }



        /// <summary>
        /// perqindja e qendres ne skeme
        /// </summary>
        public decimal Perqindja
        {
            get
            {
                return perqindja;
            }
            set
            {
                perqindja = value;
            }
        }

        /// <summary>
        /// pershkrimi i qk
        /// </summary>
        public string Pershkrimi
        {
            get
            {
                return pershkrimi;
            }
            set
            {
                pershkrimi = value;
            }
        }

     
        #endregion

        #region Metoda Publike

        /// <summary>
        /// kontroll nese objekti i aktivitietit i ka te dhenat e sakta 
        /// </summary>
        /// <param name="idndermarje">id e ndermarjes</param>
        /// <returns> clsMesazh  me statusin nese te dhenat jane te sakta apo jo</returns>
        private clsMesazh kontrolloSkeme(int idndermarje)
        {
            if (kodi == "")
                return new clsMesazh(false, STR_PlotesoniBurimit);
            if (perqindja < 0)
                return new clsMesazh(false, "Perqindja duhet te jete nje numer >0 dhe <100");
            if (!clsQendraKosto.ekzistonQK(kodi, idndermarje))
                return new clsMesazh(false, STR_BurimiNukEkziston);
            return new clsMesazh(true, STR_KontrolletEAktivitetitUKaluanMeSukses);
        }

        public clsMesazh ruaj()
        {
            clsDatabaseQendraKosto db = new clsDatabaseQendraKosto();
            clsMesazh mesazh = ruaj(db);
            db.Dispose();
            return mesazh;
        }

        /// <summary>
        /// ruan skemen trupin
        /// </summary>
        /// <param name="db"> clsDatabaseQendraKosto per te qene pjese e trasaksionit</param>
        /// <returns> cls mesazh qe tregon nqs eshte ruajtur apo jo skema</returns>
        public clsMesazh ruaj(clsDatabaseQendraKosto db)
        {
            clsMesazh mesazh = new clsMesazh();
          
            int idtrupi = 0;
            mesazh = db.ruajTrupiSkemaQK(out idtrupi, idKoka, idQK, perqindja);
            idTrupi = idtrupi;
            return mesazh;
        }

        /// <summary>
        /// modifikon skemen
        /// </summary>
        /// <returns> cls mesazh qe tregon nqs eshte ruajtur apo jo aktiviteti</returns>   
        public clsMesazh modifiko()
        {
            clsMesazh mesazh = new clsMesazh();
            clsDatabaseQendraKosto db = new clsDatabaseQendraKosto();
            mesazh = db.modifikoTrupiSkemaQK(idTrupi, idKoka, idQK, perqindja);
            db.Dispose();
            return mesazh;
        }

        public clsMesazh fshi()
        {
            clsDatabaseQendraKosto db = new clsDatabaseQendraKosto();
            clsMesazh mesazh = fshi(db);
            db.Dispose();
            return mesazh;
        }

        /// <summary>
        /// Fshin objektin skemen trupin ne tabelen perkatese ne databaze
        /// </summary>
        /// <param name="db">db nqs ben pjese ne nje transaksion</param>
        /// <returns > nje objekt clsMesazh qe tregon nese fshirja eshte kryer ne rregull apo jo</returns>
        public clsMesazh fshi(clsDatabaseQendraKosto db)
        {
            
            clsMesazh u_fshi = db.fshiTrupiSkemaQK(idTrupi);
            return u_fshi;
        }

        /// <summary>
        /// Merr objektin skemen trupi nga tabela perkatese ne databaze.
        /// </summary>
        public void merr()
        {
            clsDatabaseQendraKosto db = new clsDatabaseQendraKosto();            
            db.ktheTrupiSkemaQK(idTrupi);
            db.Dispose();
        }
        #endregion

        #region Metoda Internal

        /// <summary>
        /// mbush skema trupi me te dhenat nga databaza
        /// </summary>
        /// <param name="dbDataRow">rreshti me te dhena</param>
        /// <returns> kthen ne se mbushja u be ne rregull apo jo</returns>
        internal clsMesazh mbushTrupiSkema(DataRow dbDataRow)
        {
            if (dbDataRow != null)
            {
                try
                {
                    int.TryParse(dbDataRow["IDTRUPI"].ToString(), out idTrupi);
                    int.TryParse(dbDataRow["IDKOKA"].ToString(), out idKoka);
                    int.TryParse(dbDataRow["IDQK"].ToString(), out idQK);
                    decimal.TryParse(dbDataRow["PERQINDJA"].ToString(), out     perqindja);
                    kodi = dbDataRow["KODI"].ToString();
                    pershkrimi = dbDataRow["PERSHKRIMI"].ToString();

                    return new clsMesazh(true, mbushjeSukses);
                }
                catch (InvalidCastException)
                {
                    throw new Exception(gabimNeTeDhena);
                }
            }
            else
                return new clsMesazh(false, drbosh);
        }

        #endregion
    }
}
