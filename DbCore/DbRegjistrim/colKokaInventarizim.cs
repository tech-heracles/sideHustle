using DbCore.DbRegjistrim;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbCore.DbRegjistrim
{   
    /// <summary>
    ///  Kjo eshte klasa qe mban nje liste me objekte te tipit clsKokaInventarizim
    ///  dhe lejon te manipulohet kjo liste nepermjet indekseve
    ///  (Te dhenat nuk merren nga ndonje tabele)
    /// </summary>
  public  class colKokaInventarizim: System.Collections.Generic.List<clsKokaInventarizim>
    {

        #region Konstruktor

        /// <summary>
        /// 
        /// </summary>
        public colKokaInventarizim()
        {
        }


        public static DataTable merrKokaInventarizimDT(int idndermvit, int idperdoruesi, string datanga, string dataderi, bool gjithedokagj, bool gjithedokash, bool lloj)
        {
            using (clsDatabaseRegjistrim dbartikuj = new clsDatabaseRegjistrim())
            {
                return dbartikuj.merrKokaInventarizimDT(idndermvit, idperdoruesi, datanga, dataderi, gjithedokagj, gjithedokash, lloj);
            }
        }

      

       
        #endregion

        #region Metoda Publike

        /// <summary>
        /// kthen objektin <see cref="DbCore.DbRegjistrim.clsKokaInventarizim"/>  qe ndodhet ne nje index te caktuar te arraylist-es
        /// </summary> 
        public new clsKokaInventarizim this[int index]
        {
            get { return ((clsKokaInventarizim)base[index]); }
        }

        public static DataTable merrDokInventarizimiPerEksport(int idnderm, int idperdorues, int idNdermViti, int lloji, string emerTabKoka, string emerFusheId, bool afatgjate, string idPerEksport)
        {
            clsDatabaseRegjistrim dbRegj = new clsDatabaseRegjistrim();
            DataTable tabela = dbRegj.ktheDokInventarizimiPerExport(idnderm, idperdorues, idNdermViti, lloji, emerTabKoka, emerFusheId,afatgjate, idPerEksport);
            dbRegj.Dispose();
            return tabela;
        }


        //public static DataTable merrDokMagazinePerImport(string emerTabKoka, string emerTabTrupi, string ndermarrjeKod, string ndermarjeKodi, string nenkategoria,int lloji, string primaryKey)
        //{
        //    clsDatabaseRegjistrim dbImportMagazina = new clsDatabaseRegjistrim();
        //    DataTable dt = dbImportMagazina.merrDokMagazinePerImport(emerTabKoka, emerTabTrupi, ndermarrjeKod, ndermarjeKodi, nenkategoria, lloji, primaryKey);
        //    dbImportMagazina.Dispose();
        //    return dt;
        //}

        #endregion

        #region Metoda Private

        /// <summary>
        /// metoda perdoret gjeresisht ne rastet kur marrim te dhenat e kthyera ne ndonje store procedure ne formatin e nje datatable
        ///  <see cref="DbCore.DbRegjistrim.clsKokaInventarizim"/> 
        /// </summary>
        private bool mbushKokaInventarizm(DataTable dt)
        {
            //try
            //{

                foreach (DataRow rreshti in dt.Rows)
                {
                    //clsKokaInventarizim koka = new clsKokaInventarizim();
                    //koka.mbushKokaMagazina(rreshti, db);
                    Add(new clsKokaInventarizim(rreshti));
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
