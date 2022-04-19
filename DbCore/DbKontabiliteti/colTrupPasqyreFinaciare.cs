using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DbCore.DbKontabiliteti
{
    /// <summary>
    ///  Kjo eshte klasa qe mban nje liste me objekte te tipit clsTrupPasqyreFinanciare
    ///  dhe lejon te manipulohet kjo liste nepermjet indekseve
    ///  (Te dhenat nuk merren nga ndonje tabele)
    /// </summary>
    public class colTrupPasqyreFinaciare : System.Collections.Generic.List<clsTrupPasqyreFinanciare>
    {
        #region Konstruktoret

        /// <summary>
        /// konstruktor pa parametra
        /// </summary>
        public colTrupPasqyreFinaciare()
        {
        }

        /// <summary>
        /// konstruktor me 1 parameter
        /// </summary>
        /// <param name="id">id e kokes se pasqyres financiare </param>
        public colTrupPasqyreFinaciare(int id)
        {
            clsDatabaseKontabilitet dbTrupPasFinanc = new clsDatabaseKontabilitet();
            mbushTrupPasqyratFinanciare(dbTrupPasFinanc.kthePasqyraFinaciareTrupiSipasIdKoka(id));
            dbTrupPasFinanc.Dispose();
        }

        /// <summary>
        /// konstruktor me 2 parametra
        /// </summary>
        /// <param name="lloji">lloji i pasqyres financiare</param>
        /// <param name="idkoka">id e kokes se pasqyres financiare</param>
        public colTrupPasqyreFinaciare(string lloji, int idkoka)
        {
            clsDatabaseKontabilitet dbTrupPasFinanc = new clsDatabaseKontabilitet();
            mbushTrupPasqyratFinanciare(dbTrupPasFinanc.kthePasqyraFinaciareTrupiSipasLlojit(lloji, idkoka));
            dbTrupPasFinanc.Dispose();
        }

        #endregion

        #region Metoda Publike

        /// <summary>
        /// kthen objektin <see cref="DbCore.DbKontabiliteti.clsTrupPasqyreFinanciare"/>  qe ndodhet ne nje index te caktuar te arraylist-es
        /// </summary>
        public new clsTrupPasqyreFinanciare this[int index]
        {
            get { return ((clsTrupPasqyreFinanciare)base[index]); }
        }

        #endregion

        #region Metoda Private

        /// <summary>
        /// metoda perdoret gjeresisht ne rastet kur marrim te dhenat e kthyera ne ndonje store procedure ne formatin e nje datatable
        ///  <see cref="DbCore.DbKontabiliteti.clsTrupPasqyreFinanciare"/> 
        /// </summary>
        private bool mbushTrupPasqyratFinanciare(DataTable dt)
        {
            //try
            //{

                foreach (DataRow rreshti in dt.Rows)
                {
                    //clsTrupPasqyreFinanciare trupi = new clsTrupPasqyreFinanciare();
                    //trupi.mbushTrupPasqyreFinanciare(rreshti);
                    Add(new clsTrupPasqyreFinanciare(rreshti));
                }

            //}
            //catch (Exception)
            //{
            //    return false;
            //    //throw;
            //}
            return true;
        }

        #endregion
        [Obsolete("Perdor: bool mbushTrupPasqyratFinanciare(DataTable dt)", true)]
        public colTrupPasqyreFinaciare mbushArrayListPasqyreFinanciare(DataSet ds)
        {
            colTrupPasqyreFinaciare colTrup = new colTrupPasqyreFinaciare();
            foreach (DataRow rreshti in ds.Tables[0].Rows)
            {
                clsTrupPasqyreFinanciare trupi = new clsTrupPasqyreFinanciare();

                trupi.IdTrupi = int.Parse(rreshti[0].ToString());
                trupi.IdKoka = int.Parse(rreshti[1].ToString());
                trupi.PershkrimiZerit = rreshti[2].ToString();
                trupi.PrindiZerit = rreshti[3].ToString();
                trupi.NiveliZerit = int.Parse(rreshti[4].ToString());
                trupi.LlojiZerit = rreshti[5].ToString();
                trupi.GjeneroTotal = int.Parse(rreshti[6].ToString());
                colTrup.Add(trupi);
            }
            return colTrup;
        }
    }
}
