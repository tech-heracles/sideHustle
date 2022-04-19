using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data.SqlClient;
using System.Data;
using DbCore.DbRegjistrim;

namespace DbCore.DbRegjistrim
{
    /// <summary>
    ///  Kjo eshte klasa qe mban nje liste me objekte te tipit clsKategoriNivelDok
    ///  dhe lejon te manipulohet kjo liste nepermjet indekseve
    ///  (Te dhenat nuk merren nga ndonje tabele)
    /// </summary>
    public class colKategoriNiveleDok : System.Collections.Generic.List<clsKategoriNivelDok>
    {
        #region Metoda Publike

        /// <summary>
        /// kthen objektin <see cref="DbCore.DbRegjistrim.clsKategoriNivelDok"/>  qe ndodhet ne nje index te caktuar te arraylist-es
        /// </summary> 
        public new clsKategoriNivelDok this[int index]
        {
            get { return ((clsKategoriNivelDok)base[index]); }
        }

        /// <summary>
        /// mbush gjithe kategorite e niveleve te dokumentit
        /// </summary>
        /// <param name="idNdermVit">id qe lidh ndermarrjen me vitin</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert false</returns>
        public bool mbushGjitheKategoriNivelDok()
        {
            clsDatabaseRegjistrim dbKatNivelDok = new clsDatabaseRegjistrim();
            bool mbush = mbushKategoriNivelDok(dbKatNivelDok.ktheGjitheKategoriNivelDok());
            dbKatNivelDok.Dispose();
            return mbush;
        }

        /// <summary>
        /// mbush gjithe kategorite e niveleve te dokumentit
        /// </summary>
        /// <param name="idNdermVit">id qe lidh ndermarrjen me vitin</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert false</returns>
        public bool mbushKategoriNivelDokPerFormatNumrash()
        {
            clsDatabaseRegjistrim dbKatNivelDok = new clsDatabaseRegjistrim();
            bool mbush = mbushKategoriNivelDok(dbKatNivelDok.ktheKategoriNivelDokPerFormatNumrash());
           dbKatNivelDok.Dispose();
             return mbush;
        }

        public bool merriTeGjithePerImport()
        {
            clsDatabaseRegjistrim dbKatNivelDok = new clsDatabaseRegjistrim();
            bool sukses = mbushKategoriNivelDok(dbKatNivelDok.ktheKategoriDokPerImport());
            dbKatNivelDok.Dispose();           
            return sukses;
        }

        public bool merriTeGjitheKategoritePerImportSipasTeDrejtave(int idNdermarrje, int idViti, int idPerdoruesi, string komponente)
        {
            clsDatabaseRegjistrim dbKatNivelDok = new clsDatabaseRegjistrim();//E re
            bool sukses = mbushKategoriNivelDok(dbKatNivelDok.ktheKategoriDokPerImportSipasTeDrejtave(idNdermarrje, idViti, idPerdoruesi, komponente));
            dbKatNivelDok.Dispose();
            return sukses;
        }

        /// <summary>
        /// mbush gjithe kategorite e niveleve te dokumentit
        /// </summary>
        /// <param name="idNdermVit">id qe lidh ndermarrjen me vitin</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert false</returns>
        public bool mbushGjitheKategoriNivelDokPa(int idNdermVit)
        {
            clsDatabaseRegjistrim dbKatNivelDok = new clsDatabaseRegjistrim();
            bool mbush = mbushKategoriNivelDok(dbKatNivelDok.ktheGjitheKategoriNivelDokPa(idNdermVit));
            dbKatNivelDok.Dispose();
            return mbush;
        }

        /// <summary>
        /// mbush gjithe kategorite e niveleve te dokumentit
        /// </summary>
        /// <param name="idNdermVit">id qe lidh ndermarrjen me vitin</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert false</returns>
        public bool mbushGjitheKategoriNivelDokPa2(int idNdermVit)
        {
            clsDatabaseRegjistrim dbKatNivelDok = new clsDatabaseRegjistrim();
            bool mbush = mbushKategoriNivelDokPaNivele(dbKatNivelDok.ktheGjitheKategoriNivelDokPa2(idNdermVit));
            dbKatNivelDok.Dispose();
            return mbush;
        }
         public bool mbushGjitheKategoriNivelDokPaSipasSuperKat(int idsuperkat)
        {
            clsDatabaseRegjistrim dbKatNivelDok = new clsDatabaseRegjistrim();
             bool mbush = mbushKategoriNivelDokPaNivele(dbKatNivelDok.ktheGjitheKategoriNivelDokPaSipasSuperKat(idsuperkat));
             dbKatNivelDok.Dispose();
             return mbush;
        }
        /// <summary>
        /// mbush gjithe kategorite e niveleve te dokumentit pa lupat
        /// </summary>
        /// <param name="idNdermVit">id qe lidh ndermarrjen me vitin</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert false</returns>
        public bool mbushGjitheKategoriNivelDokPaLupa(int idNdermVit)
        {
            clsDatabaseRegjistrim dbKatNivelDok = new clsDatabaseRegjistrim();
            bool mbush = mbushKategoriNivelDokPaLupa(dbKatNivelDok.ktheGjitheKategoriNivelDokPaLupa(idNdermVit));
            dbKatNivelDok.Dispose();
            return mbush;
        }
        public bool mbushGjitheKategoriSipasKomponentes(int idkomponente)
        {
            clsDatabaseRegjistrim dbKatNivelDok = new clsDatabaseRegjistrim();
            bool mbush = mbushKategoriNivelDokPaLupa(dbKatNivelDok.ktheKategoriNivelDokSipasIDKomponente(idkomponente));
            dbKatNivelDok.Dispose();
            return mbush;
        }

