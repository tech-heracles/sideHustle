using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DbCore.DbArkaBanka
{
    public class colVeprimBankaKoka : System.Collections.Generic.List<clsVeprimBankaKoka>
    {
        #region Konstruktoret

        public colVeprimBankaKoka()
        {
        }
        /// <summary>
        /// mbush coleksionin me gjithe veprimet e bankes ose arkes
        /// </summary>
        /// <param name="idnderviti">idja e ndervitit</param>
        /// <param name="idkatdok">id kategori dokumenti</param>
        public colVeprimBankaKoka(int idnderviti, int idkatdok)
        {
            clsDatabaseArkaBanka data = new clsDatabaseArkaBanka();
            mbushVeprimeBankaKoka(data.ktheGjitheVeprimetBanka(idnderviti, idkatdok));
        }

        public colVeprimBankaKoka(String kodi)
        {
            clsDatabaseArkaBanka data = new clsDatabaseArkaBanka();
            mbushVeprimeBankaKoka(data.ktheVeprimBankeKokaSipasKodit(kodi));
        }

        #endregion

        #region Metoda Publike

        public new clsVeprimBankaKoka this[int index]
        {
            get { return ((clsVeprimBankaKoka)base[index]); }
        }

        public static DataTable merrVeprimBankaDT(int idndermvit, int idkategoria, int idperdoruesi, string datanga, string dataderi, string lloji)
        {
            using (clsDatabaseArkaBanka dbartikuj = new clsDatabaseArkaBanka())
            {
                return dbartikuj.merrVeprimBankaDT(idndermvit, idkategoria, idperdoruesi, datanga, dataderi, lloji);
            }
        }

        public static DataTable merrArkaBankaPerImport(string emerTabKoka, string emerTabTrupi, string ndermarrjeKod, string ndermarjeKodi, string nenkategoria, int lloji, string primaryKey, bool merrTePaImportuara, bool rimerrTeImportuara, int? nrDokumentash)
        {
            clsDatabaseArkaBanka dbArkaBanka = new clsDatabaseArkaBanka();
            DataTable dt = dbArkaBanka.merrDokArkaBankaPerImport(emerTabKoka, emerTabTrupi, ndermarrjeKod, ndermarjeKodi, nenkategoria, lloji, primaryKey, merrTePaImportuara, rimerrTeImportuara, nrDokumentash);
            dbArkaBanka.Dispose();
            return dt;
        }
        public void ktheGjitheVeprimetBankaPerTuLidhur(int idkokashitje)
        {
            using (clsDatabaseArkaBanka db = new clsDatabaseArkaBanka())
            {
                mbushVeprimeBankaKoka(db.ktheGjitheVeprimetBankaPerTuLidhur(idkokashitje));
            }
        }

        /// <summary>        
        /// Kthen dokumentat e regjistrimeve Arka/Banka ne DataTable.
        /// </summary>
        /// <param name="idnderviti">(int) Id e lidhjes se ndemarrjes me vitin.</param>
        /// <param name="idperdoruesi">(int) Id e perdoruesit.</param>
        /// <returns>Kthen DataTable te dokumetave te kokes se amortizimit.</returns>
        public static DataTable kthedokVeprimeArkaBankaPerEksport(int idNdermarrje, int idPerdorues, int idNderViti, int idKatDok, int lloji, string emerTabKoka, string emerFusheId, string idPerEksport)
        {
            using (clsDatabaseArkaBanka dbArkaBanka = new clsDatabaseArkaBanka())
            {
                return dbArkaBanka.kthedokVeprimeArkaBankaPerEksport(idNdermarrje, idPerdorues, idNderViti, idKatDok, lloji, emerTabKoka, emerFusheId, idPerEksport);
            }
        }

        #endregion

        #region Metoda Private

        private bool mbushVeprimeBankaKoka(DataTable dt)
        {
            //try
                foreach (DataRow rreshti in dt.Rows)
                {
                    Add(new clsVeprimBankaKoka(rreshti));
                }

            return true;
        }

        #endregion
        //[Obsolete("Perdor: bool mbushVeprimeBankaKoka(DataTable dt)", true)]


    }
}
