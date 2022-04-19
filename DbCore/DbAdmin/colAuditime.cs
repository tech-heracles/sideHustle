using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DbCore.DbAdmin
{
    /// <summary>
    ///  Kjo eshte klasa qe mban nje liste me objekte te tipit clsAuditim
    ///  dhe lejon te manipulohet kjo liste nepermjet indekseve
    ///  (Te dhenat nuk merren nga ndonje tabele)
    /// </summary>
    public class colAuditime : System.Collections.Generic.List<clsAuditim>
    {
        #region Metoda Publike

        /// <summary>
        /// kthen obj nga index-i i dhene i arraylist-es
        /// </summary>
        public new clsAuditim this[int index]
        {
            get { return ((clsAuditim)base[index]); }
        }

        ///// <summary>
        ///// metoda kthen obj e auditimit qe i parkasin veprimeve me nej tabele te caktuar
        /////  <see cref="DbCore.DbAdmin.clsDatabaseAdmin.merrKolonatPerAuditim"/> 
        ///// </summary>
        //public colAuditime gjejAuditimePerTabelen(int idtabele, int idndermarrjeviti)
        //{
        //    clsDatabaseAdmin data = new clsDatabaseAdmin();
        //    return mbushAuditimet(data.merrKolonatPerAuditim("1", 1)) ? this : null;
        //}

        #endregion 

        #region Metoda Private

        /// <summary>
        /// Mbush koleksionin e Auditimeve me te dhenat e Datatable-it dt
        /// </summary>
        /// <param name="dt">datatable ne hyrje</param>
        /// <returns>True nese koleksioni kryhet me sukses, false perndryshe</returns>
        private bool mbushAuditimet(DataTable dt)
        {
            //try
            //{

                foreach (DataRow dr in dt.Rows)
                {
                    //clsAuditim audit = new clsAuditim();
                    //audit.mbushAuditim(dr);
                    this.Add(new clsAuditim(dr));
                }
                return true;

            //}
            //catch (Exception)
            //{
            //    return false;
            //}
        }

        #endregion
        /// <summary>
        /// metoda perdoret gjeresisht ne rastet kur marrim te dhenat e kthyera ne ndonje store procedure ne formatin e nje dataseti
        /// nepermjet kesaj metode informacioni kalohet nga dataset-i ne nje liste me objekte te tipit 
        ///  <see cref="DbCore.DbAdmin.clsAuditim"/> 
        /// </summary>
        [Obsolete("Perdor: bool mbushAuditimet(DataTable dt)", true)]
        public colAuditime mbushArrayListAuditime(DataSet ds)
        {
            colAuditime col = new colAuditime();
            foreach (DataRow rreshti in ds.Tables[0].Rows)
            {
                clsAuditim audit = new clsAuditim();

                audit.IdAuditim = int.Parse(rreshti[0].ToString());
                audit.IdTabele = int.Parse(rreshti[1].ToString());
                audit.IdKolone = int.Parse(rreshti[2].ToString());
                audit.IdNderViti = int.Parse(rreshti[3].ToString());
                audit.IdPerdoruesi = int.Parse(rreshti[4].ToString());
                col.Add(audit);
            }
            return col;
        }

    }
}
