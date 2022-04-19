using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DbCore.DbAdmin
{
  public  class clsTrupiErrorImporti
    {
      /// <summary>
        /// Kjo klase permban metodat e nevojshme per te perdorur te dhenat e tabeles T_TRUPIERRORIMPORTI
        /// </summary>

        #region Atribute

        private int id;
       
        private string gabimi;
        private int idKoka;
        private int rreshti;
        private string kodi;
        private DataRow rreshti1;
        

    

        #endregion

        #region Konstruktoret

        /// <summary>
        /// Konstruktori me parametra
        /// </summary>
        /// <param name="idKoka">id </param>
        /// <param name="kodi">kodi</param>
        /// <param name="pershkrimi">pershkrimi</param>
        /// <param name="idkategori">kategoria</param>
        /// <param name="idNder">id ndermarrjes</param>
        /// <param name="idPerd">id perdoruesit</param>
        /// <param name="idStatus">id statusit dok</param>
        public clsTrupiErrorImporti(int id, string kodi, string gabimi, int idkoka, int rreshti
                                   )
        {
            this.id = id;
            this.gabimi = gabimi;
            this.idKoka = idkoka;
            this.rreshti = rreshti;
            this.kodi = kodi;


           
        }
     

        /// <summary>
        /// Konstruktori default
        /// </summary>
        public clsTrupiErrorImporti()
        {

        }

        public clsTrupiErrorImporti(DataRow rreshti1)
        {
            
            mbushErrorImporti(rreshti1);
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
        /// Kthen/Vendos pershkrimin 
        /// </summary>
        public string Gabimi
        {
            get { return gabimi; }
            set { gabimi = value; }
        }


        /// <summary>
        /// Kthen/Vendos  kategorine e formatit te importit
        /// </summary>
        public int IdKoka
        {
            get { return idKoka; }
            set { idKoka = value; }
        }

        /// <summary>
        /// Kthen/Vendos id e ndermarrjes 
        /// </summary>
        public int Rreshti
        {
            get { return rreshti; }
            set { rreshti = value; }
        }

        /// <summary>
        /// Kthen/Vendos id se perdoruesit 
        /// </summary>
        public string Kodi
        {
            get { return kodi; }
            set { kodi = value; }
        }


        #endregion

        #region Metoda Publike

        public override string ToString()
        {
            return $"ERORR RRESHT IMPORTI ||rreshti :{rreshti}||gabimi :{gabimi}||kodi :{kodi}||";
        }

        #endregion

        #region Metoda Internal

        internal bool mbushErrorImporti(DataRow dbDataRow)
        {
            if (dbDataRow != null)
            {
                try
                {
                    int.TryParse(dbDataRow["ID"].ToString(), out id);
                    gabimi = dbDataRow["GABIMI"].ToString();
                     int.TryParse(dbDataRow["IDKOKA"].ToString(), out idKoka);
                    int.TryParse(dbDataRow["RRESHTI"].ToString(), out rreshti);
                    kodi = dbDataRow["KODI"].ToString();

                    return true;
                }
                catch (InvalidCastException)
                {
                    throw new Exception("ERROR: Gabim gjate marrjes se TRUPIT se error importi  nga db-ja");
                }
            }
            else
                return false;
        }
        internal bool mbushErrorImportiNgaProg(DataRow dbDataRow)
        {
            if (dbDataRow != null)
            {
                try
                {

                    gabimi = dbDataRow["Gabimi"].ToString();

                    int.TryParse(dbDataRow["Rreshti"].ToString(), out rreshti);
                    kodi = dbDataRow["Kodi"].ToString();

                    return true;
                }
                catch (InvalidCastException)
                {
                    throw new Exception("ERROR: Gabim gjate marrjes se TRUPIT se error importi  nga programi-ja");
                }
            }
            else
                return false;
        }

        internal bool mbushErrorImportiNgaAmbienti(DataRow dbDataRow)
        {
            if (dbDataRow != null)
            {
                try
                {
                    gabimi = dbDataRow["Gabimi"].ToString();
                    int.TryParse(dbDataRow["Rreshti me id"].ToString(), out rreshti);
                    kodi = dbDataRow["Kodi"].ToString();
                    return true;
                }
                catch (InvalidCastException)
                {
                    throw new Exception("ERROR: Gabim gjate marrjes se TRUPIT se error importi  nga programi-ja");
                }
            }
            else
                return false;
        }

        #endregion
    }
}
