using DbCore.IMBUtils.Logging;
using System;
using System.Data;

namespace DbCore.DbInventari
{
    /// <summary>
    ///  Kjo eshte klasa qe sherben per objektet qe perfaqsojne detajimet e  artikullit
    ///  (Te dhenat  merren nga tabela : T_DETAJIMART)
    ///  lidh artikullin me detajimin
    /// </summary>
    public class clsDetajimPerArt
    {
        #region Atributet

        private int idDetajimArt;
        private int idArtikulli;
        private int idDetajimArtikulli;
        private int llojDetajim; //me cilen nga kategorite e detajimeve te art eshte lidhur, me te paren, apo te dyten.
        private DataRow rreshti;

        #endregion

        #region Properties

        /// <summary>
        /// Kthen/Vendos ID-ne qe gjenerohet automatikisht.
        /// </summary>
        public int IdDetajimArt
        {
            get
            {
                return idDetajimArt;
            }
            set
            {
                idDetajimArt = value;
            }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne  e artikullit.
        /// </summary>
        public int IdArtikulli
        {
            get
            {
                return idArtikulli;
            }
            set
            {
                idArtikulli = value;
            }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e detajimit te artikullit.
        /// </summary>
        public int IdDetajimArtikulli
        {
            get
            {
                return idDetajimArtikulli;
            }
            set
            {
                this.idDetajimArtikulli = value;
            }

        }

        /// <summary>
        /// kthen/vendos llojin e detajimit 1- detajim i pare 2 -detajimi i dyte
        /// </summary>
        public int LlojDetajim
        {
            get
            {
                return llojDetajim;
            }
            set
            {
                llojDetajim = value;
            }
        }
        #endregion

        #region Kontruktoret

        /// <summary>
        /// konstruktori me parametra
        /// </summary>
        /// <param name="idDetajimArt"> id ritese e detajimit</param>
        /// <param name="idArtikulli"> id e artikullit</param>
        /// <param name="idDetajimArtikulli"> id e detajimit te artikullit</param>
        /// <param name="llojdetajimi">lloji i detajimit 1- detajimi i pare 2-detajimi i dyte</param>
        public clsDetajimPerArt(int idDetajimArt, int idArtikulli, int idDetajimArtikulli, int llojdetajimi)
        {
            this.idDetajimArt = idDetajimArt;
            this.idArtikulli = idArtikulli;
            this.idDetajimArtikulli = idDetajimArtikulli;
            this.llojDetajim = llojdetajimi;

        }

        /// <summary>
        /// konstruktori pa parametra
        /// </summary>
        public clsDetajimPerArt()
        {
        }

        public clsDetajimPerArt(DataRow rreshti)
        {
            
            mbushDetajimPerArtikull(rreshti);
        }

        #endregion

        #region Metoda Publike
        public static clsMesazh ruajLidhje(clsArtikulli artikull, int idDetajim, int lloji, int idNdermarrje, int idPerdorues)
        {
            using (clsDatabaseInventari data = new clsDatabaseInventari())
            {
                try
                {
                    data.beginTransaksion();
                    clsMesazh mesazh = ruajLidhje(artikull, idDetajim, lloji, idNdermarrje, idPerdorues, data);
                    if (!mesazh.Status)
                    {
                        data.rollbackTransaksion();
                        return mesazh;
                    }


                    data.commitTransaksion();
                    return mesazh;
                }
                catch
                {
                    data.rollbackTransaksion();
                    return new clsMesazh(false, "Ndodhi nje gabim gjate ruajtjes!");
                }
            }
        }

        public static clsMesazh ruajLidhje(clsArtikulli artikull, int idDetajim, int lloji, int idNdermarrje, int idPerdorues, clsDatabaseInventari data)
        {
            ImbLogger.LogWarningShitje("Filloi metoda ruajLidhje!");        
            DataTable col = DbInventari.colDetajimeArtikulli.ktheDetajimeSipasArtikullitNdermarrjesDheLlojit(artikull.KodArtikulli, idNdermarrje, idPerdorues, lloji, data);

            clsMesazh mesazh = data.ruajDetajimArt(artikull.IdArtikulli, idDetajim, lloji);           
            if (!mesazh.Status)
                return mesazh;

            if (col.Rows.Count != 0)
                mesazh = data.modifikoArt(artikull.IdArtikulli, artikull.KodArtikulli, artikull.PershkrimArtikulli, artikull.PershkrimiAngArtikulli, artikull.KodiDoganorArtikulli, artikull.VendodhjeArtikulli, artikull.Kodifikimi1Artikulli, artikull.Kodifikimi2Artikulli, artikull.OrigjineArtikulli, artikull.Njesi1Artikulli, artikull.Njesi2Artikulli, artikull.KoeficientArtikulli, artikull.IdFurnitoriKryesor, artikull.PeshaBrutoArtikulli, artikull.PeshaNetoArtikulli, true, artikull.Klasa, artikull.IdSkemaKontabilitetiArtikulli, artikull.IdLlogariInventari, artikull.IdLlogariBlerje, artikull.IdLlogariShitje, artikull.IdLlogariTeTrete, artikull.IdLlogariShpenzime, artikull.IdLlogariAmortizimi, artikull.IdLlogariPakesim, artikull.IdLlogRez, artikull.IdLlogPakRez, artikull.MinimumArtikulli, artikull.MaximumArtikulli, artikull.MetodeKostojeArtikulli, artikull.LlogaritjaKMSHArtikulli, artikull.ZevendesimAutomatikArtikulli, artikull.IdPerdoruesi, artikull.IdNdermarje, artikull.KontrollGjendje, artikull.KontrollCmimi, artikull.KontrollGjendjeArtikulli, artikull.IdTvsh, artikull.IdKonfig, artikull.Aktiv, artikull.IdStatusDok, artikull.LlojiArt, artikull.SasiNjesi, artikull.Scrap, artikull.ProdhimMePorosi, artikull.IdKategoriDetajimi, artikull.IdKategoriDetajimi2, artikull.KontrollGjendjeDetajim2, artikull.IdObjektivaKosto, artikull.IdllojGarancie, artikull.Garancia, artikull.IdMagazina, artikull.IRezervueshem, artikull.PerTransferim, artikull.Loan, artikull.Dhurate, artikull.AplikimDhurate, artikull.Pike, artikull.Vlere, artikull.KodVFOne, artikull.MeSerial, artikull.IShitshem, artikull.MbetjeShitshme, artikull.IdArtRaportuesi, artikull.PerPeshore, artikull.PershkrimFurnitori, artikull.SiperfaqjaM2, artikull.NrKontrate, artikull.NrPasurie, artikull.ZonaKadastrale, artikull.Shasia, artikull.Marka, artikull.Modeli, artikull.VitProdhimi, artikull.TeDhenaTeknika, artikull.MeBarkodLogjik, artikull.SkemaBarkodit, artikull.Kodifikimi3Artikulli, artikull.AparatBazaar, artikull.KodOferte, artikull.ArtikullIVjeter, artikull.IdFormatSeriali, artikull.MeRezerveRivleresimi, 0, false, 0, artikull.StokuMaxVfOne, artikull.KodiIBarit, artikull.IRimbursueshem);
            else if (col.Rows.Count == 0)
            {
                clsDetajimArtikulli detajim = new clsDetajimArtikulli();
                detajim.mbushDetajimArtikulliSipasId(idDetajim, data);
                mesazh = data.modifikoArt(artikull.IdArtikulli, artikull.KodArtikulli, artikull.PershkrimArtikulli, artikull.PershkrimiAngArtikulli, artikull.KodiDoganorArtikulli, artikull.VendodhjeArtikulli, artikull.Kodifikimi1Artikulli, artikull.Kodifikimi2Artikulli, artikull.OrigjineArtikulli, artikull.Njesi1Artikulli, artikull.Njesi2Artikulli, artikull.KoeficientArtikulli, artikull.IdFurnitoriKryesor, artikull.PeshaBrutoArtikulli, artikull.PeshaNetoArtikulli, true, artikull.Klasa, artikull.IdSkemaKontabilitetiArtikulli, artikull.IdLlogariInventari, artikull.IdLlogariBlerje, artikull.IdLlogariShitje, artikull.IdLlogariTeTrete, artikull.IdLlogariShpenzime, artikull.IdLlogariAmortizimi, artikull.IdLlogariPakesim, artikull.IdLlogRez, artikull.IdLlogPakRez, artikull.MinimumArtikulli, artikull.MaximumArtikulli, artikull.MetodeKostojeArtikulli, artikull.LlogaritjaKMSHArtikulli, artikull.ZevendesimAutomatikArtikulli, artikull.IdPerdoruesi, artikull.IdNdermarje, artikull.KontrollGjendje, artikull.KontrollCmimi, artikull.KontrollGjendjeArtikulli, artikull.IdTvsh, artikull.IdKonfig, artikull.Aktiv, artikull.IdStatusDok, artikull.LlojiArt, artikull.SasiNjesi, artikull.Scrap, artikull.ProdhimMePorosi, lloji==1?detajim.KategoriDetajimi:artikull.IdKategoriDetajimi, lloji==2?detajim.KategoriDetajimi:artikull.IdKategoriDetajimi2, artikull.KontrollGjendjeDetajim2, artikull.IdObjektivaKosto, artikull.IdllojGarancie, artikull.Garancia, artikull.IdMagazina, artikull.IRezervueshem, artikull.PerTransferim, artikull.Loan, artikull.Dhurate, artikull.AplikimDhurate, artikull.Pike, artikull.Vlere, artikull.KodVFOne, artikull.MeSerial, artikull.IShitshem, artikull.MbetjeShitshme, artikull.IdArtRaportuesi, artikull.PerPeshore, artikull.PershkrimFurnitori, artikull.SiperfaqjaM2, artikull.NrKontrate, artikull.NrPasurie, artikull.ZonaKadastrale, artikull.Shasia, artikull.Marka, artikull.Modeli, artikull.VitProdhimi, artikull.TeDhenaTeknika, artikull.MeBarkodLogjik, artikull.SkemaBarkodit, artikull.Kodifikimi3Artikulli, artikull.AparatBazaar, artikull.KodOferte, artikull.ArtikullIVjeter, artikull.IdFormatSeriali, artikull.MeRezerveRivleresimi, 0, false, 0, artikull.StokuMaxVfOne, artikull.KodiIBarit, artikull.IRimbursueshem);
            }
            col.Dispose();
            ImbLogger.LogWarningShitje("Mbaroi metoda ruajLidhje!");
            return mesazh;
        }

        public bool mbushDetajimArtSipasIdArtikulliDheDetajimi(int idartikulli, int iddetajim, clsDatabaseInventari dbDetajimPerArt)
        {
            return mbushDetajimPerArtikull(dbDetajimPerArt.ktheDetajimArtSipasIdArtikulliDheDetajimit(idartikulli, iddetajim));
        }
        #endregion

        #region Metoda Internal

        /// <summary>
        /// mbush detajimet per artikullin nga databaza
        /// </summary>
        /// <param name="dbDataRowDetajimPerArtikull">Data row qe duhet te mbushet nga databaza</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert false</returns>
        internal bool mbushDetajimPerArtikull(DataRow dbDataRowDetajimPerArtikull)
        {
            ImbLogger.LogTraceShitje("Filloi metoda mbushDetajimPerArtikull");
            if (dbDataRowDetajimPerArtikull != null)
            {

                try
                {
                    int.TryParse(dbDataRowDetajimPerArtikull["IDDETAJIMART"].ToString(), out idDetajimArt);
                    int.TryParse(dbDataRowDetajimPerArtikull["IDARTIKULLI"].ToString(), out idArtikulli);
                    int.TryParse(dbDataRowDetajimPerArtikull["IDDETAJIMARTIKULLI"].ToString(), out idDetajimArtikulli);
                    int.TryParse(dbDataRowDetajimPerArtikull["LLOJDETAJIM"].ToString(), out llojDetajim);
                    ImbLogger.LogTraceShitje("Mbaroi metoda mbushDetajimPerArtikull");
                    return true;
                }
                catch (InvalidCastException)
                {
                    ImbLogger.LogErrorShitje("ERROR: Gabim gjate marrjes se cross midis detajimeve dhe artikullit nga db-ja");
                    throw new Exception("ERROR: Gabim gjate marrjes se cross midis detajimeve dhe artikullit nga db-ja");
                }
            }
            else
                return false;
        }

        #endregion

    }
}
