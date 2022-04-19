using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DbCore.DbAdmin
{
    public class clsKategoriNrAuto
    {
        #region Atribute
        private int id;
        private string pershkrimi;
        private DataRow rreshti;
        #endregion

        #region Konstruktoret
          /// <summary>
        /// Konstruktori i klases me parametra
        /// </summary>
        /// <param name="idKat"> id ritese e kategorise</param>
       /// <param name="pershk"> pershkrimi i kategorise</param>
        public clsKategoriNrAuto(int idKat, string pershk)
        {
            id = idKat;
            pershkrimi = pershk;
          
        }

        /// <summary>
        /// konstruktor me 1 parameter string
        /// </summary>
        /// <param name="pershk">pershkrimi i kategorise</param>
        public clsKategoriNrAuto(string pershk)
        {
            clsDatabaseAdmin dbKatNivelDok = new clsDatabaseAdmin();
            mbushKat(dbKatNivelDok.ktheKategoriSipasPershkrimit(pershk));
            dbKatNivelDok.Dispose();
        }

        /// <summary>
        /// Konstruktori i klases pa parametra
        /// </summary>
        public clsKategoriNrAuto()
        {
        }

        public clsKategoriNrAuto(DataRow rreshti)
        {
            
            mbushKat(rreshti);
        }

        #endregion

        #region Properties
        public int Id
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
            }
        }
        public string Pershkrimi
        {
            get
            {
                return pershkrimi;
            }
            set
            {
                pershkrimi = value;
            }
        }
        #endregion

        #region Metoda Publike


        /// <summary>
        /// Merr objektin e  kategorise se nivelit te dokumentit sipas pershkrimit nga tabelen perkatese ne databaze.Therret funksionin
        /// :  <see cref="DbCore.DbRegjistrim.clsDatabaseRegjistrim.ktheKategoriNivelDok"/> 
        /// </summary>
        /// <returns > nje objekt  clsKategoriNivelDok qe permban kategorine e nivelit te dokumentit te kerkuar</returns>
        public clsKategoriNrAuto merr(string pershkrimi)
        {
            clsKategoriNrAuto data = new clsKategoriNrAuto(pershkrimi);
            return data;
        }

        /// <summary>
        /// Merr objektin e  kategorise se nivelit te dokumentit sipas id nga tabelen perkatese ne databaze.Therret funksionin
        /// :  <see cref="DbCore.DbRegjistrim.clsDatabaseRegjistrim.ktheKategoriNivelDokSipasID"/> 
        /// </summary>
        /// <returns > nje objekt  clsKategoriNivelDok qe permban kategorine e nivelit te dokumentit te kerkuar</returns>
        public bool merrSipasId(int id)
        {
            using (clsDatabaseAdmin dbKatNivelDok = new clsDatabaseAdmin())
            {
                return mbushKat( dbKatNivelDok.mbushKategoriSipasId(id));
            }
          
        }

      

        /// <summary>
        /// Merr te gjitha objektet e kategorise se nivelit te dokumentit sipas ndermarjes.Therret funksionin
        /// :  <see cref="DbCore.DbRegjistrim.clsDatabaseRegjistrim.ktheGjitheKategoriNivelDok"/> 
        /// </summary>
        /// <param name="idNdermVit">id e ndermarje vitit </param>
        /// <returns > nje objekt  colKategoriNivelDok qe permban kategorite e nivelit te dokumentit  te nje ndermarje</returns>
        public colKategoriNrAuto merriTeGjithe()
        {
            colKategoriNrAuto data = new colKategoriNrAuto();
            data.mbushGjitheKategoriNrAuto();
            return data;
        }

        #endregion

        #region Metoda Internal

        /// <summary>
        /// mbush kategorite e nivelit te dokumentit nga databaza
        /// </summary>
        /// <param name="dbDataRowKatNivelDok">datarow qe duhet mbushur nga db</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert false</returns>
        internal bool mbushKat(DataRow dbDataRowKat)
        {
            if (dbDataRowKat != null)
            {
                try
                {
                    int.TryParse(dbDataRowKat["ID"].ToString(), out id);
                    pershkrimi = dbDataRowKat["PERSHKRIMI"].ToString();


                    return true;
                }
                catch (InvalidCastException)
                {
                    throw new Exception("ERROR: Gabim gjate marrjes se kategorive te nr automatik nga db-ja");
                }
            }
            else
                return false;
        }


        #endregion
    }
}
