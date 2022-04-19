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
        //public static DataTable merrVeprimBankaDT(int idndermvit, int idkategoria, int idperdoruesi)
        //{
        //    clsDatabaseArkaBanka dbartikuj = new clsDatabaseArkaBanka();
        //    return dbartikuj.merrVeprimBankaDT(idndermvit, idkategoria, idperdoruesi);
        //}

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
            //{
                foreach (DataRow rreshti in dt.Rows)
                {
                    //clsVeprimBankaKoka koka = new clsVeprimBankaKoka();
                    //koka.mbushVeprimBankaKoka(rreshti);
                    Add(new clsVeprimBankaKoka(rreshti));
                }

            //}
            //catch (Exception)
            //{
            //    return false;
            //}
            return true;
        }

        #endregion
        //[Obsolete("Perdor: bool mbushVeprimeBankaKoka(DataTable dt)", true)]
        //public colVeprimBankaKoka mbushArrayListVeprimeBanka(DataSet ds)
        // {
        //     colVeprimBankaKoka col = new colVeprimBankaKoka();
        //     foreach (DataRow rreshti in ds.Tables[0].Rows)
        //     {
        //         clsVeprimBankaKoka koka = new clsVeprimBankaKoka();

        //         koka.IdKoka = int.Parse(rreshti[0].ToString());
        //         koka.IdBanka = int.Parse(rreshti[1].ToString());
        //         koka.Kursi= int.Parse(rreshti[2].ToString());
        //         koka.DateDokumenti = DateTime.Parse(rreshti[3].ToString());
        //         koka.DateRegjistrimi = DateTime.Parse(rreshti[4].ToString());
        //         koka.NrDokumenti = rreshti[5].ToString();
        //         koka.NrReference = int.Parse(rreshti[6].ToString());
        //         koka.NrSerial = rreshti[7].ToString();
        //         koka.PershkrimiKoka = rreshti[8].ToString();
        //         koka.IdMenyrePagese = int.Parse(rreshti[9].ToString());
        //         koka.Vlera = double.Parse(rreshti[10].ToString());
        //         koka.VleraMonedhaBaze = double.Parse(rreshti[11].ToString());
        //         koka.KomisioniBankar = double.Parse(rreshti[12].ToString());
        //         koka.KomisioniMonedhaBaze = double.Parse(rreshti[13].ToString());
        //         koka.LlojiVeprimit = rreshti[14].ToString();
        //         koka.IdPerdoruesi = int.Parse (rreshti[15].ToString());
        //         koka.IdLlojDokumenti = int.Parse(rreshti[16].ToString());
        //         koka.IdStatusDokumenti = int.Parse(rreshti[17].ToString());
        //         koka.IdNderViti = int.Parse(rreshti[18].ToString());
        //         koka.IdKonfigAmbjente = int.Parse(rreshti[19].ToString());
        //         koka.IdNivelGjenerues = int.Parse(rreshti[20].ToString());
        //         koka.IdKonfigGjenerues = int.Parse(rreshti[21].ToString());
        //         koka.IdGjenerues = int.Parse(rreshti[22].ToString());
        //         koka.IdNivel = int.Parse(rreshti[23].ToString());
        //         koka.IdDokNga = int.Parse(rreshti[24].ToString());
        //         ////DbCore.DbKontabiliteti.clsDatabaseKontabilitet data = new DbCore.DbKontabiliteti.clsDatabaseKontabilitet();

        //         ////koka.o = data.ktheKokaFleteKontabel();
        //         col.Add(koka);
        //     }
        //     return col;
        //}
    }
}
