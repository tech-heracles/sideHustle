using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Globalization;
using System.Resources;


namespace DbCore.DbRegjistrim
{
    /// <summary>
    ///  Kjo eshte klasa qe mban nje liste me objekte te tipit clsDokumentLidhesKoka
    ///  dhe lejon te manipulohet kjo liste nepermjet indekseve
    ///  (Te dhenat nuk merren nga ndonje tabele)
    /// </summary>
    public class colDokumentLidhesKoka : System.Collections.Generic.List<clsDokumentLidhesKoka>
    {
        #region Metoda Publike

        /// <summary>
        /// kthen objektin <see cref="clsDokumentLidhesKoka"/>  qe ndodhet ne nje index te caktuar te arraylist-es
        /// </summary>       
        public new clsDokumentLidhesKoka this[int index]
        {
            get { return ((clsDokumentLidhesKoka)base[index]); }
        }
        public static DataTable merrDokumentLidhesDT(int idndermvit, int idperdoruesi)
        {
            clsDatabaseRegjistrim dbartikuj = new clsDatabaseRegjistrim();
            DataTable dt = dbartikuj.merrDokumentLidhesDT(idndermvit, idperdoruesi);
            dbartikuj.Dispose();
            return dt;
        }
        
        /// <summary>
        /// konstruktor me 1 parameter
        /// </summary>
        /// <param name="idNdermVit">id qe lidh ndermarrjen me vitin</param>
        public colDokumentLidhesKoka(int idNdermVit)
        {
            clsDatabaseRegjistrim dbKoka = new clsDatabaseRegjistrim();
            mbushDokumentatLidhesKoka(dbKoka.ktheGjitheKokaDokumentaLidhes(idNdermVit));
            dbKoka.Dispose();
        } 
        public colDokumentLidhesKoka()
        {
          
        }

        /// <summary>
        /// konstruktor me 2 parametra
        /// </summary>
        /// <param name="idLidhese">id lidhese</param>
        /// <param name="idKatDok">id e kategorise se dokumentit</param>
        public colDokumentLidhesKoka(int idLidhese, int idKatDok)
        {
            clsDatabaseRegjistrim db = new clsDatabaseRegjistrim();
            mbushDokumentatLidhesKoka(db.merrLidhjenDokSipasIdLidheseDheLlojit(idLidhese, idKatDok));
            db.Dispose();
        }

          /// <summary>
        /// konstruktor me 2 parametra
        /// </summary>
        /// <param name="idLidhese">id lidhese</param>
        /// <param name="idKatDok">id e kategorise se dokumentit</param>
        public colDokumentLidhesKoka(int idLidhese, int idKatDok, clsDatabaseRegjistrim db)
        {
            mbushDokumentatLidhesKoka(db.merrLidhjenDokSipasIdLidheseDheLlojit(idLidhese, idKatDok));
        }
        #endregion

        #region Metoda Private

        /// <summary>
        /// metoda perdoret gjeresisht ne rastet kur marrim te dhenat e kthyera ne ndonje store procedure ne formatin e nje dataseti
        /// nepermjet kesaj metode informacioni kalohet nga dataset-i ne nje liste me objekte te tipit 
        ///  <see cref="clsDokumentLidhesKoka"/> 
        /// </summary>
        private bool mbushDokumentatLidhesKoka(DataTable dt)
        {
            //try
            //{
                if (dt == null)
                    return true;

                foreach (DataRow rreshti in dt.Rows)
                {
                    //clsDokumentLidhesKoka koka = new clsDokumentLidhesKoka();
                    //koka.mbushDokumentLidhesKoka(rreshti);
                    Add(new clsDokumentLidhesKoka(rreshti));
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

        public clsMesazh Fshi(clsDatabaseRegjistrim db)
        {            
            foreach (clsDokumentLidhesKoka kk in this)
            {
                clsMesazh mesazh = kk.fshiDokumentDheKontabilitet(db);
                //Tani per tani kur modifikohet nje veprim banke fshihet lidhja e meparsheme e dok dhe ruhet lidhja e re. Kjo do ndryshohet me vone dhe lidhjes se vjeter do i vihet nje status dallues.
                if (!mesazh.Status)
                    return mesazh;
            }
            return new clsMesazh(true,"Ska dokumenta te lidhur");
        }
    }
}
