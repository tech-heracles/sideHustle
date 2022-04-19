using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data.SqlClient;
using System.Data;
namespace DbCore.DbRegjistrim
{
    /// <summary>
    ///  Kjo eshte klasa qe mban nje liste me objekte te tipit clsKokaNdryshimCmimSasi
    ///  dhe lejon te manipulohet kjo liste nepermjet indekseve
    ///  (Te dhenat nuk merren nga ndonje tabele)
    /// </summary>
    public class colKokaNdryshimCmimSasi : System.Collections.Generic.List<clsKokaNdryshimCmimSasi>
    {

        #region Konstruktor

        /// <summary>
        /// 
        /// </summary>
        public colKokaNdryshimCmimSasi()
        {
        }

        /// <summary>
        /// konstruktor me 1 parameter
        /// </summary>
        /// <param name="idNdermVit">id qe lidh ndermarrjen me vitin</param>
        public colKokaNdryshimCmimSasi(int idNdermVit)
        {
            clsDatabaseRegjistrim dbKokaMagazina = new clsDatabaseRegjistrim();
            mbushKokatNdryshimCmimSasi(dbKokaMagazina.ktheGjitheKokaNdryshimCmimSasi(idNdermVit));
            dbKokaMagazina.Dispose();
        }
        public static DataTable merrKokaNdryshimCmimSasiDT(int idndermvit, int idperdoruesi)
        {
            clsDatabaseRegjistrim dbartikuj = new clsDatabaseRegjistrim();
            DataTable dt = dbartikuj.merrKokaNdryshimCmimSasiDT(idndermvit, idperdoruesi);
            dbartikuj.Dispose();
            return dt;
        }

      
        #endregion

        #region Metoda Publike

        /// <summary>
        /// kthen objektin <see cref="DbCore.DbRegjistrim.clsKokaNdryshimCmimSasi"/>  qe ndodhet ne nje index te caktuar te arraylist-es
        /// </summary> 
        public new clsKokaNdryshimCmimSasi this[int index]
        {
            get { return ((clsKokaNdryshimCmimSasi)base[index]); }
        }

        #endregion

        #region Metoda Private

        /// <summary>
        /// metoda perdoret gjeresisht ne rastet kur marrim te dhenat e kthyera ne ndonje store procedure ne formatin e nje datatable
        ///  <see cref="DbCore.DbRegjistrim.clsKokaNdryshimCmimSasi"/> 
        /// </summary>
        private bool mbushKokatNdryshimCmimSasi(DataTable dt)
        {
            //try
            //{

                foreach (DataRow rreshti in dt.Rows)
                {
                    //clsKokaNdryshimCmimSasi koka = new clsKokaNdryshimCmimSasi();
                    //koka.mbushKokaNdryshimCmimSasi(rreshti);
                    Add(new clsKokaNdryshimCmimSasi(rreshti));
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
