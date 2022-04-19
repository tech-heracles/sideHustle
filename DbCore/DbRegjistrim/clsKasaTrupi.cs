using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbCore.DbRegjistrim
{
    public class clsKasaTrupi
    {
        #region Atribute

        private int departamenti;
        private string artikulli;
        private double sasia;
        private double cmimi;
        private double zbritjeAnalitike;
        private double vleftaMeTVSH;
        private string pershkrimArtikulli;
        private string barkod;
        private int plu;
        private double perqindjeTvsh;
        #endregion

        #region Properties

        public int Departamenti
        {
            get { return departamenti; }
        }

        public string Artikulli
        {
            get { return artikulli; }
        }

        public double Sasia
        {
            get { return sasia; }
        }

        public double Cmimi
        {
            get { return cmimi; }
        }

        public double ZbritjeAnalitike
        {
            get { return zbritjeAnalitike; }
        }

        public double VleftaMeTVSH
        {
            get { return vleftaMeTVSH; }
        }

        public string PershkrimArtikulli
        {
            get { return pershkrimArtikulli; }
        }

        public string Barkod
        {
            get { return barkod; }
        }

        public int PLU
        {
            get { return plu; }
        }
        public double TVSH
        {
            get { return perqindjeTvsh; }
        }
        #endregion

        #region Konstruktore

        /// <summary>
        /// Konstruktori bosh
        /// </summary>
        public clsKasaTrupi()
        {

        }

        /// <summary>
        /// Konsturktori per krijimin e trupit me zbritje analitike
        /// </summary>
        /// <param name="departamenti"></param>
        /// <param name="artikulli"></param>
        /// <param name="sasia"></param>
        /// <param name="cmimi"></param>
        /// <param name="zbritjeAnalitike"></param>

        public clsKasaTrupi(int departamenti, string artikulli, double sasia, double cmimi, double zbritjeAnalitike, double vleftaMeTVSH, string pershkrimArtikulli, string barkod, int plu, double perqindjeTvsh)
        {
            this.departamenti = departamenti;
            this.artikulli = artikulli;
            this.sasia = sasia;
            this.cmimi = cmimi;
            this.zbritjeAnalitike = zbritjeAnalitike;
            this.vleftaMeTVSH = vleftaMeTVSH;
            this.pershkrimArtikulli = pershkrimArtikulli;
            this.barkod = barkod;
            this.plu = plu;
            this.perqindjeTvsh = perqindjeTvsh;
        }
        public clsKasaTrupi(int departamenti, string artikulli, double sasia, double cmimi, double zbritjeAnalitike)
        {
            this.departamenti = departamenti;
            this.artikulli = artikulli;
            this.sasia = sasia;
            this.cmimi = cmimi;
            this.vleftaMeTVSH = cmimi;
            this.zbritjeAnalitike = zbritjeAnalitike;
        }

        #endregion
    }
}
