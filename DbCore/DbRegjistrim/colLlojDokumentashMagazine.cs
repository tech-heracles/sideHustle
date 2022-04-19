using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data.SqlClient;
using System.Data;


namespace DbCore.DbRegjistrim
{
    /// <summary>
    ///  Kjo eshte klasa qe mban nje liste me objekte te tipit clsLlojDokumentiMagazine
    ///  dhe lejon te manipulohet kjo liste nepermjet indekseve
    ///  (Te dhenat nuk merren nga ndonje tabele)
    /// </summary>
    public  class colLlojDokumentashMagazine: System.Collections.Generic.List<clsLlojDokumentiMagazine >
    {
        #region Metoda Publike

        /// <summary>
        /// kthen objektin <see cref="DbCore.DbRegjistrim.clsLlojDokumentiMagazine"/>  qe ndodhet ne nje index te caktuar te arraylist-es
        /// </summary>
        public new clsLlojDokumentiMagazine this[int index]
        {
            get { return ((clsLlojDokumentiMagazine)base[index]); }
        }

        /// <summary>
        /// metoda per shtimin e nje obj clsLlojDokumentiMagazine ne nje arraylist
        /// </summary>
        public bool shtoLlojDokumentiMagazine(clsLlojDokumentiMagazine llojDok)
        {
            base.Add(llojDok);
            if (base.Contains(llojDok))
                return true;
            else return false;
        }

        /// <summary>
        /// metoda per heqjen e nje obj clsLlojDokumentiMagazine ne nje arraylist
        /// </summary>
        public bool fshiLlojDokumentiMagazine(clsLlojDokumentiMagazine llojDok)
        {
            base.Remove(llojDok);
            if (base.Contains(llojDok))
                return false;
            else return true;
        }

        /// <summary>
        /// metoda per heqjen e nje obj clsLlojDokumentiMagazine ne nje arraylist
        /// </summary>
        public bool fshiGjitheLlojDokumentashMagazine()
        {
            base.Clear();
            if (base.Count == 0)
                return true;
            else
                return false;
        }

        /// <summary>
        /// metoda per heqjen e nje obj clsLlojDokumentiMagazine ne nje arraylist
        /// </summary>
        public void fshiKeteFunksionMakro(int index)
        {
            base.RemoveAt(index);
        }

        /// <summary>
        /// metoda shton nje obj te ri ne array list ne pozicionin e percaktuar nga index-i
        /// </summary>
        public void shtoLlojDokumentiMagazineNeIndeksin(int index, clsLlojDokumentiMagazine llojDok)
        {
            base.Insert(index, llojDok);
        }

        /// <summary>
        /// metoda gjen index-in ne liste ne te cilin ndodhet nje obj i caktuar
        /// </summary>
        public int indeksiLlojDokumentiMagazine(clsLlojDokumentiMagazine llojDok)
        {
            return base.IndexOf(llojDok);
        }

        /// <summary>
        /// metoda kontrollon nqs nje obj ndodhet ne arraylist
        /// </summary>
        public bool ekzistonLlojDokumentiMagazine(clsLlojDokumentiMagazine llojDok)
        {
            if (base.Contains(llojDok))
                return true;
            else return false;
        }

        /// <summary>
        /// metoda qe kthen nr e obj qe ka arraylist
        /// </summary>
        public int numriLlojDokumentashMagazine()
        {
            return base.Count;
        }

        /// <summary>
        /// mbush gjithe lloj dokumentash magazine
        /// </summary>
        /// <returns></returns>
        public bool mbushGjitheLlojDokumentashMagazine()
        {
            clsDatabaseRegjistrim dbLlojDokumetashMagazine = new clsDatabaseRegjistrim();
            bool mbush = mbushDokumentatMagazine(dbLlojDokumetashMagazine.ktheGjitheLlojDokumentashMagazine());
            dbLlojDokumetashMagazine.Dispose();
            return mbush;
        }

        #endregion

        #region Metoda Private

        /// <summary>
        /// metoda perdoret gjeresisht ne rastet kur marrim te dhenat e kthyera ne ndonje store procedure ne formatin e nje datatable
        /// <see cref="DbCore.DbRegjistrim.clsLlojDokumentiMagazine"/> 
        /// </summary>
        private bool mbushDokumentatMagazine(DataTable dt)
        {
            //try
            //{

                foreach (DataRow rreshti in dt.Rows)
                {
                    //clsLlojDokumentiMagazine llojDok = new clsLlojDokumentiMagazine();
                    //llojDok.mbushLlojDokumentiMagazine(rreshti);
                    Add(new clsLlojDokumentiMagazine(rreshti));
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
        [Obsolete("Perdor: bool mbushDokumentatMagazine(DataTable dt)", true)]
        public colLlojDokumentashMagazine mbushArrayListLlojDokumentashMagazine(DataSet ds)
        {
            colLlojDokumentashMagazine llojeDok = new colLlojDokumentashMagazine();
            foreach (DataRow rreshti in ds.Tables[0].Rows)
            {
                clsLlojDokumentiMagazine llojDok = new clsLlojDokumentiMagazine();
                llojDok.IdLlojDokumentiMagazine = int.Parse(rreshti[0].ToString());
                llojDok.Pershkrimi = rreshti[1].ToString();
                llojeDok.Add(llojDok);
            }
            return llojeDok;
        }
    }

}
