using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;

namespace DbCore.DbRegjistrim
{
    /// <summary>
    ///  Kjo eshte klasa qe sherben per objektet qe perfaqsojne  trupin e nje dokumenti riparime
    ///  (Te dhenat  merren nga tabela : T_TRUPIRIPARIME)
    /// </summary>
  public  class clsTrupiRiparime
    {
         /// <summary>
        /// konstante per mesazhin e gabimit te marrjes se te dhenave nga db
        /// </summary>
        private const string STR_ERRORGabimGjateMarrjesSeTrupitTePlanifikimitNgaD = "ERROR: Gabim gjate marrjes se trupit te riparimeve nga db-ja";

        #region Attributet
        /// <summary>
        /// id e trupit te riparimeve
        /// </summary>
        private int idTrupi;
        /// <summary>
        /// id e kokes te riparimeve
        /// </summary>
        private int idKoka;
        /// <summary>
        /// id e artikullit loan
        /// </summary>
        private int idArtLoan;
        /// <summary>
        /// kodi i artikullit loan
        /// </summary>
        private string artLoan;
      
        /// <summary>
        /// id e detajimi i artikullit loan
        /// </summary>
        private int idDetLoan;
        /// <summary>
        /// aksesor
        /// </summary>
        private string  aksesor;
        /// <summary>
        /// id e artikullit swap
        /// </summary>
        private int idArtSwap;
        /// <summary>
        ///  id e detajimit swap
        /// </summary>
        private int idDetSwap;
        /// <summary>
        /// kodi i detajimit loan 
        /// </summary>
        private string detLoan;
        /// <summary>
        /// kodi i artikullit swap
        /// </summary>
        private string artSwap;
        /// <summary>
        /// kodi i detajimit swap
        /// </summary>
        private string detSwap;
        private DataRow rreshti;
    
        #endregion

        #region Konstruktoret

        /// <summary>
        /// konstruktori i plote
        /// </summary>
        /// <param name="idtrupi"></param>
        /// <param name="idkoka"></param>
        /// <param name="idArtloan"></param>
        /// <param name="iddetloan"></param>
        /// <param name="aksesor"></param>
        /// <param name="idartswap"></param>
        /// <param name="iddetswap"></param>
        public clsTrupiRiparime(int idtrupi, int idkoka, int idArtloan,  int iddetloan, string aksesor, int idartswap, int iddetswap)
        {
            idTrupi = idtrupi;
            idKoka = idkoka;
            idArtLoan = idArtloan;
             idDetLoan = iddetloan;
            this.aksesor = aksesor;
            idArtSwap = idartswap;
            this.idDetSwap = iddetswap;
            
           
        }

        /// <summary>
        /// konstruktor me 1 parameter
        /// </summary>
        /// <param name="idTrupi">id e trupit</param>
        /// <param name="db">clsDatabaseprodhimi per raste transaksioni</param>
        public clsTrupiRiparime(int idTrupi, int idndermarje)
        {
            clsDatabaseRegjistrim db = new clsDatabaseRegjistrim();
            mbushTrupRiparime(db.ktheTrupiRiparimeSipasID(idTrupi, idndermarje));
            db.Dispose();
        }

        /// <summary>
        /// Konstruktori i klases pa parametra
        /// </summary>
        public clsTrupiRiparime()
        {
        }

        public clsTrupiRiparime(DataRow rreshti)
        {
            
            mbushTrupRiparime(rreshti);
        }
        #endregion

        #region Properties

