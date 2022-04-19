using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DbCore.DbRegjistrim
{
    /// <summary>
    ///  Kjo eshte klasa qe mban nje liste me objekte te tipit clsShperndarjeShpenzimeKoka
    ///  dhe lejon te manipulohet kjo liste nepermjet indekseve
    ///  (Te dhenat nuk merren nga ndonje tabele)
    /// </summary>
    public class colShperndarjeShpenzimeKoka : System.Collections.Generic.List<clsShperndarjeShpenzimeKoka>
    {
        #region Konstruktoret

        /// <summary>
        /// konstruktor pa parametra
        /// </summary>
        public colShperndarjeShpenzimeKoka()
        {
        }
        
        /// <summary>
        /// konstruktor me 1 parameter
        /// </summary>
        /// <param name="idNdermVit">id e qe lidh ndermarrjen me vitin</param>
        public colShperndarjeShpenzimeKoka(int idNdermVit)
        {
            clsDatabaseRegjistrim dbShperndShpenKoka = new clsDatabaseRegjistrim();
            mbushShperndarjeShpenzimeshKoka(dbShperndShpenKoka.ktheGjitheShperndarjeShpenzimeshKoka(idNdermVit));
            dbShperndShpenKoka.Dispose();
        }

        #endregion

        #region Metoda Publike

        /// <summary>
        /// kthen objektin <see cref="DbCore.DbRegjistrim.clsShperndarjeShpenzimeKoka"/>  qe ndodhet ne nje index te caktuar te arraylist-es
        /// </summary> 
        public new clsShperndarjeShpenzimeKoka this[int index]
        {
            get { return ((clsShperndarjeShpenzimeKoka)base[index]); }
        }

        public static DataTable merrSHSHDT(int idndermvit, int idperdoruesi)
        {
            clsDatabaseRegjistrim dbartikuj = new clsDatabaseRegjistrim();
            DataTable dt = dbartikuj.merrSHSHDT(idndermvit, idperdoruesi);
            dbartikuj.Dispose();
            return dt;
        }

        public static DataTable ktheDokShperndarjeShpenzimiPerEksport(int idNdermarrje, int idFormatImporti)
        {
            using (clsDatabaseRegjistrim db = new clsDatabaseRegjistrim())
                return db.ktheDokShperndarjeShpenzimiPerEksport(idNdermarrje, idFormatImporti);
        }
        #endregion

        #region Metoda Private

        /// <summary>
        /// metoda perdoret gjeresisht ne rastet kur marrim te dhenat e kthyera ne ndonje store procedure ne formatin e nje datatable
        /// <see cref="DbCore.DbRegjistrim.clsShperndarjeShpenzimeKoka"/> 
        /// </summary>
        private bool mbushShperndarjeShpenzimeshKoka(DataTable dt)
        {
            //try
            //{

                foreach (DataRow rreshti in dt.Rows)
                {
                    //clsShperndarjeShpenzimeKoka koka = new clsShperndarjeShpenzimeKoka();
                    //koka.mbushShperndarjeShpenzKok(rreshti);
                    Add(new clsShperndarjeShpenzimeKoka(rreshti));
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
        [Obsolete("Perdor: bool mbushShperndarjeShpenzimeshKoka(DataTable dt)", true)]
        public colShperndarjeShpenzimeKoka mbushArrayListShperndarjeShpenzimeKoka(DataSet ds)
        {
            colShperndarjeShpenzimeKoka kokat = new colShperndarjeShpenzimeKoka();

            foreach (DataRow rreshti in ds.Tables[0].Rows)
            {
                clsShperndarjeShpenzimeKoka koka = new clsShperndarjeShpenzimeKoka();

                koka.IdKokaShperndarjeShpenz = int.Parse(rreshti[0].ToString());
                koka.NrDok = rreshti[1].ToString();
                koka.DtDok = DateTime.Parse(rreshti[2].ToString());
                koka.DtRegjistrimi = DateTime.Parse(rreshti[3].ToString());
                koka.Shenime = rreshti[4].ToString();
                koka.VleraTotale = double.Parse(rreshti[5].ToString());
                koka.IdStatusDok = int.Parse(rreshti[6].ToString());
                koka.IdNdermarrje = int.Parse(rreshti[7].ToString());
                koka.IdNdermarrjeVit = int.Parse(rreshti[8].ToString());
                koka.IdNivel = int.Parse(rreshti[9].ToString());
                koka.IdKonfigAmbjente = int.Parse(rreshti[10].ToString());
                koka.IdDokNga = int.Parse(rreshti[11].ToString());
                koka.IdNivelGjenerues = int.Parse(rreshti[12].ToString());
                koka.IdKonfigGjenerues = int.Parse(rreshti[13].ToString());
                koka.IdGjenerues = int.Parse(rreshti[14].ToString());

                koka.OColTrupi = new colShperndarjeShpenzimeTrupi();
                koka.OColFaturat = new colShperndarjeShpenzimeFaturat();
                kokat.Add(koka);
            }
            return kokat;
        }

    }
}
