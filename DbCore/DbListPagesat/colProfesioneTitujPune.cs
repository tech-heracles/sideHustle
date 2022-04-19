using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DbCore.DbListPagesat
{
   public class colProfesioneTitujPune: List<clsProfesioneTitujPune>
    {
        #region Konstruktori

        /// <summary>
        /// konstruktori pa parametra
        /// </summary>
        public colProfesioneTitujPune()
        {
        }

        /// <summary>
        /// konstruktori me 1 parametra
        /// merr burimet te ndermarjes 
        /// </summary>
        /// <param name="idndermarje">id e ndermarrjes</param>
        public colProfesioneTitujPune(int idndermarje, int lloji)
        {
            clsDatabazeListPagesa db = new clsDatabazeListPagesa();
            mbushProfesioneTitujPune(db.ktheGjitheProfesioneTitujPunetSipasNdermarjesDheLlojit(idndermarje,lloji));
            db.Dispose();
        }

        /// <summary>
        /// konstruktori qe implementon klasen baze
        /// </summary>
        /// <param name="collection">koleksion me clsProfesioneTitujPune</param>
        public colProfesioneTitujPune(IEnumerable<clsProfesioneTitujPune> collection)
            : base(collection)
        {

        }

        #endregion

        #region Metoda Publike

        /// <summary>
        /// kthen objektin <see cref="clsProfesioneTitujPune"/>  qe ndodhet ne nje index te caktuar te arraylist-es
        /// </summary>
        public new clsProfesioneTitujPune this[int index]
        {
            get
            {
                return ((clsProfesioneTitujPune)base[index]);
            }
        }

        /// <summary>
        /// merr burimet sipas id ne formen e data row
        /// </summary>
        /// <param name="id"> id burimit</param>
        /// <returns> kthen data row me kete burim</returns>
        public static DataRow merrProfesioneTitujPuneSipasIdDR(int id)
        {
            clsDatabazeListPagesa db = new clsDatabazeListPagesa();
            DataRow dr = db.merrProfesioneTitujPuneSipasIdDR(id);
            db.Dispose();
            return dr;
        }

        /// <summary>
        /// merr burim sipas ndermarje  ne forme data table
        /// </summary>
        /// <param name="idnderm"> idndermarje</param>
        /// <returns> kthen data table me keto burime</returns>
        public static DataTable merrProfesioneTitujPuneDT(int idnderm, int lloji)
        {
            clsDatabazeListPagesa db = new clsDatabazeListPagesa();
            DataTable dt = db.merrProfesioneTitujPuneDT(idnderm,lloji);
            db.Dispose();
            return dt;
        }

        /// <summary>
        /// merr burim sipas ndermarje aktive  ne forme data table
        /// </summary>
        /// <param name="idnderm"> idndermarje</param>
        /// <returns> kthen data table me keto burime</returns>
        public static DataTable merrProfesioneTitujPuneDTAktive(int idnderm, int lloji)
        {
            clsDatabazeListPagesa db = new clsDatabazeListPagesa();
            DataTable dt = db.merrProfesioneTitujPuneDTAktive(idnderm,lloji);
            db.Dispose();
            return dt;
        }


        /// <summary>
        /// mbush te gjitha burimet sipas ndermarrjes 
        /// </summary>
        /// <param name="idnder">id e ndermarrjes</param>
        /// <returns>kthen true kur mbushja kryhet me sukses, ne te kundert false</returns>
        public bool ktheGjitheProfesioneTitujPunetSipasNdermarjesDheLlojit(int idnder, int lloji)
        {
            clsDatabazeListPagesa db = new clsDatabazeListPagesa();
            bool mbush = mbushProfesioneTitujPune(db.ktheGjitheProfesioneTitujPunetSipasNdermarjesDheLlojit(idnder, lloji));
            db.Dispose();
            return mbush;            
        }

        /// <summary>
        /// mbush te gjitha burimet sipas ndermarrjes  aktive
        /// </summary>
        /// <param name="idnder">id e ndermarrjes</param>
        /// <returns>kthen true kur mbushja kryhet me sukses, ne te kundert false</returns>
        public bool ktheGjitheProfesioneTitujPunetSipasNdermarjesDheLlojitAktiv(int idnder, int lloji)
        {
            clsDatabazeListPagesa db = new clsDatabazeListPagesa();
            bool mbush = mbushProfesioneTitujPune(db.ktheGjitheProfesioneTitujPunetSipasNdermarjesDheLlojitAktiv(idnder, lloji));
            db.Dispose();
            return mbush;
        }

        #endregion

        #region Metoda Private

        /// <summary>
        /// metoda perdoret gjeresisht ne rastet kur marrim te dhenat e kthyera ne ndonje store procedure ne formatin e nje datatable
        /// </summary>
        /// <param name="dt">data table me te dhena te tipit burim</param>
        /// <returns>true ose false nqs objekti u mbush ne rregull me te dhena</returns>
        private bool mbushProfesioneTitujPune(DataTable dt)
        {
            //try
            //{
                foreach (DataRow rreshti in dt.Rows)
                {
                    //clsProfesioneTitujPune profesion = new clsProfesioneTitujPune();
                    //profesion.mbushProfesioneTitujPune(rreshti);
                    Add(new clsProfesioneTitujPune(rreshti));
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
