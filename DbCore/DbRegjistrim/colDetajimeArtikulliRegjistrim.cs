using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DbCore.DbRegjistrim
{
    /// <summary>
    ///  Kjo eshte klasa qe mban nje liste me objekte te tipit clsDetajimArtikulliRegjistrim
    ///  dhe lejon te manipulohet kjo liste nepermjet indekseve
    ///  (Te dhenat nuk merren nga ndonje tabele)
    /// </summary>
    public class colDetajimeArtikulliRegjistrim : System.Collections.Generic.List<clsDetajimArtikulliRegjistrim>
    {
        #region Konstruktor

        /// <summary>
        /// konstruktor pa parametra
        /// </summary>
        public colDetajimeArtikulliRegjistrim()
        {
        }

        /// <summary>
        /// konstruktor me 1 parameter
        /// </summary>
        /// <param name="idTrupiShitje">id e trupit te shitjes</param>
        public colDetajimeArtikulliRegjistrim(int idTrupiShitje)
        {
            clsDatabaseRegjistrim dbDetArt = new clsDatabaseRegjistrim();
            mbushDetajimeRegjistrime(dbDetArt.ktheDetajimetSipasIdTrupiShitje(idTrupiShitje));
            dbDetArt.Dispose();
        }

        #endregion

        #region Metoda Publike

        /// <summary>
        /// kthen objektin <see cref="DbCore.DbRegjistrim.clsDetajimArtikulliRegjistrim"/>  qe ndodhet ne nje index te caktuar te arraylist-es
        /// </summary> 
        public new clsDetajimArtikulliRegjistrim this[int index]
        {
            get { return ((clsDetajimArtikulliRegjistrim)base[index]); }
        }

        #endregion

        #region Metoda Private

        /// <summary>
        /// metoda perdoret gjeresisht ne rastet kur marrim te dhenat e kthyera ne ndonje store procedure ne formatin e nje datatable
        ///  <see cref="DbCore.DbRegjistrim.clsDetajimArtikulliRegjistrim"/> 
        /// </summary>
        private bool mbushDetajimeRegjistrime(DataTable dt)
        {
            //try
            //{

                foreach (DataRow rreshti in dt.Rows)
                {
                    //clsDetajimArtikulliRegjistrim det = new clsDetajimArtikulliRegjistrim();
                    //det.mbushDetajimArtikullRegjistrim(rreshti);
                    Add(new clsDetajimArtikulliRegjistrim(rreshti));
                }

            //}
            //catch (Exception)
            //{
            //    return false;
            //    //throw;
            //}
            return true;
        }

        /// <summary>
        /// metoda perdoret gjeresisht ne rastet kur marrim te dhenat e kthyera ne ndonje store procedure ne formatin e nje datatable
        ///  <see cref="DbCore.DbRegjistrim.clsDetajimArtikulliRegjistrim"/> 
        /// </summary>
        private bool mbushDetajimeRegjistrimeMagazine(DataTable dt)
        {
            try
            {

                foreach (DataRow rreshti in dt.Rows)
                {
                    clsDetajimArtikulliRegjistrim det = new clsDetajimArtikulliRegjistrim();
                    det.mbushDetajimRegjistrimMagazine(rreshti);
                    Add(det);
                }

            }
            catch (Exception)
            {
                return false;
                //throw;
            }
            return true;
        }

        #endregion
        [Obsolete("Perdor: bool mbushDetajimeRegjistrime(DataTable dt)", true)]
        public colDetajimeArtikulliRegjistrim mbushArrayListDetajimeRegjistrime(DataSet ds)
        {
            colDetajimeArtikulliRegjistrim detajime = new colDetajimeArtikulliRegjistrim();
            foreach (DataRow rreshti in ds.Tables[0].Rows)
            {
                clsDetajimArtikulliRegjistrim det = new clsDetajimArtikulliRegjistrim();

                det.IdDetArtRegjistrim = int.Parse(rreshti[0].ToString());
                det.IdTrupiShitje = int.Parse(rreshti[1].ToString());
                det.IdDetajim = int.Parse(rreshti[2].ToString());
                det.Sasia = double.Parse(rreshti[3].ToString());
                det.Vlefta = double.Parse(rreshti[4].ToString());
                det.Cmimi = double.Parse(rreshti[5].ToString());

                detajime.Add(det);
            }
            return detajime;
        }

        [Obsolete("Perdor: bool mbushDetajimeRegjistrimeMagazine(DataTable dt)", true)]
        public colDetajimeArtikulliRegjistrim mbushArrayListDetajimeRegjistrimeMagazine(DataSet ds)
        {
            colDetajimeArtikulliRegjistrim detajime = new colDetajimeArtikulliRegjistrim();
            foreach (DataRow rreshti in ds.Tables[0].Rows)
            {
                clsDetajimArtikulliRegjistrim det = new clsDetajimArtikulliRegjistrim();

                det.IdDetArtRegjistrim = int.Parse(rreshti[0].ToString());
                det.IdTrupiShitje = int.Parse(rreshti[1].ToString());
                det.IdDetajim = int.Parse(rreshti[2].ToString());
                det.Sasia = double.Parse(rreshti[3].ToString());
                det.Vlefta = double.Parse(rreshti[4].ToString());
                det.Cmimi = double.Parse(rreshti[5].ToString());
                det.SasiaProgresive = double.Parse(rreshti[6].ToString());
                det.VleftaProgresive = double.Parse(rreshti[7].ToString());
                det.Magazina = int.Parse(rreshti[8].ToString());
                det.Data = DateTime.Parse(rreshti[9].ToString());

                detajime.Add(det);
            }
            return detajime;
        }
    }
}
