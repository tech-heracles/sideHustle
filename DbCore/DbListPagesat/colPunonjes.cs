using System;
using System.Collections.Generic;
using System.Linq;

using System.Data;

namespace DbCore.DbListPagesat
{
    /// <summary>
    ///  Kjo eshte klasa qe mban nje liste me objekte te tipit clsPunonjes
    ///  dhe lejon te manipulohet kjo liste nepermjet indekseve
    ///  (Te dhenat nuk merren nga ndonje tabele)
    /// </summary>
    /// <seealso cref="clsPunonjes"/>
    public class colPunonjes : List<clsPunonjes>
    {
        #region Konstruktoret

        /// <summary>
        /// konstruktori pa parametra
        /// </summary>
        public colPunonjes()
        {
        }

        /// <summary>
        /// konstruktori qe implementon klasen baze
        /// </summary>
        /// <param name="collection">koleksion me clsPunonjes</param>
        public colPunonjes(IEnumerable<clsPunonjes> collection)
            : base(collection)
        {

        }
        /// <summary>
        /// konstruktori me 1 parametra
        /// merr punonjesit e nje ndermarje
        /// </summary>
        /// <param name="idndermarje">id e ndermarrjes</param>
        public colPunonjes(int idndermarje) : this(new clsDatabazeListPagesa().ktheGjithePunonjesitSipasNdermarjes(idndermarje))
        {
        }

        public colPunonjes(int idNdermarrje, bool aktiv):this(new clsDatabazeListPagesa().ktheGjithePunonjesitSipasNdermarjesAktiv(idNdermarrje))
        {
            
        }
        /// <summary>
        /// id sipas te cilave do mbushet col
        /// </summary>
        /// <param name="ids"></param>
        public colPunonjes(List<int> ids, bool KlonimHiqPunonjesTeLarguar,  DateTime dtlp) :this(new clsDatabazeListPagesa().MerrPunonjesitSipasIds(ids, KlonimHiqPunonjesTeLarguar, dtlp))
        {

        }
        public colPunonjes(string[] nrPersonals,int idNdermarrje):this(new clsDatabazeListPagesa().ktheColPunonjesSipasNrPersonal(nrPersonals,idNdermarrje))
        {

        }
      
        #endregion

        #region Metoda Publike

        /// <summary>
        /// kthen objektin <see cref="clsPunonjes"/>  qe ndodhet ne nje index te caktuar te arraylist-es
        /// </summary>
        public new clsPunonjes this[int index]{get{return base[index];}}

        /// <summary>
        /// merr punonjesin sipas id ne formen e data row
        /// </summary>
        /// <param name="idpunonjesi"> id punonjesi</param>
        /// <returns> kthen data row me kete punonjes</returns>
        public static DataRow merrPunonjesitDR( int idpunonjesi)
        {
            clsDatabazeListPagesa db = new clsDatabazeListPagesa();
            DataRow dr = db.merrPunonjesDR(idpunonjesi);
            db.Dispose();
            return dr;
        }

        /// <summary>
        /// merr punonjesit e ndermarjes ne formen e data table
        /// </summary>
        /// <param name="idnderm"> idndermarje</param>
        /// <returns> kthen data table me keta punonjes</returns>
        public static DataTable merrPunonjesNdermarjeDT(int idnderm,int idGjuha)
        {
            clsDatabazeListPagesa db = new clsDatabazeListPagesa();
            DataTable dt = db.merrPunonjesDT(idnderm,idGjuha);
            db.Dispose();
            return dt;
        }

        /// <summary>
        /// merr punonjesit e ndermarjes aktiv ne forme data table
        /// </summary>
        /// <param name="idnderm"> idndermarje</param>
        /// <returns> kthen data table me keta punonjes</returns>
        public static DataTable merrPunonjesNdermarjeDTAktiv(int idnderm, bool raportuese)
        {
            clsDatabazeListPagesa db = new clsDatabazeListPagesa();
            DataTable dt = raportuese ? db.merrPunonjesAktivNdermarjeDTNeNivelRaportues(idnderm) : db.merrPunonjesAktivNdermarjeDT(idnderm);
            db.Dispose();
            return dt;
        }
        public static DataTable merrPunonjesNdermarjeDTAktivJoTeLarguar(int idnderm, DateTime data, int iddep, int idnendep)
        {
            clsDatabazeListPagesa db = new clsDatabazeListPagesa();
            DataTable dt = db.merrPunonjesAktivNdermarjeDTJoTeLarguar(idnderm,data, iddep,idnendep);
            db.Dispose();
            return dt;
        }

        public static DataTable kthePunonjesNdermarrjesAndAutorizimeDTExport(int idnderm)
        {
            using (clsDatabazeListPagesa dbartikuj = new clsDatabazeListPagesa())
            {
                DataTable tabela = dbartikuj.kthePunonjesNdermarrjesAndAutorizimeDTExport(idnderm);
                return tabela;
            }
        }
        /// <summary>
        /// Kerkon punonjesin sipas shprehjes se kerkimit
        /// </summary>
        /// <param name="shprehjeKerkimi">(string) Shprehja e kerkimit qe vjen nga People Finder</param>
        /// <returns>Kthen nese kerkimi perfundoi me sukses apo jo</returns>
        public bool kerkoPunonjesPeopleFinder(string shprehjeKerkimi, string kodNdermarrje)
        {
            using (clsDatabazeListPagesa db = new clsDatabazeListPagesa())
            {
                bool mbush = mbushPunonjesitPeopleFinder(db.ktheGjithePunonjesitSipasKerkimitPeopleFinder(shprehjeKerkimi, kodNdermarrje));
                db.Dispose();
                return mbush;
            }
        }

        public Dictionary<string, int> ktheNrPersonalePerId()
        {
            Dictionary<string, int> punonjesit = new Dictionary<string, int>();
            foreach(clsPunonjes p in this)
            {
                punonjesit.Add(p.NrPersonal, p.IdPunonjes);
            }

            return punonjesit;
        }



        public static DataTable ktheACListePunonjesishLikeKodiEmerMbiemer(string kodi, int idNdermarrje)
        {
            using(clsDatabazeListPagesa data = new clsDatabazeListPagesa())
                return data.ktheACListePunonjesishLikeKodiEmerMbiemer(kodi, idNdermarrje);
        }

        #endregion

        #region Metoda Private
        /// <summary>
        /// Metoda perdoret vetem per rastet kur duam te mbushim punonjesin per People Finder
        /// </summary>
        /// <param name="dt"> data table me te dhena te tipit punonjes</param>
        /// <returns>true ose false nqs objekti u mbush ne rregull me te dhena</returns>
        private bool mbushPunonjesitPeopleFinder(DataTable dt)
        {
            foreach (DataRow rreshti in dt.Rows)
            {
                var komp = new clsPunonjes();
                komp.mbushPunonjesPeopleFinder(rreshti);
                Add(komp);
            }
            return true;
        }

        public static Dictionary<string,int> GetDictionaryNrPersonalIdPunonjesi(int idNdermarrje)
        {

            using (clsDatabazeListPagesa db = new clsDatabazeListPagesa())
            {
                try
                {
                    return db.GetDictionaryNrPersonalIdPunonjesi(idNdermarrje);
                }
                catch (ArgumentException argEx)
                {
                    throw new MyException($"Te pakten nje numer personal ekziston me teper se nje here per ndermarrjen {idNdermarrje}",argEx);
                }
            }
            
        }

        #endregion
    }
}
