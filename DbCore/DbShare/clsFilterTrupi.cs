using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DbCore.DbAdmin;
using System.Data;

namespace DbCore.DbShare
{
    public class clsFilterTrupi
    {
        #region Atribute

        private int idTrupiFilter;
        private int idKokaFilter;
        private int idKontrolli;
        private string vlera;        
        //private String veprim1;
        //private String vlera1;
        //private String lidhesaLogjike;

        #endregion

        #region Konstruktoret

        /// <summary>
        /// Thirret kur nuk dihet id-ja e e filtertrupit qe po krijohet
        /// </summary>
        /// <param name="idKontrolli"></param>
        /// <param name="vlera"></param>
        public clsFilterTrupi(int idKontrolli, String vlera)
        {
            this.idKontrolli = idKontrolli;
            this.vlera = vlera;
        }        

        public clsFilterTrupi()
        {
        }

        #endregion

        #region Properties

        public int IdTrupiFilter
        {
            get { return idTrupiFilter; }
            set { idTrupiFilter = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int IdKokaFilter
        {
            get { return idKokaFilter; }
            set { idKokaFilter = value; }
        }

        public int IdKontrolli
        {
            get { return idKontrolli; }
            set { idKontrolli = value; }
        }

        public String Vlera
        {
            get { return vlera; }
            set { vlera = value; }
        }

        //public String LidhesaLogjike
        //{
        //    get { return lidhesaLogjike; }
        //    set { lidhesaLogjike = value; }
        //}

        #endregion

        #region Metoda Publike

        public clsMesazh ruaj()
        {
            clsDatabaseShare data = new clsDatabaseShare();
            clsMesazh u_ruajt = data.ruajFilterTrupi(idKokaFilter, idKontrolli, vlera);
            data.Dispose();
            return u_ruajt;
        }

        //public clsMesazh modifiko()
        //{
        //    //clsDatabaseShare data = new clsDatabaseShare();
        //    //clsMesazh u_modifikua = data.modifikoFilterTrupi(this);
        //    //return u_modifikua;
        //}

        /// <summary>
        /// Fshin filter trupin.
        /// </summary>
        /// <returns></returns>
        public clsMesazh fshi()
        {
            clsDatabaseShare data = new clsDatabaseShare();
            clsMesazh u_fshi = fshi(data);
            data.Dispose();
            return u_fshi;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data">clsdatabaseshare nese do te perdoresh transaksion</param>
        /// <returns></returns>
        public clsMesazh fshi(clsDatabaseShare data)
        {
            //if (data == null)
            //    data = new clsDatabaseShare();
            clsMesazh u_fshi = data.fshiFilterTrupi(this.IdTrupiFilter);
            return u_fshi;
        }

        //public colFilterTrupi merrFilterTrupiRaportDefault(int rapid)
        //{
        //    clsDatabaseShare data = new clsDatabaseShare();
        //    return data.merrFilterTrupiRapDefault(rapid);
        //}

        //public colFilterTrupi merrFilterTrupin(clsFilterKoka oKoka)
        //{
        //    clsDatabaseShare data = new clsDatabaseShare();
        //    return data.merrFilterTrupin(oKoka);
        //}

        #endregion

        #region Metoda Internal

        /// <summary>
        /// mbush clsfiltertrupi
        /// </summary>
        /// <param name="rreshti"></param>
        /// <returns>kthe true nese mbushja kryhet me sukses, false perndryshe</returns>
        internal bool mbushFilterTrupi(DataRow rreshti)
        {
            if (rreshti != null)
            {
                try
                {
                    int.TryParse(rreshti["IDTRUPIFILTER"].ToString(), out idTrupiFilter);
                    int.TryParse(rreshti["IDKOKAFILTER"].ToString(), out idKokaFilter);
                    int.TryParse(rreshti["IDKONTROLLI"].ToString(), out idKontrolli);
                    vlera = rreshti["VLERA"].ToString();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
            else
                return false;
        }

        /// <summary>
        /// Krijon nje DataRow me te dhenat e objektit
        /// </summary>
        /// <returns>Kthen DataRow me te dhenat e objektit, null perndryshe</returns>
        internal DataRow mbushFilterTrupi()
        {
            try
            {
                DataTable filtrat = new DataTable("filtrat");
                filtrat.Columns.Add("IDTRUPIFILTER", (new System.Decimal()).GetType());
                filtrat.Columns.Add("IDKOKAFILTER", (new System.Decimal()).GetType());
                filtrat.Columns.Add("IDKONTROLLI", (new System.Decimal()).GetType());
                filtrat.Columns.Add("VLERA", ("").GetType());
                DataRow rreshti = filtrat.NewRow();

                rreshti["IDTRUPIFILTER"] = this.idTrupiFilter;
                rreshti["IDKOKAFILTER"] = this.idKokaFilter;
                rreshti["IDKONTROLLI"] = this.idKontrolli;
                rreshti["VLERA"] = this.vlera;
                return rreshti;
            }
            catch (Exception)
            {
                return null;
            }
        }

        #endregion
    }
}