        /// <summary>
        /// Kthen/Vendos ID-ne qe gjenerohet automatikisht.
        /// </summary>
        public int IdTrupi
        {
            get { return idTrupi; }
            set { idTrupi = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e kokes .
        /// </summary>
        public int IdKoka
        {
            get { return idKoka; }
            set { idKoka = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e artikullit loan .
        /// </summary>
        public int IdArtLoan
        {
            get { return idArtLoan; }
            set { idArtLoan = value; }
        }

    
        /// <summary>
        /// Kthen/Vendos Kodi i artikullit loan .
        /// </summary>
        public String ArtLoan
        {
            get { return artLoan; }
            set { artLoan = value; }
        }


        /// <summary>
        /// Kthen/Vendos ID-ne e detajimit loan.
        /// </summary>
        public int IdDetLoan
        {
            get { return idDetLoan; }
            set { idDetLoan = value; }
        }

        /// <summary>
        /// Kthen/Vendos aksesor.
        /// </summary>
        public string Aksesor
        {
            get { return aksesor; }
            set { aksesor = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e artikullit swap.
        /// </summary>
        public int IdArtSwap
        {
            get { return idArtSwap; }
            set { idArtSwap = value; }
        }

        /// <summary>
        /// id e detajimit swap
        /// </summary>
        public int IdDetSwap
        {
            get
            {
                return idDetSwap;
            }
            set
            {
                idDetSwap = value;
            }
        }
        /// <summary>
        /// kodi i detajimit loan 
        /// </summary>
        public string DetLoan
        {
            get
            {
                return detLoan;
            }
            set
            {
                detLoan = value;
            }
        }
        /// <summary>
        /// kodi i artikullit swap
        /// </summary>
        public string ArtSwap
        {
            get
            {
                return artSwap;
            }
            set
            {
                artSwap = value;
            }
        }

        /// <summary>
        /// kodi i detajimit swap
        /// </summary>
        public string DetSwap
        {
            get
            {
                return detSwap;
            }
            set
            {
                detSwap = value;
            }
        }
        #endregion

        #region Metoda Publike


        /// <summary>
        /// Ruan objektin e  trupit te dokumentit te riparime ne tabelen perkatese ne databaze
        /// </summary>
        /// <returns > nje objekt clsMesazh qe tregon nese ruajtja eshte kryer ne rregull apo jo</returns>
        public clsMesazh ruaj()
        {
            clsDatabaseRegjistrim data = new clsDatabaseRegjistrim();
            int id;
            clsMesazh u_ruajt = data.ruajTrupiRiparime(out id, IdKoka, IdArtLoan, IdDetLoan, Aksesor, IdArtSwap,IdDetSwap);
            IdTrupi = id;
            data.Dispose();
            return u_ruajt;
        }

        /// <summary>
        /// modifikon objektin e  trupit te dokumentit te riparime ne tabelen perkatese ne databaze
        /// </summary>
        /// <returns > nje objekt clsMesazh qe tregon nese modifikimi eshte kryer ne rregull apo jo</returns>
        public clsMesazh modifiko()
        {
            clsDatabaseRegjistrim data = new clsDatabaseRegjistrim();
            clsMesazh u_modifikua = data.modifikoTrupiRiparime(IdTrupi, IdKoka, IdArtLoan, IdDetLoan, Aksesor, IdArtSwap, idDetSwap);
            data.Dispose();
            return u_modifikua;
        }

        /// <summary>
        /// fshin objektin e  trupit te dokumentit te riparime ne tabelen perkatese ne databaze
        /// </summary>
        /// <returns > nje objekt clsMesazh qe tregon nese fshirja eshte kryer ne rregull apo jo</returns>
        public clsMesazh fshi()
        {
            clsDatabaseRegjistrim data = new clsDatabaseRegjistrim();
            clsMesazh u_fshi = data.fshiTrupiRiparimeSipasID(IdTrupi);
            data.Dispose();
            return u_fshi;
        }

        /// <summary>
        /// fshin objektet e  trupit te dokumentit te riparime sipas id se kokes ne tabelen perkatese ne databaze
        /// </summary>
        /// <returns > nje objekt clsMesazh qe tregon nese fshirja eshte kryer ne rregull apo jo</returns>
        public clsMesazh fshiSipasKoka()
        {
            clsDatabaseRegjistrim data = new clsDatabaseRegjistrim();
            clsMesazh u_fshi = data.fshiTrupiRiparimeSipasKoka(IdKoka);
            data.Dispose();
            return u_fshi;
        }

        /// <summary>
        /// Merr objektet e  trupit te dokumentit te riparime sipas kokes nga tabela perkatese ne databaze
        /// </summary>
        /// <returns > nje objekt  colTrupiRiparime me te gjithe trupat e nje dokumenti</returns>
        public colTrupiRiparime merriSipasKoka(int idndermarje)
        {
            colTrupiRiparime data = new colTrupiRiparime();

            data.mbushTrupiRiparime(IdKoka, idndermarje);
            return data;
        }

        /// <summary>
        /// Merr objektin e  trupit te dokumentit te riparime sipas id nga tabela perkatese ne databaze
        /// </summary>
        /// <returns > nje objekt clsTrupiPlanifikim me trupin e dokumentit te riparime te kerkuar</returns>
        public clsTrupiRiparime merriSipasID(int idndermarje)
        {
            clsTrupiRiparime data = new clsTrupiRiparime(IdTrupi, idndermarje);
            return data;

        }

        #endregion

        #region Metoda Internal

        /// <summary>
        /// mbush trupin e riparimeve nga databaza
        /// </summary>
        /// <param name="dbDataRow">datarow qe duhet mbushur nga db</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert false</returns>
        internal bool mbushTrupRiparime(DataRow dbDataRow)
        {
            if (dbDataRow != null)
            {
                try
                {
                    int.TryParse(dbDataRow["IDTRUPI"].ToString(), out idTrupi);
                    int.TryParse(dbDataRow["IDKOKA"].ToString(), out idKoka);
                    int.TryParse(dbDataRow["IDARTLOAN"].ToString(), out idArtLoan);
                    int.TryParse(dbDataRow["IDDETLOAN"].ToString(), out idDetLoan);
                    aksesor = dbDataRow["AKSESORLOAN"].ToString();
                    detLoan = dbDataRow["DETLOAN"].ToString();
                    artSwap = dbDataRow["ARTSWAP"].ToString();
                    int.TryParse(dbDataRow["IDARTSWAP"].ToString(), out idArtSwap);
                    artLoan = dbDataRow["ARTLOAN"].ToString();
                    int.TryParse(dbDataRow["IDDETSWAP"].ToString(), out idDetSwap);
                    detSwap = dbDataRow["DETSWAP"].ToString();
                    return true;
                }
                catch (InvalidCastException)
                {
                    throw new MyException("Gabim gjate cast-it!");
                }
                catch (Exception)
                {
                    throw new MyException(STR_ERRORGabimGjateMarrjesSeTrupitTePlanifikimitNgaD);
                }
            }
            else
                return false;
        }

        #endregion
    }
}
