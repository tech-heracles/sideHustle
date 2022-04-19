using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data.SqlClient;
using System.Data;

namespace DbCore.DbAdmin
{
    /// <summary>
    ///  Kjo eshte klasa qe sherben per objektet qe perfaqsojne veprimet e te drejtave (shtim, modifikim, fshirje etj)
    ///  (Te dhenat  merren nga tabela : T_DREJTAVEPRIM)
    /// </summary>
    public class colACRNumratAutomatike : System.Collections.Generic.List<clsACRNumraAutomatike>
    {
        #region Konstruktoret

        public colACRNumratAutomatike()
        {
        }

        public colACRNumratAutomatike(int idndermarje)
        {
            clsDatabaseAdmin data = new clsDatabaseAdmin();
            mbushACRNumratAutomatike(data.ktheGjitheACRNumraAutomatike(idndermarje));

        }

        #endregion

        #region Metoda Publike

        public new clsACRNumraAutomatike this[int index]
        {
            get { return ((clsACRNumraAutomatike)base[index]); }
        }

        public bool shtoACRNumraAutomatike(clsACRNumraAutomatike ACRNumraAutomatike)
        {// metoda per shtimin e nje ACRNumraAutomatike ne nje arraylist
            base.Add(ACRNumraAutomatike);
            if (base.Contains(ACRNumraAutomatike))
                return true;
            else return false;
        }

        public bool fshiACRNumraAutomatike(clsACRNumraAutomatike ACRNumraAutomatike)
        {// metoda per heqjen e nje ACRNumraAutomatike ne nje arraylist
            base.Remove(ACRNumraAutomatike);
            if (base.Contains(ACRNumraAutomatike))
                return false;
            else return true;
        }

        public bool fshiGjitheACRNumraAutomatike()
        {// metoda per heqjen e te gjithe ACRNumraAutomatike nga arraylist
            base.Clear();
            if (base.Count == 0)
                return true;
            else
                return false;
        }

        public void fshiKeteACRNumraAutomatike(int index)
        {// metoda per heqjen e nje ACRNumraAutomatike ne nje arraylist ne nje pozicion te caktuar
            base.RemoveAt(index);
        }

        public void shtoACRNumraAutomatikeNeIndeksin(int index, clsACRNumraAutomatike ACRNumraAutomatike)
        {// metoda per shtimin e nje ACRNumraAutomatike ne nje arraylist ne nje pozicion te caktuar
            base.Insert(index, ACRNumraAutomatike);
        }

        public int indeksiACRNumraAutomatike(clsACRNumraAutomatike ACRNumraAutomatike)
        {//metoda per te marre indeksin e nje ACRNumraAutomatike
            return base.IndexOf(ACRNumraAutomatike);
        }

        public bool ekzistonACRNumraAutomatike(clsACRNumraAutomatike ACRNumraAutomatike)
        {// metoda per te pare nqs ACRNumraAutomatike ekziston ne nje arraylist
            if (base.Contains(ACRNumraAutomatike))
                return true;
            else return false;
        }

        public int numriACRNumraAutomatike()
        {// metoda per te marre nr e ACRNumraAutomatike ne arrayList
            return base.Count;
        }

        #endregion

        #region Metoda Private

        private bool mbushACRNumratAutomatike(DataTable dt)
        {
            try
            {
                foreach (DataRow rreshti in dt.Rows)
                {
                    clsACRNumraAutomatike ACRNumraAutomatike = new clsACRNumraAutomatike();
                    ACRNumraAutomatike.mbushACRNumraAutomatike(rreshti);
                    Add(ACRNumraAutomatike);
                }

            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        #endregion
        [Obsolete("Perdor: bool mbushACRNumratAutomatike(DataTable dt)", true)]
        public colACRNumratAutomatike mbushArrayListeACRNumraAutomatike(DataSet ds)
        {// metoda per te mbushur nje arraylist me ACRNumraAutomatike nga nje dataset
            colACRNumratAutomatike ACRNumratAutomatike = new colACRNumratAutomatike();
            foreach (DataRow rreshti in ds.Tables[0].Rows)
            {
                clsACRNumraAutomatike ACRNumraAutomatike = new clsACRNumraAutomatike();
                ACRNumraAutomatike.IdLidhjeNrAuto = int.Parse(rreshti[0].ToString());
                ACRNumraAutomatike.IdLidhjeCR = int.Parse(rreshti[1].ToString());
                ACRNumraAutomatike.IdLlojiLidhje = int.Parse(rreshti[2].ToString());
                ACRNumraAutomatike.IdNumraAutoLidhje = int.Parse(rreshti[3].ToString());
                ACRNumraAutomatike.VleraFunditLidhje = rreshti[4].ToString();
                ACRNumraAutomatike.IdPerdoruesi = int.Parse(rreshti[5].ToString());
                ACRNumraAutomatike.IdNdermarje = int.Parse(rreshti[6].ToString());
                ACRNumratAutomatike.Add(ACRNumraAutomatike);
            }
            return ACRNumratAutomatike;
        }
    }
}

