using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DbCore.DbAdmin
{
    /// <summary>
    ///  Kjo eshte klasa qe sherben per objektet qe perfaqsojne nje ambjent Celje/Regjistrimi.
    ///  Keto perdoren ne rastin kur behet lidhja e numrave automatike(
    ///  Te dhenat  merren nga tabela : T_LISTEAMBJENTECELJEREGJSTRIMI)
    /// </summary>
    public class clsListeAmbjenteCeljeRegjistrim
    {  
        #region Atributet

        private int idCR;
        private string kodiCR;
        private string pershkrimCR;
        private DataRow rreshti;

        #endregion
       
        #region Konstruktoret

        /// <summary>
        /// Konstruktori i klases
        /// </summary>
        public clsListeAmbjenteCeljeRegjistrim(int idCR, string kodiCR, string pershkrimCR)
        {
            this.idCR = idCR;
            this.kodiCR = kodiCR;
            this.pershkrimCR = pershkrimCR;
           
        }

        /// <summary>
        /// konstruktor me 1 parameter int
        /// </summary>
        /// <param name="id">id e listes se ambjentit</param>
        public clsListeAmbjenteCeljeRegjistrim(int id)
        {
            clsDatabaseAdmin data = new clsDatabaseAdmin();
            mbushListAmbjenteCeljeReg(data.merrListeAmbjentiCeljeRegjistrim(id));
            data.Dispose();
        }

        /// <summary>
        /// Konstruktori default i klases
        /// </summary>
        public clsListeAmbjenteCeljeRegjistrim()
        {
        }

        public clsListeAmbjenteCeljeRegjistrim(DataRow rreshti)
        {
            
            mbushListAmbjenteCeljeReg(rreshti);
        }

        #endregion
       
        #region Properties

        /// <summary>
        /// Kthen/Vendos ID-ne qe gjenerohet automatikisht.
        /// </summary>
        public int IdCR {
            get
            {
                return idCR;
            }
            set
            {
                idCR = value;
            }
        }

        /// <summary>
        /// Kthen/Vendos kodin per ambjentin e Celje/Regjistrimit.
        /// </summary>
        public string  KodiCR {
            get
            {
                return kodiCR;
            }
            set
            {
                kodiCR = value;
            }
        }

        /// <summary>
        /// Kthen/Vendos pershkrimin per ambjentin e Celje/Regjistrimit.
        /// </summary>
        public string  PershkrimCR {
            get
            {
                return pershkrimCR ;
            }
            set
            {
                pershkrimCR = value;
            }
        }
        
        #endregion

        #region Metoda Publike

        /// <summary>
        /// Ruan objektin e Celeje/Regjistrimit ne tabelen perkatese ne databaze.
        /// </summary>
        public clsMesazh ruaj()
        {//metoda qe therret klasen clsDatabaseAdmin per ruajtjen e nje ambjenti celje regjistrim 
            clsDatabaseAdmin  data = new clsDatabaseAdmin();
            clsMesazh u_ruajt = data.ruajListeAmbjenteCeljeRegjistrim(this.IdCR, this.KodiCR, this.PershkrimCR);
            data.Dispose();
            return u_ruajt;
        }

        /// <summary>
        /// Modifikon rreshtin perkates ne databaze duke perdorur te dhenat qe jane tek objekti i kushtit te dergimit.
        /// </summary>
        public clsMesazh modifiko()
        {//metoda qe therret klasen clsDatabaseAdmin per modifikimin e nje ambjenti celje regjistrim
            clsDatabaseAdmin data = new clsDatabaseAdmin();
            clsMesazh u_modifikua = data.modifikoListeAmbjenteCeljeRegjistrim(this.IdCR, this.KodiCR, this.PershkrimCR);
            data.Dispose();
            return u_modifikua;
        }

        /// <summary>
        /// Fshin rreshtin perkates nga tabela perkatese ne databaze sipas ID-se qe i eshte caktuar objektit te 
        /// celje/regjistrimit.
        /// </summary>
        public clsMesazh fshi()
        {//metoda qe therret klasen clsDatabaseAdmin per fshirjen e nje  ambjenti celje regjistrim
            clsDatabaseAdmin data = new clsDatabaseAdmin();
            clsMesazh u_fshi = data.fshiListeAmbjenteCeljeRegjistrim(this.IdCR);
            data.Dispose();
            return u_fshi;
        }

        /// <summary>
        /// Nuk perdoret.
        /// </summary>
        public void merr()
        {//metoda qe therret klasen clsDatabaseAdmin per marrjen e nje  ambjenti celje regjistrim
            clsDatabaseAdmin data = new clsDatabaseAdmin();
            data.merrListeAmbjenteCeljeRegjistrim(this.IdCR);
            data.Dispose();
        }

        /// <summary>
        /// Kthen nje collection me objekte te tipit <see cref="DbCore.DbAdmin.clsListeAmbjenteCeljeRegjistrim"/>.
        /// </summary>
        public colListeAmbjenteshCeljeRegjistrim merrGjitheListeAmbjenteshCeljeRegjistrim()
        {//metoda qe therret klasen clsDatabaseAdmin per marrjen e te gjithave  ambjenteve celje regjistrim
            colListeAmbjenteshCeljeRegjistrim data = new colListeAmbjenteshCeljeRegjistrim();
            data.mbushGjitheListeAmbjenteshCeljeRegjistrim();
            return data;

        }

        public static int ktheIdCR(string kod, clsDatabaseAdmin data )
        {
             int idCr = (data.merrIDListeAmbjentiCeljeRegjistrim(kod));
          
            return idCr;
        }
 public static int ktheIdCR(string kod )
        {
            using (clsDatabaseAdmin data = new clsDatabaseAdmin())
            {
                int idCr = (data.merrIDListeAmbjentiCeljeRegjistrim(kod));

                return idCr;
            }
        }

        #endregion

        #region Metoda Internal

        internal bool mbushListAmbjenteCeljeReg(DataRow dbDataRowListAmbjent)
        {
            if (dbDataRowListAmbjent != null)
            {
                try
                {
                    int.TryParse(dbDataRowListAmbjent["IDCR"].ToString(), out idCR);
                    kodiCR = dbDataRowListAmbjent["KODICR"].ToString();
                    pershkrimCR = dbDataRowListAmbjent["PERSHKRIMICR"].ToString();
                    return true;
                }
                catch (InvalidCastException)
                {
                    throw new Exception("ERROR: Gabim gjate marrjes se listes se ambjenteve ne celjen e regjistrimit nga db-ja");
                }
            }
            else
                return false;
        }

        #endregion
    }
}
