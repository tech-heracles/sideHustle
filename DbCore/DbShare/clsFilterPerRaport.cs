using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DbCore.DbShare
{
   public class clsFilterPerRaport
   {
        #region Atribute
        private int idFilter;
        private String emri;
        private String pershkrimi;
        private int idRaporti;
        private DataRow rreshti;
       #endregion

        #region Konstruktoret
        public clsFilterPerRaport()
        {
            idFilter = 0;
            emri = "";
            pershkrimi = "";
            idRaporti = 0;
        }

        public clsFilterPerRaport(DataRow rreshti)
        {
            
            mbushFilterPerRaport(rreshti);
        }
        #endregion

        #region Properties
        public int IdFilter
        {
            get { return idFilter; }
            set { idFilter = value; }
        }

        public String FilterEmri
        {
            get { return emri; }
            set { emri = value; }
        }

        public String FilterPershkrimi
        {
            get { return pershkrimi; }
            set { pershkrimi = value; }
        }

        public int IdRaporti
        {
            get { return idRaporti; }
            set { idRaporti = value; }
        }
        #endregion
        //duhet pare metoda merrTrupinFiltri
        
        internal bool mbushFilterPerRaport(DataRow rreshti)
        {
            int.TryParse(rreshti["IDFILTER"].ToString(), out idFilter);
            emri = rreshti["FILTEREMRI"].ToString();
            pershkrimi = rreshti["FILTERPERSHKRIMI"].ToString();
            int.TryParse(rreshti["idraport"].ToString(), out idRaporti);
            return true;
        }

    }
}
