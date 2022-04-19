using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;

namespace DbCore.DbProdhimi
{
    /// <summary>
    ///  Kjo eshte klasa qe mban nje liste me objekte te tipit clsKokaEkzekutim
    ///  dhe lejon te manipulohet kjo liste nepermjet indekseve
    ///  (Te dhenat nuk merren nga ndonje tabele)
    /// </summary>
    public class colKokaEkzekutim : List<clsKokaEkzekutim>
    {

        #region Konstruktor

        /// <summary>
        /// konstruktori pa parametra
        /// </summary>
        public colKokaEkzekutim()
        {
        }

        /// <summary>
        /// konstruktor me 1 parameter
        /// </summary>
        /// <param name="idNdermVit">id qe lidh ndermarrjen me vitin</param>
        public colKokaEkzekutim(int idNdermVit)
        {
            clsDatabazeProdhimi db = new clsDatabazeProdhimi();
            mbushKokaEkzekutim(db.ktheGjitheKokaEkzekutimi(idNdermVit), db);
            db.Dispose();
        }

        /// <summary>
        /// konstruktori qe implementon klasen baze
        /// </summary>
        /// <param name="collection">koleksion me clsKokaEkzekutim</param>
        public colKokaEkzekutim(IEnumerable<clsKokaEkzekutim> collection)
            : base(collection)
        {

        }

        #endregion

        #region Metoda Publike
        
        /// <summary>
        /// merr gjithe ekzekutimet sipas inderviti dhe autorizimeve ne forme data table
        /// </summary>
        /// <param name="idndermvit">id e ndermarje vitit</param>
        /// <param name="idperdoruesi">id e perdoruesit</param>
        /// <returns>data table me keto te dhena</returns>
        public static DataTable merrKokaEkzekutimDT(int idndermvit, int idperdoruesi)
        {
            clsDatabazeProdhimi db = new clsDatabazeProdhimi();
            DataTable dt = db.merrKokaEkzekutimDT(idndermvit, idperdoruesi);
            db.Dispose();
            return dt;           
        }

        public static DataTable merrEkzekutimePerEksport(int idnderm, int idperdorues, int idNdermViti, int lloji, string emerTabKoka, string emerFusheId, string idPerEksport)
        {
            clsDatabazeProdhimi db = new clsDatabazeProdhimi();
            DataTable dt = db.merrEkzekutimePerEksport(idnderm, idperdorues, idNdermViti, lloji, emerTabKoka, emerFusheId, idPerEksport);
            db.Dispose();
            return dt;
        }

        public static DataTable merrEkzekutimePerImport(string emerTabEkz, string emerTabProd, string emerTabRec, string ndermarrjeKey, string ndermarjeKodi, string primaryKeyEkzekutimi, string primaryKeyProdukte, bool merrTePaImportuara, bool rimerrTeImportuara, int? nrDokumentash)
        {
            clsDatabazeProdhimi db = new clsDatabazeProdhimi();
            DataTable dt = db.merrDokEkzekutimProdhimiPerImport(emerTabEkz, emerTabProd, emerTabRec, ndermarrjeKey, ndermarjeKodi, primaryKeyEkzekutimi, primaryKeyProdukte, merrTePaImportuara, rimerrTeImportuara, nrDokumentash);
            db.Dispose();
            return dt;
        }

        public static DataTable merrEkzekutimePerImport(string emerTabEkz, string emerTabProd, string ndermarrjeKey, string ndermarjeKodi, string primaryKeyEkzekutimi, bool merrTePaImportuara, bool riMerrTeImportuara, int? nrDokumentash)
        {
            clsDatabazeProdhimi db = new clsDatabazeProdhimi();
            DataTable dt = db.merrDokEkzekutimProdhimiPerImport(emerTabEkz, emerTabProd, ndermarrjeKey, ndermarjeKodi, primaryKeyEkzekutimi, merrTePaImportuara, riMerrTeImportuara, nrDokumentash);
            db.Dispose();
            return dt;
        }

        public static DataTable merrEkzekutimePerImport(string emerTabEkz, string ndermarrjeKey, string ndermarjeKodi)
        {
            clsDatabazeProdhimi db = new clsDatabazeProdhimi();
            DataTable dt = db.merrDokEkzekutimKokaPerImport(emerTabEkz, ndermarrjeKey, ndermarjeKodi);
            db.Dispose();
            return dt;
        }

        /// <summary>
        /// kthen objektin <see cref="DbCore.DbProdhimi.clsKokaEkzekutim"/>  qe ndodhet ne nje index te caktuar te arraylist-es
        /// </summary> 
        public new clsKokaEkzekutim this[int index]
        {
            get { return ((clsKokaEkzekutim)base[index]); }
        }



        #endregion

        #region Metoda Private

        /// <summary>
        /// metoda perdoret gjeresisht ne rastet kur marrim te dhenat e kthyera ne ndonje store procedure ne formatin e nje datatable
        /// </summary>
        /// <param name="db">clsDatabazeProdhimi ne rast transaksioni</param>
        /// <param name="dt">data table me te dhenat e tipit koka ekzekutim</param>
        /// <returns>true ose false ne se coleksioni u mbush ne rregull</returns>
        private bool mbushKokaEkzekutim(DataTable dt, clsDatabazeProdhimi db)
        {
            //try
            //{

                foreach (DataRow rreshti in dt.Rows)
                {
                    //clsKokaEkzekutim koka = new clsKokaEkzekutim();
                    //koka.mbushKokaEkzekutim(rreshti, db);
                    Add(new clsKokaEkzekutim(rreshti, db));
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

    }
}
