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
    ///  Kjo eshte klasa qe mban nje liste me objekte te tipit clsFazaKontrate
    ///  dhe lejon te manipulohet kjo liste nepermjet indekseve
    ///  (Te dhenat nuk merren nga ndonje tabele)
    /// </summary>
    public class colFazaKontrate : System.Collections.Generic.List<clsFazaKontrate>
    {
        #region Metoda Publike

        public colFazaKontrate()
        {
        }


        public colFazaKontrate(int idndermarje, int idkontrate)
        {
            clsDatabaseRegjistrim data = new clsDatabaseRegjistrim();
            
            mbushFazat(data.merrFazeSipasKontrate(idndermarje,idkontrate));
            data.Dispose();
        }

        /// <summary>
        /// Merr fazat e mbetura te KNSH te pa perdorura ende ne konvertim
        /// </summary>
        /// <param name="ids"></param>
        public colFazaKontrate(string ids)
        {
            colFazaKontrate col = new colFazaKontrate();
            clsDatabaseRegjistrim data = new clsDatabaseRegjistrim();
            mbushFazat(data.MerrFazaTeMbeturaPerKonvertimPerDok(ids));
            data.Dispose();
        }

        /// <summary>
        /// kthen objektin <see cref="clsFazaKontrate"/>  qe ndodhet ne nje index te caktuar te arraylist-es
        /// </summary>
        public new clsFazaKontrate this[int index]
        {
            get { return ((clsFazaKontrate)base[index]); }
        }

        /// <summary>
        /// metoda per shtimin e nje obj clsFazaKontrate ne nje arraylist
        /// </summary>
        public bool shtoKarte(clsFazaKontrate karta)
        {
            Add(karta);
            if (base.Contains(karta))
                return true;
            else return false;
        }

        /// <summary>
        /// metoda per heqjen e nje obj clsFazaKontrate ne nje arraylist
        /// </summary>
        public bool fshiKarte(clsFazaKontrate karte)
        {
            base.Remove(karte);
            if (base.Contains(karte))
                return false;
            else return true;
        }

        /// <summary>
        /// metoda per heqjen e nje obj clsFazaKontrate ne nje arraylist
        /// </summary>
        public bool fshiGjitheArtikujt()
        {
            base.Clear();
            if (base.Count == 0)
                return true;
            else
                return false;
        }

        /// <summary>
        /// metoda per heqjen e nje obj clsFazaKontrate ne nje arraylist
        /// </summary>
        public void fshiKeteKarte(int index)
        {
            base.RemoveAt(index);
        }

        /// <summary>
        /// metoda shton nje obj te ri ne array list ne pozicionin e percaktuar nga index-i
        /// </summary>
        public void shtoKartenNeIndeksin(int index, clsFazaKontrate karte)
        {
            base.Insert(index, karte);
        }

        /// <summary>
        /// metoda gjen index-in ne liste ne te cilin ndodhet nje obj i caktuar
        /// </summary>
        public int indeksiKartes(clsFazaKontrate karte)
        {
            return base.IndexOf(karte);
        }

        /// <summary>
        /// metoda kontrollon nqs nje obj ndodhet ne arraylist
        /// </summary>
        public bool ekzistonKarte(clsFazaKontrate karte)
        {
            if (base.Contains(karte))
                return true;
            else return false;
        }

        /// <summary>
        /// metoda qe kthen nr e obj qe ka arraylist
        /// </summary>
        public int numriKartave()
        {
            return base.Count;
        }

       

        //public static DataTable merrKartatSipasKlientit(int idNdermarrje, int idKlient)
        //{
        //    clsDatabaseRegjistrim db = new clsDatabaseRegjistrim();
        //    DataTable table = db.merrKarteSipasKlientit(idNdermarrje, idKlient);
        //    db.Dispose();
        //    return table;
        //}

       



      

        public bool mbushFazatSipasKontrates(int idNdermarrje,int idKontrata)
        {
            clsDatabaseRegjistrim db = new clsDatabaseRegjistrim();
            bool sukses = mbushFazat(db.merrFazeSipasKontrate(idNdermarrje, idKontrata));
            db.Dispose();
            return sukses;

        }
        //public static DataTable ktheKarteSipasNdermMeFilter(string filter, long startIndex, long endIndex, int idnderm)
        //{
        //    clsDatabaseRegjistrim db = new clsDatabaseRegjistrim();
        //    DataTable tabela = db.merrKarteSipasNdermarrjesMeFilter(filter, startIndex, endIndex, idnderm);
        //    db.Dispose();
        //    return tabela;
        //}

 
        #endregion

        #region Metoda Private

        /// <summary>
        /// Metoda perdoret gjeresisht ne rastet kur marrim te dhenat e kthyera nga nje stored procedure.
        /// </summary>
        /// <param name="dt">Merr si parameter nje DataTable.</param>
        /// <returns>Kthen True nese kryhet me sukses, ne te kundert False.</returns>
        /// <param name="dbInventari"></param>
        private bool mbushFazat(DataTable dt)
        {
          
                foreach (DataRow rreshti in dt.Rows)
                {
       
                    this.Add(new clsFazaKontrate(rreshti));
                }
       
            return true;
        }

        #endregion

    }
}