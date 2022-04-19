using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DbCore.DbRegjistrim
{
    /// <summary>
    ///  Kjo eshte klasa qe mban nje liste me objekte te tipit clsFleteDoganoreKoka
    ///  dhe lejon te manipulohet kjo liste nepermjet indekseve
    ///  (Te dhenat nuk merren nga ndonje tabele)
    /// </summary>
    public class colFleteDoganoreKoka : System.Collections.Generic.List<clsFleteDoganoreKoka>
    {
        #region Konstruktor

        /// <summary>
        /// konstruktor pa parametera
        /// </summary>
        public colFleteDoganoreKoka()
        {
        }

        /// <summary>
        /// konstruktor me  1 parameter
        /// </summary>
        /// <param name="idNdermVit">id qe lidh ndermarrjen me vitin</param>
        public colFleteDoganoreKoka(int idNdermVit)
        {
            clsDatabaseRegjistrim dbFleteDogKoka = new clsDatabaseRegjistrim();
            mbushFletatDoganoreKoka(dbFleteDogKoka.ktheFleteDoganoreKokaSipasNdermVit(idNdermVit));
            dbFleteDogKoka.Dispose();
        }
        public static DataTable merrFleteDoganoreDT(int idndermvit, int idperdoruesi, string datanga, string dataderi)
        {
            clsDatabaseRegjistrim dbartikuj = new clsDatabaseRegjistrim();
            DataTable dt = dbartikuj.merrFleteDoganoreDT(idndermvit, idperdoruesi, datanga, dataderi);
            dbartikuj.Dispose();
            return dt;
        }
        #endregion

        #region Metoda Publike

        /// <summary>
        /// kthen objektin <see cref="DbCore.DbRegjistrim.clsFleteDoganoreKoka"/>  qe ndodhet ne nje index te caktuar te arraylist-es
        /// </summary> 
        public new clsFleteDoganoreKoka this[int index]
        {
            get { return ((clsFleteDoganoreKoka)base[index]); }
        }

        #endregion

        #region Metoda Private

        /// <summary>
        /// metoda perdoret gjeresisht ne rastet kur marrim te dhenat e kthyera ne ndonje store procedure ne formatin e nje datatable
        ///  <see cref="DbCore.DbRegjistrim.clsFleteDoganoreKoka"/> 
        /// </summary>
        private bool mbushFletatDoganoreKoka(DataTable dt)
        {
            //try
            //{

                foreach (DataRow rreshti in dt.Rows)
                {
                    //clsFleteDoganoreKoka koka = new clsFleteDoganoreKoka();
                    //koka.mbushFleteDoganoreKok(rreshti);
                    Add(new clsFleteDoganoreKoka(rreshti));
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
        [Obsolete("Perdor: bool mbushFletatDoganoreKoka(DataTable dt)", true)]
        public colFleteDoganoreKoka mbushArrayListFleteDoganoreKoka(DataSet ds)
        {
            colFleteDoganoreKoka kokat = new colFleteDoganoreKoka();
            foreach (DataRow rreshti in ds.Tables[0].Rows)
            {
                clsFleteDoganoreKoka koka = new clsFleteDoganoreKoka();
                koka.IdFleteDoganoreKoka = int.Parse(rreshti[0].ToString());
                koka.NrDok = rreshti[1].ToString();
                koka.DtDok = DateTime.Parse(rreshti[2].ToString());
                koka.DtRegjistrimi = DateTime.Parse(rreshti[3].ToString());
                koka.IdMonedha = int.Parse(rreshti[4].ToString());
                koka.Kursi = decimal.Parse(rreshti[5].ToString());
                koka.VlFaturuar = decimal.Parse(rreshti[6].ToString());
                koka.VlMb = decimal.Parse(rreshti[7].ToString());
                koka.VlTransport = decimal.Parse(rreshti[8].ToString());
                koka.VlSiguracion = decimal.Parse(rreshti[9].ToString());
                koka.VlTjera = decimal.Parse(rreshti[10].ToString());
                koka.VlDoganim = decimal.Parse(rreshti[11].ToString());
                koka.IdStatusDok = int.Parse(rreshti[12].ToString());
                koka.IdNdermarrje = int.Parse(rreshti[13].ToString());
                koka.IdNdermarrjeVit = int.Parse(rreshti[14].ToString());
                koka.IdKonfigAmbjente = int.Parse(rreshti[15].ToString());
                koka.IdNivel = int.Parse(rreshti[16].ToString());
                koka.IdDokNga = int.Parse(rreshti[17].ToString());
                koka.IdNivelGjenerues = int.Parse(rreshti[18].ToString());
                koka.IdKonfigGjenerues = int.Parse(rreshti[19].ToString());
                koka.IdGjenerues = int.Parse(rreshti[20].ToString());
                koka.OColTrupi = new colFleteDoganoreTrupi();
                //koka.OColDetajim = new colFleteDoganoreDetajim();
                koka.OColTaksat = new colFleteDoganoreTaksa();
                kokat.Add(koka);
            }
            return kokat;
        }
    }
}
