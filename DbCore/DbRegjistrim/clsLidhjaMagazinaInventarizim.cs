using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbCore.DbRegjistrim
{
   public  class clsLidhjaMagazinaInventarizim
    { 
       #region Atributet

        private int id;
        private int idKokaMagazina;
        private int idKokaInventarizimi;
        private int idKonfigMagazina;
        private int idKonfigInventarizimi;
        private DataRow rreshti;

        #endregion

        #region Konstruktoret

        /// <summary>
        /// Konstruktori i klases
        /// </summary>
        public clsLidhjaMagazinaInventarizim(int id, int idinv, int idmag, int idkonfmag, int idkonfinv)
        {
            this.id = id;
            this.idKokaInventarizimi = idinv;
            this.idKokaMagazina = idmag;
            this.idKonfigMagazina = idkonfmag;
            this.idKonfigInventarizimi = idkonfinv;
        }

        /// <summary>
        /// Konstruktori i klases
        /// </summary>
        public clsLidhjaMagazinaInventarizim()
        {
        }

        public clsLidhjaMagazinaInventarizim(DataRow rreshti)
        {
            
            mbushLidhje(rreshti);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Kthen/Vendos ID-ne qe gjenerohet automatikisht.
        /// </summary>
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e dokumentit qe eshte konvertuar
        /// </summary>
        public int IdKokaMagazina
        {
            get { return idKokaMagazina; }
            set { idKokaMagazina = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e dokumentit ne te cilin u konvertua
        /// </summary>
        public int IdKokaInventarizimi
        {
            get
            {
                return idKokaInventarizimi;
            }
            set
            {
                idKokaInventarizimi = value;
            }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e konfigurimit te dokumentit te konvertuar
        /// </summary>
        public int IdKonfigMagazina
        {
            get { return idKonfigMagazina; }
            set { idKonfigMagazina = value; }
        }
        /// <summary>
        /// kthen vendos id e konfigurimit te dokumentit ne te cilin u konvertua
        /// </summary>
        public int IdKonfigInventarizimi
        {
            get
            {
                return idKonfigInventarizimi;
            }
            set
            {
                idKonfigInventarizimi = value;
            }
        }
        #endregion

        #region Metoda Publike

        /// <summary>
        /// Ruan objektin e konvertimit ne tabelen perkatese ne databaze.
        /// </summary>
        public clsMesazh ruaj()
        {
            clsDatabaseRegjistrim data = new clsDatabaseRegjistrim();
            clsMesazh u_ruajt = data.ruajLidhjeMagInv(this.Id, this.IdKokaMagazina,this.IdKokaInventarizimi,  this.IdKonfigMagazina, this.IdKonfigInventarizimi);
            data.Dispose();
            return u_ruajt;
        }



        /// <summary>
        /// Fshin rreshtin perkates nga tabela perkatese ne databaze sipas ID-se qe i eshte caktuar objektit te 
        /// konvertimit sipas id se dokumentit te konvertuar
        /// </summary>
        public clsMesazh fshiLidhjeMagInvSipasInv(int idinv)
        {
            clsDatabaseRegjistrim data = new clsDatabaseRegjistrim();
            clsMesazh u_fshi = data.fshiLidhjeMagInvSipasInv(idinv);
            data.Dispose();
            return u_fshi;
        }
        /// <summary>
        /// Fshin rreshtin perkates nga tabela perkatese ne databaze sipas ID-se qe i eshte caktuar objektit te 
        /// konvertimit sipas id se dokumentit ne te cilin u konvertua
        /// </summary>
        public clsMesazh fshiLidhjeMagInvSipasMag(int idmag)
        {
            clsDatabaseRegjistrim data = new clsDatabaseRegjistrim();
            clsMesazh u_fshi = data.fshiLidhjeMagInvSipasMag(idmag);
            data.Dispose();
            return u_fshi;
        }

        #endregion

        #region Metoda Internal

        internal bool mbushLidhje(DataRow dbDataRow)
        {
            if (dbDataRow != null)
            {
                try
                {
                    int.TryParse(dbDataRow["IDKONVERTIMI"].ToString(), out id);
                    int.TryParse(dbDataRow["IDKOKAINVENTARIZIM"].ToString(), out idKokaInventarizimi);
                    int.TryParse(dbDataRow["IDKOKAMAGAZINA"].ToString(), out idKokaMagazina);
                    int.TryParse(dbDataRow["IDKONFIGMAGAZINA"].ToString(), out idKonfigMagazina);
                    int.TryParse(dbDataRow["IDKONFIGINVENTARIZIM"].ToString(), out idKonfigInventarizimi);
                    return true;
                }
                catch (InvalidCastException)
                {
                    throw new Exception("ERROR: Gabim gjate marrjes se lidhje mag inv nga db-ja");
                }
            }
            else
                return false;
        }

        #endregion
    }
}