        #endregion

        #region Metoda Private

        /// <summary>
        /// metoda perdoret gjeresisht ne rastet kur marrim te dhenat e kthyera ne ndonje store procedure ne formatin e nje datatable
        /// <see cref="DbCore.DbRegjistrim.clsKategoriNivelDok"/> 
        /// </summary>
        private bool mbushKategoriNivelDok(DataTable dt)
        {
            //try
            //{
                foreach (DataRow rreshti in dt.Rows)
                {
                    //clsKategoriNivelDok kategori = new clsKategoriNivelDok();
                    //kategori.mbushKatNivelDok(rreshti);
                    Add(new clsKategoriNivelDok(rreshti));
                }
            //}
            //catch (Exception)
            //{
            //    return false;
            //}
            return true;
        }

        private bool mbushKategoriNivelDokPaNivele(DataTable dt)
        {
            //try
            //{

                foreach (DataRow rreshti in dt.Rows)
                {
                    clsKategoriNivelDok kategori = new clsKategoriNivelDok();
                    kategori.mbushKatNivelDokPaNiv(rreshti);
                    Add(kategori);
                }

            //}
            //catch (Exception)
            //{
            //    return false;
            //    //throw;
            //}
            return true;
        }

        private bool mbushKategoriNivelDokPaLupa(DataTable dt)
        {
            //try
            //{

                foreach (DataRow rreshti in dt.Rows)
                {
                    clsKategoriNivelDok kategori = new clsKategoriNivelDok();
                    kategori.mbushKatNivelDokPaLupa(rreshti);
                    Add(kategori);
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
        //[Obsolete("Perdor: bool mbushKategoriNivelDok(DataTable dt)", true)]
        //public colKategoriNiveleDok mbushArrayListKategoriNivelDok(DataSet ds)
        //{
        //    colKategoriNiveleDok kategorite = new colKategoriNiveleDok();
        //    colNivelRegjistrimi nivelet = new colNivelRegjistrimi();

        //    foreach (DataRow rreshti in ds.Tables[0].Rows)
        //    {
        //        clsKategoriNivelDok kategori = new clsKategoriNivelDok();

        //        kategori.IdKategori = int.Parse(rreshti[0].ToString());
        //        kategori.Pershkrimi = rreshti[1].ToString();
        //        kategori.IdKomponente = int.Parse(rreshti[2].ToString());
        //        kategori.IdSuperKategori = int.Parse(rreshti[3].ToString());
        //        kategori.OColNivelRegjistrimi = new colNivelRegjistrimi();
        //        kategori.OColNivelRegjistrimi = kategori.merrNiveleRegjistrimi();
        //        kategori.OColNivelRegjistrimi = new colNivelRegjistrimi();
        //        kategorite.Add(kategori);
        //    }
        //    return kategorite;
        //}

        [Obsolete("Perdor: bool mbushKategoriNivelDokPaNivele(DataTable dt)", true)]
        public colKategoriNiveleDok mbushArrayListKategoriNivelDokPaNivele(DataSet ds)
        {
            colKategoriNiveleDok kategorite = new colKategoriNiveleDok();
            colNivelRegjistrimi nivelet = new colNivelRegjistrimi();

            foreach (DataRow rreshti in ds.Tables[0].Rows)
            {
                clsKategoriNivelDok kategori = new clsKategoriNivelDok();

                kategori.IdKategori = int.Parse(rreshti[0].ToString());
                kategori.Pershkrimi = rreshti[1].ToString();
                kategori.IdKomponente = int.Parse(rreshti[2].ToString());
                kategori.IdSuperKategori = int.Parse(rreshti[3].ToString());
                kategori.OColNivelRegjistrimi = new colNivelRegjistrimi();
                kategorite.Add(kategori);
            }
            return kategorite;
        }

    }
}
