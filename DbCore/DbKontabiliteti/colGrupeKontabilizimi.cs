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
    ///  Kjo eshte klasa qe mban nje liste me objekte te tipit clsGrupKontabilizimi
    ///  dhe lejon te manipulohet kjo liste nepermjet indekseve
    ///  (Te dhenat nuk merren nga ndonje tabele)
    /// </summary>
    public class colGrupeKontabilizimi : System.Collections.Generic.List<clsGrupKontabilizimi>
    {
        #region Konstruktoret

        /// <summary>
        /// konstruktor pa parametra
        /// </summary>
        public colGrupeKontabilizimi()
        {
        }

        /// <summary>
        /// konstruktor me 1 parameter
        /// </summary>
        /// <param name="idndermarje">id e ndermarrjes</param>
        public colGrupeKontabilizimi(int idndermarje)
        {
            clsDatabaseKontabilitet dbGrupKontabilizimi = new clsDatabaseKontabilitet();
            mbushGrupeKontabilizime(dbGrupKontabilizimi.ktheGjitheGrupetKontabilizimi(idndermarje));
            dbGrupKontabilizimi.Dispose();
        }

        #endregion

        #region Metoda Publike

        /// <summary>
        /// kthen objektin <see cref="DbCore.DbKontabiliteti.clsGrupKontabilizimi"/>  qe ndodhet ne nje index te caktuar te arraylist-es
        /// </summary>
        public new clsGrupKontabilizimi this[int index]
        {
            get { return ((clsGrupKontabilizimi)base[index]); }
        }

        /// <summary>
        /// metoda per shtimin e nje objekti clsGrupKontabilizimi ne nje arraylist
        /// </summary>
        public bool shtoGrupKontabilizimi(clsGrupKontabilizimi grupKontabilizimi)
        {
            base.Add(grupKontabilizimi);
            if (base.Contains(grupKontabilizimi))
                return true;
            else return false;
        }

        /// <summary>
        /// metoda per fshirjen e nje objekti clsGrupKontabilizimi nga nje arraylist
        /// </summary>
        public bool fshiGrupKontabilizimi(clsGrupKontabilizimi grupKontabilizimi)
        {
            base.Remove(grupKontabilizimi);
            if (base.Contains(grupKontabilizimi))
                return false;
            else return true;
        }

        /// <summary>
        /// metoda per fshirjen e objekti clsGrupKontabilizimi qe ndodhet ne nje indeks te caktuar  ne nje arraylist
        /// </summary>
        public void fshiKeteGrupKontabilizimi(int index)
        {
            base.RemoveAt(index);
        }

        /// <summary>
        /// metoda per shtimin e objekti clsGrupKontabilizimi ne nje indeks te caktuar  ne nje arraylist
        /// </summary>
        public void shtoGrupKontabilizimiNeIndeksin(int index, clsGrupKontabilizimi grupKontabilizimi)
        {
            base.Insert(index, grupKontabilizimi);
        }

        /// <summary>
        /// metoda qe kthen indeksin e objektit clsGrupKontabilizimi ne nje arraylist
        /// </summary>
        public int indeksiGrupKontabilizimit(clsGrupKontabilizimi grupKontabilizimi)
        {
            return base.IndexOf(grupKontabilizimi);
        }

        /// <summary>
        /// metoda qe kontrollon nese objekti clsGrupKontabilizimi ndodhet ne arraylist
        /// </summary>
        public bool ekzistonGrupKontabilizimi(clsGrupKontabilizimi grupKontabilizimi)
        {
            if (base.Contains(grupKontabilizimi))
                return true;
            else return false;
        }

        /// <summary>
        /// metoda qe kthen numrin e objekteve clsGrupKontabilizimi ne nje arraylist
        /// </summary>
        public int numriGrupeveKontabilizimi()
        {
            return base.Count;
        }

        #endregion

        #region Metoda Private

        /// <summary>
        /// metoda perdoret gjeresisht ne rastet kur marrim te dhenat e kthyera ne ndonje store procedure ne formatin e nje datatable
        ///  <see cref="DbCore.DbKontabiliteti.clsGrupKontabilizimi"/> 
        /// </summary>
        private bool mbushGrupeKontabilizime(DataTable dt)
        {
            //try
            //{

                foreach (DataRow rreshti in dt.Rows)
                {
                    //clsGrupKontabilizimi grupKontabilizimi = new clsGrupKontabilizimi();
                    //grupKontabilizimi.mbushGrupKontabilizime(rreshti);
                    Add(new clsGrupKontabilizimi(rreshti));
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
        [Obsolete("Perdor: bool mbushGrupeKontabilizime(DataTable dt)", true)]
        public colGrupeKontabilizimi mbushArrayListGrupeKontabilizimi(DataSet ds)
        {
            colGrupeKontabilizimi grupeKontabilizimi = new colGrupeKontabilizimi();
            foreach (DataRow rreshti in ds.Tables[0].Rows)
            {
                clsGrupKontabilizimi grupKontabilizimi = new clsGrupKontabilizimi();

                grupKontabilizimi.IdGrupKontabilizimi = int.Parse(rreshti[0].ToString());
                grupKontabilizimi.NrGrupKontabilizimi = rreshti[1].ToString();
                grupKontabilizimi.PershkrimGrupKontabilizimi = rreshti[2].ToString();
                grupKontabilizimi.IdPerdoruesi = int.Parse(rreshti[3].ToString());
                grupKontabilizimi.IdNdermarje = int.Parse(rreshti[4].ToString());

                grupeKontabilizimi.Add(grupKontabilizimi);
            }
            return grupeKontabilizimi;
        }

    }
}