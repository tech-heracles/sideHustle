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
    ///  Kjo eshte klasa qe mban nje liste me objekte te tipit clsGrupiLlogaria
    ///  dhe lejon te manipulohet kjo liste nepermjet indekseve
    ///  (Te dhenat nuk merren nga ndonje tabele)
    /// </summary>
    public class colGrupetLlogaria : System.Collections.Generic.List<clsGrupiLlogaria>
    {
        #region Konstruktoret

        /// <summary>
        /// konstruktor pa parametra
        /// </summary>
        public colGrupetLlogaria()
        {
        }

        /// <summary>
        /// konstruktor me parametra
        /// </summary>
        /// <param name="idNdermarje">id e ndermarrjes</param>
        public colGrupetLlogaria(int idNdermarje, int idGjuha)
        {
            clsDatabaseKontabilitet dbGrupLlogari = new clsDatabaseKontabilitet();
            mbushGrupetLlogaria(dbGrupLlogari.ktheGjitheGrupetLlogariapozitive(idNdermarje, idGjuha));
            dbGrupLlogari.Dispose();
        }
        #endregion

        #region Metoda Publike

        /// <summary>
        /// kthen objektin <see cref="DbCore.DbKontabiliteti.clsGrupiLlogaria"/>  qe ndodhet ne nje index te caktuar te arraylist-es
        /// </summary>
        public new clsGrupiLlogaria this[int index]
        {
            get { return ((clsGrupiLlogaria)base[index]); }
        }

        /// <summary>
        /// metoda per shtimin e objekti clsGrupiLlogaria ne nje arraylist
        /// </summary>
        public bool shtoGrupLlogaria(clsGrupiLlogaria grupiLlogaria)
        {
            base.Add(grupiLlogaria);
            if (base.Contains(grupiLlogaria))
                return true;
            else return false;
        }

        /// <summary>
        /// metoda per fshirjen e objekti clsGrupiLlogaria ne nje arraylist
        /// </summary>
        public bool fshiGrupLlogaria(clsGrupiLlogaria grupLlogaria)
        {
            base.Remove(grupLlogaria);
            if (base.Contains(grupLlogaria))
                return false;
            else return true;
        }

        /// <summary>
        /// metoda per fshirjene gjithe objekteve clsGrupiLlogaria ne nje arraylist
        /// </summary>
        public bool fshiGjitheGrupetLlogaria()
        {
            base.Clear();
            if (base.Count == 0)
                return true;
            else
                return false;
        }

        /// <summary>
        /// metoda per fshirjen e objekti clsGrupiLlogaria ne nje indeks te caktuar  ne nje arraylist
        /// </summary>
        public void fshiKeteGrupLlogaria(int index)
        {
            base.RemoveAt(index);
        }

        /// <summary>
        /// metoda per shtimin e objekti clsGrupiLlogaria ne nje indeks te caktuar  ne nje arraylist
        /// </summary>
        public void shtoGrupLlogariaNeIndeksin(int index, clsGrupiLlogaria grupLlogaria)
        {
            base.Insert(index, grupLlogaria);
        }

        /// <summary>
        /// metoda kthen indeksin e nje objekti clsGrupiLlogaria ne nje arraylist
        /// </summary>
        public int indeksiGrupitLlogaria(clsGrupiLlogaria grupLlogaria)
        {
            return base.IndexOf(grupLlogaria);
        }

        /// <summary>
        /// metoda kontrollon nese objekti clsGrupiLlogaria ndodhet ne nje arraylist
        /// </summary>
        public bool ekzistonGrupLlogaria(clsGrupiLlogaria grupLlogaria)
        {
            if (base.Contains(grupLlogaria))
                return true;
            else return false;
        }

        /// <summary>
        /// metoda kthen numrin e objekteve clsGrupiLlogaria ne nje arraylist
        /// </summary>
        public int numriGrupeveLlogaria()
        {
            return base.Count;
        }

        /// <summary>
        /// mbush te gjitha grupet e llogarive
        /// </summary>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert kthen false</returns>
        public bool mbushGjitheGrupetLlogaria()
        {
            clsDatabaseKontabilitet dbGrupLlogari = new clsDatabaseKontabilitet();
            bool sukses = mbushGrupetLlogaria(dbGrupLlogari.ktheGjitheGrupetLlogaria());
            dbGrupLlogari.Dispose();
            return sukses;
        }

        #endregion

        #region Metoda Private
        
        /// <summary>
        /// metoda perdoret gjeresisht ne rastet kur marrim te dhenat e kthyera ne ndonje store procedure ne formatin e nje dataseti
        /// nepermjet kesaj metode informacioni kalohet nga dataset-i ne nje liste me objekte te tipit 
        ///  <see cref="DbCore.DbKontabiliteti.clsGrupiLlogaria"/> 
        /// </summary>
        private bool mbushGrupetLlogaria(DataTable dt)
        {
            //try
            //{

                foreach (DataRow rreshti in dt.Rows)
                {
                    //clsGrupiLlogaria grupLlogaria = new clsGrupiLlogaria();
                    //grupLlogaria.mbushGrupLlogaria(rreshti);
                    Add(new clsGrupiLlogaria(rreshti));
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
        [Obsolete("Perdor: bool mbushGrupetLlogaria(DataTable dt)", true)]
        public colGrupetLlogaria mbushArrayListGrupetLlogaria(DataSet ds)
        {
            colGrupetLlogaria grupetLlogaria = new colGrupetLlogaria();
            foreach (DataRow rreshti in ds.Tables[0].Rows)
            {
                clsGrupiLlogaria grupLlogaria = new clsGrupiLlogaria();

                grupLlogaria.IdGrupiLlogaria = int.Parse(rreshti[0].ToString());
                grupLlogaria.NrGrupiLlogaria = int.Parse(rreshti[1].ToString());
                grupLlogaria.PershkrimiGrupiLlogaria = rreshti[2].ToString();
                grupLlogaria.IdNdermarje = int.Parse(rreshti[3].ToString());

                grupetLlogaria.Add(grupLlogaria);
            }
            return grupetLlogaria;
        }
    }
}
