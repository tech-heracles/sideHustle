using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data.SqlClient;
using System.Data;
using AlphaWeb.Infrastructure.Data.AdoNet;

namespace DbCore.DbAdmin
{
    public class colKomponentet : System.Collections.Generic.List<clsKomponente>
    {
        #region Konstruktoret

        public colKomponentet()
        {
        }

        public colKomponentet(int idModuli, int idllojlicence)
        {
            clsDatabaseAdmin data = new clsDatabaseAdmin();
            mbushKomponentet(data.ktheKomponentetAmbjentitSipasModulit(idModuli, idllojlicence));
            data.Dispose();
        }

        public colKomponentet(int idModuli, int idllojlicence, clsDatabaseAdmin data)
        {
            if (data == null)
                data = new clsDatabaseAdmin();
            mbushKomponentet(data.ktheKomponentetAmbjentitSipasModulit(idModuli, idllojlicence));
        }

        #endregion

        #region Metoda Publike

        public new clsKomponente this[int index]
        {
            get { return ((clsKomponente)base[index]); }
        }




        public static List<string> MerriTeGjitha()
        {
            using (clsDatabaseAdmin dbAdmin = new clsDatabaseAdmin(Constants.DefaultConnectionName))
            {
                return dbAdmin.MerrGjitheKomponentet();
            }
        }








        public bool mbushKomponentetAmbjentit(int idllojlicence)
        {
            clsDatabaseAdmin data = new clsDatabaseAdmin();
            bool sukses = mbushKomponentet(data.ktheKomponentetAmbjentit(idllojlicence));
            data.Dispose();
            return sukses;
        }

        /// <summary>
        /// Kthen listen e konfigurimeve per nje ambjent te caktuar per te cilat ka autorizime perdoruesi.
        /// </summary>
        /// <param name="idKomponente">id e komponentes</param>
        /// <param name="idPerdorues">id e perdoruesit</param>
        /// <param name="idNdermarrje">id e ndermarrjes</param>
        /// <param name="idKatDok">id e kategorise se nivelit te dokumentit</param>
        /// <returns>Kthen nje DataTable me listen e konfigurimeve qe ka autorizime perdoruesi</returns>
        public static string[] ktheKonfigurimetMeAutorizimPerdoruesiSipasKomponentes(int idKomponente, int idPerdorues, int idNdermarrje, int idKatDok)
        {
            clsDatabaseAdmin dbAdmin = new clsDatabaseAdmin();
            DataTable dt = dbAdmin.ktheKonfigurimetMeAutorizimPerdoruesiSipasKomponentes(idKomponente, idPerdorues, idNdermarrje, idKatDok);
            string[] array = dt
                 .AsEnumerable()
                 .Select(row => row.Field<string>("IDKONFIGAMBJENTE"))
                 .ToArray();
            dbAdmin.Dispose();
            return array;
        }

        #endregion

        #region Metoda Private

        //u be koment, u spostua si funksion tek klasa e komponenteve
        ////public colKomponentet merrKomponenteSipasKodit(clsKomponente oKomponente)
        ////{
        ////    clsDatabaseAdmin data = new clsDatabaseAdmin();
        ////    return data.merrKomponenteSipasKodit(oKomponente);
        ////}
        private bool mbushKomponentet(DataTable dt)
        {
            //try
            //{
            foreach (DataRow rreshti in dt.Rows)
            {
                clsKomponente komp = new clsKomponente();
                if (komp.mbushKomponente(rreshti))
                    Add(komp);
            }
            //}
            //catch (Exception)
            //{

            //    return false;
            //}
            return true;
        }

        #endregion
        [Obsolete("Perdor: bool mbushKomponentet(DataTable dt)", true)]
        public colKomponentet mbushArrayListKomponentet(DataSet ds)
        {
            colKomponentet komponentet = new colKomponentet();
            foreach (DataRow rreshti in ds.Tables[0].Rows)
            {
                clsKomponente komp = new clsKomponente();

                komp.IdKomponente = int.Parse(rreshti[0].ToString());
                komp.IdModuli = int.Parse(rreshti[1].ToString());
                komp.EmriKomponente = rreshti[2].ToString();
                komp.PershkrimiKomponente_sq = rreshti[3].ToString();
                komponentet.Add(komp);
            }
            return komponentet;
        }
    }
}
