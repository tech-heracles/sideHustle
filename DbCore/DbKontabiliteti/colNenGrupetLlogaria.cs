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
    ///  Kjo eshte klasa qe mban nje liste me objekte te tipit clsNenGrupiLlogaria
    ///  dhe lejon te manipulohet kjo liste nepermjet indekseve
    ///  (Te dhenat nuk merren nga ndonje tabele)
    /// </summary>
    public class colNenGrupetLlogaria : System.Collections.Generic.List<clsNenGrupiLlogaria>
    {
        #region Metoda Publike

        /// <summary>
        /// kthen objektin <see cref="DbCore.DbKontabiliteti.clsNenGrupiLlogaria"/>  qe ndodhet ne nje index te caktuar te arraylist-es
        /// </summary>
        public new clsNenGrupiLlogaria this[int index]
        {
            get { return ((clsNenGrupiLlogaria)base[index]); }
        }

        /// <summary>
        /// mbush gjithe nen grupet e llogarise sipas ndermarrjes
        /// </summary>
        /// <param name="idnderm">id e ndermarrjes</param>
        /// <returns>kthe true nese mbushja kryhet me sukses, ne te kundert kthe false</returns>
        public bool mbushGjitheNenGrupetLlogaria(int idnderm, int idGjuha)
        {
            clsDatabaseKontabilitet dbNenGrupLlogari = new clsDatabaseKontabilitet();
            bool sukses = mbushNenGrupetLlogaria(dbNenGrupLlogari.ktheGjitheNenGrupetLlogaria(idnderm, idGjuha));
            dbNenGrupLlogari.Dispose();
            return sukses;
        }

        /// <summary>
        /// mbush gjithe nengrupet e llogarise sipas grupit
        /// </summary>
        /// <param name="idGrupiLlogaria">id e grupit te llogarise</param>
        /// <returns>kthe true nese mbushja kryhet me sukses, ne te kundert kthe false</returns>
        public bool mbushNenGrupetLlogariaSipasGrupit(int idGrupiLlogaria, int idGjuha)
        {
            clsDatabaseKontabilitet dbNenGrupLlogari = new clsDatabaseKontabilitet();
            bool sukses = mbushNenGrupetLlogaria(dbNenGrupLlogari.ktheNenGrupetLlogariaSipasGrupit(idGrupiLlogaria, idGjuha));
            dbNenGrupLlogari.Dispose();
            return sukses;
        }

        /// <summary>
        /// mbush gjithe nen grupet me llogari pozitive sipas grupit
        /// </summary>
        /// <param name="idGrupiLlogaria">id e grupit te llogarise</param>
        /// <returns>kthe true nese mbushja kryhet me sukses, ne te kundert kthe false</returns>
        public bool mbushNenGrupetLlogariaSipasGrupitPozitive(int idGrupiLlogaria, int idGjuha)
        {
            clsDatabaseKontabilitet dbNenGrupLlogari = new clsDatabaseKontabilitet();
            bool sukses = mbushNenGrupetLlogaria(dbNenGrupLlogari.ktheNenGrupetLlogariaSipasGrupitPozitive(idGrupiLlogaria, idGjuha));
            dbNenGrupLlogari.Dispose();
            return sukses;
        }

        /// <summary>
        /// mbush gjithe nengrupet me llogari pozitive sipas ndermarrjes
        /// </summary>
        /// <param name="idnder">id e ndermarrjes</param>
        /// <returns>kthe true nese mbushja kryhet me sukses, ne te kundert kthe false</returns>
        public bool mbushGjitheNenGrupetLlogariaPozitive(int idnder)
        {
            clsDatabaseKontabilitet dbNenGrupLlogari = new clsDatabaseKontabilitet();
            bool sukses = mbushNenGrupetLlogaria(dbNenGrupLlogari.ktheGjitheNenGrupetLlogariaPozitive(idnder));
            dbNenGrupLlogari.Dispose();
            return sukses;
        }

        #endregion

        #region Metoda Private
        
        /// <summary>
        /// metoda perdoret gjeresisht ne rastet kur marrim te dhenat e kthyera ne ndonje store procedure ne formatin e nje datatable
        ///  <see cref="DbCore.DbKontabiliteti.clsNenGrupiLlogaria"/> 
        /// </summary>
        private bool mbushNenGrupetLlogaria(DataTable dt)
        {
            //try
            //{

                foreach (DataRow rreshti in dt.Rows)
                {
                    //clsNenGrupiLlogaria nengrupLlogaria = new clsNenGrupiLlogaria();
                    //nengrupLlogaria.mbushNenGrupLlogaria(rreshti);
                    Add(new clsNenGrupiLlogaria(rreshti));
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
        [Obsolete("Perdor: bool mbushNenGrupetLlogaria(DataTable dt)", true)]       
        public colNenGrupetLlogaria mbushArrayListNenGrupetLlogaria(DataSet ds)
        {
            colNenGrupetLlogaria nengrupetLlogaria = new colNenGrupetLlogaria();
            foreach (DataRow rreshti in ds.Tables[0].Rows)
            {
                clsNenGrupiLlogaria nengrupLlogaria = new clsNenGrupiLlogaria();

                nengrupLlogaria.IdNenGrupiLlogaria = int.Parse(rreshti[0].ToString());
                nengrupLlogaria.NrNenGrupiLlogaria = int.Parse(rreshti[1].ToString());
                nengrupLlogaria.PershkrimiNenGrupiLlogaria = rreshti[2].ToString();
                nengrupLlogaria.IdGrupiLlogaria = int.Parse(rreshti[3].ToString());
                nengrupLlogaria.IdNdermarje = int.Parse(rreshti[4].ToString());

                nengrupetLlogaria.Add(nengrupLlogaria);
            }
            return nengrupetLlogaria;
        }
    }
}

