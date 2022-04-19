using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DbCore.DbAdmin;
using DbCore.DbKontabiliteti;
using DbCore.IMBUtils.DataBase;

namespace DbCore.DbQendraKosto
{
    /// <summary>
    ///  Kjo eshte klasa qe mban nje liste me objekte te tipit clsTrupiQendraKosto
    ///  dhe lejon te manipulohet kjo liste nepermjet indekseve
    ///  (Te dhenat nuk merren nga ndonje tabele)
    /// </summary>
    public class colTrupiQendraKosto: List<clsTrupiQendraKosto>
    {        

        #region konstruktoret

        /// <summary>
        /// konstruktori pa parametra
        /// </summary>
        public colTrupiQendraKosto()
        {

        }

        /// <summary>
        /// konstruktori me nje parameter
        /// </summary>
        /// <param name="idkoka"></param>
        public colTrupiQendraKosto(int idkoka)
        {
            using (clsDatabaseQendraKosto db = new clsDatabaseQendraKosto())
            {
                mbushTrupatQendraKosto(db.ktheGjitheTrupiQendraKostoNgaKoka(idkoka));
            }
        }

        /// <summary>
        /// konstruktori me nje parameter
        /// </summary>
        /// <param name="idkoka"></param>
        public colTrupiQendraKosto(int idkoka, clsDatabaseQendraKosto db)
        {
            mbushTrupatQendraKosto(db.ktheGjitheTrupiQendraKostoNgaKoka(idkoka));
        }

        /// <summary>
        /// konstruktori qe implementon klasen baze
        /// </summary>
        /// <param name="collection">koleksion me clsTrupiQendraKosto</param>
        public colTrupiQendraKosto(IEnumerable<clsTrupiQendraKosto> collection)
            : base(collection)
        {

        }
        #endregion

        #region Metoda Publike

        public void VendosIdKokeNeTrup(int idKoka)
        {
            ForEach(x => x.IdKoka = idKoka);
        }

        /// <summary>
        /// kthen objektin <see cref="DbCore.DbProdhimi.clsTrupiQendraKosto"/>  qe ndodhet ne nje index te caktuar te arraylist-es
        /// </summary> 
        public new clsTrupiQendraKosto this[int index]
        {
            get { return ((clsTrupiQendraKosto)base[index]); }
        }     

        /// <summary>
        /// mbush trupin e qendres se kostos sipas id se kokes se qendres se kostos
        /// </summary>
        /// <param name="idkoka">id koka e qendres se kostos</param>
        /// <param name="db">clsDatabaseQendraKosto ne rast transaksioni</param>
        /// <returns>kthen true nese mbushja kruhet me sukses, ne te kundert false</returns>
        public bool mbushTrupiQendraKosto(int idkoka, clsDatabaseQendraKosto db)
        {            
            return mbushTrupatQendraKosto(db.ktheGjitheTrupiQendraKostoNgaKoka(idkoka));            
        }

        #endregion

        #region Metoda Private

        /// <summary>
        /// metoda perdoret gjeresisht ne rastet kur marrim te dhenat e kthyera ne ndonje store procedure ne formatin e nje datatable
        /// </summary>
        /// <param name="dt">data table me te dhenat e tipit trupi qendra kosto</param>
        /// <returns>true ose false ne se coleksioni u mbush ne rregull</returns>
        private bool mbushTrupatQendraKosto(DataTable dt)
        {
            foreach (DataRow rreshti in dt.Rows)
            {
                Add(new clsTrupiQendraKosto(rreshti));
            }

            return true;
        }

        #endregion

    }
}
