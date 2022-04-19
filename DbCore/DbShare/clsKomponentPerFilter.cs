using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DbCore.DbShare
{
   public class clsKomponentPerFilter
    {
        // properties
        private int idKomponente;
        private String emri;
        private int idFilter;
        private DataRow rreshti;
      

        //konstruktoret
        public clsKomponentPerFilter()
        {
            idKomponente = 0;
            emri = "";
            idFilter = 0;
        }

        public clsKomponentPerFilter(DataRow rreshti)
        {
            
            mbushKomponentPerFilter(rreshti);
        }
    
        //metodat per marrjen e te dhenave
        public int IdFilter
        {
            get { return idFilter; }
            set { idFilter = value; }
        }

        public String KomponenteEmri
        {
            get { return emri; }
            set { emri = value; }
        }

       

        public int IdKomponente
        {
            get { return idKomponente; }
            set { idKomponente = value; }
        }

        //duhet pare metoda merrTrupinFiltri
        internal bool mbushKomponentPerFilter(DataRow rreshti)
        {
            idKomponente = int.Parse(rreshti[0].ToString());
            emri = rreshti[1].ToString();
            idFilter = int.Parse(rreshti[2].ToString());
            return true;
        }
        

    }
}
