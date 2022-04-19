using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DbCore.DbQendraKosto
{
    /// <summary>
    ///  Kjo eshte klasa qe sherben per objektet qe perfaqsojne nje burim
    ///  (Te dhenat  merren nga tabela : T_LLOGARISHPERNDARJEQK)
    /// </summary>
    public class clsLlogariShperndarjeQK
    {
        /// <summary>
        /// mesazh kur burimi u mbush me sukses
        /// </summary>
        public static string mbushjeSukses = "Llogaria e shperndarje qendra kosto u mbush me sukses";
        /// <summary>
        /// mesazh gabimi kur merren te dhenat
        /// </summary>
        public static string gabimNeTeDhena = "ERROR: Gabim gjate marrjes se llogari shperndarje qendra kosto nga db-ja";
        /// <summary>
        /// mesazh gabimi kur nuk merret asnje e dhene
        /// </summary>
        public static string drbosh = "Mbushja nuk u krye sepse nuk u morr asgje nga db-ja";
        #region Atribute
        /// <summary>
        /// id e llogarise
        /// </summary>
        private int idLlogariShperndarje;
        /// <summary>
        /// id e kpf
        /// </summary>
        private int idKPF;
        /// <summary>
        /// id e ndermarjes
        /// </summary>
        private int idNdermarje;
        /// <summary>
        /// id e perdoruesit
        /// </summary>
        private int idPerdoruesi;
        /// <summary>
        /// emertimi i kpf
        /// </summary>
        private string emertimiKPF;
        /// <summary>
        /// niveli i kpf
        /// </summary>
        private int niveliKPF;
        /// <summary>
        /// prindi i kpf
        /// </summary>
        private string prindi;
        private DataRow rreshti;

        #endregion

        #region Konstruktoret
        /// <summary>
        /// konstruktori pa parametra
        /// </summary>
        public clsLlogariShperndarjeQK()
        {
        }

        /// <summary>
        /// konstruktori me parameter
        /// </summary>
        /// <param name="idllogari">id e llogarise</param>
        /// <param name="idkpf"> id e kpf</param>
        /// <param name="idPerdoruesi"> id e perdoruesit</param>
        /// <param name="idNdermarje">id e ndermarjes</param> 
        public clsLlogariShperndarjeQK(int idllogari, int idkpf, int idPerdoruesi, int idNdermarje)
        {
            idLlogariShperndarje = idllogari;
            this.idKPF = idkpf;
            this.idPerdoruesi = idPerdoruesi;
            this.idNdermarje = idNdermarje;

        }
        /// <summary>
        /// konstruktori me parameter
        /// </summary>
        /// <param name="idllogari">id e llogarise</param>
        /// <param name="idkpf"> id e kpf</param>
        /// <param name="idPerdoruesi"> id e perdoruesit</param>
        /// <param name="idNdermarje">id e ndermarjes</param> 
        public clsLlogariShperndarjeQK(int idllogari, int idkpf, int idPerdoruesi, int idNdermarje, string emerkf, string prind)
        {
            idLlogariShperndarje = idllogari;
            this.idKPF = idkpf;
            this.idPerdoruesi = idPerdoruesi;
            this.idNdermarje = idNdermarje;
            this.emertimiKPF = emerkf;
            this.prindi = prind;

        }
        /// <summary>
        /// konstruktor me 1 parameter
        /// </summary>
        /// <param name="id">id </param>
        public clsLlogariShperndarjeQK(int id)
        {
            clsDatabaseQendraKosto db = new clsDatabaseQendraKosto();
            mbushLlogari(db.ktheLlogariShpernarjeQKSipasId(id));
            db.Dispose();
        }

        public clsLlogariShperndarjeQK(DataRow rreshti)
        {

            mbushLlogari(rreshti);
        }
        #endregion

        #region Properties
        /// <summary>
        /// id e ritese e llogarise se shperndarje qk
        /// </summary>
        public int IdLlogariShperndarje
        {
            get
            {
                return
                    idLlogariShperndarje;
            }
            set
            {
                idLlogariShperndarje = value;
            }
        }
        /// <summary>
        ///  id e kpf
        /// </summary>
        public int IdKPF
        {
            get
            {
                return idKPF;
            }
            set
            {
                idKPF = value;
            }
        }
        /// <summary>
        /// id e ndermarjes
        /// </summary>
        public int IdNdermarje
        {
            get
            {
                return idNdermarje;
            }
            set
            {
                idNdermarje = value;
            }
        }

        /// <summary>
        /// id e perdoruesit qe ka kryer veprimin
        /// </summary>
        public int IdPerdoruesi
        {
            get
            {
                return idPerdoruesi;
            }
            set
            {
                idPerdoruesi = value;
            }
        }

        /// <summary>
        /// emertimi i kpf
        /// </summary>
        public string EmertimiKPF
        {
            get
            {
                return emertimiKPF;
            }
            set
            {
                emertimiKPF = value;
            }
        }
        /// <summary>
        /// niveli i kpf
        /// </summary>
        public int NiveliKPF
        {
            get
            {
                return niveliKPF;
            }
        }
        /// <summary>
        /// prindi i kpf
        /// </summary>
        public string Prindi
        {
            get
            {
                return prindi;
            }
            set
            {
                prindi = value;
            }
        }
        #endregion

        #region Metoda Publike

        /// <summary>
        /// ruan llogarine
        /// </summary>
        /// <param name="db"> clsDatabaseQendraKosto per te qene pjese e trasaksionit</param>
        /// <returns> cls mesazh qe tregon nqs eshte ruajtur apo jo llogaria</returns>
        public clsMesazh ruaj(clsDatabaseQendraKosto db)
        {
            clsMesazh mesazh = new clsMesazh();
            int idllog = 0;
            mesazh = db.ruajLlogariShperndarjeQK(out idllog, idKPF, idNdermarje, idPerdoruesi);
            idLlogariShperndarje = idllog;
            return mesazh;
        }

        /// <summary>
        /// modifikon llogarine
        /// </summary>
        /// <returns> cls mesazh qe tregon nqs eshte ruajtur apo jo llogaria</returns>   
        public clsMesazh modifiko()
        {
            clsMesazh mesazh = new clsMesazh();
            clsDatabaseQendraKosto db = new clsDatabaseQendraKosto();
            mesazh = db.modifikoLlogariShperndarjeQK(idLlogariShperndarje, idKPF, idNdermarje, idPerdoruesi);
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
        /// Fshin objektin llogarine ne tabelen perkatese ne databaze
        /// </summary>
        /// <param name="db">db nqs ben pjese ne nje transaksion</param>
        /// <returns > nje objekt clsMesazh qe tregon nese fshirja eshte kryer ne rregull apo jo</returns>
        public clsMesazh fshi(clsDatabaseQendraKosto db)
        {
            clsMesazh u_fshi = db.fshiLlogariShperndarjeQK(idLlogariShperndarje);
            return u_fshi;
        }

        public static clsMesazh fshi(int idndermarje, clsDatabaseQendraKosto db)
        {
            clsMesazh u_fshi = db.fshiLlogariShperndarjeQKSipasIdNdermarje(idndermarje);
            return u_fshi;
        }

        /// <summary>
        /// Merr objektin llogarine nga tabela perkatese ne databaze.
        /// </summary>
        public void merr(int idllog)
        {
            clsDatabaseQendraKosto db = new clsDatabaseQendraKosto();
            mbushLlogari(db.ktheLlogariShpernarjeQKSipasId(idllog));
            db.Dispose();
        }

        /// <summary>
        /// Merr objektin llogarine nga tabela perkatese ne databaze sipas idkpf.
        /// </summary>
        public void merrSipasIdKpf(int idkpf)
        {
            clsDatabaseQendraKosto db = new clsDatabaseQendraKosto();
            mbushLlogari(db.ktheLlogariShpernarjeQKSipasKPF(idkpf));
            db.Dispose();
        }

        public static bool kaVeprime(int idkpf, int idndermarje)
        {
            clsDatabaseQendraKosto db = new clsDatabaseQendraKosto();
            bool mbush = db.kaVeprime(idkpf, idndermarje);
            return mbush;
        }
        #endregion

        #region Metoda Internal

        /// <summary>
        /// mbush llogarite me te dhenat nga databaza
        /// </summary>
        /// <param name="dbDataRow">rreshti me te dhena</param>
        /// <returns> kthen ne se mbushja u be ne rregull apo jo</returns>
        internal clsMesazh mbushLlogari(DataRow dbDataRow)
        {
            if (dbDataRow != null)
            {
                try
                {
                    int.TryParse(dbDataRow["IDLLOGSHPERNDARJE"].ToString(), out idLlogariShperndarje);
                    int.TryParse(dbDataRow["IDKPF"].ToString(), out idKPF);
                    prindi = dbDataRow["PRIND"].ToString();

                    emertimiKPF = dbDataRow["EMERTIMIKPF"].ToString();
                    int.TryParse(dbDataRow["IDPERDORUESI"].ToString(), out idPerdoruesi);
                    int.TryParse(dbDataRow["NIVELIKPF"].ToString(), out niveliKPF);
                    int.TryParse(dbDataRow["IDNDERMARJE"].ToString(), out idNdermarje);


                    return new clsMesazh(true, clsLlogariShperndarjeQK.mbushjeSukses);
                }
                catch (InvalidCastException)
                {
                    throw new Exception(clsLlogariShperndarjeQK.gabimNeTeDhena);
                }
            }
            else
                return new clsMesazh(false, clsLlogariShperndarjeQK.drbosh);
        }

        #endregion
    }
}
