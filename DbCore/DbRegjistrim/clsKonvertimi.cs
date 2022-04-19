using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DbCore.DbRegjistrim
{   
    /// <summary>
    ///  Kjo eshte klasa qe sherben per objektet qe perfaqsojne  lidhjen e dokumentave qe konvertohen ne njeri tjetri
    ///  (Te dhenat  merren nga tabela : T_KONVERTIMI)
    /// </summary>
    public class clsKonvertimi
    {
        #region Atributet

        private int idKonvertimi;
        private int idDokKonvertuar;
        private int idDokPasKonvertimi;
        private int idKonfigAmbjenteKonvertuar;
        private int idKonfigAmbjentePasKonvertimi;
        private DataRow rreshti;

        #endregion

        #region Konstruktoret

        /// <summary>
        /// Konstruktori i klases
        /// </summary>
        public clsKonvertimi(int idKonvertimi, int idDokPasKonvertimi, int idDokKonvertuar, int idKonfigAmbjenteKonvertuar, int idKonfigAmbjentePasKonvertimi)
        {
            this.idKonvertimi = idKonvertimi;
            this.idDokPasKonvertimi = idDokPasKonvertimi;
            this.idDokKonvertuar = idDokKonvertuar;
            this.idKonfigAmbjenteKonvertuar = idKonfigAmbjenteKonvertuar;
            this.idKonfigAmbjentePasKonvertimi = idKonfigAmbjentePasKonvertimi;
        }

        /// <summary>
        /// Konstruktori i klases
        /// </summary>
        public clsKonvertimi()
        {
        }

        public clsKonvertimi(DataRow rreshti)
        {
            
            mbushKonvertim(rreshti);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Kthen/Vendos ID-ne qe gjenerohet automatikisht.
        /// </summary>
        public int IdKonvertimi
        {
            get { return idKonvertimi; }
            set { idKonvertimi = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e dokumentit qe eshte konvertuar
        /// </summary>
        public int IdDokKonvertuar
        {
            get { return idDokKonvertuar; }
            set { idDokKonvertuar = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e dokumentit ne te cilin u konvertua
        /// </summary>
        public int IdDokPasKonvertimi
        {
            get
            {
                return idDokPasKonvertimi;
            }
            set
            {
                idDokPasKonvertimi = value;
            }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e konfigurimit te dokumentit te konvertuar
        /// </summary>
        public int IdKonfigAmbjenteKonvertuar
        {
            get { return idKonfigAmbjenteKonvertuar; }
            set { idKonfigAmbjenteKonvertuar = value; }
        }
        /// <summary>
        /// kthen vendos id e konfigurimit te dokumentit ne te cilin u konvertua
        /// </summary>
        public int IdKonfigAmbjentePasKonvertimi
        {
            get
            {
                return idKonfigAmbjentePasKonvertimi;
            }
            set
            {
                idKonfigAmbjentePasKonvertimi = value;
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
            clsMesazh u_ruajt = data.ruajkonvertim(this.IdKonvertimi, this.IdDokPasKonvertimi, this.IdDokKonvertuar, this.IdKonfigAmbjenteKonvertuar, this.IdKonfigAmbjentePasKonvertimi);
            data.Dispose();
            return u_ruajt;
        }

        /// <summary>
        /// Fshin rreshtin perkates nga tabela perkatese ne databaze sipas ID-se qe i eshte caktuar objektit te 
        /// konvertimit sipas id se dokumentit te konvertuar
        /// </summary>
        public clsMesazh fshiSipasIdDokKonvertuar(int iddokkonvertuar)
        {
            clsDatabaseRegjistrim data = new clsDatabaseRegjistrim();
            clsMesazh u_fshi = data.fshiKonvertimSipasIdDokKonvertuar(iddokkonvertuar);
            data.Dispose();
            return u_fshi;
        }
        /// <summary>
        /// Fshin rreshtin perkates nga tabela perkatese ne databaze sipas ID-se qe i eshte caktuar objektit te 
        /// konvertimit sipas id se dokumentit ne te cilin u konvertua
        /// </summary>
        public clsMesazh fshiSipasIdDokPasKonvertimi(int iddokpasKonvertuar)
        {
            clsDatabaseRegjistrim data = new clsDatabaseRegjistrim();
            clsMesazh u_fshi = data.fshiKonvertimSipasIdDokPasKonvertimi(iddokpasKonvertuar);
            data.Dispose();
            return u_fshi;
        }

        #endregion

        #region Metoda Internal

        internal bool mbushKonvertim(DataRow dbDataRow)
        {
            if (dbDataRow != null)
            {
                try
                {
                    int.TryParse(dbDataRow["IDKONVERTIMI"].ToString(), out idKonvertimi);
                    int.TryParse(dbDataRow["IDDOKPASKONVERTIMI"].ToString(), out idDokPasKonvertimi);
                    int.TryParse(dbDataRow["IDDOKKONVERTUAR"].ToString(), out idDokKonvertuar);
                    int.TryParse(dbDataRow["IDKONFIGAMBJENTEKONVERTUAR"].ToString(), out idKonfigAmbjenteKonvertuar);
                    int.TryParse(dbDataRow["IDKONFIGAMBJENTEPASKONVERTIMI"].ToString(), out idKonfigAmbjentePasKonvertimi);
                    return true;
                }
                catch (InvalidCastException)
                {
                    throw new Exception("ERROR: Gabim gjate marrjes se konvertimit nga db-ja");
                }
            }
            else
                return false;
        }

        #endregion
    }
}
