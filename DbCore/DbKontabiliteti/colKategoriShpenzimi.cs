using System.Collections.Generic;
using System.Linq;
using System.Data;

namespace DbCore.DbKontabiliteti
{
    /// <summary>
    ///  Kjo eshte klasa qe mban nje liste me objekte te tipit clsKategoriShpenzimi
    ///  dhe lejon te manipulohet kjo liste nepermjet indekseve
    ///  (Te dhenat nuk merren nga ndonje tabele)
    /// </summary>
    /// <seealso cref="clsKategoriShpenzimi"/>
    public class colKategoriShpenzimi : List<clsKategoriShpenzimi>
    {
        #region Konstruktori

        /// <summary>
        /// konstruktori pa parametra
        /// </summary>
        public colKategoriShpenzimi()
        {
        }

        public colKategoriShpenzimi(int idKokaShitje)
        {
            using (var db = new clsDatabaseKontabilitet())
                MbushKategorite(db.ktheGjitheKategoriShpenzimiSipasKokaShitje(idKokaShitje));
        }

        #endregion

        #region Metoda Publike

        /// <summary>
        /// kthen objektin <see cref="clsKategoriShpenzimi"/>  qe ndodhet ne nje index te caktuar te arraylist-es
        /// </summary>
        public new clsKategoriShpenzimi this[int index] => base[index];

        /// <summary>
        /// merr kategorine sipas id ne formen e data row
        /// </summary>
        /// <param name="idkoka"> id koka</param>
        /// <returns> kthen data row me kete koka</returns>
        public static DataRow MerrKategoriSipasIdDr(int idkoka)
        {
            using (var db = new clsDatabaseKontabilitet())
                return db.merrKategoriShpenzimiSipasIdDR(idkoka);
        }

        /// <summary>
        /// merr kategorine sipas ndermarje  ne forme data table
        /// </summary>
        /// <param name="idnderm"> idndermarje</param>
        /// <returns> kthen data table me keto skema</returns>
        public static DataTable MerrKategorineNdermarjeDt(int idnderm, bool merrVetemJoPrind = false)
        {
            using (var db = new clsDatabaseKontabilitet())
                return db.merrKategoriShpenzimiDT(idnderm, merrVetemJoPrind);
        }

        public static DataTable merrBijatKategoriShpenzimiSipasPrindit(int idnderm, string prindi)
        {
            using (clsDatabaseKontabilitet db = new clsDatabaseKontabilitet())
            {
                return db.merrBijatKategoriShpenzimiSipasPrindit(idnderm, prindi);
            }
        }

        /// <summary>
        /// mbush te gjitha kategorite sipas ndermarrjes 
        /// </summary>
        /// <param name="idnder">id e ndermarrjes</param>
        /// <returns>kthen true kur mbushja kryhet me sukses, ne te kundert false</returns>
        public void MbushGjitheKategoriSipasNdermarjes(int idnder)
        {
            using (var db = new clsDatabaseKontabilitet())
                MbushKategorite(db.ktheGjitheKategoriShpenzimiSipasNdermarjes(idnder));
        }

        public static IEnumerable<AutoCompleteItem> MerrKategoriShpenzimiPerAutoComplete(string infix, int idNdermarrje, int idPerdoruesi)
        {
            using (var kont = new clsDatabaseKontabilitet())
                return kont.MerrKategoriShpenzimiPerAutoComplete(infix, idNdermarrje, idPerdoruesi).ToList();
        }

        public static DataTable MerrKategoriShpenzimiExport(int idNdermarrja)
        {
            using (var kont = new clsDatabaseKontabilitet())
                return kont.MerrKategoriShpenzimiExport(idNdermarrja);
        }

        #endregion

        #region Metoda Private

        /// <summary>
        /// metoda perdoret gjeresisht ne rastet kur marrim te dhenat e kthyera ne ndonje store procedure ne formatin e nje datatable
        /// </summary>
        /// <param name="dt">data table me te dhena te tipit kategorite koka</param>
        /// <returns>true ose false nqs objekti u mbush ne rregull me te dhena</returns>
        private void MbushKategorite(DataTable dt)
        {
            foreach (DataRow rreshti in dt.Rows)
                Add(new clsKategoriShpenzimi(rreshti));
        }

        #endregion
    }
}

