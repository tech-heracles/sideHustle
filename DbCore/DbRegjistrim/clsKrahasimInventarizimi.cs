using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbCore.DbRegjistrim
{
   public class clsKrahasimInventarizimi
    {  
        #region Attributet
        private int id;
        private string barkodi;
        private String seriali;
        private string kodi; 
        private string pershkrimi;
        private int magazina;
        private decimal sasiaInv;
        private decimal sasiaPrg;
        private decimal diferenca;
        private decimal cmimi;
        private string grup1;
      
        private DataRow rreshti;

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
        /// Kthen/Vendos barkodin.
        /// </summary>
        public string Barkodi
        {
            get { return barkodi; }
            set { barkodi = value; }
        }


        /// <summary>
        /// Kthen/Vendos kodin e artikullit
        /// </summary>
        public string Kodi
        {
            get { return kodi; }
            set { kodi = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e magazines
        /// </summary>
        public int Magazina
        {
            get { return magazina; }
            set { magazina = value; }
        }

        /// <summary>
        /// Kthen/Vendos serialin.
        /// </summary>
        public String Seriali
        {
            get { return seriali; }
            set { seriali = value; }
        }
       
        /// <summary>
        /// Kthen/Vendos pershkrimin e artikullit
        /// </summary>
        public string Pershkrimi
        {
            get { return pershkrimi; }
            set { pershkrimi = value; }
        }

        /// <summary>
        /// Kthen/Vendos sasine ne inventar
        /// </summary>
        public decimal SasiaInv
        {
            get { return sasiaInv; }
            set { sasiaInv = value; }
        }
        /// <summary>
        /// Kthen/Vendos sasine ne program
        /// </summary>
        public decimal SasiaPrg
        {
            get { return sasiaPrg; }
            set { sasiaPrg = value; }
        }
        /// <summary>
        /// Kthen/Vendos diferenca
        /// </summary>
        public decimal Diferenca
        {
            get { return diferenca; }
            set { diferenca = value; }
        }
        public decimal Cmimi
        {
            get { return cmimi; }
            set { cmimi = value; }
        }
        /// <summary>
        /// Kthen/Vendos kodin e grupimit 1
        /// </summary>
        public string Grup1
        {
            get { return grup1; }
            set { grup1 = value; }
        }
        #endregion

        #region Konstruktoret
        /// <summary>
        /// Konstruktori i klases me parametra
        /// </summary>

        public clsKrahasimInventarizimi(int id, string seriali, string barkodi, string kodi, string pershkrimi, decimal sasiainv, decimal sasiaprg, decimal diferenca, int magazina, decimal cmimi, string grup1)
        {
            this.id = id;
            this.seriali = seriali;
            this.barkodi = barkodi;
            this.kodi = kodi;
            this.pershkrimi = pershkrimi;
            this.sasiaInv = sasiainv;
            this.sasiaPrg = sasiaprg;
            this.diferenca = diferenca;
            this.magazina = magazina;
            this.cmimi = cmimi;
            this.grup1 = grup1;
        }
        /// <summary>
        /// Konstruktori i klases pa parametra
        /// </summary>
        public clsKrahasimInventarizimi()
        {
           
        }

        #endregion
    }
}
