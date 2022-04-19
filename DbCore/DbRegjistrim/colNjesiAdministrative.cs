using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace DbCore.DbRegjistrim
{
    /// <summary>
    ///  Kjo eshte klasa qe mban nje liste me objekte te tipit clsNjesiAdministrative
    ///  dhe lejon te manipulohet kjo liste nepermjet indekseve
    ///  (Te dhenat nuk merren nga ndonje tabele)
    /// </summary>
    public class colNjesiAdministrative : System.Collections.Generic.List<clsNjesiAdministrative>
    {
        #region Konstruktoret

        public colNjesiAdministrative()
        {


        }
        
        /// <summary>
        /// 
        /// Konstruktor qe sherben per mbushjen e magazinave per shitjen dhe blerjen
        /// </summary>
        /// <param name="idkokashitje">
        /// id e fatures
        /// </param>
        public colNjesiAdministrative(int idkokashitje)
        {
            if (idkokashitje > 0)
                using (clsDatabaseRegjistrim db = new clsDatabaseRegjistrim())
                {
                    mbushNjesiteAdministrative(db.merrNjesiAdministrativeFature(idkokashitje));
                }
        }
        public colNjesiAdministrative(int idkokashitje, colNjesiAdministrative colNjesiAd)
        {
            if (idkokashitje > 0)
                using (clsDatabaseRegjistrim db = new clsDatabaseRegjistrim())
                {
                    mbushNjesiteAdministrative(db.merrNjesiAdministrativeFature(idkokashitje), colNjesiAd);
                }
        }

        public colNjesiAdministrative(List<int> idNjesiAdm)
        {
            if (idNjesiAdm.Count <= 0)
                return;
            using (clsDatabaseRegjistrim db = new clsDatabaseRegjistrim())
            {
                mbushNjesiteAdministrative(db.merrNjesiAdministrativeSipasIdve(idNjesiAdm));
            }
        }
        public colNjesiAdministrative(List<string> kodNjesiadm, int idNdermarrje)
        {
            if (kodNjesiadm.Count <= 0)
                return;
            using (clsDatabaseRegjistrim db = new clsDatabaseRegjistrim())
            {
                mbushNjesiteAdministrative(db.merrNjesiAdministrativeSipasKodeveve(kodNjesiadm, idNdermarrje));
            }
        }
        #endregion

        #region Metoda Publike

        /// <summary>
        /// kthen objektin <see cref="DbCore.DbRegjistrim.clsNjesiAdministrative"/>  qe ndodhet ne nje index te caktuar te arraylist-es
        /// </summary> 
        public new clsNjesiAdministrative this[int index]
        {
            get { return ((clsNjesiAdministrative)base[index]); }
        }

        /// <summary>
        /// mbush gjithe njesite administrative
        /// </summary>
        /// <param name="idNderm">id e ndermarrjes</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert false</returns>
        public bool mbushGjitheNjesiAdministrative(int idNderm, int idPerdorues)
        {
            clsDatabaseRegjistrim dbNjesiAdministrative = new clsDatabaseRegjistrim();
            bool mbush = mbushNjesiteAdministrative(dbNjesiAdministrative.ktheGjitheNjesiAdministrative(idNderm, idPerdorues));
            dbNjesiAdministrative.Dispose();
            return mbush;
        }
        public bool mbushGjitheNjesiAdministrative(int idNderm, int idPerdorues, int idLlojMagazine)
        {
            clsDatabaseRegjistrim dbNjesiAdministrative = new clsDatabaseRegjistrim();
            bool mbush = mbushNjesiteAdministrative(dbNjesiAdministrative.ktheGjitheNjesiAdministrative(idNderm, idPerdorues, idLlojMagazine));
            dbNjesiAdministrative.Dispose();
            return mbush;
        }

        public bool mbushGjitheNjesiAdministrative(int idNderm)
        {
            clsDatabaseRegjistrim dbNjesiAdministrative = new clsDatabaseRegjistrim();
            bool mbush = mbushNjesiteAdministrative(dbNjesiAdministrative.ktheGjitheNjesiAdministrativePaAutorizime(idNderm));
            dbNjesiAdministrative.Dispose();
            return mbush;
        }
        public bool mbushGjitheNjesiAdministrativeSipasLlojit(int idNderm, int lloji)
        {
            using (var db = new clsDatabaseRegjistrim())
                return mbushNjesiteAdministrative(db.ktheGjitheNjesiAdministrativePaAutorizime(idNderm, lloji));
        }
        /// <summary>
        /// mbush gjithe njesite administrative aktive
        /// </summary>
        /// <param name="idNderm">id e ndermarrjes</param>
        /// <returns>kthen truen nese mbushja kryhet me sukses, ne te kunder false</returns>
        public bool mbushGjitheNjesiAdministrativeAktive(int idNderm, int idPerdorues)
        {
            using (clsDatabaseRegjistrim dbNjesiAdministrative = new clsDatabaseRegjistrim())
            {
                return mbushNjesiteAdministrative(dbNjesiAdministrative.ktheGjitheNjesiAdministrativeAktive(idNderm, idPerdorues, true));
            }
        }
        /// <summary>
        /// mbush gjithe njesite administrative aktive 
        /// </summary>
        /// <param name="idNderm"></param>
        /// <param name="idPerdorues"></param>
        /// <param name="MerrMeAutorizim">merr vleren true nese duhet te mbushet sipas autorizimeve, false perndryshe</param>
        /// <returns></returns>
        public bool mbushGjitheNjesiAdministrativeAktive(int idNderm, int idPerdorues, bool MerrMeAutorizim)
        {
            using (clsDatabaseRegjistrim dbNjesiAdministrative = new clsDatabaseRegjistrim())
            {
                return mbushNjesiteAdministrative(dbNjesiAdministrative.ktheGjitheNjesiAdministrativeAktive(idNderm, idPerdorues, MerrMeAutorizim));
            }
        }

        public bool mbushGjitheNjesiAdministrativeAktiveSipasLlojit(int idNderm, int idPerdorues, int idlloj)
        {
            clsDatabaseRegjistrim dbNjesiAdministrative = new clsDatabaseRegjistrim();
            bool mbush = mbushNjesiteAdministrative(dbNjesiAdministrative.ktheGjitheNjesiAdministrativeAktiveSipasLlojit(idNderm, idPerdorues, idlloj));
            dbNjesiAdministrative.Dispose();
            return mbush;
        }

        public static DataRow merrSipasNjesiNdermarrjesDR(int idnderm, int iddege, int idPerd)
        {
            clsDatabaseRegjistrim dbartikuj = new clsDatabaseRegjistrim();
            DataRow dr = dbartikuj.merrSipasNjesiNdermarrjesDR(idnderm, iddege, idPerd);
            dbartikuj.Dispose();
            return dr;
        }

        public static DataTable ktheTreNjesiteEParaAdministrativeAktiveMeLloj(int idNderm, int idPerdorues, bool MerrMeAutorizim)
        {
            using (clsDatabaseRegjistrim dbNjesiAdministrative = new clsDatabaseRegjistrim())
            {
                return dbNjesiAdministrative.ktheTreNjesiteEParaAdministrativeAktiveMeLloj(idNderm, idPerdorues, MerrMeAutorizim);
            }
        }

        public static DataTable merrSipasNjesiNdermarrjesDT(int idnderm, int idPerd)
        {
            clsDatabaseRegjistrim dbartikuj = new clsDatabaseRegjistrim();
            DataTable dt = dbartikuj.merrSipasNjesiNdermarrjesDT(idnderm, idPerd);
            dbartikuj.Dispose();
            return dt;
        }

        public static DataTable ktheKoordinatatGjitheNjesiNdermarrjesDT(int idnderm, int idPerd)
        {
            clsDatabaseRegjistrim dbRegj = new clsDatabaseRegjistrim();
            DataTable dt = dbRegj.ktheKoordinatatGjitheNjesiAdministrative(idnderm, idPerd);
            dbRegj.Dispose();
            return dt;
        }

        public static DataTable ktheKoordinatatGjitheNjesiAdministrativeDheShitje(int idnderm, int idPerd)
        {
            clsDatabaseRegjistrim dbRegj = new clsDatabaseRegjistrim();
            DataTable dt = dbRegj.ktheKoordinatatGjitheNjesiAdministrativeDheShitje(idnderm, idPerd);
            dbRegj.Dispose();
            return dt;
        }

        public static DataTable ktheKoordinatatGjitheNjesiAdministrativeDheMagazinaGjendje(int idnderm, int idPerd)
        {
            clsDatabaseRegjistrim dbRegj = new clsDatabaseRegjistrim();
            DataTable dt = dbRegj.ktheKoordinatatGjitheNjesiAdministrativeDheMagazinaGjendje(idnderm, idPerd);
            dbRegj.Dispose();
            return dt;
        }

        public static DataTable ktheKoordinatatGjitheNjesiAdministrativeDheAmortizimiShqiptar(int idnderm, int idPerd)
        {
            clsDatabaseRegjistrim dbRegj = new clsDatabaseRegjistrim();
            DataTable dt = dbRegj.ktheKoordinatatGjitheNjesiAdministrativeDheAmortizimiShqiptar(idnderm, idPerd);
            dbRegj.Dispose();
            return dt;
        }

        public static DataTable merrSipasNjesiNdermarrjesDTExport(int idnderm)
        {
            clsDatabaseRegjistrim dbartikuj = new clsDatabaseRegjistrim();
            DataTable dt = dbartikuj.merrSipasNjesiNdermarrjesDTExport(idnderm);
            dbartikuj.Dispose();
            return dt;
        }

        public static DataTable merrSipasNjesiNdermarjePerLupeDege(int idnderm, int idPerd, bool magNeHArte, int idLlojLayerMagazine, bool kushtMerrMagMeAutorizim)
        {
            clsDatabaseRegjistrim dbartikuj = new clsDatabaseRegjistrim();
            DataTable dt = dbartikuj.merrSipasNjesiNdermarjePerLupeDege(idnderm, idPerd, magNeHArte, idLlojLayerMagazine, kushtMerrMagMeAutorizim);
            dbartikuj.Dispose();
            return dt;
        }

        public static DataTable merrSipasNjesiNdermarjePerLupeDegeMeID(int idnderm, int idPerd, bool magNeHArte, int idLlojLayerMagazine, bool kushtMerrMagMeAutorizim, int idNjesiAdministrative)
        {
            clsDatabaseRegjistrim dbartikuj = new clsDatabaseRegjistrim();
            DataTable dt = dbartikuj.merrSipasNjesiNdermarjePerLupeDegeMeID(idnderm, idPerd, magNeHArte, idLlojLayerMagazine, kushtMerrMagMeAutorizim, idNjesiAdministrative);
            dbartikuj.Dispose();
            return dt;
        }

        public static DataTable merrSipasNjesiNdermarjePerLupeDegeSipasLlojArtikulli(int idnderm, int idPerd, int idllojmagazine)
        {
            clsDatabaseRegjistrim dbartikuj = new clsDatabaseRegjistrim();
            DataTable dt = dbartikuj.merrSipasNjesiNdermarjePerLupeDegeSipasLlojArtikulli(idnderm, idPerd, idllojmagazine);
            dbartikuj.Dispose();
            return dt;
        }

        public static DataTable merrSipasNjesiNdermarjePerLupeDegeMeFilter(string filter, long startIndex, long endIndex, int idNdermarrje, int idPerdorues, bool magNeHArte, int idLlojLayerMagazine, bool kushtMerrMagMeAutorizim)
        {
            clsDatabaseRegjistrim db = new clsDatabaseRegjistrim();
            DataTable tabela = db.merrNjesiAdministrativeSipasNdermarrjesMeFilter(filter, startIndex, endIndex, idNdermarrje, idPerdorues, magNeHArte, idLlojLayerMagazine, kushtMerrMagMeAutorizim);
            db.Dispose();
            return tabela;
        }

        public static IEnumerable<AutoCompleteItem> merrMagazinatPerAutoComplete(string infix, int idNdermarrje, int idPerdoruesi, bool meAutorizim, int llojArt)
        {
            using (clsDatabaseRegjistrim dbMagazina = new clsDatabaseRegjistrim())
            {
                return dbMagazina.MerrMagazinatPerAutoComplete(infix, idNdermarrje, idPerdoruesi, meAutorizim, llojArt).ToList();
            }
        }
        public static DataRow merrMagazinePerTrupDokumentiSipasLlojit(int idNderm, int idPerd, bool merrMeAutorizim, bool llojArt, string magazinaKokaDok, string magazinaArt)
        {
            using (clsDatabaseRegjistrim dbMagazina = new clsDatabaseRegjistrim())
            {
                return dbMagazina.merrMagazinePerTrupDokumentiSipasLlojit(idNderm, idPerd, merrMeAutorizim, llojArt, magazinaKokaDok, magazinaArt);
            }
        }

        #endregion

        #region Metoda Private

        /// <summary>
        /// metoda perdoret gjeresisht ne rastet kur marrim te dhenat e kthyera ne ndonje store procedure ne formatin e nje datatable
        ///  <see cref="DbCore.DbRegjistrim.clsNjesiAdministrative"/> 
        /// </summary>
        private bool mbushNjesiteAdministrative(DataTable dt, colNjesiAdministrative colNjesiAd)
        {
            //try
            //{

            foreach (DataRow rreshti in dt.Rows)
            {
                //clsNjesiAdministrative nivel = new clsNjesiAdministrative();
                //nivel.mbushNjesiAdministrative(rreshti, db);
                if (!rreshti.IsNull("IDNJESIADM"))
                    Add(colNjesiAd.Where(x=>x.IdNjesiAdministrative == int.Parse(rreshti["IDNJESIADM"].ToString())).FirstOrDefault());
                else
                    Add(new clsNjesiAdministrative());
            }

            //}
            //catch (Exception)
            //{
            //    return false;
            //    //throw;
            //}
            return true;
        }
        private bool mbushNjesiteAdministrative(DataTable dt)
        {
            //try
            //{

            foreach (DataRow rreshti in dt.Rows)
            {
                //clsNjesiAdministrative nivel = new clsNjesiAdministrative();
                //nivel.mbushNjesiAdministrative(rreshti, db);
                Add(new clsNjesiAdministrative(rreshti));
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
