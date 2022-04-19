using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;

namespace DbCore.DbProdhimi
{
    /// <summary>
    ///  Kjo eshte klasa qe mban nje liste me objekte te tipit clsAktiviteteTrupi
    ///  dhe lejon te manipulohet kjo liste nepermjet indekseve
    ///  (Te dhenat nuk merren nga ndonje tabele)
    /// </summary>
    /// <seealso cref="clsAktiviteteTrupi"/>
    public class colAktiviteteTrupi : List<clsAktiviteteTrupi>
    {
        #region Konstruktori

        /// <summary>
        /// konstruktori pa parametra
        /// </summary>
        public colAktiviteteTrupi()
        {
        }

        /// <summary>
        /// konstruktori me 1 parametra
        /// merr aktivitete trupi sipas idkoka
        /// </summary>
        /// <param name="idkoka">id e kokes</param>
        public colAktiviteteTrupi(int idkoka)
        {
            clsDatabazeProdhimi db = new clsDatabazeProdhimi();
            mbushAktiviteteTrupi(db.ktheAktiviteteTrupiSipasIdKoka(idkoka));
            db.Dispose();
        } 
        public colAktiviteteTrupi(int idkoka,clsDatabazeProdhimi db)
        {
           
            mbushAktiviteteTrupi(db.ktheAktiviteteTrupiSipasIdKoka(idkoka));
           
        }

        /// <summary>
        /// konstruktori me 2 parametra
        /// merr aktivitete trupi sipas idkoka dhe dt se ndryshimit
        /// </summary>
        /// <param name="idkoka">id e kokes</param>
        /// <param name="dtndryshimi">data e ndryshimit</param>
        public colAktiviteteTrupi(int idkoka, DateTime dtndryshimi)
        {
            clsDatabazeProdhimi db = new clsDatabazeProdhimi();
            mbushAktiviteteTrupi(db.ktheAktiviteteTrupiSipasIdKokaDheDtNdryshimi(idkoka, dtndryshimi));
            db.Dispose();
        } 
        public colAktiviteteTrupi(int idkoka, DateTime dtndryshimi, clsDatabazeProdhimi db)
        {
         
            mbushAktiviteteTrupi(db.ktheAktiviteteTrupiSipasIdKokaDheDtNdryshimi(idkoka, dtndryshimi));

        }
        /// <summary>
        /// konstruktori qe implementon klasen baze
        /// </summary>
        /// <param name="collection">koleksion me clsAktiviteteTrupi</param>
        public colAktiviteteTrupi(IEnumerable<clsAktiviteteTrupi> collection)
            : base(collection)
        {

        }

        #endregion

        #region Metoda Publike

        /// <summary>
        /// kthen objektin <see cref="clsAktiviteteTrupi"/>  qe ndodhet ne nje index te caktuar te arraylist-es
        /// </summary>
        public new clsAktiviteteTrupi this[int index]
        {
            get
            {
                return ((clsAktiviteteTrupi)base[index]);
            }
        }


        /// <summary>
        /// merr datat ne te cilat kemi ndryshime ne aktivitet
        /// </summary>
        ///<param name="idkoka">id e kokes</param>
        /// <returns>liste me datat</returns>
        public static List<string> merrDataAktiviteti(int idkoka)
        {
            clsDatabazeProdhimi db = new clsDatabazeProdhimi();
            List<string> list = new List<string>();
            DataTable dt = db.merrDataNdryshimiAktiviteti(idkoka);
            foreach (DataRow rreshti in dt.Rows)
            {
                list.Add(DateTime.Parse(rreshti["DTNDRYSHIMI"].ToString()).ToShortDateString());
            }
            db.Dispose();
            return list;
        } 
        public static List<string> merrDataAktiviteti(int idkoka, clsDatabazeProdhimi db)
        {
           List<string> list = new List<string>();
            DataTable dt = db.merrDataNdryshimiAktiviteti(idkoka);
            foreach (DataRow rreshti in dt.Rows)
            {
                list.Add(DateTime.Parse(rreshti["DTNDRYSHIMI"].ToString()).ToShortDateString());
            }
          
            return list;
        }

        public bool mbushAktiviteteTrupiSipasIdKoka(int idkoka)
        {
            clsDatabazeProdhimi db = new clsDatabazeProdhimi();
            bool sukses = mbushAktiviteteTrupi(db.ktheAktiviteteTrupiSipasIdKoka(idkoka));
            db.Dispose();
            return sukses;
        }

        #endregion

        #region Metoda Private

        /// <summary>
        /// metoda perdoret gjeresisht ne rastet kur marrim te dhenat e kthyera ne ndonje store procedure ne formatin e nje datatable
        /// </summary>
        /// <param name="dt">data table me te dhena te tipit aktivitete trupi</param>
        /// <returns>true ose false nqs objekti u mbush ne rregull me te dhena</returns>
        private bool mbushAktiviteteTrupi(DataTable dt)
        {
            //try
            //{
                foreach (DataRow rreshti in dt.Rows)
                {
                    //clsAktiviteteTrupi aktivitet = new clsAktiviteteTrupi();
                    //aktivitet.mbushAktivitetTrupi(rreshti);
                    Add(new clsAktiviteteTrupi(rreshti));
                }
            //}
            //catch (Exception)
            //{
            //    return false;
            //}
            return true;
        }

        #endregion
    }
}
