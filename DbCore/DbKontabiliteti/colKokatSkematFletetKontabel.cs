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
    ///  Kjo eshte klasa qe mban nje liste me objekte te tipit clsKokaSkemaFleteKontabel
    ///  dhe lejon te manipulohet kjo liste nepermjet indekseve
    ///  (Te dhenat nuk merren nga ndonje tabele)
    /// </summary>
    public class colKokatSkematFletetKontabel : System.Collections.Generic.List<clsKokaSkemaFleteKontabel>
    {

        #region Konstruktor

        /// <summary>
        /// konstruktor pa parametra
        /// </summary>
        public colKokatSkematFletetKontabel()
        {
        }

        /// <summary>
        /// konstruktor me 2 parametra
        /// </summary>
        /// <param name="idNdermarje">id  ndermarrjen </param>
        /// <param name="idperdoruesi">id perdoruesi</param>
        public colKokatSkematFletetKontabel(int idNdermarje, int idperdoruesi)
        {
            clsDatabaseKontabilitet dbKokaSkemeFleteKont = new clsDatabaseKontabilitet();
            mbushKokaSkematFletetKont(dbKokaSkemeFleteKont.ktheGjitheSkematFletetKontabelSipasNdermarjesAndAutorizimit(idNdermarje, idperdoruesi));
            dbKokaSkemeFleteKont.Dispose();
        }

        #endregion

        #region Metoda Publike

        /// <summary>
        /// kthen objektin <see cref="DbCore.DbKontabiliteti.clsKokaSkemaFleteKontabel"/>  qe ndodhet ne nje index te caktuar te arraylist-es
        /// </summary>
        public new clsKokaSkemaFleteKontabel this[int index]
        {
            get { return ((clsKokaSkemaFleteKontabel)base[index]); }
        }

        /// <summary>
        /// metoda per shtimin e nje objekti clsKokaFleteKontabel ne nje arraylist
        /// </summary>
        public bool shtoKokaSkemaFK(clsKokaSkemaFleteKontabel kokaSkemaFK)
        {
            base.Add(kokaSkemaFK);
            if (base.Contains(kokaSkemaFK))
                return true;
            else return false;
        }

        /// <summary>
        /// metoda per fshirjen e nje objekti clsKokaFleteKontabel nga nje arraylist
        /// </summary>
        public bool fshiKokaSkemaFK(clsKokaSkemaFleteKontabel kokaSkemaFK)
        {
            base.Remove(kokaSkemaFK);
            if (base.Contains(kokaSkemaFK))
                return false;
            else return true;
        }

        /// <summary>
        /// metoda per fshirjen e gjithe objekteve clsKokaFleteKontabel nga nje arraylist
        /// </summary>
        public bool fshiGjitheKokatSkemaFK()
        {
            base.Clear();
            if (base.Count == 0)
                return true;
            else
                return false;
        }

        /// <summary>
        /// metoda per fshirjen e nje objekti clsKokaFleteKontabel ne nje indeks te caktuar ne nje arraylist
        /// </summary>
        public void fshiKeteKokaSkemaFK(int index)
        {
            base.RemoveAt(index);
        }

        /// <summary>
        /// metoda per shtimin e nje objekti clsKokaFleteKontabel ne nje indeks te caktuar ne nje arraylist
        /// </summary>
        public void shtoKokaSkemaFKNeIndeksin(int index, clsKokaSkemaFleteKontabel kokaSkemaFK)
        {
            base.Insert(index, kokaSkemaFK);
        }

        /// <summary>
        /// metoda kthen indeksin e nje objekti clsKokaFleteKontabel ne nje arraylist
        /// </summary>
        public int indeksiKokaSkemaFK(clsKokaSkemaFleteKontabel kokaSkemaFK)
        {
            return base.IndexOf(kokaSkemaFK);
        }

        /// <summary>
        /// metoda kontrollon ekzistencen e nje objekti clsKokaFleteKontabel ne nje arraylist
        /// </summary>
        public bool ekzistonKokaSkemaFK(clsKokaSkemaFleteKontabel kokaSkemaFK)
        {
            if (base.Contains(kokaSkemaFK))
                return true;
            else return false;
        }

        /// <summary>
        /// metoda kthen numrin e objekteve clsKokaFleteKontabel ne nje arraylist
        /// </summary>
        public int numriKokaSkemaFKve()
        {
            return base.Count;
        }

        #endregion
        
        #region Metoda Private

        /// <summary>
        /// metoda perdoret gjeresisht ne rastet kur marrim te dhenat e kthyera ne ndonje store procedure ne formatin e nje datatable
        ///  <see cref="DbCore.DbKontabiliteti.clsKokaSkemaFleteKontabel"/> 
        /// </summary>
        private bool mbushKokaSkematFletetKont(DataTable dt)
        {
            //try
            //{

                foreach (DataRow rreshti in dt.Rows)
                {
                    //clsKokaSkemaFleteKontabel kokaSkemaFK = new clsKokaSkemaFleteKontabel();
                    //kokaSkemaFK.mbushKokaSkemaFleteKont(rreshti);
                    Add(new clsKokaSkemaFleteKontabel(rreshti));
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
        [Obsolete("Perdor: bool mbushKokaSkematFletetKont(DataTable dt)", true)]
        public colKokatSkematFletetKontabel mbushArrayListKokaSkemaFKsh(DataSet ds)
        {
            colKokatSkematFletetKontabel kokatSkemaFK = new colKokatSkematFletetKontabel();
            foreach (DataRow rreshti in ds.Tables[0].Rows)
            {
                clsKokaSkemaFleteKontabel kokaSkemaFK = new clsKokaSkemaFleteKontabel();

                kokaSkemaFK.IdKokaSkemaFK = int.Parse(rreshti[0].ToString());
                kokaSkemaFK.KodiKokaSkemaFK = rreshti[1].ToString();
                kokaSkemaFK.PershkrimiKokaSkemaFK = rreshti[2].ToString();

                //colLlojeBuxhetesh colLloj = new DbCore.DbKontabiliteti.clsDatabaseKontabilitet().merrLlojBuxhetiSipasKodit("SkematFleteKontabel");
               
                kokaSkemaFK.IdPerdoruesi = int.Parse(rreshti[3].ToString());
                kokaSkemaFK.IdNdermarje = int.Parse(rreshti[4].ToString());

                kokatSkemaFK.Add(kokaSkemaFK);
            }
            return kokatSkemaFK;
        }

    }
}