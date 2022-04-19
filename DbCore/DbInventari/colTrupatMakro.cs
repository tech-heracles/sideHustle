using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data.SqlClient;
using System.Data;
using DbCore.DbKontabiliteti;

namespace DbCore.DbInventari
{
    /// <summary>
    ///  Kjo eshte klasa qe mban nje liste me objekte te tipit clsTrupiMakro
    ///  dhe lejon te manipulohet kjo liste nepermjet indekseve
    ///  (Te dhenat nuk merren nga ndonje tabele)
    /// </summary>
    public class colTrupatMakro : System.Collections.Generic.List<clsTrupiMakro >
    {
        #region Konstruktoret

        /// <summary>
        /// konstruktor pa parametra
        /// </summary>
        public colTrupatMakro()
        {
        }

        /// <summary>
        /// konstruktor me 1 parameter
        /// </summary>
        /// <param name="idkoka">id e kokes se makros</param>
        public colTrupatMakro(int idkoka)
        {
            clsDatabaseInventari dbTrupiMakro = new clsDatabaseInventari();
            mbushTrupaMakrosh(dbTrupiMakro.ktheTrupatMakroSipasKokes(idkoka));
            dbTrupiMakro.Dispose();
        }

        #endregion

        #region Metoda Publike

        /// <summary>
        /// kthen objektin <see cref="DbCore.DbInventari.clsTrupiMakro"/>  qe ndodhet ne nje index te caktuar te arraylist-es
        /// </summary>
        public new clsTrupiMakro this[int index]
        {
            get { return ((clsTrupiMakro)base[index]); }
        }
        /// <summary>
        /// metoda per shtimin e nje obj clsTrupiMakro ne nje arraylist
        /// </summary>
        public bool shtoTrupiMakro(clsTrupiMakro trupiMakro)
        {
            base.Add(trupiMakro);
            if (base.Contains(trupiMakro))
                return true;
            else return false;
        }
        /// <summary>
        /// metoda per heqjen e nje obj clsTrupiMakro ne nje arraylist
        /// </summary>
        public bool fshiTrupiMakro(clsTrupiMakro trupiMakro)
        {
            base.Remove(trupiMakro);
            if (base.Contains(trupiMakro))
                return false;
            else return true;
        }
        /// <summary>
        /// metoda per heqjen e nje obj clsTrupiMakro ne nje arraylist
        /// </summary>
        public bool fshiGjitheTrupatMakro()
        {
            base.Clear();
            if (base.Count == 0)
                return true;
            else
                return false;
        }
        /// <summary>
        /// metoda per heqjen e nje obj clsTrupiMakro ne nje arraylist
        /// </summary>
        public void fshiKeteTrupiMakro(int index)
        {
            base.RemoveAt(index);
        }
        /// <summary>
        /// metoda shton nje obj te ri ne array list ne pozicionin e percaktuar nga index-i
        /// </summary>
        public void shtoTrupiMakroNeIndeksin(int index, clsTrupiMakro trupiMakro)
        {
            base.Insert(index, trupiMakro);
        }
        /// <summary>
        /// metoda gjen index-in ne liste ne te cilin ndodhet nje obj i caktuar
        /// </summary>
        public int indeksiTrupiMakrot(clsTrupiMakro trupiMakro)
        {
            return base.IndexOf(trupiMakro);
        }
        /// <summary>
        /// metoda kontrollon nqs nje obj ndodhet ne arraylist
        /// </summary>
        public bool ekzistonTrupiMakro(clsTrupiMakro trupiMakro)
        {
            if (base.Contains(trupiMakro))
                return true;
            else return false;
        }
        /// <summary>
        /// metoda qe kthen nr e obj qe ka arraylist
        /// </summary>
        public int numriTrupiMakrove()
        {
            return base.Count;
        }

        #endregion

        #region Metoda Private

        /// <summary>
        /// metoda perdoret gjeresisht ne rastet kur marrim te dhenat e kthyera ne ndonje store procedure ne formatin e nje datatable
        ///  <see cref="DbCore.DbInventari.clsTrupiMakro"/> 
        /// </summary>
        private bool mbushTrupaMakrosh(DataTable dt)
        {
            //try
            //{

                foreach (DataRow rreshti in dt.Rows)
                {
                    //clsTrupiMakro trupiMakro = new clsTrupiMakro();
                    //trupiMakro.mbushTrupiMakro(rreshti);
                    this.Add(new clsTrupiMakro(rreshti));
                }

            //}
            //catch (Exception)
            //{
            //    return false;
            //}
            return true;
        }

        #endregion
        [Obsolete("Perdor: bool mbushTrapaMakrosh(DataTable dt)", true)]
        public colTrupatMakro  mbushArrayListTrupaMakrosh(DataSet ds)
        {
            colTrupatMakro  trupiMakrot = new colTrupatMakro();
            foreach (DataRow rreshti in ds.Tables[0].Rows)
            {
                clsTrupiMakro trupiMakro = new clsTrupiMakro();

                trupiMakro.IdTrupiMakro = int.Parse(rreshti[0].ToString());
                trupiMakro.IdLlojMakro = int.Parse (rreshti[1].ToString());
                trupiMakro.IdProdukti = int.Parse (rreshti[2].ToString());
                trupiMakro.IdFunksionMakro = int.Parse(rreshti[3].ToString());
                trupiMakro.Vlera = decimal.Parse(rreshti[4].ToString());
                trupiMakro.Renditja = int.Parse(rreshti[5].ToString());
                trupiMakro.IdKokaMakro = int.Parse(rreshti[6].ToString());
                trupiMakro.Pershkrimi = rreshti[7].ToString();
                if (trupiMakro.IdLlojMakro == 1)
                {
                    DbCore.DbInventari.clsArtikulli art = new DbCore.DbInventari.clsArtikulli();
                    art.mbushArtikull(trupiMakro.IdProdukti);
                    trupiMakro.KodProdukti = art.KodArtikulli;

                    //trupiMakro.KodProdukti = new DbCore.DbInventari.clsDatabaseInventari().ktheArtikull(trupiMakro.IdProdukti)[0].KodArtikulli;
                }
                else if (trupiMakro.IdLlojMakro == 2)
                    trupiMakro.KodProdukti = new DbCore.DbInventari.clsKokaMakro(trupiMakro.IdProdukti).KodiKokaMakro;
                //trupiMakro.KodProdukti = new DbCore.DbInventari.clsDatabaseInventari().ktheKokaMakro(trupiMakro.IdProdukti)[0].KodiKokaMakro;
                else trupiMakro.KodProdukti = "";
                trupiMakrot.Add(trupiMakro);
            }
            return trupiMakrot;
        }
    }
}
