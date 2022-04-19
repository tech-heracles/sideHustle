using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DbCore.DbAdmin
{
   public class clsLlojVeprimi
    {
        #region properties
        private int idLloji;//id ritese e tabeles
        private string pershkrimLloji;
        private int prioriteti;
        private bool check;
        private int idKonfigAmbjenteLupa;
        private string kodLupa;
        #endregion

        #region konstruktoret
        public clsLlojVeprimi(int idlloji, string pershkrimi, int prioriteti, bool check, int idkonfigambjentelupa, string kodlupa)
        {
            this.idLloji = idlloji;
            this.pershkrimLloji = pershkrimi;
            this.prioriteti = prioriteti;
            this.check = check;
            this.idKonfigAmbjenteLupa = idkonfigambjentelupa;
            this.kodLupa = kodlupa;
        }



        public clsLlojVeprimi()
        {
        }

        #endregion

        #region metodat per marrjen e te dhenave
        public int IdLloji
        {
            get { return idLloji; }
            set { idLloji = value; }
        }
        public string PershkrimLloji
        {
            get { return pershkrimLloji; }
            set { pershkrimLloji = value; }
        }


        public int Prioriteti
        {
            get { return prioriteti; }
            set { prioriteti = value; }
        }
        public bool Check
        {
            get { return check ; }
            set { check  = value; }
        }  
       public int IdKonfigAmbjenteLupa
        {
            get { return idKonfigAmbjenteLupa; }
            set { idKonfigAmbjenteLupa = value; }
        }
        public string KodLupa
        {
            get { return kodLupa; }
            set { kodLupa = value; }
        }
        #endregion


    }
}

