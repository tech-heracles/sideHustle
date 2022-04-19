using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DbCore.DbRegjistrim
{

    /// <summary>
    ///  Kjo eshte klasa qe sherben per objektet qe perfaqsojne  komentet e aprovimit
    ///  (Te dhenat  merren nga tabela : T_KOMENTEAPROVIMI)
    ///  lidh furnitoret me artikujt
    /// </summary>
    public class clsKomenteAprovimi
    {
         #region Atribute

        private int idKomenti;
        private int idEtape;
        private int idPerdoruesi;
        private DateTime data;
        private string koment;
        private string nrEtape;
        private string perdoruesUsername;
        private DataRow rreshti;

        #endregion 

        #region Properties

        /// <summary>
        /// Kthen/Vendos ID-ne qe gjenerohet automatikisht.
        /// </summary>
        public int IdKomenti
        {
            get { return idKomenti ; }
            set { idKomenti  = value; }
        }
        /// <summary>
        /// Kthen/Vendos ID-ne e etapes
        /// </summary>
        public int IdEtape
        {
            get { return idEtape ; }
            set { idEtape = value; }
        }
        /// <summary>
        /// Kthen/Vendos ID-ne e perdoruesit.
        /// </summary>
        public int IdPerdoruesi
        {
            get { return idPerdoruesi; }
            set { idPerdoruesi = value; }
        }
        /// <summary>
        /// Kthen/Vendos data.
        /// </summary>
        public DateTime Data
        {
            get { return data ; }
            set { data  = value; }
        }
        /// <summary>
        /// Kthen/Vendos koment.
        /// </summary>
        public string Koment
        {
            get
            {
                return koment;  

            }
            set
            {
                koment = value;
            }
        }
        /// <summary>
        /// Kthen/Vendos nr etape.
        /// </summary>
        public string NrEtape
        {
            get
            {
                return nrEtape;
            }
            set
            {
                this.nrEtape = value;
            }

        }

public string PerdoruesUsername
{
    get
    {
        return perdoruesUsername;
    }
    set
    {
        perdoruesUsername = value;
    }
}
        #endregion

        #region Konstruktoret

        /// <summary>
        /// konstruktori me parametra
        /// </summary>
/// <param name="idkomenti"> id koment </param>
/// <param name="idetape"> id e etapes</param>
/// <param name="idperdoruesi">id e perdoruesit</param>
        /// <param name="data">data</param>
        /// <param name="koment">komenti</param>
        public clsKomenteAprovimi(int idkomenti,int idetape,int idperdoruesi,  DateTime data, string koment)
        {
            this.idKomenti = idkomenti;
            this.idEtape = idetape;
            this.idPerdoruesi = idperdoruesi;
            this.data = data;
            this.koment = koment;
        }

        /// <summary>
        /// konstruktori pa parametra
        /// </summary>

        public clsKomenteAprovimi()
        { 
        }

        public clsKomenteAprovimi(DataRow rreshti)
        {
            
            mbushKomenteAprovimi(rreshti);
        }

        #endregion

        #region Metoda Publike

        /// <summary>
        ///  ruan objektin koment te aprovimit
        /// </summary>
        /// <returns > nje objekt clsMesazh qe tregon nese ruajtja eshte kryer ne rregull apo jo</returns>
        public clsMesazh ruaj()
        {
            clsDatabaseRegjistrim data = new clsDatabaseRegjistrim();
            clsMesazh u_ruajt = ruaj(data); 
            data.Dispose();
            return u_ruajt;
        }
        public clsMesazh ruaj(clsDatabaseRegjistrim data)
        {
           
            int id;
            clsMesazh u_ruajt = data.ruajKomentAprovimi(out id, this.idEtape, this.idPerdoruesi, this.data,this.koment);
       
            return u_ruajt;
        }

        /// <summary>
        /// Modifikon objektin koment aprovimi
        /// </summary>
        /// <returns > nje objekt clsMesazh qe tregon nese modifikimi eshte kryer ne rregull apo jo</returns>
     
        public clsMesazh modifiko()
        {
            clsDatabaseRegjistrim data = new clsDatabaseRegjistrim();
            clsMesazh u_modifikua = data.modifikoKomentAprovimi(this.idKomenti, this.idEtape, this.idPerdoruesi, this.data, this.koment);
            data.Dispose();
            return u_modifikua;
        }

        /// <summary>
        /// Fshin objektin koment aprovimi
        /// </summary>
        /// <returns > nje objekt clsMesazh qe tregon nese fshirja eshte kryer ne rregull apo jo</returns>
     
        public clsMesazh fshi( int idkoment)
        {
            clsDatabaseRegjistrim data = new clsDatabaseRegjistrim();
            clsMesazh u_fshi =fshi(idkoment,data);
            data.Dispose();
            return u_fshi;
        }
    public clsMesazh fshi( int idkoment,clsDatabaseRegjistrim data)
        {
           
            clsMesazh u_fshi = data.fshiKomentAprovimi(idkoment);
          
            return u_fshi;
        }
        /// <summary>
        /// merr objektin koment aprovimi
        /// </summary>
      
        public void merr(int idkoment)
        {
            clsDatabaseRegjistrim data = new clsDatabaseRegjistrim();
            data.merrKomentAprovimi(idkoment);
            data.Dispose();
           
        }

        #endregion

        #region Metoda Internal

        /// <summary>
        /// mbush koment aprovimi nga db
        /// </summary>
        /// <param name="db">datarow qe duhet mbushur nga databaza</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert false</returns>
        internal bool mbushKomenteAprovimi(DataRow db)
        {

            if (db != null)
            {

                try
                {
                    int.TryParse(db["IDKOMENTI"].ToString(), out idKomenti);
                    int.TryParse(db["IDETAPE"].ToString(), out idEtape);
                    int.TryParse(db["IDPERDORUESI"].ToString(), out idPerdoruesi);
                    data =DateTime.Parse( db["DATA"].ToString());
                    koment = db["KOMENT"].ToString();
                    nrEtape = db["NRETAPE"].ToString();
                    perdoruesUsername = db["PERDORUESUSERNAME"].ToString();
                    return true;
                }
                catch (InvalidCastException)
                {
                    throw new Exception("ERROR: Gabim gjate marrjes se komenteve te aprovimit nga db-ja");
                }
            }
            else
                return false;

        }

        #endregion
    }
}