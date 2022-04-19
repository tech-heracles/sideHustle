using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
namespace DbCore.DbProdhimi
{
    /// <summary>
    ///  Kjo eshte klasa qe mban nje liste me objekte te tipit clsKokaSkedulimProdhimi
    ///  dhe lejon te manipulohet kjo liste nepermjet indekseve
    ///  (Te dhenat nuk merren nga ndonje tabele)
    /// </summary>
    public class colKokaSkedulimProdhimi : List<clsKokaSkedulimProdhimi>
    {

        #region Konstruktor

        /// <summary>
        /// konstruktori pa parametra
        /// </summary>
        public colKokaSkedulimProdhimi()
        {
        }

        /// <summary>
        /// konstruktor me 1 parameter
        /// </summary>
        /// <param name="idNdermVit">id qe lidh ndermarrjen me vitin</param>
        public colKokaSkedulimProdhimi(int idNdermVit)
        {
            clsDatabazeProdhimi db = new clsDatabazeProdhimi();
            mbushKokaSkedulim(db.ktheGjitheKokaSkedulimProdhimi(idNdermVit), db);
            db.Dispose();
        }

        /// <summary>
        /// konstruktori qe implementon klasen baze
        /// </summary>
        /// <param name="collection">koleksion me clsKokaSkedulimProdhimi</param>
        public colKokaSkedulimProdhimi(IEnumerable<clsKokaSkedulimProdhimi> collection)
            : base(collection)
        {

        }

        #endregion

        #region Metoda Publike
        /// <summary>
        /// merr gjithe skedulimet sipas inderviti dhe autorizimeve ne forme data table
        /// </summary>
        /// <param name="idndermvit">id e ndermarje vitit</param>
        /// <param name="idperdoruesi">id e perdoruesit</param>
        /// <returns>data table me keto te dhena</returns>
        public static DataTable merrKokaSkedulimProdhimiDT(int idndermvit, int idperdoruesi)
        {
            clsDatabazeProdhimi db = new clsDatabazeProdhimi();
            DataTable dt = db.merrKokaSkedulimProdhimiDT(idndermvit, idperdoruesi);
            db.Dispose();
            return dt;
        }

        /// <summary>
        /// kthen objektin <see cref="DbCore.DbProdhimi.clsKokaSkedulimProdhimi"/>  qe ndodhet ne nje index te caktuar te arraylist-es
        /// </summary> 
        public new clsKokaSkedulimProdhimi this[int index]
        {
            get { return ((clsKokaSkedulimProdhimi)base[index]); }
        }

        #endregion

        #region Metoda Private

        /// <summary>
        /// metoda perdoret gjeresisht ne rastet kur marrim te dhenat e kthyera ne ndonje store procedure ne formatin e nje datatable
        /// </summary>
        /// <param name="db">clsDatabazeProdhimi ne rast transaksioni</param>
        /// <param name="dt">data table me te dhenat e tipit koka planifikim</param>
        /// <returns>true ose false ne se coleksioni u mbush ne rregull</returns>
        private bool mbushKokaSkedulim(DataTable dt, clsDatabazeProdhimi db)
        {
            //try
            //{

                foreach (DataRow rreshti in dt.Rows)
                {
                    //clsKokaSkedulimProdhimi koka = new clsKokaSkedulimProdhimi();
                    //koka.mbushKokaSkedulimProdhimi(rreshti, db);
                    Add(new clsKokaSkedulimProdhimi(rreshti, db));
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
