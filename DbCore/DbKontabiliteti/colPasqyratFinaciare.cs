
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data.SqlClient;
using System.Data;
namespace DbCore.DbKontabiliteti
{ 
    /// <summary>
    ///  Kjo eshte klasa qe mban nje liste me objekte te tipit clsPasqyreFinanciare
    ///  dhe lejon te manipulohet kjo liste nepermjet indekseve
    ///  (Te dhenat nuk merren nga ndonje tabele)
    /// </summary>
    public class colPasqyratFinaciare : System.Collections.Generic.List<clsPasqyreFinanciare >
    {
        #region Konstruktor

        /// <summary>
        /// konstruktor pa parametra
        /// </summary>
        public colPasqyratFinaciare()
        {

        }

        /// <summary>
        /// konstruktor me 3 parametra
        /// </summary>
        ///<param name="tipi"> tipi</param>
        ///<param name="idndermarje">id e ndermarjes</param>
        ///<param name="idraporti">id e raportit</param>
        public colPasqyratFinaciare(string tipi, int idndermarje,int idraporti)
        {
            clsDatabaseKontabilitet dbPasqyraFinanc = new clsDatabaseKontabilitet();
            mbushPasqyrat(dbPasqyraFinanc.kthePasqyraFinaciareSipasTipitRaporte(tipi, idndermarje, idraporti));
            dbPasqyraFinanc.Dispose();
        }
        /// <summary>
        /// konstruktor me 2 parametra
        /// </summary>
        ///<param name="tipi"> tipi</param>
        ///<param name="idndermarje">id e ndermarjes</param>
   
        public colPasqyratFinaciare(string tipi, int idndermarje)
        {
            using (var dbPasqyraFinanc = new clsDatabaseKontabilitet())
                mbushPasqyrat(dbPasqyraFinanc.kthePasqyraFinaciareSipasTipit(tipi, idndermarje));
        }

        #endregion

        #region Metoda Publike

        /// <summary>
        /// kthen objektin <see cref="DbCore.DbRegjistrim.clsPasqyreFinanciare"/>  qe ndodhet ne nje index te caktuar te arraylist-es
        /// </summary> 
        public new clsPasqyreFinanciare this[int index]
        {
            get { return ((clsPasqyreFinanciare)base[index]); }
        }

        #endregion

        #region Metoda Private

        /// <summary>
        /// metoda perdoret gjeresisht ne rastet kur marrim te dhenat e kthyera ne ndonje store procedure ne formatin e nje datatable
        ///  <see cref="DbCore.DbRegjistrim.clsPasqyreFinanciare"/> 
        /// </summary>
        private bool mbushPasqyrat(DataTable dt)
        {
            //try
            //{

                foreach (DataRow rreshti in dt.Rows)
                {
                    //clsPasqyreFinanciare pasqyra = new clsPasqyreFinanciare();
                    //pasqyra.mbushPasqyraFinanciare(rreshti);
                    Add(new clsPasqyreFinanciare(rreshti));
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
        [Obsolete("Perdor: bool mbushPasqyrat(DataTable dt)", true)]
        public colPasqyratFinaciare mbushArrayListPasqyrat(DataSet ds)
        {// metoda per te mbushur nje arraylist me llogari nga nje dataset
            colPasqyratFinaciare pasqyrat = new colPasqyratFinaciare();
            foreach (DataRow rreshti in ds.Tables[0].Rows)
            {
                clsPasqyreFinanciare pasqyra = new clsPasqyreFinanciare();
                pasqyra.IdPasqyresFin = int.Parse(rreshti[0].ToString());
                pasqyra.KodiPasqyresFin = rreshti[1].ToString();
                pasqyra.EmertimiPasqyresFin = rreshti[2].ToString();
                pasqyra.TipiPasqyresFin = rreshti[3].ToString();
                pasqyra.Metoda = rreshti[4].ToString();
                    pasqyra.IdNdermarja = int.Parse(rreshti[5].ToString());
                pasqyra.Viti = int.Parse(rreshti[6].ToString());
                //pasqyra.IdNdermVit = int.Parse(rreshti[7].ToString());
                pasqyra.IdPerdoruesi = int.Parse(rreshti[7].ToString());
                pasqyrat.Add(pasqyra);
            }
            return pasqyrat;
        }
    }
}
